Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public NotInheritable Class CertificateRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, ICertificateEntity})
    Inherits Repository(Of TType, CertificateContext)
    Implements ICertificateRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of CertificateContext)())
    End Sub

    Public Sub GetCertificateCoverageRate(ByVal pCertId As Guid,
                                 ByVal pCoverageDate As Date,
                                 ByRef poGWP As Decimal,
                                 ByRef poSalexTax As Decimal) Implements ICertificateRepository(Of TType).GetCertificateCoverageRate
        MyBase.Context.GetCertificateCoverageRate(pCertId, pCoverageDate, poGWP, poSalexTax)
    End Sub
    Public Sub GetFirstCertEndorseDates(ByVal pCertId As Guid,
                                 ByRef poEndorseEffectiveDate As String,
                                 ByRef poEndorseExpirationDate As String) Implements ICertificateRepository(Of TType).GetFirstCertEndorseDates

        MyBase.Context.GetFirstCertEndorseDates(pCertId, poEndorseEffectiveDate, poEndorseExpirationDate)
    End Sub

    Public Function SearchCertificate(pCompanyCode As String,
                                       pDealerCode As String,
                                       pCertificateNumber As String,
                                       pPhoneNumber As String,
                                       pSerialNumber As String,
                                       pIdentificationNumber As String,
                                       pServiceLineNumber As String) As DataSet Implements ICertificateRepository(Of TType).SearchCertificate


        Dim certlist As DataSet = MyBase.Context.SearchCertificate(pCompanyCode, pDealerCode, pCertificateNumber, pPhoneNumber, pSerialNumber,
                                                                                            pIdentificationNumber, pServiceLineNumber)

        Return certlist

    End Function

    Public Function SearchCertificateByTaxId(pCountryCode As String, pIdentificationNumber As String, pPhoneNumber As String, numberOfRecords As Integer, ByRef totalRecordFound As Long) As DataSet Implements ICertificateRepository(Of TType).SearchCertificateByTaxId
        Dim certlist As DataSet = MyBase.Context.SearchCertificateByTaxId(pCountryCode, pIdentificationNumber, pPhoneNumber, numberOfRecords, totalRecordFound)

        Return certlist
    End Function

    Public Function GWSearchCertificateByCertNumber(pDealerCode As String, pCertNumber As String) As Collections.Generic.List(Of Guid) Implements ICertificateRepository(Of TType).GWSearchCertificateByCertNumber
        Dim certlist As DataSet = MyBase.Context.GWSearchCertificateByCertNumber(pDealerCode, pCertNumber)
        Dim IdList As New Collections.Generic.List(Of Guid)
        If certlist.Tables(0).Rows.Count > 0 Then
            For Each r As DataRow In certlist.Tables(0).Rows
                IdList.Add(New Guid(CType(r("cert_id"), Byte())))
            Next
        End If
        Return IdList
    End Function

    Friend Sub GetGrossAmountByProductCode(ByVal pCertId As Guid,
                                    ByRef pCurrencyCode As String,
                                    ByRef pGrossAmt As Decimal) Implements ICertificateRepository(Of TType).GetGrossAmountByProductCode
        MyBase.Context.GetPremiumFromProduct(pCertId,
                                                    pCurrencyCode,
                                                    pGrossAmt)
    End Sub

     Public Function SearchCertificateBYCustomerInfo(ByVal pCompanyCode As String, ByVal pDealerCode As String, ByVal pDealerGrp As String, ByVal pCustomerFirstName As String, ByVal pCustomerLastName As String,
                                                ByVal pWorkPhone As String, ByVal pEmail As String, ByVal pPostalCode As String, ByVal pIdentificationNumber As String) As DataSet Implements ICertificateRepository(Of TType).SearchCertificateBYCustomerInfo
        Dim certlist As DataSet = MyBase.Context.SearchCertificateBYCustomerInfo(pCompanyCode, pDealerCode, pDealerGrp, pCustomerFirstName, pCustomerLastName,pWorkPhone,pEmail,pPostalCode,pIdentificationNumber)

        Return certlist
    End Function

End Class
