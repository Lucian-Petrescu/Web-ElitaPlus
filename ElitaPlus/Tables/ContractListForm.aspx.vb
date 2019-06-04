Partial Class ContractListForm
    Inherits ElitaPlusSearchPage



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"

    Public Const GRID_COL_COMPANY_CODE_IDX As Integer = 1
    Public Const GRID_COL_DEALER_CODE_IDX As Integer = 0
    Public Const GRID_COL_DEALER_NAME_IDX As Integer = 2
    Public Const GRID_COL_EFFECTIVE_IDX As Integer = 3
    Public Const GRID_COL_EXPIRATION_IDX As Integer = 4
    Public Const GRID_COL_CONTRACT_ID_IDX As Integer = 5

    Public Const GRID_TOTAL_COLUMNS As Integer = 6

    Private Const LABEL_DEALER As String = "DEALER"

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SelectedContractId As Guid = Guid.Empty
        Public SelectedDealerId As Guid = Guid.Empty
        Public IsGridVisible As Boolean = False
        Public searchDV As Contract.ContractSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public SortExpression As String = Contract.ContractSearchDV.COL_DEALER_NAME
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

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            If Me.State.searchDV Is Nothing Then
                Me.State.IsGridVisible = False
            Else
                Me.State.IsGridVisible = True
            End If
            Dim retObj As ContractForm.ReturnType = CType(ReturnPar, ContractForm.ReturnType)
            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.SelectedContractId = retObj.EditingBo.Id
                        End If
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

                TranslateGridHeader(Me.Grid)

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                PopulateDealerDropDown()
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.selectedPageSize = Grid.PageSize) Then
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Contract")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Contract")
        End If
    End Sub

    Sub PopulateDealerDropDown()
        moDealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
        moDealerMultipleDrop.NothingSelected = True

        moDealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
        moDealerMultipleDrop.AutoPostBackDD = False
    End Sub

    Public Sub PopulateGrid()
        'Me.State.SelectedDealerId = Me.GetSelectedItem(Me.cboDealer)
        Me.State.SelectedDealerId = moDealerMultipleDrop.SelectedGuid
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = Contract.getList(Me.State.SelectedDealerId)
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'If (Not (Me.State.HasDataChanged)) Then
            '    'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            'End If
        End If
        Me.State.searchDV.Sort = Me.State.SortExpression
        Me.Grid.AutoGenerateColumns = False
        Me.Grid.Columns(Me.GRID_COL_DEALER_CODE_IDX).SortExpression = Contract.ContractSearchDV.COL_DEALER_CODE
        Me.Grid.Columns(Me.GRID_COL_DEALER_NAME_IDX).SortExpression = Contract.ContractSearchDV.COL_DEALER_NAME
        Me.Grid.Columns(Me.GRID_COL_COMPANY_CODE_IDX).SortExpression = Contract.ContractSearchDV.COL_COMPANY_CODE
        Me.Grid.Columns(Me.GRID_COL_EFFECTIVE_IDX).SortExpression = Contract.ContractSearchDV.COL_EFFECTIVE
        Me.Grid.Columns(Me.GRID_COL_EXPIRATION_IDX).SortExpression = Contract.ContractSearchDV.COL_EXPIRATION

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.SelectedContractId, Me.Grid, Me.State.PageIndex)

        Me.SortAndBindGrid()

    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.PageIndex
        Me.Grid.DataSource = Me.State.searchDV
        HighLightSortColumn(Grid, Me.State.SortExpression)
        Me.Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        Session("recCount") = Me.State.searchDV.Count

        If Me.State.searchDV.Count > 0 Then
            Me.State.bnoRow = False
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            Me.State.bnoRow = True
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub


    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(ByVal dv As Contract.ContractSearchDV) As Integer
        Try
            If Me.State.SelectedContractId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(Contract.ContractSearchDV.COL_CONTRACT_ID), Byte())).Equals(Me.State.SelectedContractId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        Catch ex As Exception
            Return -1
        End Try
    End Function


#End Region


#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditItem As LinkButton
        Try
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    btnEditItem = CType(e.Row.Cells(Me.GRID_COL_DEALER_CODE_IDX).FindControl("SelectAction"), LinkButton)
                    btnEditItem.Text = dvRow(Contract.ContractSearchDV.COL_DEALER_CODE).ToString
                    e.Row.Cells(Me.GRID_COL_COMPANY_CODE_IDX).Text = dvRow(Contract.ContractSearchDV.COL_COMPANY_CODE).ToString
                    e.Row.Cells(Me.GRID_COL_DEALER_NAME_IDX).Text = dvRow(Contract.ContractSearchDV.COL_DEALER_NAME).ToString
                    e.Row.Cells(Me.GRID_COL_EFFECTIVE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(Contract.ContractSearchDV.COL_EFFECTIVE), Date))
                    e.Row.Cells(Me.GRID_COL_EXPIRATION_IDX).Text = Me.GetDateFormattedString(CType(dvRow(Contract.ContractSearchDV.COL_EXPIRATION), Date))
                    e.Row.Cells(Me.GRID_COL_CONTRACT_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Contract.ContractSearchDV.COL_CONTRACT_ID), Byte()))
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
                Me.State.SelectedContractId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_CONTRACT_ID_IDX).Text)
                Me.callPage(ContractForm.URL, Me.State.SelectedContractId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
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

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.SelectedContractId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.SelectedContractId = Guid.Empty
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
        Try
            Me.callPage(ContractForm.URL)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ' Me.cboDealer.SelectedIndex = 0
            moDealerMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Error Handling"


#End Region







End Class
