Imports Assurant.ElitaPlus.DataEntities

Public Interface ICacheFacade

    Function GetList(pListCode As String) As ElitaListItem()

    Function GetList(pListCode As String, pLanguageCode As String) As ElitaListItem()

    Function GetLabelTranslation(pUiCode As String) As IEnumerable(Of LabelTranslation)

    Function GetLabelTranslation(pUiCode As String, pLanguageCode As String) As IEnumerable(Of LabelTranslation)

    Function GetDealer(pDealerCode As String) As Dealer

    Function GetDealerById(pDealerId As Guid) As Dealer

    Function GetProduct(pDealerCode As String, pProductCode As String) As Product

    Function GetCompany(pCompanyCode As String) As Company

    Function GetCompany(pCompanyId As Guid) As Company

    Function GetContract(pDealerCode As String, pCertificateWSD As Date) As Contract

    Function GetCountry(pCountryId As Guid) As Country

    Function GetCountryByCode(pCountryCode As String) As Country

    Function GetAddress(pAddressId As Guid) As Address

    Function GetCurrency(pCurrencyId As Guid) As Currency

    Function GetCompanyGroup(pCompanyGroupId As Guid) As CompanyGroup

    'Function GetRiskType(pCompanyGroupId As Guid, pRiskTypeId As Guid) As RiskType

    Function GetEquipment(pEquipmentId As Guid) As Equipment

    Function GetRegion(pAddressId As Guid, pRegionId As Guid) As Region

    Function GetServiceCenterById(pCountryId As Guid, pSvcCenterId As Guid) As ServiceCenter

    Function GetServiceCenterByCode(pCountryId As Guid, pSvcCenterCode As String) As ServiceCenter

    Function GetExpression(pExpressionId As Guid) As Expression

    'Function GetSuspendedReason(ByVal pDealerCode As String, pSuspendedReasonId As Guid) As SuspendedReason

    Function GetBankInfoById(pCountryId As Guid, pBankInfoId As Guid) As BankInfo

    Function GetBranch(pDealerId As Guid, pBranchCode As String) As Branch
End Interface
