'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/16/2015)********************


Public Class ClaimCloseRulesDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_CLAIM_CLOSE_RULES"
	Public Const TABLE_KEY_NAME As String = "claim_close_rule_id"
	
	Public Const COL_NAME_CLAIM_CLOSE_RULE_ID As String = "claim_close_rule_id"
	Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_CLOSE_RULE_BASED_ON_ID As String = "close_rule_based_on_id"
	Public Const COL_NAME_CLAIM_STATUS_BY_GROUP_ID As String = "claim_status_by_group_id"
	Public Const COL_NAME_TIME_PERIOD As String = "time_period"
    Public Const COL_NAME_REASON_CLOSED_ID As String = "reason_closed_id"
    Public Const COL_NAME_PARENT_CLAIM_CLOSE_RULE_ID As String = "parent_claim_close_rule_id"
    Public Const COL_NAME_ACTIVE_FLAG As String = "active_flag"

    'stored procedure parameter names
    Public Const PAR_NAME_COMPANY As String = "pi_company_id"
    Public Const PAR_NAME_DEALER As String = "pi_dealer_id"

    Public Const PAR_NAME_NEW_DEALER As String = "pi_New_dealer_id"
    Public Const PAR_NAME_OLD_DEALER As String = "pi_Old_dealer_id"
    Public Const PAR_NAME_OLD_COMPANY As String = "pi_old_company_id"
    Public Const PAR_NAME_NEW_COMPANY As String = "pi_new_company_id"
    Public Const PAR_NAME_CLAIM_CLOSE_RULE As String = "pi_claim_close_rule_id"
    Public Const PAR_NAME_RETURN_CODE As String = "po_return_code"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_close_rule_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
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
#End Region

#Region "Public Functions"
    'Public Function LoadClaimCloseRules(ByVal companyId As Guid, ByVal dealerId As Guid) As DataSet

    '    Try
    '        Dim selectStmt As String = Me.Config("/SQL/LOAD_CLAIM_CLOSE_RULES")
    '        Dim whereClauseConditions As String = ""

    '        If Not (companyId = Guid.Empty) Then
    '            whereClauseConditions &= " AND " & Environment.NewLine & "ccr." & "company_id" & "= " & MiscUtil.GetDbStringFromGuid(companyId)

    '        End If
    '        If Not (dealerId = Guid.Empty) Then
    '            whereClauseConditions &= " AND " & Environment.NewLine & "ccr." & "dealer_id" & "= " & MiscUtil.GetDbStringFromGuid(dealerId)
    '        End If


    '        If Not whereClauseConditions = "" Then
    '            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
    '        Else
    '            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
    '        End If

    '        Dim ds As New DataSet
    '        ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    '        Return ds

    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Function

    Public Function LoadClaimCloseRules(ByVal companyId As Guid, ByVal dealerId As Guid) As DataSet

        Try
            Dim selectStmt As String
            Dim parameters As OracleParameter()
            If (dealerId = Guid.Empty) Then
                selectStmt = Me.Config("/SQL/LOAD_CLAIM_CLOSE_RULES_BY_COMPANY")
                parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray)}
            Else
                selectStmt = Me.Config("/SQL/LOAD_CLAIM_CLOSE_RULES_BY_DEALER")
                parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray), _
                                                    New OracleParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray), _
                                                    New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                                                    New OracleParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray), _
                                                    New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

            End If

            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    'Def-25716: Added function to validate if the claim close rules is already exists.
    Public Function ValidateClaimRule(ByVal companyId As Guid, ByVal dealerId As Guid, ByVal closeRuleBasedOnId As Guid, ByVal claimStatusByGroupId As Guid, ByVal entityType As String) As Integer
        Try
            Dim selectStmt As String

            Dim parameters() As DBHelper.DBHelperParameter
            If entityType = "Dealer" Then
                selectStmt = Me.Config("/SQL/VALIDATE_DEALER_CLAIM_CLOSE_RULES")
                parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_COMPANY_ID, companyId.ToByteArray), _
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_STATUS_BY_GROUP_ID, claimStatusByGroupId.ToByteArray), _
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_CLOSE_RULE_BASED_ON_ID, closeRuleBasedOnId.ToByteArray)}
            End If

            If entityType = "Company" Then
                selectStmt = Me.Config("/SQL/VALIDATE_COMPANY_CLAIM_CLOSE_RULES")
                parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_COMPANY_ID, companyId.ToByteArray), _
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_CLAIM_STATUS_BY_GROUP_ID, claimStatusByGroupId.ToByteArray), _
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_CLOSE_RULE_BASED_ON_ID, closeRuleBasedOnId.ToByteArray)}
            End If






            'Return CType(DBHelper.ExecuteScalar(selectStmt), Integer)
            Return Convert.ToInt32(DBHelper.ExecuteScalar(selectStmt, parameters))

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function UpdateClaimRuleInactive(ByVal claimCloseRuleId As Guid) As Integer
        Try
            Dim selectStmt As String = Me.Config("/SQL/UPDATE_CLAIM_RULE_IN_ACTIVE")
            Dim inParameters(0) As DBHelper.DBHelperParameter
            inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_CLAIM_CLOSE_RULE, claimCloseRuleId.ToByteArray)

            Dim outParameters(0) As DBHelper.DBHelperParameter
            outParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN_CODE, GetType(Integer))

            DBHelper.ExecuteSp(selectStmt, inParameters, outParameters)

            Return CInt(outParameters(0).Value)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadClaimCloseRulesByCompany(ByVal companyId As Guid) As DataSet
        Try
            Dim selectStmt As String = Me.Config("/SQL/LOAD_CLAIM_CLOSE_RULES_BY_COMPANY")
            Dim inParameters(0) As DBHelper.DBHelperParameter
            inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY, companyId.ToByteArray)

            Dim outParameters(0) As DBHelper.DBHelperParameter
            outParameters(0) = New DBHelper.DBHelperParameter("po_claimCloseRulesinfo", GetType(DataSet))

            Dim ds As New DataSet
            Dim tbl As String = Me.TABLE_NAME

            ' Call DBHelper Store Procedure
            DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadClaimCloseRulesByDealer(ByVal companyId As Guid, ByVal dealerId As Guid) As DataSet
        Try
            Dim selectStmt As String = Me.Config("/SQL/LOAD_CLAIM_CLOSE_RULES_BY_DEALER")
            Dim inParameters(1) As DBHelper.DBHelperParameter
            inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY, companyId.ToByteArray)
            inParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_NAME_DEALER, dealerId.ToByteArray)

            Dim outParameters(0) As DBHelper.DBHelperParameter
            outParameters(0) = New DBHelper.DBHelperParameter("po_claimCloseRulesinfo", GetType(DataSet))

            Dim ds As New DataSet
            Dim tbl As String = Me.TABLE_NAME

            ' Call DBHelper Store Procedure
            DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CopyClaimCloseRulesToNewCompany(ByVal OldcompanyId As Guid, ByVal NewcompanyId As Guid) As Integer
        Try
            Dim selectStmt As String = Me.Config("/SQL/COPY_CLAIM_CLOSE_RULES_To_NEW_COMPANY")
            Dim inParameters(1) As DBHelper.DBHelperParameter
            inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_OLD_COMPANY, OldcompanyId.ToByteArray)
            inParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_NAME_NEW_COMPANY, NewcompanyId.ToByteArray)

            Dim outParameters(0) As DBHelper.DBHelperParameter
            outParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN_CODE, GetType(Integer))

            DBHelper.ExecuteSp(selectStmt, inParameters, outParameters)

            Return CInt(outParameters(0).Value)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CopyClaimCloseRulesToNewDealer(ByVal companyId As Guid, ByVal OlddealerId As Guid, ByVal NewDealerId As Guid) As Integer
        Try
            Dim selectStmt As String = Me.Config("/SQL/COPY_CLAIM_CLOSE_RULES_To_NEW_DEALER")
            Dim inParameters(2) As DBHelper.DBHelperParameter
            inParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_COMPANY, companyId.ToByteArray)
            inParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_NAME_OLD_DEALER, OlddealerId.ToByteArray)
            inParameters(2) = New DBHelper.DBHelperParameter(Me.PAR_NAME_OLD_DEALER, NewDealerId.ToByteArray)

            Dim outParameters(0) As DBHelper.DBHelperParameter
            outParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_RETURN_CODE, GetType(Integer))

            DBHelper.ExecuteSp(selectStmt, inParameters, outParameters)

            Return CInt(outParameters(0).Value)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class


