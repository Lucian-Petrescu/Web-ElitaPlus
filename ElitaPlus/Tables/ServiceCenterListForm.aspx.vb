Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Partial Class ServiceCenterListForm
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
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_CODE_IDX As Integer = 1
    Public Const GRID_COL_SERVICE_GROUP_DESCX As Integer = 2
    Public Const GRID_COL_DESCRIPTION_IDX As Integer = 3
    Public Const GRID_COL_ADDRESS_IDX As Integer = 4
    Public Const GRID_COL_CITY_IDX As Integer = 5
    Public Const GRID_COL_ZIP_IDX As Integer = 6
    Public Const GRID_COL_COUNTRY_IDX As Integer = 7
    Public Const GRID_COL_SERVICE_CENTER_ID_IDX As Integer = 8
    Public Const GRID_TOTAL_COLUMNS As Integer = 9
    Public Const GRID_COL_CODE_CTRL As String = "btnEditCode"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Public Const TABLES As String = "Tables"
    Public Const SERVICE_CENTER As String = "Service Center"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = ServiceCenter.ServiceCenterSearchDV.COL_DESCRIPTION
        Public selectedServiceCenterId As Guid = Guid.Empty
        Public SearchCountryId As Guid
        Public serviceCenterCode As String
        Public serviceCenterDescription As String
        Public serviceGroupID As Guid
        Public address1 As String
        Public city As String
        Public zip As String
        Public selectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public IsGridVisible As Boolean
        Public searchDV As ServiceCenter.ServiceCenterSearchDV = Nothing
        Public HasDataChanged As Boolean
        Public searchBtnClicked As Boolean = False
        Public servicegroup As ServiceGroup = Nothing

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
            Dim retObj As ServiceCenterForm.ReturnType = CType(ReturnPar, ServiceCenterForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                State.searchDV = Nothing
            End If
            State.IsGridVisible = True
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.selectedServiceCenterId = retObj.EditingBo.Id
                        End If
                        '     Me.State.IsGridVisible = True
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

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(SERVICE_CENTER)
        End If
    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try

            MasterPage.MessageController.Clear()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(TABLES)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SERVICE_CENTER)
            UpdateBreadCrum()

            If Not IsPostBack Then
                SetDefaultButton(TextBoxSearchCode, btnSearch)
                SetDefaultButton(TextBoxSearchDescription, btnSearch)
                SetDefaultButton(TextBoxSearchAddress, btnSearch)
                SetDefaultButton(TextBoxSearchCity, btnSearch)
                SetDefaultButton(TextBoxSearchZip, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                PopulateCountry()
                PopulateSearchFieldsFromState()
                If State.IsGridVisible Then
                    'If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                    '    cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                    '    Grid.PageSize = Me.State.selectedPageSize
                    'End If
                    PopulateGrid()
                End If
                'Set page size
                cboPageSize.SelectedValue = State.selectedPageSize.ToString()
                SetGridItemStyleColor(Grid)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
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
            ControlMgr.SetVisibleControl(Me, TRlblCountry, False)
            ControlMgr.SetVisibleControl(Me, TRddlCountry, False)
        End If
    End Sub

    Public Sub PopulateGrid()

        PopulateStateFromSearchFields()

        'If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) AndAlso (Me.State.searchBtnClicked) Then
        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = ServiceCenter.getList(State.serviceCenterCode,
                                                      State.serviceCenterDescription,
                                                      State.address1,
                                                      State.city,
                                                      State.zip,
                                                      State.SearchCountryId)

            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'If (Not (Me.State.HasDataChanged)) Then
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            'End If
        End If

        If Not (State.searchDV Is Nothing) Then
            State.searchDV.Sort = State.SortExpression
            Grid.AutoGenerateColumns = False
            Grid.PageSize = State.selectedPageSize

            Grid.Columns(GRID_COL_COUNTRY_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_COUNTRY_DESC
            Grid.Columns(GRID_COL_CODE_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_CODE
            Grid.Columns(GRID_COL_SERVICE_GROUP_DESCX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_SERVICE_GROUP_DESC
            Grid.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_DESCRIPTION
            Grid.Columns(GRID_COL_ADDRESS_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_ADDRESS
            Grid.Columns(GRID_COL_CITY_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_CITY
            Grid.Columns(GRID_COL_ZIP_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_ZIP

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedServiceCenterId, Grid, State.PageIndex)
            SortAndBindGrid()
        End If

    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = Grid.CurrentPageIndex
        Grid.DataSource = State.searchDV
        HighLightSortColumn(Grid, State.SortExpression, IsNewUI)
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

    Public Sub PopulateSearchFieldsFromState()
        SetSelectedItem(moCountryDrop, State.SearchCountryId)
        TextBoxSearchCode.Text = State.serviceCenterCode
        TextBoxSearchDescription.Text = State.serviceCenterDescription
        TextBoxSearchAddress.Text = State.address1
        TextBoxSearchCity.Text = State.city
        TextBoxSearchZip.Text = State.zip
    End Sub

    Public Sub PopulateStateFromSearchFields()
        State.SearchCountryId = GetSelectedItem(moCountryDrop)
        State.serviceCenterCode = TextBoxSearchCode.Text
        If (Not (TextBoxSearchDescription.Text Is Nothing)) Then
            State.serviceCenterDescription = TextBoxSearchDescription.Text.ToUpper
        Else
            State.serviceCenterDescription = Nothing
        End If
        State.address1 = TextBoxSearchAddress.Text
        State.city = TextBoxSearchCity.Text
        State.zip = TextBoxSearchZip.Text
    End Sub

    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(dv As ServiceCenter.ServiceCenterSearchDV) As Integer
        If State.selectedServiceCenterId.Equals(Guid.Empty) Then
            Return -1
        Else
            'Jump to the Right Page
            Dim i As Integer
            For i = 0 To dv.Count - 1
                If New Guid(CType(dv(i)(ServiceCenter.ServiceCenterSearchDV.COL_SERVICE_CENTER_ID), Byte())).Equals(State.selectedServiceCenterId) Then
                    Return i
                End If
            Next
        End If
        Return -1
    End Function


#End Region

#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim btnEditButtonCode As LinkButton

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then

                If (e.Item.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_CTRL) IsNot Nothing) Then
                    btnEditButtonCode = CType(e.Item.Cells(GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_CTRL), LinkButton)
                    btnEditButtonCode.Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_CODE).ToString
                    btnEditButtonCode.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(ServiceCenter.ServiceCenterSearchDV.COL_SERVICE_CENTER_ID), Byte()))
                    btnEditButtonCode.CommandName = SELECT_ACTION_COMMAND
                End If

                e.Item.Cells(GRID_COL_SERVICE_CENTER_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(ServiceCenter.ServiceCenterSearchDV.COL_SERVICE_CENTER_ID), Byte()))
                e.Item.Cells(GRID_COL_COUNTRY_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_COUNTRY_DESC).ToString
                ' e.Item.Cells(Me.GRID_COL_CODE_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_CODE).ToString
                e.Item.Cells(GRID_COL_SERVICE_GROUP_DESCX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_SERVICE_GROUP_DESC).ToString
                e.Item.Cells(GRID_COL_DESCRIPTION_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_DESCRIPTION).ToString
                e.Item.Cells(GRID_COL_ADDRESS_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_ADDRESS).ToString
                e.Item.Cells(GRID_COL_CITY_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_CITY).ToString
                e.Item.Cells(GRID_COL_ZIP_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_ZIP).ToString
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
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
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                State.selectedServiceCenterId = New Guid(e.Item.Cells(GRID_COL_SERVICE_CENTER_ID_IDX).Text)
                callPage(ServiceCenterForm.URL, New ServiceCenterForm.Parameters(State.selectedServiceCenterId))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.selectedServiceCenterId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.selectedServiceCenterId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            State.searchBtnClicked = True
            PopulateGrid()
            State.searchBtnClicked = False
            ValidSearchResultCountNew(State.searchDV.Count, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            callPage(ServiceCenterForm.URL)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            TextBoxSearchCode.Text = String.Empty
            TextBoxSearchDescription.Text = String.Empty
            TextBoxSearchAddress.Text = String.Empty
            TextBoxSearchCity.Text = String.Empty
            TextBoxSearchZip.Text = String.Empty
            moCountryDrop.SelectedIndex = BLANK_ITEM_SELECTED
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

End Class

