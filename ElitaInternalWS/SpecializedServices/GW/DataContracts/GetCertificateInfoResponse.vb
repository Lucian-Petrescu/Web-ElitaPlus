Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities


Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/GetCertificate", Name:="GetCertificateInfoResponse")>
    Public Class GetCertificateInfoResponse

        Private Property m_ClaimRepository As IClaimRepository(Of Claim)

        <DataMember(IsRequired:=True, Name:="CertificateNumber")>
        Public Property CertificateNumber As String

        <DataMember(IsRequired:=True, Name:="PlanCode")>
        Public Property ProductCode As String

        <DataMember(IsRequired:=False, Name:="PlanName", EmitDefaultValue:=False)>
        Public Property ProductDescription As String

        <DataMember(IsRequired:=True, Name:="Status")>
        Public Property Status As String

        <DataMember(IsRequired:=False, Name:="CancellationDate", EmitDefaultValue:=False)>
        Public Property CancellationDate As Nullable(Of Date)

        <DataMember(IsRequired:=True, Name:="AgreementDate")>
        Public Property WarrantySalesDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="ClaimsPaidAmount", EmitDefaultValue:=False)>
        Public Property ClaimsPaidAmount As Nullable(Of Decimal)

        <DataMember(IsRequired:=True, Name:="CurrencyCode")>
        Public Property CurrencyCode As String

        <DataMember(IsRequired:=False, Name:="PaidThroughDate", EmitDefaultValue:=False)>
        Public Property DatePaidFor As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="OriginalContractNumber", EmitDefaultValue:=False)>
        Public Property LinkedCertNumber As String

        <DataMember(IsRequired:=False, Name:="PaymentTypeCode", EmitDefaultValue:=False)>
        Public Property PaymentTypeCode As String

        <DataMember(IsRequired:=False, Name:="PaymentTypeDesc", EmitDefaultValue:=False)>
        Public Property PaymentTypeDesc As String

        <DataMember(IsRequired:=False, Name:="FinanceCurrencyCode", EmitDefaultValue:=False)>
        Public Property FinanceCurrencyCode As String

        <DataMember(IsRequired:=True, Name:="ProductSalesDate")>
        Public Property ProductSalesDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="InvoiceNumber", EmitDefaultValue:=False)>
        Public Property InvoiceNumber As String

        <DataMember(IsRequired:=False, Name:="DealerBranchCode", EmitDefaultValue:=False)>
        Public Property DealerBranchCode As String

        <DataMember(IsRequired:=False, Name:="SalesPrice", EmitDefaultValue:=False)>
        Public Property SalesPrice As String

        <DataMember(IsRequired:=True, Name:="InsuranceActivationDate")>
        Public Property InsuranceActivationDate As Nullable(Of Date)

        <DataMember(IsRequired:=True, Name:="CountryPurchase")>
        Public Property CountryPurchase As String

        <DataMember(IsRequired:=False, Name:="ExchangeRate", EmitDefaultValue:=False)>
        Public Property ExchangeRate As Decimal

        <DataMember(IsRequired:=False, Name:="UseDepreciation", EmitDefaultValue:=False)>
        Public Property UseDepreciation As String

        <DataMember(IsRequired:=False, Name:="PostPrePaid", EmitDefaultValue:=False)>
        Public Property PostPrePaid As String

        <DataMember(IsRequired:=False, Name:="SubscriberStatus", EmitDefaultValue:=False)>
        Public Property SubscriberStatus As String

        <DataMember(IsRequired:=False, Name:="BillingPlan", EmitDefaultValue:=False)>
        Public Property BillingPlan As String

        <DataMember(IsRequired:=False, Name:="BillingCycle", EmitDefaultValue:=False)>
        Public Property BillingCycle As String

        <DataMember(IsRequired:=False, Name:="ServiceLineNumber", EmitDefaultValue:=False)>
        Public Property ServiceLineNumber As String

        <DataMember(IsRequired:=False, Name:="SubscriberStatusChangeDate", EmitDefaultValue:=False)>
        Public Property SubscriberStatusChangeDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="ProdLiabilityLimit", EmitDefaultValue:=False)>
        Public Property ProdLiabilityLimit As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="UpgradeTerm", EmitDefaultValue:=False)>
        Public Property UpgradeTerm As Nullable(Of Integer)

        <DataMember(IsRequired:=False, Name:="CancellationReasonCode", EmitDefaultValue:=False)>
        Public Property CancellationReasonCode As String

        <DataMember(IsRequired:=False, Name:="CancellationReasonDesc", EmitDefaultValue:=False)>
        Public Property CancellationReasonDesc As String

        <DataMember(IsRequired:=False, Name:="Salutation", EmitDefaultValue:=False)>
        Public Property Salutation As String

        <DataMember(IsRequired:=False, Name:="CustomerFullName", EmitDefaultValue:=False)>
        Public Property CustomerFullName As String

        <DataMember(IsRequired:=False, Name:="CustomerHomePhoneNumber", EmitDefaultValue:=False)>
        Public Property CustomerHomePhoneNumber As String

        <DataMember(IsRequired:=False, Name:="CustomerWorkPhoneNumber", EmitDefaultValue:=False)>
        Public Property CustomerWorkPhoneNumber As String

        <DataMember(IsRequired:=False, Name:="CustomerEmailAddress", EmitDefaultValue:=False)>
        Public Property CustomerEmailAddress As String

        <DataMember(IsRequired:=False, Name:="CustomerNumber", EmitDefaultValue:=False)>
        Public Property MembershipNumber As String

        <DataMember(IsRequired:=False, Name:="CustomerIdentificationNumber", EmitDefaultValue:=False)>
        Public Property CustomerIdentificationNumber As String

        <DataMember(IsRequired:=False, Name:="CustomerDOB", EmitDefaultValue:=False)>
        Public Property CustomerDOB As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="Language", EmitDefaultValue:=False)>
        Public Property Language As String

        <DataMember(IsRequired:=False, Name:="PrimaryMemberName", EmitDefaultValue:=False)>
        Public Property PrimaryMemberName As String

        <DataMember(IsRequired:=False, Name:="MembershipType", EmitDefaultValue:=False)>
        Public Property MembershipType As String

        <DataMember(IsRequired:=False, Name:="PoliticallyExposed", EmitDefaultValue:=False)>
        Public Property PoliticallyExposed As String

        <DataMember(IsRequired:=False, Name:="Nationality", EmitDefaultValue:=False)>
        Public Property Nationality As String

        <DataMember(IsRequired:=False, Name:="PlaceOfBirth", EmitDefaultValue:=False)>
        Public Property PlaceOfBirth As String

        <DataMember(IsRequired:=False, Name:="Gender", EmitDefaultValue:=False)>
        Public Property Gender As String

        <DataMember(IsRequired:=False, Name:="TerroristFlag", EmitDefaultValue:=False)>
        Public Property TerroristFlag As String

        <DataMember(IsRequired:=False, Name:="MaritalStatus", EmitDefaultValue:=False)>
        Public Property MaritalStatus As String

        <DataMember(IsRequired:=False, Name:="TotalPremium", EmitDefaultValue:=False)>
        Public Property TotalPremium As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="RefundAmount", EmitDefaultValue:=False)>
        Public Property RefundAmount As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="MonthlyPayments", EmitDefaultValue:=False)>
        Public Property MonthlyPayments As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="FinancedAmount", EmitDefaultValue:=False)>
        Public Property FinancedAmount As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="FinancedTabAmount", EmitDefaultValue:=False)>
        Public Property FinancedTabAmount As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="FinancedTabTerm", EmitDefaultValue:=False)>
        Public Property FinancedTabTerm As Nullable(Of Integer)

        <DataMember(IsRequired:=False, Name:="FinancedTabFrequency", EmitDefaultValue:=False)>
        Public Property FinancedTabFrequency As Nullable(Of Integer)

        <DataMember(IsRequired:=False, Name:="FinancedTabInstAmount", EmitDefaultValue:=False)>
        Public Property FinancedTabInstAmount As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="FinanceDate", EmitDefaultValue:=False)>
        Public Property FinanceDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="DownPayment", EmitDefaultValue:=False)>
        Public Property DownPayment As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="AdvancePayment", EmitDefaultValue:=False)>
        Public Property AdvancePayment As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="CommissionBreakdown", EmitDefaultValue:=False)>
        Public Property CommissionBreakdown As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="UnearnedPermium", EmitDefaultValue:=False)>
        Public Property UnearnedPermium As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="SuspendedReasonCode", EmitDefaultValue:=False)>
        Public Property SuspendedReasonCode As String

        <DataMember(IsRequired:=False, Name:="SuspendedReasonDesc", EmitDefaultValue:=False)>
        Public Property SuspendedReasonDesc As String

        <DataMember(IsRequired:=False, Name:="NewUsed", EmitDefaultValue:=False)>
        Public Property NewUsed As String

        <DataMember(IsRequired:=False, Name:="PaymentInstrumentCode", EmitDefaultValue:=False)>
        Public Property PaymentInstrumentCode As String

        <DataMember(IsRequired:=False, Name:="PaymentInstrumentDesc", EmitDefaultValue:=False)>
        Public Property PaymentInstrumentDesc As String

        <DataMember(IsRequired:=False, Name:="CollectionMethodCode", EmitDefaultValue:=False)>
        Public Property CollectionMethodCode As String

        <DataMember(IsRequired:=False, Name:="CollectionMethodDesc", EmitDefaultValue:=False)>
        Public Property CollectionMethodDesc As String

        <DataMember(IsRequired:=False, Name:="TypeOfEquipmentCode", EmitDefaultValue:=False)>
        Public Property TypeOfEquipmentCode As String

        <DataMember(IsRequired:=False, Name:="TypeOfEquipmentDesc", EmitDefaultValue:=False)>
        Public Property TypeOfEquipmentDesc As String

        'REQ-5838 start Add new fields to the operation
        <DataMember(IsRequired:=False, Name:="CuitCuil", EmitDefaultValue:=False)>
        Public Property CuitCuil As String

        <DataMember(IsRequired:=False, Name:="DealerProductCode", EmitDefaultValue:=False)>
        Public Property DealerProductCode As String

        <DataMember(IsRequired:=False, Name:="DealerItem", EmitDefaultValue:=False)>
        Public Property DealerItem As String

        <DataMember(IsRequired:=False, Name:="SalesRepNumber", EmitDefaultValue:=False)>
        Public Property SalesRepNumber As String

        <DataMember(IsRequired:=False, Name:="ControlNumber", EmitDefaultValue:=False)>
        Public Property ControlNumber As String

        <DataMember(IsRequired:=False, Name:="PaidOn", EmitDefaultValue:=False)>
        Public Property PaidOn As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="LinesOnAccount", EmitDefaultValue:=False)>
        Public Property LinesOnAccount As Nullable(Of Integer)

        <DataMember(IsRequired:=False, Name:="PersonType", EmitDefaultValue:=False)>
        Public Property PersonType As String

        <DataMember(IsRequired:=False, Name:="DateAdded", EmitDefaultValue:=False)>
        Public Property DateAdded As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="LastMaintained", EmitDefaultValue:=False)>
        Public Property LastMaintained As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="Source", EmitDefaultValue:=False)>
        Public Property Source As String

        <DataMember(IsRequired:=False, Name:="Region", EmitDefaultValue:=False)>
        Public Property Region As String

        <DataMember(IsRequired:=False, Name:="Retailer", EmitDefaultValue:=False)>
        Public Property Retailer As String

        <DataMember(IsRequired:=False, Name:="DealerGroupName", EmitDefaultValue:=False)>
        Public Property DealerGroupName As String
        'REQ-5838 end 

        <DataMember(IsRequired:=False, Name:="CustomerAddress", EmitDefaultValue:=False)>
        Public Property CustomerAddress As AddressInfo

        <DataMember(IsRequired:=False, Name:="CurrencyISOCode", EmitDefaultValue:=False)>
        Public Property CurrencyISOCode As String

        <DataMember(IsRequired:=False, Name:="SalesTax", EmitDefaultValue:=False)>
        Public Property SalesTax As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="Occupation", EmitDefaultValue:=False)>
        Public Property Occupation As String

        <DataMember(IsRequired:=True, Name:="DefaultMethodOfRepairCode", EmitDefaultValue:=False)>
        Public Property MethodOfRepairCode As String

        <DataMember(IsRequired:=True, Name:="DefaultMethodOfRepairDesc", EmitDefaultValue:=False)>
        Public Property MethodOfRepairDesc As String

        <DataMember(IsRequired:=False, Name:="InstallmentAmount", EmitDefaultValue:=False)>
        Public Property InstallmentAmount As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="CoverageRate", EmitDefaultValue:=False)>
        Public Property CoverageRate As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="CoverageRateNetOfTax", EmitDefaultValue:=False)>
        Public Property CoverageRateNetOfTax As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="Endorsement_0_EffectiveDate")>
        Public Property EndorsementEffectiveDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="Endorsement_0_ExpirationDate")>
        Public Property EndorsementExpirationDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="LastPolicyNumber")>
        Public Property LastPolicyNumber As String

        <DataMember(IsRequired:=False, Name:="OriginalPolicyNumber")>
        Public Property OriginalPolicyNumber As String

        <DataMember(IsRequired:=True, Name:="Contract")>
        Public Property Contract As ContractInfo

        <DataMember(IsRequired:=True, Name:="Dealer")>
        Public Property Dealer As DealerInfo

        <DataMember(IsRequired:=True, Name:="Company")>
        Public Property Company As CompanyInfo

        <DataMember(Name:="Items")>
        Public Property Items As IEnumerable(Of CertificateItemInfo)

        Friend Sub Populate(pCertificate As Certificate,
                            pDealer As Dealer,
                            pProduct As Product,
                            pcompany As Company,
                            pAddress As Address,
                            pCountry As Country,
                            pContract As Contract,
                            pCompanyGroup As CompanyGroup,
                            pEquipmentManager As EquipmentManager,
                            pCommonManager As ICommonManager,
                            pClaimManager As IClaimManager,
                            pCountryManager As ICountryManager,
                            pCertificateManager As ICertificateManager,
                            pLanguage As String,
                            Optional ByVal pCurrency As Currency = Nothing)
            With Me

                .CertificateNumber = pCertificate.CertificateNumber
                .ProductCode = pCertificate.ProductCode
                .ProductDescription = pProduct.Description
                'pDealer.Products.Where(Function(p) p.ProductCode = pCertificate.ProductCode).FirstOrDefault.Description  'pProduct.Description
                .Status = pCertificate.StatusCode.ToDescription(pCommonManager, ListCodes.CertificateStatus, pLanguage)
                .InvoiceNumber = pCertificate.InvoiceNumber
                .WarrantySalesDate = pCertificate.WarrantySalesDate
                .ClaimsPaidAmount = pClaimManager.GetClaimsPaidAmountByCertificate(pCertificate.CertificateId)
                .BillingCycle = pCertificate.BillingCycle
                .BillingPlan = pCertificate.BillingPlan
                .ProductSalesDate = pCertificate.ProductSalesDate
                .DealerBranchCode = pCertificate.DealerBranchCode
                .SalesPrice = pCertificate.SalesPrice

                .InsuranceActivationDate = pCertificate.InsuranceActivationDate
                .DatePaidFor = pCertificate.DatePaidFor
                .LinkedCertNumber = pCertificate.LinkedCertNumber
                .PaymentTypeCode = If(Not pCertificate.PaymentTypeId Is Nothing,
                                    pCompanyGroup.PaymentTypes.Where(Function(p) p.PaymentTypeId = pCertificate.PaymentTypeId).FirstOrDefault.Code, Nothing)
                .PaymentTypeDesc = If(Not pCertificate.PaymentTypeId Is Nothing,
                                    pCompanyGroup.PaymentTypes.Where(Function(p) p.PaymentTypeId = pCertificate.PaymentTypeId).FirstOrDefault.Description, Nothing)
                .CurrencyCode = If(Not pCurrency Is Nothing, pCurrency.Code, String.Empty)
                .FinanceCurrencyCode = If(Not pCurrency Is Nothing, pCurrency.Code, String.Empty)
                .ExchangeRate = If(Not pCertificate.ExchangeRate Is Nothing, pCertificate.ExchangeRate, 1) 'default to 1 if no exchange rate
                .UseDepreciation = If(Not pCertificate.UseDepreciation Is Nothing, pCertificate.UseDepreciation.ToDescription(pCommonManager, ListCodes.YesNo, pLanguage), Nothing)
                If (Not pCertificate.PostPrePaidId Is Nothing) Then
                    .PostPrePaid = pCertificate.PostPrePaidId.ToCode(pCommonManager, ListCodes.PostPrePaid, pLanguage) + "-" + pCertificate.PostPrePaidId.ToDescription(pCommonManager, ListCodes.PostPrePaid, pLanguage)
                End If
                .SubscriberStatus = If(Not pCertificate.SubscriberStatus Is Nothing, pCertificate.SubscriberStatus.ToDescription(pCommonManager, ListCodes.SubscriberStatus, pLanguage), Nothing)
                .ServiceLineNumber = pCertificate.ServiceLineNumber
                .SubscriberStatusChangeDate = pCertificate.SubscriberStatusChangeDate
                .ProdLiabilityLimit = pProduct.PROD_LIABILITY_LIMIT
                .UpgradeTerm = pCertificate.UpgradeFixedTerm
                .DownPayment = pCertificate.DownPayment
                .AdvancePayment = pCertificate.AdvancePayment
                .Salutation = If(Not pCertificate.SalutationId Is Nothing, pCertificate.SalutationId.ToDescription(pCommonManager, ListCodes.Salutation, pLanguage), Nothing)
                If Not (pCertificate.CustomerName Is Nothing) AndAlso pCertificate.CustomerName.Trim <> String.Empty Then
                    .CustomerFullName = pCertificate.CustomerName
                End If
                .CustomerEmailAddress = pCertificate.Email
                .MembershipNumber = pCertificate.MembershipNumber
                .CustomerIdentificationNumber = pCertificate.IdentificationNumber
                .CustomerDOB = pCertificate.BirthDate
                .PrimaryMemberName = pCertificate.PrimaryMemberName
                .MembershipType = If(Not pCertificate.MembershipTypeId Is Nothing, pCertificate.MembershipTypeId.ToDescription(pCommonManager, ListCodes.MembershipType, pLanguage), Nothing)
                .PoliticallyExposed = If(Not pCertificate.PoliticallyExposedId Is Nothing, pCertificate.PoliticallyExposedId.ToDescription(pCommonManager, ListCodes.YesNo, pLanguage), Nothing)
                .Nationality = If(Not pCertificate.NationalityId Is Nothing, pCertificate.NationalityId.ToDescription(pCommonManager, ListCodes.Nationality, pLanguage), Nothing)
                .PlaceOfBirth = If(Not pCertificate.PlaceOfBirthId Is Nothing, pCertificate.PlaceOfBirthId.ToDescription(pCommonManager, ListCodes.PlaceOfBirth, pLanguage), Nothing)
                .Gender = If(Not pCertificate.GenderId Is Nothing, pCertificate.GenderId.ToDescription(pCommonManager, ListCodes.Gender, pLanguage), Nothing)
                .MaritalStatus = If(Not pCertificate.MaritalStatusId Is Nothing, pCertificate.MaritalStatusId.ToDescription(pCommonManager, ListCodes.MaritalStatus, pLanguage), Nothing)
                .MonthlyPayments = pCertificate.MonthlyPayments
                .FinancedAmount = pCertificate.FinancedAmount
                .FinancedTabAmount = pCertificate.FinancedTabAmount
                .FinancedTabTerm = pCertificate.FinancedTabTerm
                .FinancedTabFrequency = pCertificate.FinancedTabFrequency
                .FinancedTabInstAmount = pCertificate.FinancedTabInstAmount
                .FinanceDate = pCertificate.FinanceDate
                If Not pAddress Is Nothing Then
                    .CustomerAddress = New AddressInfo(pAddress, pCountryManager)
                End If
                .CustomerHomePhoneNumber = pCertificate.HomePhone
                .CustomerWorkPhoneNumber = pCertificate.WorkPhone
                '''''''check the below line for certificate cancellation
                If (Not pCertificate.Cancellations.LastOrDefault Is Nothing) Then
                    .CancellationDate = pCertificate.Cancellations.LastOrDefault.CancellationDate
                    .CancellationReasonCode = pcompany.CancellationReasons.Where(Function(cc) cc.CancellationId = pCertificate.Cancellations.LastOrDefault.CancellationReasonId).FirstOrDefault.Code
                    .CancellationReasonDesc = pcompany.CancellationReasons.Where(Function(cc) cc.CancellationId = pCertificate.Cancellations.LastOrDefault.CancellationReasonId).FirstOrDefault.Description
                End If

                .CountryPurchase = pCountry.Code
                .SuspendedReasonCode = If(Not pCertificate.SuspendedReasonId Is Nothing, pDealer.SuspendedReasons.Where(Function(s) s.SuspendedReasonId = pCertificate.SuspendedReasonId).FirstOrDefault.Code, Nothing)
                .SuspendedReasonDesc = If(Not pCertificate.SuspendedReasonId Is Nothing, pDealer.SuspendedReasons.Where(Function(s) s.SuspendedReasonId = pCertificate.SuspendedReasonId).FirstOrDefault.Description, Nothing)
                .TerroristFlag = IIf(pCertificate.TerroristFlag = "1", "Yes", "No")
                .NewUsed = pCertificate.NewUsed
                .CollectionMethodCode = If(Not pCertificate.PaymentTypeId Is Nothing,
                                        pCompanyGroup.PaymentTypes.Where(Function(p) p.PaymentTypeId = pCertificate.PaymentTypeId).FirstOrDefault.CollectionMethodId.ToCode(pCommonManager, ListCodes.CollectionMethod, pLanguage), Nothing)
                .CollectionMethodDesc = If(Not pCertificate.PaymentTypeId Is Nothing,
                                        pCompanyGroup.PaymentTypes.Where(Function(p) p.PaymentTypeId = pCertificate.PaymentTypeId).FirstOrDefault.CollectionMethodId.ToDescription(pCommonManager, ListCodes.CollectionMethod, pLanguage), Nothing)
                .PaymentInstrumentCode = If(Not pCertificate.PaymentTypeId Is Nothing,
                                        pCompanyGroup.PaymentTypes.Where(Function(p) p.PaymentTypeId = pCertificate.PaymentTypeId).FirstOrDefault.PaymentInstrumentId.ToCode(pCommonManager, ListCodes.PaymentInstrumentType, pLanguage), Nothing)
                .PaymentInstrumentDesc = If(Not pCertificate.PaymentTypeId Is Nothing,
                                        pCompanyGroup.PaymentTypes.Where(Function(p) p.PaymentTypeId = pCertificate.PaymentTypeId).FirstOrDefault.PaymentInstrumentId.ToDescription(pCommonManager, ListCodes.PaymentInstrumentType, pLanguage), Nothing)
                .TypeOfEquipmentCode = If(Not pCertificate.TypeOfEquipmentId Is Nothing, pCertificate.TypeOfEquipmentId.ToCode(pCommonManager, ListCodes.EquipmentType, pLanguage), Nothing)
                .TypeOfEquipmentDesc = If(Not pCertificate.TypeOfEquipmentId Is Nothing, pCertificate.TypeOfEquipmentId.ToDescription(pCommonManager, ListCodes.EquipmentType, pLanguage), Nothing)
                .TotalPremium = pCertificate.ItemCoverages.Sum(Function(ic) ic.GrossAmtReceived)

                .CuitCuil = pCertificate.CuitCuil
                .DealerProductCode = pCertificate.DealerProductCode
                .DealerItem = pCertificate.DealerItem
                .SalesRepNumber = pCertificate.SalesRepNumber
                .ControlNumber = pCertificate.CampaignNumber
                .PaidOn = pCertificate.DatePaid
                .LinesOnAccount = pCertificate.LinesOnAccount
                .PersonType = If(Not pCertificate.PersonTypeId Is Nothing, pCertificate.PersonTypeId.ToDescription(pCommonManager, ListCodes.PersonType, pLanguage), Nothing)
                .DateAdded = pCertificate.CreatedDate
                .LastMaintained = pCertificate.ModifiedDate
                .Source = pCertificate.Source
                .Region = pCertificate.REGION
                .Retailer = pCertificate.Retailer
                If Not pDealer.DealerGroup Is Nothing Then
                    .DealerGroupName = pDealer.DealerGroup.Description
                End If

                'REQ-5843 start
                .CurrencyISOCode = If(Not pCurrency Is Nothing, pCurrency.ISO_CODE, String.Empty)

                Dim dEffectiveDate As String, dExpirationDate As String
                pCertificateManager.GetFirstCertEndorseDates(pCertificate.CertificateId, dEffectiveDate, dExpirationDate)
                If dEffectiveDate <> String.Empty Then
                    .EndorsementEffectiveDate = CDate(dEffectiveDate)
                End If
                If dExpirationDate <> String.Empty Then
                    .EndorsementExpirationDate = CDate(dExpirationDate)
                End If

                If Not pCertificate.PreviousCertId Is Nothing Then
                    .LastPolicyNumber = pCertificateManager.GetCertNumber(pCertificate.PreviousCertId)
                End If

                If Not pCertificate.OriginalCertId Is Nothing Then
                    .OriginalPolicyNumber = pCertificateManager.GetCertNumber(pCertificate.OriginalCertId)
                End If

                .Occupation = pCertificate.OCCUPATION
                .MethodOfRepairCode = pCertificate.MethodOfRepairId.ToCode(pCommonManager, ListCodes.MethodOfRepair, pLanguage)
                .MethodOfRepairDesc = pCertificate.MethodOfRepairId.ToDescription(pCommonManager, ListCodes.MethodOfRepair, pLanguage)

                ' todo: load installment amount from elp_cert_installment record
                If pCertificate.CertificateInstallments.Count > 0 AndAlso pCertificate.CertificateInstallments(0).InstallmentAmount.HasValue = True Then
                    .InstallmentAmount = pCertificate.CertificateInstallments(0).InstallmentAmount.Value
                End If
                Dim dGWP As Decimal, dSalesTax As Decimal
                pCertificateManager.GetCertificateCoverageRate(pCertificate.CertificateId, Today.Date, dGWP, dSalesTax)
                .CoverageRate = dGWP
                .CoverageRateNetOfTax = dGWP - dSalesTax
                .SalesTax = dSalesTax
                'todo- add new premium

                'REQ-5843 end

            End With

            Items = New List(Of CertificateItemInfo)()

            For Each ci As CertificateItem In pCertificate.Item
                DirectCast(Items, IList(Of CertificateItemInfo)).Add(New CertificateItemInfo(ci, pCommonManager, pCompanyGroup, pEquipmentManager, pProduct, pLanguage))
            Next


        End Sub

    End Class

End Namespace