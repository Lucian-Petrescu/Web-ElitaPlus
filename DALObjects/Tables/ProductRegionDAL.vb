'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (6/21/2012)********************


Public Class ProductRegionDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRODUCT_REGION"
    Public Const TABLE_KEY_NAME As String = "product_region_id"

    Public Const COL_NAME_PRODUCT_REGION_ID As String = "product_region_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_REGION_ID As String = "region_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("product_region_id", id.ToByteArray)}
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

    Public Sub LoadList(ds As DataSet, ProductCodeId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ProductCodeIdParam As New DBHelper.DBHelperParameter("product_code_id", ProductCodeId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {ProductCodeIdParam})
        'Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Sub


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


