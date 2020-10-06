Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class BrokerCommissionEnglishUSAForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "BROKER COMMISSION"
        Private Const RPT_FILENAME As String = "BrokerCommission"
        Private Const RPT_FILENAME_EXPORT As String = "BrokerCommission_Exp"

        Private Const TOTAL_PARAMS As Integer = 6 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 6 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 6 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

        Public Const PAGETITLE As String = "BROKER COMMISSION"
        Public Const PAGETITLEWITHCURRENCY As String = "BROKER COMMISSION WITH CURRENCY"
        Public Const PAGETAB As String = "REPORTS"
        Public Const ONE_ITEM As Integer = 1
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public userId As String
            Public selectedYearMonth As String
            Public companyCode As String
            Public dealerCode As String
            Public detailCode As String
            Public dealerForCur As String
            Public rptCurrency As String
        End Structure

#End Region

#Region "Properties"
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleDealerDropControl Is Nothing Then
                    multipleDealerDropControl = CType(FindControl("multipleDealerDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDealerDropControl
            End Get
        End Property
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleCompDropControl Is Nothing Then
                    multipleCompDropControl = CType(FindControl("multipleCompDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleCompDropControl
            End Get
        End Property
#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
        Private reportName As String = RPT_FILENAME
        Private queryStringCaller As String = String.Empty
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
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl

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
            ErrControllerMaster.Clear_Hide()
            ClearLabelsErrSign()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not IsPostBack Then
                    JavascriptCalls()
                    SetFormTab(PAGETAB)
                    InitializeForm()
                End If
                CheckQuerystringForCurrencyReports()
                InstallProgressBar()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(MonthYearLabel)
                ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
                ClearLabelErrSign(lblCurrency)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region

#Region "Handlers-DropDown"

        Protected Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
             Handles multipleCompDropControl.SelectedDropChanged
            Try
                rbnSelectAllComp.Checked = False
                PopulateDealerDropDown()
                PopulateCurrencyDropdown()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub OnFromDealerDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
             Handles multipleDealerDropControl.SelectedDropChanged
            Try
                If multipleDealerDropControl.SelectedCode <> "" Or multipleDealerDropControl.SelectedDesc <> "" Then
                    rbnSelectAllComp.Checked = False
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Populate"
        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, UserCompanyMultipleDrop.NO_CAPTION, True, False)
            If dv.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                OnFromDrop_Changed(UserCompanyMultipleDrop)
            End If
        End Sub
        'ALR Need to create new method to pull full list based on multiple company membership
        Private Sub PopulateDealerDropDown()
            If UserCompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                Dim dv As DataView = LookupListNew.GetDealerCommissionBreakDownLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;document.forms[0]." + ddlDealerCurrency.ClientID + ".selectedIndex = -1;")
            Else
                Dim dv As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "  " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;document.forms[0]." + ddlDealerCurrency.ClientID + ".selectedIndex = -1;")
            End If
        End Sub

        Private Sub PopulateYearsDropdown()
            Dim listcontext As ListContext = New ListContext()
            listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id

            Dim YearListLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetClosingYearsByUser", context:=listcontext)
            YearDropDownList.Populate(YearListLkl, New PopulateOptions() With
                {
                   .AddBlankItem = True,
                   .BlankItemValue = "0",
                   .ValueFunc = AddressOf .GetCode
                })

            'Dim dv As DataView = AccountingCloseInfo.GetClosingYearsByUser(ElitaPlusIdentity.Current.ActiveUser.Id)
            'Me.BindListTextToDataView(Me.YearDropDownList, dv, , , True)
        End Sub

        Private Sub PopulateMonthsDropdown()
            Dim monthLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            MonthDropDownList.Populate(monthLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            'Me.BindListControlToDataView(Me.MonthDropDownList, dv, , , True)
        End Sub

        Private Sub PopulateCurrencyDropdown()
            If Not UserCompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyId = UserCompanyMultipleDrop.SelectedGuid
                Dim CurrencyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetCurrencyByCompany", context:=listcontext)

                ddlCurrency.Populate(CurrencyList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                ddlDealerCurrency.Populate(CurrencyList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Dim dv As DataView = LookupListNew.GetCurrenciesForCompanyandDealersInCompanyLookupList(UserCompanyMultipleDrop.SelectedGuid)
                'Me.BindListControlToDataView(Me.ddlCurrency, dv, , , True)
                'Me.BindListControlToDataView(Me.ddlDealerCurrency, dv, , , True)
            Else
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyId = UserCompanyMultipleDrop.SelectedGuid
                Dim CurrencyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetCurrencyByCompany", context:=listcontext)

                ddlCurrency.Populate(CurrencyList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                ddlDealerCurrency.Populate(CurrencyList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

                'Dim dv As DataView = LookupListNew.GetCurrenciesForCompanyandDealersInCompanyLookupList(UserCompanyMultipleDrop.SelectedGuid)
                'Me.BindListControlToDataView(Me.ddlCurrency, dv, , , True)
                'Me.BindListControlToDataView(Me.ddlDealerCurrency, dv, , , True)
            End If
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
            'PopulateCurrencyDropdownByUser()
            'TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

        Private Sub CheckQuerystringForCurrencyReports()
            ShowHideControls(False)
            SetFormTitle(PAGETITLE)

            If (Request.QueryString("CALLER") IsNot Nothing) Then
                If (Request.QueryString("CALLER") = "CR") Then
                    queryStringCaller = Request.QueryString("CALLER")
                    SetFormTitle(PAGETITLEWITHCURRENCY)
                    ShowHideControls(True)
                End If
            End If
        End Sub

#End Region

#Region "Crystal Enterprise"

        Private Sub GenerateReport()

            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim selectedYear As String = GetSelectedDescription(YearDropDownList)
            Dim selectedMonthID As Guid = GetSelectedItem(MonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim selectedYearMonth As String = selectedYear & selectedMonth
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)
            'Dim dvDealer As DataView = LookupListNew.GetDealerCommissionBreakDownLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim companyCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim detailCode As String
            Dim dealerForCur As Guid = Guid.Empty
            Dim rptCurrency As Guid = Guid.Empty
            Dim params As ReportCeBaseForm.Params

            If RadiobuttonTotalsOnly.Checked Then
                detailCode = YES
            Else
                detailCode = NO
            End If

            'Validating the MonthYear selection
            If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(MonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If
            'If currency report is selected, either of Select All Dealers or a particular dealer or only dealers with option AND Currency should be selected 
            'If regular report is selected then either Select All Dealers or a particular dealer should be selected
            If (queryStringCaller = "CR") Then

                'if Select All Companies is checked then comany and dealer are passed null
                If Not rbnSelectAllComp.Checked Then

                    'either of the three options should be selected
                    If (rdealer.Checked = False And selectedDealerId.Equals(Guid.Empty) And ddlDealerCurrency.SelectedIndex = 0) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_REQUIRED)
                    End If

                    If ddlDealerCurrency.SelectedIndex > 0 Then
                        dealerForCur = New Guid(ddlDealerCurrency.SelectedValue)
                    End If
                Else
                    companyCode = String.Empty

                End If

                'currency should be selected for every run
                If ddlCurrency.SelectedIndex = 0 Then
                    ElitaPlusPage.SetLabelError(lblCurrency)
                    Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CURRENCY_MUST_BE_SELECTED_ERR)
                Else
                    rptCurrency = New Guid(ddlCurrency.SelectedValue)
                End If

            End If

            If rdealer.Checked Then
                dealerCode = ALL
            Else
                'Validating the dealer selection
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                detailCode = NO
                params = SetExpParameters(userId, selectedYearMonth, dealerCode, detailCode, dealerForCur, rptCurrency, companyCode)
            Else
                'View Report
                params = SetParameters(userId, dealerCode, selectedYearMonth, detailCode, dealerForCur, rptCurrency, companyCode)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetParameters(userId As String, dealerCode As String, selectedYearMonth As String, _
                               detailCode As String, dealerForCur As Guid, rptCurrency As Guid, companyCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim rptParams As ReportParams
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam

            With rptParams
                .userId = userId
                .companyCode = companyCode
                .selectedYearMonth = selectedYearMonth
                .dealerCode = dealerCode
                .detailCode = detailCode
                .dealerForCur = DALBase.GuidToSQLString(dealerForCur)
                .rptCurrency = DALBase.GuidToSQLString(rptCurrency)
            End With

            Dim exportData As String = NO

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                detailCode = YES
                exportData = YES
                reportName = RPT_FILENAME_EXPORT
            End If

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

        Function SetExpParameters(userId As String, selectedYearMonth As String, dealerCode As String, _
                                detailCode As String, dealerForCur As Guid, rptCurrency As Guid, companyCode As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams

            With rptParams
                .userId = userId
                .companyCode = companyCode
                .selectedYearMonth = selectedYearMonth
                .dealerCode = dealerCode
                .detailCode = detailCode
                .dealerForCur = DALBase.GuidToSQLString(dealerForCur)
                .rptCurrency = DALBase.GuidToSQLString(rptCurrency)
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            reportName = RPT_FILENAME_EXPORT

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam, _
                            rptName As String, startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_KEY", .userId, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_COMPANY_CODE", .companyCode, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DEALER_FOR_CUR", .dealerForCur, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_RPT_CUR", .rptCurrency, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_YEAR_MONTH", .selectedYearMonth, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_IS_SUMMARY", .detailCode, rptName)
            End With

        End Sub

#End Region

#Region "visibility control logic"
        Private Sub ShowHideControls(show As Boolean)
            If (show) Then
                trOnlyDealersWith.Style.Add("display", "block")
                trCurrency.Style.Add("display", "block")
                trHrAfterCurrencyRow.Style.Add("display", "block")
                trSelectAllComp.Style.Add("display", "block")
                trcomp.Style.Add("display", "block")
            Else
                trOnlyDealersWith.Style.Add("display", "none")
                trCurrency.Style.Add("display", "none")
                trHrAfterCurrencyRow.Style.Add("display", "none")
                trSelectAllComp.Style.Add("display", "none")
                trcomp.Style.Add("display", "none")
            End If
        End Sub
#End Region

        Private Sub rbnSelectAllComp_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnSelectAllComp.CheckedChanged
            If rbnSelectAllComp.Checked Then
                'remove the company selection
                UserCompanyMultipleDrop.SelectedIndex = -1
                UserCompanyMultipleDrop.SelectedGuid = Guid.Empty
                'clear all dealers
                PopulateDealerDropDown()
                'PopulateCurrencyDropdownByUser()
                rdealer.Checked = True
                ddlDealerCurrency.Items.Clear()
            End If
        End Sub
    End Class

End Namespace

