Namespace Tables

    Partial Public Class QuestionListDetailForm
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
        Public Const URL As String = "QuestionListDetailForm.aspx"
        Private Const GRID_COL_EDIT As Integer = 0
        Private Const GRID_COL_QUESTION_LIST_ID_IDX As Integer = 1
        Private Const GRID_COL_CODE_IDX As Integer = 2
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 3
        Private Const GRID_COL_EFFECTIVE_IDX As Integer = 4
        Private Const GRID_COL_EXPIRATION_IDX As Integer = 5
        Private Const QLIST As String = "QLIST"
        Private Const USERCONTROLAVAILABLESELECTED As String = "UserControlAvailableSelectedEquipmentCodes"
        Private Const SELECTEDLISTBOX As String = "moSelectedList"
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As QuestionList
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As QuestionList, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public IssueQuestionListlId As Guid = Guid.Empty
            Public MyBO As QuestionList
            Public ScreenSnapShotBO As QuestionList

            Public MyChildBO As IssueQuestionList
            Public ScreenSnapShotChildBO As IssueQuestionList

            Public MyChildDealerBO As Dealer
            Public ScreenSnapShotChildDealerBO As Dealer

            Public IsACopy As Boolean

            Public Code As String = String.Empty
            Public Description As String = String.Empty
            Public EffectiveDate As DateType = Nothing
            Public ExpirationDate As DateType = Nothing
            Public IsQuestionListEditing As Boolean = False
            Public IsDealerListEditing As Boolean = False
            Public IsChildCreated As Boolean = False
            Public IsDealerCreated As Boolean = False
            Public IsEditMode As Boolean = False
            Public IsInvalidEffective As Boolean = False
            Public IsInvalidExpiration As Boolean = False
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
                    State.MyBO = New QuestionList(CType(CallingParameters, Guid))
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
                Dim retObj As QuestionListDetailForm.ReturnType = CType(ReturnPar, QuestionListDetailForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.IssueQuestionListlId = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Expire
                        DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
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
                ValidateDates()

                If Not IsPostBack Then
                    AddCalendarwithTime(imgEffectiveDate, moEffectiveDateText)
                    AddCalendarwithTime(imgExpirationDate, moExpirationDateText)

                    If State.MyBO Is Nothing Then
                        State.MyBO = New QuestionList
                    Else
                        EnableHeaderControls(False)
                    End If

                    ErrorCtrl.Clear_Hide()
                    If Not IsPostBack Then
                        AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                        If State.MyBO Is Nothing Then
                            State.MyBO = New QuestionList
                        End If

                        PopulateFormFromBOs()
                        EnableDisableFields()
                        PopulateChildern()
                    End If
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    RestoreGuiState()
                Else
                    SaveGuiState()
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
                '#1 - Restrict to save backdated list in edit mode
                If State.EffectiveDate IsNot Nothing And State.MyBO.IsNew = False Then
                    If DateHelper.GetDateValue(State.MyBO.Effective.ToString) <> DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) Then
                        If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) < IssueQuestionList.GetCurrentDateTime() Then
                            State.IsInvalidEffective = True
                        Else
                            State.IsInvalidEffective = False
                        End If
                    End If
                End If

                '#2 - Restrict to save backdated list in edit mode
                If State.ExpirationDate IsNot Nothing And State.MyBO.IsNew = False Then
                    If DateHelper.GetDateValue(State.MyBO.Expiration.ToString) <> DateHelper.GetDateValue(moExpirationDateText.Text.ToString) Then
                        If (State.MyBO.CheckIfListIsAssignedToDealer(State.MyBO.Code, State.MyBO.Id)) Then
                            If DateHelper.GetDateValue(State.ExpirationDate.ToString) < IssueQuestionList.GetCurrentDateTime() Then
                                State.IsInvalidExpiration = True
                            Else
                                State.IsInvalidExpiration = False
                            End If
                        End If
                    End If
                End If
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
                '#1 - Restrict to save backdated list in edit mode
                If State.EffectiveDate IsNot Nothing And State.MyBO.IsNew = False Then
                    If DateHelper.GetDateValue(State.MyBO.Effective.ToString) <> DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) Then
                        If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) < IssueQuestionList.GetCurrentDateTime() Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                        End If
                    End If
                End If

                '#2 - Restrict to save backdated list in edit mode
                If State.ExpirationDate IsNot Nothing And State.MyBO.IsNew = False Then
                    If DateHelper.GetDateValue(State.MyBO.Expiration.ToString) <> DateHelper.GetDateValue(moExpirationDateText.Text.ToString) Then
                        If (State.MyBO.CheckIfListIsAssignedToDealer(State.MyBO.Code, State.MyBO.Id)) Then
                            If DateHelper.GetDateValue(State.ExpirationDate.ToString) < IssueQuestionList.GetCurrentDateTime() Then
                                Throw New GUIException(Message.MSG_GUI_INVALID_EXPIRATION_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE_LESSTHAN_SYSDATE)
                            End If
                        End If
                    End If
                End If

                PopulateBOsFormFrom()

                '#3 - Effective date should be greater than Expiration Date
                If State.EffectiveDate IsNot Nothing And State.ExpirationDate IsNot Nothing Then
                    If DateHelper.GetDateValue(State.EffectiveDate.ToString) > DateHelper.GetDateValue(State.ExpirationDate.ToString) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE)
                    End If
                End If

                '#4 - For new records, check for no backdated LIst code and no duplicate List code - Effective Date Combination
                If Not State.IsEditMode Then
                    If State.EffectiveDate IsNot Nothing And State.ExpirationDate IsNot Nothing Then
                        If DateHelper.GetDateValue(State.EffectiveDate.ToString) < IssueQuestionList.GetCurrentDateTime().Today Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                        End If
                    End If

                    If (IsListCodeDuplicate(State.Code, State.EffectiveDate.ToString, State.MyBO.Id)) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_LIST_CODE_WITH_SAME_EFFECIVE_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_DUPLICATE_CODE_EFFECTIVE)
                    End If
                End If

                '#5 - Expiration date lies between Effective and Expiration of earlier list code and there is no future list code available
                If (IsListCodeOverlapped(State.Code, State.EffectiveDate, State.ExpirationDate, State.MyBO.Id)) Then
                    DisplayMessage(Message.MSG_EXPIRE_PREVIOUS_LIST, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                    Return
                End If

                '#6 - Expiration/Expiration date lies between any other Effective and Expiration range of earlier list code
                If (IsListCodeDurationOverlapped(State.Code, State.EffectiveDate, State.ExpirationDate, State.MyBO.Id)) Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                End If

                If State.MyBO.IsFamilyDirty Then
                    State.MyBO.Save()
                    UpdateTranslation()
                    State.HasDataChanged = True
                    PopulateFormFromBOs()
                    'took out not for issue 2347 fix
                    If State.IsChildCreated Then
                        State.IsQuestionListEditing = True
                        EnableDisableUserControlButtons(PanelQuestionsEditDetail, True)
                    End If
                    'took out not for issue 2347 fix
                    If State.IsDealerCreated Then
                        State.IsDealerListEditing = True
                        EnableDisableUserControlButtons(PanelDealerEditDetail, True)
                    End If
                    EnableDisableFields()
                    EnableHeaderControls(False)
                    State.IsChildCreated = False
                    ' added the following line for issue 2347 fix
                    State.IsDealerCreated = False
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
                    State.MyBO = New QuestionList(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New QuestionList
                End If
                State.IsQuestionListEditing = False
                PopulateFormFromBOs()
                EnableDisableFields()
                EnableDisableUserControlButtons(PanelQuestionsEditDetail, False)
                EnableDisableUserControlButtons(PanelDealerEditDetail, False)
                EnableUserControl(True)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If (State.MyBO.CheckIfListIsAssignedToDealer(State.MyBO.Code, State.MyBO.Id)) Then
                    Throw New GUIException(Message.MSG_GUI_LIST_CODE_ASSIGNED_TO_DEALER_NO_DELETE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_CODE_ERR)
                Else
                    PopulateChildern()
                    State.MyBO.Delete()
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) > IssueQuestionList.GetCurrentDateTime() Then
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
                    Else
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
            UserControlQuestionsAvailable.ShowCancelButton = True
            UserControlQuestionsAvailable.ShowUpButton = True
            UserControlQuestionsAvailable.ShowDownButton = True
            UserControlDealerAvailable.ShowCancelButton = True
            Dim qstn As New QuestionList
            UserControlQuestionsAvailable.dvSelectedQuestions = qstn.GetSelectedQuestionList(State.MyBO.Id)
            If Not String.IsNullOrEmpty(State.MyBO.Code) Then
                UserControlDealerAvailable.dvSelectedDealer = qstn.GetSelectedDealertList(State.MyBO.Code)
            Else
                'DEF-2933 : START : if the Question list code is empty, Clear the existing Selected Dealer list
                CType(UserControlDealerAvailable.FindControl("UserControlAvailableSelectedDealerCodes").FindControl(SELECTEDLISTBOX), ListBox).Items.Clear()
                'DEF-2933 : END
            End If
        End Sub

        Public Sub ValidateDates()
           
            If moEffectiveDateText.Text IsNot String.Empty Then
                If (DateHelper.IsDate(moEffectiveDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(moEffectiveDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End If
            End If
           
            If moExpirationDateText.Text IsNot String.Empty Then
                If (DateHelper.IsDate(moExpirationDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(moExpirationDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End If
            End If

        End Sub

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
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub PopulateChildern()
            Dim DealerListChildren As DealerList = State.MyBO.BestDealerListChildren
            Dim QuestionListChildren As IssueQuestionListDetail = State.MyBO.BestQuestionListChildren
        End Sub

        Protected Sub PopulateFormFromBOs()
            With State.MyBO
                PopulateControlFromBOProperty(moCodeText, .Code)
                PopulateControlFromBOProperty(moDescriptionText, .Description)
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
            End With

        End Sub

        Sub ResetChildControls()
            CType(UserControlQuestionsAvailable.FindControl("UserControlAvailableSelectedQuestionsCodes").FindControl(SELECTEDLISTBOX), ListBox).Items.Clear()
            CType(UserControlDealerAvailable.FindControl("UserControlAvailableSelectedDealerCodes").FindControl(SELECTEDLISTBOX), ListBox).Items.Clear()
        End Sub

        Sub EnableHeaderControls(enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, moCodeText, enableToggle)
            If (State.MyBO.CheckIfListIsAssignedToDealer(State.MyBO.Code, State.MyBO.Id)) Then
                ControlMgr.SetEnableControl(Me, moDescriptionText, enableToggle)
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, enableToggle)
            Else
                ControlMgr.SetEnableControl(Me, moDescriptionText, Not (enableToggle))
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, Not (enableToggle))
            End If
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
            If State.IsQuestionListEditing Then
                EnableHeaderControls(False)
                EnableDisableParentControls(False)
                EnableUserControl(True)
            Else
                EnableDisableParentControls(True)
                EnableUserControl(False)
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
                EnableDisableUserControlButtons(PanelQuestionsEditDetail, False)
                EnableDisableUserControlButtons(PanelDealerEditDetail, False)
            End If
        End Sub

        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            State.MyBO = New QuestionList
            State.IsQuestionListEditing = False
            PopulateFormFromBOs()
            EnableDisableFields()
            State.IsChildCreated = False
            State.IsEditMode = False
            ResetChildControls()
        End Sub

        Protected Sub CreateNewWithCopy()
            State.IsACopy = True
            Dim newObj As New QuestionList
            newObj.Copy(State.MyBO)

            State.MyBO = newObj
            State.MyBO.Effective = IssueQuestionList.GetCurrentDateTime()
            State.MyBO.Expiration = New DateTime(2499, 12, 31, 23, 59, 59)
            State.MyBO.Code = Nothing
            State.MyBO.Description = Nothing
            PopulateFormFromBOs()

            State.IsQuestionListEditing = False
            EnableDisableFields()

            State.ScreenSnapShotBO = New QuestionList
            State.ScreenSnapShotBO.Clone(State.MyBO)
            State.IsACopy = False
            State.IsChildCreated = False
            State.IsEditMode = False
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                    BindBoPropertiesToLabels()

                    '#1 - Restrict to save backdated list in edit mode
                    If State.IsInvalidEffective Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                    End If

                    '#2 - Restrict to save backdated list in edit mode
                    If State.IsInvalidExpiration Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EXPIRATION_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE_LESSTHAN_SYSDATE)
                    End If

                    '#3 - Effective date should be greater than Expiration Date
                    If State.EffectiveDate IsNot Nothing And State.ExpirationDate IsNot Nothing Then
                        If DateHelper.GetDateValue(State.EffectiveDate.ToString) > DateHelper.GetDateValue(State.ExpirationDate.ToString) Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE)
                        End If
                    End If

                    '#4 - For new records, check for no backdated LIst code and no duplicate List code - Effective Date Combination
                    If Not State.IsEditMode Then
                        If State.EffectiveDate IsNot Nothing And State.ExpirationDate IsNot Nothing Then
                            If DateHelper.GetDateValue(State.EffectiveDate.ToString) < IssueQuestionList.GetCurrentDateTime().Today Then
                                Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                            End If
                        End If

                        If (IsListCodeDuplicate(State.Code, State.EffectiveDate.ToString, State.MyBO.Id)) Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_LIST_CODE_WITH_SAME_EFFECIVE_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_DUPLICATE_CODE_EFFECTIVE)
                        End If
                    End If

                    '#5 - Expiration date lies between Effective and Expiration of earlier list code and there is no future list code available
                    If (IsListCodeOverlapped(State.Code, State.EffectiveDate, State.ExpirationDate, State.MyBO.Id)) Then
                        DisplayMessage(Message.MSG_EXPIRE_PREVIOUS_LIST, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                        Return
                    End If

                    '#6 - Expiration/Expiration date lies between any other Effective and Expiration range of earlier list code
                    If (IsListCodeDurationOverlapped(State.Code, State.EffectiveDate, State.ExpirationDate, State.MyBO.Id)) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                    End If

                    State.MyBO.Save()
                    UpdateTranslation()
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
                            State.MyBO.Save()
                            QuestionList.ExpirePreviousList(State.Code, State.EffectiveDate, State.ExpirationDate, State.MyBO.Id)
                            UpdateTranslation()
                            State.HasDataChanged = True
                            PopulateFormFromBOs()
                            If Not State.IsChildCreated Then
                                State.IsQuestionListEditing = True
                                EnableDisableFields()
                                EnableDisableUserControlButtons(PanelQuestionsEditDetail, True)
                                EnableDisableUserControlButtons(PanelDealerEditDetail, True)
                                EnableHeaderControls(False)
                                State.IsChildCreated = False
                            End If
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
                        EnableDisableFields()
                End Select
            End If
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        Sub UpdateTranslation()
            Dim dropdownBO As New DropdownItem
            Dim retVal As Integer
            Dim DataChanged As Boolean = False
            Dim DropdownItemId As Guid
            Dim DropdownId As Guid

            DropdownId = QuestionList.GetDropdownId(QLIST)
            DropdownItemId = QuestionList.GetDropdownItemId(DropdownId, moCodeText.Text.Trim())

            If Not DropdownId = Guid.Empty Then
                retVal = dropdownBO.AddDropdownItem(moCodeText.Text.Trim().ToUpper(), Codes.YESNO_Y, Codes.YESNO_Y, DropdownId, moDescriptionText.Text.Trim(), ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                If retVal <> 0 Then
                    ErrorCtrl.AddError(Message.ERR_SAVING_DATA)
                End If
            End If

            If Not DropdownItemId = Guid.Empty Then
                retVal = dropdownBO.UpdateDropdownItem(DropdownItemId, moCodeText.Text.Trim().ToUpper(), Codes.YESNO_Y, Codes.YESNO_Y, moDescriptionText.Text.Trim(), ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                If retVal <> 0 Then
                    ErrorCtrl.AddError(Message.ERR_SAVING_DATA)
                End If
            End If
        End Sub

#End Region

#Region "Child_Control"

        Sub BeginChildEdit(IssueQuestionId As Guid, expireNow As Boolean, DisplayOrder As Integer)
            State.IsQuestionListEditing = True
            State.SelectedChildId = Guid.Empty
            State.SelectedChildId = New Guid(State.MyChildBO.IsChild(State.MyBO.Id, IssueQuestionId))
            With State
                If Not .SelectedChildId = Guid.Empty Then
                    .MyChildBO = .MyBO.GetChild(.SelectedChildId)
                Else
                    .MyChildBO = .MyBO.GetNewChild
                End If
                .MyChildBO.BeginEdit()
                .MyChildBO.DisplayOrder = DisplayOrder
            End With

            If expireNow Then
                SetExpiration(IssueQuestionId)
            Else
                PopulateChildBOFromDetail(IssueQuestionId)
            End If
        End Sub


        Sub BeginChildDealerEdit(DealerId As Guid, noMoreAssignedDealer As Boolean)
            State.IsDealerListEditing = True
            Dim NullDealer As String = String.Empty
            With State
                .MyChildDealerBO = .MyBO.GetDealerChild(DealerId)
                .MyChildDealerBO.BeginEdit()
            End With
            If noMoreAssignedDealer Then
                RemoveDealerList(DealerId)
            Else
                State.MyChildDealerBO.QuestionListCode = State.MyBO.Code
            End If
        End Sub

        Sub RemoveDealerList(DealerId As Guid)
            With State.MyChildDealerBO

                State.MyChildDealerBO.QuestionListCode = String.Empty
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub


        ''' <summary>
        ''' Update Child IssueQuestion BO with exact Effective and Expiraiton
        ''' #1 : Update IssueQuestion Id
        ''' #2 : Set earliest available expiration date 
        ''' #3 : Set Effective date if Equipment added was earlier existing equipment
        ''' </summary>
        ''' <param name="equipmentId">Equipment GUID</param>
        ''' <remarks></remarks>

        Sub PopulateChildBOFromDetail(IssueQuestionId As Guid)
            Dim NewIssueQuestionExpiration As DateTime
            Dim SelectedIssueQuestionExpiration As DateTime
            Dim IssueQuestionOldExpiraitonDate As DateTime

            With State.MyChildBO
                SelectedIssueQuestionExpiration = IssueQuestionList.GetQuestionExpiration(IssueQuestionId)
                IssueQuestionOldExpiraitonDate = CDate("#" & .Expiration.ToString & "#")

                If State.MyBO.Expiration IsNot Nothing Then
                    NewIssueQuestionExpiration = CDate("#" & State.MyBO.Expiration.ToString & "#")
                End If
                ''#1 
                .QuestionListId = State.MyBO.Id
                .IssueQuestionId = IssueQuestionId
                ''#2
                If Not SelectedIssueQuestionExpiration = Nothing And SelectedIssueQuestionExpiration < NewIssueQuestionExpiration Then
                    .Expiration = SelectedIssueQuestionExpiration
                Else
                    .Expiration = NewIssueQuestionExpiration
                End If
                ''#3
                If IssueQuestionOldExpiraitonDate < IssueQuestionList.GetCurrentDateTime() Then
                    .Effective = IssueQuestionList.GetCurrentDateTime()
                End If
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub SetExpiration(IssueQuestionId As Guid)
            With State.MyChildBO
                State.MyChildBO.IssueQuestionId = IssueQuestionId
                State.MyChildBO.Expiration = IssueQuestionList.GetCurrentDateTime().AddSeconds(-1)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub EndChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
            Try
                With State
                    Select Case lastop
                        Case ElitaPlusPage.DetailPageCommand.OK
                            .MyChildBO.Save()
                            .MyChildBO.EndEdit()
                        Case ElitaPlusPage.DetailPageCommand.Cancel
                            .MyChildBO.cancelEdit()
                            If .MyChildBO.IsSaveNew Then
                                .MyChildBO.Delete()
                                .MyChildBO.Save()
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Back
                            .MyChildBO.cancelEdit()
                            If .MyChildBO.IsSaveNew Then
                                .MyChildBO.Delete()
                                .MyChildBO.Save()
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            .MyChildBO.Delete()
                            .MyChildBO.Save()
                            .MyChildBO.EndEdit()
                            .SelectedChildId = Guid.Empty
                    End Select
                End With
                State.IsQuestionListEditing = False
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Sub EndDealerChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
            Try
                With State
                    Select Case lastop
                        Case ElitaPlusPage.DetailPageCommand.OK
                            .MyChildDealerBO.Save()
                            .MyChildDealerBO.EndEdit()
                        Case ElitaPlusPage.DetailPageCommand.Cancel
                            .MyChildDealerBO.cancelEdit()
                            If .MyChildDealerBO.IsSaveNew Then
                                .MyChildDealerBO.Delete()
                                .MyChildDealerBO.Save()
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Back
                            .MyChildDealerBO.cancelEdit()
                            If .MyChildDealerBO.IsSaveNew Then
                                .MyChildDealerBO.Delete()
                                .MyChildDealerBO.Save()
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            .MyChildDealerBO.Delete()
                            .MyChildDealerBO.Save()
                            .MyChildDealerBO.EndEdit()
                            .SelectedChildId = Guid.Empty
                    End Select
                End With
                State.IsDealerListEditing = False
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
#End Region

#Region "Error Handling"


#End Region

#Region "User Control Event Handler"

#Region "Question"

        Protected Sub ExecuteSearchFilter(sender As Object, args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.ExecuteSearchFilter
            Dim question As New IssueQuestion
            Try
                args.dvAvailableQuestions = question.ExecuteQuestionsListFilter(args.Issue, args.QuestionList, args.SearchTags)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub SaveClicked(sender As Object, args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.EventSaveQuestionsListDetail
            Dim oQuestionList As ArrayList
            Dim oDisplayOrder As Integer = 1
            Dim dictQuestions As Hashtable

            Try
                oQuestionList = New ArrayList()
                PopulateBOsFormFrom()
                dictQuestions = New Hashtable()
                For Each argQuestion As String In args.listSelectedQuestions
                    dictQuestions.Add(argQuestion, oDisplayOrder)
                    oDisplayOrder += 1
                Next

                oQuestionList = IssueQuestionList.GetQuestionInList(State.MyBO.Id)
                For Each argQuestion As String In args.listSelectedQuestions
                    For Each questionRaw As Byte() In oQuestionList
                        If New Guid(questionRaw).ToString = argQuestion Then
                            oQuestionList.Remove(questionRaw)
                            Exit For
                        End If
                    Next
                    BeginChildEdit(New Guid(argQuestion), False, CInt(dictQuestions.Item(argQuestion)))
                    EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                For Each Question As Byte() In oQuestionList
                    BeginChildEdit(New Guid(Question), True, CInt(dictQuestions.Item(Question)))
                    EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                State.HasDataChanged = True
                State.IsQuestionListEditing = False
                State.IsChildCreated = True
                EnableDisableFields()
                EnableDisableParentControls(True)
                EnableDisableUserControlButtons(PanelQuestionsEditDetail, False)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub CancelButtonClicked(sender As Object, args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.EventCancelButtonClicked
            Try
                Dim equip As New QuestionList
                UserControlQuestionsAvailable.dvSelectedQuestions = equip.GetSelectedQuestionList(State.MyBO.Id)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Dealer"

        Protected Sub ExecuteDealerSearchFilter(sender As Object, args As SearchAvailableDealerEventArgs) Handles UserControlDealerAvailable.ExecuteDealerSearchFilter
            Dim question As New IssueQuestion
            Try
                args.dvAvailableDealer = question.ExecuteDealerListFilter(State.MyBO.Code)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub SaveClickedDealer(sender As Object, args As SearchAvailableDealerEventArgs) Handles UserControlDealerAvailable.EventSaveDealerListDetail
            Dim oAvailableDealer As ArrayList

            Try
                Dim AddDealerList As ArrayList = New ArrayList()
                Dim RemoveDealerList As ArrayList = New ArrayList()
                oAvailableDealer = New ArrayList()
                PopulateBOsFormFrom()
                AddDealerList = DirectCast(args.listSelectedDealer.Clone(), ArrayList)
                oAvailableDealer = IssueQuestionList.GetDealerInList(State.MyBO.Code)

                For Each dealerRaw As String In oAvailableDealer
                    If AddDealerList.Contains(dealerRaw) Then
                        AddDealerList.Remove(dealerRaw)
                    End If
                Next
                RemoveDealerList = IssueQuestionList.GetDealerInList(State.MyBO.Code)
                For Each argDealer As String In args.listSelectedDealer
                    If RemoveDealerList.Contains(argDealer) Then
                        RemoveDealerList.Remove(argDealer)
                    End If
                Next

                For Each oRemoveDealer As String In RemoveDealerList
                    BeginChildDealerEdit(New Guid(oRemoveDealer), True)
                    EndDealerChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                For Each oAddDealer As String In AddDealerList
                    BeginChildDealerEdit(New Guid(oAddDealer), False)
                    EndDealerChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next

                'added the following lines for issue 2347 fix
                State.HasDataChanged = True
                State.IsDealerListEditing = False
                State.IsDealerCreated = True
                EnableDisableFields()
                EnableDisableParentControls(True)
                ' issue 2347 ends

                EnableDisableUserControlButtons(PanelDealerEditDetail, False)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub CancelButtonClicked(sender As Object, args As SearchAvailableDealerEventArgs) Handles UserControlDealerAvailable.EventCancelButtonClicked
            Try
                Dim equip As New IssueQuestion
                UserControlDealerAvailable.dvSelectedDealer = equip.GetSelectedDealerList(State.Code)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

    End Class

End Namespace
