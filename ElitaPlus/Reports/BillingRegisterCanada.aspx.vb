Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports
    Partial Public Class BillingRegisterCanada
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "BILLING_REGISTER_CANADA"
        Private Const RPT_FILENAME As String = "BillingRegisterCanada"
        Private Const RPT_FILENAME_EXPORT As String = "BillingRegisterCanada-Exp"

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
        Private Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"
        Private Const LABEL_SELECT_DEALER_GRP As String = "OR_A_SINGLE_DEALER_GROUP"

        Public Const PAGETITLE As String = "BILLING_REGISTER_CANADA"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "BILLING_REGISTER_CANADA"
        Public Const DEALER_CODE As String = "0"
        Public Const DEALER_NAME As String = "1"

        Private Const TOTAL_PARAMS As Integer = 17
        Private Const TOTAL_EXP_PARAMS As Integer = 9
        Private Const PARAMS_PER_REPORT As Integer = 9


#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public dealerCode As String
            Public dealergrpCode As String
            Public begindate As String
            Public enddate As String
            Public isdealer As Boolean
            Public issummary As String
            Public sortorder As String
            Public culturevalue As String
        End Structure

#End Region

#Region "Properties"

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If DealerDropControl Is Nothing Then
                    DealerDropControl = CType(FindControl("DealerDropControl"), MultipleColumnDDLabelControl)
                End If
                Return DealerDropControl
            End Get
        End Property

        Public ReadOnly Property DealerGrpMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If DealerGrpDropControl Is Nothing Then
                    DealerGrpDropControl = CType(FindControl("DealerGrpDropControl"), MultipleColumnDDLabelControl)
                End If
                Return DealerGrpDropControl
            End Get
        End Property

        Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If CompanyDropControl Is Nothing Then
                    CompanyDropControl = CType(FindControl("CompanyDropControl"), MultipleColumnDDLabelControl)
                End If
                Return CompanyDropControl
            End Get
        End Property

#End Region

#Region "JavaScript"

        'Public Sub JavascriptCalls()
        '    Dim onloadScript As New System.Text.StringBuilder()
        '    onloadScript.Append("<script type='text/javascript'>")
        '    onloadScript.Append(Environment.NewLine)
        '    onloadScript.Append("var arrDealerGrpCtr = '" + "[[" + DealerGrpMultipleDrop.DescDropDown.ClientID + "'],[' " + DealerGrpMultipleDrop.CodeDropDown.ClientID + "' ]]';")
        '    onloadScript.Append("var arrDealerCtr = '" + "[[" + DealerMultipleDrop.DescDropDown.ClientID + "'],[' " + DealerMultipleDrop.CodeDropDown.ClientID + "' ]]';")
        '    onloadScript.Append(Environment.NewLine)
        '    onloadScript.Append("</script>")
        '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "onLoadCall", onloadScript.ToString())

        '    DealerDropControl.CodeDropDown.Attributes.Add("onclick", "return ToggleExt(this, arrDealerGrpCtr);")
        '    DealerDropControl.DescDropDown.Attributes.Add("onclick", "return ToggleExt(this, arrDealerGrpCtr);")
        '    DealerGrpDropControl.CodeDropDown.Attributes.Add("onclick", "return ToggleExt(this, arrDealerCtr);")
        '    DealerGrpDropControl.DescDropDown.Attributes.Add("onclick", "return ToggleExt(this, arrDealerCtr);")
        'End Sub
#End Region

#Region "Web Form Designer Generated Code "
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
            Dim t As Date = Date.Now.AddDays(-1)
            Me.BeginDateText.Text = GetDateFormattedString(t)
            Me.EndDateText.Text = GetDateFormattedString(Date.Now)
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            PopulateDealerGroupDropDown()
            Me.RadiobuttonTotalsOnly.Checked = True
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.ErrControllerMaster.Clear_Hide()
            Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    ' JavascriptCalls()
                    InitializeForm()
                    Me.AddCalendar(Me.BtnBeginDate, Me.BeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.EndDateText)
                Else
                    ClearErrLabels()
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

        Protected Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
     Handles CompanyDropControl.SelectedDropChanged
            Try
                PopulateDealerDropDown()
                PopulateDealerGroupDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(BeginDateLabel)
            Me.ClearLabelErrSign(EndDateLabel)
            Me.ClearLabelErrSign(CompanyMultipleDrop.CaptionLabel)
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            DealerMultipleDrop.NothingSelected = True
            DealerGrpMultipleDrop.NothingSelected = True
        End Sub

#End Region

#Region "Populate"

        Sub PopulateDealerDropDown()

            If Not CompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                Dim dv As DataView = LookupListNew.GetDealerLookupList(CompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, "document.forms[0]." + DealerGrpMultipleDrop.CodeDropDown.ClientID + ".selectedIndex = -1;" +
                                                                                       "document.forms[0]." + DealerGrpMultipleDrop.DescDropDown.ClientID + ".selectedIndex = -1;")
            Else
                Dim dv As DataView = LookupListNew.GetDealerLookupList(CompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, "document.forms[0]." + DealerGrpMultipleDrop.CodeDropDown.ClientID + ".selectedIndex = -1;" +
                                                                           "document.forms[0]." + DealerGrpMultipleDrop.DescDropDown.ClientID + ".selectedIndex = -1;")
            End If

        End Sub
        Sub PopulateDealerGroupDropDown()

            If Not CompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                Dim dv As DataView = LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                DealerGrpMultipleDrop.NothingSelected = True
                DealerGrpMultipleDrop.SetControl(False, DealerGrpMultipleDrop.MODES.NEW_MODE, True, dv, "  " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER_GRP), True, True, "document.forms[0]." + DealerMultipleDrop.CodeDropDown.ClientID + ".selectedIndex = -1;" +
                                                                                       "document.forms[0]." + DealerMultipleDrop.DescDropDown.ClientID + ".selectedIndex = -1;")
            Else
                Dim dv As DataView = LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                DealerGrpMultipleDrop.NothingSelected = True
                DealerGrpMultipleDrop.SetControl(False, DealerGrpMultipleDrop.MODES.NEW_MODE, True, dv, "  " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER_GRP), True, True, "document.forms[0]." + DealerMultipleDrop.CodeDropDown.ClientID + ".selectedIndex = -1;" +
                                                                                       "document.forms[0]." + DealerMultipleDrop.DescDropDown.ClientID + ".selectedIndex = -1;")
            End If

        End Sub
        Private Sub PopulateCompaniesDropdown()

            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            CompanyMultipleDrop.NothingSelected = True
            ControlMgr.SetVisibleControl(Me, pnlComp, True)
            CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            'CompanyMultipleDrop.CaptionLabel
            If dv.Count.Equals(ONE_ITEM) Then
                ControlMgr.SetVisibleControl(Me, pnlComp, False)
                'ControlMgr.SetVisibleControl(Me, trcomp, False)
                'HideHtmlElement(trcomp.ClientID)
                CompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                CompanyMultipleDrop.Visible = False
            End If

        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal companyCode As String, ByVal dealerCode As String, ByVal dealergrpCode As String,
                               ByVal begindate As String, ByVal enddate As String, ByVal isSummary As String,
                               ByVal sortorder As String, ByVal isdealer As Boolean) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim exportData As String = NO
            Dim culturevalue As String
            Dim reportName As String
            Dim rptParams As ReportParams

            culturevalue = TheReportCeInputControl.getCultureValue(False)
            reportName = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            With rptParams
                .companyCode = companyCode
                .dealerCode = dealerCode
                .dealergrpCode = dealergrpCode
                .begindate = begindate
                .enddate = enddate
                .sortorder = sortorder
                .isdealer = isdealer
                .issummary = isSummary
                .culturevalue = culturevalue
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report

            rptParams.issummary = NO

            SetReportParams(rptParams, repParams, "Total", PARAMS_PER_REPORT * 1)  ' Summary (Sub Report)


            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function
        Function SetExpParameters(ByVal companyCode As String, ByVal dealerCode As String, ByVal dealergrpCode As String,
                              ByVal begindate As String, ByVal enddate As String, ByVal isSummary As String,
                              ByVal sortorder As String, ByVal isdealer As Boolean) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim exportData As String = NO
            Dim culturevalue As String
            Dim reportName As String
            Dim rptParams As ReportParams

            culturevalue = TheReportCeInputControl.getCultureValue(True)
            reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            With rptParams
                .companyCode = companyCode
                .dealerCode = dealerCode
                .dealergrpCode = dealergrpCode
                .begindate = begindate
                .enddate = enddate
                .sortorder = sortorder
                .isdealer = isdealer
                .issummary = isSummary
                .culturevalue = culturevalue
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                          ByVal rptName As String, ByVal startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY", .companyCode, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER", .dealerCode, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_DEALERGROUP", .dealergrpCode, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .begindate, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_END_DATE", .enddate, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_ISIDEALER", .isdealer.ToString, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_IS_SUMMARY", .issummary, rptName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("V_SORT_ORDER", .sortorder, rptName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, rptName)
            End With

        End Sub

        Private Sub GenerateReport()
            Dim oCompanyId As Guid = CompanyMultipleDrop.SelectedGuid
            Dim compCode As String = CompanyMultipleDrop.SelectedCode
            Dim compDesc As String = CompanyMultipleDrop.SelectedDesc
            Dim endDate As String
            Dim beginDate As String
            'Dates
            ReportCeBase.ValidateBeginEndDate(BeginDateLabel, BeginDateText.Text, EndDateLabel, EndDateText.Text)
            endDate = ReportCeBase.FormatDate(EndDateLabel, EndDateText.Text)
            beginDate = ReportCeBase.FormatDate(BeginDateLabel, BeginDateText.Text)

            'Validating the Company selection
            If CompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(CompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealercode As String = DealerMultipleDrop.SelectedCode
            Dim selectedDealer As String = DealerMultipleDrop.SelectedCode

            Dim dealergrpID As Guid = DealerGrpMultipleDrop.SelectedGuid
            Dim dealergrpCode As String = DealerGrpMultipleDrop.SelectedCode
            Dim selectedDealerGrp As String = DealerGrpMultipleDrop.SelectedDesc

            Dim detailCode As String
            Dim sortOrder As String
            Dim isdealer As Boolean
            Dim params As ReportCeBaseForm.Params
            ' Dim reportFormat As ReportCeBaseForm.RptFormat

            If Me.RadiobuttonTotalsOnly.Checked Then
                detailCode = NO
            Else
                detailCode = YES
            End If

            Select Case Me.rdReportSortOrder.SelectedValue()
                Case DEALER_CODE
                    sortOrder = "C"
                Case DEALER_NAME
                    sortOrder = "N"
            End Select

            If Not dealerID.Equals(Guid.Empty) Then
                dealercode = DealerMultipleDrop.SelectedCode
                isdealer = True
            ElseIf Not dealergrpID.Equals(Guid.Empty) Then
                isdealer = False
                dealergrpCode = DealerGrpDropControl.SelectedCode
                Dim dealerdrp As New DealerGroup(dealergrpID)
                Dim isalldealerobligor As Boolean = dealerdrp.CheckAllDealerObligor(dealergrpID)
                If isalldealerobligor = False Then
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ALL_DEALER_NOT_SAME_OBLIGOR_STATUS)
                End If
            Else
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                detailCode = YES
                params = SetExpParameters(compCode, dealercode, dealergrpCode, beginDate, endDate, detailCode, sortOrder, isdealer)
            Else
                params = SetParameters(compCode, dealercode, dealergrpCode, beginDate, endDate, detailCode, sortOrder, isdealer)
            End If
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class
End Namespace
