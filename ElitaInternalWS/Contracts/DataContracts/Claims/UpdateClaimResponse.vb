Imports System.Runtime.Serialization
Imports System.Collections.Generic

<DataContract(Name:="UpdateClaimResponse", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class UpdateClaimResponse

    <DataMember(Name:="ClaimsUpdated", IsRequired:=True)> _
    Public Property ClaimsUpdated As Integer

End Class
