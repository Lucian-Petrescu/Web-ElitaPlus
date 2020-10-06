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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Try
                IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                If retObj IsNot Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.moProductCodeId = retObj.moProductCodeId
                            State.IsGridVisible = True
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                            State.moProductCodeId = Guid.Empty
                        Case Else
                            State.moProductCodeId = Guid.Empty
                    End Select
                    Grid.PageIndex = State.PageIndex                    
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    grid.PageSize = State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, grid.Visible)
                    'PopulateDealer()
                    'PopulateRiskGroup()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub



#End Region

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moProductCodeId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oProductCodeId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moProductCodeId = oProductCodeId
                Me.BoChanged = boChanged
            End Sub

        End Class

        '#End Region
#Region "Page_Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                MasterPage.MessageController.Clear_Hide()
                'SetSession()
                'grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                If Not Page.IsPostBack Then

                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()

                    TranslateGridHeader(Grid)

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SortDirection = State.SortExpression
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    PopulateDropdown()
                    If State.IsGridVisible Then
                        If Not (State.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.PageSize = Grid.PageSize) Then
                            Grid.PageSize = State.PageSize
                        End If
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
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
                DealerMultipleDrop.SelectedGuid = State.DealerId

            Catch ex As Exception
                MasterPage.MessageController.AddError(PRODUCTCODE_LIST_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
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
                BindSelectItem(State.RiskGroupId.ToString, moRiskGroupDrop)
            Catch ex As Exception
                MasterPage.MessageController.AddError(PRODUCTCODE_LIST_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateProductCode()
            moProductCodeText.Text = State.ProductCodeMask
        End Sub

        Private Sub BindDataGrid(oDataView As DataView)
            grid.DataSource = oDataView
            grid.DataBind()
        End Sub


        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (State.searchDV Is Nothing) Then
                    State.searchDV = ProductCode.getList(ElitaPlusIdentity.Current.ActiveUser.Companies, DealerMultipleDrop.SelectedGuid, GetSelectedItem(moRiskGroupDrop), State.ProductCodeMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                End If

                If (State.searchDV.Count = 0) Then

                    State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, SortDirection)
                Else
                    State.bnoRow = False
                    Grid.Enabled = True
                End If

                State.searchDV.Sort = State.SortExpression
                grid.AutoGenerateColumns = False

                grid.Columns(GRID_COL_DEALER_NAME).SortExpression = ProductCode.ProductCodeSearchDV.COL_DEALER_NAME
                grid.Columns(GRID_COL_PRODUCT_CODE).SortExpression = ProductCode.ProductCodeSearchDV.COL_PRODUCT_CODE
                grid.Columns(GRID_COL_RISK_GROUP).SortExpression = ProductCode.ProductCodeSearchDV.COL_RISK_GROUP
                grid.Columns(GRID_COL_DESCRIPTION).SortExpression = ProductCode.ProductCodeSearchDV.COL_DESCRIPTION
                HighLightSortColumn(grid, State.SortExpression)
                ' BasePopulateGrid(Grid, Me.State.searchDV, Me.State.moProductCodeId, oAction)

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.DealerId, Grid, State.PageIndex)

                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()               

                ControlMgr.SetVisibleControl(Me, trPageSize, grid.Visible)

                Session("recCount") = State.searchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearch()
            DealerMultipleDrop.SelectedIndex = 0
            moRiskGroupDrop.SelectedIndex = 0
            moProductCodeText.Text = Nothing
            State.moProductCodeId = Guid.Empty
        End Sub

        Private Function GetDataView() As DataView

            Return (ProductCode.getList(ElitaPlusIdentity.Current.ActiveUser.Companies, DealerMultipleDrop.SelectedGuid, GetSelectedItem(moRiskGroupDrop), State.ProductCodeMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId))

        End Function

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("PRODUCT_CODE")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRODUCT_CODE")
                End If
            End If
        End Sub

#End Region

#Region "Datagrid Related"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageIndex = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                State.DealerId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If dvRow IsNot Nothing And Not State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        btnEditItem = CType(e.Row.Cells(GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(ProductCode.ProductCodeSearchDV.COL_PRODUCT_CODE).ToString
                        e.Row.Cells(GRID_COL_RISK_GROUP).Text = dvRow(ProductCode.ProductCodeSearchDV.COL_RISK_GROUP).ToString
                        e.Row.Cells(GRID_COL_DEALER_NAME).Text = dvRow(ProductCode.ProductCodeSearchDV.COL_DEALER_NAME).ToString
                        e.Row.Cells(GRID_COL_DESCRIPTION).Text = dvRow(ProductCode.ProductCodeSearchDV.COL_DESCRIPTION).ToString                        
                        e.Row.Cells(GRID_COL_PRODUCT_CODE_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(ProductCode.ProductCodeSearchDV.COL_ProductCode_ID), Byte()))
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    State.moProductCodeId = New Guid(Grid.Rows(index).Cells(GRID_COL_PRODUCT_CODE_IDX).Text)
                    callPage(ProductCodeForm.URL, State.moProductCodeId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region " Button Clicks "
        Private Sub moBtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSearch.Click
            Try
                ' Dim oState As TheState
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    State.IsGridVisible = True
                End If                
                Grid.PageIndex = NO_PAGE_INDEX
                grid.DataMember = Nothing
                State.searchDV = Nothing
                SetSession()
                Grid.PageIndex = NO_PAGE_INDEX
                grid.DataMember = Nothing
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(sender As System.Object, e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                State.moProductCodeId = Guid.Empty
                SetSession()
                '     Response.Redirect(PRODUCTCODE_DETAIL_PAGE)
                callPage(ProductCodeForm.URL, State.moProductCodeId)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "State-Management"

        Private Sub SetSession()
            With State
                .ProductCodeMask = moProductCodeText.Text.ToUpper
                .DealerId = DealerMultipleDrop.SelectedGuid
                .RiskGroupId = GetSelectedItem(moRiskGroupDrop)
                .PageIndex = Grid.PageIndex
                .PageSize = grid.PageSize
                .PageSort = State.SortExpression
                .SearchDataView = State.searchDV
            End With
        End Sub


#End Region
    End Class
End Namespace
