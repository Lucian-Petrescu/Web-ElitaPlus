'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/12/2006)********************


Public Class BillingPlanDAL
    Inherits DALBase



#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_BILLING_PLAN"
    Public Const TABLE_KEY_NAME As String = "billing_plan_id"
    Public Const COL_NAME_BILLING_PLAN_ID As String = "billing_plan_id"
    Public Const COL_NAME_DEALER_GROUP_ID As String = "dealer_group_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_DEALER_GROUP_CODE As String = "dealer_group_code"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("billing_plan_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(DealerId As Guid, dealer_group_id As Guid, _
                             billingPlanMask As String, compIds As ArrayList, compGroupId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        ' hextoraw
        inCausecondition &= "(" & MiscUtil.BuildListForSql("d.company_id", compIds, True)

        inCausecondition &= " OR g.company_group_id = '" & GuidToSQLString(compGroupId) & "')"

        If Not IsNothing(dealer_group_id) Then
            whereClauseConditions &= " AND g.dealer_group_id = '" & GuidToSQLString(dealer_group_id) & "'"
        End If

        If Not IsNothing(DealerId) Then
            whereClauseConditions &= " AND d.dealer_id = '" & GuidToSQLString(DealerId) & "'"
        End If

        If ((billingPlanMask IsNot Nothing) AndAlso (FormatSearchMask(billingPlanMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(bp.code)" & billingPlanMask.ToUpper
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & COL_NAME_DEALER_GROUP_CODE & ", " & COL_NAME_DEALER_CODE)
        Try

            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Sub Delete(billingPlanId As Guid)
        Try
            Dim deleteStatement As String = Config("/SQL/DELETE")
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_BILLING_PLAN_ID, billingPlanId.ToByteArray)}
            DBHelper.Execute(deleteStatement, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region


End Class


