Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class CompanyGroupCacheManager
    Implements ICacheManager
    Private Property CompanyGroupRepository As ICompanyGroupRepository(Of CompanyGroup)
    Friend Sub New(ByVal pCompanyGroupRepository As ICompanyGroupRepository(Of CompanyGroup))
        Me.CompanyGroupRepository = pCompanyGroupRepository
    End Sub

    Private Const CacheKeyValue As String = "CompanyGroup"
    Friend Function CacheKey(ByVal pCompanyGroupId As Guid) As String
        Return String.Format("{0}#{1}", CacheKeyValue, pCompanyGroupId)
    End Function

    Friend Function BuildCache(ByVal pCompanygroupId As Guid) As CompanyGroup
        Return CompanyGroupRepository.Get(Function(cg) cg.CompanyGroupId = pCompanygroupId, Nothing, "RiskTypes, Manufacturers,PaymentTypes,ClaimStatusByGroups,DefaultClaimStatuses,CoverageLosses").FirstOrDefault()
    End Function

    Public Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 15, 0) ' 15 Mins
        Return policy
    End Function


End Class
