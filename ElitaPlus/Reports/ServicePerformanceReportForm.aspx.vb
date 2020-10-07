Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Partial Class ServicePerformanceReportForm
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String

        End Structure

#End Region

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "SERVICE_PERFORMANCE"
        Private Const RPT_FILENAME As String = "ServicePerformance"
        Private Const RPT_FILENAME_EXPORT As String = "ServicePerformance-Exp"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Public Const PAGETITLE As String = "SERVICE_PERFORMANCE"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "SERVICE_PERFORMANCE"
        Private Const ONE_ITEM As Integer = 1

        Private Const LABEL_SELECT_SERVICE_CENTER As String = "OR_ONLY_SERVICE_CENTER"

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "JavaScript"

        Public Sub JavascriptCalls()
            rdealer.Attributes.Add("onclick", "return ToggleDualDropDownsSelection('" + DealerDropControl.CodeDropDown.ClientID + "','" + DealerDropControl.DescDropDown.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")
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

        Public ReadOnly Property SVCMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If SVCDropControl Is Nothing Then
                    SVCDropControl = CType(FindControl("SVCDropControl"), MultipleColumnDDLabelControl)
                End If
                Return SVCDropControl
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub



        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here


            ErrControllerMaster.Clear_Hide()
            ClearLabelsErrSign()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            If CompanyMultipleDrop.Visible = False Then
                HideHtmlElement(trcomp.ClientID)
            End If
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    JavascriptCalls()
                    AddCalendar(BtnBeginDate, moBeginDateText)
                    AddCalendar(BtnEndDate, moEndDateText)
                    InitializeForm()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(lblCompany)
                ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
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
                CompanyMultipleDrop.SelectedIndex = ONE_ITEM
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

        Sub PopulateSVCDropDown()
            Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)
            SVCMultipleDrop.NothingSelected = True
            SVCMultipleDrop.SetControl(False, SVCMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_SERVICE_CENTER), True, True,
                                       "document.forms[0]." + rAllSVC.ClientID + ".checked = false; document.forms[0]." + moAllExtStatus.ClientID + ".checked = false;" +
                                       "document.forms[0]." + moSingleExtStatus.ClientID + ".checked = false;" +
                                       "if (document.forms[0]." + rAllSvcActivity.ClientID + ".checked == false && " +
                                           "document.forms[0]." + rInProcess.ClientID + ".checked == false && " +
                                           "document.forms[0]." + rRepaired.ClientID + ".checked == false && " +
                                           "document.forms[0]." + rCompleted.ClientID + ".checked == false)" +
                                       "{document.forms[0]." + rAllSvcActivity.ClientID + ".checked = true;}")
        End Sub
        Sub PopulateExtStatusDropDown()
            'Me.BindListControlToDataView(moSingleExtStatus, LookupListNew.GetExtendedStatusLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim extendedLKl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ExtendedStatusByCompanyGroup, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            moSingleExtStatus.Populate(extendedLKl, New PopulateOptions() With
             {
               .AddBlankItem = True
                })
        End Sub


        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            PopulateSVCDropDown()
            PopulateExtStatusDropDown()
            RadiobuttonTotalsOnly.Checked = True
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            moBeginDateText.Text = GetDateFormattedString(t)
            moEndDateText.Text = GetDateFormattedString(Date.Now)
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

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
             Handles CompanyDropControl.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

        Private Sub GenerateReport()
            Dim oCompanyId As Guid = CompanyMultipleDrop.SelectedGuid
            Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)
            Dim compDesc As String = LookupListNew.GetDescriptionFromId("COMPANIES", oCompanyId)
            Dim endDate As String
            Dim beginDate As String
            Dim isDetail As String
            Dim svcCode As String
            Dim svcDesc As String
            Dim selection_type As String
            Dim Exists_status_desc As String
            Dim Exists_status_Code As String
            Dim NOT_Exists_status_Code As String
            Dim LANG_CODE As String = LookupListNew.GetCodeFromId("LANGUAGES", ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid

            Dim svcID As Guid = SVCDropControl.SelectedGuid

            '''Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyId)
            '''Dim dealerCode As String = LookupListNew.GetCodeFromId(dv, dealerID)
            '''Dim selectedDealer As String = LookupListNew.GetDescriptionFromId(dv, dealerID)

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
            If rdealer.Checked Then
                dealerCode = ALL
            Else
                If dealerID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerDropControl.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If rAllSVC.Checked = True Then
                svcCode = ALL
                selection_type = "SVC"
                If rAllSvcActivity.Checked = True Then
                    Exists_status_Code = ALL
                    NOT_Exists_status_Code = Nothing
                ElseIf rInProcess.Checked = True Then
                    Exists_status_Code = "IP"
                    NOT_Exists_status_Code = "REPRD"
                ElseIf rRepaired.Checked = True Then
                    Exists_status_Code = "REPRD"
                    NOT_Exists_status_Code = "RBS"
                ElseIf rCompleted.Checked = True Then
                    Exists_status_Code = "RBS"
                    NOT_Exists_status_Code = Nothing
                End If
            ElseIf Not svcID = Guid.Empty Then
                Dim osvc As New ServiceCenter(svcID)
                svcCode = osvc.Code
                svcDesc = osvc.Description
                selection_type = "SVC"
                If rAllSvcActivity.Checked = True Then
                    Exists_status_Code = ALL
                    NOT_Exists_status_Code = Nothing
                ElseIf rInProcess.Checked = True Then
                    Exists_status_Code = "IP"
                    NOT_Exists_status_Code = "REPRD"
                ElseIf rRepaired.Checked = True Then
                    Exists_status_Code = "REPRD"
                    NOT_Exists_status_Code = "RBS"
                ElseIf rCompleted.Checked = True Then
                    Exists_status_Code = "RBS"
                    NOT_Exists_status_Code = Nothing
                End If
            ElseIf moAllExtStatus.Checked = True Then
                selection_type = "STATUS"
                Exists_status_desc = ALL
                Exists_status_Code = Nothing
                NOT_Exists_status_Code = Nothing
            Else
                selection_type = "STATUS"
                Exists_status_desc = moSingleExtStatus.SelectedItem.Text
                Exists_status_Code = Nothing
                NOT_Exists_status_Code = Nothing
            End If

            If RadiobuttonTotalsOnly.Checked Then
                isDetail = NO
            Else
                isDetail = YES
            End If

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, compDesc, dealerCode, dealerDesc, _
                    svcCode, svcDesc, beginDate, endDate, selection_type, Exists_status_Code, NOT_Exists_status_Code, Exists_status_desc, LANG_CODE, isDetail)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetParameters(companyCode As String, companyDesc As String, dealerCode As String, _
                               dealerDesc As String, svcCode As String, svcDesc As String, beginDate As String, _
                               endDate As String, selection_type As String, Exists_status_Code As String, _
                               NOT_Exists_status_Code As String, Exists_status_desc As String, LANG_CODE As String, _
                               isDetail As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(CompanyDropControl.SelectedGuid))
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            Dim exportData As String = NO
            'Dim isDetail As String = YES

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturecode = TheReportCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(CompanyDropControl.SelectedGuid))
                isDetail = NO
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    { _
                     New ReportCeBaseForm.RptParam("V_COMPANY_CODE", companyCode), _
                     New ReportCeBaseForm.RptParam("V_COMPANY_DESC", companyDesc), _
                     New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode), _
                     New ReportCeBaseForm.RptParam("V_DEALER_DESC", dealerDesc), _
                     New ReportCeBaseForm.RptParam("V_SVC_CODE", svcCode), _
                     New ReportCeBaseForm.RptParam("V_SVC_DESC", svcDesc), _
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate), _
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate), _
                     New ReportCeBaseForm.RptParam("V_SELECTION_TYPE", selection_type), _
                     New ReportCeBaseForm.RptParam("V_EXISTS_STATUS_CODE", Exists_status_Code), _
                     New ReportCeBaseForm.RptParam("V_NOTEXISTS_STATUS_CODE", NOT_Exists_status_Code), _
                     New ReportCeBaseForm.RptParam("V_EXISTS_STATUS_DESC", Exists_status_desc), _
                     New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", LANG_CODE), _
                     New ReportCeBaseForm.RptParam("V_IS_DETAIL", isDetail), _
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

    End Class
End Namespace