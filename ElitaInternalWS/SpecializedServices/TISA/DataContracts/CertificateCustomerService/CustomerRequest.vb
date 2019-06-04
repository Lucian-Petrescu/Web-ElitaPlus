Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/CertificateCustomerService/GetCustomerCertificate", Name:="GetCustomerRequest")>
    Public Class CustomerRequest

        <DataMember(IsRequired:=True, Name:="CompanyCode"), Required()>
        Public Property CompanyCode As String

        <DataMember(IsRequired:=False, Name:="DealerCode")>
        Public Property DealerCode As String

    End Class
End Namespace