Imports System.Threading

Namespace Tables

    Partial Class DepreciationScheduleListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Private Const DepreciationScheduleListForm001 As String = "DEPRECIATION_SCHEDULE_LIST_FORM001" ' Error while populating company
        Private Const LabelCompany As String = "COMPANY"

        Private Const GridColDepreciationScheduleCode As Integer = 0
        Private Const GridColDepreciationScheduleDescription As Integer = 1
        Private Const GridColDepreciationScheduleActive As Integer = 2
        Private Const GridColDepreciationScheduleId As Integer = 3

        Private Const SelectActionCommand As String = "SelectRecord"

#End Region

#Region "Page State"
        ' This class keeps the current state for the search page.
        Class MyState
            Public DepreciationScheduleId As Guid = Guid.Empty
            Public CompanyId As Guid = Guid.Empty


            Public SearchDv As DataView = Nothing
            Public PageIndex As Integer = 0
            Public SelectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public SortColumn As String = DepreciationScd.ColDepreciationScheduleCode
            Public SortDirection As WebControls.SortDirection = WebControls.SortDirection.Ascending

            Public ReadOnly Property SortExpression As String
                Get
                    Return String.Format("{0} {1}", SortColumn, If(SortDirection = WebControls.SortDirection.Ascending, "ASC", "DESC"))
                End Get
            End Property
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

#Region "Page Return"
        Private Sub Page_PageReturn(returnFromUrl As String, returnParameter As Object) Handles MyBase.PageReturn
            MenuEnabled = True
            Dim returnObject As PageReturnType(Of Object) = CType(returnParameter, PageReturnType(Of Object))
            If (returnObject.HasDataChanged) Then State.SearchDv = Nothing
        End Sub

#End Region

#Region "Page_Events"
        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    MasterPage.MessageController.Clear()
                    UpdateBreadCrum()
                    PopulateDropdown()
                    TranslateGridHeader(DepreciationScheduleGridView)
                    SetGridItemStyleColor(DepreciationScheduleGridView)
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            Dim pageTitle = TranslationBase.TranslateLabelOrMessage("DEPRECIATION_SCHEDULE_SEARCH")
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("Tables") & ElitaBase.Sperator & pageTitle
            MasterPage.PageTitle = pageTitle
            MasterPage.UsePageTabTitleInBreadCrum = False
        End Sub
        Private Sub PopulateDropdown()
            PopulateCompany()
        End Sub
        Private Sub PopulateCompany()
            Try
                CompanyMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LabelCompany)
                CompanyMultipleDrop.AutoPostBackDD = False
                CompanyMultipleDrop.NothingSelected = False
                CompanyMultipleDrop.BindData(LookupListNew.GetUserCompaniesLookupList())
                If Not State.CompanyId.Equals(Guid.Empty) Then
                    CompanyMultipleDrop.SelectedGuid = State.CompanyId
                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(DepreciationScheduleListForm001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub
        Private Sub PopulateGrid()
            Try
                If (State.SearchDv Is Nothing) AndAlso Not State.CompanyId.Equals(Guid.Empty) Then
                    State.SearchDv = DepreciationScd.LoadList(State.CompanyId)
                End If
                DepreciationScheduleGridView.AutoGenerateColumns = False

                If (State.SearchDv Is Nothing) OrElse (State.SearchDv.Count = 0) Then
                    ControlMgr.SetVisibleControl(Me, DepreciationScheduleGridView, False)
                    RecordCountLabel.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
                Else
                    ControlMgr.SetVisibleControl(Me, DepreciationScheduleGridView, True)
                    State.SearchDv.Sort = State.SortExpression
                    DepreciationScheduleGridView.Columns(GridColDepreciationScheduleCode).SortExpression = DepreciationScd.ColDepreciationScheduleCode
                    DepreciationScheduleGridView.Columns(GridColDepreciationScheduleDescription).SortExpression = DepreciationScd.ColDepreciationScheduleDescription
                    DepreciationScheduleGridView.Columns(GridColDepreciationScheduleActive).SortExpression = DepreciationScd.ColDepreciationScheduleActive
                    DepreciationScheduleGridView.PageIndex = State.PageIndex
                    DepreciationScheduleGridView.PageSize = State.SelectedPageSize
                    DepreciationScheduleGridView.DataSource = State.SearchDv
                    HighLightSortColumn(DepreciationScheduleGridView, State.SortExpression, True)
                    DepreciationScheduleGridView.DataBind()
                    RecordCountLabel.Text = String.Format("{0} {1}", State.SearchDv.Count.ToString(), TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
                End If
                If State.CompanyId.Equals(Guid.Empty) Then
                    ControlMgr.SetVisibleControl(Me, PageSizeRow, False)
                Else
                    ControlMgr.SetVisibleControl(Me, PageSizeRow, True)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearchCriteria()
            CompanyMultipleDrop.SelectedIndex = 0
        End Sub
        Private Sub ClearSearchResults()
            State.PageIndex = 0
            State.CompanyId = Guid.Empty
            State.SearchDv = Nothing
        End Sub

#End Region

#Region "Datagrid Related"
        Private Sub DepreciationScheduleGridView_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles DepreciationScheduleGridView.RowCreated
            BaseItemCreated(sender, e)
        End Sub
        Private Sub DepreciationScheduleGridView_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DepreciationScheduleGridView.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton

                If dvRow IsNot Nothing Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        btnEditItem = CType(e.Row.Cells(GridColDepreciationScheduleCode).FindControl("SelectAction"), LinkButton)
                        btnEditItem.CommandArgument = e.Row.RowIndex.ToString
                        btnEditItem.CommandName = "SelectRecord"
                        btnEditItem.Text = dvRow(DepreciationScd.ColDepreciationScheduleCode).ToString

                        e.Row.Cells(GridColDepreciationScheduleDescription).Text = dvRow(DepreciationScd.ColDepreciationScheduleDescription).ToString
                        e.Row.Cells(GridColDepreciationScheduleActive).Text = dvRow(DepreciationScd.ColDepreciationScheduleActive).ToString
                        e.Row.Cells(GridColDepreciationScheduleId).Text = GetGuidStringFromByteArray(CType(dvRow(DepreciationScd.ColDepreciationScheduleId), Byte()))
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub DepreciationScheduleGridView_RowCommand(source As Object, e As GridViewCommandEventArgs) Handles DepreciationScheduleGridView.RowCommand
            Try
                Select Case e.CommandName
                    Case SelectActionCommand
                        Dim index As Integer = CInt(e.CommandArgument)
                        State.DepreciationScheduleId = New Guid(DepreciationScheduleGridView.Rows(index).Cells(GridColDepreciationScheduleId).Text)
                        callPage(DepreciationScheduleForm.Url, State.DepreciationScheduleId)
                        Return
                End Select
            Catch ex As ThreadAbortException
                ' Do nothing
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_PageIndexChanged(source As Object, e As GridViewPageEventArgs) Handles DepreciationScheduleGridView.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As GridViewSortEventArgs) Handles DepreciationScheduleGridView.Sorting
            Try
                If (State.SortColumn = e.SortExpression) Then
                    State.SortDirection = If(State.SortDirection = WebControls.SortDirection.Ascending, WebControls.SortDirection.Descending, WebControls.SortDirection.Ascending)
                Else
                    State.SortDirection = WebControls.SortDirection.Ascending
                End If
                State.SortColumn = e.SortExpression
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub PageSizeCombo_SelectedIndexChanged(source As Object, e As EventArgs) Handles PageSizeCombo.SelectedIndexChanged
            Try
                State.SelectedPageSize = CType(PageSizeCombo.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(DepreciationScheduleGridView, State.SearchDv.Count(), State.SelectedPageSize)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button Clicks"
        Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
            Try
                ClearSearchResults()
                SetStateSession()
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearchButton_Click(sender As Object, e As EventArgs) Handles ClearSearchButton.Click
            Try
                ClearSearchCriteria()
                ClearSearchResults()
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub NewButton_WRITE_Click(sender As Object, e As EventArgs) Handles NewButton_WRITE.Click
            Try
                Dim guidTempDepreciationScheduleId As Guid = Guid.Empty
                callPage(DepreciationScheduleForm.Url, guidTempDepreciationScheduleId)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "State-Management"
        Private Sub SetStateSession()
            With State
                .CompanyId = CompanyMultipleDrop.SelectedGuid
                .PageIndex = DepreciationScheduleGridView.PageIndex
                .SelectedPageSize = DepreciationScheduleGridView.PageSize
            End With
        End Sub
#End Region
    End Class
End Namespace
