Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Class NewCancellationsWEPPReportForm
        Inherits ElitaPlusPage
#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "NEW CANCELLATIONS REPORT"
        Private Const RPT_FILENAME As String = "NewCancellationsWEPP"
        Private Const RPT_FILENAME_EXPORT As String = "NewCancellationsWEPP-Exp"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button

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
            Me.ClearLabelErrSign(moDealerLabel)
        End Sub

#End Region

#Region "Populate"

        Sub PopulateDealerDropDown()
            'Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            'Dim oDescrip As String = Me.GetSelectedDescription(Me.cboDealer)
            Dim DealerList As New Collections.Generic.List(Of DataElements.ListItem)

            For Each CompanyId As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                Dim Dealers As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CompanyId = CompanyId
                                                                    })

                If Dealers.Count > 0 Then
                    If Not DealerList Is Nothing Then
                        DealerList.AddRange(Dealers)
                    Else
                        DealerList = Dealers.Clone()
                    End If
                End If
            Next

            Me.cboDealer.Populate(DealerList.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        End Sub

        Private Sub InitializeForm()
            PopulateDealerDropDown()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rdealer.Checked = True
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal UserId As String, ByVal dealerCode As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal isSummary As String, ByVal companyCurrency As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            Dim exportData As String = NO
            Dim culturevalue As String

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                exportData = YES
                reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                isSummary = NO
                culturevalue = TheRptCeInputControl.getCultureValue(True)

                repParams = New ReportCeBaseForm.RptParam() _
                   {
                    New ReportCeBaseForm.RptParam("V_USER_KEY", UserId),
                    New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode),
                    New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                    New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                    New ReportCeBaseForm.RptParam("V_IS_SUMMARY", isSummary),
                    New ReportCeBaseForm.RptParam("P_CURRENCY", companyCurrency),
                    New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue)}
            Else
                ' For SSRS report.
                reportName = TheRptCeInputControl.getReportName(RPT_FILENAME, False)
                    culturevalue = TheRptCeInputControl.getCultureValue(False)
                repParams = New ReportCeBaseForm.RptParam() _
                                {
                                 New ReportCeBaseForm.RptParam("V_USER_KEY", UserId),
                                 New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode),
                                 New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                                 New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                                 New ReportCeBaseForm.RptParam("V_IS_SUMMARY", isSummary),
                                 New ReportCeBaseForm.RptParam("P_CURRENCY", companyCurrency),
                                 New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue)}
            End If

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
            Dim UserId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim dealerID As Guid = Me.GetSelectedItem(Me.cboDealer)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dv, dealerID)
            Dim selectedDealer As String = LookupListNew.GetDescriptionFromId(dv, dealerID)
            Dim endDate As String
            Dim beginDate As String
            Dim isSummary As String = NO

            Dim countryId As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            Dim oCountry As New Country(countryId)
            Dim currencyId As Guid = oCountry.PrimaryCurrencyId
            Dim strCurrency As String = LookupListNew.GetDescriptionFromId("CURRENCIES", currencyId)

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealer.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moDealerLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If RadiobuttonTotalsOnly.Checked() Then
                isSummary = YES
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(UserId, dealerCode, beginDate, endDate, isSummary, strCurrency)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region


    End Class
End Namespace