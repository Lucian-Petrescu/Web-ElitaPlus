Imports System.Runtime.Serialization


<Serializable()> Public NotInheritable Class ConfigAccessException
    Inherits ElitaPlusException

    Public Sub New()
        Me.New("Error Accessing the App Config")
    End Sub

    Public Sub New(message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, ErrorCodes.CONFIG_ACCESS_ERR, innerException)
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class
