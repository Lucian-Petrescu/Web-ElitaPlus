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

    Class LineBatch
        Public LineNumberMin As Integer
        Public LineNumberMax As Integer  
        
        Public Sub New(ByVal intMin As Integer, ByVal intMax As Integer)
            LineNumberMin = intMin
            LineNumberMax = intMax
        End Sub
    End Class
    Class MyState
        Public HasDataChanged As Boolean =False
 
        Public PageIndex As Integer = 0
        Public SelectedInvoiceLineId As Guid = Guid.Empty
        Public myBO As ApInvoiceHeader
        Public myBOExtendedInfo As DataView = Nothing
        Public myBOLines As ApInvoiceLines.APInvoiceLinesDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_GRID_PAGE_SIZE
        Public PrviousLineNumberMin As Integer = 0
        Public PrviousLineNumberMax As Integer = 0        
        Public UnMatchedLineOnly As Boolean = False
        Public LineBatches As Collections.Generic.Stack(Of LineBatch)
        Public CurrentMaxLineNumber As Integer = 0

        Public Sub New()
            LineBatches = new Collections.Generic.Stack(Of LineBatch)
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
                populateLinesGrid(1, LineBatchSize)
                SetGridItemStyleColor(Grid)            
            End If
            
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub APInvoiceDetailForm_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            Dim tempGuid As Guid = CallingPar
            State.myBO = New ApInvoiceHeader(tempGuid)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub APInvoiceDetailForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ShowButtons()
    End Sub
#End Region

#Region "Controlling logic"
    Private sub ShowButtons()
        'show Run Match button only if the invoice has unmatched lines and also not paid yet            
        If State.UnMatchedLineCount > 0 And State.PaidAmount = 0 Then
            ControlMgr.SetVisibleControl(Me, btnRunMatch_WRITE, True)
            ControlMgr.SetVisibleControl(Me, rbGetUnmatched, True)
        Else
            ControlMgr.SetVisibleControl(Me, btnRunMatch_WRITE, False)
            ControlMgr.SetVisibleControl(Me, rbGetUnmatched, False)
        End If

        'enable disable the previous/next batch buttons
        If State.TotalLineCount = State.myBOLines.Count Then 'all lines had been returned
            ControlMgr.SetVisibleControl(Me, btnGetNextBatch, False)
            ControlMgr.SetVisibleControl(Me, btnPreviousBatch, False)
        Else
            If State.LineBatches.Count > 1 Then 
                ControlMgr.SetVisibleControl(Me, btnPreviousBatch, True)
            Else
                ControlMgr.SetVisibleControl(Me, btnPreviousBatch, False)
            End If
            
            If State.myBOLines.Count >= LineBatchSize andalso State.CurrentMaxLineNumber < State.TotalLineCount Then ' there is more to get after max line number in current batch
                ControlMgr.SetVisibleControl(Me, btnGetNextBatch, true)
            Else
                ControlMgr.SetVisibleControl(Me, btnGetNextBatch, False)
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
    Private Sub PopulateFormFromBO()
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

    Private Sub PopulateFormFromBOExtendedInfo()
        If State.myBOExtendedInfo Is Nothing Then
            State.myBOExtendedInfo = State.myBO.GetInvoiceExtendedInfo
        End If
        txtVendor.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_VENDOR)
        txtVendorAddress.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_VENDOR_ADDRESS)
        txtTotalLineCnt.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_TOTAL_LINE_COUNT)
        txtUnMatchedLineCnt.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_UNMATCHED_LINES_COUNT)
        
        If not State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_PAID_AMOUNT) Is DBNull.Value Then
            txtPaidAmount.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_PAID_AMOUNT)
        Else
            txtPaidAmount.Text = String.Empty
        End If

        If not State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_PAID_AMOUNT) Is DBNull.Value Then
            txtPaidAmount.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_PAID_AMOUNT)
        Else
            txtPaidAmount.Text = String.Empty
        End If

        If not State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_MATCHED_AMOUNT) Is DBNull.Value Then
            txtMatchedAmount.Text = State.myBOExtendedInfo(0)(ApInvoiceHeader.APInvoiceSearchDV.COL_MATCHED_AMOUNT)
        Else
            txtMatchedAmount.Text = String.Empty
        End If
        
    End Sub

    Private Sub PopulateLinesGrid(ByVal intMinLineNum As Integer, ByVal intMaxLineNum As Integer)
        If State.myBOLines Is Nothing Then
            State.myBOLines = State.myBO.GetInvoiceLines(intMinLineNum, intMaxLineNum, State.UnMatchedLineOnly, LineBatchSize)

            Dim intMin As Integer, intMax As Integer
            If State.myBOLines.Count > 0 Then
                intMin = State.myBOLines.Table.Compute("Min(line_number)", String.Empty)
                intMax = State.myBOLines.Table.Compute("Max(line_number)", String.Empty)
            Else
                intMin = 0
                intMax = MaxLineNumberAllowed
            End If
            State.CurrentMaxLineNumber = intMax

            If State.LineBatches Is Nothing Then
                State.LineBatches = new Collections.Generic.Stack(Of LineBatch)
            End If
            State.LineBatches.Push(New LineBatch(intMin, intMax))
        End If

        Grid.PageSize = State.selectedPageSize

        Grid.DataSource = State.myBOLines
        Grid.PageIndex = State.PageIndex

        HighLightSortColumn(Grid, String.Empty, IsNewUI)

        Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, True)

        ControlMgr.SetVisibleControl(Me, trPageSize, True)

        Session("recCount") = State.myBOLines.Count

        If Grid.Visible Then
            If State.UnMatchedLineOnly = True Then
                lblRecordCount.Text = State.UnMatchedLineCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND) & " " & State.myBOLines.Count & " " & TranslationBase.TranslateLabelOrMessage("MSG_RECORDS_DISPLAYED")
            Else
                lblRecordCount.Text = State.TotalLineCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND) & " " & State.myBOLines.Count & " " & TranslationBase.TranslateLabelOrMessage("MSG_RECORDS_DISPLAYED")
            End If
            
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
            populateLinesGrid(0, 0)
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
            populateLinesGrid(0, 0)
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
    private Sub RefreshScreen()
        State.myBO = New ApInvoiceHeader(State.myBO.id)
        State.myBOExtendedInfo = Nothing
        State.myBOLines = Nothing
        State.PageIndex = 0 'show first page after refresh
        State.LineBatches = Nothing
        State.UnMatchedLineOnly = False 'get all lines after match
        rbGetAll.Checked = True
        rbGetUnmatched.Checked = False

        populateFormFromBO
        populateFormFromBOExtendedInfo
        If State.UnMatchedLineOnly = True Then
            populateLinesGrid(1, MaxLineNumberAllowed)
        Else
            populateLinesGrid(1, LineBatchSize)
        End If
        
    End Sub
    Private Sub btnRunMatch_WRITE_Click(sender As Object, e As EventArgs) Handles btnRunMatch_WRITE.Click
        Try
            'call delete invoice method for each invoices
            dim matchedCnt As Integer = ApInvoiceHeader.MatchInvoice(State.myBO.id)

            Dim strSuccessMsg As String = TranslationBase.TranslateLabelOrMessage("AP_INVOICE_MATCH_SUCCESS") & ": " & matchedCnt
            MasterPage.MessageController.AddSuccess(strSuccessMsg, False)

            If matchedCnt > 0 Then
                State.HasDataChanged = True
                RefreshScreen()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnGetNextBatch_Click(sender As Object, e As EventArgs) Handles btnGetNextBatch.Click
        Try
            State.myBOLines = Nothing
            State.PageIndex = 0

            Dim intMin As Integer, intMax As Integer

            intMin = State.CurrentMaxLineNumber + 1                
            If State.UnMatchedLineOnly = true then
                intMax = MaxLineNumberAllowed
            Else
                intMax = intMin + LineBatchSize
            End If
            
            PopulateLinesGrid(intMin, intMax)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnPreviousBatch_Click(sender As Object, e As EventArgs) Handles btnPreviousBatch.Click
        Try
            State.myBOLines = Nothing
            State.PageIndex = 0 
            State.LineBatches.Pop() 'remove the current batch
            dim previousBatch As LineBatch = State.LineBatches.Pop() 'also remove previous batch since it will be added back again
            PopulateLinesGrid(previousBatch.LineNumberMin, previousBatch.LineNumberMax)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub rbGetAll_CheckedChanged(sender As Object, e As EventArgs) Handles rbGetAll.CheckedChanged
        If rbGetAll.Checked = True Then
            State.myBOLines = Nothing
            State.PageIndex = 0
            State.LineBatches = Nothing

            State.UnMatchedLineOnly = False
            populateLinesGrid(1, LineBatchSize)
        End If
    End Sub

    Private Sub rbGetUnmatched_CheckedChanged(sender As Object, e As EventArgs) Handles rbGetUnmatched.CheckedChanged
        If rbGetUnmatched.Checked = True Then
            State.myBOLines = Nothing
            State.PageIndex = 0
            State.LineBatches = Nothing

            State.UnMatchedLineOnly = True
            populateLinesGrid(1, MaxLineNumberAllowed)
        End If
    End Sub

    
#End Region
End Class