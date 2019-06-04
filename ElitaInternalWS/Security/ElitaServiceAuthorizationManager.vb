Imports System.ServiceModel.Description
Imports System.IdentityModel.Claims
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.IdentityModel.Policy
Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Linq
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Audit
Imports System.ServiceModel.Web
Imports Assurant.Elita.Configuration
Imports System.Security.Cryptography.X509Certificates
Imports System.Web

Namespace Security
    Public Class ElitaServiceAuthorizationManager
        Inherits ServiceAuthorizationManager


        Private Function GetOperationDescription(ByVal operationContext As OperationContext) As OperationDescription
            Dim od As OperationDescription = Nothing
            Dim bindingName As String = operationContext.EndpointDispatcher.ChannelDispatcher.BindingName
            Dim methodName As String
            If bindingName.Contains("WebHttpBinding") Then
                'REST request
                methodName = DirectCast(operationContext.IncomingMessageProperties("HttpOperationName"), String)
            Else
                'SOAP request
                Dim action As String = operationContext.IncomingMessageHeaders.Action
                methodName = operationContext.EndpointDispatcher.DispatchRuntime.Operations.FirstOrDefault(Function(o) o.Action = action).Name
            End If

            Dim epa As EndpointAddress = operationContext.EndpointDispatcher.EndpointAddress
            Dim hostDesc As ServiceDescription = operationContext.Host.Description
            Dim ep As ServiceEndpoint = hostDesc.Endpoints.Find(epa.Uri)

            If ep IsNot Nothing Then
                od = ep.Contract.Operations.Find(methodName)
            End If

            Return od
        End Function

        Private Function GetPermissionCode(ByVal operationContext As OperationContext) As String
            Dim od As OperationDescription = GetOperationDescription(operationContext)
            If (od Is Nothing) Then Return Nothing

            Dim ct As Type = od.DeclaringContract.ContractType
            Dim permissionAttributes As Object() = ct.GetMethod(od.Name).GetCustomAttributes(GetType(ElitaPermissionAttribute), False)
            If ((permissionAttributes Is Nothing) OrElse (permissionAttributes.Count() = 0)) Then
                Throw New InvalidOperationException("ElitaPermission not defined on Operation Contract")
            End If

            Dim pa As ElitaPermissionAttribute = DirectCast(permissionAttributes.First(), ElitaPermissionAttribute)

            Return pa.PermissionCode
        End Function

        Private Function GetServiceEndPoint(ByVal operationContext As OperationContext) As ServiceEndpoint

            Dim epa As EndpointAddress = operationContext.EndpointDispatcher.EndpointAddress
            Dim hostDesc As ServiceDescription = operationContext.Host.Description
            Dim sep As ServiceEndpoint = hostDesc.Endpoints.Find(epa.Uri)
            Return sep

        End Function

        Private Function GetCertificateThumprint(ByVal serviceEndPoint As ServiceEndpoint, ByVal iwrc As IncomingWebRequestContext) As X509Certificate2

            If (serviceEndPoint.EndpointBehaviors.Contains(GetType(RequiresX509Attribute))) Then

                If (Not iwrc.Headers.AllKeys.Contains("X-Client-Cert")) Then
                    Throw New UnauthorizedAccessException("0x‭2712")
                End If

                '' Extract Certificate Information
                Dim certificateHeader As String = iwrc.Headers("X-Client-Cert")

                Try
                    Dim buffer As Byte() = Convert.FromBase64String(certificateHeader)
                    Dim clientCertificate As X509Certificate2 = New X509Certificate2(buffer)
                    Return clientCertificate

                Catch ex As Exception
                    ''Log Exception
                    Throw New UnauthorizedAccessException("0x‭2713")
                End Try
                Throw New UnauthorizedAccessException("0x‭2713")
            Else
                Return Nothing
            End If

        End Function

        Private Function ValidateCertificateExpirationDate(x509Certificate As X509Certificate2) As Boolean
            ''validate the certifiate 
            If DateTime.Now < Convert.ToDateTime(x509Certificate.GetEffectiveDateString()) Or Convert.ToDateTime(x509Certificate.GetExpirationDateString()) < DateTime.Now Then
                Return False
            Else
                Return True
            End If

        End Function

        Private Function GetActionName(operationContext As OperationContext) As String

            Dim ocAction As String = operationContext.IncomingMessageHeaders.Action
            Return ocAction.Substring(ocAction.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1)

        End Function


        Protected Overrides Function CheckAccessCore(ByVal operationContext As System.ServiceModel.OperationContext) As Boolean
            Try
                Dim warning As Boolean = False
                Dim X509Certificate As X509Certificate2 = Nothing
                Dim x509CertificateData As String = String.Empty
                Dim actionName As String = String.Empty

                If (operationContext Is Nothing) Then
                    Throw New ArgumentNullException("operationContext")
                End If

                '''extract the action name from the current operation context
                Dim ocAction As String = GetActionName(operationContext)

                Dim tokenPosition As Integer = operationContext.RequestContext.RequestMessage.Headers.FindHeader(ElitaHeader.LocalName, ElitaHeader.NamespaceName)
                If tokenPosition >= 0 Then '' Header Found

                    '' Read Header
                    Dim header As ElitaHeader = operationContext.RequestContext.RequestMessage.Headers.GetHeader(Of ElitaHeader)(ElitaHeader.LocalName, ElitaHeader.NamespaceName)

                    '' Authenticate User Name and Password
                    If (Not (LdapAuthenticationManager.Current.ValidateLdapUser(String.Format("{0}\{1}", header.GetGroup(), header.GetNetworkId()), header.GetPassword()))) Then
                        SecurityAudit.RecordSecurityAudit(New SecurityAuditInfo With {
                        .ActionName = actionName,
                        .AuditSecurityTypeCode = AuditSecurityTypeCode.FailedAccess,
                        .ClientIPAddress = String.Empty,
                        .IPAddressChain = String.Empty,
                        .RequestUrl = operationContext.Channel.LocalAddress.Uri.AbsoluteUri,
                        .UserName = header.GetNetworkId(),
                        .X509Certificate = String.Empty
                   })
                        Throw New UnauthorizedAccessException("Failed to Verify Security")
                    End If
                End If

                '' Get Authentication Context, This will trigger Policy to be executed which will populate Principal
                Dim authContext As AuthorizationContext = operationContext.ServiceSecurityContext.AuthorizationContext

                '' Check if Web Context is Applicable
                Dim woctx As WebOperationContext = WebOperationContext.Current
                If (woctx Is DBNull.Value) Then
                    Throw New UnauthorizedAccessException("0x‭2711‬")
                End If

                Dim iwrc As IncomingWebRequestContext = woctx.IncomingRequest


                ''Extract Client IP information
                Dim ipAddressChain As String = String.Empty
                Dim clientIp As String = String.Empty
                Dim ipAddresses As String()

                ''serivce end point
                Dim sep As ServiceEndpoint = GetServiceEndPoint(operationContext)

                '' Get Principal Created by Authorization Policy
                Dim principalObject As Object = authContext.Properties(ElitaAuthorizationPolicyBase.ClaimsPrincipalKey)

                If (principalObject Is Nothing) Then Return False
                If (Not principalObject.GetType().Equals(GetType(ElitaPlusPrincipal))) Then Return False

                Dim elitaPrincipal As ElitaPlusPrincipal = DirectCast(principalObject, ElitaPlusPrincipal)


                If (sep.EndpointBehaviors.Contains(GetType(RequiresX509Attribute))) Then

                    If (Not iwrc.Headers.AllKeys.Contains("HTTP_X_FORWARDED_FOR")) Then
                        Throw New UnauthorizedAccessException("0x‭2714")
                    End If
                    ipAddressChain = iwrc.Headers("HTTP_X_FORWARDED_FOR")
                    ipAddresses = ipAddressChain.Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)
                    clientIp = ipAddresses.First()

                    X509Certificate = GetCertificateThumprint(sep, iwrc)
                    If (Not X509Certificate Is Nothing AndAlso X509Certificate.RawData.Length > 0) Then
                        x509CertificateData = Convert.ToBase64String(X509Certificate.RawData)
                    End If

                    If (Not elitaPrincipal.HasX509Thumbprint(X509Certificate.Thumbprint)) Then
                        '' TODO: Match Thumprint
                        '' IF Not match then Error + Audit (Certificate Compromized)
                        SecurityAudit.RecordSecurityAudit(New SecurityAuditInfo With {
                        .ActionName = actionName,
                        .AuditSecurityTypeCode = AuditSecurityTypeCode.CompromisedCertificate,
                        .ClientIPAddress = clientIp,
                        .IPAddressChain = ipAddressChain,
                        .RequestUrl = operationContext.Channel.LocalAddress.Uri.AbsoluteUri,
                        .UserName = elitaPrincipal.Identity.Name,
                        .X509Certificate = x509CertificateData
                   })
                        Throw New UnauthorizedAccessException("0x‭2715")

                    End If

                    If (Not elitaPrincipal.IsX509CertificateValid(X509Certificate.Thumbprint)) Then

                        SecurityAudit.RecordSecurityAudit(New SecurityAuditInfo With {
                        .ActionName = actionName,
                        .AuditSecurityTypeCode = AuditSecurityTypeCode.CertificateExpired,
                        .ClientIPAddress = clientIp,
                        .IPAddressChain = ipAddressChain,
                        .RequestUrl = operationContext.Channel.LocalAddress.Uri.AbsoluteUri,
                        .UserName = elitaPrincipal.Identity.Name,
                        .X509Certificate = x509CertificateData
                   })

                        Throw New UnauthorizedAccessException("0x‭2717")
                    End If

                    '' Check Client IP
                    '' If Not match then Audit (IP Mismatch)
                    If (Not elitaPrincipal.HasClientIP(clientIp)) Then

                        SecurityAudit.RecordSecurityAudit(New SecurityAuditInfo With {
                        .ActionName = actionName,
                        .AuditSecurityTypeCode = AuditSecurityTypeCode.IPAddressMismatch,
                        .ClientIPAddress = clientIp,
                        .IPAddressChain = ipAddressChain,
                        .RequestUrl = operationContext.Channel.LocalAddress.Uri.AbsoluteUri,
                        .UserName = elitaPrincipal.Identity.Name,
                        .X509Certificate = x509CertificateData
                        })
                        warning = True
                    End If

                    ''If client IP Is expired
                    If (Not elitaPrincipal.IsClientIPValid(clientIp)) Then
                        SecurityAudit.RecordSecurityAudit(New SecurityAuditInfo With {
                        .ActionName = actionName,
                        .AuditSecurityTypeCode = AuditSecurityTypeCode.IPAddressExpired,
                        .ClientIPAddress = clientIp,
                        .IPAddressChain = ipAddressChain,
                        .RequestUrl = operationContext.Channel.LocalAddress.Uri.AbsoluteUri,
                        .UserName = elitaPrincipal.Identity.Name,
                        .X509Certificate = x509CertificateData
                        })
                        warning = True
                    End If

                Else
                    '' Regular Endpoint
                    '' Check if IP Is external
                    If (Not String.IsNullOrEmpty(ipAddressChain) AndAlso ipAddresses.Count() > 1) Then

                        '' Audit Insecure Communication
                        SecurityAudit.RecordSecurityAudit(New SecurityAuditInfo With {
                        .ActionName = actionName,
                        .AuditSecurityTypeCode = AuditSecurityTypeCode.InsecureCommunication,
                        .ClientIPAddress = clientIp,
                        .IPAddressChain = ipAddressChain,
                        .RequestUrl = operationContext.Channel.LocalAddress.Uri.AbsoluteUri,
                        .UserName = elitaPrincipal.Identity.Name,
                        .X509Certificate = x509CertificateData
                        })

                        warning = True

                    End If

                    '' Extract Operation Contract find What Permission is Demanded by Action
                    Dim permissionCode As String = GetPermissionCode(operationContext)

                    '' Check if User have Permission
                    If (Not elitaPrincipal.HasPermission(permissionCode)) Then
                        '' Add Audit Action Authorization Failed
                        SecurityAudit.RecordSecurityAudit(New SecurityAuditInfo With {
                        .ActionName = actionName,
                        .AuditSecurityTypeCode = AuditSecurityTypeCode.ActionAuthorizationFailed,
                        .ClientIPAddress = clientIp,
                        .IPAddressChain = ipAddressChain,
                        .RequestUrl = operationContext.Channel.LocalAddress.Uri.AbsoluteUri,
                        .UserName = elitaPrincipal.Identity.Name,
                        .X509Certificate = x509CertificateData
                        })

                        Throw New UnauthorizedAccessException("0x‭2716")

                    End If

                End If

                ''Sucess Audit
                If (Not warning) Then
                    SecurityAudit.RecordSecurityAudit(New SecurityAuditInfo With {
                        .ActionName = actionName,
                        .AuditSecurityTypeCode = AuditSecurityTypeCode.Success,
                        .ClientIPAddress = clientIp,
                        .IPAddressChain = ipAddressChain,
                        .RequestUrl = operationContext.Channel.LocalAddress.Uri.AbsoluteUri,
                        .UserName = elitaPrincipal.Identity.Name,
                        .X509Certificate = x509CertificateData
                   })

                End If

                Return True

            Catch ex As UnauthorizedAccessException
                Throw New UnauthorizedAccessException(ex.Message)
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Class
End Namespace

