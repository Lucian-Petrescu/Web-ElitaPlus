Public Class LookupListCache

#Region "Constants"

    Private Const CACHE_ITEM_SUFFIX As String = "_LOOKUP_LIST"

#End Region

#Region "Private Methods"

    Private Shared Function DetermineCacheItemName(ByVal itemName As String) As String
        Dim parameters As ElitaPlusParameters = Nothing
        Try
            parameters = CType(Threading.Thread.CurrentThread.CurrentPrincipal.Identity, ElitaPlusParameters)
        Catch ex As Exception
        End Try
        If (parameters Is Nothing) Then
            Return itemName & CACHE_ITEM_SUFFIX
        Else
            Return itemName & CACHE_ITEM_SUFFIX & String.Format("_{0}", parameters.ConnectionType.ToUpperInvariant())
        End If

    End Function

#End Region

#Region "Class Methods"

    Public Shared Function RetrieveFromCache(ByVal listName As String, Optional ByVal displayNothingSelected As Boolean = True) As DataView

        Dim key As String = listName & "-" & displayNothingSelected

        Dim dv As DataView = AppConfig.CommonCache.GetEntry(DetermineCacheItemName(key))

        Return dv

    End Function

    Public Shared Sub AddToCache(ByVal listName As String, ByVal dv As DataView, Optional ByVal displayNothingSelected As Boolean = True)

        Dim key As String = listName & "-" & displayNothingSelected
        ' JLR - Note: AddPermanententry may be used only when a mechanism to update cache across servers is implemented.

        'If AppConfig.CommonCache.GetDalLookupTable.ContainsList(listName) Then
        'AppConfig.CommonCache.AddPermanentEntry(DetermineCacheItemName(key), dv)
        'Else
        AppConfig.CommonCache.AddEntry(DetermineCacheItemName(key), dv)
        'End If

    End Sub

    Public Shared Function ClearFromCache(ByVal DALName As String) As Boolean

        Dim key As String
        Dim oDalTable As DalLookupTable = AppConfig.CommonCache.GetDalLookupTable
        Dim oLookupList As ArrayList = oDalTable.GetLookupList(DALName)
        Dim oLookupName As String

        If Not oLookupList Is Nothing Then
            For Each oLookupName In oLookupList
                key = DetermineCacheItemName(oLookupName & "-True")
                AppConfig.CommonCache.InvalidateEntry(key)
                key = DetermineCacheItemName(oLookupName & "-False")
                AppConfig.CommonCache.InvalidateEntry(key)
            Next
        End If

    End Function

#End Region

#Region "Available Lookup Lists"
    Public Const LK_USER_ROLE_PERMISSION As String = "USR_ROLE_PERMISSION"
    Public Const LK_OPERATORS As String = "OPERATORS"
    Public Const LK_FIELD_NAMES As String = "FIELD_NAMES"
    Public Const LK_CURRENCIES As String = "CURRENCIES"
    Public Const LK_COMPANIES As String = "COMPANIES"
    Public Const LK_ACCTCOMPANY As String = "ACCTCOMPANIES"
    Public Const LK_ADDRESSES As String = "ADDRESSES"
    Public Const LK_LANG_INDEPENDENT_YES_NO As String = "YES_NO"
    Public Const LK_PRO_RATA_METHOD As String = "PRMETHOD"
    Public Const LK_INCLUDE_FIRST_PAYMENT As String = "INCFIRSTPYMT"
    Public Const LK_BILLING_FREQUENCY As String = "BILLING_FREQUENCY"
    Public Const LK_LANGUAGE_CULTURES As String = "LANGUAGE_CULTURES"
    Public Const LK_LANGUAGES As String = "LANGUAGES"
    Public Const LK_MANUFACTURERS As String = "MANUFACTURERS"
    Public Const LK_DEALERS As String = "DEALERS"
    Public Const LK_PRODUCERS As String = "PRODUCERS"
    Public Const LK_PARENT_DEALERS As String = "PARENT_DEALERS"
    Public Const LK_DEALERS_BY_DEALER_TYPE As String = "DEALERS_BY_DEALER_TYPE"
    Public Const LK_DEALERS_DUAL_COLUMNS As String = "DEALERS_DUAL_COLUMNS"
    Public Const LK_DEALERS_WITH_EDIT_BRANCH_ONLY As String = "DEALERS_WITH_EDIT_BRANCH_ONLY"
    Public Const LK_DEALERS_DUAL_COLUMNS_WITH_EDIT_BRANCH_ONLY As String = "DEALERS_DUAL_COLUMNS_WITH_EDIT_BRANCH_ONLY"
    Public Const LK_POLICE_STATIONS As String = "POLICE_STATIONS"
    Public Const LK_DEALER_GROUPS As String = "DEALER_GROUPS"
    Public Const LK_DEALER_EDIT_MODEL As String = "DEALER_EDIT_MODEL"
    Public Const LK_PRODUCTCODE As String = "PRODUCTCODE"
    Public Const LK_AVAILABLEPRODUCTCODERISKTYPE As String = "AVAILABLEPRODUCTCODERISKTYPE"
    Public Const LK_SELECTEDPRODUCTCODERISKTYPE As String = "SELECTEDPRODUCTCODERISKTYPE"
    Public Const LK_BRANCHCODE As String = "BRANCHCODE"
    Public Const LK_PRODUCTCODE_BY_COMPANY As String = "PRODUCTCODE_BY_COMPANY"
    Public Const LK_USER_DEALER As String = "USER_DEALER"
    Public Const LK_USER_DEALER_ASSIGNED As String = "USER_DEALER_ASSIGNED"
    Public Const LK_TEMPLATE_DEALER As String = "DEALER_TEMPLATES"
    Public Const LK_SPLIT_SYSTEM As String = "SPLIT_SYSTEM"
    Public Const LK_DEALERS_MONTHLY_BILLING As String = "DEALERS_MONTHLY_BILLING"
    Public Const LK_DEALER_COMMISSION_BREAKDOWN As String = "DEALER_COMMISSION_BREAKDOWN"
    Public Const LK_DEALER_COMMISSION_BREAKDOWN_DUAL_COLUMNS As String = "DEALER_COMMISSION_BREAKDOWN_DUAL_COLUMNS"
    Public Const LK_ZIP_DISTRICTS As String = "ZIP_DISTRICTS"
    Public Const LK_RISKTYPES As String = "RISK_TYPES"
    Public Const LK_ITEM_RISKTYPE As String = "ITEM_RISKTYPE"
    Public Const LK_RISK_PRODUCTCODE As String = "RISK_PRODUCTCODE"
    Public Const LK_PRICEMATRIX_ITEM As String = "PRICEMATRIX_ITEM"
    Public Const LK_PERCENTOF_RETAIL_ITEM As String = "PERCENTOF_RETAIL_ITEM"
    Public Const LK_POSTALCODEFORMAT As String = "POSTALCODEFORMAT"
    Public Const LK_PRICE_GROUPS As String = "PRICE_GROUPS"
    Public Const LK_SERVICE_GROUPS As String = "SERVICE_GROUPS"
    Public Const LK_SERVICE_CENTERS As String = "SERVICE_CENTERS"
    Public Const LK_PICKLIST_STORES As String = "PICKLIST_STORES"
    Public Const LK_REPLACEMENT_STORES As String = "REPLACEMENT_STORES"
    Public Const LK_PICKLIST_HEADER As String = "PICKLIST_HEADER"
    Public Const LK_SERVICE_NETWORKS As String = "SERVICE_NETWORKS"
    Public Const LK_ROUTE As String = "ROUTE"
    'Public Const LK_SERVICE_TYPES As String = "SERVICE_TYPES" ' this does not seem to be used anywhere so commenting it out
    Public Const LK_POSTPRE_PAID As String = "POSTPRE_PAID"
    Public Const LK_COMPUTE_DEDUCTIBLE_BASED_ON_AND_EXPRESSIONS As String = "COMPUTE_DEDUCTIBLE_BASED_ON_AND_EXPRESSIONS"
    Public Const LK_COLLECTION_CYCLE_TYPE As String = "COLLECTION_CYCLE_TYPE"
    Public Const LK_REPLACEMENT_BASED_ON As String = "REPLACEMENT_BASED_ON"
    Public Const LK_INTEGRATED_WITH As String = "INTEGRATED_WITH"
    Public Const LK_GVS_FUNCTION_TYPE As String = "GVS_FUNCTION_TYPE"
    Public Const LK_GVS_TRANSACTION_STATUS As String = "GVS_TRANSACTION_STATUS"
    Public Const LK_COMPANY As String = "COMPANIES"
    Public Const LK_COMPANY_CREDIT_CARDS_FORMAT As String = "COMPANY_CREDIT_CARDS_FORMAT"
    Public Const LK_COMPANY_CREDIT_CARDS As String = "COMPANY_CREDIT_CARDS"
    Public Const LK_GET_COMPANY As String = "GET_COMPANY"
    Public Const LK_COMPANY_GROUP As String = "COMPANY_GROUP"
    Public Const LK_COMPANY_GROUPS As String = "COMPANY_GROUPS"
    Public Const LK_COVERAGE_TYPES As String = "COVERAGE_TYPES"
    Public Const LK_NOTIICATION_TYPES As String = "NOTIFICATION_TYPES"
    Public Const LK_WHO_PAYS As String = "WHO_PAYS"
    Public Const LK_COVERAGE_TYPE_BY_COMPANY_GROUP As String = "COVERAGE_TYPE_BY_COMPANY_GROUP"
    Public Const LK_COVERAGE_TYPE_BY_DEALER As String = "COVERAGE_TYPE_BY_DEALER"
    Public Const LK_ITEM_COVERAGETYPE As String = "ITEM_COVERAGETYPE"
    Public Const LK_CERTIFICATEDURATION = "CERTIFICATEDURATION"
    Public Const LK_COVERAGEDURATION = "COVERAGEDURATION"
    Public Const LK_CLAIM_TYPES As String = "CLAIM_TYPES"
    Public Const LK_COLORS As String = "COLORS"
    Public Const LK_ACTION As String = "ACTION"
    Public Const LK_WQ_ACTION As String = "WQ_ACTION"
    Public Const LK_CLAIM_ACTIVITIES As String = "CLAIM_ACTIVITIES"
    Public Const LK_CLAIM_FORMAT As String = "CLAIM_FORMAT"
    Public Const LK_CERT_NUMBER_FORMAT As String = "CERT_NUMBER_FORMAT"
    Public Const LK_CAUSES_OF_LOSS As String = "CAUSES_OF_LOSS"
    Public Const LK_INCOME_RANGE As String = "INCOME_RANGE"
    Public Const LK_EARNING_PATTERN_STARTS_ON As String = "EPSO"
    Public Const LK_METHODS_OF_REPAIR As String = "METHODS_OF_REPAIR"
    Public Const LK_METHODS_OF_REPAIR_FOR_REPAIRS As String = "METHODS_OF_REPAIR_FOR_REPAIRS"
    Public Const LK_REASONS_CLOSED As String = "REASONS_CLOSED"
    Public Const LK_DENIED_REASON As String = "DENIED_REASON"
    Public Const LK_DEALER_DENIED_REASON As String = "DEALER_DENIED_REASON"
    Public Const LK_LOANER_CENTERS As String = "LOANER_CENTERS"
    Public Const LK_PAYMENT_TYPES As String = "PAYMENT_TYPES"
    Public Const LK_DOCUMENT_TYPES As String = "DOCUMENT_TYPES"
    Public Const LK_VALIDATION_TYPES As String = "VALIDATION_TYPES"
    Public Const LK_AUDIANCE_TYPES As String = "AUDIANCE_TYPES"
    Public Const LK_AUDIT_SOURCE As String = "AUDIT_SOURCE"
    Public Const LK_SYSTEM_NOTIFICATION_TYPES As String = "SYSTEM_NOTIFICATION_TYPE"
    Public Const LK_CURRENCY_TYPES As String = "CURRENCY_TYPES"
    Public Const LK_PRICE_LIST_DETAIL_TYPE As String = "PLDTYP"
    Public Const LK_VALIDATE_BANK_INFO As String = "VALIDATE_BANK_INFO"
    Public Const LK_CURRENCY_NOTATION_BY_COUNTRY As String = "CURRENCY_NOTATION_BY_COUNTRY"
    Public Const LK_CURRENCY_BY_COMPANY_AND_DEALERS_IN_COMPANY As String = "CURRENCY_BY_COMPANY_AND_DEALERS_IN_COMPANY"
    Public Const LK_CURRENCY_BY_COMPANIES_BY_USER As String = "CURRENCY_BY_COMPANIES_BY_USER"
    'Public Const LK_CREDIT_CARD_TYPES As String = "CREDIT_CARD_TYPES"
    Public Const LK_CREDIT_CARD_TYPES_1 As String = "CCTYPE"
    Public Const LK_COUNTRIES As String = "COUNTRIES"
    Public Const LK_CANCELLATION_REASONS As String = "CANCELLATION_REASONS"
    Public Const LK_CANCELLATION_REASONS_DEALER_FILE As String = "CANCELLATION_REASONS_DEALER_FILE"
    Public Const LK_CANCELLATION_REASONS_WITH_CODE As String = "CANCELLATION_REASONS_WITH_CODE"
    Public Const LK_REGIONS As String = "REGIONS"
    Public Const LK_COUNTRY_REGIONS As String = "COUNTRY_REGIONS"
    Public Const LK_REGION_TAX As String = "REGION_TAX"
    Public Const LK_REPAIR_CODES As String = "REPAIR_CODES"
    Public Const LK_SERVICE_ORDER_REPORTS As String = "SERVICE_ORDER_REPORTS"
    Public Const LK_INVOICE_METHOD As String = "INVOICE_METHOD"
    Public Const LK_FTP_SITE As String = "FTP_SITE"
    Public Const LK_USERS As String = "USERS"
    Public Const LK_COMMENT_TYPES As String = "COMMENT_TYPES"
    Public Const LK_DAYS_OF_WEEK As String = "DAYS_OF_WEEK"
    Public Const LK_WQ_HIST_ACTION As String = "WQ_HIST_ACTION"
    Public Const LK_CLM_IMG_STATUS As String = "CLM_IMG_STATUS"
    Public Const LK_TAX_TYPES As String = "TAX_TYPES"
    Public Const LK_RISK_GROUPS As String = "RISK_GROUPS"
    Public Const LK_CLAIM_SEARCH_FIELDS As String = "CLAIM_SEARCH_FIELDS"
    Public Const LK_ADJUSTER_CLAIM_SEARCH_FIELDS As String = "ADJUSTER_CLAIM_SEARCH_FIELDS"
    Public Const LK_NOTIFICATION_SEARCH_FIELDS As String = "NOTIFICATION_SEARCH_FIELDS"
    Public Const LK_HUB_REGIONS As String = "HUB_REGIONS"
    Public Const LK_REPAIR_LOGISTICS_SEARCH_FIELDS As String = "REPAIR_LOGISTICS_SEARCH_FIELDS"
    Public Const LK_CLAIM_SEARCH_BY_COMMENT_TYPE_FIELDS As String = "CLAIM_SEARCH_BY_COMMENT_TYPE"
    Public Const LK_PENDING_CLAIM_SEARCH_FIELDS As String = "PENDING_CLAIM_SEARCH_FIELDS"
    Public Const LK_PENDING_APPROVAL_CLAIM_SEARCH_FIELDS As String = "PENDING_APPROVAL_CLAIM_SEARCH_FIELDS"
    Public Const LK_CERTIFICATE_SEARCH_FIELDS As String = "CERTIFICATE_SEARCH_FIELDS"
    Public Const LK_CLAIM_FOLLOWUP_SEARCH_FIELDS As String = "CLAIM_FOLLOWUP_SEARCH_FIELDS"
    Public Const LK_INVOICE_SEARCH_FIELDS As String = "INVOICE_SEARCH_FIELDS"
    Public Const LK_ACCOUNTING_CLOSING_YEARS As String = "ACCOUNTING_CLOSING_YEARS"
    Public Const LK_MONTHS As String = "MONTHS"
    Public Const LK_EXTERNAL_USER_TYPES As String = "EXTERNAL_USER_TYPE"
    Public Const LK_REPLACEMENT_POLICIES As String = "REPLACEMENT_POLICIES"
    Public Const LK_SALUTATION As String = "SALUTATION"
    Public Const LK_SALUTATION_LANGUAGE As String = "SALUTATION_LANGUAGE"
    Public Const LK_MEMBERSHIP_TYPE_LANGUAGE As String = "MEMBERSHIP_TYPE_LANGUAGE"
    Public Const LK_PAYMENTMETHOD As String = "PAYMENT_METHOD"

    Public Const LK_CLAIMRESERVED As String = "CLAIMRESERVED"
    Public Const LK_PAYMENTMETHOD_BY_ROLE As String = "PAYMENT_METHOD_BY_ROLE"
    Public Const LK_PAYMENTMETHOD_WITH_OUT_BANKTRANSFER As String = "LK_PAYMENTMETHOD_WITH_OUT_BANKTRANSFER"
    Public Const LK_PAYMENTREASON As String = "PAYMENT_REASON"
    Public Const LK_ACCTSTATUS As String = "ACCT_STATUS"
    Public Const LK_CRST = "CRST"
    Public Const LK_ACCOUNT_TYPES As String = "ACCOUNT_TYPES"
    Public Const LK_SUBSCRIBER_STATUS As String = "SUBSTAT"
    Public Const LK_CONTRACT_TYPES As String = "CONTRACT_TYPES"
    Public Const LK_PAYEE As String = "PAYEE"
    Public Const LK_PAYEE_TYPE As String = "PAYEE_TYPE"
    Public Const LK_COMM_SCHL As String = "COMM_SCHL"
    Public Const LK_COINSURANCE As String = "COINS"
    Public Const LK_USER_COMPANIES As String = "USER_COMPANIES"
    Public Const LK_USER_COMPANY_GROUPS As String = "GET_USER_COMPANY_GROUPS"
    Public Const LK_USER_COUNTRIES As String = "USER_COUNTRIES"
    Public Const LK_USER_COUNTRIES_FOR_ALL_COMPANIES As String = "USER_COUNTRIES_FOR_ALL_COMPANIES"
    Public Const LK_COMPANY_GROUP_COUNTRIES As String = "COMPANY_GROUP_COUNTRIES"
    Public Const LK_COTYP As String = "COTYP"
    Public Const LK_MASTERCLAIMPROC As String = "MASTERCLAIMPROC"
    Public Const LK_COMPANY_TYPE As String = "COMPANY_TYPE"
    Public Const LK_PRODUCT_TAX_TYPE As String = "PTT"
    Public Const LK_IHQ_ROLES As String = "IHQ_ROLES"
    Public Const LK_SPLIT_SYSTEM_TRANSLATIONS As String = "SPLIT_SYSTEM_TRANSLATIONS"
    Public Const LK_SPLIT_SYSTEM_CODE As String = "SPLIT_SYSTEM_CODE"
    Public Const LK_ALL_LANGUAGE_LISTS As String = "ALL_LANGUAGE_LISTS"
    Public Const LK_LIST_LANGUAGE_ITEMS As String = "LIST_LANGUAGE_ITEMS"
    Public Const LK_LIST_ITEMS As String = "LIST_ITEMS"
    Public Const LK_LIST_ID As String = "LIST_ID"
    Public Const LK_LIST As String = "LIST"
    Public Const LK_EXTOWN As String = "EXTOWN"
    Public Const LK_LIST_ITEM_ID As String = "LIST_ITEM_ID"
    Public Const LK_ROLES_ALL As String = "ROLES_ALL"
    Public Const LK_TABS_ALL As String = "TABS_ALL"
    Public Const LK_DEALERS_PROD_CONV_DUAL_COLUMNS As String = "DEALERS_PRODUCT_CONVERSIONS_DUAL"
    Public Const LK_DEALERS_PROD_CONV As String = "DEALERS_PRODUCT_CONVERSIONS"
    Public Const LK_DEALERS_COMM_PROD As String = "DEALERS_COMM_PROD"
    Public Const LK_DEALERS_GROUPS_BY_COMPANY_DUAL_COLUMNS As String = "DEALERS_GROUPS_BY_COMPANY_DUAL_COLUMNS"
    Public Const LK_DEALERS_GROUPS_BY_COMPANY As String = "DEALERS_GROUPS_BY_COMPANY"
    Public Const LK_CAMPAIGN_NUMBERS As String = "CAMPAIGN_NUMBERS"
    Public Const LK_YESNO As String = "YES_NO"
    Public Const LK_REFUND_DESTINATION As String = "REFUND_DESTINATION"
    Public Const LK_COUNTRY_BY_COMPANY As String = "COUNTRY_BY_COMPANY"
    Public Const LK_IBNR_COMPUTE_METHODS As String = "IBNR"  'this is GAAP IBNR
    Public Const LK_STAT_IBNR_COMPUTE_METHODS As String = "STAT_IBNR"
    Public Const LK_LAE_IBNR_COMPUTE_METHODS As String = "LAE_IBNR"
    Public Const LK_SERIAL_NUMBER_VALIDATION As String = "SNV"
    Public Const LK_CERT_CANCEL_BY As String = "CCANBY"
    Public Const LK_REQUIRE_CUSTOMER_AML_INFO As String = "CAIT"

    Public Const LK_EARNING_CODES As String = "EARNING_CODES"
    Public Const LK_COMMISSION_ENTITIES As String = "COMMISSION_ENTITIES"
    Public Const LK_COVERAGE_TYPES_NOT_IN_COV_LOSS As String = "COVERAGE_TYPES_NOT_IN_COV_LOSS"
    Public Const LK_COVERAGE_TYPES_BY_COMPANY_GROUP_NOT_IN_COV_LOSS As String = "COVERAGE_TYPES_BY_COMPANY_GROUP_NOT_IN_COV_LOSS"
    Public Const LK_CAUSE_OF_LOSS_BY_COVERAGE_TYPE As String = "CAUSE_OF_LOSS_BY_COVERAGE_TYPE"
    Public Const LK_DEALER_TYPE_CODE As String = "DLTYP"
    Public Const LK_OLITA_SEARCH As String = "OLTASRCH"
    Public Const LK_DEALER_TYPE As String = "DEALER_TYPE"
    Public Const LK_CONTRACT_END_PERIOD_CODE As String = "ENDP"
    Public Const LK_CONTRACT_END_PERIOD As String = "END_PERIOD"
    Public Const LK_COMPANY_GROUP_CLAIM_NUMBERING As String = "COMPANY_GROUP_CLAIM_NUMBERING"
    Public Const LK_VSC_MANUFACTURERS As String = "MANUFACTURERS_BY_COMPANY_GROUP"
    Public Const LK_VSC_MODELS As String = "MODELS_BY_MANUFACTURERS"
    Public Const LK_VSC_YEARS As String = "YEARS_BY_TRIM"
    Public Const LK_VSC_TRIMS As String = "TRIMS_BY_MODEL"
    Public Const LK_BILLING_STATUS As String = "BILLING_STATUS"
    Public Const LK_REJECT_CODES As String = "REJECT_CODES"
    Public Const LK_CLAIM_SYSTEM As String = "CLAIM_SYSTEM"
    Public Const LK_VSC_PLAN_BY_COMPANY_GROUP_ID As String = "VSC_PLAN_BY_COMPANY_GROUP_ID"
    Public Const LK_DEALER_TYPE_COMPANY_GROUP_ID As String = "DEALER_TYPE_BY_COMPANY_GROUP_ID"
    Public Const LK_COMPANIES_COUNTRY_ID As String = "COMPANIES_BY_COUNTRY_ID" 'REQ-5980
    Public Const LK_DEALERS_COUNTRY_ID As String = "DEALERS_BY_COUNTRY_ID" 'REQ-5980
    Public Const LK_PRODUCTCODE_COUNTRY_ID As String = "PRODUCTCODE_BY_COUNTRY_ID" 'REQ-5980
    Public Const LK_VSC_CLASS_CODES_BY_COMPANY_GROUP As String = "VSC_CLASS_CODES_BY_COMPANY_GROUP"
    Public Const LK_ORIGINAL_DEALER_LIST As String = "ORIGINAL_DEALER_LIST"
    Public Const LK_PLANS As String = "LK_VSC_PLAN_ID"
    Public Const LK_COVERAGE_LIMIT As String = "LK_VSC_COVERAGE_LIMIT"
    Public Const LK_ACCT_FIELD_TYPE As String = "ACCTFIELDTYP"
    Public Const LK_ACCT_TRANS_TYPE As String = "ACCTTRANSTYP"
    Public Const LK_ADDL_DAC As String = "ADDL_DAC"
    Public Const LK_ADDL_DAC_CODE As String = "ADDAC"
    Public Const LK_FREQ As String = "FREQ"
    Public Const LK_CREDIT_DEBIT As String = "CRDBT"
    Public Const LK_RISKGROUPS As String = "LK_RISK_GROUPS"
    Public Const LK_RISK_TYPES As String = "LK_RISK_TYPES"
    Public Const LK_CANCELLATION_REASONS_BY_DEALER As String = "CANCELLATION_REASONS_BY_DEALER"
    Public Const LK_SERVICE_CENTER_STATUS As String = "SERVICE_CENTER_STATUS"
    Public Const LK_COMMISSION_TYPE As String = "LK_COMMISSION_TYPE"
    Public Const LK_DEALE_BROKER As String = "LK_DEALE_BROKER"
    Public Const LK_COMPANY_GROUP_INVOICE_NUMBERING As String = "COMPANY_GROUP_INVOICE_NUMBERING"
    Public Const LK_TRANSLATE_PRODUCT_CODE As String = "TPRDC"
    Public Const LK_MSG_TYPE As String = "MSGTYPE"
    Public Const LK_CATEGORIES As String = "CATEGORIES"
    Public Const LK_REPORT As String = "REPORT"
    Public Const LK_CLIPMETHOD As String = "CLIPMETHOD"
    Public Const LK_TIME_ZONE_NAMES As String = "TZN"
    Public Const LK_COMPUTE_TAX_BASED As String = "COMTAXBASED"
    Public Const LK_COLLECTION_METHODS As String = "COLLMETHOD"
    Public Const LK_PAYMENT_INSTRUMENT As String = "PMTINSTR"
    Public Const LK_INSTALLMENT_DEFINITION As String = "INSTALLMENT_DEFINITION"
    Public Const LK_INSTDEF As String = "INSTDEF"
    Public Const LK_DEDUCTIBLE_BASED_ON As String = "COMDEDUCTBASED"
    Public Const LK_CLAIM_LOAD_FILE_TYPE As String = "CLMLDFTYP"
    Public Const LK_AUTH_AMT_BASED_ON As String = "AUTHABO"
    Public Const LK_AUTH_DTL As String = "AUTHDTL"
    Public Const LK_INVOICE_STATUS As String = "INV_STAT"
    Public Const LK_CLAIM_AUTHORIZATION_STATUS As String = "CLM_AUTH_STAT"
    Public Const LK_CLITYP As String = "CLITYP"
    Public Const LK_CAIT As String = "CAIT"
    Public Const LK_CertNumLK As String = "CL"
    Public Const LK_AFA_PRODUCT_TYPE As String = "AFAPT"
    'REQ-1142
    Public Const LK_LICENSE_TAG_FLAG As String = "LCNSTAG"
    Public Const LK_INACTIVATE_NEW_VEHICLE As String = "INCVTNEWVHCL"
    'REQ-1142 End
    Public Const LK_PRICE_LIST_DETAIL As String = "PLDTYP"
    Public Const LK_USER_DEFINED_CODE As String = "UD"
    Public Const LK_TEMPLATE_CODE As String = "T"

    'Accounting Lists
    Public Const LK_ACCOUNTING_EVENTS As String = "ACCOUNTING_EVENTS"
    Public Const LK_ACCOUNTING_COMPANIES As String = "ACCOUNTING_COMPANIES"
    Public Const LK_ACCOUNTING_FILENAMES As String = "ACCOUNTING_FILENAMES"
    Public Const LK_ACCTBO As String = "ACCTBO"
    Public Const LK_ALLOCMRK As String = "ALLOCMRK"
    Public Const LK_YESNO_NUM As String = "YESNO_NUM"
    Public Const LK_YESNO_EXT As String = "YESNO"
    Public Const LK_POSTINGTYPE As String = "POSTTYPE"
    Public Const LK_ACCT_SYSTEM As String = "ACCTSYS"
    Public Const LK_ACCOUNTING_EVENT_TYPE As String = "ACCTTRANSTYP"
    Public Const LK_ACCOUNTING_PROCESS_METHOD As String = "ACCTPROC"
    Public Const LK_ACCOUNTING_TRANS_STATUS As String = "ACCTSTAT"
    Public Const LK_ACCOUNTING_PAYMENT_METHOD As String = "ACCTPMTMETH"
    Public Const LK_ACCOUNTING_ANALYSIS_SOURCE As String = "ACCTTCODE"
    Public Const LK_ACCOUNTING_DESC_SOURCE As String = "ACCTDESC"

    Public Const LK_ANYLSCODE As String = "ANYLSCODE"
    Public Const LK_CRDBT_NUM As String = "CRDBT_NUM"
    Public Const LK_CONVCTL As String = "CONVCTL"
    Public Const LK_VENSTAT As String = "VENSTAT"
    Public Const LK_PMTHD_NUM As String = "PMTHD_NUM"
    Public Const LK_PAPAT As String = "PAPAT"
    Public Const LK_PAYMENT_TERMS As String = "PMTTRM"
    Public Const LK_BUSINESS_ENTITY As String = "BENT"
    Public Const LK_BUSINESS_ENTITY_COV As String = "BENTCOV"
    Public Const LK_CHECK_REPORT_RUNDATE As String = "CHECK_REPORT_RUNDATE"
    Public Const LK_REASON_CODE As String = "REASON_CODE"
    Public Const LK_REFUND_COMP_METHOD As String = "REFUND_COMP_METHOD"

    'End Accounting Form Lists

    'Translate Product Codes 
    Public Const LK_TRANSLATE_PRODUCT_CODES As String = "TRANSLATE_PRODUCT_CODES"

    'WS
    Public Const LK_EXTENDED_CLAIM_STATUSES As String = "EXTENDED_CLAIM_STATUSES"
    Public Const LK_SERVICE_CENTERS_DEALER As String = "SERVICE_CENTERS_DEALER"
    Public Const LK_EXTENDED_STATUS As String = "EXTENDED_STATUS"
    Public Const LK_EXTENDED_STATUS_BY_GROUP_LIST As String = "EXTENDED_STATUS_BY_GROUP_LIST"
    Public Const LK_SC_TAT_BY_GROUP_LIST As String = "SC_TAT_BY_GROUP_LIST"
    Public Const LK_SORT_ORDER As String = "SORT_ORDER"
    Public Const LK_EXTENDED_CLAIM_STATUS_OWNER As String = "EXTENDED_CLAIM_STATUS_OWNER"
    '5623
    Public Const LK_EXTENDED_STATUS_BY_USER_ROLE_LIST As String = "EXTENDED_STATUS_BY_USER_ROLE"
    'TransAll
    Public Const LK_TRANSALL_LAYOUT_CODES As String = "TALAYOUT"

    'REQ-490 - Eidt Mfg Coverage Changes
    Public Const LK_EDTMFGTRM As String = "EDTMFGTRM"
    Public Const LK_PRODUCTCODE_BY_DEALER As String = "PRODUCTCODE_BY_DEALER"
    Public Const LK_OCCURANCES_ALLOWED As String = "OCCURANCES_ALLOWED"
    Public Const LK_PRICE_GROUP_DP As String = "PRICE_GROUP_DP"

    Public Const LK_EQUIPMENTS As String = "EQUIPMENTS"
    Public Const LK_EQUIPMENT_BY_EQUIPMENTlIST As String = "EQUIPMENTS_BY_EQUIPMENTLIST"
    Public Const LK_BEST_REPLACEMENT As String = "BEST_REPLACEMENT"
    Public Const LK_EQUIPMENT_LIST As String = "EQUIPMENT_LIST"
    Public Const LK_EQUIPMENT_MAKE_MODEL As String = "EQUIPMENT_MAKE_MODEL"
    Public Const LK_EQUIPMENT_LIST_FOR_PRICE_LIST As String = "EQUIPMENT_LIST_FOR_PRICE_LIST"

    Public Const LK_BEST_REPLACEMENT_BY_COMPANY_GROUP As String = "BEST_REPLACEMENT_BY_COMPANY_GROUP"
    Public Const LK_EQUIPMENT_CLASS As String = "EQPCLS"
    Public Const LK_EQUIPMENT_TYPE As String = "EQPTYPE"
    Public Const LK_IMAGE_TYPE As String = "IMGTYP"
    Public Const LK_ATTRIBUTE_DATA_TYPE As String = "ATBDTYP"
    Public Const LK_ATTRIBUTE As String = "ATTRIBUTE"
    Public Const LK_CLAIM_EQUIPMENT_TYPE As String = "CLAIM_EQUIP_TYPE"

    Public Const LK_CAUSE_OF_LOSS_BY_COVERAGE_TYPE_AND_SPL_SVC As String = "CAUSE_OF_LOSS_BY_COVERAGE_TYPE_AND_SPL_SVC"

    Public Const LK_SOURCE As String = "SOURCE"

    Public Const LK_ANSWER_TYPE As String = "ANSWER_TYPE"
    Public Const LK_QUESTION As String = "QUESTION"
    Public Const LK_ANSWER As String = "ANSWER"
    Public Const LK_ENTITY_ATTRIBUTE As String = "ENTITY_ATTR"

    Public Const LK_CLAIM_EXTENDED_STATUS_ENTRY As String = "CLAIM_EXTENDED_STATUS_ENTRY"
    Public Const LK_CLAIM_EXTENDED_STATUS_ENTRY_LIST_CODE As String = "EXTSTATENT"

    'REQ-860 - Elita Buildout - Issues/Adjudication
    Public Const LK_QUESTION_TYPE As String = "QTYP"
    Public Const LK_QUESTION_LIST As String = "QUESTION_LIST"
    Public Const LK_RULE_TYPE As String = "RTYPE"
    Public Const LK_RULE_CATEGORY As String = "RCAT"

    Public Const LK_ISSUES As String = "ISSUES"
    Public Const LK_ISSUE_TYPE As String = "ISSTYP"
    Public Const LK_NOTE_TYPE As String = "ISSNOTE"
    Public Const LK_ISSUE_TYPE_LIST As String = "ISSUE_TYPE"
    Public Const LK_ISSUE_TYPE_CODE_LIST As String = "ISSUE_TYPE_CODE"
    Public Const LK_NOTE_TYPE_LIST As String = "NOTE_TYPE"

    Public Const LK_RISK_TYPE As String = "RISK_TYPE"
    Public Const LK_COVERAGE_TYPE As String = "COVERAGE_TYPE"

    'Req-1057 ClaimIssues
    Public Const LK_CLAIM_ISSUE_STATUS As String = "CLMISSUESTATUS"
    Public Const LK_ISSUE_REOPEN_REASON As String = "ISSREOPNRSN"
    Public Const LK_ISSUE_WAIVE_REASON As String = "ISSWAIVERSN"

    'Req-1016 Start
    Public Const LK_PERIOD_RENEW As String = "PERIODRENEW"
    'Req-1016 end

    Public Const LK_CLAIM_STATUS As String = "CLSTAT"
    Public Const LK_CERTIFICATE_STATUS As String = "CSTAT"

    'REQ-1198
    Public Const LK_FUTURE_DATE_ALLOW_FOR As String = "FUTURE_DATE_ALLOW_FOR"
    Public Const LK_FUTURE_DATE_ALLOW_FOR_CODE As String = "FDAF"

    'REQ-5405
    Public Const LK_COMPUTE_METHOD As String = "COMPUTE_METHOD"
    Public Const LK_COMPUTE_METHOD_CODE As String = "MCM"

    ' Added as part of Vendor Mgmt REQ 858
    Public Const LK_SERVICE_CLASS As String = "SVCCLASS"
    Public Const LK_SERVICE_LEVEL As String = "SVC_LVL"
    Public Const LK_SERVICE_TYPE As String = "STYP"
    Public Const LK_SERVICE_TYPE_NEW As String = "SVCTYP"
    Public Const LK_SERVICE_TYPE_BY_SERVICE_CLASS_NEW As String = "SVC_TYP_BY_CLASS"
    Public Const LK_DIST_METHOD As String = "DISTMETH"
    Public Const LK_ADJUSTMENT_REASON As String = "ADJ_RESN"
    Public Const LK_MANAGE_INVENTORY As String = "MNGINVENT"
    Public Const LK_SCHEDULE As String = "SCHEDULE"
    Public Const LK_CONDITION As String = "TEQP"
    Public Const LK_PRICE_LIST As String = "PRICE_LIST"

    Public Const LK_DED_COLL_METHOD As String = "DED_COLL_METHOD"

    'Req-1154 start
    Public Const LK_REGSTATUS As String = "REGSTATUS"
    Public Const LK_EVNT_TYPE = "EVNT_TYP"
    'Req-1154 end

    'REQ 1194
    Public Const LK_CERT_RISKTYPES As String = "CERT_RISK_TYPES"
    'REQ-1194

    Public Const LK_TASK_STATUS As String = "TASK_STATUS"
    Public Const LK_CLAIM_AUTHORIZATION_TYPE As String = "CLM_AUTH_TYP"
    'REQ 1155
    Public Const LK_SKU_VALIDATION_CODE = "SKUVAL"
    'end

    'REQ-863
    Public Const LK_COMPANY_GROUP_INVOICE_GRP_NUMBERING As String = "COMPANY_GROUP_INV_GRP_NUMBERING"
    Public Const LK_COMPANY_GROUP_AUTHORIZATION_NUMBERING As String = "COMPANY_GROUP_AUTHORIZATION_NUMBERING"
    Public Const LK_COMPANY_GROUP_PAYMENT_GROUP_NUMBERING As String = "PMTGRPNUM"
    Public Const LK_DEVICE As String = "DEVICE"
    'REQ-863
    'Claims Payable
    Public Const LK_PAYMENT_GRP_STAT As String = "PMTGRPSTAT"
    Public Const LK_INV_RECON_STAT As String = "INVRECONSTAT"

    'DEF-2855
    Public Const LK_ISSUE_DESCRIPTION As String = "ISSUE_DESCRIPTION"

    'REQ - 1106
    Public Const LK_MANUFACTURER_BY_EQUIPMENT_LIST As String = "GET_MANUFACTURER_BY_EQUIPMENT_LIST"
    'Req-1297
    Public Const LK_USE_FULLFILE_PROCESS As String = "USE_FULLFILE_PROCESS"
    Public Const LK_FLP = "FLP"
    'Req-1297 End

    'REQ-1255 - START
    Public Const LK_MARITALSTATUS As String = "MARITAL_STATUS"
    Public Const LK_NATIONALITY As String = "NATIONALITY"
    Public Const LK_PLACEOFBIRTH As String = "PLACEOFBIRTH"
    Public Const LK_GENDER As String = "GENDER"
    'REQ-1255 - END

    Public Const LK_UPGRADE_TERM_UNIT_OF_MEASURE As String = "UPG_TERM_UNIT_OF_MEASURE"
    Public Const LK_UPG_FINANCE_INFO_REQUIRE As String = "UPG_FINANCE_INFO_REQUIRE"

    Public Const LK_PERSON_TYPE As String = "PERSON_TYPE"

    'Req- 5416 Start
    Public Const LK_REPORT_ERR_REJ_TYPE As String = "AUTO_REJ_ERR_TYPE"

    'Req -5416 End

    'Req- 5416 Start
    Public Const LK_PROD_CONV_TYPE As String = "TPRDC"

    'Req -5416 End

    Public Const LK_DEALER_EXTRACT_PERIOD As String = "DEALER_EXTRACT_PERIOD"

    'REQ-5565
    Public Const LK_PRE_INVOICE_STATUS As String = "PREINVSTAT"

    'req 5547
    Public Const LK_FAST_APPROVAL_TYPE As String = "FSTAPRVL"

    'REQ-5546
    Public Const LK_CLAIM_EXTENDED_STATUS_DEFAULT_TYPES As String = "CLAIM_EXTENDED_STATUS_DEFAULT_TYPES"
    'REQ-5586
    Public Const LK_PROD_LIABILITY_LIMIT_BASED_ON_TYPES As String = "PROD_LIABILITY_LIMIT_BASED_ON_TYPES"
    Public Const LK_PROD_LIABILITY_LIMIT_POLICY_TYPES As String = "PROD_LIABILITY_LIMIT_POLICY_TYPES"
    Public Const LK_INVSTAT As String = "INVSTAT" 'Req-5615
    Public Const LK_INVTYP As String = "INVTYP" 'Req-5615

    'REQ-5598
    Public Const LK_CLAIM_RULSE_BASED_ON As String = "CLRULEBASEDON"

    Public Const LK_UPG_FINANCE_BAL_COMP_METH As String = "UPG_FINANCE_BAL_COMP_METH"

    Public Const LK_COV_EXT_CODE As String = "COVEXT"
    Public Const LK_COVERAGE_EXTENSIONS As String = "COVERAGE_EXTENSIONS"
    Public Const LK_DATE_OF_PAYMENT_OPTION As String = "DATE_OF_PAYMENT_OPTION"
    Public Const LK_NUMBER_OF_DIGITS_ROUNDOFF As String = "NUMBER_OF_DIGITS_ROUNDOFF"

    Public Const LK_STAGES As String = "STAGES"

    'REQ-5480
    Public Const LK_SHIPPING_TYPES = "SHIPPING_TYPES"

    'REQ-5464
    Public Const LK_FILE_TYPE = "FILE_TYP"

    'REQ-5623


    Public Const LK_GET_USER_COMPANIES As String = "GET_USER_COMPANIES"
    Public Const LK_GET_CLAIM_STATUS_BY_USER_ROLE As String = "GET_CLAIM_STATUS_BY_USER_ROLE"

    'REQ-5723
    Public Const LK_VRSTID As String = "VRSTID"
    Public Const LK_PLAN_QUOTE_IN_QUOTE_OUTPUT As String = "PLNQT"
    Public Const LK_AUTO_GEN_REJ_PYMT_FILE = "AUTO_GEN_REJ_PYMT_FILE"
    Public Const LK_FILE_TYP_PAYMENT As String = "PYMT_FILE"

    'REQ-5773
    Public Const LK_COMM_ENTITY_TYPE_ID As String = "CET"
    Public Const LK_PPT As String = "PPT"

    'REQ-5578
    Public Const LK_BONUS_COMPUTATION_METHOD As String = "CBCM"

    '5750
    Public Const LK_REINSURANCE_STATUSES As String = "REINSURANCE_STATUSES"
    Public Const LK_POST_MIG_CONDITION As String = "POST_MIG_CONDITIONS"
    Public Const LK_REINS_STATUSES_WITHOUT_PARTIAL_STATUSES As String = "REINS_STATUSES_WITHOUT_PARTIAL_STATUSES"
    Public Const LK_REINS_STATUS_REJECTED As String = "REJECTED"
    Public Const LK_REINS_REC_TYPE As String = "REINSRECTYP"

    'REQ-5761
    Public Const LK_ALLOW_CC_REJECTIONS As String = "ALLOW_CC_REJECTIONS"
    Public Const LK_ALLOW_CC_REJECTIONS_CODE As String = "ACCR"

    Public Const LK_PROD_REWARD_NAMES As String = "PROD_REWARD_NAMES"
    Public Const LK_PROD_REWARD_TYPES As String = "PROD_REWARD_TYPES"
    Public Const LK_DEVICE_GROUPS As String = "DEVICE_GROUPS"
    Public Const LK_DEVICE_TYPES As String = "DEVICE_TYPES"

    'REQ-6002
    Public Const LK_UPDATE_REPLACE__REG_ITEMS As String = "UPDATE_REPLACE__REG_ITEMS"

    'REQ-6155
    Public Const LK_CASE_SEARCH_FIELDS As String = "CASE_SEARCH_FIELDS"
    Public Const LK_CASE_STATUS As String = "CASE_STATUS"
    Public Const LK_CASE_PURPOSE As String = "CASE_PURPOSE"
    Public Const LK_RELATION_TYPE As String = "CUSTOMER_TYPE"
    Public Const LK_CASE_CLOSE_REASON As String = "CASE_CLOSE_REASON"


    Public Const LK_REWARD_SEARCH_FIELDS As String = "REWARD_SEARCH_FIELDS"
    'REQ
    Public Const LK_DCM_QUESTION_SET As String = "DCM_QUESTION_SET"

    Public Const LK_COVERAGE_PERIL_TYPE As String = "COVERAGE_PERIL_TYPE"

    'REQ 6289
    Public Const LK_PRODUCT_LIABILITY_LIMIT_BASE = "PRODUCT_LIABILITY_LIMIT_BASE"
    Public Const LK_COVERAGE_CERT_LIFETIME_TYPE As String = "CERTLIFETIME"
    Public Const LK_COVERAGE_RENEW_BY_EFFDT_TYPE As String = "RENEWBYEFFDT"
    Public Const LK_COVERAGE_RENEW_BY_DOL_TYPE As String = "RENEWBYDOL"
    Public Const LK_COVERAGE_NOT_APPLICABLE_TYPE As String = "NOTAPPL"
    Public Const LK_PRODUCT_LIMIT_APP_TO_TYPES As String = "PRODUCT_LIMIT_APP_TO_TYPES"
    Public Const LK_PROD_LIMIT_APPLICABLE_TO_ALL As String = "PRODLIMITAPPTO-ALL"
    Public Const LK_PROD_LIMIT_APPLICABLE_TO_LIABILITYONLY As String = "PRODLIMITAPPTO-LIABILITY"
    Public Const LK_PROD_LIMIT_APPLICABLE_TO_CLAIMONLY As String = "PRODLIMITAPPTO-CLAIMCOUNT"

    'REQ-6313
    Public Const LK_PARAM_DATA_TYPE As String = "OCTPT"
    Public Const LK_PARAM_VALUE_SOURCE As String = "OCTPS"
    Public Const LK_YESNO_XCD As String = "YESNO_XCD"
    Public Const LK_RECIPIENT_SOURCE_FIELD As String = "OCTR"

    'REQ-6156
    Public Const LK_ISSUES_TYPES As String = "ISSUES_TYPES"
    Public Const LK_AUTH_FUL_TYPE As String = "AUTH_FUL_TYPE"
    Public Const LK_DEPRECIATION_SCHEDULE_USAGE As String = "DEP_SCH_USAGE"

    'Product-Benefits
    Public Const LK_PRODUCT_BENEFITS_ACTION As String = "BENEFITS_ELIGIBLE_ACTION"

    'Thunder
    Public Const LK_BENEFIT_TAX_TYPES As String = "LK_BENEFIT_TAX_TYPES"
    Public Const LK_UNIT_OF_MEASURE As String = "LK_UNIT_OF_MEASURE"

    Public Const LK_EXTRACT_TYPE_LIST As String = "EXTRACT_TYPE_LIST"

    'REQ-6230
    Public Const LK_RETAIL_PRICE_SEARCH_SORTBY_LIST As String = "RETAIL_PRICE_SEARCH_SORTBY_LIST"

    Public Const LK_TAX_COMPUTE_METHOD As String = "LK_TCOMP"

    Public Const LK_GET_COMPANY_GROUPS As String = "GET_COMPANY_GROUPS"

    'Contract Policy Type (collective or individual)
    Public Const LK_CONTRACT_POLICY_TYPE As String = "POLTYPE"
    Public Const LK_CONTRACT_POLICY_GEN_TYPE As String = "POLGEN"
    Public Const LK_COUNTRY_LINE_OF_BUSINESS As String = "COUNTRY_LINE_OF_BUSINESS"

    Public Const LK_DESIRED_DELIVERY_TIME_SLOT As String = "LK_DESIRED_DELIVERY_TIME_SLOT"

    Public Const LK_CLAIM_PAY_DEDUCTIBLE As String = "CLAIM_PAY_DEDUCTIBLE"

    Public Const LK_JOURNALLEVEL As String = "JOURNALLEVEL"

    'US 489857
    Public Const LK_ACCTBUCKETSSOURCEOPTION As String = "ACCTBUCKETSSOURCEOPTION"
#End Region

End Class
