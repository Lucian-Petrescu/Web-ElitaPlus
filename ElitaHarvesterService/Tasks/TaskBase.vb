Imports Assurant.ElitaPlus.BusinessObjectsNew

Public MustInherit Class TaskBase
    Implements ITask

    Private _publishedTask As PublishedTask
    Private ReadOnly _machineName As String
    Private ReadOnly _processThreadName As String
    Private _failReason As String

    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        _machineName = machineName
        _processThreadName = processThreadName
    End Sub

    Public Property FailReason As String
        Get
            Return _failReason
        End Get
        Protected Set(ByVal value As String)
            _failReason = value
        End Set
    End Property

    Friend ReadOnly Property MachineName() As String
        Get
            Return _machineName
        End Get
    End Property

    Friend ReadOnly Property ProcessThreadName() As String
        Get
            Return _processThreadName
        End Get
    End Property

    Friend Property PublishedTask As PublishedTask
        Get
            Return _publishedTask
        End Get
        Set(ByVal value As PublishedTask)
            _publishedTask = value
        End Set
    End Property

    Protected Friend MustOverride Sub Execute()

    Private Sub CompleteTask()
        Me.PublishedTask.CompleteTask(Me.MachineName, Me.ProcessThreadName)
    End Sub

    Private Sub FailedTask()
        Me.PublishedTask.FailedTask(Me.MachineName, Me.ProcessThreadName, Me.FailReason)
    End Sub

    Public Sub Process() Implements ITask.Process
        Try
            Execute()
            CompleteTask()
            Logger.AddInfo("Task executed successfully")
        Catch ex As Exception
            Me.FailReason = Me.FailReason + String.Format("|Exception Message: {0} ", ex.Message)
            FailedTask()
            Logger.AddError("Task failed.", ex)
        End Try
    End Sub


End Class
