Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Interfaces

    Partial Class SvrNotificationFileProcessedController
        Inherits System.Web.UI.UserControl

#Region "Page State"

#Region "MyState"

        Class MyState
            Public SelectedNotificationFileProcessedId As Guid = Guid.Empty
            Public SelectedNotificationFileLayout As String = ""
            Public SelectedDealerCode As String = ""
            Public IsGridVisible As Boolean = False
            Public SelectedNotificationId As Guid = Guid.Empty
            Public ErrorCtrl As ErrorController
            Public msUrlDetailPage As String
            Public msUrlPrintPage As String
            Public moInterfaceTypeCode As ServiceNotificationFileProcessedData.InterfaceTypeCode
            Public mnPageIndex As Integer
            Public intStatusId As Guid
            Public errorStatus As InterfaceStatusWrk.IntError

            Public Sub New(UrlDetailPage As String, UrlPrintPage As String,
            oInterfaceTypeCode As ServiceNotificationFileProcessedData.InterfaceTypeCode)
                msUrlDetailPage = UrlDetailPage
                msUrlPrintPage = UrlPrintPage
                moInterfaceTypeCode = oInterfaceTypeCode
            End Sub
        End Class
#End Region

#Region "Page Return"
        Private IsReturningFromChild As Boolean = False

        Public Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object)
            IsReturningFromChild = True
            Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        Try
                            TheState.SelectedNotificationFileProcessedId = retObj.SelectedNotificationFileProcessedId
                            moDataGrid.CurrentPageIndex = TheState.mnPageIndex
                            If Not TheState.SelectedNotificationFileProcessedId.Equals(Guid.Empty) Then
                                TheState.IsGridVisible = True
                                PopulateServiceInterfaceDropDown()
                                PopulateGrid(ThePage.POPULATE_ACTION_NONE)
                                ThePage.SetGridItemStyleColor(moDataGrid)
                                EnableDisableButtons()
                                ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                                ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                            End If
                        Catch ex As Exception
                            ThePage.HandleErrors(ex, ErrorCtrl)

                        End Try
                    End If
            End Select
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public SelectedNotificationFileProcessedId As Guid
            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, selNotificationFileProcessedId As Guid)
                LastOperation = LastOp
                SelectedNotificationFileProcessedId = selNotificationFileProcessedId
            End Sub
        End Class
#End Region

#End Region

#Region "Constants"

        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_SELECT_IDX As Integer = 1
        ' Public Const GRID_COL_DEALERFILE_PROCESSED_ID_IDX As Integer = 2
        Public Const GRID_COL_NOTIFICATIONFILE_PROCESSED_ID_IDX As Integer = 2
        Public Const GRID_COL_FILENAME_IDX As Integer = 3
        Public Const GRID_COL_RECEIVED_IDX As Integer = 4
        Public Const GRID_COL_COUNTED_IDX As Integer = 5
        Public Const GRID_COL_BYPASSES_IDX As Integer = 6
        Public Const GRID_COL_REJECTED_IDX As Integer = 7
        Public Const GRID_COL_VALIDATED_IDX As Integer = 8
        Public Const GRID_COL_LOADED_IDX As Integer = 9
        'Public Const GRID_COL_PROCESSED_AMOUNT_IDX As Integer = 10

        Private Const INTERFACE_CODE_NEW_NOTIFICATION As String = "001"
        Private Const INTERFACE_LIST_CODE_NEW_NOTIFICATION As String = "OPINF"
        Private Const INTERFACE_CODE_CLOSE_NOTIFICATION As String = "002"
        Private Const INTERFACE_LIST_CODE_CLOSE_NOTIFICATION As String = "CLINF"

        Private Const SP_VALIDATE As Integer = 0
        Private Const SP_PROCESS As Integer = 1
        Private Const SP_DELETE As Integer = 2
        Private Const DEALER_FILE_REQUIRED As String = "NOTIFICATION_FILE_REQUIRED"
        Private Const DEALER_PAYMENT_FILE_SUBSTRING As String = "P"
        Private Const NOTIFICATION_VARIABLE_NAME As String = "moSvrNotificationController_"
        Public Const SESSION_LOCALSTATE_KEY As String = "NOTIFICATIONFILE_PROCESSEDCONTROLLER_SESSION_LOCALSTATE_KEY"
        Private Const PORT As Integer = 21

#End Region

#Region "Variables"
        Private moState As MyState
        Private ErrorCtrl As ErrorController
        Protected WithEvents moInterfaceProgressControl As InterfaceProgressControl

        Private Const SYSTEM_CODE_SINA As String = "SINA"
        Private Const SYSTEM_CODE_SM As String = "SM"
        Private Const SYSTEM_CODE_SINC As String = "SINC"

        Private Const FILE_EXTENSION As String = ".TXT"
#End Region

#Region "Properties"


        Protected ReadOnly Property TheState() As MyState
            Get
                Try
                    If moState Is Nothing Then
                        moState = CType(Session(SESSION_LOCALSTATE_KEY), MyState)
                    End If
                    Return moState
                Catch ex As Exception
                    'When we are in design mode there is no session object
                    Return Nothing
                End Try
            End Get
        End Property

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property

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
            ControlMgr.SetEnableControl(ThePage, moUpLoadPanel, False)
            If IsReturningFromChild Then
                If moDealerDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
                    TheState.IsGridVisible = True
                    PopulateGrid(ThePage.POPULATE_ACTION_SAVE)
                    'ThePage.SetGridItemStyleColor(Me.moDataGrid)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                End If
            End If

        End Sub

        ' This method should be called for every pageload
        Public Sub SetErrorController(oErrorCtrl As ErrorController)
            ErrorCtrl = oErrorCtrl
        End Sub

        ' This is the initialization Method
        Public Sub InitController(UrlDetailPage As String, UrlPrintPage As String, _
        oInterfaceTypeCode As ServiceNotificationFileProcessedData.InterfaceTypeCode)
            moState = New MyState(UrlDetailPage, UrlPrintPage, oInterfaceTypeCode)
            Session(SESSION_LOCALSTATE_KEY) = moState
            PopulateServiceInterfaceDropDown()
            ThePage.SetGridItemStyleColor(moDataGrid)
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub moDealerDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moDealerDrop.SelectedIndexChanged
            Try
                ClearAll()
                If moDealerDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
                    TheState.IsGridVisible = True
                    PopulateGrid(ThePage.POPULATE_ACTION_NONE)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                End If
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnCopyDealerFile_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopyDealerFile_WRITE.Click
            Try
                uploadNotificationFile()
                ThePage.DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", ThePage.MSG_BTN_OK, ThePage.MSG_TYPE_INFO)
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub



        Private Sub ExecuteSp(oSP As Integer)
            Dim oServiceNotificationFileProcessedData As New ServiceNotificationFileProcessedData

            If Not TheState.SelectedNotificationFileProcessedId.Equals(Guid.Empty) Then

                Dim oServiceNotificationFileProcessed As New ServiceNotificationFileProcessed(TheState.SelectedNotificationFileProcessedId)

                'Dim oSplitSystem As New SplitSystem(oServiceNotificationFileProcessed.SplitSystemId)
                With oServiceNotificationFileProcessedData
                    .filename = oServiceNotificationFileProcessed.Filename
                    '.filename = oServiceNotificationFileProcessed.UniqueId
                    .layout = "new" 'oSplitSystem.Layout
                    .fileTypeCode = TheState.moInterfaceTypeCode
                End With
                Select Case oSP
                    Case SP_VALIDATE
                        ServiceNotificationFileProcessed.ValidateFile(oServiceNotificationFileProcessedData)
                    Case SP_PROCESS
                        ServiceNotificationFileProcessed.ProcessFileRecords(oServiceNotificationFileProcessedData)
                    Case SP_DELETE
                        ServiceNotificationFileProcessed.DeleteFile(oServiceNotificationFileProcessedData)
                End Select
            Else
                Throw New GUIException("You must select a Notification file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If
            TheState.intStatusId = oServiceNotificationFileProcessedData.interfaceStatus_id

        End Sub

        Private Sub BtnValidate_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnValidate_WRITE.Click
            ExecuteAndWait(SP_PROCESS)
        End Sub

        Private Sub BtnLoadCertificate_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnLoadCertificate_WRITE.Click
            ExecuteAndWait(SP_PROCESS)
        End Sub

        Private Sub BtnDeleteDealerFile_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnDeleteDealerFile_WRITE.Click
            ExecuteAndWait(SP_PROCESS)
        End Sub

        Private Sub BtnRejectReport_Click(sender As System.Object, e As System.EventArgs) Handles BtnRejectReport.Click
            'Try

            '    If Not TheState.SelectedNotificationFileProcessedId.Equals(Guid.Empty) Then
            '        'Dim param As New PrintClaimLoadRejectForm.MyState
            '        'param.ServiceNotificationfileProcessedId = TheState.SelectedNotificationFileProcessedId
            '        ' param.moInterfaceTypeCode = TheState.moInterfaceTypeCode
            '        ThePage.callPage(TheState.msUrlPrintPage, param)
            '    Else
            '        Throw New GUIException("You must select a Claim file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            '    End If
            'Catch ex As Threading.ThreadAbortException
            'Catch ex As Exception
            '    ThePage.HandleErrors(ex, Me.ErrorCtrl)
            'End Try
        End Sub

#End Region

#Region "Handlers-Progress Buttons"

        Private Sub btnAfterProgressBar_Click(sender As System.Object, e As System.EventArgs) Handles btnAfterProgressBar.Click
            AfterProgressBar()
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub moDataGrid_PageIndexChanged(source As System.Object, _
                e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
            Try
                moDataGrid.CurrentPageIndex = e.NewPageIndex
                TheState.mnPageIndex = moDataGrid.CurrentPageIndex
                ClearSelectedNotificationFile(ThePage.POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            ThePage.BaseItemCreated(sender, e)
        End Sub

        Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = ThePage.EDIT_COMMAND_NAME Then
                    TheState.SelectedNotificationFileProcessedId = New Guid(e.Item.Cells(GRID_COL_NOTIFICATIONFILE_PROCESSED_ID_IDX).Text)
                    TheState.mnPageIndex = moDataGrid.CurrentPageIndex
                    ThePage.callPage(TheState.msUrlDetailPage, TheState.SelectedNotificationFileProcessedId)

                ElseIf e.CommandName = ThePage.SELECT_COMMAND_NAME Then
                    moDataGrid.SelectedIndex = e.Item.ItemIndex
                    TheState.SelectedNotificationFileProcessedId = ThePage.GetGuidFromString( _
                                ThePage.GetSelectedGridText(moDataGrid, GRID_COL_NOTIFICATIONFILE_PROCESSED_ID_IDX))
                    EnableDisableButtons()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub moDataGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                With e.Item
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_NOTIFICATIONFILE_PROCESSED_ID_IDX), dvRow(ServiceNotificationFileProcessed.COL_NAME_SVC_NOTIFICATION_PROCESSED_ID))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_FILENAME_IDX), dvRow(ServiceNotificationFileProcessed.COL_NAME_FILENAME))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_RECEIVED_IDX), dvRow(ServiceNotificationFileProcessed.COL_NAME_RECEIVED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_BYPASSES_IDX), dvRow(ServiceNotificationFileProcessed.COL_NAME_BYPASSED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_COUNTED_IDX), dvRow(ServiceNotificationFileProcessed.COL_NAME_COUNTED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_REJECTED_IDX), dvRow(ServiceNotificationFileProcessed.COL_NAME_REJECTED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_VALIDATED_IDX), dvRow(ServiceNotificationFileProcessed.COL_NAME_VALIDATED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_LOADED_IDX), dvRow(ServiceNotificationFileProcessed.COL_NAME_LOADED))
                    'ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_PROCESSED_AMOUNT_IDX), dvRow(ServiceNotificationFileProcessed.COL_NAME_PROCESSED_AMOUNT))

                End With
            End If
        End Sub

#End Region

#End Region

#Region "Progress Bar"



        Public Sub InstallInterfaceProgressBar()
            ThePage.InstallDisplayProgressBar()
        End Sub



        Private Sub ExecuteAndWait(oSP As Integer)
            Dim intStatus As InterfaceStatusWrk
            Dim params As InterfaceBaseForm.Params

            Try
                ExecuteSp(oSP)
                params = SetParameters(TheState.intStatusId, NOTIFICATION_VARIABLE_NAME)
                Session(InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
                TheInterfaceProgress.EnableInterfaceProgress(NOTIFICATION_VARIABLE_NAME)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Function SetParameters(intStatusId As Guid, baseController As String) As InterfaceBaseForm.Params
            Dim params As New InterfaceBaseForm.Params

            With params
                .intStatusId = intStatusId
                .baseController = baseController
            End With
            Return params
        End Function

        Private Sub AfterProgressBar()
            ClearSelectedNotificationFile(ThePage.POPULATE_ACTION_SAVE)
            ThePage.DisplayMessage(Message.MSG_INTERFACES_HAS_COMPLETED, "", ThePage.MSG_BTN_OK, ThePage.MSG_TYPE_INFO)
        End Sub

#End Region

#Region "Error-Management"

        Private Sub ShowError(msg As String)
            ErrorCtrl.AddError(msg)
            ErrorCtrl.Show()
            AppConfig.Log(New Exception(msg))
        End Sub

#End Region

#Region "Button-Management"

        Private Sub uploadNotificationFile()
            Dim NotificationFileName As String
            Dim layoutFileName As String
            Dim fileLen As Integer = notificationFileInput.PostedFile.ContentLength
            ServiceNotificationFileProcessed.ValidateFileName(fileLen)
            'NotificationFileName = MiscUtil.ReplaceSpaceByUnderscore(notificationFileInput.PostedFile.FileName)
            NotificationFileName = notificationFileInput.PostedFile.FileName
            Dim fileBytes(fileLen - 1) As Byte
            Dim objStream As System.IO.Stream
            objStream = notificationFileInput.PostedFile.InputStream
            objStream.Read(fileBytes, 0, fileLen)

            Dim webServerPath As String = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
            Dim webServerFile As String = webServerPath & "\" & System.IO.Path.GetFileName(NotificationFileName)
            layoutFileName = webServerPath & "\" & _
                            System.IO.Path.GetFileNameWithoutExtension(webServerFile) & AppConfig.UnixServer.FtpTriggerExtension
            CreateFolder(webServerPath)
            System.IO.File.WriteAllBytes(webServerFile, fileBytes)
            'System.IO.File.WriteAllBytes(webServerPath & "\" & System.IO.Path.GetFileNameWithoutExtension(webServerFile), System.Text.Encoding.ASCII.GetBytes(TheState.SelectedClaimFileLayout))
            System.IO.File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes(TheState.SelectedNotificationFileLayout))
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
                ThePage.HandleErrors(ex, ErrorCtrl)
            Finally
                '' ''objUnixFTP.CloseConnection()
            End Try

        End Sub

        Private Sub DisableButtons()
            ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)

        End Sub

        Private Sub EnableDisableButtons()
            If Not TheState.SelectedNotificationFileProcessedId.Equals(Guid.Empty) Then
                Dim oNotificationFile As ServiceNotificationFileProcessed = New ServiceNotificationFileProcessed(TheState.SelectedNotificationFileProcessedId)
                DisableButtons()
                With oNotificationFile
                    'If .Received.Value = .Counted.Value Then ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, True)
                    'If .Received.Value = .Validated.Value Then ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)

                    'If .Rejected.Value > 0 Then
                    '    ControlMgr.SetEnableControl(ThePage, BtnRejectReport, True)
                    'Else
                    '    ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                    'End If

                    If .Rejected.Value > 0 Then ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, True)
                    ' If .Rejected.Value = .Counted.Value Then ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, True)

                    'If .Received.Value = .Loaded.Value Then
                    '     BtnDeleteDealerFile_WRITE.Enabled = True
                    '  ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, False)
                    '    ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                    'End If

                    ''If (.Loaded.Value = .Counted.Value) OrElse (.Loaded.Value = 0) OrElse _
                    ''   (.Bypassed.Value > 0) Then
                    ''    ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, True)
                    ''End If

                    'If (.Loaded.Value = 0) OrElse (.Received.Value = .Bypassed.Value + .Loaded.Value) Then
                    '    ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, True)
                    'End If

                End With
            Else
                Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)

            End If
        End Sub

        Public Sub EnableDisableEditControl()
            Dim i As Integer
            Dim edt As ImageButton

            '  Enable or Disable all the EDIT  buttons on the DataGrid
            For i = 0 To (moDataGrid.Items.Count - 1)
                edt = CType(moDataGrid.Items(i).Cells(ThePage.EDIT_COL).FindControl(ThePage.EDIT_CONTROL_NAME), ImageButton)
                If edt IsNot Nothing Then
                    edt.Enabled = (moDataGrid.Items(i).Cells(GRID_COL_REJECTED_IDX).Text.Trim() <> "0" OrElse (moDataGrid.Items(i).Cells(GRID_COL_REJECTED_IDX).Text.Trim() = "0" AndAlso moDataGrid.Items(i).Cells(GRID_COL_BYPASSES_IDX).Text.Trim() <> "0"))
                End If
            Next
            If TheState.moInterfaceTypeCode = ServiceNotificationFileProcessedData.InterfaceTypeCode.CLOSE_NOTIFICATION Then
                'moDataGrid.Columns(GRID_COL_PROCESSED_AMOUNT_IDX).Visible = True
            End If
        End Sub

        Private Sub ClearSelectedNotificationFile(oAction As String)
            moDataGrid.SelectedIndex = ThePage.NO_ITEM_SELECTED_INDEX
            DisableButtons()
            TheState.SelectedNotificationFileProcessedId = Guid.Empty
            PopulateGrid(oAction)
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearAll()
            moDataGrid.CurrentPageIndex = ThePage.NO_PAGE_INDEX
            moDataGrid.DataSource = Nothing
            moDataGrid.DataBind()
            TheState.SelectedNotificationFileProcessedId = Guid.Empty
            TheState.SelectedDealerCode = ""
            TheState.SelectedNotificationFileLayout = ""
            moExpectedFileLabel_NO_TRANSLATE.Text = String.Empty
            DisableButtons()
            ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, False, True)
            ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, False, True)
        End Sub

#End Region

#Region "Populate"

        Sub PopulateServiceInterfaceDropDown()
            Try
                'Dim InterfaceCode As String
                'Dim ListCode As String
                Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)

                'InterfaceCode = INTERFACE_CODE_NEW_NOTIFICATION
                'ListCode = INTERFACE_LIST_CODE_CLOSE_NOTIFICATION

                'ThePage.BindListControlToDataView(moDealerDrop,
                '    LookupListNew.GetSplitSystemTranslationsLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies,
                '       InterfaceCode, langCode, ListCode), , , True)

                Dim SplitSystemCodeList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
                For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    Dim SplitSystemCodeByCompany As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="SplitSystemByCodeAndInterface",
                                                                            languageCode:=Thread.CurrentPrincipal.GetLanguageCode(),
                                                                            context:=New ListContext() With
                                                                            {
                                                                              .CompanyId = _company,
                                                                              .ListCode = INTERFACE_LIST_CODE_CLOSE_NOTIFICATION,
                                                                              .InterfaceCode = INTERFACE_CODE_NEW_NOTIFICATION,
                                                                              .LanguageId = Thread.CurrentPrincipal.GetLanguageId()
                                                                            })
                    If SplitSystemCodeByCompany.Count > 0 Then
                        If SplitSystemCodeList IsNot Nothing Then
                            SplitSystemCodeList.AddRange(SplitSystemCodeByCompany)
                        Else
                            SplitSystemCodeList = SplitSystemCodeByCompany.Clone()
                        End If
                    End If
                Next

                moDealerDrop.Populate(SplitSystemCodeList.ToArray(),
                                      New PopulateOptions() With
                                      {
                                        .AddBlankItem = True
                                      })

                ThePage.BindSelectItem(TheState.SelectedNotificationId.ToString, moDealerDrop)
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Function GetDataView() As DataView
            Dim oNotificationFileData As ServiceNotificationFileProcessedData = New ServiceNotificationFileProcessedData
            Dim oDataView As DataView

            With oNotificationFileData
                .svcnotificationprocessedid = TheState.SelectedNotificationFileProcessedId
                .splitSystemId = ThePage.GetSelectedItem(moDealerDrop)
                oDataView = ServiceNotificationFileProcessed.LoadList(oNotificationFileData)
            End With

            Return oDataView
        End Function

        Private Function createFileName(systemCode As String, interfaceCode As String) As String
            Dim FileName As String = systemCode
            Select Case systemCode
                Case SYSTEM_CODE_SINA, SYSTEM_CODE_SINC
                    Dim dateStr As String = System.DateTime.Now.ToString("yyMMdd")
                    FileName = FileName + dateStr
                Case SYSTEM_CODE_SM
                    FileName = FileName + "O" & interfaceCode + "001"
            End Select
            FileName = FileName + FILE_EXTENSION
            Return FileName
        End Function

        Private Sub SetExpectedFile()
            Dim PathFileName As String = String.Empty
            Dim SplitSystemId As Guid = ThePage.GetSelectedItem(moDealerDrop)


            TheState.SelectedNotificationId = SplitSystemId
            Dim oSplitSystem As SplitSystem = New SplitSystem(SplitSystemId)

            If Not SplitSystemId.Equals(Guid.Empty) Then
                TheState.SelectedNotificationFileLayout = oSplitSystem.Layout
                Dim Directory As String = AppConfig.FileClientDirectory
                Dim SystemCode As String = oSplitSystem.SystemCode
                Dim InterfaceCode As String = oSplitSystem.InterfaceCode
                PathFileName = Directory + createFileName(SystemCode, InterfaceCode)
            End If
            moExpectedFileLabel_NO_TRANSLATE.Text = PathFileName
        End Sub

        Private Sub PopulateGrid(oAction As String)
            Dim oDataView As DataView

            Try
                SetExpectedFile()
                oDataView = GetDataView()
                ThePage.BasePopulateGrid(moDataGrid, oDataView, TheState.SelectedNotificationFileProcessedId, oAction)
                ThePage.SetPageAndSelectedIndexFromGuid(oDataView, TheState.SelectedNotificationFileProcessedId, moDataGrid, TheState.mnPageIndex)

                ThePage.ErrCollection.Clear()

                EnableDisableEditControl()
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

    End Class

End Namespace
