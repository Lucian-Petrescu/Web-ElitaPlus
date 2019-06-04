Imports System.ServiceModel
Imports System.ServiceModel.Activation
Imports Assurant.ElitaPlus.Business
Imports Microsoft.Practices.Unity
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataAccess

Namespace Core
    Public Class UnityServiceHostFactory
        Inherits ServiceHostFactory

        Private m_container As UnityContainer

        Public Sub New()
            MyBase.New()

            m_container = ApplicationContext.Current.Container

            'US 203684
            m_container.RegisterType(Of ICertificateManager, SpecializedCertificateManager)(ElitaWebServiceConstants.SPECIALIZED_SERVICE_CERTIFICATE_MANAGER, New InjectionConstructor(m_container.Resolve(Of IDealerManager)))
            m_container.RegisterType(Of ICertificateRepository(Of Certificate), TimbCertificateRepository(Of Certificate))(ElitaWebServiceConstants.SPECIALIZED_SERVICE_TIMB_CERTIFICATE_REPOSITORY)

            'US 203685
            m_container.RegisterType(Of IClaimManager, SpecializedClaimManager)(ElitaWebServiceConstants.SPECIALIZED_SERVICE_CLAIM_MANAGER)
            m_container.RegisterType(Of IClaimRepository(Of Claim), TimbClaimRepository(Of Claim))(ElitaWebServiceConstants.SPECIALIZED_SERVICE_TIMB_CLAIM_REPOSITORY)

        End Sub

        Protected Overrides Function CreateServiceHost(serviceType As Type, baseAddresses As Uri()) As ServiceHost
            Return New UnityServiceHost(m_container, serviceType, baseAddresses)
        End Function
    End Class
End Namespace
