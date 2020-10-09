Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Partial Class NewCertificatesChinaReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Public Const PAGETITLE As String = "CHINA_REINSURANCE_REPORTS"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "CHINA_REINSURANCE_REPORTS"

        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NOTHING_SELECTED As Int16 = -1


#End Region

#Region "Properties"

        Public ReadOnly Property DealerAvailableSelected() As Generic.UserControlAvailableSelected_New
            Get
                If DealerAvailableSelected Is Nothing Then

                    DealerAvailableSelected = DirectCast(AvailableSelectedDealers, Generic.UserControlAvailableSelected_New)
                End If
                Return DealerAvailableSelected
            End Get
        End Property

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property
#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'Protected WithEvents moReportCeInputControl As ReportCeInputControl
        ''Protected WithEvents ErrorCtrl As ErrorController
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Protected WithEvents ddlReportType As Global.System.Web.UI.WebControls.DropDownList
        Protected WithEvents ddlCoverage As Global.System.Web.UI.WebControls.DropDownList
        Protected WithEvents ErrorCtrl As ErrorController

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

#Region "Handlers-DropDown"



        Protected Sub rdealer_CheckedChanged(sender As Object, e As EventArgs) Handles rdealer.CheckedChanged
            Try
                PopulateDealerDropDown()
                ControlMgr.SetVisibleControl(Me, rbDealerTypeESC, True)
                ControlMgr.SetVisibleControl(Me, rbDealerTypeHW, True)
                ControlMgr.SetVisibleControl(Me, esclabel, True)
                ControlMgr.SetVisibleControl(Me, hwlabel, True)
                DealerAvailableSelected.SelectedListListBox.Items.Clear()
                AvailableSelectedDealers.Visible = False
                If IsPostBack Then
                    btnGenRpt.Enabled = True
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            Try
                If Not IsPostBack Then
                    'Me.SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    UpdateBreadCrum()
                    InitializeForm()

                    'Date Calendars
                    AddCalendar(btnBeginDate, moBeginDateText)
                    AddCalendar(btnEndDate, moEndDateText)
                Else
                    ClearErrLabels()
                End If


            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(mobegindatelabel)
            ClearLabelErrSign(moenddatelabel)
            ClearLabelErrSign(lblCoverage)

        End Sub

#End Region

#Region "Populate"

        Public Sub PopulateDealerDropDown()
            Dim dealerType As String = If(rbDealerTypeESC.Checked, "ESC", "Home Warranty")

            DealerAvailableSelected.ClearLists()

            DealerAvailableSelected.SetAvailableData(LookupListNew.GetDealerLookupListByDealerType(ElitaPlusIdentity.Current.ActiveUser.Companies, dealerType), LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)

        End Sub
        Sub PopulateDropDowns()

            'Me.BindListTextToDataView(Me.ddlReportType, LookupListNew.DropdownLookupList("ACCIDENTAL_PROTECTION_REPORT_TYPE", Authentication.LangId), , "CODE", False)
            'Me.BindListControlToDataView(Me.ddlCoverage, LookupListNew.GetCoverageTypeByCompanyGroupLookupList(Authentication.LangId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)

            Dim ReportTypes As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="ACCIDENTAL_PROTECTION_REPORT_TYPE",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            ddlReportType.Populate(ReportTypes.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .ValueFunc = AddressOf .GetCode
                                    })



            Dim CoverageTypes As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByCompanyGroup",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode(),
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

            ddlCoverage.Populate(CoverageTypes.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        End Sub

        Private Sub InitializeForm()

            PopulateDealerDropDown()
            PopulateDropDowns()

            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)

            moBeginDateText.Text = GetDateFormattedString(t)

            moEndDateText.Text = GetDateFormattedString(Date.Now)
            rdealer.Checked = True

            AvailableSelectedDealers.Visible = False
            rbCoverage.Checked = True
            rdealer.Checked = True
            RadiobuttonTotalsOnly.Checked = True

        End Sub

#End Region

#Region "Handlers-Buttons"
        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub
        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub GenerateReport()
            Dim reportParams As New System.Text.StringBuilder
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim langId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim strRrpt_Type As String = ddlReportType.SelectedValue
            Dim companygrpId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim selectedDetailType As String

            Dim selectedCoverageId As String = GuidControl.GuidToHexString(GetSelectedItem(ddlCoverage))

            Dim selectedDealerType As String
            Dim dealerIds As String
            Dim selectedDealerId As Guid

            ReportExtractBase.ValidateBeginEndDate(mobegindatelabel, moBeginDateText.Text, moenddatelabel, moEndDateText.Text)
            Dim endDate As String = ReportExtractBase.FormatDate(moenddatelabel, moEndDateText.Text)
            Dim beginDate As String = ReportExtractBase.FormatDate(mobegindatelabel, moBeginDateText.Text)


            reportParams.AppendFormat("pi_user_key => '{0}',", userId)
            reportParams.AppendFormat("pi_company_group_id => '{0}',", companygrpId)
            reportParams.AppendFormat("pi_Start_Date => '{0}',", beginDate)
            reportParams.AppendFormat("pi_End_Date => '{0}',", endDate)
            reportParams.AppendFormat("pi_report_type => '{0}',", strRrpt_Type)


            If (RadiobuttonDetail.Checked.Equals(True)) Then
                selectedDetailType = "Y"
                reportParams.AppendFormat("pi_detail => '{0}',", selectedDetailType)
            Else
                selectedDetailType = "N"
                reportParams.AppendFormat("pi_detail => '{0}',", selectedDetailType)
            End If


            If rdealer.Checked Then

                reportParams.AppendFormat("pi_dealer_id => '{0}',", "00000000000000000000000000000000")
            Else
                If DealerAvailableSelected.SelectedListListBox.Items.Count = 0 Then
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                Else
                    dealerIds = DealerAvailableSelected.SelectedListValuewithCommaSep
                End If

                reportParams.AppendFormat("pi_dealer_id => '{0}',", dealerIds)
            End If

            If rbCoverage.Checked Then
                reportParams.AppendFormat("pi_coverage_id => '{0}',", "00000000000000000000000000000000")
            Else
                If selectedCoverageId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(lblCoverage)
                    Throw New GUIException(Message.MSG_INVALID_COVERAGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COVERAGE_TYPE_MUST_BE_SELECTED_ERR)
                End If
                reportParams.AppendFormat("pi_coverage_id => '{0}',", selectedCoverageId)
            End If


            If (rdealer.Checked.Equals(True) AndAlso rbDealerTypeHW.Checked) Then
                selectedDealerType = "HW"
                reportParams.AppendFormat("pi_dealer_type => '{0}'", selectedDealerType)
            ElseIf (rdealer.Checked.Equals(True) AndAlso rbDealerTypeESC.Checked) Then
                selectedDealerType = "ESC/Mobile"
                reportParams.AppendFormat("pi_dealer_type => '{0}'", selectedDealerType)

            ElseIf (rdealer2.Checked.Equals(True) AndAlso rbDealerTypeHW.Checked) Then
                selectedDealerType = "HW"
                reportParams.AppendFormat("pi_dealer_type => '{0}'", selectedDealerType)

            ElseIf (rdealer2.Checked.Equals(True) AndAlso rbDealerTypeESC.Checked) Then
                selectedDealerType = "ESC/Mobile"
                reportParams.AppendFormat("pi_dealer_type => '{0}'", selectedDealerType)

            Else
                If LookupListNew.GetCodeFromId(LookupListCache.LK_DEALER_TYPE, Dealer.GetDealerTypeId(selectedDealerId)) = "5" Then
                    selectedDealerType = "HW"
                    reportParams.AppendFormat("pi_dealer_type => '{0}'", selectedDealerType)
                Else
                    selectedDealerType = "ESC/Mobile"
                    reportParams.AppendFormat("pi_dealer_type => '{0}'", selectedDealerType)
                End If
            End If

            State.MyBO = New ReportRequests
            State.ForEdit = True
            If String.Compare(strRrpt_Type, "NEW").Equals(0) Then
                PopulateBOProperty(State.MyBO, "ReportType", "New Certificates Report")
            ElseIf String.Compare(strRrpt_Type, "CLAIM").Equals(0) Then
                PopulateBOProperty(State.MyBO, "ReportType", "New Claims report")
            Else
                PopulateBOProperty(State.MyBO, "ReportType", "New Cancellations Report")
            End If
            PopulateBOProperty(State.MyBO, "ReportProc", "R_Accidental_Protection_China.Report")
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

                Else
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region


        Protected Sub rdealer2_CheckedChanged(sender As Object, e As EventArgs)

            AvailableSelectedDealers.Visible = True
            PopulateDealerDropDown()

            If IsPostBack Then
                btnGenRpt.Enabled = True
            End If
        End Sub

        Private Sub rbDealerTypeHW_CheckedChanged(sender As Object, e As EventArgs) Handles rbDealerTypeHW.CheckedChanged
            If rdealer2.Checked = True Then
                PopulateDealerDropDown()
            End If
            If IsPostBack Then
                btnGenRpt.Enabled = True
            End If
        End Sub

        Private Sub rbDealerTypeESC_CheckedChanged(sender As Object, e As EventArgs) Handles rbDealerTypeESC.CheckedChanged
            If rdealer2.Checked = True Then
                PopulateDealerDropDown()
            End If
            If IsPostBack Then
                btnGenRpt.Enabled = True
            End If
        End Sub
    End Class

End Namespace
