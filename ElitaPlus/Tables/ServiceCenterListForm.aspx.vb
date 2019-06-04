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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As ServiceCenterForm.ReturnType = CType(ReturnPar, ServiceCenterForm.ReturnType)
            Me.State.HasDataChanged = retObj.HasDataChanged
            If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                Me.State.searchDV = Nothing
            End If
            Me.State.IsGridVisible = True
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.selectedServiceCenterId = retObj.EditingBo.Id
                        End If
                        '     Me.State.IsGridVisible = True
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

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(SERVICE_CENTER)
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try

            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(TABLES)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SERVICE_CENTER)
            Me.UpdateBreadCrum()

            If Not Me.IsPostBack Then
                Me.SetDefaultButton(Me.TextBoxSearchCode, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchDescription, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchAddress, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchCity, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchZip, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                PopulateCountry()
                PopulateSearchFieldsFromState()
                If Me.State.IsGridVisible Then
                    'If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                    '    cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                    '    Grid.PageSize = Me.State.selectedPageSize
                    'End If
                    Me.PopulateGrid()
                End If
                'Set page size
                cboPageSize.SelectedValue = Me.State.selectedPageSize.ToString()
                Me.SetGridItemStyleColor(Me.Grid)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub PopulateCountry()
        ' Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList())
        Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

        Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                              Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                              Select x).ToArray()

        Me.moCountryDrop.Populate(filteredCountryList, New PopulateOptions() With
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
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = ServiceCenter.getList(Me.State.serviceCenterCode,
                                                      Me.State.serviceCenterDescription,
                                                      Me.State.address1,
                                                      Me.State.city,
                                                      Me.State.zip,
                                                      Me.State.SearchCountryId)

            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'If (Not (Me.State.HasDataChanged)) Then
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            'End If
        End If

        If Not (Me.State.searchDV Is Nothing) Then
            Me.State.searchDV.Sort = Me.State.SortExpression
            Me.Grid.AutoGenerateColumns = False
            Me.Grid.PageSize = Me.State.selectedPageSize

            Me.Grid.Columns(Me.GRID_COL_COUNTRY_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_COUNTRY_DESC
            Me.Grid.Columns(Me.GRID_COL_CODE_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_CODE
            Me.Grid.Columns(Me.GRID_COL_SERVICE_GROUP_DESCX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_SERVICE_GROUP_DESC
            Me.Grid.Columns(Me.GRID_COL_DESCRIPTION_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_DESCRIPTION
            Me.Grid.Columns(Me.GRID_COL_ADDRESS_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_ADDRESS
            Me.Grid.Columns(Me.GRID_COL_CITY_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_CITY
            Me.Grid.Columns(Me.GRID_COL_ZIP_IDX).SortExpression = ServiceCenter.ServiceCenterSearchDV.COL_ZIP

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedServiceCenterId, Me.Grid, Me.State.PageIndex)
            Me.SortAndBindGrid()
        End If

    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.CurrentPageIndex
        Me.Grid.DataSource = Me.State.searchDV
        HighLightSortColumn(Grid, Me.State.SortExpression, Me.IsNewUI)
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

    Public Sub PopulateSearchFieldsFromState()
        Me.SetSelectedItem(moCountryDrop, Me.State.SearchCountryId)
        Me.TextBoxSearchCode.Text = Me.State.serviceCenterCode
        Me.TextBoxSearchDescription.Text = Me.State.serviceCenterDescription
        Me.TextBoxSearchAddress.Text = Me.State.address1
        Me.TextBoxSearchCity.Text = Me.State.city
        Me.TextBoxSearchZip.Text = Me.State.zip
    End Sub

    Public Sub PopulateStateFromSearchFields()
        Me.State.SearchCountryId = Me.GetSelectedItem(moCountryDrop)
        Me.State.serviceCenterCode = Me.TextBoxSearchCode.Text
        If (Not (Me.TextBoxSearchDescription.Text Is Nothing)) Then
            Me.State.serviceCenterDescription = Me.TextBoxSearchDescription.Text.ToUpper
        Else
            Me.State.serviceCenterDescription = Nothing
        End If
        Me.State.address1 = Me.TextBoxSearchAddress.Text
        Me.State.city = Me.TextBoxSearchCity.Text
        Me.State.zip = Me.TextBoxSearchZip.Text
    End Sub

    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(ByVal dv As ServiceCenter.ServiceCenterSearchDV) As Integer
        If Me.State.selectedServiceCenterId.Equals(Guid.Empty) Then
            Return -1
        Else
            'Jump to the Right Page
            Dim i As Integer
            For i = 0 To dv.Count - 1
                If New Guid(CType(dv(i)(ServiceCenter.ServiceCenterSearchDV.COL_SERVICE_CENTER_ID), Byte())).Equals(Me.State.selectedServiceCenterId) Then
                    Return i
                End If
            Next
        End If
        Return -1
    End Function


#End Region

#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim btnEditButtonCode As LinkButton

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                If (Not e.Item.Cells(Me.GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_CTRL) Is Nothing) Then
                    btnEditButtonCode = CType(e.Item.Cells(Me.GRID_COL_CODE_IDX).FindControl(GRID_COL_CODE_CTRL), LinkButton)
                    btnEditButtonCode.Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_CODE).ToString
                    btnEditButtonCode.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(ServiceCenter.ServiceCenterSearchDV.COL_SERVICE_CENTER_ID), Byte()))
                    btnEditButtonCode.CommandName = SELECT_ACTION_COMMAND
                End If

                e.Item.Cells(Me.GRID_COL_SERVICE_CENTER_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(ServiceCenter.ServiceCenterSearchDV.COL_SERVICE_CENTER_ID), Byte()))
                e.Item.Cells(Me.GRID_COL_COUNTRY_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_COUNTRY_DESC).ToString
                ' e.Item.Cells(Me.GRID_COL_CODE_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_CODE).ToString
                e.Item.Cells(Me.GRID_COL_SERVICE_GROUP_DESCX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_SERVICE_GROUP_DESC).ToString
                e.Item.Cells(Me.GRID_COL_DESCRIPTION_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_DESCRIPTION).ToString
                e.Item.Cells(Me.GRID_COL_ADDRESS_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_ADDRESS).ToString
                e.Item.Cells(Me.GRID_COL_CITY_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_CITY).ToString
                e.Item.Cells(Me.GRID_COL_ZIP_IDX).Text = dvRow(ServiceCenter.ServiceCenterSearchDV.COL_ZIP).ToString
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Me.State.selectedServiceCenterId = New Guid(e.Item.Cells(Me.GRID_COL_SERVICE_CENTER_ID_IDX).Text)
                Me.callPage(ServiceCenterForm.URL, New ServiceCenterForm.Parameters(Me.State.selectedServiceCenterId))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.selectedServiceCenterId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.selectedServiceCenterId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.State.searchBtnClicked = True
            Me.PopulateGrid()
            Me.State.searchBtnClicked = False
            Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            Me.callPage(ServiceCenterForm.URL)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.TextBoxSearchCode.Text = String.Empty
            Me.TextBoxSearchDescription.Text = String.Empty
            Me.TextBoxSearchAddress.Text = String.Empty
            Me.TextBoxSearchCity.Text = String.Empty
            Me.TextBoxSearchZip.Text = String.Empty
            moCountryDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

End Class

