Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.DownLoad

Namespace Reports
    Partial Class ClaimDetailByExtendedStatusByDealerReportForm
        Inherits ElitaPlusPage

#Region "Constants"
        Private Const RPT_FILENAME As String = "ClaimDetailByExtendedStatus"
        Public Const ALL As String = "*"
        Private Const PAGETITLE As String = "Claim_Detail_By_Extended_Status_By_Dealer"
        Public Const PAGETAB As String = "REPORTS"

#End Region

#Region "Properties"

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
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
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
                    Me.AddCalendar(Me.BtnClaimCreatedBeginDate, Me.moClaimCreatedBeginDateText)
                    Me.AddCalendar(Me.BtnClaimCreatedEndDate, Me.moClaimCreatedEndDateText)
                    Me.AddCalendar(Me.BtnClaimClosedBeginDate, Me.moClaimClosedBeginDateText)
                    Me.AddCalendar(Me.BtnClaimClosedEndDate, Me.moClaimClosedEndDateText)
                    Me.AddCalendar(Me.BtnExtStatusBeginDate, Me.moExtStatusBeginDateText)
                    Me.AddCalendar(Me.BtnExtStatusEndDate, Me.moExtStatusEndDateText)

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
            PopulateDealerDropDown()
            PopulateExtClaimStatusDropDown()
        End Sub
        Private Sub PopulateDealerDropDown()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                oListContext.CompanyId = _company
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If Not oDealerList Is Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If
                End If
            Next
            Me.cboDealer.Populate(oDealerList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

        End Sub
        Private Sub PopulateExtClaimStatusDropDown()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oExtendedStatusList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ExtendedStatusByCompanyGroup", context:=oListContext)
            Me.cboExtendedStatus.Populate(oExtendedStatusList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

        End Sub

#End Region

#Region "Clear"

        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(moClaimCreatedBeginDateLabel)
                Me.ClearLabelErrSign(moClaimCreatedEndDateLabel)
                Me.ClearLabelErrSign(moClaimClosedBeginDateLabel)
                Me.ClearLabelErrSign(moClaimClosedEndDateLabel)
                Me.ClearLabelErrSign(moExtStatusBeginDateLabel)
                Me.ClearLabelErrSign(moExtStatusEndDateLabel)
                Me.ClearLabelErrSign(moDealerLabel)
                Me.ClearLabelErrSign(moExtendedLabel)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

        Private Sub GenerateReport()
            Dim reportParams As New System.Text.StringBuilder
            Dim langCode As String = ElitaPlusIdentity.Current.ActiveUser.LanguageCode

            Dim selectedDealerId As Guid = Me.GetSelectedItem(Me.cboDealer)
            Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)

            Dim selectedExtendedStatusId As Guid = Me.GetSelectedItem(Me.cboExtendedStatus)
            Dim dvExtendedStatus As DataView = LookupListNew.GetExtendedStatusByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim extendedStatusCode As String = LookupListNew.GetCodeFromId(dvExtendedStatus, selectedExtendedStatusId)

            If selectedDealerId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(moDealerLabel)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            If moClaimCreatedBeginDateText.Text.Equals(String.Empty) And moClaimCreatedEndDateText.Text.Equals(String.Empty) And
               moClaimClosedBeginDateText.Text.Equals(String.Empty) And moClaimClosedEndDateText.Text.Equals(String.Empty) And
               moExtStatusBeginDateText.Text.Equals(String.Empty) And moExtStatusEndDateText.Text.Equals(String.Empty) And
               selectedExtendedStatusId.Equals(Guid.Empty) Then
                Throw New GUIException(Message.MSG_MAX_LIMIT_EXCEEDED_GENERIC, Message.MSG_MAX_LIMIT_EXCEEDED_GENERIC)
            End If

            If (Not moClaimCreatedBeginDateText.Text.Equals(String.Empty) And Not moClaimCreatedEndDateText.Text.Equals(String.Empty)) Then
                ReportExtractBase.ValidateBeginEndDate(moClaimCreatedBeginDateLabel, moClaimCreatedBeginDateText.Text, moClaimCreatedEndDateLabel, moClaimCreatedEndDateText.Text)
            Else
                If (Not moClaimCreatedBeginDateText.Text.Equals(String.Empty) And moClaimCreatedEndDateText.Text.Equals(String.Empty)) Then
                    ElitaPlusPage.SetLabelError(moClaimCreatedEndDateLabel)
                    Throw New GUIException(Message.MSG_INVALID_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_RANGE)
                End If
                If (moClaimCreatedBeginDateText.Text.Equals(String.Empty) And Not moClaimCreatedEndDateText.Text.Equals(String.Empty)) Then
                    ElitaPlusPage.SetLabelError(moClaimCreatedBeginDateLabel)
                    Throw New GUIException(Message.MSG_INVALID_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_RANGE)
                End If
            End If

            If (Not moClaimClosedBeginDateText.Text.Equals(String.Empty) And Not moClaimClosedEndDateText.Text.Equals(String.Empty)) Then
                ReportExtractBase.ValidateBeginEndDate(moClaimClosedBeginDateLabel, moClaimClosedBeginDateText.Text, moClaimClosedEndDateLabel, moClaimClosedEndDateText.Text)
            Else
                If (Not moClaimClosedBeginDateText.Text.Equals(String.Empty) And moClaimClosedEndDateText.Text.Equals(String.Empty)) Then
                    ElitaPlusPage.SetLabelError(moClaimClosedEndDateLabel)
                    Throw New GUIException(Message.MSG_INVALID_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_RANGE)
                End If
                If (moClaimClosedBeginDateText.Text.Equals(String.Empty) And Not moClaimClosedEndDateText.Text.Equals(String.Empty)) Then
                    ElitaPlusPage.SetLabelError(moClaimClosedBeginDateLabel)
                    Throw New GUIException(Message.MSG_INVALID_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_RANGE)
                End If
            End If

            If (Not moExtStatusBeginDateText.Text.Equals(String.Empty) And Not moExtStatusEndDateText.Text.Equals(String.Empty)) Then
                ReportExtractBase.ValidateBeginEndDate(moExtStatusBeginDateLabel, moExtStatusBeginDateText.Text, moExtStatusEndDateLabel, moExtStatusEndDateText.Text)
            Else
                If (Not moExtStatusBeginDateText.Text.Equals(String.Empty) And moExtStatusEndDateText.Text.Equals(String.Empty)) Then
                    ElitaPlusPage.SetLabelError(moExtStatusEndDateLabel)
                    Throw New GUIException(Message.MSG_INVALID_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_RANGE)
                End If
                If (moExtStatusBeginDateText.Text.Equals(String.Empty) And Not moExtStatusEndDateText.Text.Equals(String.Empty)) Then
                    ElitaPlusPage.SetLabelError(moExtStatusBeginDateLabel)
                    Throw New GUIException(Message.MSG_INVALID_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_RANGE)
                End If
            End If

            If Not selectedExtendedStatusId.Equals(Guid.Empty) Then
                If ((moExtStatusBeginDateText.Text.Equals(String.Empty)) And (moExtStatusEndDateText.Text.Equals(String.Empty))) Then
                    ElitaPlusPage.SetLabelError(moExtStatusBeginDateLabel)
                    Throw New GUIException(Message.MSG_INVALID_DATE_RANGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_DATE_RANGE)
                End If
            End If

            If ((Not moExtStatusBeginDateText.Text.Equals(String.Empty)) And (Not moExtStatusEndDateText.Text.Equals(String.Empty))) Then
                If selectedExtendedStatusId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moExtendedLabel)
                    Throw New GUIException(Message.MSG_INVALID_EXTENDED_STATUS, Assurant.ElitaPlus.Common.ErrorCodes.GUI_EXTENDED_STATUS_MUST_BE_SELECTED_ERR)
                End If
            End If

            reportParams.AppendFormat("pi_dealer_code => '{0}',", dealerCode)
            reportParams.AppendFormat("pi_claim_created_begin_date => '{0}',", moClaimCreatedBeginDateText.Text)
            reportParams.AppendFormat("pi_claim_created_end_date => '{0}',", moClaimCreatedEndDateText.Text)
            reportParams.AppendFormat("pi_claim_closed_begin_date => '{0}',", moClaimClosedBeginDateText.Text)
            reportParams.AppendFormat("pi_claim_closed_end_date => '{0}',", moClaimClosedEndDateText.Text)
            reportParams.AppendFormat("pi_ext_status_begin_date => '{0}',", moExtStatusBeginDateText.Text)
            reportParams.AppendFormat("pi_ext_status_end_date => '{0}',", moExtStatusEndDateText.Text)
            reportParams.AppendFormat("pi_extended_status_code => '{0}',", extendedStatusCode)
            reportParams.AppendFormat("pi_language_code => '{0}'", langCode)

            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "CLAIM_DETAIL_BY_EXTENDED_STATUS_BY_DEALER")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "r_claimdetailbyExtStatusByDlr.Report")
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
