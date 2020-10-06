Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Partial Class BillingRegisterReportForm
        Inherits ElitaPlusPage


#Region "Parameters"

#End Region

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "Billing_Register"
        Private Const RPT_FILENAME As String = "BillingRegisterArgentina"
        Private Const RPT_FILENAME_EXPORT As String = "BillingRegisterArgentina-Exp"
        Private Const RPT_SUBREPORT As String = "rptRStampTax"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const PAGETITLE As String = "Billing_Register"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "Billing_Register"

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "Properties"

#End Region

#Region "Handlers-DropDown"

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()

                    InitializeForm()
                    AddCalendar(BtnBeginDate, txtBeginDate)
                    AddCalendar(BtnEndDate, txtEndDate)
                    JavascriptCalls()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(lblMonth)
                ClearLabelErrSign(lblYear)
                ClearLabelErrSign(lblEndDate)
                ClearLabelErrSign(lblBeginDate)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub JavascriptCalls()

            moMonthList.Attributes.Add("onchange", "return ClearDateSelection('" + txtBeginDate.ClientID + "', '" + txtEndDate.ClientID + "') ;")
            moYearList.Attributes.Add("onchange", "return ClearDateSelection('" + txtBeginDate.ClientID + "', '" + txtEndDate.ClientID + "') ;")

            txtBeginDate.Attributes.Add("onfocus", "return ClearMonthYearSelection('" + moMonthList.ClientID + "', '" + moYearList.ClientID + "') ;")
            txtEndDate.Attributes.Add("onfocus", "return ClearMonthYearSelection('" + moMonthList.ClientID + "', '" + moYearList.ClientID + "') ;")

            txtBeginDate.Attributes.Add("onchange", "return ClearMonthYearSelection('" + moMonthList.ClientID + "', '" + moYearList.ClientID + "') ;")
            txtEndDate.Attributes.Add("onchange", "return ClearMonthYearSelection('" + moMonthList.ClientID + "', '" + moYearList.ClientID + "') ;")

        End Sub

        Private Sub PopulateYearsDropdown()
            ' Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.moYearList, dv, , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            moYearList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .ValueFunc = AddressOf PopulateOptions.GetCode
                  })
            Dim oDescrip As String = GetSelectedDescription(moYearList)
        End Sub

        Private Sub PopulateMonthsDropdown()
            ' Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            'Me.BindListControlToDataView(Me.moMonthList, dv, , , True, False)
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            moMonthList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        End Sub

        Private Sub InitializeForm()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub
        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Billing_Register")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Billing_Register")
        End Sub

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GenerateReport()
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)
            Dim compDesc As String = LookupListNew.GetDescriptionFromId("COMPANIES", oCompanyId)
            Dim oCompanyGrpId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim endDate As String
            Dim beginDate As String
            Dim reportBasedOn As String = rdReportBasedOn.SelectedValue()

            Dim selectedYear As String = GetSelectedDescription(moYearList)
            Dim selectedMonthID As Guid = GetSelectedItem(moMonthList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim selectedYearMonth As String = selectedYear & selectedMonth

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

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, compDesc, selectedYearMonth, beginDate, endDate, reportBasedOn)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetParameters(companyCode As String, companyDesc As String,
                               selectedYearMonth As String, BeginDate As String, EndDate As String,
                               reportBasedOn As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False)
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            Dim exportData As String = NO
            Dim isSummary As String = YES

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturecode = TheReportCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID))
                isSummary = NO
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_COMPANY", companyCode),
                     New ReportCeBaseForm.RptParam("V_COMPANY_DESC", companyDesc),
                     New ReportCeBaseForm.RptParam("V_DEALER", ALL),
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
