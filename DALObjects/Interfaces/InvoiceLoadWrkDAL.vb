'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/7/2013)********************


Public Class InvoiceLoadWrkDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_INVOICE_LOAD_WRK"
    Public Const TABLE_KEY_NAME As String = "invoice_load_wrk_id"

    Public Const COL_NAME_INVOICE_LOAD_WRK_ID As String = "invoice_load_wrk_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_COMPANY As String = "company"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_ATTRIBUTES As String = "attributes"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_LINE_ITEM_NUMBER As String = "line_item_number"
    Public Const COL_NAME_LINE_ITEM_CODE As String = "line_item_code"
    Public Const COL_NAME_LINE_ITEM_DESCRIPTION As String = "line_item_description"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_ENTIRE_RECORD As String = "entire_record"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("invoice_load_wrk_id", id.ToByteArray)}
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


