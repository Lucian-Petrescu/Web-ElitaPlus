Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Namespace Reports

    Partial Class GapInCertNumberSeqReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Gap_InSeqOf_CertNumber"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const PAGETITLE As String = "Gap_InSeqOf_CertNumber"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "Gap_InSeqOf_CertNumber"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"

#End Region

#Region "variables"
        'Dim moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "Properties"
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl_New)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property


        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl_New)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property
#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                rdealer.Checked = True
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
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

#End Region

#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            Public IsACopy As Boolean

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public ForEdit As Boolean = False
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False

                    InitializeForm()
                    AddCalendar(BtnBeginDate, txtBeginDate)
                    AddCalendar(BtnEndDate, txtEndDate)
                    JavascriptCalls()
                End If
                If rdealer.Checked Then DealerMultipleDrop.SelectedIndex = NOTHING_SELECTED
                InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(lblEndDate)
                ClearLabelErrSign(lblBeginDate)
                ClearLabelErrSign(lblCertNumberFrom)
                ClearLabelErrSign(lblCertNumberTo)
                ClearLabelErrSign(lblCertPrefixLength)
                ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
                ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub JavascriptCalls()
            rdealer.Attributes.Add("onclick", "return ToggleDualDropDownsSelection('" + DealerMultipleDrop.CodeDropDown.ClientID + "','" + DealerMultipleDrop.DescDropDown.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")
        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, ALL + " " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                UserCompanyMultipleDrop.Visible = False

            End If
        End Sub
        Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
        End Sub
        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GenerateReport()
            Dim reportParams As New System.Text.StringBuilder
            Dim NetworkId As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim compCode As String
            Dim companyId As Guid
            Dim endDate As String
            Dim beginDate As String
            Dim showTotalsOnly As String
            Dim CertNumberFrom As String
            Dim CertNumberTo As String
            Dim CertPrefixLength As String
            ClearLabelsErrSign()

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            Else
                compCode = UserCompanyMultipleDrop.SelectedCode
                companyId = UserCompanyMultipleDrop.SelectedGuid
            End If

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode


            'Validating the dates
            If ((txtBeginDate.Text.Equals(String.Empty) AndAlso txtEndDate.Text.Equals(String.Empty)) AndAlso
                (txtCertNumberFrom.Text.Equals(String.Empty) AndAlso txtCertNumberTo.Text.Equals(String.Empty))) Then
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                ElitaPlusPage.SetLabelError(lblCertNumberFrom)
                ElitaPlusPage.SetLabelError(lblCertNumberTo)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
            ElseIf ((Not txtBeginDate.Text.Equals(String.Empty) And txtEndDate.Text.Equals(String.Empty)) Or (txtBeginDate.Text.Equals(String.Empty) And Not txtEndDate.Text.Equals(String.Empty))) Then
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
            ElseIf ((Not txtCertNumberFrom.Text.Equals(String.Empty) And txtCertNumberTo.Text.Equals(String.Empty)) Or (txtCertNumberFrom.Text.Equals(String.Empty) And Not txtCertNumberTo.Text.Equals(String.Empty))) Then
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
            ElseIf Not txtBeginDate.Text.Equals(String.Empty) And Not txtEndDate.Text.Equals(String.Empty) Then

                ReportExtractBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
                endDate = ReportExtractBase.FormatDate(lblEndDate, txtEndDate.Text)
                beginDate = ReportExtractBase.FormatDate(lblBeginDate, txtBeginDate.Text)

                If DateHelper.GetDateValue(txtEndDate.Text) >= DateHelper.GetDateValue(DateTime.Today.Date.ToString) Then
                    ElitaPlusPage.SetLabelError(lblEndDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_MUST_NOT_BE_HIGHER_THAN_YESTERDAY_ERR)
                End If
                CertNumberFrom = String.Empty
                CertNumberTo = String.Empty
            ElseIf Not txtCertNumberFrom.Text.Equals(String.Empty) And Not txtCertNumberTo.Text.Equals(String.Empty) Then
                CertNumberFrom = txtCertNumberFrom.Text
                CertNumberTo = txtCertNumberTo.Text
                beginDate = String.Empty
                endDate = String.Empty
            End If

            'Validating the Dealer selection
            If rdealer.Checked Then
                dealerCode = ALL
            Else
                If dealerID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If chkShowTotalsOnly.Checked Then
                showTotalsOnly = YES
            Else
                showTotalsOnly = NO
            End If

            If Not txtCertPrefixLength.Text.Equals(String.Empty) AndAlso Not IsNumeric(txtCertPrefixLength.Text) Then
                If dealerID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)
                End If
            ElseIf txtCertPrefixLength.Text.Equals(String.Empty) Then
                CertPrefixLength = "0"
            Else
                CertPrefixLength = txtCertPrefixLength.Text
            End If


            reportParams.AppendFormat("pi_network_id => '{0}',", NetworkId)
            reportParams.AppendFormat("pi_company_code => '{0}',", compCode)
            reportParams.AppendFormat("pi_dealer_code => '{0}',", dealerCode)
            reportParams.AppendFormat("pi_begin_date => '{0}',", beginDate)
            reportParams.AppendFormat("pi_end_date => '{0}',", endDate)
            reportParams.AppendFormat("pi_totals_only => '{0}', ", showTotalsOnly)
            reportParams.AppendFormat("pi_cert_number_from => '{0}',", CertNumberFrom)
            reportParams.AppendFormat("pi_cert_number_to => '{0}', ", CertNumberTo)
            reportParams.AppendFormat("pi_cert_prefix_len => '{0}' ", CertPrefixLength)

            State.MyBO = New ReportRequests
            State.ForEdit = True

            PopulateBOProperty(State.MyBO, "ReportType", "GAP_INSEQOF_CERTNUMBER")
            PopulateBOProperty(State.MyBO, "ReportProc", "r_certnumbergapreport.report")
            PopulateBOProperty(State.MyBO, "ReportParameters", ReportParams.ToString())
            PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            ScheduleReport()
        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheReportExtractInputControl.GetSchedDate()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()

                    State.IsNew = False
                    State.HasDataChanged = True
                    State.MyBO.CreateJob(scheduleDate)
                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    Else
                        DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    End If
                    btnGenRpt.Enabled = False
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
    End Class
End Namespace
