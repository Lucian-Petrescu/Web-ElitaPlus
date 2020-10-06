Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class ProductGroupListForm
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
    Public Const GRID_COL_EDIT As Integer = 0
    Public Const GRID_COL_EXDN_ID As Integer = 1
    Public Const GRID_COL_CODE As Integer = 2
    Public Const GRID_COL_DESCRIPTION As Integer = 3
    Private Const LABEL_DEALER As String = "DEALER_NAME"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = ProductGroup.ProductGroupSearchDV.COL_NAME_DESCRIPTION
        Public SelectedEXDNId As Guid = Guid.Empty
        Public SelectedDealerId As Guid = Guid.Empty
        Public SelectedProdCode As Guid = Guid.Empty
        Public SelectedRiskType As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Public SearchDescription As String
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As ProductGroup.ProductGroupSearchDV = Nothing
        Public HasDataChanged As Boolean

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
            Dim retObj As ProductGroupForm.ReturnType = CType(ReturnPar, ProductGroupForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.SelectedEXDNId = retObj.EditingBo.Id
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
        State.SearchDescription = TextboxDescription.Text
        State.SelectedDealerId = TheDealerControl.SelectedGuid
        If moProductDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            State.SelectedProdCode = New Guid(moProductDrop.SelectedValue)
        Else
            State.SelectedProdCode = Guid.Empty
        End If

        If moRiskDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            State.SelectedRiskType = New Guid(moRiskDrop.SelectedValue)
        Else
            State.SelectedRiskType = Guid.Empty
        End If
    End Sub

    Private Sub RestoreGuiState()
        TextboxDescription.Text = State.SearchDescription
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        Try
            If Not IsPostBack Then
                PopulateDealer()
                SetDefaultButton(TextboxDescription, btnSearch)
                SetDefaultButton(moProductDrop, btnSearch)
                SetDefaultButton(moRiskDrop, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
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
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub

#End Region

#Region "Controlling Logic"
    Protected Sub EnableDisableFields()
        If Not TheDealerControl.SelectedGuid.Equals(Guid.Empty) Then
            ControlMgr.SetEnableControl(Me, moProductDrop, True)
            If moProductDrop.SelectedIndex > 0 Then
                ControlMgr.SetEnableControl(Me, moRiskDrop, True)
            Else
                ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            End If
        Else
            ControlMgr.SetEnableControl(Me, moProductDrop, False)
            ControlMgr.SetEnableControl(Me, moRiskDrop, False)
        End If
    End Sub
    Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
         Handles multipleDealerDropControl.SelectedDropChanged
        Try
            ClearList(moProductDrop)
            If TheDealerControl.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                PopulateProductCode()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
    Protected Sub moProductDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moProductDrop.SelectedIndexChanged
        Try
            ClearList(moRiskDrop)
            If moProductDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                PopulateRiskType()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
    Private Sub PopulateDealer()
        Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Try
            Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
            TheDealerControl.SetControl(True, TheDealerControl.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
            TheDealerControl.SelectedGuid = State.SelectedDealerId
            PopulateProductCode()
        Catch ex As Exception
            ErrorCtrl.AddError(ex.Message, False)
            ErrorCtrl.Show()
        End Try
    End Sub
    Private Sub PopulateProductCode()
        Dim oDealerId As Guid = TheDealerControl.SelectedGuid
        Try
            'Me.BindListControlToDataView(moProductDrop, LookupListNew.GetProductCodeLookupList(oDealerId), "CODE")
            Dim listcontext As ListContext = New ListContext()
            listcontext.DealerId = oDealerId
            Dim ProdLKL As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ProductCodeByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            moProductDrop.Populate(ProdLKL, New PopulateOptions() With
                {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode,
                .SortFunc = AddressOf .GetCode
                })
            If moProductDrop.Items.Contains(moProductDrop.Items.FindByValue(State.SelectedProdCode.ToString)) Then
                moProductDrop.SelectedValue = State.SelectedProdCode.ToString
            End If
            PopulateRiskType()
        Catch ex As Exception
            ErrorCtrl.AddError(ex.Message, False)
            ErrorCtrl.Show()
        End Try
    End Sub

    Private Sub PopulateRiskType()
        Dim oProductCode As Guid = New Guid(moProductDrop.SelectedValue) 'Me.State.SelectedProdCode
        Try
            ' Me.BindListControlToDataView(moRiskDrop, LookupListNew.GetRiskProductCodeLookupList(oProductCode)) 'ItemRiskTypeByProduct
            Dim listcontext As ListContext = New ListContext()
            listcontext.ProductCodeId = oProductCode
            Dim riskLKL As ListItem() = CommonConfigManager.Current.ListManager.GetList("ItemRiskTypeByProduct", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            moRiskDrop.Populate(riskLKL, New PopulateOptions() With
                {
                .AddBlankItem = True
                })
            If moRiskDrop.Items.Contains(moRiskDrop.Items.FindByValue(State.SelectedRiskType.ToString)) Then
                moRiskDrop.SelectedValue = State.SelectedRiskType.ToString
            End If
        Catch ex As Exception
            ErrorCtrl.AddError(ex.Message, False)
            ErrorCtrl.Show()
        End Try
    End Sub

    Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
        Get
            If multipleDealerDropControl Is Nothing Then
                multipleDealerDropControl = CType(FindControl("multipleDealerDropControl"), MultipleColumnDDLabelControl)
            End If
            Return multipleDealerDropControl
        End Get
    End Property

    Public Sub PopulateGrid()
        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = ProductGroup.getList(TextboxDescription.Text.Trim, TheDealerControl.SelectedGuid, moProductDrop.SelectedValue.Trim, moRiskDrop.SelectedValue.Trim)
        End If

        State.searchDV.Sort = State.SortExpression

        Grid.AutoGenerateColumns = False
        Grid.Columns(GRID_COL_CODE).SortExpression = ProductGroup.ProductGroupSearchDV.COL_NAME_DEALER
        Grid.Columns(GRID_COL_DESCRIPTION).SortExpression = ProductGroup.ProductGroupSearchDV.COL_NAME_DESCRIPTION

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.SelectedEXDNId, Grid, State.PageIndex)
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

    'The Binding Logic is here  
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(GRID_COL_EXDN_ID).Text = New Guid(CType(dvRow(ProductGroup.ProductGroupSearchDV.COL_NAME_PRODUCT_GROUP_ID), Byte())).ToString
                e.Item.Cells(GRID_COL_CODE).Text = dvRow(ProductGroup.ProductGroupSearchDV.COL_NAME_DEALER).ToString
                e.Item.Cells(GRID_COL_DESCRIPTION).Text = dvRow(ProductGroup.ProductGroupSearchDV.COL_NAME_DESCRIPTION).ToString
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
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
                State.SelectedEXDNId = New Guid(e.Item.Cells(GRID_COL_EXDN_ID).Text)
                callPage(ProductGroupForm.URL, State.SelectedEXDNId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.SelectedEXDNId = Guid.Empty
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
            State.SelectedEXDNId = Guid.Empty
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
            callPage(ProductGroupForm.URL)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            TextboxDescription.Text = ""
            TheDealerControl.SelectedIndex = -1
            moProductDrop.SelectedIndex = -1
            moRiskDrop.SelectedIndex = -1
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region
End Class
