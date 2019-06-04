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
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
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
                    SetControlState()
                    Me.State.PageIndex = 0
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                    PopulateSearchControls()
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

#Region "Control Handler"
        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try
                ddlSearchTask.SelectedIndex = -1
                ddlSearchSubscriberStatus.SelectedIndex = -1
                ddlSearchSubscriberType.SelectedIndex = -1

                Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX

                With State
                    .IsGridAddNew = False
                    .SubscriberTaskID = Guid.Empty
                    .searchTask = Guid.Empty
                    .searchSubscriberType = Guid.Empty
                    .searchSubscriberStatus = Guid.Empty
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
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
                Me.State.MyBO = New SubscriberTask
                State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
            End If
            Me.State.SubscriberTaskID = Me.State.MyBO.Id
            State.IsGridAddNew = True
            PopulateGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.SubscriberTaskID, Me.Grid,
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
                    .SubscriberTaskID = Guid.Empty
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

            Dim obj As SubscriberTask = New SubscriberTask(Me.State.SubscriberTaskID)

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
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "TaskId", Me.Grid.Columns(Me.GRID_COL_TASK_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "SubscriberTypeId", Me.Grid.Columns(Me.GRID_COL_SUBSCRIBER_TYPE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "SubscriberStatusId", Me.Grid.Columns(Me.GRID_COL_SUBSCRIBER_STATUS_IDX))
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
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, .SubscriberTaskID)
                End If
            End With
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub

        Private Function PopulateBOFromForm() As Boolean
            Dim objDDL As DropDownList
            With Me.State.MyBO
                objDDL = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_TASK_IDX).FindControl(GRID_CTRL_NAME_EDIT_TASK), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "TaskId", objDDL)
                objDDL = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_SUBSCRIBER_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_SUBSCRIBER_TYPE), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "SubscriberTypeId", objDDL)
                objDDL = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_SUBSCRIBER_STATUS_IDX).FindControl(GRID_CTRL_NAME_EDIT_SUBSCRIBER_STATUS), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "SubscriberStatusId", objDDL)
            End With
            If Me.ErrCollection.Count > 0 Then
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
                Me.ddlSearchSubscriberType.Populate(CommonConfigManager.Current.ListManager.GetList("SUBSCRIBER_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                .AddBlankItem = True
               })
                'Me.BindListControlToDataView(Me.ddlSearchSubscriberStatus, LookupListNew.DropdownLookupList("EVNT_SUB_STAT", ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True)
                Me.ddlSearchSubscriberStatus.Populate(CommonConfigManager.Current.ListManager.GetList("EVNT_SUB_STAT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                .AddBlankItem = True
               })

                If Me.State.searchTask <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchTask, Me.State.searchTask)
                End If
                If Me.State.searchSubscriberType <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchSubscriberType, Me.State.searchSubscriberType)
                End If
                If Me.State.searchSubscriberStatus <> Guid.Empty Then
                    Me.SetSelectedItem(ddlSearchSubscriberStatus, Me.State.searchSubscriberStatus)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

                Me.State.searchDV.Sort = Me.SortDirection
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.SubscriberTaskID, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.SubscriberTaskID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
                End If

                Me.Grid.AutoGenerateColumns = False
                Me.Grid.Columns(Me.GRID_COL_TASK_IDX).SortExpression = SubscriberTask.SubscriberTaskSearchDV.COL_TASK_CODE
                Me.Grid.Columns(Me.GRID_COL_SUBSCRIBER_TYPE_IDX).SortExpression = SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_TYPE_DESC
                Me.Grid.Columns(Me.GRID_COL_SUBSCRIBER_STATUS_IDX).SortExpression = SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_STATUS_DESC
                SortAndBindGrid(blnNewSearch)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

            Me.TranslateGridControls(Grid)

            If (Me.State.searchDV.Count = 0) Then
                Me.State.searchDV = Nothing
                Me.State.MyBO = New SubscriberTask
                SubscriberTask.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
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
                    Me.State.SubscriberTaskID = Guid.Empty
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
                    strID = GetGuidStringFromByteArray(CType(dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_TASK_ID), Byte()))
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(Me.GRID_COL_SUBSCRIBER_TASK_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_SUBSCRIBER_TASK_ID), Label).Text = strID

                        If (Me.State.IsEditMode = True AndAlso Me.State.SubscriberTaskID.ToString.Equals(strID)) Then
                            Dim objDDL As DropDownList
                            Dim dv As DataView
                            Dim guidVal As Guid

                            'populate task dropdown
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_TASK_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_TASK), DropDownList)
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
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_SUBSCRIBER_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_SUBSCRIBER_TYPE), DropDownList)
                            ' dv = LookupListNew.DropdownLookupList("SUBSCRIBER_TYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            'Me.BindListControlToDataView(objDDL, dv, , , True)
                            objDDL.Populate(CommonConfigManager.Current.ListManager.GetList("SUBSCRIBER_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                             {
                            .AddBlankItem = True
                              })
                            guidVal = New Guid(CType(dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_TYPE_ID), Byte()))
                            SetSelectedItem(objDDL, guidVal)

                            'populate subscriber status dropdown
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_SUBSCRIBER_STATUS_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_SUBSCRIBER_STATUS), DropDownList)
                            'dv = LookupListNew.DropdownLookupList("EVNT_SUB_STAT", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            'Me.BindListControlToDataView(objDDL, dv, , , True)
                            objDDL.Populate(CommonConfigManager.Current.ListManager.GetList("EVNT_SUB_STAT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                             {
                            .AddBlankItem = True
                              })
                            guidVal = New Guid(CType(dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_STATUS_ID), Byte()))
                            SetSelectedItem(objDDL, guidVal)

                        Else
                            CType(e.Row.Cells(Me.GRID_COL_TASK_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_TASK), Label).Text = dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_TASK_CODE).ToString & "-" & dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_TASK_DESC).ToString
                            CType(e.Row.Cells(Me.GRID_COL_SUBSCRIBER_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_SUBSCRIBER_TYPE), Label).Text = dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_TYPE_DESC).ToString
                            CType(e.Row.Cells(Me.GRID_COL_SUBSCRIBER_STATUS_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_SUBSCRIBER_STATUS), Label).Text = dvRow(SubscriberTask.SubscriberTaskSearchDV.COL_SUBSCRIBER_STATUS_DESC).ToString

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

                    Me.State.SubscriberTaskID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_SUBSCRIBER_TASK_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_SUBSCRIBER_TASK_ID), Label).Text)
                    Me.State.MyBO = New SubscriberTask(Me.State.SubscriberTaskID)

                    Me.PopulateGrid()

                    Me.State.PageIndex = Grid.PageIndex

                    Me.SetControlState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.SubscriberTaskID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_SUBSCRIBER_TASK_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_SUBSCRIBER_TASK_ID), Label).Text)
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
    End Class
End Namespace