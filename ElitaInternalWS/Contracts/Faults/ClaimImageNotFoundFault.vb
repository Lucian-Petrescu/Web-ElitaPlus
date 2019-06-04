Imports System.Runtime.Serialization

<DataContract(Name:="ImageNotFound", Namespace:="http://elita.assurant.com/Faults")> _
Public Class ClaimImageNotFoundFault

    <DataMember(Name:="ImageId", IsRequired:=True)> _
    Public Property ImageId As Guid

    <DataMember(Name:="RepositoryCode", IsRequired:=True)> _
    Public Property RepositoryCode As String

End Class