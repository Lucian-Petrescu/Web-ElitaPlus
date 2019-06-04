Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization

Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/BaseClaim", Name:="BaseClaimRequest")>
    Public Class BaseClaimRequest
        <DataMember(IsRequired:=True, Name:="CompanyCode"), StringLength(5, MinimumLength:=1)>
        Public Property CompanyCode As String

        <DataMember(IsRequired:=True, Name:="DealerCode")>
        Public Property DealerCode As String

        <DataMember(IsRequired:=True, Name:="CertificateNumber"), Required()>
        Public Property CertificateNumber As String

        <DataMember(IsRequired:=True, Name:="ClaimNumber"), Required(), StringLength(20, MinimumLength:=1)>
        Public Property ClaimNumber As String

        <DataMember(IsRequired:=True, Name:="Comments"), Required()>
        Public Property Comments As CommentTypeEnum

    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/BaseClaim", Name:="UpdateActionType")>
    Public Enum UpdateActionType
        <EnumMember>
        UnderMFW = 1                'RUOEM 
        <EnumMember>
        NoProblemFound = 2          'NVF 
        <EnumMember>
        Repaired = 3                'REPRD
        <EnumMember>
        RepairedBoardSwap = 4       'REPBSWAP
        <EnumMember>
        RepairedWithRefurbParts = 5 'TRAFALREP
        <EnumMember>
        WaitingForSpareParts = 6    'EQDEM
        <EnumMember>
        ReplacedRefurbished = 7     'TRAIRR
        <EnumMember>
        Irreparable = 8             'CHLIRR
        <EnumMember>
        NotRepairedMissingParts = 9 'FALREP
        <EnumMember>
        DeviceDelivered = 10        'EQDE
    End Enum


    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/BaseClaim", Name:="CommentTypeEnum")>
    Public Enum CommentTypeEnum
        <EnumMember()>
        Ack_Email_Sent

        <EnumMember>
        Claim_Cancelled

        <EnumMember>
        Warranty_Changed

        <EnumMember>
        Device_Shipped

        <EnumMember>
        Trade_In_Received

        <EnumMember>
        Trade_In_Not_Received

    End Enum

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/BaseClaim", Name:="DamageTypeEnum")>
    Public Enum DamageTypeEnum

        <EnumMember>
        LIQUIDONLY

        <EnumMember>
        BRSCREEN

        <EnumMember>
        SCREENLQ

        <EnumMember>
        OTHERDMG


    End Enum
End Namespace