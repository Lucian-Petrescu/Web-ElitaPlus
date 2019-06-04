Imports System.Runtime.Serialization

<DataContract(Name:="ClaimNotFoundFault", Namespace:="http://elita.assurant.com/Faults")> _
Public Class ClaimNotFoundFault

    <DataMember(Name:="ClaimSearch", IsRequired:=True)> _
    Public Property ClaimSearch As ClaimLookup

End Class
