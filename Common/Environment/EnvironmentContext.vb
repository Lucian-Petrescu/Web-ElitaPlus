Public NotInheritable Class EnvironmentContext

#Region "Singleton Implementation"
    Private Shared msSyncRoot As Object = New Object()
    Private Shared msInstance As EnvironmentContext

    Public Shared ReadOnly Property Current As EnvironmentContext
        Get
            If (msInstance Is Nothing) Then
                SyncLock (msSyncRoot)
                    If (msInstance Is Nothing) Then
                        msInstance = New EnvironmentContext()
                    End If
                End SyncLock
            End If
            Return msInstance
        End Get
    End Property

#End Region

    Private ReadOnly mCurrentEnvironment As Environments
    Private ReadOnly mCurrentEnvironmentName As String
    Private ReadOnly mCurrentEnvironmentShortName As String

    Private Sub New()
        mCurrentEnvironmentName =
            EnvironmentVariableHelper.GetEnvironmentVariableValue(
                EnvironmentVariables.AssurNetEnvironment,
                EnvironmentConstants.Default).ToUpperInvariant()
        mCurrentEnvironment = EnvironmentConstants.ToEnvironments(mCurrentEnvironmentName)
        If (mCurrentEnvironment = Environments.Development) Then
            mCurrentEnvironmentShortName = "dev"
        Else
            mCurrentEnvironmentShortName = mCurrentEnvironmentName.ToLowerInvariant()
        End If
    End Sub

    Public ReadOnly Property Environment As Environments
        Get
            Return mCurrentEnvironment
        End Get
    End Property

    Public ReadOnly Property EnvironmentName As String
        Get
            Return mCurrentEnvironmentName
        End Get
    End Property

    Public ReadOnly Property EnvironmentShortName As String
        Get
            Return mCurrentEnvironmentShortName
        End Get
    End Property

End Class
