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

    Partial Class DealerEndorsementsReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "DEALER_ENDORSEMENTS"
        Private Const RPT_FILENAME As String = "DealerEndorsementsSpanish"
        Private Const RPT_FILENAME_EXPORT As String = "DealerEndorsementsSpanish_Exp"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"

        Private Const COL_NAME_ACCOUNTING_CLOSE_INFO_ID As String = "ACCOUNTING_CLOSE_INFO_ID"
        Private Const COL_NAME_CLOSING_DATE As String = "CLOSING_DATE"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public userId As String
            Public selectedYearMonth As String
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
                    If ViewState("dtLatestAccountingCloseDate") IsNot System.DBNull.Value Then
                        dtLatestAccountingCloseDate = CType(ViewState("dtLatestAccountingCloseDate"), Date)
                    End If
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
            ClearLabelErrSign(MonthYearLabel)
            ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            ClearLabelErrSign(moEndorsmentNumberLabel)
            If rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDealerDropDown()
            '''Me.BindListControlToDataView(Me.cboDealerDec, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            '''Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            '''DealersLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboDealerCode, DealersLookupListSortedByCode, "CODE", , True)

            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;")

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
            .ValueFunc = AddressOf PopulateOptions.GetCode
                  })

        End Sub

        Private Sub PopulateMonthsDropdown()
            ' Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            ' dv.Sort = "CODE"
            'Me.BindListControlToDataView(Me.MonthDropDownList, dv, , , True)
            MonthDropDownList.Populate(CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        End Sub

        Private Sub InitializeForm()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
            rdealer.Checked = True
            rEndorsementNumber.Checked = True
            'load Accounting Close Date
            dtLatestAccountingCloseDate = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Date.Today)
            ViewState("dtLatestAccountingCloseDate") = dtLatestAccountingCloseDate
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(userId As String, dealerCode As String, selectedYearAndMonth As String, EndorsementNumber As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME
            Dim moReportFormat As ReportCeBaseForm.RptFormat

            Dim exportData As String = NO

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = RPT_FILENAME_EXPORT
            End If


            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                                {
                                                 New ReportCeBaseForm.RptParam("V_USER_KEY", userId),
                                                 New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                                                 New ReportCeBaseForm.RptParam("V_ENDORSEMENT_NUMBER", EndorsementNumber),
                                                 New ReportCeBaseForm.RptParam("V_BEGIN_DATE", selectedYearAndMonth)}


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
            Dim selectedYear As String = GetSelectedDescription(YearDropDownList)
            Dim selectedMonthID As Guid = GetSelectedItem(MonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)

            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)
            'Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim EndorsementNumber As String
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)

            If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(MonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            'Dim selectedDate As String = GetPreciseDate(selectedYear & "/" & selectedMonth & "/01")
            'Dim beginDate As Date = Date.Parse(selectedDate, System.Globalization.CultureInfo.InvariantCulture)
            'Me.ValidateAccountingCloseDate(beginDate, MonthYearLabel)
            Dim selectedDateString As String = selectedYear & selectedMonth & "01"

            'Endorsment validation
            If rEndorsementNumber.Checked Then
                EndorsementNumber = ALL
            Else
                If txtEndorsmentNumber.Text = "" Then
                    ElitaPlusPage.SetLabelError(moEndorsmentNumberLabel)
                    Throw New GUIException(Message.MSG_GUI_ENDORSEMENT_NUMBER_NOT_ENTERED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ENDORSEMENT_NUMBER_NOT_ENTERED)
                Else
                    EndorsementNumber = txtEndorsmentNumber.Text
                End If
            End If
            'dealer validation
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

            Dim params As ReportCeBaseForm.Params = SetParameters(userId, dealerCode, selectedDateString, EndorsementNumber)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub
        Private Sub ValidateAccountingCloseDate(dtDateToCompare As Date, lbl As Label)
            If dtDateToCompare > dtLatestAccountingCloseDate Then
                ElitaPlusPage.SetLabelError(lbl)
                Throw New GUIException(Message.MSG_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED)
            End If
        End Sub
        Private Function GetPreciseDate(strInquiringDate As String) As String
            Dim dtInquiringDate As Date = Date.Parse((strInquiringDate), System.Globalization.CultureInfo.InvariantCulture)
            Dim dtInqAccountingCloseDate As Date = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, dtInquiringDate)
            dtInqAccountingCloseDate = dtInqAccountingCloseDate.AddDays(1)
            If dtInqAccountingCloseDate.Month < 10 Then
                Return dtInqAccountingCloseDate.Year & "/0" & dtInqAccountingCloseDate.Month & "/" & dtInqAccountingCloseDate.Day
            Else
                Return dtInqAccountingCloseDate.Year & "/" & dtInqAccountingCloseDate.Month & "/" & dtInqAccountingCloseDate.Day
            End If
        End Function

#End Region



    End Class

End Namespace
