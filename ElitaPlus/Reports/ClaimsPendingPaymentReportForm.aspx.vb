Namespace Reports
    Partial Class ClaimsPendingPaymentReportForm
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public userId As String
            Public beginbatch As String
            Public endbatch As String
            Public reportType As String
            Public SvcCode As String
            Public taxtype As String
            Public cultureCode As String
        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "CLAIMS_PENDING_PAYMENTS"
        Private Const RPT_FILENAME As String = "ClaimsPendingPayments"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimsPendingPayments-Exp"
        Private Const BY_BATCH_NUMBER As String = "B"
        Private Const PERIOD_ROW As String = "period"
        Private Const INVOICE_ROW As String = "invoice"
        Private Const PAYEE_ROW As String = "payee"
        Private Const BATCH_ROW As String = "batch"
        Private Const TOTAL_PARAMS As Integer = 7 ' 21 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 7 ' 6 Elements
        Private Const PARAMS_PER_REPORT As Integer = 7 ' 6 Elements

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
        Public Const PAGETITLE As String = "CLAIMS_PENDING_PAYMENTS"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "CLAIMS_PENDING_PAYMENTS"

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

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents rdReportFormat_NO_TRANSLATE As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents Label5 As System.Web.UI.WebControls.Label
        Protected WithEvents rdReportSortOrder As System.Web.UI.WebControls.RadioButtonList
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
            chkSvcCode.Checked = False
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrControllerMaster.Clear_Hide()
            ClearErrLabels()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    InitializeForm()
                    TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
                    'Date Calendars                    
                Else
                    ClearErrLabels()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)

        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(lblbeginbatch)
            ClearLabelErrSign(lblendbatch)
        End Sub

#End Region

#Region "Populate"

#End Region

#Region "Crystal Enterprise"

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam,
                        rptName As String, startIndex As Integer)
            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_KEY", .userId, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_BEGIN_BATCH", .beginbatch, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_END_BATCH", .endbatch, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_REPORT_TYPE", .reportType, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_INCLUDE_SVCCODE", .SvcCode, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_TAX_TYPE", .taxtype, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", .cultureCode, rptName)
            End With

        End Sub

        Function SetParameters(userId As String, beginbatch As String,
                               endbatch As String, selectionType As String,
                               svccode As String, taxtype As String, culturecode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            'Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            'Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)
            reportFormat = ReportCeBase.GetReportFormat(Me)

            With rptParams
                .userId = userId
                .beginbatch = beginbatch
                .endbatch = endbatch
                .reportType = selectionType
                .SvcCode = svccode
                .taxtype = taxtype
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

        Function SetExpParameters(userId As String, beginbatch As String,
                                  endbatch As String, selectionType As String,
                                  svccode As String, taxtype As String, culturecode As String) As ReportCeBaseForm.Params

            Dim reportName As String
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim reportFormat As ReportCeBaseForm.RptFormat

            reportFormat = ReportCeBase.GetReportFormat(Me)
            reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            With rptParams
                .userId = userId
                .beginbatch = beginbatch
                .endbatch = endbatch
                .reportType = selectionType
                .SvcCode = svccode
                .taxtype = taxtype
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
            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False)
            Dim svccode As String
            Dim ds As DataSet
            Dim invoiceTrans As New InvoiceTrans
            Dim taxtypeID As Guid
            Dim taxtype As String
            Dim beginbatch As String
            Dim endbatch As String


            ' taxtype code - 4 = Manual...
            taxtypeID = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, "4")
            ds = invoiceTrans.CheckInvoiceTaxType(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID, taxtypeID, Guid.Empty)
            If ds.Tables(0).Rows.Count > 0 Then
                taxtype = "Invoice"
            Else
                taxtype = String.Empty
            End If

            reportFormat = ReportCeBase.GetReportFormat(Me)
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse _
                reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                culturecode = TheReportCeInputControl.getCultureValue(True)
            End If

            selectionType = BY_BATCH_NUMBER

            beginbatch = txtBeginbatch.Text.Trim
            endbatch = txtEndbatch.Text.Trim

            If (beginbatch Is Nothing Or beginbatch Is String.Empty) Then
                ElitaPlusPage.SetLabelError(lblbeginbatch)
                Throw New GUIException(Message.MSG_INVOICE_NUMBER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_BATCH_NUMBER_MUST_BE_ENTERED_ERR)
            End If
            If (endbatch Is Nothing Or endbatch Is String.Empty) Then
                ElitaPlusPage.SetLabelError(lblendbatch)
                Throw New GUIException(Message.MSG_INVOICE_NUMBER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_BATCH_NUMBER_MUST_BE_ENTERED_ERR)
            End If
            If beginbatch > endbatch Then
                Throw New GUIException(Message.MSG_INVOICE_NUMBER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_BATCH_NUMBER_MUST_BE_LESS_THAN_END_BATCH_NUMBER_ERR)
            End If

            If chkSvcCode.Checked = True Then
                svccode = YES
            Else
                svccode = NO
            End If

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                '   reportName = RPT_FILENAME_EXPORT
                params = SetExpParameters(userId, beginbatch, endbatch, selectionType, svccode, taxtype, culturecode)
            Else
                'View Report
                params = SetParameters(userId, beginbatch, endbatch, selectionType, svccode, taxtype, culturecode)
            End If
            'Dim params As ReportCeBaseForm.Params = SetParameters(compCode, invoiceNumber, payee, beginDate, endDate)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub
#End Region

    End Class

End Namespace