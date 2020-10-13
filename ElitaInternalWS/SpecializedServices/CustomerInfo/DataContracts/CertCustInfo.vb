Imports System.Runtime.Serialization
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.BusinessObjectsNew

<DataContract(Name:="CertCustInfo", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class CertCustInfo
    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="CustomerName", Order:=1)>
    Public Property CustomerName As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="CustomerTaxId", Order:=2)>
    Public Property CustomerTaxId As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="DealerProductCode", Order:=3)>
    Public Property DealerProductCode As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="ElitaProductCode", Order:=4)>
    Public Property ElitaProductCode As String

    <DataMember(EmitDefaultValue:=True, IsRequired:=False, Name:="WarrantySalesDate", Order:=5)>
    Public Property WarrantySalesDate As Date

    Public Sub New(pCertificate As Certificate)
        If pCertificate.CustomerName IsNot Nothing Then
            CustomerName = pCertificate.CustomerName
        End If
        If pCertificate.IdentificationNumber IsNot Nothing Then
            CustomerTaxId = pCertificate.IdentificationNumber
        End If
        If pCertificate.DealerProductCode IsNot Nothing Then
            DealerProductCode = pCertificate.DealerProductCode
        End If
        If pCertificate.ProductCode IsNot Nothing Then
            ElitaProductCode = pCertificate.ProductCode
        End If
        If pCertificate.WarrantySalesDate IsNot Nothing Then
            WarrantySalesDate = pCertificate.WarrantySalesDate
        End If
    End Sub

End Class
