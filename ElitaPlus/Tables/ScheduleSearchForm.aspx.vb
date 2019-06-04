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
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("GENERAL_INFORMATION") &
                                          ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("SEARCH_SCHEDULE")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("SEARCH_SCHEDULE")
            End If
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.MasterPage.MessageController.Clear()
            Me.Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not Me.IsPostBack Then

                    UpdateBreadCrum()
                    If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = False Then
                        PlaceHolder1.Visible = False
                        PlaceHolder2.Visible = False

                        moMessageController.AddWarning(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ERROR_DISABLE_FUNCTIONALITY, True)
                        Throw New GUIException("", "")
                    End If
                    'Me.SortDirection = Me.State.SortExpression
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("TABLES")

                    TranslateGridHeader(Grid)
                    GetStateProperties()

                    divDataContainer.Visible = False

                    If Me.State.IsGridVisible Then
                        divDataContainer.Visible = True
                        Me.PopulateGrid()
                    End If

                    'Set page size
                    cboPageSize.SelectedValue = Me.State.PageSize.ToString()
                    moMessageController.Clear()
                End If
                Me.DisplayNewProgressBarOnClick(Me.btnSearch, "LOADING_SCHEDULES")

            Catch ex As GUIException

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)

        End Sub
#End Region

#Region "Helper functions"
        Private Sub GetStateProperties()
            Try
                Me.moScheduleCode.Text = Me.State.scheduleCode
                Me.moScheduleDescription.Text = Me.State.scheduleDescription
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                Dim oDataView As DataView
                Dim sortBy As String = String.Empty
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = Schedule.GetSchedulesList(Me.State.scheduleCode, Me.State.scheduleDescription)
                    If Me.State.SearchClicked Then
                        Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
                        Me.State.SearchClicked = False
                    End If
                End If

                Grid.PageSize = State.PageSize
                If State.searchDV.Count = 0 Then
                Else
                    ''''SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedScheduleId, Me.Grid, Me.State.PageIndex)
                    Me.Grid.DataSource = Me.State.searchDV
                End If

                Me.State.PageIndex = Me.Grid.PageIndex
                'lblSearchResults.Visible = True
                divDataContainer.Visible = True

                Grid.Columns(GRID_COL_NAME_SCHEDULE_ID).Visible = False
                Grid.Columns(GRID_COL_COL_NAME_CODE).Visible = True
                Grid.Columns(GRID_COL_COL_NAME_CODE).SortExpression = Schedule.ScheduleSearchDV.COL_NAME_CODE
                Grid.Columns(GRID_COL_NAME_DESCRIPTION).Visible = True
                Grid.Columns(GRID_COL_NAME_DESCRIPTION).SortExpression = Schedule.ScheduleSearchDV.COL_NAME_DESCRIPTION

                If (Not Me.State.SortExpression.Equals(String.Empty)) Then
                    Me.State.searchDV.Sort = Me.State.SortExpression 'Me.SortDirection
                End If

                HighLightSortColumn(Grid, Me.State.SortExpression, True) 'Me.SortDirection
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Session("recCount") = Me.State.searchDV.Count

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

        Public Sub ClearSearch()
            Try
                Me.moScheduleCode.Text = String.Empty
                Me.moScheduleDescription.Text = String.Empty
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetStateProperties()
            Try
                If Me.State Is Nothing Then
                    Me.Trace(Me, "Restoring State")
                    Me.RestoreState(New MyState)
                End If

                Me.State.scheduleCode = Me.moScheduleCode.Text.ToUpper.Trim
                Me.State.scheduleDescription = Me.moScheduleDescription.Text.ToUpper.Trim
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region " Button Clicks "

        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.State.SearchClicked = True
                Me.State.PageIndex = 0
                Me.State.selectedScheduleId = Guid.Empty
                Me.SetStateProperties()
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                Me.ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew_WRITE.Click
            Try
                Me.State.selectedScheduleId = Guid.Empty
                Me.callPage(ScheduleForm.URL, Me.State.selectedScheduleId)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Grid related"

        Protected Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If (e.Row.RowType = DataControlRowType.DataRow) _
                    OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                    If (Not e.Row.Cells(Me.GRID_COL_NAME_SCHEDULE_ID).FindControl(GRID_COL_SCHEDULE_CODE_CTRL) Is Nothing) Then
                        btnEditItem = CType(e.Row.Cells(Me.GRID_COL_NAME_SCHEDULE_ID).FindControl(GRID_COL_SCHEDULE_CODE_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Schedule.ScheduleSearchDV.COL_NAME_SCHEDULE_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(Schedule.ScheduleSearchDV.COL_NAME_CODE).ToString
                    End If

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = SELECT_ACTION_COMMAND Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        Me.State.selectedScheduleId = New Guid(e.CommandArgument.ToString())
                        Me.callPage(ScheduleForm.URL, Me.State.selectedScheduleId)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
               (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
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

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = Grid.PageIndex
                Me.State.selectedScheduleId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class
End Namespace