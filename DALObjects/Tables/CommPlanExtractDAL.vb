'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/26/2018)********************


Public Class CommPlanExtractDAL
    Inherits DALBase


#Region "Constants"
	Public Const TABLE_NAME As String = "ELP_COMM_PLAN_EXTRACT"
	Public Const TABLE_KEY_NAME As String = "comm_plan_extract_id"
	
	Public Const COL_NAME_COMM_PLAN_EXTRACT_ID As String = "comm_plan_extract_id"
	Public Const COL_NAME_COMMISSION_PLAN_ID As String = "commission_plan_id"
	Public Const COL_NAME_COMM_EXTRACT_PACKAGE_ID As String = "comm_extract_package_id"
	Public Const COL_NAME_SEQUENCE_NUMBER As String = "sequence_number"
	Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
	Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
	Public Const COL_NAME_COMM_TITLE_XCD As String = "comm_title_xcd"
	Public Const COL_NAME_CYCLE_FREQUENCY_XCD As String = "cycle_frequency_xcd"
	Public Const COL_NAME_CYCLE_CUT_OFF_DAY As String = "cycle_cut_off_day"
	Public Const COL_NAME_CYCLE_RUN_DAY As String = "cycle_run_day"
	Public Const COL_NAME_AMOUNT_SOURCE_XCD As String = "amount_source_xcd"
	Public Const COL_NAME_COMMISSION_PERCENTAGE As String = "commission_percentage"
	Public Const COL_NAME_COMMISSION_AMOUNT As String = "commission_amount"
	Public Const COL_NAME_CYCLE_CUT_OFF_SOURCE_XCD As String = "cycle_cut_off_source_xcd"
	Public Const COL_NAME_CODE As String = "code"
	Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_COMM_AT_RATE_XCD As String = "comm_at_rate_xcd"

    Private Const DSNAME As String = "LIST"

    Public Const TOTAL_PARAM_A = 1
    Public Const COMMPLAN_ID = 0
    Public Const CODE_ID = 1

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("comm_plan_extract_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal CommissionPlanId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter("comm_plan_extract_id", CommissionPlanId.ToByteArray)}

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function IsCodeInUse(ByVal commPlanId As Guid, ByVal commissionCode As String) As Integer

        Dim selectStmt As String = Me.Config("/SQL/CODE_UNIQUE")
        Dim parameters(TOTAL_PARAM_A) As DBHelper.DBHelperParameter
        Dim result As Object
        Dim inCausecondition As String

        parameters(COMMPLAN_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMMISSION_PLAN_ID, commPlanId.ToByteArray)
        parameters(CODE_ID) = New DBHelper.DBHelperParameter(COL_NAME_CODE, commissionCode)

        result = DBHelper.ExecuteScalar(selectStmt, parameters)

        Return Convert.ToInt32(result)

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


