Imports System.Runtime.Serialization

<DataContract(Name:="DownloadCertificateDocumentResponse", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class DownloadCertificateDocumentResponse

    <DataMember(Name:="ImageData", IsRequired:=True)>
    Public Property ImageData As Byte()

End Class