Imports Assurant.ElitaPlus.DataEntities

Public Interface ICountryManager
    Function GetCountry(countryId As Guid) As Country

    Function GetCountryByCode(countryCode As String) As Country

    Function GetRegion(AddressId As Guid, RegionId As Guid) As Region

    Function GetServiceCenterById(countryId As Guid, svcCenterId As Guid) As ServiceCenter

    Function GetServiceCenterByCode(countryId As Guid, svcCenterCode As String) As ServiceCenter


    Function GetBankInfoById(countryId As Guid, BankInfoId As Guid) As BankInfo

End Interface
