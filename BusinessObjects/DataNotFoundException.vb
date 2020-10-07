Imports System.Runtime.Serialization



<Serializable> Public NotInheritable Class DataNotFoundException
    Inherits ElitaPlusException

    Public Sub New()
        MyBase.New(Assurant.ElitaPlus.Common.ErrorCodes.BO_DATA_NOT_FOUND, ErrorTypes.ERROR_BO)
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message, ErrorTypes.ERROR_BO)
    End Sub

    Public Sub New(ByVal message As String, ByVal innerException As Exception)
        MyBase.New(message, ErrorTypes.ERROR_BO, innerException)
    End Sub

    Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class
