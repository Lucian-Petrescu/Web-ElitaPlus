Imports Assurant.ElitaPlus.DataEntities

Friend Class DeductibleCollectionRule
    Inherits BaseClaimRule

    Public Sub New(ByVal pRule As Rule, ByVal pCommonManager As CommonManager)
        MyBase.New(pRule, pCommonManager)

        If (pRule.Code <> RuleCodes.DeductibleCollection) Then
            Throw New InvalidOperationException
        End If
    End Sub
    Friend Overrides Sub Execute(ByVal pClaim As Claim, pCertItemCoverage As CertificateItemCoverage)
        Dim coverageTypeCode As String = pCertItemCoverage.CoverageTypeId.ToCode(CommonManager, ListCodes.CoverageType)
        ExecuteAction(pClaim)

    End Sub
End Class
