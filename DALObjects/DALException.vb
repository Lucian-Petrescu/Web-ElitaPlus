Imports System.Runtime.Serialization

<Serializable()> Public Class DALException
    Inherits ElitaPlusException

    Public Sub New(message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, ErrorCodes.DAL_ERROR, innerException)
        Type = ErrorTypes.ERROR_DAL
    End Sub

    Public Sub New(Optional ByVal innerException As Exception = Nothing)
        Me.New("Data Access Layer Error", innerException)
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
