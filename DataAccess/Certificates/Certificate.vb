'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

#if DataEntities
Partial Public Class Certificate
         Inherits BaseEntity
         Implements ICertificateEntity, IRecordCreateModifyInfo 
    Public Property CertificateId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return CertificateId
		End Get
	End Property
    Public Property DealerId As System.Guid
    Public Property CertificateNumber As String
    Public Property PaymentTypeId As Nullable(Of System.Guid)
    Public Property CommissionBreakdownId As Nullable(Of System.Guid)
    Public Property FinanceCurrencyId As Nullable(Of System.Guid)
    Public Property PURCHASE_CURRENCY_ID As Nullable(Of System.Guid)
    Public Property MethodOfRepairId As System.Guid
    Public Property TypeOfEquipmentId As Nullable(Of System.Guid)
    Public Property AddressId As Nullable(Of System.Guid)
    Public Property ProductCode As String
    Public Property StatusCode As String
    Public Property ProductSalesDate As Nullable(Of Date)
    Public Property WarrantySalesDate As Nullable(Of Date)
    Public Property IdentificationNumber As String
    Public Property CustomerName As String
    Public Property HomePhone As String
    Public Property WorkPhone As String
    Public Property Email As String
    Public Property DealerBranchCode As String
    Public Property SalesRepNumber As String
    Public Property QUOTA_NUMBER As Nullable(Of Short)
    Public Property MonthlyPayments As Nullable(Of Decimal)
    Public Property FinancedAmount As Nullable(Of Decimal)
    Public Property SalesPrice As Nullable(Of Decimal)
    Public Property InterestRate As Nullable(Of Decimal)
    Public Property CampaignNumber As String
    Public Property Source As String
    Public Property DatePaidFor As Nullable(Of Date)
    Public Property DatePaid As Nullable(Of Date)
    Public Property Retailer As String
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property CompanyId As System.Guid
    Public Property SalutationId As Nullable(Of System.Guid)
    Public Property OLD_NUMBER As String
    Public Property InsuranceActivationDate As Nullable(Of Date)
    Public Property DOCUMENT_AGENCY As String
    Public Property DOCUMENT_ISSUE_DATE As Nullable(Of Date)
    Public Property RG_NUMBER As String
    Public Property ID_TYPE As String
    Public Property RATING_PLAN As Nullable(Of Short)
    Public Property DOCUMENT_TYPE_ID As Nullable(Of System.Guid)
    Public Property CountryPurchaseId As Nullable(Of System.Guid)
    Public Property ExchangeRate As Nullable(Of Decimal)
    Public Property CURRENCY_CERT_ID As Nullable(Of System.Guid)
    Public Property PASSWORD As String
    Public Property VEHICLE_YEAR As Nullable(Of Integer)
    Public Property MODEL_ID As Nullable(Of System.Guid)
    Public Property ODOMETER As Nullable(Of Integer)
    Public Property CLASS_CODE_ID As Nullable(Of System.Guid)
    Public Property REGISTRATION_DATE As Nullable(Of Date)
    Public Property VEHICLE_LICENSE_TAG As String
    Public Property VIN_LOCATOR As String
    Public Property UseDepreciation As Nullable(Of System.Guid)
    Public Property BirthDate As Nullable(Of Date)
    Public Property MAILING_ADDRESS_ID As Nullable(Of System.Guid)
    Public Property MembershipNumber As String
    Public Property PrimaryMemberName As String
    Public Property MembershipTypeId As Nullable(Of System.Guid)
    Public Property VAT_NUM As String
    Public Property LANGUAGE_ID As Nullable(Of System.Guid)
    Public Property PostPrePaidId As Nullable(Of System.Guid)
    Public Property MARKETING_PROMO_SER As String
    Public Property MARKETING_PROMO_NUM As String
    Public Property SubscriberStatus As Nullable(Of System.Guid)
    Public Property BillingPlan As String
    Public Property BillingCycle As String
    Public Property ServiceLineNumber As String
    Public Property REGION As String
    Public Property SubscriberStatusChangeDate As Nullable(Of Date)
    Public Property LinesOnAccount As Nullable(Of Short)
    Public Property OCCUPATION As String
    Public Property PoliticallyExposedId As Nullable(Of System.Guid)
    Public Property INCOME_RANGE_ID As Nullable(Of System.Guid)
    Public Property CESS_OFFICE As String
    Public Property CESS_SALESREP As String
    Public Property BusinessLine As String
    Public Property SALES_DEPARTMENT As String
    Public Property LinkedCertNumber As String
    Public Property ADDITIONAL_INFO As String
    Public Property SuspendedReasonId As Nullable(Of System.Guid)
    Public Property FINANCING_AGENCY As String
    Public Property EXTERNAL_REGISTRATION_DATE As Nullable(Of Date)
    Public Property CUSTOMERINFO_LASTCHANGE_DATE As Nullable(Of Date)
    Public Property PEP_FLAGGED_DATE As Nullable(Of Date)
    Public Property TERRORIST_FLAGGED_DATE As Nullable(Of Date)
    Public Property NationalityId As Nullable(Of System.Guid)
    Public Property PlaceOfBirthId As Nullable(Of System.Guid)
    Public Property GenderId As Nullable(Of System.Guid)
    Public Property CuitCuil As String
    Public Property TerroristFlag As String
    Public Property NewUsed As String
    Public Property FinancedTabAmount As Nullable(Of Decimal)
    Public Property FinancedTabTerm As Nullable(Of Short)
    Public Property FinancedTabFrequency As Nullable(Of Short)
    Public Property FinancedTabInstAmount As Nullable(Of Decimal)
    Public Property MaritalStatusId As Nullable(Of System.Guid)
    Public Property ProdLiabilityLimit As Nullable(Of Decimal)
    Public Property PROD_LIABILITY_LIMIT_BASE_ID As Nullable(Of System.Guid)
    Public Property PROD_LIABILITY_LIMIT_POLICY_ID As Nullable(Of System.Guid)
    Public Property FinanceDate As Nullable(Of Date)
    Public Property DownPayment As Nullable(Of Decimal)
    Public Property AdvancePayment As Nullable(Of Decimal)
    Public Property UpgradeFixedTerm As Nullable(Of Short)
    Public Property PersonTypeId As Nullable(Of System.Guid)
    Public Property PreviousCertId As Nullable(Of System.Guid)
    Public Property OriginalCertId As Nullable(Of System.Guid)
    Public Property InvoiceNumber As String
    Public Property DealerItem As String
    Public Property DealerProductCode As String

    Public Overridable Property Item As ICollection(Of CertificateItem) = New HashSet(Of CertificateItem)
    Public Overridable Property ItemCoverages As ICollection(Of CertificateItemCoverage) = New HashSet(Of CertificateItemCoverage)
    Public Overridable Property Cancellations As ICollection(Of CertificateCancellation) = New HashSet(Of CertificateCancellation)
    Public Overridable Property BillingDetails As ICollection(Of BillingDetail) = New HashSet(Of BillingDetail)
    Public Overridable Property CertificateInstallments As ICollection(Of CertificateInstallment) = New HashSet(Of CertificateInstallment)

End Class
#End If
