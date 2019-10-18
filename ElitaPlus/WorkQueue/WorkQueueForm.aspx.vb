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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingParameters As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.WorkQueueReturnObject = CType(CallingParameters, WorkQueueListForm.WorkQueueReturnType)
                If (Not Me.State.WorkQueueReturnObject.WorkQueueId.Equals(Guid.Empty)) Then
                    Me.State.MyBO = New WorkQueue(Me.State.WorkQueueReturnObject.WorkQueueId)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear()
            If (Not Me.IsPostBack) Then
                ' Popup Configuration for Delete Message Box
                lblCancelMessage.Text = TranslationBase.TranslateLabelOrMessage("MSG_CONFIRM_PROMPT")
                btnModalCancelYes.Attributes.Add("onclick", "YesButtonClick();")

                ' Date Calendars
                Me.AddCalendarwithTime_New(btnEffectiveDate, moEffectiveDate)
                Me.AddCalendarwithTime_New(btnExpirationDate, moExpirationDate)

                If (Me.State.MyBO Is Nothing) Then
                    Me.State.MyBO = New WorkQueue()
                End If

                Dim extendedUser As Auth.ExtendedUser
                Me.State.UserHasPermissions = True
                extendedUser = ElitaPlusIdentity.Current.ActiveUser.ExtendedUser
                If (Not extendedUser.HasPermission(New Auth.Permission With {.ResourceType = ServiceHelper.RESTYP_WORKQUEUESYSTEM, .Resource = ServiceHelper.RES_WORKQUEUESYSTEM, .Action = ServiceHelper.PA_WQS_CREATE_QUEUE})) Then
                    Me.State.UserHasPermissions = False
                End If
                If (Me.State.UserHasPermissions AndAlso Not extendedUser.HasPermission(New Auth.Permission With {.ResourceType = ServiceHelper.RESTYP_WORKQUEUESYSTEM, .Resource = ServiceHelper.RES_WORKQUEUESYSTEM, .Action = ServiceHelper.PA_WQS_MANAGE_ITEM_STATUS})) Then
                    Me.State.UserHasPermissions = False
                End If

                ' Translate Grid Headers
                TranslateGridHeader(Me.GridViewReDirectReasons)
                TranslateGridHeader(Me.GridViewSchedules)
                TranslateGridHeader(Me.GridViewReQueueReasons)

                ' Populate Bread Crum
                UpdateBreadCrum()

                ' Populate Drop Downs
                PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Else
                If (Me.State.MyBO.IsNew) Then
                    Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "TimeZoneCode", GetSelectedValue(Me.moTimeZoneDropDown))
                    Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "Effective", Me.moEffectiveDate)
                    Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "Expiration", Me.moExpirationDate)
                End If
                BindBoPropertiesToGridHeaders()
                SelectedTabIndex = hdnSelectedTab.Value
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
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

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            Me.State.WorkQueueReturnObject.WorkQueueId = Me.State.MyBO.WorkQueue.Id
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.WorkQueueReturnObject, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.State.MyBO = New WorkQueue(Me.State.MyBO.Id)
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New WorkQueue(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBOId.Equals(Guid.Empty) Then
                'It was a new with copy
                Me.State.MyBO = New WorkQueue(Me.State.ScreenSnapShotBOId)
            Else
                Me.State.MyBO = New WorkQueue()
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DoDelete()
        Me.State.MyBO.Delete()
        Me.State.MyBO.Save()
        Me.State.HasDataChanged = True
        Me.State.WorkQueueReturnObject.WorkQueueId = Me.State.MyBO.WorkQueue.Id
        Me.ReturnToCallingPage(New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(ElitaPlusPage.DetailPageCommand.Delete, Me.State.WorkQueueReturnObject, Me.State.HasDataChanged))
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Delete _
                AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Expire Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.WorkQueueReturnObject, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.WorkQueueReturnObject, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DoDelete()
                Case ElitaPlusPage.DetailPageCommand.Expire
                    If (Me.State.IsScheduleAdding) Then
                        Try
                            Me.State.MyScheduleChildBO.Validate()
                        Catch ex As BOValidationException
                            Dim validationError As ValidationError() = ex.ValidationErrorList()
                            If (validationError.Length = 1 AndAlso validationError(0).Message = Assurant.ElitaPlus.Common.ErrorCodes.OVERLAPPING_SCHEDULE_ERR) Then
                                For Each drv As DataRowView In Me.State.MyBO.GetScheduleSelectionView
                                    Dim es As EntitySchedule = Me.State.MyBO.GetScheduleChild(New Guid(CType(drv(EntitySchedule.ScheduleSelectionView.COL_NAME_ENTITY_SCHEDULE_ID), Byte())))
                                    If (Not es.Id.Equals(Me.State.MyScheduleChildBO.Id)) Then
                                        If (Me.State.MyScheduleChildBO.Effective.Value > es.Effective.Value AndAlso Me.State.MyScheduleChildBO.Effective.Value < es.Expiration.Value) Then
                                            es.BeginEdit()
                                            es.Expiration = Me.State.MyScheduleChildBO.Effective.Value.AddSeconds(-1)
                                            es.EndEdit()
                                            Me.State.MyScheduleChildBO.Validate()
                                            Exit For
                                        End If
                                    End If
                                Next
                            Else
                                Throw
                            End If
                        End Try
                        Me.State.MyScheduleChildBO.EndEdit()
                        Me.State.IsScheduleAdding = False
                        Me.State.IsScheduleEditing = False
                        Me.State.MyScheduleChildBO = Nothing
                        Me.EnableDisableFields()
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
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.WorkQueueReturnObject, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)

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
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub UpdateBreadCrum()
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGESUBTAB) & ElitaBase.Sperator
        Me.MasterPage.BreadCrum = Me.MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
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

        Me.BindListControlToArray(moCompanyDropDown, companies)
        Me.BindListControlToArray(moAdminRole, roles)
        Me.BindListControlToArray(moActionDropDown, actions)
        Me.BindListControlToArray(moWorkQueueTypeDropDown, workQueueTypes, , , Guid.Empty.ToString())
        Me.BindListControlToArray(moLockableDataType, workQueueItemDataTypes, , , Guid.Empty.ToString())
        Me.BindListControlToArray(moTimeZoneDropDown, timeZones)
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "Name", Me.moWorkQueueNameLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "CompanyCode", Me.moCompanyLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "TimeZoneCode", Me.moTimeZoneLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "Effective", Me.moEffectiveDateLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "ActiveOn", Me.moEffectiveDateLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "Expiration", Me.moExpirationDateLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "StartItemDelayMinutes", Me.moStartItemDelayMinutesLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "TimeToCompleteMinutes", Me.moTimeToCompleteMinutesLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "MaxRequeue", Me.moMaximumReQueueLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "WorkQueueTypeId", Me.moWorkQueueTypeLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "AdminRole", Me.moAdminRoleLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "ActionCode", Me.moActionLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "TransformationFile", Me.moTransformationFileLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "LockableDataTypeId", Me.moLockableDataTypeLabel)
        'DEF-3035
        Me.BindBOPropertyToLabel(Me.State.MyBO.WorkQueue, "RequeueItemDelayMinutes", Me.moReQueueDelayLabel)
        'DEF-3035 End
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateFormFromBOs()
        moMessageController.Clear()
        With Me.State.MyBO
            Me.State.IsReadOnly = False
            If (.WorkQueue.InActiveOn.HasValue AndAlso .WorkQueue.InActiveOn.Value < DateTime.UtcNow) Then Me.State.IsReadOnly = True

            If (Not Me.State.UserHasPermissions) Then
                moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_NO_CREATEQUEUE_MANAGEITEMSTATUSLIST_PERMISSION)
                Me.State.IsReadOnly = True
            End If
            If (Not .IsNew) Then
                If (Not ElitaPlusIdentity.Current.ActiveUser.isInRole(.WorkQueue.AdminRole)) Then
                    moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_NO_ADMIN_ROLE)
                    Me.State.IsReadOnly = True
                End If

                If (Not ElitaPlusIdentity.Current.ActiveUser.ExtendedUser.HasPermission(New Auth.Permission With {.ResourceType = ServiceHelper.RESTYP_WORKQUEUE, .Resource = Me.State.MyBO.WorkQueue.Name, .Action = ServiceHelper.PA_WQ_EDIT})) Then
                    moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_NO_EDIT_PERMISSION)
                    Me.State.IsReadOnly = True
                End If
            End If

            PopulateScheduleGrid()
            PopulateReQueueReasonGrid()
            PopulateReDirectReasonGrid()

            Me.PopulateControlFromBOProperty(Me.moWorkQueueName, .WorkQueue.Name)
            Me.SetSelectedItem(Me.moCompanyDropDown, .WorkQueue.CompanyCode)
            Me.SetSelectedItem(Me.moTimeZoneDropDown, .WorkQueue.TimeZoneCode)
            Me.PopulateControlFromBOProperty(Me.moEffectiveDate, .WorkQueue.Effective)
            Me.PopulateControlFromBOProperty(Me.moExpirationDate, .WorkQueue.Expiration)
            Me.PopulateControlFromBOProperty(Me.moStartItemDelayMinutes, .WorkQueue.StartItemDelayMinutes)
            Me.PopulateControlFromBOProperty(Me.moTimeToCompleteMinutes, .WorkQueue.TimeToCompleteMinutes)
            Me.PopulateControlFromBOProperty(Me.moMaximumReQueue, .WorkQueue.MaxRequeue)
            Me.PopulateControlFromBOProperty(Me.moWorkQueueTypeDropDown, .WorkQueue.WorkQueueTypeId)
            Me.SetSelectedItem(Me.moAdminRole, .WorkQueue.AdminRole)
            Me.SetSelectedItem(Me.moActionDropDown, .WorkQueue.ActionCode)
            Me.PopulateControlFromBOProperty(Me.moTransformationFile, .WorkQueue.TransformationFile)
            Me.PopulateControlFromBOProperty(Me.moLockableDataType, .WorkQueue.LockableDataTypeId)
            'DEF-3035
            Me.PopulateControlFromBOProperty(Me.moReQueueDelay, .WorkQueue.RequeueItemDelayMinutes)
            'End of DEF-3035

            Me.moCompanyText.Text = Me.GetSelectedDescription(moCompanyDropDown)
            Me.moActionText.Text = Me.GetSelectedDescription(moActionDropDown)
            Me.moWorkQueueTypeText.Text = Me.GetSelectedDescription(moWorkQueueTypeDropDown)
            Me.moTimeZoneText.Text = Me.GetSelectedDescription(moTimeZoneDropDown)

            Me.moWorkQueueName.ReadOnly = Not .IsNew
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
        Me.BindBOPropertyToGridHeader(Me.State.MyScheduleChildBO, "ScheduleId", Me.GridViewSchedules.Columns(Me.GRID_COL_CODE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyScheduleChildBO, "ScheduleCode", Me.GridViewSchedules.Columns(Me.GRID_COL_CODE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyScheduleChildBO, "ScheduleDescription", Me.GridViewSchedules.Columns(Me.GRID_COL_DESCRIPTION_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyScheduleChildBO, "Effective", Me.GridViewSchedules.Columns(Me.GRID_COL_EFFECTIVE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyScheduleChildBO, "Expiration", Me.GridViewSchedules.Columns(Me.GRID_COL_EXPIRATION_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyScheduleChildBO, "EntityEffective", Me.GridViewSchedules.Columns(Me.GRID_COL_EFFECTIVE_IDX))

        Me.BindBOPropertyToGridHeader(Me.State.MyReDirectReasonChildBO, "Reason", Me.GridViewReDirectReasons.Columns(Me.GRID_COL_CODE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyReDirectReasonChildBO, "Description", Me.GridViewReDirectReasons.Columns(Me.GRID_COL_DESCRIPTION_IDX))

        Me.BindBOPropertyToGridHeader(Me.State.MyReQueueReasonChildBO, "Reason", Me.GridViewReQueueReasons.Columns(Me.GRID_COL_CODE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyReQueueReasonChildBO, "Description", Me.GridViewReQueueReasons.Columns(Me.GRID_COL_DESCRIPTION_IDX))

        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Protected Sub EnableDisableFields()
        If Me.State.IsScheduleEditing OrElse Me.State.IsReDirectReasonEditing OrElse Me.State.IsReQueueReasonEditing Then
            EnableDisableParentControls(False)
        Else
            EnableDisableParentControls(True)
        End If
    End Sub

    Sub EnableDisableParentControls(ByVal enableToggle As Boolean)
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

        Me.moWorkQueueName.ReadOnly = Not (Me.State.MyBO.IsNew)
        ControlMgr.SetEnableControl(Me, moCompanyDropDown, Me.State.MyBO.IsNew)
        ControlMgr.SetEnableControl(Me, moTimeZoneDropDown, Me.State.MyBO.IsNew)
        Me.moEffectiveDate.ReadOnly = Not (Me.State.MyBO.IsNew)
        ControlMgr.SetVisibleControl(Me, btnEffectiveDate, Me.State.MyBO.IsNew)

        If (enableToggle) Then
            'Enabled by Default
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)

            'Now disable depebding on the object state
            If Me.State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If
            If (Me.State.IsReadOnly) Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
                ControlMgr.SetEnableControl(Me, btnAddNewReDirectReason_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnAddNewReQueueReason_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnAddNewSchedule_WRITE, False)
            End If
            If (Not Me.State.UserHasPermissions) Then
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If
        End If
    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBOId = Guid.Empty 'Reset the backup copy
        Me.State.MyBO = New WorkQueue()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New WorkQueue
        newObj.Copy(Me.State.MyBO)

        'create the backup copy
        Me.State.ScreenSnapShotBOId = Me.State.MyBO.Id

        Me.State.MyBO = newObj
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "Name", Me.moWorkQueueName)
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "CompanyCode", GetSelectedValue(Me.moCompanyDropDown))
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "TimeZoneCode", GetSelectedValue(Me.moTimeZoneDropDown))
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "Effective", Me.moEffectiveDate)
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "Expiration", Me.moExpirationDate)
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "StartItemDelayMinutes", Me.moStartItemDelayMinutes)
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "TimeToCompleteMinutes", Me.moTimeToCompleteMinutes)
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "MaxRequeue", Me.moMaximumReQueue)
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "WorkQueueTypeId", Me.moWorkQueueTypeDropDown)
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "AdminRole", GetSelectedValue(Me.moAdminRole))
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "ActionCode", GetSelectedValue(Me.moActionDropDown))
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "TransformationFile", Me.moTransformationFile)
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "LockableDataTypeId", Me.moLockableDataType)
            'DEF-3035
            Me.PopulateBOProperty(Me.State.MyBO.WorkQueue, "RequeueItemDelayMinutes", Me.moReQueueDelay)
            'DEF-3035 End
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub PopulateScheduleGrid()
        Dim isEmpty As Boolean
        Dim dv As EntitySchedule.ScheduleSelectionView = Me.State.MyBO.GetScheduleSelectionView()
        dv.Sort = Me.State.ScheduleSortExpression

        If (dv.Count > 0) Then
            Me.GridViewSchedules.PageSize = dv.Count
        Else
            Dim dr As DataRow
            dr = dv.Table.NewRow()
            dr(EntitySchedule.ScheduleSelectionView.COL_NAME_IS_NEW) = True
            dr(EntitySchedule.ScheduleSelectionView.COL_NAME_ENTITY_SCHEDULE_ID) = Guid.NewGuid().ToByteArray()
            dr(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE) = DateTime.Now
            dv.Table.Rows.Add(dr)
            isEmpty = True
        End If

        SetPageAndSelectedIndexFromGuid(dv, Me.State.EntityScheduleChildId, Me.GridViewSchedules, 1, Me.State.IsScheduleEditing)

        Me.GridViewSchedules.DataSource = dv
        Me.GridViewSchedules.AutoGenerateColumns = False
        Me.GridViewSchedules.DataBind()
        If (isEmpty) Then Me.GridViewSchedules.Rows(0).Visible = False
    End Sub

    Private Sub PopulateReQueueReasonGrid()
        Dim isEmpty As Boolean = False
        Me.GridViewReQueueReasons.Columns(Me.GRID_COL_CODE_IDX).SortExpression = WorkQueueItemStatusReason.COL_REASON
        Me.GridViewReQueueReasons.Columns(Me.GRID_COL_DESCRIPTION_IDX).SortExpression = WorkQueueItemStatusReason.COL_DESCRIPTION

        Dim dv As IOrderedEnumerable(Of WorkQueueItemStatusReason)
        dv = Me.State.MyBO.ReQueueReasons.OrderBy(Me.State.ReQueueReasonSortExpression, Me.State.ReQueueReasonSortDirection)

        If (dv.Count() > 0) Then
            Me.GridViewReQueueReasons.PageSize = dv.Count()
            If (Not Me.State.MyReQueueReasonChildBO Is Nothing) Then
                Me.GridViewReQueueReasons.EditIndex = GetSelectedRowIndex(Of WorkQueueItemStatusReason)(dv, Me.State.MyReQueueReasonChildBO.ItemStatusReason.Id, Function(item) item.ItemStatusReason.Id)
            Else
                Me.GridViewReQueueReasons.EditIndex = NO_ITEM_SELECTED_INDEX
            End If
        Else
            Dim emptyArray(0) As WorkQueueItemStatusReason
            emptyArray(0) = Me.State.MyBO.CreateReason()
            dv = emptyArray.OrderBy(Me.State.ReQueueReasonSortExpression, Me.State.ReQueueReasonSortDirection)
            isEmpty = True
        End If

        Me.GridViewReQueueReasons.DataSource = dv
        Me.GridViewReQueueReasons.AutoGenerateColumns = False
        Me.GridViewReQueueReasons.DataBind()
        If (isEmpty) Then Me.GridViewReQueueReasons.Rows(0).Visible = False
    End Sub

    Private Sub PopulateReDirectReasonGrid()
        Dim isEmpty As Boolean = False
        Me.GridViewReDirectReasons.Columns(Me.GRID_COL_CODE_IDX).SortExpression = WorkQueueItemStatusReason.COL_REASON
        Me.GridViewReDirectReasons.Columns(Me.GRID_COL_DESCRIPTION_IDX).SortExpression = WorkQueueItemStatusReason.COL_DESCRIPTION

        Dim dv As IOrderedEnumerable(Of WorkQueueItemStatusReason)
        dv = Me.State.MyBO.ReDirectReasons.OrderBy(Me.State.ReDirectReasonSortExpression, Me.State.ReDirectReasonSortDirection)

        If (dv.Count() > 0) Then
            Me.GridViewReDirectReasons.PageSize = dv.Count()
            If (Not Me.State.MyReDirectReasonChildBO Is Nothing) Then
                Me.GridViewReDirectReasons.EditIndex = GetSelectedRowIndex(Of WorkQueueItemStatusReason)(dv, Me.State.MyReDirectReasonChildBO.ItemStatusReason.Id, Function(item) item.ItemStatusReason.Id)
            Else
                Me.GridViewReDirectReasons.EditIndex = NO_ITEM_SELECTED_INDEX
            End If
        Else
            Dim emptyArray(0) As WorkQueueItemStatusReason
            emptyArray(0) = Me.State.MyBO.CreateReason()
            dv = emptyArray.OrderBy(Me.State.ReDirectReasonSortExpression, Me.State.ReDirectReasonSortDirection)
            isEmpty = True
        End If

        Me.GridViewReDirectReasons.DataSource = dv
        Me.GridViewReDirectReasons.AutoGenerateColumns = False
        Me.GridViewReDirectReasons.DataBind()
        If (isEmpty) Then Me.GridViewReDirectReasons.Rows(0).Visible = False
    End Sub


#End Region

#Region "Common Grid Events/Methods"
    Private Function GetSelectedRowIndex(Of T)(ByVal dv As IOrderedEnumerable(Of T), ByVal selectedGuid As Guid, ByVal idFunction As Func(Of T, Guid)) As Integer
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

    Private Sub GridViewReasons_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewReDirectReasons.RowCreated, GridViewReQueueReasons.RowCreated, GridViewSchedules.RowCreated
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "ReQueue / ReDirect Reason Grid"
    Public Sub GridViewReasons_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewReDirectReasons.RowDataBound, GridViewReQueueReasons.RowDataBound
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
                    moCodeDropDown = CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_CODE_DROPDOWN), DropDownList)
                    moCodeDropDown.Visible = True
                    moDescriptionDropDown = CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(Me.GRID_COL_DESCRIPTION_DROPDOWN), DropDownList)
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
                        Me.SetSelectedItemByText(moCodeDropDown, wqisr.Reason)
                    End If
                    If (wqisr.Description <> String.Empty) Then
                        Me.SetSelectedItemByText(moDescriptionDropDown, wqisr.Description)
                    End If

                    moCodeDropDown.Attributes.Add("onchange", String.Format("ToggleSelection('{0}', '{1}', '{2}', '{3}')", moCodeDropDown.ClientID, moDescriptionDropDown.ClientID, "D", String.Empty))
                    moDescriptionDropDown.Attributes.Add("onchange", String.Format("ToggleSelection('{0}', '{1}', '{2}', '{3}')", moCodeDropDown.ClientID, moDescriptionDropDown.ClientID, "C", String.Empty))
                Else
                    moLabel = CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_CODE_LABEL), Label)
                    moLabel.Text = wqisr.ItemStatusReason.Reason
                    moLabel = CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(Me.GRID_COL_DESCRIPTION_LABEL), Label)
                    moLabel.Text = wqisr.Description

                    moImageButton = CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_DELETE_IMAGE_BUTTON), ImageButton)
                    If (Me.State.IsReadOnly) Then
                        ControlMgr.SetVisibleControl(Me, moImageButton, False)
                    Else
                        moImageButton.Attributes.Add("onclick", String.Format("ShowDeleteConfirmation('{0}', '{1}${2}'); return false;", (DirectCast(sender, GridView)).UniqueID, DELETE_COMMAND_NAME, wqisr.Id.ToString()))
                        moImageButton.Attributes.Add("onclick1", Me.ClientScript.GetPostBackEventReference(DirectCast(sender, GridView), String.Format("{0}${1}", DELETE_COMMAND_NAME, wqisr.Id.ToString())))
                    End If
                End If

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewReQueueReasons_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewReQueueReasons.RowCommand
        Try
            Select Case e.CommandName
                Case SAVE_COMMAND_NAME
                    PopulateReQueueReasonBOsFormFrom()
                    Me.State.MyReQueueReasonChildBO.Validate()
                    Me.State.IsReQueueReasonAdding = False
                    Me.State.IsReQueueReasonEditing = False
                    Me.State.MyReQueueReasonChildBO = Nothing
                Case CANCEL_COMMAND_NAME
                    If (Me.State.MyReQueueReasonChildBO.IsNew) Then
                        If (Me.State.IsReQueueReasonAdding) Then Me.State.MyReQueueReasonChildBO.Delete()
                    End If
                    Me.State.IsReQueueReasonAdding = False
                    Me.State.IsReQueueReasonEditing = False
                    Me.State.MyReQueueReasonChildBO = Nothing
                Case DELETE_COMMAND_NAME
                    Me.State.MyReQueueReasonChildBO = (From wqisr In Me.State.MyBO.ReQueueReasons Where wqisr.ItemStatusReason.Id = New Guid(e.CommandArgument.ToString()) Select wqisr).First()
                    Me.State.MyReQueueReasonChildBO.Delete()
                    Me.State.MyReQueueReasonChildBO = Nothing
                Case Else
                    'TODO : Throw
            End Select
            Me.EnableDisableFields()
            PopulateReQueueReasonGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewReDirectReasons_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewReDirectReasons.RowCommand
        Try
            Select Case e.CommandName
                Case SAVE_COMMAND_NAME
                    PopulateReDirectReasonBOsFormFrom()
                    Me.State.MyReDirectReasonChildBO.Validate()
                    Me.State.IsReDirectReasonAdding = False
                    Me.State.IsReDirectReasonEditing = False
                    Me.State.MyReDirectReasonChildBO = Nothing
                Case CANCEL_COMMAND_NAME
                    If (Me.State.MyReDirectReasonChildBO.IsNew) Then
                        If (Me.State.IsReDirectReasonAdding) Then Me.State.MyReDirectReasonChildBO.Delete()
                    End If
                    Me.State.IsReDirectReasonAdding = False
                    Me.State.IsReDirectReasonEditing = False
                    Me.State.MyReDirectReasonChildBO = Nothing
                Case DELETE_COMMAND_NAME
                    Me.State.MyReDirectReasonChildBO = (From wqisr In Me.State.MyBO.ReDirectReasons Where wqisr.ItemStatusReason.Id = New Guid(e.CommandArgument.ToString()) Select wqisr).First()
                    Me.State.MyReDirectReasonChildBO.Delete()
                    Me.State.MyReDirectReasonChildBO = Nothing
                Case Else
                    'TODO : Throw
            End Select
            Me.EnableDisableFields()
            PopulateReDirectReasonGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Schedule Grid"
    Private Sub GridViewSchedules_DataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewSchedules.RowDataBound
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
                        moCodeDropDown = CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_CODE_DROPDOWN), DropDownList)
                        moCodeDropDown.Visible = True
                        moDescriptionDropDown = CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(Me.GRID_COL_DESCRIPTION_DROPDOWN), DropDownList)
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
                            Me.SetSelectedItemByText(moCodeDropDown, dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_CODE).ToString())
                        End If
                        If (dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_DESCRIPTION).ToString() <> String.Empty) Then
                            Me.SetSelectedItemByText(moDescriptionDropDown, dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_DESCRIPTION).ToString())
                        End If


                        moTextBox = CType(e.Row.Cells(Me.GRID_COL_EFFECTIVE_IDX).FindControl(Me.GRID_COL_EFFECTIVE_TEXT), TextBox)
                        moTextBox.Visible = True
                        moImageButton = CType(e.Row.Cells(Me.GRID_COL_EFFECTIVE_IDX).FindControl(Me.GRID_COL_EFFECTIVE_IMAGE), ImageButton)
                        moImageButton.Visible = True
                        moTextBox.Text = GetLongDateFormattedString(DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE), Date))
                        Me.AddCalendarwithTime_New(moImageButton, moTextBox)
                    Else
                        moLabel = CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_CODE_LABEL), Label)
                        moLabel.Visible = True
                        moLabel.Text = dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_CODE).ToString()

                        moLabel = CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(Me.GRID_COL_DESCRIPTION_LABEL), Label)
                        moLabel.Visible = True
                        moLabel.Text = dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_DESCRIPTION).ToString()

                        moLabel = CType(e.Row.Cells(Me.GRID_COL_EFFECTIVE_IDX).FindControl(Me.GRID_COL_EFFECTIVE_LABEL), Label)
                        moLabel.Visible = True
                        moLabel.Text = GetLongDateFormattedString(DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE), Date))
                    End If

                    Dim expirationDate As DateTime = WorkQueue.DEFAULT_EXPIRATION_DATE
                    If (Not dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EXPIRATION) Is DBNull.Value) Then
                        expirationDate = DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EXPIRATION), DateTime)
                    End If
                    If (isNewRow OrElse expirationDate > DateTime.Now) Then
                        moTextBox = CType(e.Row.Cells(Me.GRID_COL_EXPIRATION_IDX).FindControl(Me.GRID_COL_EXPIRATION_TEXT), TextBox)
                        moTextBox.Visible = True
                        moImageButton = CType(e.Row.Cells(Me.GRID_COL_EXPIRATION_IDX).FindControl(Me.GRID_COL_EXPIRATION_IMAGE), ImageButton)
                        moImageButton.Visible = True
                        moTextBox.Text = GetLongDateFormattedString(expirationDate)
                        Me.AddCalendarwithTime_New(moImageButton, moTextBox)
                    Else
                        moLabel = CType(e.Row.Cells(Me.GRID_COL_EXPIRATION_IDX).FindControl(Me.GRID_COL_EXPIRATION_LABEL), Label)
                        moLabel.Visible = True
                        moLabel.Text = GetLongDateFormattedString(expirationDate)
                    End If
                Else
                    moLabel = CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_CODE_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_CODE).ToString()

                    moLabel = CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(Me.GRID_COL_DESCRIPTION_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_DESCRIPTION).ToString()

                    moLabel = CType(e.Row.Cells(Me.GRID_COL_EFFECTIVE_IDX).FindControl(Me.GRID_COL_EFFECTIVE_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = GetLongDateFormattedString(DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE), Date))

                    Dim expirationDate As DateTime = WorkQueue.DEFAULT_EXPIRATION_DATE
                    If (Not dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EXPIRATION) Is DBNull.Value) Then
                        expirationDate = DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EXPIRATION), DateTime)
                    End If
                    moLabel = CType(e.Row.Cells(Me.GRID_COL_EXPIRATION_IDX).FindControl(Me.GRID_COL_EXPIRATION_LABEL), Label)
                    moLabel.Visible = True
                    moLabel.Text = GetLongDateFormattedString(expirationDate)

                    If (Me.State.IsScheduleEditing) Then
                        moImageButton = CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_EDIT_IMAGE_BUTTON), ImageButton)
                        moImageButton.Visible = False
                        moImageButton = CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_DELETE_IMAGE_BUTTON), ImageButton)
                        moImageButton.Visible = False
                    Else
                        Dim scheduleId As Guid
                        scheduleId = New Guid(CType(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_ENTITY_SCHEDULE_ID), Byte()))
                        moImageButton = CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_EDIT_IMAGE_BUTTON), ImageButton)
                        If (Me.State.IsReadOnly) Then
                            ControlMgr.SetVisibleControl(Me, moImageButton, False)
                        Else
                            moImageButton.CommandArgument = scheduleId.ToString()
                        End If
                        moImageButton = CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_DELETE_IMAGE_BUTTON), ImageButton)
                        If (Me.State.IsReadOnly) Then
                            ControlMgr.SetVisibleControl(Me, moImageButton, False)
                        Else
                            moImageButton.Attributes.Add("onclick", String.Format("ShowDeleteConfirmation('{0}', '{1}${2}'); return false;", (DirectCast(sender, GridView)).UniqueID, DELETE_COMMAND_NAME, scheduleId.ToString()))
                            moImageButton.Attributes.Add("onclick1", Me.ClientScript.GetPostBackEventReference(DirectCast(sender, GridView), String.Format("{0}${1}", DELETE_COMMAND_NAME, scheduleId.ToString())))
                        End If
                        'Check if the Schedule’s effective date has been passed, if yes then don’t show the delete Icon to the user 
                        If (Not dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE) Is DBNull.Value) Then
                            Dim EffectiveDate As DateTime = DirectCast(dvRow(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE), DateTime)
                            If (EffectiveDate < DateTime.UtcNow) Then
                                ControlMgr.SetVisibleControl(Me, moImageButton, False)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewSchedules_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewSchedules.RowCommand
        Try
            Select Case e.CommandName
                Case EDIT_COMMAND_NAME
                    Me.State.EntityScheduleChildId = New Guid(e.CommandArgument.ToString())
                    Me.BeginScheduleChildEdit()
                Case SAVE_COMMAND_NAME
                    PopulateScheduleBOsFormFrom()
                    ' Check if adding new Schedule
                    If (Me.State.MyScheduleChildBO.IsNew) Then
                        ' Check if Current Schedule Effective Date is greater than any Effective Date
                        For Each drv As DataRowView In Me.State.MyBO.GetScheduleSelectionView
                            Dim es As EntitySchedule = Me.State.MyBO.GetScheduleChild(New Guid(CType(drv(EntitySchedule.ScheduleSelectionView.COL_NAME_ENTITY_SCHEDULE_ID), Byte())))
                            If (Not es.Id.Equals(Me.State.MyScheduleChildBO.Id)) Then
                                If (Me.State.MyScheduleChildBO.Effective.Value > es.Effective.Value AndAlso Me.State.MyScheduleChildBO.Effective.Value < es.Expiration.Value) Then
                                    ' Display Message
                                    Me.DisplayMessage(Message.MSG_EXPIRE_PREVIOUS_WQ_SCHEDULE, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Expire
                                    Return
                                End If
                            End If
                        Next
                    End If
                    Me.State.MyScheduleChildBO.Validate()
                    Me.State.MyScheduleChildBO.EndEdit()
                    Me.State.IsScheduleAdding = False
                    Me.State.IsScheduleEditing = False
                    Me.State.MyScheduleChildBO = Nothing
                Case CANCEL_COMMAND_NAME
                    If (Me.State.MyScheduleChildBO.IsNew) Then
                        If (Me.State.IsScheduleAdding) Then Me.State.MyScheduleChildBO.Delete()
                    Else
                        Me.State.MyScheduleChildBO.cancelEdit()
                    End If
                    Me.State.IsScheduleAdding = False
                    Me.State.IsScheduleEditing = False
                    Me.State.MyScheduleChildBO = Nothing
                Case DELETE_COMMAND_NAME
                    Me.State.MyScheduleChildBO = Me.State.MyBO.GetScheduleChild(New Guid(e.CommandArgument.ToString()))
                    Me.State.MyScheduleChildBO.Delete()
                    Me.State.MyScheduleChildBO = Nothing
                Case Else
                    'TODO : Throw
            End Select
            Me.EnableDisableFields()
            PopulateScheduleGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Child Objects Command Buttons Event Handlers"
    Private Sub btnAddNewReDirectReason_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewReDirectReason_WRITE.Click
        Try
            Me.State.IsReDirectReasonAdding = True
            Me.State.ReDirectReasonSelectedChildId = Guid.Empty
            Me.BeginReDirectReasonChildEdit()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateReDirectReasonBOsFormFrom()
        Dim moCodeDropDown As DropDownList
        With GridViewReDirectReasons.Rows(GridViewReDirectReasons.EditIndex)
            If (Me.State.MyReDirectReasonChildBO.IsNew) Then
                moCodeDropDown = CType(.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_CODE_DROPDOWN), DropDownList)
                Me.PopulateBOProperty(Me.State.MyReDirectReasonChildBO.ItemStatusReason, "Reason", GetSelectedDescription(moCodeDropDown))
            End If
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Sub BeginReDirectReasonChildEdit()
        Me.State.IsReDirectReasonEditing = True
        Me.EnableDisableFields()
        With Me.State
            If .ReDirectReasonSelectedChildId.Equals(Guid.Empty) Then
                .MyReDirectReasonChildBO = Me.State.MyBO.AddReDirectReason()
                .ReDirectReasonSelectedChildId = .MyReDirectReasonChildBO.Id
            Else
                .MyReDirectReasonChildBO = (From wqisr In Me.State.MyBO.ReDirectReasons Where wqisr.ItemStatusReason.Id = .ReDirectReasonSelectedChildId Select wqisr).First()
            End If
        End With
        PopulateReDirectReasonGrid()
    End Sub

    Private Sub btnAddNewReQueueReason_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewReQueueReason_WRITE.Click
        Try
            Me.State.IsReQueueReasonAdding = True
            Me.State.ReQueueReasonSelectedChildId = Guid.Empty
            Me.BeginReQueueReasonChildEdit()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateReQueueReasonBOsFormFrom()
        Dim moCodeDropDown As DropDownList
        With GridViewReQueueReasons.Rows(GridViewReQueueReasons.EditIndex)
            If (Me.State.MyReQueueReasonChildBO.IsNew) Then
                moCodeDropDown = CType(.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_CODE_DROPDOWN), DropDownList)
                Me.PopulateBOProperty(Me.State.MyReQueueReasonChildBO.ItemStatusReason, "Reason", GetSelectedDescription(moCodeDropDown))
            End If
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Sub BeginReQueueReasonChildEdit()
        Me.State.IsReQueueReasonEditing = True
        Me.EnableDisableFields()
        With Me.State
            If .ReQueueReasonSelectedChildId.Equals(Guid.Empty) Then
                .MyReQueueReasonChildBO = Me.State.MyBO.AddReQueueReason()
                .ReQueueReasonSelectedChildId = .MyReQueueReasonChildBO.Id
            Else
                .MyReQueueReasonChildBO = (From wqisr In Me.State.MyBO.ReQueueReasons Where wqisr.ItemStatusReason.Id = .ReQueueReasonSelectedChildId Select wqisr).First()
            End If
        End With
        PopulateReQueueReasonGrid()
    End Sub

    Private Sub btnAddNewSchedule_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewSchedule_WRITE.Click
        Try
            Me.State.IsScheduleAdding = True
            Me.State.EntityScheduleChildId = Guid.Empty
            Me.BeginScheduleChildEdit()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateScheduleBOsFormFrom()
        Dim moCodeDropDown As DropDownList
        Dim moDescriptionDropDown As DropDownList
        Dim moTextBox As TextBox
        With GridViewSchedules.Rows(GridViewSchedules.EditIndex)
            If (Me.State.MyScheduleChildBO.IsNew) Then
                moCodeDropDown = CType(.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_COL_CODE_DROPDOWN), DropDownList)
                moDescriptionDropDown = CType(.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(Me.GRID_COL_DESCRIPTION_DROPDOWN), DropDownList)
                moTextBox = CType(.Cells(Me.GRID_COL_EFFECTIVE_IDX).FindControl(Me.GRID_COL_EFFECTIVE_TEXT), TextBox)

                Me.PopulateBOProperty(Me.State.MyScheduleChildBO, "ScheduleId", moCodeDropDown)
                Me.PopulateBOProperty(Me.State.MyScheduleChildBO, "ScheduleCode", GetSelectedDescription(moCodeDropDown))
                If (Me.State.MyScheduleChildBO.ScheduleId.Equals(GetSelectedItem(moDescriptionDropDown))) Then
                    Me.PopulateBOProperty(Me.State.MyScheduleChildBO, "ScheduleDescription", GetSelectedDescription(moDescriptionDropDown))
                Else
                    Me.PopulateBOProperty(Me.State.MyScheduleChildBO, "ScheduleDescription", String.Empty)
                End If
                Me.PopulateBOProperty(Me.State.MyScheduleChildBO, "Effective", moTextBox)
            End If
            moTextBox = CType(.Cells(Me.GRID_COL_EXPIRATION_IDX).FindControl(Me.GRID_COL_EXPIRATION_TEXT), TextBox)
            Me.PopulateBOProperty(Me.State.MyScheduleChildBO, "Expiration", moTextBox)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub BeginScheduleChildEdit()
        Me.State.IsScheduleEditing = True
        Me.EnableDisableFields()
        With Me.State
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
    Private Sub EnableDisableTabs(ByVal blnFlag As Boolean)
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

