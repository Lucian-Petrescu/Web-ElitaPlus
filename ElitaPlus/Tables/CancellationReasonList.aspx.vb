Partial Class CancellationReasonListForm
    Inherits ElitaPlusSearchPage

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
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_COMPANY_CODE_IDX As Integer = 2
    Public Const GRID_COL_DESCRIPTION_IDX As Integer = 1
    Public Const GRID_COL_CODE_IDX As Integer = 0
    Public Const GRID_COL_CANCELLATIONREASON_IDX As Integer = 3

    Public Const GRID_TOTAL_COLUMNS As Integer = 4

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public DescriptionMask As String
        Public CodeMask As String
        Public CompanyId As Guid
        Public CancellationReasonId As Guid = Guid.Empty
        Public IsGridVisible As Boolean = False
        Public searchBtnClicked As Boolean = False
        Public searchDV As CancellationReason.CancellationReasonSearchDV = Nothing
        Public SortExpression As String = CancellationReason.CancellationReasonSearchDV.COL_CODE
        Public bnoRow As Boolean = False
        'Public SortExpression As String = CancellationReason.CancellationReasonSearchDV.COL_DESCRIPTION

        ' these variables are used to store the sorting columns information.
        Public SortColumns(GRID_TOTAL_COLUMNS - 1) As String
        Public IsSortDesc(GRID_TOTAL_COLUMNS - 1) As Boolean
        Sub New()
            SortColumns(GRID_COL_DESCRIPTION_IDX) = CancellationReason.CancellationReasonSearchDV.COL_DESCRIPTION
            SortColumns(GRID_COL_CODE_IDX) = CancellationReason.CancellationReasonSearchDV.COL_CODE

            IsSortDesc(GRID_COL_DESCRIPTION_IDX) = False
            IsSortDesc(GRID_COL_CODE_IDX) = False
        End Sub

        ' this will be called before the populate list to get the correct sort order
        Public ReadOnly Property CurrentSortExpresion1() As String
            Get
                Dim s As String
                Dim i As Integer
                Dim sortExp As String = ""
                For i = 0 To Me.SortColumns.Length - 1
                    If Not Me.SortColumns(i) Is Nothing Then
                        sortExp &= Me.SortColumns(i)
                        If Me.IsSortDesc(i) Then sortExp &= " DESC"
                        sortExp &= ","
                    End If
                Next
                Return sortExp.Substring(0, sortExp.Length - 1) 'to remove the last comma
            End Get
        End Property

        Public Sub ToggleSort1(ByVal gridColIndex As Integer)
            IsSortDesc(gridColIndex) = Not IsSortDesc(gridColIndex)
        End Sub

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            If Me.State.searchDV Is Nothing Then
                Me.State.IsGridVisible = False
            Else
                Me.State.IsGridVisible = True
            End If
            Dim retObj As CancellationReasonForm.ReturnType = CType(ReturnPar, CancellationReasonForm.ReturnType)
            If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                Me.State.searchDV = Nothing
            End If
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.CancellationReasonId = retObj.EditingBo.Id
                        End If
                        'Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
            Grid.PageIndex = Me.State.PageIndex
            cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
            Grid.PageSize = Me.State.PageSize
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.MasterPage.MessageController.Clear_Hide()
        Me.SetStateProperties()
        Try
            If Not Me.IsPostBack Then
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()

                Me.SortDirection = CancellationReason.CancellationReasonSearchDV.COL_DESCRIPTION
                Me.SetDefaultButton(Me.SearchCodeTextBox, Me.moBtnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.SetGridItemStyleColor(Grid)
                Me.TranslateGridHeader(Me.Grid)
                Me.TranslateGridControls(Me.Grid)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.PageSize = Grid.PageSize) Then
                        Grid.PageSize = Me.State.PageSize
                    End If
                    Me.PopulateGrid()
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    'Public Sub PopulateGrid()
    '    'Dim dv As CancellationReason.CancellationReasonSearchDV = CancellationReason.getList(Me.State.DescriptionMask, Me.State.CodeMask, Me.State.CompanyId)

    '    If (Me.State.searchDV Is Nothing) Then
    '        Me.State.searchDV = CancellationReason.getList(Me.State.DescriptionMask, Me.State.CodeMask)
    '        'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
    '        'If Me.State.searchBtnClicked Then
    '        '    'Me.ValidSearchResultCount(Me.State.searchDV.Count, True) 
    '        'End If
    '    End If

    '    'Me.State.searchDV.Sort = Me.Grid.DataMember()
    '    Me.State.searchDV.Sort = Me.SortDirection
    '    Me.Grid.AutoGenerateColumns = False
    '    'Me.Grid.Columns(Me.GRID_COL_DESCRIPTION_IDX).SortExpression = CancellationReason.CancellationReasonSearchDV.COL_DESCRIPTION
    '    'Me.Grid.Columns(Me.GRID_COL_CODE_IDX).SortExpression = CancellationReason.CancellationReasonSearchDV.COL_CODE

    '    SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.CancellationReasonId, Me.Grid, Me.State.PageIndex)
    '    SortAndBindGrid()

    'End Sub

    Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
        Dim oDataView As DataView

        Try
            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = CancellationReason.getList(Me.State.DescriptionMask, Me.State.CodeMask)

            End If

            If (Me.State.searchDV.Count = 0) Then

                Me.State.bnoRow = True
                CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
            Else
                Me.State.bnoRow = False
                Me.Grid.Enabled = True
            End If

            Me.State.searchDV.Sort = Me.State.SortExpression
            grid.AutoGenerateColumns = False

            Grid.Columns(Me.GRID_COL_CODE_IDX).SortExpression = CancellationReason.CancellationReasonSearchDV.COL_CODE
            Grid.Columns(Me.GRID_COL_DESCRIPTION_IDX).SortExpression = CancellationReason.CancellationReasonSearchDV.COL_DESCRIPTION
            Grid.Columns(Me.GRID_COL_COMPANY_CODE_IDX).SortExpression = CancellationReason.CancellationReasonSearchDV.COL_COMPANY

            HighLightSortColumn(grid, Me.State.SortExpression)
            ' BasePopulateGrid(Grid, Me.State.searchDV, Me.State.moProductCodeId, oAction)

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.CancellationReasonId, Me.Grid, Me.State.PageIndex)

            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, trPageSize, grid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SortAndBindGrid()
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
            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

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
    'The Binding Logic is here
    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditItem As LinkButton
        If Not dvRow Is Nothing And Not Me.State.bnoRow Then
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                btnEditItem = CType(e.Row.Cells(Me.GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                btnEditItem.Text = dvRow(CancellationReason.CancellationReasonSearchDV.COL_CODE).ToString
                e.Row.Cells(Me.GRID_COL_COMPANY_CODE_IDX).Text = dvRow(CancellationReason.CancellationReasonSearchDV.COL_COMPANY).ToString
                ' e.Row.Cells(Me.GRID_COL_CODE_IDX).Text = dvRow(CancellationReason.CancellationReasonSearchDV.COL_CODE).ToString
                e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).Text = dvRow(CancellationReason.CancellationReasonSearchDV.COL_DESCRIPTION).ToString
                e.Row.Cells(Me.GRID_COL_CANCELLATIONREASON_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(CancellationReason.CancellationReasonSearchDV.COL_CANCELLATIONREASON_ID), Byte()))
            End If
        End If
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

            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                Me.State.CancellationReasonId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_CANCELLATIONREASON_IDX).Text)
                Me.callPage(CancellationReasonForm.URL, Me.State.CancellationReasonId)
            ElseIf e.CommandName = "Sort" Then
                Grid.DataMember = e.CommandArgument.ToString
                Me.PopulateGrid()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.CancellationReasonId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.PageSize = Grid.PageSize
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Buttons Clicks "
    Private Sub SetStateProperties()

        Me.State.DescriptionMask = SearchDescriptionTextBox.Text
        Me.State.CodeMask = SearchCodeTextBox.Text
        Me.State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
        Try
            If Not State.IsGridVisible Then
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                End If
                Me.State.IsGridVisible = True
            End If
            Me.State.PageIndex = 0
            Me.State.CancellationReasonId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.searchBtnClicked = True
            Me.PopulateGrid()
            Me.State.searchBtnClicked = False
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            Me.callPage(CancellationReasonForm.URL)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnClear.Click
        Try
            ClearSearchCriteria()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("CancellationReason_List")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CancellationReason_List")
            End If
        End If
    End Sub
    Private Sub ClearSearchCriteria()

        Try
            SearchCodeTextBox.Text = String.Empty
            SearchCodeTextBox.Text = String.Empty

            'Update Page State
            With Me.State
                .DescriptionMask = SearchDescriptionTextBox.Text
                .CodeMask = SearchCodeTextBox.Text
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Error Handling"


#End Region
End Class
