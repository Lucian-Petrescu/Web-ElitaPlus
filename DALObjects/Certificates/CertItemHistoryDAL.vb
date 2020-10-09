'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/11/2010)********************


Public Class CertItemHistoryDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CERT_ITEM_HISTORY"
    Public Const TABLE_KEY_NAME As String = "cert_item_hist_id"

    Public Const COL_NAME_CERT_ITEM_HIST_ID As String = "cert_item_hist_id"
    Public Const COL_NAME_CERT_ITEM_ID As String = "cert_item_id"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_ITEM_NUMBER As String = "item_number"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_MAX_REPLACEMENT_COST As String = "max_replacement_cost"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_ITEM_CREATED_BY As String = "item_created_by"
    Public Const COL_NAME_ITEM_CREATED_DATE As String = "item_created_date"
    Public Const COL_NAME_ITEM_MODIFIED_BY As String = "item_modified_by"
    Public Const COL_NAME_ITEM_MODIFIED_DATE As String = "item_modified_date"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_ITEM_DESCRIPTION As String = "item_description"
    Public Const COL_NAME_ITEM_CODE As String = "item_code"
    Public Const COL_NAME_ITEM_RETAIL_PRICE As String = "item_retail_price"
    Public Const COL_NAME_ITEM_REPLACE_RETURN_DATE As String = "item_replace_return_date"
    Public Const COL_NAME_EXTERNAL_PRODUCT_CODE As String = "external_product_code"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_item_hist_id", id.ToByteArray)}
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

    Public Function LoadItemHistList(certId As Guid, certItemId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_HISTORY")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                 {New DBHelper.DBHelperParameter(COL_NAME_CERT_ID, certId.ToByteArray), _
                  New DBHelper.DBHelperParameter(COL_NAME_CERT_ITEM_ID, certItemId.ToByteArray)}
        Try
            Dim ds = New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
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


