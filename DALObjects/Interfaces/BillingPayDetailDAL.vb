Public Class BillingPayDetailDAL
    Inherits DALBase

#Region "Constants"
    Public Const BILLPAY_TABLE_NAME As String = "ELP_BILLING_DETAIL"
    Public Const BILLPAYTBL_KEY_NAME As String = "billing_detail_id"
    Public Const COLLECTPAY_TABLE_NAME As String = "ELP_CERT_PAYMENT"

    'ELP_BILLING_DETAIL
    Public Const COL_NAME_BILLING_DETAIL_ID As String = "billing_detail_id"
    Public Const COL_NAME_INSTALLMENT_NUMBER As String = "installment_number"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_REJECTED_ID As String = "rejected_id"
    Public Const COL_NAME_PAID As String = "paid_id"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_PAYMENT_DUE_DATE As String = "payment_due_date"
    Public Const COL_NAME_COVERAGE_SEQ As String = "coverage_sequence"
    'Public Const COL_NAME_DATE_PROCESSED As String = "process_date"
    Public Const COL_NAME_BILLING_DATE As String = "billing_date"
    Public Const COL_NAME_FROM_DATE As String = "from_date"
    Public Const COL_NAME_TO_DATE As String = "to_date"
    Public Const COL_NAME_BILLED_AMOUNT As String = "billed_amount"
    Public Const COL_NAME_BILLING_STATUS As String = "billing_status"
    Public Const COL_NAME_REJECT_CODE As String = "rejection_code"
    Public Const COL_NAME_OPEN_AMOUNT As String = "open_amount"
    Public Const COL_NAME_INCOMING_AMOUNT As String = "incoming_amount"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"

    Public Const PARAM_NAME_STATUS As String = "p_BillingStatus"
    Public Const PARAM_NAME_CERT_ID As String = "p_cert_id"
    Public Const PARAM_NAME_INSTAL_NO As String = "p_installment_no"
    Public Const PARAM_NAME_REJECT_CODE_ID As String = "p_reject_code_id"
    Public Const PARAM_NAME_BILL_HIST_ID As String = "p_bill_detail_id"
    Public Const PARAM_NAME_REJECT_DATE As String = "p_reject_date"
    Public Const PARAM_NAME_SCR_PYMT As String = "p_scr_pymt"

    Public Const PARAM_CERT_ID As String = "pi_cert_id"
    Public Const PARAM_LANG_ID As String = "pi_language_id"

    Public Const COL_NEW_PAYMENT_DATE As String = "p_new_payment_date"
    Public Const COL_NAME_RETURN_REASON As String = "p_exception_msg"
    Public Const COL_NAME_RETURN_CODE As String = "p_return_code"

    Public Const TOTAL_PARAM_IN = 1 '2
    Public Const TOTAL_PARAM_OUT = 2 '2
    Public Const IN_STATUS_ID = 0
    Public Const IN_CERT_ID = 1
    Public Const IN_INSTAL_NO = 2
    Public Const IN_REJECT_CODE_ID = 3
    Public Const IN_BILLHIST_ID = 4
    Public Const IN_REJECT_DATE = 5
    Public Const IN_SCR_PYMT = 6

    Public Const OUT_NEW_PAYMENT_DATE = 0
    Public Const OUT_REJ_CODE = 1
    Public Const OUT_RET_MSG = 2

    Public Const YES As String = "Y"


    'ELP_CERT_PAYMENT
    Public Const COL_NAME_CERT_PAYMENT_ID As String = "cert_id"
    Public Const COL_NAME_COLLECTED_AMOUNT As String = "collected_amount"
    Public Const COL_NAME_PAYMENT_AMOUNT As String = "payment_amount"
    Public Const COL_NAME_COLLECTED_DATE As String = "collected_date"
    Public Const COL_NAME_DATE_SEND As String = "date_send"
    Public Const COL_NAME_DATE_PROCESSED_PC As String = "process_date"
    Public Const COL_NAME_INSTALLMENT_NUM As String = "installment_number"
    Public Const COL_NAME_LANGUAGE_ID_PC As String = "language_id"
    Public Const COL_NAME_COVERAGE_SEQUENCE_PC As String = "coverage_sequence"
    'Public Const COL_NAME_CREDIT_CARD_TYPE As String = "credit_card_type"
    'Public Const COL_NAME_CREDIT_CARD_NUMBER As String = "credit_card_number"
    Public Const COL_NAME_PAYMENT_METHOD As String = "payment_method"
    Public Const COL_NAME_PAYMENT_INSTRUMENT_NUMBER As String = "payment_instrument_number"
    Public Const COL_NAME_PAYMENT_ID As String = "id"
    Public Const COL_NAME_REJECT_CODE_PC As String = "processor_reject_code"
    Public Const COL_NAME_REJECT_REASON_PC As String = "pymt_Reject_reason"
    Public Const COL_NAME_REJECT_DATE_PC As String = "reject_date"
    Public Const COL_NAME_PAYMENT_STATUS As String = "payment_status"
    Public Const COL_NAME_PROCESSOR_REJECT_CODE As String = "process_reject_code"
    Public Const COL_NAME_PAYMENT_TYPE_XCD As String = "payment_type_xcd"



#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub
#End Region

#Region "Load Methods"

#Region "BillingPay Load"
    Public Sub LoadSchema(ds As DataSet)
        LoadBillPay(ds, Guid.Empty)
    End Sub

    Public Function LoadBillPay(familyDS As DataSet, id As Guid, Optional ByVal useCertId As Boolean = False)
        Dim selectStmt As String = Config("/SQL/LOAD_BILLPAY")
        Dim parameters() As DBHelper.DBHelperParameter

        If useCertId Then
            selectStmt = Config("/SQL/LOAD_BY_CERT_ID")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_cert_id", id.ToByteArray)}
        Else
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_billing_detail_id", id.ToByteArray)}
        End If
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}

        Try
            Dim ds = New DataSet
            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, BILLPAY_TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function loadBillpayList(BillingHeaderId As Guid) As DataSet
        Dim selectstmt As String = Config("/SQL/LOAD_BILLPAYLIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pi_billing_detail_id", BillingHeaderId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            Dim ds = New DataSet
            DBHelper.FetchSp(selectstmt, parameters, outParameters, ds, BILLPAY_TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function loadBillpayListForNonBillingDealer(BillingHeaderId As Guid, language_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_BILLPAYLIST_FOR_NON_BILLING_BY_DEALER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id1", language_id.ToByteArray),
                            New DBHelper.DBHelperParameter("language_id2", language_id.ToByteArray),
                            New DBHelper.DBHelperParameter("billing_header_id", BillingHeaderId.ToByteArray)}
        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, BILLPAY_TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception

        End Try
    End Function

    Public Function LoadBillHistList(langId As Guid, certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_BILLHIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                 {New DBHelper.DBHelperParameter(PARAM_CERT_ID, certId.ToByteArray),
                  New DBHelper.DBHelperParameter(PARAM_LANG_ID, langId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            Dim ds = New DataSet
            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, BILLPAY_TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetMaxActiveInstNoForCert(certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_MAX_ACTIVEINSTNO_FORCERT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray)}
        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, BILLPAY_TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetLatestRejInstNoForCert(certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_LATEST_REJ_INSTNO_FORCERT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray)}
        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, BILLPAY_TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAllRejInstNoForCert(certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_ALL_REJ_INSTNO_FORCERT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("V_RejInsNo", GetType(DataSet))}
        Dim ds As New DataSet
        Try

            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, "BILLING")
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "PayCollection Load"

    Public Function LoadCollectPayHistList(langId As Guid, certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_COLLPAY_HISTORY")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                 {New DBHelper.DBHelperParameter(PARAM_CERT_ID, certId.ToByteArray),
                  New DBHelper.DBHelperParameter(PARAM_LANG_ID, langId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            Dim ds = New DataSet
            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, COLLECTPAY_TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadCollectPayTotals(certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/COLLPAY_SUM_AND_COUNT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                 {New DBHelper.DBHelperParameter(PARAM_CERT_ID, certId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            Dim ds = New DataSet
            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, COLLECTPAY_TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#End Region

#Region "Overloaded Methods"

    Public Overloads Sub Updtae(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal ChangesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(BILLPAY_TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(BILLPAY_TABLE_NAME), Transaction, ChangesFilter)
        End If
    End Sub

    Public Function LoadTotals(certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/BILLPAY_SUM_AND_COUNT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                 {New DBHelper.DBHelperParameter(PARAM_CERT_ID, certId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            Dim ds = New DataSet
            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, BILLPAY_TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadTotalsNew(certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/BILLPAY_SUM_AND_COUNT_NEW")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                 {New DBHelper.DBHelperParameter(PARAM_CERT_ID, certId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            Dim ds = New DataSet
            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, BILLPAY_TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadLaterBillPayRow(CertId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_LATER_BILLING_ROW")
        Try
            selectStmt = OracleDbHelper.ReplaceParameter(selectStmt, COL_NAME_CERT_ID.ToUpper, CertId)
            Return OracleDbHelper.Fetch(selectStmt, BILLPAY_TABLE_NAME)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "StoreProcedures Control"

    ' Execute Store Procedure
    Public Function ExecuteSP(statusId As Guid, certId As Guid) As String
        Dim inputParameter(TOTAL_PARAM_IN) As DBHelper.DBHelperParameter
        Dim outputParameter(TOTAL_PARAM_OUT) As DBHelper.DBHelperParameter
        Dim selectStmt As String = Config("/SQL/BILLING_STATUS_CHANGE")

        inputParameter(IN_STATUS_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_STATUS, statusId)

        inputParameter(IN_CERT_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_CERT_ID, certId)


        outputParameter(OUT_NEW_PAYMENT_DATE) = New DBHelper.DBHelperParameter(COL_NEW_PAYMENT_DATE, GetType(String), 10)
        outputParameter(OUT_REJ_CODE) = New DBHelper.DBHelperParameter(COL_NAME_RETURN_CODE, GetType(Integer))
        outputParameter(OUT_RET_MSG) = New DBHelper.DBHelperParameter(COL_NAME_RETURN_REASON, GetType(String), 100)

        'Call DBHelper SP
        DBHelper.ExecuteSp(selectStmt, inputParameter, outputParameter)

        If outputParameter(OUT_REJ_CODE).Value <> 0 Then
            Throw New StoredProcedureGeneratedException("Data Base Generated Error:", outputParameter(OUT_RET_MSG).Value)
        End If
        Return outputParameter(OUT_NEW_PAYMENT_DATE).Value

    End Function

    Public Function CreateBillPayForRejOrAct(NewStatusId As Guid, certId As Guid, InstalNo As Integer, RejectCodeId As Guid, BillHistId As Guid) As Integer
        Dim inputParameters(6) As DBHelper.DBHelperParameter
        Dim outputParameter(1) As DBHelper.DBHelperParameter
        Dim selectStmt As String = Config("/SQL/CREATE_BILLING_HIST")


        inputParameters(IN_STATUS_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_STATUS, NewStatusId)
        inputParameters(IN_CERT_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_CERT_ID, certId)
        inputParameters(IN_INSTAL_NO) = New DBHelper.DBHelperParameter(PARAM_NAME_INSTAL_NO, InstalNo)
        inputParameters(IN_REJECT_CODE_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_REJECT_CODE_ID, RejectCodeId)
        inputParameters(IN_BILLHIST_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_BILL_HIST_ID, BillHistId)
        inputParameters(IN_REJECT_DATE) = New DBHelper.DBHelperParameter(PARAM_NAME_REJECT_DATE, DBNull.Value)
        inputParameters(IN_SCR_PYMT) = New DBHelper.DBHelperParameter(PARAM_NAME_SCR_PYMT, YES) 'DEF-1073

        outputParameter(0) = New DBHelper.DBHelperParameter(COL_NAME_RETURN_CODE, GetType(Integer))
        outputParameter(1) = New DBHelper.DBHelperParameter(COL_NAME_RETURN_REASON, GetType(String), 100)

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

        If outputParameter(0).Value <> 0 Then
            Throw New StoredProcedureGeneratedException("Data Base Generated Error: ", outputParameter(1).Value)
        End If
        Return outputParameter(0).Value
    End Function

#End Region
End Class
