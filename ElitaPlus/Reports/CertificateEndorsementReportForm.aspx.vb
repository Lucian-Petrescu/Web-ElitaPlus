Imports Microsoft.VisualBasic
Namespace Reports

    Partial Class CertificateEndorsementReportForm
        Inherits ElitaPlusPage


#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "CERTIFICATE ENDORSEMENTS"
        Private Const RPT_FILENAME As String = "Certificate Endorsement_Exp"
        Private Const RPT_FILENAME_EXPORT As String = "Certificate Endorsement_Exp"

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
                    TheRptCeInputControl.SetExportOnly()
                    'Date Calendars
                    Me.AddCalendar(Me.BtnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.moEndDateText)
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

#Region "Populate"

        Private Sub InitializeForm()
            Dim t As Date = Date.Now.AddDays(-Date.Today.Day + 1)
            t = t.AddMonths(-Date.Today.Month + 1)
            t = t.AddYears(-1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now.AddDays(-1))

            Dim currentDate As Date = Now

            Me.moBeginDateText.Text = GetDateFormattedString(DateAdd("YYYY", -1, DateAdd("m", -Month(Now) + 1, DateAdd("d", -Date.Today.Day + 1, Now))))

            Me.moEndDateText.Text = GetDateFormattedString(DateAdd("d", -1, Now))

        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal userId As String, ByVal beginDate As String,
                                  ByVal endDate As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME
            Dim moReportFormat As ReportCeBaseForm.RptFormat

            Dim exportData As String = NO

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = RPT_FILENAME_EXPORT
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_USER_ID", userId),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate)}

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
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim endDate As String
            Dim beginDate As String

            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(userId), beginDate, endDate)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region


    End Class
End Namespace
