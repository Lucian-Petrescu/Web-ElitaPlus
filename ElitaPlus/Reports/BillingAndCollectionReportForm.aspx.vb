Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports

    Partial Class BillingAndCollectionReportForm

        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "BILLING_AND_COLLECTION_RPT"
        Private Const RPT_FILENAME As String = "" ' "Accidental_Protection_Reports"
        Private Const RPT_FILENAME_EXPORT As String = "Billing-Collection-PAY-Exp" ' "NewCertificatesWEPP-Exp""

        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

        '  Private Const TOTALPARAMS As Integer = 8  ' 23
        ' Private Const TOTALEXPPARAMS As Integer = 8  ' 7
        ' Private Const PARAMS_PER_REPORT As Integer = 8 '8
#End Region

        '#Region "parameters"
        '        Public Structure ReportParams
        '            Public userId As String
        '            Public dealerCode As String
        '            Public beginDate As String
        '            Public endDate As String
        '            Public sCurrency As String
        '            ' Public isSummary As String
        '            ' Public totalsByCov As String
        '            Public culturevalue As String
        '        End Structure
        '#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
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

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        Protected WithEvents errorctrl As Assurant.ElitaPlus.ElitaPlusWebApp.ErrorController

        'Protected WithEvents moReportCeInputControl As ReportCeInputControl
        ''Protected WithEvents ErrorCtrl As ErrorController
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        '   Protected WithEvents ddlReportType As Global.System.Web.UI.WebControls.DropDownList


        Private designerPlaceholderDeclaration As System.Object


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            'Put user code to initialize the page here
            Me.errorctrl.Clear_Hide()

            Try
                If Not Me.IsPostBack Then
                    LabelReportHeader.Text = RPT_FILENAME_WINDOW

                    InitializeForm()

                    'Date Calendars
                    Me.AddCalendar(Me.btnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.btnEndDate, Me.moEndDateText)
                Else
                    ClearErrLabels()
                End If

                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.errorctrl)
            End Try

            Me.ShowMissingTranslations(Me.errorctrl)
        End Sub
#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(mobegindatelabel)
            Me.ClearLabelErrSign(moenddatelabel)
            '            Me.ClearLabelErrSign(lblCoverage)
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
            '     If Me.rbCoverage.Checked Then ddlCoverage.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Sub PopulateDropDowns()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;")
        End Sub

        Private Sub InitializeForm()
            'moReportCeInputControl.ExcludeExport()
            moReportCeInputControl.SetExportOnly()

            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)

            PopulateDropDowns()

            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rdealer.Checked = True
            '    Me.rbCoverage.Checked = True
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngenrpt.Click
            Try
                Dim params As ReportCeBaseForm.Params

                Dim strRrpt_Mode As String
                Dim strDate_Type As String = rdDate_type.SelectedValue()
                ' Dim strRrpt_Type As String = ddlReportType.SelectedValue
                Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
                Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)

                'Dates
                ReportCeBase.ValidateBeginEndDate(mobegindatelabel, moBeginDateText.Text, moenddatelabel, moEndDateText.Text)

                Dim endDate As String = ReportCeBase.FormatDate(moenddatelabel, moEndDateText.Text)
                Dim beginDate As String = ReportCeBase.FormatDate(mobegindatelabel, moBeginDateText.Text)


                If Me.rdealer.Checked Then
                    selectedDealerId = Guid.Empty
                Else
                    If selectedDealerId.Equals(Guid.Empty) Then
                        ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                    End If
                End If

                strDate_Type = rdDate_type.SelectedValue()


                ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

                moReportFormat = ReportCeBase.GetReportFormat(Me)

                If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                    'Export Report
                    strRrpt_Mode = "CSV"
                Else
                    'View Report
                    strRrpt_Mode = "RPT"
                End If

                params = SetParameters(strRrpt_Mode _
                                       , GuidControl.GuidToHexString(userId) _
                                       , "" _
                                       , strDate_Type _
                                       , beginDate _
                                       , endDate _
                                       , GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id) _
                                       , GuidControl.GuidToHexString(selectedDealerId))

                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

            Catch ex As Threading.ThreadAbortException

            Catch ex As Exception
                Me.HandleErrors(ex, Me.errorctrl)
            End Try
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal rptMode As String,
                               ByVal userId As String,
                               ByVal RPT_Type As String,
                               ByVal Date_Type As String,
                               ByVal beginDate As String,
                               ByVal endDate As String,
                               ByVal company_groupId As String,
                               ByVal dealerId As String) As ReportCeBaseForm.Params
            ' ByVal CoverageId As String) As ReportCeBaseForm.Params

            Dim reportName As String
            Dim SubfileName As String = ""

            Dim Params As New ReportCeBaseForm.Params

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            ' Dim culturevalue As String = TheReportCeInputControl.getCultureValue(False)

            If rptMode = "RPT" Then
                reportName = RPT_FILENAME & "_EN"
                '     reportName = TheReportCeInputControl.getReportName(RPT_FILENAME, False)
            Else
                reportName = RPT_FILENAME_EXPORT & "_EN"
                'reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
            End If

            Dim repParams(7) As ReportCeBaseForm.RptParam

            repParams(0) = New ReportCeBaseForm.RptParam("PI_USER_KEY", userId, SubfileName)
            repParams(1) = New ReportCeBaseForm.RptParam("PI_REPORT_TYPE", RPT_Type, SubfileName)
            repParams(2) = New ReportCeBaseForm.RptParam("PI_DATE_TYPE", Date_Type, SubfileName)
            repParams(3) = New ReportCeBaseForm.RptParam("PI_START_DATE", beginDate, SubfileName)
            repParams(4) = New ReportCeBaseForm.RptParam("PI_END_DATE", endDate, SubfileName)
            repParams(5) = New ReportCeBaseForm.RptParam("PI_COMPANY_GROUP_ID", company_groupId, SubfileName)
            repParams(6) = New ReportCeBaseForm.RptParam("PI_DEALER_ID", dealerId, SubfileName)
            '  repParams(6) = New ReportCeBaseForm.RptParam("PI_COVERAGE_ID", CoverageId, SubfileName)

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return Params
        End Function

#End Region

        Protected Sub rdealer_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rdealer.CheckedChanged

        End Sub
    End Class


End Namespace
