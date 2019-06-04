Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations

Namespace SpecializedServices.Abag
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetClaimDetail", Name:="GetClaimDetailRequest")>
    Public Class GetClaimDetailRequest

        <DataMember(IsRequired:=True, Name:="ClaimNumber"), Required(), StringLength(20, MinimumLength:=1)>
        Public Property ClaimNumber As String

        <DataMember(IsRequired:=True, Name:="CompanyCode"), Required(), StringLength(16, MinimumLength:=1)>
        Public Property CompanyCode As String

        <DataMember()>
        Public Property ForServiceCenterUse As DetailsListEnum

        <DataMember()>
        Public Property IncludePartDescriptions As DetailsListEnum

    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetClaimDetail", Name:="DetailsListEnum")>
    Public Enum DetailsListEnum

        <EnumMember()>
        No = 0

        <EnumMember>
        PartsInfo = 1

        <EnumMember>
        Comments = 1

    End Enum

End Namespace