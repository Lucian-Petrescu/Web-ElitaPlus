Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace Claims
    <ServiceContract(Namespace:="http://elita.assurant.com/Claim", Name:="IClaimDocumentService"), _
    ServiceKnownType(GetType(ClaimSerialNumberLookup)), _
    ServiceKnownType(GetType(ClaimNumberLookup))> _
    Public Interface IClaimDocumentServiceV1

        <OperationContract(Name:="GetElitaHeader")> _
        Function GetElitaHeader() As ElitaHeader

        <OperationContract(Name:="AttachDocument"),
            ElitaPermission(PermissionCodes.WS_ClaimDocument_Attach),
            FaultContract(GetType(FileTypeNotValidFault)),
            FaultContract(GetType(ClaimNotFoundFault)),
            FaultContract(GetType(ValidationFault)),
            FaultContract(GetType(ImageRepositoryNotFoundFault))>
        Function AttachDocument(request As AttachDocumentRequest) As AttachDocumentResponse

        <OperationContract(Name:="AttachDocumentMultiThread"),
            ElitaPermission(PermissionCodes.WS_ClaimDocument_Attach),
            FaultContract(GetType(FileTypeNotValidFault)),
            FaultContract(GetType(ClaimNotFoundFault)),
            FaultContract(GetType(ValidationFault)),
            FaultContract(GetType(ImageRepositoryNotFoundFault))>
        Function AttachDocumentMultiThread(request As AttachDocumentRequest) As AttachDocumentResponse

        <OperationContract(Name:="DownloadDocument"), _
            ElitaPermission(PermissionCodes.WS_ClaimDocument_Download), _
            FaultContract(GetType(ClaimNotFoundFault)), _
            FaultContract(GetType(ClaimImageNotFoundFault)), _
            FaultContract(GetType(FileIntegrityFailedFault)), _
            FaultContract(GetType(FileTypeNotValidFault)), _
            FaultContract(GetType(ValidationFault)), _
            FaultContract(GetType(ImageRepositoryNotFoundFault))> _
        Function DownloadDocument(request As DownloadDocumentRequest) As DownloadDocumentResponse

    End Interface
End Namespace
