Imports System.Collections.Generic

Public Class ElitaPlusSearchPage
    Inherits ElitaPlusPage

#Region "Constants"
    'Public Const NO_ITEM_SELECTED_INDEX As Integer = -1
    'Public Const NO_PAGE_INDEX As Integer = 0
    Public Const SELECTED_GUID_COL As Integer = 0

    Public Const GRID_NORMAL_HEADER_COLOR As String = "#12135B"

    Public Const EDIT_COL As Integer = 0
    Public Const DELETE_COL As Integer = 1
    Public Const EDIT_CONTROL_NAME As String = "EditButton_WRITE"
    Public Const DELETE_CONTROL_NAME As String = "DeleteButton_WRITE"
    Public Const CANCEL_CONTROL_NAME As String = "CancelButton_WRITE"
    Public Const SAVE_CONTROL_NAME As String = "SaveButton_WRITE"
    Public Const EDIT_COMMAND_NAME As String = "EditRecord"
    Public Const SAVE_COMMAND_NAME As String = "SaveRecord"
    Public Const CANCEL_COMMAND_NAME As String = "CancelRecord"
    Public Const SELECT_COMMAND_NAME As String = "SelectRecord"
    Public Const DELETE_COMMAND_NAME As String = "DeleteRecord"
    Public Const HISTORY_COMMAND_NAME As String = "HistoryRecord"
    Public Const SORT_COMMAND_NAME As String = "Sort"
    Public Const PAGE_COMMAND_NAME As String = "Page"
    Public Const EDIT As String = "Edit"
    Public Const DELETE As String = "Delete"

    ' Column Cells
    Public Const CELL_NO_CONTROL_SIZE As Integer = 0
    Public Const CELL_BOUND_CONTROL_SIZE As Integer = 1
    Public Const CELL_BOUND_CONTROL_POS As Integer = 0
    Public Const CELL_TEMPLATE_CONTROL_POS As Integer = 1
    Public Const CELL_NEXT_TEMPLATE_CONTROL_POS As Integer = 3
    Public Const CELL_NEXT_TEMPLATE_CONTROL_SIZE As Integer = 5

    ' Populate Actions
    Public Const POPULATE_ACTION_NONE As String = "ACTION_NONE"
    Public Const POPULATE_ACTION_SAVE As String = "ACTION_SAVE"
    Public Const POPULATE_ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
    Public Const POPULATE_ACTION_EDIT As String = "ACTION_EDIT"
    Public Const POPULATE_ACTION_NEW As String = "ACTION_NEW"

    Public Const DEFAULT_PAGE_INDEX As Integer = 0
    Public Const DEFAULT_PAGE_SIZE As Integer = 10
    Public Const DEFAULT_NEW_UI_PAGE_SIZE As Integer = 30
    Public Const DEFAULT_NEW_UI_TAB_PAGE_SIZE As Integer = 5

    Public Const INTERFACE_PAGER_CLASS As String = "PAGER_LEFT"


#End Region

#Region "Constructors For Page State"

    Public Sub New(State As Object)
        MyBase.New(State)
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(useExistingState As Boolean)
        MyBase.New(useExistingState)
    End Sub

#End Region

#Region "Handlers"

    Public Overloads Sub BaseItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim newUi As Boolean = IsNewUI
        '-------------------------------------
        'Name:ReasorbTranslation
        'Purpose:Translate any message tobe display
        'Input Values: Message
        'Uses:
        '-------------------------------------
        ' get the type of item being created
        Dim elemType As ListItemType = e.Item.ItemType

        ' make sure it is the pager bar
        If elemType = ListItemType.Pager Then
            ' the pager bar as a whole has the follwoing layout
            ' <TR><TD colspan=x>.....links</TD></TR>
            ' item points to <TR>. The code below moves to <TD>
            Dim pager As TableCell = DirectCast(e.Item.Controls(0), TableCell)
            'To center the pager when use the new master page.

            If (newUi) Then
                pager.CssClass = "gridPager"
            Else
                pager.CssClass = "GRD_PAGER"
            End If



            If CType(sender, DataGrid).PagerStyle.CssClass = INTERFACE_PAGER_CLASS Then
                pager.Attributes.Add("style", "text-align:left;")
            Else
                pager.Attributes.Add("style", "text-align:center;")
            End If


            ' To fix the Pager bug Ticket #1095554
            Dim td0 As TableCell = e.Item.Cells(0)
            Dim td1 As New TableCell
            With td1
                .ColumnSpan = 1
                .Visible = False
            End With
            e.Item.Cells.AddAt(0, td1)
            td0.ColumnSpan = pager.ColumnSpan

            Dim i As Int32 = 0
            Dim bFound As Boolean = False
            For i = 0 To pager.Controls.Count
                Dim obj As Object = pager.Controls(i)

                If obj.GetType.ToString = "System.Web.UI.WebControls.DataGridLinkButton" Then

                    Dim h As LinkButton = CType(obj, LinkButton)
                    'h.Text = "[" & h.Text & "]"


                    If h.Text.Equals("...") Then
                        If (Not newUi) Then
                            If Not bFound Then
                                h.ToolTip = "Previous set of pages"
                                h.Style.Add("BACKGROUND-REPEAT", "no-repeat")
                                h.Style.Add("COLOR", "#dee3e7")
                                h.Style.Add("BACKGROUND-IMAGE", "url(../Navigation/images/arrow_back.gif)")
                                bFound = True
                            Else
                                h.ToolTip = "Next set of pages"
                                h.Text = ".        .."
                                h.Style.Add("COLOR", "#dee3e7")
                                h.Style.Add("BACKGROUND-REPEAT", "no-repeat")
                                h.Style.Add("BACKGROUND-IMAGE", "url(../Navigation/images/arrow_foward.gif)")
                            End If
                        Else
                            If Not bFound Then
                                h.ToolTip = "Previous set of pages"
                                bFound = True
                            Else
                                h.ToolTip = "Next set of pages"
                            End If
                        End If
                    End If

                    Dim oDataGrid As DataGrid = CType(sender, DataGrid)
                    If oDataGrid.EditItemIndex = NO_ITEM_SELECTED_INDEX Then
                        h.Enabled = True
                    Else
                        h.Enabled = False
                    End If
                Else
                    bFound = True
                    Dim l As System.Web.UI.WebControls.Label = CType(obj, System.Web.UI.WebControls.Label)
                    If (Not newUi) Then
                        l.Text = TranslationBase.TranslateLabelOrMessage("Page") & l.Text
                    End If
                    l.CssClass = "SELECTED_PAGE"
                End If

                i += 1
            Next
        ElseIf (elemType = ListItemType.AlternatingItem Or elemType = ListItemType.Item Or elemType = ListItemType.SelectedItem) Then
            If (IsNewUI) Then
                e.Item.Attributes.Add("onmouseover", "this.className='over'")
                e.Item.Attributes.Add("onmouseout", "this.className='out'")
            End If
        End If
    End Sub


    Public Overloads Sub BaseItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim newUi As Boolean = IsNewUI
        '-------------------------------------
        'Name:ReasorbTranslation
        'Purpose:Translate any message tobe display
        'Input Values: Message
        'Uses:
        '-------------------------------------
        ' get the type of item being created
        Dim elemType As DataControlRowType = e.Row.RowType

        ' make sure it is the pager bar
        If elemType = DataControlRowType.Pager Then
            ' the pager bar as a whole has the follwoing layout
            ' <TR><TD colspan=x>.....links</TD></TR>
            ' item points to <TR>. The code below moves to <TD>
            Dim pager As TableCell = DirectCast(e.Row.Controls(0), TableCell)
            'To center the pager when use the new master page.

            If (newUi) Then
                pager.CssClass = "gridPager"
            Else
                pager.CssClass = "GRD_PAGER"
            End If

            If CType(sender, GridView).PagerStyle.CssClass = INTERFACE_PAGER_CLASS Then
                pager.Attributes.Add("style", "text-align:left;")
            Else
                pager.Attributes.Add("style", "text-align:center;")
            End If

            ' To fix the Pager bug Ticket #1095554
            Dim td0 As TableCell = e.Row.Cells(0)
            Dim td1 As New TableCell
            With td1
                .ColumnSpan = 1
                .Visible = False
            End With
            e.Row.Cells.AddAt(0, td1)
            td0.ColumnSpan = pager.ColumnSpan

            Dim i As Int32 = 0
            Dim bFound As Boolean = False
            Dim pagerTable As System.Web.UI.WebControls.Table = CType(pager.Controls(0), System.Web.UI.WebControls.Table)

            If CType(sender, GridView).PagerStyle.CssClass <> INTERFACE_PAGER_CLASS Then
                pagerTable.Attributes.Add("align", "center")
            End If


            For i = 0 To pagerTable.Rows(0).Cells.Count - 1
                Dim obj As Object = pagerTable.Rows(0).Cells(i).Controls(0)

                If obj.GetType.ToString = "System.Web.UI.WebControls.DataControlPagerLinkButton" Then

                    Dim h As LinkButton = CType(obj, LinkButton)


                    If h.Text.Equals("...") Then
                        If (Not newUi) Then
                            If Not bFound Then
                                h.ToolTip = "Previous set of pages"
                                h.Style.Add("BACKGROUND-REPEAT", "no-repeat")
                                h.Style.Add("COLOR", "#dee3e7")
                                h.Style.Add("BACKGROUND-IMAGE", "url(../Navigation/images/arrow_back.gif)")
                                bFound = True
                            Else
                                h.ToolTip = "Next set of pages"
                                h.Text = ".        .."
                                h.Style.Add("COLOR", "#dee3e7")
                                h.Style.Add("BACKGROUND-REPEAT", "no-repeat")
                                h.Style.Add("BACKGROUND-IMAGE", "url(../Navigation/images/arrow_foward.gif)")
                            End If
                        Else
                            If Not bFound Then
                                h.ToolTip = "Previous set of pages"
                                bFound = True
                            Else
                                h.ToolTip = "Next set of pages"
                            End If
                        End If
                    End If
                    Dim oDataGrid As GridView = CType(sender, GridView)
                    If oDataGrid.EditIndex = NO_ITEM_SELECTED_INDEX Then
                        h.Enabled = True
                    Else
                        h.Enabled = False
                    End If
                Else
                    bFound = True
                    Dim l As System.Web.UI.WebControls.Label = CType(obj, System.Web.UI.WebControls.Label)
                    If (Not newUi) Then
                        l.Text = TranslationBase.TranslateLabelOrMessage("Page") & l.Text
                    End If

                    l.CssClass = "SELECTED_PAGE"

                End If

            Next
        ElseIf (elemType = DataControlRowType.DataRow) Then
            If (IsNewUI) Then
                e.Row.Attributes.Add("onmouseover", "this.className='over'")
                e.Row.Attributes.Add("onmouseout", "this.className='out'")
            End If
        End If
    End Sub

    Protected Overloads Sub BaseItemBound(source As Object, e As DataGridItemEventArgs)

        If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse _
            e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem) Then

            Dim del As ImageButton = CType(e.Item.Cells(DELETE_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
            If (del IsNot Nothing) Then
                Dim tlTip As String = TranslationBase.TranslateLabelOrMessage(DELETE)
                del.ToolTip = tlTip
                '   Me.AddConfirmation(del, Message.DELETE_RECORD_PROMPT)
                AddControlMsg(del, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, _
                                                                    MSG_TYPE_CONFIRM, True)
            End If
        End If

        If (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse _
            e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem) Then

            Dim edt As ImageButton = CType(e.Item.Cells(EDIT_COL).FindControl(EDIT_CONTROL_NAME), ImageButton)
            If (Not (edt Is Nothing)) Then
                Dim tlTip As String = TranslationBase.TranslateLabelOrMessage(EDIT)
                edt.ToolTip = tlTip
            End If
        End If

    End Sub

    Protected Overloads Sub BaseItemBound(source As Object, e As GridViewRowEventArgs)
        If (e.Row.RowType = ListItemType.Item OrElse e.Row.RowType = ListItemType.AlternatingItem OrElse _
                    e.Row.RowType = ListItemType.EditItem OrElse e.Row.RowType = ListItemType.SelectedItem) AndAlso _
                    e.Row.RowType <> DataControlRowType.Pager Then
            Dim edt As ImageButton


            For Each tc As TableCell In e.Row.Cells
                tc.Attributes("style") = "border-color:#999999"
            Next

            If e.Row.Cells(0).Controls.Count > 0 Then
                For Each ctl As Control In e.Row.Cells(0).Controls
                    If ctl.GetType.Equals(GetType(ImageButton)) Then
                        edt = CType(ctl, ImageButton)
                    End If
                Next
            End If
            If (Not (edt Is Nothing)) Then
                Dim tlTip As String = TranslationBase.TranslateLabelOrMessage(EDIT)
                edt.ToolTip = tlTip
            End If
        End If

        If (e.Row.RowType = ListItemType.Item OrElse e.Row.RowType = ListItemType.AlternatingItem OrElse _
                    e.Row.RowType = ListItemType.EditItem OrElse e.Row.RowType = ListItemType.SelectedItem) AndAlso _
                    e.Row.RowType <> DataControlRowType.Pager Then
            Dim del As ImageButton
            If e.Row.Cells(1).Controls.Count > 0 Then
                For Each ctl As Control In e.Row.Cells(1).Controls
                    If ctl.GetType.Equals(GetType(ImageButton)) Then
                        del = CType(ctl, ImageButton)
                    End If
                Next
            End If
            If (del IsNot Nothing) Then
                Dim tlTip As String = TranslationBase.TranslateLabelOrMessage(DELETE)
                del.ToolTip = tlTip
                '   Me.AddConfirmation(del, Message.DELETE_RECORD_PROMPT)
                'Me.AddControlMsg(del, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
                '                                                    Me.MSG_TYPE_CONFIRM, True)
            End If
        End If
    End Sub

    Protected Overloads Sub BaseItemCreated(sender As Repeater, e As System.Web.UI.WebControls.RepeaterItemEventArgs, Optional ByVal pagerCellControlId As String = "moPagerCell")
        Dim oPagedDataSource As PagedDataSource = DirectCast(sender.DataSource, PagedDataSource)
        Dim moPagerCell As HtmlTableCell = DirectCast(e.Item.FindControl(pagerCellControlId), HtmlTableCell)
        Dim cell As HtmlTableCell
        Dim moPagerBodyRow As HtmlTableRow
        If (e.Item.ItemType = ListItemType.Header OrElse e.Item.ItemType = ListItemType.Footer) Then
            ' If Data being displayed is only of one Page, let Pager be invisible
            If (oPagedDataSource.PageCount > 1) Then
                ' Display moPagerRow
                DirectCast(moPagerCell.Parent, HtmlTableRow).Style.Remove("display")
                ' Add TDs to moPagerBodyRow

                Dim moPagerTable As HtmlTable
                moPagerTable = New HtmlTable()
                moPagerTable.Attributes.Add("align", "center")
                moPagerTable.Attributes.Add("border", "0")
                moPagerBodyRow = New HtmlTableRow()
                moPagerTable.Rows.Add(moPagerBodyRow)
                moPagerCell.Controls.Add(moPagerTable)

                For pageNumber As Integer = 1 To oPagedDataSource.PageCount
                    cell = New HtmlTableCell
                    If (pageNumber = oPagedDataSource.CurrentPageIndex + 1) Then
                        Dim span As New HtmlGenericControl("span")
                        span.InnerHtml = pageNumber.ToString()
                        span.Attributes.Add("class", "SELECTED_PAGE")
                        cell.Controls.Add(span)
                    Else
                        Dim pagerButton As HtmlAnchor
                        pagerButton = New HtmlAnchor()
                        pagerButton.HRef = String.Format("javascript:__doPostBack('', '{0}:{1}');", PAGE_COMMAND_NAME, (pageNumber - 1).ToString())
                        pagerButton.InnerText = pageNumber.ToString()
                        cell.Controls.Add(pagerButton)
                    End If
                    moPagerBodyRow.Cells.Add(cell)
                Next
            Else
                DirectCast(moPagerCell.Parent, HtmlTableRow).Style("display") = "none"
            End If
        End If
    End Sub
#End Region

#Region "Navigation"

    'This method returns the row index of the selected guid
    Public Function FindSelectedRowIndexFromGuid(dv As DataView, selectedGuid As Guid) As Integer

        Dim selectedRowIndex As Integer = NO_ITEM_SELECTED_INDEX
        If (Not (selectedGuid.Equals(Guid.Empty))) Then
            'Jump to the Right Page
            Dim i As Integer
            For i = 0 To dv.Count - 1
                If Not String.IsNullOrEmpty(dv(i)(SELECTED_GUID_COL).ToString) then
                    If (New Guid(CType(dv(i)(SELECTED_GUID_COL), Byte())).Equals(selectedGuid)) Then
                        selectedRowIndex = i
                        Return (selectedRowIndex)
                    End If
                End If
            Next
        End If
        Return (selectedRowIndex)
    End Function

    Public Function FindSelectedRowIndexFromGuid(Of TType)(oDataSource As IList(Of TType), selectedItemCondition As Func(Of TType, Boolean)) As Integer

        'Jump to the Right Page
        Dim i As Integer
        For Each oItem As TType In oDataSource
            If selectedItemCondition(oItem) Then
                Return i
            End If
            i = i + 1
        Next
        Return NO_ITEM_SELECTED_INDEX
    End Function

    'This method returns the row index of the selected guid
    'Unique Id must be assigned to BO for it to work
    Public Function FindSelectedRowIndexFromGuid(oBusinessObjectList As BusinessObjectListBase, selectedGuid As Guid) As Integer

        Dim selectedRowIndex As Integer = NO_ITEM_SELECTED_INDEX
        If (Not (selectedGuid.Equals(Guid.Empty))) Then
            'Jump to the Right Page
            Dim i As Integer
            For i = 0 To oBusinessObjectList.Count - 1
                If (New Guid(CType(oBusinessObjectList(i), BusinessObjectBase).UniqueId).Equals(selectedGuid)) Then
                    selectedRowIndex = i
                    Return (selectedRowIndex)
                End If
            Next
        End If
        Return (selectedRowIndex)
    End Function

    'This method sets the Page Index and the Selected Index on the DataGrid
    Public Overloads Sub SetPageAndSelectedIndexFromGuid(oDataView As DataView, selectedGuid As Guid, oGrid As DataGrid, _
                                               nLastSelectedPageIndex As Integer, Optional ByVal isEditing As Boolean = False)
        Dim nSelectedRow As Integer
        Dim nCurrentLastPageIndex As Integer = ((oDataView.Count - 1) \ oGrid.PageSize)

        nSelectedRow = FindSelectedRowIndexFromGuid(oDataView, selectedGuid)
        oGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
        oGrid.EditItemIndex = NO_ITEM_SELECTED_INDEX
        If (nSelectedRow > NO_ITEM_SELECTED_INDEX) Then
            ' The Guid was found in the datagrid, Therefore go to the page of this guid
            oGrid.CurrentPageIndex = (nSelectedRow \ oGrid.PageSize)
            oGrid.SelectedIndex = (nSelectedRow Mod oGrid.PageSize)
            If (isEditing) Then
                oGrid.EditItemIndex = oGrid.SelectedIndex
            End If
        ElseIf oDataView.Count = 0 Then
            ' No Select Page because the Grid is Empty
            oGrid.CurrentPageIndex = NO_PAGE_INDEX
        ElseIf nLastSelectedPageIndex < nCurrentLastPageIndex Then
            ' Go to the last selected page
            oGrid.CurrentPageIndex = nLastSelectedPageIndex
        Else
            ' Go to the last page. 
            oGrid.CurrentPageIndex = nCurrentLastPageIndex
        End If
        If isEditing Then
            oGrid.AllowSorting = False
        Else
            oGrid.AllowSorting = True
        End If
    End Sub

    Public Overloads Sub SetPageAndSelectedIndexFromGuid(Of TType)(oDataSource As IList(Of TType), selectedItemCondition As Func(Of TType, Boolean), oGrid As DataGrid, _
                                               nLastSelectedPageIndex As Integer, Optional ByVal isEditing As Boolean = False)
        Dim nSelectedRow As Integer
        Dim nCurrentLastPageIndex As Integer = ((oDataSource.Count - 1) \ oGrid.PageSize)

        nSelectedRow = FindSelectedRowIndexFromGuid(oDataSource, selectedItemCondition)
        oGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
        oGrid.EditItemIndex = NO_ITEM_SELECTED_INDEX
        If (nSelectedRow > NO_ITEM_SELECTED_INDEX) Then
            ' The Guid was found in the datagrid, Therefore go to the page of this guid
            oGrid.CurrentPageIndex = (nSelectedRow \ oGrid.PageSize)
            oGrid.SelectedIndex = (nSelectedRow Mod oGrid.PageSize)
            If (isEditing) Then
                oGrid.EditItemIndex = oGrid.SelectedIndex
            End If
        ElseIf oDataSource.Count = 0 Then
            ' No Select Page because the Grid is Empty
            oGrid.CurrentPageIndex = NO_PAGE_INDEX
        ElseIf nLastSelectedPageIndex < nCurrentLastPageIndex Then
            ' Go to the last selected page
            oGrid.CurrentPageIndex = nLastSelectedPageIndex
        Else
            ' Go to the last page. 
            oGrid.CurrentPageIndex = nCurrentLastPageIndex
        End If
        If isEditing Then
            oGrid.AllowSorting = False
        Else
            oGrid.AllowSorting = True
        End If
    End Sub

    Public Overloads Sub SetPageAndSelectedIndexFromGuid(Of TType)(oDataSource As IList(Of TType), selectedItemCondition As Func(Of TType, Boolean), oGrid As GridView, _
                                               nLastSelectedPageIndex As Integer, Optional ByVal isEditing As Boolean = False)
        Dim nSelectedRow As Integer
        Dim nCurrentLastPageIndex As Integer = ((oDataSource.Count - 1) \ oGrid.PageSize)

        nSelectedRow = FindSelectedRowIndexFromGuid(oDataSource, selectedItemCondition)
        oGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
        oGrid.EditIndex = NO_ITEM_SELECTED_INDEX
        If (nSelectedRow > NO_ITEM_SELECTED_INDEX) Then
            ' The Guid was found in the datagrid, Therefore go to the page of this guid
            oGrid.PageIndex = (nSelectedRow \ oGrid.PageSize)
            oGrid.SelectedIndex = (nSelectedRow Mod oGrid.PageSize)
            If (isEditing) Then
                oGrid.EditIndex = oGrid.SelectedIndex
            End If
        ElseIf oDataSource.Count = 0 Then
            ' No Select Page because the Grid is Empty
            oGrid.PageIndex = NO_PAGE_INDEX
        ElseIf nLastSelectedPageIndex < nCurrentLastPageIndex Then
            ' Go to the last selected page
            oGrid.PageIndex = nLastSelectedPageIndex
        Else
            ' Go to the last page. 
            oGrid.PageIndex = nCurrentLastPageIndex
        End If
        If isEditing Then
            oGrid.AllowSorting = False
        Else
            oGrid.AllowSorting = True
        End If
    End Sub

    'This method sets the Page Index and the Selected Index on the DataGrid
    Public Overloads Sub SetPageAndSelectedIndexFromGuid(oDataView As DataView, selectedGuid As Guid, oGrid As GridView, _
                                               nLastSelectedPageIndex As Integer, Optional ByVal isEditing As Boolean = False)
        Dim nSelectedRow As Integer
        Dim nCurrentLastPageIndex As Integer = ((oDataView.Count - 1) \ oGrid.PageSize)

        nSelectedRow = FindSelectedRowIndexFromGuid(oDataView, selectedGuid)
        oGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
        oGrid.EditIndex = NO_ITEM_SELECTED_INDEX
        If (nSelectedRow > NO_ITEM_SELECTED_INDEX) Then
            ' The Guid was found in the datagrid, Therefore go to the page of this guid
            oGrid.PageIndex = (nSelectedRow \ oGrid.PageSize)
            oGrid.SelectedIndex = (nSelectedRow Mod oGrid.PageSize)
            If (isEditing) Then
                oGrid.EditIndex = oGrid.SelectedIndex
            End If
        ElseIf oDataView.Count = 0 Then
            ' No Select Page because the Grid is Empty
            oGrid.PageIndex = NO_PAGE_INDEX
        ElseIf nLastSelectedPageIndex < nCurrentLastPageIndex Then
            ' Go to the last selected page
            oGrid.PageIndex = nLastSelectedPageIndex
        Else
            ' Go to the last page. 
            oGrid.PageIndex = nCurrentLastPageIndex
        End If
        If isEditing Then
            oGrid.AllowSorting = False
        Else
            oGrid.AllowSorting = True
        End If
    End Sub


    'This method sets the Page Index and the Selected Index on the GridView
    'Unique Id must be assigned to BO for it to work
    Public Overloads Sub SetPageAndSelectedIndexFromGuid(oBusinessObjectList As BusinessObjectListBase, selectedGuid As Guid, oGrid As GridView, _
                                               nLastSelectedPageIndex As Integer, Optional ByVal isEditing As Boolean = False)
        Dim nSelectedRow As Integer
        Dim nCurrentLastPageIndex As Integer = ((oBusinessObjectList.Count - 1) \ oGrid.PageSize)

        nSelectedRow = FindSelectedRowIndexFromGuid(oBusinessObjectList, selectedGuid)
        oGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
        oGrid.EditIndex = NO_ITEM_SELECTED_INDEX
        If (nSelectedRow > NO_ITEM_SELECTED_INDEX) Then
            ' The Guid was found in the datagrid, Therefore go to the page of this guid
            oGrid.PageIndex = (nSelectedRow \ oGrid.PageSize)
            oGrid.SelectedIndex = (nSelectedRow Mod oGrid.PageSize)
            If (isEditing) Then
                oGrid.EditIndex = oGrid.SelectedIndex
            End If
        ElseIf oBusinessObjectList.Count = 0 Then
            ' No Select Page because the Grid is Empty
            oGrid.PageIndex = NO_PAGE_INDEX
        ElseIf nLastSelectedPageIndex < nCurrentLastPageIndex Then
            ' Go to the last selected page
            oGrid.PageIndex = nLastSelectedPageIndex
        Else
            ' Go to the last page. 
            oGrid.PageIndex = nCurrentLastPageIndex
        End If
        If isEditing Then
            oGrid.AllowSorting = False
        Else
            oGrid.AllowSorting = True
        End If
    End Sub

#End Region

#Region "Enable-Disable"

    'This method enables or disables the EDIT and DELETE buttons for all the rows on a DataGrid 
    'based on the flag passed into it.

    Public Overloads Shared Sub SetGridControls(grid As DataGrid, enable As Boolean)
        Dim i As Integer
        Dim edt As ImageButton
        Dim del As ImageButton

        grid.AllowSorting = enable  ' Enable/Disable the sorting
        '  Enable or Disable all the EDIT and DELETE buttons on the DataGrid
        For i = 0 To (grid.Items.Count - 1)
            edt = CType(grid.Items(i).Cells(EDIT_COL).FindControl(EDIT_CONTROL_NAME), ImageButton)
            If edt IsNot Nothing Then
                edt.Enabled = enable
                edt.Visible = enable
            End If

            del = CType(grid.Items(i).Cells(DELETE_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
            If del IsNot Nothing Then
                del.Enabled = enable
                del.Visible = enable
            End If
        Next

    End Sub

    'This method enables or disables the EDIT and DELETE buttons for all the rows on a DataGrid 
    'based on the flag passed into it.

    Public Overloads Shared Sub SetGridControls(grid As GridView, enable As Boolean)
        Dim i As Integer
        Dim edt As ImageButton
        Dim del As ImageButton

        grid.AllowSorting = enable  ' Enable/Disable the sorting
        '  Enable or Disable all the EDIT and DELETE buttons on the DataGrid
        For i = 0 To (grid.Rows.Count - 1)
            edt = CType(grid.Rows(i).Cells(EDIT_COL).FindControl(EDIT_CONTROL_NAME), ImageButton)
            If edt IsNot Nothing Then
                edt.Enabled = enable
                edt.Visible = enable
            End If

            del = CType(grid.Rows(i).Cells(DELETE_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
            If del IsNot Nothing Then
                del.Enabled = enable
                del.Visible = enable
            End If
        Next

    End Sub

#End Region

#Region "Cells Access"

    Public Overloads Function GetGridText(oDataGrid As DataGrid, rowPosition As Integer, cellPosition As Integer) As String
        Dim oItem As DataGridItem = oDataGrid.Items(rowPosition)
        Dim oControl As Control
        Dim sText As String

        If oItem.Cells(cellPosition).Controls.Count > CELL_NO_CONTROL_SIZE Then
            If oItem.Cells(cellPosition).Controls.Count = CELL_BOUND_CONTROL_SIZE Then
                oControl = oItem.Cells(cellPosition).Controls(CELL_BOUND_CONTROL_POS)
            Else
                oControl = oItem.Cells(cellPosition).Controls(CELL_TEMPLATE_CONTROL_POS)
            End If
            If TypeOf oControl Is Label Then
                sText = CType(oControl, Label).Text
            Else
                sText = CType(oControl, TextBox).Text
            End If
        Else
            ' No Controls
            sText = oItem.Cells(cellPosition).Text
        End If

        Return sText
    End Function

    Public Overloads Function GetGridText(oDataGrid As GridView, rowPosition As Integer, cellPosition As Integer) As String
        Dim oItem As GridViewRow = oDataGrid.Rows(rowPosition)
        Dim oControl As Control
        Dim sText As String

        If oItem.Cells(cellPosition).Controls.Count > CELL_NO_CONTROL_SIZE Then
            If oItem.Cells(cellPosition).Controls.Count = CELL_BOUND_CONTROL_SIZE Then
                oControl = oItem.Cells(cellPosition).Controls(CELL_BOUND_CONTROL_POS)
            Else
                oControl = oItem.Cells(cellPosition).Controls(CELL_TEMPLATE_CONTROL_POS)
            End If
            If TypeOf oControl Is Label Then
                sText = CType(oControl, Label).Text
            ElseIf TypeOf oControl Is LinkButton Then
                sText = CType(oControl, LinkButton).Text
            Else
                sText = CType(oControl, TextBox).Text
            End If
        Else
            ' No Controls
            sText = oItem.Cells(cellPosition).Text
        End If

        Return sText
    End Function

    Public Overloads Sub SetGridText(oDataGrid As DataGrid, rowPosition As Integer, cellPosition As Integer, sText As String)
        Dim oItem As DataGridItem = oDataGrid.Items(rowPosition)
        Dim oControl As Control

        If oItem.Cells(cellPosition).Controls.Count > CELL_NO_CONTROL_SIZE Then
            If oItem.Cells(cellPosition).Controls.Count = CELL_BOUND_CONTROL_SIZE Then
                oControl = oItem.Cells(cellPosition).Controls(CELL_BOUND_CONTROL_POS)
            Else
                oControl = oItem.Cells(cellPosition).Controls(CELL_TEMPLATE_CONTROL_POS)
            End If
            If TypeOf oControl Is Label Then
                CType(oControl, Label).Text = sText
            Else
                CType(oControl, TextBox).Text = sText
            End If
        Else
            ' No Control
            oItem.Cells(cellPosition).Text = sText
        End If
    End Sub

    Public Overloads Sub SetGridText(oDataGrid As GridView, rowPosition As Integer, cellPosition As Integer, sText As String)
        Dim oItem As GridViewRow = oDataGrid.Rows(rowPosition)
        Dim oControl As Control

        If oItem.Cells(cellPosition).Controls.Count > CELL_NO_CONTROL_SIZE Then
            If oItem.Cells(cellPosition).Controls.Count = CELL_BOUND_CONTROL_SIZE Then
                oControl = oItem.Cells(cellPosition).Controls(CELL_BOUND_CONTROL_POS)
            Else
                oControl = oItem.Cells(cellPosition).Controls(CELL_TEMPLATE_CONTROL_POS)
            End If
            If TypeOf oControl Is Label Then
                CType(oControl, Label).Text = sText
            Else
                CType(oControl, TextBox).Text = sText
            End If
        Else
            ' No Control
            oItem.Cells(cellPosition).Text = sText
        End If
    End Sub

    Public Function GetSelectedGridText(oDataGrid As DataGrid, cellPosition As Integer) As String
        Return GetGridText(oDataGrid, oDataGrid.SelectedIndex, cellPosition)
    End Function

    Public Function GetSelectedGridText(oDataGrid As GridView, cellPosition As Integer) As String
        Return GetGridText(oDataGrid, oDataGrid.SelectedIndex, cellPosition)
    End Function

    Public Overloads Sub SetSelectedGridText(oDataGrid As DataGrid, cellPosition As Integer, sText As String)
        SetGridText(oDataGrid, oDataGrid.SelectedIndex, cellPosition, sText)
    End Sub

    Public Overloads Sub SetSelectedGridText(oDataGrid As GridView, cellPosition As Integer, sText As String)
        SetGridText(oDataGrid, oDataGrid.SelectedIndex, cellPosition, sText)
    End Sub

    Public Overloads Shared Function GetGridControl(oDataGrid As DataGrid, rowPosition As Integer, cellPosition As Integer, _
    Optional ByVal nextTemplateControl As Boolean = False) As Control
        Dim oItem As DataGridItem = oDataGrid.Items(rowPosition)
        Dim oControl As Control
        Dim sText As String

        If oItem.Cells(cellPosition).Controls.Count = CELL_NO_CONTROL_SIZE Then Return Nothing
        If oItem.Cells(cellPosition).Controls.Count = CELL_BOUND_CONTROL_SIZE Then
            oControl = oItem.Cells(cellPosition).Controls(CELL_BOUND_CONTROL_POS)
        ElseIf nextTemplateControl = False Then
            oControl = oItem.Cells(cellPosition).Controls(CELL_TEMPLATE_CONTROL_POS)
        Else
            oControl = oItem.Cells(cellPosition).Controls(CELL_NEXT_TEMPLATE_CONTROL_POS)
        End If

        Return oControl
    End Function

    Public Overloads Shared Function GetGridControl(oDataGrid As GridView, rowPosition As Integer, cellPosition As Integer, _
    Optional ByVal nextTemplateControl As Boolean = False) As Control
        Dim oItem As GridViewRow = oDataGrid.Rows(rowPosition)
        Dim oControl As Control
        Dim sText As String

        If oItem.Cells(cellPosition).Controls.Count = CELL_NO_CONTROL_SIZE Then Return Nothing
        If oItem.Cells(cellPosition).Controls.Count = CELL_BOUND_CONTROL_SIZE Then
            oControl = oItem.Cells(cellPosition).Controls(CELL_BOUND_CONTROL_POS)
        ElseIf nextTemplateControl = False Then
            oControl = oItem.Cells(cellPosition).Controls(CELL_TEMPLATE_CONTROL_POS)
        Else
            oControl = oItem.Cells(cellPosition).Controls(CELL_NEXT_TEMPLATE_CONTROL_POS)
        End If

        Return oControl
    End Function

    Public Overloads Function GetSelectedGridControl(oDataGrid As DataGrid, cellPosition As Integer) As Control
        Return GetGridControl(oDataGrid, oDataGrid.SelectedIndex, cellPosition)
    End Function

    Public Overloads Function GetSelectedGridControl(oDataGrid As GridView, cellPosition As Integer) As Control
        Return GetGridControl(oDataGrid, oDataGrid.SelectedIndex, cellPosition)
    End Function

    Public Overloads Function GetSelectedGridDropItem(oDataGrid As DataGrid, cellPosition As Integer) As Guid
        Return GetSelectedItem(CType(GetSelectedGridControl(oDataGrid, cellPosition), ListControl))
    End Function

    Public Overloads Function GetSelectedGridDropItem(oDataGrid As GridView, cellPosition As Integer) As Guid
        Return GetSelectedItem(CType(GetSelectedGridControl(oDataGrid, cellPosition), ListControl))
    End Function

    ' Set Focus in the TextBox of a Cell in a DataGrid
    Public Overloads Sub SetFocusInGrid(oDataGrid As DataGrid, rowPosition As Integer, cellPosition As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim oItem As DataGridItem = oDataGrid.Items(rowPosition)
        Dim oText As TextBox

        If oItem.Cells(cellPosition).Controls.Count = CELL_NO_CONTROL_SIZE Then Return ' No Focus
        If oItem.Cells(cellPosition).Controls.Count = CELL_BOUND_CONTROL_SIZE Then
            oText = CType(oItem.Cells(cellPosition).Controls(CELL_BOUND_CONTROL_POS), TextBox)
        Else
            oText = CType(oItem.Cells(cellPosition).Controls(CELL_TEMPLATE_CONTROL_POS), TextBox)
        End If
        SetFocus(oText)
    End Sub

    ' Set Focus in the TextBox of a Cell in a DataGrid
    Public Overloads Sub SetFocusInGrid(oDataGrid As GridView, rowPosition As Integer, cellPosition As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim oItem As GridViewRow = oDataGrid.Rows(rowPosition)
        Dim oText As TextBox

        If oItem.Cells(cellPosition).Controls.Count = CELL_NO_CONTROL_SIZE Then Return ' No Focus
        If oItem.Cells(cellPosition).Controls.Count = CELL_BOUND_CONTROL_SIZE Then
            oText = CType(oItem.Cells(cellPosition).Controls(CELL_BOUND_CONTROL_POS), TextBox)
        Else
            oText = CType(oItem.Cells(cellPosition).Controls(CELL_TEMPLATE_CONTROL_POS), TextBox)
        End If
        SetFocus(oText)
    End Sub

#End Region

#Region "Style"

    Public Overloads Sub SetGridItemStyleColor(oDataGrid As DataGrid)
        oDataGrid.SelectedItemStyle.BackColor = Color.LightSteelBlue
        oDataGrid.EditItemStyle.BackColor = Color.LightSteelBlue
        'oDataGrid.SelectedItemStyle.ForeColor = Color.LimeGreen
        'oDataGrid.HeaderStyle.ForeColor = Color.Magenta
    End Sub

    Public Overloads Sub SetGridItemStyleColor(oDataGrid As GridView)
        oDataGrid.SelectedRowStyle.BackColor = Color.LightSteelBlue
        oDataGrid.EditRowStyle.BackColor = Color.LightSteelBlue
        'oDataGrid.SelectedItemStyle.ForeColor = Color.LimeGreen
        'oDataGrid.HeaderStyle.ForeColor = Color.Magenta
    End Sub

    Public Overloads Shared Sub ClearGridHeaders(oDataGrid As DataGrid)
        Dim oGridCol As DataGridColumn

        For Each oGridCol In oDataGrid.Columns
            oGridCol.HeaderStyle.ForeColor = Color.Empty
        Next
    End Sub

    Public Overloads Shared Sub ClearGridHeaders(oDataGrid As GridView)
        Dim oGridCol As DataControlField

        For Each oGridCol In oDataGrid.Columns
            oGridCol.HeaderStyle.ForeColor = Color.Empty
        Next
    End Sub
#End Region

#Region "Populate"

    Public Sub GetNewDataViewRow(dv As DataView, id As Guid)
        Dim dt As DataTable
        Dim oColumns As DataColumnCollection
        Dim oRow As DataRow
        Dim oItem As Object
        Dim nIndex As Integer

        dt = dv.Table
        oColumns = dt.Columns
        oRow = dt.NewRow

        For nIndex = 0 To oColumns.Count - 1
            If (oColumns(nIndex).DataType() Is GetType(Byte()) = True) Then
                oRow(nIndex) = id.ToByteArray
            ElseIf (oColumns(nIndex).DataType() Is GetType(String) = True) Then
                oRow(nIndex) = String.Empty
            ElseIf (oColumns(nIndex).DataType() Is GetType(Date) = True) Then
                oRow(nIndex) = Date.MinValue
            ElseIf (oColumns(nIndex).DataType() Is GetType(Decimal) = True) Then
                oRow(nIndex) = 0
            End If
        Next

        dt.Rows.Add(oRow)
    End Sub

    Public Overloads Sub BaseAddNewGridRow(oDataGrid As DataGrid, oDataView As DataView, oId As Guid)
        Dim dv As DataView

        SetGridControls(oDataGrid, False)
        dv = oDataView
        GetNewDataViewRow(dv, oId)
        oDataGrid.DataSource = dv
        SetPageAndSelectedIndexFromGuid(dv, oId, oDataGrid, oDataGrid.CurrentPageIndex, True)
        oDataGrid.DataBind()
        '     Me.TranslateGridControls(oDataGrid)
    End Sub

    Public Overloads Sub BaseAddNewGridRow(oDataGrid As GridView, oDataView As DataView, oId As Guid)
        Dim dv As DataView

        SetGridControls(oDataGrid, False)
        dv = oDataView
        GetNewDataViewRow(dv, oId)
        oDataGrid.DataSource = dv
        SetPageAndSelectedIndexFromGuid(dv, oId, oDataGrid, oDataGrid.PageIndex, True)
        oDataGrid.DataBind()
        '     Me.TranslateGridControls(oDataGrid)
    End Sub

    ' The Form should override this routine in order to set the button state (enable/disable, visible/invisible, etc)
    Public Overridable Sub BaseSetButtonsState(bIsEdit As Boolean)

    End Sub

    ' The Form should overrides this routine in order to create a new Bo row in the datagrid
    ' and additional necessary logic
    Public Overridable Sub AddNewBoRow(oDataView As DataView)

    End Sub


    Public Overloads Sub BasePopulateGrid(oDataGrid As DataGrid, oDataView As DataView, oId As Guid, _
                                Optional ByVal oAction As String = POPULATE_ACTION_NONE)
        Select Case oAction
            Case POPULATE_ACTION_NONE
                SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, oDataGrid, 0)
                BaseSetButtonsState(False)
            Case POPULATE_ACTION_SAVE
                SetPageAndSelectedIndexFromGuid(oDataView, oId, oDataGrid, oDataGrid.CurrentPageIndex)
                BaseSetButtonsState(False)
            Case POPULATE_ACTION_NO_EDIT
                SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, oDataGrid, oDataGrid.CurrentPageIndex)
                BaseSetButtonsState(False)
            Case POPULATE_ACTION_EDIT
                SetPageAndSelectedIndexFromGuid(oDataView, oId, oDataGrid, oDataGrid.CurrentPageIndex, True)
                BaseSetButtonsState(True)
            Case POPULATE_ACTION_NEW
                AddNewBoRow(oDataView)
                BaseSetButtonsState(True)
        End Select
        If oAction <> POPULATE_ACTION_NEW Then
            oDataGrid.DataSource = oDataView
            '  Me.TranslateGridHeader(oDataGrid)
            oDataGrid.DataBind()
        End If

    End Sub

    Public Overloads Sub BasePopulateGrid(oDataGrid As GridView, oDataView As DataView, oId As Guid, _
                                Optional ByVal oAction As String = POPULATE_ACTION_NONE)
        Select Case oAction
            Case POPULATE_ACTION_NONE
                SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, oDataGrid, 0)
                BaseSetButtonsState(False)
            Case POPULATE_ACTION_SAVE
                SetPageAndSelectedIndexFromGuid(oDataView, oId, oDataGrid, oDataGrid.PageIndex)
                BaseSetButtonsState(False)
            Case POPULATE_ACTION_NO_EDIT
                SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, oDataGrid, oDataGrid.PageIndex)
                BaseSetButtonsState(False)
            Case POPULATE_ACTION_EDIT
                SetPageAndSelectedIndexFromGuid(oDataView, oId, oDataGrid, oDataGrid.PageIndex, True)
                BaseSetButtonsState(True)
            Case POPULATE_ACTION_NEW
                AddNewBoRow(oDataView)
                BaseSetButtonsState(True)
        End Select
        If oAction <> POPULATE_ACTION_NEW Then
            oDataGrid.DataSource = oDataView
            '  Me.TranslateGridHeader(oDataGrid)
            oDataGrid.DataBind()
        End If

    End Sub

    Protected Function ValidSearchResultCount(count As Int32, Optional ByVal checkForCount As Boolean = False, Optional ByVal msg As String = Message.MSG_NO_RECORDS_FOUND) As Boolean
        If count >= 100 AndAlso checkForCount Then
            'Me.DisplayMessage("Only the first 100 records are shown. Please modify your search criteria.", "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            DisplayMessage(Message.MSG_MAX_LIMIT_EXCEEDED_REFINE_SEARCH_CRITERIA, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Return False
        ElseIf count = 0 Then
            'Me.DisplayMessage("No records were found.", "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            DisplayMessage(msg, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Return False
        Else
            Return True
        End If
    End Function

    Protected Function ValidSearchResultCount(CurrentCount As Int32, MaxCount As Int32, Optional ByVal checkForCount As Boolean = False, Optional ByVal msg As String = Message.MSG_NO_RECORDS_FOUND) As Boolean
        If CurrentCount >= MaxCount AndAlso checkForCount Then
            DisplayMessage(Message.MSG_MAX_LIMIT_EXCEEDED_GENERIC, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Return False
        ElseIf CurrentCount = 0 Then
            'Me.DisplayMessage("No records were found.", "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            DisplayMessage(msg, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Return False
        Else
            Return True
        End If
    End Function

    Protected Function ValidSearchResultCountNew(count As Int32, Optional ByVal checkForCount As Boolean = False, Optional ByVal msg As String = Message.MSG_NO_RECORDS_FOUND) As Boolean
        If count >= 100 AndAlso checkForCount Then
            If IsNewUI Then
                MasterPage.MessageController.AddInformation(Message.MSG_MAX_LIMIT_EXCEEDED_REFINE_SEARCH_CRITERIA, True)
            Else
                DisplayMessage(Message.MSG_MAX_LIMIT_EXCEEDED_REFINE_SEARCH_CRITERIA, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
        ElseIf count = 0 Then
            If IsNewUI Then
                MasterPage.MessageController.AddInformation(msg, True)
            Else
                DisplayMessage(msg, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
        Else
            Return True
        End If
        Return False

    End Function

    Protected Overloads Function NewCurrentPageIndex(dg As DataGrid, intRecordCount As Integer, intNewPageSize As Integer) As Integer
        Dim intOldPageSize As Integer    ' old page size    
        Dim intFirstRecordIndex As Integer        ' top record index on current page
        Dim intNewPageCount As Integer  ' new page count
        Dim intNewCurrentPageIndex As Integer

        intOldPageSize = dg.PageSize      ' is given from the DataGrid PageSize property
        ' identifies the current page
        intFirstRecordIndex = dg.CurrentPageIndex * intOldPageSize + 1

        ' set the new page size for the Data grig
        dg.PageSize = intNewPageSize

        ' The actual page count of the DataGrid control
        ' is the "old" page count.
        ' The new page count of the DataGrid control will be set
        ' automatically after we bind the Datagrid to the data source
        ' with new page size set.
        ' We we need the new page count arleady now to find out the new current page index,
        ' so we must calculate it.        
        intNewPageCount = CType(Math.Ceiling(intRecordCount / intNewPageSize), Integer)
        ' get the new current page index
        Dim i As Integer
        For i = 1 To intNewPageCount
            If intFirstRecordIndex >= (i - 1) * intNewPageSize + 1 And intFirstRecordIndex <= i * intNewPageSize Then
                intNewCurrentPageIndex = i - 1
                Exit For
            End If
        Next i

        NewCurrentPageIndex = intNewCurrentPageIndex

    End Function

    Protected Overloads Function NewCurrentPageIndex(dg As GridView, intRecordCount As Integer, intNewPageSize As Integer) As Integer
        Dim intOldPageSize As Integer    ' old page size    
        Dim intFirstRecordIndex As Integer        ' top record index on current page
        Dim intNewPageCount As Integer  ' new page count
        Dim intNewCurrentPageIndex As Integer

        intOldPageSize = dg.PageSize      ' is given from the DataGrid PageSize property
        ' identifies the current page
        intFirstRecordIndex = dg.PageIndex * intOldPageSize + 1

        ' set the new page size for the Data grig
        dg.PageSize = intNewPageSize

        ' The actual page count of the DataGrid control
        ' is the "old" page count.
        ' The new page count of the DataGrid control will be set
        ' automatically after we bind the Datagrid to the data source
        ' with new page size set.
        ' We we need the new page count arleady now to find out the new current page index,
        ' so we must calculate it.        
        intNewPageCount = CType(Math.Ceiling(intRecordCount / intNewPageSize), Integer)
        ' get the new current page index
        Dim i As Integer
        For i = 1 To intNewPageCount
            If intFirstRecordIndex >= (i - 1) * intNewPageSize + 1 And intFirstRecordIndex <= i * intNewPageSize Then
                intNewCurrentPageIndex = i - 1
                Exit For
            End If
        Next i

        NewCurrentPageIndex = intNewCurrentPageIndex

    End Function

    Protected Overloads Function NewCurrentPageIndex(currentPageIndex As Integer, currentPageSize As Integer, newPageSize As Integer) As Integer
        Return Convert.ToInt32(Math.Ceiling(((currentPageIndex * currentPageSize) + 1) / newPageSize) - 1)
    End Function
#End Region

#Region "Sorting"
    'Public Const UP_ARROW As String = "&nbsp;&uarr;" '"&nbsp;&Delta;" 
    'Public Const DOWN_ARROW As String = "&nbsp;&darr;" '"&nbsp;&nabla;"  
    Public Const UP_ARROW As String = "&nbsp;<img style=""color:DarkSlateBlue;height:8px;COLOR:#dee3e7;BACKGROUND-REPEAT:no-repeat"" border=""0"" src=""../Navigation/images/arrow_up_grid.gif"">"
    Public Const DOWN_ARROW As String = "&nbsp;<img style=""color:DarkSlateBlue;height:8px;COLOR:#dee3e7;BACKGROUND-REPEAT:no-repeat"" border=""0"" src=""../Navigation/images/arrow_down_grid.gif"">"
    Public Const UP_ARROW_IMG_SOURCE As String = "../Navigation/images/arrow_up_grid.gif"
    Public Const DOWN_ARROW_IMG_SOURCE As String = "../Navigation/images/arrow_down_grid.gif"

    'Arrows for new UI
    Public Const UP_ARROW_NEW_UI As String = "&nbsp;<img style=""border=""0"" src=""../App_Themes/Default/Images/sort_indicator_asc.png"">"
    Public Const DOWN_ARROW_NEW_UI As String = "&nbsp;<img style=""border=""0"" src=""../App_Themes/Default/Images/sort_indicator_des.png"">"


    Public Shared Sub HighLightGridViewSortColumn(grid As GridView, sortExp As String)

        If grid.HeaderRow IsNot Nothing Then
            Dim img As New System.Web.UI.WebControls.Image()
            img.CssClass = "SORTARROW"
            If sortExp.ToUpper.EndsWith("DESC") Then
                img.ImageUrl = DOWN_ARROW_IMG_SOURCE
            Else
                img.ImageUrl = UP_ARROW_IMG_SOURCE
            End If
            Dim lnk As LinkButton
            For Each tc As TableCell In grid.HeaderRow.Cells
                If tc.HasControls Then
                    lnk = CType(tc.Controls(0), LinkButton)
                    If lnk IsNot Nothing Then
                        If sortExp.ToUpper.EndsWith("DESC") OrElse sortExp.ToUpper.EndsWith("ASC") Then
                            If sortExp.ToUpper.StartsWith(lnk.CommandArgument.ToUpper & " ") Then
                                tc.Controls.Add(img)
                            End If
                        Else
                            If sortExp.ToUpper.Trim = lnk.CommandArgument.ToUpper.Trim Then
                                tc.Controls.Add(img)
                            End If
                        End If

                    End If
                End If
            Next
        End If
    End Sub

    Public Overloads Shared Sub HighLightSortColumn(grid As DataGrid, sortExp As String, Optional ByVal newUI As Boolean = False)
        Dim column As DataGridColumn, SortExpColumn As String
        SortExpColumn = (sortExp.Replace(" DESC", "")).Replace(" ", "")

        For Each column In grid.Columns
            If Not newUI Then
                If column.HeaderText.IndexOf(UP_ARROW) >= 0 Then
                    column.HeaderText = column.HeaderText.Substring(0, column.HeaderText.Length - UP_ARROW.Length)
                ElseIf column.HeaderText.IndexOf(DOWN_ARROW) >= 0 Then
                    column.HeaderText = column.HeaderText.Substring(0, column.HeaderText.Length - DOWN_ARROW.Length)
                End If
                'If sortExp.StartsWith(column.SortExpression) AndAlso column.SortExpression.Length > 0 Then
                If (SortExpColumn.ToUpper = column.SortExpression.ToUpper) AndAlso (column.SortExpression.Length > 0) Then
                    If sortExp.EndsWith(" DESC") Then
                        column.HeaderText &= DOWN_ARROW
                    Else
                        column.HeaderText &= UP_ARROW
                    End If
                End If
            Else
                If column.HeaderText.IndexOf(UP_ARROW_NEW_UI) >= 0 Then
                    column.HeaderText = column.HeaderText.Substring(0, column.HeaderText.Length - UP_ARROW_NEW_UI.Length)
                ElseIf column.HeaderText.IndexOf(DOWN_ARROW_NEW_UI) >= 0 Then
                    column.HeaderText = column.HeaderText.Substring(0, column.HeaderText.Length - DOWN_ARROW_NEW_UI.Length)
                End If
                'If sortExp.StartsWith(column.SortExpression) AndAlso column.SortExpression.Length > 0 Then
                If (SortExpColumn.ToUpper = column.SortExpression.ToUpper) AndAlso (column.SortExpression.Length > 0) Then
                    If sortExp.EndsWith(" DESC") Then
                        column.HeaderText &= DOWN_ARROW_NEW_UI
                        column.HeaderStyle.CssClass = "columnHighlight"
                    Else
                        column.HeaderText &= UP_ARROW_NEW_UI
                        column.HeaderStyle.CssClass = "columnHighlight"
                    End If
                End If

            End If
        Next

    End Sub

    Public Overloads Shared Sub HighLightSortColumn(repeaterHeader As LinkButton, sortExpresssion As String, sortColumnName As String)
        With repeaterHeader
            .Text = TranslationBase.TranslateLabelOrMessage(.Text)
            .CommandName = SORT_COMMAND_NAME
            .CommandArgument = sortColumnName
            Dim SortExpresssionColumn As String = sortExpresssion.Replace(" DESC", "").Replace(" ", "")
            If (.CommandArgument.Length > 0) AndAlso (SortExpresssionColumn.ToUpper = .CommandArgument.ToUpper) Then
                If sortExpresssion.EndsWith(" DESC") Then
                    .Text = .Text & DOWN_ARROW_NEW_UI
                Else
                    .Text = .Text & UP_ARROW_NEW_UI
                End If
                .CssClass = "columnHighlight"
            End If
        End With
    End Sub

    Public Overloads Shared Sub HighLightSortColumn(grid As GridView, sortExp As String, Optional ByVal newUI As Boolean = False)
        Dim column As DataControlField, SortExpColumn As String
        If sortExp.EndsWith(" ASC") Then
            SortExpColumn = (sortExp.Replace(" ASC", "")).Replace(" ", "")
        ElseIf sortExp.EndsWith(" DESC") Then
            SortExpColumn = (sortExp.Replace(" DESC", "")).Replace(" ", "")
        Else
            SortExpColumn = sortExp
        End If

        For Each column In grid.Columns

            If Not newUI Then
                If column.HeaderText.IndexOf(UP_ARROW) >= 0 Then
                    column.HeaderText = column.HeaderText.Substring(0, column.HeaderText.Length - UP_ARROW.Length)
                ElseIf column.HeaderText.IndexOf(DOWN_ARROW) >= 0 Then
                    column.HeaderText = column.HeaderText.Substring(0, column.HeaderText.Length - DOWN_ARROW.Length)
                End If
                'If sortExp.StartsWith(column.SortExpression) AndAlso column.SortExpression.Length > 0 Then
                If (SortExpColumn.ToUpper = column.SortExpression.ToUpper) AndAlso (column.SortExpression.Length > 0) Then
                    If sortExp.EndsWith(" DESC") Then
                        column.HeaderText &= DOWN_ARROW
                    Else
                        column.HeaderText &= UP_ARROW
                    End If
                End If

            Else
                'Get rid of the previous arrow
                If column.HeaderText.IndexOf(UP_ARROW_NEW_UI) >= 0 Then
                    column.HeaderText = column.HeaderText.Substring(0, column.HeaderText.Length - UP_ARROW_NEW_UI.Length)
                ElseIf column.HeaderText.IndexOf(DOWN_ARROW_NEW_UI) >= 0 Then
                    column.HeaderText = column.HeaderText.Substring(0, column.HeaderText.Length - DOWN_ARROW_NEW_UI.Length)
                End If
                'add the new arrow
                If (SortExpColumn.ToUpper = column.SortExpression.ToUpper) AndAlso (column.SortExpression.Length > 0) Then
                    If sortExp.EndsWith(" DESC") Then
                        column.HeaderText &= DOWN_ARROW_NEW_UI
                        column.HeaderStyle.CssClass = "columnHighlight"
                    Else
                        column.HeaderText &= UP_ARROW_NEW_UI
                        column.HeaderStyle.CssClass = "columnHighlight"
                    End If
                End If
            End If

        Next
    End Sub


    Public Overloads Sub CreateHeaderForEmptyGrid(Grid As GridView, SortDirection As String)

        Dim dt As DataTable = New DataTable()
        Dim dv As New DataView(dt)

        dv.Table.Rows.InsertAt(dv.Table.NewRow(), 0)
        Grid.PagerSettings.Visible = True
        HighLightSortColumn(Grid, SortDirection)
        Grid.DataSource = dv
        Grid.DataBind()
        Grid.Rows(0).Visible = False

    End Sub

#End Region



End Class
