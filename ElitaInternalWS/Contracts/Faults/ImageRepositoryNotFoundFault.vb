Imports System.Runtime.Serialization

<DataContract(Name:="ImageRepositoryNotFoundFault", Namespace:="http://elita.assurant.com/Faults")> _
Public Class ImageRepositoryNotFoundFault

    <DataMember(Name:="RepositoryCode", IsRequired:=True)> _
    Public Property RepositoryCode As String

End Class


