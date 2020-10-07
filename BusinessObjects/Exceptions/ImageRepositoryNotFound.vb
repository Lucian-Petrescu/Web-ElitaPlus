Public NotInheritable Class ImageRepositoryNotFound
    Inherits ElitaPlusException

    Private ReadOnly oRepositoryName As String

    Public ReadOnly Property RepositoryName As String
        Get
            Return oRepositoryName
        End Get
    End Property

    Public Sub New(ByVal pRepositoryName As String)
        MyBase.New(String.Format("Repository {0} not found.", pRepositoryName), "REPOSITORY_NOT_FOUND")
        oRepositoryName = pRepositoryName
    End Sub

End Class
