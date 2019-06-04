Imports System.Configuration

Public Class HarvesterSection
    Inherits ConfigurationSection

    Private Shared _instance As HarvesterSection = Nothing
    Private Shared _syncRoot As Object = New Object

    <ConfigurationProperty("Harvesters", IsDefaultCollection:=False)> _
    <ConfigurationCollection(GetType(HarvestersCollection), AddItemName:="add", ClearItemsName:="clear", RemoveItemName:="remove")> _
    Public ReadOnly Property Harvesters As HarvestersCollection
        Get
            Return DirectCast(MyBase.Item("Harvesters"), HarvestersCollection)
        End Get
    End Property

    Friend Shared ReadOnly Property Current As HarvesterSection
        Get
            If (_instance Is Nothing) Then
                SyncLock _syncRoot
                    If (_instance Is Nothing) Then
                        ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                        _instance = DirectCast(ConfigurationManager.GetSection("HarvesterSection"), HarvesterSection)
                    End If
                End SyncLock
            End If
            Return _instance
        End Get
    End Property
End Class
