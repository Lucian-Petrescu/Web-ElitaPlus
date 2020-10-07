Public Class CheckPaymentDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AR_PAYMENTS"
    Public Const TABLE_KEY_NAME As String = "payment_id"

    Public Const COL_NAME_PAYMENT_ID As String = "payment_id"
    Public Const COL_NAME_PAYMENT_DATE As String = "payment_date"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_CURRENCY_CODE As String = "currency_code"
    Public Const COL_NAME_EXCHANGE_RATE As String = "exchange_rate"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_REFERENCE As String = "reference"
    Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
    Public Const COL_NAME_APPLICATION_MODE As String = "application_mode"
    Public Const COL_NAME_PAYMENT_METHOD As String = "payment_method"
    Public Const COL_NAME_DISTRIBUTED As String = "distributed"
    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_PAYMENT_TYPE_XCD As String = "payment_type_xcd"
    Public Const COL_NAME_PAYMENT_REJECT_CODE_XCD As String = "payment_reject_code_xcd"
    Public Const COL_NAME_PAYMENT_REJECT_DATE As String = "payment_reject_date"
    Public Const COL_NAME_CHECK_NUMBER As String = "check_number"
    Public Const COL_NAME_PAYMENT_SEPA_UNIQUE_ID As String = "payment_sepa_unique_id"
    Public Const COL_NAME_BANK_INFO_ID As String = "bank_info_id"
    Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const COL_NAME_BANK_NAME As String = "bank_name"
    Public Const COL_NAME_COMMENTS As String = "comments"

    Public Const COL_NAME_REJECT_REASON_XCD As String = "payment_reject_code_xcd"
    Public Const COL_NAME_REJECT_DATE As String = "payment_reject_date"
    Public Const COL_NAME_SLT_REJECT_CHK As String = "check_number"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("payment_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(certId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_FOR_CERT")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_id", certId.ToByteArray)}
        Try
            Dim ds = New DataSet
            Return (DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Add / Update Methods"
    Public Sub AddPayment(payment_date As Date,
                          amount As Decimal,
                          currency_code As String,
                          exchange_rate As String,
                          source As String,
                          reference As String,
                          reference_id As Guid,
                          application_mode As String,
                          payment_method As String,
                          distributed As String,
                          check_number As String,
                          check_customer_name As String,
                          check_bank_name As String,
                          comments As String,
                          user_id As String,
                          ByRef payment_id As Guid,
                          ByRef err_no As Integer,
                          ByRef err_msg As String)

        Dim selectStmt As String = Config("/SQL/ADD_CHECK_PAYMENT")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter(2) As DBHelper.DBHelperParameter

        inputParameters = New DBHelper.DBHelperParameter() _
        {SetParameter("pi_payment_date", payment_date),
         SetParameter("pi_amount", amount),
         SetParameter("pi_currency_code", currency_code),
         SetParameter("pi_exchange_rate", exchange_rate),
         SetParameter("pi_source", source),
         SetParameter("pi_reference", reference),
         SetParameter("pi_reference_id", reference_id),
         SetParameter("pi_application_mode", application_mode),
         SetParameter("pi_payment_method", payment_method),
         SetParameter("pi_distributed", distributed),
         SetParameter("pi_check_number", check_number),
         SetParameter("pi_check_cust_name", check_customer_name),
         SetParameter("pi_check_bank_name", check_bank_name),
         SetParameter("pi_comments", comments),
         SetParameter("pi_user", user_id)}

        outputParameter(0) = New DBHelper.DBHelperParameter("po_payment_id", GetType(Guid))
        outputParameter(1) = New DBHelper.DBHelperParameter("po_err_code", GetType(Integer))
        outputParameter(2) = New DBHelper.DBHelperParameter("po_err_msg", GetType(String), 5000)

        Try
            DBHelper.ExecuteSpParamBindByName(selectStmt, inputParameters, outputParameter)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        payment_id = CType(outputParameter(0).Value, Guid)
        err_no = CType(outputParameter(1).Value, Integer)
        err_msg = CType(outputParameter(2).Value, String)

    End Sub

    Public Sub AddRejectPayment(reject_date As Date,
                          payment_id As Guid,
                          reject_reason As String,
                          comments As String,
                          ByRef err_no As Integer,
                          ByRef err_msg As String)

        Dim selectStmt As String = Config("/SQL/ADD_REJECT_PAYMENT")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter(1) As DBHelper.DBHelperParameter

        inputParameters = New DBHelper.DBHelperParameter() _
        {SetParameter("pi_rejection_date", reject_date),
         SetParameter("pi_payment_id", payment_id),
         SetParameter("pi_reject_reason", reject_reason),
         SetParameter("pi_comments", comments),
         SetParameter("pi_source", "MANUAL_REJECT_PAYMENT")}

        outputParameter(0) = New DBHelper.DBHelperParameter("po_err_code", GetType(Integer))
        outputParameter(1) = New DBHelper.DBHelperParameter("po_err_msg", GetType(String), 5000)

        Try
            DBHelper.ExecuteSpParamBindByName(selectStmt, inputParameters, outputParameter)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        err_no = CType(outputParameter(0).Value, Integer)
        err_msg = CType(outputParameter(1).Value, String)

    End Sub

    Function SetParameter(name As String, value As Object) As DBHelper.DBHelperParameter
        name = name.Trim
        If value Is Nothing Then value = DBNull.Value
        If value.GetType Is GetType(String) Then value = DirectCast(value, String).Trim

        Return New DBHelper.DBHelperParameter(name, value, value.GetType)
    End Function

#End Region

End Class
