Imports System.Runtime.Serialization
Imports System.Collections.Generic

<DataContract(Name:="ValidationFault", Namespace:="http://elita.assurant.com/Faults")> _
Public Class ValidationFault

    Public Sub New()
        Me.New(New Dictionary(Of String, String))
    End Sub

    Public Sub New(ByVal pValidationErrors As Dictionary(Of String, String))
        Me.ValidationErrors = pValidationErrors
    End Sub

    Friend Shared Sub Validate(ByVal pObject As Object)
        ' Scan thru object to check if DataMember attribute is present

    End Sub

    <DataMember(Name:="ValidationErrors")> _
    Public Property ValidationErrors As Dictionary(Of String, String)

End Class

