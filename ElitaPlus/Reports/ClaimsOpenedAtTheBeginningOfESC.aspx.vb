Namespace Reports

    Partial Class ClaimsOpenedAtTheBeginningOfESC
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Claims Opened At The Beginning Of ESC"
        Private Const RPT_FILENAME As String = "ClaimsOpenedAtTheBeginningOfESC"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimsOpenedAtTheBeginningOfESC_Exp"

        '''Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        '''Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        '''Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const DEFAULT_NUMBER_OF_DAYS_SINCE_START_OF_COVERAGE As String = "30"

        Private Const BY_CLAIM_NUMBER As String = "0"
        Private Const BY_CLAIM_DATE As String = "1"
        Private Const BY_SERVICE_CENTER_NAME As String = "2"

        Private Const SORT_BY_CLAIM_NUMBER As String = "C"
        Private Const SORT_BY_CLAIM_DATE As String = "D"
        Private Const SORT_BY_SERVICE_CENTER_NAME As String = "S"

#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public userID As String
            'Public companyCode As String
            Public selectedBeginDate As String
            Public selectedEndDate As String
            Public numberOfDaysSinceStartOfCoverage As String
            Public sortOrder As String
        End Structure

#End Region

#Region "Properties"
        Public ReadOnly Property MyReportCeInputControl() As ReportCeInputControl
            Get
                If moReportCeInputControl Is Nothing Then
                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
                End If
                Return moReportCeInputControl
            End Get
        End Property
#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Private reportName As String = RPT_FILENAME
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    'Date Calendars
                    Me.AddCalendar(Me.BtnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.moEndDateText)
                Else
                    ClearErrLabels()
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
            Me.ClearLabelErrSign(numberOfDaysLabel)
        End Sub

#End Region

#Region "Populate"

        Private Sub InitializeForm()
            Me.rdReportSortOrder.Items(0).Selected = True
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.PopulateControlFromBOProperty(Me.txtNumberOfDaysSinceStartOfCoverage, Me.DEFAULT_NUMBER_OF_DAYS_SINCE_START_OF_COVERAGE)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Private Sub GenerateReport()
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim numberOfDaysSinceStartOfCoverage As Integer = CType(Me.txtNumberOfDaysSinceStartOfCoverage.Text, Integer)
            Dim sortOrder As String
            Dim endDate As String
            Dim beginDate As String

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            'Sort Order
            Select Case Me.rdReportSortOrder.SelectedValue()
                Case BY_CLAIM_DATE
                    sortOrder = Me.SORT_BY_CLAIM_DATE
                Case BY_CLAIM_NUMBER
                    sortOrder = Me.SORT_BY_CLAIM_NUMBER
                Case BY_SERVICE_CENTER_NAME
                    sortOrder = Me.SORT_BY_SERVICE_CENTER_NAME
            End Select

            'Validate DaysSinceStartOfCoverage integer
            If ((numberOfDaysSinceStartOfCoverage < 0) OrElse (numberOfDaysSinceStartOfCoverage > 999)) Then
                ElitaPlusPage.SetLabelError(numberOfDaysLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER_OF_ACTIVE_DAYS_ERR)
            End If

            ReportCeBase.EnableReportCe(Me, MyReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(userId), beginDate, endDate, numberOfDaysSinceStartOfCoverage, sortOrder)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub


        Function SetParameters(ByVal userId As String, ByVal selectedBeginDate As String,
                               ByVal selectedEndDate As String, ByVal numberOfDaysSinceStartOfCoverage As Integer, ByVal sortOrder As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim rptParams As ReportParams

            With rptParams
                .userID = userId
                .selectedBeginDate = selectedBeginDate
                .selectedEndDate = selectedEndDate
                .numberOfDaysSinceStartOfCoverage = numberOfDaysSinceStartOfCoverage.ToString
                .sortOrder = sortOrder
            End With

            reportFormat = ReportCeBase.GetReportFormat(Me)

            reportName = Me.RPT_FILENAME
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = Me.RPT_FILENAME_EXPORT
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                                           { _
                                                            New ReportCeBaseForm.RptParam("V_USER_ID", rptParams.userID), _
                                                            New ReportCeBaseForm.RptParam("V_BEGIN_DATE", rptParams.selectedBeginDate), _
                                                            New ReportCeBaseForm.RptParam("V_END_DATE", rptParams.selectedEndDate), _
                                                            New ReportCeBaseForm.RptParam("V_NUMBER_DAYS_SINCE_COVERAGE", rptParams.numberOfDaysSinceStartOfCoverage), _
                                                            New ReportCeBaseForm.RptParam("V_SORT_ORDER", rptParams.sortOrder)}

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params


        End Function



#End Region

    End Class

End Namespace
