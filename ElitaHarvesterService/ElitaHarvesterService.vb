Imports System.ServiceProcess
Imports System.Timers
Imports System.Globalization
Imports System.Diagnostics
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Configuration
Imports Assurant.Elita.WorkerFramework
Imports Assurant.ElitaPlus.Common

Public Class ElitaHarvesterService
    Inherits ServiceBase

#If DEBUG Then
    Shared Sub New()
        Debugger.Launch()
    End Sub
#End If

    Dim pollingTimer As Timers.Timer = Nothing
    Dim pollingFrequency As Integer = 30
    Private _threadingManager As ThreadingManager

    Public Sub New()
        Logger.Initialize("ElitaHarvesterService")
        'AppConfig.Debug("testLog")
        Logger.AddInfo("ElitaHarvesterService has been started successfully")
        InitializeComponent()
        _threadingManager = New ThreadingManager()

    End Sub

    Protected Overrides Sub OnStart(args() As String)
        Try
            'Debugger.Launch()
            'Logger.Initialize("ElitaHarvesterService")
            Logger.AddInfo("ElitaHarvesterService has been started successfully")
            _threadingManager.InitializeThreads()
            WorkersManager.Current.Logger = New LoggerAdapter()
            WorkersManager.Current.Start()
        Catch ex As Exception
            StopMe()
        End Try
    End Sub

    Protected Overrides Sub OnStop()
        Try
            _threadingManager.StopThreads()
            WorkersManager.Current.Stop()
        Catch ex As Exception
            Logger.AddError(String.Format("Error While Stopping Threads: {0}", ex.Message), ex)
        End Try

        Logger.AddInfo("Stopping service")
    End Sub

    Public Sub Start(args() As String)
        Logger.AddDebugLogEnter()
        OnStart(args)
        Logger.AddDebugLogExit()

    End Sub

    Public Sub StopMe()
        OnStop()
        Logger.AddInfo("ElitaHarvetserService has been stopped successfully")
    End Sub


End Class
