Option Strict On
Option Explicit On

Namespace Tables

    Partial Class RepairCodeForm
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
        Private moRepairCode As RepairCode
        Private moRepairCodeId As String

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

        Private ReadOnly Property TheRepairCode() As RepairCode
            Get
                If IsNewRepairCode() = True Then
                    ' For creating, inserting
                    moRepairCode = New RepairCode
                    RepairCodeId = moRepairCode.Id.ToString
                Else
                    ' For updating, deleting
                    Dim oRepairCodeId As Guid = GetGuidFromString(RepairCodeId)
                    moRepairCode = New RepairCode(oRepairCodeId)
                End If

                Return moRepairCode
            End Get
        End Property

        Private Property IsNewRepairCode() As Boolean
            Get
                Return State.IsNew
            End Get
            Set(Value As Boolean)
                State.IsNew = Value
            End Set
        End Property

        Private Property RepairCodeId() As String
            Get
                If Grid.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    moRepairCodeId = GetSelectedGridText(Grid, ID_COL_IDX)
                End If
                Return moRepairCodeId
            End Get
            Set(Value As String)
                If Grid.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    SetSelectedGridText(Grid, ID_COL_IDX, Value)
                End If
                moRepairCodeId = Value
            End Set
        End Property
#End Region

#Region "Page State"

        Class MyState
            Public IsNew As Boolean
            Public PageIndex As Integer = 0
            'Public myBO As RepairCode = New RepairCode
            Public myBO As RepairCode
            Public DescriptionMask As String
            Public CodeMask As String
            'Public CompanyId As Guid
            'Public CompanyIds As ArrayList
            Public Id As Guid
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = RepairCode.RepairCodeSearchDV.COL_DESCRIPTION
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public newRepairCodeId As Guid

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

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ErrController As ErrorController
        Protected WithEvents SearchDescriptionLabel As System.Web.UI.WebControls.Label

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

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

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const COMPANY_GROUP_CODE_IN_GRID_CONTROL_NAME As String = "TextBoxGridGroup"
        Private Const DESCRIPTION_IN_GRID_CONTROL_NAME As String = "TextBoxGridDescription"
        Private Const CODE_IN_GRID_CONTROL_NAME As String = "TextBoxGridCode"

        Private Const DESCRIPTION_CELL_STYLE_WIDTH As String = "300"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"
        Private Const ACTION_NEW As String = "ACTION_NEW"
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

            ClearSearchCriteria()

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty
                SearchCodeTextBox.Text = String.Empty

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
                IsNewRepairCode = True
                State.IsEditMode = True
                State.IsGridVisible = True
                State.AddingNewRow = True
                AddNew()
                SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                PopulateBOFromForm()
                If (State.myBO.IsDirty) Then
                    State.myBO.Save()
                    State.IsAfterSave = True
                    State.AddingNewRow = False
                    AddInfoMsg(MSG_RECORD_SAVED_OK)
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
                If IsNewRepairCode = True Then
                    IsNewRepairCode = False
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click

            Try
                IsNewRepairCode = True
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                If (State.AddingNewRow) Then
                    State.AddingNewRow = False
                    State.searchDV = Nothing
                End If
                ReturnFromEditing()
                IsNewRepairCode = False
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
                    SetDefaultButton(SearchDescriptionTextBox, SearchButton)
                    SetDefaultButton(SearchCodeTextBox, SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(Grid)
                    IsNewRepairCode = False
                    State.PageIndex = 0
                    If State.MyBO Is Nothing Then
                        State.myBO = New RepairCode
                    End If
                    SetButtonsState()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
            ShowMissingTranslations(ErrController)
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = ACTION_NONE)

            If (State.searchDV Is Nothing) Then
                State.searchDV = GetGridDataView()
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                'If Not IsNewRepairCode Then Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            End If

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
            Grid.Columns(DESCRIPTION_COL_IDX).SortExpression = RepairCode.RepairCodeSearchDV.COL_DESCRIPTION
            Grid.Columns(CODE_COL_IDX).SortExpression = RepairCode.RepairCodeSearchDV.COL_CODE

            'If oAction <> Me.ACTION_EDIT Then
            SortAndBindGrid()

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

        Private Function GetGridDataView() As DataView

            With State
                Return (RepairCode.getList(.DescriptionMask, .CodeMask, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
            End With

        End Function

        Private Sub SetStateProperties()

            State.DescriptionMask = SearchDescriptionTextBox.Text
            State.CodeMask = SearchCodeTextBox.Text
            'Me.State.CompanyIds = ElitaPlusIdentity.Current.ActiveUser.Companies

        End Sub

        Private Sub AddNew()

            State.searchDV = GetGridDataView()

            State.myBO = New RepairCode
            State.Id = State.myBO.Id

            State.searchDV = State.myBO.GetNewDataViewRow(State.searchDV, State.Id)

            Grid.DataSource = State.searchDV
            SetGridControls(Grid, False)
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)

            Grid.AutoGenerateColumns = False
            Grid.Columns(DESCRIPTION_COL_IDX).SortExpression = RepairCode.RepairCodeSearchDV.COL_DESCRIPTION
            Grid.Columns(CODE_COL_IDX).SortExpression = RepairCode.RepairCodeSearchDV.COL_CODE

            SortAndBindGrid()

            'Me.CreateCompanyDropDownOrLabel()

            'Set focus on the Code TextBox for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(Grid, CODE_COL_IDX, Grid.EditItemIndex)

        End Sub

        Private Sub PopulateBOFromForm()

            Try
                With State.myBO
                    .Description = CType(Grid.Items(Grid.EditItemIndex).Cells(DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text
                    .ShortDesc = CType(Grid.Items(Grid.EditItemIndex).Cells(CODE_COL_IDX).FindControl(CODE_IN_GRID_CONTROL_NAME), TextBox).Text
                    .CompanygroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id()
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

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse _
                    itemType = ListItemType.AlternatingItem OrElse _
                    itemType = ListItemType.SelectedItem) Then

                    e.Item.Cells(ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(RepairCode.RepairCodeSearchDV.COL_REPAIR_CODE_ID), Byte()))
                    e.Item.Cells(DESCRIPTION_COL_IDX).Text = dvRow(RepairCode.RepairCodeSearchDV.COL_DESCRIPTION).ToString
                    e.Item.Cells(CODE_COL_IDX).Text = dvRow(RepairCode.RepairCodeSearchDV.COL_CODE).ToString
                ElseIf (itemType = ListItemType.EditItem) Then
                    e.Item.Cells(ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(RepairCode.RepairCodeSearchDV.COL_REPAIR_CODE_ID), Byte()))
                    CType(e.Item.Cells(DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(RepairCode.RepairCodeSearchDV.COL_DESCRIPTION).ToString
                    CType(e.Item.Cells(CODE_COL_IDX).FindControl(CODE_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(RepairCode.RepairCodeSearchDV.COL_CODE).ToString

                End If
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

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        'Public Overrides Sub AddNewBoRow(ByVal dv As DataView)
        '    Dim oRepairCodeId As Guid = Guid.NewGuid
        '    Me.State.newRepairCodeId = oRepairCodeId
        '    Dim oDealerDrop, oProductCodeDrop As DropDownList

        '    Me.BaseAddNewGridRow(Grid, dv, oRepairCodeId)

        '    'Create and select the company drop down
        '    Dim oCompanyDrop As DropDownList
        '    oCompanyDrop = CType(Me.GetSelectedGridControl(Grid, COMPANY_CODE_COL_IDX), DropDownList)
        '    PopulateCompanyCodeDropGrid(oCompanyDrop)


        'End Sub

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

                    State.Id = New Guid(Grid.Items(e.Item.ItemIndex).Cells(ID_COL_IDX).Text)

                    State.myBO = New RepairCode(State.Id)

                    PopulateGrid(ACTION_EDIT)

                    'Me.CreateCompanyDropDownOrLabel()

                    State.PageIndex = Grid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)

                    'Set focus on the Code TextBox for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(Grid, CODE_COL_IDX, index)

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session
                    State.Id = New Guid(Grid.Items(e.Item.ItemIndex).Cells(ID_COL_IDX).Text)
                    State.myBO = New RepairCode(State.Id)

                    Try
                        State.myBO.Delete()
                        'Call the Save() method in the Business Object here
                        State.myBO.Save()
                    Catch ex As Exception
                        State.myBO.RejectChanges()
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

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.myBO, "Description", Grid.Columns(DESCRIPTION_COL_IDX))
            BindBOPropertyToGridHeader(State.myBO, "ShortDesc", Grid.Columns(CODE_COL_IDX))
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, itemIndex As Integer)
            'Set focus on the specified control on the EditItemIndex row for the grid
            Dim ctrlCode As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(CODE_IN_GRID_CONTROL_NAME), TextBox)
            SetFocus(ctrlCode)
            Dim ctrlDescription As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition + 1).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox)
            ctrlDescription.Style("width") = DESCRIPTION_CELL_STYLE_WIDTH
        End Sub

        Private Sub SetFieldWidthStyle(grid As DataGrid, cellPosition As Integer, itemIndex As Integer, width As String)

            Dim ctrl As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).Controls.Item(0), TextBox)
            ctrl.Style("width") = width

        End Sub

        'Private Sub CreateCompanyDropDownOrLabel()
        '    Dim oCompanyDrop As DropDownList
        '    oCompanyDrop = CType(Me.GetSelectedGridControl(Grid, COMPANY_GROUP_CODE_COL_IDX), DropDownList)

        '    Me.BindListControlToDataView(oCompanyDrop, LookupListNew.GetUserCompanyGroupList(), "CODE", , True)
        '    If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
        '        BindSelectItem(TheRepairCode.CompanygroupId.ToString, oCompanyDrop)
        '        If Me.State.IsNew Then
        '            oCompanyDrop.Enabled = True
        '        Else
        '            oCompanyDrop.Enabled = False
        '        End If
        '    Else
        '            BindSelectItem(ElitaPlusIdentity.Current.ActiveUser.CompanyId.ToString, oCompanyDrop)
        '            oCompanyDrop.Enabled = False
        '        End If
        'End Sub

#End Region


    End Class

End Namespace
