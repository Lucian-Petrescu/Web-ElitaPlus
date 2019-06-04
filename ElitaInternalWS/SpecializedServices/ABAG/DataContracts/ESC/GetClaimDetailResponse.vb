Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetClaimDetail", Name:="GetClaimDetailResponse")>
    Public Class GetClaimDetailResponse

        <DataMember>
        Public Property CompanyCode As String

        <DataMember>
        Public Property ClaimNumber As String

        <DataMember>
        Public Property CoverageType As String

        <DataMember>
        Public Property AuthorizationNumber As String

        <DataMember>
        Public Property MethodofRepair As String

        <DataMember>
        Public Property ClaimTypeCode As String

        <DataMember>
        Public Property ClaimCreatedtDate As Date?

        <DataMember>
        Public Property ProblemDescription As String

        <DataMember>
        Public Property VisitDate As Date?

        <DataMember>
        Public Property RepairDate As Date?

        <DataMember>
        Public Property CertificateNumber As String

        <DataMember>
        Public Property ProductDescription As String

        <DataMember>
        Public Property Model As String

        <DataMember>
        Public Property Make As String

        <DataMember>
        Public Property SerialNumber As String

        <DataMember>
        Public Property IvaResponsible As String

        <DataMember>
        Public Property TaxId As String

        <DataMember>
        Public Property Zipcode As String

        <DataMember>
        Public Property StatusCode As String

        <DataMember>
        Public Property DealerCode As String

        <DataMember>
        Public Property DealerBranchCode As String

        <DataMember>
        Public Property CoverageStartDate As Date?

        <DataMember>
        Public Property CoverageEndDate As Date?

        <DataMember>
        Public Property NonReplacementClaimsCount As Integer

        <DataMember>
        Public Property TotalAuthorizedAmount As Decimal?

        <DataMember>
        Public Property ProductSalePrice As Decimal?

        <DataMember>
        Public Property CustomerName As String

        <DataMember>
        Public Property CustomerAddress As String

        <DataMember>
        Public Property CustomerCity As String

        <DataMember>
        Public Property CustomerProvince As String

        <DataMember>
        Public Property CustomerHomePhone As String

        <DataMember>
        Public Property CustomerWorkPhone As String

        <DataMember>
        Public Property CustomerEmail As String

        <DataMember>
        Public Property LabourAmount As Decimal?

        <DataMember>
        Public Property OtherAmount As Decimal?

        <DataMember>
        Public Property OtherDescription As String

        <DataMember>
        Public Property ServiceChargeAmount As Decimal?

        <DataMember>
        Public Property TripAmount As Decimal?

        <DataMember>
        Public Property ShipmentAmount As Decimal?

        <DataMember>
        Public Property AuthorizedAmount As Decimal?

        <DataMember>
        Public Property ConsumerPaid As Decimal?

        <DataMember>
        Public Property SalvageAmount As Decimal?

        <DataMember>
        Public Property AssurantPays As Decimal?

        <DataMember>
        Public Property Deductible As Decimal?

        <DataMember>
        Public Property TotalPaid As Decimal?

        <DataMember>
        Public Property ClaimCommentsList As IEnumerable(Of ClaimCommentList)

        <DataMember>
        Public Property ClaimExtendedStatusList As IEnumerable(Of ClaimStatusInfo)

        <DataMember>
        Public Property PartsDescriptionInfo As IEnumerable(Of PartsDescriptionInfo)

        <DataMember>
        Public Property PartsListInfo As IEnumerable(Of PartsListInfo)

        <DataMember>
        Public Property DealerName As String
    End Class
End Namespace
