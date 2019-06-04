'Imports System
Imports System.Security.Principal

Namespace Security

    Public Class PasswordIdentity
        Inherits GenericIdentity

#Region "Variables"

        Private ReadOnly _isAuthenticated As Boolean
        Private msPassword As String

#End Region

        Public Sub New(ByVal oUserName As String, ByVal oPassword As String, ByVal isAuthenticated As Boolean, _
                       ByVal authenticationType As String)
            MyBase.New(oUserName, authenticationType)
            Password = oPassword
            _isAuthenticated = isAuthenticated
        End Sub
            

        ' Gets a value that indicates whether the user has been authenticated.
        Public Overrides ReadOnly Property IsAuthenticated() As Boolean
            Get
                Return (_isAuthenticated AndAlso MyBase.IsAuthenticated)
            End Get
        End Property

        Public Property Password() As String
            Get
                Return msPassword
            End Get
            Set(ByVal value As String)
                msPassword = value
            End Set
        End Property

    End Class

End Namespace