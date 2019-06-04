'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/12/2008)********************


Public Class TransDtlClmUpdte2elitaDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_TRANS_DTL_CLM_UPDTE_2ELITA"
    Public Const TABLE_KEY_NAME As String = "trans_dtl_clm_updte_2elita_id"

    Public Const COL_NAME_TRANS_DTL_CLM_UPDTE_2ELITA_ID As String = "trans_dtl_clm_updte_2elita_id"
    Public Const COL_NAME_TRANSACTION_LOG_HEADER_ID As String = "transaction_log_header_id"
    Public Const COL_NAME_ITEM_NUMBER As String = "item_number"
    Public Const COL_NAME_RESPONSE As String = "response"
    Public Const COL_NAME_RESPONSE_DETAIL As String = "response_detail"
    Public Const COL_NAME_XML_CLAIM_NUMBER As String = "xml_claim_number"
    Public Const COL_NAME_XML_SERVICE_ORDER_NUMBER As String = "xml_service_order_number"
    Public Const COL_NAME_XML_EXTERNAL_ITEM_CODE As String = "xml_external_item_code"
    Public Const COL_NAME_XML_IN_HOME_VISIT_DATE As String = "xml_in_home_visit_date"
    Public Const COL_NAME_XML_VISIT_DATE As String = "xml_visit_date"
    Public Const COL_NAME_XML_DEFECT_REASON As String = "xml_defect_reason"
    Public Const COL_NAME_XML_TECHNICAL_REPORT As String = "xml_technical_report"
    Public Const COL_NAME_XML_LABOR As String = "xml_labor"
    Public Const COL_NAME_XML_TRIP_AMOUNT As String = "xml_trip_amount"
    Public Const COL_NAME_XML_EXPECTED_REPAIR_DATE As String = "xml_expected_repair_date"
    Public Const COL_NAME_XML_QUOTATION_DATE As String = "xml_quotation_date"
    Public Const COL_NAME_XML_CLAIM_STATUS As String = "xml_claim_status"
    Public Const COL_NAME_XML_REPAIR_DATE As String = "xml_repair_date"
    Public Const COL_NAME_XML_SHIPPING As String = "xml_shipping"
    Public Const COL_NAME_XML_PICKUP_DATE As String = "xml_pickup_date"
    Public Const COL_NAME_XML_E_TICKET As String = "xml_e_ticket"
    Public Const COL_NAME_XML_COLLECT_DATE As String = "xml_collect_date"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("trans_dtl_clm_updte_2elita_id", id.ToByteArray)}
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


