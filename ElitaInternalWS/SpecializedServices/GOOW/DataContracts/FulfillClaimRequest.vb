Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/FulfillClaim", Name:="FulfillClaimRequest")>
    Public Class FulfillClaimRequest
        Inherits BaseClaimRequest

        <DataMember(IsRequired:=True, Name:="ClaimType"), Required()>
        Public Property ClaimType As ClaimTypeEnum

        <DataMember(IsRequired:=True, Name:="RepairDate"), Required()>
        Public Property RepairDate As Date

        <DataMember(IsRequired:=False, Name:="Model"), StringLength(100)>
        Public Property Model As String

        <DataMember(IsRequired:=False, Name:="Make"), StringLength(255)>
        Public Property Make As String

        <DataMember(IsRequired:=False, Name:="SerialNumber"), StringLength(30)>
        Public Property SerialNumber As String

        <DataMember(IsRequired:=True, Name:="ServiceCenterCode"), StringLength(40)>
        Public Property ServiceCenterCode As String

        <DataMember(IsRequired:=False, Name:="TrackingNumber"), StringLength(40)>
        Public Property TrackingNumber As String


    End Class
End Namespace
