Public NotInheritable Class DocumentFormatNotFound
    Inherits ElitaPlusException

    Private ReadOnly oExtension As String

    Public ReadOnly Property Extension As String
        Get
            Return oExtension
        End Get
    End Property

    Public Sub New(ByVal pExtension As String)
        MyBase.New(String.Format("Document Format with Extension {0} not found.", pExtension), "DOCUMENT_FORMAT_NOT_FOUND")
        oExtension = pExtension
    End Sub

End Class