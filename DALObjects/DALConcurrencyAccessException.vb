Imports System.Runtime.Serialization

<Serializable()> Public Class DALConcurrencyAccessException
    Inherits DALException

    Public Sub New(Optional ByVal innerExc As Exception = Nothing)
        MyBase.New("Concurrent Record Access Exception. The database has changed", innerExc)
        Me.Code = ErrorCodes.DAL_CONCURRENT_DATA_MODIFICATION
    End Sub


    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
