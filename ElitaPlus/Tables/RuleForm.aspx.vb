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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Assurant.ElitaPlus.BusinessObjectsNew.Rule, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Me.State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule(CType(Me.CallingParameters, Guid))
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
                Dim retObj As RuleForm.ReturnType = CType(ReturnPar, RuleForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.RuleId = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub SaveGuiState()
            Me.State.Code = Me.moCodeText.Text
            Me.State.Description = Me.moDescriptionText.Text
            If Not Me.moEffectiveDateText.Text Is String.Empty Then
                Me.State.EffectiveDate = DateHelper.GetDateValue(Me.moEffectiveDateText.Text)
            End If
            If Not Me.moExpirationDateText.Text Is String.Empty Then
                Me.State.ExpirationDate = DateHelper.GetDateValue(Me.moExpirationDateText.Text)
            End If
        End Sub

        Private Sub RestoreGuiState()
            Me.moCodeText.Text = Me.State.MyBO.Code
            Me.moDescriptionText.Text = Me.State.MyBO.Description
            If Not Me.State.MyBO.Effective Is Nothing Then
                Me.moEffectiveDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(Me.State.MyBO.Effective))
            Else
                Me.moEffectiveDateText.Text = String.Empty
            End If
            If Not Me.State.MyBO.Expiration Is Nothing Then
                Me.moExpirationDateText.Text = ElitaPlusPage.GetLongDate12FormattedString(CDate(Me.State.MyBO.Expiration))
            Else
                Me.moExpirationDateText.Text = String.Empty
            End If
        End Sub

#End Region

#Region "Page_Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.ErrorCtrl.Clear_Hide()
                Me.MenuEnabled = False
                'Me.ValidateDates()

                If Not Me.IsPostBack Then
                    Me.AddCalendarwithTime(Me.imgEffectiveDate, Me.moEffectiveDateText)
                    Me.AddCalendarwithTime(Me.imgExpirationDate, Me.moExpirationDateText)

                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule
                    Else
                        Me.EnableHeaderControls(False)
                    End If

                    Me.ErrorCtrl.Clear_Hide()
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule
                    End If

                    PopulateDropdowns()
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()

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

        Private Function CheckRuleOverlap() As Boolean
            Return Me.State.MyBO.Accept(New OverlapValidationVisitor)
        End Function

        Private Function CheckExistingFutureRuleOverlap() As Boolean
            Return Me.State.MyBO.Accept(New FutureOverlapValidationVisitor)
        End Function


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
                Me.ClearGridHeadersAndLabelsErrSign()
                Me.State.MyBO.Validate()
                If Me.CheckRuleOverlap() Then
                    If Me.CheckExistingFutureRuleOverlap() Then
                        Throw New GUIException(Message.MSG_GUI_OVERLAPPING_RULES, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                    End If
                    Me.DisplayMessage(Message.MSG_GUI_OVERLAPPING_RULES, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = DetailPageCommand.Accept
                    Me.State.OverlapExists = True
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

                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = False
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                    Me.ClearGridHeadersAndLabelsErrSign()
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
                    Me.State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule
                End If
                Me.State.IsRuleEditing = False
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If Me.State.MyBO.IsIssueAssignedtoRule Then
                    Throw New GUIException(Message.MSG_GUI_ISSUE_ASSIGNED_TO_RULE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_CODE_ERR)
                Else
                    If Me.State.MyBO.Effective.Value > DateTime.Now Then
                        'for future effective date delete the Rule
                        Me.State.MyBO.Delete()
                        Me.State.MyBO.Save()
                    Else
                        'for current Rule - expire it by setting the expiry date
                        Me.State.MyBO.Accept(New ExpirationVisitor)
                        Me.State.MyBO.Save()
                    End If
                    Me.State.HasDataChanged = True
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
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
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                Me.PopulateBOsFormFrom()
                If Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    Me.CreateNewWithCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Function IsListCodeDuplicate(ByVal code As String, ByVal effective As String,
                                        ByVal id As Guid) As Boolean

            If (QuestionList.CheckDuplicateQuestionListCode(code, CDate(effective).ToString(ElitaPlusPage.DATE_TIME_FORMAT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")), id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeOverlapped(ByVal code As String, ByVal effective As DateType,
                                        ByVal expiration As DateType, ByVal id As Guid) As Boolean

            If (QuestionList.CheckListCodeForOverlap(code, effective, expiration, id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeDurationOverlapped(ByVal code As String, ByVal effective As DateType,
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

            If (QuestionList.CheckListCodeDurationOverlap(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function ExpirePreviousList(ByVal code As String, ByVal effective As DateType,
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

            If (QuestionList.ExpirePreviousList(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Sub EnableUserControl(ByVal bVisible As Boolean)
            'Not neede for this form. REQ-860 

            'UserControlQuestionsAvailable.ShowCancelButton = True
            'UserControlQuestionsAvailable.ShowUpButton = True
            'UserControlQuestionsAvailable.ShowDownButton = True
            'UserControlDealerAvailable.ShowCancelButton = True
            'Dim qstn As New QuestionList
            'UserControlQuestionsAvailable.dvSelectedQuestions = qstn.GetSelectedQuestionList(Me.State.MyBO.Id)
            If Not String.IsNullOrEmpty(Me.State.MyBO.Code) Then
                'UserControlDealerAvailable.dvSelectedDealer = qstn.GetSelectedDealertList(Me.State.MyBO.Code)
            End If
        End Sub

        Public Sub ValidateDates()
            Dim tempDate As DateTime = New DateTime

            If Not moEffectiveDateText.Text Is String.Empty Then
                If (DateHelper.IsDate(moEffectiveDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(Me.moEffectiveDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End If
            End If

            If Not moExpirationDateText.Text Is String.Empty Then
                If (DateHelper.IsDate(moExpirationDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(Me.moEffectiveDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End If
            End If

        End Sub

        Protected Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.moCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RuleType", Me.moRuleTypeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.moDescriptionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Effective", Me.moEffectiveDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RuleCategory", Me.moRuleCategoryLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Expiration", Me.moExpirationDateLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RuleExecutionPoint", Me.moRuleExecutionPointLabel)
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


            Me.AddCalendarwithTime(imgEffectiveDate, moEffectiveDateText, , moEffectiveDateText.Text)
            Me.AddCalendarwithTime(imgExpirationDate, moExpirationDateText, , moExpirationDateText.Text)
        End Sub

        Protected Sub PopulateBOsFormFrom()
            With Me.State.MyBO
                Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.moCodeText)
                Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.moDescriptionText)
                Me.PopulateBOProperty(Me.State.MyBO, "Effective", Me.moEffectiveDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "Expiration", Me.moExpirationDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "RuleTypeId", Me.ddlRuleType)
                Me.PopulateBOProperty(Me.State.MyBO, "RuleCategoryId", Me.ddlRuleCategory)
                Me.PopulateBOProperty(Me.State.MyBO, "RuleExecutionPoint", Me.moRuleExecutionPointText)
            End With

            Me.State.MyBO.PopulateIssueList(IssueAvailableSelected.SelectedList)
            Me.State.MyBO.PopulateProcessList(ProcessAvailableSelected.SelectedList)

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub PopulateUserControls()
            'User Control Issues REQ-860
            IssueAvailableSelected.SetAvailableData(Me.State.MyBO.GetAvailableIssues(), "Description", "ISSUE_ID")
            IssueAvailableSelected.SetSelectedData(Me.State.MyBO.GetIssueRuleSelectionView(), "Description", "ISSUE_ID")
            IssueAvailableSelected.BackColor = "#d5d6e4"
            IssueAvailableSelected.RemoveSelectedFromAvailable()


            'User Control Processes REQ-860
            ProcessAvailableSelected.SetAvailableData(Me.State.MyBO.GetAvailableProcesses(), "Description", "PROCESS_ID")
            ProcessAvailableSelected.SetSelectedData(Me.State.MyBO.GetProcessRuleSelectionView(), "Description", "PROCESS_ID")
            ProcessAvailableSelected.BackColor = "#d5d6e4"
            ProcessAvailableSelected.RemoveSelectedFromAvailable()

        End Sub


        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(Me.moCodeText, .Code)
                Me.PopulateControlFromBOProperty(Me.moDescriptionText, .Description)
                Me.PopulateControlFromBOProperty(ddlRuleCategory, .RuleCategoryId)
                Me.PopulateControlFromBOProperty(ddlRuleType, .RuleTypeId)
                Me.PopulateControlFromBOProperty(moEffectiveDateText, .Effective)
                Me.PopulateControlFromBOProperty(moExpirationDateText, .Expiration)
                Me.PopulateControlFromBOProperty(moRuleExecutionPointText, .RuleExecutionPoint)

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

        Sub EnableHeaderControls(ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, moCodeText, enableToggle)
            ControlMgr.SetEnableControl(Me, moDescriptionText, enableToggle)
            ControlMgr.SetEnableControl(Me, moEffectiveDateText, enableToggle)
            ControlMgr.SetEnableControl(Me, moDescriptionText, Not (enableToggle))
            ControlMgr.SetEnableControl(Me, moEffectiveDateText, Not (enableToggle))
        End Sub

        Sub EnableDisableParentControls(ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
        End Sub

        Sub EnableDisableUserControlButtons(ByVal panel As WebControl, ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, panel, enableToggle)
        End Sub

        Protected Sub EnableDisableFields()
            'REQ-860
            If Me.State.IsRuleEditing Then
                Me.EnableDisableParentControls(False)
            Else
                Me.EnableDisableParentControls(True)
            End If

            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            If Me.State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, moCodeText, True)
                ControlMgr.SetEnableControl(Me, moDescriptionText, True)
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, True)

                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If
        End Sub

        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            Me.State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
            Me.State.IsEditMode = False
        End Sub

        Protected Sub CreateNewWithCopy()

            Me.State.IsACopy = True
            Dim newObj As New Assurant.ElitaPlus.BusinessObjectsNew.Rule
            newObj.Copy(Me.State.MyBO)
            Me.State.MyBO = newObj

            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()

            Me.State.IsRuleEditing = False

            Me.State.ScreenSnapShotBO = New Assurant.ElitaPlus.BusinessObjectsNew.Rule
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
            Me.State.IsACopy = False
            Me.State.IsEditMode = False
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
                        'If Me.State.MyBO.IsDirty Then
                        '    Me.State.MyBO.Save()
                        '    Me.State.HasDataChanged = True
                        '    Me.PopulateFormFromBOs()
                        '    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        'Else
                        '    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        'End If

                        If Me.State.OverlapExists Then
                            If Me.State.MyBO.IsDirty Then
                                If Me.State.MyBO.ExpireOverLappingRules() Then
                                    'Me.State.MyChildBO.Save()
                                    'Me.State.MyChildProcessBO.Save()
                                    Me.State.MyBO.Save()
                                    Me.State.HasDataChanged = False
                                    Me.PopulateFormFromBOs()
                                    Me.EnableDisableFields()

                                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                                End If
                            Else
                                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                            End If
                            Me.State.OverlapExists = False
                        Else
                            Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        End If
                        Me.EnableDisableFields()
                        ''
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
                        Me.EnableDisableFields()
                End Select
            End If
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub

#End Region

#Region "Error Handling"


#End Region

#Region "User Control Event Handler"

#End Region

    End Class

End Namespace
