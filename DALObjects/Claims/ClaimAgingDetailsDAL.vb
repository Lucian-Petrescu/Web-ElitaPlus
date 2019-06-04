
Public Class ClaimAgingDetailsDAL
    Inherits DALBase

#Region "Constants"

    Public Const TABLE_NAME As String = "AGING_DETAILS"
    Public Const TABLE_KEY_NAME As String = "claim_stage_id"

    Public Const COL_NAME_CLAIM_STAGE_ID As String = "claim_stage_id"
    Public Const COL_NAME_STAGE_NAME As String = "stage_name"
    Public Const COL_NAME_STAGE_NAME_ID As String = "stage_name_Id"
    Public Const COL_NAME_AGING_START_STATUS As String = "aging_start_status"
    Public Const COL_NAME_AGING_START_STATUS_ID As String = "aging_start_status_id"
    Public Const COL_NAME_AGING_START_DATETIME As String = "aging_start_datetime"
    Public Const COL_NAME_AGING_END_STATUS As String = "aging_end_status"
    Public Const COL_NAME_AGING_END_STATUS_ID As String = "aging_end_status_id"
    Public Const COL_NAME_AGING_END_DATETIME As String = "aging_end_datetime"
    Public Const COL_NAME_AGING_DAYS As String = "aging_days"
    Public Const COL_NAME_AGING_HOURS As String = "aging_hours"
    Public Const COL_NAME_AGING_SINCE_CLAIM_DAYS As String = "aging_since_claim_days"
    Public Const COL_NAME_AGING_SINCE_CLAIM_HOURS As String = "aging_since_claim_hours"

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "pi_claim_stage_id", OracleDbType.Raw, id.ToByteArray)
        OracleDbHelper.AddParameter(cmd, "po_resultcursor", OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal claim_id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "pi_claim_id", OracleDbType.Raw, claim_id.ToByteArray)
        OracleDbHelper.AddParameter(cmd, "po_resultcursor", OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(ByVal claim_id As Guid, ByVal language_id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "pi_claim_id", OracleDbType.Raw, claim_id.ToByteArray)
        OracleDbHelper.AddParameter(cmd, "pi_language_id", OracleDbType.Raw, language_id.ToByteArray)
        OracleDbHelper.AddParameter(cmd, "po_resultcursor", OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub LoadList(ByVal familyDS As DataSet, ByVal claim_id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "pi_claim_id", OracleDbType.Raw, claim_id.ToByteArray)
        OracleDbHelper.AddParameter(cmd, "po_resultcursor", OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(ByVal familyDS As DataSet, ByVal claim_id As Guid, ByVal language_id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "pi_claim_id", OracleDbType.Raw, claim_id.ToByteArray)
        OracleDbHelper.AddParameter(cmd, "pi_language_id", OracleDbType.Raw, language_id.ToByteArray)
        OracleDbHelper.AddParameter(cmd, "po_resultcursor", OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

End Class
