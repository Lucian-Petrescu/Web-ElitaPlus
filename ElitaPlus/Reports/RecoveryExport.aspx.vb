Namespace Reports
    Partial Public Class RecoveryExport
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "RECOVERY"
        Private Const RPT_FILENAME As String = "Recovery"
        Private Const RPT_FILENAME_EXPORT As String = "Recovery-Exp"

        Public Const ALL As String = "*"
        ' Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"

#End Region

#Region "Properties"
        Public ReadOnly Property TheRptCeInputControl() As ReportCeInputControl
            Get
                If moReportCeInputControl Is Nothing Then
                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
                End If
                Return moReportCeInputControl
            End Get
        End Property
#End Region

#Region "Handlers"
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                    TheRptCeInputControl.SetExportOnly()
                    TheRptCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT)
                    'Date Calendars
                    AddCalendar(BtnBeginDate, moBeginDateText)
                    AddCalendar(BtnEndDate, moEndDateText)
                Else
                    ClearErrLabels()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)

        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region


#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moBeginDateLabel)
            ClearLabelErrSign(moEndDateLabel)
        End Sub
#End Region

#Region "Populate"

        Private Sub InitializeForm()
            Dim t As Date = Date.Now.AddDays(-Date.Today.Day + 1)
            t = t.AddMonths(-Date.Today.Month + 1)
            t = t.AddYears(-1)
            moBeginDateText.Text = GetDateFormattedString(t)
            moEndDateText.Text = GetDateFormattedString(Date.Now.AddDays(-1))
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(user_id As String, beginDate As String,
                                  endDate As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturevalue As String
            Dim reportName As String = RPT_FILENAME
            Dim moReportFormat As ReportCeBaseForm.RptFormat

            Dim exportData As String = NO

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturevalue = TheRptCeInputControl.getCultureValue(True)
            End If
            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(RPT_FILENAME_WINDOW)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_USER", user_id),
                     New ReportCeBaseForm.RptParam("V_FROM_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_TO_DATE", endDate),
                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue)}

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

        Private Sub GenerateReport()
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim endDate As String
            Dim beginDate As String

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(userId, beginDate, endDate)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region


    End Class
End Namespace
