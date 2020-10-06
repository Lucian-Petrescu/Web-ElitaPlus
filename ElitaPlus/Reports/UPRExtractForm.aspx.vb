Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Class UPRExtractForm
        Inherits ElitaPlusPage

#Region "Constants"

        Public Const PAGETITLE As String = "UPR_EXTRACT"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const LABEL_OR_A_SINGLE_DEALER As String = "OR A SINGLE DEALER"
        Private Const ONE_ITEM As Integer = 1
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const NO As String = "N"

#End Region

#Region "Parameters"
        Public Structure ReportParams
            Public companyCode As String
            Public selectedYearMonth As String
            Public dealerCode As String
            Public detailCode As String
            Public groupCode As String
            Public addlDAC As String
            Public culturevalue As String
            Public langCode As String
            Public dealerForCur As String
            Public rptCurrency As String
            Public exchangeRateCode As String
        End Structure

#End Region

#Region "Properties"
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl_New)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property

        Public ReadOnly Property UserDealerMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If moUserDealerMultipleDrop Is Nothing Then
                    moUserDealerMultipleDrop = CType(FindControl("moUserDealerMultipleDrop"), MultipleColumnDDLabelControl_New)
                End If
                Return moUserDealerMultipleDrop
            End Get
        End Property

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moReportCeInputControl As ReportCeInputControl

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

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()
            Try
                If Not IsPostBack Then

                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                    InitializeForm()

                End If
                InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Buttons"


        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerGroup()
                PopulateCurrencyDropdown()
                PopulateDealerDropDown()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#End Region

#Region "Populate"
        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                OnFromDrop_Changed(UserCompanyMultipleDrop)
            End If
        End Sub

        Private Sub PopulateDealerDropDown()

            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            UserDealerMultipleDrop.NothingSelected = True
            UserDealerMultipleDrop.SetControl(False,
                                              UserDealerMultipleDrop.MODES.NEW_MODE,
                                              True,
                                              dv,
                                              TranslationBase.TranslateLabelOrMessage(LABEL_OR_A_SINGLE_DEALER),
                                              True,
                                              False,
                                              "ctl00_BodyPlaceHolder_rdealer.checked = false;  ctl00_BodyPlaceHolder_rGroup.checked = false; ctl00_BodyPlaceHolder_cboDealerGroup.selectedIndex = -1; ctl00_BodyPlaceHolder_ddlDealerCurrency.selectedIndex = -1;",
                                              "ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_moMultipleColumnDrop",
                                              "ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_moMultipleColumnDropDesc", "ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_lb_DropDown")

        End Sub

        Private Sub PopulateYearsDropdown()
            ' Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            ' BindListTextToDataView(Me.YearDropDownList, dv, , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            YearDropDownList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .ValueFunc = AddressOf PopulateOptions.GetCode,
            .BlankItemValue = "0"
                  })
        End Sub

        Private Sub PopulateMonthsDropdown()
            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            'BindListControlToDataView(Me.MonthDropDownList, dv, , , True)
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            MonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })

        End Sub

        Private Sub PopulateDealerGroup()
            'BindListControlToDataView(cboDealerGroup, LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))

            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            Dim dealerGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("DealerGroupByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            cboDealerGroup.Populate(dealerGroupLkl, New PopulateOptions() With
             {
            .AddBlankItem = True
             })
        End Sub

        Private Sub PopulateCurrencyDropdown()
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = UserCompanyMultipleDrop.SelectedGuid
            Dim currLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("GetCurrencyByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Dim currTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                             Return li.Code + " " + "(" + li.Translation + ")"
                                                                         End Function
            If Not UserCompanyMultipleDrop.SelectedGuid = Guid.Empty Then
                'Dim dv As DataView = LookupListNew.GetCurrenciesForCompanyandDealersInCompanyLookupList(UserCompanyMultipleDrop.SelectedGuid)
                'BindListControlToDataView(Me.ddlCurrency, dv, , , True)
                ddlCurrency.Populate(currLkl, New PopulateOptions() With
                 {
                 .AddBlankItem = True,
                 .TextFunc = currTextFunc
                 })
                ' BindListControlToDataView(Me.ddlDealerCurrency, dv, , , True)
                ddlDealerCurrency.Populate(currLkl, New PopulateOptions() With
                {
               .AddBlankItem = True,
               .TextFunc = currTextFunc
               })
            Else
                'Dim dv As DataView = LookupListNew.GetCurrenciesForCompanyandDealersInCompanyLookupList(UserCompanyMultipleDrop.SelectedGuid)
                'BindListControlToDataView(Me.ddlCurrency, dv, , , True)
                ddlCurrency.Populate(currLkl, New PopulateOptions() With
                 {
                 .AddBlankItem = True,
                 .TextFunc = currTextFunc
                 })
                'BindListControlToDataView(Me.ddlDealerCurrency, dv, , , True)
                ddlDealerCurrency.Populate(currLkl, New PopulateOptions() With
                {
               .AddBlankItem = True,
               .TextFunc = currTextFunc
               })
            End If
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerGroup()
            rdealer.Checked = True

        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(MonthYearLabel)
                ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
                ClearLabelErrSign(UserDealerMultipleDrop.CaptionLabel)
                ClearLabelErrSign(GroupLabel)
                ClearLabelErrSign(lblCurrency)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

#End Region

        Private Sub GenerateReport()

            Dim reportParams As New System.Text.StringBuilder
            Dim NetworkId As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim selectedYear As String = GetSelectedDescription(YearDropDownList)
            Dim selectedMonthID As Guid = GetSelectedItem(MonthDropDownList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim selectedYearMonth As String = selectedYear & selectedMonth
            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim selectedDealerId As Guid = UserDealerMultipleDrop.SelectedGuid
            Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim selectByGroup As String
            Dim selectedGroupId As Guid = GetSelectedItem(cboDealerGroup)
            Dim compid As Guid = UserCompanyMultipleDrop.SelectedGuid
            Dim dealerForCurId As Guid = Guid.Empty
            Dim rptCurrencyId As Guid = Guid.Empty

            'Def-26227: Added separate condition to validate month, year and combination of both.
            If selectedMonthID.Equals(Guid.Empty) AndAlso selectedYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(MonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_UPR_YEARMONTH_MUST_BE_SELECTED_ERR)

            ElseIf selectedMonthID.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(MonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_UPR_MONTH_MUST_BE_SELECTED_ERR)

            ElseIf selectedYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(MonthYearLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_UPR_YEAR_MUST_BE_SELECTED_ERR)

            End If
            'Def-26227:End

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If


            'Select All Dealers or a particular dealer or only dealers with Currency 
            'or Select All Groups or a particular group
            If (Not rdealer.Checked AndAlso Not rGroup.Checked AndAlso selectedDealerId.Equals(Guid.Empty) AndAlso ddlDealerCurrency.SelectedIndex = 0 AndAlso selectedGroupId.Equals(Guid.Empty)) Then
                Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_GROUP_MUST_BE_SELECTED_ERR)
            End If
            'currency should be selected for every run
            If ddlCurrency.SelectedIndex = 0 Then
                ElitaPlusPage.SetLabelError(lblCurrency)
                Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CURRENCY_MUST_BE_SELECTED_ERR)
            Else
                rptCurrencyId = New Guid(ddlCurrency.SelectedValue)
            End If

            If ddlDealerCurrency.SelectedIndex > 0 Then
                dealerForCurId = New Guid(ddlDealerCurrency.SelectedValue)
            End If

            selectByGroup = NO
            If rdealer.Checked Then
                dealerCode = ALL
                UserDealerMultipleDrop.SelectedIndex = 0
            ElseIf rGroup.Checked Then
                selectByGroup = ALL
                dealerCode = ALL
            End If

            If Not selectedGroupId.Equals(Guid.Empty) Then
                selectByGroup = DALBase.GuidToSQLString(selectedGroupId)
                dealerCode = ALL
            End If

            Dim ExchangeRateErrorMessage As String = ReportRequests.CheckExchaneRate(selectedYearMonth, compCode, dealerCode, selectByGroup, DALBase.GuidToSQLString(dealerForCurId), DALBase.GuidToSQLString(rptCurrencyId))

            If ExchangeRateErrorMessage Is Nothing Then

                reportParams.AppendFormat("pi_network_id => '{0}',", NetworkId)
                reportParams.AppendFormat("pi_reporting_year_month => '{0}',", selectedYearMonth)
                reportParams.AppendFormat("pi_company_code => '{0}',", compCode)
                reportParams.AppendFormat("pi_dealer_code => '{0}',", dealerCode)
                reportParams.AppendFormat("pi_group_id => '{0}',", selectByGroup)
                reportParams.AppendFormat("pi_dealer_with_Cur => '{0}',", DALBase.GuidToSQLString(dealerForCurId))
                reportParams.AppendFormat("pi_currency_id => '{0}'", DALBase.GuidToSQLString(rptCurrencyId))

                State.MyBO = New ReportRequests
                State.ForEdit = True

                PopulateBOProperty(State.MyBO, "ReportType", "UPR_EXTRACT")
                PopulateBOProperty(State.MyBO, "ReportProc", "r_uprextract.report")
                PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
                PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

                ScheduleReport()
            ElseIf ExchangeRateErrorMessage = "100" Then
                Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.CONTRACT_IS_EXPIRED)
            Else
                Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_EXCHANGE_RATE_ERROR)
            End If
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
