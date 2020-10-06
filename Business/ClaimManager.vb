
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Public Class ClaimManager
    Implements IClaimManager

    Private ReadOnly m_CommonManager As ICommonManager
    Private ReadOnly m_DealerManager As IDealerManager
    Private ReadOnly m_CompanyManager As ICompanyManager
    Private ReadOnly m_CompanyGroupManager As ICompanyGroupManager
    Private ReadOnly m_EquipmentManager As IEquipmentManager
    Private ReadOnly m_CountryManager As ICountryManager
    Private ReadOnly m_CurrencyManager As ICurrencyManager
    Private Property m_CertificateManager As ICertificateManager
    Private Property m_AddressManager As IAddressManager
    Private Property m_ClaimRepository As IClaimRepository(Of Claim)
    Private Property m_ClaimInvoiceRepository As IClaimRepository(Of ClaimInvoice)
    Private Property m_DisbursementRepository As IClaimRepository(Of Disbursement)

    Private Property m_CertItemRepository As ICertificateRepository(Of CertificateItem)
    Private Property m_DealerRepository As IDealerRepository(Of Dealer)

    Public Const m_PriceListLowPrice As Decimal = 0.00
    Public Const m_PriceListHighPrice As Decimal = 999999999.99

    Public Const m_GPriceListHighPrice As Decimal = 9999999

    Public Const m_NewClaimRepairDedPercent As Integer = 30
    Public Const m_NewClaimOrigReplDedPercent As Integer = 50

    Public ReadOnly Property CommonManager As ICommonManager
        Get
            Return m_CommonManager
        End Get
    End Property

    Public ReadOnly Property DealerManager As IDealerManager
        Get
            Return m_DealerManager
        End Get
    End Property

    Public ReadOnly Property CompanyManager As ICompanyManager
        Get
            Return m_CompanyManager
        End Get
    End Property
    Public ReadOnly Property CompanyGroupManager As ICompanyGroupManager
        Get
            Return m_CompanyGroupManager
        End Get
    End Property
    Public ReadOnly Property CountryManager As ICountryManager
        Get
            Return m_CountryManager
        End Get
    End Property
    Public ReadOnly Property CurrencyManager As ICurrencyManager
        Get
            Return m_CurrencyManager
        End Get
    End Property
    Public ReadOnly Property CertificateManager As ICompanyManager
        Get
            Return m_CertificateManager
        End Get
    End Property
    Public ReadOnly Property AddressManager As IAddressManager
        Get
            Return m_AddressManager
        End Get
    End Property
    Public ReadOnly Property ClaimRepository As IClaimRepository(Of Claim)
        Get
            Return m_ClaimRepository
        End Get
    End Property

    Public ReadOnly Property DealerRepository As IDealerRepository(Of Dealer)
        Get
            Return m_DealerRepository
        End Get
    End Property

    Public ReadOnly Property CertItemRepository As ICertificateRepository(Of CertificateItem)
        Get
            Return m_CertItemRepository
        End Get
    End Property
    Public ReadOnly Property ClaimInvoiceRepository As IClaimRepository(Of ClaimInvoice)
        Get
            Return m_ClaimInvoiceRepository
        End Get
    End Property
    Public ReadOnly Property DisbursementRepository As IClaimRepository(Of Disbursement)
        Get
            Return m_DisbursementRepository
        End Get
    End Property
    Public Sub New(pCommonManager As ICommonManager,
                   pCertificateManager As ICertificateManager,
                   pDealerManager As IDealerManager,
                   pCompanyManager As ICompanyManager,
                   pCompanyGroupManager As ICompanyGroupManager,
                   pEquipmentManager As IEquipmentManager,
                   pCountryManager As ICountryManager,
                   pCurrencyManager As ICurrencyManager,
                   pAddressManager As IAddressManager,
                   pClaimRepository As IClaimRepository(Of Claim),
                   pDealerRepository As IDealerRepository(Of Dealer),
                   pCertificateItemRepository As ICertificateRepository(Of CertificateItem),
                   pClaimInvoiceRepository As IClaimRepository(Of ClaimInvoice),
                   pDisbursementRepository As IClaimRepository(Of Disbursement))

        If (pCommonManager Is Nothing) Then
            Throw New ArgumentNullException("pCommonManager")
        End If

        If (pDealerManager Is Nothing) Then
            Throw New ArgumentNullException("pDealerManager")
        End If

        If (pCompanyManager Is Nothing) Then
            Throw New ArgumentNullException("pCompanyManager")
        End If

        If (pCompanyGroupManager Is Nothing) Then
            Throw New ArgumentNullException("pCompanyGroupManager")
        End If

        If (pEquipmentManager Is Nothing) Then
            Throw New ArgumentNullException("pEquipmentManager")
        End If

        If (pCountryManager Is Nothing) Then
            Throw New ArgumentNullException("pCountryManager")
        End If

        If (pCurrencyManager Is Nothing) Then
            Throw New ArgumentNullException("pCurrencyManager")
        End If

        If (pCertificateManager Is Nothing) Then
            Throw New ArgumentNullException("pCurrencyManager")
        End If

        If (pAddressManager Is Nothing) Then
            Throw New ArgumentNullException("pAddressManager")
        End If

        If (pClaimRepository Is Nothing) Then
            Throw New ArgumentNullException("pClaimRepository")
        End If

        If (pDealerRepository Is Nothing) Then
            Throw New ArgumentNullException("pDealerRepository")
        End If

        If (pCertificateItemRepository Is Nothing) Then
            Throw New ArgumentNullException("pCertificateItemRepository")
        End If

        If (pClaimInvoiceRepository Is Nothing) Then
            Throw New ArgumentNullException("pClaimInvoiceRepository")
        End If

        If (pDisbursementRepository Is Nothing) Then
            Throw New ArgumentNullException("pDisbursementRepository")
        End If

        m_CommonManager = pCommonManager
        m_CertificateManager = pCertificateManager
        m_DealerManager = pDealerManager
        m_CompanyManager = pCompanyManager
        m_CompanyGroupManager = pCompanyGroupManager
        m_EquipmentManager = pEquipmentManager
        m_CountryManager = pCountryManager
        m_CurrencyManager = pCurrencyManager
        m_AddressManager = pAddressManager
        m_ClaimRepository = pClaimRepository
        m_DealerRepository = pDealerRepository
        m_CertItemRepository = pCertificateItemRepository
        m_ClaimInvoiceRepository = pClaimInvoiceRepository
        m_DisbursementRepository = pDisbursementRepository

    End Sub

#Region "GOOW SERVICE"

    Public Function CreateClaim(pCic As CertificateItemCoverage,
                                pClaim As Claim,
                                pDealer As Dealer,
                                pCoverageTypeCode As String,
                                pServiceCenterCode As String,
                                pClaimType As Integer,
                                pMake As String,
                                pModel As String,
                                pComments As String,
                                pDamageType As String) As Object Implements IClaimManager.CreateClaim

        Dim oClaim As Claim = pClaim
        Dim oMethodOfRepairCode As String

        Dim serviceClassCode As String = String.Empty
        Dim serviceTypeCode As String = String.Empty

        Dim certItemCoverageDed As CertificateItemCoverageDeductible = Nothing
        Dim oCompany As Company = pDealer.GetCompany(CompanyManager)

        oClaim.CertItemCoverageId = pCic.CertItemCoverageId
        oClaim.OriginalRiskTypeId = pCic.Item.RiskTypeId
        oClaim.CompanyId = pDealer.GetCompany(CompanyManager).CompanyId
        If (pClaimType = 0) Then
            oClaim.MethodOfRepairId = If(pCic.MethodOfRepairId.Equals(Guid.Empty) OrElse pCic.MethodOfRepairId Is Nothing, pCic.Certificate.MethodOfRepairId, pCic.MethodOfRepairId)
            oMethodOfRepairCode = oClaim.MethodOfRepairId.ToCode(CommonManager, ListCodes.MethodOfRepair)

        Else
            oClaim.MethodOfRepairId = MethodofRepairCodes.Replacement.ToGuid(ListCodes.MethodOfRepair, CommonManager)
            oMethodOfRepairCode = MethodofRepairCodes.Replacement

        End If
        oClaim.Deductible = 0D
        oClaim.ReportedDate = DateTime.Today
        oClaim.WhoPaysId = WhoPaysCodes.Assurant.ToGuid(ListCodes.WhoPaysCode, CommonManager)
        oClaim.MgrAuthAmountFlag = YesNoCodes.No
        oClaim.StatusCode = ClaimStatusCodes.Active
        oClaim.ClaimAuthTypeId = GetClaimAuthTypeCode(ClaimAuthorizationTypes.Single_Auth)
        oClaim.CauseOfLossId = pDamageType.ToGuid(ListCodes.CauseOfLoss, CommonManager)

        oClaim.ServiceCenterId = CountryManager.GetServiceCenterByCode(pDealer.GetCompany(CompanyManager).BusinessCountryId, pServiceCenterCode).ServiceCenterId

        ''''check whether the cert item has make and model and if not expect the make and model in the request.
        If (pCic.Item.ManufacturerId.Equals(Guid.Empty) OrElse IsNothing(pCic.Item.ManufacturerId)) Then
            ''''validate the make and model if provided in the request
            If (pMake = String.Empty) Then
                '''''if the cert item has no make and model and also make and model are not provided in the request.
                Throw New CertificateItemNotFoundException(Guid.Empty, pMake)
            Else
                Dim oManufacturerId As Guid = ValidateManufacturerByCompanyGroup(pMake,
                                                                                 pCic.Certificate.CompanyId,
                                                                                 pCic.CertItemCoverageId)

                pCic.Item.ManufacturerId = oManufacturerId
                pCic.Item.Model = pModel

                m_CertItemRepository.Save(pCic.Item)
            End If
        End If
        ''''Create Claimed Equipment"
        oClaim = CreateEquipmentInfo(pCic, oClaim)

        ''''Check Denying and Pending Rules
        CheckForRules(pCic, oClaim)

        '''''Get next claim number based on claim type
        oClaim.ClaimNumber = String.Empty

        GetServiceClassAndTypeByMor(oMethodOfRepairCode, serviceClassCode, serviceTypeCode)

        Dim oPriceListbyMakeAndModel As IEnumerable(Of PriceListDetailRecord) = GetPriceListByMakeAndModel(pCic,
                                                                                                           pServiceCenterCode,
                                                                                                           String.Empty,
                                                                                                           serviceClassCode,
                                                                                                           serviceTypeCode,
                                                                                                           oCompany.Code,
                                                                                                           m_CompanyGroupManager.GetCompanyGroup(oCompany.CompanyGroupId).RiskTypes.Where(Function(r) r.RiskTypeId = pCic.Item.RiskTypeId).First.RiskTypeEnglish,
                                                                                                           pMake,
                                                                                                           pModel,
                                                                                                           pClaim,
                                                                                                           pDealer.DealerCode,
                                                                                                           m_PriceListLowPrice,
                                                                                                           m_GPriceListHighPrice,
                                                                                                           DateTime.Today,
                                                                                                           m_CommonManager)
        Dim oAuthAmount As Decimal = 0

        '''''if make and model is not found price list falls back on the risk type , priority for risk type=4
        If (Not oPriceListbyMakeAndModel Is Nothing) Then
            If (oPriceListbyMakeAndModel.Where(Function(p) p.Make.ToUpper() = pMake.ToUpper() AndAlso p.Model.ToUpper() = pModel.ToUpper()).Count = 0) Then
                Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Price List not configured for Make/Model/SC: " & pMake & "/" & pModel & "/" & pServiceCenterCode)
            End If
            oAuthAmount = oPriceListbyMakeAndModel.Where(Function(p) p.ServiceClassCode = serviceClassCode AndAlso
                                   p.ServiceTypeCode = serviceTypeCode).FirstOrDefault.Price
        End If
        pClaim.AuthorizedAmount = oAuthAmount


        GetNewClaimNumberByClaimType(pClaimType, pDealer, pClaim)

        ' Dim b As Boolean = Me.HasIssues(pCic, pClaim)-----ToDo as GetClaim method is throwing error

        ''''Adjust Method of Repair"
        oClaim = AdjustMethodofRepair(pCic, oClaim)

        ''''Locate Claim Activity"
        oClaim.ClaimActivityId = LocateClaimActivity(oClaim)

        If (oMethodOfRepairCode = MethodofRepairCodes.Recovery) Then
            oClaim.Deductible = 0D
            oClaim.LiabilityLimit = 0D
            oClaim.DeductiblePercent = 0D
            oClaim.DeductibleByPercentId = YesNoCodes.Yes.ToGuid(ListCodes.YesNo, CommonManager)
        Else
            oClaim.LiabilityLimit = pCic.LiabilityLimits
            ''''PrePopulate Deductible
            PrePopulateDeductible(pCic, pDealer, oClaim,, ClaimActions.NewClaim, pClaimType)
        End If
        oClaim.FollowupDate = Date.Now.AddDays(pDealer.GetCompany(CompanyManager).DefaultFollowUpDays)
        ''''Calculate Discount Percent
        oClaim = CalculateDiscountPercent(pCic, oClaim, oMethodOfRepairCode)

        ''''Add Rules and Issues only for Repair claims
        If (pClaimType = ClaimTypeCodes.Repair) Then
            AddRulesAndIssuesToClaim(pDealer, pCic, oClaim)
        End If

        oClaim.Bonus = 0D
        oClaim.BonusTax = 0D

        If (oClaim.StatusCode = ClaimStatusCodes.Active) Then
            AddCommentToCertificate(pCic.Certificate.CertificateId, oClaim, pComments)
        End If
        Return oClaim
    End Function

    Public Sub FulfillClaim(pClaim As Claim,
                            pCert As Certificate,
                            pCompany As Company,
                            pDealerCode As String,
                            pComments As String,
                            pRepairDate As Date,
                            pClaimType As Int64,
                            pSerialNumber As String,
                            pModel As String,
                            pMake As String,
                            pServiceCenterCode As String,
                            pTrackingNumber As String) Implements IClaimManager.FulfillClaim


        Dim oReplacementClaim As Claim
        Dim pClaimNUmber As String
        'Dim pServiceLevelCode As String
        Dim replaceClaimCiC As CertificateItemCoverage
        Dim pMethodOfRepairCode As String

        Dim serviceClassCode As String = String.Empty
        Dim serviceTypeCode As String = String.Empty

        'pServiceLevelCode = pClaim.SERVICE_LEVEL_CODE
        pClaimNUmber = pClaim.ClaimNumber

        Try
            pClaim.RepairDate = pRepairDate

            If (pTrackingNumber <> String.Empty) Then
                pClaim.TrackingNumber = pTrackingNumber
            End If

            If (pClaimType = ClaimTypeCodes.OriginalReplacement) Then
                If (pClaim.ClaimEquipments.Count > 0) Then
                    pClaim.ClaimEquipments().First().SerialNumber = pSerialNumber
                End If

                '''''get the active item from the certificate
                Dim ci As CertificateItem = pCert.Item.Where(Function(i) i.ItemNumber = 1).First()

                If (ci.Model <> pModel OrElse ci.SerialNumber <> pSerialNumber) Then
                    ''''validate MAke if necessary
                    ci.Model = pModel
                    ci.SerialNumber = pSerialNumber

                    m_CertItemRepository.Save(ci)

                End If

            ElseIf (pClaimType = ClaimTypeCodes.Replacement) Then 'Repair To replacement

                pClaim.AuthorizedAmount = 0
                pClaim.StatusCode = ClaimStatusCodes.Closed
                pClaim.ClaimClosedDate = DateTime.Today
                Dim ci As CertificateItem = pCert.Item.Where(Function(i) i.ItemNumber = 1).First()

                If (ci.Model <> pModel OrElse ci.SerialNumber <> pSerialNumber) Then
                    ''''validate MAke if necessary
                    ci.Model = pModel
                    ci.SerialNumber = pSerialNumber

                    m_CertItemRepository.Save(ci)
                End If

                oReplacementClaim = New Claim()
                oReplacementClaim.ClaimId = Guid.NewGuid()
                oReplacementClaim.CompanyId = pClaim.CompanyId
                oReplacementClaim.ServiceCenterId = pClaim.ServiceCenterId
                oReplacementClaim.MethodOfRepairId = MethodofRepairCodes.Replacement.ToGuid(ListCodes.MethodOfRepair, CommonManager)
                oReplacementClaim.ClaimNumber = pClaimNUmber
                oReplacementClaim.MasterClaimNumber = pClaim.ClaimNumber
                oReplacementClaim.StatusCode = ClaimStatusCodes.Active
                oReplacementClaim.ContactName = pClaim.ContactName
                oReplacementClaim.CallerName = pClaim.CallerName
                oReplacementClaim.FollowupDate = pClaim.FollowupDate
                oReplacementClaim.DeductibleByPercentId = pClaim.DeductibleByPercentId
                oReplacementClaim.WhoPaysId = pClaim.WhoPaysId
                oReplacementClaim.ClaimAuthTypeId = pClaim.ClaimAuthTypeId
                oReplacementClaim.MgrAuthAmountFlag = pClaim.MgrAuthAmountFlag
                oReplacementClaim.LossDate = pClaim.LossDate
                oReplacementClaim.LiabilityLimit = pClaim.LiabilityLimit
                oReplacementClaim.ReportedDate = pClaim.ReportedDate
                oReplacementClaim.RepairDate = pRepairDate

                If (pTrackingNumber <> String.Empty) Then
                    oReplacementClaim.TrackingNumber = pTrackingNumber
                End If

                oReplacementClaim.CertItemCoverageId = pClaim.CertItemCoverageId

                replaceClaimCiC = pCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = oReplacementClaim.CertItemCoverageId).FirstOrDefault()
                pMethodOfRepairCode = oReplacementClaim.MethodOfRepairId.ToCode(CommonManager, ListCodes.MethodOfRepair)
                Try
                    AddEquipment(replaceClaimCiC, oReplacementClaim, ClaimEquipmentTypeCodes.Claimed,
                                   pSerialNumber, pSerialNumber, pMake, pModel)
                Catch ex As ManufacturerNotFoundException
                    Throw New ManufacturerNotFoundException(Guid.Empty, pMake)
                End Try

                If (oReplacementClaim.ClaimNumber.ToUpper.EndsWith("R")) Then
                    'as the replacement claim is not saved yet, price list will fail if Repl claim number is passed.
                    oReplacementClaim.ClaimNumber = pClaim.ClaimNumber
                End If


                GetServiceClassAndTypeByMor(pMethodOfRepairCode, serviceClassCode, serviceTypeCode)

                Dim oPriceListbyMakeAndModel As IEnumerable(Of PriceListDetailRecord) = GetPriceListByMakeAndModel(replaceClaimCiC,
                                                                                                           pServiceCenterCode,
                                                                                                           String.Empty,
                                                                                                           serviceClassCode,
                                                                                                           serviceTypeCode,
                                                                                                           pCompany.Code,
                                                                                                           m_CompanyGroupManager.GetCompanyGroup(pCompany.CompanyGroupId).RiskTypes.Where(Function(r) r.RiskTypeId = replaceClaimCiC.Item.RiskTypeId).First.RiskTypeEnglish,
                                                                                                           pMake,
                                                                                                           pModel,
                                                                                                           pClaim,
                                                                                                           pDealerCode,
                                                                                                           m_PriceListLowPrice,
                                                                                                           m_GPriceListHighPrice,
                                                                                                           DateTime.Today,
                                                                                                           m_CommonManager)
                Dim oAuthAmount As Decimal = 0

                '''''if make and model is not found price list falls back on the risk type , priority for risk type=4
                If (Not oPriceListbyMakeAndModel Is Nothing) Then
                    If (oPriceListbyMakeAndModel.Where(Function(p) p.Make.ToUpper() = pMake.ToUpper() AndAlso p.Model.ToUpper() = pModel.ToUpper()).Count = 0) Then
                        Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Price List not configured for Make/Model/SC: " & pMake & "/" & pModel & "/" & pServiceCenterCode)
                    End If
                    oAuthAmount = oPriceListbyMakeAndModel.Where(Function(p) p.ServiceClassCode = serviceClassCode AndAlso
                                   p.ServiceTypeCode = serviceTypeCode).FirstOrDefault.Price
                End If

                oReplacementClaim.AuthorizedAmount = oAuthAmount

                oReplacementClaim.ClaimNumber = pClaimNUmber + "R"

                AddCommentToCertificate(pCert.CertificateId, oReplacementClaim, pComments)
                Save(pClaim)
                Save(oReplacementClaim)
                Exit Sub
            End If

            AddCommentToCertificate(pCert.CertificateId, pClaim, pComments)
            Save(pClaim)

        Catch ex As Exception
        End Try
    End Sub

    Public Sub ChangeServiceCenter(pClaim As Claim,
                            pCert As Certificate,
                            pCompany As Company,
                            pComments As String,
                            pServiceCenterCode As String) Implements IClaimManager.ChangeServiceCenter

        Try
            ''''check whether the service center is different from original
            Dim oNewSvcCenter As ServiceCenter = CountryManager.GetServiceCenterByCode(pCompany.BusinessCountryId, pServiceCenterCode)

            If (Not pClaim.ServiceCenterId.Equals(oNewSvcCenter.ServiceCenterId)) Then
                pClaim.ServiceCenterId = oNewSvcCenter.ServiceCenterId
            End If
            AddCommentToCertificate(pCert.CertificateId, pClaim, pComments)
            Save(pClaim)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub ReturnDamagedDeviceAdvEx(pClaim As Claim, pCert As Certificate,
                            pCompany As Company,
                            pDealer As Dealer,
                            pComments As String,
                            pCoverageTypeCode As String) Implements IClaimManager.ReturnDamagedDeviceAdvEx
        Try

            ClaimRepository.ReturnAdvanceExchange(pCert.CertificateNumber, pDealer.DealerCode, pCoverageTypeCode)

            AddCommentToCertificate(pCert.CertificateId, pClaim, pComments)
            Save(pClaim)
        Catch ex As Exception
            Throw New CoverageNotFoundException(Guid.Empty, pCoverageTypeCode)
        End Try
    End Sub

    Public Sub MaxDaysElapsedAdvEx(pClaim As Claim, pCert As Certificate,
                            pCompany As Company,
                            pDealer As Dealer,
                            pComments As String) Implements IClaimManager.MaxDaysElapsedAdEx
        Try
            AddCommentToCertificate(pCert.CertificateId, pClaim, pComments)
            Save(pClaim)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub PayClaim(pClaim As Claim,
                            pCert As Certificate,
                            pCompany As Company,
                            pServiceCenterCode As String,
                            pInvoiceNumber As String,
                            pAmount As Decimal) Implements IClaimManager.PayClaim

        Dim pcic As CertificateItemCoverage = pCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = pClaim.CertItemCoverageId).FirstOrDefault()

        If Not ValidatePaymentAmount(pClaim.ClaimId, pAmount) Then
            Throw New InvalidPaymentAmountException(pAmount, pClaim.ClaimNumber)
        End If

        'If Not ValidateCauseOfLoss(pClaim.ClaimId, pcic, pCompany.CompanyId) Then
        '    Throw New InvalidCauseOfLossException(pClaim.ClaimNumber, pCert.CertificateNumber, pCompany.CompanyId)
        'End If

        ''''check whether the service center is different from original
        Dim oNewSvcCenter As ServiceCenter = CountryManager.GetServiceCenterByCode(pCompany.BusinessCountryId, pServiceCenterCode)

            Dim oBankInfo As BankInfo = New BankInfo()

            If Not oNewSvcCenter.BankInfoId Is Nothing Then
                oBankInfo = CountryManager.GetBankInfoById(pCompany.BusinessCountryId, oNewSvcCenter.BankInfoId)
            End If

            If (Not pClaim.ServiceCenterId.Equals(oNewSvcCenter.ServiceCenterId)) Then
                pClaim.ServiceCenterId = oNewSvcCenter.ServiceCenterId
            End If

            Dim oClaimInvoice = New ClaimInvoice()
            Dim oDisbursement = New Disbursement()

            PrePopulateDisbursement(oDisbursement, pClaim, oNewSvcCenter, oBankInfo, pCompany, pAmount, pInvoiceNumber)
            PrePopulateClaimInvoice(oClaimInvoice, pClaim, oDisbursement, pAmount, pInvoiceNumber)

            Try
                m_DisbursementRepository.Save(oDisbursement)
                m_ClaimInvoiceRepository.Save(oClaimInvoice)

            pClaim.StatusCode = ClaimStatusCodes.Closed
            pClaim.ReasonClosedId = ReasonClosedCodes.REASON_CLOSED__TO_BE_PAID.ToGuid(ListCodes.ReasonClosedCode, CommonManager)
        Catch ex As Exception

            End Try
            Save(pClaim)

    End Sub

    Private Function GetClaimCaseReserve(pClaimId As Guid) As Decimal
        Dim oCaseReserve As Decimal = 0
        oCaseReserve = m_ClaimRepository.GetClaimCaseReserve(pClaimId)
        Return oCaseReserve

    End Function

    Private Function ValidatePaymentAmount(pClaimId As Guid,
                                           pPaymentAmount As Decimal) As Boolean

        Dim oCaseReserve As Decimal = GetClaimCaseReserve(pClaimId)

        If (pPaymentAmount > oCaseReserve) Then
            Return False
        End If

        Return True
    End Function

    Private Function ValidateCauseOfLoss(pClaim As Claim, pcic As CertificateItemCoverage, pCompanyGroupId As Guid) As Boolean

        Dim causeOfLossId As Guid?
        Dim active As String = String.Empty
        causeOfLossId = pClaim.CauseOfLossId

        ''''get the cause of loss from coverage loss table
        If (causeOfLossId.Equals(Guid.Empty)) Then
            Try
                causeOfLossId = m_CompanyGroupManager.GetCompanyGroup(pCompanyGroupId).CoverageLosses.Where(Function(cl) cl.CoverageTypeId = pcic.CoverageTypeId).First().CauseOfLossId
            Catch ex As Exception
                Return False
            End Try
        End If

        active = m_CompanyGroupManager.GetCompanyGroup(pCompanyGroupId).CoverageLosses.Where(Function(cl) cl.CoverageTypeId = pcic.CoverageTypeId And cl.CauseOfLossId = causeOfLossId).First().Active

        If (active = "N") Then
            Return False
        End If

        Return True
    End Function

    Public Sub PrePopulateClaimInvoice(ByRef oClaimInvoice As ClaimInvoice,
                                       pClaim As Claim,
                                       pDisbursement As Disbursement,
                                       pAmount As Decimal,
                                       pInvoiceNumber As String)

        oClaimInvoice.ClaimInvoiceId = Guid.NewGuid()
        oClaimInvoice.ClaimId = pClaim.ClaimId
        oClaimInvoice.ClaimNumber = pClaim.ClaimNumber
        oClaimInvoice.BatchNumber = "1"
        oClaimInvoice.LABOR_TAX = 0D
        oClaimInvoice.PART_TAX = 0D
        oClaimInvoice.RecordCount = 1
        oClaimInvoice.SOURCE = Nothing
        oClaimInvoice.CompanyId = pClaim.CompanyId
        oClaimInvoice.AUTHORIZATION_NUMBER = pClaim.AuthorizationNumber
        oClaimInvoice.REPAIR_DATE = pClaim.RepairDate
        oClaimInvoice.REPAIR_ESTIMATE = pClaim.RepairEstimate
        oClaimInvoice.DISBURSEMENT_ID = pDisbursement.DISBURSEMENT_ID
        oClaimInvoice.SvcControlNumber = pInvoiceNumber
        oClaimInvoice.AMOUNT = pAmount

    End Sub

    Public Sub PrePopulateDisbursement(ByRef oDisbursement As Disbursement,
                                       pclaim As Claim,
                                       pServiceCenter As ServiceCenter,
                                       pBankInfo As BankInfo,
                                       pCompany As Company,
                                       pAmount As Decimal,
                                       pInvoiceNumber As String)


        oDisbursement.DISBURSEMENT_ID = Guid.NewGuid()
        oDisbursement.CLAIM_ID = pclaim.ClaimId
        oDisbursement.PAYMENT_AMOUNT = pAmount
        oDisbursement.COMPANY_ID = pclaim.CompanyId
        oDisbursement.CLAIM_NUMBER = pclaim.ClaimNumber
        oDisbursement.RECORD_COUNT = 1

        Dim svcAddress As Address = AddressManager.GetAddress(pServiceCenter.ADDRESS_ID)

        oDisbursement.PAYEE = If(Not pServiceCenter Is Nothing, pServiceCenter.CODE, String.Empty)
        oDisbursement.STATUS_DATE = DateTime.Today.ToShortDateString()
        oDisbursement.PAYMENT_DATE = DateTime.Today.ToShortDateString()
        oDisbursement.BANK_ID = pBankInfo.BankId
        oDisbursement.BANK_NAME = pBankInfo.BankName
        oDisbursement.BANK_SORT_CODE = pBankInfo.BankSortCode
        oDisbursement.BANK_SUB_CODE = pBankInfo.BANK_SUB_CODE
        oDisbursement.IBAN_NUMBER = pBankInfo.IbanNumber
        oDisbursement.BANK_LOOKUP_CODE = pBankInfo.BANK_LOOKUP_CODE
        oDisbursement.BRANCH_NAME = pBankInfo.BranchName
        oDisbursement.ADDRESS1 = svcAddress.Address1
        oDisbursement.ADDRESS2 = svcAddress.Address2
        oDisbursement.CITY = svcAddress.City
        oDisbursement.SVC_CONTROL_NUMBER = pInvoiceNumber
        oDisbursement.TRACKING_NUMBER = pclaim.TrackingNumber
        oDisbursement.ACCT_STATUS = AccountingStatusCodes.REQUESTED

        If Not svcAddress.RegionId Is Nothing Then
            oDisbursement.REGION_DESC = CountryManager.GetRegion(svcAddress.CountryId, svcAddress.RegionId).Description
        Else
            oDisbursement.REGION_DESC = String.Empty
        End If
        oDisbursement.COUNTRY = CountryManager.GetCountry(svcAddress.CountryId).Description
        oDisbursement.ZIP = svcAddress.PostalCode

        ''''TODO: oDisbursement.PAYMENT_METHOD
        oDisbursement.MANUFACTURER = m_CompanyGroupManager.GetCompanyGroup(pCompany.CompanyGroupId).Manufacturers.Where(Function(m) m.ManufacturerId = pclaim.ClaimEquipments.First().ManufacturerId).FirstOrDefault.Description
        oDisbursement.MODEL = pclaim.ClaimEquipments.First().Model

    End Sub

    Public Function ComputeTax(pAmount As Decimal,
                               pDealerId As Guid?,
                               pCountryId As Guid?,
                               pCompanyTypeId As Guid?,
                               pTaxTypeId As Guid?,
                               pRegionId As Guid?,
                               pExpectedPremiumIsWpId As Guid?,
                               pProductTaxTypeId As Guid?,
                               pSalesDate As Date) As Decimal Implements IClaimManager.ComputeTax

        Return m_DealerRepository.ComputeTax(pAmount,
                                                 pDealerId,
                                                 pCountryId,
                                                 pCompanyTypeId,
                                                 pTaxTypeId,
                                                 pRegionId,
                                                 pExpectedPremiumIsWpId,
                                                 pProductTaxTypeId,
                                                 pSalesDate)


    End Function

    Public Function GetClaimInfo(pClaim As Claim,
                                pDealer As Dealer, pCert As Certificate) As Object Implements IClaimManager.GetClaimInfo
        Dim oClaim As Claim = pClaim

        Return oClaim
    End Function

    Private Sub AddCommentToCertificate(pCertificateId As Guid,
                                        ByRef pClaim As Claim,
                                        pComments As String)

        Dim translation As String = String.Empty
        Dim oCommentTypeId As Guid = CommentTypeCodes.Other.ToGuid(ListCodes.CommentType, CommonManager)

        If (Not pComments = String.Empty) Then
            Try
                translation = CommonManager.GetLabelTranslations(pComments).FirstOrDefault().Translation
            Catch ex As Exception
                translation = String.Empty
            End Try
        End If

        pClaim.Comments.Add(New Comment() With {
                            .CommentId = Guid.NewGuid(),
                            .CertId = pCertificateId,
                            .CommentTypeId = oCommentTypeId,
                            .ClaimId = pClaim.ClaimId,
                            .Comments = translation,
                            .CommentCreatedDate = DateTime.Now,
                            .CallerName = pClaim.CallerName
                            })
    End Sub

#End Region

    Public Function Save(pClaim As Claim) As Claim Implements IClaimManager.SaveClaim
        m_ClaimRepository.Save(pClaim)
        ''return the claim without querying back to the caller.
        Return pClaim

    End Function

    Function LocateCoverage(pCertificate As Certificate,
                            pDateOfLoss As Date,
                            pCoverageTypeCode As String,
                            pItemNumber As Short?) As CertificateItemCoverage Implements IClaimManager.LocateCoverage

        Dim ci As CertificateItem = (From oci As CertificateItem In pCertificate.Item.
                                          Where(Function(pci) pci.ItemNumber = 1 AndAlso pDateOfLoss >= pci.EffectiveDate AndAlso
                                                 pDateOfLoss <= pci.ExpirationDate)).FirstOrDefault()

        If (Not ci Is Nothing) Then

            Dim count As Integer = (From cic As CertificateItemCoverage In
                                                 ci.ItemCoverages.Where(Function(c) pDateOfLoss >= c.BeginDate AndAlso
                                                 pDateOfLoss <= c.EndDate AndAlso
                                                 pCoverageTypeCode = c.CoverageTypeId.ToCode(CommonManager, ListCodes.CoverageType))).Count


            If (count = 0) Then
                Throw New CoverageNotFoundException(Guid.Empty, pCoverageTypeCode)
            ElseIf (count > 1) Then
                Throw New MultipleCoverageFoundException(Guid.Empty, pCoverageTypeCode)
            Else
                Return ci.ItemCoverages.Where(Function(c) pDateOfLoss >= c.BeginDate AndAlso
                                                     pDateOfLoss <= c.EndDate AndAlso
                                                     pCoverageTypeCode = c.CoverageTypeId.ToCode(CommonManager, ListCodes.CoverageType)).FirstOrDefault
            End If
        Else
            Throw New CoverageNotFoundException(Guid.Empty, String.Empty)
        End If
    End Function

    Function GetClaim(pClaimNUmber As String,
                      pCompanyId As Guid) As Claim Implements IClaimManager.GetClaim

        If m_ClaimRepository.Get(Function(c) c.ClaimNumber = pClaimNUmber AndAlso c.CompanyId = pCompanyId, Nothing, "claimequipments,comments,entityissues,claimstatuses").Count > 0 Then
            Return m_ClaimRepository.Get(Function(c) c.ClaimNumber = pClaimNUmber AndAlso c.CompanyId = pCompanyId).FirstOrDefault()
        Else
            Throw New ClaimNotFoundException(pCompanyId, pClaimNUmber)
        End If

    End Function

    Private Function IsCoverageChangeValid(pCertificate As Certificate,
                                           CoverageTypeCode As String,
                                           pDateOfLoss As Date) As Boolean Implements IClaimManager.IsCoverageChangeValid

        Dim count As Integer = (From ocic As CertificateItemCoverage In
                                   pCertificate.ItemCoverages.Where(Function(cic) cic.CoverageTypeId = CoverageTypeCode.ToGuid(ListCodes.CoverageType, CommonManager) AndAlso
                                                                pDateOfLoss > cic.BeginDate AndAlso pDateOfLoss <= cic.EndDate)).Count

        If count > 0 Then
            Return True
        Else
            Return False
        End If


    End Function

    Public Sub UpdateRepairableClaim(pRepairClaim As Claim,
                                     pCert As Certificate,
                                     pCompany As Company,
                                     pCoverageTypeCode As String,
                                     pServiceCenterCode As String,
                                     pServiceLevelCode As String,
                                     pHasCoverageChanged As Boolean,
                                     pHasSvcCenterChanged As Boolean,
                                     pIsServiceWarrantyClaim As Boolean,
                                     pMake As String,
                                     pModel As String,
                                     pSerialNumberReplacedDevice As String,
                                     pAuthorizedAmount As Decimal?,
                                     pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields) Implements IClaimManager.UpdateRepairableClaim


        Dim methodOfRepairCode As String
        Dim oReplacementClaim As Claim
        Dim repClaimNUmber As String
        Dim repairClaimCi As CertificateItem
        Dim serviceClassCode As String = String.Empty
        Dim serviceTypeCode As String = String.Empty

        'If Diagnostic is Repaired (All valid update Actions of this Operation) or Denied, No Replacement Claim is expected
        Try
            If pRepairClaim.ClaimNumber.ToUpper.EndsWith("S") Then
                repClaimNUmber = pRepairClaim.ClaimNumber.Substring(0, pRepairClaim.ClaimNumber.Length - 1)
                repClaimNUmber &= "R"
            Else
                repClaimNUmber = pRepairClaim.ClaimNumber
                repClaimNUmber &= "R"
            End If

            oReplacementClaim = GetClaim(repClaimNUmber, pRepairClaim.CompanyId)

            'Expected Behavior as per DEF-27210
            'When diagnostic Is "Repaired" Or "Denied" Or "Replaced Refurbished" And there Is an existing REPLACEMENT CLAIM; 
            '   a.No update should be done to the Repair Claim
            '   b.If Existing Replacement Claim Is Active / Not paid
            '       System should mark both REPAIR And REPLACEMENT claims as pending And return error message "Active Replacement Claim found"
            '   c.If existing REPLACEMENT CLAIM Is already fully Paid/closed; 
            'System should mark REPAIR claim as pending And return error message "Closed Replacement Claim found"

            'If Both Repair and Replacment Claims are found Pending,that means its a Repocessing Case so Deny the Replacement Claim
            If oReplacementClaim.StatusCode = ClaimStatusCodes.Pending AndAlso pRepairClaim.StatusCode = ClaimStatusCodes.Pending Then
                'Deny Replacment Claim and Perform the Next Update steps with the Repaired Claim
                oReplacementClaim.AuthorizedAmount = 0D
                oReplacementClaim.Deductible = 0D
                oReplacementClaim.StatusCode = ClaimStatusCodes.Denied
                oReplacementClaim.ClaimClosedDate = DateTime.Today

                ''Set the Appropriate Denial Reason
                'Select Case pClaimBasicUpdateFields.ExtendedStatuses.First.Code
                '    Case UpdateActionTypeCodes.NoProblemFound
                '        oReplacementClaim.DeniedReasonId = DeniedReasonCodes.NoProblemFound.ToGuid(ListCodes.DeniedReasonCode, Me.CommonManager)
                '    Case UpdateActionTypeCodes.UnderMFW
                '        oReplacementClaim.DeniedReasonId = DeniedReasonCodes.UnderMFGWarranty.ToGuid(ListCodes.DeniedReasonCode, Me.CommonManager)
                'End Select

                Save(oReplacementClaim)

            ElseIf oReplacementClaim.StatusCode = ClaimStatusCodes.Active Then
                'Case b. from above behaviour
                oReplacementClaim.StatusCode = ClaimStatusCodes.Pending
                Save(oReplacementClaim)
                pRepairClaim.StatusCode = ClaimStatusCodes.Pending
                Save(pRepairClaim)
                'No Updates to the Repair Claim and send the User a Message Back
                Throw New ReplacementClaimFoundException(pRepairClaim.CompanyId, pRepairClaim.ClaimNumber,
                                                         "Active Replacement Claim Found")

            ElseIf oReplacementClaim.StatusCode = ClaimStatusCodes.Closed Then
                'Case c. from above behaviour
                pRepairClaim.StatusCode = ClaimStatusCodes.Pending
                Save(pRepairClaim)
                Throw New ReplacementClaimFoundException(pRepairClaim.CompanyId, pRepairClaim.ClaimNumber,
                                                        "Closed Replacement Claim Found")
            End If

        Catch cnf As ClaimNotFoundException
            'Do Nothing and Perform the Next Update steps with the Repaired Claim
        End Try

        'Update Repair Claim as per REQ section # U.2
        UpdateClaimBasicFields(pRepairClaim, pCert, pCompany,
                               pHasCoverageChanged, pCoverageTypeCode,
                               pHasSvcCenterChanged, pServiceCenterCode, pClaimBasicUpdateFields)

        'Get the CertItemCoverage at this state to include and Coverage change if applicable
        Dim claimCiC As CertificateItemCoverage = pCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = pRepairClaim.CertItemCoverageId).FirstOrDefault()

        repairClaimCi = claimCiC.Item

        'repairClaimCi = (From oci As CertificateItem In
        '                                             pCert.Item.Where(Function(pci) pci.ItemNumber = 1 AndAlso
        '                                             pRepairClaim.LossDate > pci.EffectiveDate AndAlso
        '                                             pRepairClaim.LossDate <= pci.ExpirationDate))


        ' Serial Number, Make and Model of the Replaced Device should be stored at the Claim Level and not replaced to the Item level
        Try
            AddEquipment(claimCiC, pRepairClaim, ClaimEquipmentTypeCodes.Claimed, pSerialNumberReplacedDevice, pSerialNumberReplacedDevice, pMake, pModel)
        Catch ex As ManufacturerNotFoundException
            Throw New ManufacturerNotFoundException(Guid.Empty, pMake)
        End Try


        'Update the Claim Auth Amount and Deductibles
        If pIsServiceWarrantyClaim Then
            pRepairClaim.Deductible = 0D
            pRepairClaim.AuthorizedAmount = 0D
        Else

            Dim oDealer As Dealer = claimCiC.Certificate.GetDealer(DealerManager)

            methodOfRepairCode = pRepairClaim.MethodOfRepairId.ToCode(CommonManager, ListCodes.MethodOfRepair)

            'b.	If any amount is provided by the Claim Portal, System shall assigned the provided amount to the claim authorization amount otherwise
            If pAuthorizedAmount > 0 Then
                pRepairClaim.AuthorizedAmount = pAuthorizedAmount
            Else
                'For Repairable Claims, if method of repair is 'Replacement' then Auth amount is based on existing behaviour
                If methodOfRepairCode = MethodofRepairCodes.Replacement Then
                    pRepairClaim.AuthorizedAmount = GetPriceListByMethodOfRepair(claimCiC, pServiceCenterCode, pServiceLevelCode, methodOfRepairCode, pRepairClaim)

                Else
                    Dim pCurrencyConversionDate As Date
                    If Not pRepairClaim.RepairDate Is Nothing Then
                        pCurrencyConversionDate = pRepairClaim.RepairDate
                    Else
                        pCurrencyConversionDate = DateTime.Today
                    End If
                    Dim effectiveDate As Date
                    If Not pRepairClaim.RepairDate Is Nothing Then
                        effectiveDate = pRepairClaim.RepairDate
                    Else
                        effectiveDate = Nothing
                    End If

                    Dim bSvcLevelValid As Boolean = IsServiceLevelValid(pRepairClaim.ServiceCenterId, repairClaimCi.RiskTypeId,
                                                                           effectiveDate, repairClaimCi.Certificate.SalesPrice, pServiceLevelCode)
                    If Not bSvcLevelValid Then
                        Throw New InvalidServiceLevelException(pServiceLevelCode, "Invalid Service Level Code")
                    End If

                    GetServiceClassAndTypeByMor(methodOfRepairCode, serviceClassCode, serviceTypeCode)

                    pRepairClaim.AuthorizedAmount = GetAuthAmountByLaborAndParts(claimCiC,
                                                                                    pCompany.Code,
                                                                                    m_DealerManager.GetDealerById(claimCiC.Certificate.DealerId).DealerCode,
                                                                                    pServiceCenterCode,
                                                                                    pServiceLevelCode,
                                                                                    serviceClassCode,
                                                                                    serviceTypeCode,
                                                                                    pRepairClaim,
                                                                                    DateTime.Today,
                                                                                    m_CompanyGroupManager.GetCompanyGroup(pCompany.CompanyGroupId).RiskTypes.Where(Function(r) r.RiskTypeId = pRepairClaim.OriginalRiskTypeId).First.RiskTypeEnglish,
                                                                                    pMake,
                                                                                    pModel,
                                                                                    0,
                                                                                    pCert.SalesPrice,
                                                                                    m_CommonManager)


                End If
            End If

            PrePopulateDeductible(claimCiC, oDealer, pRepairClaim,, ClaimActions.Update,,
                                  pMake, pModel, pHasCoverageChanged)

        End If
        If (pRepairClaim.AuthorizedAmount = 0) Then
            Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode)
        End If

        If (pCoverageTypeCode = "A" And pRepairClaim.Deductible = 0) Then
            Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Price List not configured for Make/Model/SC: " & pMake & "/" & pModel & "/" & pServiceCenterCode)
        End If

        'Save the Repair Claim
        Save(pRepairClaim)

    End Sub

    Private Sub GetServiceClassAndTypeByMor(pMethodofRepairCode As String,
                                            ByRef pServiceClassCode As String,
                                            ByRef pServiceTypeCode As String)
        Select Case pMethodofRepairCode
            Case MethodofRepairCodes.CarryIn
                pServiceClassCode = ServiceClassCodes.Repair
                pServiceTypeCode = ServiceTypeCodes.CarryInPrice
            Case MethodofRepairCodes.SendIn
                pServiceClassCode = ServiceClassCodes.Repair
                pServiceTypeCode = ServiceTypeCodes.SendInPrice
            Case MethodofRepairCodes.Replacement
                pServiceClassCode = ServiceClassCodes.Replacement
                pServiceTypeCode = ServiceTypeCodes.ReplacementPrice
        End Select

    End Sub

    Private Sub UpdateClaimBasicFields(ByRef pClaim As Claim,
                                       pCert As Certificate,
                                       pCompany As Company,
                                       pHasCoverageChanged As Boolean,
                                       pCoverageTypeCode As String,
                                       pHasSvcCenterChanged As Boolean,
                                       pServiceCenterCode As String,
                                       pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields
                                       ) Implements IClaimManager.UpdateClaimBasicFields

        Dim oCompGroup As CompanyGroup
        Dim oNewSvcCenter As ServiceCenter
        Dim newCertItemCovg As CertificateItemCoverage

        If Not pClaimBasicUpdateFields.RepairDate Is Nothing Then
            pClaim.RepairDate = pClaimBasicUpdateFields.RepairDate
        End If

        If Not pClaimBasicUpdateFields.Specialnstructions Is Nothing AndAlso
            pClaimBasicUpdateFields.Specialnstructions <> String.Empty Then
            pClaim.SpecialInstruction = pClaimBasicUpdateFields.Specialnstructions
        End If

        If Not pClaimBasicUpdateFields.AuthNumber Is Nothing AndAlso
            pClaimBasicUpdateFields.AuthNumber <> String.Empty Then
            pClaim.AuthorizationNumber = pClaimBasicUpdateFields.AuthNumber
        End If

        If Not pClaimBasicUpdateFields.TechnicalReport Is Nothing AndAlso
            pClaimBasicUpdateFields.TechnicalReport <> String.Empty Then
            pClaim.TechnicalReport = pClaimBasicUpdateFields.TechnicalReport
        End If

        If Not pClaimBasicUpdateFields.ProblemDescription Is Nothing AndAlso
            pClaimBasicUpdateFields.ProblemDescription <> String.Empty Then
            pClaim.ProblemDescription = pClaimBasicUpdateFields.ProblemDescription
        End If


        'Update the Extended Statuses for the Claim
        If Not pClaimBasicUpdateFields.ExtendedStatuses Is Nothing Then
            If (pClaimBasicUpdateFields.ExtendedStatuses.Count > 0) Then
                For Each es As IClaimManager.ExtendedStatus In pClaimBasicUpdateFields.ExtendedStatuses
                    InsertClaimExtendedStatus(pCert.GetDealer(DealerManager), es.Code, es.StatusDate, pClaim)
                Next
            End If
        End If

        'Change the Coverage Type if there is a Valid change! Update the Coverage on the Claim with the new CType
        If pHasCoverageChanged = True Then

            Dim claimCiCId = pClaim.CertItemCoverageId
            Dim claimLossDate = pClaim.LossDate

            newCertItemCovg = pCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId <> claimCiCId AndAlso cic.CoverageTypeId = pCoverageTypeCode.ToGuid(ListCodes.CoverageType, CommonManager) And
                                                            claimLossDate > cic.BeginDate And claimLossDate <= cic.EndDate).First()

            'The Deductible Amount calculation would use it with the new cert item coverage Id
            pClaim.CertItemCoverageId = newCertItemCovg.CertItemCoverageId

            'Also change the Default 'cause of loss' defined for the new given coverage type at the Comp Group level
            oCompGroup = CompanyGroupManager.GetCompanyGroup(pCompany.CompanyGroupId)
            Dim newCauseOfLossId As Guid = Guid.Empty
            If oCompGroup.CoverageLosses.Count > 0 Then
                newCauseOfLossId = oCompGroup.CoverageLosses.Where(Function(covgL) covgL.CoverageTypeId = newCertItemCovg.CoverageTypeId AndAlso covgL.DefaultFlag = "Y").FirstOrDefault().CauseOfLossId
            End If
            pClaim.CauseOfLossId = newCauseOfLossId

        End If

        'Change the Service Center if there is a Valid change!
        If pHasSvcCenterChanged Then
            oNewSvcCenter = CountryManager.GetServiceCenterByCode(pCompany.BusinessCountryId, pServiceCenterCode)
            pClaim.ServiceCenterId = oNewSvcCenter.ServiceCenterId
        End If


    End Sub

    Public Sub UpdateClaimReplacedWithRefubished(pRepairClaim As Claim,
                                       pCert As Certificate,
                                       pCompany As Company,
                                       pCoverageTypeCode As String,
                                       pServiceCenterCode As String,
                                       pServiceLevelCode As String,
                                       pHasCoverageChanged As Boolean,
                                       pHasSvcCenterChanged As Boolean,
                                       pIsServiceWarrantyClaim As Boolean,
                                       pAuthorizedAmount As Decimal?,
                                       pSimCardAmount As Decimal?,
                                       pSerialNumberReplacedDevice As String,
                                       pModel As String,
                                       pManufacturer As String,
                                       pUpdateAction As String,
                                       pCondition As IClaimManager.DeviceConditionEnum,
                                       pLossType As String,
                                       pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields) Implements IClaimManager.UpdateClaimReplacedWithRefubished

        Dim oDealer As Dealer
        Dim methodOfRepairCode As String
        Dim oReplacementClaim As Claim
        Dim repClaimNUmber As String

        'Dim replaceClaimCi As CertificateItem
        Dim replaceClaimCiC As CertificateItemCoverage

        'Initialize the Replacement Claim Number either for looking up existing Claim or creating a New Claim
        If pRepairClaim.ClaimNumber.ToUpper.EndsWith("S") Then
            repClaimNUmber = pRepairClaim.ClaimNumber.Substring(0, pRepairClaim.ClaimNumber.Length - 1)
            repClaimNUmber &= "R"
        Else
            repClaimNUmber = pRepairClaim.ClaimNumber
            repClaimNUmber &= "R"
        End If

        oDealer = pCert.GetDealer(DealerManager)

        'Expected Behavior as per DEF-27210
        'When diagnostic Is "Repaired" Or "Denied" Or "Replaced Refurbished" And there Is an existing REPLACEMENT CLAIM; 
        '   a.No update should be done to the Repair Claim
        '   b.If Existing Replacement Claim Is Active / Not paid
        '       System should mark both REPAIR And REPLACEMENT claims as pending And return error message "Active Replacement Claim found"
        '   c.If existing REPLACEMENT CLAIM Is already fully Paid/closed; 
        'System should mark REPAIR claim as pending And return error message "Closed Replacement Claim found"

        Try
            oReplacementClaim = GetClaim(repClaimNUmber, pRepairClaim.CompanyId)

            If (Not oReplacementClaim Is Nothing) Then
                ''''a.	There is already a replacement claim created for the same repair claim
                Throw New ReplacementClaimFoundException(pRepairClaim.CompanyId, pRepairClaim.ClaimNumber,
                                                      "Replacement Claim Found")
            End If

        Catch cnf As ClaimNotFoundException
            'do not update coverage type and svc center for repair claim as this will be closed.
            UpdateRepairClaimWhenIrreparable(pRepairClaim, pCert, oDealer, pCompany, pCoverageTypeCode,
                                         pServiceCenterCode, pServiceLevelCode, False,
                                         False, pIsServiceWarrantyClaim, pCondition,
                                         pClaimBasicUpdateFields)

            '''''''Creat a New Replacement Claim

            'Create a New Replacement Claim automatically without waiting for customer confirmation
            oReplacementClaim = New Claim()
            oReplacementClaim.ClaimId = Guid.NewGuid()
            oReplacementClaim.CompanyId = pRepairClaim.CompanyId
            oReplacementClaim.ServiceCenterId = pRepairClaim.ServiceCenterId
            oReplacementClaim.MethodOfRepairId = MethodofRepairCodes.Replacement.ToGuid(ListCodes.MethodOfRepair, CommonManager)
            oReplacementClaim.ClaimNumber = repClaimNUmber
            oReplacementClaim.MasterClaimNumber = pRepairClaim.ClaimNumber
            oReplacementClaim.StatusCode = ClaimStatusCodes.Active
            oReplacementClaim.ContactName = pRepairClaim.ContactName
            oReplacementClaim.CallerName = pRepairClaim.CallerName
            oReplacementClaim.FollowupDate = pRepairClaim.FollowupDate
            oReplacementClaim.DeductibleByPercentId = pRepairClaim.DeductibleByPercentId
            oReplacementClaim.WhoPaysId = pRepairClaim.WhoPaysId
            oReplacementClaim.ClaimAuthTypeId = pRepairClaim.ClaimAuthTypeId
            oReplacementClaim.MgrAuthAmountFlag = pRepairClaim.MgrAuthAmountFlag
            oReplacementClaim.LossDate = pRepairClaim.LossDate
            oReplacementClaim.LiabilityLimit = pRepairClaim.LiabilityLimit
            oReplacementClaim.ReportedDate = pRepairClaim.ReportedDate

            'TODO : set the cert item coverage id
            oReplacementClaim.CertItemCoverageId = pRepairClaim.CertItemCoverageId

            replaceClaimCiC = pCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = oReplacementClaim.CertItemCoverageId).FirstOrDefault()

            ''check for max replacements limit
            If IsMaxReplacementExceeded(replaceClaimCiC, pRepairClaim, m_DealerManager.GetContract(oDealer.DealerCode, pCert.WarrantySalesDate)) Then
                ''b.	Replacement claim limit has been reached out already (customer already has 2 replacement claims for example);
                Throw New ReplacementClaimFoundException(pRepairClaim.CompanyId, pRepairClaim.ClaimNumber,
                                                      "Max Replacement Claims Reached")
            End If

            ' Serial Number, Make and Model of the Replaced Device should be stored at the Claim Level and not replaced to the Item level
            Try
                AddEquipment(replaceClaimCiC, oReplacementClaim, ClaimEquipmentTypeCodes.Claimed,
                               pSerialNumberReplacedDevice, pSerialNumberReplacedDevice, pManufacturer, pModel,
                                CType(pCondition, IClaimManager.DeviceConditionEnum).ToString())
            Catch ex As ManufacturerNotFoundException
                Throw New ManufacturerNotFoundException(Guid.Empty, pManufacturer)
            End Try


            'Replacement Claim Basic Fields should get Updated, except coverage type
            UpdateClaimBasicFields(oReplacementClaim, pCert, pCompany,
                               pHasCoverageChanged, pCoverageTypeCode,
                               pHasSvcCenterChanged, pServiceCenterCode, pClaimBasicUpdateFields)

            '''''update the coverage if the coverage has changed
            If (pHasCoverageChanged) Then
                replaceClaimCiC = pCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = oReplacementClaim.CertItemCoverageId).FirstOrDefault()
            End If
            '''''if auth amount in the request is >0 then auth amount is set to the incoming auth amount irrespective of device type
            If pAuthorizedAmount > 0 Then
                oReplacementClaim.AuthorizedAmount = pAuthorizedAmount
            Else
                If pCondition = IClaimManager.DeviceConditionEnum.Refurbished Then
                    'For a Refurb Device the Auth amount is mandatory and has to be greater than zero
                    oReplacementClaim.AuthorizedAmount = pAuthorizedAmount
                Else
                    methodOfRepairCode = oReplacementClaim.MethodOfRepairId.ToCode(CommonManager, ListCodes.MethodOfRepair)

                    If (oReplacementClaim.ClaimNumber.ToUpper.EndsWith("R")) Then
                        'as the replacement claim is not saved yet, price list will fial if Repl claim number is passed.
                        oReplacementClaim.ClaimNumber = pRepairClaim.ClaimNumber
                    End If

                    'For Replacement Claims, the Auth Amount is based on the Method of Repair and make/model passed in the request
                    Dim oPriceListbyMakeAndModel As IEnumerable(Of PriceListDetailRecord) = GetPriceListByMakeAndModel(replaceClaimCiC,
                                                                                                           pServiceCenterCode,
                                                                                                           pServiceLevelCode,
                                                                                                           ServiceClassCodes.Replacement,
                                                                                                           ServiceTypeCodes.ReplacementPrice,
                                                                                                           pCompany.Code,
                                                                                                           m_CompanyGroupManager.GetCompanyGroup(pCompany.CompanyGroupId).RiskTypes.Where(Function(r) r.RiskTypeId = replaceClaimCiC.Item.RiskTypeId).First.RiskTypeEnglish,
                                                                                                           pManufacturer,
                                                                                                           pModel,
                                                                                                           oReplacementClaim,
                                                                                                           oDealer.DealerCode,
                                                                                                           m_PriceListLowPrice,
                                                                                                           m_PriceListHighPrice,
                                                                                                           DateTime.Today,
                                                                                                           m_CommonManager)

                    oReplacementClaim.ClaimNumber = repClaimNUmber
                    'Me.GetPriceListByMakeAndModel(replaceClaimCiC, pServiceCenterCode, pServiceLevelCode, methodOfRepairCode, oReplacementClaim)
                    Dim oAuthAmount As Decimal = 0
                    If (Not oPriceListbyMakeAndModel Is Nothing) Then
                        '''''if make and model is not found price list falls back on the risk type , priority for risk type=4
                        If (oPriceListbyMakeAndModel.Where(Function(p) p.Make.ToUpper() = pManufacturer.ToUpper() AndAlso p.Model.ToUpper() = pModel.ToUpper()).Count = 0) Then
                            If (oPriceListbyMakeAndModel.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Replacement AndAlso p.ServiceTypeCode = ServiceTypeCodes.ReplacementPrice).Count = 0) Then
                                Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Price List not configured for Make/Model/SC: " & pManufacturer & "/" & pModel & "/" & pServiceCenterCode)
                            End If
                        End If
                        oAuthAmount = oPriceListbyMakeAndModel.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Replacement AndAlso
                                   p.ServiceTypeCode = ServiceTypeCodes.ReplacementPrice).FirstOrDefault.Price
                    End If

                    'Me.GetPriceListByMethodOfRepair(replaceClaimCiC, pServiceCenterCode, pServiceLevelCode, methodOfRepairCode, pRepairClaim)
                    oReplacementClaim.AuthorizedAmount = oAuthAmount
                End If
            End If

            If (oReplacementClaim.AuthorizedAmount = 0) Then
                Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Price List not configured for Make/Model/SC: " & pManufacturer & "/" & pModel & "/" & pServiceCenterCode)
            End If

            'Add sim card amount to the authorized amount for Irreparable and Replace-Refurbished
            If (pUpdateAction = 7 OrElse pUpdateAction = 8) Then
                oReplacementClaim.AuthorizedAmount = oReplacementClaim.AuthorizedAmount + pSimCardAmount
            End If

            If (oReplacementClaim.ClaimNumber.ToUpper.EndsWith("R")) Then
                'as the replacement claim is not saved yet, price list will fial if Repl claim number is passed.
                oReplacementClaim.ClaimNumber = pRepairClaim.ClaimNumber
            End If

            'Dedutible for Replacement Claim depends on the Loss Type - Total/Partial
            Dim certItemMake As String = m_CompanyGroupManager.GetCompanyGroup(pCompany.CompanyGroupId).Manufacturers.Where(Function(m) m.ManufacturerId = replaceClaimCiC.Item.ManufacturerId).FirstOrDefault.Description

            PrePopulateDeductible(replaceClaimCiC, oDealer, oReplacementClaim, pLossType, ClaimActions.Update,,
                                  certItemMake, replaceClaimCiC.Item.Model, pHasCoverageChanged)

            oReplacementClaim.ClaimNumber = repClaimNUmber

            If (pCoverageTypeCode = "A" And oReplacementClaim.Deductible = 0) Then
                Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Price List not configured for Make/Model/SC: " & certItemMake & "/" & replaceClaimCiC.Item.Model & "/" & pServiceCenterCode)
            End If

            'save the repair claim
            Save(pRepairClaim)

            'Save the New Replacement Claim 
            Save(oReplacementClaim)

        End Try
    End Sub

    Private Sub UpdateRepairClaimWhenIrreparable(pRepairClaim As Claim,
                                                pCert As Certificate,
                                                oDealer As Dealer,
                                                pCompany As Company,
                                                pCoverageTypeCode As String,
                                                pServiceCenterCode As String,
                                                pServiceLevelCode As String,
                                                pHasCoverageChanged As Boolean,
                                                pHasSvcCenterChanged As Boolean,
                                                pIsServiceWarrantyClaim As Boolean,
                                                pCondition As IClaimManager.DeviceConditionEnum,
                                                pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields)

        Dim repairClaimCi As CertificateItem
        Dim repairClaimCiC As CertificateItemCoverage

        'TODO : Update the Claim Basic Fields for repair claim, do not change coverage type, svc center
        UpdateClaimBasicFields(pRepairClaim, pCert, pCompany,
                               pHasCoverageChanged, pCoverageTypeCode,
                               pHasSvcCenterChanged, pServiceCenterCode, pClaimBasicUpdateFields)

        'Update Repair Claim Fields
        Dim pCurrencyConversionDate As Date
        If Not pRepairClaim.RepairDate Is Nothing Then
            pCurrencyConversionDate = pRepairClaim.RepairDate
        Else
            pCurrencyConversionDate = DateTime.Today
        End If

        'Update the Claim Auth Amount and Deductibles
        If pIsServiceWarrantyClaim Then
            pRepairClaim.Deductible = 0D
            pRepairClaim.AuthorizedAmount = 0D
        Else
            repairClaimCiC = pCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = pRepairClaim.CertItemCoverageId).FirstOrDefault()
            repairClaimCi = repairClaimCiC.Item
            'repairClaimCi = (From oci As CertificateItem In
            '                                         pCert.Item.Where(Function(pci) pci.ItemNumber = 1 AndAlso
            '                                         pRepairClaim.LossDate > pci.EffectiveDate AndAlso
            '                                         pRepairClaim.LossDate <= pci.ExpirationDate)).FirstOrDefault()
            pRepairClaim.Deductible = 0D

            Dim effectiveDate As Date
            If Not pRepairClaim.RepairDate Is Nothing Then
                effectiveDate = pRepairClaim.RepairDate
            Else
                effectiveDate = Nothing
            End If

            Dim bSvcLevelValid As Boolean = IsServiceLevelValid(pRepairClaim.ServiceCenterId, repairClaimCi.RiskTypeId,
                                                                   effectiveDate, repairClaimCi.Certificate.SalesPrice, pServiceLevelCode)
            If Not bSvcLevelValid Then
                Throw New InvalidServiceLevelException(pServiceLevelCode, "Invalid Service Level Code")
            End If
            'methodOfRepairCode = pRepairClaim.MethodOfRepairId.ToCode(Me.CommonManager, ListCodes.MethodOfRepair)
            ''For Repairable Claims, the Auth Amount is based on the Method of Repair
            'Dim oAuthAmount As Decimal = Me.GetPriceListByMethodOfRepair(repairClaimCiC, pServiceCenterCode, pServiceLevelCode, methodOfRepairCode, pRepairClaim)
            'pRepairClaim.AuthorizedAmount = oAuthAmount

            'If visit fee is configured in the price list, assign the visit fee otherwise ZERO if no amount is configured
            pRepairClaim.AuthorizedAmount = GetPriceListByServiceType(repairClaimCiC, pServiceCenterCode, pServiceLevelCode, ServiceClassCodes.Repair, ServiceTypeCodes.EstimatePrice, pRepairClaim, pCurrencyConversionDate)
            If pRepairClaim.AuthorizedAmount = 0 Then
                pRepairClaim.StatusCode = ClaimStatusCodes.Closed
                pRepairClaim.ClaimClosedDate = DateTime.Today
            End If
        End If

        'Save the Repair Claim Updates
        ' Me.Save(pRepairClaim)
    End Sub

    'Public Sub UpdateIrRepairableClaim(ByVal pRepairClaim As Claim,
    '                                   ByVal pCert As Certificate,
    '                                   ByVal pCompany As Company,
    '                                   ByVal pCoverageTypeCode As String,
    '                                   ByVal pServiceCenterCode As String,
    '                                   ByVal pServiceLevelCode As String,
    '                                   ByVal pHasCoverageChanged As Boolean,
    '                                   ByVal pHasSvcCenterChanged As Boolean,
    '                                   ByVal pIsServiceWarrantyClaim As Boolean,
    '                                   ByVal pAuthorizedAmount As Nullable(Of Decimal),
    '                                   ByVal pSimCardAmount As Nullable(Of Decimal),
    '                                   ByVal pSerialNumberReplacedDevice As String,
    '                                   ByVal pModel As String,
    '                                   ByVal pManufacturer As String,
    '                                   ByVal pCondition As IClaimManager.DeviceConditionEnum,
    '                                   ByVal pLossType As String,
    '                                   ByVal pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields) Implements IClaimManager.UpdateIrRepairableClaim

    '    Dim oDealer As Dealer
    '    Dim methodOfRepairCode As String
    '    Dim oReplacementClaim As Claim
    '    Dim repClaimNUmber As String

    '    'Dim replaceClaimCi As CertificateItem
    '    Dim replaceClaimCiC As CertificateItemCoverage


    '    'Initialize the Replacement Claim Number either for looking up existing Claim or creating a New Claim
    '    If pRepairClaim.ClaimNumber.ToUpper.EndsWith("S") Then
    '        repClaimNUmber = pRepairClaim.ClaimNumber.Substring(0, pRepairClaim.ClaimNumber.Length - 1)
    '        repClaimNUmber &= "R"
    '    Else
    '        repClaimNUmber = pRepairClaim.ClaimNumber
    '        repClaimNUmber &= "R"
    '    End If

    '    oDealer = pCert.GetDealer(Me.DealerManager)


    '    'If Diagnostic is Irreparable or Not Repaired then a Replacement Claim is Expected
    '    Try
    '        'Scenario 1 : When Replacement was created by Service Portal before the Claim Update comes from the Svc Center File

    '        oReplacementClaim = Me.GetClaim(repClaimNUmber, pRepairClaim.CompanyId)

    '        'If Both Repair and Replacment Claims are found Pending,that means its a Repocessing Case so Deny the Replacement Claim
    '        If oReplacementClaim.StatusCode = ClaimStatusCodes.Pending AndAlso pRepairClaim.StatusCode = ClaimStatusCodes.Pending Then
    '            'No Special handling for Reprocessing in the case of Update Irreparable
    '            'As per Defect # 27210, System should update both fields as Section U.3
    '        End If

    '        'Update the RepairClaimWhen its Irreparable
    '        UpdateRepairClaimWhenIrreparable(pRepairClaim, pCert, oDealer, pCompany, pCoverageTypeCode,
    '                                     pServiceCenterCode, pServiceLevelCode, pHasCoverageChanged,
    '                                     pHasSvcCenterChanged, pIsServiceWarrantyClaim, pCondition,
    '                                     pClaimBasicUpdateFields)


    '        'TODO : Update the Replacement Claim Basic Fields
    '        UpdateClaimBasicFields(oReplacementClaim, pCert, pCompany,
    '                           pHasCoverageChanged, pCoverageTypeCode,
    '                           pHasSvcCenterChanged, pServiceCenterCode, pClaimBasicUpdateFields)

    '        'Update Replacement Claim Fields
    '        replaceClaimCiC = pCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = oReplacementClaim.CertItemCoverageId).FirstOrDefault()

    '        ' Serial Number, Make and Model of the Replaced Device should be stored at the Claim Level and not replaced to the Item level
    '        Me.AddEquipment(replaceClaimCiC,
    '                        oReplacementClaim,
    '                        ClaimEquipmentTypeCodes.Claimed,
    '                        pSerialNumberReplacedDevice,,
    '                        pManufacturer,
    '                        pModel)

    '        If pCondition = IClaimManager.Enum.Refurbished Then
    '            oReplacementClaim.AuthorizedAmount = pAuthorizedAmount
    '        Else
    '            methodOfRepairCode = oReplacementClaim.MethodOfRepairId.ToCode(Me.CommonManager, ListCodes.MethodOfRepair)
    '            'For Replacement Claims, the Auth Amount is based on the Method of Repair and make/model passed in the request
    '            Dim oPriceListbyMakeAndModel As IEnumerable(Of PriceListDetailRecord) = GetPriceListByMakeAndModel(replaceClaimCiC,
    '                                                                                                       pServiceCenterCode,
    '                                                                                                       pServiceLevelCode,
    '                                                                                                       ServiceClassCodes.Replacement,
    '                                                                                                       ServiceTypeCodes.ReplacementPrice,
    '                                                                                                       pCompany.Code,
    '                                                                                                       m_CompanyGroupManager.GetCompanyGroup(pCompany.CompanyGroupId).RiskTypes.Where(Function(r) r.RiskTypeId = replaceClaimCiC.Item.RiskTypeId).First.RiskTypeEnglish,
    '                                                                                                       pManufacturer,
    '                                                                                                       pModel,
    '                                                                                                       oReplacementClaim,
    '                                                                                                       oDealer.DealerCode,
    '                                                                                                       m_PriceListLowPrice,
    '                                                                                                       m_PriceListHighPrice,
    '                                                                                                       DateTime.Today,
    '                                                                                                       m_CommonManager)


    '            'Me.GetPriceListByMakeAndModel(replaceClaimCiC, pServiceCenterCode, pServiceLevelCode, methodOfRepairCode, oReplacementClaim)
    '            Dim oAuthAmount As Decimal = 0
    '            If (Not oPriceListbyMakeAndModel Is Nothing) Then
    '                oAuthAmount = oPriceListbyMakeAndModel.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Replacement AndAlso
    '                               p.ServiceTypeCode = ServiceTypeCodes.ReplacementPrice).FirstOrDefault.Price
    '            End If

    '            oReplacementClaim.AuthorizedAmount = oAuthAmount
    '        End If

    '        'Add sim card cost to the Authorized amount
    '        oReplacementClaim.AuthorizedAmount = oReplacementClaim.AuthorizedAmount + pSimCardAmount

    '        'Dedutible for Replacement Claim depends on the Loss Type - Total/Partial
    '        PrePopulateDeductible(replaceClaimCiC, oDealer, oReplacementClaim, pLossType)

    '        'Save the Replacement Claim Updates
    '        Me.Save(oReplacementClaim)

    '    Catch cnf As ClaimNotFoundException
    '        'DEF-27183 : Update the Repair Claim Basic Fields Only
    '        UpdateClaimBasicFields(pRepairClaim, pCert, pCompany,
    '                           pHasCoverageChanged, pCoverageTypeCode,
    '                           pHasSvcCenterChanged, pServiceCenterCode, pClaimBasicUpdateFields)
    '        'Save the Repair Claim Updates
    '        Me.Save(pRepairClaim)


    '        'Scenario 2 : When the Customer do not show up same day and No Replacement was created by the Svc Portal on the same day 
    '        'Later the Claim Update comes from the Svc Center File and Repair Claim is updated with Diagnostics

    '        'Now what if the CreateRepairReplacement was called after this day, lets say the Customer comes back next day or later?
    '        ' If the CreateRepairReplacement provides some diagnostic Info then this issue can be solved
    '        'On receiving that Diagnostic Info, CreateRepairReplacement will do exactly whats being done in above block in UpdateIrReparableClaim

    '    End Try

    'End Sub

    Public Sub UpdateTheftClaim(pReplacementClaim As Claim,
                                       pCert As Certificate,
                                       pCompany As Company,
                                       pHasSvcCenterChanged As Boolean,
                                       pCoverageTypeCode As String,
                                       pServiceCenterCode As String,
                                       pServiceLevelCode As String,
                                       pAuthorizedAmount As Decimal?,
                                       pSerialNumberReplacedDevice As String,
                                       pModel As String,
                                       pManufacturer As String,
                                       pCondition As IClaimManager.DeviceConditionEnum,
                                       pLossType As String,
                                       pSimCardAmount As Decimal?,
                                       pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields) Implements IClaimManager.UpdateTheftClaim

        Dim oDealer As Dealer
        ' Dim methodOfRepairCode As String

        'Dim replaceClaimCi As CertificateItem
        Dim replaceClaimCiC As CertificateItemCoverage

        oDealer = pCert.GetDealer(DealerManager)


        'Update the Replacement Claim Basic Fields
        UpdateClaimBasicFields(pReplacementClaim, pCert,
                               pCompany, False,
                               String.Empty, pHasSvcCenterChanged,
                               pServiceCenterCode, pClaimBasicUpdateFields)

        'Update Replacement Claim Fields
        replaceClaimCiC = pCert.ItemCoverages.Where(Function(cic) cic.CertItemCoverageId = pReplacementClaim.CertItemCoverageId).FirstOrDefault()


        ' Serial Number, Make and Model of the Replaced Device should be stored at the Claim Level and not replaced to the Item level
        AddEquipment(replaceClaimCiC,
                            pReplacementClaim,
                            ClaimEquipmentTypeCodes.Claimed,
                            pSerialNumberReplacedDevice,,
                            pManufacturer,
                            pModel,
                            CType(pCondition, IClaimManager.DeviceConditionEnum).ToString())

        '''''if auth amount in the request is >0 then auth amount is set to the incoming auth amount irrespective of device type
        If pAuthorizedAmount > 0 Then
            pReplacementClaim.AuthorizedAmount = pAuthorizedAmount
        Else
            If pCondition = IClaimManager.DeviceConditionEnum.Refurbished Then
                pReplacementClaim.AuthorizedAmount = pAuthorizedAmount
            Else
                ' methodOfRepairCode = MethodofRepairCodes.Replacement 'pReplacementClaim.MethodOfRepairId.ToCode(Me.CommonManager, ListCodes.MethodOfRepair)
                Dim oPriceListbyMakeAndModel As IEnumerable(Of PriceListDetailRecord) = GetPriceListByMakeAndModel(replaceClaimCiC,
                                                                                                           pServiceCenterCode,
                                                                                                           pServiceLevelCode,
                                                                                                           ServiceClassCodes.Replacement,
                                                                                                           ServiceTypeCodes.ReplacementPrice,
                                                                                                           pCompany.Code,
                                                                                                           m_CompanyGroupManager.GetCompanyGroup(pCompany.CompanyGroupId).RiskTypes.Where(Function(r) r.RiskTypeId = replaceClaimCiC.Item.RiskTypeId).First.RiskTypeEnglish,
                                                                                                           pManufacturer,
                                                                                                           pModel,
                                                                                                           pReplacementClaim,
                                                                                                           oDealer.DealerCode,
                                                                                                           m_PriceListLowPrice,
                                                                                                           m_PriceListHighPrice,
                                                                                                           DateTime.Today,
                                                                                                           m_CommonManager)
                'Me.GetPriceListByMakeAndModel(replaceClaimCiC, pServiceCenterCode, pServiceLevelCode, methodOfRepairCode, oReplacementClaim)
                Dim oAuthAmount As Decimal = 0
                '''''if make and model is not found price list falls back on the risk type , priority for risk type=4
                If (Not oPriceListbyMakeAndModel Is Nothing) Then
                    If (oPriceListbyMakeAndModel.Where(Function(p) p.Make.ToUpper() = pManufacturer.ToUpper() AndAlso p.Model.ToUpper() = pModel.ToUpper()).Count = 0) Then
                        Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Price List not configured for Make/Model/SC: " & pManufacturer & "/" & pModel & "/" & pServiceCenterCode)
                    End If
                    oAuthAmount = oPriceListbyMakeAndModel.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Replacement AndAlso
                                   p.ServiceTypeCode = ServiceTypeCodes.ReplacementPrice).FirstOrDefault.Price
                End If
                pReplacementClaim.AuthorizedAmount = oAuthAmount
            End If
        End If

        If (pReplacementClaim.AuthorizedAmount = 0) Then
            Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Price List not configured for Make/Model/SC: " & pManufacturer & "/" & pModel & "/" & pServiceCenterCode)
        End If

        ''''add the sim card amount
        pReplacementClaim.AuthorizedAmount = pReplacementClaim.AuthorizedAmount + pSimCardAmount

        'Dedutible for Replacement Claim depends on the Loss Type - Total/Partial
        Dim certItemMake As String = String.Empty
        certItemMake = m_CompanyGroupManager.GetCompanyGroup(pCompany.CompanyGroupId).Manufacturers.Where(Function(m) m.ManufacturerId = replaceClaimCiC.Item.ManufacturerId).FirstOrDefault.Description

        PrePopulateDeductible(replaceClaimCiC, oDealer, pReplacementClaim, pLossType, ClaimActions.Update,,
                              certItemMake, replaceClaimCiC.Item.Model, False)

        If (pReplacementClaim.Deductible = 0) Then
            Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Price List not configured for Make/Model/SC: " & certItemMake & "/" & replaceClaimCiC.Item.Model & "/" & pServiceCenterCode)
        End If
        'Save the Replacement Claim Updates
        Save(pReplacementClaim)

    End Sub

    Public Sub DenyClaim(pRepairClaim As Claim,
                         pCert As Certificate,
                         pCompany As Company,
                         pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields) Implements IClaimManager.DenyClaim
        'Get the Claim that needs to be updated

        Dim oReplacementClaim As Claim
        Dim repClaimNUmber As String

        'Expected Behavior as per DEF-27210
        'When diagnostic Is "Repaired" Or "Denied" Or "Replaced Refurbished" And there Is an existing REPLACEMENT CLAIM; 
        '   a.No update should be done to the Repair Claim
        '   b.If Existing Replacement Claim Is Active / Not paid
        '       System should mark both REPAIR And REPLACEMENT claims as pending And return error message "Active Replacement Claim found"
        '   c.If existing REPLACEMENT CLAIM Is already fully Paid/closed; 
        'System should mark REPAIR claim as pending And return error message "Closed Replacement Claim found"

        'If Diagnostic is Denied, No Active Replacement Claim is expected
        Try
            If pRepairClaim.ClaimNumber.ToUpper.EndsWith("S") Then
                repClaimNUmber = pRepairClaim.ClaimNumber.Substring(0, pRepairClaim.ClaimNumber.Length - 1)
                repClaimNUmber &= "R"
            Else
                repClaimNUmber = pRepairClaim.ClaimNumber
                repClaimNUmber &= "R"
            End If

            oReplacementClaim = GetClaim(repClaimNUmber, pRepairClaim.CompanyId)

            'If Active Replacement Claim is found then set that Claim as Pending and throw Fault
            'If Pending Replacement Claim is found that means its a Repocessing Case so Deny the Replacement Claim
            If oReplacementClaim.StatusCode = ClaimStatusCodes.Pending AndAlso pRepairClaim.StatusCode = ClaimStatusCodes.Pending Then
                oReplacementClaim.AuthorizedAmount = 0D
                oReplacementClaim.Deductible = 0D
                oReplacementClaim.StatusCode = ClaimStatusCodes.Denied
                oReplacementClaim.ClaimClosedDate = DateTime.Today

                'Set the Denial Reason
                Select Case pClaimBasicUpdateFields.ExtendedStatuses.First.Code
                    Case UpdateActionTypeCodes.NoProblemFound
                        oReplacementClaim.DeniedReasonId = DeniedReasonCodes.NoProblemFound.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                    Case UpdateActionTypeCodes.UnderMFW
                        oReplacementClaim.DeniedReasonId = DeniedReasonCodes.UnderMFGWarranty.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                End Select

                Save(oReplacementClaim)

                'Continue with Repair claim update in this scenario

            ElseIf oReplacementClaim.StatusCode = ClaimStatusCodes.Active Then
                'Case b. from above expected Behavior
                oReplacementClaim.StatusCode = ClaimStatusCodes.Pending
                Save(oReplacementClaim)
                pRepairClaim.StatusCode = ClaimStatusCodes.Pending
                Save(pRepairClaim)

                'No Updates to the Repair Claim and send the User a Message Back
                Throw New ReplacementClaimFoundException(pRepairClaim.CompanyId, pRepairClaim.ClaimNumber,
                                                         "Active Replacement Claim Found")

            ElseIf oReplacementClaim.StatusCode = ClaimStatusCodes.Closed Then
                'Case c. from above expected Behavior
                pRepairClaim.StatusCode = ClaimStatusCodes.Pending
                Save(pRepairClaim)
                Throw New ReplacementClaimFoundException(pRepairClaim.CompanyId, pRepairClaim.ClaimNumber,
                                                        "Closed Replacement Claim Found")
            End If


        Catch cnf As ClaimNotFoundException
            'Do Nothing and Perform the Next Update steps with the Repaired Claim
        End Try

        'TODO : Do we need to Update Basic Claim Fields for Deny Claim?
        UpdateClaimBasicFields(pRepairClaim, pCert, pCompany,
                                   False, String.Empty,
                                   False, String.Empty, pClaimBasicUpdateFields)

        pRepairClaim.AuthorizedAmount = 0D
        pRepairClaim.Deductible = 0D
        'Set the Claim status_code as 'D'
        pRepairClaim.StatusCode = ClaimStatusCodes.Denied

        'Set the Denial Reason
        If Not pClaimBasicUpdateFields.ExtendedStatuses Is Nothing Then
            Select Case pClaimBasicUpdateFields.ExtendedStatuses.First.Code
                Case UpdateActionTypeCodes.NoProblemFound
                    pRepairClaim.DeniedReasonId = DeniedReasonCodes.NoProblemFound.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                Case UpdateActionTypeCodes.UnderMFW
                    pRepairClaim.DeniedReasonId = DeniedReasonCodes.UnderMFGWarranty.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
            End Select
        Else
            pRepairClaim.DeniedReasonId = DeniedReasonCodes.NoProblemFound.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
        End If

        pRepairClaim.ClaimClosedDate = Date.Today

        Save(pRepairClaim)


    End Sub

    Public Sub DenyClaim(pClaim As Claim,
                         pCert As Certificate,
                         pComments As String) Implements IClaimManager.DenyClaim
        'Get the Claim that needs to be updated

        pClaim.AuthorizedAmount = 0D
        pClaim.Deductible = 0D
        pClaim.StatusCode = ClaimStatusCodes.Denied
        pClaim.ClaimClosedDate = Date.Today

        pClaim.DeniedReasonId = DeniedReasonCodes.CustomerDesistClaim.ToGuid(ListCodes.DeniedReasonCode, CommonManager)

        AddCommentToCertificate(pCert.CertificateId,
                                pClaim,
                                pComments)

        Save(pClaim)


    End Sub

    Public Function CreateRepairReplacmentClaim(pCertificate As Certificate, pRepairClaim As Claim) As Claim Implements IClaimManager.CreateRepairReplacmentClaim

        '''''create a shell for Replacement claim and copy entire Repair claim information

        Dim oClaimNumber As String = String.Empty
        Dim oReplacementClaim As Claim

        oReplacementClaim = pRepairClaim.Clone


        If (pRepairClaim.ClaimNumber.ToUpperInvariant.EndsWith("R") OrElse pRepairClaim.ClaimNumber.ToUpperInvariant.EndsWith("S")) Then
            oClaimNumber = pRepairClaim.ClaimNumber.Substring(0, pRepairClaim.ClaimNumber - 1)
        Else
            oClaimNumber = pRepairClaim.ClaimNumber
        End If

        oReplacementClaim.ClaimNumber = oClaimNumber + "R"
        oReplacementClaim.MethodOfRepairId = MethodofRepairCodes.Replacement.ToGuid(ListCodes.MethodOfRepair, CommonManager)

        oReplacementClaim.AuthorizedAmount = GetPriceListByMethodOfRepair(pCertificate.ItemCoverages.ElementAt(0),
                                                                             CountryManager.GetServiceCenterById(CompanyManager.GetCompany(pCertificate.CompanyId).CountryId, oReplacementClaim.ServiceCenterId).CODE,
                                                                             String.Empty, MethodofRepairCodes.Replacement, oReplacementClaim)
        oReplacementClaim.StatusCode = ClaimStatusCodes.Active
        oReplacementClaim.Deductible = 0

        Return oReplacementClaim

    End Function

    Public Function CreateServiceWarrantyClaim(pCertificate As Certificate, pRepairClaim As Claim) As Claim Implements IClaimManager.CreateServiceWarrantyClaim

        '''''create a shell for Replacement claim and copy entire Repair claim information
        Dim oClaimNumber As String = String.Empty
        Dim oServiceWarrantyClaim As Claim
        oServiceWarrantyClaim = pRepairClaim.Clone

        If (pRepairClaim.ClaimNumber.ToUpperInvariant.EndsWith("R") OrElse pRepairClaim.ClaimNumber.ToUpperInvariant.EndsWith("S")) Then
            oClaimNumber = pRepairClaim.ClaimNumber.Substring(0, pRepairClaim.ClaimNumber - 1)
        Else
            oClaimNumber = pRepairClaim.ClaimNumber
        End If

        oServiceWarrantyClaim.ClaimNumber = pRepairClaim.ClaimNumber + "S"
        oServiceWarrantyClaim.AuthorizedAmount = 0D
        oServiceWarrantyClaim.Deductible = 0D

        Return oServiceWarrantyClaim

    End Function

    Public Function CreateClaim(pCic As CertificateItemCoverage,
                                pClaim As Claim,
                                pDealer As Dealer,
                                pCoverageTypeCode As String,
                                pServiceCenterCode As String,
                                pClaimType As Integer,
                                pMake As String,
                                pModel As String) As Object Implements IClaimManager.CreateClaim

        Dim oClaim As Claim = pClaim
        ''Dim pDealer As Dealer = pCic.Certificate.GetDealer(Me.DealerManager)
        Dim oMethodOfRepairCode As String
        Dim certItemCoverageDed As CertificateItemCoverageDeductible = Nothing

        oClaim.CertItemCoverageId = pCic.CertItemCoverageId
        oClaim.OriginalRiskTypeId = pCic.Item.RiskTypeId
        oClaim.CompanyId = pDealer.GetCompany(CompanyManager).CompanyId
        If (pClaimType = 0) Then
            oClaim.MethodOfRepairId = If(pCic.MethodOfRepairId.Equals(Guid.Empty) OrElse pCic.MethodOfRepairId Is Nothing, pCic.Certificate.MethodOfRepairId, pCic.MethodOfRepairId)
            oMethodOfRepairCode = oClaim.MethodOfRepairId.ToCode(CommonManager, ListCodes.MethodOfRepair)
        Else
            oClaim.MethodOfRepairId = MethodofRepairCodes.Replacement.ToGuid(ListCodes.MethodOfRepair, CommonManager)
            oMethodOfRepairCode = MethodofRepairCodes.Replacement
        End If
        oClaim.Deductible = 0D
        oClaim.ReportedDate = DateTime.Today
        oClaim.WhoPaysId = WhoPaysCodes.Assurant.ToGuid(ListCodes.WhoPaysCode, CommonManager)
        oClaim.MgrAuthAmountFlag = YesNoCodes.No
        oClaim.StatusCode = ClaimStatusCodes.Active
        oClaim.ClaimAuthTypeId = GetClaimAuthTypeCode(ClaimAuthorizationTypes.Single_Auth)

        oClaim.ServiceCenterId = CountryManager.GetServiceCenterByCode(pDealer.GetCompany(CompanyManager).BusinessCountryId, pServiceCenterCode).ServiceCenterId

        ''''check whether the cert item has make and model and if not expect the make and model in the request.
        If (pCic.Item.ManufacturerId.Equals(Guid.Empty) OrElse IsNothing(pCic.Item.ManufacturerId)) Then
            ''''validate the make and model if provided in the request
            If (pMake = String.Empty) Then
                '''''if the cert item has no make and model and also make and model are not provided in the request.
                Throw New CertificateItemNotFoundException(Guid.Empty, pMake)
            Else
                Dim oManufacturerId As Guid = ValidateManufacturerByCompanyGroup(pMake,
                                                                                 pCic.Certificate.CompanyId,
                                                                                 pCic.CertItemCoverageId)

                pCic.Item.ManufacturerId = oManufacturerId
                pCic.Item.Model = pModel

                m_CertItemRepository.Save(pCic.Item)
            End If
        End If
        ''''Create Claimed Equipment"
        oClaim = CreateEquipmentInfo(pCic, oClaim)

        ''''Check Denying and Pending Rules
        CheckForRules(pCic, oClaim)

        '''''Get next claim number based on claim type
        oClaim.ClaimNumber = String.Empty

        GetAuthAmountByMORandClaimType(pClaimType, pCic, oClaim, pServiceCenterCode, oMethodOfRepairCode)
        GetNewClaimNumberByClaimType(pClaimType, pDealer, pClaim)

        ' Dim b As Boolean = Me.HasIssues(pCic, pClaim)-----ToDo as GetClaim method is throwing error

        ''''Adjust Method of Repair"
        oClaim = AdjustMethodofRepair(pCic, oClaim)

        ''''Locate Claim Activity"
        oClaim.ClaimActivityId = LocateClaimActivity(oClaim)

        If (oMethodOfRepairCode = MethodofRepairCodes.Recovery) Then
            oClaim.Deductible = 0D
            oClaim.LiabilityLimit = 0D
            oClaim.DeductiblePercent = 0D
            oClaim.DeductibleByPercentId = YesNoCodes.Yes.ToGuid(ListCodes.YesNo, CommonManager)
        Else
            oClaim.LiabilityLimit = pCic.LiabilityLimits
            ''''PrePopulate Deductible
            PrePopulateDeductible(pCic, pDealer, oClaim,, ClaimActions.NewClaim, pClaimType)
        End If
        oClaim.FollowupDate = Date.Now.AddDays(pDealer.GetCompany(CompanyManager).DefaultFollowUpDays)
        ''''Calculate Discount Percent
        oClaim = CalculateDiscountPercent(pCic, oClaim, oMethodOfRepairCode)

        ''''Add Rules and Issues only for Repair claims
        If (pClaimType = ClaimTypeCodes.Repair) Then
            AddRulesAndIssuesToClaim(pDealer, pCic, oClaim)
        End If

        oClaim.Bonus = 0D
        oClaim.BonusTax = 0D

        Return oClaim
    End Function

    Private Sub AddRulesAndIssuesToClaim(pDealer As Dealer, pCic As CertificateItemCoverage, ByRef pClaim As Claim)

        ''''Add Rules
        For Each pRule As Rule In pDealer.GetRules()
            DirectCast(RuleFactory.Current.GetRuleExecutor(pRule), BaseClaimRule).Execute(pClaim, pCic)
        Next

        Dim oClaimId As Guid = pClaim.ClaimId

        ''''Add claim issue status
        For Each oei As EntityIssue In pClaim.EntityIssues.Where(Function(ei) ei.EntityId = oClaimId)
            AddClaimIssueStatus(oei, ClaimIssueStatusCodes.Open)
        Next
    End Sub

    Private Sub AddClaimIssueStatus(ByRef pEntityIssue As EntityIssue, pClaimIssueStatusCode As String)

        Dim oComment As String = String.Empty

        If (pClaimIssueStatusCode = ClaimIssueStatusCodes.Open) Then
            oComment = "Issue Opened"
        End If
        '''''Add claim issue status
        pEntityIssue.ClaimIssueStatuses.Add(New ClaimIssueStatus() With {
                .ClaimIssueStatusId = Guid.NewGuid(),
                .ClaimIssueId = pEntityIssue.EntityIssueId,
                .ClaimIssueStatusCId = pClaimIssueStatusCode.ToGuid(ListCodes.ClaimIssueStatusCode, CommonManager),
                .Comments = oComment})


    End Sub

    Private Sub GetAuthAmountByMORandClaimType(pClaimType As Integer,
                                                    pCic As CertificateItemCoverage,
                                                    ByRef pClaim As Claim,
                                                    pServiceCentercode As String,
                                                    pMethodOfRepairCode As String)

        Dim oAuthAmount As Decimal

        If (pClaimType = ClaimTypeCodes.Repair OrElse pClaimType = ClaimTypeCodes.OriginalReplacement) Then
            ''''Authorization amount
            oAuthAmount = GetPriceListByMethodOfRepair(pCic,
                                                          pServiceCentercode,
                                                          String.Empty,
                                                          pMethodOfRepairCode,
                                                          pClaim)
            pClaim.AuthorizedAmount = oAuthAmount

        ElseIf (pClaimType = ClaimTypeCodes.ServiceWarranty) Then
            pClaim.AuthorizedAmount = 0D
        End If
    End Sub

    Private Sub GetNewClaimNumberByClaimType(pClaimType As Integer,
                                                    pDealer As Dealer,
                                                    ByRef pClaim As Claim)

        Dim oClaimNumber As String = m_ClaimRepository.GetNextClaimNumber(pDealer.GetCompany(CompanyManager).CompanyId)
        pClaim.ClaimNumber = oClaimNumber
        pClaim.MasterClaimNumber = oClaimNumber

        If (pClaimType = ClaimTypeCodes.OriginalReplacement) Then
            pClaim.ClaimNumber = pClaim.ClaimNumber + "R"

        ElseIf (pClaimType = ClaimTypeCodes.ServiceWarranty) Then
            pClaim.ClaimNumber = pClaim.ClaimNumber + "S"
        End If
    End Sub

    Public Function ValidateServiceCenter(pDealer As Dealer, pServiceCenterCode As String) As Boolean Implements IClaimManager.ValidateServiceCenter
        Dim oCompany As Company = pDealer.GetCompany(CompanyManager)
        If (CountryManager.GetServiceCenterByCode(pDealer.GetCompany(CompanyManager).BusinessCountryId, pServiceCenterCode) Is Nothing) Then
            Return False
        End If
        Return True
    End Function

    Public Function ValidateManufacturerByCompanyGroup(pNewMake As String, pCompanyId As Guid, pCertItemCoverageId As Guid) As Guid Implements IClaimManager.ValidateManufacturerByCompanyGroup
        Dim oManufacturerId As Guid = m_ClaimRepository.ValidateManufacturerByCompanyGroup(pNewMake,
                                                                                                 pCompanyId,
                                                                                                 pCertItemCoverageId)
        If (oManufacturerId.Equals(Guid.Empty)) Then
            Throw New ManufacturerNotFoundException(Guid.Empty, pNewMake)
        End If

        Return oManufacturerId

    End Function

    Private Function CalculateDiscountPercent(pCic As CertificateItemCoverage, oClaim As Claim, methodOfRepairCode As String) As Claim
        Dim discountPercent As Long = 0
        If (methodOfRepairCode = MethodofRepairCodes.CarryIn OrElse methodOfRepairCode = MethodofRepairCodes.Home) Then
            oClaim.DiscountPercent = pCic.RepairDiscountPCt
        ElseIf (methodOfRepairCode = MethodofRepairCodes.CarryIn) Then
            oClaim.DiscountPercent = pCic.ReplacementDiscountPct
        End If

        Return oClaim
    End Function

    'Public Sub PrePopulateDeductible(ByVal pCic As CertificateItemCoverage, ByVal pDealerId As Guid, ByVal pClaim As IClaim, Optional ByVal pLossType As String = "")
    '    Dim oDealer As Dealer
    '    oDealer = Me.DealerManager.GetDealerById(pDealerId)

    '    pClaim.AuthAmount = 0.00

    'End Sub

    'Private Sub PrePopulateDeductible(ByVal pCic As CertificateItemCoverage,
    '                                  ByVal pDealer As Dealer,
    '                                  ByVal pClaim As IClaim,
    '                                  Optional ByVal pLossType As String = "")
    '    ''deductible information
    '    Dim odeductibleType As DeductibleType = Me.GetDeductible(pCic, pClaim.MethodOfRepairId)

    '    Select Case odeductibleType.DeductibleBasedOn.ToUpperInvariant()
    '        Case DeductibleBasedOnCodes.PercentageOfAuthAmount
    '            If odeductibleType.Deductible > 0 Then
    '                pClaim.DeductibleByPercentId = YesNoCodes.Yes.ToGuid(ListCodes.YesNo, CommonManager)
    '                pClaim.Deductible = 0D
    '                pClaim.DeductiblePercent = odeductibleType.Deductible
    '                ''''calculate deductible if by percentage
    '                CalculatedeductibleIfByPercentage(pClaim, odeductibleType)
    '            Else
    '                pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
    '                pClaim.Deductible = 0D
    '            End If
    '        Case DeductibleBasedOnCodes.Fixed
    '            pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
    '            pClaim.Deductible = odeductibleType.Deductible

    '        Case DeductibleBasedOnCodes.PercentageOfItemRetailPrice
    '            pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
    '            pClaim.Deductible = pItemRetailPrice * odeductibleType.Deductible / 100

    '        Case DeductibleBasedOnCodes.PercentageOfOrigRetailPrice
    '            pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
    '            pClaim.Deductible = pOriginalRetailPrice * odeductibleType.Deductible / 100

    '        Case DeductibleBasedOnCodes.SalesPrice
    '            pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
    '            pClaim.Deductible = pSalesPrice * odeductibleType.Deductible / 100

    '        Case DeductibleBasedOnCodes.PercentageOfListPrice
    '            pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
    '            ''calculate list price deductible
    '            Dim listPriceDed As Decimal = 0D
    '            listPriceDed = Me.GetListPrice(pCertItemSkuNumber, pDealer, pClaimEquipmentSkuNumber, pLossDate)
    '            If (listPriceDed > 0) Then
    '                pClaim.Deductible = listPriceDed * odeductibleType.Deductible / 100
    '            End If
    '        Case DeductibleBasedOnCodes.Expression
    '            pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
    '            Dim oClaimSvcCenter As ServiceCenter = Me.CountryManager.GetServiceCenterById(pDealer.GetCompany(m_CompanyManager).BusinessCountryId,
    '                                                                                          pServiceCenterId)
    '            pClaim.DeductiblePercent = Nothing

    '            Dim listPrice As Nullable(Of Decimal) = Nothing
    '            pClaim.Deductible = SerializationExtensions.Serialize(Of BaseExpression)(
    '                CommonManager.GetExpression(odeductibleType.ExpressionId).ExpressionXml).
    '                Evaluate(Function(variableName)
    '                             Select Case variableName.ToUpperInvariant()
    '                                 Case "ListPrice".ToUpperInvariant()
    '                                     If (Not listPrice.HasValue) Then
    '                                         listPrice = Me.GetListPrice(pCertItemSkuNumber, pDealer, pClaimEquipmentSkuNumber, pLossDate)
    '                                     End If
    '                                     Return listPrice.Value
    '                                 Case "SalesPrice".ToUpperInvariant()
    '                                     Return pSalesPrice
    '                                 Case "OrigRetailPrice".ToUpperInvariant()
    '                                     Return pOriginalRetailPrice
    '                                 Case "ItemRetailPrice".ToUpperInvariant()
    '                                     Return pItemRetailPrice
    '                                 Case "LossType".ToUpperInvariant()
    '                                     Return pLossType.ToUpperInvariant()
    '                                 Case "AuthorizedAmount".ToUpperInvariant()
    '                                     Return pClaim.AuthAmount
    '                                 Case "DeductibleBasePrice".ToUpperInvariant()
    '                                     Return GetPriceListByServiceType(pCic,
    '                                                                      oClaimSvcCenter.CODE,
    '                                                                      Nothing,
    '                                                                      ServiceClassCodes.Deductible,
    '                                                                      ServiceTypeCodes.DeductibleBasePrice,
    '                                                                      pClaim,
    '                                                                      If(Not pRepairDate Is Nothing,
    '                                                                      pRepairDate,
    '                                                                      DateTime.Today))
    '                                 Case Else
    '                                     Return String.Empty
    '                             End Select
    '                         End Function)
    '    End Select
    'End Sub

    Private Sub PrePopulateDeductible(pCic As CertificateItemCoverage,
                                      pDealer As Dealer,
                                      pClaim As Claim,
                                      Optional ByVal pLossType As String = "",
                                      Optional ByVal pClaimAction As String = "",
                                      Optional ByVal pClaimType As Integer = Nothing,
                                      Optional ByVal pMake As String = "",
                                      Optional ByVal pModel As String = "",
                                      Optional ByVal pHasCoverageChanged As Boolean = False)
        ''deductible information
        Dim odeductibleType As DeductibleType = GetDeductible(pCic, pClaim.MethodOfRepairId, pHasCoverageChanged)

        Select Case odeductibleType.DeductibleBasedOn.ToUpperInvariant()
            Case DeductibleBasedOnCodes.PercentageOfAuthAmount
                If odeductibleType.Deductible > 0 Then
                    pClaim.DeductibleByPercentId = YesNoCodes.Yes.ToGuid(ListCodes.YesNo, CommonManager)
                    pClaim.Deductible = 0D
                    pClaim.DeductiblePercent = odeductibleType.Deductible
                    ''''calculate deductible if by percentage
                    CalculatedeductibleIfByPercentage(pClaim, odeductibleType)
                Else
                    pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
                    pClaim.Deductible = 0D
                End If
            Case DeductibleBasedOnCodes.Fixed
                pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
                pClaim.Deductible = odeductibleType.Deductible

            Case DeductibleBasedOnCodes.PercentageOfItemRetailPrice
                pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
                pClaim.Deductible = pCic.Item.ItemRetailPrice * odeductibleType.Deductible / 100

            Case DeductibleBasedOnCodes.PercentageOfOrigRetailPrice
                pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
                pClaim.Deductible = pCic.Item.OriginalRetailPrice * odeductibleType.Deductible / 100

            Case DeductibleBasedOnCodes.SalesPrice
                pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
                pClaim.Deductible = pCic.Item.Certificate.SalesPrice * odeductibleType.Deductible / 100

            Case DeductibleBasedOnCodes.PercentageOfListPrice
                pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
                ''calculate list price deductible
                Dim listPriceDed As Decimal = 0D
                listPriceDed = GetListPrice(pCic, pDealer, pClaim)
                If (listPriceDed > 0) Then
                    pClaim.Deductible = listPriceDed * odeductibleType.Deductible / 100
                End If
            Case DeductibleBasedOnCodes.Expression
                pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
                Dim oClaimSvcCenter As ServiceCenter = CountryManager.GetServiceCenterById(pDealer.GetCompany(m_CompanyManager).BusinessCountryId,
                                                                                              pClaim.ServiceCenterId)
                pClaim.DeductiblePercent = Nothing

                ''For New claims if deductible is based on expression then deductible is 30% for Repair and 50% REplacement of Auth amount
                Select Case pClaimAction

                    Case ClaimActions.NewClaim
                        If pClaimType = ClaimTypeCodes.Repair Then
                            pClaim.Deductible = pClaim.AuthorizedAmount * m_NewClaimRepairDedPercent / 100
                        ElseIf pClaimType = ClaimTypeCodes.OriginalReplacement Then
                            pClaim.Deductible = pClaim.AuthorizedAmount * m_NewClaimOrigReplDedPercent / 100
                        End If

                    Case ClaimActions.Update
                        Dim listPrice As Decimal? = Nothing
                        Dim oCompany As Company = m_CompanyManager.GetCompany(pDealer.CompanyId)

                        If odeductibleType.ExpressionId Is Nothing Then
                            Exit Sub
                        Else
                            pClaim.Deductible = Serialize (Of BaseExpression)(
                            CommonManager.GetExpression(odeductibleType.ExpressionId).ExpressionXml).
                            Evaluate(Function(variableName)
                                         Select Case variableName.ToUpperInvariant()
                                             Case "ListPrice".ToUpperInvariant()
                                                 If (listPrice.HasValue) Then
                                                     Return listPrice.Value
                                                 Else
                                                     listPrice = GetListPrice(pCic, pDealer, pClaim)
                                                     If listPrice.HasValue Then
                                                         Return listPrice.Value
                                                     Else
                                                         Return Nothing
                                                     End If
                                                 End If
                                                 Return listPrice.Value
                                             Case "SalesPrice".ToUpperInvariant()
                                                 Return pCic.Item.Certificate.SalesPrice
                                             Case "OrigRetailPrice".ToUpperInvariant()
                                                 Return pCic.Item.OriginalRetailPrice
                                             Case "ItemRetailPrice".ToUpperInvariant()
                                                 Return pCic.Item.ItemRetailPrice
                                             Case "LossType".ToUpperInvariant()
                                                 Return pLossType.ToUpperInvariant()
                                             Case "AuthorizedAmount".ToUpperInvariant()
                                                 Return pClaim.AuthorizedAmount
                                             Case "DeductibleBasePrice".ToUpperInvariant()
                                                 Dim oPriceListbyMakeAndModel As IEnumerable(Of PriceListDetailRecord) = GetPriceListByMakeAndModel(pCic,
                                                                           oClaimSvcCenter.CODE,
                                                                           Nothing,
                                                                           ServiceClassCodes.Deductible,
                                                                           ServiceTypeCodes.DeductibleBasePrice,
                                                                           oCompany.Code,
                                                                           m_CompanyGroupManager.GetCompanyGroup(oCompany.CompanyGroupId).RiskTypes.Where(Function(r) r.RiskTypeId = pCic.Item.RiskTypeId).First.RiskTypeEnglish,
                                                                           pMake,
                                                                           pModel,
                                                                           pClaim,
                                                                           pDealer.DealerCode,
                                                                           m_PriceListLowPrice,
                                                                           m_PriceListHighPrice,
                                                                           DateTime.Today,
                                                                           m_CommonManager)
                                                 Dim oDeductible As Decimal = 0
                                                 If (Not oPriceListbyMakeAndModel Is Nothing) Then
                                                     oDeductible = oPriceListbyMakeAndModel.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Deductible AndAlso
                                                                p.ServiceTypeCode = ServiceTypeCodes.DeductibleBasePrice).FirstOrDefault.Price
                                                 End If
                                                 Return oDeductible

                                             Case Else
                                                 Return String.Empty
                                         End Select
                                     End Function)
                        End If
                End Select
            Case DeductibleBasedOnCodes.ComputedExternally
                pClaim.DeductibleByPercentId = YesNoCodes.No.ToGuid(ListCodes.YesNo, CommonManager)
                pClaim.Deductible = 0D
        End Select
    End Sub

    Public Sub CalculatedeductibleIfByPercentage(ByRef pClaim As Claim, pDeductibleType As DeductibleType)
        Dim authAmount As Decimal = New Decimal(0D)
        authAmount = pClaim.AuthorizedAmount
        'If Me.ClaimAuthorizationType = BusinessObjectsNew.ClaimAuthorizationType.Single Then
        '    authAmount = Me.AuthorizedAmount.Value
        'Else
        '    authAmount = CalculateAuthorizedAmountForDeductible()
        'End If

        If pClaim.LiabilityLimit.Value > 0 Then
            If pClaim.LiabilityLimit.Value > authAmount Then
                pClaim.Deductible = (authAmount * pClaim.DeductiblePercent.Value) / 100
            Else
                pClaim.Deductible = (pClaim.LiabilityLimit.Value * pClaim.DeductiblePercent.Value) / 100
            End If
        End If

        If pClaim.LiabilityLimit.Value = 0 Then
            pClaim.Deductible = (authAmount * pClaim.DeductiblePercent.Value) / 100
        End If

        'If Me.LiabilityLimit.Value > 0 Then
        '    If Me.LiabilityLimit.Value > authAmount Then
        '        Me.Deductible = New DecimalType((authAmount * Me.DeductiblePercent.Value) / 100)
        '    Else
        '        Me.Deductible = New DecimalType((Me.LiabilityLimit.Value * Me.DeductiblePercent.Value) / 100)
        '    End If
        'End If
        'If Me.LiabilityLimit.Value = 0 Then
        '    Me.Deductible = New DecimalType((authAmount * Me.DeductiblePercent.Value) / 100)
        'End If
        'End If
    End Sub

    Private Function AdjustMethodofRepair(pCic As CertificateItemCoverage, oClaim As Claim) As Claim
        If (oClaim.MethodOfRepairId.ToCode(CommonManager, ListCodes.MethodOfRepair) <> MethodofRepairCodes.Recovery) Then
            Dim coverageTypeCode As String = pCic.CoverageTypeId.ToCode(CommonManager, ListCodes.CoverageType)
            If (coverageTypeCode = CoverageTypeCodes.TheftLoss OrElse
                coverageTypeCode = CoverageTypeCodes.Theft OrElse
                coverageTypeCode = CoverageTypeCodes.Loss) Then
                oClaim.MethodOfRepairId = MethodofRepairCodes.Replacement.ToGuid(ListCodes.MethodOfRepair, CommonManager)
            End If

        End If

        Return oClaim
    End Function

    Private Function CreateEquipmentInfo(pCic As CertificateItemCoverage, oClaim As Claim) As Claim
        ''''Enrolled vs Claimed equipment
        If (oClaim.ClaimEquipments.Count = 0 AndAlso
            pCic.Certificate.GetDealer(DealerManager).UseEquipmentId.ToCode(CommonManager, ListCodes.YesNo) = YesNoCodes.Yes AndAlso
            (Not pCic.Item.ManufacturerId.Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(pCic.Item.Model))) Then

            AddEquipment(pCic, oClaim, ClaimEquipmentTypeCodes.Enrolled)

        ElseIf (pCic.Certificate.GetDealer(DealerManager).UseEquipmentId.ToCode(CommonManager, ListCodes.YesNo) = YesNoCodes.No AndAlso
        Not pCic.Item Is Nothing AndAlso
        pCic.Certificate.GetDealer(DealerManager).DealerTypeId.ToCode(CommonManager, ListCodes.DealerType) = DealerTypeCodes.Wepp) Then
            AddEquipment(pCic, oClaim, ClaimEquipmentTypeCodes.Claimed)
            '.EquipmentId = Me.m_EquipmentManager.GetEquipmentIdByEquipmentList(pCic.Item),'''''implement this
        End If
        Return oClaim
    End Function

    Private Sub AddEquipment(pCic As CertificateItemCoverage,
                             oClaim As Claim,
                             oClaimEquipmentTypeCode As String,
                             Optional ByVal pSerialNumberReplaced As String = Nothing,
                             Optional ByVal pImeiNumberReplaced As String = Nothing,
                             Optional ByVal pManufacturer As String = Nothing,
                             Optional ByVal pModel As String = Nothing,
                             Optional ByVal pDeviceCondition As String = Nothing)

        Dim oequipmentId As Guid?
        Dim oManufacturerId As Guid?
        Dim serialNumber As String
        Dim imeiNUmber As String
        Dim model As String
        Dim oDeviceConditonId As Guid?

        If (oClaimEquipmentTypeCode = "C") Then
            oequipmentId = m_EquipmentManager.GetEquipmentIdByEquipmentList(pCic.Item)
        Else
            If (Not pCic.Item.EquipmentId Is Nothing AndAlso Not pCic.Item.EquipmentId.Equals(Guid.Empty)) Then
                oequipmentId = pCic.Item.EquipmentId
            End If
        End If

        If Not String.IsNullOrEmpty(pSerialNumberReplaced) Then
            serialNumber = pSerialNumberReplaced
        Else
            serialNumber = pCic.Item.SerialNumber
        End If

        If Not String.IsNullOrEmpty(pImeiNumberReplaced) Then
            imeiNUmber = pImeiNumberReplaced
        Else
            imeiNUmber = pCic.Item.ImeiNumber
        End If

        If Not String.IsNullOrEmpty(pModel) Then
            model = pModel
        Else
            model = pCic.Item.Model
        End If

        If Not String.IsNullOrEmpty(pManufacturer) Then
            oManufacturerId = m_ClaimRepository.ValidateManufacturerByCompanyGroup(pManufacturer, pCic.Certificate.CompanyId, pCic.CertItemCoverageId)
            If (oManufacturerId.Equals(Guid.Empty)) Then
                Throw New ManufacturerNotFoundException(Guid.Empty, pManufacturer)
            End If
        Else
            oManufacturerId = pCic.Item.ManufacturerId
        End If

        If Not String.IsNullOrEmpty(pDeviceCondition) Then
            oDeviceConditonId = m_CommonManager.GetListItems(ListCodes.DeviceCondition).Where(Function(li) li.Description = pDeviceCondition).FirstOrDefault.ListItemId
        End If

        'TODO : Add Manufacturer Id from the incoming Make Description in the Request
        'Also Validate the Manufacturer coming from the Request
        If oClaim.ClaimEquipments.Count > 0 Then
            Dim ce As ClaimEquipment
            Try
                ce = oClaim.ClaimEquipments.Where(Function(i) i.ManufacturerId = pCic.Item.ManufacturerId).First()
            Catch ex As Exception
                ce = oClaim.ClaimEquipments.First()
            End Try

            ce.ManufacturerId = oManufacturerId
            ce.Model = model
            ce.DeviceTypeId = oDeviceConditonId
            ce.SerialNumber = pSerialNumberReplaced
            ce.ImeiNumber = pImeiNumberReplaced


        Else
            oClaim.ClaimEquipments.Add(New ClaimEquipment() With
                       {
                       .ClaimEquipmentId = Guid.NewGuid(),
                       .ClaimId = oClaim.ClaimId,
                       .ClaimEquipmentDate = pCic.Item.CreatedDate,
                       .ManufacturerId = oManufacturerId,
                       .Model = model,
                       .Sku = pCic.Item.SkuNumber,
                       .ImeiNumber = imeiNUmber,
                       .SerialNumber = serialNumber,
                       .EquipmentId = If(oequipmentId.Equals(Guid.Empty), Nothing, oequipmentId),
                       .ClaimEquipmentTypeId = oClaimEquipmentTypeCode.ToGuid(ListCodes.ClaimEquipmentTypeCode, CommonManager),
                       .DeviceTypeId = oDeviceConditonId
                       })
        End If






    End Sub

    Private Function LocateClaimActivity(oClaim As Claim) As Guid?
        Dim oMorCode As String = String.Empty
        Dim oClaimActivityId As Guid? = Nothing

        If (oClaim.MethodOfRepairId.Equals(Guid.Empty)) Then
            Return Nothing
        Else
            oMorCode = oClaim.MethodOfRepairId.ToCode(CommonManager, ListCodes.MethodOfRepair)
        End If

        If (oMorCode = MethodofRepairCodes.Replacement) Then
            oClaimActivityId = ClaimActivityCodes.PendingReplacement.ToGuid(ListCodes.ClaimActivityCode, CommonManager)
        ElseIf (oMorCode = MethodofRepairCodes.Legal OrElse oMorCode = MethodofRepairCodes.General) Then
            oClaimActivityId = ClaimActivityCodes.LegalGeneral.ToGuid(ListCodes.ClaimActivityCode, CommonManager)
        End If

        Return oClaimActivityId
    End Function

    Private Sub CheckForRules(pCic As CertificateItemCoverage, ByRef pClaim As Claim)
        Dim oComment As Comment = New Comment()
        Dim oDealer As Dealer = pCic.Certificate.GetDealer(DealerManager)
        If pClaim.StatusCode = ClaimStatusCodes.Pending Then
            pClaim.StatusCode = ClaimStatusCodes.Active
            oComment.CommentTypeId = CommentTypeCodes.PendingClaimApproved.ToGuid(ListCodes.CommentType, CommonManager)
        End If

        'Check for Rules for which claim could be denied
        CheckForDenyingRules(pCic, pClaim, oComment, oDealer)
        AddComment(pCic, pClaim, oComment)

        'Check for Rules for which claim could go to Pending status
        If (pClaim.StatusCode = ClaimStatusCodes.Active) Then
            CheckForPendingRules(pCic, pClaim, oComment, oDealer)
            AddComment(pCic, pClaim, oComment)
        End If

    End Sub

    Private Sub AddComment(pCic As CertificateItemCoverage, ByRef pClaim As Claim, pComment As Comment)
        If (Not pComment.CommentTypeId.Equals(Guid.Empty)) Then
            pClaim.Comments.Add(New Comment() With {
                            .CommentId = Guid.NewGuid(),
                            .CertId = pCic.Certificate.CertificateId,
                            .CommentTypeId = pComment.CommentTypeId,
                            .ClaimId = pClaim.ClaimId,
                            .Comments = pComment.Comments,
                            .CommentCreatedDate = DateTime.Now,
                            .CallerName = pClaim.CallerName
                            })

        End If
    End Sub

    Private Sub CheckForDenyingRules(pCic As CertificateItemCoverage, ByRef pClaim As Claim, ByRef pComment As Comment, pDealer As Dealer)
        'Dim osubscriberStatus As String
        Dim oFlag As Boolean = True
        Dim oClaimReportedWithinGracePeriod As Boolean = True
        Dim oClaimReportedWithinPeriod As Boolean = True
        Dim oCoverageTypeNotMissing As Boolean = True
        Dim oExceedMaxReplacements As Boolean = False
        Dim oContract As Contract

        'If (Not pCic.Certificate.SubscriberStatus.Equals(Guid.Empty) OrElse Not pCic.Certificate.SubscriberStatus Is Nothing) Then
        '    osubscriberStatus = pCic.Certificate.SubscriberStatus.ToCode(CommonManager, ListCodes.SubscriberStatus)

        If (pCic.Certificate.StatusCode = CertificateStatusCodes.Cancelled AndAlso IsGracePeriodSpecified(pDealer)) Then

            If (Not pClaim.ClaimActivityId.ToCode(CommonManager, ListCodes.ClaimActivityCode) = ClaimActivityCodes.Rework) Then
                oCoverageTypeNotMissing = IsClaimReportedWithValidCoverage(pCic, pDealer, pClaim)
            End If

            If (Not pClaim.ClaimActivityId.ToCode(CommonManager, ListCodes.ClaimActivityCode) = ClaimActivityCodes.Rework) Then
                oClaimReportedWithinGracePeriod = IsClaimReportedWithinGracePeriod(pCic, pDealer, pClaim)
            End If

            If (oCoverageTypeNotMissing AndAlso oClaimReportedWithinGracePeriod) Then
                oContract = DealerManager.GetContract(pDealer.DealerCode, pCic.Certificate.WarrantySalesDate)
                oExceedMaxReplacements = IsMaxReplacementExceeded(pCic, pClaim, oContract)
            End If
        Else '''' 'only check the condition for new claim
            oContract = DealerManager.GetContract(pDealer.DealerCode, pCic.Certificate.WarrantySalesDate)
            oExceedMaxReplacements = IsMaxReplacementExceeded(pCic, pClaim, oContract)
            If (Not pClaim.ClaimActivityId.ToCode(CommonManager, ListCodes.ClaimActivityCode) = ClaimActivityCodes.Rework) Then
                oClaimReportedWithinPeriod = IsClaimReportedWithinPeriod(pCic, pDealer, pClaim)
            End If
        End If

        'Add comments to indicate that the claim will be closed
        If (pCic.Certificate.StatusCode = CertificateStatusCodes.Cancelled AndAlso IsGracePeriodSpecified(pDealer)) Then
            If Not oCoverageTypeNotMissing Then
                oFlag = False
                pClaim.DeniedReasonId = DeniedReasonCodes.CoverageTypeMissing.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                pComment.CommentTypeId = CommentTypeCodes.ClaimDeniedCoverageTypeMissing.ToGuid(ListCodes.CommentType, CommonManager)

            ElseIf Not oClaimReportedWithinGracePeriod Then
                oFlag = False
                pClaim.DeniedReasonId = DeniedReasonCodes.NotReportedWithinGracePeriod.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                pComment.CommentTypeId = CommentTypeCodes.ClaimDeniedReportTimeNotWithInGracePeriod.ToGuid(ListCodes.CommentType, CommonManager)

            ElseIf oExceedMaxReplacements Then
                oFlag = False
                pClaim.DeniedReasonId = DeniedReasonCodes.ReplacementsExceeded.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                pComment.CommentTypeId = CommentTypeCodes.ClaimDeniedReplacementExceed.ToGuid(ListCodes.CommentType, CommonManager)

                'ElseIf (Me.EvaluateIssues = Codes.CLAIMISSUE_STATUS__REJECTED) Then
                '   oFlag = False
                '    Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                '    comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                '    comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_DENIED_ISSUE_REJECTED")


            ElseIf (Not pCic.CoverageLiabilityLimit Is Nothing AndAlso pCic.CoverageLiabilityLimit.HasValue) Then

                If (m_ClaimRepository.GetRemainingCoverageLiabilityLimit(pCic.CertItemCoverageId, pClaim.LossDate) <= 0) Then
                    oFlag = False
                    pClaim.DeniedReasonId = DeniedReasonCodes.MaxCoverageLiabilityLimitReached.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                    pComment.CommentTypeId = CommentTypeCodes.ClaimDenied.ToGuid(ListCodes.CommentType, CommonManager)
                End If
            ElseIf (Not pCic.Certificate.ProdLiabilityLimit Is Nothing AndAlso pCic.Certificate.ProdLiabilityLimit.HasValue) Then

                If (m_ClaimRepository.GetProductRemainingLiabilityLimit(pCic.Certificate.CertificateId, pClaim.LossDate) <= 0) Then
                    oFlag = False
                    pClaim.DeniedReasonId = DeniedReasonCodes.MaxProductLiabilityLimitReached.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                    pComment.CommentTypeId = CommentTypeCodes.ClaimDenied.ToGuid(ListCodes.CommentType, CommonManager)
                End If
            End If
        Else
            If oExceedMaxReplacements Then
                oFlag = False
                pClaim.DeniedReasonId = DeniedReasonCodes.ReplacementsExceeded.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                pComment.CommentTypeId = CommentTypeCodes.ClaimDeniedReplacementExceed.ToGuid(ListCodes.CommentType, CommonManager)
            End If

            If Not oClaimReportedWithinPeriod Then
                oFlag = False
                pClaim.DeniedReasonId = ReasonClosedCodes.NotReportedWithinPeriod.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                pComment.CommentTypeId = CommentTypeCodes.ClaimDeniedReportTimeExceed.ToGuid(ListCodes.CommentType, CommonManager)
            End If

            'If (Me.EvaluateIssues = Codes.CLAIMISSUE_STATUS__REJECTED) Then
            '    oFlag = False
            '    Me.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
            '    Comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
            '    Comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_DENIED_ISSUE_REJECTED")
            'End If

            If (Not pCic.CoverageLiabilityLimit Is Nothing AndAlso pCic.CoverageLiabilityLimit.HasValue) Then
                If (m_ClaimRepository.GetRemainingCoverageLiabilityLimit(pCic.CertItemCoverageId, pClaim.LossDate) <= 0) Then
                    oFlag = False
                    pClaim.DeniedReasonId = DeniedReasonCodes.MaxCoverageLiabilityLimitReached.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                    pComment.CommentTypeId = CommentTypeCodes.ClaimDenied.ToGuid(ListCodes.CommentType, CommonManager)
                End If
            End If

            If (Not pCic.Certificate.ProdLiabilityLimit Is Nothing AndAlso pCic.Certificate.ProdLiabilityLimit.HasValue) Then
                If (m_ClaimRepository.GetProductRemainingLiabilityLimit(pCic.Certificate.CertificateId, pClaim.LossDate) <= 0) Then
                    oFlag = False
                    pClaim.DeniedReasonId = DeniedReasonCodes.MaxProductLiabilityLimitReached.ToGuid(ListCodes.DeniedReasonCode, CommonManager)
                    pComment.CommentTypeId = CommentTypeCodes.ClaimDenied.ToGuid(ListCodes.CommentType, CommonManager)

                End If
            End If
        End If
        If (Not oFlag) Then
            pClaim.ClaimClosedDate = DateTime.Today
            pClaim.ClaimActivityId = Guid.Empty
            pClaim.StatusCode = ClaimStatusCodes.Denied
        End If

    End Sub

    Private Sub CheckForPendingRules(pCic As CertificateItemCoverage, pClaim As Claim, ByRef pComment As Comment, pDealer As Dealer)
        Dim oFlag As Boolean = True

        If (Not IsSubscriberStatusValid(pCic)) Then
            oFlag = False
            pComment.CommentTypeId = CommentTypeCodes.ClaimPendingSubscriberStatusNotValid.ToGuid(ListCodes.CommentType, CommonManager)
        End If

        'If Me.Contract.PayOutstandingPremiumId.Equals(LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) AndAlso Me.OutstandingPremiumAmount.Value > 0 Then
        Dim oContract As Contract = DealerManager.GetContract(pDealer.DealerCode, pCic.Certificate.WarrantySalesDate)
        If (oContract.PAY_OUTSTANDING_PREMIUM_ID.ToCode(CommonManager, ListCodes.YesNo) = YesNoCodes.Yes AndAlso CalculateOutstandingPremiumAmount(pDealer, pCic) > 0) Then
            oFlag = False
            pComment.CommentTypeId = CommentTypeCodes.PendingPaymentOnOutstandingPremium.ToGuid(ListCodes.CommentType, CommonManager)
        End If

        ''''New Equipment Flow starts here
        If IsEquipmentRequired(pDealer) Then
            If (Not ValidateAndMatchClaimedEnrolledEquipments(pCic, pDealer, pClaim, pComment)) Then
                oFlag = False
            End If
        End If
        '''''
        Dim oDeductibleType As DeductibleType = GetDeductible(pCic, pClaim.MethodOfRepairId, False)

        ' Check if Deductible Calculation Method is List and SKU Price is resolved
        If (oDeductibleType.DeductibleBasedOn = DeductibleBasedOnCodes.PercentageOfListPrice) Then
            Dim listPriceDed As Decimal = 0D
            listPriceDed = GetListPrice(pCic, pDealer, pClaim)
            'Me.GetListPrice(pCic.Item.SkuNumber, pDealer, pClaim.ClaimEquipments.FirstOrDefault.Sku, pClaim.LossDate)
            If (listPriceDed <= 0) Then
                oFlag = False
                pComment.CommentTypeId = CommonManager.GetListItems(ListCodes.CommentType).Where(Function(l) l.Code = CommentTypeCodes.ClaimSetToPending).FirstOrDefault.ListItemId
                pComment.Comments &= Environment.NewLine & CommonManager.GetLabelTranslations("DEDUCTIBLE_CAN_NOT_BE_DETERMINED_ERR").FirstOrDefault.Translation
            End If
        End If

        If (pDealer.DeductibleCollectionId.ToCode(CommonManager, ListCodes.YesNo) = YesNoCodes.Yes) Then
            If (pClaim.DedCollMethodId = CommonManager.GetListItems(ListCodes.DeductibleCollectionMethodCode).Where(Function(l) l.Code = DeductibleCollectionCodes.DeferredCollection).FirstOrDefault.ListItemId) Then
                oFlag = False
                pComment.CommentTypeId = CommonManager.GetListItems(ListCodes.CommentType).Where(Function(l) l.Code = CommentTypeCodes.ClaimSetToPending).FirstOrDefault.ListItemId
                pComment.Comments &= Environment.NewLine & CommonManager.GetLabelTranslations("ERR_DEDUCTIBLE_NOT_COLLECTED").FirstOrDefault.Translation
            End If
        End If

        'If (Me.HasIssues AndAlso Not Me.AllIssuesResolvedOrWaived) Then ---ToDo
        '    flag = flag And False
        '    Comment.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
        '    Comment.Comments &= Environment.NewLine & TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_ISSUE_OPEN")
        'End If

        If (Not oFlag) Then pClaim.StatusCode = ClaimStatusCodes.Pending

    End Sub

    Private Function IsGracePeriodSpecified(pDealer As Dealer) As Boolean
        If (Not pDealer.GracePeriodDays Is Nothing OrElse pDealer.GracePeriodMonths Is Nothing) Then
            Return True
        End If
        Return False
    End Function

    Private Function IsSubscriberStatusValid(pCic As CertificateItemCoverage) As Boolean
        Dim osubscriberStatus As String
        If (Not pCic.Certificate.SubscriberStatus.Equals(Guid.Empty) OrElse Not pCic.Certificate.SubscriberStatus Is Nothing) Then
            osubscriberStatus = pCic.Certificate.SubscriberStatus.ToCode(CommonManager, ListCodes.SubscriberStatus)
            If (osubscriberStatus = SubscriberStatusCodes.Active OrElse osubscriberStatus = SubscriberStatusCodes.PastDueClaimsAllowed) Then
                Return True
                ' REQ-1251 Allow Claim if Suspended Reason code is set to Allow Cliams
            ElseIf (osubscriberStatus = SubscriberStatusCodes.Suspended)
                If (Not pCic.Certificate.SuspendedReasonId.Equals(Guid.Empty) OrElse Not pCic.Certificate.SuspendedReasonId Is Nothing) Then
                    If (pCic.Certificate.GetDealer(DealerManager).SuspendedReasons.Where(Function(s) s.SuspendedReasonId = pCic.Certificate.SuspendedReasonId).FirstOrDefault.ClaimAllowed = YesNoCodes.Yes) Then
                        Return True
                    End If
                End If
            End If
        End If
        Return False
    End Function

    Private Function IsClaimReportedWithValidCoverage(pCic As CertificateItemCoverage, pDealer As Dealer, pClaim As Claim) As Boolean
        If (pDealer.GracePeriodMonths > 0 OrElse pDealer.GracePeriodDays > 0) Then
            Dim oGracePeriodEndDate As Date = pCic.EndDate.AddMonths(pDealer.GracePeriodMonths).AddDays(pDealer.GracePeriodDays)

            If (pClaim.LossDate > pCic.EndDate) Then
                If (pClaim.ReportedDate <= oGracePeriodEndDate OrElse pClaim.ReportedDate >= oGracePeriodEndDate) Then
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Function IsClaimReportedWithinGracePeriod(pCic As CertificateItemCoverage, pDealer As Dealer, pClaim As Claim) As Boolean
        If (pDealer.GracePeriodMonths > 0 OrElse pDealer.GracePeriodDays > 0) Then
            Dim oGracePeriodEndDate As Date = pCic.EndDate.AddMonths(pDealer.GracePeriodMonths).AddDays(pDealer.GracePeriodDays)

            If (pClaim.LossDate <= pCic.EndDate) Then
                If (pClaim.ReportedDate > oGracePeriodEndDate) Then
                    Return False
                End If
            End If
        End If
        Return True

    End Function

    Public Function IsClaimReportedWithinPeriod(pCic As CertificateItemCoverage, pDealer As Dealer, pClaim As Claim) As Boolean

        Dim pContract As Contract = DealerManager.GetContract(pDealer.DealerCode, pCic.Certificate.WarrantySalesDate)

        If Not pContract Is Nothing Then
            If (Not pContract.DaysToReportClaim Is Nothing) AndAlso (pContract.DaysToReportClaim.Value > 0) Then
                Dim intDaysReported As Integer = pClaim.ReportedDate.Subtract(pClaim.LossDate).Days
                If intDaysReported <= pContract.DaysToReportClaim.Value Then
                    Return True
                End If
            Else ' always return true if DaysToReportClaim is not specified in contract
                Return True
            End If
        End If
        Return False
    End Function

    Private Function IsMaxReplacementExceeded(pCic As CertificateItemCoverage, pClaim As Claim, pContract As Contract) As Boolean
        ' only valid for repair and replacement claim
        Return m_ClaimRepository.IsMaxReplacementsExceeded(pCic.Certificate.CertificateId, pClaim.MethodOfRepairId, pClaim.LossDate, pContract.ClaimLimitBasedOnId.ToCode(CommonManager, ListCodes.ClaimLimitBasedOn), pCic.Certificate.InsuranceActivationDate)

        'If Not (oMethodOfRepairCode = MethodofRepairCodes.Replacement _
        '   OrElse oMethodOfRepairCode = MethodofRepairCodes.Home _
        '   OrElse oMethodOfRepairCode = MethodofRepairCodes.CarryIn _
        '   OrElse oMethodOfRepairCode = MethodofRepairCodes.PickUp _
        '   OrElse oMethodOfRepairCode = MethodofRepairCodes.SendIn) Then
        '    Return False
        'End If

        'If (oContract.ClaimLimitBasedOnId.Equals(Guid.Empty) OrElse oContract.ClaimLimitBasedOnId Is Nothing) Then
        '    Return False
        'Else
        '    'Dim oClaimNumber As String
        '    'If blnExcludeSelf AndAlso (Not pClaim.ClaimNumber Is Nothing) Then
        '    '    oClaimNumber = ClaimNumber
        '    'End If
        '    Dim oCurrentLossDate As Date = Nothing
        '    Dim oReplacementBasedOn As String = oContract.ClaimLimitBasedOnId.ToCode(CommonManager, ListCodes.ReplacementBasedOn)
        '    If (oReplacementBasedOn = ReplacementBasedOnCodes.InsuranceActivationDate) Then

        '        If (Not pCic.Certificate.InsuranceActivationDate Is Nothing) Then
        '            oCurrentLossDate = Me.GetStartDateOf12MonthWindow(pCic.Certificate.InsuranceActivationDate.Value, pClaim.LossDate.Value)
        '        End If
        '    ElseIf (oReplacementBasedOn = ReplacementBasedOnCodes.DateOfLoss)
        '        Dim oLossDate As Date = m_ClaimRepository.GetFirstLossDate(pCic.Certificate.CertificateId)
        '    End If

        'End If

    End Function

    Private Function IsServiceLevelValid(pServiceCenterId As Guid,
                                         pRiskTypeId As Guid,
                                         pEffectiveDate As Date,
                                         pSalesPrice As Decimal,
                                         pServiceLevel As String) As Boolean
        ' only valid for repair and replacement claim
        Return m_ClaimRepository.IsServiceLevelValid(pServiceCenterId, pRiskTypeId, pEffectiveDate, pSalesPrice, pServiceLevel)

    End Function

    Private Function GetStartDateOf12MonthWindow(pStartDate As Date, pCurrentDate As Date) As Date
        Dim oTemp As Boolean = False
        If pCurrentDate <= pStartDate Then
            Return pStartDate
        Else
            Do Until (oTemp = True)
                If pCurrentDate >= pStartDate AndAlso pCurrentDate <= pStartDate.AddMonths(12).AddDays(-1) Then
                    oTemp = True
                    Exit Do
                Else
                    pStartDate = pStartDate.AddMonths(12)
                End If
            Loop
            Return pStartDate
        End If
    End Function

    Private Function CalculateOutstandingPremiumAmount(pDealer As Dealer, pCic As CertificateItemCoverage) As Decimal
        Dim outAmt As Decimal = 0
        Dim pContract As Contract = DealerManager.GetContract(pDealer.DealerCode, pCic.Certificate.WarrantySalesDate)
        If pContract.PAY_OUTSTANDING_PREMIUM_ID.ToCode(CommonManager, ListCodes.YesNo) = YesNoCodes.Yes Then
            Dim grossAmountReceived As Decimal
            Dim oBillingTotalAmount As Decimal

            grossAmountReceived = GetTotalGrossAmountReceived(pCic)
            oBillingTotalAmount = GetBillingTotals(pCic)
            outAmt = grossAmountReceived - oBillingTotalAmount
        End If
        Return outAmt
    End Function

    Private Function GetTotalGrossAmountReceived(pCic As CertificateItemCoverage) As Decimal

        Return pCic.Certificate.ItemCoverages.Sum(Function(cic) cic.GrossAmtReceived)
    End Function

    Private Function GetBillingTotals(pCic As CertificateItemCoverage) As Decimal

        Return pCic.Certificate.BillingDetails.Sum(Function(bd) bd.BilledAmount)
    End Function

    Private Function IsEquipmentRequired(pDealer As Dealer) As Boolean
        If (pDealer.UseEquipmentId.ToCode(CommonManager, ListCodes.YesNo) = YesNoCodes.Yes) Then
            Return True
        End If
        Return False

    End Function

    Public Function ValidateAndMatchClaimedEnrolledEquipments(pCic As CertificateItemCoverage, pDealer As Dealer, pClaim As Claim, ByRef pComment As Comment) As Boolean
        Dim retval As Boolean = True '
        If IsEquipmentRequired(pDealer) Then
            Dim claimedEquipment As ClaimEquipment = pClaim.ClaimEquipments.Where(Function(ce) ce.ClaimEquipmentTypeId.ToCode(CommonManager, ListCodes.ClaimEquipmentTypeCode) = ClaimEquipmentTypeCodes.Claimed).FirstOrDefault
            Dim enrolledEquipment As ClaimEquipment = pClaim.ClaimEquipments.Where(Function(ce) ce.ClaimEquipmentTypeId.ToCode(CommonManager, ListCodes.ClaimEquipmentTypeCode) = ClaimEquipmentTypeCodes.Enrolled).FirstOrDefault

            'Validate Claimed Equipment
            If Not claimedEquipment Is Nothing AndAlso claimedEquipment.ClaimEquipmentId.Equals(Guid.Empty) Then
                pComment.CommentTypeId = CommentTypeCodes.ClaimedEquipmentNotConfigured.ToGuid(ListCodes.CommentType, CommonManager)
                pComment.Comments &= Environment.NewLine & CommonManager.GetLabelTranslations("MSG_CLAIM_PENDING_CLAIMED_EQUIPMENT_NOT_RESOLVED").FirstOrDefault.Translation

                retval = False
                Return retval
            Else
                'Validate Enrolled Equipment
                If Not enrolledEquipment Is Nothing AndAlso enrolledEquipment.ClaimEquipmentId.Equals(Guid.Empty) Then
                    pComment.CommentTypeId = CommentTypeCodes.EnrolledEquipmentNotConfigured.ToGuid(ListCodes.CommentType, CommonManager)
                    pComment.Comments &= Environment.NewLine & CommonManager.GetLabelTranslations("MSG_CLAIM_PENDING_ENROLLED_EQUIPMENT_NOT_RESOLVED").FirstOrDefault.Translation

                    retval = False
                    Return retval
                Else
                    'Match Claimed and Enrolled
                    If IsEquipmentMisMatch(pCic, pDealer, pClaim) Then
                        pComment.CommentTypeId = CommentTypeCodes.MakeModelImeiMismatch.ToGuid(ListCodes.CommentType, CommonManager)
                        pComment.Comments &= Environment.NewLine & CommonManager.GetLabelTranslations("EQUIPMENT_MISMATCH").FirstOrDefault.Translation

                        retval = False
                        Return retval
                    End If
                End If
            End If
        End If


        Return retval

    End Function

    Private Function IsEquipmentMisMatch(pCic As CertificateItemCoverage, pDealer As Dealer, pClaim As Claim) As Boolean
        'when a mismatch found function returns true 
        Dim retVal As Boolean = False
        Dim enrolledEquipment As ClaimEquipment = pClaim.ClaimEquipments.Where(Function(ce) ce.ClaimEquipmentTypeId.ToCode(CommonManager, ListCodes.ClaimEquipmentTypeCode) = ClaimEquipmentTypeCodes.Enrolled).FirstOrDefault
        Dim claimedEquipment As ClaimEquipment = pClaim.ClaimEquipments.Where(Function(ce) ce.ClaimEquipmentTypeId.ToCode(CommonManager, ListCodes.ClaimEquipmentTypeCode) = ClaimEquipmentTypeCodes.Claimed).FirstOrDefault

        If IsEquipmentRequired(pDealer) AndAlso Not enrolledEquipment Is Nothing AndAlso enrolledEquipment.EquipmentId.Equals(Guid.Empty) Then
            If (pDealer.UseEquipmentId.ToCode(CommonManager, ListCodes.YesNo) = YesNoCodes.Yes) Then
                If Not enrolledEquipment.EquipmentId = claimedEquipment.EquipmentId OrElse
                        Not enrolledEquipment.SerialNumber = claimedEquipment.SerialNumber Then
                    Return True
                End If
            End If
        End If

        Return False
    End Function

    Private Function GetDeductible(pCic As CertificateItemCoverage,
                                   pMethodofRepairId As Guid,
                                   pHasCoverageChanged As Boolean) As DeductibleType
        Dim returnValue As New DeductibleType(CommonManager)
        returnValue.DeductibleBasedOnId = DeductibleBasedOnCodes.Fixed.ToGuid(ListCodes.DeductibleBasedOn, CommonManager)
        returnValue.Deductible = 0
        returnValue.ExpressionId = Nothing

        Dim deductible As Decimal = 0D
        Dim deductibleBasedOnId As Guid = Guid.Empty
        Dim ocertItemCoverageDed As CertificateItemCoverageDeductible

        ''''reload certificate if the coverage has changed
        If (pHasCoverageChanged) Then
            Dim oCert As Certificate = m_CertificateManager.GetCertifcateByItemCoverage(pCic.CertItemCoverageId)
            Dim oCic As CertificateItemCoverage = oCert.Item.Where(Function(i) i.Id = pCic.Item.Id).First.ItemCoverages.Where(Function(c) c.CertItemCoverageId = pCic.CertItemCoverageId).FirstOrDefault()
            ocertItemCoverageDed = oCic.ItemCoverageDeductibles.Where(Function(cicd) cicd.MethodOfRepairId = pMethodofRepairId).FirstOrDefault()
        Else
            ocertItemCoverageDed = pCic.ItemCoverageDeductibles.Where(Function(cicd) cicd.MethodOfRepairId = pMethodofRepairId).FirstOrDefault()
        End If

        If (pMethodofRepairId.ToCode(CommonManager, ListCodes.MethodOfRepair) <> MethodofRepairCodes.Recovery) Then

            If (ocertItemCoverageDed Is Nothing) Then

                If (Not pCic Is Nothing) Then

                    returnValue.DeductibleBasedOnId = pCic.DeductibleBasedOnId

                    If (returnValue.DeductibleBasedOn = DeductibleBasedOnCodes.Fixed) Then
                        If (pCic.Deductible <= 0) Then
                            returnValue.Deductible = 0
                        Else
                            returnValue.Deductible = pCic.Deductible
                        End If
                    ElseIf returnValue.DeductibleBasedOn = DeductibleBasedOnCodes.Expression Then
                        returnValue.Deductible = 0
                        returnValue.ExpressionId = pCic.DeductibleExpressionId
                    ElseIf returnValue.DeductibleBasedOn = DeductibleBasedOnCodes.ComputedExternally Then
                        returnValue.Deductible = 0
                        returnValue.ExpressionId = Nothing
                    Else
                        If (pCic.DeductiblePercent Is Nothing) Then
                            returnValue.Deductible = 0
                        Else
                            returnValue.Deductible = pCic.DeductiblePercent
                        End If
                    End If
                End If
            Else
                returnValue.DeductibleBasedOnId = ocertItemCoverageDed.DeductibleBasedOnId
                returnValue.Deductible = ocertItemCoverageDed.Deductible
                returnValue.ExpressionId = ocertItemCoverageDed.DeductibleExpressionId
            End If
        End If

        Return returnValue

    End Function

    Private Function HasIssues(pCic As CertificateItemCoverage, pClaim As Claim) As Boolean
        Dim oclaim As Claim = GetClaim(pClaim.ClaimNumber.ToUpperInvariant, pCic.Certificate.CompanyId)
        For Each ei As EntityIssue In oclaim.EntityIssues
            For Each cis1 As ClaimIssueStatus In ei.ClaimIssueStatuses
                If (cis1.ClaimIssueStatusCId.ToCode(CommonManager, ListCodes.ClaimIssueStatusCode) = ClaimIssueStatusCodes.Open) Then
                    Return True
                End If
            Next
        Next

        Return False
    End Function

    Private Function GetClaimAuthTypeCode(pAuthType As String) As Guid?
        If pAuthType = ClaimAuthorizationTypes.Single_Auth Then
            Return ClaimAuthorizationTypes.Single_Auth.ToGuid(ListCodes.ClaimAuthorizationTypeCode, CommonManager)
        ElseIf pAuthType = ClaimAuthorizationTypes.Multi_Auth
            Return ClaimAuthorizationTypes.Multi_Auth.ToGuid(ListCodes.ClaimAuthorizationTypeCode, CommonManager)
        End If

    End Function

    Private Function GetPriceList(pCic As CertificateItemCoverage,
                                      pServiceCenterCode As String,
                                  pServiceLevelCode As String,
                                  pServiceClassCode As String,
                                  pServiceTypeCode As String,
                                  pClaim As Claim,
                                  pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord)

        Dim oEquipmentConditionId As Guid?
        Dim oEquipmentId As Guid?
        Dim oEquipmentClassId As Guid?
        GetEquipmentInfoForPriceList(pCic, pClaim, oEquipmentConditionId, oEquipmentId, oEquipmentClassId)

        Return m_ClaimRepository.GetPriceList(pCic.Certificate.CompanyId,
                            pServiceCenterCode,
                            DateTime.Today,
                            pCic.Certificate.SalesPrice,
                            pCic.Item.RiskTypeId,
                            oEquipmentClassId,
                            oEquipmentId,
                            oEquipmentConditionId,
                            pCic.Certificate.DealerId,
                            pServiceLevelCode,
                            pServiceClassCode,
                            pServiceTypeCode,
                            m_CurrencyManager.GetCurrency(m_CountryManager.GetCountry(pCic.Certificate.CountryPurchaseId).PrimaryCurrencyId).Code,
                            pCurrencyConversionDate)


    End Function

    Private Function GetPriceListByMethodOfRepair(pCic As CertificateItemCoverage,
                                      pServiceCenterCode As String,
                                      pServiceLevelCode As String,
                                      pMethodOfRepairCode As String,
                                      pClaim As Claim) As Decimal

        Dim oEquipmentConditionId As Guid?
        Dim oEquipmentId As Guid?
        Dim oEquipmentClassId As Guid?

        'Dim oPriceList As IEnumerable(Of PriceListDetailRecord) = GetPriceList(pCic,
        '                                                                       pServiceCenterCode,
        '                                                                       pServiceLevelCode,
        '                                                                       pServiceClassCode,
        '                                                                       pServiceTypeCode,
        '                                                                       pClaim,
        '                                                                       DateTime.Today.AddDays(15))
        Dim oPriceList As IEnumerable(Of PriceListDetailRecord) = m_ClaimRepository.GetPriceListByMethodOfRepair(pMethodOfRepairCode.ToGuid(ListCodes.MethodOfRepair, CommonManager),
                                                                                                                 pCic.Certificate.CompanyId,
                                                                                                                 pServiceCenterCode, DateTime.Today,
                                                                                                                pCic.Certificate.SalesPrice,
                                                                                                                pCic.Item.RiskTypeId,
                                                                                                                oEquipmentClassId,
                                                                                                                oEquipmentId,
                                                                                                                oEquipmentConditionId,
                                                                                                                pCic.Certificate.DealerId,
                                                                                                                pServiceLevelCode,
                                                                                                                m_CurrencyManager.GetCurrency(m_CountryManager.GetCountry(pCic.Certificate.CountryPurchaseId).PrimaryCurrencyId).Code,
                                                                                                                DateTime.Today.AddDays(15))
        Dim pricelist As Decimal = Nothing

        If (Not oPriceList Is Nothing AndAlso oPriceList.Count > 0) Then
            Select Case pMethodOfRepairCode
                Case MethodofRepairCodes.Home
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                        p.ServiceTypeCode = ServiceTypeCodes.HomePrice).FirstOrDefault.Price
                Case MethodofRepairCodes.CarryIn
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                        p.ServiceTypeCode = ServiceTypeCodes.CarryInPrice).FirstOrDefault.Price
                Case MethodofRepairCodes.PickUp
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                        p.ServiceTypeCode = ServiceTypeCodes.PickUpPrice).FirstOrDefault.Price
                Case MethodofRepairCodes.SendIn
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                        p.ServiceTypeCode = ServiceTypeCodes.SendInPrice).FirstOrDefault.Price
                Case MethodofRepairCodes.Replacement
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Replacement AndAlso
                                                        p.ServiceTypeCode = ServiceTypeCodes.ReplacementPrice).FirstOrDefault.Price
                Case MethodofRepairCodes.Labor
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                       p.ServiceTypeCode = ServiceTypeCodes.Labor).FirstOrDefault.Price

                Case Else
                    pricelist = Nothing

            End Select
        Else
            Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode)
        End If

        Return pricelist
    End Function

    Public Function GetPriceListByServiceType(pCic As CertificateItemCoverage,
                                      pServiceCenterCode As String,
                                      pServiceLevelCode As String,
                                      pServiceClassCode As String,
                                      pServiceTypeCode As String,
                                      pClaim As Claim,
                                      pCurrencyConversionDate As Date) As Decimal

        Dim oPriceList As IEnumerable(Of PriceListDetailRecord) = GetPriceList(pCic,
                                                                               pServiceCenterCode,
                                                                               pServiceLevelCode,
                                                                               pServiceClassCode,
                                                                               pServiceTypeCode,
                                                                               pClaim,
                                                                               pCurrencyConversionDate)


        Dim pricelist As Decimal = 0D

        If (Not oPriceList Is Nothing AndAlso oPriceList.Count > 0) Then
            Select Case pServiceTypeCode
                Case ServiceTypeCodes.HomePrice
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                        p.ServiceTypeCode = ServiceTypeCodes.HomePrice).FirstOrDefault.Price
                Case ServiceTypeCodes.CarryInPrice
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                        p.ServiceTypeCode = ServiceTypeCodes.CarryInPrice).FirstOrDefault.Price
                Case ServiceTypeCodes.PickUpPrice
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                        p.ServiceTypeCode = ServiceTypeCodes.PickUpPrice).FirstOrDefault.Price
                Case ServiceTypeCodes.SendInPrice
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                        p.ServiceTypeCode = ServiceTypeCodes.SendInPrice).FirstOrDefault.Price
                Case ServiceTypeCodes.ReplacementPrice
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Replacement AndAlso
                                                        p.ServiceTypeCode = ServiceTypeCodes.ReplacementPrice).FirstOrDefault.Price
                Case ServiceTypeCodes.Labor
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                       p.ServiceTypeCode = ServiceTypeCodes.Labor).FirstOrDefault.Price
                Case ServiceTypeCodes.EstimatePrice
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                       p.ServiceTypeCode = ServiceTypeCodes.EstimatePrice).FirstOrDefault.Price
                Case ServiceTypeCodes.Logistics
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                       p.ServiceTypeCode = ServiceTypeCodes.Logistics).FirstOrDefault.Price
                Case ServiceTypeCodes.DeductibleBasePrice
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Deductible AndAlso
                                                       p.ServiceTypeCode = ServiceTypeCodes.DeductibleBasePrice).FirstOrDefault.Price
                Case ServiceTypeCodes.Parts
                    pricelist = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                                       p.ServiceTypeCode = ServiceTypeCodes.Parts).FirstOrDefault.Price

                Case Else
                    pricelist = 0D

            End Select

        End If

        Return pricelist
    End Function

    Private Function GetPriceListByMakeAndModel(pCic As CertificateItemCoverage,
                                                pServiceCenterCode As String,
                                                pServiceLevelCode As String,
                                                pServiceClassCode As String,
                                                pServiceTypeCode As String,
                                                pCompanyCode As String,
                                                pRiskTypeCode As String,
                                                pMake As String,
                                                pModel As String,
                                                pClaim As Claim,
                                                pDealerCode As String,
                                                pLowPrice As Decimal?,
                                                pHighPrice As Decimal?,
                                                pCurrencyConversionDate As Date,
                                                pCommonManager As CommonManager) As IEnumerable(Of PriceListDetailRecord)

        Dim oEquipmentConditionId As Guid?
        Dim oEquipmentId As Guid?
        Dim oEquipmentClassId As Guid?
        GetEquipmentInfoForPriceList(pCic, pClaim, oEquipmentConditionId, oEquipmentId, oEquipmentClassId)


        Return m_ClaimRepository.GetPriceListByMakeAndModel(DateTime.Today,
                                                            pClaim.ClaimNumber,
                                                            pCompanyCode,
                                                            pServiceCenterCode,
                                                            pRiskTypeCode,
                                                            oEquipmentClassId.ToCode(pCommonManager, ListCodes.EquipmentClassCode),
                                                            oEquipmentId,
                                                            oEquipmentConditionId,
                                                            pDealerCode,
                                                            pServiceClassCode,
                                                            pServiceTypeCode,
                                                            pMake,
                                                            pModel,
                                                            pLowPrice,
                                                            pHighPrice,
                                                            pServiceLevelCode,
                                                            m_CurrencyManager.GetCurrency(m_CountryManager.GetCountry(pCic.Certificate.CountryPurchaseId).PrimaryCurrencyId).Code,
                                                            pCurrencyConversionDate)


    End Function

    Private Function GetAuthAmountByLaborAndParts(pCic As CertificateItemCoverage,
                                                  pCompanyCode As String,
                                                  pDealerCode As String,
                                                  pServiceCenterCode As String,
                                                  pServiceLevelCode As String,
                                                  pServiceClassCode As String,
                                                  pServiceTypeCode As String,
                                                  pClaim As Claim,
                                                  pCurrencyConversionDate As Date,
                                                  pRiskTypeCode As String,
                                                  pMake As String,
                                                  pModel As String,
                                                  pLowPrice As Decimal,
                                                  pHighPrice As Decimal,
                                                  pCommonManger As CommonManager) As Decimal


        Dim laborAmount As Decimal = 0D
        Dim partsAmount As Decimal = 0D
        Dim priceListDetailRecord As PriceListDetailRecord?

        Try

            '''''Get the Labor Amount
            Dim oPriceList As IEnumerable(Of PriceListDetailRecord) = GetPriceList(pCic,
                                                                               pServiceCenterCode,
                                                                               pServiceLevelCode,
                                                                               ServiceClassCodes.Repair,
                                                                               ServiceTypeCodes.Labor,
                                                                               pClaim,
                                                                               pCurrencyConversionDate)
            '''''Get the Parts Amount
            Dim oPriceListbyMakeAndModel As IEnumerable(Of PriceListDetailRecord) = GetPriceListByMakeAndModel(pCic,
                                                                                                                pServiceCenterCode,
                                                                                                               pServiceLevelCode,
                                                                                                               ServiceClassCodes.Repair,
                                                                                                               ServiceTypeCodes.Parts,
                                                                                                               pCompanyCode,
                                                                                                               pRiskTypeCode,
                                                                                                               pMake,
                                                                                                               pModel,
                                                                                                               pClaim,
                                                                                                               pDealerCode,
                                                                                                               m_PriceListLowPrice,
                                                                                                               m_PriceListHighPrice,
                                                                                                               pCurrencyConversionDate,
                                                                                                               pCommonManger)

            If (Not oPriceList Is Nothing AndAlso oPriceList.Count > 0) Then
                '''''Get the Labor amount
                priceListDetailRecord = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                       p.ServiceTypeCode = ServiceTypeCodes.Labor).FirstOrDefault

                If (Not priceListDetailRecord Is Nothing) Then
                    laborAmount = oPriceList.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                       p.ServiceTypeCode = ServiceTypeCodes.Labor).FirstOrDefault.Price
                End If
            Else
                Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Labor Amount not configured for Service Center: " + pServiceCenterCode)
            End If

            If (Not oPriceListbyMakeAndModel Is Nothing AndAlso oPriceListbyMakeAndModel.Count > 0) Then
                '''''Get the Parts amount
                priceListDetailRecord = Nothing
                priceListDetailRecord = oPriceListbyMakeAndModel.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                       p.ServiceTypeCode = ServiceTypeCodes.Parts).FirstOrDefault

                If (Not priceListDetailRecord Is Nothing) Then
                    partsAmount = oPriceListbyMakeAndModel.Where(Function(p) p.ServiceClassCode = ServiceClassCodes.Repair AndAlso
                                       p.ServiceTypeCode = ServiceTypeCodes.Parts).FirstOrDefault.Price
                End If
            Else
                Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Parts Amount not configured for Service Center: " + pServiceCenterCode)
            End If


            Return laborAmount + partsAmount

        Catch ex As InvalidOperationException
            Throw New PriceListNotConfiguredException(Guid.Empty, pServiceCenterCode, "Exchange Rate not configured for Dealer:" + pDealerCode)
        End Try

    End Function

    Private Sub GetEquipmentInfoForPriceList(pCic As CertificateItemCoverage, pClaim As Claim, ByRef oEquipmentConditionId As Guid?, ByRef oEquipmentId As Guid?, ByRef oEquipmentClassId As Guid?)
        If (pCic.Certificate.GetDealer(DealerManager).UseEquipmentId.ToCode(CommonManager, ListCodes.YesNo) = YesNoCodes.Yes) Then

            oEquipmentConditionId = EquipmentConditionCodes.[New].ToGuid(ListCodes.EquipmentType, CommonManager)
            If (pClaim.ClaimEquipments.Count > 0) Then
                If (pClaim.ClaimEquipments.FirstOrDefault.ClaimEquipmentTypeId.ToCode(CommonManager, ListCodes.ClaimEquipmentTypeCode) <> ClaimEquipmentTypeCodes.Claimed) Then
                    AddEquipment(pCic, pClaim, ClaimEquipmentTypeCodes.Claimed)
                End If
                If (pClaim.ClaimEquipments.FirstOrDefault.ClaimEquipmentTypeId.ToCode(CommonManager, ListCodes.ClaimEquipmentTypeCode).Count > 0) Then
                    oEquipmentId = pClaim.ClaimEquipments.ElementAt(1).EquipmentId
                    'pClaim.ClaimEquipments.Where(Function(ce) ce.ClaimEquipmentTypeId.ToCode(CommonManager, ListCodes.ClaimEquipmentTypeCode) = ClaimEquipmentTypeCodes.Claimed).First.EquipmentId
                    If (Not oEquipmentId Is Nothing) Then
                        oEquipmentClassId = m_EquipmentManager.GetEquipment(pClaim.ClaimEquipments.ElementAt(0).EquipmentId).EquipmentClassId
                    End If
                End If
            End If

        End If
        ''''original code from Claim.vb - GetRePairPricesByMethodOfRepair method
        ''''If (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.Dealer.UseEquipmentId) = Codes.YESNO_Y) Then
        ''''    'equipConditionid = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW) 'sending condition type as 'NEW'
        ''''    'If Me.ClaimedEquipment Is Nothing OrElse Me.ClaimedEquipment.EquipmentBO Is Nothing Then
        ''''    '    Me.CreateClaimedEquipment(Me.CertificateItem.CopyEnrolledEquip_into_ClaimedEquip())
        ''''    'End If

        ''''    If Not Me.ClaimedEquipment Is Nothing AndAlso Not Me.ClaimedEquipment.EquipmentBO Is Nothing Then
        ''''        equipmentId = Me.ClaimedEquipment.EquipmentId
        ''''        equipClassId = Me.ClaimedEquipment.EquipmentBO.EquipmentClassId
        ''''        dv = PriceListDetail.GetRepairPricesforMethodofRepair(Me.MethodOfRepairId, Me.CompanyId, servCenter.Code, Me.RiskTypeId, DateTime.Now,
        ''''                   Me.Certificate.SalesPrice.Value, equipClassId, equipmentId, equipConditionid, Me.Dealer.Id, String.Empty)
        ''''    End If
        ''''Else
        ''''    dv = PriceListDetail.GetRepairPricesforMethodofRepair(Me.MethodOfRepairId, Me.CompanyId, servCenter.Code, Me.RiskTypeId, DateTime.Now,
        ''''                   Me.Certificate.SalesPrice.Value, equipClassId, equipmentId, equipConditionid, Me.Dealer.Id, String.Empty)
        ''''End If
    End Sub

    Private Function GetListPrice(pCic As CertificateItemCoverage, pDealer As Dealer, pClaim As Claim) As Decimal
        'Private Function GetListPrice(ByVal pSkuNumber As String,
        '                              ByVal pDealer As Dealer,
        '                              ByVal pClaimEquipmentSku As String,
        '                              ByVal pLossDate As Date) As Decimal
        Dim oSku As String = String.Empty
        Dim oPrice As Decimal = 0D
        If (IsEquipmentRequired(pDealer)) Then
            oSku = pClaim.ClaimEquipments.FirstOrDefault.Sku
        Else
            oSku = pCic.Item.SkuNumber
        End If

        Try
            Dim oWarrantyMaster As IEnumerable(Of WarrantyMaster) = DealerManager.GetWarranytMaster(pDealer.DealerId, oSku)

            If (oWarrantyMaster.Count > 0) Then
                Dim oListPrice As ListPrice = oWarrantyMaster.First.ListPrices.
                    Where(Function(lp) lp.AmountTypeId.ToCode(m_CommonManager, ListCodes.ListPriceAmountTypeCode) = ListPriceAmountTypeCodes.ListPrice AndAlso (Not (lp.Effective > pClaim.LossDate Or lp.Expiration < pClaim.LossDate))).FirstOrDefault
                oPrice = oListPrice.Amount
            End If


        Catch ex As Exception

        End Try
        Return oPrice
    End Function

    Public Sub InsertClaimExtendedStatus(pDealer As Dealer, pExtendedStatusCode As String, pExtendedStatusDate As Date, ByRef pClaim As Claim) Implements IClaimManager.InsertClaimExtendedStatus
        Dim oCompanyGroup As CompanyGroup
        oCompanyGroup = CompanyGroupManager.GetCompanyGroup(pDealer.GetCompany(CompanyManager).CompanyGroupId)

        Dim oClaimStatusByGroupId As Guid = oCompanyGroup.ClaimStatusByGroups.Where(Function(csg) csg.ListItemId =
                 CommonManager.GetListItems(ListCodes.ClaimExtendedStatusCode).Where(Function(li) li.Code = pExtendedStatusCode).First.ListItemId).First.ClaimStatusByGroupId

        If (Not oClaimStatusByGroupId.Equals(Guid.Empty)) Then
            pClaim.ClaimStatuses.Add(New ClaimStatus() With {
                        .ClaimStatusId = Guid.NewGuid(),
                        .ClaimId = pClaim.ClaimId,
                        .ClaimStatusByGroupId = oClaimStatusByGroupId,
                        .StatusDate = pExtendedStatusDate
                    })
        End If

    End Sub

    Public Sub AddDefaultExtendedStatus(ByRef pClaim As Claim, pDealer As Dealer, Action As String) Implements IClaimManager.AddDefaultExtendedStatus

        ''''Adding default extended status for New Claim
        If (Action = ClaimActions.NewClaim) Then
            Dim oCompGroup As CompanyGroup = CompanyGroupManager.GetCompanyGroup(pDealer.GetCompany(m_CompanyManager).CompanyGroupId)
            Try
                Dim oClaimStatusByGroupId As Guid = oCompGroup.DefaultClaimStatuses.Where(Function(d) d.DefaultTypeId.ToCode(CommonManager, ListCodes.ClaimExtendedStatusDefaultType) = ClaimExtendedStatusDefaultTypeCodes.NewClaim).FirstOrDefault.ClaimStatusByGroupId
                If (Not oClaimStatusByGroupId.Equals(Guid.Empty)) Then
                    'Dim oExtStatusId As Guid = oCompGroup.ClaimStatusByGroups.Where(Function(csg) csg.ClaimStatusByGroupId = oClaimStatusByGroupId).FirstOrDefault.ListItemId
                    If (Not oClaimStatusByGroupId.Equals(Guid.Empty)) Then
                        pClaim.ClaimStatuses.Add(New ClaimStatus() With {
                        .ClaimStatusId = Guid.NewGuid(),
                        .ClaimId = pClaim.ClaimId,
                        .ClaimStatusByGroupId = oClaimStatusByGroupId,
                        .StatusDate = DateTime.Today
                    })
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    Public Function GetListItemIdForCode(pListItemCode As String, pListCode As String) As Guid?
        Return pListItemCode.ToGuid(pListCode, CommonManager)

    End Function

    Private Function GetClaimsPaidAmountByCertificate(pCertificateId As Guid) As Decimal Implements IClaimManager.GetClaimsPaidAmountByCertificate
        '''''used in GW PIL Service
        Return m_ClaimRepository.GetClaimsPaidAmountByCertificate(pCertificateId)

    End Function

    Public Function GetCertificateClaimInfo(pCompanyCode As String, pCertificateNumber As String, pSerialNumber As String, pPhoneNumber As String, pUserId As Guid, pLanguageId As Guid, ByRef pErrorCode As String, ByRef pErrorMessage As String) As DataSet Implements IClaimManager.GetCertificateClaimInfo
        Throw New NotImplementedException()
    End Function

    Public Class DeductibleType
        Private oDeductible As Decimal
        Private oExpressionId As Guid?
        Private oDeductibleBasedOn As String
        Private oDeductibleBasedOnId As Guid
        Private ReadOnly m_CommonManager As ICommonManager

        Public Sub New(pCommonManager As ICommonManager)
            If (pCommonManager Is Nothing) Then
                Throw New ArgumentNullException("pCommonManager")
            End If

            m_CommonManager = pCommonManager
        End Sub

        Public ReadOnly Property CommonManager As ICommonManager
            Get
                Return m_CommonManager
            End Get
        End Property

        Public Property Deductible() As Decimal
            Get
                Return oDeductible
            End Get
            Set(value As Decimal)
                oDeductible = value
            End Set
        End Property

        Public ReadOnly Property DeductibleBasedOn() As String
            Get
                Return oDeductibleBasedOn
            End Get

        End Property

        Public Property DeductibleBasedOnId() As Guid
            Get
                Return oDeductibleBasedOnId
            End Get
            Set(value As Guid)
                oDeductibleBasedOnId = value
                oDeductibleBasedOn = DeductibleBasedOnId.ToCode(CommonManager, ListCodes.DeductibleBasedOn)
            End Set
        End Property

        Public Property ExpressionId As Guid?
            Get
                Return oExpressionId
            End Get
            Set(value As Guid?)
                oExpressionId = value
            End Set
        End Property
    End Class

End Class
Public Class ClaimActions
    Public Const NewClaim As String = "New"
    Public Const Update As String = "Update"
End Class