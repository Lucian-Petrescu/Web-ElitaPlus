'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (6/11/2008)********************


Public Class ClaimStatusByGroupDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CLAIM_STATUS_BY_GROUP"
    Public Const TABLE_KEY_NAME As String = "claim_status_by_group_id"

    Public Const COL_NAME_CLAIM_STATUS_BY_GROUP_ID As String = "claim_status_by_group_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_LIST_ITEM_ID As String = "list_item_id"
    Public Const COL_NAME_STATUS_ORDER As String = "status_order"
    Public Const COL_NAME_OWNER_ID As String = "owner_id"
    Public Const COL_NAME_SKIPPING_ALLOWED_ID As String = "skipping_allowed_id"
    Public Const COL_NAME_STATUS_DATE As String = "status_date"
    Public Const COL_NAME_ACTIVE_ID As String = "active_id"
    Public Const COL_NAME_COMPANY_GROUP_NAME As String = "company_group_name"
    Public Const COL_NAME_COMPANY_GROUP_CODE As String = "company_group_code"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_GROUP_NUMBER As String = "group_number"
    Public Const COL_NAME_TURNAROUND_DAYS As String = "turnaround_time_days"
    Public Const COL_NAME_TAT_REMINDER_HOURS As String = "tat_reminder_hours"
    Public Const DYNAMIC_LANGUAGE_JOIN_CLAUSE_PLACE_HOLDER = "--dynamic language id"
    Public Const DYNAMIC_GROUP_JOIN_CLAUSE_PLACE_HOLDER = "--dynamic company group id or dealer id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const DSNAME As String = "LIST"

    Public Enum SearchByType
        Dealer
        CompanyGroup
    End Enum

    Public Enum CMD
        insert
        update
    End Enum

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_status_by_group_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal SearchBy As Integer, ByVal CompanyGroupId As Guid, ByVal dealerId As Guid, ByVal compIds As ArrayList) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_COMPANY_GROUP")
        Dim whereClauseConditions As String = ""
        Dim sFilterCondition As String = MiscUtil.BuildListForNetSql("p.company_id", compIds)

        If SearchBy = SearchByType.Dealer Then
            selectStmt = Me.Config("/SQL/LOAD_LIST")
            If Not dealerId.Equals(Guid.Empty) Then
                whereClauseConditions &= Environment.NewLine & " AND a.dealer_id = " & MiscUtil.GetDbStringFromGuid(dealerId)
            Else
                whereClauseConditions &= Environment.NewLine & " AND a.dealer_id in (SELECT dealer_id FROM ELP_DEALER r, elp_company p WHERE r.company_id = p.company_id AND " & sFilterCondition & ")"
            End If
        ElseIf SearchBy = SearchByType.CompanyGroup Then
            whereClauseConditions &= Environment.NewLine & " AND a.company_group_id = " & MiscUtil.GetDbStringFromGuid(CompanyGroupId)
        Else
            whereClauseConditions &= Environment.NewLine & " AND 1=0 "
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadListByCompanyGroup(ByVal companyGroupId As Guid, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_DYNAMIC")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() _
                            {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                             New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListByCompanyGroupOrDealer(ByVal SearchBy As Integer, ByVal CompanyGroupId As Guid, ByVal dealerId As Guid, ByVal languageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_COMPANY_GROUP_OR_DEALER")

        Dim languageClauseConditions As String = ""
        Dim groupClauseConditions As String = ""

        languageClauseConditions &= Environment.NewLine & " AND t.language_id = '" & GuidControl.GuidToHexString(languageId) & "'"

        If SearchBy = SearchByType.Dealer Then
            groupClauseConditions &= Environment.NewLine & " AND g.dealer_id = '" & GuidControl.GuidToHexString(dealerId) & "'"
        ElseIf SearchBy = SearchByType.CompanyGroup Then
            groupClauseConditions &= Environment.NewLine & " AND g.company_group_id = '" & GuidControl.GuidToHexString(CompanyGroupId) & "'"
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_LANGUAGE_JOIN_CLAUSE_PLACE_HOLDER, languageClauseConditions)
        selectStmt = selectStmt.Replace(Me.DYNAMIC_GROUP_JOIN_CLAUSE_PLACE_HOLDER, groupClauseConditions)

        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function IsStatusOrderExist(ByVal claimStatusGroupId As Guid, ByVal CompanyGroupId As Guid, ByVal dealerId As Guid, ByVal statusOrder As Integer) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/STATUS_ORDER_EXIST")
        Dim whereClauseConditions As String = ""
        Dim retVal As Boolean = False

        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND dealer_id = " & MiscUtil.GetDbStringFromGuid(dealerId)
        Else
            whereClauseConditions &= Environment.NewLine & " AND company_group_id = " & MiscUtil.GetDbStringFromGuid(CompanyGroupId)
        End If

        whereClauseConditions &= Environment.NewLine & " AND status_order = " & CType(statusOrder, String)
        whereClauseConditions &= Environment.NewLine & " AND claim_status_by_group_id <> '" & GuidControl.GuidToHexString(claimStatusGroupId) & "'"

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Dim ds As DataSet = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)

        If Not ds Is Nothing AndAlso Not ds.Tables(0) Is Nothing AndAlso CType(ds.Tables(0).Rows(0)(0), Integer) > 0 Then
            retVal = True
        Else
            retVal = False
        End If

        Return retVal
    End Function

    Public Function IsClaimStatusExist(ByVal SearchBy As Integer, ByVal CompanyGroupId As Guid, ByVal dealerId As Guid) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/CLAIM_STATUS_EXIST")
        Dim whereClauseConditions As String = ""
        Dim retVal As Boolean = False

        If SearchBy = SearchByType.Dealer Then
            whereClauseConditions &= Environment.NewLine & " AND dealer_id = " & MiscUtil.GetDbStringFromGuid(dealerId)
        Else
            whereClauseConditions &= Environment.NewLine & " AND company_group_id = " & MiscUtil.GetDbStringFromGuid(CompanyGroupId)
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Dim ds As DataSet = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)

        If Not ds Is Nothing AndAlso Not ds.Tables(0) Is Nothing AndAlso CType(ds.Tables(0).Rows(0)(0), Integer) > 0 Then
            retVal = True
        Else
            retVal = False
        End If

        Return retVal
    End Function

    Public Function IsDeletable(ByVal listItemId As String, ByVal dealerId As Guid, ByVal CompanyGroupId As Guid, ByVal searchBy As Integer) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/IS_DELETABLE")
        Dim whereClauseConditions As String = ""
        Dim retVal As Boolean = False

        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND dealer_id = " & MiscUtil.GetDbStringFromGuid(dealerId)
        Else
            whereClauseConditions &= Environment.NewLine & " AND company_group_id = " & MiscUtil.GetDbStringFromGuid(CompanyGroupId)
        End If

        whereClauseConditions &= Environment.NewLine & " AND list_item_id = '" & listItemId & "'"

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Dim ds As DataSet = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)

        If Not ds Is Nothing AndAlso Not ds.Tables(0) Is Nothing AndAlso CType(ds.Tables(0).Rows(0)(0), Integer) = 0 Then
            retVal = True
        Else
            retVal = False
        End If

        Return retVal
    End Function

    Public Function GetClaimStatusByGroupID(ByVal statusCode As String, ByVal languageId As Guid, ByVal companyGroupId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_CLAIM_STATUS_BY_CODE")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                            {New OracleParameter(Me.COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                             New OracleParameter(Me.COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                             New OracleParameter(DALObjects.ClaimStatusDAL.COL_NAME_STATUS_CODE, statusCode)}

        Try
            Return DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters)
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

    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            Me.Update(familyDataset, tr, DataRowState.Deleted)
            Me.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

#End Region


End Class


