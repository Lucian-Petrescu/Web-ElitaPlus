Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Interfaces

    Partial Class ClaimFileProcessedController
        Inherits System.Web.UI.UserControl


#Region "Page State"

#Region "MyState"

        Class MyState
            Public SelectedClaimFileProcessedId As Guid = Guid.Empty
            Public SelectedDealerId As Guid = Guid.Empty
            Public SelectedClaimFileLayout As String = ""
            Public SelectedDealerCode As String = ""
            Public IsGridVisible As Boolean = False
            Public SelectedClaimId As Guid = Guid.Empty
            Public ErrorCtrl As ErrorController
            Public msUrlDetailPage As String
            Public msUrlPrintPage As String
            Public moInterfaceTypeCode As ClaimFileProcessedData.InterfaceTypeCode
            Public mnPageIndex As Integer
            Public intStatusId As Guid
            Public errorStatus As InterfaceStatusWrk.IntError

            Public Sub New(UrlDetailPage As String, UrlPrintPage As String,
            oInterfaceTypeCode As ClaimFileProcessedData.InterfaceTypeCode)
                msUrlDetailPage = UrlDetailPage
                msUrlPrintPage = UrlPrintPage
                moInterfaceTypeCode = oInterfaceTypeCode
            End Sub
        End Class

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property
#End Region

#Region "Page Return"
        Private IsReturningFromChild As Boolean = False

        Public Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object, Optional ByVal DealerCode As String = "")
            IsReturningFromChild = True
            Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        Try
                            TheState.SelectedClaimFileProcessedId = retObj.SelectedClaimFileProcessedId
                            TheState.SelectedDealerCode = retObj.SelectedDealerCode
                            moDataGrid.CurrentPageIndex = TheState.mnPageIndex
                            If Not TheState.SelectedClaimFileProcessedId.Equals(Guid.Empty) Then
                                TheState.IsGridVisible = True
                                PopulateClaimInterfaceDropDown()
                                PopulateDealerDropDown()
                                PopulateGrid(ThePage.POPULATE_ACTION_SAVE)
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
            Public SelectedClaimFileProcessedId As Guid
            Public SelectedDealerCode As String = ""
            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, selClaimFileProcessedId As Guid, Optional ByVal selDealerCode As String = "")
                LastOperation = LastOp
                SelectedClaimFileProcessedId = selClaimFileProcessedId
                SelectedDealerCode = selDealerCode
            End Sub
        End Class
#End Region

#End Region

#Region "Constants"

        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_SELECT_IDX As Integer = 1
        Public Const GRID_COL_DEALERFILE_PROCESSED_ID_IDX As Integer = 2
        Public Const GRID_COL_CLAIMFILE_PROCESSED_ID_IDX As Integer = 2
        Public Const GRID_COL_FILENAME_IDX As Integer = 3
        Public Const GRID_COL_RECEIVED_IDX As Integer = 4
        Public Const GRID_COL_COUNTED_IDX As Integer = 5
        Public Const GRID_COL_BYPASSES_IDX As Integer = 6
        Public Const GRID_COL_REJECTED_IDX As Integer = 7
        Public Const GRID_COL_VALIDATED_IDX As Integer = 8
        Public Const GRID_COL_LOADED_IDX As Integer = 9
        Public Const GRID_COL_PROCESSED_AMOUNT_IDX As Integer = 10

        Private Const INTERFACE_CODE_NEW_CLAIM As String = "001"
        Private Const INTERFACE_LIST_CODE_NEW_CLAIM As String = "OPINF"
        Private Const INTERFACE_CODE_CLOSE_CLAIM As String = "002"
        Private Const INTERFACE_LIST_CODE_CLOSE_CLAIM As String = "CLINF"

        Private Const SP_VALIDATE As Integer = 0
        Private Const SP_PROCESS As Integer = 1
        Private Const SP_DELETE As Integer = 2
        Private Const DEALER_FILE_REQUIRED As String = "CLAIMS_FILE_REQUIRED"
        Private Const DEALER_PAYMENT_FILE_SUBSTRING As String = "P"
        Private Const CLAIMP_VARIABLE_NAME As String = "moClaimController_"
        Public Const SESSION_LOCALSTATE_KEY As String = "CLAIMFILE_PROCESSEDCONTROLLER_SESSION_LOCALSTATE_KEY"
        Private Const PORT As Integer = 21
        Private Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"
#End Region

#Region "Variables"
        Private moState As MyState
        Private ErrorCtrl As ErrorController
        Protected WithEvents moInterfaceProgressControl As InterfaceProgressControl

        Private Const SYSTEM_CODE_SINA As String = "SINA"
        Private Const SYSTEM_CODE_SM As String = "SM"
        Private Const SYSTEM_CODE_SINC As String = "SINC"
        Private Const SYSTEM_CODE_XXX As String = "XXX"

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
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            If IsReturningFromChild Then
                If TheState.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM Then
                    PopulateClaimInterfaceForADealer()
                Else
                    If moClaimInterfaceDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
                        PopulateClaimInterface()
                    End If
                End If

                EnableDisableDropdowns()
            End If
        End Sub

        ' This method should be called for every pageload
        Public Sub SetErrorController(oErrorCtrl As ErrorController)
            ErrorCtrl = oErrorCtrl
        End Sub

        ' This is the initialization Method
        Public Sub InitController(UrlDetailPage As String, UrlPrintPage As String,
        oInterfaceTypeCode As ClaimFileProcessedData.InterfaceTypeCode)
            moState = New MyState(UrlDetailPage, UrlPrintPage, oInterfaceTypeCode)
            Session(SESSION_LOCALSTATE_KEY) = moState
            PopulateClaimInterfaceDropDown()
            PopulateDealerDropDown()
            EnableDisableDropdowns()
            ThePage.SetGridItemStyleColor(moDataGrid)
            PopulateClaimInterface()
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub moClaimInterfaceDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moClaimInterfaceDrop.SelectedIndexChanged
            PopulateClaimInterface()
        End Sub
        Private Sub PopulateClaimInterface()
            Try
                ClearAll()
                'If Me.moClaimInterfaceDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
                TheState.IsGridVisible = True
                PopulateGrid(ThePage.POPULATE_ACTION_NONE)
                ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                ' End If
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
        Public Sub PopulateClaimInterfaceForADealer()
            Try
                ClearAll()
                If DealerMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
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
        Protected Sub moBtnSearch_Click(sender As Object, e As EventArgs) Handles moBtnSearch.Click
            Try
                ClearAll()
                If TheState.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM Then
                    If DealerMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
                        TheState.IsGridVisible = True
                        PopulateGrid(ThePage.POPULATE_ACTION_NONE)
                        ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                        ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                    End If
                Else
                    If moClaimInterfaceDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
                        TheState.IsGridVisible = True
                        PopulateGrid(ThePage.POPULATE_ACTION_NONE)
                        ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                        ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                    End If
                End If
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
        Private Sub btnCopyDealerFile_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopyDealerFile_WRITE.Click
            Try
                uploadClaimFile()
                ThePage.DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", ThePage.MSG_BTN_OK, ThePage.MSG_TYPE_INFO)
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub



        Private Sub ExecuteSp(oSP As Integer)
            Dim oClaimFileProcessedData As New ClaimFileProcessedData
            Dim oInterfaceStatusWrk As New InterfaceStatusWrk


            If Not TheState.SelectedClaimFileProcessedId.Equals(Guid.Empty) Then
                Dim oClaimFileProcessed As New ClaimFileProcessed(TheState.SelectedClaimFileProcessedId)
                Dim oSplitSystem As New SplitSystem(oClaimFileProcessed.SplitSystemId)
                If oInterfaceStatusWrk.IsfileBeingProcessed(oClaimFileProcessed.Filename) Then
                    With oClaimFileProcessedData
                        .filename = oClaimFileProcessed.Filename
                        .layout = oSplitSystem.Layout
                        .fileTypeCode = TheState.moInterfaceTypeCode
                    End With
                    Select Case oSP
                        Case SP_VALIDATE
                            ClaimFileProcessed.ValidateFile(oClaimFileProcessedData)

                        Case SP_PROCESS
                            ClaimFileProcessed.ProcessFileRecords(oClaimFileProcessedData)
                        Case SP_DELETE
                            ClaimFileProcessed.DeleteFile(oClaimFileProcessedData)
                    End Select
                Else
                    Throw New GUIException("File is Been Process", Assurant.ElitaPlus.Common.ErrorCodes.ERR_INTERFACE_FILE_IN_PROCESS)
                End If
            Else
                Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If


            TheState.intStatusId = oClaimFileProcessedData.interfaceStatus_id

        End Sub

        Private Sub BtnValidate_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnValidate_WRITE.Click
            ExecuteAndWait(SP_VALIDATE)
        End Sub

        Private Sub BtnLoadCertificate_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnLoadCertificate_WRITE.Click
            ExecuteAndWait(SP_PROCESS)
        End Sub

        Private Sub BtnDeleteDealerFile_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnDeleteDealerFile_WRITE.Click
            ExecuteAndWait(SP_DELETE)
        End Sub

        Private Sub BtnRejectReport_Click(sender As System.Object, e As System.EventArgs) Handles BtnRejectReport.Click
            Try

                If Not TheState.SelectedClaimFileProcessedId.Equals(Guid.Empty) Then
                    Dim param As New PrintClaimLoadRejectForm.MyState
                    param.ClaimfileProcessedId = TheState.SelectedClaimFileProcessedId
                    param.moInterfaceTypeCode = TheState.moInterfaceTypeCode
                    param.reportType = PrintClaimLoadRejectForm.REJECT_REPORT
                    ThePage.callPage(TheState.msUrlPrintPage, param)
                Else
                    Throw New GUIException("You must select a Claim file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub BtnProcessedExport_Click(sender As Object, e As EventArgs) Handles BtnProcessedExport.Click
            Try

                If Not TheState.SelectedClaimFileProcessedId.Equals(Guid.Empty) Then
                    Dim param As New PrintClaimLoadRejectForm.MyState
                    param.ClaimfileProcessedId = TheState.SelectedClaimFileProcessedId
                    param.moInterfaceTypeCode = TheState.moInterfaceTypeCode
                    param.reportType = PrintClaimLoadRejectForm.PROCESSED_EXPORT
                    ThePage.callPage(TheState.msUrlPrintPage, param)
                Else
                    Throw New GUIException("You must select a Claim file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
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
                ClearSelectedClaimFile(ThePage.POPULATE_ACTION_NO_EDIT)
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
                    TheState.SelectedClaimFileProcessedId = New Guid(e.Item.Cells(GRID_COL_CLAIMFILE_PROCESSED_ID_IDX).Text)
                    TheState.mnPageIndex = moDataGrid.CurrentPageIndex
                    ThePage.callPage(TheState.msUrlDetailPage, TheState.SelectedClaimFileProcessedId)

                ElseIf e.CommandName = ThePage.SELECT_COMMAND_NAME Then
                    moDataGrid.SelectedIndex = e.Item.ItemIndex
                    TheState.SelectedClaimFileProcessedId = ThePage.GetGuidFromString( _
                                ThePage.GetSelectedGridText(moDataGrid, GRID_COL_CLAIMFILE_PROCESSED_ID_IDX))
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
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_DEALERFILE_PROCESSED_ID_IDX), dvRow(ClaimFileProcessed.COL_NAME_CLAIMFILE_PROCESSED_ID))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_FILENAME_IDX), dvRow(ClaimFileProcessed.COL_NAME_FILENAME))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_RECEIVED_IDX), dvRow(ClaimFileProcessed.COL_NAME_RECEIVED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_BYPASSES_IDX), dvRow(ClaimFileProcessed.COL_NAME_BYPASSED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_COUNTED_IDX), dvRow(ClaimFileProcessed.COL_NAME_COUNTED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_REJECTED_IDX), dvRow(ClaimFileProcessed.COL_NAME_REJECTED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_VALIDATED_IDX), dvRow(ClaimFileProcessed.COL_NAME_VALIDATED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_LOADED_IDX), dvRow(ClaimFileProcessed.COL_NAME_LOADED))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_PROCESSED_AMOUNT_IDX), dvRow(ClaimFileProcessed.COL_NAME_PROCESSED_AMOUNT))

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
                params = SetParameters(TheState.intStatusId, CLAIMP_VARIABLE_NAME)
                Session(InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
                TheInterfaceProgress.EnableInterfaceProgress(CLAIMP_VARIABLE_NAME)
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
            ClearSelectedClaimFile(ThePage.POPULATE_ACTION_SAVE)
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

        Private Sub uploadClaimFile()
            Dim claimFileName As String
            Dim layoutFileName As String
            Dim fileLen As Integer = claimFileInput.PostedFile.ContentLength
            ClaimFileProcessed.ValidateFileName(fileLen)
            'claimFileName = MiscUtil.ReplaceSpaceByUnderscore(claimFileInput.PostedFile.FileName)
            claimFileName = claimFileInput.PostedFile.FileName
            Dim fileBytes(fileLen - 1) As Byte
            Dim objStream As System.IO.Stream
            objStream = claimFileInput.PostedFile.InputStream
            objStream.Read(fileBytes, 0, fileLen)

            Dim webServerPath As String = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
            Dim webServerFile As String = webServerPath & "\" & System.IO.Path.GetFileName(claimFileName)
            layoutFileName = webServerPath & "\" & _
                            System.IO.Path.GetFileNameWithoutExtension(webServerFile) & AppConfig.UnixServer.FtpTriggerExtension
            CreateFolder(webServerPath)
            System.IO.File.WriteAllBytes(webServerFile, fileBytes)
            'System.IO.File.WriteAllBytes(webServerPath & "\" & System.IO.Path.GetFileNameWithoutExtension(webServerFile), System.Text.Encoding.ASCII.GetBytes(TheState.SelectedClaimFileLayout))
            System.IO.File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes(TheState.SelectedClaimFileLayout))
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
            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
        End Sub

        Private Sub EnableDisableButtons()
            If Not TheState.SelectedClaimFileProcessedId.Equals(Guid.Empty) Then
                Dim oClaimFile As ClaimFileProcessed = New ClaimFileProcessed(TheState.SelectedClaimFileProcessedId)
                DisableButtons()
                With oClaimFile
                    If .Received.Value = .Counted.Value Then ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, True)
                    If .Received.Value = .Validated.Value Then ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)

                    If .Rejected.Value > 0 Then
                        ControlMgr.SetEnableControl(ThePage, BtnRejectReport, True)
                    Else
                        ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                    End If

                    If .Validated.Value > 0 Then ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, True)

                    If .Received.Value = .Loaded.Value Then
                        '   BtnDeleteDealerFile_WRITE.Enabled = True
                        ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, False)
                        ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                    End If

                    If .Loaded.Value > 0 Then
                        ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, True)
                    Else
                        ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                    End If

                    'If (.Loaded.Value = .Counted.Value) OrElse (.Loaded.Value = 0) OrElse _
                    '   (.Bypassed.Value > 0) Then
                    '    ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, True)
                    'End If

                    If (.Loaded.Value = 0) OrElse (.Received.Value = .Bypassed.Value + .Loaded.Value) Then
                        ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, True)
                    End If

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
            If TheState.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM Then
                moDataGrid.Columns(GRID_COL_PROCESSED_AMOUNT_IDX).Visible = True
            End If
        End Sub

        Public Sub EnableDisableDropdowns()
            If TheState.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM Then
                ControlMgr.SetVisibleControl(ThePage, moDealerMultipleDrop, True)
                ControlMgr.SetVisibleControl(ThePage, moClaimInterfaceDrop, False)
                ControlMgr.SetVisibleControl(ThePage, moClaimInterfaceLabel, False)
                ControlMgr.SetVisibleControl(ThePage, ClaimInterfacePanel, True)
            Else
                ControlMgr.SetVisibleControl(ThePage, moDealerMultipleDrop, False)
                ControlMgr.SetVisibleControl(ThePage, moClaimInterfaceDrop, False)
                ControlMgr.SetVisibleControl(ThePage, moClaimInterfaceLabel, False)
                ControlMgr.SetVisibleControl(ThePage, ClaimInterfacePanel, False)
            End If

        End Sub

        Private Sub ClearSelectedClaimFile(oAction As String)
            moDataGrid.SelectedIndex = ThePage.NO_ITEM_SELECTED_INDEX
            DisableButtons()
            TheState.SelectedClaimFileProcessedId = Guid.Empty
            PopulateGrid(oAction)
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearAll()
            moDataGrid.CurrentPageIndex = ThePage.NO_PAGE_INDEX
            moDataGrid.DataSource = Nothing
            moDataGrid.DataBind()
            TheState.SelectedClaimFileProcessedId = Guid.Empty
            TheState.SelectedDealerCode = ""
            TheState.SelectedClaimFileLayout = ""
            moExpectedFileLabel_NO_TRANSLATE.Text = String.Empty
            DisableButtons()
            ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, False, True)
            ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, False, True)
        End Sub

#End Region


#Region "Populate"
        Sub PopulateDealerDropDown()
            Try
                Dim oCompanyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
                Dim dv As DataView
                dv = LookupListNew.GetDealerLookupList(oCompanyIds, False, "Code", "CODE")
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(True, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True)

                DealerMultipleDrop.SelectedGuid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALERS, TheState.SelectedDealerCode)
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Sub PopulateClaimInterfaceDropDown()
            Try
                Dim InterfaceCode As String
                Dim ListCode As String
                'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
                'Dim dv As DataView

                Dim oListContext As New ListContext
                Dim contextListCode As String = String.Empty
                Dim splitSystemTranslationList As New Collections.Generic.List(Of DataElements.ListItem)
                Dim filteredSplitSystemTranslationList As New Collections.Generic.List(Of DataElements.ListItem)

                Select Case TheState.moInterfaceTypeCode
                    Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM, ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM_HP
                        InterfaceCode = INTERFACE_CODE_NEW_CLAIM
                        ListCode = INTERFACE_LIST_CODE_NEW_CLAIM
                        contextListCode = "NewClaimSplitSystemTranslationByCompany"
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM, ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM
                        InterfaceCode = INTERFACE_CODE_CLOSE_CLAIM
                        ListCode = INTERFACE_LIST_CODE_CLOSE_CLAIM
                        contextListCode = "CloseClaimSplitSystemTranslationByCompany"
                    Case Else
                        Return
                End Select

                For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    oListContext.CompanyId = _company
                    oListContext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                    Dim splitSystemTranslationListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:=contextListCode, context:=oListContext, languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    If splitSystemTranslationListForCompany.Count > 0 Then
                        If splitSystemTranslationList IsNot Nothing Then
                            splitSystemTranslationList.AddRange(splitSystemTranslationListForCompany)
                        Else
                            splitSystemTranslationList = splitSystemTranslationListForCompany.Clone()
                        End If
                    End If
                Next

                filteredSplitSystemTranslationList = (From lst In splitSystemTranslationList
                                                      Where lst.ExtendedCode.StartsWith(InterfaceCode)
                                                      Select lst).ToList()

                If TheState.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM Then
                    'dv = LookupListNew.GetSplitSystemTranslationsLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies,
                    '                                                        InterfaceCode, langCode, ListCode)
                    'ThePage.BindListControlToDataView(moClaimInterfaceDrop, dv, , , False)

                    moClaimInterfaceDrop.Populate(filteredSplitSystemTranslationList.ToArray(), New PopulateOptions())
                Else
                    'dv = LookupListNew.GetSplitSystemTranslationsLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies,
                    '                                                        InterfaceCode, langCode, ListCode)
                    'ThePage.BindListControlToDataView(moClaimInterfaceDrop, dv, , , False)

                    moClaimInterfaceDrop.Populate(filteredSplitSystemTranslationList.ToArray(), New PopulateOptions())
                End If

                ThePage.BindSelectItem(TheState.SelectedClaimId.ToString, moClaimInterfaceDrop)

            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Function GetSplitSystemIDForNewFormat() As Guid
            Try

                Dim InterfaceCode As String
                Dim ListCode As String
                'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
                'Dim dv As DataView

                Dim oListContext As New ListContext
                Dim contextListCode As String = String.Empty
                Dim splitSystemTranslationList As New Collections.Generic.List(Of DataElements.ListItem)
                Dim filteredSplitSystemTranslationList As New Collections.Generic.List(Of DataElements.ListItem)

                Select Case TheState.moInterfaceTypeCode
                    Case ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM, ClaimFileProcessedData.InterfaceTypeCode.NEW_CLAIM_HP
                        InterfaceCode = INTERFACE_CODE_NEW_CLAIM
                        ListCode = INTERFACE_LIST_CODE_NEW_CLAIM
                        contextListCode = "NewClaimSplitSystemTranslationByCompany"
                    Case ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM, ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM
                        InterfaceCode = INTERFACE_CODE_CLOSE_CLAIM
                        ListCode = INTERFACE_LIST_CODE_CLOSE_CLAIM
                        contextListCode = "CloseClaimSplitSystemTranslationByCompany"
                    Case Else
                        Return Guid.Empty
                End Select

                For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    oListContext.CompanyId = _company
                    oListContext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                    Dim splitSystemTranslationListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:=contextListCode, context:=oListContext, languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                    If splitSystemTranslationListForCompany.Count > 0 Then
                        If splitSystemTranslationList IsNot Nothing Then
                            splitSystemTranslationList.AddRange(splitSystemTranslationListForCompany)
                        Else
                            splitSystemTranslationList = splitSystemTranslationListForCompany.Clone()
                        End If
                    End If
                Next

                If TheState.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM Then
                    'dv = LookupListNew.GetSplitSystemTranslationsLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies,
                    '       InterfaceCode, langCode, ListCode)

                    filteredSplitSystemTranslationList = (From lst In splitSystemTranslationList
                                                          Where lst.ExtendedCode.StartsWith(InterfaceCode)
                                                          Select lst).ToList()
                Else
                    'dv = LookupListNew.GetSplitSystemTranslationsLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies,
                    '       InterfaceCode, langCode, ListCode)
                    'dv.RowFilter = "system_code = '" & SYSTEM_CODE_SM & "'"

                    filteredSplitSystemTranslationList = (From lst In splitSystemTranslationList
                                                          Where lst.ExtendedCode.StartsWith(InterfaceCode + SYSTEM_CODE_SM)
                                                          Select lst).ToList()

                End If

                If filteredSplitSystemTranslationList.Any Then
                    Return filteredSplitSystemTranslationList.FirstOrDefault().ListItemId
                Else
                    Return Guid.Empty
                End If

                'If dv.Count > 0 Then
                '    Return New Guid(CType(dv.Item(0)("ID"), Byte()))
                'Else
                '    Return Guid.Empty
                'End If

            Catch ex As Exception
                Return Guid.Empty
            End Try
        End Function

        Private Function GetDataView() As DataView
            Dim oClaimFileData As ClaimFileProcessedData = New ClaimFileProcessedData
            Dim oDataView As DataView

            With oClaimFileData
                .splitSystemId = GetSplitSystemIDForNewFormat() 'ThePage.GetSelectedItem(moClaimInterfaceDrop)
                oDataView = ClaimFileProcessed.LoadList(oClaimFileData)
            End With

            Return oDataView
        End Function

        Private Function GetDataViewForADealer() As DataView
            Dim oClaimFileData As ClaimFileProcessedData = New ClaimFileProcessedData
            Dim oDataView As DataView, dealerCode As String

            With oClaimFileData
                .splitSystemId = ThePage.GetSelectedItem(moClaimInterfaceDrop)
                dealerCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, DealerMultipleDrop.SelectedGuid)
                oDataView = ClaimFileProcessed.LoadListForADealer(oClaimFileData, dealerCode)
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
                Case SYSTEM_CODE_XXX
                    Dim dateStr As String = System.DateTime.Now.ToString("yyMMdd")
                    FileName = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, DealerMultipleDrop.SelectedGuid) + "CL" + dateStr
            End Select
            FileName = FileName + FILE_EXTENSION
            Return FileName
        End Function

        Private Function GetSplitSystemIDByDealer_SUNCOM() As Guid
            Try
                Dim companyid As Guid = New Dealer(DealerMultipleDrop.SelectedGuid).CompanyId
                Dim InterfaceCode As String = INTERFACE_CODE_CLOSE_CLAIM
                Dim ListCode As String = INTERFACE_LIST_CODE_CLOSE_CLAIM
                Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Dim CompanyList As New ArrayList
                CompanyList.Add(companyid)
                Dim dv As DataView = LookupListNew.GetSplitSystemTranslationsLookupList(CompanyList, InterfaceCode, langCode, ListCode)
                If dv.Count > 0 Then
                    Return New Guid(CType(dv.Item(0)("ID"), Byte()))
                Else
                    Return Guid.Empty
                End If
            Catch ex As Exception
                Return Guid.Empty
            End Try
        End Function
        Private Sub SetExpectedFile()
            Dim PathFileName As String = String.Empty
            Dim SplitSystemId As Guid = GetSplitSystemIDForNewFormat() 'ThePage.GetSelectedItem(Me.moClaimInterfaceDrop)

            If TheState.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM Then 'AndAlso moClaimInterfaceDrop.Items.Count > 1 Then
                SplitSystemId = GetSplitSystemIDByDealer_SUNCOM()
                If SplitSystemId.Equals(Guid.Empty) Then
                    SplitSystemId = ThePage.GetSelectedItem(moClaimInterfaceDrop)
                Else
                    ThePage.BindSelectItem(SplitSystemId.ToString, moClaimInterfaceDrop)
                End If
            End If

            TheState.SelectedClaimId = SplitSystemId
            Dim oSplitSystem As SplitSystem = New SplitSystem(SplitSystemId)

            If Not SplitSystemId.Equals(Guid.Empty) Then
                TheState.SelectedClaimFileLayout = oSplitSystem.Layout
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
                If TheState.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM Then
                    oDataView = GetDataViewForADealer()
                Else
                    oDataView = GetDataView()
                End If
                ThePage.BasePopulateGrid(moDataGrid, oDataView, TheState.SelectedClaimFileProcessedId, oAction)
                ThePage.SetPageAndSelectedIndexFromGuid(oDataView, TheState.SelectedClaimFileProcessedId, moDataGrid, TheState.mnPageIndex)
                EnableDisableEditControl()
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region


    End Class



End Namespace
