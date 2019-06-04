'Imports System.Diagnostics
Imports System.IdentityModel.Selectors
'Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.ServiceModel.Security
'Imports System.Threading


Namespace Security

    Public Class PasswordServiceCredentials
        Inherits ServiceCredentials

        Public Sub New()

        End Sub
       
        Private Sub New(ByVal clone As PasswordServiceCredentials)
            MyBase.New(clone)
        End Sub

        Public Overrides Function CreateSecurityTokenManager() As SecurityTokenManager
            'Check if the current validation mode is for custom username password validation
            If (UserNameAuthentication.UserNamePasswordValidationMode _
                = UserNamePasswordValidationMode.Custom) Then
                Return New PasswordSecurityTokenManager(Me)
            End If

            Trace.TraceWarning("CustomUserNamePasswordValidationNotEnabled")

            Return MyBase.CreateSecurityTokenManager()
        End Function


        Protected Overrides Function CloneCore() As ServiceCredentials
            Return New PasswordServiceCredentials(Me)
        End Function

    End Class

End Namespace