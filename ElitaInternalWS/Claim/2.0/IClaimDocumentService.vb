Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace Claims
    <ServiceContract(Namespace:="http://elita.assurant.com/Claim", Name:="IClaimDocumentService"), _
    ServiceKnownType(GetType(ClaimSerialNumberLookup)), _
    ServiceKnownType(GetType(ClaimNumberLookup))> _
    Public Interface IClaimDocumentServiceV2

        <OperationContract(Name:="AttachDocument"), _
            ElitaPermission(PermissionCodes.WS_ClaimDocument_Attach), _
            FaultContract(GetType(FileTypeNotValidFault)), _
            FaultContract(GetType(ClaimNotFoundFault)), _
            FaultContract(GetType(ValidationFault)), _
            FaultContract(GetType(ImageRepositoryNotFoundFault))> _
        Function AttachDocument(ByVal request As AttachDocumentRequest) As AttachDocumentResponse

        <OperationContract(Name:="DownloadDocument"), _
            ElitaPermission(PermissionCodes.WS_ClaimDocument_Download), _
            FaultContract(GetType(ClaimNotFoundFault)), _
            FaultContract(GetType(ClaimImageNotFoundFault)), _
            FaultContract(GetType(FileIntegrityFailedFault)), _
            FaultContract(GetType(FileTypeNotValidFault)), _
            FaultContract(GetType(ValidationFault)), _
            FaultContract(GetType(ImageRepositoryNotFoundFault))> _
        Function DownloadDocument(ByVal request As DownloadDocumentRequest) As DownloadDocumentResponse

    End Interface
End Namespace
