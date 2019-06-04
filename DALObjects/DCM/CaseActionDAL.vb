'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (7/4/2017)********************


Public Class CaseActionDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added  Or DataRowState.Modified 
    Public Const TABLE_NAME As String = "ELP_CASE_ACTION"
    Public Const TABLE_KEY_NAME As String = COL_NAME_CASE_ACTION_ID
	
    Public Const COL_NAME_CASE_ACTION_ID As String = "case_action_id"
    Public Const COL_NAME_CASE_ID As String = "case_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_ACTION_OWNER_XCD As String = "action_owner_xcd"
    Public Const COL_NAME_ACTION_TYPE_XCD As String = "action_type_xcd"
    Public Const COL_NAME_ACTION_OWNER As String = "action_owner"
    Public Const COL_NAME_ACTION_TYPE As String = "action_type"
    Public Const COL_NAME_STATUS_XCD As String = "status_xcd"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_REF_SOURCE As String = "ref_source"
    Public Const COL_NAME_REF_ID As String = "ref_id"
    
    Public Const PAR_I_NAME_CASE_ACTION_ID As String = "pi_case_action_id"
    Public Const PAR_I_NAME_CASE_ID As String = "pi_case_id"
    Public Const PAR_I_NAME_CLAIM_ID As String = "pi_claim_id"
    Public Const PAR_I_NAME_ACTION_OWNER_XCD As String = "pi_action_owner_xcd"
    Public Const PAR_I_NAME_ACTION_TYPE_XCD As String = "pi_action_type_xcd"
    Public Const PAR_I_NAME_STATUS_XCD As String = "pi_status_xcd"
    Public Const PAR_I_NAME_REF_SOURCE As String = "pi_ref_source"
    Public Const PAR_I_NAME_REF_ID As String = "pi_ref_id"

    Public Const PO_CURSOR_CASE As Integer = 0
    Public Const SP_PARAM_NAME_CASE_ACTION_LIST As String = "po_case_action_list"
    Public Const SP_PARAM_NAME_CLAIM_ACTION_LIST As String = "po_claim_action_list"

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
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD"))
                cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, familyDS)
            End Using        
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadCaseActionList(ByVal CaseId As Guid, ByVal LanguageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_CASE_ACTION_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(Me.PO_CURSOR_CASE) As DBHelper.DBHelperParameter
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_case_id", CaseId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_language_id", LanguageId.ToByteArray)
        inParameters.Add(param)

        outputParameter(Me.PO_CURSOR_CASE) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME_CASE_ACTION_LIST, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetCaseActionList")
            ds.Tables(0).TableName = "GetCaseActionList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadClaimActionList(ByVal ClaimId As Guid, ByVal LanguageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_CLAIM_ACTION_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(Me.PO_CURSOR_CASE) As DBHelper.DBHelperParameter
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_claim_id", ClaimId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_language_id", LanguageId.ToByteArray)
        inParameters.Add(param)

        outputParameter(Me.PO_CURSOR_CASE) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME_CLAIM_ACTION_LIST, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetClaimActionList")
            ds.Tables(0).TableName = "GetClaimActionList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
		If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CASE_ACTION_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_ACTION_ID)
            .AddParameter(PAR_I_NAME_CASE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_ID)
            .AddParameter(PAR_I_NAME_CLAIM_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CLAIM_ID)
            .AddParameter(PAR_I_NAME_ACTION_OWNER_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACTION_OWNER_XCD)
            .AddParameter(PAR_I_NAME_ACTION_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACTION_TYPE_XCD)
            .AddParameter(PAR_I_NAME_STATUS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STATUS_XCD)
            .AddParameter(PAR_I_NAME_REF_SOURCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REF_SOURCE)
            .AddParameter(PAR_I_NAME_REF_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REF_ID)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CASE_ACTION_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_ACTION_ID)
            .AddParameter(PAR_I_NAME_CASE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_ID)
            .AddParameter(PAR_I_NAME_CLAIM_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CLAIM_ID)
            .AddParameter(PAR_I_NAME_ACTION_OWNER_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACTION_OWNER_XCD)
            .AddParameter(PAR_I_NAME_ACTION_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ACTION_TYPE_XCD)
            .AddParameter(PAR_I_NAME_STATUS_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STATUS_XCD)
            .AddParameter(PAR_I_NAME_REF_SOURCE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REF_SOURCE)
            .AddParameter(PAR_I_NAME_REF_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REF_ID)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
        End With

    End Sub
#End Region

End Class


