'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (6/17/2015)********************

Public Class DocumentDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added
    Public Const TABLE_NAME As String = "ELP_DOC_DOCUMENT"
    Public Const TABLE_KEY_NAME As String = COL_NAME_DOCUMENT_ID

    Public Const COL_NAME_DOCUMENT_ID As String = "document_id"
    Public Const COL_NAME_REPOSITORY_CODE As String = "repository_code"
    Public Const COL_NAME_FILE_TYPE As String = "file_type"
    Public Const COL_NAME_FILE_NAME As String = "file_name"
    Public Const COL_NAME_FILE_SIZE_BYTES As String = "file_size_bytes"
    Public Const COL_NAME_HASH_VALUE As String = "hash_value"

    Public Const PAR_I_NAME_DOCUMENT_ID As String = "pi_document_id"
    Public Const PAR_I_NAME_REPOSITORY_CODE As String = "pi_repository_code"
    Public Const PAR_I_NAME_FILE_TYPE As String = "pi_file_type"
    Public Const PAR_I_NAME_FILE_NAME As String = "pi_file_name"
    Public Const PAR_I_NAME_FILE_SIZE_BYTES As String = "pi_file_size_bytes"
    Public Const PAR_I_NAME_HASH_VALUE As String = "pi_hash_value"

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

    Public Function LoadList() As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_DOCUMENT_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DOCUMENT_ID)
            .AddParameter(PAR_I_NAME_REPOSITORY_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REPOSITORY_CODE)
            .AddParameter(PAR_I_NAME_FILE_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_FILE_TYPE)
            .AddParameter(PAR_I_NAME_FILE_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_FILE_NAME)
            .AddParameter(PAR_I_NAME_FILE_SIZE_BYTES, OracleDbType.Decimal, sourceColumn:=COL_NAME_FILE_SIZE_BYTES)
            .AddParameter(PAR_I_NAME_HASH_VALUE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_HASH_VALUE)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub
#End Region

End Class

