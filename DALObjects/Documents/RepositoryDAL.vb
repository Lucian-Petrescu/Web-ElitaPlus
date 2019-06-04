'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (6/17/2015)********************

Public Class RepositoryDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified
    Public Const TABLE_NAME As String = "ELP_DOC_REPOSITORY"
    Public Const TABLE_KEY_NAME As String = COL_NAME_REPOSITORY_ID

    Public Const COL_NAME_REPOSITORY_ID As String = "repository_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_STORAGE_PATH As String = "storage_path"
    Public Const COL_NAME_REPOSITORY_TYPE_XCD As String = "repository_type_xcd"


    Public Const PAR_I_NAME_REPOSITORY_ID As String = "pi_repository_id"
    Public Const PAR_I_NAME_CODE As String = "pi_code"
    Public Const PAR_I_NAME_DESCRIPTION As String = "pi_description"
    Public Const PAR_I_NAME_STORAGE_PATH As String = "pi_storage_path"
    Public Const PAR_I_NAME_REPOSITORY_TYPE_XCD As String = "pi_repository_type_xcd"

    Public Const PAR_O_NAME_REPOSITORY_LIST As String = "po_RepositoryList"
    Public Const PAR_O_NAME_FILE_TYPE_LIST As String = "po_FileTypeList"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Function LoadList() As DataSet
        Try
            Dim ds As New DataSet
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_O_NAME_REPOSITORY_LIST, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                cmd.AddParameter(PAR_O_NAME_FILE_TYPE_LIST, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, New String() {RepositoryDAL.TABLE_NAME, FileTypeDAL.TABLE_NAME}, ds)
            End Using
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

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
        Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .AddParameter(PAR_I_NAME_REPOSITORY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REPOSITORY_ID)
            .AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_STORAGE_PATH, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STORAGE_PATH)
            .AddParameter(PAR_I_NAME_REPOSITORY_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REPOSITORY_TYPE_XCD)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .AddParameter(PAR_I_NAME_REPOSITORY_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REPOSITORY_ID)
            .AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_STORAGE_PATH, OracleDbType.Varchar2, sourceColumn:=COL_NAME_STORAGE_PATH)
            .AddParameter(PAR_I_NAME_REPOSITORY_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_REPOSITORY_TYPE_XCD)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
        End With

    End Sub
#End Region

End Class

