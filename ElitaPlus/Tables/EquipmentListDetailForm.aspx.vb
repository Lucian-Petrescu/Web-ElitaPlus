Namespace Tables

    Partial Public Class EquipmentListDetailForm
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
        Public Const URL As String = "EquipmentListDetailForm.aspx"
        Private Const GRID_COL_EDIT As Integer = 0
        Private Const GRID_COL_EQUIPMENT_LIST_ID_IDX As Integer = 1
        Private Const GRID_COL_CODE_IDX As Integer = 2
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 3
        Private Const GRID_COL_EFFECTIVE_IDX As Integer = 4
        Private Const GRID_COL_EXPIRATION_IDX As Integer = 5
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As EquipmentList
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As EquipmentList, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public EquipmentListDetailId As Guid = Guid.Empty
            Public MyBO As EquipmentList
            Public ScreenSnapShotBO As EquipmentList

            Public MyChildBO As EquipmentListDetail
            Public ScreenSnapShotChildBO As EquipmentListDetail

            Public IsACopy As Boolean

            Public Code As String = String.Empty
            Public Description As String = String.Empty
            Public Comments As String = String.Empty
            Public EffectiveDate As DateType = Nothing
            Public ExpirationDate As DateType = Nothing

            Public IsEquipmentListEditing As Boolean = False
            Public IsChildCreated As Boolean = False
            Public IsEditMode As Boolean = False
            Public SelectedChildId As Guid = Guid.Empty
            Public IsInvalidEffective As Boolean = False
            Public IsInvalidExpiration As Boolean = False

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
                    'Get the id from the parent
                    State.MyBO = New EquipmentList(CType(CallingParameters, Guid))
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
                Dim retObj As EquipmentListDetailForm.ReturnType = CType(ReturnPar, EquipmentListDetailForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.EquipmentListDetailId = retObj.EditingBo.Id
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
            State.Comments = moCommentText.Text
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
            moCommentText.Text = State.MyBO.Comments
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
                        State.MyBO = New EquipmentList
                    Else
                        EnableHeaderControls(False)
                    End If

                    ErrorCtrl.Clear_Hide()
                    If Not IsPostBack Then
                        AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                        If State.MyBO Is Nothing Then
                            State.MyBO = New EquipmentList
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
                If _
                    State.EffectiveDate IsNot Nothing AndAlso State.MyBO.IsNew = False AndAlso
                    DateHelper.GetDateValue(State.MyBO.Effective.ToString) <>
                    DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) Then
                    If _
                        DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) <
                        EquipmentListDetail.GetCurrentDateTime() Then
                        State.IsInvalidEffective = True
                    Else
                        State.IsInvalidEffective = False
                    End If
                End If

                '#2 - Restrict to save backdated list in edit mode
                If _
                    State.ExpirationDate IsNot Nothing AndAlso State.MyBO.IsNew = False AndAlso
                    (DateHelper.GetDateValue(State.MyBO.Expiration.ToString) <>
                     DateHelper.GetDateValue(moExpirationDateText.Text.ToString) AndAlso
                     State.MyBO.CheckIfListIsAssignedToDealer(State.MyBO.Code, State.MyBO.Id)) Then
                    If _
                        DateHelper.GetDateValue(State.ExpirationDate.ToString) <
                        EquipmentListDetail.GetCurrentDateTime() Then
                        State.IsInvalidExpiration = True
                    Else
                        State.IsInvalidExpiration = False
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
                If State.EffectiveDate IsNot Nothing AndAlso State.MyBO.IsNew = False Then
                    If DateHelper.GetDateValue(State.MyBO.Effective.ToString) <> DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) Then
                        If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) < EquipmentListDetail.GetCurrentDateTime() Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                        End If
                    End If
                End If

                '#2 - Restrict to save backdated list in edit mode
                If State.ExpirationDate IsNot Nothing AndAlso State.MyBO.IsNew = False Then
                    If DateHelper.GetDateValue(State.MyBO.Expiration.ToString) <> DateHelper.GetDateValue(moExpirationDateText.Text.ToString) Then
                        If (State.MyBO.CheckIfListIsAssignedToDealer(State.MyBO.Code, State.MyBO.Id)) Then
                            If DateHelper.GetDateValue(State.ExpirationDate.ToString) < EquipmentListDetail.GetCurrentDateTime() Then
                                Throw New GUIException(Message.MSG_GUI_INVALID_EXPIRATION_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE_LESSTHAN_SYSDATE)
                            End If
                        End If
                    End If
                End If

                PopulateBOsFormFrom()

                '#3 - Effective date should be greater than Expiration Date
                If State.EffectiveDate IsNot Nothing AndAlso State.ExpirationDate IsNot Nothing Then
                    If DateHelper.GetDateValue(State.EffectiveDate.ToString) > DateHelper.GetDateValue(State.ExpirationDate.ToString) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE)
                    End If
                End If

                '#4 - For new records, check for no backdated LIst code and no duplicate List code - Effective Date Combination
                If Not State.IsEditMode Then
                    If State.EffectiveDate IsNot Nothing AndAlso State.ExpirationDate IsNot Nothing Then
                        If DateHelper.GetDateValue(State.EffectiveDate.ToString) < EquipmentListDetail.GetCurrentDateTime().Today Then
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

                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    PopulateFormFromBOs()
                    If Not State.IsChildCreated Then
                        State.IsEquipmentListEditing = True
                        EnableDisableUserControlButtons(True)
                        EnableDisableFields()
                        EnableHeaderControls(False)
                        State.IsChildCreated = False
                    End If
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
                    State.MyBO = New EquipmentList(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New EquipmentList
                End If
                State.IsEquipmentListEditing = False
                PopulateFormFromBOs()
                EnableDisableFields()
                EnableDisableUserControlButtons(False)
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
                    If DateHelper.GetDateValue(State.MyBO.Effective.ToString) > EquipmentListDetail.GetCurrentDateTime() Then
                        EquipmentList.ExpireList(State.MyBO.Effective, State.MyBO.Id)
                    Else
                        State.MyBO.Delete()
                        State.MyBO.Save()
                        State.HasDataChanged = True
                    End If
                    If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) > EquipmentListDetail.GetCurrentDateTime() Then
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

        Private Sub ExpireListAndListItems()
            Try
                Dim oEquipmentList As ArrayList = New ArrayList()
                If DateHelper.GetDateValue(State.MyBO.Effective.ToString) > EquipmentListDetail.GetCurrentDateTime() Then
                    moExpirationDateText.Text = moEffectiveDateText.Text
                    PopulateBOsFormFrom()
                    oEquipmentList = EquipmentListDetail.GetEquipmentsInList(State.MyBO.Id)
                    For Each argEquipment As Byte() In oEquipmentList
                        BeginChildEdit(New Guid(argEquipment), True)
                        State.MyChildBO.Expiration = State.MyBO.Effective
                        EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                    Next
                End If
            Catch ex As Exception
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

            If (EquipmentList.CheckDuplicateEquipmentListCode(code, CDate(effective).ToString(ElitaPlusPage.DATE_TIME_FORMAT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")), id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeOverlapped(code As String, effective As DateType,
                                        expiration As DateType, id As Guid) As Boolean

            If (EquipmentList.CheckListCodeForOverlap(code, effective, expiration, id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeDurationOverlapped(code As String, effective As DateType,
                                        expiration As DateType, listId As Guid) As Boolean

            If (EquipmentList.CheckListCodeDurationOverlap(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function ExpirePreviousList(code As String, effective As DateType,
                                        expiration As DateType, listId As Guid) As Boolean

            If (EquipmentList.ExpirePreviousList(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Sub EnableUserControl(bVisible As Boolean)
            UserControlSearchAvailableEquipment.ShowCancelButton = True
            Dim equip As New EquipmentList
            UserControlSearchAvailableEquipment.dvSelectedEquipment = equip.GetSelectedEquipmentList(State.MyBO.Id)
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
            BindBOPropertyToLabel(State.MyBO, "Comments", moCommentLabel)
        End Sub

        Protected Sub PopulateBOsFormFrom()
            With State.MyBO
                PopulateBOProperty(State.MyBO, "Code", moCodeText)
                PopulateBOProperty(State.MyBO, "Description", moDescriptionText)
                PopulateBOProperty(State.MyBO, "Effective", moEffectiveDateText)
                PopulateBOProperty(State.MyBO, "Expiration", moExpirationDateText)
                PopulateBOProperty(State.MyBO, "Comments", moCommentText)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub PopulateChildern()
            Dim detail As EquipmentListDetailList = State.MyBO.BestEquipmentListChildren
        End Sub

        Protected Sub PopulateFormFromBOs()
            With State.MyBO
                PopulateControlFromBOProperty(moCodeText, .Code)
                PopulateControlFromBOProperty(moDescriptionText, .Description)
                PopulateControlFromBOProperty(moCommentText, .Comments)
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

        Sub EnableHeaderControls(enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, moCodeText, enableToggle)
            If (State.MyBO.CheckIfListIsAssignedToDealer(State.MyBO.Code, State.MyBO.Id)) Then
                ControlMgr.SetEnableControl(Me, moDescriptionText, enableToggle)
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, enableToggle)
                ControlMgr.SetEnableControl(Me, moCommentText, enableToggle)
            Else
                ControlMgr.SetEnableControl(Me, moDescriptionText, Not (enableToggle))
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, Not (enableToggle))
                ControlMgr.SetEnableControl(Me, moCommentText, Not (enableToggle))
            End If
        End Sub

        Sub EnableDisableParentControls(enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
        End Sub

        Sub EnableDisableUserControlButtons(enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, PanelCommentEditDetail, enableToggle)
        End Sub

        Protected Sub EnableDisableFields()
            If State.IsEquipmentListEditing Then
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
                ControlMgr.SetEnableControl(Me, moCommentText, True)

                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
                EnableDisableUserControlButtons(False)
            End If
        End Sub

        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            State.MyBO = New EquipmentList
            State.IsEquipmentListEditing = False
            PopulateFormFromBOs()
            EnableDisableFields()
            State.IsChildCreated = False
            State.IsEditMode = False
            CType(UserControlSearchAvailableEquipment.FindControl("UserControlAvailableSelectedEquipmentCodes").FindControl("moSelectedList"), ListBox).Items.Clear()
        End Sub

        Protected Sub CreateNewWithCopy()
            State.IsACopy = True
            Dim newObj As New EquipmentList
            newObj.Copy(State.MyBO)

            State.MyBO = newObj
            State.MyBO.Effective = EquipmentListDetail.GetCurrentDateTime()
            State.MyBO.Expiration = New DateTime(2499, 12, 31, 23, 59, 59)
            State.MyBO.Code = Nothing
            State.MyBO.Description = Nothing
            State.MyBO.Comments = Nothing
            PopulateFormFromBOs()

            State.IsEquipmentListEditing = False
            EnableDisableFields()

            State.ScreenSnapShotBO = New EquipmentList
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
                    If _
                        State.EffectiveDate IsNot Nothing AndAlso State.ExpirationDate IsNot Nothing AndAlso
                        DateHelper.GetDateValue(State.EffectiveDate.ToString) >
                        DateHelper.GetDateValue(State.ExpirationDate.ToString) Then
                        Throw _
                            New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE,
                                             Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EXPIRATION_DATE)
                    End If

                    '#4 - For new records, check for no backdated LIst code and no duplicate List code - Effective Date Combination
                    If Not State.IsEditMode Then
                        If State.EffectiveDate IsNot Nothing AndAlso State.ExpirationDate IsNot Nothing Then
                            If DateHelper.GetDateValue(State.EffectiveDate.ToString) < EquipmentListDetail.GetCurrentDateTime().Today Then
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
                            EquipmentList.ExpirePreviousList(State.Code, State.EffectiveDate, State.ExpirationDate, State.MyBO.Id)
                            State.HasDataChanged = True
                            PopulateFormFromBOs()
                            If Not State.IsChildCreated Then
                                State.IsEquipmentListEditing = True
                                EnableDisableFields()
                                EnableDisableUserControlButtons(True)
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

#End Region

#Region "Child_Control"

        Sub BeginChildEdit(equipmentId As Guid, expireNow As Boolean)
            State.IsEquipmentListEditing = True
            State.SelectedChildId = Guid.Empty
            State.SelectedChildId = New Guid(State.MyChildBO.IsChild(State.MyBO.Id, equipmentId))
            With State
                If Not .SelectedChildId = Guid.Empty Then
                    .MyChildBO = .MyBO.GetChild(.SelectedChildId)
                Else
                    .MyChildBO = .MyBO.GetNewChild
                End If
                .MyChildBO.BeginEdit()
            End With

            If expireNow Then
                SetExpiration(equipmentId)
            Else
                PopulateChildBOFromDetail(equipmentId)
            End If
        End Sub

        ''' <summary>
        ''' Update Child Equipment BO with exact Effective and Expiraiton
        ''' #1 : Update Equipment Id
        ''' #2 : Set earliest available expiration date 
        ''' #3 : Set Effective date if Equipment added was earlier existing equipment
        ''' </summary>
        ''' <param name="equipmentId">Equipment GUID</param>
        ''' <remarks></remarks>

        Sub PopulateChildBOFromDetail(equipmentId As Guid)
            Dim NewEquipmetExpiration As DateTime
            Dim SelectedEquipmetExpiration As DateTime
            Dim equipmentOldExpiraitonDate As DateTime

            With State.MyChildBO
                SelectedEquipmetExpiration = EquipmentListDetail.GetEquipmentExpiration(equipmentId)
                equipmentOldExpiraitonDate = CDate("#" & .Expiration.ToString & "#")

                If State.MyBO.Expiration IsNot Nothing Then
                    NewEquipmetExpiration = CDate("#" & State.MyBO.Expiration.ToString & "#")
                End If
                ''#1 
                .EquipmentId = equipmentId
                ''#2
                If Not SelectedEquipmetExpiration = Nothing AndAlso SelectedEquipmetExpiration < NewEquipmetExpiration Then
                    .Expiration = SelectedEquipmetExpiration
                Else
                    .Expiration = NewEquipmetExpiration
                End If
                ''#3
                If equipmentOldExpiraitonDate < EquipmentListDetail.GetCurrentDateTime() Then
                    .Effective = EquipmentListDetail.GetCurrentDateTime()
                End If
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub SetExpiration(equipmentId As Guid)
            With State.MyChildBO
                State.MyChildBO.EquipmentId = equipmentId
                State.MyChildBO.Expiration = EquipmentListDetail.GetCurrentDateTime().AddSeconds(-1)
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
                State.IsEquipmentListEditing = False
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Error Handling"


#End Region

#Region "User Control Event Handler"
        Protected Sub ExecuteSearchFilter(sender As Object, args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.ExecuteSearchFilter
            Dim equip As New Equipment
            Try
                args.dvAvailableEquipment = equip.ExecuteEquipmentListFilter(args.ManufactorerID, args.EquipmentClass, args.EquipmentType, args.Model, args.Description, Guid.Empty)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub SaveClicked(sender As Object, args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventSaveEquipmentListDetail
            Dim oEquipmentList As ArrayList

            Try
                oEquipmentList = New ArrayList()
                PopulateBOsFormFrom()
                oEquipmentList = EquipmentListDetail.GetEquipmentsInList(State.MyBO.Id)
                For Each argEquipment As String In args.listSelectedEquipment
                    For Each equipmentRaw As Byte() In oEquipmentList
                        If New Guid(equipmentRaw).ToString = argEquipment Then
                            oEquipmentList.Remove(equipmentRaw)
                            Exit For
                        End If
                    Next
                    BeginChildEdit(New Guid(argEquipment), False)
                    EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                For Each equipment As Byte() In oEquipmentList
                    BeginChildEdit(New Guid(equipment), True)
                    EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                State.HasDataChanged = True
                State.IsEquipmentListEditing = False
                State.IsChildCreated = True
                EnableDisableFields()
                EnableDisableParentControls(True)
                EnableDisableUserControlButtons(False)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub CancelButtonClicked(sender As Object, args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventCancelButtonClicked
            Try
                Dim equip As New EquipmentList
                UserControlSearchAvailableEquipment.dvSelectedEquipment = equip.GetSelectedEquipmentList(State.MyBO.Id)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub CustomPopulateDropDown(sender As Object, args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventCustomPopulateDropDown
            Try
                If sender IsNot Nothing Then
                    args.dvmakeList = LookupListNew.GetEquipmentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

    End Class

End Namespace
