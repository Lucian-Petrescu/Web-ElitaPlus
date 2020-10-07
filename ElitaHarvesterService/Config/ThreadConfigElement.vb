Imports System.Configuration

Public MustInherit Class ThreadConfigElement
    Inherits ConfigurationElement
    Implements IThreadConfig

    Private Const PROPERTY_NAME_NAME As String = "Name"
    Private Const PROPERTY_NAME_ENVIRONMENT As String = "Environment"
    Private Const PROPERTY_NAME_SLEEP_TIME_SECONDS As String = "SleepTimeSeconds"

    Public Sub New()

    End Sub

    Public Sub New(pName As String, pEnvironment As String)
        Name = pName
        Environment = pEnvironment
    End Sub

    <ConfigurationProperty(PROPERTY_NAME_NAME, IsRequired:=True, IsKey:=True)> _
    Public Property Name As String Implements IThreadConfig.Name
        Get
            Return DirectCast(Me(PROPERTY_NAME_NAME), String)
        End Get
        Set(value As String)
            Me(PROPERTY_NAME_NAME) = value
        End Set
    End Property

    <ConfigurationProperty(PROPERTY_NAME_ENVIRONMENT, IsRequired:=True, IsKey:=False)> _
    Public Property Environment As String Implements IThreadConfig.Environment
        Get
            Return DirectCast(Me(PROPERTY_NAME_ENVIRONMENT), String)
        End Get
        Set(value As String)
            Me(PROPERTY_NAME_ENVIRONMENT) = value
        End Set
    End Property

    <ConfigurationProperty(PROPERTY_NAME_SLEEP_TIME_SECONDS, IsRequired:=False, DefaultValue:=600, IsKey:=False)> _
    Public Property SleepTimeSeconds As Integer Implements IThreadConfig.SleepTimeSeconds
        Get
            Return DirectCast(Me(PROPERTY_NAME_SLEEP_TIME_SECONDS), Integer)
        End Get
        Set(value As Integer)
            Me(PROPERTY_NAME_SLEEP_TIME_SECONDS) = value
        End Set
    End Property
End Class
