Imports System.Web.Services
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports System.Web.Script.Services
Imports Assurant.ElitaPlus.Security
Imports System.Web.Script.Serialization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Partial Class ZipDistrictListForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents ErrorCtrl As ErrorController


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
    Public Const GRID_COL_EDIT As Integer = 0
    Public Const GRID_COL_ZD_ID As Integer = 1
    Public Const GRID_COL_COUNTRY As Integer = 2
    Public Const GRID_COL_CODE As Integer = 3
    Public Const GRID_COL_DESCRIPTION As Integer = 4
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = ZipDistrict.ZipDistrictSearchDV.COL_NAME_SHORT_DESC
        Public SelectedZDId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Public SearchCode As String
        Public SearchDescription As String
        Public SearchCountryId As Guid = Guid.Empty
        Public searchDV As ZipDistrict.ZipDistrictSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public HasDataChanged As Boolean
        Public searchBtnClicked As Boolean = False

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

    Private Sub SaveGuiState()
        State.SearchDescription = TextBoxSearchDescription.Text
        State.SearchCode = TextBoxSearchCode.Text
        State.SearchCountryId = GetSelectedItem(moCountryDrop)
    End Sub

    Private Sub RestoreGuiState()
        TextBoxSearchDescription.Text = State.SearchDescription
        TextBoxSearchCode.Text = State.SearchCode
        SetSelectedItem(moCountryDrop, State.SearchCountryId)
    End Sub

#End Region

#Region "Page_Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        Try
            If Not IsPostBack Then
                SetDefaultButton(TextBoxSearchCode, btnSearch)
                SetDefaultButton(TextBoxSearchDescription, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                PopulateCountry()
                RestoreGuiState()
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If
                SetGridItemStyleColor(Grid)
            Else
                SaveGuiState()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As ZipDistrictForm.ReturnType = CType(ReturnPar, ZipDistrictForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.SelectedZDId = retObj.EditingBo.Id
                        End If
                        State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End Select
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub PopulateCountry()
        ' Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList())
        Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

        Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                              Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                              Select x).ToArray()

        moCountryDrop.Populate(filteredCountryList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

        If moCountryDrop.Items.Count < 3 Then
            ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
            ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
        End If
    End Sub

    Public Sub PopulateGrid()
        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) AndAlso (State.searchBtnClicked) Then
            State.searchDV = ZipDistrict.getList(TextBoxSearchCode.Text, TextBoxSearchDescription.Text,
                                  State.SearchCountryId)
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'If (Not (Me.State.HasDataChanged)) Then
            '    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            'End If
        End If

        If State.HasDataChanged Then
            State.searchDV = ZipDistrict.getList(TextBoxSearchCode.Text, TextBoxSearchDescription.Text,
                                State.SearchCountryId)
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
        End If

        If Not (State.searchDV Is Nothing) Then
            State.searchDV.Sort = State.SortExpression

            Grid.AutoGenerateColumns = False
            Grid.Columns(GRID_COL_COUNTRY).SortExpression = ZipDistrict.ZipDistrictSearchDV.COL_NAME_COUNTRY_DESC
            Grid.Columns(GRID_COL_CODE).SortExpression = ZipDistrict.ZipDistrictSearchDV.COL_NAME_SHORT_DESC
            Grid.Columns(GRID_COL_DESCRIPTION).SortExpression = ZipDistrict.ZipDistrictSearchDV.COL_NAME_DESCRIPTION

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.SelectedZDId, Grid, State.PageIndex)
            SortAndBindGrid()
        End If
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

    'The Binding LOgic is here  
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                e.Item.Cells(GRID_COL_ZD_ID).Text = New Guid(CType(dvRow(ZipDistrict.ZipDistrictSearchDV.COL_NAME_ZIP_DISTRICT_ID), Byte())).ToString
                e.Item.Cells(GRID_COL_COUNTRY).Text = dvRow(ZipDistrict.ZipDistrictSearchDV.COL_NAME_COUNTRY_DESC).ToString
                e.Item.Cells(GRID_COL_CODE).Text = dvRow(ZipDistrict.ZipDistrictSearchDV.COL_NAME_SHORT_DESC).ToString
                e.Item.Cells(GRID_COL_DESCRIPTION).Text = dvRow(ZipDistrict.ZipDistrictSearchDV.COL_NAME_DESCRIPTION).ToString
            End If
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

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                State.SelectedZDId = New Guid(e.Item.Cells(GRID_COL_ZD_ID).Text)
                callPage(ZipDistrictForm.URL, State.SelectedZDId)
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

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.SelectedZDId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.SelectedZDId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            State.searchBtnClicked = True
            PopulateGrid()
            State.searchBtnClicked = False
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            callPage(ZipDistrictForm.URL)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            TextBoxSearchCode.Text = ""
            TextBoxSearchDescription.Text = ""
            moCountryDrop.SelectedIndex = BLANK_ITEM_SELECTED
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region




End Class

