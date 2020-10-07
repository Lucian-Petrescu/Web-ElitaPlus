Partial Class CommissionEntityListForm
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
    Public Const GRID_COL_PHONE_IDX As Integer = 2
    Public Const GRID_COL_COMMISSION_ENTITY_NAME_IDX As Integer = 1
    Public Const GRID_COL_COMMISSION_ENTITY_IDX As Integer = 3

    Public Const GRID_TOTAL_COLUMNS As Integer = 4
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const COMMISSIONENTITYLISTFORM As String = "CommissionEntityListForm.aspx"
    Public Const YES As String = "Y"

#End Region


#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As CommissionEntity
        Public PageIndex As Integer = 0
        Public DescriptionMask As String
        Public PhoneMask As String
        Public CommissionEntityId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Private mnPageIndex As Integer = 0
        Private msPageSort As String
        Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As CommissionEntity.CommissionEntitySearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        'Public SortExpression As String = CommissionEntity.CommissionEntitySearchDV.COL_COMMISSION_ENTITY_NAME
        Public HasDataChanged As Boolean
        Public bnoRow As Boolean = False


        Public Property PageSize() As Integer
            Get
                Return mnPageSize
            End Get
            Set(Value As Integer)
                mnPageSize = Value
            End Set
        End Property

        Public Property PageSort() As String
            Get
                Return msPageSort
            End Get
            Set(Value As String)
                msPageSort = Value
            End Set
        End Property

        Public Property SearchDataView() As CommissionEntity.CommissionEntitySearchDV
            Get
                Return searchDV
            End Get
            Set(Value As CommissionEntity.CommissionEntitySearchDV)
                searchDV = Value
            End Set
        End Property

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property


#Region "Page Return"
    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As CommissionEntityForm.ReturnType = CType(ReturnPar, CommissionEntityForm.ReturnType)

            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        State.CommissionEntityId = retObj.EditingBo.Id
                        State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region
#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        UpdateBreadCrum()
        MasterPage.MessageController.Clear()

        Try

            If Not IsPostBack Then
                SortDirection = CommissionEntity.CommissionEntitySearchDV.COL_COMMISSION_ENTITY_NAME
                SetDefaultButton(SearchPhoneTextbox, btnSearch)
                SetDefaultButton(SearchDescriptionTextBox, btnSearch)
                If IsReturningFromChild = True Then
                    GetSession()
                End If

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If
                SetGridItemStyleColor(Grid)
            End If

            If IsReturningFromChild = True Then
                IsReturningFromChild = False
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)

    End Sub


#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        Dim CompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = CommissionEntity.getList(SearchDescriptionTextBox.Text, SearchPhoneTextbox.Text, CompanyGroupId)
        End If

        If (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT)) Then
            State.searchDV.RowFilter = ""
        Else
            State.searchDV.RowFilter = "display_id='" & YES & "'"
        End If

        State.searchDV.Sort = SortDirection
        Grid.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.CommissionEntityId, Grid, State.PageIndex)
        SortAndBindGrid()

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

        If State.searchDV.Count > 0 Then

            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
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
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        If dvRow IsNot Nothing And Not State.bnoRow Then
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Row.Cells(GRID_COL_PHONE_IDX).Text = dvRow(CommissionEntity.CommissionEntitySearchDV.COL_COMMISSION_ENTITY_PHONE).ToString
                e.Row.Cells(GRID_COL_COMMISSION_ENTITY_NAME_IDX).Text = dvRow(CommissionEntity.CommissionEntitySearchDV.COL_COMMISSION_ENTITY_NAME).ToString
                e.Row.Cells(GRID_COL_COMMISSION_ENTITY_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(CommissionEntity.CommissionEntitySearchDV.COL_COMMISSION_ENTITY_ID), Byte()))
            End If
        End If
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                State.CommissionEntityId = New Guid(Grid.Rows(index).Cells(GRID_COL_COMMISSION_ENTITY_IDX).Text)
                SetSession()
                callPage(CommissionEntityForm.URL, State.CommissionEntityId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
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

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            State.CommissionEntityId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()
        State.DescriptionMask = SearchDescriptionTextBox.Text
        State.PhoneMask = SearchPhoneTextbox.Text
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.CommissionEntityId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        SetSession()
        callPage(CommissionEntityForm.URL)
    End Sub
    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
    End Sub

    Private Sub ClearSearchCriteria()

        Try
            SearchDescriptionTextBox.Text = String.Empty
            SearchPhoneTextbox.Text = String.Empty

            'Update Page State
            With State
                .DescriptionMask = SearchDescriptionTextBox.Text
                .PhoneMask = SearchPhoneTextbox.Text
            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
#End Region

#Region "State-Management"

    Private Sub SetSession()
        With State
            .PhoneMask = SearchPhoneTextbox.Text
            .DescriptionMask = SearchDescriptionTextBox.Text
            .PageIndex = Grid.PageIndex
            .PageSize = Grid.PageSize
            .PageSort = SortDirection
            .SearchDataView = State.searchDV
        End With
    End Sub

    Private Sub GetSession()
        'Dim oDataView As DataView
        With State
            SearchPhoneTextbox.Text = .PhoneMask
            SearchDescriptionTextBox.Text = .DescriptionMask
            Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
            'oDataView.Sort = .PageSort
        End With
    End Sub
#End Region

    Private Sub UpdateBreadCrum()
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("COMMISSION_ENTITY")
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COMMISSION_ENTITY")
        MasterPage.MessageController.Clear()
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") & ElitaBase.Sperator & MasterPage.PageTab
    End Sub

End Class
