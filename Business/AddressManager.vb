Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Security

Public Class AddressManager
    Implements IAddressManager

    Private ReadOnly m_CacheFacade As ICacheFacade
    Private ReadOnly m_AdderssRepository As IAddressRepository(Of Address)

    Public Sub New(ByVal pCacheFacade As ICacheFacade, ByVal pAddressRepository As IAddressRepository(Of Address))
        m_CacheFacade = pCacheFacade
        m_AdderssRepository = pAddressRepository
    End Sub

    Private ReadOnly Property CacheFacade As ICacheFacade
        Get
            Return m_CacheFacade
        End Get
    End Property

    Public Function GetAddress(pAddressId As Guid) As Address Implements IAddressManager.GetAddress
        Dim oAddress As Address = m_AdderssRepository.Get(Function(a) a.AddressId = pAddressId).First
        'CacheFacade.GetAddress(pAddressId)

        Return oAddress
    End Function

End Class
