Imports System.IdentityModel.Selectors
Imports System.IdentityModel.Tokens
Imports Assurant.Elita.Audit

Namespace Security
    Public NotInheritable Class ElitaUserNamePasswordValidator
        Inherits UserNamePasswordValidator
        Public Overrides Sub Validate(userName As String, password As String)
            '' Authenticate User Name and Password for v2 services
            If (Not LdapAuthenticationManager.Current.ValidateLdapUser(userName, password)) Then

                SecurityAudit.RecordSecurityAudit(New SecurityAuditInfo With {
                        .ActionName = String.Empty,
                        .AuditSecurityTypeCode = AuditSecurityTypeCode.FailedAccess,
                        .ClientIPAddress = String.Empty,
                        .IPAddressChain = String.Empty,
                        .RequestUrl = String.Empty,
                        .UserName = userName,
                        .X509Certificate = String.Empty
                   })

                Throw New SecurityTokenException()
            End If
        End Sub
    End Class
End Namespace