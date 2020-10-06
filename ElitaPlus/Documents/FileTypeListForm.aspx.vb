Imports Assurant.ElitaPlus.BusinessObjectsNew.Documents
Imports System.Collections.Generic

Public Class FileTypeListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "Documents/FileTypeListForm.aspx"
    Public Const PAGETITLE As String = "FILE_TYPE"
    Public Const PAGETAB As String = "ADMIN"
    Public Const SUMMARYTITLE As String = "SEARCH"

    Private Const GRID_COL_CODE_IDX As Integer = 0
    Private Const GRID_COL_DESCRIPTION_IDX As Integer = 1
    Private Const GRID_COL_EXTENSION_IDX As Integer = 2
    Private Const GRID_COL_MIME_TYPE_IDX As Integer = 3
    Private Const GRID_COL_BUTTON_IDX As Integer = 4


    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"


    Private Const GRID_CTRL_NAME_LABLE_CODE As String = "CodeLabel"
    Private Const GRID_CTRL_NAME_LABLE_DESCRIPTION As String = "DescriptionLabel"
    Private Const GRID_CTRL_NAME_LABEL_EXTENSION As String = "ExtensionLabel"
    Private Const GRID_CTRL_NAME_LABLE_MIME_TYPE As String = "MimeTypeLabel"

    Private Const GRID_CTRL_NAME_EDIT_CODE As String = "CodeTextBox"
    Private Const GRID_CTRL_NAME_EDIT_DESCRIPTION As String = "DescriptionTextBox"
    Private Const GRID_CTRL_NAME_EDIT_EXTENSION As String = "ExtensionTextBox"
    Private Const GRID_CTRL_NAME_EDIT_MIME_TYPE As String = "MimeTypeTextBox"

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
        Public MyBO As FileType
        Public FileTypeId As Guid
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public FileTypeList As List(Of FileType)
        Public HasDataChanged As Boolean
        Public IsGridAddNew As Boolean = False
        Public IsEditMode As Boolean = False
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        '        Public SortExpression As String = EventConfig.EventConfigSearchDV.COL_EVENT_TYPE_DESC

        Public SearchCode As String = String.Empty
        Public SearchDescription As String = String.Empty
        Public SearchExtension As String = String.Empty
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

#Region "Bread Crum"
    Private Sub UpdateBreadCrum()
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub
#End Region

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()
        Form.DefaultButton = btnSearch.UniqueID
        Try
            If Not IsPostBack Then
                UpdateBreadCrum()
                'Me.SortDirection = Role.RoleSearchDV.COL_NAME_CODE
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                'SetControlState()
                State.PageIndex = 0
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
            Else
                BindBoPropertiesToGridHeaders()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Helper functions"

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

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.MyBO, "Code", Grid.Columns(GRID_COL_CODE_IDX))
        BindBOPropertyToGridHeader(State.MyBO, "Description", Grid.Columns(GRID_COL_DESCRIPTION_IDX))
        BindBOPropertyToGridHeader(State.MyBO, "Extension", Grid.Columns(GRID_COL_EXTENSION_IDX))
        BindBOPropertyToGridHeader(State.MyBO, "MimeType", Grid.Columns(GRID_COL_MIME_TYPE_IDX))
        ClearGridViewHeadersAndLabelsErrSign()
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

    Private Function PopulateBOFromForm() As Boolean

        With State.MyBO
            .Code = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox).Text.Trim.ToUpper
            .Description = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_CTRL_NAME_EDIT_DESCRIPTION), TextBox).Text.Trim
            .Extension = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_EXTENSION_IDX).FindControl(GRID_CTRL_NAME_EDIT_EXTENSION), TextBox).Text.Trim.ToUpper
            .MimeType = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_MIME_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_MIME_TYPE), TextBox).Text.Trim
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Function


#End Region

#Region "Grid related"
    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = Grid.PageIndex
                State.FileTypeId = Guid.Empty
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
            State.PageIndex = NewCurrentPageIndex(Grid, State.FileTypeList.Count(), State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            If (e.Row.RowType <> DataControlRowType.DataRow) Then
                Return
            End If
            If ((e.Row.RowState And DataControlRowState.Edit) = DataControlRowState.Edit) Then
                Dim ft As FileType = CType(e.Row.DataItem, FileType)
                CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox).Text = ft.Code
                CType(e.Row.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_CTRL_NAME_EDIT_DESCRIPTION), TextBox).Text = ft.Description
                CType(e.Row.Cells(GRID_COL_EXTENSION_IDX).FindControl(GRID_CTRL_NAME_EDIT_EXTENSION), TextBox).Text = ft.Extension
                CType(e.Row.Cells(GRID_COL_MIME_TYPE_IDX).FindControl(GRID_CTRL_NAME_EDIT_MIME_TYPE), TextBox).Text = ft.MimeType
                CType(e.Row.Cells(GRID_COL_BUTTON_IDX).FindControl(GRID_CTRL_NAME_BUTTON_CANCEL), ImageButton).CommandArgument = ft.Id.ToString()
                CType(e.Row.Cells(GRID_COL_BUTTON_IDX).FindControl(GRID_CTRL_NAME_BUTTON_SAVE), ImageButton).CommandArgument = ft.Id.ToString()

                CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_EDIT_CODE), TextBox).ReadOnly = Not State.IsGridAddNew
                CType(e.Row.Cells(GRID_COL_EXTENSION_IDX).FindControl(GRID_CTRL_NAME_EDIT_EXTENSION), TextBox).ReadOnly = Not State.IsGridAddNew
            Else
                Dim ft As FileType = CType(e.Row.DataItem, FileType)
                CType(e.Row.Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_LABLE_CODE), Label).Text = ft.Code
                CType(e.Row.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_CTRL_NAME_LABLE_DESCRIPTION), Label).Text = ft.Description
                CType(e.Row.Cells(GRID_COL_EXTENSION_IDX).FindControl(GRID_CTRL_NAME_LABEL_EXTENSION), Label).Text = ft.Extension
                CType(e.Row.Cells(GRID_COL_MIME_TYPE_IDX).FindControl(GRID_CTRL_NAME_LABLE_MIME_TYPE), Label).Text = ft.MimeType
                CType(e.Row.Cells(GRID_COL_BUTTON_IDX).FindControl(GRID_CTRL_NAME_BUTTON_EDIT), ImageButton).CommandArgument = ft.Id.ToString()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub


    Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

        Try
            Dim fileTypeId As Guid
            Select Case e.CommandName
                Case EDIT_COMMAND
                    fileTypeId = New Guid(e.CommandArgument.ToString())
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.FileTypeId = fileTypeId
                    State.MyBO = State.FileTypeList.Where(Function(ft) ft.Id = State.FileTypeId).First()

                    PopulateGrid()
                    State.PageIndex = Grid.PageIndex
                    SetControlState()
                Case SAVE_COMMAND
                    PopulateBOFromForm()
                    If (State.MyBO.IsDirty) Then
                        DocumentManager.Current.Save(State.MyBO)
                        State.IsAfterSave = True
                        State.IsGridAddNew = False
                        MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                        State.FileTypeList = Nothing
                        State.MyBO = Nothing
                        ReturnFromEditing()
                    Else
                        MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                        ReturnFromEditing()
                    End If
                Case CANCEL_COMMAND
                    With State
                        If .IsGridAddNew Then
                            If (State.FileTypeList.Contains(State.MyBO)) Then
                                State.FileTypeList.Remove(State.MyBO)
                            End If
                            .IsGridAddNew = False
                            Grid.PageIndex = .PageIndex
                        End If
                        .FileTypeId = Guid.Empty
                        .MyBO = Nothing
                        .IsEditMode = False
                    End With
                    Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                    'Me.State.searchDV = Nothing
                    PopulateGrid()
                    SetControlState()
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Private Function GetSortKey(pFileType As FileType) As String
        If (SortDirection.Contains("CODE")) Then
            Return pFileType.Code
        ElseIf (SortDirection.Contains("DESCRIPTION")) Then
            Return pFileType.Description
        ElseIf (SortDirection.Contains("EXTENSION")) Then
            Return pFileType.Extension
        ElseIf (SortDirection.Contains("MIME_TYPE")) Then
            Return pFileType.MimeType
        End If

    End Function

    Public Sub PopulateGrid()
        Dim blnNewSearch As Boolean = False
        Dim dv As DataView
        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView
            With State
                If (.FileTypeList Is Nothing) Then
                    .FileTypeList = _
                        New List(Of FileType)(DocumentManager.Current.FileTypes.AsEnumerable() _
                        .Where(Function(ft) _
                                   ((State.SearchCode Is Nothing) OrElse (State.SearchCode.Trim().Length = 0) OrElse (ft.Code.Contains(State.SearchCode))) AndAlso _
                                   ((State.SearchDescription Is Nothing) AndAlso (State.SearchDescription.Trim().Length = 0) OrElse (ft.Description.Contains(State.SearchDescription))) AndAlso _
                                   ((State.SearchExtension Is Nothing) AndAlso (State.SearchExtension.Trim().Length = 0) OrElse (ft.Extension.Contains(State.SearchExtension)))))

                    blnNewSearch = True
                End If
            End With

            If (SortDirection.EndsWith("DESC")) Then
                State.FileTypeList = State.FileTypeList.OrderByDescending(Function(ft) GetSortKey(ft)).ToList()
            Else
                State.FileTypeList = State.FileTypeList.OrderBy(Function(ft) GetSortKey(ft)).ToList()
            End If

            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(Of FileType)( _
                    State.FileTypeList, _
                    Function(ft) ft.Id = State.FileTypeId, _
                    Grid, _
                    State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(Of FileType)( _
                    State.FileTypeList, _
                    Function(ft) ft.Id = State.FileTypeId, _
                    Grid, _
                    State.PageIndex, _
                    State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(Of FileType)( _
                    State.FileTypeList,
                    Function(ft) ft.Id = Guid.Empty, _
                    Grid, _
                    State.PageIndex)
            End If

            Grid.AutoGenerateColumns = False
            SortAndBindGrid(blnNewSearch)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

        TranslateGridControls(Grid)

        If (State.FileTypeList.Count = 0) Then
            State.MyBO = DocumentManager.Current.NewFileType()
            State.FileTypeList.Add(State.MyBO)
            Grid.DataSource = State.FileTypeList
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
            Grid.DataSource = State.FileTypeList
            State.IsGridVisible = True
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()
        End If


        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, moSearchResults, State.IsGridVisible)

        Session("recCount") = State.FileTypeList.Count

        If Grid.Visible Then
            If (State.IsGridAddNew) Then
                lblRecordCount.Text = (State.FileTypeList.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                lblRecordCount.Text = State.FileTypeList.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

    End Sub
#End Region

#Region "Control Handler"
    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try
            CodeTextBox.Text = String.Empty
            DescriptionTextBox.Text = String.Empty
            ExtensionTextBox.Text = String.Empty

            Grid.EditIndex = NO_ITEM_SELECTED_INDEX

            With State
                .IsGridAddNew = False
                .FileTypeId = Guid.Empty
                .MyBO = Nothing
                .SearchCode = String.Empty
                .SearchDescription = String.Empty
                .SearchExtension = String.Empty
            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            With State
                .PageIndex = 0
                .FileTypeId = Guid.Empty
                .MyBO = Nothing
                .IsGridVisible = True
                .FileTypeList = Nothing
                .HasDataChanged = False
                .IsGridAddNew = False
                'get search control value
                .SearchCode = CodeTextBox.Text
                .SearchDescription = DescriptionTextBox.Text
                .SearchExtension = ExtensionTextBox.Text
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
            State.MyBO = DocumentManager.Current.NewFileType()
            If (State.FileTypeList Is Nothing) Then
                State.FileTypeList = New List(Of FileType)(DocumentManager.Current.FileTypes.AsEnumerable())
            End If
            State.FileTypeList.Add(State.MyBO)
        End If
        State.FileTypeId = State.MyBO.Id
        State.IsGridAddNew = True
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        SetPageAndSelectedIndexFromGuid(Of FileType)( _
            State.FileTypeList, _
            Function(ft) ft.Id = State.FileTypeId, _
            Grid, _
            State.PageIndex, _
            State.IsEditMode)
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        SetGridControls(Grid, False)
    End Sub

#End Region

End Class