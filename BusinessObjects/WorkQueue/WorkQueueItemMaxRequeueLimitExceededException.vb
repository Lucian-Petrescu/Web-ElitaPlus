Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Common

<Serializable> _
Public NotInheritable Class WorkQueueItemMaxRequeueLimitExceededException
    Inherits ElitaPlusException

    Public Sub New()
        Me.New(TranslationBase.TranslateLabelOrMessage("MSG_MAX_REQUEUE_COUNT_REACHED"))
    End Sub

    Public Sub New(message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(TranslationBase.TranslateLabelOrMessage("MSG_MAX_REQUEUE_COUNT_REACHED") & message, ErrorCodes.GUI_ERROR_MAX_REQUEUE_LIMIT_EXCEEDED, innerException)
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class