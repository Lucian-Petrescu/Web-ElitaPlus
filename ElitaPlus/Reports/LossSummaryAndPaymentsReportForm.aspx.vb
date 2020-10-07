Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports System.Web.Script.Services
Imports Assurant.ElitaPlus.Security
Imports System.Web.Script.Serialization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Linq

Namespace Reports

    Partial Class LossSummaryAndPaymentsReportForm
        Inherits ElitaPlusPage

#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
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
#Region "Constants"

        Private Const ONE_ITEM As Integer = 1
        Private Const AccountingPeriod As String = "AccountingPeriod"
        Private Const CutOff As String = "CutOff"
        Private Const Summarized As String = "Summarized"
        Private Const Detailed As String = "Detailed"

        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const LABEL_OR_A_SINGLE_DEALER As String = "OR A SINGLE DEALER"
        Public Const PAGETITLE As String = "LOSS_SUMMARY_AND_PAYMENTS_REPORTFORM"
        Public Const PAGETAB As String = "REPORTS"
        Private Const RPT_FILENAME_WINDOW As String = "LOSS_SUMMARY_AND_PAYMENTS_REPORTFORM"

#End Region

#Region "Properties"

        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If multipleDealerDropControl Is Nothing Then
                    multipleDealerDropControl = CType(FindControl("multipleDealerDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return multipleDealerDropControl
            End Get
        End Property
#End Region

#Region "variables"
        Private currentAccountingMonth As Integer
        Private currentAccountingYear As Integer

#End Region

#Region "JavaScript"

        Public Sub JavascriptCalls()
            rdealer.Attributes.Add("onclick", "return ToggleDualDropDownsSelection('" + multipleDealerDropControl.CodeDropDown.ClientID + "','" + multipleDealerDropControl.DescDropDown.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")
        End Sub
#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            MasterPage.MessageController.Clear_Hide()
            ClearLabelsError()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not IsPostBack Then

                    currentAccountingMonth = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Date.Today).Month
                    currentAccountingYear = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Date.Today).Year
                    ViewState("CURRENTACCOUNTINGMONTH") = currentAccountingMonth
                    ViewState("CURRENTACCOUNTINGYEAR") = currentAccountingYear

                    InitializeForm()
                    UpdateBreadCrum()
                    JavascriptCalls()

                    AddCalendar_New(imgCutOffDate, txtCutOffDate)

                Else
                    currentAccountingMonth = CType(ViewState("CURRENTACCOUNTINGMONTH"), Integer)
                    currentAccountingYear = CType(ViewState("CURRENTACCOUNTINGYEAR"), Integer)
                    If rdoAccountingPeriod.Checked Then
                        trSelectMonthYear.Style.Add("display", "block")
                        trCutOff.Style.Add("display", "none")
                    Else
                        trSelectMonthYear.Style.Add("display", "none")
                        trCutOff.Style.Add("display", "block")
                    End If
                    If rdealer.Checked Then
                        multipleDealerDropControl.CodeDropDown.SelectedIndex = -1
                        multipleDealerDropControl.DescDropDown.SelectedIndex = -1
                    End If
                End If

                InstallDisplayNewReportProgressBar()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub InitializeForm()

            TheReportExtractInputControl.ViewVisible = False
            TheReportExtractInputControl.PdfVisible = False
            TheReportExtractInputControl.ExportDataVisible = False
            TheReportExtractInputControl.DestinationVisible = False

            PopulateCompaniesDropdown()
            PopulateMonthsAndYearsDropdown()
            PopulateDealerDropDown()

            MonthYearLabel.Text = "* " + MonthYearLabel.Text + ":"
            lblCutOff.Text = "* " + lblCutOff.Text + ":"
            lblSelectAllDealers.Text = "* " + lblSelectAllDealers.Text
        End Sub
#End Region
#Region "Handlers-DropDown"

        Protected Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
             Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
                rdealer.Checked = True
                btnGenRpt.Enabled = True

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                ClearLabelsError()
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"
        Private Sub ClearLabelsError()
            Try
                ClearLabelErrSign(MonthYearLabel)
                ClearLabelErrSign(lblCutOff)
                ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
                ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
                ClearLabelErrSign(lblSelectAllDealers)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region


#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                OnFromDrop_Changed(UserCompanyMultipleDrop)
            End If
        End Sub

        Private Sub PopulateMonthsAndYearsDropdown()
            '  Dim dvM As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dvM.Sort = "CODE"
            ' BindListControlToDataView(Me.MonthDropDownList, dvM, , , True)
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            MonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True,
              .SortFunc = AddressOf PopulateOptions.GetCode
           })



            '    Dim dvY As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            '    dvY.RowFilter = " DESCRIPTION LIKE '" & currentAccountingYear & "' or DESCRIPTION LIKE '" & currentAccountingYear - 1 & "'"
            ' BindListTextToDataView(Me.YearDropDownList, dvY, , , True)

            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId

            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)



            Dim filteredYearList As ListItem() = (From x In YearListLkl
                                                  Where x.Description = currentAccountingYear.ToString() Or x.Description = (currentAccountingYear - 1).ToString()
                                                  Select x).ToArray()

            YearDropDownList.Populate(filteredYearList, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .BlankItemValue = "0",
            .ValueFunc = AddressOf PopulateOptions.GetCode
                  })
        End Sub

        Private Sub PopulateDealerDropDown()
            If Not UserCompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                Dim dv As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_OR_A_SINGLE_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")
            Else
                Dim dv As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "  " + TranslationBase.TranslateLabelOrMessage(LABEL_OR_A_SINGLE_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")
            End If
        End Sub
#End Region

        Private Sub UpdateBreadCrum()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)

        End Sub
#Region "Report Generation"
        Private Sub GenerateReport()

            Dim scheduleDate As DateTime = TheReportExtractInputControl.GetSchedDate()

            Dim reportParams As New System.Text.StringBuilder

            Dim AccountingMonthAndYear As String
            Dim selectedReportingPeriod As String
            Dim selectedReportingtype As String
            Dim selectedCutoffDate As String

            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid

            Dim selectedYear As String = GetSelectedDescription(YearDropDownList)
            Dim selectedMonthID As Guid = GetSelectedItem(MonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)

            If rdoAccountingPeriod.Checked Then
                ' Code when Accountingperiod reporting period is selected
                selectedReportingPeriod = AccountingPeriod

                If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                    ' either month or year is not selected
                    ElitaPlusPage.SetLabelError(MonthYearLabel)
                    Throw New GUIException(Message.MSG_INVALID_YEARMONTH, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_NOT_SELECTED_ERR)
                End If

                If (DateTime.Compare(scheduleDate.Date, DateTime.Now.Date) = 0) Then
                    ' If report schedulling option is not selected
                    If CType(selectedMonth, Integer) > currentAccountingMonth And CType(selectedYear, Integer) >= currentAccountingYear Then
                        ' Accounting month is not closed for the selected Month and Year
                        ElitaPlusPage.SetLabelError(MonthYearLabel)
                        Throw New GUIException(Message.MSG_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED)
                    End If
                End If

                AccountingMonthAndYear = selectedMonth & selectedYear

            ElseIf rdoCutOff.Checked
                ' Code when Cutoff reporting period is selected
                selectedReportingPeriod = CutOff

                If (txtCutOffDate.Text.Equals(String.Empty)) Then
                    ' either cutoff date is empty
                    ElitaPlusPage.SetLabelError(lblCutOff)
                    Throw New GUIException(Message.MSG_INVALID_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CUTOFF_DATE_MUST_ERR)
                Else
                    selectedCutoffDate = ReportExtractBase.FormatDate(lblCutOff, txtCutOffDate.Text)
                    If (DateTime.Compare(DateHelper.GetDateValue(txtCutOffDate.Text), scheduleDate.Date) >= 0) Then
                        ' Cutoff date is equal or greater than either Current date or Scheduled date
                        ElitaPlusPage.SetLabelError(lblCutOff)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CUTOFF_DATE_INVALID_ERR)
                    End If
                End If
            End If


            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_COMPANY_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            ' Validating Dealer selection
            If (rdealer.Checked = False And selectedDealerId.Equals(Guid.Empty)) Then
                ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                ElitaPlusPage.SetLabelError(lblSelectAllDealers)
                Throw New GUIException(Message.MSG_DEALER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR) 'DEALER_IS_REQUIRED, GUI_DEALER_NOT_SELECTED
            End If

            ' Reporting type
            If (rdoSummarized.Checked = True) Then
                selectedReportingtype = Summarized
            Else
                selectedReportingtype = Detailed
            End If

            ' Report Paramater 
            reportParams.AppendFormat("pi_reporting_period => '{0}',", selectedReportingPeriod)
            reportParams.AppendFormat("pi_company_code => '{0}',", compCode)
            reportParams.AppendFormat("pi_acc_month_and_year => '{0}',", AccountingMonthAndYear)
            reportParams.AppendFormat("pi_cutoff_date => '{0}',", selectedCutoffDate)
            reportParams.AppendFormat("pi_dealer_code => '{0}',", dealerCode)
            reportParams.AppendFormat("pi_reporting_type => '{0}'", selectedReportingtype)

            State.MyBO = New ReportRequests
            State.ForEdit = True
            PopulateBOProperty(State.MyBO, "ReportType", "LOSS_SUMMARY_AND_PAYMENTS_REPORTFORM")
            PopulateBOProperty(State.MyBO, "ReportProc", "r_loss_summary_and_payments.Report")
            PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
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
#End Region
    End Class
End Namespace

