Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Partial Class CustomerRegistrationReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "CUSTOMER REGISTRATION"
        Private Const RPT_FILENAME As String = "CustomerRegistration"
        ' Private Const RPT_FILENAME As String = "ExpiredCertificatesEnglishUSAdgfasf"
        Private Const RPT_FILENAME_EXPORT As String = "CustomerRegistration_Exp"

        Public Const ALL As String = "*"
        '  Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
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
        'Protected WithEvents moReportCeInputControl As ReportCeInputControl
        'Protected WithEvents ErrorCtrl As ErrorController
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
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
            Me.ClearLabelErrSign(lblProductcode)
            If Me.rproductcode.Checked Then txtProductcode.Text = String.Empty
            If Me.rrisktype.Checked Then cborisktype.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Sub PopulateDealerDropDown()
            '''Me.BindListControlToDataView(Me.cboDealerDec, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            '''Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            '''DealersLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboDealerCode, DealersLookupListSortedByCode, "CODE", , True)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, False, " document.forms[0].rdealer.checked = false;")

        End Sub
        'Sub PopulateProductDropDown()
        '    Dim dv As DataView = LookupListNew.GetProductCodeByCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
        '    Me.BindListControlToDataView(Me.cboProductcode, dv, "CODE", , True)
        'End Sub
        Sub PopulateRiskTypeDropDown()
            ' Dim dv As DataView = LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            ' Me.BindListControlToDataView(Me.cborisktype, dv, , , True)
            Dim listcontext As ListContext = New ListContext()
            Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            listcontext.CompanyGroupId = compGroupId
            Dim riskLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.cborisktype.Populate(riskLkl, New PopulateOptions() With
            {
              .AddBlankItem = True
            })

        End Sub

        Private Sub InitializeForm()
            PopulateDealerDropDown()
            'PopulateProductDropDown()
            PopulateRiskTypeDropDown()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rdealer.Checked = True
            Me.RadiobuttonTotalsOnly.Checked = True
            Me.rproductcode.Checked = True
            Me.rrisktype.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal userId As String, ByVal dealerCode As String, ByVal dealerDesc As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal isSummary As String, ByVal productcode As String, ByVal risktype As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME
            Dim moReportFormat As ReportCeBaseForm.RptFormat

            Dim exportData As String = NO

            moReportFormat = ReportCeBase.GetReportFormat(Me)



            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                isSummary = NO
                exportData = YES
                reportName = RPT_FILENAME_EXPORT
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                    {
                                     New ReportCeBaseForm.RptParam("V_USER_ID", userId),
                                     New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode),
                                     New ReportCeBaseForm.RptParam("V_DEALER_DESC", dealerDesc),
                                     New ReportCeBaseForm.RptParam("V_PRODUCT_CODE", productcode),
                                     New ReportCeBaseForm.RptParam("V_RISK_TYPE", risktype),
                                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                                     New ReportCeBaseForm.RptParam("V_IS_SUMMARY", isSummary)}


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
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)
            'Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dv, selectedDealerId)
            Dim dealerDesc As String = DealerMultipleDrop.SelectedDesc
            Dim isSummary As String
            Dim endDate As String
            Dim beginDate As String
            Dim oCountryId As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            Dim oCountry As New Country(oCountryId)
            Dim oRisktypeDV As DataView = LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim selectedrisktypeId As Guid = Me.GetSelectedItem(Me.cborisktype)
            Dim risktype As String = LookupListNew.GetDescriptionFromId(oRisktypeDV, selectedrisktypeId)
            '  Dim oProductDV As DataView = LookupListNew.GetProductCodeByCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            'Dim selectedproductId As Guid = Me.GetSelectedItem(Me.cboProductcode)
            'Dim productCode As String = LookupListNew.GetCodeFromId(oProductDV, selectedproductId)
            Dim productCode As String = txtProductcode.Text.Trim.ToString
            Dim sortOrder As String

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            If Me.RadiobuttonTotalsOnly.Checked Then
                isSummary = YES
            Else
                isSummary = NO
            End If

            If Me.rdealer.Checked Then
                dealerCode = ALL
                dealerDesc = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.rproductcode.Checked Then
                productCode = ALL
            Else
                If productCode.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(lblProductcode)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_PRODUCT_CODE_MUST_BE_ENTERED_ERR)
                End If
            End If

            If Me.rrisktype.Checked Then
                risktype = ALL
            Else
                If selectedrisktypeId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(lblrisktype)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_RISK_TYPE_MUST_BE_SELECTED_ERR)
                End If
            End If
            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(userId), dealerCode, dealerDesc, beginDate, endDate, isSummary, productCode, risktype)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class
End Namespace
