Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic

Module ExtensionHelper

    <Extension()> _
    Public Function ToInteger(ByVal inputString As String, Optional ByVal defaultValue As Integer = 0) As Integer
        Dim outputValue As Integer = defaultValue
        'should we be logging an 'error' if we are attempting to convert a 'bad' string into an integer?
        If (Not String.IsNullOrWhiteSpace(inputString) And IsNumeric(inputString)) Then
            Integer.TryParse(inputString, outputValue)
        End If
        Return outputValue
    End Function

End Module
