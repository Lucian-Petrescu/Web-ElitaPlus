'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (10/11/2017)********************
Imports System.Collections.Generic

Public Class ConsentActionsDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified
    Public Const TABLE_NAME As String = "ELP_CONSENT_VALUE"
    Public Const TABLE_KEY_NAME As String = COL_NAME_CONSENT_VALUE_ID

    Public Const COL_NAME_CONSENT_VALUE_ID As String = "consent_value_id"
    Public Const COL_NAME_CONSENT_TYPE_XCD As String = "consent_type_xcd"
    Public Const COL_NAME_CONSENT_FIELD_NAME_XCD As String = "consent_field_name_xcd"
    Public Const COL_FIELD_VALUE As String = "field_value"
    Public Const COL_NAME_ACTION_XCD As String = "action_xcd"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"

    Public Const PAR_I_NAME_REFERENCE_TYPE As String = "pi_reference_type"
    Public Const PAR_I_NAME_REFERENCE_ID As String = "pi_reference_id"
    Public Const PAR_I_NAME_CONSENT_TYPE_XCD As String = "pi_consent_type_xcd"
    Public Const PAR_I_NAME_CONSENT_FIELD_NAME_XCD As String = "pi_consent_field_name_xcd"
    Public Const PAR_I_NAME_LANGUAGE_ID As String = "pi_language_id"

    Public Const PO_CURSOR_CONSENT_ACTIONS As Integer = 0
    Public Const SP_PARAM_NAME_CONSENT_ACTIONS_LIST As String = "po_consent_actions_list"

#End Region

#Region "Constructors"

#End Region

#Region "Load Methods"

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim cmd As OracleCommand = CreateCommand(selectStmt, CommandType.StoredProcedure, CreateConnection())

        cmd.BindByName = True
        cmd.AddParameter("pi_consent_value_id", OracleDbType.Raw, id.ToByteArray())
        cmd.AddParameter("po_resultcursor", OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            Fetch(cmd, TABLE_NAME, familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadConsentActionsList(ReferenceType As String, ReferenceValueId As Guid, ConsentType As String, ConsentFieldName As String, LanguageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PO_CURSOR_CONSENT_ACTIONS) As DBHelper.DBHelperParameter
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter(PAR_I_NAME_REFERENCE_TYPE, ReferenceType)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter(PAR_I_NAME_REFERENCE_ID, ReferenceValueId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter(PAR_I_NAME_CONSENT_TYPE_XCD, ConsentType)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter(PAR_I_NAME_CONSENT_FIELD_NAME_XCD, ConsentFieldName)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter(PAR_I_NAME_LANGUAGE_ID, LanguageId.ToByteArray)
        inParameters.Add(param)

        outputParameter(PO_CURSOR_CONSENT_ACTIONS) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_CONSENT_ACTIONS_LIST, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetConsentActionsList")
            ds.Tables(0).TableName = "GetConsentActionsList"
            Return ds
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
        With command
            .AddParameter("pi_consent_value_id", OracleDbType.Raw, sourceColumn:=COL_NAME_CONSENT_VALUE_ID, direction:=ParameterDirection.Input)
        End With
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub
#End Region

End Class
