Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security

Partial Class APInvoiceDetailForm
    Inherits ElitaPlusSearchPage

#Region "Constants"

    Public Const URL As String = "~/Claims/AccountPayable/APInvoiceDetailForm.aspx"
    Public Const PAGETITLE = "AP_INVOICE_DETAIL"
    Public Const PAGETAB As String = "CLAIM"
    Public Const LineBatchSize As Integer = 100
    Public Const MaxLineNumberAllowed As Integer = 99999999
    Public Const DEFAULT_GRID_PAGE_SIZE As Integer = 15
#End Region

#Region "Page State"

    Class MyState
        Public HasDataChanged As Boolean =False
 
        Public PageIndex As Integer = 0
        Public SelectedInvoiceLineId As Guid = Guid.Empty
        Public myBO As ApInvoiceHeader
        Public myBOExtendedInfo As DataView = Nothing
        Public myBOLines As ApInvoiceLines.APInvoiceLinesDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_GRID_PAGE_SIZE
        Public minLineNumber As Integer = 0
        Public maxLineNumber As Integer = MaxLineNumberAllowed

        Public Sub New()
        End Sub

        Public ReadOnly Property TotalLineCount As Integer
            Get
                If myBOExtendedInfo Is Nothing Then
                    myBOExtendedInfo = myBO.GetInvoiceExtendedInfo
                End If
                Dim intCnt As Integer = myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_TOTAL_LINE_COUNT)
                Return intCnt
            End Get
        End Property

        Public ReadOnly Property UnMatchedLineCount As Integer
            Get
                If myBOExtendedInfo Is Nothing Then
                    myBOExtendedInfo = myBO.GetInvoiceExtendedInfo
                End If
                Dim intCnt As Integer = myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_UNMATCHED_LINES_COUNT)
                Return intCnt
            End Get
        End Property

        Public ReadOnly Property PaidAmount As Double
            Get
                If myBOExtendedInfo Is Nothing Then
                    myBOExtendedInfo = myBO.GetInvoiceExtendedInfo
                End If
                Dim dblAmt As Double = myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_PAID_AMOUNT)
                Return dblAmt
            End Get
        End Property
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

#Region "Page Return Type"
    Public Class ReturnType
        Public DataChanged As Boolean = False
        Public Sub New(ByVal boChanged As Boolean)
            Me.DataChanged = boChanged
        End Sub
    End Class
#End Region

#Region "Page Event handler"
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End If
    End Sub

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear_Hide()
        Try
            If Not IsPostBack Then
                UpdateBreadCrum()

                'ControlMgr.SetVisibleControl(Me, trPageSize, False)
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                
                populateFormFromBO
                populateFormFromBOExtendedInfo

                TranslateGridHeader(Grid)
                cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                Grid.PageSize = Me.State.selectedPageSize
                populateLinesGrid()
                SetGridItemStyleColor(Grid)            
            End If
            SetControlStatus()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        'ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub APInvoiceDetailForm_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            Dim tempGuid As Guid = CallingPar
            State.myBO = New ApInvoiceHeader(tempGuid)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Controlling logic"
    Private sub SetControlStatus()
        'show Run Match button only if the invoice has unmatched lines and also not paid yet            
        If State.UnMatchedLineCount > 0 And State.PaidAmount = 0 Then
            ControlMgr.SetVisibleControl(Me, btnRunMatch_WRITE, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnRunMatch_WRITE, False)
        End If
        If State.TotalLineCount = State.myBOLines.Count Then 'all lines had been returned
            ControlMgr.SetVisibleControl(Me, btnGetNextBatch, False)
            ControlMgr.SetVisibleControl(Me, btnPreviousBatch, False)
        Else
            If State.minLineNumber > 1 Then 
                ControlMgr.SetVisibleControl(Me, btnPreviousBatch, true)
            Else
                ControlMgr.SetVisibleControl(Me, btnPreviousBatch, true)
            End If
            
            If State.maxLineNumber < State.TotalLineCount Then ' there is more to get after max line number in current batch
                ControlMgr.SetVisibleControl(Me, btnPreviousBatch, true)
            Else
                ControlMgr.SetVisibleControl(Me, btnPreviousBatch, False)
            End If
        End If
    End sub
    Private Function GetDescriptionFromExtendedCode(ByVal list As DataElements.ListItem(), ByVal lookupValue As String) As String
        For Each item As DataElements.ListItem In list
            If item.ExtendedCode = lookupValue Then
                Return item.Description
            End If
        Next
        Return String.Empty
    End Function

    Private Function GetDescriptionFromCode(ByVal list As DataElements.ListItem(), ByVal lookupValue As String) As String
        For Each item As DataElements.ListItem In list
            If item.Code = lookupValue Then
                Return item.Description
            End If
        Next
        Return String.Empty
    End Function
    Private Sub populateFormFromBO()
        With Me.State.myBO
            PopulateControlFromBOProperty(txtInvoiceNumber, .InvoiceNumber)
            PopulateControlFromBOProperty(txtInvoiceAmount, .InvoiceAmount)
            PopulateControlFromBOProperty(txtInvoiceDate, .InvoiceDate)
            PopulateControlFromBOProperty(txtSource, .Source)
            PopulateControlFromBOProperty(txtAccountingPeriod, .AccountingPeriod)
            'get the translation from list code PMTTRM (Payment Term)
            Dim tempList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="PMTTRM", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            PopulateControlFromBOProperty(txtTerm, GetDescriptionFromExtendedCode(tempList,.TermXcd))
            PopulateControlFromBOProperty(txtCurrency, .CurrencyIsoCode)

            'todo, get the translation from list code YESNO (Yes No list)
            Dim oYesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            PopulateControlFromBOProperty(txtApproved, GetDescriptionFromExtendedCode(oYesNoList,.ApprovedXcd))
            PopulateControlFromBOProperty(txtPosted, GetDescriptionFromCode(oYesNoList,.Posted))
            PopulateControlFromBOProperty(txtDistributed, GetDescriptionFromCode(oYesNoList,.Distributed))
        End With
    End Sub

    Private Sub populateFormFromBOExtendedInfo()
        If State.myBOExtendedInfo Is Nothing Then
            State.myBOExtendedInfo = State.myBO.GetInvoiceExtendedInfo
        End If
        txtVendor.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_VENDOR)
        txtVendorAddress.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_VENDOR_ADDRESS)
        txtTotalLineCnt.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_TOTAL_LINE_COUNT)
        txtUnMatchedLineCnt.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_UNMATCHED_LINES_COUNT)
        txtPaidAmount.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_PAID_AMOUNT)
        txtMatchedAmount.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_MATCHED_AMOUNT)
    End Sub

    Private Sub populateLinesGrid()
        If State.myBOLines Is Nothing Then
            If State.maxLineNumber = 0 Then State.maxLineNumber = MaxLineNumberAllowed
            State.myBOLines = State.myBO.GetInvoiceLines(State.minLineNumber, State.maxLineNumber, False, LineBatchSize)

            State.minLineNumber = State.myBOLines.Table.Compute("Min(line_number)", String.Empty)
            State.maxLineNumber = State.myBOLines.Table.Compute("Max(line_number)", String.Empty)
        End If

        Grid.PageSize = State.selectedPageSize

        Grid.DataSource = State.myBOLines
        State.PageIndex = Grid.PageIndex

        HighLightSortColumn(Grid, String.Empty, IsNewUI)

        Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, True)

        ControlMgr.SetVisibleControl(Me, trPageSize, True)

        Session("recCount") = State.myBOLines.Count

        If Grid.Visible Then
            lblRecordCount.Text = State.TotalLineCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND) & " " & State.myBOLines.Count & " " & TranslationBase.TranslateLabelOrMessage("MSG_RECORDS_DISPLAYED")
        End If
    End Sub
#End Region

#Region "Grid Event handlers"

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Private Sub cboPageSize_SelectedIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.myBOLines.Count, State.selectedPageSize)
            Grid.PageIndex = NewCurrentPageIndex(Grid, Session("recCount"),cboPageSize.SelectedValue)
            populateLinesGrid()
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
            populateLinesGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button handlers"
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try            
            ReturnToCallingPage(new ReturnType(State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnRunMatch_WRITE_Click(sender As Object, e As EventArgs) Handles btnRunMatch_WRITE.Click
        Try
            'call delete invoice method for each invoices
            ApInvoiceHeader.MatchInvoice(State.myBO.id)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
End Class