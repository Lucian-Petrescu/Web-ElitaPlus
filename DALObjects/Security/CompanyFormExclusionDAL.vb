Public Class CompanyFormExclusionDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMPANY_FORM_EXCLUSION"
    Public Const TABLE_KEY_NAME As String = "company_form_id"

    Public Const COL_NAME_FORM_ID As String = "form_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_COMPANY_FORM_ID As String = "company_form_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "LANGUAGE_ID"
    Public Const COL_NAME_TAB_ID As String = "TAB_ID"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_form_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(familyDS As DataSet, oFormId As Guid, oCompanyId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_FORMCOMPANY")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_FORM_ID, oFormId.ToByteArray), _
             New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, oCompanyId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function PopulateList(oLanguageID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_FORM_EXCLUSIONS")
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

    Public Function LoadFormListByTabID(TabID As Guid, oLanguageID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_FORMS_BY_TAB_ID")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageID.ToByteArray), _
                    New DBHelper.DBHelperParameter(COL_NAME_TAB_ID, TabID.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Public Function LoadCompanyPermissionListByFormID(FormID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_COMPANY_PERMISSION_BY_FORM_ID")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_FORM_ID, FormID.ToByteArray)}
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
