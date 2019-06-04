Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Imports System.Object
Imports Assurant.ElitaPlus.BusinessObjectsNew.CertItem
Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew

<DataContract(Name:="CertificateAccountNumberLookup", Namespace:="http://elita.assurant.com/DataContracts/Certificate")> _
Public Class CertificateAccountNumberLookup
    Inherits CertificateLookup

    <DataMember(Name:="BillingAccountNumber", IsRequired:=True), StringLength(50, ErrorMessage:="BillingAccountNumber value exceeded maximum length of 50"),
    Required(ErrorMessage:="BillingAccountNumber is Required")> _
    Public Property BillingAccountNumber As String

    <DataMember(Name:="SerialNumber", IsRequired:=True), StringLength(30, ErrorMessage:="SerialNumber value exceeded maximum length of 30"),
    Required(ErrorMessage:="SerialNumber is Required")> _
    Public Property SerialNumber As String

    <DataMember(Name:="DealerCode", IsRequired:=True), StringLength(5, ErrorMessage:="DealerCode value exceeded maximum length of 5"),
    Required(ErrorMessage:="DealerCode is Required")> _
    Public Property DealerCode As String

    Friend Overrides Sub Validate(pCert As Certificate)
        MyBase.Validate(pCert)

        Dim certItemDataView As CertItemSearchDV = pCert.CertItems
        If certItemDataView.IsSerialNumberExist(SerialNumber) = False Then

            Throw New FaultException(Of InvalidSerialnumber)(New InvalidSerialnumber() With {.CertificateSearch = Me}, "Invalid Serial Number")
        End If



    End Sub
End Class
