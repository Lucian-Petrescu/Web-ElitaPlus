Public Class CaseDetailsForm
    Inherits ElitaPlusSearchPage
#Region "Constants"
    Public Const Url As String = "~/DCM/CaseDetailsForm.aspx"
    Public Const NoData As String = " - "
    Public Const CaseDenedReasonInfoTab As Integer = 3
    Private Const OneSpace As String = " "
#End Region
#Region "Variables"
    Private _listDisabledTabs As New Collections.Generic.List(Of Integer)
#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CaseBase
        Public BoChanged As Boolean = False
        Public Sub New(ByVal lastOp As DetailPageCommand, ByVal curEditingBo As CaseBase, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            BoChanged = boChanged
        End Sub
    End Class

#End Region
    #Region "Page State"
    Class BaseState
        Public NavCtrl As INavigationController
    End Class

    Class MyState
        Public MyBo As CaseBase
        Public SelectedTab As Integer = 0
        Public CaseInteractionListDv As CaseInteraction.CaseInteractionDV = Nothing
        Public CaseActionListDv As CaseAction.CaseActionDV = Nothing
        Public CaseQuestionAnswerListDv As CaseQuestionAnswer.CaseQuestionAnswerDV = Nothing
        Public CaseDeniedReasonsListDv As CaseBase.CaseDeniedReasonsDV = Nothing
        Public CaseNotesListDv As CaseBase.CaseNotesDV = Nothing
        Public SortExpression As String = "created_date desc"
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public InputParameters As Parameters
        Public DisabledTabs As String = String.Empty
        Sub New()
        End Sub
    End Class


    Public Sub New()
        MyBase.New(New BaseState)
    End Sub
    Protected Shadows ReadOnly Property State() As MyState
        Get
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                InitializeFromFlowSession()
            Else
                If NavController.IsFlowEnded Then
                    'restart flow
                    Dim s As MyState = CType(NavController.State, MyState)
                    StartNavControl()
                    NavController.State = s
                End If
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property
    Protected Sub InitializeFromFlowSession()

        State.InputParameters = TryCast(NavController.ParametersPassed, Parameters)

        Try
            If Not State.InputParameters Is Nothing Then
                State.MyBo = CType(NavController.ParametersPassed, Parameters).CaseBo
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Parameters"
    Public Class Parameters
        'Public caseID As Guid
        Public CaseBo As CaseBase
        Public Sub New(ByVal caseBo As CaseBase)
            Me.CaseBo = caseBo
        End Sub
        Public Sub New(ByVal caseId As Guid)
            Me.CaseBo = New CaseBase(caseId)
        End Sub
    End Class
#End Region
#Region "Page Events"
    Private Sub Page_PageCall(ByVal callFromUrl As String, ByVal callingPar As Object) Handles MyBase.PageCall
        Try

            If Not CallingParameters Is Nothing Then
                StartNavControl()
                State.MyBo = New CaseBase(CType(CallingParameters, Guid))
            Else
                Throw New Exception("No Calling Parameters")
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Case")

        UpdateBreadCrum()
        Try
            MasterPage.MessageController.Clear()
            If Not IsPostBack Then

                PopulateFormFromBOs()

                TranslateGridHeader(CaseInteractionGrid)
                TranslateGridHeader(CaseActionGrid)
                TranslateGridHeader(CaseQuestionAnswerGrid)
                TranslateGridHeader(CaseDeniedReasonsGrid)
                TranslateGridHeader(GridViewCaseNotes)

                PopulateInteractionGrid()
                PopulateActionGrid()
                PopulateQuestionAnswerGrid()

                PopulateDeniedReasonsGrid()
                PopulateCaseNotesGrid()

                EnableDisableFields()
                'End If
            Else 'page posted back
                State.SelectedTab = hdnSelectedTab.Value 'store the selected tab
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub CertificateForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        'set tab display info
        hdnSelectedTab.Value = State.SelectedTab
        Dim strTemp As String = String.Empty
        If _listDisabledTabs.Count > 0 Then
            For Each i As Integer In _listDisabledTabs
                strTemp = strTemp + "," + i.ToString
            Next
            strTemp = strTemp.Substring(1) 'remove the first comma
        End If
        State.DisabledTabs = strTemp 'store the disabled state
        hdnDisabledTabs.Value = State.DisabledTabs
    End Sub
#End Region

#Region "Navigation Control"

    Sub StartNavControl()
        Dim nav As New ElitaPlusNavigation
        Me.NavController = New NavControllerBase(nav.Flow("CREATE_CASE_DETAIL"))
    End Sub
    #End Region
#Region "Controlling Logic"
    Private Sub UpdateBreadCrum()
        If (Not State Is Nothing) Then
            If (Not State.MyBO Is Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("Case") & " " & State.MyBO.CaseNumber
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Message.CASE_DETAIL) & " (<strong>" &
                    State.MyBO.CaseNumber & "</strong>) " & TranslationBase.TranslateLabelOrMessage("SUMMARY")
            End If
        End If
    End Sub
    Protected Sub EnableDisableFields()
        btnBack.Enabled = True
        EnableDisableControls(CaseInteractionTabPanel, True)
        EnableDisableControls(CaseActionTabPanel, True)
        EnableDisableControls(CaseQuestionAnswerTabPanel, True)
    End Sub


    Protected Sub PopulateFormFromBOs(Optional ByVal blnPremiumEdit As Boolean = False)
        Dim cssClassName As String
        With State.MyBO
            PopulateControlFromBOProperty(LabelCompanyValue, .CompanyDesc)
            PopulateControlFromBOProperty(LabelCaseNumberValue, .CaseNumber)
            PopulateControlFromBOProperty(LabelCaseOpenDateValue, .CaseOpenDate)
            PopulateControlFromBOProperty(LabelCasePurposeValue, LookupListNew.GetDescriptionFromCode(LookupListNew.GetCasePurposeLookupList(Authentication.LangId), .CasePurposeCode))
            PopulateControlFromBOProperty(LabelCaseStatusValue, LookupListNew.GetDescriptionFromCode(LookupListNew.GetCaseStatusLookupList(Authentication.LangId), .CaseStatusCode))
            PopulateControlFromBOProperty(LabelCaseCloseDateValue, .CaseCloseDate)
            PopulateControlFromBOProperty(LabelCaseCloseValue, LookupListNew.GetDescriptionFromCode(LookupListNew.GetCaseCloseReasonLookupList(Authentication.LangId), .CaseCloseCode))
            PopulateControlFromBOProperty(LabelCertificateNumberValue, .CertNumber)
            PopulateControlFromBOProperty(LabelClaimNumberValue, .ClaimNumber)
            If (.CaseStatusCode = Codes.CASE_STATUS__OPEN) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If
            CaseStatusTD.Attributes.Item("Class") = cssClassName
        End With

    End Sub
    Private Sub PopulateInteractionGrid()
        Try

            If (State.CaseInteractionListDV Is Nothing) Then
                State.CaseInteractionListDV = CaseInteraction.GetCaseInteractionList(State.MyBO.Id, Authentication.LangId)
            End If

            lblInteractionRecordFound.Visible = True

            If State.CaseInteractionListDV.Count = 0 Then
                lblInteractionRecordFound.Text = State.CaseInteractionListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                CaseInteractionGrid.DataSource = State.CaseInteractionListDV
                State.CaseInteractionListDV.Sort = State.SortExpression
                HighLightSortColumn(CaseInteractionGrid, State.SortExpression, IsNewUI)
                CaseInteractionGrid.DataBind()
                lblInteractionRecordFound.Text = State.CaseInteractionListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

            ControlMgr.SetVisibleControl(Me, CaseInteractionGrid, True)

        Catch ex As Exception
            Dim getExceptionType As String = ex.GetBaseException.GetType().Name
            If ((Not getExceptionType.Equals(String.Empty)) And getExceptionType.Equals("BOValidationException")) Then
                ControlMgr.SetVisibleControl(Me, CaseInteractionGrid, False)
                lblInteractionRecordFound.Visible = False
            End If
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateActionGrid()
        Try

            If (State.CaseActionListDV Is Nothing) Then
                State.CaseActionListDV = CaseAction.GetCaseActionList(State.MyBO.Id, Authentication.CurrentUser.LanguageId)
            End If


            lblActionRecordFound.Visible = True

            If State.CaseActionListDV.Count = 0 Then
                lblActionRecordFound.Text = State.CaseActionListDV.Count & OneSpace  & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                CaseActionGrid.DataSource = State.CaseActionListDV
                State.CaseActionListDV.Sort = State.SortExpression
                HighLightSortColumn(CaseActionGrid, State.SortExpression, IsNewUI)
                CaseActionGrid.DataBind()
                lblActionRecordFound.Text = State.CaseActionListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

            ControlMgr.SetVisibleControl(Me, CaseActionGrid, True)

        Catch ex As Exception
            Dim getExceptionType As String = ex.GetBaseException.GetType().Name
            If ((Not getExceptionType.Equals(String.Empty)) And getExceptionType.Equals("BOValidationException")) Then
                ControlMgr.SetVisibleControl(Me, CaseActionGrid, False)
                lblActionRecordFound.Visible = False
            End If
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateQuestionAnswerGrid()
        Try

            If (State.CaseQuestionAnswerListDV Is Nothing) Then
                State.CaseQuestionAnswerListDV = CaseQuestionAnswer.getCaseQuestionAnswerList(State.MyBO.Id, Authentication.CurrentUser.LanguageId)
            End If

            lblQuestionRecordFound.Visible = True

            If State.CaseQuestionAnswerListDV.Count = 0 Then
                lblQuestionRecordFound.Text = State.CaseQuestionAnswerListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                CaseQuestionAnswerGrid.DataSource = State.CaseQuestionAnswerListDV
                State.CaseQuestionAnswerListDV.Sort = State.SortExpression
                HighLightSortColumn(CaseQuestionAnswerGrid, State.SortExpression, IsNewUI)
                CaseQuestionAnswerGrid.DataBind()
                lblQuestionRecordFound.Text = State.CaseQuestionAnswerListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
            ControlMgr.SetVisibleControl(Me, CaseQuestionAnswerGrid, True)

        Catch ex As Exception
            Dim getExceptionType As String = ex.GetBaseException.GetType().Name
            If ((Not getExceptionType.Equals(String.Empty)) And getExceptionType.Equals("BOValidationException")) Then
                ControlMgr.SetVisibleControl(Me, CaseQuestionAnswerGrid, False)
                lblQuestionRecordFound.Visible = False
            End If
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateDeniedReasonsGrid()
        Try

            If (State.CaseDeniedReasonsListDV Is Nothing) Then
                State.CaseDeniedReasonsListDV = CaseBase.GetCaseDeniedReasonsList(State.MyBO.Id, Authentication.CurrentUser.LanguageId)
            End If

            lblDeniedReasonsRecordFound.Visible = True

            If State.CaseDeniedReasonsListDV.Count = 0 Then
                'lblDeniedReasonsRecordFound.Text = State.CaseDeniedReasonsListDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                'ControlMgr.SetVisibleControl(Me, CaseDeniedReasonsGrid, False)
                EnableTab(CaseDenedReasonInfoTab, false)
            Else
                CaseDeniedReasonsGrid.DataSource = State.CaseDeniedReasonsListDV
                State.CaseDeniedReasonsListDV.Sort = State.SortExpression
                HighLightSortColumn(CaseDeniedReasonsGrid, State.SortExpression, IsNewUI)
                CaseDeniedReasonsGrid.DataBind()
                lblDeniedReasonsRecordFound.Text = State.CaseDeniedReasonsListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                ControlMgr.SetVisibleControl(Me, CaseDeniedReasonsGrid, True)
            End If
            

        Catch ex As Exception
            Dim getExceptionType As String = ex.GetBaseException.GetType().Name
            If ((Not getExceptionType.Equals(String.Empty)) And getExceptionType.Equals("BOValidationException")) Then
                ControlMgr.SetVisibleControl(Me, CaseDeniedReasonsGrid, False)
                lblQuestionRecordFound.Visible = False
            End If
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateCaseNotesGrid()
        Try
            If (State.CaseNotesListDV Is Nothing) Then
                State.CaseNotesListDV = CaseBase.GetCaseNotesList(State.MyBO.Id)
            End If

            LabelCaseNotesRecordFound.Visible = True

            If State.CaseNotesListDV.Count = 0 Then
                LabelCaseNotesRecordFound.Text = State.CaseNotesListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                GridViewCaseNotes.DataSource = State.CaseNotesListDV
                State.CaseNotesListDV.Sort = State.SortExpression
                HighLightSortColumn(GridViewCaseNotes, State.SortExpression, IsNewUI)
                GridViewCaseNotes.DataBind()
                LabelCaseNotesRecordFound.Text = State.CaseNotesListDV.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

            ControlMgr.SetVisibleControl(Me, GridViewCaseNotes, True)

        Catch ex As Exception
            Dim getExceptionType As String = ex.GetBaseException.GetType().Name
            If ((Not getExceptionType.Equals(String.Empty)) And getExceptionType.Equals("BOValidationException")) Then
                ControlMgr.SetVisibleControl(Me, GridViewCaseNotes, False)
                LabelCaseNotesRecordFound.Visible = False
            End If
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Button Click"
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnBack.Click
        Try

            Dim myBo As CaseBase = State.MyBO
            Dim retObj As ReturnType = New ReturnType(DetailPageCommand.Back, myBo, False)
            NavController = Nothing
            ReturnToCallingPage(retObj)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            State.ActionInProgress = DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    'Protected Sub btnOutboundCommHistory_Click(sender As Object, e As EventArgs) Handles btnOutboundCommHistory.Click
    '    Try
    '        Dim cert As BusinessObjectsNew.Certificate = New BusinessObjectsNew.Certificate(State.MyBO.CertId)
    '        Dim Msg_Search_Form_URL As String = "../Tables/OcMessageSearchForm.aspx"
    '        Dim MsgPara As Tables.OcMessageSearchForm.CallType = New Tables.OcMessageSearchForm.CallType("case_number", State.MyBO.CaseNumber, State.MyBO.Id, cert.DealerId)

    '        StartNavControl()
    '        'NavController.Navigate(Me, "message_history", MsgPara)
    '        callPage("~/Tables/OcMessageSearchForm.aspx", MsgPara, Nothing)
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Exception
    '        HandleErrors(ex, MasterPage.MessageController)
    '    End Try
    'End Sub

#End Region

#Region "Tab related"
    Private Sub EnableTab(ByVal tabInd As Integer, ByVal blnFlag As Boolean)
        If blnFlag = True Then 'enable - remove from disabled list
            If _listDisabledTabs.Contains(tabInd) = True Then
                _listDisabledTabs.Remove(tabInd)
            End If
        Else 'disable - add to the disabled list
            If _listDisabledTabs.Contains(tabInd) = False Then
                _listDisabledTabs.Add(tabInd)
            End If
        End If
    End Sub

#End Region
End Class

