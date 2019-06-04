'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (10/11/2017)********************

Public Class RewardsDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified
    Public Const TABLE_NAME As String = "ELP_REWARDS"
    Public Const TABLE_KEY_NAME As String = COL_NAME_REWARD_ID

    Public Const COL_NAME_REWARD_ID As String = "reward_id"
    Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
    Public Const COL_NAME_REFERENCE_TYPE_XCD As String = "reference_type_xcd"
    Public Const COL_NAME_REWARD_TYPE_XCD As String = "reward_type"
    Public Const COL_NAME_REWARD_STATUS_XCD As String = "reward_status"
    Public Const COL_NAME_REWARD_AMOUNT As String = "reward_amount"
    Public Const COL_NAME_REWARD_PYMT_MODE_XCD As String = "reward_pymt_mode_xcd"
    Public Const COL_NAME_BANK_INFO_ID As String = "bank_info_id"
    Public Const COL_NAME_FORM_SIGNED_XCD As String = "form_signed_xcd"
    Public Const COL_NAME_SUBSCRIPTION_FORM_SIGNED_XCD As String = "subscription_form_signed_xcd"
    Public Const COL_NAME_INVOICE_SIGNED_XCD As String = "invoice_signed_xcd"
    Public Const COL_NAME_RIB_SIGNED_XCD As String = "rib_signed_xcd"
    Public Const COL_NAME_DELIVERY_DATE As String = "delivery_date"
    Public Const COL_NAME_SEQUENCE_NUMBER As String = "sequence_number"
    Public Const COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const COL_NAME_IBAN_NUMBER As String = "iban_number"
    Public Const COL_NAME_SWIFT_CODE As String = "swift_code"
    Public Const COL_NAME_GIFT_CARD_REQUEST_ID As String = "gift_card_request_id"
    Public Const COL_NAME_DEALER As String = "dealer"
    Public Const COL_NAME_COMPANY As String = "company"

    Public Const PAR_I_NAME_REWARD_ID As String = "pi_reward_id"
    Public Const PAR_I_NAME_REFERENCE_ID As String = "pi_reference_id"
    Public Const PAR_I_NAME_REFERENCE_TYPE_XCD As String = "pi_reference_type_xcd"
    Public Const PAR_I_NAME_REWARD_TYPE_XCD As String = "pi_reward_type_xcd"
    Public Const PAR_I_NAME_REWARD_STATUS_XCD As String = "pi_reward_status_xcd"
    Public Const PAR_I_NAME_REWARD_AMOUNT As String = "pi_reward_amount"
    Public Const PAR_I_NAME_REWARD_PYMT_MODE_XCD As String = "pi_reward_pymt_mode_xcd"
    Public Const PAR_I_NAME_BANK_INFO_ID As String = "pi_bank_info_id"
    Public Const PAR_I_NAME_FORM_SIGNED_XCD As String = "pi_form_signed_xcd"
    Public Const PAR_I_NAME_SUBSCRIPTION_FORM_SIGNED_XCD As String = "pi_subscription_form_signed_xcd"
    Public Const PAR_I_NAME_INVOICE_SIGNED_XCD As String = "pi_invoice_signed_xcd"
    Public Const PAR_I_NAME_RIB_SIGNED_XCD As String = "pi_rib_signed_xcd"
    Public Const PAR_I_NAME_DELIVERY_DATE As String = "pi_delivery_date"
    Public Const PAR_I_NAME_SEQUENCE_NUMBER As String = "pi_sequence_number"
    Public Const PAR_I_NAME_GIFT_CARD_REQUEST_ID As String = "pi_gift_card_request_id"

    Public Const PO_CURSOR_REWARD As Integer = 0
    Public Const SP_PARAM_NAME_REWARD_LIST As String = "po_reward_list"

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
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD"))
                cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, familyDS)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadRewardList(ByVal CompanyId As Guid, ByVal DealerId As Guid, ByVal CertificateNumber As String, ByVal RewardStatus As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(Me.PO_CURSOR_REWARD) As DBHelper.DBHelperParameter
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_company_id", CompanyId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_dealer_id", DealerId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_cert_number", CertificateNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_reward_status", RewardStatus)
        inParameters.Add(param)

        outputParameter(Me.PO_CURSOR_REWARD) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME_REWARD_LIST, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetRewardList")
            ds.Tables(0).TableName = "GetRewardList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
        If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_REWARD_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REWARD_ID)
            .AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REFERENCE_ID)
            .AddParameter(PAR_I_NAME_REFERENCE_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REFERENCE_TYPE_XCD)
            .AddParameter(PAR_I_NAME_REWARD_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REWARD_TYPE_XCD)
            .AddParameter(PAR_I_NAME_REWARD_STATUS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REWARD_STATUS_XCD)
            .AddParameter(PAR_I_NAME_REWARD_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_REWARD_AMOUNT)
            .AddParameter(PAR_I_NAME_REWARD_PYMT_MODE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REWARD_PYMT_MODE_XCD)
            .AddParameter(PAR_I_NAME_BANK_INFO_ID, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BANK_INFO_ID)
            .AddParameter(PAR_I_NAME_FORM_SIGNED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_FORM_SIGNED_XCD)
            .AddParameter(PAR_I_NAME_SUBSCRIPTION_FORM_SIGNED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SUBSCRIPTION_FORM_SIGNED_XCD)
            .AddParameter(PAR_I_NAME_INVOICE_SIGNED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INVOICE_SIGNED_XCD)
            .AddParameter(PAR_I_NAME_RIB_SIGNED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RIB_SIGNED_XCD)
            .AddParameter(PAR_I_NAME_DELIVERY_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_DELIVERY_DATE)
            .AddParameter(PAR_I_NAME_SEQUENCE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SEQUENCE_NUMBER)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_REWARD_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REWARD_ID)
            .AddParameter(PAR_I_NAME_REWARD_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REWARD_TYPE_XCD)
            .AddParameter(PAR_I_NAME_REWARD_STATUS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REWARD_STATUS_XCD)
            .AddParameter(PAR_I_NAME_REWARD_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_REWARD_AMOUNT)
            .AddParameter(PAR_I_NAME_REWARD_PYMT_MODE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REWARD_PYMT_MODE_XCD)
            .AddParameter(PAR_I_NAME_FORM_SIGNED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_FORM_SIGNED_XCD)
            .AddParameter(PAR_I_NAME_SUBSCRIPTION_FORM_SIGNED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SUBSCRIPTION_FORM_SIGNED_XCD)
            .AddParameter(PAR_I_NAME_INVOICE_SIGNED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INVOICE_SIGNED_XCD)
            .AddParameter(PAR_I_NAME_RIB_SIGNED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_RIB_SIGNED_XCD)
            .AddParameter(PAR_I_NAME_DELIVERY_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_DELIVERY_DATE)
            .AddParameter(PAR_I_NAME_SEQUENCE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_SEQUENCE_NUMBER)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
            .AddParameter(PAR_I_NAME_GIFT_CARD_REQUEST_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_GIFT_CARD_REQUEST_ID)
        End With

    End Sub
#End Region

End Class
