Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

<DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetServiceCenterClaims", Name:="ServiceCenterClaimInfo")>
    Public Class ServiceCenterClaimsInfo

        <DataMember>
        Public Property ClaimId As String

        <DataMember>
        Public Property ClaimNumber As String

        <DataMember>
        Public Property CompanyCode As String

        <DataMember>
        Public Property AuthorizationNumber As String

        <DataMember>
        Public Property ClaimTypeCode As String

        <DataMember>
        Public Property ClaimTypeDescription As String

        <DataMember>
        Public Property ClaimStatus As String

        <DataMember>
        Public Property MethodofRepair As String

        <DataMember>
        Public Property ClaimCreatedtDate As Date?

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
        Public Property ClaimExtendedStatusCode As String

        <DataMember>
        Public Property ClaimExtendedStatusDescription As String

        <DataMember>
        Public Property ClaimExtendedStatusDate As Date?

        <DataMember>
        Public Property ClaimExtendedStatusOwner As String

        <DataMember>
        Public Property AuthorizationAmount As Decimal?

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
        Public Property WorkPhone As String

        <DataMember>
        Public Property HomePhone As String

        <DataMember>
        Public Property CoverageType As String

        <DataMember>
        Public Property LossDate As Date?

        <DataMember>
        Public Property ClaimPaidAmount As Decimal?

        <DataMember>
        Public Property BonusTotal As Decimal?

        <DataMember>
        Public Property Deductible As Decimal?

        <DataMember>
        Public Property TaxId As String

        <DataMember>
        Public Property ClaimTAT As String

        <DataMember>
        Public Property ServiceCenterTAT As String

        <DataMember>
        Public Property DealerCode As String

        <DataMember>
        Public Property DealerName As String

    End Class

End Namespace


