

'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/9/2013)********************


Public Class DailyObdFileDetailTempDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DAILY_OBD_FILE_DETAIL_TEMP"
    Public Const TABLE_KEY_NAME As String = "file_detail_temp_id"

    Public Const COL_NAME_FILE_DETAIL_TEMP_ID As String = "file_detail_temp_id"
    Public Const COL_NAME_FILE_HEADER_TEMP_ID As String = "file_header_temp_id"
    Public Const COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const COL_NAME_WARRANTY_SALES_DATE As String = "warranty_sales_date"
    Public Const COL_NAME_CERT_CREATED_DATE As String = "cert_created_date"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_ITEM_RETAIL_PRICE As String = "item_retail_price"
    Public Const COL_NAME_CANCELLATION_DATE As String = "cancellation_date"
    Public Const COL_NAME_REFUND_AMT As String = "refund_amt"
    Public Const COL_NAME_PAYMENT_INSTRUMENT As String = "payment_instrument"
    Public Const COL_NAME_ACCOUNT_NUMBER As String = "account_number"
    Public Const COL_NAME_CREDIT_CARD_NUMBER As String = "credit_card_number"
    Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const COL_NAME_INSTALLMENT_AMOUNT As String = "installment_amount"
    Public Const COL_NAME_CESS_SALESREP As String = "cess_salesrep"
    Public Const COL_NAME_CESS_OFFICE As String = "cess_office"
    Public Const COL_NAME_SALES_REP_NUMBER As String = "sales_rep_number"
    Public Const COL_NAME_SALES_DEPARTMENT As String = "sales_department"
    Public Const COL_NAME_BUSINESSLINE As String = "businessline"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_DEALER_BRANCH_CODE As String = "dealer_branch_code"
    Public Const COL_NAME_DOCUMENT_TYPE As String = "document_type"
    Public Const COL_NAME_IDENTIFICATION_NUMBER As String = "identification_number"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_POSTAL_CODE As String = "postal_code"
    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_HOME_PHONE As String = "home_phone"
    Public Const COL_NAME_MFG_DESCRIPTION As String = "mfg_description"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_ADDITIONAL_INFO As String = "additional_info"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_LINKED_CERT_NUMBER As String = "linked_cert_number"
    Public Const COL_NAME_DATE_PAID_FOR As String = "date_paid_for"
    Public Const COL_NAME_BILLED_AMOUNT As String = "billed_amount"
    Public Const COL_NAME_INSTALLMENT_NUMBER As String = "installment_number"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_RECORD_SELECTED As String = "record_selected"
    Public Const COL_NAME_CANCELLATION_REASON_CODE As String = "cancellation_reason_code"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("file_detail_temp_id", id.ToByteArray)}
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
    Public Sub delete(ByVal detailtempID As Guid)
        Try
            Dim deletestmt As String = Me.Config("/SQL/DELETE")
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_FILE_DETAIL_TEMP_ID, detailtempID.ToByteArray)}
            DBHelper.Execute(deletestmt, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region


    'Sub getHeaderRecordList(ByVal DealerCode As String, ByVal CompanyCode As String, ByVal CreatedDate As Date, ByVal ModifiedDate As Date, ByVal NewEnrollment As String, ByVal Billing As String, ByVal Cancellations As String)
    '    Dim sqlStmt As String
    '    sqlStmt = Me.Config("/SQL/ELP_CREATE_ECI_OUTBOUND_FILE")
    '    Try
    '        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("v_dealer_id", DealerID.ToByteArray), New DBHelper.DBHelperParameter("v_network_id", userNetworkId)}
    '        Dim outParameters() As DBHelper.DBHelperParameter
    '        DBHelper.ExecuteSp(sqlStmt, inParameters, outParameters)
    '        'ExecuteSPCreateInvoice(DealerID, userNetworkId, sqlStmt)
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Sub


End Class



