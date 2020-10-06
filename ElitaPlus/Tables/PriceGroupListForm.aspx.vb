Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class PriceGroupListForm
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
    Public Const GRID_COL_SG_ID As Integer = 1
    Public Const GRID_COL_COUNTRY As Integer = 2
    Public Const GRID_COL_CODE As Integer = 3
    Public Const GRID_COL_DESCRIPTION As Integer = 4
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = PriceGroup.PriceGroupSearchDV.COL_NAME_SHORT_DESC
        Public SelectedPGId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Public SearchCode As String
        Public SearchDescription As String
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As PriceGroup.PriceGroupSearchDV = Nothing
        Public HasDataChanged As Boolean
        Public searchBtnClicked As Boolean = False
        Public SearchCountryId As Guid = Guid.Empty

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
            Dim retObj As PriceGroupForm.ReturnType = CType(ReturnPar, PriceGroupForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.SelectedPGId = retObj.EditingBo.Id
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

    Private Sub SaveGuiState()
        State.SearchCountryId = GetSelectedItem(moCountryDrop)
        State.SearchDescription = TextBoxSearchDescription.Text
        State.SearchCode = TextBoxSearchCode.Text
    End Sub

    Private Sub RestoreGuiState()
        SetSelectedItem(moCountryDrop, State.SearchCountryId)
        TextBoxSearchDescription.Text = State.SearchDescription
        TextBoxSearchCode.Text = State.SearchCode
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            ErrorCtrl.Clear_Hide()
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

        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = PriceGroup.getList(TextBoxSearchCode.Text, TextBoxSearchDescription.Text, State.SearchCountryId)
        End If
        State.searchDV.Sort = State.SortExpression

        Grid.AutoGenerateColumns = False
        Grid.Columns(GRID_COL_COUNTRY).SortExpression = PriceGroup.PriceGroupSearchDV.COL_COUNTRY_DESC
        Grid.Columns(GRID_COL_CODE).SortExpression = PriceGroup.PriceGroupSearchDV.COL_NAME_SHORT_DESC
        Grid.Columns(GRID_COL_DESCRIPTION).SortExpression = PriceGroup.PriceGroupSearchDV.COL_NAME_DESCRIPTION

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.SelectedPGId, Grid, State.PageIndex)
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

    'The Binding LOgic is here  
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(GRID_COL_SG_ID).Text = New Guid(CType(dvRow(PriceGroup.PriceGroupSearchDV.COL_NAME_PRICE_GROUP_ID), Byte())).ToString
                e.Item.Cells(GRID_COL_COUNTRY).Text = dvRow(ServiceGroup.ServiceGroupSearchDV.COL_COUNTRY_DESC).ToString
                e.Item.Cells(GRID_COL_CODE).Text = dvRow(PriceGroup.PriceGroupSearchDV.COL_NAME_SHORT_DESC).ToString
                e.Item.Cells(GRID_COL_DESCRIPTION).Text = dvRow(PriceGroup.PriceGroupSearchDV.COL_NAME_DESCRIPTION).ToString
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
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

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                State.SelectedPGId = New Guid(e.Item.Cells(GRID_COL_SG_ID).Text)
                callPage(PriceGroupForm.URL, State.SelectedPGId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.SelectedPGId = Guid.Empty
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
            State.SelectedPGId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            callPage(PriceGroupForm.URL)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            TextBoxSearchCode.Text = ""
            TextBoxSearchDescription.Text = ""
            moCountryDrop.SelectedIndex = BLANK_ITEM_SELECTED
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Error Handling"


#End Region


End Class
