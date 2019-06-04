Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace Certificates
    <ServiceContract(Namespace:="http://elita.assurant.com/Certificate", Name:="ICertificateService"),
    ServiceKnownType(GetType(CertificateNumberLookup)),
    ServiceKnownType(GetType(CertificateAccountNumberLookup)),
    ServiceKnownType(GetType(CertificateByCompanyLookup)),
    ServiceKnownType(GetType(CertificateSerialTaxLookup)),
    ServiceKnownType(GetType(CertficateDealerPhoneLookUp)),
    ServiceKnownType(GetType(CertificateDealerSerialLookUp))>
    Public Interface ICertificateServiceV1

        <OperationContract(Name:="GetElitaHeader")>
        Function GetElitaHeader() As ElitaHeader

        <OperationContract(Name:="GetCertificate"),
            ElitaPermission(PermissionCodes.WS_Cert_GetDetails),
            FaultContract(GetType(CertificateNotFound)),
            FaultContract(GetType(DealerNotFoundFault)),
            FaultContract(GetType(CompanyNotFoundFault)),
            FaultContract(GetType(ValidationFault)),
            FaultContract(GetType(InvalidIdentificationNumber)),
            FaultContract(GetType(InvalidSerialnumber))>
        Function GetCertificate(ByVal request As GetCertificateRequest) As GetCertificateResponse

        <OperationContract(Name:="CancelByExternalCertNum"),
            ElitaPermission(PermissionCodes.WS_PS_CANCEL),
            FaultContract(GetType(DealerNotFoundFault)),
            FaultContract(GetType(CompanyNotFoundFault)),
            FaultContract(GetType(ValidationFault))>
        Function CancelByExternalCertNum(ByVal request As CancelByExternalCertNumRequest) As CancelByExternalCertNumResponse

        <OperationContract(Name:="SearchAppleCertificateByImei"),
            ElitaPermission(PermissionCodes.WS_Cert_GetDetails),
            FaultContract(GetType(CertificateNotFound)),
            FaultContract(GetType(CompanyNotFoundFault)),
            FaultContract(GetType(DealerNotFoundFault))>
        Function SearchAppleCertificateByImei(ByVal request As SearchAppleCertificateByImeiRequest) As SearchAppleCertificateByImeiResponse
    End Interface
End Namespace
