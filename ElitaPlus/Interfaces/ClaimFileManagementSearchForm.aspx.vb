Imports System.Collections.Generic
Imports System.ServiceModel
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.ElitaPlusWebApp.FileManagerAdminService
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.ClaimFileManagementDetailForm

Public Class ClaimFileManagementSearchForm
    Inherits ElitaPlusSearchPage

#Region "Constants"

    Public PageTitle As String = "CLAIM_FILE_MANAGEMENT_SEARCH"
    Private Shared FILE_NAME_INVALID_CHARACTERS As String() = {"\", "/", ":", "*", "?", """", "<", ">", "|"} 'Operating system invalid characters
    Private Const ServiceEndPointName = "CustomBinding_FileManagerAdmin"

    Public Enum GridDefinitionEnum
        FileIdentifier = 0
        SelectRecord
        EditRecord
        Filename
        Received
        Counted
        Bypassed
        Rejected
        Validated
        Processed
        Status
    End Enum

    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_DELETED_FAILED As String = "MSG_RECORD_DELETED_FAILED"
    Private Const MSG_RECORDS_REPROCESSED_OK As String = "MSG_RECORDS_REPROCESSED_OK"
    Private Const MSG_RECORDS_REPROCESSED_FAILED As String = "MSG_RECORDS_REPROCESSED_FAILED"

    Public Const GRID_COL_FILE_IDENTIFIER_IDX As Integer = 0
    Public Const GRID_COL_SELECT_IDX As Integer = 1
    Public Const GRID_COL_FILENAME_IDX As Integer = 2
    Public Const GRID_COL_RECEIVED_IDX As Integer = 3
    Public Const GRID_COL_COUNTED_IDX As Integer = 4
    Public Const GRID_COL_REJECTED_IDX As Integer = 5
    Public Const GRID_COL_VALIDATED_IDX As Integer = 6
    Public Const GRID_COL_QUEUED_IDX As Integer = 7
    Public Const GRID_COL_LOADED_IDX As Integer = 8
    Public Const GRID_COL_STATUS_IDX As Integer = 9

    Public Const GRID_COL_FILENAME As String = "LogicalFileName"
    Public Const GRID_COL_RECEIVED As String = "ReceivedRecords"
    Public Const GRID_COL_COUNTED As String = "CountedRecords"
    Public Const GRID_COL_REJECTED As String = "RejectedRecords"
    Public Const GRID_COL_VALIDATED As String = "ValidatedRecords"
    Public Const GRID_COL_LOADED As String = "ProcessedRecords"
    Public Const GRID_COL_STATUS As String = "RecordState"

    Public Const GRID_LINK_BTN_SELECT As String = "SelectRecord"
    Public Const GRID_LINK_BTN_REJECTED As String = "BtnShowRejected"
    Public Const GRID_LINK_BTN_VALIDATED As String = "BtnShowValidated"
    Public Const GRID_LINK_BTN_PROCESSED As String = "BtnShowProcessed"
    Public Const GRID_LINK_BTN_STATUS As String = "BtnShowStatus"

    Public Const SHOW_COMMAND_REJECTED As String = "ShowRecordRejected"
    Public Const SHOW_COMMAND_VALIDATED As String = "ShowRecordValidated"
    Public Const SHOW_COMMAND_PROCESSED As String = "ShowRecordProcessed"

    REM -------------------
    Public Const GRID_COMMAND_SHOW_PROCESSED As String = "ShowProcessed"
    Public Const GRID_COMMAND_SHOW_FILES As String = "ShowFiles"
    REM -------------------
    Public Const STATUS_PROCESSED = "P"
    Public Const STATUS_REPROCESS = "R"

    Private Const SESSION_SELECTED_FILE As String = "ClaimUpSelectedFile"
#End Region

#Region "Page Navigation"

    Private IsReturningFromChild As Boolean = False

    Public Class ReturnType
        Public LastOperation As ElitaPlusPage.DetailPageCommand

        Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand)
            LastOperation = LastOp
        End Sub

    End Class

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
        Try
            IsReturningFromChild = True
            Dim retObj As ReturnType = CType(ReturnPar, ReturnType)

            If retObj IsNot Nothing Then
                State.SearchDV = Nothing

                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        State.IsGridVisible = True

                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)

                    Case Else
                        ' ????

                End Select

                DataGridView.PageIndex = State.PagingInfo.PageIndex
                cboPageSize.SelectedValue = CType(State.PagingInfo.PageSize, String)
                DataGridView.PageSize = State.PagingInfo.PageSize

                ControlMgr.SetVisibleControl(Me, trPageSize, DataGridView.Visible)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub
#End Region

#Region "Page State"
    Class MyState
        Public PagingInfo As PagingFilter
        Public PageSort As String
        REM ---------------
        Public IsGridVisible As Boolean
        Public IsReturningFromChild As Boolean
        Public SearchDV As IEnumerable(Of FileInfoDto) = Nothing
        Public SelectedIndex As Integer = -1
        REM ---------------

        Public Sub New()
            Console.WriteLine("Building the state")
        End Sub

        Public Property SelectedFile As FileInfoSummary
            Get
                Dim FileInfo As FileInfoSummary = Nothing

                If (HttpContext.Current.Session(SESSION_SELECTED_FILE) IsNot Nothing) Then
                    FileInfo = CType(HttpContext.Current.Session(SESSION_SELECTED_FILE), FileInfoSummary)
                End If

                Return FileInfo
            End Get
            Set(value As FileInfoSummary)
                HttpContext.Current.Session(SESSION_SELECTED_FILE) = value
            End Set
        End Property

    End Class

    Class FileInfoSummary

        Private SelectedGridRow As GridViewRow = Nothing

        Public Sub New(ByVal SelectedRow As GridViewRow)
            SelectedGridRow = SelectedRow
        End Sub

        Public ReadOnly Property FileIndentifier As String
            Get
                Return SelectedGridRow.Cells(GRID_COL_FILE_IDENTIFIER_IDX).Text
            End Get
        End Property

        Public ReadOnly Property FileName As String
            Get
                Return SelectedGridRow.Cells(GRID_COL_FILENAME_IDX).Text
            End Get
        End Property

        Public ReadOnly Property RecordsReceived As Integer
            Get
                Return CInt(SelectedGridRow.Cells(GRID_COL_RECEIVED_IDX).Text)
            End Get
        End Property

        Public ReadOnly Property RecordsCounted As Integer
            Get
                Return CInt(SelectedGridRow.Cells(GRID_COL_COUNTED_IDX).Text)
            End Get
        End Property

        Public ReadOnly Property RecordsRejected As Integer
            Get
                Dim oLinkButton As LinkButton = CType(SelectedGridRow.FindControl(GRID_LINK_BTN_REJECTED), LinkButton)

                Return CInt(oLinkButton.Text)
            End Get
        End Property

        Public ReadOnly Property RecordsValidated As Integer
            Get
                Dim oLinkButton As LinkButton = CType(SelectedGridRow.FindControl(GRID_LINK_BTN_VALIDATED), LinkButton)

                Return CInt(oLinkButton.Text)
            End Get
        End Property

        Public ReadOnly Property RecordsProcessed As Integer
            Get
                Return CInt(SelectedGridRow.Cells(GRID_COL_LOADED_IDX).Text)
            End Get
        End Property

        ' operations that can be performed on the file are based on the status of the file record summary information.

        Public ReadOnly Property FileValidationIsAvailable As Boolean
            Get
                Return RecordsReceived > 0 And RecordsCounted > 0 And RecordsRejected > 0
            End Get
        End Property

        Public ReadOnly Property FileReprocessingIsAvailable As Boolean
            Get
                Return RecordsReceived > 0 And RecordsCounted > 0 And RecordsRejected > 0
            End Get
        End Property

        Public ReadOnly Property FileDeletionIsAvailable As Boolean
            Get
                Return RecordsProcessed = 0
            End Get
        End Property

        Public Function ClaimFileParams(ByVal CommandName As String) As ClaimFileDetailPageParams
            Dim FileInfoParams As ClaimFileInfoParams = Nothing

            Select Case CommandName

                Case SHOW_COMMAND_REJECTED
                    FileInfoParams = New ClaimFileInfoParams(RecordStateType.Rejected, RecordsRejected)

                Case SHOW_COMMAND_VALIDATED
                    FileInfoParams = New ClaimFileInfoParams(RecordStateType.Validated, RecordsValidated)

                Case SHOW_COMMAND_PROCESSED
                    FileInfoParams = New ClaimFileInfoParams(RecordStateType.Processed, RecordsProcessed)

                Case Else
                    ' default to the counted records.
                    FileInfoParams = New ClaimFileInfoParams(RecordStateType.Initialized, RecordsCounted)

            End Select

            Return New ClaimFileDetailPageParams(FileIndentifier, FileInfoParams)

        End Function

    End Class

#End Region

#Region "Page event handlers"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            MasterPage.MessageController.Clear()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Interfaces")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PageTitle)

            UpdateBreadCrum()

            Dim GridIsLoaded As Boolean = False

            If Not IsPostBack Then
                TranslateGridHeader(DataGridView)

                If State.IsGridVisible Then
                    If Not (State.PagingInfo.PageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PagingInfo.PageSize, String)
                        DataGridView.PageSize = State.PagingInfo.PageSize
                    End If
                End If

                SetGridItemStyleColor(DataGridView)
                State.PagingInfo = New PagingFilter With {.PageIndex = DEFAULT_PAGE_INDEX, .PageSize = DEFAULT_PAGE_SIZE}

                If IsReturningFromChild Then
                    PopulateGrid(True)
                    GridIsLoaded = True
                End If
            End If

            If Not GridIsLoaded Then
                PopulateGrid(True) ' force a population
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try

        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub UpdateBreadCrum()

        If (Not State Is Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage(PageTitle)
        End If

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

#Region "Populate"
    Private Sub PopulateGrid(Optional ByVal RefreshData As Boolean = False)
        Try
            If State.SearchDV Is Nothing OrElse RefreshData Then
                LoadClaimFileInfo(RefreshData)

                ControlMgr.SetVisibleControl(Me, trPageSize, State.SearchDV.Any())
            End If

            If (DataGridView.BottomPagerRow IsNot Nothing AndAlso Not DataGridView.BottomPagerRow.Visible) Then
                DataGridView.BottomPagerRow.Visible = True
            End If

            If (RefreshData) Then
                State.SelectedIndex = -1
            Else
                EnableDisableButtons()
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub LoadClaimFileInfo(Optional ByVal refreshData As Boolean = False)
        Try
            Dim SearchInfo As SearchCriteria = New SearchCriteria With
            {
                .RecordFilter = Nothing,
                .PagingFilter = State.PagingInfo
            }

            Dim wsResponse As FileInfoDto() = WcfClientHelper.Execute(Of FileManagerAdminClient, FileManagerAdmin, FileInfoDto())(
                GetClient(),
                New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                Function(ByVal c As FileManagerAdminClient)
                    Return c.SearchFileInfoRecords(SearchInfo)
                End Function)

            If (Response IsNot Nothing) Then
                State.SearchDV = wsResponse

                DataGridView.DataSource = State.SearchDV

                DataGridView.PageSize = State.PagingInfo.PageSize
                DataGridView.PageIndex = State.PagingInfo.PageIndex

                DataGridView.VirtualItemCount =
                    WcfClientHelper.Execute(Of FileManagerAdminClient, FileManagerAdmin, FileInfoDto())(
                    GetClient(),
                    New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                    Function(ByVal c As FileManagerAdminClient)
                        Return c.SearchFileInfoRecords(Nothing)
                    End Function).Count

                DataGridView.DataBind()
            End If

        Catch ex As FaultException
            ThrowWebServiceFaultException(ex)

        End Try
    End Sub

    Private Function DeleteClaimFileInfo(ByVal FileIdentifier As String) As Boolean

        Try
            Dim SelectedFile As FileInfoSummary = State.SelectedFile

            If (SelectedFile IsNot Nothing) Then
                Dim FileInfoLocator As DataItemLocator = New DataItemLocator With
                {
                    .Identifier = State.SelectedFile.FileIndentifier,
                    .Type = DataItemType.FileInfo,
                    .ForceRefresh = True
                }

                Dim wsResponse As FileInfoDto = WcfClientHelper.Execute(Of FileManagerAdminClient, FileManagerAdmin, FileInfoDto)(
                        GetClient(),
                        New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                        Function(ByVal c As FileManagerAdminClient)
                            Return c.RemoveFileInfoRecord(FileInfoLocator)
                        End Function)

                Return (wsResponse IsNot Nothing)
            End If

        Catch ex As FaultException
            ThrowWebServiceFaultException(ex)

        End Try

        Return False

    End Function

    Private Function ReprocessFileInfoRecords(ByVal FileIdentifier As String) As Boolean
        Try
            Dim SelectedFile As FileInfoSummary = State.SelectedFile

            If (SelectedFile IsNot Nothing) Then
                Dim FileInfoLocator As DataItemLocator = New DataItemLocator With
                {
                    .Identifier = SelectedFile.FileIndentifier,
                    .Type = DataItemType.FileInfo,
                    .ForceRefresh = True
                }

                Return WcfClientHelper.Execute(Of FileManagerAdminClient, FileManagerAdmin, Boolean)(
                    GetClient(),
                    New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                    Function(ByVal c As FileManagerAdminClient)
                        Return c.ReprocessFileInfoRecords(FileInfoLocator)
                    End Function)
            End If

        Catch ex As FaultException
            ThrowWebServiceFaultException(ex)

        End Try

        Return False

    End Function

    '' <summary>
    '' Gets New Instance of File Admin Service Client with Credentials Configured
    '' </summary>
    '' <returns>Instance of <see cref="FileAdminClient"/></returns>
    Private Shared Function GetClient() As FileManagerAdminClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__FILE_MANAGEMENT_ADMIN_SERVICE), False)

        Dim client = New FileManagerAdminClient(ServiceEndPointName, oWebPasswd.Url)

        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password

        Return client
    End Function


    Private Sub ThrowWebServiceFaultException(fex As FaultException)

        Log(fex)
        MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_RECORDING_SERVICE_ERR) & " - " & fex.Message, False)

    End Sub
#End Region

#Region "Button Event Handlers"
    Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property


    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Try
            PopulateGrid(True)

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)

        Finally
            State.SelectedIndex = -1
            ShowPageDisplay()

        End Try
    End Sub

    Private Sub BtnDeleteFile_Click(sender As Object, e As EventArgs) Handles BtnDeleteFile.Click
        Try
            Dim SelectedFile As FileInfoSummary = State.SelectedFile

            If (SelectedFile IsNot Nothing) Then
                Dim Message As String

                If (DeleteClaimFileInfo(SelectedFile.FileIndentifier)) Then
                    Message = TranslationBase.TranslateLabelOrMessage(MSG_RECORD_DELETED_OK)

                    PopulateGrid(True)

                Else
                    Message = TranslationBase.TranslateLabelOrMessage(MSG_RECORD_DELETED_FAILED)

                End If

                MasterPage.MessageController.AddSuccess(Message)
            End If

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)

        Finally
            State.SelectedIndex = -1
            ShowPageDisplay()

        End Try
    End Sub

    Private Sub BtnReprocessFile_Click(sender As Object, e As EventArgs) Handles BtnReprocessFile.Click
        Try
            Dim SelectedFile As FileInfoSummary = State.SelectedFile

            If (SelectedFile IsNot Nothing) Then
                If (ReprocessFileInfoRecords(SelectedFile.FileIndentifier)) Then
                    MasterPage.MessageController.AddSuccess(MSG_RECORDS_REPROCESSED_OK)

                    PopulateGrid(True)
                Else
                    MasterPage.MessageController.AddMessage(MSG_RECORDS_REPROCESSED_FAILED)

                End If
            End If

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)

        Finally
            State.SelectedIndex = -1
            ShowPageDisplay()

        End Try
    End Sub

#End Region

    Private Sub ShowPageDisplay()

        DataGridView.SelectedIndex = State.SelectedIndex

        If (DataGridView.SelectedIndex = -1) Then
            State.SelectedFile = Nothing
        End If

        EnableDisableButtons()

    End Sub

    Private Sub EnableDisableButtons()

        ControlMgr.SetEnableControl(ThePage, BtnRefresh, True)
        DataGridView.SelectedIndex = State.SelectedIndex

        Dim SelectedFile As FileInfoSummary = State.SelectedFile

        If (SelectedFile Is Nothing) Then
            ControlMgr.SetEnableControl(ThePage, BtnReprocessFile, False)
            ControlMgr.SetEnableControl(ThePage, BtnDeleteFile, False)

        Else
            ControlMgr.SetEnableControl(ThePage, BtnReprocessFile, SelectedFile.FileReprocessingIsAvailable)
            ControlMgr.SetEnableControl(ThePage, BtnDeleteFile, SelectedFile.FileDeletionIsAvailable)

        End If

    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Dim PageSize As Integer = CType(cboPageSize.SelectedValue, Int32)

            State.PagingInfo = New PagingFilter With {.PageIndex = DEFAULT_PAGE_INDEX, .PageSize = PageSize}
            PopulateGrid(True)

        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DataGridView.PageIndexChanging
        Try
            State.PagingInfo = New PagingFilter With {.PageIndex = e.NewPageIndex, .PageSize = State.PagingInfo.PageSize}
            PopulateGrid(True)

        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub


    Protected Sub DataGridView_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles DataGridView.RowCommand
        Try
            If (Not e.CommandName.Equals("Page")) Then
                State.PagingInfo.PageIndex = DataGridView.PageIndex
                State.SelectedIndex = CInt(e.CommandArgument)

                Dim SelectedFile As FileInfoSummary = New FileInfoSummary(DataGridView.Rows(State.SelectedIndex))

                If (e.CommandName = GRID_LINK_BTN_SELECT) Then
                    State.SelectedFile = SelectedFile
                    ShowPageDisplay()

                Else
                    If (SelectedFile IsNot Nothing) Then
                        ThePage.callPage(ClaimFileManagementDetailForm.PageUrl, SelectedFile.ClaimFileParams(e.CommandName))
                    End If

                End If
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)

        End Try
    End Sub

    Protected Sub DataGridView_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DataGridView.RowDataBound
        Try
            If (e.Row.DataItem IsNot Nothing) Then
                Dim fileInfo As FileInfoDto = CType(e.Row.DataItem, FileInfoDto)
                Dim oLinkButton As LinkButton
                Dim oStatusLabel As Label

                If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                    With e.Row

                        ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_FILE_IDENTIFIER_IDX), fileInfo.FileIdentifier)
                        ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_FILENAME_IDX), fileInfo.LogicalFileName)
                        ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_RECEIVED_IDX), fileInfo.ReceivedRecords)
                        ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_COUNTED_IDX), fileInfo.CountedRecords)

                        oLinkButton = CType(.FindControl(GRID_LINK_BTN_REJECTED), LinkButton)
                        ThePage.PopulateControlFromBOProperty(oLinkButton, fileInfo.RejectedRecords)
                        oLinkButton.Enabled = fileInfo.RejectedRecords > 0

                        oLinkButton = CType(.FindControl(GRID_LINK_BTN_VALIDATED), LinkButton)
                        ThePage.PopulateControlFromBOProperty(oLinkButton, fileInfo.ValidatedRecords)
                        oLinkButton.Enabled = fileInfo.ValidatedRecords > 0

                        ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_QUEUED_IDX), fileInfo.QueuedRecords)
                        ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_LOADED_IDX), fileInfo.ProcessedRecords) ' processed records are removed from service fabric; available in azure storage only.

                        oStatusLabel = CType(.FindControl(GRID_LINK_BTN_STATUS), Label)
                        ThePage.PopulateControlFromBOProperty(oStatusLabel, System.Enum.GetName(GetType(FileStateType), fileInfo.FileState))

                    End With
                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Protected Sub DataGridView_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles DataGridView.RowCreated
        BaseItemCreated(sender, e)
    End Sub

End Class