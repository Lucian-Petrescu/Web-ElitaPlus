Imports Assurant.ElitaPlus.DataEntities

Friend MustInherit Class EnrolledEquipment
    Inherits BaseEquipment

    Private Const EnrolledEquipmentType As String = "E"

    Protected Sub New(pClaimEquipment As ClaimEquipment, pCommonManager As CommonManager)
        MyBase.New(pClaimEquipment, pCommonManager)
    End Sub

    Protected Sub AddClaimedEquipment(pClaim As Claim, pCertificateItem As CertificateItem)
        Dim enrolledEquipmenTypeId As Guid = CommonManager.GetListItems(EnrolledEquipmentType).FirstOrDefault.ListItemId
        pClaim.AddEquipment(pCertificateItem, enrolledEquipmenTypeId)
    End Sub
End Class
