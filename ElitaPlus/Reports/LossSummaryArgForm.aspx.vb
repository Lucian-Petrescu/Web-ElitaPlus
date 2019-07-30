Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Public Class LossSummaryArgForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "LossSummaryArg"
        Private Const RPT_FILENAME As String = "ArgentinaLossSummary"
        Private Const RPT_FILENAME_EXPORT As String = "LossSummary_Exp_Arg"

        Public Const NO As String = "N"

        Private Const ONE_ITEM As Integer = 1
        Private Const MTD As String = "M-T-D"
        Private Const QTD As String = "Q-T-D"
        Private Const YTD As String = "Y-T-D"

        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Public Const PAGETITLE As String = "LOSS_SUMMARY_ARGENTINA"
        Public Const PAGETAB As String = "REPORTS"

#End Region

#Region "Properties"
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property

#End Region

#Region "variables"

        Private currentAccountingMonth As Integer
        Private currentAccountingYear As Integer

#End Region


#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not Me.IsPostBack Then

                    currentAccountingMonth = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Date.Today).Month
                    currentAccountingYear = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Date.Today).Year
                    ViewState("CURRENTACCOUNTINGMONTH") = currentAccountingMonth
                    ViewState("CURRENTACCOUNTINGYEAR") = currentAccountingYear
                    InitializeForm()
                    UpdateBreadCrum()
                    TheReportCeInputControl.ExcludeExport()
                    Me.SetFormTab(PAGETAB)
                    ClearErrLabels()
                Else
                    currentAccountingMonth = CType(ViewState("CURRENTACCOUNTINGMONTH"), Integer)
                    currentAccountingYear = CType(ViewState("CURRENTACCOUNTINGYEAR"), Integer)
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateMonthsAndYearsDropdown()

            MonthYearLabel.Text = "* " + MonthYearLabel.Text + ":"
            'TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

        Private Sub UpdateBreadCrum()
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)

        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"
        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(MonthYearLabel)
            Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
        End Sub
#End Region

        '#Region "Handlers-DropDown"

        '        Protected Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
        '             Handles moUserCompanyMultipleDrop.SelectedDropChanged
        '            Try
        '                'PopulateDealerDropDown()
        '                'PopulateCurrencyDropdown()
        '            Catch ex As Exception
        '                HandleErrors(ex, Me.MasterPage.MessageController)
        '            End Try
        '        End Sub

        '#End Region

#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(False, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                'HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                'UserCompanyMultipleDrop.Visible = False
                ' OnFromDrop_Changed(UserCompanyMultipleDrop)
            End If
        End Sub

        Private Sub PopulateMonthsAndYearsDropdown()
           
            Dim MonthList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="MONTH",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            Me.MonthDropDownList.Populate(MonthList.ToArray(),
                                         New PopulateOptions() With
                                         {
                                           .AddBlankItem = True,
                                           .SortFunc = AddressOf PopulateOptions.GetCode
                                         })

            Dim YearList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="ClosingYearsByCompany",
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
                                                                })

            Dim filteredYearList As DataElements.ListItem() = (From x In YearList
                                                               Where x.Description = currentAccountingYear.ToString() Or x.Description = (currentAccountingYear - 1).ToString()
                                                               Select x).ToArray()

            Me.YearDropDownList.Populate(filteredYearList,
                                         New PopulateOptions() With
                                         {
                                           .AddBlankItem = True,
                                           .BlankItemValue = "0",
                                           .ValueFunc = AddressOf PopulateOptions.GetCode,
                                           .TextFunc = AddressOf PopulateOptions.GetDescription
                                         })
        End Sub
#End Region

#Region "Report Generation"
        Function SetParameters(ByVal companyCode As String, ByVal BeginMonthAndYear As String, ByVal EndMonthAndYear As String,
                                 ByVal selectedReportingPeriod As String, ByVal dealerCode As String, dealerForCur As Guid, rptCurrency As Guid) As ReportCeBaseForm.Params
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
                     New ReportCeBaseForm.RptParam("PI_COMPANY_CODE", companyCode),
                     New ReportCeBaseForm.RptParam("PI_REPORTING_PERIOD", selectedReportingPeriod),
                     New ReportCeBaseForm.RptParam("PI_BEGIN_MONTH_AND_YEAR", BeginMonthAndYear),
                     New ReportCeBaseForm.RptParam("PI_END_MONTH_AND_YEAR", EndMonthAndYear),
                     New ReportCeBaseForm.RptParam("PI_DEALER_CODE", dealerCode),
                     New ReportCeBaseForm.RptParam("PI_DEALER_WITH_CUR", DALBase.GuidToSQLString(dealerForCur)),
                     New ReportCeBaseForm.RptParam("PI_RPT_CUR", DALBase.GuidToSQLString(rptCurrency))}

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
            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim EndMonthAndYear As String
            Dim BeginMonthAndYear As String
            Dim selectedReportingPeriod As String
            Dim dealerForCur As Guid = Guid.Empty
            Dim rptCurrency As Guid = Guid.Empty

            Dim selectedYear As String = Me.GetSelectedDescription(Me.YearDropDownList)
            Dim selectedMonthID As Guid = Me.GetSelectedItem(Me.MonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)

            If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(MonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            ElseIf CType(selectedMonth, Integer) > currentAccountingMonth And CType(selectedYear, Integer) >= currentAccountingYear Then
                ElitaPlusPage.SetLabelError(MonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED)
            End If

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            If Me.rdoMTD.Checked Then
                EndMonthAndYear = selectedMonth & selectedYear
                BeginMonthAndYear = EndMonthAndYear
                selectedReportingPeriod = Me.MTD
            ElseIf Me.rdoQTD.Checked Then
                If CType(selectedMonth, Integer) Mod 3 <> 0 Then
                    ElitaPlusPage.SetLabelError(MonthYearLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_MONTH_FOR_QUARTER_END_ERR)
                Else
                    EndMonthAndYear = selectedMonth & selectedYear
                    BeginMonthAndYear = (CType(selectedMonth, Integer) - 2).ToString & selectedYear
                    selectedReportingPeriod = Me.QTD
                End If
            Else
                EndMonthAndYear = selectedMonth & selectedYear
                BeginMonthAndYear = "01" & selectedYear
                selectedReportingPeriod = Me.YTD
            End If
            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, BeginMonthAndYear, EndMonthAndYear, selectedReportingPeriod, String.Empty, Guid.Empty, Guid.Empty)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class
End Namespace