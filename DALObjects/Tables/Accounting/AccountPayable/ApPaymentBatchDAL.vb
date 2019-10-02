'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/2/2019)********************


Public Class ApPaymentBatchDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_AP_PAYMENT_BATCH"
	Public Const TABLE_KEY_NAME As String = "ap_payment_batch_id"
	
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("ap_payment_batch_id", id.ToByteArray)}
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


