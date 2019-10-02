'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/2/2019)********************


Public Class ApInvoiceMatchingDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_AP_INVOICE_MATCHING"
	Public Const TABLE_KEY_NAME As String = "invoice_matching_id"
	
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("invoice_matching_id", id.ToByteArray)}
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


