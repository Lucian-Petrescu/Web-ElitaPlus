Imports System.Collections.Specialized
Imports System.Runtime.Serialization

<Serializable()> _
Public Class ElitaPlusException
    Inherits BasePublishableException

#Region "Constructors"

    'Paramters:
    'message: The English Version of the Exception Message
    'code: the unique code identifying this class of exception (e.g.: "BO_VALIDATION_EXCEPTION")
    'innerException: The lower level exception caught    
    Public Sub New(message As String, code As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, innerException)
        Type = ErrorTypes.ERROR_GENERAL
        Me.Code = code
        SourceApplicationName = "Elita+"
    End Sub


    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
#End Region



End Class
