Imports System.Runtime.Serialization

<DataContract(Name:="DownloadDocumentResponse", Namespace:="http://elita.assurant.com/DataContracts/Claim")>
Public Class DownloadServiceOrderDocumentResponse
    <DataMember(Name:="ImageData", IsRequired:=True)>
    Public Property ServiceOrderImageData As Byte()

End Class
