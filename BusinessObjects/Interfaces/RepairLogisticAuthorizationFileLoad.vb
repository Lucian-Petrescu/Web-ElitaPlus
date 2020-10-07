Imports Assurant.Common.MessagePublishing

Public Class RepairLogisticAuthorizationFileLoad
    Inherits FileLoadBase(Of ClaimloadFileProcessed, ClaimloadReconWrk)

#Region "Constructor"
    Public Sub New()
        MyBase.New(True) '' Custom Save Constructor
    End Sub
#End Region

#Region "Fields"
    Private _claimloadFileProcessed As ClaimloadFileProcessed
    Private _claim As MultiAuthClaim
#End Region

#Region "Properties"
    Private Property ClaimLoadFileProcessed As ClaimloadFileProcessed
        Get
            Return _claimloadFileProcessed
        End Get
        Set
            _claimloadFileProcessed = value
        End Set
    End Property

    Private Property Claim As MultiAuthClaim
        Get
            Return _claim
        End Get
        Set
            _claim = value
        End Set
    End Property
#End Region

    Protected Overrides Function CreateFileLoadHeader(ByVal fileLoadHeaderId As System.Guid) As ClaimloadFileProcessed
        ClaimLoadFileProcessed = New ClaimloadFileProcessed(fileLoadHeaderId)
        Return ClaimLoadFileProcessed
    End Function

    Protected Overrides Function CreateFileLoadDetail(ByVal fileLoadDetailId As System.Guid, ByVal headerRecord As ClaimloadFileProcessed) As ClaimloadReconWrk
        Dim returnValue As ClaimloadReconWrk
        returnValue = New ClaimloadReconWrk(fileLoadDetailId, headerRecord.Dataset)
        Return returnValue
    End Function

    Protected Overrides Sub CustomSave(ByVal headerRecord As ClaimloadFileProcessed)
        MyBase.CustomSave(headerRecord)
        headerRecord.Save(Claim)
    End Sub

    Protected Overrides Function ProcessDetailRecord(ByVal reconRecord As ClaimloadReconWrk, ByVal familyDataSet As DataSet) As ProcessResult
        Try
            Dim claim As MultiAuthClaim
            Dim claimAuthorization As ClaimAuthorization
            Dim claimAuthDetails As ClaimAuthDetail

            claim = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(reconRecord.OriginalClaimId)
            familyDataSet = claim.Dataset
            claimAuthorization = claim.ClaimAuthorizationChildren.GetChild(reconRecord.ClaimAuthorizationId)

            If (Not reconRecord.BatchNumber Is Nothing AndAlso reconRecord.BatchNumber.Trim().Length > 0) Then
                claimAuthorization.BatchNumber = reconRecord.BatchNumber.Trim()
                claimAuthorization.Save()
            End If

            If (Not reconRecord.DeductibleCollected Is Nothing) Then
                If (claimAuthorization.ContainsDeductible) Then
                    claimAuthorization.Claim.Deductible = reconRecord.DeductibleCollected
                End If
            End If

            Me.Claim = claim

            Return ProcessResult.Loaded
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
End Class
