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
    Private ReadOnly clientId As String = ConfigurationManager.AppSettings("okta:ClientId")
    Private ReadOnly redirectUri As String = ConfigurationManager.AppSettings("okta:RedirectUri")
    Private ReadOnly authority As String = ConfigurationManager.AppSettings("okta:OrgUri")
    Private ReadOnly clientSecret As String = ConfigurationManager.AppSettings("okta:ClientSecret")
    Private ReadOnly postLogoutRedirectUri As String = ConfigurationManager.AppSettings("okta:PostLogoutRedirectUri")
    Private ReadOnly responseType As String = ConfigurationManager.AppSettings("okta:ResponseType")
    Private ReadOnly scope As String = ConfigurationManager.AppSettings("okta:Scope")

    Public Sub Configuration(ByVal app As IAppBuilder)
        app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType)
        app.UseCookieAuthentication(New CookieAuthenticationOptions())

        Dim requireHttpsMetadata As Boolean = True
        'If (EnvironmentContext.Current.Environment = Environments.Development) Then
        '    requireHttpsMetadata = False
        'End If

        app.UseOpenIdConnectAuthentication(New OpenIdConnectAuthenticationOptions With {
                .RequireHttpsMetadata = requireHttpsMetadata,
                .ClientId = clientId,
                .ClientSecret = clientSecret,
                .Authority = authority,
                .RedirectUri = redirectUri,
                .ResponseType = responseType,
                .Scope = scope,
                .PostLogoutRedirectUri = postLogoutRedirectUri,
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
                            Dim principal As ElitaPlusPrincipal
                            Dim oAuthentication As New Authentication

                            Dim identity As ClaimsIdentity = context.AuthenticationTicket.Identity
                            identity.AddClaim(New Claim("id_token", context.ProtocolMessage.IdToken))

                            Dim networkId As String = identity.Claims.FirstOrDefault(Function(claim) claim.Type = "preferred_username").Value.Substring(0, 6)
                            ' Get the OKTA groups for this user
                            Dim groups As List(Of String) = identity.Claims.Where(Function(claim) claim.Type = "groups").Select(Function(claim) claim.Value).ToList()

                            ' Create the principal
                            principal = oAuthentication.CreatePrincipalBasedOnExternalGroups(networkId, groups)
                            principal.IdToken = context.ProtocolMessage.IdToken

                            HttpContext.Current.Session(ElitaPlusPage.PRINCIPAL_SESSION_KEY) = principal

                            System.Threading.Thread.CurrentPrincipal = DirectCast(HttpContext.Current.Session(ElitaPlusPage.PRINCIPAL_SESSION_KEY), ElitaPlusPrincipal)
                            HttpContext.Current.User = System.Threading.Thread.CurrentPrincipal

                            'will have to test if the User is part of either 'RegularGroup' or 'SecureGroup' or 'DataProtectionGroup'
                            If groups Is Nothing OrElse (Not groups.Contains(Elita.Configuration.ElitaConfig.Current.Security.RegularGroup) _
                                AndAlso Not groups.Contains(Elita.Configuration.ElitaConfig.Current.Security.SecureGroup) _
                                AndAlso Not groups.Contains(Elita.Configuration.ElitaConfig.Current.Security.DataProtectionGroup)) Then

                                context.HandleResponse()

                                ' Using the session to retrieve the message in the login form because okta doesn't accept 
                                ' an url with ?key=value if Not set completely in the whitelist (okta online configuration)
                                Dim sLoginMessage As String = TranslationBase.TranslateLabelOrMessage(ELPWebConstants.UI_INVALID_LOGIN_RIGHTS_ERR_MSG, TranslationBase.Get_EnglishLanguageID)
                                HttpContext.Current.Session(ELPWebConstants.SESSION_LOGIN_ERROR_MESSAGE) = sLoginMessage

                                ' at the end, clear okta authentification and rediret to the postLogoutRedirectUri url
                                context.OwinContext.Authentication.SignOut(
                                    CookieAuthenticationDefaults.AuthenticationType,
                                    OpenIdConnectAuthenticationDefaults.AuthenticationType
                                )
                            End If

                            Return Task.CompletedTask
                        End Function,
                    .AuthenticationFailed =
                        Function(n)
                            If (String.Equals(n.ProtocolMessage.Error, "access_denied", StringComparison.Ordinal)) Then
                                n.HandleResponse()

                                Dim sLoginMessage As String = TranslationBase.TranslateLabelOrMessage(ELPWebConstants.UI_INVALID_LOGIN_ERR_MSG, TranslationBase.Get_EnglishLanguageID)
                                HttpContext.Current.Session(ELPWebConstants.SESSION_LOGIN_ERROR_MESSAGE) = sLoginMessage

                                n.Response.Redirect(postLogoutRedirectUri)
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
    End Sub
End Class
