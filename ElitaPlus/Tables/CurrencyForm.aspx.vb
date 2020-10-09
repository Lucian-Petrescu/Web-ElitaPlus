Namespace Tables

    Partial Public Class CurrencyForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const URL As String = "Tables/InvoiceControlListForm.aspx"
        Public Const PAGETITLE As String = "CURRENCY"
        Public Const PAGETAB As String = "ADMIN"

        Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

        Private Const GRID_COL_EDIT_IDX As Integer = 0
        Private Const GRID_COL_DELETE_IDX As Integer = 1
        Private Const GRID_COL_CURRENCY_ID_IDX As Integer = 2
        Private Const GRID_COL_CURRENCY_CODE_IDX As Integer = 3
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 4
        Private Const GRID_COL_NOTATION_IDX As Integer = 5
        Private Const GRID_COL_ISO_CODE_IDX As Integer = 6


        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"


        Private Const GRID_CTRL_NAME_CURRENCY_CODE_ID As String = "moCurrencyId"
        Private Const GRID_CTRL_NAME_CURRENCY_CODE_LABLE As String = "moCodeLabel"
        Private Const GRID_CTRL_NAME_CURRENCY_DESCRIPTION_LABEL As String = "moDescriptionLabel"
        Private Const GRID_CTRL_NAME_CURRENCY_CODE_TXT As String = "moCodeText"
        Private Const GRID_CTRL_NAME_CURRENCY_DESCRIPTION_TXT As String = "moDescriptionText"
        Private Const GRID_CTRL_NAME_CURRENCY_NOTATION_TXT As String = "moNotationText"
        Private Const GRID_CTRL_NAME_CURRENCY_ISO_CODE_TXT As String = "moISOCodeText"


        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Public Const MSG_NONE_OR_MORE_THAN_ONE_RECORD_FOUND As String = "NONE_OR_MORE_THAN_ONE_RECORD_FOUND"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        'Private MSGTYPELIST As DataView
#End Region

#Region "Page State"
        ' This class keeps the current state for the search page.
        Class MyState
            Public MyBO As Currency
            Public CurrencyID As Guid
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public searchDV As Currency.CurrencySearchDV = Nothing
            Public HasDataChanged As Boolean
            Public IsGridAddNew As Boolean = False
            Public IsEditMode As Boolean = False
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public SortExpression As String = Currency.CurrencySearchDV.COL_NAME_DESCRIPTION
            Public searchCurrencyDescription As String = ""
            Public searchCurrencyCode As String = ""
            Public SearchNotation As String = ""
            Public SearchISOCode As String = ""
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
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            ErrControllerMaster.Clear_Hide()
            Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not IsPostBack Then
                    SortDirection = Currency.CurrencySearchDV.COL_NAME_CODE
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    SetControlState()
                    State.PageIndex = 0
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                Else
                    BindBoPropertiesToGridHeaders()
                    CheckIfComingFromDeleteConfirm()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region

#Region "Grid Handler"

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = Grid.PageIndex
                    State.CurrencyID = Guid.Empty
                    PopulateGrid()
                End If
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

        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.CurrencyID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_CURRENCY_ID_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_CODE_ID), Label).Text)
                    State.MyBO = New Currency(State.CurrencyID)

                    PopulateGrid()

                    State.PageIndex = Grid.PageIndex

                    'Me.SetGridControls(Me.Grid, False)

                    'Set focus on the Code TextBox for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(Grid, GRID_COL_CURRENCY_CODE_IDX, GRID_CTRL_NAME_CURRENCY_CODE_TXT, index)

                    PopulateFormFromBO()

                    SetControlState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.CurrencyID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_CURRENCY_ID_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_CODE_ID), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        'Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing AndAlso Not State.searchDV.Count > 0 Then

                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(GRID_COL_CURRENCY_ID_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_CODE_ID), Label).Text = GetGuidStringFromByteArray(CType(dvRow(Currency.CurrencySearchDV.COL_NAME_CURRENCY_ID), Byte()))

                        If (State.IsEditMode = True _
                                AndAlso State.CurrencyID.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(Currency.CurrencySearchDV.COL_NAME_CURRENCY_ID), Byte())))) Then
                            CType(e.Row.Cells(GRID_COL_CURRENCY_CODE_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_CODE_TXT), TextBox).Text = dvRow(Currency.CurrencySearchDV.COL_NAME_DESCRIPTION).ToString
                            CType(e.Row.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_DESCRIPTION_TXT), TextBox).Text = dvRow(Currency.CurrencySearchDV.COL_NAME_CODE).ToString
                            CType(e.Row.Cells(GRID_COL_NOTATION_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_DESCRIPTION_TXT), TextBox).Text = dvRow(Currency.CurrencySearchDV.COL_NAME_NOTATION).ToString
                            CType(e.Row.Cells(GRID_COL_ISO_CODE_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_DESCRIPTION_TXT), TextBox).Text = dvRow(Currency.CurrencySearchDV.COL_NAME_ISO_CODE).ToString

                        Else
                            CType(e.Row.Cells(GRID_COL_CURRENCY_CODE_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_CODE_LABLE), Label).Text = dvRow(Currency.CurrencySearchDV.COL_NAME_CODE).ToString
                            CType(e.Row.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_DESCRIPTION_LABEL), Label).Text = dvRow(Currency.CurrencySearchDV.COL_NAME_DESCRIPTION).ToString
                            CType(e.Row.Cells(GRID_COL_NOTATION_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_DESCRIPTION_LABEL), Label).Text = dvRow(Currency.CurrencySearchDV.COL_NAME_NOTATION).ToString
                            CType(e.Row.Cells(GRID_COL_ISO_CODE_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_DESCRIPTION_LABEL), Label).Text = dvRow(Currency.CurrencySearchDV.COL_NAME_ISO_CODE).ToString
                        End If
                        'e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub Grid_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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

        Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region

#Region "Button click Handler"
        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try
                SearchCodeTextBox.Text = String.Empty
                SearchDescriptionTextBox.Text = String.Empty
                SearchNotationTextBox.Text = String.Empty
                SearchISOCodeTextBox.Text = String.Empty
                State.searchCurrencyCode = String.Empty
                State.searchCurrencyDescription = String.Empty
                State.SearchNotation = String.Empty
                State.SearchISOCode = String.Empty
                State.IsGridAddNew = False
                Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                With State
                    .CurrencyID = Guid.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                With State
                    .PageIndex = 0
                    .CurrencyID = Guid.Empty
                    .IsGridVisible = True
                    .searchDV = Nothing
                    .HasDataChanged = False
                    .IsGridAddNew = False
                    .searchCurrencyCode = SearchCodeTextBox.Text.Trim
                    .searchCurrencyDescription = SearchDescriptionTextBox.Text.Trim
                    .SearchNotation = SearchNotationTextBox.Text.Trim
                    .SearchISOCode = SearchISOCodeTextBox.Text.Trim
                End With
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.IsGridAddNew = True
                AddNew()
                'Me.AddNewRiskType()
                SetControlState()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

            Try
                PopulateBOFromForm()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    State.IsGridAddNew = False
                    AddInfoMsg(MSG_RECORD_SAVED_OK)
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Try
                With State
                    If .IsGridAddNew Then
                        RemoveNewRowFromSearchDV()
                        .IsGridAddNew = False
                        'Grid.PageIndex = .PageIndex
                    End If
                    .CurrencyID = Guid.Empty
                    .IsEditMode = False
                End With
                'Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                State.searchDV = Nothing
                PopulateGrid()
                SetControlState()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Control"

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Grid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
            SetControlState()

        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            'Me.HiddenDeletePromptResponse.Value = ""
        End Sub


        Private Sub DoDelete()
            'Do the delete here

            'Save the CurrencyId in the Session

            Dim CurrencyBO As Currency = New Currency(State.CurrencyID)

            CurrencyBO.Delete()

            'Call the Save() method in the Currency Business Object here

            CurrencyBO.Save()

            State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
            State.IsEditMode = False
            SetControlState()
        End Sub
#End Region

#Region "Helper functions"

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "Description", Grid.Columns(GRID_COL_DESCRIPTION_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "Code", Grid.Columns(GRID_COL_CURRENCY_CODE_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "Notation", Grid.Columns(GRID_COL_NOTATION_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "ISOCode", Grid.Columns(GRID_COL_ISO_CODE_IDX))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub PopulateFormFromBO()

            Dim gridRowIdx As Integer = Grid.EditIndex
            Try
                With State.MyBO
                    If (.Code IsNot Nothing) Then
                        CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_CURRENCY_CODE_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_CODE_TXT), TextBox).Text = .Code
                    End If
                    If (.Description IsNot Nothing) Then
                        CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_DESCRIPTION_TXT), TextBox).Text = .Description
                    End If

                    If (.Notation IsNot Nothing) Then
                        CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_NOTATION_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_NOTATION_TXT), TextBox).Text = .Notation
                    End If

                    If (.IsoCode IsNot Nothing) Then
                        CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_ISO_CODE_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_ISO_CODE_TXT), TextBox).Text = .IsoCode
                    End If

                    CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_CURRENCY_ID_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_CODE_ID), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Function PopulateBOFromForm() As Boolean

            With State.MyBO
                .Code = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_CURRENCY_CODE_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_CODE_TXT), TextBox).Text.Trim.ToUpper
                .Description = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_DESCRIPTION_TXT), TextBox).Text.Trim
                .Notation = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_NOTATION_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_NOTATION_TXT), TextBox).Text.Trim.ToUpper
                .IsoCode = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_ISO_CODE_IDX).FindControl(GRID_CTRL_NAME_CURRENCY_ISO_CODE_TXT), TextBox).Text.Trim
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Function

        Public Sub PopulateGrid()

            Dim dv As DataView
            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetDV()
                End If
                State.searchDV.Sort = SortDirection
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.CurrencyID, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.CurrencyID, Grid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
                End If
                'If Me.State.searchDV.Count > 0 Then
                Grid.AutoGenerateColumns = False
                Grid.Columns(GRID_COL_CURRENCY_CODE_IDX).SortExpression = Currency.CurrencySearchDV.COL_NAME_CODE
                Grid.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = Currency.CurrencySearchDV.COL_NAME_DESCRIPTION
                Grid.Columns(GRID_COL_NOTATION_IDX).SortExpression = Currency.CurrencySearchDV.COL_NAME_NOTATION
                Grid.Columns(GRID_COL_ISO_CODE_IDX).SortExpression = Currency.CurrencySearchDV.COL_NAME_ISO_CODE
                SortAndBindGrid()
                'End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Function GetDV() As Currency.CurrencySearchDV

            State.searchDV = GetCurrencyGridDataView()
            State.searchDV.Sort = Grid.DataMember()
            Grid.DataSource = State.searchDV

            Return (State.searchDV)

        End Function

        Private Function GetCurrencyGridDataView() As Currency.CurrencySearchDV

            With State
                Return Currency.getList(.searchCurrencyCode, .searchCurrencyDescription, .SearchNotation, .SearchISOCode)
            End With

        End Function

        Private Sub SortAndBindGrid()

            TranslateGridControls(Grid)

            If (State.searchDV.Count = 0) Then
                State.searchDV = Nothing
                State.MyBO = New Currency
                State.MyBO.AddNewRowToCurrencySearchDV(State.searchDV, State.MyBO)
                Grid.DataSource = State.searchDV
                Grid.DataBind()
                Grid.Rows(0).Visible = False
                State.IsGridAddNew = True
            Else
                Grid.Enabled = True
                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
            End If
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True


            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.searchDV.Count

            If Grid.Visible Then
                If (State.IsGridAddNew) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

        End Sub

        Private Sub SetControlState()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, btnNew, False)
                ControlMgr.SetVisibleControl(Me, btnCancel, True)
                ControlMgr.SetVisibleControl(Me, btnSave, True)
                ControlMgr.SetEnableControl(Me, btnSearch, False)
                ControlMgr.SetEnableControl(Me, btnClearSearch, False)
                MenuEnabled = False
                If (cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnNew, True)
                ControlMgr.SetVisibleControl(Me, btnCancel, False)
                ControlMgr.SetVisibleControl(Me, btnSave, False)
                ControlMgr.SetEnableControl(Me, btnSearch, True)
                ControlMgr.SetEnableControl(Me, btnClearSearch, True)
                MenuEnabled = True
                If Not (cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub

        Private Sub AddNew()

            State.MyBO = New Currency
            State.CurrencyID = State.MyBO.Id
            State.MyBO.AddNewRowToCurrencySearchDV(State.searchDV, State.MyBO)
            State.IsGridAddNew = True
            'SetGridControls(Me.Grid, False)
            PopulateGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.CurrencyID, Grid, _
                                               State.PageIndex, State.IsEditMode)
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            SetGridControls(Grid, False)
        End Sub

        Private Function GetRowIndexFromSearchDVByID(CurrencyID As Guid) As Integer
            Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
            With State
                If .searchDV IsNot Nothing Then
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, CurrencyID)
                End If
            End With
            Return rowind
        End Function
        Private Sub RemoveNewRowFromSearchDV()
            Dim rowind As Integer = GetRowIndexFromSearchDVByID(State.CurrencyID)
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub
#End Region

    End Class
End Namespace
