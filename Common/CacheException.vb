Imports System.Runtime.Serialization

<Serializable()> _
Public Class CacheException
    Inherits ElitaPlusException

    Public Sub New(msg As String, Optional ByVal innerExcp As Exception = Nothing)
        MyBase.New(msg, ErrorCodes.CONFIG_CACHE_ERR, innerExcp)
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
