Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Imports Microsoft.Practices.Unity

Public Class SpecializedCertificateManager
    Implements ICertificateManager
    Implements ISpecializedCertificateManager

    Private m_CertificateRepository As ICertificateRepository(Of Certificate)

    Private m_CertificateItemCoverageRepository As ICertificateRepository(Of CertificateItemCoverage)

    Private Property m_DealerManager As IDealerManager

    Public Property CertificateItemCoverageRepository() As ICertificateRepository(Of CertificateItemCoverage) Implements ISpecializedCertificateManager.CertificateItemCoverageRepository
        Get
            Return m_CertificateItemCoverageRepository
        End Get
        Set(ByVal value As ICertificateRepository(Of CertificateItemCoverage))
            m_CertificateItemCoverageRepository = value
        End Set
    End Property

    Public Property CertificateRepository() As ICertificateRepository(Of Certificate) Implements ISpecializedCertificateManager.CertificateRepository
        Get
            Return m_CertificateRepository
        End Get
        Set(ByVal value As ICertificateRepository(Of Certificate))
            m_CertificateRepository = value
        End Set
    End Property


    Public Sub New(ByVal pDealerManager As IDealerManager)
        Me.m_DealerManager = pDealerManager
    End Sub


    Public Sub GetCertificateCoverageRate(pCertId As Guid, pCoverageDate As Date, ByRef poGWP As Decimal, ByRef poSalexTax As Decimal) Implements ICertificateManager.GetCertificateCoverageRate
        Throw New NotImplementedException()
    End Sub
    Public Sub GetFirstCertEndorseDates(ByVal pCertId As Guid, ByRef poEndorseEffectiveDate As String, ByRef poEndorseExpirationDate As String) Implements ICertificateManager.GetFirstCertEndorseDates
        Throw New NotImplementedException()
    End Sub
    Public Sub GetCertificateGrossAmtByProductCode(pCertId As Guid, ByRef pCurrencyCode As String, ByRef pGrossAmt As Decimal) Implements ICertificateManager.GetCertificateGrossAmtByProductCode
        Throw New NotImplementedException()
    End Sub

    Public Function GetCertifcateByItemCoverage(pCertItemCovgId As Guid) As Certificate Implements ICertificateManager.GetCertifcateByItemCoverage
        Throw New NotImplementedException()
    End Function

    Public Function GetCertificate(pCertId As Guid, Optional pIncludedList As String = "Item, Item.Itemcoverages") As Certificate Implements ICertificateManager.GetCertificate
        Throw New NotImplementedException()
    End Function

    Public Function GetCertificate(pDealerCode As String, pCertificateNumber As String) As Certificate Implements ICertificateManager.GetCertificate
        Throw New NotImplementedException()
    End Function

    Public Function GetCertificate(pCompanyCode As String, pDealerCode As String, pCertificateNumber As String, pIdentificationNumber As String, pPhoneNumber As String, pServiceLineNumber As String, pSerialNumber As String) As DataSet Implements ICertificateManager.GetCertificate
        Return m_CertificateRepository.SearchCertificate(pCompanyCode,
                                                                    pDealerCode,
                                                                    pCertificateNumber,
                                                                    pPhoneNumber,
                                                                    pSerialNumber,
                                                                    pIdentificationNumber,
                                                                    pServiceLineNumber)
    End Function

    Public Function GetCertificateByTaxId(pCountryCode As String, pIdentificationNumber As String, pPhoneNumber As String, pNumberOfRecords As Integer, ByRef totalRecordFound As Long) As DataSet Implements ICertificateManager.GetCertificateByTaxId
        Throw New NotImplementedException()
    End Function

    Public Function GetCertificateForGwPil(pDealerCode As String, pCertificateNumber As String) As Certificate Implements ICertificateManager.GetCertificateForGwPil
        Throw New NotImplementedException()
    End Function

    Public Function GetSearchCertificateResultForGwPil(pCertList As List(Of Guid)) As IEnumerable(Of Certificate) Implements ICertificateManager.GetSearchCertificateResultForGwPil
        Throw New NotImplementedException()
    End Function

    Public Function SearchCertificateBYCustomerInfo(pCompanyCode As String, pDealerCode As String, pDealerGrp As String, pCustomerFirstName As String, pCustomerLastName As String, pWorkPhone As String, pEmail As String, pPostalCode As String, pIdentificationNumber As String) As DataSet Implements ICertificateManager.SearchCertificateBYCustomerInfo
        Throw New NotImplementedException()
    End Function

    Public Function GetCertNumber(pCertId As Guid) As String Implements ICertificateManager.GetCertNumber
        Throw New NotImplementedException()
    End Function
End Class
