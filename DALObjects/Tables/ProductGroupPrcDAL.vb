Public Class ProductGroupPrcDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRODUCT_GROUP_DETAIL"
    Public Const TABLE_KEY_NAME As String = "product_group_detail_id"

    Public Const COL_NAME_PRODUCT_GROUP_DETAIL_ID As String = "product_group_detail_id"
    Public Const COL_NAME_PRODUCT_GROUP_ID As String = "product_group_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_GROUP_DETAIL_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList() As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function

    'This Method's body was added manually
    Public Sub LoadList(ByVal ds As DataSet, ByVal productGroupPrcId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters As New DBHelper.DBHelperParameter(TABLE_KEY_NAME, productGroupPrcId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {parameters})
    End Sub

    Public Sub LoadProductGroupList(ByVal ds As DataSet, ByVal productCode As String)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_PRODUCT_GROUP_LIST")
        Dim parameters As New DBHelper.DBHelperParameter(TABLE_KEY_NAME, productCode)
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {parameters})
    End Sub

    Public Sub LoadByProductGroupIdProductCodeId(ByVal familyDS As DataSet, ByVal productGroupId As Guid, ByVal productCodeId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_BY_PRODUCTGROUPID_PRODUCTCODEID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                    New DBHelper.DBHelperParameter(Me.COL_NAME_PRODUCT_GROUP_ID, productGroupId.ToByteArray), _
                    New DBHelper.DBHelperParameter(Me.COL_NAME_PRODUCT_CODE_ID, productCodeId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadGroupProductCodeIDs(ByVal productGroupId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_GROUP_PRODUCT_CODE_IDs")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_PRODUCT_GROUP_ID, productGroupId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadAllGroupProductCodeIDs(ByVal dealerID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ALL_GROUP_PRODUCT_CODE_IDs")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                      {New OracleParameter("company_group_id", dealerID.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
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


