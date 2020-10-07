Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports System.Web.Script.Services
Imports Assurant.ElitaPlus.Security
Imports System.Web.Script.Serialization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements

Namespace Reports
    Partial Class ProRatedPremiumAnalysis
        Inherits ElitaPlusPage

#Region "Constants"
        Private Const RPT_FILENAME_WINDOW As String = "PRO_RATED_PREMIUM_ANALYSIS_REPORT"
        Private Const RPT_FILENAME As String = "ProRatedPremiumAnalysis"
        Private Const RPT_FILENAME_EXPORT As String = "ProRatedPremiumAnalysis_Exp"
        Private Const RPT_FILENAME_TOTAL_EXPORT As String = "ProRatedPremiumAnalysisTotal_Exp"

        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Private Const LABEL_SELECT_DEALER As String = "DEALER"
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
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property
#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "
        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        End Sub
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button

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
            Try
                If Not IsPostBack Then
                    InitializeForm()
                Else
                    ClearErrLabels()
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

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moMonthYearLabel)
            ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateMonthYear()
            Dim reportMonthYear As Date = Date.Today.AddMonths(-1)
            Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            cboMonth.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = False,
              .SortFunc = AddressOf PopulateOptions.GetCode
           })

            SetSelectedItem(cboMonth, LookupListNew.GetIdFromCode(dv, reportMonthYear.ToString("MM")))


            moYearText.Text = reportMonthYear.Year.ToString()
        End Sub

        Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True)
        End Sub

        Private Sub InitializeForm()
            PopulateDealerDropDown()
            PopulateMonthYear()
            RadiobuttonTotalsOnly.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(language As String, company_code As String, dealerCode As String,
                               month As String, year As String, isTotalsOnly As String) As ReportCeBaseForm.Params
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            Dim culturecode As String = TheRptCeInputControl.getCultureValue(False)

            Dim exportData As String = NO

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                If (isTotalsOnly = "N") Then
                    reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                    culturecode = TheRptCeInputControl.getCultureValue(True)
                Else
                    reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_TOTAL_EXPORT, True)
                    culturecode = TheRptCeInputControl.getCultureValue(True)
                End If
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                    {New ReportCeBaseForm.RptParam("V_COMPANY_CODE", company_code),
                                     New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode),
                                     New ReportCeBaseForm.RptParam("V_MONTH", month),
                                     New ReportCeBaseForm.RptParam("V_YEAR", year),
                                     New ReportCeBaseForm.RptParam("V_IS_TOTALS_ONLY", isTotalsOnly),
                                     New ReportCeBaseForm.RptParam("V_LANG_CODE", language),
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

        Private Sub GenerateReport()
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim isTotalsOnly As String
            Dim month As String
            Dim language As String = LookupListNew.GetCodeFromId("LANGUAGES", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim oDealer As Dealer
            Dim oCompany As Company
            Dim companyCode As String
            Dim year As String
            Dim i As Integer

            month = LookupListNew.GetCodeFromId(LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(GetSelectedValue(cboMonth)))
            year = moYearText.Text
            If (year.Length < 4 OrElse Not Integer.TryParse(year, i)) Then
                ElitaPlusPage.SetLabelError(moMonthYearLabel)
                Throw New GUIException(Message.MSG_INVALID_YEARMONTH, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_YEAR_ENTERED_ERROR)
            End If
            If RadiobuttonTotalsOnly.Checked Then
                isTotalsOnly = YES
            Else
                isTotalsOnly = NO
            End If

            If selectedDealerId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If
            oDealer = New Dealer(selectedDealerId)
            oCompany = New Company(oDealer.CompanyId)
            companyCode = oCompany.Code

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(language, companyCode, dealerCode, month, year, isTotalsOnly)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class

End Namespace