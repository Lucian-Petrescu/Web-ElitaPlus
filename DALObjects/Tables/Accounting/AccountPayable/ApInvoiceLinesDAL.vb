'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (10/21/2019)********************


Public Class ApInvoiceLinesDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added  Or DataRowState.Modified 
    Public Const TABLE_NAME As String = "ELP_AP_INVOICE_LINES"
    Public Const TABLE_KEY_NAME As String = COL_NAME_AP_INVOICE_LINES_ID
	
    Public Const COL_NAME_AP_INVOICE_LINES_ID As String = "ap_invoice_lines_id"
    Public Const COL_NAME_AP_INVOICE_HEADER_ID As String = "ap_invoice_header_id"
    Public Const COL_NAME_LINE_NUMBER As String = "line_number"
    Public Const COL_NAME_LINE_TYPE As String = "line_type"
    Public Const COL_NAME_VENDOR_ITEM_CODE As String = "vendor_item_code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_QUANTITY As String = "quantity"
    Public Const COL_NAME_UOM_XCD As String = "uom_xcd"
    Public Const COL_NAME_MATCHED_QUANTITY As String = "matched_quantity"
    Public Const COL_NAME_PAID_QUANTITY As String = "paid_quantity"
    Public Const COL_NAME_UNIT_PRICE As String = "unit_price"
    Public Const COL_NAME_TOTAL_PRICE As String = "total_price"
    Public Const COL_NAME_PARENT_LINE_NUMBER As String = "parent_line_number"
    Public Const COL_NAME_PO_NUMBER As String = "po_number"
    Public Const COL_NAME_PO_DATE As String = "po_date"
    Public Const COL_NAME_BILLING_PERIOD_START_DATE As String = "billing_period_start_date"
    Public Const COL_NAME_BILLING_PERIOD_END_DATE As String = "billing_period_end_date"
    Public Const COL_NAME_REFERENCE_NUMBER As String = "reference_number"
    Public Const COL_NAME_VENDOR_TRANSACTION_TYPE As String = "vendor_transaction_type"
    
    Public Const PAR_I_NAME_AP_INVOICE_LINES_ID As String = "pi_ap_invoice_lines_id"
    Public Const PAR_I_NAME_AP_INVOICE_HEADER_ID As String = "pi_ap_invoice_header_id"
    Public Const PAR_I_NAME_LINE_NUMBER As String = "pi_line_number"
    Public Const PAR_I_NAME_LINE_TYPE As String = "pi_line_type"
    Public Const PAR_I_NAME_VENDOR_ITEM_CODE As String = "pi_vendor_item_code"
    Public Const PAR_I_NAME_DESCRIPTION As String = "pi_description"
    Public Const PAR_I_NAME_QUANTITY As String = "pi_quantity"
    Public Const PAR_I_NAME_UOM_XCD As String = "pi_uom_xcd"
    Public Const PAR_I_NAME_MATCHED_QUANTITY As String = "pi_matched_quantity"
    Public Const PAR_I_NAME_PAID_QUANTITY As String = "pi_paid_quantity"
    Public Const PAR_I_NAME_UNIT_PRICE As String = "pi_unit_price"
    Public Const PAR_I_NAME_TOTAL_PRICE As String = "pi_total_price"
    Public Const PAR_I_NAME_PARENT_LINE_NUMBER As String = "pi_parent_line_number"
    Public Const PAR_I_NAME_PO_NUMBER As String = "pi_po_number"
    Public Const PAR_I_NAME_PO_DATE As String = "pi_po_date"
    Public Const PAR_I_NAME_BILLING_PERIOD_START_DATE As String = "pi_billing_period_start_date"
    Public Const PAR_I_NAME_BILLING_PERIOD_END_DATE As String = "pi_billing_period_end_date"
    Public Const PAR_I_NAME_REFERENCE_NUMBER As String = "pi_reference_number"
    Public Const PAR_I_NAME_VENDOR_TRANSACTION_TYPE As String = "pi_vendor_transaction_type"
    Public Const PAR_O_AP_INVOICE_LINES_CUR As String = "po_invoice_lines"

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
    Public Function LoadList() As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadList(ByVal apInvoiceHeaderId As Guid) As DataSet
        Try
            Dim selectStmt As String = Me.Config("/SQL/LOAD_INVOICE_LINES")

            Dim inparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PAR_I_NAME_AP_INVOICE_HEADER_ID, apInvoiceHeaderId.ToByteArray)}
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PAR_O_AP_INVOICE_LINES_CUR, GetType(DataSet))}

            Dim ds As New DataSet
            Dim tbl As String = Me.TABLE_NAME

            DBHelper.FetchSp(selectStmt, inparameters, outParameters, ds, tbl)
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
            .AddParameter(PAR_I_NAME_AP_INVOICE_LINES_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_INVOICE_LINES_ID)
            .AddParameter(PAR_I_NAME_AP_INVOICE_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_INVOICE_HEADER_ID)
            .AddParameter(PAR_I_NAME_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_LINE_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LINE_TYPE)
            .AddParameter(PAR_I_NAME_VENDOR_ITEM_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_ITEM_CODE)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_QUANTITY)
            .AddParameter(PAR_I_NAME_UOM_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_UOM_XCD)
            .AddParameter(PAR_I_NAME_MATCHED_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_MATCHED_QUANTITY)
            .AddParameter(PAR_I_NAME_PAID_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_PAID_QUANTITY)
            .AddParameter(PAR_I_NAME_UNIT_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_UNIT_PRICE)
            .AddParameter(PAR_I_NAME_TOTAL_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_TOTAL_PRICE)
            .AddParameter(PAR_I_NAME_PARENT_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_PARENT_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_PO_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PO_NUMBER)
            .AddParameter(PAR_I_NAME_PO_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_PO_DATE)
            .AddParameter(PAR_I_NAME_BILLING_PERIOD_START_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_BILLING_PERIOD_START_DATE)
            .AddParameter(PAR_I_NAME_BILLING_PERIOD_END_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_BILLING_PERIOD_END_DATE)
            .AddParameter(PAR_I_NAME_REFERENCE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REFERENCE_NUMBER)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
            .AddParameter(PAR_I_NAME_VENDOR_TRANSACTION_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_TRANSACTION_TYPE)
        End With
        
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_AP_INVOICE_LINES_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_INVOICE_LINES_ID)
            .AddParameter(PAR_I_NAME_AP_INVOICE_HEADER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_AP_INVOICE_HEADER_ID)
            .AddParameter(PAR_I_NAME_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_LINE_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LINE_TYPE)
            .AddParameter(PAR_I_NAME_VENDOR_ITEM_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_ITEM_CODE)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_QUANTITY)
            .AddParameter(PAR_I_NAME_UOM_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_UOM_XCD)
            .AddParameter(PAR_I_NAME_MATCHED_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_MATCHED_QUANTITY)
            .AddParameter(PAR_I_NAME_PAID_QUANTITY, OracleDbType.Decimal, sourceColumn:=COL_NAME_PAID_QUANTITY)
            .AddParameter(PAR_I_NAME_UNIT_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_UNIT_PRICE)
            .AddParameter(PAR_I_NAME_TOTAL_PRICE, OracleDbType.Decimal, sourceColumn:=COL_NAME_TOTAL_PRICE)
            .AddParameter(PAR_I_NAME_PARENT_LINE_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_PARENT_LINE_NUMBER)
            .AddParameter(PAR_I_NAME_PO_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PO_NUMBER)
            .AddParameter(PAR_I_NAME_PO_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_PO_DATE)
            .AddParameter(PAR_I_NAME_BILLING_PERIOD_START_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_BILLING_PERIOD_START_DATE)
            .AddParameter(PAR_I_NAME_BILLING_PERIOD_END_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_BILLING_PERIOD_END_DATE)
            .AddParameter(PAR_I_NAME_REFERENCE_NUMBER, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REFERENCE_NUMBER)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
            .AddParameter(PAR_I_NAME_VENDOR_TRANSACTION_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_VENDOR_TRANSACTION_TYPE)
        End With
        
    End Sub
#End Region

End Class


