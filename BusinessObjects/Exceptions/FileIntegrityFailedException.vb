Public Class FileIntegrityFailedException
    Inherits ElitaPlusException

    Private ReadOnly oStoredHash As String
    Private ReadOnly oComputedHash As String
    Private ReadOnly oRepositoryCode As String
    Private ReadOnly oStoragePath As String
    Private ReadOnly oAbsoluteFileName As String

    Public ReadOnly Property StoredHash As String
        Get
            Return oStoredHash
        End Get
    End Property

    Public ReadOnly Property ComputedHash As String
        Get
            Return oComputedHash
        End Get
    End Property

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

    Public Sub New(pRepositoryCode As String, pStoragePath As String, pAbsoluteFileName As String, pStoredHash As String, pComputedHash As String, ex As Exception)
        MyBase.New(String.Format("Document Download Failed for Repository : {0}, Storage Path : {1}, Absolute Path : {2}, Stored Hash : {3}, Computed Hash : {4}", _
                                 pRepositoryCode, pStoragePath, pAbsoluteFileName, pStoredHash, pComputedHash), "FILE_INTEGRITY_FAILED", ex)
        oRepositoryCode = pRepositoryCode
        oStoragePath = pStoragePath
        oAbsoluteFileName = pAbsoluteFileName
        oStoredHash = pStoredHash
        oComputedHash = pComputedHash
    End Sub

End Class
