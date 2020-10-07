Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic

Module ExtensionHelper

    <Extension()> _
    Public Function ToInteger(inputString As String, Optional ByVal defaultValue As Integer = 0) As Integer
        Dim outputValue As Integer = defaultValue
        'should we be logging an 'error' if we are attempting to convert a 'bad' string into an integer?
        If (Not String.IsNullOrWhiteSpace(inputString) AndAlso IsNumeric(inputString)) Then
            Integer.TryParse(inputString, outputValue)
        End If
        Return outputValue
    End Function

End Module
