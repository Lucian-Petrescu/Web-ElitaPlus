Partial Class CancellationReasonListForm
    Inherits ElitaPlusSearchPage

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
                For i = 0 To SortColumns.Length - 1
                    If SortColumns(i) IsNot Nothing Then
                        sortExp &= SortColumns(i)
                        If IsSortDesc(i) Then sortExp &= " DESC"
                        sortExp &= ","
                    End If
                Next
                Return sortExp.Substring(0, sortExp.Length - 1) 'to remove the last comma
            End Get
        End Property

        Public Sub ToggleSort1(gridColIndex As Integer)
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

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            If State.searchDV Is Nothing Then
                State.IsGridVisible = False
            Else
                State.IsGridVisible = True
            End If
            Dim retObj As CancellationReasonForm.ReturnType = CType(ReturnPar, CancellationReasonForm.ReturnType)
            If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                State.searchDV = Nothing
            End If
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.CancellationReasonId = retObj.EditingBo.Id
                        End If
                        'Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
            Grid.PageIndex = State.PageIndex
            cboPageSize.SelectedValue = CType(State.PageSize, String)
            Grid.PageSize = State.PageSize
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        MasterPage.MessageController.Clear_Hide()
        SetStateProperties()
        Try
            If Not IsPostBack Then
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()

                SortDirection = CancellationReason.CancellationReasonSearchDV.COL_DESCRIPTION
                SetDefaultButton(SearchCodeTextBox, moBtnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                SetGridItemStyleColor(Grid)
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                If State.IsGridVisible Then
                    If Not (State.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) OrElse Not (State.PageSize = Grid.PageSize) Then
                        Grid.PageSize = State.PageSize
                    End If
                    PopulateGrid()
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
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
            If (State.searchDV Is Nothing) Then
                State.searchDV = CancellationReason.getList(State.DescriptionMask, State.CodeMask)

            End If

            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(Grid, SortDirection)
            Else
                State.bnoRow = False
                Grid.Enabled = True
            End If

            State.searchDV.Sort = State.SortExpression
            grid.AutoGenerateColumns = False

            Grid.Columns(GRID_COL_CODE_IDX).SortExpression = CancellationReason.CancellationReasonSearchDV.COL_CODE
            Grid.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = CancellationReason.CancellationReasonSearchDV.COL_DESCRIPTION
            Grid.Columns(GRID_COL_COMPANY_CODE_IDX).SortExpression = CancellationReason.CancellationReasonSearchDV.COL_COMPANY

            HighLightSortColumn(grid, State.SortExpression)
            ' BasePopulateGrid(Grid, Me.State.searchDV, Me.State.moProductCodeId, oAction)

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.CancellationReasonId, Grid, State.PageIndex)

            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, trPageSize, grid.Visible)

            Session("recCount") = State.searchDV.Count

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SortAndBindGrid()
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
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

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
    'The Binding Logic is here
    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditItem As LinkButton
        If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                btnEditItem = CType(e.Row.Cells(GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                btnEditItem.Text = dvRow(CancellationReason.CancellationReasonSearchDV.COL_CODE).ToString
                e.Row.Cells(GRID_COL_COMPANY_CODE_IDX).Text = dvRow(CancellationReason.CancellationReasonSearchDV.COL_COMPANY).ToString
                ' e.Row.Cells(Me.GRID_COL_CODE_IDX).Text = dvRow(CancellationReason.CancellationReasonSearchDV.COL_CODE).ToString
                e.Row.Cells(GRID_COL_DESCRIPTION_IDX).Text = dvRow(CancellationReason.CancellationReasonSearchDV.COL_DESCRIPTION).ToString
                e.Row.Cells(GRID_COL_CANCELLATIONREASON_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(CancellationReason.CancellationReasonSearchDV.COL_CANCELLATIONREASON_ID), Byte()))
            End If
        End If
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

            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                State.CancellationReasonId = New Guid(Grid.Rows(index).Cells(GRID_COL_CANCELLATIONREASON_IDX).Text)
                callPage(CancellationReasonForm.URL, State.CancellationReasonId)
            ElseIf e.CommandName = "Sort" Then
                Grid.DataMember = e.CommandArgument.ToString
                PopulateGrid()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            State.CancellationReasonId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.PageSize = Grid.PageSize
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Buttons Clicks "
    Private Sub SetStateProperties()

        State.DescriptionMask = SearchDescriptionTextBox.Text
        State.CodeMask = SearchCodeTextBox.Text
        State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles moBtnSearch.Click
        Try
            If Not State.IsGridVisible Then
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                End If
                State.IsGridVisible = True
            End If
            State.PageIndex = 0
            State.CancellationReasonId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.searchBtnClicked = True
            PopulateGrid()
            State.searchBtnClicked = False
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            callPage(CancellationReasonForm.URL)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click1(sender As Object, e As System.EventArgs) Handles moBtnClear.Click
        Try
            ClearSearchCriteria()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("CancellationReason_List")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CancellationReason_List")
            End If
        End If
    End Sub
    Private Sub ClearSearchCriteria()

        Try
            SearchCodeTextBox.Text = String.Empty
            SearchCodeTextBox.Text = String.Empty

            'Update Page State
            With State
                .DescriptionMask = SearchDescriptionTextBox.Text
                .CodeMask = SearchCodeTextBox.Text
            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Error Handling"


#End Region
End Class
