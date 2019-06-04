Friend Module EnvironmentVariableHelper

    Friend Function GetEnvironmentVariableValue(ByVal pVariableName As String, ByVal pDefaultValue As String) As String
        Dim value As String = System.Environment.GetEnvironmentVariable(pVariableName)
        If String.IsNullOrWhiteSpace(value) AndAlso Not String.IsNullOrWhiteSpace(pDefaultValue) Then
            Return pDefaultValue
        End If
        Return value
    End Function

End Module
