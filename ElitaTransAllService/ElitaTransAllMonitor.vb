Imports System.ServiceProcess
Imports System.IO
Imports Assurant.Common
Imports Assurant.ElitaPlus
Imports System.Net
Imports System.Configuration


Public Class ElitaTransAllMonitor
    Inherits System.ServiceProcess.ServiceBase

#Region "Private Variables"

    Private Const CONFIG_FILE_EXTENSION As String = "FILE_EXTENSION"
    Private Const CONFIG_MONITOR_IN_DIRECTORY As String = "MONITOR_IN_DIRECTORY"
    Private Const CONFIG_MONITOR_OUT_DIRECTORY As String = "MONITOR_OUT_DIRECTORY"
    Private Const CONFIG_COMMAND_LINE As String = "COMMAND_LINE"

    Private MONITOR_IN_DIRECTORY As String
    Private MONITOR_OUT_DIRECTORY As String
    Private watcherInbound As FileSystemWatcher
    Private watcherOutbound As FileSystemWatcher

    Private Const PROCESS_EXT As String = ".prc"

#End Region

#Region "Initialization and Completion"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Shared Sub Main()

        Dim ServicesToRun As System.ServiceProcess.ServiceBase()
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New ElitaTransAllMonitor()}
        System.ServiceProcess.ServiceBase.Run(ServicesToRun)

    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.

        Try

            If Not Login() Then
                Diagnostics.EventLog.WriteEntry(Me.ServiceName, "LOGIN FAILED", EventLogEntryType.Error)
                Me.Stop()
                Exit Sub
            Else
                Diagnostics.EventLog.WriteEntry(Me.ServiceName, "LOGIN SUCCESSFUL", EventLogEntryType.SuccessAudit)
            End If

            MONITOR_IN_DIRECTORY = ConfigurationManager.AppSettings(CONFIG_MONITOR_IN_DIRECTORY)
            MONITOR_OUT_DIRECTORY = ConfigurationManager.AppSettings(CONFIG_MONITOR_OUT_DIRECTORY)
            StartMonitoring()

        Catch ex As Exception
            Diagnostics.EventLog.WriteEntry(Me.ServiceName, ex.Message + "|" + ex.StackTrace, EventLogEntryType.Error)
        End Try

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        Try
            StopMonitoring()

        Catch ex As Exception

        End Try

    End Sub


#End Region

#Region "Application Logic"

    Private Sub LaunchTransAll(ByVal dv As DataView, ByVal FileName As String)

        Dim strCommand As String
        Dim _Proc = New Process

        'Before starting process, check to be sure output directory exists.  If not, create it so Transall doesn't break
        Dim dr As New DirectoryInfo(String.Format("{0}\{1}", ConfigurationManager.AppSettings(CONFIG_MONITOR_OUT_DIRECTORY).Trim("\"), dv(0)(DALObjects.TransAllMappingDAL.COL_NAME_OUTPUT_PATH).ToString.Trim("\")))
        If Not dr.Exists Then
            dr.Create()
        End If

        strCommand = ConfigurationManager.AppSettings(CONFIG_COMMAND_LINE)

        _Proc.StartInfo = New ProcessStartInfo(strCommand)
        _Proc.StartInfo.Arguments = String.Format("""{0}"" Startup ""{1}"" ""{2}\{3}""", dv(0)(DALObjects.TransAllMappingDAL.COL_NAME_TRANSALL_PACKAGE).ToString, _
                                   FileName, _
                                   ConfigurationManager.AppSettings(CONFIG_MONITOR_OUT_DIRECTORY).Trim("\"), _
                                   dv(0)(DALObjects.TransAllMappingDAL.COL_NAME_OUTPUT_PATH).ToString.Trim("\"))

        AddHandler _Proc.Exited, AddressOf ProcessCompleted

        _Proc.EnableRaisingEvents = True
        _Proc.Start()

    End Sub

    Public Sub StartMonitoring()

        watcherInbound = New FileSystemWatcher
        watcherOutbound = New FileSystemWatcher

        'Set the path to monitor from the appsettings
        watcherInbound.Path = MONITOR_IN_DIRECTORY
        watcherOutbound.Path = MONITOR_OUT_DIRECTORY

        'Set the watcher to monitor subdirectories
        watcherInbound.IncludeSubdirectories = True
        watcherOutbound.IncludeSubdirectories = True

        AddHandler watcherInbound.Created, AddressOf watcherIn_FileCreated
        AddHandler watcherOutbound.Created, AddressOf watcherOut_FileCreated

        'Enable event handling
        watcherInbound.EnableRaisingEvents = True
        watcherOutbound.EnableRaisingEvents = True

    End Sub

    Public Sub StopMonitoring()

        Try

            watcherInbound.EnableRaisingEvents = False
            watcherInbound.Dispose()

            watcherOutbound.EnableRaisingEvents = False
            watcherOutbound.Dispose()

        Finally
        End Try

    End Sub

#End Region

#Region "Events"

    Private Sub watcherIn_FileCreated(ByVal sender As Object, ByVal e As FileSystemEventArgs)

        Dim fil As New FileInfo(e.FullPath)

        If fil.Exists AndAlso Not fil.IsReadOnly AndAlso fil.Length > 0 Then

            'Check if new file has the correct extension.  If not, exit monitoring instance
            If Not ConfigurationManager.AppSettings(CONFIG_FILE_EXTENSION).Contains(fil.Extension.ToLower) Then
                Exit Sub
            End If

            Try
                'Find file path / pattern in dataset and run transall command
                Dim dv As DataView = BusinessObjectsNew.TransallMapping.GetList(fil.Name)
                If dv.Count = 1 Then

                    'If only 1 file is needed for this client, call transall with the file
                    If dv(0)(DALObjects.TransallMappingDAL.COL_NAME_NUM_FILES) = 1 Then
                        If File.Exists(fil.FullName + PROCESS_EXT) Then
                            File.Delete(fil.FullName + PROCESS_EXT)
                            'File.Replace(fil.FullName + PROCESS_EXT, fil.FullName + PROCESS_EXT, fil.Name + "_" + Now.TimeOfDay.Seconds.ToString + ".bak")
                        End If
                        fil.MoveTo(fil.FullName + PROCESS_EXT)

                        LaunchTransAll(dv, fil.FullName)

                    Else  'multiple files are needed for this client.  Check to see if they are all there

                        Dim dir As New DirectoryInfo(fil.DirectoryName)
                        Dim fils() As FileInfo

                        fils = dir.GetFiles(dv(0)(DALObjects.TransAllMappingDAL.COL_NAME_INBOUND_FILENAME))
                        If fils.Count = dv(0)(DALObjects.TransAllMappingDAL.COL_NAME_NUM_FILES) Then
                            Dim FileName As String = ""
                            For Each tmpFile As FileInfo In fils
                                If File.Exists(tmpFile.FullName + PROCESS_EXT) Then
                                    File.Delete(tmpFile.FullName + PROCESS_EXT)
                                    'File.Replace(tmpFile.FullName + PROCESS_EXT, tmpFile.FullName + PROCESS_EXT, tmpFile.Name + "_" + Now.TimeOfDay.Seconds.ToString + ".bak")
                                End If
                                tmpFile.MoveTo(tmpFile.FullName + PROCESS_EXT)

                                FileName += tmpFile.FullName + " "
                            Next

                            LaunchTransAll(dv, FileName)

                        End If

                    End If
                End If
            Catch ex As Exception
                Diagnostics.EventLog.WriteEntry(Me.ServiceName, String.Format("Error Processing Incoming files: {3} {1} {1} {0} {1} {1} {2}", ex.Message, Environment.NewLine, ex.StackTrace, e.FullPath), EventLogEntryType.Error)
            End Try

        End If


    End Sub

    Protected Sub watcherOut_FileCreated(ByVal sender As Object, ByVal e As FileSystemEventArgs)

        Dim fil As New FileInfo(e.FullPath)

        If fil.Exists AndAlso Not fil.IsReadOnly AndAlso fil.Length > 0 Then

            Try

                'Find file information by directory
                'Find file path / pattern in dataset and run transall command
                Dim dv As DataView = BusinessObjectsNew.TransallMapping.GetListByDirectory(fil.DirectoryName.Substring(fil.DirectoryName.LastIndexOf("\")))
                If dv.Count = 1 Then

                    If fil.Name.Contains("ReportLog") Then
                        ProcessLogs(fil.FullName, dv)
                    ElseIf fil.Name.Contains("RejectRecords") Then
                        ProcessRejects(fil.FullName, dv)
                    Else
                        ProcessOutput(fil.FullName, dv)
                    End If

                    fil.Delete()

                End If

            Catch ex As Exception
                Diagnostics.EventLog.WriteEntry(Me.ServiceName, String.Format("Error Processing Outgoing files: {3} {1} {0} {1} {2}", ex.Message, Environment.NewLine, ex.StackTrace, e.FullPath), EventLogEntryType.Error)
            End Try

        End If

    End Sub

    ''' <summary>
    ''' ProcessCompleted
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Used for cleaning up the processed files once TransAll Completes</remarks>
    Private Sub ProcessCompleted(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim myProcess As Process = DirectCast(sender, Process)
        Dim files() As String = myProcess.StartInfo.Arguments.Split(" ")
        Dim fileName As FileInfo

        For Each fil As String In files
            Try
                If fil.EndsWith(ElitaTransAllMonitor.PROCESS_EXT) Then
                    fileName = New FileInfo(fil)
                    If fileName IsNot Nothing Then
                        fileName.Delete()
                    End If
                End If
               
            Catch ex As Exception
                Diagnostics.EventLog.WriteEntry(Me.ServiceName, String.Format("Error Removing Processed files: {3} {1} {1} {0} {1} {1} {2}", ex.Message, Environment.NewLine, ex.StackTrace, myProcess.StartInfo.Arguments), EventLogEntryType.Error)
            End Try
        Next

    End Sub

    Protected Sub ProcessOutput(ByVal fileName As String, ByVal dv As DataView)


        Dim fil As New FileInfo(fileName)
        Dim _uri As Uri

        Try

            'Dim wftp As FtpWebRequest = WebRequest.Create("ftp://" + Common.AppConfig.UnixServer.HostName + "/../.." + Common.AppConfig.UnixServer.FtpDirectory + "/" + fil.Name)
            _uri = New Uri("ftp://" + Common.AppConfig.UnixServer.HostName + "/%2e%2e/%2e%2e" + Common.AppConfig.UnixServer.FtpDirectory + "/" + fil.Name, UriKind.Absolute)

            Dim wftp As FtpWebRequest = WebRequest.Create(_uri)
            wftp.Method = WebRequestMethods.Ftp.UploadFile
            wftp.Credentials = New NetworkCredential(Common.AppConfig.UnixServer.UserId, Common.AppConfig.UnixServer.Password)

            Dim bFile() As Byte = System.IO.File.ReadAllBytes(fil.FullName)
            Dim str As System.IO.Stream = wftp.GetRequestStream
            str.Write(bFile, 0, bFile.Length)
            str.Close()
            str.Dispose()

            ''Create and send trigger file.
            Dim Buffer As New MemoryStream
            Dim fileData() As Byte = System.Text.Encoding.UTF8.GetBytes(BusinessObjectsNew.LookupListNew.GetCodeFromId(BusinessObjectsNew.LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_TRANSALL_LAYOUT_CODES, BusinessObjectsNew.ElitaPlusIdentity.Current.ActiveUser.LanguageId), Common.GuidControl.ByteArrayToGuid(dv(0)(DALObjects.TransAllMappingDAL.COL_NAME_LAYOUT_CODE))).ToLower)
            Buffer.Write(fileData, 0, fileData.Length)

            ''Upload the file.
            _uri = New Uri("ftp://" + Common.AppConfig.UnixServer.HostName + "/%2e%2e/%2e%2e" + Common.AppConfig.UnixServer.FtpDirectory + "/" + fil.Name.Replace(".txt", ""), UriKind.Absolute)
            wftp = WebRequest.Create(_uri)

            wftp.Method = WebRequestMethods.Ftp.UploadFile
            wftp.Credentials = New NetworkCredential(Common.AppConfig.UnixServer.UserId, Common.AppConfig.UnixServer.Password)

            str = wftp.GetRequestStream
            str.Write(fileData, 0, fileData.Length)
            str.Close()
            str.Dispose()


        Catch ex As Exception

            fil.MoveTo(fil.FullName + String.Format(".err_{0}", Now.ToString("yyyyMMddhhmmss")))
            Throw ex

        End Try

    End Sub

    Private Sub ProcessRejects(ByVal fileName As String, ByVal dv As DataView)

        Dim _mail As New System.Net.Mail.MailMessage
        Dim _ser As New System.Net.Mail.SmtpClient(Common.AppConfig.ServiceOrderEmail.SmtpServer)
        Dim _recips() As String = dv(0)(DALObjects.TransAllMappingDAL.COL_NAME_LOGFILE_EMAILS).ToString.Split(",")
        Dim _fil As New FileInfo(fileName)

        Try

            _mail.To.Add("esc_analysts@assurant.com")

            'TODO - Change this to something translatable
            _mail.Subject = "TRANSALL FILES REJECTS"
            _mail.From = New Mail.MailAddress("esc_analysts@assurant.com")
            _mail.Attachments.Add(New Mail.Attachment(_fil.FullName))
            _ser.Send(_mail)

        Catch ex As Exception
            _fil.MoveTo(_fil.FullName + String.Format(".err_{0}", Now.ToString("yyyyMMddhhmmss")))
            Throw ex
        End Try

    End Sub

    Private Sub ProcessLogs(ByVal fileName As String, ByVal dv As DataView)

        Dim _mail As New System.Net.Mail.MailMessage
        Dim _ser As New System.Net.Mail.SmtpClient(Common.AppConfig.ServiceOrderEmail.SmtpServer)
        Dim _recips() As String = dv(0)(DALObjects.TransAllMappingDAL.COL_NAME_LOGFILE_EMAILS).ToString.Split(",")
        Dim _fil As New FileInfo(fileName)

        Try
            For Each _recip As String In _recips
                If Not _recip.Contains("@") Then
                    _recip = _recip + "@assurant.com"
                End If
                _mail.To.Add(_recip)
            Next

            'TODO - Change this to something translatable
            _mail.Subject = "TRANSALL FILES LOADED"
            _mail.From = New Mail.MailAddress("esc_analysts@assurant.com")
            _mail.Attachments.Add(New Mail.Attachment(_fil.FullName))
            _ser.Send(_mail)

        Catch ex As Exception
            _fil.MoveTo(_fil.FullName + String.Format(".err_{0}", Now.ToString("yyyyMMddhhmmss")))
            Throw ex
        End Try

    End Sub

#End Region

#Region "AUTHENTICATION"

    Private Function Login() As Boolean

        Try
            'DELETE THIS LINE AND ENABLE THE OTHER LOGIN
            LoginElita("CO1G4T")

            'LoginElita(networkId)
            'isValidUser = Assurant.ElitaPlus.BusinessObjectsNew.Authentication.IsLdapUser(group, appId, appPassword)
            Return True

        Catch ex As Exception
            Return False
        End Try
       
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
    ''' ------------------------------sc-----------------------------------------------
    Private Sub LoginElita(ByVal networkID As String)

        Dim oAuthentication As New Assurant.ElitaPlus.BusinessObjectsNew.Authentication

        oAuthentication.CreatePrincipal(networkID)
        Assurant.ElitaPlus.BusinessObjectsNew.Authentication.SetCulture()

    End Sub

#End Region
End Class
