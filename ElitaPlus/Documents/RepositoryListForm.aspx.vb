
Imports Assurant.ElitaPlus.BusinessObjectsNew.Documents
Imports System.Collections.Generic

Public Class RepositoryListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "Documents/RepositoryListForm.aspx"
    Public Const PAGETITLE As String = "REPOSITORY"
    Public Const PAGETAB As String = "ADMIN"
    Public Const SUMMARYTITLE As String = "SEARCH"

    Private Const GRID_COL_CODE_IDX As Integer = 0
    Private Const GRID_COL_DESCRIPTION_IDX As Integer = 1
    Private Const GRID_COL_STORAGE_PATH_IDX As Integer = 2
    Private Const GRID_COL_BUTTON_IDX As Integer = 3
    Private Const GRID_COL_REPOSITORY_TYPE_IDX As Integer = 4


    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"


    Private Const GRID_CTRL_NAME_LABLE_CODE As String = "CodeLabel"
    Private Const GRID_CTRL_NAME_LABLE_DESCRIPTION As String = "DescriptionLabel"
    Private Const GRID_CTRL_NAME_LABEL_STORAGE_PATH As String = "StoragePathLabel"
    Private Const GRID_CTRL_NAME_LABEL_REPOSITORY_TYPE As String = "RepositoryTypeLabel"

    Private Const GRID_CTRL_NAME_EDIT_CODE As String = "CodeTextBox"
    Private Const GRID_CTRL_NAME_EDIT_DESCRIPTION As String = "DescriptionTextBox"
    Private Const GRID_CTRL_NAME_EDIT_STORAGE_PATH As String = "StoragePathTextBox"
    Private Const GRID_CTRL_NAME_EDIT_REPOSITORY_TYPE As String = "RepositoryTypeTextBox"

    Private Const GRID_CTRL_NAME_BUTTON_SAVE As String = "SaveButton"
    Private Const GRID_CTRL_NAME_BUTTON_CANCEL As String = "CancelButton"
    Private Const GRID_CTRL_NAME_BUTTON_EDIT As String = "EditButton"

    Private Const EDIT_COMMAND As String = "EditRecord"
    Private Const SAVE_COMMAND As String = "SaveRecord"
    Private Const CANCEL_COMMAND As String = "CancelRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As Repository
        Public RepositoryId As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public RepositoryList As List(Of Repository)
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        '        Public SortExpression As String = EventConfig.EventConfigSearchDV.COL_EVENT_TYPE_DESC

        Public SearchCode As String = String.Empty
        Public SearchDescription As String = String.Empty
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
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

#Region "Bread Crum"
    Private Sub UpdateBreadCrum()
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub
#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Me.Form.DefaultButton = btnSearch.UniqueID
        Try
            If Not Me.IsPostBack Then
                UpdateBreadCrum()
                'Me.SortDirection = Role.RoleSearchDV.COL_NAME_CODE
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                'SetControlState()
                Me.State.PageIndex = 0
                Me.TranslateGridHeader(Grid)
                Me.TranslateGridControls(Grid)
            Else
                BindBoPropertiesToGridHeaders()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Helper functions"

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

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Code", Me.Grid.Columns(Me.GRID_COL_CODE_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Description", Me.Grid.Columns(Me.GRID_COL_DESCRIPTION_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "StoragePath", Me.Grid.Columns(Me.GRID_COL_STORAGE_PATH_IDX))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "RepositoryType", Me.Grid.Columns(Me.GRID_COL_REPOSITORY_TYPE_IDX))
        Me.ClearGridViewHeadersAndLabelsErrSign()
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

    Private Function PopulateBOFromForm() As Boolean

        With Me.State.MyBO
            .Code = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox).Text.Trim.ToUpper
            .Description = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_CTRL_NAME_EDIT_DESCRIPTION), TextBox).Text.Trim
            .StoragePath = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_STORAGE_PATH_IDX).FindControl(GRID_CTRL_NAME_EDIT_STORAGE_PATH), TextBox).Text.Trim.ToUpper
            .RepositoryTypeXcd = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_REPOSITORY_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_REPOSITORY_TYPE), TextBox).Text.Trim.ToUpper
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Function


#End Region

#Region "Grid related"
    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = Grid.PageIndex
                Me.State.RepositoryId = Guid.Empty
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
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.RepositoryList.Count(), State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            If (e.Row.RowType <> DataControlRowType.DataRow) Then
                Return
            End If
            If ((e.Row.RowState And DataControlRowState.Edit) = DataControlRowState.Edit) Then
                Dim ft As Repository = CType(e.Row.DataItem, Repository)
                CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_CODE), TextBox).Text = ft.Code
                CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_DESCRIPTION), TextBox).Text = ft.Description
                CType(e.Row.Cells(Me.GRID_COL_STORAGE_PATH_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_STORAGE_PATH), TextBox).Text = ft.StoragePath
                CType(e.Row.Cells(Me.GRID_COL_REPOSITORY_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_REPOSITORY_TYPE), TextBox).Text = ft.RepositoryTypeXcd
                CType(e.Row.Cells(Me.GRID_COL_BUTTON_IDX).FindControl(Me.GRID_CTRL_NAME_BUTTON_CANCEL), ImageButton).CommandArgument = ft.Id.ToString()
                CType(e.Row.Cells(Me.GRID_COL_BUTTON_IDX).FindControl(Me.GRID_CTRL_NAME_BUTTON_SAVE), ImageButton).CommandArgument = ft.Id.ToString()

                CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_CODE), TextBox).ReadOnly = Not Me.State.IsGridAddNew
                CType(e.Row.Cells(Me.GRID_COL_STORAGE_PATH_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_STORAGE_PATH), TextBox).ReadOnly = Not Me.State.IsGridAddNew
                CType(e.Row.Cells(Me.GRID_COL_REPOSITORY_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_REPOSITORY_TYPE), TextBox).ReadOnly = Not Me.State.IsGridAddNew

            Else
            Dim ft As Repository = CType(e.Row.DataItem, Repository)
            CType(e.Row.Cells(Me.GRID_COL_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_CODE), Label).Text = ft.Code
            CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_DESCRIPTION), Label).Text = ft.Description
                CType(e.Row.Cells(Me.GRID_COL_STORAGE_PATH_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_STORAGE_PATH), Label).Text = ft.StoragePath
                CType(e.Row.Cells(Me.GRID_COL_BUTTON_IDX).FindControl(Me.GRID_CTRL_NAME_BUTTON_EDIT), ImageButton).CommandArgument = ft.Id.ToString()
                CType(e.Row.Cells(Me.GRID_COL_REPOSITORY_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_REPOSITORY_TYPE), Label).Text = ft.RepositoryTypeXcd
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub


    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

        Try
            Dim RepositoryId As Guid
            Select Case e.CommandName
                Case Me.EDIT_COMMAND
                    RepositoryId = New Guid(e.CommandArgument.ToString())
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.RepositoryId = RepositoryId
                    Me.State.MyBO = Me.State.RepositoryList.Where(Function(ft) ft.Id = Me.State.RepositoryId).First()

                    Me.PopulateGrid()
                    Me.State.PageIndex = Grid.PageIndex
                    Me.SetControlState()
                Case Me.SAVE_COMMAND
                    PopulateBOFromForm()
                    If (Me.State.MyBO.IsDirty) Then
                        DocumentManager.Current.Save(Me.State.MyBO)
                        Me.State.IsAfterSave = True
                        Me.State.IsGridAddNew = False
                        Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                        Me.State.RepositoryList = Nothing
                        Me.State.MyBO = Nothing
                        Me.ReturnFromEditing()
                    Else
                        Me.MasterPage.MessageController.AddWarning(Me.MSG_RECORD_NOT_SAVED, True)
                        Me.ReturnFromEditing()
                    End If
                Case Me.CANCEL_COMMAND
                    With State
                        If .IsGridAddNew Then
                            If (Me.State.RepositoryList.Contains(Me.State.MyBO)) Then
                                Me.State.RepositoryList.Remove(Me.State.MyBO)
                            End If
                            .IsGridAddNew = False
                            Grid.PageIndex = .PageIndex
                        End If
                        .RepositoryId = Guid.Empty
                        .MyBO = Nothing
                        .IsEditMode = False
                    End With
                    Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                    'Me.State.searchDV = Nothing
                    PopulateGrid()
                    SetControlState()
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Private Function GetSortKey(ByVal pRepository As Repository) As String
        If (Me.SortDirection.Contains("CODE")) Then
            Return pRepository.Code
        ElseIf (Me.SortDirection.Contains("DESCRIPTION")) Then
            Return pRepository.Description
        ElseIf (Me.SortDirection.Contains("STORAGE_PATH")) Then
            Return pRepository.StoragePath
        ElseIf (Me.SortDirection.Contains("REPOSITORY_TYPE")) Then
            Return pRepository.RepositoryTypeXcd
        End If
    End Function

    Public Sub PopulateGrid()
        Dim blnNewSearch As Boolean = False
        Dim dv As DataView
        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView
            With State
                If (.RepositoryList Is Nothing) Then
                    .RepositoryList = _
                        New List(Of Repository)(DocumentManager.Current.Repositories.AsEnumerable() _
                        .Where(Function(r) _
                                   ((Me.State.SearchCode Is Nothing) OrElse (Me.State.SearchCode.Trim().Length = 0) OrElse (r.Code.Contains(Me.State.SearchCode))) AndAlso _
                                   ((Me.State.SearchDescription Is Nothing) AndAlso (Me.State.SearchDescription.Trim().Length = 0) OrElse (r.Description.Contains(Me.State.SearchDescription)))))

                    blnNewSearch = True
                End If
            End With

            If (Me.SortDirection.EndsWith("DESC")) Then
                Me.State.RepositoryList = Me.State.RepositoryList.OrderByDescending(Function(ft) GetSortKey(ft)).ToList()
            Else
                Me.State.RepositoryList = Me.State.RepositoryList.OrderBy(Function(ft) GetSortKey(ft)).ToList()
            End If

            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(Of Repository)( _
                    Me.State.RepositoryList, _
                    Function(ft) ft.Id = Me.State.RepositoryId, _
                    Me.Grid, _
                    Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(Of Repository)( _
                    Me.State.RepositoryList, _
                    Function(ft) ft.Id = Me.State.RepositoryId, _
                    Me.Grid, _
                    Me.State.PageIndex, _
                    Me.State.IsEditMode)
            Else
                Me.SetPageAndSelectedIndexFromGuid(Of Repository)( _
                    Me.State.RepositoryList,
                    Function(ft) ft.Id = Guid.Empty, _
                    Me.Grid, _
                    Me.State.PageIndex)
            End If

            Me.Grid.AutoGenerateColumns = False
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

        Me.TranslateGridControls(Grid)

        If (Me.State.RepositoryList.Count = 0) Then
            Me.State.MyBO = DocumentManager.Current.NewRepository()
            Me.State.RepositoryList.Add(Me.State.MyBO)
            Me.Grid.DataSource = Me.State.RepositoryList
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
            Me.Grid.DataSource = Me.State.RepositoryList
            Me.State.IsGridVisible = True
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
        End If


        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

        Session("recCount") = Me.State.RepositoryList.Count

        If Me.Grid.Visible Then
            If (Me.State.IsGridAddNew) Then
                Me.lblRecordCount.Text = (Me.State.RepositoryList.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                Me.lblRecordCount.Text = Me.State.RepositoryList.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

    End Sub
#End Region

#Region "Control Handler"
    Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
        Try
            CodeTextBox.Text = String.Empty
            DescriptionTextBox.Text = String.Empty

            Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX

            With State
                .IsGridAddNew = False
                .RepositoryId = Guid.Empty
                .MyBO = Nothing
                .SearchCode = String.Empty
                .SearchDescription = String.Empty
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try
            With State
                .PageIndex = 0
                .RepositoryId = Guid.Empty
                .MyBO = Nothing
                .IsGridVisible = True
                .RepositoryList = Nothing
                .HasDataChanged = False
                .IsGridAddNew = False
                'get search control value
                .SearchCode = CodeTextBox.Text
                .SearchDescription = DescriptionTextBox.Text
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
            Me.State.MyBO = DocumentManager.Current.NewRepository()
            If (Me.State.RepositoryList Is Nothing) Then
                Me.State.RepositoryList = New List(Of Repository)(DocumentManager.Current.Repositories.AsEnumerable())
            End If
            Me.State.RepositoryList.Add(Me.State.MyBO)
        End If
        Me.State.RepositoryId = Me.State.MyBO.Id
        State.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        Me.SetPageAndSelectedIndexFromGuid(Of Repository)(
            Me.State.RepositoryList,
            Function(ft) ft.Id = Me.State.RepositoryId,
            Me.Grid,
            Me.State.PageIndex,
            Me.State.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        SetGridControls(Me.Grid, False)
    End Sub

#End Region

End Class