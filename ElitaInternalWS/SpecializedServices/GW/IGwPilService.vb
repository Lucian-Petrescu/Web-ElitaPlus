Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace SpecializedServices.GW
    <ServiceContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService", Name:="GwPilService")>
    Public Interface IGwPilService
        <OperationContract(Name:="GetCertificate"),
        ElitaPermission(PermissionCodes.WS_GWPIL_GetCertificate),
        FaultContract(GetType(ElitaInternalWS.SpecializedServices.CertificateNotFoundFault))>
        Function GetCertificate(ByVal request As GetCertificateInfoRequest) As GetCertificateInfoResponse

        <OperationContract(Name:="SearchCertificate"),
        ElitaPermission(PermissionCodes.WS_GWPIL_GetCertificate),
        FaultContract(GetType(ElitaInternalWS.SpecializedServices.CertificateNotFoundFault))>
        Function SearchCertificate(ByVal request As SearchCertificateRequest) As SearchCertificateResponse

        <OperationContract(Name:="SearchCertificateByTaxId"),
        ElitaPermission(PermissionCodes.WS_GWPIL_GetCertificate),
        FaultContract(GetType(ElitaInternalWS.SpecializedServices.InvalidCountryFault))>
        Function SearchCertificateByTaxId(ByVal request As SearchCertificateByTaxIdRequest) As SearchCertificateByTaxIdResponse
    End Interface
End Namespace


