Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Imports Microsoft.Practices.Unity

Public NotInheritable Class TimbCertificateRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, ICertificateEntity})
    Inherits Repository(Of TType, TimbCertificateContext)
    Implements ICertificateRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of TimbCertificateContext))
    End Sub

    Public Sub GetCertificateCoverageRate(pCertId As Guid, pCoverageDate As Date, ByRef poGWP As Decimal, ByRef poSalexTax As Decimal) Implements ICertificateRepository(Of TType).GetCertificateCoverageRate
        Throw New NotImplementedException()
    End Sub
    Public Sub GetFirstCertEndorseDates(ByVal pCertId As Guid, ByRef pEndorseEffectiveDate As String, ByRef pEndorseExpirationDate As String) Implements ICertificateRepository(Of TType).GetFirstCertEndorseDates
        Throw New NotImplementedException()
    End Sub

    Public Sub GetGrossAmountByProductCode(pCertId As Guid, ByRef pCurrencyCode As String, ByRef pGrossAmt As Decimal) Implements ICertificateRepository(Of TType).GetGrossAmountByProductCode
        Throw New NotImplementedException()
    End Sub


    Public Function SearchCertificate(pCompanyCode As String, pDealerCode As String, pCertificateNumber As String, pPhoneNumber As String, pSerialNumber As String, pIdentificationNumber As String, pServiceLineNumber As String) As DataSet Implements ICertificateRepository(Of TType).SearchCertificate

        Dim certlist As DataSet = MyBase.Context.SearchCertificate(pCompanyCode,
                                                                    pDealerCode,
                                                                    pCertificateNumber,
                                                                    pPhoneNumber,
                                                                    pSerialNumber,
                                                                    pIdentificationNumber,
                                                                    pServiceLineNumber)

        Return certlist

    End Function

    Public Function SearchCertificateBYCustomerInfo(pCompanyCode As String, pDealerCode As String, pDealerGrp As String, pCustomerFirstName As String, pCustomerLastName As String, pWorkPhone As String, pEmail As String, pPostalCode As String, pAccountNumber As String) As DataSet Implements ICertificateRepository(Of TType).SearchCertificateBYCustomerInfo
        Throw New NotImplementedException()
    End Function

    Public Function SearchCertificateByTaxId(pCountryCode As String, pIdentificationNumber As String, pPhoneNumber As String, numberOfRecords As Integer, ByRef totalRecordFound As Long) As DataSet Implements ICertificateRepository(Of TType).SearchCertificateByTaxId
        Throw New NotImplementedException()
    End Function
End Class
