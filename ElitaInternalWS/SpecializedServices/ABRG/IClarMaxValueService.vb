Imports System.ServiceModel
Imports ElitaInternalWS.Security

<ServiceContract(Namespace:="http://elita.assurant.com/SpecialzedServices/ClarMaxValueService", Name:="IClarMaxValueService"),
    ServiceKnownType(GetType(CertificateNumberLookup)),
    ServiceKnownType(GetType(CertificateAccountNumberLookup)),
        ServiceKnownType(GetType(CertificateSerialTaxLookup))>
Public Interface IClarMaxValueService

    <OperationContract(Name:="GetCertificate"),
            ElitaPermission(PermissionCodes.WS_ClarMaxValueService_GetCertificateInfo),
            FaultContract(GetType(CertificateNotFound)),
            FaultContract(GetType(InvalidSerialnumber)),
            FaultContract(GetType(DealerNotFoundFault)),
            FaultContract(GetType(InvalidIdentificationNumber))>
    Function GetCertificate(ByVal request As GetCertificateRequest) As GetCertificateResponse

End Interface
