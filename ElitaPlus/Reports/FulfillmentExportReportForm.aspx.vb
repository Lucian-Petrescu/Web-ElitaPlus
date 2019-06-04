Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports

    Partial Class FulfillmentExportReportForm
        Inherits ElitaPlusPage


#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "FULFILLMENT_EXPORT"
        Private Const RPT_FILENAME As String = "FulFillmentExport"
        Private Const RPT_FILENAME_EXPORT As String = "FulfillmentBilling-Exp"

        Public Const ALL As String = "*"
        ' Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"

        Public Const MAX_LOWER_DATE_DIFF As Integer = -30
        Private Const ONE_ITEM As Integer = 1
        'Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"
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
        'Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
        '    Get
        '        If moUserCompanyMultipleDrop Is Nothing Then
        '            moUserCompanyMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
        '        End If
        '        Return moUserCompanyMultipleDrop
        '    End Get
        'End Property
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
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

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    'PopulateCompaniesDropdown()
                    PopulateDealerDropDown()
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
            'Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
        End Sub
#End Region

#Region "Populate"

        Private Sub InitializeForm()

            'TODO - set begin date
            'Dim BeginDate As Date
            'BeginDate = Date.Now
            'BeginDate = BeginDate.AddMonths(1)
            'BeginDate = BeginDate.AddYears(-5)
            'Me.moBeginDateText.Text = GetDateFormattedString(BeginDate)


            Me.moEndDateText.Text = GetDateFormattedString(Date.Now.AddDays(-1))
            'TheRptCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT)
        End Sub
        'Private Sub PopulateCompaniesDropdown()
        '    Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

        '    '''Me.UserCompanyMultipleDrop.NothingSelected = False
        '    '''Me.UserCompanyMultipleDrop.Caption = Me.TranslateLabelOrMessage("SELECT_COMPANY")
        '    '''UserCompanyMultipleDrop.BindData(dv)
        '    '''If UserCompanyMultipleDrop.Count.Equals(ONE_ITEM) Then
        '    '''    ' HideHtmlElement("ddSeparator")
        '    '''    UserCompanyMultipleDrop.Visible = False
        '    '''End If


        '    UserCompanyMultipleDrop.NothingSelected = True
        '    UserCompanyMultipleDrop.SetControl(False, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
        '    If dv.Count.Equals(ONE_ITEM) Then
        '        UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
        '        UserCompanyMultipleDrop.Visible = False
        '    End If
        'End Sub

        Sub PopulateDealerDropDown()
            'Me.BindListControlToDataView(Me.moDealerMultipleDrop, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER)
            DealerMultipleDrop.NothingSelected = True

            DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
            DealerMultipleDrop.AutoPostBackDD = True
        End Sub
#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal dealerCode As String, ByVal beginDate As String, ByVal endDate As String, ByVal langCode As String) As ReportCeBaseForm.Params

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
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
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
            'Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim dealerCode As String = moDealerMultipleDrop.SelectedCode
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
            Dim beginDate, endDate As String
            Dim reportParams As New System.Text.StringBuilder

            'High date cannot be higher than the high date. 
            If Microsoft.VisualBasic.IsDate(moEndDateText.Text) Then
                Dim UserEnteredEndDate As Date = CType(moEndDateText.Text, Date)
                If UserEnteredEndDate > Date.Now.AddDays(-1) Then
                    ElitaPlusPage.SetLabelError(Me.moEndDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_MUST_NOT_BE_HIGHER_THAN_YESTERDAY_ERR)
                End If
            End If

            If Microsoft.VisualBasic.IsDate(moBeginDateText.Text) Then
                Dim UserEnteredBeginDate As Date = CType(moBeginDateText.Text, Date)
                Dim UserEnteredEndDate As Date = CType(moEndDateText.Text, Date)
                If UserEnteredBeginDate > UserEnteredEndDate Then
                    ElitaPlusPage.SetLabelError(Me.moBeginDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                End If
            End If

            'Validating the Dealer selection
            If DealerMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_DEALER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            reportParams.AppendFormat("pi_begin_date => '{0}',", beginDate)
            reportParams.AppendFormat("pi_end_date => '{0}',", endDate)
            reportParams.AppendFormat("pi_dealer => '{0}'", dealerCode)

            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "FULFILLMENT_BILLING_EXPORT")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_FULFILLMENTBILLINGEXPORT.Oralce_Export")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", reportParams.ToString())
            Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            'ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            'Dim params As ReportCeBaseForm.Params = SetParameters(dealerCode, beginDate, endDate, langCode)
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
