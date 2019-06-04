﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace TestBatchService
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://elita.assurant.com", ConfigurationName:="TestBatchService.ITestService")>  _
    Public Interface ITestService
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://elita.assurant.com/ITestService/HealthCheck", ReplyAction:="http://elita.assurant.com/ITestService/HealthCheckResponse")>  _
        Function HealthCheck(ByVal networkID As String, ByVal password As String, ByVal LDAPGroup As String) As String
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface ITestServiceChannel
        Inherits TestBatchService.ITestService, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class TestServiceClient
        Inherits System.ServiceModel.ClientBase(Of TestBatchService.ITestService)
        Implements TestBatchService.ITestService
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Function HealthCheck(ByVal networkID As String, ByVal password As String, ByVal LDAPGroup As String) As String Implements TestBatchService.ITestService.HealthCheck
            Return MyBase.Channel.HealthCheck(networkID, password, LDAPGroup)
        End Function
    End Class
End Namespace
