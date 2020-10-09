Namespace Tables

    Partial Public Class RuleListDetailForm
        Inherits ElitaPlusSearchPage

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
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As RuleList, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
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

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    State.MyBO = New RuleList(CType(CallingParameters, Guid))
                    State.IsEditMode = True
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As RuleListDetailForm.ReturnType = CType(ReturnPar, RuleListDetailForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.DealerRuleListId = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Page_Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                ErrorCtrl.Clear_Hide()
                MenuEnabled = False

                If Not IsPostBack Then
                    AddCalendarwithTime(imgEffectiveDate, moEffectiveDateText)
                    AddCalendarwithTime(imgExpirationDate, moExpirationDateText)

                    If State.MyBO Is Nothing Then
                        State.MyBO = New RuleList
                    End If
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)

                    ErrorCtrl.Clear_Hide()
                    PopulateFormFromBOs()
                    EnableDisableFields(True)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                End If
                CheckIfComingFromSaveConfirm()
                BindBoPropertiesToLabels()
                AddLabelDecorations(State.MyBO)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub
#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFormFrom()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = ErrorCtrl.Text
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try

                PopulateBOsFormFrom()
                State.MyBO.Validate()
                If CheckRuleListOverlap() Then
                    If CheckExistingFutureRuleOverlap() Then
                        Throw New GUIException(Message.MSG_GUI_OVERLAPPING_RULES, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                    End If
                    DisplayMessage(Message.MSG_GUI_OVERLAPPING_RECORDS, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = DetailPageCommand.Accept
                    State.OverlapExists = True
                    Exit Sub
                End If

                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    State.HasDataChanged = False
                    PopulateFormFromBOs()
                    EnableDisableFields(True)
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New RuleList(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New RuleList
                End If
                PopulateFormFromBOs()
                EnableDisableFields(True)

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If State.MyBO.CheckIfListIsAssignedToDealer() Then
                    Throw New GUIException(Message.MSG_GUI_LIST_CODE_ASSIGNED_TO_DEALER_NO_DELETE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_CODE_ERR)
                Else
                    If State.MyBO.Effective.Value > DateTime.Now Then
                        State.MyBO.Delete()
                        State.MyBO.Save()
                        State.HasDataChanged = True
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
                    Else
                        'for current question - expire it by setting the expiry date
                        State.MyBO.Accept(New ExpirationVisitor)
                        State.MyBO.Save()
                        State.HasDataChanged = True
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Expire, State.MyBO, State.HasDataChanged))
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                'undo the delete
                State.MyBO.RejectChanges()
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                PopulateBOsFormFrom()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                    ClearGridHeadersAndLabelsErrSign()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
                PopulateBOsFormFrom()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Private Function CheckRuleListOverlap() As Boolean
            Return State.MyBO.Accept(New OverlapValidationVisitor)
        End Function

        Private Function CheckExistingFutureRuleOverlap() As Boolean
            Return State.MyBO.Accept(New FutureOverlapValidationVisitor)
        End Function


        Protected Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, "Code", moCodeLabel)
            BindBOPropertyToLabel(State.MyBO, "Description", moDescriptionLabel)
            BindBOPropertyToLabel(State.MyBO, "Effective", moEffectiveDateLabel)
            BindBOPropertyToLabel(State.MyBO, "Expiration", moExpirationDateLabel)
        End Sub

        Protected Sub PopulateBOsFormFrom()
            With State.MyBO
                PopulateBOProperty(State.MyBO, "Code", moCodeText)
                PopulateBOProperty(State.MyBO, "Description", moDescriptionText)
                PopulateBOProperty(State.MyBO, "Effective", moEffectiveDateText)
                PopulateBOProperty(State.MyBO, "Expiration", moExpirationDateText)
            End With

            'populate selection of rule, dealer and company to children
            State.MyBO.PopulateDealerList(UC_AvaSel_Dealer.SelectedList)
            State.MyBO.PopulateRuleList(UC_AvaSel_Rule.SelectedList)
            State.MyBO.PopulateCompanyList(UC_AvaSel_Company.SelectedList)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub PopulateFormFromBOs()
            With State.MyBO
                PopulateControlFromBOProperty(moCodeText, .Code)
                PopulateControlFromBOProperty(moDescriptionText, .Description)
                PopulateControlFromBOProperty(moEffectiveDateText, .Effective)
                PopulateControlFromBOProperty(moExpirationDateText, .Expiration)

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
                If State.MyBO.Expiration.Value < DateTime.Now Then
                    'disable everything
                    EnableDisableButtons(False)
                    'disable user control as well
                End If
            End With
        End Sub

        Sub EnableDisableButtons(enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
            ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, enableToggle)
        End Sub

        Protected Sub EnableDisableFields(toggle As Boolean)

            EnableDisableButtons(toggle)

            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            If State.MyBO.Expiration.Value < DateTime.Now Then
                If State.MyBO.IsNew OrElse State.IsEditMode Then
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

            If State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)

            End If


        End Sub

        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            State.MyBO = New RuleList
            PopulateFormFromBOs()
            State.IsEditMode = False
            EnableDisableFields(True)
        End Sub

        Protected Sub CreateNewWithCopy()
            State.IsACopy = True
            Dim newObj As New RuleList
            newObj.Copy(State.MyBO)

            State.MyBO = newObj
            PopulateFormFromBOs()
            State.ScreenSnapShotBO = New RuleList
            State.ScreenSnapShotBO.Clone(State.MyBO)
            State.IsACopy = False
            State.IsEditMode = False
            EnableDisableFields(True)
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                    BindBoPropertiesToLabels()
                    State.MyBO.Save()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        If State.MyBO.IsDirty Then
                            If State.OverlapExists Then
                                If State.MyBO.ExpireOverLappingQuestions() Then
                                    State.OverlapExists = False
                                End If
                            End If
                            State.MyBO.Save()
                            State.HasDataChanged = True
                            PopulateFormFromBOs()
                            DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        Else
                            DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        End If
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        EnableDisableFields(True)
                End Select
            End If
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        Protected Sub populateuserConctrols()

            'UC_AvaSel_Dealer
            UC_AvaSel_Dealer.SetAvailableData(State.MyBO.GetAvailableDealers(), "Description", "ID")
            UC_AvaSel_Dealer.SetSelectedData(State.MyBO.GetDealerRuleListSelectionView, "Description", "DEALER_ID")
            UC_AvaSel_Dealer.BackColor = "#d5d6e4"
            UC_AvaSel_Dealer.RemoveSelectedFromAvailable()
            'UC_AvaSel_Rule
            UC_AvaSel_Rule.SetAvailableData(State.MyBO.GetAvailableRules(), "Description", "ID")
            UC_AvaSel_Rule.SetSelectedData(State.MyBO.GetRuleListSelectionView, "Description", "RULE_ID")
            UC_AvaSel_Rule.BackColor = "#d5d6e4"
            UC_AvaSel_Rule.RemoveSelectedFromAvailable()
            'UC_AvaSel_Company
            UC_AvaSel_Company.SetAvailableData(State.MyBO.GetAvailableCompanys(), "Description", "ID")
            UC_AvaSel_Company.SetSelectedData(State.MyBO.GetCompanyRuleListSelectionView, "Description", "COMPANY_ID")
            UC_AvaSel_Company.BackColor = "#d5d6e4"
            UC_AvaSel_Company.RemoveSelectedFromAvailable()


        End Sub

#End Region

#Region "Error Handling"


#End Region

    End Class
End Namespace
