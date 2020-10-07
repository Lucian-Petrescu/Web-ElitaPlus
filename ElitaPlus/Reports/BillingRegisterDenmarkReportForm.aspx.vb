Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports
    Partial Public Class BillingRegisterDenmarkReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "BILLING_REGISTER_DENMARK"
        Private Const RPT_FILENAME As String = "BillingRegisterDenmark"
        Private Const RPT_FILENAME_EXPORT As String = "BillingRegisterDenmark-Exp"
        Private Const BY_REPORTING_PERIOD As String = "P"
        Private Const BY_INVOICE_NUMBER As String = "I"
        Private Const BY_CREDIT_NOTE_NUMBER As String = "C"
        Private Const TOTAL_PARAMS As Integer = 19 ' 18 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 10 ' 6 Elements
        Private Const PARAMS_PER_REPORT As Integer = 10 ' 6 Elements

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
        Private Const LABEL_OR_A_SINGLE_DEALER As String = "OR A SINGLE DEALER"
        Private Const MaxLength As Integer = 8
        Private Const ONE_ITEM As Integer = 1

        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
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

        Public ReadOnly Property TheMultipleDropControl() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("moUserDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property
#End Region

#Region "parameters"
        Public Structure ReportParams
            Public CompanyId As String
            Public dealercode As String
            Public invoiceNumber As String
            Public creditnotenumber As String
            Public beginDate As String
            Public endDate As String
            Public isSummary As String
            Public selectionType As String
            Public dealerlevel As String
            Public culturevalue As String

        End Structure
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
            Dim t As Date = Date.Now.AddDays(-1)
            moBeginDateText.Text = GetDateFormattedString(t)
            moEndDateText.Text = GetDateFormattedString(Date.Now)
            PopulateCompaniesDropdown()
            PopulateDealer()
            rdealer.Checked = True
            rdealerlevel.Checked = True
            RadiobuttonTotalsOnly.Checked = True
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                    'Date Calendars
                    AddCalendar(BtnBeginDate, moBeginDateText)
                    AddCalendar(BtnEndDate, moEndDateText)
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

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealer()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moBeginDateLabel)
            ClearLabelErrSign(moEndDateLabel)
            ClearLabelErrSign(InvoiceNumberLabel)
            ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                UserCompanyMultipleDrop.Visible = False

            End If
        End Sub

        Private Sub PopulateDealer()

            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            TheMultipleDropControl.NothingSelected = True
            TheMultipleDropControl.SetControl(False,
                                              TheMultipleDropControl.MODES.NEW_MODE,
                                              True,
                                              dv,
                                              TranslationBase.TranslateLabelOrMessage(LABEL_OR_A_SINGLE_DEALER),
                                              True,
                                              False,
                                              " document.forms[0].rdealer.checked = false;  document.forms[0].InvoiceNumberTextbox.value = ''; document.forms[0].CreditNoteTextbox.value = ''; ")
            '"moUserDealerMultipleDrop_moMultipleColumnDrop", _
            '"moUserDealerMultipleDrop_moMultipleColumnDropDesc", "moUserDealerMultipleDrop_lb_DropDown")


        End Sub
#End Region

#Region "Crystal Enterprise"


        Function SetParameters(CompanyId As String, dealercode As String, invoiceNumber As String, creditnotenumber As String,
                                beginDate As String, endDate As String, selectionType As String, detailCode As String, dealerlevel As String) As ReportCeBaseForm.Params

            Dim exportData As String = NO
            Dim culturevalue As String
            Dim reportName As String
            Dim moReportFormat As ReportCeBaseForm.RptFormat

            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams

            culturevalue = TheRptCeInputControl.getCultureValue(False, CompanyId)
            reportName = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            With rptParams
                .CompanyId = CompanyId
                .dealercode = dealercode
                .invoiceNumber = invoiceNumber
                .creditnotenumber = creditnotenumber
                .beginDate = beginDate
                .endDate = endDate
                .isSummary = detailCode
                .dealerlevel = dealerlevel
                .selectionType = selectionType
                .culturevalue = culturevalue
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            rptParams.isSummary = "N"

            SetReportParams(rptParams, repParams, "ToTals", PARAMS_PER_REPORT * 1)     ' Main Report

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return Params
        End Function


        Function SetExpParameters(CompanyId As String, dealercode As String, invoiceNumber As String, creditnotenumber As String,
                                beginDate As String, endDate As String, selectionType As String, detailCode As String, dealerlevel As String) As ReportCeBaseForm.Params


            Dim exportData As String = NO
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(True)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)

            With rptParams
                .CompanyId = CompanyId
                .dealercode = dealercode
                .invoiceNumber = invoiceNumber
                .creditnotenumber = creditnotenumber
                .beginDate = beginDate
                .endDate = endDate
                .isSummary = "Y"
                .dealerlevel = dealerlevel
                .selectionType = selectionType
                .culturevalue = TheRptCeInputControl.getCultureValue(True, CompanyId)
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return Params
        End Function

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam,
                         reportName As String, startIndex As Integer)
            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANYID", .CompanyId, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER", .dealercode, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_INVOICE_NUMBER", .invoiceNumber, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_CREDIT_NOTE_NUMBER", .creditnotenumber, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .beginDate, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_END_DATE", .endDate, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_IS_SUMMARY", .isSummary, reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("V_IS_DEALERLEVEL", .dealerlevel, reportName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("V_REPORT_TYPE", .selectionType, reportName)
                repParams(startIndex + 9) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, reportName)
            End With
        End Sub


        Private Sub GenerateReport()
            Dim UserId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim selectionType As String

            Dim endDate As String
            Dim beginDate As String
            Dim invoicenumber As String
            Dim creditnotenumber As String
            Dim detailCode As String
            Dim CompanyId As Guid = UserCompanyMultipleDrop.SelectedGuid
            Dim dealerlevel As String

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Validating date selection
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            Dim dealerID As Guid = TheMultipleDropControl.SelectedGuid
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            Dim dealerCode As String '= LookupListNew.GetCodeFromId(dv, dealerID)
            ' Dim dealerDesc As String = LookupListNew.GetDescriptionFromId(dv, dealerID)


            If rdealer.Checked Then
                dealerCode = ALL
                selectionType = BY_REPORTING_PERIOD
            ElseIf Not dealerID.Equals(Guid.Empty) Then
                dealerCode = LookupListNew.GetCodeFromId(dv, dealerID)
                selectionType = BY_REPORTING_PERIOD
            ElseIf Not ((InvoiceNumberTextbox.Text.Trim.ToString) Is String.Empty) Then
                invoicenumber = InvoiceNumberTextbox.Text
                selectionType = BY_INVOICE_NUMBER
                If invoicenumber.Length <> MaxLength Then
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_INVOICE_NUMBER)
                End If
            ElseIf Not ((CreditNoteTextbox.Text.Trim.ToString) Is String.Empty) Then
                creditnotenumber = CreditNoteTextbox.Text
                selectionType = BY_CREDIT_NOTE_NUMBER
                If creditnotenumber.Length <> MaxLength Then
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_CREDIT_NOTE_NUMBER)
                End If
            Else
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            If rdealerlevel.Checked Then
                dealerlevel = YES
            Else
                dealerlevel = NO
            End If

            If RadiobuttonTotalsOnly.Checked Then
                detailCode = NO
            Else
                detailCode = YES
            End If


            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report

                params = SetExpParameters(GuidControl.GuidToHexString(CompanyId), dealerCode, invoicenumber, creditnotenumber, beginDate, endDate, selectionType, detailCode, dealerlevel)
            Else
                params = SetParameters(GuidControl.GuidToHexString(CompanyId), dealerCode, invoicenumber, creditnotenumber, beginDate, endDate, selectionType, detailCode, dealerlevel)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub
#End Region

    End Class

End Namespace
