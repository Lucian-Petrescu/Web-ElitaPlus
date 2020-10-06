Imports System.ServiceModel
Imports ElitaInternalWS.Security

' NOTE: You can use the "Rename" command on the context menu to change the interface name "ICertUpgradeService" in both code and config file together.

<ServiceContract(Namespace:="http://elita.assurant.com/SpecialzedServices/CertAfterUpgradeService", Name:="ICertAfterUpgradeService"),
    ServiceKnownType(GetType(CertificateNumberLookup)),
    ServiceKnownType(GetType(CertificateAccountNumberLookup)),
    ServiceKnownType(GetType(CertificateSerialTaxLookup)),
    ServiceKnownType(GetType(CertificateDealerSerialLookUp)),
    ServiceKnownType(GetType(CertAfterUpgradeLookup))>
Public Interface ICertAfterUpgradeService

    <OperationContract(Name:="GetCertAfterUpgrade"),
            ElitaPermission(PermissionCodes.WS_CertUpgradeService_GetCertificateInfo),
            FaultContract(GetType(CertificateNotFound)),
            FaultContract(GetType(DuplicateCertFound)),
            FaultContract(GetType(CoverageNotFoundFault)),
            FaultContract(GetType(ValidationFault)),
            FaultContract(GetType(InvalidIdentificationNumber)),
            FaultContract(GetType(InvalidUpgradeDateFault))>
    Function GetCertAfterUpgrade(request As GetCertificateRequest) As GetCertificateResponse

End Interface

