Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Partial Class PaidLossAndCaseReserveSummaryForm
        Inherits ElitaPlusPage


#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "LossSummary"
        Private Const RPT_FILENAME As String = "LossSummary"
        Private Const RPT_FILENAME_EXPORT As String = "LossSummary_Exp"

        Public Const ALL As String = "*"
        ' Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"

        Public Const MAX_LOWER_DATE_DIFF As Integer = -30
        Protected WithEvents Label9 As System.Web.UI.WebControls.Label
        Protected WithEvents Label8 As System.Web.UI.WebControls.Label
        Private Const ONE_ITEM As Integer = 1
        Private Const MTD As String = "M-T-D"
        Private Const QTD As String = "Q-T-D"
        Private Const YTD As String = "Y-T-D"

        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Public Const PAGETITLE As String = "LOSSSUMMARY"
        Public Const PAGETITLEWITHCURRENCY As String = "LOSSSUMMARYWITHCURRENCY"
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

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleDealerDropControl Is Nothing Then
                    multipleDealerDropControl = CType(FindControl("multipleDealerDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDealerDropControl
            End Get
        End Property
#End Region

#Region "variables"
        Private queryStringCaller As String = String.Empty
        Private currentAccountingMonth As Integer
        Private currentAccountingYear As Integer

#End Region

#Region "JavaScript"

        Public Sub JavascriptCalls()
            rdealer.Attributes.Add("onclick", "return TogglelDropDownsSelectionsForCurrencyReports('" + multipleDealerDropControl.CodeDropDown.ClientID + "','" + multipleDealerDropControl.DescDropDown.ClientID + "','" + ddlDealerCurrency.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")
        End Sub
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrControllerMaster.Clear_Hide()
            ClearErrLabels()
            Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not Me.IsPostBack Then

                    currentAccountingMonth = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Date.Today).Month
                    currentAccountingYear = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Date.Today).Year
                    ViewState("CURRENTACCOUNTINGMONTH") = currentAccountingMonth
                    ViewState("CURRENTACCOUNTINGYEAR") = currentAccountingYear
                    InitializeForm()
                    TheReportCeInputControl.ExcludeExport()
                    JavascriptCalls()
                    Me.SetFormTab(PAGETAB)
                Else
                    currentAccountingMonth = CType(ViewState("CURRENTACCOUNTINGMONTH"), Integer)
                    currentAccountingYear = CType(ViewState("CURRENTACCOUNTINGYEAR"), Integer)
                End If
                CheckQuerystringForCurrencyReports()
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)

        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateMonthsAndYearsDropdown()
            PopulateDealerDropDown()
            'TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(MonthYearLabel)
            Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            Me.ClearLabelErrSign(lblCurrency)
        End Sub
#End Region

#Region "Handlers-DropDown"

        Protected Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
             Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
                PopulateCurrencyDropdown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region
#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                'HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                'UserCompanyMultipleDrop.Visible = False
                OnFromDrop_Changed(UserCompanyMultipleDrop)
            End If
        End Sub

        Private Sub PopulateMonthsAndYearsDropdown()
            'Dim dvM As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dvM.Sort = "CODE"
            'Me.BindListControlToDataView(Me.MonthDropDownList, dvM, , , True)

            Dim MonthList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="MONTH",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            Me.MonthDropDownList.Populate(MonthList.ToArray(),
                                         New PopulateOptions() With
                                         {
                                           .AddBlankItem = True,
                                           .SortFunc = AddressOf PopulateOptions.GetCode
                                         })


            'Dim dvY As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'dvY.RowFilter = " DESCRIPTION LIKE '" & currentAccountingYear & "' or DESCRIPTION LIKE '" & currentAccountingYear - 1 & "'"
            'Me.BindListTextToDataView(Me.YearDropDownList, dvY, , , True)

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
                                           .BlankItemValue = "0"
                                         })
        End Sub

        Private Sub PopulateDealerDropDown()
            If Not UserCompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                Dim dv As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;document.forms[0]." + ddlDealerCurrency.ClientID + ".selectedIndex = -1;")
            Else
                Dim dv As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "  " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;document.forms[0]." + ddlDealerCurrency.ClientID + ".selectedIndex = -1;")
            End If
        End Sub

        Private Sub PopulateCurrencyDropdown()
            If Not UserCompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                Dim dv As DataView = LookupListNew.GetCurrenciesForCompanyandDealersInCompanyLookupList(UserCompanyMultipleDrop.SelectedGuid)
                Me.BindListControlToDataView(Me.ddlCurrency, dv, , , True)
                Me.BindListControlToDataView(Me.ddlDealerCurrency, dv, , , True)
            Else
                Dim dv As DataView = LookupListNew.GetCurrenciesForCompanyandDealersInCompanyLookupList(UserCompanyMultipleDrop.SelectedGuid)
                Me.BindListControlToDataView(Me.ddlCurrency, dv, , , True)
                Me.BindListControlToDataView(Me.ddlDealerCurrency, dv, , , True)
            End If
        End Sub

        Private Sub CheckQuerystringForCurrencyReports()
            ShowHideControls(False)
            Me.SetFormTitle(PAGETITLE)

            If (Not Request.QueryString("CALLER") Is Nothing) Then
                If (Request.QueryString("CALLER") = "CR") Then
                    queryStringCaller = Request.QueryString("CALLER")
                    Me.SetFormTitle(PAGETITLEWITHCURRENCY)
                    ShowHideControls(True)
                End If
            End If
        End Sub
#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal companyCode As String, ByVal BeginMonthAndYear As String, ByVal EndMonthAndYear As String, _
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
                    { _
                     New ReportCeBaseForm.RptParam("PI_COMPANY_CODE", companyCode), _
                     New ReportCeBaseForm.RptParam("PI_REPORTING_PERIOD", selectedReportingPeriod), _
                     New ReportCeBaseForm.RptParam("PI_BEGIN_MONTH_AND_YEAR", BeginMonthAndYear), _
                     New ReportCeBaseForm.RptParam("PI_END_MONTH_AND_YEAR", EndMonthAndYear), _
                     New ReportCeBaseForm.RptParam("PI_DEALER_CODE", dealerCode), _
                     New ReportCeBaseForm.RptParam("PI_DEALER_WITH_CUR", DALBase.GuidToSQLString(dealerForCur)), _
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
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode

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

            If (queryStringCaller = "CR") Then
                Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid
                'either of the three options should be selected
                If (rdealer.Checked = False And selectedDealerId.Equals(Guid.Empty) And ddlDealerCurrency.SelectedIndex = 0) Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_REQUIRED)
                End If
                'currency should be selected for every run
                If ddlCurrency.SelectedIndex = 0 Then
                    ElitaPlusPage.SetLabelError(lblCurrency)
                    Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CURRENCY_MUST_BE_SELECTED_ERR)
                Else
                    rptCurrency = New Guid(Me.ddlCurrency.SelectedValue)
                End If

                If ddlDealerCurrency.SelectedIndex > 0 Then
                    dealerForCur = New Guid(Me.ddlDealerCurrency.SelectedValue)
                End If

            End If
            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, BeginMonthAndYear, EndMonthAndYear, selectedReportingPeriod, dealerCode, dealerForCur, rptCurrency)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

#Region "visibility control logic"
        Private Sub ShowHideControls(show As Boolean)
            If (show) Then
                trOnlyDealersWith.Style.Add("display", "block")
                trCurrency.Style.Add("display", "block")
                trHrAfterCurrencyRow.Style.Add("display", "block")
                trSelectAllDealers.Style.Add("display", "block")
                trHrAfterSelectAllDealersRow.Style.Add("display", "block")
            Else
                trOnlyDealersWith.Style.Add("display", "none")
                trCurrency.Style.Add("display", "none")
                trHrAfterCurrencyRow.Style.Add("display", "none")
                trSelectAllDealers.Style.Add("display", "none")
                trHrAfterSelectAllDealersRow.Style.Add("display", "none")
            End If
        End Sub
#End Region

    End Class
End Namespace

