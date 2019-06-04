Imports System.Runtime.Serialization


<Serializable()> Public Class DatabaseException
    Inherits DALException

    Public Sub New(ByVal message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, innerException)
        Me.Code = ErrorCodes.DB_ERROR
        Me.Type = ErrorTypes.ERROR_DATABASE
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class
