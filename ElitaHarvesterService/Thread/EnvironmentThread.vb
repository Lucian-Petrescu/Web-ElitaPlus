Imports System.Configuration
Imports System.Threading
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Net.Mail

Public Class EnvironmentThread
    Inherits ThreadBase

    Private _config As IThreadEnvironmentConfig
    Private _taskManager As TaskManager
    Private Const NETWORK_ID = "NETWORKID"
    Private Const MAX_ALLOWED_FAILURE_COUNT = "MAX_ALLOWED_FAILURE_COUNT"
    Private Const FAILURE_SLEEP_TIME_IN_MIN = "FAILURE_SLEEP_TIME_IN_MIN"
    Private Const SMTP_MAILSERVER = "SMTPMAILSERVER"
    Private Const ERRORMAIL = "ERRORMAIL"

    Public Property Config() As IThreadEnvironmentConfig
        Get
            Return _config
        End Get
        Private Set(value As IThreadEnvironmentConfig)
            _config = value
        End Set
    End Property

    Public Sub New(config As IThreadEnvironmentConfig)
        MyBase.New(config)
        _config = config
        _taskManager = New TaskManager(Me.Config.Name)
    End Sub

    Private _maxAllowedFailureCount As Integer = 5
    Private ReadOnly Property MaxAllowedFailureCount As Integer
        Get
            Dim v As Integer
            If (Integer.TryParse(ConfigurationManager.AppSettings(MAX_ALLOWED_FAILURE_COUNT), v)) Then
                _maxAllowedFailureCount = v
            End If
            Return _maxAllowedFailureCount
        End Get

    End Property

    Private _failureSleepTime As Integer = 600000
    Private ReadOnly Property FailureSleepTime As Integer
        Get
            Dim v As Integer
            If (Integer.TryParse(ConfigurationManager.AppSettings(FAILURE_SLEEP_TIME_IN_MIN), v)) Then
                _failureSleepTime = v * 60 * 1000
            End If
            Return _failureSleepTime
        End Get

    End Property


    Protected Overrides Sub MethodToInvoke()
        Logger.AddDebugLogEnter()
        If (Not Login()) Then
            Logger.AddError("Login Failed. Exiting Thread")
            Exit Sub
        End If

        Dim failureCount As Integer = 0
        Try
            Logger.AddDebugLogEnter()
            While (Not _stopped)
                Dim processResponse As TaskManager.TaskManagerReturnStatus
                processResponse = _taskManager.Process()
                Select Case processResponse
                    Case TaskManager.TaskManagerReturnStatus.NoTaskFound
                        Logger.AddInfo("No Task Found. Thread going to sleep")
                        MyBase.Sleep()
                    Case TaskManager.TaskManagerReturnStatus.Success
                        failureCount = 0
                        Continue While
                    Case TaskManager.TaskManagerReturnStatus.Failed
                        Logger.AddError("Failed to Process the Task")
                        failureCount = failureCount + 1
                        If (failureCount > MaxAllowedFailureCount) Then
                            'Send Email
                            SendNotificationEmail()
                            Thread.Sleep(FailureSleepTime)
                        End If
                        Continue While
                End Select
               
            End While
        Catch ex As Exception
            Logger.AddError(ex)
        End Try
    End Sub

    Protected Function Login() As Boolean
        Logger.AddDebugLogEnter()
        Dim networkId As String = ConfigurationManager.AppSettings(NETWORK_ID)
        Dim loginStatus As Boolean = False

        loginStatus = _taskManager.Login(networkId, _config.Hub, _config.MachineDomain)
        If (Not loginStatus) Then
            Logger.AddError(String.Format("LOGIN FAILED for {0} :machine Domain: {1} connType: {2}", networkId, _config.MachineDomain, _config.Hub))
        End If
        Logger.AddInfo(String.Format("LOGIN SUCCESS for {0} :machine Domain: {1} connType: {2}", networkId, _config.MachineDomain, _config.Hub))
        Logger.AddDebugLogExit()
        Return loginStatus
    End Function

    Private Sub Initialize()
    End Sub

    Private Sub SendNotificationEmail()
        Dim mail As MailMessage = New System.Net.Mail.MailMessage()
        Dim ser As SmtpClient = New System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings(SMTP_MAILSERVER))
        Dim recips As String = ConfigurationManager.AppSettings(ERRORMAIL)
        Try
            mail.To.Add(ConfigurationManager.AppSettings(ERRORMAIL))
            mail.Subject = String.Format("[Environment : {0} | Hub : {1} | Machine Domain : {2}] - Elita Harvester Service - More than {3} taks failed consecutively", _config.Environment, _config.Hub, _config.MachineDomain, MaxAllowedFailureCount.ToString())
            mail.From = New MailAddress(ConfigurationManager.AppSettings(ERRORMAIL))
            mail.Body = String.Format("Elita Harvester is going to sleep for next {0} min for Environment : {1} | Hub : {2} | Machine Domain : {3}.", ConfigurationManager.AppSettings(FAILURE_SLEEP_TIME_IN_MIN).ToString(), _config.Environment, _config.Hub, _config.MachineDomain)

            ser.Send(mail)
        Catch ex As Exception
            Logger.AddError("Failed to Send the Notification Email", ex)
            Throw ex
        End Try
    End Sub


End Class
