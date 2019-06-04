Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace AppServices.SA

    <ServiceContract(Namespace:="http://elita.assurant.com/AppServices/SA")>
    Public Interface IAPRService

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_APR_UpdateImei),
        ServiceKnownType(GetType(UpdateImeiRequest))>
        Sub UpdateImei(ByVal request As UpdateImeiRequest)

    End Interface
End Namespace