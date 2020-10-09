Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports BO = Assurant.ElitaPlus.BusinessObjectsNew

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificate", Name:="Certificate")>
    Public Class SearchResultCertificateInfo
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

        <DataMember(IsRequired:=False, Name:="SourceSytem")>
        Public Property SourceSystem As String

        <DataMember(IsRequired:=True, Name:="Status")>
        Public Property Status As String

        <DataMember(IsRequired:=True, Name:="ProductSalesDate")>
        Public Property ProductSalesDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="InvoiceNumber")>
        Public Property InvoiceNumber As String

        <DataMember(IsRequired:=True, Name:="SalesPrice")>
        Public Property SalesPrice As String

        <DataMember(IsRequired:=True, Name:="SubscriberStatus")>
        Public Property SubscriberStatus As String

        <DataMember(IsRequired:=True, Name:="ServiceLineNumber")>
        Public Property ServiceLineNumber As String

        <DataMember(IsRequired:=False, Name:="SuspendedReasonCode")>
        Public Property SuspendedReasonCode As String

        <DataMember(IsRequired:=False, Name:="SuspendedReasonDesc")>
        Public Property SuspendedReasonDesc As String

        <DataMember(IsRequired:=True, Name:="Salutation")>
        Public Property Salutation As String

        <DataMember(IsRequired:=True, Name:="CustomerFullName")>
        Public Property CustomerFullName As String

        <DataMember(IsRequired:=True, Name:="CustomerAddressStreet1")>
        Public Property CustomerAddressStreet1 As String

        <DataMember(IsRequired:=False, Name:="CustomerAddressStreet2")>
        Public Property CustomerAddressStreet2 As String

        <DataMember(IsRequired:=True, Name:="CustomerAddressCity")>
        Public Property CustomerAddressCity As String

        <DataMember(IsRequired:=True, Name:="CustomerAddressStateCode")>
        Public Property CustomerAddressStateCode As String

        <DataMember(IsRequired:=True, Name:="CustomerAddressCountryCode")>
        Public Property CustomerAddressCountryCode As String

        <DataMember(IsRequired:=True, Name:="CustomerAddressPostalCode1")>
        Public Property CustomerAddressPostalCode1 As String

        <DataMember(IsRequired:=True, Name:="CustomerHomePhoneNumber")>
        Public Property CustomerHomePhoneNumber As String

        <DataMember(IsRequired:=False, Name:="CustomerWorkPhoneNumber")>
        Public Property CustomerWorkPhoneNumber As String

        <DataMember(IsRequired:=True, Name:="CustomerEmailAddress")>
        Public Property CustomerEmailAddress As String

        <DataMember(IsRequired:=True, Name:="CustomerIdentificationNumber")>
        Public Property CustomerIdentificationNumber As String

        <DataMember(IsRequired:=False, Name:="CustomerNumber")>
        Public Property MembershipNumber As String

        <DataMember(IsRequired:=False, Name:="PrimaryMemberName")>
        Public Property PrimaryMemberName As String

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
        Public Property Items As IEnumerable(Of SearchResultCertificateItemInfo)


        Public Sub New(pCertificate As Certificate,
                       ByRef pCertManager As ICertificateManager,
                       ByRef pCompanyGroupManager As ICompanyGroupManager,
                       ByRef pcompanyManager As ICompanyManager,
                       ByRef pDealerManager As IDealerManager,
                       ByRef pCountryManager As CountryManager,
                       ByRef pAddressManager As AddressManager,
                       ByRef pCommonManager As CommonManager,
                       ByRef pEquipmentManager As EquipmentManager,
                       pLanguage As String)
            ' Copy properties from Certificate to current instance
            Dim dealer As Dealer = pDealerManager.GetDealerForGwPil(pCertificate.DealerId)
            Dim company As Company = pcompanyManager.GetCompany(dealer.CompanyId)
            Dim companyGroup As CompanyGroup = pCompanyGroupManager.GetCompanyGroup(company.CompanyGroupId)
            With Me
                .DealerName = dealer.DealerName
                .DealerCode = dealer.DealerCode
                .DealerType = dealer.DealerTypeId.ToDescription(pCommonManager, ListCodes.DealerType, LanguageCodes.USEnglish)
                .CompanyCode = company.Code
                .CompanyDescription = company.Description

                .CertificateNumber = pCertificate.CertificateNumber
                .Status = pCertificate.StatusCode.ToDescription(pCommonManager, ListCodes.CertificateStatus, pLanguage)
                .InvoiceNumber = pCertificate.InvoiceNumber
                .ProductSalesDate = pCertificate.ProductSalesDate
                .SalesPrice = pCertificate.SalesPrice
                .SubscriberStatus = pCertificate.SubscriberStatus.ToDescription(pCommonManager, ListCodes.SubscriberStatus, pLanguage)
                .ServiceLineNumber = pCertificate.ServiceLineNumber
                .Salutation = pCertificate.SalutationId.ToDescription(pCommonManager, ListCodes.Salutation, pLanguage)
                .CustomerFullName = pCertificate.CustomerName
                .CustomerEmailAddress = pCertificate.Email
                .MembershipNumber = pCertificate.MembershipNumber
                .CustomerIdentificationNumber = pCertificate.IdentificationNumber
                .PrimaryMemberName = pCertificate.PrimaryMemberName
                .MembershipNumber = pCertificate.MembershipNumber
                .SourceSystem = "ElitaCL"
                Dim pAddress As Address, pCountry As Country

                If pCertificate.AddressId.HasValue Then
                    pAddress = pAddressManager.GetAddress(pCertificate.AddressId)
                    pCountry = pCountryManager.GetCountry(pAddress.CountryId)

                    If Not pAddress Is Nothing Then
                        .CustomerAddressStreet1 = pAddress.Address1
                        .CustomerAddressStreet2 = pAddress.Address2
                        .CustomerAddressCity = pAddress.City
                        .CustomerAddressStateCode = If(Not pAddress.RegionId Is Nothing,
                                            pCountry.Regions.Where(Function(r) r.RegionId = pAddress.RegionId.GetValueOrDefault).FirstOrDefault.Description, String.Empty)
                        .CustomerAddressPostalCode1 = pAddress.PostalCode
                    End If
                End If


                .CustomerAddressCountryCode = pCountry.Code
                .CustomerHomePhoneNumber = pCertificate.HomePhone
                .CustomerWorkPhoneNumber = pCertificate.WorkPhone


                .SuspendedReasonCode = If(Not pCertificate.SuspendedReasonId Is Nothing, dealer.SuspendedReasons.Where(Function(s) s.SuspendedReasonId = pCertificate.SuspendedReasonId).FirstOrDefault.Code, String.Empty)
                .SuspendedReasonDesc = If(Not pCertificate.SuspendedReasonId Is Nothing, dealer.SuspendedReasons.Where(Function(s) s.SuspendedReasonId = pCertificate.SuspendedReasonId).FirstOrDefault.Description, String.Empty)

                If Not pCertificate.PreviousCertId Is Nothing Then
                    .LastPolicyNumber = pCertManager.GetCertNumber(pCertificate.PreviousCertId)
                End If

                If Not pCertificate.OriginalCertId Is Nothing Then
                    .OriginalPolicyNumber = pCertManager.GetCertNumber(pCertificate.OriginalCertId)
                End If

                Dim dEffectiveDate As String, dExpirationDate As String
                pCertManager.GetFirstCertEndorseDates(pCertificate.CertificateId, dEffectiveDate, dExpirationDate)
                If dEffectiveDate <> String.Empty Then
                    .EndorsementEffectiveDate = CDate(dEffectiveDate)
                End If
                If dExpirationDate <> String.Empty Then
                    .EndorsementExpirationDate = CDate(dExpirationDate)
                End If
            End With

            Dim certContract As Contract = dealer.Contracts.Where(Function(c) pCertificate.WarrantySalesDate >= c.Effective AndAlso pCertificate.WarrantySalesDate <= c.Expiration).FirstOrDefault

            Dim producerAddress As Address
            Dim contractProducer As BO.Producer

            If Not certContract.ProducerId Is Nothing Then
                contractProducer = New BO.Producer(certContract.ProducerId)
                If Not contractProducer.AddressId.Equals(Guid.Empty) Then
                    producerAddress = pAddressManager.GetAddress(contractProducer.AddressId)
                End If
            End If
            Contract = New ContractInfo(certContract, pCommonManager, pLanguage, pCountryManager, contractProducer, producerAddress)

            Dim dGWP As Decimal, dSalesTax As Decimal
            pCertManager.GetCertificateCoverageRate(pCertificate.CertificateId, Today.Date, dGWP, dSalesTax)
            CoverageRate = dGWP
            CoverageRateNetOfTax = dGWP - dSalesTax
            SalesTax = dSalesTax

            Items = New List(Of SearchResultCertificateItemInfo)()

            'Dim pProduct As Product = pDealerManager.GetProduct(pDealer.DealerCode, pCertificate.ProductCode)

            For Each ci As CertificateItem In pCertificate.Item
                DirectCast(Items, IList(Of SearchResultCertificateItemInfo)).Add(New SearchResultCertificateItemInfo(ci, pCommonManager, companyGroup, pEquipmentManager, pLanguage))
            Next

        End Sub



    End Class
End Namespace
