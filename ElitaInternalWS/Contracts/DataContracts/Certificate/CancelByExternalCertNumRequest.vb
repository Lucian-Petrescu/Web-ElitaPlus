
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.ComponentModel.DataAnnotations

<DataContract(Name:="ExternalCertNumType", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Enum ExternalCertNumType
    <EnumMember()>
    ServiceLineNumber

    <EnumMember()>
    TaxId
End Enum

<DataContract(Name:="CancelByExternalCertNumRequest", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class CancelByExternalCertNumRequest

    <DataMember(IsRequired:=True), StringLength(5, ErrorMessage:="DealerCode value exceeded maximum length of 5")>
    Public Property CompanyCode As String

    <DataMember(IsRequired:=False), StringLength(5, ErrorMessage:="DealerCode value exceeded maximum length of 5")>
    Public Property DealerCode As String

    <DataMember(IsRequired:=True)>
    Public Property ExternalCertNumType As ExternalCertNumType

    <DataMember(IsRequired:=True)>
    Public Property ExternalCertNum As String

    <DataMember(IsRequired:=True)>
    Public Property CancellationDate As DateTime

    <DataMember(IsRequired:=False), StringLength(5, ErrorMessage:="CancellationReasonCode value exceeded maximum length of 5")>
    Public Property CancellationReasonCode As String

    <DataMember(IsRequired:=False)>
    Public Property CallerName As String

End Class