Imports System.ServiceModel
Imports ElitaInternalWS.Security

<ServiceContract(Namespace:="http://elita.assurant.com/SpecializedServices/FVSTMobileApplicationService", Name:="FVSTMobileApplicationService")>
Public Interface IFVSTMobileApplicationService

    <OperationContract(Name:="GetCertificateInfo"),
        ElitaPermission(PermissionCodes.WS_FVSTMobileApplicationService_GetCertificateInfo),
        FaultContract(GetType(ElitaInternalWS.SpecializedServices.CertificateNotFoundFault))>
    Function GetCertificateInfo(ByVal request As GetCertificateInfoRequest) As GetCertificateInfoResponse

End Interface
