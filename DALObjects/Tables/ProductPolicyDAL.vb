'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/17/2013)********************


Public Class ProductPolicyDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRODUCT_POLICY"
    Public Const TABLE_KEY_NAME As String = "product_policy_id"

    Public Const COL_NAME_PRODUCT_POLICY_ID As String = "product_policy_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_TYPE_OF_EQUIPMENT_ID As String = "type_of_equipment_id"
    Public Const COL_NAME_EXTERNAL_PROD_CODE_ID As String = "external_prod_code_id"
    Public Const COL_NAME_POLICY_NUM As String = "policy"
    Public Const COL_NAME_EXTERNAL_PROD_CODE As String = "external_prod_code"
    Public Const COL_NAME_TYPE_OF_EQUIPMENT As String = "type_of_equipment"

    Private Const DSNAME As String = "LIST"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("product_policy_id", id.ToByteArray)}
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


#Region "CRUD Methods"

    Public Function LoadList(ByVal languageId As Guid, ByVal ProductCodeId As Guid, ByVal familyDS As DataSet)

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        ' Dim ds As DataSet
        Dim dcPk As DataColumnCollection

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                            New OracleParameter(COL_NAME_PRODUCT_CODE_ID, ProductCodeId.ToByteArray)}

        Try

            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
            '  familyDS.Tables(Me.TABLE_NAME).PrimaryKey = New DataColumn() {familyDS.Tables(Me.TABLE_NAME).Columns("Product_Policy_Id")}
            '   familyDS = DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters)

            '   familyDS.Tables(0).PrimaryKey = New DataColumn()  {familyDS.Tables(0).Columns("Product_Policy_Id")}

            ' dcPk.Item = ds.Tables(Me.TABLE_NAME).Columns(0)
            ' ds.Tables(Me.TABLE_NAME).PrimaryKey = dcPk

            '   Return (familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(ByVal languageId As Guid, ByVal ProductCodeId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim dcPk As DataColumnCollection

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                            New OracleParameter(COL_NAME_PRODUCT_CODE_ID, ProductCodeId.ToByteArray)}

        Try
            ds = DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters)

            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("Product_Policy_Id")}

            ' dcPk.Item = ds.Tables(Me.TABLE_NAME).Columns(0)
            ' ds.Tables(Me.TABLE_NAME).PrimaryKey = dcPk

            Return (ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


  
#End Region

End Class


