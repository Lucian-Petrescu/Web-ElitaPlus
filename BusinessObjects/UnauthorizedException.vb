Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Common

<Serializable> _
Public NotInheritable Class UnauthorizedException
    Inherits ElitaPlusException

    Public Sub New()
        Me.New("You are not authorized to perform this action.")
    End Sub

    Public Sub New(ByVal message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New("You are not authorized to perform this action. " & message, ErrorCodes.BO_INVALID_OPERATION, innerException)
    End Sub

    Public Sub New(ByVal service As String, ByVal operation As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(String.Format("You are not authorized to perform {0} operation on {1} service.", service, operation), ErrorCodes.BO_INVALID_OPERATION, innerException)
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class
