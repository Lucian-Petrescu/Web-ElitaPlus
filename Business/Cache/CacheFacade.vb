Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.Security
Imports System.Threading
Imports Assurant.ElitaPlus.DataEntities
Imports Microsoft.Practices.Unity

Public Class CacheFacade
    Implements ICacheFacade

    Private Const CacheName As String = "Assurant.Elita.Business"
    Private Const ListCacheName As String = "List"

    Private m_cache As MemoryCache
    Private m_container As UnityContainer

    Private Property Cache As MemoryCache
        Get
            Return m_cache
        End Get
        Set(value As MemoryCache)
            m_cache = value
        End Set
    End Property

    Public Sub New()
        m_cache = New MemoryCache(CacheName)
    End Sub

    Friend Function GetList(pListCode As String) As ElitaListItem() Implements ICacheFacade.GetList
        Dim languageCode As String = Thread.CurrentPrincipal.GetLanguageCode()
        languageCode = "CH"

        Return GetList(pListCode, languageCode)

    End Function

    Friend Function GetList(pListCode As String, pLanguageCode As String) As ElitaListItem() Implements ICacheFacade.GetList
        Return GetCacheItem(
            ApplicationContext.Current.Container.Resolve(Of ListCacheManager)(),
            Function(cm) cm.CacheKey(pListCode, pLanguageCode),
            Function(cm) cm.BuildCache(pListCode, pLanguageCode))
    End Function

    Public Function GetDealer(pDealerCode As String) As Dealer Implements ICacheFacade.GetDealer
        Try
            Return GetCacheItem(
                              ApplicationContext.Current.Container.Resolve(Of DealerCacheManager)(),
                                  Function(cm) cm.CacheKey(pDealerCode),
                                  Function(cm) cm.BuildCache(pDealerCode))
        Catch ex As Exception
            Throw New DealerNotFoundException(Guid.Empty, pDealerCode)
        End Try


    End Function

    Public Function GetDealer(pDealerId As Guid) As Dealer Implements ICacheFacade.GetDealerById
        Return GetCacheItem(
                   ApplicationContext.Current.Container.Resolve(Of DealerCacheManager)(),
                       Function(cm) cm.CacheKey(pDealerId),
                       Function(cm) cm.BuildCache(pDealerId))

    End Function

    Private Function GetCacheItem(Of TReturnType, TCacheManager As ICacheManager)(
                                                                                  pCacheManager As TCacheManager,
                                                                                  pCacheKeyFunction As Func(Of TCacheManager, String),
                                                                                  pBuildFunction As Func(Of TCacheManager, TReturnType)) As TReturnType
        Dim cacheItemObject As CacheItem
        Dim cacheKey As String = pCacheKeyFunction(pCacheManager)
        cacheItemObject = Cache.GetCacheItem(cacheKey)
        If (cacheItemObject Is Nothing) Then
            Dim cacheObject As TReturnType = pBuildFunction(pCacheManager)
            If cacheObject Is Nothing Then ' build function didn't find the object to cache
                Return Nothing
            Else
                cacheItemObject = New CacheItem(cacheKey, cacheObject)
                Cache.Add(cacheItemObject,
                          pCacheManager.GetPolicy())
            End If
        End If
        Return DirectCast(cacheItemObject.Value, TReturnType)
    End Function

    Public Function GetProduct(pDealerCode As String, pProductCode As String) As Product Implements ICacheFacade.GetProduct
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of DealerCacheManager)(),
            Function(cm) cm.CacheKey(pDealerCode, pProductCode),
            Function(cm) cm.BuildCache(pDealerCode, pProductCode))
    End Function

    Public Function GetContract(pDealerCode As String, pCertificateWSD As Date) As Contract Implements ICacheFacade.GetContract
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of DealerCacheManager)(),
            Function(cm) cm.CacheKey(pDealerCode, pCertificateWSD),
            Function(cm) cm.BuildCache(pDealerCode, pCertificateWSD))
    End Function

    Public Function GetCompany(pCompanyId As Guid) As Company Implements ICacheFacade.GetCompany
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of CompanyCacheManager)(),
            Function(cm) cm.CacheKey(pCompanyId),
            Function(cm) cm.BuildCache(pCompanyId))
    End Function

    Public Function GetCompany(pCompanyCode As String) As Company Implements ICacheFacade.GetCompany
        Try
            Return GetCacheItem(
                    ApplicationContext.Current.Container.Resolve(Of CompanyCacheManager)(),
                        Function(cm) cm.CacheKey(pCompanyCode),
                        Function(cm) cm.BuildCache(pCompanyCode))
        Catch ex As Exception
            Throw New CompanyNotFoundException(Guid.Empty, pCompanyCode)
        End Try

    End Function


    Public Function GetCountry(pcountryId As Guid) As Country Implements ICacheFacade.GetCountry
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of CountryCacheManager)(),
            Function(cm) cm.CacheKey(pcountryId),
            Function(cm) cm.BuildCache(pcountryId))
    End Function

    Public Function GetCountryByCode(pcountryCode As String) As Country Implements ICacheFacade.GetCountryByCode
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of CountryCacheManager)(),
            Function(cm) cm.CacheKey(pcountryCode),
            Function(cm) cm.BuildCache(pcountryCode))
    End Function

    Public Function GetRegion(pCountryId As Guid, pRegionId As Guid) As Region Implements ICacheFacade.GetRegion
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of CountryCacheManager)(),
            Function(cm) cm.CacheKey(pCountryId, pRegionId),
            Function(cm) cm.BuildCache(pCountryId, pRegionId))
    End Function

    Function GetServiceCenterById(pCountryId As Guid, pSvcCenterId As Guid) As ServiceCenter Implements ICacheFacade.GetServiceCenterById
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of CountryCacheManager)(),
            Function(cm) cm.CacheKeySvcCenterById(pCountryId, pSvcCenterId),
            Function(cm) cm.BuildCacheSvcCenterById(pCountryId, pSvcCenterId))
    End Function

    Function GetServiceCenterByCode(pCountryId As Guid, pSvcCenterCode As String) As ServiceCenter Implements ICacheFacade.GetServiceCenterByCode
        Try
            Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of CountryCacheManager)(),
            Function(cm) cm.CacheKeySvcCenterByCode(pCountryId, pSvcCenterCode),
            Function(cm) cm.BuildCacheSvcCenterByCode(pCountryId, pSvcCenterCode))
        Catch ex As Exception
            Throw New ServiceCenterNotFoundException(Guid.Empty, pSvcCenterCode)
        End Try
    End Function
    Public Function GetAddress(pAddressId As Guid) As Address Implements ICacheFacade.GetAddress
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of AddressCacheManager)(),
            Function(cm) cm.CacheKey(pAddressId),
            Function(cm) cm.BuildCache(pAddressId))
    End Function

    Public Function GetCurrency(pCurrencyId As Guid) As Currency Implements ICacheFacade.GetCurrency
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of CurrencyCacheManager)(),
            Function(cm) cm.CacheKey(pCurrencyId),
            Function(cm) cm.BuildCache(pCurrencyId))
    End Function

    Public Function GetCompanyGroup(pCompanyGroupId As Guid) As CompanyGroup Implements ICacheFacade.GetCompanyGroup
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of CompanyGroupCacheManager)(),
            Function(cm) cm.CacheKey(pCompanyGroupId),
            Function(cm) cm.BuildCache(pCompanyGroupId))
    End Function

    'Public Function GetRiskType(pCompanyGroupId As Guid, pRiskTypeId As Guid) As RiskType Implements ICacheFacade.GetRiskType
    '    Return GetCacheItem(Of RiskType, CompanyGroupCacheManager)(
    '    ApplicationContext.Current.Container.Resolve(Of CompanyGroupCacheManager)(),
    '        Function(cm) cm.CacheKey(pCompanyGroupId, pRiskTypeId),
    '        Function(cm) cm.BuildCache(pCompanyGroupId, pRiskTypeId))
    'End Function

    Public Function GetEquipment(pEquipmentId As Guid) As Equipment Implements ICacheFacade.GetEquipment
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of EquipmentCacheManager)(),
            Function(cm) cm.CacheKey(pEquipmentId),
            Function(cm) cm.BuildCache(pEquipmentId))
    End Function

    Public Function GetLabelTranslation(pUiCode As String) As IEnumerable(Of LabelTranslation) Implements ICacheFacade.GetLabelTranslation
        Dim languageCode As String = Thread.CurrentPrincipal.GetLanguageCode()

        Return GetLabelTranslation(pUiCode, languageCode)
    End Function

    Public Function GetLabelTranslation(pUiCode As String, pLanguageCode As String) As IEnumerable(Of LabelTranslation) Implements ICacheFacade.GetLabelTranslation
        Return GetCacheItem(
           ApplicationContext.Current.Container.Resolve(Of ListCacheManager)(),
           Function(cm) cm.CacheKeyForLabelTranslations(pUiCode, pLanguageCode),
           Function(cm) cm.BuildCacheForLabelTranslations(pUiCode, pLanguageCode))
    End Function

    Public Function GetExpression(pExpressionId As Guid) As Expression Implements ICacheFacade.GetExpression
        Return GetCacheItem(
            ApplicationContext.Current.Container.Resolve(Of ExpressionCacheManager)(),
            Function(cm) cm.CacheKeyForExpression(pExpressionId),
            Function(cm) cm.BuildCacheForExpression(pExpressionId))
    End Function

    Function GetBankInfoById(pCountryId As Guid, pBankInfoID As Guid) As BankInfo Implements ICacheFacade.GetBankInfoById
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of CountryCacheManager)(),
            Function(cm) cm.CacheKeyBankInfoById(pCountryId, pBankInfoID),
            Function(cm) cm.BuildCacheBankInfoById(pCountryId, pBankInfoID))
    End Function

    Public Function GetBranch(pDealerId As Guid, pBranchCode As String) As Branch Implements ICacheFacade.GetBranch
        Return GetCacheItem(
        ApplicationContext.Current.Container.Resolve(Of DealerCacheManager)(),
            Function(cm) cm.CacheKey(pDealerId, pBranchCode, "Branch"),
            Function(cm) cm.BuildBranchCache(pDealerId, pBranchCode))
    End Function
End Class
