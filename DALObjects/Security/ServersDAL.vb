'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/17/2006)********************


Public Class ServersDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERVERS"
    Public Const TABLE_KEY_NAME As String = "server_id"

    Public Const COL_NAME_SERVER_ID As String = "server_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_HUB_REGION As String = "hub_region"
    Public Const COL_NAME_MACHINE_PREFIX As String = "machine_prefix"
    Public Const COL_NAME_ENVIRONMENT As String = "environment"
    Public Const COL_NAME_WEB_SERVICE_NAME_1 As String = "web_service_name1"
    Public Const COL_NAME_WEB_SERVICE_NAME_2 As String = "web_service_name2"
    Public Const COL_NAME_WEB_SERVICE_FUNCTION_NAME As String = "function_name"
    Public Const COL_NAME_FTP_HOSTNAME As String = "ftp_hostname"
    Public Const COL_NAME_CRYSTAL_SDK As String = "crystal_sdk"
    Public Const COL_NAME_CRYSTAL_VIEWER As String = "crystal_viewer"
    Public Const COL_NAME_FELITA_FTP_HOSTNAME As String = "felita_ftp_hostname"
    Public Const COL_NAME_LDAP_IP As String = "ldap_ip"
    Public Const COL_NAME_FTP_HOST_PATH As String = "ftp_host_path"
    Public Const COL_NAME_FTP_TRIGGER_EXTENSION As String = "ftp_trigger_extension"
    Public Const COL_NAME_FTP_SPLIT_PATH As String = "ftp_split_path"
    Public Const COL_NAME_SMARTSTREAM_HOSTNAME As String = "smartstream_hostname"
    Public Const COL_NAME_SERVICEORDER_IMAGE_HOSTNAME As String = "serviceorder_image_hostname"
    Public Const COL_NAME_PRIVACY_LEVEL_XCD As String = "privacy_level_xcd"
    Public Const COL_NAME_DATABASE_NAME As String = "database_name"
    Public Const COL_NAME_WEB_SERVICE_OFF_LINE_MESSAGE As String = "ws_off_line_message"
    Public Const COL_NAME_WEB_SERVICE_FUNCTION_OFF_LINE_MESSAGE As String = "function_off_line_message"
    Public Const COL_NAME_BATCH_HOSTNAME As String = "batch_hostname"
    Public Const COL_NAME_SMARTSTREAM_AP_UPLOAD As String = "smartstream_ap_upload"
    Public Const COL_NAME_SMARTSTREAM_GL_UPLOAD As String = "smartstream_gl_upload"
    Public Const COL_NAME_SMARTSTREAM_GL_STATUS As String = "smartstream_gl_status"
    Public Const COL_NAME_ACCT_BALANCE_HOSTNAME As String = "acct_balance_hostname"
    Public Const COL_NAME_DB_UNIQUE_NAME As String = "db_unique_name"
    Public Const COL_NAME_NO_OF_PARALLEL_PROCESSES = "no_of_parallel_processes"
    Public Const COL_NAME_COMMIT_FREQUENCY = "commit_frequency"

    '  Public Const COL_NAME_LDAP_IP_DR As String = "ldap_ip_dr"

    Public Const PARAM_NETWORK_ID As String = "NETWORK_ID"
    Public Const PARAM_PASSWORD As String = "PASSWORD"
    Public Const PARAM_LDAPGROUP As String = "LDAPGROUP"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("server_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(familyDS As DataSet, sHubRegion As String, sMachinePrefix As String, _
                    sEnvironment As String, Optional ByVal webServiceName As String = Nothing, _
                    Optional ByVal webServiceFunctionName As String = Nothing)
        Dim selectStmt As String = Config("/SQL/LOAD_BY_CODE")
        Dim objWebServiceParam As String
        If webServiceName IsNot Nothing Then
            objWebServiceParam = webServiceName.ToUpper()
        End If
        Dim objWebServiceFunctionParam As String
        If webServiceFunctionName IsNot Nothing Then
            objWebServiceFunctionParam = webServiceFunctionName.ToUpper()
        End If
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                        New DBHelper.DBHelperParameter(COL_NAME_WEB_SERVICE_NAME_1, objWebServiceParam), _
                                        New DBHelper.DBHelperParameter(COL_NAME_WEB_SERVICE_FUNCTION_NAME, objWebServiceFunctionParam), _
                                        New DBHelper.DBHelperParameter(COL_NAME_WEB_SERVICE_NAME_2, objWebServiceParam), _
                                        New DBHelper.DBHelperParameter(COL_NAME_HUB_REGION, sHubRegion.ToUpper), _
                                        New DBHelper.DBHelperParameter(COL_NAME_MACHINE_PREFIX, sMachinePrefix.ToUpper), _
                                        New DBHelper.DBHelperParameter(COL_NAME_ENVIRONMENT, sEnvironment.ToUpper)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    'Public Function LoadList() As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function

    Public Function LoadList(envStr As String, description As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        If FormatSearchMask(envStr) Then
            whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & COL_NAME_ENVIRONMENT & ") " & envStr.ToUpper
        End If


        If FormatSearchMask(description) Then
            If whereClauseConditions = "" Then
                whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & COL_NAME_DESCRIPTION & ") " & description.ToUpper
            Else
                whereClauseConditions &= environment.NewLine & "AND  UPPER(" & COL_NAME_DESCRIPTION & ") " & description.ToUpper
            End If
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet
        Try
            ds = DBHelper.Fetch(selectStmt, TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

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


#Region "Additional Logic"

    Public Function TestBatchServices(UserName As String, Password As String, Group As String) As String
        Dim selectStmt As String = Config("/SQL/TEST_BATCH")

        Try
            Return DBHelper.ExecuteScalar(String.Format(selectStmt, UserName, Password, Group)).ToString
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class



