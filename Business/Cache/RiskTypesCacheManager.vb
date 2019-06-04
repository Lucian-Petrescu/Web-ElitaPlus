Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class RiskTypesCacheManager
    Implements ICacheManager
    Private Property RiskTypesRepository As ICompanyGroupRepository(Of RiskTypes)
    Friend Sub New(ByVal pRiskTypesRepository As ICompanyGroupRepository(Of RiskTypes))
        Me.RiskTypesRepository = pRiskTypesRepository
    End Sub

    Private Const CacheKeyValue As String = "RiskTypes"

    Friend Function CacheKey(ByVal pcompanyGroupId As Guid) As String
        Return String.Format("{0}#{1}", CacheKeyValue, pcompanyGroupId.ToByteArray)
    End Function

    Friend Function BuildCache(ByVal pcompanyGroupId As Guid) As RiskTypes
        Return RiskTypesRepository.Get(Function(r) r.CompanyGroupId = pcompanyGroupId, Nothing, "CompanyGroup").FirstOrDefault()
    End Function

    Public Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 15, 0) ' 15 Mins
        Return policy
    End Function
End Class
