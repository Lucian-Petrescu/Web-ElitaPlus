Imports Assurant.ElitaPlus.DataEntities

Friend Class UpgradeRule
    Inherits BaseClaimRule

    Public Sub New(prule As Rule, pCommonManager As CommonManager)
        MyBase.New(prule, pCommonManager)

        If (prule.Code <> RuleCodes.Upgrade) Then
            Throw New InvalidOperationException
        End If

    End Sub


    Friend Overrides Sub Execute(pClaim As Claim, pCertItemCoverage As CertificateItemCoverage)
        If (pClaim.LossDate > pCertItemCoverage.Certificate.WarrantySalesDate.Value.AddMonths(12)) Then
            ExecuteAction(pClaim)
        End If
    End Sub
End Class
