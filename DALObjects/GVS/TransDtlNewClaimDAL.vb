'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/12/2008)********************


Public Class TransDtlNewClaimDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_TRANS_DTL_NEW_CLAIM"
    Public Const TABLE_KEY_NAME As String = "trans_dtl_new_claim_id"

    Public Const COL_NAME_TRANS_DTL_NEW_CLAIM_ID As String = "trans_dtl_new_claim_id"
    Public Const COL_NAME_TRANSACTION_LOG_HEADER_ID As String = "transaction_log_header_id"
    Public Const COL_NAME_ITEM_NUMBER As String = "item_number"
    Public Const COL_NAME_RESPONSE As String = "response"
    Public Const COL_NAME_RESPONSE_DETAIL As String = "response_detail"
    Public Const COL_NAME_XML_CLAIM_NUMBER As String = "xml_claim_number"
    Public Const COL_NAME_XML_CREATED_DATE As String = "xml_created_date"
    Public Const COL_NAME_XML_SERVICE_CENTER_CODE As String = "xml_service_center_code"
    Public Const COL_NAME_XML_CUSTOMER_NAME As String = "xml_customer_name"
    Public Const COL_NAME_XML_IDENTIFICATION_NUMBER As String = "xml_identification_number"
    Public Const COL_NAME_XML_ADDRESS1 As String = "xml_address1"
    Public Const COL_NAME_XML_ADDRESS2 As String = "xml_address2"
    Public Const COL_NAME_XML_CITY As String = "xml_city"
    Public Const COL_NAME_XML_REGION As String = "xml_region"
    Public Const COL_NAME_XML_POSTAL_CODE As String = "xml_postal_code"
    Public Const COL_NAME_XML_HOME_PHONE As String = "xml_home_phone"
    Public Const COL_NAME_XML_WORK_PHONE As String = "xml_work_phone"
    Public Const COL_NAME_XML_EMAIL As String = "xml_email"
    Public Const COL_NAME_XML_CONTACT_NAME As String = "xml_contact_name"
    Public Const COL_NAME_XML_PRODUCT_CODE As String = "xml_product_code"
    Public Const COL_NAME_XML_DESCRIPTION As String = "xml_description"
    Public Const COL_NAME_XML_ITEM_DESCRIPTION As String = "xml_item_description"
    Public Const COL_NAME_XML_SERIAL_NUMBER As String = "xml_serial_number"
    Public Const COL_NAME_XML_INVOICE_NUMBER As String = "xml_invoice_number"
    Public Const COL_NAME_XML_PROBLEM_DESCRIPTION As String = "xml_problem_description"
    Public Const COL_NAME_XML_METHOD_OF_REPAIR As String = "xml_method_of_repair"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("trans_dtl_new_claim_id", id.ToByteArray)}
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


