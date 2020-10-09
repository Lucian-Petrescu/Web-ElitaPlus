Imports System.ServiceModel
Imports ElitaInternalWS.Security

Namespace SpecializedServices.Goow
    ' NOTE: You can use the "Rename" command on the context menu to change the interface name "IGoogleService" in both code and config file together.
    <ServiceContract(Namespace:="http://elita.assurant.com/SpecialzedServices/Goow")>
    Public Interface IGoogleService

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_GOOW_GoogleService),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault)),
        FaultContract(GetType(CoverageNotFoundFault)),
        FaultContract(GetType(MultipleCoveragesFoundFault)),
        FaultContract(GetType(ServiceCenterNotFoundFault)),
        FaultContract(GetType(InvalidDateOfLossFault)),
        FaultContract(GetType(PriceListNotConfiguredFault)),
        FaultContract(GetType(InvalidClaimTypeFault))>
        Function CreateClaim(request As CreateClaimRequest) As CreateClaimResponse

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_GOOW_GoogleService),
        FaultContract(GetType(CompanyNotFoundFault)),
        FaultContract(GetType(ClaimNotFoundFault)),
        FaultContract(GetType(DealerNotFoundFault))>
        Sub DenyClaim(request As DenyClaimRequest)

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_GOOW_GoogleService),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CompanyNotFoundFault))>
        Function ComputeTax(request As ComputeTaxRequest) As ComputeTaxResponse


        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_GOOW_GoogleService),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault)),
        FaultContract(GetType(CompanyNotFoundFault))>
        Sub FulfillClaim(request As FulfillClaimRequest)


        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_GOOW_GoogleService),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault)),
        FaultContract(GetType(CompanyNotFoundFault))>
        Sub ChangeServiceCenter(request As ChangeServiceCenterRequest)

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_GOOW_GoogleService),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault)),
        FaultContract(GetType(CompanyNotFoundFault))>
        Sub PayClaim(request As PayClaimRequest)

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_GOOW_GoogleService),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault)),
        FaultContract(GetType(CoverageNotFoundFault)),
        FaultContract(GetType(CompanyNotFoundFault))>
        Sub ReturnDamagedDeviceAdvEx(request As ReturnDamagedDeviceAdvExRequest)


        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_GOOW_GoogleService),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault)),
        FaultContract(GetType(CompanyNotFoundFault))>
        Sub MaxDaysElapsedAdvEx(request As MaxDaysElapsedAdvExRequest)

        <OperationContract(),
        ElitaPermission(PermissionCodes.WS_GOOW_GoogleService),
        FaultContract(GetType(DealerNotFoundFault)),
        FaultContract(GetType(CertificateNotFoundFault)),
        FaultContract(GetType(CompanyNotFoundFault))>
        Function GetClaimInfo(request As GetClaimInfoRequest) As GetClaimInfoResponse

    End Interface
End Namespace