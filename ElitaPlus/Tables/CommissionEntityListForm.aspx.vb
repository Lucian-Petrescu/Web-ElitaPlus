Partial Class CommissionEntityListForm
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
            Set(ByVal Value As Integer)
                mnPageSize = Value
            End Set
        End Property

        Public Property PageSort() As String
            Get
                Return msPageSort
            End Get
            Set(ByVal Value As String)
                msPageSort = Value
            End Set
        End Property

        Public Property SearchDataView() As CommissionEntity.CommissionEntitySearchDV
            Get
                Return searchDV
            End Get
            Set(ByVal Value As CommissionEntity.CommissionEntitySearchDV)
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
    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As CommissionEntityForm.ReturnType = CType(ReturnPar, CommissionEntityForm.ReturnType)

            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        Me.State.CommissionEntityId = retObj.EditingBo.Id
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
#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.UpdateBreadCrum()
        Me.MasterPage.MessageController.Clear()

        Try

            If Not Me.IsPostBack Then
                Me.SortDirection = CommissionEntity.CommissionEntitySearchDV.COL_COMMISSION_ENTITY_NAME
                Me.SetDefaultButton(Me.SearchPhoneTextbox, btnSearch)
                Me.SetDefaultButton(Me.SearchDescriptionTextBox, btnSearch)
                If Me.IsReturningFromChild = True Then
                    GetSession()
                End If

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
            End If

            If Me.IsReturningFromChild = True Then
                Me.IsReturningFromChild = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub


#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        Dim CompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = CommissionEntity.getList(Me.SearchDescriptionTextBox.Text, Me.SearchPhoneTextbox.Text, CompanyGroupId)
        End If

        If (ElitaPlusPrincipal.Current.IsInRole(Codes.USER_ROLE__IHQ_SUPPORT)) Then
            Me.State.searchDV.RowFilter = ""
        Else
            Me.State.searchDV.RowFilter = "display_id='" & YES & "'"
        End If

        Me.State.searchDV.Sort = Me.SortDirection
        Me.Grid.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.CommissionEntityId, Me.Grid, Me.State.PageIndex)
        Me.SortAndBindGrid()

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
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        If Not dvRow Is Nothing And Not Me.State.bnoRow Then
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Row.Cells(Me.GRID_COL_PHONE_IDX).Text = dvRow(CommissionEntity.CommissionEntitySearchDV.COL_COMMISSION_ENTITY_PHONE).ToString
                e.Row.Cells(Me.GRID_COL_COMMISSION_ENTITY_NAME_IDX).Text = dvRow(CommissionEntity.CommissionEntitySearchDV.COL_COMMISSION_ENTITY_NAME).ToString
                e.Row.Cells(Me.GRID_COL_COMMISSION_ENTITY_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(CommissionEntity.CommissionEntitySearchDV.COL_COMMISSION_ENTITY_ID), Byte()))
            End If
        End If
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                Me.State.CommissionEntityId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_COMMISSION_ENTITY_IDX).Text)
                SetSession()
                Me.callPage(CommissionEntityForm.URL, Me.State.CommissionEntityId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
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

            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.CommissionEntityId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()
        Me.State.DescriptionMask = SearchDescriptionTextBox.Text
        Me.State.PhoneMask = SearchPhoneTextbox.Text
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.CommissionEntityId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        SetSession()
        Me.callPage(CommissionEntityForm.URL)
    End Sub
    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
    End Sub

    Private Sub ClearSearchCriteria()

        Try
            SearchDescriptionTextBox.Text = String.Empty
            SearchPhoneTextbox.Text = String.Empty

            'Update Page State
            With Me.State
                .DescriptionMask = SearchDescriptionTextBox.Text
                .PhoneMask = SearchPhoneTextbox.Text
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
#End Region

#Region "State-Management"

    Private Sub SetSession()
        With Me.State
            .PhoneMask = Me.SearchPhoneTextbox.Text
            .DescriptionMask = Me.SearchDescriptionTextBox.Text
            .PageIndex = Grid.PageIndex
            .PageSize = Grid.PageSize
            .PageSort = Me.SortDirection
            .SearchDataView = Me.State.searchDV
        End With
    End Sub

    Private Sub GetSession()
        'Dim oDataView As DataView
        With Me.State
            Me.SearchPhoneTextbox.Text = .PhoneMask
            Me.SearchDescriptionTextBox.Text = .DescriptionMask
            Me.Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
            'oDataView.Sort = .PageSort
        End With
    End Sub
#End Region

    Private Sub UpdateBreadCrum()
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("COMMISSION_ENTITY")
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COMMISSION_ENTITY")
        Me.MasterPage.MessageController.Clear()
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") & ElitaBase.Sperator & Me.MasterPage.PageTab
    End Sub

End Class
