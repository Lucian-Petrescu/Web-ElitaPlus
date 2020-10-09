'Imports System
Imports System.Collections.Generic
'Imports System.Collections.ObjectModel
Imports System.IdentityModel.Policy
Imports System.Security.Principal
Imports System.IdentityModel.Selectors
Imports System.IdentityModel.Claims

Namespace Security

    Public Class PasswordAuthorizationPolicy
        Implements IAuthorizationPolicy

#Region "Variables"

        Private msId As String
        Private moIssuer As ClaimSet
        Private msPassword As String
        Private msUserName As String

#End Region

#Region "Properties"

        'Gets a string that identifies this authorization component.
        Public ReadOnly Property Id() As String Implements IAuthorizationPolicy.Id
            Get
                Return msId
            End Get
        End Property

        Public WriteOnly Property WriteId() As String
            Set(value As String)
                msId = value
            End Set
        End Property

        ' Gets a claim set that represents the issuer of the authorization policy.
        Public ReadOnly Property Issuer() As ClaimSet Implements IAuthorizationPolicy.Issuer
            Get
                Return moIssuer
            End Get

        End Property

        ' Gets a claim set that represents the issuer of the authorization policy.
        Private WriteOnly Property WriteIssuer() As ClaimSet
            Set(value As ClaimSet)
                moIssuer = value
            End Set
        End Property


        ' Gets or sets the password.
        Private Property Password() As String
            Get
                Return msPassword
            End Get
            Set(value As String)
                msPassword = value
            End Set
        End Property

        ' Gets or sets the name of the user.
        Private Property UserName() As String
            Get
                Return msUserName
            End Get
            Set(value As String)
                msUserName = value
            End Set
        End Property

#End Region

        Public Sub New(oUserName As String, oPassword As String)
            Const UserNameParameterName As String = "userName"

            If (String.IsNullOrEmpty(oUserName)) Then
                Throw New ArgumentNullException(UserNameParameterName)
            End If

            WriteId = Guid.NewGuid().ToString()
            WriteIssuer = ClaimSet.System
            UserName = oUserName
            Password = oPassword
        End Sub

        Private Function IsIdentityMatch(oIdentity As IIdentity) As Boolean
            If ((TypeOf (oIdentity) Is GenericIdentity) _
                AndAlso (String.Equals(oIdentity.Name, UserName, _
                                              StringComparison.OrdinalIgnoreCase))) Then
                Return True
            Else
                Return False
            End If

        End Function


        ' Evaluates whether a user meets the requirements for this authorization policy.
        Public Function Evaluate(evaluationContext As EvaluationContext, ByRef state As Object) _
                                            As Boolean Implements IAuthorizationPolicy.Evaluate
            Const IdentitiesKey As String = "Identities"

            ' Check if the properties of the context has the identities list
            If ((evaluationContext.Properties.Count = 0) _
                OrElse (evaluationContext.Properties.ContainsKey(IdentitiesKey) = False) _
                OrElse (evaluationContext.Properties(IdentitiesKey) Is Nothing)) Then
                Return False
            End If

            ' Get the identities list
            Dim identities As List(Of IIdentity) = _
                       CType(evaluationContext.Properties(IdentitiesKey), List(Of IIdentity))

            ' Validate that the identities list is valid
            If (identities Is Nothing) Then
                Return False
            End If

            ' Get the current identity
            Dim currentIdentity As IIdentity = _
                    identities.Find(AddressOf IsIdentityMatch)

            'Check if an identity was found
            If (currentIdentity Is Nothing) Then
                Return False
            End If

            ' Create new identity
            Dim newIdentity As PasswordIdentity = New PasswordIdentity( _
                    UserName, Password, currentIdentity.IsAuthenticated, _
                    currentIdentity.AuthenticationType)

            Const PrimaryIdentityKey As String = "PrimaryIdentity"

            ' Update the list and the context with the new identity
            identities.Remove(currentIdentity)
            identities.Add(newIdentity)
            evaluationContext.Properties(PrimaryIdentityKey) = newIdentity

            ' Create a new principal for this identity
            Dim newPrincipal As PasswordPrincipal = New PasswordPrincipal(newIdentity, Nothing)

            Const PrincipalKey As String = "Principal"

            ' Store the new principal in the context
            evaluationContext.Properties(PrincipalKey) = newPrincipal

            ' This policy has successfully been evaluated and doesn't need to be called again
            Return True

        End Function

    End Class

End Namespace
