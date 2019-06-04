Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations


Namespace SpecializedServices.Ascn
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Ascn/GetServiceCenterClaims", Name:="GetServiceCenterClaimsRequest")>
    Public Class GetServiceCenterClaimsRequest

        <DataMember(IsRequired:=True, Name:="CompanyCode", Order:=1), Required(), StringLength(16, MinimumLength:=1)>
        Public Property CompanyCode As String

        <DataMember(IsRequired:=True, Name:="ClaimStatus", Order:=2), Required()>
        Public Property ClaimStatus As String

        <DataMember(IsRequired:=True, Name:="ServiceCenterCode", Order:=3), Required()>
        Public Property ServiceCenterCode As String

        <DataMember(IsRequired:=True, Name:="ClaimType", Order:=4), Required()>
        Public Property ClaimType As String

        <DataMember(IsRequired:=True, Name:="MethodofRepair", Order:=5), Required()>
        Public Property MethodofRepair As String

        <DataMember(Name:="ClaimNumber", Order:=6)>
        Public Property ClaimNumber As String

        <DataMember(Name:="CertificateNumber", Order:=7)>
        Public Property CertificateNumber As String

        <DataMember(Name:="CustomerName", Order:=8)>
        Public Property CustomerName As String

        <DataMember(Name:="FromClaimCreatedDate", Order:=9)>
        Public Property FromClaimCrtDate As Date

        <DataMember(Name:="ToClaimCreatedDate", Order:=10)>
        Public Property ToClaimCrtDate As Date

        <DataMember(Name:="ClaimExtendedStatusCode", Order:=11)>
        Public Property ClaimExStusCode As String

        <DataMember(Name:="ClaimExtendedStatusOwnerCode", Order:=12)>
        Public Property ClaimExStusOwnCode As String

        <DataMember(Name:="FromVisitDate", Order:=13)>
        Public Property FromVisitDate As Date

        <DataMember(Name:="ToVisitDate", Order:=14)>
        Public Property ToVisitDate As Date

        <DataMember(IsRequired:=True, Name:="SortBy", Order:=15), Required()>
        Public Property SortBy As String

        <DataMember(IsRequired:=True, Name:="SortOrder", Order:=16), Required()>
        Public Property SortOrder As Integer

        <DataMember(IsRequired:=True, Name:="PageSize", Order:=17), Required()>
        Public Property PageSize As Integer

        <DataMember(IsRequired:=True, Name:="PageNumber", Order:=18), Required()>
        Public Property PageNumber As Integer

        <DataMember(Name:="TATRangeCode", Order:=19)>
        Public Property TATRangeCode As String

        <DataMember(Name:="AuthorizationNumber", Order:=20)>
        Public Property AuthorizationNumber As String

    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Ascn/GetServiceCenterClaims", Name:="ClaimTypeEnum")>
    Public Enum ClaimTypeEnum

        <EnumMember()>
        Repair = 0

        <EnumMember>
        OriginalReplacement = 1

    End Enum

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Ascn/GetServiceCenterClaims", Name:="ClaimExtStsOwnCodeEnum")>
    Public Enum ClaimExtStsOwnCodeEnum

        <EnumMember()>
        Retailer = 1

        <EnumMember()>
        Customer = 2

        <EnumMember()>
        ServiceCenter = 3

        <EnumMember()>
        Assurant = 4

    End Enum

End Namespace