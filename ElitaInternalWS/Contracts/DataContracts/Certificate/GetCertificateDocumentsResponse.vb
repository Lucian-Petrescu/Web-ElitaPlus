Imports System.Collections.Generic
Imports System.Runtime.Serialization

<DataContract(Name:="GetCertificateDocumentsResponse", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class GetCertificateDocumentsResponse

    <DataMember(Name:="Documents", IsRequired:=False)>
    Public Property Documents As IEnumerable(Of CertificateDocumentInfo)
End Class
