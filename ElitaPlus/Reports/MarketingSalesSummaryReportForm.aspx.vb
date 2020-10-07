Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Reports

    Partial Class MarketingSalesSummaryReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Marketing Sales Summary"
        Private Const RPT_FILENAME_DATEADDED As String = "MarketingSalesSummaryDateAdded"
        Private Const RPT_FILENAME_DATESOLD As String = "MarketingSalesSummaryDateSold"
        Private Const RPT_SUBREPORT As String = "Marketing Sales Summary Totals"

        Private Const RPT_FILENAME_DATEADDED_EXPORT As String = "MarketingSalesSummaryDateAdded_Exp"
        Private Const RPT_FILENAME_DATESOLD_EXPORT As String = "MarketingSalesSummaryDateSold_Exp"

        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public userId As String
            Public selectedBeginYearMonth As String
            Public selectedEndYearMonth As String
            Public dealerCode As String
            Public detailCode As String
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
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property
#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Protected WithEvents rPriceGroups As System.Web.UI.WebControls.RadioButton
        Protected WithEvents cboPriceGroups As System.Web.UI.WebControls.DropDownList
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents moBeginDateLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moBeginDateText As System.Web.UI.WebControls.TextBox
        Protected WithEvents BtnBeginDate As System.Web.UI.WebControls.ImageButton
        Protected WithEvents moEndDateLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moEndDateText As System.Web.UI.WebControls.TextBox
        Protected WithEvents BtnEndDate As System.Web.UI.WebControls.ImageButton
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents txtNumberOfDaysSinceStartOfCoverage As System.Web.UI.WebControls.TextBox
        Protected WithEvents Label4 As System.Web.UI.WebControls.Label
        Protected WithEvents rdReportSortOrder As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents RadiobuttonDealer As System.Web.UI.WebControls.RadioButton
        Protected WithEvents RadiobuttonRiskType As System.Web.UI.WebControls.RadioButton
        Protected WithEvents RadiobuttonRiskTypePerDealer As System.Web.UI.WebControls.RadioButton
        Private reportName As String = RPT_FILENAME_DATEADDED
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl

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
            ClearLabelErrSign(BeginMonthYearLabel)
            ClearLabelErrSign(EndMonthYearLabel)
            ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            If rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDealerDropDown()
            '''Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            '''Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            '''DealersLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboDealerCode, DealersLookupListSortedByCode, "CODE", , True)

            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;")

        End Sub

        Private Sub PopulateYearsDropdown()

            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId

            Dim closingYears As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            BeginYearDropDownList.Populate(closingYears, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .BlankItemValue = "0",
                                                    .ValueFunc = AddressOf PopulateOptions.GetCode
                                                   })
            EndYearDropDownList.Populate(closingYears, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .BlankItemValue = "0",
                                                    .ValueFunc = AddressOf PopulateOptions.GetCode
                                                    })
        End Sub

        Private Sub PopulateMonthsDropdown()

            Dim months As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            BeginMonthDropDownList.Populate(months, New PopulateOptions() With
                                              {
                                                .AddBlankItem = True
                                               })

            EndMonthDropDownList.Populate(months, New PopulateOptions() With
                                              {
                                                .AddBlankItem = True
                                               })

        End Sub

        Private Sub InitializeForm()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
            rdealer.Checked = True
            RadiobuttonDateAdded.Checked = True
            moReportCeInputControl.ExcludeExport()
        End Sub

#End Region

#Region "Crystal Enterprise"


        Function SetParameters(userId As String,
                               dealerCode As String,
                               selectedBeginYearMonth As String,
                               selectedEndYearMonth As String,
                               detailCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME_DATEADDED
            Dim params As New ReportCeBaseForm.Params
            Dim rptParams() As ReportCeBaseForm.RptParam

            reportFormat = ReportCeBase.GetReportFormat(Me)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                If detailCode = YES Then
                    reportName = RPT_FILENAME_DATEADDED_EXPORT
                Else
                    reportName = RPT_FILENAME_DATESOLD_EXPORT
                End If
                rptParams = New ReportCeBaseForm.RptParam() _
                                {
                                 New ReportCeBaseForm.RptParam("V_USER_KEY", userId),
                                 New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                                 New ReportCeBaseForm.RptParam("V_BEGIN_DATE", selectedBeginYearMonth),
                                 New ReportCeBaseForm.RptParam("V_END_DATE", selectedEndYearMonth)}
            Else
                If detailCode = NO Then
                    reportName = RPT_FILENAME_DATESOLD
                End If
                rptParams = New ReportCeBaseForm.RptParam() _
                                {
                                 New ReportCeBaseForm.RptParam("V_USER_KEY", userId),
                                 New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                                 New ReportCeBaseForm.RptParam("V_BEGIN_DATE", selectedBeginYearMonth),
                                 New ReportCeBaseForm.RptParam("V_END_DATE", selectedEndYearMonth)}

            End If

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = rptParams
            End With

            Return params
        End Function

        Private Sub GenerateReport()
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim selectedBeginYear As String = GetSelectedDescription(BeginYearDropDownList)
            Dim selectedEndYear As String = GetSelectedDescription(EndYearDropDownList)
            Dim selectedBeginMonthID As Guid = GetSelectedItem(BeginMonthDropDownList)
            Dim selectedEndMonthID As Guid = GetSelectedItem(EndMonthDropDownList)
            Dim selectedBeginMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedBeginMonthID)
            Dim selectedEndMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedEndMonthID)
            Dim selectedBeginYearMonth As String = selectedBeginYear & selectedBeginMonth & "01"
            Dim selectedEndYearMonth As String = selectedEndYear & selectedEndMonth & "01"
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealer)
            'Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim detailCode As String
            Dim params As ReportCeBaseForm.Params

            'Dates
            ReportCeBase.ValidateBeginEndDate(BeginMonthYearLabel, "01-" & GetSelectedDescription(BeginMonthDropDownList).ToString & "-" & selectedBeginYear,
                EndMonthYearLabel, "01-" & GetSelectedDescription(EndMonthDropDownList).ToString & "-" & selectedEndYear)

            If RadiobuttonDateAdded.Checked Then
                detailCode = YES    'Date Added
            Else
                detailCode = NO     'Date Sold
            End If

            If selectedBeginMonthID.Equals(Guid.Empty) OrElse selectedBeginYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(BeginMonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            If selectedEndMonthID.Equals(Guid.Empty) OrElse selectedEndYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(EndMonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If



            If rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            params = SetParameters(userId, dealerCode, selectedBeginYearMonth, selectedEndYearMonth, detailCode)

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

#End Region

    End Class

End Namespace
