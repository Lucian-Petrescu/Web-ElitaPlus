Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations

<DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/FVSTMobileApplicationService/GetCertificateInfo", Name:="GetCertificateInfoRequest")>
Public Class GetCertificateInfoRequest


    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="CertificateNumber", Order:=1),
        Required(), StringLength(20, MinimumLength:=1)>
    Public Property CertificateNumber As String


End Class
