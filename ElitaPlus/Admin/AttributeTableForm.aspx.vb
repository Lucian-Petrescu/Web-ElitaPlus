Public Class AttributeTableForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents ErrorCtrl As ErrorController

#Region "Constants"
    Public Const URL As String = "AttributeTableForm.aspx"

    Private Const DEFAULT_SORT As String = "TABLE_NAME ASC"
    Private Const GRID_COL_TABLE_NAME_IDX As Integer = 0
    Private Const GRID_COL_ATTRIBUTE_COUNT_IDX As Integer = 1
    Private Const GRID_COL_VIEW_BUTTON_IDX As Integer = 2
    Private Const ADMIN As String = "Admin"
    Private Const ATTRIBUTE As String = "Attribute"
#End Region

#Region "Page State"
    Class MyState
        Public PageIndex As Integer = 0
        Public SearchDV As DataView = Nothing
        Public SortExpression As String = DEFAULT_SORT
        Public Count As Integer = 0
        Public TableName As String = String.Empty
        Public SelectedPageSize As Integer = 10
        Public SelectTableName As String = String.Empty
    End Class

    Public Sub New()
        MyBase.New(New MyState())
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
#End Region

#Region "Page Events"
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & ATTRIBUTE
        End If
    End Sub

    Private Sub AttributeTableForm_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
        Try
            Me.MenuEnabled = True
            Dim retObj As String = CType(ReturnPar, String)
            If retObj Is Nothing Then
                Me.State.SelectTableName = String.Empty
            Else
                Me.State.SearchDV = Nothing
                Me.State.SelectTableName = retObj
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(ATTRIBUTE)
            Me.UpdateBreadCrum()

            If Not Me.IsPostBack Then
                Me.trPageSize.Visible = False
                Me.SetGridItemStyleColor(DataGridTables)
                Me.TranslateGridHeader(DataGridTables)

                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
                Me.MenuEnabled = False
                Me.SortDirection = DEFAULT_SORT
            End If
            If (Me.State.SelectTableName.Trim().Length > 0) Then
                PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button-Click Events"
    Public Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Me.State.TableName = moTableNameText.Text
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            moTableNameText.Text = String.Empty
            Me.State.TableName = moTableNameText.Text
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Grid View Events"
    Public Sub DataGridTables_RowCommand(ByVal source As Object, ByVal e As GridViewCommandEventArgs) Handles DataGridTables.RowCommand
        Try
            If e.CommandName = "AttributesCMD" Then
                Me.callPage(AttributeForm.URL, e.CommandArgument)
            Else
                RefreshAttributeTable()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            DataGridTables.PageIndex = NewCurrentPageIndex(DataGridTables, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.RefreshAttributeTable()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub DataGridTables_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles DataGridTables.RowCreated
        Try
            BaseItemCreated(sender, e)
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditClaim As LinkButton

            Const SELECT_ACTION_COMMAND As String = "AttributesCMD"

            If Not dvRow Is Nothing Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    If (Not e.Row.Cells(GRID_COL_TABLE_NAME_IDX).FindControl("btnEditClaim") Is Nothing) Then
                        btnEditClaim = CType(e.Row.Cells(GRID_COL_TABLE_NAME_IDX).FindControl("btnEditClaim"), LinkButton)
                        btnEditClaim.Text = dvRow(ElitaAttribute.COL_NAME_TABLE_NAME).ToString()
                        'btnEditClaim.CommandArgument = dvRow(ElitaAttribute.COL_NAME_TABLE_NAME).ToString()
                        'btnEditClaim.CommandName = SELECT_ACTION_COMMAND
                    End If
                    e.Row.Cells(Me.GRID_COL_ATTRIBUTE_COUNT_IDX).Text = dvRow(ElitaAttribute.COL_NAME_ATTRIBUTE_COUNT).ToString()
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub DataGridTables_OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles DataGridTables.PageIndexChanging
        Try
            Me.DataGridTables.PageIndex = e.NewPageIndex
            Me.RefreshAttributeTable()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Public Sub DataGridTables_Sorting(ByVal source As Object, ByVal e As GridViewSortEventArgs) Handles DataGridTables.Sorting
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Controlling Logic"

    Public Function FindSelectedRowIndexFromString(ByVal dv As DataView, ByVal selectedGuid As String) As Integer

        Dim selectedRowIndex As Integer = NO_ITEM_SELECTED_INDEX
        If (Not (selectedGuid.Equals(Guid.Empty))) Then
            'Jump to the Right Page
            Dim i As Integer
            For i = 0 To dv.Count - 1
                If (dv(i)(SELECTED_GUID_COL).ToString()).Equals(selectedGuid) Then
                    selectedRowIndex = i
                    Return (selectedRowIndex)
                End If
            Next
        End If
        Return (selectedRowIndex)
    End Function

    Private Sub SetPageAndSelectedIndexFromString(ByVal oDataView As DataView, ByVal selectedGuid As String, ByVal oGrid As GridView, ByVal nLastSelectedPageIndex As Integer, Optional ByVal isEditing As Boolean = False)
        Dim nSelectedRow As Integer
        Dim nCurrentLastPageIndex As Integer = ((oDataView.Count - 1) \ oGrid.PageSize)

        nSelectedRow = Me.FindSelectedRowIndexFromString(oDataView, selectedGuid)
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

    Private Sub PopulateGrid()
        Try
            Me.State.PageIndex = 0
            Me.State.SearchDV = ElitaAttribute.GetTableList(Me.State.TableName, Me.State.SortExpression)

            If (Me.State.SearchDV Is Nothing) Then
                Me.State.Count = 0
            Else
                Me.State.Count = Me.State.SearchDV.Count
            End If

            Me.lblRecordCount.Text = Me.State.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            If Me.State.Count = 0 Then

                Dim dt As DataTable = New DataTable()
                dt.Columns.Add(ElitaAttribute.COL_NAME_TABLE_NAME)
                dt.Columns.Add(ElitaAttribute.COL_NAME_ATTRIBUTE_COUNT)
                Dim dv As New DataView(dt)

                dv.Table.Rows.InsertAt(dv.Table.NewRow(), 0)
                Me.State.SearchDV = dv
                RefreshAttributeTable()
                Me.DataGridTables.Rows(0).Visible = False
                Me.AddInfoMsg(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND)
            Else
                Me.SetPageAndSelectedIndexFromString(Me.State.SearchDV, Me.State.SelectTableName, Me.DataGridTables, Me.DataGridTables.PageIndex, False)

                RefreshAttributeTable()

                If (Me.State.SelectTableName.Trim().Length > 0) Then
                    Me.SetPageAndSelectedIndexFromString(Me.State.SearchDV, Me.State.SelectTableName, Me.DataGridTables, Me.DataGridTables.PageIndex, False)
                    Me.State.SelectTableName = String.Empty
                End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub RefreshAttributeTable()
        Try
            Me.trPageSize.Visible = Me.DataGridTables.Visible
            HighLightSortColumn(Me.DataGridTables, Me.State.SortExpression)
            Me.DataGridTables.DataSource = Me.State.SearchDV
            Me.DataGridTables.DataBind()
            If Not Me.DataGridTables.BottomPagerRow.Visible Then Me.DataGridTables.BottomPagerRow.Visible = True
            Me.DataGridTables.SelectedIndex = NO_ITEM_SELECTED_INDEX
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Ajax related"

    <System.Web.Services.WebMethod()>
    <Script.Services.ScriptMethod()>
    Public Shared Function PopulateTableDrop(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim dv As DataView = ElitaAttribute.GetTableList(prefixText, AttributeTableForm.DEFAULT_SORT)
        Dim resultCount As Integer
        Dim result() As String
        resultCount = Math.Min(count, dv.Count)
        ReDim result(resultCount - 1)

        Dim index As Integer
        For index = 0 To (resultCount - 1) Step 1
            result(index) = dv(index).Item("TABLE_NAME").ToString()
        Next
        Return result
    End Function
#End Region
End Class