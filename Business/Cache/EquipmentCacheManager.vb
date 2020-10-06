Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class EquipmentCacheManager
    Implements ICacheManager

    Private Property EquipmentRepository As IEquipmentRepository(Of Equipment)
    Friend Sub New(EquipmentRepository As IEquipmentRepository(Of Equipment))
        Me.EquipmentRepository = EquipmentRepository
    End Sub

    Private Const CacheKeyValue As String = "Equipments"

    Friend Function CacheKey(equipmentId As Guid) As String
        Return String.Format("{0}#{1}", CacheKeyValue, equipmentId)
    End Function

    Friend Function BuildCache(equipmentId As Guid) As Equipment
        Return EquipmentRepository.Get(Function(e) e.EquipmentId = equipmentId, Nothing, "EquipmentListDetails").FirstOrDefault()
    End Function

    Public Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 30, 0) ' 30 Mins
        Return policy
    End Function
End Class
