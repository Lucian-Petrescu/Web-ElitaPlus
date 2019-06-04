Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/CreateClaim", Name:="CreateClaimRequest")>
    Public Class CreateClaimRequest

        <DataMember(IsRequired:=True, Name:="DealerCode"),
        Required(), StringLength(5, MinimumLength:=1)>
        Public Property DealerCode As String

        <DataMember(IsRequired:=True, Name:="CertificateNumber"),
        Required(), StringLength(20, MinimumLength:=1)>
        Public Property CertificateNumber As String

        <DataMember(IsRequired:=True, Name:="DateOfLoss"),
        Required()>
        Public Property DateOfLoss As Date

        <DataMember(IsRequired:=True, Name:="DamageType"),
        Required()>
        Public Property CauseOfLoss As DamageTypeEnum

        ''TODO confirm with Guo
        <DataMember(IsRequired:=True, Name:="CoverageTypeCode"),
        Required(), StringLength(255, MinimumLength:=1)>
        Public Property CoverageTypeCode As String

        <DataMember(IsRequired:=True, Name:="ServiceCenterCode"),
        Required(), StringLength(40, MinimumLength:=1)>
        Public Property ServiceCenterCode As String

        <DataMember(IsRequired:=False, Name:="ProblemDescription"),
        Required(), StringLength(500, MinimumLength:=1)>
        Public Property ProblemDescription As String

        <DataMember(IsRequired:=False, Name:="ContactName"),
        StringLength(50)>
        Public Property ContactName As String

        <DataMember(IsRequired:=False, Name:="ContactNumber"),
        StringLength(15, MinimumLength:=1)>
        Public Property ContactNumber As String

        <DataMember(IsRequired:=False, Name:="EmailAddress")>
        Public Property ContactEmail As String

        <DataMember(IsRequired:=False, Name:="Model"), StringLength(100)>
        Public Property Model As String

        <DataMember(IsRequired:=False, Name:="Make"), StringLength(255)>
        Public Property Make As String

        <DataMember(IsRequired:=True, Name:="ClaimType")>
        Public Property ClaimType As ClaimTypeEnum

        <DataMember(IsRequired:=True, Name:="Comments"), Required()>
        Public Property Comments As CommentTypeEnum

    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/CreateClaim", Name:="ClaimTypeEnum")>
    Public Enum ClaimTypeEnum

        <EnumMember()>
        Repair = 0

        <EnumMember>
        OriginalReplacement = 1

        <EnumMember>
        RepairToReplacement = 2

    End Enum

End Namespace