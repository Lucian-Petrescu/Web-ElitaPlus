Imports System.Runtime.Serialization
Imports System.Collections.Generic

<DataContract(Name:="GetClaimsResponse", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class GetClaimsResponse

    <DataMember(Name:="Claims", IsRequired:=False)> _
    Public Property Claims As IEnumerable(Of ClaimInfo)

    <DataMember(Name:="ClaimExists", IsRequired:=True)> _
    Public Property ClaimExists As Boolean

    <DataMember(Name:="ClaimsCount", IsRequired:=True)> _
    Public Property ClaimsCount As Integer

End Class
