Imports System.Runtime.CompilerServices
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ServiceModel

Public Module ValidationExtensions

    <Extension()>
    Public Sub HandleFault(ByVal pResults As IEnumerable(Of ValidationResult))
        If ((pResults Is Nothing) OrElse (pResults.Count() = 0)) Then
            Exit Sub
        End If

        Throw New FaultException(Of ValidationFault)(New ValidationFault(pResults.ToDictionary()), "Validation Faults Found")

    End Sub

    <Extension()>
    Private Function ToDictionary(ByVal pResults As IEnumerable(Of ValidationResult)) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)
        For Each vr As ValidationResult In pResults
            result.Add(vr.MemberNames.FirstOrDefault(), vr.ErrorMessage)
        Next
        Return result
    End Function
End Module
