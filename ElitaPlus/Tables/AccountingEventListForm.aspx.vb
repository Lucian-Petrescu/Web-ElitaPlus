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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Set(ByVal Value As Boolean)
            ControlMgr.SetEnableControl(Me, Me.btnSearch, Value)
            ControlMgr.SetEnableControl(Me, Me.btnClearSearch, Value)
            ControlMgr.SetEnableControl(Me, Me.btnAdd_WRITE, Value)
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

        Public Property SearchDataView() As AcctEvent.AcctEventSearchDV
            Get
                Return searchDV
            End Get
            Set(ByVal Value As AcctEvent.AcctEventSearchDV)
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
            Dim retObj As AccountingEventForm.ReturnType = CType(ReturnPar, AccountingEventForm.ReturnType)

            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        Me.State.AccountingEventId = retObj.EditingBo.Id
                        Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region
#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()

        Try
            If Not Me.IsPostBack Then
                Populate()
                If Me.IsReturningFromChild = True Then
                    GetSession()
                End If
                Me.SortDirection = Me.State.SortExpression
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.TranslateGridHeader(Me.Grid)
                Me.TranslateGridControls(Me.Grid)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                'Me.TranslateGridHeader(Me.Grid)
                'Me.TranslateGridControls(Me.Grid)
                Me.SetGridItemStyleColor(Me.Grid)
            End If

            If Me.IsReturningFromChild = True Then
                Me.IsReturningFromChild = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)

    End Sub


#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()

        Dim AccountingCompanyId As Guid
        Dim EventTypeId As Guid
        Dim BusinessUnitId As Guid

        If Me.moAccountingCompanyDropDown.SelectedValue = Me.NOTHING_SELECTED Then
            Me.ErrorCtrl.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
            Exit Sub
        Else
            AccountingCompanyId = New Guid(Me.moAccountingCompanyDropDown.SelectedValue)
        End If

        If Me.moAccountingEventTypeDropDown.SelectedValue = Me.NOTHING_SELECTED Then
            EventTypeId = Guid.Empty
        Else
            EventTypeId = New Guid(Me.moAccountingEventTypeDropDown.SelectedValue)
        End If

        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = AcctEvent.getList(EventTypeId, AccountingCompanyId)
        End If
        Me.State.searchDV.Sort = Me.State.SortExpression
        Me.Grid.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.AccountingEventId, Me.Grid, Me.State.PageIndex)
        Me.SortAndBindGrid()

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

    Protected Sub Populate()

        For Each _acctCo As AcctCompany In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies
            If Not _acctCo.IsNew Then
                Me.moAccountingCompanyDropDown.Items.Add(New System.Web.UI.WebControls.ListItem(_acctCo.Description, _acctCo.Id.ToString))
            Else
                Me.ErrorCtrl.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_COMPANIES_NOT_FOUND_ERR)
                EnableControls = False
                Exit Sub
            End If
        Next

        If Me.moAccountingCompanyDropDown.Items.Count = 1 Then
            Me.moAccountingCompanyDropDown.SelectedIndex = 0
            ControlMgr.SetEnableControl(Me, Me.moAccountingCompanyDropDown, False)
        ElseIf Me.moAccountingCompanyDropDown.Items.Count = 0 Then
            Me.ErrorCtrl.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_COMPANIES_NOT_FOUND_ERR)
            EnableControls = False
        ElseIf Me.moAccountingCompanyDropDown.Items.Count > 1 Then
            ControlMgr.SetEnableControl(Me, Me.moAccountingCompanyDropDown, True)
            Me.moAccountingCompanyDropDown.Items.Add(New System.Web.UI.WebControls.ListItem("", Me.NOTHING_SELECTED))
            Me.moAccountingCompanyDropDown.SelectedValue = Me.NOTHING_SELECTED
        End If

        '(Me.moAccountingEventTypeDropDown, LookupListNew.DropdownLookupListByDisplayToUserOption(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))
        Dim moAccountingEventTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ACCTTRANSTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        Me.moAccountingEventTypeDropDown.Populate(moAccountingEventTypeLkl, New PopulateOptions() With
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
                e.Row.Cells(Me.GRID_COL_ACCT_EVENT_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(AcctEvent.AcctEventSearchDV.COL_EVENT_ID), Byte()))
                e.Row.Cells(Me.GRID_COL_ACCT_EVENT_TYPE_IDX).Text = dvRow(AcctEvent.AcctEventSearchDV.COL_EVENT_TYPE).ToString
            End If
        End If

    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                Me.State.AccountingEventId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_ACCT_EVENT_IDX).Text)
                SetSession()
                Me.callPage(AccountingEventForm.URL, Me.State.AccountingEventId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.AccountingEventId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()
        Me.State.AccountingEventId = New Guid(Me.moAccountingCompanyDropDown.SelectedValue)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.AccountingCompanyId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        SetSession()
        Me.callPage(AccountingEventForm.URL)
    End Sub
    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
    End Sub

    Private Sub ClearSearchCriteria()

        Try
            Me.moAccountingEventTypeDropDown.SelectedIndex = 0
            Me.moAccountingCompanyDropDown.SelectedIndex = 0

            'Update Page State
            With Me.State
                .AccountingEventId = Guid.Empty
                .EventName = String.Empty
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub
#End Region

#Region "State-Management"

    Private Sub SetSession()
        With Me.State
            .AccountingCompanyId = New Guid(Me.moAccountingCompanyDropDown.SelectedValue)
            .PageIndex = Grid.PageIndex
            .PageSize = Grid.PageSize
            .PageSort = Me.State.SortExpression
            .SearchDataView = Me.State.searchDV
        End With
    End Sub

    Private Sub GetSession()
        With Me.State
            If Not Me.moAccountingCompanyDropDown.Items.FindByValue(.AccountingCompanyId.ToString) Is Nothing Then
                Me.moAccountingCompanyDropDown.SelectedValue = .AccountingCompanyId.ToString
            End If
            Me.Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
        End With
    End Sub
#End Region


End Class
