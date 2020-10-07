Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Class DKInvoicesToBePaidForm
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public userId As String
            Public payee As String
            Public invoiceNumber As String
            Public beginDate As String
            Public endDate As String
            Public reportType As String
            Public SvcCode As String
            Public cultureCode As String
        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "DK Invoices To Be Paid"
        Private Const RPT_FILENAME As String = "DKInvoicesToBePaid"
        Private Const RPT_FILENAME_EXPORT As String = "InvoicesToBePaid-Exp"
        Private Const BY_REPORTING_PERIOD As String = "P"
        Private Const BY_INVOICE_NUMBER As String = "I"
        Private Const PERIOD_ROW As String = "period"
        Private Const INVOICE_ROW As String = "invoice"
        Private Const PAYEE_ROW As String = "payee"
        Private Const TOTAL_PARAMS As Integer = 23 ' 21 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 7 ' 6 Elements
        Private Const PARAMS_PER_REPORT As Integer = 8 ' 6 Elements

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
        '   Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

#End Region

#Region "variables"
        'Private reportFormat As ReportCeBaseForm.RptFormat
        'Private reportName As String = RPT_FILENAME
        Private showPayeeRowFlag As Boolean
        Private invoiceNumber As String
        Private payee As String
        'Private tempEndDate As Date
        'Private tempBeginDate As Date
        'Private endDate As String
        'Private beginDate As String

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
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents rdReportFormat_NO_TRANSLATE As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents Label5 As System.Web.UI.WebControls.Label
        Protected WithEvents rdReportSortOrder As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
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
            Dim t As Date = Date.Now.AddDays(-6)
            BeginDateText.Text = GetDateFormattedString(t)
            EndDateText.Text = GetDateFormattedString(Date.Now)
            RadiobuttonByReportingPeriod.Checked = True
            chkSvcCode.Checked = False
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                    TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
                    'Date Calendars
                    AddCalendar(BtnBeginDate, BeginDateText)
                    AddCalendar(BtnEndDate, EndDateText)
                Else
                    ClearErrLabels()
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
            ClearLabelErrSign(BeginDateLabel)
            ClearLabelErrSign(EndDateLabel)
            ClearLabelErrSign(InvoiceNumberLabel)
        End Sub

#End Region

#Region "Populate"

        Sub BindPayee(invoiceNumber As String)
            Try
                If invoiceNumber IsNot Nothing AndAlso invoiceNumber.Trim.Length > 0 Then
                    PayeeLabel.Visible = True
                    cboPayee.Visible = True
                    Dim companyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
                    'Dim dv As Disbursement.DisbursementSearchDV = Disbursement.GetList(invoiceNumber, companyList)
                    'Me.BindListControlToDataView(Me.cboPayee, dv, "payee", "disbursement_id", False)

                    Dim PayeeList As New Collections.Generic.List(Of DataElements.ListItem)
                    For Each Company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                        Dim Payee As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.PayeeListByInvoiceNumberAndCompany,
                                                                                context:=New ListContext() With
                                                                                {
                                                                                  .CompanyId = Company_id,
                                                                                  .InvoiceNumber = invoiceNumber
                                                                                })

                        If Payee.Count > 0 Then
                            If PayeeList IsNot Nothing Then
                                PayeeList.AddRange(Payee)
                            Else
                                PayeeList = Payee.Clone()
                            End If
                        End If
                    Next

                    cboPayee.Populate(PayeeList.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = False
                        })

                    If (PayeeList.Any() > 0) Then
                        'HidePayeeRow()
                        showPayeeRowFlag = True
                        Throw New GUIException(Message.MSG_NO_PAYEE_FOUND, Assurant.ElitaPlus.Common.ErrorCodes.GUI_NO_PAYEE_FOUND_ERR)
                    End If
                    'Set flag to true
                    showPayeeRowFlag = True
                Else
                    'set flag false
                    showPayeeRowFlag = False
                    HidePayeeRow()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub HidePayeeRow()
            cboPayee.Items.Clear()
            PayeeLabel.Visible = False
            cboPayee.Visible = False
        End Sub
        Private Sub InvoiceNumberTextbox_TextChanged(sender As Object, e As System.EventArgs) Handles InvoiceNumberTextbox.TextChanged

            BindPayee(InvoiceNumberTextbox.Text)
            'Me.InvoiceNumberTextbox.Attributes.Add(onchange, 
        End Sub

        Public Function toggleDisplay(rowParam As String) As String

            Select Case rowParam
                Case INVOICE_ROW
                    If (RadiobuttonByInvoiceNumber.Checked) Then
                        Return "style='display:block;'"
                    End If
                Case PAYEE_ROW
                    If ((RadiobuttonByInvoiceNumber.Checked) AndAlso
                         Not InvoiceNumberTextbox.Equals(String.Empty)) Then
                        ' Me.InvoiceNumberTextbox.Text.is(showPayeeRowFlag)) Then
                        Return "style='display:block;'"
                    End If
                Case PERIOD_ROW
                    If (RadiobuttonByReportingPeriod.Checked) Then
                        Return "style='display:block;'"
                    End If
            End Select

            Return "style='display:none;'"

            ' Return "style='display:none'"

        End Function

#End Region

#Region "Crystal Enterprise"

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam,
                        rptName As String, startIndex As Integer)
            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_KEY", .userId, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_SVC_CONTROL_NUMBER", .invoiceNumber, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_PAYEE", .payee, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .beginDate, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_END_DATE", .endDate, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_REPORT_TYPE", .reportType, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_INCLUDE_SVCCODE", .SvcCode, rptName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", .cultureCode, rptName)
            End With

        End Sub

        Function SetParameters(userId As String, invoiceNumber As String, payee As String,
                                beginDate As String, endDate As String, selectionType As String, svccode As String, culturecode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            'Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)
            reportFormat = ReportCeBase.GetReportFormat(Me)

            'If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse _
            '    reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
            '    reportName = RPT_FILENAME_EXPORT
            'End If

            With rptParams
                .userId = userId
                .payee = payee
                .invoiceNumber = invoiceNumber
                .beginDate = beginDate
                .endDate = endDate
                .reportType = selectionType
                .SvcCode = svccode
                .cultureCode = culturecode
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            SetReportParams(rptParams, repParams, "Payee Summary", PARAMS_PER_REPORT * 1)  ' Payees SubReport
            SetReportParams(rptParams, repParams, "Dealer Summary", PARAMS_PER_REPORT * 2) ' Dealer SubReport

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Function SetExpParameters(userId As String, invoiceNumber As String, payee As String,
                                beginDate As String, endDate As String, selectionType As String, svccode As String, culturecode As String) As ReportCeBaseForm.Params

            Dim reportName As String
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim reportFormat As ReportCeBaseForm.RptFormat

            reportFormat = ReportCeBase.GetReportFormat(Me)
            reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            With rptParams
                .userId = userId
                .payee = payee
                .invoiceNumber = invoiceNumber
                .beginDate = beginDate
                .endDate = endDate
                .reportType = selectionType
                .SvcCode = svccode
                .cultureCode = culturecode
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

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
            Dim selectionType As String
            Dim params As ReportCeBaseForm.Params
            Dim endDate As String
            Dim beginDate As String
            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim culturecode As String = TheRptCeInputControl.getCultureValue(False)
            Dim svccode As String

            reportFormat = ReportCeBase.GetReportFormat(Me)
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse
                reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                culturecode = TheRptCeInputControl.getCultureValue(True)
            End If

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(RPT_FILENAME_WINDOW)

            If (RadiobuttonByReportingPeriod.Checked) Then
                selectionType = BY_REPORTING_PERIOD
            Else
                selectionType = BY_INVOICE_NUMBER
            End If

            If (selectionType = BY_REPORTING_PERIOD) Then
                invoiceNumber = Nothing
                payee = Nothing

                'Dates
                ReportCeBase.ValidateBeginEndDate(BeginDateLabel, BeginDateText.Text, EndDateLabel, EndDateText.Text)
                endDate = ReportCeBase.FormatDate(EndDateLabel, EndDateText.Text)
                beginDate = ReportCeBase.FormatDate(BeginDateLabel, BeginDateText.Text)
            Else
                beginDate = Nothing
                endDate = Nothing
                invoiceNumber = InvoiceNumberTextbox.Text
                If cboPayee.Items.Count > 0 Then
                    payee = cboPayee.SelectedItem.Text
                End If
                If (invoiceNumber Is Nothing) Then
                    ElitaPlusPage.SetLabelError(InvoiceNumberLabel)
                    Throw New GUIException(Message.MSG_INVOICE_NUMBER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_NUMBER_MUST_BE_ENTERED_ERR)
                End If
                If payee Is Nothing Then
                    ElitaPlusPage.SetLabelError(PayeeLabel)
                    Throw New GUIException(Message.MSG_NO_PAYEE_FOUND, Assurant.ElitaPlus.Common.ErrorCodes.GUI_NO_PAYEE_FOUND_ERR)
                End If
            End If

            If chkSvcCode.Checked = True Then
                svccode = YES
            Else
                svccode = NO
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                '   reportName = RPT_FILENAME_EXPORT
                params = SetExpParameters(userId, invoiceNumber, payee, beginDate, endDate, selectionType, svccode, culturecode)
            Else
                'View Report
                params = SetParameters(userId, invoiceNumber, payee, beginDate, endDate, selectionType, svccode, culturecode)
            End If
            'Dim params As ReportCeBaseForm.Params = SetParameters(compCode, invoiceNumber, payee, beginDate, endDate)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub
#End Region

    End Class

End Namespace