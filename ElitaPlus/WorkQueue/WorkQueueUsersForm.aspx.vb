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

        Public Sub New(ByVal strWorkQueueName As String, ByVal oWorkQueueId As Guid, ByVal strCompanyCode As String, ByVal strAvailableItems As String, _
                       Optional ByVal strUserName As String = Nothing, Optional ByVal WorkQueueReturnType As WorkQueueListForm.WorkQueueReturnType = Nothing)
            WorkQueueId = oWorkQueueId
            WorkQueueName = strWorkQueueName
            WorkQueueCompanyCode = strCompanyCode
            AvailableItems = strAvailableItems
            If Not strUserName Is Nothing Then
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
    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Dim oParam As WorkQueueUsersForm.Parameters
        Try
            If Not Me.CallingParameters Is Nothing Then

                oParam = CType(Me.CallingParameters, Parameters)
                Me.State.WorkqueueId = oParam.WorkQueueId
                Me.State.WorkqueueName = oParam.WorkQueueName
                Me.State.WorkqueueCompanyCode = oParam.WorkQueueCompanyCode
                Me.State.WorkqueueAvailableItems = oParam.AvailableItems
                Me.State.AddNewUserName = oParam.AddNewUserName
                Me.State.WorkQueueReturnType = oParam.WorkQueueReturnType
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
#End Region

#Region "Page_Events"

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("WORK_QUEUE_USERS")
            Me.MasterPage.PageTitle = Me.State.WorkqueueName & " : " & _
                                      TranslationBase.TranslateLabelOrMessage("ITEMS_AVAILABLE") & " : " & Me.State.WorkqueueAvailableItems
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Try
            If Not Me.IsPostBack Then
                Me.SortDirection = Me.State.SortExpression
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("WORK_QUEUE")
                UpdateBreadCrum()

                If Not Me.State.AddNewUserName Is Nothing Then
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_USER_ADDED_TO_THE_WORK_QUEUE)
                End If

                TranslateGridHeader(Grid)
                'GetStateProperties()
                divDataContainer.Visible = True
                Me.PopulateGrid()
                'Set page size
                cboPageSize.SelectedValue = Me.State.PageSize.ToString()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub PopulateGrid()
        Try
            Dim oDataView As DataView
            Dim sortBy As String = String.Empty
            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = GetDV()
            End If

            Grid.PageSize = State.PageSize
            If State.searchDV.Count = 0 Then
            Else
                Me.Grid.DataSource = Me.State.searchDV
            End If

            Me.State.PageIndex = Me.Grid.PageIndex
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

            If (Not Me.State.SortExpression.Equals(String.Empty)) Then
                Me.State.searchDV.Sort = Me.SortDirection 'Me.State.SortExpression
            End If

            HighLightSortColumn(Grid, Me.SortDirection, True)

            Me.Grid.DataBind()


            ControlMgr.SetVisibleControl(Me, Grid, True)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Function GetDV() As DataView
        Dim dv As DataView
        Me.State.searchDV = GetGridDataView()
        Me.State.searchDV.Sort = Grid.DataMember()
        Return (Me.State.searchDV)
    End Function

    Private Function GetGridDataView() As DataView
        Return (WorkqueueHistory.LoadWorkQueueUsersActions(Me.State.WorkqueueId, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
    End Function
#End Region

#Region "Grid related"
    Protected Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim historyActionId As Guid
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            If (e.Row.RowType = DataControlRowType.DataRow) OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                e.Row.Cells(Me.GRID_COL_NAME_USER_NAME).Text = CType(dvRow(WorkqueueHistoryDAL.COL_NAME_USER_NAME), String)
                e.Row.Cells(Me.GRID_COL_NAME_ITEM_ACCESSED).Text = CType(dvRow(WorkqueueHistoryDAL.COL_NAME_ITEMS_ACCESSED), String)
                e.Row.Cells(Me.GRID_COL_NAME_ITEMS_PROCESSED).Text = CType(dvRow(WorkqueueHistoryDAL.COL_NAME_ITEMS_PROCESSED), String)
                e.Row.Cells(Me.GRID_COL_NAME_ITEMS_REQUEUED).Text = CType(dvRow(WorkqueueHistoryDAL.COL_NAME_ITEMS_REQUEUED), String)
                e.Row.Cells(Me.GRID_COL_NAME_ITEMS_REDIRECTED).Text = CType(dvRow(WorkqueueHistoryDAL.COL_NAME_ITEMS_REDIRECTED), String)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If
            'Me.State.Id = Guid.Empty
            Me.State.PageIndex = 0

            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
            Me.PopulateGrid()
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

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

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            'Me.ReturnToCallingPage() 
            Me.callPage(WorkQueueListForm.URL & "?CALLER=STAFFING", New PageReturnType(Of WorkQueueListForm.WorkQueueReturnType)(DetailPageCommand.Nothing_, Me.State.WorkQueueReturnType, False))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub BtnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew_WRITE.Click
        Try
            Me.callPage(MaintainUserList.URL & MaintainUserList.STAFFING_QUERYSTRING, New MaintainUserList.Parameters(Me.State.WorkqueueName, Me.State.WorkqueueId, Me.State.WorkqueueCompanyCode, Me.State.WorkqueueAvailableItems, Me.State.WorkQueueReturnType))

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

End Class
