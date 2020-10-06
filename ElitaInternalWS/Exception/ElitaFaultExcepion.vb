Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports System.ServiceModel

<DataContract(Name:="ElitaFault", Namespace:="http://elita.assurant.com/ElitaFault")>
Public Class ElitaFault

    Private Class FaultInfo
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Convention for Fault Code is EF-{0}{1}{2} where 0 => CER,CLM,CFG, 1 => Optional Sub Module 2 => Serial Number</remarks>
        Public Property FaultCode As String
        Public Property FaultMessage As String

    End Class


    Public Enum EnumFaultType
        'General
        InvalidRequest
        InvalidRequest2
        Database
        InvalidRequestMissingRequiredField
        RequestWasNotSuccessFull
        ServiceOrderNotFound

        'Certificate
        CertificateNotFound
        InvalidCoverageType
        CoverageNotFound
        MultipleCoveragesFound
        DuplicateCertificateFound
        CertificateNotEligibleForUpgrade
        InvalidIdentificationNumber
        InvalidSerialNumber
        InvalidImeiNumber
        CertificateNotEligibleForAppleCare
        CertificateBelongsToDiffDealer

        'Dealer
        DealerNotFound
        'Company
        CompanyNotFound
        'Country
        CountryNotFound
        'Region
        RegionNotFound
        'Customer
        CustomerNameNotFound
        'Email and Postal Code
        EmailandPostalCodeNotFoundFault


        'Claim
        ServiceCenterNotFound
        InvalidExtendedStatus
        InvalidDateOfLoss
        PriceListNotConfigured
        DefaultExtendedStatusNotFound
        MakeAndModelNotFound
        InvalidClaimType
        ClaimNotFound
        RepairClaimNotFulfilled
        ReplacementClaimFound
        RefurbishedCostRequired
        InvalidManufacturer
        RepairNotFound

        'Misc
        InvalidUpdateAction

    End Enum

    Private Shared ExceptionMessages As Dictionary(Of EnumFaultType, FaultInfo)

    Shared Sub New()
        ExceptionMessages = New Dictionary(Of EnumFaultType, FaultInfo)
        ExceptionMessages.Add(EnumFaultType.InvalidRequest, New FaultInfo() With {.FaultCode = "EF-CFG000001", .FaultMessage = "Please provide atleast one value"})
        ExceptionMessages.Add(EnumFaultType.InvalidRequestMissingRequiredField, New FaultInfo() With {.FaultCode = "EF-CFG000002", .FaultMessage = "Invalid Request. Required field has no value"})
        ExceptionMessages.Add(EnumFaultType.InvalidRequest2, New FaultInfo() With {.FaultCode = "EF-CFG000003", .FaultMessage = "Please provide only one value"})


        ExceptionMessages.Add(EnumFaultType.CertificateNotFound, New FaultInfo() With {.FaultCode = "EF-CER000001", .FaultMessage = "Certificate Not Found"})
        ExceptionMessages.Add(EnumFaultType.DuplicateCertificateFound, New FaultInfo() With {.FaultCode = "EF-CER000002", .FaultMessage = "Duplicate Certificate Found"})
        ExceptionMessages.Add(EnumFaultType.InvalidUpdateAction, New FaultInfo() With {.FaultCode = "EF-CER000003", .FaultMessage = "Invalid Update Action"})
        ExceptionMessages.Add(EnumFaultType.CoverageNotFound, New FaultInfo() With {.FaultCode = "EF-CERCVG003", .FaultMessage = "Coverage Not Found"})
        ExceptionMessages.Add(EnumFaultType.InvalidCoverageType, New FaultInfo() With {.FaultCode = "EF-CERCVG004", .FaultMessage = "Invalid Coverage Type"})
        ExceptionMessages.Add(EnumFaultType.CertificateNotEligibleForUpgrade, New FaultInfo() With {.FaultCode = "EF-CER000005", .FaultMessage = "Certificate Not Eligible For Upgrade"})
        ExceptionMessages.Add(EnumFaultType.InvalidIdentificationNumber, New FaultInfo() With {.FaultCode = "EF-CER000006", .FaultMessage = "Invalid Identification Number"})
        ExceptionMessages.Add(EnumFaultType.InvalidSerialNumber, New FaultInfo() With {.FaultCode = "EF-CER000007", .FaultMessage = "Invalid Serial Number"})
        ExceptionMessages.Add(EnumFaultType.CertificateBelongsToDiffDealer, New FaultInfo() With {.FaultCode = "EF-CER000008", .FaultMessage = "This switch-up program was bought in another APR"})


        ExceptionMessages.Add(EnumFaultType.CompanyNotFound, New FaultInfo() With {.FaultCode = "EF-CFGCMP001", .FaultMessage = "Company Not Found"})
        ExceptionMessages.Add(EnumFaultType.DealerNotFound, New FaultInfo() With {.FaultCode = "EF-CFGDLR0002", .FaultMessage = "Dealer Not Found"})
        ExceptionMessages.Add(EnumFaultType.CountryNotFound, New FaultInfo() With {.FaultCode = "EF-CFGCTY001", .FaultMessage = "Country Not Found"})
        ExceptionMessages.Add(EnumFaultType.RegionNotFound, New FaultInfo() With {.FaultCode = "GS-RNF000001", .FaultMessage = "Region Not Found"})


        ExceptionMessages.Add(EnumFaultType.ClaimNotFound, New FaultInfo() With {.FaultCode = "EF-CLM000001", .FaultMessage = "Claim Not Found"})
        ExceptionMessages.Add(EnumFaultType.DefaultExtendedStatusNotFound, New FaultInfo() With {.FaultCode = "EF-CLM000002", .FaultMessage = "Default Extended Status Not Found"})
        ExceptionMessages.Add(EnumFaultType.InvalidClaimType, New FaultInfo() With {.FaultCode = "EF=CLM000003", .FaultMessage = "Invalid Claim Type"})
        ExceptionMessages.Add(EnumFaultType.InvalidDateOfLoss, New FaultInfo() With {.FaultCode = "EF-CLM000004", .FaultMessage = "Invalid Date Of Loss"})
        ExceptionMessages.Add(EnumFaultType.InvalidExtendedStatus, New FaultInfo() With {.FaultCode = "EF-CLM000005", .FaultMessage = "Invalid Extended Status"})



        ExceptionMessages.Add(EnumFaultType.Database, New FaultInfo() With {.FaultCode = "EF-DB000001", .FaultMessage = "Unexpected Database Error"})
        ExceptionMessages.Add(EnumFaultType.RepairNotFound, New FaultInfo() With {.FaultCode = "EF-CERAPP008", .FaultMessage = "Repair Not Found"})

        ExceptionMessages.Add(EnumFaultType.CertificateNotEligibleForAppleCare, New FaultInfo() With {.FaultCode = "EF-CER000009", .FaultMessage = "Certificate Does Not Have Active AppleCare"})
        ExceptionMessages.Add(EnumFaultType.RequestWasNotSuccessFull, New FaultInfo() With {.FaultCode = "EF-CFG000004", .FaultMessage = "Invalid Request. Request was not successfull"})
        ExceptionMessages.Add(EnumFaultType.ServiceOrderNotFound, New FaultInfo() With {.FaultCode = "EF-CFG000005", .FaultMessage = "Service Unavailable. The Service is unavailable"})

        'ExceptionMessages.Add(EnumFaultType.MakeAndModelNotFound, New FaultInfo() With {})
        'ExceptionMessages.Add(EnumFaultType.MultipleCoveragesFound, New FaultInfo() With {})
        'ExceptionMessages.Add(EnumFaultType.PriceListNotConfigured, New FaultInfo() With {})
        'ExceptionMessages.Add(EnumFaultType.RefurbishedCostRequired, New FaultInfo() With {})
        'ExceptionMessages.Add(EnumFaultType.RepairClaimNotFulfilled, New FaultInfo() With {})
        'ExceptionMessages.Add(EnumFaultType.ReplacementClaimFound, New FaultInfo() With {})
        'ExceptionMessages.Add(EnumFaultType.ServiceCenterNotFound, New FaultInfo() With {})




    End Sub


    Private m_faultCode As String
    Private m_faultMessage As String


    <DataMember(IsRequired:=True, Name:="FaultCode")>
    Public Property FaultCode As String
        Get
            Return m_faultCode
        End Get
        Private Set(value As String)
            m_faultCode = value
        End Set
    End Property

    <DataMember(IsRequired:=True, Name:="FaultMessage")>
    Public Property FaultMessage As String
        Get
            Return m_faultMessage
        End Get
        Private Set(value As String)
            m_faultMessage = value
        End Set
    End Property

    Public Sub New(faultType As EnumFaultType)

        Dim fi As FaultInfo
        If (ExceptionMessages.TryGetValue(faultType, fi)) Then
            FaultCode = fi.FaultCode
            FaultMessage = fi.FaultMessage
        End If
    End Sub



End Class
