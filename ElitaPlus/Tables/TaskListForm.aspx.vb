Namespace Tables

    Partial Public Class TaskListForm
        Inherits ElitaPlusSearchPage

#Region "Bread Crum"
        Private Sub UpdateBreadCrum()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
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
                Return Me.Grid.EditIndex > Me.NO_ITEM_SELECTED_INDEX
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                If Not ViewState("SortDirection") Is Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If

            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
#End Region

#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.MasterPage.MessageController.Clear()
            Me.Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not Me.IsPostBack Then
                    UpdateBreadCrum()
                    ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                    'SetControlState()
                    Me.State.PageIndex = 0
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                Else
                    CheckIfComingFromDeleteConfirm()
                    BindBoPropertiesToGridHeaders()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub
#End Region

#Region "Helper functions"
        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            'Do the delete here
            Me.State.ActionInProgress = DetailPageCommand.Nothing_
            'Save the RiskTypeId in the Session

            Dim obj As Task = New Task(Me.State.TaskID)

            obj.Delete()

            'Call the Save() method in the Role Business Object here

            obj.Save()

            Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_DELETED_OK, True)

            Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            Me.State.IsEditMode = False
            SetControlState()
        End Sub

        Private Sub SetControlState()
            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, btnNew, False)
                ControlMgr.SetEnableControl(Me, btnSearch, False)
                ControlMgr.SetEnableControl(Me, btnClearSearch, False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnNew, True)
                ControlMgr.SetEnableControl(Me, btnSearch, True)
                ControlMgr.SetEnableControl(Me, btnClearSearch, True)
                Me.MenuEnabled = True
                If Not (Me.cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
                End If
            End If
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Code", Me.Grid.Columns(Me.GRID_COL_CODE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Description", Me.Grid.Columns(Me.GRID_COL_DESC_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "RetryCount", Me.Grid.Columns(Me.GRID_COL_RETRY_COUNT_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "RetryDelaySeconds", Me.Grid.Columns(Me.GRID_COL_RETRY_DELAY_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "TimeoutSeconds", Me.Grid.Columns(Me.GRID_COL_TIMEOUT_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "TaskParameters", Me.Grid.Columns(Me.GRID_COL_PARAMETERS_IDX))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Me.Grid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            SetControlState()
        End Sub

        Private Sub RemoveNewRowFromSearchDV()
            Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
            With State
                If Not .searchDV Is Nothing Then
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, .TaskID)
                End If
            End With
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub

        Private Function PopulateBOFromForm() As Boolean
            Dim objtxt As TextBox
            With Me.State.MyBO
                objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox)
                objtxt.Text = objtxt.Text.ToUpper
                PopulateBOProperty(State.MyBO, "Code", objtxt)
                objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_DESC_IDX).FindControl(GRID_CTRL_NAME_EDIT_DESC), TextBox)
                PopulateBOProperty(State.MyBO, "Description", objtxt)
                objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_PARAMETERS_IDX).FindControl(GRID_CTRL_NAME_EDIT_PARAMETERS), TextBox)
                PopulateBOProperty(State.MyBO, "TaskParameters", objtxt)
                objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_RETRY_COUNT_IDX).FindControl(GRID_CTRL_NAME_EDIT_RETRY_COUNT), TextBox)
                PopulateBOProperty(State.MyBO, "RetryCount", objtxt)                
                objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_RETRY_DELAY_IDX).FindControl(GRID_CTRL_NAME_EDIT_RETRY_DELAY), TextBox)
                PopulateBOProperty(State.MyBO, "RetryDelaySeconds", objtxt)
                objtxt = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_TIMEOUT_IDX).FindControl(GRID_CTRL_NAME_EDIT_TIMEOUT), TextBox)
                PopulateBOProperty(State.MyBO, "TimeoutSeconds", objtxt)
            End With
            If Me.ErrCollection.Count > 0 Then
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

                Me.State.searchDV.Sort = Me.SortDirection
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.TaskID, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.TaskID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
                End If

                Me.Grid.AutoGenerateColumns = False
                Me.Grid.Columns(Me.GRID_COL_CODE_IDX).SortExpression = Task.TaskSearchDV.COL_CODE
                Me.Grid.Columns(Me.GRID_COL_DESC_IDX).SortExpression = Task.TaskSearchDV.COL_DESCRIPTION
                SortAndBindGrid(blnNewSearch)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

            Me.TranslateGridControls(Grid)

            If (Me.State.searchDV.Count = 0) Then
                Me.State.searchDV = Nothing
                Me.State.MyBO = New Task
                State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
                Me.Grid.DataSource = Me.State.searchDV
                Me.Grid.DataBind()
                Me.Grid.Rows(0).Visible = False
                Me.State.IsGridAddNew = True
                Me.State.IsGridVisible = False
                If blnShowErr Then
                    Me.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                Me.Grid.Enabled = True
                Me.Grid.PageSize = Me.State.PageSize
                Me.Grid.DataSource = Me.State.searchDV
                Me.State.IsGridVisible = True
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()
            End If


            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.Grid.Visible Then
                If (Me.State.IsGridAddNew) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

        End Sub

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = Grid.PageIndex
                    Me.State.TaskID = Guid.Empty
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")


                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If

                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim strID As String
                
                If Not dvRow Is Nothing Then
                    strID = GetGuidStringFromByteArray(CType(dvRow(Task.TaskSearchDV.COL_TASK_ID), Byte()))
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(Me.GRID_COL_TASK_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_TASK_ID), Label).Text = strID

                        If (Me.State.IsEditMode = True AndAlso Me.State.TaskID.ToString.Equals(strID)) Then
                            CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_CODE), TextBox).Text = dvRow(Task.TaskSearchDV.COL_CODE).ToString
                            If (Me.State.IsGridAddNew) Then
                                CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_CODE), TextBox).ReadOnly = False
                            Else
                                CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_CODE), TextBox).ReadOnly = True
                            End If
                            CType(e.Row.Cells(Me.GRID_COL_DESC_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DESC), TextBox).Text = dvRow(Task.TaskSearchDV.COL_DESCRIPTION).ToString
                            CType(e.Row.Cells(Me.GRID_COL_RETRY_COUNT_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_RETRY_COUNT), TextBox).Text = dvRow(Task.TaskSearchDV.COL_RETRY_COUNT).ToString
                            CType(e.Row.Cells(Me.GRID_COL_RETRY_DELAY_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_RETRY_DELAY), TextBox).Text = dvRow(Task.TaskSearchDV.COL_RETRY_DELAY).ToString
                            CType(e.Row.Cells(Me.GRID_COL_TIMEOUT_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_TIMEOUT), TextBox).Text = dvRow(Task.TaskSearchDV.COL_TIMEOUT).ToString
                            CType(e.Row.Cells(Me.GRID_COL_PARAMETERS_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_PARAMETERS), TextBox).Text = dvRow(Task.TaskSearchDV.COL_TASK_PARAMS).ToString

                        Else
                            CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_CODE), Label).Text = dvRow(Task.TaskSearchDV.COL_CODE).ToString
                            CType(e.Row.Cells(Me.GRID_COL_DESC_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_DESC), Label).Text = dvRow(Task.TaskSearchDV.COL_DESCRIPTION).ToString
                            CType(e.Row.Cells(Me.GRID_COL_RETRY_COUNT_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_RETRY_COUNT), Label).Text = dvRow(Task.TaskSearchDV.COL_RETRY_COUNT).ToString
                            CType(e.Row.Cells(Me.GRID_COL_RETRY_DELAY_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_RETRY_DELAY), Label).Text = dvRow(Task.TaskSearchDV.COL_RETRY_DELAY).ToString
                            CType(e.Row.Cells(Me.GRID_COL_TIMEOUT_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_TIMEOUT), Label).Text = dvRow(Task.TaskSearchDV.COL_TIMEOUT).ToString
                            CType(e.Row.Cells(Me.GRID_COL_PARAMETERS_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_PARAMETERS), Label).Text = dvRow(Task.TaskSearchDV.COL_TASK_PARAMS).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer
                If (e.CommandName = Me.EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.TaskID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_TASK_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_TASK_ID), Label).Text)
                    Me.State.MyBO = New Task(Me.State.TaskID)

                    Me.PopulateGrid()

                    Me.State.PageIndex = Grid.PageIndex

                    Me.SetControlState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.TaskID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_TASK_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_TASK_ID), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Control Handler"
        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try
                Me.txtSearchCode.Text = String.Empty
                Me.txtSearchDesc.Text = String.Empty
                Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX
                
                With State
                    .IsGridAddNew = False
                    .TaskID = Guid.Empty
                    .MyBO = Nothing
                    .searchCode = String.Empty
                    .searchDesc = String.Empty
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
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
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.IsGridAddNew = True
                AddNew()
                Me.SetControlState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub AddNew()
            If Me.State.MyBO Is Nothing OrElse Me.State.MyBO.IsNew = False Then
                Me.State.MyBO = New Task
                State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
            End If
            Me.State.TaskID = Me.State.MyBO.Id
            State.IsGridAddNew = True
            PopulateGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.TaskID, Me.Grid, _
                                               Me.State.PageIndex, Me.State.IsEditMode)
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            SetGridControls(Me.Grid, False)
        End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

            Try
                PopulateBOFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.IsGridAddNew = False
                    Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                    Me.State.searchDV = Nothing
                    Me.State.MyBO = Nothing
                    Me.ReturnFromEditing()
                Else
                    Me.MasterPage.MessageController.AddWarning(Me.MSG_RECORD_NOT_SAVED, True)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                With State
                    If .IsGridAddNew Then
                        RemoveNewRowFromSearchDV()
                        .IsGridAddNew = False
                        Grid.PageIndex = .PageIndex
                    End If
                    .TaskID = Guid.Empty
                    Me.State.MyBO = Nothing
                    .IsEditMode = False
                End With
                Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                'Me.State.searchDV = Nothing
                PopulateGrid()
                SetControlState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region
    End Class

End Namespace