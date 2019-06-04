'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/21/2013)********************


Public Class InvoiceItemDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_INVOICE_ITEM"
    Public Const TABLE_KEY_NAME As String = "invoice_item_id"

    Public Const COL_NAME_INVOICE_ITEM_ID As String = "invoice_item_id"
    Public Const COL_NAME_INVOICE_ID As String = "invoice_id"
    Public Const COL_NAME_CLAIM_AUTHORIZATION_ID As String = "claim_authorization_id"
    Public Const COL_NAME_LINE_ITEM_NUMBER As String = "line_item_number"
    Public Const COL_NAME_VENDOR_SKU As String = "vendor_sku"
    Public Const COL_NAME_VENDOR_SKU_DESCRIPTION As String = "vendor_sku_description"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_ADJUSTMENT_REASON_ID As String = "adjustment_reason_id"
    Public Const COL_NAME_SERVICE_CLASS_ID As String = "service_class_id"
    Public Const COL_NAME_SERVICE_TYPE_ID As String = "service_type_id"
    Public Const COL_NAME_SERVICE_LEVEL_ID As String = "service_level_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("invoice_item_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal familyDS As DataSet, ByVal invoiceId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = _
           New DBHelper.DBHelperParameter() _
           { _
               New DBHelper.DBHelperParameter(InvoiceItemDAL.COL_NAME_INVOICE_ID, invoiceId.ToByteArray()) _
           }
        Try
            familyDS = DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
            Return familyDS
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


