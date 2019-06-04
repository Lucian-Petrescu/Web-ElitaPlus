Public Class ServerViewStatePage

    Inherits System.Web.UI.Page

    Protected Overrides ReadOnly Property PageStatePersister() As System.Web.UI.PageStatePersister
        Get
            Return New SessionPageStatePersister(Me)
            'Return MyBase.PageStatePersister
        End Get
    End Property

    'Protected Overrides Sub SavePageStateToPersistenceMedium(ByVal viewState As Object)
    '    Dim _formatter As New LosFormatter
    '    '-- Use the URL to make a key for this page
    '    Dim key As String = Request.Url.ToString & "__VIEWSTATE"
    '    '-- Create a new memory stream
    '    Dim memStream As New System.IO.MemoryStream
    '    '-- Serialize the viewstate to the memory stream
    '    _formatter.Serialize(memStream, viewState)
    '    '-- Ensure the data has been written
    '    memStream.Flush()
    '    '-- Save it in the session object
    '    Session(key) = memStream
    'End Sub

    'Protected Overrides Function LoadPageStateFromPersistenceMedium() As Object


    '    Dim _formatter As New LosFormatter
    '    '-- Use the URL to make a key for this page
    '    Dim key As String = Request.Url.ToString & "__VIEWSTATE"
    '    '-- Is a memory stream there in the session?
    '    If Not Session(key) Is Nothing Then
    '        '-- Yes, return it
    '        Dim memStream As System.IO.MemoryStream = _
    '            CType(Session(key), System.IO.MemoryStream)
    '        '-- Seek to the first byte
    '        memStream.Seek(0, IO.SeekOrigin.Begin)
    '        '-- Deserialize it into the viewstate
    '        Return _formatter.Deserialize(memStream)
    '    End If


    'End Function

End Class
