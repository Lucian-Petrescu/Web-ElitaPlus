Imports System.Collections.Generic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Tables

Namespace Tables

    Partial Public Class RoleListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "~/Tables/RoleListForm.aspx"
        Public Const PAGETITLE As String = "ROLES"
        Public Const PAGETAB As String = "ADMIN"
        Public Const SUMMARYTITLE As String = "SEARCH_RESULTS_FOR_ROLE"

        Private Const GRID_COL_ROLE_CODE_IDX As Integer = 0
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 1
        Private Const GRID_COL_IHQ_ONLY_IDX As Integer = 2
        Private Const GRID_COL_ROLE_PROVIDER_IDX As Integer = 3

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"


        Private Const GRID_CTRL_NAME_ROLE_CODE_ID As String = "moRoleId"
        Private Const GRID_CTRL_NAME_ROLE_CODE_LABLE As String = "moCodeLabel"
        Private Const GRID_CTRL_NAME_ROLE_DESCRIPTION_LABEL As String = "moDescriptionLabel"
        Private Const GRID_CTRL_NAME_ROLE_IHQ_ONLY_LABLE As String = "moIHQOnlyLabel"
        Private Const GRID_CTRL_NAME_ROLE_PROVIDER_LABLE As String = "moRoleProviderLabel"

        Private Const GRID_CTRL_NAME_ROLE_CODE_TXT As String = "moCodeText"
        Private Const GRID_CTRL_NAME_ROLE_DESCRIPTION_TXT As String = "moDescriptionText"
        Private Const GRID_CTRL_NAME_ROLE_IHQ_ONLY_DROPDOWN As String = "moIHQOnlyDrop"
        Private Const GRID_CTRL_NAME_ROLE_PROVIDER_DROPDOWN As String = "moRoleProviderDrop"

        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Return Type"
        Public Class RoleReturnType
            Public Code As String
            Public Description As String
            Public RoleId As Guid
        End Class
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False
        Private ReturnType As RoleReturnType = Nothing

        Class MyState
            ' Selected Item Information
            Public SearchDV As Role.RoleSearchDV = Nothing
            Public PageIndex As Integer ' Stores Current Page Index
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE ' Stores Current Page Size
            Public SortExpression As String = "Code" ' Stores Sort Column Name
            Public SortDirection As String = "ASC" ' Stores Sort Direction

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
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If (Not Me.IsReturningFromChild) Then Me.MasterPage.MessageController.Clear()
            Me.Form.DefaultButton = btnSearch.UniqueID
            Try
                If (Not Me.IsPostBack) Then
                    ' Translate Grid Headers
                    Me.TranslateGridHeader(Me.RolesGrid)

                    ' Populate Search Criteria if Returning from Page and Information is Provided
                    If (Me.IsReturningFromChild AndAlso (Not (Me.ReturnType Is Nothing))) Then
                        Me.moRoleCode.Text = Me.ReturnType.Code
                        Me.moRoleDescription.Text = Me.ReturnType.Description
                        PopulateGrid()
                    End If
                End If

                UpdateBreadCrum()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub RoleListForm_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnParameter As Object) Handles Me.PageReturn, Me.PageCall
            Me.MasterPage.MessageController.Clear()
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                If (ReturnParameter Is Nothing) Then
                    Exit Sub
                End If

                Dim returnObj As PageReturnType(Of RoleReturnType) = CType(ReturnParameter, PageReturnType(Of RoleReturnType))
                If returnObj.HasDataChanged Then
                    Me.State.SearchDV = Nothing
                End If
                Me.ReturnType = returnObj.EditingBo
                Select Case returnObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        'If Not returnObj.EditingBo.IsNew Then
                        '    Me.State.SelectedWorkQueueId = returnObj.EditingBo.Id
                        'End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UpdateBreadCrum()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub
#End Region

#Region "Button Event Handlers"
        Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid(True)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
            Try
                moRoleCode.Text = String.Empty
                moRoleDescription.Text = String.Empty
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            Try
                If (Me.moRoleCode.Text = "" AndAlso _
                    Me.moRoleDescription.Text = "") Then
                    Me.MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                    Exit Sub
                End If
                'Reset the Caching on Search Results
                Me.State.SearchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnAdd_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                Dim returnType As RoleReturnType = New RoleReturnType()
                Dim activeOn As Date
                With returnType
                    .Code = moRoleCode.Text
                    .Description = moRoleDescription.Text
                End With

                Me.callPage(RoleDetailForm.URL, returnType)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid(Optional ByVal updatePageIndex As Boolean = False)
            Dim recCount As Integer
            Try

                Me.RolesGrid.PageSize = Me.State.PageSize

                Dim result As Role.RoleSearchDV
                If Me.State.SearchDV Is Nothing Then

                    result = Role.getList(moRoleCode.Text, moRoleDescription.Text)
                    Me.State.SearchDV = result
                Else
                    result = Me.State.SearchDV
                End If

                recCount = result.Count

                If (updatePageIndex) Then
                    Me.State.PageIndex = NewCurrentPageIndex(RolesGrid, recCount, State.PageSize)
                    Me.RolesGrid.PageIndex = Me.State.PageIndex
                End If

                Me.State.SearchDV.Sort = Me.State.SortExpression
                Me.HighLightSortColumn(Me.RolesGrid, Me.State.SortExpression + " " + Me.State.SortDirection, True)
                Me.RolesGrid.DataSource = result
                Me.RolesGrid.DataBind()


                If (Me.RolesGrid.Rows.Count = 0) Then
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
                    ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                Else
                    ControlMgr.SetVisibleControl(Me, moSearchResults, True)
                    lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Grid Events"

        Private Sub Grid_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles RolesGrid.Sorting
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

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RolesGrid.PageIndexChanged
            Try
                Me.State.PageIndex = RolesGrid.PageIndex
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RolesGrid.PageIndexChanging
            Try
                RolesGrid.PageIndex = e.NewPageIndex
                State.PageIndex = RolesGrid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles RolesGrid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles RolesGrid.RowCommand
            Try
                Dim returnType As RoleReturnType = New RoleReturnType()
                Dim activeOn As Date
                With returnType
                    .Code = moRoleCode.Text
                    .Description = moRoleDescription.Text
                End With

                Select Case e.CommandName.ToString().ToUpper()
                    Case "SELECTACTION"
                        returnType.RoleId = New Guid(e.CommandArgument.ToString())
                        Me.callPage(RoleDetailForm.URL, returnType)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class
End Namespace