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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As ProductGroupForm.ReturnType = CType(ReturnPar, ProductGroupForm.ReturnType)
            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.SelectedEXDNId = retObj.EditingBo.Id
                        End If
                        Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub SaveGuiState()
        Me.State.SearchDescription = Me.TextboxDescription.Text
        Me.State.SelectedDealerId = TheDealerControl.SelectedGuid
        If Me.moProductDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            Me.State.SelectedProdCode = New Guid(Me.moProductDrop.SelectedValue)
        Else
            Me.State.SelectedProdCode = Guid.Empty
        End If

        If Me.moRiskDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            Me.State.SelectedRiskType = New Guid(Me.moRiskDrop.SelectedValue)
        Else
            Me.State.SelectedRiskType = Guid.Empty
        End If
    End Sub

    Private Sub RestoreGuiState()
        Me.TextboxDescription.Text = Me.State.SearchDescription
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                PopulateDealer()
                Me.SetDefaultButton(Me.TextboxDescription, btnSearch)
                Me.SetDefaultButton(Me.moProductDrop, btnSearch)
                Me.SetDefaultButton(Me.moRiskDrop, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.RestoreGuiState()
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
            Else
                Me.SaveGuiState()
            End If
            EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
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
    Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
         Handles multipleDealerDropControl.SelectedDropChanged
        Try
            ClearList(moProductDrop)
            If TheDealerControl.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                PopulateProductCode()
            End If
        Catch ex As Exception
            HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
    Protected Sub moProductDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles moProductDrop.SelectedIndexChanged
        Try
            ClearList(moRiskDrop)
            If moProductDrop.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                PopulateRiskType()
            End If
        Catch ex As Exception
            HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
    Private Sub PopulateDealer()
        Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Try
            Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
            TheDealerControl.SetControl(True, TheDealerControl.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
            TheDealerControl.SelectedGuid = Me.State.SelectedDealerId
            PopulateProductCode()
        Catch ex As Exception
            Me.ErrorCtrl.AddError(ex.Message, False)
            Me.ErrorCtrl.Show()
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
            If moProductDrop.Items.Contains(moProductDrop.Items.FindByValue(Me.State.SelectedProdCode.ToString)) Then
                Me.moProductDrop.SelectedValue = Me.State.SelectedProdCode.ToString
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
            If moRiskDrop.Items.Contains(moRiskDrop.Items.FindByValue(Me.State.SelectedRiskType.ToString)) Then
                Me.moRiskDrop.SelectedValue = Me.State.SelectedRiskType.ToString
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
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = ProductGroup.getList(Me.TextboxDescription.Text.Trim, TheDealerControl.SelectedGuid, Me.moProductDrop.SelectedValue.Trim, Me.moRiskDrop.SelectedValue.Trim)
        End If

        Me.State.searchDV.Sort = Me.State.SortExpression

        Me.Grid.AutoGenerateColumns = False
        Me.Grid.Columns(Me.GRID_COL_CODE).SortExpression = ProductGroup.ProductGroupSearchDV.COL_NAME_DEALER
        Me.Grid.Columns(Me.GRID_COL_DESCRIPTION).SortExpression = ProductGroup.ProductGroupSearchDV.COL_NAME_DESCRIPTION

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.SelectedEXDNId, Me.Grid, Me.State.PageIndex)
        Me.SortAndBindGrid()

    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.CurrentPageIndex
        Me.Grid.DataSource = Me.State.searchDV
        HighLightSortColumn(Grid, Me.State.SortExpression)
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


#End Region


#Region " Datagrid Related "

    'The Binding Logic is here  
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(Me.GRID_COL_EXDN_ID).Text = New Guid(CType(dvRow(ProductGroup.ProductGroupSearchDV.COL_NAME_PRODUCT_GROUP_ID), Byte())).ToString
                e.Item.Cells(Me.GRID_COL_CODE).Text = dvRow(ProductGroup.ProductGroupSearchDV.COL_NAME_DEALER).ToString
                e.Item.Cells(Me.GRID_COL_DESCRIPTION).Text = dvRow(ProductGroup.ProductGroupSearchDV.COL_NAME_DESCRIPTION).ToString
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Me.State.SelectedEXDNId = New Guid(e.Item.Cells(Me.GRID_COL_EXDN_ID).Text)
                Me.callPage(ProductGroupForm.URL, Me.State.SelectedEXDNId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.SelectedEXDNId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.SelectedEXDNId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            Me.callPage(ProductGroupForm.URL)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.TextboxDescription.Text = ""
            TheDealerControl.SelectedIndex = -1
            moProductDrop.SelectedIndex = -1
            moRiskDrop.SelectedIndex = -1
            EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region
End Class
