Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Reports

    Partial Class ClaimsAnalysisByNumberOfDaysActive
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Claim Analysis By Number Of Days Active"
        Private Const RPT_FILENAME As String = "Claims Analysis By Number Of Days Active"
        Private Const RPT_FILENAME_EXPORT As String = "Claims Analysis By Number Of Days Active_Exp"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 4 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"

        Private Const SUMMARIZE_BY_DEALER As String = "DE"
        Private Const SUMMARIZE_BY_RISK_TYPE As String = "RI"
        Private Const SUMMARIZE_BY_RISK_TYPE_PER_DEALER As String = "DR"
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
        Private reportName As String = RPT_FILENAME
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
            ClearLabelsErrSign()
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
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(BeginMonthYearLabel)
                ClearLabelErrSign(EndMonthYearLabel)
                ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
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

            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;", "", "", "", False, 6)

        End Sub

        Private Sub PopulateYearsDropdown()
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId

            Dim closingYears As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            BeginYearDropDownList.Populate(closingYears, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True,
                                                    .BlankItemValue = "0",
                                                    .ValueFunc = AddressOf PopulateOptions.GetCode,
                                                    .SortFunc = AddressOf PopulateOptions.GetDescription
                                                   })
            EndYearDropDownList.Populate(closingYears, New PopulateOptions() With
                                                   {
                                                     .AddBlankItem = True,
                                                     .BlankItemValue = "0",
                                                    .ValueFunc = AddressOf PopulateOptions.GetCode,
                                                    .SortFunc = AddressOf PopulateOptions.GetDescription
                                                    })
        End Sub

        Private Sub PopulateMonthsDropdown()

            Dim months As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            BeginMonthDropDownList.Populate(months, New PopulateOptions() With
                                              {
                                                .AddBlankItem = True,
                                                .SortFunc = AddressOf PopulateOptions.GetDescription
                                               })

            EndMonthDropDownList.Populate(months, New PopulateOptions() With
                                              {
                                               .AddBlankItem = True,
                                                .SortFunc = AddressOf PopulateOptions.GetDescription
                                               })
        End Sub

        Private Sub InitializeForm()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
            rdealer.Checked = True
            RadiobuttonDealer.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(userId As String, dealerCode As String, selectedBeginYearMonth As String,
                               selectedEndYearMonth As String, detailCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams

            With rptParams
                .userId = userId
                .selectedBeginYearMonth = selectedBeginYearMonth
                .selectedEndYearMonth = selectedEndYearMonth
                .dealerCode = dealerCode
                .detailCode = detailCode
            End With

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim exportData As String = NO

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                detailCode = YES
                exportData = YES
                reportName = RPT_FILENAME_EXPORT
            End If

            repParams = New ReportCeBaseForm.RptParam() _
            {
              New ReportCeBaseForm.RptParam("V_USER_KEY", userId),
              New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
              New ReportCeBaseForm.RptParam("V_BEGIN_DATE", selectedBeginYearMonth),
              New ReportCeBaseForm.RptParam("V_END_DATE", selectedEndYearMonth),
              New ReportCeBaseForm.RptParam("P_SUMMARIZE", detailCode)
            }

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
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim selectedBeginYear As String = GetSelectedDescription(BeginYearDropDownList)
            Dim selectedEndYear As String = GetSelectedDescription(EndYearDropDownList)
            Dim selectedBeginMonthID As Guid = GetSelectedItem(BeginMonthDropDownList)
            Dim selectedEndMonthID As Guid = GetSelectedItem(EndMonthDropDownList)
            Dim selectedBeginMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedBeginMonthID)
            Dim selectedEndMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedEndMonthID)
            Dim selectedBeginYearMonth As String = selectedBeginYear & selectedBeginMonth
            Dim selectedEndYearMonth As String = selectedEndYear & selectedEndMonth
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)
            'Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim detailCode As String
            Dim params As ReportCeBaseForm.Params

            'Dates
            ReportCeBase.ValidateBeginEndDate(BeginMonthYearLabel, "01-" & GetSelectedDescription(BeginMonthDropDownList).ToString & "-" & selectedBeginYear,
                EndMonthYearLabel, "01-" & GetSelectedDescription(EndMonthDropDownList).ToString & "-" & selectedEndYear)

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

            'Summary code
            If RadiobuttonDealer.Checked Then
                detailCode = SUMMARIZE_BY_DEALER
            ElseIf RadiobuttonRiskType.Checked Then
                detailCode = SUMMARIZE_BY_RISK_TYPE
            ElseIf RadiobuttonRiskTypePerDealer.Checked Then
                detailCode = SUMMARIZE_BY_RISK_TYPE_PER_DEALER
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                detailCode = NO 'Export Report
                params = SetExpParameters(userId, dealerCode, selectedBeginYearMonth, selectedEndYearMonth, detailCode)
            Else
                'View Report
                params = SetParameters(userId, dealerCode, selectedBeginYearMonth, selectedEndYearMonth, detailCode)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

        Function SetExpParameters(userId As String, dealerCode As String, selectedBeginYearMonth As String,
                                selectedEndYearMonth As String, detailCode As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME_EXPORT
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams

            With rptParams
                .userId = userId
                .dealerCode = dealerCode
                .selectedBeginYearMonth = selectedBeginYearMonth
                .selectedEndYearMonth = selectedEndYearMonth
                .detailCode = detailCode
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            'reportName = RPT_FILENAME_EXPORT

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
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_KEY", .userId, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER", .dealerCode, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .selectedBeginYearMonth, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_END_DATE", .selectedEndYearMonth, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("P_SUMMARIZE", .detailCode, rptName)
            End With

        End Sub

#End Region

    End Class

End Namespace
