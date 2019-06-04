Imports System.Runtime.Serialization


<Serializable()> Public Class DataBaseUniqueKeyConstraintViolationException
    Inherits ElitaPlusException

#Region "Constructors"
    Public Sub New(Optional ByVal innerException As Exception = Nothing)
        MyBase.New("Unique Key Constraint Violation", ErrorCodes.DUPLICATE_KEY_CONSTRAINT_VIOLATED, innerException)
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub
#End Region

End Class
