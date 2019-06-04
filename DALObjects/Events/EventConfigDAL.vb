'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/20/2012)********************


Public Class EventConfigDAL
    Inherits OracleDALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_EVENT_CONFIG"
    Public Const TABLE_KEY_NAME As String = "event_config_id"

    Public Const COL_NAME_EVENT_CONFIG_ID As String = "event_config_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_DEALER_GROUP_ID As String = "dealer_group_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_EVENT_TYPE_ID As String = "event_type_id"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_EVENT_ARGUMENT_ID As String = "event_argument_id"

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
        cmd.AddParameter("pi_event_config_id", OracleDbType.Raw, id.ToByteArray())
        cmd.AddParameter("po_resultcursor", OracleDbType.RefCursor, direction:=ParameterDirection.Output)

        Try
            OracleDbHelper.Fetch(cmd, Me.TABLE_NAME, familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal CompGrpID As Guid, ByVal CompanyID As Guid, ByVal CountryID As Guid, ByVal DealerGroupID As Guid,
                             ByVal DealerID As Guid, ByVal strProdCode As String, ByVal CoverageTypeID As Guid,
                             ByVal LanguageID As Guid, ByVal networkID As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())

        cmd.BindByName = True
        OracleDbHelper.AddParameter(cmd, "pi_language_id", OracleDbType.Raw, LanguageID.ToByteArray, ParameterDirection.Input)
        OracleDbHelper.AddParameter(cmd, "pi_network_id", OracleDbType.Varchar2, networkID.ToUpper(), ParameterDirection.Input)

        If CompanyID <> Guid.Empty Then
            cmd.AddParameter("pi_company_id", OracleDbType.Raw, CompanyID.ToByteArray())
        End If

        If CountryID <> Guid.Empty Then
            cmd.AddParameter("pi_country_id", OracleDbType.Raw, CountryID.ToByteArray())
        End If

        If CompGrpID <> Guid.Empty Then
            cmd.AddParameter("pi_company_group_id", OracleDbType.Raw, CompGrpID.ToByteArray())
        End If

        If DealerGroupID <> Guid.Empty Then
            cmd.AddParameter("pi_dealer_group_id", OracleDbType.Raw, DealerGroupID.ToByteArray())
        End If

        If DealerID <> Guid.Empty Then
            cmd.AddParameter("pi_dealer_id", OracleDbType.Raw, DealerID.ToByteArray())
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
            .AddParameter(parameterName:="pi_event_config_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_CONFIG_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_country_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COUNTRY_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_product_code", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_event_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_coverage_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_event_argument_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_ARGUMENT_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_modified_by", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY, direction:=ParameterDirection.Input)
        End With
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .BindByName = True
            .AddParameter(parameterName:="pi_event_config_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_CONFIG_ID, direction:=ParameterDirection.Input)
        End With
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .BindByName = True
            .AddParameter(parameterName:="pi_event_config_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_CONFIG_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_company_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_country_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COUNTRY_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_dealer_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_product_code", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_event_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_coverage_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_event_argument_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_ARGUMENT_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_created_by", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY, direction:=ParameterDirection.Input)
        End With
    End Sub

#End Region

End Class