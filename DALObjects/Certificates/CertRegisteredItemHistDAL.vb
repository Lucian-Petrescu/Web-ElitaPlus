'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/5/2017)********************


Public Class CertRegisteredItemHistDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_CERT_REGISTERED_ITEM_HIST"
	Public Const TABLE_KEY_NAME As String = "elp_cert_registered_item_hist"
	
	Public Const COL_NAME_ELP_CERT_REGISTERED_ITEM_HIST As String = "elp_cert_registered_item_hist"
	Public Const COL_NAME_CERT_REGISTERED_ITEM_ID As String = "cert_registered_item_id"
	Public Const COL_NAME_CERT_ITEM_ID As String = "cert_item_id"
	Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
	Public Const COL_NAME_PROD_ITEM_MANUF_EQUIP_ID As String = "prod_item_manuf_equip_id"
	Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
	Public Const COL_NAME_MODEL As String = "model"
	Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
	Public Const COL_NAME_ITEM_DESCRIPTION As String = "item_description"
	Public Const COL_NAME_RETAIL_PRICE As String = "retail_price"
	Public Const COL_NAME_PURCHASED_DATE As String = "purchased_date"
	Public Const COL_NAME_PURCHASE_PRICE As String = "purchase_price"
	Public Const COL_NAME_ENROLLMENT_ITEM As String = "enrollment_item"
	Public Const COL_NAME_ITEM_STATUS As String = "item_status"
	Public Const COL_NAME_VALIDATED_BY As String = "validated_by"
	Public Const COL_NAME_VALIDATED_DATE As String = "validated_date"
	Public Const COL_NAME_HIST_CREATED_BY As String = "hist_created_by"
	Public Const COL_NAME_HIST_CREATED_DATE As String = "hist_created_date"
	Public Const COL_NAME_HIST_MODIFIED_BY As String = "hist_modified_by"
	Public Const COL_NAME_HIST_MODIFIED_DATE As String = "hist_modified_date"
	Public Const COL_NAME_DEVICE_TYPE_ID As String = "device_type_id"
	Public Const COL_NAME_REGISTERED_ITEM_NAME As String = "registered_item_name"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("elp_cert_registered_item_hist", id.ToByteArray)}
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

    Public Function LoadRegItemHistList(certRegItemId As Guid, langId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_HISTORY")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                 {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, langId.ToByteArray),
                  New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, langId.ToByteArray),
                  New DBHelper.DBHelperParameter(COL_NAME_CERT_REGISTERED_ITEM_ID, certRegItemId.ToByteArray)}
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


