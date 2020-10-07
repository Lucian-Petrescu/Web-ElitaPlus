Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports System.Web.Script.Services
Imports Assurant.ElitaPlus.Security
Imports System.Web.Script.Serialization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Partial Class AccountingEventListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents ErrorCtrl As ErrorController


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
    Public Const GRID_COL_ACCT_EVENT_TYPE_IDX As Integer = 1
    Public Const GRID_COL_ACCT_EVENT_IDX As Integer = 2

    Public Const GRID_TOTAL_COLUMNS As Integer = 3
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const URL As String = "AccountingEventListForm.aspx"
#End Region

#Region "PROPERTIES"

    Private WriteOnly Property EnableControls() As Boolean
        Set(Value As Boolean)
            ControlMgr.SetEnableControl(Me, btnSearch, Value)
            ControlMgr.SetEnableControl(Me, btnClearSearch, Value)
            ControlMgr.SetEnableControl(Me, btnAdd_WRITE, Value)
        End Set
    End Property

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As AcctEvent
        Public PageIndex As Integer = 0
        Public AccountingEventId As Guid = Guid.Empty
        Public AccountingCompanyId As Guid = Guid.Empty
        Public EventName As String
        Public IsGridVisible As Boolean
        Private mnPageIndex As Integer = 0
        Private msPageSort As String
        Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As AcctEvent.AcctEventSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = AcctEvent.AcctEventSearchDV.COL_EVENT_NAME
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

        Public Property SearchDataView() As AcctEvent.AcctEventSearchDV
            Get
                Return searchDV
            End Get
            Set(Value As AcctEvent.AcctEventSearchDV)
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
            Dim retObj As AccountingEventForm.ReturnType = CType(ReturnPar, AccountingEventForm.ReturnType)

            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        State.AccountingEventId = retObj.EditingBo.Id
                        State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region
#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()

        Try
            If Not IsPostBack Then
                Populate()
                If IsReturningFromChild = True Then
                    GetSession()
                End If
                SortDirection = State.SortExpression
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If
                'Me.TranslateGridHeader(Me.Grid)
                'Me.TranslateGridControls(Me.Grid)
                SetGridItemStyleColor(Grid)
            End If

            If IsReturningFromChild = True Then
                IsReturningFromChild = False
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)

    End Sub


#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()

        Dim AccountingCompanyId As Guid
        Dim EventTypeId As Guid
        Dim BusinessUnitId As Guid

        If moAccountingCompanyDropDown.SelectedValue = NOTHING_SELECTED Then
            ErrorCtrl.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
            Exit Sub
        Else
            AccountingCompanyId = New Guid(moAccountingCompanyDropDown.SelectedValue)
        End If

        If moAccountingEventTypeDropDown.SelectedValue = NOTHING_SELECTED Then
            EventTypeId = Guid.Empty
        Else
            EventTypeId = New Guid(moAccountingEventTypeDropDown.SelectedValue)
        End If

        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = AcctEvent.getList(EventTypeId, AccountingCompanyId)
        End If
        State.searchDV.Sort = State.SortExpression
        Grid.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.AccountingEventId, Grid, State.PageIndex)
        SortAndBindGrid()

    End Sub
    Private Sub SortAndBindGrid()
        'Me.State.PageIndex = Me.Grid.PageIndex
        'Me.Grid.DataSource = Me.State.searchDV
        'HighLightSortColumn(Grid, Me.State.SortExpression)
        'Me.Grid.DataBind()

        'ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

        'ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        'Session("recCount") = Me.State.searchDV.Count

        'If Me.State.searchDV.Count > 0 Then

        '    If Me.Grid.Visible Then
        '        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        '    End If
        'Else
        '    If Me.Grid.Visible Then
        '        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        '    End If
        'End If

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

    Protected Sub Populate()

        For Each _acctCo As AcctCompany In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies
            If Not _acctCo.IsNew Then
                moAccountingCompanyDropDown.Items.Add(New System.Web.UI.WebControls.ListItem(_acctCo.Description, _acctCo.Id.ToString))
            Else
                ErrorCtrl.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_COMPANIES_NOT_FOUND_ERR)
                EnableControls = False
                Exit Sub
            End If
        Next

        If moAccountingCompanyDropDown.Items.Count = 1 Then
            moAccountingCompanyDropDown.SelectedIndex = 0
            ControlMgr.SetEnableControl(Me, moAccountingCompanyDropDown, False)
        ElseIf moAccountingCompanyDropDown.Items.Count = 0 Then
            ErrorCtrl.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_COMPANIES_NOT_FOUND_ERR)
            EnableControls = False
        ElseIf moAccountingCompanyDropDown.Items.Count > 1 Then
            ControlMgr.SetEnableControl(Me, moAccountingCompanyDropDown, True)
            moAccountingCompanyDropDown.Items.Add(New System.Web.UI.WebControls.ListItem("", NOTHING_SELECTED))
            moAccountingCompanyDropDown.SelectedValue = NOTHING_SELECTED
        End If

        '(Me.moAccountingEventTypeDropDown, LookupListNew.DropdownLookupListByDisplayToUserOption(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
        Dim moAccountingEventTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ACCTTRANSTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        moAccountingEventTypeDropDown.Populate(moAccountingEventTypeLkl, New PopulateOptions() With
         {
        .AddBlankItem = True
              })
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
        If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                e.Row.Cells(GRID_COL_ACCT_EVENT_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(AcctEvent.AcctEventSearchDV.COL_EVENT_ID), Byte()))
                e.Row.Cells(GRID_COL_ACCT_EVENT_TYPE_IDX).Text = dvRow(AcctEvent.AcctEventSearchDV.COL_EVENT_TYPE).ToString
            End If
        End If

    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                State.AccountingEventId = New Guid(Grid.Rows(index).Cells(GRID_COL_ACCT_EVENT_IDX).Text)
                SetSession()
                callPage(AccountingEventForm.URL, State.AccountingEventId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
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
            HandleErrors(ex, ErrorCtrl)
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
            State.SortExpression = SortDirection
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            State.AccountingEventId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()
        State.AccountingEventId = New Guid(moAccountingCompanyDropDown.SelectedValue)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.AccountingCompanyId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        SetSession()
        callPage(AccountingEventForm.URL)
    End Sub
    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
    End Sub

    Private Sub ClearSearchCriteria()

        Try
            moAccountingEventTypeDropDown.SelectedIndex = 0
            moAccountingCompanyDropDown.SelectedIndex = 0

            'Update Page State
            With State
                .AccountingEventId = Guid.Empty
                .EventName = String.Empty
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub
#End Region

#Region "State-Management"

    Private Sub SetSession()
        With State
            .AccountingCompanyId = New Guid(moAccountingCompanyDropDown.SelectedValue)
            .PageIndex = Grid.PageIndex
            .PageSize = Grid.PageSize
            .PageSort = State.SortExpression
            .SearchDataView = State.searchDV
        End With
    End Sub

    Private Sub GetSession()
        With State
            If moAccountingCompanyDropDown.Items.FindByValue(.AccountingCompanyId.ToString) IsNot Nothing Then
                moAccountingCompanyDropDown.SelectedValue = .AccountingCompanyId.ToString
            End If
            Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
        End With
    End Sub
#End Region


End Class
