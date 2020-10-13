'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (6/9/2015)********************


Public Class AttributeDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted
    Public Const TABLE_NAME As String = "ELP_ATTRIBUTE"
    Public Const TABLE_KEY_NAME As String = COL_NAME_ATTRIBUTE_ID

    Public Const COL_NAME_ATTRIBUTE_ID As String = "attribute_id"
    Public Const COL_NAME_DATA_TYPE_ID As String = "data_type_id"
    Public Const COL_NAME_UI_PROG_CODE As String = "ui_prog_code"
    Public Const COL_NAME_TABLE_NAME As String = "table_name"
    Public Const COL_NAME_ALLOW_DUPLICATES As String = "allow_duplicates"
    Public Const COL_NAME_USE_EFFECTIVE_DATE As String = "use_effective_date"

    Public Const PAR_I_NAME_ATTRIBUTE_ID As String = "pi_attribute_id"
    Public Const PAR_I_NAME_DATA_TYPE_ID As String = "pi_data_type_id"
    Public Const PAR_I_NAME_UI_PROG_CODE As String = "pi_ui_prog_code"
    Public Const PAR_I_NAME_TABLE_NAME As String = "pi_table_name"
    Public Const PAR_I_NAME_ALLOW_DUPLICATES As String = "pi_allow_duplicates"
    Public Const PAR_I_NAME_USE_EFFECTIVE_DATE As String = "pi_use_effective_date"
    Public Const PAR_I_SORT As String = "pi_sort"
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
                cmd.AddParameter(PAR_I_NAME_ATTRIBUTE_ID, OracleDbType.Raw, id.ToByteArray())
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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        command.AddParameter(PAR_I_NAME_ATTRIBUTE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_ATTRIBUTE_ID)

    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_ATTRIBUTE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_ATTRIBUTE_ID)
            .AddParameter(PAR_I_NAME_DATA_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DATA_TYPE_ID)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_UI_PROG_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_UI_PROG_CODE)
            .AddParameter(PAR_I_NAME_TABLE_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_TABLE_NAME)
            .AddParameter(PAR_I_NAME_ALLOW_DUPLICATES, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ALLOW_DUPLICATES)
            .AddParameter(PAR_I_NAME_USE_EFFECTIVE_DATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_USE_EFFECTIVE_DATE)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_ATTRIBUTE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_ATTRIBUTE_ID)
            .AddParameter(PAR_I_NAME_DATA_TYPE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_DATA_TYPE_ID)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_UI_PROG_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_UI_PROG_CODE)
            .AddParameter(PAR_I_NAME_TABLE_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_TABLE_NAME)
            .AddParameter(PAR_I_NAME_ALLOW_DUPLICATES, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ALLOW_DUPLICATES)
            .AddParameter(PAR_I_NAME_USE_EFFECTIVE_DATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_USE_EFFECTIVE_DATE)
        End With

    End Sub
#End Region

#Region "Public Methods"

    Public Function LoadAttributeList(pTableName As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_ATTRIBUTE_LIST")

        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure)
        cmd.AddParameter(PAR_I_NAME_TABLE_NAME, OracleDbType.Varchar2, 30, pTableName)
        cmd.AddParameter(OracleDALBase.PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            Dim ds As New DataSet
            OracleDbHelper.Fetch(cmd, TABLE_NAME, ds)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadTableList(pTableName As String, pSortExpression As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_TABLES")
        Dim sort As Integer
        If (pSortExpression Is Nothing) Then
            sort = 1 '' Default Sort Order TABLE_NAME ASC
        Else
            If (pSortExpression.ToUpperInvariant().Contains("ATTRIBUTE_COUNT")) Then
                sort = 2
            Else
                sort = 1
            End If

            If (pSortExpression.ToUpperInvariant().Contains("DESC")) Then
                sort = sort * -1
            End If
        End If

        If (Not pTableName.Contains("%")) Then
            pTableName = "%" + pTableName + "%"
        End If

        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure)
        cmd.AddParameter(PAR_I_NAME_TABLE_NAME, OracleDbType.Varchar2, 30, pTableName)
        cmd.AddParameter(PAR_I_SORT, OracleDbType.Int32, 2, sort)
        cmd.AddParameter(OracleDALBase.PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            Dim ds As New DataSet
            OracleDbHelper.Fetch(cmd, TABLE_NAME, ds)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

End Class


