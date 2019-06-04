Imports System
Imports System.Security.Principal

Namespace Security

    Public Class PasswordPrincipal
        Inherits GenericPrincipal

        Public Sub New(ByVal identity As PasswordIdentity, ByVal roles As String())
            MyBase.New(identity, roles)
        End Sub
    End Class

End Namespace