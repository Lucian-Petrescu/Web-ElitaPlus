Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables

    Partial Class ProductCodeSearchForm
        Inherits ElitaPlusSearchPage

        'Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl

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

        Private Const PRODUCTCODE_LIST_FORM001 As String = "PRODUCTCODE_LIST_FORM001" ' Maintain Product Code List Exception
        Private Const PRODUCTCODE_DETAIL_PAGE As String = "ProductCodeForm.aspx"
        Private Const PRODUCTCODE_STATE As String = "ProductCodeState"
        'Public Const GRID_TOTAL_COLUMNS As Integer = 6
        'Public Const GRID_COL_EDIT_IDX As Integer = 0
        'Public Const GRID_COL_PRODUCT_CODE_IDX As Integer = 1
        'Public Const GRID_COL_DEALER_NAME As Integer = 2
        'Public Const GRID_COL_PRODUCT_CODE As Integer = 3
        'Public Const GRID_COL_RISK_GROUP As Integer = 4
        'Public Const GRID_COL_DESCRIPTION As Integer = 5
        Private Const LABEL_DEALER As String = "DEALER"

#Region "Constants"
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_PRODUCT_CODE As Integer = 0
        Public Const GRID_COL_DEALER_NAME As Integer = 1
        Public Const GRID_COL_RISK_GROUP As Integer = 2
        Public Const GRID_COL_DESCRIPTION As Integer = 3
        Public Const GRID_COL_PRODUCT_CODE_IDX As Integer = 4

        Public Const GRID_TOTAL_COLUMNS As Integer = 5
        Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
        Private Const DEALERLISTFORM As String = "ProductCodeForm.aspx"
        Private Const LABEL_SELECT_PRODUCTCODE As String = "PRODUCT_CODE"

#End Region
#End Region

#Region "Properties"

        'Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
        '    Get
        '        If multipleDropControl Is Nothing Then
        '            multipleDropControl = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl)
        '        End If
        '        Return multipleDropControl
        '    End Get
        'End Property

#End Region

#Region "Page State"

        ' This class keeps the current state for the search page.
        Class MyState

            Public IsGridVisible As Boolean = False
            Public searchDV As ProductCode.ProductCodeSearchDV = Nothing

            Public SortExpression As String = ProductCode.ProductCodeSearchDV.COL_PRODUCT_CODE
            Public PageIndex As Integer = 0
            Public PageSort As String
            Public PageSize As Integer = 15
            Public SearchDataView As ProductCode.ProductCodeSearchDV
            Public moProductCodeId As Guid = Guid.Empty
            'Public inputParameters As Parameters

            Public ProductCodeMask As String
            Public DealerId As Guid = Guid.Empty
            Public RiskGroupId As Guid = Guid.Empty
            Public bnoRow As Boolean = False

            Sub New()
                '    SortColumns(GRID_COL_DEALER_NAME) = ProductCode.ProductCodeSearchDV.COL_DEALER_NAME
                '    SortColumns(GRID_COL_PRODUCT_CODE) = ProductCode.ProductCodeSearchDV.COL_PRODUCT_CODE
                '    SortColumns(GRID_COL_RISK_GROUP) = ProductCode.ProductCodeSearchDV.COL_RISK_GROUP
                '    SortColumns(GRID_COL_DESCRIPTION) = ProductCode.ProductCodeSearchDV.COL_DESCRIPTION

                '    IsSortDesc(GRID_COL_DEALER_NAME) = False
                '    IsSortDesc(GRID_COL_PRODUCT_CODE) = False
                '    IsSortDesc(GRID_COL_RISK_GROUP) = False
                '    IsSortDesc(GRID_COL_DESCRIPTION) = False
                ''moProductCodeData = New ProductCodeData
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

#Region "Page Return"

        Private IsReturningFromChild As Boolean = False

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
            Try
                Me.IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                If Not retObj Is Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.moProductCodeId = retObj.moProductCodeId
                            Me.State.IsGridVisible = True
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                            Me.State.moProductCodeId = Guid.Empty
                        Case Else
                            Me.State.moProductCodeId = Guid.Empty
                    End Select
                    Grid.PageIndex = Me.State.PageIndex                    
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    grid.PageSize = Me.State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, grid.Visible)
                    'PopulateDealer()
                    'PopulateRiskGroup()
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub



#End Region

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moProductCodeId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal oProductCodeId As Guid, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.moProductCodeId = oProductCodeId
                Me.BoChanged = boChanged
            End Sub

        End Class

        '#End Region
#Region "Page_Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.MasterPage.MessageController.Clear_Hide()
                'SetSession()
                'grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                If Not Page.IsPostBack Then

                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()

                    TranslateGridHeader(Grid)

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SortDirection = Me.State.SortExpression
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    PopulateDropdown()
                    If Me.State.IsGridVisible Then
                        If Not (Me.State.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.PageSize = Grid.PageSize) Then
                            Grid.PageSize = Me.State.PageSize
                        End If
                        Me.PopulateGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region

#Region "Controlling Logic"

        Private Sub PopulateDropdown()
            PopulateDealer()
            PopulateRiskGroup()
            PopulateProductCode()
        End Sub

        Private Sub PopulateDealer()
            Try

                Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.BindData(oDataView)
                DealerMultipleDrop.AutoPostBackDD = False
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SelectedGuid = Me.State.DealerId

            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(PRODUCTCODE_LIST_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateRiskGroup()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                '  Me.BindListControlToDataView(moRiskGroupDrop, LookupListNew.GetRiskGroupsLookupList(oLanguageId)) 'RGRP
                moRiskGroupDrop.Populate(CommonConfigManager.Current.ListManager.GetList("RGRP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                            {
                  .AddBlankItem = True
                    })
                BindSelectItem(Me.State.RiskGroupId.ToString, moRiskGroupDrop)
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(PRODUCTCODE_LIST_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateProductCode()
            moProductCodeText.Text = Me.State.ProductCodeMask
        End Sub

        Private Sub BindDataGrid(ByVal oDataView As DataView)
            grid.DataSource = oDataView
            grid.DataBind()
        End Sub


        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = ProductCode.getList(ElitaPlusIdentity.Current.ActiveUser.Companies, DealerMultipleDrop.SelectedGuid, Me.GetSelectedItem(moRiskGroupDrop), Me.State.ProductCodeMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                End If

                If (Me.State.searchDV.Count = 0) Then

                    Me.State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
                Else
                    Me.State.bnoRow = False
                    Me.Grid.Enabled = True
                End If

                Me.State.searchDV.Sort = Me.State.SortExpression
                grid.AutoGenerateColumns = False

                grid.Columns(Me.GRID_COL_DEALER_NAME).SortExpression = ProductCode.ProductCodeSearchDV.COL_DEALER_NAME
                grid.Columns(Me.GRID_COL_PRODUCT_CODE).SortExpression = ProductCode.ProductCodeSearchDV.COL_PRODUCT_CODE
                grid.Columns(Me.GRID_COL_RISK_GROUP).SortExpression = ProductCode.ProductCodeSearchDV.COL_RISK_GROUP
                grid.Columns(Me.GRID_COL_DESCRIPTION).SortExpression = ProductCode.ProductCodeSearchDV.COL_DESCRIPTION
                HighLightSortColumn(grid, Me.State.SortExpression)
                ' BasePopulateGrid(Grid, Me.State.searchDV, Me.State.moProductCodeId, oAction)

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.DealerId, Me.Grid, Me.State.PageIndex)

                Me.Grid.DataSource = Me.State.searchDV
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()               

                ControlMgr.SetVisibleControl(Me, trPageSize, grid.Visible)

                Session("recCount") = Me.State.searchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearch()
            DealerMultipleDrop.SelectedIndex = 0
            moRiskGroupDrop.SelectedIndex = 0
            moProductCodeText.Text = Nothing
            Me.State.moProductCodeId = Guid.Empty
        End Sub

        Private Function GetDataView() As DataView

            Return (ProductCode.getList(ElitaPlusIdentity.Current.ActiveUser.Companies, DealerMultipleDrop.SelectedGuid, Me.GetSelectedItem(moRiskGroupDrop), Me.State.ProductCodeMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId))

        End Function

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("PRODUCT_CODE")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRODUCT_CODE")
                End If
            End If
        End Sub

#End Region

#Region "Datagrid Related"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageIndex = Grid.PageSize
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.State.DealerId = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        btnEditItem = CType(e.Row.Cells(Me.GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(ProductCode.ProductCodeSearchDV.COL_PRODUCT_CODE).ToString
                        e.Row.Cells(Me.GRID_COL_RISK_GROUP).Text = dvRow(ProductCode.ProductCodeSearchDV.COL_RISK_GROUP).ToString
                        e.Row.Cells(Me.GRID_COL_DEALER_NAME).Text = dvRow(ProductCode.ProductCodeSearchDV.COL_DEALER_NAME).ToString
                        e.Row.Cells(Me.GRID_COL_DESCRIPTION).Text = dvRow(ProductCode.ProductCodeSearchDV.COL_DESCRIPTION).ToString                        
                        e.Row.Cells(Me.GRID_COL_PRODUCT_CODE_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(ProductCode.ProductCodeSearchDV.COL_ProductCode_ID), Byte()))
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Me.State.moProductCodeId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_PRODUCT_CODE_IDX).Text)
                    Me.callPage(ProductCodeForm.URL, Me.State.moProductCodeId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region " Button Clicks "
        Private Sub moBtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
            Try
                ' Dim oState As TheState
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    Me.State.IsGridVisible = True
                End If                
                Grid.PageIndex = Me.NO_PAGE_INDEX
                grid.DataMember = Nothing
                Me.State.searchDV = Nothing
                SetSession()
                Grid.PageIndex = Me.NO_PAGE_INDEX
                grid.DataMember = Nothing
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.State.moProductCodeId = Guid.Empty
                SetSession()
                '     Response.Redirect(PRODUCTCODE_DETAIL_PAGE)
                Me.callPage(ProductCodeForm.URL, Me.State.moProductCodeId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "State-Management"

        Private Sub SetSession()
            With Me.State
                .ProductCodeMask = moProductCodeText.Text.ToUpper
                .DealerId = DealerMultipleDrop.SelectedGuid
                .RiskGroupId = Me.GetSelectedItem(moRiskGroupDrop)
                .PageIndex = Grid.PageIndex
                .PageSize = grid.PageSize
                .PageSort = Me.State.SortExpression
                .SearchDataView = Me.State.searchDV
            End With
        End Sub


#End Region
    End Class
End Namespace
