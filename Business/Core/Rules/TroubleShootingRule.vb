Imports Assurant.ElitaPlus.DataEntities

Friend Class TroubleShootingRule
    Inherits BaseClaimRule

    Public Sub New(pRule As Rule, pCommonManager As CommonManager)
        MyBase.New(pRule, pCommonManager)

        If (pRule.Code <> RuleCodes.Troubleshooting) Then
            Throw New InvalidOperationException
        End If
    End Sub

    Friend Overrides Sub Execute(pClaim As Claim, pCertItemCoverage As CertificateItemCoverage)

        Dim coverageTypeCode As String = pCertItemCoverage.CoverageTypeId.ToCode(CommonManager, ListCodes.CoverageType)
        If New List(Of String)({CoverageTypeCodes.Accidental, CoverageTypeCodes.MechanicalBreakdown}).Contains(coverageTypeCode) Then
            ExecuteAction(pClaim)
        End If
    End Sub
End Class
