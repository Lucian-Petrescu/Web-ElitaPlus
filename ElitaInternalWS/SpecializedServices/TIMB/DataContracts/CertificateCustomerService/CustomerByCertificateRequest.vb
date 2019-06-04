Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Timb
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Timb/CertificateCustomerService/GetCustomerCertificate", Name:="CustomerByCertificateRequest")>
    Public Class CustomerByCertificateRequest

        <DataMember(IsRequired:=True, Name:="CompanyCode"), Required()>
        Public Property CompanyCode As String

        <DataMember(IsRequired:=True, Name:="CertificateNumber"), Required()>
        Public Property CertificateNumber As String

         <DataMember(IsRequired:=False, Name:="DealerCode")>
        Public Property DealerCode As String


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
