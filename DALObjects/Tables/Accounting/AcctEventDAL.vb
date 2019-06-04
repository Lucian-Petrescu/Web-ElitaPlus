'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/19/2007)********************


Public Class AcctEventDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCT_EVENT"
    Public Const TABLE_KEY_NAME As String = "acct_event_id"

    Public Const COL_NAME_ACCT_EVENT_ID As String = "acct_event_id"
    Public Const COL_NAME_ACCT_COMPANY_ID As String = "acct_company_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_ACCT_EVENT_TYPE_ID As String = "acct_event_type_id"
    Public Const COL_NAME_EVENT_CONDITION As String = "event_condition"
    Public Const COL_NAME_LAST_RUN_DATE As String = "last_run_date"
    Public Const COL_NAME_LAST_COMPLETE_DATE As String = "last_complete_date"
    Public Const COL_NAME_DYN_SQL As String = "dyn_sql"
    Public Const COL_NAME_ALLOW_BAL_TRAN As String = "allow_bal_tran"
    Public Const COL_NAME_ALLOW_OVER_BUDGET As String = "allow_over_budget"
    Public Const COL_NAME_ALLOW_POST_TO_SUSPENDED As String = "allow_post_to_suspended"
    Public Const COL_NAME_BALANCING_OPTIONS As String = "balancing_options"
    Public Const COL_NAME_JOURNAL_TYPE As String = "journal_type"
    Public Const COL_NAME_LOAD_ONLY As String = "load_only"
    Public Const COL_NAME_POSTING_TYPE As String = "posting_type"
    Public Const COL_NAME_POST_PROVISIONAL As String = "post_provisional"
    Public Const COL_NAME_POST_TO_HOLD As String = "post_to_hold"
    Public Const COL_NAME_REPORTING_ACCOUNT As String = "reporting_account"
    Public Const COL_NAME_SUPPRESS_SUBSTITUTED_MESSAGES As String = "suppress_substituted_messages"
    Public Const COL_NAME_SUSPENSE_ACCOUNT As String = "suspense_account"
    Public Const COL_NAME_TRANSACTION_AMOUNT_ACCOUNT As String = "transaction_amount_account"
    Public Const COL_NAME_LAYOUT_CODE As String = "layout_code"
    Public Const COL_NAME_EVENT_NAME As String = "event_name"
    Public Const COL_NAME_EVENT_DESCRIPTION As String = "event_description"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_event_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal EventTypeMask As Guid, ByVal AcctcompanyMask As Guid, ByVal LanguageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        whereClauseConditions &= " AND UPPER(" & Me.TABLE_NAME & "." & Me.COL_NAME_ACCT_COMPANY_ID & ") =HEXTORAW('" & Me.GuidToSQLString(AcctcompanyMask) & "')"
        whereClauseConditions &= " AND " & TranslationDAL.COL_NAME_LANGUAGE_ID & " =HEXTORAW('" & Me.GuidToSQLString(LanguageId) & "')"

        If (Not EventTypeMask = Guid.Empty) Then
            whereClauseConditions &= " AND UPPER(" & Me.COL_NAME_ACCT_EVENT_TYPE_ID & ") =HEXTORAW('" & Me.GuidToSQLString(EventTypeMask) & "')"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        'selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & Me.COL_NAME_EVENT_NAME)
        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

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


End Class


