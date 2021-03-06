'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/13/2006)********************


Public Class RoleAuthFormInclusionDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ROLE_AUTH_FORM_INCLUSION"
    Public Const TABLE_KEY_NAME As String = "role_form_id"

    Public Const COL_NAME_ROLE_ID As String = "role_id"
    Public Const COL_NAME_FORM_ID As String = "form_id"
    Public Const COL_NAME_PERMISSION_TYPE As String = "permission_type"
    Public Const COL_NAME_ROLE_FORM_ID As String = "role_form_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "LANGUAGE_ID"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("role_form_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal oFormId As Guid, ByVal oRoleId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_FORMROLE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(Me.COL_NAME_FORM_ID, oFormId.ToByteArray), _
             New DBHelper.DBHelperParameter(Me.COL_NAME_ROLE_ID, oRoleId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function PopulateList(ByVal oLanguageID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_FORMS_PERMISSIONS")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageID.ToByteArray) _
                    }
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Public Function LoadTabFormList(ByVal oLanguageID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_TABS_FORMS")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageID.ToByteArray) _
                    }
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Public Function LoadPermissionByFormID(ByVal FormID As Guid) As DataSet
        Dim selectStmt As Text.StringBuilder = New Text.StringBuilder(Me.Config("/SQL/GET_PERMISSIONS_BY_FORM_ID"))
        Dim strFormID As String = MiscUtil.GetDbStringFromGuid(FormID)
        selectStmt.Replace("#FORMID", strFormID)
        Dim ds As New DataSet
        Try
            ds = DBHelper.Fetch(selectStmt.ToString, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
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



