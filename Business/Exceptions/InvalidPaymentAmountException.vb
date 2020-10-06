Imports System.Runtime.Serialization
<Serializable>
Public Class InvalidPaymentAmountException
    Inherits Exception

    Public ReadOnly ClaimNumber As String
    Public ReadOnly PaymentAmount As Decimal

    Public Sub New(pPaymentAmount As Decimal, pClaimNumber As String)
        PaymentAmount = pPaymentAmount
        ClaimNumber = pClaimNumber
    End Sub
    Public Sub New(pPaymentAmount As Decimal, pClaimNumber As String, pMessage As String)
        MyBase.New(pMessage)
        PaymentAmount = pPaymentAmount
        ClaimNumber = pClaimNumber
    End Sub
    Public Sub New(pPaymentAmount As Decimal, pClaimNumber As String, pMessage As String, pInner As Exception)
        MyBase.New(pMessage, pInner)
        PaymentAmount = pPaymentAmount
        ClaimNumber = pClaimNumber
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class


