Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Public Class ClaimLoadForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "Interfaces/ClaimLoadForm.aspx"
    Public Const PAGETITLE As String = "CLAIM_FILE"
    Public Const PAGETAB As String = "INTERFACES"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Private Const SP_VALIDATE As Integer = 0
    Private Const SP_PROCESS As Integer = 1
    Private Const SP_DELETE As Integer = 2
    Private Const FTP_PORT As Integer = 21

    Private Const GRID_COL_FILE_NAME As Integer = 2
    Private Const GRID_COL_REJECT As Integer = 5
    Private Const GRID_COL_FILETYPE As Integer = 8
    Private Const TELEFONICA_COMPANY_GROUP_CODE As String = "TISA"
    Private Const BRAZIL_COMPANY_GROUP_CODE As String = "ABRG"
#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As ClaimFileProcessed
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As DataView = Nothing
        Public intStatusId As Guid
        Public selectedFileName As String
        Public selectedFileID As Guid
        Public selectedFileType As String
        Public selectedRow As Integer
        Public selectedCountry As Guid
        Public selectFileType As Guid
        Public FilenameSearch As String
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
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ErrControllerMaster.Clear_Hide()
        Try
            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                PopulateDropdowns()
                TranslateGridHeader(Grid)
                PopulateGrid()
                PopulateControlsFromState()
                If IsReturningFromChild = True Then
                    EnableDisableButtons()
                End If
            End If
            DisplaySearchPanel()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        InstallDisplayProgressBar()
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

#Region "Page Return"
    Private IsReturningFromChild As Boolean = False

    Public Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object, Optional ByVal DealerCode As String = "") Handles Me.PageReturn
        IsReturningFromChild = True
        Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
        Select Case retObj.LastOperation
            Case ElitaPlusPage.DetailPageCommand.Back
                If retObj IsNot Nothing Then
                    Try
                        State.selectedFileID = retObj.SelectedClaimFileID
                        State.selectedFileName = retObj.SelectedClaimFileName

                        If State.selectedFileName = String.Empty AndAlso (Not State.selectedFileID.Equals(Guid.Empty)) Then
                            Dim i As Integer
                            With State
                                For i = 0 To .searchDV.Count - 1
                                    If (New Guid(CType(.searchDV(i)(ClaimloadFileProcessed.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID), Byte())).Equals(.selectedFileID)) Then
                                        .selectedFileName = .searchDV(i)(ClaimloadFileProcessed.COL_NAME_FILENAME).ToString
                                        Exit For
                                    End If
                                Next
                            End With
                        End If

                        Grid.PageIndex = State.PageIndex
                    Catch ex As Exception
                        HandleErrors(ex, ErrControllerMaster)
                    End Try
                End If
        End Select
    End Sub

    Public Class ReturnType
        Public LastOperation As ElitaPlusPage.DetailPageCommand
        Public SelectedClaimFileName As String
        Public SelectedClaimFileID As Guid
        Public SelectedDealerCode As String = ""
        Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, selClaimFileName As String)
            LastOperation = LastOp
            SelectedClaimFileName = selClaimFileName
            SelectedClaimFileID = Guid.Empty
        End Sub

        Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, selClaimFileID As Guid)
            LastOperation = LastOp
            SelectedClaimFileName = String.Empty
            SelectedClaimFileID = selClaimFileID
        End Sub
    End Class
#End Region

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

    Public ReadOnly Property ProgressBarBaseController() As String
        Get
            If _ProgBarBaseController = String.Empty Then
                _ProgBarBaseController = moInterfaceProgressControl.ClientID.Replace("moInterfaceProgressControl", "")
            End If
            Return _ProgBarBaseController
        End Get
    End Property
#End Region

#Region "Button event handlers and helper functions"
    Function SetParameters(intStatusId As Guid, baseController As String) As Interfaces.InterfaceBaseForm.Params
        Dim params As New Interfaces.InterfaceBaseForm.Params
        With params
            .intStatusId = intStatusId
            .baseController = baseController
        End With
        Return params
    End Function

    Private Sub AfterProgressBar()
        ClearSelectedClaimFile()
        DisplayMessage(Message.MSG_INTERFACES_HAS_COMPLETED, "", MSG_BTN_OK, MSG_TYPE_INFO)
    End Sub

    Private Sub ExecuteAndWait(oSP As Integer)
        Dim intStatus As InterfaceStatusWrk
        Dim params As Interfaces.InterfaceBaseForm.Params

        Try
            ExecuteSp(oSP)
            params = SetParameters(State.intStatusId, ProgressBarBaseController)
            Session(Interfaces.InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
            TheInterfaceProgress.EnableInterfaceProgress(ProgressBarBaseController)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub uploadClaimFile()
        Dim claimFileName As String
        Dim layoutFileName As String
        Dim fileLen As Integer = claimFileInput.PostedFile.ContentLength

        ClaimloadFileProcessed.ValidateFileName(fileLen)
        'claimFileName = MiscUtil.ReplaceSpaceByUnderscore(claimFileInput.PostedFile.FileName)
        claimFileName = claimFileInput.PostedFile.FileName
        Dim fileBytes(fileLen - 1) As Byte
        Dim objStream As System.IO.Stream
        objStream = claimFileInput.PostedFile.InputStream
        objStream.Read(fileBytes, 0, fileLen)

        Dim webServerPath As String = MiscUtil.GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
        Dim webServerFile As String = webServerPath & "\" & System.IO.Path.GetFileName(claimFileName)
        layoutFileName = webServerPath & "\" &
                        System.IO.Path.GetFileNameWithoutExtension(webServerFile) & AppConfig.UnixServer.FtpTriggerExtension
        MiscUtil.CreateFolder(webServerPath)
        System.IO.File.WriteAllBytes(webServerFile, fileBytes)
        '' ''DEF-24620 Start
        If (AppConfig.HubRegion.ToUpper() = "C1" Or AppConfig.HubRegion.ToUpper() = "C2" Or AppConfig.HubRegion.ToUpper() = "AS") Then
            System.IO.File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes("gen_clc"))
        Else
            Dim dv As DataView
            Dim oFileInfo As New System.IO.FileInfo(claimFileName)
            Dim ofileName As String = oFileInfo.Name
            dv = TransallMapping.GetList(ofileName)

            If dv.Count > 0 Then
                System.IO.File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes("NA"))
            Else
                System.IO.File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes("gen_cl"))
            End If
        End If
        '' ''System.IO.File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes("gen_cl"))
        '' ''DEF-24620 End
        Dim unixPath As String = AppConfig.UnixServer.FtpDirectory
        '' ''Dim objUnixFTP As New Assurant.Common.Ftp.aFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, _
        '' ''                     AppConfig.UnixServer.Password, FTP_PORT)
        Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId,
                                 AppConfig.UnixServer.Password)
        Try
            '' ''If (objUnixFTP.Login()) Then
            '' ''    objUnixFTP.UploadFile(webServerFile, False)
            '' ''    objUnixFTP.UploadFile(layoutFileName, False)
            '' ''End If
            objUnixFTP.UploadFile(webServerFile)
            objUnixFTP.UploadFile(layoutFileName)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        Finally
            '' ''objUnixFTP.CloseConnection()
        End Try

    End Sub

    Private Sub ExecuteSp(oSP As Integer)
        If State.selectedFileName <> String.Empty Then
            If InterfaceStatusWrk.IsfileBeingProcessed(State.selectedFileName) Then
                Select Case oSP
                    Case SP_VALIDATE
                        State.intStatusId = ClaimloadFileProcessed.ValidateFile(State.selectedFileName)
                    Case SP_PROCESS
                        Select Case State.selectedFileType
                            Case Codes.CLAIM_LOAD_FILE_TYPE__VENDOR_INVOICE
                                Dim viFileLoad As New InvoiceFileLoad()
                                State.intStatusId = viFileLoad.ProcessAsync(State.selectedFileID)
                            Case Codes.CLAIM_LOAD_FILE_TYPE__REPAIR_AND_LOGISTIC_AUTHORIZATIONS
                                Dim viFileLoad As New RepairLogisticAuthorizationFileLoad()
                                State.intStatusId = viFileLoad.ProcessAsync(State.selectedFileID)
                            Case Codes.CLAIM_LOAD_FILE_TYPE__VENDOR_AUTHORIZATIONS
                                Dim viFileLoad As New VendorAuthorizationFileLoad()
                                State.intStatusId = viFileLoad.ProcessAsync(State.selectedFileID)
                            Case Codes.CLAIM_LOAD_FILE_TYPE__INVENTORY_MGMT
                                Dim imFileLoad As New InvMgmtFileLoad()
                                State.intStatusId = imFileLoad.ProcessAsync(State.selectedFileID)
                            Case Else
                                State.intStatusId = ClaimloadFileProcessed.ProcessFile(State.selectedFileName)
                        End Select

                    Case SP_DELETE
                        State.intStatusId = ClaimloadFileProcessed.DeleteFile(State.selectedFileName)
                End Select
            Else
                Throw New GUIException("File is Been Process", Assurant.ElitaPlus.Common.ErrorCodes.ERR_INTERFACE_FILE_IN_PROCESS)
            End If
        Else
            Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
        End If
    End Sub

    Private Sub btnCopyDealerFile_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCopyDealerFile_WRITE.Click
        Try
            uploadClaimFile()
            DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", MSG_BTN_OK, MSG_TYPE_INFO)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub BtnDelete_WRITE_Click(sender As Object, e As System.EventArgs) Handles BtnDelete_WRITE.Click
        ExecuteAndWait(SP_DELETE)
    End Sub

    Private Sub BtnProcess_WRITE_Click(sender As Object, e As System.EventArgs) Handles BtnProcess_WRITE.Click
        ExecuteAndWait(SP_PROCESS)
    End Sub


    Private Sub BtnRejectReport_Click(sender As Object, e As System.EventArgs) Handles BtnRejectReport.Click
        Dim param As New PrintClaimLoadRejectForm.MyState
        param.ClaimfileProcessedId = GetFileIDByFileName(State.selectedFileName)
        If State.selectedFileType = Codes.CLAIM_LOAD_FILE_TYPE__VENDOR_INVOICE Then
            param.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.INVOICE_LOAD_COMMON
        Else
            param.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLAIM_LOAD_COMMON
        End If
        callPage(PrintClaimLoadRejectForm.URL, param)
    End Sub

    Private Sub BtnValidate_WRITE_Click(sender As Object, e As System.EventArgs) Handles BtnValidate_WRITE.Click
        ExecuteAndWait(SP_VALIDATE)
    End Sub

    Private Sub btnAfterProgressBar_Click(sender As Object, e As System.EventArgs) Handles btnAfterProgressBar.Click
        AfterProgressBar()
    End Sub

    Private Function GetFileIDByFileName(strFileName As String) As Guid
        Dim fileID As Guid = Guid.Empty
        If (State.searchDV IsNot Nothing) AndAlso (strFileName <> String.Empty) Then
            For i As Integer = 0 To State.searchDV.Count
                If State.searchDV.Item(i)(ClaimloadFileProcessed.COL_NAME_FILENAME).ToString = strFileName Then
                    fileID = New Guid(CType(State.searchDV.Item(i)(ClaimloadFileProcessed.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID), Byte()))
                    Exit For
                End If
            Next
        End If
        Return fileID
    End Function

    Private Sub moBtnSearch_Click(sender As Object, e As System.EventArgs) Handles moBtnSearch.Click
        Try
            State.PageIndex = 0
            State.searchDV = Nothing
            SetStateFromControls()
            PopulateGrid()
        Catch ex As Exception
            'Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub moBtnClearSearch_Click(sender As Object, e As System.EventArgs) Handles moBtnClearSearch.Click
        Try
            ddlCountry.SelectedIndex = BLANK_ITEM_SELECTED
            ddlFileType.SelectedIndex = BLANK_ITEM_SELECTED
            FileNameTextBox.Text = String.Empty

            'BillingPlanTextBox.Text = String.Empty



        Catch ex As Exception
            'Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region
#Region "Helper functions"
    Public Shared Function AddNewRowToEmptySearchDV(dv As DataView) As DataView
        If dv.Count > 0 Then
            AddNewRowToEmptySearchDV = dv
        Else
            Dim dt As DataTable
            Dim guidTemp As New Guid

            Dim row As DataRow
            If dv Is Nothing Then
                dt = New DataTable
                dt.Columns.Add(ClaimloadFileProcessed.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimloadFileProcessed.COL_NAME_FILENAME, GetType(String))
                dt.Columns.Add(ClaimloadFileProcessed.COL_NAME_RECEIVED, GetType(Integer))
                dt.Columns.Add(ClaimloadFileProcessed.COL_NAME_COUNTED, GetType(Integer))
                dt.Columns.Add(ClaimloadFileProcessed.COL_NAME_VALIDATED, GetType(Integer))
                dt.Columns.Add(ClaimloadFileProcessed.COL_NAME_REJECTED, GetType(Integer))
                dt.Columns.Add(ClaimloadFileProcessed.COL_NAME_LOADED, GetType(Integer))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(ClaimloadFileProcessed.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID) = guidTemp.ToByteArray
            row(ClaimloadFileProcessed.COL_NAME_FILENAME) = String.Empty
            row(ClaimloadFileProcessed.COL_NAME_RECEIVED) = 0
            row(ClaimloadFileProcessed.COL_NAME_COUNTED) = 0
            row(ClaimloadFileProcessed.COL_NAME_VALIDATED) = 0
            row(ClaimloadFileProcessed.COL_NAME_REJECTED) = 0
            row(ClaimloadFileProcessed.COL_NAME_LOADED) = 0
            dt.Rows.Add(row)
            AddNewRowToEmptySearchDV = dt.DefaultView
        End If
    End Function

    Public Sub DisplaySearchPanel()
        If ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Code = TELEFONICA_COMPANY_GROUP_CODE Then
            ControlMgr.SetVisibleControl(Me, trFileType, True)
            ControlMgr.SetVisibleControl(Me, trCountry, True)
        ElseIf ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Code = BRAZIL_COMPANY_GROUP_CODE Then
            ControlMgr.SetVisibleControl(Me, trFileType, True)
            ControlMgr.SetVisibleControl(Me, trCountry, False)
        Else
            ControlMgr.SetVisibleControl(Me, trFileType, False)
            ControlMgr.SetVisibleControl(Me, trCountry, False)
        End If
    End Sub

    Public Sub PopulateGrid()
        With State
            If (.searchDV Is Nothing) Then
                .searchDV = ClaimloadFileProcessed.LoadList(ElitaPlusIdentity.Current.ActiveUser.Id,
                                                            LookupListNew.GetCodeFromId(LookupListNew.GetCountryLookupList(),
                                                                                        New Guid(ddlCountry.SelectedValue)),
                                                            LookupListNew.GetCodeFromId(LookupListNew.GetClaimLoadFileTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId),
                                                                                        New Guid(ddlFileType.SelectedValue)),
                                                            FileNameTextBox.Text.Trim)
            End If
        End With

        If State.searchDV.Count = 0 Then
            Dim dt As DataTable = State.searchDV.Table.Clone()
            Dim dv As DataView = AddNewRowToEmptySearchDV(State.searchDV)
            SetPageAndSelectedIndexFromGuid(dv, Nothing, Grid, State.PageIndex, False)
            SortAndBindGrid(dv, True)
        Else
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedFileID, Grid, State.PageIndex, False)
            SortAndBindGrid(State.searchDV)
            SetGridSeletedIndex()
        End If
        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
    End Sub

    Private Sub SortAndBindGrid(dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
        Grid.DataSource = dvBinding
        Grid.DataBind()
        'If Me.State.searchDV.Count > 0 Then
        '    If Me.Grid.Visible Then
        '        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        '    End If
        'Else
        '    If Me.Grid.Visible Then
        '        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        '    End If
        'End If
        If blnEmptyList Then
            lblRecordCount.Text = "0 " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        Else
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

        If blnEmptyList Then
            For Each gvRow As GridViewRow In Grid.Rows
                gvRow.Visible = False
                gvRow.Controls.Clear()
            Next
        End If
    End Sub

    Private Sub ClearSelectedClaimFile()
        Grid.SelectedIndex = GRID_NO_SELECTEDITEM_INX
        EnableDisableAllButtons(False)
        State.selectedRow = -1
        State.selectedFileName = String.Empty
        State.searchDV = Nothing
        PopulateGrid()
    End Sub

    Private Sub EnableDisableAllButtons(blnEnabled As Boolean)
        ControlMgr.SetEnableControl(Me, BtnValidate_WRITE, blnEnabled)
        ControlMgr.SetEnableControl(Me, BtnProcess_WRITE, blnEnabled)
        ControlMgr.SetEnableControl(Me, BtnDelete_WRITE, blnEnabled)
        ControlMgr.SetEnableControl(Me, BtnRejectReport, blnEnabled)
    End Sub

    Private Sub EnableDisableButtons()
        Dim drv As DataRowView, blnFound As Boolean = False
        Dim intReceived, intRejected, intValidated, intLoaded, intCounted As Integer
        If (Not State.selectedFileName = String.Empty) AndAlso (State.searchDV IsNot Nothing) Then
            For i As Integer = 0 To State.searchDV.Count
                drv = State.searchDV.Item(i)
                If drv(ClaimloadFileProcessed.COL_NAME_FILENAME).ToString = State.selectedFileName Then
                    intReceived = CInt(drv(ClaimloadFileProcessed.COL_NAME_RECEIVED))
                    intCounted = CInt(drv(ClaimloadFileProcessed.COL_NAME_COUNTED))
                    intValidated = CInt(drv(ClaimloadFileProcessed.COL_NAME_VALIDATED))
                    intRejected = CInt(drv(ClaimloadFileProcessed.COL_NAME_REJECTED))
                    intLoaded = CInt(drv(ClaimloadFileProcessed.COL_NAME_LOADED))
                    blnFound = True
                    Exit For
                End If
            Next

            If blnFound Then
                EnableDisableAllButtons(False)
                If intReceived = intCounted Then ControlMgr.SetEnableControl(Me, BtnValidate_WRITE, True)

                If intReceived = intLoaded Then ControlMgr.SetEnableControl(Me, BtnValidate_WRITE, False)

                If intRejected > 0 Then ControlMgr.SetEnableControl(Me, BtnRejectReport, True)

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
        strFileName = State.selectedFileName
        If strFileName <> String.Empty Then
            For index = 0 To Grid.Rows.Count - 1
                If strFileName = GetGridText(Grid, index, GRID_COL_FILE_NAME) Then
                    Grid.SelectedIndex = index
                    Exit For
                End If
            Next
        End If
    End Sub

    Protected Sub PopulateDropdowns()
        'Me.BindListControlToDataView(Me.ddlCountry, LookupListNew.GetUserCountriesLookupList())
        'Me.BindListControlToDataView(Me.ddlFileType, LookupListNew.GetClaimLoadFileTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

        Dim CountryList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)

        Dim UserCountries As DataElements.ListItem() = (From Country In CountryList
                                                        Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                        Select Country).ToArray()

        ddlCountry.Populate(UserCountries.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

        Dim FileType As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="CLMLDFTYP",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        ddlFileType.Populate(FileType.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
    End Sub

    Protected Sub SetStateFromControls()
        If Not GetSelectedItem(ddlCountry).Equals(Guid.Empty) Then
            State.selectedCountry = GetSelectedItem(ddlCountry)
        End If
        If Not GetSelectedItem(ddlFileType).Equals(Guid.Empty) Then
            State.selectFileType = GetSelectedItem(ddlFileType)
        End If

        State.FilenameSearch = FileNameTextBox.Text
    End Sub
    Protected Sub PopulateControlsFromState()
        If Not State.selectedCountry.Equals(Guid.Empty) Then
            SetSelectedItem(ddlCountry, State.selectedCountry)
        End If
        If Not State.selectFileType.Equals(Guid.Empty) Then
            SetSelectedItem(ddlFileType, State.selectFileType)
        End If

        FileNameTextBox.Text = State.FilenameSearch
    End Sub
#End Region

#Region "Grid related"
    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = "EditAction" OrElse e.CommandName = "SelectRecord" Then
                Dim lblCtrl As Label
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                State.selectedRow = RowInd
                State.selectedFileName = Grid.Rows(RowInd).Cells(GRID_COL_FILE_NAME).Text
                State.selectedFileID = GetFileIDByFileName(State.selectedFileName)
                State.selectedFileType = Grid.Rows(RowInd).Cells(GRID_COL_FILETYPE).Text
                If e.CommandName = "EditAction" Then
                    State.PageIndex = Grid.PageIndex
                    Dim strTemp As String = Grid.Rows(RowInd).Cells(GRID_COL_REJECT).Text
                    If strTemp <> "0" Then
                        If State.selectedFileType = Codes.CLAIM_LOAD_FILE_TYPE__VENDOR_INVOICE Then
                            Dim param As InvoiceReconWrkForm.Parameters = New InvoiceReconWrkForm.Parameters()
                            param.selectedFileId = State.selectedFileID
                            param.selectedFileName = State.selectedFileName

                            callPage(InvoiceReconWrkForm.URL, param)
                        Else

                            callPage(ClaimLoadReconWrkForm.URL, State.selectedFileName)
                        End If
                    End If

                ElseIf e.CommandName = "SelectRecord" Then
                    Grid.SelectedIndex = RowInd
                    EnableDisableButtons()
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region


End Class