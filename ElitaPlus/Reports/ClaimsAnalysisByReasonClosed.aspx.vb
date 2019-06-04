Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Reports

    Partial Class ClaimsAnalysisByReasonClosed
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Claims Analysis By Reason Closed"
        Private Const RPT_FILENAME As String = "ClaimsAnalysisByReasonClosed_EN"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimsAnalysisByReasonClosed_Exp"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"

        Private Const COL_NAME_ACCOUNTING_CLOSE_INFO_ID As String = "ACCOUNTING_CLOSE_INFO_ID"
        Private Const COL_NAME_CLOSING_DATE As String = "CLOSING_DATE"
        Private Const FEBRUARY As Integer = 2
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const DV_ID_COL As Integer = 1
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
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
        Protected WithEvents Label5 As System.Web.UI.WebControls.Label
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents Label3 As System.Web.UI.WebControls.Label
        Private reportName As String = RPT_FILENAME
        Private dtLatestAccountingCloseDate As Date
        Private dtMinLatestAccountingCloseDate As Date
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

        'ALR need to filter dealers by user, rather than company
        Private Sub PopulateDealerDropDown()
            '''Me.BindListControlToDataView(Me.cboDealerDec, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            '''Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            '''DealersLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboDealerCode, DealersLookupListSortedByCode, "CODE", , True)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;", "", "", "", False, 5)

        End Sub

        Private Sub PopulateYearsDropdown()

            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId

            Dim closingYears As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            closingYears.OrderBy("Code", LinqExtentions.SortDirection.Ascending)
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

            'Me.BindListTextToDataView(Me.BeginYearDropDownList, dv, , , True)
            ' Me.BindListTextToDataView(Me.EndYearDropDownList, dv, , , True)
        End Sub

        Private Sub PopulateMonthsDropdown()
            ' Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            'Me.BindListControlToDataView(Me.BeginMonthDropDownList, dv, , , True)
            'Me.BindListControlToDataView(Me.EndMonthDropDownList, dv, , , True)

            Dim months As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            months.OrderBy("Code", LinqExtentions.SortDirection.Ascending)
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
            Me.rdealer.Checked = True
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

        Function SetParameters(ByVal dealerCode As String, ByVal selectedBeginDate As String,
                               ByVal selectedEndDate As String, ByVal langCode As String, ByVal strYTDBeginDate As String, ByVal strLTMBeginDate As String,
                               ByVal strHeaderSelectedBeginDate As String, ByVal strHeaderSelectedEndDate As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            reportFormat = ReportCeBase.GetReportFormat(Me)
            reportName = Me.RPT_FILENAME
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = RPT_FILENAME_EXPORT
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                                {
                                                 New ReportCeBaseForm.RptParam("V_USER_KEY", GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)),
                                                 New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                                                 New ReportCeBaseForm.RptParam("P_START_DATE", selectedBeginDate),
                                                 New ReportCeBaseForm.RptParam("P_END_DATE", selectedEndDate),
                                                 New ReportCeBaseForm.RptParam("P_YTD_BEGIN_DATE", strYTDBeginDate),
                                                 New ReportCeBaseForm.RptParam("P_LTM_BEGIN_DATE", strLTMBeginDate),
                                                 New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", langCode),
                                                 New ReportCeBaseForm.RptParam("P_SELECTED_BEGIN_DATE", strHeaderSelectedBeginDate),
                                                 New ReportCeBaseForm.RptParam("P_SELECTED_END_DATE", strHeaderSelectedEndDate),
                                                 New ReportCeBaseForm.RptParam("V_FREE_ZONE_FLAG", "ALL")}


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

            Dim selectedBeginYear As String = Me.GetSelectedDescription(Me.BeginYearDropDownList)
            Dim selectedEndYear As String = Me.GetSelectedDescription(Me.EndYearDropDownList)
            Dim selectedBeginMonthID As Guid = Me.GetSelectedItem(Me.BeginMonthDropDownList)
            Dim selectedEndMonthID As Guid = Me.GetSelectedItem(Me.EndMonthDropDownList)
            Dim selectedBeginMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedBeginMonthID)
            Dim selectedEndMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedEndMonthID)

            'Dates validation - ALR- Relocated this checking as the selectedBeginDate and selectedEndDate methods 
            '                        throw errors if values are NULL
            ReportCeBase.ValidateBeginEndDate(BeginMonthYearLabel, "01-" & Me.GetSelectedDescription(Me.BeginMonthDropDownList).ToString & "-" & selectedBeginYear,
                EndMonthYearLabel, "01-" & Me.GetSelectedDescription(Me.EndMonthDropDownList).ToString & "-" & selectedEndYear)

            If selectedBeginMonthID.Equals(Guid.Empty) OrElse selectedBeginYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(BeginMonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            If selectedEndMonthID.Equals(Guid.Empty) OrElse selectedEndYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(EndMonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            Me.ValidateMinAccountingCloseDate(CType(selectedBeginYear & "/" & selectedBeginMonth & "/01", Date), BeginMonthYearLabel)
            Me.ValidateMinAccountingCloseDate(CType(selectedEndYear & "/" & selectedEndMonth & "/" & DateTime.DaysInMonth(CType(selectedEndYear, Integer), CType(selectedEndMonth, Integer)), Date), EndMonthYearLabel)


            Dim selectedBeginDate As String = GetPreciseDate(selectedBeginYear & "/" & selectedBeginMonth & "/01", True, False)
            Dim selectedEndDate As String = GetPreciseDate(selectedEndYear & "/" & selectedEndMonth & "/" & DateTime.DaysInMonth(CType(selectedEndYear, Integer), CType(selectedEndMonth, Integer)), False, False)

            Dim strYTDBeginDate As String = GetPreciseDate(selectedEndYear & "/01/01", True, True)

            Dim datLTMBeginDate As Date = CType(selectedEndYear & "/" & selectedEndMonth & "/15", Date).AddMonths(-11)
            Dim strLTMBeginDate As String = GetPreciseDate(datLTMBeginDate.Year & "/" & datLTMBeginDate.Month & "/01", True, True)

            Dim strHeaderSelectedBeginDate As String = selectedBeginYear & "/" & selectedBeginMonth & "/01"
            Dim strHeaderSelectedEndDate As String = selectedEndYear & "/" & selectedEndMonth & "/15"


            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)

            'Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)


            'Accounting close dates validation
            Me.ValidateAccountingCloseDate(CType(selectedBeginDate, Date), BeginMonthYearLabel)
            Me.ValidateAccountingCloseDate(CType(selectedEndDate, Date), EndMonthYearLabel)

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

            Dim params As ReportCeBaseForm.Params = SetParameters(dealerCode, selectedBeginDate, selectedEndDate, langCode, strYTDBeginDate, strLTMBeginDate, strHeaderSelectedBeginDate, strHeaderSelectedEndDate)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub
        Private Sub ValidateAccountingCloseDate(ByVal dtDateToCompare As Date, ByVal lbl As Label)
            If dtDateToCompare > dtLatestAccountingCloseDate Then
                ElitaPlusPage.SetLabelError(lbl)
                Throw New GUIException(Message.MSG_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED)
            End If
        End Sub
        Private Sub ValidateMinAccountingCloseDate(ByVal dtDateToCompare As Date, ByVal lbl As Label)
            dtMinLatestAccountingCloseDate = AccountingCloseInfo.GetMinAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            If dtDateToCompare < dtMinLatestAccountingCloseDate Then
                ElitaPlusPage.SetLabelError(lbl)
                Throw New GUIException(Message.MSG_GUI_CLOSED_ACCOUNTING_MONTH_NOT_FOUND, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLOSED_ACCOUNTING_MONTH_NOT_FOUND)
            End If
        End Sub
        Private Function GetPreciseDate(ByVal strInquiringDate As String, ByVal blnStartPeriod As Boolean, Optional ByVal blnMinCloseDate As Boolean = False) As String
            Dim dtInquiringDate As Date
            Dim dtInqAccountingCloseDate As Date
            Try

                If blnMinCloseDate = True Then

                    If CType(strInquiringDate, Date).Month = FEBRUARY Then
                        dtInquiringDate = (Date.Parse((strInquiringDate), System.Globalization.CultureInfo.InvariantCulture)).AddDays(1)
                        dtInqAccountingCloseDate = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, dtInquiringDate, True)
                    Else
                        dtInquiringDate = Date.Parse((strInquiringDate), System.Globalization.CultureInfo.InvariantCulture)
                        dtInqAccountingCloseDate = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, dtInquiringDate, True)
                        If blnStartPeriod Then dtInqAccountingCloseDate = dtInqAccountingCloseDate.AddDays(1)
                    End If
                Else

                    If CType(strInquiringDate, Date).Month = FEBRUARY Then
                        dtInquiringDate = (Date.Parse((strInquiringDate), System.Globalization.CultureInfo.InvariantCulture)).AddDays(1)
                        dtInqAccountingCloseDate = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, dtInquiringDate, False)
                    Else
                        dtInquiringDate = Date.Parse((strInquiringDate), System.Globalization.CultureInfo.InvariantCulture)
                        dtInqAccountingCloseDate = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, dtInquiringDate, False)
                        If blnStartPeriod Then dtInqAccountingCloseDate = dtInqAccountingCloseDate.AddDays(1)
                    End If
                End If

            Catch ex As DataNotFoundException
                Throw New GUIException(Message.MSG_GUI_CLOSED_ACCOUNTING_MONTH_NOT_FOUND, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLOSED_ACCOUNTING_MONTH_NOT_FOUND)
            End Try

            If dtInqAccountingCloseDate.Month < 10 Then
                Return dtInqAccountingCloseDate.Year & "/0" & dtInqAccountingCloseDate.Month & "/" & dtInqAccountingCloseDate.Day
            Else
                Return dtInqAccountingCloseDate.Year & "/" & dtInqAccountingCloseDate.Month & "/" & dtInqAccountingCloseDate.Day
            End If
        End Function

#End Region



    End Class

End Namespace
