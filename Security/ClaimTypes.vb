
Public NotInheritable Class ClaimTypes
    Private Sub New()
    End Sub
    Const ClaimTypeNamespace As String = "http://www.assurant.com/ElitaPlus/claims"

    Const m_networkId As String = claimTypeNamespace & "/networkId"
    Const m_languageCode As String = ClaimTypeNamespace & "/languageCode"
    Public Const CultureCode As String = ClaimTypeNamespace & "/cultureCode"
    Const m_languageId As String = claimTypeNamespace & "/languageId"
    Const m_companyId As String = claimTypeNamespace & "/companyId"
    Const m_companyCode As String = claimTypeNamespace & "/companyCode"
    Const m_companyPaymentLimit As String = claimTypeNamespace & "/companyPaymentLimit"
    Const m_companyAuthorizationLimit As String = claimTypeNamespace & "/companyAuthorizationLimit"
    Const m_permissionId As String = claimTypeNamespace & "/m_permissionId"
    Const m_permissionCode As String = claimTypeNamespace & "/m_permissionCode"
    Const m_x509Thumbprint As String = claimTypeNamespace & "/x509Certificate"
    Const m_clientIP As String = claimTypeNamespace & "/clientIP"
    Const m_clientIPEffectiveDate As String = claimTypeNamespace & "/effective"
    Const m_clientIPexpirationDate As String = claimTypeNamespace & "/expiration"
    Const m_x509ExpirationDate As String = claimTypeNamespace & "/effective"
    Const m_x509EffectiveDate As String = claimTypeNamespace & "/expiration"


    Public Shared ReadOnly Property NetworkId As String
        Get
            Return m_networkId
        End Get
    End Property

    Public Shared ReadOnly Property LanguageCode As String
        Get
            Return m_languageCode
        End Get
    End Property

    Public Shared ReadOnly Property LanguageId As String
        Get
            Return m_languageId
        End Get
    End Property
    Public Shared ReadOnly Property CompanyId As String
        Get
            Return m_companyId
        End Get
    End Property

    Public Shared ReadOnly Property CompanyAuthorizationLimit As String
        Get
            Return m_companyAuthorizationLimit
        End Get
    End Property

    Public Shared ReadOnly Property CompanyPaymentLimit As String
        Get
            Return m_companyPaymentLimit
        End Get
    End Property

    Public Shared ReadOnly Property CompanyCode As String
        Get
            Return m_companyCode
        End Get
    End Property

    Public Shared ReadOnly Property PermissionId As String
        Get
            Return m_permissionId
        End Get
    End Property

    Public Shared ReadOnly Property PermissionCode As String
        Get
            Return m_permissionCode
        End Get
    End Property

    Public Shared ReadOnly Property X509Thumbprint As String
        Get
            Return m_x509Thumbprint
        End Get
    End Property

    Public Shared ReadOnly Property ClientIP As String
        Get
            Return m_clientIP
        End Get
    End Property
    Public Shared ReadOnly Property ClientIPEffectiveDate As String
        Get
            Return m_clientIPEffectiveDate
        End Get
    End Property

    Public Shared ReadOnly Property ClientIPExpirationDate As String
        Get
            Return m_clientIPexpirationDate
        End Get
    End Property

    Public Shared ReadOnly Property X509EffectiveDate As String
        Get
            Return m_x509EffectiveDate
        End Get
    End Property

    Public Shared ReadOnly Property X509ExpirationDate As String
        Get
            Return m_x509ExpirationDate
        End Get
    End Property

End Class
