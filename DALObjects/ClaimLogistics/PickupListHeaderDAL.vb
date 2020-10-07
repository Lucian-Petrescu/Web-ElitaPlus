'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/29/2008)********************
Imports System.Globalization

Public Class PickupListHeaderDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PICKUP_LIST_HEADER"
    Public Const TABLE_KEY_NAME As String = "header_id"

    Public Const COL_NAME_HEADER_ID As String = "header_id"
    Public Const COL_NAME_PICKUP_TYPE As String = "pickup_type"
    Public Const COL_NAME_PICKLIST_NUMBER As String = "picklist_number"
    Public Const COL_NAME_ROUTE_ID As String = "route_id"
    Public Const COL_NAME_HEADER_STATUS_ID As String = "header_status_id"

    Public Const TABLE_NAME_NEW_OPEN_CLAIM As String = "NEW_OPEN_CLAIMS"
    Public Const TABLE_NAME_STORE_SC_RELATIONS As String = "STORE_SC_RELATIONS"
    Public Const TABLE_NAME_SC_CLAIM_RELATIONS As String = "SC_CLAIM_RELATIONS"
    Public Const TABLE_NAME_PICKLIST_STORE_RELATIONS As String = "PICKLIST_STORE_RELATIONS"
    Public Const TABLE_NAME_PICKLIST As String = "PICKLIST"
    Public Const TABLE_NAME_STORE As String = "STORE"
    Public Const TABLE_NAME_SERVICE_CENTER As String = "SERVICE_CENTER"
    Public Const TABLE_NAME_CLAIM_DETAIL As String = "CLAIM"
    Public Const TABLE_NAME_CLAIM_STATUS_HISTORY As String = "CLAIM_STATUS_HISTORY"
    Public Const COL_NAME_STORE_SERVICE_CENTER_ID As String = "store_service_center_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const TABLE_NAME_CLAIM_BY_PICKLIST As String = "CLAIM_BY_PICKLIST"

    Public Const TOTAL_OUTPUT_PARAM_INFO As Integer = 1

    Public Const TOTAL_INPUT_PARAM_IITEMS As Integer = 0
    Public Const TOTAL_OUTPUT_PARAM_ITEMS As Integer = 2
    Public Const SP_PARAM_NAME_P_USER_ID As String = "p_user_id"
    Public Const SP_PARAM_NAME_P_PICK_LIST_NUMBER As String = "p_pick_list_number"
    Public Const SP_PARAM_NAME_P_PICKUP_BY As String = "p_pickup_by"
    Public Const SP_PARAM_NAME_P_RETURN As String = "p_return"
    Public Const SP_PARAM_NAME_P_EXCEPTION_MSG As String = "p_exception_msg"

    Public Const P_RETURN = 0
    Public Const P_EXCEPTION_MSG = 1

    Public Const TABLE_NAME_UPDATE_PICK_LIST_STATUS_INFO As String = "UPDATE_PICK_LIST_STATUS"
    Public Const TABLE_NAME_UPDATE_PICK_LIST_STATUS As String = "UPDATE_PICK_LIST_STATUS"

    Public Const SP_PARAM_NAME_P_CLAIMS As String = "p_claims"
    Public Const TABLE_NAME_UPDATE_PICK_LIST_STATUS_RECEIVED As String = "UPDATE_PICK_LIST_STATUS_RECEIVED"
    Public Const SP_PARAM_NAME_P_COMPANY_GROUP_ID As String = "p_company_group_id"
    Public Const SP_PARAM_NAME_P_SERVICE_CENTER_ID As String = "p_service_center_id"

    Public Const TABLE_NAME_READY_FROM_SC_PICKUP As String = "READY_FROM_SC_PICKUP"

    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_START_DATE As String = "start_date"
    Public Const COL_NAME_END_DATE As String = "end_date"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("header_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function


#End Region


#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Function GetClaimIDByCode(compIds As ArrayList, claimNumber As String, certItemCoverageCode As String) As DataSet

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/GET_CLAIM_ID_BY_CODE")
        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("claim." & ClaimDAL.COL_NAME_COMPANY_ID, compIds, False)
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        parameters = New OracleParameter() {New OracleParameter(ClaimDAL.COL_NAME_CLAIM_NUMBER, claimNumber), _
                                            New OracleParameter("code", certItemCoverageCode)}

        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

    End Function

    Public Function GetNewOpenClaims(routeId As Guid, companies As ArrayList) As DataSet
        Dim ds As New DataSet(TABLE_NAME_NEW_OPEN_CLAIM)
        Dim doNotCatch As Boolean = False

        Try
            ds = GetNewOpenClaimPickList(ds, routeId, companies)
            If ds.Tables.Count > 0 AndAlso Not ds.Tables(TABLE_NAME_PICKLIST) Is Nothing AndAlso ds.Tables(TABLE_NAME_PICKLIST).Rows.Count > 1 Then
                doNotCatch = True
                Throw New ElitaPlusException("GetNewOpenClaims ", ErrorCodes.MORE_THAN_ONE_PICKLIST_FOUND)
            End If

            ds = GetNewOpenClaimStoresByRoute(ds, routeId, companies)
            ds = GetNewOpenClaimServiceCentersByRoute(ds, routeId, companies)
            ds = GetNewOpenClaimDetailByRoute(ds, routeId, companies)

            Dim picklistToStoreRel As New DataRelation(TABLE_NAME_PICKLIST_STORE_RELATIONS, _
                                                 ds.Tables(TABLE_NAME_PICKLIST).Columns(COL_NAME_ROUTE_ID), _
                                                 ds.Tables(TABLE_NAME_STORE).Columns(COL_NAME_ROUTE_ID))
            picklistToStoreRel.Nested = True
            ds.Relations.Add(picklistToStoreRel)

            Dim storeToSCRel As New DataRelation(TABLE_NAME_STORE_SC_RELATIONS, _
                                                 ds.Tables(TABLE_NAME_STORE).Columns(COL_NAME_STORE_SERVICE_CENTER_ID), _
                                                 ds.Tables(TABLE_NAME_SERVICE_CENTER).Columns(COL_NAME_STORE_SERVICE_CENTER_ID))
            storeToSCRel.Nested = True
            ds.Relations.Add(storeToSCRel)

            Dim parentCols() As DataColumn = New DataColumn() _
                            {ds.Tables(TABLE_NAME_SERVICE_CENTER).Columns(COL_NAME_STORE_SERVICE_CENTER_ID), _
                             ds.Tables(TABLE_NAME_SERVICE_CENTER).Columns(COL_NAME_SERVICE_CENTER_ID)}

            Dim childCols() As DataColumn = New DataColumn() _
                {ds.Tables(TABLE_NAME_CLAIM_DETAIL).Columns(COL_NAME_STORE_SERVICE_CENTER_ID), _
                 ds.Tables(TABLE_NAME_CLAIM_DETAIL).Columns(COL_NAME_SERVICE_CENTER_ID)}

            Dim SCToClaimRel As New DataRelation(TABLE_NAME_SC_CLAIM_RELATIONS, parentCols, childCols)

            SCToClaimRel.Nested = True
            ds.Relations.Add(SCToClaimRel)

            Return ds
        Catch ex As Exception
            If doNotCatch Then
                Throw ex
            Else
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End If
        End Try

    End Function

    Public Function GetNewOpenClaimPickList(ds As DataSet, routeId As Guid, companies As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_NEW_OPEN_CLAIM_PICKLIST")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND route.route_id = " & MiscUtil.GetDbStringFromGuid(routeId, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_PICKLIST, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetNewOpenClaimStoresByRoute(ds As DataSet, routeId As Guid, companies As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_NEW_OPEN_CLAIM_STORE")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND route.route_id = " & MiscUtil.GetDbStringFromGuid(routeId, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_STORE, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetNewOpenClaimServiceCentersByRoute(ds As DataSet, routeId As Guid, companies As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_NEW_OPEN_CLAIM_SC")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND route.route_id = " & MiscUtil.GetDbStringFromGuid(routeId, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_SERVICE_CENTER, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetNewOpenClaimDetailByRoute(ds As DataSet, routeId As Guid, companies As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_NEW_OPEN_CLAIM_DETAIL")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND route.route_id = " & MiscUtil.GetDbStringFromGuid(routeId, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_CLAIM_DETAIL, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function UpdatePickListStatus(PickListNumber As String, pickupBy As String, userId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/UPDATE_PICK_LIST_STATUS_TO_IN_PICK")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_INFO) As DBHelper.DBHelperParameter



        inputParameters = New DBHelper.DBHelperParameter() _
                {SetParameter(SP_PARAM_NAME_P_USER_ID, userId.ToByteArray), _
                 SetParameter(SP_PARAM_NAME_P_PICK_LIST_NUMBER, PickListNumber), _
                 SetParameter(SP_PARAM_NAME_P_PICKUP_BY, pickupBy)}


        outputParameter(P_RETURN) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_RETURN, GetType(Integer))
        outputParameter(P_EXCEPTION_MSG) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_EXCEPTION_MSG, GetType(String), 50)

        Dim ds As New DataSet()
        ' Call DBHelper Store Procedure
        DBHelper.FetchSp(selectStmt, inputParameters, outputParameter, ds, TABLE_NAME_UPDATE_PICK_LIST_STATUS)

        If outputParameter(P_RETURN).Value <> 0 Then
            Throw New StoredProcedureGeneratedException("Update Pick List Generated Error: ", outputParameter(P_EXCEPTION_MSG).Value)
        Else
            Return ds
        End If

    End Function

    Public Function UpdatePickListStatus_Received(PickListNumber As String, ServiceCenterID As Guid, claimStr As String, userId As Guid, companyGroupId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/UPDATE_PICK_LIST_STATUS_TO_RECEIVED")
        Dim inputParameters() As DBHelper.DBHelperParameter
        Dim outputParameter(TOTAL_OUTPUT_PARAM_INFO) As DBHelper.DBHelperParameter



        inputParameters = New DBHelper.DBHelperParameter() _
                {SetParameter(SP_PARAM_NAME_P_USER_ID, userId.ToByteArray), _
                 SetParameter(SP_PARAM_NAME_P_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                 SetParameter(SP_PARAM_NAME_P_SERVICE_CENTER_ID, ServiceCenterID.ToByteArray), _
                 SetParameter(SP_PARAM_NAME_P_PICK_LIST_NUMBER, PickListNumber), _
                 SetParameter(SP_PARAM_NAME_P_CLAIMS, claimStr)}


        outputParameter(P_RETURN) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_RETURN, GetType(Integer))
        outputParameter(P_EXCEPTION_MSG) = New DBHelper.DBHelperParameter(SP_PARAM_NAME_P_EXCEPTION_MSG, GetType(String), 50)

        Dim ds As New DataSet()
        ' Call DBHelper Store Procedure
        DBHelper.FetchSp(selectStmt, inputParameters, outputParameter, ds, TABLE_NAME_UPDATE_PICK_LIST_STATUS_RECEIVED)

        If outputParameter(P_RETURN).Value <> 0 Then
            Throw New StoredProcedureGeneratedException("Service Center Received Items Generated Error: ", outputParameter(P_EXCEPTION_MSG).Value)
        Else
            Return ds
        End If

    End Function

    Function SetParameter(name As String, value As Object) As DBHelper.DBHelperParameter

        name = name.Trim
        If value Is Nothing Then value = DBNull.Value
        If value.GetType Is GetType(String) Then value = DirectCast(value, String).Trim

        Return New DBHelper.DBHelperParameter(name, value)

    End Function

    Public Function GetClaimsReadyFromSC(routeId As Guid, companies As ArrayList, languageId As Guid) As DataSet
        Dim ds As New DataSet(TABLE_NAME_READY_FROM_SC_PICKUP)

        Try
            ds = GetClaimsReadyFromSCPickList(ds, routeId, companies)
            ds = GetClaimsReadyFromSCStoresByRoute(ds, routeId, companies)
            ds = GetClaimsReadyFromSCServiceCentersByRoute(ds, routeId, companies)
            ds = GetClaimsReadyFromSCDetailByRoute(ds, routeId, companies, languageId)

            Dim picklistToStoreRel As New DataRelation(TABLE_NAME_PICKLIST_STORE_RELATIONS, _
                                                 ds.Tables(TABLE_NAME_PICKLIST).Columns(COL_NAME_ROUTE_ID), _
                                                 ds.Tables(TABLE_NAME_STORE).Columns(COL_NAME_ROUTE_ID))
            picklistToStoreRel.Nested = True
            ds.Relations.Add(picklistToStoreRel)

            Dim storeToSCRel As New DataRelation(TABLE_NAME_STORE_SC_RELATIONS, _
                                                 ds.Tables(TABLE_NAME_STORE).Columns(COL_NAME_STORE_SERVICE_CENTER_ID), _
                                                 ds.Tables(TABLE_NAME_SERVICE_CENTER).Columns(COL_NAME_STORE_SERVICE_CENTER_ID))
            storeToSCRel.Nested = True
            ds.Relations.Add(storeToSCRel)

            Dim parentCols() As DataColumn = New DataColumn() _
                            {ds.Tables(TABLE_NAME_SERVICE_CENTER).Columns(COL_NAME_STORE_SERVICE_CENTER_ID), _
                             ds.Tables(TABLE_NAME_SERVICE_CENTER).Columns(COL_NAME_SERVICE_CENTER_ID)}

            Dim childCols() As DataColumn = New DataColumn() _
                {ds.Tables(TABLE_NAME_CLAIM_DETAIL).Columns(COL_NAME_STORE_SERVICE_CENTER_ID), _
                 ds.Tables(TABLE_NAME_CLAIM_DETAIL).Columns(COL_NAME_SERVICE_CENTER_ID)}

            Dim SCToClaimRel As New DataRelation(TABLE_NAME_SC_CLAIM_RELATIONS, parentCols, childCols)

            SCToClaimRel.Nested = True
            ds.Relations.Add(SCToClaimRel)

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimsReadyFromSCPickList(ds As DataSet, routeId As Guid, companies As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_READY_FROM_SC_PICKLIST")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND route.route_id = " & MiscUtil.GetDbStringFromGuid(routeId, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_PICKLIST, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimsReadyFromSCStoresByRoute(ds As DataSet, routeId As Guid, companies As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_READY_FROM_SC_STORE")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND route.route_id = " & MiscUtil.GetDbStringFromGuid(routeId, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_STORE, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimsReadyFromSCServiceCentersByRoute(ds As DataSet, routeId As Guid, companies As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_READY_FROM_SC_SC")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND route.route_id = " & MiscUtil.GetDbStringFromGuid(routeId, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_SERVICE_CENTER, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimsReadyFromSCDetailByRoute(ds As DataSet, routeId As Guid, companies As ArrayList, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_READY_FROM_SC_DETAIL")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND route.route_id = " & MiscUtil.GetDbStringFromGuid(routeId, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                     New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                     New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN, GetType(Integer))}

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_CLAIM_DETAIL, parameters)

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


#End Region

    Public Function GetActiveClaimsForSvc(serviceNetworkID As Guid, sortOrder As Integer, claimStatusCodeId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_ACTIVE_CLAIMS_FOR_SVC")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        'whereClauseConditions &= Environment.NewLine & " AND upper(route.code) = upper(" & DBHelper.ValueToSQLString(routeCode) & ")"
        'whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If
        Dim ds As New DataSet()
        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_STORE, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimInfo(claimId As Guid, includeStatusHistory As String, companies As ArrayList, LanguageId As Guid, customerName As String, customerPhone As String, authorizationNumber As String) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_GET_CLAIM_INFO")
        Dim ds As New DataSet()

        '  selectStmt = "ELITA.OFA_GET_CLAIM_INFO"
        Try

            Dim cmd As OracleCommand = DB_OracleCommand(selectStmt, CommandType.StoredProcedure)

            cmd.Parameters.Add("Parm1", OracleDbType.Raw).Value = claimId.ToByteArray
            cmd.Parameters.Add("Parm2", OracleDbType.Varchar2).Value = authorizationNumber
            cmd.Parameters.Add("Parm3", OracleDbType.Varchar2).Value = customerName
            cmd.Parameters.Add("Parm4", OracleDbType.Varchar2).Value = customerPhone
            cmd.Parameters.Add("Parm5", OracleDbType.Raw).Value = LanguageId.ToByteArray
            cmd.Parameters.Add("Parm6", OracleDbType.Varchar2).Value = MiscUtil.BuildListForSql("", companies)
            cmd.Parameters.Add("Parm7", OracleDbType.Varchar2).Value = includeStatusHistory & ""
            cmd.Parameters.Add("OUT1", OracleDbType.RefCursor, ParameterDirection.Output)
            cmd.Parameters.Add("OUT2", OracleDbType.RefCursor, ParameterDirection.Output)

            DB_Fill_DataSet(ds, cmd, TABLE_NAME_CLAIM_DETAIL, TABLE_NAME_CLAIM_STATUS_HISTORY)

            '{SetParameter("CLAIM_ID", claimId.ToByteArray), _
            ' SetParameter("AUTHORIZATIONNUMBER",  authorizationNumber), _
            ' SetParameter("CUSTOMERNAME",  customerName), _
            ' SetParameter("CUSTOMERPHONE",  customerPhone), _
            ' SetParameter("LANGUAGEID", LanguageId.ToByteArray), _
            ' SetParameter("COMPANYLIST", MiscUtil.BuildListForSql("", companies, False)), _
            ' SetParameter("HISTORY", includeStatusHistory)
            '}

            '  outputParameter(0) = New DBHelper.DBHelperParameter("CLAIM_CUR", GetType(DataSet))
            '   outputParameter(1) = New DBHelper.DBHelperParameter("CLAIM_HISTORY_CUR", GetType(DataSet))

            ' Call DBHelper Store Procedure
            '  DBHelper.FetchSp(selectStmt, inputParameters, outputParameter, ds, "CLAIMINFO")

            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Private Function DB_OracleCommand(p_SqlStatement As String, p_CommandType As CommandType) As OracleCommand
        Dim conn As IDbConnection = New OracleConnection(DBHelper.ConnectString)
        Dim cmd As OracleCommand = conn.CreateCommand()

        cmd.CommandText = P_SqlStatement
        cmd.CommandType = p_CommandType

        Return cmd
    End Function
    Private Sub DB_Fill_DataSet(ByRef p_ds As DataSet, p_cmd As OracleCommand, p_Table_Name As String, Optional ByVal p_Table_Name2 As String = "")
        Dim da As OracleDataAdapter

        Try
            da = New OracleDataAdapter(P_cmd)

            da.Fill(p_ds, p_Table_Name)
            p_ds.Locale = CultureInfo.InvariantCulture

            If p_ds.Tables.Count > 1 AndAlso p_Table_Name2 <> "" Then
                p_ds.Tables(1).TableName = p_Table_Name2
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        Finally
            Try
                da.Dispose()
                da = Nothing
            Catch ex As Exception
            End Try
        End Try
    End Sub
    Public Function GetClaimStatusHistory(claimId As Guid, companies As ArrayList, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_GET_CLAIM_STATUS_HISTORY")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND claim.claim_id = " & DBHelper.ValueToSQLString(claimId)
        whereClauseConditions &= Environment.NewLine & " AND lang.language_id = " & DBHelper.ValueToSQLString(languageId)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("claim.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If
        Dim ds As New DataSet()
        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_CLAIM_STATUS_HISTORY, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimsByPickList(HeaderID As Guid, StoreServiceCenterID As Guid, ServiceCenterID As Guid, companies As ArrayList, languageId As Guid) As DataSet
        Dim ds As New DataSet(TABLE_NAME_CLAIM_BY_PICKLIST)
        Dim doNotCatch As Boolean = False

        Try
            ds = GetClaimsPickListHeader(ds, HeaderID, StoreServiceCenterID, ServiceCenterID, companies)
            If ds.Tables.Count > 0 AndAlso Not ds.Tables(TABLE_NAME_PICKLIST) Is Nothing AndAlso ds.Tables(TABLE_NAME_PICKLIST).Rows.Count > 1 Then
                doNotCatch = True
                Throw New ElitaPlusException("GetClaimsByPickList ", ErrorCodes.MORE_THAN_ONE_PICKLIST_FOUND)
            End If

            ds = GetClaimsPickListStores(ds, HeaderID, StoreServiceCenterID, ServiceCenterID, companies)
            ds = GetClaimsPickListServiceCenters(ds, HeaderID, StoreServiceCenterID, ServiceCenterID, companies)
            ds = GetClaimsPickListDetail(ds, HeaderID, StoreServiceCenterID, ServiceCenterID, companies, languageId)

            Dim picklistToStoreRel As New DataRelation(TABLE_NAME_PICKLIST_STORE_RELATIONS, _
                                                 ds.Tables(TABLE_NAME_PICKLIST).Columns(COL_NAME_ROUTE_ID), _
                                                 ds.Tables(TABLE_NAME_STORE).Columns(COL_NAME_ROUTE_ID))
            picklistToStoreRel.Nested = True
            ds.Relations.Add(picklistToStoreRel)

            Dim storeToSCRel As New DataRelation(TABLE_NAME_STORE_SC_RELATIONS, _
                                                 ds.Tables(TABLE_NAME_STORE).Columns(COL_NAME_STORE_SERVICE_CENTER_ID), _
                                                 ds.Tables(TABLE_NAME_SERVICE_CENTER).Columns(COL_NAME_STORE_SERVICE_CENTER_ID))
            storeToSCRel.Nested = True
            ds.Relations.Add(storeToSCRel)

            Dim parentCols() As DataColumn = New DataColumn() _
                            {ds.Tables(TABLE_NAME_SERVICE_CENTER).Columns(COL_NAME_STORE_SERVICE_CENTER_ID), _
                             ds.Tables(TABLE_NAME_SERVICE_CENTER).Columns(COL_NAME_SERVICE_CENTER_ID)}

            Dim childCols() As DataColumn = New DataColumn() _
                {ds.Tables(TABLE_NAME_CLAIM_DETAIL).Columns(COL_NAME_STORE_SERVICE_CENTER_ID), _
                 ds.Tables(TABLE_NAME_CLAIM_DETAIL).Columns(COL_NAME_SERVICE_CENTER_ID)}

            Dim SCToClaimRel As New DataRelation(TABLE_NAME_SC_CLAIM_RELATIONS, parentCols, childCols)

            SCToClaimRel.Nested = True
            ds.Relations.Add(SCToClaimRel)

            Return ds
        Catch ex As Exception
            If doNotCatch Then
                Throw ex
            Else
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End If
        End Try
    End Function

    Public Function GetClaimsPickListHeader(ds As DataSet, HeaderID As Guid, StoreServiceCenterID As Guid, ServiceCenterID As Guid, companies As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_CLAIM_PICKLIST")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND header.header_id = " & MiscUtil.GetDbStringFromGuid(HeaderID, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_PICKLIST, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimsPickListStores(ds As DataSet, HeaderID As Guid, StoreServiceCenterID As Guid, ServiceCenterID As Guid, companies As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_CLAIM_STORE")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND header.header_id = " & MiscUtil.GetDbStringFromGuid(HeaderID, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not StoreServiceCenterID.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND store.service_center_id = " & MiscUtil.GetDbStringFromGuid(StoreServiceCenterID, True)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_STORE, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimsPickListServiceCenters(ds As DataSet, HeaderID As Guid, StoreServiceCenterID As Guid, ServiceCenterID As Guid, companies As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_CLAIM_SERVICE_CENTER")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND header.header_id = " & MiscUtil.GetDbStringFromGuid(HeaderID, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not ServiceCenterID.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND sc.service_center_id = " & MiscUtil.GetDbStringFromGuid(ServiceCenterID, True)
        End If

        If Not StoreServiceCenterID.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND claim.store_service_center_id = " & MiscUtil.GetDbStringFromGuid(StoreServiceCenterID, True)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Dim rowNum As New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_SERVICE_CENTER, _
                            New DBHelper.DBHelperParameter() {rowNum})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimsPickListDetail(ds As DataSet, HeaderID As Guid, StoreServiceCenterID As Guid, ServiceCenterID As Guid, companies As ArrayList, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_CLAIM_DETAIL")

        Dim whereClauseConditions As String = ""
        Dim MAX_ROWS_RETURN As Integer = 10000

        whereClauseConditions &= Environment.NewLine & " AND header.header_id = " & MiscUtil.GetDbStringFromGuid(HeaderID, True)
        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("company.company_id", companies, False)

        If Not ServiceCenterID.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND sc.service_center_id = " & MiscUtil.GetDbStringFromGuid(ServiceCenterID, True)
        End If

        If Not StoreServiceCenterID.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND store.service_center_id = " & MiscUtil.GetDbStringFromGuid(StoreServiceCenterID, True)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                 New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                 New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                 New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                 New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, MAX_ROWS_RETURN)}
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_CLAIM_DETAIL, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetPicklistByDateRange(languageId As Guid, companyGroupId As Guid, startDate As DateTime, endDate As DateTime) As DataSet

        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_PICKLIST_BY_DATE_RANGE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_START_DATE, startDate, GetType(DateTime)), _
                         New DBHelper.DBHelperParameter(COL_NAME_END_DATE, endDate, GetType(DateTime))}
        Dim ds As New DataSet

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetClaimsByDateRange(startDate As DateTime, endDate As DateTime, serviceCenterId As Guid, companies As ArrayList, LanguageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/CLAIM_LOGISTICS_GET_CLAIMS_BY_DATE_RANGE")
        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("claim.company_id", companies, False)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, LanguageId.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_START_DATE, startDate, GetType(DateTime)), _
                         New DBHelper.DBHelperParameter(COL_NAME_END_DATE, endDate, GetType(DateTime)), _
                         New DBHelper.DBHelperParameter(COL_NAME_SERVICE_CENTER_ID, serviceCenterId.ToByteArray)}

        Dim ds As New DataSet()

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME_CLAIM_DETAIL, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
End Class


