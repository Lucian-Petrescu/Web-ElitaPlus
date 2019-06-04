'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/13/2004)********************


Public Class CancellationReasonDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CANCELLATION_REASON"
    Public Const TABLE_KEY_NAME As String = "cancellation_id"

    Public Const COL_NAME_CANCELLATION_ID = "cancellation_id"
    Public Const COL_NAME_DESCRIPTION = "description"
    Public Const COL_NAME_CODE = "code"
    Public Const COL_NAME_COMPANY_ID = "company_id"
    Public Const COL_NAME_REFUND_COMPUTE_METHOD_ID = "refund_compute_method_id"
    Public Const COL_NAME_REFUND_DESTINATION_ID = "refund_destination_id"
    Public Const COL_NAME_INPUT_AMT_REQ_ID = "input_amt_req_id"
    Public Const COL_DISPLAY_CODE = "display_code_id"
    Public Const COL_DEF_REFUND_PAYMENT_METHOD_ID = "def_refund_payment_method_id"
    Public Const COL_NAME_IS_LAWFUL = "is_lawful"
    Public Const COL_NAME_BENEFIT_CANCEL_REASON_CODE As String = "benefit_cancel_reason_code"
    Private Const DSNAME As String = "LIST"
    Public Const WILDCARD As Char = "%"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "CRUD Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cancellation_id", id.ToByteArray)}
        'Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("cancellation_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Try
            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadList(ByVal description As String, ByVal code As String, ByVal compIds As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim parameters() As OracleParameter

        description = GetFormattedSearchStringForSQL(description)
        code = GetFormattedSearchStringForSQL(code)
        'description &= WILDCARD
        'code &= WILDCARD
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_CODE, code), _
                                     New OracleParameter(COL_NAME_DESCRIPTION, description)}

        inCausecondition &= MiscUtil.BuildListForSql("AND c." & Me.COL_NAME_COMPANY_ID, compIds, True)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListbyRoleExlusion(ByVal User_Id As Guid, ByVal company_id As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_CANC_REASONS_LIST_BY_ROLE_EXCLUSION")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                    {New OracleParameter(UserDAL.COL_NAME_USER_ID, User_Id.ToByteArray), _
                                     New OracleParameter(COL_NAME_COMPANY_ID, company_id.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    'Public Sub Update(ByVal ds As DataSet)
    '    Dim conn As OracleConnection
    '    Dim transaction As OracleTransaction
    '    Try
    '        conn = New OracleConnection(DBHelper.ConnectString)
    '        conn.Open()
    '        transaction = conn.BeginTransaction
    '        Update(ds, transaction)
    '        transaction.Commit()
    '    Catch ex As Exception
    '        transaction.Rollback()
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
    '    Finally
    '        If conn.State = ConnectionState.Open Then conn.Close()
    '    End Try
    'End Sub

    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)
        DBHelper.Execute(ds.Tables(Me.TABLE_NAME), Config("/SQL/INSERT"), Config("/SQL/UPDATE"), Config("/SQL/DELETE"), Nothing, transaction)
        LookupListCache.ClearFromCache(Me.GetType.ToString)
    End Sub

    'Public Sub Update(ByVal ds As DataSet, ByVal transaction As OracleTransaction)
    '    Dim da As OracleDataAdapter = configureDataAdapter(ds, transaction)
    '    da.Update(ds.Tables(TABLE_NAME))
    'End Sub

    'Protected Function configureDataAdapter(ByVal ds As DataSet, ByVal transaction As OracleTransaction) As OracleDataAdapter
    '    Dim da As New OracleDataAdapter
    '    'associate commands to data adapter

    '    da.UpdateCommand = New OracleCommand(Config("/SQL/UPDATE"), transaction.Connection)
    '    AddCommonParameters(da.UpdateCommand)
    '    AddUpdateAuditParameters(da.UpdateCommand)
    '    AddWhereParameters(da.UpdateCommand)

    '    da.InsertCommand = New OracleCommand(Config("/SQL/INSERT"), transaction.Connection)
    '    AddCommonParameters(da.InsertCommand)
    '    AddInsertAuditParameters(da.InsertCommand)
    '    da.InsertCommand.Parameters.Add("cancellation_id", OracleDbType.Raw, 16, "cancellation_id")

    '    da.DeleteCommand = New OracleCommand(Config("/SQL/DELETE"), transaction.Connection)
    '    AddWhereParameters(da.DeleteCommand)
    '    Return da
    'End Function

    'Protected Sub AddCommonParameters(ByVal cmd As OracleCommand)
    '    cmd.Parameters.Add("description", OracleDbType.Varchar2, 50, "description")
    '    cmd.Parameters.Add("code", OracleDbType.Varchar2, 5, "code")
    '    cmd.Parameters.Add("company_id", OracleDbType.Raw, 16, "company_id")
    '    cmd.Parameters.Add("refund_compute_method_id", OracleDbType.Raw, 16, "refund_compute_method_id")
    '    cmd.Parameters.Add("refund_destination_id", OracleDbType.Raw, 16, "refund_destination_id")
    '    cmd.Parameters.Add("input_amt_req_id", OracleDbType.Raw, 16, "input_amt_req_id")
    'End Sub

    'Protected Sub AddWhereParameters(ByVal cmd As OracleCommand)
    '    cmd.Parameters.Add("cancellation_id", OracleDbType.Raw, 16, "cancellation_id")
    'End Sub

    'Protected Sub AddUpdateAuditParameters(ByVal cmd As OracleCommand)
    '    cmd.Parameters.Add("modified_by", OracleDbType.Varchar2, 30, "modified_by")
    'End Sub

    'Protected Sub AddInsertAuditParameters(ByVal cmd As OracleCommand)
    '    cmd.Parameters.Add("created_by", OracleDbType.Varchar2, 30, "created_by")
    'End Sub

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim CancResnbyRoleDal As New ExcludeCancReasonByRoleDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions           
            CancResnbyRoleDal.Update(familyDataset, tr, DataRowState.Deleted)

            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes            
            Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            CancResnbyRoleDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            'At the end delete the Address
            If Not familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME) Is Nothing AndAlso familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim oTransactionLogHeaderDAL As New TransactionLogHeaderDAL
                oTransactionLogHeaderDAL.Update(familyDataset.Tables(TransactionLogHeaderDAL.TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub


#End Region

End Class



