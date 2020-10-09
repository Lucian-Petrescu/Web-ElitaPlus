Imports System.Collections.Generic
Imports Assurant.Elita.ClientIntegration
Imports Assurant.ElitaPlus.BusinessObjectsNew.LegacyBridgeService
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Partial Class CommentForm
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "~/Certificates/CommentForm.aspx"
    Public Const COMMENT_DETAIL As String = "COMMENT_DETAIL"
    Public Const NEW_CLAIM_FORM As String = "newclaimform"
    Public Const CERTIFICATE_FORM As String = "CertificateForm"
    Public Const PROTECTION_AND_EVENT_DETAILS As String = "PROTECTION_AND_EVENT_DETAILS"
#End Region

#Region "Variables"

    Private mbIsFirstPass As Boolean = True

#End Region

#Region "Parameters"
    Public Class Parameters
        Public CommentBo As Comment
        Public Sub New(commentBo As Comment)
            Me.CommentBo = commentBo
        End Sub
        Public Sub New(commentID As Guid)
            CommentBo = New Comment(commentID)
        End Sub
    End Class
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Comment
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Comment)
            LastOperation = LastOp
            EditingBo = curEditingBo
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As Comment
        Public ScreenSnapShotBO As Comment

        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String

        Public CertItemBO As CertItem
        Public claimEquipmentBO As ClaimEquipment
        Public ClaimBO As ClaimBase
        Public CertificateBO As Certificate
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                Me.State.MyBO = CType(NavController.ParametersPassed, Parameters).CommentBo
                If Me.State.MyBO IsNot Nothing Then
                    Me.State.MyBO.BeginEdit()
                End If
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New Comment(CType(CallingParameters, Guid))
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Events"

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State.MyBO IsNot Nothing) Then
                If IsFromCertificateForm() Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & "Certificate " & State.MyBO.CertificateNumber & ElitaBase.Sperator & "Comment"
                Else
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & "Certificate " & State.MyBO.CertificateNumber & ElitaBase.Sperator & "File New Claim"
                End If
            End If
        End If
    End Sub

    Private Function IsFromCertificateForm() As Boolean
        If Request.UrlReferrer.ToString.ToUpper.Contains(CERTIFICATE_FORM.ToUpper) Then
            Return True
        End If
        Return False
    End Function

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If mbIsFirstPass = True Then
            mbIsFirstPass = False
        Else
            ' Do not load again the Page that was already loaded
            Return
        End If
        If NavController.CurrentNavState.Name <> COMMENT_DETAIL Then
            Return
        End If
        If Request.UrlReferrer.ToString.ToUpper.Contains(NEW_CLAIM_FORM.ToUpper) Then
            WizardControl.Visible = True
        Else
            WizardControl.Visible = False
        End If
        If IsFromCertificateForm() Then
            If CallingParameters IsNot Nothing Then
                Try
                    State.CertificateBO = New Certificate(CType(CallingParameters, Guid))
                Catch ex As Exception
                    Dim callingParams As Certificates.CertificateForm.Parameters = CType(CallingParameters, Certificates.CertificateForm.Parameters)
                    State.CertificateBO = New Certificate(callingParams.CertificateId)
                End Try
            ElseIf State.MyBO IsNot Nothing Then
                State.CertificateBO = New Certificate(State.MyBO.CertId)
            End If
            ControlMgr.SetVisibleControl(Me, moCertificateInfoController, True)
            ControlMgr.SetVisibleControl(Me, moProtectionEvtDtl, False)
            moCertificateInfoController.InitController(State.CertificateBO, , )
            LabelTitle.Text = TranslationBase.TranslateLabelOrMessage("Comment")
            btnAdd_WRITE.Text = TranslationBase.TranslateLabelOrMessage("Save")
        Else
            ControlMgr.SetVisibleControl(Me, moCertificateInfoController, False)
        End If
        MasterPage.MessageController.Clear()

        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificate")
        If IsFromCertificateForm() Then
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Message.Certificate_Detail) &
                " (<strong>" & State.CertificateBO.CertNumber & "</strong>) " & TranslationBase.TranslateLabelOrMessage("SUMMARY")
        Else
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PROTECTION_AND_EVENT_DETAILS)
        End If
        UpdateBreadCrum()

        Try
            If Not IsPostBack Then
                PopulateProtectionAndEventDetail()
                If State.MyBO Is Nothing Then
                    State.MyBO = New Comment
                    State.MyBO.BeginEdit()
                End If
                Trace(Me, "Comment Id =" & GuidControl.GuidToHexString(State.MyBO.Id))
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            CleanPopupInput()
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        Try
            ChangeEnabledProperty(TextboxCallerName, True)
            ChangeEnabledProperty(TextboxCommentText, True)
            ChangeEnabledProperty(cboCommentType, True)
            btnBack.Enabled = True
            'BEGIN - Ravi - Comment out the disabling of the "BACK" button
            'If Not Me.State.MyBO.IsNew Then
            '    Me.btnBack.Enabled = False
            'End If
            'END - Ravi - Comment out the disabling of the "BACK" button
            If TextboxDateTime.Text Is Nothing OrElse TextboxDateTime.Text.Trim.Length = 0 Then
                'Make the Date/Time info invisible
                ControlMgr.SetVisibleControl(Me, LabelDateTime, False)
                ControlMgr.SetVisibleControl(Me, TextboxDateTime, False)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Try
            ClearGridHeadersAndLabelsErrSign()
            If Not LabelDateTime.Text.EndsWith(":") Then
                LabelDateTime.Text &= ":"
            End If
            BindBOPropertyToLabel(State.MyBO, "CallerName", LabelCallerName)
            BindBOPropertyToLabel(State.MyBO, "CommentTypeId", LabelCommentType)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateDropdowns()
        Try
            Dim commentType As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("COMMT", Thread.CurrentPrincipal.GetLanguageCode())
            cboCommentType.Populate(commentType, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateFormFromBOs()
        Try
            With State.MyBO
                PopulateControlFromBOProperty(TextboxCallerName, .CallerName)
                PopulateControlFromBOProperty(TextboxCertificate, .CertificateNumber)
                PopulateControlFromBOProperty(TextboxCommentText, .Comments)
                PopulateControlFromBOProperty(TextboxDealer, .Dealer)
                PopulateControlFromBOProperty(cboCommentType, .CommentTypeId)

                If .CreatedDate IsNot Nothing Then
                    PopulateControlFromBOProperty(TextboxDateTime, GetLongDateFormattedString(.CreatedDate.Value))
                Else
                    PopulateControlFromBOProperty(TextboxDateTime, Nothing)
                End If

                If State.ClaimBO IsNot Nothing Then
                    If State.ClaimBO.IsAuthorizationLimitExceeded AndAlso (State.ClaimBO.StatusCode = Codes.CLAIM_STATUS__PENDING) Then
                        MasterPage.MessageController.Clear()
                        MasterPage.MessageController.AddInformation("MSG_AUTHORIZATION_LIMIT_EXCEEDED")
                    End If
                End If


            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateBOsFormFrom()
        Try
            With State.MyBO
                PopulateBOProperty(State.MyBO, "CallerName", TextboxCallerName)
                PopulateBOProperty(State.MyBO, "Comments", TextboxCommentText)
                PopulateBOProperty(State.MyBO, "CommentTypeId", cboCommentType)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CleanPopupInput()
        Try
            If State IsNot Nothing Then
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = ""
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Try
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            Dim actionInProgress As ElitaPlusPage.DetailPageCommand = State.ActionInProgress
            'Clean after consuming the action
            CleanPopupInput()
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If actionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    State.MyBO.Save()
                    State.MyBO.EndEdit()
                    NavController.Navigate(Me, FlowEvents.EVENT_ADD, Message.MSG_COMMENT_ADDED)
                End If
                If actionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case actionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        State.MyBO.cancelEdit()
                        'Me.NavController.Navigate(Me, FlowEvents.EVENT_NEXT)
                        NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                End Select
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                State.MyBO.cancelEdit()
                'Me.NavController.Navigate(Me, FlowEvents.EVENT_NEXT)
                Dim eventName As String
                Select Case NavController.PrevNavState.Name
                    Case "SOFTQUESTION_COMMENTS"
                        eventName = "back_soft_question"
                    Case "GET_CLAIM_COMMENT"
                        If NavController.CurrentFlow.EndState.Name = "CLAIM_DETAIL" Then
                            eventName = "back_claim_details"
                        Else
                            eventName = "back_new_claim"
                        End If
                    Case "SAVE_CLAIM_CONFIRMATION"
                        eventName = "back_claim_details"
                    Case Else
                        eventName = FlowEvents.EVENT_BACK

                End Select

                NavController.Navigate(Me, eventName)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
                (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try

    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            PopulateBOsFormFrom()

            NavController.FlowSession(FlowSessionKeys.SESSION_IS_CUSTOMER_EMAIL) = cbEmailToCustomer.Checked
            NavController.FlowSession(FlowSessionKeys.SESSION_IS_SERVICE_CENTER_EMAIL) = cbEmailToServiceCenter.Checked
            NavController.FlowSession(FlowSessionKeys.SESSION_IS_SALVAGE_CENTER_EMAIL) = cbEmailToSalvageCenter.Checked

            If State.MyBO.Claim IsNot Nothing AndAlso Me.State.MyBO.Claim.Status = BasicClaimStatus.Active Then
                'user story 192764 - Task-199011--Start------
                Dim dsCaseFields As DataSet = CaseBase.GetCaseFieldsList(State.MyBO.Claim.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If (dsCaseFields IsNot Nothing AndAlso dsCaseFields.Tables.Count > 0 AndAlso dsCaseFields.Tables(0).Rows.Count > 0) Then

                    Dim hasBenefit As DataRow() = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                    Dim benefitCheckError As DataRow() = dsCaseFields.Tables(0).Select("field_code='BENEFITCHECKERROR'")
                    Dim preCheckError As DataRow() = dsCaseFields.Tables(0).Select("field_code='PRECHECKERROR'")
                    Dim lossType As DataRow() = dsCaseFields.Tables(0).Select("field_code='LOSSTYPE'")

                    If hasBenefit IsNot Nothing AndAlso hasBenefit.Length > 0 Then
                        If hasBenefit(0)("field_value") IsNot Nothing AndAlso String.Equals(hasBenefit(0)("field_value").ToString(), Boolean.FalseString, StringComparison.CurrentCultureIgnoreCase) Then
                            UpdateCaseFieldValues(hasBenefit, lossType)

                            dsCaseFields = CaseBase.GetCaseFieldsList(State.MyBO.Claim.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            hasBenefit = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                        End If
                    End If
                    If benefitCheckError IsNot Nothing AndAlso benefitCheckError.Length > 0 Then
                        If benefitCheckError(0)("field_value") IsNot Nothing AndAlso Not String.Equals(benefitCheckError(0)("field_value").ToString(), "NO ERROR", StringComparison.CurrentCultureIgnoreCase) Then
                            UpdateCaseFieldValues(benefitCheckError, lossType)

                            dsCaseFields = CaseBase.GetCaseFieldsList(State.MyBO.Claim.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            hasBenefit = dsCaseFields.Tables(0).Select("field_code='HASBENEFIT'")
                        End If
                    End If

                    If preCheckError IsNot Nothing AndAlso preCheckError.Length = 0 Then
                        If hasBenefit IsNot Nothing AndAlso hasBenefit.Length > 0 Then
                            If hasBenefit(0)("field_value") IsNot Nothing AndAlso hasBenefit(0)("field_value").ToString().ToUpper() = Boolean.TrueString.ToUpper() Then
                                RunPreCheck(hasBenefit)
                            End If
                        ElseIf benefitCheckError IsNot Nothing AndAlso benefitCheckError.Length > 0 Then
                            If benefitCheckError(0)("field_value") IsNot Nothing AndAlso benefitCheckError(0)("field_value").ToString().ToUpper() <> "NO ERROR" Then
                                RunPreCheck(benefitCheckError)
                            End If
                        End If
                    End If
                End If
            End If

            If State.MyBO.IsDirty Then
                State.MyBO.EndEdit()
                State.MyBO.Save()
                If State.MyBO.Claim IsNot Nothing Then
                    If (Me.State.MyBO.Claim.Status = BasicClaimStatus.Pending) Then
                        NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                        Exit Sub
                    Else
                        NavController.Navigate(Me, FlowEvents.EVENT_ADD, Message.MSG_COMMENT_ADDED)
                    End If
                Else
                    NavController.Navigate(Me, FlowEvents.EVENT_ADD, Message.MSG_COMMENT_ADDED)
                End If
            Else
                If State.MyBO.IsNew Then
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Else
                    NavController.Navigate(Me, FlowEvents.EVENT_ADD, Message.MSG_COMMENT_ADDED)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RunPreCheck(caseRecord As DataRow())
        Try
            Dim benefitCheckResponse As LegacyBridgeResponse
            Dim client As LegacyBridgeServiceClient = Claim.GetLegacyBridgeServiceClient()

            benefitCheckResponse = WcfClientHelper.Execute(Of LegacyBridgeServiceClient, ILegacyBridgeService, LegacyBridgeResponse)(
                client,
                New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(lc As LegacyBridgeServiceClient)
                    Return lc.BenefitClaimPreCheck(GuidControl.ByteArrayToGuid(caseRecord(0)("case_Id")).ToString())
                End Function)

                If (benefitCheckResponse IsNot Nothing) Then
                    State.MyBO.Claim.Status = If(benefitCheckResponse.StatusDecision = LegacyBridgeStatusDecisionEnum.Approve, BasicClaimStatus.Active, BasicClaimStatus.Pending)
                    If (benefitCheckResponse.StatusDecision = LegacyBridgeStatusDecisionEnum.Deny) Then
                        Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECKFAIL")
                        Dim newClaimIssue As ClaimIssue = CType(State.MyBO.Claim.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                        newClaimIssue.SaveNewIssue(State.MyBO.Claim.Id, issueId, State.MyBO.Claim.Certificate.Id, True)
                    End If
                Else
                    State.MyBO.Claim.Status = BasicClaimStatus.Pending
                    Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECK")
                    Dim newClaimIssue As ClaimIssue = CType(State.MyBO.Claim.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                    newClaimIssue.SaveNewIssue(State.MyBO.Claim.Id, issueId, State.MyBO.Claim.Certificate.Id, True)
                End If

            Catch ex As Exception
                Log(ex)
                State.MyBO.Claim.Status = BasicClaimStatus.Pending
                Dim issueId As Guid = LookupListNew.GetIssueTypeIdFromCode(LookupListNew.LK_ISSUES, "PRECK")
                Dim newClaimIssue As ClaimIssue = CType(State.MyBO.Claim.ClaimIssuesList.GetNewChild, BusinessObjectsNew.ClaimIssue)
                newClaimIssue.SaveNewIssue(State.MyBO.Claim.Id, issueId, State.MyBO.Claim.Certificate.Id, True)
            End Try
    End Sub

    Private Shared Sub UpdateCaseFieldValues(ByRef caseFieldRow As DataRow(), ByRef lossType As DataRow())
        Dim caseFieldXcds() As String
        Dim caseFieldValues() As String

        If lossType IsNot Nothing AndAlso lossType.Length > 0 Then
            If lossType(0)("field_value") IsNot Nothing AndAlso (lossType(0)("field_value").ToString().ToUpper() = "ADH1234" OrElse lossType(0)("field_value").ToString().ToUpper() = "ADH5") Then
                caseFieldXcds = { "CASEFLD-HASBENEFIT", "CASEFLD-ADCOVERAGEREMAINING" }
                caseFieldValues = { Boolean.TrueString.ToUpper(), Boolean.TrueString.ToUpper() }
            Else If lossType(0)("field_value") IsNot Nothing AndAlso lossType(0)("field_value").ToString().ToUpper() = "THEFT/LOSS" Then
                caseFieldXcds = { "CASEFLD-HASBENEFIT" }
                caseFieldValues = { Boolean.TrueString.ToUpper() }
            End If
        End If

        CaseBase.UpdateCaseFieldValues(GuidControl.ByteArrayToGuid(caseFieldRow(0)("case_Id")), caseFieldXcds, caseFieldValues)
    End Sub

#End Region

#Region "Page Control Events"

    Private Sub cboMonthlyBilling_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Try
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboDealerMarkup_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Try
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Error Handling"
#End Region

#Region "Elita Look And Feel"

    ''' <summary>
    ''' Populate Header Claim information
    ''' </summary>
    ''' <remarks>REQ-1031</remarks>
    Private Sub PopulateProtectionAndEventDetail()
        Dim cssClassName As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            Dim newClaim As ClaimBase = CType(NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM), ClaimBase)
            'DEF 3577
            If newClaim Is Nothing Then
                newClaim = CType(NavController.FlowSession(FlowSessionKeys.SESSION_OLD_CLAIM), ClaimBase)
            End If
            State.ClaimBO = newClaim
            Dim strMasterClaimNumber As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_MSTR_CLAIM_NUMB), String)
            Dim objDateOfLoss As DateType = Nothing
            Dim strDateOfLoss As String = CType(NavController.FlowSession(FlowSessionKeys.SESSION_DATE_OF_LOSS), String)

            If newClaim IsNot Nothing Then
                Dim oCerticate As Certificate = New Certificate(newClaim.CertificateId)

                moProtectionEvtDtl.CustomerName = newClaim.CustomerName
                moProtectionEvtDtl.DealerName = newClaim.DealerName
                moProtectionEvtDtl.CallerName = newClaim.CallerName
                moProtectionEvtDtl.ClaimNumber = newClaim.ClaimNumber
                moProtectionEvtDtl.DateOfLoss = GetDateFormattedStringNullable(newClaim.LossDate.Value)
                moProtectionEvtDtl.ProtectionStatus = LookupListNew.GetClaimStatusFromCode(langId, oCerticate.StatusCode)

                If (oCerticate.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
                    cssClassName = "StatActive"
                Else
                    cssClassName = "StatClosed"
                End If
                moProtectionEvtDtl.ProtectionStatusCss = cssClassName

                moProtectionEvtDtl.ClaimStatus = LookupListNew.GetClaimStatusFromCode(langId, newClaim.StatusCode)
                If (newClaim.StatusCode = Codes.CLAIM_STATUS__ACTIVE) Then
                    cssClassName = "StatActive"
                Else
                    cssClassName = "StatClosed"
                End If
                moProtectionEvtDtl.ClaimStatusCss = cssClassName

                Dim oCertItemCoverage As CertItemCoverage = New CertItemCoverage(newClaim.CertItemCoverageId)
                State.CertItemBO = New CertItem(oCertItemCoverage.CertItemId)
                If State.CertItemBO IsNot Nothing AndAlso Not State.CertItemBO.ManufacturerId.Equals(Guid.Empty) Then
                    moProtectionEvtDtl.EnrolledMake = New Manufacturer(State.CertItemBO.ManufacturerId).Description
                    moProtectionEvtDtl.EnrolledModel = State.CertItemBO.Model
                Else
                    moProtectionEvtDtl.EnrolledMake = String.Empty
                    moProtectionEvtDtl.EnrolledModel = String.Empty
                End If
            Else
                moProtectionEvtDtl.CustomerName = String.Empty
                moProtectionEvtDtl.DateOfLoss = String.Empty
                moProtectionEvtDtl.ProtectionStatus = String.Empty
                With State.MyBO
                    moProtectionEvtDtl.CallerName = .CallerName
                    TextboxCommentText.Text = .Comments
                    moProtectionEvtDtl.ClaimNumber = .ClaimNumber
                    moProtectionEvtDtl.ClaimStatus = LookupListNew.GetClaimStatusFromCode(langId, .ClaimStatus)
                    If (.ClaimStatus = Codes.CLAIM_STATUS__ACTIVE) Then
                        cssClassName = "StatActive"
                    Else
                        cssClassName = "StatClosed"
                    End If
                    moProtectionEvtDtl.ClaimStatusCss = cssClassName
                    moProtectionEvtDtl.DealerName = .Dealer
                End With
            End If

            moProtectionEvtDtl.ClaimedMake = String.Empty
            moProtectionEvtDtl.ClaimedModel = String.Empty
            moProtectionEvtDtl.TypeOfLoss = String.Empty
            If State.ClaimBO IsNot Nothing Then
                If State.ClaimBO.ClaimedEquipment IsNot Nothing Then
                    moProtectionEvtDtl.ClaimedMake = State.ClaimBO.ClaimedEquipment.Manufacturer
                    moProtectionEvtDtl.ClaimedModel = State.ClaimBO.ClaimedEquipment.Model
                    moProtectionEvtDtl.TypeOfLoss = State.ClaimBO.ClaimedEquipment.ClaimEquipmentType
                End If
            End If
            If State.ClaimBO IsNot Nothing AndAlso Me.State.ClaimBO.ClaimAuthorizationType = ClaimAuthorizationType.Single Then
                ManageEmail()
            Else
                TREmailToCustomer.Visible = False
                TREmailToServiceCenter.Visible = False
                TREmailToSalvageCenter.Visible = False
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ManageEmail()
        If State.CertItemBO IsNot Nothing Then
            ControlMgr.SetVisibleControl(Me, TREmailToCustomer, True)
            If Not String.IsNullOrEmpty(State.CertItemBO.Cert.Email) Then
                TREmailToCustomer.Visible = True
            Else
                TREmailToCustomer.Visible = False
            End If
            EmailToCustomerText.Text = State.CertItemBO.Cert.Email
        ElseIf State.CertItemBO Is Nothing Then
            ControlMgr.SetVisibleControl(Me, TREmailToCustomer, False)
        End If

        LabelAddedBy.Text = ElitaPlusIdentity.Current.ActiveUser.UserName

        If State.ClaimBO IsNot Nothing AndAlso Me.State.ClaimBO.ClaimAuthorizationType = ClaimAuthorizationType.Single Then
            ControlMgr.SetVisibleControl(Me, TREmailToServiceCenter, True)
            Dim oServiceCenter As ServiceCenter = New ServiceCenter(CType(State.ClaimBO, Claim).ServiceCenterId)
            If (oServiceCenter.DefaultToEmailFlag) Then
                cbEmailToServiceCenter.Checked = True
            End If
            EmailToServiceCenterText.Text = oServiceCenter.Email
            If Not State.ClaimBO.Dealer.DefaultSalvgeCenterId = Guid.Empty AndAlso State.ClaimBO.ClaimNumber.Substring(State.ClaimBO.ClaimNumber.Length - 1) = "R" Then
                Dim oSalvageCenter As ServiceCenter = New ServiceCenter(State.ClaimBO.Dealer.DefaultSalvgeCenterId)
                EmailToSalvageCenterText.Text = oSalvageCenter.Email
                If Not EmailToSalvageCenterText.Text = Nothing Then
                    ControlMgr.SetVisibleControl(Me, TREmailToSalvageCenter, True)
                    cbEmailToSalvageCenter.Checked = True
                End If
            End If
        ElseIf State.ClaimBO Is Nothing Then
            ControlMgr.SetVisibleControl(Me, TREmailToServiceCenter, False)
            ControlMgr.SetVisibleControl(Me, TREmailToSalvageCenter, False)
        End If
    End Sub

#End Region

End Class

