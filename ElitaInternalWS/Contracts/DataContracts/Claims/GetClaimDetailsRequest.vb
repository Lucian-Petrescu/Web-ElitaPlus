Imports System.Runtime.Serialization

<DataContract(Name:="GetClaimDetailsRequest", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class GetClaimDetailsRequest

    <DataMember(Name:="ClaimsSearch", IsRequired:=True)> _
    Public Property ClaimsSearch As ClaimLookup

End Class
