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
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            If (Not IsReturningFromChild) Then MasterPage.MessageController.Clear()
            Form.DefaultButton = btnSearch.UniqueID
            Try
                If (Not IsPostBack) Then
                    ' Translate Grid Headers
                    TranslateGridHeader(RolesGrid)

                    ' Populate Search Criteria if Returning from Page and Information is Provided
                    If (IsReturningFromChild AndAlso (Not (ReturnType Is Nothing))) Then
                        moRoleCode.Text = ReturnType.Code
                        moRoleDescription.Text = ReturnType.Description
                        PopulateGrid()
                    End If
                End If

                UpdateBreadCrum()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub RoleListForm_PageReturn(ReturnFromUrl As String, ReturnParameter As Object) Handles Me.PageReturn, Me.PageCall
            MasterPage.MessageController.Clear()
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                If (ReturnParameter Is Nothing) Then
                    Exit Sub
                End If

                Dim returnObj As PageReturnType(Of RoleReturnType) = CType(ReturnParameter, PageReturnType(Of RoleReturnType))
                If returnObj.HasDataChanged Then
                    State.SearchDV = Nothing
                End If
                ReturnType = returnObj.EditingBo
                Select Case returnObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        'If Not returnObj.EditingBo.IsNew Then
                        '    Me.State.SelectedWorkQueueId = returnObj.EditingBo.Id
                        'End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UpdateBreadCrum()
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub
#End Region

#Region "Button Event Handlers"
        Protected Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid(True)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
            Try
                moRoleCode.Text = String.Empty
                moRoleDescription.Text = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                If (moRoleCode.Text = "" AndAlso _
                    moRoleDescription.Text = "") Then
                    MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                    Exit Sub
                End If
                'Reset the Caching on Search Results
                State.SearchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnAdd_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                Dim returnType As RoleReturnType = New RoleReturnType()
                Dim activeOn As Date
                With returnType
                    .Code = moRoleCode.Text
                    .Description = moRoleDescription.Text
                End With

                callPage(RoleDetailForm.URL, returnType)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid(Optional ByVal updatePageIndex As Boolean = False)
            Dim recCount As Integer
            Try

                RolesGrid.PageSize = State.PageSize

                Dim result As Role.RoleSearchDV
                If State.SearchDV Is Nothing Then

                    result = Role.getList(moRoleCode.Text, moRoleDescription.Text)
                    State.SearchDV = result
                Else
                    result = State.SearchDV
                End If

                recCount = result.Count

                If (updatePageIndex) Then
                    State.PageIndex = NewCurrentPageIndex(RolesGrid, recCount, State.PageSize)
                    RolesGrid.PageIndex = State.PageIndex
                End If

                State.SearchDV.Sort = State.SortExpression
                HighLightSortColumn(RolesGrid, State.SortExpression + " " + State.SortDirection, True)
                RolesGrid.DataSource = result
                RolesGrid.DataBind()


                If (RolesGrid.Rows.Count = 0) Then
                    MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
                    ControlMgr.SetVisibleControl(Me, moSearchResults, False)
                Else
                    ControlMgr.SetVisibleControl(Me, moSearchResults, True)
                    lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Grid Events"

        Private Sub Grid_Sorting(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles RolesGrid.Sorting
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

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles RolesGrid.PageIndexChanged
            Try
                State.PageIndex = RolesGrid.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RolesGrid.PageIndexChanging
            Try
                RolesGrid.PageIndex = e.NewPageIndex
                State.PageIndex = RolesGrid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs) Handles RolesGrid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles RolesGrid.RowCommand
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
                        callPage(RoleDetailForm.URL, returnType)
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

    End Class
End Namespace