Imports System.ServiceModel
Imports ElitaInternalWS.Security
Imports ElitaInternalWS.SpecializedServices

Namespace ServiceOrderDocument

    <ServiceContract(Namespace:="http://elita.assurant.com/Claim/ServiceOrder")>
    Public Interface IServiceOrderDocumentService

        <OperationContract(),
            ElitaPermission(PermissionCodes.WS_ServiceOrder_Download),
            FaultContract(GetType(CompanyNotFoundFault)),
            FaultContract(GetType(ServiceCenterNotFoundFault)),
            FaultContract(GetType(ClaimNotFoundFault)),
            FaultContract(GetType(FileIntegrityFailedFault)),
            FaultContract(GetType(FileTypeNotValidFault)),
            FaultContract(GetType(ValidationFault)),
            FaultContract(GetType(ImageRepositoryNotFoundFault)),
            ServiceKnownType(GetType(DownloadServiceOrderDocumentRequest))>
        Function DownloadDocument(request As DownloadServiceOrderDocumentRequest) As DownloadServiceOrderDocumentResponse

    End Interface
End Namespace