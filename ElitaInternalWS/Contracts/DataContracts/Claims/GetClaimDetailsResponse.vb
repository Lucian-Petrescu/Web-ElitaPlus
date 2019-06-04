Imports System.Runtime.Serialization
Imports System.Collections.Generic

<DataContract(Name:="GetClaimDetailsResponse", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class GetClaimDetailsResponse

    <DataMember(Name:="Images", IsRequired:=False)> _
    Public Property Images As IEnumerable(Of ClaimImageInfo)

End Class
