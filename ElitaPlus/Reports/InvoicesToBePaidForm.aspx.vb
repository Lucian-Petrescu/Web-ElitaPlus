Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports
    Partial Class InvoicesToBePaidForm
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
            Public taxtype As String
            Public customerAddress As String
            Public cultureCode As String
            Public companyCode As String
            Public dealerCode As String
            Public dealerForCur As String
            Public rptCurrency As String
        End Structure

        Public Structure ReportExpParams
            Public userId As String
            Public payee As String
            Public invoiceNumber As String
            Public beginDate As String
            Public endDate As String
            Public reportType As String
            Public SvcCode As String
            Public taxtype As String
            Public customerAddress As String
            Public cultureCode As String
            Public companyCode As String
            Public dealerCode As String
            Public dealerForCur As String
            Public rptCurrency As String
            Public infotype As String
        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Invoices To Be Paid"
        Private Const RPT_FILENAME As String = "InvoicesToBePaid"
        Private Const RPT_FILENAME_EXPORT As String = "InvoicesToBePaid-Exp"
        Private Const BY_REPORTING_PERIOD As String = "P"
        Private Const BY_INVOICE_NUMBER As String = "I"
        Private Const PERIOD_ROW As String = "period"
        Private Const INVOICE_ROW As String = "invoice"
        Private Const PAYEE_ROW As String = "payee"
        Private Const TOTAL_EXP_PARAMS As Integer = 14 ' 6 Elements
        Private Const PARAMS_PER_REPORT As Integer = 15 ' 6 Elements
        Private Const PARAMS_PER_EXT_REPORT As Integer = 16 ' 6 Elements
        Private Const CLAIMS_INFORMATION As String = "CLAIMS"
        Private Const INVOICE_INFORMATION As String = "INVOICES"

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

        Public Const PAGETAB As String = "REPORTS"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Public Const PAGETITLE As String = "INVOICES TO BE PAID"
        Public Const PAGETITLEWITHCURRENCY As String = "INVOICES TO BE PAID WITH CURRENCY"
        Public Const ONE_ITEM As Integer = 1
#End Region

#Region "variables"
        Private showPayeeRowFlag As Boolean
        Private invoiceNumber As String
        Private payee As String
        Private queryStringCaller As String = String.Empty
#End Region


#Region "Properties"
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleDealerDropControl Is Nothing Then
                    multipleDealerDropControl = CType(FindControl("multipleDealerDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDealerDropControl
            End Get
        End Property
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleCompDropControl Is Nothing Then
                    multipleCompDropControl = CType(FindControl("multipleCompDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleCompDropControl
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
            RadiobuttonClaims.Checked = True
            chkSvcCode.Checked = False
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrControllerMaster.Clear_Hide()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not IsPostBack Then
                    JavascriptCalls()
                    SetFormTab(PAGETAB)
                    InitializeForm()
                    TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
                    'Date Calendars
                    AddCalendar(BtnBeginDate, BeginDateText)
                    AddCalendar(BtnEndDate, EndDateText)
                Else
                    ClearErrLabels()
                End If
                CheckQuerystringForCurrencyReports()
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)

        End Sub

        Private Sub CheckQuerystringForCurrencyReports()
            ShowHideControls(False)
            SetFormTitle(PAGETITLE)

            If (Request.QueryString("CALLER") IsNot Nothing) Then
                If (Request.QueryString("CALLER") = "CR") Then
                    queryStringCaller = Request.QueryString("CALLER")
                    SetFormTitle(PAGETITLEWITHCURRENCY)
                    ShowHideControls(True)
                End If
            End If
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

#Region "Handlers-DropDown"

        Protected Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
             Handles multipleCompDropControl.SelectedDropChanged
            Try
                rbnSelectAllComp.Checked = False
                PopulateDealerDropDown()
                PopulateCurrencyDropdown()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub OnFromDealerDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles multipleDealerDropControl.SelectedDropChanged
            Try
                If multipleDealerDropControl.SelectedCode <> "" Or multipleDealerDropControl.SelectedDesc <> "" Then
                    rbnSelectAllComp.Checked = False
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(BeginDateLabel)
            ClearLabelErrSign(EndDateLabel)
            ClearLabelErrSign(InvoiceNumberLabel)
            ClearLabelErrSign(lblCurrency)
        End Sub

#End Region

#Region "JavaScript"

        Public Sub JavascriptCalls()
            rdealer.Attributes.Add("onclick", "return TogglelDropDownsSelectionsForCurrencyReports('" + multipleDealerDropControl.CodeDropDown.ClientID + "','" + multipleDealerDropControl.DescDropDown.ClientID + "','" + ddlDealerCurrency.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")
        End Sub
#End Region

#Region "Populate"
        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, UserCompanyMultipleDrop.NO_CAPTION, True, False)
            If dv.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                OnFromDrop_Changed(UserCompanyMultipleDrop)
            End If
        End Sub

        'ALR Need to create new method to pull full list based on multiple company membership
        Private Sub PopulateDealerDropDown()
            If Not UserCompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                Dim dv As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;document.forms[0]." + ddlDealerCurrency.ClientID + ".selectedIndex = -1;")
            Else
                Dim dv As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "  " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;document.forms[0]." + ddlDealerCurrency.ClientID + ".selectedIndex = -1;")
            End If
        End Sub

        Private Sub PopulateCurrencyDropdown()

            Dim populateOptions = New PopulateOptions() With
                                {
                                   .AddBlankItem = True,
                                   .TextFunc = Function(x)
                                                   Return x.Code + " (" + x.Translation + ")"
                                               End Function,
                                   .ValueFunc = AddressOf .GetListItemId,
                                   .SortFunc = Function(x)
                                                   Return x.Code + " (" + x.Translation + ")"
                                               End Function
                                }

            If Not UserCompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                Dim Currency As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="GetCurrencyByCompany",
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyId = UserCompanyMultipleDrop.SelectedGuid
                                                                })

                ddlCurrency.Populate(Currency.ToArray(), populateOptions)
                ddlDealerCurrency.Populate(Currency.ToArray(), populateOptions)
            Else
                Dim Currency As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="GetCurrencyByCompany",
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyId = UserCompanyMultipleDrop.SelectedGuid
                                                                })

                ddlCurrency.Populate(Currency.ToArray(), populateOptions)
                ddlDealerCurrency.Populate(Currency.ToArray(), populateOptions)
            End If
        End Sub

        Sub BindPayee(invoiceNumber As String)
            Try
                If invoiceNumber IsNot Nothing AndAlso invoiceNumber.Trim.Length > 0 Then
                    PayeeLabel.Visible = True
                    cboPayee.Visible = True
                    Dim PayeeList As New Collections.Generic.List(Of DataElements.ListItem)
                    For Each Company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                        Dim Payee As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PayeeListByInvoiceNumberAndCompany",
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
                    If (PayeeList.Count = 0) Then
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
                HandleErrors(ex, ErrControllerMaster)
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
                repParams(startIndex) = New ReportCeBaseForm.RptParam("PI_USER_KEY", .userId, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("PI_REPORT_TYPE", .reportType, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("PI_BEGIN_DATE", .beginDate, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("PI_END_DATE", .endDate, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("PI_PAYEE", .payee, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("PI_SVC_CONTROL_NUMBER", .invoiceNumber, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("PI_FREE_ZONE_FLAG", "ALL", rptName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("PI_INCLUDE_SVCCODE", .SvcCode, rptName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("PI_INCLUDE_CUSTOMER_ADDR", .customerAddress, rptName)
                repParams(startIndex + 9) = New ReportCeBaseForm.RptParam("PI_COMPANY_CODE", .companyCode, rptName)
                repParams(startIndex + 10) = New ReportCeBaseForm.RptParam("PI_DEALER_CODE", .dealerCode, rptName)
                repParams(startIndex + 11) = New ReportCeBaseForm.RptParam("PI_DEALER_WITH_CUR", .dealerForCur, rptName)
                repParams(startIndex + 12) = New ReportCeBaseForm.RptParam("PI_RPT_CUR", .rptCurrency, rptName)
                repParams(startIndex + 13) = New ReportCeBaseForm.RptParam("PI_TAX_TYPE", .taxtype, rptName)
                repParams(startIndex + 14) = New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", .cultureCode, rptName)
            End With

        End Sub

        Sub SetReportExpParams(rptParams As ReportExpParams, repParams() As ReportCeBaseForm.RptParam,
                            rptName As String, startIndex As Integer)
            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("PI_USER_KEY", .userId, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("PI_REPORT_TYPE", .reportType, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("PI_BEGIN_DATE", .beginDate, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("PI_END_DATE", .endDate, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("PI_PAYEE", .payee, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("PI_SVC_CONTROL_NUMBER", .invoiceNumber, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("PI_FREE_ZONE_FLAG", "ALL", rptName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("PI_INCLUDE_SVCCODE", .SvcCode, rptName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("PI_INCLUDE_CUSTOMER_ADDR", .customerAddress, rptName)
                repParams(startIndex + 9) = New ReportCeBaseForm.RptParam("PI_COMPANY_CODE", .companyCode, rptName)
                repParams(startIndex + 10) = New ReportCeBaseForm.RptParam("PI_DEALER_CODE", .dealerCode, rptName)
                repParams(startIndex + 11) = New ReportCeBaseForm.RptParam("PI_DEALER_WITH_CUR", .dealerForCur, rptName)
                repParams(startIndex + 12) = New ReportCeBaseForm.RptParam("PI_RPT_CUR", .rptCurrency, rptName)
                repParams(startIndex + 13) = New ReportCeBaseForm.RptParam("PI_TAX_TYPE", .taxtype, rptName)
                repParams(startIndex + 14) = New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", .cultureCode, rptName)
                repParams(startIndex + 15) = New ReportCeBaseForm.RptParam("PI_INFO_TYPE", .infotype, rptName)
            End With

        End Sub

        Function SetParameters(userId As String, invoiceNumber As String, payee As String,
                               beginDate As String, endDate As String, selectionType As String,
                               svccode As String, taxtype As String, culturecode As String,
                               customerAddress As String, companyCode As String, dealerCode As String, dealerForCur As Guid, rptCurrency As Guid) As ReportCeBaseForm.Params

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
                .payee = payee
                .invoiceNumber = invoiceNumber
                .beginDate = beginDate
                .endDate = endDate
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

        Function SetExpParameters(userId As String, invoiceNumber As String, payee As String,
                                  beginDate As String, endDate As String, selectionType As String,
                                  svccode As String, taxtype As String, culturecode As String,
                                  customerAddress As String, companyCode As String, dealerCode As String, dealerForCur As Guid,
                                  rptCurrency As Guid, infoType As String) As ReportCeBaseForm.Params

            Dim reportName As String
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(PARAMS_PER_EXT_REPORT) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportExpParams
            Dim reportFormat As ReportCeBaseForm.RptFormat

            reportFormat = ReportCeBase.GetReportFormat(Me)
            reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            With rptParams
                .userId = userId
                .payee = payee
                .invoiceNumber = invoiceNumber
                .beginDate = beginDate
                .endDate = endDate
                .reportType = selectionType
                .SvcCode = svccode
                .taxtype = taxtype
                .customerAddress = customerAddress
                .cultureCode = culturecode
                .companyCode = companyCode
                .dealerCode = dealerCode
                .dealerForCur = DALBase.GuidToSQLString(dealerForCur)
                .rptCurrency = DALBase.GuidToSQLString(rptCurrency)
                .infotype = infoType
            End With

            SetReportExpParams(rptParams, repParams, String.Empty, PARAMS_PER_EXT_REPORT * 0)     ' Main Report

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
            Dim infoType As String
            Dim params As ReportCeBaseForm.Params
            Dim endDate As String
            Dim beginDate As String
            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False)
            Dim svccode As String
            Dim ds As DataSet
            Dim invoiceTrans As New InvoiceTrans
            Dim taxtypeID As Guid
            Dim taxtype As String
            Dim customerAddress As String
            Dim companyCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
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


            'Get the company code
            companyCode = UserCompanyMultipleDrop.SelectedCode
            'If currency report is selected, either of Select All Dealers or a particular dealer or only dealers with option AND Currency should be selected 
            'If regular report is selected then either Select All Dealers or a particular dealer should be selected
            If (queryStringCaller = "CR") Then

                Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid

                'if Select All Companies is checked then comany and dealer are passed null
                If Not rbnSelectAllComp.Checked Then

                    'either of the three options should be selected
                    If (rdealer.Checked = False And selectedDealerId.Equals(Guid.Empty) And ddlDealerCurrency.SelectedIndex = 0) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_REQUIRED)
                    End If

                    If ddlDealerCurrency.SelectedIndex > 0 Then
                        dealerForCur = New Guid(ddlDealerCurrency.SelectedValue)
                    End If
                End If

                'currency should be selected for every run
                If ddlCurrency.SelectedIndex <= 0 Or ddlCurrency.SelectedItem Is Nothing Then
                    ElitaPlusPage.SetLabelError(lblCurrency)
                    Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CURRENCY_MUST_BE_SELECTED_ERR)
                Else
                    rptCurrency = New Guid(ddlCurrency.SelectedValue)
                End If

            End If

            reportFormat = ReportCeBase.GetReportFormat(Me)
            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse
                reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                culturecode = TheReportCeInputControl.getCultureValue(True)
            End If

            If (RadiobuttonByReportingPeriod.Checked) Then
                selectionType = BY_REPORTING_PERIOD
            Else
                selectionType = BY_INVOICE_NUMBER
            End If

            If (RadiobuttonClaims.Checked) Then
                infoType = CLAIMS_INFORMATION
            Else
                infoType = INVOICE_INFORMATION
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

            If chkCustomerAddress.Checked = True Then
                customerAddress = YES
            Else
                customerAddress = NO
            End If

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                '   reportName = RPT_FILENAME_EXPORT
                params = SetExpParameters(userId, invoiceNumber, payee, beginDate, endDate, selectionType, svccode, taxtype, culturecode, customerAddress,
                                          companyCode, dealerCode, dealerForCur, rptCurrency, infoType)
            Else
                'View Report
                params = SetParameters(userId, invoiceNumber, payee, beginDate, endDate, selectionType, svccode, taxtype, culturecode, customerAddress,
                                       companyCode, dealerCode, dealerForCur, rptCurrency)
            End If
            'Dim params As ReportCeBaseForm.Params = SetParameters(compCode, invoiceNumber, payee, beginDate, endDate)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub
#End Region

#Region "visibility control logic"
        Private Sub ShowHideControls(show As Boolean)
            If (show) Then
                trSelectAllComp.Style.Add("display", "block")
                trcomp.Style.Add("display", "block")
                trHrAfterCompanyRow.Style.Add("display", "block")
                trSelectAllDealers.Style.Add("display", "block")
                trHrAfterSelectAllDealersRow.Style.Add("display", "block")
                trOnlyDealersWith.Style.Add("display", "block")
                trHrAfterOnlyDealersWithRow.Style.Add("display", "block")
                trCurrency.Style.Add("display", "block")
                trHrAfterCurrencyRow.Style.Add("display", "block")

            Else
                trSelectAllComp.Style.Add("display", "none")
                trcomp.Style.Add("display", "none")
                trHrAfterCompanyRow.Style.Add("display", "none")
                trSelectAllDealers.Style.Add("display", "none")
                trHrAfterSelectAllDealersRow.Style.Add("display", "none")
                trOnlyDealersWith.Style.Add("display", "none")
                trHrAfterOnlyDealersWithRow.Style.Add("display", "none")
                trCurrency.Style.Add("display", "none")
                trHrAfterCurrencyRow.Style.Add("display", "none")
            End If
        End Sub
#End Region

        Private Sub rbnSelectAllComp_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbnSelectAllComp.CheckedChanged
            If rbnSelectAllComp.Checked Then
                'remove the company selection
                UserCompanyMultipleDrop.SelectedIndex = -1
                UserCompanyMultipleDrop.SelectedGuid = Guid.Empty
                'clear all dealers
                PopulateDealerDropDown()
                rdealer.Checked = True
                ddlDealerCurrency.Items.Clear()
            End If
        End Sub
    End Class

End Namespace

