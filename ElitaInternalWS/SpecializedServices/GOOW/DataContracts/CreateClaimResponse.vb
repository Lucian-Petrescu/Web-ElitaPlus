Imports System.Runtime.Serialization

Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/CreateClaim", Name:="CreateClaimResponse")>
    Public Class CreateClaimResponse
        <DataMember(IsRequired:=True, Name:="ClaimNumber")>
        Public Property ClaimNumber As String

    End Class
End Namespace
