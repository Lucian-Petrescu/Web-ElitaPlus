Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Tables

    Partial Class CommissionPeriodSearchForm
        Inherits ElitaPlusSearchPage
        ' Inherits System.Web.UI.Page

#Region "Constants"

        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const COMMISSION_PERIOD_ID As Integer = 1
        Public Const GRID_COL_COMPANY_CODE_IDX As Integer = 2
        Public Const GRID_COL_DEALER_IDX As Integer = 3
        Public Const GRID_COL_EFFECTIVE_IDX As Integer = 4
        Public Const GRID_COL_EXPIRATION_IDX As Integer = 5
        Public Const GRID_TOTAL_COLUMNS As Integer = 6

        Public Const COMM_P_CODE_ID As Integer = 1
        Public Const GRID_COL_PRODUCT_IDX As Integer = 4
        Public Const GRID_COL_CM_EFFECTIVE_IDX As Integer = 5
        Public Const GRID_COL_CM_EXPIRATION_IDX As Integer = 6

        Private Const GRID_CTRL_NAME_COMM_PERIOD_ID As String = "moCommissionPeriodId_NO_TRANSLATE"

        Private Const LABEL_DEALER As String = "DEALER"
        Public Const PAGETITLE As String = "COMMISSION_BREAKDOWN"
        Public Const PAGETAB As String = "TABLES"
#End Region

#Region "Page State"

#Region "MyState"


        Class MyState
            Public moCommissionPeriodId As Guid = Guid.Empty
            Public moCommPCodeId As Guid = Guid.Empty
            Public moDealerId As Guid = Guid.Empty
            Public moProductCodeId As Guid = Guid.Empty
            Public mnPageIndex As Integer
            Public moChkIsCommProdCode As Boolean = False
            Public searchDV As DataView = Nothing
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean = False
            Public searchBtnClicked As Boolean = False
            Public SortExpression As String = CommissionPeriodDAL.COL_NAME_DEALER_NAME

            ' these variables are used to store the sorting columns information.
            Public SortColumns(GRID_TOTAL_COLUMNS - 1) As String
            Public IsSortDesc(GRID_TOTAL_COLUMNS - 1) As Boolean
            Public bnoRow As Boolean = False

            Sub New()

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
#End Region

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

                State.HasDataChanged = retObj.BoChanged
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                If retObj IsNot Nothing Then

                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            If State.moChkIsCommProdCode = True Then
                                State.moCommPCodeId = retObj.moCommissionPeriodId
                                State.moCommissionPeriodId = Guid.Empty
                            Else
                                State.moCommissionPeriodId = retObj.moCommissionPeriodId
                                State.moCommPCodeId = Guid.Empty
                            End If
                        Case Else
                            State.moCommissionPeriodId = Guid.Empty
                            State.moCommPCodeId = Guid.Empty
                    End Select
                    PopulateDealerDropDown()
                    If State.IsGridVisible Then
                        If State.moChkIsCommProdCode = True Then
                            GridCommPrd.PageIndex = State.mnPageIndex
                            GridCommPrd.PageSize = State.PageSize
                            cboPageSize.SelectedValue = CType(State.PageSize, String)
                            GridCommPrd.PageSize = State.PageSize
                            ControlMgr.SetVisibleControl(Me, trPageSize, GridCommPrd.Visible)
                            chkIsCommProdCode.Checked = True
                            If moDealerMultipleDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                                PopulateProductCode()
                            End If
                        Else
                            Grid.PageIndex = State.mnPageIndex
                            Grid.PageSize = State.PageSize
                            cboPageSize.SelectedValue = CType(State.PageSize, String)
                            Grid.PageSize = State.PageSize
                            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                            chkIsCommProdCode.Checked = False
                        End If
                    End If

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moCommissionPeriodId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oCommissionPeriodId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moCommissionPeriodId = oCommissionPeriodId
                Me.BoChanged = boChanged
            End Sub


        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region

#Region "Variables"

#End Region

#Region "Properties"

        'Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
        '    Get
        '        If multipleDropControl Is Nothing Then
        '            multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
        '        End If
        '        Return multipleDropControl
        '    End Get
        'End Property       

#End Region

#Region "Handlers"


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

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            Try
                If Not IsPostBack Then
                    ' Set Master Page Header
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")

                    ' Update Bread Crum
                    UpdateBreadCrum()

                    SortDirection = CommissionPeriod.CommissionPeriodSearchDV.COL_DEALER_NAME
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    TranslateGridHeader(GridCommPrd)
                    TranslateGridControls(GridCommPrd)
                    If Not IsReturningFromChild Then
                        ' It is The First Time
                        ' It is not Returning from Detail
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        PopulateDealerDropDown()
                    Else
                        ' It is returning from detail
                        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
                        ControlMgr.SetVisibleControl(Me, trPageSize, State.IsGridVisible)
                        If State.IsGridVisible Then
                            PopulateGrid()
                        End If
                    End If
                    SetGridItemStyleColor(Grid)
                    SetGridItemStyleColor(GridCommPrd)
                    If chkIsCommProdCode.Checked Then
                        ControlMgr.SetVisibleControl(Me, moProdCodePanel_WRITE, True)
                    Else
                        ControlMgr.SetVisibleControl(Me, moProdCodePanel_WRITE, False)
                    End If
                End If
                If IsReturningFromChild = True Then
                    IsReturningFromChild = False
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub BtnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
            Try
                ClearAll()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                Grid.PageIndex = NO_PAGE_INDEX
                GridCommPrd.PageIndex = NO_PAGE_INDEX
                State.searchDV = Nothing
                State.searchBtnClicked = True
                State.moDealerId = moDealerMultipleDrop.SelectedGuid ' Me.GetSelectedItem(moDealerDrop)
                State.HasDataChanged = False
                State.IsGridVisible = True
                PopulateGrid()
                State.searchBtnClicked = False
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew.Click
            Try
                State.moDealerId = moDealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(moDealerDrop)
                If chkIsCommProdCode.Checked Then
                    State.moCommPCodeId = Guid.Empty
                    State.moChkIsCommProdCode = True
                    State.mnPageIndex = GridCommPrd.PageIndex
                    State.moProductCodeId = GetSelectedItem(moProductDrop)
                    callPage(CommPCodeForm.URL, State.moCommPCodeId)
                Else
                    State.moCommissionPeriodId = Guid.Empty
                    State.moChkIsCommProdCode = False
                    State.mnPageIndex = Grid.PageIndex
                    State.moProductCodeId = Guid.Empty
                    callPage(CommissionPeriodForm.URL, State.moCommissionPeriodId)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If dvRow IsNot Nothing And Not State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Row.Cells(COMMISSION_PERIOD_ID).Text = GetGuidStringFromByteArray(CType(dvRow(CommissionPeriod.CommissionPeriodSearchDV.COL_COMMISSION_PERIOD_ID), Byte()))
                    e.Row.Cells(GRID_COL_COMPANY_CODE_IDX).Text = dvRow(CommissionPeriod.CommissionPeriodSearchDV.COL_COMPANY_CODE).ToString
                    e.Row.Cells(GRID_COL_DEALER_IDX).Text = dvRow(CommissionPeriod.CommissionPeriodSearchDV.COL_DEALER_NAME).ToString
                    e.Row.Cells(GRID_COL_EFFECTIVE_IDX).Text = GetDateFormattedString(CType(dvRow(CommissionPeriod.CommissionPeriodSearchDV.COL_EFFECTIVE_DATE), Date))
                    e.Row.Cells(GRID_COL_EXPIRATION_IDX).Text = GetDateFormattedString(CType(dvRow(CommissionPeriod.CommissionPeriodSearchDV.COL_EXPIRATION_DATE), Date))
                End If
            End If
        End Sub
        Private Sub Grid_PageIndexChanged(source As System.Object,
            e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try

                State.mnPageIndex = e.NewPageIndex
                Grid.PageIndex = e.NewPageIndex
                State.moCommissionPeriodId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try

                If e.CommandName = SELECT_COMMAND_NAME Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    'Me.State.moCommissionPeriodId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.COMMISSION_PERIOD_ID).FindControl(Me.GRID_CTRL_NAME_COMM_PERIOD_ID), Label).Text)
                    State.moCommissionPeriodId = New Guid(Grid.Rows(index).Cells(COMMISSION_PERIOD_ID).Text)

                    State.mnPageIndex = Grid.PageIndex
                    State.moDealerId = moDealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(moDealerDrop)
                    callPage(CommissionPeriodForm.URL, State.moCommissionPeriodId)

                End If
            Catch ex As Threading.ThreadAbortException
                '  System.Threading.Thread.Sleep(500)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
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

                State.mnPageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GridCommPrd_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridCommPrd.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If dvRow IsNot Nothing And Not State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Row.Cells(COMM_P_CODE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(CommissionPeriod.CommPrdPeriodSearchDV.COL_COMM_P_CODE_ID), Byte()))
                    e.Row.Cells(GRID_COL_COMPANY_CODE_IDX).Text = dvRow(CommissionPeriod.CommPrdPeriodSearchDV.COL_COMPANY_CODE).ToString
                    e.Row.Cells(GRID_COL_DEALER_IDX).Text = dvRow(CommissionPeriod.CommPrdPeriodSearchDV.COL_DEALER_NAME).ToString
                    e.Row.Cells(GRID_COL_PRODUCT_IDX).Text = dvRow(CommissionPeriod.CommPrdPeriodSearchDV.COL_PRODUCT_CODE).ToString
                    e.Row.Cells(GRID_COL_CM_EFFECTIVE_IDX).Text = GetDateFormattedString(CType(dvRow(CommissionPeriod.CommPrdPeriodSearchDV.COL_EFFECTIVE_DATE), Date))
                    e.Row.Cells(GRID_COL_CM_EXPIRATION_IDX).Text = GetDateFormattedString(CType(dvRow(CommissionPeriod.CommPrdPeriodSearchDV.COL_EXPIRATION_DATE), Date))
                End If
            End If
        End Sub
        Private Sub GridCommPrd_PageIndexChanged(source As System.Object,
             e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridCommPrd.PageIndexChanging
            Try
                State.mnPageIndex = e.NewPageIndex
                GridCommPrd.PageIndex = e.NewPageIndex
                State.moCommPCodeId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Public Sub GridCommPrd_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridCommPrd.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GridCommPrd_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridCommPrd.RowCommand
            Try

                If e.CommandName = SELECT_COMMAND_NAME Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    State.moCommPCodeId = New Guid(GridCommPrd.Rows(index).Cells(COMM_P_CODE_ID).Text)

                    State.mnPageIndex = Grid.PageIndex
                    State.moDealerId = moDealerMultipleDrop.SelectedGuid
                    State.moProductCodeId = GetSelectedItem(moProductDrop)
                    callPage(CommPCodeForm.URL, State.moCommPCodeId)

                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GridCommPrd_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridCommPrd.Sorting
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

                State.mnPageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#End Region

#Region "Clear"

        Private Sub ClearAll()

            moDealerMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED
            If chkIsCommProdCode.Checked Then
                ClearList(moProductDrop)
            End If
        End Sub

#End Region

#Region "Populate"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End Sub

        Sub PopulateDealerDropDown()
            Try
                Dim oDataView As DataView
                If chkIsCommProdCode.Checked Then
                    oDataView = LookupListNew.GetDealersCommPrdLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                Else
                    oDataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                End If
                moDealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
                moDealerMultipleDrop.NothingSelected = True

                moDealerMultipleDrop.BindData(oDataView)
                moDealerMultipleDrop.AutoPostBackDD = True
                moDealerMultipleDrop.SelectedGuid = State.moDealerId

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateProductCode()
            Dim oDealerId As Guid = moDealerMultipleDrop.SelectedGuid
            Try
                Dim oListContext As New ListContext
                oListContext.DealerId = oDealerId
                Dim productListForDealer As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.ProductCodeByDealer, context:=oListContext)
                moProductDrop.Populate(productListForDealer, New PopulateOptions() With
                {
                    .TextFunc = AddressOf .GetCode,
                    .AddBlankItem = True,
                    .SortFunc = AddressOf .GetCode
                })

                'Me.BindListControlToDataView(moProductDrop, LookupListNew.GetProductCodeLookupList(oDealerId), "CODE", , True)
                BindSelectItem(State.moProductCodeId.ToString, moProductDrop)
                ControlMgr.SetEnableControl(Me, moProductDrop, True)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function GetSearchParm() As CommissionPeriodData
            Dim oCommissionPeriod As CommissionPeriodData = New CommissionPeriodData

            With oCommissionPeriod
                .companyIds = ElitaPlusIdentity.Current.ActiveUser.Companies
                .dealerId = State.moDealerId
            End With

            Return oCommissionPeriod

        End Function

        Private Function GetCommPrdSearchParm() As CommPrdData
            Dim oCommPrdData As CommPrdData = New CommPrdData

            With oCommPrdData
                .companyIds = ElitaPlusIdentity.Current.ActiveUser.Companies
                .ProductCodeId = State.moProductCodeId
                .dealerId = State.moDealerId
            End With

            Return oCommPrdData

        End Function

        Private Sub PopulateGrid()
            Try
                State.moDealerId = moDealerMultipleDrop.SelectedGuid
                If chkIsCommProdCode.Checked Then
                    State.moChkIsCommProdCode = True
                    State.moProductCodeId = GetSelectedItem(moProductDrop)
                    'If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                    State.searchDV = CommissionPeriod.getCommPrdList(GetCommPrdSearchParm)
                    'End If
                Else
                    State.moProductCodeId = Guid.Empty
                    State.moChkIsCommProdCode = False
                    'If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                    State.searchDV = CommissionPeriod.getList(GetSearchParm)
                    'End If

                End If

                State.searchDV.Sort = SortDirection
                If chkIsCommProdCode.Checked Then
                    GridCommPrd.AutoGenerateColumns = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.moCommPCodeId, GridCommPrd, State.mnPageIndex)
                Else
                    Grid.AutoGenerateColumns = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.moCommissionPeriodId, Grid, State.mnPageIndex)
                End If

                SortAndBindGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SortAndBindGrid()
            If chkIsCommProdCode.Checked Then
                State.mnPageIndex = GridCommPrd.PageIndex
                If (State.searchDV.Count = 0) Then
                    State.bnoRow = True
                    CreateHeaderForEmptyGrid(GridCommPrd, SortDirection)
                Else
                    State.bnoRow = False
                    GridCommPrd.Enabled = True
                    GridCommPrd.DataSource = State.searchDV
                    HighLightSortColumn(GridCommPrd, SortDirection)
                    GridCommPrd.DataBind()
                End If

                If Not GridCommPrd.BottomPagerRow.Visible Then GridCommPrd.BottomPagerRow.Visible = True

                ControlMgr.SetVisibleControl(Me, GridCommPrd, State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, Grid, Not State.IsGridVisible)

                ControlMgr.SetVisibleControl(Me, trPageSize, GridCommPrd.Visible)

                Session("recCount") = State.searchDV.Count

                If State.searchDV.Count > 0 Then

                    If GridCommPrd.Visible Then
                        lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                    End If
                Else
                    If GridCommPrd.Visible Then
                        lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                    End If
                End If
            Else
                State.mnPageIndex = Grid.PageIndex
                If (State.searchDV.Count = 0) Then
                    State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, SortDirection)
                Else
                    State.bnoRow = False
                    Grid.Enabled = True
                    Grid.DataSource = State.searchDV
                    HighLightSortColumn(Grid, SortDirection)
                    Grid.DataBind()
                End If

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
                ControlMgr.SetVisibleControl(Me, GridCommPrd, Not State.IsGridVisible)
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
            End If
        End Sub
        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If chkIsCommProdCode.Checked Then
                    GridCommPrd.PageIndex = NewCurrentPageIndex(GridCommPrd, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    State.PageSize = GridCommPrd.PageSize
                Else
                    Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    State.PageSize = Grid.PageSize
                End If
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub chkIsCommProdCode_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsCommProdCode.CheckedChanged
            If chkIsCommProdCode.Checked Then
                ControlMgr.SetVisibleControl(Me, moProdCodePanel_WRITE, True)
            Else
                ControlMgr.SetVisibleControl(Me, moProdCodePanel_WRITE, False)
            End If
            ControlMgr.SetVisibleControl(Me, GridCommPrd, False)
            ControlMgr.SetVisibleControl(Me, Grid, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)

            State.moDealerId = Guid.Empty
            PopulateDealerDropDown()
            ClearList(moProductDrop)
        End Sub
        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
           Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                State.moDealerId = moDealerMultipleDrop.SelectedGuid
                If moDealerMultipleDrop.SelectedIndex > NO_ITEM_SELECTED_INDEX And chkIsCommProdCode.Checked Then
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
    End Class


End Namespace