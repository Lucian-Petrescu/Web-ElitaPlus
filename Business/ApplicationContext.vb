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
        Me.Container.LoadConfiguration(DirectCast(ConfigurationManager.GetSection("Unity"), UnityConfigurationSection))
        Me.Container.RegisterInstance(Of ListCacheManager)(New ListCacheManager(Me.Container.Resolve(Of ICommonRepository(Of List))()))
        Me.Container.RegisterInstance(Of DealerCacheManager)(New DealerCacheManager(Me.Container.Resolve(Of IDealerRepository(Of Dealer))(),
                                                             Me.Container.Resolve(Of IDealerRepository(Of Product))()))
        Me.Container.RegisterInstance(Of CompanyCacheManager)(New CompanyCacheManager(Me.Container.Resolve(Of ICompanyRepository(Of Company))()))


        Me.Container.RegisterInstance(Of CountryCacheManager)(New CountryCacheManager(Me.Container.Resolve(Of ICountryRepository(Of Country))(),
                                                                                      Me.Container.Resolve(Of ICountryRepository(Of ServiceCenter))(),
                                                                                      Me.Container.Resolve(Of ICountryRepository(Of BankInfo))()))


        Me.Container.RegisterInstance(Of AddressCacheManager)(New AddressCacheManager(Me.Container.Resolve(Of IAddressRepository(Of Address))()))
        Me.Container.RegisterInstance(Of CompanyGroupCacheManager)(New CompanyGroupCacheManager(Me.Container.Resolve(Of ICompanyGroupRepository(Of CompanyGroup))()))
        Me.Container.RegisterInstance(Of EquipmentCacheManager)(New EquipmentCacheManager(Me.Container.Resolve(Of IEquipmentRepository(Of Equipment))()))
        Me.Container.RegisterInstance(Of CurrencyCacheManager)(New CurrencyCacheManager(Me.Container.Resolve(Of ICurrencyRepository(Of Currency))()))
        Me.Container.RegisterInstance(Of ExpressionCacheManager)(New ExpressionCacheManager(Me.Container.Resolve(Of ICommonRepository(Of Expression))()))

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
