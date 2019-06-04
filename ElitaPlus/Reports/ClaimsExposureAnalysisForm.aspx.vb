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

    Partial Class ClaimsExposureAnalysisForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Claims_Exposure_Analysis"
        Private Const RPT_FILENAME As String = "ClaimsExposureAnalysis"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimsExposureAnalysis_Exp"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const SUMMARIZE_BY_DEALER As String = "DE"
        Private Const SUMMARIZE_BY_RISK_TYPE As String = "RI"
        Private Const SUMMARIZE_BY_RISK_TYPE_PER_DEALER As String = "DR"

        Private Const COL_NAME_ACCOUNTING_CLOSE_INFO_ID As String = "ACCOUNTING_CLOSE_INFO_ID"
        Private Const COL_NAME_CLOSING_DATE As String = "CLOSING_DATE"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public selectedBeginYearMonth As String
            Public selectedEndYearMonth As String
            Public dealerCode As String
            Public summaryCode As String
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
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents Label5 As System.Web.UI.WebControls.Label
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents Label3 As System.Web.UI.WebControls.Label
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Private reportName As String = RPT_FILENAME
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        Private dtLatestAccountingCloseDate As Date
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Me.ClearLabelsErrSign()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                Else
                    ClearErrLabels()
                    If Not ViewState("dtLatestAccountingCloseDate") Is System.DBNull.Value Then
                        dtLatestAccountingCloseDate = CType(ViewState("dtLatestAccountingCloseDate"), Date)
                    End If
                End If
                Me.InstallProgressBar()


            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(BeginMonthYearLabel)
                Me.ClearLabelErrSign(EndMonthYearLabel)
                Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(BeginMonthYearLabel)
            Me.ClearLabelErrSign(EndMonthYearLabel)
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDealerDropDown()
            '''Me.BindListControlToDataView(Me.cboDealerDec, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            '''Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            '''DealersLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboDealerCode, DealersLookupListSortedByCode, "CODE", , True)
            ''''Me.BindListControlToDataView(Me.cboDealerDec, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            ''''Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            ''''DealersLookupListSortedByCode.Sort = "CODE"
            ''''Me.BindListControlToDataView(Me.cboDealerCode, DealersLookupListSortedByCode, "CODE", , True)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;", "", "", "", False, 5)

        End Sub

        Private Sub PopulateYearsDropdown()
            Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            ' Me.BindListTextToDataView(Me.BeginYearDropDownList, dv, , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.BeginYearDropDownList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .ValueFunc = AddressOf PopulateOptions.GetCode
                  })
            '  Me.BindListTextToDataView(Me.EndYearDropDownList, dv, , , True)
            Me.EndYearDropDownList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .ValueFunc = AddressOf PopulateOptions.GetCode
                  })
        End Sub

        Private Sub PopulateMonthsDropdown()
            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            '   dv.Sort = "CODE"
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            '  Me.BindListControlToDataView(Me.BeginMonthDropDownList, dv, , , True)
            Me.BeginMonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
            ' Me.BindListControlToDataView(Me.EndMonthDropDownList, dv, , , True)
            Me.EndMonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        End Sub

        Private Sub InitializeForm()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
            Me.rdealer.Checked = True
            Me.RadiobuttonDealer.Checked = True

            'Disable Export option
            If moReportCeInputControl Is Nothing Then
                moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
            End If
            moReportCeInputControl.ExcludeExport()

            'load Accounting Close Date
            dtLatestAccountingCloseDate = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Date.Today)
            ViewState("dtLatestAccountingCloseDate") = dtLatestAccountingCloseDate
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal userId As String, ByVal dealerCode As String, ByVal selectedBeginYearMonth As String,
                               ByVal selectedEndYearMonth As String, ByVal summaryCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            reportFormat = ReportCeBase.GetReportFormat(Me)
            reportName = Me.RPT_FILENAME
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = RPT_FILENAME_EXPORT
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                                {
                                                 New ReportCeBaseForm.RptParam("V_USER_ID", userId),
                                                 New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                                                 New ReportCeBaseForm.RptParam("V_BEGIN_DATE", selectedBeginYearMonth),
                                                 New ReportCeBaseForm.RptParam("V_END_DATE", selectedEndYearMonth),
                                                 New ReportCeBaseForm.RptParam("P_SUMMARIZE", summaryCode)}

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
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim selectedBeginYear As String = Me.GetSelectedDescription(Me.BeginYearDropDownList)
            Dim selectedEndYear As String = Me.GetSelectedDescription(Me.EndYearDropDownList)
            Dim selectedBeginMonthID As Guid = Me.GetSelectedItem(Me.BeginMonthDropDownList)
            Dim selectedEndMonthID As Guid = Me.GetSelectedItem(Me.EndMonthDropDownList)
            Dim selectedBeginMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedBeginMonthID)
            Dim selectedEndMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedEndMonthID)
            Dim selectedBeginYearMonth As String = selectedBeginYear & selectedBeginMonth
            Dim selectedEndYearMonth As String = selectedEndYear & selectedEndMonth
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerCode)            
            'Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim summaryCode As String


            'Dates
            ReportCeBase.ValidateBeginEndDate(BeginMonthYearLabel, "01-" & Me.GetSelectedDescription(Me.BeginMonthDropDownList).ToString & "-" & selectedBeginYear,
                EndMonthYearLabel, "01-" & Me.GetSelectedDescription(Me.EndMonthDropDownList).ToString & "-" & selectedEndYear)

            'Summary code
            If Me.RadiobuttonDealer.Checked Then
                summaryCode = SUMMARIZE_BY_DEALER
            ElseIf Me.RadiobuttonRiskType.Checked Then
                summaryCode = SUMMARIZE_BY_RISK_TYPE
            ElseIf Me.RadiobuttonRiskTypePerDealer.Checked Then
                summaryCode = SUMMARIZE_BY_RISK_TYPE_PER_DEALER
            End If

            'Dates validation
            If selectedBeginMonthID.Equals(Guid.Empty) OrElse selectedBeginYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(BeginMonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            If selectedEndMonthID.Equals(Guid.Empty) OrElse selectedEndYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(EndMonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            'Accounting close dates validation
            Dim beginDate As Date = Date.Parse((selectedBeginYear & "/" & selectedBeginMonth & "/01"), System.Globalization.CultureInfo.InvariantCulture)
            Me.ValidateAccountingCloseDate(beginDate, BeginMonthYearLabel)
            Dim endDate As Date = Date.Parse((selectedEndYear & "/" & selectedEndMonth & "/15"), System.Globalization.CultureInfo.InvariantCulture)
            Me.ValidateAccountingCloseDate(endDate, EndMonthYearLabel)

            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(userId), dealerCode, selectedBeginYearMonth, selectedEndYearMonth, summaryCode)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

        Private Sub ValidateAccountingCloseDate(ByVal dtDateToCompare As Date, ByVal lbl As Label)
            If dtDateToCompare > dtLatestAccountingCloseDate Then
                ElitaPlusPage.SetLabelError(lbl)
                Throw New GUIException(Message.MSG_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED)
            End If
        End Sub
#End Region


    End Class

End Namespace
