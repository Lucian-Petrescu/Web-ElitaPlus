Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class BillingRegisterDetailReportForm
        Inherits ElitaPlusPage


#Region "Parameters"

        'Public Structure ReportParams
        '    Public companyCode As String
        '    Public beginDate As String
        '    Public endDate As String
        'End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Billing_Register_Detail"
        Private Const RPT_FILENAME As String = "BillingRegisterDetailArgentina"
        Private Const RPT_FILENAME_EXPORT As String = "BillingRegisterDetailArgentina-Exp"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const PAGETITLE As String = "Billing_Register_Detail"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "Billing_Register_Detail"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "Properties"

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl_New)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property

        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl_New)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property
#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                rdealer.Checked = True
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label

        ' Protected WithEvents MasterPage.MessageController As ErrorController
        ' Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label
        ' Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl_New
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            Me.ClearLabelsErrSign()
            Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    'Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETAB)
                    'Me.MasterPage.PageForm = TranslationBase.TranslateLabelOrMessage("Billing Register Detail")
                    UpdateBreadCrum()

                    InitializeForm()
                    Me.AddCalendar(Me.BtnBeginDate, Me.txtBeginDate)
                    Me.AddCalendar(Me.BtnEndDate, Me.txtEndDate)
                    JavascriptCalls()
                End If
                If rdealer.Checked Then DealerMultipleDrop.SelectedIndex = NOTHING_SELECTED
                Me.InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(lblMonth)
                Me.ClearLabelErrSign(lblYear)
                Me.ClearLabelErrSign(lblEndDate)
                Me.ClearLabelErrSign(lblBeginDate)
                Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
                Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub JavascriptCalls()
            rdealer.Attributes.Add("onclick", "return ToggleDualDropDownsSelection('" + DealerMultipleDrop.CodeDropDown.ClientID + "','" + DealerMultipleDrop.DescDropDown.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")

            moMonthList.Attributes.Add("onchange", "return ClearDateSelection('" + txtBeginDate.ClientID + "', '" + txtEndDate.ClientID + "') ;")
            moYearList.Attributes.Add("onchange", "return ClearDateSelection('" + txtBeginDate.ClientID + "', '" + txtEndDate.ClientID + "') ;")

            txtBeginDate.Attributes.Add("onfocus", "return ClearMonthYearSelection('" + moMonthList.ClientID + "', '" + moYearList.ClientID + "') ;")
            txtEndDate.Attributes.Add("onfocus", "return ClearMonthYearSelection('" + moMonthList.ClientID + "', '" + moYearList.ClientID + "') ;")

            txtBeginDate.Attributes.Add("onchange", "return ClearMonthYearSelection('" + moMonthList.ClientID + "', '" + moYearList.ClientID + "') ;")
            txtEndDate.Attributes.Add("onchange", "return ClearMonthYearSelection('" + moMonthList.ClientID + "', '" + moYearList.ClientID + "') ;")

        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, ALL + " " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub
        Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")
        End Sub

        Private Sub PopulateYearsDropdown()
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString)

            Dim YearListLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ClosingYearsByCompany", context:=listcontext)
            Me.moYearList.Populate(YearListLkl, New PopulateOptions() With
                {
                   .AddBlankItem = True,
                   .BlankItemValue = "0",
                   .ValueFunc = AddressOf .GetCode
                })
        End Sub

        Private Sub PopulateMonthsDropdown()
            Dim monthLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            Me.moMonthList.Populate(monthLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf .GetCode
                })
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub
        Private Sub UpdateBreadCrum()
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Billing_Register_Detail")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Billing_Register_Detail")
        End Sub

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GenerateReport()
            Dim compId As Guid
            Dim compCode As String
            Dim compDesc As String
            Dim endDate As String
            Dim beginDate As String
            Dim reportBasedOn As String = Me.rdReportBasedOn.SelectedValue()

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            Else
                compCode = UserCompanyMultipleDrop.SelectedCode
                compDesc = UserCompanyMultipleDrop.SelectedDesc
                compId = UserCompanyMultipleDrop.SelectedGuid
            End If

            Dim selectedYear As String = Me.GetSelectedDescription(Me.moYearList)
            Dim selectedMonthID As Guid = Me.GetSelectedItem(Me.moMonthList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim selectedYearMonth As String = selectedYear & selectedMonth

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode

            'Validating the month and year
            If (selectedMonthID.Equals(Guid.Empty) AndAlso selectedYear.Equals(String.Empty) AndAlso txtBeginDate.Text.Equals(String.Empty) AndAlso txtEndDate.Text.Equals(String.Empty)) Then
                ElitaPlusPage.SetLabelError(lblMonth)
                ElitaPlusPage.SetLabelError(lblYear)
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
            ElseIf ((Not selectedMonthID.Equals(Guid.Empty) And selectedYear.Equals(String.Empty)) Or (selectedMonthID.Equals(Guid.Empty) And Not selectedYear.Equals(String.Empty))) Then
                ElitaPlusPage.SetLabelError(lblMonth)
                ElitaPlusPage.SetLabelError(lblYear)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_MONTH_YEAR_SELECTION_ERROR)
            ElseIf ((Not txtBeginDate.Text.Equals(String.Empty) And txtEndDate.Text.Equals(String.Empty)) Or (txtBeginDate.Text.Equals(String.Empty) And Not txtEndDate.Text.Equals(String.Empty))) Then
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
            ElseIf (Not selectedMonthID.Equals(Guid.Empty) And Not selectedYear.Equals(String.Empty)) Then
                selectedYearMonth = selectedYear & selectedMonth
            Else
                ReportCeBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
                endDate = ReportCeBase.FormatDate(lblEndDate, txtEndDate.Text)
                beginDate = ReportCeBase.FormatDate(lblBeginDate, txtBeginDate.Text)

                If DateHelper.GetDateValue(txtEndDate.Text) >= DateHelper.GetDateValue(DateTime.Today.Date.ToString) Then
                    ElitaPlusPage.SetLabelError(lblEndDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_MUST_NOT_BE_HIGHER_THAN_YESTERDAY_ERR)
                End If

                Dim ts As TimeSpan
                ts = DateHelper.GetDateValue(txtEndDate.Text).Subtract(DateHelper.GetDateValue(txtBeginDate.Text))
                If ts.Days > 45 Then
                    ElitaPlusPage.SetLabelError(lblEndDate)
                    ElitaPlusPage.SetLabelError(lblBeginDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DATE_DIFFERENCE_CANNOT_BE_GREATER_THAN_45_ERR)
                End If
            End If

            'Validating the Dealer selection
            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If dealerID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If

            End If

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, compId, dealerCode, selectedYearMonth, beginDate, endDate, reportBasedOn)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetParameters(ByVal companyCode As String, ByVal companyId As Guid, ByVal dealerCode As String,
                               ByVal selectedYearMonth As String, ByVal BeginDate As String, ByVal EndDate As String,
                               ByVal reportBasedOn As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False)
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            Dim exportData As String = NO
            Dim isSummary As String = YES

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturecode = TheReportCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(companyId))
                isSummary = NO
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_COMPANY", companyCode),
                     New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                     New ReportCeBaseForm.RptParam("V_YEAR_MONTH", selectedYearMonth),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", BeginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", EndDate),
                     New ReportCeBaseForm.RptParam("V_REPORT_BASED_ON", reportBasedOn),
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
    End Class
End Namespace
