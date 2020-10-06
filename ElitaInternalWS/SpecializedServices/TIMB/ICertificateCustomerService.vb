Imports System.Collections.Generic
Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace SpecializedServices.Timb
    ' NOTE: You can use the "Rename" command on the context menu to change the interface name "ICertificateCustomerService" in both code and config file together.
    <ServiceContract(Namespace:="http://elita.assurant.com/SpecialzedServices/TIMB")>
    Public Interface ICertificateCustomerService

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_TIMB_GetCertCustomerInfo),
        FaultContract(GetType(ElitaFault)),
        FaultContract(GetType(CompanyNotFoundFault)),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault))>
        Function GetCertificateCustomer(request As CustomerByCertificateRequest) As CustomerResponse

    End Interface
End Namespace