Imports System
Imports System.Reflection
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

Public MustInherit Class ElitaMethodBase

    Protected MyObject As Object
    Protected _objectType As String
    Private _param() As MethodParams


    Public Const SERVICE_NAME As String = "ElitaBatchServices"
    Public Const LOG_SOURCE_NAME As String = "ElitaBatchServicesApp"
    Private LOG_NAME As String = "ElitaPlus"
    Protected _eventLog As EventLog = Nothing

    Public Sub New()
        InitializeEventLog()
    End Sub

    Public MustOverride Function Execute() As String
    Public MustOverride Function ExecuteAsync() As String
    Protected MustOverride Function AsyncMethodSub() As String
    Public MustOverride Sub SetDS(xmlData As String)

    Public Class MethodParams

        Public Name As String = String.Empty
        Public ObjectType As String = String.Empty
        Public Value As Object

        Sub New(Name As String, Value As String)
            Me.Name = Name
            ObjectType = ObjectType
            Me.Value = Value
        End Sub
    End Class

    Protected Sub SetIsProcessing(isProcessing As Boolean)

        My.Settings.isRunning = isProcessing

    End Sub

    Protected Sub InitializeEventLog()

        If EventLog.SourceExists(LOG_SOURCE_NAME) Then EventLog.DeleteEventSource(LOG_SOURCE_NAME)

        If Not EventLog.SourceExists(LOG_SOURCE_NAME) Then
            EventLog.CreateEventSource(LOG_SOURCE_NAME, LOG_NAME)
        End If

        _eventLog = New EventLog()
        _eventLog.Log = LOG_NAME
        _eventLog.Source = LOG_SOURCE_NAME

    End Sub

    Protected Sub WriteLogs(logEntry As String, className As String, messageType As EventLogEntryType)

        AppConfig.DebugMessage.Trace("ElitaBatchServices", className, logEntry)
        _eventLog.WriteEntry(String.Format("ElitaBatchServices:  {0} : {1}", className, logEntry), messageType)

    End Sub

    Protected Function GetErrorMessages(ex As Exception) As String

        Dim strB As New Text.StringBuilder
        Dim hasInnerExc As Boolean = False

        strB.AppendLine(String.Format("Error Message:  {0}", ex.Message))
        strB.AppendLine(String.Format("StackTrace:  {0}", ex.StackTrace))

        If ex.InnerException IsNot Nothing Then hasInnerExc = True
        While hasInnerExc
            ex = ex.InnerException
            strB.AppendLine("------- Next Error -------")
            strB.AppendLine(String.Format("Error Message:  {0}", ex.Message))
            strB.AppendLine(String.Format("StackTrace:  {0}", ex.StackTrace))
            If ex.InnerException Is Nothing Then hasInnerExc = False
        End While

        Return strB.ToString

    End Function

End Class
