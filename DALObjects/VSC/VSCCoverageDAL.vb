'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/14/2007)********************


Public Class VSCCoverageDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_VSC_PLAN"
    Public Const TABLE_KEY_NAME As String = "vsc_coverage_id"

    Public Const COL_NAME_VSC_COVERAGE_ID As String = "vsc_coverage_id"
    Public Const COL_NAME_VSC_PLAN_ID As String = "vsc_plan_id"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_IS_DEALER_DISCOUNT_ID As String = "is_dealer_discount_id"
    Public Const COL_NAME_IS_CLAIM_ALLOWED_ID As String = "is_claim_allowed_id"
    Public Const COL_NAME_IS_BASE_PLAN_ID As String = "is_base_plan_id"
    Public Const COL_NAME_ADD_TO_PLAN_ID As String = "add_to_plan_id"
    Public Const COL_NAME_ALLOCATION_PERCENT_USED As String = "allocation_percent_used"
    Public Const COL_NAME_ALLOCATION_PERCENT_NEW As String = "allocation_percent_new"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEALER_CODE As String = "dealer"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    'Public Const COL_NAME_DEALER_GROUP_CODE As String = "code"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    'Public Const COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_DEALER_GROUP As String = "code"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_Id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "companygroupId"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("vsc_coverage_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadListByPlanID(ByVal LanguageID As Guid, ByVal PlanID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_PLAN_ID")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                    New DBHelper.DBHelperParameter("language_id", LanguageID.ToByteArray), _
                    New DBHelper.DBHelperParameter("vsc_plan_id", PlanID.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
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
#End Region

#Region "LOAD VSC COVERAGE LIST"
    Public Function Load_VSC_Coverage_Plan(ByVal VSC_PLAN_ID As Guid, ByVal DEALER As String, ByVal DEALER_NAME As String, ByVal CODE As String, ByVal M_DATE As String, ByVal companygroupId As Guid) As DataSet  '
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Me.Config("/SQL/VSC_COVERAGE_LOAD_LIST")

        Dim inClauseCondition As String
        Dim whereClauseConditions As String = ""
       
        If Not VSC_PLAN_ID.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND ELP_VSC_PLAN.VSC_PLAN_ID in  (select VSC_PLAN_ID from ELP_VSC_RATE_VERSION where VSC_PLAN_ID like " & MiscUtil.GetDbStringFromGuid(VSC_PLAN_ID) & ")"
        End If
        If Not DEALER = "" Then
            whereClauseConditions &= Environment.NewLine & "AND ELP_DEALER.DEALER in (select DEALER from ELP_DEALER  where upper(DEALER) like  upper('" & DEALER & "'))"
        End If

        If Not CODE = "" Then
            whereClauseConditions &= Environment.NewLine & "AND ELP_DEALER_GROUP.CODE in (select CODE from ELP_DEALER_GROUP  where upper(CODE) =upper('" & CODE & "'))"
        End If
        If Not DEALER_NAME = "" Then
            whereClauseConditions &= Environment.NewLine & "AND ELP_DEALER.DEALER_NAME in (select DEALER_NAME from ELP_DEALER  where upper(DEALER_NAME) like upper('" & DEALER_NAME & "'))"
        End If
        If Not M_DATE = "" Then
            whereClauseConditions &= Environment.NewLine & "AND ELP_VSC_RATE_VERSION.EFFECTIVE_DATE between TO_DATE('" & M_DATE & "','DD-Mon-YYYY')and TO_DATE('31-Dec-2999','DD-Mon-YYYY')"
        End If
        If Not companygroupId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND ELP_VSC_PLAN.COMPANY_GROUP_ID = " & MiscUtil.GetDbStringFromGuid(companygroupId) & ""
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")

        End If

        ' - -dynamic_where_clause
        Try
            Dim ds As DataSet
            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function Load_VSC_Coverage(ByVal VSC_PLAN_ID As Guid) As DataSet  '
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Me.Config("/SQL/VSC_COVERAGE_LIST")
        Dim whereClauseConditions As String = ""

        If Not VSC_PLAN_ID.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "ELP_VSC_COVERAGE.VSC_PLAN_ID =" & MiscUtil.GetDbStringFromGuid(VSC_PLAN_ID) & ""
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")

        End If

        ' - -dynamic_where_clause
        Try
            Dim ds As DataSet
            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


#End Region

End Class


