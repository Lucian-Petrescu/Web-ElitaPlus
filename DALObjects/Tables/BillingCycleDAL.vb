'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/10/2012)********************


Public Class BillingCycleDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_BILLING_CYCLE"
    Public Const TABLE_KEY_NAME As String = "billing_cycle_id"

    Public Const COL_NAME_BILLING_CYCLE_ID As String = "billing_cycle_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_BILLING_CYCLE_CODE As String = "billing_cycle_code"
    Public Const COL_NAME_START_DAY As String = "start_day"
    Public Const COL_NAME_END_DAY As String = "end_day"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_BILLING_RUN_DATE_OFFSET_DAYS As String = "billing_run_date_offset_days"
    Public Const COL_NAME_DATE_OF_PAYMENT_OPTION_ID As String = "date_of_payment_option_id"
    Public Const COL_NAME_DATE_OF_PAYMENT_OFFSET_DAYS As String = "date_of_payment_offset_days"
    Public Const COL_NAME_NUMBER_OF_DIGITS_ROUNDOFF_ID As String = "number_of_digits_roundoff_id"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("billing_cycle_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(dealerId As Guid, billingCycleCodeMask As String, companyIds As ArrayList, _
                             companyGroupId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        inCausecondition &= MiscUtil.BuildListForSql("elp_dealer.company_id", companyIds, True)


        If Not IsNothing(dealerId) Then
            whereClauseConditions &= " AND elp_dealer.dealer_id = '" & GuidToSQLString(dealerId) & "'"
        End If

        If ((billingCycleCodeMask IsNot Nothing) AndAlso (FormatSearchMask(billingCycleCodeMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(elp_billing_cycle.BILLING_CYCLE_CODE)" & billingCycleCodeMask.ToUpper
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & COL_NAME_DEALER_NAME)
        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    Public Sub Delete(billingPlanId As Guid)
        Try
            Dim deleteStatement As String = Config("/SQL/DELETE")
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_BILLING_CYCLE_ID, billingPlanId.ToByteArray)}
            DBHelper.Execute(deleteStatement, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region


End Class


