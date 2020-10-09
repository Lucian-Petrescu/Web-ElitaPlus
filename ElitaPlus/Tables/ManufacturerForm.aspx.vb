Option Strict On
Option Explicit On



Partial Class ManufacturerForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents ErrController As ErrorController

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
            IsEditing = (Grid.EditIndex > NO_ROW_SELECTED_INDEX)
        End Get
    End Property

#End Region

#Region "Page State"
    Class MyState
        Public PageIndex As Integer = 0
        Public Manufacturer As BusinessObjectsNew.Manufacturer
        Public DescriptionMask As String
        Public CompanyGroupId As Guid
        Public ManufacturerId As Guid
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        Public IsEditMode As Boolean
        Public AddingNewRow As Boolean
        Public Canceling As Boolean
        Public searchDV As DataView = Nothing
        Public SortExpression As String = Manufacturer.COL_NAME_DESCRIPTION
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public bnoRow As Boolean = False
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

    Private Const ID_COL As Integer = 2
    Private Const DESCRIPTION_COL As Integer = 3

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionTextBox"
    Private Const ID_CONTROL_NAME As String = "IdLabel"

    Private Const EDIT_COMMAND As String = "EditRecord"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    'Private Const EDIT As String = "Edit"
    'Private Const DELETE As String = "Delete"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region

#Region "Button Click Handlers"

    Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click
        Try
            State.IsGridVisible = True
            State.searchDV = Nothing
            PopulateGrid()
            'Me.State.PageIndex = Grid.CurrentPageIndex
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

            'Update Page State
            With State
                .DescriptionMask = SearchDescriptionTextBox.Text
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
            AddNewManufacturer()
            SetButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            PopulateBOFromForm()
            If (State.Manufacturer.IsDirty) Then
                State.Manufacturer.Save()
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

    'Private Sub ProcessLinkButtonClick(ByVal sender As System.Object)
    '    Dim clickedLinkButton As LinkButton = CType(sender, LinkButton)
    '    If clickedLinkButton.Text.Trim() <> "*" Then
    '        SearchDescriptionTextBox.Text = clickedLinkButton.Text
    '        Me.State.DescriptionMask = SearchDescriptionTextBox.Text
    '    Else
    '        ClearSearchCriteria()
    '    End If
    '    Try
    '        Me.State.PageIndex = 0
    '        PopulateGrid()
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.ErrController)
    '    End Try
    'End Sub

    'Protected Sub hlnkAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (Not Me.State.IsEditMode) Then ProcessLinkButtonClick(sender)
    'End Sub



#End Region


#Region "Private Methods"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        Try
            ErrController.Clear_Hide()
            SetStateProperties()
            If Not Page.IsPostBack Then
                SortDirection = State.SortExpression
                SetDefaultButton(SearchDescriptionTextBox, SearchButton)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                SetGridItemStyleColor(Grid)
                State.PageIndex = 0
                SetButtonsState()
            End If
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
        ShowMissingTranslations(ErrController)
    End Sub

    Private Sub PopulateGrid()

        Dim dv As DataView
        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView
            'dv = GetDV()
            If (State.searchDV Is Nothing) Then
                State.searchDV = GetDV()
            End If
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            State.searchDV.Sort = State.SortExpression
            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.ManufacturerId, Grid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.ManufacturerId, Grid, State.PageIndex, State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
            End If

            Grid.AutoGenerateColumns = False
            Grid.Columns(DESCRIPTION_COL).SortExpression = Manufacturer.COL_NAME_DESCRIPTION

            SortAndBindGrid()

            'Me.TranslateGridControls(Grid)
            'Grid.DataSource = dv
            'Me.Grid.DataBind()

            'Me.trPageSize.Visible = Me.Grid.Visible

            'Session("recCount") = dv.Count

            'If Me.Grid.Visible Then

            '    Me.lblRecordCount.Text = dv.Count & " " & Me.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            'End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        State.searchDV = GetGridDataView()
        State.searchDV.Sort = Grid.DataMember()

        Return (State.searchDV)

    End Function

    Private Function GetGridDataView() As DataView

        With State
            Return (Assurant.ElitaPlus.BusinessObjectsNew.Manufacturer.LoadList(.DescriptionMask, .CompanyGroupId))
        End With

    End Function

    Private Sub SetStateProperties()

        State.DescriptionMask = SearchDescriptionTextBox.Text
        State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

    End Sub

    Private Sub AddNewManufacturer()

        Dim dv As DataView

        State.searchDV = GetGridDataView()

        State.Manufacturer = New Assurant.ElitaPlus.BusinessObjectsNew.Manufacturer
        State.ManufacturerId = State.Manufacturer.Id

        State.searchDV = State.Manufacturer.GetNewDataViewRow(State.searchDV, State.ManufacturerId)

        Grid.DataSource = State.searchDV

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.ManufacturerId, Grid, State.PageIndex, State.IsEditMode)

        Grid.DataBind()

        State.PageIndex = Grid.PageIndex

        SetGridControls(Grid, False)

        'Set focus on the Description TextBox for the EditItemIndex row
        SetFocusOnEditableFieldInGrid(Grid, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, Grid.EditIndex)

        PopulateFormFromBO()

        'Me.TranslateGridControls(Grid)
        SetButtonsState()
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Private Sub SortAndBindGrid()
        'Me.State.PageIndex = Me.Grid.CurrentPageIndex
        'Me.Grid.DataSource = Me.State.searchDV
        'HighLightSortColumn(Grid, Me.State.SortExpression)

        State.PageIndex = Grid.PageIndex
        If (State.searchDV.Count = 0) Then

            State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, SortDirection)
        Else
            State.bnoRow = False
            Grid.Enabled = True
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()
        End If

        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

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

    Private Sub PopulateBOFromForm()

        Try
            With State.Manufacturer
                .Description = CType(Grid.Rows(Grid.EditIndex).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text.Trim
                '.Code = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Grid.EditIndex
        Try
            With State.Manufacturer
                If .Description IsNot Nothing Then
                    CType(Grid.Rows(gridRowIdx).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                End If
                'If Not .Code Is Nothing Then
                '    CType(Me.Grid.Items(gridRowIdx).Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text = .Code
                'End If
                CType(Grid.Rows(gridRowIdx).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

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
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
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
            'Linkbutton_panel.Enabled = False
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
            'Linkbutton_panel.Enabled = True
        End If

    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub


#End Region

#Region " Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = e.NewPageIndex
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer

            If (e.CommandName = EDIT_COMMAND) Then
                'Do the Edit here
                index = CInt(e.CommandArgument)
                'Set the IsEditMode flag to TRUE
                State.IsEditMode = True

                State.ManufacturerId = New Guid(CType(Grid.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)

                State.Manufacturer = New Assurant.ElitaPlus.BusinessObjectsNew.Manufacturer(State.ManufacturerId)

                PopulateGrid()

                State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                SetFocusOnEditableFieldInGrid(Grid, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, index)

                PopulateFormFromBO()

                SetButtonsState()

            ElseIf (e.CommandName = DELETE_COMMAND) Then
                'Do the delete here
                index = CInt(e.CommandArgument)
                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                State.ManufacturerId = New Guid(CType(Grid.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)

                State.Manufacturer = New Assurant.ElitaPlus.BusinessObjectsNew.Manufacturer(State.ManufacturerId)
                Try
                    State.Manufacturer.Delete()
                    'Call the Save() method in the Manufacturer Business Object here
                    State.Manufacturer.Save()
                Catch ex As Exception
                    State.Manufacturer.RejectChanges()
                    Throw ex
                End Try

                State.PageIndex = Grid.PageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                State.IsAfterSave = True
                State.searchDV = Nothing
                PopulateGrid()
                State.PageIndex = Grid.PageIndex

            ElseIf ((e.CommandName = SORT_COMMAND) AndAlso Not (IsEditing)) Then


            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound

        Try
            BaseItemBound(source, e)
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub
    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        If dvRow IsNot Nothing AndAlso Not State.bnoRow Then

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                CType(e.Row.Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow("manufacturer_id"), Byte()))


                If (State.IsEditMode = True _
                        AndAlso State.ManufacturerId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow("manufacturer_id"), Byte())))) Then
                    CType(e.Row.Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = dvRow("DESCRIPTION").ToString
                Else
                    CType(e.Row.Cells(DESCRIPTION_COL).FindControl("DescriptionLabel"), Label).Text = dvRow("DESCRIPTION").ToString

                End If
                'e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString
            End If
        End If
    End Sub

    Protected Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)


    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            State.SortExpression = SortDirection
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.Manufacturer, "Description", Grid.Columns(DESCRIPTION_COL))
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub

#End Region

End Class
