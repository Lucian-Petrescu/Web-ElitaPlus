'Imports System.Diagnostics
Imports System.IdentityModel.Selectors
Imports System.IdentityModel.Tokens
Imports System.ServiceModel.Security

Namespace Security

    ' Represents a System.IdentityModel.Selectors.SecurityTokenManager implementation that 
    ' provides security token serializers based on the 
    ' System.ServiceModel.Description.ServiceCredentials configured on the service.
    Public Class PasswordSecurityTokenManager
        Inherits ServiceCredentialsSecurityTokenManager

        Public Sub New(credentials As PasswordServiceCredentials)
            MyBase.New(credentials)
        End Sub
         
        Public Overrides Function CreateSecurityTokenAuthenticator( _
             tokenRequirement As SecurityTokenRequirement, _
             ByRef outOfBandTokenResolver As SecurityTokenResolver) As SecurityTokenAuthenticator

            If (tokenRequirement.TokenType = SecurityTokenTypes.UserName) Then
                outOfBandTokenResolver = Nothing

                ' Get the current validator
                Dim validator As UserNamePasswordValidator = _
                    ServiceCredentials.UserNameAuthentication.CustomUserNamePasswordValidator

                ' Ensure that a validator exists
                If (validator Is Nothing) Then
                    Trace.TraceWarning("Resources.NoCustomUserNamePasswordValidatorConfigured")
                    validator = New DefaultPasswordValidator
                End If

                Return New PasswordSecurityTokenAuthenticator(validator)
            End If

            Return MyBase.CreateSecurityTokenAuthenticator(tokenRequirement, outOfBandTokenResolver)
        End Function

    End Class

End Namespace