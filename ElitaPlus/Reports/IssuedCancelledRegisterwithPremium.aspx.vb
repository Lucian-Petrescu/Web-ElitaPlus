Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements
Namespace Reports

    Partial Class IssuedCancelledRegisterwithPremium
        Inherits ElitaPlusPage


#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String

        End Structure

#End Region

#Region "Constants"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const PAGETITLE As String = "Issued / Cancelled Register with Premium"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "Issued / Cancelled Register with Premium"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"

#End Region

#Region "variables"
        Private dtLatestAccountingCloseDate As Date
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            Me.ClearLabelsErrSign()
            ' Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not Me.IsPostBack Then

                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.SetFormTab(PAGETAB)
                    UpdateBreadCrum()

                    InitializeForm()
                    ' JavascriptCalls()
                End If
                If rdealer.Checked Then DealerMultipleDrop.SelectedIndex = NOTHING_SELECTED
                Me.InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(lblMonth)
                Me.ClearLabelErrSign(lblYear)
                Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
                Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId

            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            moYearList.Populate(YearListLkl, New PopulateOptions() With
            {
              .AddBlankItem = True,
              .BlankItemValue = "0",
                   .ValueFunc = AddressOf PopulateOptions.GetCode,
                    .SortFunc = AddressOf PopulateOptions.GetDescription
                })

        End Sub

        Private Sub PopulateMonthsDropdown()

            Dim MonthListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            moMonthList.Populate(MonthListLkl, New PopulateOptions() With
            {
              .AddBlankItem = True,
              .SortFunc = AddressOf .GetCode
            })

        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
        End Sub
        Private Sub UpdateBreadCrum()
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

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
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)
            Dim compDesc As String = LookupListNew.GetDescriptionFromId("COMPANIES", oCompanyId)
            Dim oCompanyGrpId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim beginDate As Date
            Dim langId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId)


            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            Else
                compCode = UserCompanyMultipleDrop.SelectedCode
                compDesc = UserCompanyMultipleDrop.SelectedDesc
            End If

            Dim selectedYear As String = Me.GetSelectedDescription(Me.moYearList)
            Dim selectedMonthID As Guid = Me.GetSelectedItem(Me.moMonthList)
            Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            Dim selectedYearMonth As String = selectedYear & selectedMonth
            Dim SelectedReportOption As String

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid
            Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyId)
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode

            'Validating the month and year
            If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(lblMonth)
                ElitaPlusPage.SetLabelError(lblYear)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
            End If

            Dim selectedDate As String = GetPreciseDate(selectedYear & "/" & selectedMonth & "/01")
            beginDate = Date.Parse(selectedDate, System.Globalization.CultureInfo.InvariantCulture)
            Me.ValidateAccountingCloseDate(beginDate, lblMonth)
            Dim selectedDateString As String = selectedYear & selectedMonth & "01"

            'Validating the Dealer selection
            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If dealerID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            SelectedReportOption = moReportOptionList.SelectedValue

            reportParams.AppendFormat("pi_company => '{0}',", compCode)
            reportParams.AppendFormat("pi_language_id => '{0}',", langId)
            reportParams.AppendFormat("pi_dealer => '{0}',", dealerCode)
            reportParams.AppendFormat("pi_begin_date => '{0}',", selectedDateString)
            reportParams.AppendFormat("pi_report_option => '{0}'", SelectedReportOption)


            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "Issued / Cancelled Register with Premium")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_IssuedCancelledwithPremium.Report")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", reportParams.ToString())
            Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            ScheduleReport()
        End Sub

        Private Sub ValidateAccountingCloseDate(ByVal dtDateToCompare As Date, ByVal lbl As Label)
            If dtDateToCompare > AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, Date.Today) Then
                ElitaPlusPage.SetLabelError(lblMonth)
                ElitaPlusPage.SetLabelError(lblYear)
                Throw New GUIException(Message.MSG_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_MONTH_NOT_YET_COMPLETED_IS_NOT_ALLOWED)
            End If
        End Sub
        Private Function GetPreciseDate(ByVal strInquiringDate As String) As String
            Dim dtInquiringDate As Date = Date.Parse((strInquiringDate), System.Globalization.CultureInfo.InvariantCulture)
            Dim dtInqAccountingCloseDate As Date = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.CompanyId, dtInquiringDate)
            dtInqAccountingCloseDate = dtInqAccountingCloseDate.AddDays(1)
            If dtInqAccountingCloseDate.Month < 10 Then
                Return dtInqAccountingCloseDate.Year & "/0" & dtInqAccountingCloseDate.Month & "/" & dtInqAccountingCloseDate.Day
            Else
                Return dtInqAccountingCloseDate.Year & "/" & dtInqAccountingCloseDate.Month & "/" & dtInqAccountingCloseDate.Day
            End If
        End Function
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

                Else
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
    End Class
End Namespace


