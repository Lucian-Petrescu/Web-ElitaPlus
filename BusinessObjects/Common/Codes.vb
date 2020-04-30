Public Class Codes
    ' Claim Issue Status
    Public Const CLAIM_ISSUE_STATUS As String = "CLMISSUESTATUS"
    Public Const CLAIM_ISSUE_STATUS__WAIVED As String = "WAIVED"
    Public Const CLAIM_ISSUE_STATUS__RESOLVED As String = "RESOLVED"
    Public Const CLAIM_ISSUE_STATUS__REJECTED As String = "REJECTED"
    Public Const CLAIM_ISSUE_STATUS__PENDING As String = "PENDING"
    Public Const CLAIM_ISSUE_STATUS__OPEN As String = "OPEN"
    Public Const CLAIM_ISSUE_STATUS__CLOSED As String = "CLOSED"

    ' Event Code
    'Public Const EVENT_CODE__MECHANICAL_BREAKDOWN = "MECHANICAL_BREAKDOWN"
    'Public Const EVENT_CODE__DEDUCTIBLE_COLLECTED = "DEDUCTIBLE_COLLECTED"
    'Public Const EVENT_CODE__DEDUCTIBLE_NOT_COLLECTED = "DEDUCTIBLE_NOT_COLLECTED"
    'Public Const EVENT_CODE__DEDUCTIBLE_WAIVED = "DEDUCTIBLE_WAIVED"
    'Public Const EVENT_CODE__DEVICE_NOT_RECEIVED = "DEVICE_DENIED"
    'Public Const EVENT_CODE__DEVICE_WAIVED = "DEVICE_WAIVED"
    'Public Const EVENT_CODE__DIAGNOSTIC_COMPLETE = "DEVICE_RESOLVED"
    'Public Const EVENT_CODE__POLICE_REPORT_RECEIVED = "POLICE_REPORT_RESOLVED"
    'Public Const EVENT_CODE__POLICE_REPORT_NOT_RECEIVED = "POLICE_REPORT_REJECTED"
    'Public Const EVENT_CODE__POLICE_REPORT_WAVED = "POLICE_REPORT_WAVED"
    Public Const DEFAULT_CLAIM_INVOICE_NUMBER As String = "DEFAULT_CLAIM_INVOICE_NUMBER"
    Public Const AUTO_POPULATE_CERT_TAX_ID As String = "AUTO_POPULATE_CERT_TAX_ID"
    Public Const DEFAULT_CLAIM_BANK_SORT_CODE As String = "DEFAULT_CLAIM_BANK_SORT_CODE"
    Public Const DEFAULT_CLAIM_BANK_SUB_CODE As String = "DEFAULT_CLAIM_BANK_SUB_CODE"

    Public Const CLM_STAT__COM_FB_CLM_UPD_DMG As String = "COM_FB_CLM_UPD_DMG"
    Public Const CLM_STAT__COM_FB_CLM_UPD_THFT As String = "COM_FB_CLM_UPD_THFT"
    Public Const EXTENDED_CLAIM_STATUSES As String = "EXTENDED_CLAIM_STATUSES"
    Public Const EVNT_TYP As String = "EVNT_TYP"
    Public Const EVNT_TYP__ISSUE_OPENED As String = "ISSUE_OPENED"
    Public Const EVNT_TYP__ISSUE_RESOLVED As String = "ISSUE_RESOLVED"
    Public Const EVNT_TYP__ISSUE_REJECTED As String = "ISSUE_REJECTED"
    Public Const EVNT_TYP__ISSUE_CLOSED As String = "ISSUE_CLOSED"
    Public Const EVNT_TYP__ISSUE_PENDING As String = "ISSUE_PENDING"
    Public Const EVNT_TYP__ISSUE_WAIVED As String = "ISSUE_WAIVED"
    Public Const EVNT_TYP__DEVICE_RECEPTION_DATE_RECEIVED As String = "DEVICE_RECEPTION_DATE_RECEIVED"
    Public Const EVNT_TYP__SHIPPED_TO_SERVICE_CENTER As String = "SHIPPED_TO_SERVICE_CENTER"
    Public Const EVNT_TYP__PRD_SHPD_C As String = "PRD_SHPD_C"
    Public Const EVNT_TYP__ALL_ISSUES_RESOLVED_BUDGET_APPROVED As String = "ALL_ISSUES_RESOLVED_BUDGET_APPROVED"
    Public Const EVNT_TYP__CLM_EXT_STATUS As String = "CLM_EXT_STATUS"
    Public Const EVNT_TYP__CUST_DEVICE_RECEPTION_DATE As String = "CUST_DEVICE_RECEPTION_DATE"
    Public Const EVNT_TYP_CRT_ENROLL As String = "CRT_ENROLL"
    Public Const EVNT_TYP_CRT_RESEND_WELCOME_PACK As String = "CRT_RESEND_WELCOME_PACK"
    Public Const EVNT_TYP_CRT_RESEND_REWARDS_INFO As String = "CRT_RESEND_REWARDS_INFO"
    Public Const EVNT_TYP__CLM_SEND_REIMBURSE_INFO As String = "CLM_SEND_REIMBURSE_INFO"
    Public Const EVNT_TYP_CUST_CLM_REIMBURSE_INFO As String = "CUST_CLM_SEND_REIMBURSE_INFO"
    Public Const EVNT_TYP__CLM_RESEND_REIMBURSE_INFO As String = "CLM_RESEND_REIMBURSE_INFO"
    Public Const EVNT_TYP__CLAIM_UPDATED As String = "CLAIM_UPDATE"
    Public Const EVNT_TYP__CLAIM_APPROVED As String = "CLAIM_APPROVED"
    Public Const EVNT_TYP__CLAIM_DENIED As String = "CLAIM_DENIED"
    Public Const EVNT_TYP__CLAIM_REFERRED As String = "CLAIM_REFERRED"
    Public Const PRESERVE_AUTH_AMOUNT_AT_REPLACE_ITEM As String = "PRESERVE_AUTH_AMOUNT_AT_REPLACE_ITEM"
    Public Const EVNT_TYP_SEND_CLAIM_GIFT_CARD = "SEND_CLAIM_GIFT_CARD"
    Public Const EVNT_TYP_SEND_CERT_VOUCHER = "SEND_CERT_VOUCHER"
    Public Const EVNT_TYP__NEW_CLAIM = "NEW_CLAIM"


    Public Const EVNT_COMM_DIRECTION As String = "COMM_DIRECTION"
    Public Const EVNT_COMM_DIR_OUTBOUND As String = "OUTBOUND"

    Public Const EVNT_COMM_CHANNEL As String = "COMM_CHANNEL"
    Public Const EVNT_COMM_CHAN_EMAIL As String = "EMAIL"

    Public Const ERROR_FLAG As String = "100"
    Public Const NO_HELP_COMTS_FOUND As String = "NO_HELP_COMTS_FOUND"

    ' Claim Issue Code
    Public Const ISSUE_CODE__TRBSHT = "TRBSHT"
    Public Const ISSUE_CODE__DEDCOLL = "DEDCOLL"
    Public Const ISSUE_CODE__DEVICE = "DEVICE"
    Public Const ISSUE_CODE__PRPTRQ = "PRPTRQ"

    'Fraudulent Claim Issue Codes
    Public Const ISSUE_CODE__FMI_R1 = "FMI_R1"
    Public Const ISSUE_CODE__FMI_R2 = "FMI_R2"
    Public Const ISSUE_CODE__FMI_R3 = "FMI_R3"
    Public Const ISSUE_CODE__FMI_R4 = "FMI_R4"
    Public Const ISSUE_CODE__FMI_R5 = "FMI_R5"
    Public Const ISSUE_CODE__FMI_R6 = "FMI_R6"
    Public Const ISSUE_CODE__FMI_R7 = "FMI_R7"

    'Certificate Status
    Public Const CERTIFICATE_STATUS__ACTIVE As String = "A"
    Public Const CERTIFICATE_STATUS__CANCELLED As String = "C"

    'Subscriber Status
    Public Const SUBSCRIBER_STATUS__ACTIVE As String = "A"
    Public Const SUBSCRIBER_STATUS__CANCELLED As String = "C"
    Public Const SUBSCRIBER_STATUS__SUSPENDED As String = "S"
    Public Const SUBSCRIBER_STATUS__PAST_DUE As String = "P"
    Public Const SUBSCRIBER_STATUS__PAST_DUE_CLAIMS_ALLOWED = "PA"

    'Deductible Based On
    Public Const DEDUCTIBLE_BASED_ON__FIXED As String = "FIXED"
    Public Const DEDUCTIBLE_BASED_ON__PERCENT_OF_AUTHORIZED_AMOUNT As String = "AUTH"
    Public Const DEDUCTIBLE_BASED_ON__PERCENT_OF_SALES_PRICE As String = "SALES"
    Public Const DEDUCTIBLE_BASED_ON__PERCENT_OF_ORIGINAL_RETAIL_PRICE As String = "ORIG"
    Public Const DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE As String = "LIST"
    Public Const DEDUCTIBLE_BASED_ON__PERCENT_OF_ITEM_RETAIL_PRICE As String = "ITEM"
    Public Const DEDUCTIBLE_BASED_ON__EXPRESSION As String = "EXP"
    Public Const DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE_WSD As String = "LISTWSD"
    Public Const DEDUCTIBLE_BASED_ON__COMPUTED_EXTERNALLY As String = "COMPUTED_EXTERNALLY"

    ' Claim Issue processor
    Public Const ISSUE_PROCESSOR__SYSTEM = "ISSPRO-SYSTEM"
    Public Const ISSUE_PROCESSOR__CSR = "ISSPRO-CSR"
    Public Const ISSUE_PROCESSOR__CUST = "ISSPRO-CUST"

    'Claim Bonus Computation Method
    Public Const BONUS_METHOD_FIXED_AMOUNT As String = "FIX"
    Public Const BONUS_METHOD_LABOUR_AMOUNT As String = "LABAMTPCT"
    Public Const BONUS_METHOD_AUTHORIZED_AMOUNT As String = "AUTHAMTPCT"

    'Claim Activity
    Public Const CLAIM_ACTIVITY__PENDING_REPLACEMENT As String = "PREP"
    Public Const CLAIM_ACTIVITY__REPLACED As String = "CLREP"
    Public Const CLAIM_ACTIVITY__TO_BE_REPLACED As String = "TBREP"
    Public Const CLAIM_ACTIVITY__REWORK As String = "REWRK"
    Public Const CLAIM_ACTIVITY__LEGAL_GENERAL As String = "LGGL"

    'Claim ServiceOrder Type
    Public Const CLAIM_SERVICE_ORDER_TYPE_REPLACEMENT As String = "REPLACEMENT"
    Public Const CLAIM_SERVICE_ORDER_TYPE_REPAIR As String = "REPAIR"

    'Cause of Loss
    Public Const CAUSE_OF_LOSS__BREAKAGE As String = "BREAK"
    Public Const CAUSE_OF_LOSS__THEFT As String = "THEFT"

    'Billing Frequency
    Public Const MONTHLY As String = "1"
    Public Const BI_MONTHLY As String = "2"
    Public Const HALF_YEARLY As String = "6"
    Public Const QUARTERLY As String = "3"
    Public Const YEARLY As String = "12"

    'Billing Status
    Public Const BILLING_STATUS__ACTIVE As String = "A"
    Public Const BILLING_STATUS__NOT_ACTIVE As String = "NA"
    Public Const BILLING_STATUS__ON_HOLD As String = "H"
    Public Const BILLING_STATUS__REJECTED As String = "R"

    'Coverage Extensions LookupList
    Public Const COV_EXT_BYPYMT As String = "P"
    Public Const COV_EXT_AUTOMATIC As String = "A"
    Public Const COV_EXT_NONE As String = "N"

    'Method of Repair
    Public Const METHOD_OF_REPAIR__CARRY_IN As String = "C"
    Public Const METHOD_OF_REPAIR__AT_HOME As String = "H"
    Public Const METHOD_OF_REPAIR__REPLACEMENT As String = "R"
    Public Const METHOD_OF_REPAIR__LEGAL As String = "L"
    Public Const METHOD_OF_REPAIR__GENERAL As String = "G"
    Public Const METHOD_OF_REPAIR__AUTOMOTIVE As String = "A"
    Public Const METHOD_OF_REPAIR__RECOVERY As String = "RC"
    Public Const METHOD_OF_REPAIR__PICK_UP As String = "P"
    Public Const METHOD_OF_REPAIR__SEND_IN As String = "S"

    Public Const METHOD_OF_REPAIR_CARRY_IN As String = "CARRY_IN_PRICE"
    Public Const METHOD_OF_REPAIR_AT_HOME As String = "HOME_PRICE"
    Public Const METHOD_OF_REPAIR_LEGAL As String = "L"
    Public Const METHOD_OF_REPAIR_GENERAL As String = "G"
    Public Const METHOD_OF_REPAIR_AUTOMOTIVE As String = "A"
    Public Const METHOD_OF_REPAIR_RECOVERY As String = "RC"
    Public Const METHOD_OF_REPAIR_PICK_UP As String = "PICK_UP_PRICE"
    Public Const METHOD_OF_REPAIR_SEND_IN As String = "SEND_IN_PRICE"
    Public Const METHOD_OF_REPAIR_CLEANING As String = "CLEANING_PRICE"
    Public Const METHOD_OF_REPAIR_ESTIMATE As String = "ESTIMATE_PRICE"
    Public Const METHOD_OF_REPAIR_REPLACEMENT As String = "REPLACEMENT_PRICE"
    Public Const METHOD_OF_REPAIR_DISCOUNTED As String = "DISCOUNTED_PRICE"

    Public Const CERT_STATUS_CANCELED As String = "C"

    Public Const CLAIM_STATUS__CLOSED As String = "C"
    Public Const CLAIM_STATUS__PENDING As String = "P"
    Public Const CLAIM_STATUS__ACTIVE As String = "A"
    Public Const CLAIM_STATUS__DENIED As String = "D"

    'Claim Extended statuses
    Public Const CLAIM_EXTENDED_STATUS__WAITING_ON_BUDGET_APPROVAL As String = "COD"
    Public Const CLAIM_EXTENDED_STATUS__WAITING_DOCUMENTATION As String = "WDOC"
    Public Const CLAIM_EXTENDED_STATUS__BUDGET_APPROVED As String = "BAPP"
    Public Const CLAIM_EXTENDED_STATUS__BUDGET_REJECTED As String = "BREJ"
    Public Const CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT As String = "PRFPMT"
    Public Const CLAIM_EXTENDED_STATUS__PAYMENT_REVIEW_APPROVED As String = "PMTRAP"
    Public Const CLAIM_EXTENDED_STATUS_ENTREY__PREDEFINED As String = "PRE"
    Public Const CLAIM_EXTENDED_STATUS_ENTREY__USER_SYSTEM_SELECT As String = "FLEX"

    'Accounting Status
    Public Const ACCT_STATUS__REQUESTED As String = "R"
    Public Const ACCT_STATUS__ISSUED As String = "I"
    Public Const ACCT_STATUS__REFERFINANCE As String = "F"
    Public Const ACCT_STATUS__NOTISSUED As String = "N"

    'Reason Closed
    Public Const REASON_CLOSED__TO_BE_REPLACED As String = "TBRP"
    Public Const REASON_CLOSED__TO_BE_PAID As String = "PAID"
    Public Const REASON_CLOSED__TO_BE_REPAIRED As String = "REP"
    Public Const REASON_CLOSED__DENIED_UNDER_DEDUCTIBLE As String = "DDUCT"
    Public Const REASON_CLOSED__PENDING_CLAIM_NOT_APPROVED As String = "PCNA"
    Public Const REASON_CLOSED__DENIED_VOIDED As String = "DVOID"
    Public Const REASON_CLOSED__REPLACEMENT_EXCEED As String = "DMAXR"
    Public Const REASON_CLOSED__NOT_REPORTED_WITHIN_PERIOD As String = "DNOL"
    Public Const REASON_CLOSED__ACTIVE_TRADEIN_QUOTE_EXISTS As String = "DATQ"
    Public Const REASON_CLOSED__NO_ACTIVITY As String = "NOACT"
    Public Const REASON_CLOSED_DENIED As String = "DENY"
    Public Const FLP_NO = "NO"
    Public Const REASON_CLOSED_LLE As String = "LLE"

    'Coverage Type
    Public Const COVERAGE_TYPE__EXTENDED As String = "E"
    Public Const COVERAGE_TYPE__MANUFACTURER As String = "M"
    Public Const COVERAGE_TYPE__MANUFACTURER_MAIN_PARTS As String = "MP"
    Public Const COVERAGE_TYPE__ACCIDENTAL As String = "A"
    Public Const COVERAGE_TYPE__THEFTLOSS As String = "T"
    Public Const COVERAGE_TYPE__THEFT As String = "T2"
    Public Const COVERAGE_TYPE__LOSS As String = "L1"
    Public Const COVERAGE_TYPE__MECHANICAL_BREAKDOWN As String = "B"

    'Comment Type
    Public Const COMMENT_TYPE__COMPLAINT_CUSTOMER As String = "COMC"
    Public Const COMMENT_TYPE__CUSTOMER_CALL As String = "CCAL"
    Public Const COMMENT_TYPE__REPLACEMENT_RECORD_CREATED As String = "RPCR"
    Public Const COMMENT_TYPE__REWORK_RECORD_CREATED As String = "RWKR"
    Public Const COMMENT_TYPE__LEGALGENERAL_RECORD_CREATED As String = "LEGE"
    Public Const COMMENT_TYPE__CLAIM_RECORD_CREATED As String = "CLCR"
    Public Const COMMENT_TYPE__PENDING_CLAIM_NOT_APPROVED As String = "PCNA"
    Public Const COMMENT_TYPE__PENDING_CLAIM_APPROVED As String = "PCAPR"
    Public Const COMMENT_TYPE__PAYMENT_REVERSAL As String = "PAYR"
    Public Const COMMENT_TYPE__PAYMENT_ADJUSTMENT As String = "PADJ"
    Public Const COMMENT_TYPE__CLAIM_DENIED_REPLACEMENT_EXCEED As String = "DMAXR"
    Public Const COMMENT_TYPE__CLAIM_DENIED_REPORT_TIME_EXCEED As String = "DNOL"
    Public Const COMMENT_TYPE__CLAIM_PENDING_SUBSCRIBER_STATUS_NOT_VALID As String = "PCDTS"
    Public Const COMMENT_TYPE__CLAIM_DENIED As String = "CLMDN"
    Public Const COMMENT_TYPE__CLAIM_SET_TO_PENDING As String = "PEND"
    Public Const COMMENT_TYPE__CERT_CANCEL_REQUEST As String = "CNREQ"
    Public Const COMMENT_TYPE__PENDING_PAYMENT_ON_OUTSTANDING_PREMIUM As String = "PPOP"
    Public Const COMMENT_TYPE__MAKE_MODEL_IMEI_MISMATCH As String = "WEBCLM"
    Public Const COMMENT_TYPE__OTHER As String = "TYOTH"
    Public Const COMMENT_TYPE__CALL_BACK_CUSTOMER As String = "CBC"
    Public Const COMMENT_TYPE__CLAIMED_EQUIPMENT_NOT_CONFIGURED As String = "CPCENR"
    Public Const COMMENT_TYPE__ENROLLED_EQUIPMENT_NOT_CONFIGURED As String = "CPENR"
    '5623
    Public Const COMMENT_TYPE__CLAIM_DENIED_REPORT_TIME_NOT_WITHIN_GRACE_PERIOD As String = "DNGP"
    Public Const COMMENT_TYPE__CLAIM_DENIED_COVERAGE_TYPE_MISSING As String = "DNCTM"
    Public Const COMMENT_TYPE__CLAIM_DENIED_DEVICE_PURCHASED_DATE As String = "DNDPD"


    'Default Search Fields
    Public Const DEFAULT_SORT_FOR_CLAIMS As String = "CLNUM"
    Public Const DEFAULT_SORT_FOR_INVOICES As String = "INVMB"
    Public Const DEFAULT_SORT_FOR_ADJUSTER_INBOX As String = "SC_TAT"
    Public Const DEFAULT_SORT_ORDER_FOR_ADJUSTER_INBOX As String = "DESC"

    'NOTIFICATION Search Fields
    Public Const SYSTEM_NOTIFICATION_SORT_BY__AUDIANCE_TYPE As String = "AT"
    Public Const SYSTEM_NOTIFICATION_SORT_BY__NOTIFICATION_DATE_RANGE As String = "NDR"
    Public Const SYSTEM_NOTIFICATION_SORT_BY__NOTIFICATION_TYPE As String = "NT"
    Public Const DEFAULT_SORT_ORDER_FOR_SYSTEM_NOTIFICATION As String = "DESC"

    'Pending Claim Search Sort Columns
    Public Const PENDING_CLAIM_SORT_COLUMN__CERT_NUMBER As String = "CERT_NUMBER"
    Public Const PENDING_CLAIM_SORT_COLUMN__CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Public Const PENDING_CLAIM_SORT_COLUMN__SERIAL_NUMBER As String = "SERIAL_NUMBER"
    Public Const PENDING_CLAIM_SORT_COLUMN__DATE_ADDED As String = "DATE_ADDED"
    Public Const PENDING_CLAIM_SORT_COLUMN__DEALER_CODE As String = "DEALER_CODE"
    Public Const PENDING_CLAIM_SORT_COLUMN__CUSTOMER_NAME As String = "CUSTOMER_NAME"
    Public Const PENDING_CLAIM_SORT_COLUMN__SERVICE_CENTER_NAME As String = "SERVICE_CENTER_NAME"
    Public Const PENDING_CLAIM_SORT_COLUMN__STATUS_DATE As String = "STATUS_DATE"

    'Service Order
    Public Const SERVICE_WARRANTY As String = "ServiceWarranty"
    Public Const REPLACEMENT_ORDER As String = "ReplacementOrder"
    Public Const SERVICE_ORDER As String = "ServiceOrder"

    'User Roles
    Public Const USER_ROLE__CSR As String = "CSR"
    Public Const USER_ROLE__CSR_MANAGER As String = "CSRMA"
    Public Const USER_ROLE__CLAIMS As String = "CLAIM"
    Public Const USER_ROLE__CLAIMS_MANAGER As String = "CLAMM"
    Public Const USER_ROLE__OFFICE As String = "OFFIC"
    Public Const USER_ROLE__OFFICE_MANAGER As String = "OFICM"
    Public Const USER_ROLE__LOCAL_SECURITY_ADMIN As String = "LOCAL"
    Public Const USER_ROLE__VIEW_ONLY As String = "VIEWO"
    Public Const USER_ROLE__IHQ_SUPPORT As String = "IHQSU"
    Public Const USER_ROLE__OFFICE_MANAGER_ABR As String = "OFMBR"
    Public Const USER_ROLE__OPERATOR As String = "OPERA"
    Public Const USER_ROLE__IHQ_ACTUARIAL As String = "IHQAC"
    Public Const USER_ROLE__IHQ_VIEW As String = "IHQVI"
    Public Const USER_ROLE__OFFICE_NETWORK As String = "OFNET"
    Public Const USER_ROLE__OFFICE_PREMIUMS As String = "OFPRE"
    Public Const USER_ROLE__SERVICE_CENTER As String = "SVCC"
    Public Const USER_ROLE__CALL_CENTER_AGENT As String = "CCA"
    Public Const USER_ROLE__CALL_CENTER_SUPERVISOR As String = "CCS"
    Public Const USER_ROLE__CLAIMS_ANALYST As String = "CLMAN"
    Public Const USER_ROLE__CLAIM_SUPPORT As String = "CLMSP"
    Public Const USER_ROLE__COMMENTS As String = "COMMT"
    Public Const USER_ROLE__CSR2 As String = "CSR2"
    Public Const USER_ROLE__COUNTY_SUPERUSER As String = "SUSER"
    Public Const USER_ROLE_EQUIPMENT_UPDATE As String = "EQUIP"

    ' Document Repository Types
    Public Const DOCUMENT_REPOSITORY_TYPE As String = "DOCUMENT_REPOSITORY_TYPE"
    Public Const DOCUMENT_REPOSITORY_TYPE__FILE_SYSTEM As String = "FILE_SYSTEM"
    Public Const DOCUMENT_REPOSITORY_TYPE__AZURE_BLOB As String = "AZURE_BLOB"

    'External User Types
    Public Const EXTERNAL_USER_TYPE__DEALER_GROUP As String = "DLRGRP"
    Public Const EXTERNAL_USER_TYPE__DEALER As String = "DLR"
    Public Const EXTERNAL_USER_TYPE__SERVICE_CENTER As String = "SC"
    Public Const EXTERNAL_USER_TYPE__OTHER As String = "OTH"

    'REPLACEMENT POLICIES Types
    Public Const REPLACEMENT_POLICY__CNCL As String = "CNCL" 'Cancel Certificate'
    Public Const REPLACEMENT_POLICY__KEEP As String = "KEEP" 'Keep Certificate'
    Public Const REPLACEMENT_POLICY__CLAST As String = "CLAST" 'Cancel Certificate in last 12 monthe'
    Public Const REPLACEMENT_POLICY__CNCLAF As String = "CNCLAF" 'Cancel Certificate after fulfillment

    Public Const AUOT_GEN_REJ_PYMT_FILE__NONE As String = "NONE"

#Region "Financial Balance Computation Method For Upgrade"
    Public Const UPG_FINANCE_BAL_COMP_METH__BR As String = "CLARO-BR"
    Public Const UPG_FINANCE_BAL_COMP_METH__PR As String = "CLARO-PR"
    Public Const UPG_FINANCE_BAL_COMP_METH__H3GIT As String = "H3G-IT"
    Public Const UPG_FINANCE_BAL_COMP_METH__NONE As String = "NONE"
    Public Const UPG_MOVISTAR_CUST_REWARD_POINTS__CH As String = "CH_CUST_REWARD_POINTS"
    Public Const UPG_FINANCE_BAL_COMP_METH_IA As String = "IA" ' Incoming Amount
#End Region

    'Future Date Allow For
    Public Const FDAF_NONE As String = "NONE"

    'Other Searches
    Public Const SALUTATION As String = "SLTN"

    ' Tabs
    Public Const TAB_HOME_PAGE As String = "HOME PAGE"

    'Document Types
    Public Const DOCUMENT_TYPE__CPF As String = "CPF"
    Public Const DOCUMENT_TYPE__CNPJ As String = "CNPJ"
    Public Const DOCUMENT_TYPE__CON As String = "CON"
    Public Const DOCUMENT_TYPE__OTHER As String = "OTHER"

    'PAYMENT METHODS
    Public Const PAYMENT_METHOD__BANK_TRANSFER As String = "CTT"
    Public Const PAYMENT_METHOD__ADMIN_CHECK As String = "CAD"
    Public Const PAYMENT_METHOD__CHECK_TO_CONSUMER As String = "CHK"
    Public Const PAYMENT_METHOD__PAYMENT_ORDER As String = "PYO"
    Public Const PAYMENT_METHOD__DARTY_GIFT_CARD As String = "DGFT"

    'LANGUAGES
    Public Const ENGLISH_LANG_CODE As String = "EN"

    'Yes No LookupList
    Public Const YESNO As String = "YESNO"
    Public Const YESNO_Y As String = "Y"
    Public Const YESNO_N As String = "N"
    Public Const EXT_YESNO_Y As String = "YESNO-Y"
    Public Const EXT_YESNO_N As String = "YESNO-N"

    'MasterClaimProcessing LookupList
    Public Const MasterClmProc_ANYMC As String = "ANYMC"
    Public Const MasterClmProc_BYDOL As String = "BYDOL"
    Public Const MasterClmProc_NONE As String = "NONE"

    'Include First Payment LookupList
    Public Const INCLUDE_FIRST_PAYMENT_NO As String = "NO"
    Public Const INCLUDE_FIRST_PAYMENT_COMP As String = "COMP"
    Public Const INCLUDE_FIRST_PAYMENT_INC As String = "INC"

    'Pro Rata Method LookupList
    Public Const PRO_RATA_METHOD_A As String = "A"
    Public Const PRO_RATA_METHOD_FMO As String = "FMO"
    Public Const PRO_RATA_METHOD_NPR As String = "NPR"

    'Refunds Destination
    Public Const REFUND_DESTINATION__ACCOUNT As String = "1"
    Public Const REFUND_DESTINATION__CUSTOMER As String = "2"

    'Service Types
    Public Const SERVICE_TYPES__AUTOMOTIVE As String = "8"
    Public Const SERVICE_TYPES__RECOVERY As String = "9"

    'Dealer Types
    Public Const DEALER_TYPES__ESC As String = "1"
    Public Const DEALER_TYPES__VSC As String = "2"
    Public Const DEALER_TYPE_WEPP As String = "3"

    'Companies
    Public Const COMPANY__ABR As String = "ABR"
    Public Const COMPANY__TBR As String = "TBR"
    Public Const COMPANY__VBR As String = "VBR"
    Public Const COMPANY__APR As String = "APR"
    Public Const COMPANY__PRC As String = "PRC"

    ' Dealers
    Public Const DEALER__DUDR As String = "DUDR"
    Public Const DEALER__CLARO As String = "CLRO"
    Public Const DEALER__TMOBIL As String = "TMBL"
    Public Const DEALER__CLAC As String = "CLAC"
    Public Const DEALER_TABLE As String = "ELP_DEALER"

    'ADDITIONAL DAC
    Public Const ADDL_DAC__NONE As String = "NONE"
    Public Const ADDL_DAC__TAX As String = "TAX"
    Public Const ADDL_DAC__ADMIN As String = "ADM"

    ' Refund Compute Method
    Public Const REFUND_COMPUTE_METHOD__1 = "1"
    Public Const REFUND_COMPUTE_METHOD__2 = "2"
    Public Const REFUND_COMPUTE_METHOD__3 = "3"
    Public Const REFUND_COMPUTE_METHOD__4 = "4"
    Public Const REFUND_COMPUTE_METHOD__5 = "5"
    Public Const REFUND_COMPUTE_METHOD__6 = "6"
    Public Const REFUND_COMPUTE_METHOD__7 = "7"
    Public Const REFUND_COMPUTE_METHOD__8 = "8"
    Public Const REFUND_COMPUTE_METHOD__9 = "9"
    Public Const REFUND_COMPUTE_METHOD__10 = "10"
    Public Const REFUND_COMPUTE_METHOD__11 = "11"
    Public Const REFUND_COMPUTE_METHOD__12 = "12"
    Public Const REFUND_COMPUTE_METHOD__13 = "13"
    Public Const REFUND_COMPUTE_METHOD__14 = "14"
    Public Const REFUND_COMPUTE_METHOD__15 = "15"
    Public Const REFUND_COMPUTE_METHOD__16 = "16"
    Public Const REFUND_COMPUTE_METHOD__17 = "17"
    Public Const REFUND_COMPUTE_METHOD__18 = "18"
    Public Const REFUND_COMPUTE_METHOD__19 = "19"
    Public Const REFUND_COMPUTE_METHOD__20 = "20"
    Public Const REFUND_COMPUTE_METHOD__21 = "21"
    Public Const REFUND_COMPUTE_METHOD__22 = "22"

    'ID VAlidation
    Public Const ID_VALIDATION_FULL = "1"
    Public Const ID_VALIDATION_PARTIAL = "2"
    Public Const ID_VALIDATION_NONE = "3"

    'Olita Search type LookupList
    Public Const OLITA_SEARCH_EXACT As String = "EX"
    Public Const OLITA_SEARCH_GENERIC As String = "GE"

    'Translate Product Codes
    Public Const TPRDC_CODE_ONLY As String = "Y"
    Public Const TPRDC_EXTENDED As String = "EXT"
    Public Const TPRDC_NO As String = "N"
    Public Const TPRDC_PARTIAL As String = "P"

    'Cert cancel By fields
    Public Const CCANBY_CERTNO As String = "CERTNO"
    Public Const CCANBY_INVNO As String = "INVNO"
    Public Const CCANBY_SERNO As String = "SERNO"

    'Service Center Integration
    Public Const INTEGRATED_WITH_GVS As String = "GVS"
    Public Const INTEGRATED_WITH_AGVS As String = "AGVS"

    'Report CE Destination
    Public Const REPORTCE_DEST__WEB As String = "WBARH"
    Public Const REPORTCE_DEST__FTP As String = "FTP"
    Public Const REPORTCE_DEST__EMAIL As String = "EMAIL"
    'Country Codes
    Public Const Country_Code_Argentina As String = "AR"
    Public Const Country_Code_Brasil As String = "BR"
    Public Const Country_Code_France As String = "FR"

    ' Payee Type
    Public Const Payee_Type_Dealer_Group As String = "1"
    Public Const Payee_Type_Dealer As String = "2"
    Public Const Payee_Type_Branch As String = "3"
    Public Const Payee_Type_Comm_Entity As String = "4"

    ' Compute Tax Based
    Public Const COMPUTE_TAX_BASED As String = "COMTAXBASED"
    Public Const COMPUTE_TAX_BASED_CUSTOMERS_ADDRESS As String = "CA"
    Public Const COMPUTE_TAX_BASED_OBLIGOR_STATUS As String = "OS"

    'PAYMENT TYPES
    Public Const PAYMENT_TYPE__CASH As String = "1"
    Public Const PAYMENT_TYPE__CREDIT_CARD As String = "2"
    Public Const PAYMENT_TYPE__CHECK As String = "3"
    Public Const PAYMENT_TYPE__FINANCED_BY_CREDIT_CARD As String = "4"
    Public Const PAYMENT_TYPE__NO_INTEREST As String = "5"
    Public Const PAYMENT_TYPE__DEBIT_ACCOUNT As String = "6"

    ' Tax Type
    Public Const TAX_TYPE As String = "TTYP"
    Public Const TAX_TYPE__POS As String = "1"
    Public Const TAX_TYPE__PREMIUMS As String = "2"
    Public Const TAX_TYPE__CREDITCARDS As String = "3"
    Public Const TAX_TYPE__INVOICE As String = "4"
    Public Const TAX_TYPE__COMMISSIONS As String = "6"
    Public Const TAX_TYPE__REPAIRS As String = "7"
    Public Const TAX_TYPE__REPLACEMENT As String = "8"
    Public Const TAX_TYPE__CLAIM_DIAGNOSTICS As String = "CDG"
    Public Const TAX_TYPE__CLAIM_DISPOSITION As String = "D"
    Public Const TAX_TYPE__CLAIM_LABOR_REPAIR As String = "LRP"
    Public Const TAX_TYPE__CLAIM_LABOR_REPLACEMENT As String = "LRL"
    Public Const TAX_TYPE__CLAIM_OTHER As String = "CO"
    Public Const TAX_TYPE__CLAIM_PARTS As String = "P"
    Public Const TAX_TYPE__CLAIM_SERVICE As String = "SV"
    Public Const TAX_TYPE__CLAIM_SHIPPING As String = "S"
    Public Const TAX_TYPE__CLAIM_TRIP As String = "T"
    Public Const TAX_TYPE__CLAIM_DEFAULT As String = "CD"

    'Tax_Compute_Method
    Public Const TAX_COMPUTE_METHODS As String = "TCOMP"
    Public Const TAX_COMPUTE_METHOD__MANUAL_COMPUTE_METHOD As String = "I"
    Public Const TAX_COMPUTE_METHOD__COMPUTE_ON_GROSS As String = "G"
    Public Const TAX_COMPUTE_METHOD__COMPUTE_ON_NET As String = "N"
    Public Const TAX_COMPUTE_METHOD__COMPUTE_ON_PRODUCT_PRICE As String = "P"

    ' Task Status
    Public Const TASK_STATUS As String = "TASK_STATUS"
    Public Const TASK_STATUS__COMPLETED As String = "C"
    Public Const TASK_STATUS__DELETED As String = "D"
    Public Const TASK_STATUS__FAILED As String = "F"
    Public Const TASK_STATUS__IN_PROGRESS As String = "P"
    Public Const TASK_STATUS__OPEN As String = "O"

    ' Product Tax Type
    Public Const PRODUCT_TAX_TYPE As String = "PTT"
    Public Const PRODUCT_TAX_TYPE__REAL_PROPERTY As String = "RP"
    Public Const PRODUCT_TAX_TYPE__PERSONAL_PROPERTY As String = "PP"
    Public Const PRODUCT_TAX_TYPE__ALL As String = "AL"

    'Collection Methods
    Public Const COLLECTION_METHOD__DEALER_COLLECTS As String = "1"
    Public Const COLLECTION_METHOD__ASSURANT_COLLECTS As String = "6"
    Public Const COLLECTION_METHOD__THRID_PARTY_COLLECTS As String = "7"
    Public Const COLLECTION_METHOD__ASSURANT_COLLECTS_PRE_AUTH As String = "8"
    Public Const COLLECTION_METHOD__PARTIAL_PAYMENT As String = "3"

    'Deductible Collection Methods
    Public Const DED_COLL_METHOD_CASH_ON_DELIVERY As String = "COD"
    Public Const DED_COLL_METHOD_CR_CARD As String = "CC"
    Public Const DED_COLL_METHOD_DEFFERED_COLL As String = "DEFCOLL"
    Public Const DED_COLL_CR_AUTH_CODE_LEN As String = "6"

    'Payment Instruments
    Public Const PAYMENT_INSTRUMENT__CASH As String = "S"
    Public Const PAYMENT_INSTRUMENT__CHECK As String = "K"
    Public Const PAYMENT_INSTRUMENT__DEBIT_ACCOUNT As String = "B"
    Public Const PAYMENT_INSTRUMENT__CREDIT_CARD As String = "C"
    Public Const PAYMENT_INSTRUMENT__FINANCED_BY_CREDIT_CARD As String = "F"
    Public Const PAYMENT_INSTRUMENT__FINANCED_BY_THRID_PARTY As String = "T"
    Public Const PAYMENT_INSTRUMENT__NO_INTREST As String = "I"
    Public Const PAYMENT_INSTRUMENT__DEALER_BILL As String = "D"
    Public Const PAYMENT_INSTRUMENT__PRE_AUTH_CREDIT_CARD As String = "A"
    Public Const PAYMENT_INSTRUMENT__PAYROLL_DEDUCTION As String = "P"
    Public Const PAYMENT_INSTRUMENT__PAYPAL As String = "EBAYPP"

    'Installment Definition LookupList
    Public Const INSTALLMENT_DEFINITION__INCOMING As String = "I"
    Public Const INSTALLMENT_DEFINITION__PRODUCT_CODE As String = "P"
    Public Const INSTALLMENT_DEFINITION__PRODUCT_CODE_OR_CONTRACT As String = "PC"

    'Replacement Based On 
    Public Const REPLACEMENT_BASED_ON__DATE_OF_LOSS As String = "DOL"
    Public Const REPLACEMENT_BASED_ON__INSURANCE_ACTIVATION_DATE As String = "IAD"

    'Claim Limt By
    Public Const CLAIM_LIMIT_BY__BOTH As String = "BOTH"
    Public Const CLAIM_LIMIT_BY__REPAIR As String = "RPR"
    Public Const CLAIM_LIMIT_BY__REPLACEMENT As String = "REP"
    Public Const CLAIM_LIMIT_BY__MAX2REPLACEMENT As String = "MAXR"

    'Denied Reason
    Public Const DENIED_MAX__OCCURRENCES_REACHED As String = "DMXOC"
    Public Const REASON_DENIED_CLAIM_ISSUE_REJECTED As String = "DCIR"
    Public Const REASON_DENIED__MANUFACTURER_WARRANTY As String = "DMF"
    Public Const REASON_DENIED__NO_COVERAGE As String = "DNOCO"
    Public Const REASON_DENIED__SERIAL_NUMBER_MISMATCH As String = "DSNMIS"
    Public Const REASON_DENIED_MAX_COVERAGE_LIABILITY_LIMIT_REACHED As String = "DMXCL"
    Public Const REASON_DENIED_MAX_PRODUCT_LIABILITY_LIMIT_REACHED As String = "DMXPL"
    '5623
    Public Const REASON_DENIED__NOT_REPORTED_WITHIN_GRACE_PERIOD As String = "DNGP"
    Public Const REASON_DENIED__COVERAGE_TYPE_MISSING As String = "DNCTM"
    Public Const REASON_DENIED__DEVICE_PURCHASED_DATE As String = "DNDPD"

    Public Const REASON_DENIED__INCORRECT_DEVICE_SELECTED As String = "DNCTM"
    'Kept code individually to keep changes here only

    Public Const REASON_DENIED__PRE_CHECK_FAILED As String = "APCF"

    'OCCURRENCES_ALWD
    Public Const OCCURRENCES_ALWD_SPL_SVC_UNLIMITED As String = "U"
    Public Const OCCURRENCES_ALWD_SPL_SVC_ONE_PER_YEAR As String = "Y"
    Public Const OCCURRENCES_ALWD_SPL_SVC_ONE_PER_CERT_PERIOD As String = "P"

    'PriceGroup
    Public Const PRICEGROUP_SPL_SVC_CARRY_IN As String = "I"
    Public Const PRICEGROUP_SPL_SVC_CLEANING As String = "C"
    Public Const PRICEGROUP_SPL_SVC_ESTIMATE As String = "E"
    Public Const PRICEGROUP_SPL_SVC_HOME As String = "H"
    Public Const PRICEGROUP_SPL_SVC_OTHER As String = "O"
    Public Const PRICEGROUP_SPL_SVC_MANUAL As String = "M"
    Public Const PRICEGROUP_SPL_SVC_PRICE_LIST As String = "P"


    'WhoPays
    Public Const ASSURANT_PAYS As String = "AIZ"

    'Repair Codes
    Public Const REPAIR_CODE__DELIVERY_FEE_NO_DEDUCTIBLE As String = "DLVRY"

    'Effective Expiration
    Public Const EFFECTIVE As String = "Effective"
    Public Const EXPIRATION As String = "Expiration"

    'Req-1016 Start
    'PERIOD RENEW Premium
    Public Const PERIOD_RENEW__SINGLE_PREMIUM As String = "0"
    Public Const PERIOD_RENEW__MONTHLY_PREMIUM As String = "1"
    Public Const PERIOD_RENEW__QUARTERLY_PREMIUM As String = "3"

    'Req-1016 End

    'REQ-5761
    Public Const ALLOW_CC_REJECTIONS_NO As String = "0"
    Public Const ALLOW_CC_REJECTIONS_ONE As String = "1"
    Public Const ALLOW_CC_REJECTIONS_TWO As String = "2"
    Public Const ALLOW_CC_REJECTIONS_THREE As String = "3"

    'REQ-1057
    'CLAIM ISSUE STATUS
    Public Const CLAIMISSUE_STATUS__OPEN As String = "OPEN"
    Public Const CLAIMISSUE_STATUS__WAIVED As String = "WAIVED"
    Public Const CLAIMISSUE_STATUS__REJECTED As String = "REJECTED"
    Public Const CLAIMISSUE_STATUS__RESOLVED As String = "RESOLVED"
    Public Const CLAIMISSUE_STATUS__PENDING As String = "PENDING"
    Public Const CLAIMISSUE_STATUS__REOPEN As String = "REOPEN"
    'REQ-1057 End

    ' Service Type - SVC_TYP
    Public Const SERVICE_TYPE As String = "SVC_TYP"
    Public Const SERVICE_TYPE__WORK_QUEUE As String = "WRKQUE"
    Public Const SERVICE_TYPE__AUTHORIZATION As String = "AUTH"
    Public Const SERVICE_TYPE__DOCUMENT_IMAGING As String = "DOCIMG"
    Public Const SERVICE_TYPE__DRP_SYSTEM As String = "DRPSYSTEM"
    Public Const SERVICE_TYPE__DOCUMENT_ADMIN As String = "DOCADM"
    Public Const SERVICE_TYPE__DOCUMENT As String = "DOC"
    Public Const SERVICE_TYPE__ACTIVATE_AV_PRODUCT As String = "ACT_AV_PROD"
    Public Const SERVICE_TYPE__CANCEL_AV_PRODUCT As String = "CAN_AV_PROD"
    Public Const SERVICE_TYPE__UPDATE_AV_PRODUCT As String = "UPD_AV_PROD"
    Public Const SERVICE_TYPE__ACSELX As String = "ACSELX"
    Public Const SERVICE_TYPE__BR_CC_AUTH As String = "BR_CC_AUTH"
    Public Const SERVICE_TYPE__SERVICE_NETWORK_GVS As String = "SVC_NTWRK_GVS"
    Public Const SERVICE_TYPE__DIGITAL_SERVICE As String = "DIGITAL_SERVICE_AUTH"
    Public Const SERVICE_TYPE_CUST_COMM_SERVICE As String = "CUSTOMER_COMM_AUTH"
    Public Const SERVICE_TYPE__SERVICE_FABRIC_SERVICE As String = "SERVICE_FABRIC_AUTH"
    Public Const SERVICE_TYPE__CLAIMS_QUESTION_SERVICE As String = "CLAIM_QUESTION_SERVICE"
    Public Const SERVICE_TYPE__CLAIMS_INDIX_SERVICE As String = "INDIX_SERVICE"
    Public Const SERVICE_TYPE__CLAIMS_FALABELLA_SERVICE_WORKORDER As String = "CL_FALABELLA_SERVICE_WO"
    Public Const SERVICE_TYPE__CLAIMS_FALABELLA_SERVICE_CONFIRMATION As String = "CL_FALABELLA_SERVICE_CONF"
    Public Const SERVICE_TYPE__GIFT_CARD_SERVICE = "GIFTCARD_SERVICE"
    Public Const SERVICE_TYPE__KDDI_APPLECARE_SERVICE As String = "KDDI_APPLECARE_SVC"
    Public Const SERVICE_TYPE_ASM_MEXICO As String = "ASM_MEXICO"
    Public Const SERVICE_TYPE_ISHOP_MEXICO As String = "ISHOP_MEXICO"
    Public Const SERVICE_TYPE_MAC_STORE As String = "MAC_STORE_MEXICO"
    Public Const SERVICE_TYPE__CLAIMS_FULFILLMENT_SERVICE As String = "CLAIM_FULFILLMENT_SERVICE"
    Public Const SERVICE_TYPE__WEB_API_LOCKER_PASSCODE As String = "WEB_API_LOCKER_PASSCODE"
    Public Const SERVICE_TYPE__CLAIM_SERVICE As String = "CLAIM_SERVICE"
    Public Const SERVICE_TYPE__SVC_CERT_ENROLL As String = "SVC_CERT_ENROLL"
    Public Const SERVICE_TYPE__CLAIM_LEGACY_BRIDGE_SERVICE As String = "CLAIM_LEGACY_BRIDGE_SERVICE"
    Public Const SERVICE_TYPE__FTP_LOCATION_PCI_SECURE_ZONE As String = "FTP_LOCATION_PCI_SECURE"
    Public Const SERVICE_TYPE__FTP_LOCATION_NON_PCI_SECURE_ZONE As String = "FTP_LOCATION_NON_PCI_SECURE"
    Public Const SERVICE_TYPE__CLAIM_FULFILLMENT_WEB_APP_GATEWAY_SERVICE As String = "WEB_APP_GATEWAY_SERVICE"
    Public Const SERVICE_TYPE__FILE_ADMIN_SERVICE As String = "FILE_ADMIN_SERVICE"
    Public Const SERVICE_TYPE__CASE_MANAGEMENT_WEB_APP_GATEWAY_SERVICE As String = "CASE_MANAGEMENT_WEB_APP_GATEWAY_SERVICE"
    Public Const SERVICE_TYPE__FILE_MANAGEMENT_ADMIN_SERVICE As String = "FILE_MANAGEMENT_ADMIN_SERVICE"
    Public Const SERVICE_TYPE_DF_API_URL As String = "DF_UI_API"
    Public Const SERVICE_TYPE__UTILTY_SERVICE As String = "UTILITY_SERVICE"
    Public Const SERVICE_TYPE_GROUP_NAME As String = "Services"


    'REQ-6230
    Public Const SERVICE_TYPE__CLAIMS_INDIX_SERVICE_PRODUCT_SEARCH As String = "INDIX_SERVICE_PRODUCT_SEARCH"

    ' Work Queue Action
    Public Const WORK_QUEUE_ACTION As String = "WQ_ACTION"
    Public Const WORK_QUEUE_ACTION__WORK_ON_CLAIM As String = "CLM"
    Public Const WORK_QUEUE_ACTION__INDEX_IMAGE As String = "IMG"

    ' Role Provider Type - ROLE_PROVIDER
    Public Const ROLE_PROVIDER As String = "ROLE_PROVIDER"
    Public Const ROLE_PROVIDER__WORKQUEUE As String = "WRK_QUEUE"

    ' WQ Reasons - REASON_CODE
    Public Const REASON_CODE As String = "REASON_CODE"

    ' WQ History Action Codes
    Public Const WQ_HISTORY_ACTION_PROCESS_CODE As String = "PROC"
    Public Const WQ_HISTORY_ACTION_REQUEUE_CODE As String = "REQU"
    Public Const WQ_HISTORY_ACTION_REDIRECT_CODE As String = "REDR"

    ' Claim Image Codes
    Public Const CLAIM_IMAGE_PROCESSED As String = "PRC"
    Public Const CLAIM_IMAGE_PENDING As String = "PND"
    Public Const HND_STORE_TYPE As String = "HND_STR_TYPE"

    'Customer Identifier Types
    Public Const CUSTOMER_IDENTIFIER_TYPE__SERIAL_NUMBER As String = "SERIALNUMBER"
    Public Const CUSTOMER_IDENTIFIER_TYPE__MOBILE_NUMBER As String = "MOBILENUMBER"

    'WS Web Experience File a Claim:  Response Statuses    
    Public Const WEB_EXPERIENCE__NO_ERROR As String = "NoError"
    Public Const WEB_EXPERIENCE__FATAL_ERROR As String = "FatalError"
    Public Const WEB_EXPERIENCE__LOOKUP_ERROR As String = "LookUpError"
    Public Const WEB_EXPERIENCE__VALIDATION_ERROR As String = "ValidationError"
    Public Const WEB_EXPERIENCE__CARRIER_MISMATCH_ERROR As String = "CarrierMismatchError"

    ' Subscriber Type
    Public Const SUBSCRIBER_TYPE As String = "SUBSCRIBER_TYPE"
    Public Const SUBSCRIBER_TYPE__HARVESTER As String = "HRVSTR"

    ' Claim Authorization Type
    Public Const CLAIM_AUTHORIZATION_TYPE As String = "CLM_AUTH_TYP"
    Public Const CLAIM_AUTHORIZATION_TYPE__SINGLE As String = "S"
    Public Const CLAIM_AUTHORIZATION_TYPE__MULTIPLE As String = "M"

    ' Claim Authorization Status
    Public Const CLAIM_AUTHORIZATION_STATUS As String = "CLM_AUTH_STAT"
    Public Const CLAIM_AUTHORIZATION_STATUS__AUTHORIZED As String = "A"
    Public Const CLAIM_AUTHORIZATION_STATUS__TO_BE_PAID As String = "TP"
    Public Const CLAIM_AUTHORIZATION_STATUS__PAID As String = "PA"
    Public Const CLAIM_AUTHORIZATION_STATUS__VOID As String = "V"
    Public Const CLAIM_AUTHORIZATION_STATUS__PENDING As String = "PE"
    Public Const CLAIM_AUTHORIZATION_STATUS__FULFILLED As String = "F"
    Public Const CLAIM_AUTHORIZATION_STATUS__RECONSILED As String = "R"
    Public Const CLAIM_AUTHORIZATION_STATUS__SENT As String = "S"
    Public Const CLAIM_AUTHORIZATION_STATUS__ONHOLD As String = "H"
    Public Const CLAIM_AUTHORIZATION_STATUS__CANCELLED As String = "C"
    Public Const CLAIM_AUTHORIZATION_STATUS__COLLECTED As String = "CO"
    Public Const CLAIM_AUTHORIZATION_STATUS__REVERSED As String = "RV"

    'Upgrade Unit Of Measure
    Public Const UPG_UNIT_OF_MEASURE__FIXED_NUMBER_Of_DAYS As String = "FIXEDDAYS"
    Public Const UPG_UNIT_OF_MEASURE__FIXED_NUMBER_Of_MONTHS As String = "FIXEDMONTHS"
    Public Const UPG_UNIT_OF_MEASURE__RANGE_IN_DAYS As String = "DAYSRANGE"
    Public Const UPG_UNIT_OF_MEASURE__RANGE_IN_MONTHS As String = "MONTHSRANGE"

    ' Search Type
    Public Const SEARCH_TYPE As String = "SEARCH_TYPE"
    Public Const SEARCH_TYPE__GREATER_THAN_DATE As String = "GTD"
    Public Const SEARCH_TYPE__LESS_THAN_DATE As String = "LTD"
    Public Const SEARCH_TYPE__BETWEEN As String = "BT"
    Public Const SEARCH_TYPE__EQUALS As String = "E"
    Public Const SEARCH_TYPE__GREATER_THAN As String = "GTN"
    Public Const SEARCH_TYPE__LESS_THAN As String = "LTN"
    Public Const SEARCH_TYPE__LIKE As String = "LK"

    ' Task Types
    Public Const TASK__AV_CANCEL_PRODUCT As String = "AV_CANCEL_PRODUCT"
    Public Const TASK__AV_UPDATE_PRODUCT As String = "AV_UPDATE_PRODUCT"
    Public Const TASK__AV_ACTIVATE_PRODUCT As String = "AV_ACTIVATE_PRODUCT"
    Public Const TASK__AV_RE_ACTIVATE_PRODUCT As String = "AV_RE_ACTIVATE_PRODUCT"
    Public Const TASK__VENDOR_AUTHORIZATION_LOAD As String = "VENDOR_AUTHORIZATION_LOAD"
    Public Const TASK__REPAIR_LOGISTIC_AUTHORIZATION_LOAD As String = "REPAIR_LOGISTIC_AUTHORIZATION_LOAD"
    Public Const TASK__VENDOR_INVOICE_LOAD As String = "VENDOR_INVOICE_LOAD"
    Public Const TASK__INVOICE_LOAD As String = "VENDOR_INVOICE_LOAD"
    Public Const TASK_ACSELX_DEDCOLL_FILE_LOAD = "ACSELX_DEDCOLL_FILE_LOAD"
    Public Const TASK_INV_MGMT_FILE_LOAD = "INV_MGMT_FILE_LOAD"
    Public Const TASK_SEND_EMAIL_NOTIFICATION = "AML_SND_MAIL"
    Public Const TASK_FBL_SLA_MONTR = "FBL_SLA_MONTR"
    Public Const TASK_NOTIFY_DIGITAL_SERVICE = "NOTIFY_DIGITAL_SERVICE"
    Public Const TASK_SEND_WELCOME_EMAIL = "SEND_WELCOME_EMAIL"
    Public Const TASK_SEND_ENR_REWARD_EMAIL = "SEND_ENR_REWARD_EMAIL"
    Public Const TASK_SEND_CANC_REQ_REJ_EMAIL = "SEND_CANC_REQ_REJ_EMAIL"
    Public Const TASK_SEND_CLM_GIFTCARD_EMAIL = "SEND_CLM_GIFTCARD_EMAIL"
    Public Const TASK_RESEND_WELCOME_EMAIL = "RESEND_WELCOME_EMAIL"
    Public Const TASK_RESEND_ENR_REWARD_EMAIL = "RESEND_ENR_REWARD_EMAIL"
    Public Const TASK_RESEND_CLM_GIFTCARD_EMAIL = "RESEND_CLM_GIFTCARD_EMAIL"
    Public Const TASK_ASM_UPDATE_CERT_IMEI = "ASM_UPDATE_CERT_IMEI"
    Public Const TASK_INVOKE_FALABELLA = "INVOKE_FALABELLA"


    Public Const TaskSendOutboundEmail = "OUTBOUND_EMAIL_SEND"
    Public Const TaskResendOutboundEmail = "OUTBOUND_EMAIL_RESEND"

    Public Const TASK_APPLECARE_ENROLL_ITEM_UPD = "KDDI_APPLECARE_ENROLL_TASK"

    Public Const DEALER_TYPE As String = "DEALER_TYPE"
    Public Const DEALER_TYPE_CODE_WEPP As String = "3"

    'REQ-1156
    Public Const EQUIPMENT_TYPE As String = "EQPTYPE"
    Public Const EQUIPMENT_TYPE__HANDSET As String = "HNDST"
    Public Const EQUIPMENT_TYPE__BATTEY As String = "BTRY"
    Public Const EQUIPMENT_TYPE__SIM As String = "SIM"
    Public Const EQUIPMENT_TYPE__CHARGER As String = "CRG"
    Public Const EQUIPMENT_TYPE__HEADSET As String = "HEAD"
    Public Const EQUIPMENT_TYPE__TABLET As String = "TAB"
    Public Const EQUIPMENT_TYPE__SMARTPHONE As String = "SMRT"
    Public Const EQUIPMENT_TYPE__FEATUREPHONE As String = "FTRE"

    Public Const EQUIPMENT_COND__NEW As String = "N"
    Public Const EQUIPMENT_COND__USED As String = "U"
    Public Const EQUIPMENT_COND__NEWORUSED As String = "R"

    Public Const INVOICE_STATUS As String = "INV_STAT"
    Public Const INVOICE_STATUS__NEW As String = "N"
    Public Const INVOICE_STATUS__UNDER As String = "U"
    Public Const INVOICE_STATUS__OVER As String = "O"
    Public Const INVOICE_STATUS__BALANCED As String = "B"
    Public Const INVOICE_STATUS__VOID As String = "V"


    Public Const MCAFEE_PROD_CODE_PHONE As String = "1046-47942-mmsspl"
    Public Const MCAFEE_PROD_CODE_TABLET As String = "1046-47944-mmstl"
    Public Const MCAFEE_REQ_PASSWORD As String = "mcafee123"

    'REQ-1153
    ' Address Types 
    Public Const ADDRESS_TYPE__SHIPPING As String = "S"
    Public Const ADDRESS_TYPE__BILLING As String = "B"
    Public Const ADDRESS_TYPE__PICKUP As String = "P"

    'Req-1154 start
    Public Const REGSTATUS_ACTIVE As String = "ACTIVE"
    Public Const REGSTATUS_PENDING As String = "PENDING"
    Public Const REGSTATUS_INACTIVE As String = "INACTIVE"
    Public Const EVNT_TYPE_ACTIVATE As String = "DVC_ACTIVATE"
    Public Const EVNT_TYPE_REACTIVATE As String = "DVC_RE_ACTIVATE"
    Public Const EVNT_TYPE_ENR_NOT_REC As String = "DVC_ENR_NOT_REC"
    'Req-1154 end

    'req 1106
    Public Const CLAIM_EQUIP_TYPE__CLAIMED As String = "C"
    Public Const CLAIM_EQUIP_TYPE__ENROLLED As String = "E"
    Public Const CLAIM_EQUIP_TYPE__REPLACEMENT As String = "R"
    Public Const CLAIM_EQUIP_TYPE__REPLACEMENT_OPTION As String = "RO"
    Public Const CLAIM_EQUIP_TYPE__REFURBISHED_REPLACEMENT As String = "RR"

    Public Const EQUIPMENT_VERIFIED As String = "EQUIPMENT_VERIFIED"
    Public Const EQUIPMENT_NOT_FOUND As String = "EQUIPMENT_NOT_FOUND"
    'REQ 1106

    'Claims Payable
    Public Const INVOICE_RECON_STATUS_RECONCILED As String = "RC"
    Public Const INVOICE_RECON_STATUS_TO_BE_PAID As String = "TP"
    Public Const INVOICE_RECON_STATUS_PAID As String = "PD"
    Public Const PYMNT_GRP_STATUS_APPROVED_FOR_PAYMENT As String = "APPRV"
    Public Const PYMNT_GRP_STATUS_OPEN As String = "OPEN"
    Public Const SERVICE_CLASS As String = "SVCCLASS"
    Public Const SERVICE_CLASS_TYPE As String = "SVCTYP"
    Public Const SERVICE_CLASS__REPAIR As String = "REPAIR"
    Public Const SERVICE_CLASS__REPLACEMENT As String = "REPLACEMENT"
    Public Const SERVICE_CLASS__DEDUCTIBLE As String = "DEDUCTIBLE"
    Public Const SERVICE_CLASS__MISCELLANEOUS As String = "MISC"
    Public Const SERVICE_CLASS__REIMBURSEMENT As String = "REIMBURSEMENT"

    Public Const VENDOR_SKU_DEDUCTIBLE = "DEDUCTIBLE"
    Public Const VENDOR_SKU_DESC_DEDUCTIBLE = "DEDUCTIBLE"
    Public Const VENDOR_SKU_PAY_DEDUCTIBLE = "PAY_DEDUCTIBLE"
    Public Const VENDOR_SKU_DESC_PAY_DEDUCTIBLE = "PAY DEDUCTIBLE"
    Public Const VENDOR_SKU_ABOVE_LIABILITY_LIMIT = "ABOVE_LIABILITY_LIMIT"
    Public Const VENDOR_SKU_DESC_ABOVE_LIABILITY_LIMIT = "ABOVE_LIABILITY_LIMIT"

    Public Const SERVICE_TYPE__HOME_PRICE As String = "HOME_PRICE"
    Public Const SERVICE_TYPE__CARRY_IN_PRICE As String = "CARRY_IN_PRICE"
    Public Const SERVICE_TYPE__SEND_IN_PRICE As String = "SEND_IN_PRICE"
    Public Const SERVICE_TYPE__PICK_UP_PRICE As String = "PICK_UP_PRICE"
    Public Const SERVICE_TYPE__CLEANING_PRICE As String = "CLEANING_PRICE"
    Public Const SERVICE_TYPE__ESTIMATE_PRICE As String = "ESTIMATE_PRICE"
    Public Const SERVICE_TYPE__DISCOUNTED_PRICE As String = "DISCOUNTED_PRICE"
    Public Const SERVICE_TYPE__REPLACEMENT_PRICE As String = "REPLACEMENT_PRICE"
    Public Const SERVICE_TYPE__PAY_DEDUCTIBLE As String = "PAY_DEDUCTIBLE"
    Public Const SERVICE_TYPE__DEDUCTIBLE As String = "DEDUCTIBLE"
    Public Const SERVICE_TYPE__SERVICE_WARRANTY As String = "SERVICE_WARRANTY"
    Public Const SERVICE_TYPE__LABOR_AMOUNT As String = "LA"
    Public Const SERVICE_TYPE__DIAGNOSTIC_AMOUNT As String = "DA"
    Public Const SERVICE_TYPE__PARTS_AMOUNT As String = "PA"
    Public Const SERVICE_TYPE__LOGISTICS_AMOUNT As String = "LGA"
    Public Const SERVICE_TYPE__ABOVE_LIABILITY_LIMIT As String = "ABOVE_LIABILITY_LIMIT"

    ' Auto Adjustment Reasons - Claim Authorization
    Public Const ADJUSTMENT_REASON As String = "ADJ_RESN"
    Public Const ADJUSTMENT_REASON__AA_DEDUCTIBLE As String = "AA_DEDUCTIBLE"
    Public Const ADJUSTMENT_REASON__AA_AUTO_ADJUSTED As String = "AUTO_ADJUSTED"
    Public Const ADJUSTMENT_REASON__INV_AMT_GT_AUTH_AMT As String = "INV_AMT_GT_AUTH_AMT"
    Public Const ADJUSTMENT_REASON__NOT_INCLUDED_ON_AUTHORIZATION As String = "NOT_INC_ON_AUTH"
    Public Const ADJUSTMENT_REASON__AA_PAY_DEDUCTIBLE As String = "AA_PAY_DEDUCTIBLE"
    Public Const ADJUSTMENT_REASON__ABOVE_LIABILITY_LIMIT As String = "ABOVE_LIABILITY_LIMIT"

    Public Const SPL_SVC_SERVICE_TYPE__HOME_PRICE As String = "HOME_PRICE"
    Public Const SPL_SVC_SERVICE_TYPE__CARRY_IN_PRICE As String = "CARRY_IN_PRICE"
    Public Const SPL_SVC_SERVICE_TYPE__SEND_IN_PRICE As String = "SEND_IN_PRICE"
    Public Const SPL_SVC_SERVICE_TYPE__PICK_UP_PRICE As String = "PICK_UP_PRICE"
    Public Const SPL_SVC_SERVICE_TYPE__CLEANING_PRICE As String = "CLEANING_PRICE"
    Public Const SPL_SVC_SERVICE_TYPE__ESTIMATE_PRICE As String = "ESTIMATE_PRICE"
    Public Const SPL_SVC_SERVICE_TYPE__DISCOUNTED_PRICE As String = "DISCOUNTED_PRICE"
    Public Const SPL_SVC_SERVICE_TYPE__REPLACEMENT_PRICE As String = "REPLACEMENT_PRICE"

    Public Const SERVICE_LEVEL As String = "SVC_LVL"
    Public Const SERVICE_LEVEL__L0 As String = "L0"
    Public Const SERVICE_LEVEL__L1 As String = "L1"
    Public Const SERVICE_LEVEL__L2 As String = "L2"
    Public Const SERVICE_LEVEL__L3 As String = "L3"

    ' Claim Status
    Public Const CLAIM_EXTENDED_STATUS As String = "CLMSTAT"
    Public Const CLAIM_EXTENDED_STATUS__REPAIRED As String = "REPRD"
    Public Const CLAIM_EXTENDED_STATUS__NOT_REPAIRED_BEYOND_ECONOMICAL_REPAIR As String = "NRBER"
    Public Const CLAIM_EXTENDED_STATUS__NOT_REPAIRED_LACK_OF_SERVICE_LEVEL As String = "NRSERLVL"
    Public Const CLAIM_EXTENDED_STATUS__NOT_REPAIRED_MISSING_SPARE_PARTS As String = "NRNOPART"
    Public Const CLAIM_EXTENDED_STATUS__NOT_REPAIRED_CYCLE_TIME_OUT As String = "NRTIMEOUT"
    Public Const CLAIM_EXTENDED_STATUS__NOT_REPAIRED_IMEI_MISMATCH As String = "NRIMEIMIS"
    Public Const CLAIM_EXTENDED_STATUS__NOT_REPAIRED_CLAIM_MISMATCH As String = "NRCLMMIS"
    Public Const CLAIM_EXTENDED_STATUS__REPAIRED_UNDER_OEM_WARRANTY As String = "RUOEM"
    Public Const CLAIM_EXTENDED_STATUS__NOT_REPAIRED_NO_ORIGINAL_ACCESSORIES As String = "NRNORIGAC"
    Public Const CLAIM_EXTENDED_STATUS__NOT_REPAIRED_OTHER_CAUSES As String = "NROTHER"

    ' Attribute Data Types
    Public Const ATTRIBUTE_DATE_TYPE As String = "ATBDTYP"
    Public Const ATTRIBUTE_DATE_TYPE__NUMBER As String = "NBR"
    Public Const ATTRIBUTE_DATE_TYPE__DATE As String = "DATE"
    Public Const ATTRIBUTE_DATE_TYPE__TEXT As String = "TXT"
    Public Const ATTRIBUTE_DATE_TYPE__HEXADECIMAL As String = "HEX"
    Public Const ATTRIBUTE_DATE_TYPE__YESNO As String = "YN"
    Public Const ATTRIBUTE_DATE_TYPE__REINSURANCESTATUS As String = "REJECTEDREINSUREDPENDING"
    Public Const ATTRIBUTE_DATE_TYPE__POST_MIGRATION_CONDITION As String = "POSTMIGRATIONCONDITIONS"
    Public Const ATTRIBUTE_DATE_TYPE__ACCT_PRORATE As String = "ACCT_PRORATE"
    Public Const ATTRIBUTE_DATE_TYPE__AUTO_RENEW_COV_LIMIT As String = "AUTO_RENEW_COV_LIMIT"

    ' Attribute
    Public Const ATTRIBUTE As String = "ATTRIBUTE"
    Public Const ATTRIBUTE__PERCEPTION_IIBB_REGION_ID As String = "PERCEPTION_IIBB_REGION_ID"
    Public Const ATTRIBUTE__PERCEPTION_IIBB As String = "PERCEPTION_IIBB"
    Public Const ATTRIBUTE__PERCEPTION_IVA As String = "PERCEPTION_IVA"
    Public Const ATTRIBUTE__BATCH_NUMBER As String = "BATCH_NUMBER"
    Public Const ATTRIBUTE__DEFAULT_REINSURANCE_STATUS As String = "DEFAULT_REINSURANCE_STATUS"
    Public Const ATTR_CANCEL_RULES_FOR_SFR As String = "CANCEL_RULES_FOR_SFR"
    Public Const ATTR_COMPUTE_CANCELLATION_DATE_AS_EOFMONTH As String = "COMPUTE_CANCELLATION_DATE_AS_EOFMONTH"
    Public Const ATTR_ENABLE_CHANGING_MFG_TERM_If_NO_CLAIMS_EXIST_In_PARENT_CHILD As String = "ECMFGTNC"
    Public Const SFR_CR_DEATH As String = "104"
    Public Const SFR_CR_MOVINGABROAD As String = "105"
    Public Const SFR_CR_CHATELLAW As String = "106"
    Public Const SFR_CR_HAMONLAW As String = "107"

    'Company Attributes
    Public Const COMP_ATTR__RPT_EXP_NOTIFY_EMAIL As String = "RPT_EXP_NOTIFY_EMAIL"
    Public Const COMP_ATTR__UPD_ZIP_LOCATOR As String = "UPDATE_ZIP_LOCATOR"

    'Dealer Attributes
    Public Const DLR_ATTRBT__TRD_IN As String = "PRC_CLM_DRP_CHECK_TRADE_IN"
    Public Const DLR_ATTRBT__FIXED_COMMISSION As String = "FIXED_COMMISSION"
    Public Const DLR_ATTRBT__AIZ_FILE_RUN_DT As String = "ASSURANT_LAST_EVENT_FILE_RUN_DATE"
    Public Const DLR_ATTRBT__SKP_CLM_CRT_CAN As String = "SKIP_CLAIM_CHECK_DURING_CERT_CANCEL"
    Public Const DLR_ATTRBT__DAYS_COMM_RPT As String = "NO_OF_DAYS_FOR_FIXED_COMM_RPT"
    Public Const DLR_ATTRBT__GEN_RESPONSE_FILE As String = "GENERATE_RESPONSE_FILE"
    Public Const DLR_ATTRBT__USE_DETAILED_INV_RPT As String = "USE_DETAILED_INVOICE_REPORT"
    Public Const DLR_ATTRBT__NEW_INVOICE_PAYMENT As String = "NEW_INVOICE_PAYMENT"
    Public Const DLR_ATTRBT__UPDATE_BANK_FOR_INSTALLMENTS As String = "UPDATE_BANK_FOR_INSTALLMENTS"
    Public Const DLR_ATTRBT__VALIDATE_NUM_OF_CLAIMS As String = "VALIDATE_NUM_OF_CLAIMS"
    Public Const DLR_ATTR_SKIP_SERVICE_CENTER_SCREEN As String = "SKIP_SERVICE_CENTER_SCREEN"
    Public Const DLR_ATTR_ENROLLMENT_FILE_PCI_PARAMETER As String = "ENROLLMENT_FILE_PCI_PARAMETER"
    Public Const DLR_ATTR__INVOKE_EXTERNAL_SERVICE As String = "INVOKE_EXTERNAL_SERVICE"
    Public Const DLR_ATTR__ENABLE_ALT_CUSTOMER_NAME As String = "ENABLE_ALT_CUSTOMER_NAME"
    Public Const DLR_ATTRBT_PAY_TO_CUST_AMT As String = "PAY_TO_CUST_AMT"
    'US #268248'
    Public Const DLR_ATTR_MANUAL_CLAIM_CASH_PYMT As String = "MANUAL_CLAIM_CASH_PYMT"
    Public Const DLR_ATTR_ALLOW_DELIVERY_DATE_SELECTION As String = "ALLOW_DELIVERY_DATE_SELECTION"

    'Product Attributes
    Public Const PRD_ATTRBT__CANCEL_CERT_QUES_SET_CODE As String = "CANCEL_POLICY_QUESTION_SET_CODE"

    ' Claim Load File Type
    Public Const CLAIM_LOAD_FILE_TYPE As String = "CLMLDFTYP"
    Public Const CLAIM_LOAD_FILE_TYPE__VENDOR_CLAIMS As String = "VC"
    Public Const CLAIM_LOAD_FILE_TYPE__VENDOR_INVOICE As String = "VI"
    Public Const CLAIM_LOAD_FILE_TYPE__REPAIR_AND_LOGISTIC As String = "RL"
    Public Const CLAIM_LOAD_FILE_TYPE__VENDOR_AUTHORIZATIONS As String = "VA"
    Public Const CLAIM_LOAD_FILE_TYPE__REPAIR_AND_LOGISTIC_AUTHORIZATIONS As String = "RLA"
    Public Const CLAIM_LOAD_FILE_TYPE__INVENTORY_MGMT As String = "IM"

    ' Authorization Details Required
    Public Const AUTHORIZATION_DETAIL As String = "AUTHDTL"
    Public Const AUTHORIZATION_DETAIL__NO As String = "N"
    Public Const AUTHORIZATION_DETAIL__REQUIRED As String = "ADR"
    Public Const AUTHORIZATION_DETAIL__SERVICE_CENTER As String = "ADFSC"

    'Installment Status Details
    Public Const IN_COLLECTION As String = "I"

    Public Const VEHICLE_COND__NEW As String = "NEW"
    Public Const VEHICLE_COND__USED As String = "USED"

    'FullFile Processing
    Public Const FULLFILE_NONE As String = "NO"

    'AUDIANCE Types
    Public Const AUDIANCE_TYPE__EXTERNAL_USER As String = "EU"
    Public Const AUDIANCE_TYPE__INTERNAL_USER As String = "IU"
    Public Const AUDIANCE_TYPE__ALL_USERS As String = "AU"

    'Notification Types
    Public Const AUDIANCE_TYPE__NEW_RELEASE As String = "NR"
    Public Const AUDIANCE_TYPE__PLANNED_MAINTENANCE As String = "PM"
    Public Const AUDIANCE_TYPE__PRODUCTION_ISSUE As String = "PI"


    Public Const SORT_BY_CERT_NUMBER As String = "CERTN"

    'Shipping Types
    Public Const SHIP_TYPE_TO_SC As String = "SHIP_TO_SC"
    Public Const SHIP_TYPE_TO_CUST As String = "SHIP_TO_CUST"

    Public Const LIST__SVCPREINV As String = "SVCPREINV"
    Public Const LIST_ITEM__SVCPICOMP As String = "SVCPICOMP"
    'REQ-5773  Payment Processing Types
    Public Const DEALER_PAYMENT As String = "DP"
    Public Const THIRD_PARTY_PAYMENT As String = "TPP"

    'Req-5921
    Public Const MAX_SVC_WARRANT_COUNT As String = "MAX_NO_OF_SVC_WRNTY"
    'Req-6171
    Public Const ATTR_DARTY_GIFT_CARD_TYPE As String = "DARTY_GIFT_CARD_TYPE"

    Public Const ATTR_CAN_DT_GTRTHN_12_MNTHS As String = "CAN_DT_GTRTHN_12_MNTHS"

    Public Const PRIVILEGE = "PRIVILEGE"
    Public Const PREMISSION_COMPLIANCE_OFFICER = "CMPOFF"

    'Customer Communication Entity Names
    Public Const ENTITY_NAME_CERT As String = "CERT"
    Public Const ENTITY_NAME_CLAIM As String = "CLAIM"
    Public Const ENTITY_NAME_CERT_CANC_REQ As String = "CERT_CANC_REQ"
    Public Const DEALER_CLAIM_RECORDING_DYNAMIC_QUESTIONS As String = "CLMREC-CLMDQ"
    Public Const DEALER_CLAIM_RECORDING_BOTH As String = "CLMREC-BOTH"
    Public Const COVERAGE_CONSEQ_DAMAGE_LIABILITY_LIMIT_BASED_ON_NOTAPPL As String = "PRODLILIMBASEDON-NOTAPPL"
    Public Const COVERAGE_CONSEQ_DAMAGE_FULFILMENT_METHOD_REIMB As String = "FULFILMETH-REIMB"

    'Certificate Status
    Public Const CASE_STATUS__OPEN As String = "OPEN"
    Public Const CASE_STATUS__CLOSED As String = "CLOSED"

    Public Const REWARD_STATUS__APPROVED As String = "REWARD_STATUS-APPROVED"
    Public Const REWARD_STATUS__GIFT_CARD_SENT As String = "REWARD_STATUS-GIFT_CARD_SENT"
    Public Const REWARD_STATUS__SEPA_XFER_SENT As String = "REWARD_STATUS-SEPA_XFER_SENT"
    Public Const REWARD_PYMT_MODE__GIFTCARD As String = "REWARD_PYMT_MODE-GIFT_CARD"


    ' Claim/Case Recording Page - Purpose
    Public Const CASE_PURPOSE__REPORT_CLAIM As String = "REPCLM"
    Public Const CASE_PURPOSE__CANCELLATION_REQUEST As String = "CERTCANCEL"
    Public Const CASE_PURPOSE__CONSEQUENTIAL_DAMAGE As String = "REPCONSEQDMG"
    Public Const CASE_PURPOSE__INQUIRY As String = "INQUIRY"

    ' Depreciation Schedule Usage
    Public Const DEPRECIATION_SCHEDULE_USAGE__CASH_REIMBURSE As String = "DEP_SCH_USAGE-CASH_REIMBURSE"
    Public Const DEPRECIATION_SCHEDULE_USAGE__LIABILITY_LIMIT As String = "DEP_SCH_USAGE-LIABILITY_LIMIT"

    Public Const ISSUE_TYPE_CONSEQUENTIAL_DAMAGE As String = "ISSUE_TYPE_CONSEQUENTIAL_DAMAGE"
    'CONSEQUENTIAL DAMAGE STATUS
    Public Const CONSEQUENTIAL_DAMAGE_STATUS__REQUESTED As String = "PERILSTAT-REQUESTED"
    Public Const CONSEQUENTIAL_DAMAGE_STATUS__FULFILLED As String = "PERILSTAT-FULFILLED"
    Public Const CONSEQUENTIAL_DAMAGE_STATUS__DENIED As String = "PERILSTAT-DENIED"
    Public Const CONSEQUENTIAL_DAMAGE_STATUS__APPROVED As String = "PERILSTAT-APPROVED"

    'ENTITY ATTRIBUTES(check attribute column of ELP_ENTITY_ATTRIBUTE table)
    Public Const ENTITY_ATTRIBUTE_REIMBURSEMENT_AMOUNT As String = "ReimbursementAmount"
    Public Const ENTITY_ATTRIBUTE_REPORT_NUMBER As String = "ReportNumber"
    Public Const ENTITY_ATTRIBUTE_BENEFIT_CHECK_IMEI_NUMBER As String = "IMEInumber"
    Public Const ENTITY_ATTRIBUTE_BENEFIT_CHECK_NO_ACTION_IMEI As String = "HasBenefitFalseNoAction"

    'ENTITY Type(check entity column of ELP_ENTITY_ATTRIBUTE table)
    Public Const ENTITY_TYPE_CONSEQUENTIAL_DAMAGE As String = "ConsequentialDamage"
    Public Const ENTITY_TYPE_BENEFIT_CHECK As String = "BenefitCheck"

    ' Authorization Fulfillment type
    Public Const AUTH_FULFILLMENT_TYPE_SERVICE_WARRANTY_REPAIR As String = "AUTH_FUL_TYPE-SWRPR"
    Public Const AUTH_FULFILLMENT_TYPE_SERVICE_WARRANTY_DIAGNOSTICS As String = "AUTH_FUL_TYPE-SWDIAG"
    Public Const AUTH_FULFILLMENT_TYPE_DEDUCTIBLE_COLLECTION As String = "AUTH_FUL_TYPE-DC"
    Public Const AUTH_FULFILLMENT_TYPE_SERVICE_WARRANTY_REPLACEMENT As String = "AUTH_FUL_TYPE-SWRPL"
    Public Const AUTH_FULFILLMENT_TYPE_REPAIR As String = "AUTH_FUL_TYPE-RPR"
    Public Const AUTH_FULFILLMENT_TYPE_DIAGNOSTIC As String = "AUTH_FUL_TYPE-DIAG"
    Public Const AUTH_FULFILLMENT_TYPE_REPLACEMENT As String = "AUTH_FUL_TYPE-RPL"
    Public Const AUTH_FULFILLMENT_TYPE_CASH_REIMBURSEMENT As String = "AUTH_FUL_TYPE-CR"

    ' Provider Types
    Public Const PROVIDER_TYPE__FULFILLMENT As String = "FULFILLMENT"

    Public Const FULFILLMENT_FW_LOGISTIC_STAGE As String = "FW"

    ' Provider Types
    Public Const PROVIDER_CLASS_CODE__FULPROVORAEBS As String = "FULPROVORAEBS"

    ' AccountType
    Public Const ACCOUNT_TYPE_CODE__SAVE As String = "SAVE"
    Public Const ACCOUNT_TYPE_CODE__CHECK As String = "CHECK"

    'Benefit Eligibility status   
    Public Const BENEFIT_STATUS__ACTIVE As String = "Active"
    Public Const BENEFIT_STATUS__INACTIVE As String = "Inactive"
    Public Const BENEFIT_STATUS__INELIGIBLE As String = "Not Eligible"

    'Edit Mfg term
    Public Const EDIT_MFG_TERM__NONE As String = "NONE"

    'Entity Restriction
    Public Const ENTITY_IS__RESTRICTED As String = "Y"

    'Refund Status
    Public Const REFUND_STATUS_PENDING As String = "CRST-P"

    'Fulfilment Method
    Public Const FULFILMENT_METHOD_REIMBURSEMENT As String = "FULFILMETH-REIMB"

    'REQ-6230
    Public Const DEFAULT_SORT_FOR_RETAIL_PRICE As String = "MK"

    'KDDI-Claim Auth Sub Status XCD'
    Public Const CLM_AUTH_SUBSTAT_CANCEL_COMPLETE As String = "CLM_AUTH_SUBSTAT-CNLCOM"
    Public Const CLM_AUTH_SUBSTAT_CANCEL_REQUESTED As String = "CLM_AUTH_SUBSTAT-CNLREQ"
    Public Const CLM_AUTH_SUBSTAT_CANCEL_FAILD As String = "CLM_AUTH_SUBSTAT-CNLFAIL"
    Public Const CLM_AUTH_SUBSTAT_CANCEL_PENDING As String = "CLM_AUTH_SUBSTAT-CNLPND"
    Public Const CLM_AUTH_SUBSTAT_RESHIPMENT_REQ As String = "CLM_AUTH_SUBSTAT-RESHIPREQ"
    Public Const CLM_AUTH_SUBSTAT_SOSUBMIT As String = "CLM_AUTH_SUBSTAT-SOSUBMIT"
    Public Const CLM_AUTH_SUBSTAT_CANCEL_SENT As String = "CLM_AUTH_SUBSTAT-CNLSENT"
    Public Const CLM_AUTH_SUBSTAT_AWR As String = "CLM_AUTH_SUBSTAT-AWR"

    'Dealer Contract Policy Type (Collective or Individual)
    Public Const CONTRACT_POLTYPE_COLLECTIVE As String = "CP"
    Public Const CONTRACT_POLTYPE_INDIVIDUAL As String = "IP"

    'Dealer Contract Policy Number Generation Type (Auto or Mannual)
    Public Const CONTRACT_POLGEN_AUTOGENERATE As String = "AG"
    Public Const CONTRACT_POLGEN_MANUALENTER As String = "ME"

    'claim payment based on deductible flag
    Public Const FULL_INVOICE_Y As String = "Y_FULL_INVOICE"
    Public Const AUTH_LESS_DEDUCT_Y As String = "Y_AUTH_LESS_DEDUCT"

    'Company codes
    Public Const AUS_COMPANY_CODE As String = "AAU"


End Class
