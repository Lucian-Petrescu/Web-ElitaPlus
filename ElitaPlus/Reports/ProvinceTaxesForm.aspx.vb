Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Partial Class ProvinceTaxesForm
        Inherits ElitaPlusPage
#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Province Taxes"
        Private Const RPT_FILENAME As String = "ProvinceTaxes"
        Private Const RPT_FILENAME_EXPORT As String = "ProvinceTaxes_Exp"

        Public Const CRYSTAL As String = "0"
        'Public Const PDF As String = "1"
        Public Const HTML As String = "2"
        Public Const CSV As String = "3"
        'Public Const EXCEL As String = "4"

        Public Const VIEW_REPORT As String = "1"
        Public Const PDF As String = "2"
        Public Const TAB As String = "3"
        Public Const EXCEL As String = "4"
        Public Const EXCEL_DATA_ONLY As String = "5"
        Public Const JAVA As String = "6"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Private Const ONE_ITEM As Integer = 1

#End Region
#Region "variables"
        Dim reportFormat As ReportCeBaseForm.RptFormat
        Dim reportName As String = RPT_FILENAME
        Dim fileName As String = RPT_FILENAME_WINDOW
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

        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDropControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDropControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property
#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label7 As System.Web.UI.WebControls.Label
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents moUserCompanyMultipleDrop As MultipleColumnDropControl

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDropControl) _
                        Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = False
            UserCompanyMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage("SELECT_COMPANY")
            UserCompanyMultipleDrop.BindData(dv)
            If UserCompanyMultipleDrop.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub
        Private Sub PopulateDealerDropDown()
            'Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid))
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = UserCompanyMultipleDrop.SelectedGuid
            Dim dealerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.DealerListByCompany, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            cboDealer.Populate(dealerLkl, New PopulateOptions() With
             {
               .AddBlankItem = True
             })
        End Sub
        Private Sub PopulateYearsDropdown()
            'Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.YearDropDownList, dv, , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            YearDropDownList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .ValueFunc = AddressOf PopulateOptions.GetCode,
            .BlankItemValue = "0"
             })
            'Dim yearMonth As String = Me.GetSelectedDescription(Me.YearDropDownList)
        End Sub

        Private Sub PopulateMonthsDropdown()
            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            ' Me.BindListControlToDataView(Me.MonthDropDownList, dv, , , True)
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            MonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        End Sub
        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
            TheRptCeInputControl.SetExportOnly()
            rdealer.Checked = True
            ' Me.RadiobuttonLossReport.Checked = True
        End Sub
        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click

            Try
                'Dim companyId As Guid = Me.GetApplicationUser.CompanyID
                'Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", companyId)
                Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
                Dim selectedYear As String = GetSelectedDescription(YearDropDownList)
                Dim selectedMonthID As Guid = GetSelectedItem(MonthDropDownList)
                Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
                Dim selectedYearMonth As String = selectedYear & selectedMonth
                Dim selectedDealerId As Guid = GetSelectedItem(cboDealer)
                Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
                Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
                Dim params As ReportCeBaseForm.Params

                If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
                End If

                If rdealer.Checked Then
                    dealerCode = ALL
                Else
                    If selectedDealerId.Equals(Guid.Empty) Then
                        Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                    End If
                End If

                ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

                reportFormat = ReportCeBase.GetReportFormat(Me)

                If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                    'Export Report
                    reportName = RPT_FILENAME_EXPORT
                    params = SetParameters(compCode, selectedYearMonth, dealerCode)
                Else
                    'View Report
                    params = SetParameters(compCode, selectedYearMonth, dealerCode)
                End If

                ' Dim params As ReportCeBaseForm.Params = SetParameters(compCode, selectedYearMonth, dealerCode)
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Function SetParameters(companyCode As String, selectedYearMonth As String, dealerCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    { _
                     New ReportCeBaseForm.RptParam("V_COMPANY", companyCode), _
                     New ReportCeBaseForm.RptParam("V_DEALER", dealerCode), _
                     New ReportCeBaseForm.RptParam("V_YEAR_MONTH", selectedYearMonth), _
                     New ReportCeBaseForm.RptParam("V_FORHEADER", NO)}

            reportFormat = ReportCeBase.GetReportFormat(Me)

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(fileName)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

    End Class
End Namespace

