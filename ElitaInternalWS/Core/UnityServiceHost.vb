Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports Microsoft.Practices.Unity

Namespace Core

    Public Class UnityServiceHost
        Inherits ServiceHost
        Public Sub New(container As IUnityContainer, serviceType As Type, ParamArray baseAddresses As Uri())
            MyBase.New(serviceType, baseAddresses)
            If container Is Nothing Then
                Throw New ArgumentNullException("container")
            End If

            For Each contractDefinition As ContractDescription In ImplementedContracts.Values
                contractDefinition.Behaviors.Add(New UnityInstanceProvider(container))
            Next
        End Sub
    End Class
End Namespace
