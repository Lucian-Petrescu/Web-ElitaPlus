'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/26/2004)********************
Imports System.Text

Public Class UserDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_USER"
    Public Const TABLE_USERS_SEARCH As String = "users_search"
    Public Const TABLE_COMPANIES_SEARCH As String = "companies_search"
    Public Const TABLE_ROLES_SEARCH As String = "roles_search"
    Public Const TABLE_USERS_ROLES_SEARCH As String = "users_roles_search"
    Public Const TABLE_USERS_COMPANIES_SEARCH As String = "users_companies_search"
    Public Const TABLE_KEY_NAME As String = "user_id"
    Public Const TABLE_USER_DEALERS As String = "user_dealers"
    'Public Const TABLE_USER_SC As String = "user_sc"
    Public Const TABLE_EXTERNAL_USER_SERVICE_CENTER As String = "external_user_service_center"
    Public Const TABLE_USER_ROLES As String = "user_roles"
    Public Const TABLE_USER_COMPANIES As String = "user_companies"
    Public Const TABLE_USER_COUNTRY_COMPANIES As String = "user_country_companies"
    Public Const TABLE_USER_COUNTRIES As String = "user_countries"
    Public Const TABLE_USER_COMPANY_GROUPS As String = "user_company_groups"
    Public Const TABLE_USER_COMPANY_ASSIGNED As String = "elp_user_company_assigned"
    Public Const TABLE_USER_SP_USER_CLAIMS As String = "elp_sp_user_claims"

    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const COL_NAME_COMPANY_CODE As String = "code"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_NETWORK_ID As String = "network_id"
    Public Const COL_NAME_USER_NAME As String = "user_name"

    '  Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_ACTIVE As String = "active"
    Public Const COL_NAME_PASSWORD As String = "password"
    Public Const COL_NAME_ROLE_CODE As String = "code"
    Public Const COL_NAME_ROLE_SEARCH_CODE As String = "user_roles"
    Public Const COL_NAME_COMPANY_SEARCH_CODE As String = "user_companies"
    Public Const COL_NAME_EXTERNAL As String = "external"
    Public Const COL_NAME_EXTERNAL_TYPE_ID As String = "external_type_id"
    Public Const COL_NAME_SC_DEALER_ID As String = "sc_dealer_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_ID1 As String = "id1"
    Public Const COL_NAME_ID2 As String = "id2"
    Public Const COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_DEALER_SERVICE_CENTER_ID As String = "id"
    Public Const COL_NAME_PERMISSION_TYPE_CODE As String = "permission_type_code"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "CRUD Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("user_id", id.ToByteArray)}
        DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
    End Sub

    Public Sub LoadByNetworkId(familyDS As DataSet, networkId As String)
        Dim selectStmt As String = Config("/SQL/LOAD_BY_NETWORK_ID")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("network_id", networkId.ToUpper)}
        DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
    End Sub

    Private Sub CreateRelation(dsUsers As DataSet, dsRoles As DataSet, dsCompanies As DataSet)

        'put the 3 data tables into one dataset
        Dim oTableRoleToAdd As DataTable
        Dim oTableCompaniesToAdd As DataTable

        'set the primary key column so that the child function can use the find method.
        Dim oPrimaryKeyColumn(0) As DataColumn
        dsUsers.Tables(0).TableName = TABLE_USERS_SEARCH

        oPrimaryKeyColumn(0) = dsUsers.Tables(TABLE_USERS_SEARCH).Columns("NETWORK_ID")

        dsUsers.Tables(TABLE_USERS_SEARCH).PrimaryKey = oPrimaryKeyColumn

        'a copy is needed to avoid a "dataset table belongs to other table error"
        oTableRoleToAdd = dsRoles.Tables(0).Copy
        oTableCompaniesToAdd = dsCompanies.Tables(0).Copy


        oTableRoleToAdd.TableName = TABLE_ROLES_SEARCH
        dsUsers.Tables.Add(oTableRoleToAdd)

        oTableCompaniesToAdd.TableName = TABLE_COMPANIES_SEARCH
        dsUsers.Tables.Add(oTableCompaniesToAdd)

        Dim parentCol As DataColumn
        Dim childCol As DataColumn
        Dim childCompCol As DataColumn

        parentCol = dsUsers.Tables(TABLE_USERS_SEARCH).Columns("NETWORK_ID")
        childCol = dsUsers.Tables(TABLE_ROLES_SEARCH).Columns("NETWORK_ID")
        childCompCol = dsUsers.Tables(TABLE_COMPANIES_SEARCH).Columns("C_NETWORK_ID")

        ' Create DataRelation.
        Dim relUserToRoles As DataRelation
        Dim relCompanyToRoles As DataRelation
        relUserToRoles = New DataRelation(TABLE_USERS_ROLES_SEARCH, parentCol, childCol)
        relCompanyToRoles = New DataRelation(TABLE_USERS_COMPANIES_SEARCH, parentCol, childCompCol)

        ' Add the relation to the DataSet.
        dsUsers.Relations.Add(relUserToRoles)
        dsUsers.Relations.Add(relCompanyToRoles)

    End Sub

    Private Function GetUserRolesAsString(oDSUsers As DataSet, sNetworkID As String) As String

        Dim SingleRow As DataRow = oDSUsers.Tables(TABLE_USERS_SEARCH).Rows.Find(sNetworkID)
        Dim sResultString As String
        Dim oStringBuilder As New StringBuilder

        Dim oRows As DataRow() = SingleRow.GetChildRows(TABLE_USERS_ROLES_SEARCH)

        Dim oSingleRow As DataRow

        For Each oSingleRow In oRows
            oStringBuilder.Append(oSingleRow.Item("CODE").ToString & ",")
        Next

        sResultString = oStringBuilder.ToString

        'remove the last comma.
        If sResultString.Length > 2 Then
            Return sResultString.Remove(sResultString.Length - 1, 1)
        Else
            Return sResultString
        End If

        Return oStringBuilder.ToString

    End Function

    Private Function GetUserCompaniesAsString(oDSUsers As DataSet, sNetworkID As String) As String

        Dim SingleRow As DataRow = oDSUsers.Tables(TABLE_USERS_SEARCH).Rows.Find(sNetworkID)
        Dim sResultString As String
        Dim oStringBuilder As New StringBuilder

        Dim oRows As DataRow() = SingleRow.GetChildRows(TABLE_USERS_COMPANIES_SEARCH)

        Dim oSingleRow As DataRow

        For Each oSingleRow In oRows
            oStringBuilder.Append(oSingleRow.Item("CODE").ToString & ",")
        Next

        sResultString = oStringBuilder.ToString

        'remove the last comma.
        If sResultString.Length > 2 Then
            Return sResultString.Remove(sResultString.Length - 1, 1)
        Else
            Return sResultString
        End If

        Return oStringBuilder.ToString

    End Function

    Private Function AddUserRoleValues(DSUsers As DataSet) As String


        Dim oSingleRow As DataRow
        Dim sNetworkID As String
        Dim sRoles As String

        Dim oColumn As New DataColumn("USER_ROLES", GetType(String))
        DSUsers.Tables(0).Columns.Add(oColumn)


        For Each oSingleRow In DSUsers.Tables(0).Rows

            sNetworkID = oSingleRow.Item("NETWORK_ID").ToString


            With oSingleRow

                .BeginEdit()
                .Item("USER_ROLES") = GetUserRolesAsString(DSUsers, sNetworkID)
                .EndEdit()

            End With

        Next

    End Function

    Private Function AddUserCompaniesValues(DSUsers As DataSet) As String


        Dim oSingleRow As DataRow
        Dim sNetworkID As String
        Dim sRoles As String

        Dim oColumn As New DataColumn("USER_COMPANIES", GetType(String))
        DSUsers.Tables(0).Columns.Add(oColumn)


        For Each oSingleRow In DSUsers.Tables(0).Rows

            sNetworkID = oSingleRow.Item("NETWORK_ID").ToString


            With oSingleRow

                .BeginEdit()
                .Item("USER_COMPANIES") = GetUserCompaniesAsString(DSUsers, sNetworkID)
                .EndEdit()

            End With

        Next

    End Function

    Public Function LoadList() As DataSet
        Dim DSUsers, dsRoles, dsCompanies As DataSet
        Dim SQLUsers As String = Config("/SQL/MAINTAIN_USER_GET_USERS")
        Dim SQLRoles As String = Config("/SQL/MAINTAIN_USER_ROLE_FETCH")
        Dim SQLCompanies As String = Config("/SQL/MAINTAIN_USER_COMPANY_FETCH")


        DSUsers = DBHelper.Fetch(SQLUsers, TABLE_USERS_SEARCH)
        dsRoles = DBHelper.Fetch(SQLRoles, TABLE_ROLES_SEARCH)
        dsCompanies = DBHelper.Fetch(SQLCompanies, TABLE_COMPANIES_SEARCH)
        CreateRelation(DSUsers, dsRoles, dsCompanies)
        AddUserRoleValues(DSUsers)
        AddUserCompaniesValues(DSUsers)

        Return DSUsers
    End Function

    Public Function LoadUserList(networkID As String, userName As String, _
                               role As String, companyCode As String) As DataSet

        Dim DSUsers, dsRoles, dsCompanies As DataSet
        Dim SQLUsers As String = Config("/SQL/MAINTAIN_USER_GET_USERS")
        Dim SQLRoles As String = Config("/SQL/MAINTAIN_USER_ROLE_FETCH")
        Dim SQLCompanies As String = Config("/SQL/MAINTAIN_USER_COMPANY_FETCH")

        Dim whereClauseConditions_Users As String = ""
        Dim whereClauseConditions_Roles As String = ""
        Dim whereClauseConditions_Companies As String = ""

        If FormatSearchMask(networkID) Then
            whereClauseConditions_Users &= " WHERE " & Environment.NewLine & COL_NAME_NETWORK_ID & " " & networkID
            whereClauseConditions_Roles &= " AND " & Environment.NewLine & "UPPER(u." & COL_NAME_NETWORK_ID & ") " & networkID
            whereClauseConditions_Companies &= " AND " & Environment.NewLine & "UPPER(u." & COL_NAME_NETWORK_ID & ") " & networkID
        End If

        If FormatSearchMask(userName) Then
            If whereClauseConditions_Users = "" Then
                whereClauseConditions_Users &= " WHERE " & Environment.NewLine & "UPPER(" & COL_NAME_USER_NAME & ") " & userName
            Else
                whereClauseConditions_Users &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_USER_NAME & ") " & userName
            End If
            whereClauseConditions_Roles &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_USER_NAME & ") " & userName
            whereClauseConditions_Companies &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_USER_NAME & ") " & userName
        End If

        'If Me.FormatSearchMask(role) Then
        '    whereClauseConditions_Roles &= " AND " & Environment.NewLine & "UPPER(r." & Me.COL_NAME_ROLE_CODE & ") " & role
        'End If

        'If Me.FormatSearchMask(companyCode) Then
        'whereClauseConditions_Companies &= " AND " & Environment.NewLine & "UPPER(c." & Me.COL_NAME_COMPANY_CODE & ") " & companyCode
        'End If

        ' not HextoRaw
        'whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & Me.COL_NAME_COMPANY_ID, compIds, False)

        If Not whereClauseConditions_Users = "" Then
            SQLUsers = SQLUsers.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions_Users)
        Else
            SQLUsers = SQLUsers.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not whereClauseConditions_Roles = "" Then
            SQLRoles = SQLRoles.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions_Roles)
        Else
            SQLRoles = SQLRoles.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not whereClauseConditions_Companies = "" Then
            SQLCompanies = SQLCompanies.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions_Companies)
        Else
            SQLCompanies = SQLCompanies.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        'If Not IsNothing(sortBy) Then
        '    selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
        '                                    Environment.NewLine & "ORDER BY " & Environment.NewLine & sortBy)
        'Else
        '    selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "")
        'End If

        Try
            Dim ds As New DataSet
            ''Dim compIdPar As New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY_ID, compId)
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_NUMBER_OF_ROWS)

            'DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, _
            '                New DBHelper.DBHelperParameter() {rowNum})
            'Return ds

            DSUsers = DBHelper.Fetch(SQLUsers, TABLE_USERS_SEARCH)
            dsRoles = DBHelper.Fetch(SQLRoles, TABLE_ROLES_SEARCH)
            dsCompanies = DBHelper.Fetch(SQLCompanies, TABLE_COMPANIES_SEARCH)
            CreateRelation(DSUsers, dsRoles, dsCompanies)
            AddUserRoleValues(DSUsers)
            AddUserCompaniesValues(DSUsers)

            Return DSUsers
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim oUserRoleDal As New UserRoleDAL
        Dim oUserCompanyAssignedDal As New UserCompanyAssignedDAL
        Dim oUserCompanyDal As New UserCompanyDAL
        Dim oUserPermissionDal As New UserPermissionDAL
        Dim oSpUserClaimsDAL As New SpUserClaimsDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            oUserRoleDal.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            oUserCompanyAssignedDal.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            oUserCompanyDal.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            oUserPermissionDal.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            oSpUserClaimsDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            MyBase.Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            oUserRoleDal.Update(familyDataset.GetChanges(DataRowState.Added), tr, DataRowState.Added Or DataRowState.Modified)
            oUserCompanyAssignedDal.Update(familyDataset.GetChanges(DataRowState.Added), tr, DataRowState.Added Or DataRowState.Modified)
            oUserCompanyDal.Update(familyDataset.GetChanges(DataRowState.Added), tr, DataRowState.Added Or DataRowState.Modified)
            oUserPermissionDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            oSpUserClaimsDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
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

    Public Overloads Sub Update(ds As DataSet)
        Dim conn As OracleConnection
        Dim transaction As OracleTransaction
        Try
            conn = New OracleConnection(DBHelper.ConnectString)
            conn.Open()
            transaction = conn.BeginTransaction
            Update(ds, transaction)
            transaction.Commit()
        Catch ex As Exception
            transaction.Rollback()
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Public Overloads Sub Update(ds As DataSet, transaction As OracleTransaction)
        Dim da As OracleDataAdapter = configureDataAdapter(ds, transaction)
        da.Update(ds.Tables(TABLE_NAME))
    End Sub

    Protected Function configureDataAdapter(ds As DataSet, transaction As OracleTransaction) As OracleDataAdapter
        Dim da As New OracleDataAdapter
        'associate commands to data adapter

        da.UpdateCommand = New OracleCommand(Config("/SQL/UPDATE"), transaction.Connection)
        AddCommonParameters(da.UpdateCommand)
        AddUpdateAuditParameters(da.UpdateCommand)
        AddWhereParameters(da.UpdateCommand)

        da.InsertCommand = New OracleCommand(Config("/SQL/INSERT"), transaction.Connection)
        AddCommonParameters(da.InsertCommand)
        AddInsertAuditParameters(da.InsertCommand)
        da.InsertCommand.Parameters.Add("user_id", OracleDbType.Raw, 16, "user_id")

        da.DeleteCommand = New OracleCommand(Config("/SQL/DELETE"), transaction.Connection)
        AddWhereParameters(da.DeleteCommand)
        Return da
    End Function

    Protected Sub AddCommonParameters(cmd As OracleCommand)
        cmd.Parameters.Add("network_id", OracleDbType.Varchar2, 8, "network_id")
        cmd.Parameters.Add("user_name", OracleDbType.Varchar2, 70, "user_name")
        cmd.Parameters.Add("authorization_limit", OracleDbType.Decimal, 0, "authorization_limit")
        ' cmd.Parameters.Add("company_id", OracleDbType.Raw, 16, "company_id")
        cmd.Parameters.Add("language_id", OracleDbType.Raw, 16, "language_id")
        cmd.Parameters.Add("active", OracleDbType.Varchar2, 1, "active")
        cmd.Parameters.Add("password", OracleDbType.Varchar2, 20, "password")
        cmd.Parameters.Add("external", OracleDbType.Varchar2, 1, "external")
        cmd.Parameters.Add("external_type_id", OracleDbType.Raw, 16, "external_type_id")
        cmd.Parameters.Add("sc_dealer_id", OracleDbType.Raw, 16, "sc_dealer_id")
    End Sub

    Protected Sub AddWhereParameters(cmd As OracleCommand)
        cmd.Parameters.Add("user_id", OracleDbType.Raw, 16, "user_id")
    End Sub

    Protected Sub AddUpdateAuditParameters(cmd As OracleCommand)
        cmd.Parameters.Add("modified_by", OracleDbType.Varchar2, 30, "modified_by")
    End Sub

    Protected Sub AddInsertAuditParameters(cmd As OracleCommand)
        cmd.Parameters.Add("created_by", OracleDbType.Varchar2, 30, "created_by")
    End Sub

#End Region

#Region "User Methods"

    Public Function LoadUserRoles(userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_USER_ROLES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_ROLES, parameters)
    End Function

    Public Function LoadUserIHQRoles(userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_USER_IHQ_ROLES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_ROLES, parameters)
    End Function

    Public Function LoadAvailableRoles(userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_AVAILABLE_ROLES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_ROLES, parameters)
    End Function

    Public Function LoadUserCompanyAssigned(grpId As Guid, userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_USER_COMPANY_ASSIGNED")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray), _
                                            New OracleParameter(COMPANY_GROUP_ID, grpId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COMPANY_ASSIGNED, parameters)
    End Function

    Public Function LoadAvailableCompanies(grpId As Guid, userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_AVAILABLE_COMPANIES")

        parameters = New OracleParameter() {New OracleParameter(COMPANY_GROUP_ID, grpId.ToByteArray), _
                                            New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray), _
                                            New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray), _
                                            New OracleParameter(COMPANY_GROUP_ID, grpId.ToByteArray), _
                                            New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray), _
                                            New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COMPANIES, parameters)
    End Function

    Public Function LoadAvailableAssignedCompanies(userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_AVAILABLE_COMPANIES_ASSIGNED")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COMPANIES, parameters)
    End Function

    Public Function LoadSelectedAssignedCompanies(userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_SELECTED_COMPANIES_ASSIGNED")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COMPANY_ASSIGNED, parameters)
    End Function

    Public Function LoadAvailableCompanyGroup(userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_AVAILABLE_COMPANY_GROUPS")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COMPANY_GROUPS, parameters)
    End Function

    Public Function LoadSelectedCompanies(grpId As Guid, userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_SELECTED_COMPANIES")

        parameters = New OracleParameter() {New OracleParameter(COMPANY_GROUP_ID, grpId.ToByteArray), _
                                            New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COMPANIES, parameters)
    End Function

    Public Function LoadUserCompanies(userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_USER_COMPANIES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COMPANIES, parameters)
    End Function

    Public Function LoadUserCompanies(userId As Guid, oCountryId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_USER_COUNTRY_COMPANIES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray),
                                            New OracleParameter(COL_NAME_COUNTRY_ID, oCountryId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COUNTRY_COMPANIES, parameters)
    End Function
    Public Function LoadUsersBasedOnPermission(country_id As Guid, permission_type_code As String) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_USERS_BASED_ON_PERMISSION")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COUNTRY_ID, country_id.ToByteArray),
                                            New OracleParameter(COL_NAME_PERMISSION_TYPE_CODE, permission_type_code)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COMPANIES, parameters)
    End Function

    Public Function LoadUsersBasedOnPermission(user_id As Guid, company_id As Guid, permission_type_code As String) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_USER_BASED_ON_INC_PERMISSION")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMPANY_ID, company_id.ToByteArray),
                                            New OracleParameter(COL_NAME_USER_ID, user_id.ToByteArray),
                                            New OracleParameter(COL_NAME_PERMISSION_TYPE_CODE, permission_type_code)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COMPANIES, parameters)
    End Function

    Public Function LoadUserCountries(userId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_USER_COUNTRIES")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_USER_ID, userId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COMPANIES, parameters)
    End Function



    Public Function LoadExternalUserServiceCenters(oServiceCenterId As Guid) As DataSet
        Dim ds As New DataSet
        Dim inputParameters(1) As DBHelper.DBHelperParameter
        Dim selectStmt As String = Config("/SQL/GET_EXTERNAL_USER_SERVICE_CENTERS")

        inputParameters(0) = New DBHelper.DBHelperParameter(UserDAL.COL_NAME_SERVICE_CENTER_ID, oServiceCenterId.ToByteArray)
        inputParameters(1) = New DBHelper.DBHelperParameter(UserDAL.COL_NAME_SERVICE_CENTER_ID, oServiceCenterId.ToByteArray)
        Return DBHelper.Fetch(ds, selectStmt, TABLE_EXTERNAL_USER_SERVICE_CENTER, inputParameters)
    End Function

    Public Sub UpdateUserCompanies(oUserId As Guid, oDataset As DataSet)
        Dim tr As IDbTransaction = DBHelper.GetNewTransaction
        Dim deleteStmt As String = Config("/SQL/DELETE_USER_COMPANIES")
        Dim inputParameters(0) As DBHelper.DBHelperParameter
        Dim oUserCompanyDAL As New UserCompanyDAL

        Try
            ' Delete User Companies
            inputParameters(0) = New DBHelper.DBHelperParameter(UserDAL.COL_NAME_USER_ID, oUserId.ToByteArray)
            DBHelper.Execute(deleteStmt, inputParameters, tr)

            ' Insert User Companies
            oUserCompanyDAL.Update(oDataset, tr, DataRowState.Added Or DataRowState.Modified)

            'We are the creator of the transaction we shoul commit it  and close the connection
            DBHelper.Commit(tr)
        Catch ex As Exception
            'We are the creator of the transaction we shoul commit it  and close the connection
            DBHelper.RollBack(tr)

            Throw ex
        End Try
    End Sub

    Public Function LoadUserOtherCompaniesIDs(UserFirstCompany_id As Guid, companyGroup_id As Guid) As DataSet
        ' Since the Active user companies may not have all the companies of the user's company_group,
        ' this method is intended to provide the companies (IDs only) within the company group of the active user.
        ' The active user first company will be excluded.
        ' This fuction is being used by the AccountingCloseDates screen.
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_USER_OTHER_COMPANIES_IDs")
        parameters = New OracleParameter() {New OracleParameter(COMPANY_GROUP_ID, companyGroup_id.ToByteArray),
                                            New OracleParameter(COL_NAME_COMPANY_ID, UserFirstCompany_id.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_USER_COMPANIES, parameters)
    End Function
    Public Function LoadSpUserClaims(userId As Guid, languageId As Guid, SpClaimCode As String) As DataSet
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter
        Dim selectStmt As String = Config("/SQL/GET_SP_USER_CLAIMS")

        parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(SpClaimTypesDAL.COL_NAME_CODE, SpClaimCode),
                                                       New DBHelper.DBHelperParameter("lang_id", languageId.ToByteArray),
                                                       New DBHelper.DBHelperParameter(COL_NAME_USER_ID, userId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_USER_SP_USER_CLAIMS, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region

    #Region "User Claims Validations"

    Public Function IsDealerValidForUserClaim(userId As Guid, dealerCode As String) As Boolean
        Dim selectStmt As String = Config("/SQL/CHECK_DEALER_FOR_USER_CLAIM")
       
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("user_id", userId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("dealer", dealerCode)}
        Dim count As Integer
        
        Try
            count = DBHelper.ExecuteScalar(selectStmt, parameters)
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

     Public Function IsDealerGroupValidForUserClaim(userId As Guid, dealerGroupCode As String) As Boolean
        Dim selectStmt As String = Config("/SQL/CHECK_DEALER_GROUP_FOR_USER_CLAIM")
       
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("user_id", userId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("dealer_group_code", dealerGroupCode)}
        Dim count As Integer
        
        Try
            count = DBHelper.ExecuteScalar(selectStmt, parameters)
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function IsCompanyGroupValidForUserClaim(userId As Guid, companyGroupCode As String) As Boolean
        Dim selectStmt As String = Config("/SQL/CHECK_COMPANY_GROUP_FOR_USER_CLAIM")
       
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("user_id", userId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("company_group_code", companyGroupCode)}
        Dim count As Integer
        
        Try
            count = DBHelper.ExecuteScalar(selectStmt, parameters)
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function IsServiceCenterValidForUserClaim(userId As Guid, serviceCenterCode As String, countryCode As string) As Boolean
        Dim selectStmt As String = Config("/SQL/CHECK_SERVICE_CENTER_FOR_USER_CLAIM")
       
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("user_id", userId.ToByteArray),
                                                                                           New DBHelper.DBHelperParameter("country_code", countryCode),
                                                                                           New DBHelper.DBHelperParameter("service_center_code", serviceCenterCode)}
        Dim count As Integer
        
        Try
            count = DBHelper.ExecuteScalar(selectStmt, parameters)
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    
#End Region
End Class



