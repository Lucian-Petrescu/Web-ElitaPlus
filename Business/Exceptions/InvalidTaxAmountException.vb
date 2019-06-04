Imports System.Runtime.Serialization
<Serializable>
    Public Class InvalidTaxAmountException
        Inherits Exception

    Public ReadOnly CountryId As Nullable(Of Guid)
    Public ReadOnly TaxAmount As Decimal

    Public Sub New(ByVal pTaxAmount As Decimal, ByVal pCountryId As Nullable(Of Guid))
        TaxAmount = pTaxAmount
        CountryId = pCountryId
    End Sub
    Public Sub New(ByVal pTaxAmount As Decimal, ByVal pCountryId As Nullable(Of Guid), pMessage As String)
        MyBase.New(pMessage)
        TaxAmount = pTaxAmount
        CountryId = pCountryId
    End Sub
    Public Sub New(ByVal pTaxAmount As Decimal, ByVal pCountryId As Nullable(Of Guid), pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        TaxAmount = pTaxAmount
        CountryId = pCountryId
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
        End Sub
    End Class


