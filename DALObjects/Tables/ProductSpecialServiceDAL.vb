
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/28/2010)********************


Public Class ProductSpecialServiceDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRODUCT_SPECIAL_SERVICE"
    Public Const TABLE_KEY_NAME As String = "product_special_service_id"

    Public Const COL_NAME_PRODUCT_SPECIAL_SERVICE_ID As String = "product_special_service_id"
    Public Const COL_NAME_SPECIAL_SERVICE_ID As String = "special_service_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("product_special_service_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(ds As DataSet, ProductSpecialServiceId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim serviceCenterZipDistrictParam As New DBHelper.DBHelperParameter("product_special_service_id", ProductSpecialServiceId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {serviceCenterZipDistrictParam})
        'Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Sub


    Public Function LoadProdSplSvcList(SpecialServiceId As Guid, ProductCodeId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/LOAD_BY_PROD_CODE_SPL_SVC")
        Try
            parameters = New OracleParameter() {New OracleParameter(COL_NAME_SPECIAL_SERVICE_ID, SpecialServiceId.ToByteArray), _
                                New OracleParameter(COL_NAME_ID, ProductCodeId.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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


