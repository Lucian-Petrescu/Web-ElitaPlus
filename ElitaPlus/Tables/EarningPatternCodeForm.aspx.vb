Option Strict On
Option Explicit On

Namespace Tables

    Partial Class EarningPatternCodeForm
        Inherits ElitaPlusSearchPage

        Private Class PageStatus

            Public Sub New()
                pageIndex = 0
                pageCount = 0
            End Sub

        End Class


#Region "Member Variables"

        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            'Public MyBO As EarningCode = New EarningCode
            Public MyBO As EarningCode
            Public DescriptionMask As String
            Public CodeMask As String
            Public CompanyGroupId As Guid
            Public Id As Guid
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = EarningCode.EarningCodeSearchDV.COL_CODE
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
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

#Region " Web Form Designer Generated Code "

        '    Protected WithEvents lblPageSize As System.Web.UI.WebControls.Label
        '    Protected WithEvents cboPageSize As System.Web.UI.WebControls.DropDownList
        '    Protected WithEvents lblRecordCount As System.Web.UI.WebControls.Label
        '    Protected WithEvents trPageSize As System.Web.UI.HtmlControls.HtmlTableRow


        Protected WithEvents ErrController As ErrorController
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"

        Private Const ID_COL_IDX As Integer = 2
        Private Const CODE_COL_IDX As Integer = 3
        Private Const DESCRIPTION_COL_IDX As Integer = 4

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionTextBox"
        Private Const CODE_CONTROL_NAME As String = "CodeTextBox"
        Private Const ID_CONTROL_NAME As String = "IdLabel"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region

#Region "Button Click Handlers"

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click
            Try
                State.PageIndex = 0
                State.Id = Guid.Empty
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
                State.PageIndex = Grid.CurrentPageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            Try
                ClearSearchCriteria()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty
                SearchCodeTextBox.Text = String.Empty

                'Me.Grid.CurrentPageIndex = 0
                'Me.Grid.DataSource = Nothing
                'Me.Grid.DataBind()
                'ControlMgr.SetVisibleControl(Me, trPageSize, False)
                'Me.State.Id = Guid.Empty

                'Update Page State
                With State
                    .DescriptionMask = SearchDescriptionTextBox.Text
                    .CodeMask = SearchCodeTextBox.Text
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.AddingNewRow = True
                AddNew()
                SetGridControls(Grid, False)
                SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                PopulateBOFromForm()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    State.AddingNewRow = False
                    AddInfoMsg(MSG_RECORD_SAVED_OK)
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click

            Try
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                If (State.AddingNewRow) Then
                    State.AddingNewRow = False
                    State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

#End Region

#Region "Private Methods"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                ErrController.Clear_Hide()
                SetStateProperties()
                If Not Page.IsPostBack Then
                    SetDefaultButton(SearchCodeTextBox, SearchButton)
                    SetDefaultButton(SearchDescriptionTextBox, SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(Grid)
                    State.PageIndex = 0
                    If State.MyBO Is Nothing Then
                        State.MyBO = New EarningCode
                    End If
                    SetButtonsState()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
            ShowMissingTranslations(ErrController)
        End Sub

        Private Sub PopulateGrid()

            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView

                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetGridDataView()
                    '  Me.State.searchDV.Sort = Me.Grid.DataMember
                End If
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                State.searchDV.Sort = State.SortExpression

                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)
                Else
                    'In a Delete scenario...
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex, State.IsEditMode)
                End If
                Grid.AutoGenerateColumns = False
                Grid.Columns(DESCRIPTION_COL_IDX).SortExpression = EarningCode.EarningCodeSearchDV.COL_DESCRIPTION
                Grid.Columns(CODE_COL_IDX).SortExpression = EarningCode.EarningCodeSearchDV.COL_CODE

                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Function GetGridDataView() As DataView

            With State
                Return (EarningCode.LoadList(.DescriptionMask, .CodeMask, .CompanyGroupId))
            End With

        End Function

        Private Sub SetStateProperties()

            State.DescriptionMask = SearchDescriptionTextBox.Text
            State.CodeMask = SearchCodeTextBox.Text
            State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.searchDV.Count

            If Grid.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub


        Private Sub AddNew()
            State.searchDV = GetGridDataView()

            State.MyBO = New EarningCode
            State.Id = State.MyBO.Id

            State.searchDV = State.MyBO.GetNewDataViewRow(State.searchDV, State.Id, State.MyBO)

            Grid.DataSource = State.searchDV

            SetGridControls(Grid, False)

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)

            Grid.AutoGenerateColumns = False
            Grid.Columns(DESCRIPTION_COL_IDX).SortExpression = EarningCode.EarningCodeSearchDV.COL_DESCRIPTION
            Grid.Columns(CODE_COL_IDX).SortExpression = EarningCode.EarningCodeSearchDV.COL_CODE

            SortAndBindGrid()

            'Set focus on the Code TextBox for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(Grid, CODE_COL_IDX, CODE_CONTROL_NAME, Grid.EditItemIndex)

        End Sub

        Private Sub PopulateBOFromForm()

            Try
                With State.MyBO
                    .Description = CType(Grid.Items(Grid.EditItemIndex).Cells(DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text
                    .Code = CType(Grid.Items(Grid.EditItemIndex).Cells(CODE_COL_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text
                    .CompanyGroupId = State.CompanyGroupId
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub PopulateFormFromBO()

            Dim gridRowIdx As Integer = Grid.EditItemIndex
            Try
                With State.MyBO
                    If .Description IsNot Nothing Then
                        CType(Grid.Items(gridRowIdx).Cells(DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                    End If
                    If .Code IsNot Nothing Then
                        CType(Grid.Items(gridRowIdx).Cells(CODE_COL_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text = .Code
                    End If
                    'If Not (.Id.Equals(Guid.Empty)) Then
                    '    CType(Me.Grid.Items(gridRowIdx).Cells(Me.ID_COL_IDX).FindControl(Me.ID_CONTROL_NAME), Label).Text = .Id.ToString
                    'End If
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditItemIndex = NO_ROW_SELECTED_INDEX

            If Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            SetGridControls(Grid, True)
            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = Grid.CurrentPageIndex
            SetButtonsState()

        End Sub

        Private Sub SetButtonsState()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                ControlMgr.SetEnableControl(Me, SearchButton, False)
                ControlMgr.SetEnableControl(Me, ClearButton, False)
                MenuEnabled = False
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                ControlMgr.SetEnableControl(Me, SearchButton, True)
                ControlMgr.SetEnableControl(Me, ClearButton, True)
                MenuEnabled = True
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub

#End Region

#Region " Datagrid Related "

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    Grid.CurrentPageIndex = State.PageIndex
                    PopulateGrid()
                    Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = EDIT_COMMAND) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.Id = New Guid(CType(Grid.Items(e.Item.ItemIndex).Cells(ID_COL_IDX).FindControl(ID_CONTROL_NAME), Label).Text)

                    State.MyBO = New EarningCode(State.Id)

                    PopulateGrid()

                    State.PageIndex = Grid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(Grid, CODE_COL_IDX, CODE_CONTROL_NAME, index)

                    PopulateFormFromBO()

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session

                    State.Id = New Guid(CType(Grid.Items(e.Item.ItemIndex).Cells(ID_COL_IDX).FindControl(ID_CONTROL_NAME), Label).Text)

                    State.MyBO = New EarningCode(State.Id)
                    Try
                        State.MyBO.Delete()
                        'Call the Save() method in the EarningCode Business Object here
                        State.MyBO.Save()
                    Catch ex As Exception
                        State.MyBO.RejectChanges()
                        Throw ex
                    End Try

                    State.PageIndex = Grid.CurrentPageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    State.IsAfterSave = True
                    State.searchDV = Nothing
                    PopulateGrid()
                    State.PageIndex = Grid.CurrentPageIndex

                End If

            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Protected Sub ItemBound(source As Object, e As DataGridItemEventArgs) Handles Grid.ItemDataBound

            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

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
                'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
                'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
                State.Id = Guid.Empty
                State.PageIndex = 0

                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "Description", Grid.Columns(DESCRIPTION_COL_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "Code", Grid.Columns(CODE_COL_IDX))
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region

    End Class

End Namespace

