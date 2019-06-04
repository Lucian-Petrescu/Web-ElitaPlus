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
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage("BILLING_RECONCILIATION")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BILLING_RECONCILIATION")
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Try
            If Not Me.IsPostBack Then

                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("FINANCE_AUTOMATION")
                UpdateBreadCrum()
                TranslateGridHeader(Grid)
                TranslateGridHeader(GridViewMHP)
                TranslateGridHeader(GridViewBLGS)
                BtnOverRideRecon.Enabled = False
                divDataContainer.Visible = False
                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                'cboResPageSize.SelectedValue = CType(Me.State.PageSize, String)
                PopulateDropdowns()

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                If Not oDealerList Is Nothing Then
                    oDealerList.AddRange(oDealerListForCompany)
                Else
                    oDealerList = oDealerListForCompany.Clone()
                End If

            End If
        Next

        Return oDealerList.ToArray()

    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            'Me.State.SearchClicked = True
            If Me.GetSelectedItem(ddlDealer) = Guid.Empty Then
                Me.MasterPage.MessageController.AddInformation(Message.MSG_DEALER_REQUIRED)
                Exit Sub
            End If
            Me.State.SearchClicked = True
            Me.State.PageIndex = 0
            Me.State.IsGridVisible = True
            Me.State.searchActiveDV = Nothing
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateGrid()
        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            divDataContainer.Visible = True

            If Me.State.DealerObjInSearch.Dealer = "TPHP" Then
                ' First Grid
                If (Me.State.searchActiveDV Is Nothing) Then
                    Me.State.searchActiveDV = Reconciliation.GetPHPReconData(Me.State.DealerIdInSearch, _
                                                Me.State.firstDayOfMonth, Me.State.lastDayOfMonth, Me.State.IsShowDiscrepChecked)
                    If Me.State.SearchClicked Then
                        Me.ValidSearchResultCountNew(Me.State.searchActiveDV.Count, True)
                        Me.State.SearchClicked = False
                    End If
                End If

                Grid.PageSize = State.PageSize

                If State.searchActiveDV.Count = 0 Then
                    BtnOverRideRecon.Enabled = False
                    ControlMgr.SetVisibleControl(Me, Grid, False)
                Else
                    Me.Grid.DataSource = Me.State.searchActiveDV
                    ControlMgr.SetVisibleControl(Me, Grid, True)
                    BtnOverRideRecon.Enabled = True
                End If

                Me.State.PageIndex = Me.Grid.PageIndex

                If (Not Me.State.PHPGridSortExpression.Equals(String.Empty)) Then
                    Me.State.searchActiveDV.Sort = Me.State.PHPGridSortExpression
                End If
                HighLightSortColumn(Grid, Me.State.PHPGridSortExpression, True)

                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, GridViewMHP, False) ' Make sure that MHP grid is not visible when populating PHP Grid
                ControlMgr.SetVisibleControl(Me, GridViewBLGS, False) ' Make sure that BLGS grid is not visible when populating PHP Grid
                Session("recCount") = Me.State.searchActiveDV.Count

                'If Me.Grid.Visible Then 'DEF-25086
                Me.lblActiveSearchResults.Visible = True
                Me.lblActiveSearchResults.Text = Me.State.searchActiveDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                'End If

            ElseIf Me.State.DealerObjInSearch.Dealer = "TMHP" Then
                ' Second Grid
                If (Me.State.searchActiveDV Is Nothing) Then
                    Me.State.searchActiveDV = Reconciliation.GetMHPReconData(Me.State.DealerIdInSearch, _
                                                Me.State.firstDayOfMonth, Me.State.lastDayOfMonth, Me.State.IsShowDiscrepChecked)
                    If Me.State.SearchClicked Then
                        Me.ValidSearchResultCountNew(Me.State.searchActiveDV.Count, True)
                        Me.State.SearchClicked = False
                    End If
                End If

                GridViewMHP.PageSize = State.PageSize

                If State.searchActiveDV.Count = 0 Then
                    BtnOverRideRecon.Enabled = False
                    ControlMgr.SetVisibleControl(Me, GridViewMHP, False)
                Else
                    Me.GridViewMHP.DataSource = Me.State.searchActiveDV
                    ControlMgr.SetVisibleControl(Me, GridViewMHP, True)
                    BtnOverRideRecon.Enabled = True
                End If

                Me.State.PageIndex = Me.GridViewMHP.PageIndex

                'Disabled the Sorting on MHP Grid as it has only a small number of records and in certain sequence
                'If (Not Me.State.MHPGridSortExpression.Equals(String.Empty)) Then
                '    Me.State.searchActiveDV.Sort = Me.State.MHPGridSortExpression
                'End If
                'HighLightSortColumn(GridViewMHP, Me.State.MHPGridSortExpression, True)

                Me.GridViewMHP.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, False) ' Make sure that PHP grid is not visible when populating MHP Grid
                ControlMgr.SetVisibleControl(Me, GridViewBLGS, False) ' Make sure that BLGS grid is not visible when populating MHP Grid
                Session("recCount") = Me.State.searchActiveDV.Count


                'If Me.GridViewMHP.Visible Then 'DEF-25086
                Me.lblActiveSearchResults.Visible = True
                Me.lblActiveSearchResults.Text = Me.State.searchActiveDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                'End If
            ElseIf Me.State.DealerObjInSearch.Dealer = "BLGS" Then
                ' Third Grid
                If (Me.State.searchActiveDV Is Nothing) Then
                    Me.State.searchActiveDV = Reconciliation.GetPHPReconData(Me.State.DealerIdInSearch, _
                                                Me.State.firstDayOfMonth, Me.State.lastDayOfMonth, Me.State.IsShowDiscrepChecked)
                    If Me.State.SearchClicked Then
                        Me.ValidSearchResultCountNew(Me.State.searchActiveDV.Count, True)
                        Me.State.SearchClicked = False
                    End If
                End If

                GridViewBLGS.PageSize = State.PageSize

                If State.searchActiveDV.Count = 0 Then
                    BtnOverRideRecon.Enabled = False
                    ControlMgr.SetVisibleControl(Me, GridViewBLGS, False)
                Else
                    Me.GridViewBLGS.DataSource = Me.State.searchActiveDV
                    ControlMgr.SetVisibleControl(Me, GridViewBLGS, True)
                    BtnOverRideRecon.Enabled = True
                End If

                Me.State.PageIndex = Me.GridViewBLGS.PageIndex

                'Disabled the Sorting on BLGS Grid as it has only a small number of records

                Me.GridViewBLGS.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, False) ' Make sure that PHP grid is not visible when populating BLGS Grid
                ControlMgr.SetVisibleControl(Me, GridViewMHP, False) ' Make sure that MHP grid is not visible when populating BLGS Grid
                Session("recCount") = Me.State.searchActiveDV.Count


                'If Me.GridViewBLGS.Visible Then 'DEF-25086
                Me.lblActiveSearchResults.Visible = True
                Me.lblActiveSearchResults.Text = Me.State.searchActiveDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                'End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim iMonth As Integer
        Dim iYear As Integer
        Try
            If Not Me.State.DealerIdInSearch = Guid.Empty Then
                Me.State.DealerIdInSearch = Me.GetSelectedItem(ddlDealer)
                Me.State.DealerObjInSearch = New Dealer(Me.State.DealerIdInSearch)
                Integer.TryParse(GetSelectedValue(ddlAcctPeriodMonth), iMonth)
                Integer.TryParse(GetSelectedValue(ddlAcctPeriodYear), iYear)
                Me.State.firstDayOfMonth = GetFirstDayOfMonth(iMonth, iYear).ToString(DALObjects.DALBase.DOTNET_QUERY_DATEFORMAT)
                Me.State.lastDayOfMonth = GetLastDayOfMonth(iMonth, iYear).ToString(DALObjects.DALBase.DOTNET_QUERY_DATEFORMAT)

                Return True
            Else
                ControlMgr.SetVisibleControl(Me, GridViewMHP, False)
                ControlMgr.SetVisibleControl(Me, GridViewBLGS, False)
                ControlMgr.SetVisibleControl(Me, Grid, False)
                BtnOverRideRecon.Enabled = False
                Return False
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Function


    Private Sub ddlDealer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDealer.SelectedIndexChanged
        Try
            ClearReconciliationResults()
            Me.State.DealerIdInSearch = Me.GetSelectedItem(ddlDealer)
            Me.State.DealerObjInSearch = New Dealer(Me.State.DealerIdInSearch)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.State.searchActiveDV = Nothing
            Me.chkDiscrepOnly.Checked = False
            Me.State.IsShowDiscrepChecked = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub chkDiscrepOnly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDiscrepOnly.CheckedChanged
        Try
            'State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            'Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchActiveDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.State.searchActiveDV = Nothing
            If chkDiscrepOnly.Checked Then
                Me.State.IsShowDiscrepChecked = True
            Else
                Me.State.IsShowDiscrepChecked = False
            End If
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#Region "Date Functions"


    Private Function GetFirstDayOfMonth(ByVal iMonth As Int32, ByVal iYear As Int32) As DateTime
        'set return value to the last day of the month for any date passed in to the method
        'create a datetime variable set to the passed in date
        Dim dtFrom As New DateTime(iYear, iMonth, 1)
        'remove all of the days in the month except the first day and set the variable to hold that date
        dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1))
        'return the first day of the month
        Return dtFrom
    End Function

    Private Function GetLastDayOfMonth(ByVal iMonth As Int32, ByVal iYear As Int32) As DateTime
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

    Public Sub GridRowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
            'If Not Me.State.DealerObjInSearch Is Nothing Then
            '    If Me.State.DealerObjInSearch.Dealer = "BLGS" Then
            '        Grid.Columns(Me.GRID_COL_NAME_BILLABLE_COUNT).HeaderText = "ePrism Count"
            '        Grid.Columns(Me.GRID_COL_NAME_CARRIER_COUNT).HeaderText = "BI Count"
            '    End If
            'End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim discrepancy As String

        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If (e.Row.RowType = DataControlRowType.Header) Then
                If Me.State.DealerObjInSearch.Dealer = "BLGS" Then
                    Grid.Columns(Me.GRID_COL_NAME_BILLABLE_COUNT).HeaderText = "ePrism Count"
                    Grid.Columns(Me.GRID_COL_NAME_CARRIER_COUNT).HeaderText = "BI Count"
                End If
            End If

            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then

                discrepancy = dvRow(Reconciliation.ReconciliationDV.COL_NAME_DISCREPANCY).ToString
                If discrepancy = "Y" Then
                    e.Row.Cells(Me.GRID_COL_NAME_BILLABLE_COUNT).CssClass = "StatInactive"
                    e.Row.Cells(Me.GRID_COL_NAME_CARRIER_COUNT).CssClass = "StatInactive"
                End If

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            'Me.State.selectedPaymentGroupId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If Me.State.PHPGridSortExpression.StartsWith(e.SortExpression) Then
                If Me.State.PHPGridSortExpression.EndsWith(" DESC") Then
                    Me.State.PHPGridSortExpression = e.SortExpression
                Else
                    Me.State.PHPGridSortExpression &= " DESC"
                End If
            Else
                Me.State.PHPGridSortExpression = e.SortExpression
            End If

            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region "MHP Grid related"
    Public Sub GridViewMHPRowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewMHP.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewMHP_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewMHP.RowDataBound
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewMHP_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewMHP.PageIndexChanged
        Try
            Me.State.PageIndex = GridViewMHP.PageIndex
            'Me.State.selectedPaymentGroupId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewMHP_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewMHP.PageIndexChanging
        Try
            GridViewMHP.PageIndex = e.NewPageIndex
            State.PageIndex = GridViewMHP.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "BLGS Grid related"
    Public Sub GridViewBLGSRowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBLGS.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewBLGS_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewBLGS.RowDataBound
        Dim discrepancy As String

        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then

                discrepancy = dvRow(Reconciliation.ReconciliationDV.COL_NAME_DISCREPANCY).ToString
                If discrepancy = "Y" Then
                    e.Row.Cells(Me.GRID_COL_NAME_EPRISM_COUNT).CssClass = "StatInactive"
                    e.Row.Cells(Me.GRID_COL_NAME_BI_COUNT).CssClass = "StatInactive"
                End If

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewBLGS_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridViewBLGS.PageIndexChanged
        Try
            Me.State.PageIndex = GridViewBLGS.PageIndex
            'Me.State.selectedPaymentGroupId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridViewBLGS_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewBLGS.PageIndexChanging
        Try
            GridViewBLGS.PageIndex = e.NewPageIndex
            State.PageIndex = GridViewBLGS.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region



    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            If Me.State.DealerObjInSearch.Dealer = "TPHP" Then
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchActiveDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
            ElseIf Me.State.DealerObjInSearch.Dealer = "TMHP" Then
                Me.State.PageIndex = NewCurrentPageIndex(GridViewMHP, State.searchActiveDV.Count, State.PageSize)
                Me.GridViewMHP.PageIndex = Me.State.PageIndex
            ElseIf Me.State.DealerObjInSearch.Dealer = "BLGS" Then
                Me.State.PageIndex = NewCurrentPageIndex(GridViewBLGS, State.searchActiveDV.Count, State.PageSize)
                Me.GridViewBLGS.PageIndex = Me.State.PageIndex
            End If

            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub BtnOverRideRecon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOverRideRecon.Click
        Dim Result As Boolean
        Try
            Result = Reconciliation.OverRideReconciliation(Me.State.DealerIdInSearch, Me.State.firstDayOfMonth, _
                                                           Me.State.lastDayOfMonth, ElitaPlusIdentity.Current.ActiveUser.UserName)

            If Result Then
                Me.MasterPage.MessageController.AddSuccess(Message.MSG_RECON_OVERRIDEN_SUCCESSFULLY, True)
            Else
                Me.MasterPage.MessageController.AddErrorAndShow(Message.MSG_COULD_NOT_OVERRIDE_RECON, True)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        ddlDealer.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
        'Me.ddlAcctPeriodMonth.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
        'Me.ddlAcctPeriodYear.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
        Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX

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
            Me.State.searchActiveDV = Nothing
            Me.chkDiscrepOnly.Checked = False
            Me.State.IsShowDiscrepChecked = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


End Class