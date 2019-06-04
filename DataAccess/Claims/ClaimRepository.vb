Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Imports Oracle.ManagedDataAccess.Client

Public NotInheritable Class ClaimRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, IClaimEntity})
    Inherits Repository(Of TType, ClaimContext)
    Implements IClaimRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of ClaimContext)())
    End Sub

    Private Property m_ClaimContext As ClaimContext


    Public Function GetPriceList(ByVal pCompanyId As Guid,
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
                                 ByVal pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord) Implements IClaimRepository(Of TType).GetPriceList


        Dim pricelist As IEnumerable(Of PriceListDetailRecord) = MyBase.Context.GetPriceList(pCompanyId, pServiceCenterCode, DateTime.Today, pSalesPrice, pRiskTypeId, pEquipmentClassId, pEquipmentId,
                                  pEquipmentConditionId, pDealerId, pServiceLevelCode, pServiceClassCode, pServiceTypeCode, pCurrencyCode, pCurrencyConversionDate)
        Return pricelist

    End Function

    Public Function GetPriceListByMakeAndModel(ByVal pForceDate As Date,
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
                                                ByVal pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord) Implements IClaimRepository(Of TType).GetPriceListByMakeAndModel

        Dim pricelist As IEnumerable(Of PriceListDetailRecord) = MyBase.Context.GetPriceListByMakeAndModel(pForceDate, pClaimNumber, pCompanyCode, pServiceCenterCode, pRiskTypeCode, pEquipmentClassCode, pEquipmentId, pEquipmentConditionId,
                                                                                                           pDealerCode, pServiceClassCode, pServiceTypeCode, pMake, pModel, pLowPrice, pHighPrice, pServiceLevelCode, pCurrencyCode, pCurrencyConversionDate)

        Return pricelist
    End Function
    Public Function GetProductRemainingLiabilityLimit(pCertificateId As Guid,
                                                      pLossDate As Date) As Nullable(Of Decimal) Implements IClaimRepository(Of TType).GetProductRemainingLiabilityLimit
        Return MyBase.Context.GetProductRemainingLiabilityLimit(pCertificateId, pLossDate)
    End Function

    Public Function GetRemainingCoverageLiabilityLimit(pCertItemCoverageId As Guid, pLossDate As Date) As Nullable(Of Decimal) Implements IClaimRepository(Of TType).GetRemainingCoverageLiabilityLimit
        Return MyBase.Context.GetRemainingCoverageLiabilityLimit(pCertItemCoverageId, pLossDate)
    End Function

    Public Function GetNextClaimNumber(pCompanyId As Guid) As String Implements IClaimRepository(Of TType).GetNextClaimNumber
        Return MyBase.Context.GetNextClaimNumber(pCompanyId)

    End Function


    Public Function IsMaxReplacementsExceeded(ByVal pCertificateId As Guid,
                                              ByVal pMethodOfRepairId As Guid,
                                              ByVal pLossDate As Date,
                                              ByVal pReplacementBasedOn As String,
                                              ByVal pInsuranceActivationDate As Date) As Boolean Implements IClaimRepository(Of TType).IsMaxReplacementsExceeded
        Return MyBase.Context.IsMaxReplacementsExceeded(pCertificateId, pMethodOfRepairId, pLossDate, pReplacementBasedOn, pInsuranceActivationDate)
    End Function

    Public Function IsServiceLevelValid(ByVal pServiceCenterId As Guid,
                                        ByVal pRiskTypeId As Guid,
                                        ByVal pEffectiveDate As Date,
                                        ByVal pSalesPrice As Decimal,
                                        ByVal pServiceLevel As String) As Boolean Implements IClaimRepository(Of TType).IsServiceLevelValid
        Return MyBase.Context.IsServiceLevelValid(pServiceCenterId, pRiskTypeId, pEffectiveDate, pSalesPrice, pServiceLevel)
    End Function

    Public Function GetClaimsPaidAmountByCertificate(ByVal pServiceCenterId As Guid) As Decimal Implements IClaimRepository(Of TType).GetClaimsPaidAmountByCertificate
        Return MyBase.Context.GetClaimsPaidAmountByCertificate(pServiceCenterId)
    End Function

    Private Function ValidateManufacturerByCompanyGroup(ByVal pManufacturerDesc As String, ByVal pCompanyId As Guid, ByVal pCertItemCoverageId As Guid) As Guid Implements IClaimRepository(Of TType).ValidateManufacturerByCompanyGroup
        Return MyBase.Context.ValidateManufacturerByCompanyGroup(pManufacturerDesc, pCompanyId, pCertItemCoverageId)
    End Function

    Public Function GetPriceListByMethodOfRepair(pMethodofRepairId As Guid, pCompanyId As Guid, pServiceCenterCode As String, pLookupDate As Date, pSalesPrice As Decimal, pRiskTypeId As Guid, pEquipmentClassId As Guid?, pEquipmentId As Guid?, pEquipmentConditionId As Guid?, pDealerId As Guid, pServiceLevelCode As String, pCurrencyCode As String, pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord) Implements IClaimRepository(Of TType).GetPriceListByMethodOfRepair
        Dim pricelist As IEnumerable(Of PriceListDetailRecord) = MyBase.Context.GetPriceListByMethodOfRepair(pMethodofRepairId, pCompanyId, pServiceCenterCode, DateTime.Today, pSalesPrice, pRiskTypeId, pEquipmentClassId, pEquipmentId,
                                  pEquipmentConditionId, pDealerId, pServiceLevelCode, pCurrencyCode, pCurrencyConversionDate)
        Return pricelist
    End Function

    Public Function ReturnAdvanceExchange(ByVal pCert As String,
                                          ByVal pDealer As String,
                                          ByVal pCoverageTypeCode As String) As String Implements IClaimRepository(Of TType).ReturnAdvanceExchange
        Return MyBase.Context.ReturnAdvanceExchange(pCert, pDealer, pCoverageTypeCode)
    End Function

    Friend Function GetClaimCaseReserve(ByVal pClaimId As Guid) As Decimal Implements IClaimRepository(Of TType).GetClaimCaseReserve
        Return MyBase.Context.GetClaimCaseReserve(pClaimId)
    End Function

    Public Function GetCertificateClaimInfo(pCompanyCode As String, pCertificateNumber As String, ByVal pSerialNumber As String, ByVal pPhoneNumber As String, pUserId As Guid, pLanguageId As Guid, ByRef pErrorCode As String, ByRef pErrorMessage As String) As DataSet Implements IClaimRepository(Of TType).GetCertificateClaimInfo
        Throw New NotImplementedException()
    End Function
End Class
