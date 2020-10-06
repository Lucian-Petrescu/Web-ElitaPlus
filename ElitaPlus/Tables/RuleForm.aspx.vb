Imports Microsoft.VisualBasic
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Tables

    Partial Public Class RuleForm
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
        Public Const URL As String = "RuleForm.aspx"
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Assurant.ElitaPlus.BusinessObjectsNew.Rule
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Assurant.ElitaPlus.BusinessObjectsNew.Rule, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public RuleId As Guid = Guid.Empty
            Public RuleTypeListlId As Guid = Guid.Empty
            Public RuleCategoryListId As Guid = Guid.Empty
            Public MyBO As Assurant.ElitaPlus.BusinessObjectsNew.Rule
            Public ScreenSnapShotBO As Assurant.ElitaPlus.BusinessObjectsNew.Rule

            Public MyChildBO As Issue
            Public ScreenSnapShotChildBO As Issue

            Public MyChildProcessBO As Process
            Public ScreenSnapShotChildDealerBO As Process

            Public IsACopy As Boolean

            Public Code As String = String.Empty
            Public Description As String = String.Empty
            Public EffectiveDate As DateType = Nothing
            Public ExpirationDate As DateType = Nothing

            Public IsRuleEditing As Boolean = False
            Public IsRuleIssueEditing As Boolean = False
            Public IsRuleProcessEditing As Boolean = False

            Public IsChildCreated As Boolean = False

            Public IsEditMode As Boolean = False
            Public IsInvalidEffective As Boolean = False
            Public IsInvalidExpiration As Boolean = False
            Public SelectedChildId As Guid = Guid.Empty

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public OverlapExists As Boolean = False
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
                    State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule(CType(CallingParameters, Guid))
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
                Dim retObj As RuleForm.ReturnType = CType(ReturnPar, RuleForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.RuleId = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub SaveGuiState()
            State.Code = moCodeText.Text
            State.Description = moDescriptionText.Text
            If moEffectiveDateText.Text IsNot String.Empty Then
                State.EffectiveDate = DateHelper.GetDateValue(moEffectiveDateText.Text)
            End If
            If moExpirationDateText.Text IsNot String.Empty Then
                State.ExpirationDate = DateHelper.GetDateValue(moExpirationDateText.Text)
            End If
        End Sub

        Private Sub RestoreGuiState()
            moCodeText.Text = State.MyBO.Code
            moDescriptionText.Text = State.MyBO.Description
            If State.MyBO.Effective IsNot Nothing Then
                moEffectiveDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(State.MyBO.Effective))
            Else
                moEffectiveDateText.Text = String.Empty
            End If
            If State.MyBO.Expiration IsNot Nothing Then
                moExpirationDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(State.MyBO.Expiration))
            Else
                moExpirationDateText.Text = String.Empty
            End If
        End Sub

#End Region

#Region "Page_Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                ErrorCtrl.Clear_Hide()
                MenuEnabled = False
                'Me.ValidateDates()

                If Not IsPostBack Then
                    AddCalendarwithTime(imgEffectiveDate, moEffectiveDateText)
                    AddCalendarwithTime(imgExpirationDate, moExpirationDateText)

                    If State.MyBO Is Nothing Then
                        State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule
                    Else
                        EnableHeaderControls(False)
                    End If

                    ErrorCtrl.Clear_Hide()
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                    If State.MyBO Is Nothing Then
                        State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule
                    End If

                    PopulateDropdowns()
                    PopulateFormFromBOs()
                    EnableDisableFields()

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

        Private Function CheckRuleOverlap() As Boolean
            Return State.MyBO.Accept(New OverlapValidationVisitor)
        End Function

        Private Function CheckExistingFutureRuleOverlap() As Boolean
            Return State.MyBO.Accept(New FutureOverlapValidationVisitor)
        End Function


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
                ClearGridHeadersAndLabelsErrSign()
                State.MyBO.Validate()
                If CheckRuleOverlap() Then
                    If CheckExistingFutureRuleOverlap() Then
                        Throw New GUIException(Message.MSG_GUI_OVERLAPPING_RULES, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                    End If
                    DisplayMessage(Message.MSG_GUI_OVERLAPPING_RULES, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = DetailPageCommand.Accept
                    State.OverlapExists = True
                    Exit Sub
                End If

                'If Me.State.OverlapExists Then
                '    If Me.State.MyBO.IsDirty Then
                '        If Me.State.MyBO.ExpireOverLappingRules() Then
                '            Me.State.MyChildBO.Save()
                '            Me.State.MyChildProcessBO.Save()
                '            Me.State.MyBO.Save()
                '            Me.State.HasDataChanged = False
                '            Me.PopulateFormFromBOs()
                '            Me.EnableDisableFields()
                '            Me.ClearGridHeadersAndLabelsErrSign()
                '            Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                '        End If
                '    Else
                '        Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                '    End If
                '    Me.State.OverlapExists = False
                'End If

                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    State.HasDataChanged = False
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    ClearGridHeadersAndLabelsErrSign()
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
                    State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule
                End If
                State.IsRuleEditing = False
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If State.MyBO.IsIssueAssignedtoRule Then
                    Throw New GUIException(Message.MSG_GUI_ISSUE_ASSIGNED_TO_RULE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_CODE_ERR)
                Else
                    If State.MyBO.Effective.Value > DateTime.Now Then
                        'for future effective date delete the Rule
                        State.MyBO.Delete()
                        State.MyBO.Save()
                    Else
                        'for current Rule - expire it by setting the expiry date
                        State.MyBO.Accept(New ExpirationVisitor)
                        State.MyBO.Save()
                    End If
                    State.HasDataChanged = True
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
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
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                PopulateBOsFormFrom()
                If State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Function IsListCodeDuplicate(code As String, effective As String,
                                        id As Guid) As Boolean

            If (QuestionList.CheckDuplicateQuestionListCode(code, CDate(effective).ToString(ElitaPlusPage.DATE_TIME_FORMAT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")), id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeOverlapped(code As String, effective As DateType,
                                        expiration As DateType, id As Guid) As Boolean

            If (QuestionList.CheckListCodeForOverlap(code, effective, expiration, id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeDurationOverlapped(code As String, effective As DateType,
                                        expiration As DateType, listId As Guid) As Boolean

            If (QuestionList.CheckListCodeDurationOverlap(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function ExpirePreviousList(code As String, effective As DateType,
                                        expiration As DateType, listId As Guid) As Boolean

            If (QuestionList.ExpirePreviousList(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Sub EnableUserControl(bVisible As Boolean)
            'Not neede for this form. REQ-860 

            'UserControlQuestionsAvailable.ShowCancelButton = True
            'UserControlQuestionsAvailable.ShowUpButton = True
            'UserControlQuestionsAvailable.ShowDownButton = True
            'UserControlDealerAvailable.ShowCancelButton = True
            'Dim qstn As New QuestionList
            'UserControlQuestionsAvailable.dvSelectedQuestions = qstn.GetSelectedQuestionList(Me.State.MyBO.Id)
            If Not String.IsNullOrEmpty(State.MyBO.Code) Then
                'UserControlDealerAvailable.dvSelectedDealer = qstn.GetSelectedDealertList(Me.State.MyBO.Code)
            End If
        End Sub

        Public Sub ValidateDates()
            Dim tempDate As DateTime = New DateTime

            If moEffectiveDateText.Text IsNot String.Empty Then
                If (DateHelper.IsDate(moEffectiveDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(moEffectiveDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End If
            End If

            If moExpirationDateText.Text IsNot String.Empty Then
                If (DateHelper.IsDate(moExpirationDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(moEffectiveDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End If
            End If

        End Sub

        Protected Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, "Code", moCodeLabel)
            BindBOPropertyToLabel(State.MyBO, "RuleType", moRuleTypeLabel)
            BindBOPropertyToLabel(State.MyBO, "Description", moDescriptionLabel)
            BindBOPropertyToLabel(State.MyBO, "Effective", moEffectiveDateLabel)
            BindBOPropertyToLabel(State.MyBO, "RuleCategory", moRuleCategoryLabel)
            BindBOPropertyToLabel(State.MyBO, "Expiration", moExpirationDateLabel)
            BindBOPropertyToLabel(State.MyBO, "RuleExecutionPoint", moRuleExecutionPointLabel)
        End Sub

        Protected Sub PopulateDropdowns()
            Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            ' Me.BindListControlToDataView(ddlRuleType, LookupListNew.GetRuleTypeLookupList(languageId))--RTYPE
            Dim ruleTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RTYPE", Thread.CurrentPrincipal.GetLanguageCode())
            ddlRuleType.Populate(ruleTypeLkl, New PopulateOptions() With
                                                              {
                                                                .AddBlankItem = True
                                                               })
            ' Me.BindListControlToDataView(ddlRuleCategory, LookupListNew.GetRuleCategoryLookupList(languageId))--RCAT
            Dim ruleCategoryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RCAT", Thread.CurrentPrincipal.GetLanguageCode())
            ddlRuleCategory.Populate(ruleCategoryLkl, New PopulateOptions() With
                                                              {
                                                                .AddBlankItem = True
                                                               })


            AddCalendarwithTime(imgEffectiveDate, moEffectiveDateText, , moEffectiveDateText.Text)
            AddCalendarwithTime(imgExpirationDate, moExpirationDateText, , moExpirationDateText.Text)
        End Sub

        Protected Sub PopulateBOsFormFrom()
            With State.MyBO
                PopulateBOProperty(State.MyBO, "Code", moCodeText)
                PopulateBOProperty(State.MyBO, "Description", moDescriptionText)
                PopulateBOProperty(State.MyBO, "Effective", moEffectiveDateText)
                PopulateBOProperty(State.MyBO, "Expiration", moExpirationDateText)
                PopulateBOProperty(State.MyBO, "RuleTypeId", ddlRuleType)
                PopulateBOProperty(State.MyBO, "RuleCategoryId", ddlRuleCategory)
                PopulateBOProperty(State.MyBO, "RuleExecutionPoint", moRuleExecutionPointText)
            End With

            State.MyBO.PopulateIssueList(IssueAvailableSelected.SelectedList)
            State.MyBO.PopulateProcessList(ProcessAvailableSelected.SelectedList)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub PopulateUserControls()
            'User Control Issues REQ-860
            IssueAvailableSelected.SetAvailableData(State.MyBO.GetAvailableIssues(), "Description", "ISSUE_ID")
            IssueAvailableSelected.SetSelectedData(State.MyBO.GetIssueRuleSelectionView(), "Description", "ISSUE_ID")
            IssueAvailableSelected.BackColor = "#d5d6e4"
            IssueAvailableSelected.RemoveSelectedFromAvailable()


            'User Control Processes REQ-860
            ProcessAvailableSelected.SetAvailableData(State.MyBO.GetAvailableProcesses(), "Description", "PROCESS_ID")
            ProcessAvailableSelected.SetSelectedData(State.MyBO.GetProcessRuleSelectionView(), "Description", "PROCESS_ID")
            ProcessAvailableSelected.BackColor = "#d5d6e4"
            ProcessAvailableSelected.RemoveSelectedFromAvailable()

        End Sub


        Protected Sub PopulateFormFromBOs()
            With State.MyBO
                PopulateControlFromBOProperty(moCodeText, .Code)
                PopulateControlFromBOProperty(moDescriptionText, .Description)
                PopulateControlFromBOProperty(ddlRuleCategory, .RuleCategoryId)
                PopulateControlFromBOProperty(ddlRuleType, .RuleTypeId)
                PopulateControlFromBOProperty(moEffectiveDateText, .Effective)
                PopulateControlFromBOProperty(moExpirationDateText, .Expiration)
                PopulateControlFromBOProperty(moRuleExecutionPointText, .RuleExecutionPoint)

                If .IsNew AndAlso Not .Effective.Value.Date < DateTime.Now.Date Then
                    ControlMgr.SetEnableControl(Me, moEffectiveDateText, True)
                    ControlMgr.SetEnableControl(Me, imgEffectiveDate, True)
                Else
                    ControlMgr.SetEnableControl(Me, moEffectiveDateText, False)
                    ControlMgr.SetEnableControl(Me, imgEffectiveDate, False)
                End If

                PopulateUserControls()

            End With

        End Sub

        Sub EnableHeaderControls(enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, moCodeText, enableToggle)
            ControlMgr.SetEnableControl(Me, moDescriptionText, enableToggle)
            ControlMgr.SetEnableControl(Me, moEffectiveDateText, enableToggle)
            ControlMgr.SetEnableControl(Me, moDescriptionText, Not (enableToggle))
            ControlMgr.SetEnableControl(Me, moEffectiveDateText, Not (enableToggle))
        End Sub

        Sub EnableDisableParentControls(enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
        End Sub

        Sub EnableDisableUserControlButtons(panel As WebControl, enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, panel, enableToggle)
        End Sub

        Protected Sub EnableDisableFields()
            'REQ-860
            If State.IsRuleEditing Then
                EnableDisableParentControls(False)
            Else
                EnableDisableParentControls(True)
            End If

            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            If State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, moCodeText, True)
                ControlMgr.SetEnableControl(Me, moDescriptionText, True)
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, True)

                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If
        End Sub

        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule
            PopulateFormFromBOs()
            EnableDisableFields()
            State.IsEditMode = False
        End Sub

        Protected Sub CreateNewWithCopy()

            State.IsACopy = True
            Dim newObj As New Assurant.ElitaPlus.BusinessObjectsNew.Rule
            newObj.Copy(State.MyBO)
            State.MyBO = newObj

            PopulateFormFromBOs()
            EnableDisableFields()

            State.IsRuleEditing = False

            State.ScreenSnapShotBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule
            State.ScreenSnapShotBO.Clone(State.MyBO)
            State.IsACopy = False
            State.IsEditMode = False
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
                        'If Me.State.MyBO.IsDirty Then
                        '    Me.State.MyBO.Save()
                        '    Me.State.HasDataChanged = True
                        '    Me.PopulateFormFromBOs()
                        '    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        'Else
                        '    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        'End If

                        If State.OverlapExists Then
                            If State.MyBO.IsDirty Then
                                If State.MyBO.ExpireOverLappingRules() Then
                                    'Me.State.MyChildBO.Save()
                                    'Me.State.MyChildProcessBO.Save()
                                    State.MyBO.Save()
                                    State.HasDataChanged = False
                                    PopulateFormFromBOs()
                                    EnableDisableFields()

                                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                                End If
                            Else
                                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                            End If
                            State.OverlapExists = False
                        Else
                            DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        End If
                        EnableDisableFields()
                        ''
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
                        EnableDisableFields()
                End Select
            End If
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub

#End Region

#Region "Error Handling"


#End Region

#Region "User Control Event Handler"

#End Region

    End Class

End Namespace
