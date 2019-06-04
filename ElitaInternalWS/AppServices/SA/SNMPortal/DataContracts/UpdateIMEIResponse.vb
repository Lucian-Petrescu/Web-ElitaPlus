Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports System.ComponentModel.DataAnnotations

Namespace AppServices.SA
    <DataContract(Namespace:="http://elita.assurant.com/AppServices/SA/UpdateIMEIResponse", Name:="UpdateIMEIResponse")>
    Public Class UpdateIMEIResponse

        Public Property ErrorCode As String
        Public Property ErrorMessage As String

    End Class
End Namespace
