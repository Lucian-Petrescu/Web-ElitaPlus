
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/9/2010)********************


Public Class ReportsPageCtrlDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_REPORTS_PAGECTRL"
    Public Const TABLE_KEY_NAME As String = "pagectrl_id"

    Public Const COL_NAME_PAGECTRL_ID As String = "pagectrl_id"
    Public Const COL_NAME_REPORT_NAME As String = "report_name"
    Public Const COL_NAME_PERIOD_GENERATED As String = "period_generated"
    Public Const COL_NAME_LAST_PAGENUM As String = "last_pagenum"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_CLOSING_DATE As String = "closing_date"
    Public Const COL_NAME_BEGIN_DATE As String = "begin_date"
    Public Const V_PAGECTRL As String = "V_PAGECTRL"
    Public Const STATUS_RUNNING As String = "Running"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("pagectrl_id", id.ToByteArray)}
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

    Public Function GetRptRunDateAndPageNum(RptName As String, CompanyId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/REPORT_RUNDATE_PAGENUM")

        Dim parameters(1) As DBHelper.DBHelperParameter
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(V_PAGECTRL, GetType(DataSet))}
        Dim ds As New DataSet

        parameters(0) = New DBHelper.DBHelperParameter(COL_NAME_REPORT_NAME, RptName)
        parameters(1) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, CompanyId.ToByteArray)
        Try

            DBHelper.FetchSp(selectStmt, parameters, outParameters, ds, TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetRunningPeriod(RptName As String, CompanyId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/RUNNING_PERIOD")

        Dim parameters(1) As DBHelper.DBHelperParameter
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(V_PAGECTRL, GetType(DataSet))}
        Dim ds As New DataSet

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(ReportsPagectrlDAL.COL_NAME_REPORT_NAME, RptName) _
                        , New DBHelper.DBHelperParameter(ReportsPagectrlDAL.COL_NAME_COMPANY_ID, CompanyId)}

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, params)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetRptStatusForAPeriod(RptName As String, ReportPeriod As String, CompanyId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/REPORT_STATUS_FOR_A_PERIOD")

        Dim parameters(1) As DBHelper.DBHelperParameter
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(V_PAGECTRL, GetType(DataSet))}
        Dim ds As New DataSet

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(ReportsPagectrlDAL.COL_NAME_REPORT_NAME, RptName) _
                        , New DBHelper.DBHelperParameter(ReportsPagectrlDAL.COL_NAME_PERIOD_GENERATED, ReportPeriod) _
                        , New DBHelper.DBHelperParameter(ReportsPagectrlDAL.COL_NAME_COMPANY_ID, CompanyId)}

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, params)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAccountingStartDate(CompanyId As Guid, ClosingDate As Date) As DataSet
        Dim selectStmt As String = Config("/SQL/REPORT_ACCOUNTING_START_DATE")

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(V_PAGECTRL, GetType(DataSet))}
        Dim ds As New DataSet

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(ReportsPageCtrlDAL.COL_NAME_COMPANY_ID, CompanyId), _
                         New DBHelper.DBHelperParameter(ReportsPageCtrlDAL.COL_NAME_CLOSING_DATE, ClosingDate)}

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, params)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAccountingCloseDate(CompanyId As Guid, BeginDate As Date) As DataSet
        Dim selectStmt As String = Config("/SQL/REPORT_ACCOUNTING_CLOSE_DATE")

        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(V_PAGECTRL, GetType(DataSet))}
        Dim ds As New DataSet

        Dim params() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(ReportsPageCtrlDAL.COL_NAME_COMPANY_ID, CompanyId), _
                         New DBHelper.DBHelperParameter(ReportsPageCtrlDAL.COL_NAME_BEGIN_DATE, BeginDate)}

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, params)
            Return ds

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
#End Region


End Class


