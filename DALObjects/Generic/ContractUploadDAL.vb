'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/25/2016)********************


Public Class ContractUploadDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CONTRACT_UPLOAD"
    Public Const TABLE_KEY_NAME As String = "contract_upload_id"

    Public Const COL_NAME_CONTRACT_UPLOAD_ID As String = "contract_upload_id"
    Public Const COL_NAME_UPLOAD_SESSION_ID As String = "upload_session_id"
    Public Const COL_NAME_RECORD_NUMBER As String = "record_number"
    Public Const COL_NAME_VALIDATION_ERRORS As String = "validation_errors"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_CONTRACT_TYPE_ID As String = "contract_type_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_COMMISSIONS_PERCENT As String = "commissions_percent"
    Public Const COL_NAME_MARKETING_PERCENT As String = "marketing_percent"
    Public Const COL_NAME_ADMIN_EXPENSE As String = "admin_expense"
    Public Const COL_NAME_PROFIT_PERCENT As String = "profit_percent"
    Public Const COL_NAME_LOSS_COST_PERCENT As String = "loss_cost_percent"
    Public Const COL_NAME_CURRENCY_ID As String = "currency_id"
    Public Const COL_NAME_TYPE_OF_MARKETING_ID As String = "type_of_marketing_id"
    Public Const COL_NAME_TYPE_OF_EQUIPMENT_ID As String = "type_of_equipment_id"
    Public Const COL_NAME_TYPE_OF_INSURANCE_ID As String = "type_of_insurance_id"
    Public Const COL_NAME_MIN_REPLACEMENT_COST As String = "min_replacement_cost"
    Public Const COL_NAME_WARRANTY_MAX_DELAY As String = "warranty_max_delay"
    Public Const COL_NAME_NET_COMMISSIONS_ID As String = "net_commissions_id"
    Public Const COL_NAME_NET_MARKETING_ID As String = "net_marketing_id"
    Public Const COL_NAME_NET_TAXES_ID As String = "net_taxes_id"
    Public Const COL_NAME_DEDUCTIBLE As String = "deductible"
    Public Const COL_NAME_WAITING_PERIOD As String = "waiting_period"
    Public Const COL_NAME_FUNDING_SOURCE_ID As String = "funding_source_id"
    Public Const COL_NAME_EDIT_MODEL_ID As String = "edit_model_id"
    Public Const COL_NAME_DEALER_MARKUP_ID As String = "dealer_markup_id"
    Public Const COL_NAME_AUTO_MFG_COVERAGE_ID As String = "auto_mfg_coverage_id"
    Public Const COL_NAME_RESTRICT_MARKUP_ID As String = "restrict_markup_id"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_SUSPENSE_DAYS As String = "suspense_days"
    Public Const COL_NAME_CANCELLATION_DAYS As String = "cancellation_days"
    Public Const COL_NAME_COMMENT1 As String = "comment1"
    Public Const COL_NAME_FIXED_ESC_DURATION_FLAG As String = "fixed_esc_duration_flag"
    Public Const COL_NAME_POLICY As String = "policy"
    Public Const COL_NAME_REPLACEMENT_POLICY_ID As String = "replacement_policy_id"
    Public Const COL_NAME_COINSURANCE_ID As String = "coinsurance_id"
    Public Const COL_NAME_PARTICIPATION_PERCENT As String = "participation_percent"
    Public Const COL_NAME_ID_VALIDATION_ID As String = "id_validation_id"
    Public Const COL_NAME_CLAIM_CONTROL_ID As String = "claim_control_id"
    Public Const COL_NAME_RATING_PLAN As String = "rating_plan"
    Public Const COL_NAME_CURRENCY_CONVERSION_ID As String = "currency_conversion_id"
    Public Const COL_NAME_CURRENCY_OF_COVERAGES_ID As String = "currency_of_coverages_id"
    Public Const COL_NAME_REMAINING_MFG_DAYS As String = "remaining_mfg_days"
    Public Const COL_NAME_ACSEL_PROD_CODE_ID As String = "acsel_prod_code_id"
    Public Const COL_NAME_CANCELLATION_REASON_ID As String = "cancellation_reason_id"
    Public Const COL_NAME_FULL_REFUND_DAYS As String = "full_refund_days"
    Public Const COL_NAME_AUTO_SET_LIABILITY_ID As String = "auto_set_liability_id"
    Public Const COL_NAME_DEDUCTIBLE_PERCENT As String = "deductible_percent"
    Public Const COL_NAME_COVERAGE_DEDUCTIBLE_ID As String = "coverage_deductible_id"
    Public Const COL_NAME_IGNORE_INCOMING_PREMIUM_ID As String = "ignore_incoming_premium_id"
    Public Const COL_NAME_REPAIR_DISCOUNT_PCT As String = "repair_discount_pct"
    Public Const COL_NAME_REPLACEMENT_DISCOUNT_PCT As String = "replacement_discount_pct"
    Public Const COL_NAME_IGNORE_COVERAGE_AMT_ID As String = "ignore_coverage_amt_id"
    Public Const COL_NAME_BACKEND_CLAIMS_ALLOWED_ID As String = "backend_claims_allowed_id"
    Public Const COL_NAME_EDIT_MFG_TERM_ID As String = "edit_mfg_term_id"
    Public Const COL_NAME_ACCT_BUSINESS_UNIT_ID As String = "acct_business_unit_id"
    Public Const COL_NAME_INSTALLMENT_PAYMENT_ID As String = "installment_payment_id"
    Public Const COL_NAME_DAYS_OF_FIRST_PYMT As String = "days_of_first_pymt"
    Public Const COL_NAME_DAYS_TO_SEND_LETTER As String = "days_to_send_letter"
    Public Const COL_NAME_DAYS_TO_CANCEL_CERT As String = "days_to_cancel_cert"
    Public Const COL_NAME_DEDUCT_BY_MFG_ID As String = "deduct_by_mfg_id"
    Public Const COL_NAME_PENALTY_PCT As String = "penalty_pct"
    Public Const COL_NAME_CLIP_PERCENT As String = "clip_percent"
    Public Const COL_NAME_IS_COMM_P_CODE_ID As String = "is_comm_p_code_id"
    Public Const COL_NAME_BASE_INSTALLMENTS As String = "base_installments"
    Public Const COL_NAME_BILLING_CYCLE_FREQUENCY As String = "billing_cycle_frequency"
    Public Const COL_NAME_MAX_INSTALLMENTS As String = "max_installments"
    Public Const COL_NAME_INSTALLMENTS_BASE_REDUCER As String = "installments_base_reducer"
    Public Const COL_NAME_PAST_DUE_MONTHS_ALLOWED As String = "past_due_months_allowed"
    Public Const COL_NAME_COLLECTION_RE_ATTEMPTS As String = "collection_re_attempts"
    Public Const COL_NAME_INCLUDE_FIRST_PMT As String = "include_first_pmt"
    Public Const COL_NAME_COLLECTION_CYCLE_TYPE_ID As String = "collection_cycle_type_id"
    Public Const COL_NAME_CYCLE_DAY As String = "cycle_day"
    Public Const COL_NAME_OFFSET_BEFORE_DUE_DATE As String = "offset_before_due_date"
    Public Const COL_NAME_INS_PREMIUM_FACTOR As String = "ins_premium_factor"
    Public Const COL_NAME_EXTEND_COVERAGE_ID As String = "extend_coverage_id"
    Public Const COL_NAME_EXTRA_MONS_TO_EXTEND_COVERAGE As String = "extra_mons_to_extend_coverage"
    Public Const COL_NAME_EXTRA_DAYS_TO_EXTEND_COVERAGE As String = "extra_days_to_extend_coverage"
    Public Const COL_NAME_ALLOW_DIFFERENT_COVERAGE As String = "allow_different_coverage"
    Public Const COL_NAME_ALLOW_NO_EXTENDED As String = "allow_no_extended"
    Public Const COL_NAME_NUM_OF_CLAIMS As String = "num_of_claims"
    Public Const COL_NAME_CLAIM_LIMIT_BASED_ON_ID As String = "claim_limit_based_on_id"
    Public Const COL_NAME_DAYS_TO_REPORT_CLAIM As String = "days_to_report_claim"
    Public Const COL_NAME_MARKETING_PROMO_ID As String = "marketing_promo_id"
    Public Const COL_NAME_CUST_ADDRESS_REQUIRED_ID As String = "cust_address_required_id"
    Public Const COL_NAME_FIRST_PYMT_MONTHS As String = "first_pymt_months"
    Public Const COL_NAME_ALLOW_MULTIPLE_REJECTIONS_ID As String = "allow_multiple_rejections_id"
    Public Const COL_NAME_DEDUCTIBLE_BASED_ON_ID As String = "deductible_based_on_id"
    Public Const COL_NAME_PRO_RATA_METHOD_ID As String = "pro_rata_method_id"
    Public Const COL_NAME_PAY_OUTSTANDING_PREMIUM_ID As String = "pay_outstanding_premium_id"
    Public Const COL_NAME_AUTHORIZED_AMOUNT_MAX_UPDATES As String = "authorized_amount_max_updates"
    Public Const COL_NAME_RECURRING_PREMIUM_ID As String = "recurring_premium_id"
    Public Const COL_NAME_RECURRING_WARRANTY_PERIOD As String = "recurring_warranty_period"
    Public Const COL_NAME_ALLOW_PYMT_SKIP_MONTHS As String = "allow_pymt_skip_months"
    Public Const COL_NAME_NUMBER_OF_DAYS_TO_REACTIVATE As String = "number_of_days_to_reactivate"
    Public Const COL_NAME_BILLING_CYCLE_TYPE_ID As String = "billing_cycle_type_id"
    Public Const COL_NAME_DAILY_RATE_BASED_ON_ID As String = "daily_rate_based_on_id"
    Public Const COL_NAME_ALLOW_BILLING_AFTER_CNCLTN As String = "allow_billing_after_cncltn"
    Public Const COL_NAME_ALLOW_COLLCTN_AFTER_CNCLTN As String = "allow_collctn_after_cncltn"
    Public Const COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT As String = "replacement_policy_claim_count"
    Public Const COL_NAME_FUTURE_DATE_ALLOW_FOR_ID As String = "future_date_allow_for_id"
    Public Const COL_NAME_IGNORE_WAITING_PERIOD_WSD_PSD As String = "ignore_waiting_period_wsd_psd"
    Public Const COL_NAME_ALLOW_COVERAGE_MARKUP_DTBN As String = "allow_coverage_markup_dtbn"
    Public Const COL_NAME_NUM_OF_REPAIR_CLAIMS As String = "num_of_repair_claims"
    Public Const COL_NAME_NUM_OF_REPLACEMENT_CLAIMS As String = "num_of_replacement_claims"
    Public Const COL_NAME_PAYMENT_PROCESSING_TYPE_ID As String = "payment_processing_type_id"
    Public Const COL_NAME_THIRD_PARTY_NAME As String = "third_party_name"
    Public Const COL_NAME_THIRD_PARTY_TAX_ID As String = "third_party_tax_id"
    Public Const COL_NAME_RDO_NAME As String = "rdo_name"
    Public Const COL_NAME_RDO_TAX_ID As String = "rdo_tax_id"
    Public Const COL_NAME_RDO_PERCENT As String = "rdo_percent"
    Public Const COL_NAME_POLICY_TYPE_CODE As String = "policy_type_code"
    Public Const COL_NAME_POLICY_GENERATION_CODE As String = "policy_generation_code"
    Public Const COL_NAME_LINE_OF_BUSINESS_CODE As String = "line_of_business_code"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("contract_upload_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadPreValidatedContractsForUpload(ByVal UploadSessionId As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_PREVALIDATED_CONTRACTS_FOR_UPLOAD")

        Try
            Dim ds As New DataSet
            Dim parameter As DBHelper.DBHelperParameter

            parameter = New DBHelper.DBHelperParameter(COL_NAME_UPLOAD_SESSION_ID, UploadSessionId) 'UploadSessionId.ToByteArray)

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME,
                            New DBHelper.DBHelperParameter() {parameter})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function UpdatePreValidatedContractRecord(preValidatedContractId As Guid, ByVal strValidationErrors As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/UPDATE_PREVALIDATED_CONTRACT_RECORD")

        Try
            Dim ds As New DataSet
            Dim parameter As DBHelper.DBHelperParameter

            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(Me.COL_NAME_CONTRACT_UPLOAD_ID, preValidatedContractId.ToByteArray) _
                         , New DBHelper.DBHelperParameter(Me.COL_NAME_VALIDATION_ERRORS, strValidationErrors)}

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class