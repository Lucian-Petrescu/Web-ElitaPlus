Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Xml
Imports System.Xml.Xsl
Imports System.ServiceModel
Public Class WorkQueueActionForm
    Inherits ElitaPlusSearchPage

#Region "Private Variables"
    Private IsReturningFromChild As Boolean = False
    Private ItemAction As ItemActionType = ItemActionType.None
#End Region

#Region "Enum"
    Public Enum ItemActionType
        Process
        Requeue
        Redirect
        None
    End Enum


#End Region


#Region "Page State"

    Class MyState
        Public Action As BaseActionProvider
        Public WorkQueueItem As WorkQueueItem
    End Class


    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFlowFromSession()
                GetAction()
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Protected Sub GetAction()
        Try
            If Me.State.Action Is Nothing Then
                Me.State.Action = BaseActionProvider.GetAction()
                If (Not Me.State.Action Is Nothing) Then
                    Me.State.WorkQueueItem = Me.State.Action.WorkQueueItem
                End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub InitializeFlowFromSession()
        If (Not Me.NavController.ParametersPassed Is Nothing) Then
            ItemAction = CType(Me.NavController.ParametersPassed, ItemActionType)
        End If

    End Sub

#End Region


#Region "Page Events"

    Private Sub UpdateBreadCrum()

        Me.MasterPage.BreadCrum = Me.MasterPage.PageTab

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            If Not Me.IsPostBack Then
                UpdateBreadCrum()
                If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = False Then
                    PlaceHolder1.Visible = False
                    PlaceHolder2.Visible = False
                    mcWorkQueueAction.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_DISABLE_FUNCTIONALITY, True)
                    Throw New GUIException("", "")
                End If
                'This Code must be the first thing to execute
                If Not Me.IsReturningFromChild Then
                    Me.StartNavControl()
                End If
            End If

            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("WORK_QUEUE_ITEM")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("WORK_QUEUE_ITEM")


            Me.MasterPage.MessageController.Clear()
            mcWorkQueueAction.Clear()
            If Not Me.IsPostBack Then
                PopulateForm()
            End If

            If (ItemAction = ItemActionType.Redirect) Then
                mcWorkQueueAction.AddSuccess("MSG_WQ_ITEM_REDIRECT_SUCCESS")
            ElseIf (ItemAction = ItemActionType.Process) Then
                mcWorkQueueAction.AddSuccess("MSG_WQ_ITEM_PROCESS_SUCCESS")
            End If

            'Check if Queue Item belongs to same company group or not
            If (Not Me.State.WorkQueueItem Is Nothing) Then
                If (Not CheckQueueItemForUserCompany()) Then
                    Me.MasterPage.MessageController.AddWarning("MSG_WO_ITEM_DIFFERENT_COMPANY")
                    btnZone.Visible = False
                    Exit Sub
                Else
                    btnZone.Visible = True
                End If
            End If


            btnRequeueContinue.Attributes.Add("onclick", String.Format("return validate('{0}','{1}');", rdbtlstRequeueReasons.ClientID, modalMessageBoxRequeue.ClientID))
            btnRequeueCancel.Attributes.Add("onclick", String.Format("HideErrorAndModal('{0}','{1}','{2}');", "ModalRequeueReasons", rdbtlstRequeueReasons.ClientID, modalMessageBoxRequeue.ClientID))

            btnRedirectContinue.Attributes.Add("onclick", String.Format("return validate('{0}','{1}');", rdbtRedirectRsn.ClientID, modalMessageBoxRedirect.ClientID))
            btnRedirectCancel.Attributes.Add("onclick", String.Format("HideErrorAndModal('{0}','{1}','{2}');", "ModalRedirectReason", rdbtRedirectRsn.ClientID, modalMessageBoxRedirect.ClientID))
            ddlWorkQueueList.Attributes.Add("onChange", String.Format("document.body.style.cursor = 'wait';UncheckRadioButton('{0}');", rdbtRedirectRsn.ClientID))

        Catch ex As GUIException

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button Handlers"

    Protected Sub btnProceed_Click() Handles btnProceed.Click
        Try
            NavigateNext()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnRequeueContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRequeueContinue.Click
        Try
            Me.State.WorkQueueItem.ReQueue(New Guid(rdbtlstRequeueReasons.SelectedItem.Value), rdbtlstRequeueReasons.SelectedItem.Text)
            Me.State.Action = BaseActionProvider.GetAction()
            If (Not Me.State.Action Is Nothing) Then
                Me.State.WorkQueueItem = Me.State.Action.WorkQueueItem
            End If
            mcWorkQueueAction.Clear()
            If (Me.State.Action Is Nothing) Then
                mcWorkQueueAction.AddSuccess("MSG_WQ_ITEM_REQUEUE_SUCCESS_NO_NEXT_ITEM")
            Else
                mcWorkQueueAction.AddSuccess("MSG_WQ_ITEM_REQUEUE_SUCCESS")
            End If
            PopulateForm()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            PopulateForm()
        End Try
    End Sub

    Protected Sub btnRedirectContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRedirectContinue.Click
        Try
            Me.State.WorkQueueItem.ReDirect(ddlWorkQueueList.SelectedItem.Text, New Guid(rdbtRedirectRsn.SelectedItem.Value), rdbtRedirectRsn.SelectedItem.Text)
            Me.State.Action = BaseActionProvider.GetAction()
            If (Not Me.State.Action Is Nothing) Then
                Me.State.WorkQueueItem = Me.State.Action.WorkQueueItem
            End If
            mcWorkQueueAction.Clear()
            If (Me.State.Action Is Nothing) Then
                mcWorkQueueAction.AddSuccess("MSG_WQ_ITEM_REDIRECT_SUCCESS_NO_NEXT_ITEM")
            Else
                mcWorkQueueAction.AddSuccess("MSG_WQ_ITEM_REDIRECT_SUCCESS")
            End If
            PopulateForm()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            PopulateForm()
        End Try
    End Sub

    Protected Sub ddlWorkQueueList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlWorkQueueList.SelectedIndexChanged
        Try
            Dim wq As WorkQueue
            Dim wqReasonList As ListItem()
            If (Not String.IsNullOrEmpty(ddlWorkQueueList.SelectedItem.Value)) Then
                wq = New WorkQueue(New Guid(ddlWorkQueueList.SelectedItem.Value))
                wqReasonList = (From wqr In wq.ReDirectReasons Select New ListItem(If(wqr.Description Is Nothing, wqr.ItemStatusReason.Reason, wqr.Description), wqr.ItemStatusReason.Id.ToString())).ToArray()
                Me.BindListControlToArray(rdbtRedirectRsn, wqReasonList, False, False, Guid.Empty.ToString())
                ddlWorkQueueList.SelectedValue = ddlWorkQueueList.SelectedItem.Value
                If (wqReasonList.Count = 0) Then
                    msgRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_NO_REDIRECT_REASONS_TO_POPULATE")
                    modalMessageBoxRedirect.Attributes.Add("class", "infoMsg")
                    modalMessageBoxRedirect.Attributes.Add("style", "display: block")
                    imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_info.png"
                    trLblRedirectRsn.Visible = False
                    rdbtRedirectRsn.Visible = False
                    btnRedirectContinue.Visible = False
                    btnRedirectCancel.Visible = False
                Else
                    modalMessageBoxRedirect.Attributes.Add("class", "errorMsg")
                    modalMessageBoxRedirect.Attributes.Add("style", "display: none")
                    imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
                    trLblRedirectRsn.Visible = True
                    rdbtRedirectRsn.Visible = True
                    btnRedirectContinue.Visible = True
                    btnRedirectCancel.Visible = True
                End If
            Else
                modalMessageBoxRedirect.Attributes.Add("class", "errorMsg")
                modalMessageBoxRedirect.Attributes.Add("style", "display: none")
                imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
                trLblRedirectRsn.Visible = False
                rdbtRedirectRsn.Visible = False
                btnRedirectContinue.Visible = True
                btnRedirectCancel.Visible = True
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Controlling Logic"

    Private Sub PopulateForm()

        If (Me.State.Action Is Nothing) Then
            lblItemAssignmentTimestampValue.Text = String.Empty
            lblQueueNameValue.Text = String.Empty
            mcWorkQueueAction.AddInformation("MSG_NO_WORK_QUEUE_ITEM_FOR_USER")
            btnProceed.Visible = False
            btnRedirect.Visible = False
            btnRequeue.Visible = False
            Return
        End If

        xmlSource.TransformSource = Me.State.Action.XsltPath.AbsolutePath
        xmlSource.Document = Me.State.Action.DisplayXml
        xmlSource.TransformArgumentList = GetXSLTArgumentList(Me.State.WorkQueueItem.WorkQueueItemType)
        lblQueueNameValue.Text = Me.State.WorkQueueItem.WorkQueue.WorkQueue.Name
        lblItemAssignmentTimestampValue.Text = Me.State.WorkQueueItem.StartDate.ToString()
        lblCompanyValue.Text = LookupListNew.GetDescriptionFromCode("COMPANIES", Me.State.WorkQueueItem.WorkQueue.WorkQueue.CompanyCode)
        lblRequeueReason.Text = TranslationBase.TranslateLabelOrMessage("REQUEUE_REASONS_SELECT")
        lblRedirectModalTitle.Text = TranslationBase.TranslateLabelOrMessage("REDIRECT_REASONS")
        lblRequeueReasons.Text = TranslationBase.TranslateLabelOrMessage("REQUEUE_REASONS")
        lblRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("REDIRECT_REASONS_SELECT")
        lblQueueToRedirect.Text = TranslationBase.TranslateLabelOrMessage("QUEUE_NAME")
        msgRequeueReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_SELECT_REQUEUE_REASON")
        msgRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_SELECT_REDIRECT_REASON")

        Dim wqList As ListItem()
        Dim wqReasonList As ListItem()
        wqList = (From wq In GetWorkQueueList(Me.State.WorkQueueItem.WorkQueue.WorkQueue.CompanyCode, Me.State.WorkQueueItem.WorkQueue.WorkQueue.ActionCode) Select New ListItem(wq.Name, wq.Id.ToString())).ToArray()
        Me.BindListControlToArray(ddlWorkQueueList, wqList, False, True, Guid.Empty.ToString())
        If (wqList.Count = 0) Then
            msgRedirectReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_NO_WORK_QUEUES_TO_POPULATE")
            modalMessageBoxRedirect.Attributes.Add("class", "infoMsg")
            modalMessageBoxRedirect.Attributes.Add("style", "display: block")
            imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_info.png"
            trLblRedirectRsn.Visible = False
            rdbtRedirectRsn.Visible = False
            btnRedirectContinue.Visible = False
            btnRedirectCancel.Visible = False
        Else
            modalMessageBoxRedirect.Attributes.Add("class", "errorMsg")
            modalMessageBoxRedirect.Attributes.Add("style", "display: none")
            imgRedirectReasonMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
            trLblRedirectRsn.Visible = False
            rdbtRedirectRsn.Visible = False
            btnRedirectContinue.Visible = True
            btnRedirectCancel.Visible = True
        End If
        wqReasonList = (From wqr In Me.State.WorkQueueItem.WorkQueue.ReQueueReasons Select New ListItem(wqr.Description, wqr.ItemStatusReason.Id.ToString())).ToArray()
        Me.BindListControlToArray(rdbtlstRequeueReasons, wqReasonList, False, False, Guid.Empty.ToString())
        If (wqReasonList.Count = 0) Then
            msgRequeueReasons.Text = TranslationBase.TranslateLabelOrMessage("MSG_NO_REQUEUE_REASONS_TO_POPULATE")
            modalMessageBoxRequeue.Attributes.Add("class", "infoMsg")
            modalMessageBoxRequeue.Attributes.Add("style", "display: block")
            imgRequeueReasonMsg.Src = "~/App_Themes/Default/Images/icon_info.png"
            btnRequeueContinue.Visible = False
            btnRequeueCancel.Visible = False
        Else
            modalMessageBoxRequeue.Attributes.Add("class", "errorMsg")
            modalMessageBoxRequeue.Attributes.Add("style", "display: none")
            imgRequeueReasonMsg.Src = "~/App_Themes/Default/Images/icon_error.png"
            btnRequeueContinue.Visible = True
            btnRequeueCancel.Visible = True
        End If

        If (Me.State.WorkQueueItem.WorkQueueItem.RequeueCount < Me.State.WorkQueueItem.WorkQueue.WorkQueue.MaxRequeue) Then
            btnRequeue.Visible = True
        Else
            btnRequeue.Visible = False
            mcWorkQueueAction.AddInformation("MSG_MAX_REQUEUE_COUNT_REACHED")
        End If


    End Sub

    Private Sub NavigateNext()
        If (Me.State.WorkQueueItem.WorkQueueItemType = WorkQueueItem.ItemType.Issue) Then
            Dim claimBo As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.WorkQueueItem.WorkQueueItem.ClaimId)
            claimBo.CurrentWorkQueueItem = Me.State.WorkQueueItem
            If (claimBo.ClaimAuthorizationType = ClaimAuthorizationType.Single) Then
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                Me.NavController.Navigate(Me, FlowEvents.EVENT_GO_TO_CLAIM)
            Else
                Me.NavController = Nothing
                Me.callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, Nothing, claimBo))
            End If
        ElseIf (Me.State.WorkQueueItem.WorkQueueItemType = WorkQueueItem.ItemType.Image) Then
            Me.NavController.Navigate(Me, FlowEvents.EVENT_GO_TO_IMAGE, Me.State.WorkQueueItem)
        End If
    End Sub

    Private Function GetWorkQueueRequeueReasons(ByVal workQueueId As Guid) As WorkQueueItemStatusReason()
        Dim wkQ As WorkQueue = New WorkQueue(Me.State.WorkQueueItem.WorkQueueItem.WorkQueueId)
        Return wkQ.ReQueueReasons
    End Function

    Private Function GetWorkQueueList(ByVal companyCode As String, ByVal actionCode As String) As WrkQueue.WorkQueue()
        Dim wkQList As WrkQueue.WorkQueue() = WorkQueue.GetList("*", companyCode, actionCode, Date.Now.UtcNow, False)
        wkQList = (From wq In wkQList Where wq.Id <> Me.State.WorkQueueItem.WorkQueue.Id Select wq).ToArray()
        Return wkQList
    End Function

    Private Function GetXSLTArgumentList(ByVal actionCode As WorkQueueItem.ItemType) As XsltArgumentList
        Dim xsltArgList As New XsltArgumentList
        Select Case actionCode
            Case WorkQueueItem.ItemType.Issue
                xsltArgList.AddParam("INSURED", String.Empty, TranslationBase.TranslateLabelOrMessage("INSURED"))
                xsltArgList.AddParam("CLAIM_NUMBER", String.Empty, TranslationBase.TranslateLabelOrMessage("CLAIM_NUMBER"))
                xsltArgList.AddParam("ISSUE_DATE", String.Empty, TranslationBase.TranslateLabelOrMessage("ISSUE_DATE"))
                xsltArgList.AddParam("DATE_OF_LOSS", String.Empty, TranslationBase.TranslateLabelOrMessage("DATE_OF_LOSS"))
                xsltArgList.AddParam("ISSUE", String.Empty, TranslationBase.TranslateLabelOrMessage("ISSUE"))
                xsltArgList.AddParam("TYPE_OF_LOSS", String.Empty, TranslationBase.TranslateLabelOrMessage("TYPE_OF_LOSS"))
                xsltArgList.AddParam("HDR_ISSUE_GRID", String.Empty, TranslationBase.TranslateLabelOrMessage("HDR_ISSUE_GRID"))
                xsltArgList.AddParam("PAGE_SIZE", String.Empty, TranslationBase.TranslateLabelOrMessage("PAGE_SIZE"))
                xsltArgList.AddParam("CREATED_DATE", String.Empty, TranslationBase.TranslateLabelOrMessage("CREATED_DATE"))
                xsltArgList.AddParam("CREATED_BY", String.Empty, TranslationBase.TranslateLabelOrMessage("CREATED_BY"))
                xsltArgList.AddParam("PROCESSED_DATE", String.Empty, TranslationBase.TranslateLabelOrMessage("PROCESSED_DATE"))
                xsltArgList.AddParam("PROCESSED_BY", String.Empty, TranslationBase.TranslateLabelOrMessage("PROCESSED_BY"))
                xsltArgList.AddParam("STATUS", String.Empty, TranslationBase.TranslateLabelOrMessage("STATUS"))
            Case WorkQueueItem.ItemType.Image
                xsltArgList.AddParam("IMAGE_ID", String.Empty, TranslationBase.TranslateLabelOrMessage("IMAGE_ID"))
                xsltArgList.AddParam("SCAN_DATE", String.Empty, TranslationBase.TranslateLabelOrMessage("SCAN_DATE"))

        End Select
        Return xsltArgList

    End Function

    Private Function CheckQueueItemForUserCompany() As Boolean
        Dim flag As Boolean = False
        Dim userCompanies As DataView = ElitaPlusIdentity.Current.ActiveUser.GetSelectedCompanies(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.Id)

        For Each row As DataRowView In userCompanies
            If (Me.State.WorkQueueItem.WorkQueue.WorkQueue.CompanyCode = CType(row("Code"), String)) Then
                flag = True
            End If
        Next

        Return flag
    End Function
#End Region

#Region "State Controller"
    Public Const FLOW_NAME As String = "WORK_ON_QUEUE"
    Sub StartNavControl()
        Dim nav As New ElitaPlusNavigation
        Me.NavController = New NavControllerBase(nav.Flow(FLOW_NAME))
    End Sub

    Function IsFlowStarted() As Boolean
        Return Not Me.NavController Is Nothing AndAlso Me.NavController.CurrentFlow.Name = Me.FLOW_NAME
    End Function
#End Region

End Class
