Imports System.Globalization

Partial Class APInvoiceListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"


    Public Const PAGETITLE = "AP_INVOICE_SEARCH"
    Public Const PAGETAB As String = "CLAIM"

    Public Const GRID_COL_INVOICE_NUMBER_CTRL As String = "btnInvoiceDetails"

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
#End Region
End Class