'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (11/11/2013)********************


Public Class DealerReconWrkDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Modified
    Public Const TABLE_NAME As String = "ELP_DEALER_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = COL_NAME_DEALER_RECON_WRK_ID

    Public Const COL_NAME_DEALER_RECON_WRK_ID As String = "dealer_recon_wrk_id"
    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_CERTIFICATE_LOADED As String = "certificate_loaded"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_ITEM_CODE As String = "item_code"
    Public Const COL_NAME_ITEM As String = "item"
    Public Const COL_NAME_PRODUCT_PRICE As String = "product_price"
    Public Const COL_NAME_MAN_WARRANTY As String = "man_warranty"
    Public Const COL_NAME_EXT_WARRANTY As String = "ext_warranty"
    Public Const COL_NAME_PRICE_POL As String = "price_pol"
    Public Const COL_NAME_SR As String = "sr"
    Public Const COL_NAME_BRANCH_CODE As String = "branch_code"
    Public Const COL_NAME_NUMBER_COMP As String = "number_comp"
    Public Const COL_NAME_DATE_COMP As String = "date_comp"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_IDENTIFICATION_NUMBER As String = "identification_number"
    Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_ZIP As String = "zip"
    Public Const COL_NAME_STATE_PROVINCE As String = "state_province"
    Public Const COL_NAME_HOME_PHONE As String = "home_phone"
    Public Const COL_NAME_CURRENCY As String = "currency"
    Public Const COL_NAME_EXTWARR_SALEDATE As String = "extwarr_saledate"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_TYPE_PAYMENT As String = "type_payment"
    Public Const COL_NAME_NUMBER_MONTHLY As String = "number_monthly"
    Public Const COL_NAME_MONTHLY_PAYMENT As String = "monthly_payment"
    Public Const COL_NAME_COEF_FIN As String = "coef_fin"
    Public Const COL_NAME_CURRENCY_PAYMENT As String = "currency_payment"
    Public Const COL_NAME_FINANCED_VALUE As String = "financed_value"
    Public Const COL_NAME_CREDIT_CARD_TYPE As String = "credit_card_type"
    Public Const COL_NAME_CREDIT_CARD As String = "credit_card"
    Public Const COL_NAME_CREDIT_AUTHORIZATION_NUMBER As String = "credit_authorization_number"
    Public Const COL_NAME_CANCELLATION_CODE As String = "cancellation_code"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_IMEI_NUMBER As String = "imei_number"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_NEW_PRODUCT_CODE As String = "new_product_code"
    Public Const COL_NAME_MULTIPLE_DURATIONS As String = "multiple_durations"
    Public Const COL_NAME_ADDRESS2 As String = "address2"
    Public Const COL_NAME_OLD_NUMBER As String = "old_number"
    Public Const COL_NAME_ENTIRE_RECORD As String = "entire_record"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_DOCUMENT_TYPE As String = "document_type"
    Public Const COL_NAME_DOCUMENT_AGENCY As String = "document_agency"
    Public Const COL_NAME_DOCUMENT_ISSUE_DATE As String = "document_issue_date"
    Public Const COL_NAME_RG_NUMBER As String = "rg_number"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_ID_TYPE As String = "id_type"
    Public Const COL_NAME_SALES_TAX As String = "sales_tax"
    Public Const COL_NAME_CUST_COUNTRY As String = "cust_country"
    Public Const COL_NAME_COUNTRY_PURCH As String = "country_purch"
    Public Const COL_NAME_ISO_CODE As String = "iso_code"
    Public Const COL_NAME_OLITA_TRANS_ID As String = "olita_trans_id"
    Public Const COL_NAME_EMAIL As String = "email"
    Public Const COL_NAME_WORK_PHONE As String = "work_phone"
    Public Const COL_NAME_OCCUPATION As String = "Occupation"
    Public Const COL_NAME_ITEM2_MANUFACTURER As String = "item2_manufacturer"
    Public Const COL_NAME_ITEM2_MODEL As String = "item2_model"
    Public Const COL_NAME_ITEM2_SERIAL_NUMBER As String = "item2_serial_number"
    Public Const COL_NAME_ITEM2_DESCRIPTION As String = "item2_description"
    Public Const COL_NAME_ITEM2_PRICE As String = "item2_price"
    Public Const COL_NAME_ITEM2_BUNDLE_VAL As String = "item2_bundle_val"
    Public Const COL_NAME_ITEM2_MAN_WARRANTY As String = "item2_man_warranty"
    Public Const COL_NAME_ITEM3_MANUFACTURER As String = "item3_manufacturer"
    Public Const COL_NAME_ITEM3_MODEL As String = "item3_model"
    Public Const COL_NAME_ITEM3_SERIAL_NUMBER As String = "item3_serial_number"
    Public Const COL_NAME_ITEM3_DESCRIPTION As String = "item3_description"
    Public Const COL_NAME_ITEM3_PRICE As String = "item3_price"
    Public Const COL_NAME_ITEM3_BUNDLE_VAL As String = "item3_bundle_val"
    Public Const COL_NAME_ITEM3_MAN_WARRANTY As String = "item3_man_warranty"
    Public Const COL_NAME_ITEM4_MANUFACTURER As String = "item4_manufacturer"
    Public Const COL_NAME_ITEM4_MODEL As String = "item4_model"
    Public Const COL_NAME_ITEM4_SERIAL_NUMBER As String = "item4_serial_number"
    Public Const COL_NAME_ITEM4_DESCRIPTION As String = "item4_description"
    Public Const COL_NAME_ITEM4_PRICE As String = "item4_price"
    Public Const COL_NAME_ITEM4_BUNDLE_VAL As String = "item4_bundle_val"
    Public Const COL_NAME_ITEM4_MAN_WARRANTY As String = "item4_man_warranty"
    Public Const COL_NAME_ITEM5_MANUFACTURER As String = "item5_manufacturer"
    Public Const COL_NAME_ITEM5_MODEL As String = "item5_model"
    Public Const COL_NAME_ITEM5_SERIAL_NUMBER As String = "item5_serial_number"
    Public Const COL_NAME_ITEM5_DESCRIPTION As String = "item5_description"
    Public Const COL_NAME_ITEM5_PRICE As String = "item5_price"
    Public Const COL_NAME_ITEM5_BUNDLE_VAL As String = "item5_bundle_val"
    Public Const COL_NAME_ITEM5_MAN_WARRANTY As String = "item5_man_warranty"
    Public Const COL_NAME_ITEM6_MANUFACTURER As String = "item6_manufacturer"
    Public Const COL_NAME_ITEM6_MODEL As String = "item6_model"
    Public Const COL_NAME_ITEM6_SERIAL_NUMBER As String = "item6_serial_number"
    Public Const COL_NAME_ITEM6_DESCRIPTION As String = "item6_description"
    Public Const COL_NAME_ITEM6_PRICE As String = "item6_price"
    Public Const COL_NAME_ITEM6_BUNDLE_VAL As String = "item6_bundle_val"
    Public Const COL_NAME_ITEM6_MAN_WARRANTY As String = "item6_man_warranty"
    Public Const COL_NAME_ITEM7_MANUFACTURER As String = "item7_manufacturer"
    Public Const COL_NAME_ITEM7_MODEL As String = "item7_model"
    Public Const COL_NAME_ITEM7_SERIAL_NUMBER As String = "item7_serial_number"
    Public Const COL_NAME_ITEM7_DESCRIPTION As String = "item7_description"
    Public Const COL_NAME_ITEM7_PRICE As String = "item7_price"
    Public Const COL_NAME_ITEM7_BUNDLE_VAL As String = "item7_bundle_val"
    Public Const COL_NAME_ITEM7_MAN_WARRANTY As String = "item7_man_warranty"
    Public Const COL_NAME_ITEM8_MANUFACTURER As String = "item8_manufacturer"
    Public Const COL_NAME_ITEM8_MODEL As String = "item8_model"
    Public Const COL_NAME_ITEM8_SERIAL_NUMBER As String = "item8_serial_number"
    Public Const COL_NAME_ITEM8_DESCRIPTION As String = "item8_description"
    Public Const COL_NAME_ITEM8_PRICE As String = "item8_price"
    Public Const COL_NAME_ITEM8_BUNDLE_VAL As String = "item8_bundle_val"
    Public Const COL_NAME_ITEM8_MAN_WARRANTY As String = "item8_man_warranty"
    Public Const COL_NAME_ITEM9_MANUFACTURER As String = "item9_manufacturer"
    Public Const COL_NAME_ITEM9_MODEL As String = "item9_model"
    Public Const COL_NAME_ITEM9_SERIAL_NUMBER As String = "item9_serial_number"
    Public Const COL_NAME_ITEM9_DESCRIPTION As String = "item9_description"
    Public Const COL_NAME_ITEM9_PRICE As String = "item9_price"
    Public Const COL_NAME_ITEM9_BUNDLE_VAL As String = "item9_bundle_val"
    Public Const COL_NAME_ITEM9_MAN_WARRANTY As String = "item9_man_warranty"
    Public Const COL_NAME_ITEM10_MANUFACTURER As String = "item10_manufacturer"
    Public Const COL_NAME_ITEM10_MODEL As String = "item10_model"
    Public Const COL_NAME_ITEM10_SERIAL_NUMBER As String = "item10_serial_number"
    Public Const COL_NAME_ITEM10_DESCRIPTION As String = "item10_description"
    Public Const COL_NAME_ITEM10_PRICE As String = "item10_price"
    Public Const COL_NAME_ITEM10_BUNDLE_VAL As String = "item10_bundle_val"
    Public Const COL_NAME_ITEM10_MAN_WARRANTY As String = "item10_man_warranty"
    Public Const COL_NAME_ITEM11_MANUFACTURER As String = "item11_manufacturer"
    Public Const COL_NAME_ITEM11_MODEL As String = "item11_model"
    Public Const COL_NAME_ITEM11_SERIAL_NUMBER As String = "item11_serial_number"
    Public Const COL_NAME_ITEM11_DESCRIPTION As String = "item11_description"
    Public Const COL_NAME_ITEM11_PRICE As String = "item11_price"
    Public Const COL_NAME_ITEM11_BUNDLE_VAL As String = "item11_bundle_val"
    Public Const COL_NAME_ITEM11_MAN_WARRANTY As String = "item11_man_warranty"
    Public Const COL_NAME_NEW_BRANCH_CODE As String = "new_branch_code"
    Public Const COL_NAME_BILLING_FREQUENCY As String = "billing_frequency"
    Public Const COL_NAME_NUMBER_OF_INSTALLMENTS As String = "number_of_installments"
    Public Const COL_NAME_INSTALLMENT_AMOUNT As String = "installment_amount"
    Public Const COL_NAME_BANK_ACCT_OWNER_NAME As String = "bank_acct_owner_name"
    Public Const COL_NAME_REJECT_MSG_PARMS As String = "reject_msg_parms"
    Public Const COL_NAME_BANK_ACCOUNT_NUMBER As String = "bank_account_number"
    Public Const COL_NAME_BANK_RTN_NUMBER As String = "bank_rtn_number"
    Public Const COL_NAME_SALUTATION As String = "salutation"
    Public Const COL_NAME_BANK_BRANCH_NUMBER As String = "bank_branch_number"
    Public Const COL_NAME_PAYMENT_INSTRUMENT_TYPE As String = "payment_instrument_type"
    Public Const COL_NAME_CREDIT_CARD_EXPIRATION As String = "credit_card_expiration"
    Public Const COL_NAME_NEXT_DUE_DATE As String = "next_due_date"
    Public Const COL_NAME_LANGUAGE_PREF As String = "language_pref"
    Public Const COL_NAME_POST_PRE_PAID As String = "post_pre_paid"
    Public Const COL_NAME_DATE_PAID_FOR As String = "date_paid_for"
    Public Const COL_NAME_MARKETING_PROMO_SER As String = "marketing_promo_ser"
    Public Const COL_NAME_MARKETING_PROMO_NUM As String = "marketing_promo_num"
    Public Const COL_NAME_MEMBERSHIP_NUMBER As String = "membership_number"
    Public Const COL_NAME_ADDRESS3 As String = "address3"
    Public Const COL_NAME_SUBSCRIBER_STATUS As String = "subscriber_status"
    Public Const COL_NAME_BILLING_PLAN As String = "billing_plan"
    Public Const COL_NAME_BILLING_CYCLE As String = "billing_cycle"
    Public Const COL_NAME_SERVICE_LINE_NUMBER As String = "service_line_number"
    Public Const COL_NAME_ORIGINAL_RETAIL_PRICE As String = "original_retail_price"
    Public Const COL_NAME_MOBILE_TYPE As String = "mobile_type"
    Public Const COL_NAME_FIRST_USE_DATE As String = "first_use_date"
    Public Const COL_NAME_LAST_USE_DATE As String = "last_use_date"
    Public Const COL_NAME_SIM_CARD_NUMBER As String = "sim_card_number"
    Public Const COL_NAME_REGION As String = "region"
    Public Const COL_NAME_SKU_NUMBER As String = "sku_number"
    Public Const COL_NAME_MEMBERSHIP_TYPE As String = "membership_type"
    Public Const COL_NAME_SUBSCRIBER_STATUS_CHANGE_DATE As String = "subscriber_status_change_date"
    Public Const COL_NAME_LINES_ON_ACCOUNT As String = "lines_on_account"
    Public Const COL_NAME_CESS_OFFICE As String = "cess_office"
    Public Const COL_NAME_CESS_SALESREP As String = "cess_salesrep"
    Public Const COL_NAME_BUSINESSLINE As String = "businessline"
    Public Const COL_NAME_SALES_DEPARTMENT As String = "sales_department"
    Public Const COL_NAME_SUSPENDED_REASON As String = "suspended_reason"
    Public Const COL_NAME_LINKED_CERT_NUMBER As String = "linked_cert_number"
    Public Const COL_NAME_ADDITIONAL_INFO As String = "additional_info"
    Public Const COL_NAME_CREDITCARD_LAST_FOUR_DIGIT As String = "creditcard_last_four_digit"
    Public Const COL_NAME_CANCEL_COMMENT_TYPE_CODE As String = "cancel_comment_type_code"
    Public Const COL_NAME_CANCELLATION_COMMENT As String = "cancellation_comment"
    Public Const COL_NAME_SUBSCRIBER_STATUS_ID As String = "subscriber_status_id"
    Public Const COL_NAME_MEMBERSHIP_TYPE_ID As String = "membership_type_id"
    Public Const COL_NAME_STATE_PROVINCE_ID As String = "state_province_id"
    Public Const COL_NAME_EXTERNAL_REGISTRATION_DATE As String = "external_registration_date"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_INPUT_DATE As String = "input_date"
    Public Const COL_NAME_FINANCED_AMOUNT As String = "financed_amount"
    Public Const COL_NAME_FINANCED_FREQUENCY As String = "financed_frequency"
    Public Const COL_NAME_FINANCED_INSTALLMENT_NUMBER As String = "financed_installment_number"
    Public Const COL_NAME_FINANCED_INSTALLMENT_AMOUNT As String = "financed_installment_amount"
    'REQ-5619 - Start
    Public Const COL_NAME_GENDER As String = "gender"
    Public Const COL_NAME_MARITAL_STATUS As String = "marital_status"
    Public Const COL_NAME_NATIONALITY As String = "nationality"
    Public Const COL_NAME_BIRTH_DATE As String = "birth_date"
    'REQ-5619 - End

    'REQ-5657 - Start
    Public Const COL_NAME_FINANCE_DATE As String = "finance_date"
    Public Const COL_NAME_DOWN_PAYMENT As String = "down_payment"
    Public Const COL_NAME_ADVANCE_PAYMENT As String = "advance_payment"
    Public Const COL_NAME_UPGRADE_TERM As String = "upgrade_term"
    Public Const COL_NAME_BILLING_ACCOUNT_NUMBER As String = "billing_account_number"
    'REQ-5657 - End

    'REQ-5681 - Start
    Public Const COL_NAME_PLACE_OF_BIRTH As String = "place_of_birth"
    Public Const COL_NAME_PERSON_TYPE As String = "person_type"
    Public Const COL_NAME_CUIT_CUIL As String = "cuit_cuil"
    'REQ-5681 - End

    Public Const COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS As String = "num_of_consecutive_payments"
    Public Const COL_NAME_DEALER_CURRENT_PLAN_CODE As String = "dealer_current_plan_code"
    Public Const COL_NAME_DEALER_SCHEDULED_PLAN_CODE As String = "dealer_scheduled_plan_code"
    Public Const COL_NAME_DEALER_UPDATE_REASON As String = "dealer_update_reason"

    Public Const COL_NAME_LOAN_CODE As String = "loan_code"
    Public Const COL_NAME_PAYMENT_SHIFT_NUMBER As String = "payment_shift_number"
    Public Const COL_NAME_IS_RECON_RECORD_PARENT As String = "is_recon_record_parent"
    Public Const COL_NAME_REFUND_AMOUNT As String = "refund_amount"
    Public Const COL_NAME_DEVICE_TYPE As String = "device_type"

    Public Const COL_NAME_UPGRADE_DATE As String = "upgrade_date"
    Public Const COL_NAME_VOUCHER_NUMBER As String = "voucher_number"
    Public Const COL_NAME_RMA As String = "rma"
    Public Const COL_NAME_APPLECARE_FEE As String = "applecare_fee"
    Public Const PAR_I_NAME_DEALER_RECON_WRK_ID As String = "pi_dealer_recon_wrk_id"
    Public Const PAR_I_NAME_DEALERFILE_PROCESSED_ID As String = "pi_dealerfile_processed_id"
    Public Const PAR_I_NAME_REJECT_REASON As String = "pi_reject_reason"
    Public Const PAR_I_NAME_PARENT_FILE As String = "pi_is_split_file"
    Public Const PAR_I_PAGE_INDEX As String = "pi_page_index"
    Public Const PAR_I_PAGE_SIZE As String = "pi_page_size"
    Public Const PAR_I_NAME_CERTIFICATE_LOADED As String = "pi_certificate_loaded"
    Public Const PAR_I_NAME_RECORD_TYPE As String = "pi_record_type"
    Public Const PAR_I_NAME_ITEM_CODE As String = "pi_item_code"
    Public Const PAR_I_NAME_ITEM As String = "pi_item"
    Public Const PAR_I_NAME_PRODUCT_PRICE As String = "pi_product_price"
    Public Const PAR_I_NAME_MAN_WARRANTY As String = "pi_man_warranty"
    Public Const PAR_I_NAME_EXT_WARRANTY As String = "pi_ext_warranty"
    Public Const PAR_I_NAME_PRICE_POL As String = "pi_price_pol"
    Public Const PAR_I_NAME_SR As String = "pi_sr"
    Public Const PAR_I_NAME_BRANCH_CODE As String = "pi_branch_code"
    Public Const PAR_I_NAME_NUMBER_COMP As String = "pi_number_comp"
    Public Const PAR_I_NAME_DATE_COMP As String = "pi_date_comp"
    Public Const PAR_I_NAME_CERTIFICATE As String = "pi_certificate"
    Public Const PAR_I_NAME_IDENTIFICATION_NUMBER As String = "pi_identification_number"
    Public Const PAR_I_NAME_CUSTOMER_NAME As String = "pi_customer_name"
    Public Const PAR_I_NAME_ADDRESS1 As String = "pi_address1"
    Public Const PAR_I_NAME_CITY As String = "pi_city"
    Public Const PAR_I_NAME_ZIP As String = "pi_zip"
    Public Const PAR_I_NAME_STATE_PROVINCE As String = "pi_state_province"
    Public Const PAR_I_NAME_HOME_PHONE As String = "pi_home_phone"
    Public Const PAR_I_NAME_CURRENCY As String = "pi_currency"
    Public Const PAR_I_NAME_EXTWARR_SALEDATE As String = "pi_extwarr_saledate"
    Public Const PAR_I_NAME_PRODUCT_CODE As String = "pi_product_code"
    Public Const PAR_I_NAME_TYPE_PAYMENT As String = "pi_type_payment"
    Public Const PAR_I_NAME_NUMBER_MONTHLY As String = "pi_number_monthly"
    Public Const PAR_I_NAME_MONTHLY_PAYMENT As String = "pi_monthly_payment"
    Public Const PAR_I_NAME_COEF_FIN As String = "pi_coef_fin"
    Public Const PAR_I_NAME_CURRENCY_PAYMENT As String = "pi_currency_payment"
    Public Const PAR_I_NAME_FINANCED_VALUE As String = "pi_financed_value"
    Public Const PAR_I_NAME_CREDIT_CARD_TYPE As String = "pi_credit_card_type"
    Public Const PAR_I_NAME_CREDIT_CARD As String = "pi_credit_card"
    Public Const PAR_I_NAME_CREDIT_AUTHORIZATION_NUMBER As String = "pi_credit_authorization_number"
    Public Const PAR_I_NAME_CANCELLATION_CODE As String = "pi_cancellation_code"
    Public Const PAR_I_NAME_MANUFACTURER As String = "pi_manufacturer"
    Public Const PAR_I_NAME_MODEL As String = "pi_model"
    Public Const PAR_I_NAME_SERIAL_NUMBER As String = "pi_serial_number"
    Public Const PAR_I_NAME_IMEI_NUMBER As String = "pi_imei_number"
    Public Const PAR_I_NAME_LAYOUT As String = "pi_layout"
    Public Const PAR_I_NAME_NEW_PRODUCT_CODE As String = "pi_new_product_code"
    Public Const PAR_I_NAME_MULTIPLE_DURATIONS As String = "pi_multiple_durations"
    Public Const PAR_I_NAME_ADDRESS2 As String = "pi_address2"
    Public Const PAR_I_NAME_OLD_NUMBER As String = "pi_old_number"
    Public Const PAR_I_NAME_ENTIRE_RECORD As String = "pi_entire_record"
    Public Const PAR_I_NAME_REJECT_CODE As String = "pi_reject_code"
    Public Const PAR_I_NAME_DOCUMENT_TYPE As String = "pi_document_type"
    Public Const PAR_I_NAME_DOCUMENT_AGENCY As String = "pi_document_agency"
    Public Const PAR_I_NAME_DOCUMENT_ISSUE_DATE As String = "pi_document_issue_date"
    Public Const PAR_I_NAME_RG_NUMBER As String = "pi_rg_number"
    Public Const PAR_I_NAME_DEALER_ID As String = "pi_dealer_id"
    Public Const PAR_I_NAME_ID_TYPE As String = "pi_id_type"
    Public Const PAR_I_NAME_SALES_TAX As String = "pi_sales_tax"
    Public Const PAR_I_NAME_CUST_COUNTRY As String = "pi_cust_country"
    Public Const PAR_I_NAME_COUNTRY_PURCH As String = "pi_country_purch"
    Public Const PAR_I_NAME_ISO_CODE As String = "pi_iso_code"
    Public Const PAR_I_NAME_OLITA_TRANS_ID As String = "pi_olita_trans_id"
    Public Const PAR_I_NAME_EMAIL As String = "pi_email"
    Public Const PAR_I_NAME_WORK_PHONE As String = "pi_work_phone"
    Public Const PAR_I_NAME_ITEM2_MANUFACTURER As String = "pi_item2_manufacturer"
    Public Const PAR_I_NAME_ITEM2_MODEL As String = "pi_item2_model"
    Public Const PAR_I_NAME_ITEM2_SERIAL_NUMBER As String = "pi_item2_serial_number"
    Public Const PAR_I_NAME_ITEM2_DESCRIPTION As String = "pi_item2_description"
    Public Const PAR_I_NAME_ITEM2_PRICE As String = "pi_item2_price"
    Public Const PAR_I_NAME_ITEM2_BUNDLE_VAL As String = "pi_item2_bundle_val"
    Public Const PAR_I_NAME_ITEM2_MAN_WARRANTY As String = "pi_item2_man_warranty"
    Public Const PAR_I_NAME_ITEM3_MANUFACTURER As String = "pi_item3_manufacturer"
    Public Const PAR_I_NAME_ITEM3_MODEL As String = "pi_item3_model"
    Public Const PAR_I_NAME_ITEM3_SERIAL_NUMBER As String = "pi_item3_serial_number"
    Public Const PAR_I_NAME_ITEM3_DESCRIPTION As String = "pi_item3_description"
    Public Const PAR_I_NAME_ITEM3_PRICE As String = "pi_item3_price"
    Public Const PAR_I_NAME_ITEM3_BUNDLE_VAL As String = "pi_item3_bundle_val"
    Public Const PAR_I_NAME_ITEM3_MAN_WARRANTY As String = "pi_item3_man_warranty"
    Public Const PAR_I_NAME_ITEM4_MANUFACTURER As String = "pi_item4_manufacturer"
    Public Const PAR_I_NAME_ITEM4_MODEL As String = "pi_item4_model"
    Public Const PAR_I_NAME_ITEM4_SERIAL_NUMBER As String = "pi_item4_serial_number"
    Public Const PAR_I_NAME_ITEM4_DESCRIPTION As String = "pi_item4_description"
    Public Const PAR_I_NAME_ITEM4_PRICE As String = "pi_item4_price"
    Public Const PAR_I_NAME_ITEM4_BUNDLE_VAL As String = "pi_item4_bundle_val"
    Public Const PAR_I_NAME_ITEM4_MAN_WARRANTY As String = "pi_item4_man_warranty"
    Public Const PAR_I_NAME_ITEM5_MANUFACTURER As String = "pi_item5_manufacturer"
    Public Const PAR_I_NAME_ITEM5_MODEL As String = "pi_item5_model"
    Public Const PAR_I_NAME_ITEM5_SERIAL_NUMBER As String = "pi_item5_serial_number"
    Public Const PAR_I_NAME_ITEM5_DESCRIPTION As String = "pi_item5_description"
    Public Const PAR_I_NAME_ITEM5_PRICE As String = "pi_item5_price"
    Public Const PAR_I_NAME_ITEM5_BUNDLE_VAL As String = "pi_item5_bundle_val"
    Public Const PAR_I_NAME_ITEM5_MAN_WARRANTY As String = "pi_item5_man_warranty"
    Public Const PAR_I_NAME_ITEM6_MANUFACTURER As String = "pi_item6_manufacturer"
    Public Const PAR_I_NAME_ITEM6_MODEL As String = "pi_item6_model"
    Public Const PAR_I_NAME_ITEM6_SERIAL_NUMBER As String = "pi_item6_serial_number"
    Public Const PAR_I_NAME_ITEM6_DESCRIPTION As String = "pi_item6_description"
    Public Const PAR_I_NAME_ITEM6_PRICE As String = "pi_item6_price"
    Public Const PAR_I_NAME_ITEM6_BUNDLE_VAL As String = "pi_item6_bundle_val"
    Public Const PAR_I_NAME_ITEM6_MAN_WARRANTY As String = "pi_item6_man_warranty"
    Public Const PAR_I_NAME_ITEM7_MANUFACTURER As String = "pi_item7_manufacturer"
    Public Const PAR_I_NAME_ITEM7_MODEL As String = "pi_item7_model"
    Public Const PAR_I_NAME_ITEM7_SERIAL_NUMBER As String = "pi_item7_serial_number"
    Public Const PAR_I_NAME_ITEM7_DESCRIPTION As String = "pi_item7_description"
    Public Const PAR_I_NAME_ITEM7_PRICE As String = "pi_item7_price"
    Public Const PAR_I_NAME_ITEM7_BUNDLE_VAL As String = "pi_item7_bundle_val"
    Public Const PAR_I_NAME_ITEM7_MAN_WARRANTY As String = "pi_item7_man_warranty"
    Public Const PAR_I_NAME_ITEM8_MANUFACTURER As String = "pi_item8_manufacturer"
    Public Const PAR_I_NAME_ITEM8_MODEL As String = "pi_item8_model"
    Public Const PAR_I_NAME_ITEM8_SERIAL_NUMBER As String = "pi_item8_serial_number"
    Public Const PAR_I_NAME_ITEM8_DESCRIPTION As String = "pi_item8_description"
    Public Const PAR_I_NAME_ITEM8_PRICE As String = "pi_item8_price"
    Public Const PAR_I_NAME_ITEM8_BUNDLE_VAL As String = "pi_item8_bundle_val"
    Public Const PAR_I_NAME_ITEM8_MAN_WARRANTY As String = "pi_item8_man_warranty"
    Public Const PAR_I_NAME_ITEM9_MANUFACTURER As String = "pi_item9_manufacturer"
    Public Const PAR_I_NAME_ITEM9_MODEL As String = "pi_item9_model"
    Public Const PAR_I_NAME_ITEM9_SERIAL_NUMBER As String = "pi_item9_serial_number"
    Public Const PAR_I_NAME_ITEM9_DESCRIPTION As String = "pi_item9_description"
    Public Const PAR_I_NAME_ITEM9_PRICE As String = "pi_item9_price"
    Public Const PAR_I_NAME_ITEM9_BUNDLE_VAL As String = "pi_item9_bundle_val"
    Public Const PAR_I_NAME_ITEM9_MAN_WARRANTY As String = "pi_item9_man_warranty"
    Public Const PAR_I_NAME_ITEM10_MANUFACTURER As String = "pi_item10_manufacturer"
    Public Const PAR_I_NAME_ITEM10_MODEL As String = "pi_item10_model"
    Public Const PAR_I_NAME_ITEM10_SERIAL_NUMBER As String = "pi_item10_serial_number"
    Public Const PAR_I_NAME_ITEM10_DESCRIPTION As String = "pi_item10_description"
    Public Const PAR_I_NAME_ITEM10_PRICE As String = "pi_item10_price"
    Public Const PAR_I_NAME_ITEM10_BUNDLE_VAL As String = "pi_item10_bundle_val"
    Public Const PAR_I_NAME_ITEM10_MAN_WARRANTY As String = "pi_item10_man_warranty"
    Public Const PAR_I_NAME_ITEM11_MANUFACTURER As String = "pi_item11_manufacturer"
    Public Const PAR_I_NAME_ITEM11_MODEL As String = "pi_item11_model"
    Public Const PAR_I_NAME_ITEM11_SERIAL_NUMBER As String = "pi_item11_serial_number"
    Public Const PAR_I_NAME_ITEM11_DESCRIPTION As String = "pi_item11_description"
    Public Const PAR_I_NAME_ITEM11_PRICE As String = "pi_item11_price"
    Public Const PAR_I_NAME_ITEM11_BUNDLE_VAL As String = "pi_item11_bundle_val"
    Public Const PAR_I_NAME_ITEM11_MAN_WARRANTY As String = "pi_item11_man_warranty"
    Public Const PAR_I_NAME_NEW_BRANCH_CODE As String = "pi_new_branch_code"
    Public Const PAR_I_NAME_BILLING_FREQUENCY As String = "pi_billing_frequency"
    Public Const PAR_I_NAME_NUMBER_OF_INSTALLMENTS As String = "pi_number_of_installments"
    Public Const PAR_I_NAME_INSTALLMENT_AMOUNT As String = "pi_installment_amount"
    Public Const PAR_I_NAME_BANK_ACCT_OWNER_NAME As String = "pi_bank_acct_owner_name"
    Public Const PAR_I_NAME_REJECT_MSG_PARMS As String = "pi_reject_msg_parms"
    Public Const PAR_I_NAME_BANK_ACCOUNT_NUMBER As String = "pi_bank_account_number"
    Public Const PAR_I_NAME_BANK_RTN_NUMBER As String = "pi_bank_rtn_number"
    Public Const PAR_I_NAME_SALUTATION As String = "pi_salutation"
    Public Const PAR_I_NAME_BANK_BRANCH_NUMBER As String = "pi_bank_branch_number"
    Public Const PAR_I_NAME_PAYMENT_INSTRUMENT_TYPE As String = "pi_payment_instrument_type"
    Public Const PAR_I_NAME_CREDIT_CARD_EXPIRATION As String = "pi_credit_card_expiration"
    Public Const PAR_I_NAME_NEXT_DUE_DATE As String = "pi_next_due_date"
    Public Const PAR_I_NAME_LANGUAGE_PREF As String = "pi_language_pref"
    Public Const PAR_I_NAME_POST_PRE_PAID As String = "pi_post_pre_paid"
    Public Const PAR_I_NAME_DATE_PAID_FOR As String = "pi_date_paid_for"
    Public Const PAR_I_NAME_MARKETING_PROMO_SER As String = "pi_marketing_promo_ser"
    Public Const PAR_I_NAME_MARKETING_PROMO_NUM As String = "pi_marketing_promo_num"
    Public Const PAR_I_NAME_MEMBERSHIP_NUMBER As String = "pi_membership_number"
    Public Const PAR_I_NAME_ADDRESS3 As String = "pi_address3"
    Public Const PAR_I_NAME_SUBSCRIBER_STATUS As String = "pi_subscriber_status"
    Public Const PAR_I_NAME_BILLING_PLAN As String = "pi_billing_plan"
    Public Const PAR_I_NAME_BILLING_CYCLE As String = "pi_billing_cycle"
    Public Const PAR_I_NAME_SERVICE_LINE_NUMBER As String = "pi_service_line_number"
    Public Const PAR_I_NAME_ORIGINAL_RETAIL_PRICE As String = "pi_original_retail_price"
    Public Const PAR_I_NAME_MOBILE_TYPE As String = "pi_mobile_type"
    Public Const PAR_I_NAME_FIRST_USE_DATE As String = "pi_first_use_date"
    Public Const PAR_I_NAME_LAST_USE_DATE As String = "pi_last_use_date"
    Public Const PAR_I_NAME_SIM_CARD_NUMBER As String = "pi_sim_card_number"
    Public Const PAR_I_NAME_REGION As String = "pi_region"
    Public Const PAR_I_NAME_SKU_NUMBER As String = "pi_sku_number"
    Public Const PAR_I_NAME_MEMBERSHIP_TYPE As String = "pi_membership_type"
    Public Const PAR_I_NAME_SUBSCRIBER_STATUS_CHANGE_DATE As String = "pi_subscriber_status_change_date"
    Public Const PAR_I_NAME_LINES_ON_ACCOUNT As String = "pi_lines_on_account"
    Public Const PAR_I_NAME_CESS_OFFICE As String = "pi_cess_office"
    Public Const PAR_I_NAME_CESS_SALESREP As String = "pi_cess_salesrep"
    Public Const PAR_I_NAME_BUSINESSLINE As String = "pi_businessline"
    Public Const PAR_I_NAME_SALES_DEPARTMENT As String = "pi_sales_department"
    Public Const PAR_I_NAME_SUSPENDED_REASON As String = "pi_suspended_reason"
    Public Const PAR_I_NAME_LINKED_CERT_NUMBER As String = "pi_linked_cert_number"
    Public Const PAR_I_NAME_ADDITIONAL_INFO As String = "pi_additional_info"
    Public Const PAR_I_NAME_CREDITCARD_LAST_FOUR_DIGIT As String = "pi_creditcard_last_four_digit"
    Public Const PAR_I_NAME_CANCEL_COMMENT_TYPE_CODE As String = "pi_cancel_comment_type_code"
    Public Const PAR_I_NAME_CANCELLATION_COMMENT As String = "pi_cancellation_comment"
    Public Const PAR_I_NAME_SUBSCRIBER_STATUS_ID As String = "pi_subscriber_status_id"
    Public Const PAR_I_NAME_MEMBERSHIP_TYPE_ID As String = "pi_membership_type_id"
    Public Const PAR_I_NAME_STATE_PROVINCE_ID As String = "pi_state_province_id"
    Public Const PAR_I_NAME_EXTERNAL_REGISTRATION_DATE As String = "pi_external_registration_date"
    Public Const PAR_I_NAME_CERT_ID As String = "pi_cert_id"
    Public Const PAR_I_NAME_INPUT_DATE As String = "pi_input_date"

    Public Const PAR_I_NAME_FINANCED_AMOUNT As String = "pi_financed_amount"
    Public Const PAR_I_NAME_FINANCED_FREQUENCY As String = "pi_financed_frequency"
    Public Const PAR_I_NAME_FINANCED_INSTALLMENT_NUMBER As String = "pi_financed_installment_number"
    Public Const PAR_I_NAME_FINANCED_INSTALLMENT_AMOUNT As String = "pi_financed_installment_amount"
    Public Const PAR_I_GENDER As String = "pi_gender"
    Public Const PAR_I_MARITAL_STATUS As String = "pi_marital_status"
    Public Const PAR_I_NATIONALITY As String = "pi_nationality"
    Public Const PAR_I_BIRTH_DATE As String = "pi_birth_date"

    'REQ-5657 - Start
    Public Const PAR_I_NAME_FINANCE_DATE As String = "pi_finance_date"
    Public Const PAR_I_NAME_DOWN_PAYMENT As String = "pi_down_payment"
    Public Const PAR_I_NAME_ADVANCE_PAYMENT As String = "pi_advance_payment"
    Public Const PAR_I_NAME_UPGRADE_TERM As String = "pi_upgrade_term"
    Public Const PAR_I_NAME_BILLING_ACCOUNT_NUMBER As String = "pi_billing_account_number"
    'REQ-5657 - End

    'REQ-5681 - Start
    Public Const PAR_I_NAME_PLACE_OF_BIRTH As String = "pi_place_of_birth"
    Public Const PAR_I_NAME_CUIT_CUIL As String = "pi_cuit_cuil"
    Public Const PAR_I_NAME_PERSON_TYPE As String = "pi_person_type"
    'REQ-5681 - End

    Public Const PAR_I_NAME_NUM_OF_CONSECUTIVE_PAYMENTS As String = "pi_num_of_consecutive_payments"
    Public Const PAR_I_NAME_LOAN_CODE As String = "pi_loan_code"
    Public Const PAR_I_NAME_PAYMENT_SHIFT_NUMBER As String = "pi_payment_shift_number"

    Public Const PAR_I_NAME_REFUND_AMOUNT As String = "pi_refund_amount"
    Public Const PAR_I_NAME_DEVICE_TYPE As String = "pi_device_type"
    Public Const PAR_I_NAME_OCCUPATION As String = "pi_occupation"

    Public Const PAR_I_NAME_DEALER_CURRENT_PLAN_CODE As String = "pi_dealer_current_plan_code"
    Public Const PAR_I_NAME_DEALER_SCHEDULED_PLAN_CODE As String = "pi_dealer_scheduled_plan_code"
    Public Const PAR_I_NAME_DEALER_UPDATE_REASON As String = "pi_dealer_update_reason"
    Public Const PAR_I_NAME_UPGRADE_DATE As String = "pi_upgrade_date"
    Public Const PAR_I_NAME_VOUCHER_NUMBER As String = "pi_voucher_number"
    Public Const PAR_I_NAME_RMA As String = "pi_rma"
    Public Const PAR_I_NAME_APPLEACRE_FEE As String = "pi_applecare_fee"
    Public Const PAR_IO_NAME_DEALER_RECON_WRK_ID As String = "pio_dealer_recon_wrk_id"

    'for reject message translation
    Public Const COL_UI_PROD_CODE As String = "UI_PROG_CODE"
    Public Const COL_REJECT_MSG_PARMS As String = "REJECT_MSG_PARMS"
    Public Const COL_MSG_PARAMETER_COUNT As String = "MSG_PARAMETER_COUNT"
    Public Const COL_REJECT_REASON As String = "reject_reason"
    Public Const COL_TRANSLATED_MSG As String = "Translated_MSG"

    'Custom parameter
    Public Const PAR_I_NAME_RECORD_MODE As String = "pi_record_mode"
    Public Const PAR_I_FI_RECORD_TYPE As String = "fi_record_type"
    Public Const PAR_I_FI_REJECT_CODE As String = "fi_reject_code"
    Public Const PAR_I_FI_REJECT_REASON As String = "fi_reject_reason"
    Public Const PAR_I_NAME_SORT_EXPRESSION As String = "pi_sort_expression"
    Public Const PAR_I_NAME_SORT_DIRECTION As String = "pi_sort_direction"
    Public Const PAR_I_SORT_BY As String = "sort_by"
    Public Const DYNAMIC_AND_CLAUSE_PLACE_HOLDER As String = "--dynamic_and_clause"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD"))
                cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Fetch(cmd, TABLE_NAME, familyDS)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(dealerfileProcessedID As Guid,
                                languageID As Guid,
                                recordMode As String,
                                recordType As String,
                                rejectCode As String,
                                rejectReason As String,
                                parentFile As String,
                                pageindex As Integer,
                                pagesize As Integer,
                                sortExpression As String) As DataSet
        Try
            rejectReason = FormatWildCard(rejectReason)

            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_I_NAME_DEALERFILE_PROCESSED_ID, OracleDbType.Raw, dealerfileProcessedID.ToByteArray())
                cmd.AddParameter(PAR_I_NAME_LANGUAGE_ID, OracleDbType.Raw, languageID.ToByteArray())
                cmd.AddParameter(PAR_I_NAME_RECORD_MODE, OracleDbType.Varchar2, recordMode)
                cmd.AddParameter(PAR_I_NAME_RECORD_TYPE, OracleDbType.Varchar2, recordType)
                cmd.AddParameter(PAR_I_NAME_REJECT_CODE, OracleDbType.Varchar2, rejectCode)
                cmd.AddParameter(PAR_I_NAME_REJECT_REASON, OracleDbType.Varchar2, rejectReason)
                cmd.AddParameter(PAR_I_NAME_PARENT_FILE, OracleDbType.Varchar2, parentFile)
                cmd.AddParameter(PAR_I_PAGE_INDEX, OracleDbType.Int64, value:=pageindex)
                cmd.AddParameter(PAR_I_PAGE_SIZE, OracleDbType.Int64, value:=pagesize)
                cmd.AddParameter(PAR_I_NAME_SORT_EXPRESSION, OracleDbType.Varchar2, value:=sortExpression)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CountSearch(dealerfileProcessedID As Guid,
                                recordMode As String,
                                recordType As String,
                                rejectCode As String,
                                rejectReason As String,
                                fi_record_type As String,
                                fi_reject_code As String,
                                fi_reject_reason As String) As Double
        Dim selectStmt As String = Config("/SQL/CountSearch")
        Dim recordtypeconstraint As String

        Dim familyDS As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealerfile_processed_id", dealerfileProcessedID.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                           New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                           New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                           New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                           New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                           New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                           New DBHelper.DBHelperParameter("pi_record_type", recordType),
                                                                                           New DBHelper.DBHelperParameter("pi_record_type", recordType),
                                                                                           New DBHelper.DBHelperParameter("pi_reject_code", rejectCode),
                                                                                           New DBHelper.DBHelperParameter("pi_reject_code", rejectCode),
                                                                                           New DBHelper.DBHelperParameter("pi_reject_reason", rejectReason),
                                                                                           New DBHelper.DBHelperParameter("pi_reject_reason", rejectReason),
                                                                                           New DBHelper.DBHelperParameter("fi_record_type", fi_record_type),
                                                                                           New DBHelper.DBHelperParameter("fi_reject_reason", fi_reject_reason),
                                                                                           New DBHelper.DBHelperParameter("fi_reject_code", fi_reject_code)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
            Return CType(familyDS.Tables(0).Rows(0)(0), Integer)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function ParentCount(dealerfileProcessedID As Guid,
                                recordMode As String,
                                recordType As String,
                                rejectCode As String,
                                rejectReason As String,
                                fi_record_type As String,
                                fi_reject_code As String,
                                fi_reject_reason As String) As Double
        Dim selectStmt As String = Config("/SQL/PARENT_COUNT")
        Dim recordtypeconstraint As String

        Dim familyDS As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealerfile_processed_id", dealerfileProcessedID.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("dealerfile_processed_id", dealerfileProcessedID.ToByteArray),
                                                                                               New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                               New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                               New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                               New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                               New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                               New DBHelper.DBHelperParameter("pi_record_mode", recordMode),
                                                                                               New DBHelper.DBHelperParameter("pi_record_type", recordType),
                                                                                               New DBHelper.DBHelperParameter("pi_record_type", recordType),
                                                                                               New DBHelper.DBHelperParameter("pi_reject_code", rejectCode),
                                                                                               New DBHelper.DBHelperParameter("pi_reject_code", rejectCode),
                                                                                               New DBHelper.DBHelperParameter("pi_reject_reason", rejectReason),
                                                                                               New DBHelper.DBHelperParameter("pi_reject_reason", rejectReason),
                                                                                               New DBHelper.DBHelperParameter("fi_record_type", fi_record_type),
                                                                                               New DBHelper.DBHelperParameter("fi_reject_reason", fi_reject_reason),
                                                                                               New DBHelper.DBHelperParameter("fi_reject_code", fi_reject_code)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
            Return CType(familyDS.Tables(0).Rows(0)(0), Integer)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function LoadRejectList(dealerfileProcessedID As Guid) As DataSet
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD_REJECT_LIST"))
                cmd.AddParameter(PAR_I_NAME_DEALERFILE_PROCESSED_ID, OracleDbType.Raw, dealerfileProcessedID.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"

    Public Sub UpdateHeaderCount(dealerFileProcessedId As Guid)
        Dim cmd As OracleCommand
        cmd = CreateCommand(Config("/SQL/UPDATE_HEADER_COUNT"), CommandType.StoredProcedure, CreateConnection())
        cmd.AddParameter("pi_dealerfile_processed_id", OracleDbType.Raw, dealerFileProcessedId.ToByteArray())
        cmd.ExecuteNonQuery()
    End Sub

    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
        If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_DEALER_RECON_WRK_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_RECON_WRK_ID)
            .AddParameter(PAR_I_NAME_DEALERFILE_PROCESSED_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALERFILE_PROCESSED_ID)
            .AddParameter(PAR_I_NAME_REJECT_REASON, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_REASON)
            .AddParameter(PAR_I_NAME_CERTIFICATE_LOADED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CERTIFICATE_LOADED)
            .AddParameter(PAR_I_NAME_RECORD_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RECORD_TYPE)
            .AddParameter(PAR_I_NAME_ITEM_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM_CODE)
            .AddParameter(PAR_I_NAME_ITEM, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM)
            .AddParameter(PAR_I_NAME_PRODUCT_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_PRODUCT_PRICE)
            .AddParameter(PAR_I_NAME_MAN_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_MAN_WARRANTY)
            .AddParameter(PAR_I_NAME_EXT_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_EXT_WARRANTY)
            .AddParameter(PAR_I_NAME_PRICE_POL, OracleDbType.Decimal, sourceColumn:=COL_NAME_PRICE_POL)
            .AddParameter(PAR_I_NAME_SR, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SR)
            .AddParameter(PAR_I_NAME_BRANCH_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BRANCH_CODE)
            .AddParameter(PAR_I_NAME_NUMBER_COMP, OracleDbType.Varchar2, sourceColumn:=COL_NAME_NUMBER_COMP)
            .AddParameter(PAR_I_NAME_DATE_COMP, OracleDbType.Date, sourceColumn:=COL_NAME_DATE_COMP)
            .AddParameter(PAR_I_NAME_CERTIFICATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CERTIFICATE)
            .AddParameter(PAR_I_NAME_IDENTIFICATION_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_IDENTIFICATION_NUMBER)
            .AddParameter(PAR_I_NAME_CUSTOMER_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CUSTOMER_NAME)
            .AddParameter(PAR_I_NAME_ADDRESS1, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ADDRESS1)
            .AddParameter(PAR_I_NAME_CITY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CITY)
            .AddParameter(PAR_I_NAME_ZIP, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ZIP)
            .AddParameter(PAR_I_NAME_STATE_PROVINCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STATE_PROVINCE)
            .AddParameter(PAR_I_NAME_HOME_PHONE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_HOME_PHONE)
            .AddParameter(PAR_I_NAME_CURRENCY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CURRENCY)
            .AddParameter(PAR_I_NAME_EXTWARR_SALEDATE, OracleDbType.Date, sourceColumn:=COL_NAME_EXTWARR_SALEDATE)
            .AddParameter(PAR_I_NAME_PRODUCT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE)
            .AddParameter(PAR_I_NAME_TYPE_PAYMENT, OracleDbType.Varchar2, sourceColumn:=COL_NAME_TYPE_PAYMENT)
            .AddParameter(PAR_I_NAME_NUMBER_MONTHLY, OracleDbType.Decimal, sourceColumn:=COL_NAME_NUMBER_MONTHLY)
            .AddParameter(PAR_I_NAME_MONTHLY_PAYMENT, OracleDbType.Decimal, sourceColumn:=COL_NAME_MONTHLY_PAYMENT)
            .AddParameter(PAR_I_NAME_COEF_FIN, OracleDbType.Decimal, sourceColumn:=COL_NAME_COEF_FIN)
            .AddParameter(PAR_I_NAME_CURRENCY_PAYMENT, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CURRENCY_PAYMENT)
            .AddParameter(PAR_I_NAME_FINANCED_VALUE, OracleDbType.Decimal, sourceColumn:=COL_NAME_FINANCED_VALUE)
            .AddParameter(PAR_I_NAME_CREDIT_CARD_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREDIT_CARD_TYPE)
            .AddParameter(PAR_I_NAME_CREDIT_CARD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREDIT_CARD)
            .AddParameter(PAR_I_NAME_CREDIT_AUTHORIZATION_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREDIT_AUTHORIZATION_NUMBER)
            .AddParameter(PAR_I_NAME_CANCELLATION_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CANCELLATION_CODE)
            .AddParameter(PAR_I_NAME_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MANUFACTURER)
            .AddParameter(PAR_I_NAME_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODEL)
            .AddParameter(PAR_I_NAME_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_IMEI_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_IMEI_NUMBER)
            .AddParameter(PAR_I_NAME_LAYOUT, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LAYOUT)
            .AddParameter(PAR_I_NAME_NEW_PRODUCT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_NEW_PRODUCT_CODE)
            .AddParameter(PAR_I_NAME_MULTIPLE_DURATIONS, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MULTIPLE_DURATIONS)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
            .AddParameter(PAR_I_NAME_ADDRESS2, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ADDRESS2)
            .AddParameter(PAR_I_NAME_OLD_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_OLD_NUMBER)
            .AddParameter(PAR_I_NAME_ENTIRE_RECORD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ENTIRE_RECORD)
            .AddParameter(PAR_I_NAME_REJECT_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_CODE)
            .AddParameter(PAR_I_NAME_DOCUMENT_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DOCUMENT_TYPE)
            .AddParameter(PAR_I_NAME_DOCUMENT_AGENCY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DOCUMENT_AGENCY)
            .AddParameter(PAR_I_NAME_DOCUMENT_ISSUE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_DOCUMENT_ISSUE_DATE)
            .AddParameter(PAR_I_NAME_RG_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RG_NUMBER)
            .AddParameter(PAR_I_NAME_DEALER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID)
            .AddParameter(PAR_I_NAME_ID_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ID_TYPE)
            .AddParameter(PAR_I_NAME_SALES_TAX, OracleDbType.Decimal, sourceColumn:=COL_NAME_SALES_TAX)
            .AddParameter(PAR_I_NAME_CUST_COUNTRY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CUST_COUNTRY)
            .AddParameter(PAR_I_NAME_COUNTRY_PURCH, OracleDbType.Varchar2, sourceColumn:=COL_NAME_COUNTRY_PURCH)
            .AddParameter(PAR_I_NAME_ISO_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ISO_CODE)
            .AddParameter(PAR_I_NAME_OLITA_TRANS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_OLITA_TRANS_ID)
            .AddParameter(PAR_I_NAME_EMAIL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_EMAIL)
            .AddParameter(PAR_I_NAME_WORK_PHONE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_WORK_PHONE)
            .AddParameter(PAR_I_NAME_ITEM2_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM2_MANUFACTURER)
            .AddParameter(PAR_I_NAME_ITEM2_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM2_MODEL)
            .AddParameter(PAR_I_NAME_ITEM2_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM2_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_ITEM2_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM2_DESCRIPTION)
            .AddParameter(PAR_I_NAME_ITEM2_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM2_PRICE)
            .AddParameter(PAR_I_NAME_ITEM2_BUNDLE_VAL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM2_BUNDLE_VAL)
            .AddParameter(PAR_I_NAME_ITEM2_MAN_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM2_MAN_WARRANTY)
            .AddParameter(PAR_I_NAME_ITEM3_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM3_MANUFACTURER)
            .AddParameter(PAR_I_NAME_ITEM3_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM3_MODEL)
            .AddParameter(PAR_I_NAME_ITEM3_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM3_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_ITEM3_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM3_DESCRIPTION)
            .AddParameter(PAR_I_NAME_ITEM3_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM3_PRICE)
            .AddParameter(PAR_I_NAME_ITEM3_BUNDLE_VAL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM3_BUNDLE_VAL)
            .AddParameter(PAR_I_NAME_ITEM3_MAN_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM3_MAN_WARRANTY)
            .AddParameter(PAR_I_NAME_ITEM4_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM4_MANUFACTURER)
            .AddParameter(PAR_I_NAME_ITEM4_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM4_MODEL)
            .AddParameter(PAR_I_NAME_ITEM4_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM4_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_ITEM4_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM4_DESCRIPTION)
            .AddParameter(PAR_I_NAME_ITEM4_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM4_PRICE)
            .AddParameter(PAR_I_NAME_ITEM4_BUNDLE_VAL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM4_BUNDLE_VAL)
            .AddParameter(PAR_I_NAME_ITEM4_MAN_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM4_MAN_WARRANTY)
            .AddParameter(PAR_I_NAME_ITEM5_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM5_MANUFACTURER)
            .AddParameter(PAR_I_NAME_ITEM5_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM5_MODEL)
            .AddParameter(PAR_I_NAME_ITEM5_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM5_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_ITEM5_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM5_DESCRIPTION)
            .AddParameter(PAR_I_NAME_ITEM5_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM5_PRICE)
            .AddParameter(PAR_I_NAME_ITEM5_BUNDLE_VAL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM5_BUNDLE_VAL)
            .AddParameter(PAR_I_NAME_ITEM5_MAN_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM5_MAN_WARRANTY)
            .AddParameter(PAR_I_NAME_ITEM6_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM6_MANUFACTURER)
            .AddParameter(PAR_I_NAME_ITEM6_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM6_MODEL)
            .AddParameter(PAR_I_NAME_ITEM6_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM6_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_ITEM6_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM6_DESCRIPTION)
            .AddParameter(PAR_I_NAME_ITEM6_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM6_PRICE)
            .AddParameter(PAR_I_NAME_ITEM6_BUNDLE_VAL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM6_BUNDLE_VAL)
            .AddParameter(PAR_I_NAME_ITEM6_MAN_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM6_MAN_WARRANTY)
            .AddParameter(PAR_I_NAME_ITEM7_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM7_MANUFACTURER)
            .AddParameter(PAR_I_NAME_ITEM7_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM7_MODEL)
            .AddParameter(PAR_I_NAME_ITEM7_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM7_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_ITEM7_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM7_DESCRIPTION)
            .AddParameter(PAR_I_NAME_ITEM7_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM7_PRICE)
            .AddParameter(PAR_I_NAME_ITEM7_BUNDLE_VAL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM7_BUNDLE_VAL)
            .AddParameter(PAR_I_NAME_ITEM7_MAN_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM7_MAN_WARRANTY)
            .AddParameter(PAR_I_NAME_ITEM8_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM8_MANUFACTURER)
            .AddParameter(PAR_I_NAME_ITEM8_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM8_MODEL)
            .AddParameter(PAR_I_NAME_ITEM8_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM8_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_ITEM8_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM8_DESCRIPTION)
            .AddParameter(PAR_I_NAME_ITEM8_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM8_PRICE)
            .AddParameter(PAR_I_NAME_ITEM8_BUNDLE_VAL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM8_BUNDLE_VAL)
            .AddParameter(PAR_I_NAME_ITEM8_MAN_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM8_MAN_WARRANTY)
            .AddParameter(PAR_I_NAME_ITEM9_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM9_MANUFACTURER)
            .AddParameter(PAR_I_NAME_ITEM9_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM9_MODEL)
            .AddParameter(PAR_I_NAME_ITEM9_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM9_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_ITEM9_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM9_DESCRIPTION)
            .AddParameter(PAR_I_NAME_ITEM9_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM9_PRICE)
            .AddParameter(PAR_I_NAME_ITEM9_BUNDLE_VAL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM9_BUNDLE_VAL)
            .AddParameter(PAR_I_NAME_ITEM9_MAN_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM9_MAN_WARRANTY)
            .AddParameter(PAR_I_NAME_ITEM10_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM10_MANUFACTURER)
            .AddParameter(PAR_I_NAME_ITEM10_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM10_MODEL)
            .AddParameter(PAR_I_NAME_ITEM10_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM10_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_ITEM10_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM10_DESCRIPTION)
            .AddParameter(PAR_I_NAME_ITEM10_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM10_PRICE)
            .AddParameter(PAR_I_NAME_ITEM10_BUNDLE_VAL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM10_BUNDLE_VAL)
            .AddParameter(PAR_I_NAME_ITEM10_MAN_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM10_MAN_WARRANTY)
            .AddParameter(PAR_I_NAME_ITEM11_MANUFACTURER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM11_MANUFACTURER)
            .AddParameter(PAR_I_NAME_ITEM11_MODEL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM11_MODEL)
            .AddParameter(PAR_I_NAME_ITEM11_SERIAL_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM11_SERIAL_NUMBER)
            .AddParameter(PAR_I_NAME_ITEM11_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM11_DESCRIPTION)
            .AddParameter(PAR_I_NAME_ITEM11_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM11_PRICE)
            .AddParameter(PAR_I_NAME_ITEM11_BUNDLE_VAL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM11_BUNDLE_VAL)
            .AddParameter(PAR_I_NAME_ITEM11_MAN_WARRANTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_ITEM11_MAN_WARRANTY)
            .AddParameter(PAR_I_NAME_NEW_BRANCH_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_NEW_BRANCH_CODE)
            .AddParameter(PAR_I_NAME_BILLING_FREQUENCY, OracleDbType.Decimal, sourceColumn:=COL_NAME_BILLING_FREQUENCY)
            .AddParameter(PAR_I_NAME_NUMBER_OF_INSTALLMENTS, OracleDbType.Decimal, sourceColumn:=COL_NAME_NUMBER_OF_INSTALLMENTS)
            .AddParameter(PAR_I_NAME_INSTALLMENT_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_INSTALLMENT_AMOUNT)
            .AddParameter(PAR_I_NAME_BANK_ACCT_OWNER_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BANK_ACCT_OWNER_NAME)
            .AddParameter(PAR_I_NAME_REJECT_MSG_PARMS, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REJECT_MSG_PARMS)
            .AddParameter(PAR_I_NAME_BANK_ACCOUNT_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BANK_ACCOUNT_NUMBER)
            .AddParameter(PAR_I_NAME_BANK_RTN_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BANK_RTN_NUMBER)
            .AddParameter(PAR_I_NAME_SALUTATION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SALUTATION)
            .AddParameter(PAR_I_NAME_BANK_BRANCH_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BANK_BRANCH_NUMBER)
            .AddParameter(PAR_I_NAME_PAYMENT_INSTRUMENT_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYMENT_INSTRUMENT_TYPE)
            .AddParameter(PAR_I_NAME_CREDIT_CARD_EXPIRATION, OracleDbType.Date, sourceColumn:=COL_NAME_CREDIT_CARD_EXPIRATION)
            .AddParameter(PAR_I_NAME_NEXT_DUE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_NEXT_DUE_DATE)
            .AddParameter(PAR_I_NAME_LANGUAGE_PREF, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LANGUAGE_PREF)
            .AddParameter(PAR_I_NAME_POST_PRE_PAID, OracleDbType.Varchar2, sourceColumn:=COL_NAME_POST_PRE_PAID)
            .AddParameter(PAR_I_NAME_DATE_PAID_FOR, OracleDbType.Date, sourceColumn:=COL_NAME_DATE_PAID_FOR)
            .AddParameter(PAR_I_NAME_MARKETING_PROMO_SER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MARKETING_PROMO_SER)
            .AddParameter(PAR_I_NAME_MARKETING_PROMO_NUM, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MARKETING_PROMO_NUM)
            .AddParameter(PAR_I_NAME_MEMBERSHIP_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MEMBERSHIP_NUMBER)
            .AddParameter(PAR_I_NAME_ADDRESS3, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ADDRESS3)
            .AddParameter(PAR_I_NAME_SUBSCRIBER_STATUS, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SUBSCRIBER_STATUS)
            .AddParameter(PAR_I_NAME_BILLING_PLAN, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BILLING_PLAN)
            .AddParameter(PAR_I_NAME_BILLING_CYCLE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BILLING_CYCLE)
            .AddParameter(PAR_I_NAME_SERVICE_LINE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SERVICE_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_ORIGINAL_RETAIL_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_ORIGINAL_RETAIL_PRICE)
            .AddParameter(PAR_I_NAME_MOBILE_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MOBILE_TYPE)
            .AddParameter(PAR_I_NAME_FIRST_USE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_FIRST_USE_DATE)
            .AddParameter(PAR_I_NAME_LAST_USE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_LAST_USE_DATE)
            .AddParameter(PAR_I_NAME_SIM_CARD_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SIM_CARD_NUMBER)
            .AddParameter(PAR_I_NAME_REGION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REGION)
            .AddParameter(PAR_I_NAME_SKU_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SKU_NUMBER)
            .AddParameter(PAR_I_NAME_MEMBERSHIP_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MEMBERSHIP_TYPE)
            .AddParameter(PAR_I_NAME_SUBSCRIBER_STATUS_CHANGE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_SUBSCRIBER_STATUS_CHANGE_DATE)
            .AddParameter(PAR_I_NAME_LINES_ON_ACCOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_LINES_ON_ACCOUNT)
            .AddParameter(PAR_I_NAME_CESS_OFFICE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CESS_OFFICE)
            .AddParameter(PAR_I_NAME_CESS_SALESREP, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CESS_SALESREP)
            .AddParameter(PAR_I_NAME_BUSINESSLINE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BUSINESSLINE)
            .AddParameter(PAR_I_NAME_SALES_DEPARTMENT, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SALES_DEPARTMENT)
            .AddParameter(PAR_I_NAME_SUSPENDED_REASON, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SUSPENDED_REASON)
            .AddParameter(PAR_I_NAME_LINKED_CERT_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LINKED_CERT_NUMBER)
            .AddParameter(PAR_I_NAME_ADDITIONAL_INFO, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ADDITIONAL_INFO)
            .AddParameter(PAR_I_NAME_CREDITCARD_LAST_FOUR_DIGIT, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREDITCARD_LAST_FOUR_DIGIT)
            .AddParameter(PAR_I_NAME_CANCEL_COMMENT_TYPE_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CANCEL_COMMENT_TYPE_CODE)
            .AddParameter(PAR_I_NAME_CANCELLATION_COMMENT, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CANCELLATION_COMMENT)
            .AddParameter(PAR_I_NAME_SUBSCRIBER_STATUS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_SUBSCRIBER_STATUS_ID)
            .AddParameter(PAR_I_NAME_MEMBERSHIP_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_MEMBERSHIP_TYPE_ID)
            .AddParameter(PAR_I_NAME_STATE_PROVINCE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_STATE_PROVINCE_ID)
            .AddParameter(PAR_I_NAME_EXTERNAL_REGISTRATION_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_EXTERNAL_REGISTRATION_DATE)
            .AddParameter(PAR_I_NAME_CERT_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CERT_ID)
            .AddParameter(PAR_I_NAME_INPUT_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INPUT_DATE)
            .AddParameter(PAR_I_NAME_FINANCED_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_FINANCED_AMOUNT)
            .AddParameter(PAR_I_NAME_FINANCED_FREQUENCY, OracleDbType.Decimal, sourceColumn:=COL_NAME_FINANCED_FREQUENCY)
            .AddParameter(PAR_I_NAME_FINANCED_INSTALLMENT_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_FINANCED_INSTALLMENT_NUMBER)
            .AddParameter(PAR_I_NAME_FINANCED_INSTALLMENT_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_FINANCED_INSTALLMENT_AMOUNT)
            .AddParameter(PAR_I_GENDER, OracleDbType.Char, sourceColumn:=COL_NAME_GENDER)
            .AddParameter(PAR_I_MARITAL_STATUS, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MARITAL_STATUS)
            .AddParameter(PAR_I_NATIONALITY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_NATIONALITY)
            .AddParameter(PAR_I_BIRTH_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_BIRTH_DATE)
            'REQ-5657 - Start
            .AddParameter(PAR_I_NAME_FINANCE_DATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_FINANCE_DATE)
            .AddParameter(PAR_I_NAME_DOWN_PAYMENT, OracleDbType.Decimal, sourceColumn:=COL_NAME_DOWN_PAYMENT)
            .AddParameter(PAR_I_NAME_ADVANCE_PAYMENT, OracleDbType.Decimal, sourceColumn:=COL_NAME_ADVANCE_PAYMENT)
            .AddParameter(PAR_I_NAME_UPGRADE_TERM, OracleDbType.Decimal, sourceColumn:=COL_NAME_UPGRADE_TERM)
            .AddParameter(PAR_I_NAME_BILLING_ACCOUNT_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BILLING_ACCOUNT_NUMBER)
            'REQ-5657 - End
            'REQ-5681 - Start
            .AddParameter(PAR_I_NAME_PLACE_OF_BIRTH, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PLACE_OF_BIRTH)
            .AddParameter(PAR_I_NAME_CUIT_CUIL, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CUIT_CUIL)
            .AddParameter(PAR_I_NAME_PERSON_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PERSON_TYPE)
            'REQ-5681 - End
            .AddParameter(PAR_I_NAME_NUM_OF_CONSECUTIVE_PAYMENTS, OracleDbType.Int16, sourceColumn:=COL_NAME_NUM_OF_CONSECUTIVE_PAYMENTS)
            .AddParameter(PAR_I_NAME_LOAN_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LOAN_CODE)
            .AddParameter(PAR_I_NAME_PAYMENT_SHIFT_NUMBER, OracleDbType.Int16, sourceColumn:=COL_NAME_PAYMENT_SHIFT_NUMBER)
            .AddParameter(PAR_I_NAME_DEALER_CURRENT_PLAN_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DEALER_CURRENT_PLAN_CODE)
            .AddParameter(PAR_I_NAME_DEALER_SCHEDULED_PLAN_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DEALER_SCHEDULED_PLAN_CODE)
            .AddParameter(PAR_I_NAME_DEALER_UPDATE_REASON, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DEALER_UPDATE_REASON)
            .AddParameter(PAR_I_NAME_REFUND_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_REFUND_AMOUNT)
            .AddParameter(PAR_I_NAME_DEVICE_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DEVICE_TYPE)
            .AddParameter(PAR_I_NAME_OCCUPATION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_OCCUPATION)
        End With

    End Sub
#End Region

End Class


