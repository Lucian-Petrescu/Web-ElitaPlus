Imports Assurant.ElitaPlus.DataEntities

Public Interface IEquipmentManager
    Function GetEquipment(pEquipmentId As Guid) As Equipment

    Function GetEquipmentIdByEquipmentList(pci As CertificateItem) As Guid?
End Interface
