Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Common

<Serializable> _
Public NotInheritable Class WorkQueueItemNotLockedByUserException
    Inherits ElitaPlusException

    Public Sub New()
        Me.New("Work Queue Item cannot be processed because its time for processing has expired.Please load next item.")
    End Sub

    Public Sub New(message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New("Work Queue Item cannot be processed because its time for processing has expired.Please load next item." & message, ErrorCodes.GUI_ERROR_QUEUE_ITEM_TIME_EXPIRED, innerException)
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class