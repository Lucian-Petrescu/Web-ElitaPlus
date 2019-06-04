'Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IdentityModel.Policy
Imports System.IdentityModel.Selectors

Namespace Security

    Public Class PasswordSecurityTokenAuthenticator
        Inherits CustomUserNameSecurityTokenAuthenticator

        Public Sub New(ByVal validator As UserNamePasswordValidator)
            MyBase.New(validator)
        End Sub

        Protected Overrides Function ValidateUserNamePasswordCore( _
                ByVal userName As String, ByVal password As String) As  _
                                        ReadOnlyCollection(Of IAuthorizationPolicy)
            ' Get the existing policies
            Dim currentPolicies As ReadOnlyCollection(Of IAuthorizationPolicy) = _
                         MyBase.ValidateUserNamePasswordCore(userName, password)
            Dim newPolicies As List(Of IAuthorizationPolicy) = New List(Of IAuthorizationPolicy)

            ' Check if there are existing policies to add
            If (Not currentPolicies Is Nothing) Then
                ' Add the existing policies
                newPolicies.AddRange(currentPolicies)
            End If

            ' Add the custom authorization policy
            newPolicies.Add(New PasswordAuthorizationPolicy(userName, password))

            Return newPolicies.AsReadOnly()
        End Function

    End Class

End Namespace