Imports System.Runtime.Serialization
Imports System.Collections.Generic

Namespace SpecializedServices.Ascn

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Ascn/GetServiceCenterClaims", Name:="GetServiceCenterClaimsResponse")>
    Public Class GetServiceCenterClaimsResponse

        <DataMember>
        Public Property ServiceCenterClaims As IEnumerable(Of ServiceCenterClaimsInfo)

        <DataMember>
        Public Property TotalRecordCount As String

    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Ascn/GetServiceCenterClaims", Name:="ServiceCenterClaimInfo")>
    Public Class ServiceCenterClaimsInfo
        <DataMember>
        Public Property ClaimNumber As String

        <DataMember>
        Public Property AuthorizationNumber As String

        <DataMember>
        Public Property ClaimTypeDescription As String

        <DataMember>
        Public Property ClaimStatus As String

        <DataMember>
        Public Property MethodofRepair As String

        <DataMember>
        Public Property ClaimCreatedtDate As Date?

        <DataMember>
        Public Property ProblemDescription As String

        <DataMember>
        Public Property CertificateNumber As String

        <DataMember>
        Public Property ProductDescription As String

        <DataMember>
        Public Property Model As String

        <DataMember>
        Public Property Make As String

        <DataMember>
        Public Property CustomerName As String

        <DataMember>
        Public Property ClaimExtendedStatusDescription As String

        <DataMember>
        Public Property ClaimExtendedStatusOwner As String

        <DataMember>
        Public Property AuthorizationAmount As String

        <DataMember>
        Public Property ServiceCenterCode As String

        <DataMember>
        Public Property ServiceCenterDescription As String

        <DataMember>
        Public Property VisitDate As Date?

        <DataMember>
        Public Property RepairDate As Date?

        <DataMember>
        Public Property BatchNumber As String

        <DataMember>
        Public Property SerialNumber As String

        <DataMember>
        Public Property CoverageType As String

        <DataMember>
        Public Property CustomerAddress As String

        <DataMember>
        Public Property CustomerCity As String

        <DataMember>
        Public Property CustomerProvince As String

        <DataMember>
        Public Property HomePhone As String

        <DataMember>
        Public Property WorkPhone As String

        <DataMember>
        Public Property CustomerEmail As String

        <DataMember>
        Public Property ProductSalePrice As String

        <DataMember>
        Public Property LossDate As Date?

        <DataMember>
        Public Property BonusTotal As String

        <DataMember>
        Public Property ClaimTAT As String

        <DataMember>
        Public Property ServiceCenterTAT As String
        <DataMember>
        Public Property ConsumerPaid As String

        <DataMember>
        Public Property SalvageAmount As String

        <DataMember>
        Public Property AssurantPays As String

        <DataMember>
        Public Property ClaimPaidAmount As String

        <DataMember>
        Public Property CoverageStartDate As Date?

        <DataMember>
        Public Property CoverageEndDate As Date?
        '<DataMember>
        'Public Property ServiceCenterTAT2 As String

    End Class
End Namespace