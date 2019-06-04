Namespace Tables

    Partial Public Class QuestionListDetailForm
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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As QuestionList, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Me.State.MyBO = New QuestionList(CType(Me.CallingParameters, Guid))
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
                Dim retObj As QuestionListDetailForm.ReturnType = CType(ReturnPar, QuestionListDetailForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.IssueQuestionListlId = retObj.EditingBo.Id
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Expire
                        Me.DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
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
                Me.ValidateDates()

                If Not Me.IsPostBack Then
                    Me.AddCalendarwithTime(Me.imgEffectiveDate, Me.moEffectiveDateText)
                    Me.AddCalendarwithTime(Me.imgExpirationDate, Me.moExpirationDateText)

                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New QuestionList
                    Else
                        Me.EnableHeaderControls(False)
                    End If

                    Me.ErrorCtrl.Clear_Hide()
                    If Not Me.IsPostBack Then
                        Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                        If Me.State.MyBO Is Nothing Then
                            Me.State.MyBO = New QuestionList
                        End If

                        Me.PopulateFormFromBOs()
                        Me.EnableDisableFields()
                        Me.PopulateChildern()
                    End If
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.RestoreGuiState()
                Else
                    Me.SaveGuiState()
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
                '#1 - Restrict to save backdated list in edit mode
                If Not Me.State.EffectiveDate Is Nothing And Me.State.MyBO.IsNew = False Then
                    If DateHelper.GetDateValue(Me.State.MyBO.Effective.ToString) <> DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) Then
                        If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) < IssueQuestionList.GetCurrentDateTime() Then
                            Me.State.IsInvalidEffective = True
                        Else
                            Me.State.IsInvalidEffective = False
                        End If
                    End If
                End If

                '#2 - Restrict to save backdated list in edit mode
                If Not Me.State.ExpirationDate Is Nothing And Me.State.MyBO.IsNew = False Then
                    If DateHelper.GetDateValue(Me.State.MyBO.Expiration.ToString) <> DateHelper.GetDateValue(moExpirationDateText.Text.ToString) Then
                        If (Me.State.MyBO.CheckIfListIsAssignedToDealer(Me.State.MyBO.Code, Me.State.MyBO.Id)) Then
                            If DateHelper.GetDateValue(Me.State.ExpirationDate.ToString) < IssueQuestionList.GetCurrentDateTime() Then
                                Me.State.IsInvalidExpiration = True
                            Else
                                Me.State.IsInvalidExpiration = False
                            End If
                        End If
                    End If
                End If
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
                '#1 - Restrict to save backdated list in edit mode
                If Not Me.State.EffectiveDate Is Nothing And Me.State.MyBO.IsNew = False Then
                    If DateHelper.GetDateValue(Me.State.MyBO.Effective.ToString) <> DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) Then
                        If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) < IssueQuestionList.GetCurrentDateTime() Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                        End If
                    End If
                End If

                '#2 - Restrict to save backdated list in edit mode
                If Not Me.State.ExpirationDate Is Nothing And Me.State.MyBO.IsNew = False Then
                    If DateHelper.GetDateValue(Me.State.MyBO.Expiration.ToString) <> DateHelper.GetDateValue(moExpirationDateText.Text.ToString) Then
                        If (Me.State.MyBO.CheckIfListIsAssignedToDealer(Me.State.MyBO.Code, Me.State.MyBO.Id)) Then
                            If DateHelper.GetDateValue(Me.State.ExpirationDate.ToString) < IssueQuestionList.GetCurrentDateTime() Then
                                Throw New GUIException(Message.MSG_GUI_INVALID_EXPIRATION_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE_LESSTHAN_SYSDATE)
                            End If
                        End If
                    End If
                End If

                Me.PopulateBOsFormFrom()

                '#3 - Effective date should be greater than Expiration Date
                If Not Me.State.EffectiveDate Is Nothing And Not Me.State.ExpirationDate Is Nothing Then
                    If DateHelper.GetDateValue(Me.State.EffectiveDate.ToString) > DateHelper.GetDateValue(Me.State.ExpirationDate.ToString) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE)
                    End If
                End If

                '#4 - For new records, check for no backdated LIst code and no duplicate List code - Effective Date Combination
                If Not Me.State.IsEditMode Then
                    If Not Me.State.EffectiveDate Is Nothing And Not Me.State.ExpirationDate Is Nothing Then
                        If DateHelper.GetDateValue(Me.State.EffectiveDate.ToString) < IssueQuestionList.GetCurrentDateTime().Today Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                        End If
                    End If

                    If (IsListCodeDuplicate(Me.State.Code, Me.State.EffectiveDate.ToString, Me.State.MyBO.Id)) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_LIST_CODE_WITH_SAME_EFFECIVE_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_DUPLICATE_CODE_EFFECTIVE)
                    End If
                End If

                '#5 - Expiration date lies between Effective and Expiration of earlier list code and there is no future list code available
                If (IsListCodeOverlapped(Me.State.Code, Me.State.EffectiveDate, Me.State.ExpirationDate, Me.State.MyBO.Id)) Then
                    Me.DisplayMessage(Message.MSG_EXPIRE_PREVIOUS_LIST, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                    Return
                End If

                '#6 - Expiration/Expiration date lies between any other Effective and Expiration range of earlier list code
                If (IsListCodeDurationOverlapped(Me.State.Code, Me.State.EffectiveDate, Me.State.ExpirationDate, Me.State.MyBO.Id)) Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                End If

                If Me.State.MyBO.IsFamilyDirty Then
                    Me.State.MyBO.Save()
                    Me.UpdateTranslation()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormFromBOs()
                    'took out not for issue 2347 fix
                    If Me.State.IsChildCreated Then
                        Me.State.IsQuestionListEditing = True
                        Me.EnableDisableUserControlButtons(PanelQuestionsEditDetail, True)
                    End If
                    'took out not for issue 2347 fix
                    If Me.State.IsDealerCreated Then
                        Me.State.IsDealerListEditing = True
                        Me.EnableDisableUserControlButtons(PanelDealerEditDetail, True)
                    End If
                    Me.EnableDisableFields()
                    Me.EnableHeaderControls(False)
                    Me.State.IsChildCreated = False
                    ' added the following line for issue 2347 fix
                    Me.State.IsDealerCreated = False
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
                    Me.State.MyBO = New QuestionList(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New QuestionList
                End If
                Me.State.IsQuestionListEditing = False
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.EnableDisableUserControlButtons(PanelQuestionsEditDetail, False)
                Me.EnableDisableUserControlButtons(PanelDealerEditDetail, False)
                Me.EnableUserControl(True)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If (Me.State.MyBO.CheckIfListIsAssignedToDealer(Me.State.MyBO.Code, Me.State.MyBO.Id)) Then
                    Throw New GUIException(Message.MSG_GUI_LIST_CODE_ASSIGNED_TO_DEALER_NO_DELETE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_CODE_ERR)
                Else
                    Me.PopulateChildern()
                    Me.State.MyBO.Delete()
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    If DateHelper.GetDateValue(Me.moEffectiveDateText.Text.ToString) > IssueQuestionList.GetCurrentDateTime() Then
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
                    Else
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
            UserControlQuestionsAvailable.ShowCancelButton = True
            UserControlQuestionsAvailable.ShowUpButton = True
            UserControlQuestionsAvailable.ShowDownButton = True
            UserControlDealerAvailable.ShowCancelButton = True
            Dim qstn As New QuestionList
            UserControlQuestionsAvailable.dvSelectedQuestions = qstn.GetSelectedQuestionList(Me.State.MyBO.Id)
            If Not String.IsNullOrEmpty(Me.State.MyBO.Code) Then
                UserControlDealerAvailable.dvSelectedDealer = qstn.GetSelectedDealertList(Me.State.MyBO.Code)
            Else
                'DEF-2933 : START : if the Question list code is empty, Clear the existing Selected Dealer list
                CType(Me.UserControlDealerAvailable.FindControl("UserControlAvailableSelectedDealerCodes").FindControl(SELECTEDLISTBOX), ListBox).Items.Clear()
                'DEF-2933 : END
            End If
        End Sub

        Public Sub ValidateDates()
           
            If Not moEffectiveDateText.Text Is String.Empty Then
                If (DateHelper.IsDate(moEffectiveDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(Me.moEffectiveDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End If
            End If
           
            If Not moExpirationDateText.Text Is String.Empty Then
                If (DateHelper.IsDate(moExpirationDateText.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(Me.moExpirationDateLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End If
            End If

        End Sub

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
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub PopulateChildern()
            Dim DealerListChildren As DealerList = Me.State.MyBO.BestDealerListChildren
            Dim QuestionListChildren As IssueQuestionListDetail = Me.State.MyBO.BestQuestionListChildren
        End Sub

        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(Me.moCodeText, .Code)
                Me.PopulateControlFromBOProperty(Me.moDescriptionText, .Description)
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
            End With

        End Sub

        Sub ResetChildControls()
            CType(Me.UserControlQuestionsAvailable.FindControl("UserControlAvailableSelectedQuestionsCodes").FindControl(SELECTEDLISTBOX), ListBox).Items.Clear()
            CType(Me.UserControlDealerAvailable.FindControl("UserControlAvailableSelectedDealerCodes").FindControl(SELECTEDLISTBOX), ListBox).Items.Clear()
        End Sub

        Sub EnableHeaderControls(ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, moCodeText, enableToggle)
            If (Me.State.MyBO.CheckIfListIsAssignedToDealer(Me.State.MyBO.Code, Me.State.MyBO.Id)) Then
                ControlMgr.SetEnableControl(Me, moDescriptionText, enableToggle)
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, enableToggle)
            Else
                ControlMgr.SetEnableControl(Me, moDescriptionText, Not (enableToggle))
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, Not (enableToggle))
            End If
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
            If Me.State.IsQuestionListEditing Then
                Me.EnableHeaderControls(False)
                Me.EnableDisableParentControls(False)
                Me.EnableUserControl(True)
            Else
                Me.EnableDisableParentControls(True)
                Me.EnableUserControl(False)
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
                Me.EnableDisableUserControlButtons(PanelQuestionsEditDetail, False)
                Me.EnableDisableUserControlButtons(PanelDealerEditDetail, False)
            End If
        End Sub

        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            Me.State.MyBO = New QuestionList
            Me.State.IsQuestionListEditing = False
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
            Me.State.IsChildCreated = False
            Me.State.IsEditMode = False
            Me.ResetChildControls()
        End Sub

        Protected Sub CreateNewWithCopy()
            Me.State.IsACopy = True
            Dim newObj As New QuestionList
            newObj.Copy(Me.State.MyBO)

            Me.State.MyBO = newObj
            Me.State.MyBO.Effective = IssueQuestionList.GetCurrentDateTime()
            Me.State.MyBO.Expiration = New DateTime(2499, 12, 31, 23, 59, 59)
            Me.State.MyBO.Code = Nothing
            Me.State.MyBO.Description = Nothing
            Me.PopulateFormFromBOs()

            Me.State.IsQuestionListEditing = False
            Me.EnableDisableFields()

            Me.State.ScreenSnapShotBO = New QuestionList
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
            Me.State.IsACopy = False
            Me.State.IsChildCreated = False
            Me.State.IsEditMode = False
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                    Me.BindBoPropertiesToLabels()

                    '#1 - Restrict to save backdated list in edit mode
                    If Me.State.IsInvalidEffective Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                    End If

                    '#2 - Restrict to save backdated list in edit mode
                    If Me.State.IsInvalidExpiration Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EXPIRATION_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE_LESSTHAN_SYSDATE)
                    End If

                    '#3 - Effective date should be greater than Expiration Date
                    If Not Me.State.EffectiveDate Is Nothing And Not Me.State.ExpirationDate Is Nothing Then
                        If DateHelper.GetDateValue(Me.State.EffectiveDate.ToString) > DateHelper.GetDateValue(Me.State.ExpirationDate.ToString) Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE)
                        End If
                    End If

                    '#4 - For new records, check for no backdated LIst code and no duplicate List code - Effective Date Combination
                    If Not Me.State.IsEditMode Then
                        If Not Me.State.EffectiveDate Is Nothing And Not Me.State.ExpirationDate Is Nothing Then
                            If DateHelper.GetDateValue(Me.State.EffectiveDate.ToString) < IssueQuestionList.GetCurrentDateTime().Today Then
                                Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                            End If
                        End If

                        If (IsListCodeDuplicate(Me.State.Code, Me.State.EffectiveDate.ToString, Me.State.MyBO.Id)) Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_LIST_CODE_WITH_SAME_EFFECIVE_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_DUPLICATE_CODE_EFFECTIVE)
                        End If
                    End If

                    '#5 - Expiration date lies between Effective and Expiration of earlier list code and there is no future list code available
                    If (IsListCodeOverlapped(Me.State.Code, Me.State.EffectiveDate, Me.State.ExpirationDate, Me.State.MyBO.Id)) Then
                        Me.DisplayMessage(Message.MSG_EXPIRE_PREVIOUS_LIST, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                        Return
                    End If

                    '#6 - Expiration/Expiration date lies between any other Effective and Expiration range of earlier list code
                    If (IsListCodeDurationOverlapped(Me.State.Code, Me.State.EffectiveDate, Me.State.ExpirationDate, Me.State.MyBO.Id)) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_LIST_CODE_OVERLAPPED)
                    End If

                    Me.State.MyBO.Save()
                    Me.UpdateTranslation()
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
                            Me.State.MyBO.Save()
                            QuestionList.ExpirePreviousList(Me.State.Code, Me.State.EffectiveDate, Me.State.ExpirationDate, Me.State.MyBO.Id)
                            Me.UpdateTranslation()
                            Me.State.HasDataChanged = True
                            Me.PopulateFormFromBOs()
                            If Not Me.State.IsChildCreated Then
                                Me.State.IsQuestionListEditing = True
                                Me.EnableDisableFields()
                                Me.EnableDisableUserControlButtons(PanelQuestionsEditDetail, True)
                                Me.EnableDisableUserControlButtons(PanelDealerEditDetail, True)
                                Me.EnableHeaderControls(False)
                                Me.State.IsChildCreated = False
                            End If
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
                        Me.EnableDisableFields()
                End Select
            End If
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        Sub UpdateTranslation()
            Dim dropdownBO As New DropdownItem
            Dim retVal As Integer
            Dim DataChanged As Boolean = False
            Dim DropdownItemId As Guid
            Dim DropdownId As Guid

            DropdownId = QuestionList.GetDropdownId(QLIST)
            DropdownItemId = QuestionList.GetDropdownItemId(DropdownId, Me.moCodeText.Text.Trim())

            If Not DropdownId = Guid.Empty Then
                retVal = dropdownBO.AddDropdownItem(Me.moCodeText.Text.Trim().ToUpper(), Codes.YESNO_Y, Codes.YESNO_Y, DropdownId, Me.moDescriptionText.Text.Trim(), ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                If retVal <> 0 Then
                    Me.ErrorCtrl.AddError(Message.ERR_SAVING_DATA)
                End If
            End If

            If Not DropdownItemId = Guid.Empty Then
                retVal = dropdownBO.UpdateDropdownItem(DropdownItemId, Me.moCodeText.Text.Trim().ToUpper(), Codes.YESNO_Y, Codes.YESNO_Y, Me.moDescriptionText.Text.Trim(), ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                If retVal <> 0 Then
                    Me.ErrorCtrl.AddError(Message.ERR_SAVING_DATA)
                End If
            End If
        End Sub

#End Region

#Region "Child_Control"

        Sub BeginChildEdit(ByVal IssueQuestionId As Guid, ByVal expireNow As Boolean, ByVal DisplayOrder As Integer)
            Me.State.IsQuestionListEditing = True
            Me.State.SelectedChildId = Guid.Empty
            Me.State.SelectedChildId = New Guid(Me.State.MyChildBO.IsChild(Me.State.MyBO.Id, IssueQuestionId))
            With Me.State
                If Not .SelectedChildId = Guid.Empty Then
                    .MyChildBO = .MyBO.GetChild(.SelectedChildId)
                Else
                    .MyChildBO = .MyBO.GetNewChild
                End If
                .MyChildBO.BeginEdit()
                .MyChildBO.DisplayOrder = DisplayOrder
            End With

            If expireNow Then
                Me.SetExpiration(IssueQuestionId)
            Else
                Me.PopulateChildBOFromDetail(IssueQuestionId)
            End If
        End Sub


        Sub BeginChildDealerEdit(ByVal DealerId As Guid, ByVal noMoreAssignedDealer As Boolean)
            Me.State.IsDealerListEditing = True
            Dim NullDealer As String = String.Empty
            With Me.State
                .MyChildDealerBO = .MyBO.GetDealerChild(DealerId)
                .MyChildDealerBO.BeginEdit()
            End With
            If noMoreAssignedDealer Then
                Me.RemoveDealerList(DealerId)
            Else
                Me.State.MyChildDealerBO.QuestionListCode = Me.State.MyBO.Code
            End If
        End Sub

        Sub RemoveDealerList(ByVal DealerId As Guid)
            With Me.State.MyChildDealerBO

                Me.State.MyChildDealerBO.QuestionListCode = String.Empty
            End With
            If Me.ErrCollection.Count > 0 Then
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

        Sub PopulateChildBOFromDetail(ByVal IssueQuestionId As Guid)
            Dim NewIssueQuestionExpiration As DateTime
            Dim SelectedIssueQuestionExpiration As DateTime
            Dim IssueQuestionOldExpiraitonDate As DateTime

            With Me.State.MyChildBO
                SelectedIssueQuestionExpiration = IssueQuestionList.GetQuestionExpiration(IssueQuestionId)
                IssueQuestionOldExpiraitonDate = CDate("#" & .Expiration.ToString & "#")

                If Not Me.State.MyBO.Expiration Is Nothing Then
                    NewIssueQuestionExpiration = CDate("#" & Me.State.MyBO.Expiration.ToString & "#")
                End If
                ''#1 
                .QuestionListId = Me.State.MyBO.Id
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
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub SetExpiration(ByVal IssueQuestionId As Guid)
            With Me.State.MyChildBO
                Me.State.MyChildBO.IssueQuestionId = IssueQuestionId
                Me.State.MyChildBO.Expiration = IssueQuestionList.GetCurrentDateTime().AddSeconds(-1)
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub EndChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
            Try
                With Me.State
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
                Me.State.IsQuestionListEditing = False
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Sub EndDealerChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
            Try
                With Me.State
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
                Me.State.IsDealerListEditing = False
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub
#End Region

#Region "Error Handling"


#End Region

#Region "User Control Event Handler"

#Region "Question"

        Protected Sub ExecuteSearchFilter(ByVal sender As Object, ByVal args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.ExecuteSearchFilter
            Dim question As New IssueQuestion
            Try
                args.dvAvailableQuestions = question.ExecuteQuestionsListFilter(args.Issue, args.QuestionList, args.SearchTags)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub SaveClicked(ByVal sender As Object, ByVal args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.EventSaveQuestionsListDetail
            Dim oQuestionList As ArrayList
            Dim oDisplayOrder As Integer = 1
            Dim dictQuestions As Hashtable

            Try
                oQuestionList = New ArrayList()
                Me.PopulateBOsFormFrom()
                dictQuestions = New Hashtable()
                For Each argQuestion As String In args.listSelectedQuestions
                    dictQuestions.Add(argQuestion, oDisplayOrder)
                    oDisplayOrder += 1
                Next

                oQuestionList = IssueQuestionList.GetQuestionInList(Me.State.MyBO.Id)
                For Each argQuestion As String In args.listSelectedQuestions
                    For Each questionRaw As Byte() In oQuestionList
                        If New Guid(questionRaw).ToString = argQuestion Then
                            oQuestionList.Remove(questionRaw)
                            Exit For
                        End If
                    Next
                    Me.BeginChildEdit(New Guid(argQuestion), False, CInt(dictQuestions.Item(argQuestion)))
                    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                For Each Question As Byte() In oQuestionList
                    Me.BeginChildEdit(New Guid(Question), True, CInt(dictQuestions.Item(Question)))
                    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                Me.State.HasDataChanged = True
                Me.State.IsQuestionListEditing = False
                Me.State.IsChildCreated = True
                Me.EnableDisableFields()
                Me.EnableDisableParentControls(True)
                Me.EnableDisableUserControlButtons(PanelQuestionsEditDetail, False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub CancelButtonClicked(ByVal sender As Object, ByVal args As SearchAvailableQuestionsEventArgs) Handles UserControlQuestionsAvailable.EventCancelButtonClicked
            Try
                Dim equip As New QuestionList
                UserControlQuestionsAvailable.dvSelectedQuestions = equip.GetSelectedQuestionList(Me.State.MyBO.Id)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Dealer"

        Protected Sub ExecuteDealerSearchFilter(ByVal sender As Object, ByVal args As SearchAvailableDealerEventArgs) Handles UserControlDealerAvailable.ExecuteDealerSearchFilter
            Dim question As New IssueQuestion
            Try
                args.dvAvailableDealer = question.ExecuteDealerListFilter(Me.State.MyBO.Code)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub SaveClickedDealer(ByVal sender As Object, ByVal args As SearchAvailableDealerEventArgs) Handles UserControlDealerAvailable.EventSaveDealerListDetail
            Dim oAvailableDealer As ArrayList

            Try
                Dim AddDealerList As ArrayList = New ArrayList()
                Dim RemoveDealerList As ArrayList = New ArrayList()
                oAvailableDealer = New ArrayList()
                Me.PopulateBOsFormFrom()
                AddDealerList = DirectCast(args.listSelectedDealer.Clone(), ArrayList)
                oAvailableDealer = IssueQuestionList.GetDealerInList(Me.State.MyBO.Code)

                For Each dealerRaw As String In oAvailableDealer
                    If AddDealerList.Contains(dealerRaw) Then
                        AddDealerList.Remove(dealerRaw)
                    End If
                Next
                RemoveDealerList = IssueQuestionList.GetDealerInList(Me.State.MyBO.Code)
                For Each argDealer As String In args.listSelectedDealer
                    If RemoveDealerList.Contains(argDealer) Then
                        RemoveDealerList.Remove(argDealer)
                    End If
                Next

                For Each oRemoveDealer As String In RemoveDealerList
                    Me.BeginChildDealerEdit(New Guid(oRemoveDealer), True)
                    Me.EndDealerChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                For Each oAddDealer As String In AddDealerList
                    Me.BeginChildDealerEdit(New Guid(oAddDealer), False)
                    Me.EndDealerChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next

                'added the following lines for issue 2347 fix
                Me.State.HasDataChanged = True
                Me.State.IsDealerListEditing = False
                Me.State.IsDealerCreated = True
                Me.EnableDisableFields()
                Me.EnableDisableParentControls(True)
                ' issue 2347 ends

                Me.EnableDisableUserControlButtons(PanelDealerEditDetail, False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub CancelButtonClicked(ByVal sender As Object, ByVal args As SearchAvailableDealerEventArgs) Handles UserControlDealerAvailable.EventCancelButtonClicked
            Try
                Dim equip As New IssueQuestion
                UserControlDealerAvailable.dvSelectedDealer = equip.GetSelectedDealerList(Me.State.Code)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

    End Class

End Namespace
