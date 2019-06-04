Option Strict On
Option Explicit On
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables
    Public Class ClaimFulfillmentOrderListForm
        Inherits ElitaPlusSearchPage
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

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
        Public Const GRID_COL_CODE_IDX As Integer = 1
        Public Const GRID_COL_DESCRIPTION_IDX As Integer = 2
        Public Const GRID_COL_PRICE_LIST_SOURCE_IDX As Integer = 3
        Public Const GRID_COL_COUNTRY_IDX As Integer = 4
        Public Const GRID_COL_PRICE_LIST_CODE_IDX As Integer = 5
        Public Const GRID_COL_EQUIPMENT_TYPE_IDX As Integer = 6
        Public Const GRID_COL_CF_ORDER_DETIAL_ID_IDX As Integer = 7

        Public Const GRID_TOTAL_COLUMNS As Integer = 7
        Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
        Private Const CLAIMFULFILLMENTORDERLISTLISTFORM As String = "ClaimFulfillmentOrderListForm.aspx"
        Public Const LABEL_SELECT_DEALERCODE As String = "DEALER"

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        ' This class keeps the current state for the search page.
        Class MyState
            Public PageIndex As Integer = 0
            Public CFOrderDetailId As Guid = Guid.Empty
            Public IsGridVisible As Boolean
            Public SearchDV As ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV = Nothing
            Public SelectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public SortExpression As String = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_CODE
            Public HasDataChanged As Boolean
            Public bNoRow As Boolean = False

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
#End Region

#Region "Page_Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.MasterPage.MessageController.Clear_Hide()

            Try
                If Not Me.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    Me.TranslateGridHeader(Grid)
                    UpdateBreadCrum()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SortDirection = Me.State.SortExpression
                    PopulateDropdown()

                    If Me.State.IsGridVisible Then
                        If Not (Me.State.SelectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.SelectedPageSize = Grid.PageSize) Then
                            Grid.PageSize = Me.State.SelectedPageSize
                        End If
                        cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                        Me.PopulateGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As ClaimFulfillmentOrderDetailForm.ReturnType = CType(ReturnPar, ClaimFulfillmentOrderDetailForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            Me.State.CFOrderDetailId = retObj.moCFOrderDetailleId
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                        Me.State.CFOrderDetailId = Guid.Empty
                    Case Else
                        Me.State.CFOrderDetailId = Guid.Empty
                End Select
                Grid.PageIndex = Me.State.PageIndex
                cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                Grid.PageSize = Me.State.SelectedPageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()
            If ((Me.State.SearchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                Me.State.SearchDV = ClaimFulfillmentOrderDetail.GetList(Me.CodeText.Text.ToUpper, Me.DescriptionText.Text.ToUpper, Me.PriceListSourceDropDown.SelectedValue)
            End If

            If (Me.State.SearchDV.Count = 0) Then
                Me.State.bNoRow = True
                CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
            Else
                Me.State.bNoRow = False
                Me.Grid.Enabled = True
            End If

            Me.State.SearchDV.Sort = Me.State.SortExpression
            Grid.AutoGenerateColumns = False

            Grid.Columns(GRID_COL_CODE_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_CODE
            Grid.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_DESCRIPTION
            Grid.Columns(GRID_COL_PRICE_LIST_SOURCE_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_PRICE_LIST_SOURCE
            Grid.Columns(GRID_COL_COUNTRY_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_COUNTRY
            Grid.Columns(GRID_COL_PRICE_LIST_CODE_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_PRICE_LIST_CODE
            Grid.Columns(GRID_COL_EQUIPMENT_TYPE_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_EQUIPMENT_TYPE
            Grid.Columns(GRID_COL_CF_ORDER_DETIAL_ID_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_CF_ORDER_DETAIL_ID

            HighLightSortColumn(Grid, Me.State.SortExpression)

            SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.CFOrderDetailId, Me.Grid, Me.State.PageIndex)

            Me.Grid.DataSource = Me.State.SearchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = Me.State.SearchDV.Count

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End Sub

        Private Sub PopulateDropdown()
            Dim pricelistsource As ListItem() = CommonConfigManager.Current.ListManager.GetList("PRICE_LIST_SOURCE", Thread.CurrentPrincipal.GetLanguageCode())
            pricelistsource.OrderBy("Translation", LinqExtentions.SortDirection.Descending)

            PriceListSourceDropDown.Items.Clear()
            Dim itm As New WebControls.ListItem
            itm.Text = String.Empty
            itm.Value = String.Empty
            PriceListSourceDropDown.Items.Add(itm)
            For Each li As ListItem In pricelistsource
                itm = New WebControls.ListItem
                itm.Text = li.Translation
                itm.Value = li.ExtendedCode
                PriceListSourceDropDown.Items.Add(itm)
            Next
        End Sub

        Private Sub SortAndBindGrid()
            If (Me.State.SearchDV.Count = 0) Then
                Me.State.bNoRow = True
                CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Grid.PagerSettings.Visible = True
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            Else
                Me.State.bNoRow = False
                Me.Grid.Enabled = True
                Me.Grid.DataSource = Me.State.SearchDV
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()
                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If

            If Me.State.SearchDV.Count > 0 Then
                Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                Me.lblRecordCount.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("CLAIM_FULFILLMENT_ORDER_DETAIL")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_FULFILLMENT_ORDER_DETAIL")
                End If
            End If
        End Sub

        Private Sub ClearSearchCriteria()
            Try
                Me.PriceListSourceDropDown.SelectedIndex = BLANK_ITEM_SELECTED
                Me.CodeText.Text = String.Empty
                Me.DescriptionText.Text = String.Empty
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "GridView Related"
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Not Me.State.bNoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CF_ORDER_DETIAL_ID_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_CF_ORDER_DETAIL_ID))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CODE_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_CODE))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DESCRIPTION_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_DESCRIPTION))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRICE_LIST_SOURCE_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_PRICE_LIST_SOURCE))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_COUNTRY_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_COUNTRY))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRICE_LIST_CODE_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_PRICE_LIST_CODE))
                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_EQUIPMENT_TYPE_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_EQUIPMENT_TYPE))
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(ByVal source As Object, ByVal e As GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")

                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If
                Me.State.SortExpression = Me.SortDirection
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Me.State.CFOrderDetailId = New Guid(Me.Grid.Rows(index).Cells(GRID_COL_CF_ORDER_DETIAL_ID_IDX).Text)
                    Me.callPage(ClaimFulfillmentOrderDetailForm.URL, Me.State.CFOrderDetailId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Button Clicks"
        Private Sub moBtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
            Try
                Me.State.PageIndex = 0
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                        Grid.PageSize = State.SelectedPageSize
                    End If
                    Me.State.IsGridVisible = True
                End If
                Me.State.SearchDV = Nothing
                Me.State.HasDataChanged = False
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnClearSearch.Click
            ClearSearchCriteria()
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.State.CFOrderDetailId = Guid.Empty
                Me.callPage(ClaimFulfillmentOrderDetailForm.URL, Me.State.CFOrderDetailId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Helper Functions"

        Private Sub PopulateDropdown(ByVal DealerList As DropDownList)
            Try
                'Me.BindListControlToDataView(DealerList, CType(Dealer.getList(Guid.Empty, Guid.Empty, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), DataView), "dealer", "dealer_id", , True)
                Dim oDealerList As ListItem() = GetDealerListByCompanyForUser()
                'Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                '                                                                   Return li.Translation + " " + "(" + li.Code + ")"
                '                                                               End Function
                DealerList.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .SortFunc = AddressOf .GetDescription
                                                   })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            'For Index = 0 To UserCompanies.Count - 1
            For Each Ele As Guid In UserCompanies
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = Ele
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If Not oDealerList Is Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = CType(oDealerListForCompany.Clone(),
                                            Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem))
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function

#End Region

    End Class
End Namespace
