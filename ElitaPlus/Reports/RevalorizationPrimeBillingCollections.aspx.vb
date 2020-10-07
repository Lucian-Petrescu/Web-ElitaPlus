Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports
    Partial Class RevalorizationPrimeBillingCollections
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW_PV As String = "PRIME_VALORIZATION"
        Private Const RPT_FILENAME_WINDOW_BC As String = "BILLING_AND_COLLECTION"
        Private Const RPT_FILENAME_PV As String = "ValorizationPrime"
        Private Const RPT_FILENAME_BC As String = "ValorizationBillingCollected"
        Private Const RPT_FILENAME_EXPORT_PV As String = "ValorizationPrime-Exp"
        Private Const RPT_FILENAME_EXPORT_BC As String = "ValorizationBillingCollected-Exp"
        Public Const PAGETAB As String = "REPORTS"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "D"
        Private Const NO As String = "N"
        Private Const ONE_ITEM As Integer = 1
        Private Const BRAZIL_CODE As String = "BR"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const LABEL_OR_A_SINGLE_DEALER As String = "OR A SINGLE DEALER"

#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public dealerCode As String
            Public begindate As String
            Public enddate As String
            Public isSummary As String
            Public CompanyCode As String
            Public addlDAC As String
            Public CompanyName As String
            Public langCultureValue As String
            Public langCode As String
            Public totalsByCov As String
        End Structure

#End Region

#Region "variables"
        Private ReportFormat As ReportCeBaseForm.RptFormat
        Dim strRptType As String = String.Empty
        Dim oCompany As Company
        Private rptName As String = ""
        Dim DAC_CODE As String
        Public Event SelectedDropChanged(aSrc As MultipleColumnDDLabelControl)
#End Region

#Region "Properties"
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property

        Public ReadOnly Property UserDealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserDealerMultipleDrop Is Nothing Then
                    moUserDealerMultipleDrop = CType(FindControl("moUserDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserDealerMultipleDrop
            End Get
        End Property

#End Region

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
            ErrControllerMaster.Clear_Hide()
            ClearLabelsErrSign()
            If Request.QueryString("CALLER") IsNot Nothing Then
                strRptType = Request.QueryString("CALLER")
                If Request.QueryString("CALLER") = "PV" Then
                    Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW_PV)
                Else
                    Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW_BC)
                End If
            End If
            InstallProgressBar()

            Try
                If Not IsPostBack Then
                    SetFormTitle(Title, False)
                    SetFormTab(PAGETAB)
                    JavascriptCalls()
                    AddCalendar(BtnBeginDate, moBeginDateText)
                    AddCalendar(BtnEndDate, moEndDateText)
                    InitializeForm()
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#Region "JavaScript"

        Public Sub JavascriptCalls()
            rdealer.Attributes.Add("onclick", "return ToggleDualDropDownsSelection('" + moUserDealerMultipleDrop.CodeDropDown.ClientID + "','" + moUserDealerMultipleDrop.DescDropDown.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")

        End Sub
#End Region

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
                ClearLabelErrSign(UserDealerMultipleDrop.CaptionLabel)

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub InitializeForm()
            TheReportCeInputControl.SetExportOnly()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            moBeginDateText.Text = GetDateFormattedString(t)
            moEndDateText.Text = GetDateFormattedString(Date.Now)
            rdealer.Checked = True
            If strRptType = "PV" Then
                TheReportCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT_PV)
            Else
                TheReportCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT_BC)
            End If
        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
            End If
        End Sub

        Sub PopulateDealerDropDown()

            Dim dv As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)

            UserDealerMultipleDrop.NothingSelected = True
            UserDealerMultipleDrop.SetControl(False,
                                             UserDealerMultipleDrop.MODES.NEW_MODE,
                                             True,
                                             dv,
                                             TranslationBase.TranslateLabelOrMessage(LABEL_OR_A_SINGLE_DEALER),
                                             True,
                                             False,
                                             " ctl00_ContentPanelMainContentBody_rdealer.checked = false;",
                                             "moUserDealerMultipleDrop_moMultipleColumnDrop",
                                             "moUserDealerMultipleDrop_moMultipleColumnDropDesc", "moUserDealerMultipleDrop_lb_DropDown")

        End Sub

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

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Report Parameters"

        Private Sub GenerateReport()
            Dim endDate As String
            Dim beginDate As String
            Dim isDetail As String

            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGES, langId)
            Dim langCultureValue As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGE_CULTURES, langId)
            Dim addlDACCode As String

            'Dim objCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
            Dim company_code As String = UserCompanyMultipleDrop.SelectedCode
            Dim company_description As String = UserCompanyMultipleDrop.SelectedDesc

            Dim dealerID As Guid = UserDealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = UserDealerMultipleDrop.SelectedCode
            Dim dealerDesc As String = UserDealerMultipleDrop.SelectedDesc
            Dim isSummary As String
            Dim totalsByCov As String
            Dim params As ReportCeBaseForm.Params

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)
            If String.IsNullOrEmpty(dealerCode) Then
                dealerCode = ALL
            End If

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            ReportFormat = ReportCeBase.GetReportFormat(Me)
            If (ReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (ReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                params = SetExpParameters(company_code, dealerCode, beginDate, endDate)

            Else
                'View Report
                params = SetParameters(company_code, dealerCode, beginDate, endDate)

            End If
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params


        End Sub

        Function SetExpParameters(companyCode As String, _
                                 dealerCode As String, _
                              beginDate As String, _
                              endDate As String) As ReportCeBaseForm.Params
            Dim params As New ReportCeBaseForm.Params
            Dim rptParams As ReportParams
            Dim culturevalue As String = TheReportCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = String.Empty

            If strRptType = "PV" Then
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT_PV, True)
            Else
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT_BC, True)
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                        { _
                          New ReportCeBaseForm.RptParam("V_COMPANY_CODE", companyCode), _
                          New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode), _
                          New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate), _
                          New ReportCeBaseForm.RptParam("V_END_DATE", endDate)}


            With params
                .msRptName = reportName
                If strRptType = "PV" Then
                    .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW_PV)
                    'Me.rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW_PV))
                Else
                    .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW_BC)
                    'Me.rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW_BC))
                End If
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params

        End Function

        Function SetParameters(companyCode As String,
                               dealerCode As String, _
                               beginDate As String, _
                               endDate As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim rptParams As ReportParams
            Dim reportName As String = String.Empty

            If strRptType = "PV" Then
                'reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_PV, False)
                reportName = RPT_FILENAME_PV
            Else
                reportName = RPT_FILENAME_BC
                'reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_BC, False)
            End If

            With rptParams
                .CompanyCode = companyCode
                .dealerCode = dealerCode
                .begindate = beginDate
                .enddate = endDate
            End With
            reportFormat = ReportCeBase.GetReportFormat(Me)
            rptParams.isSummary = "Y"

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                        { _
                          New ReportCeBaseForm.RptParam("V_COMPANY_CODE", companyCode), _
                          New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode), _
                          New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate), _
                          New ReportCeBaseForm.RptParam("V_END_DATE", endDate)}


            With params
                .msRptName = reportName
                If strRptType = "PV" Then
                    .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW_PV)
                    rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW_PV))
                Else
                    .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW_BC)
                    rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW_BC))
                End If
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params

        End Function

#End Region

    End Class
End Namespace
