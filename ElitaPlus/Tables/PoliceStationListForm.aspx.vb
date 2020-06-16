Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class PoliceStationListForm
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
    Public Const GRID_COL_POLICE_STATION_CODE_IDX As Integer = 2
    Public Const GRID_COL_POLICE_STATION_NAME_IDX As Integer = 1
    Public Const GRID_COL_POLICE_STATION_DISTRICT_CODE_IDX As Integer = 4
    Public Const GRID_COL_POLICE_STATION_DISTRICT_NAME_IDX As Integer = 3
    Public Const GRID_COL_POLICE_STATION_IDX As Integer = 5

    Public Const GRID_TOTAL_COLUMNS As Integer = 6
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const POLICESTATIONLISTFORM As String = "PoliceStationListForm.aspx"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As PoliceStation
        Public PageIndex As Integer = 0
        Public DescriptionMask As String
        Public CodeMask As String
        Public CountryId As Guid
        Public PoliceStationId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Private mnPageIndex As Integer = 0
        Private msPageSort As String
        Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As PoliceStation.PoliceStationSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_NAME
        Public HasDataChanged As Boolean


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

        Public Property SearchDataView() As PoliceStation.PoliceStationSearchDV
            Get
                Return searchDV
            End Get
            Set(ByVal Value As PoliceStation.PoliceStationSearchDV)
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
            Dim retObj As PoliceStationForm.ReturnType = CType(ReturnPar, PoliceStationForm.ReturnType)

            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        Me.State.PoliceStationId = retObj.EditingBo.Id
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
                Me.SetDefaultButton(Me.SearchCodeTextBox, btnSearch)
                Me.SetDefaultButton(Me.SearchDescriptionTextBox, btnSearch)
                If Me.IsReturningFromChild = True Then
                    GetSession()
                End If
                Me.PopulateFormFromBOs()
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)

    End Sub

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

            Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                  Select x).ToArray()
            '  Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
            moCountryDrop.Populate(filteredCountryList, New PopulateOptions())
            If Me.IsReturningFromChild = True Then
                Me.SetSelectedItem(moCountryDrop, Me.State.CountryId)
            End If
            If moCountryDrop.Items.Count < 2 Then
                ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
                ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
            End If
        End With
    End Sub
#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = PoliceStation.getList(Me.SearchDescriptionTextBox.Text, Me.SearchCodeTextBox.Text, Me.GetSelectedItem(moCountryDrop))
        End If
        Me.State.searchDV.Sort = Me.State.SortExpression
        Me.Grid.AutoGenerateColumns = False
        Me.Grid.Columns(Me.GRID_COL_POLICE_STATION_CODE_IDX).SortExpression = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_CODE
        Me.Grid.Columns(Me.GRID_COL_POLICE_STATION_NAME_IDX).SortExpression = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_NAME
        Me.Grid.Columns(Me.GRID_COL_POLICE_STATION_DISTRICT_CODE_IDX).SortExpression = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_DISTRICT_CODE
        Me.Grid.Columns(Me.GRID_COL_POLICE_STATION_DISTRICT_NAME_IDX).SortExpression = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_DISTRICT_NAME

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.PoliceStationId, Me.Grid, Me.State.PageIndex)
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
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
            e.Item.Cells(Me.GRID_COL_POLICE_STATION_CODE_IDX).Text = dvRow(PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_CODE).ToString
            e.Item.Cells(Me.GRID_COL_POLICE_STATION_NAME_IDX).Text = dvRow(PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_NAME).ToString
            e.Item.Cells(Me.GRID_COL_POLICE_STATION_DISTRICT_CODE_IDX).Text = dvRow(PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_DISTRICT_CODE).ToString
            e.Item.Cells(Me.GRID_COL_POLICE_STATION_DISTRICT_NAME_IDX).Text = dvRow(PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_DISTRICT_NAME).ToString
            e.Item.Cells(Me.GRID_COL_POLICE_STATION_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_ID), Byte()))
        End If
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Me.State.PoliceStationId = New Guid(e.Item.Cells(Me.GRID_COL_POLICE_STATION_IDX).Text)
                SetSession()
                Me.callPage(PoliceStationForm.URL, Me.State.PoliceStationId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.PoliceStationId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()
        Me.State.DescriptionMask = SearchDescriptionTextBox.Text
        Me.State.CodeMask = SearchCodeTextBox.Text
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.PoliceStationId = Guid.Empty
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
        Me.callPage(PoliceStationForm.URL)
    End Sub
    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
    End Sub

    Private Sub ClearSearchCriteria()

        Try
            SearchDescriptionTextBox.Text = String.Empty
            SearchCodeTextBox.Text = String.Empty

            'Update Page State
            With Me.State
                .DescriptionMask = SearchDescriptionTextBox.Text
                .CodeMask = SearchCodeTextBox.Text
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub
#End Region

#Region "State-Management"

    Private Sub SetSession()
        With Me.State
            .CodeMask = Me.SearchCodeTextBox.Text
            .DescriptionMask = Me.SearchDescriptionTextBox.Text
            .CountryId = Me.GetSelectedItem(moCountryDrop)
            .PageIndex = Grid.CurrentPageIndex
            .PageSize = Grid.PageSize
            .PageSort = Me.State.SortExpression
            .SearchDataView = Me.State.searchDV
        End With
    End Sub

    Private Sub GetSession()
        'Dim oDataView As DataView
        With Me.State
            Me.SearchCodeTextBox.Text = .CodeMask
            Me.SearchDescriptionTextBox.Text = .DescriptionMask
            Me.Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
            'oDataView.Sort = .PageSort
        End With
    End Sub
#End Region




End Class
