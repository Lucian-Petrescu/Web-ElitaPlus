Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/GetCertificate", Name:="GetCertificateInfoRequest")>
    Public Class GetCertificateInfoRequest

        <DataMember(IsRequired:=True, Name:="CertificateNumber"),
        Required(), StringLength(20, MinimumLength:=1)>
        Public Property CertificateNumber As String


        <DataMember(IsRequired:=True, Name:="DealerCode"),
        Required(), StringLength(10, MinimumLength:=1)>
        Public Property DealerCode As String


        <DataMember(IsRequired:=False, Name:="Culture")>
        Public Property Culture As String

    End Class
End Namespace