Imports System.ServiceModel
Imports ElitaInternalWS.AppServices.SA.SNMPortal.DataContracts
Imports ElitaInternalWS.Security

Namespace AppServices.SA.SNMPortal
        <ServiceContract(Namespace:=SnmPortalConstants.ContractNameSpace, Name:="IClaimReportService")> _
        Public Interface IClaimReportService

#Region "Operations"
            <OperationContract(Name:="GetClaimCharterReport"), _
                ElitaPermission(PermissionCodes.WS_CHLMobileSCPortal_GetCertClaimInfo), _
                FaultContract(GetType(Faults.InvalidRequestFault))> _
            Function GetClaimCharterReport (request As GetClaimCharterReportRequest) As GetClaimCharterReportResponse
#End Region

            <OperationContract(Name:="GetClaimCharterReportNextPage"), _
                ElitaPermission(PermissionCodes.WS_CHLMobileSCPortal_GetCertClaimInfo), _
                FaultContract(GetType(Faults.InvalidRequestFault))> _
            Function GetClaimCharterReportNextPage (request As GetClaimCharterReportNextPageRequest) As GetClaimCharterReportResponse
        End Interface
    
End Namespace
