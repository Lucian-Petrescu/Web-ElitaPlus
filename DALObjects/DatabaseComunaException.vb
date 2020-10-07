
Imports System.Runtime.Serialization


<Serializable()> Public Class DatabaseComunaException
    Inherits ElitaPlusException

#Region "Constructors"
    Public Sub New(Optional ByVal innerException As Exception = Nothing)
        MyBase.New("Integrity Constraint Violation", ErrorCodes.DB_ERROR_COMUNA_NOT_FOUND, innerException)
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
#End Region

End Class