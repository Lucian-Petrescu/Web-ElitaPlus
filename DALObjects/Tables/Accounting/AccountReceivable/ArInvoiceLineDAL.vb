'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (10/29/2020)********************


Public Class ArInvoiceLineDal
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added  Or DataRowState.Modified 
    Public Const TABLE_NAME As String = "ELP_AR_INVOICE_LINE"
    Public Const TABLE_KEY_NAME As String = COL_NAME_INVOICE_LINE_ID
	
    Public Const COL_NAME_INVOICE_LINE_ID As String = "invoice_line_id"
    Public Const COL_NAME_INVOICE_HEADER_ID As String = "invoice_header_id"
    Public Const COL_NAME_LINE_TYPE As String = "line_type"
    Public Const COL_NAME_ITEM_CODE As String = "item_code"
    Public Const COL_NAME_CERT_ITEM_COVERAGE_ID As String = "cert_item_coverage_id"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_ERNING_PARTER_XCD As String = "erning_parter_xcd"
    Public Const COL_NAME_PARENT_LINE_NUMBER As String = "parent_line_number"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_INVOICE_PERIOD_START_DATE As String = "invoice_period_start_date"
    Public Const COL_NAME_INVOICE_PERIOD_END_DATE As String = "invoice_period_end_date"
    Public Const COL_NAME_REF_INVOICE_LINE_ID As String = "ref_invoice_line_id"
    Public Const COL_NAME_LINE_NUMBER As String = "line_number"
    Public Const COL_NAME_INCOMING_AMOUNT As String = "incoming_amount"
    
    Public Const PAR_I_NAME_INVOICE_LINE_ID As String = "pi_invoice_line_id"
    Public Const PAR_I_NAME_INVOICE_HEADER_ID As String = "pi_invoice_header_id"
    Public Const PAR_I_NAME_LINE_TYPE As String = "pi_line_type"
    Public Const PAR_I_NAME_ITEM_CODE As String = "pi_item_code"
    Public Const PAR_I_NAME_CERT_ITEM_COVERAGE_ID As String = "pi_cert_item_coverage_id"
    Public Const PAR_I_NAME_AMOUNT As String = "pi_amount"
    Public Const PAR_I_NAME_ERNING_PARTER_XCD As String = "pi_erning_parter_xcd"
    Public Const PAR_I_NAME_PARENT_LINE_NUMBER As String = "pi_parent_line_number"
    Public Const PAR_I_NAME_DESCRIPTION As String = "pi_description"
    Public Const PAR_I_NAME_INVOICE_PERIOD_START_DATE As String = "pi_invoice_period_start_date"
    Public Const PAR_I_NAME_INVOICE_PERIOD_END_DATE As String = "pi_invoice_period_end_date"
    Public Const PAR_I_NAME_REF_INVOICE_LINE_ID As String = "pi_ref_invoice_line_id"
    Public Const PAR_I_NAME_LINE_NUMBER As String = "pi_line_number"
    Public Const PAR_I_NAME_INCOMING_AMOUNT As String = "pi_incoming_amount"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDs As DataSet, ByVal id As Guid)
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD"))
                cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Fetch(cmd, TABLE_NAME, familyDs)
            End Using        
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return Fetch(cmd, TABLE_NAME)
            End Using        
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function    
    

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
		If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            Update(ds.Tables(TABLE_NAME), CType(transaction, OracleTransaction), changesFilter)
        End If
    End Sub
    
    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException() 
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_INVOICE_LINE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_LINE_ID)
            .AddParameter(PAR_I_NAME_INVOICE_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_HEADER_ID)
            .AddParameter(PAR_I_NAME_LINE_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LINE_TYPE)
            .AddParameter(PAR_I_NAME_ITEM_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM_CODE)
            .AddParameter(PAR_I_NAME_CERT_ITEM_COVERAGE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CERT_ITEM_COVERAGE_ID)
            .AddParameter(PAR_I_NAME_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_AMOUNT)
            .AddParameter(PAR_I_NAME_ERNING_PARTER_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ERNING_PARTER_XCD)
            .AddParameter(PAR_I_NAME_PARENT_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_PARENT_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_INVOICE_PERIOD_START_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_PERIOD_START_DATE)
            .AddParameter(PAR_I_NAME_INVOICE_PERIOD_END_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_PERIOD_END_DATE)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_REF_INVOICE_LINE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REF_INVOICE_LINE_ID)
            .AddParameter(PAR_I_NAME_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_INCOMING_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_INCOMING_AMOUNT)
        End With
        
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_INVOICE_LINE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_LINE_ID)
            .AddParameter(PAR_I_NAME_INVOICE_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_HEADER_ID)
            .AddParameter(PAR_I_NAME_LINE_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LINE_TYPE)
            .AddParameter(PAR_I_NAME_ITEM_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ITEM_CODE)
            .AddParameter(PAR_I_NAME_CERT_ITEM_COVERAGE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CERT_ITEM_COVERAGE_ID)
            .AddParameter(PAR_I_NAME_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_AMOUNT)
            .AddParameter(PAR_I_NAME_ERNING_PARTER_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ERNING_PARTER_XCD)
            .AddParameter(PAR_I_NAME_PARENT_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_PARENT_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_INVOICE_PERIOD_START_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_PERIOD_START_DATE)
            .AddParameter(PAR_I_NAME_INVOICE_PERIOD_END_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INVOICE_PERIOD_END_DATE)
            .AddParameter(PAR_I_NAME_REF_INVOICE_LINE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REF_INVOICE_LINE_ID)
            .AddParameter(PAR_I_NAME_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_INCOMING_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_INCOMING_AMOUNT)
        End With
        
    End Sub
#End Region

End Class


