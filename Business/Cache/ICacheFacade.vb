Imports Assurant.ElitaPlus.DataEntities

Public Interface ICacheFacade

    Function GetList(ByVal pListCode As String) As ElitaListItem()

    Function GetList(ByVal pListCode As String, ByVal pLanguageCode As String) As ElitaListItem()

    Function GetLabelTranslation(ByVal pUiCode As String) As IEnumerable(Of LabelTranslation)

    Function GetLabelTranslation(ByVal pUiCode As String, ByVal pLanguageCode As String) As IEnumerable(Of LabelTranslation)

    Function GetDealer(ByVal pDealerCode As String) As Dealer

    Function GetDealerById(ByVal pDealerId As Guid) As Dealer

    Function GetProduct(ByVal pDealerCode As String, ByVal pProductCode As String) As Product

    Function GetCompany(ByVal pCompanyCode As String) As Company

    Function GetCompany(ByVal pCompanyId As Guid) As Company

    Function GetContract(ByVal pDealerCode As String, pCertificateWSD As Date) As Contract

    Function GetCountry(ByVal pCountryId As Guid) As Country

    Function GetCountryByCode(ByVal pCountryCode As String) As Country

    Function GetAddress(ByVal pAddressId As Guid) As Address

    Function GetCurrency(ByVal pCurrencyId As Guid) As Currency

    Function GetCompanyGroup(pCompanyGroupId As Guid) As CompanyGroup

    'Function GetRiskType(pCompanyGroupId As Guid, pRiskTypeId As Guid) As RiskType

    Function GetEquipment(pEquipmentId As Guid) As Equipment

    Function GetRegion(ByVal pAddressId As Guid, pRegionId As Guid) As Region

    Function GetServiceCenterById(ByVal pCountryId As Guid, pSvcCenterId As Guid) As ServiceCenter

    Function GetServiceCenterByCode(ByVal pCountryId As Guid, pSvcCenterCode As String) As ServiceCenter

    Function GetExpression(ByVal pExpressionId As Guid) As Expression

    'Function GetSuspendedReason(ByVal pDealerCode As String, pSuspendedReasonId As Guid) As SuspendedReason

    Function GetBankInfoById(ByVal pCountryId As Guid, pBankInfoId As Guid) As BankInfo

    Function GetBranch(ByVal pDealerId As Guid, ByVal pBranchCode As String) As Branch
End Interface
