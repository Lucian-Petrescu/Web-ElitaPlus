Imports System.Collections.Generic
Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace SpecializedServices.Tisa
    ' NOTE: You can use the "Rename" command on the context menu to change the interface name "ICertificateCustomerService" in both code and config file together.
    <ServiceContract(Namespace:="http://elita.assurant.com/SpecialzedServices/TISA")>
    Public Interface ICertificateCustomerService

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_CHLMovistarUpgrade_GetCertCustomerInfo),
        FaultContract(GetType(ElitaFault)),
        FaultContract(GetType(InvalidRequestFault)),
        FaultContract(GetType(CompanyNotFoundFault)),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault))>
        Function GetCertificateCustomer(ByVal request As CustomerByCertificateRequest) As CustomerResponse

    End Interface
End Namespace