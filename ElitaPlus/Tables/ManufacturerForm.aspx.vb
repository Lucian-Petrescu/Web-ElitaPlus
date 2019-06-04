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
            IsEditing = (Me.Grid.EditIndex > NO_ROW_SELECTED_INDEX)
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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        Try
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            'Me.State.PageIndex = Grid.CurrentPageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

        Try
            ClearSearchCriteria()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub ClearSearchCriteria()

        Try
            SearchDescriptionTextBox.Text = String.Empty

            'Update Page State
            With Me.State
                .DescriptionMask = SearchDescriptionTextBox.Text
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

        Try
            Me.State.IsEditMode = True
            Me.State.IsGridVisible = True
            Me.State.AddingNewRow = True
            AddNewManufacturer()
            SetButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            PopulateBOFromForm()
            If (Me.State.Manufacturer.IsDirty) Then
                Me.State.Manufacturer.Save()
                Me.State.IsAfterSave = True
                Me.State.AddingNewRow = False
                Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                Me.State.searchDV = Nothing
                Me.ReturnFromEditing()
            Else
                Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click

        Try
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            Me.State.Canceling = True
            If (Me.State.AddingNewRow) Then
                Me.State.AddingNewRow = False
                Me.State.searchDV = Nothing
            End If
            ReturnFromEditing()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        Try
            ErrController.Clear_Hide()
            Me.SetStateProperties()
            If Not Page.IsPostBack Then
                Me.SortDirection = Me.State.SortExpression
                Me.SetDefaultButton(Me.SearchDescriptionTextBox, Me.SearchButton)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.TranslateGridHeader(Me.Grid)
                Me.TranslateGridControls(Me.Grid)
                Me.SetGridItemStyleColor(Grid)
                Me.State.PageIndex = 0
                SetButtonsState()
            End If
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
        Me.ShowMissingTranslations(ErrController)
    End Sub

    Private Sub PopulateGrid()

        Dim dv As DataView
        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView
            'dv = GetDV()
            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = GetDV()
            End If
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            Me.State.searchDV.Sort = Me.State.SortExpression
            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ManufacturerId, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ManufacturerId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
            End If

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.DESCRIPTION_COL).SortExpression = Manufacturer.COL_NAME_DESCRIPTION

            Me.SortAndBindGrid()

            'Me.TranslateGridControls(Grid)
            'Grid.DataSource = dv
            'Me.Grid.DataBind()

            'Me.trPageSize.Visible = Me.Grid.Visible

            'Session("recCount") = dv.Count

            'If Me.Grid.Visible Then

            '    Me.lblRecordCount.Text = dv.Count & " " & Me.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            'End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        Me.State.searchDV = GetGridDataView()
        Me.State.searchDV.Sort = Grid.DataMember()

        Return (Me.State.searchDV)

    End Function

    Private Function GetGridDataView() As DataView

        With State
            Return (Assurant.ElitaPlus.BusinessObjectsNew.Manufacturer.LoadList(.DescriptionMask, .CompanyGroupId))
        End With

    End Function

    Private Sub SetStateProperties()

        Me.State.DescriptionMask = SearchDescriptionTextBox.Text
        Me.State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

    End Sub

    Private Sub AddNewManufacturer()

        Dim dv As DataView

        Me.State.searchDV = GetGridDataView()

        Me.State.Manufacturer = New Assurant.ElitaPlus.BusinessObjectsNew.Manufacturer
        Me.State.ManufacturerId = Me.State.Manufacturer.Id

        Me.State.searchDV = Me.State.Manufacturer.GetNewDataViewRow(Me.State.searchDV, Me.State.ManufacturerId)

        Grid.DataSource = Me.State.searchDV

        Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ManufacturerId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

        Grid.DataBind()

        Me.State.PageIndex = Grid.PageIndex

        SetGridControls(Me.Grid, False)

        'Set focus on the Description TextBox for the EditItemIndex row
        Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, Me.Grid.EditIndex)

        Me.PopulateFormFromBO()

        'Me.TranslateGridControls(Grid)
        Me.SetButtonsState()
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Private Sub SortAndBindGrid()
        'Me.State.PageIndex = Me.Grid.CurrentPageIndex
        'Me.Grid.DataSource = Me.State.searchDV
        'HighLightSortColumn(Grid, Me.State.SortExpression)

        Me.State.PageIndex = Me.Grid.PageIndex
        If (Me.State.searchDV.Count = 0) Then

            Me.State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
        Else
            Me.State.bnoRow = False
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
        End If

        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

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

    Private Sub PopulateBOFromForm()

        Try
            With Me.State.Manufacturer
                .Description = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text.Trim
                '.Code = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Me.Grid.EditIndex
        Try
            With Me.State.Manufacturer
                If Not .Description Is Nothing Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                End If
                'If Not .Code Is Nothing Then
                '    CType(Me.Grid.Items(gridRowIdx).Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text = .Code
                'End If
                CType(Me.Grid.Rows(gridRowIdx).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = .Id.ToString
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If Me.Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        Me.State.IsEditMode = False
        Me.PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
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
            'Linkbutton_panel.Enabled = False
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
            'Linkbutton_panel.Enabled = True
        End If

    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub


#End Region

#Region " Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = e.NewPageIndex
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer

            If (e.CommandName = Me.EDIT_COMMAND) Then
                'Do the Edit here
                index = CInt(e.CommandArgument)
                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode = True

                Me.State.ManufacturerId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                Me.State.Manufacturer = New Assurant.ElitaPlus.BusinessObjectsNew.Manufacturer(Me.State.ManufacturerId)

                Me.PopulateGrid()

                Me.State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Me.Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, index)

                Me.PopulateFormFromBO()

                Me.SetButtonsState()

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                'Do the delete here
                index = CInt(e.CommandArgument)
                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                Me.State.ManufacturerId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                Me.State.Manufacturer = New Assurant.ElitaPlus.BusinessObjectsNew.Manufacturer(Me.State.ManufacturerId)
                Try
                    Me.State.Manufacturer.Delete()
                    'Call the Save() method in the Manufacturer Business Object here
                    Me.State.Manufacturer.Save()
                Catch ex As Exception
                    Me.State.Manufacturer.RejectChanges()
                    Throw ex
                End Try

                Me.State.PageIndex = Grid.PageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                Me.State.IsAfterSave = True
                Me.State.searchDV = Nothing
                PopulateGrid()
                Me.State.PageIndex = Grid.PageIndex

            ElseIf ((e.CommandName = Me.SORT_COMMAND) AndAlso Not (Me.IsEditing)) Then


            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound

        Try
            BaseItemBound(source, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub
    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        If Not dvRow Is Nothing And Not Me.State.bnoRow Then

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                CType(e.Row.Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow("manufacturer_id"), Byte()))


                If (Me.State.IsEditMode = True _
                        AndAlso Me.State.ManufacturerId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow("manufacturer_id"), Byte())))) Then
                    CType(e.Row.Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text = dvRow("DESCRIPTION").ToString
                Else
                    CType(e.Row.Cells(Me.DESCRIPTION_COL).FindControl("DescriptionLabel"), Label).Text = dvRow("DESCRIPTION").ToString

                End If
                'e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString
            End If
        End If
    End Sub

    Protected Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)


    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            Me.State.SortExpression = Me.SortDirection
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.Manufacturer, "Description", Me.Grid.Columns(Me.DESCRIPTION_COL))
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub

#End Region

End Class
