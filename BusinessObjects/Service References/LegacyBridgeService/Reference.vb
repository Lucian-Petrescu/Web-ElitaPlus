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

Imports System
Imports System.Runtime.Serialization

Namespace LegacyBridgeService
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="HasBenefitActionEnum", [Namespace]:="http://schemas.datacontract.org/2004/07/Assurant.Elita.ClaimService.Contracts.Cla"& _ 
        "imService")>  _
    Public Enum HasBenefitActionEnum As Integer
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        None = 0
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        Yes = 1
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        No = 2
    End Enum
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="ValidationFault", [Namespace]:="http://assurant.com/Elita/ServiceIntegration/Faults"),  _
     System.SerializableAttribute()>  _
    Partial Public Class ValidationFault
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        Private ItemsField() As LegacyBridgeService.ValidationFaultItem
        
        <Global.System.ComponentModel.BrowsableAttribute(false)>  _
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)>  _
        Public Property Items() As LegacyBridgeService.ValidationFaultItem()
            Get
                Return Me.ItemsField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ItemsField, value) <> true) Then
                    Me.ItemsField = value
                    Me.RaisePropertyChanged("Items")
                End If
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="ValidationFaultItem", [Namespace]:="http://assurant.com/Elita/ServiceIntegration/Faults"),  _
     System.SerializableAttribute()>  _
    Partial Public Class ValidationFaultItem
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private MemberNameField As String
        
        Private MessageField As String
        
        <Global.System.ComponentModel.BrowsableAttribute(false)>  _
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property MemberName() As String
            Get
                Return Me.MemberNameField
            End Get
            Set
                If (Object.ReferenceEquals(Me.MemberNameField, value) <> true) Then
                    Me.MemberNameField = value
                    Me.RaisePropertyChanged("MemberName")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute(IsRequired:=true)>  _
        Public Property Message() As String
            Get
                Return Me.MessageField
            End Get
            Set
                If (Object.ReferenceEquals(Me.MessageField, value) <> true) Then
                    Me.MessageField = value
                    Me.RaisePropertyChanged("Message")
                End If
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="LegacyBridgeResponse", [Namespace]:="http://schemas.datacontract.org/2004/07/Assurant.Elita.ClaimService.Contracts.Cla"& _ 
        "imService"),  _
     System.SerializableAttribute()>  _
    Partial Public Class LegacyBridgeResponse
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private DenialCodeField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private StatusDecisionField As LegacyBridgeService.LegacyBridgeStatusDecisionEnum
        
        <Global.System.ComponentModel.BrowsableAttribute(false)>  _
        Public Property ExtensionData() As System.Runtime.Serialization.ExtensionDataObject Implements System.Runtime.Serialization.IExtensibleDataObject.ExtensionData
            Get
                Return Me.extensionDataField
            End Get
            Set
                Me.extensionDataField = value
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property DenialCode() As String
            Get
                Return Me.DenialCodeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.DenialCodeField, value) <> true) Then
                    Me.DenialCodeField = value
                    Me.RaisePropertyChanged("DenialCode")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property StatusDecision() As LegacyBridgeService.LegacyBridgeStatusDecisionEnum
            Get
                Return Me.StatusDecisionField
            End Get
            Set
                If (Me.StatusDecisionField.Equals(value) <> true) Then
                    Me.StatusDecisionField = value
                    Me.RaisePropertyChanged("StatusDecision")
                End If
            End Set
        End Property
        
        Public Event PropertyChanged As System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
        
        Protected Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim propertyChanged As System.ComponentModel.PropertyChangedEventHandler = Me.PropertyChangedEvent
            If (Not (propertyChanged) Is Nothing) Then
                propertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="LegacyBridgeStatusDecisionEnum", [Namespace]:="http://schemas.datacontract.org/2004/07/Assurant.Elita.ClaimService.Contracts.Cla"& _ 
        "imService")>  _
    Public Enum LegacyBridgeStatusDecisionEnum As Integer
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        Deny = 0
        
        <System.Runtime.Serialization.EnumMemberAttribute()>  _
        Approve = 1
    End Enum
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://elita.assurant.com/Elita/LegacyBridgeService", ConfigurationName:="LegacyBridgeService.ILegacyBridgeService")>  _
    Public Interface ILegacyBridgeService
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://elita.assurant.com/Elita/LegacyBridgeService/ILegacyBridgeService/ExecuteC"& _ 
            "laimRecordingRules", ReplyAction:="http://elita.assurant.com/Elita/LegacyBridgeService/ILegacyBridgeService/ExecuteC"& _ 
            "laimRecordingRulesResponse"),  _
         System.ServiceModel.FaultContractAttribute(GetType(LegacyBridgeService.ValidationFault), Action:="http://elita.assurant.com/Elita/LegacyBridgeService/ILegacyBridgeService/ExecuteC"& _ 
            "laimRecordingRulesValidationFaultFault", Name:="ValidationFault", [Namespace]:="http://assurant.com/Elita/ServiceIntegration/Faults")>  _
        Function ExecuteClaimRecordingRules(ByVal claimId As String, ByVal benefitCheckFlag As Boolean, ByVal hasBenefitAction As LegacyBridgeService.HasBenefitActionEnum) As Boolean
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://elita.assurant.com/Elita/LegacyBridgeService/ILegacyBridgeService/ExecuteC"& _ 
            "laimRecordingRules", ReplyAction:="http://elita.assurant.com/Elita/LegacyBridgeService/ILegacyBridgeService/ExecuteC"& _ 
            "laimRecordingRulesResponse")>  _
        Function ExecuteClaimRecordingRulesAsync(ByVal claimId As String, ByVal benefitCheckFlag As Boolean, ByVal hasBenefitAction As LegacyBridgeService.HasBenefitActionEnum) As System.Threading.Tasks.Task(Of Boolean)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://elita.assurant.com/Elita/LegacyBridgeService/ILegacyBridgeService/BenefitC"& _ 
            "laimPreCheck", ReplyAction:="http://elita.assurant.com/Elita/LegacyBridgeService/ILegacyBridgeService/BenefitC"& _ 
            "laimPreCheckResponse"),  _
         System.ServiceModel.FaultContractAttribute(GetType(LegacyBridgeService.ValidationFault), Action:="http://elita.assurant.com/Elita/LegacyBridgeService/ILegacyBridgeService/BenefitC"& _ 
            "laimPreCheckValidationFaultFault", Name:="ValidationFault", [Namespace]:="http://assurant.com/Elita/ServiceIntegration/Faults")>  _
        Function BenefitClaimPreCheck(ByVal caseId As String) As LegacyBridgeService.LegacyBridgeResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://elita.assurant.com/Elita/LegacyBridgeService/ILegacyBridgeService/BenefitC"& _ 
            "laimPreCheck", ReplyAction:="http://elita.assurant.com/Elita/LegacyBridgeService/ILegacyBridgeService/BenefitC"& _ 
            "laimPreCheckResponse")>  _
        Function BenefitClaimPreCheckAsync(ByVal caseId As String) As System.Threading.Tasks.Task(Of LegacyBridgeService.LegacyBridgeResponse)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface ILegacyBridgeServiceChannel
        Inherits LegacyBridgeService.ILegacyBridgeService, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class LegacyBridgeServiceClient
        Inherits System.ServiceModel.ClientBase(Of LegacyBridgeService.ILegacyBridgeService)
        Implements LegacyBridgeService.ILegacyBridgeService
        
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
        
        Public Function ExecuteClaimRecordingRules(ByVal claimId As String, ByVal benefitCheckFlag As Boolean, ByVal hasBenefitAction As LegacyBridgeService.HasBenefitActionEnum) As Boolean Implements LegacyBridgeService.ILegacyBridgeService.ExecuteClaimRecordingRules
            Return MyBase.Channel.ExecuteClaimRecordingRules(claimId, benefitCheckFlag, hasBenefitAction)
        End Function
        
        Public Function ExecuteClaimRecordingRulesAsync(ByVal claimId As String, ByVal benefitCheckFlag As Boolean, ByVal hasBenefitAction As LegacyBridgeService.HasBenefitActionEnum) As System.Threading.Tasks.Task(Of Boolean) Implements LegacyBridgeService.ILegacyBridgeService.ExecuteClaimRecordingRulesAsync
            Return MyBase.Channel.ExecuteClaimRecordingRulesAsync(claimId, benefitCheckFlag, hasBenefitAction)
        End Function
        
        Public Function BenefitClaimPreCheck(ByVal caseId As String) As LegacyBridgeService.LegacyBridgeResponse Implements LegacyBridgeService.ILegacyBridgeService.BenefitClaimPreCheck
            Return MyBase.Channel.BenefitClaimPreCheck(caseId)
        End Function
        
        Public Function BenefitClaimPreCheckAsync(ByVal caseId As String) As System.Threading.Tasks.Task(Of LegacyBridgeService.LegacyBridgeResponse) Implements LegacyBridgeService.ILegacyBridgeService.BenefitClaimPreCheckAsync
            Return MyBase.Channel.BenefitClaimPreCheckAsync(caseId)
        End Function
    End Class
End Namespace
