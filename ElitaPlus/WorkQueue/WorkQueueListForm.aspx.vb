Imports System.Collections.Generic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Tables
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class WorkQueueListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const PAGETAB As String = "TABLES"
    Public Const PAGESUBTAB As String = "WORK_QUEUE"
    Public Const PAGETITLE As String = "WORK_QUEUE_SEARCH"
    Public Const BREADCRUMTITLE_WORKQUEUE_MAINTENANCE As String = "WORK_QUEUE_MAINTENANCE"
    Public Const BREADCRUMTITLE_WORKQUEUE_STAFFING As String = "WORK_QUEUE_STAFFING"
    Public Const URL As String = "~/WorkQueue/WorkQueueListForm.aspx"

    Public Const GRID_TOTAL_COLUMNS As Integer = 7
    Public Const GRID_COL_NAME As Integer = 0
    Public Const GRID_COL_OLDEST_ITEM_DATE As Integer = 1
    Public Const GRID_COL_ITEMS_AVAILABLE As Integer = 2
    Public Const GRID_COL_TOTAL_ITEMS As Integer = 3
    Public Const GRID_COL_ITEMS_BEING_WORKED As Integer = 4
    Public Const GRID_COL_TOTAL_PEOPLE As Integer = 5
    Public Const GRID_COL_WORK_QUEUE_HISTORY As Integer = 6
#End Region

#Region "Return Type"
    Public Class WorkQueueReturnType
        Public WorkQueueName As String
        Public CompanyId As Guid
        Public ActionId As Guid
        Public ActiveOn As Nullable(Of Date)
        Public WorkQueueId As Guid
    End Class
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False
    Private ReturnType As WorkQueueReturnType = Nothing

    Class MyState
        Public IsWorkQueueStaffing As Boolean = False

        ' Selected Item Information
        Public SearchResults() As WrkQueue.WorkQueue = Nothing
        Public UserHasPermissions As Boolean ' Stores if User has Permission to Create Work Queue or View Stats
        Public PageIndex As Integer ' Stores Current Page Index
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE ' Stores Current Page Size
        Public SortExpression As String = "Name" ' Stores Sort Column Name
        Public SortDirection As LinqExtentions.SortDirection = LinqExtentions.SortDirection.Ascending ' Stores Sort Direction
        Public wrkQResults As WrkQueue.WorkQueue() = Nothing

        Sub New()

        End Sub
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

#Region "Page Event Handlers"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If (Not IsReturningFromChild) Then MasterPage.MessageController.Clear()
        Form.DefaultButton = btnSearch.UniqueID
        Try

            If (Not IsPostBack) Then

                UpdateBreadCrum()

                If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = False Then
                    PlaceHolder1.Visible = False
                    PlaceHolder2.Visible = False

                    moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_DISABLE_FUNCTIONALITY, True)
                    Throw New GUIException("", "")
                End If

                ' Check if User has Permissions
                AddCalendar_New(imgActiveOn, moActiveOn)

                ' Populate Action and Company Drop Downs
                'Me.BindListControlToDataView(Me.moCompanyName, LookupListNew.GetCompanyLookupList())

                Dim Companies As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="Company")

                moCompanyName.Populate(Companies.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                'Me.BindListControlToDataView(Me.moAction, LookupListNew.GetWorkQueueAction(ElitaPlusIdentity.Current.ActiveUser.LanguageId, True))

                Dim WorkQueueActions As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="WQ_ACTION",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                moAction.Populate(WorkQueueActions.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                ' Check Query String and set the Value in Me.State
                If (Request.QueryString("CALLER") = "STAFFING") Then
                    State.IsWorkQueueStaffing = True
                    WorkQueueGrid.Visible = False
                    WorkQueueStaffingGrid.Visible = True

                    State.UserHasPermissions = ElitaPlusIdentity.Current.ActiveUser.ExtendedUser.HasPermission(New Auth.Permission With {.ResourceType = ServiceHelper.RESTYP_WORKQUEUESYSTEM, .Resource = ServiceHelper.RES_WORKQUEUESYSTEM, .Action = ServiceHelper.PA_WQS_VIEW_STATISTICS})
                Else
                    State.UserHasPermissions = True
                    Dim extendedUser As Auth.ExtendedUser
                    extendedUser = ElitaPlusIdentity.Current.ActiveUser.ExtendedUser
                    If (Not extendedUser.HasPermission(New Auth.Permission With {.ResourceType = ServiceHelper.RESTYP_WORKQUEUESYSTEM, .Resource = ServiceHelper.RES_WORKQUEUESYSTEM, .Action = ServiceHelper.PA_WQS_CREATE_QUEUE})) Then
                        State.UserHasPermissions = False
                    End If
                    If (State.UserHasPermissions AndAlso Not extendedUser.HasPermission(New Auth.Permission With {.ResourceType = ServiceHelper.RESTYP_WORKQUEUESYSTEM, .Resource = ServiceHelper.RES_WORKQUEUESYSTEM, .Action = ServiceHelper.PA_WQS_MANAGE_ITEM_STATUS})) Then
                        State.UserHasPermissions = False
                    End If

                    State.IsWorkQueueStaffing = False
                    WorkQueueGrid.Visible = True
                    WorkQueueStaffingGrid.Visible = False
                End If

                ' Translate Grid Headers
                TranslateGridHeader(Grid)

                ' Populate Search Criteria if Returning from Page and Information is Provided
                If (IsReturningFromChild AndAlso (Not (ReturnType Is Nothing))) Then
                    moWorkQueueName.Text = ReturnType.WorkQueueName
                    PopulateControlFromBOProperty(moCompanyName, ReturnType.CompanyId)
                    PopulateControlFromBOProperty(moAction, ReturnType.ActionId)
                    If (ReturnType.ActiveOn.HasValue) Then
                        moActiveOn.Text = ReturnType.ActiveOn.Value.ToString()
                    End If
                    PopulateGrid()
                End If
            End If

            If Not Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = False Then
                moMessageController.Clear()
            End If

            If (State.UserHasPermissions) Then
                ControlMgr.SetEnableControl(Me, btnAdd_WRITE, True AndAlso Not State.IsWorkQueueStaffing)
            Else
                If (State.IsWorkQueueStaffing) Then
                    moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_NO_VIEWSTAT_PERMISSION, True)
                    moSearchResults.Visible = False
                    searchGrid.Visible = False
                Else
                    moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_NO_CREATEQUEUE_MANAGEITEMSTATUSLIST_PERMISSION, True)
                End If
                ControlMgr.SetEnableControl(Me, btnAdd_WRITE, False)
            End If


        Catch ex As GUIException

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        'Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub

    Private Sub WorkQueueListForm_PageReturn(ReturnFromUrl As String, ReturnParameter As Object) Handles Me.PageReturn, Me.PageCall
        MasterPage.MessageController.Clear()
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            If (ReturnParameter Is Nothing) Then
                Exit Sub
            End If

            Dim returnObj As PageReturnType(Of WorkQueueReturnType) = CType(ReturnParameter, PageReturnType(Of WorkQueueReturnType))
            If returnObj.HasDataChanged Then
                State.SearchResults = Nothing
            End If
            ReturnType = returnObj.EditingBo
            Select Case returnObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    'If Not returnObj.EditingBo.IsNew Then
                    '    Me.State.SelectedWorkQueueId = returnObj.EditingBo.Id
                    'End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateBreadCrum()
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGESUBTAB) & ElitaBase.Sperator
        If (State.IsWorkQueueStaffing) Then
            MasterPage.BreadCrum = MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage(BREADCRUMTITLE_WORKQUEUE_STAFFING)
        Else
            MasterPage.BreadCrum = MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage(BREADCRUMTITLE_WORKQUEUE_MAINTENANCE)
        End If
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        MasterPage.UsePageTabTitleInBreadCrum = False
    End Sub
#End Region

#Region "Button Event Handlers"
    Protected Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid(True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            moWorkQueueName.Text = String.Empty
            moAction.SelectedIndex = BLANK_ITEM_SELECTED
            moCompanyName.SelectedIndex = BLANK_ITEM_SELECTED
            moActiveOn.Text = String.Empty
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If (moWorkQueueName.Text = "" AndAlso _
                moCompanyName.SelectedIndex = BLANK_ITEM_SELECTED AndAlso _
                moAction.SelectedIndex = BLANK_ITEM_SELECTED) AndAlso _
                moActiveOn.Text = "" Then
                MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                Exit Sub
            End If
            'Reset the Caching on Search Results
            State.wrkQResults = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateGrid(Optional ByVal updatePageIndex As Boolean = False)
        Dim activeOnDate As Nullable(Of Date) = Nothing
        Dim activeOn As Date
        Dim recCount As Integer
        Try
            If (DateTime.TryParse(DateHelper.GetDateValue(moActiveOn.Text), activeOn)) Then
                activeOnDate = New Nullable(Of Date)(activeOn)
            End If

            Grid.PageSize = State.PageSize
            If (State.IsWorkQueueStaffing) Then
                Dim result As WrkQueue.WorkQueue()
                If State.wrkQResults Is Nothing Then
                    result = WorkQueue.GetStatList(If(moWorkQueueName.Text = "", "*", moWorkQueueName.Text), _
                                                         LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList(), GetSelectedItem(moCompanyName)), _
                                                         LookupListNew.GetCodeFromId(LookupListNew.LK_WQ_ACTION, GetSelectedItem(moAction)), activeOnDate)
                    State.wrkQResults = result
                Else
                    result = State.wrkQResults
                End If
               recCount = result.Length
                If (updatePageIndex) Then
                    State.PageIndex = NewCurrentPageIndex(Grid, recCount, State.PageSize)
                    Grid.PageIndex = State.PageIndex
                End If

                WorkQueueStaffingGrid.DataSource = result.OrderBy(State.SortExpression, State.SortDirection).ToList()
                HighLightSortColumn(Grid, State.SortExpression + If(Me.State.SortDirection = LinqExtentions.SortDirection.Descending, " DESC", String.Empty), True)
                WorkQueueStaffingGrid.DataBind()
            Else
                Dim result As WrkQueue.WorkQueue()
                Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                If State.wrkQResults Is Nothing Then
                    result = WorkQueue.GetList(If(moWorkQueueName.Text = "", "*", moWorkQueueName.Text), _
                                                       LookupListNew.GetCodeFromId(LookupListNew.GetCompanyLookupList(), GetSelectedItem(moCompanyName)), _
                                                       LookupListNew.GetCodeFromId(LookupListNew.LK_WQ_ACTION, GetSelectedItem(moAction)), activeOnDate, False)
                Else
                    result = State.wrkQResults
                End If
                recCount = result.Length
                If (updatePageIndex) Then
                    State.PageIndex = NewCurrentPageIndex(Grid, recCount, State.PageSize)
                    Grid.PageIndex = State.PageIndex
                End If

                WorkQueueGrid.DataSource = result.OrderBy(State.SortExpression, State.SortDirection).ToList()
                HighLightSortColumn(Grid, State.SortExpression + If(Me.State.SortDirection = LinqExtentions.SortDirection.Descending, " DESC", String.Empty), True)
                WorkQueueGrid.DataBind()
            End If
            If (Grid.Rows.Count = 0) Then
                MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
            Else
                ControlMgr.SetVisibleControl(Me, moSearchResults, True)
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private ReadOnly Property Grid As GridView
        Get
            If (State.IsWorkQueueStaffing) Then
                Return WorkQueueStaffingGrid
            Else
                Return WorkQueueGrid
            End If
        End Get
    End Property
#End Region

#Region "Grid Events"

    Private Sub Grid_Sorting(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles WorkQueueStaffingGrid.Sorting, WorkQueueGrid.Sorting
        Try
            If State.SortExpression = e.SortExpression Then
                If Me.State.SortDirection = LinqExtentions.SortDirection.Descending Then
                    State.SortDirection = LinqExtentions.SortDirection.Ascending
                Else
                    State.SortDirection = LinqExtentions.SortDirection.Descending
                End If
            Else
                State.SortExpression = e.SortExpression
                State.SortDirection = LinqExtentions.SortDirection.Ascending
            End If
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles WorkQueueStaffingGrid.PageIndexChanged, WorkQueueGrid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles WorkQueueStaffingGrid.PageIndexChanging, WorkQueueGrid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs) Handles WorkQueueStaffingGrid.RowCreated, WorkQueueGrid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles WorkQueueStaffingGrid.RowCommand, WorkQueueGrid.RowCommand
        Try
            Dim workQueueName As String
            Dim workQueueId As Guid
            Dim oWorkQueue As WorkQueue
            Dim workQueueAvailableItems As String
            Dim returnType As WorkQueueReturnType = New WorkQueueReturnType()
            Dim activeOn As Date
            With returnType
                .ActionId = GetSelectedItem(moAction)
                If (DateTime.TryParse(DateHelper.GetDateValue(moActiveOn.Text), activeOn)) Then
                    .ActiveOn = New Nullable(Of Date)(activeOn)
                End If
                .CompanyId = GetSelectedItem(moCompanyName)
                .WorkQueueName = moWorkQueueName.Text
            End With


            Select Case e.CommandName.ToString().ToUpper()
                Case "SELECTACTION"
                    returnType.WorkQueueId = New Guid(e.CommandArgument.ToString())
                    callPage(WorkQueueForm.URL, returnType)
                Case "WORKQUEUEUSER"
                    workQueueId = New Guid(e.CommandArgument.ToString())
                    WorkQueueStaffingGrid.SelectedIndex = DirectCast(DirectCast(DirectCast(e.CommandSource, System.Web.UI.WebControls.LinkButton).Parent, System.Web.UI.WebControls.DataControlFieldCell).Parent, System.Web.UI.WebControls.GridViewRow).RowIndex
                    workQueueName = CType(GetSelectedGridControl(WorkQueueStaffingGrid, GRID_COL_NAME), Label).Text
                    workQueueAvailableItems = CType(GetSelectedGridControl(WorkQueueStaffingGrid, GRID_COL_ITEMS_AVAILABLE), Label).Text
                    oWorkQueue = New WorkQueue(workQueueId)
                    callPage(WorkQueueUsersForm.URL, New WorkQueueUsersForm.Parameters(workQueueName, workQueueId, oWorkQueue.WorkQueue.CompanyCode, workQueueAvailableItems, , returnType))
                Case "WORKQUEUEHISTORY"
                    workQueueId = New Guid(e.CommandArgument.ToString())
                    WorkQueueStaffingGrid.SelectedIndex = DirectCast(DirectCast(DirectCast(e.CommandSource, System.Web.UI.WebControls.LinkButton).Parent, System.Web.UI.WebControls.DataControlFieldCell).Parent, System.Web.UI.WebControls.GridViewRow).RowIndex
                    workQueueName = CType(GetSelectedGridControl(WorkQueueStaffingGrid, GRID_COL_NAME), Label).Text
                    workQueueAvailableItems = CType(GetSelectedGridControl(WorkQueueStaffingGrid, GRID_COL_ITEMS_AVAILABLE), Label).Text
                    callPage(WorkqueueHistoryForm.URL, New WorkqueueHistoryForm.Parameters(workQueueName, workQueueId, workQueueAvailableItems, returnType))
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

    Private Sub btnAdd_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            Dim workQueueName As String
            Dim workQueueId As Guid
            Dim workQueueAvailableItems As String
            Dim returnType As WorkQueueReturnType = New WorkQueueReturnType()
            Dim activeOn As Date
            With returnType
                .ActionId = GetSelectedItem(moAction)
                If (DateTime.TryParse(DateHelper.GetDateValue(moActiveOn.Text), activeOn)) Then
                    .ActiveOn = New Nullable(Of Date)(activeOn)
                End If
                .CompanyId = GetSelectedItem(moCompanyName)
                .WorkQueueName = moWorkQueueName.Text
            End With

            callPage(WorkQueueForm.URL, returnType)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


End Class
