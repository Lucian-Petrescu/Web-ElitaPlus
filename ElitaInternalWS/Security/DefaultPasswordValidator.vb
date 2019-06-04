'Imports System
Imports System.IdentityModel.Selectors
Imports System.IdentityModel.Tokens

Namespace Security

    Public Class DefaultPasswordValidator
        Inherits UserNamePasswordValidator

        Public Overrides Sub Validate(ByVal userName As String, ByVal password As String)
            ' Check if the username exists
            If (String.IsNullOrEmpty(userName)) Then
                Throw New SecurityTokenException("NoUserNameProvided")
            End If
            
        End Sub


    End Class

End Namespace
