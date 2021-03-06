﻿Imports Microsoft.Practices.Unity
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Business

Public Class CertificateManager
    Implements ICertificateManager

    Private Property m_CertificateRepository As ICertificateRepository(Of Certificate)

    Private Property m_CertificateItemCoverageRepository As ICertificateRepository(Of CertificateItemCoverage)

    Private Property m_DealerManager As IDealerManager

    Public Sub New(ByVal pCertificateRepository As ICertificateRepository(Of Certificate),
                   ByVal pDealerManager As IDealerManager,
                   ByVal pCertificateItemCoverageRepository As ICertificateRepository(Of CertificateItemCoverage))
        Me.m_CertificateRepository = pCertificateRepository
        Me.m_DealerManager = pDealerManager
        Me.m_CertificateItemCoverageRepository = pCertificateItemCoverageRepository

    End Sub

    Public Function GetCertificate(ByVal pDealerCode As String, ByVal pCertificateNumber As String) As Certificate Implements ICertificateManager.GetCertificate
        pCertificateNumber = pCertificateNumber.ToUpperInvariant()
        Dim dealer As Dealer = m_DealerManager.GetDealer(pDealerCode)
        If (dealer Is Nothing) Then
            Throw New DealerNotFoundException(Guid.Empty, pDealerCode)
        End If
        Return m_CertificateRepository.Get(Function(c) c.DealerId = dealer.DealerId And c.CertificateNumber = pCertificateNumber, Nothing, "Item, Item.ItemCoverages, Item.ItemCoverages.ItemCoverageDeductibles, Cancellations,BillingDetails").FirstOrDefault()

    End Function
    Public Function GetCertificateForGwPil(ByVal pDealerCode As String, ByVal pCertificateNumber As String) As Certificate Implements ICertificateManager.GetCertificateForGwPil

        pCertificateNumber = pCertificateNumber.ToUpperInvariant()

        Dim dealer As Dealer = m_DealerManager.GetDealerForGwPil(pDealerCode)

        If (dealer Is Nothing) Then
            Throw New DealerNotFoundException(Guid.Empty, pDealerCode)
        End If

        Dim searchResultIdList As Collections.Generic.List(Of Guid)
        searchResultIdList = GWGetCertificateByCertNumber(pDealerCode, pCertificateNumber)
        Dim objCert As Certificate, objCertEndorse As Certificate
        objCert = m_CertificateRepository.Get(Function(c) searchResultIdList.Contains(c.CertificateId), Nothing, "Item, Item.ItemCoverages, Item.ItemCoverages.ItemCoverageDeductibles, Cancellations,BillingDetails, CertificateInstallments").FirstOrDefault()

        'load endorsement separately to avoid error "Oracle 11.2.0.4.0 does not support APPLY" when add the Item.Endorsements to the include list
        If Not objCert Is Nothing Then
            objCertEndorse = m_CertificateRepository.Get(Function(c) c.CertificateId = objCert.CertificateId, Nothing, "Item, Item.Endorsements").FirstOrDefault()
            For Each objItem In objCert.Item
                For Each objE In objCertEndorse.Item
                    If objItem.CertificateItemId = objE.CertificateItemId Then
                        objItem.Endorsements = objE.Endorsements
                    End If
                Next
            Next
        End If

        Return objCert
    End Function
    Public Function GetCertNumber(ByVal pCertId As Guid) As String Implements ICertificateManager.GetCertNumber
        Dim objCert As Certificate
        objCert = m_CertificateRepository.Get(Function(c) c.CertificateId = pCertId).FirstOrDefault()
        If Not objCert Is Nothing Then
            Return objCert.CertificateNumber
        Else
            Return String.Empty
        End If
    End Function

    Public Function GetCertifcateByItemCoverage(ByVal pCertItemCovgId As Guid) As Certificate Implements ICertificateManager.GetCertifcateByItemCoverage

        Dim certItemCoverage As CertificateItemCoverage = m_CertificateItemCoverageRepository.Get(Function(cic) cic.CertItemCoverageId = pCertItemCovgId).FirstOrDefault

        Return m_CertificateRepository.Get(Function(c) c.CertificateId = certItemCoverage.CertificateId, Nothing, "Item, Item.Itemcoverages, Item.ItemCoverages.ItemCoverageDeductibles").FirstOrDefault()

    End Function

    Public Function GetSearchCertificateResultForGwPil(pCertList As List(Of Guid)) As IEnumerable(Of Certificate) Implements ICertificateManager.GetSearchCertificateResultForGwPil
        Return m_CertificateRepository.Get(Function(c) pCertList.Contains(c.CertificateId), Nothing, "Item, Item.ItemCoverages,Item.Endorsements")
    End Function

    Public Function GetCertificate(ByVal pCompanyCode As String,
                                   ByVal pDealerCode As String,
                                   ByVal pCertificateNumber As String,
                                   ByVal pIdentificationNumber As String,
                                   ByVal pPhoneNumber As String,
                                   ByVal pServiceLineNumber As String,
                                   ByVal pSerialNumber As String) As DataSet Implements ICertificateManager.GetCertificate


        Return m_CertificateRepository.SearchCertificate(pCompanyCode,
                                                         pDealerCode,
                                                         pCertificateNumber,
                                                         pPhoneNumber,
                                                         pSerialNumber,
                                                         pIdentificationNumber,
                                                         pServiceLineNumber)





    End Function

    Public Function GetCertificateByTaxId(pCountryCode As String, pIdentificationNumber As String, pPhoneNumber As String, pNumberOfRecords As Integer, ByRef totalRecordFound As Long) As DataSet Implements ICertificateManager.GetCertificateByTaxId
        Return m_CertificateRepository.SearchCertificateByTaxId(pCountryCode,
                                                         pIdentificationNumber,
                                                         pPhoneNumber,
                                                         pNumberOfRecords,
                                                         totalRecordFound)
    End Function

    Public Function GWGetCertificateByCertNumber(pDealerCode As String, pCertNumber As String) As Collections.Generic.List(Of Guid) Implements ICertificateManager.GWGetCertificateByCertNumber
        Return m_CertificateRepository.GWSearchCertificateByCertNumber(pDealerCode,
                                                         pCertNumber)
    End Function

    Public Function GetCertificate(pCertId As Guid, Optional pIncludedList As String = "Item, Item.Itemcoverages") As Certificate Implements ICertificateManager.GetCertificate
        Return m_CertificateRepository.Get(Function(c) c.CertificateId = pCertId, Nothing, pIncludedList).FirstOrDefault()
    End Function

    Public Sub GetCertificateCoverageRate(ByVal pCertId As Guid,
                                 ByVal pCoverageDate As Date,
                                 ByRef poGWP As Decimal,
                                 ByRef poSalexTax As Decimal) Implements ICertificateManager.GetCertificateCoverageRate

        m_CertificateRepository.GetCertificateCoverageRate(pCertId, pCoverageDate, poGWP, poSalexTax)
    End Sub
    Public Sub GetFirstCertEndorseDates(ByVal pCertId As Guid,
                                 ByRef poEndorseEffectiveDate As String,
                                 ByRef poEndorseExpirationDate As String) Implements ICertificateManager.GetFirstCertEndorseDates

        m_CertificateRepository.GetFirstCertEndorseDates(pCertId, poEndorseEffectiveDate, poEndorseExpirationDate)
    End Sub
    Public Sub GetCertificateGrossAmtByProductCode(ByVal pCertId As Guid,
                                    ByRef pCurrencyCode As String,
                                    ByRef pGrossAmt As Decimal) Implements ICertificateManager.GetCertificateGrossAmtByProductCode

        m_CertificateRepository.GetGrossAmountByProductCode(pCertId, pCurrencyCode, pGrossAmt)

    End Sub

      Public Function SearchCertificateBYCustomerInfo(ByVal pCompanyCode As String, ByVal pDealerCode As String, ByVal pDealerGrp As String, ByVal pCustomerFirstName As String, ByVal pCustomerLastName As String,
                                                ByVal pWorkPhone As String, ByVal pEmail As String, ByVal pPostalCode As String, ByVal pIdentificationNumber As String) As DataSet Implements ICertificateManager.SearchCertificateBYCustomerInfo
        Return m_CertificateRepository.SearchCertificateBYCustomerInfo(pCompanyCode,
                                                         pDealerCode,
                                                         pDealerGrp,
                                                         pCustomerFirstName,
                                                         pCustomerLastName,
                                                         pWorkPhone,
                                                         pEmail,
                                                         pPostalCode,
                                                         pIdentificationNumber)
    End Function
End Class
