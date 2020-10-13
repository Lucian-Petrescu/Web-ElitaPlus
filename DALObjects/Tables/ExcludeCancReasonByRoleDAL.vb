''************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (3/13/2014)********************


'Public Class ExcludeCancReasonByRoleDAL
'    Inherits OracleDALBase

'#Region "Constants"
'    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted
'    Public Const TABLE_NAME As String = "ELP_EXCLUDE_CANCREASON_BY_ROLE"
'    Public Const TABLE_KEY_NAME As String = COL_NAME_EXCLUDE_CANCREASON_ROLE_ID

'    Public Const COL_NAME_EXCLUDE_CANCREASON_ROLE_ID As String = "exclude_cancreason_role_id"
'    Public Const COL_NAME_CANCELLATION_REASON_ID As String = "cancellation_reason_id"
'    Public Const COL_NAME_ROLE_ID As String = "role_id"

'    Public Const PAR_I_NAME_EXCLUDE_CANCREASON_ROLE_ID As String = "pi_exclude_cancreason_role_id"
'    Public Const PAR_I_NAME_CANCELLATION_REASON_ID As String = "pi_cancellation_reason_id"
'    Public Const PAR_I_NAME_ROLE_ID As String = "pi_role_id"
'    Public Const PAR_IO_NAME_EXCLUDE_CANCREASON_ROLE_ID As String = "pio_exclude_cancreason_role_id"

'#End Region

'#Region "Constructors"
'    Public Sub New()
'        MyBase.new()
'    End Sub

'#End Region

'#Region "Load Methods"

'    Public Sub LoadSchema(ByVal ds As DataSet)
'        Load(ds, Guid.Empty)
'    End Sub

'    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
'        Try
'            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD"))
'                cmd.AddParameter(TABLE_KEY_NAME, OracleDbType.Raw, id.ToByteArray())
'                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
'                OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, familyDS)
'            End Using
'        Catch ex As Exception
'            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
'        End Try
'    End Sub

'    Public Function LoadList() As DataSet
'        Try
'            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_LIST"))
'                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
'                Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
'            End Using
'        Catch ex As Exception
'            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
'        End Try
'    End Function

'    Public Function LoadList(ByVal ds As DataSet, ByVal CancellationReasonId As Guid) As DataSet
'        Try
'            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_LIST_FOR_CANC_REASON"))
'                cmd.AddParameter(PAR_I_NAME_CANCELLATION_REASON_ID, OracleDbType.Raw, CancellationReasonId.ToByteArray())
'                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
'                Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, ds)
'            End Using
'        Catch ex As Exception
'            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
'        End Try
'    End Function


'#End Region

'#Region "Overloaded Methods"
'    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
'        If ds Is Nothing Then
'            Return
'        End If
'        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
'            Throw New NotSupportedException()
'        End If
'        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
'            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
'        End If
'    End Sub

'    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
'        command.AddParameter(PAR_I_NAME_EXCLUDE_CANCREASON_ROLE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_EXCLUDE_CANCREASON_ROLE_ID)

'    End Sub

'    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
'        With command
'            .AddParameter(PAR_IO_NAME_EXCLUDE_CANCREASON_ROLE_ID, OracleDbType.Raw, direction:=ParameterDirection.InputOutput, sourceColumn:=COL_NAME_EXCLUDE_CANCREASON_ROLE_ID)
'            .AddParameter(PAR_I_NAME_CANCELLATION_REASON_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CANCELLATION_REASON_ID)
'            .AddParameter(PAR_I_NAME_ROLE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_ROLE_ID)
'            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
'            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
'        End With

'    End Sub

'    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)
'        With command
'            .AddParameter(PAR_I_NAME_EXCLUDE_CANCREASON_ROLE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_EXCLUDE_CANCREASON_ROLE_ID)
'            .AddParameter(PAR_I_NAME_CANCELLATION_REASON_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CANCELLATION_REASON_ID)
'            .AddParameter(PAR_I_NAME_ROLE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_ROLE_ID)
'            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
'            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
'        End With

'    End Sub
'#End Region

'End Class




'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/2/2014)********************


Public Class ExcludeCancReasonByRoleDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_EXCLUDE_CANCREASON_BY_ROLE"
    Public Const TABLE_KEY_NAME As String = "exclude_cancreason_role_id"

    Public Const COL_NAME_EXCLUDE_CANCREASON_ROLE_ID As String = "exclude_cancreason_role_id"
    Public Const COL_NAME_CANCELLATION_REASON_ID As String = "cancellation_reason_id"
    Public Const COL_NAME_ROLE_ID As String = "role_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("exclude_cancreason_role_id", id.ToByteArray)}
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
    Public Function LoadList(familyDs As DataSet, CancellationReasonId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST_FOR_CANC_REASON")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cancellation_reason_id", CancellationReasonId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDs, selectStmt, TABLE_NAME, parameters)

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


End Class





