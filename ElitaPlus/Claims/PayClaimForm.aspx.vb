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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
                _Company = New Company(Me.State.ClaimInvoiceBO.CompanyId)
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
                _Certificate = New Certificate(Me.State.ClaimBO.CertificateId)
            End If
            Return _Certificate
        End Get
    End Property

    Public ReadOnly Property oDealer() As Dealer
        Get
            If _Dealer Is Nothing Then
                _Dealer = New Dealer(Me.oCertificate.DealerId)
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ClaimInvoice, Optional ByVal boChanged As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

        Public Sub New(ByVal claimBO As Claim, Optional ByVal isForClaimPayAdjustment As Boolean = False)
            Me.ClaimBO = claimBO
            Me.PayClaimID = Guid.Empty
            Me.ViewOnly = False
            Me.ClaimID = claimBO.Id
            Me.isForClaimPayAdjust = isForClaimPayAdjustment
        End Sub

        Public Sub New(ByVal claimInvoiceID As Guid)
            Me.ClaimBO = Nothing
            Me.PayClaimID = claimInvoiceID
            Me.ViewOnly = True
            Me.ClaimID = Guid.Empty
        End Sub

        Public Sub New(ByVal claimID As Guid, ByVal viewOnly As Boolean)
            Me.ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(claimID)
            Me.PayClaimID = Guid.Empty
            Me.ViewOnly = viewOnly
            Me.ClaimID = claimID
        End Sub

        'DEF-17426
        Public Sub New(ByVal claimId As Guid, ByVal hasDataChanged As Boolean, ByVal viewOnly As Boolean)
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

        Public Sub LoadManualTaxesAmountChanges(ByVal lstMT As Collections.Generic.List(Of PayClaimManualTaxForm.ManualTaxDetail))
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
                    If (Not dvManulTax Is Nothing) AndAlso (dvManulTax.Count > 0) Then
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
                If (Not ManualTaxes Is Nothing) AndAlso (ManualTaxes.Count > 0) Then
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
                If (Not ManualTaxes Is Nothing) AndAlso (ManualTaxes.Count > 0) Then
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
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                StartNavControl()
                Dim params As Parameters = CType(Me.CallingParameters, Parameters)
                If params.PayClaimID.Equals(Guid.Empty) Then
                    Me.State.ClaimBO = params.ClaimBO
                    'coming from payclaim , pass claimbo to prepopulate data
                    Me.State.ClaimInvoiceBO = New ClaimInvoice
                    Me.State.DisbursementBO = Me.State.ClaimInvoiceBO.AddNewDisbursement()
                    Me.State.ClaimInvoiceBO.PrepopulateClaimInvoice(Me.State.ClaimBO, True)
                    Me.State.ClaimInvoiceBO.PrepopulateDisbursment()

                    Me.State.isForClaimPayAdjust = params.isForClaimPayAdjust
                    Me.State.ClaimInvoiceBO.IsNewPaymentFromPaymentAdjustment = Me.State.isForClaimPayAdjust
                    'default values
                    Me.State.ClaimInvoiceBO.BatchNumber = "1"
                    Me.State.ClaimInvoiceBO.LaborTax = New DecimalType(0D)
                    Me.State.ClaimInvoiceBO.DispositionAmount = New DecimalType(0D)
                    Me.State.ClaimInvoiceBO.DiagnosticsAmount = New DecimalType(0D)

                    'Initialize the part tax value with the amount for the parts info  
                    Me.State.ClaimInvoiceBO.PartAmount = PartsInfo.getTotalCost(Me.State.ClaimBO.Id).Value
                    Me.State.ClaimInvoiceBO.PartTax = New DecimalType(0D)
                    Me.State.ClaimInvoiceBO.RecordCount = New LongType(1)
                    Me.State.ClaimInvoiceBO.Source = Nothing
                    Me.State.ClaimInvoiceBO.BeginEdit()
                Else
                    'coming from view invoice
                    Me.State.ClaimBO = Nothing
                    Me.State.ClaimInvoiceBO = New ClaimInvoice(params.PayClaimID)
                    Me.State.DisbursementBO = New Disbursement(Me.State.ClaimInvoiceBO.DisbursementId)
                    Me.State.ClaimInvoiceBO.PaymentMethodID = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYMENTMETHOD, Me.State.DisbursementBO.PaymentMethod)
                End If
                Me.State.ViewOnly = params.ViewOnly
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
#End Region

#Region "Navigation Control"
    Sub StartNavControl()
        'If Me.NavController Is Nothing Then
        Dim nav As New ElitaPlusNavigation
        Me.NavController = New NavControllerBase(nav.Flow("PAY_CLAIM_DETAIL"))
        'End If
    End Sub
#End Region

#Region "Page_Events"

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If State.HasManualTaxes Then
            EnableDisabManualTaxFields(True)
            Me.txtManualTax.Text = State.TotalManualTaxes.ToString
        Else
            EnableDisabManualTaxFields(False, Me.State.OverrideShowGrandTotal)
        End If
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            'if coming from ClaimAuthDetail form reload claim
            If Me.CalledUrl = ClaimAuthDetailForm.URL Then
                Dim retObj As ClaimAuthDetailForm.ReturnType = CType(ReturnPar, ClaimAuthDetailForm.ReturnType)
                If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                    'reload
                    Me.State.ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.ClaimBO.Id)
                    Me.State.ClaimInvoiceBO.RefreshCurrentClaim()
                End If
            ElseIf Me.CalledUrl = PayClaimManualTaxForm.URL Then
                Dim retTaxDetails As PayClaimManualTaxForm.Parameters
                retTaxDetails = CType(ReturnPar, PayClaimManualTaxForm.Parameters)
                State.LoadManualTaxesAmountChanges(retTaxDetails.ManualTaxList)
                'Me.State.ClaimBO.AuthorizedAmount = New DecimalType(Me.State.ClaimBO.AuthorizedAmount.Value + State.TotalManualTaxes)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        Me.MasterPage.MessageController.Clear_Hide()

        Try
            'DEF-17426
            If Me.NavController.Context = "PAY_CLAIM_DETAIL" Then
                Dim retObj As Parameters = CType(Me.NavController.ParametersPassed, Parameters)
                If Not retObj Is Nothing AndAlso retObj.hasDataChanged Then
                    Me.State.ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(retObj.ClaimID)
                    Me.State.ClaimInvoiceBO.RefreshCurrentClaim()
                End If

                If (Not Me.NavController Is Nothing) AndAlso (Not Me.NavController.PrevNavState Is Nothing) AndAlso (Me.NavController.PrevNavState.Name = "AUTH_DETAIL" Or Me.NavController.PrevNavState.Name = "PARTS_INFO") Then
                    Me.State.ComingFromChildForm = True
                End If

                If (Not Me.CalledUrl Is Nothing) AndAlso (Me.CalledUrl = PayClaimManualTaxForm.URL Or Me.CalledUrl = Claims.ReplacementForm.URL) Then
                    Me.State.ComingFromChildForm = True
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

            If Not State.ClaimBO Is Nothing AndAlso State.ClaimBO.Dealer.AttributeValues.Contains(Codes.DLR_ATTRBT_PAY_TO_CUST_AMT) Then
                If Not (State.ClaimBO.Dealer.AttributeValues.Value(Codes.DLR_ATTRBT_PAY_TO_CUST_AMT) = Codes.YESNO_Y AndAlso Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT) Then
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

            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                CheckifComingFromNewClaimBackEndClaim()

                CheckifComingFromClaimAdjustment()

                ControlMgr.SetVisibleControl(Me, Me.hrSeprator, False)

                Me.MenuEnabled = False

                Me.AddCalendar(Me.ImageButtonRepairDate, Me.txtRepairDate)
                Me.AddCalendar(Me.ImageButtonLoanerReturnedDate, Me.txtLoanerReturnedDate)
                Me.AddCalendar(Me.ImageButtonInvoiceDate, Me.txtInvoiceDate)

                'Pickup date: do not display if replacement and/or interface claim
                If Not Me.State.ClaimBO Is Nothing AndAlso Me.State.ClaimBO.CanDisplayVisitAndPickUpDates And Not Me.State.isForClaimPayAdjust Then
                    If Not Me.State.ClaimBO.LoanerTaken Then Me.AddCalendar(Me.ImageButtonPickupDate, Me.TextboxPickupDate)
                End If

                If Me.State.ClaimInvoiceBO Is Nothing Then
                    Me.State.ClaimInvoiceBO = New ClaimInvoice
                End If
                'Trace(Me, "ClaimInvoice Id=" & GuidControl.GuidToHexString(Me.State.ClaimInvoiceBO.Id))
                'Claim Auth Detail logic
                If Not Me.State.ClaimBO Is Nothing Then
                    If oCompany.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) Then
                        Me.State.AuthDetailEnabled = True
                        Me.LoadClaimAuthDetail()
                    End If
                End If

                PopulateDropdowns()
                Me.PopulateFormFromBOs()
                If Not Me.State.selectedPaymentMethodID.Equals(Guid.Empty) Then
                    Me.PopulateControlFromBOProperty(Me.PaymentMethodDrop, Me.State.selectedPaymentMethodID)
                    Me.PaymentMethodChanged(Me.State.selectedPaymentMethodID, Me.GetSelectedItem(Me.cboPayeeSelector), False)
                End If
            Else
                If Not Me.IsClientScriptBlockRegistered("load") AndAlso (Me.InvoiceMethod.Value = "2" And Me.State.AuthDetailEnabled) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "load", "doAmtCalc(document.getElementById('ctl00_SummaryPlaceHolder_txtLabor'));", True)
                End If
                loadTaxAmountsFromHiddenFields()
            End If
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "load", "doAmtCalc(document.getElementById('txtLabor'));", True)
            CheckIfComingFromSaveConfirm()
            Me.EnableDisableFields()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.ClaimInvoiceBO)
                AddLabelColons()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub AddLabelColons()
        If Not Me.LabelPayee.Text.EndsWith(":") Then
            Me.LabelPayee.Text = Me.LabelPayee.Text & ":"
        End If
        If Not Me.LabelInvNumber.Text.EndsWith(":") Then
            Me.LabelInvNumber.Text = Me.LabelInvNumber.Text & ":"
        End If
        If Not Me.LabelCustomerName.Text.EndsWith(":") Then
            Me.LabelCustomerName.Text = Me.LabelCustomerName.Text & ":"
        End If
        If Not Me.LabelBillingSvcCenter.Text.EndsWith(":") Then
            Me.LabelBillingSvcCenter.Text = Me.LabelBillingSvcCenter.Text & ":"
        End If
        If Not Me.LabelCauseOfLoss.Text.EndsWith(":") Then
            Me.LabelCauseOfLoss.Text = Me.LabelCauseOfLoss.Text & ":"
        End If
        If Not Me.LabelLabor.Text.EndsWith(":") Then
            Me.LabelLabor.Text = Me.LabelLabor.Text & ":"
        End If
        If Not Me.LabelParts.Text.EndsWith(":") Then
            Me.LabelParts.Text = Me.LabelParts.Text & ":"
        End If
        If Not Me.LabelSvcCharge.Text.EndsWith(":") Then
            Me.LabelSvcCharge.Text = Me.LabelSvcCharge.Text & ":"
        End If
        If Not Me.LabelTripAmount.Text.EndsWith(":") Then
            Me.LabelTripAmount.Text = Me.LabelTripAmount.Text & ":"
        End If
        If Not Me.LabelShipping.Text.EndsWith(":") Then
            Me.LabelShipping.Text = Me.LabelShipping.Text & ":"
        End If
        If Not Me.LabelTotal.Text.EndsWith(":") Then
            Me.LabelTotal.Text = Me.LabelTotal.Text & ":"
        End If
        If Not Me.LabelRepairCode.Text.EndsWith(":") Then
            Me.LabelRepairCode.Text = Me.LabelRepairCode.Text & ":"
        End If
        If Not Me.LabelPickUpDate.Text.EndsWith(":") Then
            Me.LabelPickUpDate.Text = Me.LabelPickUpDate.Text & ":"
        End If
    End Sub

    Protected Sub DisableButtonsForClaimSystem()
        If Not Me.State.ClaimBO.CertificateId.Equals(Guid.Empty) Then
            Dim oCert As New Certificate(Me.State.ClaimBO.CertificateId)
            Dim oDealer As New Dealer(oCert.DealerId)
            Dim oClmSystem As New ClaimSystem(oDealer.ClaimSystemId)
            Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            If oClmSystem.PayClaimId.Equals(noId) Then
                State.isClaimSystemMaintAllowed = False
                If Me.btnSave_WRITE.Visible And Me.btnSave_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, False)
                End If
                If Me.btnUndo_Write.Visible And Me.btnUndo_Write.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, False)
                End If
                If Me.btnPartsInfo_WRITE.Visible And Me.btnPartsInfo_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnPartsInfo_WRITE, False)
                End If
                If Me.btnAuthDetail_WRITE.Visible And Me.btnAuthDetail_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnAuthDetail_WRITE, False)
                End If
                If Me.btnReplacement_WRITE.Visible And Me.btnReplacement_WRITE.Enabled Then
                    ControlMgr.SetEnableControl(Me, Me.btnReplacement_WRITE, False)
                End If
            End If
        End If
    End Sub



    Public Sub loadTaxAmountsFromHiddenFields()
        Me.PopulateControlFromBOProperty(Me.txtPartsTax, hdPartsTaxAmt.Value)
        Me.PopulateControlFromBOProperty(Me.txtLaborTax, hdLaborTaxAmt.Value)
        Me.PopulateControlFromBOProperty(Me.txtServiceChargeTax, hdServiceChargeTaxAmt.Value)
        Me.PopulateControlFromBOProperty(Me.txtTripAmtTax, hdTripTaxAmt.Value)
        Me.PopulateControlFromBOProperty(Me.txtShippingTax, hdShippingTaxAmt.Value)
        Me.PopulateControlFromBOProperty(Me.txtDispositionTax, hdDispositionTaxAmt.Value)
        Me.PopulateControlFromBOProperty(Me.txtDiagnosticsTax, hdDiagnosticsTaxAmt.Value)
        Me.PopulateControlFromBOProperty(Me.txtOtherTax, hdOtherTaxAmt.Value)

        Me.PopulateControlFromBOProperty(Me.txtTotal, hdTotal.Value)
        Me.PopulateControlFromBOProperty(Me.txtSubTotal, hdSubTotalAmt.Value)
        Me.PopulateControlFromBOProperty(Me.txtTotalTaxAmount, hdTotalTaxAmt.Value)
        Me.PopulateControlFromBOProperty(Me.txtTotalWithholdingAmount, hdTotalWithholdings.Value)
        Me.PopulateControlFromBOProperty(Me.txtGrandTotal, hdGrandTotalAmt.Value)

    End Sub

    Private Sub EnableDisablePerceptionFields(ByVal blnEnabled As Boolean, Optional ByVal blnOverrideShowGrandTotal As Boolean = False)
        ControlMgr.SetVisibleControl(Me, trPerception_IIBB, blnEnabled)
        ControlMgr.SetVisibleControl(Me, trPerception_IVA, blnEnabled)

        If blnEnabled OrElse trManualTax.Visible Then
            ControlMgr.SetVisibleControl(Me, trGrandTotal, True)
        Else
            ControlMgr.SetVisibleControl(Me, trGrandTotal, False)
        End If

        If blnOverrideShowGrandTotal Then ControlMgr.SetVisibleControl(Me, trGrandTotal, True)
    End Sub

    Private Sub EnableDisabManualTaxFields(ByVal blnEnabled As Boolean, Optional ByVal blnOverrideShowGrandTotal As Boolean = False)
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
            Me.State.OverrideShowGrandTotal = False
            Dim oInvoiceMethodId As Guid = oCompany.InvoiceMethodId
            Me.InvoiceMethod.Value = LookupListNew.GetCodeFromId(LookupListNew.LK_INVOICE_METHOD, oInvoiceMethodId)

            If oInvoiceMethodId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_INVOICE_METHOD, ClaimInvoice.INVOICE_METHOD_DETAIL)) And Not Me.State.AuthDetailEnabled Then
                Me.ChangeEnabledProperty(txtLabor, (Not Me.State.ViewOnly) And True)
                Me.ChangeEnabledProperty(txtParts, (Not Me.State.ViewOnly) And True)
                Me.ChangeEnabledProperty(txtServiceCharge, (Not Me.State.ViewOnly) And True)
                Me.ChangeEnabledProperty(txtTripAmt, (Not Me.State.ViewOnly) And True)
                Me.ChangeEnabledProperty(txtOtherAmt, (Not Me.State.ViewOnly) And True)
                Me.ChangeEnabledProperty(txtOtherDesc, (Not Me.State.ViewOnly) And True)
                Me.ChangeEnabledProperty(txtDisposition, (Not Me.State.ViewOnly) And True)
                Me.ChangeEnabledProperty(txtDiagnostics, (Not Me.State.ViewOnly) And True)
                Me.ChangeEnabledProperty(txtSubTotal, False)
                Me.ChangeEnabledProperty(txtTotalTaxAmount, False)
                Me.ChangeEnabledProperty(txtTotal, False)
                Me.ChangeEnabledProperty(txtGrandTotal, False)
                'Me.InvoiceMethod.Value = ClaimInvoice.INVOICE_METHOD_DETAIL
                ControlMgr.SetVisibleControl(Me, btnPartsInfo_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, False)
                ControlMgr.SetVisibleControl(Me, trGrandTotal, True)
                Me.State.OverrideShowGrandTotal = True
            Else
                Me.ChangeEnabledProperty(txtLabor, False)
                Me.ChangeEnabledProperty(txtParts, False)
                Me.ChangeEnabledProperty(txtServiceCharge, False)
                Me.ChangeEnabledProperty(txtTripAmt, False)
                Me.ChangeEnabledProperty(txtOtherAmt, False)
                Me.ChangeEnabledProperty(txtOtherDesc, False)
                Me.ChangeEnabledProperty(txtDisposition, False)
                Me.ChangeEnabledProperty(txtDiagnostics, False)
                Me.ChangeEnabledProperty(txtIvaTax, False)
                Me.ChangeEnabledProperty(txtTotalTaxAmount, False)
                Me.ChangeEnabledProperty(txtGrandTotal, False)
                Me.ChangeEnabledProperty(txtSubTotal, False)
                If Me.State.AuthDetailEnabled And Me.State.ClaimInvoiceBO.Invoiceable.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
                    Me.ChangeEnabledProperty(txtTotal, False)
                Else
                    Me.ChangeEnabledProperty(txtTotal, (Not Me.State.ViewOnly) And True)
                End If

                ControlMgr.SetVisibleControl(Me, btnPartsInfo_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, False)
                If oCompany.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) OrElse Not Me.State.ClaimInvoiceBO.IsIvaResponsibleFlag Then
                    ControlMgr.SetVisibleControl(Me, trGrandTotal, True)
                    Me.State.OverrideShowGrandTotal = True
                Else
                    ControlMgr.SetVisibleControl(Me, trGrandTotal, False)
                End If

                'Me.InvoiceMethod.Value = ClaimInvoice.INVOICE_METHOD_TOTAL
            End If

            ' as per the partsinfo specs , parts info needs to be visible only for inv method = 1 and repair claims.
            If Me.State.ClaimInvoiceBO.Invoiceable.ClaimActivityId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT)) Then
                ControlMgr.SetVisibleControl(Me, btnReplacement_WRITE, (Not Me.State.ViewOnly) And True)
                If Me.IsPostBack Then
                    Me.LabelRepairDate.Text = "*" + TranslationBase.TranslateLabelOrMessage("REPLACEMENT_DATE") + ":"
                Else
                    Me.LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT_DATE") + ":"
                End If

                Me.State.PartsInfoViewed = True
                'ControlMgr.SetVisibleControl(Me, Me.LabelRepairCode, False)
                'ControlMgr.SetVisibleControl(Me, Me.cboRepairCode, False)
                'REQ-786 Display repair code with only delivery fee option for pending replacement claims
                Dim dvRepairCode As DataView = LookupListNew.GetRepairCodeLookupList(oCompany.CompanyGroupId)
                If (dvRepairCode.Count > 0) Then
                    dvRepairCode.RowFilter = " code = '" + Codes.REPAIR_CODE__DELIVERY_FEE_NO_DEDUCTIBLE + "'"
                End If
                If Not IsPostBack Then
                    Me.BindListControlToDataView(Me.cboRepairCode, dvRepairCode)
                End If
                'REQ-786 End

                If (dvRepairCode.Count > 0) Then
                    If (GetGuidFromString(Me.cboRepairCode.SelectedValue) = New Guid(CType(dvRepairCode(0).Item("id"), Byte()))) Then
                        btnReplacement_WRITE.Style.Add("display", "none")
                    End If
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnReplacement_WRITE, False)
                If Me.IsPostBack Then
                    Me.LabelRepairDate.Text = "*" + TranslationBase.TranslateLabelOrMessage("REPAIR_DATE") + ":"
                Else
                    Me.LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("REPAIR_DATE") + ":"
                End If
                ControlMgr.SetVisibleControl(Me, Me.LabelRepairCode, True And Not Me.State.isForClaimPayAdjust)
                ControlMgr.SetVisibleControl(Me, Me.cboRepairCode, True And Not Me.State.isForClaimPayAdjust)
                If oInvoiceMethodId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_INVOICE_METHOD, ClaimInvoice.INVOICE_METHOD_DETAIL)) Then
                    'its always viewed in this case...
                    If Me.State.AuthDetailEnabled And Me.State.ClaimInvoiceBO.Invoiceable.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK Then
                        ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, (Not Me.State.ViewOnly))
                    End If

                    Select Case Me.State.ClaimInvoiceBO.Invoiceable.MethodOfRepairCode
                        Case Codes.METHOD_OF_REPAIR__CARRY_IN, Codes.METHOD_OF_REPAIR__AT_HOME, Codes.METHOD_OF_REPAIR__SEND_IN, Codes.METHOD_OF_REPAIR__PICK_UP
                            ControlMgr.SetVisibleControl(Me, btnPartsInfo_WRITE, (Not Me.State.ViewOnly))
                    End Select
                Else
                    ControlMgr.SetVisibleControl(Me, btnPartsInfo_WRITE, False)
                    ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, False)
                    Me.State.PartsInfoViewed = True
                End If

            End If

            'ControlMgr.SetVisibleControl(Me, PerceptionPanel, False)
            EnableDisablePerceptionFields(False, Me.State.OverrideShowGrandTotal)
            ControlMgr.SetVisibleControl(Me, cboRegionDropID, False)
            ControlMgr.SetVisibleControl(Me, lblRigion, False)

            If (Me.oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "N")) Then
                trDeductibleAmount.Visible = False
                trDeductibleTaxAmount.Visible = False
            End If

            If Me.State.AuthDetailEnabled AndAlso Not Me.State.AuthDetailExist Then
                ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, Not Me.State.ViewOnly)
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)

            Else
                ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, (Not Me.State.ViewOnly) And Me.State.AuthDetailEnabled)
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, (Not Me.State.ViewOnly))
                If Me.State.ClaimInvoiceBO.isTaxTypeInvoice() Then
                    Dim oClaim As Claim
                    oClaim = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.ClaimInvoiceBO.ClaimId)
                    If Not oClaim.ServiceCenterId.Equals(Guid.Empty) Then
                        Dim svcCenter As ServiceCenter = New ServiceCenter(oClaim.ServiceCenterId)
                        If svcCenter.IvaResponsibleFlag Then
                            'ControlMgr.SetVisibleControl(Me, PerceptionPanel, True)
                            EnableDisablePerceptionFields(True, Me.State.OverrideShowGrandTotal)
                            Me.ChangeEnabledProperty(Me.txtPerceptionIva, True)
                            Me.ChangeEnabledProperty(Me.txtPerceptionIIBB, True)
                            ControlMgr.SetVisibleControl(Me, cboRegionDropID, True)
                            ControlMgr.SetVisibleControl(Me, lblRigion, True)
                            trIVA_Amount.Visible = True
                            trTotalTax_Amount.Visible = False
                        Else
                            'ControlMgr.SetVisibleControl(Me, PerceptionPanel, False)
                            EnableDisablePerceptionFields(False, Me.State.OverrideShowGrandTotal)
                            ControlMgr.SetVisibleControl(Me, cboRegionDropID, False)
                            ControlMgr.SetVisibleControl(Me, lblRigion, False)
                            trIVA_Amount.Visible = False
                            trTotalTax_Amount.Visible = True
                        End If
                    End If
                End If
            End If


            ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonLoanerReturnedDate, (Not Me.State.ViewOnly) And (Not Me.State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty)), True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonRepairDate, (Not Me.State.ViewOnly) And True, True)

            ControlMgr.SetVisibleControl(Me, LabelLoanerReturnedDate, (Not Me.State.ViewOnly) And (Not Me.State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty)))
            ControlMgr.SetVisibleControl(Me, txtLoanerReturnedDate, (Not Me.State.ViewOnly) And (Not Me.State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty)))

            ControlMgr.SetVisibleControl(Me, Me.txtAcctStatusDate, False)
            ControlMgr.SetVisibleControl(Me, Me.txtAcctStatusCode, False)
            ControlMgr.SetVisibleControl(Me, Me.txtboxTrackingNumber, False)

            If Me.State.ViewOnly Then
                Me.ChangeEnabledProperty(Me.cboPayeeSelector, False)
                ControlMgr.SetVisibleControl(Me, Me.txtRepairDate, False) ' dont show in viewonly
                ControlMgr.SetVisibleControl(Me, Me.cboRepairCode, False) ' dont show in viewonly

                ControlMgr.SetVisibleControl(Me, Me.txtAcctStatusDate, True) ' felita passback
                ControlMgr.SetVisibleControl(Me, Me.txtAcctStatusCode, True)  ' felita passback
                ControlMgr.SetVisibleControl(Me, Me.txtboxTrackingNumber, True) ' felita passback
                Me.LabelRepairDate.Text = TranslationBase.TranslateLabelOrMessage("ACCT_STATUS_DATE") + ":"
                Me.LabelRepairCode.Text = TranslationBase.TranslateLabelOrMessage("ACCT_STATUS") + ":"
                Me.LabelPickUpDate.Text = TranslationBase.TranslateLabelOrMessage("ACCT_TRACK_NUMBER") + ":"

                Me.ChangeEnabledProperty(Me.txtInvoiceNumber, False)
                Me.ChangeEnabledProperty(Me.txtInvoiceDate, False)
                ControlMgr.SetVisibleControl(Me, Me.ImageButtonInvoiceDate, False)

                Me.ChangeEnabledProperty(Me.cboCauseOfLossID, False)
                ControlMgr.SetVisibleControl(Me, Me.btnUndo_Write, False)
                ControlMgr.SetVisibleControl(Me, Me.TextboxPickupDate, False) ' dont show in viewonly
                Me.ChangeEnabledProperty(Me.ImageButtonPickupDate, False)

                Me.ChangeEnabledProperty(Me.cboDocumentTypeId, False)
                Me.ChangeEnabledProperty(Me.moTaxIdText, False)
                Me.ChangeEnabledProperty(Me.txtAcctStatusDate, False)
                Me.ChangeEnabledProperty(Me.txtAcctStatusCode, False)
                Me.ChangeEnabledProperty(Me.txtboxTrackingNumber, False)

            End If

            If isForClaimPayAdjustments() Then
                ControlMgr.SetVisibleControl(Me, Me.LabelCauseOfLoss, False)
                ControlMgr.SetVisibleControl(Me, Me.cboCauseOfLossID, False)
                ControlMgr.SetVisibleControl(Me, Me.LabelRepairDate, False)
                ControlMgr.SetVisibleControl(Me, Me.txtRepairDate, False)
                ControlMgr.SetVisibleControl(Me, Me.ImageButtonRepairDate, False)
                ControlMgr.SetVisibleControl(Me, Me.TextboxPickupDate, False)
                ControlMgr.SetVisibleControl(Me, Me.ImageButtonPickupDate, False)
            End If

            Me.ChangeEnabledProperty(Me.txtInvoiceNumber, (Not Me.State.ViewOnly) And True)
            Me.ChangeEnabledProperty(Me.txtInvoiceDate, (Not Me.State.ViewOnly) And True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonInvoiceDate, (Not Me.State.ViewOnly) And True, True)

            'WR 754196, Serial Number maintenance:
            Me.ChangeEnabledProperty(Me.TextSerialNumber, (Not Me.State.ViewOnly) And True)

            'Pickup date: do not display if replacement and/or interface claim
            Dim blnCanDisplayVisitAndPickUpDates As Boolean
            If Not Me.State.ClaimBO Is Nothing Then
                blnCanDisplayVisitAndPickUpDates = Me.State.ClaimBO.CanDisplayVisitAndPickUpDates
            End If
            If Not Me.State.ClaimBO Is Nothing AndAlso Not Me.State.ClaimBO.LoanerTaken And Not Me.State.isForClaimPayAdjust Then
                ControlMgr.SetVisibleForControlFamily(Me, Me.ImageButtonPickupDate, blnCanDisplayVisitAndPickUpDates And Not Me.State.ViewOnly, True)
                Me.SetEnabledForControlFamily(Me.ImageButtonPickupDate, blnCanDisplayVisitAndPickUpDates And Not Me.State.ViewOnly, True)
                ControlMgr.SetVisibleControl(Me, TextboxPickupDate, blnCanDisplayVisitAndPickUpDates And Not Me.State.isForClaimPayAdjust)
                ControlMgr.SetVisibleControl(Me, LabelPickUpDate, blnCanDisplayVisitAndPickUpDates And Not Me.State.isForClaimPayAdjust)
            Else
                ControlMgr.SetVisibleControl(Me, TextboxPickupDate, False)
                ControlMgr.SetVisibleControl(Me, LabelPickUpDate, False)
                ControlMgr.SetVisibleControl(Me, Me.ImageButtonPickupDate, False)
            End If
            If Me.State.ViewOnly Then
                ControlMgr.SetVisibleControl(Me, Me.LabelPickUpDate, True)
                ControlMgr.SetVisibleControl(Me, Me.TextboxPickupDate, False)
            End If

            If oCompany.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) Then
                ControlMgr.SetVisibleControl(Me, btnAuthDetail_WRITE, True)
            End If

            Dim oCompaniesDv As DataView, oUser As New User
            oCompaniesDv = oUser.GetUserCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
            oCompaniesDv.RowFilter = "code in ('ABA','VBA','SBA')"
            If oCompaniesDv.Count > 0 Then
                ControlMgr.SetVisibleControl(Me, Me.LabelInvDateAsterisk, True)
            Else
                ControlMgr.SetVisibleControl(Me, Me.LabelInvDateAsterisk, False)
            End If

            DisableButtonsForClaimSystem()
            ''''''REQ-5565 check for claim  whether it is part of pre-invoice process
            If (Me.State.ClaimBO.Company.UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
                Dim preInv As New PreInvoiceDetails
                Dim count As Integer

                count = preInv.CheckClaimInPreInvoice(Me.State.ClaimBO.Claim_Id)
                If (count > 0) Then
                    ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, False)
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.MessageController.AddInformation(TranslationBase.TranslateLabelOrMessage("CLAIM_CANNOT_PAID_INDIVIDUALLY"), False)
                Else
                    ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, True)
                End If
            Else
                ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, True)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Try
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "CustomerName", Me.LabelCustomerName)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "ServiceCenterName", Me.LabelBillingSvcCenter)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "PayeeName", Me.LabelPayee)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "SvcControlNumber", Me.LabelInvNumber)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "RepairDate", Me.LabelRepairDate)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "InvoiceDate", Me.LabelInvoiceDate)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "LoanerReturnedDate", Me.LabelLoanerReturnedDate)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "LaborAmt", Me.LabelLabor)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "PartAmount", Me.LabelParts)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "ServiceCharge", Me.LabelSvcCharge)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "TripAmount", Me.LabelTripAmount)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "ShippingAmount", Me.LabelShipping)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "Amount", Me.LabelTotal)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "RepairCodeId", Me.LabelRepairCode)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "CauseOfLossID", Me.LabelCauseOfLoss)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "RegionID", Me.lblRigion)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "PerceptionIVA", Me.lblPerception_Iva)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "PerceptionIIBB", Me.lblPerception_IIBB)
            If Not Me.State.isForClaimPayAdjust Then Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "PickUpDate", Me.LabelPickUpDate)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "PaymentMethodID", Me.moPaymentMethodLabel)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "DocumentType", Me.moDocumentTypeLabel)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "TaxId", Me.moTaxIdLabel)
            Me.BindBOPropertyToLabel(Me.State.DisbursementBO, "VendorRegionDesc", Me.lblRigion)
            Me.BindBOPropertyToLabel(Me.State.ClaimInvoiceBO, "SalvageAmt", Me.LabelSalvageAmt)
            '''''Me.BindBOPropertyToNonLabeled(Me.State.ClaimInvoiceBO, "TaxId")

            Me.ClearGridHeadersAndLabelsErrSign()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateDropdowns()
        Try
            If Me.State.ClaimBO Is Nothing Then
                Me.State.ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(Me.State.ClaimInvoiceBO.ClaimId)
            End If

            'Me.BindListControlToDataView(Me.cboCauseOfLossID, LookupListNew.GetCauseOfLossByCoverageTypeLookupList(Authentication.LangId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, Me.State.ClaimBO.CoverageTypeId))

            Dim listcontextForCauseOfLoss As ListContext = New ListContext()
            listcontextForCauseOfLoss.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            listcontextForCauseOfLoss.CoverageTypeId = Me.State.ClaimBO.CoverageTypeId
            listcontextForCauseOfLoss.DealerId = Me.oCertificate.DealerId
            listcontextForCauseOfLoss.ProductCode = Me.oCertificate.ProductCode
            listcontextForCauseOfLoss.LanguageId = Authentication.LangId
            Me.cboCauseOfLossID.Populate(CommonConfigManager.Current.ListManager.GetList("CauseOfLossByCoverageTypeAndSplSvcLookupList", , listcontextForCauseOfLoss), New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True
                                                })

            Me.BindListControlToDataView(Me.cboRepairCode, LookupListNew.GetRepairCodeLookupList(oCompany.CompanyGroupId))

            Dim listContext As ListContext = New ListContext()
            listContext.CompanyGroupId = oCompany.CompanyGroupId
            Me.cboRepairCode.Populate(CommonConfigManager.Current.ListManager.GetList("RepairCodebyCompanyGroup", , listContext), New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True
                                                })

            'Dim payeeDV As DataView = LookupListNew.GetPayeeLookupList(Authentication.LangId)

            Dim claimServiceCenter As ServiceCenter = New ServiceCenter(Me.State.ClaimInvoiceBO.Invoiceable.ServiceCenterId)
            If claimServiceCenter.MasterCenterId.Equals(Guid.Empty) Then

                'payeeDV.RowFilter = payeeDV.RowFilter & " and code <>'" & ClaimInvoice.PAYEE_OPTION_MASTER_CENTER & "'"
                Dim payeeTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                  If (li.Code <> ClaimInvoice.PAYEE_OPTION_MASTER_CENTER) Then
                                                                                      Return li.Translation
                                                                                  Else
                                                                                      Return Nothing
                                                                                  End If
                                                                              End Function
                Me.cboPayeeSelector.Populate(CommonConfigManager.Current.ListManager.GetList("PAYEE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True,
                                                    .TextFunc = payeeTextFunc
                                                 })


            End If

            'Commented the code for DEF-825
            'Filter out the master center - changes for Req 124
            'payeeDV.RowFilter = payeeDV.RowFilter & " and code <>'" & ClaimInvoice.PAYEE_OPTION_MASTER_CENTER & "'"

            If Me.State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                'payeeDV.RowFilter = payeeDV.RowFilter & " and code <>'" & ClaimInvoice.PAYEE_OPTION_LOANER_CENTER & "'"
                Dim payeeTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                  If (li.Code <> ClaimInvoice.PAYEE_OPTION_LOANER_CENTER) Then
                                                                                      Return li.Translation
                                                                                  Else
                                                                                      Return Nothing
                                                                                  End If
                                                                              End Function
                Me.cboPayeeSelector.Populate(CommonConfigManager.Current.ListManager.GetList("PAYEE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True,
                                                    .TextFunc = payeeTextFunc
                                                 })
            End If

            'Me.BindListControlToDataView(Me.cboPayeeSelector, payeeDV, "DESCRIPTION", , False) 'AA

            Me.cboPayeeSelector.Populate(CommonConfigManager.Current.ListManager.GetList("PAYEE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True
                                                })

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
            Me.cboDocumentTypeId.Populate(CommonConfigManager.Current.ListManager.GetList("DTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Public Sub ExcludeFromDropdown(value As String)
        Dim hasGiftCardAttriute As Boolean = False

        If (Not PaymentMethodDrop Is Nothing AndAlso PaymentMethodDrop.Items.Count > 0) Then

            If (value <> String.Empty) Then

                If (Me.oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = value).Count > 0) Then
                    hasGiftCardAttriute = True
                End If

                Dim listItem As ListItem = PaymentMethodDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode("PMTHD", Codes.PAYMENT_METHOD__DARTY_GIFT_CARD))

                'Exclude Darty gift card from the dealers with no attribute or dealer with the attribute and payee not Customer
                If (Not listItem Is Nothing) Then
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
            If Me.State.AuthDetailEnabled Then
                Me.State.ClaimAuthDetailBO = New ClaimAuthDetail(Me.State.ClaimBO.Id, True)
                If Not Me.State.ClaimAuthDetailBO Is Nothing Then
                    ' There is a previously record created for this claim, the user does not have to add the detail before paying claim; 
                    ' Pay Claim button to be enabled
                    Me.State.AuthDetailExist = True
                    Me.State.PartsInfoBO = New PartsInfo()
                    Dim partInfoPartAmount As DecimalType = Me.State.PartsInfoBO.getTotalCost(Me.State.ClaimBO.Id)
                    If partInfoPartAmount.Value > 0 Then Me.State.ClaimAuthDetailBO.PartAmount = partInfoPartAmount

                Else
                    ' No previously record created for this claim, the user must add the detail before paying claim; 
                    ' Pay Claim button to be disabled
                    Me.State.AuthDetailExist = False
                End If
            End If
        Catch ex As DataNotFoundException
            ' No previously record created for this claim, the user must add the detail before paying claim; 
            ' Pay Claim button to be disabled
            Me.State.AuthDetailExist = False
            Me.State.ClaimAuthDetailBO = Nothing
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub UpdateClaimInvoiceBOWithClaimAuthDetail()
        Try
            If Me.State.AuthDetailEnabled Then
                If Not Me.State.ClaimAuthDetailBO Is Nothing Then
                    With Me.State.ClaimInvoiceBO
                        .LaborAmt = Me.State.ClaimAuthDetailBO.LaborAmount
                        .PartAmount = Me.State.ClaimAuthDetailBO.PartAmount
                        .ServiceCharge = Me.State.ClaimAuthDetailBO.ServiceCharge
                        .TripAmount = Me.State.ClaimAuthDetailBO.TripAmount
                        .OtherExplanation = Me.State.ClaimAuthDetailBO.OtherExplanation
                        .OtherAmount = Me.State.ClaimAuthDetailBO.OtherAmount
                        .ShippingAmount = Me.State.ClaimAuthDetailBO.ShippingAmount
                        .DiagnosticsAmount = Me.State.ClaimAuthDetailBO.DiagnosticsAmount
                        .DispositionAmount = Me.State.ClaimAuthDetailBO.DispositionAmount
                        .Amount = Me.GetAuthDetailTotal + Me.State.ClaimAuthDetailBO.TotalTaxAmount
                    End With
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Function GetAuthDetailTotal() As Decimal
        Dim amount As Decimal = 0
        If Not Me.State.ClaimAuthDetailBO.LaborAmount Is Nothing Then amount += Me.State.ClaimAuthDetailBO.LaborAmount.Value
        If Not Me.State.ClaimAuthDetailBO.PartAmount Is Nothing Then amount += Me.State.ClaimAuthDetailBO.PartAmount.Value
        If Not Me.State.ClaimAuthDetailBO.ServiceCharge Is Nothing Then amount += Me.State.ClaimAuthDetailBO.ServiceCharge.Value
        If Not Me.State.ClaimAuthDetailBO.TripAmount Is Nothing Then amount += Me.State.ClaimAuthDetailBO.TripAmount.Value
        If Not Me.State.ClaimAuthDetailBO.ShippingAmount Is Nothing Then amount += Me.State.ClaimAuthDetailBO.ShippingAmount.Value
        If Not Me.State.ClaimAuthDetailBO.OtherAmount Is Nothing Then amount += Me.State.ClaimAuthDetailBO.OtherAmount.Value
        If Not Me.State.ClaimAuthDetailBO.DiagnosticsAmount Is Nothing Then amount += Me.State.ClaimAuthDetailBO.DiagnosticsAmount.Value
        If Not Me.State.ClaimAuthDetailBO.DispositionAmount Is Nothing Then amount += Me.State.ClaimAuthDetailBO.DispositionAmount.Value


        Return amount
    End Function

    Protected Sub PopulateFormFromBOs()

        Try

            Dim claimServiceCenter As ServiceCenter = New ServiceCenter(Me.State.ClaimInvoiceBO.Invoiceable.ServiceCenterId)
            Dim GrandTotal As Decimal
            Me.UpdateClaimInvoiceBOWithClaimAuthDetail()
            'If Not claimServiceCenter.WithholdingRate Is Nothing Then Me.State.WithholdingRate = claimServiceCenter.WithholdingRate.Value * (-1)

            With Me.State.ClaimInvoiceBO

                Me.PopulateControlFromBOProperty(Me.TextboxClaimNumber, .ClaimNumber)
                Me.PopulateControlFromBOProperty(Me.TextboxCertificateNumber, .CertificateNumber)
                Me.PopulateControlFromBOProperty(Me.TextboxRiskType, .RiskType)
                Me.PopulateControlFromBOProperty(Me.TextManufacturer, .Manufacturer)
                Me.PopulateControlFromBOProperty(Me.TextModel, .Model)
                Me.PopulateControlFromBOProperty(Me.txtPerceptionIva, .PerceptionIVA)
                Me.PopulateControlFromBOProperty(Me.txtPerceptionIIBB, .PerceptionIIBB)

                If Me.State.ClaimInvoiceBO.SerialNumberTempContainer Is Nothing Then
                    Me.PopulateControlFromBOProperty(Me.TextSerialNumber, .SerialNumber)
                Else
                    Me.PopulateControlFromBOProperty(Me.TextSerialNumber, .SerialNumberTempContainer)
                End If

                Me.PopulateControlFromBOProperty(Me.CustomerAddressLabel, .CustomerName & Environment.NewLine & (New Address(Me.State.ClaimInvoiceBO.CustomerAddressID)).MailingAddressLabel)
                Me.PopulateControlFromBOProperty(Me.ServiceCenterAddressLabel, .ServiceCenterName & Environment.NewLine & (New Address(Me.State.ClaimInvoiceBO.ServiceCenterAddressID)).MailingAddressLabel)
                Me.PopulateControlFromBOProperty(Me.txtInvoiceNumber, .SvcControlNumber)
                Me.PopulateControlFromPropertyName(Me.State.ClaimInvoiceBO, Me.cboCauseOfLossID, "CauseOfLossID")
                Me.PopulateControlFromBOProperty(Me.cboRepairCode, .RepairCodeId)
                Me.PopulateControlFromBOProperty(Me.txtLoanerReturnedDate, .LoanerReturnedDate)
                Me.PopulateControlFromBOProperty(Me.txtRepairDate, .RepairDate)
                Me.PopulateControlFromBOProperty(Me.txtInvoiceDate, .InvoiceDate)
                Me.PopulateControlFromBOProperty(Me.txtAcctStatusDate, Me.State.DisbursementBO.StatusDate)
                Me.PopulateControlFromBOProperty(Me.txtboxTrackingNumber, Me.State.DisbursementBO.TrackingNumber)
                Me.PopulateControlFromBOProperty(Me.txtAcctStatusCode, LookupListNew.GetDescriptionFromCode(LookupListNew.LK_ACCTSTATUS, Me.State.DisbursementBO.AcctStatus))

                'Me.State.ClaimInvoiceBO.CalculateAmounts()
                'check if the user is coming from the parts info page (i.e previous page was Parts info page), if yes, then refresh the parts info value if it is changed
                If Not Me.NavController Is Nothing AndAlso Not Me.NavController.PrevNavState Is Nothing AndAlso Not Me.NavController.PrevNavState.Name Is Nothing AndAlso Me.NavController.PrevNavState.Name = "PARTS_INFO" Then
                    'get the amount entered in parts info table
                    Dim PartsInfoValue As DecimalType = PartsInfo.getTotalCost(Me.State.ClaimBO.Id).Value
                    'Check if the parts amount has been changed, if yes, the refresh the parts amount value
                    If Me.State.ClaimInvoiceBO.PartAmount <> PartsInfoValue Then
                        'set it in part amount value
                        Me.State.ClaimInvoiceBO.PartAmount = PartsInfoValue
                        Me.State.ClaimInvoiceBO.CalculateAmounts()
                    End If
                End If


                If Me.State.AuthDetailEnabled AndAlso Me.State.ClaimBO.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__REWORK AndAlso Not Me.State.ClaimAuthDetailBO Is Nothing Then

                    Me.PopulateControlFromBOProperty(Me.txtLabor, Me.State.ClaimAuthDetailBO.LaborAmount)
                    Me.PopulateControlFromBOProperty(Me.txtParts, Me.State.ClaimAuthDetailBO.PartAmount)
                    Me.PopulateControlFromBOProperty(Me.txtServiceCharge, Me.State.ClaimAuthDetailBO.ServiceCharge)
                    Me.PopulateControlFromBOProperty(Me.txtTripAmt, Me.State.ClaimAuthDetailBO.TripAmount)
                    Me.PopulateControlFromBOProperty(Me.txtShipping, Me.State.ClaimAuthDetailBO.ShippingAmount)
                    Me.PopulateControlFromBOProperty(Me.txtOtherDesc, Me.State.ClaimAuthDetailBO.OtherExplanation)
                    Me.PopulateControlFromBOProperty(Me.txtOtherAmt, Me.State.ClaimAuthDetailBO.OtherAmount)
                    Me.PopulateControlFromBOProperty(Me.txtDiagnostics, Me.State.ClaimAuthDetailBO.DiagnosticsAmount)
                    Me.PopulateControlFromBOProperty(Me.txtDisposition, Me.State.ClaimAuthDetailBO.DispositionAmount)
                    'Me.PopulateControlFromBOProperty(Me.txtIvaTax, Me.State.ClaimAuthDetailBO.IvaAmount)
                    Dim authdetailTtl As Decimal = Me.GetAuthDetailTotal
                    Dim dedAmt As Decimal
                    If Me.State.ClaimBO.Deductible Is Nothing Then
                        dedAmt = 0
                    Else
                        dedAmt = State.ClaimBO.Deductible.Value
                    End If

                    Me.PopulateControlFromBOProperty(Me.txtTotalTaxAmount, Me.State.ClaimInvoiceBO.TotalTaxAmount.Value)

                    Dim totAmount As Decimal = authdetailTtl + Me.State.ClaimInvoiceBO.TotalTaxAmount.Value - dedAmt
                    Me.PopulateControlFromBOProperty(Me.txtTotal, totAmount)
                    If Me.State.ClaimInvoiceBO.WithholdingAmount Is Nothing Then
                        Me.State.ClaimInvoiceBO.WithholdingAmount = New DecimalType(0)
                    Else
                        Me.PopulateControlFromBOProperty(Me.txtTotalWithholdingAmount, .WithholdingAmount)
                    End If
                    GrandTotal = totAmount + Me.State.ClaimInvoiceBO.WithholdingAmount.Value
                    Me.txtGrandTotal.Text = System.Convert.ToString(GrandTotal)
                    hdGrandTotalAmt.Value = GrandTotal

                    Me.PopulateControlFromBOProperty(Me.txtSubTotal, authdetailTtl)
                    Me.hdSubTotalAmt.Value = authdetailTtl
                    hdTotal.Value = Me.txtTotal.Text
                    hdOtherAmt.Value = Me.txtOtherAmt.Text
                    hdDispositionAmt.Value = Me.txtDisposition.Text
                    hdDiagnosticsAmt.Value = Me.txtDiagnostics.Text
                    hdTotalTaxAmount.Value = Me.txtTotalTaxAmount.Text
                    hdpaymenttocustomer.Value = Me.txtPaymenttoCustomer.Text

                Else
                    Me.PopulateControlFromBOProperty(Me.txtLabor, .LaborAmt)
                    Me.PopulateControlFromBOProperty(Me.txtParts, .PartAmount)
                    Me.PopulateControlFromBOProperty(Me.txtServiceCharge, .ServiceCharge)
                    Me.PopulateControlFromBOProperty(Me.txtTripAmt, .TripAmount)
                    Me.PopulateControlFromBOProperty(Me.txtShipping, .ShippingAmount)
                    Me.PopulateControlFromBOProperty(Me.txtOtherDesc, .OtherExplanation)
                    Me.PopulateControlFromBOProperty(Me.txtOtherAmt, .OtherAmount)
                    Me.PopulateControlFromBOProperty(Me.txtDiagnostics, .DiagnosticsAmount)
                    Me.PopulateControlFromBOProperty(Me.txtDisposition, .DispositionAmount)
                    'Me.PopulateControlFromBOProperty(Me.txtIvaTax, .IvaAmount)
                    Me.PopulateControlFromBOProperty(Me.txtDeductibleTaxAmount, .DeductibleTaxAmount)
                    Me.PopulateControlFromBOProperty(Me.txtTotalWithholdingAmount, .WithholdingAmount)
                    Me.PopulateControlFromBOProperty(Me.txtTotal, .Amount)
                    Me.PopulateControlFromBOProperty(Me.txtSubTotal, CalculateSubTotal())
                    Me.PopulateControlFromBOProperty(Me.txtTotalTaxAmount, Me.State.ClaimInvoiceBO.TotalTaxAmount.Value)
                    Me.PopulateControlFromBOProperty(Me.txtPaymenttoCustomer, .PaytocustomerAmount)
                    hdTotal.Value = Me.txtTotal.Text
                    hdOtherAmt.Value = Me.txtOtherAmt.Text
                    hdDispositionAmt.Value = Me.txtDisposition.Text
                    hdDiagnosticsAmt.Value = Me.txtDiagnostics.Text
                    hdTotalTaxAmount.Value = Me.txtTotalTaxAmount.Text
                    hdpaymenttocustomer.Value = Me.txtPaymenttoCustomer.Text

                End If

                Dim PerceptionIIBB As Decimal
                Dim PerceptionIVA As Decimal
                Dim TotalAmount As Decimal

                Decimal.TryParse(Me.txtPerceptionIIBB.Text, PerceptionIIBB)
                Decimal.TryParse(Me.txtPerceptionIva.Text, PerceptionIVA)
                Decimal.TryParse(Me.txtTotal.Text, TotalAmount)

                If Me.State.ClaimInvoiceBO.WithholdingAmount Is Nothing Then Me.State.ClaimInvoiceBO.WithholdingAmount = New DecimalType(0D)
                GrandTotal = TotalAmount + State.TotalManualTaxes + PerceptionIIBB + PerceptionIVA + Me.State.ClaimInvoiceBO.WithholdingAmount.Value
                Me.txtGrandTotal.Text = System.Convert.ToString(GrandTotal)
                hdGrandTotalAmt.Value = GrandTotal

                Me.PopulateControlFromBOProperty(Me.txtIvaTax, .IvaAmount)
                Me.PopulateControlFromBOProperty(Me.txtSalvageAmt, .Invoiceable.SalvageAmount)
                'Me.PopulateControlFromBOProperty(Me.txtLiabilityLimit, .Invoiceable.LiabilityLimit)
                Me.PopulateControlFromBOProperty(Me.txtLiabilityLimit, Me.CalculateLiabilityLimit())
                Me.PopulateControlFromBOProperty(Me.txtDeductible, .Invoiceable.Deductible)
                Me.PopulateControlFromBOProperty(Me.txtDiscount, .Invoiceable.DiscountAmount)
                Me.PopulateControlFromBOProperty(Me.txtAuthAmt, .Invoiceable.AuthorizedAmount)
                'Me.PopulateControlFromBOProperty(Me.txtAboveLiability, .Invoiceable.AboveLiability)
                Me.PopulateControlFromBOProperty(Me.txtAboveLiability, Me.AboveLiability())
                Me.PopulateControlFromBOProperty(Me.txtAssurantPays, .Invoiceable.AssurantPays)
                Me.PopulateControlFromBOProperty(Me.txtConsumerPays, .Invoiceable.ConsumerPays)

                If (Me.State.ViewOnly = True) Or (Me.State.ComingFromChildForm = True) Then
                    Me.PopulateControlFromBOProperty(Me.cboPayeeSelector, Me.State.DisbursementBO.PayeeOptionId)
                Else
                    Dim PayeeOptionId As Guid
                    If claimServiceCenter.PayMaster = True Then
                        PayeeOptionId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, ClaimInvoice.PAYEE_OPTION_MASTER_CENTER)
                        Me.SetSelectedItem(Me.cboPayeeSelector, PayeeOptionId)
                    Else
                        PayeeOptionId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, ClaimInvoice.PAYEE_OPTION_SERVICE_CENTER)
                        Me.SetSelectedItem(Me.cboPayeeSelector, PayeeOptionId)
                    End If
                End If
                Me.PayeeChanged(GetSelectedItem(cboPayeeSelector), False)

                Me.PopulateControlFromBOProperty(Me.txtPayeeName, Me.State.DisbursementBO.Payee)

                'If Me.State.ViewOnly And Me.PaymentMethodDrop.Visible Then
                If Me.PaymentMethodDrop.Visible Then
                    Me.PopulateControlFromBOProperty(Me.PaymentMethodDrop, Me.State.ClaimInvoiceBO.PaymentMethodID)
                    If Me.State.ViewOnly Then
                        Me.ChangeEnabledProperty(Me.PaymentMethodDrop, False)
                    Else
                        Me.ChangeEnabledProperty(Me.PaymentMethodDrop, True)
                    End If
                    Me.PaymentMethodChanged(Me.State.ClaimInvoiceBO.PaymentMethodID, Me.State.DisbursementBO.PayeeOptionId, False)
                End If

                If Me.State.ViewOnly And Me.PaymentMethodDrop.Visible Then
                    If (Me.State.DisbursementBO.PayeeOptionId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, ClaimInvoice.PAYEE_OPTION_CUSTOMER) Or
                        Me.State.DisbursementBO.PayeeOptionId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, ClaimInvoice.PAYEE_OPTION_OTHER) AndAlso
                        (Me.State.ClaimInvoiceBO.PaymentMethodID = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYMENTMETHOD, "CTT"))) Then
                        Me.PayeeBankInfo.DisplayTaxId()
                    Else
                        Me.PayeeBankInfo.HideTaxId()
                    End If
                End If

                If (Me.oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
                    PayDeductible.Value = "Y"
                    RemainingDeductible.Value = (.Invoiceable.Deductible.Value - .AlreadyPaidDeductible.Value).ToString()
                    Me.PopulateControlFromBOProperty(Me.txtDeductibleAmount, .DeductibleAmount)
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

                If Me.State.ClaimInvoiceBO.isTaxTypeInvoice() Then
                    If Not claimServiceCenter Is Nothing Then
                        Me.PopulateControlFromBOProperty(Me.cboRegionDropID, claimServiceCenter.Address.RegionId)
                    End If
                End If

                If claimServiceCenter.IvaResponsibleFlag Then
                    ServiceCenterIvaResponsible.Value = "Y"
                Else
                    ServiceCenterIvaResponsible.Value = "N"
                End If

                LoanerCenterIvaResponsible.Value = "N"
                If Not Me.State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                    Dim loanerCenter As ServiceCenter = New ServiceCenter(Me.State.ClaimInvoiceBO.Invoiceable.LoanerCenterId)
                    If loanerCenter.IvaResponsibleFlag Then
                        LoanerCenterIvaResponsible.Value = "Y"
                    End If
                End If

                If (ServiceCenterIvaResponsible.Value = "Y") Or (MasterCenterIvaResponsible.Value = "Y") Or (LoanerCenterIvaResponsible.Value = "Y") Then
                    Me.TaxRate.Value = .TaxRate.Value.ToString()
                    Me.DeductibleTaxRate.Value = .DeductibleTaxRate.Value.ToString()
                    Me.PopulateControlFromBOProperty(Me.txtDeductibleTaxAmount, .DeductibleTaxAmount)
                End If

                'Pickup date: do not display if replacement and/or interface claim
                'Interface claim = Not claim.Source.Equals(String.Empty)
                If Not Me.State.ClaimBO Is Nothing AndAlso (Me.State.ClaimBO.CanDisplayVisitAndPickUpDates And Not Me.State.ClaimBO.LoanerTaken) And Not Me.State.isForClaimPayAdjust Then
                    Me.PopulateControlFromBOProperty(Me.TextboxPickupDate, Me.State.ClaimBO.PickUpDate)
                End If
                Me.hdAlreadyPaid.Value = .AlreadyPaid.Value.ToString()
                Me.hdAssurantPays.Value = .Invoiceable.AssurantPays.Value.ToString()
                Me.hdSalvageAmt.Value = .Invoiceable.SalvageAmount.Value.ToString()
                Me.hdDeductibleAmt.Value = .Invoiceable.Deductible.Value.ToString()

                If Not LookupListNew.GetIdFromCode(LookupListNew.GetRepairCodeLookupList(oCompany.CompanyGroupId), Codes.REPAIR_CODE__DELIVERY_FEE_NO_DEDUCTIBLE) = Guid.Empty Then
                    hdDeliveryFeeOnly.Value = LookupListNew.GetIdFromCode(LookupListNew.GetRepairCodeLookupList(oCompany.CompanyGroupId), Codes.REPAIR_CODE__DELIVERY_FEE_NO_DEDUCTIBLE).ToString
                End If

                Me.PopulateControlFromBOProperty(Me.txtAlreadyPaid, .AlreadyPaid)

                Me.PopulateControlFromBOProperty(Me.txtRemainingAmt, .RemainingAmount)

                'DEF-394 - ALR -- Changed to populate payment method based on service center value
                If Me.State.ClaimInvoiceBO.PaymentMethodID.Equals(Guid.Empty) Then
                    If Not claimServiceCenter.PaymentMethodId.Equals(Guid.Empty) Then
                        Me.State.ClaimInvoiceBO.PaymentMethodID = claimServiceCenter.PaymentMethodId
                        Me.State.ClaimInvoiceBO.PaymentMethodCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.State.ClaimInvoiceBO.PaymentMethodID)
                        Me.State.DisbursementBO.PaymentMethod = Me.State.ClaimInvoiceBO.PaymentMethodCode
                        If Not Me.PaymentMethodDrop.Items.FindByText(Me.State.ClaimInvoiceBO.PaymentMethodCode) Is Nothing Then
                            Me.PopulateControlFromBOProperty(Me.PaymentMethodDrop, Me.State.ClaimInvoiceBO.PaymentMethodID)
                        End If
                    End If
                End If

                If Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR_REPLACEMENT Or Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                    Me.hdClaimMethodOfRepair.Value = "RPL"
                    Me.LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPLACEMENT")
                Else
                    Me.hdClaimMethodOfRepair.Value = "RPR"
                    Me.LabelTaxType.Text = TranslationBase.TranslateLabelOrMessage("REPAIR")
                End If
                LoadClaimTaxRates()

            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub LoadClaimTaxRates()

        If Me.State.ClaimTaxRatesData Is Nothing Then
            Dim RegionId As Guid
            If Me.State.ClaimInvoiceBO.PayeeAddress Is Nothing Then
                Dim addressObj As New Address(Me.State.ClaimInvoiceBO.ServiceCenterAddressID)
                RegionId = addressObj.RegionId
            Else
                RegionId = Me.State.ClaimInvoiceBO.PayeeAddress.RegionId
            End If
            Me.State.ClaimTaxRatesData = Me.State.ClaimInvoiceBO.ClaimTaxRatesData(RegionId, Me.hdClaimMethodOfRepair.Value)
        End If

        With Me.State.ClaimTaxRatesData
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

            If Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate <> 0 Then
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

            hdWithholdingRate.Value = Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate

        End With
    End Sub

    Private Sub ComputeAutoTaxes()
        Me.State.ClaimInvoiceBO.TotalTaxAmount = New DecimalType(0D)
        Me.State.ClaimInvoiceBO.WithholdingAmount = New DecimalType(0D)
        Dim tmpAmtSubjectToWithHoding As Decimal = 0

        Try
            Me.txtPartsTax.Text = Me.hdPartsAmt.Value.ToString
            Me.txtPartsTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtParts.Text, Decimal), CType(Me.hdTaxRateClaimParts.Value, Decimal), hdComputeMethodClaimParts.Value)
            If Me.hdApplyWithholdingFlagClaimParts.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding = CType(Me.txtParts.Text, Decimal)
                If Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then Me.CheckBoxPartWithhodling.Checked = True
            End If
        Catch ex As Exception
            Me.txtPartsTax.Text = ""
        End Try

        Try
            Me.txtLaborTax.Text = Me.hdLaborAmt.Value.ToString
            Me.txtLaborTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtLabor.Text, Decimal), CType(Me.hdTaxRateClaimLabor.Value, Decimal), hdComputeMethodClaimLabor.Value)
            If Me.hdApplyWithholdingFlagClaimLabor.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(Me.txtLabor.Text, Decimal)
                If Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then Me.CheckBoxLaborWithhodling.Checked = True
            End If

        Catch ex As Exception
            Me.txtLaborTax.Text = ""
        End Try

        Try
            Me.txtServiceChargeTax.Text = Me.hdServiceChargeAmt.Value.ToString
            Me.txtServiceChargeTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtServiceCharge.Text, Decimal), CType(Me.hdTaxRateClaimService.Value, Decimal), hdComputeMethodClaimService.Value)
            If Me.hdApplyWithholdingFlagClaimService.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(Me.txtServiceCharge.Text, Decimal)
                If Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then Me.CheckBoxServiceWithhodling.Checked = True
            End If
        Catch ex As Exception
            Me.txtServiceChargeTax.Text = ""
        End Try

        Try
            Me.txtTripAmtTax.Text = Me.hdTripAmt.Value.ToString
            Me.txtTripAmtTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtTripAmt.Text, Decimal), CType(Me.hdTaxRateClaimTrip.Value, Decimal), hdComputeMethodClaimTrip.Value)
            If Me.hdApplyWithholdingFlagClaimTrip.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(Me.txtTripAmt.Text, Decimal)
                If Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then Me.CheckBoxTripWithhodling.Checked = True
            End If
        Catch ex As Exception
            Me.txtTripAmtTax.Text = ""
        End Try

        Try
            Me.txtShippingTax.Text = Me.hdShippingAmt.Value.ToString
            Me.txtShippingTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtShipping.Text, Decimal), CType(Me.hdTaxRateClaimShipping.Value, Decimal), hdComputeMethodClaimShipping.Value)
            If Me.hdApplyWithholdingFlagClaimShipping.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(Me.txtShipping.Text, Decimal)
                If Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then Me.CheckBoxShippingWithhodling.Checked = True
            End If
        Catch ex As Exception
            Me.txtShippingTax.Text = ""
        End Try

        Try
            Me.txtDispositionTax.Text = Me.hdDispositionAmt.Value.ToString
            Me.txtDispositionTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtDisposition.Text, Decimal), CType(Me.hdTaxRateClaimDisposition.Value, Decimal), hdComputeMethodClaimDisposition.Value)
            If Me.hdApplyWithholdingFlagClaimDisposition.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(Me.txtDisposition.Text, Decimal)
                If Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then Me.CheckBoxDispositionWithhodling.Checked = True
            End If
        Catch ex As Exception
            Me.txtDispositionTax.Text = ""
        End Try

        Try
            Me.txtDiagnosticsTax.Text = Me.hdDiagnosticsAmt.Value.ToString
            Me.txtDiagnosticsTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtDiagnostics.Text, Decimal), CType(Me.hdTaxRateClaimDiagnostics.Value, Decimal), hdComputeMethodClaimDiagnostics.Value)
            If Me.hdApplyWithholdingFlagClaimDiagnostics.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(Me.txtDiagnostics.Text, Decimal)
                If Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then Me.CheckBoxDiagnosticsWithhodling.Checked = True
            End If
        Catch ex As Exception
            Me.txtDiagnosticsTax.Text = ""
        End Try

        Try
            Me.txtOtherAmt.Text = Me.hdOtherAmt.Value.ToString
            Me.txtOtherTax.Text = computeTaxAmtByComputeMethod(CType(Me.txtOtherAmt.Text, Decimal), CType(Me.hdTaxRateClaimOther.Value, Decimal), hdComputeMethodClaimOther.Value)
            If Me.hdApplyWithholdingFlagClaimParts.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(Me.hdOtherAmt.Value, Decimal)
                If Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then Me.CheckBoxOtherWithhodling.Checked = True
            End If
        Catch ex As Exception
            Me.txtOtherTax.Text = ""
        End Try

        Try
            Me.txtPaymenttoCustomer.Text = Me.hdpaymenttocustomer.Value.ToString
            Me.txtPaymenttoCustomertax.Text = computeTaxAmtByComputeMethod(CType(Me.txtPaymenttoCustomer.Text, Decimal), 0D, 0)
            If Me.hdApplyWithholdingFlagClaimParts.Value.ToUpper.Equals("Y") Then
                tmpAmtSubjectToWithHoding += CType(Me.hdpaymenttocustomer.Value, Decimal)
                If Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value > 0 Then Me.CheckBoxOtherWithhodling.Checked = True
            End If
        Catch ex As Exception
            Me.txtPaymenttoCustomer.Text = ""
        End Try

        Dim tmp As String = computeTaxAmtByComputeMethod(tmpAmtSubjectToWithHoding, CType(Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate, Decimal), "N", True)

        Me.txtTotalTaxAmount.Text = Math.Round(Me.State.ClaimInvoiceBO.TotalTaxAmount.Value, 2).ToString
        Me.txtTotalWithholdingAmount.Text = Math.Round(Me.State.ClaimInvoiceBO.WithholdingAmount.Value, 2).ToString


        Try
            Me.txtGrandTotal.Text = ((Math.Round(CType(Me.txtTotal.Text, Decimal), 2)) + Math.Round(Me.State.ClaimInvoiceBO.WithholdingAmount.Value, 2)).ToString
            hdTotalWithholdings.Value = Me.State.ClaimInvoiceBO.WithholdingAmount.Value
        Catch ex As Exception
            Me.txtGrandTotal.Text = ""
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
            Me.State.ClaimInvoiceBO.WithholdingAmount += taxAmount
        Else
            Me.State.ClaimInvoiceBO.TotalTaxAmount += taxAmount
        End If


        Return taxAmount.ToString
    End Function
    Private Function getDecimalValue(ByVal decimalObj As DecimalType, Optional ByVal decimalDigit As Integer = 2) As Decimal
        If decimalObj Is Nothing Then
            Return 0D
        Else
            Return Math.Round(decimalObj.Value, decimalDigit, MidpointRounding.AwayFromZero)
        End If
    End Function

    Private Function isForClaimPayAdjustments() As Boolean
        Return Me.State.isForClaimPayAdjust
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


        If Not Me.State.ClaimInvoiceBO.LaborAmt Is Nothing Then
            laborAmount = Me.State.ClaimInvoiceBO.LaborAmt.Value
        End If
        If Not Me.State.ClaimInvoiceBO.PartAmount Is Nothing Then
            partAmt = Me.State.ClaimInvoiceBO.PartAmount.Value
        End If
        If Not Me.State.ClaimInvoiceBO.ServiceCharge Is Nothing Then
            svcCharge = Me.State.ClaimInvoiceBO.ServiceCharge.Value
        End If
        If Not Me.State.ClaimInvoiceBO.TripAmount Is Nothing Then
            tripAmt = Me.State.ClaimInvoiceBO.TripAmount.Value
        End If
        If Not Me.State.ClaimInvoiceBO.ShippingAmount Is Nothing Then
            shippingAmt = Me.State.ClaimInvoiceBO.ShippingAmount.Value
        End If
        If Not Me.State.ClaimInvoiceBO.OtherAmount Is Nothing Then
            otherAmt = Me.State.ClaimInvoiceBO.OtherAmount.Value
        End If
        If Not Me.State.ClaimInvoiceBO.DispositionAmount Is Nothing Then
            dispositionAmt = Me.State.ClaimInvoiceBO.DispositionAmount.Value
        End If
        If Not Me.State.ClaimInvoiceBO.DiagnosticsAmount Is Nothing Then
            diagnosticsAmt = Me.State.ClaimInvoiceBO.DiagnosticsAmount.Value
        End If
        If Not Me.State.ClaimInvoiceBO.Invoiceable.SalvageAmount Is Nothing Then
            salvageAmt = Me.State.ClaimInvoiceBO.Invoiceable.SalvageAmount.Value
        End If
        If Not Me.State.ClaimInvoiceBO.PaytocustomerAmount Is Nothing Then
            Paytocustomer = Me.State.ClaimInvoiceBO.PaytocustomerAmount.Value
        End If

        With Me.State.ClaimInvoiceBO
            retVal = laborAmount + partAmt + svcCharge + tripAmt + otherAmt + shippingAmt + dispositionAmt + diagnosticsAmt
            If Me.State.ClaimInvoiceBO.IsSalvagePayment Then
                retVal = retVal - salvageAmt
            End If
        End With
        Return New DecimalType(retVal)
    End Function

    Private Function CalculateLiabilityLimit() As DecimalType

        Dim al As ArrayList = Me.State.ClaimBO.CalculateLiabilityLimit(Me.State.ClaimBO.CertificateId, Me.State.ClaimBO.Contract.Id, Me.State.ClaimBO.CertItemCoverageId, CType(Me.State.ClaimBO.LossDate.ToString, DateType))
        Dim liabLimit As Decimal = CType(al(0), Decimal)

        Return New DecimalType(liabLimit)
    End Function

    Private Function AboveLiability() As DecimalType

        Dim abovLiability As Decimal = 0D
        Dim liabLimit As Decimal = CDec(Me.CalculateLiabilityLimit())
        Dim Authorizedamount As Decimal = CDec(Me.State.ClaimInvoiceBO.Invoiceable.AuthorizedAmount)

        If (liabLimit = 0D And (CType(Me.State.ClaimBO.Certificate.ProductLiabilityLimit, Decimal) = 0 And CType(Me.State.ClaimBO.CertificateItemCoverage.CoverageLiabilityLimit, Decimal) = 0)) Then
            liabLimit = CDec(999999999.99)
        End If

        If (Authorizedamount > liabLimit) Then
            abovLiability = Authorizedamount - liabLimit
        End If
        Return New DecimalType(abovLiability)
    End Function

    Protected Sub PopulateBOsFromForm(Optional ByVal blnExcludeSaveUserControls As Boolean = False, Optional ByVal blnComingFromSave As Boolean = False)
        Dim OtherAmt As Decimal = 0D

        If Me.State.ViewOnly Then Return
        If blnComingFromSave Then
            Dim TotalVal As Decimal = 0
            Dim xx As String

            If Not hdTotal Is Nothing AndAlso hdTotal.Value <> "" Then
                'TotalVal = CType(hdTotal.Value, Decimal) 'DEF-17426
                'TotalVal = CType(hdTotal.Value.Replace(",", ""), Decimal)
                'DEF-22413-START
                'TotalVal = Decimal.Parse(hdTotal.Value.Trim, System.Globalization.NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture)
                TotalVal = Decimal.Parse(hdTotal.Value.Trim.Replace(ElitaPlusPage.GetGroupSeperator(System.Threading.Thread.CurrentThread.CurrentCulture.ToString()), ""), System.Globalization.NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture)
                'DEF-22413-END
            End If

            If Not hdOtherAmt Is Nothing AndAlso hdOtherAmt.Value <> "" Then
                'OtherAmt = CType(hdOtherAmt.Value, Decimal) 'DEF-17426
                'OtherAmt = CType(hdOtherAmt.Value.Replace(",", ""), Decimal)
                'DEF-22413-START
                'OtherAmt = Decimal.Parse(hdOtherAmt.Value.Trim, System.Globalization.NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture)
                OtherAmt = Decimal.Parse(hdOtherAmt.Value.Trim.Replace(ElitaPlusPage.GetGroupSeperator(System.Threading.Thread.CurrentThread.CurrentCulture.ToString()), ""), System.Globalization.NumberStyles.Any, System.Threading.Thread.CurrentThread.CurrentCulture)
                'DEF-22413-END
            End If

            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "Amount", TotalVal.ToString)
            Session("TotalPaidAmount") = Me.State.ClaimInvoiceBO.Amount.Value
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "LaborAmt", Me.txtLabor)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "PartAmount", Me.txtParts)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "ServiceCharge", Me.txtServiceCharge)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "TripAmount", Me.txtTripAmt)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "ShippingAmount", Me.txtShipping)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "DiagnosticsAmount", Me.txtDiagnostics)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "DispositionAmount", Me.txtDisposition)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "OtherAmount", OtherAmt.ToString)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "WithholdingAmount", Me.txtTotalWithholdingAmount)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "TotalTaxAmount", Me.txtTotalTaxAmount)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "OtherExplanation", Me.txtOtherDesc)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "IvaAmount", Me.txtIvaTax)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "PerceptionIIBB", Me.txtPerceptionIIBB)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "PerceptionIVA", Me.txtPerceptionIva)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "WithholdingAmount", Me.txtTotalWithholdingAmount)
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "PaytocustomerAmount", Me.txtPaymenttoCustomer)

            Dim DeductibleAmount As Double
            Dim DeductibleTaxAmount As Double

            Double.TryParse(Me.txtDeductibleAmount.Text, DeductibleAmount)
            Double.TryParse(Me.txtDeductibleTaxAmount.Text, DeductibleTaxAmount)

            If (oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
                'Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "DeductibleAmount", (Convert.ToDouble(Me.txtDeductibleAmount.Text) + Convert.ToDouble(Me.txtDeductibleTaxAmount.Text)).ToString)
                Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "DeductibleAmount", (DeductibleAmount + DeductibleTaxAmount).ToString)
                If (Me.State.ClaimInvoiceBO.IsIvaResponsibleFlag) Then
                    Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "DeductibleTaxAmount", Me.txtDeductibleTaxAmount)
                End If
                Me.State.ClaimInvoiceBO.Amount = New DecimalType(Me.State.ClaimInvoiceBO.Amount.Value - Me.State.ClaimInvoiceBO.DeductibleAmount.Value)
            End If
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO.Invoiceable, "SalvageAmount", Me.txtSalvageAmt)
            'Me.State.ClaimInvoiceBO.Invoiceable.SalvageAmount = Decimal.Parse(Me.txtSalvageAmt.Text)

        End If
        Dim objCountry As Country = New Country(Me.State.ClaimBO.Certificate.Company.CountryId)
        Me.State.validateBankInfoCountry = LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId)

        Me.PopulateBOProperty(Me.State.ClaimInvoiceBO.Invoiceable, "Deductible", Me.txtDeductible)

        Me.PopulateBOProperty(Me.State.DisbursementBO, "Payee", Me.txtPayeeName)
        Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "SvcControlNumber", Me.txtInvoiceNumber)

        Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "RepairDate", Me.txtRepairDate)
        Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "InvoiceDate", Me.txtInvoiceDate)
        'If Me.txtInvoiceDate.Text.Trim <> String.Empty Then
        '    Me.PopulateBOProperty(Me.State.DisbursementBO, "InvoiceDate", Me.txtInvoiceDate)
        'End If
        Me.PopulateBOProperty(Me.State.DisbursementBO, "StatusDate", Me.txtAcctStatusDate) ' for felita passback
        Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "LoanerReturnedDate", Me.txtLoanerReturnedDate)

        Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "RepairCodeId", Me.cboRepairCode)
        Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "CauseOfLossID", Me.cboCauseOfLossID)
        Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "RegionId", Me.cboRegionDropID)

        'SerialNumber Maintenance
        Me.PopulateBOProperty(Me.State.ClaimInvoiceBO.CurrentCertItem, "SerialNumber", Me.TextSerialNumber)
        Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "SerialNumberTempContainer", Me.TextSerialNumber)

        'Payee info
        Me.PopulateBOProperty(Me.State.DisbursementBO, "PayeeOptionId", Me.cboPayeeSelector)
        Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "PaymentMethodID", Me.PaymentMethodDrop)

        Me.PopulateBOProperty(Me.State.DisbursementBO, "Payee", Me.txtPayeeName)

        If Not GetSelectedItem(Me.cboPayeeSelector).Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, ClaimInvoice.PAYEE_OPTION_OTHER)) Then
            Me.ChangeEnabledProperty(Me.txtPayeeName, False)
        End If

        'Pickup date: do not display if replacement and/or interface claim
        If Not Me.State.ClaimBO Is Nothing AndAlso Me.State.ClaimBO.CanDisplayVisitAndPickUpDates And Not Me.State.ClaimBO.LoanerTaken And Not Me.State.isForClaimPayAdjust Then
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "PickUpDate", Me.TextboxPickupDate)
            Me.PopulateBOProperty(Me.State.ClaimBO, "PickUpDate", Me.TextboxPickupDate)
        ElseIf Me.State.ClaimBO.LoanerTaken And Not Me.State.isForClaimPayAdjust Then
            Me.State.ClaimInvoiceBO.Invoiceable.SetPickUpDateFromLoanerReturnedDate()
            Me.State.ClaimInvoiceBO.PickUpDate = Me.State.ClaimInvoiceBO.LoanerReturnedDate
        End If

        'DEF 1430 - Subtract salvage amount from subtotal for detail invoice method and first payment on claim  ' (Me.State.ClaimInvoiceBO.AlreadyPaid.Value = 0D) 
        If oCompany.InvoiceMethodId.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetInvoiceMethodLookupList(), "1")) AndAlso
            (Not String.IsNullOrEmpty(Me.hdSalvageAmt.Value) AndAlso Convert.ToDouble(Me.hdSalvageAmt.Value) > 0D) AndAlso
            (Me.State.ClaimInvoiceBO.Invoiceable.SalvageAmount.Value <= Me.State.ClaimInvoiceBO.Amount.Value) Then
            Me.State.ClaimInvoiceBO.IsSalvagePayment = True
        End If

        Me.CapturePayeeInfo(GetSelectedItem(cboPayeeSelector), GetSelectedItem(Me.PaymentMethodDrop), blnExcludeSaveUserControls)

        PopulateManualClaimTaxes()

        If Me.ErrCollection.Count > 0 Then
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

    Public Sub CapturePayeeInfo(ByVal selectedPayee As Guid, ByVal selectedPaymentMethod As Guid, Optional ByVal blnExcludeSaveUserControls As Boolean = False)
        Dim PayeeOptionCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, selectedPayee)
        Dim PaymentMethodCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, selectedPaymentMethod)
        Me.State.ClaimInvoiceBO.PayeeOptionCode = PayeeOptionCode
        Me.State.ClaimInvoiceBO.PaymentMethodCode = PaymentMethodCode

        Select Case PayeeOptionCode
            Case ClaimInvoice.PAYEE_OPTION_MASTER_CENTER
                Me.ServiceCenterPopulateBOFromFormLogic()
            Case ClaimInvoice.PAYEE_OPTION_SERVICE_CENTER
                Me.ServiceCenterPopulateBOFromFormLogic()
            Case ClaimInvoice.PAYEE_OPTION_LOANER_CENTER
                Me.ServiceCenterPopulateBOFromFormLogic()
            Case ClaimInvoice.PAYEE_OPTION_CUSTOMER
                Me.CustomerPopulateBOFromFormLogic(PaymentMethodCode, blnExcludeSaveUserControls)
            Case ClaimInvoice.PAYEE_OPTION_OTHER
                Me.OtherPopulateBOFromFormLogic(PaymentMethodCode, blnExcludeSaveUserControls)
        End Select

    End Sub

    Private Sub ServiceCenterPopulateBOFromFormLogic()
        Dim oAccountType As String
        If Me.State.PayeeBankInfo Is Nothing Then
            Me.PayeeAddress.PopulateBOFromControl()
            Me.State.PayeeAddress = PayeeAddress.MyBO
            Me.State.ClaimInvoiceBO.PayeeAddress = Me.State.PayeeAddress
        Else
            Me.PayeeBankInfo.PopulateBOFromControl()
            Me.State.PayeeBankInfo = PayeeBankInfo.State.myBankInfoBo
            Me.State.ClaimInvoiceBO.PayeeBankInfo = Me.State.PayeeBankInfo
            If Not (PayeeBankInfo.State.myBankInfoBo.AccountTypeId.Equals(Guid.Empty)) Then
                oAccountType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_ACCOUNT_TYPES, PayeeBankInfo.State.myBankInfoBo.AccountTypeId)
                Me.PopulateBOProperty(Me.State.DisbursementBO, "AccountType", oAccountType)
            End If
        End If
    End Sub

    Private Sub ValidateAddressControl()
        If Me.txtPayeeName.Text = "" Then
            Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PAYEE_NAME)
        End If
        With Me.State.PayeeAddress
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
        If Not Me.State.PayeeBankInfo Is Nothing Then
            If Me.State.PayeeBankInfo.DomesticTransfer = True Then
                If Not Me.State.validateBankInfoCountry = Codes.Country_Code_France Then
                    If Me.State.PayeeBankInfo.Account_Name Is Nothing Then
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKACCNAME_REQD)
                    End If

                    If Me.State.PayeeBankInfo.Bank_Id Is Nothing Then
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKID_REQD)
                    End If

                    If Me.State.PayeeBankInfo.Account_Number Is Nothing Then
                        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKACCNO_REQD)
                    End If
                Else
                    ValidateBankInfoForCountry(Me.State.validateBankInfoCountry)
                End If
            End If

            If Me.State.PayeeBankInfo.InternationalEUTransfer = True Then
                If Me.State.PayeeBankInfo.Account_Name Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKACCNAME_REQD)
                End If

                If Me.State.PayeeBankInfo.SwiftCode Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKSWIFTCODE_REQD)
                End If

                If Me.State.PayeeBankInfo.IbanNumber Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKIBANNO_REQD)
                End If
            End If

            If Me.State.PayeeBankInfo.InternationalTransfer = True Then
                If Me.State.PayeeBankInfo.Account_Name Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKACCNAME_REQD)
                End If

                If Me.State.PayeeBankInfo.Bank_Id Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKID_REQD)
                End If

                If Me.State.PayeeBankInfo.Account_Number Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKACCNO_REQD)
                End If

                If Me.State.PayeeBankInfo.SwiftCode Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKSWIFTCODE_REQD)
                End If
            End If

        End If
    End Sub

    Private Sub ValidateBankInfoForCountry(ByVal code As String)
        If (code = Codes.Country_Code_France And
                    LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, Me.GetSelectedItem(Me.cboPayeeSelector)) = ClaimInvoice.PAYEE_OPTION_CUSTOMER And
                    LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.GetSelectedItem(Me.PaymentMethodDrop)) = Codes.PAYMENT_METHOD__BANK_TRANSFER) Then
            If Me.State.PayeeBankInfo.IbanNumber Is Nothing Then
                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BANKIBANNO_REQD)
            End If
            With Me.State.PayeeAddress
                If .Address1 Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_ADDRESS)
                End If
                If .City Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CITY_MUST_BE_ENTERED_ERR)
                End If
                'If .RegionId.Equals(Guid.Empty) Then
                '    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_STATE)
                'End If
                If Me.State.ClaimBO.Certificate.Company.CountryId.Equals(Guid.Empty) Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COUNTRY)
                End If
                If .PostalCode Is Nothing Then
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_ZIP)
                End If
            End With
        End If
    End Sub

    Private Sub CustomerPopulateBOFromFormLogic(ByVal PaymentMethodCode As String, Optional ByVal blnExcludeSaveUserControls As Boolean = False)
        Dim oAccountType As String
        Select Case PaymentMethodCode
            Case Codes.PAYMENT_METHOD__BANK_TRANSFER
                Me.State.PayeeBankInfo.PaymentMethodId = GetSelectedItem(PaymentMethodDrop)
                Me.State.PayeeBankInfo.PayeeId = GetSelectedItem(cboPayeeSelector)
                Me.PayeeBankInfo.PopulateBOFromControl(blnExcludeSaveUserControls)
                Me.State.ClaimInvoiceBO.PayeeBankInfo = PayeeBankInfo.State.myBankInfoBo
                Me.State.PayeeBankInfo = PayeeBankInfo.State.myBankInfoBo
                Me.State.PayeeAddress = PayeeBankInfo.State.payeeAddress
                If Not (PayeeBankInfo.State.myBankInfoBo.AccountTypeId.Equals(Guid.Empty)) Then
                    oAccountType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_ACCOUNT_TYPES, PayeeBankInfo.State.myBankInfoBo.AccountTypeId)
                    Me.PopulateBOProperty(Me.State.DisbursementBO, "AccountType", oAccountType)
                End If
                'Req-6171...Darty Claim Reimbursement logic
                If Me.State.validateBankInfoCountry = Codes.Country_Code_France Then
                    Me.State.PayeeAddress = PayeeBankInfo.PopulateAddressInfo()
                    Me.State.PayeeAddress.PaymentMethodId = GetSelectedItem(PaymentMethodDrop)
                    Me.State.PayeeAddress.PayeeId = GetSelectedItem(cboPayeeSelector)
                    Me.PopulateBOProperty(Me.State.DisbursementBO, "Address1", Me.State.PayeeAddress.Address1)
                    Me.PopulateBOProperty(Me.State.DisbursementBO, "Address2", Me.State.PayeeAddress.Address2)
                    Me.PopulateBOProperty(Me.State.DisbursementBO, "City", Me.State.PayeeAddress.City)
                    Me.PopulateBOProperty(Me.State.DisbursementBO, "Zip", Me.State.PayeeAddress.PostalCode)
                    Me.State.PayeeAddress.Validate()
                    If Me.ErrCollection.Count > 0 Then
                        Throw New PopulateBOErrorException
                    End If
                End If
            Case Codes.PAYMENT_METHOD__ADMIN_CHECK
                Me.State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                Me.State.ClaimInvoiceBO.PayeeAddress = Nothing
            Case Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER
                Me.State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                Me.PayeeAddress.PopulateBOFromControl(True)
                Me.State.ClaimInvoiceBO.PayeeAddress = PayeeAddress.MyBO
                Me.State.PayeeAddress = PayeeAddress.MyBO
            Case Codes.PAYMENT_METHOD__DARTY_GIFT_CARD
                Me.State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                Me.State.ClaimInvoiceBO.PayeeAddress = Nothing
        End Select
    End Sub

    Private Sub OtherPopulateBOFromFormLogic(ByVal PaymentMethodCode As String, Optional ByVal blnExcludeSaveUserControls As Boolean = False)
        Dim oAccountType As String
        If Me.State.ClaimInvoiceBO.IsInsuranceCompany Then
            Me.State.ClaimInvoiceBO.DocumentType = LookupListNew.GetCodeFromId(LookupListNew.LK_DOCUMENT_TYPES, GetSelectedItem(Me.cboDocumentTypeId))
            Me.PopulateBOProperty(Me.State.ClaimInvoiceBO, "TaxId", Me.moTaxIdText)
        End If
        Select Case PaymentMethodCode
            Case Codes.PAYMENT_METHOD__BANK_TRANSFER
                Me.PayeeBankInfo.PopulateBOFromControl(blnExcludeSaveUserControls)
                Me.State.ClaimInvoiceBO.PayeeBankInfo = PayeeBankInfo.State.myBankInfoBo
                Me.State.PayeeBankInfo = PayeeBankInfo.State.myBankInfoBo
                If Not (PayeeBankInfo.State.myBankInfoBo.AccountTypeId.Equals(Guid.Empty)) Then
                    oAccountType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_ACCOUNT_TYPES, PayeeBankInfo.State.myBankInfoBo.AccountTypeId)
                    Me.PopulateBOProperty(Me.State.DisbursementBO, "AccountType", oAccountType)
                End If
            Case Codes.PAYMENT_METHOD__ADMIN_CHECK
                Me.State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                Me.State.ClaimInvoiceBO.PayeeAddress = Nothing
            Case Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER
                Me.State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                Me.PayeeAddress.PopulateBOFromControl()
                Me.State.ClaimInvoiceBO.PayeeAddress = PayeeAddress.MyBO
                Me.State.PayeeAddress = PayeeAddress.MyBO
        End Select
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        Try
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_OK Then
                Me.RefreshScreen()
                CleanConsumedActions()
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
                Throw New Exception("Invalid Event")
                CleanConsumedActions()
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If CheckStatusAndSaveClaimInvoice() Then
                            If Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                                Me.NavController.Navigate(Me, "back")
                            Else
                                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimInvoiceBO, Me.State.ChangesMade))
                            End If
                            CleanConsumedActions()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.ClaimInvoiceBO, Me.State.ChangesMade))
                        CleanConsumedActions()
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        Me.State.ClaimInvoiceBO.CloseClaim = True
                        If SaveClaimInvoice() Then
                            If Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                                Me.NavController.Navigate(Me, "back")
                            Else
                                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimInvoiceBO, Me.State.ChangesMade))
                            End If
                        End If
                        CleanConsumedActions()
                    Case ElitaPlusPage.DetailPageCommand.OK
                        Me.State.ClaimInvoiceBO.CloseClaim = True
                        If SaveClaimInvoice() Then
                            Dim claim As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
                            claim.Handle_Replaced_Items(1, Me.State.ClaimBO.Id, Me.State.ClaimBO.CertificateId,
                                    Me.State.ClaimBO.CertItemCoverageId, DateHelper.GetDateValue(txtRepairDate.Text))
                            If Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                                Me.NavController.Navigate(Me, "back")
                            Else
                                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimInvoiceBO, Me.State.ChangesMade))
                            End If
                        End If
                        CleanConsumedActions()
                    Case Else
                        CheckStatusAndSaveClaimInvoice()
                        CleanConsumedActions()
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.State.ClaimInvoiceBO.cancelEdit()
                        If Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                            Me.NavController.Navigate(Me, "back")
                        Else
                            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimInvoiceBO, Me.State.ChangesMade))
                        End If
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        Me.State.ClaimInvoiceBO.CloseClaim = False
                        If SaveClaimInvoice() Then
                            If Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                                Me.NavController.Navigate(Me, "back")
                            Else
                                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimInvoiceBO, Me.State.ChangesMade))
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.OK
                        If SaveClaimInvoice() Then
                            If Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                                Me.NavController.Navigate(Me, "back")
                            Else
                                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimInvoiceBO, Me.State.ChangesMade))
                            End If
                        End If
                End Select
                CleanConsumedActions()
                'in this case, we need to exit after we show the message
                'so we dont care about user's response
            ElseIf Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK Then
                If Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                    Me.NavController.Navigate(Me, "back")
                Else
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimInvoiceBO, Me.State.ChangesMade))
                End If
                CleanConsumedActions()
            End If

            'Clean after consuming the action
            'Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            'Me.HiddenSaveChangesPromptResponse.Value = ""
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CleanConsumedActions()
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Private Sub CheckifComingFromNewClaimBackEndClaim()
        If Me.NavController Is Nothing Then
            Exit Sub
        End If
        If Me.NavController.ParametersPassed Is Nothing Then
            Exit Sub
        End If
        If Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Then
            Dim params As Parameters = CType(Me.NavController.ParametersPassed, Parameters)
            If params.PayClaimID.Equals(Guid.Empty) Then
                Me.State.ClaimBO = params.ClaimBO
                'coming from payclaim , pass claimbo to prepopulate data
                Me.State.ClaimInvoiceBO = New ClaimInvoice
                Me.State.DisbursementBO = Me.State.ClaimInvoiceBO.AddNewDisbursement()
                Me.State.ClaimInvoiceBO.PrepopulateClaimInvoice(Me.State.ClaimBO)
                Me.State.ClaimInvoiceBO.PrepopulateDisbursment()

                Me.State.isForClaimPayAdjust = params.isForClaimPayAdjust
                Me.State.ClaimInvoiceBO.IsNewPaymentFromPaymentAdjustment = Me.State.isForClaimPayAdjust
                'default values
                Me.State.ClaimInvoiceBO.BatchNumber = "1"
                Me.State.ClaimInvoiceBO.LaborTax = New DecimalType(0D)
                Me.State.ClaimInvoiceBO.PartTax = New DecimalType(0D)
                Me.State.ClaimInvoiceBO.RecordCount = New LongType(1)
                Me.State.ClaimInvoiceBO.Source = Nothing
                Me.State.ClaimInvoiceBO.BeginEdit()
            End If
            Me.State.ViewOnly = params.ViewOnly
        End If
    End Sub

    Private Sub CheckifComingFromClaimAdjustment()
        If Me.NavController Is Nothing Then
            Exit Sub
        End If
        If Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
            Dim params As Parameters = CType(Me.NavController.ParametersPassed, Parameters)
            If params.PayClaimID.Equals(Guid.Empty) Then
                Me.State.ClaimBO = params.ClaimBO
                'coming from payclaim , pass claimbo to prepopulate data
                Me.State.ClaimInvoiceBO = New ClaimInvoice
                Me.State.DisbursementBO = Me.State.ClaimInvoiceBO.AddNewDisbursement()
                Me.State.ClaimInvoiceBO.PrepopulateClaimInvoice(Me.State.ClaimBO)
                Me.State.ClaimInvoiceBO.PrepopulateDisbursment()

                Me.State.isForClaimPayAdjust = params.isForClaimPayAdjust
                Me.State.ClaimInvoiceBO.IsNewPaymentFromPaymentAdjustment = Me.State.isForClaimPayAdjust
                'default values
                Me.State.ClaimInvoiceBO.BatchNumber = "1"
                Me.State.ClaimInvoiceBO.LaborTax = New DecimalType(0D)
                Me.State.ClaimInvoiceBO.PartTax = New DecimalType(0D)
                Me.State.ClaimInvoiceBO.RecordCount = New LongType(1)
                Me.State.ClaimInvoiceBO.Source = Nothing
                Me.State.ClaimInvoiceBO.BeginEdit()
            End If
            Me.State.ViewOnly = params.ViewOnly
        End If
    End Sub

    Private Sub RefreshScreen()
        Try
            Me.ReloadClaimAndOtherAssociatedBOs()
            Me.MenuEnabled = False

            'Pickup date: do not display if replacement and/or interface claim
            'Interface claim = Not claim.Source.Equals(String.Empty)
            If Not Me.State.ClaimBO Is Nothing AndAlso Me.State.ClaimBO.CanDisplayVisitAndPickUpDates And Not Me.State.isForClaimPayAdjust Then
                If Not Me.State.ClaimBO.LoanerTaken Then Me.AddCalendar(Me.ImageButtonPickupDate, Me.TextboxPickupDate)
            End If

            If Me.State.ClaimInvoiceBO Is Nothing Then
                Me.State.ClaimInvoiceBO = New ClaimInvoice
            End If

            Me.PopulateFormFromBOs()

            Me.EnableDisableFields()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub ReloadClaimAndOtherAssociatedBOs()
        Try
            If Not Me.CallingParameters Is Nothing Then
                Dim params As Parameters = CType(Me.CallingParameters, Parameters)
                params.ClaimBO = ClaimFacade.Instance.GetClaim(Of Claim)(params.ClaimBO.Id)
                params.PayClaimID = Guid.Empty
                params.ClaimID = params.ClaimBO.Id
                Me.State.ClaimBO = params.ClaimBO
                Me.State.ClaimInvoiceBO = New ClaimInvoice
                Me.State.DisbursementBO = Me.State.ClaimInvoiceBO.AddNewDisbursement()
                Me.State.ClaimInvoiceBO.PrepopulateClaimInvoice(Me.State.ClaimBO)
                Me.State.ClaimInvoiceBO.PrepopulateDisbursment()

                Me.State.isForClaimPayAdjust = params.isForClaimPayAdjust
                Me.State.ClaimInvoiceBO.IsNewPaymentFromPaymentAdjustment = Me.State.isForClaimPayAdjust
                'default values
                Me.State.ClaimInvoiceBO.BatchNumber = "1"
                Me.State.ClaimInvoiceBO.LaborTax = New DecimalType(0D)
                Me.State.ClaimInvoiceBO.PartTax = New DecimalType(0D)
                Me.State.ClaimInvoiceBO.RecordCount = New LongType(1)
                Me.State.ClaimInvoiceBO.Source = Nothing
                Me.State.ClaimInvoiceBO.BeginEdit()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub
#End Region

#Region "Button_handlers"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFromForm(False, False)
            If State.isClaimSystemMaintAllowed AndAlso (Not Me.State.ViewOnly) AndAlso Me.State.ClaimInvoiceBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                If Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Then
                    Me.NavController.Navigate(Me, "back")
                Else
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimInvoiceBO, Me.State.ChangesMade))
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try

    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.State.ClaimInvoiceBO.RefreshCurrentClaim()
            CheckStatusAndSaveClaimInvoice()
            'Req-6171 - Darty gift card (claim reimbursement)
            'GetGiftCardInfo()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Function CheckStatusAndSaveClaimInvoice() As Boolean
        Try
            Dim isPendingReplacement As Boolean = Me.State.ClaimInvoiceBO.Invoiceable.ClaimActivityId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT))
            Dim claimServiceCenter As ServiceCenter = New ServiceCenter(Me.State.ClaimInvoiceBO.Invoiceable.ServiceCenterId)
            Dim PayeeOptionCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, GetSelectedItem(cboPayeeSelector))

            If claimServiceCenter.PayMaster = False And PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_MASTER_CENTER Then
                Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.CANNOT_PAY_MASTER_CENTER)
            End If
            If (Not IsGiftCardValid()) Then
                Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_GIFT_CARD_TYPE)
            End If
            Me.PopulateBOsFromForm(True, True)

            If Not Me.State.ClaimInvoiceBO.PaymentMethodID.Equals(Guid.Empty) Then
                Dim PaymentMethodCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, GetSelectedItem(PaymentMethodDrop))
                If PaymentMethodCode = Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER Then
                    'def - 651
                    If Me.State.ClaimInvoiceBO.PayeeBankInfo Is Nothing Then
                        ValidateAddressControl()
                    End If
                End If
            End If

            'REQ-786
            If Not Me.State.ClaimInvoiceBO.RepairCodeId.Equals(Guid.Empty) Then
                If Me.State.ClaimInvoiceBO.RepairCodeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetRepairCodeLookupList(oCompany.CompanyGroupId), Codes.REPAIR_CODE__DELIVERY_FEE_NO_DEDUCTIBLE)) Then
                    Me.State.ClaimInvoiceBO.Invoiceable.Deductible = New DecimalType(0D)
                End If
            End If

            Me.State.ClaimInvoiceBO.Validate()
            'DEF 2490
            Me.State.ClaimInvoiceBO.CurrentCertItem.Validate()


            If Not Me.PayeeBankInfo Is Nothing Then
                ValidateBankUserControl()
            End If


            If Me.State.ClaimInvoiceBO.IsDirty Then
                If isForClaimPayAdjustments() Then
                    Return ProcessPayClaim(False, isPendingReplacement)
                Else
                    'for repair claim, if the remaining amt > 0 then ask for confirmation or else just close the claim.
                    Me.State.ClaimInvoiceBO.CloseClaim = False
                    If Not isPendingReplacement Then
                        If System.Math.Abs(Me.State.ClaimInvoiceBO.RemainingAmount.Value) > 0 Then
                            'Check with the user...
                            Me.DisplayMessage(Message.MSG_PROMPT_FOR_CLOSING_THE_CLAIM, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                            Return True
                        Else
                            Return ProcessPayClaim(True, isPendingReplacement)
                        End If
                    Else
                        Return ProcessPayClaim(True, isPendingReplacement)
                    End If
                End If
            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Return True
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DALConcurrencyAccessException
            Me.DisplayMessage(Message.MSG_ANOTHER_USER_HAS_MODIFIED_THIS_CLAIM_THE_SYSTEM_MUST_REFRESH_THIS_SCREEN, "", Me.MSG_BTN_OK, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Return False
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Return False
        End Try
    End Function

    Private Function ProcessPayClaim(ByVal closeClaim As Boolean, ByVal isPendingReplacement As Boolean) As Boolean
        ' By Default CancelPolicy as False
        Me.State.ClaimInvoiceBO.CancelPolicy = False

        If Me.State.ClaimBO.Contract Is Nothing Then
            Throw New GUIException(Message.MSG_TO_DEALER_MUST_HAVE_A_VALID_CONTRACT, Assurant.ElitaPlus.Common.ErrorCodes.ERR_CONTRACT_NOT_FOUND)
        End If

        If Me.State.ClaimBO.Contract.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCL)) Then
            Dim lngRepPolicyClaimCnt As Long = ReppolicyClaimCount.GetReplacementPolicyClaimCntByClaim(Me.State.ClaimBO.Contract.Id, Me.State.ClaimBO.Id)
            If lngRepPolicyClaimCnt = 1 Then 'if only require 1 replacement, cancel based on current replacement claim
                Me.State.ClaimInvoiceBO.CancelPolicy = True
            Else
                'more than 1 replacement claims required before cancelling the cert
                Dim certBO As Certificate = Me.State.ClaimBO.Certificate
                Dim paidReplacementClaimCnt As Integer
                Dim Claimlist As Certificate.CertificateClaimsDV

                If certBO.StatusCode = "A" Then 'only cancel if certificate is active
                    Claimlist = certBO.ClaimsForCertificate(certBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    paidReplacementClaimCnt = 0
                    If Claimlist.Count > 0 Then
                        Dim i As Integer, dblPayment As Double
                        For i = 0 To Claimlist.Count - 1
                            dblPayment = Claimlist(i)(Certificate.CertificateClaimsDV.COL_TOTAL_PAID)
                            If dblPayment > 0 AndAlso Claimlist(i)(Certificate.CertificateClaimsDV.COL_CLAIM_NUMBER) <> Me.State.ClaimBO.ClaimNumber _
                                AndAlso Claimlist(i)(Certificate.CertificateClaimsDV.COL_Method_Of_Repair_code) = "R" Then
                                'paid replacement claim
                                paidReplacementClaimCnt = paidReplacementClaimCnt + 1
                                If lngRepPolicyClaimCnt - 1 <= paidReplacementClaimCnt Then
                                    Me.State.ClaimInvoiceBO.CancelPolicy = True
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End If

        If isPendingReplacement Then
            Dim claim As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
            If (claim.Handle_Replaced_Items(0, Me.State.ClaimBO.Id, Me.State.ClaimBO.CertificateId,
                    Me.State.ClaimBO.CertItemCoverageId, DateHelper.GetDateValue(txtRepairDate.Text)) = 0) Then
                'close the replacement claim with reason "Replaced"
                If (closeClaim) Then
                    Me.State.ClaimInvoiceBO.CloseClaim = True
                End If
                Return SaveClaimInvoice()
            Else
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_HAVE_ITEM_REPLACED, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                Return True
            End If
        Else
            If (closeClaim) Then
                Me.State.ClaimInvoiceBO.CloseClaim = True
            End If
            Return SaveClaimInvoice()
        End If
    End Function

    Private Function SaveClaimInvoice() As Boolean
        Try
            Me.State.ClaimInvoiceBO.CurrentCertItem.Save()
            Me.State.ClaimInvoiceBO.Save()
            Me.State.ClaimInvoiceBO.EndEdit()
            Me.State.ChangesMade = True
            Dim listItem As ListItem = PaymentMethodDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode(
                                                                      "PMTHD", Codes.PAYMENT_METHOD__DARTY_GIFT_CARD))
            Dim listItem1 As ListItem = PaymentMethodDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode(
                                                                   "PMTHD", Codes.PAYMENT_METHOD__BANK_TRANSFER))
            If (Not listItem Is Nothing) Then
                If (cboPayeeSelector.SelectedItem.Text.ToUpper = Payee_Customer AndAlso PaymentMethodDrop.SelectedItem.Text = listItem.Text) Then
                    If (Me.oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).Count > 0) Then
                        Dim attvalue As AttributeValue = Me.oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).First()
                        If (attvalue.EffectiveDate < DateTime.UtcNow Or attvalue.ExpirationDate > DateTime.UtcNow) Then
                            GetGiftCardInfo() 'Req-6171 - Darty gift card (claim reimbursement)
                        End If
                    End If
                End If
            End If
            If (Not listItem1 Is Nothing) Then
                If (cboPayeeSelector.SelectedItem.Text.ToUpper = Payee_Customer AndAlso PaymentMethodDrop.SelectedItem.Text = listItem1.Text) Then
                    Dim argumentsToAddEvent As String
                    With Me.State.ClaimBO
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

            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
            Me.DisplayMessageWithSubmit(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            Return True
        Catch ex As ElitaPlusException
            Throw ex
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DALConcurrencyAccessException
            Me.DisplayMessage(Message.MSG_ANOTHER_USER_HAS_MODIFIED_THIS_CLAIM_THE_SYSTEM_MUST_REFRESH_THIS_SCREEN, "", Me.MSG_BTN_OK, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Return False
        Catch ex As Exception
            Throw ex
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Return False
        End Try
    End Function

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            Me.PopulateControlFromBOProperty(Me.txtGrandTotal, 0.0)
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnTaxes_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTaxes_WRITE.Click
        Try
            If Not Me.State.ClaimBO Is Nothing Then
                Dim objParm As New PayClaimManualTaxForm.Parameters
                objParm.ManualTaxList = State.ManualTaxes
                Me.callPage(PayClaimManualTaxForm.URL, objParm)
            End If
        Catch exT As System.Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnPartsInfo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPartsInfo_WRITE.Click
        Try
            Me.State.PartsInfoViewed = True

            'DEF-394 - ALR - Commenting out check for visibility.  Set the value regardless.
            '   If Me.PaymentMethodDrop.Visible Then
            Me.State.selectedPaymentMethodID = GetSelectedItem(PaymentMethodDrop)
            '  End If

            If Not Me.State.ClaimBO Is Nothing Then
                Me.PopulateBOsFromForm(True, True)
                'Me.State.PartsInfoReplacementClick = True
                If Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_CLADJ" Or Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL_BACKEND" Or Me.NavController.CurrentNavState.Name = "PAY_CLAIM_DETAIL" Then
                    Me.NavController.Navigate(Me, "parts_info", BuildPartsInfoParameters)
                    'Else
                    '    Me.callPage(PartsInfoForm.URL, Me.State.ClaimBO)
                End If
            End If
        Catch exT As System.Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Function BuildPartsInfoParameters() As PartsInfoForm.Parameters
        Dim claimBO As Claim = Me.State.ClaimBO
        Return New PartsInfoForm.Parameters(claimBO)
    End Function

    'DEF-17426
    Function BuildClaimAuthDetailParameters() As ClaimAuthDetailForm.Parameters
        Dim claimBO As Claim = Me.State.ClaimBO
        Return New ClaimAuthDetailForm.Parameters(claimBO, Nothing, Nothing)
    End Function

    Private Sub btnAuthDetail_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAuthDetail_WRITE.Click
        Try
            If Not Me.State.ClaimBO Is Nothing Then
                'DEF-17426
                Me.PopulateBOsFromForm(True, True)
                Me.State.ClaimBO.AuthDetailUsage = Me.URL
                'Me.callPage(ClaimAuthDetailForm.URL, Me.State.ClaimBO) 'DEF-17426
                Me.NavController.Navigate(Me, "auth_detail", BuildClaimAuthDetailParameters)
            End If
        Catch exT As System.Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnReplacement_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplacement_WRITE.Click
        Try
            Me.PopulateBOsFromForm(True, True)
            Me.callPage(Claims.ReplacementForm.URL, New Claims.ReplacementForm.Parameters(False, Me.State.ClaimInvoiceBO.Invoiceable.Claim_Id))
        Catch exT As System.Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub



    Private Sub cboPayeeSelector_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPayeeSelector.SelectedIndexChanged
        Try
            PayeeChanged(GetSelectedItem(cboPayeeSelector), True)
            LoadClaimTaxRates()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PaymentMethodDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PaymentMethodDrop.SelectedIndexChanged
        Try
            PaymentMethodChanged(GetSelectedItem(PaymentMethodDrop), GetSelectedItem(cboPayeeSelector), True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PaymentMethodChanged(ByVal selectedPaymentMethod As Guid, ByVal selectedPayee As Guid, ByVal blnClearOldBankInfo As Boolean)
        Dim PaymentMethodCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, selectedPaymentMethod)

        ControlMgr.SetVisibleControl(Me, Me.hrSeprator, False)

        Try
            Select Case PaymentMethodCode
                Case Codes.PAYMENT_METHOD__BANK_TRANSFER
                    Me.BankTransferLogic(blnClearOldBankInfo)
                    Me.PayeeBankInfo.SetRequiredFieldsForDealerWithGiftCard(oDealer)
                Case Codes.PAYMENT_METHOD__ADMIN_CHECK
                    ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, False)
                    ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, False)
                    ControlMgr.SetVisibleControl(Me, Me.hrSeprator, True)
                Case Codes.PAYMENT_METHOD__CHECK_TO_CONSUMER
                    Dim PayeeOptionCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, selectedPayee)
                    ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, True)
                    Me.CheckToConsumerLogic(PayeeOptionCode)
                Case Codes.PAYMENT_METHOD__DARTY_GIFT_CARD
                    ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, False)
                    ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, False)
                    ControlMgr.SetVisibleControl(Me, Me.hrSeprator, True)
            End Select

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
    Private Sub BankTransferLogic(ByVal blnClearOldBankInfo As Boolean)

        ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, False)
        ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, True)

        ' Me.State.ClaimInvoiceBO.PayeeAddress = Nothing
        If blnClearOldBankInfo Then Me.State.ClaimInvoiceBO.PayeeBankInfo = Nothing
        Me.State.PayeeAddress = Nothing

        If Me.State.ViewOnly Then
            Me.PopulatePayeeBankInfoBO()
            Me.PayeeBankInfo.Bind(Me.State.PayeeBankInfo)
            Me.PayeeBankInfo.ChangeEnabledControlProperty(False)
        Else
            If blnClearOldBankInfo Then Me.State.PayeeBankInfo = Me.State.ClaimInvoiceBO.Add_BankInfo() 'New BankInfo
            Me.State.PayeeBankInfo.SourceCountryID = oCompany.CountryId
            Me.PayeeBankInfo.Bind(Me.State.PayeeBankInfo)
            Me.PayeeBankInfo.ChangeEnabledControlProperty(True)
        End If


        'Me.ChangeEnabledProperty(Me.txtPayeeName, False)
        ControlMgr.SetVisibleControl(Me, txtIvaTax, False)
        txtIvaTax.Text = "0"
        ControlMgr.SetVisibleControl(Me, LabelIvaTax, False)
    End Sub

    Private Sub CheckToConsumerLogic(ByVal PayeeOptionCode As String)
        ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, True)
        ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, False)
        Me.State.ClaimInvoiceBO.PayeeBankInfo = Nothing
        Me.State.PayeeBankInfo = Nothing

        If PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_CUSTOMER Then
            Me.txtPayeeName.Text = Me.State.ClaimInvoiceBO.CustomerName
            If Me.State.ViewOnly Then
                Me.PopulatePayeeAddressBO()
                Me.PayeeAddress.Bind(Me.State.PayeeAddress)
                Me.PayeeAddress.EnableControls(True)
                Me.PayeeAddress.RegionText = Me.State.DisbursementBO.RegionDesc
            Else
                If Me.State.PayeeAddress Is Nothing Then Me.State.PayeeAddress = Me.State.ClaimInvoiceBO.Add_Address(Me.State.ClaimInvoiceBO.CustomerAddressID) 'New Address(Me.State.ClaimInvoiceBO.CustomerAddressID)
                Me.State.ClaimInvoiceBO.PayeeAddress = Me.State.PayeeAddress
                Me.PayeeAddress.Bind(Me.State.PayeeAddress)
                Me.PayeeAddress.EnableControls(False, True)
            End If


        ElseIf PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_OTHER Then
            'Me.txtPayeeName.Text = Me.State.DisbursementBO.Payee
            If Me.State.ViewOnly Then
                Me.PopulatePayeeAddressBO()
                Me.PayeeAddress.Bind(Me.State.PayeeAddress)
                Me.PayeeAddress.EnableControls(True)
                Me.PayeeAddress.RegionText = Me.State.DisbursementBO.RegionDesc
            Else
                If Me.State.PayeeAddress Is Nothing Then Me.State.PayeeAddress = Me.State.ClaimInvoiceBO.Add_Address() 'New Address
                Me.State.ClaimInvoiceBO.PayeeAddress = Me.State.PayeeAddress
                Me.PayeeAddress.Bind(Me.State.PayeeAddress)
                Me.PayeeAddress.EnableControls(False, True)
            End If

        End If

        'Me.ChangeEnabledProperty(Me.txtPayeeName, False)
        ControlMgr.SetVisibleControl(Me, txtIvaTax, False)
        txtIvaTax.Text = "0"
        ControlMgr.SetVisibleControl(Me, LabelIvaTax, False)
    End Sub


    Private Sub PayeeChanged(ByVal selectedPayee As Guid, ByVal bNewSelection As Boolean)

        Dim PayeeOptionCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, selectedPayee)

        ControlMgr.SetVisibleControl(Me, Me.PaymentMethodDrop, False)
        ControlMgr.SetVisibleControl(Me, Me.moPaymentMethodLabel, False)
        ControlMgr.SetVisibleControl(Me, Me.Req_Pay_MethodLabel, False)

        ControlMgr.SetVisibleControl(Me, Me.hrSeprator, False)

        Me.ChangeEnabledProperty(Me.txtPayeeName, False)
        ExcludeFromDropdown(Codes.ATTR_DARTY_GIFT_CARD_TYPE)

        Me.PaymentMethodDrop.SelectedIndex = NOTHING_SELECTED
        Me.PayeeCode.Value = PayeeOptionCode
        Try
            Me.ShowDocumentIDInformation(False)
            Select Case PayeeOptionCode
                Case ClaimInvoice.PAYEE_OPTION_MASTER_CENTER
                    Dim claimServiceCenter As ServiceCenter = New ServiceCenter(Me.State.ClaimInvoiceBO.Invoiceable.ServiceCenterId)
                    If Not claimServiceCenter.WithholdingRate Is Nothing Then Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate = claimServiceCenter.WithholdingRate.Value * (-1)
                    If Not claimServiceCenter.MasterCenterId.Equals(Guid.Empty) Then
                        Dim masterServiceCenter As ServiceCenter = New ServiceCenter(claimServiceCenter.MasterCenterId)
                        Me.txtPayeeName.Text = masterServiceCenter.Description
                        If Not masterServiceCenter.WithholdingRate Is Nothing Then Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate = masterServiceCenter.WithholdingRate.Value * (-1)
                        If Me.State.ClaimInvoiceBO.IsInsuranceCompany Then
                            Me.State.ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeOptionCode, masterServiceCenter, Me.State.ClaimInvoiceBO, Me.State.DisbursementBO, Nothing, Nothing, Nothing)
                        End If
                        If Not masterServiceCenter.BankInfoId.Equals(Guid.Empty) Then
                            'Me.State.ClaimInvoiceBO.PayeeBankInfoIDContainer = masterServiceCenter.BankInfoId
                            Me.State.ClaimInvoiceBO.PayeeBankInfo = masterServiceCenter.Add_BankInfo()
                            Me.State.ClaimInvoiceBO.PayeeAddress = Nothing
                            Me.State.PayeeAddress = Nothing
                            ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, False)
                            ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, True)
                            Me.State.PayeeBankInfo = Me.State.ClaimInvoiceBO.Add_BankInfo(masterServiceCenter.BankInfoId) 'New BankInfo(masterServiceCenter.BankInfoId)
                            Me.State.PayeeBankInfo.SourceCountryID = oCompany.CountryId
                            Me.PayeeBankInfo.Bind(Me.State.PayeeBankInfo)
                            Me.PayeeBankInfo.ChangeEnabledControlProperty(False)
                        Else
                            ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, True)
                            ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, False)
                            Me.State.ClaimInvoiceBO.PayeeAddress = masterServiceCenter.Address
                            Me.State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                            Me.State.PayeeBankInfo = Nothing
                            Me.State.PayeeAddress = Me.State.ClaimInvoiceBO.Add_Address(masterServiceCenter.AddressId) 'New Address(masterServiceCenter.AddressId)
                            Me.PayeeAddress.Bind(Me.State.PayeeAddress)
                            Me.ChangeEnabledProperty(Me.txtPayeeName, False)
                            Me.PayeeAddress.EnableControls(True)
                            ControlMgr.SetVisibleControl(Me, txtIvaTax, masterServiceCenter.IvaResponsibleFlag)
                            ControlMgr.SetVisibleControl(Me, LabelIvaTax, masterServiceCenter.IvaResponsibleFlag)
                            Me.ChangeEnabledProperty(txtIvaTax, (Not Me.State.ViewOnly))
                        End If
                    End If
                    Me.PayeeBankInfo.HideTaxId()

                    ControlMgr.SetVisibleControl(Me, Me.divlblPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, Me.divtxtPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, Me.divtxtPaymenttoCustomertax, False)
                    ControlMgr.SetVisibleControl(Me, Me.divCheckBoxPaymenttocustomer, False)

                    ControlMgr.SetEnableControl(Me, Me.txtLiabilityLimit, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDeductible, True)
                    ControlMgr.SetEnableControl(Me, Me.txtLabor, True)
                    ControlMgr.SetEnableControl(Me, Me.txtLaborTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiscount, True)
                    ControlMgr.SetEnableControl(Me, Me.txtParts, True)
                    ControlMgr.SetEnableControl(Me, Me.txtPartsTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAuthAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtServiceCharge, True)
                    ControlMgr.SetEnableControl(Me, Me.txtServiceChargeTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAboveLiability, True)
                    ControlMgr.SetEnableControl(Me, Me.txtTripAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtTripAmtTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtSalvageAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtShipping, True)
                    ControlMgr.SetEnableControl(Me, Me.txtShippingTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtConsumerPays, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDisposition, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDispositionTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAssurantPays, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiagnostics, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiagnosticsTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtRemainingAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, Me.LabelAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, Me.txtOtherAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtOtherTax, True)

                Case ClaimInvoice.PAYEE_OPTION_SERVICE_CENTER
                    Dim claimServiceCenter As ServiceCenter = New ServiceCenter(Me.State.ClaimInvoiceBO.Invoiceable.ServiceCenterId)
                    'If the pay master is on, pay master instead
                    If claimServiceCenter.PayMaster AndAlso (Not claimServiceCenter.MasterCenterId.Equals(Guid.Empty)) Then
                        claimServiceCenter = New ServiceCenter(claimServiceCenter.MasterCenterId)
                    End If

                    If Not claimServiceCenter.WithholdingRate Is Nothing Then Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate = claimServiceCenter.WithholdingRate.Value * (-1)

                    Me.txtPayeeName.Text = claimServiceCenter.Description
                    If Me.State.ClaimInvoiceBO.IsInsuranceCompany Then
                        Me.State.ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeOptionCode, claimServiceCenter, Me.State.ClaimInvoiceBO, Me.State.DisbursementBO, Nothing, Nothing, Nothing)
                    End If
                    If Not claimServiceCenter.BankInfoId.Equals(Guid.Empty) Then
                        Me.State.ClaimInvoiceBO.PayeeBankInfo = claimServiceCenter.Add_BankInfo()
                        Me.State.ClaimInvoiceBO.PayeeAddress = Nothing
                        Me.State.PayeeAddress = Nothing
                        ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, False)
                        ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, True)
                        Me.State.PayeeBankInfo = Me.State.ClaimInvoiceBO.Add_BankInfo(claimServiceCenter.BankInfoId) 'New BankInfo(claimServiceCenter.BankInfoId)
                        Me.State.PayeeBankInfo.SourceCountryID = oCompany.CountryId
                        Me.PayeeBankInfo.Bind(Me.State.PayeeBankInfo)
                        Me.PayeeBankInfo.ChangeEnabledControlProperty(False)
                    Else
                        ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, True)
                        ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, False)
                        Me.State.ClaimInvoiceBO.PayeeAddress = claimServiceCenter.Address
                        Me.State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                        Me.State.PayeeBankInfo = Nothing
                        Me.State.PayeeAddress = Me.State.ClaimInvoiceBO.Add_Address(claimServiceCenter.AddressId) 'New Address(claimServiceCenter.AddressId)
                        Me.PayeeAddress.Bind(Me.State.PayeeAddress)
                        Me.ChangeEnabledProperty(Me.txtPayeeName, False)
                        Me.PayeeAddress.EnableControls(True)
                        ControlMgr.SetVisibleControl(Me, txtIvaTax, claimServiceCenter.IvaResponsibleFlag)
                        ControlMgr.SetVisibleControl(Me, LabelIvaTax, claimServiceCenter.IvaResponsibleFlag)
                        Me.ChangeEnabledProperty(txtIvaTax, (Not Me.State.ViewOnly))
                    End If
                    Me.PayeeBankInfo.HideTaxId()

                    ControlMgr.SetVisibleControl(Me, Me.divlblPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, Me.divtxtPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, Me.divtxtPaymenttoCustomertax, False)
                    ControlMgr.SetVisibleControl(Me, Me.divCheckBoxPaymenttocustomer, False)

                    ControlMgr.SetEnableControl(Me, Me.txtLiabilityLimit, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDeductible, True)
                    ControlMgr.SetEnableControl(Me, Me.txtLabor, True)
                    ControlMgr.SetEnableControl(Me, Me.txtLaborTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiscount, True)
                    ControlMgr.SetEnableControl(Me, Me.txtParts, True)
                    ControlMgr.SetEnableControl(Me, Me.txtPartsTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAuthAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtServiceCharge, True)
                    ControlMgr.SetEnableControl(Me, Me.txtServiceChargeTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAboveLiability, True)
                    ControlMgr.SetEnableControl(Me, Me.txtTripAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtTripAmtTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtSalvageAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtShipping, True)
                    ControlMgr.SetEnableControl(Me, Me.txtShippingTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtConsumerPays, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDisposition, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDispositionTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAssurantPays, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiagnostics, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiagnosticsTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtRemainingAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, Me.LabelAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, Me.txtOtherAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtOtherTax, True)

                Case ClaimInvoice.PAYEE_OPTION_LOANER_CENTER
                    Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate = Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value
                    If Not Me.State.ClaimInvoiceBO.Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                        Dim loanerServiceCenter As ServiceCenter = New ServiceCenter(Me.State.ClaimInvoiceBO.Invoiceable.LoanerCenterId)
                        Me.txtPayeeName.Text = loanerServiceCenter.Description
                        If Me.State.ClaimInvoiceBO.IsInsuranceCompany Then
                            Me.State.ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeOptionCode, loanerServiceCenter, Me.State.ClaimInvoiceBO, Me.State.DisbursementBO, Nothing, Nothing, Nothing)
                        End If
                        If Not loanerServiceCenter.WithholdingRate Is Nothing Then Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate = loanerServiceCenter.WithholdingRate.Value * (-1)
                        If Not loanerServiceCenter.BankInfoId.Equals(Guid.Empty) Then
                            Me.State.ClaimInvoiceBO.PayeeBankInfo = loanerServiceCenter.Add_BankInfo()
                            Me.State.ClaimInvoiceBO.PayeeAddress = Nothing
                            Me.State.PayeeAddress = Nothing
                            ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, False)
                            ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, True)
                            Me.State.PayeeBankInfo = Me.State.ClaimInvoiceBO.Add_BankInfo(loanerServiceCenter.BankInfoId) 'New BankInfo(loanerServiceCenter.BankInfoId)
                            Me.State.PayeeBankInfo.SourceCountryID = oCompany.CountryId
                            Me.PayeeBankInfo.Bind(Me.State.PayeeBankInfo)
                            Me.PayeeBankInfo.ChangeEnabledControlProperty(False)
                        Else
                            ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, True)
                            ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, False)
                            Me.State.ClaimInvoiceBO.PayeeAddress = loanerServiceCenter.Address   'Added by AA for WR761620
                            Me.State.ClaimInvoiceBO.PayeeBankInfo = Nothing
                            Me.State.PayeeBankInfo = Nothing
                            Me.State.PayeeAddress = Me.State.ClaimInvoiceBO.Add_Address(loanerServiceCenter.AddressId) 'New Address(loanerServiceCenter.AddressId)
                            Me.PayeeAddress.Bind(Me.State.PayeeAddress)
                            Me.ChangeEnabledProperty(Me.txtPayeeName, False)
                            Me.PayeeAddress.EnableControls(True)
                            ControlMgr.SetVisibleControl(Me, txtIvaTax, loanerServiceCenter.IvaResponsibleFlag)
                            ControlMgr.SetVisibleControl(Me, LabelIvaTax, loanerServiceCenter.IvaResponsibleFlag)
                            Me.ChangeEnabledProperty(txtIvaTax, (Not Me.State.ViewOnly))
                        End If
                    End If
                    Me.PayeeBankInfo.HideTaxId()

                    ControlMgr.SetVisibleControl(Me, Me.divlblPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, Me.divtxtPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, Me.divtxtPaymenttoCustomertax, False)
                    ControlMgr.SetVisibleControl(Me, Me.divCheckBoxPaymenttocustomer, False)

                    ControlMgr.SetEnableControl(Me, Me.txtLiabilityLimit, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDeductible, True)
                    ControlMgr.SetEnableControl(Me, Me.txtLabor, True)
                    ControlMgr.SetEnableControl(Me, Me.txtLaborTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiscount, True)
                    ControlMgr.SetEnableControl(Me, Me.txtParts, True)
                    ControlMgr.SetEnableControl(Me, Me.txtPartsTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAuthAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtServiceCharge, True)
                    ControlMgr.SetEnableControl(Me, Me.txtServiceChargeTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAboveLiability, True)
                    ControlMgr.SetEnableControl(Me, Me.txtTripAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtTripAmtTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtSalvageAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtShipping, True)
                    ControlMgr.SetEnableControl(Me, Me.txtShippingTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtConsumerPays, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDisposition, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDispositionTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAssurantPays, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiagnostics, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiagnosticsTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtRemainingAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, Me.LabelAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, Me.txtOtherAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtOtherTax, True)

                Case ClaimInvoice.PAYEE_OPTION_CUSTOMER
                    If State.ClaimBO.Dealer.AttributeValues.Contains(Codes.DLR_ATTRBT_PAY_TO_CUST_AMT) Then
                        If oDealer.AttributeValues.Value(Codes.DLR_ATTRBT_PAY_TO_CUST_AMT) = Codes.YESNO_Y AndAlso Me.State.ClaimBO.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then

                            ControlMgr.SetVisibleControl(Me, Me.hrSeprator, True)
                            ControlMgr.SetVisibleControl(Me, Me.PaymentMethodDrop, True)
                            ControlMgr.SetVisibleControl(Me, Me.moPaymentMethodLabel, True)
                            ControlMgr.SetVisibleControl(Me, Me.Req_Pay_MethodLabel, True)
                            ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, False)
                            ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, False)

                            'ControlMgr.SetVisibleControl(Me, Me.trPaymenttoCustomer, True)
                            ControlMgr.SetVisibleControl(Me, Me.divlblPaymenttoCustomer, True)
                            ControlMgr.SetVisibleControl(Me, Me.divtxtPaymenttoCustomer, True)
                            ControlMgr.SetVisibleControl(Me, Me.divtxtPaymenttoCustomertax, True)
                            ControlMgr.SetVisibleControl(Me, Me.divCheckBoxPaymenttocustomer, True)

                            ControlMgr.SetEnableControl(Me, Me.txtLiabilityLimit, False)
                            ControlMgr.SetEnableControl(Me, Me.txtDeductible, False)
                            ControlMgr.SetEnableControl(Me, Me.txtLabor, False)
                            ControlMgr.SetEnableControl(Me, Me.txtLaborTax, False)
                            ControlMgr.SetEnableControl(Me, Me.txtDiscount, False)
                            ControlMgr.SetEnableControl(Me, Me.txtParts, False)
                            ControlMgr.SetEnableControl(Me, Me.txtPartsTax, False)
                            ControlMgr.SetEnableControl(Me, Me.txtAuthAmt, False)
                            ControlMgr.SetEnableControl(Me, Me.txtServiceCharge, False)
                            ControlMgr.SetEnableControl(Me, Me.txtServiceChargeTax, False)
                            ControlMgr.SetEnableControl(Me, Me.txtAboveLiability, False)
                            ControlMgr.SetEnableControl(Me, Me.txtTripAmt, False)
                            ControlMgr.SetEnableControl(Me, Me.txtTripAmtTax, False)
                            ControlMgr.SetEnableControl(Me, Me.txtSalvageAmt, False)
                            ControlMgr.SetEnableControl(Me, Me.txtShipping, False)
                            ControlMgr.SetEnableControl(Me, Me.txtShippingTax, False)
                            ControlMgr.SetEnableControl(Me, Me.txtConsumerPays, False)
                            ControlMgr.SetEnableControl(Me, Me.txtDisposition, False)
                            ControlMgr.SetEnableControl(Me, Me.txtDispositionTax, False)
                            ControlMgr.SetEnableControl(Me, Me.txtAssurantPays, False)
                            ControlMgr.SetEnableControl(Me, Me.txtDiagnostics, False)
                            ControlMgr.SetEnableControl(Me, Me.txtDiagnosticsTax, False)
                            ControlMgr.SetEnableControl(Me, Me.txtRemainingAmt, False)
                            ControlMgr.SetEnableControl(Me, Me.txtAlreadyPaid, False)
                            ControlMgr.SetEnableControl(Me, Me.LabelAlreadyPaid, False)
                            ControlMgr.SetEnableControl(Me, Me.txtOtherAmt, False)
                            ControlMgr.SetEnableControl(Me, Me.txtOtherTax, False)
                        End If
                    Else
                        ControlMgr.SetVisibleControl(Me, Me.hrSeprator, True)
                        ControlMgr.SetVisibleControl(Me, Me.PaymentMethodDrop, True)
                        ControlMgr.SetVisibleControl(Me, Me.moPaymentMethodLabel, True)
                        ControlMgr.SetVisibleControl(Me, Me.Req_Pay_MethodLabel, True)
                        ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, False)
                        ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, False)
                    End If

                    Me.txtPayeeName.Text = Me.State.ClaimInvoiceBO.CustomerName
                    If Me.State.ClaimInvoiceBO.IsInsuranceCompany Then
                        Me.ShowDocumentIDInformation(False)
                        Dim objCertificate As Certificate = New Certificate(Me.State.ClaimInvoiceBO.Invoiceable.CertificateId)
                        Me.State.ClaimInvoiceBO.CapturePayeeTaxDocumentation(PayeeOptionCode, Nothing, Me.State.ClaimInvoiceBO, Me.State.DisbursementBO, objCertificate, Nothing, Nothing)
                    End If
                    If bNewSelection Then
                        Me.State.PayeeAddress = Me.State.ClaimInvoiceBO.Add_Address(Me.State.ClaimInvoiceBO.CustomerAddressID) 'New Address(Me.State.ClaimInvoiceBO.CustomerAddressID)
                        Me.State.ClaimInvoiceBO.PayeeAddress = Me.State.PayeeAddress
                    End If
                    Me.PayeeAddress.EnableControls(False, True)
                    Me.PayeeBankInfo.DisplayTaxId()
                    Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate = 0
                Case ClaimInvoice.PAYEE_OPTION_OTHER
                    ControlMgr.SetVisibleControl(Me, Me.hrSeprator, True)
                    ControlMgr.SetVisibleControl(Me, Me.PaymentMethodDrop, True)
                    ControlMgr.SetVisibleControl(Me, Me.moPaymentMethodLabel, True)
                    ControlMgr.SetVisibleControl(Me, Me.Req_Pay_MethodLabel, True)
                    ControlMgr.SetVisibleControl(Me, Me.PayeeAddress, False)
                    ControlMgr.SetVisibleControl(Me, Me.PayeeBankInfo, False)
                    If Me.State.ClaimInvoiceBO.IsInsuranceCompany Then Me.ShowDocumentIDInformation(True)
                    If Me.State.ViewOnly Then
                        Me.PopulatePayeeAddressBO()
                        Me.ChangeEnabledProperty(Me.txtPayeeName, False)
                    Else
                        Me.txtPayeeName.Text = ""
                        If bNewSelection Then
                            Me.State.PayeeAddress = Me.State.ClaimInvoiceBO.AddressChild()
                        End If
                        Me.State.ClaimInvoiceBO.PayeeAddress = Me.State.PayeeAddress  'Added by AA for WR761620
                        Me.ChangeEnabledProperty(Me.txtPayeeName, True)

                    End If
                    Me.PayeeAddress.Bind(Me.State.PayeeAddress)

                    If bNewSelection Then
                        Me.PayeeAddress.ClearAll()
                    End If
                    Me.PayeeAddress.EnableControls(Me.State.ViewOnly, True)
                    If Me.State.ViewOnly Then Me.PayeeAddress.RegionText = Me.State.DisbursementBO.RegionDesc
                    ControlMgr.SetVisibleControl(Me, txtIvaTax, False)
                    txtIvaTax.Text = "0"
                    ControlMgr.SetVisibleControl(Me, LabelIvaTax, False)
                    Me.PayeeBankInfo.DisplayTaxId()
                    Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate = 0

                    ControlMgr.SetVisibleControl(Me, Me.divlblPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, Me.divtxtPaymenttoCustomer, False)
                    ControlMgr.SetVisibleControl(Me, Me.divtxtPaymenttoCustomertax, False)
                    ControlMgr.SetVisibleControl(Me, Me.divCheckBoxPaymenttocustomer, False)

                    ControlMgr.SetEnableControl(Me, Me.txtLiabilityLimit, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDeductible, True)
                    ControlMgr.SetEnableControl(Me, Me.txtLabor, True)
                    ControlMgr.SetEnableControl(Me, Me.txtLaborTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiscount, True)
                    ControlMgr.SetEnableControl(Me, Me.txtParts, True)
                    ControlMgr.SetEnableControl(Me, Me.txtPartsTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAuthAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtServiceCharge, True)
                    ControlMgr.SetEnableControl(Me, Me.txtServiceChargeTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAboveLiability, True)
                    ControlMgr.SetEnableControl(Me, Me.txtTripAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtTripAmtTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtSalvageAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtShipping, True)
                    ControlMgr.SetEnableControl(Me, Me.txtShippingTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtConsumerPays, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDisposition, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDispositionTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAssurantPays, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiagnostics, True)
                    ControlMgr.SetEnableControl(Me, Me.txtDiagnosticsTax, True)
                    ControlMgr.SetEnableControl(Me, Me.txtRemainingAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, Me.LabelAlreadyPaid, True)
                    ControlMgr.SetEnableControl(Me, Me.txtOtherAmt, True)
                    ControlMgr.SetEnableControl(Me, Me.txtOtherTax, True)

            End Select

            'If Me.IsPostBack AndAlso Me.State.ClaimInvoiceBO.ServiceCenterWithholdingRate.Value <> 0 AndAlso (PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_MASTER_CENTER Or
            '                                                                                                      PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_SERVICE_CENTER Or
            '                                                                                                      PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_LOANER_CENTER) Then
            '    ComputeAutoTaxes(True)
            'End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub ShowDocumentIDInformation(ByVal blnSetVisible As Boolean)
        If blnSetVisible Then
            ControlMgr.SetVisibleControl(Me, moDocumentTypeLabel, True)
            ControlMgr.SetVisibleControl(Me, cboDocumentTypeId, True)
            ControlMgr.SetVisibleControl(Me, moTaxIdLabel, True)
            ControlMgr.SetVisibleControl(Me, moTaxIdText, True)
            ControlMgr.SetVisibleControl(Me, Req_Doc_TypeLabel, True)
            ControlMgr.SetVisibleControl(Me, Req_Doc_NumLabel, True)

            If Me.State.ViewOnly AndAlso Not Me.State.DisbursementBO Is Nothing AndAlso LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, Me.State.DisbursementBO.PayeeOptionId) = ClaimInvoice.PAYEE_OPTION_OTHER Then
                Me.PopulateControlFromBOProperty(Me.cboDocumentTypeId, LookupListNew.GetIdFromCode(LookupListNew.LK_DOCUMENT_TYPES, Me.State.DisbursementBO.DocumentType))
                Me.PopulateControlFromBOProperty(Me.moTaxIdText, Me.State.DisbursementBO.IdentificationNumber)
            Else
                Me.PopulateControlFromBOProperty(Me.cboDocumentTypeId, LookupListNew.GetIdFromCode(LookupListNew.LK_DOCUMENT_TYPES, Me.State.ClaimInvoiceBO.DocumentType))
                Me.PopulateControlFromBOProperty(Me.moTaxIdText, Me.State.ClaimInvoiceBO.TaxId)
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
        Me.State.PayeeAddress = Me.State.ClaimInvoiceBO.Add_Address() 'New Address
        Me.State.PayeeAddress.Address1 = Me.State.DisbursementBO.Address1
        Me.State.PayeeAddress.Address2 = Me.State.DisbursementBO.Address2
        Me.State.PayeeAddress.City = Me.State.DisbursementBO.City
        Me.State.PayeeAddress.PostalCode = Me.State.DisbursementBO.Zip
        Me.State.PayeeAddress.CountryId = New Company(Me.State.DisbursementBO.CompanyId).CountryId

    End Sub
    Private Sub PopulatePayeeBankInfoBO()
        Me.State.PayeeBankInfo = Me.State.ClaimInvoiceBO.Add_BankInfo() 'New bankinfo
        Me.State.PayeeBankInfo.Account_Name = Me.State.DisbursementBO.AccountName
        Me.State.PayeeBankInfo.Account_Number = Me.State.DisbursementBO.AccountNumber
        Me.State.PayeeBankInfo.Bank_Id = Me.State.DisbursementBO.BankID
        Me.State.PayeeBankInfo.SwiftCode = Me.State.DisbursementBO.SwiftCode
        Me.State.PayeeBankInfo.IbanNumber = Me.State.DisbursementBO.IbanNumber
        Me.State.PayeeBankInfo.AccountTypeId = LookupListNew.GetIdFromDescription(LookupListNew.LK_ACCOUNT_TYPES, Me.State.DisbursementBO.AccountType)
        Me.State.PayeeBankInfo.CountryID = LookupListNew.GetIdFromDescription(LookupListNew.LK_COUNTRIES, Me.State.DisbursementBO.Country)
        Me.State.PayeeBankInfo.PaymentReasonID = LookupListNew.GetIdFromDescription(LookupListNew.LK_PAYMENTREASON, Me.State.DisbursementBO.PaymentReason)
        Me.State.PayeeBankInfo.SourceCountryID = oCompany.CountryId
        Me.State.PayeeBankInfo.TaxId = Me.State.DisbursementBO.IdentificationNumber
    End Sub

    Private Function IsGiftCardValid() As Boolean

        Dim listItem As ListItem = PaymentMethodDrop.Items.FindByText(LookupListNew.GetDescrionFromListCode(
                                                                      "PMTHD", Codes.PAYMENT_METHOD__DARTY_GIFT_CARD))
        If (Not listItem Is Nothing) Then
            If (cboPayeeSelector.SelectedItem.Text.ToUpper = Payee_Customer AndAlso PaymentMethodDrop.SelectedItem.Text = listItem.Text) Then
                If (Me.oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).Count > 0) Then
                    Dim attvalue As AttributeValue = Me.oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).First()
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


        With Me.State.ClaimBO
            Dim attvalue As AttributeValue = Me.oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.ATTR_DARTY_GIFT_CARD_TYPE).FirstOrDefault

            If Not attvalue Is Nothing Then
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
