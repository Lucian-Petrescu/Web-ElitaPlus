Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace SpecializedServices
    <ServiceContract(Namespace:="http://elita.assurant.com/SpecializedServices", Name:="ICHLMobileSCPortal")> _
    Public Interface ICHLMobileSCPortal

#Region "Operations"
        <OperationContract(Name:="GetElitaHeader")> _
        Function GetElitaHeader() As ElitaHeader

        <OperationContract(Name:="GetCertClaimInfo"), _
            ElitaPermission(PermissionCodes.WS_CHLMobileSCPortal_GetCertClaimInfo), _
            FaultContract(GetType(CHLMobileSCPortalFault))> _
        Function GetCertClaimInfo(CertClaimInfo As CertClaimInfoRequestDC) As CertClaimInfoResponseDC
#End Region

    End Interface
End Namespace
