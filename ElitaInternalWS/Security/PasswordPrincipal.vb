Imports System
Imports System.Security.Principal

Namespace Security

    Public Class PasswordPrincipal
        Inherits GenericPrincipal

        Public Sub New(identity As PasswordIdentity, roles As String())
            MyBase.New(identity, roles)
        End Sub
    End Class

End Namespace