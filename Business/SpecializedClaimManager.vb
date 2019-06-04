Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class SpecializedClaimManager
    Implements IClaimManager
    Implements ISpecializedClaimManager


    Private m_claimRepository As IClaimRepository(Of Claim)
    Public Property ClaimRepository As IClaimRepository(Of Claim) Implements ISpecializedClaimManager.ClaimRepository
        Get
            Return m_claimRepository
        End Get
        Set(value As IClaimRepository(Of Claim))
            m_claimRepository = value
        End Set
    End Property

    Public Sub AddDefaultExtendedStatus(ByRef pClaim As Claim, pDealer As Dealer, Action As String) Implements IClaimManager.AddDefaultExtendedStatus
        Throw New NotImplementedException()
    End Sub

    Public Sub ChangeServiceCenter(pClaim As Claim, pCert As Certificate, pCompany As Company, pComments As String, pServiceCenterCode As String) Implements IClaimManager.ChangeServiceCenter
        Throw New NotImplementedException()
    End Sub

    Public Sub DenyClaim(pRepairClaim As Claim, pCert As Certificate, pComments As String) Implements IClaimManager.DenyClaim
        Throw New NotImplementedException()
    End Sub

    Public Sub DenyClaim(pRepairClaim As Claim, pCert As Certificate, pCompany As Company, pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields) Implements IClaimManager.DenyClaim
        Throw New NotImplementedException()
    End Sub

    Public Sub FulfillClaim(pClaim As Claim, pCert As Certificate, pCompany As Company, pDealerCode As String, pComments As String, pRepairDate As Date, pClaimType As Long, pSerialNumber As String, pModel As String, pMake As String, pServiceCenterCode As String, pTrackingNumber As String) Implements IClaimManager.FulfillClaim
        Throw New NotImplementedException()
    End Sub

    Public Sub InsertClaimExtendedStatus(pDealer As Dealer, pExtendedStatusCode As String, pExtendedStatusDate As Date, ByRef pClaim As Claim) Implements IClaimManager.InsertClaimExtendedStatus
        Throw New NotImplementedException()
    End Sub

    Public Sub MaxDaysElapsedAdEx(pClaim As Claim, pCert As Certificate, pCompany As Company, pDealer As Dealer, pComments As String) Implements IClaimManager.MaxDaysElapsedAdEx
        Throw New NotImplementedException()
    End Sub

    Public Sub PayClaim(pClaim As Claim, pCert As Certificate, pCompany As Company, pServiceCenterCode As String, pInvoiceNumber As String, pPaymentAmount As Decimal) Implements IClaimManager.PayClaim
        Throw New NotImplementedException()
    End Sub

    Public Sub ReturnDamagedDeviceAdvEx(pClaim As Claim, pCert As Certificate, pCompany As Company, pDealer As Dealer, pComments As String, pCoverageTypeCode As String) Implements IClaimManager.ReturnDamagedDeviceAdvEx
        Throw New NotImplementedException()
    End Sub

    Public Sub UpdateClaimBasicFields(ByRef pClaim As Claim, pCert As Certificate, pCompany As Company, pHasCoverageChanged As Boolean, pCoverageTypeCode As String, pHasSvcCenterChanged As Boolean, pServiceCenterCode As String, pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields) Implements IClaimManager.UpdateClaimBasicFields
        Throw New NotImplementedException()
    End Sub

    Public Sub UpdateClaimReplacedWithRefubished(pRepairClaim As Claim, pCert As Certificate, pCompany As Company, pCoverageTypeCode As String, pServiceCenterCode As String, pServiceLevelCode As String, pHasCoverageChanged As Boolean, pHasSvcCenterChanged As Boolean, pIsServiceWarrantyClaim As Boolean, pAuthorizedAmount As Decimal?, pSimCardAmount As Decimal?, pSerialNumberReplacedDevice As String, pModel As String, pManufacturer As String, pUpdateAction As String, pDeviceCondition As IClaimManager.DeviceConditionEnum, pLossType As String, pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields) Implements IClaimManager.UpdateClaimReplacedWithRefubished
        Throw New NotImplementedException()
    End Sub

    Public Sub UpdateRepairableClaim(pRepairClaim As Claim, pCert As Certificate, pCompany As Company, pCoverageTypeCode As String, pServiceCenterCode As String, pServiceLevelCode As String, pHasCoverageChanged As Boolean, pHasSvcCenterChanged As Boolean, pIsServiceWarrantyClaim As Boolean, pMake As String, pModel As String, pSerialNumberReplacedDevice As String, pAuthorizedAmount As Decimal?, pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields) Implements IClaimManager.UpdateRepairableClaim
        Throw New NotImplementedException()
    End Sub

    Public Sub UpdateTheftClaim(pReplacementClaim As Claim, pCert As Certificate, pCompany As Company, pHasSvcCenterChanged As Boolean, pCoverageTypeCode As String, pServiceCenterCode As String, pServiceLevelCode As String, pAuthorizedAmount As Decimal?, pSerialNumberReplacedDevice As String, pModel As String, pManufacturer As String, pCondition As IClaimManager.DeviceConditionEnum, pLossType As String, pSimCardAmount As Decimal?, pClaimBasicUpdateFields As IClaimManager.ClaimBasicUpdateFields) Implements IClaimManager.UpdateTheftClaim
        Throw New NotImplementedException()
    End Sub

    Public Function ComputeTax(pAmount As Decimal, pDealerId As Guid?, pCountryId As Guid?, pCompanyTypeId As Guid?, pTaxTypeId As Guid?, pRegionId As Guid?, pExpectedPremiumIsWpId As Guid?, pProductTaxTypeId As Guid?, pSalesDate As Date) As Decimal Implements IClaimManager.ComputeTax
        Throw New NotImplementedException()
    End Function

    Public Function CreateClaim(pCic As CertificateItemCoverage, pClaim As Claim, pDealer As Dealer, pCoverageTypeCode As String, pServiceCenterCode As String, pClaimType As Integer, pMake As String, pModel As String) As Object Implements IClaimManager.CreateClaim
        Throw New NotImplementedException()
    End Function

    Public Function CreateClaim(pCic As CertificateItemCoverage, pClaim As Claim, pDealer As Dealer, pCoverageTypeCode As String, pServiceCenterCode As String, pClaimType As Integer, pMake As String, pModel As String, pComments As String, pDamageType As String) As Object Implements IClaimManager.CreateClaim
        Throw New NotImplementedException()
    End Function

    Public Function CreateRepairReplacmentClaim(pCertificate As Certificate, pRepairClaim As Claim) As Claim Implements IClaimManager.CreateRepairReplacmentClaim
        Throw New NotImplementedException()
    End Function

    Public Function CreateServiceWarrantyClaim(pCertificate As Certificate, pRepairClaim As Claim) As Claim Implements IClaimManager.CreateServiceWarrantyClaim
        Throw New NotImplementedException()
    End Function

    Public Function GetCertificateClaimInfo(pCompanyCode As String, pCertificateNumber As String, ByVal pSerialNumber As String, ByVal pPhoneNumber As String, pUserId As Guid, pLanguageId As Guid, ByRef pErrorCode As String, ByRef pErrorMessage As String) As DataSet Implements IClaimManager.GetCertificateClaimInfo
        If (ClaimRepository Is Nothing) Then
            Throw New ArgumentNullException("ClaimRepository")
        End If

        Return ClaimRepository.GetCertificateClaimInfo(pCompanyCode, pCertificateNumber, pSerialNumber, pPhoneNumber, pUserId, pLanguageId, pErrorCode, pErrorMessage)
    End Function

    Public Function GetClaim(pClaimNUmber As String, pCompanyId As Guid) As Claim Implements IClaimManager.GetClaim
        Throw New NotImplementedException()
    End Function

    Public Function GetClaimInfo(pClaim As Claim, pDealer As Dealer, pCertificate As Certificate) As Object Implements IClaimManager.GetClaimInfo
        Throw New NotImplementedException()
    End Function

    Public Function GetClaimsPaidAmountByCertificate(pCertificateId As Guid) As Decimal Implements IClaimManager.GetClaimsPaidAmountByCertificate
        Throw New NotImplementedException()
    End Function

    Public Function IsCoverageChangeValid(pCertificate As Certificate, CoverageTypeCode As String, pDateOfLoss As Date) As Boolean Implements IClaimManager.IsCoverageChangeValid
        Throw New NotImplementedException()
    End Function

    Public Function LocateCoverage(pCertificate As Certificate, pDateOfLoss As Date, pCoverageTypeCode As String, pItemNumber As Short?) As CertificateItemCoverage Implements IClaimManager.LocateCoverage
        Throw New NotImplementedException()
    End Function

    Public Function SaveClaim(pClaim As Claim) As Claim Implements IClaimManager.SaveClaim
        Throw New NotImplementedException()
    End Function

    Public Function ValidateManufacturerByCompanyGroup(pNewMake As String, pCompanyId As Guid, pCertItemCoverageId As Guid) As Guid Implements IClaimManager.ValidateManufacturerByCompanyGroup
        Throw New NotImplementedException()
    End Function

    Public Function ValidateServiceCenter(pDealer As Dealer, pServiceCenterCode As String) As Boolean Implements IClaimManager.ValidateServiceCenter
        Throw New NotImplementedException()
    End Function
End Class
