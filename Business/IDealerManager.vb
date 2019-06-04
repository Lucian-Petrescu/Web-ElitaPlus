Imports Assurant.ElitaPlus.DataEntities

Public Interface IDealerManager
    Function GetDealer(ByVal pDealerCode As String) As Dealer

    Function GetDealerForGwPil(ByVal pDealerCode As String) As Dealer

    Function GetDealerForGwPil(ByVal pDealerID As Guid) As Dealer

    Function GetDealerById(ByVal pDealerId As Guid) As Dealer

    Function GetProduct(ByVal pDealerCode As String, ByVal pProductCode As String) As Product

    Function GetProduct(ByVal pDealer As Dealer, ByVal pProductCode As String) As Product

    Function GetContract(ByVal pDealerCode As String, ByVal pCertificateWSD As Date) As Contract

    Function GetWarranytMaster(ByVal pDealerId As Guid, ByVal pSkuNumber As String) As IEnumerable(Of WarrantyMaster)

    Function GetListPrice(ByVal pWarrantyMasterId As Guid, ByVal pDateOfLoss As Date) As ListPrice

    Function GetBranch(ByVal pDealerId As Guid, ByVal pBranchCode As String) As Branch
End Interface
