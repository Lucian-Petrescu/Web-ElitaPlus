Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class CountryCacheManager
    Implements ICacheManager
    Private Property CountryRepository As ICountryRepository(Of Country)
    Private Property ServiceCenterRepository As ICountryRepository(Of ServiceCenter)
    Private Property BankRepository As ICountryRepository(Of BankInfo)
    Friend Sub New(pCountryRepository As ICountryRepository(Of Country),
                   pServiceCenterRepository As ICountryRepository(Of ServiceCenter),
                   pBankRepository As ICountryRepository(Of BankInfo))
        CountryRepository = pCountryRepository
        ServiceCenterRepository = pServiceCenterRepository
        BankRepository = pBankRepository
    End Sub

    Private Const CacheKeyValue As String = "Country"

    Friend Function CacheKey(pCountryId As Guid) As String
        Return String.Format("{0}#{1}", CacheKeyValue, pCountryId)
    End Function

    Friend Function BuildCache(pCountryId As Guid) As Country
        Return CountryRepository.Get(Function(c) c.CountryId = pCountryId, Nothing, "Regions").FirstOrDefault()
    End Function
    ''' <summary>
    ''' Country object from country code
    ''' </summary>
    ''' <param name="pCountryCode"></param>
    ''' <returns></returns>
    Friend Function CacheKey(pCountryCode As String) As String
        Return String.Format("{0}#{1}", CacheKeyValue, pCountryCode)
    End Function

    Friend Function BuildCache(pCountryCode As String) As Country
        Return CountryRepository.Get(Function(c) c.Code = pCountryCode, Nothing, "Regions").FirstOrDefault()
    End Function
    ''' <summary>
    ''' To get the Region object
    ''' </summary>
    ''' <param name="pCountryId"></param>
    ''' <param name="pRegionId"></param>
    ''' <returns></returns>
    Friend Function CacheKey(pCountryId As Guid, pRegionId As Guid) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, pCountryId, pRegionId)
    End Function

    Friend Function BuildCache(pCountryId As Guid, pRegionId As Guid) As Region
        Return CountryRepository.Get(Function(c) c.CountryId = pCountryId, Nothing, "Regions").FirstOrDefault().Regions.FirstOrDefault(Function (r) r.RegionId = pRegionId)


    End Function
    ''' <summary>
    ''' To get the Service Center object
    ''' </summary>
    ''' <param name="pCountryId"></param>
    ''' <param name="pSvcCenterId"></param>
    ''' <returns></returns>
    Friend Function CacheKeySvcCenterById(pCountryId As Guid, pSvcCenterId As Guid) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, pCountryId, pSvcCenterId)
    End Function

    Friend Function BuildCacheSvcCenterById(pCountryId As Guid, pSvcCenterId As Guid) As ServiceCenter
        Return ServiceCenterRepository.Get(Function(s) s.ServiceCenterId = pSvcCenterId).FirstOrDefault()
    End Function

    Friend Function CacheKeySvcCenterByCode(pCountryId As Guid, pSvcCenterCode As String) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, pCountryId, pSvcCenterCode)
    End Function

    Friend Function BuildCacheSvcCenterByCode(pCountryId As Guid, pSvcCenterCode As String) As ServiceCenter
        Return ServiceCenterRepository.Get(Function(s) s.COUNTRY_ID = pCountryId AndAlso s.CODE = pSvcCenterCode).FirstOrDefault()
    End Function

    ''' <summary>
    ''' To get the Bank Info object
    ''' </summary>
    ''' <param name="pCountryId"></param>
    ''' <param name="pBankInfoId"></param>
    ''' <returns></returns>
    Friend Function CacheKeyBankInfoById(pCountryId As Guid, pBankInfoId As Guid) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, pCountryId, pBankInfoId)
    End Function

    Friend Function BuildCacheBankInfoById(pCountryId As Guid, pBankInfoId As Guid) As BankInfo
        Return BankRepository.Get(Function(b) b.BankInfoId = pBankInfoId).FirstOrDefault()
    End Function

    Public Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 15, 0) ' 15 Mins
        Return policy
    End Function
End Class
