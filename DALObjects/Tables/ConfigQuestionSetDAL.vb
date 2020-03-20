Public Class ConfigQuestionSetDAL
    Inherits OracleDALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CONFIG_QUESN_SET_CODE"
    Public Const TABLE_KEY_NAME As String = "config_question_set_id"

    Public Const COL_NAME_CONFIG_QUESTION_SET_ID As String = "config_question_set_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEALER_GROUP_ID As String = "dealer_group_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_DEVICE_TYPE_ID As String = "device_type_id"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_COVERAGE_CONSEQ_DAMAGE_ID As String = "coverage_conseq_damage_id"
    Public Const COL_NAME_PURPOSE_ID As String = "purpose_id"
    Public Const COL_NAME_PURPOSE_CODE As String = "purpose_xcd"
    Public Const COL_NAME_QUESTION_SET_CODE As String = "question_set_code"

    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by"
    Public Const COL_NAME_MODIFIED_DATE As String = "modified_date"
    Public Const COL_NAME_MODIFIED_BY As String = "modified_by"

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
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())

        cmd.BindByName = True
        cmd.AddParameter("pi_config_question_set_id", OracleDbType.Raw, id.ToByteArray())
        cmd.AddParameter("po_resultcursor", OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal CompGrpID As Guid, ByVal CompanyID As Guid, ByVal CountryID As Guid,
                             ByVal DealerGrpID As Guid, ByVal DealerID As Guid, ByVal strProdCode As String, ByVal EventTypeID As Guid,
                             ByVal TaskID As Guid, ByVal CoverageTypeID As Guid, ByVal LanguageID As Guid,
                             ByVal networkID As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())

        cmd.BindByName = True
        cmd.AddParameter("pi_language_id", OracleDbType.Raw, LanguageID.ToByteArray())
        cmd.AddParameter("pi_network_id", OracleDbType.Varchar2, 8, networkID.ToUpper())

        If CompanyID <> Guid.Empty Then
            cmd.AddParameter("pi_company_id", OracleDbType.Raw, CompanyID.ToByteArray())
        End If

        If CountryID <> Guid.Empty Then
            cmd.AddParameter("pi_country_id", OracleDbType.Raw, CountryID.ToByteArray())
        End If

        If CompGrpID <> Guid.Empty Then
            cmd.AddParameter("pi_company_group_id", OracleDbType.Raw, CompGrpID.ToByteArray())
        End If

        If DealerGrpID <> Guid.Empty Then
            cmd.AddParameter("pi_dealer_group_id", OracleDbType.Raw, DealerGrpID.ToByteArray())
        End If

        If DealerID <> Guid.Empty Then
            cmd.AddParameter("pi_dealer_id", OracleDbType.Raw, DealerID.ToByteArray())
        End If

        If EventTypeID <> Guid.Empty Then
            cmd.AddParameter("pi_event_type_id", OracleDbType.Raw, EventTypeID.ToByteArray())
        End If

        If TaskID <> Guid.Empty Then
            cmd.AddParameter("pi_task_id", OracleDbType.Raw, TaskID.ToByteArray())
        End If

        If CoverageTypeID <> Guid.Empty Then
            cmd.AddParameter("pi_coverage_type_id", OracleDbType.Raw, CoverageTypeID.ToByteArray())
        End If

        OracleDbHelper.AddParameter(cmd, "pi_product_code", OracleDbType.Varchar2, Me.GetFormattedSearchStringForSQL(strProdCode), ParameterDirection.Input)

        cmd.AddParameter("po_resultcursor", OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            Return OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .BindByName = True
            .AddParameter(parameterName:="pi_event_task_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_TASK_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_country_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COUNTRY_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_product_code", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_event_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_task_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_TASK_ID, direction:=ParameterDirection.Input)

            .AddParameter(parameterName:="pi_retry_count", dbType:=OracleDbType.Double, sourceColumn:=COL_NAME_RETRY_COUNT, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_retry_delay_seconds", dbType:=OracleDbType.Double, sourceColumn:=COL_NAME_RETRY_DELAY_SECONDS, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_timeout_seconds", dbType:=OracleDbType.Double, sourceColumn:=COL_NAME_TIMEOUT_SECONDS, direction:=ParameterDirection.Input)

            .AddParameter(parameterName:="pi_coverage_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_event_argument_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_ARGUMENT_ID, direction:=ParameterDirection.Input)

            .AddParameter(parameterName:="pi_event_task_parameters", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_EVENT_TASK_PARAMETERS, direction:=ParameterDirection.Input, size:=4000)
            .AddParameter(parameterName:="pi_init_delay_minutes", dbType:=OracleDbType.Double, sourceColumn:=COL_NAME_INIT_DELAY_MINUTES, direction:=ParameterDirection.Input)

            .AddParameter(parameterName:="pi_modified_by", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY, direction:=ParameterDirection.Input)
        End With
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .BindByName = True
            .AddParameter(parameterName:="pi_event_task_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_TASK_ID, direction:=ParameterDirection.Input)
        End With
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .BindByName = True
            .AddParameter(parameterName:="pi_event_task_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_TASK_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_country_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COUNTRY_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_product_code", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_event_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_task_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_TASK_ID, direction:=ParameterDirection.Input)

            .AddParameter(parameterName:="pi_retry_count", dbType:=OracleDbType.Int16, sourceColumn:=COL_NAME_RETRY_COUNT, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_retry_delay_seconds", dbType:=OracleDbType.Int32, sourceColumn:=COL_NAME_RETRY_DELAY_SECONDS, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_timeout_seconds", dbType:=OracleDbType.Int32, sourceColumn:=COL_NAME_TIMEOUT_SECONDS, direction:=ParameterDirection.Input)

            .AddParameter(parameterName:="pi_coverage_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_event_argument_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_ARGUMENT_ID, direction:=ParameterDirection.Input)

            .AddParameter(parameterName:="pi_event_task_parameters", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_EVENT_TASK_PARAMETERS, direction:=ParameterDirection.Input, size:=4000)
            .AddParameter(parameterName:="pi_init_delay_minutes", dbType:=OracleDbType.Int32, sourceColumn:=COL_NAME_INIT_DELAY_MINUTES, direction:=ParameterDirection.Input)

            .AddParameter(parameterName:="pi_created_by", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY, direction:=ParameterDirection.Input, size:=30)
        End With
    End Sub

#End Region

End Class