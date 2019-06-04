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
                Set(ByVal Value As Integer)
                    mnPageIndex = Value
                End Set
            End Property

            Public Property PageSize() As Integer
                Get
                    Return mnPageSize
                End Get
                Set(ByVal Value As Integer)
                    mnPageSize = Value
                End Set
            End Property

            Public Property PageSort() As String
                Get
                    Return msPageSort
                End Get
                Set(ByVal Value As String)
                    msPageSort = Value
                End Set
            End Property

            Public Property SearchDataView() As DataView
                Get
                    Return moSearchDataView
                End Get
                Set(ByVal Value As DataView)
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
                    For i = 0 To Me.SortColumns.Length - 1
                        If Not Me.SortColumns(i) Is Nothing Then
                            sortExp &= Me.SortColumns(i)
                            If Me.IsSortDesc(i) Then sortExp &= " DESC"
                            sortExp &= ","
                        End If
                    Next
                    Return sortExp.Substring(0, sortExp.Length - 1) 'to remove the last comma
                End Get
            End Property

            Public Sub ToggleSort1(ByVal gridColIndex As Integer)
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

        Public Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.IsReturningFromChild = True
                If Me.State.searchDV Is Nothing Then
                    Me.State.IsGridVisible = False
                Else
                    Me.State.IsGridVisible = True
                End If
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                If Not retObj Is Nothing Then

                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.moItemId = retObj.moItemId
                        Case Else
                            Me.State.moItemId = Guid.Empty
                    End Select
                    moItemGrid.PageIndex = Me.State.PageIndex
                    moItemGrid.PageSize = Me.State.PageSize
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    moItemGrid.PageSize = Me.State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, moItemGrid.Visible)
                    PopulateDealer()
                    PopulateRiskType()
                    PopulateProductCode()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moItemId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal oItemId As Guid, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.moItemId = oItemId
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                moErrorController.Clear_Hide()
                If Not Page.IsPostBack Then
                    Me.SortDirection = Me.State.SortExpression
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)

                    '   Me.MenuEnabled = True
                    Me.SetGridItemStyleColor(moItemGrid)
                    Me.TranslateGridHeader(Me.moItemGrid)
                    If Not Me.IsReturningFromChild Then
                        ' It is The First Time
                        ' It is not Returning from Detail
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        PopulateDealer()
                        PopulateRiskType()
                        PopulateProductCode()
                    Else
                        ' It is returning from detail
                        PopulateProductCode()
                        Me.PopulateGrid(Me.POPULATE_ACTION_SAVE)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
            Me.ShowMissingTranslations(moErrorController)
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

        Private Sub moProductCodeDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moProductCodeDrop.SelectedIndexChanged
            Try
                ClearForProduct()
                If moProductCodeDrop.SelectedIndex > 0 Then
                    PopulateRiskType()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub moBtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
            Try
                moItemGrid.PageIndex = Me.NO_PAGE_INDEX
                moItemGrid.DataMember = Nothing
                Me.State.searchDV = Nothing
                Me.State.searchBtnClicked = True
                Me.PopulateGrid()
                Me.State.searchBtnClicked = False
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                'Me.State.moItemId = Guid.Empty
                SetSession()
                Me.callPage(ItemForm.URL)
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#Region "Handlers-Grid"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub moItemGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moItemGrid.PageIndexChanging
            Try
                moItemGrid.PageIndex = e.NewPageIndex
                PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moItemGrid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moItemGrid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moItemGrid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles moItemGrid.RowCommand
            Dim sItemId As String
            Try

                If e.CommandSource.GetType.Equals(GetType(ImageButton)) Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    'this only runs when they click the pencil button for editing.
                    'New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_ITEM_IDX).Text)
                    'sItemId = CType(e.row.FindControl("moItemId"), Label).Text
                    Me.State.moItemId = New Guid(Me.moItemGrid.Rows(index).Cells(Me.GRID_COL_ITEM_IDX).Text)
                    SetSession()
                    Me.callPage(ItemForm.URL, Me.State.moItemId)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub moItemGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moItemGrid.Sorting
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
                Me.HandleErrors(ex, Me.moErrorController)
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
                TheDealerControl.SelectedGuid = Me.State.DealerId


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
                BindSelectItem(Me.State.ProductCodeId.ToString, moProductCodeDrop)
                moProductCodeDrop.Enabled = True
            Catch ex As Exception
                moErrorController.AddError(ITEM_LIST_FORM002)
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub PopulateRiskType()
            Try
                Dim oProductId As Guid = Me.GetSelectedItem(moProductCodeDrop)

                '  Me.BindListControlToDataView(moItemRiskTypeDrop, LookupListNew.GetRiskProductCodeLookupList(oProductId)) 'need to implement
                Dim listcontext As ListContext = New ListContext()
                listcontext.ProductCodeId = oProductId
                Dim riskLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.RiskTypeByProduct, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moItemRiskTypeDrop.Populate(riskLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True
                 })
                BindSelectItem(Me.State.RiskTypeId.ToString, moItemRiskTypeDrop)
                moItemRiskTypeDrop.Enabled = True
            Catch ex As Exception
                moErrorController.AddError(ITEM_LIST_FORM002)
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub BindDataGrid(ByVal oDataView As DataView)
            'oDataView.Sort = moItemGrid.DataMember()
            'moItemGrid.DataSource = oDataView
            'moItemGrid.DataBind()
            Me.State.PageIndex = Me.moItemGrid.PageIndex

            If (oDataView.Count = 0) Then

                Me.State.bnoRow = True
                CreateHeaderForEmptyGrid(moItemGrid, Me.SortDirection)
            Else
                Me.State.bnoRow = False
                Me.moItemGrid.Enabled = True
                Me.moItemGrid.DataSource = Me.State.searchDV
                HighLightSortColumn(moItemGrid, Me.SortDirection)
                Me.moItemGrid.DataBind()
            End If

            If Not moItemGrid.BottomPagerRow.Visible Then moItemGrid.BottomPagerRow.Visible = True

        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Me.moItemGrid.PageIndex = NewCurrentPageIndex(moItemGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (Me.State.searchDV Is Nothing) Then
                    'oDataView = GetDataView()
                    Me.State.searchDV = GetDataView()
                    'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                    'If Me.State.searchBtnClicked Then
                    'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    ' End If
                End If

                Me.State.searchDV.Sort = Me.State.SortExpression
                moItemGrid.AutoGenerateColumns = False
                'moItemGrid.Columns(Me.GRID_COL_DEALER_NAME).SortExpression = Item.COL_DEALER_NAME
                'moItemGrid.Columns(Me.GRID_COL_PRODUCT_CODE).SortExpression = Item.COL_PRODUCT_CODE
                'moItemGrid.Columns(Me.GRID_COL_RISK_TYPE).SortExpression = Item.COL_RISK_TYPE
                'moItemGrid.Columns(Me.GRID_COL_ITEM_NUMBER).SortExpression = Item.COL_ITEM_NUMBER
                HighLightSortColumn(moItemGrid, Me.State.SortExpression)
                BasePopulateGrid(moItemGrid, Me.State.searchDV, Me.State.moItemId, oAction)

                ControlMgr.SetVisibleControl(Me, trPageSize, moItemGrid.Visible)

                Session("recCount") = Me.State.searchDV.Count

                If Me.moItemGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
                BindDataGrid(Me.State.searchDV)
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub EnableDropDowns(ByVal bIsEnable As Boolean)
            moProductCodeDrop.Enabled = bIsEnable
            moItemRiskTypeDrop.Enabled = bIsEnable
        End Sub

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
         Handles multipleDropControl.SelectedDropChanged
            Try
                Me.State.DealerId = TheDealerControl.SelectedGuid
                PopulateDealer()
                ClearForDealer()
                If TheDealerControl.SelectedIndex > 0 Then
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, Me.moErrorController)
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
            Me.ClearList(moItemRiskTypeDrop)
            moItemRiskTypeDrop.Enabled = False
        End Sub

        Private Sub ClearForDealer()
            ClearForProduct()
            Me.ClearList(moProductCodeDrop)
            moProductCodeDrop.Enabled = False
        End Sub

        Private Sub ClearSearch()
            TheDealerControl.SelectedIndex = 0
            ClearForDealer()
            Me.State.moItemId = Guid.Empty
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With Me.State
                .DealerId = TheDealerControl.SelectedGuid '.GetSelectedItem(moDealerDrop)
                .RiskTypeId = Me.GetSelectedItem(moItemRiskTypeDrop)
                .ProductCodeId = Me.GetSelectedItem(moProductCodeDrop)
                .PageIndex = moItemGrid.PageIndex
                .PageSize = moItemGrid.PageSize
                .PageSort = Me.State.SortExpression
                .SearchDataView = Me.State.searchDV
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
                Me.PopulateBOProperty(oItem, "DealerID", TheDealerControl.SelectedGuid)
                Me.PopulateBOProperty(oItem, "ProductCodeId", moProductCodeDrop)
                Me.PopulateBOProperty(oItem, "RiskTypeId", moItemRiskTypeDrop)
                oDataView = .GetList(.DealerID, .ProductCodeId, .RiskTypeId)
            End With
            Return oDataView
        End Function


#End Region


        Private Sub moItemGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moItemGrid.RowDataBound
            Try

                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        e.Row.Cells(Me.GRID_COL_ITEM_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Assurant.ElitaPlus.BusinessObjectsNew.Item.COL_ITEM_ID), Byte()))
                        e.Row.Cells(Me.GRID_COL_DEALER_NAME).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.Item.COL_DEALER_NAME).ToString
                        e.Row.Cells(Me.GRID_COL_PRODUCT_CODE).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.Item.COL_PRODUCT_CODE).ToString
                        e.Row.Cells(Me.GRID_COL_ITEM_NUMBER).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.Item.COL_ITEM_NUMBER).ToString
                        e.Row.Cells(Me.GRID_COL_RISK_TYPE).Text = dvRow(Assurant.ElitaPlus.BusinessObjectsNew.Item.COL_RISK_TYPE).ToString

                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub
    End Class

End Namespace


