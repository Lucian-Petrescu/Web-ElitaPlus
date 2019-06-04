Imports System.Runtime.Serialization

<DataContract(Name:="CertificateDocumentInfo", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class CertificateDocumentInfo
    <DataMember(Name:="ImageId", IsRequired:=True)>
    Public Property ImageId As Guid

    <DataMember(Name:="DocumentType", IsRequired:=True)>
    Public Property DocumentTypeCode As String

    <DataMember(Name:="ScanDate", IsRequired:=True)>
    Public Property ScanDate As DateTime

    <DataMember(Name:="FileName", IsRequired:=True)>
    Public Property FileName As String

    <DataMember(Name:="Comments", IsRequired:=False)>
    Public Property Comments As String

    <DataMember(Name:="FileSizeBytes", IsRequired:=True)>
    Public Property FileSizeBytes As Integer
End Class
