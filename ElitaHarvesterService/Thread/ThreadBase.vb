Imports System.Threading
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.Environment

Public MustInherit Class ThreadBase

    Private _config As IThreadConfig
    Protected _sleepTime As TimeSpan
    Protected _threadObject As Thread
    Protected _stopped As Boolean

    Private ReadOnly Property ThreadName As String
        Get
            Return String.Format("{0}:{1}", EnvironmentContext.Current.EnvironmentName, _config.Name)
        End Get
    End Property

    Public Sub New(ByVal config As IThreadConfig)
        _config = config
        ''TODO: Handle Seconds to Time Span Conversion.... chances of Overflow
        Me.SleepTime = New TimeSpan(0, 0, config.SleepTimeSeconds)
    End Sub

    Protected Sub Sleep()
        Thread.Sleep(Me.SleepTime)
    End Sub

    Public Property SleepTime() As TimeSpan
        Get
            Return _sleepTime
        End Get
        Private Set(ByVal value As TimeSpan)
            _sleepTime = value
        End Set
    End Property

    <Obsolete()> _
    Public Sub New(ByVal timespan As Nullable(Of TimeSpan))
        _sleepTime = timespan.Value
    End Sub

    Private ReadOnly Property ThreadObject As Thread
        Get
            Return _threadObject
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return _threadObject.Name
        End Get
    End Property

    Public ReadOnly Property State As String
        Get
            Return _threadObject.ThreadState.ToString
        End Get
    End Property

    Public ReadOnly Property IsAlive As Boolean
        Get
            Return _threadObject.IsAlive
        End Get
    End Property

    Public Sub ThreadStart(ByVal runAtBackground As Boolean)
        Logger.AddDebugLogEnter()
        _threadObject = New Thread(New ThreadStart(AddressOf MethodToInvoke))
        _threadObject.Name = Me.ThreadName
        _threadObject.IsBackground = runAtBackground
        _threadObject.Start()
        Logger.AddDebugLogExit()
    End Sub

    Public Sub ThreadStop()
        _stopped = True
        ' Increase Priority of Thread so that CPU will try to finish thread early and adhering to grace time limits
        Me.ThreadObject.Priority = ThreadPriority.AboveNormal
        Thread.CurrentThread.Priority = ThreadPriority.BelowNormal
        Thread.Sleep(30) 'Read value from Config
        If (ThreadObject.IsAlive) Then
            Try
                ThreadObject.Join(30) 'Read value from Config
            Catch ex As Exception
                Logger.AddError(String.Format("Failed to Stop Thread ", ex))
            End Try
        End If


    End Sub

    Protected MustOverride Sub MethodToInvoke()

End Class
