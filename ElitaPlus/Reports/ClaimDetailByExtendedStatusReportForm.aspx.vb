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
    Partial Class ClaimDetailByExtendedStatusReportForm
        Inherits ElitaPlusPage

#Region "Constants"
        Private Const RPT_FILENAME As String = "ClaimDetailByExtendedStatus"
        Public Const ALL As String = "*"
        Private Const PAGETITLE As String = "Claim_Detail_By_Extended_Status"
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
            Dim t As Date = Date.Now.AddDays(-1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
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
                    Me.AddCalendar(Me.BtnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.moEndDateText)
                Else
                    btnGenRpt.Enabled = True
                    If (Me.moClmExtStatusList.SelectedValue.Equals("SINGLE")) Then
                        cboExtendedStatus.Enabled = True
                    Else
                        cboExtendedStatus.Enabled = False
                        cboExtendedStatus.ClearSelection()
                    End If
                    If (Me.moSvcCtrList.SelectedValue.Equals("SINGLE")) Then
                        cboSvcCtr.Enabled = True
                    Else
                        cboSvcCtr.Enabled = False
                        cboSvcCtr.ClearSelection()
                    End If
                    If (Me.moDealerList.SelectedValue.Equals("SINGLE")) Then
                        cboDealer.Enabled = True
                    Else
                        cboDealer.Enabled = False
                        cboDealer.ClearSelection()
                    End If
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            ShowMissingTranslations(Me.MasterPage.MessageController)

        End Sub

        Private Sub UpdateBreadCrum()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            'Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

#End Region

#Region "Populate"
        Private Sub PopulateDropDowns()
            PopulateDealerDropDown()
            PopulateSvcCtrDropDown()
            PopulateExtCliamStatusDropDown()
            populateClaimAutoApproveDropDown()
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

            'Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
        End Sub
        Private Sub PopulateSvcCtrDropDown()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            Dim oServiceCenterList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            For Each _country As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                oListContext.CountryId = _country
                Dim oServiceCenterListForCountry As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry", context:=oListContext)
                If oServiceCenterListForCountry.Count > 0 Then
                    If Not oServiceCenterList Is Nothing Then
                        oServiceCenterList.AddRange(oServiceCenterListForCountry)
                    Else
                        oServiceCenterList = oServiceCenterListForCountry.Clone()
                    End If
                End If
            Next
            Me.cboSvcCtr.Populate(oServiceCenterList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(Me.cboSvcCtr, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries), , , True)
        End Sub
        Private Sub PopulateExtCliamStatusDropDown()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oExtendedStatusList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ExtendedStatusByCompanyGroup", context:=oListContext)
            Me.cboExtendedStatus.Populate(oExtendedStatusList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(Me.cboExtendedStatus, LookupListNew.GetExtendedStatusByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
        End Sub
        Private Sub populateClaimAutoApproveDropDown()
            Dim oYesNoList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.moClaimAutoApproveDrop.Populate(oYesNoList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(Me.moClaimAutoApproveDrop, LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
        End Sub
#End Region

#Region "Clear"

        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(moBeginDateLabel)
                Me.ClearLabelErrSign(moEndDateLabel)
                Me.ClearLabelErrSign(moDealerLabel)
                Me.ClearLabelErrSign(moSvcCtrLabel)
                Me.ClearLabelErrSign(moExtendedLabel)
                Me.ClearLabelErrSign(moClaimAutoApprovelbl)
                Me.ClearLabelErrSign(lblClaimExtStatusSort)
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
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim langId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Dim selectedDealerId As Guid = Me.GetSelectedItem(Me.cboDealer)
            Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)


            Dim selectedSvcCtrId As Guid = Me.GetSelectedItem(Me.cboSvcCtr)
            Dim dvSvcCtr As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)
            Dim svcCtrCode As String = LookupListNew.GetCodeFromId(dvSvcCtr, selectedSvcCtrId)

            'Dim selectedSvcCtrId As Guid = Me.GetSelectedItem(Me.cboSvcCtr)
            'Dim dvSvcCtr As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            'Dim dvSvcCtr As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)
            'Dim svcCtrCode As String = LookupListNew.GetCodeFromId(dvSvcCtr, selectedSvcCtrId)

            Dim autoapproveid As Guid = Me.GetSelectedItem(Me.moClaimAutoApproveDrop)
            Dim dvautoapprove As DataView = LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            Dim autoapproveDescription As String = LookupListNew.GetDescriptionFromId(dvautoapprove, autoapproveid)

            Dim selectedExtendedStatusId As Guid = Me.GetSelectedItem(Me.cboExtendedStatus)
            Dim dvExtendedStatus As DataView = LookupListNew.GetExtendedStatusByGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim extendedStatusDescription As String = LookupListNew.GetDescriptionFromId(dvExtendedStatus, selectedExtendedStatusId)
            Dim extendedStatusCode As String = LookupListNew.GetCodeFromId(dvExtendedStatus, selectedExtendedStatusId)
            Dim sortBy As String = Me.rdReportSortOrder.SelectedValue
            Dim selectionType As Integer
            Dim endDate As String
            Dim beginDate As String
            Dim createdby As String

            ReportExtractBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportExtractBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportExtractBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)
            reportParams.AppendFormat("pi_user_key => '{0}',", userId)
            reportParams.AppendFormat("pi_language => '{0}',", langId)
            reportParams.AppendFormat("pi_begin_date => '{0}',", beginDate)
            reportParams.AppendFormat("pi_end_date => '{0}',", endDate)

            If Not Me.moDealerList.SelectedValue Is Nothing Then
                If (Not Me.moDealerList.SelectedValue.Equals("SINGLE")) Then
                    dealerCode = moDealerList.SelectedValue
                    reportParams.AppendFormat("pi_dealer => '{0}',", dealerCode)
                Else
                    If selectedDealerId.Equals(Guid.Empty) Then
                        ElitaPlusPage.SetLabelError(moDealerLabel)
                        Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                    End If
                    If (Me.moDealerList.SelectedValue.Equals("SINGLE")) Then
                        reportParams.AppendFormat("pi_dealer => '{0}',", dealerCode)
                    End If
                End If
            End If

            If Not Me.moSvcCtrList.SelectedValue Is Nothing Then
                If (Not Me.moSvcCtrList.SelectedValue.Equals("SINGLE")) Then
                    svcCtrCode = moSvcCtrList.SelectedValue
                    reportParams.AppendFormat("pi_service_center_code => '{0}',", svcCtrCode)
                Else
                    If selectedSvcCtrId.Equals(Guid.Empty) Then
                        ElitaPlusPage.SetLabelError(moSvcCtrLabel)
                        Throw New GUIException(Message.MSG_INVALID_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR)
                    End If
                    If (Me.moSvcCtrList.SelectedValue.Equals("SINGLE")) Then
                        reportParams.AppendFormat("pi_service_center_code => '{0}',", svcCtrCode)
                    End If
                End If
            End If

            If Not Me.moClmExtStatusList.SelectedValue Is Nothing Then
                If (Not Me.moClmExtStatusList.SelectedValue.Equals("SINGLE")) Then
                    extendedStatusDescription = moClmExtStatusList.SelectedValue
                    reportParams.AppendFormat("pi_extended_status => '{0}',", extendedStatusDescription)
                Else
                    If selectedExtendedStatusId.Equals(Guid.Empty) Then
                        ElitaPlusPage.SetLabelError(moExtendedLabel)
                        Throw New GUIException(Message.MSG_INVALID_EXTENDED_STATUS, Assurant.ElitaPlus.Common.ErrorCodes.GUI_EXTENDED_STATUS_MUST_BE_SELECTED_ERR)
                    End If
                    If (Me.moClmExtStatusList.SelectedValue.Equals("SINGLE")) Then
                        reportParams.AppendFormat("pi_extended_status => '{0}',", extendedStatusDescription)
                    End If
                End If
            End If

            If autoapproveid.Equals(Guid.Empty) Then
                autoapproveDescription = ALL
                reportParams.AppendFormat("pi_claim_auto_approve => '{0}',", "*")
                'ElitaPlusPage.SetLabelError(moClaimAutoApprovelbl)
                'Throw New GUIException(Message.MSG_CLAIM_AUTO_APPROVE_MUST_BE_SELECTED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_AUTO_APPROVE_MUST_BE_SELECTED_ERR)
            Else
                reportParams.AppendFormat("pi_claim_auto_approve => '{0}',", autoapproveDescription)
            End If

            reportParams.AppendFormat("pi_sort_by => {0}", sortBy)
            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "CLAIM_DETAIL_BY_EXTENDED_STATUS")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "r_claimdetailbyextendedstatus.Report")
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
