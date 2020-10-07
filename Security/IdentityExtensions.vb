Imports System.IdentityModel.Claims
Imports System.Runtime.CompilerServices
Imports System.Security.Principal
Imports Assurant.Elita.Configuration
Imports Assurant.Elita.Security

Public Module IdentityExtensions

    <Extension()>
    Public Function GetNetworkId(pPrincipal As IPrincipal) As String
        Dim identity As IElitaClaimsIdentity
        identity = TryCast(pPrincipal.Identity, IElitaClaimsIdentity)
        If (identity Is Nothing) Then
            Return String.Empty
        Else
            Dim networkId As String = (From c As Claim In identity.Claims
                                       Where c.ClaimType = ClaimTypes.NetworkId
                                       Select c.Resource).FirstOrDefault()
            If (networkId Is Nothing) Then
                Return String.Empty
            Else
                Return DirectCast(networkId, String)
            End If

        End If
    End Function

    <Extension()>
    Public Function GetCultureCode(pPrincipal As IPrincipal) As String
        Dim identity As IElitaClaimsIdentity
        identity = TryCast(pPrincipal.Identity, IElitaClaimsIdentity)
        If (identity Is Nothing) Then
            Return String.Empty
        Else
            Dim language As Object = (From c As Claim In identity.Claims
                                      Where c.ClaimType = ClaimTypes.CultureCode
                                      Select c.Resource).FirstOrDefault()
            If (language Is Nothing) Then
                Return String.Empty
            Else
                Return DirectCast(language, String)
            End If

        End If
    End Function

    <Extension()>
    Public Function GetLanguageCode(pPrincipal As IPrincipal) As String
        Dim identity As IElitaClaimsIdentity
        identity = TryCast(pPrincipal.Identity, IElitaClaimsIdentity)
        If (identity Is Nothing) Then
            Return String.Empty
        Else
            Dim language As Object = (From c As Claim In identity.Claims
                                      Where c.ClaimType = ClaimTypes.LanguageCode
                                      Select c.Resource).FirstOrDefault()
            If (language Is Nothing) Then
                Return String.Empty
            Else
                Return DirectCast(language, String)
            End If

        End If
    End Function

    <Extension()>
    Public Function GetLanguageId(pPrincipal As IPrincipal) As Nullable(Of Guid)
        Dim identity As IElitaClaimsIdentity
        identity = TryCast(pPrincipal.Identity, IElitaClaimsIdentity)
        If (identity Is Nothing) Then
            Return Nothing
        Else
            Dim language As Object = (From c As Claim In identity.Claims
                                      Where c.ClaimType = ClaimTypes.LanguageId
                                      Select c.Resource).FirstOrDefault()
            If (language Is Nothing) Then
                Return Nothing
            Else
                Return New Guid(DirectCast(language, String))
            End If

        End If
    End Function

    <Extension()>
    Public Function HasPermission(pPrincipal As IPrincipal, pPermissionCode As String) As Boolean
        Dim identity As IElitaClaimsIdentity
        identity = TryCast(pPrincipal.Identity, IElitaClaimsIdentity)
        If (identity Is Nothing) Then
            Return False
        Else
            pPermissionCode = pPermissionCode.ToUpperInvariant()
            Dim permissionCode As String = (From c As Claim In identity.Claims
                                            Where c.ClaimType = ClaimTypes.PermissionCode AndAlso DirectCast(c.Resource, String) = pPermissionCode
                                            Select DirectCast(c.Resource, String)).FirstOrDefault()
            If (permissionCode Is Nothing) Then
                Return False
            Else
                Return True
            End If

        End If
    End Function

    <Extension()>
    Public Function HasCompany(pPrincipal As IPrincipal, pCompanyCode As String) As Boolean
        Dim identity As IElitaClaimsIdentity
        identity = TryCast(pPrincipal.Identity, IElitaClaimsIdentity)
        If (identity Is Nothing) Then
            Return False
        Else
            pCompanyCode = pCompanyCode.ToUpperInvariant()
            Dim permissionCode As String = (From c As Claim In identity.Claims
                                            Where c.ClaimType = ClaimTypes.CompanyCode AndAlso DirectCast(c.Resource, String) = pCompanyCode
                                            Select DirectCast(c.Resource, String)).FirstOrDefault()
            If (permissionCode Is Nothing) Then
                Return False
            Else
                Return True
            End If

        End If
    End Function

    <Extension()>
    Public Function HasCompany(pPrincipal As IPrincipal, pCompanyId As Guid) As Boolean
        Dim identity As IElitaClaimsIdentity
        identity = TryCast(pPrincipal.Identity, IElitaClaimsIdentity)
        If (identity Is Nothing) Then
            Return False
        Else
            Dim permissionCode As String = (From c As Claim In identity.Claims
                                            Where c.ClaimType = ClaimTypes.CompanyId AndAlso DirectCast(c.Resource, String) = pCompanyId.ToString()
                                            Select DirectCast(c.Resource, String)).FirstOrDefault()
            If (permissionCode Is Nothing) Then
                Return False
            Else
                Return True
            End If

        End If
    End Function

#Region "X509 Certificate"
    <Extension>
    Public Function HasX509Thumbprint(pPrincipal As IPrincipal, Thumbprint As String) As Boolean

        Dim identity As IElitaClaimsIdentity
        identity = TryCast(pPrincipal.Identity, IElitaClaimsIdentity)

        If (identity Is Nothing) Then
            Return False
        Else

            Dim x509Claim As String = (From c As Claim In identity.Claims
                                       Where c.ClaimType = ClaimTypes.X509Thumbprint AndAlso DirectCast(c.Resource, String) = Thumbprint
                                       Select DirectCast(c.Resource, String)).FirstOrDefault()

            ''(From c In identity.Claims Where c.Type == Security.ClaimTypes.X509Thumbprint && (c.Properties.Count! = 0 && c.Properties(ClaimPropertyNames.Code) == Thumbprint) Select c).FirstOrDefault()
            If (String.IsNullOrEmpty(x509Claim)) Then
                Return False
            Else
                Return True
            End If

        End If
    End Function
    <Extension>
    Public Function IsX509CertificateValid(pPrincipal As IPrincipal, Thumbprint As String) As Boolean

        Dim identity As IElitaClaimsIdentity
        identity = TryCast(pPrincipal.Identity, IElitaClaimsIdentity)

        Dim x509ExpDateClaim As String = (From c As Claim In identity.Claims
                                          Where c.ClaimType = ClaimTypes.X509ExpirationDate AndAlso DirectCast(c.Resource, String) = Thumbprint
                                          Select DirectCast(c.Right, String)).FirstOrDefault()

        ''(From c In identity.Claims Where c.Type == Security.ClaimTypes.X509Thumbprint && (c.Properties.Count! = 0 && c.Properties(ClaimPropertyNames.Code) == Thumbprint) Select c).FirstOrDefault()
        'Dim expirationDate As DateTime = clientIPClai
        If x509ExpDateClaim <> String.Empty AndAlso Convert.ToDateTime(x509ExpDateClaim) < DateTime.Now Then
            Return False
        Else
            Return True

        End If
    End Function
#End Region

#Region "Client IP"
    <Extension>
    Public Function HasClientIP(pPrincipal As IPrincipal, ClientIp As String) As Boolean

        Dim identity As IElitaClaimsIdentity
        identity = TryCast(pPrincipal.Identity, IElitaClaimsIdentity)

        If (identity Is Nothing) Then
            Return False
        Else

            Dim clientIPClaim As String = (From c As Claim In identity.Claims
                                           Where c.ClaimType = ClaimTypes.ClientIP AndAlso DirectCast(c.Resource, String) = ClientIp
                                           Select DirectCast(c.Resource, String)).FirstOrDefault()

            ''(From c In identity.Claims Where c.Type == Security.ClaimTypes.X509Thumbprint && (c.Properties.Count! = 0 && c.Properties(ClaimPropertyNames.Code) == Thumbprint) Select c).FirstOrDefault()
            If (String.IsNullOrEmpty(clientIPClaim)) Then
                Return False
            Else
                Return True
            End If

        End If
    End Function
    <Extension>
    Public Function IsClientIPValid(pPrincipal As IPrincipal, ClientIp As String) As Boolean

        Dim identity As IElitaClaimsIdentity
        identity = TryCast(pPrincipal.Identity, IElitaClaimsIdentity)

        Dim clientIPExpDateClaim As String = (From c As Claim In identity.Claims
                                              Where c.ClaimType = ClaimTypes.ClientIPExpirationDate AndAlso DirectCast(c.Resource, String) = ClientIp
                                              Select DirectCast(c.Right, String)).FirstOrDefault()

        ''(From c In identity.Claims Where c.Type == Security.ClaimTypes.X509Thumbprint && (c.Properties.Count! = 0 && c.Properties(ClaimPropertyNames.Code) == Thumbprint) Select c).FirstOrDefault()
        'Dim expirationDate As DateTime = clientIPClai
        If clientIPExpDateClaim <> String.Empty AndAlso Convert.ToDateTime(clientIPExpDateClaim) < DateTime.Now Then
            Return False
        Else
            Return True

        End If
    End Function
#End Region


    <Extension()>
    Public Function CanManageUsers(pPrincipal As IPrincipal) As Boolean
        if (String.IsNullOrWhiteSpace(ElitaConfig.Current.Security.UserManagementGroup)) 
            Return False
        End If
        Return LdapHelper.IsUserInGroup(pPrincipal.GetNetworkId(), ElitaConfig.Current.Security.UserManagementGroup)
    End Function


End Module
