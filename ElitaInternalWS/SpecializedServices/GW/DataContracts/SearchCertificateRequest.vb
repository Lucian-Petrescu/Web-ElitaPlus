Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificate", Name:="SearchCertificateRequest")>
    Public Class SearchCertificateRequest
        <DataMember(IsRequired:=True, Name:="CompanyCodes"), Required()>
        Public Property CompanyCodes As IEnumerable(Of String)

        <DataMember(IsRequired:=False, Name:="Culture")>
        Public Property Culture As String

        <DataMember(IsRequired:=False, Name:="CustomerName"), StringLength(50, MinimumLength:=0)>
        Public Property CustomerName As String

        <DataMember(IsRequired:=False, Name:="CertificateNumber"), StringLength(20, MinimumLength:=0)>
        Public Property CertificateNumber As String

        <DataMember(IsRequired:=False, Name:="WorkPhone"), StringLength(15, MinimumLength:=0)>
        Public Property WorkPhone As String

        <DataMember(IsRequired:=False, Name:="HomePhone"), StringLength(15, MinimumLength:=0)>
        Public Property HomePhone As String

        <DataMember(IsRequired:=False, Name:="AccountNumber"), StringLength(30, MinimumLength:=0)>
        Public Property AccountNumber As String

        <DataMember(IsRequired:=False, Name:="ServiceLineNumber"), StringLength(25, MinimumLength:=0)>
        Public Property ServiceLineNumber As String

        <DataMember(IsRequired:=False, Name:="TaxId"), StringLength(20, MinimumLength:=0)>
        Public Property TaxId As String

        <DataMember(IsRequired:=False, Name:="SerialNumber"), StringLength(30, MinimumLength:=0)>
        Public Property SerialNumber As String

        <DataMember(IsRequired:=False, Name:="IMEINumber"), StringLength(30, MinimumLength:=0)>
        Public Property IMEINumber As String

        <DataMember(IsRequired:=False, Name:="Email"), StringLength(50, MinimumLength:=0)>
        Public Property Email As String

        <DataMember(IsRequired:=False, Name:="Address"), StringLength(50, MinimumLength:=0)>
        Public Property Address As String

        <DataMember(IsRequired:=False, Name:="Address2"), StringLength(50, MinimumLength:=0)>
        Public Property Address2 As String

        <DataMember(IsRequired:=False, Name:="Address3"), StringLength(50, MinimumLength:=0)>
        Public Property Address3 As String

        <DataMember(IsRequired:=False, Name:="Country"), StringLength(40, MinimumLength:=0)>
        Public Property Country As String

        <DataMember(IsRequired:=False, Name:="State"), StringLength(30, MinimumLength:=0)>
        Public Property State As String

        <DataMember(IsRequired:=False, Name:="City"), StringLength(50, MinimumLength:=0)>
        Public Property City As String

        <DataMember(IsRequired:=False, Name:="ZipCode"), StringLength(25, MinimumLength:=0)>
        Public Property ZipCode As String

        <DataMember(IsRequired:=False, Name:="PurchaseInvoiceNumber"), StringLength(30, MinimumLength:=0)>
        Public Property PurchaseInvoiceNumber As String

        <DataMember(IsRequired:=False, Name:="NumberOfReocrds")>
        Public Property NumberOfRecords As Integer?

        <DataMember(IsRequired:=False, Name:="CertificateStatus"), StringLength(10, MinimumLength:=0)>
        Public Property CertStatus As String

    End Class
End Namespace