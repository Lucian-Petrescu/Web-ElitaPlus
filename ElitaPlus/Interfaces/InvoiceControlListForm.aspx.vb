Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class InvoiceControlListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "Interfaces/InvoiceControlListForm.aspx"
    Public Const PAGETITLE As String = "PREMIUM_INVOICE_LIST"
    Public Const PAGETAB As String = "INTERFACES"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Private Const GRID_COL_INVOICE_IDX As Integer = 1
    Private Const GRID_CONTROL_NAME_INVOICE_IDX As String = "InvCtlID"


    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_INVCTLID_IDX As Integer = 1
    Public Const GRID_COL_DEALER_IDX As Integer = 2
    Public Const GRID_COL_BRANCH_IDX As Integer = 3
    Public Const GRID_COL_INVOICE_DATE_IDX As Integer = 4
    Public Const GRID_COL_INVOICE_NUMBER_IDX As Integer = 5
    Public Const GRID_COL_CREDIT_NOTE_NUMBER_IDX As Integer = 6
    Public Const GRID_COL_CNEW_PREMIUM_TOTAL_IDX As Integer = 7
    Public Const GRID_COL_CANCEL_PREMIUM_TOTAL_IDX As Integer = 8
    Public Const GRID_TOTAL_COLUMNS As Integer = 9

    Public Const COL_DEALER_NAME As String = "Dealer_name"
    Public Const COL_BRANCH_NAME As String = "Branch_Name"
    Public Const COL_INVOICE_NUMBER As String = "Invoice_Number"
    Public Const COL_CREATED_DATE As String = "CREATED_DATE"
    Public Const COL_CREDIT_NOTE_NUMBER As String = "Credit_Note_Number"
    Public Const COL_PREVIOUS_INVOICE_DATE As String = "PREVIOUS_INVOICE_DATE"
    Public Const COL_NEW_TOTAL_CERT As String = "NEW_TOTAL_CERT"
    Public Const COL_NEW_PREMIUM_TOTAL As String = "NEW_PREMIUM_TOTAL"
    Public Const COL_CANCEL_TOTAL_CERT As String = "CANCEL_TOTAL_CERT"
    Public Const COL_CANCEL_PREMIUM_TOTAL As String = "CANCEL_PREMIUM_TOTAL"

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        'Public MyBO As VSCRateVersion
        Public InvoiceID As Guid
        Public PageIndex As Integer = 0
        Public IsGridVisible As Boolean
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As DataView = Nothing
        Public HasDataChanged As Boolean
        Public errLabel As String
        Public SearchedDealerID As Guid
        Public SearchedInvoiceNum As String
        Public SearchedStartDate As Date
        Public SearchedEndDate As Date

        Public SortExpression As String = "COMPANY, Dealer_name, Branch_Name, CREATED_DATE desc"
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

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ErrControllerMaster.Clear_Hide()
        Try
            If Not IsPostBack Then

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                AddCalendar(btnInvDateStart, txtInvDateStart)
                AddCalendar(btnInvDateEnd, txtInvDateEnd)

                populateSearchControls()

                If IsReturningFromChild AndAlso State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                    PopulateGrid(State.HasDataChanged)
                End If
            Else
                If State.errLabel <> "" Then
                    ClearLabelErrSign(CType(FindControl(State.errLabel), Label))
                    State.errLabel = ""
                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

    Private Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If ErrControllerMaster.Visible Then
            spanFiller.Text = "<tr><td colspan=""2"" style=""height:200px"">&nbsp;</td></tr>"
        Else
            spanFiller.Text = "<tr><td colspan=""2"" style=""height:1px"">&nbsp;</td></tr>"
        End If
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        MenuEnabled = True
        IsReturningFromChild = True
        If ReturnPar IsNot Nothing Then
            State.HasDataChanged = CType(ReturnPar, Boolean)
        Else
            State.HasDataChanged = False
        End If
    End Sub

#End Region

#Region "Helper functions"
    Private Sub populateSearchControls()
        Try
            Dim dv As DataView = LookupListNew.GetDealerLookupList(Authentication.CurrentUser.Companies)
            BindListControlToDataView(ddlDealer, dv, "DESCRIPTION", "ID", True)
            dv.Sort = "CODE"
            BindListControlToDataView(ddlDealerCode, dv, "CODE", "ID", True)


            'Dim DealerList As New Collections.Generic.List(Of DataElements.ListItem)
            'For Each CompanyId As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
            '    Dim Dealers As DataElements.ListItem() =
            '        CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany",
            '                                            context:=New ListContext() With
            '                                            {
            '                                              .CompanyId = CompanyId
            '                                            })

            '    If Dealers.Count > 0 Then
            '        If Not DealerList Is Nothing Then
            '            DealerList.AddRange(Dealers)
            '        Else
            '            DealerList = Dealers.Clone()
            '        End If
            '    End If
            'Next

            'ddlDealerCode.Populate(DealerList.ToArray(),
            '            New PopulateOptions() With
            '            {
            '                .AddBlankItem = True,
            '                .TextFunc = AddressOf .GetCode,
            '                .SortFunc = AddressOf .GetCode
            '            })

            'ddlDealer.Populate(DealerList.ToArray(),
            '            New PopulateOptions() With
            '            {
            '                .AddBlankItem = True
            '            })

            ddlDealer.Attributes.Add("onchange", "UpdateList('" & ddlDealerCode.ClientID & "')")
            ddlDealerCode.Attributes.Add("onchange", "UpdateList('" & ddlDealer.ClientID & "')")
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
    Public Function formatDateTime(objDate As Object) As String
        Dim strRet As String = objDate.ToString
        Try
            Dim dtVal As DateTime = CType(objDate, DateTime)
            strRet = dtVal.ToString("dd-MMM-yyyy") & " " & dtVal.ToLongTimeString()
        Catch ex As Exception
        End Try
        Return strRet
    End Function
    Private Sub PopulateGrid(Optional ByVal refreshData As Boolean = False)

        If State.searchDV Is Nothing OrElse refreshData Then SearchInvoice(refreshData)

        Grid.AutoGenerateColumns = False
        Grid.AllowSorting = True
        State.searchDV.Sort = State.SortExpression
        Grid.Columns(GRID_COL_DEALER_IDX).SortExpression = COL_DEALER_NAME
        Grid.Columns(GRID_COL_BRANCH_IDX).SortExpression = COL_BRANCH_NAME
        Grid.Columns(GRID_COL_INVOICE_DATE_IDX).SortExpression = COL_CREATED_DATE
        Grid.Columns(GRID_COL_INVOICE_NUMBER_IDX).SortExpression = COL_INVOICE_NUMBER
        Grid.Columns(GRID_COL_CREDIT_NOTE_NUMBER_IDX).SortExpression = COL_CREDIT_NOTE_NUMBER
        Grid.Columns(GRID_COL_CNEW_PREMIUM_TOTAL_IDX).SortExpression = COL_NEW_PREMIUM_TOTAL
        Grid.Columns(GRID_COL_CANCEL_PREMIUM_TOTAL_IDX).SortExpression = COL_CANCEL_TOTAL_CERT

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.InvoiceID, Grid, State.PageIndex, (Grid.EditItemIndex > GRID_NO_SELECTEDITEM_INX))

        State.PageIndex = Grid.CurrentPageIndex
        Grid.DataSource = State.searchDV
        HighLightSortColumn(Grid, State.SortExpression)
        Grid.DataBind()
        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        If Grid.Visible Then
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

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
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub SearchInvoice(Optional ByVal refreshData As Boolean = False)

        Dim DealerID As Guid, strInvNum As String, dtStart As Date, dtEnd As Date
        Dim strTemp As String = String.Empty

        If Not refreshData Then 'new search
            'clear stored search criteria
            State.SearchedDealerID = Guid.Empty
            State.SearchedInvoiceNum = ""
            State.SearchedStartDate = Date.MinValue
            State.SearchedEndDate = Date.MinValue

            DealerID = GetSelectedItem(ddlDealer)

            strInvNum = txtInvNum.Text.Trim()

            strTemp = txtInvDateStart.Text.Trim()
            If strTemp <> "" Then
                ' If Not Date.TryParse(strTemp, dtStart) Then
                If DateHelper.IsDate(strTemp) = False Then
                    SetLabelError(lblInvDate)
                    State.errLabel = lblInvDate.UniqueID
                    Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
                End If
            Else
                dtStart = Date.MinValue
            End If

            strTemp = txtInvDateEnd.Text.Trim()
            If strTemp <> "" Then
                ' If Not Date.TryParse(strTemp, dtEnd) Then
                If DateHelper.IsDate(strTemp) = False Then
                    SetLabelError(lblInvDate)
                    State.errLabel = lblInvDate.UniqueID
                    Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
                End If
            Else
                dtEnd = Date.MinValue
            End If

            State.SearchedDealerID = DealerID
            State.SearchedInvoiceNum = strInvNum
            State.SearchedStartDate = dtStart
            State.SearchedEndDate = dtEnd
        Else 'refresh data only
            DealerID = State.SearchedDealerID
            strInvNum = State.SearchedInvoiceNum
            dtStart = State.SearchedStartDate
            dtEnd = State.SearchedEndDate
        End If

        State.searchDV = AcctPremInvoice.SearchInvoices(Authentication.CurrentUser.Companies(), DealerID, strInvNum, dtStart, dtEnd)

    End Sub
#End Region

#Region "Grid Handler"
    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim lblControl As Label
        Try
            If e.CommandName = "SelectAction" Then
                lblControl = CType(Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_INVOICE_IDX).FindControl(GRID_CONTROL_NAME_INVOICE_IDX), Label)
                State.InvoiceID = New Guid(lblControl.Text)
                callPage(InvoiceControlDetailForm.URL, State.InvoiceID)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.InvoiceID = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.CurrentPageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Button click Handler"
    Protected Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
        Try
            callPage(InvoiceControlNewForm.URL)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.InvoiceID = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            State.IsGridVisible = False
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, State.IsGridVisible)
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        ddlDealer.SelectedIndex = 0
        ddlDealerCode.SelectedIndex = 0
        txtInvNum.Text = ""
        txtInvDateStart.Text = ""
        txtInvDateEnd.Text = ""
    End Sub

#End Region

End Class
