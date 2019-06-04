Imports System.Collections.Specialized
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Common

Public Class ElitaBatchProcessingException
    Inherits Assurant.Common.MessagePublishing.BasePublishableException

    Public Sub New(ByVal message As String, ByVal code As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, innerException)
        Me.Type = ErrorTypes.ERROR_UNEXPECTED
        Me.Code = code
        Me.SourceApplicationName = "Elita+ Batch"
        AppConfig.Log(CType(Me, Exception))
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class
