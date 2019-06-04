'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (7/4/2017)********************


Public Class CaseQuestionAnswerDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added  Or DataRowState.Modified 
    Public Const TABLE_NAME As String = "ELP_CASE_QUESTION_ANSWER"
    Public Const TABLE_KEY_NAME As String = COL_NAME_CASE_QUESTION_ANSWER_ID
	
    Public Const COL_NAME_CASE_QUESTION_ANSWER_ID As String = "case_question_answer_id"
    Public Const COL_NAME_CASE_QUESTION_SET_ID As String = "case_question_set_id"
    Public Const COL_NAME_DCM_ANSWER_ID As String = "dcm_answer_id"
    Public Const COL_NAME_ANSWER_TEXT As String = "answer_text"
    Public Const COL_NAME_ANSWER_DATE As String = "answer_date"
    Public Const COL_NAME_ANSWER_NUMBER As String = "answer_number"
    Public Const COL_NAME_INTERACTION_ID As String = "interaction_id"
    Public Const COL_NAME_DELETE_FLAG_XCD As String = "delete_flag_xcd"
    Public Const COL_NAME_DCM_QUESTION_ID As String = "dcm_question_id"
    
    Public Const PAR_I_NAME_CASE_QUESTION_ANSWER_ID As String = "pi_case_question_answer_id"
    Public Const PAR_I_NAME_CASE_QUESTION_SET_ID As String = "pi_case_question_set_id"
    Public Const PAR_I_NAME_DCM_ANSWER_ID As String = "pi_dcm_answer_id"
    Public Const PAR_I_NAME_ANSWER_TEXT As String = "pi_answer_text"
    Public Const PAR_I_NAME_ANSWER_DATE As String = "pi_answer_date"
    Public Const PAR_I_NAME_ANSWER_NUMBER As String = "pi_answer_number"
    Public Const PAR_I_NAME_INTERACTION_ID As String = "pi_interaction_id"
    Public Const PAR_I_NAME_DELETE_FLAG_XCD As String = "pi_delete_flag_xcd"
    Public Const PAR_I_NAME_DCM_QUESTION_ID As String = "pi_dcm_question_id"
    Public Const PO_CURSOR_CASE As Integer = 0
    Public Const SP_PARAM_NAME_CASE_LIST As String = "po_case_ques_ans_list"

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

    Public Function LoadList() As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
            End Using        
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadCaseQuestionAnswerList(ByVal CaseId As Guid, ByVal LanguageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_CASE_QUESTION_ANSWER_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(Me.PO_CURSOR_CASE) As DBHelper.DBHelperParameter
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_case_id", CaseId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_language_id", LanguageId.ToByteArray)
        inParameters.Add(param)

        outputParameter(Me.PO_CURSOR_CASE) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME_CASE_LIST, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetCaseQuestionAnswerList")
            ds.Tables(0).TableName = "GetCaseQuestionAnswerList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadClaimCaseQuestionAnswerList(ByVal ClaimId As Guid, ByVal LanguageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_CLAIM_CASE_QUESTION_ANSWER_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(Me.PO_CURSOR_CASE) As DBHelper.DBHelperParameter
        Dim inParameters As New Generic.List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_claim_id", ClaimId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_language_id", LanguageId.ToByteArray)
        inParameters.Add(param)

        outputParameter(Me.PO_CURSOR_CASE) = New DBHelper.DBHelperParameter(Me.SP_PARAM_NAME_CASE_LIST, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetClaimCaseQuestionAnswerList")
            ds.Tables(0).TableName = "GetClaimCaseQuestionAnswerList"
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
            .AddParameter(PAR_I_NAME_CASE_QUESTION_ANSWER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_QUESTION_ANSWER_ID)
            .AddParameter(PAR_I_NAME_CASE_QUESTION_SET_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_QUESTION_SET_ID)
            .AddParameter(PAR_I_NAME_DCM_ANSWER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DCM_ANSWER_ID)
            .AddParameter(PAR_I_NAME_ANSWER_TEXT, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ANSWER_TEXT)
            .AddParameter(PAR_I_NAME_ANSWER_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_ANSWER_DATE)
            .AddParameter(PAR_I_NAME_ANSWER_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_ANSWER_NUMBER)
            .AddParameter(PAR_I_NAME_INTERACTION_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INTERACTION_ID)
            .AddParameter(PAR_I_NAME_DELETE_FLAG_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DELETE_FLAG_XCD)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
            .AddParameter(PAR_I_NAME_DCM_QUESTION_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DCM_QUESTION_ID)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CASE_QUESTION_ANSWER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_QUESTION_ANSWER_ID)
            .AddParameter(PAR_I_NAME_CASE_QUESTION_SET_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CASE_QUESTION_SET_ID)
            .AddParameter(PAR_I_NAME_DCM_ANSWER_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DCM_ANSWER_ID)
            .AddParameter(PAR_I_NAME_ANSWER_TEXT, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ANSWER_TEXT)
            .AddParameter(PAR_I_NAME_ANSWER_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_ANSWER_DATE)
            .AddParameter(PAR_I_NAME_ANSWER_NUMBER, OracleDbType.Decimal, sourceColumn:=COL_NAME_ANSWER_NUMBER)
            .AddParameter(PAR_I_NAME_INTERACTION_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_INTERACTION_ID)
            .AddParameter(PAR_I_NAME_DELETE_FLAG_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DELETE_FLAG_XCD)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
            .AddParameter(PAR_I_NAME_DCM_QUESTION_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DCM_QUESTION_ID)
        End With

    End Sub
#End Region

End Class


