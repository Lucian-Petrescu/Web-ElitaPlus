Imports System.Text
Imports System.Collections.Generic
Imports Assurant.Common.MessagePublishing

Public NotInheritable Class VendorAuthorizationFileLoad
    Inherits FileLoadBase(Of ClaimloadFileProcessed, ClaimloadReconWrk)

#Region "Constants"
    Private Const FINAL_STATUS_GROUP_NUMBER As Short = 1
    Private Const DIAGNOSTIC_SKU = "DIAGNOSTIC"
    Private Const DIAGNOSTIC_SKU_DESCRIPTION = "Diagnostic"
#End Region

#Region "Constructor"
    Public Sub New(threadCount As Integer, transactionSize As Integer)
        MyBase.New(True) '' Custom Save Constructor
        YesId = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_Y)
        ServiceClassRepairId = LookupListNew.GetIdFromCode(LookupListCache.LK_SERVICE_CLASS, Codes.SERVICE_CLASS__REPAIR)
        ServiceTypeDiagnosticId = LookupListNew.GetIdFromCode(LookupListCache.LK_SERVICE_TYPE_NEW, Codes.SERVICE_TYPE__DIAGNOSTIC_AMOUNT)
    End Sub

    Public Sub New()
        MyBase.New(True) '' Custom Save Constructor
        YesId = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_Y)
        ServiceClassRepairId = LookupListNew.GetIdFromCode(LookupListCache.LK_SERVICE_CLASS, Codes.SERVICE_CLASS__REPAIR)
        ServiceTypeDiagnosticId = LookupListNew.GetIdFromCode(LookupListCache.LK_SERVICE_TYPE_NEW, Codes.SERVICE_TYPE__DIAGNOSTIC_AMOUNT)
    End Sub
#End Region

#Region "Fields"
    Private _claimloadFileProcessed As ClaimloadFileProcessed
    Private ReadOnly YesId As Guid
    Private ReadOnly ServiceClassRepairId As Guid
    Private ReadOnly ServiceTypeDiagnosticId As Guid
    Private _finalStatusList As List(Of Guid)
    Private _finalStatusLookupList As Dictionary(Of String, Guid)
    Private _pendingReviewForPaymentId As Guid
    Private _claim As MultiAuthClaim
#End Region

#Region "Properties"
    Private Property ClaimLoadFileProcessed As ClaimloadFileProcessed
        Get
            Return _claimloadFileProcessed
        End Get
        Set
            _claimloadFileProcessed = value
        End Set
    End Property

    Private ReadOnly Property FinalStatusList As List(Of Guid)
        Get
            Return _finalStatusList
        End Get
    End Property

    Private ReadOnly Property FinalStatusLookupList As Dictionary(Of String, Guid)
        Get
            Return _finalStatusLookupList
        End Get
    End Property

    Private ReadOnly Property PendingReviewForPaymentId As Guid
        Get
            Return _pendingReviewForPaymentId
        End Get
    End Property

    Private Property Claim As MultiAuthClaim
        Get
            Return _claim
        End Get
        Set
            _claim = value
        End Set
    End Property
#End Region

    Protected Overrides Function CreateFileLoadHeader(fileLoadHeaderId As System.Guid) As ClaimloadFileProcessed
        ClaimLoadFileProcessed = New ClaimloadFileProcessed(fileLoadHeaderId)
        Return ClaimLoadFileProcessed
    End Function

    Protected Overrides Function CreateFileLoadDetail(fileLoadDetailId As System.Guid, headerRecord As ClaimloadFileProcessed) As ClaimloadReconWrk
        Dim returnValue As ClaimloadReconWrk
        returnValue = New ClaimloadReconWrk(fileLoadDetailId, headerRecord.Dataset)
        Return returnValue
    End Function

    Public Overrides Sub AfterCreateFileLoadHeader()
        MyBase.AfterCreateFileLoadHeader()
        _finalStatusList = New List(Of Guid)
        _finalStatusLookupList = New Dictionary(Of String, Guid)
        Dim oCompany As New Company(ClaimLoadFileProcessed.CompanyId, ClaimLoadFileProcessed.Dataset)
        Dim dv As DataView
        dv = ClaimStatusByGroup.getListByCompanyGroupOrDealer(ClaimStatusByGroupDAL.SearchByType.CompanyGroup, oCompany.CompanyGroupId, Guid.Empty)
        For Each dvr As DataRowView In dv
            Dim listItemId As Guid
            listItemId = New Guid(DirectCast(dvr(ClaimStatusByGroupDAL.COL_NAME_LIST_ITEM_ID), Byte()))
            If (LookupListNew.GetCodeFromId(LookupListCache.LK_EXTENDED_CLAIM_STATUSES, listItemId) = Codes.CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT) Then
                _pendingReviewForPaymentId = New Guid(DirectCast(dvr(ClaimStatusByGroupDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID), Byte()))
            Else
                If ((dvr(ClaimStatusByGroupDAL.COL_NAME_GROUP_NUMBER) IsNot DBNull.Value) AndAlso (DirectCast(dvr(ClaimStatusByGroupDAL.COL_NAME_GROUP_NUMBER), Short) = FINAL_STATUS_GROUP_NUMBER)) Then
                    Dim claimStatusByGroupId As Guid
                    claimStatusByGroupId = New Guid(DirectCast(dvr(ClaimStatusByGroupDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID), Byte()))
                    _finalStatusList.Add(claimStatusByGroupId)
                    _finalStatusLookupList.Add(LookupListNew.GetCodeFromId(LookupListCache.LK_EXTENDED_CLAIM_STATUSES, listItemId), claimStatusByGroupId)
                End If
            End If
        Next
    End Sub

    Private Function GetEquipmentTypeCode(device As String) As String
        If (device Is Nothing) Then
            Return Codes.EQUIPMENT_COND__NEWORUSED
        Else
            Select Case device.Trim.ToUpper()
                Case "NEW"
                    Return Codes.EQUIPMENT_COND__NEW
                Case "REF"
                    Return Codes.EQUIPMENT_COND__USED
                Case "UNK"
                    Return Codes.EQUIPMENT_COND__NEWORUSED
                Case Else
                    Throw New NotSupportedException(String.Format("Device {0} is not supported", device.Trim.ToUpper()))
            End Select
        End If
    End Function

    Private Sub InsertOrUpdateReplacementPart(sku As String, description As String, claim As ClaimBase)
        If (sku Is Nothing OrElse sku.Trim().Length = 0) Then Return
        If (description Is Nothing OrElse description.Trim().Length = 0) Then Return

        ' Check if Part is already Present
        Dim oReplacementPart As ReplacementPart
        oReplacementPart = (From item As ReplacementPart In claim.ReplacementPartChildren Where item.SkuNumber.ToUpperInvariant() = sku.Trim.ToUpperInvariant() Select item).FirstOrDefault()
        If (oReplacementPart Is Nothing) Then oReplacementPart = claim.GetNewReplacementPartChild()
        oReplacementPart.SkuNumber = sku.Trim().ToUpperInvariant()
        oReplacementPart.Description = description
        oReplacementPart.Save()
    End Sub

    Private Sub UpdateClaimAuthorization(claimAuthorization As ClaimAuthorization, reconRecord As ClaimloadReconWrk)
        claimAuthorization.DeviceReceptionDate = reconRecord.DeviceReceptionDate
        claimAuthorization.ProblemFound = reconRecord.ProblemFound
        claimAuthorization.TechnicalReport = reconRecord.TechnicalReport
        claimAuthorization.DeliveryDate = reconRecord.DeliveryDate
        claimAuthorization.ServiceLevelId = LookupListNew.GetIdFromCode(LookupListCache.LK_SERVICE_LEVEL, reconRecord.ServiceLevel)
        claimAuthorization.ServiceCenterReferenceNumber = reconRecord.AuthorizationNum
        claimAuthorization.Source = ClaimLoadFileProcessed.Filename
        If ((reconRecord.BatchNumber IsNot Nothing) AndAlso (reconRecord.BatchNumber.Trim().Length > 0)) Then
            claimAuthorization.BatchNumber = reconRecord.BatchNumber
        End If
    End Sub

    Protected Overrides Function ProcessDetailRecord(reconRecord As ClaimloadReconWrk, familyDataSet As DataSet) As ProcessResult
        Try
            Dim claim As MultiAuthClaim
            Dim claimAuthorization As ClaimAuthorization
            Dim claimAuthDetails As ClaimAuthDetail

            Me.Claim = Nothing

            ' Create Instance of Claim and Claim Authorizations based on Recon Record
            claim = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(reconRecord.OriginalClaimId) '' Create New DataSet
            familyDataSet = claim.Dataset
            claimAuthorization = claim.ClaimAuthorizationChildren.GetChild(reconRecord.ClaimAuthorizationId)

            ' Change of Service Center
            If (Not claimAuthorization.ServiceCenterId.Equals(reconRecord.ServiceCenterId)) Then
                claimAuthorization.Void()
                claimAuthorization = claimAuthorization.Claim.AddClaimAuthorization(reconRecord.ServiceCenterId)
            End If

            ' Set Claim Common Properties
            claim.DeductibleCollected = reconRecord.DeductibleCollected
            claim.DealerReference = reconRecord.DealerReference
            claim.Pos = reconRecord.Pos

            UpdateClaimAuthorization(claimAuthorization, reconRecord)

            If ((reconRecord.CoverageCode IsNot Nothing) AndAlso (reconRecord.CoverageCode.Trim().Length > 0)) Then
                ' Change of Coverage Code
                If (Not reconRecord.CoverageCode.ToUpperInvariant().Equals(claim.CoverageTypeCode)) Then
                    Dim certificateItemCoverageId As Guid = Guid.Empty
                    Dim certItemCoverage As New CertItemCoverage
                    For Each oDrv As DataRowView In CertItemCoverage.GetItemCoverages(claimAuthorization.Claim.CertificateId)
                        Dim dr As DataRow = oDrv.Row
                        certItemCoverage = New CertItemCoverage(New Guid(CType(dr(CertItemCoverage.CertItemCoverageSearchDV.COL_CERT_ITEM_COVERAGE_ID), Byte())), familyDataSet)
                        If (certItemCoverage.CoverageTypeCode = reconRecord.CoverageCode) Then
                            ' Check if Coverage was in effect on Date of Loss
                            If (Not (claim.LossDate.Value >= certItemCoverage.BeginDate.Value AndAlso claim.LossDate.Value <= certItemCoverage.EndDate.Value)) Then
                                Continue For
                            End If
                            ' Check if Coverage is for Same item as that of Original Item
                            If (Not certItemCoverage.CertificateItem.Id.Equals(claim.CertificateItem.Id)) Then
                                Continue For
                            End If
                            ' Select Coverage ID for Change of Coverage
                            certificateItemCoverageId = certItemCoverage.Id
                            Exit For
                        End If
                    Next
                    ' Change Coverage only if Coverage is located
                    If (Not certificateItemCoverageId.Equals(Guid.Empty)) Then
                        ' Find Cause of Loss for Company Group and Coverage Type Combination
                        Dim oCoverageType As New CoverageType(certItemCoverage.CoverageTypeId, familyDataSet)
                        Dim causeOfLossId As Guid
                        If (oCoverageType.AssociatedCoveragesLoss.FindById(claimAuthorization.Claim.CauseOfLossId) Is Nothing) Then
                            causeOfLossId = oCoverageType.AssociatedCoveragesLoss.FindDefault().CauseOfLossId
                        Else
                            causeOfLossId = claimAuthorization.Claim.CauseOfLossId
                        End If

                        claimAuthorization.Claim.ChangeCoverageType(certificateItemCoverageId, causeOfLossId)
                    End If
                End If
            End If

            ' Update Date Properties
            claimAuthorization.RepairDate = reconRecord.RepairDate
            claimAuthorization.PickUpDate = reconRecord.PickupDate

            '-- Get Max Amounts
            Dim oMaximumLabor As Nullable(Of Decimal) = Nothing
            Dim oMaximumParts As Nullable(Of Decimal) = Nothing
            Dim oMaximumShipping As Nullable(Of Decimal) = Nothing
            Dim oMaximumDiagnostic As Nullable(Of Decimal) = Nothing
            Dim oMaximumReplacement As Nullable(Of Decimal) = Nothing
            Dim equipmentId As Guid
            Dim equipmentClassId As Guid
            Dim conditionId As Guid

            If (claim.Certificate.Dealer.UseEquipmentId = YesId) Then
                equipmentId = claim.ClaimedEquipment.EquipmentId
                equipmentClassId = claim.ClaimedEquipment.EquipmentBO.EquipmentClassId
                If (reconRecord.ReplacementType IsNot Nothing AndAlso reconRecord.ReplacementType.Trim.Length > 0) Then
                    conditionId = LookupListNew.GetIdFromCode(LookupListCache.LK_CONDITION, GetEquipmentTypeCode(reconRecord.ReplacementType))
                End If
            End If

            'If (LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_TYPE, claim.Dealer.DealerTypeId) = Codes.DEALER_TYPE_WEPP) Then
            '    If (Not reconRecord.ReplacementType Is Nothing AndAlso reconRecord.ReplacementType.Trim.Length > 0) Then
            '        conditionId = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, GetEquipmentTypeCode(reconRecord.ReplacementType))
            '    End If

            '    If Not claim.ClaimedEquipment Is Nothing AndAlso Not claim.ClaimedEquipment.EquipmentBO Is Nothing Then
            '        equipmentId = claim.ClaimedEquipment.EquipmentBO.Id
            '        equipmentClassId = claim.ClaimedEquipment.EquipmentBO.EquipmentClassId

            '    ElseIf Not claim.ClaimedEquipment Is Nothing Then
            '        equipmentId = Equipment.FindEquipment(claim.Dealer.Dealer, claim.ClaimedEquipment.Manufacturer, claim.ClaimedEquipment.Model, Date.Today)
            '        If (Not equipmentId = Guid.Empty) Then
            '            equipmentClassId = New Equipment(equipmentId).EquipmentClassId
            '        End If

            '    ElseIf LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, claim.Dealer.UseEquipmentId) = Codes.YESNO_Y Then
            '        Dim errors() As ValidationError = {New ValidationError(Codes.EQUIPMENT_NOT_FOUND, GetType(ClaimEquipment), Nothing, "", Nothing)}
            '        Throw New BOValidationException(errors, GetType(ClaimEquipment).FullName, "Test")
            '    End If
            'End If

            Dim priceListResultDv As PriceListDetail.PriceListResultsDV
            priceListResultDv = PriceListDetail.GetRepairPrices(claim.Company.Id, claimAuthorization.ServiceCenter.Code, claim.LossDate.Value, claim.RiskTypeId,
                                            claim.Certificate.SalesPrice, equipmentClassId, equipmentId, conditionId, claim.Certificate.Dealer.Id, reconRecord.ServiceLevel)
            priceListResultDv.RowFilter = String.Format("{0} = '{1}'", priceListResultDv.COL_NAME_SERVICE_LEVEL_CODE, reconRecord.ServiceLevel)

            For Each row As DataRowView In priceListResultDv
                If (row(PriceListDetail.PriceListResultsDV.COL_NAME_SERVICE_CLASS_CODE).ToString() = Codes.SERVICE_CLASS__REPAIR) Then
                    Select Case row(PriceListDetail.PriceListResultsDV.COL_NAME_SERVICE_TYPE_CODE).ToString()
                        Case Codes.SERVICE_TYPE__LABOR_AMOUNT
                            oMaximumLabor = New DecimalType(CType(row(PriceListDetail.PriceListResultsDV.COL_NAME_PRICE), Decimal))
                        Case Codes.SERVICE_TYPE__PARTS_AMOUNT
                            oMaximumParts = New DecimalType(CType(row(PriceListDetail.PriceListResultsDV.COL_NAME_PRICE), Decimal))
                        Case Codes.SERVICE_TYPE__LOGISTICS_AMOUNT
                            oMaximumShipping = New DecimalType(CType(row(PriceListDetail.PriceListResultsDV.COL_NAME_PRICE), Decimal))
                        Case Codes.SERVICE_TYPE__DIAGNOSTIC_AMOUNT
                            oMaximumDiagnostic = New DecimalType(CType(row(PriceListDetail.PriceListResultsDV.COL_NAME_PRICE), Decimal))
                    End Select
                ElseIf (row(PriceListDetail.PriceListResultsDV.COL_NAME_SERVICE_CLASS_CODE).ToString() = Codes.SERVICE_CLASS__REPLACEMENT) Then
                    Select Case row(PriceListDetail.PriceListResultsDV.COL_NAME_SERVICE_TYPE_CODE).ToString()
                        Case Codes.SERVICE_TYPE__REPLACEMENT_PRICE
                            oMaximumReplacement = New DecimalType(CType(row(PriceListDetail.PriceListResultsDV.COL_NAME_PRICE), Decimal))
                    End Select
                End If
            Next

            Dim oFinalStatus As String
            oFinalStatus = reconRecord.FinalStatus.Trim().ToUpper()

            ' Build Trace
            AppendTraceLine(String.Format("@ClaimId = {0}, @ClaimAuthorizationId = {1}, @CertificateId = {2}, @FinalStatus = {3}, @Serial# = {4}",
                                                  GuidControl.GuidToHexString(claim.Id), GuidControl.GuidToHexString(claimAuthorization.Id),
                                                  GuidControl.GuidToHexString(claim.Certificate.Id), oFinalStatus, reconRecord.SerialNumber))
            AppendTraceLine(String.Format("@Max Labor = {0}, @Max Parts = {1}, @Max Shipping = {2}, @Max Diagnostic = {3}, @Max Replacement = {4}",
                                                 DecimalTypeToString(oMaximumLabor), DecimalTypeToString(oMaximumParts), DecimalTypeToString(oMaximumShipping),
                                                 DecimalTypeToString(oMaximumDiagnostic), DecimalTypeToString(oMaximumReplacement)))
            AppendTraceLine(String.Format("@Before Auth Amount = {0}, @Incoming Amount = {1}",
                                                 DecimalTypeToString(claimAuthorization.AuthorizedAmount.ToString()), DecimalTypeToString(reconRecord.Amount)))

            '  Update Validation Switches
            Dim oValidateLaborAmount As Boolean
            Dim oValidatePartsAmount As Boolean
            Dim oValidateShippingAmount As Boolean
            Dim oValidateAuthorizationAmount As Boolean
            Dim oValidateDiagnosticAmount As Boolean
            Dim oAddComment As Boolean = False
            Dim oAdditionalComments As String
            Select Case oFinalStatus
                Case Codes.CLAIM_EXTENDED_STATUS__REPAIRED
                    ' Check if Original Replacement Claim
                    If (claimAuthorization.Claim.ClaimAuthorizationChildren.OrderBy(Function(item) item.CreatedDateTime).First().ClaimAuthorizationItemChildren.Where(Function(item) item.ServiceTypeCode = Codes.SERVICE_TYPE__REPLACEMENT_PRICE).Count() > 0) Then
                        ' Original Replacement
                        Dim cai As ClaimAuthItem
                        cai = claimAuthorization.ClaimAuthorizationItemChildren().Where(Function(item) item.ServiceTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_SERVICE_TYPE_NEW, Codes.SERVICE_TYPE__REPLACEMENT_PRICE)).FirstOrDefault()
                        cai.Amount = Math.Min(cai.Amount.Value, reconRecord.Amount.Value)
                        cai.Save()
                        claimAuthorization.Save()

                        ' Update Validation Switches
                        oValidateLaborAmount = False
                        oValidatePartsAmount = False
                        oValidateShippingAmount = False
                        oValidateAuthorizationAmount = True
                        oValidateDiagnosticAmount = False
                    Else
                        ' Compare Deductible Amount on Claim with Incoming Amount
                        ' If Incoming Amount is Lower or Equal then Update Auth Amount with Incoming Amount;
                        If (reconRecord.Amount.Value <= claim.Deductible.Value) Then
                            ' Update field Auth Amount with Incoming Amount
                            ' Deny Claim with Reason Denied, Under Deductible
                            claim.Status = BasicClaimStatus.Denied
                            claim.DenyClaim()
                            claim.VoidAuthorizations()
                            claim.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_CLOSED__DENIED_UNDER_DEDUCTIBLE)

                            ' Set Validation Switches
                            oValidateLaborAmount = False
                            oValidatePartsAmount = False
                            oValidateShippingAmount = False
                            oValidateAuthorizationAmount = False
                            oValidateDiagnosticAmount = False
                        Else
                            ' If Claim is Denied with reason Under Deductible then Change claim back to Active
                            ' Reprocessing Scenario
                            Dim authorizedAmount As Decimal = claimAuthorization.AuthorizedAmount
                            If (claim.Status = BasicClaimStatus.Denied AndAlso
                                claim.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_CLOSED__DENIED_UNDER_DEDUCTIBLE)) Then
                                claim.Status = BasicClaimStatus.Active
                                claim.DeniedReasonId = Nothing
                            End If

                            ' Check if Authorization Already Exists, if so update or else create new one
                            If (claimAuthorization.ClaimAuthStatus <> ClaimAuthorizationStatus.Void) Then
                                claimAuthorization.Void()
                            End If
                            claimAuthorization = claim.AddClaimAuthorization(reconRecord.ServiceCenterId)
                            With claimAuthorization.ClaimAuthorizationItemChildren.Where(Function(item) item.ServiceClassCode = Codes.SERVICE_CLASS__REPAIR).First()
                                ' Update Authorized Amount
                                .Amount = Math.Min(reconRecord.Amount.Value, authorizedAmount)
                                .Save()
                            End With
                            UpdateClaimAuthorization(claimAuthorization, reconRecord)
                            claimAuthorization.Save()

                            ' Update Validation Switches
                            oValidateLaborAmount = True
                            oValidatePartsAmount = True
                            oValidateShippingAmount = True
                            oValidateAuthorizationAmount = False
                            oValidateDiagnosticAmount = False
                        End If
                    End If
                Case Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_BEYOND_ECONOMICAL_REPAIR,
                     Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_LACK_OF_SERVICE_LEVEL, Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_OTHER_CAUSES,
                     Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_MISSING_SPARE_PARTS, Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_CYCLE_TIME_OUT

                    ' Check if Claim Authorization has Replacement Item
                    If (claimAuthorization.ClaimAuthorizationItemChildren.Where(Function(item) item.ServiceTypeCode = Codes.SERVICE_TYPE__REPLACEMENT_PRICE).Count() = 0) Then
                        '  Get Replacement Claim
                        Dim oClaim As MultiAuthClaim = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(claim.Id, claim.Dataset)
                        oClaim.CreateReplacementFromRepair(reconRecord.ServiceCenterId)
                        claimAuthorization = claim.ClaimAuthorizationChildren.Where(Function(item) item.ServiceCenterId = reconRecord.ServiceCenterId AndAlso item.ClaimAuthStatus = ClaimAuthorizationStatus.Authorized).First()
                    Else
                        claimAuthorization.Void()
                        claimAuthorization = claim.AddClaimAuthorization(reconRecord.ServiceCenterId)
                    End If

                    ' Remove Everything Except Replacement and Diagnostic
                    For Each item As ClaimAuthItem In claimAuthorization.ClaimAuthorizationItemChildren
                        If (Not (item.ServiceTypeCode = Codes.SERVICE_TYPE__REPLACEMENT_PRICE OrElse item.ServiceTypeCode = Codes.SERVICE_TYPE__DIAGNOSTIC_AMOUNT) _
                                 AndAlso (item.ServiceClassCode <> Codes.SERVICE_CLASS__DEDUCTIBLE)) Then
                            item.Delete()
                        End If
                    Next

                    Dim oClaimAuthItem As ClaimAuthItem
                    Dim authorizedAmount As Decimal = claimAuthorization.AuthorizedAmount
                    If (((oFinalStatus = Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_BEYOND_ECONOMICAL_REPAIR) AndAlso (oMaximumLabor IsNot Nothing)) OrElse
                        (((oFinalStatus = Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_LACK_OF_SERVICE_LEVEL) OrElse (oFinalStatus = Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_OTHER_CAUSES)) AndAlso (oMaximumDiagnostic IsNot Nothing))) Then
                        ' Original Claim Active with Auth Amount Replaced with Diag Cost
                        If (claimAuthorization.ClaimAuthorizationItemChildren.Where(Function(item) item.ServiceTypeCode = Codes.SERVICE_TYPE__DIAGNOSTIC_AMOUNT).Count() = 1) Then
                            oClaimAuthItem = claimAuthorization.ClaimAuthorizationItemChildren.Where(Function(item) item.ServiceTypeCode = Codes.SERVICE_TYPE__DIAGNOSTIC_AMOUNT).First()
                        Else
                            oClaimAuthItem = claimAuthorization.GetNewAuthorizationItemChild()
                            With oClaimAuthItem
                                .ServiceClassId = ServiceClassRepairId
                                .ServiceTypeId = ServiceTypeDiagnosticId
                                .VendorSku = DIAGNOSTIC_SKU
                                .VendorSkuDescription = DIAGNOSTIC_SKU_DESCRIPTION
                                .Amount = 0
                            End With
                        End If
                        oClaimAuthItem.Amount = Math.Min(authorizedAmount, reconRecord.LaborAmount.Value)
                        oClaimAuthItem.Save()
                    End If

                    '  Update Replacement Claim.AuthorizedAmount with Incoming Amount
                    If (reconRecord.ReplacementSerialNumber <> String.Empty) Then
                        ' Replacement Provided by 3PR
                        With claimAuthorization.ClaimAuthorizationItemChildren.Where(Function(item) item.ServiceTypeCode = Codes.SERVICE_TYPE__REPLACEMENT_PRICE).First()
                            .Amount = Math.Min(authorizedAmount, reconRecord.Amount.Value)
                            .Save()
                        End With
                        oValidateAuthorizationAmount = True
                    Else
                        With claimAuthorization.ClaimAuthorizationItemChildren.Where(Function(item) item.ServiceTypeCode = Codes.SERVICE_TYPE__REPLACEMENT_PRICE).First()
                            .Amount = oMaximumReplacement.GetValueOrDefault(0)
                            .Save()
                        End With
                        oValidateAuthorizationAmount = False
                    End If

                    UpdateClaimAuthorization(claimAuthorization, reconRecord)
                    claimAuthorization.Save()

                    ' Update Validation Switches
                    If (oFinalStatus = Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_BEYOND_ECONOMICAL_REPAIR) Then
                        oValidateLaborAmount = True
                    Else
                        oValidateLaborAmount = False
                    End If
                    oValidatePartsAmount = False
                    oValidateShippingAmount = False
                    oValidateDiagnosticAmount = True

                Case Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_IMEI_MISMATCH
                    ' Original Claim Active with Auth Amount Replaced with Diag Cost

                    If (oMaximumDiagnostic.HasValue) Then
                        ' Re-Activate Original Claim
                        claim.Status = BasicClaimStatus.Active
                        claim.DeniedReasonId = Nothing
                        Dim oClaimAuthItem As ClaimAuthItem = Nothing
                        Dim originalAuthorizedAmount As Decimal
                        originalAuthorizedAmount = claimAuthorization.AuthorizedAmount
                        If (claimAuthorization.ClaimAuthStatus <> ClaimAuthorizationStatus.Void) Then
                            claimAuthorization.Void()
                        End If
                        claimAuthorization = claim.AddClaimAuthorization(reconRecord.ServiceCenterId)
                        For Each oItem As ClaimAuthItem In claimAuthorization.ClaimAuthorizationItemChildren
                            If (oItem.ServiceTypeCode = Codes.SERVICE_TYPE__DIAGNOSTIC_AMOUNT) Then
                                oClaimAuthItem = oItem
                            Else
                                oItem.Delete()
                            End If
                        Next

                        If (oClaimAuthItem Is Nothing) Then
                            oClaimAuthItem = claimAuthorization.GetNewAuthorizationItemChild()
                            oClaimAuthItem.ServiceClassId = ServiceClassRepairId
                            oClaimAuthItem.ServiceTypeId = ServiceTypeDiagnosticId
                            oClaimAuthItem.VendorSku = DIAGNOSTIC_SKU
                            oClaimAuthItem.VendorSkuDescription = DIAGNOSTIC_SKU_DESCRIPTION
                            oClaimAuthItem.Amount = 0
                        End If
                        oClaimAuthItem.Amount = Math.Min(reconRecord.LaborAmount.Value, originalAuthorizedAmount)
                        oClaimAuthItem.Save()
                        If (claimAuthorization.ContainsDeductible AndAlso claimAuthorization.Claim.Dealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.GetPayDeductLookupList(Authentication.LangId), Codes.AUTH_LESS_DEDUCT_Y)) Then
                            claimAuthorization.AddDeductibleLineItem()
                        End If
                        UpdateClaimAuthorization(claimAuthorization, reconRecord)
                        claimAuthorization.Save()

                    Else
                        ' Deny Original Claim
                        claim.Status = BasicClaimStatus.Denied
                        claim.VoidAuthorizations()
                        claim.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED__NO_COVERAGE)
                    End If

                    '  Update Validation Switches
                    oValidateLaborAmount = True
                    oValidatePartsAmount = False
                    oValidateShippingAmount = False
                    oValidateAuthorizationAmount = False
                    oValidateDiagnosticAmount = True

                    ' Add Comment to Callback Customer
                    oAddComment = True
                Case Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_CLAIM_MISMATCH
                    ' Original Claim Active with Auth Amount Replaced with Diag Cost
                    claim.Deductible = 0D

                    If (oMaximumDiagnostic.HasValue) Then
                        ' Re-Activate Original Claim
                        claim.Status = BasicClaimStatus.Active
                        claim.DeniedReasonId = Nothing
                        Dim oClaimAuthItem As ClaimAuthItem = Nothing
                        Dim originalAuthorizedAmount As Decimal
                        originalAuthorizedAmount = claimAuthorization.AuthorizedAmount
                        If (claimAuthorization.ClaimAuthStatus <> ClaimAuthorizationStatus.Void) Then
                            claimAuthorization.Void()
                        End If
                        claimAuthorization = claim.AddClaimAuthorization(reconRecord.ServiceCenterId)
                        For Each oItem As ClaimAuthItem In claimAuthorization.ClaimAuthorizationItemChildren
                            If (oItem.ServiceTypeCode = Codes.SERVICE_TYPE__DIAGNOSTIC_AMOUNT) Then
                                oClaimAuthItem = oItem
                            Else
                                oItem.Delete()
                            End If
                        Next

                        If (oClaimAuthItem Is Nothing) Then
                            oClaimAuthItem = claimAuthorization.GetNewAuthorizationItemChild()
                            oClaimAuthItem.ServiceClassId = ServiceClassRepairId
                            oClaimAuthItem.ServiceTypeId = ServiceTypeDiagnosticId
                            oClaimAuthItem.VendorSku = DIAGNOSTIC_SKU
                            oClaimAuthItem.VendorSkuDescription = DIAGNOSTIC_SKU_DESCRIPTION
                            oClaimAuthItem.Amount = 0
                        End If
                        oClaimAuthItem.Amount = Math.Min(reconRecord.LaborAmount.Value, originalAuthorizedAmount)
                        oClaimAuthItem.Save()
                        If (claimAuthorization.ContainsDeductible AndAlso claimAuthorization.Claim.Dealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.GetPayDeductLookupList(Authentication.LangId), Codes.AUTH_LESS_DEDUCT_Y)) Then
                            claimAuthorization.AddDeductibleLineItem()
                        End If
                        UpdateClaimAuthorization(claimAuthorization, reconRecord)
                        claimAuthorization.Save()
                    Else
                        ' Deny Original Claim
                        claim.Status = BasicClaimStatus.Denied
                        claim.VoidAuthorizations()
                        claim.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED__NO_COVERAGE)

                        ' Add Comment to Callback Customer
                        oAddComment = True
                        oAdditionalComments = "Not covered for the problem found."
                    End If

                    ' Update Validation Switches
                    oValidateLaborAmount = False
                    oValidatePartsAmount = False
                    oValidateShippingAmount = False
                    oValidateAuthorizationAmount = False
                    oValidateDiagnosticAmount = True
                Case Codes.CLAIM_EXTENDED_STATUS__REPAIRED_UNDER_OEM_WARRANTY
                    ' Deny Original Claim
                    claim.Status = BasicClaimStatus.Denied
                    claim.VoidAuthorizations()
                    claim.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED__MANUFACTURER_WARRANTY)

                    ' Update Validation Switches
                    oValidateLaborAmount = False
                    oValidatePartsAmount = False
                    oValidateShippingAmount = False
                    oValidateAuthorizationAmount = False
                    oValidateDiagnosticAmount = False
                Case Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_NO_ORIGINAL_ACCESSORIES
                    ' Original Claim Active with Auth Amount Replaced with Diag Cost
                    claim.Deductible = 0D

                    If (oMaximumDiagnostic.HasValue) Then
                        ' Re-Activate Original Claim
                        claim.Status = BasicClaimStatus.Active
                        claim.DeniedReasonId = Nothing
                        Dim oClaimAuthItem As ClaimAuthItem = Nothing
                        Dim originalAuthorizedAmount As Decimal
                        originalAuthorizedAmount = claimAuthorization.AuthorizedAmount
                        If (claimAuthorization.ClaimAuthStatus <> ClaimAuthorizationStatus.Void) Then
                            claimAuthorization.Void()
                        End If
                        claimAuthorization = claim.AddClaimAuthorization(reconRecord.ServiceCenterId)
                        For Each oItem As ClaimAuthItem In claimAuthorization.ClaimAuthorizationItemChildren
                            If (oItem.ServiceTypeCode = Codes.SERVICE_TYPE__DIAGNOSTIC_AMOUNT) Then
                                oClaimAuthItem = oItem
                            Else
                                oItem.Delete()
                            End If
                        Next

                        If (oClaimAuthItem Is Nothing) Then
                            oClaimAuthItem = claimAuthorization.GetNewAuthorizationItemChild()
                            oClaimAuthItem.ServiceClassId = ServiceClassRepairId
                            oClaimAuthItem.ServiceTypeId = ServiceTypeDiagnosticId
                            oClaimAuthItem.VendorSku = DIAGNOSTIC_SKU
                            oClaimAuthItem.VendorSkuDescription = DIAGNOSTIC_SKU_DESCRIPTION
                            oClaimAuthItem.Amount = 0
                        End If
                        oClaimAuthItem.Amount = Math.Min(reconRecord.LaborAmount.Value, originalAuthorizedAmount)
                        oClaimAuthItem.Save()
                        If (claimAuthorization.ContainsDeductible AndAlso claimAuthorization.Claim.Dealer.PayDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.GetPayDeductLookupList(Authentication.LangId), Codes.AUTH_LESS_DEDUCT_Y)) Then
                            claimAuthorization.AddDeductibleLineItem()
                        End If
                        UpdateClaimAuthorization(claimAuthorization, reconRecord)
                        claimAuthorization.Save()
                    Else
                        ' Deny Original Claim
                        claim.Status = BasicClaimStatus.Denied
                        claim.VoidAuthorizations()
                        claim.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListCache.LK_DENIED_REASON, Codes.REASON_DENIED__NO_COVERAGE)

                        ' Add Comment to Callback Customer
                        oAddComment = True
                        oAdditionalComments = "Phone has no original accessories."
                    End If

                    ' Update Validation Switches
                    oValidateLaborAmount = False
                    oValidatePartsAmount = False
                    oValidateShippingAmount = False
                    oValidateAuthorizationAmount = False
                    oValidateDiagnosticAmount = True
            End Select

            ' Build Trace
            AppendTraceLine(String.Format("@Validate Labor = {0}, @Validate Parts = {1}, @Validate Shipping = {2}, @Validate Diagnostic = {3}, @Validate Auth = {4}",
                                                  oValidateLaborAmount.ToString(), oValidatePartsAmount.ToString(),
                                                  oValidateShippingAmount.ToString(), oValidateDiagnosticAmount.ToString(), oValidateAuthorizationAmount.ToString()))

            'w_traceLog := w_traceLog || ', @After Auth Amount = ' || case
            '                when w_Claim.Authorized_Amount is null then

            'w_traceLog := w_traceLog || ', @Replacement Created = ' || case
            '                when w_replacement_claim.claim_id is null then
            '                    'No'
            '                else
            '                    'Yes'
            '              end || ', @Org Claim Status = ' || w_claim.status_code ||
            '                    ', @Replacement Claim Status = ' ||
            '              w_replacement_claim.status_code;

            Dim oPendingForPay As Boolean = False

            ' Validate Parts Amount
            If (reconRecord.PartsAmount IsNot Nothing AndAlso reconRecord.PartsAmount.Value > oMaximumParts.GetValueOrDefault(0D)) Then
                oPendingForPay = True
                AppendTraceLine("#Pending for Payment because of Parts")
            End If

            ' Validate Labor Amount
            If (oFinalStatus = Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_BEYOND_ECONOMICAL_REPAIR OrElse
                oFinalStatus = Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_LACK_OF_SERVICE_LEVEL OrElse
                oFinalStatus = Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_OTHER_CAUSES OrElse
                oFinalStatus = Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_IMEI_MISMATCH OrElse
                oFinalStatus = Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_CLAIM_MISMATCH OrElse
                oFinalStatus = Codes.CLAIM_EXTENDED_STATUS__NOT_REPAIRED_NO_ORIGINAL_ACCESSORIES) Then

                If (reconRecord.LaborAmount IsNot Nothing AndAlso reconRecord.LaborAmount.Value > oMaximumDiagnostic.GetValueOrDefault(0D)) Then
                    oPendingForPay = True
                    AppendTraceLine("#Pending for Payment because of Diagnostic")
                End If
            Else
                If (reconRecord.LaborAmount IsNot Nothing AndAlso reconRecord.LaborAmount.Value > oMaximumLabor.GetValueOrDefault(0D)) Then
                    oPendingForPay = True
                    AppendTraceLine("#Pending for Payment because of Labor")
                End If
            End If

            ' Validate Shipping Amount
            If (reconRecord.ShippingAmount IsNot Nothing AndAlso reconRecord.ShippingAmount.Value > oMaximumShipping.GetValueOrDefault(0D)) Then
                oPendingForPay = True
                AppendTraceLine("#Pending for Payment because of Shipping")
            End If

            '-- Validate Max Authorized Amount (Replacement) Amount
            If (oValidateAuthorizationAmount) Then
                If (claimAuthorization.ClaimAuthorizationItemChildren.Where(Function(item) item.ServiceTypeCode = Codes.SERVICE_TYPE__REPLACEMENT_PRICE).Count() = 1) Then
                    With claimAuthorization.ClaimAuthorizationItemChildren.Where(Function(item) item.ServiceTypeCode = Codes.SERVICE_TYPE__REPLACEMENT_PRICE).First()
                        If (.Amount.Value > oMaximumReplacement.GetValueOrDefault(0)) Then
                            oPendingForPay = True
                            AppendTraceLine(", #Pending for Payment because of Replacement Amount")
                        End If
                    End With
                End If
            End If

            ' Update Authorization Table
            claimAuthorization.Save()


            ' *** Start - Claim Authorization Details
            ' Update Auth Details Table
            Try
                claimAuthDetails = claim.AddClaimAuthDetail(claim.Id, True, True)
            Catch ex As DataNotFoundException
                claimAuthDetails = claim.AddClaimAuthDetail(Guid.Empty)
            End Try
            With claimAuthDetails
                .ClaimId = claim.Id
                .LaborAmount = reconRecord.LaborAmount
                .PartAmount = reconRecord.PartsAmount
                .ServiceCharge = reconRecord.ServiceAmount
                .ShippingAmount = reconRecord.ShippingAmount
                .Save()
                claim.AuthDetailDataHasChanged = True
            End With
            ' *** End - Claim Authorization Details


            ' *** Start - Replacement Parts
            ' Add Replacement Parts
            For Each oReplacementPart As ReplacementPart In claim.ReplacementPartChildren
                Dim sku As String = oReplacementPart.SkuNumber.ToUpperInvariant()
                If (sku <> reconRecord.Part1Sku.ToUpperInvariant() AndAlso sku <> reconRecord.Part2Sku.ToUpperInvariant() AndAlso sku <> reconRecord.Part3Sku.ToUpperInvariant() _
                    AndAlso sku <> reconRecord.Part4Sku.ToUpperInvariant() AndAlso sku <> reconRecord.Part5Sku.ToUpperInvariant()) Then
                    oReplacementPart.Delete()
                End If
            Next

            InsertOrUpdateReplacementPart(reconRecord.Part1Sku, reconRecord.Part1Description, claim)
            InsertOrUpdateReplacementPart(reconRecord.Part2Sku, reconRecord.Part2Description, claim)
            InsertOrUpdateReplacementPart(reconRecord.Part3Sku, reconRecord.Part3Description, claim)
            InsertOrUpdateReplacementPart(reconRecord.Part4Sku, reconRecord.Part4Description, claim)
            InsertOrUpdateReplacementPart(reconRecord.Part5Sku, reconRecord.Part5Description, claim)
            ' *** End - Replacement Parts

            ' *** Start - Update Replacement Details
            If (reconRecord.ReplacementSerialNumber IsNot Nothing AndAlso reconRecord.ReplacementSerialNumber.Trim().Length > 0) Then
                Dim replacedEquipment As ClaimEquipment
                Dim claimEquipmentReplacementId As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_EQUIPMENT_TYPE, Codes.CLAIM_EQUIP_TYPE__REPLACEMENT)
                replacedEquipment = claim.ClaimEquipmentChildren.Where(Function(ce) ce.ClaimEquipmentTypeId = claimEquipmentReplacementId).FirstOrDefault()
                If (replacedEquipment Is Nothing) Then
                    replacedEquipment = New ClaimEquipment(claim.Dataset)
                    replacedEquipment.ClaimId = claim.Id
                    replacedEquipment.ClaimEquipmentTypeId = claimEquipmentReplacementId
                End If

                With replacedEquipment
                    .Model = reconRecord.ReplacementModel
                    .SerialNumber = reconRecord.ReplacementSerialNumber
                    .ManufacturerId = Manufacturer.ResolveManufacturer(reconRecord.ReplacementManufacturer, claim.Company.CompanyGroupId)
                    .SKU = reconRecord.ReplacementSku
                    .Save()
                End With
            End If
            ' *** End - Update Replacement Details


            ' *** Start - Update Final Status 
            ' Check if Final Status Already Exisits, if found then do-nothin
            ' Assumption - Final Status can not be changed during Re-Processing
            ' If not found then Insert New Record
            Dim oClaimStatusSearchDv As ClaimStatus.ClaimStatusSearchDV
            Dim oClaimStatus As ClaimStatus = Nothing
            oClaimStatusSearchDv = ClaimStatus.GetClaimStatusList(claim.Id)

            ' Check if Claim has Final Status
            For Each row As DataRowView In oClaimStatusSearchDv
                Dim cs As New ClaimStatus(GuidControl.ByteArrayToGuid(row(oClaimStatusSearchDv.COL_CLAIM_STATUS_ID)))
                If (FinalStatusList.Contains(cs.ClaimStatusByGroupId)) Then
                    ' Final Status is already updated on Claim
                    oClaimStatus = cs
                    Exit For
                End If
            Next

            If (oClaimStatus Is Nothing) Then
                ' Final Status does not exist, create one
                oClaimStatus = New ClaimStatus(familyDataSet)
                With oClaimStatus
                    .ClaimId = claim.Id
                    .ClaimStatusByGroupId = FinalStatusLookupList(oFinalStatus)
                    .StatusDate = DateTime.Now.AddSeconds(-1)
                    .Save()
                End With
            End If
            ' *** End - Update Final Status 

            ' *** Start - Pending Review for Payment
            ' Check if Claim has Pending Review Status
            oClaimStatus = Nothing
            For Each row As DataRowView In oClaimStatusSearchDv
                Dim cs As New ClaimStatus(GuidControl.ByteArrayToGuid(row(oClaimStatusSearchDv.COL_CLAIM_STATUS_ID)), claim.Dataset)
                If (cs.ClaimStatusByGroupId = PendingReviewForPaymentId) Then
                    ' Final Status is already updated on Claim
                    oClaimStatus = cs
                    Exit For
                End If
            Next

            If (oPendingForPay) Then
                If (oClaimStatus Is Nothing) Then
                    ' Add Extended Status for Pedning Review for Payment
                    oClaimStatus = New ClaimStatus(familyDataSet)
                    With oClaimStatus
                        .ClaimId = claim.Id
                        .ClaimStatusByGroupId = PendingReviewForPaymentId
                        .StatusDate = DateTime.Now
                        .Save()
                    End With
                End If
            Else
                If (oClaimStatus IsNot Nothing) Then
                    oClaimStatus.Delete()
                End If
            End If

            ' *** End - Pending Review for Payment

            ' Check if Comment needs to be Created for Claim
            Dim comment As Comment
            If (reconRecord.Comments IsNot Nothing AndAlso reconRecord.Comments.Trim().Length > 0) Then
                If (comment Is Nothing) Then comment = claim.AddNewComment()
                With comment
                    .ClaimId = claim.Id
                    .CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__OTHER)
                    .CertId = claim.Certificate.Id
                    .Comments = reconRecord.Comments
                    .Save()
                    claim.IsUpdatedComment = True
                End With
            End If

            ' Check if Comment is to be Added
            If (oAddComment) Then
                Dim commentText As New StringBuilder
                If (comment Is Nothing) Then
                    comment = claim.AddNewComment()
                Else
                    comment.Comments = comment.Comments + Environment.NewLine
                End If


                commentText.AppendFormat("Caller Name : {0}, Home : ", claim.CallerName)
                If (claim.Certificate.HomePhone Is Nothing) Then
                    commentText.Append("N/A")
                Else
                    commentText.Append(claim.Certificate.HomePhone)
                End If
                commentText.Append(", Work : ")
                If (claim.Certificate.WorkPhone Is Nothing) Then
                    commentText.Append("N/A")
                Else
                    commentText.Append(claim.Certificate.WorkPhone)
                End If

                If (oAdditionalComments IsNot Nothing AndAlso oAdditionalComments.Trim().Length > 0) Then
                    commentText.Append(", ").Append(oAdditionalComments)
                End If

                With comment
                    .ClaimId = claim.Id
                    .CertId = claim.Certificate.Id
                    .Comments = .Comments + commentText.ToString()
                    .CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CALL_BACK_CUSTOMER)
                    .Save()
                    claim.IsUpdatedComment = True
                End With
            Else
                For Each oComment As Comment In claim.ClaimCommentsList
                    If (oComment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListCache.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CALL_BACK_CUSTOMER)) Then
                        oComment.Delete()
                        claim.IsUpdatedComment = True
                    End If
                Next
            End If

            Me.Claim = claim

            Return ProcessResult.Loaded
        Catch ex As DataBaseAccessException
            AppConfig.Log(DirectCast(ex, Exception))
            If (ex.ErrorType = DataBaseAccessException.DatabaseAccessErrorType.BusinessErr) Then
                If (ex.Code Is Nothing OrElse ex.Code.Trim().Length = 0) Then
                    reconRecord.RejectReason = "Rejected During Load process"
                Else
                    reconRecord.RejectReason = TranslationBase.TranslateLabelOrMessage(ex.Code)
                    reconRecord.RejectReason = reconRecord.RejectReason.Substring(0, Math.Min(60, reconRecord.RejectReason.Length))
                End If
            Else
                reconRecord.RejectReason = "Rejected During Load process"
            End If
            reconRecord.RejectCode = "000"
            Return ProcessResult.Rejected
        Catch ex As BOValidationException
            AppConfig.Log(DirectCast(ex, Exception))
            reconRecord.RejectCode = "000"
            reconRecord.RejectReason = ex.ToRejectReason()
            reconRecord.RejectReason = reconRecord.RejectReason.Substring(0, Math.Min(60, reconRecord.RejectReason.Length))
            Return ProcessResult.Rejected
        Catch ex As Exception
            AppConfig.Log(ex)
            reconRecord.RejectCode = "000"
            reconRecord.RejectReason = "Rejected During Load process"
            Return ProcessResult.Rejected
        End Try
    End Function

    Protected Overrides Sub CustomSave(headerRecord As ClaimloadFileProcessed)
        MyBase.CustomSave(headerRecord)
        headerRecord.Save(Claim)
    End Sub
End Class
