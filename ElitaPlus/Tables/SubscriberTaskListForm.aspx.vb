Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables
    Partial Public Class SubscriberTaskListForm
        Inherits ElitaPlusSearchPage

#Region "Bread Crum"
        Private Sub UpdateBreadCrum()
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub
#End Region

#Region "Constants"
        Public Const URL As String = "Tables/SubscriberTaskListForm.aspx"
        Public Const PAGETITLE As String = "SUBSCRIBER_TASK"
        Public Const PAGETAB As String = "ADMIN"
        Public Const SUMMARYTITLE As String = "SEARCH"

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const GRID_COL_SUBSCRIBER_TASK_ID_IDX As Integer = 0
        Private Const GRID_COL_TASK_IDX As Integer = 1
        Private Const GRID_COL_SUBSCRIBER_TYPE_IDX As Integer = 2
        Private Const GRID_COL_SUBSCRIBER_STATUS_IDX As Integer = 3
        Private Const GRID_COL_EDIT_IDX As Integer = 4
        Private Const GRID_COL_DELETE_IDX As Integer = 5


        Private Const GRID_CTRL_NAME_LABLE_SUBSCRIBER_TASK_ID As String = "lblSubscriberTaskID"
        Private Const GRID_CTRL_NAME_LABLE_TASK As String = "lblTask"
        Private Const GRID_CTRL_NAME_LABEL_SUBSCRIBER_TYPE As String = "lblSubscriberType"
        Private Const GRID_CTRL_NAME_LABLE_SUBSCRIBER_STATUS As String = "lblSubscriberStatus"

        Private Const GRID_CTRL_NAME_EDIT_TASK As String = "ddlTask"
        Private Const GRID_CTRL_NAME_EDIT_SUBSCRIBER_TYPE As String = "ddlSubscriberType"
        Private Const GRID_CTRL_NAME_EDIT_SUBSCRIBER_STATUS As String = "ddlSubscriberStatus"

        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Page State"
        ' This class keeps the current state for the search page.
        Class MyState
            Public MyBO As SubscriberTask
            Public SubscriberTaskID As Guid
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public searchDV As SubscriberTask.SubscriberTaskSearchDV = Nothing
            Public HasDataChanged As Boolean
            Public IsGridAddNew As Boolean = False
            Public IsEditMode As Boolean = False
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public SortExpression As String = SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_TYPE_DESC
            Public searchTask As Guid = Guid.Empty
            Public searchSubscriberType As Guid = Guid.Empty
            Public searchSubscriberStatus As Guid = Guid.Empty
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Public ReadOnly Property IsGridInEditMode() As Boolean
            Get
                Return Grid.EditIndex > NO_ITEM_SELECTED_INDEX
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                If ViewState("SortDirection") IsNot Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If

            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
#End Region

#Region "Page Events"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            MasterPage.MessageController.Clear()
            Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not IsPostBack Then
                    UpdateBreadCrum()
                    ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                    SetControlState()
                    State.PageIndex = 0
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    PopulateSearchControls()
                Else
                    CheckIfComingFromDeleteConfirm()
                    BindBoPropertiesToGridHeaders()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
#End Region

#Region "Control Handler"
        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try
                ddlSearchTask.SelectedIndex = -1
                ddlSearchSubscriberStatus.SelectedIndex = -1
                ddlSearchSubscriberType.SelectedIndex = -1

                Grid.EditIndex = NO_ITEM_SELECTED_INDEX

                With State
                    .IsGridAddNew = False
                    .SubscriberTaskID = Guid.Empty
                    .searchTask = Guid.Empty
                    .searchSubscriberType = Guid.Empty
                    .searchSubscriberStatus = Guid.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                With State
                    .PageIndex = 0
                    .SubscriberTaskID = Guid.Empty
                    .IsGridVisible = True
                    .searchDV = Nothing
                    .HasDataChanged = False
                    .IsGridAddNew = False
                    'get search control value
                    .searchTask = GetSelectedItem(ddlSearchTask)
                    .searchSubscriberType = GetSelectedItem(ddlSearchSubscriberType)
                    .searchSubscriberStatus = GetSelectedItem(ddlSearchSubscriberStatus)
                End With
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.IsGridAddNew = True
                AddNew()
                SetControlState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub AddNew()
            If State.MyBO Is Nothing OrElse State.MyBO.IsNew = False Then
                State.MyBO = New SubscriberTask
                State.MyBO.AddNewRowToSearchDV(State.searchDV, State.MyBO)
            End If
            State.SubscriberTaskID = State.MyBO.Id
            State.IsGridAddNew = True
            PopulateGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.SubscriberTaskID, Grid,
                                               State.PageIndex, State.IsEditMode)
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            SetGridControls(Grid, False)
        End Sub

        Protected Sub btnSave_Click(sender As Object, e As EventArgs)

            Try
                PopulateBOFromForm()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    State.IsGridAddNew = False
                    MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
            Try
                With State
                    If .IsGridAddNew Then
                        RemoveNewRowFromSearchDV()
                        .IsGridAddNew = False
                        Grid.PageIndex = .PageIndex
                    End If
                    .SubscriberTaskID = Guid.Empty
                    State.MyBO = Nothing
                    .IsEditMode = False
                End With
                Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                'Me.State.searchDV = Nothing
                PopulateGrid()
                SetControlState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Helper functions"
        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            'Do the delete here
            State.ActionInProgress = DetailPageCommand.Nothing_
            'Save the RiskTypeId in the Session

            Dim obj As SubscriberTask = New SubscriberTask(State.SubscriberTaskID)

            obj.Delete()

            'Call the Save() method in the Role Business Object here

            obj.Save()

            MasterPage.MessageController.AddSuccess(MSG_RECORD_DELETED_OK, True)

            State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
            State.IsEditMode = False
            SetControlState()
        End Sub

        Private Sub SetControlState()
            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, btnNew, False)
                ControlMgr.SetEnableControl(Me, btnSearch, False)
                ControlMgr.SetEnableControl(Me, btnClearSearch, False)
                MenuEnabled = False
                If (cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnNew, True)
                ControlMgr.SetEnableControl(Me, btnSearch, True)
                ControlMgr.SetEnableControl(Me, btnClearSearch, True)
                MenuEnabled = True
                If Not (cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "TaskId", Grid.Columns(GRID_COL_TASK_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "SubscriberTypeId", Grid.Columns(GRID_COL_SUBSCRIBER_TYPE_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "SubscriberStatusId", Grid.Columns(GRID_COL_SUBSCRIBER_STATUS_IDX))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Grid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
            SetControlState()
        End Sub

        Private Sub RemoveNewRowFromSearchDV()
            Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
            With State
                If .searchDV IsNot Nothing Then
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, .SubscriberTaskID)
                End If
            End With
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub

        Private Function PopulateBOFromForm() As Boolean
            Dim objDDL As DropDownList
            With State.MyBO
                objDDL = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_TASK_IDX).FindControl(GRID_CTRL_NAME_EDIT_TASK), DropDownList)
                PopulateBOProperty(State.MyBO, "TaskId", objDDL)
                objDDL = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_SUBSCRIBER_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_SUBSCRIBER_TYPE), DropDownList)
                PopulateBOProperty(State.MyBO, "SubscriberTypeId", objDDL)
                objDDL = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_SUBSCRIBER_STATUS_IDX).FindControl(GRID_CTRL_NAME_EDIT_SUBSCRIBER_STATUS), DropDownList)
                PopulateBOProperty(State.MyBO, "SubscriberStatusId", objDDL)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Function

        Protected Sub PopulateSearchControls()
            Try
                Dim dv As DataView = Task.getList(String.Empty, String.Empty)
                ' BindListControlToDataView(ddlSearchTask, dv, Task.TaskSearchDV.COL_DESCRIPTION, Task.TaskSearchDV.COL_TASK_ID, True)
                Dim listLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.GetTaskList, Thread.CurrentPrincipal.GetLanguageCode())
                'Dim filteredList As ListItem() = (From x In listLkl
                '                                  Where x.Code = String.Empty And x.Translation = String.Empty
                '                                  Select x).ToArray()
                ddlSearchTask.Populate(listLkl, New PopulateOptions() With
                  {
                   .AddBlankItem = True
                   })
                ' Me.BindListControlToDataView(Me.ddlSearchSubscriberType, LookupListNew.DropdownLookupList("SUBSCRIBER_TYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                ddlSearchSubscriberType.Populate(CommonConfigManager.Current.ListManager.GetList("SUBSCRIBER_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                .AddBlankItem = True
               })
                'Me.BindListControlToDataView(Me.ddlSearchSubscriberStatus, LookupListNew.DropdownLookupList("EVNT_SUB_STAT", ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                ddlSearchSubscriberStatus.Populate(CommonConfigManager.Current.ListManager.GetList("EVNT_SUB_STAT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                .AddBlankItem = True
               })

                If State.searchTask <> Guid.Empty Then
                    SetSelectedItem(ddlSearchTask, State.searchTask)
                End If
                If State.searchSubscriberType <> Guid.Empty Then
                    SetSelectedItem(ddlSearchSubscriberType, State.searchSubscriberType)
                End If
                If State.searchSubscriberStatus <> Guid.Empty Then
                    SetSelectedItem(ddlSearchSubscriberStatus, State.searchSubscriberStatus)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Grid related"

        Public Sub PopulateGrid()
            Dim blnNewSearch As Boolean = False
            Dim dv As DataView
            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                With State
                    If (.searchDV Is Nothing) Then
                        .searchDV = SubscriberTask.getList(.searchTask, .searchSubscriberType, .searchSubscriberStatus)
                        blnNewSearch = True
                    End If
                End With

                State.searchDV.Sort = SortDirection
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.SubscriberTaskID, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.SubscriberTaskID, Grid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
                End If

                Grid.AutoGenerateColumns = False
                Grid.Columns(GRID_COL_TASK_IDX).SortExpression = SubscriberTask.SubscriberTaskSearchDV.COL_TASK_CODE
                Grid.Columns(GRID_COL_SUBSCRIBER_TYPE_IDX).SortExpression = SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_TYPE_DESC
                Grid.Columns(GRID_COL_SUBSCRIBER_STATUS_IDX).SortExpression = SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_STATUS_DESC
                SortAndBindGrid(blnNewSearch)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

            TranslateGridControls(Grid)

            If (State.searchDV.Count = 0) Then
                State.searchDV = Nothing
                State.MyBO = New SubscriberTask
                SubscriberTask.AddNewRowToSearchDV(State.searchDV, State.MyBO)
                Grid.DataSource = State.searchDV
                Grid.DataBind()
                Grid.Rows(0).Visible = False
                State.IsGridAddNew = True
                State.IsGridVisible = False
                If blnShowErr Then
                    MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                Grid.Enabled = True
                Grid.PageSize = State.PageSize
                Grid.DataSource = State.searchDV
                State.IsGridVisible = True
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
            End If


            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, moSearchResults, State.IsGridVisible)

            Session("recCount") = State.searchDV.Count

            If Grid.Visible Then
                If (State.IsGridAddNew) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

        End Sub

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = Grid.PageIndex
                    State.SubscriberTaskID = Guid.Empty
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")


                If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If SortDirection.EndsWith(" ASC") Then
                        SortDirection = e.SortExpression + " DESC"
                    Else
                        SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If

                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim strID As String

                If dvRow IsNot Nothing Then
                    strID = GetGuidStringFromByteArray(CType(dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_TASK_ID), Byte()))
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(GRID_COL_SUBSCRIBER_TASK_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_SUBSCRIBER_TASK_ID), Label).Text = strID

                        If (State.IsEditMode = True AndAlso State.SubscriberTaskID.ToString.Equals(strID)) Then
                            Dim objDDL As DropDownList
                            Dim dv As DataView
                            Dim guidVal As Guid

                            'populate task dropdown
                            objDDL = CType(e.Row.Cells(GRID_COL_TASK_IDX).FindControl(GRID_CTRL_NAME_EDIT_TASK), DropDownList)
                            dv = Task.getList(String.Empty, String.Empty)
                            ' Me.BindListControlToDataView(objDDL, dv, Task.TaskSearchDV.COL_DESCRIPTION, Task.TaskSearchDV.COL_TASK_ID, True) 'Task
                            Dim listLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.GetTaskList, Thread.CurrentPrincipal.GetLanguageCode())
                            'Dim filteredList As ListItem() = (From x In listLkl
                            '                                  Where x.Code = String.Empty And x.Translation = String.Empty
                            '                                  Select x).ToArray()
                            objDDL.Populate(listLkl, New PopulateOptions() With
                             {
                             .AddBlankItem = True
                             })
                            guidVal = New Guid(CType(dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_TASK_ID), Byte()))
                            SetSelectedItem(objDDL, guidVal)

                            'populate subscriber type dropdown
                            objDDL = CType(e.Row.Cells(GRID_COL_SUBSCRIBER_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_SUBSCRIBER_TYPE), DropDownList)
                            ' dv = LookupListNew.DropdownLookupList("SUBSCRIBER_TYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            'Me.BindListControlToDataView(objDDL, dv, , , True)
                            objDDL.Populate(CommonConfigManager.Current.ListManager.GetList("SUBSCRIBER_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                             {
                            .AddBlankItem = True
                              })
                            guidVal = New Guid(CType(dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_TYPE_ID), Byte()))
                            SetSelectedItem(objDDL, guidVal)

                            'populate subscriber status dropdown
                            objDDL = CType(e.Row.Cells(GRID_COL_SUBSCRIBER_STATUS_IDX).FindControl(GRID_CTRL_NAME_EDIT_SUBSCRIBER_STATUS), DropDownList)
                            'dv = LookupListNew.DropdownLookupList("EVNT_SUB_STAT", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            'Me.BindListControlToDataView(objDDL, dv, , , True)
                            objDDL.Populate(CommonConfigManager.Current.ListManager.GetList("EVNT_SUB_STAT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                             {
                            .AddBlankItem = True
                              })
                            guidVal = New Guid(CType(dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_STATUS_ID), Byte()))
                            SetSelectedItem(objDDL, guidVal)

                        Else
                            CType(e.Row.Cells(GRID_COL_TASK_IDX).FindControl(GRID_CTRL_NAME_LABLE_TASK), Label).Text = dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_TASK_CODE).ToString & "-" & dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_TASK_DESC).ToString
                            CType(e.Row.Cells(GRID_COL_SUBSCRIBER_TYPE_IDX).FindControl(GRID_CTRL_NAME_LABEL_SUBSCRIBER_TYPE), Label).Text = dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_TYPE_DESC).ToString
                            CType(e.Row.Cells(GRID_COL_SUBSCRIBER_STATUS_IDX).FindControl(GRID_CTRL_NAME_LABLE_SUBSCRIBER_STATUS), Label).Text = dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_STATUS_DESC).ToString

                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer
                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.SubscriberTaskID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_SUBSCRIBER_TASK_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_SUBSCRIBER_TASK_ID), Label).Text)
                    State.MyBO = New SubscriberTask(State.SubscriberTaskID)

                    PopulateGrid()

                    State.PageIndex = Grid.PageIndex

                    SetControlState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.SubscriberTaskID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_SUBSCRIBER_TASK_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_SUBSCRIBER_TASK_ID), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
    End Class
End Namespace