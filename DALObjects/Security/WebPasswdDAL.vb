'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/7/2009)********************


Public Class WebPasswdDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_WEB_PASSWD"
    Public Const TABLE_KEY_NAME As String = "web_pass_id"

    Public Const COL_NAME_WEB_PASS_ID As String = "web_pass_id"
    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const COL_NAME_PASSWORD As String = "password"
    Public Const COL_NAME_ENV As String = "env"
    Public Const COL_NAME_URL As String = "url"
    Public Const COL_NAME_TOKEN As String = "token"
    Public Const COL_NAME_TOKEN_CREATED_DATE As String = "token_created_date"
    Public Const COL_NAME_NUM_PER_PROCESS As String = "num_per_process"
    Public Const COL_NAME_TOKEN_DURATION As String = "token_duration"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_IS_EXTERNAL As String = "is_external"
    Public Const COL_NAME_AUTHENTICATION_KEY As String = "authentication_key"
    Public Const COL_NAME_SERVICE_TYPE_ID As String = "service_type_id"

    'fix for Def-2229
    Public Const COL_NAME_HUB As String = "hub"
    Public Const COL_NAME_GENERIC_URL As String = "generic_url"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("web_pass_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(familyDS As DataSet, companyGroupId As Guid, serviceTypeId As Guid, env As String, _
                    isExternal As Boolean, hub As String)
        Dim selectStmt As String = Config("/SQL/LOAD_WEB_PASSWD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                        New DBHelper.DBHelperParameter(COL_NAME_ENV, IIf(String.IsNullOrEmpty(env), DBNull.Value, env.ToUpper())), _
                                        New DBHelper.DBHelperParameter(COL_NAME_ENV, IIf(String.IsNullOrEmpty(env), DBNull.Value, env.ToUpper())), _
                                        New DBHelper.DBHelperParameter(COL_NAME_HUB, IIf(String.IsNullOrEmpty(hub), DBNull.Value, hub.ToUpper())), _
                                        New DBHelper.DBHelperParameter(COL_NAME_HUB, IIf(String.IsNullOrEmpty(hub), DBNull.Value, hub.ToUpper())), _
                                        New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, IIf(IsNothing(companyGroupId), DBNull.Value, companyGroupId)), _
                                        New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, IIf(IsNothing(companyGroupId), DBNull.Value, companyGroupId)), _
                                        New DBHelper.DBHelperParameter(COL_NAME_IS_EXTERNAL, IIf(isExternal, "Y", "N")), _
                                        New DBHelper.DBHelperParameter(COL_NAME_SERVICE_TYPE_ID, IIf(IsNothing(serviceTypeId), DBNull.Value, serviceTypeId)), _
                                        New DBHelper.DBHelperParameter(COL_NAME_SERVICE_TYPE_ID, IIf(IsNothing(serviceTypeId), DBNull.Value, serviceTypeId))}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Fix for Def-2229
    Public Sub Load(familyDS As DataSet, env As String, compGpId As Guid, Optional ByVal hub As String = Nothing)
        Dim selectStmt As String = Config("/SQL/LOAD_ENV_COMPANY_GROUP")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                        New DBHelper.DBHelperParameter(COL_NAME_ENV, env), _
                                            New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, compGpId), _
                                                New DBHelper.DBHelperParameter(COL_NAME_HUB, IIf(String.IsNullOrEmpty(hub), DBNull.Value, hub.ToUpper()))}
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


