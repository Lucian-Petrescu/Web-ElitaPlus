Imports Assurant.ElitaPlus.DataEntities

Public Interface IClaimManager


#Region "GOOW SERVICE"

    Function CreateClaim(ByVal pCic As CertificateItemCoverage,
                         ByVal pClaim As Claim,
                         ByVal pDealer As Dealer,
                         ByVal pCoverageTypeCode As String,
                         ByVal pServiceCenterCode As String,
                         ByVal pClaimType As Integer,
                         ByVal pMake As String,
                         ByVal pModel As String,
                         ByVal pComments As String,
                         ByVal pDamageType As String)

    Sub DenyClaim(ByVal pRepairClaim As Claim,
                  ByVal pCert As Certificate,
                  ByVal pComments As String)

    Sub ChangeServiceCenter(ByVal pClaim As Claim,
                            ByVal pCert As Certificate,
                            ByVal pCompany As Company,
                            ByVal pComments As String,
                            ByVal pServiceCenterCode As String)

    Sub FulfillClaim(ByVal pClaim As Claim,
                            ByVal pCert As Certificate,
                            ByVal pCompany As Company,
                            ByVal pDealerCode As String,
                            ByVal pComments As String,
                            ByVal pRepairDate As Date,
                            ByVal pClaimType As Int64,
                            ByVal pSerialNumber As String,
                            ByVal pModel As String,
                            ByVal pMake As String,
                            ByVal pServiceCenterCode As String,
                            ByVal pTrackingNumber As String)
    Sub PayClaim(ByVal pClaim As Claim,
                            ByVal pCert As Certificate,
                            ByVal pCompany As Company,
                            ByVal pServiceCenterCode As String,
                            ByVal pInvoiceNumber As String,
                            ByVal pPaymentAmount As Decimal)

    Sub ReturnDamagedDeviceAdvEx(pClaim As Claim,
                                 ByVal pCert As Certificate,
                                 ByVal pCompany As Company,
                                 ByVal pDealer As Dealer,
                                 ByVal pComments As String,
                                 ByVal pCoverageTypeCode As String)

    Sub MaxDaysElapsedAdEx(pClaim As Claim,
                                 ByVal pCert As Certificate,
                                 ByVal pCompany As Company,
                                 ByVal pDealer As Dealer,
                                 ByVal pComments As String)

    Function ComputeTax(ByVal pAmount As Decimal,
                               ByVal pDealerId As Nullable(Of Guid),
                               ByVal pCountryId As Nullable(Of Guid),
                               ByVal pCompanyTypeId As Nullable(Of Guid),
                               ByVal pTaxTypeId As Nullable(Of Guid),
                               ByVal pRegionId As Nullable(Of Guid),
                               ByVal pExpectedPremiumIsWpId As Nullable(Of Guid),
                               ByVal pProductTaxTypeId As Nullable(Of Guid),
                               ByVal pSalesDate As Date) As Decimal

    Function GetClaimInfo(ByVal pClaim As Claim,
                          ByVal pDealer As Dealer,
                          ByVal pCertificate As Certificate)

#End Region


    Function LocateCoverage(ByVal pCertificate As Certificate,
                            ByVal pDateOfLoss As Date,
                            ByVal pCoverageTypeCode As String,
                            ByVal pItemNumber As Nullable(Of Short)) As CertificateItemCoverage


    Function CreateClaim(ByVal pCic As CertificateItemCoverage,
                         ByVal pClaim As Claim,
                         ByVal pDealer As Dealer,
                         ByVal pCoverageTypeCode As String,
                         ByVal pServiceCenterCode As String,
                         ByVal pClaimType As Integer,
                         ByVal pMake As String,
                         ByVal pModel As String)

    Function GetClaim(ByVal pClaimNUmber As String, pCompanyId As Guid) As Claim

    Function SaveClaim(ByVal pClaim As Claim) As Claim

    Sub UpdateRepairableClaim(ByVal pRepairClaim As Claim,
                              ByVal pCert As Certificate,
                              ByVal pCompany As Company,
                              ByVal pCoverageTypeCode As String,
                              ByVal pServiceCenterCode As String,
                              ByVal pServiceLevelCode As String,
                              ByVal pHasCoverageChanged As Boolean,
                              ByVal pHasSvcCenterChanged As Boolean,
                              ByVal pIsServiceWarrantyClaim As Boolean,
                              ByVal pMake As String,
                              ByVal pModel As String,
                              ByVal pSerialNumberReplacedDevice As String,
                              ByVal pAuthorizedAmount As Nullable(Of Decimal),
                              ByVal pClaimBasicUpdateFields As ClaimBasicUpdateFields)

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

    Sub UpdateClaimReplacedWithRefubished(ByVal pRepairClaim As Claim,
                                ByVal pCert As Certificate,
                                ByVal pCompany As Company,
                                ByVal pCoverageTypeCode As String,
                                ByVal pServiceCenterCode As String,
                                ByVal pServiceLevelCode As String,
                                ByVal pHasCoverageChanged As Boolean,
                                ByVal pHasSvcCenterChanged As Boolean,
                                ByVal pIsServiceWarrantyClaim As Boolean,
                                ByVal pAuthorizedAmount As Nullable(Of Decimal),
                                ByVal pSimCardAmount As Nullable(Of Decimal),
                                ByVal pSerialNumberReplacedDevice As String,
                                ByVal pModel As String,
                                ByVal pManufacturer As String,
                                ByVal pUpdateAction As String,
                                ByVal pDeviceCondition As DeviceConditionEnum,
                                ByVal pLossType As String,
                                ByVal pClaimBasicUpdateFields As ClaimBasicUpdateFields)

    Sub UpdateTheftClaim(ByVal pReplacementClaim As Claim,
                                       ByVal pCert As Certificate,
                                       ByVal pCompany As Company,
                                       ByVal pHasSvcCenterChanged As Boolean,
                                       ByVal pCoverageTypeCode As String,
                                       ByVal pServiceCenterCode As String,
                                       ByVal pServiceLevelCode As String,
                                       ByVal pAuthorizedAmount As Nullable(Of Decimal),
                                       ByVal pSerialNumberReplacedDevice As String,
                                       ByVal pModel As String,
                                       ByVal pManufacturer As String,
                                       ByVal pCondition As IClaimManager.DeviceConditionEnum,
                                       ByVal pLossType As String,
                                       ByVal pSimCardAmount As Nullable(Of Decimal),
                                       ByVal pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields)

    Function IsCoverageChangeValid(ByVal pCertificate As Certificate, ByVal CoverageTypeCode As String, pDateOfLoss As Date) As Boolean

    Sub DenyClaim(ByVal pRepairClaim As Claim, ByVal pCert As Certificate, ByVal pCompany As Company, ByVal pClaimBasicUpdateFields As ClaimBasicUpdateFields)

    Sub UpdateClaimBasicFields(ByRef pClaim As Claim,
                               ByVal pCert As Certificate,
                               ByVal pCompany As Company,
                               ByVal pHasCoverageChanged As Boolean,
                               ByVal pCoverageTypeCode As String,
                               ByVal pHasSvcCenterChanged As Boolean,
                               ByVal pServiceCenterCode As String,
                               ByVal pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields)

    Function ValidateServiceCenter(ByVal pDealer As Dealer, ByVal pServiceCenterCode As String) As Boolean

    Function ValidateManufacturerByCompanyGroup(ByVal pNewMake As String, ByVal pCompanyId As Guid, ByVal pCertItemCoverageId As Guid) As Guid

    Sub InsertClaimExtendedStatus(ByVal pDealer As Dealer, ByVal pExtendedStatusCode As String, ByVal pExtendedStatusDate As Date, ByRef pClaim As Claim)

    Sub AddDefaultExtendedStatus(ByRef pClaim As Claim, ByVal pDealer As Dealer, ByVal Action As String)

    Function CreateRepairReplacmentClaim(ByVal pCertificate As Certificate, ByVal pRepairClaim As Claim) As Claim

    Function CreateServiceWarrantyClaim(ByVal pCertificate As Certificate, ByVal pRepairClaim As Claim) As Claim

    Function GetClaimsPaidAmountByCertificate(ByVal pCertificateId As Guid) As Decimal

    'US 203685
    Function GetCertificateClaimInfo(ByVal pCompanyCode As String,
                                                       ByVal pCertificateNumber As String,
                                                       ByVal pSerialNumber As String,
                                                       ByVal pPhoneNumber As String,
                                                       ByVal pUserId As Guid,
                                                       ByVal pLanguageId As Guid,
                                                       ByRef pErrorCode As String,
                                                       ByRef pErrorMessage As String) As DataSet
    Enum DeviceConditionEnum
        [New] = 1
        Refurbished = 2
    End Enum

    Class ExtendedStatus
        Public Property StatusDate As Nullable(Of Date)
        Public Property Code As String
    End Class

    Class ClaimBasicUpdateFields
        Public Property RepairDate As Nullable(Of Date)
        Public Property TechnicalReport As String
        Public Property Specialnstructions As String
        Public Property AuthNumber As String
        Public Property ProblemDescription As String
        Public Property ExtendedStatuses As List(Of IClaimManager.ExtendedStatus)

        Public Property Make As String

        Public Property Model As String
    End Class

End Interface
