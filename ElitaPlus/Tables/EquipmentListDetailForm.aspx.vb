Namespace Tables

    Partial Public Class EquipmentListDetailForm
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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As EquipmentList, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = New EquipmentList(CType(Me.CallingParameters, Guid))
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
                Dim retObj As EquipmentListDetailForm.ReturnType = CType(ReturnPar, EquipmentListDetailForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.EquipmentListDetailId = retObj.EditingBo.Id
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
            Me.State.Comments = Me.moCommentText.Text
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
            Me.moCommentText.Text = Me.State.MyBO.Comments
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
                        Me.State.MyBO = New EquipmentList
                    Else
                        Me.EnableHeaderControls(False)
                    End If

                    Me.ErrorCtrl.Clear_Hide()
                    If Not Me.IsPostBack Then
                        Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                        If Me.State.MyBO Is Nothing Then
                            Me.State.MyBO = New EquipmentList
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
                        If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) < EquipmentListDetail.GetCurrentDateTime() Then
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
                            If DateHelper.GetDateValue(Me.State.ExpirationDate.ToString) < EquipmentListDetail.GetCurrentDateTime() Then
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
                        If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) < EquipmentListDetail.GetCurrentDateTime() Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.EQUIPMENT_INVALID_EFFECIVE_DATE)
                        End If
                    End If
                End If

                '#2 - Restrict to save backdated list in edit mode
                If Not Me.State.ExpirationDate Is Nothing And Me.State.MyBO.IsNew = False Then
                    If DateHelper.GetDateValue(Me.State.MyBO.Expiration.ToString) <> DateHelper.GetDateValue(moExpirationDateText.Text.ToString) Then
                        If (Me.State.MyBO.CheckIfListIsAssignedToDealer(Me.State.MyBO.Code, Me.State.MyBO.Id)) Then
                            If DateHelper.GetDateValue(Me.State.ExpirationDate.ToString) < EquipmentListDetail.GetCurrentDateTime() Then
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
                        If DateHelper.GetDateValue(Me.State.EffectiveDate.ToString) < EquipmentListDetail.GetCurrentDateTime().Today Then
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

                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormFromBOs()
                    If Not Me.State.IsChildCreated Then
                        Me.State.IsEquipmentListEditing = True
                        Me.EnableDisableUserControlButtons(True)
                        Me.EnableDisableFields()
                        Me.EnableHeaderControls(False)
                        Me.State.IsChildCreated = False
                    End If
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
                    Me.State.MyBO = New EquipmentList(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New EquipmentList
                End If
                Me.State.IsEquipmentListEditing = False
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.EnableDisableUserControlButtons(False)
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
                    If DateHelper.GetDateValue(Me.State.MyBO.Effective.ToString) > EquipmentListDetail.GetCurrentDateTime() Then
                        EquipmentList.ExpireList(Me.State.MyBO.Effective, Me.State.MyBO.Id)
                    Else
                        Me.State.MyBO.Delete()
                        Me.State.MyBO.Save()
                        Me.State.HasDataChanged = True
                    End If
                    If DateHelper.GetDateValue(moEffectiveDateText.Text.ToString) > EquipmentListDetail.GetCurrentDateTime() Then
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

        Private Sub ExpireListAndListItems()
            Try
                Dim oEquipmentList As ArrayList = New ArrayList()
                If DateHelper.GetDateValue(Me.State.MyBO.Effective.ToString) > EquipmentListDetail.GetCurrentDateTime() Then
                    Me.moExpirationDateText.Text = Me.moEffectiveDateText.Text
                    Me.PopulateBOsFormFrom()
                    oEquipmentList = EquipmentListDetail.GetEquipmentsInList(Me.State.MyBO.Id)
                    For Each argEquipment As Byte() In oEquipmentList
                        Me.BeginChildEdit(New Guid(argEquipment), True)
                        Me.State.MyChildBO.Expiration = Me.State.MyBO.Effective
                        Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                    Next
                End If
            Catch ex As Exception
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

            If (EquipmentList.CheckDuplicateEquipmentListCode(code, CDate(effective).ToString(ElitaPlusPage.DATE_TIME_FORMAT, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")), id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeOverlapped(ByVal code As String, ByVal effective As DateType,
                                        ByVal expiration As DateType, ByVal id As Guid) As Boolean

            If (EquipmentList.CheckListCodeForOverlap(code, effective, expiration, id)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function IsListCodeDurationOverlapped(ByVal code As String, ByVal effective As DateType,
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

            If (EquipmentList.CheckListCodeDurationOverlap(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Function ExpirePreviousList(ByVal code As String, ByVal effective As DateType,
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

            If (EquipmentList.ExpirePreviousList(code, effective, expiration, listId)) Then
                Return True
            End If
            Return False
        End Function

        Protected Sub EnableUserControl(ByVal bVisible As Boolean)
            UserControlSearchAvailableEquipment.ShowCancelButton = True
            Dim equip As New EquipmentList
            UserControlSearchAvailableEquipment.dvSelectedEquipment = equip.GetSelectedEquipmentList(Me.State.MyBO.Id)
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
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Comments", Me.moCommentLabel)
        End Sub

        Protected Sub PopulateBOsFormFrom()
            With Me.State.MyBO
                Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.moCodeText)
                Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.moDescriptionText)
                Me.PopulateBOProperty(Me.State.MyBO, "Effective", Me.moEffectiveDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "Expiration", Me.moExpirationDateText)
                Me.PopulateBOProperty(Me.State.MyBO, "Comments", Me.moCommentText)
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub PopulateChildern()
            Dim detail As EquipmentListDetailList = Me.State.MyBO.BestEquipmentListChildren
        End Sub

        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(Me.moCodeText, .Code)
                Me.PopulateControlFromBOProperty(Me.moDescriptionText, .Description)
                Me.PopulateControlFromBOProperty(Me.moCommentText, .Comments)
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

        Sub EnableHeaderControls(ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, moCodeText, enableToggle)
            If (Me.State.MyBO.CheckIfListIsAssignedToDealer(Me.State.MyBO.Code, Me.State.MyBO.Id)) Then
                ControlMgr.SetEnableControl(Me, moDescriptionText, enableToggle)
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, enableToggle)
                ControlMgr.SetEnableControl(Me, moCommentText, enableToggle)
            Else
                ControlMgr.SetEnableControl(Me, moDescriptionText, Not (enableToggle))
                ControlMgr.SetEnableControl(Me, moEffectiveDateText, Not (enableToggle))
                ControlMgr.SetEnableControl(Me, moCommentText, Not (enableToggle))
            End If
        End Sub

        Sub EnableDisableParentControls(ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
        End Sub

        Sub EnableDisableUserControlButtons(ByVal enableToggle As Boolean)
            ControlMgr.SetEnableControl(Me, PanelCommentEditDetail, enableToggle)
        End Sub

        Protected Sub EnableDisableFields()
            If Me.State.IsEquipmentListEditing Then
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
                ControlMgr.SetEnableControl(Me, moCommentText, True)

                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
                EnableDisableUserControlButtons(False)
            End If
        End Sub

        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            Me.State.MyBO = New EquipmentList
            Me.State.IsEquipmentListEditing = False
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
            Me.State.IsChildCreated = False
            Me.State.IsEditMode = False
            CType(Me.UserControlSearchAvailableEquipment.FindControl("UserControlAvailableSelectedEquipmentCodes").FindControl("moSelectedList"), ListBox).Items.Clear()
        End Sub

        Protected Sub CreateNewWithCopy()
            Me.State.IsACopy = True
            Dim newObj As New EquipmentList
            newObj.Copy(Me.State.MyBO)

            Me.State.MyBO = newObj
            Me.State.MyBO.Effective = EquipmentListDetail.GetCurrentDateTime()
            Me.State.MyBO.Expiration = New DateTime(2499, 12, 31, 23, 59, 59)
            Me.State.MyBO.Code = Nothing
            Me.State.MyBO.Description = Nothing
            Me.State.MyBO.Comments = Nothing
            Me.PopulateFormFromBOs()

            Me.State.IsEquipmentListEditing = False
            Me.EnableDisableFields()

            Me.State.ScreenSnapShotBO = New EquipmentList
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
                            If DateHelper.GetDateValue(Me.State.EffectiveDate.ToString) < EquipmentListDetail.GetCurrentDateTime().Today Then
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
                            EquipmentList.ExpirePreviousList(Me.State.Code, Me.State.EffectiveDate, Me.State.ExpirationDate, Me.State.MyBO.Id)
                            Me.State.HasDataChanged = True
                            Me.PopulateFormFromBOs()
                            If Not Me.State.IsChildCreated Then
                                Me.State.IsEquipmentListEditing = True
                                Me.EnableDisableFields()
                                Me.EnableDisableUserControlButtons(True)
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

#End Region

#Region "Child_Control"

        Sub BeginChildEdit(ByVal equipmentId As Guid, ByVal expireNow As Boolean)
            Me.State.IsEquipmentListEditing = True
            Me.State.SelectedChildId = Guid.Empty
            Me.State.SelectedChildId = New Guid(Me.State.MyChildBO.IsChild(Me.State.MyBO.Id, equipmentId))
            With Me.State
                If Not .SelectedChildId = Guid.Empty Then
                    .MyChildBO = .MyBO.GetChild(.SelectedChildId)
                Else
                    .MyChildBO = .MyBO.GetNewChild
                End If
                .MyChildBO.BeginEdit()
            End With

            If expireNow Then
                Me.SetExpiration(equipmentId)
            Else
                Me.PopulateChildBOFromDetail(equipmentId)
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

        Sub PopulateChildBOFromDetail(ByVal equipmentId As Guid)
            Dim NewEquipmetExpiration As DateTime
            Dim SelectedEquipmetExpiration As DateTime
            Dim equipmentOldExpiraitonDate As DateTime

            With Me.State.MyChildBO
                SelectedEquipmetExpiration = EquipmentListDetail.GetEquipmentExpiration(equipmentId)
                equipmentOldExpiraitonDate = CDate("#" & .Expiration.ToString & "#")

                If Not Me.State.MyBO.Expiration Is Nothing Then
                    NewEquipmetExpiration = CDate("#" & Me.State.MyBO.Expiration.ToString & "#")
                End If
                ''#1 
                .EquipmentId = equipmentId
                ''#2
                If Not SelectedEquipmetExpiration = Nothing And SelectedEquipmetExpiration < NewEquipmetExpiration Then
                    .Expiration = SelectedEquipmetExpiration
                Else
                    .Expiration = NewEquipmetExpiration
                End If
                ''#3
                If equipmentOldExpiraitonDate < EquipmentListDetail.GetCurrentDateTime() Then
                    .Effective = EquipmentListDetail.GetCurrentDateTime()
                End If
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Sub SetExpiration(ByVal equipmentId As Guid)
            With Me.State.MyChildBO
                Me.State.MyChildBO.EquipmentId = equipmentId
                Me.State.MyChildBO.Expiration = EquipmentListDetail.GetCurrentDateTime().AddSeconds(-1)
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
                Me.State.IsEquipmentListEditing = False
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Error Handling"


#End Region

#Region "User Control Event Handler"
        Protected Sub ExecuteSearchFilter(ByVal sender As Object, ByVal args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.ExecuteSearchFilter
            Dim equip As New Equipment
            Try
                args.dvAvailableEquipment = equip.ExecuteEquipmentListFilter(args.ManufactorerID, args.EquipmentClass, args.EquipmentType, args.Model, args.Description, Guid.Empty)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub SaveClicked(ByVal sender As Object, ByVal args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventSaveEquipmentListDetail
            Dim oEquipmentList As ArrayList

            Try
                oEquipmentList = New ArrayList()
                Me.PopulateBOsFormFrom()
                oEquipmentList = EquipmentListDetail.GetEquipmentsInList(Me.State.MyBO.Id)
                For Each argEquipment As String In args.listSelectedEquipment
                    For Each equipmentRaw As Byte() In oEquipmentList
                        If New Guid(equipmentRaw).ToString = argEquipment Then
                            oEquipmentList.Remove(equipmentRaw)
                            Exit For
                        End If
                    Next
                    Me.BeginChildEdit(New Guid(argEquipment), False)
                    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                For Each equipment As Byte() In oEquipmentList
                    Me.BeginChildEdit(New Guid(equipment), True)
                    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                Next
                Me.State.HasDataChanged = True
                Me.State.IsEquipmentListEditing = False
                Me.State.IsChildCreated = True
                Me.EnableDisableFields()
                Me.EnableDisableParentControls(True)
                Me.EnableDisableUserControlButtons(False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub CancelButtonClicked(ByVal sender As Object, ByVal args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventCancelButtonClicked
            Try
                Dim equip As New EquipmentList
                UserControlSearchAvailableEquipment.dvSelectedEquipment = equip.GetSelectedEquipmentList(Me.State.MyBO.Id)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub CustomPopulateDropDown(ByVal sender As Object, ByVal args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventCustomPopulateDropDown
            Try
                If Not sender Is Nothing Then
                    args.dvmakeList = LookupListNew.GetEquipmentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

    End Class

End Namespace
