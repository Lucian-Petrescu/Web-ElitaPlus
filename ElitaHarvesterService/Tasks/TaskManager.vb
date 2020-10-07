Imports System.Text
Imports System.Threading
Imports Assurant.ElitaPlus.BusinessObjectsNew


Public Class TaskManager

    Public Enum TaskManagerReturnStatus
        Success
        Failed
        NoTaskFound
    End Enum

    Private ReadOnly _processThreadName As String
    Private _subscriberId As Guid

    Public Sub New(processThreadName As String)
        _processThreadName = processThreadName

    End Sub

    Public ReadOnly Property MachineName As String
        Get
            Return Environment.MachineName
        End Get
    End Property

    Public ReadOnly Property ProcessThreadName() As String
        Get
            Return _processThreadName
        End Get
    End Property

    Public ReadOnly Property SubscriberId() As Guid
        Get
            _subscriberId = LookupListNew.GetIdFromCode(Codes.SUBSCRIBER_TYPE, Codes.SUBSCRIBER_TYPE__HARVESTER)
            Return _subscriberId
        End Get
    End Property

    Private Function GetNextTask() As PublishedTask
        Logger.AddDebugLogEnter()
        Dim _publishedTask As PublishedTask = Nothing

        'Get the Event
        Try
            _publishedTask = PublishedTask.GetNextTask(SubscriberId, MachineName, ProcessThreadName)

        Catch ex As Exception
            Dim message As String = ex.Message
            If (ex.InnerException IsNot Nothing) Then
                message += "|Inner Exception Message: " & ex.InnerException.Message
            End If
            Logger.AddError(String.Format("Exception while fetching next task| Exception:{0}", message), ex)
            Throw
        End Try
        Logger.AddDebugLogExit()
        Return _publishedTask
    End Function

    Public Function Process() As TaskManagerReturnStatus
        Logger.AddDebugLogEnter()

        Dim status As TaskManagerReturnStatus
        Try
            Dim hostName As String = System.Environment.MachineName

            Dim ptask As PublishedTask = GetNextTask()

            If (ptask IsNot Nothing) Then
                'Process the Task based on Event Type
                'Todo Call dynamically using Reflection
                Logger.AddDebugLog(String.Format("Processing Task|Task Name:{0}|START", ptask.Task.Description))
                TaskFactory.CreateTask(ptask, MachineName, ProcessThreadName).Process()
                Logger.AddDebugLog(String.Format("Processing Task|Task Name:{0}|END", ptask.Task.Description))
                status = TaskManagerReturnStatus.Success
            Else
                status = TaskManagerReturnStatus.NoTaskFound
            End If
        Catch ex As Exception
            Logger.AddError("Exception Occurred while processing Task", ex)
            status = TaskManagerReturnStatus.Failed
        End Try
        Logger.AddDebugLogExit()
        Return status
    End Function

    Public Function Login(networkId As String, hub As String, machineDomain As String) As Boolean
        Logger.AddDebugLogEnter()
        Dim logging As StringBuilder = New StringBuilder()
        Dim errMsg As String = String.Empty
        Try
            Dim oAuthentication As Assurant.ElitaPlus.BusinessObjectsNew.Authentication = New Assurant.ElitaPlus.BusinessObjectsNew.Authentication()

            logging.Append("Authentication done")

            'The Machine name ex; ATL0, BUE0, ORK0 have to passed with EU or NONE if development
            oAuthentication.CreatePrincipalForServices(networkId, hub, machineDomain)

            logging.Append(" Principal created")
            Assurant.ElitaPlus.BusinessObjectsNew.Authentication.SetCulture()
            logging.Append(" Culture set")
            Logger.AddDebugLogExit()
            Return True
        Catch ex As Exception
            errMsg = ex.Message
            If (ex.InnerException IsNot Nothing) Then
                errMsg += " Inner Exception Message" & ex.InnerException.Message
                Logger.AddError(String.Format("Error in logging in : {0}", logging), ex)
            End If
            Logger.AddDebugLogExit()
            Return False
        End Try

    End Function
End Class

