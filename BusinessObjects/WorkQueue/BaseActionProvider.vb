Imports Assurant.ElitaPlus.BusinessObjectsNew.WrkQueue
Imports System.Xml.Serialization
Imports System.IO
Imports System.Web
Public MustInherit Class BaseActionProvider

#Region "Constants"
    Friend Const BASEXSLTPATH As String = "~/WorkQueue/"
#End Region

#Region "Private Fields"
    Friend _xml As Xml.XmlDocument
    Friend _action As WorkQueueAction
    Friend _xsltPath As Uri
    Friend _workQueueItem As WorkQueueItem

#End Region

#Region "Public Properties"

    Public ReadOnly Property DisplayXml As Xml.XmlDocument
        Get
            If (_xml Is Nothing) Then
                GenerateDisplayXML()
            End If
            Return _xml
        End Get
    End Property

    Public ReadOnly Property Action As WorkQueueAction
        Get
            Return _action
        End Get
    End Property

    Public ReadOnly Property XsltPath As System.Uri
        Get
            If (_xsltPath Is Nothing) Then
                LoadXSLTPath()
            End If
            Return _xsltPath
        End Get
    End Property

    Public ReadOnly Property WorkQueueItem As WorkQueueItem
        Get
            Return _workQueueItem
        End Get
    End Property


#End Region

    Protected MustOverride Sub GenerateDisplayXML()
    Protected MustOverride Sub LoadXSLTPath()

    Private Shared Function CreateActionProvider(ByVal actionCode As String) As BaseActionProvider
        Throw New NotImplementedException()
    End Function

    Shared Function CreateActionProvider(ByVal workQueueItem As WorkQueueItem) As BaseActionProvider

        Dim actionProv As BaseActionProvider

        Select Case workQueueItem.WorkQueueItemType
            Case workQueueItem.ItemType.Issue
                actionProv = New IssueAction(workQueueItem)
            Case workQueueItem.ItemType.Image
                actionProv = New ImageAction(workQueueItem)
            Case Else
                '''TODO:Add Message
                Throw New BOInvalidOperationException()
        End Select

        Return actionProv
    End Function

    Shared Function GetAction() As BaseActionProvider
        Dim wqItem As WorkQueueItem = WorkQueueItem.GetNextValidWorkQueueItem(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        Dim actionProvider As BaseActionProvider
        If (Not wqItem Is Nothing) Then
            actionProvider = BaseActionProvider.CreateActionProvider(wqItem)
        End If
        Return actionProvider
    End Function

    Friend Shared Function TransformationFileExists(ByVal fileName As String) As Boolean
        Dim flag As Boolean = False
        Dim xsltPath As New Uri(HttpContext.Current.Server.MapPath(String.Format("{0}{1}", BASEXSLTPATH, fileName)))
        If (File.Exists(xsltPath.AbsolutePath)) Then
            flag = True
        End If

        Return flag
    End Function

End Class
