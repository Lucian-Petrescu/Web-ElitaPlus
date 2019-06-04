Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Class InvoicesToBePaidFreeZoneForm
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public userId As String
            Public payee As String
            Public invoiceNumber As String
            Public beginDate As String
            Public endDate As String
            Public IsFreeZone As String
            Public reportType As String
            Public SvcCode As String
            Public taxtype As String
            Public customerAddress As String
            Public cultureCode As String
            Public companyCode As String
            Public dealerCode As String
            Public dealerForCur As String
            Public rptCurrency As String
        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "INVOICES TO BE PAID INCLUDING FREE ZONE"
        Private Const RPT_FILENAME As String = "InvoicesToBePaidFreeZone"
        Private Const RPT_FILENAME_EXPORT As String = "InvoicesToBePaidFreeZone-Exp"
        Private Const BY_REPORTING_PERIOD As String = "P"
        Private Const BY_INVOICE_NUMBER As String = "I"
        Private Const PERIOD_ROW As String = "period"
        Private Const INVOICE_ROW As String = "invoice"
        Private Const PAYEE_ROW As String = "payee"
        Private Const TOTAL_PARAMS As Integer = 42
        Private Const TOTAL_EXP_PARAMS As Integer = 14
        Private Const PARAMS_PER_REPORT As Integer = 14

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
        Public Const ALLSVC As String = "ALL"
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub InitializeForm()
            Dim t As Date = Date.Now.AddDays(-6)
            Me.BeginDateText.Text = GetDateFormattedString(t)
            Me.EndDateText.Text = GetDateFormattedString(Date.Now)
            Me.RadiobuttonByReportingPeriod.Checked = True
            Me.chkSvcCode.Checked = False
            Me.rSvcCtr.Checked = True
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
                    'Date Calendars
                    Me.AddCalendar(Me.BtnBeginDate, Me.BeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.EndDateText)
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
            Me.ClearLabelErrSign(BeginDateLabel)
            Me.ClearLabelErrSign(EndDateLabel)
            Me.ClearLabelErrSign(InvoiceNumberLabel)
        End Sub

#End Region

#Region "Populate"

        Sub BindPayee(ByVal invoiceNumber As String)
            Try
                If Not invoiceNumber Is Nothing AndAlso invoiceNumber.Trim.Length > 0 Then
                    Me.PayeeLabel.Visible = True
                    Me.cboPayee.Visible = True
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
                            If Not PayeeList Is Nothing Then
                                PayeeList.AddRange(Payee)
                            Else
                                PayeeList = Payee.Clone()
                            End If
                        End If
                    Next

                    Me.cboPayee.Populate(PayeeList.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = False
                        })


                    If (PayeeList.Any()) Then
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
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub HidePayeeRow()
            Me.cboPayee.Items.Clear()
            Me.PayeeLabel.Visible = False
            Me.cboPayee.Visible = False
        End Sub
        Private Sub InvoiceNumberTextbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles InvoiceNumberTextbox.TextChanged

            Me.BindPayee(Me.InvoiceNumberTextbox.Text)
            'Me.InvoiceNumberTextbox.Attributes.Add(onchange, 
        End Sub

        Public Function toggleDisplay(ByVal rowParam As String) As String

            Select Case rowParam
                Case INVOICE_ROW
                    If (Me.RadiobuttonByInvoiceNumber.Checked) Then
                        Return "style='display:block;'"
                    End If
                Case PAYEE_ROW
                    If ((Me.RadiobuttonByInvoiceNumber.Checked) AndAlso
                         Not InvoiceNumberTextbox.Equals(String.Empty)) Then
                        ' Me.InvoiceNumberTextbox.Text.is(showPayeeRowFlag)) Then
                        Return "style='display:block;'"
                    End If
                Case PERIOD_ROW
                    If (Me.RadiobuttonByReportingPeriod.Checked) Then
                        Return "style='display:block;'"
                    End If
            End Select

            Return "style='display:none;'"

            ' Return "style='display:none'"

        End Function

#End Region

#Region "Crystal Enterprise"

        Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                        ByVal rptName As String, ByVal startIndex As Integer)

            If (rptName = String.Empty) Then ' 1st param for main report only 
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", rptParams.cultureCode, rptName)
            End If

            With rptParams
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("PI_USER_KEY", .userId, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("PI_REPORT_TYPE", .reportType, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("PI_BEGIN_DATE", .beginDate, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("PI_END_DATE", .endDate, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("PI_PAYEE", .payee, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("PI_SVC_CONTROL_NUMBER", .invoiceNumber, rptName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("PI_FREE_ZONE_FLAG", "ALL", rptName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("PI_INCLUDE_SVCCODE", .SvcCode, rptName)
                repParams(startIndex + 9) = New ReportCeBaseForm.RptParam("PI_INCLUDE_CUSTOMER_ADDR", .customerAddress, rptName)
                repParams(startIndex + 10) = New ReportCeBaseForm.RptParam("PI_COMPANY_CODE", .companyCode, rptName)
                repParams(startIndex + 11) = New ReportCeBaseForm.RptParam("PI_DEALER_CODE", .dealerCode, rptName)
                repParams(startIndex + 12) = New ReportCeBaseForm.RptParam("PI_DEALER_WITH_CUR", .dealerForCur, rptName)
                repParams(startIndex + 13) = New ReportCeBaseForm.RptParam("PI_RPT_CUR", .rptCurrency, rptName)
                repParams(startIndex + 14) = New ReportCeBaseForm.RptParam("PI_TAX_TYPE", .taxtype, rptName)
            End With

        End Sub

        Function SetParameters(ByVal userId As String, ByVal invoiceNumber As String, ByVal payee As String,
                               ByVal beginDate As String, ByVal endDate As String, ByVal FreeZoneFlag As String, ByVal selectionType As String,
                               ByVal svccode As String, ByVal taxtype As String, ByVal culturecode As String,
                               ByVal customerAddress As String, ByVal companyCode As String, ByVal dealerCode As String, ByVal dealerForCur As Guid, ByVal rptCurrency As Guid) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            'Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            'Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
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
                .IsFreeZone = FreeZoneFlag
                .reportType = selectionType
                .SvcCode = svccode
                .taxtype = taxtype
                .customerAddress = customerAddress
                .cultureCode = culturecode
                .companyCode = companyCode
                .dealerCode = dealerCode
                .dealerForCur = DALBase.GuidToSQLString(dealerForCur)
                .rptCurrency = DALBase.GuidToSQLString(rptCurrency)
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

        Function SetExpParameters(ByVal userId As String, ByVal invoiceNumber As String, ByVal payee As String,
                                  ByVal beginDate As String, ByVal endDate As String, ByVal FreeZoneFlag As String, ByVal selectionType As String,
                                  ByVal svccode As String, ByVal taxtype As String, ByVal culturecode As String,
                                  ByVal customerAddress As String, ByVal companyCode As String, ByVal dealerCode As String, ByVal dealerForCur As Guid, ByVal rptCurrency As Guid) As ReportCeBaseForm.Params

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
                .IsFreeZone = FreeZoneFlag
                .reportType = selectionType
                .SvcCode = svccode
                .taxtype = taxtype
                .customerAddress = customerAddress
                .cultureCode = culturecode
                .companyCode = companyCode
                .dealerCode = dealerCode
                .dealerForCur = DALBase.GuidToSQLString(dealerForCur)
                .rptCurrency = DALBase.GuidToSQLString(rptCurrency)
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
            Dim ds As DataSet
            Dim invoiceTrans As New InvoiceTrans
            Dim taxtypeID As Guid
            Dim taxtype As String
            Dim customerAddress As String
            Dim FreeZoneFlag As String = NO
            Dim companyCode As String = String.Empty
            Dim dealerCode As String = "*"
            Dim dealerForCur As Guid = Guid.Empty
            Dim rptCurrency As Guid = Guid.Empty

            ' taxtype code - 4 = Manual...
            taxtypeID = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, "4")
            ds = invoiceTrans.CheckInvoiceTaxType(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID, taxtypeID, Guid.Empty)
            If ds.Tables(0).Rows.Count > 0 Then
                taxtype = "Invoice"
            Else
                taxtype = String.Empty
            End If

            reportFormat = ReportCeBase.GetReportFormat(Me)
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse
                reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                culturecode = TheRptCeInputControl.getCultureValue(True)
            End If

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(RPT_FILENAME_WINDOW)

            If (Me.RadiobuttonByReportingPeriod.Checked) Then
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

            If Me.chkSvcCode.Checked = True Then
                svccode = YES
            Else
                svccode = NO
            End If

            If Me.chkCustomerAddress.Checked = True Then
                customerAddress = YES
            Else
                customerAddress = NO
            End If

            If CheckBoxFreeZone.Checked Then
                FreeZoneFlag = YES
            ElseIf CheckBoxNoFreeZone.Checked Then
                FreeZoneFlag = NO
            Else
                FreeZoneFlag = ALLSVC
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                '   reportName = RPT_FILENAME_EXPORT
                params = SetExpParameters(userId, invoiceNumber, payee, beginDate, endDate, FreeZoneFlag, selectionType, svccode, taxtype, culturecode, customerAddress, _
                                          companyCode, dealerCode, dealerForCur, rptCurrency)
            Else
                'View Report
                params = SetParameters(userId, invoiceNumber, payee, beginDate, endDate, FreeZoneFlag, selectionType, svccode, taxtype, culturecode, customerAddress, _
                                       companyCode, dealerCode, dealerForCur, rptCurrency)
            End If
            'Dim params As ReportCeBaseForm.Params = SetParameters(compCode, invoiceNumber, payee, beginDate, endDate)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub
#End Region

    End Class

End Namespace