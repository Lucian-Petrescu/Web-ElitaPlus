Imports System.Diagnostics
Imports System.Reflection
Imports System.Threading

Partial Class ClaimPaymentAdjustmentsForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents ErrorCtrl As ErrorController

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As Object

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Instance Variables "
    Private blnAdjustPaymentButtonEnabledState As Boolean = False
    Private blnReversePaymentButtonEnabledState As Boolean = False
    Private blnNewPaymentButtonEnabledState As Boolean = False
    Private ButtonClicked As String
#End Region

#Region "Constants"
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_CLAIM_INVOICE_ID_IDX As Integer = 1
    Public Const GRID_COL_INVOICE_NUMBER_IDX As Integer = 2
    Public Const GRID_COL_PAYEE_IDX As Integer = 3
    Public Const GRID_COL_CREATED_DATE_IDX As Integer = 4
    Public Const GRID_COL_INVOICE_AMOUNT_IDX As Integer = 5
    Public Const GRID_COL_PAID_BY_IDX As Integer = 6

    Public Const GRID_TOTAL_COLUMNS As Integer = 7

    Public Const MAX_LIMIT As Integer = 1000

    Public Const LOADING_PAYMENTS_MSG As String = "Loading_Payments"
    Private Const REVERSE_PAYMENT As String = "R"
    Private Const ADJUST_PAYMENT As String = "A"
    Private Const NEW_PAYMENT As String = "N"

    Private Const CLAIM_CLOSED_STATUS As String = "C"

    Public Const DISBURSMENT_COL_PAYEE_NAME_IDX As Integer = 11
    Public Const DISBURSMENT_COL_PAYEE_ADDRESS_ID_IDX As Integer = 27
    Public Const DISBURSMENT_COL_PAYEE_OPTION_IDX As Integer = 28

    Public Const URL As String = "~/Claims/ClaimPaymentAdjustmentsForm.aspx"

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimInvoice
        Public BoChanged As Boolean = False
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ClaimInvoice, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
    End Class
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As Claim
        Public PayClaimID As Guid
        Public ViewOnly As Boolean = False
        Public Sub New(claimBO As Claim)
            Me.ClaimBO = claimBO
            PayClaimID = Guid.Empty
            ViewOnly = False
        End Sub
        Public Sub New(claimInvoiceID As Guid)
            ClaimBO = Nothing
            PayClaimID = claimInvoiceID
            ViewOnly = True
        End Sub

    End Class
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public ClaimInvoiceBO As ClaimInvoice
        Public SecondClaimInvoiceBO As ClaimInvoice
        Public OldClaimInvoiceBO As ClaimInvoice
        Public DisbursementBO As Disbursement
        Public SecondDisbursementBO As Disbursement
        Public OldDisbursementBO As Disbursement
        Public CommentBO As Comment
        Public AdjustmentPercentage As Decimal
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 10
        Public selectedClaimNumber As String = String.Empty
        Public selectedClaimID As Guid = Guid.Empty
        Public customerName As String = String.Empty
        Public serviceCenter As String = String.Empty
        Public svcControlNumber As String = String.Empty
        Public ClaimInvoiceDV As ClaimInvoice.ClaimInvoicesDV = Nothing
        Public ClaimStatusCode As String = Nothing
        Public SortColumns(GRID_TOTAL_COLUMNS - 1) As String
        Public IsSortDesc(GRID_TOTAL_COLUMNS - 1) As Boolean
        Public selectedClaimInvoiceId As Guid = Guid.Empty

        Public IsGridVisible As Boolean = False
        Public decTotalPaid As Decimal = 0
        Public NavigateToComment As Boolean = False
        Public NavigateToPayInvoice As Boolean = False

        Public ChangesMade As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public inputParameters As Parameters
        Public ForwardedParameters As ClaimPaymentAdjustmentsListForm.ForwardedParameters

        Sub New()
        End Sub
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    'Protected Shadows ReadOnly Property State() As MyState
    '    Get
    '        If Me.NavController.State Is Nothing Then
    '            Me.NavController.State = New MyState
    '            Me.State.inputParameters = CType(Me.NavController.ParametersPassed, Parameters)
    '        End If
    '        Return CType(Me.NavController.State, MyState)
    '    End Get
    'End Property

    Protected Shadows ReadOnly Property State() As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                Me.State.inputParameters = CType(NavController.ParametersPassed, Parameters)
            Else
                If NavController.IsFlowEnded Then
                    'restart flow
                    Dim s As MyState = CType(NavController.State, MyState)
                    StartNavControl()
                    NavController.State = s
                End If
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                StartNavControl()
                State.ForwardedParameters = CType(CallingParameters, ClaimPaymentAdjustmentsListForm.ForwardedParameters)
                State.selectedClaimNumber = State.ForwardedParameters.claimNumber ' CType(Me.CallingParameters, String)
                State.selectedClaimID = State.ForwardedParameters.ClaimId ' CType(Me.CallingParameters, String)
                State.customerName = State.ForwardedParameters.customerName
                State.serviceCenter = State.ForwardedParameters.serviceCenter
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        Try
            If Not IsPostBack Then
                InitializePage()
                AddCalendar(ImageButtonInvoiceDate, txtInvoiceDate)
            Else
                State.decTotalPaid = CType(Session("TotalAmountPaid"), Decimal)
                blnNewPaymentButtonEnabledState = CType(Session("NewPaymentButtonEnabledState"), Boolean)
                blnReversePaymentButtonEnabledState = CType(Session("ReversePaymentButtonEnabledState"), Boolean)
                blnAdjustPaymentButtonEnabledState = CType(Session("AdjustPaymentButtonEnabledState"), Boolean)
                ButtonClicked = CType(Session("ButtonClicked"), String)
                ClearErrLabels()
            End If
            cboPageSize.Enabled = True
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub

    Private Sub InitializePage()
        SetGridItemStyleColor(Grid)

        txtClaimNumber.Text = State.selectedClaimNumber
        txtCustomerName.Text = State.customerName
        txtServiceCenter.Text = State.serviceCenter

        LoadClaimPayments()

        ControlMgr.SetVisibleControl(Me, trTotalPaid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, tblButtons, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, tblPaymentDetails, False)
        ControlMgr.SetVisibleControl(Me, tblHR, False)

        If State.IsGridVisible AndAlso Not (State.PageSize = 10) Then
            cboPageSize.SelectedValue = CType(State.PageSize, String)
            Grid.PageSize = State.PageSize
        End If

        SetGridItemStyleColor(Grid)
        MenuEnabled = True
        lblADJUSTMENT_AMOUNT.Text = lblADJUSTMENT_AMOUNT.Text & ":"
        lblInvoiceDate.Text = lblInvoiceDate.Text & ":"
    End Sub
#End Region

#Region "Controlling Logic"
    Public Sub GetClaimStatusCode()
        Try
            State.ClaimStatusCode = ClaimFacade.Instance.GetClaim(Of Claim)(State.selectedClaimID).StatusCode

            'NEW_PAYMENT option will be enabled only if the claim status is Closed
            If State.ClaimStatusCode = CLAIM_CLOSED_STATUS Then
                btnNEW_PAYMENT.Enabled = True
                blnNewPaymentButtonEnabledState = True
            Else
                blnNewPaymentButtonEnabledState = False
                btnNEW_PAYMENT.Enabled = False
            End If
            Session("NewPaymentButtonEnabledState") = blnNewPaymentButtonEnabledState
        Catch ex As Exception

        End Try
    End Sub

    Public Sub PopulateGrid()
        Dim foundLabel As String
        Dim errors() As ValidationError = {New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

        State.decTotalPaid = 0
        ToggleButtons(blnNewPaymentButtonEnabledState, False, False)

        If (State.ClaimInvoiceDV Is Nothing) Then
            Dim searchFollowUpDate As String = Nothing
            Dim sortBy As String
            Dim x As Boolean = State.NavigateToComment
            Dim oCompanyId As Guid = ClaimFacade.Instance.GetClaim(Of Claim)(State.selectedClaimID).CompanyId
            State.ClaimInvoiceDV = ClaimInvoice.getPaymentsList(oCompanyId, State.selectedClaimNumber)
            ValidSearchResultCount(State.ClaimInvoiceDV.Count, True)
        End If

        State.ClaimInvoiceDV.Sort = Grid.DataMember
        Grid.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(State.ClaimInvoiceDV, State.selectedClaimInvoiceId, Grid, State.PageIndex)
        State.PageIndex = Grid.CurrentPageIndex
        Grid.DataSource = State.ClaimInvoiceDV
        Grid.AllowSorting = False
        Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        ControlMgr.SetVisibleControl(Me, trTotalPaid, Grid.Visible)

        ControlMgr.SetVisibleControl(Me, tblButtons, State.IsGridVisible)

        Session("recCount") = State.ClaimInvoiceDV.Count

        Session("TotalAmountPaid") = State.decTotalPaid

        foundLabel = State.ClaimInvoiceDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        If State.ClaimInvoiceDV.Count > 0 Then
            If Grid.Visible Then
                lblRecordCount.Text = foundLabel
                txtTotalPaid.Text = GetAmountFormattedDoubleString(State.decTotalPaid.ToString)
            End If
        Else
            If Grid.Visible Then
                lblRecordCount.Text = foundLabel
                txtTotalPaid.Text = ""
            End If
        End If

    End Sub

    Private Sub ToggleButtons(blnNewPaymentEnabled As Boolean, blnReversePaymentEnabled As Boolean, blnAdjustPaymentEnabled As Boolean)
        btnNEW_PAYMENT.Enabled = blnNewPaymentEnabled
        btnREVERSE_PAYMENT.Enabled = blnReversePaymentEnabled
        btnADJUST_PAYMENT.Enabled = blnAdjustPaymentEnabled
        DisableButtonsForClaimSystem()
    End Sub
    Protected Sub DisableButtonsForClaimSystem()
        If Not State.selectedClaimID.Equals(Guid.Empty) Then
            Dim ClaimBO As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(State.selectedClaimID)
            If Not ClaimBO.CertificateId.Equals(Guid.Empty) Then
                Dim oCert As New Certificate(ClaimBO.CertificateId)
                Dim oDealer As New Dealer(oCert.DealerId)
                Dim oClmSystem As New ClaimSystem(oDealer.ClaimSystemId)
                Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                If oClmSystem.PayClaimId.Equals(noId) Then
                    If btnNEW_PAYMENT.Visible AndAlso btnNEW_PAYMENT.Enabled Then
                        ControlMgr.SetEnableControl(Me, btnNEW_PAYMENT, False)
                    End If
                    If btnREVERSE_PAYMENT.Visible AndAlso btnREVERSE_PAYMENT.Enabled Then
                        ControlMgr.SetEnableControl(Me, btnREVERSE_PAYMENT, False)
                    End If
                    If btnADJUST_PAYMENT.Visible AndAlso btnADJUST_PAYMENT.Enabled Then
                        ControlMgr.SetEnableControl(Me, btnADJUST_PAYMENT, False)
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub LoadClaimPayments()
        Try
            State.PageIndex = 0
            State.selectedClaimInvoiceId = Guid.Empty
            State.IsGridVisible = True
            State.ClaimInvoiceDV = Nothing
            GetClaimStatusCode()
            PopulateGrid()
            'check remaining amount to decide whether the new payment button should be enabled
            If (State.ClaimInvoiceDV IsNot Nothing) AndAlso State.ClaimInvoiceDV.Count > 0 Then
                Dim guidCI As Guid = New Guid(CType(State.ClaimInvoiceDV.Item(0)(ClaimInvoice.ClaimInvoicesDV.COL_CLAIM_INVOICE_ID), Byte()))
                Dim dRemainingAMT As Decimal = (New ClaimInvoice(guidCI)).RemainingAmount.Value
                If dRemainingAMT <= 0 Then
                    btnNEW_PAYMENT.Enabled = False
                    blnNewPaymentButtonEnabledState = False
                    Session("NewPaymentButtonEnabledState") = blnNewPaymentButtonEnabledState
                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Protected Sub PopulateBOsFromForm()
        PopulateBOProperty(State.ClaimInvoiceBO, "AdjustmentAmount", txtADJUSTMENT_AMOUNT)
        PopulateBOProperty(State.ClaimInvoiceBO, "InvoiceDate", txtInvoiceDate)
        PopulateBOProperty(State.ClaimInvoiceBO, "SvcControlNumber", txtInvoiceNumber)
        PopulateBOProperty(State.ClaimInvoiceBO, "TotalAmount", txtTotalPaid)

        PopulateBOProperty(State.SecondClaimInvoiceBO, "AdjustmentAmount", txtADJUSTMENT_AMOUNT)
        PopulateBOProperty(State.SecondClaimInvoiceBO, "InvoiceDate", txtInvoiceDate)
        PopulateBOProperty(State.SecondClaimInvoiceBO, "SvcControlNumber", txtInvoiceNumber)
        PopulateBOProperty(State.SecondClaimInvoiceBO, "TotalAmount", txtTotalPaid)
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub ReversePayment(blnReversal As Boolean)

        'create new objects
        State.ClaimInvoiceBO = New ClaimInvoice
        State.ClaimInvoiceBO.IsPaymentAdjustment = False
        State.ClaimInvoiceBO.IsPaymentReversal = True
        State.DisbursementBO = State.ClaimInvoiceBO.AddNewDisbursement()

        'create Old objects
        State.OldClaimInvoiceBO = New ClaimInvoice(State.selectedClaimInvoiceId)
        'Me.State.ClaimInvoiceBO.ClaimTax = Me.State.OldClaimInvoiceBO.GetClaimTax
        State.OldDisbursementBO = New Disbursement(State.OldClaimInvoiceBO.DisbursementId)

        'Populate new ClaimInvoice BO using the existing BO
        State.ClaimInvoiceBO.PopulateClaimInvoice(State.OldClaimInvoiceBO)
        State.ClaimInvoiceBO.SvcControlNumber = txtInvoiceNumber.Text
        PopulateBOProperty(State.ClaimInvoiceBO, "InvoiceDate", txtInvoiceDate)

        'Reverse new ClaimInvoice
        State.ClaimInvoiceBO.ReverseClaimInvoice()

        'create comment Only after ClaimInvoiceBO is populated
        If blnReversal Then
            State.CommentBO = State.ClaimInvoiceBO.AddNewComment
        End If

        'Populate new Disbursement BO using the existing BO
        State.DisbursementBO.PopulateDisbursement(State.OldDisbursementBO)
        State.DisbursementBO.SvcControlNumber = txtInvoiceNumber.Text
        State.DisbursementBO.PaymentDate = New DateType(Date.Today())
        PopulateBOProperty(State.DisbursementBO, "InvoiceDate", txtInvoiceDate)

        Dim RemainingPymt, InvoiceIVA, InvoiceIIBB As Decimal
        Dim blnReverseInvoiceTax As Boolean = False

        With State.OldDisbursementBO
            If blnReversal AndAlso .PaymentAmount.Value > 0 Then
                If State.OldClaimInvoiceBO.isTaxTypeInvoice Then 'Also Reverse the invoice tax if the whole invoice is reversed
                    Disbursement.GetRemainingInvoicePaymentAfterReversal(.SvcControlNumber, .Id, .ClaimId, RemainingPymt, InvoiceIVA, InvoiceIIBB)
                    If RemainingPymt = 0 AndAlso (InvoiceIVA <> 0 OrElse InvoiceIIBB <> 0) Then
                        blnReverseInvoiceTax = True
                    End If
                End If
            End If
        End With
        'Reverse new Disbursement
        State.DisbursementBO.ReverseDisbursement(blnReverseInvoiceTax, InvoiceIVA, InvoiceIIBB)

    End Sub

    Private Sub AdjustPayment()
        'The reversal part has been done.

        'Create New ClaimInvoice
        State.SecondClaimInvoiceBO = State.ClaimInvoiceBO.CreateSecondClaimInvoice()

        'Set the payment type
        State.SecondClaimInvoiceBO.IsPaymentAdjustment = True
        State.SecondClaimInvoiceBO.IsPaymentReversal = False
        State.ClaimInvoiceBO.IsPaymentAdjustment = True
        State.ClaimInvoiceBO.IsPaymentReversal = False

        'Me.State.DisbursementBO.LoadAgain()
        State.SecondDisbursementBO = State.SecondClaimInvoiceBO.AddNewDisbursement()

        'Populate new ClaimInvoice BO using the existing BO
        State.SecondClaimInvoiceBO.PopulateClaimInvoice(State.OldClaimInvoiceBO)
        PopulateBOsFromForm()

        'Memorize AdjustmentPercentage
        State.AdjustmentPercentage = State.SecondClaimInvoiceBO.AdjustmentPercentage

        'Adjust Amounts in SecondClaimInvoiceBO
        State.SecondClaimInvoiceBO.AdjustClaimInvoiceAmounts(State.AdjustmentPercentage)

        'create comment Only after ClaimInvoiceBO is populated
        State.CommentBO = State.ClaimInvoiceBO.AddNewComment

        'Populate new Disbursement BO using the existing BO
        State.SecondDisbursementBO.PopulateDisbursement(State.OldDisbursementBO)
        State.SecondDisbursementBO.SvcControlNumber = txtInvoiceNumber.Text
        PopulateBOProperty(State.SecondDisbursementBO, "InvoiceDate", txtInvoiceDate)
        State.SecondDisbursementBO.PaymentDate = New DateType(Date.Today())

        'Adjust Amounts in SecondDisbursementBO
        State.SecondDisbursementBO.AdjustDisbursementAmounts(State.AdjustmentPercentage)

    End Sub

#End Region

#Region " Datagrid Related "

    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
            PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CLAIM_INVOICE_ID_IDX), dvRow(ClaimInvoice.ClaimInvoicesDV.COL_CLAIM_INVOICE_ID))
            PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_INVOICE_NUMBER_IDX), dvRow(ClaimInvoice.ClaimInvoicesDV.COL_INVOICE_NUMBER))
            PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_PAYEE_IDX), dvRow(ClaimInvoice.ClaimInvoicesDV.COL_PAYEE))
            PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CREATED_DATE_IDX), dvRow(ClaimInvoice.ClaimInvoicesDV.COL_CREATED_DATE))
            PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_INVOICE_AMOUNT_IDX), dvRow(ClaimInvoice.ClaimInvoicesDV.COL_INVOICE_AMOUNT))
            PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_PAID_BY_IDX), dvRow(ClaimInvoice.ClaimInvoicesDV.COL_PAID_BY))
            State.decTotalPaid += CType(dvRow(ClaimInvoice.ClaimInvoicesDV.COL_INVOICE_AMOUNT), Decimal)
        End If
    End Sub

    Public Sub ItemCommand(source As Object, e As DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" AndAlso Grid.Enabled Then
                Grid.SelectedIndex = e.Item.ItemIndex
                State.selectedClaimInvoiceId = New Guid(e.Item.Cells(GRID_COL_CLAIM_INVOICE_ID_IDX).Text)
                Dim decPaymentAmount As Decimal = CType(e.Item.Cells(GRID_COL_INVOICE_AMOUNT_IDX).Text, Decimal)

                'REVERSE_PAYMENT option is enabled only if the selected record has a positive payment amount and the amount of the 
                'selected record, when applied, will not cause the total of all payments to become negative.

                'If decPaymentAmount > 0 AndAlso (Me.State.decTotalPaid - decPaymentAmount >= 0) Then

                'the reverse payment shud be allowed selected record has a non-zero payment amount(+ve or -ve)
                ' per ofer - since a recovery claim ( which is -ve amt) shud be alloed to be reversed
                If (decPaymentAmount <> 0) AndAlso (State.decTotalPaid - decPaymentAmount >= 0) Then
                    If (decPaymentAmount < 0) Then
                        blnReversePaymentButtonEnabledState = False
                    Else
                        blnReversePaymentButtonEnabledState = True
                    End If
                Else
                    blnReversePaymentButtonEnabledState = False
                End If

                'ADJUST_PAYMENT option will be presented only if the claim status is Closed and the selected
                ' record has a positive payment amount
                If State.ClaimStatusCode = CLAIM_CLOSED_STATUS AndAlso decPaymentAmount > 0 Then
                    blnAdjustPaymentButtonEnabledState = True
                Else
                    blnAdjustPaymentButtonEnabledState = False
                End If

                ToggleButtons(blnNewPaymentButtonEnabledState, blnReversePaymentButtonEnabledState, blnAdjustPaymentButtonEnabledState)
                Session("ReversePaymentButtonEnabledState") = blnReversePaymentButtonEnabledState
                Session("AdjustPaymentButtonEnabledState") = blnAdjustPaymentButtonEnabledState

            End If
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.selectedClaimInvoiceId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.PageSize = Grid.PageSize
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region " Buttons Clicks "
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.ClaimInvoiceBO, State.ChangesMade))
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrorCtrl.Text
        End Try

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If ButtonClicked = REVERSE_PAYMENT Then
                '**************************************REVERSE PAYMENT********************************
                'The process of Reversing a payment consist of creating three records:
                '1- ClaimInvoice record (Old record reversed)
                '2- Disbursment record  (Old record reversed)
                '3- Comment record      (New)
                '****************************************************************************
                If txtInvoiceNumber.Text = "" Then
                    SetLabelError(lblINVOICE_NUMBER)
                    Throw New GUIException(Message.MSG_INVOICE_NUMBER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_NUMBER_MUST_BE_ENTERED_ERR)
                End If

                Dim oCompaniesDv As DataView, oUser As New User
                oCompaniesDv = oUser.GetUserCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
                oCompaniesDv.RowFilter = "code in ('ABA','VBA','SBA')"
                If oCompaniesDv.Count > 0 Then
                    If txtInvoiceDate.Text.Trim = "" Then
                        SetLabelError(lblInvoiceDate)
                        Throw New GUIException(Message.MSG_ADJUSTMENT_AMOUNT_MUST_BE_ENTERED, Assurant.ElitaPlus.Common.ErrorCodes.INVOICE_DATE_REQUIRED_ERR)
                    Else
                        If Not Microsoft.VisualBasic.IsDate(txtInvoiceDate.Text) Then
                            Throw New GUIException(Message.MSG_ADJUSTMENT_AMOUNT_MUST_BE_ENTERED, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_INVOICE_DATE_ENTERED_ERR)
                        End If
                    End If
                End If

                ReversePayment(True)
            ElseIf ButtonClicked = ADJUST_PAYMENT Then
                '*************************************ADJUST PAYMENT***********************************
                'The process of Adjusting a payment consist of creating five records:
                '1- ClaimInvoice record (Old record reversed)
                '2- Disbursment record  (Old record reversed)
                '3- ClaimInvoice record (New record adjusted)
                '4- Disbursment record  (New record adjusted)
                '5- Comment record      (New)
                '****************************************************************************
                If txtInvoiceNumber.Text = "" Then
                    SetLabelError(lblINVOICE_NUMBER)
                    Throw New GUIException(Message.MSG_INVOICE_NUMBER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_NUMBER_MUST_BE_ENTERED_ERR)
                End If

                Dim oCompaniesDv As DataView, oUser As New User
                oCompaniesDv = oUser.GetUserCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
                oCompaniesDv.RowFilter = "code in ('ABA','VBA','SBA')"
                If oCompaniesDv.Count > 0 Then
                    If txtInvoiceDate.Text.Trim = "" Then
                        SetLabelError(lblInvoiceDate)
                        Throw New GUIException(Message.MSG_ADJUSTMENT_AMOUNT_MUST_BE_ENTERED, Assurant.ElitaPlus.Common.ErrorCodes.INVOICE_DATE_REQUIRED_ERR)
                    Else
                        If Not Microsoft.VisualBasic.IsDate(txtInvoiceDate.Text) Then
                            Throw New GUIException(Message.MSG_ADJUSTMENT_AMOUNT_MUST_BE_ENTERED, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_INVOICE_DATE_ENTERED_ERR)
                        End If
                    End If
                End If

                Dim blnInvalidTtl As Boolean = False, dAdjAmt As Decimal = 0

                If txtADJUSTMENT_AMOUNT.Text = "" Then
                    SetLabelError(lblADJUSTMENT_AMOUNT)
                    Throw New GUIException(Message.MSG_ADJUSTMENT_AMOUNT_MUST_BE_ENTERED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ADJUSTMENT_AMOUNT_MUST_BE_ENTERED_ERR)
                Else
                    Try
                        'Dim CIBO As New ClaimInvoice
                        'Me.PopulateBOProperty(CIBO, "AdjustmentAmount", Me.txtADJUSTMENT_AMOUNT.Text)
                        'CIBO = Nothing
                        'YX: instead of create a dummy new BO, using parse function to test amount in correct format
                        dAdjAmt = Decimal.Parse(txtADJUSTMENT_AMOUNT.Text.Trim())
                    Catch ex As Exception
                        SetLabelError(lblADJUSTMENT_AMOUNT)
                        Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.MSG_INVALID_AMOUNT_ENTERED, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)
                    End Try

                    'Total paid can't go negative after adjustment
                    If State.decTotalPaid - dAdjAmt < 0 Then
                        Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.MSG_INVALID_AMOUNT_ENTERED, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_TOTAL_PAYMENTS_MINUS_ADJUSTMENT_AMOUNT_ERR)
                    End If
                End If
                ReversePayment(False)
                AdjustPayment()
            Else
                Exit Sub
            End If

            'Save ClaimInvoice Containing the family: Disbursment and Comment
            State.ClaimInvoiceBO.Save()

            'Navigate to Comment Screen
            State.NavigateToComment = True
            NavController.Navigate(Me, FlowEvents.EVENT_COMMENT_ADDED, New CommentForm.Parameters(State.CommentBO.Id))
        Catch ex As ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is TargetInvocationException) AndAlso
                (TypeOf ex.InnerException Is ThreadAbortException) Then Return
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Private Sub btnNEW_PAYMENT_Click(sender As Object, e As EventArgs) Handles btnNEW_PAYMENT.Click
        ToggleButtons(False, False, False)
        ButtonClicked = NEW_PAYMENT
        Session("ButtonClicked") = ButtonClicked
        'ControlMgr.SetEnableControl(Me, Grid, False)
        SetGridControls(Grid, False)
        NavController.Navigate(Me, "new_payment", New PayClaimForm.Parameters(ClaimFacade.Instance.GetClaim(Of Claim)(State.selectedClaimID), True))
        'Me.callPage(PayClaimForm.URL, New PayClaimForm.Parameters(New Claim(Me.State.selectedClaimID), True))
    End Sub

    Private Sub btnADJUST_PAYMENT_Click(sender As Object, e As EventArgs) Handles btnADJUST_PAYMENT.Click
        ControlMgr.SetVisibleControl(Me, tblPaymentDetails, True)
        ControlMgr.SetVisibleControl(Me, tblHR, True)
        ControlMgr.SetVisibleControl(Me, lblADJUSTMENT_AMOUNT, True)
        ControlMgr.SetVisibleControl(Me, lblAdjAmtAsterisk, True)
        ControlMgr.SetVisibleControl(Me, txtADJUSTMENT_AMOUNT, True)

        Dim oCompaniesDv As DataView, oUser As New User
        oCompaniesDv = oUser.GetUserCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
        oCompaniesDv.RowFilter = "code in ('ABA','VBA','SBA')"
        If oCompaniesDv.Count > 0 Then
            ControlMgr.SetVisibleControl(Me, lblInvDateAsterisk, True)
        Else
            ControlMgr.SetVisibleControl(Me, lblInvDateAsterisk, False)
        End If

        ControlMgr.SetVisibleControl(Me, lblInvoiceDate, True)
        ControlMgr.SetVisibleControl(Me, txtInvoiceDate, True)
        ControlMgr.SetVisibleControl(Me, ImageButtonInvoiceDate, True)

        ToggleButtons(False, False, False)
        ButtonClicked = ADJUST_PAYMENT
        Session("ButtonClicked") = ButtonClicked
        SetGridControls(Grid, False)
        SetFocus(txtInvoiceNumber)
        MenuEnabled = False
        cboPageSize.Enabled = False
    End Sub

    Private Sub btnREVERSE_PAYMENT_Click(sender As Object, e As EventArgs) Handles btnREVERSE_PAYMENT.Click
        ControlMgr.SetVisibleControl(Me, tblPaymentDetails, True)
        ControlMgr.SetVisibleControl(Me, tblHR, True)
        ControlMgr.SetVisibleControl(Me, lblADJUSTMENT_AMOUNT, False)
        ControlMgr.SetVisibleControl(Me, lblAdjAmtAsterisk, False)
        ControlMgr.SetVisibleControl(Me, txtADJUSTMENT_AMOUNT, False)

        Dim oCompaniesDv As DataView, oUser As New User
        oCompaniesDv = oUser.GetUserCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
        oCompaniesDv.RowFilter = "code in ('ABA','VBA','SBA')"
        If oCompaniesDv.Count > 0 Then
            ControlMgr.SetVisibleControl(Me, lblInvDateAsterisk, True)
        Else
            ControlMgr.SetVisibleControl(Me, lblInvDateAsterisk, False)
        End If

        ControlMgr.SetVisibleControl(Me, lblInvoiceDate, True)
        ControlMgr.SetVisibleControl(Me, txtInvoiceDate, True)
        ControlMgr.SetVisibleControl(Me, ImageButtonInvoiceDate, True)

        ToggleButtons(False, False, False)
        ButtonClicked = REVERSE_PAYMENT
        Session("ButtonClicked") = ButtonClicked
        SetGridControls(Grid, False)
        SetFocus(txtInvoiceNumber)
        MenuEnabled = False
        cboPageSize.Enabled = False
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ControlMgr.SetVisibleControl(Me, tblPaymentDetails, False)
        ControlMgr.SetVisibleControl(Me, tblHR, False)
        ControlMgr.SetVisibleControl(Me, ImageButtonInvoiceDate, False)
        ToggleButtons(blnNewPaymentButtonEnabledState, blnReversePaymentButtonEnabledState, blnAdjustPaymentButtonEnabledState)
        ControlMgr.SetEnableControl(Me, Grid, True)
        SetGridControls(Grid, True)
        MenuEnabled = True
        txtADJUSTMENT_AMOUNT.Text = ""
        txtInvoiceDate.Text = ""
        txtInvoiceNumber.Text = ""
    End Sub
#End Region

#Region "Clear"

    Private Sub ClearErrLabels()
        ClearLabelErrSign(lblINVOICE_NUMBER)
        ClearLabelErrSign(lblADJUSTMENT_AMOUNT)
        ClearLabelErrSign(lblInvoiceDate)
        'Me.ClearLabelErrSign(lblColon)
    End Sub

#End Region

#Region "Navigation Control"
    Sub StartNavControl()
        Dim nav As New ElitaPlusNavigation
        NavController = New NavControllerBase(nav.Flow("PAY_CLAIM_ADJUSTMENT"))
    End Sub

#End Region

End Class
