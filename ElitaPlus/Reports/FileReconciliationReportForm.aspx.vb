Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Reports

    Partial Class FileReconciliationReportForm
        Inherits ElitaPlusPage
#Region "Constants"

        Public Const PAGETITLE As String = "FILE_RECONCILIATION"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const ONE_ITEM As Integer = 1
        Private Const NO As String = "N"
        Public Const RPT_FILENAME_WINDOW = "FILE_RECONCILIATION"
        Public Const PAGETAB As String = "REPORTS"
        Private Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"

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
        Private Sub OnCompanyDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
          Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
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

        Protected WithEvents moReportCeInputControl As ReportCeInputControl

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

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.MasterPage.MessageController.Clear_Hide()
            Me.ClearLabelsErrSign()
            Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                    Me.AddCalendar(Me.BtnBeginDate, Me.txtBeginDate)
                    Me.AddCalendar(Me.BtnEndDate, Me.txtEndDate)
                    Me.AddCalendar(Me.BtnCutOffDate, Me.txtCutOffDate)

                    InitializeForm()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(lblEndDate)
                Me.ClearLabelErrSign(lblBeginDate)
                Me.ClearLabelErrSign(lblCutOffDate)
                Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
                Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
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
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, ALL + " " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True)
        End Sub

        Public Sub PopulateFileTypeDropDown()
            Dim fileType As ListItem() = CommonConfigManager.Current.ListManager.GetList("FILE_TYP", Thread.CurrentPrincipal.GetLanguageCode())
            fileType.OrderBy("Code", LinqExtentions.SortDirection.Ascending)
            ddlFileType.Populate(fileType, New PopulateOptions() With
                                              {
                                                .AddBlankItem = False
                                               })
        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
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
            Dim NetworkId As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim includevoidedrecords As String = "N"
            ClearLabelsErrSign()
            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            Dim compCode As String = UserCompanyMultipleDrop.SelectedCode

            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim FileType As String

            If New Guid(ddlFileType.SelectedValue).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_FILE_TYPE, LookupListNew.LK_FILE_TYP_PAYMENT)) Then
                FileType = "P"
                includevoidedrecords = "N"
            Else
                FileType = "D"
                If chkIncVoidedRecords.Checked Then
                    includevoidedrecords = "Y"
                End If
            End If

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

            Dim endDate As String
            Dim beginDate As String
            Dim CutOffDate As String

            'Validating the month and year
            ReportExtractBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
            endDate = ReportExtractBase.FormatDate(lblEndDate, txtEndDate.Text)
            beginDate = ReportExtractBase.FormatDate(lblBeginDate, txtBeginDate.Text)
            If Not (txtCutOffDate.Text.Equals(String.Empty)) Then
                CutOffDate = ReportExtractBase.FormatDate(lblCutOffDate, txtCutOffDate.Text)
            End If

            If ElitaPlusPage.GetSelectedItem(ddlFileType).Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(lblFileType)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_RPTTYPE_MUST_BE_SELECTED_ERR)
            End If


            reportParams.AppendFormat("pi_network_id => '{0}',", NetworkId)
            reportParams.AppendFormat("pi_company_code => '{0}',", compCode)
            reportParams.AppendFormat("pi_dealer_code => '{0}',", dealerCode)
            reportParams.AppendFormat("pi_file_type => '{0}',", FileType)
            reportParams.AppendFormat("pi_begin_date => '{0}',", beginDate)
            reportParams.AppendFormat("pi_end_date => '{0}',", endDate)
            reportParams.AppendFormat("pi_cutoff_date => '{0}',", CutOffDate)
            reportParams.AppendFormat("pi_include_voided_records => '{0}' ", includevoidedrecords)

            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True

            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "FILE_RECONCILIATION")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "r_file_reconciliation.report")
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
                        Me.DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    Else
                        Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    End If
                    btnGenRpt.Enabled = False
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
    End Class
End Namespace
