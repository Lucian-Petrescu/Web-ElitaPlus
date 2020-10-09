Imports System.Runtime.Serialization

<Serializable()> Public Class DALInvalidParameterValueException
    Inherits DALException

    Public Sub New(Message As String, Optional ByVal innerExc As Exception = Nothing)
        MyBase.New(Message, innerExc)
        Code = ErrorCodes.DAL_INVALID_OPERATION_PARAMETER_VALUE
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
