Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class PremiumReconciliationForChildCertificates
        Inherits ElitaPlusPage

#Region "Parameters"

#End Region

#Region "Constants"

        Public Const PAGETITLE As String = "PremiumReconciliationForChildCertificates"
        Public Const PAGETAB As String = "REPORTS"

#End Region

#Region "Handlers-DropDown"
        Public Function PopulateCompaniesDropDown() As DataView
            Return ElitaPlusIdentity.Current.ActiveUser.GetUserCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
        End Function


#End Region
#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
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

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            InitializeComponent()

        End Sub

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()

            Try
                If Not IsPostBack Then

                    ' Step - Hide standard Crystal Report Display Options
                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False

                    ' Step - Configure Header, Breadcrum, etc.
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    SetFormTab(PAGETAB)
                    UpdateBreadCrum()

                    ' Step - Attach Javascripts to Calandar and Populate Company Dropdown
                    AddCalendar_New(BeginDateCal, BeginDate)
                    AddCalendar_New(EndDateCal, EndDate)
                    ' BindCodeNameToListControl(CompanyDropDown, PopulateCompaniesDropDown(), , , "COMPANY_ID", True) 'UserCompanies

                    Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("Company", Thread.CurrentPrincipal.GetLanguageCode())
                    Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
                    Dim currTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                     Return li.Translation + " " + "(" + li.Code + ")"
                                                                                 End Function
                    Dim filteredList As ListItem() = (From x In compLkl
                                                      Where list.Contains(x.ListItemId)
                                                      Select x).ToArray()

                    CompanyDropDown.Populate(filteredList, New PopulateOptions() With
                  {
                   .AddBlankItem = True,
                   .TextFunc = currTextFunc
                  })
                    HandleChangeCompany()
                Else
                    Page.ClientScript.RegisterStartupScript([GetType](), "Startup123456", "<script>enableDisablePeriod();</script>")
                End If

                InstallDisplayNewReportProgressBar()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(lblRange)
                ClearLabelErrSign(MonthYearLabel)
                ClearLabelErrSign(lbldealers)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
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
            ClearLabelsErrSign()
            Dim reportParams As New System.Text.StringBuilder
            Dim oStartDateTime As DateTime = Nothing
            Dim oEndDateTime As DateTime = Nothing
            Dim runReport As Boolean = True

            'Validating Range Period - Should be less than or equal to 1 year
            If ReportPeriodBasedOn.SelectedValue = "R" Then
                'User Selected Range Option
                If BeginDate.Text.Equals(String.Empty) OrElse BeginDate.Text Is Nothing Then
                    ' Begin Date is Not Selected
                    ElitaPlusPage.SetLabelError(lblRange)
                    Try
                        Throw New GUIException(Message.MSG_INVALID_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_RANGE)
                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)
                        runReport = False
                    End Try
                Else
                    oStartDateTime = DateHelper.GetDateValue(BeginDate.Text)

                    If EndDate.Text.Equals(String.Empty) OrElse EndDate.Text Is Nothing Then
                        ' End Date is Not Selected
                        ElitaPlusPage.SetLabelError(lblRange)
                        Try
                            Throw New GUIException(Message.MSG_INVALID_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_RANGE)
                        Catch ex As Exception
                            HandleErrors(ex, MasterPage.MessageController)
                            runReport = False
                        End Try
                    Else
                        oEndDateTime = DateHelper.GetDateValue(EndDate.Text)

                        If oEndDateTime < oStartDateTime Then
                            ' End Date is Less Than Begin Date
                            ElitaPlusPage.SetLabelError(lblRange)
                            Try
                                Throw New GUIException(Message.MSG_INVALID_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_RANGE)
                            Catch ex As Exception
                                HandleErrors(ex, MasterPage.MessageController)
                                runReport = False
                            End Try
                        End If

                        If (oStartDateTime.AddMonths(12) <= oEndDateTime) Then
                            ' Date Range is Greater Than 12 Months
                            ElitaPlusPage.SetLabelError(lblRange)
                            Try
                                Throw New GUIException(Message.MSG_MAX_YEAR_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_MAX_YEAR_DATE_RANGE)
                            Catch ex As Exception
                                HandleErrors(ex, MasterPage.MessageController)
                                runReport = False
                            End Try
                        End If
                    End If
                End If
            End If

            If ReportPeriodBasedOn.SelectedValue = "MY" Then
                'User Selected Month and Year Option
                If MonthDropDown.SelectedValue Is Nothing OrElse MonthDropDown.SelectedValue.Equals(String.Empty) OrElse MonthDropDown.SelectedValue = "00000000-0000-0000-0000-000000000000" Then
                    'Month is Not Selected
                    ElitaPlusPage.SetLabelError(MonthYearLabel)
                    Try
                        Throw New GUIException(Message.MSG_INVALID_YEARMONTH, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_NOT_SELECTED_ERR)
                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)
                        runReport = False
                    End Try
                ElseIf YearDropDown.SelectedValue Is Nothing OrElse YearDropDown.SelectedValue.Equals(String.Empty) OrElse YearDropDown.SelectedValue = "0" Then
                    'Year is Not Selected
                    ElitaPlusPage.SetLabelError(MonthYearLabel)
                    Try
                        Throw New GUIException(Message.MSG_INVALID_YEARMONTH, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_NOT_SELECTED_ERR)
                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)
                        runReport = False
                    End Try
                End If
            End If

            If UsercontrolAvailableSelectedDealers.SelectedList.Count = 0 Then
                'Dealer is Not Selected
                ElitaPlusPage.SetLabelError(lbldealers)
                Try
                    Throw New GUIException(Message.MSG_DEALER_MUST_BE_SELECTED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                    runReport = False
                End Try
            End If

            If runReport Then
                reportParams.AppendFormat("pi_company => '{0}',", LookupListNew.GetCodeFromId("COMPANIES", GetSelectedItem(DirectCast(CompanyDropDown, DropDownList))))
                reportParams.AppendFormat("pi_dealer => '{0}',", UsercontrolAvailableSelectedDealers.SelectedListwithCommaSep)
                reportParams.AppendFormat("pi_report_based_on => '{0}',", ReportBasedOn.SelectedValue)

                If ReportPeriodBasedOn.SelectedValue = "R" Then
                    reportParams.AppendFormat("pi_begin_date => '{0}',", oStartDateTime.ToString("yyyyMMdd"))
                    reportParams.AppendFormat("pi_end_date => '{0}',", oEndDateTime.ToString("yyyyMMdd"))
                    reportParams.AppendFormat("pi_year_month => '{0}'", DBNull.Value)
                ElseIf ReportPeriodBasedOn.SelectedValue = "MY" Then
                    reportParams.AppendFormat("pi_begin_date => '{0}',", DBNull.Value)
                    reportParams.AppendFormat("pi_end_date => '{0}',", DBNull.Value)
                    reportParams.AppendFormat("pi_year_month => '{0}'", GetSelectedDescription(YearDropDown) & LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, GetSelectedItem(MonthDropDown)))
                End If

                State.MyBO = New ReportRequests
                PopulateBOProperty(State.MyBO, "ReportType", "PremiumReconciliationForChildCertificates")
                PopulateBOProperty(State.MyBO, "ReportProc", "R_PremiumReconChildCert.Report")
                PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
                PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

                ScheduleReport()
            End If
        End Sub

#Region "UI Event Handlers"

        Protected Sub CompanyDropDown_SelectedIndexChanged(sender As Object, e As EventArgs)
            HandleChangeCompany()
        End Sub

#End Region

#Region "Private Functions"
        Private Sub HandleChangeCompany()
            ' Step 1 - Get Company ID of Selected Company in Drop Down
            Dim companyId As Guid = GetSelectedItem(CompanyDropDown)

            ' Step 2 - If no Company is selected then clear Dealer, Month and Year Dropdowns and exit.
            If (companyId = Guid.Empty) Then
                UsercontrolAvailableSelectedDealers.SetAvailableData(Nothing, LookupListNew.COL_CODE_NAME, LookupListNew.COL_ID_NAME)
                UsercontrolAvailableSelectedDealers.SetSelectedData(Nothing, LookupListNew.COL_CODE_NAME, LookupListNew.COL_ID_NAME)
                MonthDropDown.Items.Clear()
                YearDropDown.Items.Clear()
                Page.ClientScript.RegisterStartupScript([GetType](), "Startup12345", "<script>enableDisablePeriod();</script>")
                Exit Sub
            End If

            ' Step 3 - Get Dealers for Selected Company
            Dim dvDealers As DataView = LookupListNew.GetParentDealerLookupList(companyId)

            ' Step 4 - When no dealers are configured for company, display message
            If dvDealers.Count = 0 Then
                DisplayMessage(Message.MSG_NO_PARENT_DEALER_FOUND, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If

            ' Step 5  Populate Available Selected Control with nothing on Selected side.
            UsercontrolAvailableSelectedDealers.SetAvailableData(dvDealers, LookupListNew.COL_CODE_NAME, LookupListNew.COL_ID_NAME)
            UsercontrolAvailableSelectedDealers.SetSelectedData(Nothing, LookupListNew.COL_CODE_NAME, LookupListNew.COL_ID_NAME)

            ' Step 6 - Get Months and Populate Dropdown
            '  Dim dvMonths As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            ' dvMonths.Sort = "CODE"
            ' BindListControlToDataView(Me.MonthDropDown, dvMonths, , , True, False)
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            MonthDropDown.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True,
              .SortFunc = AddressOf PopulateOptions.GetCode
           })
            ' Step 6 - Get Years and Populate Dropdown
            'Dim dvYears As DataView = AccountingCloseInfo.GetClosingYears(companyId)
            '  BindListTextToDataView(YearDropDown, dvYears, , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = companyId

            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            YearDropDown.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .BlankItemValue = "0",
            .ValueFunc = AddressOf PopulateOptions.GetCode
                  })
            Page.ClientScript.RegisterStartupScript([GetType](), "Startup12345", "<script>enableDisablePeriod();</script>")
            btnGenRpt.Enabled = True
        End Sub
#End Region

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheReportExtractInputControl.GetSchedDate()
                If State.MyBO.IsDirty Then
                    State.MyBO.CreateReportRequest(scheduleDate)

                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        DisplayMessage(Message.MSG_Email_not_configured, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT, , True)
                    Else
                        DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_ALERT, , True)
                    End If

                    btnGenRpt.Enabled = False
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

    End Class
End Namespace