Imports Assurant.ElitaPlus.DataEntities

Friend Class PoliceReportRequiredRule
    Inherits BaseClaimRule

    Public Sub New(ByVal pRule As Rule, ByVal pCommonManager As CommonManager)
        MyBase.New(pRule, pCommonManager)

        If (pRule.Code <> RuleCodes.PoliceReportRequired) Then
            Throw New InvalidOperationException
        End If
    End Sub

    Friend Overrides Sub Execute(ByVal pClaim As Claim, pCertItemCoverage As CertificateItemCoverage)

        Dim coverageTypeCode As String = pCertItemCoverage.CoverageTypeId.ToCode(CommonManager, ListCodes.CoverageType)
        If New List(Of String)({CoverageTypeCodes.Theft, CoverageTypeCodes.Loss, CoverageTypeCodes.TheftLoss}).Contains(coverageTypeCode) Then
            ExecuteAction(pClaim)
        End If
    End Sub
End Class
