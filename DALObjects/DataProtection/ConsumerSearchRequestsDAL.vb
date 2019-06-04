'*'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/18/2015)********************


Public Class ConsumerSearchRequestsDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_REPORT_REQUESTS"
    Public Const TABLE_KEY_NAME As String = "report_request_id"

    Public Const COL_NAME_REPORT_REQUEST_ID As String = "report_request_id"
    Public Const COL_NAME_REPORT_TYPE As String = "report_type"
    Public Const COL_NAME_FTP_FILENAME As String = "ftp_filename"
    Public Const COL_NAME_REPORT_PARAMETERS As String = "report_parameters"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_START_DATE As String = "start_date"
    Public Const COL_NAME_END_DATE As String = "end_date"
    Public Const COL_NAME_ERROR_MESSAGE As String = "error_message"
    Public Const COL_NAME_USER_EMAIL_ADDRESS As String = "user_email_address"
    Public Const COL_NAME_REPORT_PROC As String = "report_proc"
    Public Const COL_NAME_SOURCEURL As String = "sourceurl"
    Public Const PAR_NAME_IP_REPORTING_YEAR_MONTH As String = "pi_reporting_year_month"
    Public Const PAR_NAME_IP_COMPANY_CODE As String = "pi_company_code"
    Public Const PAR_NAME_IP_DEALER_CODE As String = "pi_dealer_code"
    Public Const PAR_NAME_IP_GROUP_ID As String = "pi_group_id"
    Public Const PAR_NAME_IP_DEALER_WITH_CURRENCY As String = "pi_dealer_with_Cur"
    Public Const PAR_NAME_IP_CURRENCY_ID As String = "pi_currency_id"
    Public Const PAR_NAME_OP_ERROR_MESSAGE As String = "po_error_message"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("report_request_id", id.ToByteArray)}
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
    Public Function LoadDealerList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_DEALER")
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


#Region "Addition Methods"
    Public Sub CreateJob(ByVal reportRequestId As Guid, ByVal scheduledate As DateTime)
        Dim selectStmt As String = Me.Config("/SQL/CREATE_JOB")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("pi_request_id", reportRequestId),
                            New DBHelper.DBHelperParameter("pi_schedule_date", scheduledate)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}

        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub CreateReportRequest(ByVal ReportType As String, ByVal Requester As String, ByVal FtpFileName As String, ByVal ReportParameters As String, ByVal UserEmailAddress As String,
            ByVal ReportProc As String, ByVal Optional ScheduledDate As Date = Nothing)
        Dim selectStmt As String = Me.Config("/SQL/CREATE_REPORT_REQUEST")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("pi_report_type", ReportType),
                            New DBHelper.DBHelperParameter("pi_requester_id", Requester),
                            New DBHelper.DBHelperParameter("pi_ftp_filename", FtpFileName),
                            New DBHelper.DBHelperParameter("pi_report_parameters", ReportParameters),
                            New DBHelper.DBHelperParameter("pi_user_email_address", UserEmailAddress),
                            New DBHelper.DBHelperParameter("pi_report_proc", ReportProc),
                            New DBHelper.DBHelperParameter("pi_schedule_date", ScheduledDate)}

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}

        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Function GetAccessCountByUser(ByVal userId As String) As Integer
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_ACCESS_COUNT_BY_USER")
        Dim parameters = New OracleParameter() {New OracleParameter("created_by", userId)}
        Dim returnValue As Integer = 0
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                returnValue = Convert.ToInt32(ds.Tables(0).Rows(0)(0))
            End If
            Return returnValue
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetReportsByUser(ByVal userId As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/REPORTS_BY_USER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("created_by", userId)}
        Try
            Dim ds As New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadRequestsByUser(ByVal userId As String, ByVal reportType As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_REQUESTS_BY_USER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("created_by", userId), New DBHelper.DBHelperParameter("report_type", reportType)}
        Try
            Dim ds As New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadRequestsByReportKey(ByVal userId As String, ByVal requestId As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_REQUESTS_BY_REPORT_KEY")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("created_by", userId),
                                                                                            New DBHelper.DBHelperParameter("report_request_id", requestId)}
        Try
            Dim ds As New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function LoadRequests(ByVal requestId As String, ByVal userId As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_REQUESTS")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("report_request_id", requestId), New DBHelper.DBHelperParameter("created_by", userId)}
        Try
            Dim ds As New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function CheckExchangeRate(
                                ByVal reprortingmonthyear As String,
                                ByVal companyCode As String,
                                ByVal dealerCode As String,
                                ByVal groupid As String,
                                ByVal dealerwithcurrency As String,
                                ByVal currencyid As String) As String

        Dim selectStmt As String = Me.Config("/SQL/CHECK_EXCHANGE_RATE")
        Dim inputParameters(5) As DBHelper.DBHelperParameter
        Dim outputParameter(0) As DBHelper.DBHelperParameter

        inputParameters(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_IP_REPORTING_YEAR_MONTH, reprortingmonthyear)
        inputParameters(1) = New DBHelper.DBHelperParameter(Me.PAR_NAME_IP_COMPANY_CODE, companyCode)
        inputParameters(2) = New DBHelper.DBHelperParameter(Me.PAR_NAME_IP_DEALER_CODE, dealerCode)
        inputParameters(3) = New DBHelper.DBHelperParameter(Me.PAR_NAME_IP_GROUP_ID, groupid)
        inputParameters(4) = New DBHelper.DBHelperParameter(Me.PAR_NAME_IP_DEALER_WITH_CURRENCY, dealerwithcurrency)
        inputParameters(5) = New DBHelper.DBHelperParameter(Me.PAR_NAME_IP_CURRENCY_ID, currencyid)

        outputParameter(0) = New DBHelper.DBHelperParameter(Me.PAR_NAME_OP_ERROR_MESSAGE, GetType(String), 500)

        'Try
        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

        Return outputParameter(0).Value

    End Function




#End Region




End Class


