Imports System.Collections.Generic
Imports System.Text
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.Environment

'' Responsibilities - Read Configurations and Create Threads
''                  - Start Threads
''                  - Stop Threads

Public Class ThreadingManager

    Private ReadOnly _threads As List(Of ThreadBase)
    Private numberOfRunningThreads As Integer
    Private startTime As DateTime

    Public Sub New()
        _threads = New List(Of ThreadBase)()
    End Sub

    Public ReadOnly Property Threads As List(Of ThreadBase)
        Get
            Return _threads
        End Get
    End Property

    Public Sub InitializeThreads()
        Logger.AddDebugLogEnter()
        Dim currentEnvironment As String = EnvironmentContext.Current.EnvironmentName.ToUpperInvariant()
        startTime = DateTime.Now

        ' Read From Config the value of Environments and add Threads to Threads List
        For Each tce As ThreadConfigElement In HarvesterSection.Current.Harvesters
            If (tce.Environment.ToUpperInvariant().Equals(currentEnvironment)) Then
                If (tce.GetType().Equals(GetType(ThreadEnvironmentConfigElement))) Then
                    Dim tece As ThreadEnvironmentConfigElement = DirectCast(tce, ThreadEnvironmentConfigElement)
                    Dim et As New EnvironmentThread(tece)
                    _threads.Add(et)
                End If
            End If
        Next

        ' Start Threads
        StartThreads()
        Logger.AddDebugLogExit()
    End Sub

    Public Sub StopThreads()
        Logger.AddDebugLogEnter()
        Try
            For Each thread As ThreadBase In Threads
                thread.ThreadStop()
            Next
        Catch ex As Exception
            Logger.AddError(ex)
        End Try
        Logger.AddDebugLogExit()
    End Sub

    Private Sub StartThreads()
        Logger.AddDebugLogEnter()
        For Each thread As ThreadBase In Threads
            thread.ThreadStart(True)
        Next
        Logger.AddDebugLogExit()
    End Sub

    Private Function AreAllThreadsRunning() As Boolean
        Dim enumerator As IEnumerator(Of ThreadBase) = Threads.GetEnumerator()
        Dim allThreadsRunning As Boolean = True

        While (enumerator.MoveNext())
            If (enumerator.Current.IsAlive = False) Then
                allThreadsRunning = False
                Exit While
            End If
        End While

        If (numberOfRunningThreads <> (Threads.Count - 1)) Then
            allThreadsRunning = False
        End If

        Return allThreadsRunning

    End Function

    Private Function GetAllThreadStates() As String
        Dim sb As StringBuilder = New StringBuilder()
        Dim enumerator As IEnumerator(Of ThreadBase) = Threads.GetEnumerator()

        While (enumerator.MoveNext())
            sb.Append(enumerator.Current.Name + ":" + Convert.ToString(enumerator.Current.State + "|"))
        End While
        Return sb.ToString()

    End Function

    Private Function ServiceUpTime() As String
        Return DateTime.Now.Subtract(startTime).ToString()
    End Function

    Private Function GetNumberOfThreads() As String
        Return Convert.ToString(Threads.Count)
    End Function


End Class
