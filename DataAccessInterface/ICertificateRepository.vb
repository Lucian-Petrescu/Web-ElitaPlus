Imports Assurant.ElitaPlus.DataEntities

Public Interface ICertificateRepository(Of TEntity As {BaseEntity, ICertificateEntity})
    Inherits IRepository(Of TEntity)

    Function SearchCertificate(ByVal pCompanyCode As String,
                                 ByVal pDealerCode As String,
                                 ByVal pCertificateNumber As String,
                                 ByVal pPhoneNumber As String,
                                 ByVal pSerialNumber As String,
                                 ByVal pIdentificationNumber As String,
                                 ByVal pServiceLineNumber As String) As DataSet

    Function SearchCertificateByTaxId(ByVal pCountryCode As String,
                                 ByVal pIdentificationNumber As String,
                                 ByVal pPhoneNumber As String,
                                 ByVal numberOfRecords As Integer,
                                 ByRef totalRecordFound As Long) As DataSet

    Sub GetCertificateCoverageRate(ByVal pCertId As Guid,
                                 ByVal pCoverageDate As Date,
                                 ByRef poGWP As Decimal,
                                 ByRef poSalexTax As Decimal)
    Sub GetFirstCertEndorseDates(ByVal pCertId As Guid,
                                 ByRef pEndorseEffectiveDate As String,
                                 ByRef pEndorseExpirationDate As String)

    Sub GetGrossAmountByProductCode(ByVal pCertId As Guid,
                                    ByRef pCurrencyCode As String,
                                    ByRef pGrossAmt As Decimal)

    Function SearchCertificateBYCustomerInfo(ByVal pCompanyCode As String, ByVal pDealerCode As String, ByVal pDealerGrp As String, ByVal pCustomerFirstName As String, ByVal pCustomerLastName As String,
                                                 ByVal pWorkPhone As String, ByVal pEmail As String, ByVal pPostalCode As String, ByVal pAccountNumber As String) As DataSet

    Function GWSearchCertificateByCertNumber(ByVal pDealerCode As String,
                             ByVal pCertNumber As String) As Collections.Generic.List(Of Guid)

End Interface
