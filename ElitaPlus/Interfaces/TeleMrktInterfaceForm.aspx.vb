Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Partial Public Class TeleMrktInterfaceForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "Interfaces/TeleMrktInterfaceForm.aspx"
    Public Const PAGETITLE As String = "TELEMARKETING_INTERFACE"
    Public Const PAGETAB As String = "INTERFACES"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Private Const SP_VALIDATE As Integer = 0
    Private Const SP_PROCESS As Integer = 1
    Private Const SP_DELETE As Integer = 2
    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"

    Private Const FTP_PORT As Integer = 21
    Private Const TELE_MRKT_FILE_SUBSTRING As String = "TMK"
    Private Const GRID_COL_ID As Integer = 0

    Private Const GRID_COL_FILE_NAME As Integer = 2
    Private Const GRID_COL_REJECT As Integer = 5
    Public Const SESSION_LOCALSTATE_KEY As String = "DEALERFILE_PROCESSEDCONTROLLER_SESSION_LOCALSTATE_KEY"

#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Public Class MyState
        Public MyBO As DealerTmkReconWrk
        Public SelectedDealerFileProcessedId As Guid = Guid.Empty
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As DataView = Nothing
        Public intStatusId As Guid
        Public selectedFileName As String
        Public selectedFildID As Guid
        Public selectedRow As Integer
        Public SelectedDealerFileLayout As String = ""
        Public SelectedDealerCode As String = ""
        Public IsGridVisible As Boolean = False
        Public SelectedDealerId As Guid = Guid.Empty
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

#Region "Page events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                TranslateGridHeader(Grid)
                PopulateDealer()
                If IsReturningFromChild = True Then
                    EnableDisableButtons()
                End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        InstallDisplayProgressBar()
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

#Region "Page Return"
    Private IsReturningFromChild As Boolean = False
    Public Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object, Optional ByVal DealerCode As String = "") Handles Me.PageReturn
        Me.IsReturningFromChild = True
        Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
        Select Case retObj.LastOperation
            Case ElitaPlusPage.DetailPageCommand.Back
                If Not retObj Is Nothing Then
                    Try
                        State.selectedFildID = retObj.SelectedFileID
                        State.selectedFileName = retObj.SelectedFileName

                        If State.selectedFileName = String.Empty AndAlso (Not State.selectedFildID.Equals(Guid.Empty)) Then
                            Dim i As Integer
                            With State
                                For i = 0 To .searchDV.Count - 1
                                    If (New Guid(CType(.searchDV(i)(DealerFileProcessedDAL.COL_NAME_DEALERFILE_PROCESSED_ID), Byte())).Equals(.selectedFildID)) Then
                                        .selectedFileName = .searchDV(i)(DealerFileProcessedDAL.COL_NAME_FILENAME).ToString
                                        .SelectedDealerId = New Guid(CType(.searchDV(i)(DealerFileProcessedDAL.COL_NAME_DEALER_ID), Byte()))
                                        Exit For
                                    End If
                                Next
                            End With
                        End If

                        Grid.PageIndex = State.PageIndex
                    Catch ex As Exception
                        HandleErrors(ex, Me.ErrControllerMaster)
                    End Try
                End If
        End Select
    End Sub

    Public Class ReturnType
        Public LastOperation As ElitaPlusPage.DetailPageCommand
        Public SelectedFileName As String
        Public SelectedFileID As Guid
        Public SelectedDealerCode As String = ""
        Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal selFileName As String)
            Me.LastOperation = LastOp
            Me.SelectedFileName = selFileName
            Me.SelectedFileID = Guid.Empty
        End Sub

        Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal selFileID As Guid)
            Me.LastOperation = LastOp
            Me.SelectedFileName = String.Empty
            Me.SelectedFileID = selFileID
        End Sub
    End Class

#End Region

#End Region

#Region "Variables"
    Private moState As MyState
#End Region

#Region "properties"
    Private _ProgBarBaseController As String
    Public ReadOnly Property TheInterfaceProgress() As Interfaces.InterfaceProgressControl
        Get
            If moInterfaceProgressControl Is Nothing Then
                moInterfaceProgressControl = CType(FindControl("moInterfaceProgressControl"), Interfaces.InterfaceProgressControl)
            End If
            Return moInterfaceProgressControl
        End Get
    End Property
    Protected ReadOnly Property TheState() As MyState
        Get
            Try
                If Me.moState Is Nothing Then
                    Me.moState = CType(Session(SESSION_LOCALSTATE_KEY), MyState)
                End If
                Return Me.moState
            Catch ex As Exception
                'When we are in design mode there is no session object
                Return Nothing
            End Try
        End Get
    End Property
    Public ReadOnly Property ProgressBarBaseController() As String
        Get
            If _ProgBarBaseController = String.Empty Then
                _ProgBarBaseController = moInterfaceProgressControl.ClientID.Replace("moInterfaceProgressControl", "")
            End If
            Return _ProgBarBaseController
        End Get
    End Property

    Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
        Get
            If multipleDropControl Is Nothing Then
                multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Return multipleDropControl
        End Get
    End Property
#End Region

#Region "Button event handlers and helper functions"
    Function SetParameters(ByVal intStatusId As Guid, ByVal baseController As String) As Interfaces.InterfaceBaseForm.Params
        Dim params As New Interfaces.InterfaceBaseForm.Params
        With params
            .intStatusId = intStatusId
            .baseController = baseController
        End With
        Return params
    End Function

    Private Sub AfterProgressBar()
        ClearSelectedTeleMrktFile()
        DisplayMessage(Message.MSG_INTERFACES_HAS_COMPLETED, "", MSG_BTN_OK, MSG_TYPE_INFO)
    End Sub

    Private Sub ExecuteAndWait(ByVal oSP As Integer)
        Dim intStatus As InterfaceStatusWrk
        Dim params As Interfaces.InterfaceBaseForm.Params

        Try
            ExecuteSp(oSP)
            params = SetParameters(State.intStatusId, ProgressBarBaseController)
            Session(Interfaces.InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
            TheInterfaceProgress.EnableInterfaceProgress(ProgressBarBaseController)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
    Private Sub SetExpectedFile()
        Dim sFileName As String = String.Empty
        Dim oDealerId As Guid = TheDealerControl.SelectedGuid
        Dim objDealer As New Dealer(oDealerId)
        State.SelectedDealerId = oDealerId
        Dim sDirectory As String
        If Not oDealerId.Equals(Guid.Empty) Then
            sDirectory = AppConfig.FileClientDirectory
            State.SelectedDealerCode = objDealer.Dealer
            Dim dateStr As String = System.DateTime.Now.ToString("yyMMdd")
            sFileName = sDirectory & State.SelectedDealerCode.Trim() & TELE_MRKT_FILE_SUBSTRING _
                        & dateStr & ".TXT"

        End If
        moExpectedFileLabel_NO_TRANSLATE.Text = sFileName
    End Sub
    Private Sub uploadTeleMrktFile()
        Dim tmrktFileName As String
        Dim layoutFileName As String
        Dim fileLen As Integer = teleMrktFileInput.PostedFile.ContentLength

        DealerTmkReconWrk.ValidateFileName(fileLen)
        'tmrktFileName = MiscUtil.ReplaceSpaceByUnderscore(teleMrktFileInput.PostedFile.FileName)
        tmrktFileName = teleMrktFileInput.PostedFile.FileName
        Dim fileBytes(fileLen - 1) As Byte
        Dim objStream As System.IO.Stream
        objStream = teleMrktFileInput.PostedFile.InputStream
        objStream.Read(fileBytes, 0, fileLen)

        Dim webServerPath As String = MiscUtil.GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
        Dim webServerFile As String = webServerPath & "\" & System.IO.Path.GetFileName(tmrktFileName)
        layoutFileName = webServerPath & "\" & _
                        System.IO.Path.GetFileNameWithoutExtension(webServerFile) & AppConfig.UnixServer.FtpTriggerExtension
        MiscUtil.CreateFolder(webServerPath)
        System.IO.File.WriteAllBytes(webServerFile, fileBytes)
        System.IO.File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes("gen_tm"))
        Dim unixPath As String = AppConfig.UnixServer.FtpDirectory
        '' ''Dim objUnixFTP As New Assurant.Common.Ftp.aFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, _
        '' ''                     AppConfig.UnixServer.Password, FTP_PORT)
        Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, _
                                 AppConfig.UnixServer.Password)
        Try
            '' ''If (objUnixFTP.Login()) Then
            '' ''    objUnixFTP.UploadFile(webServerFile, False)
            '' ''    objUnixFTP.UploadFile(layoutFileName, False)
            '' ''End If
            objUnixFTP.UploadFile(webServerFile)
            objUnixFTP.UploadFile(layoutFileName)
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        Finally
            '' ''objUnixFTP.CloseConnection()
        End Try

    End Sub

    Private Sub ExecuteSp(ByVal oSP As Integer)
        If State.selectedFileName <> String.Empty Then
            If InterfaceStatusWrk.IsfileBeingProcessed(State.selectedFileName) Then
                Select Case oSP
                    Case SP_VALIDATE
                        State.intStatusId = DealerTmkReconWrk.ValidateFile(State.selectedFileName)
                    Case SP_PROCESS
                        State.intStatusId = DealerTmkReconWrk.ProcessFile(State.selectedFileName)
                    Case SP_DELETE
                        State.intStatusId = DealerTmkReconWrk.DeleteFile(State.selectedFileName)
                End Select
            Else
                Throw New GUIException("File is Been Processed", Assurant.ElitaPlus.Common.ErrorCodes.ERR_INTERFACE_FILE_IN_PROCESS)
            End If
        Else
            Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
        End If
    End Sub

    Private Sub btnCopyDealerFile_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyDealerFile_WRITE.Click
        Try
            uploadTeleMrktFile()
            DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", MSG_BTN_OK, MSG_TYPE_INFO)
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub BtnDelete_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete_WRITE.Click
        ExecuteAndWait(SP_DELETE)
    End Sub

    Private Sub BtnProcess_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnProcess_WRITE.Click
        'ExecuteAndWait(SP_PROCESS)
    End Sub

    Private Sub BtnValidate_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnValidate_WRITE.Click
        ExecuteAndWait(SP_VALIDATE)
    End Sub

    Private Sub btnAfterProgressBar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAfterProgressBar.Click
        AfterProgressBar()
    End Sub

    Private Function GetFileIDByFileName(ByVal strFileName As String) As Guid
        Dim fileID As Guid = Guid.Empty
        If (Not State.searchDV Is Nothing) AndAlso (strFileName <> String.Empty) Then
            For i As Integer = 0 To State.searchDV.Count
                If State.searchDV.Item(i)(DealerFileProcessedDAL.COL_NAME_FILENAME).ToString = strFileName Then
                    fileID = New Guid(CType(State.searchDV.Item(i)(DealerFileProcessedDAL.COL_NAME_DEALERFILE_PROCESSED_ID), Byte()))
                    Exit For
                End If
            Next
        End If
        Return fileID
    End Function
#End Region

#Region "Helper functions"
    Public Shared Function AddNewRowToEmptySearchDV(ByVal dv As DataView) As DataView
        If dv.Count > 0 Then
            AddNewRowToEmptySearchDV = dv
        Else
            Dim dt As DataTable
            Dim guidTemp As New Guid

            Dim row As DataRow
            If dv Is Nothing Then
                dt = New DataTable
                dt.Columns.Add(DealerFileProcessedDAL.COL_NAME_DEALERFILE_PROCESSED_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(DealerFileProcessedDAL.COL_NAME_FILENAME, GetType(String))
                dt.Columns.Add(DealerFileProcessedDAL.COL_NAME_RECEIVED, GetType(Integer))
                dt.Columns.Add(DealerFileProcessedDAL.COL_NAME_COUNTED, GetType(Integer))
                dt.Columns.Add(DealerFileProcessedDAL.COL_NAME_VALIDATED, GetType(Integer))
                dt.Columns.Add(DealerFileProcessedDAL.COL_NAME_REJECTED, GetType(Integer))
                dt.Columns.Add(DealerFileProcessedDAL.COL_NAME_LOADED, GetType(Integer))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(DealerFileProcessedDAL.COL_NAME_DEALERFILE_PROCESSED_ID) = guidTemp.ToByteArray
            row(DealerFileProcessedDAL.COL_NAME_FILENAME) = String.Empty
            row(DealerFileProcessedDAL.COL_NAME_RECEIVED) = 0
            row(DealerFileProcessedDAL.COL_NAME_COUNTED) = 0
            row(DealerFileProcessedDAL.COL_NAME_VALIDATED) = 0
            row(DealerFileProcessedDAL.COL_NAME_REJECTED) = 0
            row(DealerFileProcessedDAL.COL_NAME_LOADED) = 0
            dt.Rows.Add(row)
            AddNewRowToEmptySearchDV = dt.DefaultView
        End If
    End Function

    Private Sub PopulateDealer()
        Try
            Dim oCompanyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(oCompanyIds, False, "Code", "CODE")
            TheDealerControl.SetControl(True, TheDealerControl.MODES.NEW_MODE, True, oDealerview, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE), True)
            If Not Me.State.SelectedDealerId.Equals(Guid.Empty) Then
                TheDealerControl.SelectedGuid = Me.State.SelectedDealerId
                PopulateGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub PopulateGrid()
        Dim oDealerFileData As DealerFileProcessedData = New DealerFileProcessedData
        Try
            If TheDealerControl.SelectedIndex > BLANK_ITEM_SELECTED Then
                SetExpectedFile()
                With oDealerFileData
                    .dealerCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, TheDealerControl.SelectedGuid)
                    .fileTypeCode = DealerFileProcessedData.InterfaceTypeCode.TLMK
                End With

                With State
                    .searchDV = DealerFileProcessed.LoadList(ElitaPlusIdentity.Current.ActiveUser.Companies, oDealerFileData)
                End With

                If State.searchDV.Count = 0 Then
                    ControlMgr.SetVisibleControl(Me, pnlPagesize, False)
                    Dim dt As DataTable = State.searchDV.Table.Clone()
                    Dim dv As DataView = AddNewRowToEmptySearchDV(State.searchDV)
                    SetPageAndSelectedIndexFromGuid(dv, Nothing, Me.Grid, Me.State.PageIndex, False)
                    SortAndBindGrid(dv, True)
                Else
                    ControlMgr.SetVisibleControl(Me, pnlPagesize, True)
                    SetPageAndSelectedIndexFromGuid(Me.State.searchDV, State.selectedFildID, Me.Grid, Me.State.PageIndex, False)
                    SortAndBindGrid(State.searchDV)
                    SetGridSeletedIndex()
                End If

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
    Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl) _
            Handles multipleDropControl.SelectedDropChanged
        Try
            PopulateGrid()
            EnableDisableAllButtons(False)
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
    Private Sub SortAndBindGrid(ByVal dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
        Me.Grid.DataSource = dvBinding
        Me.Grid.DataBind()
        If Me.State.searchDV.Count > 0 Then
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If

        If blnEmptyList Then
            For Each gvRow As GridViewRow In Grid.Rows
                gvRow.Visible = False
                gvRow.Controls.Clear()
            Next
        End If
    End Sub

    Private Sub ClearSelectedTeleMrktFile()
        Grid.SelectedIndex = GRID_NO_SELECTEDITEM_INX
        EnableDisableAllButtons(False)
        State.selectedRow = -1
        State.selectedFileName = String.Empty
        State.searchDV = Nothing
        Me.PopulateGrid()
    End Sub

    Private Sub EnableDisableAllButtons(ByVal blnEnabled As Boolean)
        ControlMgr.SetEnableControl(Me, BtnValidate_WRITE, blnEnabled)
        ControlMgr.SetEnableControl(Me, BtnProcess_WRITE, blnEnabled)
        ControlMgr.SetEnableControl(Me, BtnDelete_WRITE, blnEnabled)
    End Sub

    Private Sub EnableDisableButtons()
        Dim drv As DataRowView, blnFound As Boolean = False
        Dim intReceived, intRejected, intValidated, intLoaded, intCounted As Integer
        If (Not State.selectedFileName = String.Empty) AndAlso (Not State.searchDV Is Nothing) Then
            For i As Integer = 0 To State.searchDV.Count
                drv = State.searchDV.Item(i)
                If drv(DealerFileProcessedDAL.COL_NAME_FILENAME).ToString = State.selectedFileName Then
                    intReceived = CInt(drv(DealerFileProcessedDAL.COL_NAME_RECEIVED))
                    intCounted = CInt(drv(DealerFileProcessedDAL.COL_NAME_COUNTED))
                    intValidated = CInt(drv(DealerFileProcessedDAL.COL_NAME_VALIDATED))
                    intRejected = CInt(drv(DealerFileProcessedDAL.COL_NAME_REJECTED))
                    intLoaded = CInt(drv(DealerFileProcessedDAL.COL_NAME_LOADED))
                    blnFound = True
                    Exit For
                End If
            Next

            If blnFound Then
                EnableDisableAllButtons(False)
                If intReceived = intCounted Then ControlMgr.SetEnableControl(Me, BtnValidate_WRITE, True)

                If intReceived = intLoaded Then ControlMgr.SetEnableControl(Me, BtnValidate_WRITE, False)

                If intValidated > 0 Then ControlMgr.SetEnableControl(Me, BtnProcess_WRITE, True)

                If intLoaded = 0 Then
                    ControlMgr.SetEnableControl(Me, BtnDelete_WRITE, True)
                End If
            End If
        Else
            Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
        End If
    End Sub

    Sub SetGridSeletedIndex()
        Dim index As Integer, strFileName As String
        Try
            strFileName = State.selectedFileName
            If strFileName <> String.Empty Then
                For index = 0 To Grid.Rows.Count - 1
                    If strFileName = GetGridText(Grid, index, GRID_COL_FILE_NAME) Then
                        Grid.SelectedIndex = index
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Grid related"
    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try

            If e.CommandName = "EditAction" OrElse e.CommandName = "SelectRecord" Then
                Dim lblCtrl As Label
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                State.selectedRow = RowInd
                State.selectedFileName = Grid.Rows(RowInd).Cells(GRID_COL_FILE_NAME).Text
                State.selectedFildID = GetFileIDByFileName(State.selectedFileName)
                If e.CommandName = "EditAction" Then
                    State.PageIndex = Grid.PageIndex
                    Me.callPage("TeleMrktFileForm.aspx", State.selectedFildID)
                ElseIf e.CommandName = "SelectRecord" Then
                    Grid.SelectedIndex = RowInd
                    EnableDisableButtons()
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region


End Class