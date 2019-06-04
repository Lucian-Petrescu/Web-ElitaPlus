Partial Public Class RouteMaintenanceListForm
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
    Public Const PAGETITLE As String = "ROUTE_MAINTENANCE"
    Public Const PAGETAB As String = "TABLES"
    Private Const LABEL_SERVICE_NETWORK As String = "SERVICE_NETWORK"
    Private Const LABEL_ROUTE As String = "ROUTE"
    Public Const GRID_COL_EDIT As Integer = 0
    Public Const GRID_COL_EXDN_ID As Integer = 1
    Public Const GRID_COL_CODE As Integer = 2
    Public Const GRID_COL_DESCRIPTION As Integer = 3
    Public Const GRID_COL_SERVICE_NERWORK_CODE As Integer = 4
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = Route.RouteSearchDV.COL_NAME_SHORT_DESC
        Public SelectedEXDNId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Public SearchCode As String
        Public SearchDescription As String
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As Route.RouteSearchDV = Nothing
        Public HasDataChanged As Boolean

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
            Dim retObj As RouteMaintenanceDetailForm.ReturnType = CType(ReturnPar, RouteMaintenanceDetailForm.ReturnType)
            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.SelectedEXDNId = retObj.EditingBo.Id
                        End If
                        Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub SaveGuiState()
        'Me.State.SearchDescription = Me.TextBoxSearchDescription.Text
        'Me.State.SearchCode = Me.TextBoxSearchCode.Text
    End Sub

    Private Sub RestoreGuiState()
        'Me.TextBoxSearchDescription.Text = Me.State.SearchDescription
        'Me.TextBoxSearchCode.Text = Me.State.SearchCode
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                'Me.SetDefaultButton(Me.TextBoxSearchCode, btnSearch)
                'Me.SetDefaultButton(Me.TextBoxSearchDescription, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.RestoreGuiState()
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
                PopulateDropdowns()
            Else
                Me.SaveGuiState()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

#End Region

#Region "Controlling Logic"
    Protected Sub PopulateDropdowns()

        Try
            Me.moServiceNetworkMultipleDrop.NothingSelected = True
            Me.moServiceNetworkMultipleDrop.SetControl(False, moServiceNetworkMultipleDrop.MODES.NEW_MODE, True, LookupListNew.GetServiceNetworkLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), TranslationBase.TranslateLabelOrMessage(LABEL_SERVICE_NETWORK), True)
            Me.moRouteMultipleDrop.NothingSelected = True
            Me.moRouteMultipleDrop.SetControl(False, moRouteMultipleDrop.MODES.NEW_MODE, True, LookupListNew.GetRouteLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), TranslationBase.TranslateLabelOrMessage(LABEL_ROUTE), True)
        Catch ex As Exception
            Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
        End Try

    End Sub

    Public Sub PopulateGrid()
        'PopulateStateFromSearchFields()
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = Route.getList(Me.morouteMultipleDrop.SelectedGuid, Me.moServiceNetworkMultipleDrop.SelectedGuid)
        End If

        Me.State.searchDV.Sort = Me.State.SortExpression

        Me.Grid.AutoGenerateColumns = False
        Me.Grid.Columns(Me.GRID_COL_CODE).SortExpression = Route.RouteSearchDV.COL_NAME_SHORT_DESC
        Me.Grid.Columns(Me.GRID_COL_DESCRIPTION).SortExpression = Route.RouteSearchDV.COL_NAME_DESCRIPTION
        Me.Grid.Columns(Me.GRID_COL_SERVICE_NERWORK_CODE).SortExpression = Route.RouteSearchDV.COL_NAME_SERVICE_NETWORK_CODE

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.SelectedEXDNId, Me.Grid, Me.State.PageIndex)
        Me.SortAndBindGrid()

    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.CurrentPageIndex
        Me.Grid.DataSource = Me.State.searchDV
        HighLightSortColumn(Grid, Me.State.SortExpression)
        Me.Grid.DataBind()

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

    'The Binding Logic is here  
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(Me.GRID_COL_EXDN_ID).Text = New Guid(CType(dvRow(Route.RouteSearchDV.COL_NAME_ROUTE_ID), Byte())).ToString
                e.Item.Cells(Me.GRID_COL_CODE).Text = dvRow(Route.RouteSearchDV.COL_NAME_SHORT_DESC).ToString
                e.Item.Cells(Me.GRID_COL_DESCRIPTION).Text = dvRow(Route.RouteSearchDV.COL_NAME_DESCRIPTION).ToString
                e.Item.Cells(Me.GRID_COL_SERVICE_NERWORK_CODE).Text = dvRow(Route.RouteSearchDV.COL_NAME_SERVICE_NETWORK_CODE).ToString
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Me.State.SelectedEXDNId = New Guid(e.Item.Cells(Me.GRID_COL_EXDN_ID).Text)
                Me.callPage(RouteMaintenanceDetailForm.URL, Me.State.SelectedEXDNId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.SelectedEXDNId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.SelectedEXDNId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            'Me.TextBoxSearchCode.Text = ""
            'Me.TextBoxSearchDescription.Text = ""
            Me.moRouteMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
            Me.moServiceNetworkMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Me.callPage(RouteMaintenanceDetailForm.URL)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

End Class
