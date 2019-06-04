Imports System.IdentityModel.Claims
Imports System.IdentityModel.Policy

Public MustInherit Class ElitaAuthorizationPolicyBase
    Implements IAuthorizationPolicy

    Private _id As String

    Public Sub New()
        _id = Guid.NewGuid().ToString()
    End Sub

    Public ReadOnly Property Id As String Implements IAuthorizationPolicy.Id
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property Issuer As ClaimSet Implements IAuthorizationPolicy.Issuer
        Get
            Return WindowsClaimSet.Windows
        End Get
    End Property

    Friend Const ClaimsPrincipalKey As String = "Principal"

    Public MustOverride Function Evaluate(evaluationContext As EvaluationContext, ByRef state As Object) As Boolean Implements IAuthorizationPolicy.Evaluate

End Class
