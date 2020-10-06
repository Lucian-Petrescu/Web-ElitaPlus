Partial Class MasterClaimForm
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
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 1
    Public Const GRID_COL_STATUS_IDX As Integer = 2
    Public Const GRID_COL_AUTHORIZED_AMOUNT_IDX As Integer = 3
    Public Const GRID_COL_INVOICE_NUMBER_IDX As Integer = 4
    Public Const GRID_COL_PAYEE_IDX As Integer = 5
    Public Const GRID_COL_DATE_CREATED_IDX As Integer = 6
    Public Const GRID_COL_AMOUNT_PAID_IDX As Integer = 7
    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 8
    Public Const GRID_COL_CERT_ID_IDX As Integer = 9
    Public Const GRID_COL_CLAIM_INVOICE_ID_IDX As Integer = 10
    Public Const GRID_TOTAL_COLUMNS As Integer = 11

    Public Const URL As String = "~/Claims/MasterClaimForm.aspx"

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = Claim.ClaimSearchDV.COL_NAME_CLAIM_NUMBER
        Public selectedClaimId As Guid = Guid.Empty
        Public selectedCertId As Guid = Guid.Empty
        Public selectedClaimInvoiceId As Guid = Guid.Empty
        Public selectedInvoiceNumber As String = ""
        Public claimId As Guid = Guid.Empty
        Public certId As Guid = Guid.Empty
        Public claimNumber As String
        Public customerName As String
        Public masterClaimNumber As String
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public authorizationNumber As String
        Public authorizedAmount As String
        Public IsGridVisible As Boolean = True
        Public searchDV As Claim.ClaimSearchDV = Nothing
        Public detailDV As Claim.ClaimSearchDV = Nothing
        Public SearchClicked As Boolean
        Public authorizedAmountCulture As String

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

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public ClaimBO As ClaimBase
        Public HasDataChanged As Boolean

        Public Sub New(LastOp As DetailPageCommand, ClaimBO As ClaimBase, hasDataChanged As Boolean)
            LastOperation = LastOp
            Me.ClaimBO = ClaimBO
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region


#Region "Page_Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        ErrorCtrl.Clear_Hide()
        Try
            If Not IsPostBack Then

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                End If
                PopulateForm()
                PopulateGrid()
                SetGridItemStyleColor(Grid)
            End If

            DisableFields()
            EnableDisableButtons()

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)

    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.masterClaimNumber = CType(CType(CallingPar, ArrayList)(0), String)
                State.claimId = CType(CType(CallingPar, ArrayList)(1), Guid)
                State.certId = CType(CType(CallingPar, ArrayList)(2), Guid)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub SortSvc(oSortDv As DataView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oSortDv.RowFilter = "(LANGUAGE_ID =  '" &
                  GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                  "') AND ((CODE = 'CLNUM') OR (CODE = 'CUSTN'))"
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub DisableFields()
        TextBoxMasterClaimNumber.Enabled = False
        TextBoxCertificate.Enabled = False
        TextBoxDealer.Enabled = False
        TextBoxTotalAmountPaid.Enabled = False
        TextBoxTotalAmountAuthorized.Enabled = False

    End Sub

    Public Sub PopulateForm()
        Try
            If (State.detailDV Is Nothing) Then
                State.detailDV = Claim.getMasterClaimDetailFromArray(State.masterClaimNumber, State.certId)
            End If

            If (State.detailDV.Count > 0) Then
                TextBoxMasterClaimNumber.Text = CType(State.detailDV(0)(0), String)
                TextBoxCertificate.Text = CType(State.detailDV(0)(1), String)
                TextBoxDealer.Text = CType(State.detailDV(0)(2), String)
                TextBoxTotalAmountPaid.Text = MiscUtil.GetAmountFormattedString(CType(State.detailDV(0)(3), Decimal))
                TextBoxTotalAmountAuthorized.Text = MiscUtil.GetAmountFormattedString(CType(State.detailDV(0)(4), Decimal))
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub PopulateGrid()

        Try
            If (State.searchDV Is Nothing) Then
                State.searchDV = Claim.getMasterClaimDetailListFromArray(State.masterClaimNumber, State.certId)
                ValidSearchResultCount(State.searchDV.Count, True)
            End If

            Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex)
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.searchDV
            Grid.AllowSorting = True

            State.searchDV.Sort = State.SortExpression
            Grid.Columns(GRID_COL_CLAIM_NUMBER_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_CLAIM_NUMBER
            Grid.Columns(GRID_COL_STATUS_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_STATUS
            Grid.Columns(GRID_COL_AUTHORIZED_AMOUNT_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_AUTHORIZED_AMOUNT
            Grid.Columns(GRID_COL_INVOICE_NUMBER_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_INVOICE_NUMBER
            Grid.Columns(GRID_COL_PAYEE_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_PAYEE
            Grid.Columns(GRID_COL_DATE_CREATED_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_DATE_CREATED
            Grid.Columns(GRID_COL_AMOUNT_PAID_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_AMOUNT_PAID

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex)
            HighLightSortColumn(Grid, State.SortExpression)
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
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub EnableDisableButtons()
        If Not State.selectedClaimId.Equals(Guid.Empty) Then
            ControlMgr.SetEnableControl(Me, btnClaimDetail, True)
            ControlMgr.SetEnableControl(Me, btnInvoiceDetail, True)
            ControlMgr.SetEnableControl(Me, btnCertificate, True)
        Else
            ControlMgr.SetEnableControl(Me, btnClaimDetail, False)
            ControlMgr.SetEnableControl(Me, btnInvoiceDetail, False)
            ControlMgr.SetEnableControl(Me, btnCertificate, False)
        End If
    End Sub

    'This method will change the Page Index and the Selected Index
    Public Function FindDVSelectedRowIndex(dv As Claim.ClaimSearchDV) As Integer
        Try
            If State.selectedClaimId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(Claim.ClaimSearchDV.COL_CLAIM_ID), Byte())).Equals(State.selectedClaimId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Function

#End Region


#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        Try
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CLAIM_NUMBER))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_STATUS))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_AUTHORIZED_AMOUNT))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_INVOICE_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_INVOICE_NUMBER))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_PAYEE_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_PAYEE))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_DATE_CREATED_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_DATE_CREATED))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_AMOUNT_PAID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_AMOUNT_PAID))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CLAIM_ID))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CERT_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CERT_ID))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_CLAIM_INVOICE_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CLAIM_INVOICE_ID))

            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectRecord" Then
                Grid.SelectedIndex = e.Item.ItemIndex
                State.selectedClaimId = New Guid(e.Item.Cells(GRID_COL_CLAIM_ID_IDX).Text)
                State.selectedCertId = New Guid((e.Item.Cells(GRID_COL_CERT_ID_IDX).Text))
                State.selectedInvoiceNumber = CType(e.Item.Cells(GRID_COL_INVOICE_NUMBER_IDX).Text, String)
                State.selectedClaimInvoiceId = Guid.Empty
                If (e.Item.Cells(GRID_COL_CLAIM_INVOICE_ID_IDX) IsNot Nothing) AndAlso (e.Item.Cells(GRID_COL_CLAIM_INVOICE_ID_IDX).Text <> "") Then
                    State.selectedClaimInvoiceId = New Guid(e.Item.Cells(GRID_COL_CLAIM_INVOICE_ID_IDX).Text)
                End If
                EnableDisableButtons()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            Grid.CurrentPageIndex = State.PageIndex
            State.selectedClaimId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
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
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnClaimDetail_Click(sender As Object, e As System.EventArgs) Handles btnClaimDetail.Click
        Try
            callPage(ClaimForm.URL, State.selectedClaimId)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnInvoiceDetail_Click(sender As Object, e As System.EventArgs) Handles btnInvoiceDetail.Click
        Try
            If (Not State.selectedClaimInvoiceId.Equals(Guid.Empty)) Then
                callPage(PayClaimForm.URL, New PayClaimForm.Parameters(State.selectedClaimInvoiceId))
            Else
                DisplayMessage(Message.MSG_NO_INVOICE_FOUND, "", MSG_BTN_OK, MSG_TYPE_ALERT)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnCertificate_Click(sender As Object, e As System.EventArgs) Handles btnCertificate.Click
        Try
            callPage(Certificates.CertificateForm.URL, State.selectedCertId)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            Back(ElitaPlusPage.DetailPageCommand.Back)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Protected Sub Back(cmd As ElitaPlusPage.DetailPageCommand)
        Dim ClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.claimId)
        Dim retObj As ReturnType = New ReturnType(cmd, ClaimBO, False)
        NavController = Nothing
        ReturnToCallingPage(retObj)
    End Sub

#End Region

End Class



