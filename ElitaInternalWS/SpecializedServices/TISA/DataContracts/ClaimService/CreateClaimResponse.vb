Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/CreateClaim", Name:="CreateClaimResponse")>
    Public Class CreateClaimResponse

        <DataMember(IsRequired:=True, Name:="ClaimNumber")>
        Public Property ClaimNumber As String

    End Class
End Namespace