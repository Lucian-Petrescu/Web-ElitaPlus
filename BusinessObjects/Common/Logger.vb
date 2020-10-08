Imports System.Diagnostics
Imports System.Globalization
Imports System.Threading
Imports System.Reflection
Imports System.Text
Imports Assurant.Elita.Base.DataAccess
Imports Assurant.Elita.Configuration
Imports Assurant.Elita.Logging.Interface
Imports Oracle.ManagedDataAccess.Client
Imports Assurant.Elita.Logging.OracleLogger

Public Module Logger

    Private traceSource As TraceSource = New TraceSource("ElitaHarvesterService", Nothing)

    Public Delegate Sub AddMessageDelegate(message As String)
    Public Delegate Sub AddExceptionDelegate(exception As Exception)
    Public Delegate Sub AddErrorDelegate(message As String, exception As Exception)
    Private Property _loggerClient As ILoggerClient

    Public Sub Initialize(applicationName As String)
        Try
            Dim OracleHelper As OracleHelper = New OracleHelper(Function() New OracleConnectionStringBuilder() With {
            .UserID = ElitaConfig.Current.Database.UserName,
            .Password = ElitaConfig.Current.Database.Password,
            .DataSource = AppConfig.DataBase.Server
          }, String.Empty)

            _loggerClient = New OracleLogger(OracleHelper, applicationName, Environment.MachineName)
        Catch ex As Exception
            AddEventException(ex)
        End Try
    End Sub

    Sub New()
        For Each listener As TraceListener In traceSource.Listeners
            If (TypeOf (listener) Is EventLogTraceListener) Then
                Dim eventListener As EventLogTraceListener = DirectCast(listener, EventLogTraceListener)
                Dim sourceName As String = "ElitaHarvesterService"
                Dim logName As String = "ElitaPlus"
                Dim log As EventLog = New EventLog()
                log.Source = sourceName
                log.Log = logName
                eventListener.EventLog = log
            End If
        Next
    End Sub

    Public Sub AddInfo(message As String)
        TraceLine(TraceEventType.Information, message)

        Try
            _loggerClient.LogTrace(New TraceLogItem() With {
        .Message = message,
        .Timestamp = DateTime.Now
         })
        Catch ex As Exception
            AddEventException(ex)
        End Try

    End Sub

    Public Sub AddError(exception As Exception)
        Try
            _loggerClient.LogException(New ExceptionLogItem() With {
        .Exception = exception,
        .Timestamp = DateTime.Now
         })
        Catch ex As Exception
            AddEventException(ex)
        End Try

    End Sub

    Public Sub AddEventException(exception As Exception)
        Dim sb As New StringBuilder
        BuildExceptionString(exception, sb)
        AddError(sb.ToString())
    End Sub

    Public Sub AddError(message As String, exception As Exception)
        Try
            _loggerClient.LogException(New ExceptionLogItem() With {
        .Exception = exception,
        .Message = message,
        .Timestamp = DateTime.Now
         })


        Catch ex As Exception
            Dim sb As New StringBuilder
            sb.AppendLine(message)
            BuildExceptionString(exception, sb)
            AddError(sb.ToString())
        End Try
    End Sub

    Private Sub BuildExceptionString(exception As Exception, sb As StringBuilder)
        sb.AppendLine("*** Start of Exception ***")
        sb.AppendFormat("Exception of Type {0} with message {1}", exception.GetType().FullName, exception.Message)
        sb.AppendLine()
        sb.AppendLine(exception.StackTrace)
        sb.AppendLine("*** End of Exception ***")
        If (Not exception.InnerException Is Nothing) Then
            BuildExceptionString(exception.InnerException, sb)
        End If
    End Sub

    Public Sub AddError(message As String)
        Try
            _loggerClient.LogException(New ExceptionLogItem() With {
        .Message = message,
        .Timestamp = DateTime.Now
         })

            TraceLine(TraceEventType.Error, message)
        Catch ex As Exception
            AddEventException(ex)
        End Try
    End Sub

    Public Sub AddWarning(message As String)
        AddInfo(message)
    End Sub

    Public Sub AddDebugLog(message As String)
        AddInfo(message)
    End Sub

    Public Sub AddDebugLogEnter()
        AddInfo("Enter")
    End Sub

    Public Sub AddDebugLogExit()
        AddInfo("Exit")
    End Sub

    Private Sub TraceLine(level As TraceEventType, message As String)
        Dim traceLevel As String = Nothing

        Select Case level
            Case Diagnostics.TraceEventType.Error
                traceLevel = "ERROR: "

            Case Diagnostics.TraceEventType.Warning
                traceLevel = "WARNING: "

            Case Diagnostics.TraceEventType.Information
                traceLevel = "INFO: "

            Case Diagnostics.TraceEventType.Verbose
                traceLevel = "DEBUG: "

            Case Else
                traceLevel = "DEFAULT:"
        End Select

        Dim stackframe As New Diagnostics.StackFrame(2)
        Dim finalMessage As String = String.Format(CultureInfo.InvariantCulture,
            "{0}Environment - {4} | {1}.{2} | {3}", traceLevel, stackframe.GetMethod.DeclaringType.Name, stackframe.GetMethod.Name, message, Thread.CurrentThread.Name)

        traceSource.TraceEvent(level, 0, finalMessage)

    End Sub

End Module
