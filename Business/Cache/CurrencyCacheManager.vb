Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class CurrencyCacheManager
    Implements ICacheManager
    Private Property CurrencyRepository As ICurrencyRepository(Of Currency)
    Friend Sub New(pCurrencyRepository As ICurrencyRepository(Of Currency))
        CurrencyRepository = pCurrencyRepository
    End Sub

    Private Const CacheKeyValue As String = "Currency"

    Friend Function CacheKey(pCurrencyId As Guid) As String
        Return String.Format("{0}#{1}", CacheKeyValue, pCurrencyId)
    End Function

    Friend Function BuildCache(pCurrencyId As Guid) As Currency
        Return CurrencyRepository.Get(Function(a) a.CurrencyId = pCurrencyId).FirstOrDefault()
    End Function

    Public Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 15, 0) ' 15 Mins
        Return policy
    End Function
End Class
