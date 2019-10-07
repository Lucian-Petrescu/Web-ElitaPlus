Imports System.Globalization
Imports System.Collections.Generic

Partial Class APInvoiceListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"


    Public Const PAGETITLE = "AP_INVOICE_SEARCH"
    Public Const PAGETAB As String = "CLAIM"

    Public Const GRID_COL_INVOICE_NUMBER_CTRL As String = "btnInvoiceDetails"
    Public Const GRID_COL_SELECT_CTRL As String = "checkBoxSelected"

    Public Const GRID_COL_SELECT_IDX As Integer = 0
    Public Const GRID_COL_VENDOR_IDX As Integer = 1
    Public Const GRID_COL_INVOICE_NUMBER_IDX As Integer = 2
    Public Const GRID_COL_INVOICE_DATE_IDX As Integer = 3
    Public Const GRID_COL_DUE_DATE_IDX As Integer = 4
    Public Const GRID_COL_SOURCE_IDX As Integer = 5
    Public Const GRID_COL_INVOICE_AMT_IDX As Integer = 6
    Public Const GRID_COL_MATCHED_AMT_IDX As Integer = 7
    Public Const GRID_COL_PAID_AMT_IDX As Integer = 8
    Public Const GRID_COL_PYMT_DATE_IDX As Integer = 9
    Public Const GRID_COL_UNMATCHED_LINES_IDX As Integer = 10
    Public Const GRID_COL_INVOICE_HEADER_ID_IDX As Integer = 11

    Public Const MAX_LIMIT As Integer = 100

#End Region

#Region "Page State"

    Private IsReturningFromChild As Boolean = False

    Class SearchCriterias
        Public VendorCode As String = String.Empty
        Public InvoiceNumber As String = String.Empty
        Public Source As String = String.Empty

        Private _InvoiceDateString As String = String.Empty
        Private _InvoiceDate As Date?

        Private _DueDateFromString As String = String.Empty
        Private _DueDateFrom As Date?
        
        Private _DueDateToString As String = String.Empty
        Private _DueDateTo As Date

        Public Property DueDateFromString As String
            Set(value As String)
                _DueDateFromString = value
                Dim dt As Date
                If DateTime.TryParseExact(DueDateFromString.Trim(), DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, dt) Then
                    _DueDateFrom = dt
                End If
            End Set
            Get
                Return _DueDateFromString
            End Get
        End Property

        Public ReadOnly Property DueDateFrom As Date?
            Get
                Return _DueDateFrom
            End Get
        End Property

        Public Property DueDateToString As String
            Set(value As String)
                _DueDateToString = value
                Dim dt As Date
                If DateTime.TryParseExact(_DueDateToString.Trim(), DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, dt) Then
                    _DueDateTo = dt
                End If
            End Set
            Get
                Return _DueDateToString
            End Get
        End Property

        Public ReadOnly Property DueDateTo As Date?
            Get
                Return _DueDateTo
            End Get
        End Property

        Public Property InvoiceDateString As String
            Set(value As String)
                _InvoiceDateString = value
                Dim dt As Date
                If DateTime.TryParseExact(_InvoiceDateString.Trim(), DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, dt) Then
                    _InvoiceDate = dt
                End If
            End Set
            Get
                Return _InvoiceDateString
            End Get
        End Property

        Public ReadOnly Property InvoiceDate As Date?
            Get
                Return _InvoiceDate
            End Get
        End Property
    End Class

    Class MyState
        Public PageIndex As Integer = 0
        Public SelectedInvoiceId As Guid = Guid.Empty

        Public IsGridVisible As Boolean = False
        Public selectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public SearchDv As ApInvoiceHeader.APInvoiceSearchDV = Nothing
        Public Criterias As SearchCriterias = New SearchCriterias()

        Public Sub New()
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
        MasterPage.MessageController.Clear_Hide()
        Try
            If Not IsPostBack Then
                UpdateBreadCrum()
                SetDefaultButtonToControls()

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                AddCalendar(ImageInvoiceDate, txtInvoiceDate)
                AddCalendar(ImageInvoiceDueDateFrom, txtInvoiceDueDateFrom)
                AddCalendar(ImageInvoiceDueDateTo, txtInvoiceDueDateTo)
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                PopulateSearchFieldsFromState()

                If State.IsGridVisible Then
                    TranslateGridHeader(Grid)
                    cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                    Grid.PageSize = Me.State.selectedPageSize
                    PopulateGrid()
                    SetGridItemStyleColor(Grid)
                End If

                SetFocus(txtVendorCode)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            'Dim retObj As PayClaimForm.ReturnType = CType(Me.ReturnedValues, PayClaimForm.ReturnType)
            'If Not retObj Is Nothing AndAlso retObj.BoChanged Then
            '    Me.State.searchDV = Nothing
            'End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub APInvoiceListForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        'sysn buttons' visibility with the grid's visibility
        'divButtons.Visible = State.IsGridVisible
    End Sub
#End Region

#Region "Controlling logic"
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End If
    End Sub

    Private Sub SetDefaultButtonToControls()
        Try
            SetDefaultButton(txtVendorCode, btnSearch)
            SetDefaultButton(txtInvoiceNumber, btnSearch)
            SetDefaultButton(txtInvoiceSource, btnSearch)
            SetDefaultButton(txtInvoiceDate, btnSearch)
            SetDefaultButton(txtInvoiceDueDateFrom, btnSearch)
            SetDefaultButton(txtInvoiceDueDateTo, btnSearch)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Public Sub PopulateSearchFieldsFromState()

        Try
            If Not State.Criterias Is Nothing Then
                txtVendorCode.Text = State.Criterias.VendorCode
                txtInvoiceNumber.Text = State.Criterias.InvoiceNumber
                txtInvoiceSource.Text = State.Criterias.Source
                txtInvoiceDate.Text = State.Criterias.InvoiceDateString
                txtInvoiceDueDateFrom.Text = State.Criterias.DueDateFromString
                txtInvoiceDueDateTo.Text = State.Criterias.DueDateToString
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub PopulateGrid()

        Try

            If State.SearchDv Is Nothing Then

               State.SearchDv = ApInvoiceHeader.GetAPInvoices(
                                         State.Criterias.VendorCode.Trim.ToUpper,
                                         State.Criterias.InvoiceNumber.Trim.ToUpper,
                                         State.Criterias.Source.Trim.ToUpper,
                                         State.Criterias.InvoiceDate,
                                         State.Criterias.DueDateFrom,
                                         State.Criterias.DueDateTo,
                                         MAX_LIMIT
                                         )
            End If

            Grid.PageSize = State.selectedPageSize

            SetPageAndSelectedIndexFromGuid(State.SearchDv, State.SelectedInvoiceId, Grid, State.PageIndex)
            Grid.DataSource = State.SearchDv
            State.PageIndex = Grid.PageIndex

            HighLightSortColumn(Grid, String.Empty, IsNewUI)

            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.SearchDv.Count

            If Grid.Visible Then
                lblRecordCount.Text = State.SearchDv.TotalCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND) & " " & State.SearchDv.Count & " " & TranslationBase.TranslateLabelOrMessage("MSG_RECORDS_DISPLAYED")
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim dt As Date, blnSuccess as boolean = True

        Try
            State.Criterias = New SearchCriterias()
            State.Criterias.InvoiceNumber = txtInvoiceNumber.Text
            State.Criterias.VendorCode = txtVendorCode.Text
            State.Criterias.Source = txtInvoiceSource.Text
            
            If String.IsNullOrWhiteSpace(txtInvoiceDate.Text) = False Then
                if Date.TryParseExact(txtInvoiceDate.Text.Trim(), DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, dt) Then
                    If (dt <> Date.MinValue) Then
                        State.Criterias.InvoiceDateString = txtInvoiceDate.Text.Trim
                    End If
                        
                else
                    blnSuccess = False
                    SetLabelError(lblInvoiceDate)
                    Throw New GUIException(Message.MSG_INVALID_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
                End If
            End If

            If String.IsNullOrEmpty(txtInvoiceDueDateFrom.Text) = False Then
                if Date.TryParseExact(txtInvoiceDueDateFrom.Text.Trim(), DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, dt) Then
                    If (dt <> Date.MinValue) Then
                        State.Criterias.DueDateFromString = txtInvoiceDueDateFrom.Text.Trim
                    End If
                Else
                    blnSuccess = False
                    ElitaPlusPage.SetLabelError(lblInvoiceDueDateFrom)
                    Throw New GUIException(Message.MSG_INVALID_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
                End If
            End If

            If String.IsNullOrEmpty(txtInvoiceDueDateTo.Text) = False Then
                If Date.TryParseExact(txtInvoiceDueDateTo.Text.Trim(), DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture, DateTimeStyles.None, dt) Then
                    If (dt <> Date.MinValue) Then
                        State.Criterias.DueDateToString = txtInvoiceDueDateTo.Text.Trim
                    End If
                Else
                    blnSuccess = False        
                    ElitaPlusPage.SetLabelError(lblInvoiceDueDateTo)
                    Throw New GUIException(Message.MSG_INVALID_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
                End If
            End If            

        Catch ex As Exception
            blnSuccess = False
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        Return blnSuccess
    End Function
    
    Public function GetSelectedInvoices() as List(Of Guid)
        Dim chkbox As CheckBox, invoiceHeaderIdstr As String
        Dim selectedInvoices As List(Of Guid) = New List(Of Guid)

        For each row As GridViewRow In Grid.Rows
            If row.RowType = DataControlRowType.DataRow Then
                chkbox = CType(row.Cells(GRID_COL_SELECT_IDX).FindControl(GRID_COL_SELECT_CTRL), CheckBox)
                If chkbox.Checked Then
                   invoiceHeaderIdstr = row.Cells(GRID_COL_INVOICE_HEADER_ID_IDX).Text.Trim
                   If String.IsNullOrEmpty(invoiceHeaderIdstr) = False Then
                        selectedInvoices.Add(New Guid(invoiceHeaderIdstr))
                   End If
                End If
            End If
        Next
        Return selectedInvoices
    End function
#End Region

#Region "Button event handlers"
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try
            'clear state
            State.Criterias = New SearchCriterias
            State.SearchDv = Nothing

            'clear search controls
            txtVendorCode.Text = String.Empty
            txtInvoiceNumber.Text = String.Empty
            txtInvoiceSource.Text = String.Empty
            txtInvoiceDate.Text = String.Empty
            txtInvoiceDueDateFrom.Text = String.Empty
            txtInvoiceDueDateTo.Text = String.Empty

            'high search results
            ControlMgr.SetVisibleControl(Me, Grid, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try

            ControlMgr.SetVisibleControl(Me, Grid, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            State.IsGridVisible = False

            If PopulateStateFromSearchFields() Then
                State.PageIndex = 0
                State.SelectedInvoiceId = Guid.Empty
                State.IsGridVisible = True
                State.SearchDv = Nothing
                PopulateGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private function validInvoiceSelectionForDelete(ByVal invoiceList as List(Of Guid)) As Boolean
        Dim isSelectionValid As Boolean = True
        If invoiceList.Count > 0 Then
            Dim dv As DataView = State.SearchDv
            dv.Sort = ApInvoiceHeader.APInvoiceSearchDV.COL_INVOICE_HEADER_ID
                    
            For each guidtemp As Guid In invoiceList
                dim index As Integer = dv.Find(guidtemp)
                If index <> -1 Then 'found the record
                    if dv(index)(ApInvoiceHeader.APInvoiceSearchDV.COL_PAID_AMOUNT) > 0 Then 'invoice paid already, can not be deleted
                        MasterPage.MessageController.AddErrorAndShow("CAN_NOT_DELETE_PAID_INVOICE", true)
                    End If
                End If
            Next                
        Else
            'must select at least one invoice to delete
            MasterPage.MessageController.AddErrorAndShow("NO_INVOICE_SELECTED", true)
        End If
    End function
    Private Sub btnDelete_WRITE_Click(sender As Object, e As EventArgs) Handles btnDelete_WRITE.Click
        Try
            Dim invoiceToBeDeleted as List(Of Guid) = GetSelectedInvoices
            If validInvoiceSelectionForDelete(invoiceToBeDeleted) Then
                'call delete invoice method for each invoices
                ApInvoiceHeader.DeleteInvoices(invoiceToBeDeleted)
            End If

            ' test delete functions.
            Dim testlist As new List(Of Guid)
            testlist.Add(Guid.NewGuid)
            testlist.Add(Guid.NewGuid)
            testlist.Add(Guid.NewGuid)
            ApInvoiceHeader.DeleteInvoices(testlist)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCreatePaymentBatch_WRITE_Click(sender As Object, e As EventArgs) Handles btnCreatePaymentBatch_WRITE.Click

    End Sub
#End Region

#Region "Grid Event handlers"

    'Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
    '    Try
    '        If Me.State.SortExpression.StartsWith(e.SortExpression) Then
    '            If Me.State.SortExpression.EndsWith(" DESC") Then
    '                Me.State.SortExpression = e.SortExpression & " ASC"
    '            Else
    '                Me.State.SortExpression = e.SortExpression & " DESC"
    '            End If
    '        Else
    '            Me.State.SortExpression = e.SortExpression & " ASC"
    '        End If
    '        Me.State.PageIndex = 0
    '        Me.PopulateGrid()
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try

    'End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditClaimItem As LinkButton

        Try
            If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (Not e.Row.Cells(GRID_COL_INVOICE_NUMBER_CTRL).FindControl(GRID_COL_INVOICE_NUMBER_CTRL) Is Nothing) Then
                    btnEditClaimItem = CType(e.Row.Cells(GRID_COL_INVOICE_NUMBER_CTRL).FindControl(GRID_COL_INVOICE_NUMBER_CTRL), LinkButton)
                    btnEditClaimItem.CommandArgument = e.Row.RowIndex.ToString
                    btnEditClaimItem.CommandName = SELECT_COMMAND_NAME
                    btnEditClaimItem.Text = dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_INVOICE_NUMBER).ToString
                End If
                ' populate dates fields
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_INVOICE_DATE_IDX), GetLongDateFormattedString(CType(dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_INVOICE_DATE), Date)))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DUE_DATE_IDX), GetLongDateFormattedString(CType(dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_DUE_DATE), Date)))
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PYMT_DATE_IDX), GetLongDateFormattedString(CType(dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_PAYMENT_DATE), Date)))                
                'populate the id field
                PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_INVOICE_HEADER_ID_IDX), dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_INVOICE_HEADER_ID))

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDv.Count, State.selectedPageSize)
            Grid.PageIndex = NewCurrentPageIndex(Grid, Session("recCount"), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub Grid_RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Dim rowIndex As Integer = 0
        Dim invoiceHeaderId As String = String.Empty
        Try
            If (Not e.CommandArgument.ToString().Equals(String.Empty)) And (e.CommandName = SELECT_COMMAND_NAME) Then
                rowIndex = CInt(e.CommandArgument)

                If State Is Nothing Then
                    Trace(Me, "Restoring State")
                    RestoreState(New MyState)
                End If

                invoiceHeaderId = Grid.Rows(rowIndex).Cells(GRID_COL_INVOICE_HEADER_ID_IDX).Text
                Me.State.SelectedInvoiceId = New Guid(invoiceHeaderId)

                'to do, calling AP invoice detail pages
                'callPage(APInvoiceDetailForm.URL, Me.State.SelectedInvoiceId)

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.SelectedInvoiceId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

End Class