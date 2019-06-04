Imports Assurant.ElitaPlus.DALObjects
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports System.Web.Script.Services
Imports Assurant.ElitaPlus.Security
Imports System.Web.Script.Serialization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements

Namespace Tables

    Partial Class ScheduleForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

        Class MyState
            Public MyBO As Schedule
            Public MyScheduleDetailChildBO As ScheduleDetail
            Public ScreenSnapShotBO As Schedule
            Public ScreenSnapScheduleDetailChildBO As ScheduleDetail

            Public IsScheduleDetailEditing As Boolean = False
            Public SelectedScheduleDetailChildId As Guid = Guid.Empty

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public UserHasPermissions As Boolean

            Public PageIndex As Integer = 0
            Public ScheduleId As Guid = Guid.Empty

            Public Action As String
            Public CopyScheduleId As Guid = Guid.Empty
            Public DayofWeek As String

            Public Id As Guid
            Public IsAfterEditSave As Boolean
            Public IsAfterFinalSave As Boolean
            Public IsEditMode As Boolean
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public searchDV As DataView = Nothing
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public SortExpression As String = ScheduleDetail.ScheduleDetailSearchDV.COL_DAY_OF_WEEK_ID
            Public bnoRow As Boolean = False
            Public IsReadOnly As Boolean = False
            Public NoAdminRoleWorkQueueName As String

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

#Region "Constants"

        Public Const URL As String = "~/Tables/ScheduleForm.aspx"

        Private Const SCHEDULE_DETAIL_ID_COL As Integer = 0
        Private Const DAY_OF_WEEK_COL As Integer = 1
        Private Const FROM_TIME_COL As Integer = 2
        Private Const TO_TIME_COL As Integer = 3
        Private Const SCHEDULE_ID_COL As Integer = 4

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_COPIED_OK As String = "MSG_COPY_WAS_COMPLETED_SUCCESSFULLY"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const DAY_OF_WEEK_LIST_CONTROL As String = "moDayOfWeek"
        Private Const DAY_OF_WEEK_LABEL_CONTROL As String = "moDayOfWeekLabel"
        Private Const FROM_TIME_CONTROL_NAME As String = "moFromTimeText"
        Private Const TO_TIME_CONTROL_NAME As String = "moToTimeText"
        Private Const SCHEDULE_DETAIL_ID_CONTROL_NAME As String = "moScheduleDetail_ID"
        Private Const SCHEDULE_ID_CONTROL_NAME As String = "moSchedule_ID"
        Private Const EDIT_IMAGE_CONTROL_NAME As String = "EditButton_WRITE"
        Private Const DELETE_IMAGE_CONTROL_NAME As String = "DeleteButton_WRITE"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const CANCEL_COMMAND As String = "CancelRecord"
        Private Const SAVE_COMMAND As String = "SaveRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const ENTER_TO_TIME As String = "EnterToTime"
        Private Const ENTER_FROM_TIME As String = "EnterFromTime"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private COPY_SCHEDULE As String = "COPY_SCHEDULE"
        Private NEW_SCHEDULE As String = "NEW_SCHEDULE"
        Private INIT_LOAD As String = "INIT_LOAD"

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public ScheduleBO As Schedule
            Public HasDataChanged As Boolean

            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal ScheduleBO As Schedule, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.ScheduleBO = ScheduleBO
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Parameters"
        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Me.State.ScheduleId = CType(Me.CallingParameters, Guid)

                    If Me.State.ScheduleId.Equals(Guid.Empty) Then
                        Me.State.Action = Me.INIT_LOAD
                    Else
                        Me.State.MyBO = New Schedule(Me.State.ScheduleId)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UpdateReadOnlyFlag()
            Dim dt As DataTable
            Me.State.IsReadOnly = False
            ' Find all the Work Queues which are using this Schedule as of today or in future.
            dt = EntitySchedule.GetList(Me.State.MyBO.Id, EntitySchedule.TABLE_NAME_WORKQUEUE)
            ' For each Work Queue, check if User is in Admin Role, if not then ReadOnly = True
            For Each dr As DataRow In dt.Rows
                Dim wqId As Guid
                Dim wq As WorkQueue
                wqId = New Guid(CType(dr(EntityScheduleDAL.COL_NAME_ENTITY_ID), Byte()))
                wq = New WorkQueue(wqId)
                If (Not ElitaPlusIdentity.Current.ActiveUser.isInRole(wq.WorkQueue.AdminRole)) Then
                    Me.State.NoAdminRoleWorkQueueName = wq.WorkQueue.Name
                    Me.State.IsReadOnly = True
                    Return
                End If
            Next
        End Sub

#End Region

#Region "Page_Events"

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("GENERAL_INFORMATION") & _
                                          ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("SEARCH_SCHEDULE") & _
                                          ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("SCHEDULE_DETAILS")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("SCHEDULE_DETAILS")
            End If
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.MasterPage.MessageController.Clear()
                If Not Me.IsPostBack Then
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New Schedule
                    End If
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("TABLES")
                    UpdateBreadCrum()

                    Me.State.PageIndex = 0

                    PopulateHeader()
                    PopulateGrid()
                    SetButtonsState()
                    SetLowerButtonsState()
                    'PopulateHeader determines if the Schedule Form is Read Only
                    If Me.State.IsReadOnly Then
                        Me.MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_NO_ADMIN_ROLE_ON_WORKQUEUE)
                        Me.MasterPage.MessageController.AddWarning(Me.State.NoAdminRoleWorkQueueName)
                    End If
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
                CheckIfComingFromSaveConfirm()

                If Not Page.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                End If

            Catch ex As Exception
                ' Clean Popup Input
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = ""
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub


#End Region

#Region " GridView Related "

        Private Sub PopulateGrid()
            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetDV()
                End If

                If Me.State.IsAfterEditSave And Me.State.IsAfterFinalSave Then
                    Me.State.IsAfterEditSave = False
                    Me.State.IsAfterFinalSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
                End If

                Me.Grid.AutoGenerateColumns = False

                Me.State.SortExpression = ScheduleDetail.ScheduleDetailSearchDV.COL_DAY_OF_WEEK_ID

                Me.SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyScheduleDetailChildBO, "DayOfWeekId", Me.Grid.Columns(Me.DAY_OF_WEEK_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyScheduleDetailChildBO, "FromTime", Me.Grid.Columns(Me.FROM_TIME_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyScheduleDetailChildBO, "ToTime", Me.Grid.Columns(Me.TO_TIME_COL))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim moEditImageButton As ImageButton
            Dim moDeleteImageButton As ImageButton
            Dim dayOfWeekId As Guid
            Dim fromTime As String
            Dim toTime As String
            Dim dayOfWeekLabel As Label = CType(e.Row.FindControl(DAY_OF_WEEK_LABEL_CONTROL), Label)
            Dim scheduleDetailLabel As Label = CType(e.Row.FindControl(SCHEDULE_DETAIL_ID_CONTROL_NAME), Label)
            Dim dayOfWeekList As DropDownList = CType(e.Row.FindControl(DAY_OF_WEEK_LIST_CONTROL), DropDownList)
            Dim fromTimeTextBox As TextBox = CType(e.Row.FindControl(FROM_TIME_CONTROL_NAME), TextBox)
            Dim toTimeTextBox As TextBox = CType(e.Row.FindControl(TO_TIME_CONTROL_NAME), TextBox)

            Try

                If Not dvRow Is Nothing And (Not Me.State.bnoRow) Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Or itemType = ListItemType.EditItem Then
                        If (Me.State.IsEditMode AndAlso _
                           Me.State.Id.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_SCHEDULE_DETAIL_ID), Byte())))) Then
                            Me.PopulateControlFromBOProperty(scheduleDetailLabel, dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_SCHEDULE_DETAIL_ID))
                            Me.PopulateControlFromBOProperty(e.Row.Cells(Me.SCHEDULE_ID_COL), dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_SCHEDULE_ID))

                            PopulateDayOfWeekDropdown(dayOfWeekList)
                            If Not Me.State.DayofWeek Is Nothing Then
                                Me.SetSelectedItemByText(dayOfWeekList, Me.State.DayofWeek)
                            Else
                                dayOfWeekList.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                            End If

                            If Not Me.State.MyScheduleDetailChildBO Is Nothing AndAlso Not dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_FROM_TIME) Is DBNull.Value _
                                AndAlso Not dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_TO_TIME) Is DBNull.Value Then
                                fromTimeTextBox.Text = CType(Me.State.MyScheduleDetailChildBO.FromTime, Date).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                                toTimeTextBox.Text = CType(Me.State.MyScheduleDetailChildBO.ToTime, Date).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                            Else
                                fromTimeTextBox.Text = ""
                                toTimeTextBox.Text = ""
                            End If
                        Else
                            Me.PopulateControlFromBOProperty(scheduleDetailLabel, dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_SCHEDULE_DETAIL_ID))

                            dayOfWeekId = New Guid(CType(dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_DAY_OF_WEEK_ID), Byte()))
                            Me.PopulateControlFromBOProperty(dayOfWeekLabel, LookupListNew.GetDescriptionFromId(LookupListCache.LK_DAYS_OF_WEEK, dayOfWeekId))

                            If Not dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_FROM_TIME) Is DBNull.Value Then
                                fromTime = CType(dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_FROM_TIME), Date).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                            Else
                                fromTime = ""
                            End If
                            Me.PopulateControlFromBOProperty(e.Row.Cells(Me.FROM_TIME_COL), fromTime)

                            If Not dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_TO_TIME) Is DBNull.Value Then
                                toTime = CType(dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_TO_TIME), Date).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                            Else
                                toTime = ""
                            End If
                            Me.PopulateControlFromBOProperty(e.Row.Cells(Me.TO_TIME_COL), toTime)

                            Me.PopulateControlFromBOProperty(e.Row.Cells(Me.SCHEDULE_ID_COL), dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_SCHEDULE_ID))

                            moEditImageButton = CType(e.Row.FindControl(EDIT_IMAGE_CONTROL_NAME), ImageButton)
                            moDeleteImageButton = CType(e.Row.FindControl(DELETE_IMAGE_CONTROL_NAME), ImageButton)
                            If (Me.State.IsReadOnly) Then
                                ControlMgr.SetVisibleControl(Me, moEditImageButton, False)
                                ControlMgr.SetVisibleControl(Me, moDeleteImageButton, False)
                            End If

                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    'Do the Edit here
                    index = CInt(e.CommandArgument)
                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True
                    Me.State.DayofWeek = CType(Me.Grid.Rows(index).Cells(Me.DAY_OF_WEEK_COL).FindControl(Me.DAY_OF_WEEK_LABEL_CONTROL), Label).Text
                    Me.State.Id = New Guid(CType(Me.Grid.Rows(index).Cells(Me.SCHEDULE_DETAIL_ID_COL).FindControl(Me.SCHEDULE_DETAIL_ID_CONTROL_NAME), Label).Text)

                    Me.State.MyScheduleDetailChildBO = Me.State.MyBO.GetScheduleDetailChild(Me.State.Id)
                    Me.State.MyScheduleDetailChildBO.BeginEdit()
                    Me.PopulateGrid()
                    Me.State.PageIndex = Grid.PageIndex
                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)

                    If Me.Grid.SelectedRow.RowIndex >= 0 Then
                        'Set focus on the Description TextBox for the EditItemIndex row
                        Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DAY_OF_WEEK_COL, Me.DAY_OF_WEEK_LIST_CONTROL, Me.Grid.SelectedRow.RowIndex)
                    End If

                    If Me.Grid.EditIndex >= 0 Then
                        Me.AssignSelectedRecordFromBO()
                    End If

                    Me.SetButtonsState()
                    Me.SetLowerButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then

                    'Do the delete here
                    index = CInt(e.CommandArgument)

                    Me.PopulateGrid()
                    Me.State.PageIndex = Grid.PageIndex

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
                    'Save the Id in the Session
                    Me.State.Id = New Guid(CType(Me.Grid.Rows(index).Cells(Me.SCHEDULE_DETAIL_ID_COL).FindControl(Me.SCHEDULE_DETAIL_ID_CONTROL_NAME), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                ElseIf (e.CommandName = Me.SAVE_COMMAND) Then

                    Me.State.DayofWeek = Me.GetSelectedDescription(CType(Me.GetSelectedGridControl(Grid, DAY_OF_WEEK_COL), DropDownList))

                    SaveRecord()

                    Me.PopulateGrid()
                    Me.State.PageIndex = Grid.PageIndex

                ElseIf (e.CommandName = Me.CANCEL_COMMAND) Then
                    CancelRecord()
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)

            If controlName = Me.DAY_OF_WEEK_LIST_CONTROL Then
                'Set focus on the Day of Week Dropdownlist for the EditItemIndex row
                Dim desc As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
                SetFocus(desc)
            Else
                'Set focus on the Time TextBox for the EditItemIndex row
                Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
                SetFocus(desc)
            End If

        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.PageIndex
            If (Me.State.searchDV.Count = 0) Then
                Me.State.bnoRow = True
                If Me.State.MyScheduleDetailChildBO Is Nothing Then
                    Me.State.MyScheduleDetailChildBO = Me.State.MyBO.GetNewScheduleDetailChild
                    Me.State.Id = Me.State.MyScheduleDetailChildBO.Id
                End If

                Me.State.searchDV = Me.State.MyScheduleDetailChildBO.GetNewDataViewRow(Me.State.searchDV, Me.State.Id, Me.State.MyScheduleDetailChildBO)

                Grid.DataSource = Me.State.searchDV
                'Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Grid.DataBind()
                'TranslateGridHeader(Grid)
                'Me.TranslateGridControls(Grid)
                Grid.Rows(0).Visible = False
                ''BindBoPropertiesToGridHeaders()
            Else
                Me.State.bnoRow = False
                Me.Grid.DataSource = Me.State.searchDV
                'HighLightSortColumn(Grid, Me.State.SortExpression)
                Me.Grid.DataBind()
                If (Me.State.searchDV.Count = 1) And Me.State.AddingNewRow And Me.State.Canceling Then
                    Grid.Rows(0).Visible = False
                Else
                    Grid.Rows(0).Visible = True
                End If

            End If

            'TranslateGridHeader(Grid)
            Grid.PagerSettings.Visible = True
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True


            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            Me.State.searchDV = GetGridDataView()
            Me.State.searchDV.Sort = Grid.DataMember()

            Return (Me.State.searchDV)

        End Function

        Private Function GetGridDataView() As DataView
            Return Me.State.MyBO.GetScheduleDetailSelectionView()
        End Function

        Private Sub Grid_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.SortExpression.EndsWith(" DESC") Then
                        Me.State.SortExpression = e.SortExpression
                    Else
                        Me.State.SortExpression &= " DESC"
                    End If
                Else
                    Me.State.SortExpression = e.SortExpression
                End If
                Me.State.Id = Guid.Empty
                Me.State.PageIndex = 0

                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.Grid.PageIndex = Me.State.PageIndex
                    Me.PopulateGrid()
                    Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Helper Functions"

        Private Sub PopulateHeader()
            UpdateReadOnlyFlag()
            TranslateGridHeader(Grid)
            If (Not Me.State.ScheduleId = Guid.Empty) And (Not Me.State.MyBO Is Nothing) Then
                moScheduleCode.Enabled = False
                moScheduleCode.Text = Me.State.MyBO.Code

                If Not Me.State.IsReadOnly Then
                    moScheduleDescription.Enabled = True
                Else
                    moScheduleDescription.Enabled = False
                End If
                moScheduleDescription.Text = Me.State.MyBO.Description
                'TranslateGridHeader(Grid)
            Else
                moScheduleCode.Enabled = True
                moScheduleCode.Text = String.Empty

                moScheduleDescription.Enabled = True
                moScheduleDescription.Text = String.Empty

                If Me.State.Action <> Me.COPY_SCHEDULE Then
                    Me.State.Action = Me.INIT_LOAD
                    Me.State.IsEditMode = False
                    Me.State.searchDV = Nothing
                End If
            End If

        End Sub

        Public Sub PopulateDayOfWeekDropdown(Optional ByVal dayOfWeekList As DropDownList = Nothing)

            ' Dim commentTypeLk As DataView
            Try
                ' commentTypeLk = LookupListNew.GetDayOfWeekLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Dim commentTypeLk As ListItem() = CommonConfigManager.Current.ListManager.GetList("DAYS_OF_WEEK", Thread.CurrentPrincipal.GetLanguageCode())

                If dayOfWeekList Is Nothing Then
                    'Me.BindListControlToDataView(CType(Me.Grid.Controls.Item(Me.DAY_OF_WEEK_COL), ListControl), commentTypeLk) '.BindListControlToDataView
                Else
                    ' commentTypeLk.Table.Locale = System.Globalization.CultureInfo.CurrentCulture
                    'commentTypeLk.Sort = LookupListNew.COL_CODE_NAME
                    ' Me.BindListControlToDataView(dayOfWeekList, commentTypeLk, , , , False)
                    dayOfWeekList.Populate(commentTypeLk, New PopulateOptions() With
                    {
                              .AddBlankItem = False,
                              .SortFunc = AddressOf .GetCode
                                      })

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub AddNew()

            Dim dv As DataView

            Me.State.searchDV = GetGridDataView()
            If Me.State.MyScheduleDetailChildBO Is Nothing Then
                Me.State.MyScheduleDetailChildBO = Me.State.MyBO.GetNewScheduleDetailChild
                Me.State.Id = Me.State.MyScheduleDetailChildBO.Id
            End If

            ' To ensure we begin and end edit
            Me.State.MyScheduleDetailChildBO.BeginEdit()

            Me.State.searchDV = Me.State.MyScheduleDetailChildBO.GetNewDataViewRow(Me.State.searchDV, Me.State.Id, Me.State.MyScheduleDetailChildBO)
            Grid.DataSource = Me.State.searchDV

            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Grid.DataBind()

            Me.State.PageIndex = Grid.PageIndex

            If Me.State.bnoRow = True Then
                'ControlMgr.SetVisibleControl(Me, Grid1, False)
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            SetGridControls(Me.Grid, False)

            If Me.Grid.EditIndex >= 0 Then
                'Set focus on the Days of Week List Control for the EditItemIndex row
                Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DAY_OF_WEEK_COL, Me.DAY_OF_WEEK_LIST_CONTROL, Me.Grid.EditIndex)
                Me.AssignSelectedRecordFromBO()
            End If

            Me.TranslateGridControls(Grid)
            Me.SetButtonsState()
            Me.SetLowerButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Protected Sub PopulateBOfromForm()
            With Me.State.MyBO
                .Code = moScheduleCode.Text
                .Description = moScheduleDescription.Text
            End With

        End Sub

        Protected Sub PopulateFormfromBO()
            'UpdateReadOnlyFlag()
            'PopulateScheduleDetailGrid()
            With Me.State.MyBO
                moScheduleCode.Text = .Code
                moScheduleDescription.Text = .Description
            End With

        End Sub

        Private Sub AssignSelectedRecordFromBO()
            Dim dayOfWeekList As DropDownList
            Dim fromTimeText As TextBox
            Dim toTimeText As TextBox
            Dim scheduleIDLabel As Label
            Dim scheduleDetailIDLabel As Label
            Dim gridRowIdx As Integer = Me.Grid.EditIndex
            Try
                With Me.State.MyScheduleDetailChildBO
                    fromTimeText = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.FROM_TIME_COL).FindControl(Me.FROM_TIME_CONTROL_NAME), TextBox)
                    toTimeText = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.TO_TIME_COL).FindControl(Me.TO_TIME_CONTROL_NAME), TextBox)
                    dayOfWeekList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.DAY_OF_WEEK_COL).FindControl(Me.DAY_OF_WEEK_LIST_CONTROL), DropDownList)
                    PopulateDayOfWeekDropdown(dayOfWeekList)

                    If Not Me.State.DayofWeek Is Nothing Then
                        Me.SetSelectedItemByText(dayOfWeekList, Me.State.DayofWeek)
                    Else
                        dayOfWeekList.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                    End If

                    If (Not fromTimeText Is Nothing) And (Not .FromTime Is Nothing) And (Not .FromTime Is String.Empty) Then
                        fromTimeText.Text = CDate(.FromTime).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                    End If
                    If (Not toTimeText Is Nothing) And (Not .ToTime Is Nothing) And (Not .ToTime Is String.Empty) Then
                        toTimeText.Text = CDate(.ToTime).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                    End If

                    scheduleDetailIDLabel = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.SCHEDULE_DETAIL_ID_COL).FindControl(Me.SCHEDULE_DETAIL_ID_CONTROL_NAME), Label)
                    scheduleDetailIDLabel.Text = .Id.ToString

                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub AssignBOFromSelectedRecord(ByVal strFromTime As String, ByVal strToTime As String)

            Me.State.MyScheduleDetailChildBO.FromTime = DateTime.Parse(String.Format("{0} {1}", DateTime.MinValue.ToString( _
                                                             Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern), strFromTime))
            Me.State.MyScheduleDetailChildBO.ToTime = DateTime.Parse(String.Format("{0} {1}", DateTime.MinValue.ToString( _
                                                           Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern), strToTime))

            Me.State.MyScheduleDetailChildBO.DayOfWeekId = Me.GetSelectedItem(CType(Me.GetSelectedGridControl(Grid, DAY_OF_WEEK_COL), DropDownList))

        End Sub

        Sub PopulateMyScheduleDetailChildBOFromDetail()
            Dim strFromTime As String
            Dim strToTime As String
            Try
                strFromTime = CType(Me.GetSelectedGridControl(Grid, FROM_TIME_COL), TextBox).Text
                strToTime = CType(Me.GetSelectedGridControl(Grid, TO_TIME_COL), TextBox).Text

                If Not System.Text.RegularExpressions.Regex.IsMatch(strFromTime, ElitaPlus.Common.RegExConstants.TIME_REGEX, _
                                                                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.MessageController.AddWarning(String.Format("{0}: {1}", _
                                           TranslationBase.TranslateLabelOrMessage("FROM_TIME"), _
                                           TranslationBase.TranslateLabelOrMessage("FROM_TIME_INVALID"), False))
                    Exit Sub
                End If
                If Not System.Text.RegularExpressions.Regex.IsMatch(strToTime, ElitaPlus.Common.RegExConstants.TIME_REGEX, _
                                                                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.MessageController.AddWarning(String.Format("{0}: {1}", _
                                           TranslationBase.TranslateLabelOrMessage("TO_TIME"), _
                                           TranslationBase.TranslateLabelOrMessage("TO_TIME_INVALID"), False))
                    Exit Sub
                End If

                AssignBOFromSelectedRecord(strFromTime, strToTime)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CancelRecord()
            Try
                ' To implement BeginEdit and CancelEdit
                Me.State.MyScheduleDetailChildBO.cancelEdit()
                If Me.State.MyScheduleDetailChildBO.IsSaveNew Then
                    Me.State.MyScheduleDetailChildBO.Delete()
                    Me.State.MyScheduleDetailChildBO.Save()
                End If
                Me.State.MyScheduleDetailChildBO = Nothing

                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True
                If (Me.State.AddingNewRow) Then
                    Me.State.AddingNewRow = False
                    Me.State.searchDV = Nothing
                End If

                ReturnFromEditing()
                SetLowerButtonsState()
                Me.State.Canceling = False
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveRecord()

            Dim strFromTime As String
            Dim strToTime As String
            Try
                strFromTime = CType(Me.GetSelectedGridControl(Grid, FROM_TIME_COL), TextBox).Text
                strToTime = CType(Me.GetSelectedGridControl(Grid, TO_TIME_COL), TextBox).Text

                If Not System.Text.RegularExpressions.Regex.IsMatch(strFromTime, ElitaPlus.Common.RegExConstants.TIME_REGEX, _
                                                                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.MessageController.AddWarning(String.Format("{0}: {1}", _
                                           TranslationBase.TranslateLabelOrMessage("FROM_TIME"), _
                                           TranslationBase.TranslateLabelOrMessage("FROM_TIME_INVALID"), False))
                    Exit Sub
                End If
                If Not System.Text.RegularExpressions.Regex.IsMatch(strToTime, ElitaPlus.Common.RegExConstants.TIME_REGEX, _
                                                                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.MessageController.AddWarning(String.Format("{0}: {1}", _
                                           TranslationBase.TranslateLabelOrMessage("TO_TIME"), _
                                           TranslationBase.TranslateLabelOrMessage("TO_TIME_INVALID"), False))
                    Exit Sub
                End If

                AssignBOFromSelectedRecord(strFromTime, strToTime)

                If Me.State.MyScheduleDetailChildBO.IsDirty Then
                    Me.State.MyScheduleDetailChildBO.Save()
                    Me.State.MyScheduleDetailChildBO.EndEdit()
                    Me.State.IsAfterEditSave = True
                    Me.State.IsEditMode = False
                    Me.State.AddingNewRow = False
                    Me.State.bnoRow = False
                    Me.State.searchDV = Nothing
                    Me.State.Action = ""
                    Me.State.MyScheduleDetailChildBO = Nothing
                End If
                SetLowerButtonsState()
                Me.ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub DoDelete()

            Me.State.MyScheduleDetailChildBO = Me.State.MyBO.GetScheduleDetailChild(Me.State.Id)
            Try
                Me.State.MyScheduleDetailChildBO.Delete()
                Me.State.MyScheduleDetailChildBO.Save()
                Me.State.MyScheduleDetailChildBO.EndEdit()
                Me.State.SelectedScheduleDetailChildId = Guid.Empty
                Me.State.MyScheduleDetailChildBO = Nothing
            Catch ex As Exception
                Me.State.MyBO.RejectChanges()
                Throw ex
            End Try

            Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterEditSave = True
            Me.State.IsEditMode = False
            Me.State.searchDV = Nothing
            SetLowerButtonsState()
            Me.ReturnFromEditing()
            Me.State.PageIndex = Grid.PageIndex
        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX
            If Me.Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            Me.State.IsEditMode = False
            Me.State.searchDV = Nothing
            Me.PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            SetButtonsState()
        End Sub

        Private Sub SetButtonsState()

            If Me.State.IsEditMode Then
                ControlMgr.SetEnableControl(Me, BtnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnBack, False)
                Me.MenuEnabled = False
            ElseIf Me.State.IsReadOnly Then
                ControlMgr.SetEnableControl(Me, BtnNew_WRITE, False)
                'Diable the Edit and Delete Buttons as disabled
            Else
                If Me.State.Action <> Me.COPY_SCHEDULE Then
                    ControlMgr.SetEnableControl(Me, BtnNew_WRITE, True)
                Else
                    ControlMgr.SetEnableControl(Me, BtnNew_WRITE, False)
                End If

                ControlMgr.SetEnableControl(Me, btnBack, True)
                Me.MenuEnabled = True
            End If

        End Sub

        Private Sub SetLowerButtonsState()
            'Me.State.IsAfterSave
            If Me.State.IsEditMode = True Then
                btnBack.Enabled = False
                btnApply_WRITE.Enabled = False
                btnButtomNew_WRITE.Enabled = False
                btnCopy_WRITE.Enabled = False
                btnUndo_WRITE.Enabled = False
            ElseIf Me.State.IsReadOnly Then
                btnBack.Enabled = True
                btnApply_WRITE.Enabled = False
                btnButtomNew_WRITE.Enabled = False
                btnCopy_WRITE.Enabled = True
                btnUndo_WRITE.Enabled = False
            Else
                If Me.State.Action = Me.NEW_SCHEDULE Or _
                   Me.State.Action = Me.INIT_LOAD Then
                    btnBack.Enabled = True
                    btnApply_WRITE.Enabled = True
                    btnButtomNew_WRITE.Enabled = False
                    btnCopy_WRITE.Enabled = False
                    btnUndo_WRITE.Enabled = False
                ElseIf Me.State.Action = Me.COPY_SCHEDULE Then
                    btnBack.Enabled = False
                    btnApply_WRITE.Enabled = True
                    btnButtomNew_WRITE.Enabled = False
                    btnCopy_WRITE.Enabled = False
                    btnUndo_WRITE.Enabled = True
                ElseIf Me.State.IsAfterEditSave = True Then
                    btnBack.Enabled = True
                    btnUndo_WRITE.Enabled = True
                    btnApply_WRITE.Enabled = True
                    btnButtomNew_WRITE.Enabled = False
                    btnCopy_WRITE.Enabled = False
                ElseIf Me.State.IsAfterFinalSave = True Then
                    btnBack.Enabled = True
                    btnUndo_WRITE.Enabled = False
                    btnApply_WRITE.Enabled = True
                    btnButtomNew_WRITE.Enabled = True
                    btnCopy_WRITE.Enabled = True
                Else
                    btnBack.Enabled = True
                    btnApply_WRITE.Enabled = True
                    btnButtomNew_WRITE.Enabled = True
                    btnCopy_WRITE.Enabled = True
                    btnUndo_WRITE.Enabled = False
                End If
            End If
        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                    'Clean after consuming the action
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                'Me.State.searchDV = Nothing
                SetLowerButtonsState()
                Me.ReturnFromEditing()
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenDeletePromptResponse.Value = ""
            End If
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()

            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                    Me.State.MyBO.Save()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                        Me.ReturnToCallingPage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        'Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                        Me.ReturnToCallingPage()
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        If (Me.State.IsEditMode) Then
                            Me.PopulateMyScheduleDetailChildBOFromDetail()
                            Me.State.MyScheduleDetailChildBO.Save()
                            Me.State.MyScheduleDetailChildBO.EndEdit()
                            Me.State.IsEditMode = False
                        End If
                        Me.SetLowerButtonsState()
                        Me.ReturnFromEditing()
                        'Me.EnableDisableFields()
                        'Me.PopulateScheduleDetailGrid()
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                        Me.ReturnToCallingPage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        If (Me.State.IsEditMode) Then
                            Me.State.MyScheduleDetailChildBO.cancelEdit()
                            If Me.State.MyScheduleDetailChildBO.IsSaveNew Then
                                Me.State.MyScheduleDetailChildBO.Delete()
                                Me.State.MyScheduleDetailChildBO.Save()
                            End If
                            Me.State.IsEditMode = False
                        End If
                        Me.SetLowerButtonsState()
                        Me.ReturnFromEditing()
                        'Me.EnableDisableFields()
                        'Me.PopulateScheduleDetailGrid()
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub

#End Region

#Region "Button Click Handlers"

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.AddingNewRow = True
                ' '' ''Me.State.bnoRow = True
                AddNew()
                SetButtonsState()
                SetLowerButtonsState()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOfromForm()
                'Me.PopulateGrid()
                'Me.State.PageIndex = Grid.PageIndex
                If (Not Me.State.bnoRow) And Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Dim scheduleBO As New Schedule
                Me.ReturnToCallingPage()
            End Try
        End Sub

        Private Sub btnButtomNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnButtomNew_WRITE.Click
            Try
                Me.State.Action = Me.NEW_SCHEDULE
                Me.State.ScheduleId = Guid.Empty
                Me.State.IsEditMode = False
                Me.State.AddingNewRow = False
                Me.State.searchDV = Nothing

                Me.PopulateBOfromForm()

                If Not Me.State.bnoRow And Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.CreateNew()
                End If

                PopulateGrid()
                PopulateHeader()
                SetButtonsState()
                SetLowerButtonsState()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            Me.State.MyBO = New Schedule
            Me.PopulateBOfromForm()
            'Me.EnableDisableFields()
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                Me.State.Action = Me.COPY_SCHEDULE
                Me.State.CopyScheduleId = Me.State.ScheduleId
                Me.State.ScheduleId = Guid.Empty

                Me.PopulateBOfromForm()
                If Not Me.State.bnoRow And Me.State.MyBO.IsDirty Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    Me.CreateNewWithCopy()
                End If
                PopulateHeader()
                SetButtonsState()
                SetLowerButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CreateNewWithCopy()

            Me.PopulateBOfromForm()

            Dim newObj As New Schedule
            newObj.Copy(Me.State.MyBO)

            Me.State.MyBO = newObj
            Me.PopulateFormfromBO()

            'Me.PopulateGrid()

            'Populate the Schedule Detail
            Dim dv As DataView = ScheduleDetail.LoadScheduleDetail(Me.State.CopyScheduleId)
            Dim dt As DataTable = dv.Table
            For Each row As DataRow In dt.Rows
                Me.State.MyScheduleDetailChildBO = Me.State.MyBO.GetNewScheduleDetailChild
                Me.State.Id = Me.State.MyScheduleDetailChildBO.Id
                Me.State.MyScheduleDetailChildBO.ScheduleId = Me.State.MyBO.Id
                Me.State.MyScheduleDetailChildBO.FromTime = CType(row(ScheduleDetail.ScheduleDetailSearchDV.COL_FROM_TIME), Date)
                Me.State.MyScheduleDetailChildBO.ToTime = CType(row(ScheduleDetail.ScheduleDetailSearchDV.COL_TO_TIME), Date)
                Me.State.MyScheduleDetailChildBO.DayOfWeekId = New Guid(CType(row(ScheduleDetail.ScheduleDetailSearchDV.COL_DAY_OF_WEEK_ID), Byte()))
                Me.State.MyScheduleDetailChildBO.Save()
            Next

            Me.State.searchDV = Nothing
            Me.PopulateGrid()

        End Sub

        Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                Me.PopulateBOfromForm()
                ' We are enforcing to save the schedule Detail before saving the Schedule
                If Me.State.bnoRow And Not Me.State.MyScheduleDetailChildBO Is Nothing Then
                    If Not Me.State.MyScheduleDetailChildBO.IsValid Then
                        Me.State.MyScheduleDetailChildBO.BeginEdit()
                        Me.State.MyScheduleDetailChildBO.Delete()
                        Me.State.MyScheduleDetailChildBO.Save()
                        Me.State.MyScheduleDetailChildBO.EndEdit()
                        Me.State.MyScheduleDetailChildBO = Nothing
                    End If
                End If
                If Me.State.MyBO.IsDirty And (Me.State.MyScheduleDetailChildBO Is Nothing Or Me.State.CopyScheduleId <> Guid.Empty) Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormfromBO()

                    Me.State.IsEditMode = False
                    Me.State.IsAfterFinalSave = True
                    If Me.State.Action = Me.COPY_SCHEDULE Then
                        Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_COPIED_OK, True)
                    Else
                        Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                    End If
                    Me.State.Action = ""
                    Me.State.CopyScheduleId = Guid.Empty 'To Clear the last Copy performed
                    Me.State.ScheduleId = Me.State.MyBO.Id
                    Me.State.searchDV = Nothing
                    Me.PopulateHeader()
                Else
                    Me.MasterPage.MessageController.AddInformation(Me.MSG_RECORD_NOT_SAVED, True)
                End If
                Me.ReturnFromEditing()
                SetLowerButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New Schedule(Me.State.MyBO.Id)
                ElseIf Not Me.State.CopyScheduleId = Guid.Empty Then
                    'It was a new with copy
                    Me.State.MyBO = New Schedule(Me.State.CopyScheduleId)
                    Me.State.ScheduleId = Me.State.CopyScheduleId
                    Me.State.CopyScheduleId = Guid.Empty 'To Clear the last Copy performed
                    Me.State.Action = ""

                    If Not Me.State.MyScheduleDetailChildBO Is Nothing Then
                        Me.State.MyScheduleDetailChildBO = Nothing
                    End If
                    If Not Me.State.Id = Guid.Empty Then
                        Me.State.Id = Guid.Empty
                    End If
                Else
                    CreateNew()
                End If
                Me.PopulateFormfromBO()
                SetLowerButtonsState()
                Me.State.searchDV = Nothing
                Me.PopulateHeader()
                Me.ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class
End Namespace