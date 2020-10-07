Imports System.Configuration

Public Class HarvestersCollection
    Inherits ConfigurationElementCollection

    Public Sub New()
    End Sub

    Default Public Shadows Property Item(index As Integer) As ThreadConfigElement
        Get
            Return DirectCast(BaseGet(index), ThreadConfigElement)
        End Get
        Set(value As ThreadConfigElement)
            If (BaseGet(index) IsNot Nothing) Then
                BaseRemoveAt(index)
            End If
            BaseAdd(index, value)
        End Set
    End Property

    Public Sub Add(serviceConfig As ThreadConfigElement)
        BaseAdd(serviceConfig)
    End Sub

    Public Sub Clear()
        BaseClear()
    End Sub

    Protected Overrides Function CreateNewElement() As ConfigurationElement
        Return New ThreadEnvironmentConfigElement
    End Function

    Protected Overrides Function GetElementKey(element As ConfigurationElement) As Object
        Return (DirectCast(element, ThreadConfigElement)).Name
    End Function

    Public Sub Remove(serviceConfig As ThreadConfigElement)
        If BaseIndexOf(serviceConfig) >= 0 Then
            BaseRemove(serviceConfig.Name)
        End If
    End Sub

    Public Sub RemoveAt(index As Integer)
        BaseRemoveAt(index)
    End Sub

    Public Sub Remove(name As String)
        BaseRemove(name)
    End Sub

#If DEBUG Then
    Friend Function GetConfigDump() As String
        Dim sb As New Text.StringBuilder
        sb.AppendFormat("Found {0} Configurations", Me.Count)
        sb.AppendLine()
        For Each tec As ThreadConfigElement In Me
            If (tec.GetType().Equals(GetType(ThreadEnvironmentConfigElement))) Then
                Dim tece As ThreadEnvironmentConfigElement = DirectCast(tec, ThreadEnvironmentConfigElement)
                sb.AppendFormat("[Name:{0};Environment{1};Hub{2};MachineDomain{3};SleepTimeSeconds{4}]", _
                                tec.Name, tec.Environment, tece.Hub, tece.MachineDomain, tec.SleepTimeSeconds)
                sb.AppendLine()
            End If
        Next
        sb.AppendLine("End of Configuration")
        Return sb.ToString()
    End Function
#End If
End Class
