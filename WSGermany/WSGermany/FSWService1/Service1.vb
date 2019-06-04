Imports System.IO

Public Class Service1

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        Me.StartService()
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        Me.StopService()
    End Sub

    Private WithEvents timer1 As New System.Timers.Timer()

    Private FilesToProcess As List(Of String) = Nothing

    Public Sub StartService()
        FilesToProcess = New List(Of String)()

        Me.FileSystemWatcher1.Path = My.Settings.DRIVE_LETTER + My.Settings.LOOKOUT_PATH
        Dim interval As Integer = Integer.Parse(My.Settings.RUN_INTERVAL) * 1000

        Me.timer1.Interval = interval



        Me.timer1.Enabled = False

    End Sub

    Private _RunningEvent As Boolean = False
    Private _RunningProcessFiles As Boolean = False

    Public Sub StopService()
        While Me._RunningEvent OrElse Me._RunningProcessFiles
        End While
    End Sub

    Private Sub fileSystemWatcher1_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles FileSystemWatcher1.Created

        Me._RunningEvent = True

        System.Diagnostics.Debug.WriteLine("File created: " + e.FullPath)

        Dim finfo As New FileInfo(e.FullPath)

        Dim file_name As String = finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length)
        Dim file_ext As String = finfo.Extension


        If file_ext.ToLower.Contains(My.Settings.EXTENSION_WITH) Then

            FilesToProcess.Add(e.FullPath)
        End If

        If FilesToProcess.Count > 0 Then
            Me.timer1.Enabled = True
        End If

        Me._RunningEvent = False

    End Sub



    Private Function FileCanBeRead(ByVal FullPath As String) As Boolean
        Try

            Dim f As New FileStream(FullPath, FileMode.Open)
            f.Close()
            f.Dispose()
            Return True
        Catch
            Return False
        End Try

    End Function

    Private Sub timer1_Tick(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timer1.Elapsed
        Me.ProcessFiles()
    End Sub


    Private Sub ProcessFiles()

        Me.timer1.Enabled = False

        Me._RunningProcessFiles = True

        Dim DeletedFiles As New List(Of String)()

        Me.MoveFiles(DeletedFiles)

        Me.MarkFilesAsProcessed(DeletedFiles)

        Me._RunningProcessFiles = False

        If Me.FilesToProcess.Count = 0 Then
            Me.timer1.Enabled = False
        Else
            Me.timer1.Enabled = True
        End If
    End Sub


    Private Sub MarkFilesAsProcessed(ByVal DeletedFiles As List(Of String))
        If DeletedFiles.Count > 0 Then
            For Each file As String In DeletedFiles
                Dim idx As Integer = -1

                For i As Integer = 0 To FilesToProcess.Count - 1
                    If file.Equals(FilesToProcess(i)) Then
                        idx = i
                        Exit For
                    End If
                Next

                If idx >= 0 Then
                    FilesToProcess.RemoveAt(idx)
                End If

            Next
        End If
    End Sub


    Private Sub MoveFiles(ByVal DeletedFiles As List(Of String))
        If FilesToProcess.Count > 0 Then
            Try

                For Each file__1 As String In FilesToProcess
                    If FileCanBeRead(file__1) Then
                        Dim overwrite As Boolean = False

                        Dim finfo As New FileInfo(file__1)

                        Dim file_name As String = finfo.Name.Substring(0, finfo.Name.Length - finfo.Extension.Length)
                        Dim file_dealer As String = finfo.Name.Substring(0, 4)
                        Dim file_year As String = finfo.Name.Substring(4, 2)
                        Dim file_month As String = finfo.Name.Substring(6, 2)
                        Dim DealerPath As String = My.Settings.DRIVE_LETTER
                        Dim count As Integer = 0

                        For Each filein As String In My.Settings.WRITE_PATH

                            If file_name.ToLower().StartsWith(My.Settings.STARTS_WITH.Item(count)) Then
                                DealerPath = My.Settings.DRIVE_LETTER + My.Settings.WRITE_PATH.Item(count) + "\" + file_dealer + "\" + "20" + file_year + file_month
                            End If
                            count = count + 1
                        Next
                        ' Create Directory
                        If Not Directory.Exists(DealerPath) Then
                            Directory.CreateDirectory(DealerPath)
                        End If

                        If My.Settings.OVERRIDE_FILES.ToLower().Equals("yes") Then
                            overwrite = True
                        End If

                        ' Copy Source File
                        Dim dir_file_name As Array = Directory.GetFiles(My.Settings.DRIVE_LETTER + My.Settings.LOOKOUT_PATH)
                        For Each dirfilein As String In dir_file_name
                            If Not dirfilein.EndsWith(My.Settings.EXTENSION_WITH) Then
                                If dirfilein.Contains(file_name) Then
                                    File.Copy(dirfilein, DealerPath + "\" + System.IO.Path.GetFileName(dirfilein), overwrite)
                                    File.Delete(dirfilein)
                                    File.Delete(My.Settings.DRIVE_LETTER + My.Settings.LOOKOUT_PATH + "\" + file_name + "." + My.Settings.EXTENSION_WITH)
                                    DeletedFiles.Add(dirfilein)
                                End If
                            End If
                        Next
                    End If
                Next
            Catch
            End Try
        End If
    End Sub




End Class
