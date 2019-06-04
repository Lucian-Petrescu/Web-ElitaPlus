Imports System.Runtime.Serialization

<DataContract(Name:="DownloadDocumentResponse", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class DownloadDocumentResponse

    <DataMember(Name:="ImageData", IsRequired:=True)> _
    Public Property ImageData As Byte()

End Class
