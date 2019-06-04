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
                IsEditing = (Me.Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
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
                    Dim oRepairCodeId As Guid = Me.GetGuidFromString(RepairCodeId)
                    moRepairCode = New RepairCode(oRepairCodeId)
                End If

                Return moRepairCode
            End Get
        End Property

        Private Property IsNewRepairCode() As Boolean
            Get
                Return Me.State.IsNew
            End Get
            Set(ByVal Value As Boolean)
                Me.State.IsNew = Value
            End Set
        End Property

        Private Property RepairCodeId() As String
            Get
                If Grid.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                    moRepairCodeId = Me.GetSelectedGridText(Grid, ID_COL_IDX)
                End If
                Return moRepairCodeId
            End Get
            Set(ByVal Value As String)
                If Grid.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                    Me.SetSelectedGridText(Grid, ID_COL_IDX, Value)
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
            Try
                Me.State.PageIndex = 0
                Me.State.Id = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateGrid()
                Me.State.PageIndex = Grid.CurrentPageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

            ClearSearchCriteria()

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty
                SearchCodeTextBox.Text = String.Empty

                'Update Page State
                With Me.State
                    .DescriptionMask = SearchDescriptionTextBox.Text
                    .CodeMask = SearchCodeTextBox.Text
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                IsNewRepairCode = True
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                AddNew()
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                PopulateBOFromForm()
                If (Me.State.myBO.IsDirty) Then
                    Me.State.myBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.AddingNewRow = False
                    Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                    Me.State.searchDV = Nothing
                    Me.ReturnFromEditing()
                Else
                    Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                    Me.ReturnFromEditing()
                End If
                If IsNewRepairCode = True Then
                    IsNewRepairCode = False
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click

            Try
                IsNewRepairCode = True
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True
                If (Me.State.AddingNewRow) Then
                    Me.State.AddingNewRow = False
                    Me.State.searchDV = Nothing
                End If
                ReturnFromEditing()
                IsNewRepairCode = False
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

#End Region

#Region "Private Methods"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                ErrController.Clear_Hide()
                Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    Me.SetDefaultButton(Me.SearchDescriptionTextBox, Me.SearchButton)
                    Me.SetDefaultButton(Me.SearchCodeTextBox, Me.SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetGridItemStyleColor(Me.Grid)
                    IsNewRepairCode = False
                    Me.State.PageIndex = 0
                    If Me.State.MyBO Is Nothing Then
                        Me.State.myBO = New RepairCode
                    End If
                    SetButtonsState()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
            Me.ShowMissingTranslations(ErrController)
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = ACTION_NONE)

            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = GetGridDataView()
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                'If Not IsNewRepairCode Then Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            End If

            Me.State.searchDV.Sort = Me.State.SortExpression
            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                'In a Delete scenario...
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            End If

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.DESCRIPTION_COL_IDX).SortExpression = RepairCode.RepairCodeSearchDV.COL_DESCRIPTION
            Me.Grid.Columns(Me.CODE_COL_IDX).SortExpression = RepairCode.RepairCodeSearchDV.COL_CODE

            'If oAction <> Me.ACTION_EDIT Then
            Me.SortAndBindGrid()

        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.Grid.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
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

            Me.State.DescriptionMask = SearchDescriptionTextBox.Text
            Me.State.CodeMask = SearchCodeTextBox.Text
            'Me.State.CompanyIds = ElitaPlusIdentity.Current.ActiveUser.Companies

        End Sub

        Private Sub AddNew()

            Me.State.searchDV = GetGridDataView()

            Me.State.myBO = New RepairCode
            Me.State.Id = Me.State.myBO.Id

            Me.State.searchDV = Me.State.myBO.GetNewDataViewRow(Me.State.searchDV, Me.State.Id)

            Grid.DataSource = Me.State.searchDV
            SetGridControls(Me.Grid, False)
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.DESCRIPTION_COL_IDX).SortExpression = RepairCode.RepairCodeSearchDV.COL_DESCRIPTION
            Me.Grid.Columns(Me.CODE_COL_IDX).SortExpression = RepairCode.RepairCodeSearchDV.COL_CODE

            Me.SortAndBindGrid()

            'Me.CreateCompanyDropDownOrLabel()

            'Set focus on the Code TextBox for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.CODE_COL_IDX, Me.Grid.EditItemIndex)

        End Sub

        Private Sub PopulateBOFromForm()

            Try
                With Me.State.myBO
                    .Description = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text
                    .ShortDesc = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.CODE_COL_IDX).FindControl(CODE_IN_GRID_CONTROL_NAME), TextBox).Text
                    .CompanygroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id()
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditItemIndex = NO_ROW_SELECTED_INDEX

            If Me.Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If
            SetGridControls(Grid, True)
            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = Grid.CurrentPageIndex
            SetButtonsState()

        End Sub

        Private Sub SetButtonsState()

            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                ControlMgr.SetEnableControl(Me, SearchButton, False)
                ControlMgr.SetEnableControl(Me, ClearButton, False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                ControlMgr.SetEnableControl(Me, SearchButton, True)
                ControlMgr.SetEnableControl(Me, ClearButton, True)
                Me.MenuEnabled = True
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub

#End Region

#Region " Datagrid Related "

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse _
                    itemType = ListItemType.AlternatingItem OrElse _
                    itemType = ListItemType.SelectedItem) Then

                    e.Item.Cells(Me.ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(RepairCode.RepairCodeSearchDV.COL_REPAIR_CODE_ID), Byte()))
                    e.Item.Cells(Me.DESCRIPTION_COL_IDX).Text = dvRow(RepairCode.RepairCodeSearchDV.COL_DESCRIPTION).ToString
                    e.Item.Cells(Me.CODE_COL_IDX).Text = dvRow(RepairCode.RepairCodeSearchDV.COL_CODE).ToString
                ElseIf (itemType = ListItemType.EditItem) Then
                    e.Item.Cells(Me.ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(RepairCode.RepairCodeSearchDV.COL_REPAIR_CODE_ID), Byte()))
                    CType(e.Item.Cells(Me.DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(RepairCode.RepairCodeSearchDV.COL_DESCRIPTION).ToString
                    CType(e.Item.Cells(Me.CODE_COL_IDX).FindControl(CODE_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(RepairCode.RepairCodeSearchDV.COL_CODE).ToString

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
                'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
                'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
                Me.State.Id = Guid.Empty
                Me.State.PageIndex = 0

                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
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

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.Grid.CurrentPageIndex = Me.State.PageIndex
                    Me.PopulateGrid()
                    Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.Id = New Guid(Me.Grid.Items(e.Item.ItemIndex).Cells(Me.ID_COL_IDX).Text)

                    Me.State.myBO = New RepairCode(Me.State.Id)

                    Me.PopulateGrid(Me.ACTION_EDIT)

                    'Me.CreateCompanyDropDownOrLabel()

                    Me.State.PageIndex = Grid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)

                    'Set focus on the Code TextBox for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.CODE_COL_IDX, index)

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session
                    Me.State.Id = New Guid(Me.Grid.Items(e.Item.ItemIndex).Cells(Me.ID_COL_IDX).Text)
                    Me.State.myBO = New RepairCode(Me.State.Id)

                    Try
                        Me.State.myBO.Delete()
                        'Call the Save() method in the Business Object here
                        Me.State.myBO.Save()
                    Catch ex As Exception
                        Me.State.myBO.RejectChanges()
                        Throw ex
                    End Try

                    Me.State.PageIndex = Grid.CurrentPageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    Me.State.IsAfterSave = True

                    Me.State.searchDV = Nothing
                    PopulateGrid()
                    Me.State.PageIndex = Grid.CurrentPageIndex
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.myBO, "Description", Me.Grid.Columns(Me.DESCRIPTION_COL_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.myBO, "ShortDesc", Me.Grid.Columns(Me.CODE_COL_IDX))
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal itemIndex As Integer)
            'Set focus on the specified control on the EditItemIndex row for the grid
            Dim ctrlCode As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(CODE_IN_GRID_CONTROL_NAME), TextBox)
            SetFocus(ctrlCode)
            Dim ctrlDescription As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition + 1).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox)
            ctrlDescription.Style("width") = Me.DESCRIPTION_CELL_STYLE_WIDTH
        End Sub

        Private Sub SetFieldWidthStyle(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal itemIndex As Integer, ByVal width As String)

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
