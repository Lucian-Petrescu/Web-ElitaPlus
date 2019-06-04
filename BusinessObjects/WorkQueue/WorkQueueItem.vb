Imports System.Collections.Generic
Imports System.ServiceModel

Public Class WorkQueueItem
    Inherits BusinessObjectBase
#Region "Enumerations"
    Public Enum ItemType
        None
        Issue
        Image
    End Enum
#End Region

#Region "Static Fields"
    Private Shared syncRoot As Object = New Object()
    Private Shared oWorkQueueServiceClient As WrkQueue.WorkQueueServiceClient
    Private Shared oDataTypes As Dictionary(Of String, Guid) = New Dictionary(Of String, Guid)
#End Region

#Region "Static Components"
    Private Shared ReadOnly Property WorkQueueClientProxy() As WrkQueue.WorkQueueServiceClient
        Get
            Dim wrkQueClient As WrkQueue.WorkQueueServiceClient
            If (oWorkQueueServiceClient Is Nothing OrElse oWorkQueueServiceClient.State <> ServiceModel.CommunicationState.Opened) Then
                SyncLock syncRoot
                    If (oWorkQueueServiceClient Is Nothing OrElse oWorkQueueServiceClient.State <> ServiceModel.CommunicationState.Opened) Then
                        oWorkQueueServiceClient = ServiceHelper.CreateWorkQueueServiceClient()
                    End If
                End SyncLock
            End If
            Return oWorkQueueServiceClient
        End Get
    End Property
#End Region

#Region "Static Methods"
    Friend Shared Function GetDataTypeId(ByVal dataTypeName As String) As Guid
        Dim workQueueDataItemTypes As WrkQueue.WorkQueueItemDataType()
        Dim workQueueDataItemType As WrkQueue.WorkQueueItemDataType
        If (Not oDataTypes.ContainsKey(dataTypeName)) Then
            SyncLock (syncRoot)
                If (Not oDataTypes.ContainsKey(dataTypeName)) Then
                    workQueueDataItemTypes = WorkQueue.GetWorkQueueItemDataTypes()
                    If (Not workQueueDataItemTypes Is Nothing) Then
                        For Each wqdi As WrkQueue.WorkQueueItemDataType In workQueueDataItemTypes
                            oDataTypes.Add(wqdi.Name, wqdi.Id)
                        Next
                    End If
                End If
            End SyncLock
        End If
        Return oDataTypes(dataTypeName)
    End Function

    Private Shared Function GetNextQueueItemForUser(ByVal networkId As String) As WrkQueue.WorkQueueItem
        Try
            Return WorkQueueClientProxy.GetNextWorkQueueItem(networkId)
        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
            Throw New UnauthorizedException("WorkQueue", "GetNextWorkQueueItem", ex)
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
            Throw New ServiceException("WorkQueue", "GetNextWorkQueueItem", ex)
        Catch ex As ServerTooBusyException
            Throw New ServiceException("WorkQueue", "GetNextWorkQueueItem", ex)
        End Try
    End Function

    Public Shared Function GetNextValidWorkQueueItem(ByVal networkId As String) As WorkQueueItem
        Dim flag As Boolean = False
        Dim wkqItem As WrkQueue.WorkQueueItem
        Dim workQueueItem As WorkQueueItem
        wkqItem = GetNextQueueItemForUser(networkId)

        If (Not wkqItem Is Nothing) Then
            workQueueItem = New WorkQueueItem(wkqItem)
            Select Case workQueueItem.WorkQueueItemType
                Case Assurant.ElitaPlus.BusinessObjectsNew.WorkQueueItem.ItemType.Issue
                    If Not IsWorkQueueItemAssociatedToPendingClaim(New WorkQueueItem(wkqItem)) Then
                        workQueueItem = GetNextValidWorkQueueItem(networkId)
                    Else
                        Return workQueueItem
                    End If
            End Select
        End If
        Return workQueueItem

    End Function

    Public Function Process()
        Try
            Me.WorkQueueItem.ModifiedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Me.WorkQueueItem.WorkQueueItemStatusReasonId = Me.WorkQueue.DefaultCompletedReason.Id
            WorkQueueClientProxy.ProcessWorkQueueItem(Me.WorkQueueItem, String.Empty, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
            Throw New UnauthorizedException("WorkQueue", "ProcessWorkQueueItem", ex)
        Catch ex As FaultException(Of WrkQueue.ItemNotLockedForEditFault)
            Throw New WorkQueueItemNotLockedByUserException(String.Empty, ex)
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
            Throw New ServiceException("WorkQueue", "ProcessWorkQueueItem", ex)
        Catch ex As ServerTooBusyException
            Throw New ServiceException("WorkQueue", "ProcessWorkQueueItem", ex)
        End Try

        Me.AddWorkQueueHistoryItem(Me.WorkQueue.DefaultCompletedReason.Reason, Codes.WQ_HISTORY_ACTION_PROCESS_CODE)
    End Function

    Public Function ReQueue(ByVal reasonId As Guid, ByVal reason As String)
        Try
            Me.WorkQueueItem.ModifiedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Me.WorkQueueItem.WorkQueueItemStatusReasonId = reasonId
            WorkQueueClientProxy.ProcessWorkQueueItem(Me.WorkQueueItem, String.Empty, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
            Throw New UnauthorizedException("WorkQueue", "ProcessWorkQueueItem", ex)
        Catch ex As FaultException(Of WrkQueue.MaxItemRequeueCountExceededFault)
            Throw New WorkQueueItemMaxRequeueLimitExceededException(String.Empty, ex)
        Catch ex As FaultException(Of WrkQueue.ItemNotLockedForEditFault)
            Throw New WorkQueueItemNotLockedByUserException(String.Empty, ex)
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
            Throw New ServiceException("WorkQueue", "ProcessWorkQueueItem", ex)
        Catch ex As ServerTooBusyException
            Throw New ServiceException("WorkQueue", "ProcessWorkQueueItem", ex)
        End Try

        Me.AddWorkQueueHistoryItem(reason, Codes.WQ_HISTORY_ACTION_REQUEUE_CODE)
    End Function

    Public Function ReDirect(ByVal queueName As String, ByVal reasonId As Guid, ByVal reason As String)
        Try
            Me.WorkQueueItem.ModifiedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Me.WorkQueueItem.WorkQueueItemStatusReasonId = reasonId
            WorkQueueClientProxy.ProcessWorkQueueItem(Me.WorkQueueItem, queueName, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
            Throw New UnauthorizedException("WorkQueue", "ProcessWorkQueueItem", ex)
        Catch ex As FaultException(Of WrkQueue.ItemNotLockedForEditFault)
            Throw New WorkQueueItemNotLockedByUserException(String.Empty, ex)
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
            Throw New ServiceException("WorkQueue", "ProcessWorkQueueItem", ex)
        Catch ex As ServerTooBusyException
            Throw New ServiceException("WorkQueue", "ProcessWorkQueueItem", ex)
        End Try

        Me.AddWorkQueueHistoryItem(reason, Codes.WQ_HISTORY_ACTION_REDIRECT_CODE)
    End Function

    Private Function AddWorkQueueHistoryItem(ByVal reason As String, ByVal action As String)

        Dim itemDesc As String
        Select Case Me.WorkQueueItemType
            Case ItemType.Issue
                Dim claimNumber As String = ClaimFacade.Instance.GetClaim(Of Claim)(Me.WorkQueueItem.ClaimId).ClaimNumber
                Dim claimIssueDesc As String
                claimIssueDesc = If(Me.WorkQueueItem.ClaimIssueId <> Nothing, New ClaimIssue(Me.WorkQueueItem.ClaimIssueId).IssueDescription, String.Empty)
                itemDesc = String.Format("{0}-{1}", claimNumber, claimIssueDesc)
            Case ItemType.Image
                itemDesc = Me.WorkQueueItem.ImageId.ToString
        End Select

        WorkqueueHistory.AddItem(Me.WorkQueueItem.Id, Me.WorkQueueItem.WorkQueueId, reason, action, itemDesc)
    End Function


#End Region

#Region "Constructor"
    Public Sub New()
        Me._isNew = True
        Me._isDeleted = False
        Me._workQueueItem = New WrkQueue.WorkQueueItem()
    End Sub

    Private Sub New(ByVal pWorkQueueItem As WrkQueue.WorkQueueItem)
        Me._workQueueItem = pWorkQueueItem
        Me._isNew = False
        Me._isDeleted = False
    End Sub
#End Region

#Region "constants"
    Public Const IMAGE_NAME As String = "ImageName"
#End Region

#Region "Instance Fields"
    Private _isNew As Boolean = False
    Private _isDeleted As Boolean = False
    Private _metadata As Dictionary(Of String, String)
    Private _workQueueItem As WrkQueue.WorkQueueItem
    Private _workQueue As WorkQueue
    Private _syncRoot As Object = New Object()
#End Region

#Region "Instance Properties"
    Public ReadOnly Property WorkQueueItem As WrkQueue.WorkQueueItem
        Get
            Return Me._workQueueItem
        End Get
    End Property

    Public ReadOnly Property IsDeleted As Boolean
        Get
            Return _isDeleted
        End Get
    End Property

    Public ReadOnly Property IsNew As Boolean
        Get
            Return _isNew
        End Get
    End Property

    Public ReadOnly Property MetaData As Dictionary(Of String, String)
        Get
            If _metadata Is Nothing Then
                _metadata = New Dictionary(Of String, String)
            End If
            Return _metadata
        End Get
    End Property

    Public Property Id As Guid
        Get
            Return Me._workQueueItem.Id
        End Get
        Set(ByVal value As Guid)
            Me._workQueueItem.Id = value
        End Set
    End Property

    Public ReadOnly Property WorkQueue As WorkQueue
        Get
            If (_workQueue Is Nothing) Then
                SyncLock Me._syncRoot
                    If (_workQueue Is Nothing) Then
                        _workQueue = New WorkQueue(Me.WorkQueueItem.WorkQueueId)
                    End If
                End SyncLock
            End If
            Return _workQueue
        End Get
    End Property

    Public ReadOnly Property WorkQueueItemType() As ItemType
        Get
            Dim itemType As ItemType

            Select Case Me.WorkQueue.WorkQueue.ActionCode
                Case Codes.WORK_QUEUE_ACTION__WORK_ON_CLAIM
                    itemType = itemType.Issue
                Case Codes.WORK_QUEUE_ACTION__INDEX_IMAGE
                    itemType = itemType.Image
                Case Else
                    itemType = itemType.None
            End Select
            Return itemType
        End Get
    End Property

    Public ReadOnly Property StartDate As Nullable(Of Date)
        Get
            Return Me.WorkQueue.ConvertTimeFromUtc(Me.WorkQueueItem.StartDate)
        End Get
    End Property

    Public ReadOnly Property ImageScanDate As Nullable(Of Date)
        Get
            Dim d As Nullable(Of Date) = Nothing
            If (Not String.IsNullOrEmpty(Me.WorkQueueItem.ImageScanDate)) Then
                d = Convert.ToDateTime(Me.WorkQueueItem.ImageScanDate)
            End If

            Return Me.WorkQueue.ConvertTimeFromUtc(d)
        End Get
    End Property
#End Region

#Region "Instance Methods"
    Private Shared Function IsWorkQueueItemAssociatedToPendingClaim(ByVal wkQItem As WorkQueueItem) As Boolean
        Dim flag As Boolean = False

        If (wkQItem.WorkQueueItem.ClaimId = Nothing) Then
            Throw New BOValidationException("WorkQueueItem not associated to Claim")
        End If

        Dim claim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(wkQItem.WorkQueueItem.ClaimId)
        Dim claimIssue As ClaimIssue
        If (claim.StatusCode = Codes.CLAIM_STATUS__PENDING) Then
            If (Not wkQItem.WorkQueueItem.ClaimIssueId = Nothing) Then
                claimIssue = New ClaimIssue(wkQItem.WorkQueueItem.ClaimIssueId)
                If (claimIssue.StatusCode = Codes.CLAIMISSUE_STATUS__OPEN Or claimIssue.StatusCode = Codes.CLAIMISSUE_STATUS__PENDING _
                        Or claimIssue.StatusCode = Codes.CLAIMISSUE_STATUS__REOPEN) Then
                    flag = True
                End If
            Else
                flag = True
            End If
        End If
        If (flag = False) Then
            'Process Queue Item to Completed
            wkQItem.Process()
        End If

        Return flag
    End Function

    Public Sub Save()
        Try
            WorkQueueClientProxy.AddWorkQueueItem(Me.WorkQueueItem, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
            Throw New UnauthorizedException("WorkQueue", "AddWorkQueueItem", ex)
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
            Throw New ServiceException("WorkQueue", "AddWorkQueueItem", ex)
        End Try
    End Sub





#End Region

End Class

Namespace WrkQueue
    Partial Public Class WorkQueueItem

#Region "Shared Members"



#End Region

#Region "Public Properties"


        Public Property ClaimId As Guid
            Get
                Return MetaDataAsGuid(ServiceHelper.WQI_DT_CLAIM_ID)
            End Get
            Set(ByVal value As Guid)
                Me(ServiceHelper.WQI_DT_CLAIM_ID) = value.ToString()
            End Set
        End Property

        Public Property ClaimIssueId As Guid
            Get
                Return MetaDataAsGuid(ServiceHelper.WQI_DT_CLAIM_ISSUE_ID)
            End Get
            Set(ByVal value As Guid)
                Me(ServiceHelper.WQI_DT_CLAIM_ISSUE_ID) = value.ToString()
            End Set
        End Property

        Public ReadOnly Property ImageId As Guid
            Get
                Return MetaDataAsGuid(ServiceHelper.WQI_DT_IMAGE_ID)
            End Get
        End Property

        Public ReadOnly Property ImageScanDate As String
            Get
                Return Me(ServiceHelper.WQI_DT_SCAN_DATE)
            End Get
        End Property
#End Region

#Region "Private Methods"
        Default Friend Property Metadata(ByVal metadataName As String) As String
            Get
                Dim oWqid As WorkQueueItemData
                oWqid = (From wqid In Me.WorkQueueItemDataList Where wqid.WorkQueueDataTypeId = Assurant.ElitaPlus.BusinessObjectsNew.WorkQueueItem.GetDataTypeId(metadataName) Select wqid).FirstOrDefault()
                If (oWqid Is Nothing) Then
                    Return Nothing
                Else
                    Return oWqid.Value
                End If
            End Get
            Set(ByVal value As String)
                Dim oWorkQueueItemData As WorkQueueItemData = Nothing
                Dim length As Integer
                If (Me.WorkQueueItemDataList Is Nothing) Then
                    length = 0
                Else
                    oWorkQueueItemData = (From wqid In Me.WorkQueueItemDataList Where wqid.WorkQueueDataTypeId = Assurant.ElitaPlus.BusinessObjectsNew.WorkQueueItem.GetDataTypeId(metadataName) Select wqid).FirstOrDefault()
                    length = Me.WorkQueueItemDataList.Length
                End If
                If (oWorkQueueItemData Is Nothing) Then
                    oWorkQueueItemData = New WorkQueueItemData()
                    oWorkQueueItemData.WorkQueueDataTypeId = Assurant.ElitaPlus.BusinessObjectsNew.WorkQueueItem.GetDataTypeId(metadataName)
                    oWorkQueueItemData.CreatedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                    ReDim Preserve Me.WorkQueueItemDataList(length)
                    Me.WorkQueueItemDataList(Me.WorkQueueItemDataList.Length - 1) = oWorkQueueItemData
                Else
                    oWorkQueueItemData.UpdatedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                End If
                oWorkQueueItemData.Value = value
            End Set
        End Property

        Private Function MetaDataAsGuid(ByVal metadataName As String) As Guid
            Dim id As String = Me(metadataName)
            Return If(id Is Nothing, Nothing, New Guid(id))
        End Function
#End Region



    End Class
End Namespace
