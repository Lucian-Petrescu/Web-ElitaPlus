Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Class ItemSearchForm
        Inherits ElitaPlusSearchPage

#Region "Page State"
        '  Private IsReturningFromChild As Boolean = False

        ' This class keeps the current state for the search page.
        Class MyState
            Public IsGridVisible As Boolean = False
            Public searchDV As DataView = Nothing
            Public searchBtnClicked As Boolean = False
            Public SortExpression As String = Item.COL_DEALER_NAME
            Private moItemData As Item
            Private mnPageIndex As Integer = 0
            Private msPageSort As String
            Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
            Private moSearchDataView As DataView
            Public moItemId As Guid = Guid.Empty
            Public ProductCodeId As Guid = Guid.Empty
            Public DealerId As Guid = Guid.Empty
            Public RiskTypeId As Guid = Guid.Empty
            Public bnoRow As Boolean = False

            ' these variables are used to store the sorting columns information.
            Public SortColumns(GRID_TOTAL_COLUMNS - 1) As String
            Public IsSortDesc(GRID_TOTAL_COLUMNS - 1) As Boolean

#Region "State-Properties"


            'Public Property ProductCodeId() As Guid
            '    Get
            '        Return moItemData.ProductCodeId
            '    End Get
            '    Set(ByVal Value As Guid)
            '        moItemData.ProductCodeId = Value
            '    End Set
            'End Property

            'Public Property DealerId() As Guid
            '    Get
            '        Return moItemData.DealerID
            '    End Get
            '    Set(ByVal Value As Guid)
            '        moItemData.DealerID = Value
            '    End Set
            'End Property

            'Public Property RiskTypeId() As Guid
            '    Get
            '        Return moItemData.RiskTypeId
            '    End Get
            '    Set(ByVal Value As Guid)
            '        moItemData.RiskTypeId = Value
            '    End Set
            'End Property

            Public Property PageIndex() As Integer
                Get
                    Return mnPageIndex
                End Get
                Set(Value As Integer)
                    mnPageIndex = Value
                End Set
            End Property

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

            Public Property SearchDataView() As DataView
                Get
                    Return moSearchDataView
                End Get
                Set(Value As DataView)
                    moSearchDataView = Value
                End Set
            End Property

#End Region

            Sub New()
                SortColumns(GRID_COL_DEALER_NAME) = Item.COL_DEALER_NAME
                SortColumns(GRID_COL_PRODUCT_CODE) = Item.COL_PRODUCT_CODE
                SortColumns(GRID_COL_ITEM_NUMBER) = Item.COL_ITEM_NUMBER
                SortColumns(GRID_COL_RISK_TYPE) = Item.COL_RISK_TYPE

                IsSortDesc(GRID_COL_DEALER_NAME) = False
                IsSortDesc(GRID_COL_PRODUCT_CODE) = False
                IsSortDesc(GRID_COL_ITEM_NUMBER) = False
                IsSortDesc(GRID_COL_RISK_TYPE) = False
                '  moItemData = New Item
            End Sub

            ' this will be called before the populate list to get the correct sort order
            Public ReadOnly Property CurrentSortExpresion1() As String
                Get
                    Dim s As String
                    Dim i As Integer
                    Dim sortExp As String = ""
                    For i = 0 To SortColumns.Length - 1
                        If SortColumns(i) IsNot Nothing Then
                            sortExp &= SortColumns(i)
                            If IsSortDesc(i) Then sortExp &= " DESC"
                            sortExp &= ","
                        End If
                    Next
                    Return sortExp.Substring(0, sortExp.Length - 1) 'to remove the last comma
                End Get
            End Property

            Public Sub ToggleSort1(gridColIndex As Integer)
                IsSortDesc(gridColIndex) = Not IsSortDesc(gridColIndex)
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


#Region "Page Return"
        Private IsReturningFromChild As Boolean = False

        Public Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                IsReturningFromChild = True
                If State.searchDV Is Nothing Then
                    State.IsGridVisible = False
                Else
                    State.IsGridVisible = True
                End If
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                If retObj IsNot Nothing Then

                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.moItemId = retObj.moItemId
                        Case Else
                            State.moItemId = Guid.Empty
                    End Select
                    moItemGrid.PageIndex = State.PageIndex
                    moItemGrid.PageSize = State.PageSize
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    moItemGrid.PageSize = State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, moItemGrid.Visible)
                    PopulateDealer()
                    PopulateRiskType()
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moItemId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oItemId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moItemId = oItemId
                Me.BoChanged = boChanged
            End Sub


        End Class

#End Region

#End Region

#Region "Constants"

        Private Const ITEM_LIST_FORM001 As String = "ITEM_LIST_FORM001" ' Maintain Item List Exception
        Private Const ITEM_LIST_FORM002 As String = "ITEM_LIST_FORM002" ' Maintain Item Search Exception
        Private Const ITEM_DETAIL_PAGE As String = "ItemForm.aspx"
        Private Const ITEM_STATE As String = "ItemState"

        Public Const GRID_TOTAL_COLUMNS As Integer = 6
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_ITEM_IDX As Integer = 1
        Public Const GRID_COL_DEALER_NAME As Integer = 2
        Public Const GRID_COL_PRODUCT_CODE As Integer = 3
        Public Const GRID_COL_ITEM_NUMBER As Integer = 4
        Public Const GRID_COL_RISK_TYPE As Integer = 5
        Public Const LABEL_SELECT_DEALERCODE As String = "Dealer"
#End Region

#Region "Properties"
        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property
#End Region

#Region "Handlers"

#Region "Handlers-Init"

        Protected WithEvents moErrorController As ErrorController
        Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl

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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                moErrorController.Clear_Hide()
                If Not Page.IsPostBack Then
                    SortDirection = State.SortExpression
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)

                    '   Me.MenuEnabled = True
                    SetGridItemStyleColor(moItemGrid)
                    TranslateGridHeader(moItemGrid)
                    If Not IsReturningFromChild Then
                        ' It is The First Time
                        ' It is not Returning from Detail
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        PopulateDealer()
                        PopulateRiskType()
                        PopulateProductCode()
                    Else
                        ' It is returning from detail
                        PopulateProductCode()
                        PopulateGrid(POPULATE_ACTION_SAVE)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
            ShowMissingTranslations(moErrorController)
        End Sub

#End Region

#Region "Handlers-DropDown"

        'Private Sub moDealerDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moDealerDrop.SelectedIndexChanged
        '    Try
        '        ClearForDealer()
        '        If moDealerDrop.SelectedIndex > 0 Then
        '            PopulateProductCode()
        '        End If
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, moErrorController)
        '    End Try
        'End Sub

        Private Sub moProductCodeDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moProductCodeDrop.SelectedIndexChanged
            Try
                ClearForProduct()
                If moProductCodeDrop.SelectedIndex > 0 Then
                    PopulateRiskType()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub moBtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSearch.Click
            Try
                moItemGrid.PageIndex = NO_PAGE_INDEX
                moItemGrid.DataMember = Nothing
                State.searchDV = Nothing
                State.searchBtnClicked = True
                PopulateGrid()
                State.searchBtnClicked = False
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(sender As System.Object, e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                'Me.State.moItemId = Guid.Empty
                SetSession()
                callPage(ItemForm.URL)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#Region "Handlers-Grid"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub moItemGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moItemGrid.PageIndexChanging
            Try
                moItemGrid.PageIndex = e.NewPageIndex
                PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moItemGrid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moItemGrid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moItemGrid_ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles moItemGrid.RowCommand
            Dim sItemId As String
            Try

                If e.CommandSource.GetType.Equals(GetType(ImageButton)) Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    'this only runs when they click the pencil button for editing.
                    'New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_ITEM_IDX).Text)
                    'sItemId = CType(e.row.FindControl("moItemId"), Label).Text
                    State.moItemId = New Guid(moItemGrid.Rows(index).Cells(GRID_COL_ITEM_IDX).Text)
                    SetSession()
                    callPage(ItemForm.URL, State.moItemId)
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub moItemGrid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moItemGrid.Sorting
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
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#End Region

#Region "Populate"

        Private Sub PopulateDealer()
            Try
                Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                TheDealerControl.SetControl(True,
                                            TheDealerControl.MODES.NEW_MODE,
                                            True,
                                            oDealerview,
                                            TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE),
                                            True, True,
                                            ,
                                            "multipleDropControl_moMultipleColumnDrop",
                                            "multipleDropControl_moMultipleColumnDropDesc",
                                            "multipleDropControl_lb_DropDown",
                                            False,
                                            0)
                TheDealerControl.SelectedGuid = State.DealerId


            Catch ex As Exception
                moErrorController.AddError(ITEM_LIST_FORM002)
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub PopulateProductCode()
            Try
                Dim oDealerId As Guid = TheDealerControl.SelectedGuid 'Me.GetSelectedItem(moDealerDrop)

                'Me.BindListControlToDataView(moProductCodeDrop, LookupListNew.GetProductCodeLookupList(oDealerId), "CODE") 'same as itemform
                Dim listcontext As ListContext = New ListContext()
                listcontext.DealerId = oDealerId
                Dim prodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ProductCodeByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moProductCodeDrop.Populate(prodLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode,
                .SortFunc = AddressOf .GetCode
                 })
                BindSelectItem(State.ProductCodeId.ToString, moProductCodeDrop)
                moProductCodeDrop.Enabled = True
            Catch ex As Exception
                moErrorController.AddError(ITEM_LIST_FORM002)
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub PopulateRiskType()
            Try
                Dim oProductId As Guid = GetSelectedItem(moProductCodeDrop)

                '  Me.BindListControlToDataView(moItemRiskTypeDrop, LookupListNew.GetRiskProductCodeLookupList(oProductId)) 'need to implement
                Dim listcontext As ListContext = New ListContext()
                listcontext.ProductCodeId = oProductId
                Dim riskLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.RiskTypeByProduct, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moItemRiskTypeDrop.Populate(riskLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True
                 })
                BindSelectItem(State.RiskTypeId.ToString, moItemRiskTypeDrop)
                moItemRiskTypeDrop.Enabled = True
            Catch ex As Exception
                moErrorController.AddError(ITEM_LIST_FORM002)
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub BindDataGrid(oDataView As DataView)
            'oDataView.Sort = moItemGrid.DataMember()
            'moItemGrid.DataSource = oDataView
            'moItemGrid.DataBind()
            State.PageIndex = moItemGrid.PageIndex

            If (oDataView.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(moItemGrid, SortDirection)
            Else
                State.bnoRow = False
                moItemGrid.Enabled = True
                moItemGrid.DataSource = State.searchDV
                HighLightSortColumn(moItemGrid, SortDirection)
                moItemGrid.DataBind()
            End If

            If Not moItemGrid.BottomPagerRow.Visible Then moItemGrid.BottomPagerRow.Visible = True

        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                moItemGrid.PageIndex = NewCurrentPageIndex(moItemGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (State.searchDV Is Nothing) Then
                    'oDataView = GetDataView()
                    State.searchDV = GetDataView()
                    'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                    'If Me.State.searchBtnClicked Then
                    'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    ' End If
                End If

                State.searchDV.Sort = State.SortExpression
                moItemGrid.AutoGenerateColumns = False
                'moItemGrid.Columns(Me.GRID_COL_DEALER_NAME).SortExpression = Item.COL_DEALER_NAME
                'moItemGrid.Columns(Me.GRID_COL_PRODUCT_CODE).SortExpression = Item.COL_PRODUCT_CODE
                'moItemGrid.Columns(Me.GRID_COL_RISK_TYPE).SortExpression = Item.COL_RISK_TYPE
                'moItemGrid.Columns(Me.GRID_COL_ITEM_NUMBER).SortExpression = Item.COL_ITEM_NUMBER
                HighLightSortColumn(moItemGrid, State.SortExpression)
                BasePopulateGrid(moItemGrid, State.searchDV, State.moItemId, oAction)

                ControlMgr.SetVisibleControl(Me, trPageSize, moItemGrid.Visible)

                Session("recCount") = State.searchDV.Count

                If moItemGrid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
                BindDataGrid(State.searchDV)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub EnableDropDowns(bIsEnable As Boolean)
            moProductCodeDrop.Enabled = bIsEnable
            moItemRiskTypeDrop.Enabled = bIsEnable
        End Sub

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
         Handles multipleDropControl.SelectedDropChanged
            Try
                State.DealerId = TheDealerControl.SelectedGuid
                PopulateDealer()
                ClearForDealer()
                If TheDealerControl.SelectedIndex > 0 Then
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearForGrid()
            moItemGrid.PageIndex = 0
            moItemGrid.DataSource = Nothing
            moItemGrid.DataBind()
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
        End Sub

        Private Sub ClearForProduct()
            ' ClearForGrid()
            ClearList(moItemRiskTypeDrop)
            moItemRiskTypeDrop.Enabled = False
        End Sub

        Private Sub ClearForDealer()
            ClearForProduct()
            ClearList(moProductCodeDrop)
            moProductCodeDrop.Enabled = False
        End Sub

        Private Sub ClearSearch()
            TheDealerControl.SelectedIndex = 0
            ClearForDealer()
            State.moItemId = Guid.Empty
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With State
                .DealerId = TheDealerControl.SelectedGuid '.GetSelectedItem(moDealerDrop)
                .RiskTypeId = GetSelectedItem(moItemRiskTypeDrop)
                .ProductCodeId = GetSelectedItem(moProductCodeDrop)
                .PageIndex = moItemGrid.PageIndex
                .PageSize = moItemGrid.PageSize
                .PageSort = State.SortExpression
                .SearchDataView = State.searchDV
            End With
        End Sub

#End Region

#Region "Business Part"

        Private Function GetDataView() As DataView
            Dim oItem As Item = New Item
            Dim oDataView As DataView
            Dim CompanyIdList As ArrayList

            With oItem
                CompanyIdList = ElitaPlusIdentity.Current.ActiveUser.Companies
                PopulateBOProperty(oItem, "DealerID", TheDealerControl.SelectedGuid)
                PopulateBOProperty(oItem, "ProductCodeId", moProductCodeDrop)
                PopulateBOProperty(oItem, "RiskTypeId", moItemRiskTypeDrop)
                oDataView = .GetList(.DealerID, .ProductCodeId, .RiskTypeId)
            End With
            Return oDataView
        End Function


#End Region


        Private Sub moItemGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moItemGrid.RowDataBound
            Try

                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If _
                    dvRow IsNot Nothing AndAlso Not State.bnoRow AndAlso
                    (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse
                     itemType = ListItemType.SelectedItem) Then
                    e.Row.Cells(GRID_COL_ITEM_IDX).Text =
                        GetGuidStringFromByteArray(CType(dvRow(Assurant.ElitaPlus.BusinessObjectsNew.Item.COL_ITEM_ID),
                                                         Byte()))
                    e.Row.Cells(GRID_COL_DEALER_NAME).Text =
                        dvRow(Assurant.ElitaPlus.BusinessObjectsNew.Item.COL_DEALER_NAME).ToString
                    e.Row.Cells(GRID_COL_PRODUCT_CODE).Text =
                        dvRow(Assurant.ElitaPlus.BusinessObjectsNew.Item.COL_PRODUCT_CODE).ToString
                    e.Row.Cells(GRID_COL_ITEM_NUMBER).Text =
                        dvRow(Assurant.ElitaPlus.BusinessObjectsNew.Item.COL_ITEM_NUMBER).ToString
                    e.Row.Cells(GRID_COL_RISK_TYPE).Text =
                        dvRow(Assurant.ElitaPlus.BusinessObjectsNew.Item.COL_RISK_TYPE).ToString
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub
    End Class

End Namespace


