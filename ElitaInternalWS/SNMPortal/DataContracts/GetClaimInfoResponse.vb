Imports System.Runtime.Serialization

Namespace Assurant.SNMPortal
    <DataContract(Namespace:="http://elita.assurant.com/SNMPortal/SaveClaimInfo", Name:="GetClaimInfoRequest")>
    Public Class GetClaimInfoResponse

        <DataMember(IsRequired:=True, Name:="ClaimNumber")>
        Public Property ClaimNumber As String

    End Class
End Namespace