Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ComponentModel.DataAnnotations

<DataContract(Name:="ClaimSerialNumberLookup", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class ClaimSerialNumberLookup
    Inherits ClaimLookup

    <DataMember(Name:="SerialNumber", IsRequired:=True), StringLength(30, ErrorMessage:="SerialNumber value exceeded maximum length of 30"),
    Required(ErrorMessage:="SerialNumber is Required")> _
    Public Property SerialNumber As String


    <DataMember(Name:="DealerCode"), StringLength(5, ErrorMessage:="DealerCode value exceeded maximum length of 5")>
    Public Property DealerCode As String

    <DataMember(Name:="CompanyCode"), StringLength(20, ErrorMessage:="CompanyCode value exceeded maximum length of 20")>
    Public Property CompanyCode As String

    <DataMember(Name:="CountryCode"), StringLength(20, ErrorMessage:="CountryCode value exceeded maximum length of 20")>
    Public Property CountryCode As String

    Friend Overrides Sub Validate()
        ' MyBase.Validate()
        If String.IsNullOrEmpty(SerialNumber) Then
            Throw New FaultException(Of InvalidSerialnumber)(New InvalidSerialnumber(), "SerialNumber Number is Required")
        Else
            If SerialNumber.Length > 30 Then
                Throw New FaultException(Of InvalidSerialnumber)(New InvalidSerialnumber(), "SerialNumber value exceeded maximum length of 30")
            End If
        End If

        If String.IsNullOrEmpty(DealerCode) And String.IsNullOrEmpty(CompanyCode) And String.IsNullOrEmpty(CountryCode) Then
            Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Please enter either Country Code or Company Code or Dealer Code")
        End If

        If Not String.IsNullOrEmpty(DealerCode) And Not String.IsNullOrEmpty(CompanyCode) And Not String.IsNullOrEmpty(CountryCode) Then
            Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Please enter either Country Code or Company Code or Dealer Code")
        End If

        If String.IsNullOrEmpty(DealerCode) And Not String.IsNullOrEmpty(CompanyCode) And Not String.IsNullOrEmpty(CountryCode) Then
            Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Please enter either Country Code or Company Code or Dealer Code")
        End If

        If Not String.IsNullOrEmpty(DealerCode) And String.IsNullOrEmpty(CompanyCode) And Not String.IsNullOrEmpty(CountryCode) Then
            Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Please enter either Country Code or Company Code or Dealer Code")
        End If

        If Not String.IsNullOrEmpty(DealerCode) And Not String.IsNullOrEmpty(CompanyCode) And String.IsNullOrEmpty(CountryCode) Then
            Throw New FaultException(Of InvalidRequestFault)(New InvalidRequestFault(), "Please enter either Country Code or Company Code or Dealer Code")
        End If
    End Sub
End Class
