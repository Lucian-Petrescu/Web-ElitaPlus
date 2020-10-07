Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class RegulatoryOpenedClaimsReportForm
        Inherits ElitaPlusPage


#Region "Page State"

#Region "MyState"

        Class MyState
            Public moPageCtrlId As Guid = Guid.Empty
            Public PGenerateStatus As String
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

#End Region

#Region "Parameters"

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "REGULATORY_OPENED_CLAIMS"
        Private Const RPT_FILENAME As String = "RegulatoryOpenedClaims"
        'Private Const RPT_FILENAME_EXPORT As String = "BillingRegisterChile-Exp"
        'Private Const RPT_SUBREPORT As String = "rptRStampTax"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label

        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Public Const PAGETITLE As String = "REGULATORY_OPENED_CLAIMS"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "REGULATORY_OPENED_CLAIMS"
        Private Const ONE_ITEM As Integer = 1

        Private Const PAGENUM As String = "PageNum"
        Private Const PREVIOUS_DATE As String = "Previous_Date"
        Private Const FUTURE_DATE As String = "Future_Date"
        Private Const STATUS As String = "period_status"
        Private Const PageCtrl_ID As String = "PC_ID"
        Private Const STATUS_RUNNING As String = "Running"
        Private Const STATUS_SUCCESS As String = "Success"
        Private Const STATUS_FAILURE As String = "Failure"

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
        Dim LPeriod As String
        Dim PGenerate As String
        Dim Month As DictionaryEntry
        Dim MonthName As Hashtable
#End Region

#Region "Properties"

        Private ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property

#End Region

#Region "Handlers-DropDown"

        Protected Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
     Handles multipleDropControl.SelectedDropChanged
            Try
                GetReportDatesAndPageNumber(RPT_FILENAME)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-RadioButton"

        '   Protected Sub OnFromDrop_Changed(ByVal reportcontroller As Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ReportCeInputControl) _
        'Handles  SelectedViewOption
        '       Try
        '           GetReportDatesAndPageNumber(RPT_FILENAME)
        '       Catch ex As Exception
        '           HandleErrors(ex, Me.ErrControllerMaster)
        '       End Try
        '   End Sub

#End Region

#Region "Enable/Clear Controls"

        Private Sub EnableDisablePageControls()
            If TheReportCeInputControl.RadioButtonVIEWControl.Checked = True Then
                ControlMgr.SetVisibleControl(Me, pnlPDF, False)
                ControlMgr.SetVisibleControl(Me, pnlView, True)
            Else
                ControlMgr.SetVisibleControl(Me, pnlView, False)
                ControlMgr.SetVisibleControl(Me, pnlPDF, True)
                GetReportDatesAndPageNumber(RPT_FILENAME)
            End If
        End Sub

        Private Sub ViewPDFRadioButtonChecked(sender As Object, e As System.EventArgs)
            EnableDisablePageControls()
            'GetReportDatesAndPageNumber(RPT_FILENAME)
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(lblCompany)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label

        'Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object
        'Protected WithEvents ReportCeInputControl As Global.Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ReportCeInputControl

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            'AddHandler , AddressOf MoodChangedFromMasterPage
            AddHandler CType(Page.Master, content_Report).SelectedViewOption, AddressOf ViewPDFRadioButtonChecked
            InitializeComponent()
        End Sub


        'Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '    'Wire up the event (MoodChanged) to the event handler (MoodChangedFromMasterPage)
        '    AddHandler Master.MoodChanged, AddressOf MoodChangedFromMasterPage
        'End Sub


        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrControllerMaster.Clear_Hide()
            ClearLabelsErrSign()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            If UserCompanyMultipleDrop.Visible = False Then
                HideHtmlElement(trcomp.ClientID)
            End If
            TheReportCeInputControl.RadioButtonPDFControl.AutoPostBack = True
            TheReportCeInputControl.RadioButtonVIEWControl.AutoPostBack = True
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    'JavascriptCalls()
                    InitializeForm()
                Else
                    CheckIfComingFromSaveConfirm()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region

#Region "Initialize/Populate Controls"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, UserCompanyMultipleDrop.NO_CAPTION, True, False)
            If dv.Count.Equals(ONE_ITEM) Then
                ControlMgr.SetVisibleControl(Me, lblCompany, False)
                HideHtmlElement(trcomp.ClientID)
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
                GetReportDatesAndPageNumber(RPT_FILENAME)
            End If
        End Sub

        Private Sub PopulateYearsDropdown()
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId

            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            moYearDropDownList.Populate(YearListLkl, New PopulateOptions() With
                {
                   .AddBlankItem = True,
                   .BlankItemValue = "0",
                   .ValueFunc = AddressOf .GetCode
                })

            'Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.moYearDropDownList, dv, , , True)

            Dim oDescrip As String = GetSelectedDescription(moYearDropDownList)
        End Sub

        Private Sub PopulateMonthsDropdown()
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            moMonthDropDownList.Populate(monthLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            'Me.BindListControlToDataView(Me.moMonthDropDownList, dv, , , True)
        End Sub

        Private Function getMonthNames() As Hashtable
            Dim MonthNames As New Hashtable
            MonthNames.Add("01", "Enero")
            MonthNames.Add("02", "Febrero")
            MonthNames.Add("03", "Marzo")
            MonthNames.Add("04", "Abril")
            MonthNames.Add("05", "Mayo")
            MonthNames.Add("06", "Junio")
            MonthNames.Add("07", "Julio")
            MonthNames.Add("08", "Agosto")
            MonthNames.Add("09", "Setiembre")
            MonthNames.Add("10", "Octubre")
            MonthNames.Add("11", "Noviembre")
            MonthNames.Add("12", "Diciembre")
            Return MonthNames
        End Function

        Private Sub GetReportDatesAndPageNumber(rptname As String)
            'Dim Month As DictionaryEntry
            'Dim MonthName As Hashtable
            Dim rptBO As New ReportsPageCtrl
            'START  DEF-2875
            If UserCompanyMultipleDrop.SelectedIndex > 0 Then
                'END    DEF-2875
                MonthName = getMonthNames()
                Dim ds As DataSet = rptBO.GetRptRunDateAndPageNum(rptname, UserCompanyMultipleDrop.SelectedGuid)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtLPage.Text = ds.Tables(0).Rows(0)(PAGENUM).ToString.PadLeft(6, CChar("0".ToString))
                    For Each Month In MonthName
                        If ds.Tables(0).Rows(0)(PREVIOUS_DATE).ToString IsNot String.Empty Then
                            If Month.Key.ToString = ds.Tables(0).Rows(0)(PREVIOUS_DATE).ToString.Substring(0, 2) Then
                                txtLPeriod.Text = Month.Value.ToString & " " & ds.Tables(0).Rows(0)(PREVIOUS_DATE).ToString.Substring(2, 4)
                                hidden_LPeriod.Value = ds.Tables(0).Rows(0)(PREVIOUS_DATE).ToString
                            End If
                        Else
                            txtLPeriod.Text = String.Empty
                            hidden_LPeriod.Value = String.Empty
                        End If
                        If Month.Key.ToString = ds.Tables(0).Rows(0)(FUTURE_DATE).ToString.Substring(0, 2) Then
                            txtPGenerate.Text = Month.Value.ToString & " " & ds.Tables(0).Rows(0)(FUTURE_DATE).ToString.Substring(2, 4)
                            hidden_PGenerate.Value = ds.Tables(0).Rows(0)(FUTURE_DATE).ToString
                            State.PGenerateStatus = ds.Tables(0).Rows(0)(STATUS).ToString
                            If ds.Tables(0).Rows(0)(PageCtrl_ID).ToString IsNot String.Empty Then
                                State.moPageCtrlId = GuidControl.ByteArrayToGuid(ds.Tables(0).Rows(0)(PageCtrl_ID))
                            End If
                        End If
                    Next
                Else
                    txtLPeriod.Text = String.Empty
                    txtLPage.Text = String.Empty
                    txtPGenerate.Text = String.Empty
                    hidden_LPeriod.Value = String.Empty
                    hidden_PGenerate.Value = String.Empty
                End If
            End If
        End Sub

        Private Sub InitializeForm()
            txtLPeriod.Enabled = False
            txtLPage.Enabled = False
            txtPGenerate.Enabled = False
            TheReportCeInputControl.ExcludeExport()
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
            PopulateCompaniesDropdown()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            EnableDisablePageControls()
            'GetLatestStatusforRunningPeriod(RPT_FILENAME)
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenRptPromptResponse.Value
            HiddenRptPromptResponse.Value = Nothing
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                GenerateReport(MSG_VALUE_YES)
            End If
        End Sub
#End Region

#Region "Report Parameters"

        Function SetParameters(companyCode As String, companyDesc As String, _
                               viewOption As String, selectedYearMonth As String, _
                               Rpt_GenerateDate As String, periodExists As Boolean, _
                               langCode As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False)
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            Dim exportData As String = NO
            Dim isSummary As String = YES
            Dim RptGenerateDate As String

            If viewOption = "PDF" Then
                RptGenerateDate = Rpt_GenerateDate.Substring(2, 4) & Rpt_GenerateDate.Substring(0, 2)
            Else
                RptGenerateDate = selectedYearMonth
            End If

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    { _
                     New ReportCeBaseForm.RptParam("V_COMPANY", companyCode), _
                     New ReportCeBaseForm.RptParam("V_COMPANY_DESC", companyDesc), _
                     New ReportCeBaseForm.RptParam("V_DEALER", ALL), _
                     New ReportCeBaseForm.RptParam("V_YEAR_MONTH", RptGenerateDate), _
                     New ReportCeBaseForm.RptParam("V_REPORTNAME", RPT_FILENAME), _
                     New ReportCeBaseForm.RptParam("V_VIEWOPTION", viewOption), _
                     New ReportCeBaseForm.RptParam("V_PERIODEXISTS", CType(periodExists, String)), _
                     New ReportCeBaseForm.RptParam("V_LANGUAGE", langCode), _
                     New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", culturecode)}

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

#End Region

#Region "View Report"

        Private Sub GenerateReport(ResponseVal As String)
            Dim compDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim compId As Guid = UserCompanyMultipleDrop.SelectedGuid
            Dim PeriodGenerate As String
            Dim rptRunDate As Boolean
            Dim selectedMonthYear As String
            Dim selectedYearMonth As String
            Dim periodExists As Boolean = False
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
            Dim ReportsPagectrlBO As New ReportsPageCtrl
            Dim viewOption As String

            If compId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(lblCompany)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            If TheReportCeInputControl.RadioButtonVIEWControl.Checked = True Then
                viewOption = "View"
                Dim selectedYear As String = GetSelectedDescription(moYearDropDownList)
                Dim selectedMonthID As Guid = GetSelectedItem(moMonthDropDownList)
                Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
                selectedMonthYear = selectedMonth & selectedYear
                selectedYearMonth = selectedYear & selectedMonth

                If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                    'Report Period should be valid
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)

                ElseIf New Date(Integer.Parse(selectedYear), Integer.Parse(selectedMonth), 1) <= Date.Today.AddMonths(-6) Then
                    'Report Period should not be older than 6 months
                    Throw New GUIException(Message.MSG_INVALID_YEARMONTH, Assurant.ElitaPlus.Common.ErrorCodes.GUI_REPORT_CANNOT_RUN_FORTHAT_DATE)

                ElseIf ReportsPagectrlBO.ChkPeriodIsOpen(selectedMonthYear, compId) Then
                    'Report Period should not be open
                    Throw New GUIException(Message.MSG_INVALID_YEARMONTH, Assurant.ElitaPlus.Common.ErrorCodes.GUI_REPORT_CANNOT_RUN_FORTHAT_DATE)

                End If
            Else
                viewOption = "PDF"
                If hidden_LPeriod.Value = String.Empty AndAlso hidden_PGenerate.Value = String.Empty Then
                    'Report Period should be valid
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)

                ElseIf ReportsPagectrlBO.ChkRptRunningForPeriod(RPT_FILENAME, hidden_PGenerate.Value, compId) = True Then
                    'Report already running for given Period
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_REPORT_ALREADY_EXISTS_FORTHAT_DATE)

                ElseIf New Date(Integer.Parse(hidden_PGenerate.Value.Substring(2, 4)), Integer.Parse(hidden_PGenerate.Value.Substring(0, 2)), 1) <= Date.Today.AddMonths(-6) Then
                    'Report Period should not be older than 6 months
                    Throw New GUIException(Message.MSG_INVALID_YEARMONTH, Assurant.ElitaPlus.Common.ErrorCodes.GUI_REPORT_CANNOT_RUN_FORTHAT_DATE)

                ElseIf ReportsPagectrlBO.ChkPeriodIsOpen(hidden_PGenerate.Value, compId) Then
                    'Given Report Period should not be open
                    periodExists = True
                    If ResponseVal <> "2" Then
                        DisplayMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_REPORT_CANNOT_RUN_FORTHAT_DATE, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenRptPromptResponse, True)
                        Exit Sub
                    Else
                        periodExists = True
                        If Not hidden_LPeriod.Value = String.Empty Then
                            PeriodGenerate = hidden_LPeriod.Value
                        Else
                            PeriodGenerate = hidden_PGenerate.Value
                        End If
                    End If
                Else
                    PeriodGenerate = hidden_PGenerate.Value
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, compDesc, viewOption, selectedYearMonth, PeriodGenerate, periodExists, langCode)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try

                GenerateReport("1")

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

    End Class

End Namespace
