Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Public Class ExtendedStatusAgingReportForm
        Inherits ElitaPlusPage
#Region "Constants"

        Public Const PAGETITLE As String = "EXTENDED_STATUS_AGING_DETAIL"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "EXTENDED_STATUS_AGING_DETAIL"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1

       
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
                    SetFormTab(PAGETAB)
                    UpdateBreadCrum()

                    InitializeForm()
                    'JavascriptCalls()

                End If
                If rDealer.Checked Then cboDealer.SelectedIndex = NOTHING_SELECTED
                If rbStages.Checked Then moStageList.SelectedIndex = NOTHING_SELECTED
                If rbStageStatus.Checked Then moStageStatusList.SelectedIndex = NOTHING_SELECTED
                InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(Label2)
                ClearLabelErrSign(moDealerLabel)
                ClearLabelErrSign(lblAllStages)
                ClearLabelErrSign(lblStageName)
                ClearLabelErrSign(lblAllStageStatus)
                ClearLabelErrSign(lblStageStatus)
                ClearLabelErrSign(lblNoofDayssincestageopened)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub InitializeForm()
            PopulateDealerDropDown()
            PopulateStagesDropDown()
            PopulateStageStatusDropDown()

            rDealer.Checked = True
            rbStages.Checked = True
            rbStageStatus.Checked = True

        End Sub



        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Protected Sub PopulateDealerDropDown()
            Dim oDealerList As New Collections.Generic.List(Of DataElements.ListItem)
            Dim oListContext As New ListContext

            For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                oListContext.CompanyId = company_id
                Dim oDealerListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If oDealerList IsNot Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If
                End If
            Next

            cboDealer.Populate(oDealerList.ToArray(), New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf .GetDescription,
                                                    .ValueFunc = AddressOf .GetListItemId,
                                                    .SortFunc = AddressOf .GetDescription
                                                   })

            'Me.BindListControlToDataView(cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
        End Sub

        Private Sub PopulateStagesDropDown()
            'Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            'oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
            Dim stageList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="GetStageList")

            moStageList.Populate(stageList.ToArray(), New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf .GetDescription,
                                                    .ValueFunc = AddressOf .GetListItemId,
                                                    .SortFunc = AddressOf .GetDescription
                                                   })

            BindListControlToDataView(moStageList, LookupListNew.GetStagesByGroupLookupList())
        End Sub
        Private Sub PopulateStageStatusDropDown()
            moStageStatusList.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="CLM_STAGE_STATUS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()).ToArray(), New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf .GetDescription,
                                                    .ValueFunc = AddressOf .GetListItemId,
                                                    .SortFunc = AddressOf .GetDescription
                                                   })
            'Me.BindListControlToDataView(moStageStatusList, LookupListNew.GetOpenClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
        End Sub

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


        Private Sub GenerateReport()
            Dim reportParams As New System.Text.StringBuilder
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim langId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Dim selectedDealerId As Guid = GetSelectedItem(cboDealer)
            Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)

            Dim NoofDayssincestageopened As String = tbdayssincestageopened.Text

            Dim SelectedStageNameId As Guid = GetSelectedItem(moStageList)
            Dim dvStageName As DataView = LookupListNew.GetStagesByGroupLookupList()
            Dim stagenamecode As String = LookupListNew.GetDescriptionFromId(dvStageName, SelectedStageNameId)

            Dim SelectedStageStatusId As Guid = GetSelectedItem(moStageStatusList)
            Dim dvStageStatus As DataView = LookupListNew.GetOpenClosedLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            Dim StageStatus As String = LookupListNew.GetDescriptionFromId(dvStageStatus, SelectedStageStatusId)


            reportParams.AppendFormat("pi_user_key => '{0}',", userId)
            reportParams.AppendFormat("pi_language => '{0}',", langId)

            If rDealer.Checked Then
                dealerCode = ALL
                reportParams.AppendFormat("pi_dealer => '{0}',", "*")
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moDealerLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
                reportParams.AppendFormat("pi_dealer => '{0}',", dealerCode)
            End If

            If rbStages.Checked Then
                stagenamecode = ALL
                reportParams.AppendFormat("pi_stage_name => '{0}',", "*")
            Else
                If SelectedStageNameId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(lblStageName)
                    Throw New GUIException(Message.MSG_INVALID_CLAIM_STAGE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_STAGE_MUST_BE_SELECTED_ERR)
                End If
                reportParams.AppendFormat("pi_stage_name => '{0}',", GuidControl.GuidToHexString(SelectedStageNameId))
            End If

            If rbStageStatus.Checked Then
                StageStatus = ALL
                reportParams.AppendFormat("pi_stage_status => '{0}',", "*")
            Else
                If SelectedStageStatusId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(lblStageStatus)
                    Throw New GUIException(Message.MSG_INVALID_CLAIM_STAGE_STATUS, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_STAGE_STATUS_MUST_BE_SELECTED_ERR)

                End If
                reportParams.AppendFormat("pi_stage_status => '{0}',", StageStatus)

                End If

            If NoofDayssincestageopened.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(lblNoofDayssincestageopened)
                Throw New GUIException(Message.MSG_INVALID_DAYS, Assurant.ElitaPlus.Common.ErrorCodes.DAYS_MUST_BE_SELECTED_ERR)

            ElseIf System.Char.IsDigit(NoofDayssincestageopened) = False Then
                ElitaPlusPage.SetLabelError(lblNoofDayssincestageopened)
                Throw New GUIException(Message.MSG_INVALID_DAYS, Assurant.ElitaPlus.Common.ErrorCodes.ONLY_VALID_NUMBERS_MUST_BE_ENTERED_ERR)

            ElseIf CInt(NoofDayssincestageopened) > (9999) Then
                ElitaPlusPage.SetLabelError(lblNoofDayssincestageopened)
                Throw New GUIException(Message.MSG_INVALID_DAYS, Assurant.ElitaPlus.Common.ErrorCodes.CORRECT_NUMBER_OF_DAYS_MUST_BE_SELECTED_ERR)

            End If

            reportParams.AppendFormat("pi_number_of_days_stage_open => '{0}'", NoofDayssincestageopened)

            State.MyBO = New ReportRequests
            State.ForEdit = True
            PopulateBOProperty(State.MyBO, "ReportType", "EXTENDED_STATUS_AGING_DETAIL")
            PopulateBOProperty(State.MyBO, "ReportProc", "r_extendedstatusagingdetail.Report")
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateBosFromForm()
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
    End Class
End Namespace
