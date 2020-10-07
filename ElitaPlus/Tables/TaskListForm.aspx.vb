Namespace Tables

    Partial Public Class TaskListForm
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
        Public Const URL As String = "Tables/TaskListForm.aspx"
        Public Const PAGETITLE As String = "TASK"
        Public Const PAGETAB As String = "ADMIN"
        Public Const SUMMARYTITLE As String = "SEARCH"

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const GRID_COL_TASK_ID_IDX As Integer = 0
        Private Const GRID_COL_CODE_IDX As Integer = 1
        Private Const GRID_COL_DESC_IDX As Integer = 2
        Private Const GRID_COL_RETRY_COUNT_IDX As Integer = 3
        Private Const GRID_COL_RETRY_DELAY_IDX As Integer = 4
        Private Const GRID_COL_TIMEOUT_IDX As Integer = 5
        Private Const GRID_COL_PARAMETERS_IDX As Integer = 6
        Private Const GRID_COL_EDIT_IDX As Integer = 7
        Private Const GRID_COL_DELETE_IDX As Integer = 8


        Private Const GRID_CTRL_NAME_LABLE_TASK_ID As String = "lblTaskID"
        Private Const GRID_CTRL_NAME_LABLE_CODE As String = "lblCode"
        Private Const GRID_CTRL_NAME_LABEL_DESC As String = "lblDesc"
        Private Const GRID_CTRL_NAME_LABLE_RETRY_COUNT As String = "lblRetryCnt"
        Private Const GRID_CTRL_NAME_LABLE_RETRY_DELAY As String = "lblRetryDelay"
        Private Const GRID_CTRL_NAME_LABLE_TIMEOUT As String = "lblTimeOut"
        Private Const GRID_CTRL_NAME_LABLE_PARAMETERS As String = "lblParams"

        Private Const GRID_CTRL_NAME_EDIT_CODE As String = "txtCode"
        Private Const GRID_CTRL_NAME_EDIT_DESC As String = "txtDesc"
        Private Const GRID_CTRL_NAME_EDIT_RETRY_COUNT As String = "txtRetryCnt"
        Private Const GRID_CTRL_NAME_EDIT_RETRY_DELAY As String = "txtRetryDelay"
        Private Const GRID_CTRL_NAME_EDIT_TIMEOUT As String = "txtTimeOut"
        Private Const GRID_CTRL_NAME_EDIT_PARAMETERS As String = "txtParams"

        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Page State"
        ' This class keeps the current state for the search page.
        Class MyState
            Public MyBO As Task
            Public TaskID As Guid
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public searchDV As Task.TaskSearchDV = Nothing
            Public HasDataChanged As Boolean
            Public IsGridAddNew As Boolean = False
            Public IsEditMode As Boolean = False
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public SortExpression As String = Task.TaskSearchDV.COL_CODE
            Public searchCode As String = ""
            Public searchDesc As String = ""
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
                    'SetControlState()
                    State.PageIndex = 0
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
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

            Dim obj As Task = New Task(State.TaskID)

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
            BindBOPropertyToGridHeader(State.MyBO, "Code", Grid.Columns(GRID_COL_CODE_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "Description", Grid.Columns(GRID_COL_DESC_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "RetryCount", Grid.Columns(GRID_COL_RETRY_COUNT_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "RetryDelaySeconds", Grid.Columns(GRID_COL_RETRY_DELAY_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "TimeoutSeconds", Grid.Columns(GRID_COL_TIMEOUT_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "TaskParameters", Grid.Columns(GRID_COL_PARAMETERS_IDX))
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
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, .TaskID)
                End If
            End With
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub

        Private Function PopulateBOFromForm() As Boolean
            Dim objtxt As TextBox
            With State.MyBO
                objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox)
                objtxt.Text = objtxt.Text.ToUpper
                PopulateBOProperty(State.MyBO, "Code", objtxt)
                objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_DESC_IDX).FindControl(GRID_CTRL_NAME_EDIT_DESC), TextBox)
                PopulateBOProperty(State.MyBO, "Description", objtxt)
                objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_PARAMETERS_IDX).FindControl(GRID_CTRL_NAME_EDIT_PARAMETERS), TextBox)
                PopulateBOProperty(State.MyBO, "TaskParameters", objtxt)
                objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_RETRY_COUNT_IDX).FindControl(GRID_CTRL_NAME_EDIT_RETRY_COUNT), TextBox)
                PopulateBOProperty(State.MyBO, "RetryCount", objtxt)                
                objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_RETRY_DELAY_IDX).FindControl(GRID_CTRL_NAME_EDIT_RETRY_DELAY), TextBox)
                PopulateBOProperty(State.MyBO, "RetryDelaySeconds", objtxt)
                objtxt = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_TIMEOUT_IDX).FindControl(GRID_CTRL_NAME_EDIT_TIMEOUT), TextBox)
                PopulateBOProperty(State.MyBO, "TimeoutSeconds", objtxt)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Function
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
                        .searchDV = Task.getList(.searchCode, .searchDesc)
                        blnNewSearch = True
                    End If
                End With

                State.searchDV.Sort = SortDirection
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.TaskID, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.TaskID, Grid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
                End If

                Grid.AutoGenerateColumns = False
                Grid.Columns(GRID_COL_CODE_IDX).SortExpression = Task.TaskSearchDV.COL_CODE
                Grid.Columns(GRID_COL_DESC_IDX).SortExpression = Task.TaskSearchDV.COL_DESCRIPTION
                SortAndBindGrid(blnNewSearch)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

            TranslateGridControls(Grid)

            If (State.searchDV.Count = 0) Then
                State.searchDV = Nothing
                State.MyBO = New Task
                State.MyBO.AddNewRowToSearchDV(State.searchDV, State.MyBO)
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
                    State.TaskID = Guid.Empty
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
                    strID = GetGuidStringFromByteArray(CType(dvRow(Task.TaskSearchDV.COL_TASK_ID), Byte()))
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(GRID_COL_TASK_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_TASK_ID), Label).Text = strID

                        If (State.IsEditMode = True AndAlso State.TaskID.ToString.Equals(strID)) Then
                            CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox).Text = dvRow(Task.TaskSearchDV.COL_CODE).ToString
                            If (State.IsGridAddNew) Then
                                CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox).ReadOnly = False
                            Else
                                CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox).ReadOnly = True
                            End If
                            CType(e.Row.Cells(GRID_COL_DESC_IDX).FindControl(GRID_CTRL_NAME_EDIT_DESC), TextBox).Text = dvRow(Task.TaskSearchDV.COL_DESCRIPTION).ToString
                            CType(e.Row.Cells(GRID_COL_RETRY_COUNT_IDX).FindControl(GRID_CTRL_NAME_EDIT_RETRY_COUNT), TextBox).Text = dvRow(Task.TaskSearchDV.COL_RETRY_COUNT).ToString
                            CType(e.Row.Cells(GRID_COL_RETRY_DELAY_IDX).FindControl(GRID_CTRL_NAME_EDIT_RETRY_DELAY), TextBox).Text = dvRow(Task.TaskSearchDV.COL_RETRY_DELAY).ToString
                            CType(e.Row.Cells(GRID_COL_TIMEOUT_IDX).FindControl(GRID_CTRL_NAME_EDIT_TIMEOUT), TextBox).Text = dvRow(Task.TaskSearchDV.COL_TIMEOUT).ToString
                            CType(e.Row.Cells(GRID_COL_PARAMETERS_IDX).FindControl(GRID_CTRL_NAME_EDIT_PARAMETERS), TextBox).Text = dvRow(Task.TaskSearchDV.COL_TASK_PARAMS).ToString

                        Else
                            CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_LABLE_CODE), Label).Text = dvRow(Task.TaskSearchDV.COL_CODE).ToString
                            CType(e.Row.Cells(GRID_COL_DESC_IDX).FindControl(GRID_CTRL_NAME_LABEL_DESC), Label).Text = dvRow(Task.TaskSearchDV.COL_DESCRIPTION).ToString
                            CType(e.Row.Cells(GRID_COL_RETRY_COUNT_IDX).FindControl(GRID_CTRL_NAME_LABLE_RETRY_COUNT), Label).Text = dvRow(Task.TaskSearchDV.COL_RETRY_COUNT).ToString
                            CType(e.Row.Cells(GRID_COL_RETRY_DELAY_IDX).FindControl(GRID_CTRL_NAME_LABLE_RETRY_DELAY), Label).Text = dvRow(Task.TaskSearchDV.COL_RETRY_DELAY).ToString
                            CType(e.Row.Cells(GRID_COL_TIMEOUT_IDX).FindControl(GRID_CTRL_NAME_LABLE_TIMEOUT), Label).Text = dvRow(Task.TaskSearchDV.COL_TIMEOUT).ToString
                            CType(e.Row.Cells(GRID_COL_PARAMETERS_IDX).FindControl(GRID_CTRL_NAME_LABLE_PARAMETERS), Label).Text = dvRow(Task.TaskSearchDV.COL_TASK_PARAMS).ToString
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

                    State.TaskID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_TASK_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_TASK_ID), Label).Text)
                    State.MyBO = New Task(State.TaskID)

                    PopulateGrid()

                    State.PageIndex = Grid.PageIndex

                    SetControlState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.TaskID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_TASK_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_TASK_ID), Label).Text)
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

#Region "Control Handler"
        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try
                txtSearchCode.Text = String.Empty
                txtSearchDesc.Text = String.Empty
                Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                
                With State
                    .IsGridAddNew = False
                    .TaskID = Guid.Empty
                    .MyBO = Nothing
                    .searchCode = String.Empty
                    .searchDesc = String.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                With State
                    .PageIndex = 0
                    .TaskID = Guid.Empty
                    .MyBO = Nothing
                    .IsGridVisible = True
                    .searchDV = Nothing
                    .HasDataChanged = False
                    .IsGridAddNew = False
                    'get search control value
                    .searchCode = txtSearchCode.Text.Trim
                    .searchDesc = txtSearchDesc.Text.Trim
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
                State.MyBO = New Task
                State.MyBO.AddNewRowToSearchDV(State.searchDV, State.MyBO)
            End If
            State.TaskID = State.MyBO.Id
            State.IsGridAddNew = True
            PopulateGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.TaskID, Grid, _
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
                    State.MyBO = Nothing
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
                    .TaskID = Guid.Empty
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
    End Class

End Namespace