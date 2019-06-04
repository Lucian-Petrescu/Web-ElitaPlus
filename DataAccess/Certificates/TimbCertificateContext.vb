Public Class TimbCertificateContext
    Inherits BaseSpecializedCertificateContext


    Public Sub New()
        MyBase.New()
    End Sub
    Protected Overrides ReadOnly Property CustomizationName As String
        Get
            Return "TIMB"
        End Get
    End Property

    Friend Overrides Sub GetCertificateCoverageRate(pCertId As Guid, pCoverageDate As Date, ByRef poGWP As Decimal, ByRef poSalexTax As Decimal)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Sub GetPremiumFromProduct(pCertId As Guid, ByRef pCurrencyCode As String, ByRef pGrossAmt As Decimal)
        Throw New NotImplementedException()
    End Sub

    Friend Overrides Function SearchCertificateBYCustomerInfo(pCompanyCode As String, pDealerCode As String, pDealerGrp As String, pCustomerFirstName As String, pCustomerLastName As String, pWorkPhone As String, pEmail As String, pPostalCode As String, pIdentificationNumber As String) As DataSet
        Throw New NotImplementedException()
    End Function

    Friend Overrides Function SearchCertificateByTaxId(pCountryCode As String, pIdentificationNumber As String, pPhoneNumber As String, numberOfRecords As Integer, ByRef totalRecordFound As Long) As DataSet
        Throw New NotImplementedException()
    End Function
End Class
