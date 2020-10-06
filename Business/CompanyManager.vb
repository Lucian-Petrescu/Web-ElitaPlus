
Imports System.Threading
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Security

Public Class CompanyManager
    Implements ICompanyManager

    Private ReadOnly m_CacheFacade As ICacheFacade

    Public Sub New(pCacheFacade As ICacheFacade)
        m_CacheFacade = pCacheFacade
    End Sub

    Private ReadOnly Property CacheFacade As ICacheFacade
        Get
            Return m_CacheFacade
        End Get
    End Property

    Public Function GetCompany(pCompanyId As Guid) As Company Implements ICompanyManager.GetCompany
        Dim oCompany As Company = CacheFacade.GetCompany(pCompanyId)

        If (oCompany Is Nothing) AndAlso (Not Thread.CurrentPrincipal.HasCompany(oCompany.CompanyId)) Then
            Throw New CompanyNotFoundException(pCompanyId, String.Empty)
        End If

        Return oCompany
    End Function

    Public Function GetCompanyForGwPil(pCompanyId As Guid) As Company Implements ICompanyManager.GetCompanyForGwPil
        Dim oCompany As Company = CacheFacade.GetCompany(pCompanyId)

        If (oCompany Is Nothing) Then
            Throw New CompanyNotFoundException(pCompanyId, String.Empty)
        End If

        Return oCompany
    End Function

    Public Function GetCompany(pCompanyCode As String) As Company Implements ICompanyManager.GetCompany
        Dim oCompany As Company = CacheFacade.GetCompany(pCompanyCode)

        If (oCompany Is Nothing) AndAlso (Not Thread.CurrentPrincipal.HasCompany(oCompany.CompanyId)) Then
            Throw New CompanyNotFoundException(Nothing, pCompanyCode, "Company Not Found")
        End If

        Return oCompany
    End Function

    Public Function GetCompanyForGwPil(pCompanyCode As String) As Company Implements ICompanyManager.GetCompanyForGwPil
        Dim oCompany As Company
        Try
            oCompany = CacheFacade.GetCompany(pCompanyCode)
        Catch ex As Exception
            oCompany = Nothing
        End Try


        If (oCompany Is Nothing) Then
            Throw New CompanyNotFoundException(Nothing, pCompanyCode, "Company Not Found")
        End If

        Return oCompany
    End Function
End Class
