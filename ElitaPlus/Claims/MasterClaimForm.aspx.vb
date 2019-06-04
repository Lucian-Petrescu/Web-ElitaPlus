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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal ClaimBO As ClaimBase, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.ClaimBO = ClaimBO
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region


#Region "Page_Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Me.ErrorCtrl.Clear_Hide()
        Try
            If Not Me.IsPostBack Then

                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
                    End If
                End If
                Me.PopulateForm()
                Me.PopulateGrid()
                Me.SetGridItemStyleColor(Me.Grid)
            End If

            Me.DisableFields()
            Me.EnableDisableButtons()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)

    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.masterClaimNumber = CType(CType(CallingPar, ArrayList)(0), String)
                Me.State.claimId = CType(CType(CallingPar, ArrayList)(1), Guid)
                Me.State.certId = CType(CType(CallingPar, ArrayList)(2), Guid)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub SortSvc(ByVal oSortDv As DataView)
        Try
            If ElitaPlusIdentity.Current.ActiveUser.IsServiceCenter Then
                oSortDv.RowFilter = "(LANGUAGE_ID =  '" &
                  GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) &
                  "') AND ((CODE = 'CLNUM') OR (CODE = 'CUSTN'))"
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            If (Me.State.detailDV Is Nothing) Then
                Me.State.detailDV = Claim.getMasterClaimDetailFromArray(Me.State.masterClaimNumber, Me.State.certId)
            End If

            If (Me.State.detailDV.Count > 0) Then
                TextBoxMasterClaimNumber.Text = CType(Me.State.detailDV(0)(0), String)
                TextBoxCertificate.Text = CType(Me.State.detailDV(0)(1), String)
                TextBoxDealer.Text = CType(Me.State.detailDV(0)(2), String)
                TextBoxTotalAmountPaid.Text = MiscUtil.GetAmountFormattedString(CType(Me.State.detailDV(0)(3), Decimal))
                TextBoxTotalAmountAuthorized.Text = MiscUtil.GetAmountFormattedString(CType(Me.State.detailDV(0)(4), Decimal))
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub PopulateGrid()

        Try
            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = Claim.getMasterClaimDetailListFromArray(Me.State.masterClaimNumber, Me.State.certId)
                Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            End If

            Me.Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex)
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.searchDV
            Me.Grid.AllowSorting = True

            Me.State.searchDV.Sort = Me.State.SortExpression
            Me.Grid.Columns(Me.GRID_COL_CLAIM_NUMBER_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_CLAIM_NUMBER
            Me.Grid.Columns(Me.GRID_COL_STATUS_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_STATUS
            Me.Grid.Columns(Me.GRID_COL_AUTHORIZED_AMOUNT_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_AUTHORIZED_AMOUNT
            Me.Grid.Columns(Me.GRID_COL_INVOICE_NUMBER_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_INVOICE_NUMBER
            Me.Grid.Columns(Me.GRID_COL_PAYEE_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_PAYEE
            Me.Grid.Columns(Me.GRID_COL_DATE_CREATED_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_DATE_CREATED
            Me.Grid.Columns(Me.GRID_COL_AMOUNT_PAID_IDX).SortExpression = Claim.ClaimSearchDV.COL_NAME_AMOUNT_PAID

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex)
            HighLightSortColumn(Grid, Me.State.SortExpression)
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
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub EnableDisableButtons()
        If Not Me.State.selectedClaimId.Equals(Guid.Empty) Then
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
    Public Function FindDVSelectedRowIndex(ByVal dv As Claim.ClaimSearchDV) As Integer
        Try
            If Me.State.selectedClaimId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(Claim.ClaimSearchDV.COL_CLAIM_ID), Byte())).Equals(Me.State.selectedClaimId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Function

#End Region


#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        Try
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CLAIM_NUMBER))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_STATUS_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_STATUS))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_AUTHORIZED_AMOUNT_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_AUTHORIZED_AMOUNT))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_INVOICE_NUMBER_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_INVOICE_NUMBER))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_PAYEE_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_PAYEE))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_DATE_CREATED_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_DATE_CREATED))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_AMOUNT_PAID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_AMOUNT_PAID))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CLAIM_ID))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CERT_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CERT_ID))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_CLAIM_INVOICE_ID_IDX), dvRow(Claim.ClaimSearchDV.COL_NAME_CLAIM_INVOICE_ID))

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectRecord" Then
                Grid.SelectedIndex = e.Item.ItemIndex
                Me.State.selectedClaimId = New Guid(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
                Me.State.selectedCertId = New Guid((e.Item.Cells(Me.GRID_COL_CERT_ID_IDX).Text))
                Me.State.selectedInvoiceNumber = CType(e.Item.Cells(Me.GRID_COL_INVOICE_NUMBER_IDX).Text, String)
                Me.State.selectedClaimInvoiceId = Guid.Empty
                If (Not e.Item.Cells(Me.GRID_COL_CLAIM_INVOICE_ID_IDX) Is Nothing) AndAlso (e.Item.Cells(Me.GRID_COL_CLAIM_INVOICE_ID_IDX).Text <> "") Then
                    Me.State.selectedClaimInvoiceId = New Guid(e.Item.Cells(Me.GRID_COL_CLAIM_INVOICE_ID_IDX).Text)
                End If
                EnableDisableButtons()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Grid.CurrentPageIndex = Me.State.PageIndex
            Me.State.selectedClaimId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If

            Me.State.PageIndex = 0

            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Button Clicks"

    Private Sub btnClaimDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClaimDetail.Click
        Try
            Me.callPage(ClaimForm.URL, Me.State.selectedClaimId)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnInvoiceDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInvoiceDetail.Click
        Try
            If (Not Me.State.selectedClaimInvoiceId.Equals(Guid.Empty)) Then
                Me.callPage(PayClaimForm.URL, New PayClaimForm.Parameters(Me.State.selectedClaimInvoiceId))
            Else
                Me.DisplayMessage(Message.MSG_NO_INVOICE_FOUND, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnCertificate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCertificate.Click
        Try
            Me.callPage(Certificates.CertificateForm.URL, Me.State.selectedCertId)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.Back(ElitaPlusPage.DetailPageCommand.Back)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Protected Sub Back(ByVal cmd As ElitaPlusPage.DetailPageCommand)
        Dim ClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.claimId)
        Dim retObj As ReturnType = New ReturnType(cmd, ClaimBO, False)
        Me.NavController = Nothing
        Me.ReturnToCallingPage(retObj)
    End Sub

#End Region

End Class



