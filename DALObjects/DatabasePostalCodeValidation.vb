Imports System.Runtime.Serialization


<Serializable()> Public Class DatabasePostalCodeValidation
    Inherits ElitaPlusException

#Region "Constructors"
    Public Sub New(Optional ByVal innerException As Exception = Nothing)
        MyBase.New("Integrity Constraint Violation", ErrorCodes.DB_ERROR_POSTAL_CODE_FORMAT_NOT_RIGHT, innerException)
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub
#End Region

End Class