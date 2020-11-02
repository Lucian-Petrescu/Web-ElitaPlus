﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (6/9/2015)********************


Public Class CertExtendedItemFormDal
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted
    Public Const TABLE_NAME As String = "ELP_CERT_EXT_ITEM_CONFIG_TEMP"
    Public Const TABLE_KEY_NAME As String = COL_NAME_CERT_EXTENDED_ITEM_ID

    Public Const COL_NAME_CERT_EXTENDED_ITEM_ID As String = "cert_ext_item_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_FIELD_NAME As String = "field_name"
    Public Const COL_NAME_IN_ENROLLMENT As String = "in_enrollment"
    Public Const COL_NAME_DEFAULT_VALUE As String = "default_value"
    Public Const COL_NAME_ALLOW_UPDATE As String = "allow_update"
    Public Const COL_NAME_TABLE_NAME As String = "ELP_CERT_EXT_ITEM_CONFIG_TEMP"

    Public Const PAR_I_NAME_CERT_EXTENDED_ITEM_ID As String = "pi_cert_ext_item_id"
    Public Const PAR_I_NAME_CODE As String = "pi_code"
    Public Const PAR_I_NAME_DESCRIPTION As String = "pi_description"
    Public Const PAR_I_NAME_FIELD_NAME As String = "pi_field_name"
    Public Const PAR_I_NAME_IN_ENROLLMENT As String = "pi_in_enrollment"
    Public Const PAR_I_NAME_DEFAULT_VALUE As String = "pi_default_value"
    Public Const PAR_I_NAME_ALLOW_UPDATE As String = "pi_allow_update"
    Public Const PAR_I_SORT As String = "pi_sort"
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
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD"))
                cmd.AddParameter(PAR_I_NAME_CERT_EXTENDED_ITEM_ID, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, familyDS)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Function LoadList(ByVal codeMask As String) As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_LIST"))
                cmd.AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, codeMask)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    'Public Function LoadList() As DataSet
    '    Try
    '        'Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_LIST"))
    '        '    cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
    '        '    Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME)
    '        'End Using
    '        Dim table As New DataTable(TABLE_NAME)

    '        With table
    '            ' Create four typed columns in the DataTable.
    '            .Columns.Add("cert_extended_item_id", GetType(Guid))
    '            .Columns.Add("code", GetType(String))
    '            .Columns.Add("description", GetType(String))
    '            .Columns.Add("field_name", GetType(String))
    '            .Columns.Add("in_enrollment", GetType(String))
    '            .Columns.Add("default_value", GetType(String))
    '            .Columns.Add("allow_update", GetType(String))
    '            ' Add 1 rows with those columns filled in the DataTable.
    '            '.Rows.Add(Guid.NewGuid, "FieldName1", "Y", "", "Y")
    '        End With
    '        Dim ds As New DataSet

    '        ds.Tables.Add(table)

    '        Return ds
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Function

    Public Function GetAvailableCompany() As DataView
        Dim selectStmt As String = Me.Config("/SQL/LOAD_COMPANY_LIST")
        Try
            Return DBHelper.Fetch(selectStmt, TABLE_NAME).Tables(0).DefaultView
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
        If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
        command.AddParameter(PAR_I_NAME_CERT_EXTENDED_ITEM_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CERT_EXTENDED_ITEM_ID)

    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CERT_EXTENDED_ITEM_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CERT_EXTENDED_ITEM_ID)
            .AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE.ToUpper().Trim())
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_FIELD_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_FIELD_NAME)
            .AddParameter(PAR_I_NAME_IN_ENROLLMENT, OracleDbType.Varchar2, sourceColumn:=COL_NAME_IN_ENROLLMENT)
            .AddParameter(PAR_I_NAME_DEFAULT_VALUE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DEFAULT_VALUE)
            .AddParameter(PAR_I_NAME_ALLOW_UPDATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ALLOW_UPDATE)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CERT_EXTENDED_ITEM_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CERT_EXTENDED_ITEM_ID)
            .AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE.ToUpper().Trim())
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_FIELD_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_FIELD_NAME)
            .AddParameter(PAR_I_NAME_IN_ENROLLMENT, OracleDbType.Varchar2, sourceColumn:=COL_NAME_IN_ENROLLMENT)
            .AddParameter(PAR_I_NAME_DEFAULT_VALUE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DEFAULT_VALUE)
            .AddParameter(PAR_I_NAME_ALLOW_UPDATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ALLOW_UPDATE)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
        End With

    End Sub
#End Region

#Region "Public Methods"

    Public Function LoadCertExtendedItemList(ByVal pTableName As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_CERT_EXTENDED_ITEM_LIST")

        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure)
        cmd.AddParameter(PAR_I_NAME_CERT_EXTENDED_ITEM_ID, OracleDbType.Varchar2, 30, pTableName)
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


