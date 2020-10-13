'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (10/21/2019)********************


Public Class ApInvoiceMatchingDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added  Or DataRowState.Modified 
    Public Const TABLE_NAME As String = "ELP_AP_INVOICE_MATCHING"
    Public Const TABLE_KEY_NAME As String = COL_NAME_INVOICE_MATCHING_ID
	
    Public Const COL_NAME_INVOICE_MATCHING_ID As String = "invoice_matching_id"
    Public Const COL_NAME_PO_LINE_ID As String = "po_line_id"
    Public Const COL_NAME_INVOICE_LINE_ID As String = "invoice_line_id"
    Public Const COL_NAME_QTY As String = "qty"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_EXTENDED_QTY As String = "extended_qty"
    Public Const COL_NAME_EXTENDED_UOM As String = "extended_uom"
    Public Const COL_NAME_EXTENDED_UNIT_PRICE As String = "extended_unit_price"
    Public Const COL_NAME_AP_PAYMENT_BATCH_ID As String = "ap_payment_batch_id"
    Public Const COL_NAME_MATCH_TYPE_XCD As String = "match_type_xcd"
    Public Const COL_NAME_PAYMENT_DATE As String = "payment_date"
    
    Public Const PAR_I_NAME_INVOICE_MATCHING_ID As String = "pi_invoice_matching_id"
    Public Const PAR_I_NAME_PO_LINE_ID As String = "pi_po_line_id"
    Public Const PAR_I_NAME_INVOICE_LINE_ID As String = "pi_invoice_line_id"
    Public Const PAR_I_NAME_QTY As String = "pi_qty"
    Public Const PAR_I_NAME_AMOUNT As String = "pi_amount"
    Public Const PAR_I_NAME_EXTENDED_QTY As String = "pi_extended_qty"
    Public Const PAR_I_NAME_EXTENDED_UOM As String = "pi_extended_uom"
    Public Const PAR_I_NAME_EXTENDED_UNIT_PRICE As String = "pi_extended_unit_price"
    Public Const PAR_I_NAME_AP_PAYMENT_BATCH_ID As String = "pi_ap_payment_batch_id"
    Public Const PAR_I_NAME_MATCH_TYPE_XCD As String = "pi_match_type_xcd"
    Public Const PAR_I_NAME_PAYMENT_DATE As String = "pi_payment_date"

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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
    
    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException() 
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_INVOICE_MATCHING_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_MATCHING_ID)
            .AddParameter(PAR_I_NAME_PO_LINE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PO_LINE_ID)
            .AddParameter(PAR_I_NAME_INVOICE_LINE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_LINE_ID)
            .AddParameter(PAR_I_NAME_QTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_QTY)
            .AddParameter(PAR_I_NAME_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_AMOUNT)
            .AddParameter(PAR_I_NAME_EXTENDED_QTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_EXTENDED_QTY)
            .AddParameter(PAR_I_NAME_EXTENDED_UOM, OracleDbType.Varchar2, sourceColumn:=COL_NAME_EXTENDED_UOM)
            .AddParameter(PAR_I_NAME_EXTENDED_UNIT_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_EXTENDED_UNIT_PRICE)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_AP_PAYMENT_BATCH_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_PAYMENT_BATCH_ID)
            .AddParameter(PAR_I_NAME_MATCH_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MATCH_TYPE_XCD)
            .AddParameter(PAR_I_NAME_PAYMENT_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_PAYMENT_DATE)
        End With
        
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_INVOICE_MATCHING_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_MATCHING_ID)
            .AddParameter(PAR_I_NAME_PO_LINE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_PO_LINE_ID)
            .AddParameter(PAR_I_NAME_INVOICE_LINE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INVOICE_LINE_ID)
            .AddParameter(PAR_I_NAME_QTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_QTY)
            .AddParameter(PAR_I_NAME_AMOUNT, OracleDbType.Decimal, sourceColumn:=COL_NAME_AMOUNT)
            .AddParameter(PAR_I_NAME_EXTENDED_QTY, OracleDbType.Decimal, sourceColumn:=COL_NAME_EXTENDED_QTY)
            .AddParameter(PAR_I_NAME_EXTENDED_UOM, OracleDbType.Varchar2, sourceColumn:=COL_NAME_EXTENDED_UOM)
            .AddParameter(PAR_I_NAME_EXTENDED_UNIT_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_EXTENDED_UNIT_PRICE)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_AP_PAYMENT_BATCH_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_PAYMENT_BATCH_ID)
            .AddParameter(PAR_I_NAME_MATCH_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MATCH_TYPE_XCD)
            .AddParameter(PAR_I_NAME_PAYMENT_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_PAYMENT_DATE)
        End With
        
    End Sub
#End Region

End Class


