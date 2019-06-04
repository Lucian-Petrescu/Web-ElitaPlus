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
    Public Sub New(ByVal message As String, ByVal code As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, innerException)
        Me.Type = ErrorTypes.ERROR_GENERAL
        Me.Code = code
        Me.SourceApplicationName = "Elita+"
    End Sub


    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub
#End Region



End Class
