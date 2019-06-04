Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetServiceCenter", Name:="GetServiceCenterResponse")>
    Public Class GetServiceCenterResponse

        <DataMember>
        Public Property Code As String

        <DataMember>
        Public Property Description As String

        <DataMember>
        Public Property IvaResponsible As String

        <DataMember>
        Public Property TaxId As String

        <DataMember>
        Public Property ContactName As String

        <DataMember>
        Public Property OwnerName As String

        <DataMember>
        Public Property Phone1 As String

        <DataMember>
        Public Property Phone2 As String

        <DataMember>
        Public Property Fax As String

        <DataMember>
        Public Property Email As String

        <DataMember>
        Public Property CcEmail As String

        <DataMember>
        Public Property BusinessHours As String

        <DataMember>
        Public Property PayMaster As String

        <DataMember>
        Public Property MasterCenterCode As String

        <DataMember>
        Public Property ServiceCenterAddress As String
    End Class
End Namespace