Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.ServiceModel
Imports System.Threading
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentWebAppGatewayService
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimRecordingService
Imports Assurant.ElitaPlus.Security

Public Class UserControlServiceCenterSelection
    Inherits System.Web.UI.UserControl

    Public Class SearchByCodes
        Public Const City As String = "CITY"
        Public Const PostalCode As String = "POSTAL_CODE"
        Public Const All As String = "ALL"
        Public Const NoSvc As String = "NO_SVC"
    End Class

#Region "Locals"
    Public Enum SearchTypeEnum
        ByZip
        ByCity
        All
        None
    End Enum
#End Region
#Region "Constants"

    Private ReadOnly DefaultPageSize As Integer = 30
    Private ReadOnly PopulateOptionsWithBlankItem As PopulateOptions = New PopulateOptions() With
        {
        .AddBlankItem = True
        }
    Public Class ViewStateItems
        Public Const SearchType As String = "SearchType"
        Public Const CountryId As String = "CountryId"
        Public Const CompanyCode As String = "CompanyCode"
        Public Const CountryCode As String = "CountryCode"
        Public Const Dealer As String = "Dealer"
        Public Const Make As String = "Make"
        Public Const RiskTypeEnglish As String = "RiskTypeEnglish"
        Public Const MethodOfRepairXcd As String = "MethodOfRepairXcd"
        Public Const SelectedServiceCenter As String = "FulfillmentServicesCenter"
        Public Const ServiceCenters As String = "ServiceCenters"
        Public Const PageIndex As String = "PageIndex"
        Public Const PageSize As String = "PageSize"
        Public Const SortExpression As String = "SortExpression"
    End Class
#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim parentControl = Parent

        Debug.WriteLine($"Loading UserControlQuestion ID: {Me.ID}")
        While (Not TypeOf parentControl Is ElitaPlusPage And parentControl IsNot Nothing)
            parentControl = parentControl.Parent
        End While
        ElitaHostPage = DirectCast(parentControl, ElitaPlusPage)
        If ShowControl Then
            InitializeComponent()
        End If
    End Sub
#Region "Properties"
    Public Property HostMessageController As IMessageController
    Public Property ServiceCenterSelectedFunc As Action(Of ServiceCenterSelected)
    Public Property TranslateGridHeaderFunc As Action(Of System.Web.UI.WebControls.GridView)
    Public Property TranslationFunc As Func(Of String, String)
    Public Property HighLightSortColumnFunc As Action(Of System.Web.UI.WebControls.GridView, String)
    Public Property NewCurrentPageIndexFunc As Func(Of System.Web.UI.WebControls.GridView, Integer, Integer, Integer)
    Public Property ElitaHostPage As ElitaPlusPage
    Public Property ShowControl As Boolean = False

    Public Property CountryId As Guid
        Get
            Return DirectCast(ViewState(ViewStateItems.CountryId), Guid)
        End Get
        Set(value As Guid)
            ViewState(ViewStateItems.CountryId) = value
        End Set
    End Property
    Public Property CompanyCode As String
        Get
            Return DirectCast(ViewState(ViewStateItems.CompanyCode), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.CompanyCode) = value
        End Set
    End Property
    Public Property CountryCode As String
        Get
            Return DirectCast(ViewState(ViewStateItems.CountryCode), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.CountryCode) = value
        End Set
    End Property
    Public Property Dealer As String
        Get
            Return DirectCast(ViewState(ViewStateItems.Dealer), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.Dealer) = value
        End Set
    End Property
    Public Property Make As String
        Get
            Return DirectCast(ViewState(ViewStateItems.Make), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.Make) = value
        End Set
    End Property
    Public Property RiskTypeEnglish As String
        Get
            Return DirectCast(ViewState(ViewStateItems.RiskTypeEnglish), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.RiskTypeEnglish) = value
        End Set
    End Property
    Public Property MethodOfRepairXcd As String
        Get
            Return DirectCast(ViewState(ViewStateItems.MethodOfRepairXcd), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.MethodOfRepairXcd) = value
        End Set
    End Property
    Public Property SearchType As SearchTypeEnum
        Get
            Return DirectCast(ViewState(ViewStateItems.SearchType), SearchTypeEnum)
        End Get
        Set(value As SearchTypeEnum)
            ViewState(ViewStateItems.SearchType) = value
        End Set
    End Property
    Public Property SelectedServiceCenter As FulfillmentServicesCenter
        Get
            Return DirectCast(ViewState(ViewStateItems.SelectedServiceCenter), FulfillmentServicesCenter)
        End Get
        Set(value As FulfillmentServicesCenter)
            ViewState(ViewStateItems.SelectedServiceCenter) = value
        End Set
    End Property
    Public Property ServiceCenters As FulfillmentServicesCenter()
        Get
            Return DirectCast(ViewState(ViewStateItems.ServiceCenters), FulfillmentServicesCenter())
        End Get
        Set(value As FulfillmentServicesCenter())
            ViewState(ViewStateItems.ServiceCenters) = value
        End Set
    End Property
    Public Property PageIndex As Integer
        Get
            Return DirectCast(ViewState(ViewStateItems.PageIndex), Integer)
        End Get
        Set(value As Integer)
            ViewState(ViewStateItems.PageIndex) = value
        End Set
    End Property
    Public Property PageSize As Integer
        Get
            If ViewState(ViewStateItems.PageSize) IsNot Nothing Then
                Return DirectCast(ViewState(ViewStateItems.PageSize), Integer)
            Else
                Return DefaultPageSize
            End If
        End Get
        Set(value As Integer)
            ViewState(ViewStateItems.PageSize) = value
        End Set
    End Property
    Public Property SortExpression As String
        Get
            Return DirectCast(ViewState(ViewStateItems.SortExpression), String)
        End Get
        Set(value As String)
            ViewState(ViewStateItems.SortExpression) = value
        End Set
    End Property
#End Region

#Region "Internal Methods"
    Private Sub SetTranslations()
        If TranslationFunc Is Nothing Then
            Throw New ArgumentException("The Translation lambda function not initialized")
        End If

        lblPageSize.Text = TranslationFunc("Page_Size")
        '
        moSearchServiceCenterLabel.Text = TranslationFunc("SEARCH_SERVICE_CENTER")
        moSearchByLabel.Text = TranslationFunc("SEARCH_BY")
        moCountryLabel.Text = TranslationFunc("COUNTRY")
        moCityLabel.Text = TranslationFunc("CITY")
        moPostalCodeLabel.Text = TranslationFunc("CUST_POSTAL_CODE")
        '
        btnClearSearch.Text = TranslationFunc("Clear")
        btnSearch.Text = TranslationFunc("Search")
    End Sub

    Sub PopulateSearchFilterDropdown()
        Try
            REM Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            Dim serviceCenterSearchByList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("SERVICE_CENTER_SEARCH_BY", Thread.CurrentPrincipal.GetLanguageCode())
            moSearchByDrop.Populate(serviceCenterSearchByList, New PopulateOptions() With {.AddBlankItem = True, .BlankItemValue = String.Empty, .ValueFunc = AddressOf PopulateOptions.GetCode})

        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Sub
    Sub PopulateCountryDropdown()
        Try
            If Not CountryId.Equals(Guid.Empty) Then
                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                oListContext.CountryId = CountryId
                oListContext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
                Dim countriesList As ListItem() = CommonConfigManager.Current.ListManager.GetList("UserCountryWithSelectedCountry", Thread.CurrentPrincipal.GetLanguageCode(), oListContext)
                moCountryDrop.Populate(countriesList, PopulateOptionsWithBlankItem)
            End If
        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Sub
    Sub EnableDisableFields()

        Dim showCityFields As Boolean = IsSearchBy(SearchByCodes.City)
        Dim showAllFields As Boolean = IsSearchBy(SearchByCodes.All)
        Dim showPostalCodeFields As Boolean = IsSearchBy(SearchByCodes.PostalCode)

        moCityTextbox.Text = String.Empty
        moPostalCodeTextbox.Text = String.Empty

        ' City
        ControlMgr.SetVisibleControl(ElitaHostPage, tdCityLabel, showCityFields)
        ControlMgr.SetVisibleControl(ElitaHostPage, moCityLabel, showCityFields)
        ControlMgr.SetVisibleControl(ElitaHostPage, tdCityTextBox, showCityFields)
        ' Postal Code
        ControlMgr.SetVisibleControl(ElitaHostPage, tdPostalCodeLabel, showPostalCodeFields)
        ControlMgr.SetVisibleControl(ElitaHostPage, moPostalCodeLabel, showPostalCodeFields)
        ControlMgr.SetVisibleControl(ElitaHostPage, tdPostalCodeText, showPostalCodeFields)
        ' Buttons
        ControlMgr.SetVisibleControl(ElitaHostPage, tdClearButton, showCityFields Or showPostalCodeFields)
        ControlMgr.SetVisibleControl(ElitaHostPage, btnClearSearch, showCityFields Or showPostalCodeFields)

        ' All
        ControlMgr.SetVisibleControl(ElitaHostPage, btnSearch, Not showAllFields)
        If showAllFields Then
            PopulateGrid()
        Else
            ClearResultList()
        End If

        'NO_SVC_OPTION
        'ControlMgr.SetVisibleControl(ElitaHostPage, tdRightPanel, Not noSvcOption)

    End Sub

    Sub HandleLocalException(ex As Exception)
        Dim errorMessage As String = $"{ex.Message} {ex.StackTrace}"
        If HostMessageController IsNot Nothing Then
            HostMessageController.AddError(errorMessage, True)
        End If
    End Sub
    Sub ShowMessage(message As String)
        If HostMessageController IsNot Nothing Then
            HostMessageController.AddError(message, True)
        End If
    End Sub
    Function ParseManufacturerAuthFlagToBoolean(flagValue As String) As Boolean
        If String.IsNullOrEmpty(flagValue) Then Return False

        If flagValue.Equals("y", StringComparison.InvariantCultureIgnoreCase) Then Return True
        If flagValue.Equals("yes", StringComparison.InvariantCultureIgnoreCase) Then Return True
        If flagValue.Equals("true", StringComparison.InvariantCultureIgnoreCase) Then Return True

        Return False
    End Function
#End Region

#Region "Control Events"
    Private Sub GridServiceCenter_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridServiceCenter.Sorting
        Try
            If SortExpression.StartsWith(e.SortExpression) Then
                If SortExpression.EndsWith(" DESC") Then
                    SortExpression = e.SortExpression
                Else
                    SortExpression &= " DESC"
                End If
            Else
                SortExpression = e.SortExpression
            End If
            PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            HandleLocalException(ex)
        End Try

    End Sub

    Private Sub GridServiceCenter_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridServiceCenter.PageIndexChanged
        Try
            PageIndex = GridServiceCenter.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Sub
    Private Sub GridServiceCenter_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridServiceCenter.PageIndexChanging
        Try
            GridServiceCenter.PageIndex = e.NewPageIndex
            PageIndex = GridServiceCenter.PageIndex
        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Sub
    Protected Sub rdoServiceCenter_OnCheckedChanged(sender As Object, e As EventArgs)

        SelectedServiceCenter = Nothing
        DeselectRadioButtonGridview(GridServiceCenter, "rdoServiceCenter")
        'check the radiobutton which is checked
        Dim senderRb As System.Web.UI.WebControls.RadioButton = sender
        senderRb.Checked = True

        ' get the selected device into the state
        EnableControlinGridview(GridServiceCenter)
    End Sub
    Private Sub moSearchByDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moSearchByDrop.SelectedIndexChanged
        Try
            Debug.WriteLine("Index Changed")
            EnableDisableFields()

        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Debug.WriteLine($"btnSearch_Click")
        PopulateGrid()
    End Sub

    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Debug.WriteLine($"btnClearSearch")
        moPostalCodeTextbox.Text = String.Empty
        moCityTextbox.Text = String.Empty
        ClearResultList()
    End Sub


    Protected Sub GridServiceCenter_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridServiceCenter.RowDataBound
        Dim source As IEnumerable(Of FulfillmentServicesCenter) = CType(sender, GridView).DataSource

        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim moManufacturerAuthFlagImage As HtmlImage = CType(e.Row.FindControl("moManufacturerAuthFlagImage"), HtmlImage)
            Dim dataItem As FulfillmentServicesCenter = CType(e.Row.DataItem, FulfillmentServicesCenter)
            If dataItem IsNot Nothing Then
                ControlMgr.SetVisibleControl(ElitaHostPage, moManufacturerAuthFlagImage, ParseManufacturerAuthFlagToBoolean(dataItem.ManufacturerAuthFlag))
            End If
        End If
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Dim list As IList = GridServiceCenter.DataSource

            PageSize = CType(cboPageSize.SelectedValue, Integer)
            PageIndex = NewCurrentPageIndexFunc(GridServiceCenter, list?.Count, PageSize)
            GridServiceCenter.PageIndex = PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Sub

    Private Function GetRequestBasedOnSearchBy() As FulfillmentServiceCentersRequest
        Try
            Dim selectedItem As WebControls.ListItem = moSearchByDrop.SelectedItem

            If selectedItem.Value = SearchByCodes.City Then
                Dim request As FulfillmentServiceCentersByCityRequest = New FulfillmentServiceCentersByCityRequest()
                request.CityName = moCityTextbox.Text
                Return request

            ElseIf selectedItem.Value = SearchByCodes.PostalCode Then
                Dim request As FulfillmentServiceCentersByPostalCodeRequest = New FulfillmentServiceCentersByPostalCodeRequest()
                request.PostalCode = moPostalCodeTextbox.Text
                Return request

            ElseIf selectedItem.Value = SearchByCodes.All Then
                Dim request As FulfillmentAllServiceCentersRequest = New FulfillmentAllServiceCentersRequest()
                Return request

            ElseIf selectedItem.Value = SearchByCodes.NoSvc Then
                Dim request As FulfillmentDefaultServiceCentersRequest = New FulfillmentDefaultServiceCentersRequest()
                Return request

            Else
                Throw New ArgumentException($"Service Center Search By parameter is not supported")
            End If

        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Function
    Private Function IsSearchBy(searchByCode As String) As Boolean
        Try
            Dim selectedItem As WebControls.ListItem = moSearchByDrop.SelectedItem

            If selectedItem.Value = searchByCode Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Function

#End Region
#Region "User Control Methods"
    Sub InitializeComponent()
        Try
            SortExpression = String.Empty
            PopulateCountryDropdown()
            PopulateSearchFilterDropdown()
            EnableDisableFields()
            TranslateGridHeaderFunc.Invoke(GridServiceCenter)
            SetTranslations()

        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Sub

    Private Sub PopulateGrid()
        Try
            Dim wsRequest As FulfillmentServiceCentersRequest = GetRequestBasedOnSearchBy()

            ServiceCenters = Nothing
            wsRequest.CompanyCode = CompanyCode
            wsRequest.CountryCode = CountryCode
            wsRequest.Dealer = Dealer
            wsRequest.Make = Make
            wsRequest.RiskTypeEnglish = RiskTypeEnglish
            wsRequest.MethodOfRepairXcd = MethodOfRepairXcd

            Dim wsResponse As FulfillmentServicesCenter() = WcfClientHelper.Execute(Of WebAppGatewayClient, WebAppGateway, FulfillmentServicesCenter())(
                GetClaimFulfillmentWebAppGatewayClient(),
                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(ByVal c As WebAppGatewayClient)
                    Return c.SearchServiceCenters(wsRequest)
                End Function)

            If wsResponse IsNot Nothing Then
                ServiceCenters = wsResponse
                '
                UpdateRecordCount(wsResponse.Count)
                '
                GridServiceCenter.PageSize = PageSize
                GridServiceCenter.DataSource = wsResponse
                '
                Try
                    If Not String.IsNullOrWhiteSpace(SortExpression) Then
                        HighLightSortColumnFunc.Invoke(GridServiceCenter, SortExpression)
                    End If
                Catch ex As Exception
                    'do nothing, ignore the sorting error
                End Try

                '
                GridServiceCenter.DataBind()
                PageIndex = GridServiceCenter.PageIndex

            End If
        Catch notFoundEx As FaultException(Of ServiceCenterNotFoundFault)
            ShowMessage($"No Services Center found for RiskType: {RiskTypeEnglish}, Method: {MethodOfRepairXcd}, Make: {Make}")
        Catch fex As FaultException
            ShowMessage($"No Services Center found for RiskType: {RiskTypeEnglish}, Method: {MethodOfRepairXcd}, Make: {Make}")
            'HandleLocalException(fex)
        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Sub

    Private Sub DeselectRadioButtonGridview(ByVal gridViewTarget As GridView, ByVal rdobuttonName As String)
        'deselect all radiobutton in gridview
        For i As Integer = 0 To gridViewTarget.Rows.Count - 1
            Dim rb As System.Web.UI.WebControls.RadioButton
            rb = CType(gridViewTarget.Rows(i).FindControl(rdobuttonName), System.Web.UI.WebControls.RadioButton)
            rb.Checked = False
        Next
    End Sub
    Private Sub EnableControlinGridview(ByVal gridViewTarget As GridView)
        For i As Integer = 0 To gridViewTarget.Rows.Count - 1
            Dim rdo As System.Web.UI.WebControls.RadioButton
            rdo = CType(gridViewTarget.Rows(i).FindControl("rdoServiceCenter"), System.Web.UI.WebControls.RadioButton)

            If rdo.Checked Then
                Dim lb As System.Web.UI.WebControls.Label
                lb = CType(gridViewTarget.Rows(i).FindControl("lblServiceCenterId"), System.Web.UI.WebControls.Label)
                Dim serviceCenterId As Guid = Guid.Parse(lb.Text)
                Dim oServiceCenter As FulfillmentServicesCenter = ServiceCenters.FirstOrDefault(Function(q) q.ServiceCenterId = serviceCenterId)
                If oServiceCenter IsNot Nothing Then
                    Dim selected As ServiceCenterSelected = New ServiceCenterSelected

                    selected.ServiceCenterId = oServiceCenter.ServiceCenterId
                    selected.ServiceCenterCode = oServiceCenter.ServiceCenterCode
                    selected.Name = oServiceCenter.Name

                    SelectedServiceCenter = oServiceCenter

                    If Not ServiceCenterSelectedFunc Is Nothing Then
                        ServiceCenterSelectedFunc.Invoke(selected)
                    End If

                End If
            End If
        Next
    End Sub

    Private Sub ClearResultList()
        GridServiceCenter.DataSource = Nothing
        GridServiceCenter.DataBind()
        UpdateRecordCount(0)
    End Sub
    Private Sub UpdateRecordCount(records As Integer)
        If Me.GridServiceCenter.Visible Then
            Me.lblRecordCount.Text = $"{records} {TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)}"
        End If
    End Sub
#End Region

#Region "Web Service"
    Private Shared Function GetClaimFulfillmentWebAppGatewayClient() As WebAppGatewayClient
        Try
            Dim serviceTypeId As Guid = LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIM_FULFILLMENT_WEB_APP_GATEWAY_SERVICE)
            Dim oWebPassword As WebPasswd = New WebPasswd(Guid.Empty, serviceTypeId, False)
            If oWebPassword Is Nothing Then Throw New ArgumentNullException($"Web Password information for service {Codes.SERVICE_TYPE__CLAIM_FULFILLMENT_WEB_APP_GATEWAY_SERVICE} does not exists.")

            Dim client = New WebAppGatewayClient("CustomBinding_WebAppGateway", oWebPassword.Url)
            client.ClientCredentials.UserName.UserName = oWebPassword.UserId
            client.ClientCredentials.UserName.Password = oWebPassword.Password

            Return client
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


End Class