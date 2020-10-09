Imports System.Runtime.Serialization

<Serializable()> Public Class StoredProcedureGeneratedException
    Inherits ElitaPlusException

    Public Sub New(message As String, StoredProcedureErrorMSG As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, StoredProcedureErrorMSG, innerException)
        Type = ErrorTypes.ERROR_DATABASE
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
