﻿Namespace Security

    Partial Public Class FtpSiteListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"

        Public Const PAGETITLE As String = "FTP_SITE"
        Public Const PAGETAB As String = "ADMIN"

        Public Const GRID_COL_EDIT As Integer = 0
        Public Const GRID_COL_FTP_SITE_ID As Integer = 1
        Public Const GRID_COL_CODE As Integer = 2
        Public Const GRID_COL_DESCRIPTION As Integer = 3
        Public Const GRID_COL_HOST As Integer = 4

        Public Const GRID_TOTAL_COLUMNS As Integer = 5

#End Region

#Region "Variables"

        Private IsReturningFromChild As Boolean = False

#End Region

#Region "Page State"

#Region "MyState"

        Class MyState
            Public searchDV As FtpSite.FtpSiteSearchDV = Nothing
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public moFtpSiteId As Guid = Guid.Empty
            Public moCode As String = String.Empty
            Public moDescription As String = String.Empty
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

        Public Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                If Not retObj Is Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.moFtpSiteId = retObj.moFtpSiteId
                        Case Else
                            Me.State.moFtpSiteId = Guid.Empty
                    End Select
                    Me.Grid.PageIndex = Me.State.PageIndex
                    Me.Grid.PageSize = Me.State.PageSize
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    Me.Grid.PageSize = Me.State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moFtpSiteId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal oFtpSiteId As Guid, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.moFtpSiteId = oFtpSiteId
                Me.BoChanged = boChanged
            End Sub
        End Class

#End Region

#End Region

#Region "Handlers"


#Region "Handler-Init"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.ErrControllerMaster.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.SetDefaultButton(Me.moCodeText, moBtnSearch)
                    Me.SetDefaultButton(Me.moDescriptionText, moBtnSearch)
                    Me.SortDirection = FtpSite.FtpSiteSearchDV.COL_CODE
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)

                    Me.SetGridItemStyleColor(Me.Grid)
                    If Me.IsReturningFromChild Then
                        PopulateAll()
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

#End Region


#Region "Handlers-Grid"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try

                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        ' e.Row.Cells(Me.GRID_COL_FTP_SITE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.COL_USER_ID), Byte()))
                        e.Row.Cells(Me.GRID_COL_CODE).Text = dvRow(FtpSite.FtpSiteSearchDV.COL_CODE).ToString
                        e.Row.Cells(Me.GRID_COL_DESCRIPTION).Text = dvRow(FtpSite.FtpSiteSearchDV.COL_DESCRIPTION).ToString
                        e.Row.Cells(Me.GRID_COL_HOST).Text = dvRow(FtpSite.FtpSiteSearchDV.COL_HOST).ToString
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Me.Grid.PageIndex = NewCurrentPageIndex(Me.Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageSize = Me.Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.Grid.PageIndex = e.NewPageIndex
                Me.State.PageIndex = e.NewPageIndex
                Me.State.moFtpSiteId = Guid.Empty
                PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

            Try
                If e.CommandName = "SelectUser" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_FTP_SITE_ID).FindControl("moFtpSiteId"), Label)
                    Me.State.moFtpSiteId = New Guid(lblCtrl.Text)
                    SetSession()
                    Me.callPage(FtpSiteForm.URL, Me.State.moFtpSiteId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

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

                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Protected Sub moBtnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moBtnSearch.Click
            Try
                Me.Grid.PageIndex = Me.NO_PAGE_INDEX
                Me.Grid.DataMember = Nothing
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub moBtnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moBtnClear.Click
            Try
                ClearAll()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.State.moFtpSiteId = Guid.Empty
                SetSession()
                Me.callPage(FtpSiteForm.URL)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region
#End Region


#Region "Clear"

        Private Sub ClearAll()
            moCodeText.Text = String.Empty
            moDescriptionText.Text = String.Empty
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateTexts()
            moCodeText.Text = Me.State.moCode
            moDescriptionText.Text = Me.State.moDescription
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Try
                Dim oDataView As DataView
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = FtpSite.getList(moCodeText.Text, moDescriptionText.Text)
                End If
                Me.State.searchDV.Sort = Me.SortDirection
                Grid.PageSize = State.PageSize
                If State.searchDV.Count = 0 Then
                    Me.State.bnoRow = True
                    Dim dv As FtpSite.FtpSiteSearchDV = State.searchDV.AddNewRowToEmptyDV
                    SetPageAndSelectedIndexFromGuid(dv, Me.State.moFtpSiteId, Me.Grid, Me.State.PageIndex)
                    Me.Grid.DataSource = dv
                Else
                    Me.State.bnoRow = False
                    SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.moFtpSiteId, Me.Grid, Me.State.PageIndex)
                    Me.Grid.DataSource = Me.State.searchDV
                End If

                Me.State.PageIndex = Me.Grid.PageIndex
                HighLightSortColumn(Me.Grid, Me.SortDirection)
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Session("recCount") = Me.State.searchDV.Count

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

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


        Private Sub PopulateAll()
            PopulateTexts()
            Me.PopulateGrid(Me.POPULATE_ACTION_SAVE)
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With Me.State
                .moCode = moCodeText.Text
                .moDescription = moDescriptionText.Text
                .PageIndex = Me.Grid.PageIndex
                .PageSize = Me.Grid.PageSize
            End With
        End Sub

#End Region


    End Class

End Namespace