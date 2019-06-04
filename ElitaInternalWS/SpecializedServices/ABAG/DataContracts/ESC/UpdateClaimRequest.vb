Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations

Namespace SpecializedServices.Abag
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/UpdateClaim", Name:="UpdateClaimRequest")>
    Public Class UpdateClaimRequest
        <DataMember(IsRequired:=True, Name:="CompanyCode", Order:=1), Required(), StringLength(16, MinimumLength:=1)>
        Public Property CompanyCode As String

        <DataMember(IsRequired:=True, Name:="ClaimNumber", Order:=2), Required()>
        Public Property ClaimNumber As String

        <DataMember(Name:="ClaimExtendedStatusList", Order:=3)>
        Public Property ClaimExtendedStatusList As IEnumerable(Of ClaimExtInfo)

        <DataMember(Name:="PartsList", Order:=4)>
        Public Property PartsListInfo As IEnumerable(Of PartsList)

        <DataMember(Name:="OtherDescription", Order:=5), StringLength(200)>
        Public Property OtherDescription As String

        <DataMember(Name:="OtherAmount", Order:=6)>
        Public Property OtherAmount As Decimal?

        <DataMember(Name:="TripAmount", Order:=7)>
        Public Property TripAmount As Decimal?

        <DataMember(Name:="ShipmentAmount", Order:=8)>
        Public Property ShipmentAmount As Decimal?

        <DataMember(Name:="ServiceChargeAmount", Order:=9)>
        Public Property ServiceChargeAmount As Decimal?

        <DataMember(Name:="LabourAmount", Order:=10)>
        Public Property LabourAmount As Decimal?

        <DataMember(Name:="PartAmount", Order:=11)>
        Public Property PartAmount As Decimal?

        <DataMember(Name:="VisitDate", Order:=12)>
        Public Property VisitDate As Date?

        <DataMember(Name:="RepairDate", Order:=12)>
        Public Property RepairDate As Date?

    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/SVCUpdateClaim", Name:="ClaimExtInfo")>
    Public Class ClaimExtInfo

        <DataMember(IsRequired:=True, Name:="ClaimExtendedStatusCode", Order:=1), Required(), StringLength(10)>
        Public Property ClaimExtendedStatusCode As String

        <DataMember(IsRequired:=True, Name:="ClaimExtendedStatusDate", Order:=2), Required()>
        Public Property ClaimExtendedStatusDate As Date?

        <DataMember(IsRequired:=True, Name:="ClaimExtendedStatusComment", Order:=3), Required(), StringLength(300)>
        Public Property ClaimExtendedStatusComments As String

    End Class


    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/SVCUpdateClaim", Name:="PartsList")>
    Public Class PartsList

        <DataMember(IsRequired:=True, Name:="PartAmount", Order:=1), Required()>
        Public Property Amount As Double?

        <DataMember(IsRequired:=True, Name:="PartCode", Order:=2), Required(), StringLength(60)>
        Public Property Code As String

        <DataMember(IsRequired:=True, Name:="StockFlag", Order:=3), Required()>
        Public Property StockCode As StockCodeEnum

    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/SVCUpdateClaim", Name:="StockCodeEnum")>
    Public Enum StockCodeEnum

        <EnumMember()>
        Y
        <EnumMember>
        N

    End Enum

End Namespace