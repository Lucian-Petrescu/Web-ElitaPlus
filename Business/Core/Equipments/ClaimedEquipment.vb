Imports Assurant.ElitaPlus.DataEntities

Friend MustInherit Class ClaimedEquipment
    Inherits BaseEquipment

    Private Const ClaimedEquipmentType As String = "C"

    Protected Sub New(pClaimEquipment As ClaimEquipment, pCommonManager As CommonManager)
        MyBase.New(pClaimEquipment, pCommonManager)
    End Sub

    Protected Sub AddClaimedEquipment(pClaim As Claim, pCertificateItem As CertificateItem)
        Dim claimedEquipmenTypeId As Guid = CommonManager.GetListItems(ClaimedEquipmentType).FirstOrDefault.ListItemId
        pClaim.AddEquipment(pCertificateItem, claimedEquipmenTypeId)
    End Sub
End Class