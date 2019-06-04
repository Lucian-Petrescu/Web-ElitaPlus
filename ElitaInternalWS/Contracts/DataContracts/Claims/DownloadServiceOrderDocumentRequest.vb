Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations

<DataContract(Name:="DownloadDocumentRequest", Namespace:="http://elita.assurant.com/DataContracts/Claim")>
Public Class DownloadServiceOrderDocumentRequest

    <DataMember(IsRequired:=True, Name:="CompanyCode", Order:=1), Required()>
    Public Property CompanyCode As String

    <DataMember(IsRequired:=True, Name:="ClaimNumber", Order:=2), Required()>
    Public Property ClaimNumber As String

End Class