Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Partial Class DKPremiumAmortizationForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "DK_PREMIUM_AMORTIZATION"
        Private Const RPT_FILENAME As String = "DKPremiumAmortization"
        Private Const RPT_FILENAME_EXPORT As String = "PremiumAmortization-Exp"
        Private Const RPT_FILENAME_ADMIN As String = "DKPremiumAmortizationAdmin"
        Private Const RPT_FILENAME_ADMIN_EXPORT As String = "PremiumAmortizationAdmin-Exp"
        Private Const COMPANY_CODE_FOR_BRAZIL As String = "ABR"
        Private Const RPT_FILENAME_ADMIN_COVERAGE As String = "DKPremiumAmortizationAdminCov"
        Private Const TOTAL_PARAMS As Integer = 15 ' 12 Elements    ' 10 Elements before without addldac
        Private Const TOTAL_EXP_PARAMS As Integer = 8 ' 6 Elements  '' ' 4 - 5 Elements before without addldac
        Private Const PARAMS_PER_REPORT As Integer = 8 ' 6 Elements '' ' 5 - 5 Elements before without addldac
        Private Const TOTAL_PARAMS_BY_COV As Integer = 23
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "D"
        Private Const NO As String = "N"
        Private Const ONE_ITEM As Integer = 1
        Private Const BRAZIL_CODE As String = "BR"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const LABEL_OR_A_SINGLE_DEALER As String = "OR A SINGLE DEALER"

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
        End Structure

#End Region

#Region "Properties"
        Public ReadOnly Property TheRptCeInputControl() As ReportCeInputControl
            Get
                If moReportCeInputControl Is Nothing Then
                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
                End If
                Return moReportCeInputControl
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
        Protected WithEvents ErrorCtrl As ErrorController
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
            ErrorCtrl.Clear_Hide()
            ClearLabelsErrSign()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                Else
                    ClearLabelsErrSign()
                    EnableOrDisableControls()
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

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerGroup()
                PopulateDealerDropDown()
                EnableOrDisableControls()
                'oCompany = New Company(UserCompanyMultipleDrop.SelectedGuid)
                'DAC_CODE = LookupListNew.GetCodeFromId(LookupListNew.LK_ADDL_DAC, oCompany.AddlDACId)
                'hidden_dacvalue.Value = DAC_CODE
                'If (DAC_CODE <> Codes.ADDL_DAC__NONE) Then
                '    Me.chkTotalsPageByCov.Visible = True
                '    Me.lblTotalsByCov.Visible = True
                '    If rGroup.Checked = True Or cboDealerGroup.SelectedItem.ToString <> String.Empty Then
                '        Me.chkTotalsPageByCov.Enabled = True
                '        Me.lblTotalsByCov.Enabled = True
                '    Else
                '        Me.chkTotalsPageByCov.Enabled = False
                '        Me.lblTotalsByCov.Enabled = False
                '    End If
                'Else
                '    Me.chkTotalsPageByCov.Visible = False
                '    Me.lblTotalsByCov.Visible = False
                'End If


            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
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
                UserCompanyMultipleDrop.Visible = False

                EnableOrDisableControls()
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
            End If
        End Sub

        Private Sub PopulateDealerGroup()
            'Me.BindListControlToDataView(cboDealerGroup, LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))

            Dim DealerGroups As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.DealerGroupByCompanyGroup,
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

            cboDealerGroup.Populate(DealerGroups.ToArray(),
                                        New PopulateOptions() With
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
                                              " document.forms[0].rdealer.checked = false;  document.forms[0].rGroup.checked = false; document.forms[0].cboDealerGroup.selectedIndex = -1; fncEnable(2);",
                                              "moUserDealerMultipleDrop_moMultipleColumnDrop",
                                              "moUserDealerMultipleDrop_moMultipleColumnDropDesc", "moUserDealerMultipleDrop_lb_DropDown")

        End Sub

        Private Sub PopulateYearsDropdown()
            'Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.YearDropDownList, dv, , , True)

            Dim YearList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.ClosingYearsByCompany,
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
                                                                })
            YearDropDownList.Populate(YearList.ToArray(),
                                         New PopulateOptions() With
                                         {
                                           .AddBlankItem = True,
                                           .BlankItemValue = "0"
                                         })

        End Sub

        Private Sub PopulateMonthsDropdown()
            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            'Me.BindListControlToDataView(Me.MonthDropDownList, dv, , , True)

            Dim MonthList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="MONTH",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            MonthDropDownList.Populate(MonthList.ToArray(),
                                         New PopulateOptions() With
                                         {
                                           .AddBlankItem = True,
                                           .SortFunc = AddressOf PopulateOptions.GetCode
                                         })

        End Sub

        Private Sub InitializeForm()
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
            PopulateCompaniesDropdown()
            PopulateDealerGroup()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
            rdealer.Checked = True
            RadiobuttonTotalsOnly.Checked = True
            ControlMgr.SetEnableControl(Me, chkTotalsPageByCov, False)
            ControlMgr.SetEnableControl(Me, lblTotalsByCov, False)
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(MonthYearLabel)
                ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
                ClearLabelErrSign(UserDealerMultipleDrop.CaptionLabel)
                ClearLabelErrSign(GroupLabel)

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
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

            oCompany = New Company(UserCompanyMultipleDrop.SelectedGuid)
            DAC_CODE = LookupListNew.GetCodeFromId(LookupListNew.LK_ADDL_DAC, oCompany.AddlDACId)
            hidden_dacvalue.Value = DAC_CODE
            If (DAC_CODE <> Codes.ADDL_DAC__NONE) Then
                chkTotalsPageByCov.Visible = True
                lblTotalsByCov.Visible = True
                If rGroup.Checked = True Or GetSelectedItem(cboDealerGroup) <> Guid.Empty Then ' cboDealerGroup.SelectedItem.ToString <> String.Empty Then
                    chkTotalsPageByCov.Enabled = True
                    lblTotalsByCov.Enabled = True
                Else
                    chkTotalsPageByCov.Enabled = False
                    lblTotalsByCov.Enabled = False
                End If
            Else
                chkTotalsPageByCov.Visible = False
                lblTotalsByCov.Visible = False
            End If
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(companyCode As String, selectedYearMonth As String, dealerCode As String,
                               detailCode As String, selectedGroupId As String, addlDACCode As String, langCode As String) As ReportCeBaseForm.Params
            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(rptName, False)

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
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report

            rptParams.detailCode = NO

            SetReportParams(rptParams, repParams, "Summary", PARAMS_PER_REPORT * 1)  ' Summary (Sub Report)

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

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
                               detailCode As String, selectedGroupId As String, addlDACCode As String, langCode As String) As ReportCeBaseForm.Params
            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS_BY_COV) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(rptName, False)

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
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report

            rptParams.detailCode = NO

            SetReportParams(rptParams, repParams, "Summary", PARAMS_PER_REPORT * 1)  ' Summary (Sub Report)

            rptParams.detailCode = NO

            SetReportParams(rptParams, repParams, "SummaryByCov", PARAMS_PER_REPORT * 2)  ' Summary (Sub Report)


            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

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
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
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

            'Validating the Dealer_Group selection
            If Not rdealer.Checked AndAlso Not rGroup.Checked AndAlso selectedDealerId.Equals(Guid.Empty) AndAlso selectedGroupId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(GroupLabel)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_GROUP_MUST_BE_SELECTED_ERR)
            End If

            If Not selectedGroupId.Equals(Guid.Empty) Then
                selectByGroup = DALBase.GuidToSQLString(selectedGroupId)
                dealerCode = DALBase.GuidToSQLString(selectedGroupId)
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)
            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim oCountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(UserCompanyMultipleDrop.SelectedGuid)
            Dim countryCode As String = oCountry.Code

            oCompany = New Company(compid)
            Dim addldacflag As Guid = oCompany.AddlDACId
            addlDACCode = LookupListNew.GetCodeFromId(LookupListNew.LK_ADDL_DAC, addldacflag)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                Dim exportData As String = NO
                detailCode = YES
                exportData = YES

                rptName = RPT_FILENAME_EXPORT
                'End If
                params = SetExpParameters(compCode, selectedYearMonth, dealerCode, detailCode, selectByGroup, addlDACCode, langCode)
            Else
                'Addl_Dac_id is either 'Admin/Tax/AdminMkt' and totals with out Coverage
                If addlDACCode <> Codes.ADDL_DAC__NONE AndAlso chkTotalsPageByCov.Checked = False Then
                    rptName = RPT_FILENAME_ADMIN
                    params = SetParameters(compCode, selectedYearMonth, dealerCode, detailCode, selectByGroup, addlDACCode, langCode)
                    'Totals By Coverage
                ElseIf addlDACCode <> Codes.ADDL_DAC__NONE AndAlso selectByGroup <> NO AndAlso chkTotalsPageByCov.Checked = True Then
                    rptName = RPT_FILENAME_ADMIN_COVERAGE
                    params = SetTotalByCovParameters(compCode, selectedYearMonth, dealerCode, detailCode, selectByGroup, addlDACCode, langCode)
                Else
                    'Addl_Dac_id = None
                    rptName = RPT_FILENAME
                    params = SetParameters(compCode, selectedYearMonth, dealerCode, detailCode, selectByGroup, addlDACCode, langCode)
                End If
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetExpParameters(companyCode As String, selectedYearMonth As String, dealerCode As String,
                                detailCode As String, selectedGroupId As String, addlDACCode As String, langCode As String) As ReportCeBaseForm.Params

            'Dim reportName As String = RPT_FILENAME_EXPORT
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(rptName, True)

            With rptParams
                .companyCode = companyCode
                .dealerCode = dealerCode
                .selectedYearMonth = selectedYearMonth
                .detailCode = detailCode
                .groupCode = selectedGroupId
                .addlDAC = addlDACCode
                .langCode = langCode
                .culturevalue = culturevalue

            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))
            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam, _
                            rptName As String, startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY_KEY", .companyCode, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_YEARMONTH", .selectedYearMonth, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DETAIL_CODE", .detailCode, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_GROUP_ID", .groupCode, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_ADDL_DAC", .addlDAC, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_LANG_CODE", .langCode, rptName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, rptName)
            End With

        End Sub

#End Region

    End Class
End Namespace






