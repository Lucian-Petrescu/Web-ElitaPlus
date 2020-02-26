Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class ReconciliationReportForm
        Inherits ElitaPlusPage


#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String
        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME As String = "Reconciliation"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const PAGETITLE As String = "Reconciliation_Report"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "Reconciliation_Report"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"




#End Region

#Region "variables"
        'Dim moReportFormat As ReportCeBaseForm.RptFormat
        Dim compCode As String
        Dim compDesc As String
        Dim compId As Guid
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

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                rdealer.Checked = True
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label

        ' Protected WithEvents MasterPage.MessageController As ErrorController
        ' Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label
        ' Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl_New
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
            PopulateDropDowns()
            Dim t As Date = Date.Now.AddDays(-1)
            Me.txtBeginDate.Text = GetDateFormattedString(t)
            Me.txtEndDate.Text = GetDateFormattedString(Date.Now)
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            If Not String.IsNullOrEmpty(Request.QueryString("rid")) Then

            End If
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            Me.ClearLabelsErrSign()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                    'Date Calendars
                    Me.AddCalendar(Me.BtnBeginDate, Me.txtBeginDate)
                    Me.AddCalendar(Me.BtnEndDate, Me.txtEndDate)
                Else
                    btnGenRpt.Enabled = True
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            ShowMissingTranslations(Me.MasterPage.MessageController)

        End Sub

        Private Sub UpdateBreadCrum()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

#End Region

#Region "Populate"
        Private Sub PopulateDropDowns()
            PopulateCompaniesDropdown()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, ALL + " " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub
        Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")
        End Sub
        Private Sub PopulateYearsDropdown()
            ' Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.moYearList, dv, , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.moYearList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .ValueFunc = AddressOf PopulateOptions.GetCode
                  })
            Dim oDescrip As String = Me.GetSelectedDescription(Me.moYearList)
        End Sub
        Private Sub PopulateMonthsDropdown()
            Dim monthLkl As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            Me.moMonthList.Populate(monthLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf .GetCode
                })
        End Sub

#End Region

#Region "Clear"

        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(lblBeginDate)
                Me.ClearLabelErrSign(lblEndDate)
                Me.ClearLabelErrSign(lblMonth)
                Me.ClearLabelErrSign(lblYear)
                ''Me.ClearLabelErrSign(moDealerMultipleDrop.CodeTextBox.Text)
                ''Me.ClearLabelErrSign(moSvcCtrLabel)
                ''Me.ClearLabelErrSign(moExtendedLabel)
                ''Me.ClearLabelErrSign(moClaimAutoApprovelbl)
                ''Me.ClearLabelErrSign(lblClaimExtStatusSort)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GenerateReport()
            Dim reportParams As New System.Text.StringBuilder
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim langId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim reportBasedOn As String = Me.rdReportBasedOn.SelectedValue()
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            Else
                compCode = UserCompanyMultipleDrop.SelectedCode
                compDesc = UserCompanyMultipleDrop.SelectedDesc
                compId = UserCompanyMultipleDrop.SelectedGuid
            End If

            Dim culturecode As String = GuidControl.GuidToHexString(compId)
            Dim selectedYear As String = Me.GetSelectedDescription(Me.moYearList)
            Dim selectedMonthID As Guid = Me.GetSelectedItem(Me.moMonthList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim selectedYearMonth As String = selectedYear & selectedMonth

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode

            Dim selectionType As Integer
            Dim endDate As String
            Dim beginDate As String
            Dim createdby As String

            'Validating the month and year
            If (selectedMonthID.Equals(Guid.Empty) AndAlso selectedYear.Equals(String.Empty) AndAlso txtBeginDate.Text.Equals(String.Empty) AndAlso txtEndDate.Text.Equals(String.Empty)) Then
                ElitaPlusPage.SetLabelError(lblMonth)
                ElitaPlusPage.SetLabelError(lblYear)
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
            ElseIf ((Not selectedMonthID.Equals(Guid.Empty) And selectedYear.Equals(String.Empty)) Or (selectedMonthID.Equals(Guid.Empty) And Not selectedYear.Equals(String.Empty))) Then
                ElitaPlusPage.SetLabelError(lblMonth)
                ElitaPlusPage.SetLabelError(lblYear)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_MONTH_YEAR_SELECTION_ERROR)
            ElseIf ((Not txtBeginDate.Text.Equals(String.Empty) And txtEndDate.Text.Equals(String.Empty)) Or (txtBeginDate.Text.Equals(String.Empty) And Not txtEndDate.Text.Equals(String.Empty))) Then
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
            ElseIf (Not selectedMonthID.Equals(Guid.Empty) And Not selectedYear.Equals(String.Empty)) Then
                selectedYearMonth = selectedYear & selectedMonth
            Else
                ReportCeBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
                endDate = ReportCeBase.FormatDate(lblEndDate, txtEndDate.Text)
                beginDate = ReportCeBase.FormatDate(lblBeginDate, txtBeginDate.Text)

                If DateHelper.GetDateValue(txtEndDate.Text) >= DateHelper.GetDateValue(DateTime.Today.Date.ToString) Then
                    ElitaPlusPage.SetLabelError(lblEndDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_MUST_NOT_BE_HIGHER_THAN_YESTERDAY_ERR)
                End If

                Dim ts As TimeSpan
                ts = DateHelper.GetDateValue(txtEndDate.Text).Subtract(DateHelper.GetDateValue(txtBeginDate.Text))
                If ts.Days > 45 Then
                    ElitaPlusPage.SetLabelError(lblEndDate)
                    ElitaPlusPage.SetLabelError(lblBeginDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DATE_DIFFERENCE_CANNOT_BE_GREATER_THAN_45_ERR)
                End If
            End If

            'ReportExtractBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
            'endDate = ReportExtractBase.FormatDate(lblEndDate, txtEndDate.Text)
            'beginDate = ReportExtractBase.FormatDate(lblBeginDate, txtBeginDate.Text)
            'reportParams.AppendFormat("pi_user_key => '{0}',", userId)
            'reportParams.AppendFormat("V_LANG_CULTURE_CODE => '{0}',", langId)
            reportParams.AppendFormat("V_LANG_CULTURE_CODE => '{0}',", culturecode)
            reportParams.AppendFormat("v_begin_date => '{0}',", beginDate)
            reportParams.AppendFormat("v_end_date => '{0}',", endDate)
            reportParams.AppendFormat("v_year_month => '{0}',", selectedYearMonth)
            reportParams.AppendFormat("v_report_based_on => '{0}',", reportBasedOn)
            reportParams.AppendFormat("v_company => '{0}',", compCode)

            If dealerID.Equals(Guid.Empty) Then
                reportParams.AppendFormat("v_dealer => '{0}',", "*")
            Else
                reportParams.AppendFormat("v_dealer => '{0}',", dealerID)
            End If
            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "Reconciliation_Report")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "r_Reconciliation_Report.Report")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", reportParams.ToString())
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
                        Me.DisplayMessage(Message.MSG_Email_not_configured, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    Else
                        Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    End If

                    btnGenRpt.Enabled = False
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

    End Class
End Namespace
