Imports System.Runtime.Serialization
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.BusinessObjectsNew.CertItemCoverage
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

<DataContract(Name:="ItemInfo", Namespace:="http://elita.assurant.com/DataContracts/Certificate")> _
Public Class ItemInfo
    <DataMember(Name:="ItemNumber", IsRequired:=False)> _
    Public Property ItemNumber As Integer

    <DataMember(Name:="Manufacturer", IsRequired:=False)> _
    Public Property Manufacturer As String

    <DataMember(Name:="Model", IsRequired:=False)> _
    Public Property Model As String

    <DataMember(Name:="BeginDate", IsRequired:=False)> _
    Public Property BeginDate As Date

    <DataMember(Name:="EndDate", IsRequired:=False)> _
    Public Property EndDate As Date

    <DataMember(Name:="SerialNumber", IsRequired:=False)>
    Public Property SerialNumber As String

    <DataMember(Name:="SKU", IsRequired:=False)>
    Public Property SKUNumber As String


    <DataMember(Name:="OriginalRetailPrice", IsRequired:=False)> _
    Public Property OriginalRetailPrice As Nullable(Of Decimal)

    <DataMember(Name:="Coverages", IsRequired:=False)> _
    Public Property Coverages As IEnumerable(Of CoverageInfo)

    Public Sub New()

    End Sub

    Friend Sub New(pCertItem As CertItem)

        ItemNumber = pCertItem.ItemNumber
        If (pCertItem.ManufacturerId <> Guid.Empty) Then
            Dim oManufacturerDv As DataView = LookupListNew.GetManufacturerLookupList(pCertItem.Cert.Company.CompanyGroupId)
            Manufacturer = LookupListNew.GetDescriptionFromId(oManufacturerDv, pCertItem.ManufacturerId)
        End If
        Model = pCertItem.Model
        BeginDate = pCertItem.EffectiveDate
        EndDate = pCertItem.ExpirationDate
        SerialNumber = pCertItem.SerialNumber
        SKUNumber = pCertItem.SkuNumber
        If pCertItem.OriginalRetailPrice IsNot Nothing Then
            OriginalRetailPrice = pCertItem.OriginalRetailPrice
        Else
            OriginalRetailPrice = Nothing
        End If
    End Sub


End Class