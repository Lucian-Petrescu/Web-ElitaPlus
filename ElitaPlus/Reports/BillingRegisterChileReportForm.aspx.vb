Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class BillingRegisterChileReportForm
        Inherits ElitaPlusPage


#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String
        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "CHILE_BILLING_REGISTER"
        Private Const RPT_FILENAME As String = "BillingRegisterChile"
        Private Const RPT_FILENAME_EXPORT As String = "BillingRegisterChile-Exp"
        Private Const RPT_SUBREPORT As String = "rptRStampTax"

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
        Public Const PAGETITLE As String = "CHILE_BILLING_REGISTER"
        Public Const PAGETITLEWITHCURRENCY As String = "CHILE_BILLING_REGISTER_WITH_CURRENCY"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "CHILE_BILLING_REGISTER"
        Private Const ONE_ITEM As Integer = 1

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
        Private queryStringCaller As String = String.Empty
#End Region

#Region "Properties"

        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
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

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label

        Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-DropDown"

        Protected Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
             Handles multipleDropControl.SelectedDropChanged
            Try
                PopulateDealerDropDown()
                PopulateCurrencyDropdown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "JavaScript"

        Public Sub JavascriptCalls()
            rdealer.Attributes.Add("onclick", "return TogglelDropDownsSelectionsForCurrencyReports('" + multipleDealerDropControl.CodeDropDown.ClientID + "','" + multipleDealerDropControl.DescDropDown.ClientID + "','" + ddlDealerCurrency.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")
        End Sub
#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrControllerMaster.Clear_Hide()
            ClearLabelsErrSign()
            Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            If UserCompanyMultipleDrop.Visible = False Then
                HideHtmlElement(trcomp.ClientID)
            End If
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    JavascriptCalls()
                    InitializeForm()
                End If
                CheckQuerystringForCurrencyReports()
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(lblMonthYear)
                Me.ClearLabelErrSign(lblCompany)
                Me.ClearLabelErrSign(lblCurrency)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
        Private Sub PopulateYearsDropdown()
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId

            Dim YearListLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ClosingYearsByCompany", context:=listcontext)
            Me.moYearDropDownList.Populate(YearListLkl, New PopulateOptions() With
                {
                   .AddBlankItem = True,
                   .BlankItemValue = "0",
                   .ValueFunc = AddressOf .GetCode
                 })

            'Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.moYearDropDownList, dv, , , True)
            'Dim oDescrip As String = Me.GetSelectedDescription(Me.moYearDropDownList)
        End Sub

        Private Sub PopulateMonthsDropdown()
            Dim monthLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            Me.moMonthDropDownList.Populate(monthLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            'Me.BindListControlToDataView(Me.moMonthDropDownList, dv, , , True)
        End Sub
        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, UserCompanyMultipleDrop.NO_CAPTION, True, False)
            If dv.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                OnFromDrop_Changed(UserCompanyMultipleDrop)
            End If
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
            Dim listcontext As ListContext = New ListContext()

            If Not UserCompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                listcontext.CompanyId = UserCompanyMultipleDrop.SelectedGuid
                Dim CurrencyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetCurrencyByCompany", context:=listcontext)

                Me.ddlCurrency.Populate(CurrencyList, New PopulateOptions() With
                {
                   .AddBlankItem = True,
                   .TextFunc = AddressOf .GetCode,
                   .ValueFunc = AddressOf .GetListItemId,
                   .SortFunc = AddressOf .GetExtendedCode
                })

                Me.ddlDealerCurrency.Populate(CurrencyList, New PopulateOptions() With
                {
                   .AddBlankItem = True,
                   .TextFunc = AddressOf .GetCode,
                   .ValueFunc = AddressOf .GetListItemId,
                   .SortFunc = AddressOf .GetExtendedCode
                })

                'Dim dv As DataView = LookupListNew.GetCurrenciesForCompanyandDealersInCompanyLookupList(UserCompanyMultipleDrop.SelectedGuid)
                'Me.BindListControlToDataView(Me.ddlCurrency, dv, , , True)
                'Me.BindListControlToDataView(Me.ddlDealerCurrency, dv, , , True)
            Else
                listcontext.CompanyId = UserCompanyMultipleDrop.SelectedGuid
                Dim CurrencyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetCurrencyByCompany", context:=listcontext)

                Me.ddlCurrency.Populate(CurrencyList, New PopulateOptions() With
                {
                   .AddBlankItem = True,
                   .TextFunc = AddressOf .GetCode,
                   .ValueFunc = AddressOf .GetListItemId,
                   .SortFunc = AddressOf .GetExtendedCode
                })

                Me.ddlDealerCurrency.Populate(CurrencyList, New PopulateOptions() With
                {
                   .AddBlankItem = True,
                   .TextFunc = AddressOf .GetCode,
                   .ValueFunc = AddressOf .GetListItemId,
                   .SortFunc = AddressOf .GetExtendedCode
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
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
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

        Private Sub GenerateReport()
            Dim compDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim compId As Guid = UserCompanyMultipleDrop.SelectedGuid
            Dim selectedYear As String = Me.GetSelectedDescription(Me.moYearDropDownList)
            Dim selectedMonthID As Guid = Me.GetSelectedItem(Me.moMonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim selectedYearMonth As String = selectedYear & selectedMonth
            Dim dealerForCur As Guid = Guid.Empty
            Dim rptCurrency As Guid = Guid.Empty
            Dim dealerCode As String = String.Empty

            If compId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(lblcompany)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            'If currency report is selected, either of Select All Dealers or a particular dealer or only dealers with option AND Currency should be selected 
            'If regular report is selected then either Select All Dealers or a particular dealer should be selected
            If (queryStringCaller = "CR") Then

                If Me.rdealer.Checked Then
                    dealerCode = ALL
                Else
                    dealerCode = DealerMultipleDrop.SelectedCode
                End If

                'either of the three options should be selected
                If (rdealer.Checked = False And dealerCode = String.Empty And ddlDealerCurrency.SelectedIndex = 0) Then
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
            Else
                dealerCode = ALL
            End If
            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, compDesc, selectedYearMonth, dealerCode, dealerForCur, rptCurrency)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Function SetParameters(ByVal companyCode As String, ByVal companyDesc As String, ByVal selectedYearMonth As String, _
                              dealercode As String, dealerForCur As Guid, rptCurrency As Guid) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False)
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            Dim exportData As String = NO
            Dim isSummary As String = YES

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturecode = TheReportCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
                isSummary = NO
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    { _
                     New ReportCeBaseForm.RptParam("PI_COMPANY_CODE", companyCode), _
                     New ReportCeBaseForm.RptParam("PI_DEALER_CODE", dealercode), _
                     New ReportCeBaseForm.RptParam("PI_YEAR_MONTH", selectedYearMonth), _
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

#Region "visibility control logic"
        Private Sub ShowHideControls(show As Boolean)
            If (show) Then
                trOnlyDealersWith.Style.Add("display", "block")
                trCurrency.Style.Add("display", "block")
                trHrAfterCurrencyRow.Style.Add("display", "block")
                trDealer.Style.Add("display", "block")
            Else
                trOnlyDealersWith.Style.Add("display", "none")
                trCurrency.Style.Add("display", "none")
                trHrAfterCurrencyRow.Style.Add("display", "none")
                trDealer.Style.Add("display", "none")
            End If
        End Sub
#End Region
    End Class
End Namespace

