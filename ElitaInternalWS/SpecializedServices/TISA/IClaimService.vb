Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace SpecializedServices.Tisa

    <ServiceContract(Namespace:="http://elita.assurant.com/SpecialzedServices/TISA")>
    Public Interface IClaimService

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_TISA_ClaimService),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault)),
        FaultContract(GetType(CoverageNotFoundFault)),
        FaultContract(GetType(MultipleCoveragesFoundFault)),
        FaultContract(GetType(ServiceCenterNotFoundFault)),
        FaultContract(GetType(InvalidExtendedStatusFault)),
        FaultContract(GetType(InvalidDateOfLossFault)),
        FaultContract(GetType(PriceListNotConfiguredFault)),
        FaultContract(GetType(DefaultExtendedStatusNotFoundFault)),
        FaultContract(GetType(MakeAndModelNotFoundFault)),
        FaultContract(GetType(InvalidClaimTypeFault))>
        Function CreateClaim(request As CreateClaimRequest) As CreateClaimResponse

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_TISA_ClaimService),
        FaultContract(GetType(CompanyNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault)),
        FaultContract(GetType(ClaimNotFoundFault))>
        Function CreateRepairReplacementClaim(request As CreateRepairReplacementClaimRequest) As CreateClaimResponse

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_TISA_ClaimService),
        FaultContract(GetType(CompanyNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault)),
        FaultContract(GetType(ClaimNotFoundFault)),
        FaultContract(GetType(RepairClaimNotFulfilledFault))>
        Function CreateServiceWarrantyClaim(request As CreateServiceWarrantyClaimRequest) As CreateClaimResponse


        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_TISA_ClaimService),
        FaultContract(GetType(CompanyNotFoundFault)),
        FaultContract(GetType(ClaimNotFoundFault)),
        FaultContract(GetType(ReplacementClaimFoundFault)),
        FaultContract(GetType(InvalidUpdateActionFault)),
        FaultContract(GetType(InvalidExtendedStatusFault))>
        Sub DenyClaim(request As DenyClaimRequest)

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_TISA_ClaimService),
        FaultContract(GetType(CompanyNotFoundFault)),
        FaultContract(GetType(ClaimNotFoundFault)),
        FaultContract(GetType(ServiceCenterNotFoundFault)),
        FaultContract(GetType(ReplacementClaimFoundFault)),
        FaultContract(GetType(InvalidServiceLevelFault)),
        FaultContract(GetType(PriceListNotConfiguredFault)),
        FaultContract(GetType(InvalidRepairDateFault)),
        FaultContract(GetType(InvalidExtendedStatusFault))>
        Sub UpdateRepairableClaim(request As UpdateRepairableClaimRequest)

        '<OperationContract(),
        'ElitaPermission(PermissionCodes.WS_TISA_ClaimService),
        'FaultContract(GetType(ClaimNotFoundFault)),
        'FaultContract(GetType(ServiceCenterNotFoundFault)),
        'FaultContract(GetType(InvalidExtendedStatusFault))>
        'Sub UpdateIrrepairableClaim(ByVal request As UpdateIrrepairableClaimRequest)

        <OperationContract(),
       ElitaPermission(PermissionCodes.WS_TISA_ClaimService),
       FaultContract(GetType(ClaimNotFoundFault)),
       FaultContract(GetType(ServiceCenterNotFoundFault)),
       FaultContract(GetType(InvalidExtendedStatusFault)),
       FaultContract(GetType(PriceListNotConfiguredFault)),
       FaultContract(GetType(InvalidRepairDateFault)),
       FaultContract(GetType(RefurbishedCostRequiredFault))>
        Sub UpdateIrrepairableWithReplacement(request As UpdateClaimReplacedWithRefubishedRequest)

        <OperationContract(),
       ElitaPermission(PermissionCodes.WS_TISA_ClaimService),
       FaultContract(GetType(ClaimNotFoundFault)),
       FaultContract(GetType(ServiceCenterNotFoundFault)),
       FaultContract(GetType(PriceListNotConfiguredFault)),
       FaultContract(GetType(InvalidRepairDateFault)),
       FaultContract(GetType(InvalidExtendedStatusFault))>
        Sub UpdateTheftClaim(request As UpdateTheftClaimRequest)


    End Interface
End Namespace