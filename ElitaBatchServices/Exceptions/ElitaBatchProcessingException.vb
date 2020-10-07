Imports System.Collections.Specialized
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Common

Public Class ElitaBatchProcessingException
    Inherits Assurant.Common.MessagePublishing.BasePublishableException

    Public Sub New(message As String, code As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, innerException)
        Type = ErrorTypes.ERROR_UNEXPECTED
        Me.Code = code
        SourceApplicationName = "Elita+ Batch"
        AppConfig.Log(CType(Me, Exception))
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class
