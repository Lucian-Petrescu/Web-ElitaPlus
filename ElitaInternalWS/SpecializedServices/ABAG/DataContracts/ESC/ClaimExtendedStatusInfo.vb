Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetExtendedClaimStatusList", Name:="ClaimExtendedStatusInfo")>
    Public Class ClaimExtendedStatusInfo

        <DataMember>
        Public Property Code As String

        <DataMember>
        Public Property Description As String

        <DataMember>
        Public Property GroupNumber As String

        <DataMember>
        Public Property OrderNumber As String

        <DataMember>
        Public Property OwnerCode As String

        <DataMember>
        Public Property SkippingAllowedCode As String
    End Class
End Namespace
