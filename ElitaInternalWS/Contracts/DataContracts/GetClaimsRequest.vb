Imports System.Runtime.Serialization

<DataContract(Name:="GetClaimsRequest", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class GetClaimsRequest

    <DataMember(Name:="ClaimsSearch", IsRequired:=True)> _
    Public Property ClaimsSearch As ClaimLookup

End Class
