Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace Claims
    <ServiceContract(Namespace:="http://elita.assurant.com/Claim", Name:="IClaimService"),
    ServiceKnownType(GetType(ClaimSerialNumberLookup)),
    ServiceKnownType(GetType(ClaimImeiNumberLookup)),
    ServiceKnownType(GetType(ClaimNumberLookup)),
    ServiceKnownType(GetType(UpdateBatchNumber))>
    Public Interface IClaimServiceV1

        <OperationContract(Name:="GetElitaHeader")>
        Function GetElitaHeader() As ElitaHeader

        <OperationContract(Name:="GetClaims"),
            ElitaPermission(PermissionCodes.WS_Claim_GetClaims),
            FaultContract(GetType(DealerNotFoundFault))>
        Function GetClaims(ByVal request As GetClaimsRequest) As GetClaimsResponse

        <OperationContract(Name:="UpdateClaim"),
            ElitaPermission(PermissionCodes.WS_Claim_UpdateClaims),
            FaultContract(GetType(ClaimNotFoundFault))>
        Function UpdateClaim(ByVal request As UpdateClaimRequest) As UpdateClaimResponse

        <OperationContract(Name:="GetClaimDetails"),
            ElitaPermission(PermissionCodes.WS_Claim_GetClaimDetails),
            FaultContract(GetType(ClaimNotFoundFault))>
        Function GetClaimDetails(ByVal request As GetClaimDetailsRequest) As GetClaimDetailsResponse

    End Interface
End Namespace
