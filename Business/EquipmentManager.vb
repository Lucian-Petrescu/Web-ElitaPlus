Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Security
Imports Assurant.ElitaPlus.DataAccessInterface

Public Class EquipmentManager
    Implements IEquipmentManager

    Private ReadOnly m_CacheFacade As ICacheFacade
    Private Property EquipmentRepository As IEquipmentRepository(Of Equipment)
    Private Property ManufacturerRepository As ICompanyGroupRepository(Of Manufacturer)
    Private ReadOnly m_DealerManager As IDealerManager

    Public Sub New(ByVal pCacheFacade As ICacheFacade,
                   ByVal pDealerManager As IDealerManager)
        m_CacheFacade = pCacheFacade
        m_DealerManager = pDealerManager
    End Sub

    Private ReadOnly Property CacheFacade As ICacheFacade
        Get
            Return m_CacheFacade
        End Get
    End Property
    Public ReadOnly Property DealerManager As IDealerManager
        Get
            Return m_DealerManager
        End Get
    End Property

    Public Function GetEquipment(pEquipmentId As Guid) As Equipment Implements IEquipmentManager.GetEquipment
        Dim oEquipment As Equipment = EquipmentRepository.Get(Function(e) e.EquipmentId = pEquipmentId).FirstOrDefault
        'CacheFacade.GetEquipment(pEquipmentId)


        Return oEquipment
    End Function

    Public Function GetEquipmentIdByEquipmentList(pci As CertificateItem) As Nullable(Of Guid) Implements IEquipmentManager.GetEquipmentIdByEquipmentList
        Dim oEquipmentId As Nullable(Of Guid)
        Try
            oEquipmentId =
            EquipmentRepository.Get(Function(e) e.ManufacturerId = pci.ManufacturerId AndAlso e.Model = pci.Model AndAlso e.IsEffective()).SelectMany(Of EquipmentListDetail)(Function(elds) elds.EquipmentListDetails.Where(Function(eld) eld.IsEffective).Select(Of EquipmentList)(Function(els) els.EquipmentList.Code = pci.Certificate.GetDealer(DealerManager).EquipmentListCode)).First.EquipmentId

        Catch ex As Exception
        End Try

        Return oEquipmentId

    End Function
End Class
