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

Namespace AssurantMexicoService
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="CertificateInfoAppleCare", [Namespace]:="http://schemas.datacontract.org/2004/07/Assurant.SalesIntegration.ObjectModel"),  _
     System.SerializableAttribute()>  _
    Partial Public Class CertificateInfoAppleCare
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private BrandField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private CertificateNumberField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private DealerCodeField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private IMEIField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private IdMotiveField As Integer
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private ModelField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private PasswordField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private ReplacementIMEIField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private ReplacementSerialNumberField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private SKUField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private SerialNumberField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private TransactionTypeField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private UserIdField As String
        
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
        Public Property Brand() As String
            Get
                Return Me.BrandField
            End Get
            Set
                If (Object.ReferenceEquals(Me.BrandField, value) <> true) Then
                    Me.BrandField = value
                    Me.RaisePropertyChanged("Brand")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property CertificateNumber() As String
            Get
                Return Me.CertificateNumberField
            End Get
            Set
                If (Object.ReferenceEquals(Me.CertificateNumberField, value) <> true) Then
                    Me.CertificateNumberField = value
                    Me.RaisePropertyChanged("CertificateNumber")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property DealerCode() As String
            Get
                Return Me.DealerCodeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.DealerCodeField, value) <> true) Then
                    Me.DealerCodeField = value
                    Me.RaisePropertyChanged("DealerCode")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property IMEI() As String
            Get
                Return Me.IMEIField
            End Get
            Set
                If (Object.ReferenceEquals(Me.IMEIField, value) <> true) Then
                    Me.IMEIField = value
                    Me.RaisePropertyChanged("IMEI")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property IdMotive() As Integer
            Get
                Return Me.IdMotiveField
            End Get
            Set
                If (Me.IdMotiveField.Equals(value) <> true) Then
                    Me.IdMotiveField = value
                    Me.RaisePropertyChanged("IdMotive")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property Model() As String
            Get
                Return Me.ModelField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ModelField, value) <> true) Then
                    Me.ModelField = value
                    Me.RaisePropertyChanged("Model")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property Password() As String
            Get
                Return Me.PasswordField
            End Get
            Set
                If (Object.ReferenceEquals(Me.PasswordField, value) <> true) Then
                    Me.PasswordField = value
                    Me.RaisePropertyChanged("Password")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property ReplacementIMEI() As String
            Get
                Return Me.ReplacementIMEIField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ReplacementIMEIField, value) <> true) Then
                    Me.ReplacementIMEIField = value
                    Me.RaisePropertyChanged("ReplacementIMEI")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property ReplacementSerialNumber() As String
            Get
                Return Me.ReplacementSerialNumberField
            End Get
            Set
                If (Object.ReferenceEquals(Me.ReplacementSerialNumberField, value) <> true) Then
                    Me.ReplacementSerialNumberField = value
                    Me.RaisePropertyChanged("ReplacementSerialNumber")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property SKU() As String
            Get
                Return Me.SKUField
            End Get
            Set
                If (Object.ReferenceEquals(Me.SKUField, value) <> true) Then
                    Me.SKUField = value
                    Me.RaisePropertyChanged("SKU")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property SerialNumber() As String
            Get
                Return Me.SerialNumberField
            End Get
            Set
                If (Object.ReferenceEquals(Me.SerialNumberField, value) <> true) Then
                    Me.SerialNumberField = value
                    Me.RaisePropertyChanged("SerialNumber")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property TransactionType() As String
            Get
                Return Me.TransactionTypeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.TransactionTypeField, value) <> true) Then
                    Me.TransactionTypeField = value
                    Me.RaisePropertyChanged("TransactionType")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property UserId() As String
            Get
                Return Me.UserIdField
            End Get
            Set
                If (Object.ReferenceEquals(Me.UserIdField, value) <> true) Then
                    Me.UserIdField = value
                    Me.RaisePropertyChanged("UserId")
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
     System.Runtime.Serialization.DataContractAttribute(Name:="ServiceResultBase", [Namespace]:="http://schemas.datacontract.org/2004/07/Assurant.SalesIntegration.ObjectModel"),  _
     System.SerializableAttribute()>  _
    Partial Public Class ServiceResultBase
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private CodeField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private MessageField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private TransactionIdField As String
        
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
        Public Property Code() As String
            Get
                Return Me.CodeField
            End Get
            Set
                If (Object.ReferenceEquals(Me.CodeField, value) <> true) Then
                    Me.CodeField = value
                    Me.RaisePropertyChanged("Code")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
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
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property TransactionId() As String
            Get
                Return Me.TransactionIdField
            End Get
            Set
                If (Object.ReferenceEquals(Me.TransactionIdField, value) <> true) Then
                    Me.TransactionIdField = value
                    Me.RaisePropertyChanged("TransactionId")
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
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="https://www.assurant.mx/2016", ConfigurationName:="AssurantMexicoService.IAppleCareService")>  _
    Public Interface IAppleCareService
        
        <System.ServiceModel.OperationContractAttribute(Action:="Update", ReplyAction:="UpdateReply")>  _
        Function Update(ByVal updateInfo As AssurantMexicoService.CertificateInfoAppleCare) As AssurantMexicoService.ServiceResultBase
        
        <System.ServiceModel.OperationContractAttribute(Action:="Update", ReplyAction:="UpdateReply")>  _
        Function UpdateAsync(ByVal updateInfo As AssurantMexicoService.CertificateInfoAppleCare) As System.Threading.Tasks.Task(Of AssurantMexicoService.ServiceResultBase)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface IAppleCareServiceChannel
        Inherits AssurantMexicoService.IAppleCareService, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class AppleCareServiceClient
        Inherits System.ServiceModel.ClientBase(Of AssurantMexicoService.IAppleCareService)
        Implements AssurantMexicoService.IAppleCareService
        
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
        
        Public Function Update(ByVal updateInfo As AssurantMexicoService.CertificateInfoAppleCare) As AssurantMexicoService.ServiceResultBase Implements AssurantMexicoService.IAppleCareService.Update
            Return MyBase.Channel.Update(updateInfo)
        End Function
        
        Public Function UpdateAsync(ByVal updateInfo As AssurantMexicoService.CertificateInfoAppleCare) As System.Threading.Tasks.Task(Of AssurantMexicoService.ServiceResultBase) Implements AssurantMexicoService.IAppleCareService.UpdateAsync
            Return MyBase.Channel.UpdateAsync(updateInfo)
        End Function
    End Class
End Namespace
