Imports System.Runtime.Serialization

<DataContract(Name:="ClaimImageInfo", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class ClaimImageInfo

    <DataMember(Name:="ImageId", IsRequired:=True)> _
    Public Property ImageId As Guid

    <DataMember(Name:="DocumentType", IsRequired:=True)> _
    Public Property DocumentTypeCode As String

    <DataMember(Name:="ImageStatus", IsRequired:=True)> _
    Public Property ImageStatus As String

    <DataMember(Name:="ScanDate", IsRequired:=True)> _
    Public Property ScanDate As DateTime

    <DataMember(Name:="FileName", IsRequired:=True)> _
    Public Property FileName As String

    <DataMember(Name:="Comments", IsRequired:=False)> _
    Public Property Comments As String

    <DataMember(Name:="FileSizeBytes", IsRequired:=True)> _
    Public Property FileSizeBytes As Integer

End Class
