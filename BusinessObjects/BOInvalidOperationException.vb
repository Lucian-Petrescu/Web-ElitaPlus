Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Common


<Serializable> Public NotInheritable Class BOInvalidOperationException
    Inherits ElitaPlusException

    Public Sub New()
        Me.New("Invalid operation for the current state.")
    End Sub

    Public Sub New(ByVal message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New("Invalid operation for the current state: " & message, ErrorCodes.BO_INVALID_OPERATION, innerException)
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class
