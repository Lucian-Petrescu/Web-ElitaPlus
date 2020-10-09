Imports Microsoft.IdentityModel.Protocols.OpenIdConnect
Imports Microsoft.IdentityModel.Tokens
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OpenIdConnect
Imports Owin
Imports System.Collections.Generic
Imports System.Security.Claims
Imports System.Threading.Tasks

<Assembly: OwinStartup(GetType(Startup))>
Public Class Startup
    Private ReadOnly _clientId As String = ConfigurationManager.AppSettings("Okta.ClientId")
    Private ReadOnly _redirectUri As String = ConfigurationManager.AppSettings("Okta.RedirectUri")
    Private ReadOnly _authority As String = ConfigurationManager.AppSettings("Okta.OrgUri")
    Private ReadOnly _clientSecret As String = ConfigurationManager.AppSettings("Okta.ClientSecret")
    Private ReadOnly _postLogoutRedirectUri As String = ConfigurationManager.AppSettings("Okta.PostLogoutRedirectUri")
    Private ReadOnly _responseType As String = ConfigurationManager.AppSettings("Okta.ResponseType")
    Private ReadOnly _scope As String = ConfigurationManager.AppSettings("Okta.Scope")

    Public Sub Configuration(app As IAppBuilder)
        app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType)
        app.UseCookieAuthentication(New CookieAuthenticationOptions())

        Dim requireHttpsMetadata As Boolean = True
        If (EnvironmentContext.Current.Environment <> Environments.Development) Then 'For Non Local


            app.UseOpenIdConnectAuthentication(New OpenIdConnectAuthenticationOptions With {
                .RequireHttpsMetadata = requireHttpsMetadata,
                .ClientId = _clientId,
                .ClientSecret = _clientSecret,
                .Authority = _authority,
                .RedirectUri = _redirectUri,
                .ResponseType = _responseType,
                .Scope = _scope,
                .PostLogoutRedirectUri = _postLogoutRedirectUri,
                .TokenValidationParameters = New TokenValidationParameters With {
                    .NameClaimType = "name",
                    .RoleClaimType = "groups",
                    .ValidateIssuer = True,
                    .ValidateAudience = True,
                    .ValidateLifetime = True,
                    .ValidateIssuerSigningKey = True
                },
                .Notifications = New OpenIdConnectAuthenticationNotifications() With {
                    .SecurityTokenValidated =
                        Function(context)
                            Try '
                                Dim principal As ElitaPlusPrincipal
                                Dim oAuthentication As New Authentication

                                Dim identity As ClaimsIdentity = context.AuthenticationTicket.Identity
                                identity.AddClaim(New Claim("id_token", context.ProtocolMessage.IdToken))

                                'Dim networkId As String = identity.Claims.FirstOrDefault(Function(claim) claim.Type = "preferred_username").Value.Substring(0, 6)
                                Dim networkId As String = identity.Claims.FirstOrDefault(Function(claim) claim.Type = "preferred_username").Value
                                If networkId.IndexOf("@", StringComparison.Ordinal) > 0 Then
                                    networkId = networkId.Substring(0, networkId.IndexOf("@", StringComparison.Ordinal))
                                End If

                                ' Get the OKTA groups for this user
                                Dim groups As List(Of String) = identity.Claims.Where(Function(claim) claim.Type = "groups").Select(Function(claim) claim.Value).ToList()

                                ' Create the principal
                                principal = oAuthentication.CreatePrincipalBasedOnExternalGroups(networkId, groups)
                                principal.IdToken = context.ProtocolMessage.IdToken

                                HttpContext.Current.Session(ElitaPlusPage.PRINCIPAL_SESSION_KEY) = principal

                                Threading.Thread.CurrentPrincipal = DirectCast(HttpContext.Current.Session(ElitaPlusPage.PRINCIPAL_SESSION_KEY), ElitaPlusPrincipal)
                                HttpContext.Current.User = Threading.Thread.CurrentPrincipal

                                'will have to test if the User is part of either 'RegularGroup' or 'SecureGroup' or 'DataProtectionGroup'
                                If groups Is Nothing OrElse (Not groups.Contains(Elita.Configuration.ElitaConfig.Current.Security.RegularGroup) _
                                    AndAlso Not groups.Contains(Elita.Configuration.ElitaConfig.Current.Security.SecureGroup) _
                                    AndAlso Not groups.Contains(Elita.Configuration.ElitaConfig.Current.Security.DataProtectionGroup)) Then

                                    context.HandleResponse()

                                    ' Using the session to retrieve the message in the login form because okta doesn't accept 
                                    ' an url with ?key=value if Not set completely in the whitelist (okta online configuration)
                                    Dim sLoginMessage As String = TranslationBase.TranslateLabelOrMessage(ELPWebConstants.UI_INVALID_LOGIN_RIGHTS_ERR_MSG, TranslationBase.Get_EnglishLanguageID)
                                    HttpContext.Current.Session(ELPWebConstants.SESSION_LOGIN_ERROR_MESSAGE) = sLoginMessage

                                    ' at the end, clear okta authentication and redirect to the postLogoutRedirectUri url
                                    context.OwinContext.Authentication.SignOut(
                                        CookieAuthenticationDefaults.AuthenticationType,
                                        OpenIdConnectAuthenticationDefaults.AuthenticationType
                                    )
                                End If

                                Return Task.CompletedTask
                            Catch ex As Exception '
                                HttpContext.Current.Session(ELPWebConstants.Session_Elita_Authz_Exception) = "Authorization to Elita failed. User not configured correctly or not found. Please see with your administrator to get access through Elita." '

                                Return Task.CompletedTask
                            End Try '

                        End Function,
                    .AuthenticationFailed =
                        Function(n)
                            If (String.Equals(n.ProtocolMessage.Error, "access_denied", StringComparison.Ordinal)) Then
                                n.HandleResponse()

                                'Dim sLoginMessage As String = TranslationBase.TranslateLabelOrMessage(ELPWebConstants.UI_INVALID_LOGIN_ERR_MSG, TranslationBase.Get_EnglishLanguageID)
                                'This case should happen only if inside the Okta configuration (which is done by the Assurant Okta team), the user is not expected to be able to use the Elita application.
                                'So even if the user is existing in Elita and has rights, it won't work unless this user has the right to use Elita from the Okta side.
                                HttpContext.Current.Session(ELPWebConstants.SESSION_LOGIN_ERROR_MESSAGE) = "Authentication failed, please see with your administrator to get access through Okta."
                                n.Response.Redirect(_postLogoutRedirectUri)
                            End If

                            Return Task.FromResult(Of Object)(Nothing)
                        End Function,
                    .RedirectToIdentityProvider =
                        Function(n)
                            If n.ProtocolMessage.RequestType = OpenIdConnectRequestType.Logout Then
                                Dim idToken As String = ElitaPlusPrincipal.Current.IdToken

                                If Not String.IsNullOrEmpty(idToken) Then
                                    n.ProtocolMessage.IdTokenHint = idToken
                                End If
                            End If

                            Return Task.CompletedTask
                        End Function
                }
            })

        End If
    End Sub
End Class
