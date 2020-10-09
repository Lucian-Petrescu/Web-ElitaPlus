Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificate", Name:="CertificateItem")>
    Public Class SearchResultCertificateItemInfo


        <DataMember(Name:="ItemNumber")>
        Public Property ItemNumber As Nullable(Of Integer)

        <DataMember(Name:="RiskType")>
        Public Property RiskType As String

        <DataMember(IsRequired:=True, Name:="Manufacturer")>
        Public Property Manufacturer As String

        <DataMember(IsRequired:=True, Name:="SerialNumber")>
        Public Property SerialNumber As String

        <DataMember(IsRequired:=True, Name:="Model")>
        Public Property Model As String

        <DataMember(IsRequired:=True, Name:="ItemDescription")>
        Public Property ItemDescription As String

        <DataMember(IsRequired:=True, Name:="ItemCode")>
        Public Property ItemCode As String

        <DataMember(IsRequired:=True, Name:="ItemRetailPrice")>
        Public Property ItemRetailPrice As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="MobileType")>
        Public Property MobileType As String

        <DataMember(IsRequired:=True, Name:="SkuNumber")>
        Public Property SkuNumber As String

        <DataMember(IsRequired:=True, Name:="EffectiveDate")>
        Public Property EffectiveDate As Nullable(Of Date)

        <DataMember(IsRequired:=True, Name:="ExpirationDate")>
        Public Property ExpirationDate As Nullable(Of Date)

        <DataMember(IsRequired:=True, Name:="ImeiNumber")>
        Public Property ImeiNumber As String

        <DataMember(IsRequired:=False, Name:="TypeOfEquipmentCode")>
        Public Property TypeOfEquipmentCode As String

        <DataMember(IsRequired:=False, Name:="TypeOfEquipmentDesc")>
        Public Property TypeOfEquipmentDesc As String

        <DataMember(Name:="ItemCoverages")>
        Public Property ItemCoverages As IEnumerable(Of SearchResultCertificateItemCoverageInfo)


        Public Sub New(pCertificateItem As CertificateItem, pCommonManager As CommonManager, pcompanyGroup As CompanyGroup, pEquipmentManager As EquipmentManager, pLanguage As String)
            ' Copy properties from Certificate Item to current instance
            With Me
                .ItemNumber = pCertificateItem.ItemNumber

                Dim rt As RiskType = pcompanyGroup.RiskTypes.Where(Function(r) r.RiskTypeId = pCertificateItem.RiskTypeId).FirstOrDefault
                If Not rt Is Nothing Then
                    .RiskType = If(pLanguage.ToString().ToUpperInvariant() <> "EN", rt.Description, rt.RiskTypeEnglish)
                End If

                .SerialNumber = pCertificateItem.SerialNumber
                '.Manufacturer = pcompanyGroup.Manufacturers.Where(Function(m) m.ManufacturerId = pCertificateItem.ManufacturerId).FirstOrDefault.Description
                .Manufacturer = If(Not pCertificateItem.ManufacturerId Is Nothing,
                                    pcompanyGroup.Manufacturers.Where(Function(m) m.ManufacturerId = pCertificateItem.ManufacturerId).FirstOrDefault.Description, String.Empty)
                .Model = pCertificateItem.Model
                .ItemDescription = pCertificateItem.ItemDescription
                .ItemCode = pCertificateItem.ItemCode
                .ItemRetailPrice = pCertificateItem.ItemRetailPrice
                .MobileType = pCertificateItem.MobileType
                .SkuNumber = pCertificateItem.SkuNumber
                .EffectiveDate = pCertificateItem.EffectiveDate
                .ExpirationDate = pCertificateItem.ExpirationDate
                .ImeiNumber = pCertificateItem.ImeiNumber
                .TypeOfEquipmentCode = If(Not pCertificateItem.EquipmentId Is Nothing,
                                        pEquipmentManager.GetEquipment(pCertificateItem.EquipmentId).EquipmentTypeId.ToCode(pCommonManager, ListCodes.TypeOfEquipment, pLanguage), String.Empty)
                'pCertificateItem.TypeOfEquipmentId.ToCode(pCommonManager, ListCodes.EquipmentType, LanguageCodes.USEnglish)
                .TypeOfEquipmentDesc = If(Not pCertificateItem.EquipmentId Is Nothing,
                                        pEquipmentManager.GetEquipment(pCertificateItem.EquipmentId).EquipmentTypeId.ToDescription(pCommonManager, ListCodes.TypeOfEquipment, pLanguage), String.Empty)

            End With

            ItemCoverages = New List(Of SearchResultCertificateItemCoverageInfo)

            For Each cic As CertificateItemCoverage In pCertificateItem.ItemCoverages
                DirectCast(ItemCoverages, IList(Of SearchResultCertificateItemCoverageInfo)).Add(New SearchResultCertificateItemCoverageInfo(cic, pCommonManager, pLanguage))
            Next

        End Sub
    End Class
End Namespace
