Public Partial Class InvoiceControlDetailForm
    Inherits ElitaPlusPage

#Region "Constants"
    Public Const URL As String = "InvoiceControlDetailForm.aspx"
    Public Const PAGETITLE As String = "PREMIUM_INVOICE_DETAIL"
    Public Const PAGETAB As String = "INTERFACES"
#End Region

#Region "Page State"
    Class MyState
        Public MyBO As AcctPremInvoice
        Public InvoiceID As Guid
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

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                populatePageFromBO()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                State.InvoiceID = CType(Me.CallingParameters, Guid)
                Me.State.MyBO = New AcctPremInvoice(State.InvoiceID)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Helper functions"
    Sub populatePageFromBO()
        With State.MyBO
            Me.txtCompany.Text = (New Company(.CompanyId)).Description
            Me.txtDealer.Text = (New Dealer(.DealerId)).DealerName
            If .BranchId <> Guid.Empty Then Me.txtBranch.Text = (New Branch(.BranchId)).BranchName
            Me.txtInvDate.Text = .CreatedDate.Value.ToString("dd-MMM-yyyy") & " " & .CreatedDate.Value.ToLongTimeString()
            If Not .PreviousInvoiceDate Is Nothing Then Me.txtPreInvDate.Text = .PreviousInvoiceDate.Value.ToString("dd-MMM-yyyy") & " " & .PreviousInvoiceDate.Value.ToLongTimeString()
            Me.txtInvNum.Text = .InvoiceNumber.ToString
            If Not .CreditNoteNumber Is Nothing Then Me.txtCreditNoteNum.Text = .CreditNoteNumber.ToString

            'New cert data
            Me.txtNewTotalCert.Text = .NewTotalCert.Value.ToString("#,0")
            Me.txtNewGWP.Text = .NewGrossAmtRecvd.Value.ToString("#,0.00")
            Me.txtNewWP.Text = .NewPremiumWritten.Value.ToString("#,0.00")
            Me.txtNewCommission.Text = .NewCommission.Value.ToString("#,0.00")
            Me.txtNewTax1.Text = .NewTax1.Value.ToString("#,0.00")
            Me.txtNewTax2.Text = .NewTax2.Value.ToString("#,0.00")
            Me.txtNewTax3.Text = .NewTax3.Value.ToString("#,0.00")
            Me.txtNewTax4.Text = .NewTax4.Value.ToString("#,0.00")
            Me.txtNewTax5.Text = .NewTax5.Value.ToString("#,0.00")
            Me.txtNewTax6.Text = .NewTax6.Value.ToString("#,0.00")
            Me.txtNewTotalAmt.Text = .NewPremiumTotal.Value.ToString("#,0.00")

            'Cancelled cert data
            Me.txtCanclTotalCert.Text = .CancelTotalCert.Value.ToString("#,0")
            Me.txtCanclGWP.Text = .CancelGrossAmtRecvd.Value.ToString("#,0.00")
            Me.txtCanclWP.Text = .CancelPremiumWritten.Value.ToString("#,0.00")
            Me.txtCanclCommission.Text = .CancelCommission.Value.ToString("#,0.00")
            Me.txtCanclTax1.Text = .CancelTax1.Value.ToString("#,0.00")
            Me.txtCanclTax2.Text = .CancelTax2.Value.ToString("#,0.00")
            Me.txtCanclTax3.Text = .CancelTax3.Value.ToString("#,0.00")
            Me.txtCanclTax4.Text = .CancelTax4.Value.ToString("#,0.00")
            Me.txtCanclTax5.Text = .CancelTax5.Value.ToString("#,0.00")
            Me.txtCanclTax6.Text = .CancelTax6.Value.ToString("#,0.00")
            Me.txtCanclTotalAmt.Text = .CancelPremiumTotal.Value.ToString("#,0.00")
        End With
    End Sub
#End Region

#Region "Button click Handler"
    Protected Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

End Class