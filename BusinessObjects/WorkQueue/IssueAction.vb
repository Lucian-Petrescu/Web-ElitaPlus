Imports Assurant.ElitaPlus.BusinessObjectsNew.WrkQueue
Imports System.Xml.Serialization
Imports System.IO
Imports System.Web

Public NotInheritable Class IssueAction
    Inherits BaseActionProvider

#Region "Constants"
    Private Const NO_DATA As String = "DATA_NOT_FOUND"
#End Region

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

#Region "Private Methods"

    Protected Overrides Sub GenerateDisplayXML()

        Dim issueActionView As IssueActionView = GetIssueActionView(_workQueueItem.WorkQueueItem.ClaimId, _workQueueItem.WorkQueueItem.ClaimIssueId)
        Dim xmlString As String = Serialize(issueActionView)
        _xml = New Xml.XmlDocument
        _xml.LoadXml(xmlString)
    End Sub

    Private Function Serialize(ByVal item As IssueActionView) As String
        Dim objXS As New XmlSerializer(item.GetType())
        Dim objSW As New StringWriter

        objXS.Serialize(objSW, item)

        Return objSW.ToString()
    End Function

    Private Function GetIssueActionView(ByVal claimId As Guid, ByVal claimIssueId As Guid) As IssueActionView

        Dim claim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(claimId)
        Dim claimIssue As ClaimIssue = If(claimIssueId <> Nothing, New ClaimIssue(claimIssueId), Nothing)
        Dim certItemCvg As CertItemCoverage = New CertItemCoverage(claim.CertItemCoverageId)
        Dim certItem As CertItem = New CertItem(certItemCvg.CertItemId)

        Dim issueActionView As New IssueActionView()
        issueActionView.ClaimNumber = claim.ClaimNumber
        issueActionView.DateOfLoss = claim.LossDate.Value.ToString()
        issueActionView.InsuredName = claim.CustomerName
        issueActionView.IssueDate = If(claimIssue Is Nothing, TranslationBase.TranslateLabelOrMessage(NO_DATA), claimIssue.CreatedDate.ToString())
        issueActionView.IssueDescription = If(claimIssue Is Nothing, TranslationBase.TranslateLabelOrMessage(NO_DATA), claimIssue.IssueDescription)
        issueActionView.TypeOfLoss = LookupListNew.GetDescriptionFromId(LookupListNew.LK_RISKTYPES, certItem.RiskTypeId)

        Dim claimIssueView As Claim.ClaimIssuesView = claim.GetClaimIssuesView()
        If (Not claimIssue Is Nothing) Then
            claimIssueView.RowFilter = " Issue_description <> '" & claimIssue.IssueDescription & "'"
        End If

        Dim ds As DataSet = New DataSet
        ds.Tables.Add(claimIssueView.ToTable("OtherClaimIssues"))
        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.LoadXml(ds.GetXml())
        issueActionView.OtherClaimIssuesXML = xmlDoc

        _action = New WorkQueueAction()
        If (claim.StatusCode = Codes.CLAIM_STATUS__PENDING) Then

            _action.Target = WorkQueueAction.TargetPage.NewClaimForm
        Else
            _action.Target = WorkQueueAction.TargetPage.ClaimDetails
        End If


        Return issueActionView
    End Function

    Protected Overrides Sub LoadXSLTPath()

        'Dim wkQ As Assurant.ElitaPlus.BusinessObjectsNew.WorkQueue = New Assurant.ElitaPlus.BusinessObjectsNew.WorkQueue(_workQueueItem.WorkQueueItem.WorkQueueId)
        If (TransformationFileExists(_workQueueItem.WorkQueue.WorkQueue.TransformationFile)) Then
            _xsltPath = New Uri(HttpContext.Current.Server.MapPath(String.Format("{0}{1}", BASEXSLTPATH, _workQueueItem.WorkQueue.WorkQueue.TransformationFile)))
        Else
            Throw New BOInvalidOperationException("XSLT file does not exists for the Work Queue")
        End If

    End Sub

#End Region

#Region "View"
    <Serializable> _
    Class IssueActionView

        Private _insuredName As String

        Public Property InsuredName As String
            Get
                Return _insuredName
            End Get
            Set
                _insuredName = value
            End Set
        End Property

        Private _claimNumber As String

        Public Property ClaimNumber As String
            Get
                Return _claimNumber
            End Get
            Set
                _claimNumber = value
            End Set
        End Property

        Private _dateOfLoss As String

        Public Property DateOfLoss As String
            Get
                Return _dateOfLoss
            End Get
            Set
                _dateOfLoss = value
            End Set
        End Property

        Private _typeOfLoss As String

        Public Property TypeOfLoss As String
            Get
                Return _typeOfLoss
            End Get
            Set
                _typeOfLoss = value
            End Set
        End Property

        Private _issueDate As String

        Public Property IssueDate As String
            Get
                Return _issueDate
            End Get
            Set
                _issueDate = value
            End Set
        End Property

        Private _issueDescription As String

        Public Property IssueDescription As String
            Get
                Return _issueDescription
            End Get
            Set
                _issueDescription = value
            End Set
        End Property

        Private _otherClaimIssuesXML As Xml.XmlDocument

        Public Property OtherClaimIssuesXML As Xml.XmlDocument
            Get
                Return _otherClaimIssuesXML
            End Get
            Set
                _otherClaimIssuesXML = value
            End Set
        End Property

    End Class

#End Region

End Class


