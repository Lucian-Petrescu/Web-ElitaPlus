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
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.ErrControllerMaster.Clear_Hide()
            Me.Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    TranslateGridHeader(Grid)
                    Me.SetDefaultButton(Me.SearchEnvironmentTextBox, btnSearch)
                    Me.SetDefaultButton(Me.SearchDescriptionTextBox, btnSearch)
                    GetStateProperties()
                    If Not Me.IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Else
                        ' It is returning from detail
                        Me.PopulateGrid()
                    End If
                    SetFocus(Me.SearchEnvironmentTextBox)
                End If
                Me.DisplayProgressBarOnClick(Me.btnSearch, "Loading_Servers")
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As ServerForm.ReturnType = CType(ReturnPar, ServerForm.ReturnType)
                If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                    Me.State.searchDV = Nothing
                End If
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.selectedServerId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Button event handlers"
        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            Try
                Me.SetStateProperties()
                Me.State.PageIndex = 0
                Me.State.selectedServerId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchClick = True
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try
                Me.SearchEnvironmentTextBox.Text = String.Empty
                Me.SearchDescriptionTextBox.Text = String.Empty
               
                'Update Page State
                With Me.State
                    .moEnvironment = Me.SearchEnvironmentTextBox.Text
                    .moDescription = Me.SearchDescriptionTextBox.Text
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
            Try
                Me.callPage(ServerForm.URL)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Helper functions"
        Private Sub GetStateProperties()
            Try
                Me.SearchEnvironmentTextBox.Text = Me.State.moEnvironment
                Me.SearchDescriptionTextBox.Text = Me.State.moDescription
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub SetStateProperties()
            
            Try
                If Me.State Is Nothing Then
                    Me.RestoreState(New MyState)
                End If
                Me.State.moEnvironment = Me.SearchEnvironmentTextBox.Text
                Me.State.moDescription = Me.SearchDescriptionTextBox.Text
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                Dim oDataView As DataView
                Dim sortBy As String = "ENVIRONMENT"
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = Servers.GetServersList(State.moEnvironment, Me.State.moDescription)
                    If Me.State.searchClick Then
                        Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                        Me.State.searchClick = False
                    End If
                End If
                
                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                Grid.PageSize = State.PageSize

                If State.searchDV.Count = 0 Then
                    '   Me.State.bnoRow = True
                    Dim dv As Servers.SearchDV = State.searchDV.AddNewRowToEmptyDV
                    SetPageAndSelectedIndexFromGuid(dv, Me.State.selectedServerId, Me.Grid, Me.State.PageIndex)
                    Me.Grid.DataSource = dv
                Else
                    '  Me.State.bnoRow = False
                    SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedServerId, Me.Grid, Me.State.PageIndex)
                    Me.Grid.DataSource = Me.State.searchDV
                End If

                '' ''Grid.PageSize = State.PageSize

                Me.State.PageIndex = Me.Grid.PageIndex
                Me.Grid.AllowSorting = False
                '   Me.Grid.DataSource = Me.State.searchDV
                Me.Grid.DataBind()

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
                HighLightGridViewSortColumn(Grid, sortBy)
                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Session("recCount") = Me.State.searchDV.Count

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
                'If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Grid related"

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = Grid.PageIndex
                Me.State.selectedServerId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Sub RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = "Select" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_SERVER_ID_IDX).FindControl(Me.GRID_CTRL_SERVER_ID), Label)
                    Me.State.selectedServerId = New Guid(lblCtrl.Text)
                    Me.callPage(ServerForm.URL, Me.State.selectedServerId)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub



        Public Sub RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                BaseItemBound(sender, e)
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region
        
    End Class
End Namespace