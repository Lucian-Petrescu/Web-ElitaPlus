Imports System.Collections.Generic
Imports Assurant.ElitaPlus.DALObjects.DBHelper

'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/2/2004)********************
Imports Assurant.ElitaPlus.Common.LookupListCache

Public Class ClaimDAL
    Inherits DALBase

#Region "WebSubmitClaimPreValidateData"

    Public Class WebSubmitClaimPreValidateInputData

        Public CustomerIdentifier As String
        Public SystemUserId As Guid
        Public DealerCode As String
        Public IdentifierType As String
        Public CoverageCode As String
        Public ServiceCenterCode As String
        Public CauseOfLossCode As String
        Public RegionCode As String
        Public CountryCode As String
        Public AddressTypeCode As String
        Public PaymentMethod As String
        Public SerialNumber As String
        Public Model As String
        Public Make As String
        Public DateOfLoss As Date

    End Class

    Public Class WebSubmitClaimPreValidateOutputData

        Public CoverageTypeId As Guid
        Public CertItemCoverageId As Guid
        Public ServiceCenterId As Guid
        Public CauseOfLossId As Guid
        Public RegionId As Guid
        Public CountryId As Guid
        Public AddressTypeId As Guid
        Public HomePhone As String
        Public WorkPhone As String
        Public Deductible As Decimal
        Public PaymentMethodId As Guid
        Public DefaultClaimStatusCode As String
        Public PreValidateErrorCode As Integer
        Public PreValidateError As String
        Public PreValidateSerialNumberErrorCode As Integer
        Public PreValidateMakeModelErrorCode As Integer

    End Class

#End Region

#Region "Constants"
    Public Const TABLE_NAME_MFG_DEDUCT As String = "ELP_MFG_DEDUCT"
    Public Const TABLE_NAME_DEDUCT_BY_MFG = "ELP_DEDUCT_BY_MFG"
    Public Const TABLE_NAME As String = "ELP_CLAIM"
    Public Const TABLE_NAME_UFI_LIST As String = "ELP_UFI_LIST"
    Public Const TABLE_NAME_CERT_PAYMENT As String = "ELP_CERT_PAYMENT"
    Public Const TABLE_NAME_WS As String = "CLAIM_DETAIL"
    Public Const TABLE_NAME_NEW_OPEN_CLAIM As String = "NEW_OPEN_CLAIMS"
    Public Const TABLE_NAME_STORE_SC_RELATIONS As String = "STORE_SC_RELATIONS"
    Public Const TABLE_NAME_SC_CLAIM_RELATIONS As String = "SC_CLAIM_RELATIONS"
    Public Const TABLE_NAME_PICKLIST_STORE_RELATIONS As String = "PICKLIST_STORE_RELATIONS"
    Public Const TABLE_NAME_CLAIMSTATUS_EXTENDEDSTATUS_RELATIONS As String = "CLAIMSTATUS_EXTENDEDSTATUS_RELATIONS"
    Public Const TABLE_NAME_ATTRIBUTE As String = "ATTRIBUTE"
    Public Const TABLE_NAME_PICKLIST As String = "PICKLIST"
    Public Const TABLE_NAME_STORE As String = "STORE"
    Public Const TABLE_NAME_SVC As String = "SVC_CLAIM"
    Public Const TABLE_NAME_CLMADJ As String = "CLMADJ"
    Public Const TABLE_NAME_SVC_COUNT As String = "SVC_CLAIM_TOTAL_RECORD_COUNT"
    Public Const TABLE_NAME_SERVICE_CENTER As String = "SERVICE_CENTER"
    Public Const TABLE_NAME_CLAIM_DETAIL As String = "CLAIM"
    Public Const COL_NAME_ROUTE_ID As String = "route_id"
    Public Const COL_NAME_STORE_SERVICE_CENTER_ID As String = "store_service_center_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const TABLE_KEY_NAME As String = "claim_id"
    Public Const DSNAME As String = "LIST"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_CLAIMID As String = "claimid"
    Public Const COL_NAME_REDO_CLAIM_ID As String = "redo_claim_id"
    Public Const COL_NAME_CERT_ITEM_COVERAGE_ID As String = "cert_item_coverage_id"
    Public Const COL_NAME_CERT_ITEM_ID As String = "cert_item_id"
    Public Const COL_NAME_BEGIN_DATE As String = "begin_date"
    Public Const COL_NAME_END_DATE As String = "end_date"
    Public Const COL_NAME_USER_ID As String = "user_id"
    'Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_RISK_TYPE_ID As String = "original_risk_type_id"
    Public Const COL_NAME_CLAIM_ACTIVITY_ID As String = "claim_activity_id"
    Public Const COL_NAME_DENIED_REASON_ID As String = "denied_reason_id"
    Public Const COL_NAME_DENIED_REASONS As String = "denied_reasons"
    Public Const COL_NAME_COMMENT_TYPE_ID As String = "comment_type_id"
    Public Const COL_NAME_REASON_CLOSED_ID As String = "reason_closed_id"
    Public Const COL_NAME_REPAIR_CODE_ID As String = "repair_code_id"
    Public Const COL_NAME_PARTS_CODE_ID As String = "parts_code_id"
    Public Const COL_NAME_SYMPTOM_CODE_ID As String = "symptom_code_id"
    Public Const COL_NAME_LOANER_CENTER_ID As String = "loaner_center_id"
    Public Const COL_NAME_CLAIM_GROUP_ID As String = "claim_group_id"
    Public Const COL_NAME_CAUSE_OF_LOSS_ID As String = "cause_of_loss_id"
    Public Const COL_NAME_METHOD_OF_REPAIR_ID As String = "method_of_repair_id"
    Public Const COL_NAME_USE_RECOVERIES_ID As String = "use_recoveries_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_MASTER_CLAIM_NUMBER As String = "master_claim_number"
    Public Const COL_NAME_CALLER_TAX_NUMBER As String = "caller_tax_number"
    Public Const COL_NAME_BATCH_NUMBER As String = "batch_number"
    Public Const COL_NAME_CLAIM_AUTH_TYPE_ID = "claim_auth_type_id"
    Public Const COL_NAME_IS_LAWSUIT_ID = "is_lawsuit_id"
    Public Const COL_NAME_FULFILMENT_METHOD_XCD = "FULFILMENT_METHOD_XCD"
    Public Const COL_NAME_BANK_INFO_ID = "BANK_INFO_ID"

    'BEGIN - Ravi - Manually added code for the Display Only nature of iteration 1

    Public Const COL_NAME_CERTIFICATE_NUMBER As String = "certificate_number"
    Public Const COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const COL_NAME_TRACKING_NUMBER As String = "tracking_number"
    Public Const COL_NAME_CERT_NUMBER_WS As String = "cert.cert_number"
    Public Const COL_NAME_USER_NAME As String = "user_name"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_DEALER As String = "dealer"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const COL_NAME_COUNTRY_CODE As String = "country_code"
    Public Const COL_NAME_LAST_OPERATOR_NAME As String = "last_operator_name"
    Public Const COL_NAME_CLAIMS_ADJUSTER_NAME As String = "claims_adjuster_name"
    Public Const COL_NAME_CUSTOMER_NAME As String = "cust_name"
    Public Const COL_NAME_CUSTOMER_NAME_1 As String = "customer_name"
    Public Const COL_NAME_RISK_TYPE As String = "risk_type"
    Public Const COL_NAME_CLAIM_ACTIVITY As String = "claim_activity"
    Public Const COL_NAME_SERVICE_CENTER As String = "service_center"
    Public Const COL_NAME_SERVICE_CENTER_NAME As String = "description"
    Public Const COL_NAME_REASON_CLOSED As String = "reason_closed"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_NOTIFICATION_TYPE_ID As String = "notification_type_id"
    Public Const COL_NAME_WHO_PAYS_ID As String = "who_pays_id"
    Public Const COL_NAME_COVERAGE_TYPE As String = "coverage_type"
    Public Const COL_NAME_REPAIR_CODE As String = "repair_code"
    Public Const COL_NAME_PARTS_CODE As String = "parts_code"
    Public Const COL_NAME_SYMPTOM_CODE As String = "symptom_code"
    Public Const COL_NAME_LOANER_CENTER As String = "loaner_center"
    Public Const COL_NAME_CAUSE_OF_LOSS As String = "cause_of_loss"
    Public Const COL_NAME_METHOD_OF_REPAIR As String = "method_of_repair"
    Public Const COL_NAME_REPAIR_SHORT_DESC As String = "repair_short_desc"
    Public Const COL_NAME_SERVICE_CENTER_CODE As String = "code"
    Public Const COL_NAME_DEFECT_REASON As String = "defect_reason"
    Public Const COL_NAME_TECHNICAL_REPORT As String = "technical_report"
    Public Const COL_NAME_EXPECTED_REPAIR_DATE As String = "expected_repair_date"
    Public Const COL_NAME_CONTACT_INFO_ID As String = "contact_info_id"
    Public Const COL_NAME_PRODUCT_DESCRIPTION_WS As String = "pc.description"
    Public Const COL_NAME_MANUFACTURER_DESCRIPTION_WS As String = "manufacturer.description"
    Public Const COL_NAME_CLAIM_TYPE_WS As String = "cmv.claim_type_id"
    Public Const COL_NAME_CLAIM_EXTENDED_STATUS_WS As String = "max_claim_status.list_item_id"
    Public Const COL_NAME_CLAIM_EXTENDED_STATUS_OWNER_WS As String = "max_claim_status.owner_id"
    Public Const COL_NAME_CLAIM_STATUS As String = "status_code"
    Public Const COL_NAME_CLAIM_STATUS_WS As String = "cmv.status_code"
    Public Const COL_NAME_DEALER_REFERENCE As String = "dealer_reference"
    Public Const COL_NAME_POS As String = "point_of_sale"
    Public Const COL_NAME_DEDUCTIBLE_COLLECTED As String = "deductible_collected"
    Public Const COL_NAME_USE_PRE_INVOICE_PROCESS As String = "UsePreInvoice"
    Public Const COL_NAME_CLAIM_EXTENDED_STATUS_ID As String = "claim_extended_status_id"
    Public Const COL_NAME_OWNER_ID As String = "owner_id"

    Public Const DYNAMIC_ROW_NUMBER_PLACE_HOLDER As String = "--dynamic_row_number"
    Public Const DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER2 As String = "--dynamic_where_clause2"
    Public Const DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER3 As String = "--dynamic3_where_clause3"
    Public Const DYNAMIC_COUNT_PLACE_HOLDER As String = "--dynamic_count_number"

    'END - Ravi - Manually added code for the Display Only nature of iteration 1

    Public Const COL_NAME_CERTIFICATE_ID As String = "cert_id"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_CLAIM_NUMBER_WS As String = "cmv.claim_number"
    Public Const COL_NAME_CLAIMNUMBER_WS As String = "ClaimNumber"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_IMEI_NUMBER As String = "imei_number"
    Public Const COL_NAME_STATUS_CODE As String = "status_code"
    Public Const COL_NAME_CONTACT_NAME As String = "contact_name"
    Public Const COL_NAME_CALLER_NAME As String = "caller_name"
    Public Const COL_NAME_PROBLEM_DESCRIPTION As String = "problem_description"
    Public Const COL_NAME_SPECIAL_INSTRUCTION As String = "special_instruction"
    Public Const COL_NAME_AUTHORIZED_AMOUNT As String = "authorized_amount"
    Public Const COL_NAME_AUTHORIZED_AMOUNT_WS As String = "cmv.authorized_amount"
    Public Const COL_NAME_REPAIR_ESTIMATE As String = "repair_estimate"
    Public Const COL_NAME_LIABILITY_LIMIT As String = "liability_limit"
    Public Const COL_NAME_DEDUCTIBLE As String = "deductible"
    Public Const COL_DEDUCT_BY_MFG As String = "deduct_by_mfg"
    Public Const COL_NAME_DEDUCTIBLE_PERCENT As String = "deductible_percent"
    Public Const COL_NAME_DEDUCTIBLE_PERCENT_ID As String = "deductible_by_percent_id"
    Public Const COL_NAME_DED_COLLECTION_METHOD_ID As String = "ded_coll_method_id"
    Public Const COL_NAME_DED_COLLECTION_CC_AUTH_CODE As String = "ded_coll_cc_auth_code"
    Public Const COL_NAME_CLAIMS_ADJUSTER As String = "claims_adjuster"
    Public Const COL_NAME_REPAIR_DATE As String = "repair_date"
    Public Const COL_NAME_LOSS_DATE As String = "loss_date"
    Public Const COL_NAME_INVOICE_PROCESS_DATE As String = "invoice_process_date"
    Public Const COL_NAME_CLAIM_CLOSED_DATE As String = "claim_closed_date"
    Public Const COL_NAME_LOANER_RETURNED_DATE As String = "loaner_returned_date"
    Public Const COL_NAME_FOLLOWUP_DATE As String = "followup_date"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_SVC_REF_NUMBER As String = "svc_ref_number"
    Public Const COL_NAME_SVC_REFERENCE_NUMBER As String = "svc_reference_number"
    Public Const COL_NAME_AUTHORIZATION_STATUS_ID As String = "claim_authorization_status_id"
    Public Const COL_NAME_AUTHORIZATION_NUMBER_WS As String = "cmv.authorization_number"
    Public Const COL_NAME_CLAIM_CREATED_DATE As String = "cmv.created_date"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_VISIT_DATE As String = "visit_date"
    Public Const COL_NAME_PICKUP_DATE As String = "pick_up_date"
    Public Const COL_CONTACT_SALUTATION_ID As String = "contact_salutation_id"
    Public Const COL_CALLER_SALUTATION_ID As String = "caller_salutation_id"
    Public Const COL_NAME_SPARE_PARTS As String = "spare_parts"
    Public Const COL_NAME_SELECTED As String = "selected"
    Public Const COL_NAME_TOTAL_PAID As String = "total_paid"
    Public Const COL_NAME_POLICY_NUMBER As String = "policy_number"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_STATUS_DATE As String = "status_date"
    Public Const COL_NAME_REPORTED_DATE As String = "reported_date"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_REVERSE_LOGISTICS_ID As String = "reverse_logistics_id"
    Public Const COL_NAME_ITEM_MODEL As String = "item_model"
    Public Const COL_NAME_ITEM_MODEL_WS As String = "ci.model"
    Public Const COL_NAME_CURRENT_ODOMETER As String = "current_odometer"
    Public Const COL_NAME_NEW_DEVICE_SKU As String = "new_device_sku"
    Public Const COL_NAME_SKU_NUMBER As String = "sku_number"
    Public Const COL_NAME_BATCH_NUMBER_WS As String = "claim.batch_number"
    Public Const COL_NAME_SERIAL_NUMBER_WS As String = "ci.serial_number"
    Public Const COL_NAME_WORK_PHONE As String = "cert.work_phone"
    Public Const COL_NAME_SC_TAT As String = "elita.elp_claims.getServiceCenterTAT(cmv.claim_id)"
    Public Const COL_NAME_HOME_PHONE As String = "home_phone"
    Public Const COL_NAME_CLAIM_PAID_AMOUNT As String = "claim_paid_amount"
    Public Const COL_NAME_DEVICE_RECEPTION_DATE As String = "device_reception_date"
    Public Const COL_NAME_BONUS_TOTAL As String = "bonus_total"
    Public Const COL_NAME_DEVICE_TYPE As String = "device_type"
    Public Const COL_NAME_CASE_ID As String = "case_id"
    Public Const COL_NAME_REM_AUTH_NUMBER As String = "REM_AUTH_NUMBER"

    Public Const COL_NAME_ISSUE_TYPE As String = "issue_type"
    Public Const COL_NAME_ISSUE_STATUS As String = "issue_status"
    Public Const COL_NAME_ISSUE_TYPE_CODE As String = "code"
    Public Const COL_NAME_ISSUE_STATUS_CODE As String = "entity_issue_status_XCD"


    '10/12/06 - ALR - Calculated Columns
    Public Const COL_CAL_PAYMENT_AMOUNT As String = "payment_amount"
    Public Const COL_CAL_RESERVE_AMOUNT As String = "reserve_amount"

    '11/08/07 - PM - Discount Columns
    Public Const COL_NAME_DISCOUNT_AMOUNT As String = "discount_amount"
    Public Const COL_NAME_DISCOUNT_PERCENT As String = "discount_percent"

    Public Const COL_NAME_CLAIM_STATUSES_COUNT As String = "claim_statuses_count"

    Public Const COL_NAME_MGR_AUTH_AMOUNT_FLAG As String = "mgr_auth_amount_flag"

    'REQ-791
    Public Const COL_NAME_SALVAGE_AMOUNT As String = "salvage_amount"

    'REQ-792
    Public Const COL_NAME_FRAUDULENT As String = "fraudulent"
    Public Const COL_NAME_COMPLAINT As String = "complaint"

    Public Const COL_NAME_MOBILE_NUMBER As String = "mobile_number"

    'REQ Work Queue - Claim Locking
    Public Const COL_NAME_IS_LOCKED As String = "IS_LOCKED"
    Public Const COL_NAME_LOCKED_BY As String = "LOCKED_BY"
    Public Const COL_NAME_LOCKED_ON As String = "LOCKED_ON"

    Public Const COL_NAME_BONUS As String = "bonus"
    Public Const COL_NAME_BONUS_TAX As String = "bonus_tax"
    Public Const COL_NAME_IS_CLAIM_CHILD As String = "is_claim_child"

    'REQ-6230
    Public Const COL_NAME_PURCHASE_PRICE As String = "Purchase_Price"
    Public Const COL_NAME_INDIX_ID As String = "IndixId"

    Public Const COL_NAME_IS_CLAIM_READ_ONLY As String = "is_claim_readonly"

    Public Const COL_NAME_FULFILLMENT_PROVIDER_TYP As String = "fulfillment_provider_xcd"

    Public Const COL_NAME_LOANER_REQUESTED_XCD As String = "loaner_requested_xcd"

    'SP Parameter Names
    Public Const PAR_NAME_COMPANY As String = "p_company_id"
    Public Const PAR_NAME_CLAIM_NUMBER As String = "p_claim_number"
    Public Const PAR_NAME_CLAIM_NUMBER_INPUT As String = "p_claim_number_input"
    Public Const PAR_NAME_COVERAGE_CODE_INPUT As String = "p_coverage_code_input"
    Public Const PAR_NAME_UNIT_NUMBER_INPUT As String = "p_unit_number_input"
    Public Const PAR_NAME_CLAIM_GROUP_ID As String = "p_claim_group_id"
    Public Const PAR_NAME_RETURN As String = "p_return"
    Public Const PAR_NAME_EXCEPTION_MSG As String = "p_exception_msg"
    Public Const PAR_SERVICE_CENTER As String = "V_SERVICE_CENTER_ID"
    Public Const PAR_USER_ID As String = "V_USER_ID"
    Public Const PAR_BATCH_NUMBER As String = "V_BATCH_NUMBER"
    Public Const PAR_LANGUAGE_ID As String = "V_LANGUAGE_ID"
    Public Const PAR_CLAIMS As String = "V_CLAIMS"
    Public Const PAR_INVOICE_TRANS As String = "V_INVOICE_TRANS_ID"
    Public Const PAR_NAME_CERT_NUMBER As String = "p_cert_number"
    Public Const PAR_NAME_SERIAL_NUMBER As String = "p_serial_number"
    Public Const PAR_NAME_LANGUAGE_ID As String = "language_id"

    Public Const PAR_NAME_SINGLE_AUTH_ID As String = "SINGLE_AUTH_ID"
    Public Const PAR_NAME_MULTIPLE_AUTH_ID As String = "MULTIPLE_AUTH_ID"
    Public Const PAR_NAME_AUTH_VOID_ID As String = "AUTH_VOID_ID"
    Public Const PAR_NAME_EQUIPMENT_TYPE_CLAIMED_ID As String = "EQUIPMENT_TYPE_CLAIMED_ID"
    Public Const PAR_NAME_SERVICE_LEVEL_CODE As String = "SERVICE_LEVEL_CODE"
    Public Const PAR_NAME_REPLACEMENT_PART_SKU As String = "REPLACEMENT_PART_SKU"
    Public Const PAR_NAME_REPLACED_SKU As String = "REPLACED_SKU"
    Public Const PAR_NAME_SERVICE_LEVEL_ID As String = "SERVICE_LEVEL_ID"
    Public Const PAR_NAME_CLAIMED_MANUFACTURER_ID As String = "CLAIMED_MANUFACTURER_ID"
    Public Const PAR_NAME_CLAIMED_SKU As String = "CLAIMED_SKU"
    Public Const PAR_NAME_CLAIMED_MODEL As String = "CLAIMED_MODEL"
    Public Const PAR_NAME_EXTENDED_STATUS_ID As String = "EXTENDED_STATUS_ID"

    Public Const PAR_NAME_CMD As String = "p_cmd"
    Public Const PAR_NAME_CLAIM_IDs As String = "p_claim_ids"
    Public Const PAR_NAME_COMMENTs As String = "p_comments"
    Public Const PAR_NAME_RISK_TYPE_IDs As String = "p_risk_type_ids"
    Public Const PAR_NAME_COMPANY_GROUP_ID As String = "p_company_group_id"
    Public Const PAR_NAME_CLAIM_AUTH_TYPE_ID As String = "p_claim_type_id"

    Public Const PAR_NAME_IP_CLAIM_ID As String = "pi_claim_id"
    Public Const PAR_NAME_IP_CLAIM_NUMBER As String = "pi_claim_number"
    Public Const PAR_NAME_IP_COMPANY_ID As String = "pi_company_id"
    Public Const PAR_NAME_IP_COMPANY_CODE As String = "pi_company_code"
    Public Const PAR_NAME_IP_DEALER_ID As String = "pi_dealer_id"
    Public Const PAR_NAME_IP_DEALER_CODE As String = "pi_dealer_code"
    Public Const PAR_NAME_IP_SERIAL_NUMBER As String = "pi_serial_number"
    Public Const PAR_NAME_IP_NETWORK_ID As String = "pi_network_id"
    Public Const PAR_NAME_OP_CLAIM_AUTH_TYPE_ID As String = "po_claim_auth_type_id"
    Public Const PAR_NAME_OP_CLAIM_ID As String = "po_claim_id"
    Public Const PAR_NAME_OP_IS_CLAIM_LOCKED As String = "po_is_claim_locked"
    Public Const PAR_NAME_OP_CLAIM_LOCK_BY As String = "po_claim_locked_by"
    Public Const PAR_NAME_OP_RETURN As String = "po_return"
    Public Const PAR_NAME_OP_EXCEPTION_MESSAGE As String = "po_exception_msg"

    'REQ-6230
    Public Const PAR_NAME_OP_COUNTRY_CODE As String = "po_country_code"

    'Sort By Default
    Public Const SORT_BY_CLAIM_NUMBER As String = "clnum"
    Public Const SORT_BY_FU_DATE As String = "cfdat, cfsvn, cfcln"

    Public Const SORT_BY_SC_TAT As String = "sc_tat"
    Public Const SORT_ORDER_ASC As String = "ASC"
    Public Const SORT_ORDER_DESC As String = "DESC"

    Public Const COL_NAME_SHIPPING_INFO_ID As String = "shipping_info_id"

    Public Const TABLE_NAME_OMBILE As String = "OpenMobile_Verify_Claim"

    'WS 
    Public Const WS_SORT_ORDER_ASC As Integer = 1
    Public Const WS_SORT_ORDER_DESC As Integer = 2
    Public Const WS_SORT_BY_CLAIM_NUMBER As Integer = 1
    Public Const WS_SORT_BY_ITEM_MANUFACTURER As Integer = 2
    Public Const WS_SORT_BY_ITEM_ITEM_MODEL As Integer = 3
    Public Const WS_SORT_BY_CLAIM_STATUS As Integer = 4
    Public Const WS_PAR_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const WS_PAR_LANGUAGE_ID As String = "language_id"
    Public Const WS_PAR_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_LOW_LIMIT As String = "low_limit"
    Public Const COL_NAME_HIGH_LIMIT As String = "high_limit"

    'WS 
    Public Const WS_G_SORT_BY_VISIT_DATE As Integer = 1
    Public Const WS_G_SORT_BY_CLAIM_NUMBER As Integer = 2
    Public Const WS_G_SORT_BY_AUTHORIZATION_NUMBER As Integer = 3
    Public Const WS_G_SORT_BY_CLAIM_CREATED_DATE As Integer = 4
    Public Const WS_G_SORT_BY_CERTIFICATE_NUMBER As Integer = 5
    Public Const WS_G_SORT_BY_PRODUCT_DESCRIPTION As Integer = 6
    Public Const WS_G_SORT_BY_ITEM_MODELE As Integer = 7
    Public Const WS_G_SORT_BY_ITEM_MANUFACTURER_DESCRIPTION As Integer = 8
    Public Const WS_G_SORT_BY_CUSTOMER_NAME As Integer = 9
    Public Const WS_G_SORT_BY_CLAIM_TYPE As Integer = 10
    Public Const WS_G_SORT_BY_CLAIM_EXTENDED_STATUS_ORDER As Integer = 11
    Public Const WS_G_SORT_BY_CLAIM_EXTENDED_STATUS_OWNER As Integer = 12
    Public Const WS_G_SORT_BY_AUTHORIZED_AMOUNT As Integer = 13
    Public Const WS_G_SORT_BY_ADJUSTED_CLAIM_STATUS As Integer = 14
    Public Const WS_G_SORT_BY_BATCH_NUMBER As Integer = 15
    Public Const WS_G_SORT_BY_SERIAL_NUMBER As Integer = 16
    Public Const WS_G_SORT_BY_WORK_PHONE As Integer = 17
    Public Const WS_G_SORT_BY_SC_TAT As Integer = 18
    Public Const WS_G_SORT_BY_HOME_PHONE As Integer = 19
    Public Const WS_G_SORT_BY_LOSS_DATE As Integer = 20
    Public Const WS_G_SORT_BY_CLAIM_PAID_AMOUNT As Integer = 21
    Public Const WS_G_SORT_BY_BONUS_TOTAL As Integer = 22

    Public Const COL_NAME_CLAIM_IS_SPECIAL_SERVICE_ID As String = "claim_is_special_service_id"
    Public Const CMD_APPROVE As String = "APP"
    Public Const CMD_REJECT As String = "REJ"


    'Claim Logistics
    Public Const TOTAL_INPUT_PARAM_WS As Integer = 8
    Public Const TOTAL_INPUT_PARAM_WS_1 As Integer = 8
    Public Const TOTAL_OUTPUT_PARAM_WS As Integer = 4
    Public Const TOTAL_OUTPUT_PARAM_WS_1 As Integer = 16

    Public Const SP_PARAM_NAME__CUSTOMER_IDENTIFIER As String = "p_customer_identifier"
    Public Const SP_PARAM_NAME__IDENTIFIER_TYPE As String = "p_identifier_type"
    Public Const SP_PARAM_NAME__SYSTEM_USER_ID As String = "p_system_user_id"
    Public Const SP_PARAM_NAME__DEALER_ID As String = "p_dealer_id"
    Public Const SP_PARAM_NAME__DEALER_CODE As String = "p_dealer_code"
    Public Const SP_PARAM_NAME__COVERAGE_CODE As String = "p_coverage_code"
    Public Const SP_PARAM_NAME__SERVICE_CENTER_CODE As String = "p_service_center_code"
    Public Const SP_PARAM_NAME__SERIAL_NUMBER As String = "p_serial_number"
    Public Const SP_PARAM_NAME__MAKE As String = "p_make"
    Public Const SP_PARAM_NAME__MODEL As String = "p_model"
    Public Const SP_PARAM_NAME__CAUSE_OF_LOSS_CODE As String = "p_cause_of_loss_code"
    Public Const SP_PARAM_NAME__COUNTRY_CODE As String = "p_country_code"
    Public Const SP_PARAM_NAME__REGION_CODE As String = "p_region_code"
    Public Const SP_PARAM_NAME__ADDRESS_TYPE_CODE As String = "p_address_type_code"
    Public Const SP_PARAM_NAME__PAYMENT_METHOD As String = "p_payment_method"
    Public Const SP_PARAM_NAME__BILLING_ZIP_CODE As String = "p_billing_zip_code"
    Public Const SP_PARAM_NAME__LANGUAGE_ISO_CODE As String = "p_language_ISO_code"
    Public Const SP_PARAM_NAME__RETURN As String = "p_return"
    Public Const SP_PARAM_NAME__EXCEPTION_MSG As String = "p_exception_msg"
    Public Const SP_PARAM_NAME__CLAIM_STATUS_INFO As String = "p_claim_status_info"
    Public Const SP_PARAM_NAME__EXTENDED_CLAIM_STATUS_INFO As String = "p_extended_status_info"
    Public Const SP_PARAM_NAME__RESPONSE_STATUS As String = "p_response_status"
    Public Const SP_PARAM_NAME__COVERAGE_TYPE_ID As String = "p_coverage_type_id"
    Public Const SP_PARAM_NAME__CERT_ITEM_COVERAGE_ID As String = "p_cert_item_coverage_id"
    Public Const SP_PARAM_NAME__SERVICE_CENTER_ID As String = "p_service_center_id"
    Public Const SP_PARAM_NAME__CAUSE_OF_LOSS_ID As String = "p_cause_of_loss_id"
    Public Const SP_PARAM_NAME__HONE_PHONE As String = "p_home_phone"
    Public Const SP_PARAM_NAME__WORK_PHONE As String = "p_work_phone"
    Public Const SP_PARAM_NAME__ADDRESS_TYPE_ID As String = "p_address_type_id"
    Public Const SP_PARAM_NAME__DEDUCTIBLE As String = "p_deductible"
    Public Const SP_PARAM_NAME__COUNTRY_ID As String = "p_country_id"
    Public Const SP_PARAM_NAME__REGION_ID As String = "p_region_id"
    Public Const SP_PARAM_NAME__PAYMENT_METHOD_ID As String = "p_payment_method_id"
    Public Const SP_PARAM_NAME__DEFAULT_CLAIM_STATUS_FOR_EXT_SYS_CODE As String = "p_default_claim_sta_for_ext"
    Public Const SP_PARAM_NAME__INVALID_SERIAL_NUMBER As String = "p_invalid_serial_number"
    Public Const SP_PARAM_NAME__INVALID_MAKE_MODEL As String = "p_invalid_make_model"
    Public Const SP_PARAM_NAME__CERTIFICATE_NUMBER As String = "p_certificate_number"
    Public Const SP_PARAM_NAME__DATE_OF_LOSS As String = "p_date_of_loss"

    Public Const P_RETURN = 0
    Public Const P_EXCEPTION_MSG = 1
    Public Const P_CURSOR_CLAIM_STATUS_INFO = 2
    Public Const P_CURSOR_EXTENDED_STATUS_INFO = 3
    Public Const P_CURSOR_RESPONSE_STATUS = 4
    Public Const P_COVERAGE_TYPE_ID = 2
    Public Const P_CERT_ITEM_COVERAGE_ID = 3
    Public Const P_SERVICE_CENTER_ID = 4
    Public Const P_CAUSE_OF_LOSS_ID = 5
    Public Const P_COUNTRY_ID = 6
    Public Const P_REGION_ID = 7
    Public Const P_HOME_PHONE = 8
    Public Const P_WORK_PHONE = 9
    Public Const P_ADDRESS_TYPE_ID = 10
    Public Const P_DEDUCTIBLE = 11
    Public Const P_PAYMENT_METHOD_ID = 12
    Public Const P_CLAIM_STATUS_FOR_EXT_SYS_CODE = 13
    Public Const P_INVALID_SERIAL_NUMBER = 14
    Public Const P_INVALID_MAKE_MODEL = 15
    Public Const P_CURSOR_RESPONSE_STATUS_1 = 16

    Public Const DATASET_NAME__CLAIM_CHECK_RESPONSE As String = "ClaimCheckResponse"
    Public Const DATASET_NAME__SUBMIT_CLAIM_RESPONSE As String = "SubmitClaimResponse"
    Public Const TABLE_NAME__GET_CLAIM_STATUS_INFO_RESPONSE As String = "GetClaimStatusInfoResponse"
    Public Const TABLE_NAME__GET_CLAIM_EXT_STATUS_INFO_RESPONSE As String = "Extended_Status_History"
    Public Const TABLE_NAME__RESPONSE_STATUS As String = "ResponseStatus"
    Public Const ELEMENT_NAME_EXTENDED_STATUSES As String = "Extended_Statuses"

    Public Const CLAIM_LOCK_STATUS As String = "Claim_lock_status"

    'REQ-863
    Public Const COL_NAME_SKU As String = "sku"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"

    'MasterClaimProcessing LookupList
    Public Const MasterClmProc_ANYMC As String = "ANYMC"
    Public Const MasterClmProc_BYDOL As String = "BYDOL"
    Public Const MasterClmProc_NONE As String = "NONE"


    Public Const TABLE_NAME_COMMENT As String = "ELP_COMMENT"
    Public Const TABLE_NAME_CLAIMSTAT As String = "ELP_CLAIM_STATUS"
    Public Const TABLE_NAME_PARTSINFO As String = "ELP_PARTS_INFO"
    Public Const TABLE_NAME_PARTSDESC As String = "ELP_PARTS_DESCRIPTION"

    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_TAX_ID As String = "tax_id"

    Public Const COL_NAME_REG_ITEM_CURRENT_RETAIL_PRICE As String = "current_retail_price"
    Public Const COL_NAME_DEVICE_ACTIVATION_DATE As String = "device_activation_date"
    Public Const COL_NAME_EMPLOYEE_NUMBER As String = "employee_number"

    Public Const DepreciationSchedule_Usage_Default As String = "DEP_SCH_USAGE-LIABILITY_LIMIT"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "BO"

#Region " Constants "

    Public Const COL_ID_NAME As String = "ID"
    Public Const COL_DESCRIPTION_NAME As String = "DESCRIPTION"
    Public Const COL_CODE_NAME As String = "CODE"

    Public Const METHOD_OF_REPAIR__REPLACEMENT As String = "R"
    Public Const CLAIM_AUTH_TYPE_SINGLE As String = "S"

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id(ByVal Row As DataRow) As Guid
        Get
            If Row(ClaimDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
    End Property

    Public ReadOnly Property IsNew(ByVal Row As DataRow) As Boolean
        Get
            'Me.CheckDeleted()
            Return (Row.RowState = DataRowState.Added)
        End Get
    End Property


    Public Property ClaimGroupId(ByVal Row As DataRow) As Guid
        Get
            If Row(ClaimDAL.COL_NAME_CLAIM_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            Me.SetValue(Row, ClaimDAL.COL_NAME_CLAIM_GROUP_ID, Value)
        End Set
    End Property

    Public Property CompanyId(ByVal Row As DataRow) As Guid
        Get
            If Row(ClaimDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            Me.SetValue(Row, ClaimDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property ClaimNumber(ByVal Row As DataRow) As String
        Get
            If Row(ClaimDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            Me.SetValue(Row, ClaimDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    Public Property MasterClaimNumber(ByVal Row As DataRow) As String
        Get
            If Row(ClaimDAL.COL_NAME_MASTER_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_MASTER_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            Me.SetValue(Row, ClaimDAL.COL_NAME_MASTER_CLAIM_NUMBER, Value)
        End Set
    End Property

    Public ReadOnly Property MethodOfRepairCode(ByVal Row As DataRow) As String
        Get
            If Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return GetCodeFromId(LK_METHODS_OF_REPAIR, New Guid(CType(Row(ClaimDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte())))
            End If
        End Get

    End Property

    Public ReadOnly Property CLaimAuthTypeCode(ByVal Row As DataRow) As String
        Get
            If Row(ClaimDAL.COL_NAME_CLAIM_AUTH_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return GetCodeFromId(LK_CLAIM_AUTHORIZATION_TYPE, New Guid(CType(Row(ClaimDAL.COL_NAME_CLAIM_AUTH_TYPE_ID), Byte())))
            End If
        End Get

    End Property

    Public Property FulfillmentProviderType(ByVal Row As DataRow) As String
        Get
            If Row(ClaimDAL.COL_NAME_FULFILLMENT_PROVIDER_TYP) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimDAL.COL_NAME_FULFILLMENT_PROVIDER_TYP), String)
            End If
        End Get
        Set(ByVal Value As String)
            Me.SetValue(Row, ClaimDAL.COL_NAME_FULFILLMENT_PROVIDER_TYP, Value)
        End Set
    End Property



#End Region

#Region "Methods"

    'this generic method should be called by all BO's to set their properties
    Protected Sub SetValue(ByVal Row As DataRow, ByVal columnName As String, ByVal newValue As Object)
        If Not newValue Is Nothing And Row(columnName) Is DBNull.Value Then
            'new value is something and old value is DBNULL
            If newValue.GetType Is GetType(BooleanType) Then
                '- BooleanType, special case - convert to string Y or N
                If CType(newValue, BooleanType).Value Then
                    Row(columnName) = "Y"
                Else
                    Row(columnName) = "N"
                End If
            ElseIf newValue.GetType Is GetType(Guid) Then
                'ElseIf newValue.GetType Is GetType(Guid) Then
                If Not newValue.Equals(Guid.Empty) Then
                    Row(columnName) = CType(newValue, Guid).ToByteArray
                End If
            ElseIf newValue.GetType Is GetType(Byte()) Then
                Row(columnName) = CType(newValue, Byte())
            ElseIf newValue.GetType Is GetType(DateType) Then
                Row(columnName) = CType(newValue.ToString, DateTime)
            ElseIf newValue.GetType Is GetType(Double) Then
                Row(columnName) = CType(newValue, Double)
            ElseIf newValue.GetType Is GetType(Decimal) Then
                Row(columnName) = CType(newValue, Decimal)
            Else
                '- DateType, DecimalType, etc... all our other custome types
                '- see if 'newValue Type' has a Value property (only our custom types do)
                Dim propInfo As System.Reflection.PropertyInfo = newValue.GetType.GetProperty("Value")
                If Not (propInfo Is Nothing) Then
                    '- call the Value property to extract the native .NET type (double, decimal, etc...)
                    newValue = propInfo.GetValue(newValue, Nothing)
                End If

                '- let the DataColumn convert the value to its internal data type
                Row(columnName) = newValue
            End If
        ElseIf Not newValue Is Nothing Then
            'new value is something and old value is also something
            '- convert current value to a string
            Dim currentValue As Object = Row(columnName)
            If newValue.GetType Is GetType(Guid) Then
                currentValue = New Guid(CType(currentValue, Byte()))
            ElseIf newValue.GetType Is GetType(Byte()) Then
                currentValue = CType(currentValue, Byte())
            Else
                currentValue = currentValue.ToString
                '- create an array of types containing one type, the String type
                Dim types() As Type = {GetType(String)}
                '- see if the 'newValue Type' has a 'Parse(String)' method taking a String parameter
                Dim miMethodInfo As System.Reflection.MethodInfo = newValue.GetType.GetMethod("Parse", types)
                If Not miMethodInfo Is Nothing Then
                    '- it does have a Parse method, newValue must be a number type.
                    '- extract the current value as a string
                    Dim args() As Object = {Row(columnName).ToString}
                    '- call the Parse method to convert the currentValue string to a number
                    currentValue = miMethodInfo.Invoke(newValue, args)
                End If
            End If
            '- only dirty the BO if new value is different from old value
            If Not newValue.Equals(currentValue) Then
                If newValue.GetType Is GetType(BooleanType) Then
                    '- BooleanType, special case - convert to string Y or N
                    If CType(newValue, BooleanType).Value Then
                        newValue = "Y"
                    Else
                        newValue = "N"
                    End If
                ElseIf newValue.GetType Is GetType(Byte()) Then
                    newValue = CType(newValue, Byte())
                Else
                    '- DateType, DecimalType, etc... all our other custome types
                    '- see if 'newValue Type' has a Value property (only our custom types do)
                    Dim propInfo As System.Reflection.PropertyInfo = newValue.GetType.GetProperty("Value")
                    If Not (propInfo Is Nothing) Then
                        '- call the Value property to extract the native .NET type (double, decimal, etc...)
                        newValue = propInfo.GetValue(newValue, Nothing)
                    End If
                End If
                '- at this point, newValue has a native .NET type
                If newValue.GetType Is GetType(Guid) Then
                    If newValue.Equals(Guid.Empty) Then
                        newValue = DBNull.Value
                    Else
                        newValue = CType(newValue, Guid).ToByteArray
                    End If
                End If
                Row(columnName) = newValue
            End If
        ElseIf newValue Is Nothing And Not Row(columnName) Is DBNull.Value Then
            Row(columnName) = DBNull.Value
        End If
    End Sub


    Private Shared addMutex As New System.Threading.Mutex

    Private Shared Function RetrieveList(ByVal listName As String, Optional ByVal displayNothingSelected As Boolean = True, Optional ByVal orderByColumn As String = COL_DESCRIPTION_NAME) As DataView
        Dim dv As DataView = Nothing

        ' JLR - Added Mutex to avoid concurrency problems
        addMutex.WaitOne()

        Try
            dv = LookupListCache.RetrieveFromCache(listName, displayNothingSelected)
            If (dv Is Nothing) Then
                ' JLR - Uncomment to reproduce concurrency problems. 
                ' System.Threading.Thread.CurrentThread.Sleep(3000)
                dv = LookupListDALNew.Load(listName)
                LookupListCache.AddToCache(listName, dv, displayNothingSelected)
            End If
        Finally
            addMutex.ReleaseMutex()
        End Try



        'Now create an independent copy. So that users can sort and filter without afecting each other
        dv = New DataView(dv.Table)

        'Sort on Description by default
        If dv.Table.Columns.IndexOf(orderByColumn) >= 0 Then
            dv.Sort = orderByColumn
        End If

        Return dv

    End Function

    Public Shared Function DataView(ByVal listName As String, Optional ByVal displayNothingSelected As Boolean = True, Optional ByVal orderByColumn As String = COL_DESCRIPTION_NAME) As DataView
        Return RetrieveList(listName, displayNothingSelected, orderByColumn)
    End Function

    Public Shared Function GetCodeFromId(ByVal listName As String, ByVal id As Guid) As String
        Dim dv As DataView = DataView(listName)
        Dim i As Integer

        For i = 0 To dv.Count - 1
            If New Guid(CType(dv(i)(COL_ID_NAME), Byte())).Equals(id) Then
                Return dv(i)(COL_CODE_NAME).ToString
            End If
        Next

        Return Nothing
    End Function

#End Region

#End Region

#Region "Galaxy"

    Public Class GalaxyClaimNumber
        Public moClaimId As Guid
        Public moGalaxyClaimNumber As String
        Public moCoverageCode As String
        Public moUnitNumber As String

        Public Sub New(ByVal claimId As Guid, ByVal galaxyClaimNumber As String, ByVal coverageCode As String, ByVal unitNumber As String)
            moClaimId = claimId
            moGalaxyClaimNumber = galaxyClaimNumber
            moCoverageCode = coverageCode
            moUnitNumber = unitNumber
        End Sub

    End Class


#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal claimNumber As String, ByVal compId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_CLAIM_NUMBER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_number", claimNumber),
                                            New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, compId)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub WaiveExistingIssues(ByVal claimId As Guid, ByVal IdentificationNumber As String, ByVal RuleCode As String, ByVal IssueCode As String, Optional ByVal isreplacement As String = "Y")

        Dim selectStmt As String = Me.Config("/SQL/WAIVE_EXISTING_ISSUES")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("p_claim_id", claimId.ToByteArray),
                                                         New DBHelper.DBHelperParameter("p_identification_number", IdentificationNumber),
                                                         New DBHelper.DBHelperParameter("p_rule_code", RuleCode),
                                                         New DBHelper.DBHelperParameter("p_issue_code", IssueCode),
                                                         New DBHelper.DBHelperParameter("p_replacement_only", isreplacement)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                             New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer)),
                             New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String), 500)}



        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, parameters, outputParameters)
        If CType(outputParameters(0).Value, Integer) <> 0 Then
            If CType(outputParameters(0).Value, Integer) = 300 Then
                Throw New StoredProcedureGeneratedException("Waiving claim issue generated error: ", outputParameters(1).Value)
            Else
                Dim e As New ApplicationException("Return Value = " & outputParameters(1).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
            End If
        End If

    End Sub
    Public Function LoadListWithManuf(ByVal certItemid As Guid, ByVal begin_date As Date, ByVal end_date As Date) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ACTIVE_CLAIMS_FOR_ENDORSE_WITH_MANUF")
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ITEM_ID, certItemid.ToByteArray),
                                            New OracleParameter(COL_NAME_BEGIN_DATE, begin_date),
                                            New OracleParameter(COL_NAME_BEGIN_DATE, begin_date),
                                            New OracleParameter(COL_NAME_END_DATE, end_date)}

        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Return ds

    End Function

    Public Function LoadListWithOutManuf(ByVal certItemid As Guid, ByVal begin_date As Date, ByVal end_date As Date) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ACTIVE_CLAIMS_FOR_ENDORSE_WITH_NO_MANUF")
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ITEM_ID, certItemid.ToByteArray),
                                            New OracleParameter(COL_NAME_BEGIN_DATE, begin_date),
                                            New OracleParameter(COL_NAME_END_DATE, end_date)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function LoadListByCoverageId(ByVal certItemCoverageId As Guid, ByVal begin_date As Date, ByVal end_date As Date) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ACTIVE_CLAIMS_FOR_ENDORSE_BY_COVERAGE")
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray),
                                            New OracleParameter(COL_NAME_BEGIN_DATE, begin_date),
                                            New OracleParameter(COL_NAME_END_DATE, end_date)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    'Function overload for search without dealer id
    Public Function LoadList(ByVal compIds As ArrayList, ByVal claimNumber As String, ByVal customerName As String,
                             ByVal serviceCenterName As String, ByVal authorizationNumber As String,
                             ByVal authorizedAmount As String, ByVal sortBy As String,
                             ByVal externalUserServiceCenterIds As ArrayList, ByVal serviceCenterIds As ArrayList) As DataSet
        Return LoadList(compIds, claimNumber, customerName, serviceCenterName, authorizationNumber,
                             authorizedAmount, Guid.Empty, sortBy, externalUserServiceCenterIds, serviceCenterIds, Guid.Empty, String.Empty, String.Empty, String.Empty)
    End Function

    Public Function LoadList(ByVal compIds As ArrayList, ByVal claimNumber As String, ByVal customerName As String,
                             ByVal serviceCenterName As String, ByVal svcRefNumber As String,
                             ByVal authorizedAmount As String, ByVal hasPendingAuthId As Guid, ByVal sortBy As String,
                             ByVal externalUserServiceCenterIds As ArrayList, ByVal serviceCenterIds As ArrayList, ByVal dealerId As Guid,
                             ByVal certificateNumber As String, ByVal Status As String, ByVal networkId As String, Optional ByVal trackingNumber As String = "", Optional ByVal dealerGroup As String = "",
                             Optional ByVal authorizationNumber As String = "", Optional ByVal claimAuthStatusId As Guid = Nothing) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_DYNAMIC")

        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "c." & Me.COL_NAME_CLAIM_NUMBER & " " & claimNumber
        End If

        If Me.FormatSearchMask(customerName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(c." & Me.COL_NAME_CUSTOMER_NAME & ") " & customerName
        End If

        If (MiscUtil.IsCriteriaSelected(externalUserServiceCenterIds) = True) Then
            whereClauseConditions &= " AND " & Environment.NewLine & MiscUtil.BuildListForSql("c." & Me.COL_NAME_SERVICE_CENTER_ID, externalUserServiceCenterIds)
        End If

        If Me.FormatSearchMask(serviceCenterName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sc." & Me.COL_NAME_SERVICE_CENTER_NAME & ") " & serviceCenterName
        End If

        If Me.FormatSearchMask(certificateNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(cert." & Me.COL_NAME_CERT_NUMBER & ") " & certificateNumber
        End If

        If Me.FormatSearchMask(trackingNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(cl." & Me.COL_NAME_TRACKING_NUMBER & ") " & trackingNumber.ToUpper
        End If

        If (MiscUtil.IsCriteriaSelected(serviceCenterIds) = True) Then

            whereClauseConditions &= " AND " & Environment.NewLine & MiscUtil.BuildListForSql("c." & Me.COL_NAME_SERVICE_CENTER_ID, serviceCenterIds)
        End If

        If Me.FormatSearchMask(svcRefNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(c." & Me.COL_NAME_AUTHORIZATION_NUMBER & ") " & svcRefNumber.ToUpper
        End If

        If Me.FormatSearchMask(authorizedAmount) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "c." & Me.COL_NAME_AUTHORIZED_AMOUNT & " " & authorizedAmount
        End If

        If Me.FormatSearchMask(Status) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(c." & Me.COL_NAME_STATUS_CODE & ") " & Status
        End If

        'if dealer id present
        If Not ((dealerId = Guid.Empty) OrElse (dealerId = Nothing)) Then
            whereClauseConditions &= Environment.NewLine & " AND c.dealer_id = hextoraw(" & MiscUtil.GetDbStringFromGuid(dealerId) & ")"
        End If

        'if HasPendingAuthorizations id present
        If Not ((hasPendingAuthId = Guid.Empty) OrElse (hasPendingAuthId = Nothing)) Then
            whereClauseConditions &= Environment.NewLine & " AND c.has_pending_auth_id = hextoraw(" & MiscUtil.GetDbStringFromGuid(hasPendingAuthId) & ")"
        End If

        If (dealerGroup <> String.Empty AndAlso (Me.FormatSearchMask(dealerGroup))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(dg.code) " & dealerGroup.ToUpper & ""
        End If

        If Me.FormatSearchMask(authorizationNumber) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(cla." & Me.COL_NAME_AUTHORIZATION_NUMBER & ") " & authorizationNumber.ToUpper
        End If

        If Not ((claimAuthStatusId = Guid.Empty) OrElse (claimAuthStatusId = Nothing)) Then
            whereClauseConditions &= Environment.NewLine & " AND cla.claim_authorization_status_id = hextoraw(" & MiscUtil.GetDbStringFromGuid(claimAuthStatusId) & ")"
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COMPANY_ID, compIds, False)

        whereClauseConditions &= Environment.NewLine & " AND  elp_utl_user.Has_access_to_data('" & networkId & "', d.company_id, d.dealer_id)  = 'Y'"

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        If Not IsNothing(sortBy) Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER,
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & sortBy)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadClaimByIssue(ByVal userId As Guid, ByVal languageId As Guid,
                            ByVal issueTypeCode As String, ByVal issueTypeId As Guid,
                            ByVal issueId As Guid?, ByVal issueStatusXcd As String,
                            ByVal claimStatusCode As String, ByVal dealerId As Guid?,
                            ByVal issueAddedFromDate As Date?, ByVal issueAddedToDate As Date?, ByVal networkId As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_ISSUE")
        Dim resultCount As Integer = Me.MAX_NUMBER_OF_ROWS

        Dim inParameters As List(Of DBHelper.DBHelperParameter) = New List(Of DBHelperParameter)()

        inParameters.Add(New DBHelperParameter("pi_user_id", userId.ToByteArray()))
        inParameters.Add(New DBHelperParameter("pi_languageId", languageId.ToByteArray()))
        inParameters.Add(New DBHelperParameter("pi_issue_Type_Id", issueTypeId.ToByteArray()))
        inParameters.Add(New DBHelperParameter("pi_rowcount", resultCount))
        inParameters.Add(New DBHelperParameter("pi_network_id", networkId))

        If (issueId.HasValue AndAlso issueId.Value.Equals(Guid.Empty) = False) Then
            inParameters.Add(New DBHelperParameter("pi_issue_id", issueId.Value.ToByteArray()))
        End If

        If (issueAddedFromDate.HasValue) Then
            inParameters.Add(New DBHelper.DBHelperParameter("pi_issue_date_from", issueAddedFromDate.Value))
        End If

        If (issueAddedToDate.HasValue) Then
            inParameters.Add(New DBHelper.DBHelperParameter("pi_issue_date_to", issueAddedToDate.Value))
        End If

        If (String.IsNullOrEmpty(issueStatusXcd) = False) Then
            inParameters.Add(New DBHelper.DBHelperParameter("pi_issue_status_xcd", issueStatusXcd))
        End If

        If (String.IsNullOrEmpty(claimStatusCode) = False) Then
            inParameters.Add(New DBHelper.DBHelperParameter("pi_claim_status", claimStatusCode))
        End If

        If (dealerId.HasValue) Then
            inParameters.Add(New DBHelperParameter("pi_dealer_id", dealerId.Value.ToByteArray()))
        End If

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}

        Dim ds As New DataSet

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray(), outParameters, ds, Me.TABLE_NAME, True)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadPreviousInProgressClaimCount(ByVal claimId As guid) As Integer
        Dim selectStmt As String = Me.Config("/SQL/PreviousInProgressClaimCount")
        Dim parameters = New DBHelperParameter(){New DBHelperParameter("claim_id", claimId.ToByteArray()),
                                                 New DBHelperParameter("claim_id", claimId.ToByteArray()),
                                                 New DBHelperParameter("claim_id", claimId.ToByteArray())
                                                }
        Dim ds As New DataSet
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        return ds.Tables(0).Rows(0)("claim_count")
    End Function

    Public Function LoadListforImageIndexing(ByVal compIds As ArrayList, ByVal claimStatus As String, ByVal claimNumber As String, ByVal customerName As String,
                             ByVal serviceCenterName As String, ByVal authorizationNumber As String,
                             ByVal authorizedAmount As String, ByVal sortBy As String,
                             ByVal externalUserServiceCenterIds As ArrayList, ByVal serviceCenterIds As ArrayList, ByVal dealerId As Guid, Optional ByVal dealerGroupCode As String = "") As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_FOR_IMAGE_INDEXING")

        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & Me.COL_NAME_CLAIM_NUMBER & " " & claimNumber
        End If

        If claimStatus <> "" AndAlso Me.FormatSearchMask(claimStatus) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "c." & Me.COL_NAME_CLAIM_STATUS & " " & claimStatus
        End If

        If Me.FormatSearchMask(customerName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CUSTOMER_NAME & ") " & customerName
        End If

        If (MiscUtil.IsCriteriaSelected(externalUserServiceCenterIds) = True) Then
            whereClauseConditions &= " AND " & Environment.NewLine & MiscUtil.BuildListForSql("c." & Me.COL_NAME_SERVICE_CENTER_ID, externalUserServiceCenterIds)
        End If

        If Me.FormatSearchMask(serviceCenterName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sc." & Me.COL_NAME_SERVICE_CENTER_NAME & ") " & serviceCenterName
        End If

        If (MiscUtil.IsCriteriaSelected(serviceCenterIds) = True) Then

            whereClauseConditions &= " AND " & Environment.NewLine & MiscUtil.BuildListForSql("c." & Me.COL_NAME_SERVICE_CENTER_ID, serviceCenterIds)
        End If


        If Me.FormatSearchMask(authorizationNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_AUTHORIZATION_NUMBER & ") " & authorizationNumber.ToUpper
        End If

        If Me.FormatSearchMask(authorizedAmount) Then
            whereClauseConditions &= " AND " & Environment.NewLine & Me.COL_NAME_AUTHORIZED_AMOUNT & " " & authorizedAmount
        End If

        'if dealer id present
        If Not ((dealerId = Guid.Empty) OrElse (dealerId = Nothing)) Then
            whereClauseConditions &= Environment.NewLine & " AND c.dealer_id = hextoraw(" & MiscUtil.GetDbStringFromGuid(dealerId) & ")"
        End If

        If (dealerGroupCode <> String.Empty AndAlso (Me.FormatSearchMask(dealerGroupCode))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(dg.code) " & dealerGroupCode.ToUpper & " AND "
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COMPANY_ID, compIds, False)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        If Not IsNothing(sortBy) Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER,
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & sortBy)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadAdjusterList(ByVal userNetworkId As String, ByVal claimNumber As String, ByVal serviceCenterName As String, ByVal authorizationNumber As String,
                                     ByVal claim_status As String, ByVal claimTypeId As Guid, ByVal claimExtendedStatusId As Guid, ByVal AutoApprove As String,
                                     ByVal BeginDate As String, ByVal EndDate As String,
                                     ByVal claimExtendedStatusOwnerId As Guid, ByVal sortOrder As String, ByVal sortBy As String,
                                     ByVal ClaimAdjuster As String, ByVal ClaimAddedBy As String, Optional ByVal scTATLowLimit As LongType = Nothing, Optional ByVal scTATHighLimit As LongType = Nothing) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_CLAIM_ADJUSTER_LIST")
        Dim ds As New DataSet
        Dim da As OracleDataAdapter

        Try
            Dim cmd As OracleCommand = DB_OracleCommand(selectStmt, CommandType.StoredProcedure)
            cmd.BindByName = True
            cmd.Parameters.Add("pi_network_id", OracleDbType.Varchar2).Value = userNetworkId
            cmd.Parameters.Add("pi_claim_number", OracleDbType.Varchar2).Value = claimNumber
            cmd.Parameters.Add("pi_service_center_name", OracleDbType.Varchar2).Value = serviceCenterName
            cmd.Parameters.Add("pi_authorization_number", OracleDbType.Varchar2).Value = authorizationNumber
            cmd.Parameters.Add("pi_claim_status", OracleDbType.Varchar2).Value = claim_status
            If Not claimTypeId.Equals(Guid.Empty) Then
                cmd.Parameters.Add("pi_claim_type_id", OracleDbType.Raw).Value = claimTypeId.ToByteArray
            End If
            If Not claimExtendedStatusId.Equals(Guid.Empty) Then
                cmd.Parameters.Add("pi_claim_extended_status_id", OracleDbType.Raw).Value = claimExtendedStatusId.ToByteArray
            End If
            cmd.Parameters.Add("pi_auto_approve", OracleDbType.Varchar2).Value = AutoApprove
            cmd.Parameters.Add("pi_created_date_From", OracleDbType.Varchar2).Value = BeginDate
            cmd.Parameters.Add("pi_created_date_to", OracleDbType.Varchar2).Value = EndDate
            If Not claimExtendedStatusOwnerId.Equals(Guid.Empty) Then
                cmd.Parameters.Add("pi_claim_ext_status_owner_id", OracleDbType.Raw).Value = claimExtendedStatusOwnerId.ToByteArray
            End If
            cmd.Parameters.Add("pi_sort_order", OracleDbType.Varchar2).Value = sortOrder
            cmd.Parameters.Add("pi_sorted_by", OracleDbType.Varchar2).Value = sortBy
            cmd.Parameters.Add("pi_claim_adjuster", OracleDbType.Varchar2).Value = ClaimAdjuster
            cmd.Parameters.Add("pi_claim_added_by", OracleDbType.Varchar2).Value = ClaimAddedBy
            cmd.Parameters.Add("pi_claim_TAT_low_limit", OracleDbType.Int32).Value = scTATLowLimit
            cmd.Parameters.Add("pi_claim_TAT_high_limit", OracleDbType.Int32).Value = scTATHighLimit
            cmd.Parameters.Add("pi_row_num", OracleDbType.Int32).Value = MAX_NUMBER_OF_ROWS
            cmd.Parameters.Add("po_Error_MSG", OracleDbType.Varchar2, ParameterDirection.Output)
            cmd.Parameters.Add("po_claim_adj_cur", OracleDbType.RefCursor, ParameterDirection.Output)
            da = New OracleDataAdapter(cmd)

            da.Fill(ds, TABLE_NAME_CLMADJ)
            ds.Locale = Globalization.CultureInfo.InvariantCulture
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimsByCommentType(ByVal compIds As ArrayList, ByVal claimNumber As String, ByVal customerName As String,
                     ByVal CommentTypeId As Guid, ByVal authorizationNumber As String,
                     ByVal claimStatus As String, ByVal languageId As Guid, ByVal sortBy As String,
                     ByVal dealerId As Guid, Optional ByVal dealerGroupCode As String = "") As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_CLAIMS_BY_COMMENT_TYPE")

        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND cv." & Environment.NewLine & Me.COL_NAME_CLAIM_NUMBER & " " & claimNumber
        End If

        If claimStatus <> "" AndAlso Me.FormatSearchMask(claimStatus) Then
            whereClauseConditions &= " AND " & Environment.NewLine & Me.COL_NAME_CLAIM_STATUS & " " & claimStatus
        End If

        If Me.FormatSearchMask(customerName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CUSTOMER_NAME & ") " & customerName
        End If

        If Not ((CommentTypeId = Guid.Empty) OrElse (CommentTypeId = Nothing)) Then
            whereClauseConditions &= Environment.NewLine & " AND cm.comment_type_id = hextoraw(" & MiscUtil.GetDbStringFromGuid(CommentTypeId) & ")"
        End If

        If Me.FormatSearchMask(authorizationNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_AUTHORIZATION_NUMBER & ") " & authorizationNumber.ToUpper
        End If

        If Not ((dealerId = Guid.Empty) OrElse (dealerId = Nothing)) Then
            whereClauseConditions &= Environment.NewLine & " AND cv.dealer_id = hextoraw(" & MiscUtil.GetDbStringFromGuid(dealerId) & ")"
        End If


        If (dealerGroupCode <> String.Empty AndAlso (Me.FormatSearchMask(dealerGroupCode))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(dg.code) " & dealerGroupCode.ToUpper & ""
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("cv." & Me.COL_NAME_COMPANY_ID, compIds, False)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        If Not IsNothing(sortBy) Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER,
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & sortBy)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            Dim LangIdPar As New DBHelper.DBHelperParameter(Me.PAR_NAME_LANGUAGE_ID, languageId.ToByteArray)
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {LangIdPar, rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimID(ByVal compIds As ArrayList, ByVal claimNumber As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_CLAIM_ID")

        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & Me.COL_NAME_CLAIM_NUMBER & " " & claimNumber
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql(Me.COL_NAME_COMPANY_ID, compIds, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetCaseIdByCaseNumberAndCompany(ByVal CaseNumber As String, ByVal CompanyCode As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_CASE_ID")
        Dim ds As New DataSet
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() {New OracleParameter("company", CompanyCode),
                                            New OracleParameter("case_number", CaseNumber)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function

    Public Function GetClaimCaseDeviceInfo(ByVal claimId As Guid, ByVal languageId As Guid) As DataSet

        Dim ds As New DataSet
        
        Dim cmd As OracleCommand = DB_OracleCommand("elita.ELP_TBL_CLAIM_EQUIPMENT.GetClaimCaseDeviceInfo", CommandType.StoredProcedure)
        cmd.BindByName = True
        cmd.Parameters.Add("pi_claim_id", OracleDbType.Raw).Value = claimId.ToByteArray
        cmd.Parameters.Add("pi_language_id", OracleDbType.Raw).Value = languageId.ToByteArray
        cmd.Parameters.Add("po_device_info_cursor", OracleDbType.RefCursor, ParameterDirection.Output)

        dim da As OracleDataAdapter = New OracleDataAdapter(cmd)
        da.Fill(ds, "DeviceInfo")
        ds.Locale = Globalization.CultureInfo.InvariantCulture
        Return ds
        
    End Function

    Public Function GetClaimsByMasterClaimNumber(ByVal compIds As ArrayList, ByVal masterClaimNumber As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_CLAIMS_BY_MASTER_CLAIM_NUMBER")

        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(masterClaimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & Me.COL_NAME_MASTER_CLAIM_NUMBER & " " & masterClaimNumber
        End If

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("cl." & Me.COL_NAME_COMPANY_ID, compIds, False)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Dim ds As New DataSet
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    'Function overload for search without dealer id
    Public Function LoadMasterClaimList(ByVal compIds As ArrayList, ByVal masterClaimNumber As String,
                         ByVal claimNumber As String, ByVal customerName As String,
                         ByVal authorizationNumber As String) As DataSet
        Dim dealerId As Guid = Guid.Empty
        Return LoadMasterClaimList(compIds, masterClaimNumber, claimNumber, customerName, authorizationNumber, dealerId)
    End Function

    Public Function LoadMasterClaimList(ByVal compIds As ArrayList, ByVal masterClaimNumber As String,
                         ByVal claimNumber As String, ByVal customerName As String,
                         ByVal authorizationNumber As String, ByVal dealerId As Guid, Optional ByVal dealerGroupCode As String = "") As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_MASTER_CLAIM_LIST")

        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & Me.COL_NAME_CLAIM_NUMBER & " " & claimNumber
        End If

        If Me.FormatSearchMask(masterClaimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & Me.COL_NAME_MASTER_CLAIM_NUMBER & " " & masterClaimNumber
        End If

        If Me.FormatSearchMask(customerName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CUSTOMER_NAME & ") " & customerName
        End If

        If Me.FormatSearchMask(authorizationNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_AUTHORIZATION_NUMBER & ") " & authorizationNumber
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("cl." & Me.COL_NAME_COMPANY_ID, compIds, False)

        'if dealer id present
        If Not ((dealerId = Guid.Empty) OrElse (dealerId = Nothing)) Then
            whereClauseConditions &= Environment.NewLine & " AND cl.dealer_id = hextoraw(" & MiscUtil.GetDbStringFromGuid(dealerId) & ")"
        End If

        If (dealerGroupCode <> String.Empty AndAlso (Me.FormatSearchMask(dealerGroupCode))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(dg.code) " & dealerGroupCode.ToUpper & ""
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Dim ds As New DataSet
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadMasterClaimDetail(ByVal compIds As ArrayList, ByVal masterClaimNumber As String, ByVal certIdStr As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_MASTER_CLAIM_DETAIL")

        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(masterClaimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & Me.COL_NAME_MASTER_CLAIM_NUMBER & " " & masterClaimNumber
        End If

        If (Not certIdStr Is Nothing) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "cert." & Me.COL_NAME_CERTIFICATE_ID & " = " & certIdStr
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("cl." & Me.COL_NAME_COMPANY_ID, compIds, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadMasterClaimDetailList(ByVal compIds As ArrayList, ByVal masterClaimNumber As String, ByVal certIdStr As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_MASTER_CLAIM_DETAIL_LIST")

        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(masterClaimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & Me.COL_NAME_MASTER_CLAIM_NUMBER & " " & masterClaimNumber
        End If

        If (Not certIdStr Is Nothing) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "cert." & Me.COL_NAME_CERTIFICATE_ID & " = " & certIdStr
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("cl." & Me.COL_NAME_COMPANY_ID, compIds, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    'Function overload for search without dealer id
    Public Function LoadActiveClaimList(ByVal compIds As ArrayList, ByVal claimNumber As String, ByVal customerName As String,
                           ByVal serviceCenterName As String, ByVal authorizationNumber As String,
                           ByVal authorizedAmount As String, ByVal sortBy As String) As DataSet
        Return LoadActiveClaimList(compIds, claimNumber, customerName, serviceCenterName, authorizationNumber,
                               authorizedAmount, Guid.Empty, sortBy)
    End Function


    Public Function LoadActiveClaimList(ByVal compIds As ArrayList, ByVal claimNumber As String, ByVal customerName As String,
                               ByVal serviceCenterName As String, ByVal authorizationNumber As String,
                               ByVal authorizedAmount As String, ByVal dealerId As Guid, ByVal sortBy As String, Optional ByVal dealerGroupCode As String = "") As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_ACTIVE_CLAIMS_LIST")

        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND c." & Environment.NewLine & Me.COL_NAME_CLAIM_NUMBER & " " & claimNumber
        End If

        If Me.FormatSearchMask(customerName) Then
            whereClauseConditions &= " AND c." & Environment.NewLine & "UPPER(" & Me.COL_NAME_CUSTOMER_NAME & ") " & customerName
        End If

        If Me.FormatSearchMask(serviceCenterName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sc." & Me.COL_NAME_SERVICE_CENTER_NAME & ") " & serviceCenterName
        End If

        If Me.FormatSearchMask(authorizationNumber) Then
            whereClauseConditions &= " AND c." & Environment.NewLine & "UPPER(" & Me.COL_NAME_AUTHORIZATION_NUMBER & ") " & authorizationNumber
        End If

        If Me.FormatSearchMask(authorizedAmount) Then
            whereClauseConditions &= " AND c." & Environment.NewLine & Me.COL_NAME_AUTHORIZED_AMOUNT & " " & authorizedAmount
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND c." & MiscUtil.BuildListForSql(Me.COL_NAME_COMPANY_ID, compIds, False)

        'if dealer id present
        If Not ((dealerId = Guid.Empty) OrElse (dealerId = Nothing)) Then
            whereClauseConditions &= Environment.NewLine & " AND d.dealer_id = hextoraw(" & MiscUtil.GetDbStringFromGuid(dealerId) & ")"
        End If

        If (dealerGroupCode <> String.Empty AndAlso (Me.FormatSearchMask(dealerGroupCode))) Then
            whereClauseConditions &= Environment.NewLine & "AND dg.code " & dealerGroupCode.ToUpper & ""
        End If
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        If Not IsNothing(sortBy) Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER,
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & sortBy)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            'Dim compIdPar As New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY_ID, compId)
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListClaimFollowUp(ByVal followUpDate As String, ByVal serviceCenterName As String,
                                          ByVal claimNumber As String, ByVal claimAdjusterName As String,
                                          ByVal customerName As String, ByVal claimStatus As String,
                                          ByVal dealerId As Guid, ByVal claimTATId As Guid,
                                          ByVal claimExtendedStatusId As Guid, ByVal noActivityId As Guid,
                                          ByVal ownerId As Guid, ByVal compIds As ArrayList,
                                          ByVal languageId As Guid, ByVal nonOperatedClaims As String, ByVal sortBy As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_FOLLOW_UP")
        Dim whereClauseConditions As String = ""
        Dim whereClauseConditions2 As String = ""
        Dim whereClauseConditions3 As String = ""
        Dim ds As New DataSet

        If ((Not (followUpDate Is Nothing)) AndAlso (Me.FormatSearchMask(followUpDate))) Then
            'selectStmt &= Environment.NewLine & "AND " & GetOracleDate("cv.followup_date") & followUpDate
            whereClauseConditions &= " AND " & Environment.NewLine & GetOracleDate("cv.followup_date") & followUpDate
        End If

        If ((Not (serviceCenterName Is Nothing)) AndAlso (Me.FormatSearchMask(serviceCenterName))) Then
            'selectStmt &= Environment.NewLine & "AND UPPER(sc.description)" & serviceCenterName.ToUpper
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sc.description)" & serviceCenterName.ToUpper
        End If

        If ((Not (claimNumber Is Nothing)) AndAlso (Me.FormatSearchMask(claimNumber))) Then
            'selectStmt &= Environment.NewLine & "AND UPPER(cv.claim_number)" & claimNumber.ToUpper
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(cv.claim_number)" & claimNumber.ToUpper
        End If

        If ((Not (claimAdjusterName Is Nothing)) AndAlso (Me.FormatSearchMask(claimAdjusterName))) Then
            'selectStmt &= Environment.NewLine & "AND UPPER(cv.claims_adjuster)" & claimAdjusterName.ToUpper
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(usrclmadj.user_name)" & claimAdjusterName.ToUpper
        End If

        If ((Not (customerName Is Nothing)) AndAlso (Me.FormatSearchMask(customerName))) Then
            'selectStmt &= Environment.NewLine & "AND UPPER(cv.cust_name)" & customerName.ToUpper
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(cv.cust_name)" & customerName.ToUpper
        End If

        If ((Not (claimStatus Is Nothing)) AndAlso (Me.FormatSearchMask(claimStatus))) Then
            'selectStmt &= Environment.NewLine & "AND UPPER(cv.cust_name)" & customerName.ToUpper
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(cv.status_code)" & claimStatus.ToUpper
        End If

        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "D." & Me.COL_NAME_DEALER_ID & "=" & DBHelper.ValueToSQLString(dealerId)
        End If

        If nonOperatedClaims = "Y" Then
            whereClauseConditions3 &= " Where cs.Num_of_Reminders is not null and cs.Last_Reminder_Send_Date is not null"
        End If

        'Turn Around Time Range Code logic
        Dim dalTAT As New TurnAroundTimeRangeDAL()
        Dim dsTAT As DataSet

        dsTAT = dalTAT.GetMinMaxValFromTAT(claimTATId)
        If Not claimTATId.Equals(Guid.Empty) Then
            If dsTAT.Tables(0).DefaultView.Count = 1 Then
                whereClauseConditions &= " AND " & Environment.NewLine & " elita.elp_claims.getClaimTAT(cv.claim_id) between " & CType(dsTAT.Tables(0).DefaultView.Item(0)("min_days"), Integer) & " and " & CType(dsTAT.Tables(0).DefaultView.Item(0)("max_days"), Integer)
            Else
                whereClauseConditions2 &= Environment.NewLine & " and 1=2"
            End If

        End If

        dsTAT = dalTAT.GetMinMaxValFromTAT(noActivityId)
        If Not noActivityId.Equals(Guid.Empty) Then
            If dsTAT.Tables(0).DefaultView.Count = 1 Then
                whereClauseConditions &= " AND " & Environment.NewLine & " (round((trunc(sysdate) - trunc((select max(status_date) from elp_claim_status where claim_id = cv.claim_id)))) between " & CType(dsTAT.Tables(0).DefaultView.Item(0)("min_days"), Integer) & " and " & CType(dsTAT.Tables(0).DefaultView.Item(0)("max_days"), Integer) & ")"
            Else
                whereClauseConditions2 &= Environment.NewLine & " and 1=2"
            End If
        End If

        If Not claimExtendedStatusId.Equals(Guid.Empty) Then
            whereClauseConditions &= " AND " & Environment.NewLine & " elita.GetLatestExtendedClaimStatusId(cv.claim_id) = (select list_item_id from elp_claim_status_by_group where claim_status_by_group_id = " & DBHelper.ValueToSQLString(claimExtendedStatusId) & ")"
        End If

        If Not ownerId.Equals(Guid.Empty) Then
            whereClauseConditions &= " AND " & Environment.NewLine & " elita.GetLatestExtendedClaimOwnerID(cv.claim_id) =" & DBHelper.ValueToSQLString(ownerId)
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("cv." & Me.COL_NAME_COMPANY_ID, compIds, False)

        If Not whereClauseConditions2 = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER2, whereClauseConditions2)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER2, "")
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not whereClauseConditions3 = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER3, whereClauseConditions3)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER3, "")
        End If

        If Not IsNothing(sortBy) Then
            If sortBy = "CFDAT" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER,
                                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & Me.SORT_BY_FU_DATE)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER,
                                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & sortBy)
            End If
            'selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
            '                                Environment.NewLine & "ORDER BY " & Environment.NewLine & sortBy)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim LangIdPar As New DBHelper.DBHelperParameter(Me.PAR_NAME_LANGUAGE_ID, languageId.ToByteArray)
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {LangIdPar, LangIdPar, LangIdPar, LangIdPar, rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        'selectStmt &= Environment.NewLine & "AND ROWNUM < " & Me.MAX_NUMBER_OF_ROWS
        'selectStmt &= Environment.NewLine & "ORDER BY " & sortBy
        'Try
        '    Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

    End Function

    Public Function LoadPendingClaimList(ByVal compIds As ArrayList,
                                         ByVal claimNumber As String,
                                         ByVal certNumber As String,
                                         ByVal dealerName As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_PENDING_CLAIM_LIST")
        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "CMV." & Me.COL_NAME_CLAIM_NUMBER & " " & claimNumber
        End If

        If Me.FormatSearchMask(certNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "CMV." & Me.COL_NAME_CERT_NUMBER & " " & certNumber
        End If

        If Not Me.IsNothing(dealerName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "D." & Me.COL_NAME_DEALER & "='" & dealerName & "'"
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("CMV." & Me.COL_NAME_COMPANY_ID, compIds, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            'Dim compIdPar As New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY_ID, compIds)
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            'DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, _
            '                New DBHelper.DBHelperParameter() {compIdPar, rowNum})
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNum})

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function LoadPendingApprovalClaimList(ByVal compIds As ArrayList,
                                         ByVal claimNumber As String,
                                         ByVal certNumber As String,
                                         ByVal serviCenterName As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_PENDING_APPROVAL_CLAIM_LIST")
        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "upper(mv." & Me.COL_NAME_CLAIM_NUMBER & ") " & claimNumber
        End If

        If Me.FormatSearchMask(certNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "upper(mv." & Me.COL_NAME_CERT_NUMBER & ") " & certNumber
        End If

        If Me.FormatSearchMask(serviCenterName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "upper(sc." & Me.COL_NAME_SERVICE_CENTER_NAME & ") " & serviCenterName
        End If

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("mv." & Me.COL_NAME_COMPANY_ID, compIds, False)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Dim ds As New DataSet
            'Dim compIdPar As New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY_ID, compIds)
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            'DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, _
            '                New DBHelper.DBHelperParameter() {compIdPar, rowNum})
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNum})

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadPendingReviewPaymentClaimList(ByVal claimNumber As String, ByVal claimedSerialNumber As String, ByVal certificateNumber As String,
                                                      ByVal serviceCenterId As Guid, ByVal countryId As Guid, ByVal claimedManufacturerId As Guid, ByVal claimedModel As String, ByVal claimedSku As String,
                                                      ByVal replacedSku As String, ByVal claimStatusCode As String, ByVal extendedClaimStatusId As Guid, ByVal coverageTypeId As Guid,
                                                      ByVal serviceLevelId As Guid, ByVal originalRiskTypeId As Guid, ByVal replacementPartSku As String, ByVal replacementTypeId As Guid,
                                                      ByVal claimCreatedDate As SearchCriteriaStructType(Of Date), ByVal languageId As Guid, ByVal userId As Guid,
                                                      ByVal serviceLevelCode As String, ByVal multipleAuthId As Guid, ByVal singleAuthId As Guid, ByVal claimAuthStatusVoidId As Guid,
                                                      ByVal equipmentTypeClaimedId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_PENDING_REVIEW_PAYMENT_CLAIM_LIST")
        Dim whereClauseConditions As String = ""

        claimNumber = GetFormattedSearchStringForSQL(claimNumber)
        certificateNumber = GetFormattedSearchStringForSQL(certificateNumber)
        claimedModel = GetFormattedSearchStringForSQL(claimedModel)

        Dim parameters() As DBHelper.DBHelperParameter =
            New DBHelper.DBHelperParameter() _
            {
                New DBHelper.DBHelperParameter(Me.PAR_NAME_SINGLE_AUTH_ID, singleAuthId.ToByteArray()),
                New DBHelper.DBHelperParameter(UserDAL.COL_NAME_USER_ID & "1", userId.ToByteArray()),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_SERVICE_LEVEL_CODE & "1", serviceLevelCode),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_SERVICE_LEVEL_CODE & "2", serviceLevelCode),
                New DBHelper.DBHelperParameter(Me.COL_NAME_STATUS_CODE & "1", claimStatusCode),
                New DBHelper.DBHelperParameter(Me.COL_NAME_STATUS_CODE & "2", claimStatusCode),
                New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_NUMBER & "1", claimNumber),
                New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_NUMBER & "2", claimNumber),
                New DBHelper.DBHelperParameter(Me.COL_NAME_SERVICE_CENTER_ID & "1", IIf(serviceCenterId.Equals(Guid.Empty), DBNull.Value, serviceCenterId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.COL_NAME_SERVICE_CENTER_ID & "2", IIf(serviceCenterId.Equals(Guid.Empty), DBNull.Value, serviceCenterId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.COL_NAME_RISK_TYPE_ID & "1", IIf(originalRiskTypeId.Equals(Guid.Empty), DBNull.Value, originalRiskTypeId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.COL_NAME_RISK_TYPE_ID & "2", IIf(originalRiskTypeId.Equals(Guid.Empty), DBNull.Value, originalRiskTypeId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_REPLACEMENT_PART_SKU & "1", replacementPartSku),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_REPLACEMENT_PART_SKU & "2", replacementPartSku),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_REPLACED_SKU & "1", replacedSku),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_REPLACED_SKU & "2", replacedSku),
                New DBHelper.DBHelperParameter(ReplacementDAL.COL_NAME_DEVICE_TYPE & "1", IIf(replacementTypeId.Equals(Guid.Empty), DBNull.Value, replacementTypeId.ToByteArray())),
                New DBHelper.DBHelperParameter(ReplacementDAL.COL_NAME_DEVICE_TYPE & "2", IIf(replacementTypeId.Equals(Guid.Empty), DBNull.Value, replacementTypeId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_MULTIPLE_AUTH_ID, multipleAuthId.ToByteArray()),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_AUTH_VOID_ID, claimAuthStatusVoidId.ToByteArray()),
                New DBHelper.DBHelperParameter(UserDAL.COL_NAME_USER_ID & "2", userId.ToByteArray()),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_SERVICE_LEVEL_ID & "1", IIf(serviceLevelId.Equals(Guid.Empty), DBNull.Value, serviceLevelId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_SERVICE_LEVEL_ID & "2", IIf(serviceLevelId.Equals(Guid.Empty), DBNull.Value, serviceLevelId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.COL_NAME_STATUS_CODE & "3", claimStatusCode),
                New DBHelper.DBHelperParameter(Me.COL_NAME_STATUS_CODE & "4", claimStatusCode),
                New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_NUMBER & "3", claimNumber),
                New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_NUMBER & "4", claimNumber),
                New DBHelper.DBHelperParameter(Me.COL_NAME_SERVICE_CENTER_ID & "3", IIf(serviceCenterId.Equals(Guid.Empty), DBNull.Value, serviceCenterId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.COL_NAME_SERVICE_CENTER_ID & "4", IIf(serviceCenterId.Equals(Guid.Empty), DBNull.Value, serviceCenterId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.COL_NAME_RISK_TYPE_ID & "3", IIf(originalRiskTypeId.Equals(Guid.Empty), DBNull.Value, originalRiskTypeId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.COL_NAME_RISK_TYPE_ID & "4", IIf(originalRiskTypeId.Equals(Guid.Empty), DBNull.Value, originalRiskTypeId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_REPLACEMENT_PART_SKU & "3", replacementPartSku),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_REPLACEMENT_PART_SKU & "4", replacementPartSku),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_REPLACED_SKU & "3", replacedSku),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_REPLACED_SKU & "4", replacedSku),
                New DBHelper.DBHelperParameter(ReplacementDAL.COL_NAME_DEVICE_TYPE & "3", IIf(replacementTypeId.Equals(Guid.Empty), DBNull.Value, replacementTypeId.ToByteArray())),
                New DBHelper.DBHelperParameter(ReplacementDAL.COL_NAME_DEVICE_TYPE & "4", IIf(replacementTypeId.Equals(Guid.Empty), DBNull.Value, replacementTypeId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_LANGUAGE_ID & "1", languageId.ToByteArray),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_EQUIPMENT_TYPE_CLAIMED_ID, equipmentTypeClaimedId.ToByteArray),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIMED_MANUFACTURER_ID & "1", IIf(claimedManufacturerId.Equals(Guid.Empty), DBNull.Value, claimedManufacturerId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIMED_MANUFACTURER_ID & "2", IIf(claimedManufacturerId.Equals(Guid.Empty), DBNull.Value, claimedManufacturerId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_LANGUAGE_ID & "2", languageId.ToByteArray),
                New DBHelper.DBHelperParameter(CompanyDAL.COL_NAME_COUNTRY_ID & "1", IIf(countryId.Equals(Guid.Empty), DBNull.Value, countryId.ToByteArray())),
                New DBHelper.DBHelperParameter(CompanyDAL.COL_NAME_COUNTRY_ID & "2", IIf(countryId.Equals(Guid.Empty), DBNull.Value, countryId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.COL_NAME_COVERAGE_TYPE_ID & "1", IIf(coverageTypeId.Equals(Guid.Empty), DBNull.Value, coverageTypeId.ToByteArray())),
                New DBHelper.DBHelperParameter(Me.COL_NAME_COVERAGE_TYPE_ID & "2", IIf(coverageTypeId.Equals(Guid.Empty), DBNull.Value, coverageTypeId.ToByteArray())),
                New DBHelper.DBHelperParameter(CertificateDAL.COL_NAME_CERT_NUMBER & "1", certificateNumber),
                New DBHelper.DBHelperParameter(CertificateDAL.COL_NAME_CERT_NUMBER & "2", certificateNumber),
                New DBHelper.DBHelperParameter(ClaimEquipmentDAL.COL_NAME_SERIAL_NUMBER & "1", claimedSerialNumber),
                New DBHelper.DBHelperParameter(ClaimEquipmentDAL.COL_NAME_SERIAL_NUMBER & "2", claimedSerialNumber),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIMED_SKU & "1", claimedSku),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIMED_SKU & "2", claimedSku),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIMED_MODEL & "1", claimedModel),
                New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIMED_MODEL & "2", claimedModel)
            }
        whereClauseConditions &= claimCreatedDate.ToSqlString("cl", Me.COL_NAME_CREATED_DATE, parameters)

        ReDim Preserve parameters(parameters.Length + 2)
        parameters(parameters.Length - 3) = New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)
        parameters(parameters.Length - 2) = New DBHelper.DBHelperParameter(Me.PAR_NAME_EXTENDED_STATUS_ID & "1", IIf(extendedClaimStatusId.Equals(Guid.Empty), DBNull.Value, extendedClaimStatusId.ToByteArray()))
        parameters(parameters.Length - 1) = New DBHelper.DBHelperParameter(Me.PAR_NAME_EXTENDED_STATUS_ID & "2", IIf(extendedClaimStatusId.Equals(Guid.Empty), DBNull.Value, extendedClaimStatusId.ToByteArray()))

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Dim ds As New DataSet
            ds = DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)



            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadNonReworkClaimList(ByVal compIds As ArrayList, ByVal claimNumber As String, ByVal customerName As String,
                           ByVal serviceCenter As String, ByVal authorizationNumber As String,
                           ByVal authorizedAmount As String, ByVal sortBy As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_NonRework_LIST_DYNAMIC")

        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(claimNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "c." & Me.COL_NAME_CLAIM_NUMBER & " " & claimNumber
        End If

        If Me.FormatSearchMask(customerName) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(c." & Me.COL_NAME_CUSTOMER_NAME & ") " & customerName
        End If

        If Me.FormatSearchMask(serviceCenter) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(sc." & Me.COL_NAME_SERVICE_CENTER_NAME & ") " & serviceCenter
        End If

        If Me.FormatSearchMask(authorizationNumber) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(c." & Me.COL_NAME_AUTHORIZATION_NUMBER & ") " & authorizationNumber
        End If

        If Me.FormatSearchMask(authorizedAmount) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "c." & Me.COL_NAME_AUTHORIZED_AMOUNT & " " & authorizedAmount
        End If

        ' not HextoRaw
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COMPANY_ID, compIds, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not IsNothing(sortBy) Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER,
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & sortBy)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim ds As New DataSet
            Dim rowNum As New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadMasterClaimList(ByVal certItemCoverageId As Guid, ByVal allowDiffCoverage As Boolean, ByVal masterClaimProcCode As String) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String

        If masterClaimProcCode = MasterClmProc_ANYMC Then
            If (allowDiffCoverage) Then
                selectStmt = Me.Config("/SQL/LOAD_MASTER_CLAIMS_DIFFERENT_COVERAGE")
            Else
                selectStmt = Me.Config("/SQL/LOAD_MASTER_CLAIMS_SAME_COVERAGE")
            End If
        ElseIf masterClaimProcCode = MasterClmProc_BYDOL Then
            selectStmt = Me.Config("/SQL/LOAD_MASTER_CLAIMS_BY_ALLDATEOFLOSS")
        End If
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function
    Public Function LoadMasterClaimList(ByVal certItemCoverageId As Guid, ByVal allowDiffCoverage As Boolean, ByVal masterClaimProcCode As String, ByVal dateofLoss As Date) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String

        selectStmt = Me.Config("/SQL/LOAD_MASTER_CLAIMS_BY_SAMEDATEOFLOSS")
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray),
                                                 New OracleParameter(COL_NAME_LOSS_DATE, dateofLoss)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function

    Public Function GetRepairedMasterClaimList(ByVal certItemCoverageId As Guid, ByVal dateofLoss As Date) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String

        selectStmt = Me.Config("/SQL/GET_REPAIRED_MASTER_CLAIMS_FOR_SAMEDATEOFLOSS")
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray),
                                                 New OracleParameter(COL_NAME_LOSS_DATE, dateofLoss)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function
    Public Function LoadRedoClaimList(ByVal certItemCoverageId As Guid, ByVal createdDate As Date, ByVal redoClaimID As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_REDO_CLAIMS")

        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CREATED_DATE, createdDate.ToString("MM/dd/yyyy")),
                            New OracleParameter(COL_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray),
                            New OracleParameter(COL_NAME_REDO_CLAIM_ID, redoClaimID.ToByteArray)}


        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function


    Public Function NumberOfAvailableClaims(ByVal certItemCoverageId As Guid, ByVal createdDate As Date, ByVal redoClaimID As Guid) As Boolean
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/REDO_CLAIMS_NUMBER")

        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CREATED_DATE, createdDate.ToString("MM/dd/yyyy")),
                            New OracleParameter(COL_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray),
                            New OracleParameter(COL_NAME_REDO_CLAIM_ID, redoClaimID.ToByteArray)}


        Dim dsCount As DataSet = DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

        If CInt(dsCount.Tables(0).Rows(0).Item(0)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function IsClaimActive(ByVal claimId As Guid) As Integer
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/IS_CLAIM_ACTIVE")

        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray)}


        Dim dsCount As DataSet = DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

        Return CInt(dsCount.Tables(0).Rows(0).Item(0))

    End Function

    Public Function IsClaimAuthNumberExists(ByVal claimId As Guid, ByVal authnumber As String) As Integer
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/IS_CLAIM_AUTH_NUMBER_EXISTS")

        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_AUTHORIZATION_NUMBER, authnumber),
                                                New OracleParameter(COL_NAME_CLAIM_ID, claimId.ToByteArray)}


        Dim dsCount As DataSet = DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

        Return CInt(dsCount.Tables(0).Rows(0).Item(0))

    End Function

    Public Function GetCertClaims(ByVal certId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/ALL_CERT_CLAIMS")
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERTIFICATE_ID, certId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function LoadTotalPaid(ByVal ClaimId As Guid) As MultiValueReturn(Of DecimalType, Integer)
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_TOTAL_PAID")
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CLAIM_ID, ClaimId.ToByteArray)}
        Dim returnValue As New MultiValueReturn(Of DecimalType, Integer)
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                returnValue.Value1 = New DecimalType(Convert.ToDecimal(ds.Tables(0).Rows(0)(0)))
                returnValue.Value2 = Convert.ToInt32(ds.Tables(0).Rows(0)(1))
            Else
                returnValue.Value1 = New DecimalType(0)
                returnValue.Value2 = 0
            End If
            Return returnValue
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadTotalPaidForCert(ByVal CertId As Guid) As DecimalType
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_TOTAL_PAID_FOR_CERT")
        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERTIFICATE_ID, CertId.ToByteArray)}

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                Return New DecimalType(Convert.ToDecimal(ds.Tables(0).Rows(0)(0)))
            Else
                Return New DecimalType(0)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadDeductible(ByVal pCert_Item_Cov_Id As Guid, ByVal pService_Center_Id As Guid) As DecimalType

        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_DEDUCTIBLE")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter("Cert_Item_Cov_Id", pCert_Item_Cov_Id.ToByteArray) _
        , New DBHelper.DBHelperParameter("Service_Center_Id", pService_Center_Id.ToByteArray)}

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            LoadDeductible = New DecimalType(Convert.ToDecimal(ds.Tables(0).Rows(0)(0)))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Sub GetPreviousYearReplacements(ByVal CertID As Guid,
                                           ByVal CurrentLossDate As Date,
                                           ByVal strClaimNum As String,
                                            ByRef TotalClaimsAllowed As Integer,
                                            ByRef RepairsAllowed As Integer,
                                            ByRef ReplAllowed As Integer,
                                            ByRef TotalClaims As Integer,
                                            ByRef Replacements As Integer,
                                            ByRef Repairs As Integer)
        TotalClaimsAllowed = 0
        TotalClaims = 0
        Dim whereClauseConditions As String = ""

        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GetPreviousYearReplacements")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("pi_cert_id", CertID.ToByteArray),
                            New DBHelper.DBHelperParameter("pi_loss_date", CurrentLossDate),
                            New DBHelper.DBHelperParameter("pi_claim_number", strClaimNum)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("po_total_claims_allowed", GetType(Integer)),
                            New DBHelper.DBHelperParameter("po_repairs_allowed", GetType(Integer)),
                            New DBHelper.DBHelperParameter("po_repl_allowed", GetType(Integer)),
                            New DBHelper.DBHelperParameter("po_total_claims", GetType(Integer)),
                            New DBHelper.DBHelperParameter("po_replacements", GetType(Integer)),
                            New DBHelper.DBHelperParameter("po_repairs", GetType(Integer))}


        Try
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

            TotalClaimsAllowed = CType(outputParameters(0).Value, Integer)
            RepairsAllowed = CType(outputParameters(1).Value, Integer)
            ReplAllowed = CType(outputParameters(2).Value, Integer)
            TotalClaims = CType(outputParameters(3).Value, Integer)
            Replacements = CType(outputParameters(4).Value, Integer)
            Repairs = CType(outputParameters(5).Value, Integer)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub


    Public Function LoadClaimDetailbyClaimNumAndDealer(ByVal claimnunber As String, ByVal dealerId As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String

        selectStmt = Me.Config("/SQL/LOAD_CLAIM_DETAIL_BY_CLAIM_NUMBER_AND_DEALER")

        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CLAIM_NUMBER, claimnunber),
                         New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_WS, parameters)

    End Function

    Public Function LoadClaimDetailForWS(ByVal claimId As String, ByVal claimNunber As String, ByVal companyId As Guid, ByVal languageId As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String
        Dim whereClauseConditions As String = ""

        selectStmt = Me.Config("/SQL/LOAD_CLAIM_DETAIL_FOR_WS")
        If Not claimId Is Nothing AndAlso Not claimId.Equals(String.Empty) Then
            whereClauseConditions &= " AND c.claim_id = hextoraw('" & claimId & "')" '& Environment.NewLine & cert_number.ToUpper
        Else
            whereClauseConditions &= " AND c.claim_number = '" & claimNunber & "'"
            whereClauseConditions &= " AND c.company_id = hextoraw('" & GuidControl.GuidToHexString(companyId) & "')"
        End If


        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Dim parameters = New OracleParameter() {New OracleParameter(Me.PAR_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function

    Private Function DB_OracleCommand(ByVal p_SqlStatement As String, ByVal p_CommandType As CommandType) As OracleCommand
        Dim conn As IDbConnection = New OracleConnection(DBHelper.ConnectString)
        Dim cmd As OracleCommand = conn.CreateCommand()

        cmd.CommandText = p_SqlStatement
        cmd.CommandType = p_CommandType

        Return cmd
    End Function

    Public Function LoadGetClaimsForWS(ByVal claimNumber As String, ByVal companyId As Guid, ByVal languageId As Guid, ByVal GetComments As Integer, ByVal GetPartsDesc As Integer) As DataSet

        Dim ds As New DataSet
        Dim da As OracleDataAdapter


        Try
            Dim cmd As OracleCommand = DB_OracleCommand("elita.elp_ws_claim.GET_Claim_Detail", CommandType.StoredProcedure)
            cmd.Parameters.Add("Parm1", OracleDbType.Varchar2).Value = claimNumber
            cmd.Parameters.Add("Parm2", OracleDbType.Raw).Value = companyId.ToByteArray
            cmd.Parameters.Add("Parm3", OracleDbType.Raw).Value = languageId.ToByteArray
            cmd.Parameters.Add("Parm4", OracleDbType.Int32).Value = GetComments
            cmd.Parameters.Add("Parm5", OracleDbType.Int32).Value = GetPartsDesc
            cmd.Parameters.Add("OUT1", OracleDbType.Varchar2, ParameterDirection.Output)
            cmd.Parameters.Add("OUT2", OracleDbType.RefCursor, ParameterDirection.Output)
            cmd.Parameters.Add("OUT3", OracleDbType.RefCursor, ParameterDirection.Output)
            cmd.Parameters.Add("OUT4", OracleDbType.RefCursor, ParameterDirection.Output)
            cmd.Parameters.Add("OUT5", OracleDbType.RefCursor, ParameterDirection.Output)
            cmd.Parameters.Add("OUT6", OracleDbType.RefCursor, ParameterDirection.Output)

            da = New OracleDataAdapter(cmd)

            da.Fill(ds, TABLE_NAME)
            ds.Locale = Globalization.CultureInfo.InvariantCulture

            ds.Tables(1).TableName = TABLE_NAME_CLAIMSTAT

            ds.Tables(2).TableName = TABLE_NAME_COMMENT

            ds.Tables(3).TableName = TABLE_NAME_PARTSINFO

            ds.Tables(4).TableName = TABLE_NAME_PARTSDESC

            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function LoadClaimDetailbyClaimNumAndCompany(ByVal claimnunber As String, ByVal companyIds As ArrayList) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String

        selectStmt = Me.Config("/SQL/LOAD_CLAIM_DETAIL_BY_CLAIM_NUMBER_AND_DEALER")

        Dim whereClauseConditions As String = " AND " & MiscUtil.BuildListForSql("cl." & Me.COL_NAME_COMPANY_ID, companyIds, False)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CLAIM_NUMBER, claimnunber)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function

    Public Sub GetFirstLossDate(ByVal CertID As Guid, ByRef FirstLossDate As String)
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GetFirstLossDate")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("pi_cert_id", CertID.ToByteArray)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("po_loss_date", GetType(String))}

        Try
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

            If Not outputParameters(0).Value Is Nothing Then
                FirstLossDate = CType(outputParameters(0).Value, String)
            Else
                FirstLossDate = String.Empty
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Function ApproveOrRejectClaims(ByVal cmd, ByVal claimIds, ByVal company_group_id) As DBHelper.DBHelperParameter()
        Dim selectStmt As String = Me.Config("/SQL/APPROVE_OR_REJECT_CLAIMS")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CMD, cmd),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_IDs, claimIds),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY_GROUP_ID, company_group_id.ToByteArray)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer)),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String), 500)}

        Try

            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

            Return outputParameters
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function ApproveOrRejectClaims(ByVal cmd, ByVal claimIds, ByVal comments, ByVal risktypes, ByVal company_group_id, ByVal language_id) As DBHelper.DBHelperParameter()
        Dim selectStmt As String = Me.Config("/SQL/APPROVE_OR_REJECT_CLAIMS")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CMD, cmd),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_IDs, claimIds),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RISK_TYPE_IDs, risktypes),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_COMMENTs, comments),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_LANGUAGE_ID, language_id.ToByteArray),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY_GROUP_ID, company_group_id.ToByteArray)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer)),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String), 500)}

        Try

            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

            Return outputParameters
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function IsCustomerPresentInUFIList(ByVal CountryId As Guid, ByVal TaxID As String) As DataView 'REQ-5978
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOOKUP_UFI_LIST")

        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_COUNTRY_ID, CountryId.ToByteArray),
                            New OracleParameter(COL_NAME_TAX_ID, TaxID)}


        ds = DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_UFI_LIST, parameters)

        Return ds.Tables(0).DefaultView

    End Function
    Public Function IsCertPaymentExists(ByVal CertId As Guid) As Boolean 'REQ-6026
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_PAYMENTS_FOR_CERT")

        Dim parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERTIFICATE_ID, CertId.ToByteArray)}


        ds = DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_CERT_PAYMENT, parameters)

        If CInt(ds.Tables(0).Rows(0).Item(0)) = 0 Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function isConsequentialDamageAllowed(ByVal productCodeId As Guid) As Boolean
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/IS_CONSEQUENTIAL_DAMAGE_ALLOWED")
        Dim parameters = New OracleParameter() {New OracleParameter("product_code_id", productCodeId.ToByteArray)}

        ds = DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

        If CInt(ds.Tables(0).Rows(0).Item(0)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Claim Number Generation"
    Public Class ClaimNumberInfo
        Public ClaimNumber As String
        Public ClaimGroupId As Guid
        Public ReturenIndex As Integer
        Public ExceptionMsg As String

        Public Sub New(ByVal claimNumber As String, ByVal claimGroupId As Guid)
            Me.ClaimNumber = claimNumber
            Me.ClaimGroupId = claimGroupId
        End Sub

        Public Sub New(ByVal claimNumber As String, ByVal returnIndex As Integer, ByVal exceptionMsg As String)
            Me.ClaimNumber = claimNumber
            Me.ReturenIndex = returnIndex
            Me.ExceptionMsg = exceptionMsg
        End Sub

    End Class

    Public Function CalculateLiabilityLimit(ByVal certId As Guid, ByVal contractId As Guid, ByVal certItemCoverageId As Guid, Optional ByVal lossDate As DateType = Nothing) As ArrayList
        Dim selectStmt As String = Me.Config("/SQL/CALCULATE_LIABILITY_LIMIT")
        Dim myLossDate As Date

        If lossDate Is Nothing Then
            myLossDate = Date.Today
        Else
            myLossDate = lossDate.Value
        End If

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("p_cert_id", certId.ToByteArray),
                            New DBHelper.DBHelperParameter("p_contract_id", contractId.ToByteArray),
                            New DBHelper.DBHelperParameter("p_cert_item_coverage_id", certItemCoverageId.ToByteArray),
                            New DBHelper.DBHelperParameter("p_loss_date", myLossDate)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("p_liability_limit", GetType(Decimal)),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer))}

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Dim al As New ArrayList
        al.Add(CType(outputParameters(0).Value, Decimal))
        al.Add(CType(outputParameters(1).Value, Integer))

        Return al

    End Function

    Public Function GetDepreciationSchedule(ByVal contractId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_DEPRECIATION_SCHEDULE")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("p_contract_id", contractId.ToByteArray),
                            New DBHelper.DBHelperParameter("p_table_reference", ContractDAL.TABLE_NAME),
                            New DBHelper.DBHelperParameter("p_depreciation_sch_usage_xcd", DepreciationSchedule_Usage_Default)}

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, inputParameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    ' Execute Store Procedure
    Public Function Handle_Replaced_Items(ByVal replaceAll As Integer, ByVal claimId As Guid, ByVal certId As Guid, ByVal certItemCoverageId As Guid, ByVal replaceDate As Date) As Integer
        Dim selectStmt As String = Me.Config("/SQL/HANDLE_REPLACED_ITEMS")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("p_replace_all", replaceAll),
                            New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_ID, claimId.ToByteArray),
                            New DBHelper.DBHelperParameter(Me.COL_NAME_CERTIFICATE_ID, certId.ToByteArray),
                            New DBHelper.DBHelperParameter(Me.COL_NAME_CERT_ITEM_COVERAGE_ID, certItemCoverageId.ToByteArray),
                            New DBHelper.DBHelperParameter("p_replace_date", replaceDate)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer))}

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        If CType(outputParameters(0).Value, Integer) = 0 Or CType(outputParameters(0).Value, Integer) = 1 Then
            Return CType(outputParameters(0).Value, Integer)
        Else
            Dim e As New ApplicationException("Data base exception occurred")
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
        End If

    End Function

    Public Function GetClaimNumber(ByVal companyId As Guid, Optional ByVal claimNumber As String = "", Optional ByVal CoverageCode As String = "", Optional ByVal UnitNumber As Integer = Nothing) As ClaimNumberInfo
        Dim selectStmt As String = Me.Config("/SQL/GET_NEXT_CLAIM_NUMBER_SP")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY, companyId.ToByteArray),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_NUMBER_INPUT, claimNumber),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_COVERAGE_CODE_INPUT, CoverageCode),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_UNIT_NUMBER_INPUT, UnitNumber)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_NUMBER, GetType(String), 50),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_GROUP_ID, GetType(Guid)),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer)),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String), 500)}

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
        If CType(outputParameters(2).Value, Integer) <> 0 Then
            If CType(outputParameters(2).Value, Integer) = 300 Then
                Throw New StoredProcedureGeneratedException("Claim Insert Generated Error: ", outputParameters(3).Value)
            Else
                Dim e As New ApplicationException("Return Value = " & outputParameters(1).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
            End If
        Else
            Return New ClaimNumberInfo(outputParameters(0).Value, outputParameters(1).Value)
        End If

    End Function

    Public Function GetExistClaimNumber(ByVal companyId As Guid, ByVal claimNumber As String, ByVal CoverageCode As String, ByVal UnitNumber As Integer, Optional ByVal IsPayClaim As Boolean = False) As String
        Dim selectStmt As String = Me.Config("/SQL/GET_CLAIM_NUMBER_INFO_SP")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY, companyId.ToByteArray),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_NUMBER_INPUT, claimNumber),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_COVERAGE_CODE_INPUT, CoverageCode),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_UNIT_NUMBER_INPUT, UnitNumber)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_NUMBER, GetType(String), 50),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer)),
                            New DBHelper.DBHelperParameter(Me.PAR_NAME_EXCEPTION_MSG, GetType(String), 500)}

        Dim retClaimNumber As String

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        If CType(outputParameters(1).Value, Integer) <> 0 Then
            If IsPayClaim Then
                If CType(outputParameters(1).Value, Integer) = 200 Then
                    ' Galaxy supplymental claim
                    'Throw New StoredProcedureGeneratedException("Claim Payment Generated Error: ", outputParameters(2).Value)
                    Dim e As New ApplicationException("Return Value = " & outputParameters(1).Value)
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, e, outputParameters(2).Value)
                Else
                    Dim e As New ApplicationException("Return Value = " & outputParameters(1).Value)
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
                End If
            Else
                If Not (UnitNumber = 1) AndAlso CType(outputParameters(1).Value, Integer) = 200 Then
                    ' Galaxy supplymental claim
                    'Throw New StoredProcedureGeneratedException("Claim Update Generated Error: ", outputParameters(2).Value)
                    Dim e As New ApplicationException("Return Value = " & outputParameters(1).Value)
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr, e, outputParameters(2).Value)
                Else
                    Dim e As New ApplicationException("Return Value = " & outputParameters(1).Value)
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
                End If
            End If
        End If

        retClaimNumber = CType(outputParameters(0).Value, String)

        Return retClaimNumber

    End Function


    Public Function GetClaimsForBatch(ByVal serviceCenterId As Guid, ByVal batchNumber As String, ByVal InvoiceTransId As Guid, ByVal userId As Guid, ByVal languageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_CLAIMS_FOR_BATCH")

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                New DBHelper.DBHelperParameter(Me.PAR_SERVICE_CENTER, serviceCenterId.ToByteArray),
                                New DBHelper.DBHelperParameter(Me.PAR_INVOICE_TRANS, InvoiceTransId.ToByteArray),
                                New DBHelper.DBHelperParameter(Me.PAR_USER_ID, userId.ToByteArray),
                                New DBHelper.DBHelperParameter(Me.PAR_LANGUAGE_ID, languageId),
                                New DBHelper.DBHelperParameter(Me.PAR_BATCH_NUMBER, batchNumber)}

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                New DBHelper.DBHelperParameter(Me.PAR_CLAIMS, GetType(DataSet))}

        Dim ds As New DataSet
        Dim tbl As String = Me.TABLE_NAME

        ' Call DBHelper Store Procedure
        DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)
        Return ds

    End Function

    Public Function GetOriginalLiabilityAmount(ByVal Claim_id As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_ORIGINAL_LIABILITY_AMOUNT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                         New DBHelper.DBHelperParameter(Me.TABLE_KEY_NAME, Claim_id.ToByteArray)}
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    'Public Function RemoveClaimGroup(ByVal claimGroupId As Guid) As Integer
    '    Dim retVal As Integer = 0
    '    Dim selectStmt As String = Me.Config("/SQL/DELETE_CLAIM_GROUP")

    '    Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
    '                New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_GROUP_ID, claimGroupId.ToByteArray)}

    '    Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
    '                        New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN, GetType(Integer))}

    '    Try
    '        ' Call DBHelper Store Procedure
    '        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
    '        If CType(outputParameters(0).Value, Integer) <> 0 Then
    '            Dim e As New ApplicationException("Return Value = " & outputParameters(0).Value)
    '            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
    '        End If
    '    Catch ex As Exception
    '        retVal = 1
    '    End Try

    '    Return retVal
    'End Function

#End Region

#Region "ServiceCenterClaimsSearchData"

    Public Class ServiceCenterClaimsSearchData
        Public ServiceCenterId As Guid
        Public ClaimStatus As String
        Public ClaimTypeIds As ArrayList
        Public MethodOfRepairIds As ArrayList
        Public SortBy As Integer
        Public SortOrder As Integer
        Public PageSize As Integer
        Public PageNumber As Integer
        Public ClaimNumber As String
        Public AuthorizationNumber As String
        Public CertificateNumber As String
        Public CustomerName As String
        Public FromClaimCreatedDate As DateType
        Public ToClaimCreatedDate As DateType
        Public FromVisitDate As DateType
        Public ToVisitDate As DateType
        Public ClaimExtendedStatusIds As ArrayList
        Public ClaimExtendedStatusOwnerCodeIds As ArrayList
        Public TurnAroundTimeRangeCode As String
        Public BatchNumber As String
        Public SerialNumber As String
        Public WorkPhone As String
        Public CompanyCode As String
        Public HomePhone As String
        Public LossDate As DateType
        Public ClaimPaidAmount As String
        Public BonusTotal As String
    End Class

#End Region

#Region "Overloaded Methods"

    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, ByVal IsUpdatedComment As Boolean,
                                      ByVal galaxyClaimNumberList As ArrayList,
                                      ByVal publishEventData As PublishedTaskDAL.PublishTaskData(Of ClaimIssueDAL.PublishTaskClaimData),
                                      Optional ByVal Transaction As IDbTransaction = Nothing,
                                      Optional ByVal AuthDetailDataHasChanged As Boolean = False,
                                      Optional ByVal IsUpdatedMasterClaimComment As Boolean = False,
                                      Optional ByVal network_Id As String = "ELP_APP_USER")
        '   Dim serviceOrderDAL As New ServiceOrderDAL
        Dim commentDAL As New CommentDAL

        Dim claimEquipment As New ClaimEquipmentDAL
        'Dim commentDAL As New CommentDAL 'TODO
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        'Def - 1113. DAL would check to see if a record is already created by a back-end process
        'in elp_claim_active_sessions table. It would wait for the process to release that record 
        'and after that a new record would be created in elp_claim_active_sessions here and would be deleted 
        'after the commit or Rollback of the transaction
        'start
        Dim selectStmt As String = Me.Config("/SQL/CLAIM_CHECK_SESSION")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("p_calling_process", network_Id, Type.GetType("System.String"))}

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, Nothing)
        'DBHelper.ExecuteSp(selectStmt, Nothing, Nothing)

        Dim processId As Guid = Guid.NewGuid

        Me.UpdateActiveSession("INSERT", processId, network_Id)
        'end
        Try
            'First Pass updates Deletions
            If AuthDetailDataHasChanged Then
                Dim oPartsInfoDAL As New PartsInfoDAL
                Dim oClaimAuthDetailDAL As New ClaimAuthDetailDAL
                oPartsInfoDAL.Update(familyDataset.Tables(PartsInfoDAL.TABLE_NAME), tr, DataRowState.Deleted)
                oClaimAuthDetailDAL.Update(familyDataset.Tables(ClaimAuthDetailDAL.TABLE_NAME), tr, DataRowState.Deleted)
            End If

            claimEquipment.Update(familyDataset, tr, DataRowState.Deleted)

            Me.Update(familyDataset, tr, DataRowState.Deleted)

            '' Function call sequence:
            '' AddressDal.Update -> ContactInfoDal.Updat ->  Me.Update
            If Not familyDataset.Tables(AddressDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(AddressDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim AddressDal As New AddressDAL
                AddressDal.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Not familyDataset.Tables(ContactInfoDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(ContactInfoDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim ContactInfoDal As New ContactInfoDAL
                ContactInfoDal.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            End If

            'Second Pass updates additions and changes
            '  If isGetClaimNumber = True Then ObtainAndAssignClaimNumber(Row, suffix)
            ObtainAndAssignClaimNumber(familyDataset, galaxyClaimNumberList)
            Me.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Not familyDataset.Tables(ContactInfoDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(ContactInfoDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim ContactInfoDal As New ContactInfoDAL
                ContactInfoDal.Update(familyDataset, tr, DataRowState.Deleted)
            End If

            If Not familyDataset.Tables(AddressDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(AddressDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim AddressDal As New AddressDAL
                AddressDal.Update(familyDataset, tr, DataRowState.Deleted)
            End If

            claimEquipment.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            '  serviceOrderDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            If IsUpdatedComment = True Then

                commentDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If IsUpdatedMasterClaimComment Then
                Dim clDAL As New ClaimDAL
                clDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Not familyDataset.Tables(ClaimAuthorizationDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(ClaimAuthorizationDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim claimAuthorization As New ClaimAuthorizationDAL
                Dim companyId As Guid = New Guid(CType(familyDataset.Tables(TABLE_NAME).Rows(0)(COL_NAME_COMPANY_ID), Byte()))
                claimAuthorization.UpdateFamily(familyDataset, companyId, tr)
            End If


            If Not familyDataset.Tables(PoliceReportDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(PoliceReportDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim policeReportDAL As New PoliceReportDAL
                policeReportDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Not familyDataset.Tables(ShippingInfoDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(ShippingInfoDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim shippingInfoDal As New ShippingInfoDAL
                shippingInfoDal.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If AuthDetailDataHasChanged Then
                Dim oPartsInfoDAL As New PartsInfoDAL
                Dim oClaimAuthDetailDAL As New ClaimAuthDetailDAL
                oPartsInfoDAL.Update(familyDataset.Tables(PartsInfoDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
                oClaimAuthDetailDAL.Update(familyDataset.Tables(ClaimAuthDetailDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Not familyDataset.Tables(ClaimStatusDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(ClaimStatusDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oClaimStatusDAL As New ClaimStatusDAL
                oClaimStatusDAL.Update(familyDataset.Tables(ClaimStatusDAL.TABLE_NAME), tr, DataRowState.Deleted Or DataRowState.Added Or DataRowState.Modified)
            End If

            If Not familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Not familyDataset.Tables(ClaimIssueDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(ClaimIssueDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim ClaimIssueDAL As New ClaimIssueDAL
                ClaimIssueDAL.UpdateFamily(familyDataset, publishEventData, tr)
            End If

            If Not familyDataset.Tables(ClaimImageDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(ClaimImageDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim ClaimImageDAL As New ClaimImageDAL
                ClaimImageDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Not familyDataset.Tables(ReplacementPartDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(ReplacementPartDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim ReplacementPartDAL As New ReplacementPartDAL
                ReplacementPartDAL.Update(familyDataset, tr, DataRowState.Deleted Or DataRowState.Added Or DataRowState.Modified)
            End If

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
                'Def - 1113. Record identified by processId would be deleted from elp_claim_active_sessions table
                'start
                Me.UpdateActiveSession("DELETE", processId, network_Id)
                'end
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
                'Def - 1113. Record identified by processId would be deleted from elp_claim_active_sessions table
                'start
                Me.UpdateActiveSession("DELETE", processId, network_Id)
                'end
            End If
            Throw ex
        End Try
    End Sub

#End Region

#Region "StoreProcedures Control"

    ' Execute Store Procedure
    Public Function ExecuteSP(ByVal docType As String, ByVal IdentificationNumber As String) As String
        Dim dal As New CertificateDAL
        Return dal.ExecuteSP(docType, IdentificationNumber)
    End Function

#End Region

#Region "Claim Specific Methods"
    Public Function GetClaimNumberForOpenMobile(ByVal cert_number As String, ByVal serial_number As String, ByVal compIds As ArrayList) As DataSet

        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/OPEN_MOBILE_GET_CLAIM_NUMBER")

        Me.FormatSearchMask(cert_number)

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter(PAR_NAME_SERIAL_NUMBER, serial_number.ToUpper)}
        'Open Mobile requires generic certificate search Ticket #:1484585

        Dim whereClauseConditions As String = " AND " & MiscUtil.BuildListForSql("cl." & Me.COL_NAME_COMPANY_ID, compIds, False)
        whereClauseConditions &= " AND upper(c.cert_number) " & Environment.NewLine & cert_number.ToUpper
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Try
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_OMBILE, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetActiveClaimsForSvc(ByVal companyGroupID As Guid, ByVal serviceCenterID As Guid, ByVal sortOrder As Integer, ByVal ExtendedClaimStatusListItemID As Guid, ByVal companies As ArrayList, ByVal ExcludeRepairedClaims As String, ByVal languageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/CLAIM_LOGISTICS_ACTIVE_CLAIMS_FOR_SVC")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("claim.company_id", companies, False)

        If ExcludeRepairedClaims.Equals("Y") Then
            whereClauseConditions &= Environment.NewLine & "and claim.REPAIR_DATE is null"
        End If

        If Not ExtendedClaimStatusListItemID.Equals(Guid.Empty) Then
            Dim alCompanyGroup As New ArrayList
            alCompanyGroup.Add(companyGroupID)
            Dim al As New ArrayList
            al.Add(ExtendedClaimStatusListItemID)
            whereClauseConditions &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("CSBG.COMPANY_GROUP_ID ", alCompanyGroup, False)
            whereClauseConditions &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("CSBG.LIST_ITEM_ID ", al, False)
        End If


        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim sortOrderClause As String = ""

        Select Case sortOrder
            Case Me.WS_SORT_BY_CLAIM_NUMBER
                sortOrderClause &= Environment.NewLine & " order by claim_number"
            Case Me.WS_SORT_BY_CLAIM_STATUS
                sortOrderClause &= Environment.NewLine & " order by claim_status"
            Case Me.WS_SORT_BY_ITEM_ITEM_MODEL
                sortOrderClause &= Environment.NewLine & " order by item_model"
            Case Me.WS_SORT_BY_ITEM_MANUFACTURER
                sortOrderClause &= Environment.NewLine & " order by item_manufacturer"
        End Select

        If Not sortOrderClause = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, sortOrderClause)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(Me.WS_PAR_LANGUAGE_ID, languageId.ToByteArray) _
                         , New DBHelper.DBHelperParameter(Me.WS_PAR_LANGUAGE_ID, languageId.ToByteArray) _
                        , New DBHelper.DBHelperParameter(Me.WS_PAR_SERVICE_CENTER_ID, serviceCenterID.ToByteArray) _
                        , New DBHelper.DBHelperParameter(Me.WS_PAR_LANGUAGE_ID, languageId.ToByteArray) _
                        , New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)}

        Dim ds As New DataSet()
        Try

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_STORE, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function GetActiveClaimsForSvcGeneric(ByVal oServiceCenterClaimsSearchData As ClaimDAL.ServiceCenterClaimsSearchData, ByVal companies As ArrayList, ByVal companyGroupID As Guid, ByVal languageId As Guid, Optional ByVal IncludeTotalCount As Boolean = False) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/NEW_GENERIC_CLAIM_LOGISTICS_ACTIVE_CLAIMS_FOR_SVC")
        Dim whereClauseConditions As String = ""
        'Dim whereClauseConditions_count As String = ""
        Dim whereCMVClauseConditions As String = ""
        'Dim whereCMVClauseConditions_count As String = ""

        'Claim Status logic
        If Not oServiceCenterClaimsSearchData.ClaimStatus Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ClaimStatus.Length = 0 Then
            'Change the format from "A|C|D|P" to ('A','C','D','P')
            oServiceCenterClaimsSearchData.ClaimStatus = "('" & oServiceCenterClaimsSearchData.ClaimStatus & "')"
            oServiceCenterClaimsSearchData.ClaimStatus = oServiceCenterClaimsSearchData.ClaimStatus.Replace("|", "','")
            whereCMVClauseConditions &= Environment.NewLine & " and cmv.status_code in " & oServiceCenterClaimsSearchData.ClaimStatus
            'whereCMVClauseConditions_count &= Environment.NewLine & " and cmv.status_code in " & oServiceCenterClaimsSearchData.ClaimStatus
        End If

        'Claim Type logic
        If Not oServiceCenterClaimsSearchData.ClaimTypeIds Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ClaimTypeIds.Count = 0 Then
            whereCMVClauseConditions &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("cmv.Claim_Type_ID ", oServiceCenterClaimsSearchData.ClaimTypeIds, True)
            'whereCMVClauseConditions_count &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("cmv.Claim_Type_ID ", oServiceCenterClaimsSearchData.ClaimTypeIds, True)
        End If

        'Optional Search parameters BEGIN ------------------------------
        If Not oServiceCenterClaimsSearchData.AuthorizationNumber Is Nothing AndAlso Not oServiceCenterClaimsSearchData.AuthorizationNumber.Length = 0 Then
            whereClauseConditions &= Environment.NewLine & " and cmv.authorization_number = '" & oServiceCenterClaimsSearchData.AuthorizationNumber & "'"
            'whereClauseConditions_count &= Environment.NewLine & " and cmv.authorization_number = '" & oServiceCenterClaimsSearchData.AuthorizationNumber & "'"
        End If
        If Not oServiceCenterClaimsSearchData.CertificateNumber Is Nothing AndAlso Not oServiceCenterClaimsSearchData.CertificateNumber.Length = 0 Then
            whereClauseConditions &= Environment.NewLine & " and cert.cert_number = '" & oServiceCenterClaimsSearchData.CertificateNumber & "'"
            'whereClauseConditions_count &= Environment.NewLine & " and cert.cert_number = '" & oServiceCenterClaimsSearchData.CertificateNumber & "'"
        End If
        If Not oServiceCenterClaimsSearchData.ClaimNumber Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ClaimNumber.Length = 0 Then
            whereClauseConditions &= Environment.NewLine & " and cmv.claim_number = '" & oServiceCenterClaimsSearchData.ClaimNumber & "'"
            'whereClauseConditions_count &= Environment.NewLine & " and cmv.claim_number = '" & oServiceCenterClaimsSearchData.ClaimNumber & "'"
        End If

        If IsLikeClause(oServiceCenterClaimsSearchData.CustomerName) AndAlso FormatSearchMask(oServiceCenterClaimsSearchData.CustomerName) Then

            whereClauseConditions &= Environment.NewLine & " and upper(cert.customer_name) " & oServiceCenterClaimsSearchData.CustomerName.ToUpper & ""
            'whereClauseConditions_count &= Environment.NewLine & " and upper(cert.customer_name) " & oServiceCenterClaimsSearchData.CustomerName.ToUpper & ""
        ElseIf Not oServiceCenterClaimsSearchData.CustomerName Is Nothing AndAlso Not oServiceCenterClaimsSearchData.CustomerName.Length = 0 Then
            whereClauseConditions &= Environment.NewLine & " and upper(cert.customer_name) = '" & oServiceCenterClaimsSearchData.CustomerName.ToUpper & "'"
            'whereClauseConditions_count &= Environment.NewLine & " and upper(cert.customer_name) = '" & oServiceCenterClaimsSearchData.CustomerName.ToUpper & "'"
        End If
        'End If
        ' FROM/TO Claim created date logic
        If Not oServiceCenterClaimsSearchData.FromClaimCreatedDate Is Nothing AndAlso oServiceCenterClaimsSearchData.ToClaimCreatedDate Is Nothing Then
            whereClauseConditions &= Environment.NewLine & " and trunc(cmv.created_date) = to_date('" & oServiceCenterClaimsSearchData.FromClaimCreatedDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
            'whereClauseConditions_count &= Environment.NewLine & " and trunc(cmv.created_date) = to_date('" & oServiceCenterClaimsSearchData.FromClaimCreatedDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
        ElseIf Not oServiceCenterClaimsSearchData.ToClaimCreatedDate Is Nothing AndAlso oServiceCenterClaimsSearchData.FromClaimCreatedDate Is Nothing Then
            whereClauseConditions &= Environment.NewLine & " and trunc(cmv.created_date) = to_date('" & oServiceCenterClaimsSearchData.ToClaimCreatedDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
            'whereClauseConditions_count &= Environment.NewLine & " and trunc(cmv.created_date) = to_date('" & oServiceCenterClaimsSearchData.ToClaimCreatedDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
        ElseIf Not oServiceCenterClaimsSearchData.FromClaimCreatedDate Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ToClaimCreatedDate Is Nothing Then
            whereClauseConditions &= Environment.NewLine & " and to_date('" & oServiceCenterClaimsSearchData.FromClaimCreatedDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')" & " <= trunc(cmv.created_date) AND trunc(cmv.created_date) <= to_date('" & oServiceCenterClaimsSearchData.ToClaimCreatedDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
            'whereClauseConditions_count &= Environment.NewLine & " and to_date('" & oServiceCenterClaimsSearchData.FromClaimCreatedDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')" & " <= trunc(cmv.created_date) AND trunc(cmv.created_date) <= to_date('" & oServiceCenterClaimsSearchData.ToClaimCreatedDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
        End If
        ' FROM/TO Claim visit date logic
        If Not oServiceCenterClaimsSearchData.FromVisitDate Is Nothing AndAlso oServiceCenterClaimsSearchData.ToVisitDate Is Nothing Then
            whereClauseConditions &= Environment.NewLine & " and trunc(cmv.visit_date) = to_date('" & oServiceCenterClaimsSearchData.FromVisitDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
            'whereClauseConditions_count &= Environment.NewLine & " and trunc(cmv.visit_date) = to_date('" & oServiceCenterClaimsSearchData.FromVisitDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
        ElseIf Not oServiceCenterClaimsSearchData.ToVisitDate Is Nothing AndAlso oServiceCenterClaimsSearchData.FromVisitDate Is Nothing Then
            whereClauseConditions &= Environment.NewLine & " and trunc(cmv.visit_date) = to_date('" & oServiceCenterClaimsSearchData.ToVisitDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
            'whereClauseConditions_count &= Environment.NewLine & " and trunc(cmv.visit_date) = to_date('" & oServiceCenterClaimsSearchData.ToVisitDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
        ElseIf Not oServiceCenterClaimsSearchData.FromVisitDate Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ToVisitDate Is Nothing Then
            whereClauseConditions &= Environment.NewLine & " and to_date('" & oServiceCenterClaimsSearchData.FromVisitDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')" & " <= trunc(cmv.visit_date) AND trunc(cmv.visit_date) <= to_date('" & oServiceCenterClaimsSearchData.ToVisitDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
            'whereClauseConditions_count &= Environment.NewLine & " and to_date('" & oServiceCenterClaimsSearchData.FromVisitDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')" & " <= trunc(cmv.visit_date) AND trunc(cmv.visit_date) <= to_date('" & oServiceCenterClaimsSearchData.ToVisitDate.Value.ToString("MM/dd/yyyy") & "','MM/dd/yyyy')"
        End If
        'Optional Search parameters END ------------------------------

        'Claim Extended Status logic
        If Not oServiceCenterClaimsSearchData.ClaimExtendedStatusIds Is Nothing AndAlso oServiceCenterClaimsSearchData.ClaimExtendedStatusIds.Count > 0 Then
            whereClauseConditions &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("getlatestextendedclaimstatusid(claim.claim_id) ", oServiceCenterClaimsSearchData.ClaimExtendedStatusIds, True)
            'whereClauseConditions_count &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("getlatestextendedclaimstatusid(claim.claim_id) ", oServiceCenterClaimsSearchData.ClaimExtendedStatusIds, True)
        End If

        'Claim Extended Status Owner logic
        If Not oServiceCenterClaimsSearchData.ClaimExtendedStatusOwnerCodeIds Is Nothing AndAlso oServiceCenterClaimsSearchData.ClaimExtendedStatusOwnerCodeIds.Count > 0 Then
            whereClauseConditions &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("getlatestextendedclaimownerid(claim.claim_id) ", oServiceCenterClaimsSearchData.ClaimExtendedStatusOwnerCodeIds, True)
            'whereClauseConditions_count &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("getlatestextendedclaimownerid(claim.claim_id) ", oServiceCenterClaimsSearchData.ClaimExtendedStatusOwnerCodeIds, True)
        End If

        'Method Of Repair logic
        If Not oServiceCenterClaimsSearchData.MethodOfRepairIds Is Nothing AndAlso Not oServiceCenterClaimsSearchData.MethodOfRepairIds.Count = 0 Then
            whereCMVClauseConditions &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("cmv.method_of_repair_id ", oServiceCenterClaimsSearchData.MethodOfRepairIds, True)
            'whereCMVClauseConditions_count &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("cmv.method_of_repair_id ", oServiceCenterClaimsSearchData.MethodOfRepairIds, True)
        End If

        'Turn Around Time Range Code logic
        Dim dalTAT As New TurnAroundTimeRangeDAL
        Dim dsTAT As DataSet
        dsTAT = dalTAT.GetMinMaxValFromTAT(companyGroupID, oServiceCenterClaimsSearchData.TurnAroundTimeRangeCode)

        If Not oServiceCenterClaimsSearchData.TurnAroundTimeRangeCode Is Nothing AndAlso Not oServiceCenterClaimsSearchData.TurnAroundTimeRangeCode.Length = 0 Then
            If dsTAT.Tables(0).DefaultView.Count = 1 Then
                whereClauseConditions &= Environment.NewLine & " and elita.elp_claims.getServiceCenterTAT(cmv.claim_id) between " & CType(dsTAT.Tables(0).DefaultView.Item(0)("min_days"), Integer) & " and " & CType(dsTAT.Tables(0).DefaultView.Item(0)("max_days"), Integer)
                'whereClauseConditions_count &= Environment.NewLine & " and elita.elp_claims.getServiceCenterTAT(cmv.claim_id) between " & CType(dsTAT.Tables(0).DefaultView.Item(0)("min_days"), Integer) & " and " & CType(dsTAT.Tables(0).DefaultView.Item(0)("max_days"), Integer)
            Else
                whereClauseConditions &= Environment.NewLine & " and 1=2"
                'whereClauseConditions_count &= Environment.NewLine & " and 1=2"
            End If

        End If

        'Batch Number logic
        If Not oServiceCenterClaimsSearchData.BatchNumber Is Nothing AndAlso Not oServiceCenterClaimsSearchData.BatchNumber.Length = 0 Then
            whereClauseConditions &= Environment.NewLine & " and claim.batch_number = '" & oServiceCenterClaimsSearchData.BatchNumber & "'"
            'whereClauseConditions_count &= Environment.NewLine & " and claim.batch_number = '" & oServiceCenterClaimsSearchData.BatchNumber & "'"
        End If

        'Serial Number logic
        If Not oServiceCenterClaimsSearchData.SerialNumber Is Nothing AndAlso Not oServiceCenterClaimsSearchData.SerialNumber.Length = 0 Then
            whereClauseConditions &= Environment.NewLine & " and ci.serial_number = '" & oServiceCenterClaimsSearchData.SerialNumber & "'"
            'whereClauseConditions_count &= Environment.NewLine & " and ci.serial_number = '" & oServiceCenterClaimsSearchData.SerialNumber & "'"
        End If

        'Work Phone logic
        If Not oServiceCenterClaimsSearchData.WorkPhone Is Nothing AndAlso Not oServiceCenterClaimsSearchData.WorkPhone.Length = 0 Then
            whereClauseConditions &= Environment.NewLine & " and cert.work_phone = '" & oServiceCenterClaimsSearchData.WorkPhone & "'"
            'whereClauseConditions_count &= Environment.NewLine & " and cert.work_phone = '" & oServiceCenterClaimsSearchData.WorkPhone & "'"
        End If

        'Company Code logic
        If Not oServiceCenterClaimsSearchData.CompanyCode Is Nothing AndAlso Not oServiceCenterClaimsSearchData.CompanyCode.Length = 0 Then
            whereClauseConditions &= Environment.NewLine & " and comp.code = '" & oServiceCenterClaimsSearchData.CompanyCode & "'"
            'whereClauseConditions_count &= Environment.NewLine & " and comp.code = '" & oServiceCenterClaimsSearchData.CompanyCode & "'"
        End If

        'Bonus Total logic
        'If Not oServiceCenterClaimsSearchData.BonusTotal Is Nothing AndAlso Not oServiceCenterClaimsSearchData.BonusTotal.Length = 0 Then
        '    whereClauseConditions &= Environment.NewLine & " and (claim.bonus + claim.bonus_tax) = '" & oServiceCenterClaimsSearchData.BonusTotal & "'"
        'End If

        whereCMVClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("cmv.company_id", companies, True)
        'whereCMVClauseConditions_count &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("cmv.company_id", companies, True)

        If Not whereCMVClauseConditions = "" Then
            selectStmt = selectStmt.Replace("--dynamic_cmv_where_clause", whereCMVClauseConditions)
            'selectStmt_recordCount = selectStmt_recordCount.Replace("--dynamic_cmv_where_clause", whereCMVClauseConditions_count)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            'selectStmt_recordCount = selectStmt_recordCount.Replace("--dynamic_cmv_where_clause", "")
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            'selectStmt_recordCount = selectStmt_recordCount.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions_count)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            'selectStmt_recordCount = selectStmt_recordCount.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim Number_Of_Row As String = "ROW_NUMBER()  OVER (ORDER BY "

        Dim sortOrderClause As String = String.Empty

        If oServiceCenterClaimsSearchData.SortOrder = WS_SORT_ORDER_ASC Then
            sortOrderClause = " " & SORT_ORDER_ASC
        Else
            sortOrderClause = " " & SORT_ORDER_DESC
        End If

        Select Case oServiceCenterClaimsSearchData.SortBy
            Case Me.WS_G_SORT_BY_VISIT_DATE
                Number_Of_Row &= "cmv." & Me.COL_NAME_VISIT_DATE & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_CLAIM_NUMBER
                Number_Of_Row &= Me.COL_NAME_CLAIM_NUMBER_WS & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_AUTHORIZATION_NUMBER
                Number_Of_Row &= Me.COL_NAME_AUTHORIZATION_NUMBER_WS & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_CLAIM_CREATED_DATE
                Number_Of_Row &= Me.COL_NAME_CLAIM_CREATED_DATE & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_CERTIFICATE_NUMBER
                Number_Of_Row &= Me.COL_NAME_CERT_NUMBER_WS & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_PRODUCT_DESCRIPTION
                Number_Of_Row &= Me.COL_NAME_PRODUCT_DESCRIPTION_WS & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_ITEM_MODELE
                Number_Of_Row &= Me.COL_NAME_ITEM_MODEL_WS & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_ITEM_MANUFACTURER_DESCRIPTION
                Number_Of_Row &= "mnf.description" & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_CUSTOMER_NAME
                Number_Of_Row &= "cert.customer_name" & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_CLAIM_TYPE
                Number_Of_Row &= Me.COL_NAME_CLAIM_TYPE_WS & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_CLAIM_EXTENDED_STATUS_ORDER
                Number_Of_Row &= "getlatestextendedclaimstatusid(claim.claim_id)" & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_CLAIM_EXTENDED_STATUS_OWNER
                Number_Of_Row &= "getlatestextendedclaimownerid(claim.claim_id)" & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_AUTHORIZED_AMOUNT
                Number_Of_Row &= Me.COL_NAME_AUTHORIZED_AMOUNT_WS & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_ADJUSTED_CLAIM_STATUS
                Number_Of_Row &= Me.COL_NAME_CLAIM_STATUS_WS & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_BATCH_NUMBER
                Number_Of_Row &= Me.COL_NAME_BATCH_NUMBER_WS & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_SERIAL_NUMBER
                Number_Of_Row &= Me.COL_NAME_SERIAL_NUMBER_WS & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_WORK_PHONE
                Number_Of_Row &= Me.COL_NAME_WORK_PHONE & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_SC_TAT
                Number_Of_Row &= Me.COL_NAME_SC_TAT & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_HOME_PHONE
                Number_Of_Row &= Me.COL_NAME_HOME_PHONE & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_LOSS_DATE
                Number_Of_Row &= Me.COL_NAME_LOSS_DATE & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_CLAIM_PAID_AMOUNT
                Number_Of_Row &= Me.COL_NAME_CLAIM_PAID_AMOUNT & sortOrderClause & ") As number_of_row"
            Case Me.WS_G_SORT_BY_BONUS_TOTAL
                Number_Of_Row &= "(claim.bonus + claim.bonus_tax)" & sortOrderClause & ") As number_of_row"
        End Select

        'Compute low and high limits
        Dim LimitResultSet_Low, LimitResultSet_High As Integer
        LimitResultSet_Low = (oServiceCenterClaimsSearchData.PageNumber - 1) * oServiceCenterClaimsSearchData.PageSize
        LimitResultSet_High = oServiceCenterClaimsSearchData.PageNumber * oServiceCenterClaimsSearchData.PageSize

        selectStmt = selectStmt.Replace(Me.DYNAMIC_ROW_NUMBER_PLACE_HOLDER, Number_Of_Row)

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(Me.WS_PAR_SERVICE_CENTER_ID, oServiceCenterClaimsSearchData.ServiceCenterId.ToByteArray),
                         New DBHelper.DBHelperParameter(Me.WS_PAR_LANGUAGE_ID, languageId.ToByteArray),
                         New DBHelper.DBHelperParameter(Me.WS_PAR_LANGUAGE_ID, languageId.ToByteArray),
                         New DBHelper.DBHelperParameter(Me.WS_PAR_LANGUAGE_ID, languageId.ToByteArray),
                         New DBHelper.DBHelperParameter(Me.WS_PAR_LANGUAGE_ID, languageId.ToByteArray),
                         New DBHelper.DBHelperParameter(Me.COL_NAME_LOW_LIMIT, LimitResultSet_Low),
                         New DBHelper.DBHelperParameter(Me.COL_NAME_HIGH_LIMIT, LimitResultSet_High)}

        Dim ds As New DataSet()
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_SVC, parameters)

            Dim countTab As DataTable = New DataTable(Me.TABLE_NAME_SVC_COUNT)
            countTab.Columns.Add("COUNT")
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                countTab.Rows.Add(CType(ds.Tables(0).Rows(0).Item("COUNT"), Integer))
            End If

            ds.Tables.Add(countTab)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetClaimsForServiceCenter(ByVal oServiceCenterClaimsSearchData As ClaimDAL.ServiceCenterClaimsSearchData, ByVal username As String, Optional ByVal IncludeTotalCount As Boolean = False) As DataSet
        Dim ds As New DataSet
        Dim da As OracleDataAdapter
        Dim ClaimStatusString As String = Nothing
        Dim ClaimTypeIdsString As String = Nothing
        Dim ClaimExtendedStatusIdsString As String = Nothing
        Dim ClaimExtendedStatusOwnerCodeIdsString As String = Nothing
        Dim MethodOfRepairIdsString As String = Nothing

        Dim FromClaimCreatedDate As String = Nothing
        Dim ToClaimCreatedDate As String = Nothing
        Dim FromVisitDate As String = Nothing
        Dim ToVisitDate As String = Nothing


        Dim LimitResultSet_Low, LimitResultSet_High As Integer
        LimitResultSet_Low = (oServiceCenterClaimsSearchData.PageNumber - 1) * oServiceCenterClaimsSearchData.PageSize
        LimitResultSet_High = oServiceCenterClaimsSearchData.PageNumber * oServiceCenterClaimsSearchData.PageSize

        If Not oServiceCenterClaimsSearchData.ClaimStatus Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ClaimStatus.Length = 0 Then
            'Change the format from "A|C|D|P" to ('A','C','D','P')
            oServiceCenterClaimsSearchData.ClaimStatus = oServiceCenterClaimsSearchData.ClaimStatus.Replace("|", ",")
            ClaimStatusString = oServiceCenterClaimsSearchData.ClaimStatus
        End If

        If Not oServiceCenterClaimsSearchData.ClaimTypeIds Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ClaimTypeIds.Count = 0 Then
            ClaimTypeIdsString = MiscUtil.BuildListSVCSql(oServiceCenterClaimsSearchData.ClaimTypeIds)
        End If

        If Not oServiceCenterClaimsSearchData.ClaimExtendedStatusIds Is Nothing AndAlso oServiceCenterClaimsSearchData.ClaimExtendedStatusIds.Count > 0 Then
            ClaimExtendedStatusIdsString = MiscUtil.BuildListSVCSql(oServiceCenterClaimsSearchData.ClaimExtendedStatusIds)
        End If

        If Not oServiceCenterClaimsSearchData.ClaimExtendedStatusOwnerCodeIds Is Nothing AndAlso oServiceCenterClaimsSearchData.ClaimExtendedStatusOwnerCodeIds.Count > 0 Then
            ClaimExtendedStatusOwnerCodeIdsString = MiscUtil.BuildListSVCSql(oServiceCenterClaimsSearchData.ClaimExtendedStatusOwnerCodeIds)
        End If

        If Not oServiceCenterClaimsSearchData.MethodOfRepairIds Is Nothing AndAlso Not oServiceCenterClaimsSearchData.MethodOfRepairIds.Count = 0 Then
            MethodOfRepairIdsString = MiscUtil.BuildListSVCSql(oServiceCenterClaimsSearchData.MethodOfRepairIds)
        End If

        If Not oServiceCenterClaimsSearchData.FromClaimCreatedDate Is Nothing Then
            FromClaimCreatedDate = oServiceCenterClaimsSearchData.FromClaimCreatedDate.Value.ToString("yyyyMMdd")
        End If

        If Not oServiceCenterClaimsSearchData.ToClaimCreatedDate Is Nothing Then
            ToClaimCreatedDate = oServiceCenterClaimsSearchData.ToClaimCreatedDate.Value.ToString("yyyyMMdd")
        End If

        If Not oServiceCenterClaimsSearchData.FromVisitDate Is Nothing Then
            FromVisitDate = oServiceCenterClaimsSearchData.FromVisitDate.Value.ToString("yyyyMMdd")
        End If
        If Not oServiceCenterClaimsSearchData.ToVisitDate Is Nothing Then
            ToVisitDate = oServiceCenterClaimsSearchData.ToVisitDate.Value.ToString("yyyyMMdd")
        End If

        Try
            Dim cmd As OracleCommand = DB_OracleCommand("ELITA.elp_ws_claim_servicecenter.GET_Claim_List", CommandType.StoredProcedure)
            cmd.BindByName = True
            cmd.Parameters.Add("pi_network_id", OracleDbType.Varchar2).Value = username
            cmd.Parameters.Add("pi_company_code", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.CompanyCode
            cmd.Parameters.Add("pi_service_center_id", OracleDbType.Raw).Value = oServiceCenterClaimsSearchData.ServiceCenterId.ToByteArray
            cmd.Parameters.Add("pi_low_limit", OracleDbType.Int32).Value = LimitResultSet_Low
            cmd.Parameters.Add("pi_hight_limit", OracleDbType.Int32).Value = LimitResultSet_High
            cmd.Parameters.Add("pi_Order_by", OracleDbType.Int32).Value = oServiceCenterClaimsSearchData.SortOrder
            cmd.Parameters.Add("pi_Sort_by", OracleDbType.Int32).Value = oServiceCenterClaimsSearchData.SortBy
            cmd.Parameters.Add("po_Error_MSG", OracleDbType.Varchar2, ParameterDirection.Output)
            cmd.Parameters.Add("po_ser_center_claim_cur", OracleDbType.RefCursor, ParameterDirection.Output)
            cmd.Parameters.Add("pi_status_code", OracleDbType.Varchar2).Value = ClaimStatusString
            cmd.Parameters.Add("pi_Claim_Type_List", OracleDbType.Varchar2).Value = ClaimTypeIdsString
            cmd.Parameters.Add("pi_authorization_number", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.AuthorizationNumber
            cmd.Parameters.Add("pi_cert_number", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.CertificateNumber
            cmd.Parameters.Add("pi_claim_number", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.ClaimNumber
            cmd.Parameters.Add("pi_customer_name", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.CustomerName
            cmd.Parameters.Add("pi_created_date_From", OracleDbType.Varchar2).Value = FromClaimCreatedDate
            cmd.Parameters.Add("pi_created_date_to", OracleDbType.Varchar2).Value = ToClaimCreatedDate
            cmd.Parameters.Add("pi_visit_date_From", OracleDbType.Varchar2).Value = FromVisitDate
            cmd.Parameters.Add("pi_visit_date_to", OracleDbType.Varchar2).Value = ToVisitDate
            cmd.Parameters.Add("pi_Ext_Status_List", OracleDbType.Varchar2).Value = ClaimExtendedStatusIdsString
            cmd.Parameters.Add("pi_Ext_Status_owner_List", OracleDbType.Varchar2).Value = ClaimExtendedStatusOwnerCodeIdsString
            cmd.Parameters.Add("pi_TurnAroundTimeRangeCode", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.TurnAroundTimeRangeCode
            cmd.Parameters.Add("pi_method_of_repair_List", OracleDbType.Varchar2).Value = MethodOfRepairIdsString

            cmd.Parameters.Add("pi_BatchNumber", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.BatchNumber
            cmd.Parameters.Add("pi_serial_number", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.SerialNumber
            cmd.Parameters.Add("pi_work_phone", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.WorkPhone



            da = New OracleDataAdapter(cmd)

            da.Fill(ds, TABLE_NAME_SVC)
            ds.Locale = Globalization.CultureInfo.InvariantCulture
            Return ds

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetClaimsForServiceCenterAC(ByVal oServiceCenterClaimsSearchData As ClaimDAL.ServiceCenterClaimsSearchData, ByVal username As String, Optional ByVal IncludeTotalCount As Boolean = False) As DataSet
        Dim ds As New DataSet
        Dim da As OracleDataAdapter
        Dim ClaimStatusString As String = Nothing
        Dim ClaimTypeIdsString As String = Nothing
        Dim ClaimExtendedStatusIdsString As String = Nothing
        Dim ClaimExtendedStatusOwnerCodeIdsString As String = Nothing
        Dim MethodOfRepairIdsString As String = Nothing

        Dim FromClaimCreatedDate As String = Nothing
        Dim ToClaimCreatedDate As String = Nothing
        Dim FromVisitDate As String = Nothing
        Dim ToVisitDate As String = Nothing


        Dim LimitResultSet_Low, LimitResultSet_High As Integer
        LimitResultSet_Low = (oServiceCenterClaimsSearchData.PageNumber - 1) * oServiceCenterClaimsSearchData.PageSize
        LimitResultSet_High = oServiceCenterClaimsSearchData.PageNumber * oServiceCenterClaimsSearchData.PageSize

        If Not oServiceCenterClaimsSearchData.ClaimStatus Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ClaimStatus.Length = 0 Then
            'Change the format from "A|C|D|P" to 'A','C','D','P'
            oServiceCenterClaimsSearchData.ClaimStatus = oServiceCenterClaimsSearchData.ClaimStatus.Replace("|", "','")
            ClaimStatusString = "'" + oServiceCenterClaimsSearchData.ClaimStatus.Trim + "'"
        End If

        If Not oServiceCenterClaimsSearchData.ClaimTypeIds Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ClaimTypeIds.Count = 0 Then
            ClaimTypeIdsString = MiscUtil.BuildListSVCSql(oServiceCenterClaimsSearchData.ClaimTypeIds)
        End If

        If Not oServiceCenterClaimsSearchData.ClaimExtendedStatusIds Is Nothing AndAlso oServiceCenterClaimsSearchData.ClaimExtendedStatusIds.Count > 0 Then
            ClaimExtendedStatusIdsString = MiscUtil.BuildListSVCSql(oServiceCenterClaimsSearchData.ClaimExtendedStatusIds)
        End If

        If Not oServiceCenterClaimsSearchData.ClaimExtendedStatusOwnerCodeIds Is Nothing AndAlso oServiceCenterClaimsSearchData.ClaimExtendedStatusOwnerCodeIds.Count > 0 Then
            ClaimExtendedStatusOwnerCodeIdsString = MiscUtil.BuildListSVCSql(oServiceCenterClaimsSearchData.ClaimExtendedStatusOwnerCodeIds)
        End If

        If Not oServiceCenterClaimsSearchData.MethodOfRepairIds Is Nothing AndAlso Not oServiceCenterClaimsSearchData.MethodOfRepairIds.Count = 0 Then
            MethodOfRepairIdsString = MiscUtil.BuildListSVCSql(oServiceCenterClaimsSearchData.MethodOfRepairIds)
        End If

        If Not oServiceCenterClaimsSearchData.FromClaimCreatedDate Is Nothing Then
            FromClaimCreatedDate = oServiceCenterClaimsSearchData.FromClaimCreatedDate.Value.ToString("yyyyMMdd")
        End If

        If Not oServiceCenterClaimsSearchData.ToClaimCreatedDate Is Nothing Then
            ToClaimCreatedDate = oServiceCenterClaimsSearchData.ToClaimCreatedDate.Value.ToString("yyyyMMdd")
        End If

        If Not oServiceCenterClaimsSearchData.FromVisitDate Is Nothing Then
            FromVisitDate = oServiceCenterClaimsSearchData.FromVisitDate.Value.ToString("yyyyMMdd")
        End If
        If Not oServiceCenterClaimsSearchData.ToVisitDate Is Nothing Then
            ToVisitDate = oServiceCenterClaimsSearchData.ToVisitDate.Value.ToString("yyyyMMdd")
        End If

        Try
            Dim cmd As OracleCommand = DB_OracleCommand("ELITA.elp_ws_claim_servicecenter.GET_Claim_List_AC", CommandType.StoredProcedure)
            cmd.BindByName = True
            cmd.Parameters.Add("pi_network_id", OracleDbType.Varchar2).Value = username
            cmd.Parameters.Add("pi_company_code", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.CompanyCode
            cmd.Parameters.Add("pi_service_center_id", OracleDbType.Raw).Value = oServiceCenterClaimsSearchData.ServiceCenterId.ToByteArray
            cmd.Parameters.Add("pi_low_limit", OracleDbType.Int32).Value = LimitResultSet_Low
            cmd.Parameters.Add("pi_hight_limit", OracleDbType.Int32).Value = LimitResultSet_High
            cmd.Parameters.Add("pi_Order_by", OracleDbType.Int32).Value = oServiceCenterClaimsSearchData.SortOrder
            cmd.Parameters.Add("pi_Sort_by", OracleDbType.Int32).Value = oServiceCenterClaimsSearchData.SortBy
            cmd.Parameters.Add("po_Error_MSG", OracleDbType.Varchar2, ParameterDirection.Output)
            cmd.Parameters.Add("po_ser_center_claim_cur", OracleDbType.RefCursor, ParameterDirection.Output)
            cmd.Parameters.Add("pi_status_code", OracleDbType.Varchar2).Value = ClaimStatusString
            cmd.Parameters.Add("pi_Claim_Type_List", OracleDbType.Varchar2).Value = ClaimTypeIdsString
            cmd.Parameters.Add("pi_authorization_number", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.AuthorizationNumber
            cmd.Parameters.Add("pi_cert_number", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.CertificateNumber
            cmd.Parameters.Add("pi_claim_number", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.ClaimNumber
            cmd.Parameters.Add("pi_customer_name", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.CustomerName
            cmd.Parameters.Add("pi_created_date_From", OracleDbType.Varchar2).Value = FromClaimCreatedDate
            cmd.Parameters.Add("pi_created_date_to", OracleDbType.Varchar2).Value = ToClaimCreatedDate
            cmd.Parameters.Add("pi_visit_date_From", OracleDbType.Varchar2).Value = FromVisitDate
            cmd.Parameters.Add("pi_visit_date_to", OracleDbType.Varchar2).Value = ToVisitDate
            cmd.Parameters.Add("pi_Ext_Status_List", OracleDbType.Varchar2).Value = ClaimExtendedStatusIdsString
            cmd.Parameters.Add("pi_Ext_Status_owner_List", OracleDbType.Varchar2).Value = ClaimExtendedStatusOwnerCodeIdsString
            cmd.Parameters.Add("pi_TurnAroundTimeRangeCode", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.TurnAroundTimeRangeCode
            cmd.Parameters.Add("pi_method_of_repair_List", OracleDbType.Varchar2).Value = MethodOfRepairIdsString

            cmd.Parameters.Add("pi_BatchNumber", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.BatchNumber
            cmd.Parameters.Add("pi_serial_number", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.SerialNumber
            cmd.Parameters.Add("pi_work_phone", OracleDbType.Varchar2).Value = oServiceCenterClaimsSearchData.WorkPhone



            da = New OracleDataAdapter(cmd)

            da.Fill(ds, TABLE_NAME_SVC)
            ds.Locale = Globalization.CultureInfo.InvariantCulture
            Return ds

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetActiveClaimsByClaimNumberorCertificate(ByVal oServiceCenterClaimsSearchData As ClaimDAL.ServiceCenterClaimsSearchData, ByVal companies As ArrayList, ByVal companyGroupID As Guid, ByVal languageId As Guid, Optional ByVal IncludeTotalCount As Boolean = False) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GENERIC_CLAIM_LOGISTICS_ACTIVE_CLAIMS_BASED_ON_CERT_OR_CLAIM_NUMBER")
        Dim whereClauseConditions As String = ""
        Dim whereCMVClauseConditions As String = ""
        Dim whereCertClauseConditions As String = ""

        'Company Code logic
        If Not oServiceCenterClaimsSearchData.CompanyCode Is Nothing AndAlso Not oServiceCenterClaimsSearchData.CompanyCode.Length = 0 Then
            whereCertClauseConditions &= Environment.NewLine & " and comp.code = '" & oServiceCenterClaimsSearchData.CompanyCode & "'"
        End If

        whereCertClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("comp.company_id", companies, True)

        If Not oServiceCenterClaimsSearchData.CertificateNumber Is Nothing AndAlso Not oServiceCenterClaimsSearchData.CertificateNumber.Length = 0 Then
            whereCertClauseConditions &= Environment.NewLine & " and cert.cert_number = '" & oServiceCenterClaimsSearchData.CertificateNumber & "'"
        End If


        'Optional Search parameters BEGIN ------------------------------

        'Claim Status logic
        If Not oServiceCenterClaimsSearchData.ClaimStatus Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ClaimStatus.Length = 0 Then
            'Change the format from "A|C|D|P" to ('A','C','D','P')
            oServiceCenterClaimsSearchData.ClaimStatus = "('" & oServiceCenterClaimsSearchData.ClaimStatus & "')"
            oServiceCenterClaimsSearchData.ClaimStatus = oServiceCenterClaimsSearchData.ClaimStatus.Replace("|", "','")
            whereCMVClauseConditions &= Environment.NewLine & " and cmv.status_code in " & oServiceCenterClaimsSearchData.ClaimStatus
        End If

        'Claim Type logic
        If Not oServiceCenterClaimsSearchData.ClaimTypeIds Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ClaimTypeIds.Count = 0 Then
            whereCMVClauseConditions &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("cmv.Claim_Type_ID ", oServiceCenterClaimsSearchData.ClaimTypeIds, True)
        End If

        If Not oServiceCenterClaimsSearchData.ClaimNumber Is Nothing AndAlso Not oServiceCenterClaimsSearchData.ClaimNumber.Length = 0 Then
            whereCMVClauseConditions &= Environment.NewLine & " and cmv.claim_number = '" & oServiceCenterClaimsSearchData.ClaimNumber & "'"
        End If

        If IsLikeClause(oServiceCenterClaimsSearchData.CustomerName) AndAlso FormatSearchMask(oServiceCenterClaimsSearchData.CustomerName) Then
            whereCertClauseConditions &= Environment.NewLine & " and upper(cert.customer_name) " & oServiceCenterClaimsSearchData.CustomerName.ToUpper & ""
        ElseIf Not oServiceCenterClaimsSearchData.CustomerName Is Nothing AndAlso Not oServiceCenterClaimsSearchData.CustomerName.Length = 0 Then
            whereCertClauseConditions &= Environment.NewLine & " and upper(cert.customer_name) = '" & oServiceCenterClaimsSearchData.CustomerName.ToUpper & "'"
        End If

        'Claim Extended Status logic
        If Not oServiceCenterClaimsSearchData.ClaimExtendedStatusIds Is Nothing AndAlso oServiceCenterClaimsSearchData.ClaimExtendedStatusIds.Count > 0 Then
            whereClauseConditions &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("getlatestextendedclaimstatusid(claim.claim_id) ", oServiceCenterClaimsSearchData.ClaimExtendedStatusIds, True)
        End If

        'Method Of Repair logic
        If Not oServiceCenterClaimsSearchData.MethodOfRepairIds Is Nothing AndAlso Not oServiceCenterClaimsSearchData.MethodOfRepairIds.Count = 0 Then
            whereCMVClauseConditions &= Environment.NewLine & " and " & MiscUtil.BuildListForSql("cmv.method_of_repair_id ", oServiceCenterClaimsSearchData.MethodOfRepairIds, True)
        End If


        'Work Phone logic
        If Not oServiceCenterClaimsSearchData.WorkPhone Is Nothing AndAlso Not oServiceCenterClaimsSearchData.WorkPhone.Length = 0 Then
            whereCertClauseConditions &= Environment.NewLine & " and cert.work_phone = '" & oServiceCenterClaimsSearchData.WorkPhone & "'"
        End If


        If Not whereCMVClauseConditions = "" Then
            selectStmt = selectStmt.Replace("--dynamic_cmv_where_clause", whereCMVClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not whereCertClauseConditions = "" Then
            selectStmt = selectStmt.Replace("--dynamic_cert_where_clause", whereCertClauseConditions)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim Number_Of_Row As String = "ROW_NUMBER()  OVER (ORDER BY "
        Dim Count_Of_Row As String = "COUNT(cmv.claim_id)  OVER (ORDER BY "

        Dim sortOrderClause As String = String.Empty

        If oServiceCenterClaimsSearchData.SortOrder = WS_SORT_ORDER_ASC Then
            sortOrderClause = " " & SORT_ORDER_ASC
        Else
            sortOrderClause = " " & SORT_ORDER_DESC
        End If

        Select Case oServiceCenterClaimsSearchData.SortBy
            Case Me.WS_G_SORT_BY_VISIT_DATE
                Number_Of_Row &= "cmv." & Me.COL_NAME_VISIT_DATE & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= "cmv." & Me.COL_NAME_VISIT_DATE & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_CLAIM_NUMBER
                Number_Of_Row &= Me.COL_NAME_CLAIM_NUMBER_WS & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_CLAIM_NUMBER_WS & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_AUTHORIZATION_NUMBER
                Number_Of_Row &= Me.COL_NAME_AUTHORIZATION_NUMBER_WS & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_AUTHORIZATION_NUMBER_WS & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_CLAIM_CREATED_DATE
                Number_Of_Row &= Me.COL_NAME_CLAIM_CREATED_DATE & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_CLAIM_CREATED_DATE & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_CERTIFICATE_NUMBER
                Number_Of_Row &= Me.COL_NAME_CERT_NUMBER_WS & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_CERT_NUMBER_WS & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_PRODUCT_DESCRIPTION
                Number_Of_Row &= Me.COL_NAME_PRODUCT_DESCRIPTION_WS & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_PRODUCT_DESCRIPTION_WS & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_ITEM_MODELE
                Number_Of_Row &= Me.COL_NAME_ITEM_MODEL_WS & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_ITEM_MODEL_WS & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_ITEM_MANUFACTURER_DESCRIPTION
                Number_Of_Row &= "mnf.description" & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= "mnf.description" & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_CUSTOMER_NAME
                Number_Of_Row &= "cert.customer_name" & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= "cert.customer_name" & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_CLAIM_TYPE
                Number_Of_Row &= Me.COL_NAME_CLAIM_TYPE_WS & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_CLAIM_TYPE_WS & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_CLAIM_EXTENDED_STATUS_ORDER
                Number_Of_Row &= "getlatestextendedclaimstatusid(claim.claim_id)" & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= "getlatestextendedclaimstatusid(claim.claim_id)" & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_CLAIM_EXTENDED_STATUS_OWNER
                Number_Of_Row &= "getlatestextendedclaimownerid(claim.claim_id)" & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= "getlatestextendedclaimownerid(claim.claim_id)" & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_AUTHORIZED_AMOUNT
                Number_Of_Row &= Me.COL_NAME_AUTHORIZED_AMOUNT_WS & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_AUTHORIZED_AMOUNT_WS & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_ADJUSTED_CLAIM_STATUS
                Number_Of_Row &= Me.COL_NAME_CLAIM_STATUS_WS & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_CLAIM_STATUS_WS & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_BATCH_NUMBER
                Number_Of_Row &= Me.COL_NAME_BATCH_NUMBER_WS & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_BATCH_NUMBER_WS & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_SERIAL_NUMBER
                Number_Of_Row &= Me.COL_NAME_SERIAL_NUMBER_WS & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_SERIAL_NUMBER_WS & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_WORK_PHONE
                Number_Of_Row &= Me.COL_NAME_WORK_PHONE & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_WORK_PHONE & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_SC_TAT
                Number_Of_Row &= Me.COL_NAME_SC_TAT & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_SC_TAT & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_HOME_PHONE
                Number_Of_Row &= Me.COL_NAME_HOME_PHONE & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_HOME_PHONE & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_LOSS_DATE
                Number_Of_Row &= Me.COL_NAME_LOSS_DATE & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_LOSS_DATE & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_CLAIM_PAID_AMOUNT
                Number_Of_Row &= Me.COL_NAME_CLAIM_PAID_AMOUNT & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= Me.COL_NAME_CLAIM_PAID_AMOUNT & sortOrderClause & ") As COUNT,"
            Case Me.WS_G_SORT_BY_BONUS_TOTAL
                Number_Of_Row &= "(claim.bonus + claim.bonus_tax)" & sortOrderClause & ") As number_of_row"
                Count_Of_Row &= "(claim.bonus + claim.bonus_tax)" & sortOrderClause & ") As COUNT,"
        End Select

        'Compute low and high limits
        Dim LimitResultSet_Low, LimitResultSet_High As Integer
        LimitResultSet_Low = (oServiceCenterClaimsSearchData.PageNumber - 1) * oServiceCenterClaimsSearchData.PageSize
        LimitResultSet_High = oServiceCenterClaimsSearchData.PageNumber * oServiceCenterClaimsSearchData.PageSize

        selectStmt = selectStmt.Replace(Me.DYNAMIC_ROW_NUMBER_PLACE_HOLDER, Number_Of_Row)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_COUNT_PLACE_HOLDER, Count_Of_Row)

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(Me.WS_PAR_LANGUAGE_ID, languageId.ToByteArray),
                         New DBHelper.DBHelperParameter(Me.WS_PAR_LANGUAGE_ID, languageId.ToByteArray),
                         New DBHelper.DBHelperParameter(Me.WS_PAR_LANGUAGE_ID, languageId.ToByteArray),
                         New DBHelper.DBHelperParameter(Me.WS_PAR_LANGUAGE_ID, languageId.ToByteArray),
                         New DBHelper.DBHelperParameter(Me.COL_NAME_LOW_LIMIT, LimitResultSet_Low),
                         New DBHelper.DBHelperParameter(Me.COL_NAME_HIGH_LIMIT, LimitResultSet_High)}

        Dim ds As New DataSet()
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_SVC, parameters)

            Dim countTab As DataTable = New DataTable(Me.TABLE_NAME_SVC_COUNT)
            countTab.Columns.Add("COUNT")
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                countTab.Rows.Add(CType(ds.Tables(0).Rows(0).Item("COUNT"), Integer))
            End If

            ds.Tables.Add(countTab)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetProblemDescription(ByVal claim_number As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/PROBLEM_DESCRIPTION")
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim Claim_id As Guid = New Guid(claim_number)

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_CLAIM_ID, Claim_id.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function

    Public Function GetTechnicalReport(ByVal claim_number As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/TECHNICAL_REPORT")
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim Claim_id As Guid = New Guid(claim_number)

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_CLAIM_ID, Claim_id.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function

    Public Function GetExtendedStatusComment(ByVal claim_number As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/EXTENDED_STATUS_COMMENT")
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim Claim_id As Guid = New Guid(claim_number)

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_CLAIM_ID, Claim_id.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function

    'Public Sub ObtainAndAssignClaimNumber(ByVal Row As DataRow, ByVal suffix As String)
    '    Dim dal As New ClaimDAL
    '    Dim claimNumberInfo As ClaimDAL.ClaimNumberInfo = dal.GetClaimNumber(Me.CompanyId(Row))
    '    If Me.ClaimGroupId(Row).Equals(Guid.Empty) Then Me.ClaimGroupId(Row) = claimNumberInfo.ClaimGroupId
    '    If Me.ClaimNumber(Row) Is Nothing Then Me.ClaimNumber(Row) = claimNumberInfo.ClaimNumber & suffix
    '    If Me.MasterClaimNumber(Row) Is Nothing Then Me.MasterClaimNumber(Row) = Me.ClaimNumber(Row)
    'End Sub

    Public Sub ObtainAndAssignClaimNumber(ByVal familyDataset As DataSet, ByVal galaxyClaimNumberList As ArrayList)
        Dim dal As New ClaimDAL
        Dim oRow As DataRow
        Dim suffix As String
        Dim claimNumberInfo As ClaimDAL.ClaimNumberInfo
        Dim oGalaxyClaimNumber As GalaxyClaimNumber

        For Each oRow In familyDataset.Tables(Me.TABLE_NAME).Rows
            If Me.IsNew(oRow) AndAlso Me.ClaimNumber(oRow) Is Nothing Then
                suffix = String.Empty
                If (Me.MethodOfRepairCode(oRow) = METHOD_OF_REPAIR__REPLACEMENT AndAlso Me.CLaimAuthTypeCode(oRow) = CLAIM_AUTH_TYPE_SINGLE) Then
                    suffix = "R"
                End If

                If galaxyClaimNumberList Is Nothing Then
                    claimNumberInfo = dal.GetClaimNumber(Me.CompanyId(oRow))
                Else    ' Galaxy
                    oGalaxyClaimNumber = (From galaxyClaimNumberItem As GalaxyClaimNumber In galaxyClaimNumberList
                                          Where galaxyClaimNumberItem.moClaimId.Equals(Me.Id(oRow))
                                          Select galaxyClaimNumberItem).First

                    claimNumberInfo = dal.GetClaimNumber(Me.CompanyId(oRow), oGalaxyClaimNumber.moGalaxyClaimNumber,
                                                          oGalaxyClaimNumber.moCoverageCode, oGalaxyClaimNumber.moUnitNumber)
                End If

                If Me.ClaimGroupId(oRow).Equals(Guid.Empty) Then Me.ClaimGroupId(oRow) = claimNumberInfo.ClaimGroupId
                If String.IsNullOrEmpty(Me.MasterClaimNumber(oRow)) Then Me.MasterClaimNumber(oRow) = claimNumberInfo.ClaimNumber
                Me.ClaimNumber(oRow) = claimNumberInfo.ClaimNumber & suffix
            End If
        Next
    End Sub

    Public Function GetClaimsCntByLossDatesSplService(ByVal certId As Guid, ByVal CauseOfLoss As Guid, Optional ByVal CurrentLossDate As String = Nothing) As DataSet
        Dim ds As New DataSet
        Dim whereClauseConditions As String = ""

        Dim selectStmt As String = Me.Config("/SQL/CLAIM_LOSSDATES_COMPARE_TO_NEW_LOSSDATE_SPL_SVC")

        If Not CurrentLossDate Is Nothing Then
            whereClauseConditions &= Environment.NewLine & " AND abs(months_between( c.loss_date , to_date('" & CurrentLossDate & "','mm/dd/yyyy'))) <= 12"
        End If

        If Not CauseOfLoss = Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " AND c.CAUSE_OF_LOSS_ID =  '" & Me.GuidToSQLString(CauseOfLoss) & "'"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                {New DBHelper.DBHelperParameter(COL_NAME_CERTIFICATE_ID, certId.ToByteArray)}
        ', New DBHelper.DBHelperParameter(COL_NAME_LOSS_DATE, CurrentLossDate.ToString("MM/dd/yyyy"))}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function

    'Def-1113
    'Start
    Public Sub UpdateActiveSession(ByVal operation As String, ByVal processId As Guid, ByVal networkId As String)
        Try
            Select Case operation
                Case "INSERT"
                    CreateActiveSessionRec(processId, networkId)
                Case "DELETE"
                    RemoveActiveSessionRec(processId)
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub CreateActiveSessionRec(ByVal processId As Guid, ByVal networkId As String)

        Dim dt As New DataTable
        Dim dc As DataColumn
        Dim pk(1) As DataColumn
        Dim dr As DataRow

        Try

            dc = New DataColumn("PROCESS_ID")
            dc.DataType = Type.GetType("System.Guid")
            dt.Columns.Add(dc)
            pk(0) = dc

            dc = New DataColumn("DESCRIPTION")
            dc.DataType = Type.GetType("System.String")
            dt.Columns.Add(dc)

            dc = New DataColumn("ACTIVE")
            dc.DataType = Type.GetType("System.String")
            dt.Columns.Add(dc)

            dc = New DataColumn("CREATED_BY")
            dc.DataType = Type.GetType("System.String")
            dt.Columns.Add(dc)

            dc = New DataColumn("CREATED_DATE")
            dc.DataType = Type.GetType("System.DateTime")
            dt.Columns.Add(dc)

            dc = New DataColumn("MODIFIED_BY")
            dc.DataType = Type.GetType("System.String")
            dt.Columns.Add(dc)

            dc = New DataColumn("MODIFIED_DATE")
            dc.DataType = Type.GetType("System.DateTime")
            dt.Columns.Add(dc)

            dr = dt.NewRow()

            dr("PROCESS_ID") = processId 'CType(processId, Guid).ToByteArray
            dr("DESCRIPTION") = "Row inserted by Elita Web application - ClaimDAL"
            dr("ACTIVE") = "Y"
            dr("CREATED_BY") = networkId '"ELP_APP_USER"
            dr("CREATED_DATE") = DateTime.Now
            dr("MODIFIED_BY") = DBNull.Value
            dr("MODIFIED_DATE") = DBNull.Value
            dt.Rows.Add(dr)

            dt.PrimaryKey = pk

            DBHelper.Execute(dt, Config("/SQL/INSERT_ACTIVE_SESSION"), "", "", Nothing, Nothing, DataRowState.Added)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub RemoveActiveSessionRec(ByVal processId As Guid)
        Dim tr As IDbTransaction = DBHelper.GetNewTransaction
        Dim deleteStmt As String = Me.Config("/SQL/DELETE_ACTIVE_SESSION")
        Dim inputParameters(0) As DBHelper.DBHelperParameter

        Try
            inputParameters(0) = New DBHelper.DBHelperParameter("PROCESS_ID", processId.ToByteArray)
            DBHelper.Execute(deleteStmt, inputParameters, tr)

            DBHelper.Commit(tr)
        Catch ex As Exception
            DBHelper.RollBack(tr)
            Throw ex
        End Try
    End Sub

    'end

    Public Function GetClaimReserveAmount(ByVal claimId As Guid) As DecimalType
        Dim selectStmt As String = Me.Config("/SQL/GET_RESERVE_AMOUNT")
        Dim returnValue As Object
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_ID, claimId.ToByteArray)}
        Try

            returnValue = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (returnValue Is Nothing) Then
                Return Nothing
            Else
                Return DirectCast(returnValue, Decimal)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function IsCertCoveragesEligibleforCancel(ByVal CertId As Guid, ByVal CertItemCoverageId As Guid, ByVal lossDate As DateType) As Integer
        Dim selectStmt As String = Me.Config("/SQL/CERT_COVERAGES_ELIGIBILITY_FOR_CANCEL")
        Dim returnValue As Object
        Dim myLossDate As Date

        If lossDate Is Nothing Then
            myLossDate = Date.Today
        Else
            myLossDate = lossDate.Value
        End If
        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(Me.COL_NAME_CERTIFICATE_ID, CertId.ToByteArray) _
                        , New DBHelper.DBHelperParameter(Me.COL_NAME_CERT_ITEM_COVERAGE_ID, CertItemCoverageId.ToByteArray) _
                        , New DBHelper.DBHelperParameter(Me.COL_NAME_LOSS_DATE, myLossDate)}
        Try

            returnValue = DBHelper.ExecuteScalar(selectStmt, inParameters)
            Return CType(returnValue, Integer)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function ProductRemainLiabilityAmount(ByVal CertId As Guid, ByVal lossDate As DateType) As Decimal
        Dim selectStmt As String = Me.Config("/SQL/PRODUCT_REMAIN_LIABILITY_LIMIT")
        Dim returnValue As Object
        Dim myLossDate As Date

        If lossDate Is Nothing Then
            myLossDate = Date.Today
        Else
            myLossDate = lossDate.Value
        End If
        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(Me.COL_NAME_CERTIFICATE_ID, CertId.ToByteArray) _
                        , New DBHelper.DBHelperParameter(Me.COL_NAME_LOSS_DATE, myLossDate)}


        Try

            returnValue = DBHelper.ExecuteScalar(selectStmt, inParameters)
            If (returnValue Is Nothing) Then
                Return Nothing
            Else
                Return DirectCast(returnValue, Decimal)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function CoverageRemainLiabilityAmount(ByVal CertItemCoverageId As Guid, ByVal lossDate As DateType) As Decimal
        Dim selectStmt As String = Me.Config("/SQL/COVERAGE_REMAIN_LIABILITY_LIMIT")
        Dim returnValue As Object
        Dim myLossDate As Date

        If lossDate Is Nothing Then
            myLossDate = Date.Today
        Else
            myLossDate = lossDate.Value
        End If
        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(Me.COL_NAME_CERT_ITEM_COVERAGE_ID, CertItemCoverageId.ToByteArray) _
                        , New DBHelper.DBHelperParameter(Me.COL_NAME_LOSS_DATE, myLossDate)}
        Try

            returnValue = DBHelper.ExecuteScalar(selectStmt, inParameters)
            If (returnValue Is Nothing) Then
                Return Nothing
            Else
                Return DirectCast(returnValue, Decimal)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetClaimIDWithCertAndDealer(ByVal certID As Guid, ByVal DealerID As Guid, ByVal claim_number As String) As DataSet

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/WS_GetClaimIDWithCertAndDealer")

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_DEALER_ID, DealerID.ToByteArray),
                                            New OracleParameter(Me.COL_NAME_CERTIFICATE_ID, certID.ToByteArray),
                                            New OracleParameter(Me.COL_NAME_CLAIM_NUMBER, claim_number)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function GetLatestExtendedClaimStatus(ByVal cliamId As Guid, ByVal languageId As Guid) As String

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_LATEST_EXTENDED_STATUS")

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_CLAIM_ID, cliamId.ToByteArray),
                                            New OracleParameter(Me.COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

        Return ds.Tables(0).Rows(0)("extended_status").ToString

    End Function
#End Region

#Region "Lock Claim"

    Public Function Lock_Claim(ByVal ClaimId As Guid, ByVal LockBy As String) As DataSet
        Dim sqlStmt As String
        sqlStmt = Me.Config("/SQL/CLAIM_LOCK")

        Dim Inputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_ID, ClaimId.ToByteArray, GetType(Guid)) _
        , New DBHelper.DBHelperParameter(Me.COL_NAME_LOCKED_BY, LockBy, GetType(String))}

        Dim Outputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
         {New DBHelper.DBHelperParameter("p_error_code", GetType(String), 100),
         New DBHelper.DBHelperParameter("p_error_description", GetType(String), 1000)}

        Dim ds As New DataSet(Me.CLAIM_LOCK_STATUS)
        ' Call DBHelper Store Procedure

        Try
            DBHelper.FetchSp(sqlStmt, Inputparameters, Outputparameters, ds, Me.CLAIM_LOCK_STATUS)

            If Outputparameters(Me.P_RETURN).Value <> 0 Then
                ds.Tables(0).TableName = Me.CLAIM_LOCK_STATUS
                Return ds
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Function UnLock_Claim(ByVal ClaimId As Guid, ByVal LockBy As String) As DataSet
        Dim sqlStmt As String
        sqlStmt = Me.Config("/SQL/CLAIM_UNLOCK")

        Dim Inputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_ID, ClaimId.ToByteArray, GetType(Guid)) _
        , New DBHelper.DBHelperParameter(Me.COL_NAME_LOCKED_BY, LockBy, GetType(String))}

        Dim Outputparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
         {New DBHelper.DBHelperParameter("p_error_code", GetType(String), 100),
         New DBHelper.DBHelperParameter("p_error_description", GetType(String), 1000)}

        Dim ds As New DataSet(Me.CLAIM_LOCK_STATUS)
        ' Call DBHelper Store Procedure

        Try
            DBHelper.FetchSp(sqlStmt, Inputparameters, Outputparameters, ds, Me.CLAIM_LOCK_STATUS)

            If Outputparameters(Me.P_RETURN).Value <> 0 Then
                ds.Tables(0).TableName = Me.CLAIM_LOCK_STATUS
                Return ds
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function


#End Region

#Region "Claim Logistics"

    Public Function WS_GetClaimStatusInfo(ByVal CustomerIdentifier As String, ByVal IdentifierType As String, ByVal DealerId As Guid, ByVal userId As Guid, ByVal BillingZipCode As String, ByVal LanguageISOCode As String, ByVal CertificateNumber As String, ByRef ValidateErrorCode As Integer) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/WS_GetClaimStatusInfo")
        Dim inputParameters(Me.TOTAL_INPUT_PARAM_WS_1) As DBHelper.DBHelperParameter
        Dim outputParameter(Me.TOTAL_OUTPUT_PARAM_WS) As DBHelper.DBHelperParameter


        inputParameters = New DBHelper.DBHelperParameter() _
                {SetParameter(Me.SP_PARAM_NAME__CUSTOMER_IDENTIFIER, CustomerIdentifier),
                 SetParameter(Me.SP_PARAM_NAME__IDENTIFIER_TYPE, IdentifierType),
                 SetParameter(Me.SP_PARAM_NAME__SYSTEM_USER_ID, userId.ToByteArray),
                 SetParameter(Me.SP_PARAM_NAME__BILLING_ZIP_CODE, BillingZipCode),
                 SetParameter(Me.SP_PARAM_NAME__LANGUAGE_ISO_CODE, LanguageISOCode),
                 SetParameter(Me.SP_PARAM_NAME__DEALER_ID, DealerId.ToByteArray),
                              SetParameter(Me.SP_PARAM_NAME__CERTIFICATE_NUMBER, CertificateNumber)
                }

        outputParameter(Me.P_RETURN) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__RETURN, GetType(Integer))
        outputParameter(Me.P_EXCEPTION_MSG) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__EXCEPTION_MSG, GetType(String), 50)
        outputParameter(Me.P_CURSOR_CLAIM_STATUS_INFO) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__CLAIM_STATUS_INFO, GetType(DataSet))
        outputParameter(Me.P_CURSOR_EXTENDED_STATUS_INFO) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__EXTENDED_CLAIM_STATUS_INFO, GetType(DataSet))
        outputParameter(Me.P_CURSOR_RESPONSE_STATUS) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__RESPONSE_STATUS, GetType(DataSet))

        Dim ds As New DataSet(Me.DATASET_NAME__CLAIM_CHECK_RESPONSE)
        ' Call DBHelper Store Procedure
        If DealerId.Equals(Guid.Empty) Then
            inputParameters(5).Value = DBNull.Value
        End If

        Try
            DBHelper.FetchSp(selectStmt, inputParameters, outputParameter, ds, Me.TABLE_NAME__GET_CLAIM_STATUS_INFO_RESPONSE)

            If outputParameter(Me.P_RETURN).Value <> 0 Then
                ds.Tables(0).TableName = Me.TABLE_NAME__RESPONSE_STATUS
                ValidateErrorCode = outputParameter(Me.P_RETURN).Value
                Return ds
            Else
                ds.Tables(0).TableName = Me.TABLE_NAME__GET_CLAIM_STATUS_INFO_RESPONSE
                ds.Tables(1).TableName = Me.TABLE_NAME__GET_CLAIM_EXT_STATUS_INFO_RESPONSE
                ds.Tables(2).TableName = Me.TABLE_NAME__RESPONSE_STATUS
                ValidateErrorCode = outputParameter(Me.P_RETURN).Value
                Dim ClaimStatusToExtendedStatusRel As New DataRelation(Me.TABLE_NAME_CLAIMSTATUS_EXTENDEDSTATUS_RELATIONS,
                                                 ds.Tables(Me.TABLE_NAME__GET_CLAIM_STATUS_INFO_RESPONSE).Columns(Me.COL_NAME_CLAIMNUMBER_WS),
                                                 ds.Tables(Me.TABLE_NAME__GET_CLAIM_EXT_STATUS_INFO_RESPONSE).Columns(Me.COL_NAME_CLAIMNUMBER_WS))
                ClaimStatusToExtendedStatusRel.Nested = True
                ds.Relations.Add(ClaimStatusToExtendedStatusRel)
                Return ds
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function WebSubmitClaimPreValidate(ByVal oWebSubmitClaimPreValidateInputData As WebSubmitClaimPreValidateInputData,
                                              ByRef oWebSubmitClaimPreValidateOutputData As WebSubmitClaimPreValidateOutputData) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/WS_SubmitClaimPreValidate")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter(Me.TOTAL_OUTPUT_PARAM_WS_1) As DBHelper.DBHelperParameter


        inputParameters = New DBHelper.DBHelperParameter() _
                {SetParameter(Me.SP_PARAM_NAME__CUSTOMER_IDENTIFIER, oWebSubmitClaimPreValidateInputData.CustomerIdentifier),
                 SetParameter(Me.SP_PARAM_NAME__IDENTIFIER_TYPE, oWebSubmitClaimPreValidateInputData.IdentifierType),
                 SetParameter(Me.SP_PARAM_NAME__SYSTEM_USER_ID, oWebSubmitClaimPreValidateInputData.SystemUserId.ToByteArray),
                 SetParameter(Me.SP_PARAM_NAME__DEALER_CODE, oWebSubmitClaimPreValidateInputData.DealerCode),
                 SetParameter(Me.SP_PARAM_NAME__COVERAGE_CODE, oWebSubmitClaimPreValidateInputData.CoverageCode),
                 SetParameter(Me.SP_PARAM_NAME__SERVICE_CENTER_CODE, oWebSubmitClaimPreValidateInputData.ServiceCenterCode),
                 SetParameter(Me.SP_PARAM_NAME__SERIAL_NUMBER, oWebSubmitClaimPreValidateInputData.SerialNumber),
                 SetParameter(Me.SP_PARAM_NAME__MAKE, oWebSubmitClaimPreValidateInputData.Make),
                 SetParameter(Me.SP_PARAM_NAME__MODEL, oWebSubmitClaimPreValidateInputData.Model),
                 SetParameter(Me.SP_PARAM_NAME__CAUSE_OF_LOSS_CODE, oWebSubmitClaimPreValidateInputData.CauseOfLossCode),
                 SetParameter(Me.SP_PARAM_NAME__COUNTRY_CODE, oWebSubmitClaimPreValidateInputData.CountryCode),
                 SetParameter(Me.SP_PARAM_NAME__REGION_CODE, oWebSubmitClaimPreValidateInputData.RegionCode),
                 SetParameter(Me.SP_PARAM_NAME__ADDRESS_TYPE_CODE, oWebSubmitClaimPreValidateInputData.AddressTypeCode),
                 SetParameter(Me.SP_PARAM_NAME__PAYMENT_METHOD, oWebSubmitClaimPreValidateInputData.PaymentMethod),
                 SetParameter(Me.SP_PARAM_NAME__DATE_OF_LOSS, oWebSubmitClaimPreValidateInputData.DateOfLoss)
                 }

        outputParameter(Me.P_RETURN) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__RETURN, GetType(Integer))
        outputParameter(Me.P_EXCEPTION_MSG) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__EXCEPTION_MSG, GetType(String), 100)
        outputParameter(Me.P_COVERAGE_TYPE_ID) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__COVERAGE_TYPE_ID, oWebSubmitClaimPreValidateOutputData.CoverageTypeId.ToByteArray.GetType)
        outputParameter(Me.P_CERT_ITEM_COVERAGE_ID) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__CERT_ITEM_COVERAGE_ID, oWebSubmitClaimPreValidateOutputData.CertItemCoverageId.ToByteArray.GetType)
        outputParameter(Me.P_SERVICE_CENTER_ID) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__SERVICE_CENTER_ID, oWebSubmitClaimPreValidateOutputData.ServiceCenterId.ToByteArray.GetType)
        outputParameter(Me.P_CAUSE_OF_LOSS_ID) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__CAUSE_OF_LOSS_ID, oWebSubmitClaimPreValidateOutputData.CauseOfLossId.ToByteArray.GetType)
        outputParameter(Me.P_COUNTRY_ID) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__COUNTRY_ID, oWebSubmitClaimPreValidateOutputData.CountryId.ToByteArray.GetType)
        outputParameter(Me.P_REGION_ID) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__REGION_ID, oWebSubmitClaimPreValidateOutputData.RegionId.ToByteArray.GetType)
        outputParameter(Me.P_HOME_PHONE) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__HONE_PHONE, GetType(String), 20)
        outputParameter(Me.P_WORK_PHONE) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__WORK_PHONE, GetType(String), 20)
        outputParameter(Me.P_ADDRESS_TYPE_ID) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__ADDRESS_TYPE_ID, oWebSubmitClaimPreValidateOutputData.AddressTypeId.ToByteArray.GetType)
        outputParameter(Me.P_DEDUCTIBLE) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__DEDUCTIBLE, GetType(Decimal))
        outputParameter(Me.P_PAYMENT_METHOD_ID) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__PAYMENT_METHOD_ID, oWebSubmitClaimPreValidateOutputData.PaymentMethodId.ToByteArray.GetType)
        outputParameter(Me.P_CLAIM_STATUS_FOR_EXT_SYS_CODE) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__DEFAULT_CLAIM_STATUS_FOR_EXT_SYS_CODE, GetType(String), 50)
        outputParameter(Me.P_INVALID_SERIAL_NUMBER) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__INVALID_SERIAL_NUMBER, GetType(Integer))
        outputParameter(Me.P_INVALID_MAKE_MODEL) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__INVALID_MAKE_MODEL, GetType(Integer))
        outputParameter(Me.P_CURSOR_RESPONSE_STATUS_1) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__RESPONSE_STATUS, GetType(DataSet))

        Dim ds As New DataSet(Me.DATASET_NAME__SUBMIT_CLAIM_RESPONSE)
        ' Call DBHelper Store Procedure

        Try
            DBHelper.FetchSp(selectStmt, inputParameters, outputParameter, ds, Me.TABLE_NAME__RESPONSE_STATUS)

            If outputParameter(Me.P_RETURN).Value <> 0 Then
                oWebSubmitClaimPreValidateOutputData.CoverageTypeId = Guid.Empty
                oWebSubmitClaimPreValidateOutputData.CertItemCoverageId = Guid.Empty
                oWebSubmitClaimPreValidateOutputData.ServiceCenterId = Guid.Empty
                oWebSubmitClaimPreValidateOutputData.CauseOfLossId = Guid.Empty
                oWebSubmitClaimPreValidateOutputData.CountryId = Guid.Empty
                oWebSubmitClaimPreValidateOutputData.RegionId = Guid.Empty
                oWebSubmitClaimPreValidateOutputData.AddressTypeId = Guid.Empty
                oWebSubmitClaimPreValidateOutputData.WorkPhone = Nothing
                oWebSubmitClaimPreValidateOutputData.HomePhone = Nothing
                oWebSubmitClaimPreValidateOutputData.Deductible = Nothing
                oWebSubmitClaimPreValidateOutputData.PaymentMethodId = Guid.Empty
                oWebSubmitClaimPreValidateOutputData.DefaultClaimStatusCode = Nothing
                oWebSubmitClaimPreValidateOutputData.PreValidateErrorCode = outputParameter(Me.P_RETURN).Value
                oWebSubmitClaimPreValidateOutputData.PreValidateError = outputParameter(Me.P_EXCEPTION_MSG).Value
                oWebSubmitClaimPreValidateOutputData.PreValidateMakeModelErrorCode = outputParameter(Me.P_INVALID_MAKE_MODEL).Value
                oWebSubmitClaimPreValidateOutputData.PreValidateSerialNumberErrorCode = outputParameter(Me.P_INVALID_SERIAL_NUMBER).Value
                ds.Tables(0).TableName = Me.TABLE_NAME__RESPONSE_STATUS
                Return ds
            Else
                oWebSubmitClaimPreValidateOutputData.CoverageTypeId = outputParameter(Me.P_COVERAGE_TYPE_ID).Value
                oWebSubmitClaimPreValidateOutputData.CertItemCoverageId = outputParameter(Me.P_CERT_ITEM_COVERAGE_ID).Value
                oWebSubmitClaimPreValidateOutputData.ServiceCenterId = outputParameter(Me.P_SERVICE_CENTER_ID).Value
                oWebSubmitClaimPreValidateOutputData.CauseOfLossId = outputParameter(Me.P_CAUSE_OF_LOSS_ID).Value
                oWebSubmitClaimPreValidateOutputData.CountryId = outputParameter(Me.P_COUNTRY_ID).Value
                oWebSubmitClaimPreValidateOutputData.RegionId = outputParameter(Me.P_REGION_ID).Value
                oWebSubmitClaimPreValidateOutputData.AddressTypeId = outputParameter(Me.P_ADDRESS_TYPE_ID).Value
                oWebSubmitClaimPreValidateOutputData.WorkPhone = outputParameter(Me.P_WORK_PHONE).Value
                oWebSubmitClaimPreValidateOutputData.HomePhone = outputParameter(Me.P_HOME_PHONE).Value
                oWebSubmitClaimPreValidateOutputData.Deductible = outputParameter(Me.P_DEDUCTIBLE).Value
                oWebSubmitClaimPreValidateOutputData.PaymentMethodId = outputParameter(Me.P_PAYMENT_METHOD_ID).Value
                oWebSubmitClaimPreValidateOutputData.DefaultClaimStatusCode = outputParameter(Me.P_CLAIM_STATUS_FOR_EXT_SYS_CODE).Value
                ds.Tables(0).TableName = Me.TABLE_NAME__RESPONSE_STATUS
                oWebSubmitClaimPreValidateOutputData.PreValidateErrorCode = outputParameter(Me.P_RETURN).Value
                oWebSubmitClaimPreValidateOutputData.PreValidateError = outputParameter(Me.P_EXCEPTION_MSG).Value
                oWebSubmitClaimPreValidateOutputData.PreValidateMakeModelErrorCode = outputParameter(Me.P_INVALID_MAKE_MODEL).Value
                oWebSubmitClaimPreValidateOutputData.PreValidateSerialNumberErrorCode = outputParameter(Me.P_INVALID_SERIAL_NUMBER).Value
                Return ds
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Claim Issues"
    Public Function CheckClaimPaymentInProgress(ByVal claimId As Guid, ByVal companyGroupId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/CHECK_CLAIM_PAYMENT_IN_PROGRESS")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                   New DBHelper.DBHelperParameter("claim_id", claimId.ToByteArray),
                   New DBHelper.DBHelperParameter("company_group_id", companyGroupId.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, "CHECK_CLAIM_PAYMENT", parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Private Methods"

    Function SetParameter(ByVal name As String, ByVal value As Object) As DBHelper.DBHelperParameter

        name = name.Trim
        If value Is Nothing Then value = DBNull.Value
        If value.GetType Is GetType(String) Then value = DirectCast(value, String).Trim

        Return New DBHelper.DBHelperParameter(name, value)

    End Function

#End Region

#Region "SearchClaimInfo"

    Public Structure ClaimAuthTypeDetails
        Public ClaimAuthTypeId As String
        Public IsClaimLocked As String
        Public ClaimLockedBy As String
        Public ClaimId As String
    End Structure

    Public Function GetClaimInfo(
                                ByVal claimId As Guid,
                                ByVal claimNumber As String,
                                ByVal companyId As Guid,
                                ByVal companyCode As String,
                                ByVal dealerId As Guid,
                                ByVal dealerCode As String,
                                ByVal serialNumber As String,
                                ByVal networId As String) As ClaimAuthTypeDetails
        Dim returnValue As ClaimAuthTypeDetails
        returnValue.ClaimAuthTypeId = String.Empty
        returnValue.IsClaimLocked = String.Empty
        returnValue.ClaimLockedBy = String.Empty
        returnValue.ClaimId = String.Empty


        Dim selectStmt As String = Me.Config("/SQL/SEARCH_CLAIM_INFO")
        Dim inputParameters(7) As DBHelperParameter
        Dim outputParameter(5) As DBHelperParameter

        If claimId.Equals(Guid.Empty) Then
            inputParameters(0) = New DBHelperParameter(Me.PAR_NAME_IP_CLAIM_ID, DBNull.Value)
        Else
            inputParameters(0) = New DBHelperParameter(Me.PAR_NAME_IP_CLAIM_ID, claimId, GetType(Guid))
        End If
        inputParameters(1) = New DBHelperParameter(Me.PAR_NAME_IP_CLAIM_NUMBER, claimNumber, GetType(String))
        If companyId.Equals(Guid.Empty) Then
            inputParameters(2) = New DBHelperParameter(Me.PAR_NAME_IP_COMPANY_ID, DBNull.Value)
        Else
            inputParameters(2) = New DBHelperParameter(Me.PAR_NAME_IP_COMPANY_ID, companyId, GetType(Guid))
        End If
        inputParameters(3) = New DBHelperParameter(Me.PAR_NAME_IP_COMPANY_CODE, companyCode, GetType(String))
        If dealerId.Equals(Guid.Empty) Then
            inputParameters(4) = New DBHelperParameter(Me.PAR_NAME_IP_DEALER_ID, DBNull.Value)
        Else
            inputParameters(4) = New DBHelperParameter(Me.PAR_NAME_IP_DEALER_ID, dealerId, GetType(Guid))
        End If
        inputParameters(5) = New DBHelperParameter(Me.PAR_NAME_IP_DEALER_CODE, dealerCode, GetType(String))
        inputParameters(6) = New DBHelperParameter(Me.PAR_NAME_IP_SERIAL_NUMBER, serialNumber, GetType(String))
        inputParameters(7) = New DBHelperParameter(Me.PAR_NAME_IP_NETWORK_ID, networId, GetType(String))


        outputParameter(0) = New DBHelperParameter(Me.PAR_NAME_OP_CLAIM_AUTH_TYPE_ID, GetType(String), 32)
        outputParameter(1) = New DBHelperParameter(Me.PAR_NAME_OP_CLAIM_ID, GetType(String), 32)
        outputParameter(2) = New DBHelperParameter(Me.PAR_NAME_OP_IS_CLAIM_LOCKED, GetType(String), 32)
        outputParameter(3) = New DBHelperParameter(Me.PAR_NAME_OP_CLAIM_LOCK_BY, GetType(String), 32)
        outputParameter(4) = New DBHelperParameter(Me.PAR_NAME_OP_RETURN, GetType(Integer), 32)
        outputParameter(5) = New DBHelperParameter(Me.PAR_NAME_OP_EXCEPTION_MESSAGE, GetType(String), 500)

        'Try
        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

        If CType(outputParameter(4).Value, Integer) <> 0 Then
            If CType(outputParameter(4).Value, Integer) = 200 Then
                Throw New StoredProcedureGeneratedException("Search Claim ERROR : ", outputParameter(5).Value)
            Else
                Dim e As New ApplicationException("Return Value = " & outputParameter(4).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
            End If
        Else

            If (Not outputParameter(0).Value Is Nothing) Then
                returnValue.ClaimAuthTypeId = outputParameter(0).Value
            End If
            If (Not outputParameter(1).Value Is Nothing) Then
                returnValue.ClaimId = outputParameter(1).Value
            End If
            If (Not outputParameter(2).Value Is Nothing) Then
                returnValue.IsClaimLocked = outputParameter(2).Value
            End If
            If (Not outputParameter(3).Value Is Nothing) Then
                returnValue.ClaimLockedBy = outputParameter(3).Value
            End If
        End If

        Return returnValue
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try

    End Function

    Public Function GetFraudulentClaimExtensions(ByVal claimId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_FRAUD_CLAIM_EXT")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claimId", claimId.ToByteArray)}
        outputParameter(0) = New DBHelper.DBHelperParameter("fraudulentClaimExtensions", GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, parameters, outputParameter, ds, "GetFraudClaimExtns")
            ds.Tables(0).TableName = "GetFraudClaimExtns"

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetDealerDRPTradeInQuoteFlag(ByVal dealerCode As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_DEALER_DRP_TRADE_IN_QUOTE_FLAG")
        Try
            Dim ds As New DataSet
            Dim parameters() As OracleParameter
            parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_DEALER, dealerCode)}

            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME_ATTRIBUTE, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Web Service Methods"

    ''' <summary>
    ''' Gets Claims based on Dealer and Serial Number (IMEI Number)
    ''' </summary>
    ''' <param name="dealer">Dealer Code</param>
    ''' <param name="serialNumber">Serial Number / IMEI Number of Device</param>
    ''' <param name="userId">User ID requesting the </param>
    ''' <returns><see cref="DataSet" /></returns>
    ''' <remarks></remarks>
    Public Function LoadClaimsBySerialNumber(ByVal countryCode As String,
                                             ByVal companyCode As String,
                                             ByVal dealerCode As String,
                                             ByVal serialNumber As String,
                                             ByVal userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/WS_CLAIMS_GET_CLAIMS")

        Try
            Dim parameters = New OracleParameter() _
                             {New OracleParameter(COL_NAME_DEALER_CODE, dealerCode),
                                 New OracleParameter(COL_NAME_COMPANY_CODE, companyCode),
                                 New OracleParameter(COL_NAME_COUNTRY_CODE, countryCode),
                                 New OracleParameter(COL_NAME_SERIAL_NUMBER, serialNumber),
                                 New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray()),
                                 New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray())
                             }

            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    ''' <summary>
    ''' Gets Claims based on Dealer and IMEI Number
    ''' </summary>
    ''' <param name="dealer">Dealer Code</param>
    ''' <param name="imeiNumber">IMEI Number of Device</param>
    ''' <param name="userId">User ID requesting the </param>
    ''' <returns><see cref="DataSet" /></returns>
    ''' <remarks></remarks>
    Public Function LoadClaimsByImeiNumber(ByVal countryCode As String,
                                           ByVal companyCode As String,
                                           ByVal dealerCode As String,
                                           ByVal imeiNumber As String,
                                           ByVal userId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/WS_CLAIMS_GET_CLAIMS_BY_IMEI_NUMBER")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
            New DBHelper.DBHelperParameter("pi_country_code", countryCode),
            New DBHelper.DBHelperParameter("pi_company_code", companyCode),
            New DBHelper.DBHelperParameter("pi_dealer_code", dealerCode),
            New DBHelper.DBHelperParameter("pi_imei_number", imeiNumber),
            New DBHelper.DBHelperParameter("pi_user_id", userId.ToByteArray())}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelperParameter() {
            New DBHelper.DBHelperParameter("po_claims_data", GetType(DataSet))}

        Try
            Dim ds As New DataSet
            DBHelper.FetchSp(selectStmt, inputParameters, outputParameters, ds, Me.TABLE_NAME)

            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)

        End Try
    End Function

    ''' <summary>
    ''' WS_CHLMobileSCPortal_GetCertClaimInfo : Gets Claims based on Company and Serial Number / Phone Number
    ''' </summary>
    ''' <param name="CompanyCode">Company Code</param>
    ''' <param name="LanguageId">Portal User's language</param>
    ''' <param name="SerialNumber">Serial Number of the certificate found on the certificate item</param>
    ''' <param name="PhoneNumber">Phone Number of the customer the certificate belongs</param>
    ''' <returns>Certificate_Info => Returns Certificate information</returns>
    ''' <returns>Claim_Info => Returns Claim information of the Certificate</returns>
    ''' <returns>Is_Valid => Returns if the data retrival is successfull</returns>
    ''' <remarks></remarks>
    Public Function WS_CHLMobileSCPortal_GetCertClaimInfo(ByVal CompanyCode As String,
                                                          ByVal UserId As Guid,
                                                          ByVal LanguageId As Guid,
                                                          ByVal SerialNumber As String,
                                                          ByVal PhoneNumber As String,
                                                          ByVal taxId As String,
                                                          ByVal claimStatusCode As String,
                                                          ByRef ErrorCode As String,
                                                          ByRef ErrorMessage As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/CHLMobileSCPortal_GetCertClaimInfo")

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                New DBHelper.DBHelperParameter("pi_company_code", CompanyCode),
                                New DBHelper.DBHelperParameter("pi_user_id", UserId.ToByteArray),
                                New DBHelper.DBHelperParameter("pi_language_id", LanguageId.ToByteArray),
                                New DBHelper.DBHelperParameter("pi_serial_number", SerialNumber),
                                New DBHelper.DBHelperParameter("pi_phone_number", PhoneNumber),
                                New DBHelper.DBHelperParameter("pi_tax_id", taxId),
                                New DBHelper.DBHelperParameter("pi_claim_status", claimStatusCode)}

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                New DBHelper.DBHelperParameter("po_certificate_info", GetType(DataSet)),
                                New DBHelper.DBHelperParameter("po_claim_info", GetType(DataSet)),
                                New DBHelper.DBHelperParameter("po_error_code", GetType(String), 255),
                                New DBHelper.DBHelperParameter("po_error_message", GetType(String), 255)}

        Dim ds As New DataSet

        Try
            DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, "CertClaimInfoResponse")

            If String.IsNullOrEmpty(outParameters(2).Value) Then
                ds.Tables(0).TableName = "CertificateInfo"
                ds.Tables(1).TableName = "ClaimInfo"
            Else
                ErrorCode = outParameters(2).Value
                ErrorMessage = outParameters(3).Value
            End If
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function WS_SNMPORTAL_SA_CLAIMREPORT(ByVal companyCode As String,
                                                ByVal userId As Guid,
                                                ByVal languageId As Guid,
                                                ByVal serviceCenterCode As String,
                                                ByVal countryIsoCode As String,
                                                ByVal fromDate As date,
                                                ByVal endDate As date,
                                                ByVal extendedStatusCode As String,
                                                ByVal dealerCode As String,
                                                Byval pageSize As Integer,
                                                ByRef batchId as guid,
                                                ByRef totalRecordCount as Integer,
                                                ByRef totalRecordsInQueue as Integer,
                                                ByRef errorCode As String,
                                                ByRef errorMessage As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/WS_SNMPORTAL_SA_CLAIMREPORT")

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                New DBHelper.DBHelperParameter("pi_service_center_code", serviceCenterCode),
                                New DBHelper.DBHelperParameter("pi_country_iso_code", countryIsoCode),
                                New DBHelper.DBHelperParameter("pi_from_date", fromDate),
                                New DBHelper.DBHelperParameter("pi_to_date", endDate),
                                New DBHelper.DBHelperParameter("pi_extended_status_code", extendedStatusCode),
                                New DBHelper.DBHelperParameter("pi_company_code", companyCode),
                                New DBHelper.DBHelperParameter("pi_dealer_code", dealerCode),
                                New DBHelper.DBHelperParameter("pi_user_id", userId.ToByteArray()),
                                New DBHelper.DBHelperParameter("pi_language_id", languageId.ToByteArray),
                                New DBHelper.DBHelperParameter("pi_page_size", pageSize)}

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                New DBHelper.DBHelperParameter("po_batch_id", GetType(guid)),
                                New DBHelper.DBHelperParameter("po_total_record_cnt", GetType(Integer)),
                                New DBHelper.DBHelperParameter("po_queued_record_cnt", GetType(Integer)),
                                New DBHelper.DBHelperParameter("po_claim_list", GetType(DataSet)),
                                New DBHelper.DBHelperParameter("po_error_code", GetType(String), 255),
                                New DBHelper.DBHelperParameter("po_error_message", GetType(String), 255)}

        Dim ds As New DataSet

        Try
            DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, "ClaimList", true)

            If String.IsNullOrEmpty(outParameters(4).Value) Then
                If ds.Tables.Count > 0 then
                    ds.Tables(0).TableName = "ClaimList"
                End If
                
                batchId = outParameters(0).Value
                totalRecordCount = outParameters(1).Value
                totalRecordsInQueue = outParameters(2).Value
            Else
                batchId = guid.Empty
                totalRecordCount = 0
                totalRecordsInQueue = 0
                ErrorCode = outParameters(4).Value
                ErrorMessage = outParameters(5).Value
            End If
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function WS_SNMPORTAL_SA_CLAIMREPORT_NextPage(ByVal batchId As Guid,
                                                         ByVal languageId As Guid,
                                                         Byval pageSize As Integer,
                                                         ByRef totalRecordCount as Integer,
                                                         ByRef totalRecordsInQueue as Integer,
                                                         ByRef errorCode As String,
                                                         ByRef errorMessage As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/WS_SNMPORTAL_SA_CLAIMREPORT_GET_NEXT_PAGE")

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                New DBHelper.DBHelperParameter("pi_batch_id", batchId.ToByteArray()),
                                New DBHelper.DBHelperParameter("pi_page_size", pageSize),
                                New DBHelper.DBHelperParameter("pi_language_id", languageId.ToByteArray)}

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                New DBHelper.DBHelperParameter("po_total_record_cnt", GetType(Integer)),
                                New DBHelper.DBHelperParameter("po_queued_record_cnt", GetType(Integer)),
                                New DBHelper.DBHelperParameter("po_claim_list", GetType(DataSet)),
                                New DBHelper.DBHelperParameter("po_error_code", GetType(String), 255),
                                New DBHelper.DBHelperParameter("po_error_message", GetType(String), 255)}

        Dim ds As New DataSet

        Try
            DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, "ClaimList", true)

            If String.IsNullOrEmpty(outParameters(3).Value) Then
                If ds.Tables.Count > 0 then
                    ds.Tables(0).TableName = "ClaimList"
                End If
                
                totalRecordCount = outParameters(0).Value
                totalRecordsInQueue = outParameters(1).Value
            Else
                totalRecordCount = 0
                totalRecordsInQueue = 0
                ErrorCode = outParameters(3).Value
                ErrorMessage = outParameters(4).Value
            End If
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region

#Region "GVS"
    'This inserts GVS transaction
    Public Sub GVSTransactionCreation(pClaimID As Guid,
                                      pFunctionTypeCode As String,
                                      pCommentID As String)
        'Dim selectStmt As String = 

        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/GVS_TRANSACTION_CREATION"))
                cmd.AddParameter("pi_key_id", OracleDbType.Raw, pClaimID.ToByteArray())
                cmd.AddParameter("pi_function_type_code", OracleDbType.Char, pFunctionTypeCode)
                cmd.AddParameter("pi_followup_xml", OracleDbType.Varchar2, pCommentID)

                OracleDbHelper.ExecuteNonQuery(cmd)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region
#Region "Google Claims Service"
    Public Function GetTotalSvcWarrantyByCert(ByVal CertId As Guid, ByVal DealerId As Guid) As Integer
        Dim selectStmt As String = Me.Config("/SQL/GET_SVC_WARRANTY_COUNT_BY_CERTIFICATE")
        Dim returnValue As Object

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter("pi_cert_id", CertId.ToByteArray) _
                        , New DBHelper.DBHelperParameter("pi_dealer_id", DealerId.ToByteArray)}


        Try

            returnValue = DBHelper.ExecuteScalar(selectStmt, inParameters)
            If (returnValue Is Nothing) Then
                Return Nothing
            Else
                Return DirectCast(returnValue, Decimal)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Indix Service"
    Public Function GetIndixIdofRegisteredDevice(ByVal ClaimId As Guid) As String
        Dim selectStmt As String = Me.Config("/SQL/GET_REGISTERED_ITEM_INDIXID")
        Dim returnValue As Object

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter("claimId", ClaimId.ToByteArray)}


        Try

            returnValue = DBHelper.ExecuteScalar(selectStmt, inParameters)
            If (returnValue Is Nothing) Then
                Return Nothing
            Else
                Return DirectCast(returnValue, String)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    'REQ-6230
    Public Function GetCountryCodeOverwrite(ByVal CompanyId As Guid) As String
        Dim selectStmt As String = Me.Config("/SQL/GET_COUNTRY_CODE_OVERWRITE")
        Dim returnValue As Object

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(Me.PAR_NAME_IP_COMPANY_ID, CompanyId.ToByteArray)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                             New DBHelper.DBHelperParameter(Me.PAR_NAME_OP_COUNTRY_CODE, GetType(String), 2),
                             New DBHelper.DBHelperParameter(Me.PAR_NAME_OP_RETURN, GetType(Integer), 32),
                             New DBHelper.DBHelperParameter(Me.PAR_NAME_OP_EXCEPTION_MESSAGE, GetType(String), 500)}

        Try

            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inParameters, outputParameters)

            If CType(outputParameters(1).Value, Integer) <> 0 Then
                Dim e As New ApplicationException("Return Value = " & outputParameters(1).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
            Else
                Return outputParameters(0).Value.ToString
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region

End Class


