'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (6/9/2015)********************


Public Class CertExtendedItemDal
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted
    Public Const TABLE_NAME As String = "elp_crt_ext_cd_flds"
    Public Const TABLE_KEY_NAME As String = COL_NAME_CRT_EXT_FIELDS_CONFIG_ID

    Public Const COL_NAME_CRT_EXT_FIELDS_CONFIG_ID As String = "crt_ext_cd_flds_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_FIELD_NAME As String = "field_name"
    Public Const COL_NAME_DEFAULT_VALUE As String = "default_value"
    Public Const COL_NAME_ALLOW_UPDATE As String = "allow_update"
    Public Const COL_NAME_ALLOW_DISPLAY As String = "allow_display"
    Public Const COL_NAME_TABLE_NAME As String = "elp_crt_ext_cd_flds"

    Public Const PAR_I_NAME_CRT_EXT_FIELDS_CONFIG_ID As String = "pi_crt_ext_cd_flds_id"
    Public Const PAR_I_NAME_REFERENCE_ID As String = "pi_reference_id"
    Public Const PAR_I_NAME_REFERENCE As String = "pi_reference"
    Public Const PAR_I_NAME_CODE As String = "pi_code"
    Public Const PAR_I_NAME_DESCRIPTION As String = "pi_description"
    Public Const PAR_I_NAME_FIELD_NAME As String = "pi_field_name"
    Public Const PAR_I_NAME_DEFAULT_VALUE As String = "pi_default_value"
    Public Const PAR_I_NAME_ALLOW_UPDATE As String = "pi_allow_update"
    Public Const PAR_I_NAME_ALLOW_DISPLAY As String = "pi_allow_display"
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

    Public Sub Load(ByVal familyDs As DataSet, ByVal id As Guid)
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD"))
                cmd.AddParameter(PAR_I_NAME_CRT_EXT_FIELDS_CONFIG_ID, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, TABLE_NAME, familyDs)
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
                Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadSelectedCompaniesList(ByVal codeMask As String) As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_SELECTED_COMPANIES_LIST"))
                cmd.AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, codeMask)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadSelectedDealersList(ByVal codeMask As String) As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/LOAD_SELECTED_DEALERS_LIST"))
                cmd.AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, codeMask)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function GetAvailableCompany() As DataView
        Dim selectStmt As String = Me.Config("/SQL/LOAD_COMPANY_LIST")
        Try
            Return DBHelper.Fetch(selectStmt, TABLE_NAME).Tables(0).DefaultView
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
#Region "Description Save"
    Public Sub SaveDescription(ByVal code As String, ByVal description As String, ByVal modifiedBy As String)
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/SAVE_CODE_DESCRIPTION"))
                cmd.AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, code)
                cmd.AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, description)
                cmd.AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, modifiedBy)
                OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region
#Region "Dealer Company List"
    Public Sub SaveDealerCompanyList(ByVal code As String, ByVal reference As String, ByVal id As Guid, ByVal created_by As String)
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/SAVE_DEALER_COMPANY_LIST"))
                cmd.AddParameter(PAR_I_NAME_REFERENCE, OracleDbType.Varchar2, reference)
                cmd.AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, code)
                cmd.AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, created_by)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Sub DeleteDealerCompanyList(ByVal code As String, ByVal reference As String, ByVal id As Guid)
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/DELETE_DEALER_COMPANY_LIST"))
                cmd.AddParameter(PAR_I_NAME_REFERENCE, OracleDbType.Varchar2, reference)
                cmd.AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, code)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region
#Region "Dealer Company List Validation"
    Public Function DealerCompanyConfigExist(ByVal code As String, ByVal reference As String, ByVal id As Guid) As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/CRT_CONFIG_ATTACHED"))
                cmd.AddParameter(PAR_I_NAME_REFERENCE, OracleDbType.Varchar2, reference)
                cmd.AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, code)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Function FieldConfigExist(ByVal code As String, ByVal fieldName As String) As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/CRT_CONFIG_EXIST"))
                cmd.AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, code)
                cmd.AddParameter(PAR_I_NAME_FIELD_NAME, OracleDbType.Varchar2, fieldName)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Function FieldConfigExist(ByVal code As String, ByVal fieldName As String,ByVal reference As String, ByVal id As Guid) As DataSet
        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(Me.Config("/SQL/CRT_CONFIG_EXIST"))
                cmd.AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, code)
                cmd.AddParameter(PAR_I_NAME_FIELD_NAME, OracleDbType.Varchar2, fieldName)
                cmd.AddParameter(PAR_I_NAME_REFERENCE, OracleDbType.Varchar2, reference)
                cmd.AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Return OracleDbHelper.Fetch(cmd, TABLE_NAME)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
#Region "Overloaded Methods"
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal transaction As IDbTransaction = Nothing)

        Dim tr As IDbTransaction = transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            Update(familyDataset.GetChanges(), tr)

            If transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we should commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try

    End Sub
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = supportChangesFilter)
        If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (supportChangesFilter)) <> (supportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), transaction, changesFilter)
        End If
    End Sub
    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE.ToUpper().Trim())
            .AddParameter(PAR_I_NAME_FIELD_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_FIELD_NAME)
        End With
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CRT_EXT_FIELDS_CONFIG_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CRT_EXT_FIELDS_CONFIG_ID)
            .AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE.ToUpper().Trim())
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_FIELD_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_FIELD_NAME)
            .AddParameter(PAR_I_NAME_DEFAULT_VALUE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DEFAULT_VALUE)
            .AddParameter(PAR_I_NAME_ALLOW_UPDATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ALLOW_UPDATE)
            .AddParameter(PAR_I_NAME_ALLOW_DISPLAY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ALLOW_DISPLAY)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .AddParameter(PAR_I_NAME_CRT_EXT_FIELDS_CONFIG_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_CRT_EXT_FIELDS_CONFIG_ID)
            .AddParameter(PAR_I_NAME_CODE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CODE.ToUpper().Trim())
            .AddParameter(PAR_I_NAME_DESCRIPTION, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DESCRIPTION)
            .AddParameter(PAR_I_NAME_FIELD_NAME, OracleDbType.Varchar2, sourceColumn:=COL_NAME_FIELD_NAME)
            .AddParameter(PAR_I_NAME_DEFAULT_VALUE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_DEFAULT_VALUE)
            .AddParameter(PAR_I_NAME_ALLOW_UPDATE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ALLOW_UPDATE)
            .AddParameter(PAR_I_NAME_ALLOW_DISPLAY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ALLOW_DISPLAY)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
        End With

    End Sub
#End Region
End Class