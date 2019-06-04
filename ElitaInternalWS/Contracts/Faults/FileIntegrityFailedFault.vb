Imports System.Runtime.Serialization

<DataContract(Name:="FileIntegrityFailedFault", Namespace:="http://elita.assurant.com/Faults")> _
Public Class FileIntegrityFailedFault

    <DataMember(Name:="StoredHash", IsRequired:=True)> _
    Public Property StoredHash As String

    <DataMember(Name:="ComputedHash", IsRequired:=True)> _
    Public Property ComputedHash As String

    <DataMember(Name:="RepositoryCode", IsRequired:=True)> _
    Public Property RepositoryCode As String

    <DataMember(Name:="StoragePath", IsRequired:=True)> _
    Public Property StoragePath As String

    Public Property AbsoluteFileName As String

End Class

