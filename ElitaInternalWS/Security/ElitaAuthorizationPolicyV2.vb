Imports System.IdentityModel.Policy
Imports System.IdentityModel.Claims
Imports Assurant.ElitaPlus.BusinessObjectsNew

Namespace Security
    Public Class ElitaAuthorizationPolicyV2
        Inherits ElitaAuthorizationPolicyBase
        Implements IAuthorizationPolicy

        Public Overrides Function Evaluate(evaluationContext As EvaluationContext, ByRef state As Object) As Boolean

            Dim principalObj As Object
            Dim principal As ElitaPlusPrincipal

            If (evaluationContext Is Nothing) OrElse (evaluationContext.Properties Is Nothing) Then
                Throw New ArgumentNullException("evaluationContext")
            End If

            Try

                If Not evaluationContext.Properties.TryGetValue(ClaimsPrincipalKey, principalObj) Then
                    Dim userId As String = DirectCast(evaluationContext.ClaimSets.FirstOrDefault().FindClaims(ClaimTypes.Name, Rights.PossessProperty).FirstOrDefault().Resource, String)
                    Dim authorizationUserId As String

                    Dim items As String() = userId.Split(New String() {"\"}, StringSplitOptions.RemoveEmptyEntries)
                    authorizationUserId = items.LastOrDefault()

                    '' Check if Operation Context Contains Token
                    principal = LdapAuthenticationManager.Current.GetPrincipal(items.Last().ToUpperInvariant())
                    evaluationContext.Properties.Add(ClaimsPrincipalKey, principal)
                Else

                    principal = DirectCast(principalObj, ElitaPlusPrincipal)
                End If
            Catch ex As Exception

            End Try

            Return False
            'System.Threading.Thread.CurrentPrincipal = principal
        End Function

    End Class
End Namespace