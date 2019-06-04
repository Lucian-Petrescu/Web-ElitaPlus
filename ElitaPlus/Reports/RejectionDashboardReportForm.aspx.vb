Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class RejectionDashboardReportForm
        Inherits ElitaPlusPage


#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String

        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Rejection_Dashboard"
        Private Const RPT_FILENAME_EXPORT As String = "RejectionDashboard-Exp"
        Private Const RPT_FILENAME_PYMT_EXPORT As String = "RejectionDashboard-PymtExp"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Public Const FROMYEAR As Integer = 2014

        Public Const PAGETITLE As String = "Rejection_Dashboard"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "Rejection_Dashboard"

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            Me.ClearLabelsErrSign()
            Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    InitializeForm()
                    JavascriptCalls()
                End If
                If rdealer.Checked Then DealerMultipleDrop.SelectedIndex = NOTHING_SELECTED
                If rbMonths.Checked Then moMonthList.SelectedIndex = NOTHING_SELECTED
                Me.InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(lblMonth)
                Me.ClearLabelErrSign(lblAllMonths)
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
            Dim count As Integer
            Me.moYearList.Items.Add(New ListItem("", ""))
            For count = FROMYEAR To System.DateTime.Now.Year
                Me.moYearList.Items.Add(New ListItem(count.ToString, count.ToString))
            Next
        End Sub

        Private Sub PopulateMonthsDropdown()
            Dim monthLkl As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            Me.moMonthList.Populate(monthLkl, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .SortFunc = AddressOf PopulateOptions.GetCode
                })

            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'dv.Sort = "CODE"
            'BindListControlToDataView(Me.moMonthList, dv, , , True, False)
        End Sub

        Public Sub PopulateFileTypeDropDown()
            ddlFileType.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="FILE_TYP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()).ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = False,
                    .SortFunc = AddressOf .GetDescription
                })
            'BindListControlToDataView(ddlFileType, LookupListNew.GetFileTypeLookupList(Authentication.LangId, False),,, False)
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
            PopulateFileTypeDropDown()
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
            Dim compCode As String
            Dim showCancellationRecords As String
            Dim selectedYear As String
            Dim selectedMonthID As Guid
            Dim selectedMonth As String
            ClearLabelsErrSign()

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            Else
                compCode = UserCompanyMultipleDrop.SelectedCode
            End If

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode

            'Validating the Dealer selection
            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If dealerID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If

            End If

            selectedYear = Me.moYearList.SelectedItem.Text
            If Me.rbMonths.Checked Then
                selectedMonth = ALL
                If selectedYear.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(lblAllMonths)
                    ElitaPlusPage.SetLabelError(lblYear)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                End If
            Else
                'Validating the month and year
                selectedMonthID = Me.GetSelectedItem(Me.moMonthList)
                If (selectedMonthID.Equals(Guid.Empty) AndAlso selectedYear.Equals(String.Empty)) Then
                    ElitaPlusPage.SetLabelError(lblMonth)
                    ElitaPlusPage.SetLabelError(lblYear)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                ElseIf ((Not selectedMonthID.Equals(Guid.Empty) And selectedYear.Equals(String.Empty)) Or (selectedMonthID.Equals(Guid.Empty) And Not selectedYear.Equals(String.Empty))) Then
                    ElitaPlusPage.SetLabelError(lblMonth)
                    ElitaPlusPage.SetLabelError(lblYear)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                End If
                selectedMonth = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
            End If

            reportParams.AppendFormat("pi_company_code => '{0}',", compCode)
            reportParams.AppendFormat("pi_dealer => '{0}',", dealerCode)
            reportParams.AppendFormat("pi_month => '{0}',", selectedMonth)
            reportParams.AppendFormat("pi_year => '{0}',", selectedYear)

            If Not New Guid(ddlFileType.SelectedValue).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_FILE_TYPE, LookupListNew.LK_FILE_TYP_PAYMENT)) Then

                If Me.chkShowCancelRecords.Checked Then
                    showCancellationRecords = YES
                Else
                    showCancellationRecords = NO
                End If
                reportParams.AppendFormat("pi_file_type => '{0}',", "D")
            Else
                showCancellationRecords = String.Empty
                reportParams.AppendFormat("pi_file_type => '{0}',", "P")
            End If

            reportParams.AppendFormat("pi_include_cancelrecords => '{0}',", showCancellationRecords)
            reportParams.AppendFormat("pi_language_id => '{0}'", DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

            ''New email functionality
            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "REJECTION_DASHBOARD")

            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_RejectionDashoardReport.Report")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", ReportParams.ToString())
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
                    Try
                        If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                            Me.DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                        Else
                            Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                        End If
                    Catch ex As Exception
                    End Try

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ddlFileType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFileType.SelectedIndexChanged

            If New Guid(ddlFileType.SelectedValue).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_FILE_TYPE, LookupListNew.LK_FILE_TYP_PAYMENT)) Then
                lblShowCancelRecords.Visible = False
                chkShowCancelRecords.Visible = False
            Else
                lblShowCancelRecords.Visible = True
                chkShowCancelRecords.Visible = True
            End If
        End Sub
    End Class
End Namespace
