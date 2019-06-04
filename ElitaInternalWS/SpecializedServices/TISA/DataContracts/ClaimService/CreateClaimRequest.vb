Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/CreateClaim", Name:="CreateClaimRequest")>
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

        <DataMember(IsRequired:=True, Name:="CoverageTypeCode"),
        Required(), StringLength(255, MinimumLength:=1)>
        Public Property CoverageTypeCode As String

        <DataMember(IsRequired:=True, Name:="ServiceCenterCode"),
        Required(), StringLength(40, MinimumLength:=1)>
        Public Property ServiceCenterCode As String

        <DataMember(IsRequired:=True, Name:="ProblemDescription"),
        Required(), StringLength(500, MinimumLength:=1)>
        Public Property ProblemDescription As String

        <DataMember(IsRequired:=False, Name:="ContactName"),
        StringLength(50)>
        Public Property ContactName As String

        <DataMember(IsRequired:=False, Name:="ContactNumber"),
        StringLength(20, MinimumLength:=1)>
        Public Property ContactNumber As String

        <DataMember(IsRequired:=False, Name:="EmailAddress")>
        Public Property ContactEmail As String

        <DataMember(IsRequired:=False, Name:="ExtendedStatuses")>
        Public Property ExtendedStatuses As List(Of ExtendedStatus)

        <DataMember(IsRequired:=True, Name:="ClaimType")>
        Public Property ClaimType As ClaimTypeEnum

        <DataMember(IsRequired:=False, Name:="Make")>
        <StringLength(255)>
        Public Property Make As String

        <DataMember(IsRequired:=False, Name:="Model")>
        <StringLength(100)>
        Public Property Model As String

        <DataMember(IsRequired:=False, Name:="WorkOrderNumber")>
        <StringLength(15)>
        Public Property WorkOrderNumber As String

    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/CreateClaim", Name:="ClaimTypeEnum")>
    Public Enum ClaimTypeEnum
        <EnumMember()>
        Repair = 0

        <EnumMember>
        OriginalReplacement = 1

    End Enum
End Namespace