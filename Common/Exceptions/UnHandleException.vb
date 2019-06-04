Public Class UnHandledException
    Inherits ElitaPlusException

    Public Sub New(Optional ByVal innerException As Exception = Nothing)
        MyBase.New("Unexpected application error. See inner exception for details", ErrorCodes.UNEXPECTED_ERROR, innerException)
        Me.Type = ErrorTypes.ERROR_UNEXPECTED
    End Sub
End Class
