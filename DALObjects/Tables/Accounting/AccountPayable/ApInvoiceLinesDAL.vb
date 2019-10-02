'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/2/2019)********************


Public Class ApInvoiceLinesDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_AP_INVOICE_LINES"
	Public Const TABLE_KEY_NAME As String = "ap_invoice_lines_id"
	
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
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("ap_invoice_lines_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function    
    

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
		If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


