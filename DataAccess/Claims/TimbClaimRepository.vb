Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class TimbClaimRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, IClaimEntity})
    Inherits Repository(Of TType, TimbClaimContext)
    Implements IClaimRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of TimbClaimContext))
    End Sub

    Public Function GetCertificateClaimInfo(pCompanyCode As String, pCertificateNumber As String, ByVal pSerialNumber As String, ByVal pPhoneNumber As String, pUserId As Guid, pLanguageId As Guid, ByRef pErrorCode As String, ByRef pErrorMessage As String) As DataSet Implements IClaimRepository(Of TType).GetCertificateClaimInfo
        Dim claimInfo As DataSet = MyBase.Context.GetCertificateClaimInfo(pCompanyCode,
                                                                            pCertificateNumber,
                                                                            pSerialNumber,
                                                                            pPhoneNumber,
                                                                            pUserId,
                                                                            pLanguageId,
                                                                            pErrorCode,
                                                                            pErrorMessage)

        Return claimInfo
    End Function

    Public Function GetClaimCaseReserve(pClaimId As Guid) As Decimal Implements IClaimRepository(Of TType).GetClaimCaseReserve
        Throw New NotImplementedException()
    End Function

    Public Function GetClaimsPaidAmountByCertificate(pCertificateId As Guid) As Decimal Implements IClaimRepository(Of TType).GetClaimsPaidAmountByCertificate
        Throw New NotImplementedException()
    End Function

    Public Function GetNextClaimNumber(pCompanyId As Guid) As String Implements IClaimRepository(Of TType).GetNextClaimNumber
        Throw New NotImplementedException()
    End Function

    Public Function GetPriceList(pCompanyId As Guid, pServiceCenterCode As String, pLookupDate As Date, pSalesPrice As Decimal, pRiskTypeId As Guid, pEquipmentClassId As Guid?, pEquipmentId As Guid?, pEquipmentConditionId As Guid?, pDealerId As Guid, pServiceLevelCode As String, pServiceClassCode As String, pServiceTypeCode As String, pCurrencyCode As String, pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord) Implements IClaimRepository(Of TType).GetPriceList
        Throw New NotImplementedException()
    End Function

    Public Function GetPriceListByMakeAndModel(pForceDate As Date, pClaimNumber As String, pCompanyCode As String, pServiceCenterCode As String, pRiskTypeCode As String, pEquipmentClassCode As String, pEquipmentId As Guid?, pEquipmentConditionId As Guid?, pDealerCode As String, pServiceClassCode As String, pServiceTypeCode As String, pMake As String, pModel As String, pLowPrice As Decimal, pHighPrice As Decimal, pServiceLevelCode As String, pCurrencyCode As String, pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord) Implements IClaimRepository(Of TType).GetPriceListByMakeAndModel
        Throw New NotImplementedException()
    End Function

    Public Function GetPriceListByMethodOfRepair(pMethodofRepairId As Guid, pCompanyId As Guid, pServiceCenterCode As String, pLookupDate As Date, pSalesPrice As Decimal, pRiskTypeId As Guid, pEquipmentClassId As Guid?, pEquipmentId As Guid?, pEquipmentConditionId As Guid?, pDealerId As Guid, pServiceLevelCode As String, pCurrencyCode As String, pCurrencyConversionDate As Date) As IEnumerable(Of PriceListDetailRecord) Implements IClaimRepository(Of TType).GetPriceListByMethodOfRepair
        Throw New NotImplementedException()
    End Function

    Public Function GetProductRemainingLiabilityLimit(pCertificateId As Guid, pLossDate As Date) As Decimal? Implements IClaimRepository(Of TType).GetProductRemainingLiabilityLimit
        Throw New NotImplementedException()
    End Function

    Public Function GetRemainingCoverageLiabilityLimit(pCertItemCoverageId As Guid, pLossDate As Date) As Decimal? Implements IClaimRepository(Of TType).GetRemainingCoverageLiabilityLimit
        Throw New NotImplementedException()
    End Function

    Public Function IsMaxReplacementsExceeded(pCertificateId As Guid, pMethodOfRepairId As Guid, pLossDate As Date, pReplacementBasedOn As String, pInsuranceActivationDate As Date) As Boolean Implements IClaimRepository(Of TType).IsMaxReplacementsExceeded
        Throw New NotImplementedException()
    End Function

    Public Function IsServiceLevelValid(pServiceCenterId As Guid, pRiskTypeId As Guid, pEffectiveDate As Date, pSalesPrice As Decimal, pServiceLevel As String) As Boolean Implements IClaimRepository(Of TType).IsServiceLevelValid
        Throw New NotImplementedException()
    End Function

    Public Function ReturnAdvanceExchange(pCert As String, pDealer As String, pCoverageTypeCode As String) As String Implements IClaimRepository(Of TType).ReturnAdvanceExchange
        Throw New NotImplementedException()
    End Function

    Public Function ValidateManufacturerByCompanyGroup(pManufacturerDesc As String, pCompanyId As Guid, pCertItemCoverageId As Guid) As Guid Implements IClaimRepository(Of TType).ValidateManufacturerByCompanyGroup
        Throw New NotImplementedException()
    End Function
End Class
