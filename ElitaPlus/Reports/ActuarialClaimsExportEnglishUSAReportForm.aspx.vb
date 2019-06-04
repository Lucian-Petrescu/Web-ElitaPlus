Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports

    Partial Class ActuarialClaimsExportEnglishUSAReportForm
        Inherits ElitaPlusPage


#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "ACTUARIAL_CLAIMS"
        Private Const RPT_FILENAME As String = "ActuarialClaims"
        Private Const RPT_FILENAME_EXPORT As String = "ActuarialClaims-Exp"

        Public Const ALL As String = "*"
        ' Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"

        Public Const MAX_LOWER_DATE_DIFF As Integer = -30
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
#End Region

#Region "Properties"
        Public ReadOnly Property TheRptCeInputControl() As ReportExtractInputControl
            Get
                If moReportExtractInputControl Is Nothing Then
                    moReportExtractInputControl = CType(FindControl("moReportExtractInputControl"), ReportExtractInputControl)
                End If
                Return moReportExtractInputControl
            End Get
        End Property
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
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
        Protected WithEvents moUserCompanyMultipleDrop As MultipleColumnDDLabelControl
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            Public ForEdit As Boolean = False
            Public HasDataChanged As Boolean
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    PopulateCompaniesDropdown()
                    TheRptCeInputControl.SetExportOnly()
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
            Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
        End Sub
#End Region

#Region "Populate"

        Private Sub InitializeForm()
            Dim BeginDate As Date
            BeginDate = Date.Now
            BeginDate = BeginDate.AddMonths(1)
            BeginDate = BeginDate.AddYears(-5)
            Me.moBeginDateText.Text = GetDateFormattedString(BeginDate)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now.AddDays(-1))
            'TheRptCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT)
        End Sub
        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            '''Me.UserCompanyMultipleDrop.NothingSelected = False
            '''Me.UserCompanyMultipleDrop.Caption = Me.TranslateLabelOrMessage("SELECT_COMPANY")
            '''UserCompanyMultipleDrop.BindData(dv)
            '''If UserCompanyMultipleDrop.Count.Equals(ONE_ITEM) Then
            '''    ' HideHtmlElement("ddSeparator")
            '''    UserCompanyMultipleDrop.Visible = False
            '''End If


            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(False, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub
#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal companyCode As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal langCode As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME_EXPORT
            Dim culturecode As String
            Dim moReportFormat As ReportCeBaseForm.RptFormat

            Dim exportData As String = NO

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturecode = TheRptCeInputControl.getCultureValue(True)
            End If

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_COMPANY_KEY", companyCode),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", langCode),
                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturecode)}


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
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            'Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)
            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
            Dim endDate As String
            Dim beginDate As String
            Dim reportParams As New System.Text.StringBuilder
            Dim culturecode As String

            culturecode = TheRptCeInputControl.getCultureValue(True)

            'Dates
            'High date must be higher than low date.
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)

            'Low date cannot be adjusted lower by more than 30 days. 
            If DateHelper.IsDate(moBeginDateText.Text) Then
                Dim orginalBeginDate As Date
                orginalBeginDate = Date.Now
                orginalBeginDate = orginalBeginDate.AddMonths(1)
                orginalBeginDate = orginalBeginDate.AddYears(-5)
                Dim UserEnteredDate As Date = DateHelper.GetDateValue(moBeginDateText.Text) 'CType(moBeginDateText.Text, Date)
                Dim timeSpanBetweenDates As TimeSpan = UserEnteredDate.Subtract(orginalBeginDate)
                'Low date cannot be adjusted lower by more than 30 days. 
                If timeSpanBetweenDates.Days < MAX_LOWER_DATE_DIFF Then
                    ElitaPlusPage.SetLabelError(Me.moBeginDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_DATE_MUST_NOT_BE_LOWER_BY_MORE_THAN_30_ERR)
                End If
            Else
                ElitaPlusPage.SetLabelError(moBeginDateLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
            End If

            'High date cannot be higher than the high date. 
            If DateHelper.IsDate(moEndDateText.Text) Then
                Dim UserEnteredEndDate As Date = DateHelper.GetDateValue(moEndDateText.Text) 'CType(moEndDateText.Text, Date)
                If UserEnteredEndDate > Date.Now.AddDays(-1) Then
                    ElitaPlusPage.SetLabelError(Me.moEndDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_MUST_NOT_BE_HIGHER_THAN_YESTERDAY_ERR)
                End If
            Else
                ElitaPlusPage.SetLabelError(moBeginDateLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
            End If

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)


            reportParams.AppendFormat("pi_company_key => '{0}',", compCode)
            reportParams.AppendFormat("pi_begin_date => '{0}',", beginDate)
            reportParams.AppendFormat("pi_end_date => '{0}',", endDate)
            reportParams.AppendFormat("pi_language_code => '{0}',", langCode)
            reportParams.AppendFormat("pi_lang_culture_code => '{0}'", culturecode)

            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "ACTUARIAL_CLAIMS")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_ActuarialClaimsExport.Oralce_Export")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", reportParams.ToString())
            Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            'ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            'Dim params As ReportCeBaseForm.Params = SetParameters(compCode, beginDate, endDate, langCode)
            'Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            ScheduleReport()
        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheRptCeInputControl.GetSchedDate()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()

                    Me.State.IsNew = False
                    Me.State.HasDataChanged = True
                    Me.State.MyBO.CreateJob(scheduleDate)

                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        Me.DisplayMessage(Message.MSG_Email_not_configured, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    Else
                        Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    End If

                    btnGenRpt.Enabled = False

                Else
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub
#End Region


    End Class
End Namespace
