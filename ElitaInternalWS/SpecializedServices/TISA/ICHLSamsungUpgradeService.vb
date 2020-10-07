Imports System.ServiceModel
Imports ElitaInternalWS.Security


' NOTE: You can use the "Rename" command on the context menu to change the interface name "ICHLSamsungUpgradeService" in both code and config file together.
Namespace SpecializedServices.Tisa

    <ServiceContract(Namespace:="http://elita.assurant.com/SpecialzedServices/TISA")>
    Public Interface ICHLSamsungUpgradeService

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_TISA_ClaimService),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault))>
        Function GetPremiumFromProduct(request As GetCertGrossAmtByProdCodeRequst) As GetCertGrossAmtByProdCodeResponse

    End Interface
End Namespace