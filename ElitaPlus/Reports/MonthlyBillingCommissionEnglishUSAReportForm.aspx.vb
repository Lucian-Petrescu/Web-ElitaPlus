Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Microsoft.VisualBasic

Namespace Reports

    Partial Class MonthlyBillingCommissionEnglishUSAReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "MONTHLY BILLING COMMISSION REPORT"
        Private Const RPT_FILENAME As String = "MonthlyBillingCommissionEnglishUSA"
        Private Const RPT_FILENAME_EXPORT As String = "MonthlyBillingCommissionEnglishUSA_Exp"

        Public Const ALL As String = "*"
        '  Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"

        Public Const DEFAULT_BROKER_COMM_RATE As Decimal = 0

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
#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController
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
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
            Me.ClearLabelErrSign(moBrokerCommPercentLabel)
        End Sub

#End Region

#Region "Populate"

        '08/02/2006 - ALR - Modified filter to filter the query with multiple companies
        Sub PopulateDealerDropDown()

            'Dim todayDate As String = ReportCeBase.FormatDate(Nothing, Date.Today.ToString)
            'Dim dv As DataView = Dealer.GetDealersWithMonthlyBilling(GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id), todayDate)
            'Me.BindListControlToDataView(Me.cboDealer, dv, , , True)

            Dim dealerWithMonthlyBillingList As New Collections.Generic.List(Of DataElements.ListItem)
            Dim oListContext As New ListContext
            For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                oListContext.CompanyId = _company
                Dim dealerListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerWithMonthlyBillingContractByCompany", context:=oListContext, languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                If dealerListForCompany.Count > 0 Then
                    If Not dealerWithMonthlyBillingList Is Nothing Then
                        dealerWithMonthlyBillingList.AddRange(dealerListForCompany)
                    Else
                        dealerWithMonthlyBillingList = dealerListForCompany.Clone()
                    End If
                End If
            Next

            Me.cboDealer.Populate(dealerWithMonthlyBillingList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

            Dim oDescrip As String = Me.GetSelectedDescription(Me.cboDealer)

        End Sub

        Private Sub InitializeForm()
            PopulateDealerDropDown()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rdealer.Checked = True
            Me.PopulateControlFromBOProperty(Me.moBrokerCommPercentText, Me.DEFAULT_BROKER_COMM_RATE, Me.DECIMAL_FORMAT)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal userId As String, ByVal dealerCode As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal brokerCommPercent As Decimal, ByVal dealerName As String) As ReportCeBaseForm.Params

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
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_PERCENT", brokerCommPercent.ToString),
                     New ReportCeBaseForm.RptParam("V_DEALER_NAME", dealerName)}


            'If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) Then
            '    reportName = RPT_FILENAME_EXPORT
            'End If

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

            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim dealerID As Guid = Me.GetSelectedItem(Me.cboDealer)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dv, dealerID)
            Dim selectedDealer As String = LookupListNew.GetDescriptionFromId(dv, dealerID)
            Dim endDate As String
            Dim beginDate As String
            Dim brokerCommPercent As Decimal

            If IsNumeric(Me.moBrokerCommPercentText.Text()) Then brokerCommPercent = CType(Me.moBrokerCommPercentText.Text, Decimal) Else brokerCommPercent = 0

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            If brokerCommPercent < 0 OrElse brokerCommPercent > 99.0 Then
                ElitaPlusPage.SetLabelError(moBrokerCommPercentLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_BROKER_COMM_PERCENT)
            End If

            If Me.rdealer.Checked Then
                dealerCode = ALL
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(userId, dealerCode, beginDate, endDate, brokerCommPercent, selectedDealer)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class
End Namespace
