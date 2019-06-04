Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization
Imports ElitaInternalWS.Claims

Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/PayClaim", Name:="PayClaimRequest")>
    Public Class PayClaimRequest

        <DataMember(IsRequired:=True, Name:="CompanyCode"), StringLength(5, MinimumLength:=1)>
        Public Property CompanyCode As String

        <DataMember(IsRequired:=True, Name:="DealerCode")>
        Public Property DealerCode As String

        <DataMember(IsRequired:=True, Name:="CertificateNumber"), Required()>
        Public Property CertificateNumber As String

        <DataMember(IsRequired:=True, Name:="ClaimNumber"), Required(), StringLength(20, MinimumLength:=1)>
        Public Property ClaimNumber As String

        <DataMember(IsRequired:=True, Name:="ServiceCenterCode"), StringLength(10)>
        Public Property ServiceCenterCode As String

        <DataMember(IsRequired:=True, Name:="InvoiceNumber"), Required(), StringLength(10, MinimumLength:=1)>
        Public Property InvoiceNumber As String

        <DataMember(IsRequired:=True, Name:="PaymentAmount"), Required()>
        Public Property PaymentAmount As Decimal

    End Class
End Namespace