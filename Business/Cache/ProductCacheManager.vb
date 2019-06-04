Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class ProductCacheManager
    Implements ICacheManager
    Private Property ProductRepository As IDealerRepository(Of Product)
    Friend Sub New(ByVal pProductRepository As IDealerRepository(Of Product))
        Me.ProductRepository = pProductRepository
    End Sub

    Private Const CacheKeyValue As String = "Products"

    Friend Function CacheKey(ByVal pDealerCode As String, ByVal pProductCode As String) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, pDealerCode.ToUpperInvariant(), pProductCode.ToUpperInvariant())
    End Function

    Friend Function BuildCache(ByVal pDealerCode As String, ByVal pProductCode As String) As Product
        Return ProductRepository.Get(Function(p) p.Dealer.DealerCode = pDealerCode AndAlso p.ProductCode = pProductCode, Nothing, "Dealer").FirstOrDefault()
    End Function

    Public Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 15, 0) ' 15 Mins
        Return policy
    End Function
End Class
