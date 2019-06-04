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

Namespace VSCBrazilCreditCardAuth
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0"),  _
     System.Runtime.Serialization.DataContractAttribute(Name:="RetornoContratacao", [Namespace]:="http://schemas.datacontract.org/2004/07/ServicoCobrancaAssurant"),  _
     System.SerializableAttribute()>  _
    Partial Public Class RetornoContratacao
        Inherits Object
        Implements System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
        
        <System.NonSerializedAttribute()>  _
        Private extensionDataField As System.Runtime.Serialization.ExtensionDataObject
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private CodigoRetornoField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private MensagemRetornoField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private NumeroContratoCobrancaField As String
        
        <System.Runtime.Serialization.OptionalFieldAttribute()>  _
        Private SucessoField As Boolean
        
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
        Public Property CodigoRetorno() As String
            Get
                Return Me.CodigoRetornoField
            End Get
            Set
                If (Object.ReferenceEquals(Me.CodigoRetornoField, value) <> true) Then
                    Me.CodigoRetornoField = value
                    Me.RaisePropertyChanged("CodigoRetorno")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property MensagemRetorno() As String
            Get
                Return Me.MensagemRetornoField
            End Get
            Set
                If (Object.ReferenceEquals(Me.MensagemRetornoField, value) <> true) Then
                    Me.MensagemRetornoField = value
                    Me.RaisePropertyChanged("MensagemRetorno")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property NumeroContratoCobranca() As String
            Get
                Return Me.NumeroContratoCobrancaField
            End Get
            Set
                If (Object.ReferenceEquals(Me.NumeroContratoCobrancaField, value) <> true) Then
                    Me.NumeroContratoCobrancaField = value
                    Me.RaisePropertyChanged("NumeroContratoCobranca")
                End If
            End Set
        End Property
        
        <System.Runtime.Serialization.DataMemberAttribute()>  _
        Public Property Sucesso() As Boolean
            Get
                Return Me.SucessoField
            End Get
            Set
                If (Me.SucessoField.Equals(value) <> true) Then
                    Me.SucessoField = value
                    Me.RaisePropertyChanged("Sucesso")
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
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="VSCBrazilCreditCardAuth.IServicoCobrancaAssurant")>  _
    Public Interface IServicoCobrancaAssurant
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IServicoCobrancaAssurant/ContratarPorCartaoDeCreditoAVista", ReplyAction:="http://tempuri.org/IServicoCobrancaAssurant/ContratarPorCartaoDeCreditoAVistaResp"& _ 
            "onse")>  _
        Function ContratarPorCartaoDeCreditoAVista(ByVal token As String, ByVal campanha As String, ByVal nomeCliente As String, ByVal numeroCpfCnpj As String, ByVal codCertificado As String, ByVal nomeCartao As String, ByVal valorPremio As Decimal, ByVal numeroCartao As String, ByVal cvvCartao As String, ByVal vencimentoCartao As String, ByVal numeroOperadora As String, ByVal numeroContratoCobranca As String, ByVal acao As String) As VSCBrazilCreditCardAuth.RetornoContratacao
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IServicoCobrancaAssurant/ContratarPorCartaoDeCreditoParcelado", ReplyAction:="http://tempuri.org/IServicoCobrancaAssurant/ContratarPorCartaoDeCreditoParceladoR"& _ 
            "esponse")>  _
        Function ContratarPorCartaoDeCreditoParcelado(ByVal token As String, ByVal campanha As String, ByVal nomeCliente As String, ByVal numeroCpfCnpj As String, ByVal codCertificado As String, ByVal nomeCartao As String, ByVal valorPremio As Decimal, ByVal numeroCartao As String, ByVal cvvCartao As String, ByVal vencimentoCartao As String, ByVal numeroOperadora As String, ByVal numeroDeParcelas As String, ByVal numeroContratoCobranca As String, ByVal acao As String) As VSCBrazilCreditCardAuth.RetornoContratacao
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IServicoCobrancaAssurant/CancelarContrato", ReplyAction:="http://tempuri.org/IServicoCobrancaAssurant/CancelarContratoResponse")>  _
        Function CancelarContrato(ByVal token As String, ByVal numeroContratoCobranca As String, ByVal valor As String) As VSCBrazilCreditCardAuth.RetornoContratacao
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IServicoCobrancaAssurant/AnularContrato", ReplyAction:="http://tempuri.org/IServicoCobrancaAssurant/AnularContratoResponse")>  _
        Function AnularContrato(ByVal token As String, ByVal numeroContratoCobranca As String) As VSCBrazilCreditCardAuth.RetornoContratacao
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface IServicoCobrancaAssurantChannel
        Inherits VSCBrazilCreditCardAuth.IServicoCobrancaAssurant, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class ServicoCobrancaAssurantClient
        Inherits System.ServiceModel.ClientBase(Of VSCBrazilCreditCardAuth.IServicoCobrancaAssurant)
        Implements VSCBrazilCreditCardAuth.IServicoCobrancaAssurant
        
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
        
        Public Function ContratarPorCartaoDeCreditoAVista(ByVal token As String, ByVal campanha As String, ByVal nomeCliente As String, ByVal numeroCpfCnpj As String, ByVal codCertificado As String, ByVal nomeCartao As String, ByVal valorPremio As Decimal, ByVal numeroCartao As String, ByVal cvvCartao As String, ByVal vencimentoCartao As String, ByVal numeroOperadora As String, ByVal numeroContratoCobranca As String, ByVal acao As String) As VSCBrazilCreditCardAuth.RetornoContratacao Implements VSCBrazilCreditCardAuth.IServicoCobrancaAssurant.ContratarPorCartaoDeCreditoAVista
            Return MyBase.Channel.ContratarPorCartaoDeCreditoAVista(token, campanha, nomeCliente, numeroCpfCnpj, codCertificado, nomeCartao, valorPremio, numeroCartao, cvvCartao, vencimentoCartao, numeroOperadora, numeroContratoCobranca, acao)
        End Function
        
        Public Function ContratarPorCartaoDeCreditoParcelado(ByVal token As String, ByVal campanha As String, ByVal nomeCliente As String, ByVal numeroCpfCnpj As String, ByVal codCertificado As String, ByVal nomeCartao As String, ByVal valorPremio As Decimal, ByVal numeroCartao As String, ByVal cvvCartao As String, ByVal vencimentoCartao As String, ByVal numeroOperadora As String, ByVal numeroDeParcelas As String, ByVal numeroContratoCobranca As String, ByVal acao As String) As VSCBrazilCreditCardAuth.RetornoContratacao Implements VSCBrazilCreditCardAuth.IServicoCobrancaAssurant.ContratarPorCartaoDeCreditoParcelado
            Return MyBase.Channel.ContratarPorCartaoDeCreditoParcelado(token, campanha, nomeCliente, numeroCpfCnpj, codCertificado, nomeCartao, valorPremio, numeroCartao, cvvCartao, vencimentoCartao, numeroOperadora, numeroDeParcelas, numeroContratoCobranca, acao)
        End Function
        
        Public Function CancelarContrato(ByVal token As String, ByVal numeroContratoCobranca As String, ByVal valor As String) As VSCBrazilCreditCardAuth.RetornoContratacao Implements VSCBrazilCreditCardAuth.IServicoCobrancaAssurant.CancelarContrato
            Return MyBase.Channel.CancelarContrato(token, numeroContratoCobranca, valor)
        End Function
        
        Public Function AnularContrato(ByVal token As String, ByVal numeroContratoCobranca As String) As VSCBrazilCreditCardAuth.RetornoContratacao Implements VSCBrazilCreditCardAuth.IServicoCobrancaAssurant.AnularContrato
            Return MyBase.Channel.AnularContrato(token, numeroContratoCobranca)
        End Function
    End Class
End Namespace
