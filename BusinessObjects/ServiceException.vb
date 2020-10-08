Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Common

<Serializable> _
Public NotInheritable Class ServiceException
    Inherits ElitaPlusException

    Public Sub New()
        Me.New("One of the Web Service Failed.")
        Type = ErrorTypes.ERROR_UNEXPECTED
    End Sub

    Public Sub New(message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New("One of the Web Service Failed. " & message, ErrorCodes.ERR_WEB_SERVICE_CALL_FAILED, innerException)
        Type = ErrorTypes.ERROR_UNEXPECTED
    End Sub

    Public Sub New(service As String, operation As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(String.Format("Operation {1} of {0} Failed.", service, operation), ErrorCodes.ERR_WEB_SERVICE_CALL_FAILED, innerException)
        Type = ErrorTypes.ERROR_UNEXPECTED
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
        Type = ErrorTypes.ERROR_UNEXPECTED
    End Sub

End Class
