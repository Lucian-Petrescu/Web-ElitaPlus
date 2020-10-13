Imports System
Imports System.Xml
Imports System.Web.Services.Protocols
Imports Microsoft.Web.Services3
Imports Microsoft.Web.Services3.Security
Imports Microsoft.Web.Services3.Security.Tokens
Imports RMEncryption
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports System.Reflection
Imports System.Text.RegularExpressions

Public Class ElitaWebService
    Inherits System.Web.Services.WebService

#Region "Constants"

    Public Const PRINCIPAL_SESSION_KEY As String = "PRINCIPAL_SESSION_KEY"

#End Region

#Region "Variables"

#End Region


#Region " Web Methods"

    <WebMethod()> _
   Public Function HelloWorld() As String
        '  Return "Hello World"
        Return ElitaService.Hello("Hello World")
    End Function


    <WebMethod()> _
    Public Function Login() As String
        Dim complexUsername As String
        Dim appPassword As String
        Dim token As String
        Dim usernameToken As UsernameToken

        AppConfig.DebugMessage.Trace("ELITAINTERNALWS", "LOGIN", ToString & "_" & Now.ToString)

        '    Reject any requests which are not valid SOAP requests
        If RequestSoapContext.Current Is Nothing Then Throw New ApplicationException("Only SOAP requests are permitted.")
        Try
            usernameToken = GetSigningToken(RequestSoapContext.Current)
            complexUsername = usernameToken.Username
            appPassword = usernameToken.Password
            token = ElitaService.VerifyLogin(False, complexUsername, appPassword)
        Catch ex As Exception
            Throw New SecurityFault(SecurityFault.FailedAuthenticationMessage, SecurityFault.FailedAuthenticationCode)
        End Try

        Return token

    End Function

    <WebMethod()> _
    Public Function LoginBody(networkID As String, password As String, group As String) As String
        Dim token As String

        AppConfig.DebugMessage.Trace("ELITAINTERNALWS", "LOGINBODY", ToString & "_" & Now.ToString)

        '    Reject any requests which are not valid SOAP requests
        If RequestSoapContext.Current Is Nothing Then Throw New ApplicationException("Only SOAP requests are permitted.")
        Try
            token = ElitaService.VerifyLogin(False, networkID, networkID, password, group)
        Catch ex As Exception
            Throw New SecurityFault(SecurityFault.FailedAuthenticationMessage, SecurityFault.FailedAuthenticationCode)
        End Try

        Return token
    End Function


    <WebMethod()> _
    Public Overridable Function ProcessRequest(token As String, _
                                               functionToProcess As String, _
                                               xmlStringDataIn As String) As String

        Return ElitaService.ProcessRequest(False, token, functionToProcess, xmlStringDataIn, _
                                            [GetType].Name().ToUpper)
    End Function


#End Region

   

    

#Region " Authentication"

   
   
    Private Function CheckSignature(context As SoapContext, signature As MessageSignature) As Boolean

        '
        ' Now verify which parts of the message were actually signed.
        '
        Dim actualOptions As SignatureOptions = signature.SignatureOptions
        Dim expectedOptions As SignatureOptions = SignatureOptions.IncludeSoapBody

        If context.Security IsNot Nothing AndAlso context.Security.Timestamp IsNot Nothing Then
            expectedOptions = expectedOptions Or SignatureOptions.IncludeTimestamp
        End If

        '
        ' The <Action> and <To> are required addressing elements.
        '
        expectedOptions = expectedOptions Or SignatureOptions.IncludeAction
        expectedOptions = expectedOptions Or SignatureOptions.IncludeTo

        If context.Addressing.FaultTo IsNot Nothing AndAlso context.Addressing.FaultTo.TargetElement IsNot Nothing Then
            expectedOptions = expectedOptions Or SignatureOptions.IncludeFaultTo
        End If

        If context.Addressing.From IsNot Nothing AndAlso context.Addressing.From.TargetElement IsNot Nothing Then
            expectedOptions = expectedOptions Or SignatureOptions.IncludeFrom
        End If

        If context.Addressing.MessageID IsNot Nothing AndAlso context.Addressing.MessageID.TargetElement IsNot Nothing Then
            expectedOptions = expectedOptions Or SignatureOptions.IncludeMessageId
        End If

        If context.Addressing.RelatesTo IsNot Nothing AndAlso context.Addressing.RelatesTo.TargetElement IsNot Nothing Then
            expectedOptions = expectedOptions Or SignatureOptions.IncludeRelatesTo
        End If

        If context.Addressing.ReplyTo IsNot Nothing AndAlso context.Addressing.ReplyTo.TargetElement IsNot Nothing Then
            expectedOptions = expectedOptions Or SignatureOptions.IncludeReplyTo
        End If
        '
        ' Check if the all the expected options are the present.
        '
        Dim options As SignatureOptions = expectedOptions And actualOptions
        Return (options = expectedOptions)

    End Function

    Private Function GetSigningToken(context As SoapContext) As SecurityToken

        Dim element As ISecurityElement
        For Each element In context.Security.Elements

            If (TypeOf (element) Is MessageSignature) Then

                ' The context contains a Signature element. 
                Dim sign As MessageSignature = element

                ' The SOAP body is signed
                If (CheckSignature(context, sign)) Then Return sign.SigningToken

            End If

        Next

        Return Nothing

    End Function ' GetSigningToken

#End Region


End Class
