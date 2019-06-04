Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/CertificateCustomerService/GetCustomerCertificate", Name:="CustomerByCertificateRequest")>
    Public Class CustomerByCertificateRequest
        Inherits CustomerRequest

        <DataMember(IsRequired:=False, Name:="CertificateNumber")>
        Public Property CertificateNumber As String

        <DataMember(IsRequired:=False, Name:="PhoneNumber")>
        Public Property PhoneNumber As String

        <DataMember(IsRequired:=False, Name:="SerialNumber")>
        Public Property SerialNumber As String

        <DataMember(IsRequired:=False, Name:="IdentificationNumber")>
        Public Property IdentificationNumber As String

        <DataMember(IsRequired:=False, Name:="ServiceLineNumber")>
        Public Property ServiceLineNumber As String

    End Class



End Namespace
