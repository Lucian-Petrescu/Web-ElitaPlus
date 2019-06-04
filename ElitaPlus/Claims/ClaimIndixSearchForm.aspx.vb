Imports System.Collections.Generic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.AccountingEventDetailForm
Imports Assurant.ElitaPlus.External.Indix
Imports Assurant.ElitaPlus.External.Indix.Parameters
Imports Assurant.ElitaPlus.External.Interfaces
Imports Assurant.ElitaPlus.ElitaPlus.Common
Imports Assurant.Common.MessagePublishing
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Public Class ClaimIndixSearchForm
    Inherits ElitaPlusSearchPage
    'Implements IStateController


#Region "Const"
    'REQ-6230
    Public Const INDIX_COUNTRY_CODE_OVERWRITE As String = "INDIX_COUNTRY_CODE_OVERWRITE"
    Public Const INDIX_DEFAULT_PAGE_SIZE As Integer = 5
    Public Const INDIX_DEFAULT_NUMBER_OF_RECORDS As Integer = 50
    Public Const INDIX_MORE_RECORDS_INCREMENT As Integer = 5
    Public Const INDIX_MAX_NUMBER_OF_RECORDS As Integer = 500
    Public Const PAGETITLE As String = "RETAIL_PRICE_SEARCH"
    Public Const DEVICE_NOT_ASSOCIATED_TO_INDIX_PRODUCT = "DEVICE_NOT_ASSOCIATED_TO_INDIX_PRODUCT"
    Public Const CLAIM_TAB As String = "Claim"
    Public Const GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR As String = "ENTER_SEARCH_TERM"
    Public Const MAX_SEARCH_TERM_LENGTH = 200
    Public Const SORT_BY_VALUE_COLUMN = "CODE"
    Public Const SEARCH_RESULTS_FOR As String = "SEARCH_RESULTS_FOR"
    Public Const NO_PRODUCT_FOUND_FOR_INDIX_ID As String = "NO_PRODUCT_FOUND_FOR_INDIX_ID"

    Public Const GRID_COL_MAKE_IDX As Integer = 0
    Public Const GRID_COL_MODEL_IDX As Integer = 1
    Public Const GRID_COL_MIN_SALE_PRICE_IDX As Integer = 2
    Public Const GRID_COL_MAX_SALE_PRICE_IDX As Integer = 3

    Public Const SESSION_KEY_BACKUP_STATE As String = "SESSION_KEY_BACKUP_STATE_CLAIM_LIST_FORM"
#End Region

#Region "Page State"

    Private IsReturningFromChild As Boolean = False
    Class MyState
        Public SearchTerm As String
        Public IndixCategoryID As String
        Public IndixID As String
        Public IndixCountryCode As String
        Public ClaimID As Guid
        Public CompanyID As Guid
        Public PageIndex As Integer = 0
        Public SortExpression As String
        Public PageSize As Integer
        Public IsGridVisible As Boolean = True
        Public NumberOfRecords As Integer
        Public LoadDataFromIndixID As Boolean = True
        Public SortType As String
        Public TotalNumberOfRecordsAvailable As Int32 = 0
        Public DeviceModelName As String
        Public PurchasePrice As DecimalType
    End Class

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub InitializeState()
        Me.State.NumberOfRecords = INDIX_DEFAULT_NUMBER_OF_RECORDS
        Me.State.PageIndex = 0

        Me.State.PageSize = INDIX_DEFAULT_PAGE_SIZE

    End Sub
#End Region


    Public Sub New()
        MyBase.New(New MyState)
    End Sub

#Region "Page Events"
    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try

            If Not Me.CallingParameters Is Nothing Then
                Dim parameters As ArrayList = CType(Me.CallingParameters, ArrayList)

                Me.State.ClaimID = CType(parameters(0), Guid)

                Me.State.DeviceModelName = parameters(1).ToString

                Me.State.CompanyID = CType(parameters(2), Guid)

                Me.State.PurchasePrice = CType(parameters(3), DecimalType)

                Me.State.IndixID = parameters(4).ToString()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Form.DefaultButton = btnSearch.UniqueID

        If Not IsPostBack Then

            InitializeState()
            Me.State.SortType = SortType.Ascending.ToString
            Me.State.LoadDataFromIndixID = True
            Me.State.IndixCountryCode = GetIndixCountryCode()

            Me.State.SearchTerm = Me.State.DeviceModelName
            Me.State.SortExpression = SortBy.MK.ToString()

            Me.SetFormTab(CLAIM_TAB)
            Me.SetFormTitle(PAGETITLE)

            InitializeControls()
        End If

        Me.DisplayNewProgressBarOnClick(Me.btnSearch, "LOADING_DEVICE")
        Me.DisplayNewProgressBarOnClick(Me.btnMore, "LOADING_DEVICE")
    End Sub

#End Region


    Private Sub InitializeControls()

        Me.MenuEnabled = False

        Me.MasterErrController.Clear()
        TextBoxSearchTerm.MaxLength = MAX_SEARCH_TERM_LENGTH

        btnResetSearch.Enabled = Not String.IsNullOrEmpty(Me.State.IndixID)
        ControlMgr.SetVisibleControl(Me, btnResetSearch, True)

        Me.SetGridItemStyleColor(Me.Grid)

        TranslateGridHeader(Me.Grid)

        BindSortByDropDown()

        BindGrid()
    End Sub

    Private Sub ResetSearch()
        InitializeState()

        Me.State.LoadDataFromIndixID = True
        Me.State.SearchTerm = Me.State.DeviceModelName
        Me.State.SortExpression = SortBy.MK.ToString()
        Me.State.SortType = SortType.Ascending.ToString

        cboSortBy.SelectedIndex = 0
        TextBoxSearchTerm.Text = String.Empty

    End Sub
    Private Sub SetDataSummaryLabels()
        LabelModelName.Text = TranslationBase.TranslateLabelOrMessage(Me.SEARCH_RESULTS_FOR) & ": " & Me.State.SearchTerm

        Me.lblRecordCount.Text = Me.State.NumberOfRecords & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
    End Sub

    Private Sub ClearSearch()
        Me.TextBoxSearchTerm.Text = String.Empty
        Me.cboSortBy.SelectedIndex = 0
    End Sub

#Region "Controls' Binding"
    Private Sub BindSortByDropDown()
        Dim oSortDv As DataView
        Try
            Dim ocboSortBy As ListItem() = CommonConfigManager.Current.ListManager.GetList("RETAIL_PRICE_SEARCH_SORTBY_LIST", Thread.CurrentPrincipal.GetLanguageCode())
            cboSortBy.Populate(ocboSortBy, New PopulateOptions() With
                                          {
                                            .AddBlankItem = False
                                           })
            Dim defaultSelectedCodeId As Guid = GetSortByGuid_FromValue(Codes.DEFAULT_SORT_FOR_RETAIL_PRICE)

            Me.SetSelectedItem(Me.cboSortBy, defaultSelectedCodeId)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            Dim products() As ProductDetail = GetIndixProducts().ToArray()

            If (Not products Is Nothing And products.Length > 0) Then

                If (Me.State.PageIndex = 0) Then
                    Grid.PageIndex = 0
                End If

                Grid.DataSource = products.ToArray()
                Grid.PageSize = Me.State.PageSize

                Dim sort As String = If(Me.State.SortType = SortType.Ascending.ToString, " ASC", " DESC")
                HighLightSortColumn(Me.Grid, Me.State.SortExpression & sort, True)

                Grid.DataBind()

                Me.State.NumberOfRecords = products.Length

                If Grid.PageCount < Me.State.PageIndex Then
                    Me.State.PageIndex = (Grid.PageCount - 1)
                End If

                Me.MasterPage.MessageController.Clear()

                SetMoreButtonEnabled()

            Else
                Me.State.NumberOfRecords = 0

                If (Not String.IsNullOrEmpty(Me.State.SearchTerm)) Then
                    ShowMessage(Message.MSG_NO_RECORDS_FOUND, IMessageController.MessageType.Information)
                End If
            End If

        Catch ex As ServiceException
            Me.State.NumberOfRecords = 0
            Dim failError As String = "Indix: " & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.ERR_WEB_SERVICE_CALL_FAILED)
            ShowMessage(failError, IMessageController.MessageType.Error, False)
        Catch ex As Exception
            Me.State.NumberOfRecords = 0
            MasterPage.MessageController.Clear()

            If (ex.Message.Equals(Me.NO_PRODUCT_FOUND_FOR_INDIX_ID)) Then
                ShowMessage(Me.NO_PRODUCT_FOUND_FOR_INDIX_ID, IMessageController.MessageType.Error)
            Else
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End If

        End Try

        SetDataSectionVisibility(Me.State.NumberOfRecords > 0)
        SetDataSummaryLabels()
    End Sub

#End Region

#Region "Setting Controls"
    Private Sub SetMoreButtonEnabled()
        btnMore.Enabled = (Me.State.NumberOfRecords < INDIX_MAX_NUMBER_OF_RECORDS And Me.State.NumberOfRecords < Me.State.TotalNumberOfRecordsAvailable)
    End Sub
    Private Sub SetDataSectionVisibility(visible As Boolean)
        ControlMgr.SetVisibleControl(Me, Me.dataSectionContainer, visible)
    End Sub
#End Region


    Private Function GetIndixProducts() As IEnumerable(Of ProductDetail)
        Dim output As List(Of ProductDetail) = New List(Of ProductDetail)
        Dim manager As IndixServiceManager = New IndixServiceManager()

        If (Me.State.LoadDataFromIndixID = True) Then
            If (Not String.IsNullOrEmpty(Me.State.IndixID)) Then
                Dim indivRequest As ProductDetailsRequest = New ProductDetailsRequest
                indivRequest.countryCode = Me.State.IndixCountryCode
                indivRequest.mpId = Me.State.IndixID

                Me.State.SearchTerm = Me.State.DeviceModelName

                Dim response As ProductDetailsResponse = manager.GetProductDetails(indivRequest)

                If (Not response Is Nothing AndAlso Not response.Product Is Nothing AndAlso Not String.IsNullOrEmpty(response.Product.CategoryId)) Then
                    output.Add(response.Product)

                    Me.State.IndixCategoryID = response.Product.CategoryId
                Else
                    Throw New Exception(Me.NO_PRODUCT_FOUND_FOR_INDIX_ID)
                End If
            Else
                Me.State.SearchTerm = String.Empty
                ShowMessage(DEVICE_NOT_ASSOCIATED_TO_INDIX_PRODUCT, IMessageController.MessageType.Warning)
            End If
        End If

        If (Not String.IsNullOrEmpty(State.SearchTerm)) Then

            Dim startPrice As String = If(Me.State.PurchasePrice.Value = 0, String.Empty, (Me.State.PurchasePrice.Value / 2).ToString)
            Dim endPrice As String = If(Me.State.PurchasePrice.Value = 0, String.Empty, (Me.State.PurchasePrice.Value).ToString)

            Dim category As List(Of Integer) = If(String.IsNullOrEmpty(Me.State.IndixCategoryID), Nothing, New List(Of Integer)(New Integer() {Integer.Parse(Me.State.IndixCategoryID)}))

            Dim sortBy = DirectCast([Enum].Parse(GetType(SortBy), Me.State.SortExpression), SortBy)

            Dim sortType = DirectCast([Enum].Parse(GetType(SortType), Me.State.SortType), SortType)

            If (Me.State.NumberOfRecords < INDIX_DEFAULT_NUMBER_OF_RECORDS) Then
                Me.State.NumberOfRecords = INDIX_DEFAULT_NUMBER_OF_RECORDS
            End If

            Dim r As ProductSearchRequest = New ProductSearchRequest(State.IndixCountryCode,
                                                                     State.SearchTerm,
                                                                     category,
                                                                     startPrice,
                                                                     endPrice,
                                                                     sortBy,
                                                                     sortType,
                                                                     Me.State.NumberOfRecords)

            Dim remainingItems As IEnumerable(Of ProductDetail) = manager.GetProducts(r, Me.State.TotalNumberOfRecordsAvailable)

            If (output.Count > 0 And Not remainingItems Is Nothing And remainingItems.Count > 0) Then
                'Delete Item with Me.State.IndixID is downloaded
                remainingItems = PrepareList(remainingItems)
            End If

            output.AddRange(remainingItems)

        End If

        Return output
    End Function

    Private Function PrepareList(list As IEnumerable(Of ProductDetail)) As IEnumerable(Of ProductDetail)
        If (Not list Is Nothing) Then
            Dim p As ProductDetail = list.FirstOrDefault(Function(i) i.Mpid.Equals(Me.State.IndixID, StringComparison.InvariantCulture))

            p = If(p Is Nothing, list.Last(), p)

            Return list.Where(Function(itm) Not itm.Mpid.Equals(p.Mpid, StringComparison.InvariantCulture))
        Else
            Return Nothing

        End If

    End Function

#Region "Reading from DB"
    Private Function GetIndixCountryCode() As String
        Dim countryCode As String = String.Empty

        Try
            'Call SQL to bring Indix CountryCode  
            countryCode = BusinessObjectsNew.Claim.GetCountryCodeOverwrite(Me.State.CompanyID)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

        Return countryCode
    End Function

#End Region

#Region "Controls' events"
    Protected Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged

        Me.State.PageSize = Int32.Parse(CType(sender, DropDownList).SelectedValue)

        If (dataSectionContainer.Visible) Then
            BindGrid()
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Me.SetSelectedItem(cboPageSize, INDIX_DEFAULT_PAGE_SIZE.ToString)
        Dim sortBy_Guid = GetSortByValue_FromGuid(New Guid(cboSortBy.SelectedValue))
        Me.State.LoadDataFromIndixID = False

        If (Not sortBy_Guid.Equals(Me.State.SortExpression)) Then
            Me.State.SortExpression = sortBy_Guid
        End If

        If (String.IsNullOrEmpty(TextBoxSearchTerm.Text.Trim())) Then
            Me.State.SearchTerm = String.Empty

            ClearGrid()
            SetDataSectionVisibility(False)

            ShowMessage(GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, IMessageController.MessageType.Error)
        Else
            InitializeState()
            Me.State.SortType = If(Me.State.SortExpression = SortBy.MXP.ToString(), SortType.Descending, SortType.Ascending).ToString

            Me.State.SearchTerm = TextBoxSearchTerm.Text.Trim()

            BindGrid()
        End If
    End Sub

    Private Sub ClearGrid()
        Me.State.NumberOfRecords = 0
        SetDataSummaryLabels()

        Grid.DataSource = Nothing
        Grid.DataBind()
    End Sub
    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click

        Try
            Me.ClearSearch()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnMore_Click(sender As Object, e As EventArgs) Handles btnMore.Click
        Me.State.NumberOfRecords += INDIX_MORE_RECORDS_INCREMENT
        BindGrid()
    End Sub

    Protected Sub btnResetSearch_Click(sender As Object, e As EventArgs) Handles btnResetSearch.Click
        ResetSearch()

        Me.SetSelectedItem(cboPageSize, INDIX_DEFAULT_PAGE_SIZE.ToString)

        BindGrid()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Dim retType As New ClaimForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back)
            Me.ReturnToCallingPage(retType)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboPageSize_Init(sender As Object, e As EventArgs) Handles cboPageSize.Init

        Try
            If (Not cboPageSize.Items.FindByValue(INDIX_DEFAULT_PAGE_SIZE.ToString) Is Nothing) Then
                Me.SetSelectedItem(cboPageSize, INDIX_DEFAULT_PAGE_SIZE.ToString)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try


    End Sub
#End Region


#Region "Grid Related"

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub Grid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Me.State.PageIndex = e.NewPageIndex
    End Sub

    Protected Sub Grid_PageIndexChanged(sender As Object, e As EventArgs) Handles Grid.PageIndexChanged
        CType(sender, GridView).PageIndex = Me.State.PageIndex
        BindGrid()
    End Sub
    Protected Sub Grid_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
        If (Not Me.State.SortExpression.Equals(e.SortExpression)) Then
            Me.State.SortExpression = e.SortExpression
            Me.State.SortType = SortType.Ascending.ToString
        Else
            Me.State.SortType = If(Me.State.SortType.Equals(SortType.Ascending.ToString), SortType.Descending.ToString, SortType.Ascending.ToString)
        End If

        BindGrid()
    End Sub


    'The Binding LOgic is here
    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        'Dim itemType As ListItemType = CType(e.Row.ItemType, ListItemType)
        Dim product As ProductDetail = CType(e.Row.DataItem, ProductDetail)
        Dim btnEditClaimItem As LinkButton

        Try
            If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_MAKE_IDX), product.BrandName)
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_MODEL_IDX), product.Title)
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_MIN_SALE_PRICE_IDX), GetFormattedPrice(product.MinSalePrice))
                Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_MAX_SALE_PRICE_IDX), GetFormattedPrice(product.MaxSalePrice))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region


    Private Function GetFormattedPrice(price As String) As Decimal
        Return Decimal.Parse(price)
    End Function



    Protected Sub Back(ByVal cmd As ElitaPlusPage.DetailPageCommand)
        Me.NavController = Nothing
        Me.ReturnToCallingPage(True)
    End Sub

    Private Sub ShowMessage(msg As String, msgType As IMessageController.MessageType, Optional translate As Boolean = True)
        Me.MasterPage.MessageController.Clear()
        If (msgType = IMessageController.MessageType.Error) Then
            Me.MasterPage.MessageController.AddErrorAndShow(msg, translate)
        Else
            Me.MasterPage.MessageController.AddMessage(msg, translate, msgType)
        End If

    End Sub

    Private Function GetSortByGuid_FromValue(value As String) As Guid
        Return LookupListNew.GetIdFromCode(LookupListNew.LK_RETAIL_PRICE_SEARCH_SORTBY_LIST, value)
    End Function

    Private Function GetSortByValue_FromGuid(id As Guid) As String
        Return LookupListNew.GetCodeFromId(LookupListNew.LK_RETAIL_PRICE_SEARCH_SORTBY_LIST, id)
    End Function

End Class
