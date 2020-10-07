Imports Assurant.ElitaPlus.BusinessObjectsNew.WrkQueue
Imports System.Xml.Serialization
Imports System.IO
Imports System.Web
Public NotInheritable Class ImageAction
    Inherits BaseActionProvider


#Region "Public Properties"

    Public ReadOnly Property ClaimIssueId As Guid
        Get
            Return New ClaimIssue(_workQueueItem.Id).ClaimIssueId
        End Get
    End Property

#End Region

#Region "Constructors"

    Friend Sub New(ByVal workQueueItem As WorkQueueItem)
        _workQueueItem = workQueueItem
        GenerateDisplayXML()
        LoadXSLTPath()
    End Sub

#End Region

#Region "Protected Methods"

    Protected Overrides Sub GenerateDisplayXML()
        Dim imageActionView As ImageActionView = GetImageActionView(_workQueueItem.WorkQueueItem.ImageId, _workQueueItem.ImageScanDate)
        Dim xmlString As String = Serialize(imageActionView)
        _xml = New Xml.XmlDocument
        _xml.LoadXml(xmlString)
    End Sub

    Protected Overrides Sub LoadXSLTPath()
        If (TransformationFileExists(_workQueueItem.WorkQueue.WorkQueue.TransformationFile)) Then
            _xsltPath = New Uri(HttpContext.Current.Server.MapPath(String.Format("{0}{1}", BASEXSLTPATH, _workQueueItem.WorkQueue.WorkQueue.TransformationFile)))
        Else
            Throw New BOInvalidOperationException("XSLT file does not exists for the Work Queue")
        End If
    End Sub

#End Region

#Region "Private Methods"
    Private Function GetImageActionView(ByVal ImageId As Guid, ByVal ScanDate As Nullable(Of Date)) As ImageActionView
        Dim imageActionView As New ImageActionView()
        imageActionView.ImageId = ImageId
        imageActionView.ScanDate = If(ScanDate.HasValue, ScanDate.Value.ToString(), String.Empty)

        _action = New WorkQueueAction()
        _action.Target = WorkQueueAction.TargetPage.ImageIndex

        Return imageActionView

    End Function

    Private Function Serialize(ByVal item As ImageActionView) As String
        Dim objXS As New XmlSerializer(item.GetType())
        Dim objSW As New StringWriter

        objXS.Serialize(objSW, item)

        Return objSW.ToString()
    End Function

#End Region

#Region "View"

    <Serializable> _
    Class ImageActionView

        Private _imageId As Guid

        Public Property ImageId As Guid
            Get
                Return _imageId
            End Get
            Set
                _imageId = value
            End Set
        End Property

        Private _scanDate As String

        Public Property ScanDate As String
            Get
                Return _scanDate
            End Get
            Set
                _scanDate = value
            End Set
        End Property

    End Class

#End Region

End Class
