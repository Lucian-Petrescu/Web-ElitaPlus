'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/20/2012)********************

Public Class ClaimStageDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const supportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified
    Public Const TABLE_NAME As String = "ELP_STAGE"
    Public Const TABLE_KEY_NAME As String = "stage_id"

    Public Const COL_NAME_STAGE_ID As String = "stage_id"
    Public Const COL_NAME_STAGE_NAME_ID As String = "stage_name_id"
    Public Const COL_NAME_START_STATUS_ID As String = "start_status_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_SEQUENCE As String = "sequence"
    Public Const COL_NAME_SCREEN_ID As String = "screen_id"
    Public Const COL_NAME_PORTAL_ID As String = "portal_id"

    Public Const COL_MIN_EFFECTIVE As String = "min_effective"
    Public Const COL_MAX_EXPIRATION As String = "max_expiration"
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
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.Text, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "stage_id", OracleDbType.Raw, id.ToByteArray, ParameterDirection.Input)

        Try
            OracleDbHelper.Fetch(cmd, TABLE_NAME, familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(StageNameID As Guid, CompGrpID As Guid, CompanyID As Guid, DealerID As Guid, _
                             CoverageTypeID As Guid, ActiveOn As DateType, Sequence As String, _
                             ScreenID As Guid, PortalID As Guid, LanguageID As Guid, _
                             userCompanies As ArrayList, userCountries As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = "", strTemp As String
        Dim ds As New DataSet

        strTemp = MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, userCompanies, True)
        whereClauseConditions &= Environment.NewLine & " and (s." & COL_NAME_COMPANY_ID & " is null or s." & strTemp & ")"
        whereClauseConditions &= Environment.NewLine & " and (s." & COL_NAME_DEALER_ID & " is null or d." & strTemp & ")"
        whereClauseConditions &= Environment.NewLine & " and (s." & COL_NAME_COMPANY_GROUP_ID & " is null or (exists (select null from elp_company where(" & COL_NAME_COMPANY_GROUP_ID & " = s." & COL_NAME_COMPANY_GROUP_ID & ") and " & strTemp & ")))"

        If StageNameID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and s." & COL_NAME_STAGE_NAME_ID & " = " & MiscUtil.GetDbStringFromGuid(StageNameID, True)
        End If

        If CompGrpID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and s." & COL_NAME_COMPANY_GROUP_ID & " = " & MiscUtil.GetDbStringFromGuid(CompGrpID, True)
        End If

        If CompanyID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and s." & COL_NAME_COMPANY_ID & " = " & MiscUtil.GetDbStringFromGuid(CompanyID, True)
        End If

        If DealerID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and s." & COL_NAME_DEALER_ID & " = " & MiscUtil.GetDbStringFromGuid(DealerID, True)
        End If

        If CoverageTypeID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and s." & COL_NAME_COVERAGE_TYPE_ID & " = " & MiscUtil.GetDbStringFromGuid(CoverageTypeID, True)
        End If

        If Not ActiveOn = Nothing Then
            whereClauseConditions &= Environment.NewLine & " and trunc(to_date('" & Date.Parse(ActiveOn).ToString("MM/dd/yyyy HH:mm:ss") _
                          & "', 'mm-dd-yyyy hh24:mi:ss')) BETWEEN trunc(s." & COL_NAME_EFFECTIVE_DATE & ")" & " AND trunc(s." & COL_NAME_EXPIRATION_DATE & ")" & ""
        End If

        If (Not Sequence = Nothing AndAlso (FormatSearchMask(Sequence))) Then
            whereClauseConditions &= Environment.NewLine & "and UPPER(s." & COL_NAME_SEQUENCE & ")" & Sequence
        End If

        If ScreenID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and s." & COL_NAME_SCREEN_ID & " = " & MiscUtil.GetDbStringFromGuid(ScreenID, True)
        End If

        If PortalID <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and s." & COL_NAME_PORTAL_ID & " = " & MiscUtil.GetDbStringFromGuid(PortalID, True)
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.Text, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "language_id", OracleDbType.Raw, LanguageID.ToByteArray, ParameterDirection.Input)
        OracleDbHelper.AddParameter(cmd, "language_id", OracleDbType.Raw, LanguageID.ToByteArray, ParameterDirection.Input)
        OracleDbHelper.AddParameter(cmd, "language_id", OracleDbType.Raw, LanguageID.ToByteArray, ParameterDirection.Input)
        OracleDbHelper.AddParameter(cmd, "language_id", OracleDbType.Raw, LanguageID.ToByteArray, ParameterDirection.Input)
        OracleDbHelper.AddParameter(cmd, "language_id", OracleDbType.Raw, LanguageID.ToByteArray, ParameterDirection.Input)

        Try
            Return OracleDbHelper.Fetch(cmd, TABLE_NAME, ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    'LoadEndStatusList(Me.Dataset, StageEndStatusDAL.TABLE_NAME, Me.Id, languageid)
    Public Sub LoadEndStatusList(familyDS As DataSet, tablename As String, stageid As Guid, languageid As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_STAGE_END_STATUS_LIST")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.Text, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "language_id", OracleDbType.Raw, languageid.ToByteArray, ParameterDirection.Input)
        OracleDbHelper.AddParameter(cmd, "stage_id", OracleDbType.Raw, stageid.ToByteArray, ParameterDirection.Input)

        Try
            OracleDbHelper.Fetch(cmd, tablename, familyDS)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Function LoadMinEffectiveMaxExpiration(StageNameId As Guid, CompanyGroupId As Guid, CompanyId As Guid, DealerId As Guid, ProductCode As String, CoverageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_MIN_EFFECTIVE_MAX_EXPIRATION")
        Dim whereClauseConditions As String = ""
        Dim groupbyClause As String = " Group By "
        Dim applygroupby As Boolean = False
        Dim ds As New DataSet

        If StageNameId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_STAGE_NAME_ID & " = " & MiscUtil.GetDbStringFromGuid(StageNameId, True)
        End If

        If CompanyGroupId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_COMPANY_GROUP_ID & " = " & MiscUtil.GetDbStringFromGuid(CompanyGroupId, True)
            groupbyClause &= COL_NAME_COMPANY_GROUP_ID
            applygroupby = True
        ElseIf CompanyId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_COMPANY_ID & " = " & MiscUtil.GetDbStringFromGuid(CompanyId, True)
            groupbyClause &= COL_NAME_COMPANY_ID
            applygroupby = True
        ElseIf DealerId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_DEALER_ID & " = " & MiscUtil.GetDbStringFromGuid(DealerId, True)
            groupbyClause &= COL_NAME_DEALER_ID
            applygroupby = True
            If Not String.IsNullOrEmpty(ProductCode) Then
                whereClauseConditions &= Environment.NewLine & " and UPPER(" & COL_NAME_PRODUCT_CODE & ") = UPPER('" & ProductCode & "')"
                groupbyClause &= ", " & COL_NAME_PRODUCT_CODE
                If CoverageId <> Guid.Empty Then
                    whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_COVERAGE_TYPE_ID & " = " & MiscUtil.GetDbStringFromGuid(CoverageId, True)
                    groupbyClause &= ", " & COL_NAME_COVERAGE_TYPE_ID
                    applygroupby = True
                End If
            End If
        End If

        If applygroupby = True Then
            whereClauseConditions &= groupbyClause
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.Text, OracleDbHelper.CreateConnection())

        Try
            Return OracleDbHelper.Fetch(cmd, TABLE_NAME, ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Function LoadStagesWithSameDefinition(StageId As Guid, StageNameId As Guid, CompanyGroupId As Guid, CompanyId As Guid, DealerId As Guid, ProductCode As String, CoverageId As Guid, StartStatusId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_STAGES_WITH_SAME_DEFINATION")
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet

        If StageId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_STAGE_ID & " <> " & MiscUtil.GetDbStringFromGuid(StageId, True)
        End If

        If StageNameId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_STAGE_NAME_ID & " = " & MiscUtil.GetDbStringFromGuid(StageNameId, True)
        End If

        If CompanyGroupId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_COMPANY_GROUP_ID & " = " & MiscUtil.GetDbStringFromGuid(CompanyGroupId, True)
        ElseIf CompanyId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_COMPANY_ID & " = " & MiscUtil.GetDbStringFromGuid(CompanyId, True)
        ElseIf DealerId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_DEALER_ID & " = " & MiscUtil.GetDbStringFromGuid(DealerId, True)
            If Not String.IsNullOrEmpty(ProductCode) Then
                whereClauseConditions &= Environment.NewLine & " and UPPER(" & COL_NAME_PRODUCT_CODE & ") = UPPER('" & ProductCode & "')"
                If CoverageId <> Guid.Empty Then
                    whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_COVERAGE_TYPE_ID & " = " & MiscUtil.GetDbStringFromGuid(CoverageId, True)
                End If
            End If
        End If

        If StartStatusId <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & " and " & COL_NAME_START_STATUS_ID & " = " & MiscUtil.GetDbStringFromGuid(StartStatusId, True)
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.Text, OracleDbHelper.CreateConnection())

        Try
            Return OracleDbHelper.Fetch(cmd, TABLE_NAME, ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Children Related"

    Public Function GetAvailableStageStartStatusList(company_group_id As Guid, language_id As Guid) As DataView
        Dim selectStmt As String = Config("/SQL/LOAD_AVAILABLE_STAGE_START_STATUS")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.Text, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "language_id", OracleDbType.Raw, language_id.ToByteArray, ParameterDirection.Input)
        OracleDbHelper.AddParameter(cmd, "company_group_id", OracleDbType.Raw, company_group_id.ToByteArray, ParameterDirection.Input)

        Try
            Dim ds As DataSet = OracleDbHelper.Fetch(cmd, "AVASTGSTARTSTATUSLST")

            If ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView()
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAvailableStageEndStatusList(company_group_id As Guid, language_id As Guid) As DataView
        Dim selectStmt As String = Config("/SQL/LOAD_AVAILABLE_STAGE_END_STATUS")

        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.Text, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "language_id", OracleDbType.Raw, language_id.ToByteArray, ParameterDirection.Input)
        OracleDbHelper.AddParameter(cmd, "company_group_id", OracleDbType.Raw, company_group_id.ToByteArray, ParameterDirection.Input)

        Try
            Dim ds As DataSet = OracleDbHelper.Fetch(cmd, "AVASTGENDSTATUSLST")

            If ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView()
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetSelectedStageEndStatusList(stage_id As Guid, language_id As Guid) As DataView
        Dim selectStmt As String = Config("/SQL/LOAD_SELECTED_STAGE_END_STATUS")
        Dim cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.Text, OracleDbHelper.CreateConnection())
        OracleDbHelper.AddParameter(cmd, "language_id", OracleDbType.Raw, language_id.ToByteArray, ParameterDirection.Input)
        OracleDbHelper.AddParameter(cmd, "stage_id", OracleDbType.Raw, stage_id.ToByteArray, ParameterDirection.Input)

        Try
            Dim ds As DataSet = OracleDbHelper.Fetch(cmd, "SELSTGENDSTATUSLST")

            If ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView()
            End If

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
        With command
            .AddParameter("pi_stage_id", OracleDbType.Raw, sourceColumn:=COL_NAME_STAGE_ID, direction:=ParameterDirection.Input)
        End With
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter("pi_stage_id", OracleDbType.Raw, sourceColumn:=COL_NAME_STAGE_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_stage_name_id", OracleDbType.Raw, sourceColumn:=COL_NAME_STAGE_NAME_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_start_status_id", OracleDbType.Raw, sourceColumn:=COL_NAME_START_STATUS_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_company_group_id", OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_company_id", OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_dealer_id", OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_coverage_type_id", OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_effective_date", OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE_DATE, direction:=ParameterDirection.Input)
            .AddParameter("pi_expiration_date", OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION_DATE, direction:=ParameterDirection.Input)
            .AddParameter("pi_created_by", OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY, direction:=ParameterDirection.Input)
            .AddParameter("pi_sequence", OracleDbType.Double, sourceColumn:=COL_NAME_SEQUENCE, direction:=ParameterDirection.Input)
            .AddParameter("pi_screen_id", OracleDbType.Raw, sourceColumn:=COL_NAME_SCREEN_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_portal_id", OracleDbType.Raw, sourceColumn:=COL_NAME_PORTAL_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_product_code", OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE, direction:=ParameterDirection.Input)
        End With
    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter("pi_stage_id", OracleDbType.Raw, sourceColumn:=COL_NAME_STAGE_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_stage_name_id", OracleDbType.Raw, sourceColumn:=COL_NAME_STAGE_NAME_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_start_status_id", OracleDbType.Raw, sourceColumn:=COL_NAME_START_STATUS_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_company_group_id", OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_GROUP_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_company_id", OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_dealer_id", OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_coverage_type_id", OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_TYPE_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_effective_date", OracleDbType.Date, sourceColumn:=COL_NAME_EFFECTIVE_DATE, direction:=ParameterDirection.Input)
            .AddParameter("pi_expiration_date", OracleDbType.Date, sourceColumn:=COL_NAME_EXPIRATION_DATE, direction:=ParameterDirection.Input)
            .AddParameter("pi_modified_by", OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY, direction:=ParameterDirection.Input)
            .AddParameter("pi_sequence", OracleDbType.Double, sourceColumn:=COL_NAME_SEQUENCE, direction:=ParameterDirection.Input)
            .AddParameter("pi_screen_id", OracleDbType.Raw, sourceColumn:=COL_NAME_SCREEN_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_portal_id", OracleDbType.Raw, sourceColumn:=COL_NAME_PORTAL_ID, direction:=ParameterDirection.Input)
            .AddParameter("pi_product_code", OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE, direction:=ParameterDirection.Input)
        End With
    End Sub

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim tr As IDbTransaction = Transaction

        If tr Is Nothing Then
            tr = OracleDbHelper.BeginTransaction
        End If

        Try

            Dim _stageEndStatusDAL As New StageEndStatusDAL
            _stageEndStatusDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            _stageEndStatusDAL.Update(familyDataset.GetChanges(DataRowState.Added), tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                OracleDbHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                OracleDbHelper.Rollback(tr)
            End If
            Throw ex
        End Try
    End Sub

#End Region

End Class