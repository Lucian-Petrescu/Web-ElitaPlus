Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Security

Public Class CompanyGroupManager
    Implements ICompanyGroupManager

    Private ReadOnly m_CacheFacade As ICacheFacade

    Public Sub New(ByVal pCacheFacade As ICacheFacade)
        m_CacheFacade = pCacheFacade
    End Sub

    Private ReadOnly Property CacheFacade As ICacheFacade
        Get
            Return m_CacheFacade
        End Get
    End Property

    Public Function GetCompanyGroup(pCompanyGroupId As Guid) As CompanyGroup Implements ICompanyGroupManager.GetCompanyGroup
        Dim oCompanyGroup As CompanyGroup = CacheFacade.GetCompanyGroup(pCompanyGroupId)

        'If (Not oCompany Is Nothing) AndAlso (Threading.Thread.CurrentPrincipal.HasCompany(oCompany.CompanyId)) Then
        '    Throw New CompanyNotFoundException(Nothing, pCompanyCode)
        'End If

        Return oCompanyGroup
    End Function

    'Public Function GetRiskTypes(pCompanyGroupId As Guid, pRiskTypeId As Guid) As RiskType Implements ICompanyGroupManager.GetRiskType
    '    Dim oRiskTypes As RiskType = CacheFacade.GetRiskType(pCompanyGroupId, pRiskTypeId)

    '    'If (Not oCompany Is Nothing) AndAlso (Threading.Thread.CurrentPrincipal.HasCompany(oCompany.CompanyId)) Then
    '    '    Throw New CompanyNotFoundException(Nothing, pCompanyCode)
    '    'End If

    '    Return oRiskTypes
    'End Function


End Class

