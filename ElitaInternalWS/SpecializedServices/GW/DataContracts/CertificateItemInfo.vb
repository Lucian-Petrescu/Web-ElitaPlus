Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/GetCertificate", Name:="CertificateItemInfo")>
    Public Class CertificateItemInfo
        Private Property EquipmentManager As IEquipmentManager
        Private Property CompanyGroupManager As ICompanyGroupManager

        <DataMember(IsRequired:=True, Name:="ItemNumber")>
        Public Property ItemNumber As Nullable(Of Integer)

        <DataMember(IsRequired:=True, Name:="RiskType")>
        Public Property RiskType As String

        <DataMember(IsRequired:=False, Name:="SerialNumber", EmitDefaultValue:=False)>
        Public Property SerialNumber As String

        <DataMember(IsRequired:=False, Name:="Manufacturer", EmitDefaultValue:=False)>
        Public Property Manufacturer As String

        <DataMember(IsRequired:=False, Name:="MaxReplacementCost", EmitDefaultValue:=False)>
        Public Property MaxReplacementCost As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="Model", EmitDefaultValue:=False)>
        Public Property Model As String

        <DataMember(IsRequired:=False, Name:="ItemDescription", EmitDefaultValue:=False)>
        Public Property ItemDescription As String

        <DataMember(IsRequired:=False, Name:="ItemCode", EmitDefaultValue:=False)>
        Public Property ItemCode As String

        <DataMember(IsRequired:=False, Name:="ItemRetailPrice", EmitDefaultValue:=False)>
        Public Property ItemRetailPrice As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="ItemReplaceReturnDate", EmitDefaultValue:=False)>
        Public Property ItemReplaceReturnDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="EquipmentId", EmitDefaultValue:=False)>
        Public Property EquipmentId As String

        <DataMember(IsRequired:=False, Name:="MobileType", EmitDefaultValue:=False)>
        Public Property MobileType As String

        <DataMember(IsRequired:=False, Name:="SimCardNumber", EmitDefaultValue:=False)>
        Public Property SimCardNumber As String

        <DataMember(IsRequired:=False, Name:="SkuNumber", EmitDefaultValue:=False)>
        Public Property SkuNumber As String

        <DataMember(IsRequired:=True, Name:="EffectiveDate")>
        Public Property EffectiveDate As Nullable(Of Date)

        <DataMember(IsRequired:=True, Name:="ExpirationDate")>
        Public Property ExpirationDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="ImeiNumber", EmitDefaultValue:=False)>
        Public Property ImeiNumber As String

        <DataMember(IsRequired:=False, Name:="TypeOfEquipmentCode", EmitDefaultValue:=False)>
        Public Property TypeOfEquipmentCode As String

        <DataMember(IsRequired:=False, Name:="TypeOfEquipmentDesc", EmitDefaultValue:=False)>
        Public Property TypeOfEquipmentDesc As String

        <DataMember(IsRequired:=False, Name:="LastUseDate", EmitDefaultValue:=False)>
        Public Property LastUseDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="FirstUseDate", EmitDefaultValue:=False)>
        Public Property FirstUseDate As Nullable(Of Date)

        <DataMember(Name:="ItemCoverages")>
        Public Property ItemCoverages As IEnumerable(Of CertificateItemCoverageInfo)

        <DataMember(IsRequired:=False, Name:="Endorsements", EmitDefaultValue:=False)>
        Public Property Endorsements As IEnumerable(Of CertificateEndorsementInfo)

        Public Sub New(pCompGroupManager As ICompanyGroupManager)
            CompanyGroupManager = pCompGroupManager
        End Sub

        Public Sub New(pCertificateItem As CertificateItem, pCommonManager As CommonManager, pcompanyGroup As CompanyGroup, pEquipmentManager As EquipmentManager, pProduct As Product, pLanguage As String)
            ' Copy properties from Certificate Item to current instance
            With Me
                .ItemNumber = pCertificateItem.ItemNumber

                .RiskType = If(pLanguage.ToString().ToUpperInvariant() <> "EN", pcompanyGroup.RiskTypes.Where(Function(r) r.RiskTypeId = pCertificateItem.RiskTypeId).FirstOrDefault.Description,
                    pcompanyGroup.RiskTypes.Where(Function(r) r.RiskTypeId = pCertificateItem.RiskTypeId).FirstOrDefault.RiskTypeEnglish)
                .SerialNumber = pCertificateItem.SerialNumber
                '.Manufacturer = pcompanyGroup.Manufacturers.Where(Function(m) m.ManufacturerId = pCertificateItem.ManufacturerId).FirstOrDefault.Description
                .Manufacturer = If(Not pCertificateItem.ManufacturerId Is Nothing,
                                    pcompanyGroup.Manufacturers.Where(Function(m) m.ManufacturerId = pCertificateItem.ManufacturerId).FirstOrDefault.Description, Nothing)
                .MaxReplacementCost = pCertificateItem.MaxReplacementCost
                .Model = pCertificateItem.Model
                .ItemDescription = pCertificateItem.ItemDescription
                .ItemCode = pCertificateItem.ItemCode
                .ItemRetailPrice = pCertificateItem.ItemRetailPrice
                .ItemReplaceReturnDate = pCertificateItem.ItemReplaceReturnDate
                .EquipmentId = If(Not pCertificateItem.EquipmentId Is Nothing, pEquipmentManager.GetEquipment(pCertificateItem.EquipmentId).Description, Nothing)
                .MobileType = pCertificateItem.MobileType
                .SimCardNumber = pCertificateItem.SimCardNumber
                .SkuNumber = pCertificateItem.SkuNumber
                .EffectiveDate = pCertificateItem.EffectiveDate
                .ExpirationDate = pCertificateItem.ExpirationDate
                .ImeiNumber = pCertificateItem.ImeiNumber
                .TypeOfEquipmentCode = If(Not pCertificateItem.EquipmentId Is Nothing,
                                        pEquipmentManager.GetEquipment(pCertificateItem.EquipmentId).EquipmentTypeId.ToCode(pCommonManager, ListCodes.TypeOfEquipment, pLanguage), Nothing)
                'pCertificateItem.TypeOfEquipmentId.ToCode(pCommonManager, ListCodes.EquipmentType, LanguageCodes.USEnglish)
                .TypeOfEquipmentDesc = If(Not pCertificateItem.EquipmentId Is Nothing,
                                        pEquipmentManager.GetEquipment(pCertificateItem.EquipmentId).EquipmentTypeId.ToDescription(pCommonManager, ListCodes.TypeOfEquipment, pLanguage), Nothing)

                .FirstUseDate = pCertificateItem.FirstUseDate
                .LastUseDate = pCertificateItem.LastUseDate
            End With

            ItemCoverages = New List(Of CertificateItemCoverageInfo)

            For Each cic As CertificateItemCoverage In pCertificateItem.ItemCoverages
                DirectCast(ItemCoverages, IList(Of CertificateItemCoverageInfo)).Add(New CertificateItemCoverageInfo(cic, pCommonManager, pProduct, pLanguage))
            Next

            If pCertificateItem.Endorsements.Count > 0 Then
                Endorsements = New List(Of CertificateEndorsementInfo)

                For Each cd As CertificateEndorsement In pCertificateItem.Endorsements
                    DirectCast(Endorsements, IList(Of CertificateEndorsementInfo)).Add(New CertificateEndorsementInfo(cd, pCommonManager, pLanguage))
                Next
            End If

        End Sub
    End Class
End Namespace
