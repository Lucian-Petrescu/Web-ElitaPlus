Public Class RoleAuthTabsExclusionDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ROLE_AUTH_TABS_EXCLUSION"
    Public Const TABLE_KEY_NAME As String = "auth_tab_role_id"

    Public Const COL_NAME_ROLE_ID As String = "role_id"
    Public Const COL_NAME_TAB_ID As String = "tab_id"
    Public Const COL_NAME_AUTH_TAB_ROLE_ID As String = "auth_tab_role_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "LANGUAGE_ID"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("auth_tab_role_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(familyDS As DataSet, oTabId As Guid, oRoleId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_TABROLE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter(COL_NAME_TAB_ID, oTabId.ToByteArray), _
            New DBHelper.DBHelperParameter(COL_NAME_ROLE_ID, oRoleId.ToByteArray)}
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

    Public Function PopulateList(oLanguageID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_TABS_PERMISSIONS")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageID.ToByteArray) _
                    }
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Public Function LoadTabList(oLanguageID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_TAB_LIST")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageID.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
    End Function

    Public Function LoadPermissionByTabID(TabID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_PERMISSIONS_BY_TAB_ID")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_TAB_ID, TabID.ToByteArray) }
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
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
