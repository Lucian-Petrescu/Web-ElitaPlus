﻿Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class AddressCacheManager
    Implements ICacheManager
    Private Property AddressRepository As IAddressRepository(Of Address)
    Friend Sub New(ByVal pAddressRepository As IAddressRepository(Of Address))
        Me.AddressRepository = pAddressRepository
    End Sub

    Private Const CacheKeyValue As String = "Address"

    Friend Function CacheKey(ByVal pAddressId As Guid) As String
        Return String.Format("{0}#{1}", CacheKeyValue, pAddressId.ToByteArray)
    End Function

    Friend Function BuildCache(ByVal pAddressId As Guid) As Address
        Return AddressRepository.Get(Function(a) a.AddressId = pAddressId).FirstOrDefault()
    End Function

    Public Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 15, 0) ' 15 Mins
        Return policy
    End Function
End Class
