Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace Assurant.SNMPortal
    <DataContract(Namespace:="http://elita.assurant.com/SNMPortal/SaveClaimInfo", Name:="GetClaimInfoRequest")>
    Public Class GetClaimInfoRequest

        <DataMember(IsRequired:=True, Name:="DealerCode"),
        Required(), StringLength(10, MinimumLength:=1)>
        Public Property DealerCode As String

        <DataMember(IsRequired:=True, Name:="CertificateNumber"),
        Required(), StringLength(20, MinimumLength:=1)>
        Public Property CertNumber As String

        <DataMember(IsRequired:=True, Name:="DateOfLoss"),
        Required()>
        Public Property DateOfLoss As Date

        <DataMember(IsRequired:=True, Name:="CoverageCode"),
        Required(), StringLength(5, MinimumLength:=1)>
        Public Property CoverageCode As String

        <DataMember(IsRequired:=True, Name:="ServiceCenterCode"),
        Required(), StringLength(5, MinimumLength:=1)>
        Public Property ServiceCenterCode As String

        <DataMember(IsRequired:=True, Name:="ProblemDescription"),
        Required(), StringLength(500, MinimumLength:=1)>
        Public Property ProblemDescription As String

        <DataMember(IsRequired:=False, Name:="ServiceLevel"),
        StringLength(2, MinimumLength:=1)>
        Public Property ServiceLevel As String

        <DataMember(IsRequired:=False, Name:="TechnicalReport"),
        StringLength(500, MinimumLength:=1)>
        Public Property TechnicalReport As String

        <DataMember(IsRequired:=False, Name:="ContactName"),
        StringLength(50, MinimumLength:=1)>
        Public Property ContactName As String

        <DataMember(IsRequired:=False, Name:="ContactNumber"),
        StringLength(10, MinimumLength:=1)>
        Public Property ContactNumber As String

        <DataMember(IsRequired:=False, Name:="EmailAddress"),
       StringLength(10, MinimumLength:=1), RegularExpression("[\w-]+@([\w-]+\.)+[\w-]+", ErrorMessage:="Invalid Email Address")>
        Public Property ContactEmail As String

        <DataMember(IsRequired:=False, Name:="ExtendStatus"),
        StringLength(5, MinimumLength:=1)>
        Public Property ExtendedStatus As String

        <DataMember(IsRequired:=False, Name:="ClaimAuthorizedAmount")>
        Public Property AuthAmount As Decimal

        <DataMember(IsRequired:=False, Name:="ClaimType"),
        StringLength(3, MinimumLength:=1)>
        Public Property ClaimType As String

        <DataMember(IsRequired:=False, Name:="ClaimNumber"),
        StringLength(20, MinimumLength:=1)>
        Public Property ClaimNumber As String

    End Class
End Namespace