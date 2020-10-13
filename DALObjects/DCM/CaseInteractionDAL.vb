'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (7/1/2017)********************


Public Class CaseInteractionDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added  Or DataRowState.Modified 
    Public Const TABLE_NAME As String = "ELP_CASE_INTERACTION"
    Public Const TABLE_KEY_NAME As String = COL_NAME_CASE_INTERACTION_ID
	
    Public Const COL_NAME_CASE_INTERACTION_ID As String = "case_interaction_id"
    Public Const COL_NAME_CASE_ID As String = "case_id"
    Public Const COL_NAME_INTERACTION_NUMBER As String = "interaction_number"
    Public Const COL_NAME_INTERACTION_PURPOSE_XCD As String = "interaction_purpose_xcd"
    Public Const COL_NAME_CHANNEL_XCD As String = "channel_xcd"
    Public Const COL_NAME_CALLER_ID As String = "caller_id"
    Public Const COL_NAME_INTERACTION_DATE As String = "interaction_date"
    Public Const COL_NAME_IS_CALLER_AUTHENTICATED_XCD As String = "is_caller_authenticated_xcd"
    Public Const COL_NAME_CALLER_AUTHENCTION_METHOD_XCD As String = "caller_authenction_method_xcd"
    Public Const COL_NAME_CULTURE_CODE As String = "culture_code"
    
    Public Const PAR_I_NAME_CASE_INTERACTION_ID As String = "pi_case_interaction_id"
    Public Const PAR_I_NAME_CASE_ID As String = "pi_case_id"
    Public Const PAR_I_NAME_INTERACTION_NUMBER As String = "pi_interaction_number"
    Public Const PAR_I_NAME_INTERACTION_PURPOSE_XCD As String = "pi_interaction_purpose_xcd"
    Public Const PAR_I_NAME_CHANNEL_XCD As String = "pi_channel_xcd"
    Public Const PAR_I_NAME_CALLER_ID As String = "pi_caller_id"
    Public Const PAR_I_NAME_INTERACTION_DATE As String = "pi_interaction_date"
    Public Const PAR_I_NAME_IS_CALLER_AUTHENTICATED_XCD As String = "pi_is_caller_authenticated_xcd"
    Public Const PAR_I_NAME_CALLER_AUTHENCTION_METHOD_XCD As String = "pi_caller_authenction_method_xcd"
    Public Const PAR_I_NAME_CULTURE_CODE As String = "pi_culture_code"

    Public Const PO_CURSOR_CASE As Integer = 0
    Public Const SP_PARAM_NAME_CASE_LIST As String = "po_case_interaction_list"

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
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Config("/SQL/LOAD"))
                cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, TABLE_NAME, familyDS)
            End Using        
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadCaseInteractionList(CaseId As Guid, LanguageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_CASE_INTERACTION_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PO_CURSOR_CASE) As DBHelper.DBHelperParameter
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_case_id", CaseId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_language_id", LanguageId.ToByteArray)
        inParameters.Add(param)

        outputParameter(PO_CURSOR_CASE) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_CASE_LIST, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetCaseInteractionList")
            ds.Tables(0).TableName = "GetCaseInteractionList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
		If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CASE_INTERACTION_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_INTERACTION_ID)
            .AddParameter(PAR_I_NAME_CASE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_ID)
            .AddParameter(PAR_I_NAME_INTERACTION_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_INTERACTION_NUMBER)
            .AddParameter(PAR_I_NAME_INTERACTION_PURPOSE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INTERACTION_PURPOSE_XCD)
            .AddParameter(PAR_I_NAME_CHANNEL_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CHANNEL_XCD)
            .AddParameter(PAR_I_NAME_CALLER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CALLER_ID)
            .AddParameter(PAR_I_NAME_INTERACTION_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INTERACTION_DATE)
            .AddParameter(PAR_I_NAME_IS_CALLER_AUTHENTICATED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_IS_CALLER_AUTHENTICATED_XCD)
            .AddParameter(PAR_I_NAME_CALLER_AUTHENCTION_METHOD_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CALLER_AUTHENCTION_METHOD_XCD)
            .AddParameter(PAR_I_NAME_CULTURE_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CULTURE_CODE)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CASE_INTERACTION_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_INTERACTION_ID)
            .AddParameter(PAR_I_NAME_CASE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_ID)
            .AddParameter(PAR_I_NAME_INTERACTION_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_INTERACTION_NUMBER)
            .AddParameter(PAR_I_NAME_INTERACTION_PURPOSE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_INTERACTION_PURPOSE_XCD)
            .AddParameter(PAR_I_NAME_CHANNEL_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CHANNEL_XCD)
            .AddParameter(PAR_I_NAME_CALLER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CALLER_ID)
            .AddParameter(PAR_I_NAME_INTERACTION_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_INTERACTION_DATE)
            .AddParameter(PAR_I_NAME_IS_CALLER_AUTHENTICATED_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_IS_CALLER_AUTHENTICATED_XCD)
            .AddParameter(PAR_I_NAME_CALLER_AUTHENCTION_METHOD_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CALLER_AUTHENCTION_METHOD_XCD)
            .AddParameter(PAR_I_NAME_CULTURE_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CULTURE_CODE)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
        End With

    End Sub
#End Region

End Class


