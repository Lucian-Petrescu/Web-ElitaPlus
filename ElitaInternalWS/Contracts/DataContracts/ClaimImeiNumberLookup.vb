Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ComponentModel.DataAnnotations

<DataContract(Name:="ClaimImeiNumberLookup", Namespace:="http://elita.assurant.com/DataContracts/Claim")>
Public Class ClaimImeiNumberLookup
    Inherits ClaimLookup

    <DataMember(Name:="ImeiNumber", IsRequired:=True), StringLength(30, ErrorMessage:="ImeiNumber value exceeded maximum length of 30"),
    Required(ErrorMessage:="ImeiNumber is Required")>
    Public Property ImeiNumber As String

    <DataMember(Name:="DealerCode"), StringLength(5, ErrorMessage:="DealerCode value exceeded maximum length of 5")>
    Public Property DealerCode As String

    <DataMember(Name:="CompanyCode"), StringLength(20, ErrorMessage:="CompanyCode value exceeded maximum length of 20")>
    Public Property CompanyCode As String

    <DataMember(Name:="CountryCode"), StringLength(20, ErrorMessage:="CountryCode value exceeded maximum length of 20")>
    Public Property CountryCode As String

    Friend Overrides Sub Validate()
        ' MyBase.Validate()
        If String.IsNullOrEmpty(ImeiNumber) Then
            Throw New FaultException(Of InvalidImeiNumber)(New InvalidImeiNumber(), "Imei Number is Required")
        Else
            If ImeiNumber.Length > 30 Then
                Throw New FaultException(Of InvalidImeiNumber)(New InvalidImeiNumber(), "Imei Number value exceeded maximum length of 30")
            End If
        End If

        If String.IsNullOrEmpty(DealerCode) AndAlso String.IsNullOrEmpty(CompanyCode) AndAlso String.IsNullOrEmpty(CountryCode) Then
            Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Please enter either Country Code or Company Code or Dealer Code")
        End If

        If Not String.IsNullOrEmpty(DealerCode) AndAlso Not String.IsNullOrEmpty(CompanyCode) AndAlso Not String.IsNullOrEmpty(CountryCode) Then
            Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Please enter either Country Code or Company Code or Dealer Code")
        End If

        If String.IsNullOrEmpty(DealerCode) AndAlso Not String.IsNullOrEmpty(CompanyCode) AndAlso Not String.IsNullOrEmpty(CountryCode) Then
            Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Please enter either Country Code or Company Code or Dealer Code")
        End If

        If Not String.IsNullOrEmpty(DealerCode) AndAlso String.IsNullOrEmpty(CompanyCode) AndAlso Not String.IsNullOrEmpty(CountryCode) Then
            Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Please enter either Country Code or Company Code or Dealer Code")
        End If

        If Not String.IsNullOrEmpty(DealerCode) AndAlso Not String.IsNullOrEmpty(CompanyCode) AndAlso String.IsNullOrEmpty(CountryCode) Then
            Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Please enter either Country Code or Company Code or Dealer Code")
        End If
    End Sub
End Class
