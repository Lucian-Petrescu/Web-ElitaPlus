Namespace Security

    Partial Public Class ServerListForm
        Inherits ElitaPlusSearchPage
#Region "Constants"
        Public Const URL As String = "Security/ServerListForm.aspx"
        Public Const PAGETITLE As String = "SERVERS"
        Public Const PAGETAB As String = "ADMIN"

        Public Const GRID_COL_SERVER_ID_IDX As Integer = 1
        Public Const GRID_CTRL_SERVER_ID As String = "lblServerID"
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public moEnvironment As String = Nothing
            Public moDescription As String = Nothing
            Public PageIndex As Integer = 0
            Public PageSize As Integer = 10
            Public searchDV As Servers.SearchDV = Nothing
            Public searchClick As Boolean = False
            Public serverFoundMSG As String
            Public selectedServerId As Guid = Guid.Empty
            Public IsGridVisible As Boolean = False

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

#Region "Page event handlers"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            ErrControllerMaster.Clear_Hide()
            Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    TranslateGridHeader(Grid)
                    SetDefaultButton(SearchEnvironmentTextBox, btnSearch)
                    SetDefaultButton(SearchDescriptionTextBox, btnSearch)
                    GetStateProperties()
                    If Not IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Else
                        ' It is returning from detail
                        PopulateGrid()
                    End If
                    SetFocus(SearchEnvironmentTextBox)
                End If
                DisplayProgressBarOnClick(btnSearch, "Loading_Servers")
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As ServerForm.ReturnType = CType(ReturnPar, ServerForm.ReturnType)
                If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                    State.searchDV = Nothing
                End If
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.selectedServerId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Button event handlers"
        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                SetStateProperties()
                State.PageIndex = 0
                State.selectedServerId = Guid.Empty
                State.IsGridVisible = True
                State.searchClick = True
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try
                SearchEnvironmentTextBox.Text = String.Empty
                SearchDescriptionTextBox.Text = String.Empty
               
                'Update Page State
                With State
                    .moEnvironment = SearchEnvironmentTextBox.Text
                    .moDescription = SearchDescriptionTextBox.Text
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
            Try
                callPage(ServerForm.URL)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Helper functions"
        Private Sub GetStateProperties()
            Try
                SearchEnvironmentTextBox.Text = State.moEnvironment
                SearchDescriptionTextBox.Text = State.moDescription
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub SetStateProperties()
            
            Try
                If State Is Nothing Then
                    RestoreState(New MyState)
                End If
                State.moEnvironment = SearchEnvironmentTextBox.Text
                State.moDescription = SearchDescriptionTextBox.Text
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                Dim oDataView As DataView
                Dim sortBy As String = "ENVIRONMENT"
                If (State.searchDV Is Nothing) Then
                    State.searchDV = Servers.GetServersList(State.moEnvironment, State.moDescription)
                    If State.searchClick Then
                        ValidSearchResultCount(State.searchDV.Count, True)
                        State.searchClick = False
                    End If
                End If
                
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                Grid.PageSize = State.PageSize

                If State.searchDV.Count = 0 Then
                    '   Me.State.bnoRow = True
                    Dim dv As Servers.SearchDV = State.searchDV.AddNewRowToEmptyDV
                    SetPageAndSelectedIndexFromGuid(dv, State.selectedServerId, Grid, State.PageIndex)
                    Grid.DataSource = dv
                Else
                    '  Me.State.bnoRow = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedServerId, Grid, State.PageIndex)
                    Grid.DataSource = State.searchDV
                End If

                '' ''Grid.PageSize = State.PageSize

                State.PageIndex = Grid.PageIndex
                Grid.AllowSorting = False
                '   Me.Grid.DataSource = Me.State.searchDV
                Grid.DataBind()

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
                HighLightGridViewSortColumn(Grid, sortBy)
                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Session("recCount") = State.searchDV.Count

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
                'If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

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

#End Region

#Region "Grid related"

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.selectedServerId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Public Sub RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = "Select" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_SERVER_ID_IDX).FindControl(GRID_CTRL_SERVER_ID), Label)
                    State.selectedServerId = New Guid(lblCtrl.Text)
                    callPage(ServerForm.URL, State.selectedServerId)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub



        Public Sub RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                BaseItemBound(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region
        
    End Class
End Namespace