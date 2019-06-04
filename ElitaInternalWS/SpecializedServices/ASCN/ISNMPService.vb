Imports System.ServiceModel
Imports ElitaInternalWS.Security
Imports Assurant.ElitaPlus.BusinessObjectsNew

Namespace SpecializedServices.Ascn

    <ServiceContract(Namespace:="http://elita.assurant.com/SpecialzedServices/Ascn")>
    Public Interface ISNMPService

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_SNMP_ClaimService),
        FaultContract(GetType(PartInfoNotFoundFault)),
        ServiceKnownType(GetType(GetPartsRequest))>
        Function GetParts(ByVal request As GetPartsRequest) As GetPartsResponse

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_SNMP_ClaimService),
        FaultContract(GetType(CompanyNotFoundFault)),
        FaultContract(GetType(ServiceCenterNotFoundFault)),
        FaultContract(GetType(CoverageNotFoundFault)),
        FaultContract(GetType(MethodOfRepairNotFoundFault)),
        FaultContract(GetType(ClaimNotFoundFault)),
        ServiceKnownType(GetType(GetServiceCenterClaimsRequest))>
        Function GetServiceCenterClaims(ByVal request As GetServiceCenterClaimsRequest) As GetServiceCenterClaimsResponse

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_SNMP_ClaimService),
        FaultContract(GetType(CompanyNotFoundFault)),
        FaultContract(GetType(ClaimNotFoundFault)),
        ServiceKnownType(GetType(GetClaimDetailRequest))>
        Function GetClaimDetail(ByVal request As GetClaimDetailRequest) As GetClaimDetailResponse

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_SNMP_ClaimService),
        FaultContract(GetType(ServiceCenterNotFoundFault)),
        ServiceKnownType(GetType(GetServiceCenterRequest))>
        Function GetServiceCenter(ByVal request As GetServiceCenterRequest) As GetServiceCenterResponse

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_SNMP_ClaimService),
        FaultContract(GetType(CompanyNotFoundFault)),
        FaultContract(GetType(ClaimNotFoundFault)),
        FaultContract(GetType(UpdateClaimErrorFault)),
        ServiceKnownType(GetType(UpdateClaimRequest))>
        Sub UpdateClaim(ByVal request As UpdateClaimRequest)

    End Interface
End Namespace