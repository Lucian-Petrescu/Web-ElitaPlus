Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports

    Partial Class MasterClaimReportForm
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public dealerCode As String
            Public begindate As String
            Public enddate As String
            Public type As String
            Public langCode As String
            Public culturevalue As String
        End Structure

#End Region

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "MASTER_CLAIM"
        Private Const RPT_FILENAME As String = "MasterClaim"
        Private Const RPT_FILENAME_EXPORT As String = "MasterClaim-Exp"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Public Const PAGETITLE As String = "MASTER_CLAIM"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "MASTER_CLAIM"
        Private Const ONE_ITEM As Integer = 1
        Private Const TOTAL_PARAMS As Integer = 13 ' 12 Elements    ' 10 Elements before without addldac
        Private Const TOTAL_EXP_PARAMS As Integer = 7 ' 6 Elements  '' ' 4 - 5 Elements before without addldac
        Private Const PARAMS_PER_REPORT As Integer = 7 ' 6 Elements '' ' 5 - 5 Elements before without addldac
        Private Const TOTAL_PARAMS_BY_COV As Integer = 14

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "JavaScript"

        Public Sub JavascriptCalls()
            rdealer.Attributes.Add("onclick", "return ToggleDualDropDownsSelection('" + DealerDropControl.CodeDropDown.ClientID + "','" + DealerDropControl.DescDropDown.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")
            'TheReportCeInputControl.RadioButtonPDFControl.Attributes.Add("onclick", "return '" + RadiobuttonTotalsOnly.ClientID + "'.Checked = True")
            'TheReportCeInputControl.RadioButtonVIEWControl.Attributes.Add("onclick", "return '" + RadiobuttonTotalsOnly.ClientID + "'.Checked = True")
            'TheReportCeInputControl.RadioButtonTXTControl.Attributes.Add("onclick", "return '" + RadiobuttonDetail.ClientID + "'.Checked = True")

        End Sub
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

        Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If CompanyDropControl Is Nothing Then
                    CompanyDropControl = CType(FindControl("CompanyDropControl"), MultipleColumnDDLabelControl)
                End If
                Return CompanyDropControl
            End Get
        End Property

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub



        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here


            Me.ErrControllerMaster.Clear_Hide()
            Me.ClearLabelsErrSign()
            Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            If CompanyMultipleDrop.Visible = False Then
                HideHtmlElement(trcomp.ClientID)
            End If
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    JavascriptCalls()
                    Me.AddCalendar(Me.BtnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.moEndDateText)
                    InitializeForm()
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(lblCompany)
                Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateCompaniesDropdown()

            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            CompanyMultipleDrop.NothingSelected = True
            CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.NEW_MODE, True, dv, CompanyMultipleDrop.NO_CAPTION, True)
            'CompanyMultipleDrop.CaptionLabel
            If dv.Count.Equals(ONE_ITEM) Then
                ControlMgr.SetVisibleControl(Me, lblCompany, False)
                HideHtmlElement(trcomp.ClientID)
                CompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                CompanyMultipleDrop.Visible = False
            End If

        End Sub

        Sub PopulateDealerDropDown()

            If Not CompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                Dim dv As DataView = LookupListNew.GetDealerLookupList(CompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")
            Else
                Dim dv As DataView = LookupListNew.GetDealerLookupList(CompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "  " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")
            End If

        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            'RadiobuttonTotalsOnly.Checked = True
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

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
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Report Parameters"

        Private Sub GenerateReport()
            Dim oCompanyId As Guid = CompanyMultipleDrop.SelectedGuid
            Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)
            Dim compDesc As String = LookupListNew.GetDescriptionFromId("COMPANIES", oCompanyId)
            Dim endDate As String
            Dim beginDate As String
            Dim isDetail As String

            Dim LANG_CODE As String = LookupListNew.GetCodeFromId("LANGUAGES", ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid

            Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyId)
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim dealerDesc As String = DealerMultipleDrop.SelectedDesc

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            'COMPANY
            If oCompanyId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(lblCompany)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Validating the Dealer selection
            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If dealerID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerDropControl.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            Dim params As ReportCeBaseForm.Params

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                params = SetExpParameters(compCode, dealerCode, beginDate, endDate, LANG_CODE)
            Else
                params = SetParameters(compCode, dealerCode, beginDate, endDate, LANG_CODE)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetExpParameters(ByVal companyCode As String, ByVal dealerCode As String, _
                              ByVal beginDate As String, _
                              ByVal endDate As String, ByVal LANG_CODE As String) As ReportCeBaseForm.Params


            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(CompanyDropControl.SelectedGuid))
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
            culturecode = TheReportCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(CompanyDropControl.SelectedGuid))

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    { _
                     New ReportCeBaseForm.RptParam("V_COMPANY_CODE", companyCode), _
                     New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode), _
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate), _
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate), _
                     New ReportCeBaseForm.RptParam("V_TYPE", "Dealer"), _
                     New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", LANG_CODE), _
                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturecode)}

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Function SetParameters(ByVal companyCode As String, ByVal dealerCode As String, _
                               ByVal beginDate As String, _
                               ByVal endDate As String, ByVal langCode As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(CompanyDropControl.SelectedGuid))
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            With rptParams
                .companyCode = companyCode
                .dealerCode = dealerCode
                .begindate = beginDate
                .enddate = endDate
                .type = "Dealer"
                .langCode = langCode
                .culturevalue = culturecode

            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            rptParams.type = "SVC"

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam, _
                            ByVal rptName As String, ByVal startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY_CODE", .companyCode, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .begindate, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_END_DATE", .enddate, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_TYPE", .type, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", .langCode, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, rptName)
            End With

        End Sub

#End Region

    End Class
End Namespace