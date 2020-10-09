Imports Assurant.Common.Zip.aZip
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.DownLoad
Imports System.IO
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Interfaces

    Partial Class SplitPremiumFileForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

        Class MyState
            Public SelectedSplitId As Guid
            Public SelectedSplitFileProcessedId As Guid
            Public FileName As String
            Public SelectedSplitFileLayout As String = ""
            Public intStatusId As Guid
            Public errorStatus As InterfaceStatusWrk.IntError
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

#Region "Constants"

        Private Const SP_SPLIT As Integer = 0
        Private Const SP_DELETE As Integer = 1
        Private Const PORT As Integer = 21

        Private Const INTERFACE_CODE_SPLIT_SYSTEM As String = "003"

        Public Const GRID_COL_SELECT_IDX As Integer = 0
        Public Const GRID_COL_CLAIMFILE_PROCESSED_ID_IDX As Integer = 1
        Public Const GRID_COL_FILENAME_IDX As Integer = 2
        Public Const GRID_COL_RECEIVED_IDX As Integer = 3
        Public Const GRID_COL_COUNTED_IDX As Integer = 4
        Public Const GRID_COL_SPLIT_IDX As Integer = 5



        Private Const SPLIT_FILE_REQUIRED As String = "SPLIT_FILE_REQUIRED"


#End Region

#Region "Variables"


#End Region

#Region "Properties"

        '/       Public ReadOnly Property TheClaimController() As SplitPremiumFileProcessedController
        '           Get
        '               If moClaimController Is Nothing Then
        '                  moClaimController = CType(FindControl("moClaimController"), SplitPremiumFileProcessedController)
        '            End If
        '           Return moClaimController
        '       End Get
        '  End Property

        Public ReadOnly Property TheInterfaceProgress() As InterfaceProgressControl
            Get
                If moInterfaceProgressControl Is Nothing Then
                    moInterfaceProgressControl = CType(FindControl("moInterfaceProgressControl"), InterfaceProgressControl)
                End If
                Return moInterfaceProgressControl
            End Get
        End Property

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moInterfaceProgressControl As InterfaceProgressControl
        ' Protected WithEvents moClaimController As SplitPremiumFileProcessedController



        'Protected WithEvents dealerFileInput As System.Web.UI.HtmlControls.HtmlInputFile

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            ErrorCtrl.Clear_Hide()
            'TheClaimController.SetErrorController(ErrorCtrl)
            Try
                If Not IsPostBack Then
                    InitializeForm()
                Else
                End If
                InstallReportViewer()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
            InstallInterfaceProgressBar()
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub ddSplit_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ddSplit.SelectedIndexChanged
            Try
                ClearAll()
                If ddSplit.SelectedIndex > BLANK_ITEM_SELECTED Then
                    State.SelectedSplitId = GetSelectedItem(ddSplit)
                    PopulateSplitFilesGrid(POPULATE_ACTION_NONE)
                    If dgSplitFiles.Items.Count() <> 0 Then
                        dgSplitFiles.SelectedIndex = 0
                        State.SelectedSplitFileProcessedId = GetGuidFromString( _
                                    GetSelectedGridText(dgSplitFiles, GRID_COL_CLAIMFILE_PROCESSED_ID_IDX))
                        State.FileName = GetSelectedGridText(dgSplitFiles, GRID_COL_FILENAME_IDX)
                        PopulateSplitReconWrkGrid(POPULATE_ACTION_NONE, State.SelectedSplitFileProcessedId)
                        SetButtonsEnable(True)
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub dgSplitFiles_PageIndexChanged(source As System.Object, _
                e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSplitFiles.PageIndexChanged
            Try
                SetButtonsEnable(False)
                dgSplitFiles.CurrentPageIndex = e.NewPageIndex
                ClearSelectedClaimFile(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Private Sub ClearSelectedClaimFile(oAction As String)
            dgSplitFiles.SelectedIndex = NO_ITEM_SELECTED_INDEX
            SetButtonsEnable(True)
            State.SelectedSplitFileProcessedId = Guid.Empty
            PopulateSplitFilesGrid(oAction)
        End Sub

        Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = SELECT_COMMAND_NAME Then
                    dgSplitFiles.SelectedIndex = e.Item.ItemIndex
                    State.SelectedSplitFileProcessedId = GetGuidFromString( _
                                GetSelectedGridText(dgSplitFiles, GRID_COL_CLAIMFILE_PROCESSED_ID_IDX))
                    State.FileName = GetSelectedGridText(dgSplitFiles, GRID_COL_FILENAME_IDX)
                    SetButtonsEnable(True)
                    Dim countSplit As Integer = CType(GetSelectedGridText(dgSplitFiles, GRID_COL_SPLIT_IDX), Integer)
                    If countSplit = 0 Then
                        BtnDownLoadFiles.Enabled = False
                    End If
                    PopulateSplitReconWrkGrid(POPULATE_ACTION_NONE, State.SelectedSplitFileProcessedId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub moDataGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSplitFiles.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                With e.Item
                    PopulateControlFromBOProperty(.Cells(GRID_COL_CLAIMFILE_PROCESSED_ID_IDX), dvRow(SplitfileProcessedDAL.COL_NAME_SPLITFILE_PROCESSED_ID))
                    PopulateControlFromBOProperty(.Cells(GRID_COL_FILENAME_IDX), dvRow(SplitfileProcessedDAL.COL_NAME_FILENAME))
                    PopulateControlFromBOProperty(.Cells(GRID_COL_RECEIVED_IDX), dvRow(SplitfileProcessedDAL.COL_NAME_RECEIVED))
                    PopulateControlFromBOProperty(.Cells(GRID_COL_COUNTED_IDX), dvRow(SplitfileProcessedDAL.COL_NAME_COUNTED))
                    PopulateControlFromBOProperty(.Cells(GRID_COL_SPLIT_IDX), dvRow(SplitfileProcessedDAL.COL_NAME_SPLIT))
                End With
            End If
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub BtnDownLoadFiles_Click(sender As System.Object, e As System.EventArgs) Handles BtnDownLoadFiles.Click
            Try
                DownloadSplitFiles()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnCopyDealerFile_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopyDealerFile_WRITE.Click
            Try
                uploadClaimFile()
                DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub BtnDeleteOriginalFile_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnDeleteOriginalFile_WRITE.Click
            Try
                ExecuteAndWait(SP_DELETE)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub BtnSplit_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnSplit_WRITE.Click
            Try
                ExecuteAndWait(SP_SPLIT)
                SetButtonsEnable(False)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Handlers-Progress Buttons"

        Private Sub btnAfterProgressBar_Click(sender As System.Object, e As System.EventArgs) Handles btnAfterProgressBar.Click
            AfterProgressBar()
        End Sub

#End Region

#End Region

#Region "Progress Bar"

        'Public Sub InstallProgressBar()
        '    'ThePage.DisplayProgressBarOnClick(BtnValidate_WRITE, "Loading_File")
        '    'ThePage.InstallDisplayProgressBar()
        'End Sub


        'Private Sub ExecuteAndWait(ByVal oSP As Integer)
        '    Dim intStatus As InterfaceStatusWrk

        '    Try
        '        ' TheInterfaceProgress.EnableInterfaceProgress(DEALERP_VARIABLE_NAME)
        '        ExecuteSp(oSP)
        '        intStatus = New InterfaceStatusWrk(TheState.intStatusId)
        '        TheState.errorStatus = intStatus.WaitTilDone()

        '    Catch ex As Threading.ThreadAbortException
        '    Catch ex As Exception
        '        ThePage.HandleErrors(ex, Me.ErrorCtrl)
        '    Finally


        '        ClearSelectedDealerFile(ThePage.POPULATE_ACTION_SAVE)

        '    End Try
        'End Sub

        Public Sub InstallInterfaceProgressBar()
            'Me.DisplayProgressBarOnClick(BtnSplit_WRITE, "Interfaces")
            'Me.DisplayProgressBarOnClick(BtnDeleteOriginalFile_WRITE, "Interfaces")
            'Me.DisplayProgressBarOnClick(BtnDownLoadFiles, "Interfaces")
            InstallDisplayProgressBar()
        End Sub

        Private Sub ExecuteAndWait(oSP As Integer)
            Dim intStatus As InterfaceStatusWrk
            Dim params As InterfaceBaseForm.Params

            Try
                ExecuteSp(oSP)
                'intStatus = New InterfaceStatusWrk(Me.State.intStatusId)
                'Me.State.errorStatus = intStatus.WaitTilDone()
                'If Me.State.errorStatus.status = InterfaceStatusWrk.IntStatus.INTERFACE_DB_FAILED Then
                '    ShowError(InterfaceStatusWrk.IntStatus.GetName(GetType(InterfaceStatusWrk.IntStatus), _
                '                    InterfaceStatusWrk.IntStatus.INTERFACE_DB_FAILED))
                'Else
                '    params = SetParameters(Me.State.intStatusId)
                '    Session(InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
                '    If State.errorStatus.status = InterfaceStatusWrk.IntStatus.PENDING Then
                '        TheInterfaceProgress.EnableInterfaceProgress()
                '    Else
                '        AfterProgressBar()
                '    End If
                'End If
                params = SetParameters(State.intStatusId)
                Session(InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
                TheInterfaceProgress.EnableInterfaceProgress()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Function SetParameters(intStatusId As Guid) As InterfaceBaseForm.Params
            Dim params As New InterfaceBaseForm.Params

            With params
                .intStatusId = intStatusId
            End With
            Return params
        End Function

        Private Sub AfterProgressBar()
            ClearSelectedClaimFile(POPULATE_ACTION_SAVE)
            DisplayMessage(Message.MSG_INTERFACES_HAS_COMPLETED, "", MSG_BTN_OK, MSG_TYPE_INFO)
        End Sub


#End Region

#Region "Error-Management"

        Private Sub ShowError(msg As String)
            ErrorCtrl.AddError(msg)
            ErrorCtrl.Show()
            AppConfig.Log(New Exception(msg))
        End Sub

#End Region

#Region "Populate"

        Sub PopulateSplitInterfaceDropDown()
            Try
                'Dim oCompanyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
                'Me.BindListControlToDataView(ddSplit, LookupListNew.GetSplitSystemLookupList(oCompanyIds, INTERFACE_CODE_SPLIT_SYSTEM), , , True)

                Dim SplitSystemCodeList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
                For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    Dim SplitSystemCodeByCompany As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="SplitSystemCodeByInterface",
                                                                            languageCode:=Thread.CurrentPrincipal.GetLanguageCode(),
                                                                            context:=New ListContext() With
                                                                            {
                                                                              .CompanyId = _company,
                                                                              .InterfaceCode = INTERFACE_CODE_SPLIT_SYSTEM
                                                                            })
                    If SplitSystemCodeByCompany.Count > 0 Then
                        If SplitSystemCodeList IsNot Nothing Then
                            SplitSystemCodeList.AddRange(SplitSystemCodeByCompany)
                        Else
                            SplitSystemCodeList = SplitSystemCodeByCompany.Clone()
                        End If
                    End If
                Next

                ddSplit.Populate(SplitSystemCodeList.ToArray(),
                                      New PopulateOptions() With
                                      {
                                        .AddBlankItem = True
                                      })

                BindSelectItem(State.SelectedSplitId.ToString, ddSplit)

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub InitializeForm()
            SetGridItemStyleColor(dgSplitFiles)
            PopulateSplitInterfaceDropDown()
            '  LoadRequiredFieldControlData()
        End Sub

        Private Function GetSplitFilesDataView() As DataView
            Dim oSplitFileData As SplitfileProcessed = New SplitfileProcessed
            Dim oDataView As DataView

            With oSplitFileData
                .SplitSystemId = GetSelectedItem(ddSplit)
                oDataView = SplitfileProcessed.LoadList(oSplitFileData)
            End With
            Return oDataView
        End Function

        Private Function GetSplitReconWrkDataView(SplitfileProcessedId As Guid) As DataView
            Dim oSplitFileData As SplitfileProcessed = New SplitfileProcessed
            Dim oDataView As DataView

            oDataView = SplitfileProcessed.LoadTotalRecordByFile(SplitfileProcessedId)
            Return oDataView
        End Function

        Private Sub PopulateSplitFilesGrid(oAction As String)
            Dim oDataView As DataView

            Try
                oDataView = GetSplitFilesDataView()
                BasePopulateGrid(dgSplitFiles, oDataView, State.SelectedSplitId, oAction)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub PopulateSplitReconWrkGrid(oAction As String, SplitfileProcessedId As Guid)
            Dim oDataView As DataView

            Try
                oDataView = GetSplitReconWrkDataView(SplitfileProcessedId)
                BasePopulateGrid(dgSplitFileRecords, oDataView, State.SelectedSplitFileProcessedId, oAction)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Private Sub ExecuteSp(oSP As Integer)
            Dim oSplitFileProcessedData As New SplitFileProcessedData

            If Not State.SelectedSplitFileProcessedId.Equals(Guid.Empty) Then
                Dim oSplitFileProcessed As New SplitfileProcessed(State.SelectedSplitFileProcessedId)
                Dim oSplitSystem As New SplitSystem(oSplitFileProcessed.SplitSystemId)
                With oSplitFileProcessedData
                    .filename = oSplitFileProcessed.Filename
                    .layout = oSplitSystem.Layout
                End With
                Select Case oSP
                    Case SP_SPLIT
                        SplitfileProcessed.SplitFile(oSplitFileProcessedData)
                    Case SP_DELETE
                        SplitfileProcessed.DeleteFile(oSplitFileProcessedData)
                End Select
            Else
                Throw New GUIException("You must select a file name", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If
            State.intStatusId = oSplitFileProcessedData.interfaceStatus_id
        End Sub

        Private Sub CreateZipFiles(zipFileName As String, sourceDirectory As String)
            Try
                Dim sourceDir As String = sourceDirectory.Replace("/", "\")
                Dim destinationFileName As String = sourceDir & "\" & zipFileName
                Dim filter As String = "TXT"
                CreateZipFile(destinationFileName, sourceDir, True, filter)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub InitDownLoad(zipFileName As String, sourceDirectory As String)
            Dim sJavaScript As String
            Dim params As DownLoadBase.DownLoadParams

            params.downLoadCode = DownLoadBase.DownLoadParams.DownLoadTypeCode.FILE
            ' params.fileName = sourceDirectory & "/" & zipFileName & ZIP_EXT
            params.fileName = sourceDirectory & "/" & zipFileName

            Session(DownLoadBase.SESSION_PARAMETERS_DOWNLOAD_KEY) = params
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "showReportViewerFrame('../Common/DownLoadWindowForm.aspx'); " & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            RegisterStartupScript("EnableReportCe", sJavaScript)
        End Sub

        'Private Sub TransferFilesUnixWebServer(ByVal destPath As String)
        '    Dim index As Integer = 0
        '    Dim totItems As Integer = Me.dgSplitFileRecords.Items.Count - 3
        '    Dim sourcePath As String = AppConfig.UnixServer.FtpDirectorySplit

        '    Dim objUnixFTP As New aFtp(AppConfig.UnixServer.HostName, sourcePath, AppConfig.UnixServer.UserId, _
        '                         AppConfig.UnixServer.Password, PORT)
        '    'Me.Trace(Me, "hostname =" & AppConfig.UnixServer.HostName)
        '    'Me.Trace(Me, "path =" & sourcePath)
        '    'Me.Trace(Me, "userid =" & AppConfig.UnixServer.UserId)
        '    Try
        '        If (objUnixFTP.Login()) Then
        '            ' Me.Trace(Me, "ftp login")
        '            System.Threading.Thread.CurrentThread.Sleep(5000)
        '            For index = 1 To totItems
        '                Dim fileName As String = Me.dgSplitFileRecords.Items(index).Cells(0).Text
        '                '  Dim destinationFile As String = destPath & "/" & fileName
        '                Dim destinationFile As String = destPath & "\" & fileName
        '                objUnixFTP.DownloadFile(fileName, destinationFile)
        '                'Me.Trace(Me, "filename=" & fileName & "Destination=" & destinationFile)
        '                System.Threading.Thread.CurrentThread.Sleep(5000)
        '            Next
        '        End If

        '        ' Catch ioe As System.IO.IOException
        '        'Me.Trace(Me, "Io=" & ioe.Message)
        '        ' Throw ioe
        '        '   Throw New GUIException("File not found", Assurant.ElitaPlus.Common.ErrorCodes.GUI_FILE_NOT_FOUND_ERR, ioe)
        '        '  Catch ex As Exception
        '        'Throw ex
        '    Finally
        '        objUnixFTP.CloseConnection()
        '    End Try

        'End Sub

        Private Function TransferFilesUnixWebServer(destPath As String) As String
            Dim fileName As String
            Dim sourcePath As String = AppConfig.UnixServer.FtpDirectorySplit

            '' ''Dim objUnixFTP As New aFtp(AppConfig.UnixServer.HostName, sourcePath, AppConfig.UnixServer.UserId, _
            '' ''                     AppConfig.UnixServer.Password, PORT)
            Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, sourcePath, AppConfig.UnixServer.UserId, _
                                 AppConfig.UnixServer.Password)

            'Me.Trace(Me, "hostname =" & AppConfig.UnixServer.HostName)
            'Me.Trace(Me, "path =" & sourcePath)
            'Me.Trace(Me, "userid =" & AppConfig.UnixServer.UserId)
            Try
                '' ''If (objUnixFTP.Login()) Then
                ' Me.Trace(Me, "ftp login")
                System.Threading.Thread.CurrentThread.Sleep(10000)
                ' filename = <CompanyCode><Interface_Code><System_Code><Filename>.zip
                Dim oSplitSystem As New SplitSystem(State.SelectedSplitId)
                Dim oCompany As Company = New Company(oSplitSystem.CompanyId)
                Dim oCompanyCode As String = oCompany.Code
                Dim oIntCode As String = oSplitSystem.InterfaceCode
                Dim oSysCode As String = oSplitSystem.SystemCode
                Dim oProcFilename As String = State.FileName
                fileName = oCompanyCode & oIntCode & oSysCode & oProcFilename & ".zip"
                '  Dim destinationFile As String = destPath & "/" & fileName
                Dim destinationFile As String = destPath & "\" & fileName
                objUnixFTP.DownloadFile(fileName, destinationFile)
                'Me.Trace(Me, "filename=" & fileName & "Destination=" & destinationFile)
                System.Threading.Thread.CurrentThread.Sleep(5000)
                '' ''End If

                ' Catch ioe As System.IO.IOException
                'Me.Trace(Me, "Io=" & ioe.Message)
                ' Throw ioe
                '   Throw New GUIException("File not found", Assurant.ElitaPlus.Common.ErrorCodes.GUI_FILE_NOT_FOUND_ERR, ioe)
                '  Catch ex As Exception
                'Throw ex
            Finally
                '' ''objUnixFTP.CloseConnection()
            End Try
            Return fileName
        End Function

        Private Sub DownloadSplitFiles()
            Try
                ' Dim zipFileName As String = Me.State.FileName
                Dim zipFileName As String
                Dim userPathWebServer As String = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
                CreateFolder(userPathWebServer)
                zipFileName = TransferFilesUnixWebServer(userPathWebServer)
                ' CreateZipFiles(zipFileName, userPathWebServer)
                InitDownLoad(zipFileName, userPathWebServer)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub uploadClaimFile()
            Dim splitFileName As String
            Dim layoutFileName As String
            Dim fileLen As Integer = claimFileInput.PostedFile.ContentLength
            ClaimFileProcessed.ValidateFileName(fileLen)
            'splitFileName = MiscUtil.ReplaceSpaceByUnderscore(Me.claimFileInput.PostedFile.FileName)
            splitFileName = claimFileInput.PostedFile.FileName
            Dim fileBytes(fileLen - 1) As Byte
            Dim objStream As System.IO.Stream
            objStream = claimFileInput.PostedFile.InputStream
            objStream.Read(fileBytes, 0, fileLen)

            Dim webServerPath As String = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
            Dim webServerFile As String = webServerPath & "\" & System.IO.Path.GetFileName(splitFileName)
            layoutFileName = webServerPath & "\" & _
                            System.IO.Path.GetFileNameWithoutExtension(webServerFile) & AppConfig.UnixServer.FtpTriggerExtension
            Dim oSplitSystem As New SplitSystem(GetSelectedItem(ddSplit))
            State.SelectedSplitFileLayout = oSplitSystem.Layout
            CreateFolder(webServerPath)
            File.WriteAllBytes(webServerFile, fileBytes)
            'File.WriteAllBytes(webServerPath & "\" & System.IO.Path.GetFileNameWithoutExtension(webServerFile), System.Text.Encoding.ASCII.GetBytes(State.SelectedSplitFileLayout))
            File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes(State.SelectedSplitFileLayout))

            Dim unixPath As String = AppConfig.UnixServer.FtpDirectory
            '' ''Dim objUnixFTP As New aFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, _
            '' ''                     AppConfig.UnixServer.Password, PORT)
            Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, _
                                 AppConfig.UnixServer.Password)
            Try
                '' ''If (objUnixFTP.Login()) Then
                '' ''    objUnixFTP.UploadFile(webServerFile, False)
                '' ''    'objUnixFTP.UploadFile(webServerPath & "\" & System.IO.Path.GetFileNameWithoutExtension(webServerFile), False)
                '' ''    objUnixFTP.UploadFile(layoutFileName, False)
                '' ''End If

                objUnixFTP.UploadFile(webServerFile)
                objUnixFTP.UploadFile(layoutFileName)

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            Finally
                '' ''objUnixFTP.CloseConnection()
            End Try

        End Sub

        Private Sub SetButtonsEnable(action As Boolean)
            BtnSplit_WRITE.Enabled = action
            BtnDeleteOriginalFile_WRITE.Enabled = action
            BtnDownLoadFiles.Enabled = action
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearAll()
            dgSplitFiles.CurrentPageIndex = NO_PAGE_INDEX
            dgSplitFiles.DataSource = Nothing
            dgSplitFileRecords.DataSource = Nothing
            dgSplitFiles.DataBind()
            dgSplitFileRecords.DataBind()
            State.SelectedSplitFileProcessedId = Guid.Empty
            State.SelectedSplitId = Guid.Empty
            State.FileName = ""
            State.SelectedSplitFileLayout = ""
            'SetButtonsEnable(False)
        End Sub

#End Region

#Region "Gui-Validation"

        'Private Sub LoadRequiredFieldControlData()
        '    With moReqValidator
        '        .ControlToValidate = "claimFileInput"
        '        .ErrorMessage = TranslateLabelOrMessage(SPLIT_FILE_REQUIRED)
        '        .Display = ValidatorDisplay.Dynamic
        '    End With
        'End Sub

#End Region

    End Class

End Namespace