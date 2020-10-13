Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports BO = Assurant.ElitaPlus.BusinessObjectsNew

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificateByTaxId", Name:="Certificate")>
    Public Class SearchByTaxIdResultCertificateInfo
        <DataMember(IsRequired:=True, Name:="DealerName")>
        Public Property DealerName As String

        <DataMember(IsRequired:=True, Name:="DealerNumber")>
        Public Property DealerCode As String

        <DataMember(IsRequired:=True, Name:="BusinessLine")>
        Public Property DealerType As String

        <DataMember(IsRequired:=True, Name:="CompanyNumber")>
        Public Property CompanyCode As String

        <DataMember(IsRequired:=True, Name:="CompanyName")>
        Public Property CompanyDescription As String

        <DataMember(IsRequired:=True, Name:="CertificateNumber")>
        Public Property CertificateNumber As String

        <DataMember(IsRequired:=True, Name:="CertificateStatus")>
        Public Property CertificateStatus As String

        <DataMember(IsRequired:=False, Name:="BranchCode", EmitDefaultValue:=False)>
        Public Property BranchCode As String

        <DataMember(IsRequired:=False, Name:="BranchName", EmitDefaultValue:=False)>
        Public Property BranchName As String

        <DataMember(IsRequired:=False, Name:="ContractPolicyNumber", EmitDefaultValue:=False)>
        Public Property ContractPolicyNumber As String

        <DataMember(IsRequired:=False, Name:="HomePhone", EmitDefaultValue:=False)>
        Public Property HomePhone As String

        <DataMember(IsRequired:=False, Name:="WorkPhone", EmitDefaultValue:=False)>
        Public Property WorkPhone As String

        <DataMember(IsRequired:=False, Name:="InsuranceActivationDate", EmitDefaultValue:=False)>
        Public Property InsuranceActivationDate As Date

        <DataMember(IsRequired:=False, Name:="MaxCoverageEndDate", EmitDefaultValue:=False)>
        Public Property MaxCoverageEndDate As Date

        <DataMember(IsRequired:=False, Name:="CustomerName", EmitDefaultValue:=False)>
        Public Property CustomerName As String

        <DataMember(IsRequired:=False, Name:="Endorsement_0_EffectiveDate")>
        Public Property EndorsementEffectiveDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="Endorsement_0_ExpirationDate")>
        Public Property EndorsementExpirationDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="LastPolicyNumber")>
        Public Property LastPolicyNumber As String

        <DataMember(IsRequired:=False, Name:="OriginalPolicyNumber")>
        Public Property OriginalPolicyNumber As String

        <DataMember(IsRequired:=False, Name:="SalesTax", EmitDefaultValue:=False)>
        Public Property SalesTax As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="CoverageRate", EmitDefaultValue:=False)>
        Public Property CoverageRate As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="CoverageRateNetOfTax", EmitDefaultValue:=False)>
        Public Property CoverageRateNetOfTax As Nullable(Of Decimal)

        <DataMember(IsRequired:=True, Name:="Contract")>
        Public Property Contract As ContractInfo

        <DataMember(Name:="Items")>
        Public Property Items As IEnumerable(Of SearchByTaxIdResultCertItem)


        Public Sub New(pCertResult As DBSearchResultCertRecord,
                       ByRef pCertManager As ICertificateManager,
                       ByRef pCompanyGroupManager As ICompanyGroupManager,
                       ByRef pCommonManager As CommonManager,
                       ByRef pdealerManager As DealerManager,
                       ByRef paddressManager As AddressManager,
                       ByRef pcountryManager As CountryManager,
                       pLanguage As String)
            ' Copy properties from Certificate to current instance
            Dim companyGroup As CompanyGroup = pCompanyGroupManager.GetCompanyGroup(pCertResult.CompanyGroupId)
            Dim wcertNumber As String = pCertManager.GetCertNumber(pCertResult.CertId)
            Dim wCert As Certificate = pCertManager.GetCertificateForGwPil(pCertResult.DealerCode, wcertNumber) 'pCertManager.GetCertificate(pCertResult.CertId)
            Dim dealer As Dealer
            dealer = pdealerManager.GetDealerForGWPIL(pCertResult.DealerCode)

            With Me
                .DealerName = pCertResult.DealerName
                .DealerCode = pCertResult.DealerCode
                .DealerType = pCertResult.DealerTypeId.ToDescription(pCommonManager, ListCodes.DealerType, LanguageCodes.USEnglish)
                .CompanyCode = pCertResult.CompanyCode
                .CompanyDescription = pCertResult.CompanyDesc

                .CertificateNumber = wCert.CertificateNumber
                .CustomerName = wCert.CustomerName
                .CertificateStatus = pCertResult.StatusCode.ToDescription(pCommonManager, ListCodes.CertificateStatus, pLanguage)
                .BranchCode = wCert.DealerBranchCode
                .BranchName = pCertResult.BranchName
                .ContractPolicyNumber = pCertResult.ContractPolicyNum
                .HomePhone = wCert.HomePhone
                .InsuranceActivationDate = IIf(wCert.InsuranceActivationDate.HasValue, wCert.InsuranceActivationDate, wCert.WarrantySalesDate)
                .MaxCoverageEndDate = pCertResult.MaxCoverageEndDate
                .WorkPhone = wCert.WorkPhone
                If wCert.PreviousCertId IsNot Nothing Then
                    .LastPolicyNumber = pCertManager.GetCertNumber(wCert.PreviousCertId)
                End If

                If wCert.OriginalCertId IsNot Nothing Then
                    .OriginalPolicyNumber = pCertManager.GetCertNumber(wCert.OriginalCertId)
                End If

                Dim dEffectiveDate As String, dExpirationDate As String
                pCertManager.GetFirstCertEndorseDates(wCert.CertificateId, dEffectiveDate, dExpirationDate)
                If dEffectiveDate <> String.Empty Then
                    .EndorsementEffectiveDate = CDate(dEffectiveDate)
                End If
                If dExpirationDate <> String.Empty Then
                    .EndorsementExpirationDate = CDate(dExpirationDate)
                End If
            End With

            Dim certContract As Contract = dealer.Contracts.Where(Function(c) wCert.WarrantySalesDate >= c.Effective AndAlso wCert.WarrantySalesDate <= c.Expiration).FirstOrDefault
            Dim producerAddress As Address
            Dim contractProducer As BO.Producer

            If certContract.ProducerId IsNot Nothing Then
                contractProducer = New BO.Producer(certContract.ProducerId)
                If Not contractProducer.AddressId.Equals(Guid.Empty) Then
                    producerAddress = paddressManager.GetAddress(contractProducer.AddressId)
                End If
            End If

            Contract = New ContractInfo(certContract, pCommonManager, pLanguage, pcountryManager, contractProducer, producerAddress)
            Dim dGWP As Decimal, dSalesTax As Decimal
            pCertManager.GetCertificateCoverageRate(wCert.CertificateId, Today.Date, dGWP, dSalesTax)
            CoverageRate = dGWP
            CoverageRateNetOfTax = dGWP - dSalesTax
            SalesTax = dSalesTax

            Items = New List(Of SearchByTaxIdResultCertItem)()

            For Each ci As CertificateItem In wCert.Item
                DirectCast(Items, IList(Of SearchByTaxIdResultCertItem)).Add(New SearchByTaxIdResultCertItem(ci, pCommonManager, companyGroup, pLanguage))
            Next

        End Sub
    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificateByTaxId", Name:="CertificateItem")>
    Public Class SearchByTaxIdResultCertItem
        <DataMember(IsRequired:=True, Name:="ItemNumber")>
        Public Property ItemNumber As Nullable(Of Integer)

        <DataMember(IsRequired:=True, Name:="RiskType")>
        Public Property RiskType As String

        <DataMember(IsRequired:=True, Name:="ItemDescription")>
        Public Property ItemDescription As String

        <DataMember(IsRequired:=False, Name:="Manufacturer", EmitDefaultValue:=False)>
        Public Property Manufacturer As String

        <DataMember(IsRequired:=False, Name:="Model", EmitDefaultValue:=False)>
        Public Property Model As String

        <DataMember(IsRequired:=True, Name:="EffectiveDate")>
        Public Property EffectiveDate As Nullable(Of Date)

        <DataMember(IsRequired:=True, Name:="ExpirationDate")>
        Public Property ExpirationDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="Endorsements", EmitDefaultValue:=False)>
        Public Property Endorsements As IEnumerable(Of CertificateEndorsementInfo)

        <DataMember(Name:="Coverages")>
        Public Property Coverages As IEnumerable(Of SearchByTaxIdResultCertItemCoverage)

        Public Sub New(pCertificateItem As CertificateItem, pCommonManager As CommonManager, pcompanyGroup As CompanyGroup, pLanguage As String)
            ' Copy properties from Certificate Item to current instance
            With Me
                .ItemNumber = pCertificateItem.ItemNumber
                .ItemDescription = pCertificateItem.ItemDescription
                .RiskType = If(pLanguage.ToString().ToUpperInvariant() <> "EN", pcompanyGroup.RiskTypes.Where(Function(r) r.RiskTypeId = pCertificateItem.RiskTypeId).FirstOrDefault.Description,
                    pcompanyGroup.RiskTypes.Where(Function(r) r.RiskTypeId = pCertificateItem.RiskTypeId).FirstOrDefault.RiskTypeEnglish)
                .Manufacturer = If(pCertificateItem.ManufacturerId IsNot Nothing,
                                    pcompanyGroup.Manufacturers.Where(Function(m) m.ManufacturerId = pCertificateItem.ManufacturerId).FirstOrDefault.Description, Nothing)
                .Model = pCertificateItem.Model
                .EffectiveDate = pCertificateItem.EffectiveDate
                .ExpirationDate = pCertificateItem.ExpirationDate
            End With

            Coverages = New List(Of SearchByTaxIdResultCertItemCoverage)

            For Each cic As CertificateItemCoverage In pCertificateItem.ItemCoverages
                DirectCast(Coverages, IList(Of SearchByTaxIdResultCertItemCoverage)).Add(New SearchByTaxIdResultCertItemCoverage(cic, pCommonManager, pLanguage))
            Next

        End Sub
    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificateByTaxId", Name:="Coverage")>
    Public Class SearchByTaxIdResultCertItemCoverage
        <DataMember(IsRequired:=True, Name:="CoverageName")>
        Public Property CoverageName As String

        <DataMember(IsRequired:=True, Name:="EffectiveStartingOn")>
        Public Property BeginDate As Date

        <DataMember(IsRequired:=True, Name:="EffectiveEndingOn")>
        Public Property EndDate As Date

        <DataMember(IsRequired:=True, Name:="CoverageTypeCode")>
        Public Property CoverageTypeCode As String

        Public Sub New(pCertficateItemCoverage As CertificateItemCoverage,
                       pCommonManager As CommonManager,
                       pLangauge As String)
            With Me
                .CoverageName = pCertficateItemCoverage.CoverageTypeId.ToDescription(pCommonManager, ListCodes.CoverageType, pLangauge)
                .CoverageTypeCode = pCertficateItemCoverage.CoverageTypeId.ToCode(pCommonManager, ListCodes.CoverageType, pLangauge)
                .BeginDate = pCertficateItemCoverage.BeginDate
                .EndDate = pCertficateItemCoverage.EndDate
            End With
        End Sub
    End Class
End Namespace
