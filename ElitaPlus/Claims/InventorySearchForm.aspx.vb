Option Infer On
Option Explicit On
Imports System.Collections.Generic
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentService
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Partial Public Class InventorySearchForm
    Inherits ElitaPlusSearchPage

#Region "Constants"

    Private Const DefaultItem As Integer = 0
    Public Const GridColCaseIdIdx As Integer = 0
    Public Const GridColCaseStatusCodeIdx As Integer = 2
    Public Const GridColCaseNumberCtrl As String = "btnEditCase"
    Public Const SelectActionCommand As String = "SelectAction"
    Public Const Url As String = "~/Claims/InventorySearchForm.aspx"

    Private Const DefaultSortColumn As String = "VENDORSKU"
    Private Const ResponseStatusFailure As String = "Failure"
    Private Const SearchLimit As Integer = 100 ' number of records to be return in case of search
#End Region

#Region "Variable"
#End Region

#Region "Page State"
    Class MyState


        Public DealerId As Guid
        Public DealerCode As String
        Public DealerFulfillmentProviderClassCode As String = String.Empty
        Public Manufacture As String
        Public Model As String
        Public VendorSkuNumber As String
        Public ServiceCenterCode As String
        Public Memory As String
        Public Color As String

        Public VendorInventoryData As IEnumerable(Of VendorInventory)
        Public TotalResultCountFound As Integer

        Public PageIndex As Integer = 0
        Public PageSize As Integer = 30
        Public SortColumn As String = DefaultSortColumn
        Public SortDirection As WebControls.SortDirection = WebControls.SortDirection.Ascending
        Public ReadOnly Property SortExpression As String
            Get
                Return String.Format("{0} {1}", SortColumn, If(SortDirection = WebControls.SortDirection.Ascending, "ASC", "DESC"))
            End Get
        End Property

        Sub New()
        End Sub
    End Class
#End Region
    Public Sub New()
        MyBase.New(New MyState)
    End Sub
    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

#Region "Handlers-Init, page events"


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            MasterPage.MessageController.Clear()
            Form.DefaultButton = btnSearch.UniqueID
            If Not IsPostBack Then
                ' Populate the header and breadcrumb
                UpdateBreadCrum()
                TranslateGridHeader(Grid)
                If Authentication.CurrentUser.IsDealer Then
                    SetStateDealer(Authentication.CurrentUser.ScDealerId)
                    ControlMgr.SetEnableControl(Me, ddlDealer, False)
                End If

                PopulateControls()
                SetSearchControlVisible()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
            End If
            DisplayNewProgressBarOnClick(btnSearch, "LOADING_INVENTORY")
            ShowMissingTranslations(MasterPage.MessageController)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try


    End Sub
#End Region

#Region "Handlers-DropDown"
    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.VendorInventoryData.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ddlDealer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDealer.SelectedIndexChanged
        Try
            ClearStateValues()
            SetStateDealer(GetSelectedItem(ddlDealer))
            SetSearchControlVisible()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Handlers-Buttons"
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            ControlMgr.SetVisibleControl(Me, Grid, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)

            SetStateProperties()
            If ValidateForm() Then
                State.PageIndex = 0
                GetDeviceData()
                PopulateGrid()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try

            ' Clear all search options typed or selected by the user
            ClearAllSearchOptions()

            ' Update the state properties with the new value
            SetStateProperties()

            ' Set the search fields
            SetSearchControlVisible()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Populate"

    Private Sub PopulateControls()
        PopulateDealerDropdown()
        PopulateManufactureDropDown()
        PopulateSortByDropDown()
    End Sub
    Private Sub PopulateDealerDropdown()
        Dim dealerList As ListItem() = GetDealerListByCompanyForUser()
        ddlDealer.Populate(dealerList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True,
                                                   .ValueFunc = AddressOf .GetListItemId,
                                                   .TextFunc = Function(x)
                                                                   Return x.Translation + " (" + x.Code + ")"
                                                               End Function,
                                                   .SortFunc = AddressOf .GetDescription
                                                  })

        'Dim dv As DataView = LookupListNew.GetDealerLookupList(Authentication.CurrentUser.Companies, False, "Code")
        'BindCodeNameToListControl(ddlDealer, dv, ,,, True)

        If ddlDealer.Items.Count > DefaultItem Then
            If State.DealerId.Equals(Guid.Empty) Then
                ddlDealer.SelectedIndex = DefaultItem
            Else
                SetSelectedItem(ddlDealer, State.DealerId)
            End If
        End If
    End Sub

    Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Dim Index As Integer
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

        Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

        For Index = 0 To UserCompanies.Count - 1
            oListContext.CompanyId = UserCompanies(Index)
            Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
            If oDealerListForCompany.Count > 0 Then
                If oDealerList IsNot Nothing Then
                    oDealerList.AddRange(oDealerListForCompany)
                Else
                    oDealerList = oDealerListForCompany.Clone()
                End If
            End If
        Next

        Return oDealerList.ToArray()

    End Function

    Private Sub PopulateServiceCenterDropdown()
        Dim listcontextForScList As ListContext = New ListContext()
        listcontextForScList.DealerId = State.DealerId
        Dim serviceCenterList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByDealer", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontextForScList)

        ddlServiceCenter.Populate(serviceCenterList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True,
                                                   .BlankItemValue = String.Empty,
                                                   .ValueFunc = AddressOf .GetCode,
                                                   .TextFunc = AddressOf .GetDescription,
                                                   .SortFunc = AddressOf .GetDescription
                                                  })

        'Dim dv As DataView = LookupListNew.GetServiceCenterByDealerLookupList(State.DealerId)
        'BindCodeToListControl(ddlServiceCenter, dv, , , False)

        If ddlServiceCenter.Items.Count > DefaultItem Then
            If State.ServiceCenterCode Is Nothing OrElse String.IsNullOrWhiteSpace(State.ServiceCenterCode) Then
                ddlServiceCenter.SelectedIndex = DefaultItem
            Else
                SetSelectedItem(ddlServiceCenter, State.ServiceCenterCode)
            End If
        End If
    End Sub
    Private Sub PopulateManufactureDropDown()
        Dim listcontextForMakeList As ListContext = New ListContext()
        listcontextForMakeList.CompanyGroupId = Authentication.CompanyGroupId
        Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontextForMakeList)

        ddlDeviceMake.Populate(manufacturerLkl, New PopulateOptions() With
            {
              .AddBlankItem = True,
              .BlankItemValue = String.Empty,
              .ValueFunc = AddressOf .GetCode,
              .SortFunc = AddressOf .GetDescription
            })

        'BindCodeToListControl(ddlDeviceMake, LookupListNew.GetManufacturerLookupList(Authentication.CompanyGroupId), , , True)

        If ddlDeviceMake.Items.Count > DefaultItem Then
            If State.Manufacture Is Nothing OrElse String.IsNullOrWhiteSpace(State.Manufacture) Then
                ddlDeviceMake.SelectedIndex = DefaultItem
            Else
                SetSelectedItemByText(ddlDeviceMake, State.Manufacture)
            End If
        End If
    End Sub
    Private Sub PopulateSortByDropDown()
        Dim sortByList As ListItem() = CommonConfigManager.Current.ListManager.GetList("VENDORINVENTORYSORTBY", Thread.CurrentPrincipal.GetLanguageCode())

        ddlSortBy.Populate(sortByList, New PopulateOptions() With
            {
              .AddBlankItem = True,
              .BlankItemValue = String.Empty,
              .ValueFunc = AddressOf .GetCode,
              .TextFunc = AddressOf .GetDescription,
              .SortFunc = AddressOf .GetDescription
            })

        'ddlSortBy.PopulateOld("VENDORINVENTORYSORTBY", ListValueType.Description, ListValueType.Code, PopulateBehavior.None, String.Empty, ListValueType.Description)
        If String.IsNullOrWhiteSpace(State.SortColumn) Then
            SetSelectedItem(ddlSortBy, DefaultSortColumn)
        Else
            SetSelectedItem(ddlSortBy, State.SortColumn)
        End If
    End Sub
    Private Sub PopulateGrid()
        Grid.AutoGenerateColumns = False

        If State.VendorInventoryData Is Nothing OrElse State.VendorInventoryData.Count = 0 Then
            ControlMgr.SetVisibleControl(Me, Grid, False)
            lblRecordCount.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
            Grid.PageIndex = State.PageIndex
            Grid.PageSize = State.PageSize
            Dim keySelector As Func(Of VendorInventory, String)
            If (State.SortColumn = "MAKE") Then
                keySelector = Function(vi) vi.Make
            ElseIf (State.SortColumn = "MODEL") Then
                keySelector = Function(vi) vi.Model
            ElseIf (State.SortColumn = "VENDORSKU") Then
                keySelector = Function(vi) vi.VendorSku
            ElseIf (State.SortColumn = "VENDORSKUDESCRIPTION") Then
                keySelector = Function(vi) vi.VendorSkuDescription
            ElseIf (State.SortColumn = "MEMORY") Then
                keySelector = Function(vi) vi.Memory
            ElseIf (State.SortColumn = "COLOR") Then
                keySelector = Function(vi) vi.Color
            ElseIf (State.SortColumn = "INVENTORYQUANTITY") Then
                keySelector = Function(vi) vi.inventoryQuantity
            Else
                keySelector = Function(vi) vi.VendorSku
            End If
            Dim dataSource = If(State.SortDirection = WebControls.SortDirection.Ascending, State.VendorInventoryData.OrderBy(Of String)(keySelector), State.VendorInventoryData.OrderByDescending(Of String)(keySelector))

            Grid.DataSource = dataSource.ToList()
            HighLightSortColumn(Grid, State.SortExpression, True)
            Grid.DataBind()
            If State.TotalResultCountFound > SearchLimit Then
                lblRecordCount.Text = String.Format("{0} {1} {2} {3}", State.VendorInventoryData.Count.ToString(), TranslationBase.TranslateLabelOrMessage("OF"), State.TotalResultCountFound.ToString(), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            Else
                lblRecordCount.Text = String.Format("{0} {1}", State.VendorInventoryData.Count.ToString(), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            End If
        End If

        ControlMgr.SetVisibleControl(Me, trPageSize, True)

    End Sub
#End Region
#Region "Other"
    Private Sub UpdateBreadCrum()
        Dim pageTab As String
        Dim pageTitle As String
        Dim breadCrum As String = String.Empty

        pageTab = TranslationBase.TranslateLabelOrMessage("CLAIMS")
        pageTitle = TranslationBase.TranslateLabelOrMessage("INVENTORY_SEARCH")
        MasterPage.UsePageTabTitleInBreadCrum = True
        MasterPage.PageTab = pageTab
        MasterPage.PageTitle = pageTitle
        MasterPage.BreadCrum = breadCrum
    End Sub
    Private Sub SetSearchControlVisible()
        If State.DealerFulfillmentProviderClassCode = Codes.PROVIDER_CLASS_CODE__FULPROVORAEBS Then
            'Hide Non-Elita Search Inventory field - Service Center, SKU Number
            ControlMgr.SetVisibleControl(Me, LabelServiceCenter, False)
            ControlMgr.SetVisibleControl(Me, ddlServiceCenter, False)
            ControlMgr.SetVisibleControl(Me, LabelVendorSkuNumber, False)
            ControlMgr.SetVisibleControl(Me, TextBoxVendorSkuNumber, False)
            'Show Elita Search Inventory field - Device Memory and Color
            ControlMgr.SetVisibleControl(Me, trElitaInventory, True)
        Else
            'Show Non-Elita Search Inventory field - Service Center, SKU Number
            ControlMgr.SetVisibleControl(Me, LabelServiceCenter, True)
            ControlMgr.SetVisibleControl(Me, ddlServiceCenter, True)
            ControlMgr.SetVisibleControl(Me, LabelVendorSkuNumber, True)
            ControlMgr.SetVisibleControl(Me, TextBoxVendorSkuNumber, True)
            'Hide Elita Search Inventory field - Device Memory and Color
            ControlMgr.SetVisibleControl(Me, trElitaInventory, False)
            PopulateServiceCenterDropdown()
        End If
    End Sub
    Private Sub SetStateDealer(dealerId As Guid)
        If Not dealerId.Equals(Guid.Empty) Then
            Dim oDealer As New Dealer(dealerId)
            State.DealerId = oDealer.Id
            State.DealerCode = oDealer.Dealer
            State.DealerFulfillmentProviderClassCode = oDealer.DealerFulfillmentProviderClassCode
        Else
            State.DealerId = Guid.Empty
            State.DealerCode = String.Empty
            State.DealerFulfillmentProviderClassCode = String.Empty
        End If
    End Sub

    Protected Sub ClearAllSearchOptions()
        If Not Authentication.CurrentUser.IsDealer AndAlso ddlDealer.Items.Count > 0 Then
            ddlDealer.SelectedIndex = DefaultItem
        End If
        If ddlDeviceMake.Items.Count > 0 Then ddlDeviceMake.SelectedIndex = DefaultItem
        TextBoxDeviceModel.Text = String.Empty
        TextBoxVendorSkuNumber.Text = String.Empty

        If ddlServiceCenter.Items.Count > 0 Then ddlServiceCenter.SelectedIndex = DefaultItem

        TextBoxDeviceMemory.Text = String.Empty
        TextBoxDeviceColor.Text = String.Empty
        If ddlSortBy.Items.Count > 0 Then SetSelectedItem(ddlSortBy, DefaultSortColumn)

        ControlMgr.SetVisibleControl(Me, Grid, False)
        ControlMgr.SetVisibleControl(Me, trPageSize, False)

    End Sub
    Protected Sub ClearStateValues()
        'clear State
        State.DealerId = Nothing
        State.DealerCode = String.Empty
        State.DealerFulfillmentProviderClassCode = String.Empty
        State.VendorSkuNumber = String.Empty
        State.Manufacture = String.Empty
        State.Model = String.Empty

        State.ServiceCenterCode = String.Empty

        State.Memory = String.Empty
        State.Color = String.Empty

        State.SortColumn = DefaultSortColumn 'String.Empty

        State.TotalResultCountFound = 0
        State.VendorInventoryData = Nothing
    End Sub
    Private Sub SetStateProperties()
        If State Is Nothing Then
            Trace(Me, "Restoring State")
            RestoreState(New MyState)
        End If

        ClearStateValues()

        SetStateDealer(GetSelectedItem(ddlDealer))
        State.Manufacture = GetSelectedValue(ddlDeviceMake)
        State.Model = TextBoxDeviceModel.Text.Trim
        State.VendorSkuNumber = TextBoxVendorSkuNumber.Text.Trim

        State.ServiceCenterCode = GetSelectedValue(ddlServiceCenter)

        State.Memory = TextBoxDeviceMemory.Text.Trim
        State.Color = TextBoxDeviceColor.Text.Trim

        State.SortColumn = GetSelectedValue(ddlSortBy)
    End Sub
    Private Function ValidateForm() As Boolean
        Dim blnValid As Boolean = True
        If State.DealerId.Equals(Guid.Empty) Then
            blnValid = False
            MasterPage.MessageController.AddError(Message.MSG_ERR_SELECT_A_DEALER, True)
        End If
        If State.DealerFulfillmentProviderClassCode = Codes.PROVIDER_CLASS_CODE__FULPROVORAEBS Then
            If String.IsNullOrWhiteSpace(State.Manufacture) _
                AndAlso String.IsNullOrWhiteSpace(State.Model) _
                AndAlso String.IsNullOrWhiteSpace(State.Color) _
                AndAlso String.IsNullOrWhiteSpace(State.Memory) Then
                blnValid = False
                MasterPage.MessageController.AddError(Message.MSG_ERR_MANDATORY_MAKE_MODEL_COLOR_MEMORY, True)
            End If
        Else
            If String.IsNullOrWhiteSpace(State.ServiceCenterCode) Then
                blnValid = False
                MasterPage.MessageController.AddError(Message.MSG_ERR_SELECT_A_SERVICE_CENTER, True)
            End If
        End If
        Return blnValid
    End Function
    Private Sub DisplayWsErrorMessage(errCode As String, errDescription As String)
        MasterPage.MessageController.AddError(errCode & " - " & errDescription, False)
    End Sub
#End Region
#Region "Grid Action"

    Private Sub Grid_PageIndexChanged(sender As Object, e As EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Grid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Grid_SortCommand(source As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If (State.SortColumn = e.SortExpression) Then
                State.SortDirection = If(State.SortDirection = WebControls.SortDirection.Ascending, WebControls.SortDirection.Descending, WebControls.SortDirection.Ascending)
            Else
                State.SortDirection = WebControls.SortDirection.Ascending
            End If
            State.SortColumn = e.SortExpression
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Private Sub Grid_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Device Inventory Search - Call Web Service"
    ''' <summary>
    ''' Gets New Instance of Claim fulfillment Service Client with Credentials Configured
    ''' </summary>
    ''' <returns>Instance of <see cref="FulfillmentServiceClient"/></returns>
    Private Shared Function GetClient() As FulfillmentServiceClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_FULFILLMENT_SERVICE), False)
        Dim client = New FulfillmentServiceClient("CustomBinding_IFulfillmentService", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function
    Private Sub GetDeviceData()
        Dim wsRequest As SearchVendorInventoryRequest = New SearchVendorInventoryRequest()

        wsRequest.DealerCode = State.DealerCode
        wsRequest.Make = State.Manufacture
        wsRequest.Model = State.Model


        If State.DealerFulfillmentProviderClassCode = Codes.PROVIDER_CLASS_CODE__FULPROVORAEBS Then
            wsRequest.Memory = State.Memory
            wsRequest.Color = State.Color
        Else
            wsRequest.ServiceCenterCode = State.ServiceCenterCode
            wsRequest.VendorSkuNumber = State.VendorSkuNumber
        End If

        wsRequest.RecordCountRequested = SearchLimit

        Dim wsResponse As SearchVendorInventoryResponse

        Try
            wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, SearchVendorInventoryResponse)(
                                                       GetClient(),
                                                       New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                       Function(c As FulfillmentServiceClient)
                                                           Return c.SearchVendorInventory(wsRequest)
                                                       End Function)
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
            Throw
        End Try


        If wsResponse IsNot Nothing Then
            If wsResponse.GetType() Is GetType(SearchVendorInventoryResponse) Then
                If wsResponse.ResponseStatus.Equals(ResponseStatusFailure) Then
                    DisplayWsErrorMessage(wsResponse.Error.ErrorCode, wsResponse.Error.ErrorMessage)
                    Exit Sub
                End If
                If wsResponse.TotalRecordsFound > SearchLimit Then
                    MasterPage.MessageController.AddInformation(Message.MSG_MAX_LIMIT_EXCEEDED_GENERIC, True)
                End If
                State.TotalResultCountFound = wsResponse.TotalRecordsFound
                State.VendorInventoryData = wsResponse.VendorInventoryList
            End If
        End If
    End Sub

#End Region
End Class
