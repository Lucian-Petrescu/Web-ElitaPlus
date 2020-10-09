Imports Assurant.ElitaPlus.DataEntities

Public Interface ICertificateManager
    Function GetCertificate(pDealerCode As String, pCertificateNumber As String) As Certificate

    Function GetCertificateForGwPil(pDealerCode As String, pCertificateNumber As String) As Certificate

    Sub GetCertificateCoverageRate(pCertId As Guid,
                                 pCoverageDate As Date,
                                 ByRef poGWP As Decimal,
                                 ByRef poSalexTax As Decimal)
    Sub GetFirstCertEndorseDates(pCertId As Guid,
                                 ByRef poEndorseEffectiveDate As String,
                                 ByRef poEndorseExpirationDate As String)

    Function GetCertNumber(pCertId As Guid) As String
    Function GetCertifcateByItemCoverage(pCertItemCovgId As Guid) As Certificate

    Function GetSearchCertificateResultForGwPil(pCertList As List(Of Guid)) As IEnumerable(Of Certificate)

    Function GetCertificate(pCompanyCode As String,
                            pDealerCode As String,
                            pCertificateNumber As String,
                            IdentificationNumber As String,
                            PhoneNumber As String,
                            ServiceLineNumber As String,
                            SerialNumber As String) As DataSet

    Function GetCertificateByTaxId(pCountryCode As String,
                            pIdentificationNumber As String,
                            pPhoneNumber As String,
                            pNumberOfRecords As Integer,
                            ByRef totalRecordFound As Long) As DataSet

    Function GWGetCertificateByCertNumber(pDealerCode As String,
                            pCertNumber As String) As List(Of Guid)

    Function GetCertificate(pCertId As Guid, Optional pIncludedList As String = "Item, Item.Itemcoverages") As Certificate

    Sub GetCertificateGrossAmtByProductCode(pCertId As Guid,
                                    ByRef pCurrencyCode As String,
                                    ByRef pGrossAmt As Decimal)

    Function SearchCertificateBYCustomerInfo(pCompanyCode As String, pDealerCode As String, pDealerGrp As String, pCustomerFirstName As String, pCustomerLastName As String,
                                                pWorkPhone As String, pEmail As String, pPostalCode As String, pIdentificationNumber As String) As DataSet
End Interface

