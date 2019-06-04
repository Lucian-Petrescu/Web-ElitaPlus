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
Partial Public Class Dealer
         Inherits BaseEntity
         Implements IDealerEntity, IRecordCreateModifyInfo 
    Public Property DealerId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return DealerId
		End Get
	End Property
    Public Property DealerCode As String
    Public Property DealerName As String
    Public Property TAX_ID_NUMBER As String
    Public Property CompanyId As System.Guid
    Public Property AddressId As Nullable(Of System.Guid)
    Public Property CONTACT_NAME As String
    Public Property CONTACT_PHONE As String
    Public Property CONTACT_EXT As String
    Public Property CONTACT_FAX As String
    Public Property CONTACT_EMAIL As String
    Public Property LAST_CERTIFICATE_NBR As Nullable(Of Long)
    Public Property RETAILER_ID As System.Guid
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property DEALER_GROUP_ID As Nullable(Of System.Guid)
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property ACTIVE_FLAG As String
    Public Property SERVICE_NETWORK_ID As Nullable(Of System.Guid)
    Public Property IBNR_FACTOR As Nullable(Of Decimal)
    Public Property IBNR_COMPUTE_METHOD_ID As Nullable(Of System.Guid)
    Public Property CONVERT_PRODUCT_CODE_ID As Nullable(Of System.Guid)
    Public Property DealerTypeId As System.Guid
    Public Property BRANCH_VALIDATION_ID As System.Guid
    Public Property BANK_INFO_ID As Nullable(Of System.Guid)
    Public Property BUSINESS_NAME As String
    Public Property STATE_TAX_ID_NUMBER As String
    Public Property CITY_TAX_ID_NUMBER As String
    Public Property WEB_ADDRESS As String
    Public Property MAILING_ADDRESS_ID As Nullable(Of System.Guid)
    Public Property NUMBER_OF_OTHER_LOCATIONS As Nullable(Of Short)
    Public Property PRICE_MATRIX_USES_WP_ID As System.Guid
    Public Property DEDUCTIBLE_DISCOUNT As Nullable(Of Decimal)
    Public Property INVOICE_BY_BRANCH_ID As System.Guid
    Public Property SEPARATED_CREDIT_NOTES_ID As System.Guid
    Public Property MANUAL_ENROLLMENT_ALLOWED_ID As System.Guid
    Public Property EDIT_BRANCH_ID As System.Guid
    Public Property OLITA_SEARCH As System.Guid
    Public Property DELAY_FACTOR_FLAG_ID As System.Guid
    Public Property INSTALLMENT_FACTOR_FLAG_ID As System.Guid
    Public Property REGISTRATION_PROCESS_FLAG_ID As System.Guid
    Public Property STAT_IBNR_FACTOR As Nullable(Of Decimal)
    Public Property STAT_IBNR_COMPUTE_METHOD_ID As Nullable(Of System.Guid)
    Public Property LAE_IBNR_FACTOR As Nullable(Of Decimal)
    Public Property LAE_IBNR_COMPUTE_METHOD_ID As Nullable(Of System.Guid)
    Public Property REGISTRATION_EMAIL_FROM As String
    Public Property USE_WARRANTY_MASTER_ID As System.Guid
    Public Property USE_INCOMING_SALES_TAX_ID As System.Guid
    Public Property AUTO_PROCESS_FILE_ID As System.Guid
    Public Property ROUND_COMM_FLAG_ID As System.Guid
    Public Property CERT_CANCEL_BY_ID As System.Guid
    Public Property PROGRAM_NAME As String
    Public Property SERVICE_LINE_PHONE As String
    Public Property SERVICE_LINE_FAX As String
    Public Property SERVICE_LINE_EMAIL As String
    Public Property ESC_INSURANCE_LABEL As String
    Public Property EXPECTED_PREMIUM_IS_WP_ID As System.Guid
    Public Property CLAIM_SYSTEM_ID As System.Guid
    Public Property ASSURANT_IS_OBLIGOR_ID As Nullable(Of System.Guid)
    Public Property USE_INSTALLMENT_DEFN_ID As System.Guid
    Public Property MAX_MAN_WARR As Short
    Public Property MIGRATION_PATH_ID As Nullable(Of System.Guid)
    Public Property UseEquipmentId As System.Guid
    Public Property CANCEL_REQUEST_FLAG_ID As Nullable(Of System.Guid)
    Public Property PayDeductibleId As Nullable(Of System.Guid)
    Public Property BANK_INFO_MANDATORY_ID As System.Guid
    Public Property ValidateSkuId As Nullable(Of System.Guid)
    Public Property MIN_MAN_WARR As Short
    Public Property VALIDATE_BILLING_CYCLE_ID As Nullable(Of System.Guid)
    Public Property AuthAmtBasedOnId As Nullable(Of System.Guid)
    Public Property EquipmentListCode As String
    Public Property QUESTION_LIST_CODE As String
    Public Property PRODUCT_BY_REGION_ID As Nullable(Of System.Guid)
    Public Property CLAIM_VERIFICATION_NUM_LENGTH As Nullable(Of Short)
    Public Property CLAIM_EXTENDED_STATUS_ENTRY_ID As System.Guid
    Public Property VALIDATE_SERIAL_NUMBER_ID As System.Guid
    Public Property ALLOW_UPDATE_CANCELLATION_ID As Nullable(Of System.Guid)
    Public Property REJECT_AFTER_CANCELLATION_ID As Nullable(Of System.Guid)
    Public Property ALLOW_FUTURE_CANCEL_DATE_ID As Nullable(Of System.Guid)
    Public Property DealerSupportWebClaimsId As System.Guid
    Public Property UseClaimAuthorizationId As Nullable(Of System.Guid)
    Public Property DeductibleCollectionId As System.Guid
    Public Property CLAIM_STATUS_FOR_EXT_SYSTEM_ID As System.Guid
    Public Property NEW_DEVICE_SKU_REQUIRED_ID As Nullable(Of System.Guid)
    Public Property LICENSE_TAG_VALIDATION As Nullable(Of System.Guid)
    Public Property ENROLLFILEPREPROCESSPROC_ID As Nullable(Of System.Guid)
    Public Property CertNumLookUpById As Nullable(Of System.Guid)
    Public Property REPLACECLAIMDEDTOLERANCEPCT As Nullable(Of Decimal)
    Public Property BILLING_PROCESS_CODE_ID As Nullable(Of System.Guid)
    Public Property BILLRESULT_EXCEPTION_DEST_ID As Nullable(Of System.Guid)
    Public Property BILLRESULT_NOTIFICATION_EMAIL As String
    Public Property MAX_NCRECORDS As Nullable(Of Decimal)
    Public Property ALLOW_ADJUST_PYMTDATE_ID As Nullable(Of System.Guid)
    Public Property OLD_ENDORSEMENTS_ID As Nullable(Of System.Guid)
    Public Property CLIENT_DEALER_CODE As String
    Public Property CERTIFICATES_AUTONUMBER_ID As Nullable(Of System.Guid)
    Public Property CERTIFICATES_AUTONUMBER_PREFIX As String
    Public Property FILE_LOAD_NOTIFICATION_EMAIL As String
    Public Property INSERT_MAKE_IF_NOT_EXIST_ID As System.Guid
    Public Property FULLFILEPROCESS_ID As System.Guid
    Public Property ALLOW_PROD_CHNG_FRM_COV_EFF_ID As System.Guid
    Public Property MAX_CERTNUM_LENGTH_ALLOWED As Nullable(Of Short)
    Public Property SUSPENDED_ENROLMENTS_ALLOWED As Nullable(Of System.Guid)
    Public Property AUTO_REJ_ERR_TYPE_ID As Nullable(Of System.Guid)
    Public Property REJECTED_RECORD_RECON_ID As Nullable(Of System.Guid)
    Public Property LAWSUIT_MANDATORY_ID As System.Guid
    Public Property AUTO_SELECT_SERVICE_CENTER As Nullable(Of System.Guid)
    Public Property DEALER_EXTRACT_PERIOD_ID As Nullable(Of System.Guid)
    Public Property POLICY_EVENT_NOTIFY_EMAIL As String
    Public Property CLAIM_AUTO_APPROVE_ID As System.Guid
    Public Property DEF_SALVAGE_CENTER_ID As Nullable(Of System.Guid)
    Public Property REUSE_SERIAL_NUMBER_ID As System.Guid
    Public Property REQUIRE_CUSTOMER_AML_INFO_ID As System.Guid
    Public Property AUTO_PROCESS_PYMT_FILE_ID As System.Guid
    Public Property GracePeriodMonths As Nullable(Of Decimal)
    Public Property GracePeriodDays As Nullable(Of Decimal)
    Public Property MAX_COMMISSION_PERCENT As Nullable(Of Decimal)
    Public Property VSC_VIN_RESTRIC_ID As Nullable(Of System.Guid)
    Public Property PLAN_CODE_IN_QUOTE_OUTPUT_ID As Nullable(Of System.Guid)
    Public Property AUTO_GEN_REJ_PYMT_FILE_ID As Nullable(Of System.Guid)
    Public Property PYMT_REJ_REC_RECON_ID As Nullable(Of System.Guid)

    Public Overridable Property Products As ICollection(Of Product) = New HashSet(Of Product)
    Public Overridable Property SuspendedReasons As ICollection(Of SuspendedReason) = New HashSet(Of SuspendedReason)
    Public Overridable Property DealerRuleLists As ICollection(Of DealerRuleList) = New HashSet(Of DealerRuleList)
    Public Overridable Property WarrantyMasters As ICollection(Of WarrantyMaster) = New HashSet(Of WarrantyMaster)
    Public Overridable Property Contracts As ICollection(Of Contract) = New HashSet(Of Contract)
    Public Overridable Property DealerGroup As DealerGroup
    Public Overridable Property Branches As ICollection(Of Branch) = New HashSet(Of Branch)

End Class
#End If
