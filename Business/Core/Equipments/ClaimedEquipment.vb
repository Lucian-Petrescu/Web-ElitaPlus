Imports Assurant.ElitaPlus.DataEntities

Friend MustInherit Class ClaimedEquipment
    Inherits BaseEquipment

    Private Const ClaimedEquipmentType As String = "C"

    Protected Sub New(ByVal pClaimEquipment As ClaimEquipment, ByVal pCommonManager As CommonManager)
        MyBase.New(pClaimEquipment, pCommonManager)
    End Sub

    Protected Sub AddClaimedEquipment(ByVal pClaim As Claim, ByVal pCertificateItem As CertificateItem)
        Dim claimedEquipmenTypeId As Guid = Me.CommonManager.GetListItems(ClaimedEquipmentType).FirstOrDefault.ListItemId
        pClaim.AddEquipment(pCertificateItem, claimedEquipmenTypeId)
    End Sub
End Class