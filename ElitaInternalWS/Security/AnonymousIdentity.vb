Imports System.Security.Principal

Namespace Security
    Public Class AnonymousIdentity
        Inherits GenericIdentity

        Sub New(name As String)
            MyBase.New(name)
        End Sub

        Sub New(name As String, type As String)
            MyBase.New(name, type)
        End Sub

        Public Overrides ReadOnly Property IsAuthenticated As Boolean
            Get
                Return False
            End Get
        End Property

    End Class
End Namespace

