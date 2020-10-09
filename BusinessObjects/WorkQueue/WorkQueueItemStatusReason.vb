Imports System.ComponentModel
Imports System.Collections.Generic

Public Class WorkQueueItemStatusReason
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const COL_REASON As String = "Reason"
    Public Const COL_DESCRIPTION As String = "Description"
#End Region

#Region "Instance Fields"
    Private _isNew As Boolean = False
    Private _isDeleted As Boolean = False
    Private _workQueueItemStatusReason As WrkQueue.WorkQueueItemStatusReason
    Private _description As String
    Private _workQueue As WorkQueue
#End Region

#Region "Constructors"
    Public Sub New(pWorkQueue As WorkQueue, pWorkQueueItemStatusReason As WrkQueue.WorkQueueItemStatusReason)
        MyBase.New()
        _isNew = False
        _isDeleted = False
        _workQueue = pWorkQueue
        _workQueueItemStatusReason = pWorkQueueItemStatusReason
        UpdateDescription()
        AddHandler _workQueueItemStatusReason.PropertyChanged, New PropertyChangedEventHandler(AddressOf WorkQueueItemStatusReason_PropertyChanged)
    End Sub

    Public Sub New(pWorkQueue As WorkQueue)
        MyBase.New()
        Dataset = pWorkQueue.Dataset
        _isNew = True
        _isDeleted = False
        _workQueue = pWorkQueue
        _workQueueItemStatusReason = New WrkQueue.WorkQueueItemStatusReason()
        Id = Guid.NewGuid
        ItemStatusReason.WorkQueueId = pWorkQueue.Id
        _workQueueItemStatusReason.IsActive = True
        _workQueueItemStatusReason.CreatedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        AddHandler _workQueueItemStatusReason.PropertyChanged, New PropertyChangedEventHandler(AddressOf WorkQueueItemStatusReason_PropertyChanged)
    End Sub
#End Region

#Region "Public Methods"
    Public Overrides Sub Delete()
        _isDeleted = True
        _workQueueItemStatusReason.IsActive = False
    End Sub

    Public Overrides Sub Validate()
        MyBase.Validate()
    End Sub
#End Region

#Region "Friend Methods"
    Private Sub UpdateDescription()
        If (_workQueueItemStatusReason.Reason Is Nothing OrElse String.IsNullOrEmpty(_workQueueItemStatusReason.Reason)) Then
            _description = String.Empty
        Else
            _description = LookupListNew.GetDescriptionFromCode(LookupListCache.LK_REASON_CODE, _workQueueItemStatusReason.Reason)
        End If
    End Sub
#End Region

#Region "Event Handlers"
    Private Sub WorkQueueItemStatusReason_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        If (e.PropertyName = "Reason") Then
            UpdateDescription()
        End If
    End Sub
#End Region

#Region "Instance Properties"
    Public ReadOnly Property WorkQueue As WorkQueue
        Get
            Return _workQueue
        End Get
    End Property

    Public Property Id As Guid
        Get
            Return ItemStatusReason.Id
        End Get
        Set
            ItemStatusReason.Id = value
        End Set
    End Property

    <ValueMandatory(""), ValidateDuplicateReasons("")> _
    Public ReadOnly Property Reason As String
        Get
            Return ItemStatusReason.Reason
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property Description As String
        Get
            Return _description
        End Get
        Private Set
            _description = value
        End Set
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

    Public Property ItemStatusReason As WrkQueue.WorkQueueItemStatusReason
        Get
            Return _workQueueItemStatusReason
        End Get
        Friend Set
            _workQueueItemStatusReason = value
        End Set
    End Property
#End Region

End Class

Namespace WrkQueue
    <ValidatorTypeDescriptorAttribute(GetType(WorkQueueItemStatusReasonTypeDef))> _
    Partial Public Class WorkQueueItemStatusReason
        Implements IBusinessObjectBase

        Public Property UniqueId As String Implements IBusinessObjectBase.UniqueId
            Get
                Return String.Empty
            End Get
            Set

            End Set
        End Property
    End Class

    Public Class WorkQueueItemStatusReasonTypeDef
        <ValueMandatory("")> _
        Public Property Reason As Object
            Get
                Throw New NotImplementedException
            End Get
            Set
                Throw New NotImplementedException
            End Set
        End Property
    End Class
End Namespace

#Region "Custom Validations"

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidateDuplicateReasons
    Inherits ValidBaseAttribute
    Public Sub New(fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Common.ErrorCodes.BO_ERROR_WQ_DUPLICATE_STATUS_REASON)
    End Sub

    Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
        Dim obj As WorkQueueItemStatusReason = CType(objectToValidate, WorkQueueItemStatusReason)
        Dim cnt As Integer
        cnt = (From wqisr As WorkQueueItemStatusReason In obj.WorkQueue.StatusReasons _
               Where wqisr.Reason = obj.Reason AndAlso obj.ItemStatusReason.Status = wqisr.ItemStatusReason.Status AndAlso obj.Id <> wqisr.Id Select wqisr).Count()
        Return cnt = 0
    End Function
End Class

#End Region


