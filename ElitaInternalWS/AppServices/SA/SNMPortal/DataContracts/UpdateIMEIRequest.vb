
Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations

Namespace AppServices.SA
    <DataContract(Namespace:="http://elita.assurant.com/AppServices/SA/UpdateImeiRequest", Name:="UpdateImeiRequest")>
    Public Class UpdateImeiRequest


        <DataMember(IsRequired:=True, Name:="CompanyCode", Order:=1), Required(), StringLength(16, MinimumLength:=1)>
        Public Property CompanyCode As String

        <DataMember(IsRequired:=True, Name:="DealerCode", Order:=2), Required()>
        Public Property DealerCode As String

        <DataMember(IsRequired:=True, Name:="CertificateNumber", Order:=3), Required(), StringLength(20, MinimumLength:=1)>
        Public Property CertificateNumber As String

        <DataMember(IsRequired:=True, Name:="OldIMEI", Order:=4), Required(), StringLength(16, MinimumLength:=1)>
        Public Property OldMEI As String

        <DataMember(IsRequired:=True, Name:="NewImei", Order:=5), Required(), StringLength(16, MinimumLength:=1)>
        Public Property NewImei As String

        <DataMember(Order:=6)>
        Public Property NewSKU As String

        <DataMember(Order:=7)>
        Public Property NewMake As String

        <DataMember(Order:=8)>
        Public Property NewModel As String

    End Class
End Namespace