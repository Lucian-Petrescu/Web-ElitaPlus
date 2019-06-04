Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Imports System.ServiceModel

<DataContract(Name:="CertificateNumberLookup", Namespace:="http://elita.assurant.com/DataContracts/Certificate")> _
Public Class CertificateNumberLookup
    Inherits CertificateLookup

    <DataMember(Name:="CertificateNumber", IsRequired:=True), StringLength(20, ErrorMessage:="CertificateNumber value exceeded maximum length of 20"),
    Required(ErrorMessage:="CertificateNumber is Required")> _
    Public Property CertificateNumber As String


    <DataMember(Name:="DealerCode", IsRequired:=True), StringLength(5, ErrorMessage:="DealerCode value exceeded maximum length of 5"),
    Required(ErrorMessage:="DealerCode is Required")> _
    Public Property DealerCode As String


    Friend Overrides Sub Validate()
        If String.IsNullOrEmpty(CertificateNumber) Then
            Throw New FaultException(Of InvalidCertificateNumber)(New InvalidCertificateNumber(), "CertificateNumber is Required")
        Else
            If CertificateNumber.Length > 20 Then
                Throw New FaultException(Of InvalidCertificateNumber)(New InvalidCertificateNumber(), "CertificateNumber value exceeded maximum length of 20")
            End If
        End If

        If String.IsNullOrEmpty(DealerCode) Then
            Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer code is Required")
        Else
            If DealerCode.Length > 5 Then
                Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault(), "Dealer Code value exceeded maximum length of 5")
            End If
        End If
    End Sub
End Class
