Imports System.Collections.Generic

Public Class ClaimIssue
    Inherits EntityIssue

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(id, familyDS)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(familyDS)
    End Sub

    Public Sub New(ByVal row As DataRow)
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
    Public ReadOnly Property ClaimIssueId() As Guid
        Get
            Return Me.Id
        End Get
    End Property

    Public Property ClaimId() As Guid
        Get
            Return Me.EntityId
        End Get
        Set(ByVal Value As Guid)
            Me.EntityId = Value
        End Set
    End Property

    Private _claim As ClaimBase

    Public Property Claim As ClaimBase
        Get
            If (_claim Is Nothing) Then
                If Not Me.ClaimId.Equals(Guid.Empty) AndAlso Me.Entity = "AUTH" Then
                    Dim claimAuth As New ClaimAuthorization(Me.ClaimId)
                    Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(claimAuth.ClaimId, Me.Dataset)
                ElseIf Not Me.ClaimId.Equals(Guid.Empty) AndAlso Me.Entity = "CONSEQUENTIALDAMAGE" Then
                    Dim ClaimConseqDamage As New CaseConseqDamage(Me.ClaimId) ' Here Me.ClaimId will be case_conseq_damage_id
                    Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ClaimConseqDamage.ClaimId, Me.Dataset)
                ElseIf Not Me.ClaimId.Equals(Guid.Empty) Then
                    Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.ClaimId, Me.Dataset)
                End If
            End If
            Return _claim
        End Get
        Private Set(ByVal value As ClaimBase)
            _claim = value
        End Set
    End Property

    Public ReadOnly Property Issue() As Issue
        Get
            Dim oIssue As New Issue(Me.IssueId)
            Return oIssue
        End Get
    End Property

    Public ReadOnly Property IssueCode() As String
        Get
            Return Issue.Code
        End Get
    End Property

    Public ReadOnly Property IssueDescription() As String
        Get
            Return Issue.Description
        End Get
    End Property

    Public ReadOnly Property ClaimIssueQuestionList() As IssueQuestionsChildrenList
        Get
            Dim claimIssue As New Issue(Me.IssueId)
            Return claimIssue.IssueQuestionsChildren
        End Get
    End Property

    Public ReadOnly Property ClaimIssueQuestionListByDealer(dealerId As Guid) As IssueQuestionsChildrenList
        Get
            Dim claimIssue As New Issue(Me.IssueId)
            Return claimIssue.IssueQuestionsChildrenByIssueDealer(Me.IssueId, dealerId)
        End Get
    End Property

    Public ReadOnly Property ClaimIssueResponseList() As ClaimIssueResponse.ClaimIssueResponseList
        Get
            Return New ClaimIssueResponse.ClaimIssueResponseList(Me)
        End Get
    End Property

    Public ReadOnly Property ClaimIssueStatusList() As ClaimIssueStatus.ClaimIssueStatusList
        Get
            Return New ClaimIssueStatus.ClaimIssueStatusList(Me)
        End Get
    End Property

    Public Property ProcessedDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimIssue.COL_NAME_PROCESSED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimIssue.COL_NAME_PROCESSED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimIssue.COL_NAME_PROCESSED_DATE, Value)
        End Set
    End Property

    Public Property ProcessedBy() As String
        Get
            CheckDeleted()
            If Row(ClaimIssue.COL_NAME_PROCESSED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimIssue.COL_NAME_PROCESSED_BY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimIssue.COL_NAME_PROCESSED_BY, Value)
        End Set
    End Property

    Public ReadOnly Property StatusId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimIssue.COL_NAME_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimIssue.COL_NAME_STATUS_ID), Byte()))

            End If
        End Get
    End Property

    Public Property StatusCode() As String
        Get
            CheckDeleted()
            If Row(ClaimIssue.COL_NAME_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimIssue.COL_NAME_STATUS_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimIssue.COL_NAME_STATUS_CODE, Value)
        End Set
    End Property

    Public Property CreatedBy() As String
        Get
            CheckDeleted()
            If Row(ClaimIssue.COL_NAME_CREATED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimIssue.COL_NAME_CREATED_BY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimIssue.COL_NAME_CREATED_BY, Value)
        End Set
    End Property

    Public Property CreatedById() As String
        Get
            CheckDeleted()
            If Row(DALBase.COL_NAME_CREATED_BY) Is DBNull.Value Then Return Nothing
            Return CType(Row(DALBase.COL_NAME_CREATED_BY), String)
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            Me.SetValue(DALBase.COL_NAME_CREATED_BY, value)
        End Set
    End Property


    Public Property CreatedDate() As DateTime
        Get
            CheckDeleted()
            If Row(ClaimIssueDAL.COL_NAME_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimIssueDAL.COL_NAME_CREATED_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateTime)
            CheckDeleted()
            Me.SetValue(ClaimIssueDAL.COL_NAME_CREATED_DATE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Friend Sub ProcessWorkQueueItem(ByVal wqItem As WorkQueueItem)
        Dim workQueueId As Guid
        Dim dal As CompanyWorkQueueIssueDAL
        Dim wqi As WorkQueueItem

        If (Me.IsDeleted) Then Exit Sub
        ' Check if Issue is still Open or Pending
        If (StatusCode = Codes.CLAIMISSUE_STATUS__OPEN OrElse StatusCode = Codes.CLAIMISSUE_STATUS__PENDING) Then
            ' Check if WorkQueueItem is already created
            If (Me.WorkQueueItemCreatedId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N))) Then
                ' Get Work Queue ID based on Issue ID and Company ID
                dal = New CompanyWorkQueueIssueDAL()
                workQueueId = dal.GetWorkQueueIdByIssueCompany(Me.Issue.Id, Me.Claim.CompanyId)
                ' Check if Work Queue can be identified based on Issue ID and Company ID
                If (Not workQueueId.Equals(Guid.Empty)) Then
                    ' Create Work Queue Item
                    wqi = New WorkQueueItem()
                    With wqi.WorkQueueItem
                        .ClaimId = Me.ClaimId
                        .ClaimIssueId = Me.Id
                        .WorkQueueId = workQueueId
                        .ModifiedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                    End With
                    wqi.Save()

                    ' Update Data Row
                    Me.WorkQueueItemCreatedId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
                End If
            End If
        Else
            If (Not wqItem Is Nothing) Then
                If (wqItem.WorkQueueItem.ClaimIssueId = Me.ClaimIssueId) Then
                    wqItem.Process()
                End If
            End If
        End If
    End Sub

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimIssueDAL
                Me.ProcessWorkQueueItem(Nothing)
                dal.UpdateFamily(Me.Dataset, New PublishedTaskDAL.PublishTaskData(Of ClaimIssueDAL.PublishTaskClaimData) With
                                             {
                                                 .CompanyGroupId = Me.Claim.Company.CompanyGroupId,
                                                 .CompanyId = Me.Claim.Company.Id,
                                                 .CountryId = Me.Claim.Company.CountryId,
                                                 .CoverageTypeId = Me.Claim.CertificateItemCoverage.CoverageTypeId,
                                                 .DealerId = Me.Claim.Dealer.Id,
                                                 .ProductCode = Me.Claim.Certificate.ProductCode,
                                                 .Data = New ClaimIssueDAL.PublishTaskClaimData() With
                                                         {
                                                             .ClaimId = Me.Claim.Id,
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
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty
        End Get
    End Property

    Public Sub SaveNewIssue(ByVal claimId As Guid, ByVal issueId As Guid, ByVal certId As Guid, ByVal IsCreatedBySytem As Boolean)

        Me.IssueId = issueId
        Me.ClaimId = claimId
        Me.Entity = "CLAIM"

        Dim claimIssueStatus As ClaimIssueStatus = CType(Me.ClaimIssueStatusList.GetNewChild, BusinessObjectsNew.ClaimIssueStatus)
        claimIssueStatus.ClaimIssueId = Me.ClaimIssueId
        claimIssueStatus.ClaimIssueStatusCId = LookupListNew.GetIdFromCode("CLMISSUESTATUS", "OPEN")
        If (IsCreatedBySytem) Then
            Me.CreatedBy = "SYSTEM"
            Me.CreatedById = "SYSTEM"
        Else
            Me.CreatedBy = ElitaPlusIdentity.Current.ActiveUser.UserName
            Me.CreatedById = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        End If
        claimIssueStatus.ProcessedBy = Me.CreatedBy
        claimIssueStatus.CreatedBy = Me.CreatedBy

        Me.CreatedDate = DateTime.Now
        claimIssueStatus.ProcessedDate = Me.CreatedDate
        Me.StatusCode = LookupListNew.GetCodeFromId("CLMISSUESTATUS", claimIssueStatus.ClaimIssueStatusCId)

        Dim issue As New Issue(Me.IssueId)
        For Each Item As IssueComment In issue.IssueNotesChildren
            If (Item.Code = "OPEN") Then
                claimIssueStatus.Comments = Item.Text
                Exit For
            End If
        Next
        claimIssueStatus.Save()

        Me.Save()
    End Sub

    Public Function GetNewClaimIssueResponseChild() As ClaimIssueResponse
        Dim newClaimIssueResponse As ClaimIssueResponse = CType(Me.ClaimIssueResponseList.GetNewChild, ClaimIssueResponse)
        newClaimIssueResponse.ClaimIssueId = Me.Id
        Return newClaimIssueResponse
    End Function

    Public Function GetClaimIssueResponseChild(ByVal cirId As Guid) As ClaimIssueResponse
        Dim oClaimIssueResponse As ClaimIssueResponse = CType(Me.ClaimIssueResponseList.GetChild(cirId), ClaimIssueResponse)
        Return oClaimIssueResponse
    End Function

    Public Function GetNewClaimIssueStatusChild() As ClaimIssueStatus
        Dim newClaimIssueStatus As ClaimIssueStatus = CType(Me.ClaimIssueStatusList.GetNewChild, ClaimIssueStatus)
        newClaimIssueStatus.ClaimIssueId = Me.Id
        Return newClaimIssueStatus
    End Function

    Public Function GetClaimIssueStatusChild(ByVal cisId As Guid) As ClaimIssueStatus
        Dim oClaimIssueStatus As ClaimIssueStatus = CType(Me.ClaimIssueStatusList.GetChild(cisId), ClaimIssueStatus)
        Return oClaimIssueStatus
    End Function

    Public Shared Function ProcessFraudMonitoringIndicatorRule(ByVal claimId As Guid,ByVal certId As Guid, ByVal issueCode As String) As String
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

        Public Sub New(ByVal parent As ClaimBase)
            MyBase.New(LoadTable(parent), GetType(ClaimIssue), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, ClaimIssue).ClaimId.Equals(CType(Parent, ClaimBase).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As ClaimBase) As DataTable
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

        Public Function Contains(ByVal issueCode As String) As Boolean
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

        Public Sub New(ByVal parent As CaseConseqDamage)
            MyBase.New(LoadTable(parent), GetType(ClaimIssue), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, ClaimIssue).ClaimId.Equals(CType(Parent, CaseConseqDamage).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As CaseConseqDamage) As DataTable
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

        Public Function Contains(ByVal issueCode As String) As Boolean
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
