Imports System.Runtime.Serialization
Imports System.Collections.Generic

<DataContract(Name:="UpdateClaimRequest", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class UpdateClaimRequest

    <DataMember(Name:="ClaimsSearch", IsRequired:=True)> _
    Public Property ClaimsSearch As ClaimLookup

    <DataMember(Name:="Operations", IsRequired:=True)> _
    Public Property Operations As IEnumerable(Of ClaimOperation)

End Class
