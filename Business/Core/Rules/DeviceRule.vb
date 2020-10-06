Imports Assurant.ElitaPlus.DataEntities

Friend Class DeviceRule
    Inherits BaseClaimRule

    Public Sub New(prule As Rule, pCommonManager As CommonManager)
        MyBase.New(prule, pCommonManager)

        If (prule.Code <> RuleCodes.Device) Then
            Throw New InvalidOperationException
        End If

    End Sub

    Friend Overrides Sub Execute(pClaim As Claim, pCertItemCoverage As CertificateItemCoverage)
        Dim coverageTypeCode As String = pCertItemCoverage.CoverageTypeId.ToCode(CommonManager, ListCodes.CoverageType)

        If (coverageTypeCode <> CoverageTypeCodes.Accidental) Then
            ExecuteAction(pClaim)
        End If
    End Sub
End Class
