Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Partial Class ClaimsPaidReportForm
        Inherits ElitaPlusPage
#Region "Parameters"

        Public Structure ReportParams
            Public userId As String
            Public payee As String
            Public invoiceNumber As String
            Public beginDate As String
            Public endDate As String
            Public reportType As String
            Public cultureCode As String
        End Structure
#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "CLAIMS_PAID"
        Private Const RPT_FILENAME As String = "ClaimPaid"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimPaid-Exp"
        Private Const BY_REPORTING_PERIOD As String = "P"
        Private Const BY_INVOICE_NUMBER As String = "I"
        Private Const TOTAL_EXP_PARAMS As Integer = 7 ' 6 Elements
        Private Const PARAMS_PER_REPORT As Integer = 7 ' 6 Elements

        Public Const CRYSTAL As String = "0"
        Public Const HTML As String = "2"
        Public Const CSV As String = "3"

        Public Const VIEW_REPORT As String = "1"
        Public Const PDF As String = "2"
        Public Const TAB As String = "3"
        Public Const EXCEL As String = "4"
        Public Const EXCEL_DATA_ONLY As String = "5"
        Public Const JAVA As String = "6"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1

#End Region

#Region "variables"
        Private invoiceNumber As String
        Private payee As String
        Private showPayeeRowFlag As Boolean
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

#Region "Handlers-Pages"

        Private Sub InitializeForm()
            Dim t As Date = Date.Now.AddDays(-6)
            BeginDateText.Text = GetDateFormattedString(t)
            EndDateText.Text = GetDateFormattedString(Date.Now)
            RadiobuttonByReportingPeriod.Checked = True
            RadiobuttonByInvoiceNumber.Checked = False
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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

        Private Sub ClaimsPaidReportForm_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
            Dim messageContent As String
            If RadiobuttonByInvoiceNumber.Checked Then
                messageContent = ("<script language=JavaScript> toggleOptionSelection('I') </script>")
            Else
                messageContent = ("<script language=JavaScript> toggleOptionSelection('D') </script>")
            End If
            Page.RegisterStartupScript("ShowReportByCtls", messageContent)
        End Sub

#End Region

#Region "Events Handlers"
        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
#End Region

#Region "Helper functions"
        Sub BindPayee(invoiceNumber As String)
            Try
                If invoiceNumber IsNot Nothing AndAlso invoiceNumber.Trim.Length > 0 Then
                    PayeeLabel.Visible = True
                    cboPayee.Visible = True

                    Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                    Dim payeeList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
                    For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                        oListContext.CompanyId = _company
                        oListContext.InvoiceNumber = invoiceNumber
                        Dim oPayeeListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="PayeeListByInvoiceNumberAndCompany", context:=oListContext)
                        If oPayeeListForCompany.Count > 0 Then
                            If payeeList IsNot Nothing Then
                                payeeList.AddRange(oPayeeListForCompany)
                            Else
                                payeeList = oPayeeListForCompany.Clone()
                            End If
                        End If
                    Next

                    cboPayee.Populate(payeeList.ToArray(), New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = False
                                                   })

                    If (payeeList.Count = 0) Then
                        showPayeeRowFlag = True
                        Throw New GUIException(Message.MSG_NO_PAYEE_FOUND, Assurant.ElitaPlus.Common.ErrorCodes.GUI_NO_PAYEE_FOUND_ERR)
                    End If

                    'Dim companyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
                    'Dim dv As Disbursement.DisbursementSearchDV = Disbursement.GetList(invoiceNumber, companyList)
                    'Me.BindListControlToDataView(Me.cboPayee, dv, "payee", "disbursement_id", False)

                    'If (dv.Count = 0) Then
                    '    showPayeeRowFlag = True
                    '    Throw New GUIException(Message.MSG_NO_PAYEE_FOUND, Assurant.ElitaPlus.Common.ErrorCodes.GUI_NO_PAYEE_FOUND_ERR)
                    'End If

                    'Set flag to true
                    showPayeeRowFlag = True
                Else
                    'set flag false
                    showPayeeRowFlag = False
                    cboPayee.SelectedIndex = 0
                    'HidePayeeRow()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub InvoiceNumberTextbox_TextChanged(sender As Object, e As System.EventArgs) Handles InvoiceNumberTextbox.TextChanged
            BindPayee(InvoiceNumberTextbox.Text)
        End Sub


        Private Sub ClearErrLabels()
            ClearLabelErrSign(BeginDateLabel)
            ClearLabelErrSign(EndDateLabel)
            ClearLabelErrSign(InvoiceNumberLabel)
            ClearLabelErrSign(PayeeLabel)
        End Sub

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
                'repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", .cultureCode, rptName)
            End With

        End Sub

        Function SetParameters(userId As String, invoiceNumber As String, payee As String,
                               beginDate As String, endDate As String, selectionType As String,
                               culturecode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)
            reportFormat = ReportCeBase.GetReportFormat(Me)

            With rptParams
                .userId = userId
                .payee = payee
                .invoiceNumber = invoiceNumber
                .beginDate = beginDate
                .endDate = endDate
                .reportType = selectionType
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

        Function SetExpParameters(userId As String, invoiceNumber As String, payee As String,
                                  beginDate As String, endDate As String, selectionType As String,
                                  culturecode As String) As ReportCeBaseForm.Params

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


            reportFormat = ReportCeBase.GetReportFormat(Me)
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse
                reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                culturecode = TheRptCeInputControl.getCultureValue(True)
            End If

            rptWindowTitle.Text = TheRptCeInputControl.getReportWindowTitle(RPT_FILENAME_WINDOW)

            If (RadiobuttonByReportingPeriod.Checked) Then
                selectionType = BY_REPORTING_PERIOD
            Else
                selectionType = BY_INVOICE_NUMBER
            End If

            If (selectionType = BY_REPORTING_PERIOD) Then
                invoiceNumber = "*"
                payee = "*"

                'Dates
                ReportCeBase.ValidateBeginEndDate(BeginDateLabel, BeginDateText.Text, EndDateLabel, EndDateText.Text)
                endDate = ReportCeBase.FormatDate(EndDateLabel, EndDateText.Text)
                beginDate = ReportCeBase.FormatDate(BeginDateLabel, BeginDateText.Text)
            Else
                beginDate = Nothing
                endDate = Nothing
                invoiceNumber = InvoiceNumberTextbox.Text.Trim
                If cboPayee.Items.Count > 0 Then
                    payee = cboPayee.SelectedItem.Text.Trim
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

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                params = SetExpParameters(userId, invoiceNumber, payee, beginDate, endDate, selectionType, culturecode)
            Else
                'View Report
                params = SetParameters(userId, invoiceNumber, payee, beginDate, endDate, selectionType, culturecode)
            End If
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub
#End Region


    End Class
End Namespace
