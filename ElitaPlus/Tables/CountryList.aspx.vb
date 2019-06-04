Option Strict On
Option Explicit On

Partial Class CountryList
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
    Public Const GRID_COL_COUNTRY_CODE_IDX As Integer = 0
    Public Const GRID_COL_DESCRIPTION_IDX As Integer = 1
    Public Const GRID_COL_EDIT_IDX As Integer = 2

    Public Const GRID_TOTAL_COLUMNS As Integer = 3
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const COUNTRYLIST As String = "CountryList.aspx"
    Private Const LABEL_SELECT_COUNTRYCODE As String = "COUNTRY"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public PageIndex As Integer = 0
        Public DescriptionMask As String
        Public CodeMask As String
        Public CountryId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Public searchDV As Country.CountrySearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public SortExpression As String = Country.CountrySearchDV.COL_DESCRIPTION
        Public HasDataChanged As Boolean
        Public bnoRow As Boolean = False

        Sub New()

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

#End Region

#Region "Properties"

#End Region

#Region "Page_Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.MasterPage.MessageController.Clear_Hide()

        Try
            If Not Me.IsPostBack Then
                ' Set Master Page Header
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                ' Update Bread Crum
                UpdateBreadCrum()

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.SortDirection = Me.State.SortExpression

                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.selectedPageSize = Grid.PageSize) Then
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                    ' Populate Grid
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As CountryForm.ReturnType = CType(ReturnPar, CountryForm.ReturnType)
            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        Me.State.CountryId = retObj.EditingBo.Id
                        Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = Country.getList(TextboxDescription.Text, TextboxCode.Text)
        End If
        If Not (Me.State.searchDV Is Nothing) Then
            Me.State.searchDV.Sort = Me.SortDirection
            Me.Grid.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.CountryId, Me.Grid, Me.State.PageIndex)
            Me.SortAndBindGrid()
        End If
    End Sub

    Private Sub SortAndBindGrid()
        If (Me.State.searchDV.Count = 0) Then
            Me.State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
        Else
            Me.State.bnoRow = False
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
        End If

        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

        If Me.State.searchDV.Count > 0 Then
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("COUNTRY")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COUNTRY")
        End If
    End Sub

#End Region

#Region "GridView Related"

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    btnEditItem = CType(e.Row.Cells(Me.GRID_COL_COUNTRY_CODE_IDX).FindControl("SelectAction"), LinkButton)
                    btnEditItem.Text = dvRow(Country.CountrySearchDV.COL_CODE).ToString
                    e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).Text = dvRow(Country.CountrySearchDV.COL_DESCRIPTION).ToString
                    e.Row.Cells(Me.GRID_COL_EDIT_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Country.CountrySearchDV.COL_COUNTRY_ID), Byte()))
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                Me.State.CountryId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_EDIT_IDX).Text)
                Me.callPage(CountryForm.URL, Me.State.CountryId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = Grid.PageSize
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.CountryId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Button Clicks"

    Private Sub moBtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.CountryId = Guid.Empty

            If Not State.IsGridVisible Then
                cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                    Grid.PageSize = State.selectedPageSize
                End If
                Me.State.IsGridVisible = True
            End If

            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        Me.callPage(CountryForm.URL)
    End Sub

    Private Sub moBtnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnClearSearch.Click
        ClearSearchCriteria()
    End Sub

    Private Sub ClearSearchCriteria()
        Try
            TextboxDescription.Text = String.Empty
            TextboxCode.Text = String.Empty
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

End Class
