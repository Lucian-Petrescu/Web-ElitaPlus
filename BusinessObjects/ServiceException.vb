Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Common

<Serializable> _
Public NotInheritable Class ServiceException
    Inherits ElitaPlusException

    Public Sub New()
        Me.New("One of the Web Service Failed.")
        MyBase.Type = ErrorTypes.ERROR_UNEXPECTED
    End Sub

    Public Sub New(ByVal message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New("One of the Web Service Failed. " & message, ErrorCodes.ERR_WEB_SERVICE_CALL_FAILED, innerException)
        MyBase.Type = ErrorTypes.ERROR_UNEXPECTED
    End Sub

    Public Sub New(ByVal service As String, ByVal operation As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(String.Format("Operation {1} of {0} Failed.", service, operation), ErrorCodes.ERR_WEB_SERVICE_CALL_FAILED, innerException)
        MyBase.Type = ErrorTypes.ERROR_UNEXPECTED
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
        MyBase.Type = ErrorTypes.ERROR_UNEXPECTED
    End Sub

End Class
