Imports Assurant.ElitaPlus.DataEntities

Public Interface IDealerManager
    Function GetDealer(pDealerCode As String) As Dealer

    Function GetDealerForGwPil(pDealerCode As String) As Dealer

    Function GetDealerForGwPil(pDealerID As Guid) As Dealer

    Function GetDealerById(pDealerId As Guid) As Dealer

    Function GetProduct(pDealerCode As String, pProductCode As String) As Product

    Function GetProduct(pDealer As Dealer, pProductCode As String) As Product

    Function GetContract(pDealerCode As String, pCertificateWSD As Date) As Contract

    Function GetWarranytMaster(pDealerId As Guid, pSkuNumber As String) As IEnumerable(Of WarrantyMaster)

    Function GetListPrice(pWarrantyMasterId As Guid, pDateOfLoss As Date) As ListPrice

    Function GetBranch(pDealerId As Guid, pBranchCode As String) As Branch
End Interface
