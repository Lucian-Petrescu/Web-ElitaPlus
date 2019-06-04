Imports System.Configuration
Imports Assurant.Elita.WorkerFramework.Config

Namespace Workers.DocumentUpload
    Public Class DocumentUploadWorkerConfig
        Inherits WorkerConfigurationElement

        Private Class PropertyNames
            Public Const DropFolder As String = "DropFolder"
            Public Const StageingFolder As String = "StageingFolder"
            Public Const ErrorFolder As String = "ErrorFolder"
            Public Const ProcessedFolder As String = "ProcessedFolder"
            Public Const CompressedFileSearchPattern As String = "CompressedFileSearchPattern"
            Public Const Hub As String = "Hub"
            Public Const MachineDomain As String = "MachineDomain"
        End Class

        <ConfigurationProperty(PropertyNames.DropFolder, IsRequired:=True, IsKey:=True)>
        Public Property DropFolder() As String
            Get
                Return DirectCast(Me(PropertyNames.DropFolder), String)
            End Get
            Set
                Me(PropertyNames.DropFolder) = Value
            End Set
        End Property

        <ConfigurationProperty(PropertyNames.ErrorFolder, IsRequired:=True, IsKey:=True)>
        Public Property ErrorFolder() As String
            Get
                Return DirectCast(Me(PropertyNames.ErrorFolder), String)
            End Get
            Set
                Me(PropertyNames.ErrorFolder) = Value
            End Set
        End Property

        <ConfigurationProperty(PropertyNames.ProcessedFolder, IsRequired:=True, IsKey:=True)>
        Public Property ProcessedFolder() As String
            Get
                Return DirectCast(Me(PropertyNames.ProcessedFolder), String)
            End Get
            Set
                Me(PropertyNames.ProcessedFolder) = Value
            End Set
        End Property

        <ConfigurationProperty(PropertyNames.StageingFolder, IsRequired:=True, IsKey:=True)>
        Public Property StageingFolder() As String
            Get
                Return DirectCast(Me(PropertyNames.StageingFolder), String)
            End Get
            Set
                Me(PropertyNames.StageingFolder) = Value
            End Set
        End Property

        <ConfigurationProperty(PropertyNames.CompressedFileSearchPattern, IsRequired:=True, IsKey:=True)>
        Public Property CompressedFileSearchPattern() As String
            Get
                Return DirectCast(Me(PropertyNames.CompressedFileSearchPattern), String)
            End Get
            Set
                Me(PropertyNames.CompressedFileSearchPattern) = Value
            End Set
        End Property

        <ConfigurationProperty(PropertyNames.Hub, IsRequired:=True, IsKey:=True)>
        Public Property Hub() As String
            Get
                Return DirectCast(Me(PropertyNames.Hub), String)
            End Get
            Set
                Me(PropertyNames.Hub) = Value
            End Set
        End Property

        <ConfigurationProperty(PropertyNames.MachineDomain, IsRequired:=True, IsKey:=True)>
        Public Property MachineDomain() As String
            Get
                Return DirectCast(Me(PropertyNames.MachineDomain), String)
            End Get
            Set
                Me(PropertyNames.MachineDomain) = Value
            End Set
        End Property

    End Class
End Namespace

