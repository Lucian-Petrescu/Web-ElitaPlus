Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class CompanyCacheManager
    Implements ICacheManager
    Private Property CompanyRepository As ICompanyRepository(Of Company)
    Friend Sub New(ByVal pCompanyRepository As ICompanyRepository(Of Company))
        Me.CompanyRepository = pCompanyRepository
    End Sub

    Private Const CacheKeyValue As String = "Companies"

    Friend Function CacheKey(ByVal pCompanyCode As String) As String
        Return String.Format("{0}#{1}", CacheKeyValue, pCompanyCode.ToUpperInvariant())
    End Function

    Friend Function CacheKey(ByVal pCompanyId As Guid) As String
        Return String.Format("{0}#{1}", CacheKeyValue, pCompanyId)
    End Function

    Friend Function BuildCache(ByVal pCompanyCode As String) As Company
        Return CompanyRepository.Get(Function(c) c.Code = pCompanyCode, Nothing, "CancellationReasons").FirstOrDefault()
    End Function

    Friend Function BuildCache(ByVal pCompanyId As Guid) As Company
        Return CompanyRepository.Get(Function(c) c.CompanyId = pCompanyId, Nothing, "CancellationReasons").FirstOrDefault()
    End Function

    Public Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 15, 0) ' 15 Mins
        Return policy
    End Function


End Class
