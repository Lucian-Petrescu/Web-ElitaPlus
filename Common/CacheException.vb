Imports System.Runtime.Serialization

<Serializable()> _
Public Class CacheException
    Inherits ElitaPlusException

    Public Sub New(ByVal msg As String, Optional ByVal innerExcp As Exception = Nothing)
        MyBase.New(msg, ErrorCodes.CONFIG_CACHE_ERR, innerExcp)
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
