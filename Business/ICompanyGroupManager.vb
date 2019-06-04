Imports Assurant.ElitaPlus.DataEntities

Public Interface ICompanyGroupManager
    Function GetCompanyGroup(pCompanyGroupId As Guid) As CompanyGroup

    'Function GetRiskType(pCompanyGroupId As Guid, pRiskTypeId As Guid) As RiskType
End Interface
