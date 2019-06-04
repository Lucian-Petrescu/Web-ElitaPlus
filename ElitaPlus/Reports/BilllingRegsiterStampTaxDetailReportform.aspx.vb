Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
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

    Partial Class BilllingRegsiterStampTaxDetailReportform
        Inherits ElitaPlusPage


#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String
        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "BILLING_REGISTER_STAMP_TAX DETAIL"
        Private Const RPT_FILENAME As String = "BillingRegisterStampTaxDetail"
        Private Const RPT_FILENAME_EXPORT As String = "BillingRegisterStampTaxDetail-Exp"
        ' Private Const RPT_SUBREPORT As String = "rptRStampTax"

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
        Public Const PAGETITLE As String = "BILLING_REGISTER_STAMP_TAX DETAIL"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "BILLING_REGISTER_STAMP_TAX DETAIL"
        Private Const ONE_ITEM As Integer = 1
        Private Const YearMonth As String = "YYYYMM"

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
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

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label

        'Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl
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
                    'JavascriptCalls()
                    InitializeForm()
                End If
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
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
        Private Sub PopulateYearsDropdown()
            '  Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.moYearDropDownList, dv, , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), ListContext)
            Me.moYearDropDownList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .ValueFunc = AddressOf PopulateOptions.GetCode
                  })
            Dim oDescrip As String = Me.GetSelectedDescription(Me.moYearDropDownList)
        End Sub

        Private Sub PopulateMonthsDropdown()
            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            ' dv.Sort = "CODE"
            ' Me.BindListControlToDataView(Me.moMonthDropDownList, dv, , , True)
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            Me.moMonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        End Sub
        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(False, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, UserCompanyMultipleDrop.NO_CAPTION, True, False)
            If dv.Count.Equals(ONE_ITEM) Then
                ControlMgr.SetVisibleControl(Me, lblCompany, False)
                HideHtmlElement(trcomp.ClientID)
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub
        Private Sub GenerateReport()
            Dim compDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim compId As Guid = UserCompanyMultipleDrop.SelectedGuid
            Dim selectedYear As String = Me.GetSelectedDescription(Me.moYearDropDownList)
            Dim selectedMonthID As Guid = Me.GetSelectedItem(Me.moMonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim selectedYearMonth As String = selectedYear & selectedMonth

            If compId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(lblcompany)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, compDesc, selectedYearMonth)
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

        Function SetParameters(ByVal companyCode As String, ByVal companyDesc As String, ByVal selectedYearMonth As String) As ReportCeBaseForm.Params

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
                     New ReportCeBaseForm.RptParam("V_COMPANY", companyCode), _
                     New ReportCeBaseForm.RptParam("V_COMPANYDESC", companyDesc), _
                     New ReportCeBaseForm.RptParam("V_DEALER", ALL), _
                     New ReportCeBaseForm.RptParam("V_YEAR_MONTH", selectedYearMonth), _
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