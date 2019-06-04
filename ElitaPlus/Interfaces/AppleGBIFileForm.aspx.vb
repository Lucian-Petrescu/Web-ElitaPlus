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

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal fileProcessedId As Guid, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.FileProcessedId = fileProcessedId
                Me.BoChanged = boChanged
            End Sub

        End Class

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
            Try
                Me.IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                If Not retObj Is Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back

                            Me.State.IsGridVisible = True
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)

                        Case Else

                    End Select
                    Grid.PageIndex = Me.State.PageIndex
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    Grid.PageSize = Me.State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Interfaces")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PageTile)
                Me.UpdateBreadCrum()

                If Not Me.IsPostBack Then
                    InitializeCalendar()
                    PopulateSearchFieldsFromState()
                    TranslateGridHeader(Grid)

                    If Not Me.State.IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Else
                        ControlMgr.SetVisibleControl(Me, trPageSize, True)
                    End If

                    If Me.State.IsGridVisible Then
                        If Not (Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                            Grid.PageSize = Me.State.SelectedPageSize
                        End If
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)

                    If IsReturningFromChild Then
                        Me.PopulateGrid(True)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)

        End Sub

        Private Sub UpdateBreadCrum()

            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage(PageTile)
            End If

        End Sub

        Private Sub InitializeCalendar()
            Me.AddCalendar(Me.ImageBtnBeginDate, Me.txtBeginDate)
            Me.AddCalendar(Me.ImageBbtnEndDate, Me.txtEndDate)
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
                If Not ViewState("SortDirection") Is Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageIndex = Grid.PageSize
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub Grid_RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try

                If e.CommandName = GRID_COMMAND_SHOW_PROCESSED OrElse
                   e.CommandName = GRID_COMMAND_SHOW_CANCELLED OrElse
                   e.CommandName = GRID_COMMAND_SHOW_PENDING_VALIDATION OrElse
                   e.CommandName = GRID_COMMAND_SHOW_FAIL OrElse
                   e.CommandName = GRID_COMMAND_SHOW_REPROCESS OrElse
                   e.CommandName = GRID_COMMAND_SHOW_PENDING_CREATION Then

                    Dim index As Integer = CInt(e.CommandArgument)
                    Me.State.FileProcessedId = New Guid(Me.Grid.Rows(index).Cells(GridDefenitionEnum.FileProcessedId).Text)
                    Dim filename As String = Me.Grid.Rows(index).Cells(GridDefenitionEnum.Filename).Text

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

                    parameters.FileProcessedId = Me.State.FileProcessedId
                    parameters.StartDate = Me.State.SearchedStartDate
                    parameters.EndDate = Me.State.SearchedEndDate
                    parameters.Filename = filename

                    Me.callPage(AppleGBIFileFormDetail.URL, parameters)
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub Grid_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub CreateLinkButton(ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs,
                                     dvRow As DataRowView,
                                     controlName As String,
                                     cellIndex As GridDefenitionEnum,
                                     colName As String)
            Dim btnEditItem As LinkButton
            Try

                Dim count As Integer = 0

                If Not dvRow(colName) Is DBNull.Value Then
                    count = dvRow(colName)
                End If

                If count > 0 Then
                    btnEditItem = CType(e.Row.Cells(cellIndex).FindControl(controlName), LinkButton)
                    btnEditItem.Text = dvRow(colName).ToString
                Else
                    e.Row.Cells(cellIndex).Text = dvRow(colName).ToString
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")

                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If
                Me.State.SortExpression = Me.SortDirection
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Controlling Logic"
        Public Sub PopulateSearchFieldsFromState()
            Try
                Me.txtBeginDate.Text = DateHelper.GetEnglishDate(Me.State.SearchedStartDate)
                Me.txtEndDate.Text = DateHelper.GetEnglishDate(Me.State.SearchedEndDate)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ClearSearch()
            Try
                Me.txtBeginDate.Text = String.Empty
                Me.txtEndDate.Text = String.Empty

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

            If Not Me.State.SelectedDealerFileLayout Is Nothing Then
                File.WriteAllBytes(webServerFile, fileBytes)
                File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes(Me.State.SelectedDealerFileLayout))
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
        Public Shared Function GetUniqueDirectory(ByVal path As String, ByVal username As String) As String
            Dim uniqueIdDirectory As String = path & username & "_" & RemoveInvalidChar(Date.Now.ToString)
            Return uniqueIdDirectory
        End Function
        Public Shared Function RemoveInvalidChar(ByVal filename As String) As String

            Dim index As Integer
            For index = 0 To FILE_NAME_INVALID_CHARACTERS.Length - 1
                'replace the invalid character with blank
                filename = filename.Replace(FILE_NAME_INVALID_CHARACTERS(index), " ")
            Next
            Return filename
        End Function

        Public Shared Sub CreateFolder(ByVal folderName As String)
            Dim objDir As New IO.DirectoryInfo(folderName)
            If Not objDir.Exists() Then
                objDir.Create()
            End If
        End Sub

        Public Shared Sub DeleteFolder(ByVal folderName As String)
            Dim objDir As New IO.DirectoryInfo(folderName)
            If objDir.Exists Then
                objDir.Delete(True)
            End If
        End Sub

#End Region
#Region "Populate"

        Private Sub PopulateGrid(Optional ByVal refreshData As Boolean = False)
            Try

                If Me.State.searchDV Is Nothing OrElse refreshData Then SearchGBIFiles(refreshData)

                If (Me.State.searchDV.Count = 0) Then

                    Me.State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, Me.State.SortExpression)
                Else
                    Me.State.bnoRow = False
                    Me.Grid.Enabled = True
                End If

                Me.State.searchDV.Sort = Me.State.SortExpression
                Grid.AutoGenerateColumns = False

                Me.Grid.AllowSorting = True
                Me.State.searchDV.Sort = Me.State.SortExpression

                Grid.Columns(GridDefenitionEnum.Filename).SortExpression = AppleGBIFileReconWrk.COL_NAME_FILE_NAME
                HighLightSortColumn(Grid, Me.State.SortExpression)

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)

                Me.Grid.DataSource = Me.State.searchDV
                HighLightSortColumn(Grid, Me.State.SortExpression)
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

                Session("recCount") = Me.State.searchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SearchGBIFiles(Optional ByVal refreshData As Boolean = False)
            Dim dtStart As Date, dtEnd As Date
            Dim strTemp As String = String.Empty


            strTemp = Me.txtBeginDate.Text.Trim()
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

            strTemp = Me.txtEndDate.Text.Trim()
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
        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try
                Me.ClearSearch()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            Try

                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    Me.State.IsGridVisible = True
                End If
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                Me.State.searchDV = Nothing
                SetSession()
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                Me.State.searchDV = Nothing
                Me.PopulateGrid()


            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub btnCopyDealerFile_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyDealerFile_WRITE.Click
            Dim filename As String
            Try
                filename = UploadFile()
                ThePage.DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", ThePage.MSG_BTN_OK, ThePage.MSG_TYPE_INFO)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            With Me.State
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = Me.State.SortExpression
                .searchDV = Me.State.searchDV
            End With
        End Sub


#End Region
    End Class

End Namespace