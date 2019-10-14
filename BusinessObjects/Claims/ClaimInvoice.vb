'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/15/2004)  ********************

#Region "Public Interfaces"

Public Interface IInvoiceable
    ReadOnly Property Claim_Id As Guid
    ReadOnly Property ClaimAuthorizationId As Guid
    Property ClaimNumber As String
    'ReadOnly Property SvcControlNumber As String
    Property RepairEstimate As DecimalType

    Property CauseOfLossId As Guid
    Property CompanyId As Guid
    Property ServiceCenterId As Guid
    Property RepairCodeId As Guid
    Property AuthorizationNumber As String
    Property AuthorizedAmount As DecimalType
    Property RepairDate As DateType
    Property PickUpDate As DateType
    Property InvoiceProcessDate As DateType
    Property SalvageAmount As DecimalType

    Property IsComingFromPayClaim As Boolean
    ReadOnly Property ClaimActivityCode As String
    Property ClaimActivityId As Guid
    Property ReasonClosedId As Guid
    Property ClaimClosedDate As DateType
    Property InvoiceDate As DateType

    ReadOnly Property CertificateId As Guid
    ReadOnly Property CustomerName As String
    Property CertItemCoverageId As Guid
    Property IsRequiredCheckLossDateForCancelledCert As Boolean
    Property StatusCode As String
    ReadOnly Property ServiceCenterObject As ServiceCenter
    ReadOnly Property CreatedDate As DateType
    ReadOnly Property CreatedDateTime As DateTimeType
    Property LoanerCenterId() As Guid
    Property LoanerReturnedDate() As DateType
    ReadOnly Property IsDirty() As Boolean
    ReadOnly Property CanDisplayVisitAndPickUpDates() As Boolean
    ReadOnly Property RiskType() As String
    ReadOnly Property MethodOfRepairCode As String
    Property RiskTypeId() As Guid

    ReadOnly Property AssurantPays As DecimalType
    Property Deductible() As DecimalType
    Property DiscountAmount() As DecimalType
    ReadOnly Property AboveLiability() As DecimalType
    ReadOnly Property PayDeductibleId() As Guid
    ReadOnly Property ConsumerPays() As DecimalType
    Property LiabilityLimit() As DecimalType

    'KDDI changes'
    ReadOnly Property IsReshipmentAllowed() As String
    ReadOnly Property IsCancelShipmentAllowed() As String
    'Methods to be implemented
    Sub SaveClaim(Optional ByVal Transaction As IDbTransaction = Nothing)
    Sub VerifyConcurrency(ByVal sModifiedDate As String)
    Sub CloseTheClaim()
    Sub CalculateFollowUpDate()
    Sub HandleGVSTransactionCreation(ByVal commentId As Guid, ByVal pIsNew As Nullable(Of Boolean))
    Function AddExtendedClaimStatus(ByVal claimStatusId As Guid) As ClaimStatus
    Sub SetPickUpDateFromLoanerReturnedDate()

End Interface

#End Region

Public Class ClaimInvoice
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const PAYEE_OPTION_MASTER_CENTER = "1"
    Public Const PAYEE_OPTION_SERVICE_CENTER = "2"
    Public Const PAYEE_OPTION_LOANER_CENTER = "3"
    Public Const PAYEE_OPTION_CUSTOMER = "4"
    Public Const PAYEE_OPTION_OTHER = "5"

    Public Const INVOICE_METHOD_DETAIL = "1"
    Public Const INVOICE_METHOD_TOTAL = "2"
    Public Const REVERSE_PAYMENT_COMMENT_TYPE_CODE = "PAYR"
    Public Const ADJUST_PAYMENT_COMMENT_TYPE_CODE = "PADJ"
    Private Const REVERSE_MULTIPLIER As Integer = -1
    Public Const PROD_LIABILITY_LIMIT_CNL_POLICY As String = "CNLCERT"

#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub


    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ClaimInvoiceDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ClaimInvoiceDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    'Private _serviceCenterName As String = ""
    Private _disbursement As Disbursement = Nothing
    Private _comment As Comment = Nothing
    'Private _claim As Claim = Nothing
    Private _cert_Item As CertItem = Nothing
    Private _claimInvoice As ClaimInvoice = Nothing
    Private _ClaimTax As ClaimTax = Nothing
    Private _ClaimTaxManual As ClaimTax = Nothing

    Private _closeClaim As Boolean = False
    Private _isPaymentAdjustment As Boolean = False
    Private _isPaymentReversal As Boolean = False
    Private _isSalvagePayment As Boolean = False
    Private _isNewPaymentFromPaymentAdjustment As Boolean = False
    Private _currentCommentId As Guid
    Private _adjustmentAmount As DecimalType
    Private _totalAmount As DecimalType
    Private _salvageamt As DecimalType
    Private _reconciledAmount As DecimalType
    'temp storage
    Private _serialNumberTempContainer As String
    Private _pickupdate As DateType
    Private _TaxRateChecked As Boolean = False
    Private _TaxRate As Decimal = -1D
    Private _DeductibleTaxRateChecked As Boolean = False
    Private _DeductibleTaxRate As Decimal = -1D
    Private _service_center_withhodling_rate As DecimalType
    'Cancel Certificate
    Private _cancelCertificateData As CertCancellationData

    Private _payeeAddress As Address
    Private _payeeBankInfo As BankInfo
    Private _payeeOptionCode As String = String.Empty
    Private _paymentMethodCode As String = String.Empty
    Private _paymentMethodID As Guid = Guid.Empty
    Private _taxId As String = String.Empty
    Private _documentType As String = String.Empty
    Private _cancelPolicy As Boolean = True

    Private _company As Company
    Private _invoiceable As IInvoiceable
    Private _claimTaxRatesData As ClaimInvoiceDAL.ClaimTaxRatesData


    Private Function getDecimalValue(ByVal decimalObj As DecimalType, Optional ByVal decimalDigit As Integer = 2) As Decimal
        If decimalObj Is Nothing Then
            Return 0D
        Else
            Return Math.Round(decimalObj.Value, decimalDigit, MidpointRounding.AwayFromZero)
        End If
    End Function

    Public Sub CreateDisbursement()
        Dim oRegion As Region
        If Me.ClaimAuthorizationId = Guid.Empty And Me.Dataset.Tables(ClaimInvoiceDAL.TABLE_NAME).Rows.Count > 1 Then
            Dim row As DataRow
            For Each row In Me.Dataset.Tables(ClaimInvoiceDAL.TABLE_NAME).Rows
                Dim ci As New ClaimInvoice(row)

                ReInitiateFieldsNotIncludedInDataSet(ci)
                ci.CurrentDisbursement.PrepopulateFromClaimInvoice(ci)
                If Not ci.RegionId = Guid.Empty Then
                    oRegion = New Region(ci.RegionId)
                    ci.CurrentDisbursement.VendorRegionDesc = oRegion.Description
                Else
                    ci.CurrentDisbursement.VendorRegionDesc = String.Empty
                End If

                ci.CurrentDisbursement.perceptionIVA = Me.PerceptionIVA
                ci.CurrentDisbursement.perceptionIIBB = Me.PerceptionIIBB
                ci.CurrentDisbursement.Save()
                ci.DisbursementId = ci.CurrentDisbursement.Id

            Next
        Else
            CurrentDisbursement.PrepopulateFromClaimInvoice(Me)
            If Not Me.RegionId = Guid.Empty Then
                oRegion = New Region(Me.RegionId)
                CurrentDisbursement.VendorRegionDesc = oRegion.Description
            Else
                CurrentDisbursement.VendorRegionDesc = String.Empty
            End If
            CurrentDisbursement.perceptionIVA = Me.PerceptionIVA
            CurrentDisbursement.perceptionIIBB = Me.PerceptionIIBB
            CurrentDisbursement.Save()
            Me.DisbursementId = CurrentDisbursement.Id
        End If
    End Sub

    Private Sub CreateClaimTax()

        Me.ClaimTax.ClaimInvoiceId = Me.Id
        Me.ClaimTax.DisbursementId = Me.DisbursementId
        Me.ClaimTax.TaxTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, "4")
        Me.ClaimTax.Tax1Amount = Me.PerceptionIVA
        Me.ClaimTax.Tax1Description = "Peception_IVA"
        Me.ClaimTax.Tax2Amount = Me.PerceptionIIBB
        Me.ClaimTax.Tax2Description = "Peception_IIBB"
        Me.ClaimTax.Save()

    End Sub

    Public Sub CreateManualClaimTaxes(ByVal strTax1Desc As String, ByVal dTax1Amt As DecimalType, _
                                      ByVal strTax2Desc As String, ByVal dTax2Amt As DecimalType, _
                                      ByVal strTax3Desc As String, ByVal dTax3Amt As DecimalType, _
                                      ByVal strTax4Desc As String, ByVal dTax4Amt As DecimalType, _
                                      ByVal strTax5Desc As String, ByVal dTax5Amt As DecimalType, _
                                      ByVal strTax6Desc As String, ByVal dTax6Amt As DecimalType)
        ClaimTaxManual.ClaimInvoiceId = Me.Id
        ClaimTaxManual.DisbursementId = Me.DisbursementId
        ClaimTaxManual.TaxTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, "7")
        If strTax1Desc <> String.Empty OrElse dTax1Amt.Value > 0 Then
            ClaimTaxManual.Tax1Description = strTax1Desc
            ClaimTaxManual.Tax1Amount = dTax1Amt
        End If
        If strTax2Desc <> String.Empty OrElse dTax2Amt.Value > 0 Then
            ClaimTaxManual.Tax2Description = strTax2Desc
            ClaimTaxManual.Tax2Amount = dTax2Amt
        End If
        If strTax3Desc <> String.Empty OrElse dTax3Amt.Value > 0 Then
            ClaimTaxManual.Tax3Description = strTax3Desc
            ClaimTaxManual.Tax3Amount = dTax3Amt
        End If
        If strTax4Desc <> String.Empty OrElse dTax4Amt.Value > 0 Then
            ClaimTaxManual.Tax4Description = strTax4Desc
            ClaimTaxManual.Tax4Amount = dTax4Amt
        End If
        If strTax5Desc <> String.Empty OrElse dTax5Amt.Value > 0 Then
            ClaimTaxManual.Tax5Description = strTax5Desc
            ClaimTaxManual.Tax5Amount = dTax5Amt
        End If
        If strTax6Desc <> String.Empty OrElse dTax6Amt.Value > 0 Then
            ClaimTaxManual.Tax6Description = strTax6Desc
            ClaimTaxManual.Tax6Amount = dTax6Amt
        End If
    End Sub

    Private Sub ReInitiateFieldsNotIncludedInDataSet(ByVal ci As ClaimInvoice)
        ' fields not included in the dataset need to reinitiate here
        ci.CloseClaim = True
        ci.PayeeOptionCode = Me.PayeeOptionCode
        ci.IsNewPaymentFromPaymentAdjustment = Me.IsNewPaymentFromPaymentAdjustment
        ci.PaymentMethodCode = Me.PaymentMethodCode
        ci.PaymentMethodID = Me.PaymentMethodID
        ci.DocumentType = Me.DocumentType
        ci.PayeeBankInfo = Me.PayeeBankInfo
        ci.PayeeAddress = Me.PayeeAddress
        ci.TaxId = Me.TaxId
    End Sub

    Private Sub ProcessClaim(Optional ByVal Transaction As IDbTransaction = Nothing)
        If Me.ClaimAuthorizationId = Guid.Empty And Me.Dataset.Tables(ClaimInvoiceDAL.TABLE_NAME).Rows.Count > 1 Then
            Dim row As DataRow
            For Each row In Me.Dataset.Tables(ClaimInvoiceDAL.TABLE_NAME).Rows
                Dim ci As New ClaimInvoice(row)

                'Directly use the Invoiceable Property
                ReInitiateFieldsNotIncludedInDataSet(ci)
                ci.Invoiceable.InvoiceProcessDate = New DateType(Date.Now)
                ci.Invoiceable.CauseOfLossId = ci.CauseOfLossID
                ci.Invoiceable.RepairDate = ci.RepairDate
                ci.Invoiceable.RepairCodeId = ci.RepairCodeId
                ci.Invoiceable.PickUpDate = ci.PickUpDate
                'ci.CurrentClaim.SalvageAmount = ci.SalvageAmt
                If Not ci.InvoiceDate Is Nothing Then
                    ci.Invoiceable.InvoiceDate = ci.InvoiceDate
                End If
                ci.CloseClaim = True
                ci.Invoiceable.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_PAID)
                ci.Invoiceable.IsComingFromPayClaim = True
                ci.Invoiceable.CloseTheClaim()

                ci.Invoiceable.CalculateFollowUpDate()

                'disable the loss date check for cancelled certification when paying claim
                ci.Invoiceable.IsRequiredCheckLossDateForCancelledCert = False

                ci.Invoiceable.SaveClaim(Transaction)

                If (ci.CancelPolicy AndAlso (Not ci.Invoiceable.CertificateId.Equals(Guid.Empty))) Then
                    Dim certBO As Certificate = New Certificate(ci.Invoiceable.CertificateId, Me.Dataset)
                    If certBO.StatusCode <> Codes.CLAIM_STATUS__CLOSED Then
                        If ci.Invoiceable.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REPLACED AndAlso CanCancelCertificateBasedOnContractReplacementPolicy(certBO.DealerId, certBO.WarrantySalesDate.Value) Then
                            PrepareCancelCertificate(certBO)
                        End If
                    Else
                        '??
                    End If
                End If

            Next

        Else

            Me.Invoiceable.InvoiceProcessDate = New DateType(Date.Now)
            Me.Invoiceable.CauseOfLossId = Me.CauseOfLossID
            'If Me.ClaimAuthorizationId = Guid.Empty Then
            Me.Invoiceable.RepairDate = Me.RepairDate
            Me.Invoiceable.RepairCodeId = Me.RepairCodeId
            Me.Invoiceable.PickUpDate = Me.PickUpDate
            Me.Invoiceable.IsComingFromPayClaim = True
            'End If

            'CurrentClaim.SalvageAmount = Me.SalvageAmt

            'Logic to Decide whether to Close the Claim along with the current Payment
            If Me.ClaimAuthorizationId = Guid.Empty Then 'Single Auth Claims
                If Invoiceable.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT And Me.RemainingAmount.Value = 0 Then  'And Me.CloseClaim Then
                    Invoiceable.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__REPLACED)
                    Invoiceable.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_REPAIRED)
                    Invoiceable.CloseTheClaim()
                ElseIf Me.CloseClaim Or (Invoiceable.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT And Me.RemainingAmount.Value = 0) Then
                    Invoiceable.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_PAID)
                    Invoiceable.CloseTheClaim()
                End If
            Else 'Multi Auth Claims
                If Me.CloseClaim And Invoiceable.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT And Me.RemainingAmount.Value = 0 Then
                    Invoiceable.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__REPLACED)
                    Invoiceable.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_REPAIRED)
                    Invoiceable.CloseTheClaim()
                ElseIf Me.CloseClaim And (Invoiceable.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT And Me.RemainingAmount.Value = 0) Then
                    Invoiceable.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListNew.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_PAID)
                    Invoiceable.CloseTheClaim()
                End If
            End If



            Invoiceable.CalculateFollowUpDate()

            'disable the loss date check for cancelled certification when paying claim
            Invoiceable.IsRequiredCheckLossDateForCancelledCert = False
            If Invoiceable.StatusCode = Codes.CLAIM_STATUS__CLOSED And Not Me.InvoiceDate Is Nothing Then
                Invoiceable.InvoiceDate = Me.InvoiceDate
                CurrentDisbursement.InvoiceDate = Me.InvoiceDate
            End If
            Invoiceable.SaveClaim(Transaction)

            If (CancelPolicy AndAlso (Not Invoiceable.CertificateId.Equals(Guid.Empty))) Then
                Dim certBO As Certificate = New Certificate(Invoiceable.CertificateId, Me.Dataset)
                Dim CertItemCoverageBO As CertItemCoverage = New CertItemCoverage(Invoiceable.CertItemCoverageId, Me.Dataset)
                If certBO.StatusCode <> Codes.CLAIM_STATUS__CLOSED Then
                    If Invoiceable.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REPLACED AndAlso CanCancelCertificateBasedOnContractReplacementPolicy(certBO.DealerId, certBO.WarrantySalesDate.Value) Then
                        PrepareCancelCertificate(certBO)
                    End If
                End If
                Dim claimBO As Claim = New Claim(Invoiceable.Claim_Id)
                If (_cancelCertificateData Is Nothing And CType(certBO.ProductLiabilityLimit.ToString, Decimal) > 0 And certBO.ProdLiabilityPolicyCd.ToString = PROD_LIABILITY_LIMIT_CNL_POLICY) Then

                    If ((CType(ProductRemainLiabilityAmount(Invoiceable.CertificateId, claimBO.LossDate), Decimal) - Me.Amount.Value <= 0)) Then

                        PrepareCancelCertificateForLiabilityLimit(certBO)
                    End If
                End If

                If (_cancelCertificateData Is Nothing And CType(CertItemCoverageBO.CoverageLiabilityLimit.ToString, Decimal) > 0 And certBO.ProdLiabilityPolicyCd.ToString = PROD_LIABILITY_LIMIT_CNL_POLICY) Then

                    If (CType(IsCertCoveragesEligbileforCancel(Invoiceable.CertificateId, Invoiceable.CertItemCoverageId, claimBO.LossDate).ToString, Integer) = 0 And
                            CType(CoverageRemainLiabilityAmount(Invoiceable.CertItemCoverageId, claimBO.LossDate), Decimal) - Me.Amount.Value <= 0) Then
                        PrepareCancelCertificateForLiabilityLimit(certBO)
                    End If
                End If
            End If
            End If

            Me.HandelExtendedStatusForGVS(True, False, False)
    End Sub
    'for DEF366
    Private Sub HandelExtendedStatusForGVS(ByVal blnForPayClaim As Boolean, ByVal blnForAdjustment As Boolean, ByVal blnForReversal As Boolean)
        'Add an INVOICE_PAID extended claim status when open paying a claim with GVS integrated
        ' Create transaction log header if the service center is integrated with GVS
        If Not Me.Invoiceable.ServiceCenterObject Is Nothing AndAlso Me.Invoiceable.ServiceCenterObject.IntegratedWithGVS AndAlso Not Me.Invoiceable.ServiceCenterObject.IntegratedAsOf Is Nothing AndAlso Me.Invoiceable.CreatedDateTime.Value >= Me.Invoiceable.ServiceCenterObject.IntegratedAsOf.Value Then
            Dim newClaimStatusByGroupId As Guid = Guid.Empty
            If blnForPayClaim Then
                newClaimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(DALObjects.ClaimStatusDAL.INVOICE_PAID_EXTENDED_CLAIM_STATUS)
            ElseIf blnForReversal Then
                newClaimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(DALObjects.ClaimStatusDAL.WAITING_DOCUMENTATION)
            ElseIf blnForAdjustment Then
                'no action
            End If

            If (blnForPayClaim Or blnForReversal) And Not blnForAdjustment Then
                Dim oExtendedeClaimStatus As ClaimStatus = Nothing
                oExtendedeClaimStatus = Me.Invoiceable.AddExtendedClaimStatus(Guid.Empty)
                oExtendedeClaimStatus.ClaimId = Me.Invoiceable.Claim_Id
                oExtendedeClaimStatus.ClaimStatusByGroupId = newClaimStatusByGroupId
                oExtendedeClaimStatus.StatusDate = DateTime.Now
                oExtendedeClaimStatus.HandelTimeZoneForClaimExtStatusDate()
                ' Create transaction log header if the service center is integrated with GVS
                Me.Invoiceable.HandleGVSTransactionCreation(Guid.Empty, Nothing)
            End If

        End If
    End Sub
    Public Shared Function IsCertCoveragesEligbileforCancel(ByVal CertId As Guid, ByVal CertItemCoverageId As Guid, ByVal lossDate As DateType) As Integer
        Dim dal As New ClaimDAL
        Return dal.IsCertCoveragesEligibleforCancel(CertId, CertItemCoverageId, lossDate)
    End Function
    Public Shared Function ProductRemainLiabilityAmount(ByVal CertId As Guid, ByVal lossDate As DateType) As Decimal
        Dim dal As New ClaimDAL
        Return dal.ProductRemainLiabilityAmount(CertId, lossDate)
    End Function
    Public Shared Function CoverageRemainLiabilityAmount(ByVal CertItemCoverageId As Guid, ByVal lossDate As DateType) As Decimal
        Dim dal As New ClaimDAL
        Return dal.CoverageRemainLiabilityAmount(CertItemCoverageId, lossDate)
    End Function
    Private Sub PrepareCancelCertificate(ByVal certBO As Certificate)
        _cancelCertificateData = New CertCancellationData
        Dim parentCertId As Guid = Nothing

        If certBO.IsChildCertificate Then
            Dim parentCertBO As Certificate
            Dim dal As New CertificateDAL
            parentCertId = dal.GetParentCertId(certBO.Id)
        End If

        If certBO.IsChildCertificate And parentCertId <> Nothing Then
            Dim parentCertBO = New Certificate(parentCertId)
            With _cancelCertificateData
                .companyId = parentCertBO.CompanyId
                .dealerId = parentCertBO.DealerId
                .certificate = parentCertBO.CertNumber
                .source = parentCertBO.Source
                .cancellationDate = Today
                .cancellationCode = Codes.REASON_CLOSED__TO_BE_REPAIRED
                .customerPaid = getDecimalValue(Me.Amount)
                .certificatestatus = parentCertBO.StatusCode
                .quote = "N"
            End With
        Else
            With _cancelCertificateData
                .companyId = Me.CompanyId
                .dealerId = certBO.DealerId
                .certificate = certBO.CertNumber
                .source = certBO.Source
                .cancellationDate = Today
                .cancellationCode = Codes.REASON_CLOSED__TO_BE_REPAIRED
                .customerPaid = getDecimalValue(Me.Amount)
                .certificatestatus = certBO.StatusCode
                .quote = "N"
            End With
        End If

        'This line has moved to be included in the transaction.
        'CertCancellation.CancelCertificate(oCancelCertificateData)
    End Sub

    Private Sub PrepareCancelCertificateForLiabilityLimit(ByVal certBO As Certificate)
        _cancelCertificateData = New CertCancellationData
        With _cancelCertificateData
            .companyId = Me.CompanyId
            .dealerId = certBO.DealerId
            .certificate = certBO.CertNumber
            .source = certBO.Source
            .cancellationDate = Today
            .cancellationCode = Codes.REASON_CLOSED_LLE
            .customerPaid = getDecimalValue(Me.Amount)
            .certificatestatus = certBO.StatusCode
            .quote = "N"
        End With

        'This line has moved to be included in the transaction.
        'CertCancellation.CancelCertificate(oCancelCertificateData)
    End Sub
    Private Function CanCancelCertificateBasedOnContractReplacementPolicy(ByVal dealerID As Guid, ByVal WarrantySalesDate As Date) As Boolean
        Dim contractBo As Contract = Contract.GetContract(dealerID, WarrantySalesDate)
        If contractBo Is Nothing Then
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.DEALER_DOES_NOT_HAVE_CURRENT_CONTRACT, GetType(ClaimInvoice), Nothing, Nothing, Nothing)}
            Throw New BOValidationException(errors, GetType(ClaimInvoice).FullName, Me.UniqueId)
        End If


        If contractBo.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCL)) Then
            'Cancel Certificate'
            'Changes for REQ-1333, cancel cert after X number of replacement claims when cancel after paid

            Dim lngRepPolicyClaimCnt As Long = ReppolicyClaimCount.GetReplacementPolicyClaimCntByClaim(contractBo.Id, Me.ClaimId)

            If lngRepPolicyClaimCnt = 1 Then 'if only require 1 replacement, cancel based on current replacement claim
                Return True
            End If

            'more than 1 replacement claims required before cancelling the cert
            Dim certBO As Certificate = New Certificate(Invoiceable.CertificateId, Me.Dataset)
            Dim paidReplacementClaimCnt As Integer
            Dim Claimlist As Certificate.CertificateClaimsDV

            If certBO.StatusCode = "A" Then 'only cancel if certificate is active
                Claimlist = certBO.ClaimsForCertificate(certBO.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                paidReplacementClaimCnt = 0
                If Claimlist.Count > 0 Then
                    Dim i As Integer, dblPayment As Double, objClaim As Claim, dt As New DataSet
                    Dim dvPaymentList As ClaimInvoicesDV
                    For i = 0 To Claimlist.Count - 1
                        dblPayment = Claimlist(i)(Certificate.CertificateClaimsDV.COL_TOTAL_PAID)
                        If dblPayment > 0 AndAlso Claimlist(i)(Certificate.CertificateClaimsDV.COL_CLAIM_NUMBER) <> Me.ClaimNumber _
                            AndAlso Claimlist(i)(Certificate.CertificateClaimsDV.COL_Method_Of_Repair_code) = "R" Then
                            'paid replacement claim
                            paidReplacementClaimCnt = paidReplacementClaimCnt + 1
                            If lngRepPolicyClaimCnt - 1 <= paidReplacementClaimCnt Then
                                Return True
                            End If
                        End If
                    Next
                End If
            End If
        ElseIf contractBo.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CLAST)) Then
            'Cancel Certificate in last 12 months'
            Dim certItemCoverageBO As CertItemCoverage = New CertItemCoverage(Invoiceable.CertItemCoverageId)
            If Invoiceable.CreatedDate.Value >= certItemCoverageBO.EndDate.Value.AddYears(-1).AddDays(1) Then
                Return True
            Else
                Return False
            End If
        ElseIf contractBo.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__KEEP)) _
            OrElse contractBo.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCLAF)) Then
            'Keep Certificate or cancel cert after fulfillment'
            Return False
        Else
            Throw New BOInvalidOperationException(Common.ErrorCodes.UNKNOWN_REPLACEMENT_POLICY_ERR)
        End If

    End Function

#End Region

#Region "Public Members"

    'Reconciled Amount is used for Multi Auth Claim as generated by the Invoice Balancing Process
    'For Multi Auth Claims we assume Amounts to be calculated as  Invoice : Total Entry Method
    Public Sub CalculateAmountsForMultiAuthClaim()
        Dim objclaimAuth As ClaimAuthorization
        Dim objInvoice As Invoice
        Dim subTotalAmt As Decimal
        Dim GrossAmount As Decimal
        Me.LaborAmt = New DecimalType(0D)
        Me.LaborTax = New DecimalType(0D)
        Me.PartAmount = New DecimalType(0D)
        Me.PartTax = New DecimalType(0D)
        Me.ServiceCharge = New DecimalType(0D)
        Me.TripAmount = New DecimalType(0D)
        Me.ShippingAmount = New DecimalType(0D)
        Me.DispositionAmount = New DecimalType(0D)
        Me.DiagnosticsAmount = New DecimalType(0D)
        Me.PaytocustomerAmount = New DecimalType(0D)

        objclaimAuth = DirectCast(New ClaimAuthorization(Me.ClaimAuthorizationId, Me.Dataset), ClaimAuthorization)

        If objclaimAuth.ContainsDeductible And objclaimAuth.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.AUTH_LESS_DEDUCT_Y) Then

            'The Payment Amount and the IVA Tax Calculation should not contain PayDeductible Amount
            Me.Amount = Me.ReconciledAmount.Value - objclaimAuth.PayDeductibleAmount

            'Set Deductible and Deductible tax Amount
            If Me.excludeDeductible Then
                Me.DeductibleAmount = Nothing
                Me.DeductibleTaxAmount = Nothing
            Else
                Me.DeductibleAmount = objclaimAuth.PayDeductibleAmount
                Dim dedTaxAmt As Decimal = (Me.DeductibleAmount * getDecimalValue(Me.DeductibleTaxRate)) / 100
                Me.DeductibleTaxAmount = New DecimalType(dedTaxAmt)
            End If
        Else
            'The Payment Amount Remains unchanged
            Me.Amount = Me.ReconciledAmount.Value
            Me.DeductibleAmount = Nothing
            Me.DeductibleTaxAmount = Nothing
        End If

        Me.OtherExplanation = "NET"
        GrossAmount = Me.Amount.Value
        Me.IvaAmount = GrossAmount - (Math.Round((getDecimalValue(GrossAmount) * 100) / (100 + getDecimalValue(Me.TaxRate)), 2))
        Me.OtherAmount = GrossAmount - Me.IvaAmount

        'Pay Invoice Taxes if they are configured for the claim Authoization's Service Center
        'Taxes should be only paid once and with the first Claim Authoirzation of the Invoice 
        If Me.isTaxTypeInvoice() Then
            'Check if this is the first Claim Authorization to be paid for this Invoice
            If Not Me.Invoice.IsAnyClaimAuthorizationPaid Then
                'Get the Perception IVA and Perception IIBB for this Invoice from the Attributes
                Me.PerceptionIVA = Me.Invoice.PerceptionIVA
                Me.PerceptionIIBB = Me.Invoice.PerceptionIIBB
                Me.RegionId = Me.Invoice.PerceptionIIBBRegion
            End If
        End If

    End Sub

    Public Sub CalculateAmounts(Optional blnExcludeTaxComputation As Boolean = False)
        Dim invMethodDV As DataView = LookupListNew.GetInvoiceMethodLookupList()
        Dim invMethodDesc As String = LookupListNew.GetDescriptionFromId(invMethodDV, Me.Company.InvoiceMethodId)

        Dim methodOfRepair As String
        If Me.Invoiceable.MethodOfRepairCode = Codes.METHOD_OF_REPAIR_REPLACEMENT Or Me.Invoiceable.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            methodOfRepair = "RPL"
        Else
            methodOfRepair = "RPR"
        End If

        If invMethodDesc = INVOICE_METHOD_DETAIL Or Me.Company.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) Then  ' detail entry
            Dim subTotalAmt As Decimal = getDecimalValue(Me.LaborAmt) +
                            getDecimalValue(Me.LaborTax) +
                            getDecimalValue(Me.PartAmount) +
                            getDecimalValue(Me.PartTax) +
                            getDecimalValue(Me.ServiceCharge) +
                            getDecimalValue(Me.TripAmount) +
                            getDecimalValue(Me.OtherAmount) +
                            getDecimalValue(Me.ShippingAmount) +
                            getDecimalValue(Me.DispositionAmount) +
                            getDecimalValue(Me.DiagnosticsAmount) +
                            getDecimalValue(Me.PaytocustomerAmount)

            If Me.IsSalvagePayment Or getDecimalValue(Me.Invoiceable.SalvageAmount) > 0 Then
                subTotalAmt = subTotalAmt - getDecimalValue(Me.Invoiceable.SalvageAmount)
            End If

            Dim taxAmt As Decimal = 0
            If Not blnExcludeTaxComputation AndAlso Me.TotalTaxAmount = 0 AndAlso subTotalAmt > 0 Then
                Me.WithholdingAmount = 0
                taxAmt = ComputeTotalTax(invMethodDesc, subTotalAmt, methodOfRepair)
            Else
                taxAmt = Me.TotalTaxAmount
            End If
            'Dim taxAmt As Decimal = (subTotalAmt * getDecimalValue(Me.TaxRate)) / 100

            If Me.IsIvaResponsibleFlag Then
                Me.IvaAmount = New DecimalType(taxAmt)
            ElseIf Me.TotalTaxAmount = 0 Then
                Me.TotalTaxAmount = New DecimalType(taxAmt)
            End If

            Me.Amount = New DecimalType(subTotalAmt + taxAmt)
            If Me.Company.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) Then
                Dim dblDeductible As Decimal
                Try
                    dblDeductible = Invoiceable.Deductible.Value
                Catch ex As Exception
                    dblDeductible = 0
                End Try
                Me.Amount = Me.Amount.Value - dblDeductible
                If Amount.Value < 0 Then 'if auth amount less than deductible, no payment
                    Amount = 0
                End If
            End If
        ElseIf invMethodDesc = INVOICE_METHOD_TOTAL Then ' total entry
            'Me.Amount is populated from the screen

            Me.LaborAmt = New DecimalType(0D)
            Me.LaborTax = New DecimalType(0D)
            Me.PartAmount = New DecimalType(0D)
            Me.PartTax = New DecimalType(0D)
            Me.ServiceCharge = New DecimalType(0D)
            Me.TripAmount = New DecimalType(0D)
            Me.ShippingAmount = New DecimalType(0D)
            Me.DispositionAmount = New DecimalType(0D)
            Me.DiagnosticsAmount = New DecimalType(0D)
            Me.PaytocustomerAmount = New DecimalType(0D)

            Me.OtherExplanation = "NET"
            Dim taxRate As DecimalType = GetTaxRateOnOther(invMethodDesc, methodOfRepair)

            Dim subTotalAmt As Decimal = Math.Round((getDecimalValue(Me.Amount) * 100) / (100 + getDecimalValue(taxRate)), 2)
            Me.OtherAmount = New DecimalType(subTotalAmt)

            If Me.IsIvaResponsibleFlag Then
                Me.IvaAmount = New DecimalType(getDecimalValue(Me.Amount) - subTotalAmt)
            ElseIf (getDecimalValue(Me.Amount) - subTotalAmt) > 0 Then
                Me.TotalTaxAmount = New DecimalType(getDecimalValue(Me.Amount) - subTotalAmt)
            End If

        Else
            Throw New NotSupportedException()
        End If

    End Sub

    Public Function GetTaxRateOnOther(invMethodDesc As String, MethodOfRepair As String) As DecimalType
        Dim OtherTaxRate As DecimalType = 0

        Dim RegionId As Guid
        If Me.PayeeAddress Is Nothing Then
            Dim addressObj As New Address(Me.ServiceCenterAddressID)
            RegionId = addressObj.RegionId
        Else
            RegionId = Me.PayeeAddress.RegionId
        End If

        With Me.ClaimTaxRatesData(RegionId, MethodOfRepair)

            If invMethodDesc = INVOICE_METHOD_DETAIL Then ' detail entry
                'N/A
            ElseIf invMethodDesc = INVOICE_METHOD_TOTAL Then ' total entry
                'find tax rate for other; if not, then use (claim_tax_type 7)
                If .taxRateClaimOther > 0 Then
                    OtherTaxRate = New DecimalType(.taxRateClaimOther)
                Else
                    'IVA tax
                    OtherTaxRate = Me.TaxRate

                End If
            Else
                Throw New NotSupportedException()
            End If

        End With

        Return OtherTaxRate

    End Function

    Public Function ComputeTotalTax(invMethodDesc As String, subTotalAmt As Decimal, MethodOfRepair As String) As Decimal
        Dim taxAmt As Decimal = 0
        Dim RegionId As Guid
        If Me.PayeeAddress Is Nothing Then
            Dim addressObj As New Address(Me.ServiceCenterAddressID)
            RegionId = addressObj.RegionId
        Else
            RegionId = Me.PayeeAddress.RegionId
        End If
        If Not Me.ClaimTaxRatesData(RegionId, MethodOfRepair) Is Nothing Then

            With Me.ClaimTaxRatesData(RegionId, MethodOfRepair)

                If invMethodDesc = INVOICE_METHOD_DETAIL Or Me.Company.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_AUTH_DTL, "ADR")) Then ' detail entry
                    'apply each line detail tax rate as they have been assigned the default/super default value if none was found for the given line (this was done in Oracle pkg)
                    If Not Me.LaborAmt Is Nothing AndAlso Me.LaborAmt.Value > 0 AndAlso .taxRateClaimLabor > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(Me.LaborAmt), .taxRateClaimLabor, .computeMethodCodeClaimLabor)
                        If .applyWithholdingFlagClaimLabor.Equals("Y") Then Me.WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(Me.LaborAmt), Me.ServiceCenterWithholdingRate, "N")
                    End If
                    If Not Me.PartAmount Is Nothing AndAlso Me.PartAmount.Value > 0 AndAlso .taxRateClaimParts > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(Me.PartAmount), .taxRateClaimParts, .computeMethodCodeClaimParts)
                        If .applyWithholdingFlagClaimParts.Equals("Y") Then Me.WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(Me.PartAmount), Me.ServiceCenterWithholdingRate, "N")
                    End If
                    If Not Me.ServiceCharge Is Nothing AndAlso Me.ServiceCharge.Value > 0 AndAlso .taxRateClaimService > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(Me.ServiceCharge), .taxRateClaimService, .computeMethodCodeClaimService)
                        If .applyWithholdingFlagClaimService.Equals("Y") Then Me.WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(Me.ServiceCharge), Me.ServiceCenterWithholdingRate, "N")
                    End If
                    If Not Me.TripAmount Is Nothing AndAlso Me.TripAmount.Value > 0 AndAlso .taxRateClaimTrip > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(Me.TripAmount), .taxRateClaimTrip, .computeMethodCodeClaimTrip)
                        If .applyWithholdingFlagClaimTrip.Equals("Y") Then Me.WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(Me.TripAmount), Me.ServiceCenterWithholdingRate, "N")
                    End If
                    If Not Me.ShippingAmount Is Nothing AndAlso Me.ShippingAmount.Value > 0 AndAlso .taxRateClaimShipping > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(Me.ShippingAmount), .taxRateClaimShipping, .computeMethodCodeClaimShipping)
                        If .applyWithholdingFlagClaimShipping.Equals("Y") Then Me.WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(Me.ShippingAmount), Me.ServiceCenterWithholdingRate, "N")
                    End If
                    If Not Me.DispositionAmount Is Nothing AndAlso Me.DispositionAmount.Value > 0 AndAlso .taxRateClaimDisposition > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(Me.DispositionAmount), .taxRateClaimDisposition, .computeMethodCodeClaimDisposition)
                        If .applyWithholdingFlagClaimDisposition.Equals("Y") Then Me.WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(Me.DispositionAmount), Me.ServiceCenterWithholdingRate, "N")
                    End If
                    If Not Me.DiagnosticsAmount Is Nothing AndAlso Me.DiagnosticsAmount.Value > 0 AndAlso .taxRateClaimDiagnostics > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(Me.DiagnosticsAmount), .taxRateClaimDiagnostics, .computeMethodCodeClaimDiagnostics)
                        If .applyWithholdingFlagClaimDiagnostics.Equals("Y") Then Me.WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(Me.DiagnosticsAmount), Me.ServiceCenterWithholdingRate, "N")
                    End If
                    If Not Me.OtherAmount Is Nothing AndAlso Me.OtherAmount.Value > 0 AndAlso .taxRateClaimOther > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(Me.OtherAmount), .taxRateClaimOther, .computeMethodCodeClaimOther)
                        If .applyWithholdingFlagClaimOther.Equals("Y") Then Me.WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(Me.OtherAmount), Me.ServiceCenterWithholdingRate, "N")
                    End If

                ElseIf invMethodDesc = INVOICE_METHOD_TOTAL Then ' total entry
                    'find super default (claim_tax_type 7); if not found, then look for default based on claim method of repair (NO Line item detail tax rates will be used).
                    If .taxRateClaimSuperDefault > 0 Then
                        taxAmt = computeTaxAmtByComputeMethod(subTotalAmt, .taxRateClaimSuperDefault, .computeMethodCodeClaimSuperDefault)
                    ElseIf .taxRateClaimDefault > 0 Then
                        taxAmt = computeTaxAmtByComputeMethod(subTotalAmt, .taxRateClaimDefault, .computeMethodCodeClaimDefault)
                    End If
                Else
                    Throw New NotSupportedException()
                End If

            End With

        End If

        Return taxAmt

    End Function


    Private Function computeTaxAmtByComputeMethod(amount As DecimalType, TaxRate As DecimalType, ComputeMethodCode As String) As Decimal
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

        Return taxAmount

    End Function

    Public Function GetClaimSumOfDeductibles() As DecimalType
        Dim dal As New ClaimInvoiceDAL
        Return dal.GetClaimSumOfDeductibles(Me.CompanyId, Me.Invoiceable.Claim_Id)
    End Function
    Public Function GetClaimSumofInvoices() As DecimalType
        Dim dal As New ClaimInvoiceDAL
        Return dal.GetClaimSumOfInvoices(Me.CompanyId, Me.Invoiceable.Claim_Id)
    End Function
    Public Function GetPerceptionTax() As DecimalType
        Dim dal As New ClaimInvoiceDAL
        Return dal.GetClaimSumOfInvoices(Me.CompanyId, Me.Invoiceable.Claim_Id)
    End Function

    Public Property Invoiceable(Optional ByVal blnMustReload As Boolean = False) As IInvoiceable
        Get
            If _invoiceable Is Nothing Then
                If Me.ClaimAuthorizationId = Guid.Empty Then
                    _invoiceable = DirectCast(ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.ClaimId, Me.Dataset, blnMustReload), Claim)
                Else
                    _invoiceable = DirectCast(New ClaimAuthorization(Me.ClaimAuthorizationId, Me.Dataset), ClaimAuthorization)
                End If
            End If
            Return _invoiceable
        End Get
        Private Set(ByVal value As IInvoiceable)
            _invoiceable = value
        End Set
    End Property

    Public ReadOnly Property Company As Company
        Get
            If (_company Is Nothing) Then
                If (Me.CompanyId.Equals(Guid.Empty)) Then
                    Return Nothing
                Else
                    Return New Company(Me.CompanyId, Me.Dataset)
                End If
            Else
                Return _company
            End If
        End Get
    End Property

    Public Sub PrepopulateClaimInvoice(ByVal invoiceable As IInvoiceable, Optional blnExcludeTaxComputation As Boolean = False)
        Me.Invoiceable = invoiceable
        Me.ClaimId = invoiceable.Claim_Id
        Me.ClaimAuthorizationId = invoiceable.ClaimAuthorizationId
        Me.ClaimNumber = invoiceable.ClaimNumber
        Me.CauseOfLossID = invoiceable.CauseOfLossId
        Me.CompanyId = invoiceable.CompanyId
        Me.RepairCodeId = invoiceable.RepairCodeId
        Me.AuthorizationNumber = invoiceable.AuthorizationNumber
        Me.RepairDate = invoiceable.RepairDate
        Me.PickUpDate = invoiceable.PickUpDate
        Me.RepairEstimate = invoiceable.RepairEstimate

        If Me.ClaimAuthorizationId = Guid.Empty Then
            If Not invoiceable.PickUpDate Is Nothing AndAlso Not invoiceable.RepairDate Is Nothing Then
                'if backend claim
                Me.SvcControlNumber = invoiceable.AuthorizationNumber
            End If
            CalculateAmounts(blnExcludeTaxComputation)
        Else
            CalculateAmountsForMultiAuthClaim()
            'This is a MultiAuthorization Claim, Check if all the Claim Authorization under this claim have been Paid then Close the Claim
            Me.CloseClaim = True
            Dim oMultiAuthClaim As MultiAuthClaim
            oMultiAuthClaim = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(Me.Invoiceable.Claim_Id, Me.Dataset)
            For Each auth As ClaimAuthorization In oMultiAuthClaim.ClaimAuthorizationChildren
                If auth.Id <> invoiceable.ClaimAuthorizationId Then
                    If (Not auth.ClaimAuthStatus = ClaimAuthorizationStatus.Void And Not auth.ClaimAuthStatus = ClaimAuthorizationStatus.Paid) Then
                        Me.CloseClaim = False
                        Exit For
                    End If
                End If
            Next
        End If

    End Sub

    Public Sub PrepopulateDisbursment()

        Me.CurrentDisbursement.PayeeOptionId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, PAYEE_OPTION_SERVICE_CENTER)

        Me.CurrentDisbursement.ClaimAuthorizationId = Me.ClaimAuthorizationId

        Me.PayeeAddress = New Address(Me.ServiceCenterAddressID)

        Me.CurrentDisbursement.Payee = Me.ServiceCenterName

        Dim svcCenterBO As ServiceCenter = New ServiceCenter(Me.Invoiceable.ServiceCenterId)
        If Not Me.ClaimAuthorizationId = Guid.Empty Then
            Me.PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_SERVICE_CENTER
            If Not svcCenterBO.BankInfoId.Equals(Guid.Empty) Then
                Me.PayeeBankInfo = New BankInfo(svcCenterBO.BankInfoId, Me.Dataset)
            End If
            If Not svcCenterBO.PaymentMethodId.Equals(Guid.Empty) Then
                Me.PaymentMethodCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, svcCenterBO.PaymentMethodId)
            End If
        End If

        If svcCenterBO.PayMaster AndAlso (Not svcCenterBO.MasterCenterId.Equals(Guid.Empty)) Then
            Dim masterCenterBO As ServiceCenter = New ServiceCenter(svcCenterBO.MasterCenterId)
            Me.PayeeAddress = New Address(masterCenterBO.AddressId)
            Me.CurrentDisbursement.Payee = masterCenterBO.Description
            If Not Me.ClaimAuthorizationId = Guid.Empty Then
                Me.PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_MASTER_CENTER
                If Not masterCenterBO.BankInfoId.Equals(Guid.Empty) Then
                    Me.PayeeBankInfo = New BankInfo(masterCenterBO.BankInfoId, Me.Dataset)
                End If
                If Not masterCenterBO.PaymentMethodId.Equals(Guid.Empty) Then
                    Me.PaymentMethodCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, masterCenterBO.PaymentMethodId)
                End If
            End If
        ElseIf Not Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
            Me.CurrentDisbursement.PayeeOptionId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYEE, PAYEE_OPTION_LOANER_CENTER)
            Dim loanerCenterBO As ServiceCenter = New ServiceCenter(Invoiceable.LoanerCenterId)
            Me.PayeeAddress = New Address(loanerCenterBO.AddressId)
            Me.CurrentDisbursement.Payee = loanerCenterBO.Description
            If Not Me.ClaimAuthorizationId = Guid.Empty Then
                Me.PayeeOptionCode = ClaimInvoice.PAYEE_OPTION_LOANER_CENTER
            End If
        End If
    End Sub

    Public Sub PopulateClaimInvoice(ByVal objOtherClaimInvoiceBO As ClaimInvoice)
        With objOtherClaimInvoiceBO
            Me.CompanyId = .CompanyId
            Me.ClaimId = .ClaimId
            Me.RepairCodeId = .RepairCodeId
            Me.CauseOfLossID = .CauseOfLossID
            Me.ClaimNumber = .ClaimNumber
            Me.SvcControlNumber = .SvcControlNumber
            Me.RecordCount = .RecordCount
            Me.BatchNumber = .BatchNumber
            Me.RepairDate = .RepairDate
            Me.RepairEstimate = .RepairEstimate
            Me.LaborAmt = .LaborAmt
            Me.LaborTax = .LaborTax
            Me.PartAmount = .PartAmount
            Me.PartTax = .PartTax
            Me.ServiceCharge = .ServiceCharge
            Me.TripAmount = .TripAmount
            Me.OtherAmount = .OtherAmount
            Me.OtherExplanation = .OtherExplanation
            Me.IvaAmount = .IvaAmount
            Me.Amount = .Amount
            Me.DisbursementId = Me.CurrentDisbursement.Id
            Me.RejectReason = .RejectReason
            Me.AuthorizationNumber = .AuthorizationNumber
            Me.Source = .Source
            Me.DispositionAmount = .DispositionAmount
            Me.DiagnosticsAmount = .DiagnosticsAmount
            Me.WithholdingAmount = .WithholdingAmount
            Me.TotalTaxAmount = .TotalTaxAmount

        End With

    End Sub

    Public Sub ReverseClaimInvoice()
        Me.RepairEstimate = New DecimalType((Me.RepairEstimate).Value * REVERSE_MULTIPLIER)
        Me.LaborAmt = New DecimalType((Me.LaborAmt).Value * REVERSE_MULTIPLIER)
        Me.LaborTax = New DecimalType((Me.LaborTax).Value * REVERSE_MULTIPLIER)
        Me.PartAmount = New DecimalType((Me.PartAmount).Value * REVERSE_MULTIPLIER)
        Me.PartTax = New DecimalType((Me.PartTax).Value * REVERSE_MULTIPLIER)
        Me.ServiceCharge = New DecimalType((Me.ServiceCharge).Value * REVERSE_MULTIPLIER)
        Me.TripAmount = New DecimalType((Me.TripAmount).Value * REVERSE_MULTIPLIER)
        Me.OtherAmount = New DecimalType((Me.OtherAmount).Value * REVERSE_MULTIPLIER)
        Me.IvaAmount = New DecimalType((Me.IvaAmount).Value * REVERSE_MULTIPLIER)
        If Not Me.DispositionAmount Is Nothing AndAlso Me.DispositionAmount.Value > 0 Then Me.DispositionAmount = New DecimalType((Me.DispositionAmount).Value * REVERSE_MULTIPLIER)
        If Not Me.DiagnosticsAmount Is Nothing AndAlso Me.DiagnosticsAmount.Value > 0 Then Me.DiagnosticsAmount = New DecimalType((Me.DiagnosticsAmount).Value * REVERSE_MULTIPLIER)
        Me.Amount = New DecimalType((Me.Amount).Value * REVERSE_MULTIPLIER)

    End Sub

    Public Sub AdjustClaimInvoiceAmounts(ByVal adjustmentPercentage As Decimal)
        Try

            Me.RepairEstimate = New DecimalType((Me.RepairEstimate).Value * adjustmentPercentage)
            Me.LaborAmt = New DecimalType((Me.LaborAmt).Value * adjustmentPercentage)
            Me.LaborTax = New DecimalType((Me.LaborTax).Value * adjustmentPercentage)
            Me.PartAmount = New DecimalType((Me.PartAmount).Value * adjustmentPercentage)
            Me.PartTax = New DecimalType((Me.PartTax).Value * adjustmentPercentage)
            Me.ServiceCharge = New DecimalType((Me.ServiceCharge).Value * adjustmentPercentage)
            Me.TripAmount = New DecimalType((Me.TripAmount).Value * adjustmentPercentage)
            Me.ShippingAmount = New DecimalType((Me.ShippingAmount).Value * adjustmentPercentage)
            Me.OtherAmount = New DecimalType((Me.OtherAmount).Value * adjustmentPercentage)
            If Not Me.DispositionAmount Is Nothing AndAlso Me.DispositionAmount.Value > 0 Then Me.DispositionAmount = New DecimalType((Me.DispositionAmount).Value * adjustmentPercentage)
            If Not Me.DiagnosticsAmount Is Nothing AndAlso Me.DiagnosticsAmount.Value > 0 Then Me.DiagnosticsAmount = New DecimalType((Me.DiagnosticsAmount).Value * adjustmentPercentage)
            Me.IvaAmount = New DecimalType((Me.IvaAmount).Value * adjustmentPercentage)
            Me.Amount = New DecimalType((Me.Amount).Value * adjustmentPercentage)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty
        End Get
    End Property

    Public Sub CapturePayeeTaxDocumentation(ByVal PayeeOptionCode As String, ByVal objServiceCenter As ServiceCenter,
                                            ByVal objClaimInvoice As ClaimInvoice, ByVal objDisbursement As Disbursement,
                                            ByVal objCertificate As Certificate, ByVal txtDocumentType As String, ByVal txtIdentificationNumber As String)
        Select Case PayeeOptionCode
            Case ClaimInvoice.PAYEE_OPTION_MASTER_CENTER, ClaimInvoice.PAYEE_OPTION_SERVICE_CENTER, ClaimInvoice.PAYEE_OPTION_LOANER_CENTER
                If objClaimInvoice.IsInsuranceCompany Then
                    objDisbursement.DocumentType = Codes.DOCUMENT_TYPE__CNPJ
                    objClaimInvoice.DocumentType = Codes.DOCUMENT_TYPE__CNPJ
                    objDisbursement.IdentificationNumber = objServiceCenter.TaxId
                    objClaimInvoice.TaxId = objServiceCenter.TaxId
                End If
            Case ClaimInvoice.PAYEE_OPTION_CUSTOMER
                If objClaimInvoice.IsInsuranceCompany Then
                    objDisbursement.DocumentType = LookupListNew.GetCodeFromId(LookupListNew.LK_DOCUMENT_TYPES, objCertificate.DocumentTypeID)
                    objDisbursement.IdentificationNumber = objCertificate.IdentificationNumber
                    objClaimInvoice.DocumentType = objDisbursement.DocumentType
                    objClaimInvoice.TaxId = objDisbursement.IdentificationNumber
                End If
            Case ClaimInvoice.PAYEE_OPTION_OTHER
                If objClaimInvoice.IsInsuranceCompany Then
                    objDisbursement.DocumentType = txtDocumentType
                    objDisbursement.IdentificationNumber = txtIdentificationNumber
                    objClaimInvoice.DocumentType = txtDocumentType
                    objClaimInvoice.TaxId = txtIdentificationNumber
                End If

        End Select

    End Sub

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ClaimInvoiceDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_CLAIM_INVOICE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property CancelPolicy() As Boolean
        Get
            Return _cancelPolicy
        End Get
        Set(ByVal Value As Boolean)
            _cancelPolicy = Value
        End Set
    End Property

    <ValueMandatory("")>
    Public Property CloseClaim() As Boolean
        Get
            Return _closeClaim
        End Get
        Set(ByVal Value As Boolean)
            _closeClaim = Value
        End Set
    End Property
    <ValueMandatory("")>
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property

    Public Property RepairCodeId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_REPAIR_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_REPAIR_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_REPAIR_CODE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property SvcControlNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_SVC_CONTROL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_SVC_CONTROL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_SVC_CONTROL_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property RecordCount() As LongType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_RECORD_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimInvoiceDAL.COL_NAME_RECORD_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_RECORD_COUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property BatchNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_BATCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_BATCH_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_BATCH_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidCauseOfLoss("")>
    Public Property CauseOfLossID() As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_CAUSE_OF_LOSS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_CAUSE_OF_LOSS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_CAUSE_OF_LOSS_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidRepairDate("")>
    Public Property RepairDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimInvoiceDAL.COL_NAME_REPAIR_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_REPAIR_DATE, Value)
        End Set
    End Property

    <ValueMandatoryConditionallyForInvDate(""), ValidInvoiceDate("")>
    Public Property InvoiceDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimInvoiceDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property

    'Invoice property is Not a Derived Property of Claim Authorization.
    ' It is only being set and used for the Payment of Claim Authorizations
    Dim _Invoice As Invoice
    Public Property Invoice() As Invoice
        Get
            Return _Invoice
        End Get
        Set(ByVal value As Invoice)
            _Invoice = value
        End Set
    End Property

    <ValidLoanerReturnedDate("")>
    Public Property LoanerReturnedDate() As DateType
        Get
            If Not Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                Return Invoiceable.LoanerReturnedDate
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal Value As DateType)
            If Not Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                Invoiceable.LoanerReturnedDate = Value
            Else
                'ignore it
            End If
        End Set
    End Property


    Public Property RegionId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property

    Public Property RepairEstimate() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_REPAIR_ESTIMATE) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_REPAIR_ESTIMATE, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_REPAIR_ESTIMATE), Decimal))
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_REPAIR_ESTIMATE, Value)
        End Set
    End Property



    Public Property LaborAmt() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_LABOR_AMT) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_LABOR_AMT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_LABOR_AMT), Decimal))
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_LABOR_AMT, Value)
        End Set
    End Property

    'Public Property SalvageAmt() As DecimalType
    '    Get
    '        Return _salvageamt 'New DecimalType(getDecimalValue(CurrentClaim.SalvageAmount))
    '    End Get
    '    Set(ByVal Value As DecimalType)
    '        _salvageamt = Value
    '    End Set
    'End Property

    'Public Property ReconciledAmt() As DecimalType
    '    Get
    '        Return _reconciledAmount
    '    End Get
    '    Set(ByVal Value As DecimalType)
    '        _reconciledAmount = Value
    '    End Set
    'End Property

    Public Property LaborTax() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_LABOR_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_LABOR_TAX), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_LABOR_TAX, Value)
        End Set
    End Property



    Public Property PartAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_PART_AMOUNT) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_PART_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_PART_AMOUNT), Decimal))

        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_PART_AMOUNT, Value)
        End Set
    End Property



    Public Property PartTax() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_PART_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_PART_TAX), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_PART_TAX, Value)
        End Set
    End Property



    Public Property ServiceCharge() As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_SERVICE_CHARGE) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_SERVICE_CHARGE, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_SERVICE_CHARGE), Decimal))

        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_SERVICE_CHARGE, Value)
        End Set
    End Property

    Public Property TripAmount() As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_TRIP_AMOUNT) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_TRIP_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_TRIP_AMOUNT), Decimal))

        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_TRIP_AMOUNT, Value)
        End Set
    End Property

    Public Property ShippingAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_SHIPPING_AMOUNT) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_SHIPPING_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_SHIPPING_AMOUNT), Decimal))

        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_SHIPPING_AMOUNT, Value)
        End Set
    End Property


    Public Property PaytocustomerAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_PAY_TO_CUST_AMOUNT) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_PAY_TO_CUST_AMOUNT, New DecimalType(0D))
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_PAY_TO_CUST_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_PAY_TO_CUST_AMOUNT, Value)
        End Set
    End Property

    Public Property OtherAmount() As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_OTHER_AMOUNT) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_OTHER_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_OTHER_AMOUNT), Decimal))

        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_OTHER_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=255)>
    Public Property OtherExplanation() As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_OTHER_EXPLANATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_OTHER_EXPLANATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_OTHER_EXPLANATION, Value)
        End Set
    End Property

    Public Property DeductibleAmount() As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_AMOUNT) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_AMOUNT), Decimal))

        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_AMOUNT, Value)
        End Set
    End Property

    Public Property DeductibleTaxAmount() As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT), Decimal))

        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT, Value)
        End Set
    End Property

    Public Property IvaAmount() As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_IVA_AMOUNT) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_IVA_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_IVA_AMOUNT), Decimal))

        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_IVA_AMOUNT, Value)
        End Set
    End Property

    Public Property TotalTaxAmount() As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_TOTAL_TAX_AMOUNT) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_TOTAL_TAX_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_TOTAL_TAX_AMOUNT), Decimal))

        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_TOTAL_TAX_AMOUNT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidAmount(""), ValidateUserLimit("")>
    Public Property Amount() As DecimalType
        Get
            CheckDeleted()


            If Row(ClaimInvoiceDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Me.SetValue(ClaimInvoiceDAL.COL_NAME_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_AMOUNT), Decimal))

        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property

    Public Property AdjustmentAmount() As DecimalType
        Get
            Return _adjustmentAmount
        End Get
        Set(ByVal Value As DecimalType)
            _adjustmentAmount = Value
        End Set
    End Property
    Public Property TotalAmount() As DecimalType
        Get
            Return _totalAmount
        End Get
        Set(ByVal Value As DecimalType)
            _totalAmount = Value
        End Set
    End Property

    Public Property DisbursementId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_DISBURSEMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_DISBURSEMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_DISBURSEMENT_ID, Value)
        End Set
    End Property

    Public Property CurrentCommentId() As Guid
        Get
            Return _currentCommentId
        End Get
        Set(ByVal Value As Guid)
            _currentCommentId = Value
        End Set
    End Property
    Public ReadOnly Property CertItemId() As Guid
        Get
            CheckDeleted()
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                If Not Invoiceable.CertItemCoverageId.Equals(Guid.Empty) Then
                    Dim certItemCoverageBO As CertItemCoverage = New CertItemCoverage(Invoiceable.CertItemCoverageId)
                    If Not certItemCoverageBO.CertItemId.Equals(Guid.Empty) Then
                        Dim certItemBO As CertItem = New CertItem(certItemCoverageBO.CertItemId)
                        Return certItemBO.Id
                    Else
                        Return Nothing
                    End If
                End If
                Return Nothing
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property RejectReason() As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property AuthorizationNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)>
    Public Property Source() As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property

    Private _excludeDed As DecimalType
    Public Property excludeDeductible() As Boolean
        Get
            Return _excludeDed
        End Get
        Set(ByVal Value As Boolean)
            _excludeDed = Value
        End Set
    End Property

    Public Property ReconciledAmount() As DecimalType
        Get
            Return _reconciledAmount
        End Get
        Set(ByVal Value As DecimalType)
            _reconciledAmount = Value
        End Set
    End Property

    Private _PerceptionIVA As DecimalType

    Public Property PerceptionIVA() As DecimalType
        Get
            Return _PerceptionIVA
        End Get
        Set(ByVal Value As DecimalType)
            _PerceptionIVA = Value
        End Set
    End Property

    Private _PerceptionIIBB As DecimalType
    Public Property PerceptionIIBB() As DecimalType
        Get
            Return _PerceptionIIBB
        End Get
        Set(ByVal Value As DecimalType)
            _PerceptionIIBB = Value
        End Set
    End Property

    Public Property IsPaymentAdjustment() As Boolean
        Get
            Return _isPaymentAdjustment
        End Get
        Set(ByVal Value As Boolean)
            _isPaymentAdjustment = Value
        End Set
    End Property

    Public Property IsPaymentReversal() As Boolean
        Get
            Return _isPaymentReversal
        End Get
        Set(ByVal Value As Boolean)
            _isPaymentReversal = Value
        End Set
    End Property

    Public Property IsSalvagePayment() As Boolean
        Get
            Return _isSalvagePayment
        End Get
        Set(ByVal Value As Boolean)
            _isSalvagePayment = Value
        End Set
    End Property

    Public ReadOnly Property AdjustmentPercentage() As Decimal
        Get
            If Me.Amount.Value <> 0 Then
                Return (Me.Amount.Value - Me.AdjustmentAmount.Value) / Me.Amount.Value
            Else
                Return 0
            End If
        End Get

    End Property

    Public Property IsNewPaymentFromPaymentAdjustment() As Boolean
        Get
            Return _isNewPaymentFromPaymentAdjustment
        End Get
        Set(ByVal Value As Boolean)
            _isNewPaymentFromPaymentAdjustment = Value
        End Set
    End Property

    Public Property ClaimAuthorizationId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_CLAIM_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Value)
        End Set
    End Property

    Public Property Bonus() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_BONUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_BONUS), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_BONUS, Value)
        End Set
    End Property

    Public Property BonusTax() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_BONUS_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_BONUS_TAX), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_BONUS_TAX, Value)
        End Set
    End Property


    Public Property DispositionAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_DISPOSITION_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_DISPOSITION_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_DISPOSITION_AMOUNT, Value)
        End Set
    End Property


    Public Property DiagnosticsAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_DIAGNOSTICS_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_DIAGNOSTICS_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_DIAGNOSTICS_AMOUNT, Value)
        End Set
    End Property


    Public Property WithholdingAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_WITHHOLDING_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_WITHHOLDING_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ClaimInvoiceDAL.COL_NAME_WITHHOLDING_AMOUNT, Value)
        End Set
    End Property


    Public Property ServiceCenterWithholdingRate() As DecimalType
        Get
            If Not _service_center_withhodling_rate Is Nothing Then
                If _service_center_withhodling_rate.Value > 0 Then
                    Return _service_center_withhodling_rate * (-1D)
                Else
                    Return _service_center_withhodling_rate
                End If
            Else
                Return New DecimalType(0D)
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            _service_center_withhodling_rate = Value
        End Set
    End Property
#End Region

#Region "Address Children"

    Public ReadOnly Property AddressChild() As Address
        Get
            Dim newAddress = New Address
            Dim oCompany As New Company(Me.CompanyId)
            newAddress.CountryId = oCompany.BusinessCountryId
            Me.PayeeAddress = newAddress
            Return newAddress
        End Get
    End Property

    Public Function Add_Address(Optional ByVal addressId As Object = Nothing) As Address
        If Not addressId Is Nothing Then
            Return New Address(CType(addressId, Guid), Me.Dataset, Nothing, True)
        Else
            Return New Address(Me.Dataset, Nothing)
        End If
    End Function

    Public Function Add_BankInfo(Optional ByVal objBankInfoId As Object = Nothing) As BankInfo
        Dim BankInfoId As Guid = Guid.Empty
        If Not objBankInfoId Is Nothing Then BankInfoId = CType(objBankInfoId, Guid)

        If Not BankInfoId.Equals(Guid.Empty) Then
            Return New BankInfo(BankInfoId, Me.Dataset)
        Else
            Dim objBankInfo As New BankInfo(Me.Dataset)
            'default new Bank Info country to the Customer's country
            Dim objCertItem As New CertItem(Me.CertItemId)
            Dim objCert As New Certificate(objCertItem.CertId)
            Dim objAddress As New Address(objCert.AddressId)
            objBankInfo.CountryID = objAddress.CountryId
            Return objBankInfo
        End If
    End Function
#End Region

#Region "Derived Properties"
    Public ReadOnly Property CurrentCertItem() As CertItem
        Get
            If _cert_Item Is Nothing Then
                _cert_Item = New CertItem(Me.CertItemId, Me.Dataset)
            End If
            Return _cert_Item
        End Get
    End Property
    'Public ReadOnly Property CurrentClaim(Optional ByVal blnMustReload As Boolean = False) As Claim
    '    Get
    '        If _claim Is Nothing Then
    '            _claim = ClaimFacade.Instance.GetClaim(Of Claim)(Me.ClaimId, Me.Dataset, blnMustReload)
    '        End If
    '        Return _claim
    '    End Get
    'End Property

    Public ReadOnly Property ClaimTax() As ClaimTax
        Get
            If _ClaimTax Is Nothing Then
                _ClaimTax = New ClaimTax(Me.Dataset)
            End If
            Return _ClaimTax
        End Get

    End Property

    Public ReadOnly Property ClaimTaxManual() As ClaimTax
        Get
            If _ClaimTaxManual Is Nothing Then
                _ClaimTaxManual = New ClaimTax(Me.Dataset)
            End If
            Return _ClaimTaxManual
        End Get
    End Property

    Public ReadOnly Property HasClaimTaxManual As Boolean
        Get
            If _ClaimTaxManual Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property
    Public ReadOnly Property RemainingAmount() As DecimalType
        Get
            Dim objclaimAuth As ClaimAuthorization
            'For MultiAuth Claims, if the Authorization contains deductible then Invoiceable.Deductible.Value would contain the deductible value at claim level 
            Dim deductiblePaidByAssurant As Decimal = 0D
            If (Invoiceable.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.AUTH_LESS_DEDUCT_Y)) Or
                   (Invoiceable.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y)) Then
                If Me.ClaimAuthorizationId.Equals(Guid.Empty) Then
                    If Invoiceable.ConsumerPays.Value > Invoiceable.Deductible.Value Then
                        'Check Contains Deductible
                        deductiblePaidByAssurant = Invoiceable.Deductible.Value

                    Else
                        deductiblePaidByAssurant = Invoiceable.ConsumerPays.Value
                    End If
                End If
            End If

            If Me.IsNew Then
                Dim remAmt As Decimal
                If Not Me.ClaimAuthorizationId = Guid.Empty Then
                    'Reconciled Amount already contains PayDeductible Amount if its applicable
                    remAmt = getDecimalValue(Invoiceable.AssurantPays) + deductiblePaidByAssurant - getDecimalValue(AlreadyPaid) _
                             - getDecimalValue(Amount)
                Else
                    'Payment Amount already does Not PayDeductible Amount even if its applicable
                    remAmt = getDecimalValue(Invoiceable.AssurantPays) + deductiblePaidByAssurant - getDecimalValue(AlreadyPaid) _
                             - getDecimalValue(Amount) - getDecimalValue(DeductibleAmount) ' - getDecimalValue(AlreadyPaidDeductible) '+ getDecimalValue(CurrentClaim.SalvageAmount) - getDecimalValue(SalvageAmt) '- getDecimalValue(PerceptionIVA) - getDecimalValue(PerceptionIIBB))
                End If
                Return New DecimalType(remAmt)
            Else
                'This case is not supported in case of Multi Auth Claims
                Dim remAmt As Decimal
                remAmt = getDecimalValue(Invoiceable.AssurantPays) + deductiblePaidByAssurant - getDecimalValue(AlreadyPaid) - getDecimalValue(DeductibleAmount) - getDecimalValue(AlreadyPaidDeductible) ' + getDecimalValue(CurrentClaim.SalvageAmount) - getDecimalValue(SalvageAmt)
                Return New DecimalType(remAmt)
            End If
        End Get
    End Property
    Public ReadOnly Property AlreadyPaid() As DecimalType
        Get
            If Not Me.ClaimAuthorizationId = Guid.Empty Then
                CalculateAmountsForMultiAuthClaim()
            Else
                CalculateAmounts()
            End If

            Return New DecimalType(getDecimalValue(GetClaimSumofInvoices()))
        End Get
    End Property
    Public ReadOnly Property AlreadyPaidDeductible() As DecimalType
        Get
            Return New DecimalType(getDecimalValue(GetClaimSumOfDeductibles()))
        End Get
    End Property
    Private ReadOnly Property CurrentDisbursement() As Disbursement
        Get
            If _disbursement Is Nothing Then
                If Me.DisbursementId.Equals(Guid.Empty) Then
                    _disbursement = New Disbursement(Me)
                Else
                    ' this will be used for view only. so we dont need to 
                    ' attach the disbursement dataset
                    _disbursement = New Disbursement(Me.DisbursementId)
                End If
            End If
            Return _disbursement
        End Get
    End Property

    Private ReadOnly Property CurrentComment() As Comment
        Get
            If _comment Is Nothing Then
                _comment = New Comment(Me.Dataset)
            End If
            Return _comment
        End Get
    End Property
    Public ReadOnly Property CertificateNumber() As String
        Get
            Dim certNumber As String = Nothing
            If Not Invoiceable.CertificateId.Equals(Guid.Empty) Then
                Dim certBO As Certificate = New Certificate(Invoiceable.CertificateId)
                certNumber = certBO.CertNumber
            End If
            Return certNumber
        End Get
    End Property

    Public ReadOnly Property CustomerName() As String
        Get
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                Return Invoiceable.CustomerName
            End If
        End Get
    End Property

    Public ReadOnly Property ServiceCenterName() As String
        Get
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                Return Invoiceable.ServiceCenterObject.Description
            End If
        End Get
    End Property

    Public ReadOnly Property RiskType() As String
        Get
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                Return Invoiceable.RiskType
            End If
        End Get
    End Property

    Public ReadOnly Property Manufacturer() As String
        Get
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                If Not Invoiceable.CertItemCoverageId.Equals(Guid.Empty) Then
                    Dim certItemCoverageBO As CertItemCoverage = New CertItemCoverage(Invoiceable.CertItemCoverageId)
                    If Not certItemCoverageBO.CertItemId.Equals(Guid.Empty) Then
                        Dim certItemBO As CertItem = New CertItem(certItemCoverageBO.CertItemId)
                        If Not certItemBO.ManufacturerId.Equals(Guid.Empty) Then
                            Dim manufacturerBO As New Manufacturer(certItemBO.ManufacturerId)
                            Return manufacturerBO.Description
                        Else
                            Return Nothing
                        End If
                    Else
                        Return Nothing
                    End If
                End If
                Return Nothing
            End If
        End Get
    End Property
    Public ReadOnly Property Model() As String
        Get
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                If Not Invoiceable.CertItemCoverageId.Equals(Guid.Empty) Then
                    Dim certItemCoverageBO As CertItemCoverage = New CertItemCoverage(Invoiceable.CertItemCoverageId)
                    If Not certItemCoverageBO.CertItemId.Equals(Guid.Empty) Then
                        Dim certItemBO As CertItem = New CertItem(certItemCoverageBO.CertItemId)
                        Return certItemBO.Model
                    Else
                        Return Nothing
                    End If
                End If
                Return Nothing
            End If
        End Get
    End Property
    Public ReadOnly Property SerialNumber() As String
        Get
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                If Not Invoiceable.CertItemCoverageId.Equals(Guid.Empty) Then
                    Dim certItemCoverageBO As CertItemCoverage = New CertItemCoverage(Invoiceable.CertItemCoverageId)
                    If Not certItemCoverageBO.CertItemId.Equals(Guid.Empty) Then
                        Dim certItemBO As CertItem = New CertItem(certItemCoverageBO.CertItemId)
                        Return certItemBO.SerialNumber
                    Else
                        Return Nothing
                    End If
                End If
                Return Nothing
            End If
        End Get
    End Property
    Public ReadOnly Property CustomerAddressID() As Guid
        Get
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                Dim certBO As Certificate = New Certificate(Invoiceable.CertificateId)
                If certBO Is Nothing Then
                    Return Nothing
                Else
                    Return certBO.AddressId
                End If
            End If
        End Get
    End Property
    Public ReadOnly Property ServiceCenterAddressID() As Guid
        Get
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                Dim svcCenter As ServiceCenter = New ServiceCenter(Invoiceable.ServiceCenterId)
                If svcCenter Is Nothing Then
                    Return Nothing
                Else
                    Return svcCenter.AddressId
                End If
            End If
        End Get
    End Property

    Public ReadOnly Property IsIvaResponsibleFlag As Boolean
        Get
            Dim PayeeOptionCode As String

            If Not Me.PayeeOptionCode Is Nothing AndAlso Me.PayeeOptionCode <> "" Then
                PayeeOptionCode = Me.PayeeOptionCode
            Else
                PayeeOptionCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, Me.CurrentDisbursement.PayeeOptionId)
            End If

            If PayeeOptionCode = PAYEE_OPTION_SERVICE_CENTER Or PayeeOptionCode = PAYEE_OPTION_MASTER_CENTER Or PayeeOptionCode = PAYEE_OPTION_LOANER_CENTER Then
                Select Case PayeeOptionCode
                    Case PAYEE_OPTION_MASTER_CENTER
                        Dim claimServiceCenter As ServiceCenter = New ServiceCenter(Me.Invoiceable.ServiceCenterId)
                        If Not claimServiceCenter.MasterCenterId.Equals(Guid.Empty) Then
                            Dim masterServiceCenter As ServiceCenter = New ServiceCenter(claimServiceCenter.MasterCenterId)
                            If Not masterServiceCenter.IvaResponsibleFlag Then
                                _service_center_withhodling_rate = masterServiceCenter.WithholdingRate
                                Return False
                            End If
                        End If
                    Case PAYEE_OPTION_SERVICE_CENTER
                        Dim claimServiceCenter As ServiceCenter = New ServiceCenter(Me.Invoiceable.ServiceCenterId)
                        If Not claimServiceCenter.IvaResponsibleFlag Then
                            _service_center_withhodling_rate = claimServiceCenter.WithholdingRate
                            Return False
                        End If
                    Case PAYEE_OPTION_LOANER_CENTER
                        If Not Me.Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                            Dim loanerServiceCenter As ServiceCenter = New ServiceCenter(Me.Invoiceable.LoanerCenterId)
                            If Not loanerServiceCenter.IvaResponsibleFlag Then
                                _service_center_withhodling_rate = loanerServiceCenter.WithholdingRate
                                Return False
                            End If
                        End If
                    Case Else
                        _service_center_withhodling_rate = 0
                        Return False
                End Select
                Return True
            ElseIf PayeeOptionCode Is Nothing Then
                Dim claimServiceCenter As ServiceCenter = New ServiceCenter(Me.Invoiceable.ServiceCenterId)
                _service_center_withhodling_rate = claimServiceCenter.WithholdingRate
                Return False
            Else
                _service_center_withhodling_rate = 0
                Return False
            End If
        End Get
    End Property


    Private Function GetTaxRate(ByVal taxOnDeductible As Boolean) As Decimal
        ' tax rate is only calculated for the service centers , not for customer or "other".
        ' besides that , the tax flag (iva_responsible) must be on for the service center/master or loaner center

        Dim retval As DecimalType = New DecimalType(0D)
        'REQ 1150
        Dim oCert As New Certificate(Me.CurrentCertItem.CertId)

        If (taxOnDeductible) Then
            Dim oDealer As New Dealer(oCert.DealerId)
            If (oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.YESNO_N)) Or
                (oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y)) Then
                Return retval
            End If
        End If

        If Me.IsIvaResponsibleFlag Then

            'to calculate the tax rate, we need the region id because sometaxes are based on the region.

            Dim payeeAddress As Address = New Address(Me.ServiceCenterAddressID) 'Added by AA for WR761620
            If Not payeeAddress.RegionId.Equals(Guid.Empty) Then
                If taxOnDeductible Then
                    If _DeductibleTaxRateChecked AndAlso _DeductibleTaxRate > -1 Then 'return saved result
                        Return New DecimalType(_DeductibleTaxRate)
                    End If
                Else
                    If _TaxRateChecked AndAlso _TaxRate > -1 Then 'return saved result
                        Return New DecimalType(_TaxRate)
                    End If
                End If
                Dim taxRateData As ClaimInvoiceDAL.TaxRateData = New ClaimInvoiceDAL.TaxRateData
                Dim dal As ClaimInvoiceDAL = New ClaimInvoiceDAL
                With taxRateData
                    Dim companyBO As Company = New Company(Me.CompanyId)
                    .countryID = companyBO.BusinessCountryId
                    .regionID = payeeAddress.RegionId
                    'REQ 1150
                    .dealerID = oCert.DealerId
                    If Me.Invoiceable.InvoiceProcessDate Is Nothing Then
                        'If Me.CurrentClaim.RepairDate Is Nothing Then
                        .salesDate = System.DateTime.Now
                    Else
                        .salesDate = Me.Invoiceable.InvoiceProcessDate.Value
                    End If
                    If (taxOnDeductible) Then
                        ' taxtype code - 9 = Deductible...
                        .taxtypeID = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, "9")
                    Else
                        ' taxtype code - 7 = Repairs...
                        If Invoiceable.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                            'Replacement claim, find the applicable IVA tax
                            Dim ReplacementTaxId As Guid
                            Dim certBO As Certificate = New Certificate(Invoiceable.CertificateId)
                            Dim ProductPrice As Decimal = certBO.SalesPrice.Value
                            dal.GetReplacementTaxType(Invoiceable.ServiceCenterId, Invoiceable.RiskTypeId, .salesDate, ProductPrice, ReplacementTaxId)
                            If ReplacementTaxId = Guid.Empty Then
                                .taxtypeID = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, "7")
                            Else
                                .taxtypeID = ReplacementTaxId
                            End If
                        Else
                            .taxtypeID = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, "7")
                        End If
                    End If
                End With

                Dim retTaxRateData As ClaimInvoiceDAL.TaxRateData = dal.GetTaxRate(taxRateData)
                If (taxOnDeductible) Then
                    _DeductibleTaxRateChecked = True
                    _DeductibleTaxRate = retTaxRateData.taxRate
                    Return New DecimalType(_DeductibleTaxRate)
                Else
                    _TaxRateChecked = True
                    _TaxRate = retTaxRateData.taxRate
                    Return New DecimalType(_TaxRate)
                End If
            End If
        End If

        Return retval
    End Function

    Private Function GetClaimTaxRates(payeeRegionId As Guid, ClaimMethodOfRepair As String) As ClaimInvoiceDAL.ClaimTaxRatesData
        ' claim tax rates will be calculated for the service centers , customer or "other".
        ' besides that , the tax flag (iva_responsible) will not be inforced; as this is not IVA tax computation. 

        Dim ClaimTaxRatesData As ClaimInvoiceDAL.ClaimTaxRatesData = New ClaimInvoiceDAL.ClaimTaxRatesData
        Dim oCert As New Certificate(Me.CurrentCertItem.CertId)

        'to calculate the tax rate, we need the region id because some taxes are based on the region.        
        Dim dal As ClaimInvoiceDAL = New ClaimInvoiceDAL
        With ClaimTaxRatesData
            Dim companyBO As Company = New Company(Me.CompanyId)
            .countryID = companyBO.BusinessCountryId
            .regionID = payeeRegionId
            .dealerID = oCert.DealerId
            .claim_type = ClaimMethodOfRepair.ToUpper
            If Me.Invoiceable.InvoiceProcessDate Is Nothing Then
                'If Me.CurrentClaim.RepairDate Is Nothing Then
                .salesDate = System.DateTime.Now
            Else
                .salesDate = Me.Invoiceable.InvoiceProcessDate.Value
            End If
        End With

        ClaimTaxRatesData = dal.GetClaimTaxRates(ClaimTaxRatesData)

        Return ClaimTaxRatesData
    End Function

    Public Shared Function GetClaimTaxRatesData(ByVal CountryId As Guid, ByVal payeeRegionId As Guid, ByVal dealer_id As Guid, ByVal InvoiceProcessDate As Date, ByVal ClaimMethodOfRepair As String) As ClaimInvoiceDAL.ClaimTaxRatesData
        ' claim tax rates will be calculated for the service centers , customer or "other".
        ' besides that , the tax flag (iva_responsible) will not be inforced; as this is not IVA tax computation. 

        Dim ClaimTaxRatesData As ClaimInvoiceDAL.ClaimTaxRatesData = New ClaimInvoiceDAL.ClaimTaxRatesData
        Dim dal As ClaimInvoiceDAL = New ClaimInvoiceDAL
        With ClaimTaxRatesData
            .countryID = CountryId
            .regionID = payeeRegionId
            .dealerID = dealer_id
            .claim_type = ClaimMethodOfRepair.ToUpper
            .salesDate = InvoiceProcessDate
        End With

        ClaimTaxRatesData = dal.GetClaimTaxRates(ClaimTaxRatesData)

        Return ClaimTaxRatesData
    End Function

    Public ReadOnly Property ClaimTaxRatesData(payeeRegionId As Guid, ClaimMethodOfRepair As String) As ClaimInvoiceDAL.ClaimTaxRatesData
        Get
            If Me._claimTaxRatesData Is Nothing Then
                Me._claimTaxRatesData = GetClaimTaxRates(payeeRegionId, ClaimMethodOfRepair)
            End If
            Return Me._claimTaxRatesData
        End Get
    End Property

    Public ReadOnly Property DeductibleTaxRate() As DecimalType
        Get
            Return GetTaxRate(True)
        End Get
    End Property

    Public ReadOnly Property TaxRate() As DecimalType
        Get
            Return GetTaxRate(False)
        End Get
    End Property

    Public ReadOnly Property isTaxTypeInvoice() As Boolean
        Get

            Dim payeeAddress As Address = New Address(Me.ServiceCenterAddressID)
            'REQ 1150
            Dim oCert As New Certificate(Me.CurrentCertItem.CertId)
            If Not payeeAddress.RegionId.Equals(Guid.Empty) Then
                Dim taxRateData As ClaimInvoiceDAL.TaxRateData = New ClaimInvoiceDAL.TaxRateData
                With taxRateData
                    Dim companyBO As Company = New Company(Me.CompanyId)
                    .countryID = companyBO.BusinessCountryId
                    .regionID = payeeAddress.RegionId
                    'REQ 1150
                    .dealerID = oCert.DealerId
                    If Me.Invoiceable.InvoiceProcessDate Is Nothing Then
                        .salesDate = System.DateTime.Now
                    Else
                        .salesDate = Me.Invoiceable.InvoiceProcessDate.Value
                    End If
                    ' taxtype code - 4 = Manual...
                    .taxtypeID = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, "4")
                End With

                Dim dal As ClaimInvoiceDAL = New ClaimInvoiceDAL
                Return dal.GetInvoiceTaxType(taxRateData)

            End If

            Return False

        End Get
    End Property


    Public ReadOnly Property SecondClaimInvoice() As ClaimInvoice
        Get
            If _claimInvoice Is Nothing Then
                _claimInvoice = New ClaimInvoice(Me.Dataset)
            End If
            Return _claimInvoice
        End Get
    End Property

#End Region

#Region "Temp Properties"
    'Created a temp property for Serial Number to be maintained between page navigation.
    Public Property SerialNumberTempContainer() As String
        Get
            Return _serialNumberTempContainer
        End Get
        Set(ByVal Value As String)
            _serialNumberTempContainer = Value
        End Set
    End Property

    <ValueMandatoryConditionally(""), ValidPickUpDate("")> _
    Public Property PickUpDate() As DateType
        Get
            Return _pickupdate
        End Get
        Set(ByVal Value As DateType)
            _pickupdate = Value
        End Set
    End Property

    Public Property PayeeAddress() As Address
        Get
            Return _payeeAddress
        End Get
        Set(ByVal Value As Address)
            _payeeAddress = Value
        End Set
    End Property

    Public Property PayeeBankInfo() As BankInfo
        Get
            Return _payeeBankInfo
        End Get
        Set(ByVal Value As BankInfo)
            _payeeBankInfo = Value
        End Set
    End Property

    Public ReadOnly Property IsInsuranceCompany() As Boolean
        Get
            Dim objCompany As New Company(Me.Invoiceable.CompanyId)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_INSURANCE Then
                Return True
            Else
                Return False
            End If

        End Get
    End Property

    Public Property PayeeOptionCode() As String
        Get
            Return _payeeOptionCode
        End Get
        Set(ByVal Value As String)
            _payeeOptionCode = Value
        End Set
    End Property

    Public Property PaymentMethodCode() As String
        Get
            Return _paymentMethodCode
        End Get
        Set(ByVal Value As String)
            _paymentMethodCode = Value
        End Set
    End Property

    <RequiredConditionally("")> _
    Public Property PaymentMethodID() As Guid
        Get
            Return _paymentMethodID
        End Get
        Set(ByVal Value As Guid)
            _paymentMethodID = Value
        End Set
    End Property

    <ValidStringLength("", Max:=5), ValueMandatoryConditionallyOnPayee("")> _
    Public Property DocumentType() As String
        Get
            Return _documentType
        End Get
        Set(ByVal Value As String)
            _documentType = Value
        End Set
    End Property

    <ValidStringLength("", Max:=15), ValidTaxNumber("")> _
    Public Property TaxId() As String
        Get
            Return _taxId
        End Get
        Set(ByVal Value As String)
            _taxId = Value
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Sub Save(Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal overrrideDsCreatorFlag As Boolean = False)
        Dim objDataBaseClaim As Claim
        Dim objclaimAuth As ClaimAuthorization
        Try
            If Me.ClaimAuthorizationId = Guid.Empty Then
                objDataBaseClaim = ClaimFacade.Instance.GetClaim(Of Claim)(Me.Invoiceable.Claim_Id)
                If Not objDataBaseClaim.ModifiedDate Is Nothing Then Me.Invoiceable.VerifyConcurrency(objDataBaseClaim.ModifiedDate.ToString)
                'If Not objDataBaseClaim.ModifiedDate Is Nothing Then objDataBaseClaim.VerifyConcurrency(objDataBaseClaim.ModifiedDate.ToString)
            Else
                objclaimAuth = DirectCast(New ClaimAuthorization(Me.ClaimAuthorizationId, Me.Dataset), ClaimAuthorization)
                If Not objclaimAuth.Claim.ModifiedDate Is Nothing Then Me.Invoiceable.VerifyConcurrency(objclaimAuth.Claim.ModifiedDate.ToString)
                'If Not objclaimAuth.Claim.ModifiedDate Is Nothing Then objclaimAuth.Claim.VerifyConcurrency(objclaimAuth.Claim.ModifiedDate.ToString)
            End If

            If Not (Me.IsPaymentAdjustment Or Me.IsPaymentReversal) Then
                If Not Me.ClaimAuthorizationId = Guid.Empty Then
                    CalculateAmountsForMultiAuthClaim()
                Else
                    CalculateAmounts()
                End If
            End If

            If CauseOfLossID = Guid.Empty Then ' if empty set the cause of loss to the default by coverage
                Dim guidID = New CertItemCoverage(Invoiceable.CertItemCoverageId).CoverageTypeId
                Dim dv As New DataView(CoverageLoss.LoadDefaultCauseOfLossByCov(guidID).Tables(0))
                If dv.Count > 0 Then
                    CauseOfLossID = New Guid(CType(dv(0)("id"), Byte()))
                End If
            End If

            MyBase.Save()
            If (Me._isDSCreator OrElse overrrideDsCreatorFlag) AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                'Process Claim and create Disbursement record for this invoice if new payment is made from Pay Invoice Form.
                If Not Me.IsPaymentAdjustment And Not Me.IsPaymentReversal Then
                    CreateDisbursement()
                    Me._ClaimTax = Nothing
                    If Me.isTaxTypeInvoice() Then
                        CreateClaimTax()
                    End If

                    'set the claiminvoiceid and disbursement id of the claim tax record
                    If HasClaimTaxManual Then
                        ClaimTaxManual.ClaimInvoiceId = Me.Id
                        ClaimTaxManual.DisbursementId = Me.DisbursementId
                        ClaimTaxManual.Save()
                    End If

                    ProcessClaim(Transaction)
                Else
                    'create
                    Invoiceable.InvoiceProcessDate = New DateType(Date.Now)
                    'For def-366
                    If Me.IsPaymentReversal Then Me.HandelExtendedStatusForGVS(False, False, True)
                End If

                Dim dal As New ClaimInvoiceDAL

                MyBase.UpdateFamily(Me.Dataset)
                dal.UpdateFamily(Me.Dataset, _cancelCertificateData, Me.IsPaymentAdjustment Or Me.IsPaymentReversal, Transaction)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached AndAlso Transaction Is Nothing Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function GetTaxRate(ByVal oTaxRateData As ClaimInvoiceDAL.TaxRateData) As ClaimInvoiceDAL.TaxRateData
        Try
            Dim dal As New ClaimInvoiceDAL

            dal.GetTaxRate(oTaxRateData)
            Return oTaxRateData

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(ByVal invoiceNumber As String, ByVal payee As String, ByVal ClaimNumber As String, _
                           ByVal createdDate As String, ByVal invoiceAmount As String, Optional ByVal sortBy As String = ClaimInvoiceSearchDV.COL_INVOICE_NUMBER) As ClaimInvoiceSearchDV

        Try
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(ClaimInvoice), Nothing, "Search", Nothing)}
            'Check if the user has entered any search criteria... if NOT, then display an error
            If (ClaimNumber.Equals(String.Empty) AndAlso payee.Equals(String.Empty) AndAlso _
                createdDate.Equals(String.Empty) AndAlso invoiceNumber.Equals(String.Empty) AndAlso _
                invoiceAmount.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If

            Dim dal As New ClaimInvoiceDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            If (Not (payee Is Nothing)) Then
                payee = payee.ToUpper
            End If

            Return New ClaimInvoiceSearchDV(dal.LoadList(compIds, _
                                                  invoiceNumber, payee, ClaimNumber, createdDate, invoiceAmount, sortBy).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function getPaymentsList(ByVal oCompanyId As Guid, ByVal claimNumber As String) As ClaimInvoicesDV

        Try
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(ClaimInvoice), Nothing, "Search", Nothing)}
            'Check if the user has entered any search criteria... if NOT, then display an error
            If (claimNumber.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If

            Dim dal As New ClaimInvoiceDAL

            Return New ClaimInvoicesDV(dal.LoadPaymentsList(oCompanyId, claimNumber).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Class ClaimInvoiceSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CLAIM_INVOICE_ID As String = "claim_invoice_id"
        Public Const COL_INVOICE_NUMBER As String = "INVMB"
        Public Const COL_CLAIM_NUMBER As String = "CLNM"
        Public Const COL_PAYEE As String = "PAYEE"
        Public Const COL_CREATED_DATE As String = "DTCR"
        Public Const COL_INVOICE_AMOUNT As String = "INAMT"
        Public Const COL_AUTHORIZATION_NUMBER As String = "authorization_number"

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ClaimInvoiceId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_CLAIM_INVOICE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceNumber(ByVal row As DataRow) As String
            Get
                Return row(COL_INVOICE_NUMBER).ToString
            End Get
        End Property

    End Class

    Public Class ClaimInvoicesDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CLAIM_INVOICE_ID As String = "claim_invoice_id"
        Public Const COL_INVOICE_NUMBER As String = "INVMB"
        Public Const COL_CLAIM_NUMBER As String = "CLNM"
        Public Const COL_PAYEE As String = "PAYEE"
        Public Const COL_CREATED_DATE As String = "DTCR"
        Public Const COL_INVOICE_AMOUNT As String = "INAMT"
        Public Const COL_PAID_BY As String = "PAID_BY"
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ClaimInvoiceId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_CLAIM_INVOICE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceNumber(ByVal row As DataRow) As String
            Get
                Return row(COL_INVOICE_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property PaidBy(ByVal row As DataRow) As String
            Get
                Return row(COL_PAID_BY).ToString
            End Get
        End Property

    End Class
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidAmount
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_REMAINING_AMOUNT_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            If (Not obj.IsPaymentAdjustment) And (Not obj.IsPaymentReversal) Then
                If obj.Invoiceable.AssurantPays.Value > 0 Then
                    If ((Not obj.RemainingAmount Is Nothing) AndAlso obj.RemainingAmount.Value < 0) Or ((Not obj.Amount Is Nothing) AndAlso obj.Amount.Value <= 0) Then
                        Return False
                    End If
                ElseIf obj.Invoiceable.AssurantPays.Value < 0 Then
                    If Not obj.ClaimAuthorizationId = Guid.Empty Then
                        obj.CalculateAmountsForMultiAuthClaim()
                    Else
                        obj.CalculateAmounts()
                    End If
                    If ((Not obj.RemainingAmount Is Nothing) AndAlso obj.RemainingAmount.Value > 0) Or ((Not obj.Amount Is Nothing) AndAlso obj.Amount.Value >= 0) Then
                        Return False
                    End If
                End If

            Else
                If ((Not obj.Amount Is Nothing) AndAlso obj.Amount.Value = 0) Then
                    Return False
                End If
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateUserLimit
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.PAYMENT_AMOUNT_HAS_EXCEEDED_YOUR_PAYMENT_LIMIT_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)

            If ((Not obj.Amount Is Nothing) AndAlso obj.Amount.Value = 0) Then
                Return True
            End If

            If obj.Amount.Value > ElitaPlusIdentity.Current.ActiveUser.PaymentLimit(obj.CompanyId).Value Then
                Return False
            End If


            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidRepairDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_REPAIR_DATE_ERR2)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            Dim claimBO As Claim = New Claim(obj.Invoiceable.Claim_Id)

            If obj.RepairDate Is Nothing Then Return False

            If claimBO.CreatedDate Is Nothing Then
                If obj.RepairDate.Value <= System.DateTime.Now Then
                    Return True
                Else
                    Return False
                End If
            End If
            If Not claimBO.Source Is Nothing Then
                If ((obj.RepairDate.Value >= claimBO.LossDate.Value) AndAlso
                    (obj.RepairDate.Value <= System.DateTime.Now)) Then
                    Return True
                End If
            Else

                If ((obj.RepairDate.Value.Date >= claimBO.CreatedDate.Value.Date) AndAlso
                (obj.RepairDate.Value.Date <= System.DateTime.Now.Date)) Then
                    Return True
                End If
            End If

            Return False

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidInvoiceDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_INVOICE_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)

            If Not obj.InvoiceDate Is Nothing Then

                If obj.InvoiceDate.Value <= System.DateTime.Today Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidLoanerReturnedDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_LOANER_RETURNED_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)

            'Dim claimBO As Claim = New Claim(obj.ClaimId)
            If obj.LoanerReturnedDate Is Nothing Then
                If obj.Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                    Return True
                Else
                    Return False
                End If
            End If

            If obj.CreatedDate Is Nothing Then
                If obj.LoanerReturnedDate.Value <= System.DateTime.Now Then
                    Return True
                Else
                    Return False
                End If
            End If


            If ((obj.LoanerReturnedDate.Value >= obj.CreatedDate.Value) AndAlso _
                (obj.LoanerReturnedDate.Value <= System.DateTime.Now)) Then
                Return True
            End If

            Return False

        End Function
    End Class

    '<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    '      Public NotInheritable Class ValidAuthorizedAmount
    '    Inherits ValidBaseAttribute

    '    Public Sub New(ByVal fieldDisplayName As String)
    '        MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
    '    End Sub

    '    Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
    '        Dim obj As Claim = CType(objectToValidate, Claim)

    '        If obj.AuthorizedAmount Is Nothing Then Return True

    '        If (obj.AuthorizationLimit.Value >= obj.AuthorizedAmount.Value) Then
    '            Return True
    '        End If

    '        Return False

    '    End Function
    'End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidPickUpDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            If obj.PickUpDate Is Nothing Then Return True
            Dim pickUpDate As Date = obj.GetShortDate(obj.PickUpDate.Value)
            Dim createdDate As Date = Today
            If Not obj.CreatedDate Is Nothing Then
                createdDate = obj.GetShortDate(obj.CreatedDate.Value)
            End If

            ' WR 756735: For a claim that is not added from a claim interface or a replacement:
            ' PickUp Date:
            ' Must be LT or EQ today. 
            ' Must be GT or EQ to Repair Date. 

            If pickUpDate > Today Then
                Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR1 '"Pick-Up Date Must Be Less Than Or Equal To Today."
                Return False
            End If

            If Not obj.RepairDate Is Nothing Then
                If pickUpDate < obj.GetShortDate(obj.RepairDate.Value) Then
                    Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR2  '"Pick-Up Date Must Be Greater Than Or Equal To Repair Date."
                    Return False
                End If
            Else
                Me.Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR5  '"Pick-Up Date Requires The Entry Of A Repair Date."
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryConditionally
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_PICKUP_DATE_IS_REQUIRED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            If obj.PickUpDate Is Nothing And obj.Invoiceable.CanDisplayVisitAndPickUpDates Then
                If obj.IsPaymentAdjustment Or obj.IsPaymentReversal Or obj.IsNewPaymentFromPaymentAdjustment Then
                    Return True
                Else
                    Return False
                End If
            ElseIf obj.PickUpDate Is Nothing And obj.Invoiceable.CanDisplayVisitAndPickUpDates And obj.Invoiceable.LoanerReturnedDate Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryConditionallyForInvDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            Dim oCompaniesDv As DataView
            oCompaniesDv = User.GetUserCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
            oCompaniesDv.RowFilter = "code in ('ABA','VBA','SBA')"
            If oCompaniesDv.Count > 0 Then
                If obj.InvoiceDate Is Nothing Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class RequiredConditionally
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            If (obj.PayeeOptionCode = obj.PAYEE_OPTION_CUSTOMER Or obj.PayeeOptionCode = obj.PAYEE_OPTION_OTHER) And _
               obj.PaymentMethodID.Equals(Guid.Empty) Then
                Return False
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryConditionallyOnPayee
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            Dim objCompany As New Company(obj.CompanyId)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_INSURANCE _
                AndAlso obj.PayeeOptionCode = obj.PAYEE_OPTION_OTHER AndAlso _
               (obj.DocumentType Is Nothing OrElse obj.DocumentType.Equals(String.Empty)) Then
                Return False
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidTaxNumber
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            Dim objCompany As New Company(obj.CompanyId)

            If LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY_TYPE, objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_INSURANCE _
                AndAlso obj.PayeeOptionCode = obj.PAYEE_OPTION_OTHER Then

                If (obj.DocumentType Is Nothing OrElse obj.DocumentType.Equals(String.Empty)) Then Return False

                Dim dal As New ClaimInvoiceDAL
                Dim oErrMess As String
                Try
                    oErrMess = dal.ExecuteSP(obj.DocumentType, obj.TaxId)
                    If Not oErrMess Is Nothing Then
                        MyBase.Message = UCase(oErrMess)
                        Return False
                    End If

                Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
                End Try
            End If

            Return True

        End Function
    End Class


    '<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidCauseOfLoss
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COVERAGE_TYPE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            'Dim oClaim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(obj.ClaimId)
            Dim ctCovLoss As CoverageLoss = New CoverageLoss(obj.CauseOfLossID, (New CertItemCoverage(obj.Invoiceable.CertItemCoverageId)).CoverageTypeId)
            If Not ctCovLoss.Row Is Nothing Then
                If ctCovLoss.Active = "N" Then
                    Return False
                End If
            End If

            Return True

        End Function

    End Class

#End Region

#Region "Children"
    Public Function AddNewDisbursement() As Disbursement
        _disbursement = New Disbursement(Me.Dataset)
        Return _disbursement
    End Function

    Public Function GetClaimTax() As ClaimTax
        _ClaimTax = New ClaimTax(Me.Id, Me.Dataset)
        Return _ClaimTax
    End Function

    'Note : AddClaim is not currently being utilized by MultiAuth Claims.
    Public Function AddClaim(ByVal claimID As Guid) As Claim
        Dim objClaim As Claim

        If Not claimID.Equals(Guid.Empty) Then
            objClaim = ClaimFacade.Instance.GetClaim(Of Claim)(claimID, Me.Dataset)
        Else
            objClaim = ClaimFacade.Instance.CreateClaim(Of Claim)(Me.Dataset)
        End If

        Return objClaim
    End Function

    Public Function AddClaimInvoice(ByVal claimInvoiceID As Guid) As ClaimInvoice
        Dim objClaimInvoice As ClaimInvoice

        If Not claimInvoiceID.Equals(Guid.Empty) Then
            objClaimInvoice = New ClaimInvoice(claimInvoiceID, Me.Dataset)
        Else
            objClaimInvoice = New ClaimInvoice(Me.Dataset)
        End If

        Return objClaimInvoice
    End Function

    Public Function AddNewComment() As Comment
        _comment = New Comment(Me.Dataset)
        CurrentComment.PopulateWithDefaultValues(Invoiceable.CertificateId)
        If Me.IsPaymentAdjustment Then
            CurrentComment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PAYMENT_ADJUSTMENT)
        ElseIf Me.IsPaymentReversal Then
            CurrentComment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PAYMENT_REVERSAL)
        End If

        _comment.ClaimId = Invoiceable.Claim_Id

        Return _comment
    End Function

    'Public Sub LoadAgain()
    '    Me.Load()
    'End Sub

    Public Function CreateSecondClaimInvoice() As ClaimInvoice
        Return Me.SecondClaimInvoice
    End Function

    Public Sub RefreshCurrentClaim()
        Me._invoiceable = Nothing
        Dim blnRefresh As Boolean = Me.Invoiceable(True).IsDirty ' this line does nothing except reloading the current claim.
    End Sub
#End Region
End Class


