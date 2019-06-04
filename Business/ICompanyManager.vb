Imports Assurant.ElitaPlus.DataEntities

Public Interface ICompanyManager
    Function GetCompany(pCompanyId As Guid) As Company

    Function GetCompanyForGwPil(pCompanyId As Guid) As Company

    Function GetCompany(pCompanyCode As String) As Company

    Function GetCompanyForGwPil(pCompanyCode As String) As Company

End Interface
