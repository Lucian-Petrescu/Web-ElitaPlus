Imports System.ServiceModel
Imports ElitaInternalWS.Security
' NOTE: You can use the "Rename" command on the context menu to change the interface name "ICertCustomerInfoService" in both code and config file together.
<ServiceContract(Namespace:="http://elita.assurant.com/SpecialzedServices/CertCustomerInfoService", Name:="ICertCustomerInfoService"),
    ServiceKnownType(GetType(CertificateNumberLookup))>
Public Interface ICertCustomerInfoService

    <OperationContract(Name:="GetCertCustomerInfo"),
            ElitaPermission(PermissionCodes.WS_CertCustomerInfoService_GetCertificateInfo),
            FaultContract(GetType(CertificateNotFound)),
            FaultContract(GetType(DuplicateCertFound)),
            FaultContract(GetType(InvalidCertificateNumber)),
            FaultContract(GetType(ValidationFault))>
    Function GetCertCustomerInfo(ByVal request As GetCertificateRequest) As CertCustomerInfoResponse

End Interface
