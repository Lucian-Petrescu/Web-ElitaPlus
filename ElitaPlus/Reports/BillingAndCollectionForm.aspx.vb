Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports

    Partial Class BillingAndCollectionForm
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public dealerCode As String
            Public begindate As String
            Public enddate As String
            Public billingStatus As String
            Public detailCode As String
            Public CompanyCode As String
            Public CompanyName As String
            Public langCultureValue As String
        End Structure

#End Region

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "BILLING_AND_COLLECTION"
        Private Const RPT_FILENAME As String = "BillingAndCollection"
        Private Const RPT_FILENAME_ESC As String = "BillingAndCollectionESC"

        Private Const RPT_FILENAME_EXPORT As String = "BillingAndCollection-EXP"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Public Const PAGETITLE As String = "BILLING_AND_COLLECTION"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "BILLING_AND_COLLECTION"
        Private Const ONE_ITEM As Integer = 1
        Private Const TOTAL_PARAMS As Integer = 15 ' 12 Elements    ' 10 Elements before without addldac
        Private Const TOTAL_EXP_PARAMS As Integer = 8 ' 6 Elements  '' ' 4 - 5 Elements before without addldac
        Private Const PARAMS_PER_REPORT As Integer = 8 ' 6 Elements '' ' 5 - 5 Elements before without addldac


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
                Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


        Sub PopulateDealerDropDown()
            'DEF-22431-START
            'Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            'DEF-22431-END
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "  " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")

        End Sub

        Private Sub InitializeForm()
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

#Region "Report Parameters"

        Private Sub GenerateReport()
            Dim endDate As String
            Dim beginDate As String
            Dim isDetail As String

            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGES, langId)
            Dim langCultureValue As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGE_CULTURES, langId)

            Dim objCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
            Dim company_code As String = objCompany.Code
            Dim company_description As String = objCompany.Description

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim dealerDesc As String = DealerMultipleDrop.SelectedDesc
            Dim billingStatus As String
            Dim detailCode As String

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            'Validating the Dealer selection
            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If dealerID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerDropControl.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.RadiobuttonCollected.Checked Then
                billingStatus = "C"
            ElseIf Me.RadiobuttonInCollection.Checked Then
                billingStatus = "I"
            ElseIf Me.RadiobuttonNotCollected.Checked Then
                billingStatus = "R"
            ElseIf Me.RadiobuttonAllOptions.Checked Then
                billingStatus = ALL
            Else
                Throw New GUIException(Message.MSG_BILLING_STATUS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BILLING_STATUS_REQUIRED_ERR)
            End If

            If Me.RadiobuttonDetail.Checked Then
                detailCode = YES
            ElseIf Me.RadiobuttonTotalsOnly.Checked Then
                detailCode = NO
            End If

            Dim params As ReportCeBaseForm.Params

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                params = SetExpParameters(company_code, company_description, dealerCode, beginDate, endDate, billingStatus, "Y", langCultureValue)
            Else
                params = SetParameters(company_code, company_description, dealerCode, beginDate, endDate, billingStatus, detailCode, langCultureValue)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetExpParameters(ByVal company_code As String, ByVal company_description As String,
                                 ByVal dealerCode As String,
                              ByVal beginDate As String,
                              ByVal endDate As String,
                              ByVal billingStatus As String,
                              ByVal detailCode As String,
                              ByVal langCultureValue As String) As ReportCeBaseForm.Params


            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(True)
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_COMPANY_CODE", company_code),
                     New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_BILLING_STATUS", billingStatus),
                     New ReportCeBaseForm.RptParam("V_DETAIL_CODE", detailCode),
                     New ReportCeBaseForm.RptParam("V_COMPANY_NAME", company_description),
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

        Function SetParameters(ByVal company_code As String, ByVal company_description As String,
                               ByVal dealerCode As String,
                               ByVal beginDate As String,
                               ByVal endDate As String,
                               ByVal billingStatus As String,
                              ByVal detailCode As String,
                              ByVal langCultureValue As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim rptParams As ReportParams
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False)
            Dim reportName As String

            With rptParams
                .dealerCode = dealerCode
                .begindate = beginDate
                .enddate = endDate
                .billingStatus = billingStatus

                If Me.RadiobuttonDetail.Checked Then
                    .detailCode = YES
                ElseIf Me.RadiobuttonTotalsOnly.Checked Then
                    .detailCode = NO
                End If

                .langCultureValue = culturecode
                .CompanyCode = company_code
                .CompanyName = company_description

            End With

            If billingStatus = ALL Then
                Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam

                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_ESC, False)
                SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
                rptParams.detailCode = "N"

                With params
                    .msRptName = reportName
                    .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                    .moRptFormat = moReportFormat
                    .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                    .moRptParams = repParams
                End With
            Else
                Dim repParams(PARAMS_PER_REPORT - 1) As ReportCeBaseForm.RptParam

                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME, False)
                SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

                With params
                    .msRptName = reportName
                    .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                    .moRptFormat = moReportFormat
                    .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                    .moRptParams = repParams
                End With
            End If

            Return params

        End Function

        Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                            ByVal rptName As String, ByVal startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY_CODE", .CompanyCode, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .begindate, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_END_DATE", .enddate, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_BILLING_STATUS", .billingStatus, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_DETAIL_CODE", .detailCode, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_COMPANY_NAME", .CompanyName, rptName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .langCultureValue, rptName)
            End With

        End Sub

#End Region

    End Class
End Namespace