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

            Public Sub New(LastOp As DetailPageCommand, ScheduleBO As Schedule, hasDataChanged As Boolean)
                LastOperation = LastOp
                Me.ScheduleBO = ScheduleBO
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Parameters"
        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    State.ScheduleId = CType(CallingParameters, Guid)

                    If State.ScheduleId.Equals(Guid.Empty) Then
                        State.Action = INIT_LOAD
                    Else
                        State.MyBO = New Schedule(State.ScheduleId)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UpdateReadOnlyFlag()
            Dim dt As DataTable
            State.IsReadOnly = False
            ' Find all the Work Queues which are using this Schedule as of today or in future.
            dt = EntitySchedule.GetList(State.MyBO.Id, EntitySchedule.TABLE_NAME_WORKQUEUE)
            ' For each Work Queue, check if User is in Admin Role, if not then ReadOnly = True
            For Each dr As DataRow In dt.Rows
                Dim wqId As Guid
                Dim wq As WorkQueue
                wqId = New Guid(CType(dr(EntityScheduleDAL.COL_NAME_ENTITY_ID), Byte()))
                wq = New WorkQueue(wqId)
                If (Not ElitaPlusIdentity.Current.ActiveUser.isInRole(wq.WorkQueue.AdminRole)) Then
                    State.NoAdminRoleWorkQueueName = wq.WorkQueue.Name
                    State.IsReadOnly = True
                    Return
                End If
            Next
        End Sub

#End Region

#Region "Page_Events"

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("GENERAL_INFORMATION") & _
                                          ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("SEARCH_SCHEDULE") & _
                                          ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("SCHEDULE_DETAILS")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("SCHEDULE_DETAILS")
            End If
        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear()
                If Not IsPostBack Then
                    If State.MyBO Is Nothing Then
                        State.MyBO = New Schedule
                    End If
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("TABLES")
                    UpdateBreadCrum()

                    State.PageIndex = 0

                    PopulateHeader()
                    PopulateGrid()
                    SetButtonsState()
                    SetLowerButtonsState()
                    'PopulateHeader determines if the Schedule Form is Read Only
                    If State.IsReadOnly Then
                        MasterPage.MessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_NO_ADMIN_ROLE_ON_WORKQUEUE)
                        MasterPage.MessageController.AddWarning(State.NoAdminRoleWorkQueueName)
                    End If
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
                CheckIfComingFromSaveConfirm()

                If Not Page.IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                End If

            Catch ex As Exception
                ' Clean Popup Input
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = ""
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub


#End Region

#Region " GridView Related "

        Private Sub PopulateGrid()
            Try
                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetDV()
                End If

                If State.IsAfterEditSave AndAlso State.IsAfterFinalSave Then
                    State.IsAfterEditSave = False
                    State.IsAfterFinalSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
                End If

                Grid.AutoGenerateColumns = False

                State.SortExpression = ScheduleDetail.ScheduleDetailSearchDV.COL_DAY_OF_WEEK_ID

                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyScheduleDetailChildBO, "DayOfWeekId", Grid.Columns(DAY_OF_WEEK_COL))
            BindBOPropertyToGridHeader(State.MyScheduleDetailChildBO, "FromTime", Grid.Columns(FROM_TIME_COL))
            BindBOPropertyToGridHeader(State.MyScheduleDetailChildBO, "ToTime", Grid.Columns(TO_TIME_COL))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
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

                If dvRow IsNot Nothing AndAlso (Not State.bnoRow) Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem OrElse itemType = ListItemType.EditItem Then
                        If (State.IsEditMode AndAlso _
                           State.Id.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_SCHEDULE_DETAIL_ID), Byte())))) Then
                            PopulateControlFromBOProperty(scheduleDetailLabel, dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_SCHEDULE_DETAIL_ID))
                            PopulateControlFromBOProperty(e.Row.Cells(SCHEDULE_ID_COL), dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_SCHEDULE_ID))

                            PopulateDayOfWeekDropdown(dayOfWeekList)
                            If State.DayofWeek IsNot Nothing Then
                                SetSelectedItemByText(dayOfWeekList, State.DayofWeek)
                            Else
                                dayOfWeekList.SelectedIndex = NO_ITEM_SELECTED_INDEX
                            End If

                            If State.MyScheduleDetailChildBO IsNot Nothing AndAlso dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_FROM_TIME) IsNot DBNull.Value _
                                AndAlso dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_TO_TIME) IsNot DBNull.Value Then
                                fromTimeTextBox.Text = CType(State.MyScheduleDetailChildBO.FromTime, Date).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                                toTimeTextBox.Text = CType(State.MyScheduleDetailChildBO.ToTime, Date).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                            Else
                                fromTimeTextBox.Text = ""
                                toTimeTextBox.Text = ""
                            End If
                        Else
                            PopulateControlFromBOProperty(scheduleDetailLabel, dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_SCHEDULE_DETAIL_ID))

                            dayOfWeekId = New Guid(CType(dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_DAY_OF_WEEK_ID), Byte()))
                            PopulateControlFromBOProperty(dayOfWeekLabel, LookupListNew.GetDescriptionFromId(LookupListCache.LK_DAYS_OF_WEEK, dayOfWeekId))

                            If dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_FROM_TIME) IsNot DBNull.Value Then
                                fromTime = CType(dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_FROM_TIME), Date).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                            Else
                                fromTime = ""
                            End If
                            PopulateControlFromBOProperty(e.Row.Cells(FROM_TIME_COL), fromTime)

                            If dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_TO_TIME) IsNot DBNull.Value Then
                                toTime = CType(dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_TO_TIME), Date).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                            Else
                                toTime = ""
                            End If
                            PopulateControlFromBOProperty(e.Row.Cells(TO_TIME_COL), toTime)

                            PopulateControlFromBOProperty(e.Row.Cells(SCHEDULE_ID_COL), dvRow(Schedule.ScheduleDetailSelectionView.COL_NAME_SCHEDULE_ID))

                            moEditImageButton = CType(e.Row.FindControl(EDIT_IMAGE_CONTROL_NAME), ImageButton)
                            moDeleteImageButton = CType(e.Row.FindControl(DELETE_IMAGE_CONTROL_NAME), ImageButton)
                            If (State.IsReadOnly) Then
                                ControlMgr.SetVisibleControl(Me, moEditImageButton, False)
                                ControlMgr.SetVisibleControl(Me, moDeleteImageButton, False)
                            End If

                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub RowCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND) Then
                    'Do the Edit here
                    index = CInt(e.CommandArgument)
                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True
                    State.DayofWeek = CType(Grid.Rows(index).Cells(DAY_OF_WEEK_COL).FindControl(DAY_OF_WEEK_LABEL_CONTROL), Label).Text
                    State.Id = New Guid(CType(Grid.Rows(index).Cells(SCHEDULE_DETAIL_ID_COL).FindControl(SCHEDULE_DETAIL_ID_CONTROL_NAME), Label).Text)

                    State.MyScheduleDetailChildBO = State.MyBO.GetScheduleDetailChild(State.Id)
                    State.MyScheduleDetailChildBO.BeginEdit()
                    PopulateGrid()
                    State.PageIndex = Grid.PageIndex
                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)

                    If Grid.SelectedRow.RowIndex >= 0 Then
                        'Set focus on the Description TextBox for the EditItemIndex row
                        SetFocusOnEditableFieldInGrid(Grid, DAY_OF_WEEK_COL, DAY_OF_WEEK_LIST_CONTROL, Grid.SelectedRow.RowIndex)
                    End If

                    If Grid.EditIndex >= 0 Then
                        AssignSelectedRecordFromBO()
                    End If

                    SetButtonsState()
                    SetLowerButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then

                    'Do the delete here
                    index = CInt(e.CommandArgument)

                    PopulateGrid()
                    State.PageIndex = Grid.PageIndex

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX
                    'Save the Id in the Session
                    State.Id = New Guid(CType(Grid.Rows(index).Cells(SCHEDULE_DETAIL_ID_COL).FindControl(SCHEDULE_DETAIL_ID_CONTROL_NAME), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                ElseIf (e.CommandName = SAVE_COMMAND) Then

                    State.DayofWeek = GetSelectedDescription(CType(GetSelectedGridControl(Grid, DAY_OF_WEEK_COL), DropDownList))

                    SaveRecord()

                    PopulateGrid()
                    State.PageIndex = Grid.PageIndex

                ElseIf (e.CommandName = CANCEL_COMMAND) Then
                    CancelRecord()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)

            If controlName = DAY_OF_WEEK_LIST_CONTROL Then
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
            State.PageIndex = Grid.PageIndex
            If (State.searchDV.Count = 0) Then
                State.bnoRow = True
                If State.MyScheduleDetailChildBO Is Nothing Then
                    State.MyScheduleDetailChildBO = State.MyBO.GetNewScheduleDetailChild
                    State.Id = State.MyScheduleDetailChildBO.Id
                End If

                State.searchDV = State.MyScheduleDetailChildBO.GetNewDataViewRow(State.searchDV, State.Id, State.MyScheduleDetailChildBO)

                Grid.DataSource = State.searchDV
                'Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Grid.DataBind()
                'TranslateGridHeader(Grid)
                'Me.TranslateGridControls(Grid)
                Grid.Rows(0).Visible = False
                ''BindBoPropertiesToGridHeaders()
            Else
                State.bnoRow = False
                Grid.DataSource = State.searchDV
                'HighLightSortColumn(Grid, Me.State.SortExpression)
                Grid.DataBind()
                If (State.searchDV.Count = 1) AndAlso State.AddingNewRow AndAlso State.Canceling Then
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

            State.searchDV = GetGridDataView()
            State.searchDV.Sort = Grid.DataMember()

            Return (State.searchDV)

        End Function

        Private Function GetGridDataView() As DataView
            Return State.MyBO.GetScheduleDetailSelectionView()
        End Function

        Private Sub Grid_Sorting(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                If State.SortExpression.StartsWith(e.SortExpression) Then
                    If State.SortExpression.EndsWith(" DESC") Then
                        State.SortExpression = e.SortExpression
                    Else
                        State.SortExpression &= " DESC"
                    End If
                Else
                    State.SortExpression = e.SortExpression
                End If
                State.Id = Guid.Empty
                State.PageIndex = 0

                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanging(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    Grid.PageIndex = State.PageIndex
                    PopulateGrid()
                    Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Helper Functions"

        Private Sub PopulateHeader()
            UpdateReadOnlyFlag()
            TranslateGridHeader(Grid)
            If (Not State.ScheduleId = Guid.Empty) AndAlso (State.MyBO IsNot Nothing) Then
                moScheduleCode.Enabled = False
                moScheduleCode.Text = State.MyBO.Code

                If Not State.IsReadOnly Then
                    moScheduleDescription.Enabled = True
                Else
                    moScheduleDescription.Enabled = False
                End If
                moScheduleDescription.Text = State.MyBO.Description
                'TranslateGridHeader(Grid)
            Else
                moScheduleCode.Enabled = True
                moScheduleCode.Text = String.Empty

                moScheduleDescription.Enabled = True
                moScheduleDescription.Text = String.Empty

                If State.Action <> COPY_SCHEDULE Then
                    State.Action = INIT_LOAD
                    State.IsEditMode = False
                    State.searchDV = Nothing
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub AddNew()

            Dim dv As DataView

            State.searchDV = GetGridDataView()
            If State.MyScheduleDetailChildBO Is Nothing Then
                State.MyScheduleDetailChildBO = State.MyBO.GetNewScheduleDetailChild
                State.Id = State.MyScheduleDetailChildBO.Id
            End If

            ' To ensure we begin and end edit
            State.MyScheduleDetailChildBO.BeginEdit()

            State.searchDV = State.MyScheduleDetailChildBO.GetNewDataViewRow(State.searchDV, State.Id, State.MyScheduleDetailChildBO)
            Grid.DataSource = State.searchDV

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)
            Grid.DataBind()

            State.PageIndex = Grid.PageIndex

            If State.bnoRow = True Then
                'ControlMgr.SetVisibleControl(Me, Grid1, False)
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            SetGridControls(Grid, False)

            If Grid.EditIndex >= 0 Then
                'Set focus on the Days of Week List Control for the EditItemIndex row
                SetFocusOnEditableFieldInGrid(Grid, DAY_OF_WEEK_COL, DAY_OF_WEEK_LIST_CONTROL, Grid.EditIndex)
                AssignSelectedRecordFromBO()
            End If

            TranslateGridControls(Grid)
            SetButtonsState()
            SetLowerButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Protected Sub PopulateBOfromForm()
            With State.MyBO
                .Code = moScheduleCode.Text
                .Description = moScheduleDescription.Text
            End With

        End Sub

        Protected Sub PopulateFormfromBO()
            'UpdateReadOnlyFlag()
            'PopulateScheduleDetailGrid()
            With State.MyBO
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
            Dim gridRowIdx As Integer = Grid.EditIndex
            Try
                With State.MyScheduleDetailChildBO
                    fromTimeText = CType(Grid.Rows(gridRowIdx).Cells(FROM_TIME_COL).FindControl(FROM_TIME_CONTROL_NAME), TextBox)
                    toTimeText = CType(Grid.Rows(gridRowIdx).Cells(TO_TIME_COL).FindControl(TO_TIME_CONTROL_NAME), TextBox)
                    dayOfWeekList = CType(Grid.Rows(gridRowIdx).Cells(DAY_OF_WEEK_COL).FindControl(DAY_OF_WEEK_LIST_CONTROL), DropDownList)
                    PopulateDayOfWeekDropdown(dayOfWeekList)

                    If State.DayofWeek IsNot Nothing Then
                        SetSelectedItemByText(dayOfWeekList, State.DayofWeek)
                    Else
                        dayOfWeekList.SelectedIndex = NO_ITEM_SELECTED_INDEX
                    End If

                    If (fromTimeText IsNot Nothing) AndAlso (.FromTime IsNot Nothing) AndAlso (.FromTime IsNot String.Empty) Then
                        fromTimeText.Text = CDate(.FromTime).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                    End If
                    If (toTimeText IsNot Nothing) AndAlso (.ToTime IsNot Nothing) AndAlso (.ToTime IsNot String.Empty) Then
                        toTimeText.Text = CDate(.ToTime).ToString(Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern)
                    End If

                    scheduleDetailIDLabel = CType(Grid.Rows(gridRowIdx).Cells(SCHEDULE_DETAIL_ID_COL).FindControl(SCHEDULE_DETAIL_ID_CONTROL_NAME), Label)
                    scheduleDetailIDLabel.Text = .Id.ToString

                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub AssignBOFromSelectedRecord(strFromTime As String, strToTime As String)

            State.MyScheduleDetailChildBO.FromTime = DateTime.Parse(String.Format("{0} {1}", DateTime.MinValue.ToString( _
                                                             Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern), strFromTime))
            State.MyScheduleDetailChildBO.ToTime = DateTime.Parse(String.Format("{0} {1}", DateTime.MinValue.ToString( _
                                                           Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern), strToTime))

            State.MyScheduleDetailChildBO.DayOfWeekId = GetSelectedItem(CType(GetSelectedGridControl(Grid, DAY_OF_WEEK_COL), DropDownList))

        End Sub

        Sub PopulateMyScheduleDetailChildBOFromDetail()
            Dim strFromTime As String
            Dim strToTime As String
            Try
                strFromTime = CType(GetSelectedGridControl(Grid, FROM_TIME_COL), TextBox).Text
                strToTime = CType(GetSelectedGridControl(Grid, TO_TIME_COL), TextBox).Text

                If Not System.Text.RegularExpressions.Regex.IsMatch(strFromTime, ElitaPlus.Common.RegExConstants.TIME_REGEX, _
                                                                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                    MasterPage.MessageController.Clear()
                    MasterPage.MessageController.AddWarning(String.Format("{0}: {1}", _
                                           TranslationBase.TranslateLabelOrMessage("FROM_TIME"), _
                                           TranslationBase.TranslateLabelOrMessage("FROM_TIME_INVALID"), False))
                    Exit Sub
                End If
                If Not System.Text.RegularExpressions.Regex.IsMatch(strToTime, ElitaPlus.Common.RegExConstants.TIME_REGEX, _
                                                                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                    MasterPage.MessageController.Clear()
                    MasterPage.MessageController.AddWarning(String.Format("{0}: {1}", _
                                           TranslationBase.TranslateLabelOrMessage("TO_TIME"), _
                                           TranslationBase.TranslateLabelOrMessage("TO_TIME_INVALID"), False))
                    Exit Sub
                End If

                AssignBOFromSelectedRecord(strFromTime, strToTime)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CancelRecord()
            Try
                ' To implement BeginEdit and CancelEdit
                State.MyScheduleDetailChildBO.cancelEdit()
                If State.MyScheduleDetailChildBO.IsSaveNew Then
                    State.MyScheduleDetailChildBO.Delete()
                    State.MyScheduleDetailChildBO.Save()
                End If
                State.MyScheduleDetailChildBO = Nothing

                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                If (State.AddingNewRow) Then
                    State.AddingNewRow = False
                    State.searchDV = Nothing
                End If

                ReturnFromEditing()
                SetLowerButtonsState()
                State.Canceling = False
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveRecord()

            Dim strFromTime As String
            Dim strToTime As String
            Try
                strFromTime = CType(GetSelectedGridControl(Grid, FROM_TIME_COL), TextBox).Text
                strToTime = CType(GetSelectedGridControl(Grid, TO_TIME_COL), TextBox).Text

                If Not System.Text.RegularExpressions.Regex.IsMatch(strFromTime, ElitaPlus.Common.RegExConstants.TIME_REGEX, _
                                                                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                    MasterPage.MessageController.Clear()
                    MasterPage.MessageController.AddWarning(String.Format("{0}: {1}", _
                                           TranslationBase.TranslateLabelOrMessage("FROM_TIME"), _
                                           TranslationBase.TranslateLabelOrMessage("FROM_TIME_INVALID"), False))
                    Exit Sub
                End If
                If Not System.Text.RegularExpressions.Regex.IsMatch(strToTime, ElitaPlus.Common.RegExConstants.TIME_REGEX, _
                                                                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
                    MasterPage.MessageController.Clear()
                    MasterPage.MessageController.AddWarning(String.Format("{0}: {1}", _
                                           TranslationBase.TranslateLabelOrMessage("TO_TIME"), _
                                           TranslationBase.TranslateLabelOrMessage("TO_TIME_INVALID"), False))
                    Exit Sub
                End If

                AssignBOFromSelectedRecord(strFromTime, strToTime)

                If State.MyScheduleDetailChildBO.IsDirty Then
                    State.MyScheduleDetailChildBO.Save()
                    State.MyScheduleDetailChildBO.EndEdit()
                    State.IsAfterEditSave = True
                    State.IsEditMode = False
                    State.AddingNewRow = False
                    State.bnoRow = False
                    State.searchDV = Nothing
                    State.Action = ""
                    State.MyScheduleDetailChildBO = Nothing
                End If
                SetLowerButtonsState()
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub DoDelete()

            State.MyScheduleDetailChildBO = State.MyBO.GetScheduleDetailChild(State.Id)
            Try
                State.MyScheduleDetailChildBO.Delete()
                State.MyScheduleDetailChildBO.Save()
                State.MyScheduleDetailChildBO.EndEdit()
                State.SelectedScheduleDetailChildId = Guid.Empty
                State.MyScheduleDetailChildBO = Nothing
            Catch ex As Exception
                State.MyBO.RejectChanges()
                Throw ex
            End Try

            State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterEditSave = True
            State.IsEditMode = False
            State.searchDV = Nothing
            SetLowerButtonsState()
            ReturnFromEditing()
            State.PageIndex = Grid.PageIndex
        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX
            If Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            State.IsEditMode = False
            State.searchDV = Nothing
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
            SetButtonsState()
        End Sub

        Private Sub SetButtonsState()

            If State.IsEditMode Then
                ControlMgr.SetEnableControl(Me, BtnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnBack, False)
                MenuEnabled = False
            ElseIf State.IsReadOnly Then
                ControlMgr.SetEnableControl(Me, BtnNew_WRITE, False)
                'Diable the Edit and Delete Buttons as disabled
            Else
                If State.Action <> COPY_SCHEDULE Then
                    ControlMgr.SetEnableControl(Me, BtnNew_WRITE, True)
                Else
                    ControlMgr.SetEnableControl(Me, BtnNew_WRITE, False)
                End If

                ControlMgr.SetEnableControl(Me, btnBack, True)
                MenuEnabled = True
            End If

        End Sub

        Private Sub SetLowerButtonsState()
            'Me.State.IsAfterSave
            If State.IsEditMode = True Then
                btnBack.Enabled = False
                btnApply_WRITE.Enabled = False
                btnButtomNew_WRITE.Enabled = False
                btnCopy_WRITE.Enabled = False
                btnUndo_WRITE.Enabled = False
            ElseIf State.IsReadOnly Then
                btnBack.Enabled = True
                btnApply_WRITE.Enabled = False
                btnButtomNew_WRITE.Enabled = False
                btnCopy_WRITE.Enabled = True
                btnUndo_WRITE.Enabled = False
            Else
                If State.Action = NEW_SCHEDULE OrElse State.Action = INIT_LOAD Then
                    btnBack.Enabled = True
                    btnApply_WRITE.Enabled = True
                    btnButtomNew_WRITE.Enabled = False
                    btnCopy_WRITE.Enabled = False
                    btnUndo_WRITE.Enabled = False
                ElseIf State.Action = COPY_SCHEDULE Then
                    btnBack.Enabled = False
                    btnApply_WRITE.Enabled = True
                    btnButtomNew_WRITE.Enabled = False
                    btnCopy_WRITE.Enabled = False
                    btnUndo_WRITE.Enabled = True
                ElseIf State.IsAfterEditSave = True Then
                    btnBack.Enabled = True
                    btnUndo_WRITE.Enabled = True
                    btnApply_WRITE.Enabled = True
                    btnButtomNew_WRITE.Enabled = False
                    btnCopy_WRITE.Enabled = False
                ElseIf State.IsAfterFinalSave = True Then
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
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDeletePromptResponse.Value = ""
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                'Me.State.searchDV = Nothing
                SetLowerButtonsState()
                ReturnFromEditing()
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenDeletePromptResponse.Value = ""
            End If
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()

            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                    State.MyBO.Save()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                        ReturnToCallingPage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        'Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                        ReturnToCallingPage()
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        If (State.IsEditMode) Then
                            PopulateMyScheduleDetailChildBOFromDetail()
                            State.MyScheduleDetailChildBO.Save()
                            State.MyScheduleDetailChildBO.EndEdit()
                            State.IsEditMode = False
                        End If
                        SetLowerButtonsState()
                        ReturnFromEditing()
                        'Me.EnableDisableFields()
                        'Me.PopulateScheduleDetailGrid()
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                        ReturnToCallingPage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        If (State.IsEditMode) Then
                            State.MyScheduleDetailChildBO.cancelEdit()
                            If State.MyScheduleDetailChildBO.IsSaveNew Then
                                State.MyScheduleDetailChildBO.Delete()
                                State.MyScheduleDetailChildBO.Save()
                            End If
                            State.IsEditMode = False
                        End If
                        SetLowerButtonsState()
                        ReturnFromEditing()
                        'Me.EnableDisableFields()
                        'Me.PopulateScheduleDetailGrid()
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub

#End Region

#Region "Button Click Handlers"

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew_WRITE.Click

            Try
                State.IsEditMode = True
                State.AddingNewRow = True
                ' '' ''Me.State.bnoRow = True
                AddNew()
                SetButtonsState()
                SetLowerButtonsState()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOfromForm()
                'Me.PopulateGrid()
                'Me.State.PageIndex = Grid.PageIndex
                If (Not State.bnoRow) AndAlso State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                Dim scheduleBO As New Schedule
                ReturnToCallingPage()
            End Try
        End Sub

        Private Sub btnButtomNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnButtomNew_WRITE.Click
            Try
                State.Action = NEW_SCHEDULE
                State.ScheduleId = Guid.Empty
                State.IsEditMode = False
                State.AddingNewRow = False
                State.searchDV = Nothing

                PopulateBOfromForm()

                If Not State.bnoRow AndAlso State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If

                PopulateGrid()
                PopulateHeader()
                SetButtonsState()
                SetLowerButtonsState()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            State.MyBO = New Schedule
            PopulateBOfromForm()
            'Me.EnableDisableFields()
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                State.Action = COPY_SCHEDULE
                State.CopyScheduleId = State.ScheduleId
                State.ScheduleId = Guid.Empty

                PopulateBOfromForm()
                If Not State.bnoRow AndAlso State.MyBO.IsDirty Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
                PopulateHeader()
                SetButtonsState()
                SetLowerButtonsState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub CreateNewWithCopy()

            PopulateBOfromForm()

            Dim newObj As New Schedule
            newObj.Copy(State.MyBO)

            State.MyBO = newObj
            PopulateFormfromBO()

            'Me.PopulateGrid()

            'Populate the Schedule Detail
            Dim dv As DataView = ScheduleDetail.LoadScheduleDetail(State.CopyScheduleId)
            Dim dt As DataTable = dv.Table
            For Each row As DataRow In dt.Rows
                State.MyScheduleDetailChildBO = State.MyBO.GetNewScheduleDetailChild
                State.Id = State.MyScheduleDetailChildBO.Id
                State.MyScheduleDetailChildBO.ScheduleId = State.MyBO.Id
                State.MyScheduleDetailChildBO.FromTime = CType(row(ScheduleDetail.ScheduleDetailSearchDV.COL_FROM_TIME), Date)
                State.MyScheduleDetailChildBO.ToTime = CType(row(ScheduleDetail.ScheduleDetailSearchDV.COL_TO_TIME), Date)
                State.MyScheduleDetailChildBO.DayOfWeekId = New Guid(CType(row(ScheduleDetail.ScheduleDetailSearchDV.COL_DAY_OF_WEEK_ID), Byte()))
                State.MyScheduleDetailChildBO.Save()
            Next

            State.searchDV = Nothing
            PopulateGrid()

        End Sub

        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                PopulateBOfromForm()
                ' We are enforcing to save the schedule Detail before saving the Schedule
                If State.bnoRow AndAlso State.MyScheduleDetailChildBO IsNot Nothing Then
                    If Not State.MyScheduleDetailChildBO.IsValid Then
                        State.MyScheduleDetailChildBO.BeginEdit()
                        State.MyScheduleDetailChildBO.Delete()
                        State.MyScheduleDetailChildBO.Save()
                        State.MyScheduleDetailChildBO.EndEdit()
                        State.MyScheduleDetailChildBO = Nothing
                    End If
                End If
                If State.MyBO.IsDirty AndAlso (State.MyScheduleDetailChildBO Is Nothing OrElse State.CopyScheduleId <> Guid.Empty) Then
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    PopulateFormfromBO()

                    State.IsEditMode = False
                    State.IsAfterFinalSave = True
                    If State.Action = COPY_SCHEDULE Then
                        MasterPage.MessageController.AddSuccess(MSG_RECORD_COPIED_OK, True)
                    Else
                        MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                    End If
                    State.Action = ""
                    State.CopyScheduleId = Guid.Empty 'To Clear the last Copy performed
                    State.ScheduleId = State.MyBO.Id
                    State.searchDV = Nothing
                    PopulateHeader()
                Else
                    MasterPage.MessageController.AddInformation(MSG_RECORD_NOT_SAVED, True)
                End If
                ReturnFromEditing()
                SetLowerButtonsState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New Schedule(State.MyBO.Id)
                ElseIf Not State.CopyScheduleId = Guid.Empty Then
                    'It was a new with copy
                    State.MyBO = New Schedule(State.CopyScheduleId)
                    State.ScheduleId = State.CopyScheduleId
                    State.CopyScheduleId = Guid.Empty 'To Clear the last Copy performed
                    State.Action = ""

                    If State.MyScheduleDetailChildBO IsNot Nothing Then
                        State.MyScheduleDetailChildBO = Nothing
                    End If
                    If Not State.Id = Guid.Empty Then
                        State.Id = Guid.Empty
                    End If
                Else
                    CreateNew()
                End If
                PopulateFormfromBO()
                SetLowerButtonsState()
                State.searchDV = Nothing
                PopulateHeader()
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class
End Namespace