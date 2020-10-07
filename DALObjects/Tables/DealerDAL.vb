'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/19/2004)********************


Public Class DealerDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DEALER"
    Public Const TABLE_NAME_CLAIM_TYPES As String = "ELP_DEALER_CLM_APROVE_CLMTYPE"
    Public Const TABLE_NAME_COVERAGE_TYPES As String = "ELP_DEALER_CLM_APROVE_COVTYPE"
    Public Const TABLE_KEY_NAME As String = "dealer_id"

    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER As String = "dealer"
    Public Const COL_NAME_CLIENT_DEALER_CODE As String = "client_dealer_code"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_TAX_ID_NUMBER As String = "tax_id_number"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_COUNT_LEVEL As String = "count_level"
    Public Const COL_NAME_ADDRESS_ID As String = "address_id"
    Public Const COL_NAME_CONTACT_NAME As String = "contact_name"
    Public Const COL_NAME_CONTACT_PHONE As String = "contact_phone"
    Public Const COL_NAME_CONTACT_EXT As String = "contact_ext"
    Public Const COL_NAME_CONTACT_FAX As String = "contact_fax"
    Public Const COL_NAME_CONTACT_EMAIL As String = "contact_email"
    Public Const COL_NAME_RETAILER_ID As String = "retailer_id"
    Public Const COL_NAME_DEALER_GROUP_ID As String = "dealer_group_id"
    Public Const COL_NAME_ACTIVE_FLAG As String = "active_flag"
    Public Const COL_NAME_CONVERT_PRODUCT_CODE_ID As String = "convert_product_code_id"
    Public Const COL_NAME_DEALER_TYPE_ID As String = "dealer_type_id"
    Public Const COL_NAME_DEALER_DISCOUNT As String = "dealer_discount"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "Service_center_id"
    Public Const COL_NAME_DELAY_FACTOR_FLAG_ID As String = "delay_factor_flag_id"
    Public Const COL_NAME_INSTALLMENT_FACTOR_FLAG_ID As String = "installment_factor_flag_id"
    Public Const COL_NAME_REGISTRATION_PROCESS_FLAG_ID As String = "registration_process_flag_id"
    Public Const COL_NAME_REGISTRATION_EMAIL_FROM As String = "registration_email_from"

    Public Const COL_NAME_BUSINESS_NAME As String = "business_name"
    Public Const COL_NAME_STATE_TAX_ID_NUMBER As String = "state_tax_id_number"
    Public Const COL_NAME_CITY_TAX_ID_NUMBER As String = "city_tax_id_number"
    Public Const COL_NAME_WEB_ADDRESS As String = "web_address"
    Public Const COL_NAME_MAILING_ADDRESS_ID As String = "mailing_address_id"
    Public Const COL_NAME_NUMBER_OF_OTHER_LOCATIONS As String = "number_of_other_locations"
    Public Const COL_NAME_PRICE_MATRIX_USES_WP_ID As String = "price_matrix_uses_wp_id"
    Public Const COL_NAME_EXPECTED_PREMIUM_IS_WP_ID As String = "expected_premium_is_wp_id"

    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_SERVICE_NETWORK_ID As String = "service_network_id"
    Public Const COL_NAME_REUSE_SERIAL_NUMBER_ID As String = "reuse_serial_number_id"

    Public Const COL_NAME_FROM_DEALER As String = "p_from_dealer_id"
    Public Const COL_NAME_FROM_COVERAGE_ID As String = "p_from_coverage_id"
    Public Const COL_NAME_FROM_PRODUCT_CODE_ID As String = "p_from_product_code_id"
    Public Const COL_NAME_TO_DEALER As String = "p_to_dealer_id"
    Public Const COL_NAME_COPY_LEVEL As String = "p_copy_level"
    Public Const COL_NAME_EFF_DATE As String = "p_eff_date"
    Public Const COL_NAME_EXP_DATE As String = "p_exp_date"
    Public Const COL_NAME_CONTRACT As String = "p_contract_id"
    Public Const COL_NAME_OLITA_SEARCH As String = "olita_search"
    Public Const COL_NAME_CANCEL_REQUEST_FLAG_ID As String = "cancel_request_flag_id"

    Public Const COL_NAME_USE_WARRANTY_MASTER_ID As String = "use_warranty_master_id"
    Public Const COL_NAME_INSERT_MAKE_IF_NOT_EXISTS_ID As String = "Insert_Make_If_Not_Exist_ID"
    Public Const COL_NAME_USE_INCOMING_SALES_TAX_ID As String = "use_incoming_sales_tax_id"
    Public Const COL_NAME_AUTO_PROCESS_FILE_ID As String = "auto_process_file_id"
    Public Const COL_NAME_AUTO_REJ_ERR_TYPE_ID As String = "auto_rej_err_type_id"
    Public Const COL_NAME_RECON_REJ_REC_TYPE_ID As String = "rejected_record_recon_id"
    Public Const COL_NAME_DEALER_EXTRACT_PERIOD_ID As String = "dealer_extract_period_id"
    Public Const COL_NAME_VALIDATE_BILLING_CYCLE_ID As String = "validate_billing_cycle_id"
    Public Const COL_NAME_AUTO_PROCESS_PYMT_FILE_ID As String = "auto_process_pymt_file_id"
    'KDDI Changes'
    Public Const COL_NAME_IS_RESHIPMENT_ALLOWED As String = "is_reshipment_allowed"
    Public Const COL_NAME_IS_CANCEL_SHIPMENT_ALLOWED As String = "is_cancel_shipment_allowed"
    Public Const CANCEL_SHIPMENT_GRACE_PERIOD As String = "cancel_shipment_grace_period"
    Public Const COL_NAME_VALIDATE_ADDRESS As String = "validate_address_xcd"
    'Public Const COL_NAME_AUTH_AMT_BASED_ON_ID As String = "auth_amt_based_on_id"
    Public Const TOTAL_PARAM = 1 '2
    Public Const COMPANY_CODE = 0
    Public Const TODAY_DATE = 1
    Public Const DEALER_ID = 0
    Public Const COMPANY_ID = 0
    Public Const COUNT_LEVEL = 1
    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const DEALER_COVERAGES_COUNT_TABLE As String = "DEALER_COVERAGES_COUNT_TABLE"
    Public Const DEALER_PRODUCT_CODES_COUNT_TABLE As String = "DEALER_PRODUCT_CODES_COUNT_TABLE"
    Public Const DEALER_CERTIFICATES_COUNT_TABLE As String = "DEALER_CERTIFICATES_COUNT_TABLE"
    Public Const DUPLICATE_DEALER_TABLE As String = "DUPLICATE_DEALER_TABLE"
    Public Const DUPLICATE_PREFIX_COUNT_TABLE As String = "DUPLICATE_PREFIX_COUNT_TABLE"
    Public Const TOTAL_PARAM_DEALER_COVERAGES_COUNT = 0
    Public Const TOTAL_PARAM_DEALER_PRODUCTCODES_COUNT = 0
    Public Const TOTAL_PARAM_DEALER_CERTIFICATES_COUNT = 0
    Public Const TOTAL_PARAM_COPY_DEALER_DEFINITIONS = 4
    Public Const TOTAL_PARAM_RENEW_COVERAGES = 2
    Public Const TOTAL_PARAM_DELETE_DEALER_DEFINITIONS = 2
    Public Const COL_NAME_DEALER_COVERAGES_COUNT As String = "Dealer_Coverages_Count"
    Public Const COL_NAME_DEALER_PRODUCT_CODES_COUNT As String = "Dealer_Product_Code_Count"
    Public Const COL_NAME_DEALER_CERTIFICATES_COUNT As String = "Dealer_Certificates_Count"
    Public Const COL_NAME_DUPLICATE_PREFIX_COUNT As String = "duplicate_prefix_count"
    Public Const COL_NAME_DEALER_COUNT As String = "Dealer_Count"
    Public Const COL_NAME_BUSINESS_COUNTRY_ID As String = "business_country_id"
    Public Const COL_NAME_COMPANY_TYPE_ID As String = "company_type_id"
    Public Const COL_NAME_BRANCH_VALIDATION_ID As String = "branch_validation_id"
    Public Const COL_NAME_BANK_INFO_ID As String = "bank_info_id"
    Public Const COL_NAME_RETURN As String = "return_code"

    '8/2/2006 - ALR - Added to query with the user_id column for queries requiring multiple companies
    Public Const COL_NAME_USER_ID As String = "user_id"

    Public Const COL_NAME_IBNR_COMPUTE_METHOD_ID As String = "ibnr_compute_method_id"
    Public Const COL_NAME_STATIBNR_COMPUTE_METHOD_ID As String = "stat_ibnr_compute_method_id"
    Public Const COL_NAME_LAEIBNR_COMPUTE_METHOD_ID As String = "lae_ibnr_compute_method_id"
    Public Const COL_NAME_IBNR_FACTOR As String = "ibnr_factor"
    Public Const COL_NAME_STAT_IBNR_FACTOR As String = "stat_ibnr_factor"
    Public Const COL_NAME_LAE_IBNR_FACTOR As String = "lae_ibnr_factor"

    Public Const COL_NAME_INVOICE_BY_BRANCH_ID As String = "invoice_by_branch_id"
    Public Const COL_NAME_SEPARATED_CREDIT_NOTES_ID As String = "separated_credit_notes_id"
    Public Const COL_NAME_CERT_CANCEL_BY_ID As String = "cert_cancel_by_id"
    Public Const COL_NAME_MANUAL_ENROLLMENT_ALLOWED_ID As String = "manual_enrollment_allowed_id"
    Public Const COL_NAME_EDIT_BRANCH_ID As String = "edit_branch_id"
    Public Const COL_NAME_ROUND_COMM_FLAG_ID As String = "round_comm_flag_id"
    Public Const COL_NAME_USE_INSTALLMENT_DEFN_ID As String = "use_installment_defn_id"

    Public Const COL_NAME_PROGRAM_NAME As String = "program_name"
    Public Const COL_NAME_SERVICE_LINE_PHONE As String = "service_line_phone"
    Public Const COL_NAME_SERVICE_LINE_FAX As String = "service_line_fax"
    Public Const COL_NAME_SERVICE_LINE_EMAIL As String = "service_line_email"
    Public Const COL_NAME_ESC_INSURANCE_LABEL As String = "esc_insurance_label"
    Public Const COL_NAME_CLAIM_SYSTEM_ID As String = "claim_system_id"
    Public Const COL_NAME_ASSURANT_IS_OBLIGOR_ID As String = "assurant_is_obligor_id"
    Public Const COL_NAME_MAX_MAN_WARR As String = "max_man_warr"
    Public Const COL_NAME_MIN_MAN_WARR As String = "min_man_warr"
    Public Const COL_NAME_MIGRATION_PATH_ID As String = "migration_path_id"
    Public Const COL_NAME_EQUIPMENT_LIST_CODE As String = "equipment_list_code"
    'REQ-860 Elita Buildout - Issues/Adjudication
    Public Const COL_NAME_QUESTION_LIST_CODE As String = "question_list_code"
    Public Const COL_NAME_USE_EQUIPMENT_ID As String = "use_equipment_id"
    Public Const COL_NAME_VALIDATE_SKU_ID As String = "validate_sku_id"
    Public Const COL_NAME_PAY_DEDUCTIBLE_ID As String = "pay_deductible_id"
    'REQ-1294
    'Public Const COL_NAME_CUST_INFO_MANDATORY_ID As String = "cust_info_mandatory_id"
    Public Const COL_NAME_BANK_INFO_MANDATORY_ID As String = "bank_info_mandatory_id"
    Public Const COL_VALIDATE_SERIAL_NUMBER_ID As String = "validate_serial_number_id"
    Public Const COL_DEDUCTIBLE_COLLECTION_ID As String = "deductible_collection_id"
    Public Const COL_NAME_PRODUCT_BY_REGION_ID As String = "product_by_region_id"
    Public Const COL_NAME_CLAIM_VERIFICATION_NUM_LENGTH As String = "claim_verification_num_length"
    'Req-1297
    Public Const COL_NAME_MAX_NC_RECORDS As String = "max_ncrecords"
    'Req-1297 End
    'REQ-1032
    Public Const COL_NAME_CLAIM_EXTENDED_STATUS_ENTRY_ID As String = "claim_extended_status_entry_id"

    'Req 1157
    Public Const COL_NAME_NEW_DEVICE_SKU_REQUIRED_ID As String = "NEW_DEVICE_SKU_REQUIRED_ID"

    'Req-1000
    Public Const COL_NAME_ALLOW_UPDATE_CANCELLATION_ID As String = "allow_update_cancellation_id"
    Public Const COL_NAME_REJECT_AFTER_CANCELLATION_ID As String = "reject_after_cancellation_id"
    Public Const COL_NAME_ALLOW_FUTURE_CANCEL_DATE_ID As String = "allow_future_cancel_date_id"
    Public Const COL_NAME_IS_LAWSUIT_MANDATORY_ID As String = "lawsuit_mandatory_id"
    'REQ-1153
    Public Const COL_NAME_DEALER_SUPPORT_WEB_CLAIMS_ID As String = "dealer_support_web_claims_id"
    Public Const COL_NAME_CLAIM_STATUS_FOR_EXT_SYSTEM_ID As String = "claim_status_for_ext_system_id"
    'REQ-1142
    Public Const COL_NAME_LICENSE_TAG_VALIDATION As String = "license_tag_validation"
    'REQ-5723
    Public Const COL_NAME_VSC_VIN_RESTRIC_ID As String = "VSC_VIN_RESTRIC_ID"
    Public Const COL_NAME_PLAN_CODE_IN_QUOTE_OUTPUT_ID As String = "PLAN_CODE_IN_QUOTE_OUTPUT_ID"


    'REQ-1190
    Public Const COL_NAME_ENROLLFILEPREPROCESSPROC_ID As String = "enrollfilepreprocessproc_id"
    Public Const COL_NAME_CERTNUMLOOKUPBY_ID As String = "certnumlookupby_id"
    'Req-1297
    Public Const COL_NAME_USEFULLFILEPROCESS_ID As String = "FULLFILEPROCESS_ID"
    'End of Req-1297

    'REQ-1244
    Public Const COL_NAME_REPLACECLAIMDEDTOLERANCEPCT As String = "replaceclaimdedtolerancepct"

    'REQ-1274
    Public Const COL_NAME_BILLING_PROCESS_CODE_ID As String = "billing_process_code_id"
    Public Const COL_NAME_BILLRESULT_EXCEPTION_DEST_ID As String = "billresult_exception_dest_id"
    Public Const COL_NAME_BILLRESULT_NOTIFICATION_EMAIL As String = "billresult_notification_email"

    Public Const COL_NAME_CERTIFICATES_AUTONUMBER_PREFIX As String = "certificates_autonumber_prefix"
    Public Const COL_NAME_CERTIFICATES_AUTONUMBER_ID As String = "certificates_autonumber_id"

    Public Const COL_NAME_FILE_LOAD_NOTIFICATION_EMAIL As String = "file_load_notification_email"

    Private Const DSNAME As String = "LIST"
    Public Const COL_NAME_USE_CLAIM_AUTHORIZATION_ID As String = "USE_CLAIM_AUTHORIZATION_ID"

    Public Const COL_NAME_MAX_CERTNUM_LENGTH_ALLOWED As String = "MAX_CERTNUM_LENGTH_ALLOWED"

    Public Const COL_NAME_AUTO_SELECT_SERVICE_CENTER As String = "Auto_Select_Service_Center"

    Public Const COL_CLAIM_UPDATE_OPTION As String = "CLAIM_UPDATE_OPTION"

    Public Const COL_NAME_CLAIM_AUTO_APPROVE_ID As String = "claim_auto_approve_id"

    Public Const COL_NAME_REQUIRE_CUSTOMER_AML_INFO_ID As String = "require_customer_aml_info_id"
    'REQ-5586

    Public Const COL_NAME_USE_QUOTE = "use_quote"
    Public Const COL_NAME_CONTRACT_MANUAL_VERIFICATION = "contract_manual_verification"
    Public Const COL_NAME_ACCEPT_PAYMENT_BY_CHECK = "accept_payment_by_check"
    Public Const COL_POLICY_EVENT_NOTIFY_EMAIL As String = "policy_event_notify_email"

    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"

    Public Const COL_NAME_DEF_SALVAGE_CENTER_ID As String = "def_salvage_center_id"
    Public Const COL_NAME_MAX_COMMISSION_PERCENT As String = "max_commission_percent"

    'REQ-5761
    Public Const COL_NAME_USE_NEWBILLFORM As String = "USE_NEWBILLFORM"

    'REQ-5932
    Public Const COL_NAME_SHARE_CUSTOMERS As String = "SHARE_CUSTOMER"

    Public Const COL_NAME_CUSTOMER_IDENTITY_LOOKUP As String = "dealer_customer_lookup_by"

    'REQ
    Public Const COL_NAME_CLAIM_RECORDING_XCD As String = "claim_recording_xcd"
    Public Const COL_NAME_USE_FRAUD_MONITORING_XCD As String = "enable_fraud_monitoring_xcd"

    'REQ 6156 
    Public Const COL_NAME_IMEI_USE_XCD As String = "imei_use_xcd"
    Public Const COL_NAME_CLAIM_RECORDING_CHECK_INVENTORY_XCD As String = "claim_recording_check_inv_xcd"

    'US-33 - 192905 Thunder
    Public const COL_NAME_SUSPEND_APPLIES_XCD As String ="suspend_applies_xcd"
    Public const COL_NAME_VOID_DURATION As String ="void_duration"
    Public const COL_NAME_SUSPEND_PERIOD As String ="suspend_period"
    Public const COL_NAME_INVOICE_CUTOFF_DAY As String ="invoice_cutoff_day"
    Public const COL_NAME_SOURCE_SYSTEM_XCD As String ="source_system_xcd"
    Public Const COL_NAME_BENEFIT_CARRIER_CODE As String = "benefit_carrier_code"

    Public Const COL_NAME_BENEFIT_SOLD_TO_ACCOUNT As String = "benefit_sold_to_account"

    'Copy/Delete Definitions Store Procedure Parameters
    Public Const FROM_DEALER = 0        'Copy/Delete
    Public Const TO_DEALER = 1          'Copy
    Public Const COPY_LEVEL = 2         'Copy
    Public Const EFF_DATE = 3         'Copy
    Public Const EXP_DATE = 4         'Copy
    Public Const DELETE_LEVEL = 2       'Delete
    Public Const FROM_COVERAGE = 1      'Delete
    Public Const FROM_PRODUCT_CODE = 1      'Delete
    Public Const RENEW_FROM_DEALER = 0      'Renew
    Public Const RENEW_CONTRACT = 1         'Renew
    Public Const RENEW_EFF_DATE = 2         'Renew

    Public Const DELETE_DEALER_PRODUCTCODES_AND_ITEMS = 2
    Public Const DELETE_ALL_DEALER_DEFINITIONS = 4

    Public Const COL_NAME_RETURN_REASON As String = "return_reason"
    Public Const COL_NAME_RETURN_CODE As String = "return_code"
    Public Const COL_NAME_DOCUMENT_TYPE_ID As String = "document_type_id"
    Public Const PARAM_NAME_DOCUMENT_TYPE As String = "document_type"
    Public Const TOTAL_PARAM_IN = 1 '2
    Public Const TOTAL_PARAM_OUT = 1 '1
    Public Const IN_DOC_TYPE = 0
    Public Const IN_ID_NUMBER = 1

    Public Const OUT_REJ_REASON = 0
    Public Const OUT_REJ_CODE = 1
    '5623
    Public Const COL_NAME_GRACE_PERIOD_MONTHS As String = "grace_period_months"
    Public Const COL_NAME_GRACE_PERIOD_DAYS As String = "grace_period_days"
    Public Const COL_NAME_AUTO_GEN_REJ_PYMT_FILE_ID As String = "auto_gen_rej_pymt_file_id"
    Public Const COL_NAME_PYMT_REJ_REC_RECON_ID As String = "pymt_rej_rec_recon_id"

    Public Const COL_NAME_IDENTIFICATION_NUMBER_TYPE As String = "identification_number_type"

    Public Const COL_NAME_CASE_PROFILE_CODE As String = "case_profile_code"

    Public Const ParINameDealerCode As String = "pi_dealer_code"
    Public Const ParINameProviderType As String = "pi_provider_type"

    Public Const COL_NAME_CLOSE_CASE_GRACE_PERIOD_DAYS As String = "close_case_grace_period_days"

    Public Const COL_NAME_SHOW_PREV_CALLER_INFO As String = "show_prev_caller_info_xcd"
    Public Const COL_NAME_USE_TAT_NOTIFICATION As String = "use_tat_notification_xcd"

    Public Const COL_NAME_DISPLAY_DOB As String = "display_mask_dob_xcd"
    Public Const COL_NAME_ALLOW_CERT_CNL_WITH_CLAIM_XCD As String = "allow_cert_cnl_with_claim_xcd"

    'US 489857
    Public Const COL_NAME_ACCT_BUCKETS_WITH_SOURCE_XCD As String = "acct_buckets_with_source_xcd"
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
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(familyDS As DataSet, Company_id As Guid, Dealer As String)
        Dim selectStmt As String = Config("/SQL/LOAD_BY_COMPANY_DEALER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Company_id", Company_id.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("Dealer", Dealer)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadFirstDealerByDealerGrp(Dealer_Group_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_FIRST_DEALER_BY_DEALER_GROUP")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Dealer_Group_Id", Dealer_Group_id.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetDealerIDbyCodeAndDealerGroup(Dealer_Group_id As Guid, Dealer As String) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_DEALERID_BY_CODE_AND_DEALERGROUP")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Dealer_Group_Id", Dealer_Group_id.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("Dealer", Dealer)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCertAutoGenFlag(dealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_CERT_AUTO_GEN_FLAG")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Dealer_id", dealerId)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetRejRecReconFlag(dealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_REJ_REC_RECON_FLAG")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Dealer_id", dealerId)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Private Function IsThereALikeClause(descriptionMask As String, codeMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = IsLikeClause(descriptionMask) OrElse IsLikeClause(codeMask)
        Return bIsLikeClause
    End Function

    'Public Function LoadList(ByVal description As String, ByVal code As String, ByVal dealer_group_id As Guid, ByVal company_group_id As Guid) As DataSet

    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Dim whereClauseConditions As String = ""

    '    Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_COMPANY_GROUP_ID, company_group_id.ToByteArray)}

    '    whereClauseConditions &= " AND c.company_group_id = '" & Me.GuidToSQLString(company_group_id) & "'"

    '    If Not Me.IsNothing(dealer_group_id) Then
    '        whereClauseConditions &= Environment.NewLine & " AND g.dealer_group_id = '" & Me.GuidToSQLString(dealer_group_id) & "'"
    '    End If



    '    If ((Not (description Is Nothing)) AndAlso (Me.FormatSearchMask(description))) Then
    '        whereClauseConditions &= Environment.NewLine & " AND UPPER(" & Me.COL_NAME_DEALER_NAME & ")" & description.ToUpper
    '    End If

    '    If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
    '        whereClauseConditions &= Environment.NewLine & " AND UPPER(" & Me.COL_NAME_DEALER & ")" & code.ToUpper
    '    End If

    '    If Not whereClauseConditions = "" Then
    '        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
    '    Else
    '        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
    '    End If

    '    selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
    '                                       Environment.NewLine & "ORDER BY " & Environment.NewLine & Me.COL_NAME_DEALER_NAME & ", " & Me.COL_NAME_DEALER)
    '    Try
    '        Dim ds = New DataSet
    '        Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try

    'End Function
    Public Function LoadList(descriptionMask As String, codeMask As String, dealer_group_id As Guid, compIds As ArrayList) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(descriptionMask, codeMask)

        If bIsLikeClause = True Then
            ' hextoraw
            inCausecondition &= MiscUtil.BuildListForSql("d." & COL_NAME_COMPANY_ID, compIds, True)
        Else
            ' not HextoRaw
            inCausecondition &= MiscUtil.BuildListForSql("d." & COL_NAME_COMPANY_ID, compIds, False)
        End If


        If Not IsNothing(dealer_group_id) Then
            whereClauseConditions &= " AND g.dealer_group_id = '" & GuidToSQLString(dealer_group_id) & "'"
        End If

        If ((Not (descriptionMask Is Nothing)) AndAlso (FormatSearchMask(descriptionMask))) Then
            whereClauseConditions &= " AND UPPER(" & COL_NAME_DEALER_NAME & ")" & descriptionMask.ToUpper
        End If

        If ((Not (codeMask Is Nothing)) AndAlso (FormatSearchMask(codeMask))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & COL_NAME_DEALER & ")" & codeMask.ToUpper
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & COL_NAME_DEALER_NAME & ", " & COL_NAME_DEALER)
        Try
            'Dim ds = New DataSet
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Sub LoadListChild(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    '8/2/06 - ALR Modified function to accept a user guid (as a string) rather than a company id
    Public Function GetDealersWithMonthlyBilling(userId As String, todayDate As String) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_CONTRACT_MONTHLY_BILLING")
        Dim parameters(TOTAL_PARAM) As DBHelper.DBHelperParameter


        parameters(COMPANY_CODE) = New DBHelper.DBHelperParameter(COL_NAME_USER_ID, userId)
        parameters(TODAY_DATE) = New DBHelper.DBHelperParameter("today_date", todayDate)
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(DealerId As Guid, dealer_group_id As Guid, compIds As ArrayList) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        ' hextoraw
        inCausecondition &= MiscUtil.BuildListForSql("d." & COL_NAME_COMPANY_ID, compIds, True)


        If Not IsNothing(dealer_group_id) Then
            whereClauseConditions &= " AND g.dealer_group_id = '" & GuidToSQLString(dealer_group_id) & "'"
        End If

        If Not IsNothing(DealerId) Then
            whereClauseConditions &= " AND d.dealer_id = '" & GuidToSQLString(DealerId) & "'"
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & COL_NAME_DEALER_NAME & ", " & COL_NAME_DEALER)
        Try
            'Dim ds = New DataSet
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetDealerTypeId(dealerId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/GET_DEALER_TYPE_ID")

        Try
            Dim ds As New DataSet

            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Dealer_id", dealerId)}
            ' Dim dealerIdPar As New DBHelper.DBHelperParameter(dealerId)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadDealerListCertAddEnabled(companyIds As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/CERT_ADD_ENABLED_DEALER_LIST")
        Dim WhereClause As String = " and " & MiscUtil.BuildListForSql("c.company_id ", companyIds, False)
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, WhereClause)
        Dim dsDealer As DataSet
        Try
            dsDealer = ElitaPlus.DALObjects.DBHelper.Fetch(selectStmt, "Dealer")
            If companyIds.Count > 1 Then 'Use the description with company code if more than one companies
                dsDealer.Tables(0).Columns("description").ColumnName = "DescSingle"
                dsDealer.Tables(0).Columns("descriptionDual").ColumnName = "description"
            End If
            Return dsDealer
        Catch ex As Exception
            Throw New DataBaseAccessException(ElitaPlus.DALObjects.DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function





    Public Function GetDealerCountry(dealerId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/DEALER_COUNTRY")

        Try
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Dealer_id", dealerId)}
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim addressDAL As New AddressDAL
        Dim ScvOrderByDealerDAL As New ServiceOrdersAddressDAL
        Dim DlrClmAproveClmtypeDAL As New DealerClmAproveClmtypeDAL
        Dim DlrClmAproveCovtypeDAL As New DealerClmAproveCovtypeDAL
        Dim oAttributeValueDAL As New AttributeValueDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            ScvOrderByDealerDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            DlrClmAproveClmtypeDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            DlrClmAproveCovtypeDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            oAttributeValueDAL.Update(familyDataset.GetChanges(), tr)
            addressDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            ScvOrderByDealerDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            DlrClmAproveClmtypeDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            DlrClmAproveCovtypeDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            'At the end delete the Address
            addressDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try

    End Sub


    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "StoreProcedures Control"

    ' Execute Store Procedure
    Public Function ExecuteSP(docType As String, IdentificationNumber As String) As String
        Dim dal As New CertificateDAL
        Return dal.ExecuteSP(docType, IdentificationNumber)
    End Function

#End Region

#Region "Extended Functionality: New Dealer Definitions"
    Public Function dealerProviderClassCode(dealerCode As String, providerType As String) As String
        Dim selectStmt As String = Config("/SQL/GET_DEALER_PROVIDER_CLASS_CODE")
        Dim providerClassCode As String = String.Empty

        Using command As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure)
            command.BindByName = True
            command.AddParameter(ParINameDealerCode, OracleDbType.Varchar2, 5, dealerCode, ParameterDirection.Input)
            command.AddParameter(ParINameProviderType, OracleDbType.Varchar2, 100, providerType, ParameterDirection.Input)
            command.AddParameter("pReturnValue", OracleDbType.Varchar2, 100, Nothing, ParameterDirection.ReturnValue)

            Try
                OracleDbHelper.ExecuteNonQuery(command)
                providerClassCode = command.Parameters.Item("pReturnValue").Value.ToString
                Return providerClassCode
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Using
    End Function
    Public Function GetDealerProductCodesCount(dealerID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_DEALER_PRODUCT_CODES_COUNT")
        Dim parameters(TOTAL_PARAM_DEALER_PRODUCTCODES_COUNT) As DBHelper.DBHelperParameter

        parameters(DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerID.ToByteArray())

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, DEALER_PRODUCT_CODES_COUNT_TABLE, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function GetDealerCoveragesCount(dealerID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_DEALER_COVERAGES_COUNT")
        Dim parameters(TOTAL_PARAM_DEALER_COVERAGES_COUNT) As DBHelper.DBHelperParameter

        parameters(DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerID.ToByteArray())

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, DEALER_COVERAGES_COUNT_TABLE, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function GetDealerCertificatesCount(dealerID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_DEALER_CERTIFICATES_COUNT")
        Dim parameters(TOTAL_PARAM_DEALER_CERTIFICATES_COUNT) As DBHelper.DBHelperParameter

        parameters(DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, DALBase.GuidToSQLString(dealerID))

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, DEALER_CERTIFICATES_COUNT_TABLE, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetDupicateDealerCount(dealer As String, country As Guid, company_type_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_DUPLICATE_DEALER_COUNT")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                                                                              New DBHelper.DBHelperParameter(COL_NAME_DEALER, dealer),
                                                                                              New DBHelper.DBHelperParameter(COL_NAME_COMPANY_TYPE_ID, company_type_id.ToByteArray()),
                                                                                              New DBHelper.DBHelperParameter(COL_NAME_BUSINESS_COUNTRY_ID, country.ToByteArray())}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, DUPLICATE_DEALER_TABLE, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadDealerCountByCode(dealer As String) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_DEALER_COUNT_BY_CODE")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                                                                                              New DBHelper.DBHelperParameter(COL_NAME_DEALER, dealer)}
        'New DBHelper.DBHelperParameter(COL_NAME_BUSINESS_COUNTRY_ID, country.ToByteArray())}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CopyDealerDefinitions(fromDealerID As Guid, toDealerID As Guid, intCopyLevel As Integer, effdate As Date, expdate As Date) As Integer
        Dim selectStmt As String = Config("/SQL/COPY_DEALER_DEFINITIONS")
        Dim inputParameters(TOTAL_PARAM_COPY_DEALER_DEFINITIONS) As DBHelper.DBHelperParameter
        Dim outputParameter(0) As DBHelper.DBHelperParameter

        inputParameters(FROM_DEALER) = New DBHelper.DBHelperParameter(COL_NAME_FROM_DEALER, fromDealerID)
        inputParameters(TO_DEALER) = New DBHelper.DBHelperParameter(COL_NAME_TO_DEALER, toDealerID)
        inputParameters(COPY_LEVEL) = New DBHelper.DBHelperParameter(COL_NAME_COPY_LEVEL, intCopyLevel, GetType(Integer))
        'If effdate = Nothing Then
        inputParameters(EFF_DATE) = New DBHelper.DBHelperParameter(COL_NAME_EFF_DATE, effdate)
        'Else
        '    inputParameters(EFF_DATE) = New DBHelper.DBHelperParameter(COL_NAME_EFF_DATE, effdate, GetType(Date))
        'End If
        'If effdate = Nothing Then
        inputParameters(EXP_DATE) = New DBHelper.DBHelperParameter(COL_NAME_EXP_DATE, expdate)
        'Else
        '    inputParameters(EXP_DATE) = New DBHelper.DBHelperParameter(COL_NAME_EXP_DATE, expdate, GetType(Date))
        'End If
        outputParameter(0) = New DBHelper.DBHelperParameter(COL_NAME_RETURN, GetType(Integer))

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        Else
            Return 0
        End If

    End Function


    Public Function CreateExternalTable(dealerId As Guid, ByRef fullfileprocessId As Guid)
        Dim selectStmt As String = Config("/SQL/CREATE_EXTERNAL_TABLE")
        Dim inputParameters(1) As DBHelper.DBHelperParameter
        inputParameters(0) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)
        inputParameters(1) = New DBHelper.DBHelperParameter(COL_NAME_USEFULLFILEPROCESS_ID, fullfileprocessId.ToByteArray)
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        outputParameter(0) = New DBHelper.DBHelperParameter(COL_NAME_RETURN, GetType(Integer))
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        Else
            Return 0
        End If
    End Function

    'REQ-5467 : Used following method to Update Claims Asynchronously for when Lawsuit is made Mandatory
    'This Function can be made generic with additional parameters for any specific Task
    Public Function UpdateClaimsAsync(dealerId As Guid, claimUpdateOption As Integer)
        Dim selectStmt As String = Config("/SQL/UPDATE_CLAIMS_ASYNC")
        Dim inputParameters(1) As DBHelper.DBHelperParameter
        inputParameters(0) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)
        inputParameters(1) = New DBHelper.DBHelperParameter(COL_CLAIM_UPDATE_OPTION, claimUpdateOption)
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        outputParameter(0) = New DBHelper.DBHelperParameter(COL_NAME_RETURN, GetType(Integer))
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        Else
            Return 0
        End If
    End Function


    Public Function RenewCoverage(fromDealerID As Guid, contractID As Guid, effdate As Date) As Integer
        Dim selectStmt As String = Config("/SQL/RENEW_COVERAGES")
        Dim inputParameters(TOTAL_PARAM_RENEW_COVERAGES) As DBHelper.DBHelperParameter
        Dim outputParameter(0) As DBHelper.DBHelperParameter

        inputParameters(RENEW_FROM_DEALER) = New DBHelper.DBHelperParameter(COL_NAME_FROM_DEALER, fromDealerID)
        inputParameters(RENEW_CONTRACT) = New DBHelper.DBHelperParameter(COL_NAME_CONTRACT, contractID)
        inputParameters(RENEW_EFF_DATE) = New DBHelper.DBHelperParameter(COL_NAME_EFF_DATE, effdate)
        outputParameter(0) = New DBHelper.DBHelperParameter(COL_NAME_RETURN, GetType(Integer))

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

        If outputParameter(0).Value <> 0 And outputParameter(0).Value <> -1 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        Else
            Return outputParameter(0).Value
        End If

    End Function

    Public Function DeleteDealerDefinitions(fromDealerID As Guid, intDeleteLevel As Integer) As Integer
        'There are 4 Stored Procedures written for this process:
        'Procedure DeleteProductCodes. If called with deletelevel: 
        '                                   1,2,3,4: Product Codes will be deleted

        'Procedure DeleteItems.  If called with deletelevel:
        '                                   4: Items and Product Codes will be deleted
        '                                   3,2,1: Items will be deleted

        'Procedure DeleteCoverages. If called with deletelevel:
        '                                   3: Coverages, Items and Product Codes will be deleted
        '                                   2,1: Coverages will be deleted

        'Procedure DeleteCoverageRates. If called with deletelevel:
        '                                   4: all definitions will be deleted
        '                                   3: CoverageRates, Coverages and Items will be deleted
        '                                   2: CoverageRates and Coverages will be deleted
        '                                   1: CoverageRates will be deleted

        Dim selectStmt As String
        Dim inputParameters(TOTAL_PARAM_DELETE_DEALER_DEFINITIONS) As DBHelper.DBHelperParameter
        Dim outputParameter(0) As DBHelper.DBHelperParameter

        If intDeleteLevel = DELETE_ALL_DEALER_DEFINITIONS Then
            selectStmt = Config("/SQL/DELETE_ALL_DEALER_DEFINITIONS")
            inputParameters(FROM_DEALER) = New DBHelper.DBHelperParameter(COL_NAME_FROM_DEALER, fromDealerID)
            inputParameters(FROM_COVERAGE) = New DBHelper.DBHelperParameter(COL_NAME_FROM_COVERAGE_ID, System.DBNull.Value)
            inputParameters(DELETE_LEVEL) = New DBHelper.DBHelperParameter(COL_NAME_COPY_LEVEL, DELETE_ALL_DEALER_DEFINITIONS, GetType(Integer))
        ElseIf intDeleteLevel = DELETE_DEALER_PRODUCTCODES_AND_ITEMS Then
            selectStmt = Config("/SQL/DELETE_DEALER_PRODUCTCODES_AND_ITEMS")
            inputParameters(FROM_DEALER) = New DBHelper.DBHelperParameter(COL_NAME_FROM_DEALER, fromDealerID)
            inputParameters(FROM_PRODUCT_CODE) = New DBHelper.DBHelperParameter(COL_NAME_FROM_PRODUCT_CODE_ID, System.DBNull.Value)
            inputParameters(DELETE_LEVEL) = New DBHelper.DBHelperParameter(COL_NAME_COPY_LEVEL, DELETE_ALL_DEALER_DEFINITIONS, GetType(Integer))
        End If

        outputParameter(0) = New DBHelper.DBHelperParameter(COL_NAME_RETURN, GetType(Integer))

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        Else
            Return 0
        End If

    End Function

    Public Function GetDuplicatePrefixCount(companyID As Guid, Optional ByVal countLevel As Integer = 1, Optional ByVal certificatesAutonumberPrefix As String = "") As DataSet
        Dim selectStmt As String = Config("/SQL/GET_DEALERS_HAVING_DUPLICATE_CERTIFICATES_AUTONUMBER_PREFIX_COUNT")
        Dim parameters(1) As DBHelper.DBHelperParameter

        parameters(COMPANY_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyID.ToByteArray())
        parameters(COUNT_LEVEL) = New DBHelper.DBHelperParameter(COL_NAME_COUNT_LEVEL, countLevel)

        If Not certificatesAutonumberPrefix Is Nothing AndAlso Not certificatesAutonumberPrefix.Equals(String.Empty) Then
            Dim WhereClause As String = " and upper(CERTIFICATES_AUTONUMBER_PREFIX) = '" & certificatesAutonumberPrefix.ToUpper & "'"
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, WhereClause)
        End If

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, DUPLICATE_PREFIX_COUNT_TABLE, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region
#Region "Children Related"

    Public Function IsSkipActiveClaim(dealerID As Guid) As Boolean
        Dim selectStmt As String = Config("/SQL/CHECK_SKIP_ACTIVE_CLAIM")
        Dim returnValue As Int16 = 0
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerID.ToByteArray)}
        Try
            returnValue = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (returnValue > 0) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadAvailableClaimTypes(dealerID As Guid, languageId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_AVAILABLE_CLAIM_TYPES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray),
                                            New OracleParameter(COL_NAME_DEALER_ID, dealerID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME_CLAIM_TYPES, parameters)
    End Function

    Public Function LoadAvailableCoverageTypes(dealerID As Guid, languageId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_AVAILABLE_COVERAGE_TYPES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray),
                                            New OracleParameter(COL_NAME_DEALER_ID, dealerID.ToByteArray),
                                            New OracleParameter(COL_NAME_DEALER_ID, dealerID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME_COVERAGE_TYPES, parameters)
    End Function

    Public Function LoadSelectedClaimTypes(dealerID As Guid, languageId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_SELECTED_CLAIM_TYPES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_DEALER_ID, dealerID.ToByteArray),
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME_CLAIM_TYPES, parameters)
    End Function

    Public Function LoadSelectedCoverageTypes(dealerID As Guid, languageId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_SELECTED_COVERAGE_TYPES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_DEALER_ID, dealerID.ToByteArray),
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME_COVERAGE_TYPES, parameters)
    End Function


#End Region

End Class


