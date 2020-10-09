Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace GeographicServices

    ' NOTE: You can use the "Rename" command on the context menu to change the interface name "IRegionAndComunaService" in both code and config file together.
    <ServiceContract(Namespace:="http://elita.assurant.com/GeographicServices/RegionAndComunaService", Name:="IRegionAndComunaService"),
    ServiceKnownType(GetType(CertificateNumberLookup))>
    Public Interface IRegionAndComunaService

        <OperationContract(Name:="GetRegionsAndComunasInfo"),
            ElitaPermission(PermissionCodes.WS_GeographicServices_GetRegionsAndComunas),
            FaultContract(GetType(CountryNotFoundFault)),
            FaultContract(GetType(RegionNotFoundFault)),
            FaultContract(GetType(ValidationFault))>
        Function GetRegionsAndComunasInfo(request As GetRegionsAndComunasInfoRequest) As GetRegionsAndComunasInfoResponse

    End Interface

End Namespace

