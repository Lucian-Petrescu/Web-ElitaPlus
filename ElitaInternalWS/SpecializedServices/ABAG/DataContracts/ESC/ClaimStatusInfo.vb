Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/ClaimStatusInfo", Name:="ClaimStatusInfo")>
    Public Class ClaimStatusInfo

        <DataMember>
        Public Property ClaimExtendedStatusCode As String

        <DataMember>
        Public Property ClaimExtendedStatusDescription As String

        <DataMember>
        Public Property ClaimExtendedStatusDate As Date?

        <DataMember>
        Public Property ClaimExtendedStatusComments As String

        <DataMember>
        Public Property ClaimExtendedStatusOwnerDescription As String

    End Class

End Namespace