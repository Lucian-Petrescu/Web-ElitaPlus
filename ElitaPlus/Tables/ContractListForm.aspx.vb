Partial Class ContractListForm
    Inherits ElitaPlusSearchPage



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            If State.searchDV Is Nothing Then
                State.IsGridVisible = False
            Else
                State.IsGridVisible = True
            End If
            Dim retObj As ContractForm.ReturnType = CType(ReturnPar, ContractForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.SelectedContractId = retObj.EditingBo.Id
                        End If
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

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        MasterPage.MessageController.Clear_Hide()
        Try
            If Not IsPostBack Then
                ' Set Master Page Header
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                ' Update Bread Crum
                UpdateBreadCrum()

                TranslateGridHeader(Grid)

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                PopulateDealerDropDown()
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.selectedPageSize = Grid.PageSize) Then
                        Grid.PageSize = State.selectedPageSize
                    End If
                    cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                    PopulateGrid()
                End If
                SetGridItemStyleColor(Grid)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"
    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Contract")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Contract")
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
        State.SelectedDealerId = moDealerMultipleDrop.SelectedGuid
        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = Contract.getList(State.SelectedDealerId)
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'If (Not (Me.State.HasDataChanged)) Then
            '    'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            'End If
        End If
        State.searchDV.Sort = State.SortExpression
        Grid.AutoGenerateColumns = False
        Grid.Columns(GRID_COL_DEALER_CODE_IDX).SortExpression = Contract.ContractSearchDV.COL_DEALER_CODE
        Grid.Columns(GRID_COL_DEALER_NAME_IDX).SortExpression = Contract.ContractSearchDV.COL_DEALER_NAME
        Grid.Columns(GRID_COL_COMPANY_CODE_IDX).SortExpression = Contract.ContractSearchDV.COL_COMPANY_CODE
        Grid.Columns(GRID_COL_EFFECTIVE_IDX).SortExpression = Contract.ContractSearchDV.COL_EFFECTIVE
        Grid.Columns(GRID_COL_EXPIRATION_IDX).SortExpression = Contract.ContractSearchDV.COL_EXPIRATION

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.SelectedContractId, Grid, State.PageIndex)

        SortAndBindGrid()

    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = Grid.PageIndex
        Grid.DataSource = State.searchDV
        HighLightSortColumn(Grid, State.SortExpression)
        Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        Session("recCount") = State.searchDV.Count

        If State.searchDV.Count > 0 Then
            State.bnoRow = False
            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            State.bnoRow = True
            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub


    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(dv As Contract.ContractSearchDV) As Integer
        Try
            If State.SelectedContractId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(Contract.ContractSearchDV.COL_CONTRACT_ID), Byte())).Equals(State.SelectedContractId) Then
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
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditItem As LinkButton
        Try
            If dvRow IsNot Nothing And Not State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    btnEditItem = CType(e.Row.Cells(GRID_COL_DEALER_CODE_IDX).FindControl("SelectAction"), LinkButton)
                    btnEditItem.Text = dvRow(Contract.ContractSearchDV.COL_DEALER_CODE).ToString
                    e.Row.Cells(GRID_COL_COMPANY_CODE_IDX).Text = dvRow(Contract.ContractSearchDV.COL_COMPANY_CODE).ToString
                    e.Row.Cells(GRID_COL_DEALER_NAME_IDX).Text = dvRow(Contract.ContractSearchDV.COL_DEALER_NAME).ToString
                    e.Row.Cells(GRID_COL_EFFECTIVE_IDX).Text = GetDateFormattedString(CType(dvRow(Contract.ContractSearchDV.COL_EFFECTIVE), Date))
                    e.Row.Cells(GRID_COL_EXPIRATION_IDX).Text = GetDateFormattedString(CType(dvRow(Contract.ContractSearchDV.COL_EXPIRATION), Date))
                    e.Row.Cells(GRID_COL_CONTRACT_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Contract.ContractSearchDV.COL_CONTRACT_ID), Byte()))
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                State.SelectedContractId = New Guid(Grid.Rows(index).Cells(GRID_COL_CONTRACT_ID_IDX).Text)
                callPage(ContractForm.URL, State.SelectedContractId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = Grid.PageSize
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            State.SelectedContractId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.SelectedContractId = Guid.Empty
            If Not State.IsGridVisible Then
                cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                    Grid.PageSize = State.selectedPageSize
                End If
                State.IsGridVisible = True
            End If
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            callPage(ContractForm.URL)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ' Me.cboDealer.SelectedIndex = 0
            moDealerMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Error Handling"


#End Region







End Class
