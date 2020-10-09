Namespace Tables
    Partial Public Class ScheduleSearchForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
        Public Const GRID_COL_SCHEDULE_CODE_CTRL As String = "btnEditSchedule"

        Public Const GRID_COL_NAME_SCHEDULE_ID As Integer = 0
        Public Const GRID_COL_COL_NAME_CODE As Integer = 1
        Public Const GRID_COL_NAME_DESCRIPTION As Integer = 2

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public PageIndex As Integer = 0
            Public PageSize As Integer = 10
            Public searchDV As Schedule.ScheduleSearchDV
            Public SearchClicked As Boolean = False
            Public IsGridVisible As Boolean = False
            Public selectedScheduleId As Guid = Guid.Empty
            Public scheduleCode As String = String.Empty
            Public scheduleDescription As String = String.Empty
            Public SortExpression As String = Schedule.ScheduleSearchDV.COL_NAME_CODE & " DESC"
            'Public SortExpression As String = String.Empty
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

#Region "Page_Events"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("GENERAL_INFORMATION") &
                                          ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("SEARCH_SCHEDULE")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("SEARCH_SCHEDULE")
            End If
        End Sub

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            MasterPage.MessageController.Clear()
            Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not IsPostBack Then

                    UpdateBreadCrum()
                    If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = False Then
                        PlaceHolder1.Visible = False
                        PlaceHolder2.Visible = False

                        moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_DISABLE_FUNCTIONALITY, True)
                        Throw New GUIException("", "")
                    End If
                    'Me.SortDirection = Me.State.SortExpression
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("TABLES")

                    TranslateGridHeader(Grid)
                    GetStateProperties()

                    divDataContainer.Visible = False

                    If State.IsGridVisible Then
                        divDataContainer.Visible = True
                        PopulateGrid()
                    End If

                    'Set page size
                    cboPageSize.SelectedValue = State.PageSize.ToString()
                    moMessageController.Clear()
                End If
                DisplayNewProgressBarOnClick(btnSearch, "LOADING_SCHEDULES")

            Catch ex As GUIException

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

        End Sub
#End Region

#Region "Helper functions"
        Private Sub GetStateProperties()
            Try
                moScheduleCode.Text = State.scheduleCode
                moScheduleDescription.Text = State.scheduleDescription
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                Dim oDataView As DataView
                Dim sortBy As String = String.Empty
                If (State.searchDV Is Nothing) Then
                    State.searchDV = Schedule.GetSchedulesList(State.scheduleCode, State.scheduleDescription)
                    If State.SearchClicked Then
                        ValidSearchResultCountNew(State.searchDV.Count, True)
                        State.SearchClicked = False
                    End If
                End If

                Grid.PageSize = State.PageSize
                If State.searchDV.Count = 0 Then
                Else
                    ''''SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedScheduleId, Me.Grid, Me.State.PageIndex)
                    Grid.DataSource = State.searchDV
                End If

                State.PageIndex = Grid.PageIndex
                'lblSearchResults.Visible = True
                divDataContainer.Visible = True

                Grid.Columns(GRID_COL_NAME_SCHEDULE_ID).Visible = False
                Grid.Columns(GRID_COL_COL_NAME_CODE).Visible = True
                Grid.Columns(GRID_COL_COL_NAME_CODE).SortExpression = Schedule.ScheduleSearchDV.COL_NAME_CODE
                Grid.Columns(GRID_COL_NAME_DESCRIPTION).Visible = True
                Grid.Columns(GRID_COL_NAME_DESCRIPTION).SortExpression = Schedule.ScheduleSearchDV.COL_NAME_DESCRIPTION

                If (Not State.SortExpression.Equals(String.Empty)) Then
                    State.searchDV.Sort = State.SortExpression 'Me.SortDirection
                End If

                HighLightSortColumn(Grid, State.SortExpression, True) 'Me.SortDirection
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Session("recCount") = State.searchDV.Count

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

        Public Sub ClearSearch()
            Try
                moScheduleCode.Text = String.Empty
                moScheduleDescription.Text = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetStateProperties()
            Try
                If State Is Nothing Then
                    Trace(Me, "Restoring State")
                    RestoreState(New MyState)
                End If

                State.scheduleCode = moScheduleCode.Text.ToUpper.Trim
                State.scheduleDescription = moScheduleDescription.Text.ToUpper.Trim
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region " Button Clicks "

        Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                State.SearchClicked = True
                State.PageIndex = 0
                State.selectedScheduleId = Guid.Empty
                SetStateProperties()
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try
                ClearSearch()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew_WRITE.Click
            Try
                State.selectedScheduleId = Guid.Empty
                callPage(ScheduleForm.URL, State.selectedScheduleId)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Grid related"

        Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If (e.Row.RowType = DataControlRowType.DataRow) _
                    OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                    If (e.Row.Cells(GRID_COL_NAME_SCHEDULE_ID).FindControl(GRID_COL_SCHEDULE_CODE_CTRL) IsNot Nothing) Then
                        btnEditItem = CType(e.Row.Cells(GRID_COL_NAME_SCHEDULE_ID).FindControl(GRID_COL_SCHEDULE_CODE_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Schedule.ScheduleSearchDV.COL_NAME_SCHEDULE_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(Schedule.ScheduleSearchDV.COL_NAME_CODE).ToString
                    End If

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = SELECT_ACTION_COMMAND Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        State.selectedScheduleId = New Guid(e.CommandArgument.ToString())
                        callPage(ScheduleForm.URL, State.selectedScheduleId)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
               (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
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

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.selectedScheduleId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class
End Namespace