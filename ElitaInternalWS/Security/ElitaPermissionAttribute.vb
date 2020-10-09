Imports System

Namespace Security
    <AttributeUsage(AttributeTargets.Method, AllowMultiple:=False)> _
    Public Class ElitaPermissionAttribute
        Inherits Attribute

        Private ReadOnly _permissionCode As String

        Public ReadOnly Property PermissionCode
            Get
                Return _permissionCode
            End Get
        End Property

        Public Sub New(permissionCode As String)

            If ((permissionCode Is Nothing) OrElse (permissionCode.Trim().Length = 0)) Then
                Throw New ArgumentNullException("permissionCode")
            End If

            _permissionCode = permissionCode

        End Sub

    End Class

    Public Class PermissionCodes
        Private Sub New()

        End Sub

        Public Const WS_Claim_GetClaims As String = "WS-CLM-GCLM"
        Public Const WS_Claim_UpdateClaims As String = "WS-CLM-UC"
        Public Const WS_Claim_GetClaimDetails As String = "WS-CLM-GCLMDTL"
        Public Const WS_ClaimDocument_Attach As String = "WS-CLMDOC-A"
        Public Const WS_ClaimDocument_Download As String = "WS-CLMDOC-D"
        Public Const WS_ServiceOrder_Download As String = "WS-SODOC-D"
        Public Const WS_Cert_GetDetails As String = "WS-CRT-GCRT"
        Public Const WS_Cert_AttachImage As String = "WS-CRT-AI"
        Public Const WS_Cert_DownloadImage As String = "WS-CRT-DI"
        Public Const WS_CHLMobileSCPortal_GetCertClaimInfo As String = "WS-CHLMOBILESCPORTAL-CRTCLM"
        Public Const WS_FVSTMobileApplicationService_GetCertificateInfo As String = "WS-FVSTMOBILEAPPSVC-GETCERT"
        Public Const WS_ClarMaxValueService_GetCertificateInfo As String = "WS-CLARMAXVALSVC-GETCERT"
        Public Const WS_CertUpgradeService_GetCertificateInfo As String = "WS-CERTUPGRDSVC-GETCERT"
        Public Const WS_CertCustomerInfoService_GetCertificateInfo As String = "WS-CERTCUSTINFOSVC-GETCERT"
        Public Const WS_GWPIL_GetCertificate As String = "WS-GWPIL-GETCERT"
        Public Const WS_TISA_ClaimService As String = "WS-TISA-CS"
        Public Const WS_CHLMovistarUpgrade_GetCertCustomerInfo As String = "WS_CHLMOVISTARUPG_GETCERTCUST"
        Public Const WS_GOOW_GoogleService As String = "WS-GOOW-GS"
        Public Const WS_SNMP_ClaimService As String = "WS-SNMP-CS"
        Public Const WS_GeographicServices_GetRegionsAndComunas As String = "WS_GS_GETREGIONSANDCOMMUNAS"
        Public Const WS_PS_CANCEL As String = "WS-PS-CANCEL"
        Public Const WS_ESC_ClaimService As String = "WS-ESC-CS"
        Public Const WS_AccountingService_SendFile As String = "WS-ACCT-SNDFILE"
        Public Const WS_SFRPOLICY_GetCertificate As String = "WS-SFR-GETCERT"

        'US 203684
        Public Const WS_TIMB_GetCertCustomerInfo As String = "WS_TIMB_GetCertCustomerInfo"

        'US 203685
        Public Const WS_TIMB_GetCertClaimInfo As String = "WS_TIMB_GetCertClaimInfo"

        'US 175798
        Public Const WS_APR_UpdateImei As String = "WS-APR-UPDATE-IMEI"

    End Class
End Namespace

