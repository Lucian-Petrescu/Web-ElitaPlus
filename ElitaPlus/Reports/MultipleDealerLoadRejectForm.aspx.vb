Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Collections.Generic
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class MultipleDealerLoadRejectForm
        Inherits ElitaPlusPage


#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String

        End Structure

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

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "MULTIPLE_DEALERLOAD_REJECTS"
        Private Const RPT_FILENAME As String = "MultipleDealerLoadRejects"
        Private Const RPT_FILENAME_EXPORT As String = "MultipleDealerLoadRejects-Exp"
        Private Const RPT_FILENAME_REJERR_EXPORT As String = "MultipleDealerLoadRejErrors-Exp"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const PAGETITLE As String = "MULTIPLE_FILE_REJECTIONS"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "MULTIPLE_FILE_REJECTIONS"

        Private Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Public Const EMPTY_GRID_ID As String = "00000000000000000000000000000000"

        Private Const CERT_FILE_TYPE_CODE As String = "CERT"
        Private Const PYMT_FILE_TYPE_CODE As String = "PAYM"
        Private Const ERROR_EXPORT_TYPE As String = "ERREXPTYP"
        Private Const RECONCILE_REJECTS As String = "ReconcileRejects"
        Private Const REJECTS As String = "Rejects"
        Private Const Dealer_Type_ESC As String = "ESC"
        Private Const Dealer_Type_VSC As String = "VSC"

        Private Const FILE_TYP_DEALER As String = "DLR_FILE"
        Private Const FILE_TYP_PAYMENT As String = "PYMT_FILE"
        Public Const WILDCARD As Char = "%"

#End Region

#Region "variables"
        'Private moReportFormat As ReportCeBaseForm.RptFormat
        Public endDate As String
        Public beginDate As String
#End Region

#Region "Properties"
        'Public ReadOnly Property TheReportCeInputControl() As ReportCeInputControl
        '    Get
        '        If moReportCeInputControl Is Nothing Then
        '            moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
        '        End If
        '        Return moReportCeInputControl
        '    End Get
        'End Property

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

        Public ReadOnly Property FileAvailableSelected() As Generic.UserControlAvailableSelected_New
            Get
                If AvailableSelectedFiles Is Nothing Then
                    AvailableSelectedFiles = CType(FindControl("AvailableSelectedFiles"), Generic.UserControlAvailableSelected_New)
                End If
                Return AvailableSelectedFiles
            End Get
        End Property
#End Region

#Region "Handlers-DropDown/Textbox/ Radiobtn Events"

        Private Sub OnDealerDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
            Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                '       rdealer.Checked = False
                'PopulateDealerDropDown()
                PopulateFileName()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub OnCompanyDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
          Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                '      rdealer.Checked = True
                PopulateDealerDropDown()
                PopulateFileName()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub txtBeginDate_TextChanged(sender As Object, e As System.EventArgs) Handles txtBeginDate.TextChanged
            Try
                PopulateFileName()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub txtEndDate_TextChanged(sender As Object, e As EventArgs) Handles txtEndDate.TextChanged
            Try
                PopulateFileName()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub rAllRej_CheckedChanged(sender As Object, e As EventArgs) Handles rAllRej.CheckedChanged
            Try
                PopulateFileName()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub rReconcileRej_CheckedChanged(sender As Object, e As EventArgs) Handles rReconcileRej.CheckedChanged
            Try
                PopulateFileName()
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            If Not String.IsNullOrEmpty(Request.QueryString("rid")) Then

            End If
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
                    InitializeForm()
                    EnableDisableOptions(True)
                    AddCalendar(BtnBeginDate, txtBeginDate)
                    AddCalendar(BtnEndDate, txtEndDate)

                End If

                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(lblEndDate)
                ClearLabelErrSign(lblBeginDate)
                ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
                ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
                ClearLabelErrSign(lblRptlayout)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub JavascriptCalls()
            'rdealer.Attributes.Add("onclick", "Return ToggleDualDropDownsSelection('" + DealerMultipleDrop.CodeDropDown.ClientID + "','" + DealerMultipleDrop.DescDropDown.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")

            'txtBeginDate.Attributes.Add("onchange", "javascript:PopulateFileNames();")
            'txtEndDate.Attributes.Add("onchange", "javascript:PopulateFileNames();")

            'rAllRej.Attributes.Add("onclick", "return DisableDropDownSelection('" + dpRptType.ClientID + "'," + "false);")
            'rAllErr.Attributes.Add("onclick", "return DisableDropDownSelection('" + dpRptType.ClientID + "'," + "false);")
            'rReconcileRej.Attributes.Add("onclick", "return DisableDropDownSelection('" + dpRptType.ClientID + "'," + "true);")

        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, ALL + " " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, )
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub
        Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(True, DealerMultipleDrop.MODES.NEW_MODE, True, dv, ALL + " " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True) ', " document.forms[0]." + rdealer.ClientID + ".checked = false; "
        End Sub

        Private Sub InitializeForm()
            rReconcileRej.Checked = True
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            PopulateFileTypeDropDown()
            PopulateReconcileRptType()
            'TheReportCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT)
            JavascriptCalls()
            TheReportExtractInputControl.SetExportOnly()
        End Sub
        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(DOCTITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                If New Guid(ddlFileType.SelectedValue).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_FILE_TYPE, FILE_TYP_PAYMENT)) Then
                    GeneratePaymentReport()
                Else
                    GenerateDealerReport()
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub PopulateFileName()

            Dim dealercode, rejectionType As String

            If Not UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) AndAlso Not DealerMultipleDrop.SelectedGuid.Equals(Guid.Empty) _
                AndAlso (txtBeginDate.Text.Trim.ToString <> String.Empty And txtEndDate.Text.Trim.ToString <> String.Empty) _
                AndAlso Not ddlFileType.SelectedValue.Equals(Guid.Empty) Then

                dealercode = DealerMultipleDrop.SelectedCode
                ReportExtractBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
                endDate = ReportExtractBase.FormatDate(lblEndDate, txtEndDate.Text)
                beginDate = ReportExtractBase.FormatDate(lblBeginDate, txtBeginDate.Text)

                If rAllRej.Checked = True Then
                    rejectionType = "Rejects"
                Else
                    rejectionType = "ReconcileRejects"
                End If

                AvailableSelectedFiles.ClearLists()
                If New Guid(ddlFileType.SelectedValue).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_FILE_TYPE, FILE_TYP_PAYMENT)) Then
                    AvailableSelectedFiles.SetAvailableData(DealerFileProcessed.getDealerFileNamesBwtnDateRange(UserCompanyMultipleDrop.SelectedGuid, dealercode, beginDate, endDate, PYMT_FILE_TYPE_CODE, rejectionType), LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                Else
                    AvailableSelectedFiles.SetAvailableData(DealerFileProcessed.getDealerFileNamesBwtnDateRange(UserCompanyMultipleDrop.SelectedGuid, dealercode, beginDate, endDate, CERT_FILE_TYPE_CODE, rejectionType), LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                End If

            End If

        End Sub

        Public Sub PopulateReconcileRptType()
            Dim rptType As ListItem() = CommonConfigManager.Current.ListManager.GetList("AUTO_REJ_ERR_TYPE", Thread.CurrentPrincipal.GetLanguageCode())
            dpRptType.Populate(rptType, New PopulateOptions() With
                                                {
                                                    .AddBlankItem = False
                                                })
        End Sub
        Public Sub PopulateFileTypeDropDown()
            Dim fileType As ListItem() = CommonConfigManager.Current.ListManager.GetList("FILE_TYP", Thread.CurrentPrincipal.GetLanguageCode())
            ddlFileType.Populate(fileType, New PopulateOptions() With
                                                {
                                                    .AddBlankItem = False
                                                })
        End Sub
        Private Sub GenerateDealerReport()
            Dim reportParams As New System.Text.StringBuilder
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            Dim compCode As String = LookupListNew.GetCodeFromId(LookupListCache.LK_COMPANIES, oCompanyId)
            Dim oCompanyGrpId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            Else
                compCode = UserCompanyMultipleDrop.SelectedCode
            End If

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode

            Dim FileNames, ReportType, DealerType As String

            'Validating the month and year
            If ((Not txtBeginDate.Text.Equals(String.Empty) And txtEndDate.Text.Equals(String.Empty)) Or (txtBeginDate.Text.Equals(String.Empty) And Not txtEndDate.Text.Equals(String.Empty))) Then
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
            Else
                ReportExtractBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
                endDate = ReportExtractBase.FormatDate(lblEndDate, txtEndDate.Text)
                beginDate = ReportExtractBase.FormatDate(lblBeginDate, txtBeginDate.Text)
            End If

            'Validating the Dealer selection            
            If dealerID.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            If AvailableSelectedFiles.SelectedListListBox.Items.Count = 0 Then
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ATLEAST_ONE_FILE_SHLD_BE_SELECTED_ERR)
            Else
                FileNames = AvailableSelectedFiles.SelectedListwithCommaSep
            End If

            If AvailableSelectedFiles.AvailableList.Count = 0 Then
                FileNames = ALL
                FileNames = WILDCARD
            Else
                FileNames = AvailableSelectedFiles.SelectedListwithCommaSep

            End If

            If rReconcileRej.Checked = True Then
                ReportType = RECONCILE_REJECTS
            Else
                ReportType = REJECTS
            End If

            If ElitaPlusPage.GetSelectedItem(dpRptType).Equals(Guid.Empty) Then
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_RPTTYPE_MUST_BE_SELECTED_ERR)
            End If

            If (LookupListNew.GetDealerTypeId(Authentication.CurrentUser.CompanyGroup.Id).Count > 1) Then
                DealerType = Dealer_Type_ESC
            Else
                DealerType = Dealer_Type_VSC
            End If

            reportParams.AppendFormat("pi_company_code => '{0}',", compCode)
            reportParams.AppendFormat("pi_dealer => '{0}',", dealerCode)
            reportParams.AppendFormat("pi_file_names => '{0}',", FileNames)
            reportParams.AppendFormat("pi_extract_type => '{0}',", ReportType)
            reportParams.AppendFormat("pi_dealertype => '{0}',", DealerType)
            reportParams.AppendFormat("pi_language_id => '{0}',", DALBase.GuidToSQLString(Thread.CurrentPrincipal.GetLanguageId()))
            reportParams.AppendFormat("pi_start_date => '{0}',", Date.Parse(txtBeginDate.Text.Trim()).ToString("MMddyyyy"))
            reportParams.AppendFormat("pi_end_date => '{0}',", Date.Parse(txtEndDate.Text.Trim()).ToString("MMddyyyy"))
            If ddlFileType.SelectedItem.Text = "Dealer File" Then
                reportParams.AppendFormat("pi_file_type_code => '{0}',", CERT_FILE_TYPE_CODE)
            Else
                reportParams.AppendFormat("pi_file_type_code => '{0}',", PYMT_FILE_TYPE_CODE)
            End If

            If dpRptType.SelectedValue = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListCache.LK_REPORT_ERR_REJ_TYPE, Thread.CurrentPrincipal.GetLanguageId(), True), ERROR_EXPORT_TYPE).ToString Then
                reportParams.AppendFormat("pi_report_layout => '{0}'", "Error Export")
            Else
                reportParams.AppendFormat("pi_report_layout => '{0}'", "Reject Export")
            End If

            'New Email Functionality
            State.MyBO = New ReportRequests
            State.ForEdit = True

            PopulateBOProperty(State.MyBO, "ReportType", "MultipleDealerLoad" + ReportType)
            PopulateBOProperty(State.MyBO, "ReportProc", "R_MultipleDealerLoadRejections.Report")
            PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
            PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            ScheduleReport()
        End Sub

        Private Sub GeneratePaymentReport()
            Dim reportParams As New System.Text.StringBuilder
            Dim includebypassrecords As String = "Y"
            Dim FileNames As String
            Dim ReportType As String

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            ClearLabelsErrSign()

            'Validating the Dealer selection            
            If dealerID.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            If (txtBeginDate.Text.Equals(String.Empty)) Then
                ElitaPlusPage.SetLabelError(lblBeginDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
            End If

            If (txtEndDate.Text.Equals(String.Empty)) Then
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
            End If

            'Validating the month and year
            ReportExtractBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
            endDate = ReportExtractBase.FormatDate(lblEndDate, txtEndDate.Text)
            beginDate = ReportExtractBase.FormatDate(lblBeginDate, txtBeginDate.Text)

            If ElitaPlusPage.GetSelectedItem(dpRptType).Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(lblRptlayout)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_RPTTYPE_MUST_BE_SELECTED_ERR)
            End If

            If AvailableSelectedFiles.SelectedListListBox.Items.Count = 0 Then
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ATLEAST_ONE_FILE_SHLD_BE_SELECTED_ERR)
            ElseIf FileNames = ALL Then
                FileNames = WILDCARD
            Else
                FileNames = AvailableSelectedFiles.SelectedListwithCommaSep
            End If

            If rReconcileRej.Checked = True Then
                ReportType = RECONCILE_REJECTS
            Else
                ReportType = REJECTS
            End If

            If Not moInclBypassedRecCheck.Checked Then
                includebypassrecords = "N"
            End If


            reportParams.AppendFormat("pi_dealer => '{0}',", dealerCode)
            reportParams.AppendFormat("pi_file_names => '{0}',", FileNames)
            reportParams.AppendFormat("pi_extract_type => '{0}',", ReportType)
            reportParams.AppendFormat("pi_language_id => '{0}',", DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
            reportParams.AppendFormat("pi_bypass => '{0}'", includebypassrecords)

            ''New email functionality
            State.MyBO = New ReportRequests
            State.ForEdit = True
            PopulateBOProperty(State.MyBO, "ReportType", "MULTIPLE_PYMT_LOAD_REJECTIONS")
            If ElitaPlusPage.GetSelectedItem(dpRptType).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPORT_ERR_REJ_TYPE, ERROR_EXPORT_TYPE)) Then
                PopulateBOProperty(State.MyBO, "ReportProc", "R_DealerPymtErrorExp.Report")
            Else
                PopulateBOProperty(State.MyBO, "ReportProc", "R_DealerPymtRejections.Report")
            End If
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
                    Try 'To avoid write to database error when user selects 2 report types (reject error, export error)
                        If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                            DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                        Else
                            DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                        End If
                    Catch ex As Exception
                    End Try

                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ddlFileType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFileType.SelectedIndexChanged
            EnableDisableOptions(True)
            If New Guid(ddlFileType.SelectedValue).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_FILE_TYPE, FILE_TYP_PAYMENT)) Then
                moInclBypassedRecCheck.Visible = True
            End If
            PopulateFileName()
        End Sub

        Private Sub EnableDisableOptions(IsVisible As Boolean)
            lblAllrej.Visible = IsVisible
            lblAllRejStar.Visible = IsVisible
            rAllRej.Visible = IsVisible
            lblReconcileRej.Visible = IsVisible
            lblReconcileRejStar.Visible = IsVisible
            rReconcileRej.Visible = IsVisible
        End Sub

    End Class
End Namespace

