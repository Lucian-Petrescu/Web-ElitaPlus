Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Imports Assurant.ElitaPlus.BusinessObjectsNew.CertItem
Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew

<DataContract(Name:="CertificateSerialTaxLookup", Namespace:="http://elita.assurant.com/DataContracts/Certificate")> _
Public Class CertificateSerialTaxLookup
    Inherits CertificateNumberLookup


    <DataMember(Name:="SerialNumber", IsRequired:=True), StringLength(30, ErrorMessage:="SerialNumber value exceeded maximum length of 30"),
    Required(ErrorMessage:="SerialNumber is Required")> _
    Public Property SerialNumber As String


    <DataMember(Name:="IdentificationNumber", IsRequired:=True), StringLength(20, ErrorMessage:="IdentificationNumber value exceeded maximum length of 20"),
    Required(ErrorMessage:="IdentificationNumber is Required")> _
    Public Property IdentificationNumber As String

    Friend Overrides Sub Validate(pCert As Certificate)
        MyBase.Validate(pCert)
        Dim certItemDataView As CertItemSearchDV = pCert.CertItems
        If certItemDataView.IsSerialNumberExist(SerialNumber) = False Then
            Throw New FaultException(Of InvalidSerialnumber)(New InvalidSerialnumber() With {.CertificateSearch = Me}, "Invalid Serial Number")
        End If

        If Not String.IsNullOrEmpty(IdentificationNumber) Then
            If pCert.IdentificationNumber <> IdentificationNumber Then
                Throw New FaultException(Of InvalidIdentificationNumber)(New InvalidIdentificationNumber() With {.CertificateSearch = Me}, "Invalid Identification Number")
            End If
        End If
    End Sub
    Friend Overrides Sub ValidateIdentificatonNumer(pCert As Certificate)
        MyBase.ValidateIdentificatonNumer(pCert)
        If Not String.IsNullOrEmpty(IdentificationNumber) Then
            If pCert.IdentificationNumber <> IdentificationNumber Then
                Throw New FaultException(Of InvalidIdentificationNumber)(New InvalidIdentificationNumber() With {.CertificateSearch = Me}, "Invalid Identification Number")
            End If
        End If
    End Sub

End Class
