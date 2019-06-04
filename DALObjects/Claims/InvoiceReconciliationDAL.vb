'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/21/2013)********************


Public Class InvoiceReconciliationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_INVOICE_RECONCILIATION"
    Public Const TABLE_KEY_NAME As String = "invoice_reconciliation_id"

    Public Const COL_NAME_INVOICE_RECONCILIATION_ID As String = "invoice_reconciliation_id"
    Public Const COL_NAME_CLAIM_AUTH_ITEM_ID As String = "claim_auth_item_id"
    Public Const COL_NAME_INVOICE_ITEM_ID As String = "invoice_item_id"
    Public Const COL_NAME_RECONCILIATION_STATUS_ID As String = "reconciliation_status_id"
    Public Const COL_NAME_RECONCILED_AMOUNT As String = "reconciled_amount"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("invoice_reconciliation_id", id.ToByteArray)}
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

    Public Function CanUndoBalanceInvoice(ByVal invoiceId As Guid) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/CAN_UNDO_BALANCE_INVOICE")
        Dim returnCount As Decimal = DirectCast(DBHelper.ExecuteScalar(selectStmt, New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(InvoiceDAL.COL_NAME_INVOICE_ID, invoiceId.ToByteArray)}), Decimal)
        Return (returnCount = 0)
    End Function

    Public Function LoadByInvoiceItemId(ByVal familyDS As DataSet, ByVal invoiceItemId As Guid) As DataRow
        Dim selectStmt As String = Me.Config("/SQL/LOAD_BY_INVOICE_ITEM_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("invoice_item_id", invoiceItemId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadByClaimAuthItemId(ByVal familyDS As DataSet, ByVal claimAuthItemId As Guid) As DataRow
        Dim selectStmt As String = Me.Config("/SQL/LOAD_BY_CLAIM_AUTH_ITEM_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_auth_item_id", claimAuthItemId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetClaimAuthorizationStatus(ByVal claimAuthorizationId As Guid) As Guid
        Dim selectStmt As String = Me.Config("/SQL/GET_INVOICE_AUTHORIZATION_STATUS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_authorization_id", claimAuthorizationId.ToByteArray)}
        Dim result As Object
        Try
            result = DBHelper.ExecuteScalar(selectStmt, parameters)
            If result Is Nothing Then
                Return Guid.Empty
            Else
                Return New Guid(CType(result, Byte()))

            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
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


