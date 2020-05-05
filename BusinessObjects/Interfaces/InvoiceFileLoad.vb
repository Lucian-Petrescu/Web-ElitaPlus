Imports System.Linq
Imports System.Collections.Generic
Imports Assurant.Common.MessagePublishing


Public NotInheritable Class InvoiceFileLoad
    Inherits FileLoadBase(Of ClaimloadFileProcessed, InvoiceReconWrk)

    Private Const KEY_CHANGED__INVOICE As String = "INVOICE"
    Private Const KEY_CHANGED__CLAIM_AUTHORIZATION As String = "CLAIM_AUTHORIZATION"

    Protected invoice As Invoice

    Protected Overrides Function CreateFileLoadHeader(ByVal fileLoadHeaderId As System.Guid) As ClaimloadFileProcessed
        Return New ClaimloadFileProcessed(fileLoadHeaderId)
    End Function

    Protected Overrides Function CreateFileLoadDetail(ByVal fileLoadDetailId As System.Guid, ByVal headerRecord As ClaimloadFileProcessed) As InvoiceReconWrk
        Dim returnValue As InvoiceReconWrk
        returnValue = New InvoiceReconWrk(fileLoadDetailId, headerRecord.Dataset)
        Return returnValue
    End Function

    Public Overrides Sub AfterCreateFileLoadHeader()
        MyBase.AfterCreateFileLoadHeader()
    End Sub

    Public Overrides Function BeforeSave(ByVal familyDataSet As DataSet) As Object
        Dim invoiceList As New List(Of Guid)
        MyBase.BeforeSave(familyDataSet)

        If (familyDataSet.Tables.Contains(InvoiceDAL.TABLE_NAME)) Then
            For Each dr As DataRow In familyDataSet.Tables(InvoiceDAL.TABLE_NAME).Rows
                Dim oInvoice As Invoice = New Invoice(dr)
                invoiceList.Add(oInvoice.Id)
            Next
        End If

        Return invoiceList
    End Function

    Public Overrides Sub AfterSave(ByVal argument As Object, ByVal familyDataSet As DataSet)
        MyBase.AfterSave(argument, familyDataSet)
        Dim invoiceList As List(Of Guid) = DirectCast(argument, List(Of Guid))
        For Each invoiceId As Guid In invoiceList
            Dim oInvoice As Invoice = New Invoice(invoiceId)
            If (oInvoice.Balance.CanExecute) Then oInvoice.Balance.Execute()
        Next
    End Sub

    Protected Overrides Function IsKeyChanged(ByVal beforeReconRecord As InvoiceReconWrk, ByVal afterReconRecord As InvoiceReconWrk, ByVal familyDataSet As DataSet) As KeyChangeReturnType
        Dim returnValue As KeyChangeReturnType
        returnValue.IsChanged = False
        If (beforeReconRecord Is Nothing) Then
            returnValue.Key = KEY_CHANGED__INVOICE
            returnValue.IsChanged = True
        Else
            If (beforeReconRecord.InvoiceNumber = afterReconRecord.InvoiceNumber) Then
                If (beforeReconRecord.AuthorizationId.Equals(afterReconRecord.AuthorizationId)) Then
                    returnValue.IsChanged = False
                Else
                    returnValue.Key = KEY_CHANGED__CLAIM_AUTHORIZATION
                    returnValue.IsChanged = True
                End If
            Else
                returnValue.Key = KEY_CHANGED__INVOICE
                returnValue.IsChanged = True
            End If
        End If
        Return returnValue
    End Function

    Protected Overrides Sub KeyChanged(ByVal key As String, ByVal beforeReconRecord As InvoiceReconWrk, ByVal afterReconRecord As InvoiceReconWrk, ByVal familyDataSet As DataSet)
        ' Invoice Changed
        If (key = KEY_CHANGED__INVOICE) Then
            If (afterReconRecord.InvoiceId.Equals(Guid.Empty)) Then
                ' Create New Invoice
                invoice = New Invoice(familyDataSet)
                With invoice
                    invoice.DueDate = afterReconRecord.DueDate
                    invoice.InvoiceDate = afterReconRecord.InvoiceDate
                    invoice.InvoiceNumber = afterReconRecord.InvoiceNumber
                    invoice.InvoiceStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_INVOICE_STATUS, Codes.INVOICE_STATUS__NEW)
                    invoice.ServiceCenterId = afterReconRecord.ServiceCenterId
                    invoice.Source = Me.Header.Filename
                    invoice.IsComplete = True
                End With
            Else
                ' Load Existing Invoice
                invoice = New Invoice(afterReconRecord.InvoiceId)
                ' Force Un-Balance
                If (invoice.UndoBalance.CanExecute) Then
                    invoice.UndoBalance.Execute()
                End If
                invoice = New Invoice(afterReconRecord.InvoiceId, familyDataSet)
            End If
        End If

        ' Claim Authorization Changed
        If (key = KEY_CHANGED__INVOICE OrElse key = KEY_CHANGED__CLAIM_AUTHORIZATION) Then

            Dim oClaimAuthorization As ClaimAuthorization
            oClaimAuthorization = invoice.ClaimAuthorizations.Where(Function(item) item.ClaimAuthorizationId = afterReconRecord.AuthorizationId).FirstOrDefault()

            If (Not oClaimAuthorization Is Nothing AndAlso oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled) Then
                ' Check if Invoice has current Claim Authorization
                For Each invoiceItem As InvoiceItem In invoice.InvoiceItemChildren.Where(Function(item) item.ClaimAuthorizationId.Equals(afterReconRecord.AuthorizationId))
                    invoiceItem.Delete()
                Next
            End If
        End If
        MyBase.KeyChanged(key, beforeReconRecord, afterReconRecord, familyDataSet)
    End Sub

    Protected Overrides Function ProcessDetailRecord(ByVal reconRecord As InvoiceReconWrk, ByVal familyDataSet As DataSet) As ProcessResult
        Dim invoiceItem As InvoiceItem
        Try
            Dim oClaimAuthorization As ClaimAuthorization
            oClaimAuthorization = invoice.ClaimAuthorizations.Where(Function(item) item.ClaimAuthorizationId = reconRecord.AuthorizationId).FirstOrDefault()

            If (oClaimAuthorization Is Nothing OrElse oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled) Then
                invoice.Source = Me.Header.Filename
                invoiceItem = invoice.GetNewInvoiceItemChild
                invoice.InvoiceAmount = GetDecimalValue(invoice.InvoiceAmount) + reconRecord.Amount.Value
                invoiceItem.Amount = reconRecord.Amount
                invoiceItem.ClaimAuthorizationId = reconRecord.AuthorizationId
                invoiceItem.LineItemNumber = reconRecord.LineItemNumber
                invoiceItem.ServiceClassId = reconRecord.ServiceClassId
                invoiceItem.ServiceLevelId = reconRecord.ServiceLevelId
                invoiceItem.ServiceTypeId = reconRecord.ServiceTypeId
                invoiceItem.VendorSku = reconRecord.VendorSku
                invoiceItem.VendorSkuDescription = reconRecord.VendorSkuDescription
                If invoiceItem.ClaimAuthorization.RepairDate Is Nothing And Not reconRecord.RepairDate Is Nothing Then
                    invoiceItem.ClaimAuthorization.RepairDate = reconRecord.RepairDate
                End If
                invoice.Save()
                Return ProcessResult.Loaded
            Else
                reconRecord.RejectCode = "028"
                reconRecord.RejectReason = "Authorization number is not in valid status"
                Return ProcessResult.Rejected
            End If
        Catch ex As DataBaseAccessException
            Common.AppConfig.Log(DirectCast(ex, Exception))
            If (ex.ErrorType = DataBaseAccessException.DatabaseAccessErrorType.BusinessErr) Then
                If (ex.Code Is Nothing OrElse ex.Code.Trim().Length = 0) Then
                    reconRecord.RejectReason = "Rejected During Load process"
                Else
                    reconRecord.RejectReason = TranslationBase.TranslateLabelOrMessage(ex.Code)
                    reconRecord.RejectReason = reconRecord.RejectReason.Substring(0, Math.Min(60, reconRecord.RejectReason.Length))
                End If
            Else
                reconRecord.RejectReason = "Rejected During Load process"
            End If
            reconRecord.RejectCode = "000"
            Return ProcessResult.Rejected
        Catch ex As BOValidationException
            Common.AppConfig.Log(DirectCast(ex, Exception))
            reconRecord.RejectCode = "000"
            reconRecord.RejectReason = ex.ToRejectReason()
            reconRecord.RejectReason = reconRecord.RejectReason.Substring(0, Math.Min(60, reconRecord.RejectReason.Length))
            Return ProcessResult.Rejected
        Catch ex As Exception
            Common.AppConfig.Log(ex)
            reconRecord.RejectCode = "000"
            reconRecord.RejectReason = "Rejected During Load process"
            Return ProcessResult.Rejected
        End Try
    End Function

    Private Function GetDecimalValue(ByVal decimalObj As DecimalType, Optional ByVal decimalDigit As Integer = 2) As Decimal
        If decimalObj Is Nothing Then
            Return 0D
        Else
            Return Math.Round(decimalObj.Value, decimalDigit, MidpointRounding.AwayFromZero)
        End If
    End Function
End Class
