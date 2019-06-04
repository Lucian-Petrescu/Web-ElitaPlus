Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Description
Imports System.ServiceModel.Dispatcher

Namespace Security
    Public Class RequiresX509Attribute
        Inherits Attribute
        Implements IDispatchMessageInspector, IEndpointBehavior

#Region "IDispatchMessageInspector"
        Public Sub AddBindingParameters(endpoint As ServiceEndpoint, bindingParameters As BindingParameterCollection) Implements IEndpointBehavior.AddBindingParameters

        End Sub

        Public Sub ApplyClientBehavior(endpoint As ServiceEndpoint, clientRuntime As ClientRuntime) Implements IEndpointBehavior.ApplyClientBehavior
            Throw New NotSupportedException()
        End Sub

#End Region

#Region "IEndpointBehavior"
        Public Sub ApplyDispatchBehavior(endpoint As ServiceEndpoint, endpointDispatcher As EndpointDispatcher) Implements IEndpointBehavior.ApplyDispatchBehavior
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(New RequiresX509Attribute())
        End Sub

        Public Sub BeforeSendReply(ByRef reply As Message, correlationState As Object) Implements IDispatchMessageInspector.BeforeSendReply

        End Sub

        Public Sub Validate(endpoint As ServiceEndpoint) Implements IEndpointBehavior.Validate

        End Sub

        Public Function AfterReceiveRequest(ByRef request As Message, channel As IClientChannel, instanceContext As InstanceContext) As Object Implements IDispatchMessageInspector.AfterReceiveRequest
            Return Nothing
        End Function
#End Region
    End Class

End Namespace