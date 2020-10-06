Imports System.Globalization
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms


Public Class ReconciliationForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const GRID_COL_NAME_BILLING_DATE As Integer = 0
    'Public Const GRID_COL_NAME_DISCREPANCY As Integer = 1
    Public Const GRID_COL_NAME_SOC_TYPE As Integer = 1
    Public Const GRID_COL_NAME_ACCOUNT_STATUS As Integer = 2
    Public Const GRID_COL_NAME_BILLABLE_COUNT As Integer = 3
    Public Const GRID_COL_NAME_CARRIER_COUNT As Integer = 4
    Public Const GRID_COL_NAME_BILLABLE_AMOUNT As Integer = 5
    Public Const GRID_COL_NAME_CARRIER_AMOUNT As Integer = 6

    '----
    Public Const GRID_COL_NAME_EPRISM_COUNT As Integer = 2
    Public Const GRID_COL_NAME_BI_COUNT As Integer = 3

    Public Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Page State"

    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public searchActiveDV As Reconciliation.ReconciliationDV
        'Public searchResumeDV As Reconciliation.ReconciliationDV
        Public SearchClicked As Boolean = False
        Public IsGridVisible As Boolean = False
        Public IsShowDiscrepChecked As Boolean = False
        Public DealerIdInSearch As Guid = Guid.Empty
        Public DealerObjInSearch As Dealer
        Public firstDayOfMonth As String
        Public lastDayOfMonth As String
        Public PHPGridSortExpression As String = Reconciliation.ReconciliationDV.COL_NAME_BILLING_DATE & " ASC" & " ," & Reconciliation.ReconciliationDV.COL_NAME_SOC_TYPE & " DESC" & " ," & Reconciliation.ReconciliationDV.COL_NAME_ACCOUNT_STATUS & " ASC"
        'Public MHPGridSortExpression As String = Reconciliation.ReconciliationDV.COL_NAME_BILLING_DATE & " ASC" & " ," & Reconciliation.ReconciliationDV.COL_NAME_SOC_TYPE & " DESC" & " ," & Reconciliation.ReconciliationDV.COL_NAME_ACCOUNT_STATUS & " DESC"

        'order by billing_date asc,
        'decode(soc_type, 'NY Subscribers', 1, 'Non-NY Subscribers', 2, 5),
        'decode(account_status,'Enrolled', 1, 'Billable/Enrolled', 1,'DeEnrolled', 2,'Sub Total',3,'Buyers Remorse Count',4,'Deenrolled TAC Code Adjustment',5,6)

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


    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage("BILLING_RECONCILIATION")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BILLING_RECONCILIATION")
            End If
        End If
    End Sub

    'Private Sub ReconciliationForm_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
    '    If Not Me.State.DealerObjInSearch Is Nothing Then
    '        If Me.State.DealerObjInSearch.Dealer = "BLGS" Then
    '            Grid.Columns(Me.GRID_COL_NAME_BILLABLE_COUNT).HeaderText = "ePrism Count"
    '            Grid.Columns(Me.GRID_COL_NAME_CARRIER_COUNT).HeaderText = "BI Count"
    '        End If
    '    End If
    'End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()
        Try
            If Not IsPostBack Then

                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("FINANCE_AUTOMATION")
                UpdateBreadCrum()
                TranslateGridHeader(Grid)
                TranslateGridHeader(GridViewMHP)
                TranslateGridHeader(GridViewBLGS)
                BtnOverRideRecon.Enabled = False
                divDataContainer.Visible = False
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                'cboResPageSize.SelectedValue = CType(Me.State.PageSize, String)
                PopulateDropdowns()

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

    Protected Sub PopulateDropdowns()

        'Me.BindCodeNameToListControl(ddlDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, True, "Code"), , , , True)
        Dim oDealerList = GetDealerListByCompanyForUser()
        Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                           Return li.ExtendedCode + " - " + li.Translation + " " + "(" + li.Code + ")"
                                                                       End Function
        ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc,
                                            .AddBlankItem = True
                                           })
        Dim intYear As Integer = DateTime.Today.Year

        'ddlAcctPeriodYear.Items.Add(New ListItem("", Guid.Empty.ToString))

        For i As Integer = (intYear - 7) To intYear
            ddlAcctPeriodYear.Items.Add(New System.Web.UI.WebControls.ListItem(i.ToString, i.ToString))
        Next
        ddlAcctPeriodYear.SelectedValue = intYear.ToString

        'ddlAcctPeriodMonth.Items.Add(New ListItem("", Guid.Empty.ToString))

        For month As Integer = 1 To 12
            Dim monthName As String = DateTimeFormatInfo.CurrentInfo.GetMonthName(month)
            ddlAcctPeriodMonth.Items.Add(New System.Web.UI.WebControls.ListItem(monthName, month.ToString().PadLeft(2, CChar("0"))))
        Next

        Dim strMonth As String = DateTime.Today.Month.ToString().PadLeft(2, CChar("0"))
        strMonth = strMonth.Substring(strMonth.Length - 2)
        ddlAcctPeriodMonth.SelectedValue = strMonth

    End Sub
    Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Dim Index As Integer
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

        Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

        For Index = 0 To UserCompanies.Count - 1
            'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
            oListContext.CompanyId = UserCompanies(Index)
            Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
            If oDealerListForCompany.Count > 0 Then
                If oDealerList IsNot Nothing Then
                    oDealerList.AddRange(oDealerListForCompany)
                Else
                    oDealerList = oDealerListForCompany.Clone()
                End If

            End If
        Next

        Return oDealerList.ToArray()

    End Function

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            'Me.State.SearchClicked = True
            If GetSelectedItem(ddlDealer) = Guid.Empty Then
                MasterPage.MessageController.AddInformation(Message.MSG_DEALER_REQUIRED)
                Exit Sub
            End If
            State.SearchClicked = True
            State.PageIndex = 0
            State.IsGridVisible = True
            State.searchActiveDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateGrid()
        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            divDataContainer.Visible = True

            If State.DealerObjInSearch.Dealer = "TPHP" Then
                ' First Grid
                If (State.searchActiveDV Is Nothing) Then
                    State.searchActiveDV = Reconciliation.GetPHPReconData(State.DealerIdInSearch, _
                                                State.firstDayOfMonth, State.lastDayOfMonth, State.IsShowDiscrepChecked)
                    If State.SearchClicked Then
                        ValidSearchResultCountNew(State.searchActiveDV.Count, True)
                        State.SearchClicked = False
                    End If
                End If

                Grid.PageSize = State.PageSize

                If State.searchActiveDV.Count = 0 Then
                    BtnOverRideRecon.Enabled = False
                    ControlMgr.SetVisibleControl(Me, Grid, False)
                Else
                    Grid.DataSource = State.searchActiveDV
                    ControlMgr.SetVisibleControl(Me, Grid, True)
                    BtnOverRideRecon.Enabled = True
                End If

                State.PageIndex = Grid.PageIndex

                If (Not State.PHPGridSortExpression.Equals(String.Empty)) Then
                    State.searchActiveDV.Sort = State.PHPGridSortExpression
                End If
                HighLightSortColumn(Grid, State.PHPGridSortExpression, True)

                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, GridViewMHP, False) ' Make sure that MHP grid is not visible when populating PHP Grid
                ControlMgr.SetVisibleControl(Me, GridViewBLGS, False) ' Make sure that BLGS grid is not visible when populating PHP Grid
                Session("recCount") = State.searchActiveDV.Count

                'If Me.Grid.Visible Then 'DEF-25086
                lblActiveSearchResults.Visible = True
                lblActiveSearchResults.Text = State.searchActiveDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                'End If

            ElseIf State.DealerObjInSearch.Dealer = "TMHP" Then
                ' Second Grid
                If (State.searchActiveDV Is Nothing) Then
                    State.searchActiveDV = Reconciliation.GetMHPReconData(State.DealerIdInSearch, _
                                                State.firstDayOfMonth, State.lastDayOfMonth, State.IsShowDiscrepChecked)
                    If State.SearchClicked Then
                        ValidSearchResultCountNew(State.searchActiveDV.Count, True)
                        State.SearchClicked = False
                    End If
                End If

                GridViewMHP.PageSize = State.PageSize

                If State.searchActiveDV.Count = 0 Then
                    BtnOverRideRecon.Enabled = False
                    ControlMgr.SetVisibleControl(Me, GridViewMHP, False)
                Else
                    GridViewMHP.DataSource = State.searchActiveDV
                    ControlMgr.SetVisibleControl(Me, GridViewMHP, True)
                    BtnOverRideRecon.Enabled = True
                End If

                State.PageIndex = GridViewMHP.PageIndex

                'Disabled the Sorting on MHP Grid as it has only a small number of records and in certain sequence
                'If (Not Me.State.MHPGridSortExpression.Equals(String.Empty)) Then
                '    Me.State.searchActiveDV.Sort = Me.State.MHPGridSortExpression
                'End If
                'HighLightSortColumn(GridViewMHP, Me.State.MHPGridSortExpression, True)

                GridViewMHP.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, False) ' Make sure that PHP grid is not visible when populating MHP Grid
                ControlMgr.SetVisibleControl(Me, GridViewBLGS, False) ' Make sure that BLGS grid is not visible when populating MHP Grid
                Session("recCount") = State.searchActiveDV.Count


                'If Me.GridViewMHP.Visible Then 'DEF-25086
                lblActiveSearchResults.Visible = True
                lblActiveSearchResults.Text = State.searchActiveDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                'End If
            ElseIf State.DealerObjInSearch.Dealer = "BLGS" Then
                ' Third Grid
                If (State.searchActiveDV Is Nothing) Then
                    State.searchActiveDV = Reconciliation.GetPHPReconData(State.DealerIdInSearch, _
                                                State.firstDayOfMonth, State.lastDayOfMonth, State.IsShowDiscrepChecked)
                    If State.SearchClicked Then
                        ValidSearchResultCountNew(State.searchActiveDV.Count, True)
                        State.SearchClicked = False
                    End If
                End If

                GridViewBLGS.PageSize = State.PageSize

                If State.searchActiveDV.Count = 0 Then
                    BtnOverRideRecon.Enabled = False
                    ControlMgr.SetVisibleControl(Me, GridViewBLGS, False)
                Else
                    GridViewBLGS.DataSource = State.searchActiveDV
                    ControlMgr.SetVisibleControl(Me, GridViewBLGS, True)
                    BtnOverRideRecon.Enabled = True
                End If

                State.PageIndex = GridViewBLGS.PageIndex

                'Disabled the Sorting on BLGS Grid as it has only a small number of records

                GridViewBLGS.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, False) ' Make sure that PHP grid is not visible when populating BLGS Grid
                ControlMgr.SetVisibleControl(Me, GridViewMHP, False) ' Make sure that MHP grid is not visible when populating BLGS Grid
                Session("recCount") = State.searchActiveDV.Count


                'If Me.GridViewBLGS.Visible Then 'DEF-25086
                lblActiveSearchResults.Visible = True
                lblActiveSearchResults.Text = State.searchActiveDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                'End If
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim iMonth As Integer
        Dim iYear As Integer
        Try
            If Not State.DealerIdInSearch = Guid.Empty Then
                State.DealerIdInSearch = GetSelectedItem(ddlDealer)
                State.DealerObjInSearch = New Dealer(State.DealerIdInSearch)
                Integer.TryParse(GetSelectedValue(ddlAcctPeriodMonth), iMonth)
                Integer.TryParse(GetSelectedValue(ddlAcctPeriodYear), iYear)
                State.firstDayOfMonth = GetFirstDayOfMonth(iMonth, iYear).ToString(DALObjects.DALBase.DOTNET_QUERY_DATEFORMAT)
                State.lastDayOfMonth = GetLastDayOfMonth(iMonth, iYear).ToString(DALObjects.DALBase.DOTNET_QUERY_DATEFORMAT)

                Return True
            Else
                ControlMgr.SetVisibleControl(Me, GridViewMHP, False)
                ControlMgr.SetVisibleControl(Me, GridViewBLGS, False)
                ControlMgr.SetVisibleControl(Me, Grid, False)
                BtnOverRideRecon.Enabled = False
                Return False
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Function


    Private Sub ddlDealer_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDealer.SelectedIndexChanged
        Try
            ClearReconciliationResults()
            State.DealerIdInSearch = GetSelectedItem(ddlDealer)
            State.DealerObjInSearch = New Dealer(State.DealerIdInSearch)
            Grid.PageIndex = State.PageIndex
            State.searchActiveDV = Nothing
            chkDiscrepOnly.Checked = False
            State.IsShowDiscrepChecked = False
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub chkDiscrepOnly_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkDiscrepOnly.CheckedChanged
        Try
            'State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            'Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchActiveDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            State.searchActiveDV = Nothing
            If chkDiscrepOnly.Checked Then
                State.IsShowDiscrepChecked = True
            Else
                State.IsShowDiscrepChecked = False
            End If
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#Region "Date Functions"


    Private Function GetFirstDayOfMonth(iMonth As Int32, iYear As Int32) As DateTime
        'set return value to the last day of the month for any date passed in to the method
        'create a datetime variable set to the passed in date
        Dim dtFrom As New DateTime(iYear, iMonth, 1)
        'remove all of the days in the month except the first day and set the variable to hold that date
        dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1))
        'return the first day of the month
        Return dtFrom
    End Function

    Private Function GetLastDayOfMonth(iMonth As Int32, iYear As Int32) As DateTime
        'set return value to the last day of the month for any date passed in to the method
        'create a datetime variable set to the passed in date
        Dim dtTo As New DateTime(iYear, iMonth, 1)
        'overshoot the date by a month
        dtTo = dtTo.AddMonths(1)
        'remove all of the days in the next month to get bumped down to the last day of the previous(month)
        dtTo = dtTo.AddDays(-(dtTo.Day))
        'return the last day of the month
        Return dtTo
    End Function

#End Region

#Region "PHP Grid related"

    'Private Sub Grid_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PreRender
    '    If Not Me.State.DealerObjInSearch Is Nothing Then
    '        If Me.State.DealerObjInSearch.Dealer = "BLGS" Then
    '            Grid.Columns(Me.GRID_COL_NAME_BILLABLE_COUNT).HeaderText = "ePrism Count"
    '            Grid.Columns(Me.GRID_COL_NAME_CARRIER_COUNT).HeaderText = "BI Count"
    '        End If
    '    End If
    'End Sub

    Public Sub GridRowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
            'If Not Me.State.DealerObjInSearch Is Nothing Then
            '    If Me.State.DealerObjInSearch.Dealer = "BLGS" Then
            '        Grid.Columns(Me.GRID_COL_NAME_BILLABLE_COUNT).HeaderText = "ePrism Count"
            '        Grid.Columns(Me.GRID_COL_NAME_CARRIER_COUNT).HeaderText = "BI Count"
            '    End If
            'End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim discrepancy As String

        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If (e.Row.RowType = DataControlRowType.Header) Then
                If State.DealerObjInSearch.Dealer = "BLGS" Then
                    Grid.Columns(GRID_COL_NAME_BILLABLE_COUNT).HeaderText = "ePrism Count"
                    Grid.Columns(GRID_COL_NAME_CARRIER_COUNT).HeaderText = "BI Count"
                End If
            End If

            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then

                discrepancy = dvRow(Reconciliation.ReconciliationDV.COL_NAME_DISCREPANCY).ToString
                If discrepancy = "Y" Then
                    e.Row.Cells(GRID_COL_NAME_BILLABLE_COUNT).CssClass = "StatInactive"
                    e.Row.Cells(GRID_COL_NAME_CARRIER_COUNT).CssClass = "StatInactive"
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            'Me.State.selectedPaymentGroupId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If State.PHPGridSortExpression.StartsWith(e.SortExpression) Then
                If State.PHPGridSortExpression.EndsWith(" DESC") Then
                    State.PHPGridSortExpression = e.SortExpression
                Else
                    State.PHPGridSortExpression &= " DESC"
                End If
            Else
                State.PHPGridSortExpression = e.SortExpression
            End If

            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region "MHP Grid related"
    Public Sub GridViewMHPRowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewMHP.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewMHP_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewMHP.RowDataBound
        Dim TotalRows As String
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then

                TotalRows = dvRow(Reconciliation.ReconciliationDV.COL_NAME_ACCOUNT_STATUS).ToString
                If TotalRows.ToUpper.Contains("TOTAL") Then
                    e.Row.Font.Bold = True
                    e.Row.CssClass = "selected"
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewMHP_PageIndexChanged(sender As Object, e As System.EventArgs) Handles GridViewMHP.PageIndexChanged
        Try
            State.PageIndex = GridViewMHP.PageIndex
            'Me.State.selectedPaymentGroupId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewMHP_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewMHP.PageIndexChanging
        Try
            GridViewMHP.PageIndex = e.NewPageIndex
            State.PageIndex = GridViewMHP.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "BLGS Grid related"
    Public Sub GridViewBLGSRowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBLGS.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewBLGS_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBLGS.RowDataBound
        Dim discrepancy As String

        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then

                discrepancy = dvRow(Reconciliation.ReconciliationDV.COL_NAME_DISCREPANCY).ToString
                If discrepancy = "Y" Then
                    e.Row.Cells(GRID_COL_NAME_EPRISM_COUNT).CssClass = "StatInactive"
                    e.Row.Cells(GRID_COL_NAME_BI_COUNT).CssClass = "StatInactive"
                End If

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewBLGS_PageIndexChanged(sender As Object, e As System.EventArgs) Handles GridViewBLGS.PageIndexChanged
        Try
            State.PageIndex = GridViewBLGS.PageIndex
            'Me.State.selectedPaymentGroupId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewBLGS_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewBLGS.PageIndexChanging
        Try
            GridViewBLGS.PageIndex = e.NewPageIndex
            State.PageIndex = GridViewBLGS.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region



    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            If State.DealerObjInSearch.Dealer = "TPHP" Then
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchActiveDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
            ElseIf State.DealerObjInSearch.Dealer = "TMHP" Then
                State.PageIndex = NewCurrentPageIndex(GridViewMHP, State.searchActiveDV.Count, State.PageSize)
                GridViewMHP.PageIndex = State.PageIndex
            ElseIf State.DealerObjInSearch.Dealer = "BLGS" Then
                State.PageIndex = NewCurrentPageIndex(GridViewBLGS, State.searchActiveDV.Count, State.PageSize)
                GridViewBLGS.PageIndex = State.PageIndex
            End If

            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub BtnOverRideRecon_Click(sender As Object, e As System.EventArgs) Handles BtnOverRideRecon.Click
        Dim Result As Boolean
        Try
            Result = Reconciliation.OverRideReconciliation(State.DealerIdInSearch, State.firstDayOfMonth,
                                                           State.lastDayOfMonth, ElitaPlusIdentity.Current.ActiveUser.UserName)

            If Result Then
                MasterPage.MessageController.AddSuccess(Message.MSG_RECON_OVERRIDEN_SUCCESSFULLY, True)
            Else
                MasterPage.MessageController.AddErrorAndShow(Message.MSG_COULD_NOT_OVERRIDE_RECON, True)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        ddlDealer.SelectedIndex = NO_ROW_SELECTED_INDEX
        'Me.ddlAcctPeriodMonth.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
        'Me.ddlAcctPeriodYear.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
        Grid.EditIndex = NO_ITEM_SELECTED_INDEX

        ClearReconciliationResults()
    End Sub

    Private Sub ClearReconciliationResults()
        Try
            With State
                .DealerIdInSearch = Guid.Empty
                .DealerObjInSearch = Nothing
                .firstDayOfMonth = String.Empty
                .lastDayOfMonth = String.Empty
                .IsShowDiscrepChecked = False
            End With
            State.searchActiveDV = Nothing
            chkDiscrepOnly.Checked = False
            State.IsShowDiscrepChecked = False
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


End Class