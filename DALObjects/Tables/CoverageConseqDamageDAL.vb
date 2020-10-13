'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject v2.cst (5/25/2017)********************


Public Class CoverageConseqDamageDAL
    Inherits DALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified Or DataRowState.Deleted
    Public Const TABLE_NAME As String = "ELP_COVERAGE_CONSEQ_DAMAGE"
    Public Const TABLE_KEY_NAME As String = COL_NAME_COVERAGE_CONSEQ_DAMAGE_ID

    Public Const COL_NAME_COVERAGE_CONSEQ_DAMAGE_ID As String = "coverage_conseq_damage_id"
    Public Const COL_NAME_COVERAGE_ID As String = "coverage_id"
    Public Const COL_NAME_CONSEQ_DAMAGE_TYPE_XCD As String = "conseq_damage_type_xcd"
    Public Const COL_NAME_LIABILITY_LIMIT_BASE_XCD As String = "liability_limit_base_xcd"
    Public Const COL_NAME_LIABILITY_LIMIT_PER_INCIDENT As String = "liability_limit_per_incident"
    Public Const COL_NAME_LIABILITY_LIMIT_CUMULATIVE As String = "liability_limit_cumulative"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_FULFILMENT_METHOD_XCD As String = "fulfilment_method_xcd"

    Public Const COL_NAME_CONSEQ_DAMAGE_TYPE_DESC As String = "conseq_damage_type_desc"
    Public Const COL_NAME_LIABILITY_LIMIT_BASE_DESC As String = "liability_limit_base_desc"
    Public Const COL_NAME_FULFILMENT_METHOD_DESC As String = "fulfilment_method_desc"

    Public Const PAR_I_NAME_COVERAGE_CONSEQ_DAMAGE_ID As String = "pi_coverage_conseq_damage_id"
    Public Const PAR_I_NAME_COVERAGE_ID As String = "pi_coverage_id"
    Public Const PAR_I_NAME_CONSEQ_DAMAGE_TYPE_XCD As String = "pi_conseq_damage_type_xcd"
    Public Const PAR_I_NAME_LIABILITY_LIMIT_BASE_XCD As String = "pi_liability_limit_base_xcd"
    Public Const PAR_I_NAME_LIABILITY_LIMIT_PER_INCIDENT As String = "pi_liability_limit_per_incident"
    Public Const PAR_I_NAME_LIABILITY_LIMIT_CUMULATIVE As String = "pi_liability_limit_cumulative"
    Public Const PAR_I_NAME_EFFECTIVE As String = "pi_effective"
    Public Const PAR_I_NAME_EXPIRATION As String = "pi_expiration"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("coverage_peril_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(coverageId As Guid, LanguageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim ds As New DataSet
        parameters = New DBHelper.DBHelperParameter() _
               {
                New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray),
                New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray),
                New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray),
                New DBHelper.DBHelperParameter(COL_NAME_COVERAGE_ID, coverageId.ToByteArray)
               }
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Return ds
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

    'Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
    '    command.AddParameter(PAR_I_NAME_COVERAGE_PERIL_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_PERIL_ID)

    'End Sub

    'Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
    '    With command
    '        .AddParameter(COL_NAME_COVERAGE_PERIL_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_PERIL_ID)
    '        .AddParameter(COL_NAME_COVERAGE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_ID)
    '        .AddParameter(COL_NAME_PERIL_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PERIL_TYPE_XCD)
    '        .AddParameter(COL_NAME_LIABILITY_LIMIT_BASE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LIABILITY_LIMIT_BASE_XCD)
    '        .AddParameter(COL_NAME_LIABILITY_LIMIT_PER_INCIDENT, OracleDbType.Decimal, sourceColumn:=COL_NAME_LIABILITY_LIMIT_PER_INCIDENT)
    '        .AddParameter(COL_NAME_LIABILITY_LIMIT_CUMULATIVE, OracleDbType.Decimal, sourceColumn:=COL_NAME_LIABILITY_LIMIT_CUMULATIVE)
    '        .AddParameter(COL_NAME_EFFECTIVE, OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE)
    '        .AddParameter(COL_NAME_EXPIRATION, OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION)
    '        '.AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
    '        .AddParameter(COL_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
    '    End With

    'End Sub

    'Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
    '    With command
    '        .AddParameter(PAR_I_NAME_COVERAGE_PERIL_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_PERIL_ID)
    '        .AddParameter(PAR_I_NAME_COVERAGE_ID, OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_ID)
    '        .AddParameter(PAR_I_NAME_PERIL_TYPE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_PERIL_TYPE_XCD)
    '        .AddParameter(PAR_I_NAME_LIABILITY_LIMIT_BASE_XCD, OracleDbType.Varchar2, sourceColumn:=COL_NAME_LIABILITY_LIMIT_BASE_XCD)
    '        .AddParameter(PAR_I_NAME_LIABILITY_LIMIT_PER_INCIDENT, OracleDbType.Decimal, sourceColumn:=COL_NAME_LIABILITY_LIMIT_PER_INCIDENT)
    '        .AddParameter(PAR_I_NAME_LIABILITY_LIMIT_CUMULATIVE, OracleDbType.Decimal, sourceColumn:=COL_NAME_LIABILITY_LIMIT_CUMULATIVE)
    '        .AddParameter(PAR_I_NAME_EFFECTIVE, OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE)
    '        .AddParameter(PAR_I_NAME_EXPIRATION, OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION)
    '        .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
    '        .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
    '    End With

    'End Sub
#End Region

End Class


