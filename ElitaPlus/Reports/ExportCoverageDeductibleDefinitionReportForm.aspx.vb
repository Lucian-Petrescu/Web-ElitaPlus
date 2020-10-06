Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports
    Partial Public Class ExportCoverageDeductibleDefinitionReportForm
        Inherits ElitaPlusPage

#Region "Constants"
        Private Const RPT_FILENAME_WINDOW As String = "COVERAGE DEDUCTIBLE DEFINITIONS"
        Private Const RPT_FILENAME_EXPORT As String = "CoverageDeductibleDefinitions-Exp"

        Private Const LABEL_SELECT_DEALER As String = "DEALER"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "COMPANY"
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
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property
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

#Region "Handlers"

#Region " Web Form Designer Generated Code "
        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        End Sub
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button

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
                    'Date Calendars
                    AddCalendar(BtnBeginDate, moBeginDateText)
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

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moBeginDateLabel)
            ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            'If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                UserCompanyMultipleDrop.Visible = False

            End If
        End Sub
        Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True)
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            'Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            moBeginDateText.Text = GetDateFormattedString(Date.Now)
            TheRptCeInputControl.SetExportOnly()
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(selectedDealerId As String, beginDate As String, langCode As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(True, GuidControl.GuidToHexString((UserCompanyMultipleDrop.SelectedGuid)))
            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(RPT_FILENAME_WINDOW)

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                    {New ReportCeBaseForm.RptParam("V_DEALER_ID", selectedDealerId),
                                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                                     New ReportCeBaseForm.RptParam("V_LANG_CODE", langCode),
                                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue)}

            With params
                .msRptName = reportName 'RPT_FILENAME_EXPORT
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Private Sub GenerateReport()
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim reportParams As New System.Text.StringBuilder
            Dim isTotalsOnly As String
            Dim endDate As String
            Dim beginDate As String
            Dim CompanyCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim CompanyDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim companyid As String = GuidControl.GuidToHexString((UserCompanyMultipleDrop.SelectedGuid))
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGES, langId)

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Dates
            'ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text)
            If moBeginDateText.Text = String.Empty Then
                ElitaPlusPage.SetLabelError(moBeginDateLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
            Else
                beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)
            End If

            If selectedDealerId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            reportParams.AppendFormat("pi_dealer_id => '{0}',", GuidControl.GuidToHexString(selectedDealerId))
            reportParams.AppendFormat("pi_begin_date => '{0}'", beginDate)

            State.MyBO = New ReportRequests
            State.ForEdit = True
            PopulateBOProperty(State.MyBO, "ReportType", "COVERAGE_DEDUCTIBLE_DEFINITIONS")
            PopulateBOProperty(State.MyBO, "ReportProc", "R_EXPORTCOVERAGEDEDUCTIBLE.Oralce_Export")
            PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
            PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)
            'ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            'Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(selectedDealerId), beginDate, langCode)
            'Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

            ScheduleReport()
        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheRptCeInputControl.GetSchedDate()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()

                    State.IsNew = False
                    State.HasDataChanged = True
                    State.MyBO.CreateJob(scheduleDate)

                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    Else
                        DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    End If

                    btnGenRpt.Enabled = False

                Else
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

    End Class
End Namespace
