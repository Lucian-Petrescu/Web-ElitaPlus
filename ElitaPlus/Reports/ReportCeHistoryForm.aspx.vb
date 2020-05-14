'Imports CrystalDecisions.Enterprise
'Imports Assurant.Common.CrystalEnterpriseHelper
'Imports CrystalDecisions.Enterprise.Desktop
'Imports Assurant.Common.Ftp
'Imports Assurant.ElitaPlus.ElitaPlusWebApp.DownLoad

Namespace Reports

    Partial Class ReportCeHistoryForm
        Inherits ElitaPlusSearchPage



'#Region "Constants"

'        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Reports/ReportCeHistoryForm.aspx"
'        Private Const RPT_FILENAME As String = "ExpiredCertificatesEnglishUSA"
'        ' Private Const RPT_FILENAME As String = "ExpiredCertificatesEnglishUSA_Exp"
'        Private Const RPT_FILENAME_WINDOW As String = "EXPIRED CERTIFICATES"

'        Private Const GRID_COL_REPORT_ID As Integer = 1
'        Private Const GRID_COL_INSTANCE_ID As Integer = 2
'        Private Const GRID_COL_STARTTIME As Integer = 3
'        Private Const GRID_COL_ENDTIME As Integer = 4
'        Private Const GRID_COL_FORMAT As Integer = 5
'        Private Const GRID_COL_FILENAME As Integer = 6
'        Private Const GRID_COL_STATUS As Integer = 7
'        Private Const GRID_COL_DESTINATION As Integer = 8

'#End Region

'        '#Region "Properties"
'        '        Public ReadOnly Property TheRptCeInputControl() As ReportCeInputControl
'        '            Get
'        '                If moReportCeInputControl Is Nothing Then
'        '                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
'        '                End If
'        '                Return moReportCeInputControl
'        '            End Get
'        '        End Property
'        '#End Region

'#Region "Variables"




'#End Region


'        '#Region "Page State"

'#Region "MyState"


'        Class MyState

'            Public moInstanceId As Guid = Guid.Empty
'            'Public moDealerId As Guid = Guid.Empty
'            Public mnPageIndex As Integer
'            Public searchDV As DataView = Nothing
'            Public oRepInstances As DataSet
'            Public PageSize As Integer = DEFAULT_PAGE_SIZE
'            Public IsGridVisible As Boolean
'            Public SortExpression As String = "INSTANCE_KEY"
'            Public bnoRow As Boolean = False
'            Public isSearch As Boolean = False
'            Sub New()

'            End Sub

'        End Class
'#End Region

'        '        Public Sub New()
'        '            MyBase.New(New MyState)
'        '        End Sub


'        '        Protected Shadows ReadOnly Property State() As MyState
'        '            Get
'        '                Return CType(MyBase.State, MyState)
'        '            End Get
'        '        End Property

'        '        Protected Sub SetStateProperties()

'        '        End Sub
'        '        Public Property SortDirection() As String
'        '            Get
'        '                Return ViewState("SortDirection").ToString
'        '            End Get
'        '            Set(ByVal value As String)
'        '                ViewState("SortDirection") = value
'        '            End Set
'        '        End Property


'        '#End Region

'        '#Region "Handlers"


'        '#Region " Web Form Designer Generated Code "

'        '        'This call is required by the Web Form Designer.
'        '        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

'        '        End Sub
'        '        Protected WithEvents moTitleLabel As System.Web.UI.WebControls.Label
'        '        Protected WithEvents ErrorCtrl As ErrorController
'        '        Protected WithEvents moReportCeInputControl As ReportCeInputControl
'        '        'NOTE: The following placeholder declaration is required by the Web Form Designer.
'        '        'Do not delete or move it.
'        '        Private designerPlaceholderDeclaration As System.Object

'        '        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
'        '            'CODEGEN: This method call is required by the Web Form Designer
'        '            'Do not modify it using the code editor.
'        '            InitializeComponent()
'        '        End Sub

'        '#End Region

'        '#Region "Handlers-Init"


'        '        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'        '            'Put user code to initialize the page here
'        '            ErrorCtrl.Clear_Hide()
'        '            Try
'        '                If Not Page.IsPostBack Then
'        '                    Me.SortDirection = Me.State.SortExpression
'        '                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
'        '                    SetStateProperties()
'        '                    Me.TranslateGridHeader(moDataGrid)
'        '                    Me.TranslateGridControls(moDataGrid)
'        '                    TheRptCeInputControl.IsFormatVisible = False
'        '                    PopulateAll()
'        '                    ' PopulateGrid()
'        '                End If
'        '                Me.InstallProgressBar()
'        '            Catch ex As Exception
'        '                Me.HandleErrors(ex, Me.ErrorCtrl)
'        '            End Try
'        '        End Sub

'        '#End Region

'        '#Region "Handlers-DropDowns"

'        '        Private Sub moReportDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moReportDrop.SelectedIndexChanged
'        '            If moReportDrop.SelectedIndex <> BLANK_ITEM_SELECTED Then
'        '                '  moReportText.Text = Me.GetSelectedDescription(moReportDrop)
'        '                Me.State.isSearch = False
'        '                PopulateGrid(False)
'        '            Else
'        '                Clear()
'        '            End If

'        '        End Sub

'        '#End Region

'        '#Region "Handlers-Grid"

'        '        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
'        '            Try
'        '                Me.moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
'        '                Me.State.PageSize = moDataGrid.PageSize
'        '                Me.PopulateGrid(Me.State.isSearch)
'        '            Catch ex As Exception
'        '                Me.HandleErrors(ex, ErrorCtrl)
'        '            End Try
'        '        End Sub

'        '        Private Sub moDataGrid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moDataGrid.PageIndexChanged
'        '            Me.State.mnPageIndex = moDataGrid.PageIndex
'        '            PopulateGrid(Me.State.isSearch, POPULATE_ACTION_NO_EDIT)
'        '        End Sub

'        '        Private Sub moDataGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
'        '            Try
'        '                moDataGrid.PageIndex = e.NewPageIndex
'        '                Me.State.mnPageIndex = moDataGrid.PageIndex
'        '                PopulateGrid(Me.State.isSearch)
'        '            Catch ex As Exception
'        '                Me.HandleErrors(ex, Me.ErrControllerMaster)
'        '            End Try
'        '        End Sub

'        '        Private Sub moDataGrid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moDataGrid.RowCreated
'        '            Me.BaseItemCreated(sender, e)
'        '        End Sub

'        '        Private Sub moDataGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles moDataGrid.RowCommand


'        '            Dim ceFormat As String
'        '            Dim nInstanceId As Long
'        '            Dim cestartdate As String
'        '            Dim ceStatus As CeScheduleStatus
'        '            Dim oCEHelper As CEHelper
'        '            Dim sStatus As String
'        '            Dim cedestination As String
'        '            Dim cefilename As String
'        '            Dim sbMsg As New System.Text.StringBuilder
'        '            Dim count As Integer
'        '            Dim userId As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
'        '            Dim requestDT As DataTable
'        '            Try
'        '                If moReportSearchText.Text Is String.Empty Then
'        '                    requestDT = ReportRequests.LoadRequestsByUser(userId, moReportDrop.SelectedValue.Replace("DBRPT_", ""))
'        '                    count = requestDT.Rows.Count
'        '                Else
'        '                    count = moDataGrid.Rows.Count
'        '                End If

'        '                If e.CommandName = "SelectUser" Then

'        '                    Dim lblCtrl As Label
'        '                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
'        '                    Dim RowInd As Integer = row.RowIndex

'        '                    ceFormat = Me.moDataGrid.Rows(RowInd).Cells(GRID_COL_FORMAT).Text
'        '                    nInstanceId = Convert.ToInt64(moDataGrid.Rows(RowInd).Cells(GRID_COL_INSTANCE_ID).Text)
'        '                    cestartdate = moDataGrid.Rows(RowInd).Cells(GRID_COL_STARTTIME).Text
'        '                    sStatus = "ceStatus" & moDataGrid.Rows(RowInd).Cells(GRID_COL_STATUS).Text
'        '                    ceStatus = CType(CeScheduleStatus.Parse(GetType(CeScheduleStatus),
'        '                                    sStatus), CeScheduleStatus)
'        '                    cedestination = moDataGrid.Rows(RowInd).Cells(GRID_COL_DESTINATION).Text.ToString
'        '                    cefilename = moDataGrid.Rows(RowInd).Cells(GRID_COL_FILENAME).Text.ToString


'        '                    Select Case ceStatus
'        '                        Case CeScheduleStatus.ceStatusSuccess
'        '                            If nInstanceId >= 1 And count > 0 Then
'        '                                'Response.Redirect("DownloadReportData.aspx?rid=" + e.CommandArgument.ToString())
'        '                                Dim bo As New ReportRequests(New Guid(e.CommandArgument.ToString()))
'        '                                Dim fileName As String = bo.FtpFilename
'        '                                TransferFilesUnixWebServer(fileName)
'        '                            ElseIf nInstanceId >= 1 Then
'        '                                ViewReport(nInstanceId, ceFormat)
'        '                            End If



'        '                        Case CeScheduleStatus.ceStatusFailure
'        '                            If count = 0 Then
'        '                                oCEHelper = ReportCeBase.GetCEHelper()
'        '                                ErrorCtrl.AddError(oCEHelper.GetErrorMsg(nInstanceId), False)
'        '                                ErrorCtrl.Show()
'        '                            Else
'        '                                ErrorCtrl.AddError(Assurant.ElitaPlus.Common.ErrorCodes.NO_DATA, True)
'        '                                ErrorCtrl.Show()
'        '                            End If
'        '                        Case CeScheduleStatus.ceStatusPending
'        '                            If cedestination = CEHelper.CRYSTAL_FTP Then

'        '                                sbMsg.Append(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.REPORT_SCHEDULED_MESSAGE1))
'        '                                sbMsg.Append(" " + GetLongDateFormattedString(CType(cestartdate, Date)) + " ")
'        '                                sbMsg.Append(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.REPORT_SCHEDULED_MESSAGE2))
'        '                                sbMsg.Append(": " + cefilename)
'        '                                ErrorCtrl.AddError(sbMsg.ToString, False)
'        '                                ErrorCtrl.Show()
'        '                            Else

'        '                                sbMsg.Append(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.REPORT_SCHEDULED_MESSAGE1))
'        '                                sbMsg.Append(" " + GetLongDateFormattedString(CType(cestartdate, Date)))
'        '                                ErrorCtrl.AddError(sbMsg.ToString, False)
'        '                                ErrorCtrl.Show()
'        '                            End If

'        '                        Case Else
'        '                            ErrorCtrl.AddError(CeScheduleStatus.GetName(GetType(CeScheduleStatus), ceStatus), False)
'        '                            ErrorCtrl.Show()
'        '                    End Select
'        '                Else
'        '                    If e.CommandName = SORT_COMMAND_NAME Then
'        '                        moDataGrid.DataMember = e.CommandArgument.ToString
'        '                        PopulateGrid(Me.State.isSearch)
'        '                    End If
'        '                End If

'        '            Catch ex As Threading.ThreadAbortException
'        '            Catch ex As Exception
'        '                Me.HandleErrors(ex, Me.ErrorCtrl)
'        '            End Try
'        '        End Sub

'        '        Private Sub moDataGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moDataGrid.RowDataBound
'        '            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
'        '            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

'        '            Try
'        '                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
'        '                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

'        '                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_REPORT_ID), dvRow(0))
'        '                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_INSTANCE_ID), dvRow(1))
'        '                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_STARTTIME), dvRow(2))
'        '                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_ENDTIME), dvRow(3))
'        '                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_FORMAT), dvRow(5))
'        '                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_FILENAME), dvRow(7))
'        '                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_STATUS), dvRow(4))
'        '                        Me.PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_DESTINATION), dvRow(6))

'        '                    End If
'        '                End If
'        '            Catch ex As Exception
'        '                Me.HandleErrors(ex, Me.ErrControllerMaster)
'        '            End Try
'        '        End Sub

'        '        Private Sub moDataGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting

'        '            Try
'        '                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")

'        '                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
'        '                    If Me.SortDirection.EndsWith(" ASC") Then
'        '                        Me.SortDirection = e.SortExpression + " DESC"
'        '                    Else
'        '                        Me.SortDirection = e.SortExpression + " ASC"
'        '                    End If
'        '                Else
'        '                    Me.SortDirection = e.SortExpression + " ASC"
'        '                End If
'        '                Me.State.SortExpression = Me.SortDirection
'        '                Me.PopulateGrid(Me.State.isSearch)
'        '            Catch ex As Exception
'        '                Me.HandleErrors(ex, Me.ErrorCtrl)
'        '            End Try
'        '        End Sub

'        '        Private Sub SortAndBindGrid()

'        '            Me.moDataGrid.DataSource = Me.State.searchDV
'        '            HighLightSortColumn(moDataGrid, Me.State.SortExpression)
'        '            Me.moDataGrid.DataBind()

'        '            ControlMgr.SetVisibleControl(Me, moDataGrid, Me.State.IsGridVisible)
'        '            Session("recCount") = Me.State.searchDV.Count

'        '            If Me.State.searchDV.Count > 0 Then

'        '                If Me.moDataGrid.Visible Then
'        '                    Me.State.bnoRow = False
'        '                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
'        '                End If
'        '            Else
'        '                If Me.moDataGrid.Visible Then
'        '                    Me.State.bnoRow = True
'        '                    CreateHeaderForEmptyGrid(moDataGrid, Me.SortDirection)
'        '                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
'        '                End If
'        '            End If
'        '        End Sub

'        '#End Region

'        '#End Region

'        '#Region "Populate"

'        '        Private Function GetDataView() As DataView
'        '            Dim oReportName As String = GetSelectedDescription(moReportDrop)
'        '            Dim oCEHelper As CEHelper = ReportCeBase.GetCEHelper()
'        '            Dim oRepInstances As DataSet
'        '            Dim oDataView As DataView
'        '            If moReportDrop.SelectedValue.IndexOf("DBRPT_") <> -1 Then

'        '                moReportSearchText.Enabled = True
'        '                btnReportSearch.Enabled = True

'        '                Dim dsReportInstances As New DataSet
'        '                Dim dtReportInstances As New DataTable("INSTANCES")
'        '                Dim drRow As DataRow

'        '                dtReportInstances.Columns.Add("ID", GetType(Byte()))
'        '                dtReportInstances.Columns.Add("INSTANCE_KEY", GetType(Long))
'        '                dtReportInstances.Columns.Add("START_INSTANCE_TIMESTAMP", GetType(String))
'        '                dtReportInstances.Columns.Add("END_INSTANCE_TIMESTAMP", GetType(String))
'        '                dtReportInstances.Columns.Add("STATUS", GetType(String))
'        '                dtReportInstances.Columns.Add("FORMAT", GetType(String))
'        '                dtReportInstances.Columns.Add("DESTINATION", GetType(String))
'        '                dtReportInstances.Columns.Add("FILENAME", GetType(String))
'        '                dtReportInstances.Columns.Add("OWNER", GetType(String))

'        '                Dim userId As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
'        '                Dim requestDT As DataTable = ReportRequests.LoadRequestsByUser(userId, moReportDrop.SelectedValue.Replace("DBRPT_", ""))
'        '                Dim instanceKey As Integer = 1

'        '                For Each dr As DataRow In requestDT.Rows
'        '                    drRow = dtReportInstances.NewRow()
'        '                    drRow("ID") = dr("REPORT_REQUEST_ID")
'        '                    drRow("INSTANCE_KEY") = instanceKey.ToString()
'        '                    drRow("START_INSTANCE_TIMESTAMP") = dr("START_DATE")
'        '                    drRow("END_INSTANCE_TIMESTAMP") = dr("END_DATE")
'        '                    drRow("STATUS") = dr("STATUS")
'        '                    drRow("FORMAT") = "Text"
'        '                    drRow("DESTINATION") = "FTP"
'        '                    drRow("FILENAME") = dr("FTP_FILENAME")
'        '                    drRow("OWNER") = ElitaPlusIdentity.Current.ActiveUser.UserName
'        '                    dtReportInstances.Rows.Add(drRow)
'        '                    instanceKey = instanceKey + 1
'        '                Next
'        '                oDataView = dtReportInstances.DefaultView
'        '            Else
'        '                Dim oRep As Report = oCEHelper.RetrieveReport(AppConfig.CE.RootDir & "\" & oReportName)
'        '                oRepInstances = oCEHelper.RetrieveReportInstances(oRep.ID)
'        '                oDataView = oRepInstances.Tables(0).DefaultView
'        '                moReportSearchText.Enabled = False
'        '                moReportSearchText.Text = Nothing
'        '                btnReportSearch.Enabled = False
'        '            End If

'        '            oDataView.RowFilter = CEHelper.JobDataview.COL_OWNER & " = '" & ElitaPlusIdentity.Current.ActiveUser.UserName & "'"
'        '            oDataView.Sort = moDataGrid.DataMember()

'        '            Return oDataView
'        '        End Function

'        '        Private Sub PopulateGrid(ByVal isSearch As Boolean, Optional ByVal oAction As String = POPULATE_ACTION_NONE)
'        '            Dim oDataView As DataView

'        '            Try
'        '                If isSearch = False Then
'        '                    moReportSearchText.Text = String.Empty
'        '                    oDataView = GetDataView()
'        '                Else
'        '                    oDataView = GetSearchDataView()
'        '                End If

'        '                Me.State.searchDV = oDataView

'        '                moDataGrid.AutoGenerateColumns = False
'        '                oDataView.Sort = Me.SortDirection


'        '                BasePopulateGrid(moDataGrid, oDataView, Guid.Empty, oAction)

'        '                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.moInstanceId, Me.moDataGrid, Me.State.mnPageIndex)
'        '                Me.State.mnPageIndex = moDataGrid.PageIndex

'        '                ControlMgr.SetVisibleControl(Me, moDataGrid, True)
'        '                ControlMgr.SetVisibleControl(Me, trPageSize, moDataGrid.Visible)


'        '                If oDataView.Count > 0 Then
'        '                    Me.State.IsGridVisible = True
'        '                Else
'        '                    Me.State.IsGridVisible = False
'        '                End If

'        '                Session("recCount") = oDataView.Count

'        '                Me.HandleGridMessages(oDataView.Count, True)

'        '                Me.SortAndBindGrid()

'        '                If oDataView.Count > 0 Then
'        '                    If Not moDataGrid.BottomPagerRow.Visible Then moDataGrid.BottomPagerRow.Visible = True
'        '                End If

'        '            Catch ex As Exception
'        '                Me.HandleErrors(ex, ErrorCtrl)
'        '            End Try
'        '        End Sub

'        '        Private Sub PopulateAll()
'        '            ReportCeBase.PopulateUserReportDrop(moReportDrop)
'        '            Dim userId As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
'        '            Dim count As Integer
'        '            count = ReportRequests.GetAccessCountByUser(userId)
'        '            If count > 0 Then
'        '                Dim dt As DataTable = ReportRequests.GetReportsByUser(userId)
'        '                For Each dr As DataRow In dt.Rows
'        '                    moReportDrop.Items.Add(New ListItem(dr("REPORT_TYPE"), "DBRPT_" + dr("REPORT_TYPE")))
'        '                Next
'        '            End If

'        '        End Sub

'        '#End Region

'        '#Region "Clear"

'        '        Private Sub Clear()
'        '            ControlMgr.SetVisibleControl(Me, trPageSize, False)
'        '            ControlMgr.SetVisibleControl(Me, moDataGrid, False)
'        '            moReportSearchText.Enabled = True
'        '            moReportSearchText.Text = Nothing
'        '            btnReportSearch.Enabled = True
'        '        End Sub
'        '#End Region

'        '#Region "Crystal Enterprise"


'        '        Function SetParameters(ByVal nInstanceId As Long, ByVal ceFormat As String) As ReportCeBaseForm.Params
'        '            Dim params As New ReportCeBaseForm.Params
'        '            Dim reportName As String = String.Empty
'        '            Dim moReportFormat As ReportCeBaseForm.RptFormat
'        '            If moReportDrop.SelectedIndex <> BLANK_ITEM_SELECTED Then
'        '                reportName = GetSelectedDescription(moReportDrop)
'        '            End If

'        '            moReportFormat = ReportCeBase.GetReportFormat(ceFormat)

'        '            With params
'        '                .msRptName = reportName
'        '                .msRptWindowName = reportName
'        '                .moRptFormat = moReportFormat
'        '                .moAction = ReportCeBaseForm.RptAction.VIEW
'        '                .instanceId = nInstanceId
'        '            End With
'        '            Return params
'        '        End Function

'        '        Private Sub ViewReport(ByVal nInstanceId As Long, ByVal ceFormat As String)

'        '            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

'        '            Dim params As ReportCeBaseForm.Params = SetParameters(nInstanceId, ceFormat)
'        '            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

'        '        End Sub

'        '#End Region

'        '        Protected Overrides Sub Finalize()
'        '            MyBase.Finalize()
'        '        End Sub


'        '        Private Sub TransferFilesUnixWebServer(fileName As String)

'        '            Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, AppConfig.UnixServer.FtpDirectory.Replace("/ftp", "/data"), AppConfig.UnixServer.UserId,
'        '                                 AppConfig.UnixServer.Password)

'        '            Try
'        '                System.Threading.Thread.CurrentThread.Sleep(10000)
'        '                Dim userPathWebServer As String = MiscUtil.GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
'        '                MiscUtil.CreateFolder(userPathWebServer)

'        '                Dim destinationFile As String = userPathWebServer & "\" & fileName
'        '                objUnixFTP.DownloadFile(fileName, destinationFile)
'        '                System.Threading.Thread.CurrentThread.Sleep(5000)

'        '                Dim zipFile As String
'        '                zipFile = userPathWebServer & "_reportdata"
'        '                Assurant.Common.Zip.aZip.CreateZipFile(zipFile, userPathWebServer, False, Nothing)
'        '                System.IO.Directory.Delete(userPathWebServer, True)

'        '                Dim zipName As String = System.IO.Path.GetFileName(zipFile + ".zip")
'        '                InitDownLoad(zipName, AppConfig.UnixServer.InterfaceDirectory)
'        '            Finally

'        '            End Try
'        '        End Sub


'        '        Private Sub InitDownLoad(ByVal zipFileName As String, ByVal sourceDirectory As String)
'        '            Dim sJavaScript As String
'        '            Dim params As DownLoadBase.DownLoadParams

'        '            params.downLoadCode = DownLoadBase.DownLoadParams.DownLoadTypeCode.FILE

'        '            params.fileName = sourceDirectory & "/" & zipFileName
'        '            params.DeleteFileAfterDownload = True

'        '            Session(DownLoadBase.SESSION_PARAMETERS_DOWNLOAD_KEY) = params
'        '            sJavaScript = "<SCRIPT>" & Environment.NewLine
'        '            sJavaScript &= "showReportViewerFrame('../Common/DownLoadWindowForm.aspx'); " & Environment.NewLine
'        '            sJavaScript &= "</SCRIPT>" & Environment.NewLine
'        '            RegisterStartupScript("EnableReportCe", sJavaScript)
'        '        End Sub

'        '        Protected Sub btnReportSearch_Click(sender As Object, e As EventArgs)
'        '            Me.State.isSearch = True
'        '            PopulateGrid(True)
'        '        End Sub

'        '        Protected Function GetSearchDataView() As DataView
'        '            'call method to get reports
'        '            Dim dsReportInstances As New DataSet
'        '            Dim dtReportInstances As New DataTable("INSTANCES")
'        '            Dim drRow As DataRow
'        '            Dim oDataView As DataView

'        '            dtReportInstances.Columns.Add("ID", GetType(Byte()))
'        '            dtReportInstances.Columns.Add("INSTANCE_KEY", GetType(Long))
'        '            dtReportInstances.Columns.Add("START_INSTANCE_TIMESTAMP", GetType(String))
'        '            dtReportInstances.Columns.Add("END_INSTANCE_TIMESTAMP", GetType(String))
'        '            dtReportInstances.Columns.Add("STATUS", GetType(String))
'        '            dtReportInstances.Columns.Add("FORMAT", GetType(String))
'        '            dtReportInstances.Columns.Add("DESTINATION", GetType(String))
'        '            dtReportInstances.Columns.Add("FILENAME", GetType(String))
'        '            dtReportInstances.Columns.Add("OWNER", GetType(String))

'        '            Dim userId As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
'        '            Dim requestDT As DataTable = ReportRequests.LoadRequestsByReportKey(userId, moReportSearchText.Text)
'        '            If requestDT.Rows.Count = 0 Then
'        '                moReportDrop.SelectedIndex = -1
'        '            End If
'        '            Dim instanceKey As Integer = 1

'        '            For Each dr As DataRow In requestDT.Rows
'        '                drRow = dtReportInstances.NewRow()
'        '                drRow("ID") = dr("REPORT_REQUEST_ID")
'        '                drRow("INSTANCE_KEY") = instanceKey.ToString()
'        '                drRow("START_INSTANCE_TIMESTAMP") = dr("START_DATE")
'        '                drRow("END_INSTANCE_TIMESTAMP") = dr("END_DATE")
'        '                drRow("STATUS") = dr("STATUS")
'        '                drRow("FORMAT") = "Text"
'        '                drRow("DESTINATION") = "FTP"
'        '                drRow("FILENAME") = dr("FTP_FILENAME")
'        '                drRow("OWNER") = ElitaPlusIdentity.Current.ActiveUser.UserName
'        '                moReportDrop.SelectedValue = "DBRPT_" + dr("REPORT_TYPE")
'        '                dtReportInstances.Rows.Add(drRow)
'        '                instanceKey = instanceKey + 1
'        '            Next
'        '            oDataView = dtReportInstances.DefaultView
'        '            oDataView.RowFilter = CEHelper.JobDataview.COL_OWNER & " = '" & ElitaPlusIdentity.Current.ActiveUser.UserName & "'"
'        '            oDataView.Sort = moDataGrid.DataMember()

'        '            Return oDataView
'        '        End Function
    End Class

End Namespace
