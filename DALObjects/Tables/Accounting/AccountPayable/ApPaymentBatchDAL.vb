'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (10/21/2019)********************


Public Class ApPaymentBatchDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added  Or DataRowState.Modified 
    Public Const TABLE_NAME As String = "ELP_AP_PAYMENT_BATCH"
    Public Const TABLE_KEY_NAME As String = COL_NAME_AP_PAYMENT_BATCH_ID
	
    Public Const COL_NAME_AP_PAYMENT_BATCH_ID As String = "ap_payment_batch_id"
    Public Const COL_NAME_BATCH_NUMBER As String = "batch_number"
    Public Const COL_NAME_VENDOR_ID As String = "vendor_id"
    Public Const COL_NAME_VENDOR_ADDRESS_ID As String = "vendor_address_id"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_ACCOUNTING_PERIOD As String = "accounting_period"
    Public Const COL_NAME_PAYMENT_STATUS_XCD As String = "payment_status_xcd"
    Public Const COL_NAME_DISTRIBUTED As String = "distributed"
    Public Const COL_NAME_POSTED As String = "posted"
    Public Const COL_NAME_PAYMENTSOURCE As String = "paymentsource"
    
    Public Const PAR_I_NAME_AP_PAYMENT_BATCH_ID As String = "pi_ap_payment_batch_id"
    Public Const PAR_I_NAME_BATCH_NUMBER As String = "pi_batch_number"
    Public Const PAR_I_NAME_VENDOR_ID As String = "pi_vendor_id"
    Public Const PAR_I_NAME_VENDOR_ADDRESS_ID As String = "pi_vendor_address_id"
    Public Const PAR_I_NAME_AMOUNT As String = "pi_amount"
    Public Const PAR_I_NAME_ACCOUNTING_PERIOD As String = "pi_accounting_period"
    Public Const PAR_I_NAME_PAYMENT_STATUS_XCD As String = "pi_payment_status_xcd"
    Public Const PAR_I_NAME_DISTRIBUTED As String = "pi_distributed"
    Public Const PAR_I_NAME_POSTED As String = "pi_posted"
    Public Const PAR_I_NAME_PAYMENTSOURCE As String = "pi_paymentsource"

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
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Config("/SQL/LOAD"))
                cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, TABLE_NAME, familyDS)
            End Using        
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using        
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function    
    

#End Region

#Region "Overloaded Methods"
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
        With command
            .AddParameter(PAR_I_NAME_AP_PAYMENT_BATCH_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_PAYMENT_BATCH_ID)
            .AddParameter(PAR_I_NAME_BATCH_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BATCH_NUMBER)
            .AddParameter(PAR_I_NAME_VENDOR_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_VENDOR_ID)
            .AddParameter(PAR_I_NAME_VENDOR_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_VENDOR_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_AMOUNT)
            .AddParameter(PAR_I_NAME_ACCOUNTING_PERIOD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACCOUNTING_PERIOD)
            .AddParameter(PAR_I_NAME_PAYMENT_STATUS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYMENT_STATUS_XCD)
            .AddParameter(PAR_I_NAME_DISTRIBUTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DISTRIBUTED)
            .AddParameter(PAR_I_NAME_POSTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_POSTED)
            .AddParameter(PAR_I_NAME_PAYMENTSOURCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYMENTSOURCE)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
        End With
        
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_AP_PAYMENT_BATCH_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_PAYMENT_BATCH_ID)
            .AddParameter(PAR_I_NAME_BATCH_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_BATCH_NUMBER)
            .AddParameter(PAR_I_NAME_VENDOR_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_VENDOR_ID)
            .AddParameter(PAR_I_NAME_VENDOR_ADDRESS_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_VENDOR_ADDRESS_ID)
            .AddParameter(PAR_I_NAME_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_AMOUNT)
            .AddParameter(PAR_I_NAME_ACCOUNTING_PERIOD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACCOUNTING_PERIOD)
            .AddParameter(PAR_I_NAME_PAYMENT_STATUS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYMENT_STATUS_XCD)
            .AddParameter(PAR_I_NAME_DISTRIBUTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DISTRIBUTED)
            .AddParameter(PAR_I_NAME_POSTED, OracleDbType.Varchar2, sourceColumn:=COL_NAME_POSTED)
            .AddParameter(PAR_I_NAME_PAYMENTSOURCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PAYMENTSOURCE)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
        End With
        
    End Sub
#End Region

#Region "public methods"
    Public Sub ValidatePaymentBatch(vendorId As Guid, batchNumber As String, ByRef errCode As Integer, ByRef errMsg As String)
        Dim strStmt As String = Config("/SQL/VALIDATE_PAYMENT_BATCH_NUMBER")
        
        Try
            Using conn As IDbConnection = New OracleConnection(DBHelper.ConnectString)
                conn.Open
                Using cmd As OracleCommand = conn.CreateCommand()
                    cmd.CommandText = strStmt
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.BindByName = True
                    
                    'input parameters
                    cmd.Parameters.Add("pi_vendor_id", OracleDbType.Raw).Value = vendorId.ToByteArray
                    cmd.Parameters.Add("pi_batch_num", OracleDbType.Varchar2, 100).Value = batchNumber

                    'output parameters
                    dim param_err_code As OracleParameter = new OracleParameter()
                    param_err_code = cmd.Parameters.Add("po_error_code", OracleDbType.Int32, ParameterDirection.Output)
                    param_err_code.Size = 25
                    
                    dim param_err_msg As OracleParameter = new OracleParameter()
                    param_err_msg = cmd.Parameters.Add("po_error_msg", OracleDbType.Varchar2, ParameterDirection.Output)
                    param_err_msg.Size = 500

                    cmd.ExecuteNonQuery

                    errCode = CType(param_err_code.Value.ToString, Integer)
                    errMsg = param_err_msg.Value.ToString
                End Using
                conn.Close
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

End Class


