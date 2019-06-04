Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/GetClaimInfo", Name:="GetClaimInfoRequest")>
    Public Class GetClaimInfoRequest
        <DataMember(IsRequired:=True, Name:="DealerCode", Order:=1), Required()>
        Public Property DealerCode As String

        <DataMember(IsRequired:=True, Name:="CertificateNumber", Order:=2), Required()>
        Public Property CertificateNumber As String

        <DataMember(IsRequired:=True, Name:="ClaimNumber", Order:=3), Required()>
        Public Property ClaimNumber As String
    End Class
End Namespace