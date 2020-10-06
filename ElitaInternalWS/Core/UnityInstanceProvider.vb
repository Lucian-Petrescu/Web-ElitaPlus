
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Description
Imports System.ServiceModel.Dispatcher
Imports Microsoft.Practices.Unity

Namespace Core
    Public Class UnityInstanceProvider
        Implements IInstanceProvider
        Implements IContractBehavior
        Private ReadOnly container As IUnityContainer

        Public Sub New(container As IUnityContainer)
            If container Is Nothing Then
                Throw New ArgumentNullException("container")
            End If

            Me.container = container
        End Sub

#Region "IInstanceProvider Members"

        Public Function GetInstance(instanceContext As InstanceContext, message As Message) As Object Implements IInstanceProvider.GetInstance
            Return GetInstance(instanceContext)
        End Function

        Public Function GetInstance(instanceContext As InstanceContext) As Object Implements IInstanceProvider.GetInstance
            Dim returnObj As Object

            returnObj = container.Resolve(instanceContext.Host.Description.ServiceType)

            Return returnObj
        End Function

        Public Sub ReleaseInstance(instanceContext As InstanceContext, instance As Object) Implements IInstanceProvider.ReleaseInstance

        End Sub

#End Region

#Region "IContractBehavior Members"

        Public Sub AddBindingParameters(contractDescription As ContractDescription, endpoint As ServiceEndpoint, bindingParameters As BindingParameterCollection) Implements IContractBehavior.AddBindingParameters
        End Sub

        Public Sub ApplyClientBehavior(contractDescription As ContractDescription, endpoint As ServiceEndpoint, clientRuntime As ClientRuntime) Implements IContractBehavior.ApplyClientBehavior
        End Sub

        Public Sub ApplyDispatchBehavior(contractDescription As ContractDescription, endpoint As ServiceEndpoint, dispatchRuntime As DispatchRuntime) Implements IContractBehavior.ApplyDispatchBehavior
            dispatchRuntime.InstanceProvider = Me
        End Sub

        Public Sub Validate(contractDescription As ContractDescription, endpoint As ServiceEndpoint) Implements IContractBehavior.Validate
        End Sub

#End Region
    End Class

End Namespace