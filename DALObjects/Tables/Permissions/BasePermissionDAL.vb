Public MustInherit Class BasePermissionDAL(Of TType As BasePermissionDAL(Of TType))
    Inherits DALBase

    Public ReadOnly TABLE_NAME As String
    Public ReadOnly TABLE_KEY_NAME As String
    Public ReadOnly COL_NAME_ENTITY_ID As String
    Public Const COL_NAME_PERMISSION_ID As String = "PERMISSION_ID"

    Protected Friend Sub New(tableName As String, tableKeyName As String, entityIdColumnName As String)
        TABLE_NAME = tableName
        TABLE_KEY_NAME = tableKeyName
        COL_NAME_ENTITY_ID = entityIdColumnName
    End Sub

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(TABLE_KEY_NAME, id.ToByteArray)}
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

    Public Sub LoadList(familyDS As DataSet, entityId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter(COL_NAME_ENTITY_ID, OracleDbType.Raw, 16)}
        Try
            parameters(0).Value = entityId.ToByteArray
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

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


