'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (6/9/2015)********************


Public Class AttributeValueDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted
    Public Const TABLE_NAME As String = "ELP_ATTRIBUTE_VALUE"
    Public Const TABLE_KEY_NAME As String = "attribute_value_id"

    Public Const COL_NAME_ATTRIBUTE_VALUE_ID As String = "attribute_value_id"
    Public Const COL_NAME_ATTRIBUTE_ID As String = "attribute_id"
    Public Const COL_NAME_ATTRIBUTE_VALUE As String = "attribute_value"
    Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_DELETE_FLAG As String = "delete_flag"

    Public Const PAR_I_NAME_ATTRIBUTE_VALUE_ID As String = "pi_attribute_value_id"
    Public Const PAR_I_NAME_ATTRIBUTE_ID As String = "pi_attribute_id"
    Public Const PAR_I_NAME_ATTRIBUTE_VALUE As String = "pi_attribute_value"
    Public Const PAR_I_NAME_REFERENCE_ID As String = "pi_reference_id"
    Public Const PAR_I_NAME_EFFECTIVE_DATE As String = "pi_effective_date"
    Public Const PAR_I_NAME_EXPIRATION_DATE As String = "pi_expiration_date"
    Public Const PAR_I_NAME_DELETE_FLAG As String = "pi_delete_flag"
    Public Const PAR_O_NAME_ATTRIBUTE_VALUES_CURSOR As String = "po_AttributeValuesCursor"
    Public Const PAR_O_NAME_ATTRIBUTES_CURSOR As String = "po_AttributesCursor"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(TABLE_KEY_NAME, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Function LoadList(pParentTableName As String, pReferenceId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet

        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure)
        cmd.AddParameter(PAR_O_NAME_ATTRIBUTE_VALUES_CURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
        cmd.AddParameter(PAR_O_NAME_ATTRIBUTES_CURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
        cmd.AddParameter(AttributeDAL.PAR_I_NAME_TABLE_NAME, OracleDbType.Varchar2, 20, pParentTableName.ToUpper())
        cmd.AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, pReferenceId.ToByteArray())

        Try
            OracleDbHelper.Fetch(cmd, New String() {AttributeValueDAL.TABLE_NAME, AttributeDAL.TABLE_NAME}, ds)
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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        command.AddParameter(PAR_I_NAME_ATTRIBUTE_VALUE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_ATTRIBUTE_VALUE_ID)

    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_ATTRIBUTE_VALUE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_ATTRIBUTE_VALUE_ID)
            .AddParameter(PAR_I_NAME_ATTRIBUTE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_ATTRIBUTE_ID)
            .AddParameter(PAR_I_NAME_ATTRIBUTE_VALUE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ATTRIBUTE_VALUE)
            .AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REFERENCE_ID)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_EFFECTIVE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE_DATE)
            .AddParameter(PAR_I_NAME_EXPIRATION_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION_DATE)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(PAR_I_NAME_ATTRIBUTE_VALUE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_ATTRIBUTE_VALUE_ID)
            .AddParameter(PAR_I_NAME_ATTRIBUTE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_ATTRIBUTE_ID)
            .AddParameter(PAR_I_NAME_ATTRIBUTE_VALUE, OracleDbType.Varchar2, sourceColumn:=COL_NAME_ATTRIBUTE_VALUE)
            .AddParameter(PAR_I_NAME_REFERENCE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_REFERENCE_ID)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_EFFECTIVE_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE_DATE)
            .AddParameter(PAR_I_NAME_EXPIRATION_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION_DATE)
        End With

    End Sub
#End Region

End Class




'Public Class AttributeValueDAL
'    Inherits DALBase

'#Region "Constants"
'    Public Const TABLE_NAME As String = "ELP_ATTRIBUTE_VALUE"
'    Public Const TABLE_KEY_NAME As String = "attribute_value_id"

'    Public Const COL_NAME_ATTRIBUTE_VALUE_ID As String = "attribute_value_id"
'    Public Const COL_NAME_ATTRIBUTE_ID As String = "attribute_id"
'    Public Const COL_NAME_ATTRIBUTE_VALUE As String = "attribute_value"
'    Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
'    Public Const COL_NAME_ATTRIBUTE_NAME As String = "attribute_name"
'    Public Const COL_NAME_TABLE_NAME As String = "table_name"
'    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
'    Public Const COL_NAME_VALUE As String = "attribute_value"
'    Public Const COL_NAME_DATA_TYPE_ID As String = "data_type_id"
'#End Region

'#Region "Constructors"
'    Public Sub New()
'        MyBase.new()
'    End Sub
'#End Region

'#Region "Load Methods"
'    Public Function LoadSchema(ByVal ds As DataSet) As DataSet
'        Return Load(ds, Guid.Empty)
'    End Function

'    Public Function Load(ByVal familyDS As DataSet, ByVal id As Guid) As DataSet
'        Dim selectStmt As String = Me.Config("/SQL/LOAD")
'        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_ATTRIBUTE_ID, id.ToByteArray)}
'        Try
'            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
'            Return familyDS
'        Catch ex As Exception
'            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
'        End Try
'    End Function

'    <Obsolete("This Method is Specific to Service Center, this should be removed")> _
'    Public Sub LoadList(ByVal familyDS As DataSet, ByVal id As Guid, ByVal LanguageId As Guid)
'        Dim selectStmt As String = Me.Config("/SQL/LOAD_SERVICE_CENTER_ATTRIBUTE_LIST")
'        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_REFERENCE_ID, id.ToByteArray)} ', _
'        'New DBHelper.DBHelperParameter(Me.COL_NAME_LANGUAGE_ID, LanguageId)}
'        Try
'            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
'        Catch ex As Exception
'            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
'        End Try
'    End Sub

'#End Region

'#Region "Overloaded Methods"
'    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
'        'Start DEF-3088
'        If ds Is Nothing Then
'            Return
'        End If
'        'End DEF-3088

'        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
'            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
'        End If
'    End Sub
'#End Region

'#Region "Helper Methods"
'    Public Function GetAttributeCodeView(ByVal ServiceCenterId As Guid, ByVal LanguageId As Guid) As DataSet
'        Dim selectStmt As String = Me.Config("/SQL/LOAD_SERVICE_CENTER_ATTRIBUTE_LIST")
'        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_REFERENCE_ID, ServiceCenterId.ToByteArray)} ', _
'        'New DBHelper.DBHelperParameter(Me.COL_NAME_LANGUAGE_ID, LanguageId)}
'        Try
'            Dim ds As New DataSet
'            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
'            Return ds
'        Catch ex As Exception
'            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
'        End Try
'    End Function
'#End Region


'    Public Sub LoadAvailableAttributesByTableNameReferenceId(ByVal familyDS As DataSet, ByVal tableName As String, ByVal id As Guid, ByVal languageId As Guid)
'        Dim selectStmt As String = Me.Config("/SQL/LOAD_AVAILABLE_ATRIBUTES_BY_TABLE_NAME_REFERENCE_ID")
'        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_TABLE_NAME, tableName), New DBHelper.DBHelperParameter(COL_NAME_REFERENCE_ID, id.ToByteArray), New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray())}
'        Try
'            DBHelper.Fetch(familyDS, selectStmt, AttributeDAL.TABLE_NAME, parameters)
'        Catch ex As Exception
'            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
'        End Try
'    End Sub

'End Class
