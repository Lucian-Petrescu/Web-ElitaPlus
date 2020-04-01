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
    Public Const COL_NAME_PURPOSE_XCD As String = "purpose_xcd"
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

#Region "Validation Methods"

    Public Function CheckForDuplicateConfiguration(ByVal ConfigQuestionSetID As Guid, ByVal CompanyGroupID As Guid, ByVal CompanyID As Guid,
                                                   ByVal DealerGroupID As Guid, ByVal DealerID As Guid, ByVal ProductCodeID As Guid,
                                                   ByVal CoverageTypeID As Guid, ByVal DeviceTypeID As Guid, ByVal RiskTypeID As Guid,
                                                   ByVal strPurposeXCD As String, ByVal strQuestionSetCode As String) As String

        Dim errorMsg As String = String.Empty
        Dim selectStmt As String = Me.Config("/SQL/VALIDATE")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())

        cmd.BindByName = True

        If ConfigQuestionSetID <> Guid.Empty Then
            cmd.AddParameter("pi_config_question_set_id", OracleDbType.Raw, ConfigQuestionSetID.ToByteArray())
        End If

        If CompanyGroupID <> Guid.Empty Then
            cmd.AddParameter("pi_company_group_id", OracleDbType.Raw, CompanyGroupID.ToByteArray())
        End If

        If CompanyID <> Guid.Empty Then
            cmd.AddParameter("pi_company_id", OracleDbType.Raw, CompanyID.ToByteArray())
        End If

        If DealerGroupID <> Guid.Empty Then
            cmd.AddParameter("pi_dealer_group_id", OracleDbType.Raw, DealerGroupID.ToByteArray())
        End If

        If DealerID <> Guid.Empty Then
            cmd.AddParameter("pi_dealer_id", OracleDbType.Raw, DealerID.ToByteArray())
        End If

        If ProductCodeID <> Guid.Empty Then
            cmd.AddParameter("pi_product_code_id", OracleDbType.Raw, ProductCodeID.ToByteArray())
        End If

        If CoverageTypeID <> Guid.Empty Then
            cmd.AddParameter("pi_coverage_type_id", OracleDbType.Raw, CoverageTypeID.ToByteArray())
        End If

        If DeviceTypeID <> Guid.Empty Then
            cmd.AddParameter("pi_device_type_id", OracleDbType.Raw, DeviceTypeID.ToByteArray())
        End If

        If RiskTypeID <> Guid.Empty Then
            cmd.AddParameter("pi_risk_type_id", OracleDbType.Raw, RiskTypeID.ToByteArray())
        End If

        OracleDbHelper.AddParameter(cmd, "pi_purpose_xcd", OracleDbType.Varchar2, Me.GetFormattedSearchStringForSQL(strPurposeXCD), ParameterDirection.Input)

        OracleDbHelper.AddParameter(cmd, "pi_question_set_code", OracleDbType.Varchar2, Me.GetFormattedSearchStringForSQL(strQuestionSetCode), ParameterDirection.Input)

        cmd.AddParameter("po_result", OracleDbType.Varchar2, direction:=ParameterDirection.Output, size:=200)

        Try
            OracleDbHelper.ExecuteNonQuery(cmd)
            errorMsg = cmd.Parameters.Item("po_result").Value.ToString()
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return errorMsg
    End Function

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

    Public Function LoadList(ByVal CompGrpID As Guid, ByVal CompanyID As Guid, ByVal DealerGrpID As Guid, ByVal DealerID As Guid,
                             ByVal ProductCodeID As Guid, ByVal CoverageTypeID As Guid, ByVal RiskTypeID As Guid,
                             ByVal strPurposeXCD As String, ByVal strQuestionSetCode As String, ByVal LanguageID As Guid,
                             ByVal networkID As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())

        cmd.BindByName = True
        cmd.AddParameter("pi_language_id", OracleDbType.Raw, LanguageID.ToByteArray())
        cmd.AddParameter("pi_network_id", OracleDbType.Varchar2, 8, networkID.ToUpper())

        If CompGrpID <> Guid.Empty Then
            cmd.AddParameter("pi_company_group_id", OracleDbType.Raw, CompGrpID.ToByteArray())
        End If

        If CompanyID <> Guid.Empty Then
            cmd.AddParameter("pi_company_id", OracleDbType.Raw, CompanyID.ToByteArray())
        End If

        If DealerGrpID <> Guid.Empty Then
            cmd.AddParameter("pi_dealer_group_id", OracleDbType.Raw, DealerGrpID.ToByteArray())
        End If

        If DealerID <> Guid.Empty Then
            cmd.AddParameter("pi_dealer_id", OracleDbType.Raw, DealerID.ToByteArray())
        End If

        If ProductCodeID <> Guid.Empty Then
            cmd.AddParameter("pi_product_code_id", OracleDbType.Raw, ProductCodeID.ToByteArray())
        End If

        If CoverageTypeID <> Guid.Empty Then
            cmd.AddParameter("pi_coverage_type_id", OracleDbType.Raw, CoverageTypeID.ToByteArray())
        End If

        If RiskTypeID <> Guid.Empty Then
            cmd.AddParameter("pi_risk_type_id", OracleDbType.Raw, RiskTypeID.ToByteArray())
        End If

        If Not String.IsNullOrWhiteSpace(strPurposeXCD) Then
            OracleDbHelper.AddParameter(cmd, "pi_purpose_xcd", OracleDbType.Varchar2, Me.GetFormattedSearchStringForSQL(strPurposeXCD), ParameterDirection.Input)
        End If

        If Not String.IsNullOrWhiteSpace(strQuestionSetCode) Then
            OracleDbHelper.AddParameter(cmd, "pi_question_set_code", OracleDbType.Varchar2, Me.GetFormattedSearchStringForSQL(strQuestionSetCode), ParameterDirection.Input)
        End If

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
            .AddParameter(parameterName:="pi_config_question_set_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_CONFIG_QUESTION_SET_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_product_code_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_PRODUCT_CODE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_coverage_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_risk_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_RISK_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_device_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEVICE_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_purpose_xcd", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_PURPOSE_XCD, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_question_set_code", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_QUESTION_SET_CODE, direction:=ParameterDirection.Input)

            .AddParameter(parameterName:="pi_modified_by", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY, direction:=ParameterDirection.Input)
        End With
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .BindByName = True
            .AddParameter(parameterName:="pi_config_question_set_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_CONFIG_QUESTION_SET_ID, direction:=ParameterDirection.Input)
        End With
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .BindByName = True
            .AddParameter(parameterName:="pi_config_question_set_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_CONFIG_QUESTION_SET_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_product_code_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_PRODUCT_CODE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_coverage_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_risk_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_RISK_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_device_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEVICE_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_purpose_xcd", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_PURPOSE_XCD, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_question_set_code", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_QUESTION_SET_CODE, direction:=ParameterDirection.Input)

            .AddParameter(parameterName:="pi_created_by", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY, direction:=ParameterDirection.Input, size:=30)
        End With
    End Sub

#End Region

End Class