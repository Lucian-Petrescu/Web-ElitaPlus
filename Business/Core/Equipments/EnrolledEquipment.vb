Imports Assurant.ElitaPlus.DataEntities

Friend MustInherit Class EnrolledEquipment
    Inherits BaseEquipment

    Private Const EnrolledEquipmentType As String = "E"

    Protected Sub New(ByVal pClaimEquipment As ClaimEquipment, ByVal pCommonManager As CommonManager)
        MyBase.New(pClaimEquipment, pCommonManager)
    End Sub

    Protected Sub AddClaimedEquipment(ByVal pClaim As Claim, ByVal pCertificateItem As CertificateItem)
        Dim enrolledEquipmenTypeId As Guid = Me.CommonManager.GetListItems(EnrolledEquipmentType).FirstOrDefault.ListItemId
        pClaim.AddEquipment(pCertificateItem, enrolledEquipmenTypeId)
    End Sub
End Class
