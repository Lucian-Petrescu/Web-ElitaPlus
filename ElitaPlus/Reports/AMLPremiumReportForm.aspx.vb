Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Namespace Reports
    Partial Public Class AMLPremiumReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "AMLPremium"
        Private Const RPT_FILENAME As String = "AMLPremium"
        Private Const RPT_FILENAME_EXPORT As String = "AMLPremium-Exp"

        Public Const CRYSTAL As String = "0"
        'Public Const PDF As String = "1"
        Public Const HTML As String = "2"
        Public Const CSV As String = "3"
        'Public Const EXCEL As String = "4"

        Public Const VIEW_REPORT As String = "1"
        Public Const PDF As String = "2"
        Public Const TAB As String = "3"
        Public Const EXCEL As String = "4"
        Public Const EXCEL_DATA_ONLY As String = "5"
        Public Const JAVA As String = "6"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        '  Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

        Public Const PAGETITLE As String = "AMLPremium"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "AMLPremium"

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
        Dim reportName As String = RPT_FILENAME_EXPORT
#End Region

#Region "Properties"
        Private ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property
#End Region



#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
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

        Private Sub InitializeForm()
            txtActiveCerts.Text = "5"
            PopulateCompaniesDropdown()
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            MasterPage.MessageController.Clear_Hide()
            ClearErrLabels()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            If UserCompanyMultipleDrop.Visible = False Then
                ControlMgr.SetVisibleControl(Me, trcomp, False)
            End If
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    UpdateBreadCrum()
                    'JavascriptCalls()
                    InitializeForm()
                    AddCalendar(BtnBeginDate, BeginDateText)
                    AddCalendar(BtnEndDate, EndDateText)
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(DOCTITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

#End Region
#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


#End Region

#Region "Handlers-DropDown"

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(BeginDateLabel)
            ClearLabelErrSign(EndDateLabel)
            ClearLabelErrSign(lblActiveCerts)
        End Sub

#End Region

#Region "Populate"
        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, ALL + " " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, )
            If dv.Count.Equals(ONE_ITEM) Then
                ControlMgr.SetVisibleControl(Me, trcomp, False)
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub
#End Region

#Region "Crystal Enterprise"

        Function SetParameters(compcode As String, certificatesBy As String,
                                 begindate As String, endDate As String,
                                 activeCerts_TaxId As Integer, sortOrder As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturevalue As String = TheReportCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_COMPANY", compcode),
                     New ReportCeBaseForm.RptParam("V_CERTIFICATES_BY", certificatesBy),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", begindate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_ACTIVECERTS", activeCerts_TaxId.ToString),
                     New ReportCeBaseForm.RptParam("V_SORTORDER", sortOrder),
                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue)}

            ' Me.rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function
        Function SetExpParameters(compcode As String, certificatesBy As String,
                                begindate As String, endDate As String,
                                activeCerts_TaxId As Integer, sortOrder As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturevalue As String = TheReportCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_COMPANY", compcode),
                     New ReportCeBaseForm.RptParam("V_CERTIFICATES_BY", certificatesBy),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", begindate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_ACTIVECERTS", activeCerts_TaxId.ToString),
                     New ReportCeBaseForm.RptParam("V_SORTORDER", sortOrder),
                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue)}

            ' Me.rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

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
            ' Dim compDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim compId As Guid = UserCompanyMultipleDrop.SelectedGuid
            Dim certificatesBy As String
            Dim endDate As String
            Dim beginDate As String
            Dim activeCerts As Integer
            Dim sortOrder As String
            Dim params As ReportCeBaseForm.Params

            'check for the report type
            If (rblCertificatesBy.SelectedIndex = NOTHING_SELECTED) Then
                ElitaPlusPage.SetLabelError(lblCertificatesBy)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CERTIFICATES_BY_MUST_BE_SELECTED_ERR)
            End If
            certificatesBy = rblCertificatesBy.SelectedValue

            'Dates
            ReportCeBase.ValidateBeginEndDate(BeginDateLabel, BeginDateText.Text, EndDateLabel, EndDateText.Text)
            endDate = ReportCeBase.FormatDate(EndDateLabel, EndDateText.Text)
            beginDate = ReportCeBase.FormatDate(BeginDateLabel, BeginDateText.Text)

            If compId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            If txtActiveCerts.Text.Trim.ToString = String.Empty Then
                ElitaPlusPage.SetLabelError(lblActiveCerts)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)
            ElseIf IsNumeric(txtActiveCerts.Text) Then
                activeCerts = CType(txtActiveCerts.Text, Integer)
                If ((activeCerts < 0) OrElse (activeCerts > 999)) Then
                    ElitaPlusPage.SetLabelError(lblActiveCerts)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)
                End If
            Else
                ElitaPlusPage.SetLabelError(lblActiveCerts)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)
            End If

            sortOrder = rdReportSortOrder.SelectedValue()
            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report                                    
                params = SetExpParameters(compCode, certificatesBy, beginDate, endDate, activeCerts, sortOrder)
            Else
                params = SetParameters(compCode, certificatesBy, beginDate, endDate, activeCerts, sortOrder)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

#End Region

    End Class
End Namespace
