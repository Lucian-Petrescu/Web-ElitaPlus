Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace SpecializedServices.Timb
    <ServiceContract(Namespace:="http://elita.assurant.com/SpecializedServices/TIMB", Name:="ICertificateClaimService")>
    Public Interface ICertificateClaimService

#Region "Operations"
        <OperationContract(Name:="GetElitaHeader")>
        Function GetElitaHeader() As ElitaHeader

        <OperationContract(Name:="GetCertClaimInfo"),
        ElitaPermission(PermissionCodes.WS_TIMB_GetCertClaimInfo),
        FaultContract(GetType(ElitaFault)),
        FaultContract(GetType(CompanyNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault)),
        FaultContract(GetType(ValidationFault)),
        FaultContract(GetType(DatabaseErrorFault))>
        Function GetCertClaimInfo(CertClaimInfo As CertificateClaimRequest) As CertClaimInfoResponseDC
#End Region

        ReadOnly Property UserID() As Guid
        ReadOnly Property LanguageID() As Guid

        Function ValidateCompany(CompanyId As Guid) As Boolean
    End Interface
End Namespace
