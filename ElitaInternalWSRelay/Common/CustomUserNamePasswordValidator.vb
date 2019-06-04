Imports System.IdentityModel.Selectors
Imports System.IdentityModel.Tokens
Imports System.ServiceModel


Public Class CustomUserNamePasswordValidator
    Inherits UserNamePasswordValidator

    Public Overrides Sub Validate(ByVal userName As String, ByVal password As String)
        'Do Nothing
    End Sub
End Class
