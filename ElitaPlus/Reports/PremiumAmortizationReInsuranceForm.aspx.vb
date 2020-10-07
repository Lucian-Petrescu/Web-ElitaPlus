Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Public Class PremiumAmortizationReInsuranceForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "PREMIUM AMORTIZATION REINSURANCE"
        Private Const RPT_FILENAME As String = "PremiumAmortizationReinsurance"
        Private Const RPT_FILENAME_EXPORT As String = "PremiumAmortizationReinsurance-Exp"
        Private Const RPT_FILENAME_ADMIN As String = "PremiumAmortizationAdminReinsurance"
        Private Const RPT_FILENAME_ADMIN_EXPORT As String = "PremiumAmortizationAdminReinsurance-Exp"
        Private Const COMPANY_CODE_FOR_BRAZIL As String = "ABR"
        Private Const RPT_FILENAME_ADMIN_COVERAGE As String = "PremiumAmortizationAdminCov"
        Private Const TOTAL_PARAMS As Integer = 21 ' 12 Elements    ' 10 Elements before without addldac
        Private Const TOTAL_EXP_PARAMS As Integer = 11 ' 6 Elements  '' ' 4 - 5 Elements before without addldac
        Private Const PARAMS_PER_REPORT As Integer = 11 ' 6 Elements '' ' 5 - 5 Elements before without addldac
        Private Const TOTAL_PARAMS_BY_COV As Integer = 29
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "D"
        Private Const NO As String = "N"
        Private Const ONE_ITEM As Integer = 1
        Private Const BRAZIL_CODE As String = "BR"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const LABEL_OR_A_SINGLE_DEALER As String = "OR A SINGLE DEALER"
        Public Const PAGETITLE As String = "Premium Amortization"
        Public Const PAGETITLEWITHCURRENCY As String = "Premium Amortization Reinsurance WITH CURRENCY"
        Public Const PAGETAB As String = "REPORTS"

#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public selectedYearMonth As String
            Public dealerCode As String
            Public detailCode As String
            Public groupCode As String
            Public addlDAC As String
            Public culturevalue As String
            Public langCode As String
            Public dealerForCur As String
            Public rptCurrency As String
            Public exchangeRateCode As String
        End Structure

#End Region

#Region "Properties"

        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property

        Public ReadOnly Property UserDealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserDealerMultipleDrop Is Nothing Then
                    moUserDealerMultipleDrop = CType(FindControl("moUserDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserDealerMultipleDrop
            End Get
        End Property
#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Private rptName As String = RPT_FILENAME
        Dim oCompany As Company
        Dim DAC_CODE As String
        Private queryStringCaller As String = String.Empty
#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents rdReportFormat_NO_TRANSLATE As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents moExportDropDownList As System.Web.UI.WebControls.DropDownList
        Protected WithEvents rdReportType As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents ImageButtonBeginDate As System.Web.UI.WebControls.ImageButton
        Protected WithEvents ImageButtonEndDate As System.Web.UI.WebControls.ImageButton

        Protected WithEvents moEndDateLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moEndDateText As System.Web.UI.WebControls.TextBox
        Protected WithEvents BtnEndDate As System.Web.UI.WebControls.ImageButton
        Protected WithEvents moBeginDateLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moBeginDateText As System.Web.UI.WebControls.TextBox
        Protected WithEvents BtnBeginDate As System.Web.UI.WebControls.ImageButton
        Protected WithEvents moDealerLabel As System.Web.UI.WebControls.Label
        Protected WithEvents Radiobutton1 As System.Web.UI.WebControls.RadioButton
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Protected WithEvents moUserCompanyMultipleDrop As MultipleColumnDDLabelControl
        Protected WithEvents moUserDealerMultipleDrop As MultipleColumnDDLabelControl

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
                    SetFormTab(PAGETAB)
                    InitializeForm()
                    TheReportCeInputControl.SetExportOnly()
                Else
                    ClearLabelsErrSign()
                    EnableOrDisableControls()
                End If
                CheckQuerystringForCurrencyReports()
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)

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

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerGroup()
                PopulateCurrencyDropdown()
                PopulateDealerDropDown()
                EnableOrDisableControls()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region

#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                'UserCompanyMultipleDrop.Visible = False
                EnableOrDisableControls()
                OnFromDrop_Changed(UserCompanyMultipleDrop)
            End If
        End Sub

        Private Sub PopulateDealerGroup()
            'Me.BindListControlToDataView(cboDealerGroup, LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            Dim dealerGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("DealerGroupByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            cboDealerGroup.Populate(dealerGroupLkl, New PopulateOptions() With
             {
            .AddBlankItem = True
             })
        End Sub

        Private Sub PopulateDealerDropDown()
            '''Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString)))
            '''Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
            '''DealersLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboDealerCode, DealersLookupListSortedByCode, "CODE", , True)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            UserDealerMultipleDrop.NothingSelected = True
            UserDealerMultipleDrop.SetControl(False,
                                              UserDealerMultipleDrop.MODES.NEW_MODE,
                                              True,
                                              dv,
                                              TranslationBase.TranslateLabelOrMessage(LABEL_OR_A_SINGLE_DEALER),
                                              True,
                                              False,
                                              " ctl00_ContentPanelMainContentBody_rdealer.checked = false;  ctl00_ContentPanelMainContentBody_rGroup.checked = false; ctl00_ContentPanelMainContentBody_cboDealerGroup.selectedIndex = -1;ctl00_ContentPanelMainContentBody_ddlDealerCurrency.selectedIndex = -1; fncEnable(2);",
                                              "ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDrop",
                                              "ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDropDesc", "ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_lb_DropDown")

        End Sub

        Private Sub PopulateYearsDropdown()
            ' Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            ' Me.BindListTextToDataView(Me.YearDropDownList, dv, , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId

            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            YearDropDownList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .ValueFunc = AddressOf PopulateOptions.GetCode,
            .BlankItemValue = "0"
                  })
        End Sub

        Private Sub PopulateMonthsDropdown()
            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            '  Me.BindListControlToDataView(Me.MonthDropDownList, dv, , , True)
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            MonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        End Sub

        Private Sub PopulateCurrencyDropdown()
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = UserCompanyMultipleDrop.SelectedGuid
            Dim currLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("GetCurrencyByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Dim currTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                             Return li.Code + " " + "(" + li.Translation + ")"
                                                                         End Function
            If Not UserCompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                ' Dim dv As DataView = LookupListNew.GetCurrenciesForCompanyandDealersInCompanyLookupList(UserCompanyMultipleDrop.SelectedGuid)
                'Me.BindListControlToDataView(Me.ddlCurrency, dv, , , True)
                ddlCurrency.Populate(currLkl, New PopulateOptions() With
                {
               .AddBlankItem = True,
               .TextFunc = currTextFunc,
               .SortFunc = currTextFunc
               })
                ' Me.BindListControlToDataView(Me.ddlDealerCurrency, dv, , , True)
                ddlDealerCurrency.Populate(currLkl, New PopulateOptions() With
              {
              .AddBlankItem = True,
              .TextFunc = currTextFunc,
              .SortFunc = currTextFunc
             })
            Else
                '  Dim dv As DataView = LookupListNew.GetCurrenciesForCompanyandDealersInCompanyLookupList(UserCompanyMultipleDrop.SelectedGuid)
                ' Me.BindListControlToDataView(Me.ddlCurrency, dv, , , True)
                ddlCurrency.Populate(currLkl, New PopulateOptions() With
                {
               .AddBlankItem = True,
               .TextFunc = currTextFunc,
               .SortFunc = currTextFunc
               })
                ' Me.BindListControlToDataView(Me.ddlDealerCurrency, dv, , , True)
                ddlDealerCurrency.Populate(currLkl, New PopulateOptions() With
               {
               .AddBlankItem = True,
               .TextFunc = currTextFunc,
               .SortFunc = currTextFunc
              })
            End If
        End Sub

        Private Sub InitializeForm()
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
            PopulateCompaniesDropdown()
            PopulateDealerGroup()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
            rdealer.Checked = True
            RadiobuttonTotalsOnly.Checked = True
            ControlMgr.SetVisibleControl(Me, chkTotalsPageByCov, False)
            ControlMgr.SetVisibleControl(Me, lblTotalsByCov, False)
            'trCoverage.Style.Add("display", "none")
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(MonthYearLabel)
                ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
                ClearLabelErrSign(UserDealerMultipleDrop.CaptionLabel)
                ClearLabelErrSign(GroupLabel)
                ClearLabelErrSign(lblCurrency)
                ClearLabelErrSign(lblExchangeRateDate)

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub CheckQuerystringForCurrencyReports()
            ShowHideControls(False)
            SetFormTitle(PAGETITLE)
            hdnQuerystring.Value = ""
            If (Request.QueryString("CALLER") IsNot Nothing) Then
                If (Request.QueryString("CALLER") = "CR") Then
                    hdnQuerystring.Value = "CR"
                    queryStringCaller = Request.QueryString("CALLER")
                    SetFormTitle(PAGETITLEWITHCURRENCY)
                    ShowHideControls(True)
                End If
            End If
        End Sub

        Private Sub EnableOrDisableControls()

            'oCompany = New Company(UserCompanyMultipleDrop.SelectedGuid)
            'DAC_CODE = LookupListNew.GetCodeFromId(LookupListNew.LK_ADDL_DAC, oCompany.AddlDACId)
            'hidden_dacvalue.Value = DAC_CODE

            'If (DAC_CODE = Codes.ADDL_DAC__NONE) Then
            '    Me.chkTotalsPageByCov.Visible = False
            '    Me.lblTotalsByCov.Visible = False

            'ElseIf (DAC_CODE <> Codes.ADDL_DAC__NONE) Then ' And rGroup.Checked = True Or cboDealerGroup.SelectedItem.ToString <> String.Empty) Then
            '    Me.chkTotalsPageByCov.Visible = True
            '    Me.lblTotalsByCov.Visible = True
            '    Me.chkTotalsPageByCov.Enabled = True
            '    Me.lblTotalsByCov.Enabled = True
            'End If
            If (Not UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty)) Then
                oCompany = New Company(UserCompanyMultipleDrop.SelectedGuid)
                DAC_CODE = LookupListNew.GetCodeFromId(LookupListNew.LK_ADDL_DAC, oCompany.AddlDACId)
                hidden_dacvalue.Value = DAC_CODE
                If (DAC_CODE <> Codes.ADDL_DAC__NONE) Then
                    'Me.chkTotalsPageByCov.Visible = True
                    'Me.lblTotalsByCov.Visible = True
                    trCoverage.Style.Add("display", "block")
                    If rGroup.Checked = True Or GetSelectedItem(cboDealerGroup) <> Guid.Empty Then ' cboDealerGroup.SelectedItem.ToString <> String.Empty Then
                        chkTotalsPageByCov.Enabled = True
                        lblTotalsByCov.Enabled = True
                    Else
                        chkTotalsPageByCov.Enabled = False
                        lblTotalsByCov.Enabled = False
                    End If
                Else
                    'Me.chkTotalsPageByCov.Visible = False
                    'Me.lblTotalsByCov.Visible = False
                    trCoverage.Style.Add("display", "none")
                End If
            End If
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(companyCode As String, selectedYearMonth As String, dealerCode As String,
                               detailCode As String, selectedGroupId As String, addlDACCode As String, langCode As String,
                               dealerForCur As Guid, rptCurrency As Guid, exchangeRateCode As String) As ReportCeBaseForm.Params
            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturevalue As String = TheReportCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheReportCeInputControl.getReportName(rptName, False)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            With rptParams
                .companyCode = companyCode
                .dealerCode = dealerCode
                .selectedYearMonth = selectedYearMonth
                .detailCode = detailCode
                .groupCode = selectedGroupId
                .addlDAC = addlDACCode
                .langCode = langCode
                .culturevalue = culturevalue
                .dealerForCur = DALBase.GuidToSQLString(dealerForCur)
                .rptCurrency = DALBase.GuidToSQLString(rptCurrency)
                .exchangeRateCode = exchangeRateCode
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report

            rptParams.detailCode = NO

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function


        Function SetTotalByCovParameters(companyCode As String, selectedYearMonth As String, dealerCode As String,
                               detailCode As String, selectedGroupId As String, addlDACCode As String, langCode As String,
                                dealerForCur As Guid, rptCurrency As Guid, exchangeRateCode As String) As ReportCeBaseForm.Params
            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS_BY_COV) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturevalue As String = TheReportCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheReportCeInputControl.getReportName(rptName, False)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            With rptParams
                .companyCode = companyCode
                .dealerCode = dealerCode
                .selectedYearMonth = selectedYearMonth
                .detailCode = detailCode
                .groupCode = selectedGroupId
                .addlDAC = addlDACCode
                .langCode = langCode
                .culturevalue = culturevalue
                .dealerForCur = DALBase.GuidToSQLString(dealerForCur)
                .rptCurrency = DALBase.GuidToSQLString(rptCurrency)
                .exchangeRateCode = exchangeRateCode
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report

            rptParams.detailCode = NO

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams

            End With

            Return params
        End Function

        Private Sub GenerateReport()

            'Dim companyId As Guid = Me.GetApplicationUser.CompanyID
            'Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", companyId)
            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim selectedYear As String = GetSelectedDescription(YearDropDownList)
            Dim selectedMonthID As Guid = GetSelectedItem(MonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim selectedYearMonth As String = selectedYear & selectedMonth
            Dim selectedDealerId As Guid = UserDealerMultipleDrop.SelectedGuid
            Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim detailCode As String
            Dim addlDACCode As String
            Dim selectByGroup As String
            Dim selectedGroupId As Guid = GetSelectedItem(cboDealerGroup)
            Dim params As ReportCeBaseForm.Params
            Dim compid As Guid = UserCompanyMultipleDrop.SelectedGuid
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGES, langId)
            Dim dealerForCur As Guid = Guid.Empty
            Dim rptCurrency As Guid = Guid.Empty
            Dim exchangeRateCode As String = rblAccountingPeriod.SelectedValue

            If RadiobuttonTotalsOnly.Checked Then
                detailCode = NO
            Else
                detailCode = YES
            End If

            'Validating the Year-Month selection
            If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(MonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'If currency report is selected, either of Select All Dealers or a particular dealer or only dealers with option AND Currency should be selected 
            'If regular report is selected then either Select All Dealers or a particular dealer should be selected
            If (queryStringCaller = "CR") Then
                'either of the three options should be selected
                If (Not rdealer.Checked AndAlso Not rGroup.Checked AndAlso selectedDealerId.Equals(Guid.Empty) AndAlso ddlDealerCurrency.SelectedIndex = 0 AndAlso selectedGroupId.Equals(Guid.Empty)) Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_GROUP_MUST_BE_SELECTED_ERR)
                End If
                'currency should be selected for every run
                If ddlCurrency.SelectedIndex = 0 Then
                    ElitaPlusPage.SetLabelError(lblCurrency)
                    Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CURRENCY_MUST_BE_SELECTED_ERR)
                Else
                    rptCurrency = New Guid(ddlCurrency.SelectedValue)
                End If

                'exchange rate option should be selected
                If (rblAccountingPeriod.SelectedIndex = -1) Then
                    ElitaPlusPage.SetLabelError(lblExchangeRateDate)
                    Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CURRENCY_MUST_BE_SELECTED_ERR)
                End If

                If ddlDealerCurrency.SelectedIndex > 0 Then
                    dealerForCur = New Guid(ddlDealerCurrency.SelectedValue)
                End If

            Else

                'Validating the Dealer_Group selection
                If Not rdealer.Checked AndAlso Not rGroup.Checked AndAlso selectedDealerId.Equals(Guid.Empty) AndAlso selectedGroupId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(GroupLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_GROUP_MUST_BE_SELECTED_ERR)
                End If

                'Validating the Dealer selection
                selectByGroup = NO
                If rdealer.Checked Then
                    dealerCode = ALL
                ElseIf rGroup.Checked Then
                    selectByGroup = ALL
                    dealerCode = ALL
                ElseIf selectedDealerId.Equals(Guid.Empty) And selectedGroupId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(UserDealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If

                If Not selectedGroupId.Equals(Guid.Empty) Then
                    selectByGroup = DALBase.GuidToSQLString(selectedGroupId)
                    dealerCode = DALBase.GuidToSQLString(selectedGroupId)
                End If
            End If

            ''''''''''''''''''''''''''''''
            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)
            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim oCountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(UserCompanyMultipleDrop.SelectedGuid)
            Dim countryCode As String = oCountry.Code

            oCompany = New Company(compid)
            Dim addldacflag As Guid = oCompany.AddlDACId
            addlDACCode = LookupListNew.GetCodeFromId(LookupListNew.LK_ADDL_DAC, addldacflag)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                Dim exportData As String = NO
                exportData = YES

                rptName = RPT_FILENAME_EXPORT
                'End If
                params = SetExpParameters(compCode, selectedYearMonth, dealerCode, detailCode, selectByGroup, addlDACCode, langCode, dealerForCur, rptCurrency, exchangeRateCode)
            Else
                'Addl_Dac_id is either 'Admin/Tax/AdminMkt' and totals with out Coverage
                If addlDACCode <> Codes.ADDL_DAC__NONE AndAlso chkTotalsPageByCov.Checked = False Then
                    rptName = RPT_FILENAME_ADMIN
                    params = SetParameters(compCode, selectedYearMonth, dealerCode, detailCode, selectByGroup, addlDACCode, langCode, dealerForCur, rptCurrency, exchangeRateCode)
                    'Totals By Coverage
                ElseIf addlDACCode <> Codes.ADDL_DAC__NONE AndAlso selectByGroup <> NO AndAlso chkTotalsPageByCov.Checked = True Then
                    rptName = RPT_FILENAME_ADMIN_COVERAGE
                    params = SetTotalByCovParameters(compCode, selectedYearMonth, dealerCode, detailCode, selectByGroup, addlDACCode, langCode, dealerForCur, rptCurrency, exchangeRateCode)
                Else
                    'Addl_Dac_id = None
                    rptName = RPT_FILENAME
                    params = SetParameters(compCode, selectedYearMonth, dealerCode, detailCode, selectByGroup, addlDACCode, langCode, dealerForCur, rptCurrency, exchangeRateCode)
                End If
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetExpParameters(companyCode As String, selectedYearMonth As String, dealerCode As String,
                                detailCode As String, selectedGroupId As String, addlDACCode As String, langCode As String,
                                 dealerForCur As Guid, rptCurrency As Guid, exchangeRateCode As String) As ReportCeBaseForm.Params

            'Dim reportName As String = RPT_FILENAME_EXPORT
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturevalue As String = TheReportCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheReportCeInputControl.getReportName(rptName, True)

            With rptParams
                .companyCode = companyCode
                .dealerCode = dealerCode
                .selectedYearMonth = selectedYearMonth
                .detailCode = detailCode
                .groupCode = selectedGroupId
                .addlDAC = addlDACCode
                .langCode = langCode
                .culturevalue = culturevalue
                .dealerForCur = DALBase.GuidToSQLString(dealerForCur)
                .rptCurrency = DALBase.GuidToSQLString(rptCurrency)
                .exchangeRateCode = exchangeRateCode
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            'Me.rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))
            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam,
                            rptName As String, startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("PI_COMPANY_CODE", .companyCode, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("PI_DEALER_CODE", .dealerCode, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("PI_GROUP_ID", .groupCode, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("PI_YEARMONTH", .selectedYearMonth, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("PI_DETAIL_CODE", .detailCode, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("PI_ADDL_DAC", .addlDAC, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("PI_RPT_CUR", .rptCurrency, rptName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("PI_DEALER_WITH_CUR", .dealerForCur, rptName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("PI_RATE_CODE", .exchangeRateCode, rptName)
                repParams(startIndex + 9) = New ReportCeBaseForm.RptParam("PI_LANG_CODE", .langCode, rptName)
                repParams(startIndex + 10) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, rptName)

            End With

        End Sub

#End Region

#Region "visibility control logic"
        Private Sub ShowHideControls(show As Boolean)
            If (show) Then
                trOnlyDealersWith.Style.Add("display", "block")
                trCurrency.Style.Add("display", "block")
                trHrAfterCurrencyRow.Style.Add("display", "block")
                trTransactionDay.Style.Add("display", "block")
            Else
                trOnlyDealersWith.Style.Add("display", "none")
                trCurrency.Style.Add("display", "none")
                trHrAfterCurrencyRow.Style.Add("display", "none")
                trTransactionDay.Style.Add("display", "none")
            End If
        End Sub
#End Region

    End Class
End Namespace
