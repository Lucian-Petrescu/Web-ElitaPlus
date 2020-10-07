' NOTE: You can use the "Rename" command on the context menu to change the class name "SFRPolicyService" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select SFRPolicyService.svc or SFRPolicyService.svc.vb at the Solution Explorer and start debugging.


Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace SpecializedServices.SFR
    <ServiceContract(Namespace:="http://elita.assurant.com/SpecializedServices/SFRPolicyService", Name:="SFRPolicyService")>
    Public Interface ISFRPolicyService
        
        <OperationContract(Name:="SearchCertificate"),
        ElitaPermission(PermissionCodes.WS_SFRPOLICY_GetCertificate),
        FaultContract(GetType(ElitaInternalWS.SpecializedServices.CertificateNotFoundFault))>
        Function SearchCertificate(request As SearchCertificateRequest) As SearchCertificateResponse
      
    End Interface
End Namespace
