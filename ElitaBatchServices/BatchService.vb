Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.ServiceProcess
Imports System.IO
Imports Assurant.Common
Imports Assurant.ElitaPlus
Imports System.Net
Imports System.Configuration
Imports System.Linq
Imports System.ComponentModel
Imports System.ServiceModel


Public Class BatchService
    Inherits System.ServiceProcess.ServiceBase

#Region "Local Variables and Constants"

    Private _UserName As String = String.Empty
    Private _Password As String = String.Empty
    Private _LDapGroup As String = String.Empty
    Private _MethodName As String = String.Empty
    Private _ParamSet() As ElitaMethodBase.MethodParams
    Private _UseLogFile As Boolean = False
    Private _LogDir As String = String.Empty
    Private _Async As Boolean = False
    Private _ConsoleOut As Boolean = False
    Private _HubRegion As String = String.Empty

    Protected _eventLog As EventLog = Nothing

    Private Const PARAM_USERNAME As String = "USERNAME"
    Private Const PARAM_PASSWORD As String = "PASSWORD"
    Private Const PARAM_GROUP As String = "GROUP"
    Private Const PARAM_METHODNAME As String = "METHODNAME"
    Private Const PARAM_ASYNC As String = "/:ASYNC"
    Private Const PARAM_CONSOLEOUTPUT As String = "/:CONSOLEOUTPUT"
    Private Const PARAM_HELP As String = "/:?"
    Private Const PARAM_DEBUG As String = "/:DEBUG"

    Private Const METHOD_EXECUTE As String = "Execute"
    Private Const METHOD_EXECUTEASYNC As String = "ExecuteAsync"
    Private Const METHOD_SETDS As String = "SetDS"

    Private Const MSG_REQ_ALREADY_EXISTS As String = "Call already exists in Queue for processing"
    Private Const MSG_REQ_ADDED_TO_QUEUE As String = "Call added to Queue for processing"

    Public Const SERVICE_NAME As String = "ElitaBatchServices"
    Public Const LOG_SOURCE_NAME As String = "ElitaBatchServicesApp"
    Private LOG_NAME As String = "ElitaPlus"

    Public sHostAcct As ServiceHost = Nothing
    Public sHostGVS As ServiceHost = Nothing
    Public sHostTEST As ServiceHost = Nothing

    Protected PendingTimer As Timers.Timer

    'Public PendingRequests As List(Of PendingRequest)
    Public Class PendingRequest

        Public networkID As String
        Public password As String
        Public LDAPGroup As String
        Public functionToProcess As String
        Public xmlStringDataIn As String
        Public isAsync As Boolean
        Public RequestTime As DateTime = Now
        Public hubRegion As String

    End Class

#End Region



#Region "Initialization"

#Region "Designer Code"
    'UserService overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  Do not modify it
    ' using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        ServiceName = SERVICE_NAME
    End Sub

#End Region
    Private Sub InitializeEventLog()

        'If EventLog.SourceExists(LOG_SOURCE_NAME) Then EventLog.DeleteEventSource(LOG_SOURCE_NAME)

        If Not EventLog.SourceExists(LOG_SOURCE_NAME) Then
            EventLog.CreateEventSource(LOG_SOURCE_NAME, LOG_NAME)
        End If

        _eventLog = New EventLog()
        _eventLog.Log = LOG_NAME
        _eventLog.Source = LOG_SOURCE_NAME

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Shared Sub Main()

        Try
            Instrumentation.WriteLog("BEGIN")

            Dim ServicesToRun As System.ServiceProcess.ServiceBase()
            ServicesToRun = New System.ServiceProcess.ServiceBase() {New ElitaBatchServices.BatchService()}
            System.ServiceProcess.ServiceBase.Run(ServicesToRun)

            Instrumentation.WriteLog("FINISHED")
        Catch ex As Exception
            Instrumentation.WriteLog("FAILED")
            Dim _eventLog As New EventLog()
            _eventLog.Log = "ElitaPlus"
            _eventLog.Source = "ElitaBatchServicesApp"
            _eventLog.WriteEntry(ex.Message + "|" + ex.StackTrace, EventLogEntryType.Error)

        End Try

    End Sub

    Protected Overrides Sub OnStart(args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.


        Try
            Instrumentation.WriteLog("BEGIN")

            InitializeEventLog()

            Instrumentation.WriteLog("InitializeEventLog Done")

            If sHostAcct IsNot Nothing Then sHostAcct.Close()
            sHostAcct = New ServiceHost(GetType(AccountingWS))
            sHostAcct.Open()
            Instrumentation.WriteLog("BatchService : AccountingWS : INITIALISED")

            If sHostGVS IsNot Nothing Then sHostGVS.Close()
            sHostGVS = New ServiceHost(GetType(GVSInterfaceHistoryWS))
            sHostGVS.Open()

            Instrumentation.WriteLog("BatchService : GVSInterfaceHistoryWS : INITIALISED")

            If sHostTEST IsNot Nothing Then sHostTEST.Close()
            sHostTEST = New ServiceHost(GetType(TestService))
            sHostTEST.Open()

            Instrumentation.WriteLog("BatchService : TestService : INITIALISED")

            _eventLog.WriteEntry(String.Format("ElitaBatchServices Starting {0}", Now.ToString), EventLogEntryType.Information)

            Instrumentation.WriteLog("FINISHED")

        Catch ex As Exception
            Instrumentation.WriteLog("FAILED with Exception : " & ex.Message)
            _eventLog.WriteEntry(GetErrors(ex, ex.Message))
        End Try

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        Try
            Instrumentation.WriteLog("BatchService : OnStop : BEGIN")

            If Not PendingTimer Is Nothing Then PendingTimer.Dispose()

            If sHostAcct IsNot Nothing Then
                sHostAcct.Close()
                sHostAcct = Nothing
            End If

            If sHostGVS IsNot Nothing Then
                sHostGVS.Close()
                sHostGVS = Nothing
            End If

            If sHostTEST IsNot Nothing Then
                sHostTEST.Close()
                sHostTEST = Nothing
            End If

            _eventLog.WriteEntry(String.Format("ElitaBatchServices Stopping {0}", Now.ToString), EventLogEntryType.Information)

            Instrumentation.WriteLog("FINISHED")

        Catch ex As Exception
            Instrumentation.WriteLog("FAILED with Exception : " & ex.Message)
            _eventLog.WriteEntry(GetErrors(ex, ex.Message), EventLogEntryType.Error)
        End Try

    End Sub

    Protected Sub EnableTimer()

        Instrumentation.WriteLog("BEGIN")

        If PendingTimer Is Nothing OrElse PendingTimer.Enabled = False Then

            PendingTimer = New Timers.Timer
            PendingTimer.Interval = My.Settings.TimerInterval
            PendingTimer.Enabled = True
            AddHandler PendingTimer.Elapsed, AddressOf TimerElapsed

            PendingTimer.Start()

        End If

        Instrumentation.WriteLog("END")

    End Sub

    Protected Sub DisableTimer()

        Instrumentation.WriteLog("BEGIN")

        PendingTimer.Enabled = False
        PendingTimer.Stop()
        PendingTimer.Dispose()

        Instrumentation.WriteLog("END")

    End Sub
#End Region

#Region "Processing"

    Public Function ProcessRequest(networkID As String, _
                              password As String, _
                              LDAPGroup As String, _
                              functionToProcess As String, _
                              xmlStringDataIn As String, _
                              hubRegion As String, _
                              isAsync As Boolean, _
                              Optional ByVal CalledFromQueue As Boolean = False) As String

        Try
            Instrumentation.WriteLog("STARTED : networkID :  " & networkID & ", password  " & password & ", LDAPGroup  " & LDAPGroup & ", functionToProcess  " & functionToProcess & ", xmlStringDataIn  " & xmlStringDataIn & ", hubRegion  " & hubRegion & ", isAsync  " & isAsync & ", CalledFromQueue  " & CalledFromQueue)

            If _eventLog Is Nothing Then InitializeEventLog()

            _eventLog.WriteEntry(String.Format("ElitaBatchServices ProcessRequest: User: {0}, Function: {1}, XMLString: {2}", networkID, functionToProcess, xmlStringDataIn), EventLogEntryType.Information)

            If My.Settings.isRunning Then

                'If we are calling from the queue and it is already running, return false
                If CalledFromQueue Then
                    Instrumentation.WriteLog("CalledFromQueue")
                    Return "false"
                End If

                Dim pr As PendingRequest
                If My.Settings.PendingRequests Is Nothing Then My.Settings.PendingRequests = New Queue

                For Each pr In My.Settings.PendingRequests

                    Instrumentation.WriteLog("Pending Request : xmlStringDataIn : ---------------------------")
                    Instrumentation.WriteLog(xmlStringDataIn)
                    Instrumentation.WriteLog("Pending Request : pr.xmlStringDataIn : ------------------------")
                    Instrumentation.WriteLog(pr.xmlStringDataIn)
                    Instrumentation.WriteLog("---------------------------------------------------------------")

                    If pr.xmlStringDataIn = xmlStringDataIn Then
                        _eventLog.WriteEntry(String.Format("ElitaBatchServices ProcessRequest Completed: {0} : {1}", MSG_REQ_ALREADY_EXISTS, xmlStringDataIn), EventLogEntryType.Information)
                        Return MSG_REQ_ALREADY_EXISTS
                    End If
                Next

                pr = New PendingRequest
                With pr
                    .networkID = networkID
                    .password = password
                    .LDAPGroup = LDAPGroup
                    .functionToProcess = functionToProcess
                    .xmlStringDataIn = xmlStringDataIn
                    .isAsync = False
                    .hubRegion = hubRegion
                End With
                'Queue the pending requests
                My.Settings.PendingRequests.Enqueue(pr)

                'Start the timer
                EnableTimer()

                _eventLog.WriteEntry(String.Format("ElitaBatchServices ProcessRequest: {0} : {1}", MSG_REQ_ADDED_TO_QUEUE, xmlStringDataIn), EventLogEntryType.Information)
                Return MSG_REQ_ADDED_TO_QUEUE

            End If

            'Set params
            _UserName = networkID
            _Password = password
            _LDapGroup = LDAPGroup
            _MethodName = functionToProcess
            _Async = isAsync
            _HubRegion = hubRegion

            'Attempt a login.
            If Not _UserName.Equals(String.Empty) AndAlso Not _Password.Equals(String.Empty) AndAlso Not _LDapGroup.Equals(String.Empty) Then

                If VerifyLogin(_UserName, _Password, _LDapGroup, _HubRegion) Then
                    Return ProcessMethod(xmlStringDataIn.Trim)
                Else
                    Instrumentation.WriteLog("Login Failed. _UserName : " & _UserName & ", _Password : " & _Password & ", _LDapGroup : " & _LDapGroup & ", _HubRegion : " & _HubRegion)
                    Throw New ElitaBatchProcessingException("Login Failed.", "")
                End If
            Else
                Instrumentation.WriteLog("Credentials Not entered completely. _UserName : " & _UserName & ", _Password : " & _Password & ", _LDapGroup : " & _LDapGroup & ", _HubRegion : " & _HubRegion)
                Throw New ElitaBatchProcessingException("Credentials Not entered completely.", "")
            End If

            Instrumentation.WriteLog("FINISHED")

        Catch ex As Exception

            Instrumentation.WriteLog("FAILED with Exception : " & ex.Message)

            _eventLog.WriteEntry(GetErrors(ex, ex.Message), EventLogEntryType.Error)
            Return False
        Finally
            _eventLog.WriteEntry("ElitaBatchServices ProcessRequest Completed", EventLogEntryType.Information)
        End Try
    End Function

    Function ProcessMethod(Optional ByVal xmlDataIn As String = "") As String
        Try

            Instrumentation.WriteLog("STARTED with xmlDataIn : -----------------------")
            Instrumentation.WriteLog(xmlDataIn)
            Instrumentation.WriteLog("------------------------------------------------")
            Instrumentation.WriteLog("_MethodName : " & _MethodName)

            Dim _typeObj As Type = Type.GetType("ElitaBatchServices." + _MethodName)

            Instrumentation.WriteLog("_typeObj initialised")

            _eventLog.WriteEntry("ElitaBatchServices ProcessMethod", EventLogEntryType.Information)

            If Not _typeObj Is Nothing Then
                Dim _Obj As Object = Activator.CreateInstance(_typeObj)
                Instrumentation.WriteLog("_Obj initialised")
                Dim _prop As System.Reflection.PropertyInfo
                Instrumentation.WriteLog("_prop initialised")
                If Not _ParamSet Is Nothing Then
                    For Each _param As ElitaMethodBase.MethodParams In _ParamSet
                        _prop = _typeObj.GetProperty(_param.Name)
                        Instrumentation.WriteLog("_prop initialised with _param.Name : " & _param.Name)
                        If Not _prop Is Nothing Then
                            _prop.SetValue(_Obj, _param.Value, Nothing)
                            Instrumentation.WriteLog("_prop initialised with _param.Value : " & _param.Value)
                        End If
                    Next
                End If

                Dim _method As System.Reflection.MethodInfo
                Instrumentation.WriteLog("_method initialised")

                If Not xmlDataIn.Equals(String.Empty) Then
                    _method = _typeObj.GetMethod(METHOD_SETDS)
                    Instrumentation.WriteLog("xmlDataIn is empty : _typeObj.GetMethod(METHOD_SETDS) done")
                    _method.Invoke(_Obj, New Object() {xmlDataIn})
                    Instrumentation.WriteLog("xmlDataIn is empty : _method.Invoke done")
                End If

                _method = _typeObj.GetMethod(If(_Async, METHOD_EXECUTEASYNC, METHOD_EXECUTE))
                Instrumentation.WriteLog("_typeObj.GetMethod(If(_Async, METHOD_EXECUTEASYNC, METHOD_EXECUTE)) done")
                Dim obj As Object = _method.Invoke(_Obj, Nothing).ToString()
                Instrumentation.WriteLog("_method.Invoke(_Obj, Nothing).ToString() done")
                Return obj
            Else
                Instrumentation.WriteLog("Invalid Method Name.")
                Throw New ElitaBatchProcessingException("Invalid Method Name.", "")
            End If

            Instrumentation.WriteLog("FINISHED")
        Catch ex As Exception
            Instrumentation.WriteLog("FAILED WITH EXCEPTION : " & ex.Message)
            Throw
        End Try
    End Function

    Function GetErrors(ex As Exception, Message As String) As String

        Instrumentation.WriteLog("BEGIN")

        Message += String.Format("Message: {0}, StackTrace {1}", ex.Message, ex.StackTrace)
        Message += Environment.NewLine

        If ex.InnerException Is Nothing Then
            Return Message
        Else
            Return GetErrors(ex.InnerException, Message)
        End If

        Instrumentation.WriteLog("END")

    End Function


    ''' <summary>
    ''' Called by timer to run pending requests
    ''' </summary>
    ''' <remarks></remarks>
    Sub ProcessPendingRequests()

        Instrumentation.WriteLog("BEGIN")

        If My.Settings.isRunning OrElse My.Settings.PendingRequests Is Nothing OrElse My.Settings.PendingRequests.Count = 0 Then Exit Sub

        Dim p As PendingRequest
        p = My.Settings.PendingRequests.Dequeue
        With p
            Dim ret As Object
            ret = ProcessRequest(.networkID, .password, .LDAPGroup, .functionToProcess, .xmlStringDataIn, .isAsync, True)
            If ret.Equals("false") Then
                My.Settings.PendingRequests.Enqueue(p)
            End If
        End With

        Instrumentation.WriteLog("END")
    End Sub

    Public Sub TimerElapsed(source As Object, e As EventArgs)

        If Not My.Settings.PendingRequests Is Nothing AndAlso My.Settings.PendingRequests.Count > 0 Then
            If Not My.Settings.isRunning Then
                _eventLog.WriteEntry("ElitaBatchServices Processing from Timer", EventLogEntryType.Information)
                ProcessPendingRequests()
            End If
        Else
            DisableTimer()
        End If
    End Sub

#End Region

#Region "Authentication"

    Public Function VerifyLogin(networkId As String, appPassword As String, group As String, hubRegion As String) As Boolean
        Dim isValidUser As Boolean = False

        Try

            Instrumentation.WriteLog("VerifyLogin BEGIN : networkId : " & networkId & ", appPassword : " & appPassword & ", group : " & group & ", hubRegion : " & hubRegion)
            LoginElita(networkId, hubRegion)
            isValidUser = Assurant.ElitaPlus.BusinessObjectsNew.Authentication.IsLdapUser(group, networkId, appPassword)
            Instrumentation.WriteLog("VerifyLogin END : networkId : " & networkId & ", appPassword : " & appPassword & ", group : " & group & ", hubRegion : " & hubRegion)
        Catch ex As Exception
            Instrumentation.WriteLog("VerifyLogin FAILED : networkId : " & networkId & ", appPassword : " & appPassword & ", group : " & group & ", hubRegion : " & hubRegion)
            Throw New ElitaBatchProcessingException("Login Failed.", "", ex)
        End Try

        Return isValidUser

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Login in Elita and updates the Principal
    ''' </summary>
    ''' <param name="networkID"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub LoginElita(networkID As String, hubRegion As String)

        Try
            Instrumentation.WriteLog("LoginElita BEGIN : networkId : " & networkID & ", hubRegion : " & hubRegion)

            Dim oAuthentication As New Assurant.ElitaPlus.BusinessObjectsNew.Authentication

            '' Find Machine Prefix
            Dim environment As String = EnvironmentContext.Current.EnvironmentShortName
            Instrumentation.WriteLog("environment : " & environment)
            Dim hostName As String = oAuthentication.ApplicationHost
            Instrumentation.WriteLog("hostName : " & hostName)
            Dim hubRegionCode As String = oAuthentication.GetHubRegion(hubRegion, hostName)
            Instrumentation.WriteLog("hubRegionCode : " & hubRegionCode)
            Dim appConfigKey As String = New System.Text.StringBuilder().AppendFormat("{0}_DOMAIN_{1}_{2}", "MANUAL", environment.ToUpper, hubRegionCode.ToUpper).ToString()
            Instrumentation.WriteLog("appConfigKey : " & appConfigKey)
            Dim machineDomain As String = Nothing
            machineDomain = System.Configuration.ConfigurationManager.AppSettings(appConfigKey)
            Instrumentation.WriteLog("machineDomain : " & machineDomain)

            oAuthentication.CreatePrincipal(networkID, , , hubRegion, machineDomain)
            Instrumentation.WriteLog("oAuthentication created")

            Assurant.ElitaPlus.BusinessObjectsNew.Authentication.SetCulture()

            Instrumentation.WriteLog("LoginElita DONE : networkId : " & networkID & ", hubRegion : " & hubRegion)
        Catch ex As Exception
            Instrumentation.WriteLog("---------------------------------------------------------------------------")
            Instrumentation.WriteLog("LoginElita FAILED : networkId : " & networkID & ", hubRegion : " & hubRegion)
            Instrumentation.WriteLog("Exception Message : " & ex.Message)
            Instrumentation.WriteLog("Exception Stack Trace : " & ex.StackTrace)
            Instrumentation.WriteLog("---------------------------------------------------------------------------")
            Throw
        End Try
    End Sub
#End Region

End Class
