Imports System.Runtime.Serialization

<DataContract(Name:="DownloadDocumentRequest", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class DownloadDocumentRequest

    <DataMember(Name:="ClaimsSearch", IsRequired:=True)> _
    Public Property ClaimsSearch As ClaimLookup

    <DataMember(Name:="ImageId", IsRequired:=True)> _
    Public Property ImageId As Guid

End Class