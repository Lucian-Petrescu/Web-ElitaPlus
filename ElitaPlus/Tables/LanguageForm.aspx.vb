Namespace Tables
    Partial Public Class LanguageForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "Tables/InvoiceControlListForm.aspx"
        Public Const PAGETITLE As String = "LANGUAGE"
        Public Const PAGETAB As String = "ADMIN"

        Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

        Private Const GRID_COL_EDIT_IDX As Integer = 0
        Private Const GRID_COL_DELETE_IDX As Integer = 1
        Private Const GRID_COL_LANGUAGE_ID_IDX As Integer = 2
        Private Const GRID_COL_LANGUAGE_CODE_IDX As Integer = 3
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 4
        Private Const GRID_COL_CULTURE_IDX As Integer = 5
        Private Const GRID_COL_TERRITORY_IDX As Integer = 6


        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"


        Private Const GRID_CTRL_NAME_LANGUAGE_CODE_ID As String = "moLanguageId"
        Private Const GRID_CTRL_NAME_LANGUAGE_CODE_LABLE As String = "moCodeLabel"
        Private Const GRID_CTRL_NAME_LANGUAGE_DESCRIPTION_LABEL As String = "moDescriptionLabel"
        Private Const GRID_CTRL_NAME_LANGUAGE_CODE_TXT As String = "moCodeText"
        Private Const GRID_CTRL_NAME_LANGUAGE_DESCRIPTION_TXT As String = "moDescriptionText"
        Private Const GRID_CTRL_NAME_LANGUAGE_CULTURE_TXT As String = "moCultureText"
        Private Const GRID_CTRL_NAME_LANGUAGE_TERRITORY_TXT As String = "moTerritoryText"


        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Public Const MSG_NONE_OR_MORE_THAN_ONE_RECORD_FOUND As String = "NONE_OR_MORE_THAN_ONE_RECORD_FOUND"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        'Private MSGTYPELIST As DataView
#End Region

#Region "Page State"
        ' This class keeps the Language state for the search page.
        Class MyState
            Public MyBO As Language
            Public LanguageID As Guid
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public searchDV As Language.LanguageSearchDV = Nothing
            Public HasDataChanged As Boolean
            Public IsGridAddNew As Boolean = False
            Public IsEditMode As Boolean = False
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public SortExpression As String = Language.LanguageSearchDV.COL_NAME_DESCRIPTION
            Public searchLanguageDescription As String = ""
            Public searchLanguageCode As String = ""
            Public SearchCulture As String = ""
            Public SearchTerritory As String = ""
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_

            Public Sub New()

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
#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.ErrControllerMaster.Clear_Hide()
            Me.Form.DefaultButton = Me.btnSearch.UniqueID
            Try
                If Not Me.IsPostBack Then
                    Me.SortDirection = Language.LanguageSearchDV.COL_NAME_CODE
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    SetControlState()
                    Me.State.PageIndex = 0
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                Else
                    BindBoPropertiesToGridHeaders()
                    CheckIfComingFromDeleteConfirm()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

#End Region

#Region "Grid Handler"

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = Grid.PageIndex
                    Me.State.LanguageID = Guid.Empty
                    Me.PopulateGrid()
                End If
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

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.LanguageID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_LANGUAGE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_CODE_ID), Label).Text)
                    Me.State.MyBO = New Language(Me.State.LanguageID)

                    Me.PopulateGrid()

                    Me.State.PageIndex = Grid.PageIndex

                    'Me.SetGridControls(Me.Grid, False)

                    'Set focus on the Code TextBox for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.GRID_COL_LANGUAGE_CODE_IDX, Me.GRID_CTRL_NAME_LANGUAGE_CODE_TXT, index)

                    Me.PopulateFormFromBO()

                    Me.SetControlState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.LanguageID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_LANGUAGE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_CODE_ID), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        'Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Not State.searchDV.Count > 0 Then

                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(Me.GRID_COL_LANGUAGE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_CODE_ID), Label).Text = GetGuidStringFromByteArray(CType(dvRow(Language.LanguageSearchDV.COL_NAME_LANGUAGE_ID), Byte()))

                        If (Me.State.IsEditMode = True _
                                AndAlso Me.State.LanguageID.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(Language.LanguageSearchDV.COL_NAME_LANGUAGE_ID), Byte())))) Then
                            CType(e.Row.Cells(Me.GRID_COL_LANGUAGE_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_CODE_TXT), TextBox).Text = dvRow(Language.LanguageSearchDV.COL_NAME_DESCRIPTION).ToString
                            CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_DESCRIPTION_TXT), TextBox).Text = dvRow(Language.LanguageSearchDV.COL_NAME_CODE).ToString
                            CType(e.Row.Cells(Me.GRID_COL_CULTURE_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_DESCRIPTION_TXT), TextBox).Text = dvRow(Language.LanguageSearchDV.COL_NAME_CULTURE).ToString
                            CType(e.Row.Cells(Me.GRID_COL_TERRITORY_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_DESCRIPTION_TXT), TextBox).Text = dvRow(Language.LanguageSearchDV.COL_NAME_TERRITORY).ToString

                        Else
                            CType(e.Row.Cells(Me.GRID_COL_LANGUAGE_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_CODE_LABLE), Label).Text = dvRow(Language.LanguageSearchDV.COL_NAME_CODE).ToString
                            CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_DESCRIPTION_LABEL), Label).Text = dvRow(Language.LanguageSearchDV.COL_NAME_DESCRIPTION).ToString
                            CType(e.Row.Cells(Me.GRID_COL_CULTURE_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_DESCRIPTION_LABEL), Label).Text = dvRow(Language.LanguageSearchDV.COL_NAME_CULTURE).ToString
                            CType(e.Row.Cells(Me.GRID_COL_TERRITORY_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_DESCRIPTION_LABEL), Label).Text = dvRow(Language.LanguageSearchDV.COL_NAME_TERRITORY).ToString
                        End If
                        'e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub Grid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

        Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region

#Region "Button click Handler"
        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try
                Me.SearchCodeTextBox.Text = String.Empty
                Me.SearchDescriptionTextBox.Text = String.Empty
                Me.SearchCultureTextBox.Text = String.Empty
                Me.SearchTerritoryTextBox.Text = String.Empty
                Me.State.searchLanguageCode = String.Empty
                Me.State.searchLanguageDescription = String.Empty
                Me.State.SearchCulture = String.Empty
                Me.State.SearchTerritory = String.Empty
                Me.State.IsGridAddNew = False

                Grid.EditIndex = Me.NO_ITEM_SELECTED_INDEX
                With State
                    .LanguageID = Guid.Empty
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            Try
                With State
                    .PageIndex = 0
                    .LanguageID = Guid.Empty
                    .IsGridVisible = True
                    .searchDV = Nothing
                    .HasDataChanged = False
                    .IsGridAddNew = False

                    .searchLanguageCode = Me.SearchCodeTextBox.Text.Trim
                    .searchLanguageDescription = SearchDescriptionTextBox.Text.Trim
                    .SearchCulture = SearchCultureTextBox.Text.Trim
                    .SearchTerritory = SearchTerritoryTextBox.Text.Trim
                End With
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.IsGridAddNew = True
                AddNew()
                'Me.AddNewRiskType()
                Me.SetControlState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click

            Try
                PopulateBOFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.IsGridAddNew = False
                    Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                    Me.State.searchDV = Nothing
                    Me.ReturnFromEditing()
                Else
                    Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Try
                With State
                    If .IsGridAddNew Then
                        RemoveNewRowFromSearchDV()
                        .IsGridAddNew = False
                        'Grid.PageIndex = .PageIndex
                    End If
                    .LanguageID = Guid.Empty
                    .IsEditMode = False
                End With
                'Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                Me.State.searchDV = Nothing
                PopulateGrid()
                SetControlState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Control"

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Me.Grid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            SetControlState()

        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            'Me.HiddenDeletePromptResponse.Value = ""
        End Sub


        Private Sub DoDelete()
            'Do the delete here

            'Save the LanguageId in the Session

            Dim LanguageBO As Language = New Language(Me.State.LanguageID)

            LanguageBO.Delete()

            'Call the Save() method in the Language Business Object here

            LanguageBO.Save()

            'Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            Me.State.IsEditMode = False
            SetControlState()
        End Sub
#End Region

#Region "Helper functions"

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Description", Me.Grid.Columns(Me.GRID_COL_DESCRIPTION_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Code", Me.Grid.Columns(Me.GRID_COL_LANGUAGE_CODE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CultureCode", Me.Grid.Columns(Me.GRID_COL_CULTURE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Territory", Me.Grid.Columns(Me.GRID_COL_TERRITORY_IDX))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub PopulateFormFromBO()

            Dim gridRowIdx As Integer = Me.Grid.EditIndex
            Try
                With Me.State.MyBO
                    If (Not .Code Is Nothing) Then
                        CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_LANGUAGE_CODE_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_CODE_TXT), TextBox).Text = .Code
                    End If
                    If (Not .Description Is Nothing) Then
                        CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_DESCRIPTION_TXT), TextBox).Text = .Description
                    End If

                    If (Not .CultureCode Is Nothing) Then
                        CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_CULTURE_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_CULTURE_TXT), TextBox).Text = .CultureCode
                    End If

                    If (Not .Territory Is Nothing) Then
                        CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_TERRITORY_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_TERRITORY_TXT), TextBox).Text = .Territory
                    End If

                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_LANGUAGE_ID_IDX).FindControl(Me.GRID_CTRL_NAME_LANGUAGE_CODE_ID), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Function PopulateBOFromForm() As Boolean

            With Me.State.MyBO
                .Code = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_LANGUAGE_CODE_IDX).FindControl(GRID_CTRL_NAME_LANGUAGE_CODE_TXT), TextBox).Text.Trim.ToUpper
                .Description = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_CTRL_NAME_LANGUAGE_DESCRIPTION_TXT), TextBox).Text.Trim
                .CultureCode = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_CULTURE_IDX).FindControl(GRID_CTRL_NAME_LANGUAGE_CULTURE_TXT), TextBox).Text.Trim
                .Territory = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_TERRITORY_IDX).FindControl(GRID_CTRL_NAME_LANGUAGE_TERRITORY_TXT), TextBox).Text.Trim.ToUpper
                If Me.State.IsGridAddNew Then
                    .ActiveFlag = "Y"
                End If
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Function

        Public Sub PopulateGrid()

            Dim dv As DataView
            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetDV()
                End If
                Me.State.searchDV.Sort = Me.SortDirection
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.LanguageID, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.LanguageID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
                End If
                'If Me.State.searchDV.Count > 0 Then
                Me.Grid.AutoGenerateColumns = False
                Me.Grid.Columns(Me.GRID_COL_LANGUAGE_CODE_IDX).SortExpression = Language.LanguageSearchDV.COL_NAME_CODE
                Me.Grid.Columns(Me.GRID_COL_DESCRIPTION_IDX).SortExpression = Language.LanguageSearchDV.COL_NAME_DESCRIPTION
                Me.Grid.Columns(Me.GRID_COL_CULTURE_IDX).SortExpression = Language.LanguageSearchDV.COL_NAME_CULTURE
                Me.Grid.Columns(Me.GRID_COL_TERRITORY_IDX).SortExpression = Language.LanguageSearchDV.COL_NAME_TERRITORY
                SortAndBindGrid()
                'End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Function GetDV() As Language.LanguageSearchDV

            Dim dv As DataView

            Me.State.searchDV = GetLanguageGridDataView()
            Me.State.searchDV.Sort = Grid.DataMember()
            Grid.DataSource = Me.State.searchDV

            Return (Me.State.searchDV)

        End Function

        Private Function GetLanguageGridDataView() As Language.LanguageSearchDV

            With State
                Return Language.getList(.searchLanguageCode, .searchLanguageDescription, .SearchCulture, .SearchTerritory)
            End With

        End Function

        Private Sub SortAndBindGrid()

            Me.TranslateGridControls(Grid)

            If (Me.State.searchDV.Count = 0) Then
                Me.State.searchDV = Nothing
                Me.State.MyBO = New Language
                State.MyBO.AddNewRowToLanguageSearchDV(Me.State.searchDV, Me.State.MyBO)
                Me.Grid.DataSource = Me.State.searchDV
                Me.Grid.DataBind()
                Me.Grid.Rows(0).Visible = False
                Me.State.IsGridAddNew = True
            Else
                'Me.State.bnoRow = False
                Me.Grid.Enabled = True
                Me.Grid.DataSource = Me.State.searchDV
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()
            End If
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True


            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.Grid.Visible Then
                If (Me.State.IsGridAddNew) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

        End Sub

        Private Sub SetControlState()

            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, btnNew, False)
                ControlMgr.SetVisibleControl(Me, btnCancel, True)
                ControlMgr.SetVisibleControl(Me, btnSave, True)
                ControlMgr.SetEnableControl(Me, btnSearch, False)
                ControlMgr.SetEnableControl(Me, btnClearSearch, False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnNew, True)
                ControlMgr.SetVisibleControl(Me, btnCancel, False)
                ControlMgr.SetVisibleControl(Me, btnSave, False)
                ControlMgr.SetEnableControl(Me, btnSearch, True)
                ControlMgr.SetEnableControl(Me, btnClearSearch, True)
                Me.MenuEnabled = True
                If Not (Me.cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
                End If
            End If

        End Sub

        Private Sub AddNew()

            Me.State.MyBO = New Language
            Me.State.LanguageID = Me.State.MyBO.Id
            State.MyBO.AddNewRowToLanguageSearchDV(Me.State.searchDV, Me.State.MyBO)
            State.IsGridAddNew = True
            'SetGridControls(Me.Grid, False)
            PopulateGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.LanguageID, Me.Grid, _
                                               Me.State.PageIndex, Me.State.IsEditMode)
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            SetGridControls(Me.Grid, False)
        End Sub

        Private Function GetRowIndexFromSearchDVByID(ByVal LanguageID As Guid) As Integer
            Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
            With State
                If Not .searchDV Is Nothing Then
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, LanguageID)
                End If
            End With
            Return rowind
        End Function
        Private Sub RemoveNewRowFromSearchDV()
            Dim rowind As Integer = GetRowIndexFromSearchDVByID(State.LanguageID)
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub
#End Region

    End Class
End Namespace
