'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (6/17/2015)********************

Public Class FileTypeDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified
    Public Const TABLE_NAME As String = "ELP_DOC_FILE_TYPE"
    Public Const TABLE_KEY_NAME As String = COL_NAME_FILE_TYPE_ID

    Public Const COL_NAME_FILE_TYPE_ID As String = "file_type_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_EXTENSION As String = "extension"
    Public Const COL_NAME_MIME_TYPE As String = "mime_type"

    Public Const PAR_I_NAME_FILE_TYPE_ID As String = "pi_file_type_id"
    Public Const PAR_I_NAME_CODE As String = "pi_code"
    Public Const PAR_I_NAME_DESCRIPTION As String = "pi_description"
    Public Const PAR_I_NAME_EXTENSION As String = "pi_extension"
    Public Const PAR_I_NAME_MIME_TYPE As String = "pi_mime_type"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

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
            .AddParameter(PAR_I_NAME_FILE_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_FILE_TYPE_ID)
            .AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_EXTENSION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_EXTENSION)
            .AddParameter(PAR_I_NAME_MIME_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MIME_TYPE)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_FILE_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_FILE_TYPE_ID)
            .AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE)
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_EXTENSION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_EXTENSION)
            .AddParameter(PAR_I_NAME_MIME_TYPE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MIME_TYPE)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
        End With

    End Sub
#End Region

End Class
