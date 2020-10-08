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
    Property LoanerCenterId As Guid
    Property LoanerReturnedDate As DateType
    ReadOnly Property IsDirty As Boolean
    ReadOnly Property CanDisplayVisitAndPickUpDates As Boolean
    ReadOnly Property RiskType As String
    ReadOnly Property MethodOfRepairCode As String
    Property RiskTypeId As Guid

    ReadOnly Property AssurantPays As DecimalType
    Property Deductible As DecimalType
    Property DiscountAmount As DecimalType
    ReadOnly Property AboveLiability As DecimalType
    ReadOnly Property PayDeductibleId As Guid
    ReadOnly Property ConsumerPays As DecimalType
    Property LiabilityLimit As DecimalType

    'KDDI changes'
    ReadOnly Property IsReshipmentAllowed As String
    ReadOnly Property IsCancelShipmentAllowed As String
    'Methods to be implemented
    Sub SaveClaim(Optional ByVal Transaction As IDbTransaction = Nothing)
    Sub VerifyConcurrency(sModifiedDate As String)
    Sub CloseTheClaim()
    Sub CalculateFollowUpDate()
    Sub HandleGVSTransactionCreation(commentId As Guid, pIsNew As Nullable(Of Boolean))
    Function AddExtendedClaimStatus(claimStatusId As Guid) As ClaimStatus
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
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub


    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ClaimInvoiceDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New ClaimInvoiceDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
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


    Private Function getDecimalValue(decimalObj As DecimalType, Optional ByVal decimalDigit As Integer = 2) As Decimal
        If decimalObj Is Nothing Then
            Return 0D
        Else
            Return Math.Round(decimalObj.Value, decimalDigit, MidpointRounding.AwayFromZero)
        End If
    End Function

    Public Sub CreateDisbursement()
        Dim oRegion As Region
        If ClaimAuthorizationId = Guid.Empty AndAlso Dataset.Tables(ClaimInvoiceDAL.TABLE_NAME).Rows.Count > 1 Then
            Dim row As DataRow
            For Each row In Dataset.Tables(ClaimInvoiceDAL.TABLE_NAME).Rows
                Dim ci As New ClaimInvoice(row)

                ReInitiateFieldsNotIncludedInDataSet(ci)
                ci.CurrentDisbursement.PrepopulateFromClaimInvoice(ci)
                If Not ci.RegionId = Guid.Empty Then
                    oRegion = New Region(ci.RegionId)
                    ci.CurrentDisbursement.VendorRegionDesc = oRegion.Description
                Else
                    ci.CurrentDisbursement.VendorRegionDesc = String.Empty
                End If

                ci.CurrentDisbursement.perceptionIVA = PerceptionIVA
                ci.CurrentDisbursement.perceptionIIBB = PerceptionIIBB
                ci.CurrentDisbursement.Save()
                ci.DisbursementId = ci.CurrentDisbursement.Id

            Next
        Else
            CurrentDisbursement.PrepopulateFromClaimInvoice(Me)
            If Not RegionId = Guid.Empty Then
                oRegion = New Region(RegionId)
                CurrentDisbursement.VendorRegionDesc = oRegion.Description
            Else
                CurrentDisbursement.VendorRegionDesc = String.Empty
            End If
            CurrentDisbursement.perceptionIVA = PerceptionIVA
            CurrentDisbursement.perceptionIIBB = PerceptionIIBB
            CurrentDisbursement.Save()
            DisbursementId = CurrentDisbursement.Id
        End If
    End Sub

    Private Sub CreateClaimTax()

        ClaimTax.ClaimInvoiceId = Id
        ClaimTax.DisbursementId = DisbursementId
        ClaimTax.TaxTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_TAX_TYPES, "4")
        ClaimTax.Tax1Amount = PerceptionIVA
        ClaimTax.Tax1Description = "Peception_IVA"
        ClaimTax.Tax2Amount = PerceptionIIBB
        ClaimTax.Tax2Description = "Peception_IIBB"
        ClaimTax.Save()

    End Sub

    Public Sub CreateManualClaimTaxes(strTax1Desc As String, dTax1Amt As DecimalType, _
                                      strTax2Desc As String, dTax2Amt As DecimalType, _
                                      strTax3Desc As String, dTax3Amt As DecimalType, _
                                      strTax4Desc As String, dTax4Amt As DecimalType, _
                                      strTax5Desc As String, dTax5Amt As DecimalType, _
                                      strTax6Desc As String, dTax6Amt As DecimalType)
        ClaimTaxManual.ClaimInvoiceId = Id
        ClaimTaxManual.DisbursementId = DisbursementId
        ClaimTaxManual.TaxTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_TAX_TYPES, "7")
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

    Private Sub ReInitiateFieldsNotIncludedInDataSet(ci As ClaimInvoice)
        ' fields not included in the dataset need to reinitiate here
        ci.CloseClaim = True
        ci.PayeeOptionCode = PayeeOptionCode
        ci.IsNewPaymentFromPaymentAdjustment = IsNewPaymentFromPaymentAdjustment
        ci.PaymentMethodCode = PaymentMethodCode
        ci.PaymentMethodID = PaymentMethodID
        ci.DocumentType = DocumentType
        ci.PayeeBankInfo = PayeeBankInfo
        ci.PayeeAddress = PayeeAddress
        ci.TaxId = TaxId
    End Sub

    Private Sub ProcessClaim(Optional ByVal Transaction As IDbTransaction = Nothing)
        If ClaimAuthorizationId = Guid.Empty And Dataset.Tables(ClaimInvoiceDAL.TABLE_NAME).Rows.Count > 1 Then
            Dim row As DataRow
            For Each row In Dataset.Tables(ClaimInvoiceDAL.TABLE_NAME).Rows
                Dim ci As New ClaimInvoice(row)

                'Directly use the Invoiceable Property
                ReInitiateFieldsNotIncludedInDataSet(ci)
                ci.Invoiceable.InvoiceProcessDate = New DateType(Date.Now)
                ci.Invoiceable.CauseOfLossId = ci.CauseOfLossID
                ci.Invoiceable.RepairDate = ci.RepairDate
                ci.Invoiceable.RepairCodeId = ci.RepairCodeId
                ci.Invoiceable.PickUpDate = ci.PickUpDate
                'ci.CurrentClaim.SalvageAmount = ci.SalvageAmt
                If ci.InvoiceDate IsNot Nothing Then
                    ci.Invoiceable.InvoiceDate = ci.InvoiceDate
                End If
                ci.CloseClaim = True
                ci.Invoiceable.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListCache.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_PAID)
                ci.Invoiceable.IsComingFromPayClaim = True
                ci.Invoiceable.CloseTheClaim()

                ci.Invoiceable.CalculateFollowUpDate()

                'disable the loss date check for cancelled certification when paying claim
                ci.Invoiceable.IsRequiredCheckLossDateForCancelledCert = False

                ci.Invoiceable.SaveClaim(Transaction)

                If (ci.CancelPolicy AndAlso (Not ci.Invoiceable.CertificateId.Equals(Guid.Empty))) Then
                    Dim certBO As Certificate = New Certificate(ci.Invoiceable.CertificateId, Dataset)
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

            Invoiceable.InvoiceProcessDate = New DateType(Date.Now)
            Invoiceable.CauseOfLossId = CauseOfLossID
            'If Me.ClaimAuthorizationId = Guid.Empty Then
            Invoiceable.RepairDate = RepairDate
            Invoiceable.RepairCodeId = RepairCodeId
            Invoiceable.PickUpDate = PickUpDate
            Invoiceable.IsComingFromPayClaim = True
            'End If

            'CurrentClaim.SalvageAmount = Me.SalvageAmt

            'Logic to Decide whether to Close the Claim along with the current Payment
            If ClaimAuthorizationId = Guid.Empty Then 'Single Auth Claims
                If Invoiceable.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT And RemainingAmount.Value = 0 Then  'And Me.CloseClaim Then
                    Invoiceable.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__REPLACED)
                    Invoiceable.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListCache.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_REPAIRED)
                    Invoiceable.CloseTheClaim()
                ElseIf CloseClaim Or (Invoiceable.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT And RemainingAmount.Value = 0) Then
                    Invoiceable.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListCache.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_PAID)
                    Invoiceable.CloseTheClaim()
                End If
            Else 'Multi Auth Claims
                If CloseClaim And Invoiceable.ClaimActivityCode = Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT And RemainingAmount.Value = 0 Then
                    Invoiceable.ClaimActivityId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_ACTIVITIES, Codes.CLAIM_ACTIVITY__REPLACED)
                    Invoiceable.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListCache.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_REPAIRED)
                    Invoiceable.CloseTheClaim()
                ElseIf CloseClaim And (Invoiceable.ClaimActivityCode <> Codes.CLAIM_ACTIVITY__PENDING_REPLACEMENT And RemainingAmount.Value = 0) Then
                    Invoiceable.ReasonClosedId = LookupListNew.GetIdFromCode(LookupListCache.LK_REASONS_CLOSED, Codes.REASON_CLOSED__TO_BE_PAID)
                    Invoiceable.CloseTheClaim()
                End If
            End If



            Invoiceable.CalculateFollowUpDate()

            'disable the loss date check for cancelled certification when paying claim
            Invoiceable.IsRequiredCheckLossDateForCancelledCert = False
            If Invoiceable.StatusCode = Codes.CLAIM_STATUS__CLOSED And InvoiceDate IsNot Nothing Then
                Invoiceable.InvoiceDate = InvoiceDate
                CurrentDisbursement.InvoiceDate = InvoiceDate
            End If
            Invoiceable.SaveClaim(Transaction)

            If (CancelPolicy AndAlso (Not Invoiceable.CertificateId.Equals(Guid.Empty))) Then
                Dim certBO As Certificate = New Certificate(Invoiceable.CertificateId, Dataset)
                Dim CertItemCoverageBO As CertItemCoverage = New CertItemCoverage(Invoiceable.CertItemCoverageId, Dataset)
                If certBO.StatusCode <> Codes.CLAIM_STATUS__CLOSED Then
                    If Invoiceable.ClaimActivityCode = Codes.CLAIM_ACTIVITY__REPLACED AndAlso CanCancelCertificateBasedOnContractReplacementPolicy(certBO.DealerId, certBO.WarrantySalesDate.Value) Then
                        PrepareCancelCertificate(certBO)
                    End If
                End If
                Dim claimBO As Claim = New Claim(Invoiceable.Claim_Id)
                If (_cancelCertificateData Is Nothing And CType(certBO.ProductLiabilityLimit.ToString, Decimal) > 0 And certBO.ProdLiabilityPolicyCd.ToString = PROD_LIABILITY_LIMIT_CNL_POLICY) Then

                    If ((CType(ProductRemainLiabilityAmount(Invoiceable.CertificateId, claimBO.LossDate), Decimal) - Amount.Value <= 0)) Then

                        PrepareCancelCertificateForLiabilityLimit(certBO)
                    End If
                End If

                If (_cancelCertificateData Is Nothing And CType(CertItemCoverageBO.CoverageLiabilityLimit.ToString, Decimal) > 0 And certBO.ProdLiabilityPolicyCd.ToString = PROD_LIABILITY_LIMIT_CNL_POLICY) Then

                    If (CType(IsCertCoveragesEligbileforCancel(Invoiceable.CertificateId, Invoiceable.CertItemCoverageId, claimBO.LossDate).ToString, Integer) = 0 And
                            CType(CoverageRemainLiabilityAmount(Invoiceable.CertItemCoverageId, claimBO.LossDate), Decimal) - Amount.Value <= 0) Then
                        PrepareCancelCertificateForLiabilityLimit(certBO)
                    End If
                End If
            End If
            End If

            HandelExtendedStatusForGVS(True, False, False)
    End Sub
    'for DEF366
    Private Sub HandelExtendedStatusForGVS(blnForPayClaim As Boolean, blnForAdjustment As Boolean, blnForReversal As Boolean)
        'Add an INVOICE_PAID extended claim status when open paying a claim with GVS integrated
        ' Create transaction log header if the service center is integrated with GVS
        If Invoiceable.ServiceCenterObject IsNot Nothing AndAlso Invoiceable.ServiceCenterObject.IntegratedWithGVS AndAlso Invoiceable.ServiceCenterObject.IntegratedAsOf IsNot Nothing AndAlso Invoiceable.CreatedDateTime.Value >= Invoiceable.ServiceCenterObject.IntegratedAsOf.Value Then
            Dim newClaimStatusByGroupId As Guid = Guid.Empty
            If blnForPayClaim Then
                newClaimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(ClaimStatusDAL.INVOICE_PAID_EXTENDED_CLAIM_STATUS)
            ElseIf blnForReversal Then
                newClaimStatusByGroupId = ClaimStatusByGroup.GetClaimStatusByGroupID(ClaimStatusDAL.WAITING_DOCUMENTATION)
            ElseIf blnForAdjustment Then
                'no action
            End If

            If (blnForPayClaim Or blnForReversal) And Not blnForAdjustment Then
                Dim oExtendedeClaimStatus As ClaimStatus = Nothing
                oExtendedeClaimStatus = Invoiceable.AddExtendedClaimStatus(Guid.Empty)
                oExtendedeClaimStatus.ClaimId = Invoiceable.Claim_Id
                oExtendedeClaimStatus.ClaimStatusByGroupId = newClaimStatusByGroupId
                oExtendedeClaimStatus.StatusDate = DateTime.Now
                oExtendedeClaimStatus.HandelTimeZoneForClaimExtStatusDate()
                ' Create transaction log header if the service center is integrated with GVS
                Invoiceable.HandleGVSTransactionCreation(Guid.Empty, Nothing)
            End If

        End If
    End Sub
    Public Shared Function IsCertCoveragesEligbileforCancel(CertId As Guid, CertItemCoverageId As Guid, lossDate As DateType) As Integer
        Dim dal As New ClaimDAL
        Return dal.IsCertCoveragesEligibleforCancel(CertId, CertItemCoverageId, lossDate)
    End Function
    Public Shared Function ProductRemainLiabilityAmount(CertId As Guid, lossDate As DateType) As Decimal
        Dim dal As New ClaimDAL
        Return dal.ProductRemainLiabilityAmount(CertId, lossDate)
    End Function
    Public Shared Function CoverageRemainLiabilityAmount(CertItemCoverageId As Guid, lossDate As DateType) As Decimal
        Dim dal As New ClaimDAL
        Return dal.CoverageRemainLiabilityAmount(CertItemCoverageId, lossDate)
    End Function
    Private Sub PrepareCancelCertificate(certBO As Certificate)
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
                .customerPaid = getDecimalValue(Amount)
                .certificatestatus = parentCertBO.StatusCode
                .quote = "N"
            End With
        Else
            With _cancelCertificateData
                .companyId = CompanyId
                .dealerId = certBO.DealerId
                .certificate = certBO.CertNumber
                .source = certBO.Source
                .cancellationDate = Today
                .cancellationCode = Codes.REASON_CLOSED__TO_BE_REPAIRED
                .customerPaid = getDecimalValue(Amount)
                .certificatestatus = certBO.StatusCode
                .quote = "N"
            End With
        End If

        'This line has moved to be included in the transaction.
        'CertCancellation.CancelCertificate(oCancelCertificateData)
    End Sub

    Private Sub PrepareCancelCertificateForLiabilityLimit(certBO As Certificate)
        _cancelCertificateData = New CertCancellationData
        With _cancelCertificateData
            .companyId = CompanyId
            .dealerId = certBO.DealerId
            .certificate = certBO.CertNumber
            .source = certBO.Source
            .cancellationDate = Today
            .cancellationCode = Codes.REASON_CLOSED_LLE
            .customerPaid = getDecimalValue(Amount)
            .certificatestatus = certBO.StatusCode
            .quote = "N"
        End With

        'This line has moved to be included in the transaction.
        'CertCancellation.CancelCertificate(oCancelCertificateData)
    End Sub
    Private Function CanCancelCertificateBasedOnContractReplacementPolicy(dealerID As Guid, WarrantySalesDate As Date) As Boolean
        Dim contractBo As Contract = Contract.GetContract(dealerID, WarrantySalesDate)
        If contractBo Is Nothing Then
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.DEALER_DOES_NOT_HAVE_CURRENT_CONTRACT, GetType(ClaimInvoice), Nothing, Nothing, Nothing)}
            Throw New BOValidationException(errors, GetType(ClaimInvoice).FullName, UniqueId)
        End If


        If contractBo.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCL)) Then
            'Cancel Certificate'
            'Changes for REQ-1333, cancel cert after X number of replacement claims when cancel after paid

            Dim lngRepPolicyClaimCnt As Long = ReppolicyClaimCount.GetReplacementPolicyClaimCntByClaim(contractBo.Id, ClaimId)

            If lngRepPolicyClaimCnt = 1 Then 'if only require 1 replacement, cancel based on current replacement claim
                Return True
            End If

            'more than 1 replacement claims required before cancelling the cert
            Dim certBO As Certificate = New Certificate(Invoiceable.CertificateId, Dataset)
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
                        If dblPayment > 0 AndAlso Claimlist(i)(Certificate.CertificateClaimsDV.COL_CLAIM_NUMBER) <> ClaimNumber _
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
        ElseIf contractBo.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CLAST)) Then
            'Cancel Certificate in last 12 months'
            Dim certItemCoverageBO As CertItemCoverage = New CertItemCoverage(Invoiceable.CertItemCoverageId)
            If Invoiceable.CreatedDate.Value >= certItemCoverageBO.EndDate.Value.AddYears(-1).AddDays(1) Then
                Return True
            Else
                Return False
            End If
        ElseIf contractBo.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__KEEP)) _
            OrElse contractBo.ReplacementPolicyId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_REPLACEMENT_POLICIES, Codes.REPLACEMENT_POLICY__CNCLAF)) Then
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
        LaborAmt = New DecimalType(0D)
        LaborTax = New DecimalType(0D)
        PartAmount = New DecimalType(0D)
        PartTax = New DecimalType(0D)
        ServiceCharge = New DecimalType(0D)
        TripAmount = New DecimalType(0D)
        ShippingAmount = New DecimalType(0D)
        DispositionAmount = New DecimalType(0D)
        DiagnosticsAmount = New DecimalType(0D)
        PaytocustomerAmount = New DecimalType(0D)

        objclaimAuth = DirectCast(New ClaimAuthorization(ClaimAuthorizationId, Dataset), ClaimAuthorization)

        If objclaimAuth.ContainsDeductible AndAlso objclaimAuth.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_PAY_DEDUCTIBLE, Codes.AUTH_LESS_DEDUCT_Y) Then

            'The Payment Amount and the IVA Tax Calculation should not contain PayDeductible Amount
            Amount = ReconciledAmount.Value - objclaimAuth.PayDeductibleAmount

            'Set Deductible and Deductible tax Amount
            If excludeDeductible Then
                DeductibleAmount = Nothing
                DeductibleTaxAmount = Nothing
            Else
                DeductibleAmount = objclaimAuth.PayDeductibleAmount
                Dim dedTaxAmt As Decimal = (DeductibleAmount * getDecimalValue(DeductibleTaxRate)) / 100
                DeductibleTaxAmount = New DecimalType(dedTaxAmt)
            End If
        Else
            'The Payment Amount Remains unchanged
            Amount = ReconciledAmount.Value
            DeductibleAmount = Nothing
            DeductibleTaxAmount = Nothing
        End If

        OtherExplanation = "NET"
        GrossAmount = Amount.Value
        IvaAmount = GrossAmount - (Math.Round((getDecimalValue(GrossAmount) * 100) / (100 + getDecimalValue(TaxRate)), 2))
        OtherAmount = GrossAmount - IvaAmount

        'Pay Invoice Taxes if they are configured for the claim Authoization's Service Center
        'Taxes should be only paid once and with the first Claim Authoirzation of the Invoice 
        If isTaxTypeInvoice() Then
            'Check if this is the first Claim Authorization to be paid for this Invoice
            If Not Invoice.IsAnyClaimAuthorizationPaid Then
                'Get the Perception IVA and Perception IIBB for this Invoice from the Attributes
                PerceptionIVA = Invoice.PerceptionIVA
                PerceptionIIBB = Invoice.PerceptionIIBB
                RegionId = Invoice.PerceptionIIBBRegion
            End If
        End If

    End Sub

    Public Sub CalculateAmounts(Optional blnExcludeTaxComputation As Boolean = False)
        Dim invMethodDV As DataView = LookupListNew.GetInvoiceMethodLookupList()
        Dim invMethodDesc As String = LookupListNew.GetDescriptionFromId(invMethodDV, Company.InvoiceMethodId)

        Dim methodOfRepair As String
        If Invoiceable.MethodOfRepairCode = Codes.METHOD_OF_REPAIR_REPLACEMENT OrElse Invoiceable.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
            methodOfRepair = "RPL"
        Else
            methodOfRepair = "RPR"
        End If

        If invMethodDesc = INVOICE_METHOD_DETAIL OrElse Company.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_AUTH_DTL, "ADR")) Then  ' detail entry
            Dim subTotalAmt As Decimal = getDecimalValue(LaborAmt) +
                            getDecimalValue(LaborTax) +
                            getDecimalValue(PartAmount) +
                            getDecimalValue(PartTax) +
                            getDecimalValue(ServiceCharge) +
                            getDecimalValue(TripAmount) +
                            getDecimalValue(OtherAmount) +
                            getDecimalValue(ShippingAmount) +
                            getDecimalValue(DispositionAmount) +
                            getDecimalValue(DiagnosticsAmount) +
                            getDecimalValue(PaytocustomerAmount)

            If IsSalvagePayment Or getDecimalValue(Invoiceable.SalvageAmount) > 0 Then
                subTotalAmt = subTotalAmt - getDecimalValue(Invoiceable.SalvageAmount)
            End If

            Dim taxAmt As Decimal = 0
            If Not blnExcludeTaxComputation AndAlso TotalTaxAmount = 0 AndAlso subTotalAmt > 0 Then
                Me.WithholdingAmount = 0
                taxAmt = ComputeTotalTax(invMethodDesc, subTotalAmt, methodOfRepair)
            Else
                taxAmt = TotalTaxAmount
            End If
            'Dim taxAmt As Decimal = (subTotalAmt * getDecimalValue(Me.TaxRate)) / 100

            If IsIvaResponsibleFlag Then
                IvaAmount = New DecimalType(taxAmt)
            ElseIf TotalTaxAmount = 0 Then
                TotalTaxAmount = New DecimalType(taxAmt)
            End If

            Amount = New DecimalType(subTotalAmt + taxAmt)
            If Company.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_AUTH_DTL, "ADR")) Then
                Dim dblDeductible As Decimal
                Try
                    dblDeductible = Invoiceable.Deductible.Value
                Catch ex As Exception
                    dblDeductible = 0
                End Try
                Amount = Amount.Value - dblDeductible
                If Amount.Value < 0 Then 'if auth amount less than deductible, no payment
                    Amount = 0
                End If
            End If
        ElseIf invMethodDesc = INVOICE_METHOD_TOTAL Then ' total entry
            'Me.Amount is populated from the screen

            LaborAmt = New DecimalType(0D)
            LaborTax = New DecimalType(0D)
            PartAmount = New DecimalType(0D)
            PartTax = New DecimalType(0D)
            ServiceCharge = New DecimalType(0D)
            TripAmount = New DecimalType(0D)
            ShippingAmount = New DecimalType(0D)
            DispositionAmount = New DecimalType(0D)
            DiagnosticsAmount = New DecimalType(0D)
            PaytocustomerAmount = New DecimalType(0D)

            OtherExplanation = "NET"
            Dim taxRate As DecimalType = GetTaxRateOnOther(invMethodDesc, methodOfRepair)

            Dim subTotalAmt As Decimal = Math.Round((getDecimalValue(Amount) * 100) / (100 + getDecimalValue(taxRate)), 2)
            OtherAmount = New DecimalType(subTotalAmt)

            If IsIvaResponsibleFlag Then
                IvaAmount = New DecimalType(getDecimalValue(Amount) - subTotalAmt)
            ElseIf (getDecimalValue(Amount) - subTotalAmt) > 0 Then
                TotalTaxAmount = New DecimalType(getDecimalValue(Amount) - subTotalAmt)
            End If

        Else
            Throw New NotSupportedException()
        End If

    End Sub

    Public Function GetTaxRateOnOther(invMethodDesc As String, MethodOfRepair As String) As DecimalType
        Dim OtherTaxRate As DecimalType = 0

        Dim RegionId As Guid
        If PayeeAddress Is Nothing Then
            Dim addressObj As New Address(ServiceCenterAddressID)
            RegionId = addressObj.RegionId
        Else
            RegionId = PayeeAddress.RegionId
        End If

        With ClaimTaxRatesData(RegionId, MethodOfRepair)

            If invMethodDesc = INVOICE_METHOD_DETAIL Then ' detail entry
                'N/A
            ElseIf invMethodDesc = INVOICE_METHOD_TOTAL Then ' total entry
                'find tax rate for other; if not, then use (claim_tax_type 7)
                If .taxRateClaimOther > 0 Then
                    OtherTaxRate = New DecimalType(.taxRateClaimOther)
                Else
                    'IVA tax
                    OtherTaxRate = TaxRate

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
        If PayeeAddress Is Nothing Then
            Dim addressObj As New Address(ServiceCenterAddressID)
            RegionId = addressObj.RegionId
        Else
            RegionId = PayeeAddress.RegionId
        End If
        If ClaimTaxRatesData(RegionId, MethodOfRepair) IsNot Nothing Then

            With ClaimTaxRatesData(RegionId, MethodOfRepair)

                If invMethodDesc = INVOICE_METHOD_DETAIL Or Company.AuthDetailRqrdId.Equals(LookupListNew.GetIdFromCode(LookupListCache.LK_AUTH_DTL, "ADR")) Then ' detail entry
                    'apply each line detail tax rate as they have been assigned the default/super default value if none was found for the given line (this was done in Oracle pkg)
                    If LaborAmt IsNot Nothing AndAlso LaborAmt.Value > 0 AndAlso .taxRateClaimLabor > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(LaborAmt), .taxRateClaimLabor, .computeMethodCodeClaimLabor)
                        If .applyWithholdingFlagClaimLabor.Equals("Y") Then WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(LaborAmt), ServiceCenterWithholdingRate, "N")
                    End If
                    If PartAmount IsNot Nothing AndAlso PartAmount.Value > 0 AndAlso .taxRateClaimParts > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(PartAmount), .taxRateClaimParts, .computeMethodCodeClaimParts)
                        If .applyWithholdingFlagClaimParts.Equals("Y") Then WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(PartAmount), ServiceCenterWithholdingRate, "N")
                    End If
                    If ServiceCharge IsNot Nothing AndAlso ServiceCharge.Value > 0 AndAlso .taxRateClaimService > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(ServiceCharge), .taxRateClaimService, .computeMethodCodeClaimService)
                        If .applyWithholdingFlagClaimService.Equals("Y") Then WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(ServiceCharge), ServiceCenterWithholdingRate, "N")
                    End If
                    If TripAmount IsNot Nothing AndAlso TripAmount.Value > 0 AndAlso .taxRateClaimTrip > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(TripAmount), .taxRateClaimTrip, .computeMethodCodeClaimTrip)
                        If .applyWithholdingFlagClaimTrip.Equals("Y") Then WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(TripAmount), ServiceCenterWithholdingRate, "N")
                    End If
                    If ShippingAmount IsNot Nothing AndAlso ShippingAmount.Value > 0 AndAlso .taxRateClaimShipping > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(ShippingAmount), .taxRateClaimShipping, .computeMethodCodeClaimShipping)
                        If .applyWithholdingFlagClaimShipping.Equals("Y") Then WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(ShippingAmount), ServiceCenterWithholdingRate, "N")
                    End If
                    If DispositionAmount IsNot Nothing AndAlso DispositionAmount.Value > 0 AndAlso .taxRateClaimDisposition > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(DispositionAmount), .taxRateClaimDisposition, .computeMethodCodeClaimDisposition)
                        If .applyWithholdingFlagClaimDisposition.Equals("Y") Then WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(DispositionAmount), ServiceCenterWithholdingRate, "N")
                    End If
                    If DiagnosticsAmount IsNot Nothing AndAlso DiagnosticsAmount.Value > 0 AndAlso .taxRateClaimDiagnostics > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(DiagnosticsAmount), .taxRateClaimDiagnostics, .computeMethodCodeClaimDiagnostics)
                        If .applyWithholdingFlagClaimDiagnostics.Equals("Y") Then WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(DiagnosticsAmount), ServiceCenterWithholdingRate, "N")
                    End If
                    If OtherAmount IsNot Nothing AndAlso OtherAmount.Value > 0 AndAlso .taxRateClaimOther > 0 Then
                        taxAmt += computeTaxAmtByComputeMethod(getDecimalValue(OtherAmount), .taxRateClaimOther, .computeMethodCodeClaimOther)
                        If .applyWithholdingFlagClaimOther.Equals("Y") Then WithholdingAmount += computeTaxAmtByComputeMethod(getDecimalValue(OtherAmount), ServiceCenterWithholdingRate, "N")
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
        Return dal.GetClaimSumOfDeductibles(CompanyId, Invoiceable.Claim_Id)
    End Function
    Public Function GetClaimSumofInvoices() As DecimalType
        Dim dal As New ClaimInvoiceDAL
        Return dal.GetClaimSumOfInvoices(CompanyId, Invoiceable.Claim_Id)
    End Function
    Public Function GetPerceptionTax() As DecimalType
        Dim dal As New ClaimInvoiceDAL
        Return dal.GetClaimSumOfInvoices(CompanyId, Invoiceable.Claim_Id)
    End Function

    Public Property Invoiceable(Optional ByVal blnMustReload As Boolean = False) As IInvoiceable
        Get
            If _invoiceable Is Nothing Then
                If ClaimAuthorizationId = Guid.Empty Then
                    _invoiceable = DirectCast(ClaimFacade.Instance.GetClaim(Of ClaimBase)(ClaimId, Dataset, blnMustReload), Claim)
                Else
                    _invoiceable = DirectCast(New ClaimAuthorization(ClaimAuthorizationId, Dataset), ClaimAuthorization)
                End If
            End If
            Return _invoiceable
        End Get
        Private Set
            _invoiceable = value
        End Set
    End Property

    Public ReadOnly Property Company As Company
        Get
            If (_company Is Nothing) Then
                If (CompanyId.Equals(Guid.Empty)) Then
                    Return Nothing
                Else
                    Return New Company(CompanyId, Dataset)
                End If
            Else
                Return _company
            End If
        End Get
    End Property

    Public Sub PrepopulateClaimInvoice(invoiceable As IInvoiceable, Optional blnExcludeTaxComputation As Boolean = False)
        Me.Invoiceable = invoiceable
        ClaimId = invoiceable.Claim_Id
        ClaimAuthorizationId = invoiceable.ClaimAuthorizationId
        ClaimNumber = invoiceable.ClaimNumber
        CauseOfLossID = invoiceable.CauseOfLossId
        CompanyId = invoiceable.CompanyId
        RepairCodeId = invoiceable.RepairCodeId
        AuthorizationNumber = invoiceable.AuthorizationNumber
        RepairDate = invoiceable.RepairDate
        PickUpDate = invoiceable.PickUpDate
        RepairEstimate = invoiceable.RepairEstimate

        If ClaimAuthorizationId = Guid.Empty Then
            If invoiceable.PickUpDate IsNot Nothing AndAlso invoiceable.RepairDate IsNot Nothing Then
                'if backend claim
                SvcControlNumber = invoiceable.AuthorizationNumber
            End If
            CalculateAmounts(blnExcludeTaxComputation)
        Else
            CalculateAmountsForMultiAuthClaim()
            'This is a MultiAuthorization Claim, Check if all the Claim Authorization under this claim have been Paid then Close the Claim
            CloseClaim = True
            Dim oMultiAuthClaim As MultiAuthClaim
            oMultiAuthClaim = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(Me.Invoiceable.Claim_Id, Dataset)
            For Each auth As ClaimAuthorization In oMultiAuthClaim.ClaimAuthorizationChildren
                If auth.Id <> invoiceable.ClaimAuthorizationId Then
                    If (Not auth.ClaimAuthStatus = ClaimAuthorizationStatus.Void And Not auth.ClaimAuthStatus = ClaimAuthorizationStatus.Paid) Then
                        CloseClaim = False
                        Exit For
                    End If
                End If
            Next
        End If

    End Sub

    Public Sub PrepopulateDisbursment()

        CurrentDisbursement.PayeeOptionId = LookupListNew.GetIdFromCode(LookupListCache.LK_PAYEE, PAYEE_OPTION_SERVICE_CENTER)

        CurrentDisbursement.ClaimAuthorizationId = ClaimAuthorizationId

        PayeeAddress = New Address(ServiceCenterAddressID)

        CurrentDisbursement.Payee = ServiceCenterName

        Dim svcCenterBO As ServiceCenter = New ServiceCenter(Invoiceable.ServiceCenterId)
        If Not ClaimAuthorizationId = Guid.Empty Then
            PayeeOptionCode = PAYEE_OPTION_SERVICE_CENTER
            If Not svcCenterBO.BankInfoId.Equals(Guid.Empty) Then
                PayeeBankInfo = New BankInfo(svcCenterBO.BankInfoId, Dataset)
            End If
            If Not svcCenterBO.PaymentMethodId.Equals(Guid.Empty) Then
                PaymentMethodCode = LookupListNew.GetCodeFromId(LookupListCache.LK_PAYMENTMETHOD, svcCenterBO.PaymentMethodId)
            End If
        End If

        If svcCenterBO.PayMaster AndAlso (Not svcCenterBO.MasterCenterId.Equals(Guid.Empty)) Then
            Dim masterCenterBO As ServiceCenter = New ServiceCenter(svcCenterBO.MasterCenterId)
            PayeeAddress = New Address(masterCenterBO.AddressId)
            CurrentDisbursement.Payee = masterCenterBO.Description
            If Not ClaimAuthorizationId = Guid.Empty Then
                PayeeOptionCode = PAYEE_OPTION_MASTER_CENTER
                If Not masterCenterBO.BankInfoId.Equals(Guid.Empty) Then
                    PayeeBankInfo = New BankInfo(masterCenterBO.BankInfoId, Dataset)
                End If
                If Not masterCenterBO.PaymentMethodId.Equals(Guid.Empty) Then
                    PaymentMethodCode = LookupListNew.GetCodeFromId(LookupListCache.LK_PAYMENTMETHOD, masterCenterBO.PaymentMethodId)
                End If
            End If
        ElseIf Not Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
            CurrentDisbursement.PayeeOptionId = LookupListNew.GetIdFromCode(LookupListCache.LK_PAYEE, PAYEE_OPTION_LOANER_CENTER)
            Dim loanerCenterBO As ServiceCenter = New ServiceCenter(Invoiceable.LoanerCenterId)
            PayeeAddress = New Address(loanerCenterBO.AddressId)
            CurrentDisbursement.Payee = loanerCenterBO.Description
            If Not ClaimAuthorizationId = Guid.Empty Then
                PayeeOptionCode = PAYEE_OPTION_LOANER_CENTER
            End If
        End If
    End Sub

    Public Sub PopulateClaimInvoice(objOtherClaimInvoiceBO As ClaimInvoice)
        With objOtherClaimInvoiceBO
            CompanyId = .CompanyId
            ClaimId = .ClaimId
            RepairCodeId = .RepairCodeId
            CauseOfLossID = .CauseOfLossID
            ClaimNumber = .ClaimNumber
            SvcControlNumber = .SvcControlNumber
            RecordCount = .RecordCount
            BatchNumber = .BatchNumber
            RepairDate = .RepairDate
            RepairEstimate = .RepairEstimate
            LaborAmt = .LaborAmt
            LaborTax = .LaborTax
            PartAmount = .PartAmount
            PartTax = .PartTax
            ServiceCharge = .ServiceCharge
            TripAmount = .TripAmount
            OtherAmount = .OtherAmount
            OtherExplanation = .OtherExplanation
            IvaAmount = .IvaAmount
            Amount = .Amount
            DisbursementId = CurrentDisbursement.Id
            RejectReason = .RejectReason
            AuthorizationNumber = .AuthorizationNumber
            Source = .Source
            DispositionAmount = .DispositionAmount
            DiagnosticsAmount = .DiagnosticsAmount
            WithholdingAmount = .WithholdingAmount
            TotalTaxAmount = .TotalTaxAmount
            RegionId = .RegionId

        End With

    End Sub

    Public Sub ReverseClaimInvoice()
        RepairEstimate = New DecimalType((RepairEstimate).Value * REVERSE_MULTIPLIER)
        LaborAmt = New DecimalType((LaborAmt).Value * REVERSE_MULTIPLIER)
        LaborTax = New DecimalType((LaborTax).Value * REVERSE_MULTIPLIER)
        PartAmount = New DecimalType((PartAmount).Value * REVERSE_MULTIPLIER)
        PartTax = New DecimalType((PartTax).Value * REVERSE_MULTIPLIER)
        ServiceCharge = New DecimalType((ServiceCharge).Value * REVERSE_MULTIPLIER)
        TripAmount = New DecimalType((TripAmount).Value * REVERSE_MULTIPLIER)
        OtherAmount = New DecimalType((OtherAmount).Value * REVERSE_MULTIPLIER)
        IvaAmount = New DecimalType((IvaAmount).Value * REVERSE_MULTIPLIER)
        If DispositionAmount IsNot Nothing AndAlso DispositionAmount.Value > 0 Then DispositionAmount = New DecimalType((DispositionAmount).Value * REVERSE_MULTIPLIER)
        If DiagnosticsAmount IsNot Nothing AndAlso DiagnosticsAmount.Value > 0 Then DiagnosticsAmount = New DecimalType((DiagnosticsAmount).Value * REVERSE_MULTIPLIER)
        Amount = New DecimalType((Amount).Value * REVERSE_MULTIPLIER)

    End Sub

    Public Sub AdjustClaimInvoiceAmounts(adjustmentPercentage As Decimal)
        Try

            RepairEstimate = New DecimalType((RepairEstimate).Value * adjustmentPercentage)
            LaborAmt = New DecimalType((LaborAmt).Value * adjustmentPercentage)
            LaborTax = New DecimalType((LaborTax).Value * adjustmentPercentage)
            PartAmount = New DecimalType((PartAmount).Value * adjustmentPercentage)
            PartTax = New DecimalType((PartTax).Value * adjustmentPercentage)
            ServiceCharge = New DecimalType((ServiceCharge).Value * adjustmentPercentage)
            TripAmount = New DecimalType((TripAmount).Value * adjustmentPercentage)
            ShippingAmount = New DecimalType((ShippingAmount).Value * adjustmentPercentage)
            OtherAmount = New DecimalType((OtherAmount).Value * adjustmentPercentage)
            If DispositionAmount IsNot Nothing AndAlso DispositionAmount.Value > 0 Then DispositionAmount = New DecimalType((DispositionAmount).Value * adjustmentPercentage)
            If DiagnosticsAmount IsNot Nothing AndAlso DiagnosticsAmount.Value > 0 Then DiagnosticsAmount = New DecimalType((DiagnosticsAmount).Value * adjustmentPercentage)
            IvaAmount = New DecimalType((IvaAmount).Value * adjustmentPercentage)
            Amount = New DecimalType((Amount).Value * adjustmentPercentage)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property

    Public Sub CapturePayeeTaxDocumentation(PayeeOptionCode As String, objServiceCenter As ServiceCenter,
                                            objClaimInvoice As ClaimInvoice, objDisbursement As Disbursement,
                                            objCertificate As Certificate, txtDocumentType As String, txtIdentificationNumber As String)
        Select Case PayeeOptionCode
            Case PAYEE_OPTION_MASTER_CENTER, PAYEE_OPTION_SERVICE_CENTER, PAYEE_OPTION_LOANER_CENTER
                If objClaimInvoice.IsInsuranceCompany Then
                    objDisbursement.DocumentType = Codes.DOCUMENT_TYPE__CNPJ
                    objClaimInvoice.DocumentType = Codes.DOCUMENT_TYPE__CNPJ
                    objDisbursement.IdentificationNumber = objServiceCenter.TaxId
                    objClaimInvoice.TaxId = objServiceCenter.TaxId
                End If
            Case PAYEE_OPTION_CUSTOMER
                If objClaimInvoice.IsInsuranceCompany Then
                    objDisbursement.DocumentType = LookupListNew.GetCodeFromId(LookupListCache.LK_DOCUMENT_TYPES, objCertificate.DocumentTypeID)
                    objDisbursement.IdentificationNumber = objCertificate.IdentificationNumber
                    objClaimInvoice.DocumentType = objDisbursement.DocumentType
                    objClaimInvoice.TaxId = objDisbursement.IdentificationNumber
                End If
            Case PAYEE_OPTION_OTHER
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
    Public ReadOnly Property Id As Guid
        Get
            If Row(ClaimInvoiceDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_CLAIM_INVOICE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property CancelPolicy As Boolean
        Get
            Return _cancelPolicy
        End Get
        Set
            _cancelPolicy = Value
        End Set
    End Property

    <ValueMandatory("")>
    Public Property CloseClaim As Boolean
        Get
            Return _closeClaim
        End Get
        Set
            _closeClaim = Value
        End Set
    End Property
    <ValueMandatory("")>
    Public Property ClaimId As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property

    Public Property RepairCodeId As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_REPAIR_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_REPAIR_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_REPAIR_CODE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property ClaimNumber As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property SvcControlNumber As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_SVC_CONTROL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_SVC_CONTROL_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_SVC_CONTROL_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property RecordCount As LongType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_RECORD_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimInvoiceDAL.COL_NAME_RECORD_COUNT), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_RECORD_COUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)>
    Public Property BatchNumber As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_BATCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_BATCH_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_BATCH_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidCauseOfLoss("")>
    Public Property CauseOfLossID As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_CAUSE_OF_LOSS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_CAUSE_OF_LOSS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_CAUSE_OF_LOSS_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidRepairDate("")>
    Public Property RepairDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimInvoiceDAL.COL_NAME_REPAIR_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_REPAIR_DATE, Value)
        End Set
    End Property

    <ValueMandatoryConditionallyForInvDate(""), ValidInvoiceDate("")>
    Public Property InvoiceDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimInvoiceDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property

    'Invoice property is Not a Derived Property of Claim Authorization.
    ' It is only being set and used for the Payment of Claim Authorizations
    Dim _Invoice As Invoice
    Public Property Invoice As Invoice
        Get
            Return _Invoice
        End Get
        Set
            _Invoice = value
        End Set
    End Property

    <ValidLoanerReturnedDate("")>
    Public Property LoanerReturnedDate As DateType
        Get
            If Not Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                Return Invoiceable.LoanerReturnedDate
            Else
                Return Nothing
            End If
        End Get
        Set
            If Not Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                Invoiceable.LoanerReturnedDate = Value
            Else
                'ignore it
            End If
        End Set
    End Property


    Public Property RegionId As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property

    Public Property RepairEstimate As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_REPAIR_ESTIMATE) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_REPAIR_ESTIMATE, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_REPAIR_ESTIMATE), Decimal))
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_REPAIR_ESTIMATE, Value)
        End Set
    End Property



    Public Property LaborAmt As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_LABOR_AMT) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_LABOR_AMT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_LABOR_AMT), Decimal))
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_LABOR_AMT, Value)
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

    Public Property LaborTax As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_LABOR_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_LABOR_TAX), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_LABOR_TAX, Value)
        End Set
    End Property



    Public Property PartAmount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_PART_AMOUNT) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_PART_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_PART_AMOUNT), Decimal))

        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_PART_AMOUNT, Value)
        End Set
    End Property



    Public Property PartTax As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_PART_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_PART_TAX), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_PART_TAX, Value)
        End Set
    End Property



    Public Property ServiceCharge As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_SERVICE_CHARGE) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_SERVICE_CHARGE, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_SERVICE_CHARGE), Decimal))

        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_SERVICE_CHARGE, Value)
        End Set
    End Property

    Public Property TripAmount As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_TRIP_AMOUNT) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_TRIP_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_TRIP_AMOUNT), Decimal))

        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_TRIP_AMOUNT, Value)
        End Set
    End Property

    Public Property ShippingAmount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_SHIPPING_AMOUNT) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_SHIPPING_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_SHIPPING_AMOUNT), Decimal))

        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_SHIPPING_AMOUNT, Value)
        End Set
    End Property


    Public Property PaytocustomerAmount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_PAY_TO_CUST_AMOUNT) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_PAY_TO_CUST_AMOUNT, New DecimalType(0D))
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_PAY_TO_CUST_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_PAY_TO_CUST_AMOUNT, Value)
        End Set
    End Property

    Public Property OtherAmount As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_OTHER_AMOUNT) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_OTHER_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_OTHER_AMOUNT), Decimal))

        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_OTHER_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=255)>
    Public Property OtherExplanation As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_OTHER_EXPLANATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_OTHER_EXPLANATION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_OTHER_EXPLANATION, Value)
        End Set
    End Property

    Public Property DeductibleAmount As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_AMOUNT) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_AMOUNT), Decimal))

        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_AMOUNT, Value)
        End Set
    End Property

    Public Property DeductibleTaxAmount As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT), Decimal))

        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_DEDUCTIBLE_TAX_AMOUNT, Value)
        End Set
    End Property

    Public Property IvaAmount As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_IVA_AMOUNT) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_IVA_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_IVA_AMOUNT), Decimal))

        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_IVA_AMOUNT, Value)
        End Set
    End Property

    Public Property TotalTaxAmount As DecimalType
        Get
            CheckDeleted()

            If Row(ClaimInvoiceDAL.COL_NAME_TOTAL_TAX_AMOUNT) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_TOTAL_TAX_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_TOTAL_TAX_AMOUNT), Decimal))

        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_TOTAL_TAX_AMOUNT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidAmount(""), ValidateUserLimit("")>
    Public Property Amount As DecimalType
        Get
            CheckDeleted()


            If Row(ClaimInvoiceDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                SetValue(ClaimInvoiceDAL.COL_NAME_AMOUNT, New DecimalType(0D))
            End If
            Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_AMOUNT), Decimal))

        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property

    Public Property AdjustmentAmount As DecimalType
        Get
            Return _adjustmentAmount
        End Get
        Set
            _adjustmentAmount = Value
        End Set
    End Property
    Public Property TotalAmount As DecimalType
        Get
            Return _totalAmount
        End Get
        Set
            _totalAmount = Value
        End Set
    End Property

    Public Property DisbursementId As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_DISBURSEMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_DISBURSEMENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_DISBURSEMENT_ID, Value)
        End Set
    End Property

    Public Property CurrentCommentId As Guid
        Get
            Return _currentCommentId
        End Get
        Set
            _currentCommentId = Value
        End Set
    End Property
    Public ReadOnly Property CertItemId As Guid
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
    Public Property RejectReason As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property AuthorizationNumber As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)>
    Public Property Source As String
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimInvoiceDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property

    Private _excludeDed As DecimalType
    Public Property excludeDeductible As Boolean
        Get
            Return _excludeDed
        End Get
        Set
            _excludeDed = Value
        End Set
    End Property

    Public Property ReconciledAmount As DecimalType
        Get
            Return _reconciledAmount
        End Get
        Set
            _reconciledAmount = Value
        End Set
    End Property

    Private _PerceptionIVA As DecimalType

    Public Property PerceptionIVA As DecimalType
        Get
            Return _PerceptionIVA
        End Get
        Set
            _PerceptionIVA = Value
        End Set
    End Property

    Private _PerceptionIIBB As DecimalType
    Public Property PerceptionIIBB As DecimalType
        Get
            Return _PerceptionIIBB
        End Get
        Set
            _PerceptionIIBB = Value
        End Set
    End Property

    Public Property IsPaymentAdjustment As Boolean
        Get
            Return _isPaymentAdjustment
        End Get
        Set
            _isPaymentAdjustment = Value
        End Set
    End Property

    Public Property IsPaymentReversal As Boolean
        Get
            Return _isPaymentReversal
        End Get
        Set
            _isPaymentReversal = Value
        End Set
    End Property

    Public Property IsSalvagePayment As Boolean
        Get
            Return _isSalvagePayment
        End Get
        Set
            _isSalvagePayment = Value
        End Set
    End Property

    Public ReadOnly Property AdjustmentPercentage As Decimal
        Get
            If Amount.Value <> 0 Then
                Return (Amount.Value - AdjustmentAmount.Value) / Amount.Value
            Else
                Return 0
            End If
        End Get

    End Property

    Public Property IsNewPaymentFromPaymentAdjustment As Boolean
        Get
            Return _isNewPaymentFromPaymentAdjustment
        End Get
        Set
            _isNewPaymentFromPaymentAdjustment = Value
        End Set
    End Property

    Public Property ClaimAuthorizationId As Guid
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_CLAIM_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimInvoiceDAL.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Value)
        End Set
    End Property

    Public Property Bonus As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_BONUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_BONUS), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_BONUS, Value)
        End Set
    End Property

    Public Property BonusTax As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_BONUS_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_BONUS_TAX), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_BONUS_TAX, Value)
        End Set
    End Property


    Public Property DispositionAmount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_DISPOSITION_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_DISPOSITION_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_DISPOSITION_AMOUNT, Value)
        End Set
    End Property


    Public Property DiagnosticsAmount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_DIAGNOSTICS_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_DIAGNOSTICS_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_DIAGNOSTICS_AMOUNT, Value)
        End Set
    End Property


    Public Property WithholdingAmount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimInvoiceDAL.COL_NAME_WITHHOLDING_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimInvoiceDAL.COL_NAME_WITHHOLDING_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimInvoiceDAL.COL_NAME_WITHHOLDING_AMOUNT, Value)
        End Set
    End Property


    Public Property ServiceCenterWithholdingRate As DecimalType
        Get
            If _service_center_withhodling_rate IsNot Nothing Then
                If _service_center_withhodling_rate.Value > 0 Then
                    Return _service_center_withhodling_rate * (-1D)
                Else
                    Return _service_center_withhodling_rate
                End If
            Else
                Return New DecimalType(0D)
            End If
        End Get
        Set
            CheckDeleted()
            _service_center_withhodling_rate = Value
        End Set
    End Property
#End Region

#Region "Address Children"

    Public ReadOnly Property AddressChild As Address
        Get
            Dim newAddress = New Address
            Dim oCompany As New Company(CompanyId)
            newAddress.CountryId = oCompany.BusinessCountryId
            PayeeAddress = newAddress
            Return newAddress
        End Get
    End Property

    Public Function Add_Address(Optional ByVal addressId As Object = Nothing) As Address
        If addressId IsNot Nothing Then
            Return New Address(CType(addressId, Guid), Dataset, Nothing, True)
        Else
            Return New Address(Dataset, Nothing)
        End If
    End Function

    Public Function Add_BankInfo(Optional ByVal objBankInfoId As Object = Nothing) As BankInfo
        Dim BankInfoId As Guid = Guid.Empty
        If objBankInfoId IsNot Nothing Then BankInfoId = CType(objBankInfoId, Guid)

        If Not BankInfoId.Equals(Guid.Empty) Then
            Return New BankInfo(BankInfoId, Dataset)
        Else
            Dim objBankInfo As New BankInfo(Dataset)
            'default new Bank Info country to the Customer's country
            Dim objCertItem As New CertItem(CertItemId)
            Dim objCert As New Certificate(objCertItem.CertId)
            Dim objAddress As New Address(objCert.AddressId)
            objBankInfo.CountryID = objAddress.CountryId
            Return objBankInfo
        End If
    End Function
#End Region

#Region "Derived Properties"
    Public ReadOnly Property CurrentCertItem As CertItem
        Get
            If _cert_Item Is Nothing Then
                _cert_Item = New CertItem(CertItemId, Dataset)
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

    Public ReadOnly Property ClaimTax As ClaimTax
        Get
            If _ClaimTax Is Nothing Then
                _ClaimTax = New ClaimTax(Dataset)
            End If
            Return _ClaimTax
        End Get

    End Property

    Public ReadOnly Property ClaimTaxManual As ClaimTax
        Get
            If _ClaimTaxManual Is Nothing Then
                _ClaimTaxManual = New ClaimTax(Dataset)
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
    Public ReadOnly Property RemainingAmount As DecimalType
        Get
            Dim objclaimAuth As ClaimAuthorization
            'For MultiAuth Claims, if the Authorization contains deductible then Invoiceable.Deductible.Value would contain the deductible value at claim level 
            Dim deductiblePaidByAssurant As Decimal = 0D
            If (Invoiceable.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_PAY_DEDUCTIBLE, Codes.AUTH_LESS_DEDUCT_Y)) Or
                   (Invoiceable.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y)) Then
                If ClaimAuthorizationId.Equals(Guid.Empty) Then
                    If Invoiceable.ConsumerPays.Value > Invoiceable.Deductible.Value Then
                        'Check Contains Deductible
                        deductiblePaidByAssurant = Invoiceable.Deductible.Value

                    Else
                        deductiblePaidByAssurant = Invoiceable.ConsumerPays.Value
                    End If
                End If
            End If

            If IsNew Then
                Dim remAmt As Decimal
                If Not ClaimAuthorizationId = Guid.Empty Then
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
    Public ReadOnly Property AlreadyPaid As DecimalType
        Get
            If Not ClaimAuthorizationId = Guid.Empty Then
                CalculateAmountsForMultiAuthClaim()
            Else
                CalculateAmounts()
            End If

            Return New DecimalType(getDecimalValue(GetClaimSumofInvoices()))
        End Get
    End Property
    Public ReadOnly Property AlreadyPaidDeductible As DecimalType
        Get
            Return New DecimalType(getDecimalValue(GetClaimSumOfDeductibles()))
        End Get
    End Property
    Private ReadOnly Property CurrentDisbursement As Disbursement
        Get
            If _disbursement Is Nothing Then
                If DisbursementId.Equals(Guid.Empty) Then
                    _disbursement = New Disbursement(Me)
                Else
                    ' this will be used for view only. so we dont need to 
                    ' attach the disbursement dataset
                    _disbursement = New Disbursement(DisbursementId)
                End If
            End If
            Return _disbursement
        End Get
    End Property

    Private ReadOnly Property CurrentComment As Comment
        Get
            If _comment Is Nothing Then
                _comment = New Comment(Dataset)
            End If
            Return _comment
        End Get
    End Property
    Public ReadOnly Property CertificateNumber As String
        Get
            Dim certNumber As String = Nothing
            If Not Invoiceable.CertificateId.Equals(Guid.Empty) Then
                Dim certBO As Certificate = New Certificate(Invoiceable.CertificateId)
                certNumber = certBO.CertNumber
            End If
            Return certNumber
        End Get
    End Property

    Public ReadOnly Property CustomerName As String
        Get
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                Return Invoiceable.CustomerName
            End If
        End Get
    End Property

    Public ReadOnly Property ServiceCenterName As String
        Get
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                Return Invoiceable.ServiceCenterObject.Description
            End If
        End Get
    End Property

    Public ReadOnly Property RiskType As String
        Get
            If Invoiceable Is Nothing Then
                Return Nothing
            Else
                Return Invoiceable.RiskType
            End If
        End Get
    End Property

    Public ReadOnly Property Manufacturer As String
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
    Public ReadOnly Property Model As String
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
    Public ReadOnly Property SerialNumber As String
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
    Public ReadOnly Property CustomerAddressID As Guid
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
    Public ReadOnly Property ServiceCenterAddressID As Guid
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

            If Me.PayeeOptionCode IsNot Nothing AndAlso Me.PayeeOptionCode <> "" Then
                PayeeOptionCode = Me.PayeeOptionCode
            Else
                PayeeOptionCode = LookupListNew.GetCodeFromId(LookupListCache.LK_PAYEE, CurrentDisbursement.PayeeOptionId)
            End If

            If PayeeOptionCode = PAYEE_OPTION_SERVICE_CENTER Or PayeeOptionCode = PAYEE_OPTION_MASTER_CENTER Or PayeeOptionCode = PAYEE_OPTION_LOANER_CENTER Then
                Select Case PayeeOptionCode
                    Case PAYEE_OPTION_MASTER_CENTER
                        Dim claimServiceCenter As ServiceCenter = New ServiceCenter(Invoiceable.ServiceCenterId)
                        If Not claimServiceCenter.MasterCenterId.Equals(Guid.Empty) Then
                            Dim masterServiceCenter As ServiceCenter = New ServiceCenter(claimServiceCenter.MasterCenterId)
                            If Not masterServiceCenter.IvaResponsibleFlag Then
                                _service_center_withhodling_rate = masterServiceCenter.WithholdingRate
                                Return False
                            End If
                        End If
                    Case PAYEE_OPTION_SERVICE_CENTER
                        Dim claimServiceCenter As ServiceCenter = New ServiceCenter(Invoiceable.ServiceCenterId)
                        If Not claimServiceCenter.IvaResponsibleFlag Then
                            _service_center_withhodling_rate = claimServiceCenter.WithholdingRate
                            Return False
                        End If
                    Case PAYEE_OPTION_LOANER_CENTER
                        If Not Invoiceable.LoanerCenterId.Equals(Guid.Empty) Then
                            Dim loanerServiceCenter As ServiceCenter = New ServiceCenter(Invoiceable.LoanerCenterId)
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
                Dim claimServiceCenter As ServiceCenter = New ServiceCenter(Invoiceable.ServiceCenterId)
                _service_center_withhodling_rate = claimServiceCenter.WithholdingRate
                Return False
            Else
                _service_center_withhodling_rate = 0
                Return False
            End If
        End Get
    End Property


    Private Function GetTaxRate(taxOnDeductible As Boolean) As Decimal
        ' tax rate is only calculated for the service centers , not for customer or "other".
        ' besides that , the tax flag (iva_responsible) must be on for the service center/master or loaner center

        Dim retval As DecimalType = New DecimalType(0D)
        'REQ 1150
        Dim oCert As New Certificate(CurrentCertItem.CertId)

        If (taxOnDeductible) Then
            Dim oDealer As New Dealer(oCert.DealerId)
            If (oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_PAY_DEDUCTIBLE, Codes.YESNO_N)) Or
                (oDealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_PAY_DEDUCTIBLE, Codes.FULL_INVOICE_Y)) Then
                Return retval
            End If
        End If

        If IsIvaResponsibleFlag Then

            'to calculate the tax rate, we need the region id because sometaxes are based on the region.

            Dim payeeAddress As Address = New Address(ServiceCenterAddressID) 'Added by AA for WR761620
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
                    Dim companyBO As Company = New Company(CompanyId)
                    .countryID = companyBO.BusinessCountryId
                    .regionID = payeeAddress.RegionId
                    'REQ 1150
                    .dealerID = oCert.DealerId
                    If Invoiceable.InvoiceProcessDate Is Nothing Then
                        'If Me.CurrentClaim.RepairDate Is Nothing Then
                        .salesDate = System.DateTime.Now
                    Else
                        .salesDate = Invoiceable.InvoiceProcessDate.Value
                    End If
                    If (taxOnDeductible) Then
                        ' taxtype code - 9 = Deductible...
                        .taxtypeID = LookupListNew.GetIdFromCode(LookupListCache.LK_TAX_TYPES, "9")
                    Else
                        ' taxtype code - 7 = Repairs...
                        If Invoiceable.MethodOfRepairCode = Codes.METHOD_OF_REPAIR__REPLACEMENT Then
                            'Replacement claim, find the applicable IVA tax
                            Dim ReplacementTaxId As Guid
                            Dim certBO As Certificate = New Certificate(Invoiceable.CertificateId)
                            Dim ProductPrice As Decimal = certBO.SalesPrice.Value
                            dal.GetReplacementTaxType(Invoiceable.ServiceCenterId, Invoiceable.RiskTypeId, .salesDate, ProductPrice, ReplacementTaxId)
                            If ReplacementTaxId = Guid.Empty Then
                                .taxtypeID = LookupListNew.GetIdFromCode(LookupListCache.LK_TAX_TYPES, "7")
                            Else
                                .taxtypeID = ReplacementTaxId
                            End If
                        Else
                            .taxtypeID = LookupListNew.GetIdFromCode(LookupListCache.LK_TAX_TYPES, "7")
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
        Dim oCert As New Certificate(CurrentCertItem.CertId)

        'to calculate the tax rate, we need the region id because some taxes are based on the region.        
        Dim dal As ClaimInvoiceDAL = New ClaimInvoiceDAL
        With ClaimTaxRatesData
            Dim companyBO As Company = New Company(CompanyId)
            .countryID = companyBO.BusinessCountryId
            .regionID = payeeRegionId
            .dealerID = oCert.DealerId
            .claim_type = ClaimMethodOfRepair.ToUpper
            If Invoiceable.InvoiceProcessDate Is Nothing Then
                'If Me.CurrentClaim.RepairDate Is Nothing Then
                .salesDate = System.DateTime.Now
            Else
                .salesDate = Invoiceable.InvoiceProcessDate.Value
            End If
        End With

        ClaimTaxRatesData = dal.GetClaimTaxRates(ClaimTaxRatesData)

        Return ClaimTaxRatesData
    End Function

    Public Shared Function GetClaimTaxRatesData(CountryId As Guid, payeeRegionId As Guid, dealer_id As Guid, InvoiceProcessDate As Date, ClaimMethodOfRepair As String) As ClaimInvoiceDAL.ClaimTaxRatesData
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
            If _claimTaxRatesData Is Nothing Then
                _claimTaxRatesData = GetClaimTaxRates(payeeRegionId, ClaimMethodOfRepair)
            End If
            Return _claimTaxRatesData
        End Get
    End Property

    Public ReadOnly Property DeductibleTaxRate As DecimalType
        Get
            Return GetTaxRate(True)
        End Get
    End Property

    Public ReadOnly Property TaxRate As DecimalType
        Get
            Return GetTaxRate(False)
        End Get
    End Property

    Public ReadOnly Property isTaxTypeInvoice As Boolean
        Get

            Dim payeeAddress As Address = New Address(ServiceCenterAddressID)
            'REQ 1150
            Dim oCert As New Certificate(CurrentCertItem.CertId)
            If Not payeeAddress.RegionId.Equals(Guid.Empty) Then
                Dim taxRateData As ClaimInvoiceDAL.TaxRateData = New ClaimInvoiceDAL.TaxRateData
                With taxRateData
                    Dim companyBO As Company = New Company(CompanyId)
                    .countryID = companyBO.BusinessCountryId
                    .regionID = payeeAddress.RegionId
                    'REQ 1150
                    .dealerID = oCert.DealerId
                    If Invoiceable.InvoiceProcessDate Is Nothing Then
                        .salesDate = System.DateTime.Now
                    Else
                        .salesDate = Invoiceable.InvoiceProcessDate.Value
                    End If
                    ' taxtype code - 4 = Manual...
                    .taxtypeID = LookupListNew.GetIdFromCode(LookupListCache.LK_TAX_TYPES, "4")
                End With

                Dim dal As ClaimInvoiceDAL = New ClaimInvoiceDAL
                Return dal.GetInvoiceTaxType(taxRateData)

            End If

            Return False

        End Get
    End Property


    Public ReadOnly Property SecondClaimInvoice As ClaimInvoice
        Get
            If _claimInvoice Is Nothing Then
                _claimInvoice = New ClaimInvoice(Dataset)
            End If
            Return _claimInvoice
        End Get
    End Property

#End Region

#Region "Temp Properties"
    'Created a temp property for Serial Number to be maintained between page navigation.
    Public Property SerialNumberTempContainer As String
        Get
            Return _serialNumberTempContainer
        End Get
        Set
            _serialNumberTempContainer = Value
        End Set
    End Property

    <ValueMandatoryConditionally(""), ValidPickUpDate("")> _
    Public Property PickUpDate As DateType
        Get
            Return _pickupdate
        End Get
        Set
            _pickupdate = Value
        End Set
    End Property

    Public Property PayeeAddress As Address
        Get
            Return _payeeAddress
        End Get
        Set
            _payeeAddress = Value
        End Set
    End Property

    Public Property PayeeBankInfo As BankInfo
        Get
            Return _payeeBankInfo
        End Get
        Set
            _payeeBankInfo = Value
        End Set
    End Property

    Public ReadOnly Property IsInsuranceCompany As Boolean
        Get
            Dim objCompany As New Company(Invoiceable.CompanyId)
            If LookupListNew.GetCodeFromId(LookupListCache.LK_COMPANY_TYPE, objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_INSURANCE Then
                Return True
            Else
                Return False
            End If

        End Get
    End Property

    Public Property PayeeOptionCode As String
        Get
            Return _payeeOptionCode
        End Get
        Set
            _payeeOptionCode = Value
        End Set
    End Property

    Public Property PaymentMethodCode As String
        Get
            Return _paymentMethodCode
        End Get
        Set
            _paymentMethodCode = Value
        End Set
    End Property

    <RequiredConditionally("")> _
    Public Property PaymentMethodID As Guid
        Get
            Return _paymentMethodID
        End Get
        Set
            _paymentMethodID = Value
        End Set
    End Property

    <ValidStringLength("", Max:=5), ValueMandatoryConditionallyOnPayee("")> _
    Public Property DocumentType As String
        Get
            Return _documentType
        End Get
        Set
            _documentType = Value
        End Set
    End Property

    <ValidStringLength("", Max:=15), ValidTaxNumber("")> _
    Public Property TaxId As String
        Get
            Return _taxId
        End Get
        Set
            _taxId = Value
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Sub Save(Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal overrrideDsCreatorFlag As Boolean = False)
        Dim objDataBaseClaim As Claim
        Dim objclaimAuth As ClaimAuthorization
        Try
            If ClaimAuthorizationId = Guid.Empty Then
                objDataBaseClaim = ClaimFacade.Instance.GetClaim(Of Claim)(Invoiceable.Claim_Id)
                If objDataBaseClaim.ModifiedDate IsNot Nothing Then Invoiceable.VerifyConcurrency(objDataBaseClaim.ModifiedDate.ToString)
                'If Not objDataBaseClaim.ModifiedDate Is Nothing Then objDataBaseClaim.VerifyConcurrency(objDataBaseClaim.ModifiedDate.ToString)
            Else
                objclaimAuth = DirectCast(New ClaimAuthorization(ClaimAuthorizationId, Dataset), ClaimAuthorization)
                If objclaimAuth.Claim.ModifiedDate IsNot Nothing Then Invoiceable.VerifyConcurrency(objclaimAuth.Claim.ModifiedDate.ToString)
                'If Not objclaimAuth.Claim.ModifiedDate Is Nothing Then objclaimAuth.Claim.VerifyConcurrency(objclaimAuth.Claim.ModifiedDate.ToString)
            End If

            If Not (IsPaymentAdjustment Or IsPaymentReversal) Then
                If Not ClaimAuthorizationId = Guid.Empty Then
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
            If (_isDSCreator OrElse overrrideDsCreatorFlag) AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                'Process Claim and create Disbursement record for this invoice if new payment is made from Pay Invoice Form.
                If Not IsPaymentAdjustment And Not IsPaymentReversal Then
                    CreateDisbursement()
                    _ClaimTax = Nothing
                    If isTaxTypeInvoice() Then
                        CreateClaimTax()
                    End If

                    'set the claiminvoiceid and disbursement id of the claim tax record
                    If HasClaimTaxManual Then
                        ClaimTaxManual.ClaimInvoiceId = Id
                        ClaimTaxManual.DisbursementId = DisbursementId
                        ClaimTaxManual.Save()
                    End If

                    ProcessClaim(Transaction)
                Else
                    'create
                    Invoiceable.InvoiceProcessDate = New DateType(Date.Now)
                    'For def-366
                    If IsPaymentReversal Then HandelExtendedStatusForGVS(False, False, True)
                End If

                Dim dal As New ClaimInvoiceDAL

                UpdateFamily(Dataset)
                dal.UpdateFamily(Dataset, _cancelCertificateData, IsPaymentAdjustment Or IsPaymentReversal, Transaction)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached AndAlso Transaction Is Nothing Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
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

    Public Shared Function GetTaxRate(oTaxRateData As ClaimInvoiceDAL.TaxRateData) As ClaimInvoiceDAL.TaxRateData
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
    Public Shared Function getList(invoiceNumber As String, payee As String, ClaimNumber As String, _
                           createdDate As String, invoiceAmount As String, Optional ByVal sortBy As String = ClaimInvoiceSearchDV.COL_INVOICE_NUMBER) As ClaimInvoiceSearchDV

        Try
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(ClaimInvoice), Nothing, "Search", Nothing)}
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

    Public Shared Function getPaymentsList(oCompanyId As Guid, claimNumber As String) As ClaimInvoicesDV

        Try
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(ClaimInvoice), Nothing, "Search", Nothing)}
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

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ClaimInvoiceId(row) As Guid
            Get
                Return New Guid(CType(row(COL_CLAIM_INVOICE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceNumber(row As DataRow) As String
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

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ClaimInvoiceId(row) As Guid
            Get
                Return New Guid(CType(row(COL_CLAIM_INVOICE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceNumber(row As DataRow) As String
            Get
                Return row(COL_INVOICE_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property PaidBy(row As DataRow) As String
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_REMAINING_AMOUNT_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            If (Not obj.IsPaymentAdjustment) And (Not obj.IsPaymentReversal) Then
                If obj.Invoiceable.AssurantPays.Value > 0 Then
                    If ((obj.RemainingAmount IsNot Nothing) AndAlso obj.RemainingAmount.Value < 0) Or ((obj.Amount IsNot Nothing) AndAlso obj.Amount.Value <= 0) Then
                        Return False
                    End If
                ElseIf obj.Invoiceable.AssurantPays.Value < 0 Then
                    If Not obj.ClaimAuthorizationId = Guid.Empty Then
                        obj.CalculateAmountsForMultiAuthClaim()
                    Else
                        obj.CalculateAmounts()
                    End If
                    If ((obj.RemainingAmount IsNot Nothing) AndAlso obj.RemainingAmount.Value > 0) Or ((obj.Amount IsNot Nothing) AndAlso obj.Amount.Value >= 0) Then
                        Return False
                    End If
                End If

            Else
                If ((obj.Amount IsNot Nothing) AndAlso obj.Amount.Value = 0) Then
                    Return False
                End If
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateUserLimit
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.PAYMENT_AMOUNT_HAS_EXCEEDED_YOUR_PAYMENT_LIMIT_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)

            If ((obj.Amount IsNot Nothing) AndAlso obj.Amount.Value = 0) Then
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_REPAIR_DATE_ERR2)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
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
            If claimBO.Source IsNot Nothing Then
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_INVOICE_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)

            If obj.InvoiceDate IsNot Nothing Then

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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_LOANER_RETURNED_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            If obj.PickUpDate Is Nothing Then Return True
            Dim pickUpDate As Date = obj.GetShortDate(obj.PickUpDate.Value)
            Dim createdDate As Date = Today
            If obj.CreatedDate IsNot Nothing Then
                createdDate = obj.GetShortDate(obj.CreatedDate.Value)
            End If

            ' WR 756735: For a claim that is not added from a claim interface or a replacement:
            ' PickUp Date:
            ' Must be LT or EQ today. 
            ' Must be GT or EQ to Repair Date. 

            If pickUpDate > Today Then
                Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR1 '"Pick-Up Date Must Be Less Than Or Equal To Today."
                Return False
            End If

            If obj.RepairDate IsNot Nothing Then
                If pickUpDate < obj.GetShortDate(obj.RepairDate.Value) Then
                    Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR2  '"Pick-Up Date Must Be Greater Than Or Equal To Repair Date."
                    Return False
                End If
            Else
                Message = Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR5  '"Pick-Up Date Requires The Entry Of A Repair Date."
                Return False
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryConditionally
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_PICKUP_DATE_IS_REQUIRED_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            If obj.PickUpDate Is Nothing And obj.Invoiceable.CanDisplayVisitAndPickUpDates Then
                If obj.IsPaymentAdjustment Or obj.IsPaymentReversal OrElse obj.IsNewPaymentFromPaymentAdjustment Then
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            Dim objCompany As New Company(obj.CompanyId)
            If LookupListNew.GetCodeFromId(LookupListCache.LK_COMPANY_TYPE, objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_INSURANCE _
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            Dim objCompany As New Company(obj.CompanyId)

            If LookupListNew.GetCodeFromId(LookupListCache.LK_COMPANY_TYPE, objCompany.CompanyTypeId) = objCompany.COMPANY_TYPE_INSURANCE _
                AndAlso obj.PayeeOptionCode = obj.PAYEE_OPTION_OTHER Then

                If (obj.DocumentType Is Nothing OrElse obj.DocumentType.Equals(String.Empty)) Then Return False

                Dim dal As New ClaimInvoiceDAL
                Dim oErrMess As String
                Try
                    oErrMess = dal.ExecuteSP(obj.DocumentType, obj.TaxId)
                    If oErrMess IsNot Nothing Then
                        Message = UCase(oErrMess)
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COVERAGE_TYPE)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimInvoice = CType(objectToValidate, ClaimInvoice)
            'Dim oClaim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(obj.ClaimId)
            Dim ctCovLoss As CoverageLoss = New CoverageLoss(obj.CauseOfLossID, (New CertItemCoverage(obj.Invoiceable.CertItemCoverageId)).CoverageTypeId)
            If ctCovLoss.Row IsNot Nothing Then
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
        _disbursement = New Disbursement(Dataset)
        Return _disbursement
    End Function

    Public Function GetClaimTax() As ClaimTax
        _ClaimTax = New ClaimTax(Id, Dataset)
        Return _ClaimTax
    End Function

    'Note : AddClaim is not currently being utilized by MultiAuth Claims.
    Public Function AddClaim(claimID As Guid) As Claim
        Dim objClaim As Claim

        If Not claimID.Equals(Guid.Empty) Then
            objClaim = ClaimFacade.Instance.GetClaim(Of Claim)(claimID, Dataset)
        Else
            objClaim = ClaimFacade.Instance.CreateClaim(Of Claim)(Dataset)
        End If

        Return objClaim
    End Function

    Public Function AddClaimInvoice(claimInvoiceID As Guid) As ClaimInvoice
        Dim objClaimInvoice As ClaimInvoice

        If Not claimInvoiceID.Equals(Guid.Empty) Then
            objClaimInvoice = New ClaimInvoice(claimInvoiceID, Dataset)
        Else
            objClaimInvoice = New ClaimInvoice(Dataset)
        End If

        Return objClaimInvoice
    End Function

    Public Function AddNewComment() As Comment
        _comment = New Comment(Dataset)
        CurrentComment.PopulateWithDefaultValues(Invoiceable.CertificateId)
        If IsPaymentAdjustment Then
            CurrentComment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PAYMENT_ADJUSTMENT)
        ElseIf IsPaymentReversal Then
            CurrentComment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__PAYMENT_REVERSAL)
        End If

        _comment.ClaimId = Invoiceable.Claim_Id

        Return _comment
    End Function

    'Public Sub LoadAgain()
    '    Me.Load()
    'End Sub

    Public Function CreateSecondClaimInvoice() As ClaimInvoice
        Return SecondClaimInvoice
    End Function

    Public Sub RefreshCurrentClaim()
        _invoiceable = Nothing
        Dim blnRefresh As Boolean = Invoiceable(True).IsDirty ' this line does nothing except reloading the current claim.
    End Sub
#End Region
End Class


