Imports System.Runtime.Serialization

<Serializable()> Public Class DALInvalidParameterValueException
    Inherits DALException

    Public Sub New(ByVal Message As String, Optional ByVal innerExc As Exception = Nothing)
        MyBase.New(Message, innerExc)
        Me.Code = ErrorCodes.DAL_INVALID_OPERATION_PARAMETER_VALUE
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
