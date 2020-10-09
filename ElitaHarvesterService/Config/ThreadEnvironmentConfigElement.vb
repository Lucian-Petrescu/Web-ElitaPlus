Imports System.Configuration

Public Class ThreadEnvironmentConfigElement
    Inherits ThreadConfigElement
    Implements IThreadEnvironmentConfig, IThreadConfig

    Private Const PROPERTY_NAME_HUB As String = "Hub"
    Private Const PROPERTY_NAME_MACHINE_DOMAIN As String = "MachineDomain"


    Public Sub New()

    End Sub

    Public Sub New(pName As String, pEnvironment As String, pHub As String, pMachineDomain As String)
        MyBase.New(pName, pEnvironment)
        Hub = pHub
        MachineDomain = pMachineDomain
    End Sub

    <ConfigurationProperty(PROPERTY_NAME_HUB, IsRequired:=True, IsKey:=False)> _
    Public Property Hub As String Implements IThreadEnvironmentConfig.Hub
        Get
            Return DirectCast(Me(PROPERTY_NAME_HUB), String)
        End Get
        Set(value As String)
            Me(PROPERTY_NAME_HUB) = value
        End Set
    End Property

    <ConfigurationProperty(PROPERTY_NAME_MACHINE_DOMAIN, IsRequired:=True, IsKey:=False)> _
    Public Property MachineDomain As String Implements IThreadEnvironmentConfig.MachineDomain
        Get
            Return DirectCast(Me(PROPERTY_NAME_MACHINE_DOMAIN), String)
        End Get
        Set(value As String)
            Me(PROPERTY_NAME_MACHINE_DOMAIN) = value
        End Set
    End Property
End Class
