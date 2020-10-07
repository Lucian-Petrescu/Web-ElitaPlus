'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/20/2004)********************


Public Class ContractDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CONTRACT"
    Public Const DEALER_TABLE_NAME As String = "ELP_DEALER"
    Public Const TABLE_KEY_NAME As String = "contract_id"

    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_CONTRACT_ID As String = "contract_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_CONTRACT_TYPE_ID As String = "contract_type_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_COMMISSIONS_PERCENT As String = "commissions_percent"
    Public Const COL_NAME_MARKETING_PERCENT As String = "marketing_percent"
    Public Const COL_NAME_ADMIN_EXPENSE As String = "admin_expense"
    Public Const COL_NAME_PROFIT_PERCENT As String = "profit_percent"
    Public Const COL_NAME_LOSS_COST_PERCENT As String = "loss_cost_percent"
    Public Const COL_NAME_CURRENCY_ID As String = "currency_id"

    'US - 489857
    Public Const COL_NAME_COMMISSIONS_PERCENT_SOURCE_XCD As String = "commissions_percent_source_xcd"
    Public Const COL_NAME_MARKETING_PERCENT_SOURCE_XCD As String = "marketing_percent_source_xcd"
    Public Const COL_NAME_ADMIN_EXPENSE_SOURCE_XCD As String = "admin_expense_source_xcd"
    Public Const COL_NAME_PROFIT_PERCENT_SOURCE_XCD As String = "profit_percent_source_xcd"
    Public Const COL_NAME_LOSS_COST_PERCENT_SOURCE_XCD As String = "loss_cost_percent_source_xcd"

    ' Public Const COL_NAME_LAST_RECON As String = "last_recon"
    Public Const COL_NAME_TYPE_OF_MARKETING_ID As String = "type_of_marketing_id"
    Public Const COL_NAME_TYPE_OF_EQUIPMENT_ID As String = "type_of_equipment_id"
    Public Const COL_NAME_TYPE_OF_INSURANCE_ID As String = "type_of_insurance_id"
    Public Const COL_NAME_MIN_REPLACEMENT_COST As String = "min_replacement_cost"
    Public Const COL_NAME_WARRANTY_MAX_DELAY As String = "warranty_max_delay"
    Public Const COL_NAME_REMAINING_MFG_DAYS As String = "Remaining_mfg_days"
    Public Const COL_NAME_NET_COMMISSIONS_ID As String = "net_commissions_id"
    Public Const COL_NAME_NET_MARKETING_ID As String = "net_marketing_id"
    Public Const COL_NAME_NET_TAXES_ID As String = "net_taxes_id"
    Public Const COL_NAME_DEDUCTIBLE As String = "deductible"
    Public Const COL_NAME_WAITING_PERIOD As String = "waiting_period"
    Public Const COL_NAME_FUNDING_SOURCE_ID As String = "funding_source_id"
    Public Const COL_NAME_POLICY As String = "policy"
    'Req-1016 - start
    Public Const COL_NAME_MONTHLY_BILLING_ID As String = "monthly_billing_id"
    Public Const COL_NAME_RECURRING_PREMIUM_ID As String = "recurring_premium_id"
    Public Const COL_NAME_RECURRING_WARRANTY_PERIOD As String = "recurring_warranty_period"
    'Req-1016 - end
    Public Const COL_NAME_EDIT_MODEL_ID As String = "edit_model_id"
    Public Const COL_NAME_DEALER_MARKUP_ID As String = "dealer_markup_id"
    Public Const COL_NAME_FIXED_ESC_DURATION_FLAG As String = "fixed_esc_duration_flag"
    Public Const COL_NAME_AUTO_MFG_COVERAGE_ID As String = "auto_mfg_coverage_id"
    Public Const COL_NAME_RESTRICT_MARKUP_ID As String = "restrict_markup_id"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_SUSPENSE_DAYS As String = "suspense_days"
    Public Const COL_NAME_CANCELLATION_DAYS As String = "cancellation_days"
    Public Const COL_NAME_COMMENT1 As String = "comment1"
    Public Const COL_NAME_REPLACEMENT_POLICY_ID As String = "replacement_policy_id"
    Public Const COL_NAME_PARTICIPATION_PERCENT As String = "participation_percent"
    Public Const COL_NAME_COINSURANCE_ID As String = "coinsurance_id"
    Public Const COL_NAME_RATING_PLAN As String = "rating_plan"
    Public Const COL_NAME_ID_VALIDATION_ID As String = "id_validation_id"
    Public Const COL_NAME_ACSEL_PROD_CODE_ID As String = "acsel_prod_code_id"
    Public Const COL_NAME_CLAIM_CONTROL_ID As String = "claim_control_id"
    Public Const COL_NAME_IGNORE_INCOMING_PREMIUM_ID As String = "Ignore_Incoming_Premium_ID"
    Public Const COL_NAME_CURRENCY_CONVERSION_ID As String = "currency_conversion_id"
    Public Const COL_NAME_CURRENCY_OF_COVERAGES_ID As String = "currency_of_coverages_id"
    Public Const COL_NAME_AUTO_SET_LIABILITY_ID As String = "auto_set_liability_id"
    Public Const COL_NAME_DEDUCTIBLE_PERCENT As String = "deductible_percent"
    Public Const COL_NAME_COVERAGE_DEDUCTIBLE_ID As String = "coverage_deductible_id"
    Public Const COL_NAME_BACKEND_CLAIMS_ALLOWED_ID As String = "backend_claims_allowed_id"
    Public Const COL_NAME_DEDUCTIBLE_BASED_ON_ID As String = "deductible_based_on_id"
    Public Const COL_NAME_PRO_RATA_METHOD_ID As String = "pro_rata_method_id"
    Public Const COL_NAME_PAY_OUTSTANDING_PREMIUM_ID As String = "pay_outstanding_premium_id"

    Public Const COL_NAME_CONCELLATION_REASON_ID As String = "cancellation_reason_id"
    Public Const COL_NAME_FULL_REFUND_DAYS As String = "full_refund_days"
    Public Const COL_NAME_REPAIR_DISCOUNT_PCT As String = "repair_discount_pct"
    Public Const COL_NAME_REPLACEMENT_DISCOUNT_PCT As String = "replacement_discount_pct"
    Public Const COL_NAME_IGNORE_COVERAGE_AMT_ID As String = "ignore_coverage_amt_id"
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
    Public Const COL_NAME_MAX_INSTALLMENTS As String = "max_installments"
    Public Const COL_NAME_BILLING_CYCLE_FREQUENCY As String = "billing_cycle_frequency"
    Public Const COL_NAME_INSTALLMENTS_BASE_REDUCER As String = "installments_base_reducer"
    Public Const COL_NAME_PAST_DUE_MONTHS_ALLOWED As String = "past_due_months_allowed"
    Public Const COL_NAME_COLLECTION_RE_ATTEMPTS As String = "collection_re_attempts"
    Public Const COL_NAME_INCLUDE_FIRST_PMT As String = "include_first_pmt"
    Public Const COL_NAME_COLLECTION_CYCLE_TYPE_ID As String = "collection_cycle_type_id"
    Public Const COL_NAME_CYCLE_DAY As String = "cycle_day"
    Public Const COL_NAME_OFFSET_BEFORE_DUE_DATE As String = "offset_before_due_date"

    'Search column
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"

    Public Const COL_MIN_EFECTIVE As String = "min_efective"
    Public Const COL_MAX_EXPIRATION As String = "max_expiration"

    Public Const CERTIFICATE_COUNT_TABLE As String = "CERTIFICATE_COUNT_TABLE"
    Public Const COL_NAME_CERT_COUNT As String = "Cert_Count"
    Public Const COL_NAME_EDIT_MFG_TERM_ID As String = "edit_mfg_term_id"

    Public Const COL_NAME_INS_PREMIUM_FACTOR As String = "ins_premium_factor"
    Public Const COL_NAME_EXTEND_COVERAGE_ID As String = "extend_coverage_id"
    Public Const COL_NAME_EXTRA_MONS_TO_EXTEND_COVERAGE As String = "extra_mons_to_extend_coverage"
    Public Const COL_NAME_EXTRA_DAYS_TO_EXTEND_COVERAGE As String = "extra_days_to_extend_coverage"
    Public Const COL_NAME_ALLOW_DIFFERENT_COVERAGE As String = "allow_different_coverage"
    Public Const COL_NAME_ALLOW_NO_EXTENDED As String = "ALLOW_NO_EXTENDED"
    Public Const COL_NAME_NUM_OF_CLAIMS As String = "num_of_claims"
    Public Const COL_NAME_CLAIM_LIMIT_BASED_ON_ID As String = "claim_limit_based_on_id"
    Public Const COL_NAME_DAYS_TO_REPORT_CLAIM As String = "days_to_report_claim"
    Public Const COL_NAME_CUST_ADDRESS_REQUIRED_ID As String = "cust_address_required_id"
    Public Const COL_NAME_FIRST_PYMT_MONTHS As String = "first_pymt_months"
    Public Const COL_NAME_ALLOW_MULTIPLE_REJECTIONS As String = "allow_multiple_rejections_id"
    Public Const COL_NAME_AUTHORIZED_AMOUNT_MAX_UPDATES As String = "authorized_amount_max_updates"
    Public Const COL_NAME_ALLOW_PYMT_SKIP_MONTHS As String = "allow_pymt_skip_months"
    Public Const COL_NAME_BILLING_CYCLE_TYPE_ID As String = "billing_cycle_type_id"
    Public Const COL_NAME_DAILY_RATE_BASED_ON_ID As String = "daily_rate_based_on_id"
    Public Const COL_NAME_ALLOW_BILLING_AFTER_CANCELLATION As String = "allow_billing_after_cncltn"
    Public Const COL_NAME_ALLOW_COLLECTION_AFTER_CANCELLATION As String = "allow_collctn_after_cncltn"
    Public Const COL_NAME_ALLOW_COVERAGE_MARKUP_DISTRIBUTION As String = "allow_coverage_markup_dtbn"
    Public Const COL_NAME_FUTURE_DATE_ALLOW_FOR_ID As String = "future_date_allow_for_id"
    Public Const COL_NAME_DEDUCTIBLE_EXPRESSION_ID As String = "deductible_expression_id"

    'Req-703 Start
    Public Const COL_NAME_MARKETING_PROMO_ID As String = "marketing_promo_id"
    'Req-703 End

    ''REQ-794
    Public Const COL_IGNORE_COVERAGE_RATE_ID As String = "ignore_coverage_rate_id"

    'REQ-1050 Start
    Public Const COL_NAME_NUMBER_OF_DAYS_TO_REACTIVATE = "number_of_days_to_reactivate"
    'REQ-1050 END

    'REQ-1333
    Public Const COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT As String = "replacement_policy_claim_count"

    'REQ-1344/DEF-21932
    Public Const COL_NAME_IGNORE_WAITING_PERIOD_WSD_PSD As String = "ignore_waiting_period_wsd_psd"

    Public Const COL_NAME_NUM_OF_REPAIR_CLAIMS As String = "num_of_repair_claims"
    Public Const COL_NAME_NUM_OF_REPLACEMENT_CLAIMS As String = "num_of_replacement_claims"
    'REQ-5773 Start
    Public Const COL_NAME_PAYMENT_PROCESSING_TYPE_ID As String = "payment_processing_type_id"
    Public Const COL_NAME_THIRD_PARTY_NAME As String = "third_party_name"
    Public Const COL_NAME_THIRD_PARTY_TAX_ID As String = "third_party_tax_id"
    Public Const COL_NAME_RDO_NAME As String = "rdo_name"
    Public Const COL_NAME_RDO_TAX_ID As String = "rdo_tax_id"
    Public Const COL_NAME_RDO_PERCENT As String = "rdo_percent"
    'REQ-5773 End

    Public Const COL_NAME_OVERRIDE_EDIT_MFG_TERM As String = "override_edit_mfg_term_xcd"

    Public Const COL_NAME_POLICY_TYPE_ID As String = "policy_type_id"
    Public Const COL_NAME_POLICY_GENERATION_ID As String = "policy_generation_id"
    Public Const COL_NAME_LINE_OF_BUSINESS_ID As String = "line_of_business_id"
    Public Const COL_NAME_PRODUCER_ID As String = "producer_id"

    Public Const PARAM_SEQUENCE_KEY As String = "pi_sequence_key"
    Public Const PARAM_SEQ_SOURCE_ID As String = "pi_source_id"
    Public Const PARAM_SOURCE_NAME As String = "pi_source"
    Public Const PARAM_SEQ_NUM_TO_RESTORE As String = "pi_sequence_to_restore"
    Public Const PARAM_NEW_SEQ_STARTING_NUM As String = "pi_new_seq_starting_num"
    Public Const PARAM_ERROR_MSG As String = "po_error_msg"
    Public Const PARAM_PO_RETURN As String = "po_return"

#End Region


#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "CRUD Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("contract_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Function LoadList(compIds As ArrayList, dealerId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        If compIds.Count > 0 Then
            Dim inClauseCondition As String
            inClauseCondition &= MiscUtil.BuildListForSql("AND D." & COL_NAME_COMPANY_ID, compIds, True)
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER,
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & COL_NAME_DEALER_NAME & ", " & COL_NAME_EFFECTIVE & " DESC")

        Try
            Dim ds As New DataSet
            Dim parameter As DBHelper.DBHelperParameter
            If dealerId.Equals(Guid.Empty) Then
                parameter = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, GenericConstants.WILDCARD)
            Else
                parameter = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)
            End If
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME,
                            New DBHelper.DBHelperParameter() {parameter})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadMaxExpirationContract(compId As Guid, dealerId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_MAX_EXPIRATION_CONTRACT")

        Try
            Dim ds As New DataSet
            Dim dealerIdPar As New DBHelper.DBHelperParameter(PAR_NAME_COMPANY_ID, dealerId)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME,
                            New DBHelper.DBHelperParameter() {dealerIdPar})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Function LoadMinEffectiveMaxExpiration(dealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_MIN_EFFECTIVE_MAX_EXPIRATION")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id_id", dealerId.ToByteArray)}
        Try
            Dim ds As New DataSet
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Function GetSertificatesCount(Id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_SERTIFICATES_COUNT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(TABLE_KEY_NAME, Id.ToByteArray)}
        Try
            Dim ds As New DataSet
            Return DBHelper.Fetch(ds, selectStmt, CERTIFICATE_COUNT_TABLE, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetContract(compIds As ArrayList, certId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/GET_CONTRACT_BY_CERT_ID")

        Dim inClauseCondition As String

        inClauseCondition &= MiscUtil.BuildListForSql("AND D." & COL_NAME_COMPANY_ID, compIds, True)
        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER,
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & COL_NAME_DEALER_NAME & ", " & COL_NAME_EFFECTIVE & " DESC")

        Try
            Dim ds As New DataSet
            Dim parameter As DBHelper.DBHelperParameter
            If certId.Equals(Guid.Empty) Then
                parameter = New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, GenericConstants.WILDCARD)
            Else
                parameter = New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray)
            End If
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME,
                            New DBHelper.DBHelperParameter() {parameter})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAutoGenSequenceNumber(seqSourceId As Guid, sourceName As String, sequenceKey As String) As Long

        Dim autoGenSeqNum As Long

        Dim selectStmt As String = Config("/SQL/GET_AUTO_GEN_SEQ_NUM")

        Using command As OracleCommand = CreateCommand(selectStmt, CommandType.StoredProcedure)

            command.BindByName = True
            command.AddParameter(PARAM_SEQ_SOURCE_ID, OracleDbType.Raw, 100, seqSourceId, ParameterDirection.Input)
            command.AddParameter(PARAM_SOURCE_NAME, OracleDbType.Varchar2, 25, sourceName, ParameterDirection.Input)
            command.AddParameter(PARAM_SEQUENCE_KEY, OracleDbType.Varchar2, 50, sequenceKey, ParameterDirection.Input)
            command.AddParameter(PARAM_NEW_SEQ_STARTING_NUM, OracleDbType.Int64, 12, 1, ParameterDirection.Input)

            command.AddParameter("pReturnValue", OracleDbType.Int64, 10, Nothing, ParameterDirection.ReturnValue)

            Try
                ExecuteNonQuery(command)
                Int64.TryParse(command.Parameters.Item("pReturnValue").Value.ToString(), autoGenSeqNum)

                If (autoGenSeqNum <= 0) Then
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, Nothing)
                End If

                Return autoGenSeqNum

            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Using

    End Function

    Public Sub ReturnUnUsedAutoGenSequenceNumber(autoGenSequenceNumber As Long, seqSourceId As Guid, sequenceKey As String)

        Dim selectStmt As String = Config("/SQL/RETURN_UN_USED_AUTO_GEN_SEQ_NUM")

        Using command As OracleCommand = CreateCommand(selectStmt, CommandType.StoredProcedure)
            command.BindByName = True

            command.AddParameter(PARAM_SEQUENCE_KEY, OracleDbType.Varchar2, 50, sequenceKey, ParameterDirection.Input)
            command.AddParameter(PARAM_SEQ_SOURCE_ID, OracleDbType.Raw, 100, seqSourceId, ParameterDirection.Input)
            command.AddParameter(PARAM_SEQ_NUM_TO_RESTORE, OracleDbType.Long, 5, autoGenSequenceNumber, ParameterDirection.Input)

            command.AddParameter(PARAM_ERROR_MSG, OracleDbType.Varchar2, 10, Nothing, ParameterDirection.Output)
            command.AddParameter(PARAM_PO_RETURN, OracleDbType.Int16, 10, Nothing, ParameterDirection.Output)

            Try

                ExecuteNonQuery(command)

            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Using
    End Sub

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet,
                                      dealerId As Guid, expirationDate As Date, effectiveDate As Date,
                                      Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim oCoverageDAL As New CoverageDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)

            oCoverageDAL.ExpireCoverages(dealerId, expirationDate, effectiveDate, tr)

            ' Ind Policy change if Data set has Dealer table then update it.
            If familyDataset.Tables.Contains(DEALER_TABLE_NAME) Then
                Dim dealerDAL As New DealerDAL
                dealerDAL.Update(familyDataset.Tables(DEALER_TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

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
#End Region

End Class

