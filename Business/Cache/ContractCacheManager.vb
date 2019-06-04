Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class ContractCacheManager
    Implements ICacheManager
    Private Property ContractRepository As IDealerRepository(Of Contract)
    Friend Sub New(ByVal pContractRepository As IDealerRepository(Of Contract))
        Me.ContractRepository = pContractRepository
    End Sub

    Private Const CacheKeyValue As String = "Contracts"

    Friend Function CacheKey(ByVal pDealerCode As String, pCertificateWSD As Date) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, pDealerCode.ToUpperInvariant(), pCertificateWSD)
    End Function

    Friend Function BuildCache(ByVal pDealerCode As String, pCertificateWSD As Date) As Contract
        Return ContractRepository.Get(Function(c) c.Dealer.DealerCode = pDealerCode AndAlso (pCertificateWSD > c.EFFECTIVE And pCertificateWSD < c.EXPIRATION), Nothing, "Dealer").FirstOrDefault()
    End Function

    Public Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 15, 0) ' 15 Mins
        Return policy
    End Function
End Class
