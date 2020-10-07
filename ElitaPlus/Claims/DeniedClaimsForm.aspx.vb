Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports System.Web.Services
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Namespace Claims

    Partial Class DeniedClaimsForm
        Inherits ElitaPlusPage
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents UserControlAvailableSelectedServiceCenters As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
#Region "Constants"
        Public Const URL As String = "DeniedClaimsForm.aspx"
        Public Const PAGETITLE As String = "DENIAL_LETTER"
        Public Const PAGETAB As String = "CLAIMS"
        Public Const AVAILABLE_DENIAL_REASON As String = "AVAILABLE_DENIAL_REASON"
        Public Const SELECTED_DENIAL_REASON As String = "SELECTED_DENIAL_REASON"
        Private Const ADDL_DAC_NONE As String = "NONE"
        Private Const LABEL_SERVICE_NETWORK As String = "SERVICE_NETWORK"
        Private Const LIST_ID As String = "list_item_id"
        Private Const TRANS As String = "translation"
        Private Const COL_DESCRIPTION_NAME As String = "description"
        Private Const COL_ID_NAME As String = "id"
        Public Const COL_PRICE_LIST As String = "Price"
#End Region
#Region "Parameters"

        Public Class Parameters
            Public moClaimbo As ClaimBase
            Public moClaimId As Guid
            Public moClaim As String
            Public moCert As String
            Public moCertId As Guid
            Public moIsnew As Boolean
            Public moClaimDenielLetters As Claims.LetterSearchDV

            Public Sub New(oClaimbo As ClaimBase, oClaimId As Guid, oClaim As String, oCert As String, oCertId As Guid, oIsnew As Boolean, oClaimDenielLetters As Claims.LetterSearchDV)
                moClaimbo = oClaimbo
                moClaimId = oClaimId
                moClaim = oClaim
                moCert = oCert
                moCertId = oCertId
                moIsnew = oIsnew
                moClaimDenielLetters = oClaimDenielLetters
            End Sub

        End Class

#End Region
#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As DeniedClaims
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As DeniedClaims, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page State"


        Class MyState
            Public MyBO As DeniedClaims
            Public ScreenSnapShotBO As DeniedClaims
            Public moParams As Parameters
            Public SelectedSCId As Guid = Guid.Empty
            Public ServiceNetworkId As Guid = Guid.Empty
            Public SortExpressionDetailGrid As String = ServiceGroup.RiskTypeManufacturerDataView.RISK_TYPE_COL_NAME
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public IsNew As Boolean = True
            Public Drs As New ArrayList
            Public ReasonClosedId As Guid


            Public Sub New()

            End Sub
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                'Return CType(MyBase.State, MyState)
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                Else
                    If NavController.IsFlowEnded Then
                        'restart flow
                        Dim s As MyState = CType(NavController.State, MyState)
                        'Me.StartNavControl()
                        NavController.State = s
                    End If
                End If
                Return CType(NavController.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall



            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.MyBO = New DeniedClaims(CType(CallingParameters, Guid))

                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

#End Region
#Region "Page Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                ErrControllerMaster.Clear_Hide()
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    MenuEnabled = False
                    'Me.State.moParams = CType(Me.CallingParameters, Parameters)
                    State.moParams = CType(NavController.ParametersPassed, Parameters)
                    'Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                    If State.moParams.moIsnew Then
                        State.MyBO = New DeniedClaims
                        State.IsNew = True
                    Else
                        State.MyBO = New DeniedClaims(State.moParams.moClaimId)
                    End If
                    PopulateDropdowns()
                    PopulateFormFromBOs()

                    EnableDisableFields()
                End If
                BindBoPropertiesToLabels()
                'CheckIfComingFromSaveConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub
#End Region
#Region "Controlling Logic"

        Protected Sub EnableDisableFields()

            ControlMgr.SetEnableControl(Me, moBtnSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, LabelCertificate, True)
            ControlMgr.SetEnableControl(Me, TextboxShortDesc, True)
            ControlMgr.SetEnableControl(Me, LabelClaim, True)
            ControlMgr.SetEnableControl(Me, TextboxDescription, True)


            If Not State.moParams.moIsnew Then
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
                ControlMgr.SetEnableControl(Me, LabelClaim, False)
                ControlMgr.SetEnableControl(Me, TextboxShortDesc, False)
                ControlMgr.SetEnableControl(Me, LabelCertificate, False)
                ControlMgr.SetEnableControl(Me, TextboxDescription, False)
                ControlMgr.SetEnableControl(Me, LabelDate, False)
                ControlMgr.SetEnableControl(Me, TextboxDate, False)
                ControlMgr.SetEnableControl(Me, moAuthorizedApproverDrop, False)

            End If
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, "ApproverId", LabelAuthorizedApproverDrop)

            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Protected Sub PopulateDropdowns()
            Dim oAuthorizedApprover As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLADELETAUTHO", Thread.CurrentPrincipal.GetLanguageCode(), Nothing)
            moAuthorizedApproverDrop.Populate(oAuthorizedApprover, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })

            SetSelectedItem(moAuthorizedApproverDrop, State.MyBO.ApproverId)

        End Sub

#Region "Detail Grid"

        Sub PopulateUserControlAvailableDeniedReasons()
            Dim availableDv As DataView
            Dim selectedDv As DataView
            UserControlAvailableSelectedDeniedReasosns.BackColor = "#d5d6e4"
            ' ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDeniedReasosns, False)
            If Not State.MyBO.Id.Equals(Guid.Empty) Then

                If State.moParams.moIsnew Then
                    Dim availDS As DataSet = State.MyBO.GetAvailableDRs(State.moParams.moClaimId, Authentication.LangId)
                    If availDS.Tables.Count > 0 Then
                        availableDv = New DataView(availDS.Tables(0))
                    End If
                    UserControlAvailableSelectedDeniedReasosns.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                Else
                    Dim selectedDS As DataSet = State.MyBO.GetSelectedDRs(State.moParams.moClaimId, Authentication.LangId)
                    If selectedDS.Tables.Count > 0 Then
                        selectedDv = New DataView(selectedDS.Tables(0))
                    End If
                    UserControlAvailableSelectedDeniedReasosns.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    'EnableDisableControls(UserControlAvailableSelectedDeniedReasosns, True)

                End If
                UserControlAvailableSelectedDeniedReasosns.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_DENIAL_REASON)
                UserControlAvailableSelectedDeniedReasosns.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_DENIAL_REASON)

            End If
        End Sub






#End Region

        Protected Sub PopulateFormFromBOs()
            With State.MyBO

                If State.moParams.moIsnew Then
                    PopulateControlFromBOProperty(TextboxShortDesc, State.moParams.moCert)
                    PopulateControlFromBOProperty(TextboxDescription, State.moParams.moClaim)
                    PopulateControlFromBOProperty(TextboxDate, System.DateTime.Now)
                    EnableDisableControls(TextboxShortDesc, True)
                    EnableDisableControls(TextboxDescription, True)
                    EnableDisableControls(TextboxDate, True)
                    State.MyBO.ClaimId = State.moParams.moClaimbo.Id
                Else
                    PopulateControlFromBOProperty(TextboxShortDesc, State.moParams.moCert)
                    PopulateControlFromBOProperty(TextboxDescription, State.moParams.moClaim)
                    PopulateControlFromBOProperty(TextboxDate, State.MyBO.CreatedDate)
                    PopulateControlFromBOProperty(TextboxStandardEditing1, State.MyBO.ConditionProblem1)
                    PopulateControlFromBOProperty(TextboxStandardEditing2, State.MyBO.ConditionProblem2)
                    PopulateControlFromBOProperty(TextboxSpecialEditing, State.MyBO.ConditionProblem3)
                    EnableDisableControls(TextboxShortDesc, True)
                    EnableDisableControls(TextboxDescription, True)
                    EnableDisableControls(TextboxDate, True)
                    EnableDisableControls(TextboxStandardEditing1, True)
                    EnableDisableControls(TextboxStandardEditing2, True)
                    EnableDisableControls(TextboxSpecialEditing, True)
                    EnableDisableControls(UserControlAvailableSelectedDeniedReasosns, True)

                End If

                PopulateUserControlAvailableDeniedReasons()

            End With



        End Sub


        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            State.MyBO = New DeniedClaims
            State.IsNew = True
            PopulateFormFromBOs()
            EnableDisableFields()
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    State.MyBO.Save()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        'Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        'Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ErrControllerMaster.AddErrorAndShow(State.LastErrMsg)
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub


#End Region
#Region "Button Clicks"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try

                'Me.NavController.Navigate(Me, "back")

                If State.moParams.moClaimDenielLetters.Count = 0 Then
                    NavController.Navigate(Me, "back_info", New Claims.ClaimDeniedInformationForm.Parameters(State.moParams.moClaimbo, State.moParams.moClaimbo.Id, State.moParams.moCertId))
                Else
                    NavController.Navigate(Me, "back_list", New ClaimDeniedLetterListForm.Parameters(State.moParams.moClaimbo, State.moParams.moClaimDenielLetters, State.moParams.moClaimId, State.moParams.moClaim, State.moParams.moCert, State.moParams.moCertId))
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = ErrControllerMaster.Text
            End Try
        End Sub

        Private Sub moBtnCancelClick(sender As System.Object, e As System.EventArgs) Handles moBtnCancel.Click
            Try
                UndoChanges()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub UndoChanges()
            TextboxStandardEditing1.Text = String.Empty
            TextboxStandardEditing2.Text = String.Empty
            TextboxSpecialEditing.Text = String.Empty
            UserControlAvailableSelectedDeniedReasosns.SelectedListListBox.Items.Clear()

            PopulateFormFromBOs()
            PopulateDropdowns()
        End Sub

#End Region
#Region "Attach - Detach Event Handlers"

        Private Sub UserControlAvailableSelectedDeniedReasosns_Attach(aSrc As Generic.UserControlAvailableSelected, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDeniedReasosns.Attach
            Try
                If attachedList.Count > 0 Then
                    State.MyBO.AttachDeniedReason(attachedList)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedDeniedReasosns_Detach(aSrc As Generic.UserControlAvailableSelected, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDeniedReasosns.Detach
            Try
                If detachedList.Count > 0 Then
                    State.MyBO.DetachDeniedReason(detachedList)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region
        Protected Shared Sub NotifyChanges(navCtrl As INavigationController)
            navCtrl.FlowSession(FlowSessionKeys.SESSION_CHANGES_MADE_FLAG) = True
        End Sub

        Private Sub mpHoriz_SelectedIndexChange(sender As System.Object, e As System.EventArgs)

        End Sub

        Protected Sub moBtnSave_WRITE_Click(sender As Object, e As EventArgs) Handles moBtnSave_WRITE.Click
            ' Dim oClaim As New Claim(Me.State.moParams.moClaimId)

            'Dim oPriceGroupDetail As PriceGroupDetail
            Dim oNewComment As Comment
            Dim estimatePrice As New DecimalType(0)

            Try
                ' Me.PopulateBOsFormFrom()
                ' If Me.State.MyBO.IsFamilyDirty Then
                State.MyBO.ConditionProblem1 = TextboxStandardEditing1.Text
                State.MyBO.ConditionProblem2 = TextboxStandardEditing2.Text
                State.MyBO.ConditionProblem3 = TextboxSpecialEditing.Text
                PopulateBOProperty(State.MyBO, "ApproverId", moAuthorizedApproverDrop)
                State.MyBO.Save()
                State.IsNew = False
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                'Will work only for Single Auth 
                If (Me.State.moParams.moClaimbo.ClaimAuthorizationType = ClaimAuthorizationType.Single) Then
                    'oPriceGroupDetail = CType(Me.State.moParams.moClaimbo, Claim).GetCurrentPriceGroupDetail
                    Dim servCenter As New ServiceCenter(CType(State.moParams.moClaimbo, Claim).ServiceCenterId)
                    Dim equipConditionid As Guid
                    Dim equipmentId As Guid
                    Dim equipClassId As Guid

                    'get the equipment information'if equipment not used then get the prices based on risktypeid
                    If (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.moParams.moClaimbo.Dealer.UseEquipmentId) = Codes.YESNO_Y) Then
                        equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
                        If State.moParams.moClaimbo.ClaimedEquipment Is Nothing Then
                            Dim errors() As ValidationError = {New ValidationError(Codes.EQUIPMENT_NOT_FOUND, GetType(ClaimEquipment), Nothing, "", Nothing)}
                            Throw New BOValidationException(errors, GetType(ClaimEquipment).FullName, "Test")
                        End If
                        equipmentId = State.moParams.moClaimbo.ClaimedEquipment.EquipmentBO.Id
                        equipClassId = State.moParams.moClaimbo.ClaimedEquipment.EquipmentBO.EquipmentClassId
                    End If
                    'calculating the estimate price

                    Dim dvEstimate As DataView = PriceListDetail.GetPricesForServiceType(State.moParams.moClaimbo.CompanyId, servCenter.Code, State.moParams.moClaimbo.RiskTypeId, _
                                                 DateTime.Now, CType(State.moParams.moClaimbo, Claim).Certificate.SalesPrice.Value, _
                                                 LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, Codes.SERVICE_CLASS__REPAIR), _
                                                 LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, Codes.SERVICE_TYPE__ESTIMATE_PRICE), _
                                                 equipClassId, equipmentId, equipConditionid, State.moParams.moClaimbo.Dealer.Id, String.Empty)


                    If dvEstimate IsNot Nothing AndAlso dvEstimate.Count > 0 Then
                        estimatePrice = CDec(dvEstimate(0)(COL_PRICE_LIST))
                    End If

                    CType(State.moParams.moClaimbo, Claim).AuthorizedAmount = estimatePrice
                    If estimatePrice = 0 Then
                        State.moParams.moClaimbo.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED_DENIED)
                        State.moParams.moClaimbo.CloseTheClaim()
                    End If
                    NotifyChanges(NavController)
                    State.moParams.moClaimbo.Save()

                    '''''
                    ''If Not oPriceGroupDetail Is Nothing Then
                    ''    CType(Me.State.moParams.moClaimbo, Claim).AuthorizedAmount = oPriceGroupDetail.EstimatePrice
                    ''    If oPriceGroupDetail.EstimatePrice = 0 Then
                    ''        Me.State.moParams.moClaimbo.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED_DENIED)
                    ''        Me.State.moParams.moClaimbo.CloseTheClaim()
                    ''    End If
                    ''    Me.NotifyChanges(Me.NavController)
                    ''    Me.State.moParams.moClaimbo.Save()
                    ''End If
                End If
                oNewComment = Comment.GetNewComment(State.moParams.moCertId)
                oNewComment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.REASON_CLOSED_DENIED)
                NavController.FlowSession(FlowSessionKeys.SESSION_NEW_COMMENT) = oNewComment
                NavController.Navigate(Me, FlowEvents.EVENT_SAVE)


            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
    End Class

End Namespace
