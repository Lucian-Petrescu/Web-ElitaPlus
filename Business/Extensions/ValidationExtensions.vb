Imports System.Runtime.CompilerServices
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.ObjectModel

Public Module ValidationExtensions

    <Extension()>
    Public Function Validate(pValue As Object) As IEnumerable(Of ValidationResult)
        Return Validate(pValue, "Instance")
    End Function

    <Extension()>
    Public Function Validate(pValue As Object, parameterName As String) As IEnumerable(Of ValidationResult)
        Dim result As New Collection(Of ValidationResult)

        If (pValue Is Nothing) Then
            result.Add(New ValidationResult("Object is Null", New String() {parameterName}))
            Return result
        End If

        Validator.TryValidateObject(pValue, New ValidationContext(pValue, Nothing, Nothing), result, True)

        Return result
    End Function

End Module
