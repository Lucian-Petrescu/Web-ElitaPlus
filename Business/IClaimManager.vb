Imports Assurant.ElitaPlus.DataEntities

Public Interface IClaimManager


#Region "GOOW SERVICE"

    Function CreateClaim(pCic As CertificateItemCoverage,
                         pClaim As Claim,
                         pDealer As Dealer,
                         pCoverageTypeCode As String,
                         pServiceCenterCode As String,
                         pClaimType As Integer,
                         pMake As String,
                         pModel As String,
                         pComments As String,
                         pDamageType As String)

    Sub DenyClaim(pRepairClaim As Claim,
                  pCert As Certificate,
                  pComments As String)

    Sub ChangeServiceCenter(pClaim As Claim,
                            pCert As Certificate,
                            pCompany As Company,
                            pComments As String,
                            pServiceCenterCode As String)

    Sub FulfillClaim(pClaim As Claim,
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
                            pTrackingNumber As String)
    Sub PayClaim(pClaim As Claim,
                            pCert As Certificate,
                            pCompany As Company,
                            pServiceCenterCode As String,
                            pInvoiceNumber As String,
                            pPaymentAmount As Decimal)

    Sub ReturnDamagedDeviceAdvEx(pClaim As Claim,
                                 pCert As Certificate,
                                 pCompany As Company,
                                 pDealer As Dealer,
                                 pComments As String,
                                 pCoverageTypeCode As String)

    Sub MaxDaysElapsedAdEx(pClaim As Claim,
                                 pCert As Certificate,
                                 pCompany As Company,
                                 pDealer As Dealer,
                                 pComments As String)

    Function ComputeTax(pAmount As Decimal,
                               pDealerId As Guid?,
                               pCountryId As Guid?,
                               pCompanyTypeId As Guid?,
                               pTaxTypeId As Guid?,
                               pRegionId As Guid?,
                               pExpectedPremiumIsWpId As Guid?,
                               pProductTaxTypeId As Guid?,
                               pSalesDate As Date) As Decimal

    Function GetClaimInfo(pClaim As Claim,
                          pDealer As Dealer,
                          pCertificate As Certificate)

#End Region


    Function LocateCoverage(pCertificate As Certificate,
                            pDateOfLoss As Date,
                            pCoverageTypeCode As String,
                            pItemNumber As Short?) As CertificateItemCoverage


    Function CreateClaim(pCic As CertificateItemCoverage,
                         pClaim As Claim,
                         pDealer As Dealer,
                         pCoverageTypeCode As String,
                         pServiceCenterCode As String,
                         pClaimType As Integer,
                         pMake As String,
                         pModel As String)

    Function GetClaim(pClaimNUmber As String, pCompanyId As Guid) As Claim

    Function SaveClaim(pClaim As Claim) As Claim

    Sub UpdateRepairableClaim(pRepairClaim As Claim,
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
                              pClaimBasicUpdateFields As ClaimBasicUpdateFields)

    'Sub UpdateIrRepairableClaim(ByVal pRepairClaim As Claim,
    '                            ByVal pCert As Certificate,
    '                            ByVal pCompany As Company,
    '                            ByVal pCoverageTypeCode As String,
    '                            ByVal pServiceCenterCode As String,
    '                            ByVal pServiceLevelCode As String,
    '                            ByVal pHasCoverageChanged As Boolean,
    '                            ByVal pHasSvcCenterChanged As Boolean,
    '                            ByVal pIsServiceWarrantyClaim As Boolean,
    '                            ByVal pAuthorizedAmount As Nullable(Of Decimal),
    '                            ByVal pSimCardAmount As Nullable(Of Decimal),
    '                            ByVal pSerialNumberReplacedDevice As String,
    '                            ByVal pModel As String,
    '                            ByVal pManufacturer As String,
    '                            ByVal pDeviceCondition As DeviceConditionEnum,
    '                            ByVal pLossType As String,
    '                            ByVal pClaimBasicUpdateFields As ClaimBasicUpdateFields)

    Sub UpdateClaimReplacedWithRefubished(pRepairClaim As Claim,
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
                                pDeviceCondition As DeviceConditionEnum,
                                pLossType As String,
                                pClaimBasicUpdateFields As ClaimBasicUpdateFields)

    Sub UpdateTheftClaim(pReplacementClaim As Claim,
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
                                       pCondition As DeviceConditionEnum,
                                       pLossType As String,
                                       pSimCardAmount As Decimal?,
                                       pClaimBasicUpdateFields As ClaimBasicUpdateFields)

    Function IsCoverageChangeValid(pCertificate As Certificate, CoverageTypeCode As String, pDateOfLoss As Date) As Boolean

    Sub DenyClaim(pRepairClaim As Claim, pCert As Certificate, pCompany As Company, pClaimBasicUpdateFields As ClaimBasicUpdateFields)

    Sub UpdateClaimBasicFields(ByRef pClaim As Claim,
                               pCert As Certificate,
                               pCompany As Company,
                               pHasCoverageChanged As Boolean,
                               pCoverageTypeCode As String,
                               pHasSvcCenterChanged As Boolean,
                               pServiceCenterCode As String,
                               pClaimBasicUpdateFields As ClaimBasicUpdateFields)

    Function ValidateServiceCenter(pDealer As Dealer, pServiceCenterCode As String) As Boolean

    Function ValidateManufacturerByCompanyGroup(pNewMake As String, pCompanyId As Guid, pCertItemCoverageId As Guid) As Guid

    Sub InsertClaimExtendedStatus(pDealer As Dealer, pExtendedStatusCode As String, pExtendedStatusDate As Date, ByRef pClaim As Claim)

    Sub AddDefaultExtendedStatus(ByRef pClaim As Claim, pDealer As Dealer, Action As String)

    Function CreateRepairReplacmentClaim(pCertificate As Certificate, pRepairClaim As Claim) As Claim

    Function CreateServiceWarrantyClaim(pCertificate As Certificate, pRepairClaim As Claim) As Claim

    Function GetClaimsPaidAmountByCertificate(pCertificateId As Guid) As Decimal

    'US 203685
    Function GetCertificateClaimInfo(pCompanyCode As String,
                                                       pCertificateNumber As String,
                                                       pSerialNumber As String,
                                                       pPhoneNumber As String,
                                                       pUserId As Guid,
                                                       pLanguageId As Guid,
                                                       ByRef pErrorCode As String,
                                                       ByRef pErrorMessage As String) As DataSet
    Enum DeviceConditionEnum
        [New] = 1
        Refurbished = 2
    End Enum

    Class ExtendedStatus
        Public Property StatusDate As Date?
        Public Property Code As String
    End Class

    Class ClaimBasicUpdateFields
        Public Property RepairDate As Date?
        Public Property TechnicalReport As String
        Public Property Specialnstructions As String
        Public Property AuthNumber As String
        Public Property ProblemDescription As String
        Public Property ExtendedStatuses As List(Of ExtendedStatus)

        Public Property Make As String

        Public Property Model As String
    End Class

End Interface
