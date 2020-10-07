Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew.Doc
Imports Microsoft.VisualBasic
Imports System.Threading
Imports System.Globalization
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements
Namespace Reports

    Partial Class MXClaimInfoReportForm
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String

        End Structure

#End Region

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "MX_CLAIM_INFORMATION"
        Private Const RPT_FILENAME As String = "MXClaimsInfo"
        Private Const RPT_FILENAME_EXPORT As String = "MXClaimsInfo-Exp"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Public Const PAGETITLE As String = "MX_CLAIM_INFORMATION"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "MX_CLAIM_INFORMATION"
        Private Const ONE_ITEM As Integer = 1

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "JavaScript"

        Public Sub JavascriptCalls()
            rdealer.Attributes.Add("onclick", "return ToggleDualDropDownsSelection('" + DealerDropControl.CodeDropDown.ClientID + "','" + DealerDropControl.DescDropDown.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")
            'TheReportCeInputControl.RadioButtonPDFControl.Attributes.Add("onclick", "return '" + RadiobuttonTotalsOnly.ClientID + "'.Checked = True")
            'TheReportCeInputControl.RadioButtonVIEWControl.Attributes.Add("onclick", "return '" + RadiobuttonTotalsOnly.ClientID + "'.Checked = True")
            'TheReportCeInputControl.RadioButtonTXTControl.Attributes.Add("onclick", "return '" + RadiobuttonDetail.ClientID + "'.Checked = True")

        End Sub
#End Region

#Region "Properties"
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If DealerDropControl Is Nothing Then
                    DealerDropControl = CType(FindControl("DealerDropControl"), MultipleColumnDDLabelControl)
                End If
                Return DealerDropControl
            End Get
        End Property

        Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If CompanyDropControl Is Nothing Then
                    CompanyDropControl = CType(FindControl("CompanyDropControl"), MultipleColumnDDLabelControl)
                End If
                Return CompanyDropControl
            End Get
        End Property

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub



        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here


            ErrControllerMaster.Clear_Hide()
            ClearLabelsErrSign()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            If CompanyMultipleDrop.Visible = False Then
                HideHtmlElement(trcomp.ClientID)
            End If
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    JavascriptCalls()
                    AddCalendar(BtnBeginDate, moBeginDateText)
                    AddCalendar(BtnEndDate, moEndDateText)
                    InitializeForm()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(moBeginDateLabel)
                ClearLabelErrSign(moEndDateLabel)
                ClearLabelErrSign(moMonthLabel)
                ClearLabelErrSign(moYearLabel)
                ClearLabelErrSign(lblCompany)
                ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateCompaniesDropdown()

            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            ' CompanyMultipleDrop.NothingSelected = True
            CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.NEW_MODE, True, dv, CompanyMultipleDrop.NO_CAPTION, True)
            'CompanyMultipleDrop.CaptionLabel
            If dv.Count.Equals(ONE_ITEM) Then
                ControlMgr.SetVisibleControl(Me, lblCompany, False)
                HideHtmlElement(trcomp.ClientID)
                CompanyMultipleDrop.SelectedIndex = ONE_ITEM
                CompanyMultipleDrop.Visible = False
            End If
            CompanyMultipleDrop.SelectedIndex = ONE_ITEM

        End Sub

        Sub PopulateDealerDropDown()

            If Not CompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                Dim dv As DataView = LookupListNew.GetDealerLookupList(CompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")
            Else
                Dim dv As DataView = LookupListNew.GetDealerLookupList(CompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "  " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")
            End If

        End Sub
        Private Sub PopulateMonthsAndYearsDropdown()
            Dim dvM As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            dvM.Sort = "CODE"

            Dim MonthsLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            MonthDropDownList.Populate(MonthsLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf .GetCode
                    })


            Dim listcontext As ListContext = New ListContext()
            listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("GetClosingYearsByUser", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            YearDropDownList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .BlankItemValue = "0",
            .ValueFunc = AddressOf PopulateOptions.GetCode
             })

        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            PopulateMonthsAndYearsDropdown()
            'RadiobuttonTotalsOnly.Checked = True
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            moBeginDateText.Text = GetDateFormattedString(t)
            moEndDateText.Text = GetDateFormattedString(Date.Now)
            rSelectDates.Checked = True
            ControlMgr.SetEnableControl(Me, MonthDropDownList, False)
            ControlMgr.SetEnableControl(Me, YearDropDownList, False)
            TheReportCeInputControl.SetExportOnly()
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT)
        End Sub

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

        Protected Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
             Handles CompanyDropControl.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

        Private Sub GenerateReport()
            Dim oCompanyId As Guid = CompanyMultipleDrop.SelectedGuid
            Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)
            Dim compDesc As String = LookupListNew.GetDescriptionFromId("COMPANIES", oCompanyId)
            Dim endDate As String
            Dim beginDate As String
            Dim isDetail As String

            Dim LANG_CODE As String = LookupListNew.GetCodeFromId("LANGUAGES", ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid

            Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyId)
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim dealerDesc As String = DealerMultipleDrop.SelectedDesc

            'Dates
            Dim selectedYear As String = GetSelectedDescription(YearDropDownList)
            Dim selectedMonthID As Guid = GetSelectedItem(MonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim yearmonth As String


            If rSelectDates.Checked = True Then
                If Not moBeginDateText.Text.Trim.ToString = String.Empty AndAlso Not moEndDateText.Text.Trim.ToString = String.Empty Then
                    ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
                    endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
                    beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)
                Else
                    ElitaPlusPage.SetLabelError(moBeginDateLabel)
                    ElitaPlusPage.SetLabelError(moEndDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
                End If
            Else
                If rMonthYear.Checked = True Then
                    If Not selectedMonthID.Equals(Guid.Empty) AndAlso Not selectedYear.Equals(String.Empty) Then
                        yearmonth = selectedYear & selectedMonth
                    Else
                        ElitaPlusPage.SetLabelError(moMonthLabel)
                        ElitaPlusPage.SetLabelError(moYearLabel)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
                    End If
                End If
            End If

            'COMPANY
            If oCompanyId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(lblCompany)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Validating the Dealer selection
            If rdealer.Checked Then
                dealerCode = ALL
            Else
                If dealerID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerDropControl.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, dealerCode, beginDate, endDate, yearmonth)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetParameters(companyCode As String, dealerCode As String, _
                               beginDate As String, _
                               endDate As String, yearmonth As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(CompanyDropControl.SelectedGuid))
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturecode = TheReportCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(CompanyDropControl.SelectedGuid))
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    { _
                     New ReportCeBaseForm.RptParam("V_COMPANY", companyCode), _
                     New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode), _
                     New ReportCeBaseForm.RptParam("V_YEARMONTH", yearmonth), _
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate), _
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate), _
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