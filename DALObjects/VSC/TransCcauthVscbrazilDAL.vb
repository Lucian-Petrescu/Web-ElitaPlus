'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/18/2012)********************


Public Class TransCcauthVscbrazilDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_TRANS_CCAUTH_VSCBRAZIL"
    Public Const TABLE_KEY_NAME As String = "trans_ccauth_vscbrazil_id"

    Public Const COL_NAME_TRANS_CCAUTH_VSCBRAZIL_ID As String = "trans_ccauth_vscbrazil_id"
    Public Const COL_NAME_CUSTOMER_NAME As String = "customer_name"
    Public Const COL_NAME_DOCUMENT_NUM As String = "document_num"
    Public Const COL_NAME_CERTIFICATE_NUM As String = "certificate_num"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_NUM_OF_INSTALLMENTS As String = "num_of_installments"
    Public Const COL_NAME_NAME_ON_CARD As String = "name_on_card"
    Public Const COL_NAME_CARD_NUM As String = "card_num"
    Public Const COL_NAME_CARD_SECURITY_CODE As String = "card_security_code"
    Public Const COL_NAME_CARD_EXPIRATION As String = "card_expiration"
    Public Const COL_NAME_CARD_TYPE As String = "card_type"

    'New Elements 
    Public Const DATA_COL_NAME_DBS_COMPANY_CODE As String = "dbs_company_code"
    Public Const DATA_COL_NAME_DBS_PRODUCT_CODE As String = "dbs_product_code"
    Public Const DATA_COL_NAME_DBS_SYSTEM_CODE As String = "dbs_system_code"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const DATA_COL_NAME_EMAIL As String = "email"
    Public Const DATA_COL_NAME_MOBILE_AREA_CODE As String = "mobile_area_code"
    Public Const DATA_COL_NAME_MOBILE As String = "mobile"
    Public Const DATA_COL_NAME_PHONE_AREA_CODE As String = "phone_area_code"
    Public Const DATA_COL_NAME_PHONE As String = "phone"
    Public Const DATA_COL_NAME_WARRANTY_SALES_DATE As String = "warranty_sales_date"
    Public Const DATA_COL_NAME_CARD_OWNER_TAX_ID As String = "card_owner_tax_id"
    Public Const DATA_COL_NAME_DBS_PAYMENT_TYPE As String = "dbs_payment_type"
    Public Const DATA_COL_NAME_DUE_DATE As String = "due_date"
    Public Const DATA_COL_NAME_EXPIRED_DATE As String = "expired_date"

    Public Const COL_NAME_AUTH_STATUS As String = "auth_status"
    Public Const COL_NAME_AUTH_NUM As String = "auth_num"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_MSG As String = "reject_msg"
    Public Const COL_NAME_AUTH_DATE As String = "auth_date"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("trans_ccauth_vscbrazil_id", id.ToByteArray)}
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