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

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
        ErrControllerMaster.Clear_Hide()
        Try
            If Not IsPostBack Then
                SetDefaultButton(TextBoxSearchClaimNumber, btnSearch)
                SetDefaultButton(TextBoxSearchInvoiceAmount, btnSearch)
                SetDefaultButton(TextBoxSearchInvoiceNumber, btnSearch)
                SetDefaultButton(TextBoxSearchPayeeName, btnSearch)
                SetDefaultButton(TextBoxSearchCreatedDate, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                AddCalendar(ImageButtonSearchCreatedDate, TextBoxSearchCreatedDate)
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                PopulateSortByDropDown()
                PopulateSearchFieldsFromState()
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If
                SetGridItemStyleColor(Grid)
                SetFocus(TextBoxSearchInvoiceNumber)
            Else
                ClearLabelErrSign(LabelSearchCreatedDate)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As PayClaimForm.ReturnType = CType(ReturnedValues, PayClaimForm.ReturnType)
            If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                State.searchDV = Nothing
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing AndAlso CallFromUrl = "/ElitaPlus/Claims/MasterClaimForm.aspx" Then
                'Me.StartNavControl()
                State.invoiceNumber = CallingParameters.ToString
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
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

            If (State.selectedSortById.Equals(Guid.Empty)) Then
                SetSelectedItem(cboSortBy, defaultSelectedCodeId)
                State.selectedSortById = defaultSelectedCodeId
            Else
                SetSelectedItem(cboSortBy, State.selectedSortById)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


    Public Sub PopulateGrid()

        Try
            If Not PopulateStateFromSearchFields() Then Exit Sub

            If (State.searchDV Is Nothing) Then

                Dim createDateStr As String = String.Empty

                If State.createdDate.Length > 0 Then
                    Dim createdDate As DateTime = New DateTime
                    Try
                        createdDate = DateTime.Parse(State.createdDate.ToString(),
                                                        System.Threading.Thread.CurrentThread.CurrentCulture,
                                                        System.Globalization.DateTimeStyles.NoCurrentDateDefault)
                    Catch ex As Exception
                        SetLabelError(LabelSearchCreatedDate)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                    End Try

                    createDateStr = createdDate.ToString(DALObjects.DALBase.DOTNET_QUERY_DATEFORMAT)
                End If


                Dim sortBy As String
                If (Not (State.selectedSortById.Equals(Guid.Empty))) Then
                    sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_INVOICE_SEARCH_FIELDS, State.selectedSortById)
                    State.searchDV = ClaimInvoice.getList(State.invoiceNumber,
                                                                          State.payee,
                                                                          State.claimNumber,
                                                                          createDateStr,
                                                                          State.invoiceAmount, sortBy)

                Else
                    State.searchDV = ClaimInvoice.getList(State.invoiceNumber,
                                                                          State.payee,
                                                                          State.claimNumber,
                                                                          createDateStr,
                                                                          State.invoiceAmount)
                End If

                If (State.SearchClicked) Then
                    ValidSearchResultCount(State.searchDV.Count, True)
                    State.SearchClicked = False
                End If
            End If

            Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimInvoiceId, Grid, State.PageIndex)
            State.PageIndex = Grid.PageIndex
            Grid.DataSource = State.searchDV
            Grid.AllowSorting = False
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
            Else
                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
                CreateHeaderForEmptyGrid(Grid, State.SortExpression)
                AddInfoMsg(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND)
            End If

            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True


        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub PopulateSearchFieldsFromState()

        Try
            TextBoxSearchInvoiceNumber.Text = State.invoiceNumber
            TextBoxSearchClaimNumber.Text = State.claimNumber
            TextBoxSearchPayeeName.Text = State.payee
            TextBoxSearchCreatedDate.Text = State.createdDate
            'Me.TextBoxSearchInvoiceAmount.Text = Me.State.invoiceAmount
            If State.invoiceAmount Is Nothing Then
                TextBoxSearchInvoiceAmount.Text = State.invoiceAmount
            Else
                TextBoxSearchInvoiceAmount.Text = State.invoiceAmountCulture 'Me.State.authorizedAmount
            End If

            SetSelectedItem(cboSortBy, State.selectedSortById)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dblAmount As Double
        Try
            State.claimNumber = TextBoxSearchClaimNumber.Text.ToUpper
            State.invoiceNumber = TextBoxSearchInvoiceNumber.Text
            State.payee = TextBoxSearchPayeeName.Text
            State.createdDate = TextBoxSearchCreatedDate.Text
            '  Me.State.invoiceAmount = Me.TextBoxSearchInvoiceAmount.Text
            State.selectedSortById = GetSelectedItem(cboSortBy)

            If Not TextBoxSearchInvoiceAmount.Text.Trim = String.Empty Then
                If Not Double.TryParse(TextBoxSearchInvoiceAmount.Text, dblAmount) Then
                    ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                    Return False
                Else
                    State.invoiceAmountCulture = TextBoxSearchInvoiceAmount.Text
                    State.invoiceAmount = dblAmount.ToString(System.Threading.Thread.CurrentThread.CurrentCulture.InvariantCulture)
                End If
            Else
                State.invoiceAmount = TextBoxSearchInvoiceAmount.Text
            End If

            Return True

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Function

    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(dv As ClaimInvoice.ClaimInvoiceSearchDV) As Integer
        Try
            If State.selectedClaimInvoiceId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(ClaimInvoice.ClaimInvoiceSearchDV.COL_CLAIM_INVOICE_ID), Byte())).Equals(State.selectedClaimInvoiceId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Function

    Public Sub ClearSearch()
        Try
            TextBoxSearchClaimNumber.Text = String.Empty
            TextBoxSearchInvoiceNumber.Text = String.Empty
            TextBoxSearchClaimNumber.Text = String.Empty
            TextBoxSearchPayeeName.Text = String.Empty
            TextBoxSearchCreatedDate.Text = String.Empty
            TextBoxSearchInvoiceAmount.Text = String.Empty
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Try
            If dvRow IsNot Nothing AndAlso State.searchDV.Count > 0 Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_INVOICE_ID_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_CLAIM_INVOICE_ID))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_CLAIM_NUMBER))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_INVOICE_NUMBER_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_INVOICE_NUMBER))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PAYEE_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_PAYEE))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_AUTHORIZATION_NUMBER_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_AUTHORIZATION_NUMBER))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CREATED_DATE_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_CREATED_DATE))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_INVOICE_AMOUNT_IDX), dvRow(ClaimInvoice.ClaimInvoiceSearchDV.COL_INVOICE_AMOUNT))
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

        Try
            If e.CommandName = "SelectAction" Then
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                State.selectedClaimInvoiceId = New Guid(Grid.Rows(RowInd).Cells(GRID_COL_CLAIM_INVOICE_ID_IDX).Text)
                'Me.State.selectedClaimInvoiceId = New Guid(e.Item.Cells(Me.GRID_COL_CLAIM_INVOICE_ID_IDX).Text)
                callPage(PayClaimForm.URL, New PayClaimForm.Parameters(State.selectedClaimInvoiceId))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Public Sub Grid_RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
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

    Private Sub moGrid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
            'To enable paging after back button
            State.selectedClaimInvoiceId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            'Me.PopulateSearchFieldsFromState()
            State.SearchClicked = True
            State.PageIndex = 0
            State.selectedClaimInvoiceId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    'Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.callPage(ClaimForm.URL)
    'End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Error Handling"


#End Region

#Region "State Controller"

#End Region



End Class
