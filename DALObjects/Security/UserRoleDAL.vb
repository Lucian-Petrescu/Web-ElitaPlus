'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/17/2006)********************


Public Class UserRoleDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_USER_ROLE"
    Public Const TABLE_KEY_NAME As String = "user_role_id"

    Public Const COL_NAME_USER_ROLE_ID As String = "user_role_id"
    Public Const COL_NAME_ROLE_ID As String = "role_id"
    Public Const COL_NAME_USER_ID As String = "user_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("user_role_id", id.ToByteArray)}
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

    Public Sub LoadByUserIdRoleId(familyDS As DataSet, userId As Guid, roleId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_BY_USERID_ROLEID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                    New DBHelper.DBHelperParameter("user_id", userId.ToByteArray), _
                    New DBHelper.DBHelperParameter("role_id", roleId.ToByteArray)}
        Try
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class



