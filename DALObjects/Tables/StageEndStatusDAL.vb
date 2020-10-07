Public Class StageEndStatusDAL
    Inherits OracleDALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_STAGE_END"
    Public Const TABLE_KEY_NAME As String = "stage_end_id"

    Public Const COL_NAME_STAGE_END_ID As String = "stage_end_id"
    Public Const COL_NAME_STAGE_ID As String = "stage_id"
    Public Const COL_NAME_END_STATUS_ID As String = "end_status_id"
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub
#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.Text, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "stage_end_id", OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input)

        Try
            OracleDbHelper.Fetch(cmd, TABLE_NAME, familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(familyDS As DataSet, stage_id As Guid, language_id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.Text, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "stage_id", OracleDbType.Raw, stage_id.ToByteArray, ParameterDirection.Input)
        OracleDbHelper.AddParameter(cmd, "language_id", OracleDbType.Raw, language_id.ToByteArray, ParameterDirection.Input)

        Try
            OracleDbHelper.Fetch(cmd, TABLE_NAME, familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function GetSelectedStageEndStatus(familyDS As DataSet, stage_id As Guid, language_id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_SELECTED_STAGE_END_STATUS")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.Text, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "stage_id", OracleDbType.Raw, stage_id.ToByteArray, ParameterDirection.Input)
        OracleDbHelper.AddParameter(cmd, "language_id", OracleDbType.Raw, language_id.ToByteArray, ParameterDirection.Input)

        Try
            Dim ds As DataSet = OracleDbHelper.Fetch(cmd, "SELSTGENDSTATUSLST")

            If ds.Tables.Count > 0 Then
                familyDS.Tables.Add(ds.Tables(0))
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView()
            End If

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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter("pi_stage_end_id", OracleDbType.Raw, sourceColumn:=COL_NAME_STAGE_END_ID)
        End With
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter("pi_stage_end_id", OracleDbType.Raw, sourceColumn:=COL_NAME_STAGE_END_ID)
            .AddParameter("pi_stage_id", OracleDbType.Raw, sourceColumn:=COL_NAME_STAGE_ID)
            .AddParameter("pi_end_status_id", OracleDbType.Raw, sourceColumn:=COL_NAME_END_STATUS_ID)
            .AddParameter("pi_created_by", OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
        End With
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter("pi_stage_end_id", OracleDbType.Raw, sourceColumn:=COL_NAME_STAGE_END_ID)
            .AddParameter("pi_stage_id", OracleDbType.Raw, sourceColumn:=COL_NAME_STAGE_ID)
            .AddParameter("pi_end_status_id", OracleDbType.Raw, sourceColumn:=COL_NAME_END_STATUS_ID)
            .AddParameter("pi_modified_by", OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
        End With
    End Sub

#End Region

End Class