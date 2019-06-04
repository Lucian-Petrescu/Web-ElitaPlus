Imports System.Runtime.Serialization

<Serializable()> Public Class StoredProcedureGeneratedException
    Inherits ElitaPlusException

    Public Sub New(ByVal message As String, ByVal StoredProcedureErrorMSG As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, StoredProcedureErrorMSG, innerException)
        Me.Type = ErrorTypes.ERROR_DATABASE
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
