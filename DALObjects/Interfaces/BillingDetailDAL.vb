'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/20/2008)********************


Public Class BillingDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_BILLING_DETAIL"
    Public Const TABLE_KEY_NAME As String = "billing_detail_id"

    Public Const COL_NAME_BILLING_DETAIL_ID As String = "billing_detail_id"
    Public Const COL_NAME_BILLING_HEADER_ID As String = "billing_header_id"
    Public Const COL_NAME_INSTALLMENT_NUMBER As String = "installment_number"
    Public Const COL_NAME_BANK_OWNER_NAME As String = "bank_owner_name"
    Public Const COL_NAME_BANK_ACCT_NUMBER As String = "bank_acct_number"
    Public Const COL_NAME_BANK_RTN_NUMBER As String = "bank_rtn_number"
    Public Const COL_NAME_BILLED_AMOUNT As String = "billed_amount"
    Public Const COL_NAME_REASON As String = "reason"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_PAYMENT_RUN_DATE As String = "payment_run_date"
    Public Const COL_NAME_REJECTED_ID As String = "rejected_id"
    Public Const COL_NAME_MAX_PAYMENT_RUN_DATE As String = "max_payment_run_date"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_PAID As String = "paid"
    Public Const COL_NAME_BILLING_STATUS As String = "billing_status"
    Public Const COL_NAME_BILLING_STATUS_ID As String = "billing_status_id"
    Public Const COL_NAME_NIGHTLY_PAYMENT_RUN_DATE As String = "nightly_payment_run_date"
    Public Const COL_NAME_RE_ATTEMPT_COUNT As String = "re_attempt_count"
    Public Const COL_NAME_REJECT_DATE As String = "reject_date"
    Public Const COL_NAME_CREDIT_CARD_INFO_ID As String = "credit_card_info_id"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_MERCHANT_CODE As String = "merchant_code"
    Public Const COL_NAME_INSTALLMENT_DUE_DATE As String = "installment_due_date"
    Public Const COL_NAME_PROCESSOR_REJECT_CODE As String = "processor_reject_code"

    Public Const PARAM_NAME_STATUS As String = "p_BillingStatus"
    Public Const PARAM_NAME_CERT_ID As String = "p_cert_id"
    Public Const PARAM_NAME_INSTAL_NO As String = "p_installment_no"
    Public Const PARAM_NAME_REJECT_CODE_ID As String = "p_reject_code_id"
    Public Const PARAM_NAME_BILL_HIST_ID As String = "p_bill_detail_id"
    Public Const PARAM_NAME_REJECT_DATE As String = "p_reject_date"
    Public Const PARAM_NAME_SCR_PYMT As String = "p_scr_pymt"
    Public Const PARAM_CERT_ID As String = "pi_cert_id"


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

    Public Sub Load(familyDS As DataSet, id As Guid, Optional ByVal useCertId As Boolean = False)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter

        If useCertId Then
            selectStmt = Config("/SQL/LOAD_BY_CERT_ID")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_id", id.ToByteArray)}
        Else
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("billing_detail_id", id.ToByteArray)}
        End If

        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(BillingHeaderId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("billing_header_id", BillingHeaderId.ToByteArray)}
        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListForNonBillingByDealer(BillingHeaderId As Guid, language_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_FOR_NON_BILLING_BY_DEALER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id1", language_id.ToByteArray), _
                            New DBHelper.DBHelperParameter("language_id2", language_id.ToByteArray), _
                            New DBHelper.DBHelperParameter("billing_header_id", BillingHeaderId.ToByteArray)}
        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadBillHistList(langId As Guid, certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_HISTORY")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                 {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, langId.ToByteArray), _
                  New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, langId.ToByteArray), _
                  New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, langId.ToByteArray), _
                  New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray), _
                  New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray)}
        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Function LoadBillingTotals(certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/BILLING_SUM_AND_COUNT")
        Dim parameters() As DBHelper.DBHelperParameter

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray)}

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadBillingTotalsNew(certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/BILLING_SUM_AND_COUNT_NEW")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                 {New DBHelper.DBHelperParameter(PARAM_CERT_ID, certId.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("po_resultcursor", GetType(DataSet))}
        Try
            Dim ds = New DataSet
            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadLaterBillingRow(certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_LATER_BILLING_ROW")
        Dim parameters() As DBHelper.DBHelperParameter

        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray)}

        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "StoreProcedures Control"

    ' Execute Store Procedure
    Public Function ExecuteSP(statusId As Guid, certId As Guid) As String
        Dim inputParameters(TOTAL_PARAM_IN) As DBHelper.DBHelperParameter
        Dim outputParameter(TOTAL_PARAM_OUT) As DBHelper.DBHelperParameter
        Dim selectStmt As String = Config("/SQL/BILLING_STATUS_CHANGE")


        inputParameters(IN_STATUS_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_STATUS, statusId)

        inputParameters(IN_CERT_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_CERT_ID, certId)


        outputParameter(OUT_NEW_PAYMENT_DATE) = New DBHelper.DBHelperParameter(COL_NEW_PAYMENT_DATE, GetType(String), 10)
        outputParameter(OUT_REJ_CODE) = New DBHelper.DBHelperParameter(COL_NAME_RETURN_CODE, GetType(Integer))
        outputParameter(OUT_RET_MSG) = New DBHelper.DBHelperParameter(COL_NAME_RETURN_REASON, GetType(String), 100)

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

        If outputParameter(OUT_REJ_CODE).Value <> 0 Then
            Throw New StoredProcedureGeneratedException("Data Base Generated Error: ", outputParameter(OUT_RET_MSG).Value)
        End If
        'Dim xxx As DateType = New DateType(CType(outputParameter(OUT_NEW_PAYMENT_DATE).Value, Date))
        Return outputParameter(OUT_NEW_PAYMENT_DATE).Value

    End Function

    Public Function CreateBillingHistForRejOrAct(NewStatusId As Guid, certId As Guid, InstalNo As Integer, RejectCodeId As Guid, BillHistId As Guid) As Integer
        Dim inputParameters(6) As DBHelper.DBHelperParameter
        Dim outputParameter(1) As DBHelper.DBHelperParameter
        Dim selectStmt As String = Config("/SQL/CREATE_BILLING_HIST")


        inputParameters(IN_STATUS_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_STATUS, NewStatusId)
        inputParameters(IN_CERT_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_CERT_ID, certId)
        inputParameters(IN_INSTAL_NO) = New DBHelper.DBHelperParameter(PARAM_NAME_INSTAL_NO, InstalNo)
        inputParameters(IN_REJECT_CODE_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_REJECT_CODE_ID, RejectCodeId)
        inputParameters(IN_BILLHIST_ID) = New DBHelper.DBHelperParameter(PARAM_NAME_BILL_HIST_ID, BillHistId)
        inputParameters(IN_REJECT_DATE) = New DBHelper.DBHelperParameter(PARAM_NAME_REJECT_DATE, DBNull.Value) 'REQ-578: this param is only being used by Oracle
        inputParameters(IN_SCR_PYMT) = New DBHelper.DBHelperParameter(PARAM_NAME_SCR_PYMT, YES) 'DEF-1073- set to 'Y' if record is rejected thru' screen

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


