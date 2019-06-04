Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Imports System.ServiceModel

<DataContract(Name:="CertAfterUpgradeLookup", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class CertAfterUpgradeLookup
    Inherits CertificateLookup

    <DataMember(Name:="SerialNumber", IsRequired:=True), StringLength(30, ErrorMessage:="SerialNumber value exceeded maximum length of 30"),
    Required(ErrorMessage:="SerialNumber is Required")>
    Public Property SerialNumber As String

    <DataMember(Name:="IdentificationNumber", IsRequired:=True), StringLength(20, ErrorMessage:="IdentificationNumber value exceeded maximum length of 20"),
    Required(ErrorMessage:="IdentificationNumber is Required")>
    Public Property IdentificationNumber As String

    <DataMember(Name:="DealerCode", IsRequired:=True), StringLength(5, ErrorMessage:="DealerCode value exceeded maximum length of 5"),
    Required(ErrorMessage:="DealerCode is Required")>
    Public Property DealerCode As String

    <DataMember(Name:="UpgradeDate", IsRequired:=True),
    Required(ErrorMessage:="Upgrade Date is Required")>
    Public Property UpgradeDate As Date

    Friend Overrides Sub Validate()
        ' MyBase.Validate()
        If String.IsNullOrEmpty(SerialNumber) Then
            Throw New FaultException(Of InvalidSerialnumber)(New InvalidSerialnumber(), "SerialNumber Number is Required")
        Else
            If SerialNumber.Length > 30 Then
                Throw New FaultException(Of InvalidSerialnumber)(New InvalidSerialnumber(), "SerialNumber value exceeded maximum length of 30")
            End If
        End If

        If String.IsNullOrEmpty(IdentificationNumber) Then
            Throw New FaultException(Of InvalidIdentificationNumber)(New InvalidIdentificationNumber(), "Identification Number is Required")
        Else
            If IdentificationNumber.Length > 20 Then
                Throw New FaultException(Of InvalidIdentificationNumber)(New InvalidIdentificationNumber(), "IdentificationNumber value exceeded maximum length of 20")
            End If
        End If
        If String.IsNullOrEmpty(DealerCode) Then
            Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer code is Required")
        Else
            If DealerCode.Length > 5 Then
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer Code value exceeded maximum length of 5")
            End If
        End If
        If UpgradeDate.ToString Is Nothing Then
            Throw New FaultException(Of InvalidUpgradeDateFault)(New InvalidUpgradeDateFault(), "Upgrade Date is Required")
        End If
    End Sub
End Class
