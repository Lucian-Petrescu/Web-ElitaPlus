Namespace Reports

    Partial Public Class ReportCeConfigListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"

        Public Const PAGETITLE As String = "REPORT_CONFIG"
        Public Const PAGETAB As String = "REPORTS"

        Public Const GRID_COL_EDIT As Integer = 0
        Public Const GRID_COL_REPORT_CONFIG_ID As Integer = 1
        Public Const GRID_COL_COMPANY As Integer = 2
        Public Const GRID_COL_REPORT As Integer = 3
        Public Const GRID_COL_REPORT_CE_NAME As Integer = 4
        
        Public Const GRID_TOTAL_COLUMNS As Integer = 5

#End Region

#Region "Variables"

        Private IsReturningFromChild As Boolean = False

#End Region

#Region "Page State"

#Region "MyState"

        Class MyState
            Public searchDV As ReportConfig.ReportConfigSearchDV = Nothing
           Public PageIndex As Integer = 0
           Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public moReportConfigId As Guid = Guid.Empty
            Public moReport As String = String.Empty
            Public moReportCeName As String = String.Empty
            Public bnoRow As Boolean = False
        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#Region "Page Return"

        Public Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                If retObj IsNot Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.moReportConfigId = retObj.moReportConfigId
                        Case Else
                            State.moReportConfigId = Guid.Empty
                    End Select
                    Grid.PageIndex = State.PageIndex
                    Grid.PageSize = State.PageSize
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moReportConfigId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oReportConfigId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moReportConfigId = oReportConfigId
                Me.BoChanged = boChanged
            End Sub
        End Class

#End Region

#End Region

#Region "Handlers"

#Region "Handler-Init"

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            ErrControllerMaster.Clear_Hide()
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    SortDirection = ReportConfig.ReportConfigSearchDV.COL_COMPANY
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)

                    SetGridItemStyleColor(Grid)
                    If IsReturningFromChild Then
                        PopulateAll()
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers-Grid"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try

                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If dvRow IsNot Nothing And Not State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        '  e.Row.Cells(Me.GRID_COL_REPORT_CONFIG_ID).Text = GetGuidStringFromByteArray(CType(dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_USER_ID), Byte()))
                        e.Row.Cells(GRID_COL_COMPANY).Text = dvRow(ReportConfig.ReportConfigSearchDV.COL_COMPANY).ToString
                        e.Row.Cells(GRID_COL_REPORT).Text = dvRow(ReportConfig.ReportConfigSearchDV.COL_REPORT).ToString
                        e.Row.Cells(GRID_COL_REPORT_CE_NAME).Text = dvRow(ReportConfig.ReportConfigSearchDV.COL_REPORT_CE_NAME).ToString
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageSize = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = e.NewPageIndex
                PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

            Try
                If e.CommandName = "SelectUser" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_REPORT_CONFIG_ID).FindControl("moReportConfigId"), Label)
                    State.moReportConfigId = New Guid(lblCtrl.Text)
                    SetSession()
                    callPage(ReportCeConfigForm.URL, State.moReportConfigId)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
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

                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Protected Sub moBtnSearch_Click(sender As Object, e As EventArgs) Handles moBtnSearch.Click
            Try
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub moBtnClear_Click(sender As Object, e As EventArgs) Handles moBtnClear.Click
            Try
                ClearAll()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnNew_WRITE_Click(sender As Object, e As EventArgs) Handles btnNew_WRITE.Click
            Try
                State.moReportConfigId = Guid.Empty
                SetSession()
                callPage(ReportCeConfigForm.URL)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearAll()
            moReportText.Text = String.Empty
            moReportCeText.Text = String.Empty
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateTexts()
            moReportText.Text = State.moReport
            moReportCeText.Text = State.moReportCeName
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Try
                Dim oDataView As DataView
               If (State.searchDV Is Nothing) Then
                    State.searchDV = ReportConfig.getList(moReportText.Text, moReportCeText.Text)
                End If
                State.searchDV.Sort = SortDirection
                Grid.PageSize = State.PageSize
                If State.searchDV.Count = 0 Then
                    State.bnoRow = True
                    Dim dv As ReportConfig.ReportConfigSearchDV = State.searchDV.AddNewRowToEmptyDV
                    SetPageAndSelectedIndexFromGuid(dv, State.moReportConfigId, Grid, State.PageIndex)
                    Grid.DataSource = dv
                Else
                    State.bnoRow = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.moReportConfigId, Grid, State.PageIndex)
                    Grid.DataSource = State.searchDV
                End If

                State.PageIndex = Grid.PageIndex
               HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Session("recCount") = State.searchDV.Count

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub


        Private Sub PopulateAll()
            PopulateTexts()
            PopulateGrid(POPULATE_ACTION_SAVE)
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With State
                .moReport = moReportText.Text
                .moReportCeName = moReportCeText.Text
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
            End With
        End Sub

#End Region


    End Class

End Namespace