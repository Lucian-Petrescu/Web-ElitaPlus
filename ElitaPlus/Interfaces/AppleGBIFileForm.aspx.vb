Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.IO
Imports Assurant.Common.Ftp

Namespace Interfaces

    Public Class AppleGBIFileForm
        Inherits ElitaPlusSearchPage

#Region "Constants"

        Public PageTile As String = "Apple_GBI_File"
        Private Const LABEL_DEALER As String = "DEALER"
        Private Shared FILE_NAME_INVALID_CHARACTERS As String() = {"\", "/", ":", "*", "?", """", "<", ">", "|"} 'Operating system invalid characters

        Public Enum GridDefenitionEnum
            Filename = 0
            FileDate
            FileCreatedDate
            Received
            Counted
            NewClaims
            ClaimUpdate
            ProcessedStatus
            CancelledStatus
            PendingValidationStatus
            FailedStatus
            ReprocessStatus
            PendingClaimCreationStatus
            FileProcessedId
        End Enum

        Public Const COL_FILE_PROCESSED_ID As String = "FILE_PROCESSED_ID"
        Public Const COL_FILE_NAME As String = "FILE_NAME"
        Public Const COL_RECEIVED As String = "RECEIVED"
        Public Const COL_COUNTED As String = "COUNTED"
        Public Const COL_PROCESSED As String = "PROCESSED"
        Public Const COL_CANCELLED As String = "CANCELLED"
        Public Const COL_PENDING_VALIDATION As String = "PENDING_VALIDATION"
        Public Const COL_FAILED_REPROCESS As String = "FAILED_REPROCESS"
        Public Const COL_PENDING_CLAIM_CREATION As String = "PENDING_CLAIM_CREATION"
        Public Const COL_CREATED_DATE As String = "CREATED_DATE"
        REM -------------------
        Public Const GRID_COMMAND_SHOW_PROCESSED As String = "ShowProcessed"
        Public Const GRID_COMMAND_SHOW_CANCELLED As String = "ShowCancelled"
        Public Const GRID_COMMAND_SHOW_PENDING_VALIDATION As String = "ShowPendingValidation"
        Public Const GRID_COMMAND_SHOW_FAIL As String = "ShowFail"
        Public Const GRID_COMMAND_SHOW_PENDING_CREATION As String = "ShowPendingCreation"
        Public Const GRID_COMMAND_SHOW_REPROCESS As String = "ShowReprocess"
        REM -------------------
        Public Const STATUS_PROCESSED = "P"
        Public Const STATUS_CANCELLED = "X"
        Public Const STATUS_PENDING_VALIDATION = "V"
        Public Const STATUS_FAILED = "F"
        Public Const STATUS_PENDING_CLAIM_CREATION = "C"
        Public Const STATUS_REPROCESS = "R"
#End Region

#Region "Page Navigation"
        Private IsReturningFromChild As Boolean = False

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public FileProcessedId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, fileProcessedId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                Me.FileProcessedId = fileProcessedId
                Me.BoChanged = boChanged
            End Sub

        End Class

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Try
                IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                If retObj IsNot Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back

                            State.IsGridVisible = True
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)

                        Case Else

                    End Select
                    Grid.PageIndex = State.PageIndex
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Company Control"
        Protected WithEvents moCompanyMultipleDrop As MultipleColumnDDLabelControl
        Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moCompanyMultipleDrop Is Nothing Then
                    moCompanyMultipleDrop = CType(FindControl("drpCompany"), MultipleColumnDDLabelControl)
                End If
                Return moCompanyMultipleDrop
            End Get
        End Property
#End Region
#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public PageSize As Integer = 30
            Public PageSort As String
            REM ---------------
            Public SelectedDealerFileLayout As String = ""
            Public FileProcessedId As Guid
            Public CompanyCode As String
            Public DealerCode As String
            'Public FromDate As Date = DateTime.Today.AddMonths(-1)
            'Public ThruDate As Date = DateTime.Today
            'Public DealerId As Guid
            Public IsGridVisible As Boolean
            Public SelectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public IsReturningFromChild As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = $"{AppleGBIFileReconWrk.COL_NAME_FILE_NAME} desc "
            REM ---------------
            Public SearchedStartDate As Date = DateTime.Today.AddMonths(-1)
            Public SearchedEndDate As Date = DateTime.Today
            REM ---------------
            Public bnoRow As Boolean = False

            Public Sub New()
                Console.WriteLine("Building the state")
            End Sub

        End Class
#End Region


#Region "Page event handlers"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Interfaces")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PageTile)
                UpdateBreadCrum()

                If Not IsPostBack Then
                    InitializeCalendar()
                    PopulateSearchFieldsFromState()
                    TranslateGridHeader(Grid)

                    If Not State.IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Else
                        ControlMgr.SetVisibleControl(Me, trPageSize, True)
                    End If

                    If State.IsGridVisible Then
                        If Not (State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                            Grid.PageSize = State.SelectedPageSize
                        End If
                    End If
                    SetGridItemStyleColor(Grid)

                    If IsReturningFromChild Then
                        PopulateGrid(True)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

        End Sub

        Private Sub UpdateBreadCrum()

            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage(PageTile)
            End If

        End Sub

        Private Sub InitializeCalendar()
            AddCalendar(ImageBtnBeginDate, txtBeginDate)
            AddCalendar(ImageBbtnEndDate, txtEndDate)
        End Sub
#End Region

#Region "Constructor"


        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get

                Return CType(MyBase.State, MyState)

            End Get
        End Property

#End Region

#Region "Grid Handles"
        Public Property SortDirection() As String
            Get
                If ViewState("SortDirection") IsNot Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageIndex = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub Grid_RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try

                If e.CommandName = GRID_COMMAND_SHOW_PROCESSED OrElse
                   e.CommandName = GRID_COMMAND_SHOW_CANCELLED OrElse
                   e.CommandName = GRID_COMMAND_SHOW_PENDING_VALIDATION OrElse
                   e.CommandName = GRID_COMMAND_SHOW_FAIL OrElse
                   e.CommandName = GRID_COMMAND_SHOW_REPROCESS OrElse
                   e.CommandName = GRID_COMMAND_SHOW_PENDING_CREATION Then

                    Dim index As Integer = CInt(e.CommandArgument)
                    State.FileProcessedId = New Guid(Grid.Rows(index).Cells(GridDefenitionEnum.FileProcessedId).Text)
                    Dim filename As String = Grid.Rows(index).Cells(GridDefenitionEnum.Filename).Text

                    Dim parameters As AppleGBIFileFormDetail.PageParameters = New AppleGBIFileFormDetail.PageParameters()
                    If e.CommandName = GRID_COMMAND_SHOW_PROCESSED Then
                        parameters.Status = STATUS_PROCESSED
                    ElseIf e.CommandName = GRID_COMMAND_SHOW_CANCELLED Then
                        parameters.Status = STATUS_CANCELLED
                    ElseIf e.CommandName = GRID_COMMAND_SHOW_PENDING_VALIDATION Then
                        parameters.Status = STATUS_PENDING_VALIDATION
                    ElseIf e.CommandName = GRID_COMMAND_SHOW_FAIL Then
                        parameters.Status = STATUS_FAILED
                    ElseIf e.CommandName = GRID_COMMAND_SHOW_REPROCESS Then
                        parameters.Status = STATUS_REPROCESS
                    ElseIf e.CommandName = GRID_COMMAND_SHOW_PENDING_CREATION Then
                        parameters.Status = STATUS_PENDING_CLAIM_CREATION
                    Else
                    End If

                    parameters.FileProcessedId = State.FileProcessedId
                    parameters.StartDate = State.SearchedStartDate
                    parameters.EndDate = State.SearchedEndDate
                    parameters.Filename = filename

                    callPage(AppleGBIFileFormDetail.URL, parameters)
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub Grid_RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If dvRow IsNot Nothing And Not State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                        e.Row.Cells(GridDefenitionEnum.FileProcessedId).Text = GetGuidStringFromByteArray(CType(dvRow(AppleGBIFileReconWrk.COL_NAME_FILE_PROCESSED_ID), Byte()))
                        e.Row.Cells(GridDefenitionEnum.Filename).Text = dvRow(AppleGBIFileReconWrk.COL_NAME_FILE_NAME).ToString
                        e.Row.Cells(GridDefenitionEnum.FileDate).Text = GetLongDateFormattedString(CType(dvRow(AppleGBIFileReconWrk.COL_NAME_FILE_DATE), Date))
                        e.Row.Cells(GridDefenitionEnum.FileCreatedDate).Text = GetLongDateFormattedString(CType(dvRow(AppleGBIFileReconWrk.COL_NAME_FILE_CREATED_DATE), Date))
                        e.Row.Cells(GridDefenitionEnum.Received).Text = dvRow(AppleGBIFileReconWrk.COL_NAME_RECEIVED).ToString
                        e.Row.Cells(GridDefenitionEnum.Counted).Text = dvRow(AppleGBIFileReconWrk.COL_NAME_COUNTED).ToString
                        e.Row.Cells(GridDefenitionEnum.NewClaims).Text = dvRow(AppleGBIFileReconWrk.COL_NAME_NEW_CLAIMS).ToString
                        e.Row.Cells(GridDefenitionEnum.ClaimUpdate).Text = dvRow(AppleGBIFileReconWrk.COL_NAME_CLAIM_UPDATE).ToString
                        REM ---------------
                        'btnEditItem = CType(e.Row.Cells(GridDefenitionEnum.ProcessedStatus).FindControl("BtnShowProcessed"), LinkButton)
                        'btnEditItem.Text = dvRow(AppleGBIFileReconWrk.COL_NAME_PROCESSED).ToString
                        REM ---------------
                        CreateLinkButton(e, dvRow, "BtnShowProcessed", GridDefenitionEnum.ProcessedStatus, AppleGBIFileReconWrk.COL_NAME_PROCESSED)
                        CreateLinkButton(e, dvRow, "BtnShowCancelled", GridDefenitionEnum.CancelledStatus, AppleGBIFileReconWrk.COL_NAME_CANCELLED)
                        CreateLinkButton(e, dvRow, "BtnShowPendingValidation", GridDefenitionEnum.PendingValidationStatus, AppleGBIFileReconWrk.COL_NAME_PENDING_VALIDATION)
                        CreateLinkButton(e, dvRow, "BtnShowFail", GridDefenitionEnum.FailedStatus, AppleGBIFileReconWrk.COL_NAME_FAILED)
                        CreateLinkButton(e, dvRow, "BtnShowReprocess", GridDefenitionEnum.ReprocessStatus, AppleGBIFileReconWrk.COL_NAME_REPROCESS)
                        CreateLinkButton(e, dvRow, "BtnShowPendingCreation", GridDefenitionEnum.PendingClaimCreationStatus, AppleGBIFileReconWrk.COL_NAME_PENDING_CLAIM_CREATION)

                        'e.Row.Cells(GridDefenitionEnum.CancelledStatus).Text = dvRow(AppleGBIFileReconWrk.COL_NAME_CANCELLED).ToString
                        'e.Row.Cells(GridDefenitionEnum.PendingValidationStatus).Text = dvRow(AppleGBIFileReconWrk.COL_NAME_PENDING_VALIDATION).ToString
                        'e.Row.Cells(GridDefenitionEnum.FailedReprocessStatus).Text = dvRow(AppleGBIFileReconWrk.COL_NAME_FAILED_REPROCESS).ToString
                        'e.Row.Cells(GridDefenitionEnum.PendingClaimCreationStatus).Text = dvRow(AppleGBIFileReconWrk.COL_NAME_PENDING_CLAIM_CREATION).ToString
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub CreateLinkButton(e As System.Web.UI.WebControls.GridViewRowEventArgs,
                                     dvRow As DataRowView,
                                     controlName As String,
                                     cellIndex As GridDefenitionEnum,
                                     colName As String)
            Dim btnEditItem As LinkButton
            Try

                Dim count As Integer = 0

                If dvRow(colName) IsNot DBNull.Value Then
                    count = dvRow(colName)
                End If

                If count > 0 Then
                    btnEditItem = CType(e.Row.Cells(cellIndex).FindControl(controlName), LinkButton)
                    btnEditItem.Text = dvRow(colName).ToString
                Else
                    e.Row.Cells(cellIndex).Text = dvRow(colName).ToString
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")

                If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If SortDirection.EndsWith(" ASC") Then
                        SortDirection = e.SortExpression + " DESC"
                    Else
                        SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If
                State.SortExpression = SortDirection
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Controlling Logic"
        Public Sub PopulateSearchFieldsFromState()
            Try
                txtBeginDate.Text = DateHelper.GetEnglishDate(State.SearchedStartDate)
                txtEndDate.Text = DateHelper.GetEnglishDate(State.SearchedEndDate)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ClearSearch()
            Try
                txtBeginDate.Text = String.Empty
                txtEndDate.Text = String.Empty

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function UploadFile() As String
            Dim dealerFileName As String
            Dim layoutFileName As String
            Dim fileLen As Integer = dealerFileInput.PostedFile.ContentLength

            DealerFileProcessed.ValidateFileName(fileLen)
            dealerFileName = dealerFileInput.PostedFile.FileName
            Dim fileBytes(fileLen - 1) As Byte
            Dim objStream As System.IO.Stream
            objStream = dealerFileInput.PostedFile.InputStream
            objStream.Read(fileBytes, 0, fileLen)

            Dim webServerPath As String = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
            Dim webServerFile As String = webServerPath & "\" & System.IO.Path.GetFileName(dealerFileName)
            layoutFileName = webServerPath & "\" & System.IO.Path.GetFileNameWithoutExtension(webServerFile) & AppConfig.UnixServer.FtpTriggerExtension

            CreateFolder(webServerPath)

            If State.SelectedDealerFileLayout IsNot Nothing Then
                File.WriteAllBytes(webServerFile, fileBytes)
                File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes(State.SelectedDealerFileLayout))
            Else
                Throw New GUIException("Missing File Layout Code", Assurant.ElitaPlus.Common.ErrorCodes.GUI_MISSING_FILE_LAYOUT_CODE)
            End If

            Dim unixPath As String = AppConfig.UnixServer.FtpDirectory
            Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, AppConfig.UnixServer.Password)
            Try
                objUnixFTP.UploadFile(webServerFile)
                objUnixFTP.UploadFile(layoutFileName)

                Return dealerFileName
            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            Finally
            End Try

        End Function
        Public Shared Function GetUniqueDirectory(path As String, username As String) As String
            Dim uniqueIdDirectory As String = path & username & "_" & RemoveInvalidChar(Date.Now.ToString)
            Return uniqueIdDirectory
        End Function
        Public Shared Function RemoveInvalidChar(filename As String) As String

            Dim index As Integer
            For index = 0 To FILE_NAME_INVALID_CHARACTERS.Length - 1
                'replace the invalid character with blank
                filename = filename.Replace(FILE_NAME_INVALID_CHARACTERS(index), " ")
            Next
            Return filename
        End Function

        Public Shared Sub CreateFolder(folderName As String)
            Dim objDir As New IO.DirectoryInfo(folderName)
            If Not objDir.Exists() Then
                objDir.Create()
            End If
        End Sub

        Public Shared Sub DeleteFolder(folderName As String)
            Dim objDir As New IO.DirectoryInfo(folderName)
            If objDir.Exists Then
                objDir.Delete(True)
            End If
        End Sub

#End Region
#Region "Populate"

        Private Sub PopulateGrid(Optional ByVal refreshData As Boolean = False)
            Try

                If State.searchDV Is Nothing OrElse refreshData Then SearchGBIFiles(refreshData)

                If (State.searchDV.Count = 0) Then

                    State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, State.SortExpression)
                Else
                    State.bnoRow = False
                    Grid.Enabled = True
                End If

                State.searchDV.Sort = State.SortExpression
                Grid.AutoGenerateColumns = False

                Grid.AllowSorting = True
                State.searchDV.Sort = State.SortExpression

                Grid.Columns(GridDefenitionEnum.Filename).SortExpression = AppleGBIFileReconWrk.COL_NAME_FILE_NAME
                HighLightSortColumn(Grid, State.SortExpression)

                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)

                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, State.SortExpression)
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

                Session("recCount") = State.searchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SearchGBIFiles(Optional ByVal refreshData As Boolean = False)
            Dim dtStart As Date, dtEnd As Date
            Dim strTemp As String = String.Empty


            strTemp = txtBeginDate.Text.Trim()
            If Not String.IsNullOrEmpty(strTemp) Then
                If DateHelper.IsDate(strTemp) = False Then
                    Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
                Else
                    dtStart = DateHelper.GetDateValue(strTemp)
                End If
            Else
                'dtStart = Date.Today.AddMonths(-1)
                Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
            End If

            strTemp = txtEndDate.Text.Trim()
            If Not String.IsNullOrEmpty(strTemp) Then
                If DateHelper.IsDate(strTemp) = False Then
                    Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
                Else
                    dtEnd = DateHelper.GetDateValue(strTemp)
                End If
            Else
                'dtEnd = Date.Today
                Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
            End If

            Dim timeDifference As TimeSpan = dtEnd - dtStart

            If timeDifference.Days < 0 Then
                Throw New GUIException(Message.MSG_INVALID_BEGIN_END_DATES_ERR, Message.MSG_INVALID_BEGIN_END_DATES_ERR)
            ElseIf timeDifference.Days > 31 Then
                Throw New GUIException(Message.MSG_INVALID_30DAYS_RANGE_DATE, Message.MSG_INVALID_30DAYS_RANGE_DATE)
            End If

            State.SearchedStartDate = dtStart
            State.SearchedEndDate = dtEnd

            State.searchDV = AppleGBIFileReconWrk.SearchFilesname(dtStart, dtEnd)

        End Sub
#End Region
#Region "Button Event Handlers"
        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try
                ClearSearch()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try

                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    State.IsGridVisible = True
                End If
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                State.searchDV = Nothing
                SetSession()
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                State.searchDV = Nothing
                PopulateGrid()


            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub btnCopyDealerFile_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopyDealerFile_WRITE.Click
            Dim filename As String
            Try
                filename = UploadFile()
                ThePage.DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", ThePage.MSG_BTN_OK, ThePage.MSG_TYPE_INFO)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property
#End Region

#Region "State-Management"

        Private Sub SetSession()
            With State
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = State.SortExpression
                .searchDV = State.searchDV
            End With
        End Sub


#End Region
    End Class

End Namespace