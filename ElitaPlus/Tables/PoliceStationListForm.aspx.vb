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

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

        Public Property SearchDataView() As PoliceStation.PoliceStationSearchDV
            Get
                Return searchDV
            End Get
            Set(Value As PoliceStation.PoliceStationSearchDV)
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
            Dim retObj As PoliceStationForm.ReturnType = CType(ReturnPar, PoliceStationForm.ReturnType)

            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        State.PoliceStationId = retObj.EditingBo.Id
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
                SetDefaultButton(SearchCodeTextBox, btnSearch)
                SetDefaultButton(SearchDescriptionTextBox, btnSearch)
                If IsReturningFromChild = True Then
                    GetSession()
                End If
                PopulateFormFromBOs()
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
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)

    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

            Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                  Select x).ToArray()
            '  Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
            moCountryDrop.Populate(filteredCountryList, New PopulateOptions())
            If IsReturningFromChild = True Then
                SetSelectedItem(moCountryDrop, State.CountryId)
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
        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = PoliceStation.getList(SearchDescriptionTextBox.Text, SearchCodeTextBox.Text, GetSelectedItem(moCountryDrop))
        End If
        State.searchDV.Sort = State.SortExpression
        Grid.AutoGenerateColumns = False
        Grid.Columns(GRID_COL_POLICE_STATION_CODE_IDX).SortExpression = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_CODE
        Grid.Columns(GRID_COL_POLICE_STATION_NAME_IDX).SortExpression = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_NAME
        Grid.Columns(GRID_COL_POLICE_STATION_DISTRICT_CODE_IDX).SortExpression = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_DISTRICT_CODE
        Grid.Columns(GRID_COL_POLICE_STATION_DISTRICT_NAME_IDX).SortExpression = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_DISTRICT_NAME

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.PoliceStationId, Grid, State.PageIndex)
        SortAndBindGrid()

    End Sub
    Private Sub SortAndBindGrid()
        State.PageIndex = Grid.CurrentPageIndex
        Grid.DataSource = State.searchDV
        HighLightSortColumn(Grid, State.SortExpression)
        Grid.DataBind()

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

    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
            e.Item.Cells(GRID_COL_POLICE_STATION_CODE_IDX).Text = dvRow(PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_CODE).ToString
            e.Item.Cells(GRID_COL_POLICE_STATION_NAME_IDX).Text = dvRow(PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_NAME).ToString
            e.Item.Cells(GRID_COL_POLICE_STATION_DISTRICT_CODE_IDX).Text = dvRow(PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_DISTRICT_CODE).ToString
            e.Item.Cells(GRID_COL_POLICE_STATION_DISTRICT_NAME_IDX).Text = dvRow(PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_DISTRICT_NAME).ToString
            e.Item.Cells(GRID_COL_POLICE_STATION_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_ID), Byte()))
        End If
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                State.PoliceStationId = New Guid(e.Item.Cells(GRID_COL_POLICE_STATION_IDX).Text)
                SetSession()
                callPage(PoliceStationForm.URL, State.PoliceStationId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.PoliceStationId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()
        State.DescriptionMask = SearchDescriptionTextBox.Text
        State.CodeMask = SearchCodeTextBox.Text
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.PoliceStationId = Guid.Empty
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
        callPage(PoliceStationForm.URL)
    End Sub
    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
    End Sub

    Private Sub ClearSearchCriteria()

        Try
            SearchDescriptionTextBox.Text = String.Empty
            SearchCodeTextBox.Text = String.Empty

            'Update Page State
            With State
                .DescriptionMask = SearchDescriptionTextBox.Text
                .CodeMask = SearchCodeTextBox.Text
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub
#End Region

#Region "State-Management"

    Private Sub SetSession()
        With State
            .CodeMask = SearchCodeTextBox.Text
            .DescriptionMask = SearchDescriptionTextBox.Text
            .CountryId = GetSelectedItem(moCountryDrop)
            .PageIndex = Grid.CurrentPageIndex
            .PageSize = Grid.PageSize
            .PageSort = State.SortExpression
            .SearchDataView = State.searchDV
        End With
    End Sub

    Private Sub GetSession()
        'Dim oDataView As DataView
        With State
            SearchCodeTextBox.Text = .CodeMask
            SearchDescriptionTextBox.Text = .DescriptionMask
            Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
            'oDataView.Sort = .PageSort
        End With
    End Sub
#End Region




End Class
