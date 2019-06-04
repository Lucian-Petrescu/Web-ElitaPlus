Imports Assurant.ElitaPlus.DataEntities

Public Interface IClaimRepository(Of TEntity As {BaseEntity, IClaimEntity})
    Inherits IRepository(Of TEntity)

    Function GetPriceList(ByVal pCompanyId As Guid,
                          ByVal pServiceCenterCode As String,
                          ByVal pLookupDate As Date,
                          ByVal pSalesPrice As Decimal,
                          ByVal pRiskTypeId As Guid,
                          ByVal pEquipmentClassId As Nullable(Of Guid),
                          ByVal pEquipmentId As Nullable(Of Guid),
                          ByVal pEquipmentConditionId As Nullable(Of Guid),
                          ByVal pDealerId As Guid,
                          ByVal pServiceLevelCode As String,
                          ByVal pServiceClassCode As String,
                          ByVal pServiceTypeCode As String,
                          ByVal pCurrencyCode As String,
                          ByVal pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord)

    Function GetPriceListByMakeAndModel(ByVal pForceDate As Date,
                                                ByVal pClaimNumber As String,
                                                ByVal pCompanyCode As String,
                                                ByVal pServiceCenterCode As String,
                                                ByVal pRiskTypeCode As String,
                                                ByVal pEquipmentClassCode As String,
                                                ByVal pEquipmentId As Nullable(Of Guid),
                                                ByVal pEquipmentConditionId As Nullable(Of Guid),
                                                ByVal pDealerCode As String,
                                                ByVal pServiceClassCode As String,
                                                ByVal pServiceTypeCode As String,
                                                ByVal pMake As String,
                                                ByVal pModel As String,
                                                ByVal pLowPrice As Decimal,
                                                ByVal pHighPrice As Decimal,
                                                ByVal pServiceLevelCode As String,
                                                ByVal pCurrencyCode As String,
                                                ByVal pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord)

    Function GetNextClaimNumber(ByVal pCompanyId As Guid) As String

    Function GetProductRemainingLiabilityLimit(ByVal pCertificateId As Guid, ByVal pLossDate As Date) As Nullable(Of Decimal)

    Function GetRemainingCoverageLiabilityLimit(ByVal pCertItemCoverageId As Guid, ByVal pLossDate As Date) As Nullable(Of Decimal)

    Function IsMaxReplacementsExceeded(ByVal pCertificateId As Guid,
                                        ByVal pMethodOfRepairId As Guid,
                                        ByVal pLossDate As Date,
                                        ByVal pReplacementBasedOn As String,
                                        ByVal pInsuranceActivationDate As Date) As Boolean
    Function IsServiceLevelValid(ByVal pServiceCenterId As Guid,
                                       ByVal pRiskTypeId As Guid,
                                       ByVal pEffectiveDate As Date,
                                       ByVal pSalesPrice As Decimal,
                                       ByVal pServiceLevel As String) As Boolean

    Function GetClaimsPaidAmountByCertificate(ByVal pCertificateId As Guid) As Decimal

    Function ValidateManufacturerByCompanyGroup(ByVal pManufacturerDesc As String,
                                                      ByVal pCompanyId As Guid,
                                                      ByVal pCertItemCoverageId As Guid
                                                      ) As Guid

    Function GetPriceListByMethodOfRepair(ByVal pMethodofRepairId As Guid,
                                 ByVal pCompanyId As Guid,
                                 ByVal pServiceCenterCode As String,
                                 ByVal pLookupDate As Date,
                                 ByVal pSalesPrice As Decimal,
                                 ByVal pRiskTypeId As Guid,
                                 ByVal pEquipmentClassId As Nullable(Of Guid),
                                 ByVal pEquipmentId As Nullable(Of Guid),
                                 ByVal pEquipmentConditionId As Nullable(Of Guid),
                                 ByVal pDealerId As Guid,
                                 ByVal pServiceLevelCode As String,
                                 ByVal pCurrencyCode As String,
                                 ByVal pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord)

    Function ReturnAdvanceExchange(ByVal pCert As String,
                            ByVal pDealer As String,
                            ByVal pCoverageTypeCode As String) As String

    Function GetClaimCaseReserve(ByVal pClaimId As Guid) As Decimal

    Function GetCertificateClaimInfo(ByVal pCompanyCode As String,
                                    ByVal pCertificateNumber As String,
                                    ByVal pSerialNumber As String,
                                    ByVal pPhoneNumber As String,
                                    ByVal pUserId As Guid,
                                    ByVal pLanguageId As Guid,
                                    ByRef pErrorCode As String,
                                    ByRef pErrorMessage As String) As DataSet
End Interface
