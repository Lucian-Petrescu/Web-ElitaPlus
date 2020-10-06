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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            MasterPage.MessageController.Clear_Hide()

            Try
                If Not IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    TranslateGridHeader(Grid)
                    UpdateBreadCrum()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SortDirection = State.SortExpression
                    PopulateDropdown()

                    If State.IsGridVisible Then
                        If Not (State.SelectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.SelectedPageSize = Grid.PageSize) Then
                            Grid.PageSize = State.SelectedPageSize
                        End If
                        cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As ClaimFulfillmentOrderDetailForm.ReturnType = CType(ReturnPar, ClaimFulfillmentOrderDetailForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            State.CFOrderDetailId = retObj.moCFOrderDetailleId
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                        State.CFOrderDetailId = Guid.Empty
                    Case Else
                        State.CFOrderDetailId = Guid.Empty
                End Select
                Grid.PageIndex = State.PageIndex
                cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                Grid.PageSize = State.SelectedPageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()
            If ((State.SearchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                State.SearchDV = ClaimFulfillmentOrderDetail.GetList(CodeText.Text.ToUpper, DescriptionText.Text.ToUpper, PriceListSourceDropDown.SelectedValue)
            End If

            If (State.SearchDV.Count = 0) Then
                State.bNoRow = True
                CreateHeaderForEmptyGrid(Grid, SortDirection)
            Else
                State.bNoRow = False
                Grid.Enabled = True
            End If

            State.SearchDV.Sort = State.SortExpression
            Grid.AutoGenerateColumns = False

            Grid.Columns(GRID_COL_CODE_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_CODE
            Grid.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_DESCRIPTION
            Grid.Columns(GRID_COL_PRICE_LIST_SOURCE_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_PRICE_LIST_SOURCE
            Grid.Columns(GRID_COL_COUNTRY_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_COUNTRY
            Grid.Columns(GRID_COL_PRICE_LIST_CODE_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_PRICE_LIST_CODE
            Grid.Columns(GRID_COL_EQUIPMENT_TYPE_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_EQUIPMENT_TYPE
            Grid.Columns(GRID_COL_CF_ORDER_DETIAL_ID_IDX).SortExpression = ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_CF_ORDER_DETAIL_ID

            HighLightSortColumn(Grid, State.SortExpression)

            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.CFOrderDetailId, Grid, State.PageIndex)

            Grid.DataSource = State.SearchDV
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.SearchDV.Count

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
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
            If (State.SearchDV.Count = 0) Then
                State.bNoRow = True
                CreateHeaderForEmptyGrid(Grid, SortDirection)
                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Grid.PagerSettings.Visible = True
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            Else
                State.bNoRow = False
                Grid.Enabled = True
                Grid.DataSource = State.SearchDV
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If

            If State.SearchDV.Count > 0 Then
                lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                lblRecordCount.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("CLAIM_FULFILLMENT_ORDER_DETAIL")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_FULFILLMENT_ORDER_DETAIL")
                End If
            End If
        End Sub

        Private Sub ClearSearchCriteria()
            Try
                PriceListSourceDropDown.SelectedIndex = BLANK_ITEM_SELECTED
                CodeText.Text = String.Empty
                DescriptionText.Text = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "GridView Related"
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing And Not State.bNoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CF_ORDER_DETIAL_ID_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_CF_ORDER_DETAIL_ID))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CODE_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_CODE))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DESCRIPTION_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_DESCRIPTION))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRICE_LIST_SOURCE_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_PRICE_LIST_SOURCE))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_COUNTRY_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_COUNTRY))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PRICE_LIST_CODE_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_PRICE_LIST_CODE))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_EQUIPMENT_TYPE_IDX), dvRow(ClaimFulfillmentOrderDetail.CFOrderDetailSearchhDV.COL_NAME_EQUIPMENT_TYPE))
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(source As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")

                If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If SortDirection.EndsWith(" ASC") Then
                        SortDirection = e.SortExpression + " DESC"
                    Else
                        SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If
                State.SortExpression = SortDirection
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(source As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    State.CFOrderDetailId = New Guid(Grid.Rows(index).Cells(GRID_COL_CF_ORDER_DETIAL_ID_IDX).Text)
                    callPage(ClaimFulfillmentOrderDetailForm.URL, State.CFOrderDetailId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Button Clicks"
        Private Sub moBtnSearch_Click(sender As Object, e As System.EventArgs) Handles moBtnSearch.Click
            Try
                State.PageIndex = 0
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                        Grid.PageSize = State.SelectedPageSize
                    End If
                    State.IsGridVisible = True
                End If
                State.SearchDV = Nothing
                State.HasDataChanged = False
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClearSearch_Click(sender As Object, e As System.EventArgs) Handles moBtnClearSearch.Click
            ClearSearchCriteria()
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                State.CFOrderDetailId = Guid.Empty
                callPage(ClaimFulfillmentOrderDetailForm.URL, State.CFOrderDetailId)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Helper Functions"

        Private Sub PopulateDropdown(DealerList As DropDownList)
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
                HandleErrors(ex, MasterPage.MessageController)
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
                    If oDealerList IsNot Nothing Then
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
