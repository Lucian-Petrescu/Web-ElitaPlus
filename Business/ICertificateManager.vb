Imports Assurant.ElitaPlus.DataEntities

Public Interface ICertificateManager
    Function GetCertificate(ByVal pDealerCode As String, ByVal pCertificateNumber As String) As Certificate

    Function GetCertificateForGwPil(ByVal pDealerCode As String, ByVal pCertificateNumber As String) As Certificate

    Sub GetCertificateCoverageRate(ByVal pCertId As Guid,
                                 ByVal pCoverageDate As Date,
                                 ByRef poGWP As Decimal,
                                 ByRef poSalexTax As Decimal)
    Sub GetFirstCertEndorseDates(ByVal pCertId As Guid,
                                 ByRef poEndorseEffectiveDate As String,
                                 ByRef poEndorseExpirationDate As String)

    Function GetCertNumber(ByVal pCertId As Guid) As String
    Function GetCertifcateByItemCoverage(ByVal pCertItemCovgId As Guid) As Certificate

    Function GetSearchCertificateResultForGwPil(ByVal pCertList As Collections.Generic.List(Of Guid)) As IEnumerable(Of Certificate)

    Function GetCertificate(ByVal pCompanyCode As String,
                            ByVal pDealerCode As String,
                            ByVal pCertificateNumber As String,
                            ByVal IdentificationNumber As String,
                            ByVal PhoneNumber As String,
                            ByVal ServiceLineNumber As String,
                            ByVal SerialNumber As String) As DataSet

    Function GetCertificateByTaxId(ByVal pCountryCode As String,
                            ByVal pIdentificationNumber As String,
                            ByVal pPhoneNumber As String,
                            ByVal pNumberOfRecords As Integer,
                            ByRef totalRecordFound As Long) As DataSet

    Function GetCertificate(ByVal pCertId As Guid, Optional pIncludedList As String = "Item, Item.Itemcoverages") As Certificate

    Sub GetCertificateGrossAmtByProductCode(ByVal pCertId As Guid,
                                    ByRef pCurrencyCode As String,
                                    ByRef pGrossAmt As Decimal)

    Function SearchCertificateBYCustomerInfo(ByVal pCompanyCode As String, ByVal pDealerCode As String, ByVal pDealerGrp As String, ByVal pCustomerFirstName As String, ByVal pCustomerLastName As String,
                                                ByVal pWorkPhone As String, ByVal pEmail As String, ByVal pPostalCode As String, ByVal pIdentificationNumber As String) As DataSet
End Interface

