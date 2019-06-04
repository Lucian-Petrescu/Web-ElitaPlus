Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Class InvoiceListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblBlank As System.Web.UI.WebControls.Label
    Protected WithEvents trSortBy As System.Web.UI.HtmlControls.HtmlTableRow

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
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_INVOICE_NUMBER_IDX As Integer = 1
    Public Const GRID_COL_PAYEE_IDX As Integer = 2
    Public Const GRID_COL_AUTHORIZATION_NUMBER_IDX As Integer = 3
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 4
    Public Const GRID_COL_CREATED_DATE_IDX As Integer = 5
    Public Const GRID_COL_INVOICE_AMOUNT_IDX As Integer = 6
    Public Const GRID_COL_CLAIM_INVOICE_ID_IDX As Integer = 7

    Public Const GRID_TOTAL_COLUMNS As Integer = 8
    Public Const MAX_LIMIT As Integer = 1000

    Public Const PAGETITLE As String = "SEARCH_INVOICE"
    Public Const PAGETAB As String = "CLAIM"

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = ClaimInvoice.ClaimInvoiceSearchDV.COL_INVOICE_NUMBER
        Public selectedClaimInvoiceId As Guid = Guid.Empty
        Public invoiceNumber As String
        Public claimNumber As String
        Public payee As String
        Public createdDate As String
        Public invoiceAmount As String
        Public IsGridVisible As Boolean = False
        Public searchDV As ClaimInvoice.ClaimInvoiceSearchDV = Nothing
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SearchClicked As Boolean
        Public invoiceAmountCulture As String

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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                Me.SetDefaultButton(Me.TextBoxSearchClaimNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchInvoiceAmount, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchInvoiceNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchPayeeName, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchCreatedDate, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.AddCalendar(Me.ImageButtonSearchCreatedDate, Me.TextBoxSearchCreatedDate)
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                Me.TranslateGridHeader(Me.Grid)
                Me.TranslateGridControls(Me.Grid)
                PopulateSortByDropDown()
                PopulateSearchFieldsFromState()
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
                SetFocus(Me.TextBoxSearchInvoiceNumber)
            Else
                Me.ClearLabelErrSign(LabelSearchCreatedDate)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As PayClaimForm.ReturnType = CType(Me.ReturnedValues, PayClaimForm.ReturnType)
            If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                Me.State.searchDV = Nothing
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing AndAlso CallFromUrl = "/ElitaPlus/Claims/MasterClaimForm.aspx" Then
                'Me.StartNavControl()
                Me.State.invoiceNumber = Me.CallingParameters.ToString
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Sub PopulateSortByDropDown()
        Try
            'Me.BindListControlToDataView(Me.cboSortBy, LookupListNew.GetInvoiceSearchFieldsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
            cboSortBy.Populate(CommonConfigManager.Current.ListManager.GetList("INSDR",Thread.CurrentPrincipal.GetLanguageCode()),New PopulateOptions() With
                {
                .AddBlankItem = True
            })
            Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_INVOICE_SEARCH_FIELDS, Codes.DEFAULT_SORT_FOR_INVOICES)

            If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                Me.SetSelectedItem(Me.cboSortBy, defaultSelectedCodeId)
                Me.State.selectedSortById = defaultSelectedCodeId
            Else
                Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


    Public Sub PopulateGrid()

        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (Me.State.searchDV Is Nothing) Then

                Dim createDateStr As String = String.Empty

                If Me.State.createdDate.Length > 0 Then
                    Dim createdDate As DateTime = New DateTime
                    Try
                        createdDate = DateTime.Parse(Me.State.createdDate.ToString(),
                                                        System.Threading.Thread.CurrentThread.CurrentCulture,
                                                        System.Globalization.DateTimeStyles.NoCurrentDateDefault)
                    Catch ex As Exception
                        Me.SetLabelError(LabelSearchCreatedDate)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                    End Try

                    createDateStr = createdDate.ToString(DALObjects.DALBase.DOTNET_QUERY_DATEFORMAT)
                End If


                Dim sortBy As String
                If (Not (Me.State.selectedSortById.Equals(Guid.Empty))) Then
                    sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_INVOICE_SEARCH_FIELDS, Me.State.selectedSortById)
                    Me.State.searchDV = ClaimInvoice.getList(Me.State.invoiceNumber,
                                                                          Me.State.payee,
                                                                          Me.State.claimNumber,
                                                                          createDateStr,
                                                                          Me.State.invoiceAmount, sortBy)

                Else
                    Me.State.searchDV = ClaimInvoice.getList(Me.State.invoiceNumber,
                                                                          Me.State.payee,
                                                                          Me.State.claimNumber,
                                                                          createDateStr,
                                                                          Me.State.invoiceAmount)
                End If

                If (Me.State.SearchClicked) Then
                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            Me.Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimInvoiceId, Me.Grid, Me.State.PageIndex)
            Me.State.PageIndex = Me.Grid.PageIndex
            Me.Grid.DataSource = Me.State.searchDV
            Me.Grid.AllowSorting = False
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            Else
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
                CreateHeaderForEmptyGrid(Grid, Me.State.SortExpression)
                Me.AddInfoMsg(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND)
            End If

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True


        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()

        Try
            Me.TextBoxSearchInvoiceNumber.Text = Me.State.invoiceNumber
            Me.TextBoxSearchClaimNumber.Text = Me.State.claimNumber
            Me.TextBoxSearchPayeeName.Text = Me.State.payee
            Me.TextBoxSearchCreatedDate.Text = Me.State.createdDate
            'Me.TextBoxSearchInvoiceAmount.Text = Me.State.invoiceAmount
            If Me.State.invoiceAmount Is Nothing Then
                Me.TextBoxSearchInvoiceAmount.Text = Me.State.invoiceAmount
            Else
                Me.TextBoxSearchInvoiceAmount.Text = Me.State.invoiceAmountCulture 'Me.State.authorizedAmount
            End If

            Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double
        Try
            Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text.ToUpper
            Me.State.invoiceNumber = Me.TextBoxSearchInvoiceNumber.Text
            Me.State.payee = Me.TextBoxSearchPayeeName.Text
            Me.State.createdDate = Me.TextBoxSearchCreatedDate.Text
            '  Me.State.invoiceAmount = Me.TextBoxSearchInvoiceAmount.Text
            Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy)

            If Not Me.TextBoxSearchInvoiceAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(Me.TextBoxSearchInvoiceAmount.Text, dblAmount) Then
                    Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                    Return False
                Else
                    Me.State.invoiceAmountCulture = Me.TextBoxSearchInvoiceAmount.Text
                    Me.State.invoiceAmount = dblAmount.ToString(System.Threading.Thread.CurrentThread.CurrentCulture.InvariantCulture)
                End If
            Else
                Me.State.invoiceAmount = Me.TextBoxSearchInvoiceAmount.Text
            End If

            Return True

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Function

    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(ByVal dv As ClaimInvoice.ClaimInvoiceSearchDV) As Integer
        Try
            If Me.State.selectedClaimInvoiceId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(ClaimInvoice.ClaimInvoiceSearchDV.COL_CLAIM_INVOICE_ID), Byte())).Equals(Me.State.selectedClaimInvoiceId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Function

    Public Sub ClearSearch()
        Try
            Me.TextBoxSearchClaimNumber.Text = String.Empty
            Me.TextBoxSearchInvoiceNumber.Text = String.Empty
            Me.TextBoxSearchClaimNumber.Text = String.Empty
            Me.TextBoxSearchPayeeName.Text = String.Empty
            Me.TextBoxSearchCreatedDate.Text = String.Empty
            Me.TextBoxSearchInvoiceAmount.Text = String.Empty
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Try
            If Not dvRow Is Nothing And Me.State.searchDV.Count > 0 Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_INVOICE_ID_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_CLAIM_INVOICE_ID))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_CLAIM_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_INVOICE_NUMBER_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_INVOICE_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_PAYEE_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_PAYEE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_AUTHORIZATION_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CREATED_DATE_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_CREATED_DATE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_INVOICE_AMOUNT_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_INVOICE_AMOUNT))
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

        Try
            If e.CommandName = "SelectAction" Then
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                Me.State.selectedClaimInvoiceId = New Guid(Me.Grid.Rows(RowInd).Cells(Me.GRID_COL_CLAIM_INVOICE_ID_IDX).Text)
                'Me.State.selectedClaimInvoiceId = New Guid(e.Item.Cells(Me.GRID_COL_CLAIM_INVOICE_ID_IDX).Text)
                Me.callPage(PayClaimForm.URL, New PayClaimForm.Parameters(Me.State.selectedClaimInvoiceId))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

    Public Sub Grid_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        BaseItemCreated(sender, e)
    End Sub

    'Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
    '    Try
    '        'Grid.PageIndex = Grid.PageIndex
    '        Me.State.PageIndex = Grid.PageIndex
    '        Me.State.selectedClaimInvoiceId = Guid.Empty
    '        Me.PopulateGrid()
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.ErrControllerMaster)
    '    End Try
    'End Sub

    Private Sub moGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
            'To enable paging after back button
            Me.State.selectedClaimInvoiceId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            'Me.PopulateSearchFieldsFromState()
            Me.State.SearchClicked = True
            Me.State.PageIndex = 0
            Me.State.selectedClaimInvoiceId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    'Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.callPage(ClaimForm.URL)
    'End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.ClearSearch()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Error Handling"


#End Region

#Region "State Controller"

#End Region



End Class
