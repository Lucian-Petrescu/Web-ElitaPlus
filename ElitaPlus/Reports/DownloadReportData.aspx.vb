Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.DownLoad

Namespace Reports
    Public Class DownloadReportData
        Inherits ElitaPlusPage

#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            Public IsACopy As Boolean
            Public CompanyGroupIdId As Guid
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public ForEdit As Boolean = False
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object


        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            ErrorCtrl.Clear_Hide()
            Try

                If Not String.IsNullOrEmpty(Request.QueryString("rid")) Then

                    Dim dt As DataTable = ReportRequests.LoadRequests(Request.QueryString("rid"), ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                    If dt.Rows.Count > 0 Then
                        InstallReportViewer()
                        statusLabel.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_THE_REPORT_IS_BEING_DOWNLOADED)


                        TransferFilesUnixWebServer(dt.Rows(0)("ftp_filename").ToString())
                    Else
                        Throw New GUIException(Assurant.ElitaPlus.ElitaPlusWebApp.Message.MSG_INVALID_USER_ID, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_USER_ID_ERR)

                    End If

                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try


        End Sub

        Private Sub TransferFilesUnixWebServer(fileName As String)

            Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, AppConfig.UnixServer.FtpDirectory.Replace("/ftp", "/data"), AppConfig.UnixServer.UserId,
                               AppConfig.UnixServer.Password)

            Try
                System.Threading.Thread.CurrentThread.Sleep(10000)
                Dim userPathWebServer As String = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)

                CreateFolder(userPathWebServer)

                Dim destinationFile As String = userPathWebServer & "\" & fileName
                objUnixFTP.DownloadFile(fileName, destinationFile)
                System.Threading.Thread.CurrentThread.Sleep(5000)

                Dim zipFile As String
                zipFile = userPathWebServer & "_reportdata"
                Assurant.Common.Zip.aZip.CreateZipFile(zipFile, userPathWebServer, False, Nothing)
                System.IO.Directory.Delete(userPathWebServer, True)
                Dim zipName As String = System.IO.Path.GetFileName(zipFile + ".zip")
                InitDownLoad(zipName, AppConfig.UnixServer.InterfaceDirectory)
            Finally

            End Try
        End Sub


        Private Sub InitDownLoad(zipFileName As String, sourceDirectory As String)
            Dim sJavaScript As String
            Dim params As DownLoadBase.DownLoadParams

            params.downLoadCode = DownLoadBase.DownLoadParams.DownLoadTypeCode.FILE

            params.fileName = sourceDirectory & "/" & zipFileName
            params.DeleteFileAfterDownload = True

            Session(DownLoadBase.SESSION_PARAMETERS_DOWNLOAD_KEY) = params
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "showReportViewerFrame('../Common/DownLoadWindowForm.aspx'); " & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            RegisterStartupScript("EnableReportCe", sJavaScript)
        End Sub

    End Class
End Namespace
