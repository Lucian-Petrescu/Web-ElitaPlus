Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.Security
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.DataAccessInterface


Public Class DealerManager
    Implements IDealerManager

    Private ReadOnly m_CacheFacade As ICacheFacade
    Private Property m_WarrantyMasterRepository As IDealerRepository(Of WarrantyMaster)
    Private Property m_ListPriceRepository As IDealerRepository(Of ListPrice)
    Private Property m_CommonManager As ICommonManager

    Public Sub New(ByVal pCacheFacade As ICacheFacade,
                   ByVal WarrantyMasterRepository As IDealerRepository(Of WarrantyMaster),
                   ByVal ListPriceRepository As IDealerRepository(Of ListPrice),
                   ByVal pCommonManager As ICommonManager)
        m_CacheFacade = pCacheFacade
        m_WarrantyMasterRepository = WarrantyMasterRepository
        m_ListPriceRepository = ListPriceRepository
        m_CommonManager = pCommonManager

    End Sub

    Private ReadOnly Property CacheFacade As ICacheFacade
        Get
            Return m_CacheFacade
        End Get
    End Property

    Public Function GetDealer(ByVal pDealerCode As String) As Dealer Implements IDealerManager.GetDealer
        Dim oDealer As Dealer = CacheFacade.GetDealer(pDealerCode.ToUpperInvariant())


        If (oDealer Is Nothing) OrElse (Not Threading.Thread.CurrentPrincipal.HasCompany(oDealer.CompanyId)) Then
            Throw New DealerNotFoundException(Nothing, pDealerCode)
        End If

        Return oDealer

    End Function
    Function GetDealerForGWPIL(ByVal pDealerCode As String) As Dealer Implements IDealerManager.GetDealerForGwPil
        Dim oDealer As Dealer = CacheFacade.GetDealer(pDealerCode.ToUpperInvariant())

        If (oDealer Is Nothing) Then
            Throw New DealerNotFoundException(Nothing, pDealerCode)
        End If

        Return oDealer

    End Function

    Public Function GetDealer(ByVal pDealerId As Guid) As Dealer Implements IDealerManager.GetDealerById
        Dim oDealer As Dealer = CacheFacade.GetDealerById(pDealerId)

        If (oDealer Is Nothing) OrElse (Not Threading.Thread.CurrentPrincipal.HasCompany(oDealer.CompanyId)) Then
            Throw New DealerNotFoundException(pDealerId, String.Empty)
        End If

        Return oDealer

    End Function

    Public Function GetProduct(pDealer As Dealer, pProductCode As String) As Product Implements IDealerManager.GetProduct
        Return Me.GetProduct(pDealer.DealerCode, pProductCode)
    End Function

    Public Function GetProduct(pDealerCode As String, pProductCode As String) As Product Implements IDealerManager.GetProduct
        Dim oProduct As Product = CacheFacade.GetProduct(pDealerCode.ToUpperInvariant(), pProductCode.ToUpperInvariant())

        Return oProduct
    End Function

    Public Function GetContract(pDealerCode As String, pCertificateWSD As Date) As Contract Implements IDealerManager.GetContract
        Dim oContract As Contract = CacheFacade.GetContract(pDealerCode.ToUpperInvariant(), pCertificateWSD)

        Return oContract
    End Function

    Public Function GetWarrantyMaster(ByVal pDealerId As Guid, ByVal pSkuNumber As String) As IEnumerable(Of WarrantyMaster) Implements IDealerManager.GetWarranytMaster
        Return m_WarrantyMasterRepository.Get(Function(wm) wm.DealerId = pDealerId AndAlso wm.SkuNumber = pSkuNumber, Nothing, "ListPrices")

    End Function

    Function GetListPrice(ByVal pWarrantyMasterId As Guid, ByVal pDateOfLoss As Date) As ListPrice Implements IDealerManager.GetListPrice
        Return m_ListPriceRepository.Get(Function(lp) lp.WarrantyMasterId = pWarrantyMasterId AndAlso
        lp.AmountTypeId.ToCode(m_CommonManager, ListCodes.ListPriceAmountTypeCode) = ListPriceAmountTypeCodes.ListPrice AndAlso (Not (lp.Effective > pDateOfLoss Or lp.Expiration < pDateOfLoss)))
    End Function

    Public Function GetDealerForGwPil(pDealerID As Guid) As Dealer Implements IDealerManager.GetDealerForGwPil
        Dim oDealer As Dealer = CacheFacade.GetDealerById(pDealerID)

        If (oDealer Is Nothing) Then
            Throw New DealerNotFoundException(pDealerID, String.Empty)
        End If

        Return oDealer
    End Function

    Public Function GetBranch(ByVal pDealerId As Guid, ByVal pBranchCode As String) As Branch Implements IDealerManager.GetBranch
        Dim objBranch As Branch = CacheFacade.GetBranch(pDealerId, pBranchCode)
        Return objBranch
    End Function
End Class