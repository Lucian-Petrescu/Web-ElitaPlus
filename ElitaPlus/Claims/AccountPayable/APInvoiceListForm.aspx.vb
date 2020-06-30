Imports System.Globalization
Imports System.Collections.Generic

Partial Class APInvoiceListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"


    Public Const PAGETITLE = "AP_INVOICE_SEARCH"
    Public Const PAGETAB As String = "CLAIM"

    Public Const GRID_COL_INVOICE_NUMBER_CTRL As String = "btnInvoiceDetails"
    Public Const GRID_COL_SELECT_CTRL As String = "checkBoxSelected"
    Public Const GRID_COL_SELECT_ALL_CTRL As String = "checkBoxAll"

    Public Const GRID_COL_CHECKBOX_IDX As Integer = 0
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

    Public Const MAX_LIMIT As Integer = 1000

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
        Private _DueDateTo As Date?

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
        lblBatchNum.ForeColor = Color.Empty

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

                TranslateGridHeader(Grid)

                If State.IsGridVisible Then
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
            Dim retObj As APInvoiceDetailForm.ReturnType = CType(Me.ReturnedValues, APInvoiceDetailForm.ReturnType)
            If Not retObj Is Nothing AndAlso retObj.DataChanged Then 
                'refresh the search since invoice data changed
                Me.State.searchDV = Nothing
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub APInvoiceListForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        'sysn buttons' visibility with the grid's visibility
        If State.IsGridVisible = True AndAlso not State.SearchDv Is nothing andalso State.SearchDv.Count > 0 Then
            divButtons.Visible = True

            Dim chkbox As CheckBox
            chkbox = CType(Grid.HeaderRow.FindControl(GRID_COL_SELECT_ALL_CTRL), CheckBox)
            If (Not chkbox Is Nothing) Then
                chkbox.InputAttributes("class") = "checkboxHeader"
            End If

            For each row As GridViewRow In Grid.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    chkbox = CType(row.Cells(GRID_COL_CHECKBOX_IDX).FindControl(GRID_COL_SELECT_CTRL), CheckBox)
                    If (Not chkbox Is Nothing) Then
                        chkbox.InputAttributes("class") = "checkboxLine"
                    End If                    
                End If                               
            Next
        Else
            divButtons.Visible = False
        End If
        
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
    Private Sub PopulateSearchFieldsFromState()

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

    Private Sub PopulateGrid()

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

    Private Function PopulateStateFromSearchFields() As Boolean
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

            if blnSuccess = True Then
                Dim hasValue As Boolean = False
                With State.Criterias
                    If .DueDateFrom.HasValue = True Then hasValue = True
                    If .DueDateTo.HasValue = True Then hasValue = True
                    If .InvoiceDate.HasValue = True Then hasValue = True
                    If string.IsNullOrWhiteSpace(.VendorCode) = False Then hasValue = True
                    If string.IsNullOrWhiteSpace(.InvoiceNumber) = False Then hasValue = True
                    If string.IsNullOrWhiteSpace(.Source) = False Then hasValue = True
                End With
                If hasValue = False Then
                    blnSuccess = False
                    MasterPage.MessageController.AddErrorAndShow("SEARCH_CRITERION_001", true)
                End If
            End If
        Catch ex As Exception
            blnSuccess = False
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        Return blnSuccess
    End Function
    
    Private function GetSelectedInvoices() as List(Of Guid)
        Dim chkbox As CheckBox, invoiceHeaderIdstr As String
        Dim selectedInvoices As List(Of Guid) = New List(Of Guid)

        For each row As GridViewRow In Grid.Rows
            If row.RowType = DataControlRowType.DataRow Then
                chkbox = CType(row.Cells(GRID_COL_CHECKBOX_IDX).FindControl(GRID_COL_SELECT_CTRL), CheckBox)
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

    Private Sub RefreshSearchGrid()
        State.PageIndex = 0
        State.SelectedInvoiceId = Guid.Empty
        State.IsGridVisible = True
        State.SearchDv = Nothing
        PopulateGrid()
    End Sub

    Private function validInvoiceSelectionForDelete(ByVal invoiceList as List(Of Guid)) As Boolean
        Dim isSelectionValid As Boolean = True
        If invoiceList.Count > 0 Then
            Dim dv As DataView = State.SearchDv
            dv.Sort = ApInvoiceHeader.APInvoiceSearchDV.COL_INVOICE_HEADER_ID
            
            Dim strInvoiceNum As String, index As Integer
            For each guidtemp As Guid In invoiceList
                index = dv.Find(guidtemp.ToByteArray)
                If index <> -1 Then 'found the record
                    strInvoiceNum = dv(index)(ApInvoiceHeader.APInvoiceSearchDV.COL_INVOICE_NUMBER)

                    if dv(index)(ApInvoiceHeader.APInvoiceSearchDV.COL_PAID_AMOUNT) > 0 Then 'invoice paid already, can not be deleted
                        MasterPage.MessageController.AddError(string.Format("{0} - {1}", strInvoiceNum, TranslationBase.TranslateLabelOrMessage("INVOICE PAID")), False)
                        isSelectionValid = False
                    End If
                End If
            Next                
        Else
            'must select at least one invoice to delete
            MasterPage.MessageController.AddError("MSG_NO_RECORD_SELECTED", true)
            isSelectionValid = False
        End If

        If isSelectionValid = False Then
            MasterPage.MessageController.Show
        End If

        Return isSelectionValid
    End function

    Private function validInvoiceSelectionForPayment(Byval strBatchNumber As String, ByVal invoiceList as List(Of Guid)) As Boolean
        Dim isSelectionValid As Boolean = True, blnVendorErr As Boolean = False
        Dim guidVendorId As Guid = Guid.Empty

        'validate selected invoices
        If invoiceList.Count > 0 Then
            Dim dv As DataView = State.SearchDv
            dv.Sort = ApInvoiceHeader.APInvoiceSearchDV.COL_INVOICE_HEADER_ID
            
            Dim strInvoiceNum As String, index As Integer
            dim guidVendorIdNext As Guid

            For each guidtemp As Guid In invoiceList
                index = dv.Find(guidtemp.ToByteArray)
                If index <> -1 Then 'found the record
                    strInvoiceNum = dv(index)(ApInvoiceHeader.APInvoiceSearchDV.COL_INVOICE_NUMBER)
                    guidVendorIdNext = New Guid(CType(dv(index)(ApInvoiceHeader.APInvoiceSearchDV.COL_VENDOR_ID), Byte()))
                    
                    If guidVendorId = Guid.Empty Then 'first value
                        guidVendorId = guidVendorIdNext
                    Else
                        If blnVendorErr = False AndAlso guidVendorId <> guidVendorIdNext Then
                            blnVendorErr = True ' add the error only once
                            isSelectionValid = False
                        End If
                    End If

                    if dv(index)(ApInvoiceHeader.APInvoiceSearchDV.COL_PAID_AMOUNT) > 0 Then 'invoice paid already, can not be paid again
                        MasterPage.MessageController.AddError(string.Format("{0} - {1}", strInvoiceNum, TranslationBase.TranslateLabelOrMessage("INVOICE_ALREADY_PAID")), False)
                        isSelectionValid = False
                    End If

                    if dv(index)(ApInvoiceHeader.APInvoiceSearchDV.COL_UNMATCHED_LINES_COUNT) > 0 Then 'invoice has unmatched lines
                        MasterPage.MessageController.AddError(string.Format("{0} - {1}", strInvoiceNum, TranslationBase.TranslateLabelOrMessage("INVOICE_HAS_UNMATCHED_LINES")), False)
                        isSelectionValid = False
                    End If

                End If
            Next          
            
            If blnVendorErr = True Then ' add the message after the loop
                MasterPage.MessageController.AddError("INVOICES_NOT_FOR_SAME_VENDOR", True)                            
            End If
        Else
            'must select at least one invoice to delete
            MasterPage.MessageController.AddError("MSG_NO_RECORD_SELECTED", true)
            isSelectionValid = False
        End If

        

        'validate batch Number only if invoice selections are valid
        Dim blnValidBatchNum As Boolean = True
        If isSelectionValid = True AndAlso guidVendorId <> Guid.Empty Then
            If String.IsNullOrWhiteSpace(strBatchNumber) Then 'validate batch number is provided
                MasterPage.MessageController.AddError("BATCH_NUMBER_REQUIRED", True)
                isSelectionValid = False
                blnValidBatchNum = False
            Else
                'validate payment batch is not exists for the vendor or the payment batch is still open for
                Dim intErrCode As Integer, strErrMsg As String
                ApPaymentBatch.ValidatePaymentBatch(guidVendorId, strBatchNumber, intErrCode, strErrMsg)

                'batch number is valid if the number doesn't exist or exists but the payment batch is in open status
                If intErrCode <> 0 AndAlso intErrCode <> 100 Then                     
                    If intErrCode = 200 Then
                        'batch number exists but the payment batch is NOT in open status
                        MasterPage.MessageController.AddError("DUPLICATE_BATCH_NUMBER", true)
                    Else 'unknown error, show error message returned from database
                        MasterPage.MessageController.AddError(string.Format("Validate batch number error: {0} - {1}", intErrCode, strErrMsg), False)
                    End If
                    isSelectionValid = False
                    blnValidBatchNum = False
                End If
            End If

            If blnValidBatchNum = False Then
                lblBatchNum.ForeColor = Color.Red
            End If
            
            If isSelectionValid = False Then
                MasterPage.MessageController.Show
            End If
        End If
        
        Return isSelectionValid
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

            'hide search results
            ControlMgr.SetVisibleControl(Me, Grid, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            
            'clear batch number textbox
            txtBatchNum.Text = String.Empty

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try

            ControlMgr.SetVisibleControl(Me, Grid, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, False)
            State.IsGridVisible = False
            txtBatchNum.Text = String.Empty

            If PopulateStateFromSearchFields() Then
                RefreshSearchGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    
    Private Sub btnDelete_WRITE_Click(sender As Object, e As EventArgs) Handles btnDelete_WRITE.Click
        Try
            Dim invoiceToBeDeleted as List(Of Guid) = GetSelectedInvoices
            If validInvoiceSelectionForDelete(invoiceToBeDeleted) Then
                'call delete invoice method for each invoices
                ApInvoiceHeader.DeleteInvoices(invoiceToBeDeleted)
                MasterPage.MessageController.AddSuccess("MSG_RECORD_DELETED_OK")
                RefreshSearchGrid()
            End If            
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCreatePaymentBatch_WRITE_Click(sender As Object, e As EventArgs) Handles btnCreatePaymentBatch_WRITE.Click
        Try
            Dim invoiceToBePaid as List(Of Guid) = GetSelectedInvoices
            Dim strBatchNum As String = txtBatchNum.Text.Trim.ToUpper
            If validInvoiceSelectionForPayment(strBatchNum, invoiceToBePaid) Then
                'call delete invoice method for each invoices
                Dim errCode As Integer, errMsg As String
                ApInvoiceHeader.PayInvoices(strBatchNum, invoiceToBePaid, errCode, errMsg)
                If errCode > 0 Then
                    MasterPage.MessageController.AddErrorAndShow(String.Format("{0} - {1}", errCode.ToString, errMsg), False)
                Else
                    MasterPage.MessageController.AddSuccess("CREATE_PYMT_BATCH_SUCCESS", True)
                End If
                RefreshSearchGrid()            
            End If            
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
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
        Dim btnEditItem As LinkButton
        Dim chkbox As CheckBox
                    
        Try
            
            'If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                If (Not e.Row.Cells(GRID_COL_INVOICE_NUMBER_IDX).FindControl(GRID_COL_INVOICE_NUMBER_CTRL) Is Nothing) Then
                    btnEditItem = CType(e.Row.Cells(GRID_COL_INVOICE_NUMBER_IDX).FindControl(GRID_COL_INVOICE_NUMBER_CTRL), LinkButton)
                    btnEditItem.CommandArgument = e.Row.RowIndex.ToString
                    btnEditItem.CommandName = SELECT_COMMAND_NAME
                    btnEditItem.Text = dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_INVOICE_NUMBER).ToString
                End If

                ' populate dates fields
                If not dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_INVOICE_DATE) Is DBNull.Value Then
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_INVOICE_DATE_IDX), GetLongDateFormattedString(CType(dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_INVOICE_DATE), Date)))                
                End If

                If not dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_DUE_DATE) Is DBNull.Value Then
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DUE_DATE_IDX), GetLongDateFormattedString(CType(dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_DUE_DATE), Date)))
                End If
                
                If not dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_PAYMENT_DATE) Is DBNull.Value Then
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_PYMT_DATE_IDX), GetLongDateFormattedString(CType(dvRow(ApInvoiceHeader.APInvoiceSearchDV.COL_PAYMENT_DATE), Date)))                                
                End If

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

                'calling AP invoice detail pages
                callPage(APInvoiceDetailForm.URL, Me.State.SelectedInvoiceId)

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