Imports Assurant.ElitaPlus.DALObjects

Namespace Tables

    Partial Class CompensationPlanSearchForm
        Inherits ElitaPlusSearchPage
        ' Inherits System.Web.UI.Page

#Region "Constants"

        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const COMMISSION_PLAN_ID As Integer = 1
        Public Const GRID_COL_COMPANY_CODE_IDX As Integer = 2
        Public Const GRID_COL_DEALER_IDX As Integer = 3
        Public Const GRID_COL_CODE_IDX As Integer = 4
        Public Const GRID_COL_EFFECTIVE_IDX As Integer = 5
        Public Const GRID_COL_EXPIRATION_IDX As Integer = 6
        Public Const GRID_TOTAL_COLUMNS As Integer = 7
        Private Const GRID_CTRL_NAME_COMM_PLAN_ID As String = "moCompensationPlanId_NO_TRANSLATE"
        Private Const LABEL_DEALER As String = "DEALER"

#End Region


#Region "Page State"

#Region "MyState"


        Class MyState
            Public moCompensationPlanId As Guid = Guid.Empty
            Public moDealerId As Guid = Guid.Empty
            Public mnPageIndex As Integer
            Public searchDV As DataView = Nothing
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean = False
            Public searchBtnClicked As Boolean = False
            Public SortExpression As String = CompensationPlanDAL.COL_NAME_CODE


            ' these variables are used to store the sorting columns information.
            Public SortColumns(GRID_TOTAL_COLUMNS - 1) As String
            Public IsSortDesc(GRID_TOTAL_COLUMNS - 1) As Boolean
            Public bnoRow As Boolean = False

            Sub New()

            End Sub

            ' this will be called before the populate list to get the correct sort order
            Public ReadOnly Property CurrentSortExpresion1() As String
                Get
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
                            State.moCompensationPlanId = retObj.moCompensationPlanId
                        Case Else
                            State.moCompensationPlanId = Guid.Empty
                    End Select
                    PopulateDealerDropDown()
                    If State.IsGridVisible Then
                        Grid.PageIndex = State.mnPageIndex
                        Grid.PageSize = State.PageSize
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moCompensationPlanId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oCompensationPlanId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moCompensationPlanId = oCompensationPlanId
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

                    SortDirection = CompensationPlan.CompensationPlanSearchDV.COL_DEALER_NAME
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    If Not IsReturningFromChild Then
                        ' It is The First Time
                        ' It is not Returning from Detail
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        PopulateDealerDropDown()
                    Else
                        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
                        ControlMgr.SetVisibleControl(Me, trPageSize, State.IsGridVisible)
                        If State.IsGridVisible Then
                            PopulateGrid()
                        End If
                    End If
                    SetGridItemStyleColor(Grid)
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
                State.SortExpression = CompensationPlanDAL.COL_NAME_DEALER_NAME
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
                State.moCompensationPlanId = Guid.Empty
                State.mnPageIndex = Grid.PageIndex
                callPage(CompensationPlanForm.URL, State.moCompensationPlanId)

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
                    e.Row.Cells(COMMISSION_PLAN_ID).Text = GetGuidStringFromByteArray(CType(dvRow(CompensationPlan.CompensationPlanSearchDV.COL_COMMISSION_PLAN_ID), Byte()))
                    e.Row.Cells(GRID_COL_COMPANY_CODE_IDX).Text = dvRow(CompensationPlan.CompensationPlanSearchDV.COL_COMPANY_CODE).ToString
                    e.Row.Cells(GRID_COL_DEALER_IDX).Text = dvRow(CompensationPlan.CompensationPlanSearchDV.COL_DEALER_NAME).ToString
                    e.Row.Cells(GRID_COL_CODE_IDX).Text = dvRow(CompensationPlan.CompensationPlanSearchDV.COL_CODE).ToString
                    e.Row.Cells(GRID_COL_EFFECTIVE_IDX).Text = GetDateFormattedString(CType(dvRow(CompensationPlan.CompensationPlanSearchDV.COL_EFFECTIVE_DATE), Date))
                    e.Row.Cells(GRID_COL_EXPIRATION_IDX).Text = GetDateFormattedString(CType(dvRow(CompensationPlan.CompensationPlanSearchDV.COL_EXPIRATION_DATE), Date))
                End If
            End If
        End Sub
        Private Sub Grid_PageIndexChanged(source As System.Object,
            e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try

                State.mnPageIndex = e.NewPageIndex
                Grid.PageIndex = e.NewPageIndex
                State.moCompensationPlanId = Guid.Empty
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
                    State.moCompensationPlanId = New Guid(Grid.Rows(index).Cells(COMMISSION_PLAN_ID).Text)
                    State.mnPageIndex = Grid.PageIndex
                    State.moDealerId = moDealerMultipleDrop.SelectedGuid
                    callPage(CompensationPlanForm.URL, State.moCompensationPlanId)

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

#End Region

#End Region

#Region "Clear"

        Private Sub ClearAll()

            moDealerMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED
        End Sub

#End Region

#Region "Populate"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("COMPENSATION_PLAN")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COMPENSATION_PLAN")
            End If
        End Sub

        Sub PopulateDealerDropDown()
            Try
                Dim oDataView As DataView
                oDataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                moDealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
                moDealerMultipleDrop.NothingSelected = True
                moDealerMultipleDrop.BindData(oDataView)
                moDealerMultipleDrop.AutoPostBackDD = True
                moDealerMultipleDrop.SelectedGuid = State.moDealerId

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Function GetSearchParm() As CompensationPlanData
            Dim oCompensationPlan As CompensationPlanData = New CompensationPlanData
            Dim oDataView As DataView

            With oCompensationPlan
                .companyIds = ElitaPlusIdentity.Current.ActiveUser.Companies
                .dealerId = State.moDealerId
                'oDataView = CommissionPeriod.LoadList(oCommissionPeriod)
            End With

            Return oCompensationPlan

        End Function



        Private Sub PopulateGrid() '(Optional isLoaded As Boolean = False)
            Dim oDataView As DataView

            Try
                State.moDealerId = moDealerMultipleDrop.SelectedGuid
                'If isLoaded = False Then
                State.searchDV = CompensationPlan.getList(GetSearchParm)
                State.searchDV.Sort = SortDirection
                ' Else
                'Me.SortDirection = Me.State.searchDV.Sort
                'End If
                Grid.AutoGenerateColumns = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.moCompensationPlanId, Grid, State.mnPageIndex)
                SortAndBindGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SortAndBindGrid()

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
        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try

                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageSize = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


#End Region
    End Class


End Namespace