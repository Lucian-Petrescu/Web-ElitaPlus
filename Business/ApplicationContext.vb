Imports System.Configuration
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.Configuration

Public NotInheritable Class ApplicationContext

    Private Shared m_syncRoot As Object
    Private Shared m_instance As ApplicationContext

    Shared Sub New()
        m_syncRoot = New Object()
    End Sub

    Private Sub New()
        m_container = New UnityContainer()
        InitializeContainer()
    End Sub

    Public ReadOnly Property Container As UnityContainer
        Get
            Return m_container
        End Get
    End Property

    Private Sub InitializeContainer()
        Container.LoadConfiguration(DirectCast(ConfigurationManager.GetSection("Unity"), UnityConfigurationSection))
        Container.RegisterInstance(New ListCacheManager(Container.Resolve(Of ICommonRepository(Of List))()))
        Container.RegisterInstance(New DealerCacheManager(Container.Resolve(Of IDealerRepository(Of Dealer))(),
                                                             Container.Resolve(Of IDealerRepository(Of Product))()))
        Container.RegisterInstance(New CompanyCacheManager(Container.Resolve(Of ICompanyRepository(Of Company))()))


        Container.RegisterInstance(New CountryCacheManager(Container.Resolve(Of ICountryRepository(Of Country))(),
                                                                                      Container.Resolve(Of ICountryRepository(Of ServiceCenter))(),
                                                                                      Container.Resolve(Of ICountryRepository(Of BankInfo))()))


        Container.RegisterInstance(New AddressCacheManager(Container.Resolve(Of IAddressRepository(Of Address))()))
        Container.RegisterInstance(New CompanyGroupCacheManager(Container.Resolve(Of ICompanyGroupRepository(Of CompanyGroup))()))
        Container.RegisterInstance(New EquipmentCacheManager(Container.Resolve(Of IEquipmentRepository(Of Equipment))()))
        Container.RegisterInstance(New CurrencyCacheManager(Container.Resolve(Of ICurrencyRepository(Of Currency))()))
        Container.RegisterInstance(New ExpressionCacheManager(Container.Resolve(Of ICommonRepository(Of Expression))()))

    End Sub

    Public Shared ReadOnly Property Current As ApplicationContext
        Get
            If (m_instance Is Nothing) Then
                SyncLock (m_syncRoot)
                    If (m_instance Is Nothing) Then
                        m_instance = New ApplicationContext()
                    End If
                End SyncLock
            End If
            Return m_instance
        End Get
    End Property

    Private m_container As UnityContainer

End Class
