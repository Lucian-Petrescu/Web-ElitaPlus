Imports Assurant.ElitaPlus.DataEntities

Friend Class ClaimDocumentsRequiredRule
    Inherits BaseClaimRule
    Public Sub New(pRule As Rule, pCommonManager As CommonManager)
        MyBase.New(pRule, pCommonManager)

        If (pRule.Code <> RuleCodes.ClaimDocumentsRequired) Then
            Throw New InvalidOperationException
        End If
    End Sub

    Friend Overrides Sub Execute(pClaim As Claim, pCertItemCoverage As CertificateItemCoverage)
        ExecuteAction(pClaim)
    End Sub
End Class
