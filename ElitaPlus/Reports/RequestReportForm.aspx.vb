Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security


Namespace Reports
    Public Class RequestReportForm
        Inherits ElitaPlusPage

#Region "Parameters"

#End Region

#Region "Constants"

        Public Const ALL As String = "*"
        Private Const ONE_ITEM As Integer = 1

        Private Const LABEL_OR_A_SINGLE_DEALER As String = "OR A SINGLE DEALER"
        Public Const PAGETAB As String = "REPORTS"

        Private Const OPT_DEALER_ALL As String = "DEALER_ALL"
        Private Const OPT_DEALER_SINGLE As String = "DEALER_SINGLE"

        Private Const RPT_PARAM_BEGIN_DATE As String = "BEGIN_DATE"
        Private Const RPT_PARAM_END_DATE As String = "END_DATE"
        Private Const RPT_PARAM_COMPANY As String = "COMPANY"
        Private Const RPT_PARAM_DEALER As String = "DEALER"
        Private Const RPT_PARAM_EXTENDED_STATUS As String = "EXTENDED_STATUS"
        Private Const RPT_PARAM_BATCH_NUMBER As String = "BATCH_NUMBER"

        Private Const RPT_PARAM_FIELD_LABEL_CODE As String = "CODE"
        Private Const RPT_PARAM_FIELD_MANDATORY_XCD As String = "MANDATORY_XCD"
        Private Const RPT_PARAM_FIELD_LABEL_UI_PROG_CODE As String = "LABEL_UI_PROG_CODE"
        Private Const RPT_PARAM_FIELD_MAXIMUM_LENGTH As String = "MAXIMUM_LENGTH"
        Private Const RPT_PARAM_SELECT_MONTH_AND_YEAR As String = "SELECT_MONTH_AND_YEAR"

        Private Const YESNO_YES_XCD As String = "YESNO-Y"

        Private Const ATTR_REQUIRED As String = "REQUIRED"
        Private Const ATTR_MAXIMUM_LENGTH As String = "MAXIMUM_LENGTH"

        Private Const RPT_DEF_REPORT_TYPE As String = "REPORT_TYPE"
        Private Const RPT_DEF_EXP_REPORT_PROC As String = "EXP_REPORT_PROC"

#End Region

#Region "Properties"
        Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moCompanyMultipleDrop Is Nothing Then
                    moCompanyMultipleDrop = CType(FindControl("moCompanyMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moCompanyMultipleDrop
            End Get
        End Property

        Public ReadOnly Property ExtendedStatusMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If moExtStatusMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("moExtStatusMultipleDrop"), MultipleColumnDDLabelControl_New)
                End If
                Return moExtStatusMultipleDrop
            End Get
        End Property

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl_New)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            Public CompanyGroupIdId As Guid
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public ForEdit As Boolean = False
            Public ReportType As String
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

#Region "Handlers-Init"
        Private Sub InitializeForm()
            If Not String.IsNullOrEmpty(Request.QueryString("REPORT_DEFINITION")) And Request.QueryString("REPORT_DEFINITION") = "Y" Then

                Me.State.MyBO = New ReportRequests
                Me.State.ReportType = Request.QueryString(RPT_DEF_REPORT_TYPE)
                Dim dt As DataTable = ReportRequests.GetReportParams(Me.State.ReportType)
                If dt.Rows.Count > 0 Then

                    Dim drDates() As DataRow = dt.Select(String.Format("{0} in ('{1}', '{2}')", RPT_PARAM_FIELD_LABEL_CODE, RPT_PARAM_BEGIN_DATE, RPT_PARAM_END_DATE))
                    If drDates.Length > 1 Then
                        trDates.Visible = True
                        trDates_space.Visible = True
                        trDates.Attributes.Add(ATTR_REQUIRED, drDates(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString)
                        PopulateDates()

                        If drDates(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString = YESNO_YES_XCD Then
                            lblBeginDate.Text = "* " & TranslationBase.TranslateLabelOrMessage(drDates(0)(RPT_PARAM_FIELD_LABEL_UI_PROG_CODE).ToString)
                        End If

                        If drDates(1)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString = YESNO_YES_XCD Then
                            lblEndDate.Text = "* " & TranslationBase.TranslateLabelOrMessage(drDates(1)(RPT_PARAM_FIELD_LABEL_UI_PROG_CODE).ToString)
                        End If
                    End If

                    Dim drSelectMonthAndYear() As DataRow = dt.Select(String.Format("{0} = '{1}'", RPT_PARAM_FIELD_LABEL_CODE, RPT_PARAM_SELECT_MONTH_AND_YEAR))
                    If drSelectMonthAndYear.Length > 0 Then
                        trMonthandYear.Visible = True
                        trDealer_space.Visible = True
                        trMonthandYear.Attributes.Add(ATTR_REQUIRED, drSelectMonthAndYear(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString)

                        If drSelectMonthAndYear(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString = YESNO_YES_XCD Then
                            lblAcctPeriod.Text = "* " & TranslationBase.TranslateLabelOrMessage(drSelectMonthAndYear(0)(RPT_PARAM_FIELD_LABEL_UI_PROG_CODE).ToString)
                        End If

                        Dim intYear As Integer = DateTime.Today.Year

                        For i As Integer = (intYear - 7) To intYear
                            ddlAcctPeriodYear.Items.Add(New System.Web.UI.WebControls.ListItem(i.ToString, i.ToString))
                        Next
                        ddlAcctPeriodYear.SelectedValue = intYear.ToString

                        Dim monthLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
                        Me.ddlAcctPeriodMonth.Populate(monthLkl, New PopulateOptions() With
                                                           {
                                                           .AddBlankItem = True
                                                           })

                        Dim strMonth As String = DateTime.Today.Month.ToString().PadLeft(2, CChar("0"))
                        strMonth = strMonth.Substring(strMonth.Length - 2)
                        SetSelectedItem(Me.ddlAcctPeriodMonth, LookupListNew.GetIdFromCode(LookupListNew.LK_MONTHS, strMonth))
                    End If


                    Dim drCompany() As DataRow = dt.Select(String.Format("{0} = '{1}'", RPT_PARAM_FIELD_LABEL_CODE, RPT_PARAM_COMPANY))
                    If drCompany.Length > 0 Then
                        trCompany.Visible = True
                        trCompany_space.Visible = True
                        trCompany.Attributes.Add("Required", drCompany(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString)
                        If drCompany(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString = YESNO_YES_XCD Then
                            PopulateCompaniesDropdown("* " & TranslationBase.TranslateLabelOrMessage(drCompany(0)(RPT_PARAM_FIELD_LABEL_UI_PROG_CODE).ToString))
                        End If

                    End If

                    Dim drDealer() As DataRow = dt.Select(String.Format("{0} = '{1}'", RPT_PARAM_FIELD_LABEL_CODE, RPT_PARAM_DEALER))
                    If drDealer.Length > 0 Then
                        trDealer.Visible = True
                        trDealer_space.Visible = True
                        trDealer.Attributes.Add(ATTR_REQUIRED, drDealer(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString)
                        If drDealer(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString = YESNO_YES_XCD Then
                            lblDealer.Text = "* " & TranslationBase.TranslateLabelOrMessage(drDealer(0)(RPT_PARAM_FIELD_LABEL_UI_PROG_CODE).ToString)
                        End If
                        PopulateDealerDropDown()
                    End If

                    Dim drExtstattus() As DataRow = dt.Select(String.Format("{0} = '{1}'", RPT_PARAM_FIELD_LABEL_CODE, RPT_PARAM_EXTENDED_STATUS))
                    If drExtstattus.Length > 0 Then
                        trExtStatus.Visible = True
                        trExtStatus_space.Visible = True
                        trExtStatus.Attributes.Add(ATTR_REQUIRED, drExtstattus(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString)
                        If drExtstattus(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString = YESNO_YES_XCD Then
                            PopulateExtendedStatusDropDown("* " & TranslationBase.TranslateLabelOrMessage(drExtstattus(0)(RPT_PARAM_FIELD_LABEL_UI_PROG_CODE).ToString))
                        End If
                    End If

                    Dim drBatchNum() As DataRow = dt.Select(String.Format("{0} = '{1}'", RPT_PARAM_FIELD_LABEL_CODE, RPT_PARAM_BATCH_NUMBER))
                    If drBatchNum.Length > 0 Then
                        trbatchnumber.Visible = True
                        trbatchnumber_space.Visible = True
                        trbatchnumber.Attributes.Add(ATTR_REQUIRED, drBatchNum(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString)
                        trbatchnumber.Attributes.Add(ATTR_MAXIMUM_LENGTH, drBatchNum(0)(RPT_PARAM_FIELD_MAXIMUM_LENGTH).ToString)
                        Me.txtBatchNumber.Text = String.Empty
                        If drBatchNum(0)(RPT_PARAM_FIELD_MANDATORY_XCD).ToString = YESNO_YES_XCD Then
                            lblBatchNumber.Text = "* " & TranslationBase.TranslateLabelOrMessage(drBatchNum(0)(RPT_PARAM_FIELD_LABEL_UI_PROG_CODE).ToString)
                        End If
                    End If

                End If
            End If

        End Sub


        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(lblBeginDate)
                Me.ClearLabelErrSign(CompanyMultipleDrop.CaptionLabel)
                Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
                Me.ClearLabelErrSign(lblDealer)
                Me.ClearLabelErrSign(ExtendedStatusMultipleDrop.CaptionLabel)
                Me.ClearLabelErrSign(lblBatchNumber)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here      
            Me.MasterPage.MessageController.Clear_Hide()
            Me.Title = TranslationBase.TranslateLabelOrMessage(Request.QueryString(RPT_DEF_REPORT_TYPE))
            Me.ClearLabelsErrSign()
            'JavascriptCalls()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    SetFormTitle(Me.State.ReportType)
                    SetFormTab(PAGETAB)
                    UpdateBreadCrum()
                End If

                Me.InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDates()
            Dim t As Date = Date.Now.AddDays(-1)
            Me.txtBeginDate.Text = GetDateFormattedString(t)
            Me.txtEndDate.Text = GetDateFormattedString(Date.Now)
            Me.AddCalendar(Me.btnBeginDate, Me.txtBeginDate)
            Me.AddCalendar(Me.btnEndDate, Me.txtEndDate)
        End Sub

        Private Sub PopulateCompaniesDropdown(ByVal strTitle As String)
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            CompanyMultipleDrop.NothingSelected = True
            CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.NEW_MODE, True, dv, strTitle, True)
            If dv.Count.Equals(ONE_ITEM) Then
                CompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                OnFromDrop_Changed(CompanyMultipleDrop)
                CompanyMultipleDrop.ChangeEnabledControlProperty(True)
            End If
        End Sub

        Private Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(CompanyMultipleDrop.SelectedGuid.ToString))
            DealerMultipleDrop.NothingSelected = True

            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_OR_A_SINGLE_DEALER), True, True)
            DealerMultipleDrop.ChangeEnabledControlProperty(True)

        End Sub

        Private Sub PopulateExtendedStatusDropDown(ByVal strTitle As String)


            Dim dv As DataView = LookupListNew.GetExtendedStatusByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            ExtendedStatusMultipleDrop.NothingSelected = True
            ExtendedStatusMultipleDrop.SetControl(False, ExtendedStatusMultipleDrop.MODES.NEW_MODE, True, dv, strTitle, True, True)
            ExtendedStatusMultipleDrop.ChangeEnabledControlProperty(True)

        End Sub

#End Region

#Region "Handlers-DropDown"

        Protected Sub moDealerOptionList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moDealerOptionList.SelectedIndexChanged
            Try
                If moDealerOptionList.SelectedValue = OPT_DEALER_ALL Then
                    pnlDealerDropControl.Visible = False
                ElseIf moDealerOptionList.SelectedValue = OPT_DEALER_SINGLE Then
                    pnlDealerDropControl.Visible = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moCompanyMultipleDrop.SelectedDropChanged
            Try
                If trDealer.Visible = True Then
                    PopulateDealerDropDown()
                End If

            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub UpdateBreadCrum()
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(Me.State.ReportType)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Me.State.ReportType)
        End Sub

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "GenerateReport"
        Private Sub GenerateReport()
            Dim reportParams As New System.Text.StringBuilder
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim langId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Dim dealerCode As String
            Dim endDate As String
            Dim beginDate As String
            Dim SelectedReportBasedOn As String

            reportParams.AppendFormat("pi_user_id => '{0}',", userId)
            reportParams.AppendFormat("pi_language_id => '{0}',", langId)


            If trCompany.Visible = True Then

                'Validating the Company Selection
                If CompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                    If trCompany.Attributes(ATTR_REQUIRED) = YESNO_YES_XCD Then
                        ElitaPlusPage.SetLabelError(CompanyMultipleDrop.CaptionLabel)
                        Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
                    Else
                        reportParams.AppendFormat("pi_company => '{0}',", String.Empty)
                    End If

                Else
                    reportParams.AppendFormat("pi_company => '{0}',", CompanyMultipleDrop.SelectedCode)
                End If
            End If

            If trDates.Visible = True Then
                'Validate Dates
                If txtBeginDate.Text.Equals(String.Empty) AndAlso txtEndDate.Text.Equals(String.Empty) Then
                    If trDates.Attributes(ATTR_REQUIRED) = YESNO_YES_XCD Then
                        ElitaPlusPage.SetLabelError(lblBeginDate)
                        ElitaPlusPage.SetLabelError(lblEndDate)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                    End If
                ElseIf ((Not txtBeginDate.Text.Equals(String.Empty) And txtEndDate.Text.Equals(String.Empty)) Or (txtBeginDate.Text.Equals(String.Empty) And Not txtEndDate.Text.Equals(String.Empty))) Then
                    ElitaPlusPage.SetLabelError(lblBeginDate)
                    ElitaPlusPage.SetLabelError(lblEndDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
                ElseIf Not txtBeginDate.Text.Equals(String.Empty) And Not txtEndDate.Text.Equals(String.Empty) Then
                    ReportExtractBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
                End If

                endDate = ReportExtractBase.FormatDate(lblEndDate, txtEndDate.Text)
                beginDate = ReportExtractBase.FormatDate(lblBeginDate, txtBeginDate.Text)

                reportParams.AppendFormat("pi_begin_date => '{0}',", beginDate)
                reportParams.AppendFormat("pi_end_date => '{0}',", endDate)

            End If

            If trDealer.Visible = True Then

                SelectedReportBasedOn = moDealerOptionList.SelectedValue
                Dim selectedDealerOption As String = moDealerOptionList.SelectedValue
                If selectedDealerOption = OPT_DEALER_ALL Then
                    reportParams.AppendFormat("pi_dealer => '{0}',", "*")
                ElseIf selectedDealerOption = OPT_DEALER_SINGLE Then
                    dealerCode = DealerMultipleDrop.SelectedCode
                    If String.IsNullOrEmpty(dealerCode) Then
                        If trDealer.Attributes(ATTR_REQUIRED) = YESNO_YES_XCD Then
                            Throw New GUIException(Message.MSG_SELECT_A_SINGLE_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                        Else
                            reportParams.AppendFormat("pi_dealer => '{0}',", String.Empty)
                        End If
                    Else
                        Dim dealer As Dealer = New Dealer(DealerMultipleDrop.SelectedGuid)
                        If dealer Is Nothing Then
                            Throw New GUIException(Message.MSG_SELECT_A_SINGLE_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                        End If
                        reportParams.AppendFormat("pi_dealer => '{0}',", dealerCode)
                    End If

                End If
            End If

            If trExtStatus.Visible = True Then
                If ExtendedStatusMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then

                    If trExtStatus.Attributes(ATTR_REQUIRED) = YESNO_YES_XCD Then
                        ElitaPlusPage.SetLabelError(ExtendedStatusMultipleDrop.CaptionLabel)
                        Throw New GUIException(Message.MSG_INVALID_EXTENDED_STATUS, Assurant.ElitaPlus.Common.ErrorCodes.GUI_EXTENDED_STATUS_MUST_BE_SELECTED_ERR)
                    Else
                        reportParams.AppendFormat("pi_extended_status => '{0}',", String.Empty)
                    End If
                Else
                    reportParams.AppendFormat("pi_extended_status => '{0}',", ExtendedStatusMultipleDrop.SelectedCode)
                End If
            End If

            If trbatchnumber.Visible = True Then

                If String.IsNullOrWhiteSpace(txtBatchNumber.Text) Then

                    If trbatchnumber.Attributes(ATTR_REQUIRED) = YESNO_YES_XCD Then
                        ElitaPlusPage.SetLabelError(lblBatchNumber)
                        Throw New GUIException(Message.MSG_INVALID_BATCH_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BATCH_NUMBER_MUST_BE_ENTERED_ERR)
                    Else
                        reportParams.AppendFormat("pi_batch_number => '{0}',", String.Empty)
                    End If
                Else
                    If txtBatchNumber.Text.Length > trbatchnumber.Attributes(ATTR_MAXIMUM_LENGTH) Then
                        ElitaPlusPage.SetLabelError(lblBatchNumber)
                        Throw New GUIException(Message.MSG_INVALID_BATCH_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BATCH_NUMBER_LENGH_IS_INVALID_ERR)
                    Else
                        reportParams.AppendFormat("pi_batch_number => '{0}',", txtBatchNumber.Text)
                    End If

                End If
            End If

            If trMonthandYear.Visible = True Then
                Dim selectedYear As String = GetSelectedDescription(Me.ddlAcctPeriodYear)
                Dim selectedMonthId As Guid = GetSelectedItem(Me.ddlAcctPeriodMonth)
                Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
                Dim selectedYearMonth As String
                selectedYearMonth = selectedYear & selectedMonth

                If trMonthandYear.Attributes(ATTR_REQUIRED) = YESNO_YES_XCD AndAlso (selectedMonthId.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty)) Then
                    'Report Period should be valid
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)

                Else
                    reportParams.AppendFormat("pi_period => '{0}',", selectedYearMonth)
                End If

            End If

            Me.State.ForEdit = True
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", Me.State.ReportType)
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", Request.QueryString(RPT_DEF_EXP_REPORT_PROC))
            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", reportParams.ToString().Substring(0, reportParams.Length - 1))
            Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            ScheduleReport()
        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheReportExtractInputControl.GetSchedDate()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()

                    Me.State.IsNew = False
                    Me.State.HasDataChanged = True
                    Me.State.MyBO.CreateJob(scheduleDate)

                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        Me.DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    Else
                        Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    End If

                    btnGenRpt.Enabled = False

                Else
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateBosFromForm()
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

    End Class
End Namespace

