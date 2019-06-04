Imports System.IdentityModel.Policy
Imports Assurant.ElitaPlus.BusinessObjectsNew

Namespace Security
    Public Class ElitaAuthorizationPolicyV1
        Inherits ElitaAuthorizationPolicyBase
        Implements IAuthorizationPolicy

        Public Overrides Function Evaluate(evaluationContext As EvaluationContext, ByRef state As Object) As Boolean
            Try

                If (evaluationContext Is Nothing) OrElse (evaluationContext.Properties Is Nothing) Then
                    Throw New ArgumentNullException("evaluationContext")
                End If

                Dim principal As ElitaPlusPrincipal
                Dim userName As String = String.Empty
                Dim header As ElitaHeader = System.ServiceModel.OperationContext.Current.RequestContext.RequestMessage.Headers.GetHeader(Of ElitaHeader)(ElitaHeader.LocalName, ElitaHeader.NamespaceName)
                userName = header.GetNetworkId.ToUpperInvariant()

                '' Check if Operation Context Contains Token
                principal = LdapAuthenticationManager.Current.GetPrincipal(userName)
                evaluationContext.Properties.Add(ClaimsPrincipalKey, principal)

            Catch ex As Exception

            End Try

            Return False

        End Function

    End Class
End Namespace
