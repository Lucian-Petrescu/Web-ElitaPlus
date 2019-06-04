Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class CountryCacheManager
    Implements ICacheManager
    Private Property CountryRepository As ICountryRepository(Of Country)
    Private Property ServiceCenterRepository As ICountryRepository(Of ServiceCenter)
    Private Property BankRepository As ICountryRepository(Of BankInfo)
    Friend Sub New(ByVal pCountryRepository As ICountryRepository(Of Country),
                   ByVal pServiceCenterRepository As ICountryRepository(Of ServiceCenter),
                   ByVal pBankRepository As ICountryRepository(Of BankInfo))
        Me.CountryRepository = pCountryRepository
        Me.ServiceCenterRepository = pServiceCenterRepository
        Me.BankRepository = pBankRepository
    End Sub

    Private Const CacheKeyValue As String = "Country"

    Friend Function CacheKey(ByVal pCountryId As Guid) As String
        Return String.Format("{0}#{1}", CacheKeyValue, pCountryId)
    End Function

    Friend Function BuildCache(ByVal pCountryId As Guid) As Country
        Return CountryRepository.Get(Function(c) c.CountryId = pCountryId, Nothing, "Regions").FirstOrDefault()
    End Function
    ''' <summary>
    ''' Country object from country code
    ''' </summary>
    ''' <param name="pCountryCode"></param>
    ''' <returns></returns>
    Friend Function CacheKey(ByVal pCountryCode As String) As String
        Return String.Format("{0}#{1}", CacheKeyValue, pCountryCode)
    End Function

    Friend Function BuildCache(ByVal pCountryCode As String) As Country
        Return CountryRepository.Get(Function(c) c.Code = pCountryCode, Nothing, "Regions").FirstOrDefault()
    End Function
    ''' <summary>
    ''' To get the Region object
    ''' </summary>
    ''' <param name="pCountryId"></param>
    ''' <param name="pRegionId"></param>
    ''' <returns></returns>
    Friend Function CacheKey(ByVal pCountryId As Guid, pRegionId As Guid) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, pCountryId, pRegionId)
    End Function

    Friend Function BuildCache(ByVal pCountryId As Guid, ByVal pRegionId As Guid) As Region
        Return CountryRepository.Get(Function(c) c.CountryId = pCountryId, Nothing, "Regions").FirstOrDefault().Regions.Where(Function(r) r.RegionId = pRegionId).FirstOrDefault()


    End Function
    ''' <summary>
    ''' To get the Service Center object
    ''' </summary>
    ''' <param name="pCountryId"></param>
    ''' <param name="pSvcCenterId"></param>
    ''' <returns></returns>
    Friend Function CacheKeySvcCenterById(ByVal pCountryId As Guid, pSvcCenterId As Guid) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, pCountryId, pSvcCenterId)
    End Function

    Friend Function BuildCacheSvcCenterById(ByVal pCountryId As Guid, ByVal pSvcCenterId As Guid) As ServiceCenter
        Return ServiceCenterRepository.Get(Function(s) s.ServiceCenterId = pSvcCenterId).FirstOrDefault()
    End Function

    Friend Function CacheKeySvcCenterByCode(ByVal pCountryId As Guid, pSvcCenterCode As String) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, pCountryId, pSvcCenterCode)
    End Function

    Friend Function BuildCacheSvcCenterByCode(ByVal pCountryId As Guid, ByVal pSvcCenterCode As String) As ServiceCenter
        Return ServiceCenterRepository.Get(Function(s) s.COUNTRY_ID = pCountryId And s.CODE = pSvcCenterCode).FirstOrDefault()
    End Function

    ''' <summary>
    ''' To get the Bank Info object
    ''' </summary>
    ''' <param name="pCountryId"></param>
    ''' <param name="pBankInfoId"></param>
    ''' <returns></returns>
    Friend Function CacheKeyBankInfoById(ByVal pCountryId As Guid, pBankInfoId As Guid) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, pCountryId, pBankInfoId)
    End Function

    Friend Function BuildCacheBankInfoById(ByVal pCountryId As Guid, ByVal pBankInfoId As Guid) As BankInfo
        Return BankRepository.Get(Function(b) b.BankInfoId = pBankInfoId).FirstOrDefault()
    End Function

    Public Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 15, 0) ' 15 Mins
        Return policy
    End Function
End Class
