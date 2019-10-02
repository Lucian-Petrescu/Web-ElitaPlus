'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/2/2019)********************


Public Class ApInvoiceHeaderDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_AP_INVOICE_HEADER"
	Public Const TABLE_KEY_NAME As String = "ap_invoice_header_id"
	
	Public Const COL_NAME_AP_INVOICE_HEADER_ID As String = "ap_invoice_header_id"
	Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
	Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
	Public Const COL_NAME_INVOICE_AMOUNT As String = "invoice_amount"
	Public Const COL_NAME_TERM_XCD As String = "term_xcd"
	Public Const COL_NAME_PAID_AMOUNT As String = "paid_amount"
	Public Const COL_NAME_PAYMENT_STATUS_XCD As String = "payment_status_xcd"
	Public Const COL_NAME_SOURCE As String = "source"
	Public Const COL_NAME_VENDOR_ID As String = "vendor_id"
	Public Const COL_NAME_VENDOR_ADDRESS_ID As String = "vendor_address_id"
	Public Const COL_NAME_SHIP_TO_ADDRESS_ID As String = "ship_to_address_id"
	Public Const COL_NAME_CURRENCY_ISO_CODE As String = "currency_iso_code"
	Public Const COL_NAME_EXCHANGE_RATE As String = "exchange_rate"
	Public Const COL_NAME_APPROVED_XCD As String = "approved_xcd"
	Public Const COL_NAME_ACCOUNTING_PERIOD As String = "accounting_period"
	Public Const COL_NAME_DISTRIBUTED As String = "distributed"
	Public Const COL_NAME_POSTED As String = "posted"
	Public Const COL_NAME_DEALER_ID As String = "dealer_id"
	Public Const COL_NAME_COMPANY_ID As String = "company_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("ap_invoice_header_id", id.ToByteArray)}
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


