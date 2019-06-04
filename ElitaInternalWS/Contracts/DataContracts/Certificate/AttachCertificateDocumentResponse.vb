Imports System.Runtime.Serialization

<DataContract(Name:="AttachCertificateDocumentResponse", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class AttachCertificateDocumentResponse
    <DataMember(Name:="ImageId", IsRequired:=True)>
    Public Property ImageId As Guid
End Class
