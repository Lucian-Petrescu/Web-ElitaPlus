Namespace Tables

    Partial Public Class RuleListDetailForm
        Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"
        Public Const URL As String = "RuleListDetailForm.aspx"
        Private Const GRID_COL_EDIT As Integer = 0
        Private Const GRID_COL_RULE_LIST_ID_IDX As Integer = 1
        Private Const GRID_COL_CODE_IDX As Integer = 2
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 3
        Private Const GRID_COL_EFFECTIVE_IDX As Integer = 4
        Private Const GRID_COL_EXPIRATION_IDX As Integer = 5
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As RuleList
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As RuleList, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public DealerRuleListId As Guid = Guid.Empty
            Public MyBO As RuleList
            Public ScreenSnapShotBO As RuleList

            Public MyChildBO As DealerRuleList
            Public ScreenSnapShotChildBO As DealerRuleList

            Public MyChildDealerBO As Dealer
            Public ScreenSnapShotChildDealerBO As Dealer

            Public IsACopy As Boolean

            Public Code As String = String.Empty
            Public Description As String = String.Empty
            Public EffectiveDate As DateType = Nothing
            Public ExpirationDate As DateType = Nothing
            Public IsDealerListEditing As Boolean = False
            Public OverlapExists As Boolean = False
            Public IsEditMode As Boolean = False
            Public SelectedChildId As Guid = Guid.Empty

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean

            Sub New()
            End Sub
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Me.State.MyBO = New RuleList(CType(Me.CallingParameters, Guid))
                    Me.State.IsEditMode = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As RuleListDetailForm.ReturnType = CType(ReturnPar, RuleListDetailForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.DealerRuleListId = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Page_Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.ErrorCtrl.Clear_Hide()
                Me.MenuEnabled = False

                If Not Me.IsPostBack Then
                    Me.AddCalendarwithTime(Me.imgEffectiveDate, Me.moEffectiveDateText)
                    Me.AddCalendarwithTime(Me.imgExpirationDate, Me.moExpirationDateText)

                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New RuleList
                    End If
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)

                    Me.ErrorCtrl.Clear_Hide()
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields(True)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                End If
                Me.CheckIfComingFromSaveConfirm()
                Me.BindBoPropertiesToLabels()
                Me.AddLabelDecorations(Me.State.MyBO)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub
#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFormFrom()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.ErrorCtrl.Text
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try

                Me.PopulateBOsFormFrom()
                Me.State.MyBO.Validate()
                If Me.CheckRuleListOverlap() Then
                    If Me.CheckExistingFutureRuleOverlap() Then
                        Throw New GUIException(Message.MSG_GUI_OVERLAPPING_RULES, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                    End If
                    Me.DisplayMessage(Message.MSG_GUI_OVERLAPPING_RECORDS, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = DetailPageCommand.Accept
                    Me.State.OverlapExists = True
                    Exit Sub
                End If

                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = False
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields(True)
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New RuleList(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New RuleList
                End If
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields(True)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If Me.State.MyBO.CheckIfListIsAssignedToDealer() Then
                    Throw New GUIException(Message.MSG_GUI_LIST_CODE_ASSIGNED_TO_DEALER_NO_DELETE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_CODE_ERR)
                Else
                    If Me.State.MyBO.Effective.Value > DateTime.Now Then
                        Me.State.MyBO.Delete()
                        Me.State.MyBO.Save()
                        Me.State.HasDataChanged = True
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
                    Else
                        'for current question - expire it by setting the expiry date
                        Me.State.MyBO.Accept(New ExpirationVisitor)
                        Me.State.MyBO.Save()
                        Me.State.HasDataChanged = True
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Expire, Me.State.MyBO, Me.State.HasDataChanged))
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                'undo the delete
                Me.State.MyBO.RejectChanges()
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.PopulateBOsFormFrom()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.CreateNew()
                    Me.ClearGridHeadersAndLabelsErrSign()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    Me.CreateNewWithCopy()
                End If
                Me.PopulateBOsFormFrom()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Private Function CheckRuleListOverlap() As Boolean
            Return Me.State.MyBO.Accept(New OverlapValidationVisitor)
        End Function

        Private Function CheckExistingFutureRuleOverlap() As Boolean
            Return Me.State.MyBO.Accept(New FutureOverlapValidationVisitor)
        End Function


        Protected Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.moCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.moDescriptionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Effective", Me.moEffectiveDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Expiration", Me.moExpirationDateLabel)
        End Sub

        Protected Sub PopulateBOsFormFrom()
            With Me.State.MyBO
                Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.moCodeText)
                Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.moDescriptionText)
                Me.PopulateBOProperty(Me.State.MyBO, "Effective", Me.moEffectiveDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "Expiration", Me.moExpirationDateText)
            End With

            'populate selection of rule, dealer and company to children
            Me.State.MyBO.PopulateDealerList(UC_AvaSel_Dealer.SelectedList)
            Me.State.MyBO.PopulateRuleList(UC_AvaSel_Rule.SelectedList)
            Me.State.MyBO.PopulateCompanyList(UC_AvaSel_Company.SelectedList)

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(Me.moCodeText, .Code)
                Me.PopulateControlFromBOProperty(Me.moDescriptionText, .Description)
                Me.PopulateControlFromBOProperty(Me.moEffectiveDateText, .Effective)
                Me.PopulateControlFromBOProperty(Me.moExpirationDateText, .Expiration)

                If .IsNew AndAlso Not .Effective.Value.Date < DateTime.Now.Date Then
                    ControlMgr.SetEnableControl(Me, moEffectiveDateText, True)
                    ControlMgr.SetEnableControl(Me, imgEffectiveDate, True)
                    ControlMgr.SetEnableControl(Me, moCodeText, True)
                Else
                    ControlMgr.SetEnableControl(Me, moEffectiveDateText, False)
                    ControlMgr.SetEnableControl(Me, imgEffectiveDate, False)
                    ControlMgr.SetEnableControl(Me, moCodeText, False)
                End If
                populateuserConctrols()

                'if the rule list is expired then lock the form preventing change
                If Me.State.MyBO.Expiration.Value < DateTime.Now Then
                    'disable everything
                    EnableDisableButtons(False)
                    'disable user control as well
                End If
            End With
        End Sub

        Sub EnableDisableButtons(ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
            ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, enableToggle)
        End Sub

        Protected Sub EnableDisableFields(ByVal toggle As Boolean)

            Me.EnableDisableButtons(toggle)

            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            If Me.State.MyBO.Expiration.Value < DateTime.Now Then
                If Me.State.MyBO.IsNew OrElse Me.State.IsEditMode Then
                    ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                    ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                    ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
                    ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
                    ControlMgr.SetEnableControl(Me, moEffectiveDateText, False)
                    ControlMgr.SetEnableControl(Me, imgEffectiveDate, False)
                    ControlMgr.SetEnableControl(Me, moCodeText, False)
                    ControlMgr.SetEnableControl(Me, moExpirationDateText, False)
                    ControlMgr.SetEnableControl(Me, imgExpirationDate, False)
                    ControlMgr.SetEnableControl(Me, moDescriptionText, False)
                    ControlMgr.SetEnableControl(Me, pnlRuleDealer, False)
                End If
            End If

            If Me.State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)

            End If


        End Sub

        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            Me.State.MyBO = New RuleList
            Me.PopulateFormFromBOs()
            Me.State.IsEditMode = False
            Me.EnableDisableFields(True)
        End Sub

        Protected Sub CreateNewWithCopy()
            Me.State.IsACopy = True
            Dim newObj As New RuleList
            newObj.Copy(Me.State.MyBO)

            Me.State.MyBO = newObj
            Me.PopulateFormFromBOs()
            Me.State.ScreenSnapShotBO = New RuleList
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
            Me.State.IsACopy = False
            Me.State.IsEditMode = False
            Me.EnableDisableFields(True)
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                    Me.BindBoPropertiesToLabels()
                    Me.State.MyBO.Save()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        If Me.State.MyBO.IsDirty Then
                            If Me.State.OverlapExists Then
                                If Me.State.MyBO.ExpireOverLappingQuestions() Then
                                    Me.State.OverlapExists = False
                                End If
                            End If
                            Me.State.MyBO.Save()
                            Me.State.HasDataChanged = True
                            Me.PopulateFormFromBOs()
                            Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        Else
                            Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        End If
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        Me.EnableDisableFields(True)
                End Select
            End If
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        Protected Sub populateuserConctrols()

            'UC_AvaSel_Dealer
            UC_AvaSel_Dealer.SetAvailableData(Me.State.MyBO.GetAvailableDealers(), "Description", "ID")
            UC_AvaSel_Dealer.SetSelectedData(Me.State.MyBO.GetDealerRuleListSelectionView, "Description", "DEALER_ID")
            UC_AvaSel_Dealer.BackColor = "#d5d6e4"
            UC_AvaSel_Dealer.RemoveSelectedFromAvailable()
            'UC_AvaSel_Rule
            UC_AvaSel_Rule.SetAvailableData(Me.State.MyBO.GetAvailableRules(), "Description", "ID")
            UC_AvaSel_Rule.SetSelectedData(Me.State.MyBO.GetRuleListSelectionView, "Description", "RULE_ID")
            UC_AvaSel_Rule.BackColor = "#d5d6e4"
            UC_AvaSel_Rule.RemoveSelectedFromAvailable()
            'UC_AvaSel_Company
            UC_AvaSel_Company.SetAvailableData(Me.State.MyBO.GetAvailableCompanys(), "Description", "ID")
            UC_AvaSel_Company.SetSelectedData(Me.State.MyBO.GetCompanyRuleListSelectionView, "Description", "COMPANY_ID")
            UC_AvaSel_Company.BackColor = "#d5d6e4"
            UC_AvaSel_Company.RemoveSelectedFromAvailable()


        End Sub

#End Region

#Region "Error Handling"


#End Region

    End Class
End Namespace
