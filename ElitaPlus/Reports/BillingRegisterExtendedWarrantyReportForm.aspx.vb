Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class BillingRegisterExtendedWarrantyReportForm
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
        Private Const RPT_FILENAME_WINDOW As String = "Billing Register"
        'Private Const RPT_FILENAME As String = "BillingRegisterDetailArgentina"
        Private Const RPT_FILENAME As String = "BillingRegisterExtendedWarranty"
        Private Const RPT_FILENAME_EXPORT As String = "BillingRegisterExtendedWarranty_Exp"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label

        Public Const DEALER_CODE As String = "0"
        Public Const DEALER_NAME As String = "1"
        Dim sortOrder As String
        Private Const ONE_ITEM As Integer = 1

        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
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
#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                rdealer.Checked = True
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label

        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl

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
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    'Date Calendars
                    Me.AddCalendar(Me.BtnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.moEndDateText)
                Else
                    ClearErrLabels()
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, ALL + " " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub

        Sub PopulateDealerDropDown()
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString)

            Dim DealerList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=listcontext)
            Me.cboDealer.Populate(DealerList, New PopulateOptions() With
                {
                   .AddBlankItem = True
                })

            Me.cboDealerCode.Populate(DealerList, New PopulateOptions() With
                {
                   .AddBlankItem = True,
                   .TextFunc = AddressOf .GetCode,
                   .SortFunc = AddressOf .GetCode
                })

            'Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            'Me.BindListControlToDataView(Me.cboDealer, dv)
            'dv.Sort = "CODE"
            'Me.BindListControlToDataView(Me.cboDealerCode, dv, "CODE", , True)
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            Dim t As Date = Date.Now.AddDays(-1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.RadiobuttonTotalsOnly.Checked = True
        End Sub

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
        End Sub

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub GenerateReport()
            Dim oCompanyId As Guid = UserCompanyMultipleDrop.SelectedGuid
            Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)
            'Dim selectedYear As String = Me.GetSelectedDescription(Me.moYearDropDownList)
            ' Dim selectedMonthID As Guid = Me.GetSelectedItem(Me.moMonthDropDownList)
            'Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            'Dim selectedYearMonth As String = selectedYear & selectedMonth
            Dim endDate As String
            Dim beginDate As String

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)


            Dim dealerID As Guid = Me.GetSelectedItem(Me.cboDealer)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyId)
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dv, dealerID)
            Dim selectedDealer As String = LookupListNew.GetDescriptionFromId(dv, dealerID)
            Dim detailCode As String
            'Dim customerRefunds As String = YES

            If Me.RadiobuttonTotalsOnly.Checked Then
                detailCode = YES
            Else
                detailCode = NO
            End If

            Select Case Me.rdReportSortOrder.SelectedValue()
                Case DEALER_CODE
                    sortOrder = "C"
                Case DEALER_NAME
                    sortOrder = "N"
            End Select

            'If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
            '    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            'End If

            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealer.Equals(Guid.Empty) Then
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If


            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, dealerCode, beginDate, endDate, detailCode, sortOrder)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetParameters(ByVal companyCode As String, ByVal dealerCode As String, ByVal begindate As String, ByVal enddate As String, ByVal isSummary As String, ByVal sortorder As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME

            Dim exportData As String = NO
            ' Dim isSummary As String = YES

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = RPT_FILENAME_EXPORT
                isSummary = NO
            End If

            'Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
            '        { _
            '         New ReportCeBaseForm.RptParam("V_COMPANY", companyCode), _
            '         New ReportCeBaseForm.RptParam("V_DEALER", dealerCode), _
            '         New ReportCeBaseForm.RptParam("V_YEAR_MONTH", selectedYearMonth)}

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
            { _
             New ReportCeBaseForm.RptParam("V_COMPANY", companyCode), _
             New ReportCeBaseForm.RptParam("V_DEALER", dealerCode), _
             New ReportCeBaseForm.RptParam("V_BEGIN_DATE", begindate), _
             New ReportCeBaseForm.RptParam("V_END_DATE", enddate), _
             New ReportCeBaseForm.RptParam("V_IS_SUMMARY", isSummary), _
             New ReportCeBaseForm.RptParam("V_SORT_ORDER", sortorder)}

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