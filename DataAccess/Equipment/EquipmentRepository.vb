Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Public Class EquipmentRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, IEquipmentEntity})
    Inherits Repository(Of TType, EquipmentContext)
    Implements IEquipmentRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of EquipmentContext)())
    End Sub
End Class

