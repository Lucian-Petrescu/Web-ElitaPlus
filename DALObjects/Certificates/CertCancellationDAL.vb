'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/3/2004)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper
'Imports BusinessObjects.common

#Region "CertCancellationData"

Public Class CertCancellationData

    Public companyId As Guid
    Public dealerId As Guid
    Public payment_method_Id As Guid ' , payment_reason_Id
    Public certificate, certificatestatus, source, cancellationCode, quote, creditIssued As String
    Public cancellationDate As Date
    Public customerPaid, computedRefund, principalPaid, refundAmount As Decimal
    Public refund_amt_reset As String
    Public refundAmountRcvd As Decimal

    Public grossAmtReceived As Decimal
    Public inputAmountRequiredMissing As Boolean
    Public refund_dest_id As Guid
    Public def_refund_payment_method_id As Guid
    Public errorExist, errorExist2 As Boolean
    Public errorCode, ErrorMsg As String
    Public InstallmentsPaid As Long

    Public ComputeRefundCode, paymentTypeCode, AuthNumber As String
End Class

#End Region


#Region "ReverseCancellationData"

Public Class ReverseCancellationData

    Public companyId As Guid
    Public dealerId As Guid
    Public certificate As String
    Public source As String
    Public sourceType As String

End Class
Public Class UpdateBankInfoForRejectsData
    Public certCancellationId As Guid
    Public bankInfoId As Guid
    Public IBANNumber As String
    Public accountNumber As String
End Class

#End Region

Public Class CertCancellationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CERT_CANCELLATION"
    Public Const TABLE_KEY_NAME As String = "cert_cancellation_id"
    Public Const TABLE_REFUND_COMPUTE_METHOD As String = "refund_compute_method"
    Public Const TABLE_POLICY_COST As String = "policy_cost"
    Public Const TABLE_REFUND_AMOUNT As String = "refund_amount"

    Public Const COL_NAME_CERT_CANCELLATION_ID As String = "cert_cancellation_id"
    Public Const COL_NAME_BANKINFO_ID As String = "bank_info_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_CANCELLATION_REASON_ID As String = "cancellation_reason_id"
    Public Const COL_NAME_COMMISSION_BREAKDOWN_ID As String = "commission_breakdown_id"
    Public Const COL_NAME_ORIGINAL_REGION_ID As String = "original_region_id"
    Public Const COL_NAME_REFUND_DEST_ID As String = "refund_dest_id"
    Public Const COL_NAME_CANCELLATION_DATE As String = "cancellation_date"
    Public Const COL_NAME_CANCELLATION_REQUESTED_DATE As String = "cancellation_requested_date"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_PROCESSED_DATE As String = "processed_date"
    Public Const COL_NAME_GROSS_AMT_RECEIVED As String = "gross_amt_received"
    Public Const COL_NAME_PREMIUM_WRITTEN As String = "premium_written"
    Public Const COL_NAME_ORIGINAL_PREMIUM As String = "original_premium"
    Public Const COL_NAME_LOSS_COST As String = "loss_cost"
    Public Const COL_NAME_COMMISSION As String = "commission"
    Public Const COL_NAME_ADMIN_EXPENSE As String = "admin_expense"
    Public Const COL_NAME_MARKETING_EXPENSE As String = "marketing_expense"
    Public Const COL_NAME_OTHER As String = "other"
    Public Const COL_NAME_SALES_TAX As String = "sales_tax"
    Public Const COL_NAME_TAX1 As String = "tax1"
    Public Const COL_NAME_TAX2 As String = "tax2"
    Public Const COL_NAME_TAX3 As String = "tax3"
    Public Const COL_NAME_TAX4 As String = "tax4"
    Public Const COL_NAME_TAX5 As String = "tax5"
    Public Const COL_NAME_TAX6 As String = "tax6"
    Public Const COL_NAME_COMPUTED_REFUND As String = "computed_refund"
    Public Const COL_NAME_CREDIT_ISSUED_FLAG As String = "credit_issued_flag"
    Public Const COL_NAME_CUSTOMER_PAID As String = "customer_paid"
    Public Const COL_NAME_PRINCIPAL_PAID As String = "principal_paid"
    Public Const COL_NAME_REFUND_AMT As String = "refund_amt"
    Public Const COL_NAME_ASSURANT_GWP As String = "assurant_gwp"
    Public Const COL_NAME_MARKUP_COMMISSION As String = "markup_commission"
    Public Const COL_NAME_PAYMENT_METHOD_ID As String = "payment_method_id"
    Public Const COL_NAME_PAYMENT_REASON_ID As String = "payment_reason_id"
    Public Const COL_NAME_PAY_REJECT_CODE_XCD As String = "Pay_Reject_Code_XCD"
    Public Const COL_NAME_REFUND_STATUS_XCD As String = "Refund_status_XCD"
    Public Const COL_NAME_REFUND_METHOD_XCD As String = "Refund_method_XCD"
    Public Const COL_REFUND_COMPUTE_METHOD_ID As String = "refund_compute_method_id"

    Public Const COL_POLICY_COST As String = "policy_cost"

    Public Const COL_NAME_TRACKING_NUMBER As String = "tracking_number"
    Public Const COL_NAME_STATUS_DATE As String = "status_date"
    Public Const COL_NAME_STATUS_ID As String = "status_id"

    Public Const COL_NAME_MARKUP_COMMISSION_VAT As String = "markup_commission_vat"


    ' Cancel Store Procedure Input Parameters
    Public Const P_COMPANY_ID = 0
    Public Const P_DEALER_ID = 1
    Public Const P_CERTIFICATE = 2
    Public Const P_INVOICE_NUMBER = 3
    Public Const P_SOURCE = 3
    Public Const P_SOURCE_TYPE = 4
    Public Const P_CANCELLATION_DATE = 4
    Public Const P_CANCELLATION_CODE = 5
    Public Const P_CANCELLATION_CODE_BY_INVOICE = 4
    Public Const P_CUSTOMER_PAID = 6
    Public Const P_QUOTE = 7
    Public Const P_PAYMENT_METHOD_ID = 8
    Public Const P_BANK_INFO_ID = 9
    Public Const P_BANK_ACCOUNT_NAME = 10
    Public Const P_BANK_ID = 11
    Public Const P_BANK_ACCOUNT_NUMBER = 12
    Public Const P_BANK_SWIFT_CODE = 13
    Public Const P_BANK_IBAN_NUMBER = 14
    Public Const P_BANK_ACCOUNT_TYPE_ID = 15
    Public Const P_BANK_COUNTRY_ID = 16
    Public Const P_BANK_PAYMENT_REASON_ID = 17

    Public Const P_COMMENT_ID = 18
    Public Const P_CALLER_NAME = 19
    Public Const P_COMMENT_TYPE_ID = 20
    Public Const P_COMMENTS = 21
    Public Const P_BRANCH_DIGIT = 22
    Public Const P_ACCOUNT_DIGIT = 23
    Public Const P_BRANCH_NUMBER = 24
    Public Const P_COMPUTED_REFUND_RCVD = 25
    Public Const P_INSTALLMENTS_PAID_RCVD = 26
    Public Const P_BANK_NAME = 27

    Public Const P_SOURCE_BY_INVOICE = 5
    Public Const P_CANCELLATION_DATE_BY_INVOICE = 6

    Public Const COL_NAME_P_COMPANY_ID As String = "p_company_id"
    Public Const COL_NAME_P_DEALER_ID As String = "p_dealer_id"
    Public Const COL_NAME_P_CERT_ID As String = "p_Cert_id"
    Public Const COL_NAME_P_CERTIFICATE As String = "p_certificate"
    Public Const COL_NAME_P_INVOICE_NUMBER As String = "p_invoice_number"
    Public Const COL_NAME_P_SOURCE As String = "p_source"
    Public Const COL_NAME_P_SOURCE_TYPE As String = "p_source_type"
    Public Const COL_NAME_P_USER_ID As String = "p_user_id"
    Public Const COL_NAME_P_CANCELLATION_DATE As String = "p_cancellation_date"
    Public Const COL_NAME_P_CANCELLATION_REQUESTED_DATE As String = "p_cancellation_requested_date"
    Public Const COL_NAME_P_CANCELLATION_CODE As String = "p_cancellation_code"
    Public Const COL_NAME_P_CUSTOMER_PAID As String = "p_customer_paid"
    Public Const COL_NAME_P_QUOTE As String = "p_quote"
    Public Const COL_NAME_P_PAYMENT_METHOD_ID As String = "p_payment_method_id"
    Public Const COL_NAME_P_BANK_INFO_ID As String = "p_bank_info_id"
    Public Const COL_NAME_BANK_ACCOUNT_NAME As String = "p_account_name"
    Public Const COL_NAME_P_BANK_ID As String = "p_bank_id"
    Public Const COL_NAME_P_BANK_ACCOUNT_NUMBER As String = "p_account_number"
    Public Const COL_NAME_P_BANK_COUNTRY_ID As String = "p_country_id"
    Public Const COL_NAME_P_BANK_PAYMENT_REASON_ID As String = "p_payment_reason_id"
    Public Const COL_NAME_P_BANK_SWIFT_CODE As String = "p_swift_code"
    Public Const COL_NAME_P_BANK_IBAN_NUMBER = "p_iban_number"
    Public Const COL_NAME_P_BANK_ACCOUNT_TYPE_ID = "p_account_type_id"
    Public Const COL_NAME_P_CURRENT_ODOMETER = "p_current_odometer"
    Public Const COL_NAME_P_MAX_ASSURANT_MILEAGE_LIMIT = "p_max_assurant_mileage_limit"

    Public Const COL_NAME_P_BRANCH_DIGIT As String = "p_branch_digit"
    Public Const COL_NAME_P_ACCOUNT_DIGIT As String = "p_account_digit"
    Public Const COL_NAME_P_BRANCH_NUMBER As String = "p_branch_number"
    Public Const COL_NAME_P_BANK_NAME As String = "p_bank_name"
    Public Const COL_NAME_P_CERT_CANCELLATION_ID As String = "p_cert_cancellation_id"

    ' Cancel Store Procedure Output Parameters
    Public Const P_COMPUTED_REFUND = 0
    Public Const P_CREDIT_ISSUED = 1
    Public Const P_PRINCIPAL_PAID = 2
    Public Const P_REFUND_AMOUNT = 3
    Public Const P_REFUND_AMOUNT_RESET = 4
    Public Const P_RETURN = 5
    Public Const P_EXCEPTION_MSG = 6
    Public Const P_INSTALLMENTS_PAID = 7
    Public Const P_AUTH_NUMBER = 8

    Public Const COL_NAME_P_COMPUTED_REFUND As String = "p_computed_refund"
    Public Const COL_NAME_P_CREDIT_ISSUED As String = "p_credit_issued"
    Public Const COL_NAME_P_PRINCIPAL_PAID As String = "p_principal_paid"
    Public Const COL_NAME_P_REFUND_AMOUNT As String = "p_refund_amount"
    Public Const COL_NAME_P_REFUND_AMOUNT_RESET As String = "p_refund_amt_reset"
    Public Const COL_NAME_P_RETURN As String = "p_return"
    Public Const COL_NAME_P_EXCEPTION_MSG As String = "p_exception_msg"
    Public Const COL_NAME_P_INSTALLMENTS_PAID As String = "p_installments_paid"
    Public Const COL_NAME_P_AUTH_NUMBER As String = "p_auth_number"

    'comment
    Public Const COL_NAME_P_COMMENT_ID As String = "p_comment_id"
    Public Const COL_NAME_P_CALLER_NAME As String = "p_caller_name"
    Public Const COL_NAME_P_COMMENT_TYPE_ID As String = "p_comment_type_id"
    Public Const COL_NAME_P_COMMENT As String = "p_comments"

    Public Const COL_NAME_P_COMPUTED_REFUND_RCVD As String = "p_computed_refund_rcvd"
    Public Const COL_NAME_P_INSTALLMENTS_PAID_RCVD As String = "p_installments_paid_rcvd"

    Public Const TOTAL_INPUT_PARAM_CANCEL = 27
    Public Const TOTAL_OUTPUT_PARAM_CANCEL = 8
    Public Const TOTAL_INOUTPUT_PARAM_CANCEL = 0
    Public Const TOTAL_INPUT_PARAM_SETFUTURECANCEL = 10
    Public Const TOTAL_OUTPUT_PARAM_SETFUTURECANCEL = 1

    ' Reverse Cancellation Store Procedure Input Parameters
    Public Const PR_COMPANY_ID = 0
    Public Const PR_DEALER_ID = 1
    Public Const PR_CERTIFICATE = 2

    ' Reverse Cancellation Store Procedure Output Parameters
    Public Const PR_RETURN = 0

    Public Const TOTAL_INPUT_PARAM_REVERSE_CANCEL = 4
    Public Const TOTAL_OUTPUT_PARAM_REVERSE_CANCEL = 0
    Public Const TOTAL_INPUT_PARAM_CANCEL_BY_INVOICE_NUMBER = 6
    Public Const TOTAL_OUTPUT_PARAM_CANCEL_BY_INVOICE_NUMBER = 0
    Public Const TOTAL_INPUT_PARAM_CANCEL_COVERAGES = 2
    Public Const TOTAL_OUTPUT_PARAM_CANCEL_COVERAGES = 0
    Public Const TOTAL_INPUT_PARAM_CANCEL_VSC_CERT = 6
    Public Const TOTAL_INPUT_PARAM_UPDBANKINFO_CANCEL = 3
    Public Const TOTAL_OUTPUT_PARAM_UPDBANKINFO_CANCEL = 1

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_cancellation_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function


    Public Function getRefundComputeMethodId(cancellationID As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_REFUND_COMPUTE_METHOD_ID")

        parameters = New OracleParameter() {New OracleParameter(COL_REFUND_COMPUTE_METHOD_ID, cancellationID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_REFUND_COMPUTE_METHOD, parameters)

    End Function

    Public Function getPolicyCost(certId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_POLICY_COST")

        parameters = New OracleParameter() {New OracleParameter(COL_POLICY_COST, certId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_POLICY_COST, parameters)
    End Function

    Public Function getCertCancellationId(certId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_CERT_CANCELLATION_ID")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CERT_ID, certId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_KEY_NAME, parameters)
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "StoreProcedures Control"

    Public Function ExecuteSetFutureCancelSP(oCertCancellationData As CertCancellationData,
                                   Optional ByVal oBankInfoData As BankInfoData = Nothing,
                                   Optional ByVal oCommentData As CommentData = Nothing,
                                   Optional ByVal cancellationRequestedDate As Date = Nothing) As Boolean

        Dim selectStmt As String = Config("/SQL/SET_FUTURE_CANCELLATION_DATE")
        Dim inputParameters(TOTAL_INPUT_PARAM_SETFUTURECANCEL) As DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_SETFUTURECANCEL) As DBHelperParameter

        With oCertCancellationData
            inputParameters(0) = New DBHelperParameter(COL_NAME_P_COMPANY_ID, .companyId.ToByteArray)
            inputParameters(1) = New DBHelperParameter(COL_NAME_P_DEALER_ID, .dealerId.ToByteArray)
            inputParameters(2) = New DBHelperParameter(COL_NAME_P_CERTIFICATE, .certificate)
            inputParameters(3) = New DBHelperParameter(COL_NAME_P_SOURCE, .source)
            inputParameters(4) = New DBHelperParameter(COL_NAME_P_CANCELLATION_DATE, .cancellationDate)
            inputParameters(5) = New DBHelperParameter(COL_NAME_P_CANCELLATION_REQUESTED_DATE, cancellationRequestedDate)
            inputParameters(6) = New DBHelperParameter(COL_NAME_P_CANCELLATION_CODE, .cancellationCode)

            If oCommentData IsNot Nothing Then
                With oCommentData
                    inputParameters(7) = New DBHelperParameter(COL_NAME_P_COMMENT_ID, .CommentId.ToByteArray)
                    inputParameters(8) = New DBHelperParameter(COL_NAME_P_CALLER_NAME, .Callername)
                    inputParameters(9) = New DBHelperParameter(COL_NAME_P_COMMENT_TYPE_ID, .CommentTypeId.ToByteArray)
                    inputParameters(10) = New DBHelperParameter(COL_NAME_P_COMMENT, .Comment)
                End With
            Else
                inputParameters(7) = New DBHelperParameter(COL_NAME_P_COMMENT_ID, System.DBNull.Value)
                inputParameters(8) = New DBHelperParameter(COL_NAME_P_CALLER_NAME, System.DBNull.Value)
                inputParameters(9) = New DBHelperParameter(COL_NAME_P_COMMENT_TYPE_ID, System.DBNull.Value)
                inputParameters(10) = New DBHelperParameter(COL_NAME_P_COMMENT, System.DBNull.Value)
            End If
        End With

        outputParameter(0) = New DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer))
        outputParameter(1) = New DBHelperParameter(COL_NAME_P_EXCEPTION_MSG, GetType(String), 500)

        DBHelper.ExecuteSpParamBindByName(selectStmt, inputParameters, outputParameter)

        If outputParameter(0).Value = 0 Then
            Return True
        Else
            oCertCancellationData.errorCode = outputParameter(0).Value
            oCertCancellationData.ErrorMsg = outputParameter(1).Value
            Return False
        End If
    End Function


    ' Execute Store Procedure
    Public Function ExecuteCancelSP(oCertCancellationData As CertCancellationData, _
                                    Optional ByVal oBankInfoData As BankInfoData = Nothing, _
                                    Optional ByVal oCommentData As CommentData = Nothing) As CertCancellationData
        Dim selectStmt As String = Config("/SQL/PROCESS_CANCELLATION")
        Dim inputParameters(TOTAL_INPUT_PARAM_CANCEL) As DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_CANCEL) As DBHelperParameter

        With oCertCancellationData
            inputParameters(P_COMPANY_ID) = New DBHelperParameter(COL_NAME_P_COMPANY_ID, .companyId.ToByteArray)
            inputParameters(P_DEALER_ID) = New DBHelperParameter(COL_NAME_P_DEALER_ID, .dealerId.ToByteArray)
            inputParameters(P_CERTIFICATE) = New DBHelperParameter(COL_NAME_P_CERTIFICATE, .certificate)
            inputParameters(P_SOURCE) = New DBHelperParameter(COL_NAME_P_SOURCE, .source)
            inputParameters(P_CANCELLATION_DATE) = New DBHelperParameter(COL_NAME_P_CANCELLATION_DATE, .cancellationDate)
            inputParameters(P_CANCELLATION_CODE) = New DBHelperParameter(COL_NAME_P_CANCELLATION_CODE, .cancellationCode)
            inputParameters(P_CUSTOMER_PAID) = New DBHelperParameter(COL_NAME_P_CUSTOMER_PAID, .customerPaid)
            inputParameters(P_QUOTE) = New DBHelperParameter(COL_NAME_P_QUOTE, .quote)
            If .payment_method_Id.Equals(System.Guid.Empty) Then
                inputParameters(P_PAYMENT_METHOD_ID) = New DBHelperParameter(COL_NAME_P_PAYMENT_METHOD_ID, System.DBNull.Value)
            Else
                inputParameters(P_PAYMENT_METHOD_ID) = New DBHelperParameter(COL_NAME_P_PAYMENT_METHOD_ID, .payment_method_Id.ToByteArray)
            End If

            If oBankInfoData IsNot Nothing Then
                With oBankInfoData
                    inputParameters(P_BANK_INFO_ID) = New DBHelperParameter(COL_NAME_P_BANK_INFO_ID, .bankinfoId.ToByteArray)
                    inputParameters(P_BANK_ACCOUNT_NAME) = New DBHelperParameter(COL_NAME_BANK_ACCOUNT_NAME, .AccountName)
                    inputParameters(P_BANK_ID) = New DBHelperParameter(COL_NAME_P_BANK_ID, .BankID)
                    inputParameters(P_BANK_ACCOUNT_NUMBER) = New DBHelperParameter(COL_NAME_P_BANK_ACCOUNT_NUMBER, .AccountNumber)
                    If .SwiftCode Is Nothing Then
                        inputParameters(P_BANK_SWIFT_CODE) = New DBHelperParameter(COL_NAME_P_BANK_SWIFT_CODE, System.DBNull.Value)
                    Else
                        inputParameters(P_BANK_SWIFT_CODE) = New DBHelperParameter(COL_NAME_P_BANK_SWIFT_CODE, .SwiftCode)
                    End If
                    If .IBAN_Number Is Nothing Then
                        inputParameters(P_BANK_IBAN_NUMBER) = New DBHelperParameter(COL_NAME_P_BANK_IBAN_NUMBER, System.DBNull.Value)
                    Else
                        inputParameters(P_BANK_IBAN_NUMBER) = New DBHelperParameter(COL_NAME_P_BANK_IBAN_NUMBER, .IBAN_Number)
                    End If
                    If .AccountTypeId.Equals(System.Guid.Empty) Then
                        inputParameters(P_BANK_ACCOUNT_TYPE_ID) = New DBHelperParameter(COL_NAME_P_BANK_ACCOUNT_TYPE_ID, System.DBNull.Value)
                    Else
                        inputParameters(P_BANK_ACCOUNT_TYPE_ID) = New DBHelperParameter(COL_NAME_P_BANK_ACCOUNT_TYPE_ID, .AccountTypeId)
                    End If
                    inputParameters(P_BANK_COUNTRY_ID) = New DBHelperParameter(COL_NAME_P_BANK_COUNTRY_ID, .CountryId)
                    If .PaymentReasonId.Equals(System.Guid.Empty) Then
                        inputParameters(P_BANK_PAYMENT_REASON_ID) = New DBHelperParameter(COL_NAME_P_BANK_PAYMENT_REASON_ID, System.DBNull.Value)
                    Else
                        inputParameters(P_BANK_PAYMENT_REASON_ID) = New DBHelperParameter(COL_NAME_P_BANK_PAYMENT_REASON_ID, .PaymentReasonId.ToByteArray)
                    End If
                End With
            Else
                'With oBankInfoData
                inputParameters(P_BANK_INFO_ID) = New DBHelperParameter(COL_NAME_P_BANK_INFO_ID, System.DBNull.Value)
                inputParameters(P_BANK_ACCOUNT_NAME) = New DBHelperParameter(COL_NAME_BANK_ACCOUNT_NAME, System.DBNull.Value)
                inputParameters(P_BANK_ID) = New DBHelperParameter(COL_NAME_P_BANK_ID, System.DBNull.Value)
                inputParameters(P_BANK_ACCOUNT_NUMBER) = New DBHelperParameter(COL_NAME_P_BANK_ACCOUNT_NUMBER, System.DBNull.Value)
                inputParameters(P_BANK_SWIFT_CODE) = New DBHelperParameter(COL_NAME_P_BANK_SWIFT_CODE, System.DBNull.Value)
                inputParameters(P_BANK_IBAN_NUMBER) = New DBHelperParameter(COL_NAME_P_BANK_IBAN_NUMBER, System.DBNull.Value)
                inputParameters(P_BANK_ACCOUNT_TYPE_ID) = New DBHelperParameter(COL_NAME_P_BANK_ACCOUNT_TYPE_ID, System.DBNull.Value)
                inputParameters(P_BANK_COUNTRY_ID) = New DBHelperParameter(COL_NAME_P_BANK_COUNTRY_ID, System.DBNull.Value)
                inputParameters(P_BANK_PAYMENT_REASON_ID) = New DBHelperParameter(COL_NAME_P_BANK_PAYMENT_REASON_ID, System.DBNull.Value)
                'End With
            End If

            outputParameter(P_COMPUTED_REFUND) = New DBHelperParameter(COL_NAME_P_COMPUTED_REFUND, GetType(Decimal))
            outputParameter(P_CREDIT_ISSUED) = New DBHelperParameter(COL_NAME_P_CREDIT_ISSUED, GetType(String), 1)
            outputParameter(P_PRINCIPAL_PAID) = New DBHelperParameter(COL_NAME_P_PRINCIPAL_PAID, GetType(Decimal))
            outputParameter(P_REFUND_AMOUNT) = New DBHelperParameter(COL_NAME_P_REFUND_AMOUNT, GetType(Decimal))
            outputParameter(P_REFUND_AMOUNT_RESET) = New DBHelperParameter(COL_NAME_P_REFUND_AMOUNT_RESET, GetType(String), 1)
            outputParameter(P_RETURN) = New DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer))
            outputParameter(P_EXCEPTION_MSG) = New DBHelperParameter(COL_NAME_P_EXCEPTION_MSG, GetType(String), 500)
            outputParameter(P_INSTALLMENTS_PAID) = New DBHelperParameter(COL_NAME_P_INSTALLMENTS_PAID, GetType(Integer))
            outputParameter(P_AUTH_NUMBER) = New DBHelperParameter(COL_NAME_P_AUTH_NUMBER, GetType(String))

            If oCommentData IsNot Nothing Then
                With oCommentData
                    inputParameters(P_COMMENT_ID) = New DBHelperParameter(COL_NAME_P_COMMENT_ID, .CommentId.ToByteArray)
                    inputParameters(P_CALLER_NAME) = New DBHelperParameter(COL_NAME_P_CALLER_NAME, .Callername)
                    inputParameters(P_COMMENT_TYPE_ID) = New DBHelperParameter(COL_NAME_P_COMMENT_TYPE_ID, .CommentTypeId.ToByteArray)
                    inputParameters(P_COMMENTS) = New DBHelperParameter(COL_NAME_P_COMMENT, .Comment)
                End With
            Else
                inputParameters(P_COMMENT_ID) = New DBHelperParameter(COL_NAME_P_COMMENT_ID, System.DBNull.Value)
                inputParameters(P_CALLER_NAME) = New DBHelperParameter(COL_NAME_P_CALLER_NAME, System.DBNull.Value)
                inputParameters(P_COMMENT_TYPE_ID) = New DBHelperParameter(COL_NAME_P_COMMENT_TYPE_ID, System.DBNull.Value)
                inputParameters(P_COMMENTS) = New DBHelperParameter(COL_NAME_P_COMMENT, System.DBNull.Value)
            End If

            If oBankInfoData IsNot Nothing Then
                With oBankInfoData
                    inputParameters(P_BRANCH_DIGIT) = New DBHelperParameter(COL_NAME_P_BRANCH_DIGIT, .BranchDigit)
                    inputParameters(P_ACCOUNT_DIGIT) = New DBHelperParameter(COL_NAME_P_ACCOUNT_DIGIT, .AccountDigit)
                    inputParameters(P_BRANCH_NUMBER) = New DBHelperParameter(COL_NAME_P_BRANCH_NUMBER, .BranchNumber)
                End With
            Else
                inputParameters(P_BRANCH_DIGIT) = New DBHelperParameter(COL_NAME_P_BRANCH_DIGIT, System.DBNull.Value)
                inputParameters(P_ACCOUNT_DIGIT) = New DBHelperParameter(COL_NAME_P_ACCOUNT_DIGIT, System.DBNull.Value)
                inputParameters(P_BRANCH_NUMBER) = New DBHelperParameter(COL_NAME_P_BRANCH_NUMBER, System.DBNull.Value)
            End If

            inputParameters(P_COMPUTED_REFUND_RCVD) = New DBHelperParameter(COL_NAME_P_COMPUTED_REFUND_RCVD, .refundAmountRcvd)
            inputParameters(P_INSTALLMENTS_PAID_RCVD) = New DBHelperParameter(COL_NAME_P_INSTALLMENTS_PAID_RCVD, .InstallmentsPaid)

            If oBankInfoData IsNot Nothing Then
                With oBankInfoData
                    inputParameters(P_BANK_NAME) = New DBHelperParameter(COL_NAME_P_BANK_NAME, .BankName)
                End With
            Else
                inputParameters(P_BANK_NAME) = New DBHelperParameter(COL_NAME_P_BANK_NAME, System.DBNull.Value)
            End If
        End With

        ' Call DBHelper Store Procedure

        DBHelper.ExecuteSpParamBindByName(selectStmt, inputParameters, outputParameter)

        If outputParameter(P_RETURN).Value = 0 Then
            With oCertCancellationData
                .computedRefund = outputParameter(P_COMPUTED_REFUND).Value
                .creditIssued = outputParameter(P_CREDIT_ISSUED).Value
                .principalPaid = outputParameter(P_PRINCIPAL_PAID).Value
                .refundAmount = outputParameter(P_REFUND_AMOUNT).Value
                .refund_amt_reset = outputParameter(P_REFUND_AMOUNT_RESET).Value
                .InstallmentsPaid = outputParameter(P_INSTALLMENTS_PAID).Value
                .AuthNumber = outputParameter(P_AUTH_NUMBER).Value
                .errorCode = outputParameter(P_RETURN).Value
                .ErrorMsg = outputParameter(P_EXCEPTION_MSG).Value
            End With
        ElseIf outputParameter(P_RETURN).Value = 1 Then
            With oCertCancellationData
                .errorCode = outputParameter(P_RETURN).Value
                .ErrorMsg = outputParameter(P_EXCEPTION_MSG).Value
            End With
            Throw New StoredProcedureGeneratedException("", outputParameter(P_EXCEPTION_MSG).Value)
        Else
            With oCertCancellationData
                .errorCode = outputParameter(P_RETURN).Value
                .ErrorMsg = outputParameter(P_EXCEPTION_MSG).Value
            End With
            Throw New StoredProcedureGeneratedException("Data Base Generated Error: ", outputParameter(P_EXCEPTION_MSG).Value)
        End If

        Return oCertCancellationData

    End Function
    Public Function ExecuteUpdateBankInfoForRejectsSP(oUpdateBankInfoForRejectsData As UpdateBankInfoForRejectsData) As UpdateBankInfoForRejectsData
        Dim selectStmt As String = Config("/SQL/UPDATE_BANK_INFO_FOR_REJECTS")
        Dim inputParameters(TOTAL_INPUT_PARAM_UPDBANKINFO_CANCEL) As DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_UPDBANKINFO_CANCEL) As DBHelperParameter

        With oUpdateBankInfoForRejectsData
            inputParameters(0) = New DBHelperParameter(COL_NAME_P_CERT_CANCELLATION_ID, .certCancellationId.ToByteArray)
            inputParameters(1) = New DBHelperParameter(COL_NAME_P_BANK_INFO_ID, .bankInfoId.ToByteArray)
            inputParameters(2) = New DBHelperParameter(COL_NAME_P_BANK_IBAN_NUMBER, .IBANNumber)
            inputParameters(3) = New DBHelperParameter(COL_NAME_P_BANK_ACCOUNT_NUMBER, .accountNumber)

            outputParameter(0) = New DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer))
            outputParameter(1) = New DBHelperParameter(COL_NAME_P_EXCEPTION_MSG, GetType(String))

        End With

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        End If

        Return oUpdateBankInfoForRejectsData
    End Function
    Public Function ExecuteReverseCancelSP(oReverseCancellationData As ReverseCancellationData) As ReverseCancellationData
        Dim selectStmt As String = Config("/SQL/PROCESS_REVERSE_CANCELLATION")
        Dim inputParameters(TOTAL_INPUT_PARAM_REVERSE_CANCEL) As DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_REVERSE_CANCEL) As DBHelperParameter

        With oReverseCancellationData
            inputParameters(P_COMPANY_ID) = New DBHelperParameter(COL_NAME_P_COMPANY_ID, .companyId)
            inputParameters(P_DEALER_ID) = New DBHelperParameter(COL_NAME_P_DEALER_ID, .dealerId)
            inputParameters(P_CERTIFICATE) = New DBHelperParameter(COL_NAME_P_CERTIFICATE, .certificate)
            inputParameters(P_SOURCE) = New DBHelperParameter(COL_NAME_P_SOURCE, .source)
            inputParameters(P_SOURCE_TYPE) = New DBHelperParameter(COL_NAME_P_SOURCE_TYPE, .sourceType)

            outputParameter(PR_RETURN) = New DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer))
        End With

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

        If outputParameter(PR_RETURN).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        End If

        Return oReverseCancellationData

    End Function

    ' This Method will be used by the cancel cert web service off-line. any errors will be sent by email by store procedure
    Public Function CancelCertificatesByInvocieNumber(dealerId As Guid, companyId As Guid, certNumber As String, invoiceNumber As String, cancellation_code As String, _
                                                      Source As String, CancellationDate As DateType) As Boolean

        Dim selectStmt As String = Config("/SQL/CANCEL_CERTIFICATES_BY_INVOCIE_NUMBER")
        Dim inputParameters(TOTAL_INPUT_PARAM_CANCEL_BY_INVOICE_NUMBER) As DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_CANCEL_BY_INVOICE_NUMBER) As DBHelperParameter

        inputParameters(P_COMPANY_ID) = New DBHelperParameter(COL_NAME_P_COMPANY_ID, companyId)
        inputParameters(P_DEALER_ID) = New DBHelperParameter(COL_NAME_P_DEALER_ID, dealerId)
        inputParameters(P_CERTIFICATE) = New DBHelperParameter(COL_NAME_P_CERTIFICATE, certNumber)
        inputParameters(P_INVOICE_NUMBER) = New DBHelperParameter(COL_NAME_P_INVOICE_NUMBER, invoiceNumber)
        inputParameters(P_CANCELLATION_CODE_BY_INVOICE) = New DBHelperParameter(COL_NAME_P_CANCELLATION_CODE, cancellation_code)
        inputParameters(P_SOURCE_BY_INVOICE) = New DBHelperParameter(COL_NAME_P_CANCELLATION_DATE, CancellationDate.Value)
        inputParameters(P_CANCELLATION_DATE_BY_INVOICE) = New DBHelperParameter(COL_NAME_P_SOURCE, Source)

        outputParameter(PR_RETURN) = New DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer))

        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            Return True

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        Finally
            '   ds.Dispose()
        End Try

        Return True

    End Function


    Public Function CancelCoverages(dealerId As Guid, certNumber As String, CancellationDate As DateType) As Boolean

        Dim selectStmt As String = Config("/SQL/CANCEL_COVERAGES")
        Dim inputParameters(TOTAL_INPUT_PARAM_CANCEL_COVERAGES) As DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_CANCEL_COVERAGES) As DBHelperParameter


        inputParameters(0) = New DBHelperParameter(COL_NAME_P_DEALER_ID, dealerId)
        inputParameters(1) = New DBHelperParameter(COL_NAME_P_CERTIFICATE, certNumber)
        inputParameters(2) = New DBHelperParameter(COL_NAME_P_CANCELLATION_DATE, CancellationDate.Value)


        outputParameter(PR_RETURN) = New DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer))

        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            Return True

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        Finally
            '   ds.Dispose()
        End Try

        Return True

    End Function

    Public Function VSC_CancelPolicy(CertId As Guid, cancellationReasonCode As String,
                                     user_id As Guid, Source As String, _
                                     Optional ByVal CancellationDate As DateType = Nothing, _
                                     Optional ByVal MaxAssurantMileageLimit As Integer = Nothing, _
                                     Optional ByVal CurrentOdometer As Integer = Nothing) As Boolean

        Dim myCancellationDate As Date

        If CancellationDate Is Nothing Then
            myCancellationDate = Date.Today
        Else
            myCancellationDate = CancellationDate.Value
        End If

        Dim selectStmt As String = Config("/SQL/VSC_CANCEL_CERTIFICATE")
        Dim inputParameters(TOTAL_INPUT_PARAM_CANCEL_VSC_CERT) As DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_CANCEL_COVERAGES) As DBHelperParameter


        inputParameters(0) = New DBHelperParameter(COL_NAME_P_CERT_ID, CertId)
        inputParameters(1) = New DBHelperParameter(COL_NAME_P_CANCELLATION_CODE, cancellationReasonCode)
        inputParameters(2) = New DBHelperParameter(COL_NAME_P_CANCELLATION_DATE, myCancellationDate)
        inputParameters(3) = New DBHelperParameter(COL_NAME_P_MAX_ASSURANT_MILEAGE_LIMIT, MaxAssurantMileageLimit)
        inputParameters(4) = New DBHelperParameter(COL_NAME_P_CURRENT_ODOMETER, CurrentOdometer)
        inputParameters(5) = New DBHelperParameter(COL_NAME_P_SOURCE, Source)
        inputParameters(6) = New DBHelperParameter(COL_NAME_P_USER_ID, user_id)

        outputParameter(PR_RETURN) = New DBHelperParameter(COL_NAME_P_RETURN, GetType(Integer))

        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)
            Return True

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        Finally
            '   ds.Dispose()
        End Try

        Return True

    End Function
#End Region

End Class



