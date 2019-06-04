Namespace Reports
    Partial Public Class AMLComplianceReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "AMLCompliance"
        Private Const RPT_FILENAME As String = "AMLCompliance"
        Private Const RPT_FILENAME_EXPORT As String = "AMLCompliance-Exp"
        Private Const RPT_FILENAME_SUSPOP As String = "AMLComplSuspOp"
        Private Const RPT_FILENAME_EXPORT_SUSPOP As String = "AMLComplSuspOp-Exp"

        Private Const RPT_TYPE_ALL As String = "ALL"
        Private Const RPT_TYPE_PEP As String = "PEP"
        Private Const RPT_TYPE_TER As String = "TER"
        Private Const RPT_TYPE_PNT As String = "PNT"

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
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

        Public Const PAGETITLE As String = "AMLCompliance"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "AMLCompliance"
        Public Const DEFAULT_PAYMENTS As String = "6"
        Public Const DEFAULT_MIN_BEGIN_DATE As String = "3"
        Public Const DEFAULT_PAID_AMT As String = "0"

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
        Dim reportName As String = RPT_FILENAME_EXPORT
#End Region

#Region "Properties"

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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region


#Region "Handlers-Init"

        Private Sub InitializeForm()
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            Me.ErrControllerMaster.Clear_Hide()
            ClearErrLabels()
            Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)

            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    InitializeForm()
                    Me.AddCalendar(Me.btnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.btnEndDate, Me.moEndDateText)
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

#End Region
#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


#End Region

#Region "Handlers-DropDown"

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
        End Sub

#End Region

#Region "Populate"
#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal userId As String, ByVal rptBasedOnDate As String,
                               ByVal beginDate As String, ByVal endDate As String,
                               ByVal rptType As String, ByVal sortOrder As String, ByVal suspOp As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturevalue As String = TheReportCeInputControl.getCultureValue(False)
            Dim reportName As String

            If suspOp = Codes.YESNO_Y Then
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_SUSPOP, False)
            Else
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME, False)
            End If


            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue),
                     New ReportCeBaseForm.RptParam("V_USER_ID", userId),
                     New ReportCeBaseForm.RptParam("V_RPT_BASED_ON_DATE", rptBasedOnDate),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_REPORT_TYPE", rptType),
                     New ReportCeBaseForm.RptParam("V_SORT_ORDER", sortOrder),
                     New ReportCeBaseForm.RptParam("V_SUSP_OP", suspOp)}

            '' Me.rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function
        Function SetExpParameters(ByVal userId As String, ByVal rptBasedOnDate As String,
                                ByVal beginDate As String, ByVal endDate As String,
                                ByVal rptType As String, ByVal sortOrder As String, ByVal suspOp As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturevalue As String = TheReportCeInputControl.getCultureValue(True)
            Dim reportName As String

            If suspOp = Codes.YESNO_Y Then
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT_SUSPOP, False)
            Else
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, False)
            End If

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue),
                     New ReportCeBaseForm.RptParam("V_USER_ID", userId),
                     New ReportCeBaseForm.RptParam("V_RPT_BASED_ON_DATE", rptBasedOnDate),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_REPORT_TYPE", rptType),
                     New ReportCeBaseForm.RptParam("V_SORT_ORDER", sortOrder),
                     New ReportCeBaseForm.RptParam("V_SUSP_OP", suspOp)}

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
            Dim endDate As String
            Dim beginDate As String
            Dim sortOrder As String
            Dim rptBasedOnDate As String
            Dim rptType As String
            Dim suspOp As String
            Dim params As ReportCeBaseForm.Params
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            If chkAllCerts.Checked Then
                rptType = RPT_TYPE_ALL
            End If

            If chkPepReport.Checked And chkTerrReport.Checked Then
                rptType = RPT_TYPE_PNT
            End If

            If chkPepReport.Checked And Not chkTerrReport.Checked Then
                rptType = RPT_TYPE_PEP
            End If

            If Not chkPepReport.Checked And chkTerrReport.Checked Then
                rptType = RPT_TYPE_TER
            End If

            If Not chkAllCerts.Checked And Not chkPepReport.Checked And Not chkTerrReport.Checked Then
                rptType = RPT_TYPE_ALL
            End If

            If chkSusOpReport.Checked Then
                suspOp = Codes.YESNO_Y
            Else
                suspOp = Codes.YESNO_N
            End If

            rptBasedOnDate = Me.rdPeriod.SelectedValue()
            sortOrder = Me.rdReportSortOrder.SelectedValue()
            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report                                    
                params = SetExpParameters(userId, rptBasedOnDate, beginDate, endDate, rptType, sortOrder, suspOp)
            Else
                params = SetParameters(userId, rptBasedOnDate, beginDate, endDate, rptType, sortOrder, suspOp)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

#End Region
    End Class
End Namespace
