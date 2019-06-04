'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/12/2014)********************


Public Class ExcludedLiRolesDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_EXCLUDED_LI_ROLES"
    Public Const TABLE_KEY_NAME As String = "excluded_li_roles_id"

    Public Const COL_NAME_EXCLUDED_LI_ROLES_ID As String = "excluded_li_roles_id"
    Public Const COL_NAME_EXCLUDE_LISTITEM_ROLE_ID As String = "exclude_listitem_role_id"
    Public Const COL_NAME_ROLE_ID As String = "role_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("excluded_li_roles_id", id.ToByteArray)}
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

    Public Sub LoadList(ByVal ds As DataSet, ByVal ExcludeListitemRoleId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ExcludeListitemRoleIdParam As New DBHelper.DBHelperParameter("exclude_listitem_role_id", ExcludeListitemRoleId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {ExcludeListitemRoleIdParam})
        'Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Sub

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


