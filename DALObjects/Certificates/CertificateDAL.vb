Imports Assurant.ElitaPlus.DALObjects.DBHelper
Imports System.Object
Imports System.Guid

'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/12/2004)********************

Public Class CertificateDAL
    Inherits OracleDALBase



#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CERT"
    Public Const TABLE_KEY_NAME As String = "cert_id"
    Public Const TABLE_PREMIUM_TOTALS As String = "Premium_Totals"
    Public Const TABLE_SALES_TAX_DETAILS As String = "Sales_Taxes"
    Public Const TABLE_CANCEL_ID As String = "Cert_Cancel_Id"
    Public Const TABLE_CANCEL_REQUEST_ID As String = "Cert_Cancel_Request_Id"
    Public Const TABLE_CANCEL_DATE As String = "Cert_Cancel_Date"
    Public Const TABLE_CLAIMS As String = "Claims"
    Public Const TABLE_PROD_SS As String = "ProductCodeSS"
    Public Const TABLE_MAXLOSSDATE As String = "MaxLossDate"
    Public Const TABLE_CLAIMS_CANCEL_CERT As String = "ClaimsCancelCert"
    Public Const BANKINFO_TABLE_NAME As String = "elp_bank_info"


    Public Const COL_NAME_PAYMENT_ACT_DATE As String = "PaymentActDate"
    Public Const COL_NAME_MONTHSPASSED As String = "MonthsPassed"
    Public Const COL_NAME_USER_ID As String = "user_id" 'Amod Added
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_CERT_ID_GUID As String = "cert_id_guid"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const COL_NAME_SALES_CHANNEL As String = "sales_channel"

    Public Const COL_NAME_PAYMENT_TYPE_ID As String = "payment_type_id"
    Public Const COL_NAME_COMMISSION_BREAKDOWN_ID As String = "commission_breakdown_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_FINANCE_CURRENCY_ID As String = "finance_currency_id"
    Public Const COL_NAME_PURCHASE_CURRENCY_ID As String = "purchase_currency_id"
    Public Const COL_NAME_METHOD_OF_REPAIR_ID As String = "method_of_repair_id"
    Public Const COL_NAME_TYPE_OF_EQUIPMENT_ID As String = "type_of_equipment_id"
    Public Const COL_NAME_POST_PRE_PAID_ID As String = "post_pre_paid_id"
    Public Const COL_NAME_ADDRESS_ID As String = "address_id"
    Public Const COL_NAME_BANK_INFO_ID As String = "bank_info_id"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_STATUS_CODE As String = "status_code"
    Public Const COL_NAME_SUBSCRIBER_STATUS As String = "subscriber_status"
    Public Const COL_NAME_PRODUCT_SALES_DATE As String = "product_sales_date"
    Public Const COL_NAME_WARRANTY_SALES_DATE As String = "warranty_sales_date"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_IDENTIFICATION_NUMBER As String = "identification_number"
    Public Const COL_NAME_IDENTIFICATION_NUMBER_TYPE As String = "identification_number_type"
    Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const COL_NAME_HOME_PHONE As String = "home_phone"
    Public Const COL_NAME_WORK_PHONE As String = "work_phone"
    Public Const COL_NAME_EMAIL As String = "email"
    Public Const COL_NAME_DEALER_BRANCH_CODE As String = "dealer_branch_code"
    Public Const COL_NAME_SALES_REP_NUMBER As String = "sales_rep_number"
    Public Const COL_NAME_QUOTA_NUMBER As String = "quota_number"
    Public Const COL_NAME_MONTHLY_PAYMENTS As String = "monthly_payments"
    Public Const COL_NAME_FINANCED_AMOUNT As String = "financed_amount"
    Public Const COL_NAME_DEALER_ITEM As String = "dealer_item"
    'Public Const COL_NAME_DEALER_ITEM_DESCRIPTION As String = "dealer_item_description"
    Public Const COL_NAME_SALES_PRICE As String = "sales_price"
    Public Const COL_NAME_INTEREST_RATE As String = "interest_rate"
    Public Const COL_NAME_SUSPENDED_REASON_ID As String = "suspended_reason_id"
    'DEF-1476
    'Public Const COL_NAME_CREDIT_CARD_PROCESSING As String = "credit_card_processing"
    'Public Const COL_NAME_CREDIT_CARD_NUMBER As String = "credit_card_number"
    'Public Const COL_NAME_CREDIT_CARD_EXP_MMYY As String = "credit_card_exp_mmyy"
    'Public Const COL_NAME_CREDIT_AUTHORIZATION_NUMBER As String = "credit_authorization_number"
    'Public Const COL_NAME_CREDITCARD_TYPE_ID As String = "creditcard_type_id"
    'DEF-1476 END
    Public Const COL_NAME_CAMPAIGN_NUMBER As String = "campaign_number"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_DEALER_PRODUCT_CODE As String = "dealer_product_code"
    Public Const COL_NAME_VEHICLE_LICENSE_TAG As String = "vehicle_license_tag"
    Public Const COL_NAME_DATE_PAID_FOR As String = "date_paid_for"
    Public Const COL_NAME_DATE_PAID As String = "date_paid"
    Public Const COL_NAME_RETAILER As String = "retailer"
    Public Const COL_NAME_DEALER As String = "dealer"
    Public Const COL_NAME_VEHICLE_YEAR As String = "vehicle_year"
    Public Const COL_NAME_MODEL_ID As String = "model_id"
    Public Const COL_NAME_ODOMETER As String = "odometer"
    Public Const COL_NAME_CLASS_CODE_ID As String = "class_code_id"
    Public Const COL_NAME_MFG_DESCRIPTION As String = "mfg_description"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_MAILING_ADDRESS_ID As String = "mailing_address_id"
    Public Const COL_NAME_MEMBERSHIP_NUMBER As String = "membership_number"
    Public Const COL_NAME_PRIMARY_MEMBER_NAME As String = "primary_member_name"
    Public Const COL_NAME_MEMBERSHIP_TYPE_ID As String = "membership_type_id"
    Public Const COL_NAME_VAT_NUM As String = "vat_num"
    Public Const COL_NAME_SALES_REP_ID As String = "SalesRepID"
    Public Const COL_NAME_LOW_LIMIT As String = "low_limit"
    Public Const COL_NAME_HIGH_LIMIT As String = "high_limit"
    Public Const COL_NAME_SERVICE_LINE_NUMBER As String = "service_line_number"
    Public Const COL_NAME_REGION As String = "region"
    Public Const COL_NAME_BILLING_PLAN As String = "billing_plan"
    Public Const COL_NAME_BILLING_CYCLE As String = "billing_cycle"
    Public Const COL_NAME_OCCUPATION As String = "occupation"
    Public Const COL_NAME_POLITICALLY_EXPOSED_ID As String = "politically_exposed_id"
    Public Const COL_NAME_INCOME_RANGE_ID As String = "income_range_id"
    'Added for Req-703 - Start
    Public Const COL_NAME_MARKETING_PROMO_SER As String = "marketing_promo_ser"
    Public Const COL_NAME_MARKETING_PROMO_NUM As String = "marketing_promo_num"
    'Added for Req-703 - End

    Public Const COL_NAME_LINES_OF_ACCOUNT As String = "lines_on_account"
    Public Const COL_NAME_SUBSCRIBER_STATUS_CHANGE_DATE As String = "subscriber_status_change_date"

    Public Const COL_TOTAL_GROSS_AMT_RECEIVED As String = "total_gross_amt_received"
    Public Const COL_TOTAL_PREMIUM_WRITTEN As String = "Total_Premium_Written"
    Public Const COL_TOTAL_ORIGINAL_PREMIUM As String = "Total_Original_Premium"
    Public Const COL_TOTAL_LOSS_COST As String = "Total_Loss_cost"
    Public Const COL_TOTAL_COMISSION As String = "Total_Comission"
    Public Const COL_TOTAL_ADMIN_EXPENSE As String = "total_admin_expense"
    Public Const COL_TOTAL_MARKETING_EXPENSE As String = "total_marketing_expense"
    Public Const COL_TOTAL_OTHER As String = "total_other"
    Public Const COL_TOTAL_SALES_TAX As String = "total_sales_tax"
    Public Const COL_TOTAL_MTD_PAYMENTS As String = "total_mtd_payments"
    Public Const COL_TOTAL_YTD_PAYMENTS As String = "total_ytd_payments"
    Public Const COL_CERT_CANCELLATION_ID As String = "cert_cancellation_id"
    Public Const COL_OLD_NUMBER As String = "old_number"

    Public Const COL_NAME_PRODUCT_CODE_DESCRIPTION As String = "description"
    Public Const COL_NAME_SALUTATION_ID As String = "salutation_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"

    Public Const COL_INSURANCE_ACTIVATION_DATE As String = "insurance_activation_date"

    Public Const COL_NAME_DOCUMENT_AGENCY As String = "document_agency"
    Public Const COL_NAME_DOCUMENT_ISSUE_DATE As String = "document_issue_date"
    Public Const COL_NAME_RG_NUMBER As String = "rg_number"
    Public Const COL_NAME_RATING_PLAN As String = "rating_plan"
    Public Const COL_NAME_ID_TYPE As String = "id_type"
    Public Const COL_NAME_RETURN_REASON As String = "return_reason"
    Public Const COL_NAME_RETURN_CODE As String = "return_code"
    Public Const COL_NAME_RETURN_REJECT_MSG As String = "p_reject_msg_params"

    Public Const COL_NAME_DOCUMENT_TYPE_ID As String = "document_type_id"
    Public Const COL_NAME_COUNTRY_OF_PURCHASE_ID As String = "country_purchase_id"
    Public Const COL_NAME_CURRENCY_CERT_ID As String = "currency_cert_id"
    Public Const COL_NAME_PASSWORD As String = "password"

    Public Const COL_NAME_CLAIM_COUNT As String = "TotalClaim"
    Public Const COL_NAME_TOTAL_NUMBER_OF_CLAIMS As String = "TotalNumberOfClaims"
    Public Const COL_NAME_MAX_LOSS_DATE As String = "MaxLossDate"
    Public Const COL_NAME_DATE_OF_BIRTH As String = "birth_date"
    Public Const COL_NAME_USE_DEPRECIATION As String = "use_depreciation"

    Public Const COL_NAME_LINKED_CERT_NUMBER As String = "linked_cert_number"
    '
    Public Const PARAM_NAME_DOCUMENT_TYPE As String = "document_type"
    Public Const PARAM_NAME_VEHICLE_LICENSE_FLAG As String = "vehicle_license_tag"
    Public Const PARAM_NAME_CERT_NUMBER As String = "cert_number"
    Public Const PARAM_NAME_CUSTOMER_ID As String = "customer_id"
    Public Const PARAM_NAME_COMPANY_GROUP_ID As String = "company_group_id"

    Public Const DYNAMIC_ROW_NUMBER_PLACE_HOLDER As String = "--dynamic_row_number"

    Public Const COL_NAME_CUSTOMERINFO_LASTCHANGE_DATE As String = "customerinfo_lastchange_date"

    Public Const COL_NAME_COMPANY_ID2 As String = "company_id2"

    Public Const COL_NAME_FINANCE_TAB_AMOUNT As String = "financed_tab_amount"
    Public Const COL_NAME_FINANCE_TERM As String = "financed_term"
    Public Const COL_NAME_FINANCE_FREQUENCY As String = "financed_frequency"
    Public Const COL_NAME_FINANCE_INSTALLMENT_NUMBER As String = "financed_installment_number"
    Public Const COL_NAME_FINANCE_INSTALLMENT_AMOUNT As String = "financed_installment_amount"
    Public Const COL_NAME_PENALTY_FEE As String = "penalty_fee"
    Public Const COL_NAME_PROD_LIABILITY_POLICY_CD As String = "prod_liability_limit_policy_cd"
    Public Const COL_VIN_LOCATOR As String = "vin_locator"

    Public Const COL_NAME_FINANCIAL_COMPUTATION_METHOD As String = "fin_computation_method"

    Public Const COL_NAME_FINANCE_DATE As String = "finance_date"
    Public Const COL_NAME_DOWN_PAYMENT As String = "down_payment"
    Public Const COL_NAME_ADVANCE_PAYMENT As String = "advance_payment"
    Public Const COL_NAME_BILLING_ACCOUNT_NUMBER As String = "billing_account_number"

    Public Const COL_NAME_REINSURANCE_STATUS_ID As String = "reinsurance_status_id"
    Public Const COL_NAME_REINSURANCE_REJECT_REASON As String = "reinsurance_reject_reason"
    Public Const COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS As String = "num_of_consecutive_payments"
    Public Const COL_NAME_UPGRADE_FIXED_TERM As String = "upgrade_fixed_term"
    Public Const COL_NAME_UPGRADE_TERM_UOM_ID As String = "upgrade_term_uom_id"
    Public Const COL_NAME_UPGRADE_TERM_FROM As String = "upgrade_term_from"
    Public Const COL_NAME_UPGRADE_TERM_TO As String = "upgrade_term_to"
    Public Const COL_NAME_UPGRADE_PROGRAM As String = "upgrade_program"
    Public Const COL_NAME_PROD_UPGRADE_TERM_UOM = "prod_upgrade_term_uom"
    Public Const COL_NAME_LOAN_CODE = "loan_code"
    Public Const COL_NAME_PAYMENT_SHIFT_NUMBER = "payment_shift_number"
    'Public Const COL_NAME_DEALER_REWARD_POINTS As String = "dealer_reward_points"

    Public Const COL_NAME_DEALER_CURRENT_PLAN_CODE As String = "dealer_current_plan_code"
    Public Const COL_NAME_DEALER_SCHEDULED_PLAN_CODE As String = "dealer_scheduled_plan_code"
    Public Const COL_NAME_DEALER_REWARD_POINTS As String = "dealer_reward_points"
    Public Const COL_NAME_OUTSTANDING_BALANCE_AMOUNT = "outstanding_balance_amount"
    Public Const COL_NAME_OUTSTANDING_BALANCE_DUE_DATE = "outstanding_balance_due_date"
    Public Const COL_NAME_CERTIFICATE_SIGNED = "certificate_signed"
    Public Const COL_NAME_SEPA_MANDATE_SIGNED = "sepa_mandate_signed"
    Public Const COL_NAME_CHECK_SIGNED = "check_signed"
    Public Const COL_NAME_CHECK_VERIFICATION_DATE = "check_verification_date"
    Public Const COL_NAME_SERVICE_ID = "service_id"
    Public Const COL_NAME_SERVICE_START_DATE = "service_start_date"
    Public Const COL_NAME_CONTRACT_CHECK_COMPLETE_DATE = "contract_check_complete_date"
    Public Const COL_NAME_CERTIFICATE_VERIFICATION_DATE = "certificate_verification_date"
    Public Const COL_NAME_SEPA_MANDATE_DATE = "sepa_mandate_date"
    Public Const COL_NAME_CONTRACT_CHECK_COMPLETE = "contract_check_complete"

    Public Const COL_NAME_IS_CHILD_CERTIFICATE = "is_child_certificate"
    Public Const COL_NAME_IS_PARENT_CERTIFICATE = "is_parent_certificate"

    Public Const COL_NAME_DEALER_UPDATE_REASON = "dealer_update_reason"
    Public Const COL_NAME_BILLING_DOCUMENT_TYPE = "billing_document_type"
    Public Const COL_NAME_PREMIUM_AMOUNT = "premium_amount"
    Public Const COL_NAME_APPLECARE_FEE = "applecare_fee"
    Public Const COL_NAME_CUST_REQ_CANCEL_DATE = "cust_req_cancel_date"
    Public Const COL_NAME_CUST_CANCEL_DATE = "cust_cancel_date"

    Public Const COL_NAME_INSURANCE_ORDER_NUMBER = "insurance_order_number"
    Public Const COL_NAME_DEVICE_ORDER_NUMBER = "device_order_number"
    Public Const COL_NAME_UPGRADE_TYPE = "upgrade_type"
    Public Const COL_NAME_FULFILLMENT_CONSENT_ACTION = "fulfillment_consent_action"
    Public Const COL_NAME_PLAN_TYPE = "plan_type"

    Public Const COL_NAME_WAITING_PERIOD_END_DATE As String = "Waiting_Period_End_Date"

    Public Const COL_NAME_PREVIOUS_CERT_ID As String = "Previous_Cert_Id"
    Public Const COL_NAME_ORIGINAL_CERT_ID As String = "Original_Cert_Id"

    'Public Const COL_NAME_mfg_begin_date As String = "mfg_begin_date"
    'Public Const COL_NAME_mfg_end_date As String = "mfg_end_date"
    'Public Const COL_NAME_mfg_begin_km As String = "mfg_begin_km"
    'Public Const COL_NAME_mfg_end_km As String = "mfg_end_km"

    Public Const WILDCARD As Char = "%"
    Public Const NO_DEALER_SELECTED = "--"

    'Sort By Default
    Public Const SORT_BY_CUSTOMER_NAME As String = "CUSTOMER_NAME"
    Public Const SORT_BY_CERT_NUMBER As String = "cert_number"
    Public Const SORT_BY_PHONE_NUMBER As String = "HOME_PHONE"

    Public Const TOTAL_PARAM_IN = 1 '2
    Public Const TOTAL_PARAM_OUT = 2 '1
    Public Const IN_DOC_TYPE = 0
    Public Const IN_ID_NUMBER = 1

    Public Const OUT_REJ_REASON = 0
    Public Const OUT_REJ_CODE = 1
    Public Const OUT_REJ_MSG = 2



    'WS 
    Public Const WS_SORT_BY_WARRANTY_SALES_DATE As Integer = 1
    Public Const WS_SORT_BY_CERTIFICATE_NUMBER As Integer = 2
    Public Const WS_SORT_BY_CUSTOMER_NAME As Integer = 3
    Public Const WS_SORT_BY_MFG_DESCRIPTION As Integer = 4
    Public Const WS_SORT_BY_MODEL As Integer = 5
    Public Const WS_SORT_BY_SERIAL_NUMBER As Integer = 6
    Public Const WS_SORT_BY_SALES_REP_ID As Integer = 7
    Public Const WS_SORT_BY_EMAIL As Integer = 8


    Public Const WS_SORT_ORDER_ASCENDING As Integer = 1
    Public Const WS_SORT_ORDER_DESCENDING As Integer = 2

    Public Const TOTAL_INPUT_PARAM_WS As Integer = 8
    Public Const TOTAL_OUTPUT_PARAM_WS As Integer = 3
    Public Const TOTAL_OUTPUT_PARAM_WS_1 As Integer = 5

    Public Const SP_PARAM_NAME__CUSTOMER_IDENTIFIER As String = "p_customer_identifier"
    Public Const SP_PARAM_NAME__IDENTIFIER_TYPE As String = "p_identifier_type"
    Public Const SP_PARAM_NAME__SYSTEM_USER_ID As String = "p_system_user_id"
    Public Const SP_PARAM_NAME__DEALER_ID As String = "p_dealer_id"
    Public Const SP_PARAM_NAME__RETURN As String = "p_return"
    Public Const SP_PARAM_NAME__EXCEPTION_MSG As String = "p_exception_msg"
    Public Const SP_PARAM_NAME__CUSTOMER_FUNCTIONS As String = "p_customer_functions"
    Public Const SP_PARAM_NAME__COVERAGE_INFO As String = "p_coverage_Info"
    Public Const SP_PARAM_NAME__RESPONSE_STATUS As String = "p_response_status"
    Public Const SP_PARAM_NAME__CERT_ID As String = "p_cert_id"
    Public Const SP_PARAM_NAME__CAN_SUBMIT_CLAIM As String = "p_can_submit_claim"

    Public Const P_RETURN = 0
    Public Const P_EXCEPTION_MSG = 1
    Public Const P_CURSOR_CUSTOMER_FUNCTIONS = 2
    Public Const P_CURSOR_RESPONSE_STATUS = 3
    Public Const P_CERT_ID = 4
    Public Const P_CAN_SUBMIT_CLAIM = 5

    Public Const DATASET_NAME__CLAIM_CHECK_RESPONSE As String = "ClaimCheckResponse"
    Private Const TABLE_NAME__GET_CUSTOMER_FUNCTIONS_RESPONSE As String = "GetCustomerFunctionsResponse"
    Private Const TABLE_NAME__GET_COVERAGE_INFO_RESPONSE As String = "GetCoverageInfoResponse"
    Private Const TABLE_NAME__RESPONSE_STATUS As String = "ResponseStatus"

    Public Const INFORCECERTS_DATE As String = "InforceCerts"

    Public Const DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER_2 As String = "--dynamic_where_clause2"

    Public Const COL_NAME_CERT_CREATED_DATE As String = "created_date"

    'REQ-1255 - START
    Public Const COL_NAME_MARITALSTATUS As String = "MaritalStatusId"
    Public Const COL_NAME_NATIONALITY As String = "NationalityId"
    Public Const COL_NAME_PLACEOFBIRTH As String = "PlaceOfBirthId"
    Public Const COL_NAME_CITYOFBIRTH As String = "CityOfBirth"
    Public Const COL_NAME_GENDER As String = "GenderId"
    Public Const COL_NAME_CUIT_CUIL As String = "CUIT_CUIL"
    'REQ-1255 - END

    Public Const COL_NAME_PERSON_TYPE_ID As String = "person_type_id"

    Public Const COL_NAME_NEW_USED As String = "new_used"
    Public Const COL_NAME_REMAINING_BALANCE As String = "remaining_balance"
    Public Const GALAXY_MAX_NUMBER_OF_ROWS As Integer = 100

    Public Const COL_PRODUCT_TOTAL_PAID_AMOUNT As String = "Product_Total_Paid_Amount"
    Public Const COL_PRODUCT_REMAIN_LIABILITY_LIMIT As String = "Product_Remain_Liability_Limit"
    Public Const COL_PRODUCT_LIABILITY_LIMIT As String = "prod_liability_limit"

    'REQ-5612
    Public Const TOTAL_INPUT_PARAM_CERT_HISTORY As Integer = 0
    Public Const PO_CURSOR_CERT_HISTORY = 0
    Public Const SP_OUT_PARAM_NAME__RETURN As String = "po_return"
    Public Const SP_OUT_PARAM_NAME__EXCEPTION_MSG As String = "po_exception_msg"
    Public Const SP_PARAM_NAME__CERT_HISTORY As String = "po_cert_history"
    Private Const TABLE_NAME__GET_CERT_HISTORY_INFO_RESPONSE As String = "GetCoverageInfoResponse"

    Public Const SP_PARAM_NAME__INSTALLMENT_HISTORY As String = "po_cert_installment_hist"

    'REQ 5525
    Public Const PAR_I_NAME_BILLING_ACCOUNT_NUMBER As String = "pi_Billing_Account_Number"
    Public Const PAR_I_NAME_CERTIFICATE_NUMBER As String = "pi_Cert_Number"
    Public Const PAR_I_COL_NAME_DEALER As String = "pi_Dealer"

    Public Const PO_CURSOR_CERT_INFO As Integer = 0
    Public Const PO_CURSOR_CERT_PHONE_SEARCH As Integer = 0
    Public Const SP_PARAM_NAME_CERT_INFO As String = "po_cert_info"

    'REQ 5932
    Public Const PO_CURSOR_CUSTOMER_INFO As Integer = 0
    Public Const PO_CURSOR_CUSTOMERBANK_INFO As Integer = 0
    Public Const PO_CURSOR_SERIAL_NUMBER As Integer = 0
    Public Const SP_PARAM_NAME_CUST_INFO As String = "po_customer_info"
    Public Const PO_CURSOR_CUSTOMER_DETAILS As Integer = 0
    Public Const SP_PARAM_NAME_CUST_DETAILS As String = "po_customer_details"
    Public Const COL_NAME_CUSTOMER_ID As String = "customer_id"
    Public Const COL_NAME_CUSTOMER_FIRST_NAME As String = "customer_first_Name"
    Public Const COL_NAME_CUSTOMER_MIDDLE_NAME As String = "customer_middle_Name"
    Public Const COL_NAME_CUSTOMER_LAST_NAME As String = "customer_last_Name"
    Public Const COL_NAME_ALTERNATIVE_LAST_NAME As String = "customer_alt_last_name"
    Public Const COL_NAME_ALTERNATIVE_FIRST_NAME As String = "customer_alt_first_name"
    Public Const COL_NAME_CORPORATE_NAME As String = "corporate_name"
    Public Const PO_CURSOR_UPDATE_CUSTOMER As Integer = 1
    Public Const PAR_I_NAME_CERT_ID As String = "pi_cert_id"
    Public Const PAR_I_NAME_COMPANY_ID As String = "pi_company_id"
    Public Const PAR_I_NAME_DEALER_ID As String = "pi_dealer_id"
    Public Const PAR_I_NAME_CERT_NUMBER As String = "pi_cert_number"
    Public Const PAR_I_NAME_PAYMENT_TYPE_ID As String = "pi_payment_type_id"
    Public Const PAR_I_NAME_COMMISSION_BREAKDOWN_ID As String = "pi_commission_breakdown_id"
    Public Const PAR_I_NAME_FINANCE_CURRENCY_ID As String = "pi_finance_currency_id"
    Public Const PAR_I_NAME_PURCHASE_CURRENCY_ID As String = "pi_purchase_currency_id"
    Public Const PAR_I_NAME_METHOD_REPAIR_ID As String = "pi_method_of_repair_id"
    Public Const PAR_I_NAME_TYPE_OF_EQUIPMENT_ID As String = "pi_type_of_equipment_id"
    Public Const PAR_I_NAME_ADDRESS_ID As String = "pi_address_id"
    Public Const PAR_I_NAME_PRODUCT_CODE As String = "pi_product_code"
    Public Const PAR_I_NAME_STATUS_CODE As String = "pi_status_code"
    Public Const PAR_I_NAME_PRODUCT_SALES_DATE As String = "pi_product_sales_date"
    Public Const PAR_I_NAME_WARRANTY_SALES_DATE As String = "pi_warranty_sales_date"
    Public Const PAR_I_NAME_INVOICE_NUMBER As String = "pi_invoice_number"
    Public Const PAR_I_NAME_IDENTIFICATION_NUMBER As String = "pi_identification_number"
    Public Const PAR_I_NAME_CUSTOMER_NAME As String = "pi_customer_name"
    Public Const PAR_I_NAME_HOME_PHONE As String = "pi_home_phone"
    Public Const PAR_I_NAME_WORK_PHONE As String = "pi_work_phone"
    Public Const PAR_I_NAME_EMAIL As String = "pi_email"
    Public Const PAR_I_NAME_DEALER_BRANCH_CODE As String = "pi_dealer_branch_code"
    Public Const PAR_I_NAME_SALES_REP_NUMBER As String = "pi_sales_rep_number"
    Public Const PAR_I_NAME_QUOTA_NUMBER As String = "pi_quota_number"
    Public Const PAR_I_NAME_MONTLY_PAYMENTS As String = "pi_monthly_payments"
    Public Const PAR_I_NAME_FINANCED_AMOUNT As String = "pi_financed_amount"
    Public Const PAR_I_NAME_DEALER_ITEM As String = "pi_dealer_item"
    Public Const PAR_I_NAME_SALES_PRICE As String = "pi_sales_price"
    Public Const PAR_I_NAME_INTEREST_RATE As String = "pi_interest_rate"
    Public Const PAR_I_NAME_CAMPAIGN_NUMBER As String = "pi_campaign_number"
    Public Const PAR_I_NAME_SOURCE As String = "pi_source"
    Public Const PAR_I_NAME_DEALER_PRODUCT_CODE As String = "pi_dealer_product_code"
    Public Const PAR_I_NAME_DATE_PAID_FOR As String = "pi_date_paid_for"
    Public Const PAR_I_NAME_DATE_PAID As String = "pi_date_paid"
    Public Const PAR_I_NAME_RETAILER As String = "pi_retailer"
    Public Const PAR_I_NAME_SALUTATION_ID As String = "pi_salutation_id"
    Public Const PAR_I_NAME_OLD_NUMBER As String = "pi_old_number"
    Public Const PAR_I_NAME_INSURANCE_ACTIVATION_DATE As String = "pi_insurance_activation_date"
    Public Const PAR_I_NAME_DOCUMENT_AGENCY As String = "pi_document_agency"
    Public Const PAR_I_NAME_DOCUMENT_ISSUE_DATE As String = "pi_document_issue_date"
    Public Const PAR_I_NAME_RG_NUMBER As String = "pi_rg_number"
    Public Const PAR_I_NAME_RATING_PLAN As String = "pi_rating_plan"
    Public Const PAR_I_NAME_ID_TYPE As String = "pi_id_type"
    Public Const PAR_I_NAME_DOCUMENT_TYPE_ID As String = "pi_document_type_id"
    Public Const PAR_I_NAME_MODIFIED_BY As String = "pi_modified_by"
    Public Const PAR_I_NAME_COUNTRY_PURCHASE_ID As String = "pi_country_purchase_id"
    Public Const PAR_I_NAME_PASSWORD As String = "pi_password"
    Public Const PAR_I_NAME_CURRENCY_CERT_ID As String = "pi_currency_cert_id"
    Public Const PAR_I_NAME_VEHICLE_LICENSE_TAG As String = "pi_vehicle_license_tag"
    Public Const PAR_I_NAME_BIRTH_DATE As String = "pi_birth_date"
    Public Const PAR_I_NAME_MAILING_ADDRESS_ID As String = "pi_mailing_address_id"
    Public Const PAR_I_NAME_MEMBERSHIP_NUMBER As String = "pi_membership_number"
    Public Const PAR_I_NAME_PRIMARY_MEMBER_NAME As String = "pi_primary_member_name"
    Public Const PAR_I_NAME_VAT_NUM As String = "pi_vat_num"
    Public Const PAR_I_NAME_MEMBERSHIP_TYPE_ID As String = "pi_membership_type_id"
    Public Const PAR_I_NAME_LANGUAGE_ID As String = "pi_language_id"
    Public Const PAR_I_NAME_POST_PRE_PAID_ID As String = "pi_post_pre_paid_id"
    Public Const PAR_I_NAME_REGION As String = "pi_region"
    Public Const PAR_I_NAME_OCCUPATION As String = "pi_occupation"
    Public Const PAR_I_NAME_POLITICALLY_EXPOSED_ID As String = "pi_politically_exposed_id"
    Public Const PAR_I_NAME_NATIONALITYID As String = "pi_NationalityId"
    Public Const PAR_I_NAME_PLACEOFBIRTHID As String = "pi_PlaceOfBirthId"
    Public Const PAR_I_NAME_GENDERID As String = "pi_GenderId"
    Public Const PAR_I_NAME_CUIT_CUIL As String = "pi_Cuit_Cuil"
    Public Const PAR_I_NAME_LINKED_CERT_NUMBER As String = "pi_linked_cert_number"
    Public Const PAR_I_NAME_CUSTINFO_LASTCHANGE_DATE As String = "pi_custinfo_lastchange_date"
    Public Const PAR_I_NAME_SUBSCRIBER_STATUS As String = "pi_subscriber_status"
    Public Const PAR_I_NAME_SUBSCRIBER_STATUS_CHANGE_DT As String = "pi_subscriber_status_change_dt"
    Public Const PAR_I_NAME_NEW_USED As String = "pi_new_used"
    Public Const PAR_I_NAME_MARITALSTATUSID As String = "pi_MaritalStatusId"
    Public Const PAR_I_NAME_VIN_LOCATOR As String = "pi_vin_locator"
    Public Const PAR_I_NAME_PERSON_TYPE_ID As String = "pi_person_type_id"
    Public Const PAR_I_NAME_NUM_OF_CONSECUTIVE_PAYMENTS As String = "pi_num_of_consecutive_payments"
    Public Const PAR_I_NAME_UPGRADE_TERM_UOM_ID As String = "pi_upgrade_term_uom_id"
    Public Const PAR_I_NAME_UPGRADE_TERM_FROM As String = "pi_upgrade_term_from"
    Public Const PAR_I_NAME_UPGRADE_TERM_TO As String = "pi_upgrade_term_to"
    Public Const PAR_I_NAME_UPGRADE_FIXED_TERM As String = "pi_upgrade_fixed_term"
    Public Const PAR_I_NAME_CERTIFICATE_SIGNED As String = "pi_certificate_signed"
    Public Const PAR_I_NAME_SEPA_MANDATE_SIGNED As String = "pi_sepa_mandate_signed"
    Public Const PAR_I_NAME_CONTRACT_CHECK_COMPLETE_DATE As String = "pi_contract_check_complete_date"
    Public Const PAR_I_NAME_CERT_VERIFICATION_DATE As String = "pi_cert_verification_date"
    Public Const PAR_I_NAME_SEPA_MANDATE_DATE As String = "pi_sepa_mandate_date"
    Public Const PAR_I_NAME_CONTRACT_CHECK_COMPLETE As String = "pi_contract_check_complete"
    Public Const PAR_I_NAME_CHECK_SIGNED As String = "pi_check_signed"
    Public Const PAR_I_NAME_CHECK_VERIFICATION_DATE As String = "pi_check_verification_date_date"
    Public Const PAR_I_NAME_IS_RESTRICTED As String = "pi_is_restricted"
    Public Const COL_IS_RESTRICTED As String = "is_restricted"
    Public Const PAR_I_NAME_SERVICE_ID As String = "pi_service_id"
    Public Const PAR_I_NAME_SERVICE_START_DATE As String = "pi_service_start_date"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_cert_id", id.ToByteArray)}
        Dim outputParameter(Me.PO_CURSOR_CERT_INFO) As DBHelper.DBHelperParameter
        outputParameter(Me.PO_CURSOR_CERT_INFO) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME_CERT_INFO, GetType(DataSet))
        Try

            DBHelper.FetchSp(selectStmt, parameters, outputParameter, familyDS, Me.TABLE_NAME)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Private Function IsThereALikeClause(ByVal certNumberMask As String, ByVal customerNameMask As String, ByVal CustomerPhoneMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = Me.IsLikeClause(certNumberMask) OrElse Me.IsLikeClause(customerNameMask) OrElse
                            Me.IsLikeClause(CustomerPhoneMask)
        Return bIsLikeClause
    End Function

    Private Function IsThereALikeClause(ByVal certNumberMask As String, ByVal customerNameMask As String, ByVal CustomerPhoneMask As String, ByVal EmailMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = Me.IsLikeClause(certNumberMask) OrElse Me.IsLikeClause(customerNameMask) OrElse
                            Me.IsLikeClause(CustomerPhoneMask) OrElse Me.IsLikeClause(EmailMask)
        Return bIsLikeClause
    End Function

    Private Function IsThereALikeClause(ByVal certNumberMask As String, ByVal customerNameMask As String,
                                ByVal addressMask As String, ByVal postalCodeMask As String,
                                ByVal taxIdMask As String, ByVal dealerNameMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = Me.IsLikeClause(certNumberMask) OrElse Me.IsLikeClause(customerNameMask) OrElse
                            Me.IsLikeClause(addressMask) OrElse Me.IsLikeClause(postalCodeMask) OrElse
                            Me.IsLikeClause(taxIdMask) OrElse Me.IsLikeClause(dealerNameMask)
        Return bIsLikeClause
    End Function

    Private Function IsThereALikeClause(ByVal certNumberMask As String, ByVal dealerCodeMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = Me.IsLikeClause(certNumberMask) OrElse Me.IsLikeClause(dealerCodeMask)
        Return bIsLikeClause
    End Function

    Public Function LoadList(ByVal certNumberMask As String, ByVal customerNameMask As String,
                                ByVal addressMask As String, ByVal postalCodeMask As String,
                                ByVal taxIdMask As String, ByVal dealerNameMask As String,
                                ByVal compIds As ArrayList, ByVal sortBy As String, ByVal LimitResultset As Int32,
                                Optional ByVal accountNum As String = "",
                                Optional ByVal certStatus As String = "",
                                Optional ByVal invoiceNum As String = "",
                                Optional ByVal dealerGroupCode As String = "") As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet
        Dim companyParam As DBHelper.DBHelperParameter
        Dim rowNumParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(certNumberMask, customerNameMask,
                                addressMask, postalCodeMask,
                                taxIdMask, dealerNameMask)
        If ((Not (certNumberMask Is Nothing)) AndAlso (Me.FormatSearchMask(certNumberMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.CERT_NUMBER)" & certNumberMask.ToUpper & " AND"
        End If

        If ((Not ((dealerNameMask Is Nothing) OrElse (dealerNameMask = NO_DEALER_SELECTED))) AndAlso (Me.FormatSearchMask(dealerNameMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(D.dealer)" & dealerNameMask.ToUpper & " AND "
        End If

        If ((Not (customerNameMask Is Nothing)) AndAlso (Me.FormatSearchMask(customerNameMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(CUSTOMER_NAME) " & customerNameMask.ToUpper & " AND "
        End If

        If ((Not (taxIdMask Is Nothing)) AndAlso (Me.FormatSearchMask(taxIdMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(IDENTIFICATION_NUMBER) " & taxIdMask.ToUpper & " AND "
        End If

        If ((Not (addressMask Is Nothing)) AndAlso (Me.FormatSearchMask(addressMask))) Then
            whereClauseConditions &= Environment.NewLine & " C.ADDRESS_ID IN (SELECT ADDRESS_ID FROM ELP_ADDRESS WHERE UPPER(ADDRESS1) " & addressMask.ToUpper & ") AND "
        End If

        If ((Not (postalCodeMask Is Nothing)) AndAlso (Me.FormatSearchMask(postalCodeMask))) Then
            whereClauseConditions &= Environment.NewLine & " C.ADDRESS_ID IN (SELECT ADDRESS_ID FROM ELP_ADDRESS WHERE UPPER(POSTAL_CODE) " & postalCodeMask.ToUpper & ") AND "
        End If

        If (certStatus <> String.Empty AndAlso (Me.FormatSearchMask(certStatus))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.STATUS_CODE) " & certStatus.ToUpper & " AND "
        End If

        If (accountNum <> String.Empty AndAlso (Me.FormatSearchMask(accountNum))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.MEMBERSHIP_NUMBER) " & accountNum.ToUpper & " AND "
        End If

        'Added for Req-801
        If (invoiceNum <> String.Empty AndAlso (Me.FormatSearchMask(invoiceNum))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.INVOICE_NUMBER) " & invoiceNum.ToUpper & " AND "
        End If

        If (dealerGroupCode <> String.Empty AndAlso (Me.FormatSearchMask(dealerGroupCode))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(dg.code " & dealerGroupCode.ToUpper & " AND "
        End If

        If bIsLikeClause = True Then
            ' hextoraw
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COMPANY_ID, compIds, True) & " AND "
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("d." & Me.COL_NAME_COMPANY_ID, compIds, True)
        Else
            ' not HextoRaw
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COMPANY_ID, compIds, True) & " AND "
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("d." & Me.COL_NAME_COMPANY_ID, compIds, True)
        End If

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
            rowNumParam = New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, LimitResultset)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNumParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadCombinedList(ByVal PhoneTypeMask As String,
                                 ByVal PhoneNumMask As String,
                                 ByVal certNumberMask As String,
                                 ByVal customerNameMask As String,
                                 ByVal addressMask As String,
                                 ByVal postalCodeMask As String,
                                 ByVal dealerNameMask As String,
                                 ByVal certStatus As String,
                                 ByVal taxIdMask As String,
                                 ByVal invoiceNumberMask As String,
                                 ByVal accountNumberMask As String,
                                 ByVal serialNumberMask As String,
                                 ByVal isVSCSearch As Boolean,
                                 ByVal compIds As ArrayList,
                                 ByVal sortBy As String,
                                 ByVal LimitResultset As Int32,
                                 ByVal inforceDate As String,
                                 ByVal networkId As String,
                                 Optional ByVal vehicleLicenseFlagMask As String = "",
                                 Optional ByVal Service_Line_Number As String = "",
                                 Optional ByVal dealerGroupCode As String = "") As DataSet

        Dim selectStmt As String
        If isVSCSearch Then
            selectStmt = Me.Config("/SQL/COMBINED_VSC_LOAD_LIST")
        Else
            selectStmt = Me.Config("/SQL/COMBINED_LOAD_LIST")
        End If

        Dim whereClauseConditions As String = ""
        Dim fromClauseConditions As String = ""
        Dim joinCondition As String = ""
        Dim ds As New DataSet
        Dim companyParam As DBHelper.DBHelperParameter
        Dim rowNumParam As DBHelper.DBHelperParameter

        Dim bIsLikeClause As Boolean = IsLikeClause(PhoneNumMask) OrElse
                                        IsLikeClause(PhoneTypeMask) OrElse
                                        IsLikeClause(certNumberMask) OrElse
                                        IsLikeClause(customerNameMask) OrElse
                                        IsLikeClause(addressMask) OrElse
                                        IsLikeClause(postalCodeMask) OrElse
                                        IsLikeClause(dealerNameMask) OrElse
                                        IsLikeClause(certStatus) OrElse
                                        IsLikeClause(taxIdMask) OrElse
                                        IsLikeClause(invoiceNumberMask) OrElse
                                        IsLikeClause(accountNumberMask) OrElse
                                        IsLikeClause(serialNumberMask)


        If ((Not (certNumberMask Is Nothing)) AndAlso (Me.FormatSearchMask(certNumberMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.CERT_NUMBER)" & certNumberMask.ToUpper & " AND"
        End If

        If ((Not ((dealerNameMask Is Nothing) OrElse (dealerNameMask = NO_DEALER_SELECTED))) AndAlso (Me.FormatSearchMask(dealerNameMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(D.dealer)" & dealerNameMask.ToUpper & " AND "
        End If

        If ((Not (customerNameMask Is Nothing)) AndAlso (Me.FormatSearchMask(customerNameMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(CUSTOMER_NAME) " & customerNameMask.ToUpper & " AND "
        End If

        If ((Not (taxIdMask Is Nothing)) AndAlso (Me.FormatSearchMask(taxIdMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(IDENTIFICATION_NUMBER) " & taxIdMask.ToUpper & " AND "
        End If

        If ((Not (addressMask Is Nothing)) AndAlso (Me.FormatSearchMask(addressMask))) Then
            whereClauseConditions &= Environment.NewLine & " C.ADDRESS_ID IN (SELECT ADDRESS_ID FROM ELP_ADDRESS WHERE UPPER(ADDRESS1) " & addressMask.ToUpper & ") AND "
        End If

        If ((Not (postalCodeMask Is Nothing)) AndAlso (Me.FormatSearchMask(postalCodeMask))) Then
            whereClauseConditions &= Environment.NewLine & " C.ADDRESS_ID IN (SELECT ADDRESS_ID FROM ELP_ADDRESS WHERE UPPER(POSTAL_CODE) " & postalCodeMask.ToUpper & ") AND "
        End If

        If (certStatus <> String.Empty AndAlso (Me.FormatSearchMask(certStatus))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.STATUS_CODE) " & certStatus.ToUpper & " AND "
        End If

        If (accountNumberMask <> String.Empty AndAlso (Me.FormatSearchMask(accountNumberMask))) Then
            whereClauseConditions &= Environment.NewLine & "C.MEMBERSHIP_NUMBER " & accountNumberMask & " AND "
        End If

        'Added for Req-801
        If (invoiceNumberMask <> String.Empty AndAlso (Me.FormatSearchMask(invoiceNumberMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.INVOICE_NUMBER) " & invoiceNumberMask.ToUpper & " AND "
        End If

        If (Service_Line_Number <> String.Empty AndAlso (Me.FormatSearchMask(Service_Line_Number))) Then
            whereClauseConditions &= Environment.NewLine & "C.Service_Line_Number " & Service_Line_Number.ToUpper & " AND "
        End If

        'Modified for Req-610
        If ((Not (PhoneNumMask.Equals(String.Empty))) AndAlso (Not (PhoneNumMask Is Nothing)) AndAlso (Me.FormatSearchMask(PhoneNumMask))) Then
            If (PhoneTypeMask = "HM") Then
                whereClauseConditions &= Environment.NewLine & "c.HOME_PHONE " & PhoneNumMask & " AND "
            ElseIf (PhoneTypeMask = "WC") Then
                whereClauseConditions &= Environment.NewLine & "c.WORK_PHONE " & PhoneNumMask & " AND "
            End If
        End If

        If (dealerGroupCode <> String.Empty AndAlso (Me.FormatSearchMask(dealerGroupCode))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(dg.code) " & dealerGroupCode.ToUpper & " AND "
        End If

        If isVSCSearch Then
            ' FOR VSC SEARCH USE THE FOLLOWINGS
            If ((Not (serialNumberMask Is Nothing)) AndAlso (Me.FormatSearchMask(serialNumberMask)) AndAlso (Not (serialNumberMask.Equals(String.Empty)))) Then
                whereClauseConditions &= Environment.NewLine & "(ci.serial_number" & serialNumberMask.ToUpper & " or "
                whereClauseConditions &= Environment.NewLine & "ci.imei_number" & serialNumberMask.ToUpper & " ) AND "
            End If

            If ((Not (vehicleLicenseFlagMask Is Nothing)) AndAlso (Not (vehicleLicenseFlagMask.Equals(String.Empty)))) Then
                whereClauseConditions &= Environment.NewLine & "c.vehicle_license_tag is not null" & " AND"

                If ((Me.FormatSearchMask(vehicleLicenseFlagMask))) Then
                    whereClauseConditions &= Environment.NewLine & "upper(c.vehicle_license_tag)" & vehicleLicenseFlagMask.ToUpper & " AND "
                End If
            End If

            If bIsLikeClause = True Then
                ' hextoraw
                whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("ci." & Me.COL_NAME_COMPANY_ID, compIds, True)
            Else
                ' not HextoRaw
                whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("ci." & Me.COL_NAME_COMPANY_ID, compIds, False)
            End If

        Else
            If bIsLikeClause = True Then
                whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("d." & Me.COL_NAME_COMPANY_ID, compIds, True)
            Else
                whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("d." & Me.COL_NAME_COMPANY_ID, compIds, True)
            End If
        End If

        If Not inforceDate.Equals(String.Empty) Then

            Dim varInforceDate As Date

            'Date.TryParse(inforceDate, _
            '                System.Threading.Thread.CurrentThread.CurrentCulture, _
            '                System.Globalization.DateTimeStyles.NoCurrentDateDefault, varInforceDate)

            varInforceDate = DateHelper.GetDateValue(inforceDate)

            joinCondition &= Environment.NewLine & " join elp_cert_item_coverage cic on c.cert_id = cic.cert_id"

            whereClauseConditions &= Environment.NewLine & " and cic.begin_Date <= to_date('" & varInforceDate.ToString("MM/dd/yyyy") & "','MM/DD/YYYY') " _
                                                           & " and cic.end_date >= to_date('" & varInforceDate.ToString("MM/dd/yyyy") & "','MM/DD/YYYY') " _
                                                           & " and getcodefromlistitem(cic.coverage_type_id) <> 'M' "

        End If

        whereClauseConditions &= Environment.NewLine & " AND  elp_utl_user.Has_access_to_data('" & networkId & "', d.company_id, d.dealer_id)  = 'Y'"

        If (Not inforceDate.Equals(String.Empty)) Or (Not (serialNumberMask.Equals(String.Empty))) Then
            If Not joinCondition = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_JOIN_CLAUSE_PLACE_HOLDER, joinCondition)
            End If
            selectStmt = selectStmt.ToLower.Replace("select", "select distinct ")
        End If
        If joinCondition = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_JOIN_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not fromClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_FROM_CLAUSE_PLACE_HOLDER, fromClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_FROM_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not IsNothing(sortBy) Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, Environment.NewLine & "ORDER BY " & Environment.NewLine & sortBy)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            rowNumParam = New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, LimitResultset)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {rowNumParam})

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Private Function IsThereALikeClause(ByVal serialNumberMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = Me.IsLikeClause(serialNumberMask)
        Return bIsLikeClause
    End Function

    Public Function LoadSerialNumberList(ByVal serialNumberMask As String,
                                ByVal compGroupId As Guid,
                                ByVal networkId As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_SERIAL_NUMBER")
        Dim ds As New DataSet
        Dim outputParameter(PO_CURSOR_SERIAL_NUMBER) As DBHelper.DBHelperParameter
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_company_group_id", DALBase.GuidToSQLString(compGroupId))
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_serial_number", serialNumberMask.ToUpper)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_network_id", networkId)
        inParameters.Add(param)

        outputParameter(PO_CURSOR_SERIAL_NUMBER) = New DBHelper.DBHelperParameter("po_cursor_serial_number", GetType(DataSet))
        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetSerialNumList")
            ds.Tables(0).TableName = "GetSerialNumList"

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadVehicleLicenseFlagList(ByVal vehicleLicenseFlagMask As String,
                             ByVal compIds As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_VEHICLE_LICENSE_FLAG_LIST")
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet
        Dim companyParam As DBHelper.DBHelperParameter
        Dim rowNumParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim sortBy As String = "upper(vehicle_license_tag)"

        bIsLikeClause = IsThereALikeClause(vehicleLicenseFlagMask)
        If ((Not (vehicleLicenseFlagMask Is Nothing)) AndAlso (Me.FormatSearchMask(vehicleLicenseFlagMask))) Then
            whereClauseConditions &= Environment.NewLine & "upper(c.vehicle_license_tag)" & vehicleLicenseFlagMask.ToUpper & " AND"
        End If

        If bIsLikeClause = True Then
            ' hextoraw
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("ci." & Me.COL_NAME_COMPANY_ID, compIds, True)
        Else
            ' not HextoRaw
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("ci." & Me.COL_NAME_COMPANY_ID, compIds, False)
        End If

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
            rowNumParam = New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNumParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadCertInfoWEPPTMX(ByVal dealerCode As String, ByVal certNumber As String, ByVal PhoneNum As String,
                                       ByVal serialNum As String, ByVal compIds As ArrayList) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GetCertInfoWEPPTMX")
        Dim whereClauseConditions As String = ""
        Dim whereClauseConditions1 As String = ""

        If dealerCode.Trim <> String.Empty Then
            whereClauseConditions &= " and d.dealer = '" & dealerCode.Trim & "'"
            whereClauseConditions1 &= " and d1.dealer = '" & dealerCode.Trim & "'"
        End If

        If certNumber.Trim <> String.Empty Then
            whereClauseConditions &= " and c.cert_number = '" & certNumber.Trim & "'"
            whereClauseConditions1 &= " and c1.cert_number = '" & certNumber.Trim & "'"
        End If

        If PhoneNum.Trim <> String.Empty Then
            whereClauseConditions &= " and c.work_phone = '" & PhoneNum.Trim & "'"
            whereClauseConditions1 &= " and c1.work_phone = '" & PhoneNum.Trim & "'"
        End If

        If serialNum.Trim <> String.Empty Then
            whereClauseConditions &= " and upper(ci.SERIAL_NUMBER)  = upper('" & serialNum.Trim & "')"
            whereClauseConditions1 &= " and upper(ci1.SERIAL_NUMBER)  = upper('" & serialNum.Trim & "')"
        End If

        whereClauseConditions &= " AND " & MiscUtil.BuildListForSql("d.company_id", compIds, True)
        whereClauseConditions1 &= " AND " & MiscUtil.BuildListForSql("d1.company_id", compIds, True)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER & "1", whereClauseConditions1)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function WS_GetCustomerFunctions(ByVal CustomerIdentifier As String, ByVal IdentifierType As String, ByVal DealerId As Guid, ByVal userId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/WS_GetCustomerFunctions")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter(Me.TOTAL_OUTPUT_PARAM_WS_1) As DBHelper.DBHelperParameter


        inputParameters = New DBHelper.DBHelperParameter() _
                {SetParameter(Me.SP_PARAM_NAME__CUSTOMER_IDENTIFIER, CustomerIdentifier),
                 SetParameter(Me.SP_PARAM_NAME__IDENTIFIER_TYPE, IdentifierType),
                 SetParameter(Me.SP_PARAM_NAME__SYSTEM_USER_ID, userId.ToByteArray),
                 SetParameter(Me.SP_PARAM_NAME__DEALER_ID, DealerId.ToByteArray)}

        outputParameter(Me.P_RETURN) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__RETURN, GetType(Integer))
        outputParameter(Me.P_EXCEPTION_MSG) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__EXCEPTION_MSG, GetType(String), 50)
        outputParameter(Me.P_CURSOR_CUSTOMER_FUNCTIONS) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__CUSTOMER_FUNCTIONS, GetType(DataSet))
        outputParameter(Me.P_CURSOR_RESPONSE_STATUS) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__RESPONSE_STATUS, GetType(DataSet))
        outputParameter(Me.P_CERT_ID) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__CERT_ID, userId.ToByteArray.GetType)
        outputParameter(Me.P_CAN_SUBMIT_CLAIM) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__CAN_SUBMIT_CLAIM, GetType(String), 50)

        Dim ds As New DataSet(Me.DATASET_NAME__CLAIM_CHECK_RESPONSE)
        ' Call DBHelper Store Procedure
        If DealerId.Equals(Guid.Empty) Then
            inputParameters(3).Value = DBNull.Value
        End If
        Try
            DBHelper.FetchSp(selectStmt, inputParameters, outputParameter, ds, Me.TABLE_NAME__GET_CUSTOMER_FUNCTIONS_RESPONSE)

            If outputParameter(Me.P_RETURN).Value <> 0 Then
                ds.Tables(0).TableName = Me.TABLE_NAME__RESPONSE_STATUS
                Return ds
            Else
                ds.Tables(0).TableName = Me.TABLE_NAME__GET_CUSTOMER_FUNCTIONS_RESPONSE
                ds.Tables(1).TableName = Me.TABLE_NAME__RESPONSE_STATUS
                Return ds
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function WS_GetCoverageInfo(ByVal CustomerIdentifier As String, ByVal IdentifierType As String, ByVal DealerId As Guid, ByVal userId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/WS_GetCoverageInfo")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter(Me.TOTAL_OUTPUT_PARAM_WS) As DBHelper.DBHelperParameter


        inputParameters = New DBHelper.DBHelperParameter() _
                {SetParameter(Me.SP_PARAM_NAME__CUSTOMER_IDENTIFIER, CustomerIdentifier),
                 SetParameter(Me.SP_PARAM_NAME__IDENTIFIER_TYPE, IdentifierType),
                 SetParameter(Me.SP_PARAM_NAME__SYSTEM_USER_ID, userId.ToByteArray),
                 SetParameter(Me.SP_PARAM_NAME__DEALER_ID, DealerId.ToByteArray)}

        outputParameter(Me.P_RETURN) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__RETURN, GetType(Integer))
        outputParameter(Me.P_EXCEPTION_MSG) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__EXCEPTION_MSG, GetType(String), 50)
        outputParameter(Me.P_CURSOR_CUSTOMER_FUNCTIONS) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__COVERAGE_INFO, GetType(DataSet))
        outputParameter(Me.P_CURSOR_RESPONSE_STATUS) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__RESPONSE_STATUS, GetType(DataSet))

        Dim ds As New DataSet(Me.DATASET_NAME__CLAIM_CHECK_RESPONSE)
        ' Call DBHelper Store Procedure
        If DealerId.Equals(Guid.Empty) Then
            inputParameters(3).Value = DBNull.Value
        End If
        Try
            DBHelper.FetchSp(selectStmt, inputParameters, outputParameter, ds, Me.TABLE_NAME__GET_COVERAGE_INFO_RESPONSE)

            If outputParameter(Me.P_RETURN).Value <> 0 Then
                ds.Tables(0).TableName = Me.TABLE_NAME__RESPONSE_STATUS
                Return ds
            Else
                ds.Tables(0).TableName = Me.TABLE_NAME__GET_COVERAGE_INFO_RESPONSE
                ds.Tables(1).TableName = Me.TABLE_NAME__RESPONSE_STATUS
                Return ds
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadCertsWithActiveClaimByCertNumAndPhone(ByVal certNumber As String, ByVal PhoneNum As String,
                                               ByVal compIds As ArrayList) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GetCertsWithActiveClaimByCertNumAndPhone")
        Dim whereClauseConditions As String = ""

        If certNumber.Trim <> String.Empty Then
            whereClauseConditions &= " and c.cert_number = '" & certNumber.Trim & "'"
        End If

        If PhoneNum.Trim <> String.Empty Then
            whereClauseConditions &= " and c.work_phone = '" & PhoneNum.Trim & "'"
        End If

        whereClauseConditions &= " AND " & MiscUtil.BuildListForSql("d.company_id", compIds, True)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListByPhoneNum(ByVal PhoneTypeMask As String, ByVal PhoneNumMask As String, ByVal certNumberMask As String,
                                       ByVal customerNameMask As String, ByVal addressMask As String,
                                       ByVal postalCodeMask As String, ByVal dealerNameMask As String,
                                       ByVal compGroupId As Guid, ByVal networkId As String, ByVal sortBy As String,
                                       Optional ByVal dealerGroupCode As String = "") As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_CERT_PHONE_SEARCH_LIST")
        Dim ds As New DataSet
        'PO_CURSOR_CERT_PHONE_SEARCH
        Dim outputParameter(PO_CURSOR_CERT_PHONE_SEARCH) As DBHelper.DBHelperParameter
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        If dealerNameMask Is Nothing Then
            dealerNameMask = ""
        End If

        param = New DBHelper.DBHelperParameter("pi_company_group_id", GuidToSQLString(compGroupId))
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_dealer", dealerNameMask)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_cert_number", certNumberMask)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_customer_name", customerNameMask)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_address", addressMask)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_phone_number", PhoneNumMask)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_postal_code", postalCodeMask)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_network_id", networkId)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_order_by", sortBy)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_phone_type", PhoneTypeMask)
        inParameters.Add(param)


        outputParameter(PO_CURSOR_CERT_PHONE_SEARCH) = New DBHelper.DBHelperParameter("po_cursor", GetType(DataSet))
        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetPhoneNumList")
            ds.Tables(0).TableName = "GetPhoneNumList"

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function getProductCodeDescription(ByVal dealerId As Guid, ByVal productCode As String) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_PROD_CODE_DESC")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray),
                                            New OracleParameter(COL_NAME_PRODUCT_CODE, productCode)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function getPremiumTotals(ByVal certId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_TOTALS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                 {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            Dim ds = New DataSet
            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, Me.TABLE_PREMIUM_TOTALS)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function getSalesTaxDetails(ByVal certId As Guid, ByVal languageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_SALES_TAX_DETAIL")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                 {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray),
                 New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            Dim ds = New DataSet
            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, Me.TABLE_SALES_TAX_DETAILS)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function ValidateProductForSpecialServices(ByVal prodCodeId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/VALIDATE_PRODUCTCODE_SPECIALSERVICES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_PRODUCT_CODE_ID, prodCodeId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_PROD_SS, parameters)
    End Function

    Public Function getCertCancellationID(ByVal certID As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_CANCELLATION_ID")

        parameters = New OracleParameter() {New OracleParameter(COL_CERT_CANCELLATION_ID, certID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_CANCEL_ID, parameters)

    End Function

    Public Function getCertCancelReqestID(ByVal certID As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_CANCEL_REQUEST_ID")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ID, certID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_CANCEL_ID, parameters)

    End Function
    Public Function getCertInstalBankInfoID(ByVal certID As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_CERT_INSTAL_BANK_INFO_ID")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ID, certID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_CANCEL_ID, parameters)

    End Function
    Public Function getCertTerm(ByVal certID As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_CERT_TERM")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ID, certID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_CANCEL_ID, parameters)

    End Function

    'Public Function getClaimWaitingPeriod(ByVal certID As Guid) As DataSet
    '    Dim ds As New DataSet
    '    Dim parameters() As OracleParameter
    '    Dim selectStmt As String = Me.Config("/SQL/GET_CLAIM_WAITING_PERIOD")

    '    parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ID, certID.ToByteArray)}
    '    Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLAIM_WAITING_PERIOD, parameters)

    'End Function
    Public Function getCertCancellationDate(ByVal certID As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_CANCELLATION_DATE")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ID, certID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_CANCEL_DATE, parameters)

    End Function

    Public Function getCertNum(ByVal certID As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GETCERTNUM")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ID, certID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function

    Public Function getClaimsforCertificate(ByVal certID As Guid, ByVal languageId As Guid)

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_CLAIMS")

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_LANGUAGE_ID, languageId.ToByteArray),
                                            New OracleParameter(COL_NAME_CERT_ID, certID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLAIMS, parameters)
    End Function

    Public Function getClaimsWithExtstatus(ByVal certID As Guid, ByVal dealerId As Guid, ByVal SerialImeiNo As String)

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_CLAIMS_EXTENDED")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ID, certID.ToByteArray),
                                            New OracleParameter("dealer_id", dealerId.ToByteArray),
                                            New OracleParameter("serial_imei_number", SerialImeiNo)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLAIMS, parameters)
    End Function


    Public Function GetOlitaConsumerCertList(ByVal certNumberMask As String, ByVal dealerId As Guid, ByVal compIds As ArrayList, ByVal LimitResultset As Int32, Optional ByVal InvoiceNumberMask As String = Nothing) As DataSet
        Dim ds As New DataSet("Olita")
        Dim parameters() As OracleParameter
        Dim whereClauseConditions As String = ""
        Dim selectStmt As String = Me.Config("/SQL/LOAD_OLITA_CONSUMER_CERT_LIST")

        Dim bIsLikeClause As Boolean = False
        bIsLikeClause = IsThereALikeClause(certNumberMask)

        If bIsLikeClause = True Then
            ' hextoraw
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("AND c." & Me.COL_NAME_COMPANY_ID, compIds, True) & " AND "
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("d." & Me.COL_NAME_COMPANY_ID, compIds, True)
        Else
            ' not HextoRaw
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("AND c." & Me.COL_NAME_COMPANY_ID, compIds, False) & " AND "
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("d." & Me.COL_NAME_COMPANY_ID, compIds, False)
        End If

        'REQ-743
        If Not InvoiceNumberMask Is Nothing AndAlso Not InvoiceNumberMask.Equals(String.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(C.INVOICE_NUMBER) = '" & InvoiceNumberMask.ToUpper & "'"

        Else
            If ((Not (certNumberMask Is Nothing)) AndAlso (Me.FormatSearchMask(certNumberMask))) Then
                whereClauseConditions &= Environment.NewLine & " AND UPPER(C.CERT_NUMBER)" & certNumberMask.ToUpper
            End If
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_DEALER_ID, dealerId.ToByteArray),
                                            New OracleParameter(Me.COL_NAME_DEALER_ID, dealerId.ToByteArray),
                                            New OracleParameter(Me.PAR_NAME_ROW_NUMBER, LimitResultset)}


        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function UpdateOlitaConsumerInfoFamily(ByVal cert_number As String, ByVal dealer As String) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_OLITA_CONSUMER_CERT_LIST")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_NUMBER, "'" & cert_number & "%'"),
                                            New OracleParameter(COL_NAME_DEALER, dealer)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function getMaxClaimsLossDate(ByVal certID As Guid) As DataSet

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/Get_Claim_MaxLossDate")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ID, certID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_MAXLOSSDATE, parameters)
    End Function

    Public Function LoadGalaxyList(ByVal certNumberMask As String, ByVal customerNameMask As String,
                        ByVal IdentificationNumberMask As String, ByVal VehicleLicenseTagMask As String,
                        ByVal VINLocatorMask As String, ByVal dealerCodeMask As String, ByVal dealerNameMask As String, ByVal CustomerPhoneMask As String,
                        ByVal compIds As ArrayList, ByVal sortBy As String, ByVal LimitResultset As Int32) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_GALAXY_LIST")
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet
        Dim companyParam As DBHelper.DBHelperParameter
        Dim rowNumParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(certNumberMask, customerNameMask,
                                VehicleLicenseTagMask, VINLocatorMask,
                                IdentificationNumberMask, dealerCodeMask)
        If ((Not (certNumberMask Is Nothing)) AndAlso (Me.FormatSearchMask(certNumberMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.CERT_NUMBER)" & certNumberMask.ToUpper & " AND"
        End If

        If ((Not ((dealerCodeMask Is Nothing) OrElse (dealerCodeMask = NO_DEALER_SELECTED))) AndAlso (Me.FormatSearchMask(dealerCodeMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(D.dealer)" & dealerCodeMask.ToUpper & " AND "
        End If

        If ((Not ((dealerNameMask Is Nothing) OrElse (dealerNameMask = NO_DEALER_SELECTED))) AndAlso (Me.FormatSearchMask(dealerNameMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(D.dealer_name)" & dealerNameMask.ToUpper & " AND "
        End If

        If ((Not (customerNameMask Is Nothing)) AndAlso (Me.FormatSearchMask(customerNameMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.CUSTOMER_NAME) " & customerNameMask.ToUpper & " AND "
        End If

        If ((Not (CustomerPhoneMask Is Nothing)) AndAlso (Me.FormatSearchMask(CustomerPhoneMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.HOME_PHONE) " & CustomerPhoneMask.ToUpper & " AND "
        End If

        If ((Not (IdentificationNumberMask Is Nothing)) AndAlso (Me.FormatSearchMask(IdentificationNumberMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.IDENTIFICATION_NUMBER) " & IdentificationNumberMask.ToUpper & " AND "
        End If

        If ((Not (VehicleLicenseTagMask Is Nothing)) AndAlso (Me.FormatSearchMask(VehicleLicenseTagMask))) Then
            whereClauseConditions &= Environment.NewLine & " UPPER(C.VEHICLE_LICENSE_TAG) " & VehicleLicenseTagMask.ToUpper & " AND "
        End If

        If ((Not (VINLocatorMask Is Nothing)) AndAlso (Me.FormatSearchMask(VINLocatorMask))) Then
            whereClauseConditions &= Environment.NewLine & " UPPER(C.VIN_LOCATOR) " & VINLocatorMask.ToUpper & " AND "
        End If


        If bIsLikeClause = True Then
            ' hextoraw
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COMPANY_ID, compIds, True) & " AND "
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("d." & Me.COL_NAME_COMPANY_ID, compIds, True)
        Else
            ' not HextoRaw
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COMPANY_ID, compIds, True) & " AND "
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("d." & Me.COL_NAME_COMPANY_ID, compIds, True)
        End If

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
            rowNumParam = New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, LimitResultset)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNumParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function LoadCertificatesForCancellation(ByVal LimitResultSet_Low As Int32, ByVal LimitResultSet_High As Int32, ByVal DealerId As Guid, ByVal SortBy As Integer, ByVal SortOrder As Integer, ByVal forCancellation As String,
                                                              Optional ByVal BranchCodeMask As String = Nothing,
                                                              Optional ByVal CertificateNumberMask As String = Nothing,
                                                              Optional ByVal CustomerNameMask As String = Nothing,
                                                              Optional ByVal EmailMask As String = Nothing) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_CERTIFICATES_FOR_CANCELLATION")
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet

        Dim bIsLikeClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(BranchCodeMask, CertificateNumberMask, CustomerNameMask, EmailMask)

        If ((Not (CertificateNumberMask Is Nothing)) AndAlso (Me.FormatSearchMask(CertificateNumberMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(C.CERT_NUMBER)" & CertificateNumberMask.ToUpper
        End If

        If ((Not (CustomerNameMask Is Nothing)) AndAlso (Me.FormatSearchMask(CustomerNameMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(C.CUSTOMER_NAME) " & CustomerNameMask.ToUpper
        End If

        If ((Not (BranchCodeMask Is Nothing)) AndAlso (Me.FormatSearchMask(BranchCodeMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(C.DEALER_BRANCH_CODE) " & BranchCodeMask.ToUpper
        End If

        If ((Not (EmailMask Is Nothing)) AndAlso (Me.FormatSearchMask(EmailMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(C.EMAIL) " & EmailMask.ToUpper
        End If

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_DEALER_ID, DealerId.ToByteArray),
                                            New OracleParameter(Me.COL_NAME_LOW_LIMIT, LimitResultSet_Low),
                                            New OracleParameter(Me.COL_NAME_HIGH_LIMIT, LimitResultSet_High)}



        Dim fromClauseConditions As String = ""

        If Not forCancellation Is Nothing AndAlso forCancellation.Equals("Y") Then
            fromClauseConditions = ",elp_contract contract, elp_product_code pc"
            whereClauseConditions &= "and contract.dealer_id = d.dealer_id" & Environment.NewLine &
                                    "and pc.dealer_id = d.dealer_id" & Environment.NewLine &
                                    "and pc.product_code = nvl(ci.product_code, c.product_code)" & Environment.NewLine &
                                    "and trunc(c.WARRANTY_SALES_DATE) >= contract.effective" & Environment.NewLine &
                                    "and trunc(c.WARRANTY_SALES_DATE) <= contract.expiration" & Environment.NewLine &
                                    "and trunc(sysdate) - nvl(pc.FULL_REFUND_DAYS, contract.FULL_REFUND_DAYS) <=  trunc(c.WARRANTY_SALES_DATE)"
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_FROM_CLAUSE_PLACE_HOLDER, fromClauseConditions)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        'ROW_NUMBER()  OVER (ORDER BY  warranty_sales_date) As number_of_row
        Dim Number_Of_Row As String = "ROW_NUMBER()  OVER (ORDER BY "

        Dim sortOrderClause As String = ""

        Select Case SortBy
            Case Me.WS_SORT_BY_WARRANTY_SALES_DATE
                sortOrderClause &= Environment.NewLine & " order by " & Me.COL_NAME_WARRANTY_SALES_DATE
                Number_Of_Row &= Me.COL_NAME_WARRANTY_SALES_DATE & ") As number_of_row"
            Case Me.WS_SORT_BY_CERTIFICATE_NUMBER
                sortOrderClause &= Environment.NewLine & " order by upper(" & COL_NAME_CERT_NUMBER & ")"
                Number_Of_Row &= Me.COL_NAME_CERT_NUMBER & ") As number_of_row"
            Case Me.WS_SORT_BY_CUSTOMER_NAME
                sortOrderClause &= Environment.NewLine & " order by upper(" & COL_NAME_CUSTOMER_NAME & ")"
                Number_Of_Row &= Me.COL_NAME_CUSTOMER_NAME & ") As number_of_row"
            Case Me.WS_SORT_BY_MFG_DESCRIPTION
                sortOrderClause &= Environment.NewLine & " order by upper(" & COL_NAME_MFG_DESCRIPTION & ")"
                Number_Of_Row &= Me.COL_NAME_MFG_DESCRIPTION & ") As number_of_row"
            Case Me.WS_SORT_BY_MODEL
                sortOrderClause &= Environment.NewLine & " order by upper(" & COL_NAME_MODEL & ")"
                Number_Of_Row &= Me.COL_NAME_MODEL & ") As number_of_row"
            Case Me.WS_SORT_BY_SERIAL_NUMBER
                sortOrderClause &= Environment.NewLine & " order by upper(" & COL_NAME_SERIAL_NUMBER & ")"
                Number_Of_Row &= Me.COL_NAME_SERIAL_NUMBER & ") As number_of_row"
            Case Me.WS_SORT_BY_SALES_REP_ID
                sortOrderClause &= Environment.NewLine & " order by upper(" & COL_NAME_SALES_REP_ID & ")"
                Number_Of_Row &= Me.COL_NAME_SALES_REP_ID & ") As number_of_row"
            Case Me.WS_SORT_BY_EMAIL
                sortOrderClause &= Environment.NewLine & " order by upper(" & COL_NAME_EMAIL & ")"
                Number_Of_Row &= Me.COL_NAME_EMAIL & ") As number_of_row"
        End Select


        If Not sortOrderClause = "" Then
            If SortOrder = WS_SORT_ORDER_ASCENDING Then
                sortOrderClause &= " ASC"
            Else
                sortOrderClause &= " DESC"
            End If
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, sortOrderClause)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_ROW_NUMBER_PLACE_HOLDER, Number_Of_Row)

        If Not sortOrderClause = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, sortOrderClause)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        End If

        Try

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadMarkupAndCommissionList(ByVal compIds As ArrayList, ByVal LimitResultSet_Low As Int16,
                                                ByVal LimitResultSet_High As Int16, ByVal DealerId As Guid,
                                                ByVal BeginDateMask As DateTime, Optional ByVal EndDateMask As DateTime = Nothing,
                                                Optional ByVal CertificateNumberMask As String = Nothing) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_MARKUP_AND_COMMISSION")
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet

        Dim bIsLikeClause As Boolean = False

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("C." & DALObjects.CertificateDAL.COL_NAME_COMPANY_ID, compIds, False)


        bIsLikeClause = IsThereALikeClause(CertificateNumberMask, CertificateNumberMask, CertificateNumberMask)

        If (Not CertificateNumberMask Is Nothing) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(C.CERT_NUMBER) = '" & CertificateNumberMask.ToUpper & "'"
        End If

        If (Not BeginDateMask.Equals(Nothing) AndAlso BeginDateMask.Ticks > 0) Then
            whereClauseConditions &= Environment.NewLine & "AND C.created_date >=  to_date('" & BeginDateMask.ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
        End If

        If (Not EndDateMask.Equals(Nothing) AndAlso EndDateMask.Ticks > 0) Then
            whereClauseConditions &= Environment.NewLine & "AND C.created_date <= to_date('" & EndDateMask.ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
        End If

        If (Not DealerId.Equals(Guid.Empty)) Then
            whereClauseConditions &= Environment.NewLine & "AND C.DEALER_ID = " & MiscUtil.GetDbStringFromGuid(DealerId, True)
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_LOW_LIMIT, LimitResultSet_Low),
                                            New OracleParameter(Me.COL_NAME_HIGH_LIMIT, LimitResultSet_High)}

        Dim Number_Of_Row As String = ",ROW_NUMBER()  OVER (ORDER BY "
        Dim sortOrderClause As String = ""
        sortOrderClause &= Environment.NewLine & " order by upper(" & COL_NAME_CERT_NUMBER & ") ASC"
        Number_Of_Row &= Me.COL_NAME_CERT_NUMBER & ") As number_of_row"
        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, sortOrderClause)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_ROW_NUMBER_PLACE_HOLDER, Number_Of_Row)

        Try

            DBHelper.Fetch(ds, selectStmt, "MARKUP_AND_COMMISSION", parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadCertificateForCancellation(ByVal DealerId As Guid, ByVal CertificateNumber As String, ByVal forCancellation As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_CERTIFICATE_FOR_CANCELLATION")
        Dim fromClauseConditions As String = ""
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_DEALER_ID, DealerId.ToByteArray),
                                            New OracleParameter(Me.COL_NAME_CERT_NUMBER, CertificateNumber)}

        If Not forCancellation Is Nothing AndAlso forCancellation.Equals("Y") Then
            fromClauseConditions = ",elp_contract contract, elp_product_code pc"
            whereClauseConditions &= "and contract.dealer_id = d.dealer_id" & Environment.NewLine &
                                    "and pc.dealer_id = d.dealer_id" & Environment.NewLine &
                                    "and pc.product_code = nvl(ci.product_code, c.product_code)" & Environment.NewLine &
                                    "and trunc(c.WARRANTY_SALES_DATE) >= contract.effective" & Environment.NewLine &
                                    "and trunc(c.WARRANTY_SALES_DATE) <= contract.expiration" & Environment.NewLine &
                                    "and trunc(sysdate) - nvl(pc.FULL_REFUND_DAYS, contract.FULL_REFUND_DAYS) <=  trunc(c.WARRANTY_SALES_DATE)"
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_FROM_CLAUSE_PLACE_HOLDER, fromClauseConditions)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function GalaxyLoadCertificateDetail(ByVal certNumber As String, ByVal dealerCode As String, ByVal compId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_GALAXY_CERTIFICATE_DETAIL")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COMPANY_ID2, compId.ToByteArray),
                                            New OracleParameter(Me.COL_NAME_CERT_NUMBER, certNumber),
                                            New OracleParameter(Me.COL_NAME_DEALER, dealerCode),
                                            New OracleParameter(Me.COL_NAME_COMPANY_ID, compId.ToByteArray)}
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    'Public Function ClaimsForCancelCert(ByVal dealerId As Guid, _
    '                            ByVal certNumber As String) As DataSet

    '    Dim selectStmt As String = Me.Config("/SQL/CLAIMS_CANCEL_CERT")
    '    Dim ds As New DataSet

    '    Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
    '                    {New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray), _
    '                     New DBHelper.DBHelperParameter("cert_number", certNumber)}
    '    Try
    '        DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLAIMS_CANCEL_CERT, parameters)
    '        Return ds
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try

    'End Function

    Public Function ClaimsForCancelNotClosedCert(ByVal dealerId As Guid,
                                ByVal certNumber As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/CLAIMS_CANCEL_NOT_CLOSED_CERT")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray),
                         New DBHelper.DBHelperParameter("cert_number", certNumber)}
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLAIMS_CANCEL_CERT, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function ClaimsForCancelPaidCert(ByVal dealerId As Guid,
                                ByVal certNumber As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/CLAIMS_CANCEL_PAID_CERT")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray),
                         New DBHelper.DBHelperParameter("cert_number", certNumber)}
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLAIMS_CANCEL_CERT, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function TotalClaimsNotClosedForCert(ByVal dealerId As Guid,
                            ByVal certNumber As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/TOTAL_CLAIMS_NOT_CLOSED_FOR_CERT")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray),
                         New DBHelper.DBHelperParameter("cert_number", certNumber)}
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLAIMS_CANCEL_CERT, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function ActiveClaimExist(ByVal dealerId As Guid,
                            ByVal certNumber As String,
                            ByVal cancelDate As DateTime) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/TOTAL_ACTIVE_CLAIMS_FOR_CERT")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray),
                         New DBHelper.DBHelperParameter("cert_number", certNumber),
                         New DBHelper.DBHelperParameter("cancellation_date", cancelDate)}
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_CLAIMS_CANCEL_CERT, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function ClaimLogisticsGetCert(ByVal certNumber As String, ByVal dealerCode As String, ByVal compIds As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/CLAIMLOGISTICS_CERT_DETAIL")
        Dim parameters() As OracleParameter
        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & DALObjects.ClaimDAL.COL_NAME_COMPANY_ID, compIds, False)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_CERT_NUMBER, certNumber),
                                            New OracleParameter(Me.COL_NAME_DEALER, dealerCode)}
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetCertListByInvoiceNumber(ByVal InvoiceNumber As String, ByVal compIds As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_CERT_LIST_BY_INVOICE_NUMBER")
        Dim parameters() As OracleParameter
        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & DALObjects.ClaimDAL.COL_NAME_COMPANY_ID, compIds, False)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_INVOICE_NUMBER, InvoiceNumber)}

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetCertListWithInvoiceNumberByCertNUmber(ByVal CertificateNumber As String, ByVal compIds As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_CERT_LIST_WITH_INVOICE_NUMBER_BY_CERT_NUMBER")
        Dim parameters() As OracleParameter
        Dim whereClauseConditions As String = ""
        Dim whereClauseConditions2 As String = ""

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & DALObjects.ClaimDAL.COL_NAME_COMPANY_ID, compIds, False)
        whereClauseConditions2 &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("cert." & DALObjects.ClaimDAL.COL_NAME_COMPANY_ID, compIds, False)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER_2, whereClauseConditions2)

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_CERT_NUMBER, CertificateNumber)}

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadOlitaList(ByVal certNumberMask As String, ByVal customerNameMask As String,
                                  ByVal CustomerPhoneMask As String, ByVal compIds As ArrayList, ByVal sortBy As String, ByVal LimitResultset As Int32) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_OLITA_LIST")
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet
        Dim companyParam As DBHelper.DBHelperParameter
        Dim rowNumParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(certNumberMask, customerNameMask, CustomerPhoneMask)
        If ((Not (certNumberMask Is Nothing)) AndAlso (Me.FormatSearchMask(certNumberMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.CERT_NUMBER)" & certNumberMask.ToUpper & " AND"
        End If

        If ((Not (customerNameMask Is Nothing)) AndAlso (Me.FormatSearchMask(customerNameMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.CUSTOMER_NAME) " & customerNameMask.ToUpper & " AND "
        End If

        If ((Not (CustomerPhoneMask Is Nothing)) AndAlso (Me.FormatSearchMask(CustomerPhoneMask))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(C.HOME_PHONE) " & CustomerPhoneMask.ToUpper & " AND "
        End If

        If bIsLikeClause = True Then
            ' hextoraw
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COMPANY_ID, compIds, True)

        Else
            ' not HextoRaw
            whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COMPANY_ID, compIds, True)

        End If

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
            rowNumParam = New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, LimitResultset)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {rowNumParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetCertIDWithCertNumAndDealer(ByVal certNumber As String, ByVal DealerID As Guid) As DataSet

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/WS_GetCertIDWithCertNumberAndDealer")

        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_CERT_NUMBER, certNumber),
                                            New OracleParameter(COL_NAME_DEALER_ID, DealerID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function GetMonthlygrossAmount(ByVal cert_id As Guid) As Decimal
        Dim selectStmt As String = Me.Config("/SQL/CERTIFICATE_COVERAGEAMOUNT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_id", cert_id.ToByteArray)}

        Return Convert.ToDecimal(DBHelper.ExecuteScalar(selectStmt, parameters))
    End Function

    Public Function GetTotalOverallPayments(certid As Guid) As Integer
        Dim selectStmt As String = Me.Config("/SQL/CERTIFICATE_OVERALL_PAYMENTS")
        Dim parameters() As DBHelperParameter = New DBHelperParameter() _
                {New DBHelperParameter("cert_id", certid.ToByteArray)}

        Return Convert.ToInt32(DBHelper.ExecuteScalar(selectStmt, parameters))
    End Function

    Public Function GetMonthsPassedForH3GI(PymtActDate As Date) As Integer
        Dim ds As New DataSet, intMonthsPassed As Integer
        Dim inputParameters(0) As DBHelperParameter
        Dim outputParameter(0) As DBHelperParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_MONTHS_PASSED_FOR_H3GI")
        Try
            'parameters = New OracleParameter() {New OracleParameter(COL_NAME_PAYMENT_ACT_DATE, PymtActDate)}
            ' Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_MONTHSPASSED, parameters)
            inputParameters(0) = New DBHelperParameter(COL_NAME_PAYMENT_ACT_DATE, PymtActDate, GetType(Date))
            outputParameter(0) = New DBHelperParameter("MonthsPassed", GetType(String), 32)

            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)


            If (Not outputParameter(0).Value Is Nothing) Then

                intMonthsPassed = outputParameter(0).Value

            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return intMonthsPassed
    End Function
#End Region

#Region "Add Certificate"


    Public Function LoadDealerDetailsForCertADD(ByVal DealerID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/CERT_ADD_ENABLED_DEALER_DETAILS")
        Dim ds As DataSet = New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", DealerID.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("dealer_id", DealerID.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("dealer_id", DealerID.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, "DealerDetail", parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub InsertCertificate(ByVal TransID As Guid, ByVal DealerID As Guid, ByVal CertNum As String, ByVal ProdCode As String,
                        ByVal WarrSalesDate As Date, ByVal ProdPurchaseDate As Date, ByVal WarrPrice As Double, ByVal ItemCode As String,
                        ByVal ItemDesc As String, ByVal ProdPrice As Double, ByVal ExtWarranty As Integer, ByVal ManWarranty As Integer,
                        ByVal SalesRepNum As String, ByVal BranchCode As String, ByVal InvoiceNum As String, ByVal CustTaxId As String, ByVal Salutation As String,
                        ByVal CustName As String, ByVal CustAddress1 As String, ByVal CustAddress2 As String, ByVal CustCity As String,
                        ByVal CustZip As String, ByVal CustState As String, ByVal CustHomePhone As String, ByVal CustWorkPhone As String,
                        ByVal CustEmail As String, ByVal Make As String, ByVal Model As String, ByVal SerialNum As String,
                        ByVal CustCountryCode As String, ByVal PurchaseCountryCode As String, ByVal CurrencyCode As String, ByVal PaymentType As String,
                        ByVal BillingFrequency As Integer, ByVal NumOfInstallments As Integer, ByVal InstallmentAmt As Double,
                        ByVal BankAcctOwnerName As String, ByVal BankAcctNumber As String, ByVal BankRoutingNum As String,
                        ByVal MembershipNumer As String, ByVal KeepFileWhenErr As Integer,
                        ByRef ErrMsg As String, ByRef CertID As Guid, ByRef ErrMsgUIProgCode As String,
                        ByRef ErrMsgParamList As String, ByRef ErrMsgParamCnt As Integer,
                        ByVal User As String, Optional ByVal BundleItemCount As Integer = 0,
                        Optional ByVal BundleItemMake As Generic.List(Of String) = Nothing, Optional ByVal BundleItemModel As Generic.List(Of String) = Nothing,
                        Optional ByVal BundleItemSerialNum As Generic.List(Of String) = Nothing, Optional ByVal BundleItemDesc As Generic.List(Of String) = Nothing,
                        Optional ByVal BundleItemPrice As Generic.List(Of Double) = Nothing, Optional ByVal BundleItemMfgWarranty As Generic.List(Of Integer) = Nothing,
                        Optional ByVal BundleItemProductCode As Generic.List(Of String) = Nothing,
                        Optional ByVal RecordType As String = "FC", Optional ByVal SkuNumber As String = Nothing, Optional ByVal SubscriberStatus As String = Nothing,
                        Optional ByVal PostPrePaid As String = Nothing, Optional ByVal MembershipType As String = Nothing,
                        Optional ByVal BillingPlan As String = Nothing, Optional ByVal BillingCycle As String = Nothing,
                        Optional ByVal MaritalStatus As String = Nothing, Optional ByVal PersonType As String = Nothing,
                        Optional ByVal Gender As String = Nothing, Optional ByVal Nationality As String = Nothing,
                        Optional ByVal PlaceOfBirth As String = Nothing, Optional ByVal CUIT_CUIL As String = Nothing, Optional DateOfBirth As Date = Nothing,
                        Optional ByVal MarketingPromoSer As String = Nothing, Optional ByVal MarketingPromoNum As String = Nothing,
                        Optional ByVal SalesChannel As String = Nothing)

        Dim sqlStmt As String
        sqlStmt = Me.Config("/SQL/CERT_ADD_INSERT")
        Try
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("p_Err_Msg", ErrMsg.GetType, 500),
                            New DBHelper.DBHelperParameter("P_ErrMsg_UIProgCode", ErrMsgUIProgCode.GetType, 500),
                            New DBHelper.DBHelperParameter("p_ErrMsg_ParamList", ErrMsgParamList.GetType, 500),
                            New DBHelper.DBHelperParameter("p_ErrMsg_ParamCnt", ErrMsgParamCnt.GetType),
                            New DBHelper.DBHelperParameter("p_Cert_ID", CertID.ToByteArray.GetType, 16),
                            New DBHelper.DBHelperParameter("p_certificate", GetType(String), 20)}
            Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
            Dim param As DBHelper.DBHelperParameter

            param = New DBHelper.DBHelperParameter("p_Keep_File_When_Err", KeepFileWhenErr)
            inParameters.Add(param)

            param = New DBHelper.DBHelperParameter("p_User", User)
            inParameters.Add(param)

            param = New DBHelper.DBHelperParameter("p_DEALER_ID", DealerID.ToByteArray)
            inParameters.Add(param)

            param = New DBHelper.DBHelperParameter("p_Cert_Num", CertNum)
            inParameters.Add(param)

            param = New DBHelper.DBHelperParameter("p_Prod_Code", ProdCode)
            inParameters.Add(param)

            Dim strWarrSalesDate As String = WarrSalesDate.ToString("MM/dd/yyyy")
            param = New DBHelper.DBHelperParameter("p_Warranty_SaleDate", strWarrSalesDate)
            inParameters.Add(param)

            If TransID <> Guid.Empty Then
                param = New DBHelper.DBHelperParameter("p_Trans_ID", TransID.ToByteArray)
                inParameters.Add(param)
            End If

            Dim strProdPurchaseDate As String = ProdPurchaseDate.ToString("MM/dd/yyyy")
            If ProdPurchaseDate > Date.MinValue Then
                param = New DBHelper.DBHelperParameter("p_Purchase_Date", strProdPurchaseDate)
                inParameters.Add(param)
            End If

            param = New DBHelper.DBHelperParameter("p_Price_Pol", WarrPrice)
            inParameters.Add(param)

            If ItemCode <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Item_Code", ItemCode)
                inParameters.Add(param)
            End If

            If ItemDesc <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Item", ItemDesc)
                inParameters.Add(param)
            End If

            If ProdPrice >= 0 Then
                param = New DBHelper.DBHelperParameter("p_Product_Price", ProdPrice)
                inParameters.Add(param)

                param = New DBHelper.DBHelperParameter("p_Original_Retail_Price", ProdPrice)
                inParameters.Add(param)
            End If

            If ManWarranty >= 0 Then
                param = New DBHelper.DBHelperParameter("p_Man_Warranty", ManWarranty)
                inParameters.Add(param)
            End If

            If ExtWarranty >= 0 Then
                param = New DBHelper.DBHelperParameter("p_Ext_Warranty", ExtWarranty)
                inParameters.Add(param)
            End If

            If SalesRepNum <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_SR", SalesRepNum)
                inParameters.Add(param)
            End If

            If BranchCode <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Branch_Code", BranchCode)
                inParameters.Add(param)
            End If

            If InvoiceNum <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Number_Comp", InvoiceNum)
                inParameters.Add(param)
            End If

            If CustTaxId <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_ID_Num", CustTaxId)
                inParameters.Add(param)
            End If


            If Salutation <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Salutation", Salutation)
                inParameters.Add(param)
            End If

            If CustName <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Cust_Name", CustName)
                inParameters.Add(param)
            End If

            If CustAddress1 <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Cust_Address1", CustAddress1)
                inParameters.Add(param)
            End If

            If CustAddress2 <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Cust_Address2", CustAddress2)
                inParameters.Add(param)
            End If

            If CustCity <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Cust_City", CustCity)
                inParameters.Add(param)
            End If

            If CustZip <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Cust_Zip", CustZip)
                inParameters.Add(param)
            End If

            If CustState <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Cust_State", CustState)
                inParameters.Add(param)
            End If

            If CustHomePhone <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Cust_HomePhone", CustHomePhone)
                inParameters.Add(param)
            End If

            'REQ 983 06/23/2012

            If CustWorkPhone <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Cust_WorkPhone", CustWorkPhone)
                inParameters.Add(param)
            Else
                param = New DBHelper.DBHelperParameter("p_Cust_WorkPhone", "0")
                inParameters.Add(param)
            End If
            'REQ 983 end

            If CustEmail <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Cust_Email", CustEmail)
                inParameters.Add(param)
            End If

            If Make <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Manufacturer", Make)
                inParameters.Add(param)
            End If

            If Model <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Model", Model)
                inParameters.Add(param)
            End If

            If SerialNum <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Serial_Num", SerialNum)
                inParameters.Add(param)
            End If

            If CustCountryCode <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Cust_Country", CustCountryCode)
                inParameters.Add(param)
            End If

            If PurchaseCountryCode <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Purchase_Country", PurchaseCountryCode)
                inParameters.Add(param)
            End If

            If CurrencyCode <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_Currency_Code", CurrencyCode)
                inParameters.Add(param)
            End If

            If PaymentType <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_PYMT_Type", PaymentType)
                inParameters.Add(param)
            End If

            If BillingFrequency >= 0 Then
                param = New DBHelper.DBHelperParameter("p_BillingFrequency", BillingFrequency)
                inParameters.Add(param)
            End If

            If NumOfInstallments >= 0 Then
                param = New DBHelper.DBHelperParameter("p_NumOfInstallments", NumOfInstallments)
                inParameters.Add(param)
            End If

            If InstallmentAmt >= 0 Then
                param = New DBHelper.DBHelperParameter("p_InstallmentAmount", InstallmentAmt)
                inParameters.Add(param)
            End If

            If BankAcctOwnerName <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_BankAcctOwnerName", BankAcctOwnerName)
                inParameters.Add(param)
            End If

            If BankAcctNumber <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_BankAcctNumber", BankAcctNumber)
                inParameters.Add(param)
            End If

            If BankRoutingNum <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_BankRoutingNumber", BankRoutingNum)
                inParameters.Add(param)
            End If

            If MembershipNumer <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_MembershipNumber", MembershipNumer)
                inParameters.Add(param)
            End If

            If RecordType <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_RecordType", RecordType)
                inParameters.Add(param)
            End If

            If SkuNumber <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_SkuNumber", SkuNumber)
                inParameters.Add(param)
            End If

            If SubscriberStatus <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_SubscriberStatus", SubscriberStatus)
                inParameters.Add(param)
            End If

            If PostPrePaid <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_PostPrePaid", PostPrePaid)
                inParameters.Add(param)
            End If

            If MembershipType <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_MembershipType", MembershipType)
                inParameters.Add(param)
            End If

            If BillingPlan <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_BillingPlan", BillingPlan)
                inParameters.Add(param)
            End If

            If BillingCycle <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_BillingCycle", BillingCycle)
                inParameters.Add(param)
            End If

            If BundleItemCount > 0 Then
                Dim i As Integer
                Dim paramName As String
                For i = 1 To BundleItemCount
                    paramName = String.Format("p_ITEM{0}_Make", i + 1)
                    param = New DBHelper.DBHelperParameter(paramName, BundleItemMake.Item(i - 1))
                    inParameters.Add(param)

                    paramName = String.Format("p_ITEM{0}_Model", i + 1)
                    param = New DBHelper.DBHelperParameter(paramName, BundleItemModel.Item(i - 1))
                    inParameters.Add(param)

                    paramName = String.Format("p_ITEM{0}_Serial_Num", i + 1)
                    param = New DBHelper.DBHelperParameter(paramName, BundleItemSerialNum.Item(i - 1))
                    inParameters.Add(param)

                    paramName = String.Format("p_ITEM{0}_Desc", i + 1)
                    param = New DBHelper.DBHelperParameter(paramName, BundleItemDesc.Item(i - 1))
                    inParameters.Add(param)

                    paramName = String.Format("p_ITEM{0}_Price", i + 1)
                    param = New DBHelper.DBHelperParameter(paramName, BundleItemPrice.Item(i - 1))
                    inParameters.Add(param)

                    paramName = String.Format("p_ITEM{0}_Man_Warranty", i + 1)
                    param = New DBHelper.DBHelperParameter(paramName, BundleItemMfgWarranty.Item(i - 1))
                    inParameters.Add(param)

                    paramName = String.Format("p_ITEM{0}_Bundle_value", i + 1)
                    param = New DBHelper.DBHelperParameter(paramName, BundleItemProductCode.Item(i - 1))
                    inParameters.Add(param)
                Next
            End If

            If MaritalStatus <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_marital_status", MaritalStatus)
                inParameters.Add(param)
            End If

            If PersonType <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_person_type", PersonType)
                inParameters.Add(param)
            End If

            If Gender <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_gender", Gender)
                inParameters.Add(param)
            End If

            If Gender <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_nationality", Nationality)
                inParameters.Add(param)
            End If

            If PlaceOfBirth <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_place_of_birth", PlaceOfBirth)
                inParameters.Add(param)
            End If

            If CUIT_CUIL <> String.Empty Then
                param = New DBHelper.DBHelperParameter("p_CUIT_CUIL", CUIT_CUIL)
                inParameters.Add(param)
            End If

            If Not DateOfBirth = Nothing Then
                Dim strDateOfBirth As String = DateOfBirth.ToString("MM/dd/yyyy")
                param = New DBHelper.DBHelperParameter("p_date_of_birth", strDateOfBirth)
                inParameters.Add(param)
            End If

            If Not MarketingPromoSer = Nothing Then
                param = New DBHelper.DBHelperParameter("p_MarketingPromoSer", MarketingPromoSer)
                inParameters.Add(param)
            End If
            If Not MarketingPromoNum = Nothing Then
                param = New DBHelper.DBHelperParameter("p_MarketingPromoNum", MarketingPromoNum)
                inParameters.Add(param)
            End If

            If Not SalesChannel = Nothing Then
                param = New DBHelper.DBHelperParameter("p_SalesChannel", SalesChannel)
                inParameters.Add(param)
            End If


            DBHelper.ExecuteSpParamBindByName(sqlStmt, inParameters.ToArray, outParameters)

            If Not outParameters(0).Value Is Nothing Then
                ErrMsg = outParameters(0).Value.ToString().Trim
                If Not outParameters(1).Value Is Nothing Then
                    ErrMsgUIProgCode = outParameters(1).Value.ToString().Trim
                End If
                If Not outParameters(2).Value Is Nothing Then
                    ErrMsgParamList = outParameters(2).Value.ToString().Trim
                End If
                If Not outParameters(3).Value Is Nothing Then
                    Try
                        ErrMsgParamCnt = CType(outParameters(3).Value, Integer)
                    Catch ex As Exception
                        ErrMsgParamCnt = 0
                    End Try
                End If
            End If

            If ErrMsg.Trim = "" AndAlso (Not outParameters(4).Value Is Nothing) Then
                CertID = CType(outParameters(4).Value, Guid)
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub CancelCertByExternalNumber(ByVal CompanyCode As String, ByVal DealerCode As String,
                                 ByVal ExternalCertNumType As String, ByVal ExternalCertNum As String,
                                 ByVal CancelllationDate As Date, ByVal CancellationReasonCode As String,
                                 ByVal CallerName As String, ByVal User As String,
                                 ByRef oDealerCode As String, ByRef oCertificateNum As String,
                                 ByRef oErrCode As String, ByRef oErrMsg As String)

        Dim sqlStmt As String
        sqlStmt = Me.Config("/SQL/CANCEL_CERT_BY_EXTERNAL_NUMBER")
        Try
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("po_DealerCode", oDealerCode.GetType, 500),
                            New DBHelper.DBHelperParameter("po_CertNumber", oCertificateNum.GetType, 500),
                            New DBHelper.DBHelperParameter("po_ErrorCode", oErrCode.GetType, 500),
                            New DBHelper.DBHelperParameter("po_ErrMessage", oErrMsg.GetType, 3000)}

            Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
            Dim param As DBHelper.DBHelperParameter

            param = New DBHelper.DBHelperParameter("pi_CompanyCode", CompanyCode.Trim)
            inParameters.Add(param)
            param = New DBHelper.DBHelperParameter("pi_DealerCode", DealerCode.Trim)
            inParameters.Add(param)
            param = New DBHelper.DBHelperParameter("pi_ExternalCertNumType", ExternalCertNumType)
            inParameters.Add(param)
            param = New DBHelper.DBHelperParameter("pi_ExternalCertNum", ExternalCertNum.Trim)
            inParameters.Add(param)
            param = New DBHelper.DBHelperParameter("pi_CancellationDate", CancelllationDate.ToString("MM/dd/yyyy"))
            inParameters.Add(param)
            param = New DBHelper.DBHelperParameter("pi_CancellationReason", CancellationReasonCode.Trim)
            inParameters.Add(param)
            param = New DBHelper.DBHelperParameter("pi_CallerName", CallerName.Trim)
            inParameters.Add(param)
            param = New DBHelper.DBHelperParameter("pi_User", User)
            inParameters.Add(param)

            DBHelper.ExecuteSpParamBindByName(sqlStmt, inParameters.ToArray, outParameters)

            If Not outParameters(0).Value Is Nothing Then
                oDealerCode = outParameters(0).Value.ToString().Trim
            End If

            If Not outParameters(1).Value Is Nothing Then
                oCertificateNum = outParameters(1).Value.ToString().Trim
            End If

            If Not outParameters(2).Value Is Nothing Then
                oErrCode = outParameters(2).Value.ToString().Trim
            End If

            If Not outParameters(3).Value Is Nothing Then
                oErrMsg = outParameters(3).Value.ToString().Trim
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function GetCertificateByImei(ByVal companyCode As String, ByVal dealerCode As String,
                                         ByVal imeiNumber As String, ByVal certStatus As String,
                                         ByVal userId As String,
                                         ByRef oErrCode As Integer, ByRef oErrMsg As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/Get_Certificate_By_IMEI")
        Dim dsResult As New DataSet
        oErrCode = 0
        oErrMsg = String.Empty

        Using connection As New OracleConnection(DBHelper.ConnectString)
            Using command As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, connection)
                command.BindByName = True
                command.AddParameter("pi_company_code", OracleDbType.Varchar2, 25, companyCode, ParameterDirection.Input)
                command.AddParameter("pi_IMEI_number", OracleDbType.Varchar2, 100, imeiNumber, ParameterDirection.Input)
                command.AddParameter("pi_User", OracleDbType.Varchar2, 100, userId, ParameterDirection.Input)
                command.AddParameter("po_cert_table", OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                command.AddParameter("po_return_code", OracleDbType.Int64, 10, Nothing, ParameterDirection.Output)
                command.AddParameter("po_exception_msg", OracleDbType.Varchar2, 1000, Nothing, ParameterDirection.Output)

                If String.IsNullOrEmpty(dealerCode) = False Then
                    command.AddParameter("pi_dealer_code", OracleDbType.Varchar2, 25, dealerCode, ParameterDirection.Input)
                End If
                If String.IsNullOrEmpty(certStatus) = False Then
                    command.AddParameter("pi_Cert_Status", OracleDbType.Varchar2, 25, certStatus, ParameterDirection.Input)
                End If

                Try
                    dsResult = OracleDbHelper.Fetch(command, TABLE_NAME)
                    If Integer.TryParse(command.Parameters("po_return_code").Value.ToString(), oErrCode) = False Then
                        oErrCode = 999
                    End If
                    oErrMsg = command.Parameters("po_exception_msg").Value.ToString()
                Catch ex As Exception
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
                End Try
            End Using
        End Using

        Return dsResult
    End Function

    Public Sub UpdateImeiAddEvent(ByVal certItemId As Guid, ByVal imeiNumberCurrent As String, ByVal imeiNumberNew As String, ByVal identificationType As String,
                                         ByRef oErrCode As Integer, ByRef oErrMsg As String)
        Dim selectStmt As String = Me.Config("/SQL/Update_IMEI_AddEvent")
        oErrCode = 0
        oErrMsg = String.Empty

        Using command As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure)
            command.BindByName = True
            command.AddParameter("pi_Cert_Item_Id", OracleDbType.Raw, 100, certItemId.ToByteArray, ParameterDirection.Input)
            command.AddParameter("pi_IMEInumber_Current", OracleDbType.Varchar2, 100, imeiNumberCurrent, ParameterDirection.Input)
            command.AddParameter("pi_IMEInumber_New", OracleDbType.Varchar2, 100, imeiNumberNew, ParameterDirection.Input)
            command.AddParameter("pi_Identification_Type", OracleDbType.Varchar2, 100, identificationType, ParameterDirection.Input)
            command.AddParameter("po_return_code", OracleDbType.Int64, 10, oErrCode, ParameterDirection.Output)
            command.AddParameter("po_exception_msg", OracleDbType.Varchar2, 1000, Nothing, ParameterDirection.Output)

            Try
                OracleDbHelper.ExecuteNonQuery(command)
                If Integer.TryParse(command.Parameters("po_return_code").Value.ToString(), oErrCode) = False Then
                    oErrCode = 999
                End If
                oErrMsg = command.Parameters("po_exception_msg").Value.ToString()
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Using

    End Sub
#End Region

#Region "Overloaded Methods"
    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim addressDAL As New AddressDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            'MyBase.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Deleted)


            'Second Pass updates additions and changes
            addressDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            Update(familyDataset, tr, DataRowState.Modified)

            'At the end delete the Address
            addressDAL.Update(familyDataset, tr, DataRowState.Deleted)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    'Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
    '    MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
    'End Sub
    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
        Throw New NotSupportedException()
    End Sub
    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CERT_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CERT_ID)
            .AddParameter(PAR_I_NAME_COMPANY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID)
            .AddParameter(PAR_I_NAME_DEALER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter(PAR_I_NAME_CERT_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CERT_NUMBER)
            .AddParameter(PAR_I_NAME_PAYMENT_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PAYMENT_TYPE_ID)
            .AddParameter(PAR_I_NAME_COMMISSION_BREAKDOWN_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COMMISSION_BREAKDOWN_ID)
            .AddParameter(PAR_I_NAME_FINANCE_CURRENCY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_FINANCE_CURRENCY_ID)
            .AddParameter(PAR_I_NAME_PURCHASE_CURRENCY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PURCHASE_CURRENCY_ID)
            .AddParameter(PAR_I_NAME_METHOD_REPAIR_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_METHOD_OF_REPAIR_ID)
            .AddParameter(PAR_I_NAME_TYPE_OF_EQUIPMENT_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_TYPE_OF_EQUIPMENT_ID)
            .AddParameter(PAR_I_NAME_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_PRODUCT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE)
            .AddParameter(PAR_I_NAME_STATUS_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STATUS_CODE)
            .AddParameter(PAR_I_NAME_PRODUCT_SALES_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_PRODUCT_SALES_DATE)
            .AddParameter(PAR_I_NAME_WARRANTY_SALES_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_WARRANTY_SALES_DATE)
            .AddParameter(PAR_I_NAME_INVOICE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INVOICE_NUMBER)
            .AddParameter(PAR_I_NAME_IDENTIFICATION_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_IDENTIFICATION_NUMBER)
            .AddParameter(PAR_I_NAME_CUSTOMER_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CUSTOMER_NAME)
            .AddParameter(PAR_I_NAME_HOME_PHONE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_HOME_PHONE)
            .AddParameter(PAR_I_NAME_WORK_PHONE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_WORK_PHONE)
            .AddParameter(PAR_I_NAME_EMAIL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_EMAIL)
            .AddParameter(PAR_I_NAME_DEALER_BRANCH_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DEALER_BRANCH_CODE)
            .AddParameter(PAR_I_NAME_SALES_REP_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SALES_REP_NUMBER)
            .AddParameter(PAR_I_NAME_QUOTA_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_QUOTA_NUMBER)
            .AddParameter(PAR_I_NAME_MONTLY_PAYMENTS, OracleDbType.Decimal, sourceColumn:=COL_NAME_MONTHLY_PAYMENTS)
            .AddParameter(PAR_I_NAME_FINANCED_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_FINANCED_AMOUNT)
            .AddParameter(PAR_I_NAME_DEALER_ITEM, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DEALER_ITEM)
            .AddParameter(PAR_I_NAME_SALES_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_SALES_PRICE)
            .AddParameter(PAR_I_NAME_INTEREST_RATE, OracleDbType.Decimal, sourceColumn:=COL_NAME_INTEREST_RATE)
            .AddParameter(PAR_I_NAME_CAMPAIGN_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CAMPAIGN_NUMBER)
            .AddParameter(PAR_I_NAME_SOURCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SOURCE)
            .AddParameter(PAR_I_NAME_DEALER_PRODUCT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DEALER_PRODUCT_CODE)
            .AddParameter(PAR_I_NAME_DATE_PAID_FOR, OracleDbType.Date, sourceColumn:=COL_NAME_DATE_PAID_FOR)
            .AddParameter(PAR_I_NAME_DATE_PAID, OracleDbType.Date, sourceColumn:=COL_NAME_DATE_PAID)
            .AddParameter(PAR_I_NAME_RETAILER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RETAILER)
            .AddParameter(PAR_I_NAME_SALUTATION_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SALUTATION_ID)
            .AddParameter(PAR_I_NAME_OLD_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_OLD_NUMBER)
            .AddParameter(PAR_I_NAME_INSURANCE_ACTIVATION_DATE, OracleDbType.Date, sourceColumn:=COL_INSURANCE_ACTIVATION_DATE)
            .AddParameter(PAR_I_NAME_DOCUMENT_AGENCY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DOCUMENT_AGENCY)
            .AddParameter(PAR_I_NAME_DOCUMENT_ISSUE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_DOCUMENT_ISSUE_DATE)
            .AddParameter(PAR_I_NAME_RG_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RG_NUMBER)
            .AddParameter(PAR_I_NAME_RATING_PLAN, OracleDbType.Decimal, sourceColumn:=COL_NAME_RATING_PLAN)
            .AddParameter(PAR_I_NAME_ID_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ID_TYPE)
            .AddParameter(PAR_I_NAME_DOCUMENT_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DOCUMENT_TYPE_ID)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_COUNTRY_PURCHASE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COUNTRY_OF_PURCHASE_ID)
            .AddParameter(PAR_I_NAME_PASSWORD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PASSWORD)
            .AddParameter(PAR_I_NAME_CURRENCY_CERT_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CURRENCY_CERT_ID)
            .AddParameter(PAR_I_NAME_VEHICLE_LICENSE_TAG, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VEHICLE_LICENSE_TAG)
            .AddParameter(PAR_I_NAME_BIRTH_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_DATE_OF_BIRTH)
            .AddParameter(PAR_I_NAME_MAILING_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_MAILING_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_MEMBERSHIP_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MEMBERSHIP_NUMBER)
            .AddParameter(PAR_I_NAME_PRIMARY_MEMBER_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRIMARY_MEMBER_NAME)
            .AddParameter(PAR_I_NAME_VAT_NUM, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VAT_NUM)
            .AddParameter(PAR_I_NAME_MEMBERSHIP_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_MEMBERSHIP_TYPE_ID)
            .AddParameter(PAR_I_NAME_LANGUAGE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_LANGUAGE_ID)
            .AddParameter(PAR_I_NAME_POST_PRE_PAID_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_POST_PRE_PAID_ID)
            .AddParameter(PAR_I_NAME_REGION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REGION)
            .AddParameter(PAR_I_NAME_OCCUPATION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_OCCUPATION)
            .AddParameter(PAR_I_NAME_POLITICALLY_EXPOSED_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_POLITICALLY_EXPOSED_ID)
            .AddParameter(PAR_I_NAME_NATIONALITYID, OracleDbType.Raw, sourceColumn:=COL_NAME_NATIONALITY)
            .AddParameter(PAR_I_NAME_PLACEOFBIRTHID, OracleDbType.Raw, sourceColumn:=COL_NAME_PLACEOFBIRTH)
            .AddParameter(PAR_I_NAME_GENDERID, OracleDbType.Raw, sourceColumn:=COL_NAME_GENDER)
            .AddParameter(PAR_I_NAME_CUIT_CUIL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CUIT_CUIL)
            .AddParameter(PAR_I_NAME_LINKED_CERT_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LINKED_CERT_NUMBER)
            .AddParameter(PAR_I_NAME_CUSTINFO_LASTCHANGE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CUSTOMERINFO_LASTCHANGE_DATE)
            .AddParameter(PAR_I_NAME_SUBSCRIBER_STATUS, OracleDbType.Raw, sourceColumn:=COL_NAME_SUBSCRIBER_STATUS)
            .AddParameter(PAR_I_NAME_SUBSCRIBER_STATUS_CHANGE_DT, OracleDbType.Date, sourceColumn:=COL_NAME_SUBSCRIBER_STATUS_CHANGE_DATE)
            .AddParameter(PAR_I_NAME_NEW_USED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_NEW_USED)
            .AddParameter(PAR_I_NAME_MARITALSTATUSID, OracleDbType.Raw, sourceColumn:=COL_NAME_MARITALSTATUS)
            .AddParameter(PAR_I_NAME_VIN_LOCATOR, OracleDbType.Varchar2, sourceColumn:=COL_VIN_LOCATOR)
            .AddParameter(PAR_I_NAME_PERSON_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PERSON_TYPE_ID)
            .AddParameter(PAR_I_NAME_NUM_OF_CONSECUTIVE_PAYMENTS, OracleDbType.Decimal, sourceColumn:=COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS)
            .AddParameter(PAR_I_NAME_UPGRADE_TERM_UOM_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_UPGRADE_TERM_UOM_ID)
            .AddParameter(PAR_I_NAME_UPGRADE_TERM_FROM, OracleDbType.Decimal, sourceColumn:=COL_NAME_UPGRADE_TERM_FROM)
            .AddParameter(PAR_I_NAME_UPGRADE_TERM_TO, OracleDbType.Decimal, sourceColumn:=COL_NAME_UPGRADE_TERM_TO)
            .AddParameter(PAR_I_NAME_UPGRADE_FIXED_TERM, OracleDbType.Decimal, sourceColumn:=COL_NAME_UPGRADE_FIXED_TERM)
            .AddParameter(PAR_I_NAME_CERTIFICATE_SIGNED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CERTIFICATE_SIGNED)
            .AddParameter(PAR_I_NAME_SEPA_MANDATE_SIGNED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SEPA_MANDATE_SIGNED)
            .AddParameter(PAR_I_NAME_CONTRACT_CHECK_COMPLETE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CONTRACT_CHECK_COMPLETE_DATE)
            .AddParameter(PAR_I_NAME_CERT_VERIFICATION_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CERTIFICATE_VERIFICATION_DATE)
            .AddParameter(PAR_I_NAME_SEPA_MANDATE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_SEPA_MANDATE_DATE)
            .AddParameter(PAR_I_NAME_CONTRACT_CHECK_COMPLETE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CONTRACT_CHECK_COMPLETE)
            .AddParameter(PAR_I_NAME_CHECK_SIGNED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CHECK_SIGNED)
            .AddParameter(PAR_I_NAME_CHECK_VERIFICATION_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CHECK_VERIFICATION_DATE)
            .AddParameter(PAR_I_NAME_IS_RESTRICTED, OracleDbType.Varchar2, sourceColumn:=COL_IS_RESTRICTED)
            .AddParameter(PAR_I_NAME_SERVICE_ID, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SERVICE_ID)
            .AddParameter(PAR_I_NAME_SERVICE_START_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_SERVICE_START_DATE)

        End With
    End Sub

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region
#End Region

#Region "StoreProcedures Control"

    ' Execute Store Procedure
    Public Function ExecuteSP(ByVal docType As String, ByVal IdentificationNumber As String) As String
        Dim inputParameters(TOTAL_PARAM_IN) As DBHelper.DBHelperParameter
        Dim outputParameter(TOTAL_PARAM_OUT) As DBHelper.DBHelperParameter
        Dim selectStmt As String = Me.Config("/SQL/VALIDATE_BR_DOCUMENT")

        If Not docType Is Nothing Then
            inputParameters(IN_DOC_TYPE) = New DBHelper.DBHelperParameter(PARAM_NAME_DOCUMENT_TYPE, docType)
        End If

        If Not IdentificationNumber Is Nothing Then
            inputParameters(IN_ID_NUMBER) = New DBHelper.DBHelperParameter(COL_NAME_IDENTIFICATION_NUMBER, IdentificationNumber)
        Else
            inputParameters(IN_ID_NUMBER) = New DBHelper.DBHelperParameter(COL_NAME_IDENTIFICATION_NUMBER, DBNull.Value)
        End If

        outputParameter(OUT_REJ_REASON) = New DBHelper.DBHelperParameter(COL_NAME_RETURN_REASON, GetType(String), 30)
        outputParameter(OUT_REJ_CODE) = New DBHelper.DBHelperParameter(COL_NAME_RETURN_CODE, GetType(Integer))
        outputParameter(OUT_REJ_MSG) = New DBHelper.DBHelperParameter(COL_NAME_RETURN_REJECT_MSG, GetType(String), 50)

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(OUT_REJ_CODE).Value <> 0 Then
            Return outputParameter(OUT_REJ_REASON).Value
        End If
    End Function

    'Public Sub ValidateFile(ByVal oData As Object)
    '    Dim oDealerFileProcessedData As DealerFileProcessedData = CType(oData, DealerFileProcessedData)
    '    Dim selectStmt As String

    '    Select Case oDealerFileProcessedData.fileTypeCode
    '        Case oDealerFileProcessedData.InterfaceTypeCode.CERT
    '            selectStmt = Me.Config("/SQL/VALIDATE_FILE")
    '        Case oDealerFileProcessedData.InterfaceTypeCode.PAYM
    '            selectStmt = Me.Config("/SQL/VALIDATE_PAYMENT")
    '    End Select

    '    Try
    '        ExecuteSP(oDealerFileProcessedData, selectStmt)
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Sub


    Public Sub UpdateVinLocator(ByVal SerialNumber As String)
        Dim updateStmt As String = Me.Config("/SQL/VALIDATE_BR_DOCUMENT")

    End Sub

#End Region

#Region "Functions"
    Public Function LoadCustPersonalHistory(ByVal CertId As Guid, ByVal language_id As Guid) As DataSet

        Dim ds As New DataSet


        Dim selectstmt As String = Me.Config("/SQL/LOAD_CUST_PERSONAL_HISTORY")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, language_id.ToByteArray),
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, language_id.ToByteArray),
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, language_id.ToByteArray),
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, language_id.ToByteArray),
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, language_id.ToByteArray),
                         New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, CertId.ToByteArray)
                        }
        Try
            DBHelper.Fetch(ds, selectstmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadCustAddressHistory(ByVal CertId As Guid) As DataSet

        Dim ds As New DataSet


        Dim selectstmt As String = Me.Config("/SQL/LOAD_CUST_ADDRESS_HISTORY")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, CertId.ToByteArray)
                        }
        Try
            DBHelper.Fetch(ds, selectstmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadCustContactHistory(ByVal CertId As Guid) As DataSet

        Dim ds As New DataSet


        Dim selectstmt As String = Me.Config("/SQL/LOAD_CUST_CONTACT_HISTORY")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, CertId.ToByteArray)
                        }
        Try
            DBHelper.Fetch(ds, selectstmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadCustBankDetailHistory(ByVal CertId As Guid) As DataSet

        Dim ds As New DataSet


        Dim selectstmt As String = Me.Config("/SQL/LOAD_CUST_BANK_DETAIL_HISTORY")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, CertId.ToByteArray)
                        }
        Try
            DBHelper.Fetch(ds, selectstmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetInforceCertsLastestDate() As DataSet
        'Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_INFORCECERT_LASTEST_DATE")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_PREMIUM_TOTALS)
    End Function

    Public Function ValidateLicenseFlag(ByVal VehicleLicenceFlag As String, ByVal CertNumber As String, ByVal CompanyGroupId As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/VALIDATE_VSC_LICENSE_TAG")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(PARAM_NAME_VEHICLE_LICENSE_FLAG, VehicleLicenceFlag.ToUpper.ToString),
                     New DBHelper.DBHelperParameter(PARAM_NAME_CERT_NUMBER, CertNumber.ToUpper.ToString),
                     New DBHelper.DBHelperParameter(PARAM_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetCommissionForEntities(ByVal certId As Guid, ByVal langId As Guid, ByVal commAsOfDate As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_COMMISSION_FOR_ENTITIES")

        Dim parameters(2) As DBHelper.DBHelperParameter
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("v_EntityCommDet", GetType(DataSet))}
        Dim ds As New DataSet

        parameters(0) = New DBHelper.DBHelperParameter("v_cert_id", certId.ToByteArray)
        parameters(1) = New DBHelper.DBHelperParameter("v_language_id", langId.ToByteArray)
        parameters(2) = New DBHelper.DBHelperParameter("v_comm_as_of_date", commAsOfDate)

        Try

            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, "COMMFORENT")
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function IsReverseCancellationEnabled(ByVal certId As Guid) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/IS_REVERSE_CANCELLATION_ENABLED")
        Dim isReplacementPolicy As Boolean

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_cert_id", certId.ToByteArray)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pio_isReplacmentPolicy", GetType(Integer)),
                                                                                                 New DBHelper.DBHelperParameter("pio_isEnabled", GetType(Integer))}
        Try
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
            If outputParameters(0).Value <> 0 Then
                isReplacementPolicy = True
            Else
                isReplacementPolicy = False
            End If

            If outputParameters(1).Value <> 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCertHistory(ByVal cert_Number As String, ByVal dealerId As Guid, ByVal PremChanges As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/CERTIFICATE_HISTORY")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(Me.TOTAL_INPUT_PARAM_CERT_HISTORY) As DBHelper.DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_Dealer_id", dealerId.ToByteArray),
                                                                                            New DBHelper.DBHelperParameter("pi_certificate", cert_Number),
                                                                                            New DBHelper.DBHelperParameter("pi_prem_chngs", PremChanges)}
        outputParameter(Me.PO_CURSOR_CERT_HISTORY) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__CERT_HISTORY, GetType(DataSet))

        Try

            DBHelper.FetchSp(selectStmt, parameters, outputParameter, ds, "GetCertHistory")
            ds.Tables(0).TableName = "GetCertHistory"

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function GetOtherCustomerInfo(ByVal Cert_id As Guid, ByVal IdentificationNumberType As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_OTHER_CUSTOMER_INFO")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(Me.PO_CURSOR_CUSTOMER_INFO) As DBHelper.DBHelperParameter
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_cert_id", Cert_id.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_identification_number_type", IdentificationNumberType)
        inParameters.Add(param)

        outputParameter(Me.PO_CURSOR_CUSTOMER_INFO) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME_CUST_INFO, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetOtherCustomerInfo")
            ds.Tables(0).TableName = "GetOtherCustomerInfo"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function GetOtherCustomerDetails(ByVal CustomerId As Guid, ByVal Lang_Id As Guid, ByVal IdentificationNumberType As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_OTHER_CUSTOMER_DETAILS")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(Me.PO_CURSOR_CUSTOMER_DETAILS) As DBHelper.DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_customer_id", CustomerId.ToByteArray)}
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_customer_id", CustomerId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_lang_id", Lang_Id.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_identification_number_type", IdentificationNumberType)
        inParameters.Add(param)

        outputParameter(Me.PO_CURSOR_CUSTOMER_DETAILS) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME_CUST_DETAILS, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetOtherCustomerDetails")
            ds.Tables(0).TableName = "GetOtherCustomerDetails"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function GetCustomerCurrentBankInfo(ByVal CertId As Guid) As DataSet

        Dim ds As New DataSet


        Dim selectstmt As String = Me.Config("/SQL/GET_CUSTOMER_CURRENTBANKINFO")

        Dim parameters As DBHelper.DBHelperParameter() = {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, CertId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectstmt, Me.BANKINFO_TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    'Public Function GetCustomerCurrentBankInfo(ByVal Cert_id As Guid) As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/GET_CUSTOMER_CURRENTBANKINFO")
    '    Dim ds As DataSet = New DataSet
    '    Dim outputParameter(Me.PO_CURSOR_CUSTOMERBANK_INFO) As DBHelper.DBHelperParameter
    '    Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
    '    Dim param As DBHelper.DBHelperParameter

    '    param = New DBHelper.DBHelperParameter("pi_cert_id", Cert_id.ToByteArray)
    '    inParameters.Add(param)

    '    outputParameter(Me.PO_CURSOR_CUSTOMER_INFO) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME_CUST_INFO, GetType(DataSet))

    '    Try
    '        DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetOtherCustomerInfo")
    '        ds.Tables(0).TableName = "GetCustomerCurrentBankInfo"
    '        Return ds
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Function
    Public Sub UpdateCustomerDetails(ByVal CertId As Guid, ByVal CustomerId As Guid, ByVal SalutationId As Guid, ByVal CustomerFirstName As String, ByVal CustomerMiddleName As String, ByVal CustomerLastName As String, ByVal Modified_By As String,
                                          ByVal Email As String, ByVal HomePhone As String, ByVal IdentificationNumber As String, ByVal IdentificationNumberType As String, ByVal WorkPhone As String,
                                          ByVal MartialStatus As Guid, ByVal Nationality As Guid, ByVal PlaceOfBirth As Guid, ByVal Gender As Guid, ByVal CorporateName As String, ByVal AltFirstName As String, ByVal AltLastName As String, ByVal CityofBirth As String,
                                          Optional ByVal DateOfBirth As DateType = Nothing)
        Dim selectStmt As String = Me.Config("/SQL/UPDATE_CUSTOMER_DETAILS")
        Dim outputParameter(Me.PO_CURSOR_UPDATE_CUSTOMER) As DBHelper.DBHelperParameter
        outputParameter(Me.OUT_REJ_REASON) = New DBHelper.DBHelperParameter("po_reject_reason", GetType(String), 30)
        outputParameter(Me.OUT_REJ_CODE) = New DBHelper.DBHelperParameter("po_reject_code", GetType(Integer))

        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_cert_id", CertId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_customer_id", CustomerId.ToByteArray)
        inParameters.Add(param)

        If CustomerFirstName <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_customer_first_name", CustomerFirstName)
            inParameters.Add(param)
        End If

        If CustomerMiddleName <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_customer_middle_name", CustomerMiddleName)
            inParameters.Add(param)
        End If

        If CustomerLastName <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_customer_last_name", CustomerLastName)
            inParameters.Add(param)
        End If

        If Email <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_email", Email)
            inParameters.Add(param)
        End If

        If HomePhone <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_homephone", HomePhone)
            inParameters.Add(param)
        End If


        If IdentificationNumberType <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_identification_number_type", IdentificationNumberType)
            inParameters.Add(param)
        End If

        If IdentificationNumber <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_identification_number", IdentificationNumber)
            inParameters.Add(param)
        End If


        If WorkPhone <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_workphone", WorkPhone)
            inParameters.Add(param)
        End If


        If Not DateOfBirth = Nothing Then
            Dim strDateOfBirth As String = DateOfBirth.Value.ToString("MM/dd/yyyy")
            param = New DBHelper.DBHelperParameter("pi_dateofbirth", strDateOfBirth)
            inParameters.Add(param)
        End If

        If MartialStatus <> Guid.Empty Then
            param = New DBHelper.DBHelperParameter("pi_martialstatus_id", MartialStatus.ToByteArray)
            inParameters.Add(param)
        End If

        If Nationality <> Guid.Empty Then
            param = New DBHelper.DBHelperParameter("pi_nationality_id", Nationality.ToByteArray)
            inParameters.Add(param)
        End If

        If PlaceOfBirth <> Guid.Empty Then
            param = New DBHelper.DBHelperParameter("pi_placeofbirth_id", PlaceOfBirth.ToByteArray)
            inParameters.Add(param)
        End If

        If Gender <> Guid.Empty Then
            param = New DBHelper.DBHelperParameter("pi_gender_id", Gender.ToByteArray)
            inParameters.Add(param)
        End If

        If SalutationId <> Guid.Empty Then
            param = New DBHelper.DBHelperParameter("pi_salutation_id", SalutationId.ToByteArray)
            inParameters.Add(param)
        End If


        param = New DBHelper.DBHelperParameter("pi_modified_by", Modified_By)
        inParameters.Add(param)


        If CorporateName <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_corporate_name", CorporateName)
            inParameters.Add(param)
        End If

        If AltFirstName <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_customer_alt_first_name", AltFirstName)
            inParameters.Add(param)
        End If

        If AltLastName <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_customer_alt_last_name", AltLastName)
            inParameters.Add(param)
        End If

        If CityofBirth <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_city_of_birth", CityofBirth)
            inParameters.Add(param)
        End If

        Try
            DBHelper.ExecuteSpParamBindByName(selectStmt, inParameters.ToArray, outputParameter)
            If outputParameter(OUT_REJ_CODE).Value <> 0 Then
                Throw New StoredProcedureGeneratedException("Update Customer ERROR : ", outputParameter(OUT_REJ_REASON).Value)
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Function GetCertInstallmentHistory(ByVal Cert_id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/CERT_INSTALLMENT_HISTORY")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_cert_id", Cert_id.ToByteArray)}
        outputParameter(Me.PO_CURSOR_CERT_HISTORY) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME__CERT_HISTORY, GetType(DataSet))

        Try

            DBHelper.FetchSp(selectStmt, parameters, outputParameter, ds, "GetCertInstallmentHistory")
            ds.Tables(0).TableName = "GetCertInstallmentHistory"

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetParentCertId(ByVal Cert_id As Guid) As Guid
        Dim selectStmt As String = Me.Config("/SQL/GET_PARENT_CERT_ID")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_cert_id", Cert_id.ToByteArray)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_parent_cert_id", GetType(Guid))}

        Try

            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
            If outputParameters(0).Value Is Nothing Then
                Return Nothing
            Else
                Return Guid.Parse(outputParameters(0).Value.ToString)
            End If
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

        Return New DBHelper.DBHelperParameter(name, value, value.GetType)

    End Function

#End Region


#Region "Web Service Methods"

    ''' <summary>
    ''' Get Certificate Based On Dealer, Cert Number,Billing ACC Number 
    ''' </summary>
    ''' <param name="dealer">Dealer Code</param>
    ''' <param name="serialNumber">CertificateNumbere</param>
    ''' ' <param name="serialNumber">BillingACCNumber</param>

    ''' <returns><see cref="DataSet" /></returns>
    ''' <remarks></remarks>
    Public Function GetCertInfo(ByVal CertificateNumber As String, ByVal DealerCode As String, ByVal BillingAccountNumber As String,
                                ByVal UpgradeFlag As String, Optional ByVal CompanyCode As String = "", Optional ByVal MobileNumber As String = "") As Guid
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/SEARCH_CERT_INFO")
        Dim certId As Nullable(Of Guid)
        Try


            Dim inputParameters(5) As DBHelperParameter
            Dim outputParameter(2) As DBHelperParameter

            inputParameters(0) = New DBHelperParameter("pi_cert_num", CertificateNumber, GetType(String))
            inputParameters(1) = New DBHelperParameter("pi_billing_account_number", BillingAccountNumber, GetType(String))
            inputParameters(2) = New DBHelperParameter("pi_dealer_code", DealerCode, GetType(String))
            inputParameters(3) = New DBHelperParameter("pi_upgrade_flg", UpgradeFlag, GetType(String))
            inputParameters(4) = New DBHelperParameter("pi_company_code", CompanyCode, GetType(String))
            inputParameters(5) = New DBHelperParameter("pi_mobile_number", MobileNumber, GetType(String))

            outputParameter(0) = New DBHelperParameter("po_cert_id", GetType(String), 32)
            outputParameter(1) = New DBHelperParameter("po_return", GetType(Integer), 32)
            outputParameter(2) = New DBHelperParameter("po_exception_msg", GetType(String), 500)

            'Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)


            If CType(outputParameter(1).Value, Integer) <> 0 Then
                Throw New StoredProcedureGeneratedException("Search Certificate ERROR : ", outputParameter(1).Value)
            Else


                If (Not outputParameter(0).Value Is Nothing) Then

                    If (outputParameter(0).Value.GetType Is GetType(String)) Then

                        certId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(outputParameter(0).Value))

                    End If

                End If

            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return certId
    End Function

    Public Function GetCertInfoBySerialNumber(ByVal DealerCode As String, ByVal SerialNumber As String, ByVal UpgradeFlag As String) As Guid
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/SEARCH_CERT_BY_SERIAL_NUMBER")
        Dim certId As Nullable(Of Guid)
        Try


            Dim inputParameters(2) As DBHelperParameter
            Dim outputParameter(2) As DBHelperParameter

            inputParameters(0) = New DBHelperParameter("pi_dealer_code", DealerCode, GetType(String))
            inputParameters(1) = New DBHelperParameter("pi_serial_number", SerialNumber, GetType(String))
            inputParameters(2) = New DBHelperParameter("pi_upgrade_flg", UpgradeFlag, GetType(String))

            outputParameter(0) = New DBHelperParameter("po_cert_id", GetType(String), 32)
            outputParameter(1) = New DBHelperParameter("po_return", GetType(Integer), 32)
            outputParameter(2) = New DBHelperParameter("po_exception_msg", GetType(String), 500)

            'Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)


            If CType(outputParameter(1).Value, Integer) <> 0 Then
                Throw New StoredProcedureGeneratedException("Search Certificate ERROR : ", outputParameter(1).Value)
            Else


                If (Not outputParameter(0).Value Is Nothing) Then

                    If (outputParameter(0).Value.GetType Is GetType(String)) Then

                        certId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(outputParameter(0).Value))

                    End If

                End If

            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return certId
    End Function
    Public Function GetCertInfoBySrNrIdentNrUpgrdDate(ByVal DealerCode As String, ByVal SerialNumber As String, IdentificationNumber As String, UpgradeDate As Date) As Guid

        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/SEARCH_CERT_BY_SRNR_IDENTNR_UPGRDDATE")
        Dim certId As Nullable(Of Guid)
        Try


            Dim inputParameters(3) As DBHelperParameter
            Dim outputParameter(2) As DBHelperParameter

            inputParameters(0) = New DBHelperParameter("pi_dealer_code", DealerCode, GetType(String))
            inputParameters(1) = New DBHelperParameter("pi_serial_number", SerialNumber, GetType(String))
            inputParameters(2) = New DBHelperParameter("pi_identification_number", IdentificationNumber, GetType(String))
            inputParameters(3) = New DBHelperParameter("pi_upgrade_date", UpgradeDate, GetType(Date))

            outputParameter(0) = New DBHelperParameter("po_cert_id", GetType(String), 32)
            outputParameter(1) = New DBHelperParameter("po_return", GetType(Integer), 32)
            outputParameter(2) = New DBHelperParameter("po_exception_msg", GetType(String), 500)

            'Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

            Select Case CType(outputParameter(1).Value, Integer)
                Case 0
                    If (Not outputParameter(0).Value Is Nothing) Then

                        If (outputParameter(0).Value.GetType Is GetType(String)) Then

                            certId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(outputParameter(0).Value))

                        End If

                    End If
                Case 300
                    Throw New StoredProcedureGeneratedException("Duplicate_Certificate_Error", outputParameter(1).Value)
                Case 350
                    Throw New StoredProcedureGeneratedException("Coverage_Not_Found_Error", outputParameter(1).Value)
                Case Else
                    Throw New StoredProcedureGeneratedException("Search Certificate ERROR : ", outputParameter(1).Value)
            End Select

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return certId
    End Function
#End Region

#Region "GW Related"
    Public Function GWSearchCertificate(ByVal pCompanyCodes As String, ByVal pCertificateNumber As String, ByVal pCustomerName As String, ByVal pWorkPhone As String,
                                        ByVal pHomePhone As String, ByVal pAccountNumber As String, ByVal pServiceLineNumber As String, ByVal pTaxId As String,
                                        ByVal pEmail As String, ByVal pPurchaseInvoiceNumber As String, ByVal pAddress As String, ByVal pAddress2 As String,
                                        ByVal pAddress3 As String, ByVal pCountry As String, ByVal pState As String, ByVal pCity As String,
                                        ByVal pZipCode As String, ByVal pSerialNumber As String, ByVal pIMEINumber As String, ByVal pCertStatus As String,
                                        ByVal pNumberOfRecords As Integer) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GW_CERTIFICATE_SEARCH")
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_cursor_Result", GetType(DataSet))}
        Dim ds As New DataSet
        Dim tbl As String = "SEARCH_RESULT"

        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_CompanyCodeList", pCompanyCodes)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_NumOfRecord", pNumberOfRecords)
        inParameters.Add(param)

        If pCertificateNumber <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_CertNum", pCertificateNumber)
            inParameters.Add(param)
        End If

        If pCustomerName <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_CustomerName", pCustomerName)
            inParameters.Add(param)
        End If

        If pWorkPhone <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_WorkPhone", pWorkPhone)
            inParameters.Add(param)
        End If

        If pHomePhone <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_HomePhone", pHomePhone)
            inParameters.Add(param)
        End If

        If pAccountNumber <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_AccountNumber", pAccountNumber)
            inParameters.Add(param)
        End If

        If pServiceLineNumber <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_ServiceLineNumber", pServiceLineNumber)
            inParameters.Add(param)
        End If

        If pTaxId <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_TaxId", pTaxId)
            inParameters.Add(param)
        End If

        If pEmail <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_Email", pEmail)
            inParameters.Add(param)
        End If

        If pPurchaseInvoiceNumber <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_InvoiceNumber", pPurchaseInvoiceNumber)
            inParameters.Add(param)
        End If

        If pAddress <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_Address1", pAddress)
            inParameters.Add(param)
        End If

        If pAddress2 <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_Address2", pAddress2)
            inParameters.Add(param)
        End If

        If pAddress3 <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_Address3", pAddress3)
            inParameters.Add(param)
        End If

        If pCountry <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_Country", pCountry)
            inParameters.Add(param)
        End If

        If pState <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_State", pState)
            inParameters.Add(param)
        End If

        If pCity <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_City", pCity)
            inParameters.Add(param)
        End If

        If pZipCode <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_ZipCode", pZipCode)
            inParameters.Add(param)
        End If

        If pSerialNumber <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_SerialNumber", pSerialNumber)
            inParameters.Add(param)
        End If

        If pIMEINumber <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_IMEINumber", pIMEINumber)
            inParameters.Add(param)
        End If

        If pCertStatus <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_CertStatus", pCertStatus)
            inParameters.Add(param)
        End If
        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outParameters, ds, tbl, True)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function GetCertExtensionsInfo(ByVal Cert_id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_CERT_EXT_INFO")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_cert_id", Cert_id.ToByteArray)}
        outputParameter(Me.PO_CURSOR_CERT_HISTORY) = New DBHelper.DBHelperParameter("po_cert_ext_info", GetType(DataSet))

        Try

            DBHelper.FetchSp(selectStmt, parameters, outputParameter, ds, "GetCertExtnData")
            ds.Tables(0).TableName = "GetCertExtnData"

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetCertUpgradeExtensionsInfo(ByVal Cert_id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_CERT_UPG_EXT_INFO")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_cert_id", Cert_id.ToByteArray)}
        outputParameter(Me.PO_CURSOR_CERT_HISTORY) = New DBHelper.DBHelperParameter("po_cert_upg_ext_info", GetType(DataSet))

        Try

            DBHelper.FetchSp(selectStmt, parameters, outputParameter, ds, "GetCertUpgExtnData")
            ds.Tables(0).TableName = "GetCertUpgExtnData"

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetFraudulentCertExtensions(ByVal certId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_FRAUD_CERT_EXT")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("certId", certId.ToByteArray)}
        outputParameter(0) = New DBHelper.DBHelperParameter("fraudulentCertExtensions", GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, parameters, outputParameter, ds, "GetFraudCertExtns")
            ds.Tables(0).TableName = "GetFraudCertExtns"

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadCallerList(ByVal CertID As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_CERT_CALLER")
        Dim ds As DataSet = New DataSet
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_CERT_ID, CertID.ToByteArray)}

        Try
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadCallerListForCase(ByVal CaseID As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_CASE_CALLER")
        Dim ds As DataSet = New DataSet
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                                    {New OracleParameter("case_id", CaseID.ToByteArray)}

        Try
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetCertPaymentPassedDueExtInfo(ByVal Cert_id As Guid) As Integer
        Dim selectStmt As String = Me.Config("/SQL/GET_CERT_PYMT_PASSED_DUE_EXT_INFO")
        Dim ds As DataSet = New DataSet
        Dim paymentPasseddue As Integer = 0
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_cert_id", Cert_id.ToByteArray)}
        outputParameter(Me.PO_CURSOR_CERT_HISTORY) = New DBHelper.DBHelperParameter("po_cert_ppd_ext_info", GetType(DataSet))

        Try

            DBHelper.FetchSp(selectStmt, parameters, outputParameter, ds, "GetCertPymtPassedDueExtnData")
            ds.Tables(0).TableName = "GetCertPymtPassedDueExtnData"

            If ds.Tables(0).Rows.Count > 0 Then
                paymentPasseddue = Convert.ToInt16(ds.Tables(0).Rows(0)("field_value").ToString())
            End If

            Return paymentPasseddue

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Certificate Extended Fields"
    Public Function GetCertExtFieldsList(ByVal certId As Guid, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_CERT_EXT_FIELDS_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray),
                 New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)
                }

        outputParameter(PO_CURSOR_CERT_HISTORY) = New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, parameters, outputParameter, ds, "GetCertExtFieldsList")
            ds.Tables(0).TableName = "GetCertExtFieldsList"

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "SFR Related"
    Public Function SFRSearchCertificate(ByVal pCompanyCode As String, ByVal pDealerCode As String, ByVal pDealerGrp As String, ByVal pCustomerFirstName As String, ByVal pCustomerLastName As String,
                                                ByVal pPhoneNumber As String, ByVal pEmail As String, ByVal pPostalCode As String, ByVal pAccountNumber As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/SFR_CERTIFICATE_SEARCH")
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_cursor_Result", GetType(DataSet))}
        Dim ds As New DataSet
        Dim tbl As String = "SEARCH_RESULT"

        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_company_code", pCompanyCode)
        inParameters.Add(param)
        'param = New DBHelper.DBHelperParameter("pi_NumOfRecord", pNumberOfRecords)
        'inParameters.Add(param)
        If pDealerCode <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_dealer_code", pDealerCode)
            inParameters.Add(param)
        End If

        If pDealerGrp <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_dealer_group", pDealerGrp)
            inParameters.Add(param)
        End If

        If pCustomerFirstName <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_first_name", pCustomerFirstName)
            inParameters.Add(param)
        End If

        If pCustomerLastName <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_last_name", pCustomerLastName)
            inParameters.Add(param)
        End If

        If pPhoneNumber <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_phone_number", pPhoneNumber)
            inParameters.Add(param)
        End If

        If pEmail <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_email", pEmail)
            inParameters.Add(param)
        End If

        If pPostalCode <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_postal_code", pPostalCode)
            inParameters.Add(param)
        End If

        If pAccountNumber <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_account_number", pAccountNumber)
            inParameters.Add(param)
        End If

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outParameters, ds, tbl, True)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Sub UpdateCertExtension(certextId As Guid, fieldValue As String, Modified_By As String)
        Dim updateStmt As String = Me.Config("/SQL/UPDATE_CERT_EXT_FIELDS_VALUE")
        Dim outputParameter(Me.PO_CURSOR_UPDATE_CUSTOMER) As DBHelper.DBHelperParameter
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter
        param = New DBHelper.DBHelperParameter("pi_cert_ext_id", certextId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_modified_by", Modified_By)
        inParameters.Add(param)

        If fieldValue <> String.Empty Then
            param = New DBHelper.DBHelperParameter("pi_field_value", fieldValue)
            inParameters.Add(param)
        End If

        Try
            DBHelper.ExecuteSpParamBindByName(updateStmt, inParameters.ToArray, Nothing)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region
End Class