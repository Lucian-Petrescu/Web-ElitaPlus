Imports System.Runtime.Serialization

<DataContract(Name:="CertificateDocumentNotFound", Namespace:="http://elita.assurant.com/Faults")>
Public Class CertificateDocumentNotFoundFault

    <DataMember(Name:="ImageId", IsRequired:=True)>
    Public Property ImageId As Guid

    <DataMember(Name:="RepositoryCode", IsRequired:=True)>
    Public Property RepositoryCode As String

End Class