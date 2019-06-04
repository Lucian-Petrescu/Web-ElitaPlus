Imports System.Runtime.Serialization

<DataContract(Name:="AttachDocumentResponse", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class AttachDocumentResponse

    <DataMember(Name:="ImageId", IsRequired:=True)> _
    Public Property ImageId As Guid

End Class
