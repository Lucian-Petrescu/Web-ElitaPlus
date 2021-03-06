﻿Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.ServiceModel

Partial Public Class WorkQueue
    Inherits BusinessObjectBase
    Implements IEffecttiveExpiration

    Public Shared ReadOnly DEFAULT_EXPIRATION_DATE As DateTime = New DateTime(2499, 12, 31, 23, 59, 59)

#Region "Static Fields"
    Private Shared ReadOnly _yesId As Guid
    Private Shared ReadOnly _NoId As Guid

    Private Shared syncRoot As Object = New Object()
    Private Shared oWorkQueueServiceClient As WrkQueue.WorkQueueServiceClient
    Private Shared oAuthorizationServiceClient As Auth.AuthorizationClient
#End Region

#Region "Static Constructor"
    Shared Sub New()
        _yesId = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_Y)
        _NoId = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, Codes.YESNO_N)
    End Sub
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

    Private Shared ReadOnly Property AuthorizationClientProxy() As Auth.AuthorizationClient
        Get
            Dim authClient As Auth.AuthorizationClient
            If (oAuthorizationServiceClient Is Nothing OrElse oAuthorizationServiceClient.State <> ServiceModel.CommunicationState.Opened) Then
                SyncLock syncRoot
                    If (oAuthorizationServiceClient Is Nothing OrElse oAuthorizationServiceClient.State <> ServiceModel.CommunicationState.Opened) Then
                        oAuthorizationServiceClient = ServiceHelper.CreateAuthorizationClient()
                    End If
                End SyncLock
            End If
            Return oAuthorizationServiceClient
        End Get
    End Property
#End Region

#Region "Static Methods"
    Public Shared Function GetWorkQueueTypes() As WrkQueue.WorkQueueType()
        Dim returnValue As WrkQueue.WorkQueueType()
        Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        Try
            returnValue = WorkQueueClientProxy.GetWorkQueueTypes(userName)
        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
            Throw New UnauthorizedException("WorkQueue", "GetWorkQueueTypes", ex)
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
            Throw New ServiceException("WorkQueue", "GetWorkQueueTypes", ex)
        End Try
        Return returnValue
    End Function

    Public Shared Function GetWorkQueueItemDataTypes() As WrkQueue.WorkQueueItemDataType()
        Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        Dim returnValue As WrkQueue.WorkQueueItemDataType()
        Try
            returnValue = WorkQueueClientProxy.GetWorkQueueItemDataTypes(userName)
        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
            Throw New UnauthorizedException("WorkQueue", "GetWorkQueueStats", ex)
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
            Throw New ServiceException("WorkQueue", "GetWorkQueueItemDataTypes", ex)
        End Try
        Return returnValue
    End Function

    Public Shared Function GetStatList(ByVal workQueueName As String, ByVal companyCode As String, ByVal actionCode As String, ByVal activeOn As Nullable(Of Date)) As WrkQueue.WorkQueueStats()
        Dim returnValue As WrkQueue.WorkQueueStats()
        Dim oUser As User = ElitaPlusIdentity.Current.ActiveUser
        Dim userName As String = oUser.NetworkId
        Dim workQueues As WrkQueue.WorkQueue()
        Dim workQueueNames As String()
        workQueues = GetList(workQueueName, companyCode, actionCode, activeOn, True)
        If (workQueues.Length = 0) Then
            Dim emptyResult(-1) As WrkQueue.WorkQueueStats
            Return emptyResult
        End If
        workQueueNames = (From wq In workQueues Select wq.Name).ToArray()
        Try
            returnValue = WorkQueueClientProxy.GetWorkQueueStats(ServiceHelper.PA_WQ_PROCESS, userName, workQueueNames)
        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
            Throw New UnauthorizedException("WorkQueue", "GetWorkQueueStats", ex)
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
            Throw New ServiceException("WorkQueue", "GetWorkQueueStats", ex)
        End Try
        Return returnValue
    End Function

    Public Shared Function GetList(ByVal workQueueName As String, ByVal companyCode As String, ByVal actionCode As String, ByVal activeOn As Nullable(Of Date), ByVal requireAdminRole As Boolean) As WrkQueue.WorkQueue()
        Try
            Dim oUser As User = ElitaPlusIdentity.Current.ActiveUser
            Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim resultList As WrkQueue.WorkQueue()
            workQueueName = "^" + Regex.Escape(workQueueName).Replace("\*", ".*").Replace("\?", ".") + "$"
            Dim nameSearch As New Regex(workQueueName, RegexOptions.Singleline Or RegexOptions.IgnoreCase)
            Try
                resultList = WorkQueueClientProxy.GetWorkQueues(userName)
            Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
                Throw New UnauthorizedException("WorkQueue", "GetWorkQueues", ex)
            Catch ex As FaultException(Of WrkQueue.ValidationFault)
                Throw ex.AsBOValidationException()
            Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
                Throw New ServiceException("WorkQueue", "GetWorkQueues", ex)
            End Try

            resultList = (From wq In resultList _
                         Where nameSearch.IsMatch(wq.Name) AndAlso _
                         (Not activeOn.HasValue OrElse (activeOn.Value >= wq.ActiveOn And activeOn.Value <= (wq.InActiveOn.GetValueOrDefault(DateTime.MaxValue)))) AndAlso _
                         (companyCode Is Nothing OrElse companyCode.Length = 0 OrElse wq.CompanyCode = companyCode) AndAlso _
                         (actionCode Is Nothing OrElse actionCode.Length = 0 OrElse wq.ActionCode = actionCode) AndAlso _
                         (Not requireAdminRole OrElse oUser.isInRole(wq.AdminRole)) _
                         Select wq).ToArray()
            Return resultList
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    ''' <summary>
    ''' Retrieves all Work Queues for logged user
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetList() As WrkQueue.WorkQueue()
        Try
        Dim oUser As User = ElitaPlusIdentity.Current.ActiveUser
        Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        Dim resultList As WrkQueue.WorkQueue()
            resultList = WorkQueueClientProxy.GetWorkQueues(userName)
            Return resultList
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Permissions"
    Public Shared Function GrantProcessWQPermission(ByVal workQueueName As String, ByVal NetworkId As String) As Boolean
        Try
            Dim permission As Auth.Permission
            Dim permissions As Auth.Permission()

            Dim uInfo As New Auth.User, retUserInfoobj() As Auth.User
            uInfo.UserId = NetworkId

            Try
                retUserInfoobj = AuthorizationClientProxy.FindUsers(uInfo)
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "FindUsers", ex)
            End Try

            If retUserInfoobj.Length = 1 Then
                permission = New Auth.Permission
                permission.ResourceType = ServiceHelper.RESTYP_WORKQUEUE
                permission.Resource = workQueueName
                permission.Action = ServiceHelper.PA_WQ_PROCESS

                Try
                    permissions = AuthorizationClientProxy.FindPermissions(ServiceHelper.WORKQUEUE_SERVICE_NAME, permission)
                Catch ex As FaultException(Of Auth.ValidationFault)
                    Throw ex.AsBOValidationException()
                Catch ex As FaultException(Of Auth.AuthorizationFault)
                    Throw New ServiceException("Authorization", "FindPermissions", ex)
                End Try

                If (permissions.Length = 1) Then
                    Dim PermissinIds(permissions.Length - 1) As Guid
                    PermissinIds(0) = permissions(0).Id
                    Try
                        AuthorizationClientProxy.AddPermissionsToUser(PermissinIds, retUserInfoobj(0).Id)
                    Catch ex As FaultException(Of Auth.ValidationFault)
                        Throw ex.AsBOValidationException()
                    Catch ex As FaultException(Of Auth.AuthorizationFault)
                        Throw New ServiceException("Authorization", "AddPermissionsToUser", ex)
                    End Try
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr)
        End Try
    End Function


    Public Shared Function RevokeProcessWQPermission(ByVal workQueueName As String, ByVal NetworkId As String) As Boolean
        Try
            Dim permission As Auth.Permission
            Dim permissions As Auth.Permission()

            Dim uInfo As New Auth.User, retUserInfoobj() As Auth.User
            uInfo.UserId = NetworkId

            Try
                retUserInfoobj = AuthorizationClientProxy.FindUsers(uInfo)
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "FindUsers", ex)
            End Try

            If retUserInfoobj.Length = 1 Then
                permission = New Auth.Permission
                permission.ResourceType = ServiceHelper.RESTYP_WORKQUEUE
                permission.Resource = workQueueName
                permission.Action = ServiceHelper.PA_WQ_PROCESS

                Try
                    permissions = AuthorizationClientProxy.FindPermissions(ServiceHelper.WORKQUEUE_SERVICE_NAME, permission)
                Catch ex As FaultException(Of Auth.ValidationFault)
                    Throw ex.AsBOValidationException()
                Catch ex As FaultException(Of Auth.AuthorizationFault)
                    Throw New ServiceException("Authorization", "FindPermissions", ex)
                End Try

                If (permissions.Length = 1) Then
                    Dim PermissinIds(permissions.Length - 1) As Guid
                    PermissinIds(0) = permissions(0).Id
                    Try
                        AuthorizationClientProxy.RemovePermissionsFromUser(PermissinIds, retUserInfoobj(0).Id)
                    Catch ex As FaultException(Of Auth.ValidationFault)
                        Throw ex.AsBOValidationException()
                    Catch ex As FaultException(Of Auth.AuthorizationFault)
                        Throw New ServiceException("Authorization", "RemovePermissionsFromUser", ex)
                    End Try
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.BusinessErr)
        End Try
    End Function
#End Region

#Region "Instance Fields"
    Private _isNew As Boolean = False
    Private _timeZoneInfo As TimeZoneInfo
    Private _workQueue As WrkQueue.WorkQueue
    Private _workQueueId As Guid = Guid.NewGuid()

    Private _statusReasons As WorkQueueItemStatusReason()
#End Region

#Region "Constructors"
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        BuildWorkQueue(id)
    End Sub

    Private Sub BuildWorkQueue(ByVal id As Guid)
        Me.Dataset = New DataSet
        CreateEmptyTable()
        _isNew = False
        Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        Try
            Me._workQueue = WorkQueueClientProxy.GetWorkQueueById(id, userName)
        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
            Throw New UnauthorizedException("WorkQueue", "GetWorkQueueById", ex)
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
            Throw New ServiceException("WorkQueue", "GetWorkQueueById", ex)
        End Try
        ' Get Work Queue Status Reasons
        Dim wqisrs As WrkQueue.WorkQueueItemStatusReason()
        Try
            wqisrs = WorkQueueClientProxy.GetWorkQueueItemStatusReasons(Me._workQueue.Id, WrkQueue.StatusReasonOwnerType.Queue, userName)
        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
            Throw New UnauthorizedException("WorkQueue", "GetWorkQueueItemStatusReasons", ex)
        Catch ex As FaultException(Of WrkQueue.ValidationFault)
            Throw ex.AsBOValidationException()
        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
            Throw New ServiceException("WorkQueue", "GetWorkQueueItemStatusReasons", ex)
        End Try
        If (wqisrs Is Nothing) Then
            ReDim Preserve Me._statusReasons(-1)
        Else
            ReDim Preserve Me._statusReasons(wqisrs.Length - 1)
            For i As Integer = 0 To (wqisrs.Length - 1)
                Me._statusReasons(i) = New WorkQueueItemStatusReason(Me, wqisrs(i))
            Next
        End If
    End Sub

    Private Sub CreateEmptyTable()
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.TableName = "ELP_WORKQUEUE"
        dt.Columns.Add("ID", GetType(Byte()))
        dr = dt.NewRow()
        dr("ID") = Guid.NewGuid().ToByteArray()
        Me.Dataset.Tables.Add(dt)
        Me.Row = dr
    End Sub

    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        CreateEmptyTable()
        _isNew = True
        Me._workQueue = New WrkQueue.WorkQueue()
        _workQueue.ActiveOn = DateTime.UtcNow
        _workQueue.InActiveOn = DEFAULT_EXPIRATION_DATE
        _workQueue.StartItemDelayMinutes = 0
        _workQueue.TimeToCompleteMinutes = 15
        _workQueue.LockableDataTypeId = Guid.Empty
        _workQueue.TimeZoneCode = String.Empty
        'DEF-3035
        _workQueue.RequeueItemDelayMinutes = 0
        'DEF-3035 End
    End Sub
#End Region

#Region "Instance Properties"
    Public Property Effective As DateTimeType Implements IEffecttiveExpiration.Effective
        Get
            Return New DateTimeType(Me.WorkQueue.Effective)
        End Get
        Set(ByVal value As DateTimeType)
            If (Not value Is Nothing) Then
                Me.WorkQueue.ActiveOn = value.Value.AddMilliseconds(-1 * value.Value.Millisecond)
            End If
        End Set
    End Property

    Public Property Expiration As DateTimeType Implements IEffecttiveExpiration.Expiration
        Get
            If (Me.WorkQueue.Expiration.HasValue) Then
                Return New DateTimeType(Me.WorkQueue.Expiration.Value)
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As DateTimeType)
            If (value Is Nothing) Then
                Me.WorkQueue.InActiveOn = Nothing
            Else
                Me.WorkQueue.InActiveOn = value.Value.AddMilliseconds(-1 * value.Value.Millisecond)
            End If
        End Set
    End Property

    Public Property StatusReasons As WorkQueueItemStatusReason()
        Get
            Return Me._statusReasons
        End Get
        Private Set(ByVal value As WorkQueueItemStatusReason())
            Me._statusReasons = value
        End Set
    End Property

    Public ReadOnly Property ReDirectReasons As WorkQueueItemStatusReason()
        Get
            Dim returnValue(-1) As WorkQueueItemStatusReason
            If (Me.StatusReasons Is Nothing) Then Return returnValue
            Return (From wqisr In Me.StatusReasons Where wqisr.ItemStatusReason.Status = WrkQueue.StatusType.Completed And wqisr.ItemStatusReason.IsActive = True And wqisr.ItemStatusReason.Reason <> ServiceHelper.WQISR_DEFAULT_COMPLETED Select wqisr).ToArray()
        End Get
    End Property

    Public ReadOnly Property DefaultCompletedReason As WorkQueueItemStatusReason
        Get
            Return (From wqisr In Me.StatusReasons Where wqisr.ItemStatusReason.Status = WrkQueue.StatusType.Completed And wqisr.ItemStatusReason.IsActive = True And wqisr.ItemStatusReason.Reason = ServiceHelper.WQISR_DEFAULT_COMPLETED Select wqisr).FirstOrDefault()
        End Get
    End Property

    Public ReadOnly Property ReQueueReasons As WorkQueueItemStatusReason()
        Get
            Dim returnValue(-1) As WorkQueueItemStatusReason
            If (Me.StatusReasons Is Nothing) Then Return returnValue
            Return (From wqisr In Me.StatusReasons Where wqisr.ItemStatusReason.Status = WrkQueue.StatusType.Requeue And wqisr.ItemStatusReason.IsActive = True And wqisr.ItemStatusReason.Reason <> ServiceHelper.WQISR_DEFAULT_REQUEUE Select wqisr).ToArray()
        End Get
    End Property

    Public ReadOnly Property DefaultReQueueReason As WorkQueueItemStatusReason
        Get
            Return (From wqisr In Me.StatusReasons Where wqisr.ItemStatusReason.Status = WrkQueue.StatusType.Requeue And wqisr.ItemStatusReason.IsActive = True And wqisr.ItemStatusReason.Reason = ServiceHelper.WQISR_DEFAULT_REQUEUE Select wqisr).FirstOrDefault()
        End Get
    End Property

    Public ReadOnly Property IsNew As Boolean
        Get
            Return Me.WorkQueue.IsNew
        End Get
    End Property

    <ValidateReDirectReasonCount(""), ValidateReQueueReasonCount(""), ValidateScheduleCount("")> _
    Public Property Id As Guid
        Get
            If (Me.WorkQueue.IsNew) Then
                Return Me._workQueueId
            Else
                Return Me._workQueue.Id
            End If
        End Get
        Set(ByVal value As Guid)
            If (Not Me.WorkQueue.IsNew) Then
                Me._workQueue.Id = value
            End If
        End Set
    End Property

    Public ReadOnly Property WorkQueue As WrkQueue.WorkQueue
        Get
            Return Me._workQueue
        End Get
    End Property

    Public ReadOnly Property IsDirty As Boolean
        Get
            Dim originalWorkQueue As WorkQueue
            If Me.IsNew Then Return True
            If (Me.WorkQueue.InActiveOn.HasValue AndAlso Me.WorkQueue.InActiveOn.Value < DateTime.UtcNow) Then Return False
            If IsWorkQueueDirty(originalWorkQueue) Then Return True
            If Me.IsChildrenDirty Then Return True
            If (Not originalWorkQueue Is Nothing) Then
                If (Not Me.StatusReasons Is Nothing) Then
                    For Each wqisr As WorkQueueItemStatusReason In Me.StatusReasons
                        ' If New Reason is Added and Not Deleted
                        If (wqisr.IsNew AndAlso Not wqisr.IsDeleted) Then Return True
                        ' Check if Original WQ Has that reason
                        Dim originalWqisr As WorkQueueItemStatusReason
                        If ((From orgWqisr As WorkQueueItemStatusReason In originalWorkQueue.StatusReasons _
                                         Where orgWqisr.Reason = wqisr.Reason AndAlso orgWqisr.ItemStatusReason.IsActive = wqisr.ItemStatusReason.IsActive).Count() <> 1) Then
                            Return True
                        End If
                    Next
                End If
            End If
            Return False
        End Get
    End Property

    Private Function IsWorkQueueDirty(ByRef originalWorkQueue As WorkQueue) As Boolean
        If Me.IsNew Then Return True
        originalWorkQueue = New WorkQueue(Me.Id)
        If (originalWorkQueue.WorkQueue.ActionCode <> Me.WorkQueue.ActionCode) Then Return True
        If (originalWorkQueue.WorkQueue.ActiveOn.AddMilliseconds(-1 * originalWorkQueue.WorkQueue.ActiveOn.Millisecond) <> _
            Me.WorkQueue.ActiveOn.AddMilliseconds(-1 * Me.WorkQueue.ActiveOn.Millisecond)) Then Return True
        If (originalWorkQueue.WorkQueue.AdminRole <> Me.WorkQueue.AdminRole) Then Return True
        If (originalWorkQueue.WorkQueue.CompanyCode <> Me.WorkQueue.CompanyCode) Then Return True
        Dim inActiveDate As Nullable(Of Date) = originalWorkQueue.WorkQueue.InActiveOn
        If (inActiveDate.HasValue) Then
            inActiveDate = inActiveDate.Value.AddMilliseconds(-1 * inActiveDate.Value.Millisecond)
        End If
        If (Not inActiveDate.Equals(Me.WorkQueue.InActiveOn)) Then Return True
        If (Not originalWorkQueue.WorkQueue.LockableDataTypeId.Equals(Me.WorkQueue.LockableDataTypeId)) Then Return True
        If (originalWorkQueue.WorkQueue.MaxRequeue <> Me.WorkQueue.MaxRequeue) Then Return True
        If (originalWorkQueue.WorkQueue.Name <> Me.WorkQueue.Name) Then Return True
        If (originalWorkQueue.WorkQueue.StartItemDelayMinutes <> Me.WorkQueue.StartItemDelayMinutes) Then Return True
        If (originalWorkQueue.WorkQueue.TimeToCompleteMinutes <> Me.WorkQueue.TimeToCompleteMinutes) Then Return True
        If (originalWorkQueue.WorkQueue.TimeZoneCode <> Me.WorkQueue.TimeZoneCode) Then Return True
        If (originalWorkQueue.WorkQueue.TransformationFile <> Me.WorkQueue.TransformationFile) Then Return True
        If (Not originalWorkQueue.WorkQueue.WorkQueueTypeId.Equals(Me.WorkQueue.WorkQueueTypeId)) Then Return True
        'DEF-3035
        If (originalWorkQueue.WorkQueue.RequeueItemDelayMinutes <> Me.WorkQueue.RequeueItemDelayMinutes) Then Return True
        'DEF-3035 End
        Return False
    End Function
#End Region

#Region "Time Zone Conversion"
    Public ReadOnly Property TimeZone As TimeZoneInfo
        Get
            If (Not _timeZoneInfo Is Nothing AndAlso Me._workQueue Is Nothing AndAlso _timeZoneInfo.StandardName <> Me._workQueue.TimeZoneCode) Then _timeZoneInfo = Nothing
            If (Me._workQueue Is Nothing OrElse Me._workQueue.TimeZoneCode Is Nothing OrElse String.IsNullOrEmpty(Me._workQueue.TimeZoneCode)) Then _timeZoneInfo = Nothing
            If (Me._timeZoneInfo Is Nothing AndAlso Not Me._workQueue Is Nothing) Then
                If ((From tzi In TimeZoneInfo.GetSystemTimeZones() Where tzi.StandardName = Me._workQueue.TimeZoneCode Select tzi).Count() = 1) Then
                    _timeZoneInfo = (From tzi In TimeZoneInfo.GetSystemTimeZones() Where tzi.StandardName = Me._workQueue.TimeZoneCode Select tzi).First()
                End If
            End If
            Return _timeZoneInfo
        End Get
    End Property

    Public Function ConvertTimeFromUtc(ByVal value As Nullable(Of DateTime)) As Nullable(Of DateTime)
        Return WrkQueue.WorkQueue.ConvertTimeFromUtc(value, Me.TimeZone)
    End Function

    Public Function ConvertTimeToUtc(ByVal value As Nullable(Of DateTime)) As Nullable(Of DateTime)
        Return WrkQueue.WorkQueue.ConvertTimeToUtc(value, Me.TimeZone)
    End Function

    Public Function ConvertTimeFromUtc(ByVal value As DateTime) As DateTime
        Return WrkQueue.WorkQueue.ConvertTimeFromUtc(value, Me.TimeZone)
    End Function

    Public Function ConvertTimeToUtc(ByVal value As DateTime) As DateTime
        Return WrkQueue.WorkQueue.ConvertTimeToUtc(value, Me.TimeZone)
    End Function
#End Region

#Region "Data Retrieveing / Updating Methods"

    Public Overrides Sub Validate()
        Me.WorkQueue.Validate()
        MyBase.Validate()
    End Sub

    Public Sub Save()
        Dim dal As New EntityScheduleDAL
        Dim userId As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        Me.Validate()
        If (Me.IsDirty) Then
            ' Add new Time Slots to Work Queue
            If (Dataset.Tables.Contains(EntityScheduleDAL.TABLE_NAME)) Then
                For Each dr As DataRow In Me.Dataset.Tables(EntityScheduleDAL.TABLE_NAME).Rows
                    Dim oEntitySchedule As New EntitySchedule(dr, Me)
                    If (oEntitySchedule.IsNew) Then
                        Dim scheduleDetailView As DataView
                        scheduleDetailView = ScheduleDetail.LoadScheduleDetail(oEntitySchedule.ScheduleId)
                        For Each scheduleDetailDr As DataRow In scheduleDetailView.Table.Rows
                            Dim oScheduleDetail As New ScheduleDetail(scheduleDetailDr)
                            Dim timeSlot As New WrkQueue.TimeSlot
                            timeSlot.ActivateOn = Me.ConvertTimeToUtc(oEntitySchedule.Effective.Value)
                            timeSlot.DeactivateOn = Me.ConvertTimeToUtc(oEntitySchedule.Expiration.Value)
                            timeSlot.Day = CType(Int32.Parse(LookupListNew.GetCodeFromId(LookupListNew.LK_DAYS_OF_WEEK, oScheduleDetail.DayOfWeekId)), DayOfWeek)
                            timeSlot.StartTime = New TimeSpan(oScheduleDetail.FromTime.Value.Hour, oScheduleDetail.FromTime.Value.Minute, oScheduleDetail.FromTime.Value.Second)
                            timeSlot.EndTime = New TimeSpan(oScheduleDetail.ToTime.Value.Hour, oScheduleDetail.ToTime.Value.Minute, oScheduleDetail.ToTime.Value.Second)
                            timeSlot.CreatedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                            If (Me.WorkQueue.Schedule Is Nothing) Then ReDim Me.WorkQueue.Schedule(-1)
                            ReDim Preserve Me.WorkQueue.Schedule(Me.WorkQueue.Schedule.Length)
                            Me.WorkQueue.Schedule(Me.WorkQueue.Schedule.Length - 1) = timeSlot
                        Next
                    ElseIf oEntitySchedule.IsDeleted Then
                        'if the schedule is to be deleted, then set the associated time slots Deactivation date as Activation date + 1
                        For Each timeSlot As WrkQueue.TimeSlot In Me.WorkQueue.Schedule
                            timeSlot.DeactivateOn = timeSlot.ActivateOn.AddSeconds(1)
                        Next
                    Else
                        For Each timeSlot As WrkQueue.TimeSlot In (From ts In Me.WorkQueue.Schedule Where ts.ActivateOn = oEntitySchedule.OriginalEffective AndAlso ts.DeactivateOn = oEntitySchedule.OriginalExpiration Select ts)
                            timeSlot.ActivateOn = Me.ConvertTimeToUtc(oEntitySchedule.Effective.Value)
                            timeSlot.DeactivateOn = Me.ConvertTimeToUtc(oEntitySchedule.Expiration.Value)
                        Next
                    End If
                Next
            End If


            _workQueue.ModifiedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            If (Me.IsNew) Then
                Try
                    WorkQueueClientProxy.CreateWorkQueue(_workQueue, userId)
                Catch ex As FaultException(Of WrkQueue.DuplicateWorkQueueFault)
                    Throw New BOValidationException(New ValidationError() {New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_DUPLICATE_WORKQUEUE_NAME, _workQueue.GetType(), Nothing, "Name", _workQueue.Name)}, _workQueue.GetType().Name)
                Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
                    Throw New UnauthorizedException("WorkQueue", "CreateWorkQueue", ex)
                Catch ex As FaultException(Of WrkQueue.ValidationFault)
                    Throw ex.AsBOValidationException(_workQueue)
                Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
                    Throw New ServiceException("WorkQueue", "CreateWorkQueue", ex)
                End Try
            Else
                Try
                    WorkQueueClientProxy.UpdateWorkQueue(_workQueue, userId)
                Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
                    Throw New UnauthorizedException("WorkQueue", "UpdateWorkQueue", ex)
                Catch ex As FaultException(Of WrkQueue.ValidationFault)
                    Throw ex.AsBOValidationException(_workQueue)
                Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
                    Throw New ServiceException("WorkQueue", "UpdateWorkQueue", ex)
                End Try
            End If

            ' Update Permissions
            ' Get Work Queue Permissions
            Dim permissions As Auth.Permission()
            Dim groups As Auth.Group()
            Dim mwqGroup As Auth.Group
            Dim cwqiGroup As Auth.Group
            Try
                permissions = AuthorizationClientProxy.FindPermissions(ServiceHelper.WORKQUEUE_SERVICE_NAME, New Auth.Permission With _
                    {.ResourceType = ServiceHelper.RESTYP_WORKQUEUE, .Resource = _workQueue.Name})
            Catch ex As FaultException(Of WrkQueue.ValidationFault)
                Throw ex.AsBOValidationException()
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "FindPermissions", ex)
            End Try

            ' Find Role IDs
            Try
                groups = AuthorizationClientProxy.GetGroups(ServiceHelper.WORKQUEUE_SERVICE_NAME)
            Catch ex As FaultException(Of WrkQueue.ValidationFault)
                Throw ex.AsBOValidationException()
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "GetGroups", ex)
            End Try

            mwqGroup = (From grp In groups Where grp.Name = "MWQ" Select grp).FirstOrDefault()
            cwqiGroup = (From grp In groups Where grp.Name = "CWQI" Select grp).FirstOrDefault()
            ' Grant Permissions to Roles
            Try
                AuthorizationClientProxy.AddPermissionsToGroup((From permission In permissions Where permission.Action = ServiceHelper.PA_WQ_VIEW OrElse permission.Action = ServiceHelper.PA_WQ_ADD_ITEM Select permission.Id).ToArray(), cwqiGroup.Id)
                AuthorizationClientProxy.AddPermissionsToGroup((From permission In permissions Where permission.Action = ServiceHelper.PA_WQ_EDIT Select permission.Id).ToArray(), mwqGroup.Id)
            Catch ex As FaultException(Of WrkQueue.ValidationFault)
                Throw ex.AsBOValidationException()
            Catch ex As FaultException(Of Auth.AuthorizationFault)
                Throw New ServiceException("Authorization", "AddPermissionsToGroup", ex)
            End Try
            ' Update Status Reasons
            For Each wqisr In Me.StatusReasons
                If (Not (wqisr.IsNew AndAlso wqisr.IsDeleted)) Then
                    wqisr.ItemStatusReason.WorkQueueId = Me.Id
                    If (wqisr.IsNew) Then
                        wqisr.Id = Guid.Empty
                        Try
                            WorkQueueClientProxy.CreateWorkQueueItemStatusReason(wqisr.ItemStatusReason, userId)
                        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
                            Throw New UnauthorizedException("WorkQueue", "CreateWorkQueueItemStatusReason", ex)
                        Catch ex As FaultException(Of WrkQueue.ValidationFault)
                            Throw ex.AsBOValidationException()
                        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
                            Throw New ServiceException("WorkQueue", "CreateWorkQueueItemStatusReason", ex)
                        End Try
                    Else
                        Try
                            wqisr.ItemStatusReason.UpdatedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                            WorkQueueClientProxy.UpdateWorkQueueItemStatusReason(wqisr.ItemStatusReason, userId)
                        Catch ex As FaultException(Of WrkQueue.NotAuthorizedFault)
                            Throw New UnauthorizedException("WorkQueue", "UpdateWorkQueueItemStatusReason", ex)
                        Catch ex As FaultException(Of WrkQueue.ValidationFault)
                            Throw ex.AsBOValidationException()
                        Catch ex As FaultException(Of WrkQueue.WorkQueueFault)
                            Throw New ServiceException("WorkQueue", "UpdateWorkQueueItemStatusReason", ex)
                        End Try
                    End If
                End If
            Next

            ' Update Work Queue ID in Schedule Collection
            If (Dataset.Tables.Contains(EntityScheduleDAL.TABLE_NAME)) Then
                For Each dr As DataRow In Me.Dataset.Tables(EntityScheduleDAL.TABLE_NAME).Rows
                    Dim oEntirySchedule As New EntitySchedule(dr, Me)
                    'Check if the Schedule is marked as to be deleted, if no, only then associate the Work queue ID with the schedule
                    If Not oEntirySchedule.IsDeleted Then
                        oEntirySchedule.EntityId = Me.Id
                    End If
                Next
                dal.Update(Me.Dataset)
            End If
            ElitaPlusIdentity.Current.ActiveUser.ResetExtendedUser()
        End If
    End Sub

    Public Function Clone() As WorkQueue
        Dim returnValue As WorkQueue
        returnValue.Copy(Me)
        Return returnValue
    End Function

    Public Sub Copy(ByVal target As WorkQueue)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Work Queue")
        End If

        With target.WorkQueue
            Me.WorkQueue.ActionCode = .ActionCode
            Me.WorkQueue.TimeZoneCode = .TimeZoneCode
            If (.ActiveOn < DateTime.UtcNow) Then
                Me.WorkQueue.ActiveOn = DateTime.UtcNow
            Else
                Me.WorkQueue.ActiveOn = .ActiveOn
            End If
            Me.WorkQueue.AdminRole = .AdminRole
            Me.WorkQueue.CompanyCode = .CompanyCode
            Me.WorkQueue.Expiration = DEFAULT_EXPIRATION_DATE
            Me.WorkQueue.LockableDataTypeId = .LockableDataTypeId
            Me.WorkQueue.MaxRequeue = .MaxRequeue
            Me.WorkQueue.ModifiedBy = String.Empty
            Me.WorkQueue.Name = String.Empty
            Me.WorkQueue.StartItemDelayMinutes = .StartItemDelayMinutes
            Me.WorkQueue.TimeToCompleteMinutes = .TimeToCompleteMinutes
            Me.WorkQueue.TransformationFile = .TransformationFile
            Me.WorkQueue.WorkQueueTypeId = .WorkQueueTypeId
            'DEF-3035
            Me.WorkQueue.RequeueItemDelayMinutes = .RequeueItemDelayMinutes
            'DEF-3035 End

            Dim newWqisr As WorkQueueItemStatusReason
            Dim defaultCompletedReason As WorkQueueItemStatusReason = target.DefaultCompletedReason
            Dim defaultReQueueReason As WorkQueueItemStatusReason = target.DefaultReQueueReason
            Dim defaultCompletedReasonId As Guid = If(defaultCompletedReason Is Nothing, Guid.Empty, defaultCompletedReason.Id)
            Dim defaultReQueueReasonId As Guid = If(defaultReQueueReason Is Nothing, Guid.Empty, defaultReQueueReason.Id)
            For Each oWqisr As WorkQueueItemStatusReason In target.StatusReasons
                If (oWqisr.Id <> defaultCompletedReasonId And oWqisr.Id <> defaultReQueueReasonId) Then
                    Select Case oWqisr.ItemStatusReason.Status
                        Case WrkQueue.StatusType.Completed
                            newWqisr = Me.AddReDirectReason()
                            newWqisr.ItemStatusReason.Reason = oWqisr.ItemStatusReason.Reason
                        Case WrkQueue.StatusType.Requeue
                            newWqisr = Me.AddReQueueReason()
                            newWqisr.ItemStatusReason.Reason = oWqisr.ItemStatusReason.Reason
                        Case Else
                            Throw New NotSupportedException()
                    End Select
                End If
            Next

            For Each dr As DataRow In target.Dataset.Tables(EntityScheduleDAL.TABLE_NAME).Rows
                Dim orgEntitySchedule As EntitySchedule
                Dim targetEntitySchedule As EntitySchedule
                orgEntitySchedule = New EntitySchedule(dr, target)
                If (Not orgEntitySchedule.Expiration Is Nothing AndAlso orgEntitySchedule.Expiration.Value < target.ConvertTimeFromUtc(DateTime.UtcNow)) Then
                    ' Already Expirted Schedules
                    Continue For
                End If

                targetEntitySchedule = Me.GetNewScheduleChild()
                With targetEntitySchedule
                    .ScheduleId = orgEntitySchedule.ScheduleId
                    .ScheduleCode = orgEntitySchedule.ScheduleCode
                    .ScheduleDescription = orgEntitySchedule.ScheduleDescription
                    If (target.ConvertTimeToUtc(orgEntitySchedule.Effective.Value) < DateTime.UtcNow) Then
                        .Effective = Me.ConvertTimeFromUtc(DateTime.UtcNow)
                    Else
                        .Effective = orgEntitySchedule.Effective
                    End If
                    .Expiration = orgEntitySchedule.Expiration
                    targetEntitySchedule.Save()
                End With
            Next
        End With
    End Sub

    Public Sub Delete()
        ' Get Latest Version from Server
        BuildWorkQueue(Me.Id)
        ' Discontinue with DateTime.UtcNow
        If (Me.WorkQueue.ActiveOn > DateTime.UtcNow) Then
            Me.WorkQueue.InActiveOn = Me.WorkQueue.ActiveOn.AddSeconds(1)
        Else
            Me.WorkQueue.InActiveOn = DateTime.UtcNow
        End If
        ' Discontinue all Schedules
        For Each entirySchedule As EntitySchedule In Me.ScheduleChildren
            If (entirySchedule.Effective.Value > DateTime.UtcNow) Then
                entirySchedule.Expiration = New DateTimeType(entirySchedule.Effective.Value.AddSeconds(1))
            Else
                entirySchedule.Expiration = DateTime.UtcNow
            End If
        Next
    End Sub

    Public Function CreateReason() As WorkQueueItemStatusReason
        Return New WorkQueueItemStatusReason(Me)
    End Function

    Public Function AddReDirectReason() As WorkQueueItemStatusReason
        Dim newReason As WorkQueueItemStatusReason
        newReason = AddReason(WrkQueue.StatusType.Completed)
        Return newReason
    End Function

    Public Function AddReQueueReason() As WorkQueueItemStatusReason
        Dim newReason As WorkQueueItemStatusReason
        newReason = AddReason(WrkQueue.StatusType.Requeue)
        Return newReason
    End Function

    Private Function AddReason(ByVal workQueueItemStatusReasonType As WrkQueue.StatusType) As WorkQueueItemStatusReason
        Dim newReason As WorkQueueItemStatusReason
        newReason = New WorkQueueItemStatusReason(Me)
        newReason.ItemStatusReason.Status = workQueueItemStatusReasonType
        Dim length As Integer = 0
        If (Not _statusReasons Is Nothing) Then length = _statusReasons.Length
        ReDim Preserve _statusReasons(length)
        _statusReasons(_statusReasons.Length - 1) = newReason
        Return newReason
    End Function
#End Region

#Region "Schedule"
    Public ReadOnly Property ScheduleChildren() As EntityScheduleList
        Get
            Return New EntityScheduleList(Me)
        End Get
    End Property

    Public Function GetScheduleSelectionView() As EntitySchedule.ScheduleSelectionView
        Return Me.ScheduleChildren.AsSelectionView()
    End Function

    Public Function GetScheduleChild(ByVal childId As Guid) As EntitySchedule
        Return CType(Me.ScheduleChildren.GetChild(childId), EntitySchedule)
    End Function

    Public Function GetNewScheduleChild() As EntitySchedule
        Dim newScheduleValue As EntitySchedule = CType(Me.ScheduleChildren.GetNewChild, EntitySchedule)
        newScheduleValue.Entity = "ELP_WORKQUEUE"
        newScheduleValue.EntityId = Me.Id
        newScheduleValue.Effective = Me.ConvertTimeFromUtc(DateTime.UtcNow)
        newScheduleValue.Expiration = DEFAULT_EXPIRATION_DATE
        Return newScheduleValue
    End Function
#End Region
End Class

Namespace WrkQueue
    <ValidatorTypeDescriptorAttribute(GetType(WorkQueueTypeDef))> _
    Partial Public Class WorkQueue
        Implements IBusinessObjectBase

        Public ReadOnly Property IsNew As Boolean
            Get
                Return Me.Id.Equals(Guid.Empty)
            End Get
        End Property

        Public Property UniqueId As String Implements IBusinessObjectBase.UniqueId
            Get
                Return String.Empty
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Default Friend Property Metadata(ByVal metadataName As String) As String
            Get
                If (Me.MetadataList Is Nothing) Then Return String.Empty
                If ((From wqmd In Me.MetadataList Where wqmd.Name = metadataName Select wqmd).Count() = 1) Then
                    Return (From wqmd In Me.MetadataList Where wqmd.Name = metadataName Select wqmd.Value).First()
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                Dim oWorkQueueMetadata As WorkQueueMetadata = Nothing
                Dim length As Integer
                If (Me.MetadataList Is Nothing) Then
                    length = 0
                Else
                    oWorkQueueMetadata = (From wqmd In Me.MetadataList Where wqmd.Name = metadataName Select wqmd).FirstOrDefault()
                    length = Me.MetadataList.Length
                End If
                If (oWorkQueueMetadata Is Nothing) Then
                    oWorkQueueMetadata = New WorkQueueMetadata()
                    oWorkQueueMetadata.Name = metadataName
                    oWorkQueueMetadata.CreatedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                    ReDim Preserve Me.MetadataList(length)
                    Me.MetadataList(Me.MetadataList.Length - 1) = oWorkQueueMetadata
                Else
                    oWorkQueueMetadata.UpdatedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                End If
                oWorkQueueMetadata.Value = value
            End Set
        End Property

        <ValueMandatory("")> _
        Public Property CompanyCode As String
            Get
                Return Me(ServiceHelper.WQ_MD_COMPANY_CODE)
            End Get
            Set(ByVal value As String)
                Me(ServiceHelper.WQ_MD_COMPANY_CODE) = value
                RaisePropertyChanged("CompanyCode")
            End Set
        End Property

        <ValueMandatory("")> _
        Public Property ActionCode As String
            Get
                Return Me(ServiceHelper.WQ_MD_ACTION_CODE)
            End Get
            Set(ByVal value As String)
                Me(ServiceHelper.WQ_MD_ACTION_CODE) = value
                RaisePropertyChanged("ActionCode")
            End Set
        End Property

        <ValueMandatory("")> _
        Public Property AdminRole As String
            Get
                Return Me(ServiceHelper.WQ_MD_ADMIN_ROLE)
            End Get
            Set(ByVal value As String)
                Me(ServiceHelper.WQ_MD_ADMIN_ROLE) = value
                RaisePropertyChanged("AdminRole")
            End Set
        End Property

        <ValueMandatory(""), ValidateTransformationFile("")> _
        Public Property TransformationFile As String
            Get
                Return Me(ServiceHelper.WQ_MD_TRANSFORMATION_FILE)
            End Get
            Set(ByVal value As String)
                Me(ServiceHelper.WQ_MD_TRANSFORMATION_FILE) = value
                RaisePropertyChanged("TransformationFile")
            End Set
        End Property

        Public ReadOnly Property ActionName As String
            Get
                Return LookupListNew.GetDescriptionFromCode(LookupListCache.LK_WQ_ACTION, Me.ActionCode)
            End Get
        End Property

        <ValueMandatory(""), DateCompareValidatorAttribute("", Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, _
            "Expiration", DateCompareValidatorAttribute.CompareType.LessThan, DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.MaxDate, _
            DefaultCompareValue:=DateCompareValidatorAttribute.DefaultType.MinDate)> _
        Public Property Effective As Date
            Get
                Return Me.ConvertTimeFromUtc(Me.ActiveOn, Me.TimeZoneCode)
            End Get
            Set(ByVal value As Date)
                Me.ActiveOn = Me.ConvertTimeToUtc(value, Me.TimeZoneCode)
            End Set
        End Property

        <ValueMandatory(""), DateCompareValidatorAttribute("", Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, _
            "Effective", DateCompareValidatorAttribute.CompareType.GreaterThan, DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.MinDate, _
            DefaultCompareValue:=DateCompareValidatorAttribute.DefaultType.MaxDate)> _
        Public Property Expiration As Nullable(Of Date)
            Get
                Return Me.ConvertTimeFromUtc(Me.InActiveOn, Me.TimeZoneCode)
            End Get
            Set(ByVal value As Nullable(Of Date))
                Me.InActiveOn = Me.ConvertTimeToUtc(value, Me.TimeZoneCode)
            End Set
        End Property

#Region "Time Conversion Functions"
        Private Shared Function GetTimeZoneInfo(ByVal standardName As String) As TimeZoneInfo
            If (standardName Is Nothing OrElse String.IsNullOrEmpty(standardName)) Then Return Nothing
            Return (From tzi In TimeZoneInfo.GetSystemTimeZones() Where tzi.StandardName = standardName Select tzi).First()
        End Function

        Friend Shared Function ConvertTimeFromUtc(ByVal value As Nullable(Of DateTime), ByVal pTimeZoneStandardName As String) As Nullable(Of DateTime)
            Return ConvertTimeFromUtc(value, GetTimeZoneInfo(pTimeZoneStandardName))
        End Function

        Friend Shared Function ConvertTimeToUtc(ByVal value As Nullable(Of DateTime), ByVal pTimeZoneStandardName As String) As Nullable(Of DateTime)
            Return ConvertTimeToUtc(value, GetTimeZoneInfo(pTimeZoneStandardName))
        End Function

        Friend Shared Function ConvertTimeFromUtc(ByVal value As DateTime, ByVal pTimeZoneStandardName As String) As DateTime
            Return ConvertTimeFromUtc(value, GetTimeZoneInfo(pTimeZoneStandardName))
        End Function

        Friend Shared Function ConvertTimeToUtc(ByVal value As DateTime, ByVal pTimeZoneStandardName As String) As DateTime
            Return ConvertTimeToUtc(value, GetTimeZoneInfo(pTimeZoneStandardName))
        End Function

        Friend Shared Function ConvertTimeFromUtc(ByVal value As Nullable(Of DateTime), ByVal pTimeZone As TimeZoneInfo) As Nullable(Of DateTime)
            If (value.HasValue) Then
                Return New Nullable(Of DateTime)(ConvertTimeFromUtc(value.Value, pTimeZone))
            Else
                Return value
            End If
        End Function

        Friend Shared Function ConvertTimeToUtc(ByVal value As Nullable(Of DateTime), ByVal pTimeZone As TimeZoneInfo) As Nullable(Of DateTime)
            If (value.HasValue) Then
                Return New Nullable(Of DateTime)(ConvertTimeToUtc(value.Value, pTimeZone))
            Else
                Return value
            End If
        End Function

        Friend Shared Function ConvertTimeFromUtc(ByVal value As DateTime, ByVal pTimeZone As TimeZoneInfo) As DateTime
            Dim oTimeZoneInfo As TimeZoneInfo
            oTimeZoneInfo = pTimeZone
            If (oTimeZoneInfo Is Nothing) Then
                Return value
            Else
                value = DateTime.SpecifyKind(value, DateTimeKind.Unspecified)
                Return TimeZoneInfo.ConvertTimeFromUtc(value, oTimeZoneInfo)
            End If
        End Function

        Friend Shared Function ConvertTimeToUtc(ByVal value As DateTime, ByVal pTimeZone As TimeZoneInfo) As DateTime
            Dim oTimeZoneInfo As TimeZoneInfo
            oTimeZoneInfo = pTimeZone
            If (oTimeZoneInfo Is Nothing) Then
                Return value
            Else
                value = DateTime.SpecifyKind(value, DateTimeKind.Unspecified)
                Return TimeZoneInfo.ConvertTimeToUtc(value, oTimeZoneInfo)
            End If
        End Function
#End Region

    End Class

    Public Class WorkQueueTypeDef
        <ValueMandatory(""), ValidStringLength("", Max:=250, Min:=3)> _
        Public Property Name() As Object
            Get
                Throw New InvalidOperationException()
            End Get
            Set(ByVal value As Object)
                Throw New InvalidOperationException()
            End Set
        End Property

        <ValueMandatory(""), ValidStringLength("", Max:=50, Min:=3)> _
        Public Property TimeZoneCode() As Object
            Get
                Throw New InvalidOperationException()
            End Get
            Set(ByVal value As Object)
                Throw New InvalidOperationException()
            End Set
        End Property

        <ValueMandatory("")> _
        Public Property WorkQueueTypeId() As Object
            Get
                Throw New InvalidOperationException()
            End Get
            Set(ByVal value As Object)
                Throw New InvalidOperationException()
            End Set
        End Property

        <ValueMandatory("")> _
        Public Property StartItemDelayMinutes() As Object
            Get
                Throw New InvalidOperationException()
            End Get
            Set(ByVal value As Object)
                Throw New InvalidOperationException()
            End Set
        End Property
        'DEF-3035
        <ValueMandatory("")> _
        Public Property RequeueItemDelayMinutes() As Object
            Get
                Throw New InvalidOperationException()
            End Get
            Set(ByVal value As Object)
                Throw New InvalidOperationException()
            End Set
        End Property
        'DEF-3035 End

        <ValueMandatory(""), ValidNumericRange("", Min:=15, MinExclusive:=False, Message:=Assurant.ElitaPlus.Common.ErrorCodes.GUI_TIME_TO_COMPLETE_MIN_VALUE)> _
        Public Property TimeToCompleteMinutes() As Object
            Get
                Throw New InvalidOperationException()
            End Get
            Set(ByVal value As Object)
                Throw New InvalidOperationException()
            End Set
        End Property

        <DateCompareValidator("", Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, "", _
            DateCompareValidatorAttribute.CompareType.GreaterThan, CheckWhenNew:=True, CompareToType:=DateCompareValidatorAttribute.CompareToPropertyType.Nothing, _
            DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.UtcToday)> _
        Public Property ActiveOn() As DateTime
            Get
                Throw New InvalidOperationException()
            End Get
            Set(ByVal value As DateTime)
                Throw New InvalidOperationException()
            End Set
        End Property
    End Class
End Namespace

#Region "Custom Validations"
<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidateTransformationFile
    Inherits ValidBaseAttribute
    Private _fieldDisplayName As String
    Public Sub New(ByVal fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_TRANSFORMATION_FILE_NOT_FOUND)
    End Sub

    Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
        If (objectToCheck Is Nothing) Then Return False
        Return BaseActionProvider.TransformationFileExists(objectToCheck.ToString())
    End Function
End Class

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidateScheduleCount
    Inherits ValidBaseAttribute
    Public Sub New(ByVal fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_WQ_SCHEDULE_REQUIRED)
    End Sub

    Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
        Dim obj As WorkQueue = CType(objectToValidate, WorkQueue)
        Return obj.ScheduleChildren.Count > 0
    End Function
End Class

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidateReDirectReasonCount
    Inherits ValidBaseAttribute
    Public Sub New(ByVal fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_WQ_REDIRECT_REQUIRED)
    End Sub

    Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
        Dim obj As WorkQueue = CType(objectToValidate, WorkQueue)
        Return obj.ReDirectReasons.Length > 0
    End Function
End Class

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidateReQueueReasonCount
    Inherits ValidBaseAttribute
    Public Sub New(ByVal fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_WQ_REQUEUE_REQUIRED)
    End Sub

    Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
        Dim obj As WorkQueue = CType(objectToValidate, WorkQueue)
        Return obj.ReQueueReasons.Length > 0
    End Function
End Class
#End Region

