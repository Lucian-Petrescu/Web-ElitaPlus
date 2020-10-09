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
    Public Class BillingCycleListForm
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
        Public Const GRID_COL_DEALER_IDX As Integer = 1
        Public Const GRID_COL_BILLING_CYCLE_CODE_IDX As Integer = 2
        Public Const GRID_COL_START_DAY As Integer = 3
        Public Const GRID_COL_END_DAY As Integer = 4
        Public Const GRID_COL_RUN_DATE_OFFSET As Integer = 5
        Public Const GRID_COL_BILLING_CYCLE_ID_IDX As Integer = 6

        Public Const GRID_TOTAL_COLUMNS As Integer = 7
        Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
        Private Const BILLINGCYCLELISTFORM As String = "BillingCycleListForm.aspx"
        Public Const LABEL_SELECT_DEALERCODE As String = "DEALER"

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        ' This class keeps the current state for the search page.
        Class MyState
            Public PageIndex As Integer = 0
            Public BillingCycleId As Guid = Guid.Empty
            Public IsGridVisible As Boolean
            Public SearchDV As BillingCycle.BillingCycleSearchDV = Nothing
            Public SelectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public SortExpression As String = BillingCycle.BillingCycleSearchDV.COL_NAME_DEALER_NAME
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
                        If Not (State.SelectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) OrElse Not (State.SelectedPageSize = Grid.PageSize) Then
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
                Dim retObj As BillingCycleForm.ReturnType = CType(ReturnPar, BillingCycleForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            State.BillingCycleId = retObj.moBillingCycleId
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                        State.BillingCycleId = Guid.Empty
                    Case Else
                        State.BillingCycleId = Guid.Empty
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
                State.SearchDV = BillingCycle.GetList(moDealerMultipleDrop.SelectedGuid, moBillingCycleCodeText.Text.ToUpper)
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

            Grid.Columns(GRID_COL_BILLING_CYCLE_CODE_IDX).SortExpression = BillingCycle.BillingCycleSearchDV.COL_NAME_BILLING_CYCLE_CODE
            Grid.Columns(GRID_COL_DEALER_IDX).SortExpression = BillingCycle.BillingCycleSearchDV.COL_NAME_DEALER_NAME
            Grid.Columns(GRID_COL_START_DAY).SortExpression = BillingCycle.BillingCycleSearchDV.COL_NAME_START_DAY
            Grid.Columns(GRID_COL_END_DAY).SortExpression = BillingCycle.BillingCycleSearchDV.COL_NAME_END_DAY
            Grid.Columns(GRID_COL_RUN_DATE_OFFSET).SortExpression = BillingCycle.BillingCycleSearchDV.COL_NAME_BILLING_RUN_DATE_OFFSET_DAYS
            HighLightSortColumn(Grid, State.SortExpression)

            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.BillingCycleId, Grid, State.PageIndex)

            Grid.DataSource = State.SearchDV
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.SearchDV.Count

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End Sub

        Private Sub PopulateDropdown()
            moDealerMultipleDrop.AutoPostBackDD = False
            moDealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
            moDealerMultipleDrop.NothingSelected = True
            moDealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
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
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("BILLING_CYCLE")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BILLING_CYCLE")
                End If
            End If
        End Sub

        Private Sub ClearSearchCriteria()
            Try
                moDealerMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED
                moBillingCycleCodeText.Text = String.Empty
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

                If dvRow IsNot Nothing AndAlso Not State.bNoRow Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_BILLING_CYCLE_ID_IDX), dvRow(BillingCycle.BillingCycleSearchDV.COL_NAME_BILLING_CYCLE_ID))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_BILLING_CYCLE_CODE_IDX), dvRow(BillingCycle.BillingCycleSearchDV.COL_NAME_BILLING_CYCLE_CODE))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DEALER_IDX), dvRow(BillingCycle.BillingCycleSearchDV.COL_NAME_DEALER_NAME))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_START_DAY), dvRow(BillingCycle.BillingCycleSearchDV.COL_NAME_START_DAY))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_END_DAY), dvRow(BillingCycle.BillingCycleSearchDV.COL_NAME_END_DAY))
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_RUN_DATE_OFFSET), dvRow(BillingCycle.BillingCycleSearchDV.COL_NAME_BILLING_RUN_DATE_OFFSET_DAYS))
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
                    State.BillingCycleId = New Guid(Grid.Rows(index).Cells(GRID_COL_BILLING_CYCLE_ID_IDX).Text)
                    callPage(BillingCycleForm.URL, State.BillingCycleId)
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
                State.BillingCycleId = Guid.Empty
                callPage(BillingCycleForm.URL, State.BillingCycleId)
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
