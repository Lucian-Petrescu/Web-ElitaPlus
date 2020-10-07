Imports System.Linq
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class WorkQueueForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "


    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "~/WorkQueue/WorkQueueForm.aspx"

    Public Const PAGETAB As String = "TABLES"
    Public Const PAGESUBTAB As String = "WORK_QUEUE"
    Public Const PAGETITLE As String = "WORK_QUEUE_MAINTENANCE"

    ' ReQueue / ReDirect Reason  Grid
    Public Const GRID_COL_CODE_IDX As Integer = 0
    Public Const GRID_COL_DESCRIPTION_IDX As Integer = 1
    Public Const GRID_COL_EFFECTIVE_IDX As Integer = 2
    Public Const GRID_COL_EXPIRATION_IDX As Integer = 3

    Public Const GRID_COL_CODE_LABEL As String = "moCodeLabel"
    Public Const GRID_COL_DESCRIPTION_LABEL As String = "moDescriptionLabel"
    Public Const GRID_COL_CODE_DROPDOWN As String = "moCodeDropDown"
    Public Const GRID_COL_DESCRIPTION_DROPDOWN As String = "moDescriptionDropDown"
    Public Const GRID_COL_EFFECTIVE_LABEL As String = "moEffectiveDateLabel"
    Public Const GRID_COL_EFFECTIVE_TEXT As String = "moEffectiveDateText"
    Public Const GRID_COL_EFFECTIVE_IMAGE As String = "btnEffectiveDate"
    Public Const GRID_COL_EXPIRATION_LABEL As String = "moExpirationDateLabel"
    Public Const GRID_COL_EXPIRATION_TEXT As String = "moExpirationDateText"
    Public Const GRID_COL_EXPIRATION_IMAGE As String = "btnExpirationDate"
    Public Const GRID_COL_EDIT_IMAGE_BUTTON As String = "moEdit"
    Public Const GRID_COL_DELETE_IMAGE_BUTTON As String = "moDelete"

    Private Const TAB_TOTAL_COUNT As Integer = 3
    Private Const TAB_SCHEDULES As Integer = 0
    Private Const TAB_REQUEUE_REASONS As Integer = 1
    Private Const TAB_REDIRECT_REASONS As Integer = 2
#End Region


#Region "Page State"

    Class MyState
        Public MyBO As WorkQueue
        Public ScreenSnapShotBOId As Guid
        Public MyReQueueReasonChildBO As WorkQueueItemStatusReason
        Public MyReDirectReasonChildBO As WorkQueueItemStatusReason
        Public MyScheduleChildBO As EntitySchedule
        Public IsReadOnly As Boolean
        Public UserHasPermissions As Boolean

        Public IsScheduleEditing As Boolean = False
        Public IsReQueueReasonEditing As Boolean = False
        Public IsReDirectReasonEditing As Boolean = False

        Public IsScheduleAdding As Boolean = False
        Public IsReQueueReasonAdding As Boolean = False
        Public IsReDirectReasonAdding As Boolean = False

        Public ReQueueReasonSortDirection As LinqExtentions.SortDirection = LinqExtentions.SortDirection.Ascending
        Public ReQueueReasonSortExpression As String = WorkQueueItemStatusReason.COL_REASON

        Public ReDirectReasonSortDirection As LinqExtentions.SortDirection = LinqExtentions.SortDirection.Ascending
        Public ReDirectReasonSortExpression As String = WorkQueueItemStatusReason.COL_REASON

        Public ScheduleSortExpression As String = EntitySchedule.COL_NAME_EFFECTIVE

        Public ReQueueReasonSelectedChildId As Guid = Guid.Empty
        Public ReDirectReasonSelectedChildId As Guid = Guid.Empty
        Public EntityScheduleChildId As Guid = Guid.Empty

        Public WorkQueueReturnObject As WorkQueueListForm.WorkQueueReturnType
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

#End Region


#Region "Page Events"
    Private listDisabledTabs As New Collections.Generic.List(Of Integer)
    Private SelectedTabIndex As Integer = 0

    Private Sub Page_PageCall(CallFromUrl As String, CallingParameters As Object) Handles MyBase.PageCall
        Try
            If Me.CallingParameters IsNot Nothing Then
                State.WorkQueueReturnObject = CType(CallingParameters, WorkQueueListForm.WorkQueueReturnType)
                If (Not State.WorkQueueReturnObject.WorkQueueId.Equals(Guid.Empty)) Then
                    State.MyBO = New WorkQueue(State.WorkQueueReturnObject.WorkQueueId)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            MasterPage.MessageController.Clear()
            If (Not IsPostBack) Then
                ' Popup Configuration for Delete Message Box
                lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_PROMPT")
                btnModalCancelYes.Attributes.Add("onclick", "YesButtonClick();")

                ' Date Calendars
                AddCalendarwithTime_New(btnEffectiveDate, moEffectiveDate)
                AddCalendarwithTime_New(btnExpirationDate, moExpirationDate)

                If (State.MyBO Is Nothing) Then
                    State.MyBO = New WorkQueue()
                End If

                Dim extendedUser As Auth.ExtendedUser
                State.UserHasPermissions = True
                extendedUser = ElitaPlusIdentity.Current.ActiveUser.ExtendedUser
                If (Not extendedUser.HasPermission(New Auth.Permission With {.ResourceType = ServiceHelper.RESTYP_WORKQUEUESYSTEM, .Resource = ServiceHelper.RES_WORKQUEUESYSTEM, .Action = ServiceHelper.PA_WQS_CREATE_QUEUE})) Then
                    State.UserHasPermissions = False
                End If
                If (State.UserHasPermissions AndAlso Not extendedUser.HasPermission(New Auth.Permission With {.ResourceType = ServiceHelper.RESTYP_WORKQUEUESYSTEM, .Resource = ServiceHelper.RES_WORKQUEUESYSTEM, .Action = ServiceHelper.PA_WQS_MANAGE_ITEM_STATUS})) Then
                    State.UserHasPermissions = False
                End If

                ' Translate Grid Headers
                TranslateGridHeader(GridViewReDirectReasons)
                TranslateGridHeader(GridViewSchedules)
                TranslateGridHeader(GridViewReQueueReasons)

                ' Populate Bread Crum
                UpdateBreadCrum()

                ' Populate Drop Downs
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields()
            Else
                If (State.MyBO.IsNew) Then
                    PopulateBOProperty(State.MyBO.WorkQueue, "TimeZoneCode", GetSelectedValue(moTimeZoneDropDown))
                    PopulateBOProperty(State.MyBO.WorkQueue, "Effective", moEffectiveDate)
                    PopulateBOProperty(State.MyBO.WorkQueue, "Expiration", moExpirationDate)
                End If
                BindBoPropertiesToGridHeaders()
                SelectedTabIndex = hdnSelectedTab.Value
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub WorkQueueForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        hdnSelectedTab.Value = SelectedTabIndex
        Dim strTemp As String = String.Empty
        If listDisabledTabs.Count > 0 Then
            For Each i As Integer In listDisabledTabs
                strTemp = strTemp + "," + i.ToString
            Next
            strTemp = strTemp.Substring(1) 'remove the first comma
        End If

        hdnDisabledTabs.Value = strTemp
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            State.WorkQueueReturnObject.WorkQueueId = State.MyBO.WorkQueue.Id
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.WorkQueueReturnObject, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                State.MyBO = New WorkQueue(State.MyBO.Id)
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New WorkQueue(State.MyBO.Id)
            ElseIf Not State.ScreenSnapShotBOId.Equals(Guid.Empty) Then
                'It was a new with copy
                State.MyBO = New WorkQueue(State.ScreenSnapShotBOId)
            Else
                State.MyBO = New WorkQueue()
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DoDelete()
        State.MyBO.Delete()
        State.MyBO.Save()
        State.HasDataChanged = True
        State.WorkQueueReturnObject.WorkQueueId = State.MyBO.WorkQueue.Id
        ReturnToCallingPage(New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(ElitaPlusPage.DetailPageCommand.Delete, State.WorkQueueReturnObject, State.HasDataChanged))
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
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
            HandleErrors(ex, MasterPage.MessageController)
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
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Delete _
                AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Expire Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.WorkQueueReturnObject, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.WorkQueueReturnObject, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DoDelete()
                Case ElitaPlusPage.DetailPageCommand.Expire
                    If (State.IsScheduleAdding) Then
                        Try
                            State.MyScheduleChildBO.Validate()
                        Catch ex As BOValidationException
                            Dim validationError As ValidationError() = ex.ValidationErrorList()
                            If (validationError.Length = 1 AndAlso validationError(0).Message = Assurant.ElitaPlus.Common.ErrorCodes.OVERLAPPING_SCHEDULE_ERR) Then
                                For Each drv As DataRowView In State.MyBO.GetScheduleSelectionView
                                    Dim es As EntitySchedule = State.MyBO.GetScheduleChild(New Guid(CType(drv(EntitySchedule.ScheduleSelectionView.COL_NAME_ENTITY_SCHEDULE_ID), Byte())))
                                    If (Not es.Id.Equals(State.MyScheduleChildBO.Id)) Then
                                        If (State.MyScheduleChildBO.Effective.Value > es.Effective.Value AndAlso State.MyScheduleChildBO.Effective.Value < es.Expiration.Value) Then
                                            es.BeginEdit()
                                            es.Expiration = State.MyScheduleChildBO.Effective.Value.AddSeconds(-1)
                                            es.EndEdit()
                                            State.MyScheduleChildBO.Validate()
                                            Exit For
                                        End If
                                    End If
                                Next
                            Else
                                Throw
                            End If
                        End Try
                        State.MyScheduleChildBO.EndEdit()
                        State.IsScheduleAdding = False
                        State.IsScheduleEditing = False
                        State.MyScheduleChildBO = Nothing
                        EnableDisableFields()
                        PopulateScheduleGrid()
                    End If

                    'Case ElitaPlusPage.DetailPageCommand.Accept
                    '            If (Me.State.IsCommentEditing) Then
                    '                Me.PopulateCommentChildBOFromDetail()
                    '                Me.State.MyCommentChildBO.Save()
                    '                Me.State.MyCommentChildBO.EndEdit()
                    '                Me.State.IsCommentEditing = False
                    '            ElseIf (Me.State.IsImageEditing) Then
                    '                Me.PopulateImageChildBOFromDetail()
                    '                Me.State.MyImageChildBO.Save()
                    '                Me.State.MyImageChildBO.EndEdit()
                    '                Me.State.IsImageEditing = False
                    '            ElseIf (Me.State.IsAttributeValueEditing) Then
                    '                Me.PopulateAttributeValueChildBOFromDetail()
                    '                Me.State.MyAttributeValueChildBO.Save()
                    '                Me.State.MyAttributeValueChildBO.EndEdit()
                    '                Me.State.IsAttributeValueEditing = False
                    '            ElseIf (Me.State.IsRelatedEquipmentEditing) Then
                    '                Me.PopulateRelatedEquipmentChildBOFromDetail()
                    '                Me.State.MyRelatedEquipmentChildBO.Save()
                    '                Me.State.MyRelatedEquipmentChildBO.EndEdit()
                    '                Me.State.IsAttributeValueEditing = False
                    '            End If
                    '            Me.EnableDisableFields()
                    '            Me.PopulateCommentDetailGrid()
                    '            Me.PopulateImageDetailGrid()
                    '            Me.PopulateAttributeValueDetailGrid()
                    '            Me.PopulateRelatedEquipmentDetailGrid()
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(ElitaPlusPage.DetailPageCommand.Back, State.WorkQueueReturnObject, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)

                    '        Case ElitaPlusPage.DetailPageCommand.Accept
                    '            If (Me.State.IsCommentEditing) Then
                    '                Me.State.MyCommentChildBO.cancelEdit()
                    '                If Me.State.MyCommentChildBO.IsSaveNew Then
                    '                    Me.State.MyCommentChildBO.Delete()
                    '                    Me.State.MyCommentChildBO.Save()
                    '                End If
                    '                Me.State.IsCommentEditing = False
                    '            ElseIf (Me.State.IsImageEditing) Then
                    '                Me.State.MyImageChildBO.cancelEdit()
                    '                If Me.State.MyCommentChildBO.IsSaveNew Then
                    '                    Me.State.MyImageChildBO.Delete()
                    '                    Me.State.MyImageChildBO.Save()
                    '                End If
                    '                Me.State.IsImageEditing = False
                    '            ElseIf (Me.State.IsAttributeValueEditing) Then
                    '                Me.State.MyAttributeValueChildBO.cancelEdit()
                    '                If Me.State.MyAttributeValueChildBO.IsSaveNew Then
                    '                    Me.State.MyAttributeValueChildBO.Delete()
                    '                    Me.State.MyAttributeValueChildBO.Save()
                    '                End If
                    '                Me.State.IsAttributeValueEditing = False
                    '            ElseIf (Me.State.IsRelatedEquipmentEditing) Then
                    '                Me.State.MyRelatedEquipmentChildBO.cancelEdit()
                    '                If Me.State.MyRelatedEquipmentChildBO.IsSaveNew Then
                    '                    Me.State.MyRelatedEquipmentChildBO.Delete()
                    '                    Me.State.MyRelatedEquipmentChildBO.Save()
                    '                End If
                    '                Me.State.IsRelatedEquipmentEditing = False
                    '            Me.EnableDisableFields()
                    '            Me.PopulateCommentDetailGrid()
                    '            Me.PopulateImageDetailGrid()
                    '            Me.PopulateAttributeValueDetailGrid()
                    '            Me.PopulateRelatedEquipmentDetailGrid()
            End Select
        End If

        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub UpdateBreadCrum()
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGESUBTAB) & ElitaBase.Sperator
        MasterPage.BreadCrum = MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        MasterPage.UsePageTabTitleInBreadCrum = False
    End Sub

    Private Sub PopulateDropdowns()
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim workQueueTypes As ListItem()
        Dim workQueueItemDataTypes As ListItem()
        Dim timeZones As ListItem()
        Dim companies As ListItem()
        Dim roles As ListItem()
        Dim actions As ListItem()

        workQueueTypes = (From wqt In WorkQueue.GetWorkQueueTypes() Select New ListItem(wqt.Name, wqt.Id.ToString())).ToArray()
        workQueueItemDataTypes = (From wqidt In WorkQueue.GetWorkQueueItemDataTypes() Select New ListItem(wqidt.Name, wqidt.Id.ToString())).ToArray()
        timeZones = (From tzi In TimeZoneInfo.GetSystemTimeZones() Order By tzi.BaseUtcOffset Select New ListItem(tzi.StandardName, tzi.StandardName)).ToArray()
        companies = (From llItem As DataRow In LookupListNew.GetCompanyLookupList().ToTable().AsEnumerable() _
                    Select New ListItem(llItem.Field(Of String)(LookupListNew.COL_DESCRIPTION_NAME), llItem.Field(Of String)(LookupListNew.COL_CODE_NAME))).ToArray()
        roles = (From llItem As DataRow In LookupListNew.GetRolesLookupList().ToTable().AsEnumerable() _
            Select New ListItem(llItem.Field(Of String)(LookupListNew.COL_DESCRIPTION_NAME), llItem.Field(Of String)(LookupListNew.COL_CODE_NAME))).ToArray()
        actions = (From llItem As DataRow In LookupListNew.GetWorkQueueAction(languageId).ToTable().AsEnumerable() _
            Select New ListItem(llItem.Field(Of String)(LookupListNew.COL_DESCRIPTION_NAME), llItem.Field(Of String)(LookupListNew.COL_CODE_NAME))).ToArray()

        BindListControlToArray(moCompanyDropDown, companies)
        BindListControlToArray(moAdminRole, roles)
        BindListControlToArray(moActionDropDown, actions)
        BindListControlToArray(moWorkQueueTypeDropDown, workQueueTypes, , , Guid.Empty.ToString())
        BindListControlToArray(moLockableDataType, workQueueItemDataTypes, , , Guid.Empty.ToString())
        BindListControlToArray(moTimeZoneDropDown, timeZones)
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "Name", moWorkQueueNameLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "CompanyCode", moCompanyLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "TimeZoneCode", moTimeZoneLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "Effective", moEffectiveDateLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "ActiveOn", moEffectiveDateLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "Expiration", moExpirationDateLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "StartItemDelayMinutes", moStartItemDelayMinutesLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "TimeToCompleteMinutes", moTimeToCompleteMinutesLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "MaxRequeue", moMaximumReQueueLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "WorkQueueTypeId", moWorkQueueTypeLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "AdminRole", moAdminRoleLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "ActionCode", moActionLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "TransformationFile", moTransformationFileLabel)
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "LockableDataTypeId", moLockableDataTypeLabel)
        'DEF-3035
        BindBOPropertyToLabel(State.MyBO.WorkQueue, "RequeueItemDelayMinutes", moReQueueDelayLabel)
        'DEF-3035 End
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateFormFromBOs()
        moMessageController.Clear()
        With State.MyBO
            State.IsReadOnly = False
            If (.WorkQueue.InActiveOn.HasValue AndAlso .WorkQueue.InActiveOn.Value < DateTime.UtcNow) Then State.IsReadOnly = True

            If (Not State.UserHasPermissions) Then
                moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_NO_CREATEQUEUE_MANAGEITEMSTATUSLIST_PERMISSION)
                State.IsReadOnly = True
            End If
            If (Not .IsNew) Then
                If (Not ElitaPlusIdentity.Current.ActiveUser.isInRole(.WorkQueue.AdminRole)) Then
                    moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_NO_ADMIN_ROLE)
                    State.IsReadOnly = True
                End If

                If (Not ElitaPlusIdentity.Current.ActiveUser.ExtendedUser.HasPermission(New Auth.Permission With {.ResourceType = ServiceHelper.RESTYP_WORKQUEUE, .Resource = State.MyBO.WorkQueue.Name, .Action = ServiceHelper.PA_WQ_EDIT})) Then
                    moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_NO_EDIT_PERMISSION)
                    State.IsReadOnly = True
                End If
            End If

            PopulateScheduleGrid()
            PopulateReQueueReasonGrid()
            PopulateReDirectReasonGrid()

            PopulateControlFromBOProperty(moWorkQueueName, .WorkQueue.Name)
            SetSelectedItem(moCompanyDropDown, .WorkQueue.CompanyCode)
            SetSelectedItem(moTimeZoneDropDown, .WorkQueue.TimeZoneCode)
            PopulateControlFromBOProperty(moEffectiveDate, .WorkQueue.Effective)
            PopulateControlFromBOProperty(moExpirationDate, .WorkQueue.Expiration)
            PopulateControlFromBOProperty(moStartItemDelayMinutes, .WorkQueue.StartItemDelayMinutes)
            PopulateControlFromBOProperty(moTimeToCompleteMinutes, .WorkQueue.TimeToCompleteMinutes)
            PopulateControlFromBOProperty(moMaximumReQueue, .WorkQueue.MaxRequeue)
            PopulateControlFromBOProperty(moWorkQueueTypeDropDown, .WorkQueue.WorkQueueTypeId)
            SetSelectedItem(moAdminRole, .WorkQueue.AdminRole)
            SetSelectedItem(moActionDropDown, .WorkQueue.ActionCode)
            PopulateControlFromBOProperty(moTransformationFile, .WorkQueue.TransformationFile)
            PopulateControlFromBOProperty(moLockableDataType, .WorkQueue.LockableDataTypeId)
            'DEF-3035
            PopulateControlFromBOProperty(moReQueueDelay, .WorkQueue.RequeueItemDelayMinutes)
            'End of DEF-3035

            moCompanyText.Text = GetSelectedDescription(moCompanyDropDown)
            moActionText.Text = GetSelectedDescription(moActionDropDown)
            moWorkQueueTypeText.Text = GetSelectedDescription(moWorkQueueTypeDropDown)
            moTimeZoneText.Text = GetSelectedDescription(moTimeZoneDropDown)

            moWorkQueueName.ReadOnly = Not .IsNew
            ControlMgr.SetVisibleControl(Me, moCompanyDropDown, .IsNew)
            ControlMgr.SetVisibleControl(Me, moCompanyText, Not .IsNew)
            ControlMgr.SetVisibleControl(Me, moActionDropDown, .IsNew)
            ControlMgr.SetVisibleControl(Me, moActionText, Not .IsNew)
            ControlMgr.SetVisibleControl(Me, moWorkQueueTypeDropDown, .IsNew)
            ControlMgr.SetVisibleControl(Me, moWorkQueueTypeText, Not .IsNew)
            ControlMgr.SetVisibleControl(Me, moTimeZoneDropDown, .IsNew)
            ControlMgr.SetVisibleControl(Me, moTimeZoneText, Not .IsNew)
            moEffectiveDate.ReadOnly = Not .IsNew
            ControlMgr.SetVisibleControl(Me, btnEffectiveDate, .IsNew)

            moExpirationDate.ReadOnly = False
            ControlMgr.SetVisibleControl(Me, btnExpirationDate, True)
            If ((Not .IsNew) AndAlso .WorkQueue.InActiveOn.HasValue) Then
                If (.WorkQueue.InActiveOn < Date.UtcNow) Then
                    moExpirationDate.ReadOnly = True
                    ControlMgr.SetVisibleControl(Me, btnExpirationDate, False)
                End If
            End If
        End With
    End Sub

    Private Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.MyScheduleChildBO, "ScheduleId", GridViewSchedules.Columns(GRID_COL_CODE_IDX))
        BindBOPropertyToGridHeader(State.MyScheduleChildBO, "ScheduleCode", GridViewSchedules.Columns(GRID_COL_CODE_IDX))
        BindBOPropertyToGridHeader(State.MyScheduleChildBO, "ScheduleDescription", GridViewSchedules.Columns(GRID_COL_DESCRIPTION_IDX))
        BindBOPropertyToGridHeader(State.MyScheduleChildBO, "Effective", GridViewSchedules.Columns(GRID_COL_EFFECTIVE_IDX))
        BindBOPropertyToGridHeader(State.MyScheduleChildBO, "Expiration", GridViewSchedules.Columns(GRID_COL_EXPIRATION_IDX))
        BindBOPropertyToGridHeader(State.MyScheduleChildBO, "EntityEffective", GridViewSchedules.Columns(GRID_COL_EFFECTIVE_IDX))

        BindBOPropertyToGridHeader(State.MyReDirectReasonChildBO, "Reason", GridViewReDirectReasons.Columns(GRID_COL_CODE_IDX))
        BindBOPropertyToGridHeader(State.MyReDirectReasonChildBO, "Description", GridViewReDirectReasons.Columns(GRID_COL_DESCRIPTION_IDX))

        BindBOPropertyToGridHeader(State.MyReQueueReasonChildBO, "Reason", GridViewReQueueReasons.Columns(GRID_COL_CODE_IDX))
        BindBOPropertyToGridHeader(State.MyReQueueReasonChildBO, "Description", GridViewReQueueReasons.Columns(GRID_COL_DESCRIPTION_IDX))

        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Protected Sub EnableDisableFields()
        If State.IsScheduleEditing OrElse State.IsReDirectReasonEditing OrElse State.IsReQueueReasonEditing Then
            EnableDisableParentControls(False)
        Else
            EnableDisableParentControls(True)
        End If
    End Sub

    Sub EnableDisableParentControls(enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
        'ControlMgr.SetEnableControl(Me, tsHoriz, enableToggle)
        EnableDisableTabs(enableToggle)
        ControlMgr.SetEnableControl(Me, btnAddNewSchedule_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnAddNewReDirectReason_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnAddNewReQueueReason_WRITE, enableToggle)

        moWorkQueueName.ReadOnly = Not (State.MyBO.IsNew)
        ControlMgr.SetEnableControl(Me, moCompanyDropDown, State.MyBO.IsNew)
        ControlMgr.SetEnableControl(Me, moTimeZoneDropDown, State.MyBO.IsNew)
        moEffectiveDate.ReadOnly = Not (State.MyBO.IsNew)
        ControlMgr.SetVisibleControl(Me, btnEffectiveDate, State.MyBO.IsNew)

        If (enableToggle) Then
            'Enabled by Default
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)

            'Now disable depebding on the object state
            If State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If
            If (State.IsReadOnly) Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
                ControlMgr.SetEnableControl(Me, btnAddNewReDirectReason_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnAddNewReQueueReason_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnAddNewSchedule_WRITE, False)
            End If
            If (Not State.UserHasPermissions) Then
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If
        End If
    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBOId = Guid.Empty 'Reset the backup copy
        State.MyBO = New WorkQueue()
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New WorkQueue
        newObj.Copy(State.MyBO)

        'create the backup copy
        State.ScreenSnapShotBOId = State.MyBO.Id

        State.MyBO = newObj
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO.WorkQueue, "Name", moWorkQueueName)
            PopulateBOProperty(State.MyBO.WorkQueue, "CompanyCode", GetSelectedValue(moCompanyDropDown))
            PopulateBOProperty(State.MyBO.WorkQueue, "TimeZoneCode", GetSelectedValue(moTimeZoneDropDown))
            PopulateBOProperty(State.MyBO.WorkQueue, "Effective", moEffectiveDate)
            PopulateBOProperty(State.MyBO.WorkQueue, "Expiration", moExpirationDate)
            PopulateBOProperty(State.MyBO.WorkQueue, "StartItemDelayMinutes", moStartItemDelayMinutes)
            PopulateBOProperty(State.MyBO.WorkQueue, "TimeToCompleteMinutes", moTimeToCompleteMinutes)
            PopulateBOProperty(State.MyBO.WorkQueue, "MaxRequeue", moMaximumReQueue)
            PopulateBOProperty(State.MyBO.WorkQueue, "WorkQueueTypeId", moWorkQueueTypeDropDown)
            PopulateBOProperty(State.MyBO.WorkQueue, "AdminRole", GetSelectedValue(moAdminRole))
            PopulateBOProperty(State.MyBO.WorkQueue, "ActionCode", GetSelectedValue(moActionDropDown))
            PopulateBOProperty(State.MyBO.WorkQueue, "TransformationFile", moTransformationFile)
            PopulateBOProperty(State.MyBO.WorkQueue, "LockableDataTypeId", moLockableDataType)
            'DEF-3035
            PopulateBOProperty(State.MyBO.WorkQueue, "RequeueItemDelayMinutes", moReQueueDelay)
            'DEF-3035 End
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub PopulateScheduleGrid()
        Dim isEmpty As Boolean
        Dim dv As EntitySchedule.ScheduleSelectionView = State.MyBO.GetScheduleSelectionView()
        dv.Sort = State.ScheduleSortExpression

        If (dv.Count > 0) Then
            GridViewSchedules.PageSize = dv.Count
        Else
            Dim dr As DataRow
            dr = dv.Table.NewRow()
            dr(EntitySchedule.ScheduleSelectionView.COL_NAME_IS_NEW) = True
            dr(EntitySchedule.ScheduleSelectionView.COL_NAME_ENTITY_SCHEDULE_ID) = Guid.NewGuid().ToByteArray()
            dr(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE) = DateTime.Now
            dv.Table.Rows.Add(dr)
            isEmpty = True
        End If

        SetPageAndSelectedIndexFromGuid(dv, State.EntityScheduleChildId, GridViewSchedules, 1, State.IsScheduleEditing)

        GridViewSchedules.DataSource = dv
        GridViewSchedules.AutoGenerateColumns = False
        GridViewSchedules.DataBind()
        If (isEmpty) Then GridViewSchedules.Rows(0).Visible = False
    End Sub

    Private Sub PopulateReQueueReasonGrid()
        Dim isEmpty As Boolean = False
        GridViewReQueueReasons.Columns(GRID_COL_CODE_IDX).SortExpression = WorkQueueItemStatusReason.COL_REASON
        GridViewReQueueReasons.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = WorkQueueItemStatusReason.COL_DESCRIPTION

        Dim dv As IOrderedEnumerable(Of WorkQueueItemStatusReason)
        dv = State.MyBO.ReQueueReasons.OrderBy(State.ReQueueReasonSortExpression, State.ReQueueReasonSortDirection)

        If (dv.Count() > 0) Then
            GridViewReQueueReasons.PageSize = dv.Count()
            If (State.MyReQueueReasonChildBO IsNot Nothing) Then
                GridViewReQueueReasons.EditIndex = GetSelectedRowIndex(Of WorkQueueItemStatusReason)(dv, State.MyReQueueReasonChildBO.ItemStatusReason.Id, Function(item) item.ItemStatusReason.Id)
            Else
                GridViewReQueueReasons.EditIndex = NO_ITEM_SELECTED_INDEX
            End If
        Else
            Dim emptyArray(0) As WorkQueueItemStatusReason
            emptyArray(0) = State.MyBO.CreateReason()
            dv = emptyArray.OrderBy(State.ReQueueReasonSortExpression, State.ReQueueReasonSortDirection)
            isEmpty = True
        End If

        GridViewReQueueReasons.DataSource = dv
        GridViewReQueueReasons.AutoGenerateColumns = False
        GridViewReQueueReasons.DataBind()
        If (isEmpty) Then GridViewReQueueReasons.Rows(0).Visible = False
    End Sub

    Private Sub PopulateReDirectReasonGrid()
        Dim isEmpty As Boolean = False
        GridViewReDirectReasons.Columns(GRID_COL_CODE_IDX).SortExpression = WorkQueueItemStatusReason.COL_REASON
        GridViewReDirectReasons.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = WorkQueueItemStatusReason.COL_DESCRIPTION

        Dim dv As IOrderedEnumerable(Of WorkQueueItemStatusReason)
        dv = State.MyBO.ReDirectReasons.OrderBy(State.ReDirectReasonSortExpression, State.ReDirectReasonSortDirection)

        If (dv.Count() > 0) Then
            GridViewReDirectReasons.PageSize = dv.Count()
            If (State.MyReDirectReasonChildBO IsNot Nothing) Then
                GridViewReDirectReasons.EditIndex = GetSelectedRowIndex(Of WorkQueueItemStatusReason)(dv, State.MyReDirectReasonChildBO.ItemStatusReason.Id, Function(item) item.ItemStatusReason.Id)
            Else
                GridViewReDirectReasons.EditIndex = NO_ITEM_SELECTED_INDEX
            End If
        Else
            Dim emptyArray(0) As WorkQueueItemStatusReason
            emptyArray(0) = State.MyBO.CreateReason()
            dv = emptyArray.OrderBy(State.ReDirectReasonSortExpression, State.ReDirectReasonSortDirection)
            isEmpty = True
        End If

        GridViewReDirectReasons.DataSource = dv
        GridViewReDirectReasons.AutoGenerateColumns = False
        GridViewReDirectReasons.DataBind()
        If (isEmpty) Then GridViewReDirectReasons.Rows(0).Visible = False
    End Sub


#End Region

#Region "Common Grid Events/Methods"
    Private Function GetSelectedRowIndex(Of T)(dv As IOrderedEnumerable(Of T), selectedGuid As Guid, idFunction As Func(Of T, Guid)) As Integer
        Dim selectedRowIndex As Integer = NO_ITEM_SELECTED_INDEX
        If (Not (selectedGuid.Equals(Guid.Empty))) Then
            'Jump to the Right Page
            Dim i As Integer
            For i = 0 To dv.Count - 1
                Dim obj As T = dv(i)
                If (idFunction(obj).Equals(selectedGuid)) Then
                    selectedRowIndex = i
                    Return (selectedRowIndex)
                End If
            Next
        End If
        Return (selectedRowIndex)
    End Function

    Private Sub GridViewReasons_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridViewReDirectReasons.RowCreated, GridViewReQueueReasons.RowCreated, GridViewSchedules.RowCreated
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "ReQueue / ReDirect Reason Grid"
    Public Sub GridViewReasons_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewReDirectReasons.RowDataBound, GridViewReQueueReasons.RowDataBound
        Try
            Dim itemType As DataControlRowType = e.Row.RowType
            Dim reasonsDV As DataView
            Dim moLabel As Label
            Dim moImageButton As ImageButton
            Dim moCodeDropDown As DropDownList
            Dim moDescriptionDropDown As DropDownList
            Dim wqisr As WorkQueueItemStatusReason
            If itemType = DataControlRowType.DataRow Then
                wqisr = CType(e.Row.DataItem, WorkQueueItemStatusReason)
                If ((e.Row.RowState And DataControlRowState.Edit) = DataControlRowState.Edit) Then
                    moCodeDropDown = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_DROPDOWN), DropDownList)
                    moCodeDropDown.Visible = True
                    moDescriptionDropDown = CType(e.Row.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_COL_DESCRIPTION_DROPDOWN), DropDownList)
                    moDescriptionDropDown.Visible = True

                    'reasonsDV = LookupListNew.DropdownLookupList(LookupListNew.LK_REASON_CODE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
                    'reasonsDV.Sort = LookupListNew.COL_CODE_NAME
                    'ElitaPlusPage.BindListControlToDataView(moCodeDropDown, reasonsDV, LookupListNew.COL_CODE_NAME, LookupListNew.COL_ID_NAME, True)

                    Dim ReasonCode As DataElements.ListItem() =
                        CommonConfigManager.Current.ListManager.GetList(listCode:="REASON_CODE", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                    moCodeDropDown.Populate(ReasonCode.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True,
                                    .TextFunc = AddressOf .GetCode,
                                    .SortFunc = AddressOf .GetCode
                                })

                    'reasonsDV.Sort = LookupListNew.COL_DESCRIPTION_NAME
                    'ElitaPlusPage.BindListControlToDataView(moDescriptionDropDown, reasonsDV, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME, True)

                    moDescriptionDropDown.Populate(ReasonCode.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                    If (wqisr.Reason <> String.Empty) Then
                        SetSelectedItemByText(moCodeDropDown, wqisr.Reason)
                    End If
                    If (wqisr.Description <> String.Empty) Then
                        SetSelectedItemByText(moDescriptionDropDown, wqisr.Description)
                    End If

                    moCodeDropDown.Attributes.Add("onchange", String.Format("ToggleSelection('{0}', '{1}', '{2}', '{3}')", moCodeDropDown.ClientID, moDescriptionDropDown.ClientID, "D", String.Empty))
                    moDescriptionDropDown.Attributes.Add("onchange", String.Format("ToggleSelection('{0}', '{1}', '{2}', '{3}')", moCodeDropDown.ClientID, moDescriptionDropDown.ClientID, "C", String.Empty))
                Else
                    moLabel = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_LABEL), Label)
                    moLabel.Text = wqisr.ItemStatusReason.Reason
                    moLabel = CType(e.Row.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_COL_DESCRIPTION_LABEL), Label)
                    moLabel.Text = wqisr.Description

                    moImageButton = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_DELETE_IMAGE_BUTTON), ImageButton)
                    If (State.IsReadOnly) Then
                        ControlMgr.SetVisibleControl(Me, moImageButton, False)
                    Else
                        moImageButton.Attributes.Add("onclick", String.Format("ShowDeleteConfirmation('{0}', '{1}${2}'); return false;", (DirectCast(sender, GridView)).UniqueID, DELETE_COMMAND_NAME, wqisr.Id.ToString()))
                        moImageButton.Attributes.Add("onclick1", ClientScript.GetPostBackEventReference(DirectCast(sender, GridView), String.Format("{0}${1}", DELETE_COMMAND_NAME, wqisr.Id.ToString())))
                    End If
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewReQueueReasons_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewReQueueReasons.RowCommand
        Try
            Select Case e.CommandName
                Case SAVE_COMMAND_NAME
                    PopulateReQueueReasonBOsFormFrom()
                    State.MyReQueueReasonChildBO.Validate()
                    State.IsReQueueReasonAdding = False
                    State.IsReQueueReasonEditing = False
                    State.MyReQueueReasonChildBO = Nothing
                Case CANCEL_COMMAND_NAME
                    If (State.MyReQueueReasonChildBO.IsNew) Then
                        If (State.IsReQueueReasonAdding) Then State.MyReQueueReasonChildBO.Delete()
                    End If
                    State.IsReQueueReasonAdding = False
                    State.IsReQueueReasonEditing = False
                    State.MyReQueueReasonChildBO = Nothing
                Case DELETE_COMMAND_NAME
                    State.MyReQueueReasonChildBO = (From wqisr In State.MyBO.ReQueueReasons Where wqisr.ItemStatusReason.Id = New Guid(e.CommandArgument.ToString()) Select wqisr).First()
                    State.MyReQueueReasonChildBO.Delete()
                    State.MyReQueueReasonChildBO = Nothing
                Case Else
                    'TODO : Throw
            End Select
            EnableDisableFields()
            PopulateReQueueReasonGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewReDirectReasons_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewReDirectReasons.RowCommand
        Try
            Select Case e.CommandName
                Case SAVE_COMMAND_NAME
                    PopulateReDirectReasonBOsFormFrom()
                    State.MyReDirectReasonChildBO.Validate()
                    State.IsReDirectReasonAdding = False
                    State.IsReDirectReasonEditing = False
                    State.MyReDirectReasonChildBO = Nothing
                Case CANCEL_COMMAND_NAME
                    If (State.MyReDirectReasonChildBO.IsNew) Then
                        If (State.IsReDirectReasonAdding) Then State.MyReDirectReasonChildBO.Delete()
                    End If
                    State.IsReDirectReasonAdding = False
                    State.IsReDirectReasonEditing = False
                    State.MyReDirectReasonChildBO = Nothing
                Case DELETE_COMMAND_NAME
                    State.MyReDirectReasonChildBO = (From wqisr In State.MyBO.ReDirectReasons Where wqisr.ItemStatusReason.Id = New Guid(e.CommandArgument.ToString()) Select wqisr).First()
                    State.MyReDirectReasonChildBO.Delete()
                    State.MyReDirectReasonChildBO = Nothing
                Case Else
                    'TODO : Throw
            End Select
            EnableDisableFields()
            PopulateReDirectReasonGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Schedule Grid"
    Private Sub GridViewSchedules_DataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewSchedules.RowDataBound
        Try
            Dim itemType As DataControlRowType = e.Row.RowType
            Dim moLabel As Label
            Dim moCodeDropDown As DropDownList
            Dim moDescriptionDropDown As DropDownList
            Dim moTextBox As TextBox
            Dim moImageButton As ImageButton
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim isNewRow As Boolean
            'Dim scheduleDV As Schedule.ScheduleSearchDV

            If itemType = DataControlRowType.DataRow Then
                If ((e.Row.RowState And DataControlRowState.Edit) = DataControlRowState.Edit) Then
                    isNewRow = DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_IS_NEW), Boolean)
                    If (isNewRow) Then
                        moCodeDropDown = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_DROPDOWN), DropDownList)
                        moCodeDropDown.Visible = True
                        moDescriptionDropDown = CType(e.Row.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_COL_DESCRIPTION_DROPDOWN), DropDownList)
                        moDescriptionDropDown.Visible = True

                        'scheduleDV = Schedule.GetSchedulesList("*", "*")
                        'scheduleDV.Sort = Schedule.ScheduleSearchDV.COL_NAME_CODE
                        'ElitaPlusPage.BindListControlToDataView(moCodeDropDown, scheduleDV, Schedule.ScheduleSearchDV.COL_NAME_CODE, Schedule.ScheduleSearchDV.COL_NAME_SCHEDULE_ID, True)

                        Dim ScheduleList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="GetSchedule")

                        moCodeDropDown.Populate(ScheduleList.ToArray(),
                                                New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True,
                                                    .TextFunc = AddressOf .GetCode,
                                                    .SortFunc = AddressOf .GetCode
                                                })

                        'scheduleDV.Sort = Schedule.ScheduleSearchDV.COL_NAME_DESCRIPTION
                        'ElitaPlusPage.BindListControlToDataView(moDescriptionDropDown, scheduleDV, Schedule.ScheduleSearchDV.COL_NAME_DESCRIPTION, Schedule.ScheduleSearchDV.COL_NAME_SCHEDULE_ID, True)

                        moDescriptionDropDown.Populate(ScheduleList.ToArray(),
                                                        New PopulateOptions() With
                                                        {
                                                            .AddBlankItem = True
                                                        })

                        moCodeDropDown.Attributes.Add("onchange", String.Format("ToggleSelection('{0}', '{1}', '{2}', '{3}')", moCodeDropDown.ClientID, moDescriptionDropDown.ClientID, "D", String.Empty))
                        moDescriptionDropDown.Attributes.Add("onchange", String.Format("ToggleSelection('{0}', '{1}', '{2}', '{3}')", moCodeDropDown.ClientID, moDescriptionDropDown.ClientID, "C", String.Empty))

                        If (dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_CODE).ToString() <> String.Empty) Then
                            SetSelectedItemByText(moCodeDropDown, dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_CODE).ToString())
                        End If
                        If (dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_DESCRIPTION).ToString() <> String.Empty) Then
                            SetSelectedItemByText(moDescriptionDropDown, dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_DESCRIPTION).ToString())
                        End If


                        moTextBox = CType(e.Row.Cells(GRID_COL_EFFECTIVE_IDX).FindControl(GRID_COL_EFFECTIVE_TEXT), TextBox)
                        moTextBox.Visible = True
                        moImageButton = CType(e.Row.Cells(GRID_COL_EFFECTIVE_IDX).FindControl(GRID_COL_EFFECTIVE_IMAGE), ImageButton)
                        moImageButton.Visible = True
                        moTextBox.Text = GetLongDateFormattedString(DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE), Date))
                        AddCalendarwithTime_New(moImageButton, moTextBox)
                    Else
                        moLabel = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_LABEL), Label)
                        moLabel.Visible = True
                        moLabel.Text = dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_CODE).ToString()

                        moLabel = CType(e.Row.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_COL_DESCRIPTION_LABEL), Label)
                        moLabel.Visible = True
                        moLabel.Text = dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_DESCRIPTION).ToString()

                        moLabel = CType(e.Row.Cells(GRID_COL_EFFECTIVE_IDX).FindControl(GRID_COL_EFFECTIVE_LABEL), Label)
                        moLabel.Visible = True
                        moLabel.Text = GetLongDateFormattedString(DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE), Date))
                    End If

                    Dim expirationDate As DateTime = WorkQueue.DEFAULT_EXPIRATION_DATE
                    If (dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EXPIRATION) IsNot DBNull.Value) Then
                        expirationDate = DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EXPIRATION), DateTime)
                    End If
                    If (isNewRow OrElse expirationDate > DateTime.Now) Then
                        moTextBox = CType(e.Row.Cells(GRID_COL_EXPIRATION_IDX).FindControl(GRID_COL_EXPIRATION_TEXT), TextBox)
                        moTextBox.Visible = True
                        moImageButton = CType(e.Row.Cells(GRID_COL_EXPIRATION_IDX).FindControl(GRID_COL_EXPIRATION_IMAGE), ImageButton)
                        moImageButton.Visible = True
                        moTextBox.Text = GetLongDateFormattedString(expirationDate)
                        AddCalendarwithTime_New(moImageButton, moTextBox)
                    Else
                        moLabel = CType(e.Row.Cells(GRID_COL_EXPIRATION_IDX).FindControl(GRID_COL_EXPIRATION_LABEL), Label)
                        moLabel.Visible = True
                        moLabel.Text = GetLongDateFormattedString(expirationDate)
                    End If
                Else
                    moLabel = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_CODE).ToString()

                    moLabel = CType(e.Row.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_COL_DESCRIPTION_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_DESCRIPTION).ToString()

                    moLabel = CType(e.Row.Cells(GRID_COL_EFFECTIVE_IDX).FindControl(GRID_COL_EFFECTIVE_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = GetLongDateFormattedString(DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE), Date))

                    Dim expirationDate As DateTime = WorkQueue.DEFAULT_EXPIRATION_DATE
                    If (dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EXPIRATION) IsNot DBNull.Value) Then
                        expirationDate = DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EXPIRATION), DateTime)
                    End If
                    moLabel = CType(e.Row.Cells(GRID_COL_EXPIRATION_IDX).FindControl(GRID_COL_EXPIRATION_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = GetLongDateFormattedString(expirationDate)

                    If (State.IsScheduleEditing) Then
                        moImageButton = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_EDIT_IMAGE_BUTTON), ImageButton)
                        moImageButton.Visible = False
                        moImageButton = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_DELETE_IMAGE_BUTTON), ImageButton)
                        moImageButton.Visible = False
                    Else
                        Dim scheduleId As Guid
                        scheduleId = New Guid(CType(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_ENTITY_SCHEDULE_ID), Byte()))
                        moImageButton = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_EDIT_IMAGE_BUTTON), ImageButton)
                        If (State.IsReadOnly) Then
                            ControlMgr.SetVisibleControl(Me, moImageButton, False)
                        Else
                            moImageButton.CommandArgument = scheduleId.ToString()
                        End If
                        moImageButton = CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_DELETE_IMAGE_BUTTON), ImageButton)
                        If (State.IsReadOnly) Then
                            ControlMgr.SetVisibleControl(Me, moImageButton, False)
                        Else
                            moImageButton.Attributes.Add("onclick", String.Format("ShowDeleteConfirmation('{0}', '{1}${2}'); return false;", (DirectCast(sender, GridView)).UniqueID, DELETE_COMMAND_NAME, scheduleId.ToString()))
                            moImageButton.Attributes.Add("onclick1", ClientScript.GetPostBackEventReference(DirectCast(sender, GridView), String.Format("{0}${1}", DELETE_COMMAND_NAME, scheduleId.ToString())))
                        End If
                        'Check if the Schedule’s effective date has been passed, if yes then don’t show the delete Icon to the user 
                        If (dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE) IsNot DBNull.Value) Then
                            Dim EffectiveDate As DateTime = DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE), DateTime)
                            If (EffectiveDate < DateTime.UtcNow) Then
                                ControlMgr.SetVisibleControl(Me, moImageButton, False)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewSchedules_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewSchedules.RowCommand
        Try
            Select Case e.CommandName
                Case EDIT_COMMAND_NAME
                    State.EntityScheduleChildId = New Guid(e.CommandArgument.ToString())
                    BeginScheduleChildEdit()
                Case SAVE_COMMAND_NAME
                    PopulateScheduleBOsFormFrom()
                    ' Check if adding new Schedule
                    If (State.MyScheduleChildBO.IsNew) Then
                        ' Check if Current Schedule Effective Date is greater than any Effective Date
                        For Each drv As DataRowView In State.MyBO.GetScheduleSelectionView
                            Dim es As EntitySchedule = State.MyBO.GetScheduleChild(New Guid(CType(drv(EntitySchedule.ScheduleSelectionView.COL_NAME_ENTITY_SCHEDULE_ID), Byte())))
                            If (Not es.Id.Equals(State.MyScheduleChildBO.Id)) Then
                                If (State.MyScheduleChildBO.Effective.Value > es.Effective.Value AndAlso State.MyScheduleChildBO.Effective.Value < es.Expiration.Value) Then
                                    ' Display Message
                                    DisplayMessage(Message.MSG_EXPIRE_PREVIOUS_WQ_SCHEDULE, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Expire
                                    Return
                                End If
                            End If
                        Next
                    End If
                    State.MyScheduleChildBO.Validate()
                    State.MyScheduleChildBO.EndEdit()
                    State.IsScheduleAdding = False
                    State.IsScheduleEditing = False
                    State.MyScheduleChildBO = Nothing
                Case CANCEL_COMMAND_NAME
                    If (State.MyScheduleChildBO.IsNew) Then
                        If (State.IsScheduleAdding) Then State.MyScheduleChildBO.Delete()
                    Else
                        State.MyScheduleChildBO.cancelEdit()
                    End If
                    State.IsScheduleAdding = False
                    State.IsScheduleEditing = False
                    State.MyScheduleChildBO = Nothing
                Case DELETE_COMMAND_NAME
                    State.MyScheduleChildBO = State.MyBO.GetScheduleChild(New Guid(e.CommandArgument.ToString()))
                    State.MyScheduleChildBO.Delete()
                    State.MyScheduleChildBO = Nothing
                Case Else
                    'TODO : Throw
            End Select
            EnableDisableFields()
            PopulateScheduleGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Child Objects Command Buttons Event Handlers"
    Private Sub btnAddNewReDirectReason_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAddNewReDirectReason_WRITE.Click
        Try
            State.IsReDirectReasonAdding = True
            State.ReDirectReasonSelectedChildId = Guid.Empty
            BeginReDirectReasonChildEdit()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateReDirectReasonBOsFormFrom()
        Dim moCodeDropDown As DropDownList
        With GridViewReDirectReasons.Rows(GridViewReDirectReasons.EditIndex)
            If (State.MyReDirectReasonChildBO.IsNew) Then
                moCodeDropDown = CType(.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_DROPDOWN), DropDownList)
                PopulateBOProperty(State.MyReDirectReasonChildBO.ItemStatusReason, "Reason", GetSelectedDescription(moCodeDropDown))
            End If
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Sub BeginReDirectReasonChildEdit()
        State.IsReDirectReasonEditing = True
        EnableDisableFields()
        With State
            If .ReDirectReasonSelectedChildId.Equals(Guid.Empty) Then
                .MyReDirectReasonChildBO = State.MyBO.AddReDirectReason()
                .ReDirectReasonSelectedChildId = .MyReDirectReasonChildBO.Id
            Else
                .MyReDirectReasonChildBO = (From wqisr In State.MyBO.ReDirectReasons Where wqisr.ItemStatusReason.Id = .ReDirectReasonSelectedChildId Select wqisr).First()
            End If
        End With
        PopulateReDirectReasonGrid()
    End Sub

    Private Sub btnAddNewReQueueReason_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAddNewReQueueReason_WRITE.Click
        Try
            State.IsReQueueReasonAdding = True
            State.ReQueueReasonSelectedChildId = Guid.Empty
            BeginReQueueReasonChildEdit()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateReQueueReasonBOsFormFrom()
        Dim moCodeDropDown As DropDownList
        With GridViewReQueueReasons.Rows(GridViewReQueueReasons.EditIndex)
            If (State.MyReQueueReasonChildBO.IsNew) Then
                moCodeDropDown = CType(.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_DROPDOWN), DropDownList)
                PopulateBOProperty(State.MyReQueueReasonChildBO.ItemStatusReason, "Reason", GetSelectedDescription(moCodeDropDown))
            End If
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Sub BeginReQueueReasonChildEdit()
        State.IsReQueueReasonEditing = True
        EnableDisableFields()
        With State
            If .ReQueueReasonSelectedChildId.Equals(Guid.Empty) Then
                .MyReQueueReasonChildBO = State.MyBO.AddReQueueReason()
                .ReQueueReasonSelectedChildId = .MyReQueueReasonChildBO.Id
            Else
                .MyReQueueReasonChildBO = (From wqisr In State.MyBO.ReQueueReasons Where wqisr.ItemStatusReason.Id = .ReQueueReasonSelectedChildId Select wqisr).First()
            End If
        End With
        PopulateReQueueReasonGrid()
    End Sub

    Private Sub btnAddNewSchedule_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAddNewSchedule_WRITE.Click
        Try
            State.IsScheduleAdding = True
            State.EntityScheduleChildId = Guid.Empty
            BeginScheduleChildEdit()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateScheduleBOsFormFrom()
        Dim moCodeDropDown As DropDownList
        Dim moDescriptionDropDown As DropDownList
        Dim moTextBox As TextBox
        With GridViewSchedules.Rows(GridViewSchedules.EditIndex)
            If (State.MyScheduleChildBO.IsNew) Then
                moCodeDropDown = CType(.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_DROPDOWN), DropDownList)
                moDescriptionDropDown = CType(.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_COL_DESCRIPTION_DROPDOWN), DropDownList)
                moTextBox = CType(.Cells(GRID_COL_EFFECTIVE_IDX).FindControl(GRID_COL_EFFECTIVE_TEXT), TextBox)

                PopulateBOProperty(State.MyScheduleChildBO, "ScheduleId", moCodeDropDown)
                PopulateBOProperty(State.MyScheduleChildBO, "ScheduleCode", GetSelectedDescription(moCodeDropDown))
                If (State.MyScheduleChildBO.ScheduleId.Equals(GetSelectedItem(moDescriptionDropDown))) Then
                    PopulateBOProperty(State.MyScheduleChildBO, "ScheduleDescription", GetSelectedDescription(moDescriptionDropDown))
                Else
                    PopulateBOProperty(State.MyScheduleChildBO, "ScheduleDescription", String.Empty)
                End If
                PopulateBOProperty(State.MyScheduleChildBO, "Effective", moTextBox)
            End If
            moTextBox = CType(.Cells(GRID_COL_EXPIRATION_IDX).FindControl(GRID_COL_EXPIRATION_TEXT), TextBox)
            PopulateBOProperty(State.MyScheduleChildBO, "Expiration", moTextBox)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub BeginScheduleChildEdit()
        State.IsScheduleEditing = True
        EnableDisableFields()
        With State
            If .EntityScheduleChildId.Equals(Guid.Empty) Then
                .MyScheduleChildBO = .MyBO.GetNewScheduleChild
                .EntityScheduleChildId = .MyScheduleChildBO.Id
            Else
                .MyScheduleChildBO = .MyBO.GetScheduleChild(.EntityScheduleChildId)
            End If
            .MyScheduleChildBO.BeginEdit()
        End With
        PopulateScheduleGrid()
    End Sub
#End Region

#Region "Tab related"
    Private Sub EnableDisableTabs(blnFlag As Boolean)
        listDisabledTabs.Clear()
        Dim cnt As Integer
        If blnFlag = False Then 'disable tabs
            For cnt = 0 To TAB_TOTAL_COUNT - 1
                If (cnt <> SelectedTabIndex) Then ' Enable - Disable other tabs other than the selected one
                    listDisabledTabs.Add(cnt)
                End If
            Next
        End If
    End Sub

#End Region

End Class

