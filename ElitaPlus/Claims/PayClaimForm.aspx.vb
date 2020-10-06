Imports System.Globalization
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.External.Interfaces
Imports Assurant.ElitaPlus.External.Interfaces.Darty
Imports Assurant.ElitaPlus.Security

Partial Class PayClaimForm
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents LabelPayeeSelector As System.Web.UI.WebControls.Label
    Protected WithEvents LabelConsumerPays As System.Web.UI.WebControls.Label
    Protected WithEvents PayeeAddress As UserControlAddress_New
    Protected WithEvents PayeeBankInfo As UserControlBankInfo_New
    Protected WithEvents CustomValidator1 As System.Web.UI.WebControls.CustomValidator
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox


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
    Public Const URL As String = "~\Claims\PayClaimForm.aspx"
    Private Const FORM_FROM_BO As String = "FORM_FROM_BO"
    Private Const BO_FROM_FORM As String = "BO_FROM_FORM"
    Private Const NOTHING_SELECTED As Int16 = -1
    Private Const TAX_TYPE_INVOICE As String = "4"
    Private Const TAX_TYPE_REPAIR As String = "7"
    Public Const PAGETITLE As String = "PAY INVOICE"
    Public Const PAGETAB As String = "CLAIMS"

    'REQ-6171 - Darty Gift Card web service
    Public Const ApplicationSource_Darty = "ASSURANT"
    Public Const Payee_Customer = "CUSTOMER"
    Public Const Bank_Transfer = "BANK TRANSFER"
    Public Const EMPTY_GUID As String = "00000000-0000-0000-0000-000000000000"
#End Region

#Region "Private members"
    Private _Company As Company
    Private _Dealer As Dealer
    Private _Certificate As Certificate
#End Region

#Region "Properties"
    'Public ReadOnly Property UserBankInfoCtr() As UserControlBankInfo
    '    Get
    '        If moBankInfoController Is Nothing Then
    '            moBankInfoController = CType(FindControl("moBankInfoController"), UserControlBankInfo)
    '        End If
    '        Return moBankInfoController
    '    End Get
    'End Property

    Public ReadOnly Property oCompany() As Company
        Get
            If _Company Is Nothing Then
                _Company = New Company(State.ClaimInvoiceBO.CompanyId)
            End If
            Return _Company
        End Get
    End Property

    Public ReadOnly Property AuthDetailRequired() As String
        Get
            Return LookupListNew.GetCodeFromId(LookupListNew.LK_AUTH_DTL, oCompany.AuthDetailRqrdId)
        End Get
    End Property
    Public ReadOnly Property oCertificate() As Certificate
        Get
            If _Certificate Is Nothing Then
                _Certificate = New Certificate(State.ClaimBO.CertificateId)
            End If
            Return _Certificate
        End Get
    End Property

    Public ReadOnly Property oDealer() As Dealer
        Get
            If _Dealer Is Nothing Then
                _Dealer = New Dealer(oCertificate.DealerId)
            End If
            Return _Dealer
        End Get
    End Property

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
        Public ClaimID As Guid
        Public PayClaimID As Guid
        Public ViewOnly As Boolean = False
        Public isForClaimPayAdjust As Boolean
        Public hasDataChanged As Boolean

        Public Sub New(claimBO As Claim, Optional ByVal isForClaimPayAdjustment As Boolean = False)
            Me.ClaimBO = claimBO
            PayClaimID = Guid.Empty
            ViewOnly = False
            ClaimID = claimBO.Id
            isForClaimPayAdjust = isForClaimPayAdjustment
        End Sub

        Public Sub New(claimInvoiceID As Guid)
            ClaimBO = Nothing
            PayClaimID = claimInvoiceID
            ViewOnly = True
            ClaimID = Guid.Empty
        End Sub

        Public Sub New(claimID As Guid, viewOnly As Boolean)
            ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(claimID)
            PayClaimID = Guid.Empty
            Me.ViewOnly = viewOnly
            Me.ClaimID = claimID
        End Sub

        'DEF-17426
        Public Sub New(claimId As Guid, hasDataChanged As Boolean, viewOnly As Boolean)
            Me.ClaimID = claimId
            Me.hasDataChanged = hasDataChanged
            Me.ViewOnly = viewOnly
        End Sub
    End Class
#End Region

#Region "Page State"
    Class BaseState
        Public NavCtrl As INavigationController
    End Class

    Class MyState
        Public ClaimInvoiceBO As ClaimInvoice
        Public DisbursementBO As Disbursement
        Public isForClaimPayAdjust As Boolean = False
        Public PayeeAddress As Address
        Public PayeeBankInfo As BankInfo
        Public ClaimBO As Claim
        Public ViewOnly As Boolean = False
        Public ScreenSnapShotBO As ClaimInvoice
        Public PartsInfoViewed As Boolean = False
        Public ChangesMade As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public BankInfoBO As BankInfo
        Public selectedPaymentMethodID As Guid
        Public ClaimAuthDetailBO As ClaimAuthDetail
        Public AuthDetailEnabled As Boolean = False
        Public AuthDetailExist As Boolean = False
        Public isClaimSystemMaintAllowed As Boolean = True
        Public ComingFromChildForm As Boolean = False
        Public PartsInfoBO As PartsInfo
        Public validateBankInfoCountry As String = String.Empty
        Public ClaimTaxRatesData As ClaimInvoiceDAL.ClaimTaxRatesData
        Public OverrideShowGrandTotal As Boolean = False


        Private _ManualTaxes As Collections.Generic.List(Of PayClaimManualTaxForm.ManualTaxDetail)

        Public Sub LoadManualTaxesAmountChanges(lstMT As Collections.Generic.List(Of PayClaimManualTaxForm.ManualTaxDetail))
            For Each objNew As PayClaimManualTaxForm.ManualTaxDetail In lstMT
                For Each objOld As PayClaimManualTaxForm.ManualTaxDetail In _ManualTaxes
                    If objOld.Position = objNew.Position Then
                        objOld.Amount = objNew.Amount
                    End If
                Next
            Next
        End Sub

        Public ReadOnly Property ManualTaxes() As Collections.Generic.List(Of PayClaimManualTaxForm.ManualTaxDetail)
            Get
                If _ManualTaxes Is Nothing Then
                    Dim guidCountryID As Guid, dvManulTax As DataView
                    guidCountryID = ClaimTax.GetClaimTaxCountry(ClaimBO.Id)
                    'REQ 1150
                    Dim oClaim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(ClaimBO.Id)
                    dvManulTax = CountryTax.getManualTaxesByTaxType(guidCountryID, ClaimTax.TAX_TYPE_CLAIM, DateTime.Now, oClaim.Certificate.DealerId)


                    _ManualTaxes = New Collections.Generic.List(Of PayClaimManualTaxForm.ManualTaxDetail)
                    If (dvManulTax IsNot Nothing) AndAlso (dvManulTax.Count > 0) Then
                        Dim strTemp As String, strDesc As String
                        strTemp = CType(dvManulTax(0)("ManualClaimTax"), String)
                        If strTemp = "Y" Then
                            'Manual taxes exist, build the list
                            'Tax 1
                            strTemp = CType(dvManulTax(0)("Tax1Manual"), String)
                            If strTemp = "Y" Then
                                If dvManulTax(0)("TAX1_DESCRIPTION") Is DBNull.Value Then
                                    strDesc = String.Empty
                                Else
                                    strDesc = CType(dvManulTax(0)("TAX1_DESCRIPTION"), String)
                                End If
                                _ManualTaxes.Add(New PayClaimManualTaxForm.ManualTaxDetail(strDesc, 1, 0))
                            End If
                            'Tax 2
                            strTemp = CType(dvManulTax(0)("Tax2Manual"), String)
                            If strTemp = "Y" Then
                                If dvManulTax(0)("TAX2_DESCRIPTION") Is DBNull.Value Then
                                    strDesc = String.Empty
                                Else
                                    strDesc = CType(dvManulTax(0)("TAX2_DESCRIPTION"), String)
                                End If
                                _ManualTaxes.Add(New PayClaimManualTaxForm.ManualTaxDetail(strDesc, 2, 0))
                            End If
                            'Tax 3
                            strTemp = CType(dvManulTax(0)("Tax3Manual"), String)
                            If strTemp = "Y" Then
                                If dvManulTax(0)("TAX3_DESCRIPTION") Is DBNull.Value Then
                                    strDesc = String.Empty
                                Else
                                    strDesc = CType(dvManulTax(0)("TAX3_DESCRIPTION"), String)
                                End If
                                _ManualTaxes.Add(New PayClaimManualTaxForm.ManualTaxDetail(strDesc, 3, 0))
                            End If
                            'Tax 4
                            strTemp = CType(dvManulTax(0)("Tax4Manual"), String)
                            If strTemp = "Y" Then
                                If dvManulTax(0)("TAX4_DESCRIPTION") Is DBNull.Value Then
                                    strDesc = String.Empty
                                Else
                                    strDesc = CType(dvManulTax(0)("TAX4_DESCRIPTION"), String)
                                End If
                                _ManualTaxes.Add(New PayClaimManualTaxForm.ManualTaxDetail(strDesc, 4, 0))
                            End If
                            'Tax 1
                            strTemp = CType(dvManulTax(0)("Tax5Manual"), String)
                            If strTemp = "Y" Then
                                If dvManulTax(0)("TAX5_DESCRIPTION") Is DBNull.Value Then
                                    strDesc = String.Empty
                                Else
                                    strDesc = CType(dvManulTax(0)("TAX5_DESCRIPTION"), String)
                                End If
                                _ManualTaxes.Add(New PayClaimManualTaxForm.ManualTaxDetail(strDesc, 5, 0))
                            End If
                            'Tax 6
                            strTemp = CType(dvManulTax(0)("Tax6Manual"), String)
                            If strTemp = "Y" Then
                                If dvManulTax(0)("TAX6_DESCRIPTION") Is DBNull.Value Then
                                    strDesc = String.Empty
                                Else
                                    strDesc = CType(dvManulTax(0)("TAX6_DESCRIPTION"), String)
                                End If
                                _ManualTaxes.Add(New PayClaimManualTaxForm.ManualTaxDetail(strDesc, 6, 0))
                            End If
                        End If
                    End If
                End If
                Return _ManualTaxes
            End Get
        End Property

        Public ReadOnly Property HasManualTaxes() As Boolean
            Get
                If (ManualTaxes IsNot Nothing) AndAlso (ManualTaxes.Count > 0) Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property

        Public ReadOnly Property TotalManualTaxes As Decimal
            Get
                Dim dblTtlTax As Decimal
                dblTtlTax = 0
                If (ManualTaxes IsNot Nothing) AndAlso (ManualTaxes.Count > 0) Then
                    For Each obj As PayClaimManualTaxForm.ManualTaxDetail In ManualTaxes
                        dblTtlTax = dblTtlTax + obj.Amount
                    Next
                End If
                Return dblTtlTax
            End Get
        End Property

    End Class

    Public Sub New()
        MyBase.New(New BaseState)
    End Sub
    Protected Shadows ReadOnly Property State() As MyState
        Get
            If NavController.State Is Nothing Then
                NavController.State = New MyState
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                StartNavControl()
                Dim params As Parameters = CType(CallingParameters, Parameters)
                If params.PayClaimID.Equals(Guid.Empty) Then
                    State.ClaimBO = params.ClaimBO
                    'coming from payclaim , pass claimbo to prepopulate data
                    State.ClaimInvoiceBO = New ClaimInvoice
                    State.DisbursementBO = State.ClaimInvoiceBO.AddNewDisbursement()
                    State.ClaimInvoiceBO.PrepopulateClaimInvoice(State.ClaimBO, True)
                    State.ClaimInvoiceBO.PrepopulateDisbursment()

                    State.isForClaimPayAdjust = params.isForClaimPayAdjust
                    State.ClaimInvoiceBO.IsNewPaymentFromPaymentAdjustment = State.isForClaimPayAdjust
                    'default values
                    State.ClaimInvoiceBO.BatchNumber = "1"
                    State.ClaimInvoiceBO.LaborTax = New DecimalType(0D)
                    State.ClaimInvoiceBO.DispositionAmount = New DecimalType(0D)
                    State.ClaimInvoiceBO.DiagnosticsAmount = New DecimalType(0D)

                    'Initialize the part tax value with the amount for the parts info  
                    State.ClaimInvoiceBO.PartAmount = PartsInfo.getTotalCost(State.ClaimBO.Id).Value
                    State.ClaimInvoiceBO.PartTax = New DecimalType(0D)
                    State.ClaimInvoiceBO.RecordCount = New LongType(1)
                    State.ClaimInvoiceBO.Source = Nothing
                    State.ClaimInvoiceBO.BeginEdit()
                Else
                    'coming from view invoice
                    State.ClaimBO = Nothing
                    State.ClaimInvoiceBO = New ClaimInvoice(params.PayClaimID)
                    State.DisbursementBO = New Disbursement(State.ClaimInvoiceBO.DisbursementId)
                    State.ClaimInvoiceBO.PaymentMethodID = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYMENTMETHOD, State.DisbursementBO.PaymentMethod)
                End If
                State.ViewOnly = params.ViewOnly
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
#End Region

#Region "Navigation Control"
    Sub StartNavControl()
        'If Me.NavController Is Nothing Then
        Dim nav As New ElitaPlusNavigation
        NavController = New NavControllerBase(nav.Flow("PAY_CLAIM_DETAIL"))
        'End If
    End Sub
#End Region

#Region "Page_Events"

    Private Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If State.HasManualTaxes Then
            EnableDisabManualTaxFields(True)
            txtManualTax.Text = State.TotalManualTaxes.ToString
        Else
            EnableDisabManualTaxFields(False, State.OverrideShowGrandTotal)
        End If
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            'if coming from ClaimAuthDetail form reload claim
            If CalledUrl = ClaimAuthDetailForm.URL Then
                Dim retObj As ClaimAuthDetailForm.ReturnType = CType(ReturnPar, ClaimAuthDetailForm.ReturnType)
                If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                    'reload
                    State.ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(State.ClaimBO.Id)
                    State.ClaimInvoiceBO.RefreshCurrentClaim()
                End If
            ElseIf CalledUrl = PayClaimManualTaxForm.URL Then
                Dim retTaxDetails As PayClaimManualTaxForm.Parameters
                retTaxDetails = CType(ReturnPar, PayClaimManualTaxForm.Parameters)
                State.LoadManualTaxesAmountChanges(retTaxDetails.ManualTaxList)
                'Me.State.ClaimBO.AuthorizedAmount = New DecimalType(Me.State.ClaimBO.AuthorizedAmount.Value + State.TotalManualTaxes)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load, Me.Load
        MasterPage.MessageController.Clear_Hide()

        Try
            'DEF-17426
            If NavController.Context = "PAY_CLAIM_DETAIL" Then
                Dim retObj As Parameters = CType(NavController.ParametersPassed, Parameters)
                If retObj IsNot Nothing AndAlso retObj.hasDataChanged Then
                    State.ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(retObj.ClaimID)
                    State.ClaimInvoiceBO.RefreshCurrentClaim()
                End If

                If (NavController IsNot Nothing) AndAlso (NavController.PrevNavState IsNot Nothing) AndAlso (NavController.PrevNavState.Name = "AUTH_DETAIL" Or NavController.PrevNavState.Name = "PARTS_INFO") Then
                    State.ComingFromChildForm = True
                End If

                If (CalledUrl IsNot Nothing) AndAlso (CalledUrl = PayClaimManualTaxForm.URL Or CalledUrl = Claims.ReplacementForm.URL) Then
                    State.ComingFromChildForm = True
                End If

            End If

            txtLabor.Attributes.Add("onchange", "doAmtCalc(this);")
            txtParts.Attributes.Add("onchange", "doAmtCalc(this);")
            txtTripAmt.Attributes.Add("onchange", "doAmtCalc(this);")
            txtShipping.Attributes.Add("onchange", "doAmtCalc(this);")
            txtServiceCharge.Attributes.Add("onchange", "doAmtCalc(this);")
            txtOtherAmt.Attributes.Add("onchange", "doAmtCalc(this);")
            txtDiagnostics.Attributes.Add("onchange", "doAmtCalc(this);")
            txtDisposition.Attributes.Add("onchange", "doAmtCalc(this);")

            If State.ClaimBO IsNot Nothing AndAlso State.ClaimBO.Dealer.AttributeValues.Contains(Codes.DLR_ATTRBT_PAY_TO_CUST_AMT) Then
                If Not (State.ClaimBO.Dealer.AttributeValues.Value(Codes.DLR_ATTRBT_PAY_TO_CUST_AMT) = Codes.YESNO_Y AndAlso State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) Then
                    txtTotal.Attributes.Add("onchange", "getTotalAmount(this);")
                End If
            Else
                txtTotal.Attributes.Add("onchange", "getTotalAmount(this);")
            End If

            txtSalvageAmt.Attributes.Add("onchange", "doAmtCalc(this);")
            txtOtherDesc.Attributes.Add("style", "text-align: right")
            'DEF-1631
            txtDeductibleAmount.Attributes.Add("onchange", "doAmtCalc(this);")
            'End of DEF-1631

            txtPerceptionIva.Attributes.Add("onchange", "doAmtCalc(this);")
            txtPerceptionIIBB.Attributes.Add("onchange", "doAmtCalc(this);")
            txtPerceptionIva.Attributes.Add("onfocus", "select( );")
            txtPerceptionIIBB.Attributes.Add("onfocus", "select( );")

            txtLabor.Attributes.Add("onfocus", "select( );")
            txtParts.Attributes.Add("onfocus", "select( );")
            txtTripAmt.Attributes.Add("onfocus", "select( );")
            txtServiceCharge.Attributes.Add("onfocus", "select( );")
            txtOtherAmt.Attributes.Add("onfocus", "select( );")
            txtDiagnostics.Attributes.Add("onfocus", "select( );")
            txtDisposition.Attributes.Add("onfocus", "select( );")
            txtTotal.Attributes.Add("onfocus", "select( );")
            txtSalvageAmt.Attributes.Add("onfocus", "select( );")
            cboRepairCode.Attributes.Add("onchange", "rcDDOnChange(this);")

            'Dim currentCulture As String = System.Threading.Thread.CurrentThread.CurrentCulture.Name
            '----------------------------
            SetDecimalSeparatorSymbol()

            BindBoPropertiesToLabels()

            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)

                CheckifComingFromNewClaimBackEndClaim()

                CheckifComingFromClaimAdjustment()

                ControlMgr.SetVisibleControl(Me, hrSeprator, False)

                MenuEnabled = False

                AddCalendar(ImageButtonRepairDate, txtRepairDate)
                AddCalendar(ImageButtonLoanerReturnedDate, txtLoanerReturnedDate)
                AddCalendar(ImageButtonInvoiceDate, txtInvoiceDate)

                'Pickup date: do not display if replacement and/or interface claim
                If State.ClaimBO IsNot Nothing AndAlso State.ClaimBO.CanDisplayVisitAndPickUpDates And Not State.isForClaimPayAdjust Then
                    If Not State.ClaimBO.LoanerTaken Then AddCalendar(ImageButtonPickupDate, TextboxPickupDate)
                End If

                If State.ClaimInvoiceBO Is Nothing Then
                    State.ClaimInvoiceBO = New ClaimInvoice
                End If
                'Trace(Me, "ClaimInvoice Id=" & GuidControl.GuidToHexString(Me.State.ClaimInvoiceBO.Id))
                'Claim Auth Detail logic
                If State.ClaimBO IsNot Nothing Then
                    If oCompany.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) Then
                        State.AuthDetailEnabled = True
                        LoadClaimAuthDetail()
                    End If
                End If
                '
                PopulateDropdowns()
                PopulateFormFromBOs()
                If Not State.selectedPaymentMethodID.Equals(Guid.Empty) Then
                    PopulateControlFromBOProperty(PaymentMethodDrop, State.selectedPaymentMethodID)
                    PaymentMethodChanged(State.selectedPaymentMethodID, GetSelectedItem(cboPayeeSelector), False)
                End If
            Else
                If Not IsClientScriptBlockRegistered("load") AndAlso (InvoiceMethod.Value = "2" And State.AuthDetailEnabled) Then
                    ScriptManager.RegisterStartupScript(Me, [GetType](), "load", "doAmtCalc(document.getElementById('ctl00_SummaryPlaceHolder_txtLabor'));", True)
                End If
                loadTaxAmountsFromHiddenFields()
            End If

            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "load", "doAmtCalc(document.getElementById('txtLabor'));", True)
            CheckIfComingFromSaveConfirm()
            EnableDisableFields()
            If Not IsPostBack Then
                AddLabelDecorations(State.ClaimInvoiceBO)
                AddLabelColons()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub AddLabelColons()
        If Not LabelPayee.Text.EndsWith(":") Then
            LabelPayee.Text = LabelPayee.Text & ":"
        End If
        If Not LabelInvNumber.Text.EndsWith(":") Then
            LabelInvNumber.Text = LabelInvNumber.Text & ":"
        End If
        If Not LabelCustomerName.Text.EndsWith(":") Then
            LabelCustomerName.Text = LabelCustomerName.Text & ":"
        End If
        If Not LabelBillingSvcCenter.Text.EndsWith(":") Then
            LabelBillingSvcCenter.Text = LabelBillingSvcCenter.Text & ":"
        End If
        If Not LabelCauseOfLoss.Text.EndsWith(":") Then
            LabelCauseOfLoss.Text = LabelCauseOfLoss.Text & ":"
        End If
        If Not LabelLabor.Text.EndsWith(":") Then
            LabelLabor.Text = LabelLabor.Text & ":"
        End If
        If Not LabelParts.Text.EndsWith(":") Then
            LabelParts.Text = LabelParts.Text & ":"
        End If
        If Not LabelSvcCharge.Text.EndsWith(":") Then
            LabelSvcCharge.Text = LabelSvcCharge.Text & ":"
        End If
        If Not LabelTripAmount.Text.EndsWith(":") Then
            LabelTripAmount.Text = LabelTripAmount.Text & ":"
        End If
        If Not LabelShipping.Text.EndsWith(":") Then
            LabelShipping.Text = LabelShipping.Text & ":"
        End If
        If Not LabelTotal.Text.EndsWith(":") Then
            LabelTotal.Text = LabelTotal.Text & ":"
        End If
        If Not LabelRepairCode.Text.EndsWith(":") Then
            LabelRepairCode.Text = LabelRepairCode.Text & ":"
        End If
        If Not LabelPickUpDate.Text.EndsWith(":") Then
            LabelPickUpDate.Text = LabelPickUpDate.Text & ":"
        End If
    End Sub

    Protected Sub DisableButtonsForClaimSystem()
        If Not State.ClaimBO.CertificateId.Equals(Guid.Empty) Then
            Dim oCert As New Certificate(State.ClaimBO.CertificateId)
            Dim oDealer As New Dealer(oCert.DealerId)
            Dim oClmSystem As New ClaimSystem(oDealer.ClaimSystemId)
            Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            If oClmSystem.PayClaimId.Equals(noId) Then
                State.isClaimSystemMaintAllowed = False
                If btnSave_WRITE.Visible And btnSave_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
                End If
                If btnUndo_Write.Visible And btnUndo_Write.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
                End If
                If btnPartsInfo_WRITE.Visible And btnPartsInfo_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnPartsInfo_WRITE, False)
                End If
                If btnAuthDetail_WRITE.Visible And btnAuthDetail_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnAuthDetail_WRITE, False)
                End If
                If btnReplacement_WRITE.Visible And btnReplacement_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, btnReplacement_WRITE, False)
                End If
            End If
        End If
    End Sub



    Public Sub loadTaxAmountsFromHiddenFields()
        PopulateControlFromBOProperty(txtPartsTax, hdPartsTaxAmt.Value)
        PopulateControlFromBOProperty(txtLaborTax, hdLaborTaxAmt.Value)
        PopulateControlFromBOProperty(txtServiceChargeTax, hdServiceChargeTaxAmt.Value)
        PopulateControlFromBOProperty(txtTripAmtTax, hdTripTaxAmt.Value)
        PopulateControlFromBOProperty(txtShippingTax, hdShippingTaxAmt.Value)
        PopulateControlFromBOProperty(txtDispositionTax, hdDispositionTaxAmt.Value)
        PopulateControlFromBOProperty(txtDiagnosticsTax, hdDiagnosticsTaxAmt.Value)
        PopulateControlFromBOProperty(txtOtherTax, hdOtherTaxAmt.Value)

        PopulateControlFromBOProperty(txtTotal, hdTotal.Value)
        PopulateControlFromBOProperty(txtSubTotal, hdSubTotalAmt.Value)
        PopulateControlFromBOProperty(txtTotalTaxAmount, hdTotalTaxAmt.Value)
        PopulateControlFromBOProperty(txtTotalWithholdingAmount, hdTotalWithholdings.Value)
        PopulateControlFromBOProperty(txtGrandTotal, hdGrandTotalAmt.Value)

    End Sub

    Private Sub EnableDisablePerceptionFields(blnEnabled As Boolean, Optional ByVal blnOverrideShowGrandTotal As Boolean = False)
        ControlMgr.SetVisibleControl(Me, trPerception_IIBB, blnEnabled)
        ControlMgr.SetVisibleControl(Me, trPerception_IVA, blnEnabled)

        If blnEnabled OrElse trManualTax.Visible Then
            ControlMgr.SetVisibleControl(Me, trGrandTotal, True)
        Else
            ControlMgr.SetVisibleControl(Me, trGrandTotal, False)
        End If

        If blnOverrideShowGrandTotal Then ControlMgr.SetVisibleControl(Me, trGrandTotal, True)
    End Sub

    Private Sub EnableDisabManualTaxFields(blnEnabled As Boolean, Optional ByVal blnOverrideShowGrandTotal As Boolean = False)
        ControlMgr.SetVisibleControl(Me, trManualTax, blnEnabled)
        ControlMgr.SetVisibleControl(Me, btnTaxes_WRITE, blnEnabled)

        If blnEnabled OrElse trPerception_IIBB.Visible Then
            ControlMgr.SetVisibleControl(Me, trGrandTotal, True)
        Else
            ControlMgr.SetVisibleControl(Me, trGrandTotal, False)
        End If

        If blnOverrideShowGrandTotal Then ControlMgr.SetVisibleControl(Me, trGrandTotal, True)
    End Sub

    Protected Sub EnableDisableFields()
        ' 1 - detail entry
        ' 2 - total entry
        Try
            State.OverrideShowGrandTotal = False
            Dim oInvoiceMethodId As Guid = oCompany.InvoiceMethodId
            InvoiceMethod.Value = LookupListNew.GetCodeFromId(LookupListNew.LK_INVOICE_METHOD, oInvoiceMethodId)

            If oInvoiceMethodId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_INVOICE_METHOD, ClaimInvoice.INVOICE_METHOD_DETAIL)) And Not State.AuthDetailEnabled Then
                ChangeEnabledProperty(txtLabor, (Not State.ViewOnly) And True)
                ChangeEnabledProperty(txtParts, (Not State.ViewOnly) And True)
                ChangeEnabledProperty(txtServiceCharge, (Not State.ViewOnly) And True)
                ChangeEnabledProperty(txtTripAmt, (Not State.ViewOnly) And True)
                ChangeEnabledProperty(txtOtherAmt, (Not State.ViewOnly) And True)
                ChangeEnabledProperty(txtOtherDesc, (Not State.ViewOnly) And True)
                ChangeEnabledProperty(txtDisposition, (Not State.ViewOnly) And True)
                ChangeEnabledProperty(txtDiagnostics, (Not State.ViewOnly) And True)
                ChangeEnabledProperty(txtSubTotal, False)
                ChangeEnabledProperty(txtTotalTaxAmount, False)
                ChangeEnabledProperty(txtTotal, False)
                ChangeEnabledProperty(txtGrandTotal, False)
                'Me.InvoiceMethod.Value = ClaimInvoice.INVOICE_METHOD_DETAIL
                ControlMgr.SetVisibleControl(Me, btnPartsInfo_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, False)
                ControlMgr.SetVisibleControl(Me, trGrandTotal, True)
                State.OverrideShowGrandTotal = True
            Else
                ChangeEnabledProperty(txtLabor, False)
                ChangeEnabledProperty(txtParts, False)
                ChangeEnabledProperty(txtServiceCharge, False)
                ChangeEnabledProperty(txtTripAmt, False)
                ChangeEnabledProperty(txtOtherAmt, False)
                ChangeEnabledProperty(txtOtherDesc, False)
                ChangeEnabledProperty(txtDisposition, False)
                ChangeEnabledProperty(txtDiagnostics, False)
                ChangeEnabledProperty(txtIvaTax, False)
                ChangeEnabledProperty(txtTotalTaxAmount, False)
                ChangeEnabledProperty(txtGrandTotal, False)
                ChangeEnabledProperty(txtSubTotal, False)
                If State.AuthDetailEnabled And State.ClaimInvoiceBO.Invoiceable.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
                    ChangeEnabledProperty(txtTotal, False)
                Else
                    ChangeEnabledProperty(txtTotal, (Not State.ViewOnly) And True)
                End If

                ControlMgr.SetVisibleControl(Me, btnPartsInfo_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, False)
                If oCompany.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) OrElse Not State.ClaimInvoiceBO.IsIvaResponsibleFlag Then
                    ControlMgr.SetVisibleControl(Me, trGrandTotal, True)
                    State.OverrideShowGrandTotal = True
                Else
                    ControlMgr.SetVisibleControl(Me, trGrandTotal, False)
                End If

                'Me.InvoiceMethod.Value = ClaimInvoice.INVOICE_METHOD_TOTAL
            End If

            ' as per the partsinfo specs , parts info needs to be visible only for inv method = 1 and repair claims.
            If State.ClaimInvoiceBO.Invoiceable.ClaimActivityId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then
                ControlMgr.SetVisibleControl(Me, btnReplacement_WRITE, (Not State.ViewOnly) And True)
                If IsPostBack Then
                    LabelRepairDate.Text = "*" + TranslationBase.TranslateLabelOrMessage("REPLACEMENT_DATE") + ":"
                Else
                    LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT_DATE") + ":"
                End If

                State.PartsInfoViewed = True
                'ControlMgr.SetVisibleControl(Me, Me.LabelRepairCode, False)
                'ControlMgr.SetVisibleControl(Me, Me.cboRepairCode, False)
                'REQ-786 Display repair code with only delivery fee option for pending replacement claims
                Dim dvRepairCode As DataView = LookupListNew.GetRepairCodeLookupList(oCompany.CompanyGroupId)
                If (dvRepairCode.Count > 0) Then
                    dvRepairCode.RowFilter = " code = '" + Codes.REPAIR_CODE__DELIVERY_FEE_NO_DEDUCTIBLE + "'"
                End If
                If Not IsPostBack Then
                    BindListControlToDataView(cboRepairCode, dvRepairCode)
                End If
                'REQ-786 End

                If (dvRepairCode.Count > 0) Then
                    If (GetGuidFromString(cboRepairCode.SelectedValue) = New Guid(CType(dvRepairCode(0).Item("id"), Byte()))) Then
                        btnReplacement_WRITE.Style.Add("display", "none")
                    End If
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnReplacement_WRITE, False)
                If IsPostBack Then
                    LabelRepairDate.Text = "*" + TranslationBase.TranslateLabelOrMessage("REPAIR_DATE") + ":"
                Else
                    LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("REPAIR_DATE") + ":"
                End If
                ControlMgr.SetVisibleControl(Me, LabelRepairCode, True And Not State.isForClaimPayAdjust)
                ControlMgr.SetVisibleControl(Me, cboRepairCode, True And Not State.isForClaimPayAdjust)
                If oInvoiceMethodId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_INVOICE_METHOD, ClaimInvoice.INVOICE_METHOD_DETAIL)) Then
                    'its always viewed in this case...
                    If State.AuthDetailEnabled And State.ClaimInvoiceBO.Invoiceable.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
                        ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, (Not State.ViewOnly))
                    End If

                    Select Case State.ClaimInvoiceBO.Invoiceable.MethodOfRepairCode
                        Case Codes.METHOD_OF_REPAIR__CARRY_IN, Codes.METHOD_OF_REPAIR__AT_HOME, Codes.METHOD_OF_REPAIR__SEND_IN, Codes.METHOD_OF_REPAIR__PICK_UP
                            ControlMgr.SetVisibleControl(Me, btnPartsInfo_WRITE, (Not State.ViewOnly))
                    End Select
                Else
                    ControlMgr.SetVisibleControl(Me, btnPartsInfo_WRITE, False)
                    ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, False)
                    State.PartsInfoViewed = True
                End If

            End If

            'ControlMgr.SetVisibleControl(Me, PerceptionPanel, False)
            EnableDisablePerceptionFields(False, State.OverrideShowGrandTotal)
            ControlMgr.SetVisibleControl(Me, cboRegionDropID, False)
            ControlMgr.SetVisibleControl(Me, lblRigion, False)

            If (oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.YESNO_N)) Or
                (oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y)) Then
                trDeductibleAmount.Visible = False
                trDeductibleTaxAmount.Visible = False
            End If

            If State.AuthDetailEnabled AndAlso Not State.AuthDetailExist Then
                ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, Not State.ViewOnly)
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)

            Else
                ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, (Not State.ViewOnly) And State.AuthDetailEnabled)
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, (Not State.ViewOnly))
                If State.ClaimInvoiceBO.isTaxTypeInvoice() Then
                    Dim oClaim As Claim
                    oClaim = ClaimFacade.Instance.GetClaim(Of Claim)(State.ClaimInvoiceBO.ClaimId)
                    If Not oClaim.ServiceCenterId.Equals(Guid.Empty) Then
                        Dim svcCenter As ServiceCenter = New ServiceCenter(oClaim.ServiceCenterId)
                        If svcCenter.IvaResponsibleFlag Then
                            'ControlMgr.SetVisibleControl(Me, PerceptionPanel, True)
                            EnableDisablePerceptionFields(True, State.OverrideShowGrandTotal)
                            ChangeEnabledProperty(txtPerceptionIva, True)
                            ChangeEnabledProperty(txtPerceptionIIBB, True)
                            ControlMgr.SetVisibleControl(Me, cboRegionDropID, True)
                            ControlMgr.SetVisibleControl(Me, lblRigion, True)
                            trIVA_Amount.Visible = True
                            trTotalTax_Amount.Visible = False
                        Else
                            'ControlMgr.SetVisibleControl(Me, PerceptionPanel, False)
                            EnableDisablePerceptionFields(False, State.OverrideShowGrandTotal)
                            ControlMgr.SetVisibleControl(Me, cboRegionDropID, False)
                            ControlMgr.SetVisibleControl(Me, lblRigion, False)
                            trIVA_Amount.Visible = False
                            trTotalTax_Amount.Visible = True
                        End If
                    End If
                End If
            End If


            ControlMgr.SetVisibleForControlFamily(Me, ImageButtonLoanerReturnedDate, (Not State.ViewOnly) And (Not State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty)), True)
            ControlMgr.SetVisibleForControlFamily(Me, ImageButtonRepairDate, (Not State.ViewOnly) And True, True)

            ControlMgr.SetVisibleControl(Me, LabelLoanerReturnedDate, (Not State.ViewOnly) And (Not State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty)))
            ControlMgr.SetVisibleControl(Me, txtLoanerReturnedDate, (Not State.ViewOnly) And (Not State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty)))

            ControlMgr.SetVisibleControl(Me, txtAcctStatusDate, False)
            ControlMgr.SetVisibleControl(Me, txtAcctStatusCode, False)
            ControlMgr.SetVisibleControl(Me, txtboxTrackingNumber, False)

            If State.ViewOnly Then
                ChangeEnabledProperty(cboPayeeSelector, False)
                ControlMgr.SetVisibleControl(Me, txtRepairDate, False) ' dont show in viewonly
                ControlMgr.SetVisibleControl(Me, cboRepairCode, False) ' dont show in viewonly

                ControlMgr.SetVisibleControl(Me, txtAcctStatusDate, True) ' felita passback
                ControlMgr.SetVisibleControl(Me, txtAcctStatusCode, True)  ' felita passback
                ControlMgr.SetVisibleControl(Me, txtboxTrackingNumber, True) ' felita passback
                LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("ACCT_STATUS_DATE") + ":"
                LabelRepairCode.Text = TranslationBase.TranslateLabelOrMessage("ACCT_STATUS") + ":"
                LabelPickUpDate.Text = TranslationBase.TranslateLabelOrMessage("ACCT_TRACK_NUMBER") + ":"

                ChangeEnabledProperty(txtInvoiceNumber, False)
                ChangeEnabledProperty(txtInvoiceDate, False)
                ControlMgr.SetVisibleControl(Me, ImageButtonInvoiceDate, False)

                ChangeEnabledProperty(cboCauseOfLossID, False)
                ControlMgr.SetVisibleControl(Me, btnUndo_Write, False)
                ControlMgr.SetVisibleControl(Me, TextboxPickupDate, False) ' dont show in viewonly
                ChangeEnabledProperty(ImageButtonPickupDate, False)

                ChangeEnabledProperty(cboDocumentTypeId, False)
                ChangeEnabledProperty(moTaxIdText, False)
                ChangeEnabledProperty(txtAcctStatusDate, False)
                ChangeEnabledProperty(txtAcctStatusCode, False)
                ChangeEnabledProperty(txtboxTrackingNumber, False)

            End If

            If isForClaimPayAdjustments() Then
                ControlMgr.SetVisibleControl(Me, LabelCauseOfLoss, False)
                ControlMgr.SetVisibleControl(Me, cboCauseOfLossID, False)
                ControlMgr.SetVisibleControl(Me, LabelRepairDate, False)
                ControlMgr.SetVisibleControl(Me, txtRepairDate, False)
                ControlMgr.SetVisibleControl(Me, ImageButtonRepairDate, False)
                ControlMgr.SetVisibleControl(Me, TextboxPickupDate, False)
                ControlMgr.SetVisibleControl(Me, ImageButtonPickupDate, False)
            End If

            ChangeEnabledProperty(txtInvoiceNumber, (Not State.ViewOnly) And True)
            ChangeEnabledProperty(txtInvoiceDate, (Not State.ViewOnly) And True)
            ControlMgr.SetVisibleForControlFamily(Me, ImageButtonInvoiceDate, (Not State.ViewOnly) And True, True)

            'WR 754196, Serial Number maintenance:
            ChangeEnabledProperty(TextSerialNumber, (Not State.ViewOnly) And True)

            'Pickup date: do not display if replacement and/or interface claim
            Dim blnCanDisplayVisitAndPickUpDates As Boolean
            If State.ClaimBO IsNot Nothing Then
                blnCanDisplayVisitAndPickUpDates = State.ClaimBO.CanDisplayVisitAndPickUpDates
            End If
            If State.ClaimBO IsNot Nothing AndAlso Not State.ClaimBO.LoanerTaken And Not State.isForClaimPayAdjust Then
                ControlMgr.SetVisibleForControlFamily(Me, ImageButtonPickupDate, blnCanDisplayVisitAndPickUpDates And Not State.ViewOnly, True)
                SetEnabledForControlFamily(ImageButtonPickupDate, blnCanDisplayVisitAndPickUpDates And Not State.ViewOnly, True)
                ControlMgr.SetVisibleControl(Me, TextboxPickupDate, blnCanDisplayVisitAndPickUpDates And Not State.isForClaimPayAdjust)
                ControlMgr.SetVisibleControl(Me, LabelPickUpDate, blnCanDisplayVisitAndPickUpDates And Not State.isForClaimPayAdjust)
            Else
                ControlMgr.SetVisibleControl(Me, TextboxPickupDate, False)
                ControlMgr.SetVisibleControl(Me, LabelPickUpDate, False)
                ControlMgr.SetVisibleControl(Me, ImageButtonPickupDate, False)
            End If
            If State.ViewOnly Then
                ControlMgr.SetVisibleControl(Me, LabelPickUpDate, True)
                ControlMgr.SetVisibleControl(Me, TextboxPickupDate, False)
            End If

            If oCompany.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) Then
                ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, True)
            End If

            Dim oCompaniesDv As DataView, oUser As New User
            oCompaniesDv = oUser.GetUserCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
            oCompaniesDv.RowFilter = "code in ('ABA','VBA','SBA')"
            If oCompaniesDv.Count > 0 Then
                ControlMgr.SetVisibleControl(Me, LabelInvDateAsterisk, True)
            Else
                ControlMgr.SetVisibleControl(Me, LabelInvDateAsterisk, False)
            End If

            DisableButtonsForClaimSystem()
            ''''''REQ-5565 check for claim  whether it is part of pre-invoice process
            If (State.ClaimBO.Company.UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
                Dim preInv As New PreInvoiceDetails
                Dim count As Integer

                count = preInv.CheckClaimInPreInvoice(State.ClaimBO.Claim_Id)
                If (count > 0) Then
                    ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
                    MasterPage.MessageController.Clear()
                    MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("CLAIM_CANNOT_PAID_INDIVIDUALLY"), False)
                Else
                    ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
                End If
            Else
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Try
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "CustomerName", LabelCustomerName)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "ServiceCenterName", LabelBillingSvcCenter)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "PayeeName", LabelPayee)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "SvcControlNumber", LabelInvNumber)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "RepairDate", LabelRepairDate)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "InvoiceDate", LabelInvoiceDate)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "LoanerReturnedDate", LabelLoanerReturnedDate)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "LaborAmt", LabelLabor)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "PartAmount", LabelParts)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "ServiceCharge", LabelSvcCharge)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "TripAmount", LabelTripAmount)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "ShippingAmount", LabelShipping)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "Amount", LabelTotal)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "RepairCodeId", LabelRepairCode)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "CauseOfLossID", LabelCauseOfLoss)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "RegionID", lblRigion)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "PerceptionIVA", lblPerception_Iva)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "PerceptionIIBB", lblPerception_IIBB)
            If Not State.isForClaimPayAdjust Then BindBOPropertyToLabel(State.ClaimInvoiceBO, "PickUpDate", LabelPickUpDate)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "PaymentMethodID", moPaymentMethodLabel)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "DocumentType", moDocumentTypeLabel)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "TaxId", moTaxIdLabel)
            BindBOPropertyToLabel(State.DisbursementBO, "VendorRegionDesc", lblRigion)
            BindBOPropertyToLabel(State.ClaimInvoiceBO, "SalvageAmt", LabelSalvageAmt)
            '''''Me.BindBOPropertyToNonLabeled(Me.State.ClaimInvoiceBO, "TaxId")

            ClearGridHeadersAndLabelsErrSign()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateFieldsForBankTransfer()

        If ((cboPayeeSelector.SelectedValue <> EMPTY_GUID AndAlso
            LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, GetSelectedItem(cboPayeeSelector)) = ClaimInvoice.PAYEE_OPTION_CUSTOMER) Or
            (PaymentMethodDrop.SelectedValue <> EMPTY_GUID AndAlso
            LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, GetSelectedItem(PaymentMethodDrop)) = Codes.PAYMENT_METHOD__BANK_TRANSFER)) Then

            PayeeBankInfo.LoadBankSortCodeList(oCompany, oCertificate.CUIT_CUIL)

            If oCompany.AttributeValues.Contains(Codes.DEFAULT_CLAIM_INVOICE_NUMBER) Then
                txtInvoiceNumber.Text = oCompany.AttributeValues.Value(Codes.DEFAULT_CLAIM_INVOICE_NUMBER)
            End If

            txtInvoiceDate.Text = GetDateFormattedString(DateTime.Now)
            txtRepairDate.Text = GetDateFormattedString(DateTime.Now)
        Else
            SetPayeeBankInvoiceControlsEmpty()
        End If

    End Sub
    Private Sub SetFieldsEmptyForBankTrf()
        If oCompany IsNot Nothing AndAlso oCompany.AttributeValues.Contains(Codes.DEFAULT_CLAIM_BANK_SORT_CODE) Then
            If oCompany.AttributeValues.Value(Codes.DEFAULT_CLAIM_BANK_SORT_CODE) = Codes.YESNO_Y Then
                If ((cboPayeeSelector.SelectedValue <> EMPTY_GUID AndAlso
                     LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, GetSelectedItem(cboPayeeSelector)) <> ClaimInvoice.PAYEE_OPTION_CUSTOMER) Or
                    (PaymentMethodDrop.SelectedValue <> EMPTY_GUID AndAlso
                     LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, GetSelectedItem(PaymentMethodDrop)) <> Codes.PAYMENT_METHOD__BANK_TRANSFER)) Then
                    SetPayeeBankInvoiceControlsEmpty()
                End If
            End If
        End If
    End Sub
    Private Sub SetPayeeBankInvoiceControlsEmpty()
        PayeeBankInfo.SetFieldsEmpty()
        PayeeBankInfo.HideCboBankSortCode()
        txtInvoiceNumber.Text = String.Empty
        txtInvoiceDate.Text = String.Empty
        txtRepairDate.Text = String.Empty
    End Sub
    Protected Sub PopulateDropdowns()
        Try
            If State.ClaimBO Is Nothing Then
                State.ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(State.ClaimInvoiceBO.ClaimId)
            End If

            'Me.BindListControlToDataView(Me.cboCauseOfLossID, LookupListNew.GetCauseOfLossByCoverageTypeLookupList(Authentication.LangId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, Me.State.ClaimBO.CoverageTypeId))

            Dim listcontextForCauseOfLoss As ListContext = New ListContext()
            listcontextForCauseOfLoss.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            listcontextForCauseOfLoss.CoverageTypeId = State.ClaimBO.CoverageTypeId
            listcontextForCauseOfLoss.DealerId = oCertificate.DealerId
            listcontextForCauseOfLoss.ProductCode = oCertificate.ProductCode
            listcontextForCauseOfLoss.LanguageId = Authentication.LangId
            cboCauseOfLossID.Populate(CommonConfigManager.Current.ListManager.GetList("CauseOfLossByCoverageTypeAndSplSvcLookupList", , listcontextForCauseOfLoss), New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                })

            BindListControlToDataView(cboRepairCode, LookupListNew.GetRepairCodeLookupList(oCompany.CompanyGroupId))

            Dim listContext As ListContext = New ListContext()
            listContext.CompanyGroupId = oCompany.CompanyGroupId
            cboRepairCode.Populate(CommonConfigManager.Current.ListManager.GetList("RepairCodebyCompanyGroup", , listContext), New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True
                                                })

            'Dim payeeDV As DataView = LookupListNew.GetPayeeLookupList(Authentication.LangId)

            Dim claimServiceCenter As ServiceCenter = New ServiceCenter(State.ClaimInvoiceBO.Invoiceable.ServiceCenterId)
            If claimServiceCenter.MasterCenterId.Equals(Guid.Empty) Then

                'payeeDV.RowFilter = payeeDV.RowFilter & " and code <>'" & ClaimInvoice.PAYEE_OPTION_MASTER_CENTER & "'"
                Dim payeeTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                  If (li.Code <> ClaimInvoice.PAYEE_OPTION_MASTER_CENTER) Then
                                                                                      Return li.Translation
                                                                                  Else
                                                                                      Return Nothing
                                                                                  End If
                                                                              End Function
                cboPayeeSelector.Populate(CommonConfigManager.Current.ListManager.GetList("PAYEE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True,
                                                    .TextFunc = payeeTextFunc
                                                 })


            End If

            'Commented the code for DEF-825
            'Filter out the master center - changes for Req 124
            'payeeDV.RowFilter = payeeDV.RowFilter & " and code <>'" & ClaimInvoice.PAYEE_OPTION_MASTER_CENTER & "'"

            If State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                'payeeDV.RowFilter = payeeDV.RowFilter & " and code <>'" & ClaimInvoice.PAYEE_OPTION_LOANER_CENTER & "'"
                Dim payeeTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                  If (li.Code <> ClaimInvoice.PAYEE_OPTION_LOANER_CENTER) Then
                                                                                      Return li.Translation
                                                                                  Else
                                                                                      Return Nothing
                                                                                  End If
                                                                              End Function
                cboPayeeSelector.Populate(CommonConfigManager.Current.ListManager.GetList("PAYEE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True,
                                                    .TextFunc = payeeTextFunc
                                                 })
            End If

            'Me.BindListControlToDataView(Me.cboPayeeSelector, payeeDV, "DESCRIPTION", , False) 'AA

            cboPayeeSelector.Populate(CommonConfigManager.Current.ListManager.GetList("PAYEE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True
                                                })

            '

            'Me.BindListControlToDataView(PaymentMethodDrop, LookupListNew.GetPaymentMethodLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True, (ElitaPlusIdentity.Current.ActiveUser.Id).ToString, oCompany.Id.ToString, True), "DESCRIPTION", "ID", True)
            'PaymentMethod
            Dim listcontextForPaymentMethod As ListContext = New ListContext()
            listcontextForPaymentMethod.LanguageId = Authentication.LangId
            PaymentMethodDrop.Populate(CommonConfigManager.Current.ListManager.GetList("PaymentMethod", , listcontextForPaymentMethod), New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                })

            ExcludeFromDropdown(Codes.ATTR_DARTY_GIFT_CARD_TYPE)

            Dim dvDocumentType As DataView = LookupListNew.DropdownLookupList("DTYP", Authentication.LangId, True)
            'dvDocumentType.RowFilter = dvDocumentType.RowFilter & " and code <>'" & Codes.DOCUMENT_TYPE__CON & "'"
            'Me.BindListControlToDataView(Me.cboDocumentTypeId, dvDocumentType)
            Dim documentTypeTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                     If (li.Code <> Codes.DOCUMENT_TYPE__CON) Then
                                                                                         Return li.Translation
                                                                                     Else
                                                                                         Return Nothing
                                                                                     End If
                                                                                 End Function
            cboDocumentTypeId.Populate(CommonConfigManager.Current.ListManager.GetList("DTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True,
                                                    .TextFunc = documentTypeTextFunc
                                                 })
            'Me.BindListControlToDataView(cboRegionDropID, LookupListNew.GetRegionLookupList(oCompany.CountryId))

            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.CountryId = oCompany.CountryId
            Dim oRegionList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RegionsByCountry", context:=oListContext)
            cboRegionDropID.Populate(oRegionList, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })


            Dim s As Int16 = 0
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Public Sub ExcludeFromDropdown(value As String)
        Dim hasGiftCardAttriute As Boolean = False

        If (PaymentMethodDrop IsNot Nothing AndAlso PaymentMethodDrop.Items.Count > 0) Then

            If (value <> String.Empty) Then

                If (oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = value).Count > 0) Then
                    hasGiftCardAttriute = True
                End If

                Dim listItem As ListItem = PaymentMethodDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode("PMTHD", Codes.PAYMENT_METHOD__DARTY_GIFT_CARD))

                'Exclude Darty gift card from the dealers with no attribute or dealer with the attribute and payee not Customer
                If (listItem IsNot Nothing) Then
                    If (Not hasGiftCardAttriute OrElse
                        (hasGiftCardAttriute And (cboPayeeSelector.SelectedValue <> String.Empty AndAlso
                            LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, GetSelectedItem(cboPayeeSelector)) <> ClaimInvoice.PAYEE_OPTION_CUSTOMER))) Then

                        'PaymentMethodDrop.Items.Remove(listItem)
                        listItem.Enabled = False

                    Else
                        listItem.Enabled = True
                    End If
                End If

            End If
        End If

    End Sub

    Private Sub LoadClaimAuthDetail()
        Try
            Dim parttotal As Decimal = 0
            If State.AuthDetailEnabled Then
                State.ClaimAuthDetailBO = New ClaimAuthDetail(State.ClaimBO.Id, True)
                If State.ClaimAuthDetailBO IsNot Nothing Then
                    ' There is a previously record created for this claim, the user does not have to add the detail before paying claim; 
                    ' Pay Claim button to be enabled
                    State.AuthDetailExist = True
                    State.PartsInfoBO = New PartsInfo()
                    Dim partInfoPartAmount As DecimalType = State.PartsInfoBO.getTotalCost(State.ClaimBO.Id)
                    If partInfoPartAmount.Value > 0 Then State.ClaimAuthDetailBO.PartAmount = partInfoPartAmount

                Else
                    ' No previously record created for this claim, the user must add the detail before paying claim; 
                    ' Pay Claim button to be disabled
                    State.AuthDetailExist = False
                End If
            End If
        Catch ex As DataNotFoundException
            ' No previously record created for this claim, the user must add the detail before paying claim; 
            ' Pay Claim button to be disabled
            State.AuthDetailExist = False
            State.ClaimAuthDetailBO = Nothing
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub UpdateClaimInvoiceBOWithClaimAuthDetail()
        Try
            If State.AuthDetailEnabled Then
                If State.ClaimAuthDetailBO IsNot Nothing Then
                    With State.ClaimInvoiceBO
                        .LaborAmt = State.ClaimAuthDetailBO.LaborAmount
                        .PartAmount = State.ClaimAuthDetailBO.PartAmount
                        .ServiceCharge = State.ClaimAuthDetailBO.ServiceCharge
                        .TripAmount = State.ClaimAuthDetailBO.TripAmount
                        .OtherExplanation = State.ClaimAuthDetailBO.OtherExplanation
                        .OtherAmount = State.ClaimAuthDetailBO.OtherAmount
                        .ShippingAmount = State.ClaimAuthDetailBO.ShippingAmount
                        .DiagnosticsAmount = State.ClaimAuthDetailBO.DiagnosticsAmount
                        .DispositionAmount = State.ClaimAuthDetailBO.DispositionAmount
                        .Amount = GetAuthDetailTotal + State.ClaimAuthDetailBO.TotalTaxAmount
                    End With
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Function GetAuthDetailTotal() As Decimal
        Dim amount As Decimal = 0
        If State.ClaimAuthDetailBO.LaborAmount IsNot Nothing Then amount += State.ClaimAuthDetailBO.LaborAmount.Value
        If State.ClaimAuthDetailBO.PartAmount IsNot Nothing Then amount += State.ClaimAuthDetailBO.PartAmount.Value
        If State.ClaimAuthDetailBO.ServiceCharge IsNot Nothing Then amount += State.ClaimAuthDetailBO.ServiceCharge.Value
        If State.ClaimAuthDetailBO.TripAmount IsNot Nothing Then amount += State.ClaimAuthDetailBO.TripAmount.Value
        If State.ClaimAuthDetailBO.ShippingAmount IsNot Nothing Then amount += State.ClaimAuthDetailBO.ShippingAmount.Value
        If State.ClaimAuthDetailBO.OtherAmount IsNot Nothing Then amount += State.ClaimAuthDetailBO.OtherAmount.Value
        If State.ClaimAuthDetailBO.DiagnosticsAmount IsNot Nothing Then amount += State.ClaimAuthDetailBO.DiagnosticsAmount.Value
        If State.ClaimAuthDetailBO.DispositionAmount IsNot Nothing Then amount += State.ClaimAuthDetailBO.DispositionAmount.Value


        Return amount
    End Function

    Protected Sub PopulateFormFromBOs()

        Try

            Dim claimServiceCenter As ServiceCenter = New ServiceCenter(State.ClaimInvoiceBO.Invoiceable.ServiceCenterId)
            Dim GrandTotal As Decimal
            UpdateClaimInvoiceBOWithClaimAuthDetail()
            'If Not claimServiceCenter.WithholdingRate Is Nothing Then Me.State.WithholdingRate = claimServiceCenter.WithholdingRate.Value * (-1)

            With State.ClaimInvoiceBO

                PopulateControlFromBOProperty(TextboxClaimNumber, .ClaimNumber)
                PopulateControlFromBOProperty(TextboxCertificateNumber, .CertificateNumber)
                PopulateControlFromBOProperty(TextboxRiskType, .RiskType)
                PopulateControlFromBOProperty(TextManufacturer, .Manufacturer)
                PopulateControlFromBOProperty(TextModel, .Model)
                PopulateControlFromBOProperty(txtPerceptionIva, .PerceptionIVA)
                PopulateControlFromBOProperty(txtPerceptionIIBB, .PerceptionIIBB)

                If State.ClaimInvoiceBO.SerialNumberTempContainer Is Nothing Then
                    PopulateControlFromBOProperty(TextSerialNumber, .SerialNumber)
                Else
                    PopulateControlFromBOProperty(TextSerialNumber, .SerialNumberTempContainer)
                End If

                PopulateControlFromBOProperty(CustomerAddressLabel, .CustomerName & Environment.NewLine & (New Address(State.ClaimInvoiceBO.CustomerAddressID)).MailingAddressLabel)
                PopulateControlFromBOProperty(ServiceCenterAddressLabel, .ServiceCenterName & Environment.NewLine & (New Address(State.ClaimInvoiceBO.ServiceCenterAddressID)).MailingAddressLabel)
                PopulateControlFromBOProperty(txtInvoiceNumber, .SvcControlNumber)
                PopulateControlFromPropertyName(State.ClaimInvoiceBO, cboCauseOfLossID, "CauseOfLossID")
                PopulateControlFromBOProperty(cboRepairCode, .RepairCodeId)
                PopulateControlFromBOProperty(txtLoanerReturnedDate, .LoanerReturnedDate)
                PopulateControlFromBOProperty(txtRepairDate, .RepairDate)
                PopulateControlFromBOProperty(txtInvoiceDate, .InvoiceDate)
                PopulateControlFromBOProperty(txtAcctStatusDate, State.DisbursementBO.StatusDate)
                PopulateControlFromBOProperty(txtboxTrackingNumber, State.DisbursementBO.TrackingNumber)
                PopulateControlFromBOProperty(txtAcctStatusCode, LookupListNew.GetDescriptionFromCode(LookupListNew.LK_ACCTSTATUS, State.DisbursementBO.AcctStatus))

                'Me.State.ClaimInvoiceBO.CalculateAmounts()
                'check if the user is coming from the parts info page (i.e previous page was Parts info page), if yes, then refresh the parts info value if it is changed
                If NavController IsNot Nothing AndAlso NavController.PrevNavState IsNot Nothing AndAlso NavController.PrevNavState.Name IsNot Nothing AndAlso NavController.PrevNavState.Name = "PARTS_INFO" Then
                    'get the amount entered in parts info table
                    Dim PartsInfoValue As DecimalType = PartsInfo.getTotalCost(State.ClaimBO.Id).Value
                    'Check if the parts amount has been changed, if yes, the refresh the parts amount value
                    If State.ClaimInvoiceBO.PartAmount <> PartsInfoValue Then
                        'set it in part amount value
                        State.ClaimInvoiceBO.PartAmount = PartsInfoValue
                        State.ClaimInvoiceBO.CalculateAmounts()
                    End If
                End If


                If State.AuthDetailEnabled AndAlso State.ClaimBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK AndAlso State.ClaimAuthDetailBO IsNot Nothing Then

                    PopulateControlFromBOProperty(txtLabor, State.ClaimAuthDetailBO.LaborAmount)
                    PopulateControlFromBOProperty(txtParts, State.ClaimAuthDetailBO.PartAmount)
                    PopulateControlFromBOProperty(txtServiceCharge, State.ClaimAuthDetailBO.ServiceCharge)
                    PopulateControlFromBOProperty(txtTripAmt, State.ClaimAuthDetailBO.TripAmount)
                    PopulateControlFromBOProperty(txtShipping, State.ClaimAuthDetailBO.ShippingAmount)
                    PopulateControlFromBOProperty(txtOtherDesc, State.ClaimAuthDetailBO.OtherExplanation)
                    PopulateControlFromBOProperty(txtOtherAmt, State.ClaimAuthDetailBO.OtherAmount)
                    PopulateControlFromBOProperty(txtDiagnostics, State.ClaimAuthDetailBO.DiagnosticsAmount)
                    PopulateControlFromBOProperty(txtDisposition, State.ClaimAuthDetailBO.DispositionAmount)
                    'Me.PopulateControlFromBOProperty(Me.txtIvaTax, Me.State.ClaimAuthDetailBO.IvaAmount)
                    Dim authdetailTtl As Decimal = GetAuthDetailTotal
                    Dim dedAmt As Decimal
                    If State.ClaimBO.Deductible Is Nothing Then
                        dedAmt = 0
                    Else
                        dedAmt = State.ClaimBO.Deductible.Value
                    End If

                    PopulateControlFromBOProperty(txtTotalTaxAmount, State.ClaimInvoiceBO.TotalTaxAmount.Value)

                    Dim totAmount As Decimal = authdetailTtl + State.ClaimInvoiceBO.TotalTaxAmount.Value - dedAmt
                    PopulateControlFromBOProperty(txtTotal, totAmount)
                    If State.ClaimInvoiceBO.WithholdingAmount Is Nothing Then
                        State.ClaimInvoiceBO.WithholdingAmount = New DecimalType(0)
                    Else
                        PopulateControlFromBOProperty(txtTotalWithholdingAmount, .WithholdingAmount)
                    End If
                    GrandTotal = totAmount + State.ClaimInvoiceBO.WithholdingAmount.Value
                    txtGrandTotal.Text = System.Convert.ToString(GrandTotal)
                    hdGrandTotalAmt.Value = GrandTotal

                    PopulateControlFromBOProperty(txtSubTotal, authdetailTtl)
                    hdSubTotalAmt.Value = authdetailTtl
                    hdTotal.Value = txtTotal.Text
                    hdOtherAmt.Value = txtOtherAmt.Text
                    hdDispositionAmt.Value = txtDisposition.Text
                    hdDiagnosticsAmt.Value = txtDiagnostics.Text
                    hdTotalTaxAmount.Value = txtTotalTaxAmount.Text
                    hdpaymenttocustomer.Value = txtPaymenttoCustomer.Text

                Else
                    PopulateControlFromBOProperty(txtLabor, .LaborAmt)
                    PopulateControlFromBOProperty(txtParts, .PartAmount)
                    PopulateControlFromBOProperty(txtServiceCharge, .ServiceCharge)
                    PopulateControlFromBOProperty(txtTripAmt, .TripAmount)
                    PopulateControlFromBOProperty(txtShipping, .ShippingAmount)
                    PopulateControlFromBOProperty(txtOtherDesc, .OtherExplanation)
                    PopulateControlFromBOProperty(txtOtherAmt, .OtherAmount)
                    PopulateControlFromBOProperty(txtDiagnostics, .DiagnosticsAmount)
                    PopulateControlFromBOProperty(txtDisposition, .DispositionAmount)
                    'Me.PopulateControlFromBOProperty(Me.txtIvaTax, .IvaAmount)
                    PopulateControlFromBOProperty(txtDeductibleTaxAmount, .DeductibleTaxAmount)
                    PopulateControlFromBOProperty(txtTotalWithholdingAmount, .WithholdingAmount)
                    PopulateControlFromBOProperty(txtTotal, .Amount)
                    PopulateControlFromBOProperty(txtSubTotal, CalculateSubTotal())
                    PopulateControlFromBOProperty(txtTotalTaxAmount, State.ClaimInvoiceBO.TotalTaxAmount.Value)
                    PopulateControlFromBOProperty(txtPaymenttoCustomer, .PaytocustomerAmount)
                    hdTotal.Value = txtTotal.Text
                    hdOtherAmt.Value = txtOtherAmt.Text
                    hdDispositionAmt.Value = txtDisposition.Text
                    hdDiagnosticsAmt.Value = txtDiagnostics.Text
                    hdTotalTaxAmount.Value = txtTotalTaxAmount.Text
                    hdpaymenttocustomer.Value = txtPaymenttoCustomer.Text

                End If

                Dim PerceptionIIBB As Decimal
                Dim PerceptionIVA As Decimal
                Dim TotalAmount As Decimal

                Decimal.TryParse(txtPerceptionIIBB.Text, PerceptionIIBB)
                Decimal.TryParse(txtPerceptionIva.Text, PerceptionIVA)
                Decimal.TryParse(txtTotal.Text, TotalAmount)

                If State.ClaimInvoiceBO.WithholdingAmount Is Nothing Then State.ClaimInvoiceBO.WithholdingAmount = New DecimalType(0D)
                GrandTotal = TotalAmount + State.TotalManualTaxes + PerceptionIIBB + PerceptionIVA + State.ClaimInvoiceBO.WithholdingAmount.Value
                txtGrandTotal.Text = System.Convert.ToString(GrandTotal)
                hdGrandTotalAmt.Value = GrandTotal

                PopulateControlFromBOProperty(txtIvaTax, .IvaAmount)
                PopulateControlFromBOProperty(txtSalvageAmt, .Invoiceable.SalvageAmount)
                'Me.PopulateControlFromBOProperty(Me.txtLiabilityLimit, .Invoiceable.LiabilityLimit)
                PopulateControlFromBOProperty(txtLiabilityLimit, CalculateLiabilityLimit())
                PopulateControlFromBOProperty(txtDeductible, .Invoiceable.Deductible)
                PopulateControlFromBOProperty(txtDiscount, .Invoiceable.DiscountAmount)
                PopulateControlFromBOProperty(txtAuthAmt, .Invoiceable.AuthorizedAmount)
                'Me.PopulateControlFromBOProperty(Me.txtAboveLiability, .Invoiceable.AboveLiability)
                PopulateControlFromBOProperty(txtAboveLiability, AboveLiability())
                PopulateControlFromBOProperty(txtAssurantPays, .Invoiceable.AssurantPays)
                PopulateControlFromBOProperty(txtConsumerPays, .Invoiceable.ConsumerPays)

                If (State.ViewOnly = True) Or (State.ComingFromChildForm = True) Then
                    PopulateControlFromBOProperty(cboPayeeSelector, State.DisbursementBO.PayeeOptionId)
                Else
                    Dim PayeeOptionId As Guid
                    If claimServiceCenter.PayMaster = True Then
                        PayeeOptionId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, ClaimInvoice.PAYEE_OPTION_MASTER_CENTER)
                        SetSelectedItem(cboPayeeSelector, PayeeOptionId)
                    Else
                        PayeeOptionId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, ClaimInvoice.PAYEE_OPTION_SERVICE_CENTER)
                        SetSelectedItem(cboPayeeSelector, PayeeOptionId)
                    End If
                End If
                PayeeChanged(GetSelectedItem(cboPayeeSelector), False)

                PopulateControlFromBOProperty(txtPayeeName, State.DisbursementBO.Payee)

                'If Me.State.ViewOnly And Me.PaymentMethodDrop.Visible Then
                If PaymentMethodDrop.Visible Then
                    PopulateControlFromBOProperty(PaymentMethodDrop, State.ClaimInvoiceBO.PaymentMethodID)
                    If State.ViewOnly Then
                        ChangeEnabledProperty(PaymentMethodDrop, False)
                    Else
                        ChangeEnabledProperty(PaymentMethodDrop, True)
                    End If
                    PaymentMethodChanged(State.ClaimInvoiceBO.PaymentMethodID, State.DisbursementBO.PayeeOptionId, False)
                End If

                If State.ViewOnly And PaymentMethodDrop.Visible Then
                    If (State.DisbursementBO.PayeeOptionId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, ClaimInvoice.PAYEE_OPTION_CUSTOMER) Or
                        State.DisbursementBO.PayeeOptionId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, ClaimInvoice.PAYEE_OPTION_OTHER) AndAlso
                        (State.ClaimInvoiceBO.PaymentMethodID = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYMENTMETHOD, "CTT"))) Then
                        PayeeBankInfo.DisplayTaxId()
                    Else
                        PayeeBankInfo.HideTaxId()
                    End If
                End If

                If (oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.AUTH_LESS_DEDUCT_Y)) Then
                    PayDeductible.Value = "Y_AUTH_LESS_DEDUCT"
                    RemainingDeductible.Value = (.Invoiceable.Deductible.Value - .AlreadyPaidDeductible.Value).ToString()
                    PopulateControlFromBOProperty(txtDeductibleAmount, .DeductibleAmount)
                ElseIf (oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y)) Then
                    PayDeductible.Value = "Y_FULL_INVOICE"
                Else
                    PayDeductible.Value = "N"
                End If

                MasterCenterIvaResponsible.Value = "N"
                If Not claimServiceCenter.MasterCenterId.Equals(Guid.Empty) Then
                    Dim masterCenter As ServiceCenter = New ServiceCenter(claimServiceCenter.MasterCenterId)
                    If masterCenter.IvaResponsibleFlag Then
                        MasterCenterIvaResponsible.Value = "Y"
                    End If
                End If

                If State.ClaimInvoiceBO.isTaxTypeInvoice() Then
                    If claimServiceCenter IsNot Nothing Then
                        PopulateControlFromBOProperty(cboRegionDropID, claimServiceCenter.Address.RegionId)
                    End If
                End If

                If claimServiceCenter.IvaResponsibleFlag Then
                    ServiceCenterIvaResponsible.Value = "Y"
                Else
                    ServiceCenterIvaResponsible.Value = "N"
                End If

                LoanerCenterIvaResponsible.Value = "N"
                If Not State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                    Dim loanerCenter As ServiceCenter = New ServiceCenter(State.ClaimInvoiceBO.Invoiceable.LoanerCenterId)
                    If loanerCenter.IvaResponsibleFlag Then
                        LoanerCenterIvaResponsible.Value = "Y"
                    End If
                End If

                If (ServiceCenterIvaResponsible.Value = "Y") Or (MasterCenterIvaResponsible.Value = "Y") Or (LoanerCenterIvaResponsible.Value = "Y") Then
                    TaxRate.Value = .TaxRate.Value.ToString()
                    DeductibleTaxRate.Value = .DeductibleTaxRate.Value.ToString()
                    PopulateControlFromBOProperty(txtDeductibleTaxAmount, .DeductibleTaxAmount)
                End If

                'Pickup date: do not display if replacement and/or interface claim
                'Interface claim = Not claim.Source.Equals(String.Empty)
                If State.ClaimBO IsNot Nothing AndAlso (State.ClaimBO.CanDisplayVisitAndPickUpDates And Not State.ClaimBO.LoanerTaken) And Not State.isForClaimPayAdjust Then
                    PopulateControlFromBOProperty(TextboxPickupDate, State.ClaimBO.PickUpDate)
                End If
                hdAlreadyPaid.Value = .AlreadyPaid.Value.ToString()
                hdAssurantPays.Value = .Invoiceable.AssurantPays.Value.ToString()
                hdSalvageAmt.Value = .Invoiceable.SalvageAmount.Value.ToString()
                hdDeductibleAmt.Value = .Invoiceable.Deductible.Value.ToString()

                If Not LookupListNew.GetIdFromCode(LookupListNew.GetRepairCodeLookupList(oCompany.CompanyGroupId), Codes.REPAIR_CODE__DELIVERY_FEE_NO_DEDUCTIBLE) = Guid.Empty Then
                    hdDeliveryFeeOnly.Value = LookupListNew.GetIdFromCode(LookupListNew.GetRepairCodeLookupList(oCompany.CompanyGroupId), Codes.REPAIR_CODE__DELIVERY_FEE_NO_DEDUCTIBLE).ToString
                End If

                PopulateControlFromBOProperty(txtAlreadyPaid, .AlreadyPaid)

                PopulateControlFromBOProperty(txtRemainingAmt, .RemainingAmount)

                'DEF-394 - ALR -- Changed to populate payment method based on service center value
                If State.ClaimInvoiceBO.PaymentMethodID.Equals(Guid.Empty) Then
                    If Not claimServiceCenter.PaymentMethodId.Equals(Guid.Empty) Then
                        State.ClaimInvoiceBO.PaymentMethodID = claimServiceCenter.PaymentMethodId
                        State.ClaimInvoiceBO.PaymentMethodCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, State.ClaimInvoiceBO.PaymentMethodID)
                        State.DisbursementBO.PaymentMethod = State.ClaimInvoiceBO.PaymentMethodCode
                        If PaymentMethodDrop.Items.FindByText(State.ClaimInvoiceBO.PaymentMethodCode) IsNot Nothing Then
                            PopulateControlFromBOProperty(PaymentMethodDrop, State.ClaimInvoiceBO.PaymentMethodID)
                        End If
                    End If
                End If

                If State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR_REPLACEMENT Or State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                    hdClaimMethodOfRepair.Value = "RPL"
                    LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT")
                Else
                    hdClaimMethodOfRepair.Value = "RPR"
                    LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPAIR")
                End If
                LoadClaimTaxRates()

            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub LoadClaimTaxRates()

        If State.ClaimTaxRatesData Is Nothing Then
            Dim RegionId As Guid
            If State.ClaimInvoiceBO.PayeeAddress Is Nothing Then
                Dim addressObj As New Address(State.ClaimInvoiceBO.ServiceCenterAddressID)
                RegionId = addressObj.RegionId
            Else
                RegionId = State.ClaimInvoiceBO.PayeeAddress.RegionId
            End If
            State.ClaimTaxRatesData = State.ClaimInvoiceBO.ClaimTaxRatesData(RegionId, hdClaimMethodOfRepair.Value)
        End If

        With State.ClaimTaxRatesData
            hdTaxRateClaimDiagnostics.Value = .taxRateClaimDiagnostics.ToString
            hdComputeMethodClaimDiagnostics.Value = .computeMethodCodeClaimDiagnostics
            hdApplyWithholdingFlagClaimDiagnostics.Value = .applyWithholdingFlagClaimDiagnostics

            hdTaxRateClaimOther.Value = .taxRateClaimOther.ToString
            hdComputeMethodClaimOther.Value = .computeMethodCodeClaimOther
            hdApplyWithholdingFlagClaimOther.Value = .applyWithholdingFlagClaimOther

            hdTaxRateClaimDisposition.Value = .taxRateClaimDisposition.ToString
            hdComputeMethodClaimDisposition.Value = .computeMethodCodeClaimDisposition
            hdApplyWithholdingFlagClaimDisposition.Value = .applyWithholdingFlagClaimDisposition

            hdTaxRateClaimLabor.Value = .taxRateClaimLabor.ToString
            hdComputeMethodClaimLabor.Value = .computeMethodCodeClaimLabor
            hdApplyWithholdingFlagClaimLabor.Value = .applyWithholdingFlagClaimLabor

            hdTaxRateClaimParts.Value = .taxRateClaimParts.ToString
            hdComputeMethodClaimParts.Value = .computeMethodCodeClaimParts
            hdApplyWithholdingFlagClaimParts.Value = .applyWithholdingFlagClaimParts

            hdTaxRateClaimShipping.Value = .taxRateClaimShipping.ToString
            hdComputeMethodClaimShipping.Value = .computeMethodCodeClaimShipping
            hdApplyWithholdingFlagClaimShipping.Value = .applyWithholdingFlagClaimShipping

            hdTaxRateClaimService.Value = .taxRateClaimService.ToString
            hdComputeMethodClaimService.Value = .computeMethodCodeClaimService
            hdApplyWithholdingFlagClaimService.Value = .applyWithholdingFlagClaimService

            hdTaxRateClaimTrip.Value = .taxRateClaimTrip.ToString
            hdComputeMethodClaimTrip.Value = .computeMethodCodeClaimTrip
            hdApplyWithholdingFlagClaimTrip.Value = .applyWithholdingFlagClaimTrip

            ComputeAutoTaxes()

            If State.ClaimInvoiceBO.ServiceCenterWithholdingRate <> 0 Then
                If .applyWithholdingFlagClaimLabor.Equals("Y") Then CheckBoxLaborWithhodling.Checked = True
                If .applyWithholdingFlagClaimParts.Equals("Y") Then CheckBoxPartWithhodling.Checked = True
                If .applyWithholdingFlagClaimService.Equals("Y") Then CheckBoxServiceWithhodling.Checked = True
                If .applyWithholdingFlagClaimTrip.Equals("Y") Then CheckBoxTripWithhodling.Checked = True
                If .applyWithholdingFlagClaimShipping.Equals("Y") Then CheckBoxShippingWithhodling.Checked = True
                If .applyWithholdingFlagClaimDisposition.Equals("Y") Then CheckBoxDispositionWithhodling.Checked = True
                If .applyWithholdingFlagClaimDiagnostics.Equals("Y") Then CheckBoxDiagnosticsWithhodling.Checked = True
                If .applyWithholdingFlagClaimOther.Equals("Y") Then CheckBoxOtherWithhodling.Checked = True
            Else
                CheckBoxLaborWithhodling.Checked = False
                CheckBoxPartWithhodling.Checked = False
                CheckBoxServiceWithhodling.Checked = False
                CheckBoxTripWithhodling.Checked = False
                CheckBoxShippingWithhodling.Checked = False
                CheckBoxDispositionWithhodling.Checked = False
                CheckBoxDiagnosticsWithhodling.Checked = False
                CheckBoxOtherWithhodling.Checked = False
            End If

            hdWithholdingRate.Value = State.ClaimInvoiceBO.ServiceCenterWithholdingRate

        End With
    End Sub

    Private Sub ComputeAutoTaxes()
        State.ClaimInvoiceBO.TotalTaxAmount = New DecimalType(0D)
        State.ClaimInvoiceBO.WithholdingAmount = New DecimalType(0D)
        Dim tmpAmtSubjectToWithHoding As Decimal = 0

        Try
            txtPartsTax.Text = hdPartsAmt.Value.ToString
            txtPartsTax.Text = computeTaxAmtByComputeMethod(CType(txtParts.Text, Decimal), CType(hdTaxRateClaimParts.Value, Decimal), hdComputeMethodClaimParts.Value)
            If hdApplyWithholdingFlagClaimParts.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding = CType(txtParts.Text, Decimal)
                If State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then CheckBoxPartWithhodling.Checked = True
            End If
        Catch ex As Exception
            txtPartsTax.Text = ""
        End Try

        Try
            txtLaborTax.Text = hdLaborAmt.Value.ToString
            txtLaborTax.Text = computeTaxAmtByComputeMethod(CType(txtLabor.Text, Decimal), CType(hdTaxRateClaimLabor.Value, Decimal), hdComputeMethodClaimLabor.Value)
            If hdApplyWithholdingFlagClaimLabor.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(txtLabor.Text, Decimal)
                If State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then CheckBoxLaborWithhodling.Checked = True
            End If

        Catch ex As Exception
            txtLaborTax.Text = ""
        End Try

        Try
            txtServiceChargeTax.Text = hdServiceChargeAmt.Value.ToString
            txtServiceChargeTax.Text = computeTaxAmtByComputeMethod(CType(txtServiceCharge.Text, Decimal), CType(hdTaxRateClaimService.Value, Decimal), hdComputeMethodClaimService.Value)
            If hdApplyWithholdingFlagClaimService.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(txtServiceCharge.Text, Decimal)
                If State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then CheckBoxServiceWithhodling.Checked = True
            End If
        Catch ex As Exception
            txtServiceChargeTax.Text = ""
        End Try

        Try
            txtTripAmtTax.Text = hdTripAmt.Value.ToString
            txtTripAmtTax.Text = computeTaxAmtByComputeMethod(CType(txtTripAmt.Text, Decimal), CType(hdTaxRateClaimTrip.Value, Decimal), hdComputeMethodClaimTrip.Value)
            If hdApplyWithholdingFlagClaimTrip.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(txtTripAmt.Text, Decimal)
                If State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then CheckBoxTripWithhodling.Checked = True
            End If
        Catch ex As Exception
            txtTripAmtTax.Text = ""
        End Try

        Try
            txtShippingTax.Text = hdShippingAmt.Value.ToString
            txtShippingTax.Text = computeTaxAmtByComputeMethod(CType(txtShipping.Text, Decimal), CType(hdTaxRateClaimShipping.Value, Decimal), hdComputeMethodClaimShipping.Value)
            If hdApplyWithholdingFlagClaimShipping.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(txtShipping.Text, Decimal)
                If State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then CheckBoxShippingWithhodling.Checked = True
            End If
        Catch ex As Exception
            txtShippingTax.Text = ""
        End Try

        Try
            txtDispositionTax.Text = hdDispositionAmt.Value.ToString
            txtDispositionTax.Text = computeTaxAmtByComputeMethod(CType(txtDisposition.Text, Decimal), CType(hdTaxRateClaimDisposition.Value, Decimal), hdComputeMethodClaimDisposition.Value)
            If hdApplyWithholdingFlagClaimDisposition.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(txtDisposition.Text, Decimal)
                If State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then CheckBoxDispositionWithhodling.Checked = True
            End If
        Catch ex As Exception
            txtDispositionTax.Text = ""
        End Try

        Try
            txtDiagnosticsTax.Text = hdDiagnosticsAmt.Value.ToString
            txtDiagnosticsTax.Text = computeTaxAmtByComputeMethod(CType(txtDiagnostics.Text, Decimal), CType(hdTaxRateClaimDiagnostics.Value, Decimal), hdComputeMethodClaimDiagnostics.Value)
            If hdApplyWithholdingFlagClaimDiagnostics.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(txtDiagnostics.Text, Decimal)
                If State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then CheckBoxDiagnosticsWithhodling.Checked = True
            End If
        Catch ex As Exception
            txtDiagnosticsTax.Text = ""
        End Try

        Try
            txtOtherAmt.Text = hdOtherAmt.Value.ToString
            txtOtherTax.Text = computeTaxAmtByComputeMethod(CType(txtOtherAmt.Text, Decimal), CType(hdTaxRateClaimOther.Value, Decimal), hdComputeMethodClaimOther.Value)
            If hdApplyWithholdingFlagClaimOther.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(hdOtherAmt.Value, Decimal)
                If State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then CheckBoxOtherWithhodling.Checked = True
            End If
        Catch ex As Exception
            txtOtherTax.Text = ""
        End Try

        Try
            txtPaymenttoCustomer.Text = hdpaymenttocustomer.Value.ToString
            txtPaymenttoCustomertax.Text = computeTaxAmtByComputeMethod(CType(txtPaymenttoCustomer.Text, Decimal), 0D, 0)
            If hdApplyWithholdingFlagClaimParts.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(hdpaymenttocustomer.Value, Decimal)
                If State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then CheckBoxOtherWithhodling.Checked = True
            End If
        Catch ex As Exception
            txtPaymenttoCustomer.Text = ""
        End Try

        Dim tmp As String = computeTaxAmtByComputeMethod(tmpAmtSubjectToWithHoding, CType(State.ClaimInvoiceBO.ServiceCenterWithholdingRate, Decimal), "N", True)

        txtTotalTaxAmount.Text = Math.Round(State.ClaimInvoiceBO.TotalTaxAmount.Value, 2).ToString
        txtTotalWithholdingAmount.Text = Math.Round(State.ClaimInvoiceBO.WithholdingAmount.Value, 2).ToString


        Try
            txtGrandTotal.Text = ((Math.Round(CType(txtTotal.Text, Decimal), 2)) + Math.Round(State.ClaimInvoiceBO.WithholdingAmount.Value, 2)).ToString
            hdTotalWithholdings.Value = State.ClaimInvoiceBO.WithholdingAmount.Value
        Catch ex As Exception
            txtGrandTotal.Text = ""
        End Try


    End Sub

    Private Function computeTaxAmtByComputeMethod(amount As DecimalType, TaxRate As DecimalType, ComputeMethodCode As String, Optional blnWithhodling As Boolean = False) As String
        Dim taxAmount As Decimal = 0

        If amount.Value > 0 Then
            If ComputeMethodCode.ToUpper = "N" Then
                taxAmount = Math.Round(((getDecimalValue(amount) * getDecimalValue(TaxRate)) / 100), 2)

            ElseIf ComputeMethodCode.ToUpper = "G" Then
                Dim gross As Decimal
                gross = Math.Round((getDecimalValue(amount) / (1.0 + getDecimalValue(TaxRate))), 2)
                taxAmount = Math.Round(((getDecimalValue(amount) / gross) - 1), 2)
            End If
        End If

        If blnWithhodling Then
            State.ClaimInvoiceBO.WithholdingAmount += taxAmount
        Else
            State.ClaimInvoiceBO.TotalTaxAmount += taxAmount
        End If


        Return taxAmount.ToString
    End Function
    Private Function getDecimalValue(decimalObj As DecimalType, Optional ByVal decimalDigit As Integer = 2) As Decimal
        If decimalObj Is Nothing Then
            Return 0D
        Else
            Return Math.Round(decimalObj.Value, decimalDigit, MidpointRounding.AwayFromZero)
        End If
    End Function

    Private Function isForClaimPayAdjustments() As Boolean
        Return State.isForClaimPayAdjust
    End Function

    Private Function CalculateSubTotal() As DecimalType
        Dim retVal As Decimal = 0D
        Dim laborAmount As Decimal = 0D
        Dim partAmt As Decimal = 0D
        Dim svcCharge As Decimal = 0D
        Dim tripAmt As Decimal = 0D
        Dim otherAmt As Decimal = 0D
        Dim shippingAmt As Decimal = 0D
        Dim dispositionAmt As Decimal = 0D
        Dim diagnosticsAmt As Decimal = 0D
        Dim salvageAmt As Decimal = 0D
        Dim Paytocustomer As Decimal = 0D
        'Dim perIva As Decimal = 0D
        'Dim perIIBB As Decimal = 0D
        ' Me.State.ClaimInvoiceBO.Invoiceable.SalvageAmount 


        If State.ClaimInvoiceBO.LaborAmt IsNot Nothing Then
            laborAmount = State.ClaimInvoiceBO.LaborAmt.Value
        End If
        If State.ClaimInvoiceBO.PartAmount IsNot Nothing Then
            partAmt = State.ClaimInvoiceBO.PartAmount.Value
        End If
        If State.ClaimInvoiceBO.ServiceCharge IsNot Nothing Then
            svcCharge = State.ClaimInvoiceBO.ServiceCharge.Value
        End If
        If State.ClaimInvoiceBO.TripAmount IsNot Nothing Then
            tripAmt = State.ClaimInvoiceBO.TripAmount.Value
        End If
        If State.ClaimInvoiceBO.ShippingAmount IsNot Nothing Then
            shippingAmt = State.ClaimInvoiceBO.ShippingAmount.Value
        End If
        If State.ClaimInvoiceBO.OtherAmount IsNot Nothing Then
            otherAmt = State.ClaimInvoiceBO.OtherAmount.Value
        End If
        If State.ClaimInvoiceBO.DispositionAmount IsNot Nothing Then
            dispositionAmt = State.ClaimInvoiceBO.DispositionAmount.Value
        End If
        If State.ClaimInvoiceBO.DiagnosticsAmount IsNot Nothing Then
            diagnosticsAmt = State.ClaimInvoiceBO.DiagnosticsAmount.Value
        End If
        If State.ClaimInvoiceBO.Invoiceable.SalvageAmount IsNot Nothing Then
            salvageAmt = State.ClaimInvoiceBO.Invoiceable.SalvageAmount.Value
        End If
        If State.ClaimInvoiceBO.PaytocustomerAmount IsNot Nothing Then
            Paytocustomer = State.ClaimInvoiceBO.PaytocustomerAmount.Value
        End If

        With State.ClaimInvoiceBO
            retVal = laborAmount + partAmt + svcCharge + tripAmt + otherAmt + shippingAmt + dispositionAmt + diagnosticsAmt
            If State.ClaimInvoiceBO.IsSalvagePayment Then
                retVal = retVal - salvageAmt
            End If
        End With
        Return New DecimalType(retVal)
    End Function

    Private Function CalculateLiabilityLimit() As DecimalType

        Dim al As ArrayList = State.ClaimBO.CalculateLiabilityLimit(State.ClaimBO.CertificateId, State.ClaimBO.Contract.Id, State.ClaimBO.CertItemCoverageId, CType(State.ClaimBO.LossDate.ToString, DateType))
        Dim liabLimit As Decimal = CType(al(0), Decimal)

        Return New DecimalType(liabLimit)
    End Function

    Private Function AboveLiability() As DecimalType

        Dim abovLiability As Decimal = 0D
        Dim liabLimit As Decimal = CDec(CalculateLiabilityLimit())
        Dim Authorizedamount As Decimal = CDec(State.ClaimInvoiceBO.Invoiceable.AuthorizedAmount)

        If (liabLimit = 0D And (CType(State.ClaimBO.Certificate.ProductLiabilityLimit, Decimal) = 0 And CType(State.ClaimBO.CertificateItemCoverage.CoverageLiabilityLimit, Decimal) = 0)) Then
            liabLimit = CDec(999999999.99)
        End If

        If (Authorizedamount > liabLimit) Then
            abovLiability = Authorizedamount - liabLimit
        End If
        Return New DecimalType(abovLiability)
    End Function

    Protected Sub PopulateBOsFromForm(Optional ByVal blnExcludeSaveUserControls As Boolean = False, Optional ByVal blnComingFromSave As Boolean = False)
        Dim OtherAmt As Decimal = 0D

        If State.ViewOnly Then Return
        If blnComingFromSave Then
            Dim TotalVal As Decimal = 0
            Dim xx As String

            If hdTotal IsNot Nothing AndAlso hdTotal.Value <> "" Then
                'TotalVal = CType(hdTotal.Value, Decimal) 'DEF-17426
                'TotalVal = CType(hdTotal.Value.Replace(",", ""), Decimal)
                'DEF-22413-START
                'TotalVal = Decimal.Parse(hdTotal.Value.Trim, System.Globalization.NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture)
                TotalVal = Decimal.Parse(hdTotal.Value.Trim.Replace(ElitaPlusPage.GetGroupSeperator(System.Threading.Thread.CurrentThread.CurrentCulture.ToString()), ""), System.Globalization.NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture)
                'DEF-22413-END
            End If

            If hdOtherAmt IsNot Nothing AndAlso hdOtherAmt.Value <> "" Then
                'OtherAmt = CType(hdOtherAmt.Value, Decimal) 'DEF-17426
                'OtherAmt = CType(hdOtherAmt.Value.Replace(",", ""), Decimal)
                'DEF-22413-START
                'OtherAmt = Decimal.Parse(hdOtherAmt.Value.Trim, System.Globalization.NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture)
                OtherAmt = Decimal.Parse(hdOtherAmt.Value.Trim.Replace(ElitaPlusPage.GetGroupSeperator(System.Threading.Thread.CurrentThread.CurrentCulture.ToString()), ""), System.Globalization.NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture)
                'DEF-22413-END
            End If

            PopulateBOProperty(State.ClaimInvoiceBO, "Amount", TotalVal.ToString)
            Session("TotalPaidAmount") = State.ClaimInvoiceBO.Amount.Value
            PopulateBOProperty(State.ClaimInvoiceBO, "LaborAmt", txtLabor)
            PopulateBOProperty(State.ClaimInvoiceBO, "PartAmount", txtParts)
            PopulateBOProperty(State.ClaimInvoiceBO, "ServiceCharge", txtServiceCharge)
            PopulateBOProperty(State.ClaimInvoiceBO, "TripAmount", txtTripAmt)
            PopulateBOProperty(State.ClaimInvoiceBO, "ShippingAmount", txtShipping)
            PopulateBOProperty(State.ClaimInvoiceBO, "DiagnosticsAmount", txtDiagnostics)
            PopulateBOProperty(State.ClaimInvoiceBO, "DispositionAmount", txtDisposition)
            PopulateBOProperty(State.ClaimInvoiceBO, "OtherAmount", OtherAmt.ToString)
            PopulateBOProperty(State.ClaimInvoiceBO, "WithholdingAmount", txtTotalWithholdingAmount)
            PopulateBOProperty(State.ClaimInvoiceBO, "TotalTaxAmount", txtTotalTaxAmount)
            PopulateBOProperty(State.ClaimInvoiceBO, "OtherExplanation", txtOtherDesc)
            PopulateBOProperty(State.ClaimInvoiceBO, "IvaAmount", txtIvaTax)
            PopulateBOProperty(State.ClaimInvoiceBO, "PerceptionIIBB", txtPerceptionIIBB)
            PopulateBOProperty(State.ClaimInvoiceBO, "PerceptionIVA", txtPerceptionIva)
            PopulateBOProperty(State.ClaimInvoiceBO, "WithholdingAmount", txtTotalWithholdingAmount)
            PopulateBOProperty(State.ClaimInvoiceBO, "PaytocustomerAmount", txtPaymenttoCustomer)

            Dim DeductibleAmount As Double
            Dim DeductibleTaxAmount As Double

            Double.TryParse(txtDeductibleAmount.Text, DeductibleAmount)
            Double.TryParse(txtDeductibleTaxAmount.Text, DeductibleTaxAmount)

            If (oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.AUTH_LESS_DEDUCT_Y)) Then

                'Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "DeductibleAmount", (Convert.ToDouble(Me.txtDeductibleAmount.Text) + Convert.ToDouble(Me.txtDeductibleTaxAmount.Text)).ToString)
                PopulateBOProperty(State.ClaimInvoiceBO, "DeductibleAmount", (DeductibleAmount + DeductibleTaxAmount).ToString)
                If (State.ClaimInvoiceBO.IsIvaResponsibleFlag) Then
                    PopulateBOProperty(State.ClaimInvoiceBO, "DeductibleTaxAmount", txtDeductibleTaxAmount)
                End If
                State.ClaimInvoiceBO.Amount = New DecimalType(State.ClaimInvoiceBO.Amount.Value - State.ClaimInvoiceBO.DeductibleAmount.Value)
            End If
            PopulateBOProperty(State.ClaimInvoiceBO.Invoiceable, "SalvageAmount", txtSalvageAmt)
            'Me.State.ClaimInvoiceBO.Invoiceable.SalvageAmount = Decimal.Parse(Me.txtSalvageAmt.Text)

        End If
        Dim objCountry As Country = New Country(State.ClaimBO.Certificate.Company.CountryId)
        State.validateBankInfoCountry = LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId)

        PopulateBOProperty(State.ClaimInvoiceBO.Invoiceable, "Deductible", txtDeductible)

        PopulateBOProperty(State.DisbursementBO, "Payee", txtPayeeName)
        PopulateBOProperty(State.ClaimInvoiceBO, "SvcControlNumber", txtInvoiceNumber)

        PopulateBOProperty(State.ClaimInvoiceBO, "RepairDate", txtRepairDate)
        PopulateBOProperty(State.ClaimInvoiceBO, "InvoiceDate", txtInvoiceDate)
        'If Me.txtInvoiceDate.Text.Trim <> String.Empty Then
        '    Me.PopulateBOProperty(Me.State.DisbursementBO, "InvoiceDate", Me.txtInvoiceDate)
        'End If
        PopulateBOProperty(State.DisbursementBO, "StatusDate", txtAcctStatusDate) ' for felita passback
        PopulateBOProperty(State.ClaimInvoiceBO, "LoanerReturnedDate", txtLoanerReturnedDate)

        PopulateBOProperty(State.ClaimInvoiceBO, "RepairCodeId", cboRepairCode)
        PopulateBOProperty(State.ClaimInvoiceBO, "CauseOfLossID", cboCauseOfLossID)
        PopulateBOProperty(State.ClaimInvoiceBO, "RegionId", cboRegionDropID)

        'SerialNumber Maintenance
        PopulateBOProperty(State.ClaimInvoiceBO.CurrentCertItem, "SerialNumber", TextSerialNumber)
        PopulateBOProperty(State.ClaimInvoiceBO, "SerialNumberTempContainer", TextSerialNumber)

        'Payee info
        PopulateBOProperty(State.DisbursementBO, "PayeeOptionId", cboPayeeSelector)
        PopulateBOProperty(State.ClaimInvoiceBO, "PaymentMethodID", PaymentMethodDrop)

        PopulateBOProperty(State.DisbursementBO, "Payee", txtPayeeName)

        If Not GetSelectedItem(cboPayeeSelector).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, ClaimInvoice.PAYEE_OPTION_OTHER)) Then
            ChangeEnabledProperty(txtPayeeName, False)
        End If

        'Pickup date: do not display if replacement and/or interface claim
        If State.ClaimBO IsNot Nothing AndAlso State.ClaimBO.CanDisplayVisitAndPickUpDates And Not State.ClaimBO.LoanerTaken And Not State.isForClaimPayAdjust Then
            PopulateBOProperty(State.ClaimInvoiceBO, "PickUpDate", TextboxPickupDate)
            PopulateBOProperty(State.ClaimBO, "PickUpDate", TextboxPickupDate)
        ElseIf State.ClaimBO.LoanerTaken And Not State.isForClaimPayAdjust Then
            State.ClaimInvoiceBO.Invoiceable.SetPickUpDateFromLoanerReturnedDate()
            State.ClaimInvoiceBO.PickUpDate = State.ClaimInvoiceBO.LoanerReturnedDate
        End If

        'DEF 1430 - Subtract salvage amount from subtotal for detail invoice method and first payment on claim  ' (Me.State.ClaimInvoiceBO.AlreadyPaid.Value = 0D) 
        If oCompany.InvoiceMethodId.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetInvoiceMethodLookupList(), "1")) AndAlso
            (Not String.IsNullOrEmpty(hdSalvageAmt.Value) AndAlso Convert.ToDouble(hdSalvageAmt.Value) > 0D) AndAlso
            (State.ClaimInvoiceBO.Invoiceable.SalvageAmount.Value <= State.ClaimInvoiceBO.Amount.Value) Then
            State.ClaimInvoiceBO.IsSalvagePayment= True
        End If

                CapturePayeeInfo(GetSelectedItem(cboPayeeSelector), GetSelectedItem(PaymentMethodDrop), blnExcludeSaveUserControls)

        PopulateManualClaimTaxes()

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub


    Private Sub PopulateManualClaimTaxes()
        Dim strTax1Desc As String = String.Empty
        Dim strTax2Desc As String = String.Empty
        Dim strTax3Desc As String = String.Empty
        Dim strTax4Desc As String = String.Empty
        Dim strTax5Desc As String = String.Empty
        Dim strTax6Desc As String = String.Empty
        Dim dTax1Amt As DecimalType = 0
        Dim dTax2Amt As DecimalType = 0
        Dim dTax3Amt As DecimalType = 0
        Dim dTax4Amt As DecimalType = 0
        Dim dTax5Amt As DecimalType = 0
        Dim dTax6Amt As DecimalType = 0

        If State.HasManualTaxes Then
            For Each obj As PayClaimManualTaxForm.ManualTaxDetail In State.ManualTaxes
                Select Case obj.Position
                    Case 1
                        strTax1Desc = obj.Description
                        dTax1Amt = New DecimalType(obj.Amount)
                    Case 2
                        strTax2Desc = obj.Description
                        dTax2Amt = New DecimalType(obj.Amount)
                    Case 3
                        strTax3Desc = obj.Description
                        dTax3Amt = New DecimalType(obj.Amount)
                    Case 4
                        strTax4Desc = obj.Description
                        dTax4Amt = New DecimalType(obj.Amount)
                    Case 5
                        strTax5Desc = obj.Description
                        dTax5Amt = New DecimalType(obj.Amount)
                    Case 6
                        strTax6Desc = obj.Description
                        dTax6Amt = New DecimalType(obj.Amount)
                End Select
            Next

            If State.TotalManualTaxes <> 0 Then
                State.ClaimInvoiceBO.CreateManualClaimTaxes(strTax1Desc, dTax1Amt, strTax2Desc, dTax2Amt, strTax3Desc, dTax3Amt,
                                                            strTax4Desc, dTax4Amt, strTax5Desc, dTax5Amt, strTax6Desc, dTax6Amt)
            End If
        End If
    End Sub

    Public Sub CapturePayeeInfo(selectedPayee As Guid, selectedPaymentMethod As Guid, Optional ByVal blnExcludeSaveUserControls As Boolean = False)
        Dim PayeeOptionCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, selectedPayee)
        Dim PaymentMethodCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, selectedPaymentMethod)
        State.ClaimInvoiceBO.PayeeOptionCode = PayeeOptionCode
        State.ClaimInvoiceBO.PaymentMethodCode = PaymentMethodCode

        Select Case PayeeOptionCode
            Case ClaimInvoice.PAYEE_OPTION_MASTER_CENTER
                ServiceCenterPopulateBOFromFormLogic()
            Case ClaimInvoice.PAYEE_OPTION_SERVICE_CENTER
                ServiceCenterPopulateBOFromFormLogic()
            Case ClaimInvoice.PAYEE_OPTION_LOANER_CENTER
                ServiceCenterPopulateBOFromFormLogic()
            Case ClaimInvoice.PAYEE_OPTION_CUSTOMER
                CustomerPopulateBOFromFormLogic(PaymentMethodCode, blnExcludeSaveUserControls)
            Case ClaimInvoice.PAYEE_OPTION_OTHER
                OtherPopulateBOFromFormLogic(PaymentMethodCode, blnExcludeSaveUserControls)
        End Select

    End Sub

    Private Sub ServiceCenterPopulateBOFromFormLogic()
        Dim oAccountType As String
        If State.PayeeBankInfo Is Nothing Then
            PayeeAddress.PopulateBOFromControl()
            State.PayeeAddress = PayeeAddress.MyBO
            State.ClaimInvoiceBO.PayeeAddress = State.PayeeAddress
        Else
            PayeeBankInfo.PopulateBOFromControl()
            State.PayeeBankInfo = PayeeBankInfo.State.myBankInfoBo
            State.ClaimInvoiceBO.PayeeBankInfo = State.PayeeBankInfo
            If Not (PayeeBankInfo.State.myBankInfoBo.AccountTypeId.Equals(Guid.Empty)) Then
                oAccountType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_ACCOUNT_TYPES, PayeeBankInfo.State.myBankInfoBo.AccountTypeId)
                PopulateBOProperty(State.DisbursementBO, "AccountType", oAccountType)
            End If
        End If
    End Sub

    Private Sub ValidateAddressControl()
        If txtPayeeName.Text = "" Then
            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PAYEE_NAME)
        End If
        With State.PayeeAddress
            If .Address1 Is Nothing Then
                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_ADDRESS)
            End If
            If .City Is Nothing Then
                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CITY_MUST_BE_ENTERED_ERR)
            End If
            If .RegionId.Equals(Guid.Empty) Then
                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_STATE)
            End If
            If .CountryId.Equals(Guid.Empty) Then
                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COUNTRY)
            End If
            If .PostalCode Is Nothing Then
                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_ZIP)
            End If
        End With
    End Sub

    Private Sub ValidateBankUserControl()
        If State.PayeeBankInfo IsNot Nothing Then
            If State.PayeeBankInfo.DomesticTransfer = True Then
                If Not State.validateBankInfoCountry = Codes.Country_Code_France Then
                    If State.PayeeBankInfo.Account_Name Is Nothing Then
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKACCNAME_REQD)
                    End If

                    If State.PayeeBankInfo.Bank_Id Is Nothing Then
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKID_REQD)
                    End If

                    If State.PayeeBankInfo.Account_Number Is Nothing Then
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKACCNO_REQD)
                    End If
                Else
                    ValidateBankInfoForCountry(State.validateBankInfoCountry)
                End If
            End If

            If State.PayeeBankInfo.InternationalEUTransfer = True Then
                If State.PayeeBankInfo.Account_Name Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKACCNAME_REQD)
                End If

                If State.PayeeBankInfo.SwiftCode Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKSWIFTCODE_REQD)
                End If

                If State.PayeeBankInfo.IbanNumber Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKIBANNO_REQD)
                End If
            End If

            If State.PayeeBankInfo.InternationalTransfer = True Then
                If State.PayeeBankInfo.Account_Name Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKACCNAME_REQD)
                End If

                If State.PayeeBankInfo.Bank_Id Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKID_REQD)
                End If

                If State.PayeeBankInfo.Account_Number Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKACCNO_REQD)
                End If

                If State.PayeeBankInfo.SwiftCode Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKSWIFTCODE_REQD)
                End If
            End If

        End If
    End Sub

    Private Sub ValidateBankInfoForCountry(code As String)
        If (code = Codes.Country_Code_France And
                    LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, GetSelectedItem(cboPayeeSelector)) = ClaimInvoice.PAYEE_OPTION_CUSTOMER And
                    LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, GetSelectedItem(PaymentMethodDrop)) = Codes.PAYMENT_METHOD__BANK_TRANSFER) Then
            If State.PayeeBankInfo.IbanNumber Is Nothing Then
                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKIBANNO_REQD)
            End If
            With State.PayeeAddress
                If .Address1 Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_ADDRESS)
                End If
                If .City Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CITY_MUST_BE_ENTERED_ERR)
                End If
                'If .RegionId.Equals(Guid.Empty) Then
                '    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_STATE)
                'End If
                If State.ClaimBO.Certificate.Company.CountryId.Equals(Guid.Empty) Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COUNTRY)
                End If
                If .PostalCode Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_ZIP)
                End If
            End With
        End If
    End Sub

    Private Sub CustomerPopulateBOFromFormLogic(PaymentMethodCode As String, Optional ByVal blnExcludeSaveUserControls As Boolean = False)
        Dim oAccountType As String
        Select Case PaymentMethodCode
            Case Codes.PAYMENT_METHOD__BANK_TRANSFER
                State.PayeeBankInfo.PaymentMethodId = GetSelectedItem(PaymentMethodDrop)
                State.PayeeBankInfo.PayeeId = GetSelectedItem(cboPayeeSelector)
                PayeeBankInfo.PopulateBOFromControl(blnExcludeSaveUserControls)
                State.ClaimInvoiceBO.PayeeBankInfo = PayeeBankInfo.State.myBankInfoBo
                State.PayeeBankInfo = PayeeBankInfo.State.myBankInfoBo
                State.PayeeAddress = PayeeBankInfo.State.payeeAddress
                If Not (PayeeBankInfo.State.myBankInfoBo.AccountTypeId.Equals(Guid.Empty)) Then
                    oAccountType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_ACCOUNT_TYPES, PayeeBankInfo.State.myBankInfoBo.AccountTypeId)
                    PopulateBOProperty(State.DisbursementBO, "AccountType", oAccountType)
                End If
                'Req-6171...Darty Claim Reimbursement logic
                If State.validateBankInfoCountry = Codes.Country_Code_France Then
                    State.PayeeAddress = PayeeBankInfo.PopulateAddressInfo()
                    State.PayeeAddress.PaymentMethodId = GetSelectedItem(PaymentMethodDrop)
                    State.PayeeAddress.PayeeId = GetSelectedItem(cboPayeeSelector)
                    PopulateBOProperty(State.DisbursementBO, "Address1", State.PayeeAddress.Address1)
                    PopulateBOProperty(State.DisbursementBO, "Address2", State.PayeeAddress.Address2)
                    PopulateBOProperty(State.DisbursementBO, "City", State.PayeeAddress.City)
                    PopulateBOProperty(State.DisbursementBO, "Zip", State.PayeeAddress.PostalCode)
                    State.PayeeAddress.Validate()
                    If ErrCollection.Count > 0 Then
                        Throw New PopulateBOErrorException
                    End If
                End If
            Case Codes.PAYMENT_METHOD__ADMIN_CHECK
                State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                State.ClaimInvoiceBO.PayeeAddress = Nothing
            Case Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER
                State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                PayeeAddress.PopulateBOFromControl(True)
                State.ClaimInvoiceBO.PayeeAddress = PayeeAddress.MyBO
                State.PayeeAddress = PayeeAddress.MyBO
            Case Codes.PAYMENT_METHOD__DARTY_GIFT_CARD
                State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                State.ClaimInvoiceBO.PayeeAddress = Nothing
        End Select
    End Sub

    Private Sub OtherPopulateBOFromFormLogic(PaymentMethodCode As String, Optional ByVal blnExcludeSaveUserControls As Boolean = False)
        Dim oAccountType As String
        If State.ClaimInvoiceBO.IsInsuranceCompany Then
            State.ClaimInvoiceBO.DocumentType = LookupListNew.GetCodeFromId(LookupListNew.LK_DOCUMENT_TYPES, GetSelectedItem(cboDocumentTypeId))
            PopulateBOProperty(State.ClaimInvoiceBO, "TaxId", moTaxIdText)
        End If
        Select Case PaymentMethodCode
            Case Codes.PAYMENT_METHOD__BANK_TRANSFER
                PayeeBankInfo.PopulateBOFromControl(blnExcludeSaveUserControls)
                State.ClaimInvoiceBO.PayeeBankInfo = PayeeBankInfo.State.myBankInfoBo
                State.PayeeBankInfo = PayeeBankInfo.State.myBankInfoBo
                If Not (PayeeBankInfo.State.myBankInfoBo.AccountTypeId.Equals(Guid.Empty)) Then
                    oAccountType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_ACCOUNT_TYPES, PayeeBankInfo.State.myBankInfoBo.AccountTypeId)
                    PopulateBOProperty(State.DisbursementBO, "AccountType", oAccountType)
                End If
            Case Codes.PAYMENT_METHOD__ADMIN_CHECK
                State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                State.ClaimInvoiceBO.PayeeAddress = Nothing
            Case Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER
                State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                PayeeAddress.PopulateBOFromControl()
                State.ClaimInvoiceBO.PayeeAddress = PayeeAddress.MyBO
                State.PayeeAddress = PayeeAddress.MyBO
        End Select
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        Try
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_OK Then
                RefreshScreen()
                CleanConsumedActions()
            ElseIf confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_CANCEL Then
                Throw New Exception("Invalid Event")
                CleanConsumedActions()
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If CheckStatusAndSaveClaimInvoice() Then
                            If NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                                NavController.Navigate(Me, "back")
                            Else
                                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimInvoiceBO, State.ChangesMade))
                            End If
                            CleanConsumedActions()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.ClaimInvoiceBO, State.ChangesMade))
                        CleanConsumedActions()
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        State.ClaimInvoiceBO.CloseClaim = True
                        If SaveClaimInvoice() Then
                            If NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                                NavController.Navigate(Me, "back")
                            Else
                                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimInvoiceBO, State.ChangesMade))
                            End If
                        End If
                        CleanConsumedActions()
                    Case ElitaPlusPage.DetailPageCommand.OK
                        State.ClaimInvoiceBO.CloseClaim = True
                        If SaveClaimInvoice() Then
                            Dim claim As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
                            claim.Handle_Replaced_Items(1, State.ClaimBO.Id, State.ClaimBO.CertificateId,
                                    State.ClaimBO.CertItemCoverageId, DateHelper.GetDateValue(txtRepairDate.Text))
                            If NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                                NavController.Navigate(Me, "back")
                            Else
                                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimInvoiceBO, State.ChangesMade))
                            End If
                        End If
                        CleanConsumedActions()
                    Case Else
                        CheckStatusAndSaveClaimInvoice()
                        CleanConsumedActions()
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        State.ClaimInvoiceBO.cancelEdit()
                        If NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                            NavController.Navigate(Me, "back")
                        Else
                            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimInvoiceBO, State.ChangesMade))
                        End If
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        State.ClaimInvoiceBO.CloseClaim = False
                        If SaveClaimInvoice() Then
                            If NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                                NavController.Navigate(Me, "back")
                            Else
                                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimInvoiceBO, State.ChangesMade))
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.OK
                        If SaveClaimInvoice() Then
                            If NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                                NavController.Navigate(Me, "back")
                            Else
                                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimInvoiceBO, State.ChangesMade))
                            End If
                        End If
                End Select
                CleanConsumedActions()
                'in this case, we need to exit after we show the message
                'so we dont care about user's response
            ElseIf Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK Then
                If NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                    NavController.Navigate(Me, "back")
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimInvoiceBO, State.ChangesMade))
                End If
                CleanConsumedActions()
            End If

            'Clean after consuming the action
            'Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            'Me.HiddenSaveChangesPromptResponse.Value = ""
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CleanConsumedActions()
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub CheckifComingFromNewClaimBackEndClaim()
        If NavController Is Nothing Then
            Exit Sub
        End If
        If NavController.ParametersPassed Is Nothing Then
            Exit Sub
        End If
        If NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Then
            Dim params As Parameters = CType(NavController.ParametersPassed, Parameters)
            If params.PayClaimID.Equals(Guid.Empty) Then
                State.ClaimBO = params.ClaimBO
                'coming from payclaim , pass claimbo to prepopulate data
                State.ClaimInvoiceBO = New ClaimInvoice
                State.DisbursementBO = State.ClaimInvoiceBO.AddNewDisbursement()
                State.ClaimInvoiceBO.PrepopulateClaimInvoice(State.ClaimBO)
                State.ClaimInvoiceBO.PrepopulateDisbursment()

                State.isForClaimPayAdjust = params.isForClaimPayAdjust
                State.ClaimInvoiceBO.IsNewPaymentFromPaymentAdjustment = State.isForClaimPayAdjust
                'default values
                State.ClaimInvoiceBO.BatchNumber = "1"
                State.ClaimInvoiceBO.LaborTax = New DecimalType(0D)
                State.ClaimInvoiceBO.PartTax = New DecimalType(0D)
                State.ClaimInvoiceBO.RecordCount = New LongType(1)
                State.ClaimInvoiceBO.Source = Nothing
                State.ClaimInvoiceBO.BeginEdit()
            End If
            State.ViewOnly = params.ViewOnly
        End If
    End Sub

    Private Sub CheckifComingFromClaimAdjustment()
        If NavController Is Nothing Then
            Exit Sub
        End If
        If NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
            Dim params As Parameters = CType(NavController.ParametersPassed, Parameters)
            If params.PayClaimID.Equals(Guid.Empty) Then
                State.ClaimBO = params.ClaimBO
                'coming from payclaim , pass claimbo to prepopulate data
                State.ClaimInvoiceBO = New ClaimInvoice
                State.DisbursementBO = State.ClaimInvoiceBO.AddNewDisbursement()
                State.ClaimInvoiceBO.PrepopulateClaimInvoice(State.ClaimBO)
                State.ClaimInvoiceBO.PrepopulateDisbursment()

                State.isForClaimPayAdjust = params.isForClaimPayAdjust
                State.ClaimInvoiceBO.IsNewPaymentFromPaymentAdjustment = State.isForClaimPayAdjust
                'default values
                State.ClaimInvoiceBO.BatchNumber = "1"
                State.ClaimInvoiceBO.LaborTax = New DecimalType(0D)
                State.ClaimInvoiceBO.PartTax = New DecimalType(0D)
                State.ClaimInvoiceBO.RecordCount = New LongType(1)
                State.ClaimInvoiceBO.Source = Nothing
                State.ClaimInvoiceBO.BeginEdit()
            End If
            State.ViewOnly = params.ViewOnly
        End If
    End Sub

    Private Sub RefreshScreen()
        Try
            ReloadClaimAndOtherAssociatedBOs()
            MenuEnabled = False

            'Pickup date: do not display if replacement and/or interface claim
            'Interface claim = Not claim.Source.Equals(String.Empty)
            If State.ClaimBO IsNot Nothing AndAlso State.ClaimBO.CanDisplayVisitAndPickUpDates And Not State.isForClaimPayAdjust Then
                If Not State.ClaimBO.LoanerTaken Then AddCalendar(ImageButtonPickupDate, TextboxPickupDate)
            End If

            If State.ClaimInvoiceBO Is Nothing Then
                State.ClaimInvoiceBO = New ClaimInvoice
            End If

            PopulateFormFromBOs()

            EnableDisableFields()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub ReloadClaimAndOtherAssociatedBOs()
        Try
            If CallingParameters IsNot Nothing Then
                Dim params As Parameters = CType(CallingParameters, Parameters)
                params.ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(params.ClaimBO.Id)
                params.PayClaimID = Guid.Empty
                params.ClaimID = params.ClaimBO.Id
                State.ClaimBO = params.ClaimBO
                State.ClaimInvoiceBO = New ClaimInvoice
                State.DisbursementBO = State.ClaimInvoiceBO.AddNewDisbursement()
                State.ClaimInvoiceBO.PrepopulateClaimInvoice(State.ClaimBO)
                State.ClaimInvoiceBO.PrepopulateDisbursment()

                State.isForClaimPayAdjust = params.isForClaimPayAdjust
                State.ClaimInvoiceBO.IsNewPaymentFromPaymentAdjustment = State.isForClaimPayAdjust
                'default values
                State.ClaimInvoiceBO.BatchNumber = "1"
                State.ClaimInvoiceBO.LaborTax = New DecimalType(0D)
                State.ClaimInvoiceBO.PartTax = New DecimalType(0D)
                State.ClaimInvoiceBO.RecordCount = New LongType(1)
                State.ClaimInvoiceBO.Source = Nothing
                State.ClaimInvoiceBO.BeginEdit()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)

    End Sub
#End Region

#Region "Button_handlers"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFromForm(False, False)
            If State.isClaimSystemMaintAllowed AndAlso (Not State.ViewOnly) AndAlso State.ClaimInvoiceBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                If NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                    NavController.Navigate(Me, "back")
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimInvoiceBO, State.ChangesMade))
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try

    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            State.ClaimInvoiceBO.RefreshCurrentClaim()
            CheckStatusAndSaveClaimInvoice()
            'Req-6171 - Darty gift card (claim reimbursement)
            'GetGiftCardInfo()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Function CheckStatusAndSaveClaimInvoice() As Boolean
        Try
            Dim isPendingReplacement As Boolean = State.ClaimInvoiceBO.Invoiceable.ClaimActivityId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT))
            Dim claimServiceCenter As ServiceCenter = New ServiceCenter(State.ClaimInvoiceBO.Invoiceable.ServiceCenterId)
            Dim PayeeOptionCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, GetSelectedItem(cboPayeeSelector))

            If claimServiceCenter.PayMaster = False And PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_MASTER_CENTER Then
                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_PAY_MASTER_CENTER)
            End If
            If (Not IsGiftCardValid()) Then
                Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_GIFT_CARD_TYPE)
            End If
            PopulateBOsFromForm(True, True)

            If Not State.ClaimInvoiceBO.PaymentMethodID.Equals(Guid.Empty) Then
                Dim PaymentMethodCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, GetSelectedItem(PaymentMethodDrop))
                If PaymentMethodCode = Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER Then
                    'def - 651
                    If State.ClaimInvoiceBO.PayeeBankInfo Is Nothing Then
                        ValidateAddressControl()
                    End If
                End If
            End If

            'REQ-786
            If Not State.ClaimInvoiceBO.RepairCodeId.Equals(Guid.Empty) Then
                If State.ClaimInvoiceBO.RepairCodeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetRepairCodeLookupList(oCompany.CompanyGroupId), Codes.REPAIR_CODE__DELIVERY_FEE_NO_DEDUCTIBLE)) Then
                    State.ClaimInvoiceBO.Invoiceable.Deductible = New DecimalType(0D)
                End If
            End If

            State.ClaimInvoiceBO.Validate()
            'DEF 2490
            State.ClaimInvoiceBO.CurrentCertItem.Validate()


            If PayeeBankInfo IsNot Nothing Then
                ValidateBankUserControl()
            End If


            If State.ClaimInvoiceBO.IsDirty Then
                If isForClaimPayAdjustments() Then
                    Return ProcessPayClaim(False, isPendingReplacement)
                Else
                    'for repair claim, if the remaining amt > 0 then ask for confirmation or else just close the claim.
                    State.ClaimInvoiceBO.CloseClaim = False
                    If Not isPendingReplacement Then
                        If System.Math.Abs(State.ClaimInvoiceBO.RemainingAmount.Value) > 0 Then
                            'Check with the user...
                            DisplayMessage(Message.MSG_PROMPT_FOR_CLOSING_THE_CLAIM, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                            Return True
                        Else
                            Return ProcessPayClaim(True, isPendingReplacement)
                        End If
                    Else
                        Return ProcessPayClaim(True, isPendingReplacement)
                    End If
                End If
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Return True
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DALConcurrencyAccessException
            DisplayMessage(Message.MSG_ANOTHER_USER_HAS_MODIFIED_THIS_CLAIM_THE_SYSTEM_MUST_REFRESH_THIS_SCREEN, "", MSG_BTN_OK, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Return False
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            Return False
        End Try
    End Function

    Private Function ProcessPayClaim(closeClaim As Boolean, isPendingReplacement As Boolean) As Boolean
        ' By Default CancelPolicy as False
        State.ClaimInvoiceBO.CancelPolicy = False

        If State.ClaimBO.Contract Is Nothing Then
            Throw New GUIException(Message.MSG_TO_DEALER_MUST_HAVE_A_VALID_CONTRACT, Assurant.ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND)
        End If

        If State.ClaimBO.Contract.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCL)) Then
            Dim lngRepPolicyClaimCnt As Long = ReppolicyClaimCount.GetReplacementPolicyClaimCntByClaim(State.ClaimBO.Contract.Id, State.ClaimBO.Id)
            If lngRepPolicyClaimCnt = 1 Then 'if only require 1 replacement, cancel based on current replacement claim
                State.ClaimInvoiceBO.CancelPolicy = True
            Else
                'more than 1 replacement claims required before cancelling the cert
                Dim certBO As Certificate = State.ClaimBO.Certificate
                Dim paidReplacementClaimCnt As Integer
                Dim Claimlist As Certificate.CertificateClaimsDV

                If certBO.StatusCode = "A" Then 'only cancel if certificate is active
                    Claimlist = certBO.ClaimsForCertificate(certBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    paidReplacementClaimCnt = 0
                    If Claimlist.Count > 0 Then
                        Dim i As Integer, dblPayment As Double
                        For i = 0 To Claimlist.Count - 1
                            dblPayment = Claimlist(i)(Certificate.CertificateClaimsDV.COL_TOTAL_PAID)
                            If dblPayment > 0 AndAlso Claimlist(i)(Certificate.CertificateClaimsDV.COL_CLAIM_NUMBER) <> State.ClaimBO.ClaimNumber _
                                AndAlso Claimlist(i)(Certificate.CertificateClaimsDV.COL_Method_Of_Repair_code) = "R" Then
                                'paid replacement claim
                                paidReplacementClaimCnt = paidReplacementClaimCnt + 1
                                If lngRepPolicyClaimCnt - 1 <= paidReplacementClaimCnt Then
                                    State.ClaimInvoiceBO.CancelPolicy = True
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End If

        If isPendingReplacement Then
            Dim claim As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
            If (claim.Handle_Replaced_Items(0, State.ClaimBO.Id, State.ClaimBO.CertificateId,
                    State.ClaimBO.CertItemCoverageId, DateHelper.GetDateValue(txtRepairDate.Text)) = 0) Then
                'If reaminingamount is > 0 then ask for confirmation 
                State.ClaimInvoiceBO.CloseClaim = False
                If System.Math.Abs(State.ClaimInvoiceBO.RemainingAmount.Value) > 0 Then
                    DisplayMessage(Message.MSG_PROMPT_FOR_CLOSING_THE_CLAIM, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                    Return True
                End If
                Return SaveClaimInvoice()
            Else
                DisplayMessage(Message.MSG_PROMPT_FOR_HAVE_ITEM_REPLACED, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                Return True
            End If
        Else
            State.ClaimInvoiceBO.CloseClaim = False
            If System.Math.Abs(State.ClaimInvoiceBO.RemainingAmount.Value) > 0 Then
                DisplayMessage(Message.MSG_PROMPT_FOR_CLOSING_THE_CLAIM, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                Return True
            End If
            Return SaveClaimInvoice()
        End If
    End Function

    Private Function SaveClaimInvoice() As Boolean
        Try
            State.ClaimInvoiceBO.CurrentCertItem.Save()
            State.ClaimInvoiceBO.Save()
            State.ClaimInvoiceBO.EndEdit()
            State.ChangesMade = True
            Dim listItem As ListItem = PaymentMethodDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode(
                                                                      "PMTHD", Codes.PAYMENT_METHOD__DARTY_GIFT_CARD))
            Dim listItem1 As ListItem = PaymentMethodDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode(
                                                                   "PMTHD", Codes.PAYMENT_METHOD__BANK_TRANSFER))
            If (listItem IsNot Nothing) Then
                If (cboPayeeSelector.SelectedItem.Text.ToUpper = Payee_Customer AndAlso PaymentMethodDrop.SelectedItem.Text = listItem.Text) Then
                    If (oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).Count > 0) Then
                        Dim attvalue As AttributeValue = oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).First()
                        If (attvalue.EffectiveDate < DateTime.UtcNow Or attvalue.ExpirationDate > DateTime.UtcNow) Then
                            GetGiftCardInfo() 'Req-6171 - Darty gift card (claim reimbursement)
                        End If
                    End If
                End If
            End If
            If (listItem1 IsNot Nothing) Then
                If (cboPayeeSelector.SelectedItem.Text.ToUpper = Payee_Customer AndAlso PaymentMethodDrop.SelectedItem.Text = listItem1.Text) Then
                    Dim argumentsToAddEvent As String
                    With State.ClaimBO
                        argumentsToAddEvent = "ClaimId:" & DALBase.GuidToSQLString(.Id) & ";ClaimNumber:" & .ClaimNumber & ""
                        PublishedTask.AddEvent(companyGroupId:=Guid.Empty,
                                                   companyId:=Guid.Empty,
                                                   countryId:=Guid.Empty,
                                                   dealerId:= .Dealer.Id,
                                                   productCode:=String.Empty,
                                                   coverageTypeId:=Guid.Empty,
                                                   sender:="Pay Claim",
                                                   arguments:=argumentsToAddEvent,
                                                   eventDate:=DateTime.UtcNow,
                                                   eventTypeId:=LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP_CUST_CLM_REIMBURSE_INFO),
                                                   eventArgumentId:=Nothing)

                    End With
                End If
            End If

            PopulateFormFromBOs()
            EnableDisableFields()
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
            DisplayMessageWithSubmit(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Return True
        Catch ex As ElitaPlusException
            Throw ex
            HandleErrors(ex, MasterPage.MessageController)
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DALConcurrencyAccessException
            DisplayMessage(Message.MSG_ANOTHER_USER_HAS_MODIFIED_THIS_CLAIM_THE_SYSTEM_MUST_REFRESH_THIS_SCREEN, "", MSG_BTN_OK, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Return False
        Catch ex As Exception
            Throw ex
            HandleErrors(ex, MasterPage.MessageController)
            Return False
        End Try
    End Function

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            PopulateControlFromBOProperty(txtGrandTotal, 0.0)
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnTaxes_WRITE_Click(sender As Object, e As EventArgs) Handles btnTaxes_WRITE.Click
        Try
            If State.ClaimBO IsNot Nothing Then
                Dim objParm As New PayClaimManualTaxForm.Parameters
                objParm.ManualTaxList = State.ManualTaxes
                callPage(PayClaimManualTaxForm.URL, objParm)
            End If
        Catch exT As System.Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnPartsInfo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnPartsInfo_WRITE.Click
        Try
            State.PartsInfoViewed = True

            'DEF-394 - ALR - Commenting out check for visibility.  Set the value regardless.
            '   If Me.PaymentMethodDrop.Visible Then
            State.selectedPaymentMethodID = GetSelectedItem(PaymentMethodDrop)
            '  End If

            If State.ClaimBO IsNot Nothing Then
                PopulateBOsFromForm(True, True)
                'Me.State.PartsInfoReplacementClick = True
                If NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Or NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL" Then
                    NavController.Navigate(Me, "parts_info", BuildPartsInfoParameters)
                    'Else
                    '    Me.callPage(PartsInfoForm.URL, Me.State.ClaimBO)
                End If
            End If
        Catch exT As System.Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Function BuildPartsInfoParameters() As PartsInfoForm.Parameters
        Dim claimBO As Claim = State.ClaimBO
        Return New PartsInfoForm.Parameters(claimBO)
    End Function

    'DEF-17426
    Function BuildClaimAuthDetailParameters() As ClaimAuthDetailForm.Parameters
        Dim claimBO As Claim = State.ClaimBO
        Return New ClaimAuthDetailForm.Parameters(claimBO, Nothing, Nothing)
    End Function

    Private Sub btnAuthDetail_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAuthDetail_WRITE.Click
        Try
            If State.ClaimBO IsNot Nothing Then
                'DEF-17426
                PopulateBOsFromForm(True, True)
                State.ClaimBO.AuthDetailUsage = URL
                'Me.callPage(ClaimAuthDetailForm.URL, Me.State.ClaimBO) 'DEF-17426
                NavController.Navigate(Me, "auth_detail", BuildClaimAuthDetailParameters)
            End If
        Catch exT As System.Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnReplacement_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnReplacement_WRITE.Click
        Try
            PopulateBOsFromForm(True, True)
            callPage(Claims.ReplacementForm.URL, New Claims.ReplacementForm.Parameters(False, State.ClaimInvoiceBO.Invoiceable.Claim_Id))
        Catch exT As System.Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub



    Private Sub cboPayeeSelector_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPayeeSelector.SelectedIndexChanged
        Try
            PayeeChanged(GetSelectedItem(cboPayeeSelector), True)
            LoadClaimTaxRates()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PaymentMethodDrop_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles PaymentMethodDrop.SelectedIndexChanged
        Try
            PaymentMethodChanged(GetSelectedItem(PaymentMethodDrop), GetSelectedItem(cboPayeeSelector), True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PaymentMethodChanged(selectedPaymentMethod As Guid, selectedPayee As Guid, blnClearOldBankInfo As Boolean)
        Dim PaymentMethodCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, selectedPaymentMethod)

        ControlMgr.SetVisibleControl(Me, hrSeprator, False)
        SetFieldsEmptyForBankTrf()
        Try
            Select Case PaymentMethodCode
                Case Codes.PAYMENT_METHOD__BANK_TRANSFER
                    BankTransferLogic(blnClearOldBankInfo)
                    PayeeBankInfo.SetRequiredFieldsForDealerWithGiftCard(oDealer)
                    PopulateFieldsForBankTransfer()
                Case Codes.PAYMENT_METHOD__ADMIN_CHECK
                    ControlMgr.SetVisibleControl(Me, PayeeAddress, False)
                    ControlMgr.SetVisibleControl(Me, PayeeBankInfo, False)
                    ControlMgr.SetVisibleControl(Me, hrSeprator, True)
                Case Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER
                    Dim PayeeOptionCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, selectedPayee)
                    ControlMgr.SetVisibleControl(Me, PayeeAddress, True)
                    CheckToConsumerLogic(PayeeOptionCode)
                Case Codes.PAYMENT_METHOD__DARTY_GIFT_CARD
                    ControlMgr.SetVisibleControl(Me, PayeeAddress, False)
                    ControlMgr.SetVisibleControl(Me, PayeeBankInfo, False)
                    ControlMgr.SetVisibleControl(Me, hrSeprator, True)
            End Select

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Private Sub BankTransferLogic(blnClearOldBankInfo As Boolean)

        ControlMgr.SetVisibleControl(Me, PayeeAddress, False)
        ControlMgr.SetVisibleControl(Me, PayeeBankInfo, True)

        ' Me.State.ClaimInvoiceBO.PayeeAddress = Nothing
        If blnClearOldBankInfo Then State.ClaimInvoiceBO.PayeeBankInfo = Nothing
        State.PayeeAddress = Nothing

        If State.ViewOnly Then
            PopulatePayeeBankInfoBO()
            PayeeBankInfo.Bind(State.PayeeBankInfo)
            PayeeBankInfo.ChangeEnabledControlProperty(False)
        Else
            If blnClearOldBankInfo Then State.PayeeBankInfo = State.ClaimInvoiceBO.Add_BankInfo() 'New BankInfo
            State.PayeeBankInfo.SourceCountryID = oCompany.CountryId
            PayeeBankInfo.Bind(State.PayeeBankInfo)
            PayeeBankInfo.ChangeEnabledControlProperty(True)
        End If


        'Me.ChangeEnabledProperty(Me.txtPayeeName, False)
        ControlMgr.SetVisibleControl(Me, txtIvaTax, False)
        txtIvaTax.Text = "0"
        ControlMgr.SetVisibleControl(Me, LabelIvaTax, False)
    End Sub

    Private Sub CheckToConsumerLogic(PayeeOptionCode As String)
        ControlMgr.SetVisibleControl(Me, PayeeAddress, True)
        ControlMgr.SetVisibleControl(Me, PayeeBankInfo, False)
        State.ClaimInvoiceBO.PayeeBankInfo = Nothing
        State.PayeeBankInfo = Nothing

        If PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_CUSTOMER Then
            txtPayeeName.Text = State.ClaimInvoiceBO.CustomerName
            If State.ViewOnly Then
                PopulatePayeeAddressBO()
                PayeeAddress.Bind(State.PayeeAddress)
                PayeeAddress.EnableControls(True)
                PayeeAddress.RegionText = State.DisbursementBO.RegionDesc
            Else
                If State.PayeeAddress Is Nothing Then State.PayeeAddress = State.ClaimInvoiceBO.Add_Address(State.ClaimInvoiceBO.CustomerAddressID) 'New Address(Me.State.ClaimInvoiceBO.CustomerAddressID)
                State.ClaimInvoiceBO.PayeeAddress = State.PayeeAddress
                PayeeAddress.Bind(State.PayeeAddress)
                PayeeAddress.EnableControls(False, True)
            End If


        ElseIf PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_OTHER Then
            'Me.txtPayeeName.Text = Me.State.DisbursementBO.Payee
            If State.ViewOnly Then
                PopulatePayeeAddressBO()
                PayeeAddress.Bind(State.PayeeAddress)
                PayeeAddress.EnableControls(True)
                PayeeAddress.RegionText = State.DisbursementBO.RegionDesc
            Else
                If State.PayeeAddress Is Nothing Then State.PayeeAddress = State.ClaimInvoiceBO.Add_Address() 'New Address
                State.ClaimInvoiceBO.PayeeAddress = State.PayeeAddress
                PayeeAddress.Bind(State.PayeeAddress)
                PayeeAddress.EnableControls(False, True)
            End If

        End If

        'Me.ChangeEnabledProperty(Me.txtPayeeName, False)
        ControlMgr.SetVisibleControl(Me, txtIvaTax, False)
        txtIvaTax.Text = "0"
        ControlMgr.SetVisibleControl(Me, LabelIvaTax, False)
    End Sub


    Private Sub PayeeChanged(selectedPayee As Guid, bNewSelection As Boolean)

        Dim PayeeOptionCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, selectedPayee)

        ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, False)
        ControlMgr.SetVisibleControl(Me, moPaymentMethodLabel, False)
        ControlMgr.SetVisibleControl(Me, Req_Pay_MethodLabel, False)
        SetFieldsEmptyForBankTrf()
        ControlMgr.SetVisibleControl(Me, hrSeprator, False)

        ChangeEnabledProperty(txtPayeeName, False)
        ExcludeFromDropdown(Codes.ATTR_DARTY_GIFT_CARD_TYPE)

        PaymentMethodDrop.SelectedIndex = NOTHING_SELECTED
        PayeeCode.Value = PayeeOptionCode
        Try
            ShowDocumentIDInformation(False)
            Select Case PayeeOptionCode
                Case ClaimInvoice.PAYEE_OPTION_MASTER_CENTER
                    Dim claimServiceCenter As ServiceCenter = New ServiceCenter(State.ClaimInvoiceBO.Invoiceable.ServiceCenterId)
                    If claimServiceCenter.WithholdingRate IsNot Nothing Then State.ClaimInvoiceBO.ServiceCenterWithholdingRate = claimServiceCenter.WithholdingRate.Value * (-1)
                    If Not claimServiceCenter.MasterCenterId.Equals(Guid.Empty) Then
                        Dim masterServiceCenter As ServiceCenter = New ServiceCenter(claimServiceCenter.MasterCenterId)
                        txtPayeeName.Text = masterServiceCenter.Description
                        If masterServiceCenter.WithholdingRate IsNot Nothing Then State.ClaimInvoiceBO.ServiceCenterWithholdingRate = masterServiceCenter.WithholdingRate.Value * (-1)
                        If State.ClaimInvoiceBO.IsInsuranceCompany Then
                            State.ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeOptionCode, masterServiceCenter, State.ClaimInvoiceBO, State.DisbursementBO, Nothing, Nothing, Nothing)
                        End If
                        If Not masterServiceCenter.BankInfoId.Equals(Guid.Empty) Then
                            'Me.State.ClaimInvoiceBO.PayeeBankInfoIDContainer = masterServiceCenter.BankInfoId
                            State.ClaimInvoiceBO.PayeeBankInfo = masterServiceCenter.Add_BankInfo()
                            State.ClaimInvoiceBO.PayeeAddress = Nothing
                            State.PayeeAddress = Nothing
                            ControlMgr.SetVisibleControl(Me, PayeeAddress, False)
                            ControlMgr.SetVisibleControl(Me, PayeeBankInfo, True)
                            State.PayeeBankInfo = State.ClaimInvoiceBO.Add_BankInfo(masterServiceCenter.BankInfoId) 'New BankInfo(masterServiceCenter.BankInfoId)
                            State.PayeeBankInfo.SourceCountryID = oCompany.CountryId
                            PayeeBankInfo.Bind(State.PayeeBankInfo)
                            PayeeBankInfo.ChangeEnabledControlProperty(False)
                        Else
                            ControlMgr.SetVisibleControl(Me, PayeeAddress, True)
                            ControlMgr.SetVisibleControl(Me, PayeeBankInfo, False)
                            State.ClaimInvoiceBO.PayeeAddress = masterServiceCenter.Address
                            State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                            State.PayeeBankInfo = Nothing
                            State.PayeeAddress = State.ClaimInvoiceBO.Add_Address(masterServiceCenter.AddressId) 'New Address(masterServiceCenter.AddressId)
                            PayeeAddress.Bind(State.PayeeAddress)
                            ChangeEnabledProperty(txtPayeeName, False)
                            PayeeAddress.EnableControls(True)
                            ControlMgr.SetVisibleControl(Me, txtIvaTax, masterServiceCenter.IvaResponsibleFlag)
                            ControlMgr.SetVisibleControl(Me, LabelIvaTax, masterServiceCenter.IvaResponsibleFlag)
                            ChangeEnabledProperty(txtIvaTax, (Not State.ViewOnly))
                        End If
                    End If
                    PayeeBankInfo.HideTaxId()

                    ControlMgr.SetVisibleControl(Me, divlblPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, divtxtPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, divtxtPaymenttoCustomertax, False)
                    ControlMgr.SetVisibleControl(Me, divCheckBoxPaymenttocustomer, False)

                    ControlMgr.SetEnableControl(Me, txtLiabilityLimit, True)
                    ControlMgr.SetEnableControl(Me, txtDeductible, True)
                    ControlMgr.SetEnableControl(Me, txtLabor, True)
                    ControlMgr.SetEnableControl(Me, txtLaborTax, True)
                    ControlMgr.SetEnableControl(Me, txtDiscount, True)
                    ControlMgr.SetEnableControl(Me, txtParts, True)
                    ControlMgr.SetEnableControl(Me, txtPartsTax, True)
                    ControlMgr.SetEnableControl(Me, txtAuthAmt, True)
                    ControlMgr.SetEnableControl(Me, txtServiceCharge, True)
                    ControlMgr.SetEnableControl(Me, txtServiceChargeTax, True)
                    ControlMgr.SetEnableControl(Me, txtAboveLiability, True)
                    ControlMgr.SetEnableControl(Me, txtTripAmt, True)
                    ControlMgr.SetEnableControl(Me, txtTripAmtTax, True)
                    ControlMgr.SetEnableControl(Me, txtSalvageAmt, True)
                    ControlMgr.SetEnableControl(Me, txtShipping, True)
                    ControlMgr.SetEnableControl(Me, txtShippingTax, True)
                    ControlMgr.SetEnableControl(Me, txtConsumerPays, True)
                    ControlMgr.SetEnableControl(Me, txtDisposition, True)
                    ControlMgr.SetEnableControl(Me, txtDispositionTax, True)
                    ControlMgr.SetEnableControl(Me, txtAssurantPays, True)
                    ControlMgr.SetEnableControl(Me, txtDiagnostics, True)
                    ControlMgr.SetEnableControl(Me, txtDiagnosticsTax, True)
                    ControlMgr.SetEnableControl(Me, txtRemainingAmt, True)
                    ControlMgr.SetEnableControl(Me, txtAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, LabelAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, txtOtherAmt, True)
                    ControlMgr.SetEnableControl(Me, txtOtherTax, True)
                Case ClaimInvoice.PAYEE_OPTION_SERVICE_CENTER
                    Dim claimServiceCenter As ServiceCenter = New ServiceCenter(State.ClaimInvoiceBO.Invoiceable.ServiceCenterId)
                    'If the pay master is on, pay master instead
                    If claimServiceCenter.PayMaster AndAlso (Not claimServiceCenter.MasterCenterId.Equals(Guid.Empty)) Then
                        claimServiceCenter = New ServiceCenter(claimServiceCenter.MasterCenterId)
                    End If

                    If claimServiceCenter.WithholdingRate IsNot Nothing Then State.ClaimInvoiceBO.ServiceCenterWithholdingRate = claimServiceCenter.WithholdingRate.Value * (-1)

                    txtPayeeName.Text = claimServiceCenter.Description
                    If State.ClaimInvoiceBO.IsInsuranceCompany Then
                        State.ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeOptionCode, claimServiceCenter, State.ClaimInvoiceBO, State.DisbursementBO, Nothing, Nothing, Nothing)
                    End If
                    If Not claimServiceCenter.BankInfoId.Equals(Guid.Empty) Then
                        State.ClaimInvoiceBO.PayeeBankInfo = claimServiceCenter.Add_BankInfo()
                        State.ClaimInvoiceBO.PayeeAddress = Nothing
                        State.PayeeAddress = Nothing
                        ControlMgr.SetVisibleControl(Me, PayeeAddress, False)
                        ControlMgr.SetVisibleControl(Me, PayeeBankInfo, True)
                        State.PayeeBankInfo = State.ClaimInvoiceBO.Add_BankInfo(claimServiceCenter.BankInfoId) 'New BankInfo(claimServiceCenter.BankInfoId)
                        State.PayeeBankInfo.SourceCountryID = oCompany.CountryId
                        PayeeBankInfo.Bind(State.PayeeBankInfo)
                        PayeeBankInfo.ChangeEnabledControlProperty(False)
                    Else
                        ControlMgr.SetVisibleControl(Me, PayeeAddress, True)
                        ControlMgr.SetVisibleControl(Me, PayeeBankInfo, False)
                        State.ClaimInvoiceBO.PayeeAddress = claimServiceCenter.Address
                        State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                        State.PayeeBankInfo = Nothing
                        State.PayeeAddress = State.ClaimInvoiceBO.Add_Address(claimServiceCenter.AddressId) 'New Address(claimServiceCenter.AddressId)
                        PayeeAddress.Bind(State.PayeeAddress)
                        ChangeEnabledProperty(txtPayeeName, False)
                        PayeeAddress.EnableControls(True)
                        ControlMgr.SetVisibleControl(Me, txtIvaTax, claimServiceCenter.IvaResponsibleFlag)
                        ControlMgr.SetVisibleControl(Me, LabelIvaTax, claimServiceCenter.IvaResponsibleFlag)
                        ChangeEnabledProperty(txtIvaTax, (Not State.ViewOnly))
                    End If
                    PayeeBankInfo.HideTaxId()

                    ControlMgr.SetVisibleControl(Me, divlblPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, divtxtPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, divtxtPaymenttoCustomertax, False)
                    ControlMgr.SetVisibleControl(Me, divCheckBoxPaymenttocustomer, False)

                    ControlMgr.SetEnableControl(Me, txtLiabilityLimit, True)
                    ControlMgr.SetEnableControl(Me, txtDeductible, True)
                    ControlMgr.SetEnableControl(Me, txtLabor, True)
                    ControlMgr.SetEnableControl(Me, txtLaborTax, True)
                    ControlMgr.SetEnableControl(Me, txtDiscount, True)
                    ControlMgr.SetEnableControl(Me, txtParts, True)
                    ControlMgr.SetEnableControl(Me, txtPartsTax, True)
                    ControlMgr.SetEnableControl(Me, txtAuthAmt, True)
                    ControlMgr.SetEnableControl(Me, txtServiceCharge, True)
                    ControlMgr.SetEnableControl(Me, txtServiceChargeTax, True)
                    ControlMgr.SetEnableControl(Me, txtAboveLiability, True)
                    ControlMgr.SetEnableControl(Me, txtTripAmt, True)
                    ControlMgr.SetEnableControl(Me, txtTripAmtTax, True)
                    ControlMgr.SetEnableControl(Me, txtSalvageAmt, True)
                    ControlMgr.SetEnableControl(Me, txtShipping, True)
                    ControlMgr.SetEnableControl(Me, txtShippingTax, True)
                    ControlMgr.SetEnableControl(Me, txtConsumerPays, True)
                    ControlMgr.SetEnableControl(Me, txtDisposition, True)
                    ControlMgr.SetEnableControl(Me, txtDispositionTax, True)
                    ControlMgr.SetEnableControl(Me, txtAssurantPays, True)
                    ControlMgr.SetEnableControl(Me, txtDiagnostics, True)
                    ControlMgr.SetEnableControl(Me, txtDiagnosticsTax, True)
                    ControlMgr.SetEnableControl(Me, txtRemainingAmt, True)
                    ControlMgr.SetEnableControl(Me, txtAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, LabelAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, txtOtherAmt, True)
                    ControlMgr.SetEnableControl(Me, txtOtherTax, True)
                Case ClaimInvoice.PAYEE_OPTION_LOANER_CENTER
                    State.ClaimInvoiceBO.ServiceCenterWithholdingRate = State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value
                    If Not State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                        Dim loanerServiceCenter As ServiceCenter = New ServiceCenter(State.ClaimInvoiceBO.Invoiceable.LoanerCenterId)
                        txtPayeeName.Text = loanerServiceCenter.Description
                        If State.ClaimInvoiceBO.IsInsuranceCompany Then
                            State.ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeOptionCode, loanerServiceCenter, State.ClaimInvoiceBO, State.DisbursementBO, Nothing, Nothing, Nothing)
                        End If
                        If loanerServiceCenter.WithholdingRate IsNot Nothing Then State.ClaimInvoiceBO.ServiceCenterWithholdingRate = loanerServiceCenter.WithholdingRate.Value * (-1)
                        If Not loanerServiceCenter.BankInfoId.Equals(Guid.Empty) Then
                            State.ClaimInvoiceBO.PayeeBankInfo = loanerServiceCenter.Add_BankInfo()
                            State.ClaimInvoiceBO.PayeeAddress = Nothing
                            State.PayeeAddress = Nothing
                            ControlMgr.SetVisibleControl(Me, PayeeAddress, False)
                            ControlMgr.SetVisibleControl(Me, PayeeBankInfo, True)
                            State.PayeeBankInfo = State.ClaimInvoiceBO.Add_BankInfo(loanerServiceCenter.BankInfoId) 'New BankInfo(loanerServiceCenter.BankInfoId)
                            State.PayeeBankInfo.SourceCountryID = oCompany.CountryId
                            PayeeBankInfo.Bind(State.PayeeBankInfo)
                            PayeeBankInfo.ChangeEnabledControlProperty(False)
                        Else
                            ControlMgr.SetVisibleControl(Me, PayeeAddress, True)
                            ControlMgr.SetVisibleControl(Me, PayeeBankInfo, False)
                            State.ClaimInvoiceBO.PayeeAddress = loanerServiceCenter.Address   'Added by AA for WR761620
                            State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                            State.PayeeBankInfo = Nothing
                            State.PayeeAddress = State.ClaimInvoiceBO.Add_Address(loanerServiceCenter.AddressId) 'New Address(loanerServiceCenter.AddressId)
                            PayeeAddress.Bind(State.PayeeAddress)
                            ChangeEnabledProperty(txtPayeeName, False)
                            PayeeAddress.EnableControls(True)
                            ControlMgr.SetVisibleControl(Me, txtIvaTax, loanerServiceCenter.IvaResponsibleFlag)
                            ControlMgr.SetVisibleControl(Me, LabelIvaTax, loanerServiceCenter.IvaResponsibleFlag)
                            ChangeEnabledProperty(txtIvaTax, (Not State.ViewOnly))
                        End If
                    End If
                    PayeeBankInfo.HideTaxId()

                    ControlMgr.SetVisibleControl(Me, divlblPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, divtxtPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, divtxtPaymenttoCustomertax, False)
                    ControlMgr.SetVisibleControl(Me, divCheckBoxPaymenttocustomer, False)

                    ControlMgr.SetEnableControl(Me, txtLiabilityLimit, True)
                    ControlMgr.SetEnableControl(Me, txtDeductible, True)
                    ControlMgr.SetEnableControl(Me, txtLabor, True)
                    ControlMgr.SetEnableControl(Me, txtLaborTax, True)
                    ControlMgr.SetEnableControl(Me, txtDiscount, True)
                    ControlMgr.SetEnableControl(Me, txtParts, True)
                    ControlMgr.SetEnableControl(Me, txtPartsTax, True)
                    ControlMgr.SetEnableControl(Me, txtAuthAmt, True)
                    ControlMgr.SetEnableControl(Me, txtServiceCharge, True)
                    ControlMgr.SetEnableControl(Me, txtServiceChargeTax, True)
                    ControlMgr.SetEnableControl(Me, txtAboveLiability, True)
                    ControlMgr.SetEnableControl(Me, txtTripAmt, True)
                    ControlMgr.SetEnableControl(Me, txtTripAmtTax, True)
                    ControlMgr.SetEnableControl(Me, txtSalvageAmt, True)
                    ControlMgr.SetEnableControl(Me, txtShipping, True)
                    ControlMgr.SetEnableControl(Me, txtShippingTax, True)
                    ControlMgr.SetEnableControl(Me, txtConsumerPays, True)
                    ControlMgr.SetEnableControl(Me, txtDisposition, True)
                    ControlMgr.SetEnableControl(Me, txtDispositionTax, True)
                    ControlMgr.SetEnableControl(Me, txtAssurantPays, True)
                    ControlMgr.SetEnableControl(Me, txtDiagnostics, True)
                    ControlMgr.SetEnableControl(Me, txtDiagnosticsTax, True)
                    ControlMgr.SetEnableControl(Me, txtRemainingAmt, True)
                    ControlMgr.SetEnableControl(Me, txtAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, LabelAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, txtOtherAmt, True)
                    ControlMgr.SetEnableControl(Me, txtOtherTax, True)
                Case ClaimInvoice.PAYEE_OPTION_CUSTOMER
                    If State.ClaimBO.Dealer.AttributeValues.Contains(Codes.DLR_ATTRBT_PAY_TO_CUST_AMT) Then
                        If oDealer.AttributeValues.Value(Codes.DLR_ATTRBT_PAY_TO_CUST_AMT) = Codes.YESNO_Y AndAlso State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then

                            ControlMgr.SetVisibleControl(Me, hrSeprator, True)
                            ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, True)
                            ControlMgr.SetVisibleControl(Me, moPaymentMethodLabel, True)
                            ControlMgr.SetVisibleControl(Me, Req_Pay_MethodLabel, True)
                            ControlMgr.SetVisibleControl(Me, PayeeAddress, False)
                            ControlMgr.SetVisibleControl(Me, PayeeBankInfo, False)

                            'ControlMgr.SetVisibleControl(Me, Me.trPaymenttoCustomer, True)
                            ControlMgr.SetVisibleControl(Me, divlblPaymenttoCustomer, True)
                            ControlMgr.SetVisibleControl(Me, divtxtPaymenttoCustomer, True)
                            ControlMgr.SetVisibleControl(Me, divtxtPaymenttoCustomertax, True)
                            ControlMgr.SetVisibleControl(Me, divCheckBoxPaymenttocustomer, True)

                            ControlMgr.SetEnableControl(Me, txtLiabilityLimit, False)
                            ControlMgr.SetEnableControl(Me, txtDeductible, False)
                            ControlMgr.SetEnableControl(Me, txtLabor, False)
                            ControlMgr.SetEnableControl(Me, txtLaborTax, False)
                            ControlMgr.SetEnableControl(Me, txtDiscount, False)
                            ControlMgr.SetEnableControl(Me, txtParts, False)
                            ControlMgr.SetEnableControl(Me, txtPartsTax, False)
                            ControlMgr.SetEnableControl(Me, txtAuthAmt, False)
                            ControlMgr.SetEnableControl(Me, txtServiceCharge, False)
                            ControlMgr.SetEnableControl(Me, txtServiceChargeTax, False)
                            ControlMgr.SetEnableControl(Me, txtAboveLiability, False)
                            ControlMgr.SetEnableControl(Me, txtTripAmt, False)
                            ControlMgr.SetEnableControl(Me, txtTripAmtTax, False)
                            ControlMgr.SetEnableControl(Me, txtSalvageAmt, False)
                            ControlMgr.SetEnableControl(Me, txtShipping, False)
                            ControlMgr.SetEnableControl(Me, txtShippingTax, False)
                            ControlMgr.SetEnableControl(Me, txtConsumerPays, False)
                            ControlMgr.SetEnableControl(Me, txtDisposition, False)
                            ControlMgr.SetEnableControl(Me, txtDispositionTax, False)
                            ControlMgr.SetEnableControl(Me, txtAssurantPays, False)
                            ControlMgr.SetEnableControl(Me, txtDiagnostics, False)
                            ControlMgr.SetEnableControl(Me, txtDiagnosticsTax, False)
                            ControlMgr.SetEnableControl(Me, txtRemainingAmt, False)
                            ControlMgr.SetEnableControl(Me, txtAlreadyPaid, False)
                            ControlMgr.SetEnableControl(Me, LabelAlreadyPaid, False)
                            ControlMgr.SetEnableControl(Me, txtOtherAmt, False)
                            ControlMgr.SetEnableControl(Me, txtOtherTax, False)
                        Else
                            ControlMgr.SetVisibleControl(Me, hrSeprator, True)
                            ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, True)
                            ControlMgr.SetVisibleControl(Me, moPaymentMethodLabel, True)
                            ControlMgr.SetVisibleControl(Me, Req_Pay_MethodLabel, True)
                            ControlMgr.SetVisibleControl(Me, PayeeAddress, False)
                            ControlMgr.SetVisibleControl(Me, PayeeBankInfo, False)
                        End If
                    Else
                        ControlMgr.SetVisibleControl(Me, hrSeprator, True)
                        ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, True)
                        ControlMgr.SetVisibleControl(Me, moPaymentMethodLabel, True)
                        ControlMgr.SetVisibleControl(Me, Req_Pay_MethodLabel, True)
                        ControlMgr.SetVisibleControl(Me, PayeeAddress, False)
                        ControlMgr.SetVisibleControl(Me, PayeeBankInfo, False)
                    End If

                    txtPayeeName.Text = State.ClaimInvoiceBO.CustomerName
                    If State.ClaimInvoiceBO.IsInsuranceCompany Then
                        ShowDocumentIDInformation(False)
                        Dim objCertificate As Certificate = New Certificate(State.ClaimInvoiceBO.Invoiceable.CertificateId)
                        State.ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeOptionCode, Nothing, State.ClaimInvoiceBO, State.DisbursementBO, objCertificate, Nothing, Nothing)
                    End If
                    If bNewSelection Then
                        State.PayeeAddress = State.ClaimInvoiceBO.Add_Address(State.ClaimInvoiceBO.CustomerAddressID) 'New Address(Me.State.ClaimInvoiceBO.CustomerAddressID)
                        State.ClaimInvoiceBO.PayeeAddress = State.PayeeAddress
                    End If
                    PayeeAddress.EnableControls(False, True)
                    PayeeBankInfo.DisplayTaxId()
                    Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate = 0
                Case ClaimInvoice.PAYEE_OPTION_OTHER
                    ControlMgr.SetVisibleControl(Me, hrSeprator, True)
                    ControlMgr.SetVisibleControl(Me, PaymentMethodDrop, True)
                    ControlMgr.SetVisibleControl(Me, moPaymentMethodLabel, True)
                    ControlMgr.SetVisibleControl(Me, Req_Pay_MethodLabel, True)
                    ControlMgr.SetVisibleControl(Me, PayeeAddress, False)
                    ControlMgr.SetVisibleControl(Me, PayeeBankInfo, False)
                    If State.ClaimInvoiceBO.IsInsuranceCompany Then ShowDocumentIDInformation(True)
                    If State.ViewOnly Then
                        PopulatePayeeAddressBO()
                        ChangeEnabledProperty(txtPayeeName, False)
                    Else
                        txtPayeeName.Text = ""
                        If bNewSelection Then
                            State.PayeeAddress = State.ClaimInvoiceBO.AddressChild()
                        End If
                        State.ClaimInvoiceBO.PayeeAddress = State.PayeeAddress  'Added by AA for WR761620
                        ChangeEnabledProperty(txtPayeeName, True)

                    End If
                    PayeeAddress.Bind(State.PayeeAddress)

                    If bNewSelection Then
                        PayeeAddress.ClearAll()
                    End If
                    PayeeAddress.EnableControls(State.ViewOnly, True)
                    If State.ViewOnly Then PayeeAddress.RegionText = State.DisbursementBO.RegionDesc
                    ControlMgr.SetVisibleControl(Me, txtIvaTax, False)
                    txtIvaTax.Text = "0"
                    ControlMgr.SetVisibleControl(Me, LabelIvaTax, False)
                    PayeeBankInfo.DisplayTaxId()
                    Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate = 0

                    ControlMgr.SetVisibleControl(Me, divlblPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, divtxtPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, divtxtPaymenttoCustomertax, False)
                    ControlMgr.SetVisibleControl(Me, divCheckBoxPaymenttocustomer, False)

                    ControlMgr.SetEnableControl(Me, txtLiabilityLimit, True)
                    ControlMgr.SetEnableControl(Me, txtDeductible, True)
                    ControlMgr.SetEnableControl(Me, txtLabor, True)
                    ControlMgr.SetEnableControl(Me, txtLaborTax, True)
                    ControlMgr.SetEnableControl(Me, txtDiscount, True)
                    ControlMgr.SetEnableControl(Me, txtParts, True)
                    ControlMgr.SetEnableControl(Me, txtPartsTax, True)
                    ControlMgr.SetEnableControl(Me, txtAuthAmt, True)
                    ControlMgr.SetEnableControl(Me, txtServiceCharge, True)
                    ControlMgr.SetEnableControl(Me, txtServiceChargeTax, True)
                    ControlMgr.SetEnableControl(Me, txtAboveLiability, True)
                    ControlMgr.SetEnableControl(Me, txtTripAmt, True)
                    ControlMgr.SetEnableControl(Me, txtTripAmtTax, True)
                    ControlMgr.SetEnableControl(Me, txtSalvageAmt, True)
                    ControlMgr.SetEnableControl(Me, txtShipping, True)
                    ControlMgr.SetEnableControl(Me, txtShippingTax, True)
                    ControlMgr.SetEnableControl(Me, txtConsumerPays, True)
                    ControlMgr.SetEnableControl(Me, txtDisposition, True)
                    ControlMgr.SetEnableControl(Me, txtDispositionTax, True)
                    ControlMgr.SetEnableControl(Me, txtAssurantPays, True)
                    ControlMgr.SetEnableControl(Me, txtDiagnostics, True)
                    ControlMgr.SetEnableControl(Me, txtDiagnosticsTax, True)
                    ControlMgr.SetEnableControl(Me, txtRemainingAmt, True)
                    ControlMgr.SetEnableControl(Me, txtAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, LabelAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, txtOtherAmt, True)
                    ControlMgr.SetEnableControl(Me, txtOtherTax, True)
            End Select

            'If Me.IsPostBack AndAlso Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value <> 0 AndAlso (PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_MASTER_CENTER Or
            '                                                                                                      PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_SERVICE_CENTER Or
            '                                                                                                      PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_LOANER_CENTER) Then
            '    ComputeAutoTaxes(True)
            'End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ShowDocumentIDInformation(blnSetVisible As Boolean)
        If blnSetVisible Then
            ControlMgr.SetVisibleControl(Me, moDocumentTypeLabel, True)
            ControlMgr.SetVisibleControl(Me, cboDocumentTypeId, True)
            ControlMgr.SetVisibleControl(Me, moTaxIdLabel, True)
            ControlMgr.SetVisibleControl(Me, moTaxIdText, True)
            ControlMgr.SetVisibleControl(Me, Req_Doc_TypeLabel, True)
            ControlMgr.SetVisibleControl(Me, Req_Doc_NumLabel, True)

            If State.ViewOnly AndAlso State.DisbursementBO IsNot Nothing AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, State.DisbursementBO.PayeeOptionId) = ClaimInvoice.PAYEE_OPTION_OTHER Then
                PopulateControlFromBOProperty(cboDocumentTypeId, LookupListNew.GetIdFromCode(LookupListNew.LK_DOCUMENT_TYPES, State.DisbursementBO.DocumentType))
                PopulateControlFromBOProperty(moTaxIdText, State.DisbursementBO.IdentificationNumber)
            Else
                PopulateControlFromBOProperty(cboDocumentTypeId, LookupListNew.GetIdFromCode(LookupListNew.LK_DOCUMENT_TYPES, State.ClaimInvoiceBO.DocumentType))
                PopulateControlFromBOProperty(moTaxIdText, State.ClaimInvoiceBO.TaxId)
            End If

        Else
            ControlMgr.SetVisibleControl(Me, moDocumentTypeLabel, False)
            ControlMgr.SetVisibleControl(Me, cboDocumentTypeId, False)
            ControlMgr.SetVisibleControl(Me, moTaxIdLabel, False)
            ControlMgr.SetVisibleControl(Me, moTaxIdText, False)
            ControlMgr.SetVisibleControl(Me, Req_Doc_TypeLabel, False)
            ControlMgr.SetVisibleControl(Me, Req_Doc_NumLabel, False)
        End If

    End Sub
    Private Sub PopulatePayeeAddressBO()
        State.PayeeAddress = State.ClaimInvoiceBO.Add_Address() 'New Address
        State.PayeeAddress.Address1 = State.DisbursementBO.Address1
        State.PayeeAddress.Address2 = State.DisbursementBO.Address2
        State.PayeeAddress.City = State.DisbursementBO.City
        State.PayeeAddress.PostalCode = State.DisbursementBO.Zip
        State.PayeeAddress.CountryId = New Company(State.DisbursementBO.CompanyId).CountryId

    End Sub
    Private Sub PopulatePayeeBankInfoBO()
        State.PayeeBankInfo = State.ClaimInvoiceBO.Add_BankInfo() 'New bankinfo
        State.PayeeBankInfo.Account_Name = State.DisbursementBO.AccountName
        State.PayeeBankInfo.Account_Number = State.DisbursementBO.AccountNumber
        State.PayeeBankInfo.Bank_Id = State.DisbursementBO.BankID
        State.PayeeBankInfo.SwiftCode = State.DisbursementBO.SwiftCode
        State.PayeeBankInfo.IbanNumber = State.DisbursementBO.IbanNumber
        State.PayeeBankInfo.AccountTypeId = LookupListNew.GetIdFromDescription(LookupListNew.LK_ACCOUNT_TYPES, State.DisbursementBO.AccountType)
        State.PayeeBankInfo.CountryID = LookupListNew.GetIdFromDescription(LookupListNew.LK_COUNTRIES, State.DisbursementBO.Country)
        State.PayeeBankInfo.PaymentReasonID = LookupListNew.GetIdFromDescription(LookupListNew.LK_PAYMENTREASON, State.DisbursementBO.PaymentReason)
        State.PayeeBankInfo.SourceCountryID = oCompany.CountryId
        State.PayeeBankInfo.TaxId = State.DisbursementBO.IdentificationNumber
    End Sub

    Private Function IsGiftCardValid() As Boolean

        Dim listItem As ListItem = PaymentMethodDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode(
                                                                      "PMTHD", Codes.PAYMENT_METHOD__DARTY_GIFT_CARD))
        If (listItem IsNot Nothing) Then
            If (cboPayeeSelector.SelectedItem.Text.ToUpper = Payee_Customer AndAlso PaymentMethodDrop.SelectedItem.Text = listItem.Text) Then
                If (oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).Count > 0) Then
                    Dim attvalue As AttributeValue = oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).First()
                    If (attvalue.EffectiveDate > DateTime.UtcNow Or attvalue.ExpirationDate < DateTime.UtcNow) Then
                        Return False
                    End If
                End If
            End If
        End If
        Return True

    End Function

    Private Sub GetGiftCardInfo()
        Dim oDartySvcManager As IDartyServiceManager
        Dim response As GenerateGiftCardResponse
        Dim argumentsToAddEvent As String
        Dim giftCardType As String


        With State.ClaimBO
            Dim attvalue As AttributeValue = oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).FirstOrDefault

            If attvalue IsNot Nothing Then
                giftCardType = attvalue.Value
                'Me.oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE And DateTime.Today >= i.EffectiveDate And DateTime.Today < i.ExpirationDate).First().Value

                argumentsToAddEvent = "ClaimId:" & DALBase.GuidToSQLString(.Id) & ";ClaimNumber:" & .ClaimNumber & ";Amount: " & Decimal.Parse(txtTotal.Text).ToString(CultureInfo.InvariantCulture) & ";GiftCardType:" & giftCardType & ""
                PublishedTask.AddEvent(companyGroupId:=Guid.Empty,
                                           companyId:=Guid.Empty,
                                           countryId:=Guid.Empty,
                                           dealerId:= .Dealer.Id,
                                           productCode:=String.Empty,
                                           coverageTypeId:=Guid.Empty,
                                           sender:="Pay Claim",
                                           arguments:=argumentsToAddEvent,
                                           eventDate:=DateTime.UtcNow,
                                           eventTypeId:=LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__CLM_SEND_REIMBURSE_INFO),
                                           eventArgumentId:=Nothing)
            End If
        End With

    End Sub
#End Region


End Class
