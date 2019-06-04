Public Class DocumentDownloadFailedException
    Inherits ElitaPlusException

    Private ReadOnly oRepositoryCode As String
    Private ReadOnly oStoragePath As String
    Private ReadOnly oAbsoluteFileName As String

    Public ReadOnly Property RepositoryCode As String
        Get
            Return oRepositoryCode
        End Get
    End Property

    Public ReadOnly Property StoragePath As String
        Get
            Return oStoragePath
        End Get
    End Property

    Public ReadOnly Property AbsoluteFileName As String
        Get
            Return oAbsoluteFileName
        End Get
    End Property

    Public Sub New(ByVal pRepositoryCode As String, ByVal pStoragePath As String, ByVal pAbsoluteFileName As String, ByVal ex As Exception)
        MyBase.New(String.Format("Document Download Failed for Repository : {0}, Storage Path : {1}, Absolute Path : {2}", pRepositoryCode, pStoragePath, pAbsoluteFileName), "DOCUMENT_DOWNLOAD_FAILED", ex)
        oRepositoryCode = pRepositoryCode
        oStoragePath = pStoragePath
        oAbsoluteFileName = pAbsoluteFileName
    End Sub

End Class
