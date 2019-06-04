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
#End Region

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

                Me.State.HasDataChanged = retObj.BoChanged
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                If Not retObj Is Nothing Then

                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.moCompensationPlanId = retObj.moCompensationPlanId
                        Case Else
                            Me.State.moCompensationPlanId = Guid.Empty
                    End Select
                    Me.PopulateDealerDropDown()
                    If Me.State.IsGridVisible Then
                        Grid.PageIndex = Me.State.mnPageIndex
                        Grid.PageSize = Me.State.PageSize
                        cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                        Grid.PageSize = Me.State.PageSize
                        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moCompensationPlanId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal oCompensationPlanId As Guid, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.moCompensationPlanId = oCompensationPlanId
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    ' Set Master Page Header
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    ' Update Bread Crum
                    UpdateBreadCrum()

                    Me.SortDirection = CompensationPlan.CompensationPlanSearchDV.COL_DEALER_NAME
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                    If Not Me.IsReturningFromChild Then
                        ' It is The First Time
                        ' It is not Returning from Detail
                        ControlMgr.SetVisibleControl(Me, Me.trPageSize, False)
                        PopulateDealerDropDown()
                    Else
                        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
                        ControlMgr.SetVisibleControl(Me, trPageSize, Me.State.IsGridVisible)
                        If Me.State.IsGridVisible Then
                            Me.PopulateGrid()
                        End If
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)
                End If
                If Me.IsReturningFromChild = True Then
                    Me.IsReturningFromChild = False
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub BtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
            Try
                ClearAll()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Grid.PageIndex = Me.NO_PAGE_INDEX
                Me.State.SortExpression = CompensationPlanDAL.COL_NAME_DEALER_NAME
                Me.State.searchDV = Nothing
                Me.State.searchBtnClicked = True
                Me.State.moDealerId = moDealerMultipleDrop.SelectedGuid ' Me.GetSelectedItem(moDealerDrop)
                Me.State.HasDataChanged = False
                Me.State.IsGridVisible = True
                PopulateGrid()
                Me.State.searchBtnClicked = False
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
            Try
                Me.State.moDealerId = moDealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(moDealerDrop)
                Me.State.moCompensationPlanId = Guid.Empty
                Me.State.mnPageIndex = Grid.PageIndex
                Me.callPage(CompensationPlanForm.URL, Me.State.moCompensationPlanId)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Row.Cells(Me.COMMISSION_PLAN_ID).Text = GetGuidStringFromByteArray(CType(dvRow(CompensationPlan.CompensationPlanSearchDV.COL_COMMISSION_PLAN_ID), Byte()))
                    e.Row.Cells(Me.GRID_COL_COMPANY_CODE_IDX).Text = dvRow(CompensationPlan.CompensationPlanSearchDV.COL_COMPANY_CODE).ToString
                    e.Row.Cells(Me.GRID_COL_DEALER_IDX).Text = dvRow(CompensationPlan.CompensationPlanSearchDV.COL_DEALER_NAME).ToString
                    e.Row.Cells(Me.GRID_COL_CODE_IDX).Text = dvRow(CompensationPlan.CompensationPlanSearchDV.COL_CODE).ToString
                    e.Row.Cells(Me.GRID_COL_EFFECTIVE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CompensationPlan.CompensationPlanSearchDV.COL_EFFECTIVE_DATE), Date))
                    e.Row.Cells(Me.GRID_COL_EXPIRATION_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CompensationPlan.CompensationPlanSearchDV.COL_EXPIRATION_DATE), Date))
                End If
            End If
        End Sub
        Private Sub Grid_PageIndexChanged(ByVal source As System.Object,
            ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try

                Me.State.mnPageIndex = e.NewPageIndex
                Grid.PageIndex = e.NewPageIndex
                Me.State.moCompensationPlanId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = Me.SELECT_COMMAND_NAME Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Me.State.moCompensationPlanId = New Guid(Me.Grid.Rows(index).Cells(Me.COMMISSION_PLAN_ID).Text)
                    Me.State.mnPageIndex = Grid.PageIndex
                    Me.State.moDealerId = moDealerMultipleDrop.SelectedGuid
                    Me.callPage(CompensationPlanForm.URL, Me.State.moCompensationPlanId)

                End If
            Catch ex As Threading.ThreadAbortException
                '  System.Threading.Thread.Sleep(500)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
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
                Me.State.mnPageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearAll()

            moDealerMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
        End Sub

#End Region

#Region "Populate"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("COMPENSATION_PLAN")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COMPENSATION_PLAN")
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
                moDealerMultipleDrop.SelectedGuid = Me.State.moDealerId

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Private Function GetSearchParm() As CompensationPlanData
            Dim oCompensationPlan As CompensationPlanData = New CompensationPlanData
            Dim oDataView As DataView

            With oCompensationPlan
                .companyIds = ElitaPlusIdentity.Current.ActiveUser.Companies
                .dealerId = Me.State.moDealerId
                'oDataView = CommissionPeriod.LoadList(oCommissionPeriod)
            End With

            Return oCompensationPlan

        End Function



        Private Sub PopulateGrid() '(Optional isLoaded As Boolean = False)
            Dim oDataView As DataView

            Try
                Me.State.moDealerId = moDealerMultipleDrop.SelectedGuid
                'If isLoaded = False Then
                Me.State.searchDV = CompensationPlan.getList(GetSearchParm)
                Me.State.searchDV.Sort = Me.SortDirection
                ' Else
                'Me.SortDirection = Me.State.searchDV.Sort
                'End If
                Me.Grid.AutoGenerateColumns = False
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.moCompensationPlanId, Me.Grid, Me.State.mnPageIndex)
                Me.SortAndBindGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SortAndBindGrid()

            Me.State.mnPageIndex = Me.Grid.PageIndex
            If (Me.State.searchDV.Count = 0) Then
                    Me.State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
                Else
                    Me.State.bnoRow = False
                    Me.Grid.Enabled = True
                    Me.Grid.DataSource = Me.State.searchDV
                    HighLightSortColumn(Grid, Me.SortDirection)
                    Me.Grid.DataBind()
                End If

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
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
        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try

                Me.Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageSize = Grid.PageSize
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region
    End Class


End Namespace