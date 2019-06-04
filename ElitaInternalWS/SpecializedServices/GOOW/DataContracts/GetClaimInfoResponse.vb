Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/GetClaimInfo", Name:="GetClaimInfoResponse")>
    Public Class GetClaimInfoResponse
        <DataMember>
        Public Property CustomerName As String

        'Don't use elp_shipping for Shipping Info Use 

        <DataMember>
        Public Property ShippingInfo As ShippingInfo

        <DataMember>
        Public Property CertificateNumber As String

        <DataMember>
        Public Property AuthorizedAmount As Decimal

        <DataMember>
        Public Property ClaimNumber As String

        <DataMember>
        Public Property TrackingNumber As String

        <DataMember>
        Public Property Make As String

        <DataMember>
        Public Property Model As String

        <DataMember>
        Public Property SerialNumber As String
    End Class
End Namespace