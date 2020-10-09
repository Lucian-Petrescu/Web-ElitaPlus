'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/12/2008)********************


Public Class TransDtlServiceCenterDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_TRANS_DTL_SERVICE_CENTER"
    Public Const TABLE_KEY_NAME As String = "trans_dtl_service_center_id"

    Public Const COL_NAME_TRANS_DTL_SERVICE_CENTER_ID As String = "trans_dtl_service_center_id"
    Public Const COL_NAME_TRANSACTION_LOG_HEADER_ID As String = "transaction_log_header_id"
    Public Const COL_NAME_ITEM_NUMBER As String = "item_number"
    Public Const COL_NAME_RESPONSE As String = "response"
    Public Const COL_NAME_RESPONSE_DETAIL As String = "response_detail"
    Public Const COL_NAME_XML_SERVICE_CENTER_CODE As String = "xml_service_center_code"
    Public Const COL_NAME_XML_DESCRIPTION As String = "xml_description"
    Public Const COL_NAME_XML_TAX_ID As String = "xml_tax_id"
    Public Const COL_NAME_XML_STATUS_CODE As String = "xml_status_code"

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
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("trans_dtl_service_center_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


