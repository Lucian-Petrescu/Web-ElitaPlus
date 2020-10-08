Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports System.Linq
Imports System.Xml.Linq

Public Class CommBrkdwnValidation
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const XML_VERSION_AND_ENCODING As String = "<?xml version='1.0' encoding='utf-8' ?>"
    Private Const TABLE_NAME_TRANSACTION_DATA_RECORD As String = "TRANSACTION_DATA_RECORD"

    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "TRANSACTION_ID"
    Public Const DATA_COL_NAME_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const DATA_COL_NAME_RESULT As String = "RESULT"
    Public Const DATA_COL_NAME_ERROR As String = "ERROR"
    Public Const DATA_COL_NAME_CODE As String = "CODE"
    Public Const DATA_COL_NAME_MESSAGE As String = "MESSAGE"
    Public Const DATA_COL_NAME_ERROR_INFO As String = "ERROR_INFO"

    Private Const TABLE_NAME As String = "CommBrkdwnValidation"
    Private Const TABLE_RESULT As String = "RESULT"

    Private Const VALUE_OK As String = "OK"
    Private Const YESNO As String = "YESNO"
#End Region

#Region "Constructors"

    Public Sub New(ds As CommBrkdwnValidationDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

        dsMyCommBrkdwnValidation = ds
    End Sub

    Public Sub New(ds As CommBrkdwnValidationDs, xml As String)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

        dsMyCommBrkdwnValidation = ds
    End Sub

#End Region

#Region "Private Members"

    Dim dsMyCommBrkdwnValidation As CommBrkdwnValidationDs
    Dim _uploadSessionId As String
    Dim _uploadSessionGuidId As Guid = Guid.Empty

    Private Sub MapDataSet(ds As CommBrkdwnValidationDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New DataSet
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ds As CommBrkdwnValidationDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            'Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("CommBrkdwnValidation Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As CommBrkdwnValidationDs)
        Try
            If ds.CommBrkdwnValidation.Count = 0 Then Exit Sub
            With ds.CommBrkdwnValidation.Item(0)
                UploadSessionId = .UPLOAD_SESSION_ID
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property UploadSessionId As String
        Get
            Return _uploadSessionId
        End Get
        Set
            _uploadSessionId = Value
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            'Load the pre-validated Dealer level Commission Entry Breakdown records
            Dim dsPreValidatedDealerCommBrkdwnRecords As DataSet = CommEntyBrkdwnUpload.GetPreValidatedCommEntyBrkdwnsForDealer(UploadSessionId)

            If dsPreValidatedDealerCommBrkdwnRecords IsNot Nothing AndAlso dsPreValidatedDealerCommBrkdwnRecords.Tables(0) IsNot Nothing AndAlso dsPreValidatedDealerCommBrkdwnRecords.Tables(0).Rows.Count > 0 Then
                Dim preValidatedDealerCommBrkdwnRow As DataRow
                Dim commbrkdwnErrors As ValidationError()
                Dim strCommonValidationErrors As String = String.Empty
                Dim strValidationErrors As String = String.Empty
                Dim index As Integer
                Dim commpercentTotal As Decimal
                Dim markuppercentTotal As Decimal
                Dim restrictMarkup As Boolean = False

                For Each preValidatedDealerCommBrkdwnRow In dsPreValidatedDealerCommBrkdwnRecords.Tables(0).Rows
                    commpercentTotal = New DecimalType(CType(preValidatedDealerCommBrkdwnRow("comm_percent_total"), Decimal))
                    markuppercentTotal = New DecimalType(CType(preValidatedDealerCommBrkdwnRow("markup_percent_total"), Decimal))

                    Dim objCommissionPeriodData = New CommissionPeriodData()
                    Dim companyList As New ArrayList
                    companyList.Add(New Guid(CType(preValidatedDealerCommBrkdwnRow(CommissionPeriodDAL.COL_NAME_COMPANY_ID), Byte())))
                    objCommissionPeriodData.dealerId = New Guid(CType(preValidatedDealerCommBrkdwnRow(CommissionPeriodDAL.COL_NAME_DEALER_ID), Byte()))
                    objCommissionPeriodData.companyIds = companyList

                    Try
                        restrictMarkup = CommissionPeriod.GetRestrictMarkup(objCommissionPeriodData, False)
                    Catch boval As BOValidationException
                        If boval IsNot Nothing Then
                            Dim BOValidationErrors As ValidationError()
                            BOValidationErrors = boval.ValidationErrorList()
                            If BOValidationErrors.Length > 0 Then
                                For index = 0 To BOValidationErrors.Length - 1
                                    strCommonValidationErrors &= "Error Message: " & TranslationBase.TranslateLabelOrMessage(CType(BOValidationErrors.GetValue(index), Assurant.Common.Validation.ValidationError).Message)
                                    strCommonValidationErrors &= Environment.NewLine
                                Next
                            End If
                        End If
                        boval = Nothing
                        restrictMarkup = False
                    Catch ex As Exception
                        ex = Nothing
                        restrictMarkup = False
                    End Try

                    ' Commission Period
                    Dim objCommPeriod As CommissionPeriod = New CommissionPeriod
                    With objCommPeriod
                        'Dealer
                        .DealerId = New Guid(CType(preValidatedDealerCommBrkdwnRow(CommissionPeriodDAL.COL_NAME_DEALER_ID), Byte()))
                        'Compute Method
                        .ComputeMethodId = New Guid(CType(preValidatedDealerCommBrkdwnRow(CommissionPeriodDAL.COL_NAME_COMPUTE_METHOD_ID), Byte()))
                        'Effective
                        Dim commperiodEffectiveDate As DateType
                        If preValidatedDealerCommBrkdwnRow(CommissionPeriodDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                            commperiodEffectiveDate = Nothing
                        Else
                            commperiodEffectiveDate = New DateType(CType(preValidatedDealerCommBrkdwnRow(CommissionPeriodDAL.COL_NAME_EFFECTIVE_DATE), Date))
                        End If
                        'Expiration
                        Dim commperiodExpirationDate As DateType
                        If preValidatedDealerCommBrkdwnRow(CommissionPeriodDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                            commperiodExpirationDate = Nothing
                        Else
                            commperiodExpirationDate = New DateType(CType(preValidatedDealerCommBrkdwnRow(CommissionPeriodDAL.COL_NAME_EXPIRATION_DATE), Date))
                        End If

                        If commperiodEffectiveDate = Nothing Or commperiodExpirationDate = Nothing Then
                            Select Case objCommPeriod.ExpirationCount(objCommissionPeriodData)
                                Case 0
                                    .EffectiveDate = New DateType(Date.Today.AddDays(-1))
                                    .ExpirationDate = New DateType(Date.Today.AddYears(1))
                                    Exit Select
                                Case Else
                                    Dim maxExpiration As Date = objCommPeriod.MaxExpiration(objCommissionPeriodData)
                                    .EffectiveDate = maxExpiration.AddDays(1)
                                    .ExpirationDate = maxExpiration.AddYears(1)
                            End Select
                        Else
                            .EffectiveDate = commperiodEffectiveDate
                            .ExpirationDate = commperiodExpirationDate
                        End If
                    End With

                    ' Commission Tolerance
                    Dim objCommTolerance As CommissionTolerance = New CommissionTolerance
                    With objCommTolerance
                        .AllowedMarkupPct = New DecimalType(CType(preValidatedDealerCommBrkdwnRow(CommissionToleranceDAL.COL_NAME_ALLOWED_MARKUP_PCT), Decimal))
                        .Tolerance = New DecimalType(CType(preValidatedDealerCommBrkdwnRow(CommissionToleranceDAL.COL_NAME_TOLERANCE), Decimal))
                    End With

                    ' Attach Commission Tolerance to Commission Period
                    objCommPeriod.AttachTolerance(objCommTolerance)

                    'Load the pre-validated Commission Entry Breakdown records
                    Dim dsPreValidatedCommBrkdwnRecords As DataSet = CommEntyBrkdwnUpload.GetPreValidatedCommEntyBrkdwnsForUpload(UploadSessionId, objCommPeriod.DealerId)

                    If dsPreValidatedCommBrkdwnRecords IsNot Nothing AndAlso dsPreValidatedCommBrkdwnRecords.Tables(0) IsNot Nothing AndAlso dsPreValidatedCommBrkdwnRecords.Tables(0).Rows.Count > 0 Then

                        Dim preValidatedCommBrkdwnRow As DataRow
                        For Each preValidatedCommBrkdwnRow In dsPreValidatedCommBrkdwnRecords.Tables(0).Rows

                            Dim preValidatedCommBrkdwnId As New Guid(CType(preValidatedCommBrkdwnRow("comm_brkdwn_upload_id"), Byte()))
                            Dim objCommEntyBrkdwnUpload As New CommEntyBrkdwnUpload(preValidatedCommBrkdwnId)

                            ' Commission Entity
                            Dim objCommEntity As CommissionEntity
                            Dim objBankInfo As BankInfo

                            If objCommEntyBrkdwnUpload.EntityId = Nothing Then
                                ' Commission Entity
                                objCommEntity = New CommissionEntity()
                                BuildCommEntity(objCommEntity, objCommEntyBrkdwnUpload)

                                If objCommEntyBrkdwnUpload.PaymentMethodId = LookupListNew.GetIdFromCode("PMTHD", "CTT") Then
                                    ' Bank Info
                                    objBankInfo = objCommEntity.Add_BankInfo()
                                    BuildBankInfo(objBankInfo, objCommEntyBrkdwnUpload)
                                End If

                                ' Attach CommissionPeriodEntity to CommissionPeriod
                                objCommPeriod.AttachPeriodEntity(objCommEntity.Id, objCommEntyBrkdwnUpload.Position, objCommEntyBrkdwnUpload.PayeeTypeId)
                            Else
                                objCommPeriod.AttachPeriodEntity(objCommEntyBrkdwnUpload.EntityId, objCommEntyBrkdwnUpload.Position, objCommEntyBrkdwnUpload.PayeeTypeId)
                            End If

                            ' Associate Commission
                            Dim objAssociateCommissions As AssociateCommissions
                            objAssociateCommissions = objCommPeriod.AddAssocComm(Guid.Empty)
                            BuildAssociateCommissions(objAssociateCommissions, objCommEntyBrkdwnUpload)
                            objAssociateCommissions.CommTotal = commpercentTotal
                            If restrictMarkup Then
                                objAssociateCommissions.MarkupTotal = markuppercentTotal
                            Else
                                objAssociateCommissions.MarkupTotal = 100
                            End If
                            objAssociateCommissions.CommissionToleranceId = objCommTolerance.Id
                            objCommTolerance.AttachAsscComm(objCommTolerance.Dataset, objAssociateCommissions)

                            ' Validations
                            strValidationErrors &= strCommonValidationErrors
                            Dim commentityErrors As ValidationError()
                            If objCommEntity IsNot Nothing Then
                                commentityErrors = objCommEntity.ValidationErrors()
                                If commentityErrors.Length > 0 Then
                                    For index = 0 To commentityErrors.Length - 1
                                        strValidationErrors &= "PropertyName: " & CType(commentityErrors.GetValue(index), Assurant.Common.Validation.ValidationError).PropertyName & "; Error Message: " & TranslationBase.TranslateLabelOrMessage(CType(commentityErrors.GetValue(index), Assurant.Common.Validation.ValidationError).Message)
                                        strValidationErrors &= Environment.NewLine
                                    Next
                                End If
                            End If

                            Dim bankinfoErrors As ValidationError()
                            If objBankInfo IsNot Nothing Then
                                bankinfoErrors = objBankInfo.ValidationErrors()
                                If bankinfoErrors.Length > 0 Then
                                    For index = 0 To bankinfoErrors.Length - 1
                                        strValidationErrors &= "PropertyName: " & CType(bankinfoErrors.GetValue(index), Assurant.Common.Validation.ValidationError).PropertyName & "; Error Message: " & TranslationBase.TranslateLabelOrMessage(CType(bankinfoErrors.GetValue(index), Assurant.Common.Validation.ValidationError).Message)
                                        strValidationErrors &= Environment.NewLine
                                    Next
                                End If
                            End If

                            Dim associatecommissionErrors As ValidationError()
                            If objAssociateCommissions IsNot Nothing Then
                                associatecommissionErrors = objAssociateCommissions.ValidationErrors()
                                If associatecommissionErrors.Length > 0 Then
                                    For index = 0 To associatecommissionErrors.Length - 1
                                        strValidationErrors &= "PropertyName: " & CType(associatecommissionErrors.GetValue(index), Assurant.Common.Validation.ValidationError).PropertyName & "; Error Message: " & TranslationBase.TranslateLabelOrMessage(CType(associatecommissionErrors.GetValue(index), Assurant.Common.Validation.ValidationError).Message)
                                        strValidationErrors &= Environment.NewLine
                                    Next
                                End If
                            End If

                            Dim commperiodErrors As ValidationError()
                            If objCommPeriod IsNot Nothing Then
                                commperiodErrors = objCommPeriod.ValidationErrors()
                                If commperiodErrors.Length > 0 Then
                                    For index = 0 To commperiodErrors.Length - 1
                                        strValidationErrors &= "PropertyName: " & CType(commperiodErrors.GetValue(index), Assurant.Common.Validation.ValidationError).PropertyName & "; Error Message: " & TranslationBase.TranslateLabelOrMessage(CType(commperiodErrors.GetValue(index), Assurant.Common.Validation.ValidationError).Message)
                                        strValidationErrors &= Environment.NewLine
                                    Next
                                End If
                            End If

                            If strValidationErrors <> String.Empty Then
                                objCommEntyBrkdwnUpload.ValidationErrors = "Record Number " & preValidatedCommBrkdwnRow.Item("record_number").ToString & Environment.NewLine & strValidationErrors
                                objCommEntyBrkdwnUpload.Save()
                                strValidationErrors = String.Empty
                            End If

                        Next

                    End If

                Next

            End If

            Return XMLHelper.GetXML_OK_Response

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function CheckDBNull(obj As Object) As Object
        If DBNull.Value.Equals(obj) Then
            If obj.GetType Is GetType(DecimalType) Then
                Return 0
            Else
                Return Nothing
            End If
        Else
            Return obj
        End If
    End Function

    Private Sub BuildCommPeriod(ByRef objCommPeriod As CommissionPeriod, objCommEntyBrkdwnUpload As CommEntyBrkdwnUpload)
        If objCommEntyBrkdwnUpload IsNot Nothing Then
            objCommPeriod = New CommissionPeriod
            With objCommPeriod
                .DealerId = objCommEntyBrkdwnUpload.DealerId
                .ComputeMethodId = objCommEntyBrkdwnUpload.ComputeMethodId
                'Effective & Expiration
                If objCommEntyBrkdwnUpload.Effective = Nothing Or objCommEntyBrkdwnUpload.Expiration = Nothing Then
                    Dim objCommissionPeriodData = New CommissionPeriodData()
                    Dim companyList As New ArrayList
                    companyList.Add(objCommEntyBrkdwnUpload.CompanyId)
                    objCommissionPeriodData.dealerId = objCommEntyBrkdwnUpload.DealerId
                    objCommissionPeriodData.companyIds = companyList
                    Select Case objCommPeriod.ExpirationCount(objCommissionPeriodData)
                        Case 0
                            .EffectiveDate = New DateType(Date.Today.AddDays(-1))
                            .ExpirationDate = New DateType(Date.Today.AddYears(1))
                            Exit Select
                        Case Else
                            Dim maxExpiration As Date = objCommPeriod.MaxExpiration(objCommissionPeriodData)
                            .EffectiveDate = maxExpiration.AddDays(1)
                            .ExpirationDate = maxExpiration.AddYears(1)
                    End Select
                Else
                    .EffectiveDate = objCommEntyBrkdwnUpload.Effective
                    .ExpirationDate = objCommEntyBrkdwnUpload.Expiration
                End If
            End With
        End If
    End Sub

    Private Sub BuildCommEntity(ByRef objCommEntity As CommissionEntity, objCommEntyBrkdwnUpload As CommEntyBrkdwnUpload)
        If objCommEntyBrkdwnUpload IsNot Nothing Then
            objCommEntity = New CommissionEntity()
            With objCommEntity
                .EntityName = objCommEntyBrkdwnUpload.EntityName
                .Phone = objCommEntyBrkdwnUpload.Phone
                .Email = objCommEntyBrkdwnUpload.Email
                .PaymentMethodId = objCommEntyBrkdwnUpload.PaymentMethodId
                .CompanyGroupId = objCommEntyBrkdwnUpload.CompanyGroupId
                .Address1 = objCommEntyBrkdwnUpload.Address1
                .Address2 = objCommEntyBrkdwnUpload.Address2
                .City = objCommEntyBrkdwnUpload.City
                .RegionId = objCommEntyBrkdwnUpload.RegionId
                .PostalCode = objCommEntyBrkdwnUpload.PostalCode
                .CountryId = objCommEntyBrkdwnUpload.CountryId
                .DisplayId = objCommEntyBrkdwnUpload.DisplayId
                .TaxId = objCommEntyBrkdwnUpload.TaxId
                .CommissionEntityTypeid = objCommEntyBrkdwnUpload.CommissionEntityTypeid
            End With
        End If
    End Sub

    Private Sub BuildAssociateCommissions(ByRef objAssociateCommissions As AssociateCommissions, objCommEntyBrkdwnUpload As CommEntyBrkdwnUpload)
        If objCommEntyBrkdwnUpload IsNot Nothing Then
            objAssociateCommissions = New AssociateCommissions
            With objAssociateCommissions
                .CommissionPercent = objCommEntyBrkdwnUpload.CommissionPercent
                .MarkupPercent = objCommEntyBrkdwnUpload.MarkupPercent
            End With
        End If
    End Sub

    Private Sub BuildBankInfo(ByRef objBankInfo As BankInfo, objCommEntyBrkdwnUpload As CommEntyBrkdwnUpload)
        If objCommEntyBrkdwnUpload IsNot Nothing Then
            objBankInfo = New BankInfo()
            With objBankInfo
                .CountryID = objCommEntyBrkdwnUpload.BankCountryId
                .Account_Name = objCommEntyBrkdwnUpload.AccountName
                .Bank_Id = objCommEntyBrkdwnUpload.BankId
                .Account_Number = objCommEntyBrkdwnUpload.AccountNumber
                .SwiftCode = objCommEntyBrkdwnUpload.SwiftCode
                .IbanNumber = objCommEntyBrkdwnUpload.IbanNumber
                .PaymentReasonID = objCommEntyBrkdwnUpload.PaymentReasonId
                .AccountTypeId = objCommEntyBrkdwnUpload.AccountTypeId
                .BranchName = objCommEntyBrkdwnUpload.BranchName
                .BankName = objCommEntyBrkdwnUpload.BankName
                .BankSortCode = objCommEntyBrkdwnUpload.BankSortCode
                .BankSubCode = objCommEntyBrkdwnUpload.BankSubCode
                .TransactionLimit = objCommEntyBrkdwnUpload.TransactionLimit
                .BankLookupCode = objCommEntyBrkdwnUpload.BankLookupCode
                .BranchDigit = objCommEntyBrkdwnUpload.BranchDigit
                .AccountDigit = objCommEntyBrkdwnUpload.AccountDigit
                .BranchNumber = objCommEntyBrkdwnUpload.BranchNumber
                .TaxId = objCommEntyBrkdwnUpload.BankTaxId
            End With
        End If
    End Sub

#End Region

#Region "Extended Properties"

#End Region

End Class