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
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ErrControllerMaster.Clear_Hide()
        Try
            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                populatePageFromBO()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.InvoiceID = CType(CallingParameters, Guid)
                State.MyBO = New AcctPremInvoice(State.InvoiceID)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Helper functions"
    Sub populatePageFromBO()
        With State.MyBO
            txtCompany.Text = (New Company(.CompanyId)).Description
            txtDealer.Text = (New Dealer(.DealerId)).DealerName
            If .BranchId <> Guid.Empty Then txtBranch.Text = (New Branch(.BranchId)).BranchName
            txtInvDate.Text = .CreatedDate.Value.ToString("dd-MMM-yyyy") & " " & .CreatedDate.Value.ToLongTimeString()
            If .PreviousInvoiceDate IsNot Nothing Then txtPreInvDate.Text = .PreviousInvoiceDate.Value.ToString("dd-MMM-yyyy") & " " & .PreviousInvoiceDate.Value.ToLongTimeString()
            txtInvNum.Text = .InvoiceNumber.ToString
            If .CreditNoteNumber IsNot Nothing Then txtCreditNoteNum.Text = .CreditNoteNumber.ToString

            'New cert data
            txtNewTotalCert.Text = .NewTotalCert.Value.ToString("#,0")
            txtNewGWP.Text = .NewGrossAmtRecvd.Value.ToString("#,0.00")
            txtNewWP.Text = .NewPremiumWritten.Value.ToString("#,0.00")
            txtNewCommission.Text = .NewCommission.Value.ToString("#,0.00")
            txtNewTax1.Text = .NewTax1.Value.ToString("#,0.00")
            txtNewTax2.Text = .NewTax2.Value.ToString("#,0.00")
            txtNewTax3.Text = .NewTax3.Value.ToString("#,0.00")
            txtNewTax4.Text = .NewTax4.Value.ToString("#,0.00")
            txtNewTax5.Text = .NewTax5.Value.ToString("#,0.00")
            txtNewTax6.Text = .NewTax6.Value.ToString("#,0.00")
            txtNewTotalAmt.Text = .NewPremiumTotal.Value.ToString("#,0.00")

            'Cancelled cert data
            txtCanclTotalCert.Text = .CancelTotalCert.Value.ToString("#,0")
            txtCanclGWP.Text = .CancelGrossAmtRecvd.Value.ToString("#,0.00")
            txtCanclWP.Text = .CancelPremiumWritten.Value.ToString("#,0.00")
            txtCanclCommission.Text = .CancelCommission.Value.ToString("#,0.00")
            txtCanclTax1.Text = .CancelTax1.Value.ToString("#,0.00")
            txtCanclTax2.Text = .CancelTax2.Value.ToString("#,0.00")
            txtCanclTax3.Text = .CancelTax3.Value.ToString("#,0.00")
            txtCanclTax4.Text = .CancelTax4.Value.ToString("#,0.00")
            txtCanclTax5.Text = .CancelTax5.Value.ToString("#,0.00")
            txtCanclTax6.Text = .CancelTax6.Value.ToString("#,0.00")
            txtCanclTotalAmt.Text = .CancelPremiumTotal.Value.ToString("#,0.00")
        End With
    End Sub
#End Region

#Region "Button click Handler"
    Protected Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            ReturnToCallingPage()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

End Class