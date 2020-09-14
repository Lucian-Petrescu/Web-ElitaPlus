Imports System.Collections.Generic

Public Class CaseDAL
    Inherits OracleDALBase

#Region "Constants"
    Private Const SupportChangesFilter As DataRowState = DataRowState.Added Or DataRowState.Modified
    Public Const TableName As String = "ELP_CASE"
    Public Const TableKeyName As String = ColNameCaseId

    Public Const ColNameCaseId As String = "case_id"
    Public Const ColNameCompanyId As String = "company_id"
    Public Const ColNameCompanyDesc As String = "company_desc"
    Public Const ColNameCaseNumber As String = "case_number"
    Public Const ColNameCaseOpenDate As String = "case_open_date"
    Public Const ColNameCasePurposeXcd As String = "case_purpose_xcd"
    Public Const ColNameCasePurposeCode As String = "case_purpose_code"
    Public Const ColNameCaseStatusCode As String = "case_status_code"
    Public Const ColNameInitialInteractionId As String = "initial_interaction_id"
    Public Const ColNameInitialCallerName As String = "initial_caller_name"
    Public Const ColNameClaimId As String = "claim_id"
    Public Const ColNameClaimNumber As String = "claim_number"
    Public Const ColNameCertId As String = "cert_id"
    Public Const ColNameCertNumber As String = "cert_number"
    Public Const ColNameLastActivityDate As String = "last_activity_date"
    Public Const ColNameCaseCloseDate As String = "case_close_date"
    Public Const ColNameCaseCloseCode As String = "case_close_code"

    Public Const ParINameCaseId As String = "pi_case_id"
    Public Const ParINameCompanyId As String = "pi_company_id"
    Public Const ParINameCaseNumber As String = "pi_case_number"
    Public Const ParINameCaseOpenDate As String = "pi_case_open_date"
    Public Const ParINameCasePurposeXcd As String = "pi_case_purpose_xcd"
    Public Const ParINameInitialInteractionId As String = "pi_initial_interaction_id"
    Public Const ParINameClaimId As String = "pi_claim_id"
    Public Const ParINameCertId As String = "pi_cert_id"
    Public Const ParINameLastActivityDate As String = "pi_last_activity_date"
    Public Const ParINameCaseCloseDate As String = "pi_case_close_date"
    Public Const ParINameCaseFieldXcds As String = "pi_case_field_xcds"
    Public Const ParINameCaseFieldValues As String = "pi_case_field_values"


    Public Const PoCursorCase As Integer = 0
    Public Const SpParamNameCaseList As String = "po_case_list"
    Public Const SpParamNameAgentList As String = "po_agent_list"
    Public Const SpParamNameSecFieldsList As String = "po_sec_fields_list"
    Public Const SpParamNameCaseDeniedReasonList As String = "po_case_denied_reasons_list"
    Public Const SpParamNameCaseNotesList As String = "po_case_notes_list"
    Private Const ParONameQuestionSetCode As String = "po_question_set_code"
    Private Const ParONameCaseFieldsList As String = "po_claim_fields_list"
    Public Const SpParamNameAGSearchResultsConfigList As String = "po_search_results_config_list"

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
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD"))
                cmd.AddParameter(TableKeyName, OracleDbType.Raw, id.ToByteArray())
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Fetch(cmd, TableName, familyDs)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Sub LoadCaseByCaseNumber(ByVal familyDs As DataSet, ByVal caseNumber As String, ByVal companyCode As String)
        Try
            Using cmd As OracleCommand = CreateCommand(Config("/SQL/LOAD_CASE_BY_CASE_NUMBER"))
                cmd.AddParameter(ParINameCaseNumber, OracleDbType.Varchar2, caseNumber)
                cmd.AddParameter("pi_company_code", OracleDbType.Varchar2, companyCode)
                cmd.AddParameter(PAR_O_NAME_RESULTCURSOR, OracleDbType.RefCursor, direction:=ParameterDirection.Output)
                Fetch(cmd, TableName, familyDs)
            End Using
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadCaseByClaimId(ByVal claimId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_CASE_BY_CLAIM_ID")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PoCursorCase) As DBHelper.DBHelperParameter
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_claim_id", claimId.ToByteArray())
        inParameters.Add(param)

        outputParameter(PoCursorCase) = New DBHelper.DBHelperParameter("po_ResultCursor", GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "LoadCaseByClaimId")
            ds.Tables(0).TableName = "LoadCaseByClaimId"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadCaseList(ByVal companyId As Guid, ByVal caseNumber As String, ByVal caseStatus As String, ByVal callerFirstName As String, ByVal callerLastName As String, ByVal caseOpenDateFrom As Date?, ByVal caseOpenDateTo As Date?, ByVal casePurpose As String, ByVal certificateNumber As String, ByVal caseClosedReason As String,
                                 ByVal languageId As Guid, ByVal networkId As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_CASE_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PoCursorCase) As DBHelper.DBHelperParameter
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_company_id", companyId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_case_number", caseNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_case_status", caseStatus)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_caller_first_name", callerFirstName)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_caller_last_name", callerLastName)
        inParameters.Add(param)

        If caseOpenDateFrom.HasValue Then
            param = New DBHelper.DBHelperParameter("pi_case_open_date_from", caseOpenDateFrom.Value)
        Else
            param = New DBHelper.DBHelperParameter("pi_case_open_date_from", DBNull.Value)
        End If
        inParameters.Add(param)

        If caseOpenDateTo.HasValue Then
            param = New DBHelper.DBHelperParameter("pi_case_open_date_to", caseOpenDateTo.Value)
        Else
            param = New DBHelper.DBHelperParameter("pi_case_open_date_to", DBNull.Value)
        End If
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_case_purpose", casePurpose)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_certificate_number", certificateNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_case_closed_reason", caseClosedReason)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_language_id", languageId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_network_id", networkId)
        inParameters.Add(param)

        outputParameter(PoCursorCase) = New DBHelper.DBHelperParameter(SpParamNameCaseList, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetCaseList")
            ds.Tables(0).TableName = "GetCaseList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadAgentSearchList(ByVal companyId As Guid, ByVal dealerId As Guid, ByVal customerFirstName As String, ByVal customerLastName As String,
                                        ByVal caseNumber As String, ByVal claimNumber As String, ByVal certificateNumber As String,
                                        ByVal serialNumber As String, ByVal invoiceNumber As String, ByVal phoneNumber As String, ByVal zipcode As String,
                                        ByVal certificateStatus As String, ByVal email As String,
                                        ByVal taxId As String, ByVal serviceLineNumber As String, ByVal accountNumber As String,
                                        ByVal globalCustomerNumber As String, ByVal dateofBirth As String, ByVal networkId As String,
                                        ByVal languageId As Guid,ByVal branchCode As String,ByVal branchName As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_AGENT_SEARCH_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PoCursorCase) As DBHelper.DBHelperParameter
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_company_id", companyId.ToByteArray)
        inParameters.Add(param)

        If dealerId.Equals(Guid.Empty) Then
            param = New DBHelper.DBHelperParameter("pi_dealer_id", DBNull.Value)
            inParameters.Add(param)
        Else
            param = New DBHelper.DBHelperParameter("pi_dealer_id", dealerId.ToByteArray)
            inParameters.Add(param)
        End If

        param = New DBHelper.DBHelperParameter("pi_cert_number", certificateNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_case_number", caseNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_serial_number", serialNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_customer_first_name", customerFirstName)
        inParameters.Add(param)


        param = New DBHelper.DBHelperParameter("pi_customer_last_name", customerLastName)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_phone_number", phoneNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_status_code", certificateStatus)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_invoice_number", invoiceNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_email", email)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_claim_number", claimNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_tax_id", taxId)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_service_line_number", serviceLineNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_account_number", accountNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_global_customer_number", globalCustomerNumber)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_zip_code", zipcode)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_language_id", languageId.ToByteArray)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_dob", dateofBirth)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_network_id", networkId)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_branch_code", branchCode)
        inParameters.Add(param)

        param = New DBHelper.DBHelperParameter("pi_branch_name", branchName)
        inParameters.Add(param)

        outputParameter(PoCursorCase) = New DBHelper.DBHelperParameter("po_cursor", GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetAgentList")
            ds.Tables(0).TableName = "GetAgentList"

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function



    Public Function LoadAgentSearchConfigList(ByVal companyId As Guid, ByVal dealerId As Guid, ByVal searchType As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_AGENT_SEARCH_RESULTS_CONFIG_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PoCursorCase) As DBHelper.DBHelperParameter
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        If companyId.Equals(Guid.Empty) Then
            param = New DBHelper.DBHelperParameter("pi_company_id", DBNull.Value)
            inParameters.Add(param)
        Else
            param = New DBHelper.DBHelperParameter("pi_company_id", companyId.ToByteArray)
            inParameters.Add(param)
        End If

        If dealerId.Equals(Guid.Empty) Then
            param = New DBHelper.DBHelperParameter("pi_dealer_id", DBNull.Value)
            inParameters.Add(param)
        Else
            param = New DBHelper.DBHelperParameter("pi_dealer_id", dealerId.ToByteArray)
            inParameters.Add(param)
        End If

        param = New DBHelper.DBHelperParameter("pi_search_type", searchType)
        inParameters.Add(param)

        outputParameter(PoCursorCase) = New DBHelper.DBHelperParameter(SpParamNameAGSearchResultsConfigList, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "AgentSearchResultsConfigList")
            ds.Tables(0).TableName = "AgentSearchResultsConfigList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function LoadExclSecFieldsList(ByVal companyId As Guid, ByVal dealerId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_EXCL_SEC_FIELDS_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PoCursorCase) As DBHelper.DBHelperParameter
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        If dealerId.Equals(Guid.Empty) Then
            param = New DBHelper.DBHelperParameter("pi_dealer_id", DBNull.Value)
            inParameters.Add(param)
        Else
            param = New DBHelper.DBHelperParameter("pi_dealer_id", dealerId.ToByteArray)
            inParameters.Add(param)
        End If

        outputParameter(PoCursorCase) = New DBHelper.DBHelperParameter(SpParamNameSecFieldsList, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetExclSecFieldsList")
            ds.Tables(0).TableName = "GetExclSecFieldsList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function



    Public Function LoadClaimCaseList(ByVal claimId As Guid, ByVal languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_CLAIM_CASE_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PoCursorCase) As DBHelper.DBHelperParameter
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_claim_id", claimId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_language_id", languageId.ToByteArray)
        inParameters.Add(param)

        outputParameter(PoCursorCase) = New DBHelper.DBHelperParameter(SpParamNameCaseList, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetClaimCaseList")
            ds.Tables(0).TableName = "GetClaimCaseList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadCaseDeniedReasonsList(ByVal caseId As Guid, ByVal languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_CASE_DENIED_REASONS_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PoCursorCase) As DBHelper.DBHelperParameter
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_case_id", caseId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_language_id", languageId.ToByteArray)
        inParameters.Add(param)

        outputParameter(PoCursorCase) = New DBHelper.DBHelperParameter(SpParamNameCaseDeniedReasonList, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetCaseDeniedReasonsList")
            ds.Tables(0).TableName = "GetCaseDeniedReasonsList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadCaseFieldsList(ByVal claimId As Guid, ByVal languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_CASE_FIELDS_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PoCursorCase) As DBHelper.DBHelperParameter
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_claim_id", claimId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_language_id", languageId.ToByteArray)
        inParameters.Add(param)

        outputParameter(PoCursorCase) = New DBHelper.DBHelperParameter(ParONameCaseFieldsList, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetCaseFieldsList")
            ds.Tables(0).TableName = "GetCaseFieldsList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function GetQuestionSetCode(companyGroupId As Guid, companyId As Guid, dealerId As Guid, dealerGroupID As Guid, productCodeId As Guid,
                                       riskTypeId As Guid, deviceTypeId As Guid,
                                       coverageTypeId As Guid, coverageConseqDamageId As Guid, purposeCode As String) As String
        Dim selectStmt As String = Config("/SQL/GET_QUESTION_SET_CODE")
        Dim strQuestionSetCode As String = String.Empty
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PoCursorCase) As DBHelper.DBHelperParameter
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_company_group_id", companyGroupId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_company_id", companyId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_dealer_id", dealerId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_dealer_group_id", dealerGroupID.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_product_code_id", productCodeId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_risk_type_id", riskTypeId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_device_type_id", deviceTypeId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_coverage_type_id", coverageTypeId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_coverage_conseq_damage_id", productCodeId.ToByteArray)
        inParameters.Add(param)
        param = New DBHelper.DBHelperParameter("pi_purpose_code", purposeCode)
        inParameters.Add(param)

        outputParameter(PoCursorCase) = New DBHelper.DBHelperParameter(ParONameQuestionSetCode, GetType(String))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetQuestionSetCode")
            If (Not outputParameter(PoCursorCase) Is Nothing AndAlso Not outputParameter(PoCursorCase).Value Is Nothing) Then
                strQuestionSetCode = outputParameter(PoCursorCase).Value.ToString()
            End If

            Return strQuestionSetCode
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadCaseNotesList(ByVal caseId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_CASE_NOTES_LIST")
        Dim ds As DataSet = New DataSet
        Dim outputParameter(PoCursorCase) As DBHelper.DBHelperParameter
        Dim inParameters As New List(Of DBHelper.DBHelperParameter)
        Dim param As DBHelper.DBHelperParameter

        param = New DBHelper.DBHelperParameter("pi_case_id", caseId.ToByteArray)
        inParameters.Add(param)

        outputParameter(PoCursorCase) = New DBHelper.DBHelperParameter(SpParamNameCaseNotesList, GetType(DataSet))

        Try
            DBHelper.FetchSp(selectStmt, inParameters.ToArray, outputParameter, ds, "GetCaseNotesList")
            ds.Tables(0).TableName = "GetCaseNotesList"
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub UpdateCaseFieldValues(ByVal caseId As Guid, ByRef caseFieldXcds() As String, ByRef caseFieldValues() As String)
        Using command As OracleCommand = CreateCommand("elp_case_utility.UpdateCaseFieldValues", CommandType.StoredProcedure)
            Dim caseFieldXcdsParameter As New OracleParameter
            Dim caseFieldValuesParameter As New OracleParameter

            With caseFieldXcdsParameter
                .ParameterName = ParINameCaseFieldXcds
                .OracleDbType = OracleDbType.Varchar2
                .CollectionType = OracleCollectionType.PLSQLAssociativeArray
                .Value = caseFieldXcds
                .Size = caseFieldXcds.Length
                .Direction = ParameterDirection.Input
            End With

            With caseFieldValuesParameter
                .ParameterName = ParINameCaseFieldValues
                .OracleDbType = OracleDbType.Varchar2
                .CollectionType = OracleCollectionType.PLSQLAssociativeArray
                .Value = caseFieldValues
                .Size = caseFieldValues.Length
                .Direction = ParameterDirection.Input
            End With

            command.BindByName = True
            command.AddParameter(ParINameCaseId, OracleDbType.Raw, 16, caseId, ParameterDirection.Input)
            command.Parameters.Add(caseFieldXcdsParameter)
            command.Parameters.Add(caseFieldValuesParameter)

            Try
                OracleDbHelper.ExecuteNonQuery(command)
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Using
    End Sub

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = SupportChangesFilter)
        If ds Is Nothing Then
            Return
        End If
        If (changesFilter Or (SupportChangesFilter)) <> (SupportChangesFilter) Then
            Throw New NotSupportedException()
        End If
        If Not ds.Tables(TableName) Is Nothing Then
            MyBase.Update(ds.Tables(TableName), Transaction, changesFilter)
        End If
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        Throw New NotSupportedException()
    End Sub
    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(ParINameCaseId, OracleDbType.Raw, sourceColumn:=ColNameCaseId)
            .AddParameter(ParINameCompanyId, OracleDbType.Raw, sourceColumn:=ColNameCompanyId)
            .AddParameter(ParINameCaseNumber, OracleDbType.Varchar2, sourceColumn:=ColNameCaseNumber)
            .AddParameter(ParINameCaseOpenDate, OracleDbType.Date, sourceColumn:=ColNameCaseOpenDate)
            .AddParameter(ParINameCasePurposeXcd, OracleDbType.Varchar2, sourceColumn:=ColNameCasePurposeXcd)
            .AddParameter(ParINameInitialInteractionId, OracleDbType.Raw, sourceColumn:=ColNameInitialInteractionId)
            .AddParameter(ParINameClaimId, OracleDbType.Raw, sourceColumn:=ColNameClaimId)
            .AddParameter(ParINameCertId, OracleDbType.Raw, sourceColumn:=ColNameCertId)
            .AddParameter(ParINameLastActivityDate, OracleDbType.Date, sourceColumn:=ColNameLastActivityDate)
            .AddParameter(ParINameCaseCloseDate, OracleDbType.Date, sourceColumn:=ColNameCaseCloseDate)
            .AddParameter(PAR_I_NAME_CREATED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY)
            .AddParameter(PAR_I_NAME_CREATED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_CREATED_DATE)
        End With

    End Sub

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .AddParameter(ParINameCaseId, OracleDbType.Raw, sourceColumn:=ColNameCaseId)
            .AddParameter(ParINameCompanyId, OracleDbType.Raw, sourceColumn:=ColNameCompanyId)
            .AddParameter(ParINameCaseNumber, OracleDbType.Varchar2, sourceColumn:=ColNameCaseNumber)
            .AddParameter(ParINameCaseOpenDate, OracleDbType.Date, sourceColumn:=ColNameCaseOpenDate)
            .AddParameter(ParINameCasePurposeXcd, OracleDbType.Varchar2, sourceColumn:=ColNameCasePurposeXcd)
            .AddParameter(ParINameInitialInteractionId, OracleDbType.Raw, sourceColumn:=ColNameInitialInteractionId)
            .AddParameter(ParINameClaimId, OracleDbType.Raw, sourceColumn:=ColNameClaimId)
            .AddParameter(ParINameCertId, OracleDbType.Raw, sourceColumn:=ColNameCertId)
            .AddParameter(ParINameLastActivityDate, OracleDbType.Date, sourceColumn:=ColNameLastActivityDate)
            .AddParameter(ParINameCaseCloseDate, OracleDbType.Date, sourceColumn:=ColNameCaseCloseDate)
            .AddParameter(PAR_I_NAME_MODIFIED_BY, OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY)
            .AddParameter(PAR_I_NAME_MODIFIED_DATE, OracleDbType.Date, sourceColumn:=COL_NAME_MODIFIED_DATE)
        End With

    End Sub
#End Region

End Class


