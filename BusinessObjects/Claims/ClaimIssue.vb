Imports System.Collections.Generic

Public Class ClaimIssue
    Inherits EntityIssue

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(id, familyDS)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(familyDS)
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(row)
    End Sub


#End Region

#Region "Constants"
    Public Const COL_NAME_PROCESSED_BY As String = "processed_by"
    Public Const COL_NAME_PROCESSED_DATE As String = "processed_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by_name"
    Public Const COL_NAME_STATUS_ID As String = "status_id"
    Public Const COL_NAME_STATUS_CODE As String = "status_code"
    Public Const COL_NAME_ISSUE_DESCRIPTION As String = "issue_description"

#End Region

#Region "Properties"
    <ValueMandatory("")> _
    Public ReadOnly Property ClaimIssueId As Guid
        Get
            Return Id
        End Get
    End Property

    Public Property ClaimId As Guid
        Get
            Return EntityId
        End Get
        Set
            EntityId = Value
        End Set
    End Property

    Private _claim As ClaimBase

    Public Property Claim As ClaimBase
        Get
            If (_claim Is Nothing) Then
                If Not ClaimId.Equals(Guid.Empty) AndAlso Entity = "AUTH" Then
                    Dim claimAuth As New ClaimAuthorization(ClaimId)
                    Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(claimAuth.ClaimId, Dataset)
                ElseIf Not ClaimId.Equals(Guid.Empty) AndAlso Entity = "CONSEQUENTIALDAMAGE" Then
                    Dim ClaimConseqDamage As New CaseConseqDamage(ClaimId) ' Here Me.ClaimId will be case_conseq_damage_id
                    Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ClaimConseqDamage.ClaimId, Dataset)
                ElseIf Not ClaimId.Equals(Guid.Empty) Then
                    Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ClaimId, Dataset)
                End If
            End If
            Return _claim
        End Get
        Private Set
            _claim = value
        End Set
    End Property

    Public ReadOnly Property Issue As Issue
        Get
            Dim oIssue As New Issue(IssueId)
            Return oIssue
        End Get
    End Property

    Public ReadOnly Property IssueCode As String
        Get
            Return Issue.Code
        End Get
    End Property

    Public ReadOnly Property IssueDescription As String
        Get
            Return Issue.Description
        End Get
    End Property

    Public ReadOnly Property ClaimIssueQuestionList As IssueQuestionsChildrenList
        Get
            Dim claimIssue As New Issue(IssueId)
            Return claimIssue.IssueQuestionsChildren
        End Get
    End Property

    Public ReadOnly Property ClaimIssueQuestionListByDealer(dealerId As Guid) As IssueQuestionsChildrenList
        Get
            Dim claimIssue As New Issue(IssueId)
            Return claimIssue.IssueQuestionsChildrenByIssueDealer(IssueId, dealerId)
        End Get
    End Property

    Public ReadOnly Property ClaimIssueResponseList As ClaimIssueResponse.ClaimIssueResponseList
        Get
            Return New ClaimIssueResponse.ClaimIssueResponseList(Me)
        End Get
    End Property

    Public ReadOnly Property ClaimIssueStatusList As ClaimIssueStatus.ClaimIssueStatusList
        Get
            Return New ClaimIssueStatus.ClaimIssueStatusList(Me)
        End Get
    End Property

    Public Property ProcessedDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimIssue.COL_NAME_PROCESSED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimIssue.COL_NAME_PROCESSED_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimIssue.COL_NAME_PROCESSED_DATE, Value)
        End Set
    End Property

    Public Property ProcessedBy As String
        Get
            CheckDeleted()
            If Row(ClaimIssue.COL_NAME_PROCESSED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimIssue.COL_NAME_PROCESSED_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimIssue.COL_NAME_PROCESSED_BY, Value)
        End Set
    End Property

    Public ReadOnly Property StatusId As Guid
        Get
            CheckDeleted()
            If Row(ClaimIssue.COL_NAME_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimIssue.COL_NAME_STATUS_ID), Byte()))

            End If
        End Get
    End Property

    Public Property StatusCode As String
        Get
            CheckDeleted()
            If Row(ClaimIssue.COL_NAME_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimIssue.COL_NAME_STATUS_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimIssue.COL_NAME_STATUS_CODE, Value)
        End Set
    End Property

    Public Property CreatedBy As String
        Get
            CheckDeleted()
            If Row(ClaimIssue.COL_NAME_CREATED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimIssue.COL_NAME_CREATED_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimIssue.COL_NAME_CREATED_BY, Value)
        End Set
    End Property

    Public Property CreatedById As String
        Get
            CheckDeleted()
            If Row(DALBase.COL_NAME_CREATED_BY) Is DBNull.Value Then Return Nothing
            Return CType(Row(DALBase.COL_NAME_CREATED_BY), String)
        End Get
        Set
            CheckDeleted()
            SetValue(DALBase.COL_NAME_CREATED_BY, value)
        End Set
    End Property


    Public Property CreatedDate As DateTime
        Get
            CheckDeleted()
            If Row(ClaimIssueDAL.COL_NAME_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimIssueDAL.COL_NAME_CREATED_DATE), DateTime)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimIssueDAL.COL_NAME_CREATED_DATE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Friend Sub ProcessWorkQueueItem(wqItem As WorkQueueItem)
        Dim workQueueId As Guid
        Dim dal As CompanyWorkQueueIssueDAL
        Dim wqi As WorkQueueItem

        If (IsDeleted) Then Exit Sub
        ' Check if Issue is still Open or Pending
        If (StatusCode = Codes.CLAIMISSUE_STATUS__OPEN OrElse StatusCode = Codes.CLAIMISSUE_STATUS__PENDING) Then
            ' Check if WorkQueueItem is already created
            If (WorkQueueItemCreatedId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N))) Then
                ' Get Work Queue ID based on Issue ID and Company ID
                dal = New CompanyWorkQueueIssueDAL()
                workQueueId = dal.GetWorkQueueIdByIssueCompany(Issue.Id, Claim.CompanyId)
                ' Check if Work Queue can be identified based on Issue ID and Company ID
                If (Not workQueueId.Equals(Guid.Empty)) Then
                    ' Create Work Queue Item
                    wqi = New WorkQueueItem()
                    With wqi.WorkQueueItem
                        .ClaimId = ClaimId
                        .ClaimIssueId = Id
                        .WorkQueueId = workQueueId
                        .ModifiedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                    End With
                    wqi.Save()

                    ' Update Data Row
                    WorkQueueItemCreatedId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
                End If
            End If
        Else
            If (Not wqItem Is Nothing) Then
                If (wqItem.WorkQueueItem.ClaimIssueId = ClaimIssueId) Then
                    wqItem.Process()
                End If
            End If
        End If
    End Sub

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimIssueDAL
                ProcessWorkQueueItem(Nothing)
                dal.UpdateFamily(Dataset, New PublishedTaskDAL.PublishTaskData(Of ClaimIssueDAL.PublishTaskClaimData) With
                                             {
                                                 .CompanyGroupId = Claim.Company.CompanyGroupId,
                                                 .CompanyId = Claim.Company.Id,
                                                 .CountryId = Claim.Company.CountryId,
                                                 .CoverageTypeId = Claim.CertificateItemCoverage.CoverageTypeId,
                                                 .DealerId = Claim.Dealer.Id,
                                                 .ProductCode = Claim.Certificate.ProductCode,
                                                 .Data = New ClaimIssueDAL.PublishTaskClaimData() With
                                                         {
                                                             .ClaimId = Claim.Id,
                                                             .EventTypeLookup = Function(publishTaskStatusId As Guid) As Guid
                                                                                    Dim publishedTaskStatusCode As String = LookupListNew.GetCodeFromId(Codes.CLAIM_ISSUE_STATUS, publishTaskStatusId)
                                                                                    Select Case publishedTaskStatusCode
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__WAIVED
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_WAIVED)
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__RESOLVED
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_RESOLVED)
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__REJECTED
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_REJECTED)
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__PENDING
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_PENDING)
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__OPEN
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_OPENED)
                                                                                        Case Codes.CLAIM_ISSUE_STATUS__CLOSED
                                                                                            Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_CLOSED)
                                                                                        Case Else
                                                                                            Throw New NotSupportedException()
                                                                                    End Select

                                                                                End Function
                                                             }
                                                 })
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property

    Public Sub SaveNewIssue(claimId As Guid, issueId As Guid, certId As Guid, IsCreatedBySytem As Boolean)

        Me.IssueId = issueId
        Me.ClaimId = claimId
        Entity = "CLAIM"

        Dim claimIssueStatus As ClaimIssueStatus = CType(ClaimIssueStatusList.GetNewChild, BusinessObjectsNew.ClaimIssueStatus)
        claimIssueStatus.ClaimIssueId = ClaimIssueId
        claimIssueStatus.ClaimIssueStatusCId = LookupListNew.GetIdFromCode("CLMISSUESTATUS", "OPEN")
        If (IsCreatedBySytem) Then
            CreatedBy = "SYSTEM"
            CreatedById = "SYSTEM"
        Else
            CreatedBy = ElitaPlusIdentity.Current.ActiveUser.UserName
            CreatedById = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        End If
        claimIssueStatus.ProcessedBy = CreatedBy
        claimIssueStatus.CreatedBy = CreatedBy

        CreatedDate = DateTime.Now
        claimIssueStatus.ProcessedDate = CreatedDate
        StatusCode = LookupListNew.GetCodeFromId("CLMISSUESTATUS", claimIssueStatus.ClaimIssueStatusCId)

        Dim issue As New Issue(Me.IssueId)
        For Each Item As IssueComment In issue.IssueNotesChildren
            If (Item.Code = "OPEN") Then
                claimIssueStatus.Comments = Item.Text
                Exit For
            End If
        Next
        claimIssueStatus.Save()

        Save()
    End Sub

    Public Function GetNewClaimIssueResponseChild() As ClaimIssueResponse
        Dim newClaimIssueResponse As ClaimIssueResponse = CType(ClaimIssueResponseList.GetNewChild, ClaimIssueResponse)
        newClaimIssueResponse.ClaimIssueId = Id
        Return newClaimIssueResponse
    End Function

    Public Function GetClaimIssueResponseChild(cirId As Guid) As ClaimIssueResponse
        Dim oClaimIssueResponse As ClaimIssueResponse = CType(ClaimIssueResponseList.GetChild(cirId), ClaimIssueResponse)
        Return oClaimIssueResponse
    End Function

    Public Function GetNewClaimIssueStatusChild() As ClaimIssueStatus
        Dim newClaimIssueStatus As ClaimIssueStatus = CType(ClaimIssueStatusList.GetNewChild, ClaimIssueStatus)
        newClaimIssueStatus.ClaimIssueId = Id
        Return newClaimIssueStatus
    End Function

    Public Function GetClaimIssueStatusChild(cisId As Guid) As ClaimIssueStatus
        Dim oClaimIssueStatus As ClaimIssueStatus = CType(ClaimIssueStatusList.GetChild(cisId), ClaimIssueStatus)
        Return oClaimIssueStatus
    End Function

    Public Shared Function ProcessFraudMonitoringIndicatorRule(claimId As Guid,certId As Guid, issueCode As String) As String
        Try
            Dim dal As New ClaimIssueDAL
            Return dal.ProcessFraudMonitoringIndicatorRule(claimId,certId,issueCode)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        End Try
    End Function
#End Region

#Region "Claim Issue List Selection View"
    Public Class ClaimIssueList
        Inherits BusinessObjectListBase

        Public Sub New(parent As ClaimBase)
            MyBase.New(LoadTable(parent), GetType(ClaimIssue), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, ClaimIssue).ClaimId.Equals(CType(Parent, ClaimBase).Id)
        End Function

        Private Shared Function LoadTable(parent As ClaimBase) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ClaimIssueList)) Then
                    Dim dal As New ClaimIssueDAL
                    dal.LoadList(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(ClaimIssueList))
                End If
                Return parent.Dataset.Tables(ClaimIssueDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

        Public Function Contains(issueCode As String) As Boolean
            Dim hasIssue As Boolean = False

            For Each Item As ClaimIssue In Me
                If (Item.IssueCode = issueCode) Then
                    hasIssue = True
                    Exit For
                End If
            Next

            Return hasIssue
        End Function

    End Class
#End Region

#Region "Consequential Damage Issue List Selection View"
    Public Class ConseqDamageIssueList
        Inherits BusinessObjectListBase

        Public Sub New(parent As CaseConseqDamage)
            MyBase.New(LoadTable(parent), GetType(ClaimIssue), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, ClaimIssue).ClaimId.Equals(CType(Parent, CaseConseqDamage).Id)
        End Function

        Private Shared Function LoadTable(parent As CaseConseqDamage) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ConseqDamageIssueList)) Then
                    Dim dal As New ClaimIssueDAL
                    dal.LoadList(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(ConseqDamageIssueList))
                End If
                Return parent.Dataset.Tables(ClaimIssueDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

        Public Function Contains(issueCode As String) As Boolean
            Dim hasIssue As Boolean = False

            For Each Item As ClaimIssue In Me
                If (Item.IssueCode = issueCode) Then
                    hasIssue = True
                    Exit For
                End If
            Next

            Return hasIssue
        End Function

    End Class
#End Region
End Class
