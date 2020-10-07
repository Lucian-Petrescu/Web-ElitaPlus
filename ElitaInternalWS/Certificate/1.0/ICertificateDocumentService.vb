Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace Certificates
    <ServiceContract(Namespace:="http://elita.assurant.com/Certificate", Name:="ICertificateDocumentService"),
    ServiceKnownType(GetType(CertificateNumberLookup)),
    ServiceKnownType(GetType(CertificateAccountNumberLookup)),
    ServiceKnownType(GetType(CertificateByCompanyLookup)),
    ServiceKnownType(GetType(CertificateSerialTaxLookup)),
    ServiceKnownType(GetType(CertficateDealerPhoneLookUp))>
    Public Interface ICertificateDocumentServiceV1

        <OperationContract(Name:="AttachDocument"),
            ElitaPermission(PermissionCodes.WS_Cert_AttachImage),
            FaultContract(GetType(FileTypeNotValidFault)),
            FaultContract(GetType(CertificateNotFound)),
            FaultContract(GetType(ValidationFault)),
            FaultContract(GetType(ImageRepositoryNotFoundFault))>
        Function AttachDocument(request As AttachCertificateDocumentRequest) As AttachCertificateDocumentResponse

        <OperationContract(Name:="DownloadDocument"),
            ElitaPermission(PermissionCodes.WS_Cert_DownloadImage),
            FaultContract(GetType(CertificateNotFound)),
            FaultContract(GetType(CertificateDocumentNotFoundFault)),
            FaultContract(GetType(FileIntegrityFailedFault)),
            FaultContract(GetType(FileTypeNotValidFault)),
            FaultContract(GetType(ValidationFault)),
            FaultContract(GetType(ImageRepositoryNotFoundFault))>
        Function DownloadDocument(request As DownloadCertificateDocumentRequest) As DownloadCertificateDocumentResponse


        <OperationContract(Name:="GetCertificateDocuments"),
            ElitaPermission(PermissionCodes.WS_Cert_GetDetails),
            FaultContract(GetType(ClaimNotFoundFault))>
        Function GetCertificateDocuments(request As GetCertificateDocumentsRequest) As GetCertificateDocumentsResponse
    End Interface
End Namespace
