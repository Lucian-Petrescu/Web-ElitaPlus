Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Timb
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Timb/CertificateClaimService/GetCertClaimInfo", Name:="CertificateClaimRequest")>
    Public Class CertificateClaimRequest

#Region "DataMember"

        <DataMember(IsRequired:=True, Name:="CompanyCode", Order:=1), Required()>
        Public Property CompanyCode() As String

        <DataMember(IsRequired:=True, Name:="CertificateNumber", Order:=2), Required()>
        Public Property CertificateNumber() As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="SerialNumber", Order:=3)>
        Public Property SerialNumber() As String

        <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="PhoneNumber", Order:=4)>
        Public Property PhoneNumber() As String
#End Region

    End Class
End Namespace
