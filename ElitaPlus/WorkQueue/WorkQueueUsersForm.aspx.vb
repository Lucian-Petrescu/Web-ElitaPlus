Imports Assurant.ElitaPlus.DALObjects

Public Class WorkQueueUsersForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const GRID_COL_NAME_USER_NAME As Integer = 0
    Public Const GRID_COL_NAME_ITEM_ACCESSED As Integer = 1
    Public Const GRID_COL_NAME_ITEMS_PROCESSED As Integer = 2
    Public Const GRID_COL_NAME_ITEMS_REQUEUED As Integer = 3
    Public Const GRID_COL_NAME_ITEMS_REDIRECTED As Integer = 4

#End Region

#Region "Page State"

#Region "Parameters"

    Public Class Parameters
        Public WorkQueueId As Guid
        Public WorkQueueName As String
        Public WorkQueueCompanyCode As String
        Public AvailableItems As String
        Public AddNewUserName As String
        Public WorkQueueReturnType As WorkQueueListForm.WorkQueueReturnType

        Public Sub New(strWorkQueueName As String, oWorkQueueId As Guid, strCompanyCode As String, strAvailableItems As String, _
                       Optional ByVal strUserName As String = Nothing, Optional ByVal WorkQueueReturnType As WorkQueueListForm.WorkQueueReturnType = Nothing)
            WorkQueueId = oWorkQueueId
            WorkQueueName = strWorkQueueName
            WorkQueueCompanyCode = strCompanyCode
            AvailableItems = strAvailableItems
            If strUserName IsNot Nothing Then
                AddNewUserName = strUserName
            End If
            Me.WorkQueueReturnType = WorkQueueReturnType
        End Sub

    End Class

#End Region

    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 10
        Public searchDV As DataView = Nothing
        Public IsGridVisible As Boolean = False
        Public WorkqueueItemId As Guid = Guid.Empty
        Public WorkqueueId As Guid = Guid.Empty
        Public WorkqueueName As String = String.Empty
        Public WorkqueueCompanyCode As String = String.Empty
        Public AddNewUserName As String = String.Empty
        Public WorkqueueAvailableItems As String = String.Empty
        Public SortExpression As String = WorkqueueHistoryDAL.COL_NAME_USER_NAME
        Public WorkQueueReturnType As WorkQueueListForm.WorkQueueReturnType
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
    Public Const URL As String = "~/WorkQueue/WorkqueueUsersForm.aspx"
#End Region


#Region "Page Parameters"
    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Dim oParam As WorkQueueUsersForm.Parameters
        Try
            If CallingParameters IsNot Nothing Then

                oParam = CType(CallingParameters, Parameters)
                State.WorkqueueId = oParam.WorkQueueId
                State.WorkqueueName = oParam.WorkQueueName
                State.WorkqueueCompanyCode = oParam.WorkQueueCompanyCode
                State.WorkqueueAvailableItems = oParam.AvailableItems
                State.AddNewUserName = oParam.AddNewUserName
                State.WorkQueueReturnType = oParam.WorkQueueReturnType
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
#End Region

#Region "Page_Events"

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("WORK_QUEUE_USERS")
            MasterPage.PageTitle = State.WorkqueueName & " : " & _
                                      TranslationBase.TranslateLabelOrMessage("ITEMS_AVAILABLE") & " : " & State.WorkqueueAvailableItems
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()
        Try
            If Not IsPostBack Then
                SortDirection = State.SortExpression
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("WORK_QUEUE")
                UpdateBreadCrum()

                If State.AddNewUserName IsNot Nothing Then
                    MasterPage.MessageController.AddInformation(Message.MSG_USER_ADDED_TO_THE_WORK_QUEUE)
                End If

                TranslateGridHeader(Grid)
                'GetStateProperties()
                divDataContainer.Visible = True
                PopulateGrid()
                'Set page size
                cboPageSize.SelectedValue = State.PageSize.ToString()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub PopulateGrid()
        Try
            Dim oDataView As DataView
            Dim sortBy As String = String.Empty
            If (State.searchDV Is Nothing) Then
                State.searchDV = GetDV()
            End If

            Grid.PageSize = State.PageSize
            If State.searchDV.Count = 0 Then
            Else
                Grid.DataSource = State.searchDV
            End If

            State.PageIndex = Grid.PageIndex
            divDataContainer.Visible = True

            Grid.Columns(GRID_COL_NAME_USER_NAME).Visible = True
            Grid.Columns(GRID_COL_NAME_USER_NAME).SortExpression = WorkqueueHistoryDAL.COL_NAME_USER_NAME
            Grid.Columns(GRID_COL_NAME_ITEM_ACCESSED).Visible = True
            Grid.Columns(GRID_COL_NAME_ITEM_ACCESSED).SortExpression = WorkqueueHistoryDAL.COL_NAME_ITEMS_ACCESSED
            Grid.Columns(GRID_COL_NAME_ITEMS_PROCESSED).Visible = True
            Grid.Columns(GRID_COL_NAME_ITEMS_PROCESSED).SortExpression = WorkqueueHistoryDAL.COL_NAME_ITEMS_PROCESSED
            Grid.Columns(GRID_COL_NAME_ITEMS_REQUEUED).Visible = True
            Grid.Columns(GRID_COL_NAME_ITEMS_REQUEUED).SortExpression = WorkqueueHistoryDAL.COL_NAME_ITEMS_REQUEUED
            Grid.Columns(GRID_COL_NAME_ITEMS_REDIRECTED).Visible = True
            Grid.Columns(GRID_COL_NAME_ITEMS_REDIRECTED).SortExpression = WorkqueueHistoryDAL.COL_NAME_ITEMS_REDIRECTED

            If (Not State.SortExpression.Equals(String.Empty)) Then
                State.searchDV.Sort = SortDirection 'Me.State.SortExpression
            End If

            HighLightSortColumn(Grid, SortDirection, True)

            Grid.DataBind()


            ControlMgr.SetVisibleControl(Me, Grid, True)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            If Grid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

            If State.searchDV.Count = 0 Then
                For Each gvRow As GridViewRow In Grid.Rows
                    gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
                lblPageSize.Visible = False
                cboPageSize.Visible = False
                colonSepertor.Visible = False
            Else
                lblPageSize.Visible = True
                cboPageSize.Visible = True
                colonSepertor.Visible = True
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Function GetDV() As DataView
        Dim dv As DataView
        State.searchDV = GetGridDataView()
        State.searchDV.Sort = Grid.DataMember()
        Return (State.searchDV)
    End Function

    Private Function GetGridDataView() As DataView
        Return (WorkqueueHistory.LoadWorkQueueUsersActions(State.WorkqueueId, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
    End Function
#End Region

#Region "Grid related"
    Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim historyActionId As Guid
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                e.Row.Cells(GRID_COL_NAME_USER_NAME).Text = CType(dvRow(WorkqueueHistoryDAL.COL_NAME_USER_NAME), String)
                e.Row.Cells(GRID_COL_NAME_ITEM_ACCESSED).Text = CType(dvRow(WorkqueueHistoryDAL.COL_NAME_ITEMS_ACCESSED), String)
                e.Row.Cells(GRID_COL_NAME_ITEMS_PROCESSED).Text = CType(dvRow(WorkqueueHistoryDAL.COL_NAME_ITEMS_PROCESSED), String)
                e.Row.Cells(GRID_COL_NAME_ITEMS_REQUEUED).Text = CType(dvRow(WorkqueueHistoryDAL.COL_NAME_ITEMS_REQUEUED), String)
                e.Row.Cells(GRID_COL_NAME_ITEMS_REDIRECTED).Text = CType(dvRow(WorkqueueHistoryDAL.COL_NAME_ITEMS_REDIRECTED), String)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_Sorting(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            'Me.State.Id = Guid.Empty
            State.PageIndex = 0

            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    'Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
    '    Try
    '        Me.State.PageIndex = Grid.PageIndex
    '        PopulateGrid()
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
            PopulateGrid()
            Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

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

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            'Me.ReturnToCallingPage() 
            callPage(WorkQueueListForm.URL & "?CALLER=STAFFING", New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(DetailPageCommand.Nothing_, State.WorkQueueReturnType, False))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub BtnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew_WRITE.Click
        Try
            callPage(MaintainUserList.URL & MaintainUserList.STAFFING_QUERYSTRING, New MaintainUserList.Parameters(State.WorkqueueName, State.WorkqueueId, State.WorkqueueCompanyCode, State.WorkqueueAvailableItems, State.WorkQueueReturnType))

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

End Class
