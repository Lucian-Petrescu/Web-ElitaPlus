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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

            Public Sub New(ByVal oClaimbo As ClaimBase, ByVal oClaimId As Guid, ByVal oClaim As String, ByVal oCert As String, ByVal oCertId As Guid, ByVal oIsnew As Boolean, ByVal oClaimDenielLetters As Claims.LetterSearchDV)
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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As DeniedClaims, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
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
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                Else
                    If Me.NavController.IsFlowEnded Then
                        'restart flow
                        Dim s As MyState = CType(Me.NavController.State, MyState)
                        'Me.StartNavControl()
                        Me.NavController.State = s
                    End If
                End If
                Return CType(Me.NavController.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall



            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = New DeniedClaims(CType(Me.CallingParameters, Guid))

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

#End Region
#Region "Page Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.ErrControllerMaster.Clear_Hide()
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.MenuEnabled = False
                    'Me.State.moParams = CType(Me.CallingParameters, Parameters)
                    Me.State.moParams = CType(Me.NavController.ParametersPassed, Parameters)
                    'Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                    If Me.State.moParams.moIsnew Then
                        Me.State.MyBO = New DeniedClaims
                        Me.State.IsNew = True
                    Else
                        Me.State.MyBO = New DeniedClaims(Me.State.moParams.moClaimId)
                    End If
                    Me.PopulateDropdowns()
                    Me.PopulateFormFromBOs()

                    Me.EnableDisableFields()
                End If
                BindBoPropertiesToLabels()
                'CheckIfComingFromSaveConfirm()
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub
#End Region
#Region "Controlling Logic"

        Protected Sub EnableDisableFields()

            ControlMgr.SetEnableControl(Me, moBtnSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, LabelCertificate, True)
            ControlMgr.SetEnableControl(Me, TextboxShortDesc, True)
            ControlMgr.SetEnableControl(Me, LabelClaim, True)
            ControlMgr.SetEnableControl(Me, TextboxDescription, True)


            If Not Me.State.moParams.moIsnew Then
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
                ControlMgr.SetEnableControl(Me, LabelClaim, False)
                ControlMgr.SetEnableControl(Me, TextboxShortDesc, False)
                ControlMgr.SetEnableControl(Me, LabelCertificate, False)
                ControlMgr.SetEnableControl(Me, TextboxDescription, False)
                ControlMgr.SetEnableControl(Me, LabelDate, False)
                ControlMgr.SetEnableControl(Me, TextboxDate, False)
                ControlMgr.SetEnableControl(Me, Me.moAuthorizedApproverDrop, False)

            End If
        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ApproverId", Me.LabelAuthorizedApproverDrop)

            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Protected Sub PopulateDropdowns()
            Dim oAuthorizedApprover As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLADELETAUTHO", Thread.CurrentPrincipal.GetLanguageCode(), Nothing)
            moAuthorizedApproverDrop.Populate(oAuthorizedApprover, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = True
                                                   })

            Me.SetSelectedItem(Me.moAuthorizedApproverDrop, Me.State.MyBO.ApproverId)

        End Sub

#Region "Detail Grid"

        Sub PopulateUserControlAvailableDeniedReasons()
            Dim availableDv As DataView
            Dim selectedDv As DataView
            Me.UserControlAvailableSelectedDeniedReasosns.BackColor = "#d5d6e4"
            ' ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDeniedReasosns, False)
            If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then

                If Me.State.moParams.moIsnew Then
                    Dim availDS As DataSet = Me.State.MyBO.GetAvailableDRs(Me.State.moParams.moClaimId, Authentication.LangId)
                    If availDS.Tables.Count > 0 Then
                        availableDv = New DataView(availDS.Tables(0))
                    End If
                    Me.UserControlAvailableSelectedDeniedReasosns.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                Else
                    Dim selectedDS As DataSet = Me.State.MyBO.GetSelectedDRs(Me.State.moParams.moClaimId, Authentication.LangId)
                    If selectedDS.Tables.Count > 0 Then
                        selectedDv = New DataView(selectedDS.Tables(0))
                    End If
                    Me.UserControlAvailableSelectedDeniedReasosns.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    'EnableDisableControls(UserControlAvailableSelectedDeniedReasosns, True)

                End If
                Me.UserControlAvailableSelectedDeniedReasosns.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_DENIAL_REASON)
                Me.UserControlAvailableSelectedDeniedReasosns.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_DENIAL_REASON)

            End If
        End Sub






#End Region

        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO

                If Me.State.moParams.moIsnew Then
                    Me.PopulateControlFromBOProperty(Me.TextboxShortDesc, Me.State.moParams.moCert)
                    Me.PopulateControlFromBOProperty(Me.TextboxDescription, Me.State.moParams.moClaim)
                    Me.PopulateControlFromBOProperty(Me.TextboxDate, System.DateTime.Now)
                    EnableDisableControls(Me.TextboxShortDesc, True)
                    EnableDisableControls(Me.TextboxDescription, True)
                    EnableDisableControls(Me.TextboxDate, True)
                    Me.State.MyBO.ClaimId = Me.State.moParams.moClaimbo.Id
                Else
                    Me.PopulateControlFromBOProperty(Me.TextboxShortDesc, Me.State.moParams.moCert)
                    Me.PopulateControlFromBOProperty(Me.TextboxDescription, Me.State.moParams.moClaim)
                    Me.PopulateControlFromBOProperty(Me.TextboxDate, Me.State.MyBO.CreatedDate)
                    Me.PopulateControlFromBOProperty(Me.TextboxStandardEditing1, Me.State.MyBO.ConditionProblem1)
                    Me.PopulateControlFromBOProperty(Me.TextboxStandardEditing2, Me.State.MyBO.ConditionProblem2)
                    Me.PopulateControlFromBOProperty(Me.TextboxSpecialEditing, Me.State.MyBO.ConditionProblem3)
                    EnableDisableControls(Me.TextboxShortDesc, True)
                    EnableDisableControls(Me.TextboxDescription, True)
                    EnableDisableControls(Me.TextboxDate, True)
                    EnableDisableControls(Me.TextboxStandardEditing1, True)
                    EnableDisableControls(Me.TextboxStandardEditing2, True)
                    EnableDisableControls(Me.TextboxSpecialEditing, True)
                    EnableDisableControls(Me.UserControlAvailableSelectedDeniedReasosns, True)

                End If

                PopulateUserControlAvailableDeniedReasons()

            End With



        End Sub


        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            Me.State.MyBO = New DeniedClaims
            Me.State.IsNew = True
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    Me.State.MyBO.Save()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        'Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        'Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ErrControllerMaster.AddErrorAndShow(Me.State.LastErrMsg)
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub


#End Region
#Region "Button Clicks"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try

                'Me.NavController.Navigate(Me, "back")

                If Me.State.moParams.moClaimDenielLetters.Count = 0 Then
                    Me.NavController.Navigate(Me, "back_info", New Claims.ClaimDeniedInformationForm.Parameters(Me.State.moParams.moClaimbo, Me.State.moParams.moClaimbo.Id, Me.State.moParams.moCertId))
                Else
                    Me.NavController.Navigate(Me, "back_list", New ClaimDeniedLetterListForm.Parameters(Me.State.moParams.moClaimbo, Me.State.moParams.moClaimDenielLetters, Me.State.moParams.moClaimId, Me.State.moParams.moClaim, Me.State.moParams.moCert, Me.State.moParams.moCertId))
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.ErrControllerMaster.Text
            End Try
        End Sub

        Private Sub moBtnCancelClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnCancel.Click
            Try
                UndoChanges()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub UndoChanges()
            Me.TextboxStandardEditing1.Text = String.Empty
            Me.TextboxStandardEditing2.Text = String.Empty
            Me.TextboxSpecialEditing.Text = String.Empty
            Me.UserControlAvailableSelectedDeniedReasosns.SelectedListListBox.Items.Clear()

            PopulateFormFromBOs()
            Me.PopulateDropdowns()
        End Sub

#End Region
#Region "Attach - Detach Event Handlers"

        Private Sub UserControlAvailableSelectedDeniedReasosns_Attach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDeniedReasosns.Attach
            Try
                If attachedList.Count > 0 Then
                    Me.State.MyBO.AttachDeniedReason(attachedList)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedDeniedReasosns_Detach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedDeniedReasosns.Detach
            Try
                If detachedList.Count > 0 Then
                    Me.State.MyBO.DetachDeniedReason(detachedList)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region
        Protected Shared Sub NotifyChanges(ByVal navCtrl As INavigationController)
            navCtrl.FlowSession(FlowSessionKeys.SESSION_CHANGES_MADE_FLAG) = True
        End Sub

        Private Sub mpHoriz_SelectedIndexChange(ByVal sender As System.Object, ByVal e As System.EventArgs)

        End Sub

        Protected Sub moBtnSave_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moBtnSave_WRITE.Click
            ' Dim oClaim As New Claim(Me.State.moParams.moClaimId)

            'Dim oPriceGroupDetail As PriceGroupDetail
            Dim oNewComment As Comment
            Dim estimatePrice As New DecimalType(0)

            Try
                ' Me.PopulateBOsFormFrom()
                ' If Me.State.MyBO.IsFamilyDirty Then
                Me.State.MyBO.ConditionProblem1 = TextboxStandardEditing1.Text
                Me.State.MyBO.ConditionProblem2 = TextboxStandardEditing2.Text
                Me.State.MyBO.ConditionProblem3 = TextboxSpecialEditing.Text
                Me.PopulateBOProperty(Me.State.MyBO, "ApproverId", moAuthorizedApproverDrop)
                Me.State.MyBO.Save()
                Me.State.IsNew = False
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                'Will work only for Single Auth 
                If (Me.State.moParams.moClaimbo.ClaimAuthorizationType = ClaimAuthorizationType.Single) Then
                    'oPriceGroupDetail = CType(Me.State.moParams.moClaimbo, Claim).GetCurrentPriceGroupDetail
                    Dim servCenter As New ServiceCenter(CType(Me.State.moParams.moClaimbo, Claim).ServiceCenterId)
                    Dim equipConditionid As Guid
                    Dim equipmentId As Guid
                    Dim equipClassId As Guid

                    'get the equipment information'if equipment not used then get the prices based on risktypeid
                    If (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.moParams.moClaimbo.Dealer.UseEquipmentId) = Codes.YESNO_Y) Then
                        equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
                        If Me.State.moParams.moClaimbo.ClaimedEquipment Is Nothing Then
                            Dim errors() As ValidationError = {New ValidationError(Codes.EQUIPMENT_NOT_FOUND, GetType(ClaimEquipment), Nothing, "", Nothing)}
                            Throw New BOValidationException(errors, GetType(ClaimEquipment).FullName, "Test")
                        End If
                        equipmentId = Me.State.moParams.moClaimbo.ClaimedEquipment.EquipmentBO.Id
                        equipClassId = Me.State.moParams.moClaimbo.ClaimedEquipment.EquipmentBO.EquipmentClassId
                    End If
                    'calculating the estimate price

                    Dim dvEstimate As DataView = PriceListDetail.GetPricesForServiceType(Me.State.moParams.moClaimbo.CompanyId, servCenter.Code, Me.State.moParams.moClaimbo.RiskTypeId, _
                                                 DateTime.Now, CType(Me.State.moParams.moClaimbo, Claim).Certificate.SalesPrice.Value, _
                                                 LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS, Codes.SERVICE_CLASS__REPAIR), _
                                                 LookupListNew.GetIdFromCode(Codes.SERVICE_CLASS_TYPE, Codes.SERVICE_TYPE__ESTIMATE_PRICE), _
                                                 equipClassId, equipmentId, equipConditionid, Me.State.moParams.moClaimbo.Dealer.Id, String.Empty)


                    If Not dvEstimate Is Nothing AndAlso dvEstimate.Count > 0 Then
                        estimatePrice = CDec(dvEstimate(0)(COL_PRICE_LIST))
                    End If

                    CType(Me.State.moParams.moClaimbo, Claim).AuthorizedAmount = estimatePrice
                    If estimatePrice = 0 Then
                        Me.State.moParams.moClaimbo.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED_DENIED)
                        Me.State.moParams.moClaimbo.CloseTheClaim()
                    End If
                    Me.NotifyChanges(Me.NavController)
                    Me.State.moParams.moClaimbo.Save()

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
                oNewComment = Comment.GetNewComment(Me.State.moParams.moCertId)
                oNewComment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.REASON_CLOSED_DENIED)
                Me.NavController.FlowSession(FlowSessionKeys.SESSION_NEW_COMMENT) = oNewComment
                Me.NavController.Navigate(Me, FlowEvents.EVENT_SAVE)


            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
    End Class

End Namespace
