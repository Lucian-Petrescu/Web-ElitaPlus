Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Security

Public Class CountryManager
    Implements ICountryManager

    Private ReadOnly m_CacheFacade As ICacheFacade

    Public Sub New(ByVal pCacheFacade As ICacheFacade)
        m_CacheFacade = pCacheFacade
    End Sub

    Private ReadOnly Property CacheFacade As ICacheFacade
        Get
            Return m_CacheFacade
        End Get
    End Property

    Public Function GetCountry(pCountryId As Guid) As Country Implements ICountryManager.GetCountry
        Dim oCountry As Country = CacheFacade.GetCountry(pCountryId)

        Return oCountry
    End Function

    Public Function GetCountryByCode(pCountryCode As String) As Country Implements ICountryManager.GetCountryByCode
        Dim oCountry As Country = CacheFacade.GetCountryByCode(pCountryCode)

        Return oCountry
    End Function

    Public Function GetRegion(pCountryId As Guid, pRegionId As Guid) As Region Implements ICountryManager.GetRegion
        Dim oRegion As Region = CacheFacade.GetRegion(pCountryId, pRegionId)

        Return oRegion
    End Function

    Public Function GetServiceCenterById(pCountryId As Guid, pSvcCenterId As Guid) As ServiceCenter Implements ICountryManager.GetServiceCenterById
        Dim oSvcCenter As ServiceCenter = CacheFacade.GetServiceCenterById(pCountryId, pSvcCenterId)

        Return oSvcCenter
    End Function

    Public Function GetServiceCenterByCode(pCountryId As Guid, pSvcCenterCode As String) As ServiceCenter Implements ICountryManager.GetServiceCenterByCode
        Try
            Dim oSvcCenter As ServiceCenter = CacheFacade.GetServiceCenterByCode(pCountryId, pSvcCenterCode)
            Return oSvcCenter
        Catch ex As Exception
            Throw New ServiceCenterNotFoundException(pCountryId, pSvcCenterCode)
        End Try

    End Function

    Public Function GetBankInfoById(pCountryId As Guid, pBankInfoId As Guid) As BankInfo Implements ICountryManager.GetBankInfoById
        Dim oBankInfo As BankInfo = CacheFacade.GetBankInfoById(pCountryId, pBankInfoId)

        Return oBankInfo
    End Function

End Class
