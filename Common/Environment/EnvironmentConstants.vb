''' <summary>
''' The module is responsible for handling Environment Variable Values like declaring constrants and converting from strings to Enums
''' </summary>
Friend Module EnvironmentConstants

    Private Const Development As String = "DEVELOPMENT"
    Private Const Test As String = "TEST"
    Private Const Model As String = "MODL"
    Private Const Production As String = "PROD"
    Friend Const [Default] As String = Development

    ''' <summary>
    ''' Converts AssureNet Environment Valriable to Environment Enumeration
    ''' </summary>
    ''' <param name="pAssureNetEnvironment">AssureNet Environment Variable</param>
    ''' <returns></returns>
    Friend Function ToEnvironments(ByVal pAssureNetEnvironment As String) As Environments

        Select Case pAssureNetEnvironment.ToUpperInvariant()
            Case Production
                Return Environments.Production
            Case Model
                Return Environments.Model
            Case Test
                Return Environments.Test
            Case Development
                Return Environments.Development
            Case Else
                Throw New NotSupportedException()
        End Select

    End Function

End Module
