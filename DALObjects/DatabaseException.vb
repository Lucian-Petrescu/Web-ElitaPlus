Imports System.Runtime.Serialization


<Serializable()> Public Class DatabaseException
    Inherits DALException

    Public Sub New(message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, innerException)
        Code = ErrorCodes.DB_ERROR
        Type = ErrorTypes.ERROR_DATABASE
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class
